using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    IngresoPersonalService _ingresoPersonalService = new IngresoPersonalService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_ingresoPersonalService.CodigoPrograma.ToString(), "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoNuevo += (s, evt) =>
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_ingresoPersonalService.CodigoPrograma + ".idIngreso");
                Session.Remove(_ingresoPersonalService.CodigoPrograma + ".idEmpleado");
                Navegar(Pagina.Nuevo);
            };
            toolBar.eventoConsultar += btnConsultar_Click;

            ctlMensaje.eventoClick += ctlMensaje_eventoClick;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ingresoPersonalService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_ingresoPersonalService.CodigoPrograma + ".idIngreso");
                Session.Remove(_ingresoPersonalService.CodigoPrograma + ".idEmpleado");

                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ingresoPersonalService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlNomina);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto);
    }


    #endregion


    #region Eventos Botones


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigoIngreso.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        ddlCentroCosto.SelectedIndex = 0;
        ddlNomina.SelectedIndex = 0;
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        gvIngresos.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvIngresos, "Cpontratos");

        gvIngresos.AllowPaging = true;
        Actualizar();
    }


    #endregion


    #region Eventos Grillas


    protected void gvIngresos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvIngresos.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvIngresos_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvIngresos.SelectedRow.Cells[2].Text);

        Session[_ingresoPersonalService.CodigoPrograma + ".idIngreso"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvIngresos.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

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
                _ingresoPersonalService.EliminarIngresoPersonal(idBorrar, Usuario);
                ViewState.Remove("idBorrar");
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvIngresos_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

            List<IngresoPersonal> lstConsulta = _ingresoPersonalService.ListarIngresoPersonal(filtro, Usuario);

            gvIngresos.DataSource = lstConsulta;
            gvIngresos.DataBind();
            lblNumeroRegistros.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_ingresoPersonalService.CodigoPrograma, "Actualizar", ex);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoIngreso.Text))
        {
            filtro += " and int.consecutivo = " + txtCodigoIngreso.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion LIKE '%" + txtIdentificacion.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue))
        {
            filtro += " and int.codigocentrocosto = " + ddlCentroCosto.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlNomina.SelectedValue))
        {
            filtro += " and int.codigonomina = " + ddlNomina.SelectedValue;
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }


    #endregion


}
