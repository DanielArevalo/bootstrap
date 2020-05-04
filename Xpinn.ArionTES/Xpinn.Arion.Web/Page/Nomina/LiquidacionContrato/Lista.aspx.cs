using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    LiquidacionContratoService _liquidacionContratoService = new LiquidacionContratoService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_liquidacionContratoService.CodigoPrograma.ToString(), "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoNuevo += (s, evt) =>
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_liquidacionContratoService.CodigoPrograma + ".idLiquidacion");
                Session.Remove(_liquidacionContratoService.CodigoPrograma + ".idEmpleado");
                Navegar(Pagina.Nuevo);
            };
            toolBar.eventoConsultar += btnConsultar_Click;

            ctlMensaje.eventoClick += ctlMensaje_eventoClick;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionContratoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_liquidacionContratoService.CodigoPrograma + ".idLiquidacion");
                Session.Remove(_liquidacionContratoService.CodigoPrograma + ".idEmpleado");

                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionContratoService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlNomina);

        txtFechaRetiro.Attributes.Add("readonly", "readonly");
    }


    #endregion


    #region Eventos Botones


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigoLiquidacion.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtFechaRetiro.Text = string.Empty;
        ddlNomina.SelectedIndex = 0;
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        gvLiquidacion.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvLiquidacion, "Liquidacion Contrato");

        gvLiquidacion.AllowPaging = true;
        Actualizar();
    }


    #endregion


    #region Eventos Grillas


    protected void gvLiquidacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLiquidacion.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvLiquidacion.SelectedRow.Cells[2].Text);
        long idEmpleado = Convert.ToInt64(gvLiquidacion.SelectedRow.Cells[3].Text);

        Session[_liquidacionContratoService.CodigoPrograma + ".idLiquidacion"] = id;
        Session[_liquidacionContratoService.CodigoPrograma + ".idEmpleado"] = idEmpleado;
        Navegar(Pagina.Detalle);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvLiquidacion.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

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
                _liquidacionContratoService.EliminarLiquidacionContrato(idBorrar, Usuario);
                ViewState.Remove("idBorrar");
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvLiquidacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


    #endregion


    #region Metodos Ayuda


    public void Actualizar()
    {
        try
        {
            string filtro = ObtenerFiltro();

            List<LiquidacionContrato> lstConsulta = _liquidacionContratoService.ListarLiquidacionContrato(filtro, Usuario);

            gvLiquidacion.DataSource = lstConsulta;
            gvLiquidacion.DataBind();
            lblNumeroRegistros.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionContratoService.CodigoPrograma, "Actualizar", ex);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoLiquidacion.Text))
        {
            filtro += " and liq.consecutivo = " + txtCodigoLiquidacion.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion LIKE '%" + txtIdentificacion.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            filtro += " and per.nombre LIKE '%" + txtNombre.Text.Trim().ToUpperInvariant() + "%' ";    
        }

        if(!string.IsNullOrWhiteSpace(txtFechaRetiro.Text))
        {
            filtro += " and TRUNC(liq.FECHARETIRO) <= to_date('" + txtFechaRetiro.Text + "', 'dd/MM/yyyy') ";
        }

        if (!string.IsNullOrWhiteSpace(ddlNomina.SelectedValue))
        {
            filtro += " and liq.CodigoNomina = " + ddlNomina.SelectedValue;
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }


    #endregion


}
