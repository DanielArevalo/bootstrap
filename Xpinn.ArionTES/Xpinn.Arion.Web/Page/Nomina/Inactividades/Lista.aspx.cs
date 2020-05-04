using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    InactividadesService _inactividadService = new InactividadesService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_inactividadService.CodigoPrograma.ToString(), "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoNuevo += (s, evt) =>
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_inactividadService.CodigoPrograma + ".idInactividad");
                Session.Remove(_inactividadService.CodigoPrograma + ".idEmpleado");
                Navegar(Pagina.Nuevo);
            };
            toolBar.eventoConsultar += btnConsultar_Click;

            ctlMensaje.eventoClick += ctlMensaje_eventoClick;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_inactividadService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_inactividadService.CodigoPrograma + ".idInactividad");
                Session.Remove(_inactividadService.CodigoPrograma + ".idEmpleado");

                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_inactividadService.GetType().Name + "A", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoContrato, ddlContrato);
        LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlNomina);

        PagosDescuentosFijosService conceptoService = new PagosDescuentosFijosService();
        PagosDescuentosFijos concepto = new PagosDescuentosFijos();
        string filtro = ObtenerFiltroClase();
        ddlClase.DataSource = conceptoService.ListarConceptosNomina(filtro, (Usuario)Session["usuario"]);
        ddlClase.DataTextField = "descripcion";
        ddlClase.DataValueField = "consecutivo";
        ddlClase.DataBind();
        ddlClase.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));




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
        txtCodigoInactividad.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        txtNombre.Text = string.Empty;
        checkRemunerado.ClearSelection();
        txtFechaInicio.Text = string.Empty;
        txtFechaTerminacion.Text = string.Empty;
        ddlTipo.SelectedIndex = 0;
        ddlClase.SelectedIndex = 0;
        ddlContrato.SelectedIndex = 0;
        ddlNomina.SelectedIndex = 0;
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        gvInactividades.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvInactividades, "Pagos");

        gvInactividades.AllowPaging = true;
        Actualizar();
    }


    #endregion


    #region Eventos Grillas


    protected void gvInactividades_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInactividades.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvInactividades_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvInactividades.SelectedRow.Cells[2].Text);
        long idEmpleado = Convert.ToInt64(gvInactividades.SelectedRow.Cells[3].Text);

        Session[_inactividadService.CodigoPrograma + ".idInactividad"] = id;
        Session[_inactividadService.CodigoPrograma + ".idEmpleado"] = idEmpleado;
        Navegar(Pagina.Detalle);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvInactividades.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

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
                _inactividadService.EliminarInactividades(idBorrar, Usuario);
                ViewState.Remove("idBorrar");
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvInactividades_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }


    #endregion


    #region Metodos Ayuda


    public void Actualizar()
    {
        try
        {
            string filtro = ObtenerFiltro();

            List<Inactividades> lstConsulta = _inactividadService.ListarInactividades(filtro, Usuario);

            gvInactividades.DataSource = lstConsulta;
            gvInactividades.DataBind();
            lblNumeroRegistros.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_inactividadService.CodigoPrograma, "Actualizar", ex);
        }
    }
    string ObtenerFiltroClase()
    {
        string filtro = string.Empty;
        filtro += " and a.tipoconcepto  in (11)";


        return filtro;
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoInactividad.Text))
        {
            filtro += " and ina.consecutivo = " + txtCodigoInactividad.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion LIKE '%" + txtIdentificacion.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            filtro += " and per.nombre LIKE '%" + txtNombre.Text.Trim().ToUpperInvariant() + "%' ";    
        }

        if (!string.IsNullOrWhiteSpace(checkRemunerado.SelectedValue))
        {
            filtro += " and ina.REMUNERADA = = " + checkRemunerado.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(txtFechaInicio.Text) && !string.IsNullOrWhiteSpace(txtFechaTerminacion.Text))
        {
            filtro = " and TRUNC(ina.FECHAINICIO) >= to_date('" + txtFechaInicio.Text + "', 'dd/MM/yyyy') and TRUNC(ina.FECHATERMINACION) <= to_date('" + txtFechaTerminacion.Text + "', 'dd/MM/yyyy') ";
        }
        else if (!string.IsNullOrWhiteSpace(txtFechaInicio.Text))
        {
            filtro += " and TRUNC(ina.FECHAINICIO) >= to_date('" + txtFechaInicio.Text + "', 'dd/MM/yyyy') ";
        }
        else if(!string.IsNullOrWhiteSpace(txtFechaTerminacion.Text))
        {
            filtro += " and TRUNC(ina.FECHATERMINACION) <= to_date('" + txtFechaTerminacion.Text + "', 'dd/MM/yyyy') ";
        }

        if (!string.IsNullOrWhiteSpace(ddlTipo.SelectedValue))
        {
            filtro += " and ina.TIPO = " + ddlTipo.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlClase.SelectedValue) && ddlClase.SelectedValue != "0")
        {
            filtro += " and ina.CODIGOCONCEPTO = " + ddlClase.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlContrato.SelectedValue))
        {
            filtro += " and per.COD_TIPOCONTRATO = " + ddlContrato.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlNomina.SelectedValue) )
        {
            filtro += " and ina.CodigoNomina = " + ddlNomina.SelectedValue;
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }


    #endregion


}
