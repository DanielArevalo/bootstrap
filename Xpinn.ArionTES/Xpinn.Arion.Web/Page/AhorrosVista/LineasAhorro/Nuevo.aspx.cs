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
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Ahorros.Services.LineaAhorroServices linahorroServicio = new Xpinn.Ahorros.Services.LineaAhorroServices();
    Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[linahorroServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(linahorroServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(linahorroServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarConsultar(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(linahorroServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAhorros.ActiveViewIndex = 0;
                tcLineaAhorro.ActiveTabIndex = 0;
                CargarListas();
                if (Session[linahorroServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[linahorroServicio.CodigoPrograma + ".id"].ToString();                    
                    txtCodLineaAhorro.Enabled = false;
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
            BOexcepcion.Throw(linahorroServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ctlMensaje.MostrarMensaje("Desea guardar los datos de la línea de ahorro?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];

            Xpinn.Ahorros.Entities.LineaAhorro vLineaAhorro = new Xpinn.Ahorros.Entities.LineaAhorro();

            if (idObjeto != "")
                vLineaAhorro = linahorroServicio.ConsultarLineaAhorro(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

            if (lblConsecutivo.Text != "")
                vLineaAhorro.cod_linea_ahorro = lblConsecutivo.Text;
            else
                vLineaAhorro.cod_linea_ahorro = txtCodLineaAhorro.Text;
            vLineaAhorro.descripcion = txtDescripcion.Text;
            vLineaAhorro.estado = Convert.ToInt32(cbEstado.Checked);
            vLineaAhorro.cod_moneda = Convert.ToInt32(ddlMoneda.Value);
            vLineaAhorro.prioridad = txtPrioridad.Text.Trim() == "" ? 0 : Convert.ToInt32(txtPrioridad.Text);
            vLineaAhorro.valor_apertura = Convert.ToDecimal(txtValorApertura.Text);
            vLineaAhorro.saldo_minimo = Convert.ToDecimal(txtSaldoMinimo.Text);
            vLineaAhorro.movimiento_minimo = Convert.ToDecimal(txtMovimientoMinimo.Text);
            vLineaAhorro.maximo_retiro_diario = Convert.ToDecimal(txtMaximoRetiroDiario.Text);
            vLineaAhorro.retiro_max_efectivo = Convert.ToDecimal(txtRetiroMaxEfectivo.Text);
            vLineaAhorro.retiro_min_cheque = Convert.ToDecimal(txtRetiroMinCheque.Text);
            vLineaAhorro.requiere_libreta = Convert.ToInt32(cbRequiereLibreta.Checked);
            vLineaAhorro.valor_libreta = Convert.ToDecimal(txtValorLibreta.Text);
            vLineaAhorro.num_desprendibles_lib = txtNumDesprendiblesLib.Text.Trim() == "" ? 0 : Convert.ToInt32(txtNumDesprendiblesLib.Text);
            vLineaAhorro.cobra_primera_libreta = Convert.ToInt32(cbCobraPrimeraLibreta.Checked);
            vLineaAhorro.cobra_perdida_libreta = Convert.ToInt32(cbCobraPerdidaLibreta.Checked);
            vLineaAhorro.canje_automatico = Convert.ToInt32(cbCanjeAutomatico.Checked);
            vLineaAhorro.dias_canje = txtDiasCanje.Text.Trim() == "" ? 0 : Convert.ToInt32(txtDiasCanje.Text);
            vLineaAhorro.inactivacion_automatica = Convert.ToInt32(cbInactivacionAutomatica.Checked);
            vLineaAhorro.dias_inactiva = txtDiasInactiva.Text.Trim() == "" ? 0 : Convert.ToInt32(txtDiasInactiva.Text);
            vLineaAhorro.cobro_cierre = Convert.ToInt32(cbCobroCierre.Checked);
            vLineaAhorro.cierre_valor = Convert.ToDecimal(txtCierreValor.Text);
            vLineaAhorro.cierre_dias = txtCierreDias.Text.Trim() == "" ? 0 : Convert.ToInt32(txtCierreDias.Text);
            vLineaAhorro.tipo_saldo_int = Convert.ToInt32(ddlTipoSaldoInt.SelectedItem.Value);
            vLineaAhorro.cod_periodicidad_int = ddlPeriodicidad.cod_periodicidad;
            vLineaAhorro.dias_gracia = txtDiasGracia.Text.Trim() == "" ? 0 : Convert.ToInt32(txtDiasGracia.Text);
            vLineaAhorro.realiza_provision = Convert.ToInt32(cbRealizaProvision.Checked);
            vLineaAhorro.interes_dia_retencion = txtInteresDia.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtInteresDia.Text);
            vLineaAhorro.interes_por_cuenta = Convert.ToInt32(cbInteresPorCuenta.Checked);
            vLineaAhorro.forma_tasa = Convert.ToInt32(ctlTasaInteres.FormaTasa);
            vLineaAhorro.tipo_historico = Convert.ToInt32(ctlTasaInteres.TipoHistorico);
            vLineaAhorro.desviacion = Convert.ToDecimal(ctlTasaInteres.Desviacion);
            vLineaAhorro.tipo_tasa = Convert.ToInt32(ctlTasaInteres.TipoTasa);
            vLineaAhorro.retencion_por_cuenta = Convert.ToInt32(cbCobraRetencion.Checked);
            vLineaAhorro.saldo_minimo_liq = string.IsNullOrEmpty(txtsaldominimoliqu.Text) ? 0 : Convert.ToInt32(txtsaldominimoliqu.Text);
           
            vLineaAhorro.debito_automatico = Convert.ToInt32(chkDebitoAutomatico.Checked);  


            try
            {
                vLineaAhorro.tasa = Convert.ToDecimal(ctlTasaInteres.Tasa);            
            }
            catch
            {
                vLineaAhorro.tasa = null;
            }          
            vLineaAhorro.fecultmod = System.DateTime.Now;
            vLineaAhorro.usuultmod = vUsuario.identificacion;

            if (idObjeto != "")
            {
                vLineaAhorro.cod_linea_ahorro = idObjeto;                
                linahorroServicio.ModificarLineaAhorro(vLineaAhorro, (Usuario)Session["usuario"]);
            }
            else
            {
                vLineaAhorro.fechacreacion = System.DateTime.Now;
                vLineaAhorro.usuariocreacion = vUsuario.identificacion;
                vLineaAhorro = linahorroServicio.CrearLineaAhorro(vLineaAhorro, (Usuario)Session["usuario"]);
                idObjeto = vLineaAhorro.cod_linea_ahorro;
            }

            Session[linahorroServicio.CodigoPrograma + ".id"] = idObjeto;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(false);
            toolBar.MostrarConsultar(true);
            toolBar.eventoConsultar += btnConsultar_Click;
            mvAhorros.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
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
            Xpinn.Ahorros.Entities.LineaAhorro vLineaAhorro = new Xpinn.Ahorros.Entities.LineaAhorro();
            vLineaAhorro = linahorroServicio.ConsultarLineaAhorro(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            lblConsecutivo.Text = HttpUtility.HtmlDecode(vLineaAhorro.cod_linea_ahorro.ToString().Trim());
            if (!string.IsNullOrEmpty(vLineaAhorro.cod_linea_ahorro.ToString()))
                txtCodLineaAhorro.Text = HttpUtility.HtmlDecode(vLineaAhorro.cod_linea_ahorro.ToString());
            if (!string.IsNullOrEmpty(vLineaAhorro.descripcion.ToString()))
                txtDescripcion.Text = HttpUtility.HtmlDecode(vLineaAhorro.descripcion.ToString());
            else
                txtDescripcion.Text = "";
            cbEstado.Checked = ConvertirABoolean(vLineaAhorro.estado.ToString());
            if (!string.IsNullOrEmpty(vLineaAhorro.cod_moneda.ToString()))
                ddlMoneda.Value = HttpUtility.HtmlDecode(vLineaAhorro.cod_moneda.ToString());
            else
                ddlMoneda.Value = "0";
            if (!string.IsNullOrEmpty(vLineaAhorro.prioridad.ToString()))
                txtPrioridad.Text = HttpUtility.HtmlDecode(vLineaAhorro.prioridad.ToString());
            else
                txtPrioridad.Text = "";
            if (!string.IsNullOrEmpty(vLineaAhorro.valor_apertura.ToString()))
                txtValorApertura.Text = HttpUtility.HtmlDecode(vLineaAhorro.valor_apertura.ToString());
            if (!string.IsNullOrEmpty(vLineaAhorro.saldo_minimo.ToString()))
                txtSaldoMinimo.Text = HttpUtility.HtmlDecode(vLineaAhorro.saldo_minimo.ToString());
            if (!string.IsNullOrEmpty(vLineaAhorro.movimiento_minimo.ToString()))
                txtMovimientoMinimo.Text = HttpUtility.HtmlDecode(vLineaAhorro.movimiento_minimo.ToString());
            if (!string.IsNullOrEmpty(vLineaAhorro.maximo_retiro_diario.ToString()))
                txtMaximoRetiroDiario.Text = HttpUtility.HtmlDecode(vLineaAhorro.maximo_retiro_diario.ToString());
            if (!string.IsNullOrEmpty(vLineaAhorro.retiro_max_efectivo.ToString()))
                txtRetiroMaxEfectivo.Text = HttpUtility.HtmlDecode(vLineaAhorro.retiro_max_efectivo.ToString());
            if (!string.IsNullOrEmpty(vLineaAhorro.retiro_min_cheque.ToString()))
                txtRetiroMinCheque.Text = HttpUtility.HtmlDecode(vLineaAhorro.retiro_min_cheque.ToString());
            cbRequiereLibreta.Checked = ConvertirABoolean(vLineaAhorro.requiere_libreta.ToString());
            if (!string.IsNullOrEmpty(vLineaAhorro.valor_libreta.ToString()))
                txtValorLibreta.Text = HttpUtility.HtmlDecode(vLineaAhorro.valor_libreta.ToString());
            if (!string.IsNullOrEmpty(vLineaAhorro.num_desprendibles_lib.ToString()))
                txtNumDesprendiblesLib.Text = HttpUtility.HtmlDecode(vLineaAhorro.num_desprendibles_lib.ToString());
            cbCobraPrimeraLibreta.Checked = ConvertirABoolean(vLineaAhorro.cobra_primera_libreta.ToString());
            cbCobraPerdidaLibreta.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(vLineaAhorro.cobra_perdida_libreta.ToString()));
            cbCanjeAutomatico.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(vLineaAhorro.canje_automatico.ToString()));
            if (!string.IsNullOrEmpty(vLineaAhorro.dias_canje.ToString()))
                txtDiasCanje.Text = HttpUtility.HtmlDecode(vLineaAhorro.dias_canje.ToString());
            cbInactivacionAutomatica.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(vLineaAhorro.inactivacion_automatica.ToString()));
            if (!string.IsNullOrEmpty(vLineaAhorro.dias_inactiva.ToString()))
                txtDiasInactiva.Text = HttpUtility.HtmlDecode(vLineaAhorro.dias_inactiva.ToString());
            cbCobroCierre.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(vLineaAhorro.cobro_cierre.ToString()));
            if (!string.IsNullOrEmpty(vLineaAhorro.cierre_valor.ToString()))
                txtCierreValor.Text = HttpUtility.HtmlDecode(vLineaAhorro.cierre_valor.ToString());
            if (!string.IsNullOrEmpty(vLineaAhorro.cierre_dias.ToString()))
                txtCierreDias.Text = HttpUtility.HtmlDecode(vLineaAhorro.cierre_dias.ToString());
            if (!string.IsNullOrEmpty(vLineaAhorro.tipo_saldo_int.ToString()))
                ddlTipoSaldoInt.SelectedValue = HttpUtility.HtmlDecode(vLineaAhorro.tipo_saldo_int.ToString());
            ddlPeriodicidad.cod_periodicidad = vLineaAhorro.cod_periodicidad_int;
            if (!string.IsNullOrEmpty(vLineaAhorro.cod_periodicidad_int.ToString()))
                ddlPeriodicidad.Value = HttpUtility.HtmlDecode(vLineaAhorro.cod_periodicidad_int.ToString());
            else
                ddlPeriodicidad.Value = "0";
            if (!string.IsNullOrEmpty(vLineaAhorro.dias_gracia.ToString()))
                txtDiasGracia.Text = HttpUtility.HtmlDecode(vLineaAhorro.dias_gracia.ToString());
            cbRealizaProvision.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(vLineaAhorro.realiza_provision.ToString()));
            if (!string.IsNullOrEmpty(vLineaAhorro.interes_dia_retencion.ToString()))
                txtInteresDia.Text = HttpUtility.HtmlDecode(vLineaAhorro.interes_dia_retencion.ToString());
            cbInteresPorCuenta.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(vLineaAhorro.interes_por_cuenta.ToString()));
            cbCobraRetencion.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(vLineaAhorro.retencion_por_cuenta.ToString()));
            if (!string.IsNullOrEmpty(vLineaAhorro.saldo_minimo_liq.ToString()))
                  this.txtsaldominimoliqu.Text = HttpUtility.HtmlDecode(vLineaAhorro.saldo_minimo_liq.ToString());

            chkDebitoAutomatico.Checked = vLineaAhorro.debito_automatico == 1 ? true : false;



            try
            {
                if (!string.IsNullOrEmpty(vLineaAhorro.forma_tasa.ToString()))
                    ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(vLineaAhorro.forma_tasa.ToString());
                if (!string.IsNullOrEmpty(vLineaAhorro.tipo_historico.ToString()))
                    ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.tipo_historico.ToString()));
                if (!string.IsNullOrEmpty(vLineaAhorro.desviacion.ToString()))
                    ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.desviacion.ToString()));
                if (!string.IsNullOrEmpty(vLineaAhorro.tipo_tasa.ToString()))
                    ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(vLineaAhorro.tipo_tasa.ToString()));
                if (!string.IsNullOrEmpty(vLineaAhorro.tasa.ToString()))
                    ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(vLineaAhorro.tasa.ToString()));
            }
            catch
            { }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(linahorroServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    private void CargarListas()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            ctlTasaInteres.Inicializar();
            ddlMoneda.Inicializar();
            ddlMoneda.Requerido = false;
            ddlPeriodicidad.Inicializar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(linahorroServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private Boolean ConvertirABoolean(string sParametro)
    {
        if (sParametro == null)
            return false;
        if (sParametro.Trim() == "1")
            return true;
        return false;
    }

    protected void cbRequiereLibreta_CheckedChanged(object sender, EventArgs e)
    {
        if (cbRequiereLibreta.Checked == true)
        {
            txtValorLibreta.Enabled = true;
            txtNumDesprendiblesLib.Enabled = true;
            cbCobraPrimeraLibreta.Enabled = true;
            cbCobraPerdidaLibreta.Enabled = true;
        }
        else
        {
            txtValorLibreta.Enabled = false;
            txtNumDesprendiblesLib.Enabled = false;
            cbCobraPrimeraLibreta.Enabled = false;
            cbCobraPerdidaLibreta.Enabled = false;
        }
    }

    protected void cbCanjeAutomatico_CheckedChanged(object sender, EventArgs e)
    {
        if (cbCanjeAutomatico.Checked == true)
            txtDiasCanje.Enabled = true;
        else
            txtDiasCanje.Enabled = false;
    }

    protected void cbInactivacionAutomatica_CheckedChanged(object sender, EventArgs e)
    {
        if (cbInactivacionAutomatica.Checked == true)
            txtDiasInactiva.Enabled = true;
        else
            txtDiasInactiva.Enabled = false;
    }

    protected void cbCobroCierre_CheckedChanged(object sender, EventArgs e)
    {
        if (cbCobroCierre.Checked == true)
        {
            txtCierreValor.Enabled = true;
            txtCierreDias.Enabled = true;
        }
        else
        {
            txtCierreValor.Enabled = false;
            txtCierreDias.Enabled = false;
        }
    }
}