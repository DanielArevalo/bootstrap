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
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

partial class Lista : GlobalWeb
{
    Int64 cod_deudor = 0;
    AperturaCDATService ApertuService = new AperturaCDATService();
    LiquidacionCDATService LiquiService = new LiquidacionCDATService();
    LiquidacionCDAT vData = new LiquidacionCDAT();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(LiquiService.CodigoProgramaLiqui, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LiquiService.CodigoProgramaLiqui, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            mvPrincipal.ActiveViewIndex = 0;
            if (!IsPostBack)
            {
                ctlGiro.Inicializar();
                ObtenerListaGrilla();
                txtFechaLiqui.Text = DateTime.Now.ToShortDateString();
                panelGrilla.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LiquiService.CodigoProgramaLiqui, "Page_Load", ex);
        }
    }



    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (gvLista.Rows.Count > 0 && ViewState["DTCDAT"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvLista.AllowPaging = false;
                gvLista.DataSource = ViewState["DTCDAT"];
                gvLista.DataBind();
                gvLista.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvLista);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=LiquidaciónCDATS.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(sb.ToString());
                Response.End();
                gvLista.AllowPaging = true;
                gvLista.DataBind();
            }
            else
            {
                VerError("No existen Datos");
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LiquiService.CodigoProgramaLiqui, "gvLista_PageIndexChanging", ex);
        }
    }


    private List<LiquidacionCDAT> ObtenerListaGrilla()
    {
        List<LiquidacionCDAT> lstLista = new List<LiquidacionCDAT>();

        foreach (GridViewRow rFila in gvLista.Rows)
        {
            LiquidacionCDAT vData = new LiquidacionCDAT();
            if (rFila.Cells[0].Text != "" && rFila.Cells[0].Text != "&nbsp;")//CODIGO
                vData.codigo_cdat = Convert.ToInt64(rFila.Cells[0].Text);
            else
                vData.codigo_cdat = 0;

            if (rFila.Cells[1].Text != "" && rFila.Cells[1].Text != "&nbsp;")//NUMERO CDAT
                vData.numero_cdat = rFila.Cells[1].Text;

            if (rFila.Cells[2].Text != "" && rFila.Cells[2].Text != "&nbsp;")//FECHA INICIAL
                vData.fecha_inicial = Convert.ToDateTime(rFila.Cells[2].Text);

            if (rFila.Cells[3].Text != "" && rFila.Cells[3].Text != "&nbsp;")//FECHA FINAL
                vData.fecha_final = Convert.ToDateTime(rFila.Cells[3].Text);

            if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")//IDENTIFICACION
                vData.identificacion = rFila.Cells[4].Text;
            TXTIDENTIFICACCION.Text = vData.identificacion;


            // OBTENER CODIGO DE PERSONA 
            LiquidacionCDAT vLiqui = new LiquidacionCDAT();
            vLiqui.identificacion = TXTIDENTIFICACCION.Text.Trim() == "" ? "" : TXTIDENTIFICACCION.Text;

            vLiqui = LiquiService.Listartitular(vLiqui, Usuario);

            cod_deudor = vLiqui.cod_deudor;
            Session["Cod_deudor"] = cod_deudor;
            if (txtCodigo.Text != null)
            {
                ctlGiro.cargarCuentasAhorro(cod_deudor);
            }

            if (rFila.Cells[5].Text != "" && rFila.Cells[5].Text != "&nbsp;")//NOMBRE TITULAR
                vData.nombre = rFila.Cells[5].Text;

            if (rFila.Cells[6].Text != "" && rFila.Cells[6].Text != "&nbsp;")//VALOR
                vData.valor = Convert.ToDecimal(rFila.Cells[6].Text);

            if (rFila.Cells[7].Text != "" && rFila.Cells[7].Text != "&nbsp;")//PERIODICIDAD
                vData.periodicidad = rFila.Cells[7].Text;

            if (rFila.Cells[8].Text != "" && rFila.Cells[8].Text != "&nbsp;")//TASA
                vData.tasa = Convert.ToDecimal(rFila.Cells[8].Text);

            if (rFila.Cells[9].Text != "" && rFila.Cells[9].Text != "&nbsp;")//FECHA INTERES
                vData.fecha_int = Convert.ToDateTime(rFila.Cells[9].Text);
            else
                vData.fecha_int = DateTime.MinValue;

            if (rFila.Cells[10].Text != "" && rFila.Cells[10].Text != "&nbsp;")//INTERES
            {
                vData.interes = Convert.ToDecimal(rFila.Cells[10].Text);
            }
            else
            {
                vData.interes = 0;
            }
            Session["Interes"] = vData.interes;
            if (rFila.Cells[11].Text != "" && rFila.Cells[11].Text != "&nbsp;")//RETENCION
                vData.retencion = Convert.ToDecimal(rFila.Cells[11].Text);
            else
                vData.retencion = 0;

            if (rFila.Cells[12].Text != "" && rFila.Cells[12].Text != "&nbsp;")//VALOR GMF
                vData.valor_gmf = Convert.ToDecimal(rFila.Cells[12].Text);
            else
                vData.valor_gmf = 0;

            if (rFila.Cells[13].Text != "" && rFila.Cells[13].Text != "&nbsp;")//VALOR O INTERES NETO
                vData.interes_neto = Convert.ToDecimal(rFila.Cells[13].Text);
            else
                vData.interes_neto = 0;

            if (rFila.Cells[14].Text != "" && rFila.Cells[14].Text != "&nbsp;")//INTERES CAUSADO
                vData.interes_causado = Convert.ToDecimal(rFila.Cells[14].Text);
            else
                vData.interes_causado = 0;

            if (rFila.Cells[15].Text != "" && rFila.Cells[15].Text != "&nbsp;")//RETENCION CAUSADO
                vData.retencion_causado = Convert.ToDecimal(rFila.Cells[15].Text);
            else
                vData.retencion_causado = 0;


            Session["PagoInteres"] = vData.interes - vData.retencion;
            //if (rFila.Cells[16].Text != "" && rFila.Cells[16].Text != "&nbsp;")//FORMA PAGO
            //    vData.forma_pago = rFila.Cells[16].Text;
            //else
            //    vData.forma_pago = null;

            //if (rFila.Cells[17].Text != "" && rFila.Cells[17].Text != "&nbsp;")//CTA AHORROS
            //    vData.cta_ahorros = rFila.Cells[17].Text;
            //else
            //    vData.cta_ahorros = null;
            vData.fecha_liquidacion = Convert.ToDateTime(txtFechaLiqui.Text);

            if (vData.numero_cdat != "" && vData.numero_cdat != null && vData.codigo_cdat != 0 && vData.interes != 0)
            {
                lstLista.Add(vData);
            }
        }
        return lstLista;
    }
    private List<LiquidacionCDAT> ObtenerListaGrilla2()
    {
        List<LiquidacionCDAT> lstLista = new List<LiquidacionCDAT>();

        foreach (GridViewRow rFila in gvLista.Rows)
        {
            LiquidacionCDAT vData = new LiquidacionCDAT();
            if (rFila.Cells[0].Text != "" && rFila.Cells[0].Text != "&nbsp;")//CODIGO
                vData.codigo_cdat = Convert.ToInt64(rFila.Cells[0].Text);
            else
                vData.codigo_cdat = 0;

            if (rFila.Cells[1].Text != "" && rFila.Cells[1].Text != "&nbsp;")//NUMERO CDAT
                vData.numero_cdat = rFila.Cells[1].Text;

            if (rFila.Cells[2].Text != "" && rFila.Cells[2].Text != "&nbsp;")//FECHA INICIAL
                vData.fecha_inicial = Convert.ToDateTime(rFila.Cells[2].Text);

            if (rFila.Cells[3].Text != "" && rFila.Cells[3].Text != "&nbsp;")//FECHA FINAL
                vData.fecha_final = Convert.ToDateTime(rFila.Cells[3].Text);

            if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")//IDENTIFICACION
                vData.identificacion = rFila.Cells[4].Text;
            TXTIDENTIFICACCION.Text = vData.identificacion;

            if (rFila.Cells[5].Text != "" && rFila.Cells[5].Text != "&nbsp;")//NOMBRE TITULAR
                vData.nombre = rFila.Cells[5].Text;

            if (rFila.Cells[6].Text != "" && rFila.Cells[6].Text != "&nbsp;")//VALOR
                vData.valor = Convert.ToDecimal(rFila.Cells[6].Text);

            if (rFila.Cells[7].Text != "" && rFila.Cells[7].Text != "&nbsp;")//PERIODICIDAD
                vData.periodicidad = rFila.Cells[7].Text;

            if (rFila.Cells[8].Text != "" && rFila.Cells[8].Text != "&nbsp;")//TASA
                vData.tasa = Convert.ToDecimal(rFila.Cells[8].Text);

            if (rFila.Cells[9].Text != "" && rFila.Cells[9].Text != "&nbsp;")//FECHA INTERES
                vData.fecha_int = Convert.ToDateTime(rFila.Cells[9].Text);
            else
                vData.fecha_int = DateTime.MinValue;

            if (rFila.Cells[10].Text != "" && rFila.Cells[10].Text != "&nbsp;")//INTERES
            {
                vData.interes = Convert.ToDecimal(rFila.Cells[10].Text);
            }
            else
            {
                vData.interes = 0;
            }
            Session["Interes"] = vData.interes;
            if (rFila.Cells[11].Text != "" && rFila.Cells[11].Text != "&nbsp;")//RETENCION
                vData.retencion = Convert.ToDecimal(rFila.Cells[11].Text);
            else
                vData.retencion = 0;

            if (rFila.Cells[12].Text != "" && rFila.Cells[12].Text != "&nbsp;")//VALOR GMF
                vData.valor_gmf = Convert.ToDecimal(rFila.Cells[12].Text);
            else
                vData.valor_gmf = 0;

            if (rFila.Cells[13].Text != "" && rFila.Cells[13].Text != "&nbsp;")//VALOR O INTERES NETO
                vData.interes_neto = Convert.ToDecimal(rFila.Cells[13].Text);
            else
                vData.interes_neto = 0;


            if (rFila.Cells[15].Text != "" && rFila.Cells[15].Text != "&nbsp;")//RETENCION CAUSADO
                vData.retencion_causado = Convert.ToDecimal(rFila.Cells[15].Text);
            else
                vData.retencion_causado = 0;

            //if (rFila.Cells[16].Text != "" && rFila.Cells[16].Text != "&nbsp;")//FORMA PAGO
            //    vData.forma_pago = rFila.Cells[16].Text;
            //else
            //    vData.forma_pago = null;

            //if (rFila.Cells[17].Text != "" && rFila.Cells[17].Text != "&nbsp;")//CTA AHORROS
            //    vData.cta_ahorros = rFila.Cells[17].Text;
            //else
            //    vData.cta_ahorros = null;
            vData.fecha_liquidacion = Convert.ToDateTime(txtFechaLiqui.Text);

            vData.capitalizar_int = cbCapitalizaInteres.Checked == true ? 1 : 0;


            if (vData.numero_cdat != "" && vData.numero_cdat != null && vData.codigo_cdat != 0 && vData.interes != 0)
            {
                lstLista.Add(vData);
            }
        }
        return lstLista;
    }



    private void Actualizar()
    {
        try
        {
            DateTime FechaApe = txtFechaLiqui.ToDateTime == null ? DateTime.MinValue : txtFechaLiqui.ToDateTime;

            List<LiquidacionCDAT> lstConsulta = LiquiService.ListarTemporal_LiquidacionCDAT(vData, FechaApe, Usuario);

            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacía";
            gvLista.EmptyDataText = emptyQuery;

            Site toolbar = (Site)Master;
            if (lstConsulta != null && lstConsulta.Count > 0)
            {
                if (chkSoloInteres.Checked)
                {
                    lstConsulta = lstConsulta.Where(x => x.interes != 0).ToList();
                }

                lstConsulta.ForEach(x =>
                {
                    x.interes = Math.Round(x.interes);
                    x.retencion = Math.Round(x.retencion);
                });

                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                Label2.Visible = false;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                ViewState.Add("DTCDAT", lstConsulta);

                toolbar.MostrarGuardar(true);
                toolbar.MostrarExportar(true);
            }
            else
            {
                toolbar.MostrarGuardar(false);
                toolbar.MostrarExportar(false);

                Label2.Visible = true;
                ViewState["DTCDAT"] = null;
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(LiquiService.CodigoProgramaLiqui + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LiquiService.CodigoProgramaLiqui, "Actualizar", ex);
        }
    }


    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (txtFechaLiqui.Text != "")
            {
                Page.Validate();

                if (Page.IsValid)
                {
                    LiquidacionCDAT vLiqui = new LiquidacionCDAT();
                    vLiqui.fecha_liquidacion = Convert.ToDateTime(txtFechaLiqui.Text);
                    vLiqui.numero_cdat = Convert.ToString(this.txtCodigo.Text);
                    vLiqui.numero_cdat = txtCodigo.Text.Trim() == "" ? "" : txtCodigo.Text;
                    vLiqui.identificacion = txtIdentific.Text.Trim() == "" ? "" : txtIdentific.Text;
                    //liquidacion
                    vLiqui.origen = 0;
                    LiquiService.GENERAR_LiquidacionCDAT(vLiqui, Usuario);

                    Actualizar();
                    ObtenerListaGrilla();

                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    Boolean ValidarDatos()
    {
        bool pGenerarGiro = false;
        Detalle_CDAT pVar = new Detalle_CDAT();
        if (gvLista.Rows.Count == 0)
        {
            VerError("No existen datos a grabar");
            return false;
        }
        if (this.txtCodigo.Text == "" && txtIdentific.Text == "")
        {
            VerError("Digite el número de cdat o identificación Titular");
            return false;
        }
        //Validando datos del control de Giro
        if (ctlGiro.IndiceFormaDesem == 0)
        {
            VerError("Seleccione una forma de desembolso");
            return false;
        }
        else
        {
            if (ctlGiro.IndiceFormaDesem == 2 || ctlGiro.IndiceFormaDesem == 3)
            {
                if (ctlGiro.IndiceEntidadOrigen == 0)
                {
                    VerError("Seleccione un Banco de donde se girará");
                    return false;
                }
                if (ctlGiro.IndiceFormaDesem == 3)
                {
                    if (ctlGiro.IndiceEntidadDest == 0)
                    {
                        VerError("Seleccione la Entidad de destino");
                        return false;
                    }
                    if (ctlGiro.TextNumCuenta == "")
                    {
                        VerError("Ingrese el número de la cuenta");
                        return false;
                    }
                }
                if (ctlGiro.IndiceFormaDesem == 4)
                {
                    pGenerarGiro = false;

                    pVar.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? Convert.ToInt64(ctlGiro.ValueCuentaAhorro) : default(long?);

                    if (!pVar.numero_cuenta_ahorro_vista.HasValue)
                    {
                        VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");

                    }
                }
            }
        }

        return true;
    }

    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        try
        {
            txtFechaLiqui.Text = "";
            Site toolBar = (Site)Master;
            toolBar.MostrarExportar(false);
            toolBar.MostrarGuardar(false);
            panelGrilla.Visible = false;
            mvPrincipal.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LiquiService.CodigoProgramaLiqui, "btnRegresar_Click", ex);
        }
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarDatos())
        {
            //Guardar en liquidacion_cdat

            if (ViewState["DTCDAT"] != null && ((List<LiquidacionCDAT>)ViewState["DTCDAT"]).Count > 0)
            {
                ctlMensaje.MostrarMensaje("Desea guardar los datos?");
            }
            else
            {
                VerError("No se puede guardar interés igual a 0");
            }
        }
    }


    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        Detalle_CDAT pVar = new Detalle_CDAT();
        bool pGenerarGiro = false;
        Xpinn.FabricaCreditos.Services.Persona1Service personaService = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 persona = new Xpinn.FabricaCreditos.Entities.Persona1();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        try
        {
            Int64 COD_OPE = 0, COD_PERSONA = 0, COD_CDAT = 0, COD_CLIENTE = 0;
            LiquidacionCDAT pLiqui = new LiquidacionCDAT();
            LiquidacionCDAT pLiqui2 = new LiquidacionCDAT();
            pLiqui.lstLista = (List<LiquidacionCDAT>)ViewState["DTCDAT"];

            //consultar cierre historico
            String estado = "";
            DateTime fechacierrehistorico;
            DateTime fechaliquidacion=Convert.ToDateTime(txtFechaLiqui.Texto);
        
            Xpinn.CDATS.Entities.LiquidacionCDAT vliquidacioncdat = new Xpinn.CDATS.Entities.LiquidacionCDAT();
            vliquidacioncdat = LiquiService.ConsultarCierreCdats((Usuario)Session["usuario"]);
            estado = vliquidacioncdat.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vliquidacioncdat.fecha_cierre.ToString());
            //DateTime fecha = DateTime.Now;
            DateTime fecha = Convert.ToDateTime(txtFechaLiqui.Texto);
            if (estado == "D" && fecha <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO M,'CDAT'S'");
            }
            else
            {
                //Guardar en liquidacion_cdat

                if (pLiqui.lstLista.Count > 0)
                {
                    LiquiService.CrearLiquidacionCDAT(pLiqui, Usuario);
                }

                //GRABACION DE LA OPERACION
                Xpinn.Tesoreria.Services.OperacionServices xTesoreria = new Xpinn.Tesoreria.Services.OperacionServices();
                Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();

                vOpe.cod_ope = 0;
                vOpe.tipo_ope = 10;
                //COD USUARIO EN CAPA DATOS
                //COD OFICINA EN CAPA DATOS
                vOpe.cod_caja = 0;
                vOpe.cod_cajero = 0;
                vOpe.observacion = null;
                vOpe.cod_proceso = null;
                vOpe.fecha_oper = DateTime.Now;
                vOpe.fecha_calc = DateTime.Now;

                //GRABACION DEL GIRO A REALIZAR
                Xpinn.FabricaCreditos.Services.AvanceService AvancServices = new Xpinn.FabricaCreditos.Services.AvanceService();
                Usuario pusu = (Usuario)Session["usuario"];
                Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
                pGiro.idgiro = 0;
                pGiro.cod_persona = Convert.ToInt64(Session["Cod_deudor"]);
                pGiro.forma_pago = Convert.ToInt32(ctlGiro.ValueFormaDesem);
                pGiro.tipo_acto = 8;
                pGiro.fec_reg = Convert.ToDateTime(txtFechaLiqui.Texto);
                pGiro.fec_giro = DateTime.Now;
                pGiro.numero_radicacion = 0;
                pGiro.usu_gen = pusu.nombre;
                pGiro.usu_apli = null;
                pGiro.estadogi = 0;
                pGiro.usu_apro = null;

                if (ctlGiro.IndiceFormaDesem == 1) //"eFECTIVO"
                {
                    pGiro.idctabancaria = 0;
                    pGiro.cod_banco = 0;
                    pGiro.num_cuenta = null;
                    pGiro.tipo_cuenta = -1;
                }
                if (ctlGiro.IndiceFormaDesem != 1 && ctlGiro.IndiceFormaDesem != 4) //"eFECTIVO"
                {

                    //DETERMINAR LA IDENTIFICACIÓN DE LA CUENTA BANCARIA
                    CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ctlGiro.ValueEntidadOrigen), ctlGiro.TextCuentaOrigen, (Usuario)Session["usuario"]);
                    Int64 idCta = CuentaBanc.idctabancaria;
                    //DATOS DE FORMA DE PAGO
                    if (ctlGiro.IndiceFormaDesem == 3) //"Transferencia"
                    {
                        pGiro.idctabancaria = idCta;
                        pGiro.cod_banco = Convert.ToInt32(ctlGiro.ValueEntidadDest);
                        pGiro.num_cuenta = ctlGiro.TextNumCuenta;
                        pGiro.tipo_cuenta = Convert.ToInt32(ctlGiro.ValueTipoCta);
                    }
                    else if (ctlGiro.IndiceFormaDesem == 2) //Cheque
                    {
                        pGiro.idctabancaria = idCta;
                        pGiro.cod_banco = 0;        //NULO
                        pGiro.num_cuenta = null;    //NULO
                        pGiro.tipo_cuenta = -1;      //NULO
                    }
                    else if (ctlGiro.IndiceFormaDesem == 4)
                    {
                        pGenerarGiro = false;
                        
                        pVar.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? Convert.ToInt64(ctlGiro.ValueCuentaAhorro) : default(long?);

                        if (!pVar.numero_cuenta_ahorro_vista.HasValue)
                        {
                            VerError("No tiene una cuenta de ahorro a la vista asociada para esta forma de pago!.");
                        }
                    }
                    else
                    {
                        pGiro.idctabancaria = 0;
                        pGiro.cod_banco = 0;
                        pGiro.num_cuenta = null;
                        pGiro.tipo_cuenta = -1;
                    }
                }

                pGiro.fec_apro = DateTime.MinValue;
                pGiro.cob_comision = 0;
                // pGiro.valor = Convert.ToInt64(this.txtTotalPagar.Text.Replace(".", ""));

                if (pLiqui.lstLista.Count > 0)
                {
                    pLiqui.lstLista = null;
                    ObtenerListaGrilla2();

                    pLiqui2.lstLista = ObtenerListaGrilla2();

                    foreach (var elemento in pLiqui2.lstLista)
                    {
                        pLiqui2.capitalizar_int = cbCapitalizaInteres.Checked == true ? 1 : 0;
                        pLiqui2.cod_deudor = Convert.ToInt64(Session["Cod_deudor"]);
                        pLiqui2.numero_cuenta_ahorro_vista = !string.IsNullOrWhiteSpace(ctlGiro.ValueCuentaAhorro) ? Convert.ToInt64(ctlGiro.ValueCuentaAhorro) : default(long?);

                        pLiqui2.cta_ahorros = Convert.ToString(ctlGiro.ValueCuentaAhorro);

                        if (ctlGiro.ValueCuentaAhorro != "")
                            pLiqui2.numero_cuenta_ahorro_vista = Convert.ToInt64(ctlGiro.ValueCuentaAhorro);

                        pLiqui2.cta_ahorros= Convert.ToString(ctlGiro.ValueCuentaAhorro);
                        pLiqui2.fecha_liquidacion = Convert.ToDateTime(txtFechaLiqui.Text);
                        pLiqui2.valor_pagar = Convert.ToDecimal(Session["PagoInteres"]);


                        if (ctlGiro.Visible == false)
                        {
                            pGenerarGiro = false;
                            pVar.numero_cuenta_ahorro_vista = 0;
                            pGiro.valor = 0;
                        }
                        if (ctlGiro.Visible == true)
                        {
                            pGenerarGiro = true;

                        }

                        pGiro.cod_persona = Convert.ToInt64(Session["Cod_deudor"]); ;

                        LiquiService.GuardarLiquidacionCDAT(ref COD_OPE, vOpe, pGenerarGiro, Convert.ToInt64(ctlGiro.IndiceFormaDesem), pGiro, pLiqui2, Usuario);

                    }
                }

                // Grabar comprobante 
                if (vOpe.cod_ope != 0)
                {
                    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = vOpe.cod_ope;
                    Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 10;
                    Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = Session["Cod_deudor"]; //"<Colocar Aquí el código de la persona del servicio>"
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");

                    Session[LiquiService.CodigoProgramaLiqui + ".id"] = idObjeto;
                    Session["Cod_deudor"] = null;
                    Session["PagoInteres"] = null;

                }

                Site toolBar = (Site)Master;
                toolBar.eventoRegresar += btnRegresar_Click;

                toolBar.MostrarExportar(false);
                toolBar.MostrarGuardar(false);

                mvPrincipal.ActiveViewIndex = 1;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LiquiService.CodigoProgramaLiqui, "btnContinuarMen_Click", ex);
        }
    }


    protected void cbCapitalizaInteres_CheckedChanged(object sender, EventArgs e)
    {
        if (cbCapitalizaInteres.Checked == true)
        {
            ctlGiro.Visible = false;
        }
        if (cbCapitalizaInteres.Checked == false)
        {
            ctlGiro.Visible = true;

        }
    }
}