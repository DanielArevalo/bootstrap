using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    LiquidacionVacacionesEmpleadoService _vVacacionesService = new LiquidacionVacacionesEmpleadoService();
    List<ErroresCarga> _lstErroresCarga;
    List<LiquidacionVacacionesEmpleado> _lstDiasVavaciones;
    int _contadorRegistro;


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_vVacacionesService.CodigoPrograma2.ToString(), "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
              toolBar.eventoNuevo += (s, evt) =>
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_vVacacionesService.CodigoPrograma2 + ".idDiasVacaciones");
                Session.Remove(_vVacacionesService.CodigoPrograma2 + ".idEmpleado");
                Navegar(Pagina.Nuevo);
            };
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += ctlMensaje_eventoClick;

            toolBar.MostrarCancelar(false);
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_vVacacionesService.CodigoPrograma2, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_vVacacionesService.CodigoPrograma2 + ".idDiasVacaciones");
                Session.Remove(_vVacacionesService.CodigoPrograma2 + ".idEmpleado");

                InicializarPagina();
            }
            else
            {
                if (ViewState["_lstDiasVavaciones"] != null)
                {
                    _lstDiasVavaciones = (List<LiquidacionVacacionesEmpleado>)ViewState["_lstDiasVavaciones"];
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_vVacacionesService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlNomina);
    }


    #endregion


    #region Eventos Botones


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigoEmpleado.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        txtNombre.Text = string.Empty;
        ddlNomina.SelectedIndex = 0;
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        gvDiasVacaciones.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvDiasVacaciones, "Dias Vacaciones");

        gvDiasVacaciones.AllowPaging = true;
        Actualizar();
    }

  
    void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.SetActiveView(viewPrincipal);

        Site toolBar = (Site)Master;
        toolBar.MostrarCancelar(false);
        toolBar.MostrarImportar(true);
        toolBar.MostrarGuardar(false);

        toolBar.MostrarLimpiar(true);
        toolBar.MostrarExportar(true);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarNuevo(true);
    }


    #endregion


    #region Eventos Grillas


    protected void gvDiasVacaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDiasVacaciones.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvDiasVacaciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvDiasVacaciones.SelectedRow.Cells[2].Text);
        long idEmpleado = Convert.ToInt64(gvDiasVacaciones.SelectedRow.Cells[3].Text);

        Session[_vVacacionesService.CodigoPrograma2 + ".idDiasVacaciones"] = id;
        Session[_vVacacionesService.CodigoPrograma2 + ".idEmpleado"] = idEmpleado;

        Navegar(Pagina.Detalle);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvDiasVacaciones.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

        ViewState.Add("idBorrar", idBorrar);

        ctlMensaje.MostrarMensaje("Seguro que deseas eliminar esta registro?");
    }

    void ctlMensaje_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
            long idBorrar = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {
                //_vVacacionesService.EliminarHorasExtrasEmpleados(idBorrar, Usuario);
                ViewState.Remove("idBorrar");
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvDiasVacaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    #endregion


    #region Metodos Ayuda


    public void Actualizar()
    {
        try
        {
            string filtro = ObtenerFiltro();

            List<LiquidacionVacacionesEmpleado> lstConsulta = _vVacacionesService.ListarDiasVacaciones(filtro, Usuario);

            gvDiasVacaciones.DataSource = lstConsulta;
            gvDiasVacaciones.DataBind();
            lblNumeroRegistros.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_vVacacionesService.CodigoPrograma2, "Actualizar", ex);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoEmpleado.Text))
        {
            filtro += " and nov.CODIGOEMPLEADO = " + txtCodigoEmpleado.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion LIKE '%" + txtIdentificacion.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            filtro += " and per.nombre LIKE '%" + txtNombre.Text.Trim().ToUpperInvariant() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(ddlNomina.SelectedValue))
        {
            filtro += " and nov.CodigoNomina = " + ddlNomina.SelectedValue;
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }


    #endregion

    
   
}