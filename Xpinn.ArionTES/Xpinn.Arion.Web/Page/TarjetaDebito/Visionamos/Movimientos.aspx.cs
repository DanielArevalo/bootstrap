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
using System.Net;
using System.Reflection;
using Xpinn.Util;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Services;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;


partial class Lista : GlobalWeb
{    
    CuentaService CuentaService = new CuentaService();
    InterfazCOOPCENTRAL interfazCoopcentral;
    string convenio = "";
    int tipoOpe = 124;
    bool bValidar = true;
    int _tipo_convenio = 1;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(CuentaService.CodigoProgramaMovimientoCoopcentral, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoExportar += btnExportar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoImportar += btnImportar_Click;            
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            Activar(1);
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            toolBar.MostrarGuardar(false);
            panelProceso.Visible = false;
            panelCarga.Visible = true;
            interfazCoopcentral = new InterfazCOOPCENTRAL("0123456789ABCDEFFEDCBA9876543210", "00000000000000000000000000000000");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimientoCoopcentral, "Page_PreInit", ex);
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
            txtConvenio.Text = ConvenioTarjeta(_tipo_convenio);
            txtIpAppliance.Text = IpApplianceConvenioTarjeta();
            if (!IsPostBack)
            {                
                txtFecha.Text = DateTime.Now.ToString(gFormatoFecha);
                txtFechaAplica.Text = DateTime.Now.ToString(gFormatoFecha);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimientoCoopcentral, "Page_Load", ex);
        }
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            if (Session["DTCUENTAS"] != null)
            {
                List<MovimientoCoopcentral> lstConsulta = (List<MovimientoCoopcentral>)Session["DTCUENTAS"];
                gvLista.DataSource = lstConsulta;
                gvLista.DataBind();
                return;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimientoCoopcentral, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Método para consultar los movimientos
    /// </summary>
    private void Actualizar()
    {
        VerError("");
        try
        {
            List<InterfazCOOPCENTRAL.archivoSIC> lstArchivo = new List<InterfazCOOPCENTRAL.archivoSIC>();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Convertir la lista para poderla mostrar
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // ===>>>> Aqui faltaria colocar consumo del WEBSERVICES
            List<MovimientoCoopcentral> lstConsulta = new List<MovimientoCoopcentral>(); // TrasladarList(lstArchivo, txtFecha.Text);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Cargar lista en la gridview
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;           
            if (lstConsulta.Count > 0)
            {                
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
            Session.Add(CuentaService.CodigoProgramaMovimientoCoopcentral + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimientoCoopcentral, "Actualizar", ex);
        }
    }


   /// <summary>
   /// Convertir la fecha del formato en que llega el archivo  un datatime
   /// </summary>
   /// <param name="pfecha"></param>
   /// <returns></returns>
    protected DateTime? ConvertirFechaTarjeta(string pfecha)
    {
        try
        {
            pfecha = pfecha.Replace("-", "");
            return new DateTime(Convert.ToInt32(pfecha.Substring(0, 4)), Convert.ToInt32(pfecha.Substring(4, 2)), Convert.ToInt32(pfecha.Substring(6, 2)));
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
                    List<MovimientoCoopcentral> lstConsulta = (List<MovimientoCoopcentral>)Session["DTCUENTAS"];
                    foreach (MovimientoCoopcentral item in lstConsulta)
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
            List<MovimientoCoopcentral> lstConsulta = (List<MovimientoCoopcentral>)Session["DTCUENTAS"];
            if (lstConsulta.Count > 0)
            {
                Error = CuentaService.ValidarArchivoCargaCoopcentral(_tipo_convenio, convenio, lstConsulta, bValidar, (Usuario)Session["Usuario"]);
                if (Error.Trim() != "")
                {
                    VerError(Error);
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
                    if (!CuentaService.AplicarMovimientosCoopcentral(convenio, fecha, lstConsulta, (Usuario)Session["Usuario"], ref Error, ref pCodOpe, 1))
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
        catch (Exception ex)
        {
            BOexcepcion.Throw(CuentaService.CodigoProgramaMovimientoCoopcentral, "btnContinuarMen_Click", ex);
        }
    }

    /// <summary>
    /// Determinar el formato de fecha que maneja financial
    /// </summary>
    /// <returns></returns>
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
        // Cargar el Archivo      
        if (FileUploadMetas.HasFile)
        {
            List<MovimientoCoopcentral> lstConsulta = new List<MovimientoCoopcentral>();
            Stream stream = FileUploadMetas.FileContent;
            string[] data;
            int linea = 0;
            using (StreamReader strReader = new StreamReader(stream))
            {
                data = new string[stream.Length];
                while (strReader.Peek() >= 0)
                {
                    string readLine = strReader.ReadLine();
                    data[linea] = readLine;
                    linea += 1;
                }
            }
            lstConsulta = CuentaService.CargarArchivoMovCoopcentral(data, gSeparadorMiles, gSeparadorDecimal);            
            gvLista.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            if (lstConsulta.Count > 0)
            {
                // Cargar listado de cajas
                List<Datafono> lstDatafonos = new List<Datafono>();
                lstDatafonos = CuentaService.ListarDatafono((Usuario)Session["Usuario"]);
                foreach (MovimientoCoopcentral item in lstConsulta)
                {
                    Movimiento emov = new Movimiento();
                    string fecha = item.fecha_contable;
                    decimal monto = item.valor;
                    // Determinar la cuenta
                    string _cuenta = item.cuenta_origen;
                    if (item.escenario == "2" || item.escenario == "6" || item.escenario == "8" || item.escenario == "10")
                    {
                        // 2=Transferencias Intercooperativas (cuenta origen y destino de la entidad)
                        // 6=Transferencia del asociado visionamos con destino a asociado entidad en dispositivo de la entiad
                        // 8=Transferencia del asociado con destino a asociado entidad en dispositivo visionamos
                        //10=Transferencia del asociado visionamos con destino a asocaido entidad en dispositivo visionamos
                        _cuenta = item.cuenta_destino;
                    }
                    // Determinar el tipo de transaccion
                    string _transaccion = item.transaccion;
                    if (item.escenario == "5")
                        // Transferencias Intercooperativas. Escenario 5 = Transferencia del asociado en dispositivo de la entidad con destino a visionamos
                        _transaccion = "01" + item.transaccion.Substring(2, 1);
                    if (item.escenario == "10")
                        // Transferencias Intercooperativas. Escenario 5 = Transferencia del asociado en dispositivo de la entidad con destino a visionamos
                        _transaccion = "21" + item.transaccion.Substring(2, 1);
                    // Determinar el tipo de transaccion
                    string tipoCuenta = "", _error = "";
                    CuentaService cuentaService = new CuentaService();
                    cuentaService.ValidarCuentaCoopcentral(txtConvenio.Text, _cuenta, ref tipoCuenta, ref _error, (Usuario)Session["Usuario"]);
                    item.tipo_tran = cuentaService.HomologaTipoTran(tipoCuenta, _transaccion, 1);
                    // Validar si la transaccion ya existe en tran_tarjeta
                    emov = CuentaService.ConsultarMovimientoCoopcentral(_tipo_convenio, item.tarjeta, item.secuencia, _transaccion, item.secuencia, fecha, monto, (Usuario)Session["Usuario"]);
                    if (emov != null)
                    { 
                        item.num_tran = emov.num_tran;
                        // Determinar datos de aplicación del MONTO 
                        _error = "";
                        Movimiento eAplicacion = new Movimiento();
                        DateTime? fechatran = ConvertirFechaTarjeta(item.fecha_contable);
                        eAplicacion = CuentaService.DatosDeAplicacion(emov.num_tran, _cuenta, emov.cod_ope, emov.cod_cliente, Convert.ToDateTime(fechatran), item.valor, item.tipo_tran, item.secuencia, ref _error, (Usuario)Session["Usuario"]);
                        if (eAplicacion != null)
                        {
                            item.cod_ope_apl = eAplicacion.cod_ope_apl;
                            item.valor_apl = eAplicacion.valor_apl;
                            item.num_comp_apl = eAplicacion.num_comp_apl;
                            item.tipo_comp_apl = eAplicacion.tipo_comp_apl;
                            item.cuenta_porcobrar = eAplicacion.cuenta_porcobrar;
                        }
                        // Determinar datos de aplicación de la COMISION
                        _error = "";
                        Movimiento eAplicacionCom = new Movimiento();
                        int? tipo_tranCom = cuentaService.TipoTranComision(tipoCuenta, item.tipo_tran);
                        eAplicacionCom = CuentaService.DatosDeAplicacion(emov.num_tran, _cuenta, emov.cod_ope, emov.cod_cliente, Convert.ToDateTime(fechatran), item.valor, tipo_tranCom, item.secuencia, ref _error, (Usuario)Session["Usuario"]);
                        if (eAplicacion != null)
                        {
                            item.comision_apl = eAplicacionCom.valor_apl;
                        }
                    }
                    // Determinar si es DATAFONO para saber de que oficina y caja fue realizado.
                    if (lstDatafonos != null)
                        if (lstDatafonos.Count > 0)
                        {
                            if (item.tipo_terminal == "41")
                            { 
                                var query = (from p in lstDatafonos where item.codigo_terminal.Contains(p.cod_datafono) select p).FirstOrDefault();
                                if (query != null)
                                { 
                                    if ((Datafono)query != null)
                                    { 
                                        item.cod_caja = ((Datafono)query).cod_caja;
                                        item.cod_ofi = ((Datafono)query).cod_oficina;
                                    }
                                }
                            }
                        }
                }
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
            Session.Add(CuentaService.CodigoProgramaMovimientoCoopcentral + ".consulta", 1);
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
            string fecha = e.Row.Cells[8].Text.ToString();
            string documento = e.Row.Cells[15].Text.ToString();
            string cuenta = e.Row.Cells[2].Text.ToString();
            string tarjeta = e.Row.Cells[1].Text.ToString();                                    
            string tipotransaccion = e.Row.Cells[9].Text.ToString();
            string operacion = e.Row.Cells[15].Text.ToString();
            string red = e.Row.Cells[17].Text.ToString().Trim();
            decimal monto = ConvertirStringToDecimal(e.Row.Cells[12].Text);
            decimal comision = ConvertirStringToDecimal(e.Row.Cells[13].Text);
            decimal valor_apl = ConvertirStringToDecimal(e.Row.Cells[24].Text);
            decimal comision_apl = ConvertirStringToDecimal(e.Row.Cells[26].Text);
            Int64? num_tran_tarjeta = ConvertirStringToIntN(e.Row.Cells[23].Text);
            if (bValidar)
            {                 
                if (num_tran_tarjeta != null && num_tran_tarjeta != 0)
                {
                    e.Row.ForeColor = System.Drawing.Color.DarkGreen;
                }
                else
                {
                    e.Row.ForeColor = System.Drawing.Color.IndianRed;
                }
                // Si es consulta monto es cero
                if (tipotransaccion.Substring(0, 2) == "30" || tipotransaccion.Substring(0, 2) == "31" || tipotransaccion.Substring(0, 2) == "32" ||
                    tipotransaccion.Substring(0, 2) == "35" || tipotransaccion.Substring(0, 2) == "36" || tipotransaccion.Substring(0, 2) == "37" ||
                    tipotransaccion.Substring(0, 2) == "89")
                    monto = 0;
                // Determinar si fue aplicado el monto
                if (tipotransaccion != "" && (monto != 0 || valor_apl != 0))
                { 
                    if (valor_apl == Math.Abs(monto))
                    {
                        Image imgAplicada = (Image)e.Row.FindControl("imgAplicada");
                        if (imgAplicada != null)
                            imgAplicada.Visible = true;
                    }
                    else
                    {
                        Image imgNoAplicada = (Image)e.Row.FindControl("imgNoAplicada");
                        if (imgNoAplicada != null)
                            imgNoAplicada.Visible = true;
                    }
                }
                // Determinar si fue aplicada la comisión
                if (tipotransaccion != "")
                {
                    if (comision_apl == Math.Abs(comision))
                    {
                        Image imgAplicadaCom = (Image)e.Row.FindControl("imgAplicadaCom");
                        if (imgAplicadaCom != null)
                            imgAplicadaCom.Visible = true;
                    }
                    else
                    {
                        Image imgNoAplicadaCom = (Image)e.Row.FindControl("imgNoAplicadaCom");
                        if (imgNoAplicadaCom != null)
                            imgNoAplicadaCom.Visible = true;
                    }
                }
            }
        }
    }


    /// <summary>
    /// Homologa los tipos de transacción entre FINANCIAL y COOPCENTRAL
    /// </summary>
    /// <param name="ptipocuenta"></param>
    /// <param name="tipotransaccion"></param>
    /// <param name="tipo_convenio"></param>
    /// <returns></returns>
    protected int? HomologaTipoTran(string ptipocuenta, string tipotransaccion, int? tipo_convenio)
    {   
        return CuentaService.HomologaTipoTran(ptipocuenta, tipotransaccion, tipo_convenio);
    }




}