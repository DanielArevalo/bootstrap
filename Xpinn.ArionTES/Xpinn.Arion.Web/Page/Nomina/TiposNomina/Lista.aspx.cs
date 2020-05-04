using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    NominaEmpleadoService _nominaEmpleadoService = new NominaEmpleadoService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_nominaEmpleadoService.CodigoPrograma.ToString(), "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.eventoNuevo += (s, evt) =>
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_nominaEmpleadoService.CodigoPrograma + ".idNomina");
                Navegar(Pagina.Detalle);
            };
            toolBar.eventoConsultar += btnConsultar_Click;

            ctlMensaje.eventoClick += ctlMensaje_eventoClick;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_nominaEmpleadoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_nominaEmpleadoService.CodigoPrograma + ".idNomina");

                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_nominaEmpleadoService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.Oficinas, ddlOficina);
        //LlenarListasDesplegables(TipoLista.Contratacion, ddlTipoContrato);



        IngresoPersonalService ingresoservicio = new IngresoPersonalService();
        IngresoPersonal ingreso = new IngresoPersonal();
        ddlTipoContrato.DataSource = ingresoservicio.ListarContratacion(ingreso, (Usuario)Session["usuario"]);
        ddlTipoContrato.DataTextField = "descripcion";
        ddlTipoContrato.DataValueField = "consecutivo";
        ddlTipoContrato.DataBind();

    }


    #endregion


    #region Eventos Botones


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigoNomina.Text = string.Empty;
        txtNombreNomina.Text = string.Empty;
        ddlOficina.SelectedIndex = 0;
        ddlTipoContrato.SelectedIndex = 0;
    }

    void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        gvNominas.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvNominas, "Nominas");

        gvNominas.AllowPaging = true;
        Actualizar();
    }


    #endregion


    #region Eventos Grillas


    protected void gvNominas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNominas.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvNominas_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvNominas.SelectedRow.Cells[2].Text);

        Session[_nominaEmpleadoService.CodigoPrograma + ".IdNomina"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvNominas.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

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
                _nominaEmpleadoService.EliminarNominaEmpleado(idBorrar, Usuario);
                ViewState.Remove("idBorrar");
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvNominas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


    #endregion


    #region Metodos Ayuda


    public void Actualizar()
    {
        try
        {
            VerError("");
            string filtro = ObtenerFiltro();

            List<NominaEmpleado> lstConsulta = _nominaEmpleadoService.ListarNominaEmpleado(filtro, Usuario);

            gvNominas.DataSource = lstConsulta;
            gvNominas.DataBind();
            lblNumeroRegistros.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_nominaEmpleadoService.CodigoPrograma, "Actualizar", ex);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoNomina.Text))
        {
            filtro += " and nom.consecutivo = " + txtCodigoNomina.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtNombreNomina.Text))
        {
            filtro += " and nom.descripcion LIKE '%" + txtNombreNomina.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(ddlOficina.SelectedValue))
        {
            filtro += " and nom.CODIGOOFICINA = " + ddlOficina.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlTipoContrato.SelectedValue))
        {
            filtro += " and nom.CODIGOTIPOCONTRATO = " + ddlTipoContrato.SelectedValue;
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }


    #endregion


}
