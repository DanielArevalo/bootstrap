using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Linq;
using Xpinn.Util;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Net;
using System.Reflection;

partial class Lista : GlobalWeb
{    
    CuentaService CuentaService = new CuentaService();
    InterfazENPACTO interfazEnpacto;
    string convenio = "";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CuentaService.CodigoProgramaConciliacion, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarExportar(false);
            interfazEnpacto = new InterfazENPACTO("0123456789ABCDEFFEDCBA9876543210", "00000000000000000000000000000000");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaConciliacion, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Usuario usu = (Usuario)Session["Usuario"];
            txtConvenio.Text = ConvenioTarjeta(0);
            txtIpAppliance.Text = IpApplianceConvenioTarjeta();
            if (!IsPostBack)
            {                
                txtFecha.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString(gFormatoFecha);
                txtFechaFinal.Text = DateTime.Now.ToString(gFormatoFecha);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaConciliacion, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            ConsultaPeriodo();
        }
    }


   
    /// <summary>
    /// Método para cargar el list de movimientos para poderlos aplicar
    /// </summary>
    /// <param name="lstArchivo"></param>
    /// <returns></returns>
    public List<Movimiento> TrasladarList(DateTime pfecha, List<InterfazENPACTO.archivoSIC> lstArchivo)
    {
        List<Movimiento> lstConsulta = new List<Movimiento>();
        foreach (InterfazENPACTO.archivoSIC entidad in lstArchivo)
        {
            Boolean bTieneMonto = false;
            string error = "";
            int? _cod_ofi = null;
            Movimiento entmov = new Movimiento();
            entmov.fecha_movimiento = pfecha;
            entmov.fecha = entidad.fecha;
            entmov.hora = entidad.hora;
            entmov.documento = entidad.documento;
            entmov.nrocuenta = entidad.nrocuenta;
            entmov.tarjeta = entidad.tarjeta;
            entmov.tipotransaccion = entidad.tipotransaccion;
            entmov.tipocuenta = CuentaService.ConsultarTipoCuenta(convenio, entmov.nrocuenta, ref _cod_ofi, ref error, (Usuario)Session["Usuario"]);
            if (entmov.tipocuenta == "")
                entmov.tipocuenta = "A";
            entmov.cod_ofi = _cod_ofi;
            entmov.tipo_tran = HomologaTipoTran(entmov.tipocuenta, entmov.tipotransaccion);
            entmov.descripcion = entidad.descripcion;
            entmov.monto = ConvertirStringToDecimal(entidad.monto) / 100;
            entmov.comision = ConvertirStringToDecimal(entidad.comision) / 100;
            entmov.lugar = entidad.lugar;
            entmov.operacion = entidad.operacion;
            entmov.red = entidad.red;
            entmov.error = "";
            // Determinar saldo actual de la cuenta           
            try { entmov.saldo_total = CuentaService.ConsultarSaldoCuenta(txtConvenio.Text, entmov.nrocuenta, ref error, (Usuario)Session["Usuario"]); } catch { }
            // Validar si aplicó el monto
            Movimiento emov = new Movimiento();
            try { emov = CuentaService.ConsultarMovimiento(0, entmov.tarjeta, entmov.operacion, entmov.tipotransaccion, entmov.documento, entmov.fecha, entmov.monto, (Usuario)Session["Usuario"]); } catch { }
            if (emov != null)
            {
                entmov.num_tran_verifica = emov.num_tran;
                error = "";
                // Determinar datos del monto
                try
                { 
                    Movimiento eAplicacion = new Movimiento();
                    eAplicacion = CuentaService.DatosDeAplicacion(emov.num_tran, entmov.nrocuenta, emov.cod_ope, emov.cod_cliente, ConvertirStringToDate(entmov.fecha), entmov.monto, entmov.tipo_tran, entmov.operacion, ref error, (Usuario)Session["Usuario"]);
                    if (eAplicacion != null)
                        bTieneMonto = true;
                    entmov.cod_ope_apl = eAplicacion.cod_ope_apl;
                    entmov.num_tran_apl = eAplicacion.num_tran_apl;
                    entmov.valor_apl = eAplicacion.valor_apl;
                    entmov.num_comp_apl = eAplicacion.num_comp_apl;
                    entmov.tipo_comp_apl = eAplicacion.tipo_comp_apl;
                    entmov.cuenta_porcobrar = eAplicacion.cuenta_porcobrar;
                }
                catch
                {
                    VerError("Error.:" + error);
                }
                // Determinar datos de la comisión
                try
                {
                    int? tipo_tran = CuentaService.TipoTranComision(entmov.tipocuenta, entmov.tipo_tran);
                    Movimiento eAplicacionCom = new Movimiento();
                    eAplicacionCom = CuentaService.DatosDeAplicacion(emov.num_tran, entmov.nrocuenta, emov.cod_ope, emov.cod_cliente, ConvertirStringToDate(entmov.fecha), entmov.comision, tipo_tran, entmov.operacion, ref error, (Usuario)Session["Usuario"]);
                    if (entmov.cod_ope_apl == null)
                        entmov.cod_ope_apl = eAplicacionCom.cod_ope_apl;
                    entmov.comision_apl = eAplicacionCom.valor_apl;
                    if (!bTieneMonto || entmov.num_comp_apl == null)
                    {
                        entmov.num_comp_apl = eAplicacionCom.num_comp_apl;
                        entmov.tipo_comp_apl = eAplicacionCom.tipo_comp_apl;
                    }
                }
                catch (Exception ex)
                {
                    VerError("Error..:" + error + ex.Message);
                }
            }
            if (entmov.tipotransaccion == "A" || entmov.tipotransaccion == "B" || entmov.documento.Trim() == "no existe")
                entmov.monto = 0;
            if (entidad.tipotransaccion == "P")
            {
                entmov.monto_plastico = entmov.monto;
            }
            else
            {
                if (entidad.red == "3")
                    entmov.monto_pos = entmov.monto;
                else
                    entmov.monto_ath = entmov.monto;
            }
            lstConsulta.Add(entmov);
        }        
        return lstConsulta;
    }
           
    /// <summary>
    /// Método para exportar listado de movimientos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvLista.Rows.Count > 0 && Session["DTCUENTAS"] != null)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                GridView gvExportar = CopiarGridViewParaExportar(gvLista, "DTCUENTAS");
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvExportar);

                for (int i = 0; i < gvExportar.Rows.Count; i++)
                {
                    GridViewRow row = gvExportar.Rows[i];
                    row.Cells[2].Attributes.Add("style", "mso-number-format:\\@");
                    row.Cells[3].Attributes.Add("style", "mso-number-format:\\@");
                    row.Cells[4].Attributes.Add("style", "mso-number-format:\\@");
                    row.Cells[5].Attributes.Add("style", "mso-number-format:\\@");
                    row.Cells[6].Attributes.Add("style", "mso-number-format:\\@");
                }
                pagina.RenderControl(htw);

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("Content-Disposition", "attachment;filename=Conciliacion.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                string style = @"<style> .text { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Write(sb.ToString());
                Response.End();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch
        {
        }
    }


    public string formatoFecha()
    {
        return gFormatoFecha;
    }


    /// <summary>
    /// Homologa el tipo de transacción de ENPACTO con FINANCIAL
    /// </summary>
    /// <param name="ptipocuenta"></param>
    /// <param name="tipotransaccion"></param>
    /// <returns></returns>
    protected int? HomologaTipoTran(string ptipocuenta, string tipotransaccion)
    {   
        return CuentaService.HomologaTipoTran(ptipocuenta, tipotransaccion);
    }

    /// <summary>
    /// Trae todos los movimientos de ENPACTO en el período dado
    /// </summary>
    private void ConsultaPeriodo()
    {
        lblError.Text = "";
        int control = 0;
        VerError("");
        List<Movimiento> lstConsulta = new List<Movimiento>();        
        try
        {            
            // Determinar datos del convenio
            List <InterfazENPACTO.archivoSIC> lstArchivo = new List<InterfazENPACTO.archivoSIC>();
            convenio = ConvenioTarjeta();
            string s_usuario_applicance = "";
            string s_clave_appliance = "";            
            SeguridadConvenioTarjeta(ref s_usuario_applicance, ref s_clave_appliance);
            interfazEnpacto.ConfiguracionAppliance(IpApplianceConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);
            // Consultar los errores
            string error = "";
            DateTime fechaFinal = ConvertirStringToDate(txtFechaFinal.Text);
            DateTime fecha = ConvertirStringToDate(txtFecha.Text);
            while (fecha <= fechaFinal && control <= 365)
            { 
                try
                {
                    string pRequestXmlString = "";
                    if (interfazEnpacto.ServicioSICENPACTO(fecha, convenio, ref pRequestXmlString, ref lstArchivo, ref error))
                    {
                        // Cargar las transacciones
                        List<Movimiento> lstConsultaFecha = TrasladarList(fecha, lstArchivo);
                        lstConsulta = lstConsulta.Union(lstConsultaFecha).ToList();
                    }
                    else
                    {
                        lblError.Text += " No pudo consultar" + fecha.ToString("dd/MM/yyyy") + " " + error;
                    }
                }
                catch (Exception ex)
                {
                    if (lblError.Text == "")
                        lblError.Text += "No se pudo leer el archivo. Error:";
                     lblError.Text += ex.Message + " " + fecha.ToString("dd/MM/yyyy") + " " + error;
                    return;
                }                
                fecha = fecha.AddDays(1);
                control += 1;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaConciliacion, "ConsultaPeriodo", ex);
        }
        // Consolidar registros
        List<Movimiento> LstDetalleComprobante = new List<Movimiento>();
        if (cbdetallado.Checked)
            LstDetalleComprobante = (from o in lstConsulta
                                     group o by new { o.num_comp_apl, o.tipo_comp_apl} into g
                                     select new Movimiento
                                     {
                                         num_comp_apl = g.Key.num_comp_apl,
                                         tipo_comp_apl = g.Key.tipo_comp_apl,
                                         monto = Convert.ToDecimal(g.Sum(x => x.monto)),
                                         monto_pos = Convert.ToDecimal(g.Sum(x => x.monto_pos)),
                                         monto_ath = Convert.ToDecimal(g.Sum(x => x.monto_ath)),
                                         monto_plastico = Convert.ToDecimal(g.Sum(x => x.monto_plastico)),
                                         comision = Convert.ToDecimal(g.Sum(x => x.comision)),
                                         cuenta_porcobrar = Convert.ToDecimal(g.Sum(x => x.cuenta_porcobrar)),
                                         valor_apl = Convert.ToDecimal(g.Sum(x => x.valor_apl))
                                     }).OrderBy(x => x.num_comp_apl).ToList<Movimiento>();
        else
            LstDetalleComprobante = (from o in lstConsulta
                                     group o by new { o.fecha_movimiento } into g
                                     select new Movimiento
                                     {
                                         fecha_movimiento = g.Key.fecha_movimiento,
                                         monto = Convert.ToDecimal(g.Sum(x => x.monto)),
                                         monto_pos = Convert.ToDecimal(g.Sum(x => x.monto_pos)),
                                         monto_ath = Convert.ToDecimal(g.Sum(x => x.monto_ath)),
                                         monto_plastico = Convert.ToDecimal(g.Sum(x => x.monto_plastico)),
                                         comision = Convert.ToDecimal(g.Sum(x => x.comision)) 
                                     }).OrderBy(x => x.fecha_movimiento).ToList<Movimiento>();
        foreach (Movimiento item in LstDetalleComprobante)
        {
            item.saldo_total = item.monto + item.comision;
            decimal valor = 0;
            DateTime? fecha = null;
            if (cbdetallado.Checked)
            { 
                CuentaService.ComprobanteValorBanco(Convert.ToInt64(item.num_comp_apl), Convert.ToInt64(item.tipo_comp_apl), ref valor, ref fecha, (Usuario)Session["Usuario"]);
                item.comision_apl = valor;
                item.fecha_movimiento = fecha;
                item.diferencia = Convert.ToDecimal(item.comision_apl) - Convert.ToDecimal(item.saldo_total);
            }
            else
            {
                fecha = item.fecha_movimiento;
                CuentaService.ComprobanteValorBanco(0, 0, ref valor, ref fecha, (Usuario)Session["Usuario"]);
                item.comision_apl = valor;
                item.diferencia = Convert.ToDecimal(item.comision_apl) - Convert.ToDecimal(item.saldo_total);
            }
        }
        // Ocultar columns
        if (cbdetallado.Checked)
        {
            gvLista.Columns[0].Visible = true;
            gvLista.Columns[1].Visible = true;
            gvLista.Columns[9].Visible = true;
        }
        else
        {
            gvLista.Columns[0].Visible = false;
            gvLista.Columns[1].Visible = false;
            gvLista.Columns[9].Visible = false;
        }
        // Mostrar los resultados
        panelGrilla.Visible = true;
        lblTotalRegs.Visible = true;
        Site toolBar = (Site)this.Master;
        if (LstDetalleComprobante.Count > 0)
        {
            // Llenar la tabla
            gvLista.DataSource = LstDetalleComprobante;
            gvLista.DataBind();
            toolBar.MostrarExportar(true);
        }
        else
        {
            toolBar.MostrarExportar(false);
        }
        lblTotalRegs.Text = "<br/> Registros encontrados " + LstDetalleComprobante.Count.ToString();
        Session["DTCUENTAS"] = LstDetalleComprobante;

    }


}