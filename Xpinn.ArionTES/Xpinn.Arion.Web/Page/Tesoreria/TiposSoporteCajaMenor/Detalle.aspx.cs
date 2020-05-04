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
    private Xpinn.Tesoreria.Services.TipSopCajService TipSopCajServicio = new Xpinn.Tesoreria.Services.TipSopCajService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TipSopCajServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TipSopCajServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(TipSopCajServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;            
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipSopCajServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ListaPlanCuentas();
                if (Session[TipSopCajServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TipSopCajServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TipSopCajServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(TipSopCajServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

   
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Tesoreria.Entities.TipSopCaj vTipoComp = new Xpinn.Tesoreria.Entities.TipSopCaj();
            vTipoComp = TipSopCajServicio.ConsultarTipSopCaj(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vTipoComp.idtiposop.ToString()))
                txtTipSopCaj.Text = HttpUtility.HtmlDecode(vTipoComp.idtiposop.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoComp.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoComp.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoComp.cod_cuenta))
                ddlCodCuenta.SelectedValue = HttpUtility.HtmlDecode(vTipoComp.cod_cuenta.ToString().Trim());
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipSopCajServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void ListaPlanCuentas()
    {
        Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
        List<Xpinn.Contabilidad.Entities.PlanCuentas> LstPlanCuentas = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
        Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        LstPlanCuentas = PlanCuentasServicio.ListarPlanCuentasLocal(pPlanCuentas, (Usuario)Session["usuario"], "");
        ddlCodCuenta.DataTextField = "cod_cuenta";
        ddlCodCuenta.DataValueField = "cod_cuenta";
        ddlCodCuenta.DataSource = LstPlanCuentas;
        ddlCodCuenta.DataBind();
    }

}