using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Windows.Forms;
using Xpinn.Util;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using System.Text;

using System.IO;
using System.Web.UI.HtmlControls;

public partial class Nuevo : GlobalWeb
{
    private Xpinn.Aportes.Services.GrupoLineaAporteServices LineaAporteServicio = new Xpinn.Aportes.Services.GrupoLineaAporteServices();
    private Xpinn.Aportes.Services.TipoProductoServices TipoProductoServicio = new Xpinn.Aportes.Services.TipoProductoServices();
    PoblarListas poblar = new PoblarListas();
    private Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
    Int64 Cod_linea;
    String lineaaporte = "";


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LineaAporteServicio.CodigoProgramaLineas, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvAportes.ActiveViewIndex = 0;
                CargarListas();
                LlenarComboTipoProducto(DdlTipoProducto);
                Usuario usuap = (Usuario)Session["usuario"];
                Int64 oficina = Convert.ToInt64(usuap.cod_oficina);
                if (Session[LineaAporteServicio.CodigoProgramaLineas + ".id"] != null)
                {
                    idObjeto = Session[LineaAporteServicio.CodigoProgramaLineas.ToString() + ".id"].ToString();
                    Session.Remove(LineaAporteServicio.CodigoProgramaLineas.ToString() + ".id");
                    ObtenerDatos(idObjeto);
                }

                lineaaporte = (String)Session["lineaaporte"];
                if (lineaaporte == "N")
                {
                    ConsultarMaxAporte();
                    lineaaporte = "0";
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.CodigoProgramaLineas, "Page_Load", ex);
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        this.grabar();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Nuevo);
    }

    private void ConsultarMaxAporte()
    {
        Int64 maxaporte = 0;
        Int64 numeroaporte = 1;
        Xpinn.Aportes.Services.AporteServices AportesServicio = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte aporte = new Xpinn.Aportes.Entities.Aporte();
        aporte = AportesServicio.ConsultarMaxAporte((Usuario)Session["usuario"]);
        if (!string.IsNullOrEmpty(aporte.numero_aporte.ToString()))
            maxaporte = aporte.numero_aporte + numeroaporte;
    }


    protected void LlenarComboPeriodicidad(DropDownList DdlPeriodicidad)
    {
        Xpinn.FabricaCreditos.Services.PeriodicidadService periodicidadService = new Xpinn.FabricaCreditos.Services.PeriodicidadService();
        Usuario usuap = (Usuario)Session["usuario"];
        Xpinn.FabricaCreditos.Entities.Periodicidad periodicidad = new Xpinn.FabricaCreditos.Entities.Periodicidad();
        DdlPeriodicidad.DataSource = periodicidadService.ListarPeriodicidad(periodicidad, (Usuario)Session["usuario"]);
        DdlPeriodicidad.DataTextField = "Descripcion";
        DdlPeriodicidad.DataValueField = "Codigo";
        DdlPeriodicidad.DataBind();
        //DdlPeriodicidad.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboFormaPago(DropDownList DdlFormaPago)
    {
        FormaPagoService formadepagoService = new FormaPagoService();
        Usuario usuap = (Usuario)Session["usuario"];
        FormaPago formapago = new FormaPago();
        DdlFormaPago.DataSource = formadepagoService.ListarFormaPago(formapago, (Usuario)Session["usuario"]);
        DdlFormaPago.DataTextField = "Descripcion";
        DdlFormaPago.DataValueField = "Codigo";
        DdlFormaPago.DataBind();
        //  DdlFormaPago.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    protected void LlenarComboTipoProducto(DropDownList DdlTipoProducto)
    {
        DdlTipoProducto.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        DdlTipoProducto.Items.Insert(1, new ListItem("Aporte", "1"));
        DdlTipoProducto.Items.Insert(2, new ListItem("Ahorro", "2"));
        DdlTipoProducto.Items.Insert(3, new ListItem("Otros", "3"));
        DdlTipoProducto.DataBind();
    }


    protected void DdlTipoCuota_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DdlTipoCuota.SelectedValue == "1")
        {
            Txtcuotaminima.Visible = true;
            Txtcuotamaxima.Visible = true;
            lblCuotaMinima.Visible = true;
            lblCuotaMaxima.Visible = true;
            lblCuotaMinima.Text = "Valor Cuota Mínima";
            lblCuotaMaxima.Text = "Valor Cuota Máxima";
        }
        if (DdlTipoCuota.SelectedValue == "2")
        {
            Txtcuotaminima.Visible = false;
            Txtcuotamaxima.Visible = false;
            lblCuotaMinima.Visible = false;
            lblCuotaMaxima.Visible = false;
        }
        if (DdlTipoCuota.SelectedValue == "3")
        {
            Txtcuotaminima.Visible = false;
            Txtcuotamaxima.Visible = false;
            lblCuotaMinima.Visible = false;
            lblCuotaMaxima.Visible = false;
        }
        if (DdlTipoCuota.SelectedValue == "4")
        {
            Txtcuotaminima.Visible = true;
            Txtcuotamaxima.Visible = true;
            lblCuotaMinima.Visible = true;
            lblCuotaMaxima.Visible = true;
            lblCuotaMinima.Text = "Porcentaje Mínimo";
            lblCuotaMaxima.Text = "Porcentaje Máximo";
        }
        if (DdlTipoCuota.SelectedValue == "5")
        {
            Txtcuotaminima.Visible = true;
            Txtcuotamaxima.Visible = true;
            lblCuotaMinima.Visible = true;
            lblCuotaMaxima.Visible = true;
            lblCuotaMinima.Text = "Porcentaje Mínimo";
            lblCuotaMaxima.Text = "Porcentaje Máximo";
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

    private void CargarListas()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            ctlTasaInteres.Inicializar();
            ddlPeriodicidad.Inicializar();
            poblar.PoblarListaDesplegable("LINEAAPORTE", "cod_linea_aporte,nombre", " estado = 1 ", " 1 ", ddlLineaRevaloriza, Usuario);

            ddlClasificacion.DataSource = TraerResultadosLista("Cod_clasifica");
            ddlClasificacion.DataTextField = "ListaDescripcion";
            ddlClasificacion.DataValueField = "ListaId";
            ddlClasificacion.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }
    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string pListaSolicitada)
    {
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = LineasCreditoServicio.ListasDesplegables(pListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        TxtCodLineaApo.Enabled = false;
        try
        {
            GrupoLineaAporte lineaaporte = new GrupoLineaAporte();
            if (pIdObjeto != null)
            {
                lineaaporte.cod_linea_aporte = Int32.Parse(pIdObjeto);
                lineaaporte = LineaAporteServicio.ConsultarLineaAporte(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(lineaaporte.cod_linea_aporte.ToString()))
                {
                    this.TxtCodLineaApo.Text = lineaaporte.cod_linea_aporte.ToString();
                    Cod_linea = Convert.ToInt64(TxtCodLineaApo.Text);
                    //Oculta campos de retiro para aporte
                    if(Cod_linea == 1)
                    {
                        pnlRetiros.Visible = false;
                        Txtretirominimo.Text = "0";
                        Txtretiromaximo.Text = "0";
                        txtPorcentajeRetiro.Text = "0";
                        chbPermiteRetiros.Checked = false;
                        pnlCierre.Visible = false;
                        txtDiasCierre.Text = "0";
                        txtValorCierre.Text = "0";
                    }

                    if (Cod_linea != 2)
                    {
                        TabPanelLiquid.Visible = false;
                    }


                    this.TxtLinea.Text = lineaaporte.nombre.ToString();
                    txtDistribucion.Text = lineaaporte.porcentaje_distrib.ToString();
                    this.DdlTipoProducto.SelectedValue = lineaaporte.tipo_aporte.ToString();
                    DdlTipoCuota.SelectedValue = lineaaporte.tipo_cuota.ToString();
                    ddlClasificacion.SelectedValue = lineaaporte.cod_clasificacion.ToString();
                    //this.Ddltipoliquidacion.SelectedValue = lineaaporte.tipo_liquidacion.ToString();
                    //this.DdlformaPago.SelectedValue = lineaaporte.forma_pago.ToString();
                    //this.ddlModalidad.SelectedValue = lineaaporte.periodicidad.ToString();
                    tcLineaAhorro.Visible = true;
                    if (DdlTipoCuota.SelectedValue == "1")
                    {
                        Txtcuotaminima.Text = lineaaporte.valor_cuota_minima.ToString();
                        Txtcuotamaxima.Text = lineaaporte.valor_cuota_maximo.ToString();
                    }
                    else if (DdlTipoCuota.SelectedValue == "4" || DdlTipoCuota.SelectedValue == "5")
                    {
                        Txtcuotaminima.Text = lineaaporte.porcentaje_minimo.ToString().Replace(",",".");
                        Txtcuotamaxima.Text = lineaaporte.porcentaje_maximo.ToString().Replace(",", ".");
                    }
                    else
                    {
                        Txtcuotaminima.Text = lineaaporte.valor_cuota_minima.ToString();
                        Txtcuotamaxima.Text = lineaaporte.valor_cuota_maximo.ToString();
                    }
                    Txtretirominimo.Text = lineaaporte.min_valor_retiro.ToString();
                    Txtretiromaximo.Text = lineaaporte.max_valor_retiro.ToString();
                    TxtSaldoMinimo.Text = lineaaporte.saldo_minimo.ToString();
                    this.txtValorCierre.Text = lineaaporte.valor_cierre.ToString();
                    this.txtDiasCierre.Text = lineaaporte.dias_cierre.ToString();
                    //  Txtsaldominliquid.Text = lineaaporte.saldo_minimo_Liqui.ToString();
                    txtCruce.Text = lineaaporte.porcentaje_cruce.ToString();
                    Txtretiromaximo.Text = lineaaporte.max_valor_retiro.ToString();
                    //Agregado para determinar el máximo porcentaje del saldo de la cuenta a retirar
                    txtPorcentajeRetiro.Text = lineaaporte.max_porcentaje_saldo.ToString();
                    if (lineaaporte.beneficiarios == 1)
                    {
                        ChkBeneficiarios.Checked = true;
                    }

                    if (lineaaporte.alerta == 1)
                    {
                        ChkAlerta.Checked = true;
                    }

                    if (lineaaporte.distribuye == 1)
                    {
                        chkDistribuye.Checked = true;
                    }
                    TipoCruce(lineaaporte.cruzar);
                    if (lineaaporte.estado == 1)
                    {
                        Ckhactiva.Checked = true;
                    }
                    if (lineaaporte.estado == 2)
                    {
                        Ckhinactiva.Checked = true;
                    }
                    if (lineaaporte.estado == 3)
                    {
                        Ckhcerrada.Checked = true;
                    }
                    if (lineaaporte.Pago_Intereses != null)
                        ddlformapago.SelectedValue = lineaaporte.Pago_Intereses.ToString();
                    else
                        ddlformapago.SelectedValue = "0";
                    if (lineaaporte.permite_retiros != null)
                        chbPermiteRetiros.Checked = Convert.ToBoolean(lineaaporte.permite_retiros);
                    if (lineaaporte.permite_pagoprod != null)
                        chbPermitePagoProductos.Checked = Convert.ToBoolean(lineaaporte.permite_pagoprod);
                    if (lineaaporte.permite_traslados != null)
                        chbPermiteTraslados.Checked = Convert.ToBoolean(lineaaporte.permite_traslados);
                    DdlTipoCuota_SelectedIndexChanged(DdlTipoCuota, null);
                    if (lineaaporte.cap_minimo_irreduptible != 0)
                        txtCapMinIrreduc.Text = lineaaporte.cap_minimo_irreduptible.ToString();

                    if (!string.IsNullOrEmpty(lineaaporte.tipo_saldo_int.ToString()))
                        ddlTipoSaldoInt.SelectedValue = HttpUtility.HtmlDecode(lineaaporte.tipo_saldo_int.ToString());
                    ddlPeriodicidad.cod_periodicidad = lineaaporte.cod_periodicidad_int;
                    if (!string.IsNullOrEmpty(lineaaporte.cod_periodicidad_int.ToString()))
                        ddlPeriodicidad.Value = HttpUtility.HtmlDecode(lineaaporte.cod_periodicidad_int.ToString());
                    else
                        ddlPeriodicidad.Value = "0";
                    if (!string.IsNullOrEmpty(lineaaporte.dias_gracia.ToString()))
                        txtDiasGracia.Text = HttpUtility.HtmlDecode(lineaaporte.dias_gracia.ToString());
                    cbRealizaProvision.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(lineaaporte.realiza_provision.ToString()));
                    if (!string.IsNullOrEmpty(lineaaporte.interes_dia_retencion.ToString()))
                        txtInteresDia.Text = HttpUtility.HtmlDecode(lineaaporte.interes_dia_retencion.ToString());


                    if (lineaaporte.prioridad != Int32.MinValue)
                        txtPrioridad.Text = HttpUtility.HtmlDecode(lineaaporte.prioridad.ToString().Trim());



                    cbInteresPorCuenta.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(lineaaporte.interes_por_cuenta.ToString()));
                    cbCobraRetencion.Checked = ConvertirABoolean(HttpUtility.HtmlDecode(lineaaporte.retencion_por_cuenta.ToString()));
                    if (!string.IsNullOrEmpty(lineaaporte.saldo_minimo_liq.ToString()))
                        this.txtsaldominimoliqu.Text = HttpUtility.HtmlDecode(lineaaporte.saldo_minimo_liq.ToString());
                    try
                    {
                        if (lineaaporte.cod_linea_liqui_rev != null)
                            ddlLineaRevaloriza.SelectedValue = lineaaporte.cod_linea_liqui_rev.ToString();
                    }
                    catch
                    {
                    }

                    //TASA
                    try
                    {
                        if (!string.IsNullOrEmpty(lineaaporte.forma_tasa.ToString()))
                            ctlTasaInteres.FormaTasa = HttpUtility.HtmlDecode(lineaaporte.forma_tasa.ToString());
                        if (!string.IsNullOrEmpty(lineaaporte.tipo_historico.ToString()))
                            ctlTasaInteres.TipoHistorico = Convert.ToInt32(HttpUtility.HtmlDecode(lineaaporte.tipo_historico.ToString()));
                        if (!string.IsNullOrEmpty(lineaaporte.desviacion.ToString()))
                            ctlTasaInteres.Desviacion = Convert.ToDecimal(HttpUtility.HtmlDecode(lineaaporte.desviacion.ToString()));
                        if (!string.IsNullOrEmpty(lineaaporte.tipo_tasa.ToString()))
                            ctlTasaInteres.TipoTasa = Convert.ToInt32(HttpUtility.HtmlDecode(lineaaporte.tipo_tasa.ToString()));
                        if (!string.IsNullOrEmpty(lineaaporte.tasa.ToString()))
                            ctlTasaInteres.Tasa = Convert.ToDecimal(HttpUtility.HtmlDecode(lineaaporte.tasa.ToString()));
                    }
                    catch
                    { }
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineaAporteServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }

    private void grabar()
    {
        Usuario usuap = new Usuario();
        GrupoLineaAporte lineaaporte = new GrupoLineaAporte();
        lineaaporte.cod_linea_aporte = Convert.ToInt64(TxtCodLineaApo.Text);

        if (Cod_linea != lineaaporte.cod_linea_aporte)
        {
            try
            {
                lineaaporte.cod_linea_aporte = Convert.ToInt64(TxtCodLineaApo.Text);
                lineaaporte.nombre = Convert.ToString(TxtLinea.Text);
                if (chkDistribuye.Checked == true)
                {
                    lineaaporte.distribuye = 1;
                }
                else
                {
                    lineaaporte.distribuye = 0;
                }
                if (txtDistribucion.Text == "")
                {
                    lineaaporte.porcentaje = 0;
                }
                else
                {
                    lineaaporte.porcentaje = Int64.Parse(txtDistribucion.Text);
                }
                lineaaporte.tipo_aporte = Int64.Parse(DdlTipoProducto.SelectedValue);
                lineaaporte.tipo_cuota = Int64.Parse(DdlTipoCuota.SelectedValue);
                lineaaporte.obligatorio = 0;//confirmar
                lineaaporte.min_valor_pago = 0;//confirmar
                lineaaporte.min_valor_retiro = Txtretirominimo.Text != "" ? Convert.ToInt64(Txtretirominimo.Text.Trim().Replace(".", "")) : 0;
                lineaaporte.saldo_minimo = TxtSaldoMinimo.Text != "" ? Convert.ToInt64(TxtSaldoMinimo.Text.Trim().Replace(".", "")) : 0;
                lineaaporte.valor_cierre = this.txtValorCierre.Text != "" ? Convert.ToInt64(txtValorCierre.Text.Trim().Replace(".", "")) : 0;
                //Agregado para porcentaje máximo de saldo a retirar
                lineaaporte.max_porcentaje_saldo = txtPorcentajeRetiro.Text != "" ? Convert.ToInt64(txtPorcentajeRetiro.Text.Replace("%", " ").Trim()) : 0;


                if (ChkBeneficiarios.Checked == true)
                {
                    lineaaporte.beneficiarios = 1;
                }
                else
                {
                    lineaaporte.beneficiarios = 0;
                }

                if (ChkAlerta.Checked == true)
                {
                    lineaaporte.alerta = 1;
                }
                else
                {
                    lineaaporte.alerta = 0;
                }

                if (txtDiasCierre.Text != "")
                    lineaaporte.dias_cierre = Convert.ToInt64(this.txtDiasCierre.Text);
                else
                    lineaaporte.dias_cierre = 0;
                if (ChkcruceNO.Checked == true)
                {
                    lineaaporte.cruzar = 0;
                }
                if (ChkcruceSI.Checked == true)
                {
                    lineaaporte.cruzar = 1;
                }
                if (ChkcruceCOBRAR.Checked == true)
                {
                    lineaaporte.cruzar = 2;
                }
                if (txtCruce.Text == "")
                {
                    lineaaporte.porcentaje_cruce = 0;
                }
                else
                {
                    lineaaporte.porcentaje_cruce = Int64.Parse(txtCruce.Text);
                }
                lineaaporte.cobra_mora = 0;//confirmar
                lineaaporte.provisionar = 0;//confirmar
                lineaaporte.permite_pagoprod = Convert.ToInt32(chbPermitePagoProductos.Checked);
                lineaaporte.permite_retiros = Convert.ToInt32(chbPermiteRetiros.Checked);
                lineaaporte.permite_traslados = Convert.ToInt32(chbPermiteTraslados.Checked);
                if (DdlTipoCuota.SelectedValue == "1")
                {
                    lineaaporte.valor_cuota_minima = Txtcuotaminima.Text != "" ? Convert.ToInt64(Txtcuotaminima.Text.Trim().Replace(".", "")) : 0;
                    lineaaporte.valor_cuota_maximo = Txtcuotamaxima.Text != "" ? Convert.ToInt64(Txtcuotamaxima.Text.Trim().Replace(".", "")) : 0;
                }
                else
                {
                    lineaaporte.valor_cuota_minima = 0;
                    lineaaporte.valor_cuota_maximo = 0;
                }
                if (DdlTipoCuota.SelectedValue == "4" || DdlTipoCuota.SelectedValue == "5")
                {
                    lineaaporte.porcentaje_minimo = Txtcuotaminima.Text != "" ? Convert.ToDecimal(Txtcuotaminima.Text.Trim().Replace(".",",")) : 0;
                    lineaaporte.porcentaje_maximo = Txtcuotamaxima.Text != "" ? Convert.ToDecimal(Txtcuotamaxima.Text.Trim().Replace(".", ",")) : 0;
                }
                else
                {
                    lineaaporte.porcentaje_minimo = 0;
                    lineaaporte.porcentaje_maximo = 0;
                }
                lineaaporte.max_valor_retiro = Txtretiromaximo.Text != "" ? Convert.ToInt64(Txtretiromaximo.Text.Trim().Replace(".", "")) : 0;

                if (Ckhactiva.Checked == true)
                {
                    lineaaporte.estado = 1;
                }
                if (Ckhinactiva.Checked == true)
                {
                    lineaaporte.estado = 2;
                }

                if (Ckhcerrada.Checked == true)
                {
                    lineaaporte.estado = 3;
                }
                if (Ckhactiva.Checked == false && Ckhinactiva.Checked == false && Ckhcerrada.Checked == false)
                {
                    lineaaporte.estado = 1;
                }
                if (ChkcruceNO.Checked == false && ChkcruceSI.Checked == false && ChkcruceCOBRAR.Checked == false)
                {
                    lineaaporte.cruzar = 0;
                }
                if (chbPermitePagoProductos.Checked == true)
                {
                    lineaaporte.permite_pagoprod = 1;
                }
                if (txtCapMinIrreduc.Text.Trim() != "")
                    lineaaporte.cap_minimo_irreduptible = Convert.ToDecimal(txtCapMinIrreduc.Text);

                // Liquidación interes 
                lineaaporte.tipo_saldo_int = Convert.ToInt32(ddlTipoSaldoInt.SelectedItem.Value);
                if (ddlPeriodicidad.cod_periodicidad == 0)
                    lineaaporte.cod_periodicidad_int = null;
                else
                    lineaaporte.cod_periodicidad_int = ddlPeriodicidad.cod_periodicidad;

                lineaaporte.dias_gracia = txtDiasGracia.Text.Trim() == "" ? 0 : Convert.ToInt32(txtDiasGracia.Text);
                lineaaporte.realiza_provision = Convert.ToInt32(cbRealizaProvision.Checked);
                lineaaporte.interes_dia_retencion = txtInteresDia.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtInteresDia.Text);
                lineaaporte.interes_por_cuenta = Convert.ToInt32(cbInteresPorCuenta.Checked);
                lineaaporte.forma_tasa = Convert.ToInt32(ctlTasaInteres.FormaTasa);
                lineaaporte.tipo_historico = Convert.ToInt32(ctlTasaInteres.TipoHistorico);
                lineaaporte.desviacion = Convert.ToDecimal(ctlTasaInteres.Desviacion);
                lineaaporte.tipo_tasa = Convert.ToInt32(ctlTasaInteres.TipoTasa);
                lineaaporte.retencion_por_cuenta = Convert.ToInt32(cbCobraRetencion.Checked);
                lineaaporte.saldo_minimo_liq = txtsaldominimoliqu.Text == "" ? 0 : Convert.ToInt32(this.txtsaldominimoliqu.Text);
                if (ddlformapago.SelectedValue != "0")
                    lineaaporte.cod_clasificacion = Convert.ToInt64(ddlClasificacion.SelectedValue);
                if (ddlformapago.SelectedValue != "0")
                {
                    lineaaporte.Pago_Intereses = int.Parse(ddlformapago.SelectedValue);
                }
                //
                try
                {
                    lineaaporte.tasa = Convert.ToDecimal(ctlTasaInteres.Tasa);
                }
                catch
                {
                    lineaaporte.tasa = null;
                }
                if (ddlLineaRevaloriza.SelectedItem != null)
                {
                    if (ddlLineaRevaloriza.SelectedIndex != 0)
                        lineaaporte.cod_linea_liqui_rev = Convert.ToInt64(ddlLineaRevaloriza.SelectedValue);
                }


                lineaaporte.prioridad = txtPrioridad.Text.Trim() == "" ? 0 : Convert.ToInt32(ConvertirStringToInt(txtPrioridad.Text));
                

                if (idObjeto != "")
                {
                    lineaaporte.cod_linea_aporte = Convert.ToInt64(idObjeto);
                    LineaAporteServicio.ModificarLineaAporte(lineaaporte, (Usuario)Session["usuario"]);
                }
                else
                {
                    lineaaporte = LineaAporteServicio.CrearLineaAporte(lineaaporte, (Usuario)Session["usuario"]);
                }
                Navegar(Pagina.Lista);
            }
            catch (ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(LineaAporteServicio.GetType().Name, "btnGuardar_Click", ex);
            }
        }
    }

    protected void Ckhactiva_CheckedChanged(object sender, EventArgs e)
    {
        Ckhinactiva.Checked = false;
        Ckhcerrada.Checked = false;
        if (Ckhactiva.Checked == true)
        {
            Ckhinactiva.Checked = false;
            Ckhcerrada.Checked = false;
        }
    }

    protected void TipoCruce(int ptipo)
    {
        ChkcruceNO.Checked = ptipo == 0 ? true : false;
        ChkcruceSI.Checked = ptipo == 1 ? true : false;
        ChkcruceCOBRAR.Checked = ptipo == 2 ? true : false;
        if (ChkcruceSI.Checked == true)
        {
            txtCruce.Enabled = true;
            ChkcruceNO.Checked = false;
        }
        else
        {
            txtCruce.Enabled = false;
            txtCruce.Text = "0";
            ChkcruceSI.Checked = false;
        }
    }

    protected void ChkcruceNO_CheckedChanged(object sender, EventArgs e)
    {
        TipoCruce(0);
    }

    protected void ChkcruceSI_CheckedChanged(object sender, EventArgs e)
    {
        TipoCruce(1);
    }

    protected void ChkcruceCOBRAR_CheckedChanged(object sender, EventArgs e)
    {
        TipoCruce(2);
    }

    protected void Ckhinactiva_CheckedChanged(object sender, EventArgs e)
    {
        Ckhactiva.Checked = false;
        Ckhcerrada.Checked = false;
        if (Ckhinactiva.Checked == true)
        {
            Ckhactiva.Checked = false;
            Ckhcerrada.Checked = false;
        }

    }

    protected void Ckhcerrada_CheckedChanged(object sender, EventArgs e)
    {
        Ckhactiva.Checked = false;
        Ckhinactiva.Checked = false;
        if (Ckhcerrada.Checked == true)
        {
            Ckhactiva.Checked = false;
            Ckhinactiva.Checked = false;
        }
    }


    protected void TxtLinea_TextChanged(object sender, EventArgs e)
    {
        TxtLinea.Text = TxtLinea.Text.ToUpper();
    }

}