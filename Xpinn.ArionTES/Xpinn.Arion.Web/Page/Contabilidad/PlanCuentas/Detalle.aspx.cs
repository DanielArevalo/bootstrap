using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Detalle : GlobalWeb
{
    private Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDll();
                ListaPlanCuentas();
                if (Session[PlanCuentasServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[PlanCuentasServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(PlanCuentasServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    // Cuando es consulta de comprobantes carga los datos
                    if (Session["30502.id"] != null)
                    {
                        idObjeto = Session["30502.id"].ToString();
                        Session.Remove("30502.id");
                        ObtenerDatos(idObjeto);
                    }
                    // Cuando es modificación de comprobantes carga los datos
                    if (Session["30503.id"] != null)
                    {
                        idObjeto = Session["30503.id"].ToString();
                        Session.Remove("30503.id");
                        ObtenerDatos(idObjeto);
                    }
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarDll()
    {
        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["Usuario"]);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Contabilidad.Entities.PlanCuentas vPlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            vPlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vPlanCuentas.cod_cuenta))
                txtCodCuenta.Text = HttpUtility.HtmlDecode(vPlanCuentas.cod_cuenta.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentas.nombre))
                txtNombre.Text = HttpUtility.HtmlDecode(vPlanCuentas.nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentas.nivel.ToString()))
                txtNivel.Text = HttpUtility.HtmlDecode(vPlanCuentas.nivel.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentas.tipo.ToString()))
                ddlTipo.SelectedValue = HttpUtility.HtmlDecode(vPlanCuentas.tipo.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentas.cod_moneda.ToString()))
                ddlMonedas.SelectedValue = HttpUtility.HtmlDecode(vPlanCuentas.cod_moneda.ToString().Trim());
            if (!string.IsNullOrEmpty(vPlanCuentas.depende_de.ToString()))
                ddlDependede.SelectedValue = HttpUtility.HtmlDecode(vPlanCuentas.depende_de.ToString());
            chkEstado.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.estado.ToString()))
                if (vPlanCuentas.estado.ToString().Trim() == "1")
                    chkEstado.Checked = true;
            chkTerceros.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.maneja_ter.ToString()))
                if (vPlanCuentas.maneja_ter.ToString().Trim() == "1")
                    chkTerceros.Checked = true;
            chkCentroCosto.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.maneja_cc.ToString()))
                if (vPlanCuentas.maneja_cc.ToString().Trim() == "1")
                    chkCentroCosto.Checked = true;
            chkCentroGestion.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.maneja_sc.ToString()))
                if (vPlanCuentas.maneja_sc.ToString().Trim() == "1")
                    chkCentroGestion.Checked = true;
            chkCuentaPagar.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.maneja_gir.ToString()))
                if (vPlanCuentas.maneja_gir.ToString().Trim() == "1")
                    chkCuentaPagar.Checked = true;
            chkImpuestos.Checked = false;
            if (!string.IsNullOrEmpty(vPlanCuentas.impuesto.ToString()))
                if (vPlanCuentas.impuesto.ToString().Trim() == "1")
                    chkImpuestos.Checked = true;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(PlanCuentasServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void ListaPlanCuentas()
    {
        List<Xpinn.Contabilidad.Entities.PlanCuentas> LstPlanCuentas = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
        Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        LstPlanCuentas = PlanCuentasServicio.ListarPlanCuentasLocal(pPlanCuentas, (Usuario)Session["usuario"], "");
        ddlDependede.DataSource = LstPlanCuentas;
        ddlDependede.DataTextField = "cod_cuenta";
        ddlDependede.DataValueField = "cod_cuenta";
        ddlDependede.DataBind();
    }

}