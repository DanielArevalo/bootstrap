using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    LiquidacionVacacionesEmpleadoService _liquidacionVacacionesServices = new LiquidacionVacacionesEmpleadoService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_liquidacionVacacionesServices.CodigoPrograma.ToString(), "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoNuevo += (s, evt) =>
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_liquidacionVacacionesServices.CodigoPrograma + ".idLiquidacion");
                Session.Remove(_liquidacionVacacionesServices.CodigoPrograma + ".idEmpleado");
                Navegar(Pagina.Nuevo);
            };
            toolBar.eventoConsultar += btnConsultar_Click;

            ctlMensaje.eventoClick += ctlMensaje_eventoClick;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionVacacionesServices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_liquidacionVacacionesServices.CodigoPrograma + ".idLiquidacion");
                Session.Remove(_liquidacionVacacionesServices.CodigoPrograma + ".idEmpleado");

                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionVacacionesServices.GetType().Name + "A", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlNomina);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto);

        txtFechaInicio.Attributes.Add("readonly", "readonly");
        txtFechaTerminacion.Attributes.Add("readonly", "readonly");
    }


    #endregion


    #region Eventos Botones


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigoVacacionIndividual.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtCodigoEmpleado.Text = string.Empty;
        txtFechaInicio.Text = string.Empty;
        txtFechaTerminacion.Text = string.Empty;
        ddlNomina.SelectedIndex = 0;
        ddlCentroCosto.SelectedIndex = 0;
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        gvVacaciones.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvVacaciones, "Vacaciones");

        gvVacaciones.AllowPaging = true;
        Actualizar();
    }


    #endregion


    #region Eventos Grillas


    protected void gvVacaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvVacaciones.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvVacaciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvVacaciones.SelectedRow.Cells[2].Text);
        long idEmpleado = Convert.ToInt64(gvVacaciones.SelectedRow.Cells[3].Text);

        Session[_liquidacionVacacionesServices.CodigoPrograma + ".idLiquidacion"] = id;
        Session[_liquidacionVacacionesServices.CodigoPrograma + ".idEmpleado"] = idEmpleado;
        Navegar(Pagina.Detalle);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvVacaciones.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

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
                _liquidacionVacacionesServices.EliminarLiquidacionVacacionesEmpleado(idBorrar, Usuario);
                ViewState.Remove("idBorrar");
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvVacaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


    #endregion


    #region Metodos Ayuda


    public void Actualizar()
    {
        try
        {
            string filtro = ObtenerFiltro();

            List<LiquidacionVacacionesEmpleado> lstConsulta = _liquidacionVacacionesServices.ListarLiquidacionVacacionesEmpleado(filtro, Usuario);

            gvVacaciones.DataSource = lstConsulta;
            gvVacaciones.DataBind();
            lblNumeroRegistros.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionVacacionesServices.CodigoPrograma, "Actualizar", ex);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoVacacionIndividual.Text))
        {
            filtro += " and liq.consecutivo = " + txtCodigoVacacionIndividual.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion LIKE '%" + txtIdentificacion.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            filtro += " and per.nombre LIKE '%" + txtNombre.Text.Trim().ToUpperInvariant() + "%' ";    
        }

        if (!string.IsNullOrWhiteSpace(txtFechaInicio.Text) && !string.IsNullOrWhiteSpace(txtFechaTerminacion.Text))
        {
            filtro = " and TRUNC(liq.FECHAINICIO) >= to_date('" + txtFechaInicio.Text + "', 'dd/MM/yyyy') and TRUNC(liq.FECHATERMINACION) <= to_date('" + txtFechaTerminacion.Text + "', 'dd/MM/yyyy') ";
        }
        else if (!string.IsNullOrWhiteSpace(txtFechaInicio.Text))
        {
            filtro += " and TRUNC(liq.FECHAINICIO) >= to_date('" + txtFechaInicio.Text + "', 'dd/MM/yyyy') ";
        }
        else if(!string.IsNullOrWhiteSpace(txtFechaTerminacion.Text))
        {
            filtro += " and TRUNC(liq.FECHATERMINACION) <= to_date('" + txtFechaTerminacion.Text + "', 'dd/MM/yyyy') ";
        }

        if (!string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue))
        {
            filtro += " and liq.CodigoCentroCosto = " + ddlCentroCosto.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlNomina.SelectedValue))
        {
            filtro += " and liq.CodigoNomina = " + ddlNomina.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(txtCodigoEmpleado.Text))
        {
            filtro += " and liq.CodigoEmpleado = " + txtCodigoEmpleado.Text;
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }


    #endregion


}
