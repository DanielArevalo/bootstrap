using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Lista : GlobalWeb
{
    PagosDescuentosFijosService _pagosDescuentosServices = new PagosDescuentosFijosService();


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_pagosDescuentosServices.CodigoPrograma.ToString(), "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoNuevo += (s, evt) =>
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idRegistroPago");
                Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idEmpleado");
                Navegar(Pagina.Nuevo);
            };
            toolBar.eventoConsultar += btnConsultar_Click;

            ctlMensaje.eventoClick += ctlMensaje_eventoClick;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_pagosDescuentosServices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idRegistroPago");
                Session.Remove(_pagosDescuentosServices.CodigoPrograma + ".idEmpleado");

                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_pagosDescuentosServices.GetType().Name + "A", "Page_Load", ex);
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.ConceptoNomina, ddlConcepto);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto);

        txtFecha.Attributes.Add("readonly", "readonly");
    }


    #endregion


    #region Eventos Botones


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtCodigoPago.Text = string.Empty;
        txtIdentificacion.Text = string.Empty;
        txtNombre.Text = string.Empty;
        txtValorCuota.Text = string.Empty;
        txtFecha.Text = string.Empty;
        ddlCentroCosto.SelectedIndex = 0;
        ddlConcepto.SelectedIndex = 0;
        ddlDescuentoPeriocidad.SelectedIndex = 0;
    }

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
        gvPagos.AllowPaging = false;
        Actualizar();

        ExportarGridViewEnExcel(gvPagos, "Pagos");

        gvPagos.AllowPaging = true;
        Actualizar();
    }


    #endregion


    #region Eventos Grillas


    protected void gvPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPagos.PageIndex = e.NewPageIndex;
        Actualizar();
    }

    protected void gvPagos_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Convert.ToInt64(gvPagos.SelectedRow.Cells[2].Text);
        long idEmpleado = Convert.ToInt64(gvPagos.SelectedRow.Cells[3].Text);

        Session[_pagosDescuentosServices.CodigoPrograma + ".idRegistroPago"] = id;
        Session[_pagosDescuentosServices.CodigoPrograma + ".idEmpleado"] = idEmpleado;
        Navegar(Pagina.Detalle);
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Delete") return;

        long idBorrar = Convert.ToInt64(gvPagos.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);

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
                _pagosDescuentosServices.EliminarPagosDescuentosFijos(idBorrar, Usuario);
                ViewState.Remove("idBorrar");
                Actualizar();
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
    }

    protected void gvPagos_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

            List<PagosDescuentosFijos> lstConsulta = _pagosDescuentosServices.ListarPagosDescuentosFijos(filtro, Usuario);

            gvPagos.DataSource = lstConsulta;
            gvPagos.DataBind();
            lblNumeroRegistros.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_pagosDescuentosServices.CodigoPrograma, "Actualizar", ex);
        }
    }

    string ObtenerFiltro()
    {
        string filtro = string.Empty;

        if (!string.IsNullOrWhiteSpace(txtCodigoPago.Text))
        {
            filtro += " and pago.consecutivo = " + txtCodigoPago.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            filtro += " and per.identificacion LIKE '%" + txtIdentificacion.Text.Trim() + "%' ";
        }

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            filtro += " and per.nombre LIKE '%" + txtNombre.Text.Trim().ToUpperInvariant() + "%' ";    
        }

        if (!string.IsNullOrWhiteSpace(txtValorCuota.Text))
        {
            filtro += " and pago.VALORCUOTA = " + txtValorCuota.Text.Trim();
        }

        if (!string.IsNullOrWhiteSpace(txtFecha.Text))
        {
            filtro += " and TRUNC(pago.FECHA) >= to_date('" + txtFecha.Text + "', 'dd/MM/yyyy') ";
        }

        if (!string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue))
        {
            filtro += " and pago.CODIGOCENTROCOSTOS = " + ddlCentroCosto.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlConcepto.SelectedValue))
        {
            filtro += " and pago.CODIGOCONCEPTONOMINA = " + ddlConcepto.SelectedValue;
        }

        if (!string.IsNullOrWhiteSpace(ddlDescuentoPeriocidad.SelectedValue))
        {
            filtro += " and pago.DESCUENTOPERIOCIDAD = " + ddlDescuentoPeriocidad.SelectedValue;
        }

        StringHelper stringHelper = new StringHelper();
        filtro = stringHelper.QuitarAndYColocarWhereEnFiltroQuery(filtro);

        return filtro;
    }


    #endregion


}
