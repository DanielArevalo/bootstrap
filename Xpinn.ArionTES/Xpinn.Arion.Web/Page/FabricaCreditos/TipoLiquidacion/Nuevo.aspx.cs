using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.TipoLiquidacionService TipoLiquidacionServicio = new Xpinn.FabricaCreditos.Services.TipoLiquidacionService();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[TipoLiquidacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(TipoLiquidacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(TipoLiquidacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoLiquidacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session[TipoLiquidacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[TipoLiquidacionServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(TipoLiquidacionServicio.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(TipoLiquidacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.TipoLiquidacion vTipoLiquidacion = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();

            if (idObjeto != "")
                vTipoLiquidacion = TipoLiquidacionServicio.ConsultarTipoLiquidacion(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            vTipoLiquidacion.tipo_liquidacion = Convert.ToInt32(txtTipoLiquidacion.Text.Trim());
            vTipoLiquidacion.descripcion = Convert.ToString(txtDescripcion.Text.Trim());
            vTipoLiquidacion.tipo_cuota = Convert.ToInt32(ddlTipoCuota.SelectedValue);
            vTipoLiquidacion.tipo_pago = Convert.ToInt32(ddlTipoPago.SelectedValue);
            vTipoLiquidacion.tipo_interes = Convert.ToInt32(ddlTipoInteres.SelectedValue);
            vTipoLiquidacion.tip_amo = ddlTipoAmo.SelectedValue != "" ? Convert.ToInt32(ddlTipoAmo.SelectedValue) : 0;
            vTipoLiquidacion.tipo_intant = Convert.ToInt32(ddlTipoIntAnt.SelectedValue);
            vTipoLiquidacion.tip_gra = Convert.ToInt32(ddlTipoGra.SelectedValue);
            ControlarCobroInteresAntDesembolso();
            if (cbCobraIntDesembolso.Visible)
                vTipoLiquidacion.cob_intant_des = cbCobraIntDesembolso.Checked ? 1: 0;
            else
                vTipoLiquidacion.cob_intant_des = 0;
            if (txtGradiente.Text.Trim() == "")
                txtGradiente.Text = "0";
            vTipoLiquidacion.valor_gradiente = Convert.ToInt32(txtGradiente.Text);

            if (idObjeto != "")
            {
                vTipoLiquidacion.tipo_liquidacion = Convert.ToInt32(idObjeto);
                TipoLiquidacionServicio.ModificarTipoLiquidacion(vTipoLiquidacion, (Usuario)Session["usuario"]);
            }
            else
            {
                vTipoLiquidacion = TipoLiquidacionServicio.CrearTipoLiquidacion(vTipoLiquidacion, (Usuario)Session["usuario"]);
                idObjeto = vTipoLiquidacion.tipo_liquidacion.ToString();
            }

            Session[TipoLiquidacionServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            string _error = "";
            //BOexcepcion.Throw(TipoLiquidacionServicio.CodigoPrograma, "btnGuardar_Click", ex);
            if (ex.Message.Contains("ORA-20101"))
                _error = ex.Message.Substring(ex.Message.IndexOf("ORA-20101"));
            if (idObjeto != "")
                VerError("         No se puede modificar el tipo de liquidaciòn. Error:" + _error);
            else
                VerError("         No se puede crear el tipo de liquidaciòn. Error:" + _error);
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
            Xpinn.FabricaCreditos.Entities.TipoLiquidacion vTipoLiquidacion = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
            vTipoLiquidacion = TipoLiquidacionServicio.ConsultarTipoLiquidacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vTipoLiquidacion.tipo_liquidacion.ToString()))
                txtTipoLiquidacion.Text = HttpUtility.HtmlDecode(vTipoLiquidacion.tipo_liquidacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoLiquidacion.descripcion))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vTipoLiquidacion.descripcion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoLiquidacion.tipo_cuota.ToString()))
                ddlTipoCuota.SelectedValue = HttpUtility.HtmlDecode(vTipoLiquidacion.tipo_cuota.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoLiquidacion.tipo_pago.ToString()))
                ddlTipoPago.SelectedValue = HttpUtility.HtmlDecode(vTipoLiquidacion.tipo_pago.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoLiquidacion.tipo_interes.ToString()))
                ddlTipoInteres.SelectedValue = HttpUtility.HtmlDecode(vTipoLiquidacion.tipo_interes.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoLiquidacion.tip_amo.ToString()))
                ddlTipoAmo.SelectedValue = HttpUtility.HtmlDecode(vTipoLiquidacion.tip_amo.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoLiquidacion.tipo_intant.ToString()))
                ddlTipoIntAnt.SelectedValue = HttpUtility.HtmlDecode(vTipoLiquidacion.tipo_intant.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoLiquidacion.tip_gra.ToString()))
                ddlTipoGra.SelectedValue = HttpUtility.HtmlDecode(vTipoLiquidacion.tip_gra.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoLiquidacion.valor_gradiente.ToString()))
                txtGradiente.Text = HttpUtility.HtmlDecode(vTipoLiquidacion.valor_gradiente.ToString().Trim());
            if (!string.IsNullOrEmpty(vTipoLiquidacion.cob_intant_des.ToString()))
                cbCobraIntDesembolso.Checked = HttpUtility.HtmlDecode(vTipoLiquidacion.cob_intant_des.ToString()) == "1" ? true: false;
            TipoAmortizacion();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TipoLiquidacionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    protected void TipoAmortizacion()
    {
        try
        {
            if (ddlTipoCuota.SelectedValue == "1")
            {
                ddlTipoAmo.Items.Clear();
                List<Xpinn.FabricaCreditos.Entities.TipoLiquidacion> lstTipAmo = new List<Xpinn.FabricaCreditos.Entities.TipoLiquidacion>();
                Xpinn.FabricaCreditos.Entities.TipoLiquidacion eTipAmo = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
                eTipAmo.tip_amo = 6;
                eTipAmo.nomtip_amo = "Pago Unico";
                lstTipAmo.Add(eTipAmo);
                ddlTipoAmo.DataTextField = "nomtip_amo";
                ddlTipoAmo.DataValueField = "tip_amo";
                ddlTipoAmo.DataSource = lstTipAmo;
                ddlTipoAmo.DataBind();
            }
            if (ddlTipoCuota.SelectedValue == "2")
            {
                ddlTipoAmo.Items.Clear();
                List<Xpinn.FabricaCreditos.Entities.TipoLiquidacion> lstTipAmo = new List<Xpinn.FabricaCreditos.Entities.TipoLiquidacion>();
                Xpinn.FabricaCreditos.Entities.TipoLiquidacion eTipAmo2 = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
                eTipAmo2.tip_amo = 2;
                eTipAmo2.nomtip_amo = "Capital Fijo Interes Variable";
                lstTipAmo.Add(eTipAmo2);
                Xpinn.FabricaCreditos.Entities.TipoLiquidacion eTipAmo3 = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
                eTipAmo3.tip_amo = 3;
                eTipAmo3.nomtip_amo = "Capital Fijo Intere Fijo";
                lstTipAmo.Add(eTipAmo3);
                Xpinn.FabricaCreditos.Entities.TipoLiquidacion eTipAmo4 = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
                eTipAmo4.tip_amo = 4;
                eTipAmo4.nomtip_amo = "Capital Variable Interes Variable";
                lstTipAmo.Add(eTipAmo4);
                Xpinn.FabricaCreditos.Entities.TipoLiquidacion eTipAmo5 = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
                eTipAmo5.tip_amo = 5;
                eTipAmo5.nomtip_amo = "T.F.Prorrateados";
                lstTipAmo.Add(eTipAmo5);
                ddlTipoAmo.DataTextField = "nomtip_amo";
                ddlTipoAmo.DataValueField = "tip_amo";
                ddlTipoAmo.DataSource = lstTipAmo;
                ddlTipoAmo.DataBind();
            }
            if (ddlTipoCuota.SelectedValue == "3")
            {
                ddlTipoAmo.Items.Clear();
                List<Xpinn.FabricaCreditos.Entities.TipoLiquidacion> lstTipAmo = new List<Xpinn.FabricaCreditos.Entities.TipoLiquidacion>();
                Xpinn.FabricaCreditos.Entities.TipoLiquidacion eTipAmo1 = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
                eTipAmo1.tip_amo = 7;
                eTipAmo1.nomtip_amo = "Ajuste Periodico y Gradiente";
                lstTipAmo.Add(eTipAmo1);
                Xpinn.FabricaCreditos.Entities.TipoLiquidacion eTipAmo2 = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
                eTipAmo2.tip_amo = 8;
                eTipAmo2.nomtip_amo = "Incremento Anual con Salario Minimo";
                lstTipAmo.Add(eTipAmo2);
                Xpinn.FabricaCreditos.Entities.TipoLiquidacion eTipAmo3 = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
                eTipAmo3.tip_amo = 9;
                eTipAmo3.nomtip_amo = "Cuota Fija Crecimiento Periodico";
                lstTipAmo.Add(eTipAmo3);
                Xpinn.FabricaCreditos.Entities.TipoLiquidacion eTipAmo4 = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
                eTipAmo4.tip_amo = 10;
                eTipAmo4.nomtip_amo = "Incremento Periodico";
                lstTipAmo.Add(eTipAmo4);
                Xpinn.FabricaCreditos.Entities.TipoLiquidacion eTipAmo5 = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
                eTipAmo5.tip_amo = 11;
                eTipAmo5.nomtip_amo = "Gradiente sin capitalización de cuota";
                lstTipAmo.Add(eTipAmo5);
                ddlTipoAmo.DataTextField = "nomtip_amo";
                ddlTipoAmo.DataValueField = "tip_amo";
                ddlTipoAmo.DataSource = lstTipAmo;
                ddlTipoAmo.DataBind();
            }
            ControlarCobroInteresAntDesembolso();
        }
        catch
        {
            return;
        }
    }

    protected void ddlTipoCuota_SelectedIndexChanged(object sender, EventArgs e)
    {
        TipoAmortizacion();       
    }

    protected void ControlarCobroInteresAntDesembolso()
    {
        if (ddlTipoCuota.SelectedValue == "2" && (ddlTipoAmo.SelectedValue == "4" || ddlTipoAmo.SelectedValue == "5") && ddlTipoPago.SelectedValue == "1")
            cbCobraIntDesembolso.Visible = true;
        else
            cbCobraIntDesembolso.Visible = false;
    }

    protected void ddlTipoPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlarCobroInteresAntDesembolso();
    }



}