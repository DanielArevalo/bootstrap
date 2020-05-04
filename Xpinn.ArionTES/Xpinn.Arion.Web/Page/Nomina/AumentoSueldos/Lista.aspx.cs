using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    AumentoSueldoService _aumentoSueldoService = new AumentoSueldoService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_aumentoSueldoService.CodigoPrograma.ToString(), "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoNuevo += (s, evt) =>
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_aumentoSueldoService.CodigoPrograma + ".idAumento");
                Session.Remove(_aumentoSueldoService.CodigoPrograma + ".idEmpleado");
                Navegar(Pagina.Nuevo);
            };
            toolBar.eventoConsultar += btnConsultar_Click;

            ctlMensaje.eventoClick += ctlMensaje_eventoClick;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_aumentoSueldoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_aumentoSueldoService.CodigoPrograma + ".idAumento");
                Session.Remove(_aumentoSueldoService.CodigoPrograma + ".idEmpleado");

                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_aumentoSueldoService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        txtFechaCambio.Attributes.Add("readonly", "readonly");
    }


    #endregion


    #region Eventos Botones


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigoAumento.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtFechaCambio.Text = string.Empty;
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        gvAumentos.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvAumentos, "Aumentos");

        gvAumentos.AllowPaging = true;
        Actualizar();
    }


    #endregion


    #region Eventos Grillas


    protected void gvAumentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAumentos.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvAumentos_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvAumentos.SelectedRow.Cells[2].Text);
        long codEmpleado = Convert.ToInt64(gvAumentos.SelectedRow.Cells[3].Text);

        Session[_aumentoSueldoService.CodigoPrograma + ".idAumento"] = id;
        Session[_aumentoSueldoService.CodigoPrograma + ".idEmpleado"] = codEmpleado;
        Session.Remove(_aumentoSueldoService.CodigoPrograma + ".cargamasiva");
        Navegar(Pagina.Detalle);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvAumentos.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

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
                _aumentoSueldoService.EliminarAumentoSueldo(idBorrar, Usuario);
                ViewState.Remove("idBorrar");
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvAumentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnCargarDatos_Click(object sender, EventArgs e)
    {
        long id = 1;
        Session[_aumentoSueldoService.CodigoPrograma + ".cargamasiva"] = id;
        Navegar(Pagina.Detalle);
    }


    #endregion


    #region Metodos Ayuda


    public void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<AumentoSueldo> lstConsulta = _aumentoSueldoService.ListarAumentoSueldo(filtro, Usuario);

            gvAumentos.DataSource = lstConsulta;
            gvAumentos.DataBind();
            lblNumeroRegistros.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_aumentoSueldoService.CodigoPrograma, "Actualizar", ex);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoAumento.Text))
        {
            filtro += " and aum.consecutivo = " + txtCodigoAumento.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion LIKE '%" + txtIdentificacion.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            filtro += " and per.nombre LIKE '%" + txtNombre.Text.Trim().ToUpperInvariant() + "%' ";    
        }

        if (!string.IsNullOrWhiteSpace(txtFechaCambio.Text) && !string.IsNullOrWhiteSpace(txtFechaCambio.Text))
        {
            filtro += " and TRUNC(aum.FECHA) >= to_date('" + txtFechaCambio.Text + "', 'dd/MM/yyyy') ";
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }


    #endregion



  
}
