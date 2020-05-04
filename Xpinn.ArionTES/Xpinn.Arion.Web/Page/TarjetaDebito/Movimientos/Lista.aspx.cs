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
    int tipoOpe = 124;
    string clave = "****";  // Esto sirve para que el administrador de EXPINN pueda generar archivos desde una fecha hasta hoy
    string claveSinConciliar = "$$$$";
    string descripcionSinConciliar = "TRANSACCION SIN CONCILIAR";
    bool bValidar = true;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CuentaService.CodigoProgramaMovimiento, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoImportar += btnImportar_Click;            
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            Activar(0);
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            toolBar.MostrarGuardar(false);
            panelProceso.Visible = false;
            interfazEnpacto = new InterfazENPACTO("0123456789ABCDEFFEDCBA9876543210", "00000000000000000000000000000000");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimiento, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Usuario usu = (Usuario)Session["Usuario"];
            if (usu != null)
            { 
                if (usu.codperfil == 1)
                {
                    txtNroCuenta.Visible = true;
                    lblNroCuenta.Visible = true;
                }
            }
            txtConvenio.Text = ConvenioTarjeta(0);
            txtIpAppliance.Text = IpApplianceConvenioTarjeta();
            if (!IsPostBack)
            {                
                txtFecha.Text = DateTime.Now.ToString(gFormatoFecha);
                txtFechaAplica.Text = DateTime.Now.ToString(gFormatoFecha);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimiento, "Page_Load", ex);
        }
    }


    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Page.Validate();
        gvLista.Visible = true;
        if (Page.IsValid)
        {
            if (txtNroCuenta.Text == clave)
                ConsultaPeriodo();
            else
                Actualizar();
        }
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            if (Session["DTCUENTAS"] != null)
            {
                List<Movimiento> lstConsulta = (List<Movimiento>)Session["DTCUENTAS"];
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                return;
            }
            Actualizar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimiento, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para consumir el WEBSERVICES de ENPACTO y leer los movimientos en el APPLIANCE
    /// </summary>
    private void Actualizar()
    {
        VerError("");
        try
        {
            List<InterfazENPACTO.archivoSIC> lstArchivo = new List<InterfazENPACTO.archivoSIC>();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Cargar configuración del appliance
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            convenio = ConvenioTarjeta(0);
            string s_usuario_applicance = "";
            string s_clave_appliance = "";
            SeguridadConvenioTarjeta(ref s_usuario_applicance, ref s_clave_appliance);
            interfazEnpacto.ConfiguracionAppliance(IpApplianceConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Leer movimientos del appliance con el web services
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            string error = "";
            try
            {
                Site toolBar = (Site)this.Master;
                string pRequestXmlString = "";
                if (!interfazEnpacto.ServicioSICENPACTO(ConvertirStringToDate(txtFecha.Text), convenio, ref pRequestXmlString, ref lstArchivo, ref error))
                {
                    toolBar.MostrarGuardar(false);
                    VerError("Se presento error." + error + " " + pRequestXmlString);
                    return;
                }
                if (lstArchivo.Count <= 0)
                {                    
                    txtRespuesta.Text = pRequestXmlString;                    
                }
                else
                {
                    toolBar.MostrarGuardar(true);
                }
                txtpRequestXmlString.Text = pRequestXmlString;
                lblError.Text = error;
            }
            catch (Exception ex)
            {
                lblError.Text = "No se pudo leer el archivo. Error:" + ex.Message + " " + error;    
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Convertir la lista para poderla mostrar
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<Movimiento> lstConsulta = TrasladarList(lstArchivo, txtFecha.Text);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Incluir transacciones que no han sido conciliadas
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (txtNroCuenta.Text == claveSinConciliar)
            {
                if (lstConsulta == null) lstConsulta = new List<Movimiento>();
                List<Movimiento> lstSinConciliar = CuentaService.ListaTransaccionesSinConciliar(convenio, (Usuario)Session["Usuario"]);
                if (lstSinConciliar != null)
                { 
                    foreach (Movimiento item in lstSinConciliar)
                    {
                        Movimiento itemSinConciliar = new Movimiento();
                        itemSinConciliar.fecha = item.fecha;
                        itemSinConciliar.documento = item.documento;
                        itemSinConciliar.nrocuenta = item.nrocuenta;
                        itemSinConciliar.tarjeta = item.tarjeta;
                        itemSinConciliar.valor_apl = item.monto;
                        itemSinConciliar.comision_apl = item.comision;
                        itemSinConciliar.cod_ope_apl = item.cod_ope;
                        itemSinConciliar.num_tran_verifica = item.num_tran_tarjeta;
                        itemSinConciliar.descripcion = descripcionSinConciliar;
                        lstConsulta.Add(itemSinConciliar);
                    }
                }
            }

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Cargar lista en la gridview
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           
            if (lstConsulta.Count > 0)
            {
                // Cargar listado de cajas
                Xpinn.Caja.Services.CajaService cajaservicio = new Xpinn.Caja.Services.CajaService();
                List<Xpinn.Caja.Entities.Caja> listcaja = new List<Xpinn.Caja.Entities.Caja>();
                listcaja = cajaservicio.ListarComboCajaXDatafono((Usuario)Session["Usuario"]);
                Session["listcaja"] = listcaja;
                // Llenar la tabla
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;                
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();                               
            }
            else
            {
                panelGrilla.Visible = false;                
                lblTotalRegs.Visible = false;
            }
            Session["DTCUENTAS"] = lstConsulta;
            Session.Add(CuentaService.CodigoProgramaMovimiento + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimiento, "Actualizar", ex);
        }
    }

    private void TransaccionesSinConciliar()
    {
        try
        {
        }
        catch (Exception ex)
        { VerError(ex.Message);  }
     }

    /// <summary>
    /// Método para cargar el list de movimientos para poderlos aplicar
    /// </summary>
    /// <param name="lstArchivo"></param>
    /// <returns></returns>
    public List<Movimiento> TrasladarList(List<InterfazENPACTO.archivoSIC> lstArchivo, string pfecha_corte)
    {
        List<Movimiento> lstConsulta = new List<Movimiento>();
        foreach (InterfazENPACTO.archivoSIC entidad in lstArchivo)
        {
            Boolean bTieneMonto = false;
            string error = "";
            int? _cod_ofi = null;
            Movimiento entmov = new Movimiento();
            entmov.fecha = entidad.fecha;
            entmov.hora = entidad.hora;
            entmov.documento = entidad.documento;
            entmov.tarjeta = entidad.tarjeta;
            entmov.nrocuenta = entidad.nrocuenta;
            string _cuentaHomologa = CuentaService.HomologarCuentas(entidad.tarjeta, entidad.nrocuenta, (Usuario)Session["Usuario"]);
            if (_cuentaHomologa != null)
                if (_cuentaHomologa != "")
                    entmov.nrocuenta = _cuentaHomologa;
            entmov.tipotransaccion = entidad.tipotransaccion;
            entmov.tipocuenta = CuentaService.ConsultarTipoCuenta(convenio, entmov.nrocuenta, ref _cod_ofi, ref error, (Usuario)Session["Usuario"]);
            if (entmov.tipocuenta == "")
                entmov.tipocuenta = "A";
            entmov.cod_ofi = _cod_ofi;
            entmov.tipo_tran = HomologaTipoTran(entmov.tipocuenta, entmov.tipotransaccion, 0);
            entmov.descripcion = entidad.descripcion;
            entmov.monto = ConvertirStringToDecimal(entidad.monto) / 100;
            entmov.comision = ConvertirStringToDecimal(entidad.comision) / 100;
            entmov.lugar = entidad.lugar;
            entmov.operacion = entidad.operacion;
            entmov.red = entidad.red;
            entmov.fecha_corte = pfecha_corte;
            entmov.error = "";
            if (bValidar)
            {
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
                        eAplicacion = CuentaService.DatosDeAplicacion(emov.num_tran, entmov.nrocuenta, emov.cod_ope, emov.cod_cliente, Convert.ToDateTime(ConvertirFechaTarjeta(entmov.fecha)), entmov.monto, entmov.tipo_tran, entmov.operacion, ref error, (Usuario)Session["Usuario"]);
                        if (eAplicacion != null)
                            bTieneMonto = true;
                        entmov.cod_ope_apl = eAplicacion.cod_ope_apl;
                        entmov.num_tran_apl = eAplicacion.num_tran_apl;
                        entmov.valor_apl = eAplicacion.valor_apl;
                        entmov.num_comp_apl = eAplicacion.num_comp_apl;
                        entmov.tipo_comp_apl = eAplicacion.tipo_comp_apl;
                        entmov.cuenta_porcobrar = eAplicacion.cuenta_porcobrar;
                    }
                    catch (Exception ex)
                    {
                        VerError("Error.:" + error + ex.Message + " " + entmov.nrocuenta);
                    }
                    // Determinar datos de la comisión
                    try
                    {
                        int? tipo_tran = CuentaService.TipoTranComision(entmov.tipocuenta, entmov.tipo_tran);
                        Movimiento eAplicacionCom = new Movimiento();
                        eAplicacionCom = CuentaService.DatosDeAplicacion(emov.num_tran, entmov.nrocuenta, emov.cod_ope, emov.cod_cliente, Convert.ToDateTime(ConvertirFechaTarjeta(entmov.fecha)), entmov.comision, tipo_tran, entmov.operacion, ref error, (Usuario)Session["Usuario"]);
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
                    // Sumar cuenta por cobrar
                    if (entmov.monto == 0)
                    { 
                        entmov.comision_apl = Convert.ToDecimal(entmov.comision_apl) + entmov.cuenta_porcobrar;
                    }
                    else
                    {
                        if (entmov.comision == 0)
                            entmov.valor_apl = Convert.ToDecimal(entmov.valor_apl) + entmov.cuenta_porcobrar;
                    }
                }
            }
            // Colocar monto en cero para efectos de conciliar
            if  (txtNroCuenta.Text.Trim() == clave)
                if (entmov.tipotransaccion == "A" || entmov.tipotransaccion == "B" || entmov.documento.Trim() == "no existe")
                    entmov.monto = 0;
            // Cargar datos
            if (entmov.nrocuenta == txtNroCuenta.Text || txtNroCuenta.Visible == false || txtNroCuenta.Text.Trim() == "" || txtNroCuenta.Text.Trim() == clave || txtNroCuenta.Text.Trim() == claveSinConciliar)
                if ((chNokMonto.Checked && entmov.monto != (entmov.valor_apl == null ? 0 : entmov.valor_apl)) || !chNokMonto.Checked)
                    if ((chNokComision.Checked && entmov.comision != (entmov.comision_apl == null ? 0: entmov.comision_apl)) || !chNokComision.Checked)
                        lstConsulta.Add(entmov);
        }        
        return lstConsulta;
    }

    protected DateTime? ConvertirFechaTarjeta(string pfecha)
    {
        try
        {
            return new DateTime(Convert.ToInt32(pfecha.Substring(0, 4)), Convert.ToInt32(pfecha.Substring(5, 2)), Convert.ToInt32(pfecha.Substring(8, 2)));
        }
        catch
        {
            return ConvertirStringToDate(pfecha);
        }
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
                if (txtNroCuenta.Text == "&&&&")
                {
                    string fic = "lstConsulta.csv";
                    // Eliminar archivo si ya existe
                    try
                    {
                        File.Delete(fic);
                    }
                    catch
                    { }
                    // Generar el archivo
                    List<Movimiento> lstConsulta = (List<Movimiento>)Session["DTCUENTAS"];
                    foreach (Movimiento item in lstConsulta)
                    {
                        int i = 0;
                        string texto = "";
                        FieldInfo[] propiedades = item.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        foreach (FieldInfo f in propiedades)
                        {
                            i += 1;
                            object valorObject = f.GetValue(item);
                            // Si no soy nulo
                            if (valorObject != null)
                            {
                                string valorString = valorObject.ToString();
                                if (valorObject is DateTime)
                                {
                                    DateTime? fechaValidar = valorObject as DateTime?;
                                    if (fechaValidar.Value != DateTime.MinValue)
                                    {
                                        texto += f.GetValue(item) + "|";
                                    }
                                    else
                                    {
                                        texto += "" + "|";
                                    }
                                }
                                else
                                {
                                    texto += f.GetValue(item) + "|";
                                }
                            }
                            else
                            {
                                texto += "" + "|";
                            }
                        }
                        lblMensj.Text = "->" + i.ToString() + "<-";
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + fic, true);
                        sw.WriteLine(texto);
                        sw.Close();
                    }
                    System.IO.StreamReader sr;
                    sr = File.OpenText(Server.MapPath("") + fic);
                    string texo = sr.ReadToEnd();
                    sr.Close();
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ClearHeaders();
                    HttpContext.Current.Response.ContentType = "text/plain";
                    HttpContext.Current.Response.Write(texo);
                    HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
                    HttpContext.Current.Response.Flush();
                    File.Delete(Server.MapPath("") + fic);
                    HttpContext.Current.Response.End();
                }
                else
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
                        row.Cells[3].Attributes.Add("style", "mso-number-format:\\@");
                        row.Cells[4].Attributes.Add("style", "mso-number-format:\\@");
                        row.Cells[10].Attributes.Add("style", "mso-number-format:\\@");
                        row.Cells[11].Attributes.Add("style", "mso-number-format:\\@");
                    }
                    pagina.RenderControl(htw);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("Content-Disposition", "attachment;filename=Movimientos.xls");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    string style = @"<style> .text { mso-number-format:\@; } </style>";
                    Response.Write(style);
                    Response.Write(sb.ToString());
                    Response.End();
                }
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

    /// <summary>
    /// Método para guardar los datos. Se pide confirmación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            // Validar la fecha
            if (txtFechaAplica.Text.Trim() == "")
            {
                VerError("Debe ingresar la fecha de aplicación");
                return;
            }
            // Validar que exista la parametrización contable por procesos
            DateTime FechaProceso = ConvertirStringToDate(txtFechaAplica.Text.Trim());
            if (ValidarProcesoContable(FechaProceso, tipoOpe) == false)
            {
                VerError("No se encontró parametrización contable por procesos para el tipo de operación " + tipoOpe + "= Transacciones Con Tarjeta Debito");
                return;
            }
            if (txtNroCuenta.Text.Trim() == clave)
                ctlMensaje.MostrarMensaje("Desea guardar MOVIMIENTOS de TARJETA DEBITO?");
            else
                ctlMensaje.MostrarMensaje("Desea aplicar MOVIMIENTOS de TARJETA DEBITO?");
        }
        catch
        {
        }
    }

    /// <summary>
    ///  Una vez confirmado el proceso se procede a aplicar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
   protected void btnContinuarMen_Click(object sender, EventArgs e)
   {
       if (Session["DTCUENTAS"] == null)
       {
           VerError("No se ha generado el listado de cuentas para poder actualizar");
           return;
       }
       txtRespuesta.Text = "";
       try
       {
            string Error = "";
            if (txtNroCuenta.Text.Trim() == clave)
            {
                List<Movimiento> lstConsulta = (List<Movimiento>)Session["DTCUENTAS"];
                foreach (Movimiento item in lstConsulta)
                {
                    if (!CuentaService.ActualizarMovimiento(convenio, item, (Usuario)Session["Usuario"], ref Error))
                    {
                        VerError("Aplicar " + Error);
                        return;
                    }
                }
            }
            else
            { 
                List<Movimiento> lstConsulta = (List<Movimiento>)Session["DTCUENTAS"];
                if (lstConsulta.Count > 0)
               {
                    int contar = 0;               
                    // Homologando tipo de transacción y validando la cuenta              
                    foreach (Movimiento entidad in lstConsulta)
                    {
                        // Si el tipo de cuenta no esta especificado por defecto ahorros
                        string errorcon = "";
                        int? _cod_ofi = null;
                        entidad.tipocuenta = CuentaService.ConsultarTipoCuenta(convenio, entidad.nrocuenta, ref _cod_ofi, ref errorcon, (Usuario)Session["Usuario"]);
                        if (entidad.tipocuenta == "")
                            entidad.tipocuenta = "A";
                        entidad.cod_ofi = _cod_ofi;
                        // Determinar el tipo de transacción                        
                        entidad.tipo_tran = HomologaTipoTran(entidad.tipocuenta, entidad.tipotransaccion, 0);
                        // Las consultas, declinadas, otras, cambio  de pin y consulta de costo no aplicar el valor
                        if (entidad.tipotransaccion == "1" || entidad.tipotransaccion == "4" || entidad.tipotransaccion == "7" || entidad.tipotransaccion == "A" || entidad.tipotransaccion == "B")
                            entidad.monto = 0;
                        else
                            entidad.monto = Math.Abs(entidad.monto);
                        entidad.esdatafono = TransaccionEsDatafono(entidad.operacion, entidad.tipotransaccion, entidad.red);
                        // Verificar si la transacción ya fue aplicada
                        if (entidad.documento != "no existe" && entidad.descripcion != descripcionSinConciliar && bValidar)
                        {
                            bool bAplicado = true;
                            // Verificar si la transaccion fue registrada
                            Movimiento emov = new Movimiento();
                            emov = CuentaService.ConsultarMovimiento(0, entidad.tarjeta, entidad.operacion, entidad.tipotransaccion, entidad.documento, entidad.fecha, 0, (Usuario)Session["Usuario"]);
                            if (emov == null)
                            {                     
                                bAplicado = false;
                            }
                            else
                            { 
                                // Verificar si el monto fue aplicado
                                string error = "";
                                if (entidad.monto != 0)
                                { 
                                    Movimiento eAplicacion = new Movimiento();
                                    eAplicacion = CuentaService.DatosDeAplicacion(emov.num_tran, entidad.nrocuenta, emov.cod_ope, emov.cod_cliente, ConvertirStringToDate(entidad.fecha), entidad.monto, entidad.tipo_tran, entidad.operacion, ref error, (Usuario)Session["Usuario"]);
                                    VerError("eAplicacion.valor_apl:" + eAplicacion.valor_apl);
                                    if (eAplicacion == null)
                                    { 
                                        bAplicado = false;
                                    }
                                    else
                                    { 
                                        if (eAplicacion.num_tran_apl == null || eAplicacion.num_tran_apl == 0)
                                            bAplicado = false;
                                    }
                                }
                                // Verificar si la comision fue aplicado
                                if (entidad.comision != 0)
                                {
                                    int? tipo_tran = CuentaService.TipoTranComision(emov.tipocuenta, emov.tipo_tran);
                                    Movimiento eAplicacionCom = new Movimiento();
                                    eAplicacionCom = CuentaService.DatosDeAplicacion(emov.num_tran, entidad.nrocuenta, emov.cod_ope, emov.cod_cliente, ConvertirStringToDate(entidad.fecha), entidad.comision, tipo_tran, entidad.operacion, ref error, (Usuario)Session["Usuario"]);
                                    VerError("eAplicacionCom.num_tran_apl" + eAplicacionCom.num_tran_apl);                                
                                    if (eAplicacionCom == null)
                                    { 
                                        bAplicado = false;
                                    }
                                    else
                                    {
                                        if (eAplicacionCom.num_tran_apl == null || eAplicacionCom.num_tran_apl == 0)
                                            bAplicado = false;
                                    }
                                }
                            }
                            // Si esta pendiente por aplicar entonces validar la cuenta
                            if (!bAplicado)
                            {
                                contar += 1;
                                // Validar el saldo de la cuenta
                                if (!entidad.esdatafono)
                                { 
                                    if (!CuentaService.ValidarCuenta(convenio, entidad.tarjeta, entidad.nrocuenta, entidad.tipotransaccion, entidad.monto, entidad.fecha, false, ref Error, (Usuario)Session["Usuario"]))
                                    {                    
                                        // Si es transacción de cuota de manejo deja pasar        
                                        if (entidad.tipotransaccion != "M")
                                        { 
                                            VerError("Tarjeta:" + entidad.tarjeta + " Cuenta:" + entidad.nrocuenta + " Operaciòn:" + entidad.operacion + " " + Error);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            contar += 1;
                        }
                    }
                    // Verificar si hay transacciones para aplicar
                    if (contar == 0)
                    {
                        VerError("No hay transacciones pendientes por aplicar");
                        return;
                    }
                }
                // Grabar los Datos
                DateTime fecha = ConvertirStringToDate(txtFechaAplica.Text.Trim());
                // Determinar código de proceso contable para generar el comprobante
                Int64? rpta = 0;
                if (!panelProceso.Visible && panelGeneral.Visible)
                {
                    rpta = ctlproceso.Inicializar(tipoOpe, fecha, (Usuario)Session["Usuario"]);
                    if (rpta > 1)
                    {
                        Site toolBar = (Site)Master;
                        toolBar.MostrarGuardar(false);
                        // Activar demás botones que se requieran
                        panelGeneral.Visible = false;
                        panelProceso.Visible = true;
                    }
                    else
                    {
                        // Crear la tarea de ejecución del proceso        
                        Int64 pCodOpe = 0;               
                        if (!CuentaService.AplicarMovimientos(convenio, fecha, lstConsulta, (Usuario)Session["Usuario"], ref Error, ref pCodOpe, 1))
                        {
                            VerError("Aplicar " + Error);
                            return;
                        }
                        else
                        {
                            ctlproceso.CargarVariables(pCodOpe, tipoOpe, null, (Usuario)Session["Usuario"]); 
                            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                        }
                    }
                }
               else
               {
                   VerError("No hay movimientos para aplicar");
               }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimiento, "btnContinuarMen_Click", ex);
        }
    }

    public string formatoFecha()
    {
        return gFormatoFecha;
    }

    protected void btnImportar_Click(object sender, ImageClickEventArgs e)
    {
        Activar(1);
    }

    /// <summary>
    /// Método para cargar desde archivo plano los movimientos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAceptarCarga_Click(object sender, EventArgs e)
    {
        // Cargar listado de cajas
        Xpinn.Caja.Services.CajaService cajaservicio = new Xpinn.Caja.Services.CajaService();
        List<Xpinn.Caja.Entities.Caja> listcaja = new List<Xpinn.Caja.Entities.Caja>();
        listcaja = cajaservicio.ListarComboCajaXDatafono((Usuario)Session["Usuario"]);
        Session["listcaja"] = listcaja;
        // Cargar el Archivo      
        if (FileUploadMetas.HasFile)
        {
            List<Movimiento> lstConsulta;
            if (txtNroCuenta.Text == "&&&&")
            {
                lstConsulta = new List<Movimiento>();
                Stream stream = FileUploadMetas.FileContent;
                using (StreamReader strReader = new StreamReader(stream))
                {
                    while (strReader.Peek() >= 0)
                    {
                        string readLine = strReader.ReadLine();
                        string[] arrayline = readLine.Split(Convert.ToChar("|"));
                        Movimiento entidad = new Movimiento();
                        FieldInfo[] propiedades = entidad.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        int i = 0;
                        foreach (FieldInfo f in propiedades)
                        {
                            String sCampo = f.Name;
                            object valorObject = f.GetValue(entidad);
                            if (i < arrayline.Count())
                            {
                                if (arrayline[i] != null)
                                {
                                    if (f.FieldType.Name == "decimal" || f.FieldType.Name == "Decimal")
                                        f.SetValue(entidad, ConvertirStringToDecimal(arrayline[i]));
                                    else if (f.FieldType.Name == "Nullable`1")
                                        if (f.FieldType.FullName.Contains("System.Int64"))
                                            f.SetValue(entidad, ConvertirStringToIntN(arrayline[i]));
                                        else if (f.FieldType.FullName.Contains("System.Decimal") || f.FieldType.FullName.Contains("System.decimal"))
                                            f.SetValue(entidad, ConvertirStringToDecimalN(arrayline[i]));
                                        else
                                            f.SetValue(entidad, ConvertirStringToInt32N(arrayline[i]));
                                    else if (f.FieldType.Name == "Boolean")
                                        f.SetValue(entidad, (arrayline[i] == "False" ? false : true));
                                    else
                                        f.SetValue(entidad, arrayline[i]);
                                }
                            }
                            i += 1;
                        }
                        lstConsulta.Add(entidad);
                    }
                    strReader.Close();
                }
            }
            else
            {
                List<InterfazENPACTO.archivoSIC> lstArchivo = new List<InterfazENPACTO.archivoSIC>();
                Stream stream = FileUploadMetas.FileContent;
                using (StreamReader strReader = new StreamReader(stream))
                {
                    lstArchivo = interfazEnpacto.CargarArchivo(strReader);
                    strReader.Close();
                }
                lstConsulta = TrasladarList(lstArchivo, txtFecha.Text);
            }
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            if (lstConsulta.Count > 0)
            {
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                panelGrilla.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros cargados " + lstConsulta.Count.ToString();
                Site toolBar = (Site)this.Master;
                toolBar.MostrarGuardar(true);
            }
            else
            {
                panelGrilla.Visible = false;
                lblTotalRegs.Visible = false;
            }
            Session["DTCUENTAS"] = lstConsulta;
            Session.Add(CuentaService.CodigoProgramaMovimiento + ".consulta", 1);
            Activar(0);
        }
    }

    /// <summary>
    /// Método para cancelar proceso de carga desde archivo plano
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCerrarCarga_Click(object sender, EventArgs e)
    {
        Activar(0);
    }

    /// <summary>
    ///  Método para navegación entre las diferentes paneles
    /// </summary>
    /// <param name="pindex"></param>
    protected void Activar(int pindex)
    {
        if (pindex == 0)
        {
            panelGrilla.Visible = true;
            panelCarga.Visible = false;
        }
        else
        {
            panelGrilla.Visible = false;
            panelCarga.Visible = true;
        }
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
            panelProceso.Visible = false;
            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            btnContinuarMen_Click(null, null);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Verificar si la transacción ya fue aplicada
            string fecha = e.Row.Cells[0].Text.ToString();
            string documento = e.Row.Cells[2].Text.ToString();
            string cuenta = e.Row.Cells[3].Text.ToString();
            string tarjeta = e.Row.Cells[4].Text.ToString();                                    
            string tipotransaccion = e.Row.Cells[5].Text.ToString();
            string operacion = e.Row.Cells[10].Text.ToString();
            string red = e.Row.Cells[11].Text.ToString().Trim();
            decimal monto = ConvertirStringToDecimal(e.Row.Cells[7].Text);
            decimal comision = ConvertirStringToDecimal(e.Row.Cells[8].Text);
            decimal valor_apl = ConvertirStringToDecimal(e.Row.Cells[14].Text);
            decimal comision_apl = ConvertirStringToDecimal(e.Row.Cells[16].Text);
            Movimiento emov = new Movimiento();
            if (bValidar)
            { 
                emov = CuentaService.ConsultarMovimiento(0, tarjeta, operacion, tipotransaccion, documento, fecha, monto, (Usuario)Session["Usuario"]);
                if (emov != null)
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    if (txtNroCuenta.Text.Trim() != clave)
                        if (emov.num_tran != null)
                            CuentaService.ActualizarMovimientoConciliacion(Convert.ToInt64(emov.num_tran), emov.fecha_corte, (Usuario)Session["Usuario"]);
                }
                // Si es consulta monto es cero
                if (tipotransaccion == "1")
                    valor_apl = 0;
                // Determinar si fue aplicado el monto
                if (tipotransaccion != "B")
                { 
                    if (valor_apl == Math.Abs(monto))
                    {
                        if (txtNroCuenta.Text == clave)
                        {
                            Label lblAplicada = (Label)e.Row.FindControl("lblAplicada");
                            if (lblAplicada != null)
                            {
                                lblAplicada.Visible = true;
                                lblAplicada.Text = "Ok";
                            }
                        }
                        else
                        { 
                            Image imgAplicada = (Image)e.Row.FindControl("imgAplicada");
                            if (imgAplicada != null)
                                imgAplicada.Visible = true;
                        }
                    }
                    else
                    {
                        if (txtNroCuenta.Text == clave)
                        {
                            Label lblAplicada = (Label)e.Row.FindControl("lblAplicada");
                            if (lblAplicada != null)
                            {
                                lblAplicada.Visible = true;
                                lblAplicada.Text = "NOk";
                            }
                        }
                        else
                        { 
                            Image imgNoAplicada = (Image)e.Row.FindControl("imgNoAplicada");
                            if (imgNoAplicada != null)
                                imgNoAplicada.Visible = true;
                        }
                    }
                }
                // Determinar si fue aplicada la comisión
                if (tipotransaccion != "B" && tipotransaccion != "M")
                {
                    if (comision_apl == Math.Abs(comision))
                    {
                        if (txtNroCuenta.Text == clave)
                        {
                            Label lblAplicadaCom = (Label)e.Row.FindControl("lblAplicadaCom");
                            if (lblAplicadaCom != null)
                            {
                                lblAplicadaCom.Visible = true;
                                lblAplicadaCom.Text = "Ok";
                            }
                        }
                        else
                        { 
                            Image imgAplicadaCom = (Image)e.Row.FindControl("imgAplicadaCom");
                            if (imgAplicadaCom != null)
                                imgAplicadaCom.Visible = true;
                        }
                    }
                    else
                    {
                        if (txtNroCuenta.Text == clave)
                        {
                            Label lblAplicadaCom = (Label)e.Row.FindControl("lblAplicadaCom");
                            if (lblAplicadaCom != null)
                            {
                                lblAplicadaCom.Visible = true;
                                lblAplicadaCom.Text = "NOk";
                            }
                        }
                        else
                        { 
                            Image imgNoAplicadaCom = (Image)e.Row.FindControl("imgNoAplicadaCom");
                            if (imgNoAplicadaCom != null)
                                imgNoAplicadaCom.Visible = true;
                        }
                    }
                }
                // Determinar si la transacción fue realizada por DATAFONO
                if (TransaccionEsDatafono(operacion, tipotransaccion, red))
                    e.Row.Cells[12].Text = "Si";
                else
                    e.Row.Cells[12].Text = "No";
            }
        }
    }

    protected bool TransaccionEsDatafono(string poperacion, string ptipotransaccion, string pred)
    {
        if (pred == "5" && ptipotransaccion == "9")
            return false;
        if (pred == "5" && ptipotransaccion != "9")
            return true;
        return false;
    }

    protected void cbSeleccionarEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox cbSeleccionarEncabezado = (CheckBox)sender;
        if (cbSeleccionarEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox cbSeleccionar = (CheckBox)rFila.FindControl("cbSeleccionar");
                cbSeleccionar.Checked = cbSeleccionarEncabezado.Checked;
            }
        }
    }

    protected int? HomologaTipoTran(string ptipocuenta, string tipotransaccion, int? tipo_convenio)
    {   
        return CuentaService.HomologaTipoTran(ptipocuenta, tipotransaccion, tipo_convenio);
    }

    /// <summary>
    /// Consultar datos de todo un período desde la fecha especificada hasta la fecha actual
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
            convenio = ConvenioTarjeta(0);
            string s_usuario_applicance = "";
            string s_clave_appliance = "";            
            SeguridadConvenioTarjeta(ref s_usuario_applicance, ref s_clave_appliance);
            interfazEnpacto.ConfiguracionAppliance(IpApplianceConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);
            
            // Consultar los periodos
            string error = "";
            DateTime fecha = ConvertirStringToDate(txtFecha.Text);
            DateTime fechaFinal = new DateTime(fecha.Year, fecha.Month, 1).AddMonths(2).AddDays(-1); /*DateTime.Now*/;
            if (fechaFinal > DateTime.Now)
                fechaFinal = DateTime.Now;
            int año = ConvertirStringToDate(txtFecha.Text).Year;
            while (fecha < fechaFinal && año == fecha.Year && control <= 365)
            { 
                try
                {
                    string pRequestXmlString = "";
                    if (interfazEnpacto.ServicioSICENPACTO(fecha, convenio, ref pRequestXmlString, ref lstArchivo, ref error))
                    {
                        // Cargar las transacciones
                        List<Movimiento> lstConsultaFecha = TrasladarList(lstArchivo, fecha.ToString("dd/MM/yyyy"));
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
            VerError(ex.Message);
        }
        
        // Mostrar los resultados
        panelGrilla.Visible = true;
        lblTotalRegs.Visible = true;
        lblTotalRegs.Text = "<br/> Registros encontrados en el perìodo " + lstConsulta.Count.ToString();
        if (lstConsulta.Count > 0)
        {
            // Llenar la tabla
            gvLista.DataSource = lstConsulta;
            gvLista.DataBind();
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(true);
        }

        Session["DTCUENTAS"] = lstConsulta;

    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        mpeNuevoAjuste.Show();
        txtNumTranTarjeta.Text = id;
        List<Movimiento> lstTipoTran = CuentaService.ListarTipoTran(ConvertirStringToInt(id), (Usuario)Session["Usuario"]);
        ddlTipoTran.DataSource = lstTipoTran;
        ddlTipoTran.DataValueField = "tipo_tran";
        ddlTipoTran.DataTextField = "descripcion";
        ddlTipoTran.DataBind();
        ddlTipoTran_TextChanged(ddlTipoTran, null);
        txtTipoCuenta.Text = gvLista.SelectedRow.Cells[22].Text;
        txtTipoCuenta.Text = (txtTipoCuenta.Text == "R" ? "C" : txtTipoCuenta.Text);
        txtValorNuevo.Text = txtValor.Text;
    }

    protected void btnCerrarAjuste_Click(object sender, EventArgs e)
    {
        mpeNuevoAjuste.Hide();
    }

    protected void btGrabarAjuste_Click(object sender, EventArgs e)
    {
        VerError("");
        if (txtValorNuevo.Text.Trim() == "")
        {
            VerError("Debe especificar el valor");
            return;
        }
        string error = "";
        try
        {
            CuentaService.AjustarMovimiento(convenio, txtTipoCuenta.Text, ConvertirStringToInt(txtNumTranTarjeta.Text), ConvertirStringToInt32(ddlTipoTran.SelectedItem.Value), ConvertirStringToDecimal(txtValorNuevo.Text), (Usuario)Session["Usuario"], ref error);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return;
        }
        if (error.Trim() == "")
        {
            mpeNuevoAjuste.Hide();
            Actualizar();
        }
        else
        {
            VerError("No se pudo realizar el ajuste. Error:" + error);
        }
    }


    protected void ddlTipoTran_TextChanged(object sender, EventArgs e)
    {
        if (sender != null)
        {
            DropDownList _ddlTipoTran = (DropDownList)sender;
            decimal _valor = CuentaService.ConsultarValor(ConvertirStringToInt(txtNumTranTarjeta.Text), ConvertirStringToInt32(_ddlTipoTran.SelectedItem.Value), (Usuario)Session["Usuario"]);
            txtValor.Text = _valor.ToString("##,#");
            txtValorNuevo.Text = txtValor.Text;
        }
        else
        {
            VerError("No se pudo determinar datos del tipo de transacciòn");
        }
    }
    private int TipoConvenioTarjeta()
    {
        return 1;
    }

    private string NomTipoConvenioTarjeta()
    {
        return "";
    }


}