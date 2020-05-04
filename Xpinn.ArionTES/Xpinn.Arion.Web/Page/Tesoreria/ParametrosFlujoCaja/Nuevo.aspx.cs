using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    ParametrosFlujoCajaService ParamFlujoCajaServicio = new ParametrosFlujoCajaService();
    Int64 idObjeto = 0;
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ParamFlujoCajaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ParamFlujoCajaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ParamFlujoCajaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParamFlujoCajaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Session["DetalleCuentas"] = null;
                if (Session[ParamFlujoCajaServicio.CodigoPrograma + ".cod_concepto"] == null)
                    CrearDetalleInicial();
                if (Session[ParamFlujoCajaServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Convert.ToInt64(Session[ParamFlujoCajaServicio.CodigoPrograma + ".id"]);
                    Session.Remove(ParamFlujoCajaServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParamFlujoCajaServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    #region Evento Detalles

    /// <summary>
    /// Método para insertar un detalle en blanco para cuando la grilla no tiene datos
    /// </summary>
    private void CrearDetalleInicial()
    {
        List<ParametrosFlujoCaja> LstDetalleCuentas = new List<ParametrosFlujoCaja>();
        for (int i = 1; i <= 1; i++)
        {
            ParametrosFlujoCaja pDetalleCuenta = new ParametrosFlujoCaja();
            pDetalleCuenta.cod_cuenta_con = -1;
            if (txtCodConcepto.Text != "")
                pDetalleCuenta.cod_concepto = Convert.ToInt64(txtCodConcepto.Text);
            pDetalleCuenta.cod_cuenta = null;
            pDetalleCuenta.nom_cuenta = "";
            pDetalleCuenta.tipo_mov = "D";

            LstDetalleCuentas.Add(pDetalleCuenta);
        }
        gvCuentas.DataSource = LstDetalleCuentas;
        gvCuentas.DataBind();
    }

    protected void ObtenerDatos(Int64 idObjeto)
    {
        try
        {
            ParametrosFlujoCaja vConcepto = new ParametrosFlujoCaja();
            vConcepto = ParamFlujoCajaServicio.ConsultarConceptoCuenta(idObjeto, (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vConcepto.cod_concepto.ToString()))
                txtCodConcepto.Text = HttpUtility.HtmlDecode(vConcepto.cod_concepto.ToString().Trim());
            if (!string.IsNullOrEmpty(vConcepto.descripcion))
                txtConcepto.Text = HttpUtility.HtmlDecode(vConcepto.descripcion.ToString().Trim());
            if (vConcepto.tipo_concepto != 0)
                ddlTipoConcepto.SelectedValue = vConcepto.tipo_concepto.ToString();

            List<ParametrosFlujoCaja> lstCuentas = new List<ParametrosFlujoCaja>();
            lstCuentas = ParamFlujoCajaServicio.ListarCuentas(Convert.ToInt64(vConcepto.cod_concepto), (Usuario)Session["usuario"]);

            if (lstCuentas.Count > 0)
            {
                gvCuentas.DataSource = lstCuentas;
                gvCuentas.DataBind();
            }
            else
            {
                CrearDetalleInicial();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParamFlujoCajaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void ObtenerDetalleCuentas()
    {
        VerError("");
        try
        {
            List<ParametrosFlujoCaja> lstDetalleCuentas = new List<ParametrosFlujoCaja>();
            foreach (GridViewRow rfila in gvCuentas.Rows)
            {
                ParametrosFlujoCaja detalleCuentas = new ParametrosFlujoCaja();
                if (Convert.ToInt64(gvCuentas.DataKeys[rfila.DataItemIndex].Value) > 0)
                    detalleCuentas.cod_cuenta_con = Convert.ToInt64(gvCuentas.DataKeys[rfila.DataItemIndex].Value);
                else
                    detalleCuentas.cod_cuenta_con = null;
                TextBoxGrid txtCodCuenta = (TextBoxGrid)rfila.FindControl("txtCodCuenta");
                if (txtCodCuenta.Text != null && txtCodCuenta.Text.ToString().Trim() != "")
                    detalleCuentas.cod_cuenta = Convert.ToInt64(txtCodCuenta.Text);

                TextBoxGrid txtNombreCuenta = (TextBoxGrid)rfila.FindControl("txtNombreCuenta");
                if (txtNombreCuenta != null)
                    detalleCuentas.nom_cuenta = txtNombreCuenta.Text;

                DropDownListGrid ddlTipo = (DropDownListGrid)rfila.FindControl("ddlTipo");
                if (ddlTipo.SelectedValue != null)
                    detalleCuentas.tipo_mov = ddlTipo.SelectedValue;

                lstDetalleCuentas.Add(detalleCuentas);
            }
            Session["DetalleCuentas"] = lstDetalleCuentas;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    #endregion

    #region Evento botones
    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan = (ButtonGrid)sender;
        if (btnListadoPlan != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlan.CommandArgument);
            ctlPlanCuentas ctlListadoPlan = (ctlPlanCuentas)gvCuentas.Rows[rowIndex].FindControl("ctlListadoPlan");
            TextBoxGrid txtCodCuenta = (TextBoxGrid)gvCuentas.Rows[rowIndex].FindControl("txtCodCuenta");
            TextBoxGrid txtNombreCuenta = (TextBoxGrid)gvCuentas.Rows[rowIndex].FindControl("txtNombreCuenta");
            ctlListadoPlan.Motrar(true, "txtCodCuenta", "txtNombreCuenta");
            txtCodCuenta_TextChanged(txtCodCuenta, null);
        }
    }

    protected void btnAgregarFila_Click(object sender, EventArgs e)
    {
        ObtenerDetalleCuentas();
        List<ParametrosFlujoCaja> LstDetalleCuentas = new List<ParametrosFlujoCaja>();
        LstDetalleCuentas = (List<ParametrosFlujoCaja>)Session["DetalleCuentas"];
        for (int i = 1; i <= 1; i++)
        {
            ParametrosFlujoCaja pDetalleCuentas = new ParametrosFlujoCaja();
            pDetalleCuentas.cod_cuenta_con = -1;
            if (txtCodConcepto.Text != "")
                pDetalleCuentas.cod_concepto = Convert.ToInt64(txtCodConcepto.Text);
            pDetalleCuentas.cod_cuenta = null;
            pDetalleCuentas.nom_cuenta = "";
            pDetalleCuentas.tipo_mov = "D";
            LstDetalleCuentas.Add(pDetalleCuentas);
        }
        gvCuentas.PageIndex = gvCuentas.PageCount;
        gvCuentas.DataSource = LstDetalleCuentas;
        gvCuentas.DataBind();

        Session["DetalleCuentas"] = LstDetalleCuentas;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            ParametrosFlujoCaja ConceptoReporte = new ParametrosFlujoCaja();
            List<ParametrosFlujoCaja> lstDetalleCuentas = new List<ParametrosFlujoCaja>();
            ConceptoReporte.descripcion = txtConcepto.Text;
            ConceptoReporte.tipo_concepto = Convert.ToInt64(ddlTipoConcepto.SelectedValue);
            ObtenerDetalleCuentas();
            lstDetalleCuentas = (List<ParametrosFlujoCaja>)Session["DetalleCuentas"];
            if (txtCodConcepto.Text != null && txtCodConcepto.Text != "")
            {
                ConceptoReporte.cod_concepto = Convert.ToInt64(txtCodConcepto.Text);
                ConceptoReporte = ParamFlujoCajaServicio.ModificarConceptoCuenta(ConceptoReporte, lstDetalleCuentas, (Usuario)Session["usuario"]);
            }
            else
                ConceptoReporte = ParamFlujoCajaServicio.CrearConceptoCuenta(ConceptoReporte, lstDetalleCuentas, (Usuario)Session["usuario"]);

            Session.Remove(ParamFlujoCajaServicio.CodigoPrograma + ".id");
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError("btnGuardar_Click: " + ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParamFlujoCajaServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["DetalleCuentas"] != null)
            Session.Remove("DetalleCuentas");
        else
            Navegar(Pagina.Lista);
    }

    #endregion

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtCodCuenta = null;
        try
        {
            txtCodCuenta = (TextBoxGrid)sender;
        }
        catch
        {
            GridViewRow rFila = ((GridViewRow)((GridView)sender).NamingContainer.NamingContainer);
            txtCodCuenta = (TextBoxGrid)gvCuentas.Rows[rFila.RowIndex].FindControl("txtCodCuenta");
        }
        if (txtCodCuenta != null)
        {
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);
            int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);

            TextBoxGrid txtNombreCuenta = (TextBoxGrid)gvCuentas.Rows[rowIndex].FindControl("txtNombreCuenta");
            if (txtNombreCuenta != null)
                txtNombreCuenta.Text = PlanCuentas.nombre;
        }
    }

    protected void gvCuentas_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            ParamFlujoCajaServicio.EliminarConceptoCuenta(id, (Usuario)Session["usuario"]);

            List<ParametrosFlujoCaja> lstCuentas = new List<ParametrosFlujoCaja>();
            lstCuentas = ParamFlujoCajaServicio.ListarCuentas(Convert.ToInt64(txtCodConcepto.Text.Trim()), (Usuario)Session["usuario"]);

            if (lstCuentas.Count > 0)
            {
                gvCuentas.DataSource = lstCuentas;
                gvCuentas.DataBind();
            }
            else
            {
                CrearDetalleInicial();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ParamFlujoCajaServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }

    }

    protected void LimpiarCampos()
    {
        txtCodConcepto.Text = "";
        txtConcepto.Text = "";
        ddlTipoConcepto.SelectedValue = "0";
        if (Session["DetalleCuentas"] != null)
            Session.Remove("DetalleCuentas");
    }
}
