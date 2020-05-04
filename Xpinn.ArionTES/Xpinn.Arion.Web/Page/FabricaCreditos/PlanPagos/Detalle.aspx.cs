using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Diagnostics;
using Cantidad_a_Letra;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Xpinn.Comun.Entities;
using System.Linq;
using System.Threading.Tasks;
using Xpinn.Servicios.Services;
using Xpinn.Servicios.Entities;
using iTextSharp.text.html;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;

public partial class Detalle : GlobalWeb
{
    #region Variables
    TiposDocumento vTipoDoc = new TiposDocumento();
    private TiposDocumentoService tipoDocumentoServicio = new TiposDocumentoService();
    private CreditoPlanService creditoPlanServicio = new CreditoPlanService();
    DatosDeDocumentoService datosDeDocumentoServicio = new DatosDeDocumentoService();
    List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();
    private CreditoService CreditoServicio = new CreditoService();
    ClasificacionCarteraService clasificacionCarteraService = new ClasificacionCarteraService();
    ClasificacionCartera clasificacionCartera = new ClasificacionCartera();
    IList<HistoricoTasa> lsHistoricoTasas = new List<HistoricoTasa>();
    HistoricoTasaService historicoTasaService = new HistoricoTasaService();
    ReliquidacionService reliquidacionService = new ReliquidacionService();


    string cDebug = "";
    string Estado = "";
    private string pError = "";
    string cDocsSubDir = "../GeneracionDocumentos/Documentos";
    Usuario _usuario;
    General valorParametro = new General();

    #endregion

    #region Metodos Inciales
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[creditoPlanServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(creditoPlanServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(creditoPlanServicio.CodigoPrograma, "A");
            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += (s, evt) =>
            {
                toolBar.MostrarGuardar(false);
                toolBar.MostrarRegresar(true);
                toolBar.MostrarCancelar(false);

                mvLista.ActiveViewIndex = 0;
                VerError("");
            };

            framesolicitud.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _usuario = (Usuario)Session["usuario"];

            framesolicitud.Visible = false;
            if (!IsPostBack)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarCancelar(false);

                framesolicitud.Visible = false;
                UpdatePanel1.Visible = false;
                Session["ValorMonto"] = null;
                Session["VrDesembolso"] = null;
                Session["NumAuxilio"] = null;
                mvLista.ActiveViewIndex = 0;
                if (Session[creditoPlanServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[creditoPlanServicio.CodigoPrograma + ".id"].ToString();
                    if (Session["NUM_AUXILIO"] == null)
                        ObtenerDatos(idObjeto, 0);
                    if (Request.UrlReferrer.Segments[4].ToString() == "Comprobante/")
                    {
                        if (Session["NumCred_Orden"] != null)
                        {
                            if (Session["NUM_AUXILIO"] == null)
                            {
                                Imprimir(1);
                                btnImprimirOrden.Visible = true;
                            }
                            else
                            {
                                Imprimir(2);
                                btnRegresarOrden.Visible = false;
                            }
                            Session.Remove("NumCred_Orden");
                            Session.Remove("NUM_AUXILIO");
                        }
                    }
                    else
                    {
                        btnImprimirOrden.Visible = false;
                        try
                        {
                            Credito vCredito = new Credito();
                            vCredito = CreditoServicio.ConsultarCredito(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
                            if (vCredito.estado == "C")
                            {
                                LineasCreditoService LineasCreditoServicio = new LineasCreditoService();
                                LineasCredito vLineasCredito = new LineasCredito();
                                vLineasCredito = LineasCreditoServicio.ConsultarLineasCredito(Convert.ToString(txtLinea.Text), (Usuario)Session["usuario"]);
                                if (vLineasCredito.orden_servicio == 1)
                                {
                                    btnImprimirOrden.Visible = true;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }

                    Session["NumCred_Orden"] = null;
                }

                if (Session[creditoPlanServicio.CodigoPrograma + ".origen"] != null)
                {
                    btnTalonario.Visible = false;
                    BtnPagare0.Visible = false;
                    Btnautorizacion0.Visible = false;
                }
                if (Session[creditoPlanServicio.CodigoPrograma + ".solicitud"] != null)
                {
                    Session["ocultarMenu"] = "1";
                    toolBar.MostrarRegresar(false);
                    btnInforme4.Visible = false;
                    btnInformeSolicitud_Click(null, null);
                    btnInformeConsumo_Click(null, null);
                }

                framesolicitud.Visible = false;
                String radicado = "";
                radicado = Request["radicado"];
                if (radicado != null)
                {
                    this.txtNumRadic.Text = radicado;
                    this.ObtenerDatos2(radicado);
                }

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    #endregion

    #region Eventos Botones y eventos GridView
    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        if (mvLista.ActiveViewIndex == 6 || mvLista.ActiveViewIndex == 7)
        {
            string rootCompleto = Server.MapPath("~/Page/FabricaCreditos/PlanPagos/Archivos");
            string nombreArchivo = @"\DocumentoAnexo_" + ((Usuario)Session["usuario"]).codperfil;
            DocumentosAnexo.EliminarDocumentos(rootCompleto, nombreArchivo);
            mvLista.ActiveViewIndex = 0;
            return;
        }

        if (Session[creditoPlanServicio.CodigoPrograma + ".origen"] != null)
            Response.Redirect(Session[creditoPlanServicio.CodigoPrograma + ".origen"].ToString());
        else
            Navegar(Pagina.Lista);
    }
    protected void btnExportar0_Click(object sender, EventArgs e)
    {
        try
        {
            if (gvPlanPagos0.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                Page pagina = new Page();
                dynamic form = new HtmlForm();
                gvPlanPagos0.EnableViewState = false;
                pagina.EnableEventValidation = false;
                pagina.DesignerInitialize();
                pagina.Controls.Add(form);
                form.Controls.Add(gvPlanPagos0);
                pagina.RenderControl(htw);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=PlanPagos.xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
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
    protected void txtNumRadic_TextChanged(object sender, EventArgs e)
    {

    }
    protected void gvPlanPagos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPlanPagos.PageIndex = e.NewPageIndex;
            List<DatosPlanPagos> lstConsulta = new List<DatosPlanPagos>();
            if (Session["PlanPagos"] != null)
            {
                lstConsulta = (List<DatosPlanPagos>)Session["PlanPagos"];
                gvPlanPagos.DataSource = lstConsulta;
                gvPlanPagos.DataBind();
                return;
            }
            Actualizar(txtNumRadic.Text);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "vPlanPagos_PageIndexChanging", ex);
        }
    }
    protected void btnCloseDocAnexo_Click(object sender, EventArgs e)
    {
        if (txtNumRadic.Text.Trim() == "")
            return;
        mvLista.ActiveViewIndex = 7;
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        DocumentosAnexo.TablaDocumentosAnexo(txtNumRadic.Text, 2);
    }
    /// <summary>
    /// Método del botón para ir a imprimir el talonario del crédito
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnTalonario_Click(object sender, EventArgs e)
    {
        generardocumento();
    }

    static public DataControlFieldCell GetCellByName(GridViewRow Row, String CellName)
    {
        foreach (DataControlFieldCell Cell in Row.Cells)
        {
            if (Cell.ContainingField.ToString().ToUpper() == CellName.ToUpper())
                return Cell;
        }
        return null;
    }
    protected void BtnPagare_Click(object sender, EventArgs e)
    {


        string ruta = Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/Documentos/");

        if (Directory.Exists(ruta))
        {
            String fileName = txtNumRadic.Text + '_' + txttipodoc.Text + '.' + 'p' + 'd' + 'f';
            string savePath = ruta + fileName;

            FileInfo fi = new FileInfo(savePath);
            bool exists = fi.Exists;
            if (exists == false)
            {
                String Error = "No se encuentra pagaré generado";
                this.LblError.Text = Error;
            }
            else
            {
                FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                FileInfo file = new FileInfo(savePath);
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.TransmitFile(file.FullName);
                Response.End();
            }

        }

    }
    protected void Btnautorizacion_Click(object sender, EventArgs e)
    {
        string ruta = Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/Documentos/");

        if (Directory.Exists(ruta))
        {
            String fileName = txtNumRadic.Text + '_' + '2' + '5' + '.' + 'p' + 'd' + 'f';
            string savePath = ruta + fileName;

            FileInfo fi = new FileInfo(savePath);
            bool exists = fi.Exists;
            if (exists == false)
            {
                fileName = txtNumRadic.Text + '_' + '3' + '.' + 'p' + 'd' + 'f';
                savePath = ruta + fileName;
                fi = new FileInfo(savePath);
                exists = fi.Exists;
            }

            if (exists == false)
            {
                String Error = "No se encuentra carta de instrucciones generada";
                this.LblError.Text = Error;
            }
            else
            {
                FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                FileInfo file = new FileInfo(savePath);
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.TransmitFile(file.FullName);
                Response.End();
            }

        }
    }
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            ObtenerDatos(idObjeto, 0);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        Boolean bGenerado = false;
        Session.Remove("Generado");
        bGenerado = true;
        if (bGenerado == true)
        {
            Session["Generado"] = true;
            ObtenerDatos(txtNumRadic.Text, 0);

            Response.Write("<script>window.__doPostBack('','');</script>");
            lblInfo.Text = "Los DocumentosAnexo del Credito fueron generados, dar click en el icono de impresora de cada documento para visualizarlo";

            gvLista2.Visible = true;

            generardocumento();
        }
    }
    protected void btnPlanPagosOriginal_Click(object sender, EventArgs e)
    {
        idObjeto = Session[creditoPlanServicio.CodigoPrograma + ".id"].ToString();
        if (btnPlanPagosOriginal.Text == "Plan Pagos Actual")
        {
            ObtenerDatos(idObjeto, 0);
        }
        else
        {
            ObtenerDatos(idObjeto, 1);
        }
    }
    protected void btnDocumentosAnexo_Click(object sender, EventArgs e)
    {
        if (txtNumRadic.Text.Trim() == "")
            return;
        mvLista.ActiveViewIndex = 7;
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        DocumentosAnexo.TablaDocumentosAnexo(txtNumRadic.Text, 2, 0);
    }
    protected void btnVerSubirPagare_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarRegresar(false);
        toolBar.MostrarCancelar(true);

        DocumentosAnexosService docService = new DocumentosAnexosService();
        string filtro = string.Format("WHERE numero_radicacion = {0} and tipo_documento = 1 and estado = 1", txtNumRadic.Text);

        DocumentosAnexos documento = docService.ConsultarDocumentosAnexosConFiltro(filtro, _usuario);

        if (documento != null && documento.imagen != null)
        {
            MostrarArchivoEnLiteral(documento.imagen);
            lblIDImagen.Text = documento.iddocumento.ToString();
        }

        VerError("");
        lblNotificacionGuardado.Text = string.Empty;
        lblNotificacionGuardado.Visible = false;

        mvLista.ActiveViewIndex = 5;
    }
    protected void btnPrevisualizar_Click(object sender, EventArgs e)
    {
        PrevisualizarArchivo();
    }
    protected void btnAtributosCredito_Click(object sender, EventArgs e)
    {
        ListarAtributos(Convert.ToInt64(txtNumRadic.Text));
    }
    #endregion

    #region Metodos Externos
    protected void EliminarInstanciasDeLibreOffice()
    {
        /* Por el error en libreoffice (https://bugs.freedesktop.org/show_bug.cgi?id=37531) */
        while (Process.GetProcessesByName("soffice.bin").Length > 0)
        {
            Process[] procs = Process.GetProcessesByName("soffice.bin");
            foreach (Process p in procs) { p.Kill(); }
            System.Threading.Thread.Sleep(1000);
        }
        /* Por el error en libreoffice */
    }
    public void TablaPlanPagosOri(String pIdObjeto)
    {
        try
        {
            Int32 anchocolumna = 110;
            Int32 longitud = 0;

            Credito datosApp = new Credito();
            datosApp.numero_radicacion = Int64.Parse(pIdObjeto);
            DatosPlanPagosService datosServicio = new DatosPlanPagosService();
            List<DatosPlanPagos> lstConsulta = new List<DatosPlanPagos>();
            lstConsulta = datosServicio.ListarDatosPlanPagosOriginal(datosApp, Usuario);
            Session["PlanPagos"] = lstConsulta;

            gvPlanPagos.DataSource = lstConsulta;
            gvPlanPagos0.DataSource = lstConsulta;

            // Ajustar informaciòn de la grila para mostrar en pantalla
            if (lstConsulta.Count > 0)
            {
                // Mostrar los descuentos
                if (lstConsulta != null)
                {
                    if (lstConsulta[0] != null)
                    {
                        if (lstConsulta[0].lstSumados != null)
                        {
                            if (lstConsulta[0].lstSumados.Count >= 1)
                            {
                                Configuracion conf = new Configuracion();
                                decimal vrMonto = 0;
                                string sMonto = txtMonto.Text.Replace("$", "").Replace(gSeparadorMiles, "");
                                sMonto = sMonto.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, conf.ObtenerSeparadorDecimalConfig());
                                vrMonto = Convert.ToDecimal(sMonto);
                                decimal vrSumados = 0;
                                foreach (DescuentosCredito drFila in lstConsulta[0].lstSumados)
                                {
                                    vrSumados = vrSumados + Convert.ToDecimal(drFila.val_atr);
                                }
                                vrMonto = vrMonto + vrSumados;
                                Session["ValorMonto"] = vrMonto;
                                txtMontoCalculado.Text = vrMonto.ToString("C");
                            }
                            for (int i = lstConsulta[0].lstSumados.Count - 1; i <= 2; i++)
                            {
                                DescuentosCredito descCre = new DescuentosCredito();
                                lstConsulta[0].lstSumados.Add(descCre);
                            }
                            lbSumados.DataSource = lstConsulta[0].lstSumados;
                            lbSumados.DataBind();
                        }
                        if (lstConsulta[0].lstDescuentos != null)
                        {
                            if (lstConsulta[0].lstDescuentos.Count >= 1)
                            {
                                Configuracion conf = new Configuracion();
                                decimal vrMonto = 0;
                                string sMonto = txtMontoCalculado.Text.Replace("$", "").Replace(gSeparadorMiles, "");
                                sMonto = sMonto.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, conf.ObtenerSeparadorDecimalConfig());
                                vrMonto = Convert.ToDecimal(sMonto);
                                decimal vrDescontados = 0;
                                foreach (DescuentosCredito drFila in lstConsulta[0].lstDescuentos)
                                {
                                    vrDescontados = vrDescontados + Convert.ToDecimal(drFila.val_atr);
                                }
                                vrMonto = vrMonto - vrDescontados;
                                txtVrDesembolsado.Text = vrMonto.ToString("C");
                                Session["VrDesembolso"] = vrMonto;
                            }
                            for (int i = lstConsulta[0].lstDescuentos.Count - 1; i <= 2; i++)
                            {
                                DescuentosCredito descCre = new DescuentosCredito();
                                lstConsulta[0].lstDescuentos.Add(descCre);

                            }
                            lbDescontados.DataSource = lstConsulta[0].lstDescuentos;
                            Session["DescuentosPlan"] = lstConsulta[0].lstDescuentos;
                            lbDescontados.DataBind();
                        }
                    }

                    CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
                    List<CreditoRecoger> lstConsultas = new List<CreditoRecoger>();
                    CreditoRecoger refe = new CreditoRecoger();
                    lstConsultas = creditoRecogerServicio.ConsultarCreditoRecoger(Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["usuario"]);

                    if (lstConsultas.Count >= 1)
                    {

                        foreach (CreditoRecoger drFila in lstConsultas)
                        {
                            Configuracion conf = new Configuracion();
                            decimal vrMonto = 0;
                            string sMonto = txtVrDesembolsado.Text.Replace("$", "").Replace(gSeparadorMiles, "");
                            sMonto = sMonto.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, conf.ObtenerSeparadorDecimalConfig());
                            vrMonto = Convert.ToDecimal(sMonto);
                            decimal vrRecogidos = 0;

                            decimal intcorriente = 0, intmora = 0, intotros = 0;
                            Credito referea = new Credito();
                            if (drFila.numero_radicacion.ToString() != "")
                            {
                                CreditoService creditoservicio = new CreditoService();
                                try
                                {
                                    referea = creditoservicio.consultarinterescredito(drFila.numero_radicacion, DateTime.Now, (Usuario)Session["usuario"]);
                                    intcorriente = referea.intcoriente;
                                    intmora = referea.interesmora;
                                    intotros = referea.otros;
                                }
                                catch
                                {
                                    intcorriente = 0;
                                    intmora = 0;
                                    intotros = 0;
                                }
                            }

                            drFila.saldo_capital = drFila.valor_recoge - (intcorriente + intmora + intotros);
                            drFila.valor_recoge = drFila.saldo_capital + intcorriente + intmora + intotros;
                            if (drFila.valor_recoge < drFila.valor_nominas)
                            {
                                drFila.valor_recoge = drFila.valor_nominas;
                            }
                            drFila.valor_recoge -= drFila.valor_nominas;

                            vrRecogidos = vrRecogidos + Convert.ToDecimal(drFila.valor_recoge);
                            vrMonto = vrMonto - vrRecogidos;
                            txtVrDesembolsado.Text = vrMonto.ToString("C");
                            Session["VrDesembolso"] = vrMonto;
                        }
                    }


                    SolicitudCredServRecogidosService serviciosRecogidosServices = new SolicitudCredServRecogidosService();
                    List<SolicitudCredServRecogidos> listaServicios = serviciosRecogidosServices.ListarSolicitudCredServRecogidosActualizado(Convert.ToInt64(txtNumRadic.Text), Usuario);

                    if (listaServicios.Count >= 1)
                    {

                        Configuracion conf = new Configuracion();
                        decimal vrMonto = 0;
                        string sMonto = txtVrDesembolsado.Text.Replace("$", "").Replace(gSeparadorMiles, "");
                        sMonto = sMonto.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, conf.ObtenerSeparadorDecimalConfig());
                        vrMonto = Convert.ToDecimal(sMonto);
                        decimal vrRecogidos = 0;
                        foreach (SolicitudCredServRecogidos drFila in listaServicios)
                        {

                            CreditoRecoger List = new CreditoRecoger();
                            List.valor_recoge = drFila.valorrecoger;
                            List.valor_total = drFila.valorrecoger; ;
                            List.saldo_capital = drFila.valor_cuota;
                            List.numero_radicacion = drFila.numeroservicio;
                            List.linea_credito = drFila.nom_linea;

                            vrRecogidos = vrRecogidos + Convert.ToDecimal(drFila.valorrecoger);
                            lstConsultas.Add(List);
                        }
                        vrMonto = vrMonto - vrRecogidos;
                        txtVrDesembolsado.Text = vrMonto.ToString("C");
                        Session["VrDesembolso"] = vrMonto;
                    }
                    dtListRecogidos.DataSource = lstConsultas;
                    dtListRecogidos.DataBind();

                }
                // Mostrando la grilla                
                gvPlanPagos.Visible = true;
                gvPlanPagos.DataBind();
                gvPlanPagos.Columns[1].ItemStyle.Width = 90;
                // Ocultando las columnas que no deben mostrarse
                List<Atributos> lstAtr = new List<Atributos>();
                lstAtr = datosServicio.GenerarAtributosPlan((Usuario)Session["usuario"]);
                Session["AtributosPlanPagos"] = lstAtr;
                for (int i = 4; i <= 18; i++)
                {
                    gvPlanPagos.Columns[i].Visible = false;
                    int j = 0;
                    foreach (Atributos item in lstAtr)
                    {
                        if (j == i - 4)
                            gvPlanPagos.Columns[i].HeaderText = item.nom_atr;
                        j = j + 1;
                    }
                }
                // Establecer el ancho de las columnas de valores
                for (int i = 2; i < 20; i++)
                {
                    gvPlanPagos.Columns[i].ItemStyle.Width = anchocolumna;
                }
                // Ajustando el tamaño de la grilla
                longitud = 0;
                for (int i = 0; i < 20; i++)
                {
                    if (gvPlanPagos.Columns[i].Visible == true)
                        longitud = longitud + Convert.ToInt32(gvPlanPagos.Columns[i].ItemStyle.Width.Value);
                }
                if (longitud + anchocolumna > 900)
                {
                    gvPlanPagos.Width = longitud + anchocolumna;
                }
                else
                {
                    gvPlanPagos.Width = 900;
                }
                if (lstConsulta.Count < 10)
                    gvPlanPagos.Height = lstConsulta.Count * 30;
                // Mostrando las columnas que tienen valores
                foreach (DatosPlanPagos ItemPlanPagos in lstConsulta)
                {
                    if (ItemPlanPagos.int_1 != 0) { gvPlanPagos.Columns[4].Visible = true; }
                    if (ItemPlanPagos.int_2 != 0) { gvPlanPagos.Columns[5].Visible = true; }
                    if (ItemPlanPagos.int_3 != 0) { gvPlanPagos.Columns[6].Visible = true; }
                    if (ItemPlanPagos.int_4 != 0) { gvPlanPagos.Columns[7].Visible = true; }
                    if (ItemPlanPagos.int_5 != 0) { gvPlanPagos.Columns[8].Visible = true; }
                    if (ItemPlanPagos.int_6 != 0) { gvPlanPagos.Columns[9].Visible = true; }
                    if (ItemPlanPagos.int_7 != 0) { gvPlanPagos.Columns[10].Visible = true; }
                    if (ItemPlanPagos.int_8 != 0) { gvPlanPagos.Columns[11].Visible = true; }
                    if (ItemPlanPagos.int_9 != 0) { gvPlanPagos.Columns[12].Visible = true; }
                    if (ItemPlanPagos.int_10 != 0) { gvPlanPagos.Columns[13].Visible = true; }
                    if (ItemPlanPagos.int_11 != 0) { gvPlanPagos.Columns[14].Visible = true; }
                    if (ItemPlanPagos.int_12 != 0) { gvPlanPagos.Columns[15].Visible = true; }
                    if (ItemPlanPagos.int_13 != 0) { gvPlanPagos.Columns[16].Visible = true; }
                    if (ItemPlanPagos.int_14 != 0) { gvPlanPagos.Columns[17].Visible = true; }
                    if (ItemPlanPagos.int_15 != 0) { gvPlanPagos.Columns[18].Visible = true; }
                    // Mostrar fecha de primer pago
                    if (Txtprimerpago.Text.Trim() == "" && ItemPlanPagos.fechacuota != null && ItemPlanPagos.numerocuota == 1)
                        Txtprimerpago.Text = Convert.ToDateTime(ItemPlanPagos.fechacuota).ToString(gFormatoFecha);
                }
                gvPlanPagos.DataBind();
            }
            else
            {
                gvPlanPagos.Visible = true;
            }

            // Ajustar valores para la grilla que se usa para descargar los datos a excel.
            if (lstConsulta.Count > 0)
            {
                gvPlanPagos0.Visible = true;
                gvPlanPagos0.DataBind();
                gvPlanPagos0.Columns[1].ItemStyle.Width = 90;
                // Ocultando las columnas que no deben mostrarse
                List<Atributos> lstAtr = new List<Atributos>();
                lstAtr = datosServicio.GenerarAtributosPlan((Usuario)Session["usuario"]);
                for (int i = 4; i <= 18; i++)
                {
                    gvPlanPagos0.Columns[i].Visible = false;
                    int j = 0;
                    foreach (Atributos item in lstAtr)
                    {
                        if (j == i - 4)
                            gvPlanPagos0.Columns[i].HeaderText = item.nom_atr;
                        j = j + 1;
                    }
                }
                // Establecer el ancho de las columnas de valores
                for (int i = 2; i < 20; i++)
                {
                    gvPlanPagos0.Columns[i].ItemStyle.Width = anchocolumna;
                }
                // Ajustando el tamaño de la grilla
                longitud = 0;
                for (int i = 0; i < 20; i++)
                {
                    longitud = longitud + Convert.ToInt32(gvPlanPagos0.Columns[i].ItemStyle.Width.Value);
                }
                gvPlanPagos0.Width = longitud / 2;
                foreach (DatosPlanPagos ItemPlanPagos in lstConsulta)
                {
                    if (ItemPlanPagos.int_1 != 0) { gvPlanPagos0.Columns[4].Visible = true; }
                    if (ItemPlanPagos.int_2 != 0) { gvPlanPagos0.Columns[5].Visible = true; }
                    if (ItemPlanPagos.int_3 != 0) { gvPlanPagos0.Columns[6].Visible = true; }
                    if (ItemPlanPagos.int_4 != 0) { gvPlanPagos0.Columns[7].Visible = true; }
                    if (ItemPlanPagos.int_5 != 0) { gvPlanPagos0.Columns[8].Visible = true; }
                    if (ItemPlanPagos.int_6 != 0) { gvPlanPagos0.Columns[9].Visible = true; }
                    if (ItemPlanPagos.int_7 != 0) { gvPlanPagos0.Columns[10].Visible = true; }
                    if (ItemPlanPagos.int_8 != 0) { gvPlanPagos0.Columns[11].Visible = true; }
                    if (ItemPlanPagos.int_9 != 0) { gvPlanPagos0.Columns[12].Visible = true; }
                    if (ItemPlanPagos.int_10 != 0) { gvPlanPagos0.Columns[13].Visible = true; }
                    if (ItemPlanPagos.int_11 != 0) { gvPlanPagos0.Columns[14].Visible = true; }
                    if (ItemPlanPagos.int_12 != 0) { gvPlanPagos0.Columns[15].Visible = true; }
                    if (ItemPlanPagos.int_13 != 0) { gvPlanPagos0.Columns[16].Visible = true; }
                    if (ItemPlanPagos.int_14 != 0) { gvPlanPagos0.Columns[17].Visible = true; }
                    if (ItemPlanPagos.int_15 != 0) { gvPlanPagos0.Columns[18].Visible = true; }
                }
                gvPlanPagos0.DataBind();
            }
            else
            {
                gvPlanPagos0.Visible = false;
            }
            btnPlanPagosOriginal.Text = @"Plan Pagos Actual";
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    public void TablaPlanPagos(String pIdObjeto)
    {
        try
        {
            Int32 anchocolumna = 110;
            Int32 longitud = 0;

            Credito datosApp = new Credito();
            datosApp.numero_radicacion = Int64.Parse(pIdObjeto);
            DatosPlanPagosService datosServicio = new DatosPlanPagosService();
            List<DatosPlanPagos> lstConsulta = new List<DatosPlanPagos>();
            lstConsulta = datosServicio.ListarDatosPlanPagos(datosApp, Usuario);
            Session["PlanPagos"] = lstConsulta;

            gvPlanPagos.DataSource = lstConsulta;
            gvPlanPagos0.DataSource = lstConsulta;

            // Ajustar informaciòn de la grila para mostrar en pantalla
            if (lstConsulta.Count > 0)
            {
                // Mostrar los descuentos
                if (lstConsulta != null)
                {
                    if (lstConsulta[0] != null)
                    {
                        if (lstConsulta[0].lstSumados != null)
                        {
                            if (lstConsulta[0].lstSumados.Count >= 1)
                            {
                                Configuracion conf = new Configuracion();
                                decimal vrMonto = 0;
                                string sMonto = txtMonto.Text.Replace("$", "").Replace(gSeparadorMiles, "");
                                sMonto = sMonto.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, conf.ObtenerSeparadorDecimalConfig());
                                vrMonto = Convert.ToDecimal(sMonto);
                                decimal vrSumados = 0;
                                foreach (DescuentosCredito drFila in lstConsulta[0].lstSumados)
                                {
                                    vrSumados = vrSumados + Convert.ToDecimal(drFila.val_atr);
                                }
                                vrMonto = vrMonto + vrSumados;
                                Session["ValorMonto"] = vrMonto;
                                txtMontoCalculado.Text = vrMonto.ToString("C");
                            }
                            for (int i = lstConsulta[0].lstSumados.Count - 1; i <= 2; i++)
                            {
                                DescuentosCredito descCre = new DescuentosCredito();
                                lstConsulta[0].lstSumados.Add(descCre);
                            }
                            lbSumados.DataSource = lstConsulta[0].lstSumados;
                            lbSumados.DataBind();
                        }
                        if (lstConsulta[0].lstDescuentos != null)
                        {
                            if (lstConsulta[0].lstDescuentos.Count >= 1)
                            {
                                Configuracion conf = new Configuracion();
                                decimal vrMonto = 0;
                                string sMonto = txtMontoCalculado.Text.Replace("$", "").Replace(gSeparadorMiles, "");
                                sMonto = sMonto.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, conf.ObtenerSeparadorDecimalConfig());
                                vrMonto = Convert.ToDecimal(sMonto);
                                decimal vrDescontados = 0;
                                foreach (DescuentosCredito drFila in lstConsulta[0].lstDescuentos)
                                {
                                    vrDescontados = vrDescontados + Convert.ToDecimal(drFila.val_atr);
                                }
                                vrMonto = vrMonto - vrDescontados;
                                txtVrDesembolsado.Text = vrMonto.ToString("C");
                                Session["VrDesembolso"] = vrMonto;
                            }
                            for (int i = lstConsulta[0].lstDescuentos.Count - 1; i <= 2; i++)
                            {
                                DescuentosCredito descCre = new DescuentosCredito();
                                lstConsulta[0].lstDescuentos.Add(descCre);

                            }
                            lbDescontados.DataSource = lstConsulta[0].lstDescuentos;
                            Session["DescuentosPlan"] = lstConsulta[0].lstDescuentos;
                            lbDescontados.DataBind();
                        }
                    }


                    CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
                    List<CreditoRecoger> lstConsultas = new List<CreditoRecoger>();
                    CreditoRecoger refe = new CreditoRecoger();
                    lstConsultas = creditoRecogerServicio.ConsultarCreditoRecoger(Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["usuario"]);

                    if (lstConsultas.Count >= 1)
                    {

                        foreach (CreditoRecoger drFila in lstConsultas)
                        {
                            Configuracion conf = new Configuracion();
                            decimal vrMonto = 0;
                            string sMonto = txtVrDesembolsado.Text.Replace("$", "").Replace(gSeparadorMiles, "");
                            sMonto = sMonto.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, conf.ObtenerSeparadorDecimalConfig());
                            vrMonto = Convert.ToDecimal(sMonto);
                            decimal vrRecogidos = 0;

                            decimal intcorriente = 0, intmora = 0, intotros = 0;
                            Credito referea = new Credito();
                            if (drFila.numero_radicacion.ToString() != "" && drFila.numero_radicacion != Int64.MinValue)
                            {
                                CreditoService creditoservicio = new CreditoService();
                                try
                                {
                                    referea = creditoservicio.consultarinterescredito(drFila.numero_radicacion, DateTime.Now, (Usuario)Session["usuario"]);
                                    intcorriente = referea.intcoriente;
                                    intmora = referea.interesmora;
                                    intotros = referea.otros;
                                }
                                catch
                                {
                                    intcorriente = 0;
                                    intmora = 0;
                                    intotros = 0;
                                }
                            }
                            drFila.saldo_capital = drFila.valor_recoge - (intcorriente + intmora + intotros);
                            drFila.valor_recoge = drFila.saldo_capital + intcorriente + intmora + intotros;
                            if (drFila.valor_recoge < drFila.valor_nominas)
                            {
                                drFila.valor_recoge = drFila.valor_nominas;
                            }
                            drFila.valor_recoge -= drFila.valor_nominas;

                            vrRecogidos = vrRecogidos + Convert.ToDecimal(drFila.valor_recoge);
                            vrMonto = vrMonto - vrRecogidos;
                            txtVrDesembolsado.Text = vrMonto.ToString("C");
                            Session["VrDesembolso"] = vrMonto;
                        }
                    }

                    SolicitudCredServRecogidosService serviciosRecogidosServices = new SolicitudCredServRecogidosService();
                    List<SolicitudCredServRecogidos> listaServicios = serviciosRecogidosServices.ListarSolicitudCredServRecogidosActualizado(Convert.ToInt64(txtNumRadic.Text), Usuario);

                    if (listaServicios.Count >= 1)
                    {

                        Configuracion conf = new Configuracion();
                        decimal vrMonto = 0;
                        string sMonto = txtVrDesembolsado.Text.Replace("$", "").Replace(gSeparadorMiles, "");
                        sMonto = sMonto.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, conf.ObtenerSeparadorDecimalConfig());
                        vrMonto = Convert.ToDecimal(sMonto);
                        decimal vrRecogidos = 0;
                        foreach (SolicitudCredServRecogidos drFila in listaServicios)
                        {

                            CreditoRecoger List = new CreditoRecoger();
                            List.valor_recoge = drFila.valorrecoger;
                            List.valor_total = drFila.valorrecoger; ;
                            List.saldo_capital = drFila.valor_cuota;
                            List.numero_radicacion = drFila.numeroservicio;
                            List.linea_credito = drFila.nom_linea;

                            vrRecogidos = vrRecogidos + Convert.ToDecimal(drFila.valorrecoger);
                            lstConsultas.Add(List);
                        }
                        vrMonto = vrMonto - vrRecogidos;
                        txtVrDesembolsado.Text = vrMonto.ToString("C");
                        Session["VrDesembolso"] = vrMonto;
                    }

                    dtListRecogidos.DataSource = lstConsultas;
                    dtListRecogidos.DataBind();

                }
                // Mostrando la grilla                
                gvPlanPagos.Visible = true;
                gvPlanPagos.DataBind();
                gvPlanPagos.Columns[1].ItemStyle.Width = 90;
                // Ocultando las columnas que no deben mostrarse
                List<Atributos> lstAtr = new List<Atributos>();
                lstAtr = datosServicio.GenerarAtributosPlan((Usuario)Session["usuario"]);
                Session["AtributosPlanPagos"] = lstAtr;
                for (int i = 4; i <= 18; i++)
                {
                    gvPlanPagos.Columns[i].Visible = false;
                    int j = 0;
                    foreach (Atributos item in lstAtr)
                    {
                        if (j == i - 4)
                            gvPlanPagos.Columns[i].HeaderText = item.nom_atr;
                        j = j + 1;
                    }
                }
                // Establecer el ancho de las columnas de valores
                for (int i = 2; i < 20; i++)
                {
                    gvPlanPagos.Columns[i].ItemStyle.Width = anchocolumna;
                }
                // Ajustando el tamaño de la grilla
                longitud = 0;
                for (int i = 0; i < 20; i++)
                {
                    if (gvPlanPagos.Columns[i].Visible == true)
                        longitud = longitud + Convert.ToInt32(gvPlanPagos.Columns[i].ItemStyle.Width.Value);
                }
                if (longitud + anchocolumna > 900)
                {
                    gvPlanPagos.Width = longitud + anchocolumna;
                }
                else
                {
                    gvPlanPagos.Width = 900;
                }
                if (lstConsulta.Count < 10)
                    gvPlanPagos.Height = lstConsulta.Count * 30;
                // Mostrando las columnas que tienen valores
                foreach (DatosPlanPagos ItemPlanPagos in lstConsulta)
                {
                    if (ItemPlanPagos.int_1 != 0) { gvPlanPagos.Columns[4].Visible = true; }
                    if (ItemPlanPagos.int_2 != 0) { gvPlanPagos.Columns[5].Visible = true; }
                    if (ItemPlanPagos.int_3 != 0) { gvPlanPagos.Columns[6].Visible = true; }
                    if (ItemPlanPagos.int_4 != 0) { gvPlanPagos.Columns[7].Visible = true; }
                    if (ItemPlanPagos.int_5 != 0) { gvPlanPagos.Columns[8].Visible = true; }
                    if (ItemPlanPagos.int_6 != 0) { gvPlanPagos.Columns[9].Visible = true; }
                    if (ItemPlanPagos.int_7 != 0) { gvPlanPagos.Columns[10].Visible = true; }
                    if (ItemPlanPagos.int_8 != 0) { gvPlanPagos.Columns[11].Visible = true; }
                    if (ItemPlanPagos.int_9 != 0) { gvPlanPagos.Columns[12].Visible = true; }
                    if (ItemPlanPagos.int_10 != 0) { gvPlanPagos.Columns[13].Visible = true; }
                    if (ItemPlanPagos.int_11 != 0) { gvPlanPagos.Columns[14].Visible = true; }
                    if (ItemPlanPagos.int_12 != 0) { gvPlanPagos.Columns[15].Visible = true; }
                    if (ItemPlanPagos.int_13 != 0) { gvPlanPagos.Columns[16].Visible = true; }
                    if (ItemPlanPagos.int_14 != 0) { gvPlanPagos.Columns[17].Visible = true; }
                    if (ItemPlanPagos.int_15 != 0) { gvPlanPagos.Columns[18].Visible = true; }
                    // Mostrar fecha de primer pago
                    if (Txtprimerpago.Text.Trim() == "" && ItemPlanPagos.fechacuota != null && ItemPlanPagos.numerocuota == 1)
                        Txtprimerpago.Text = Convert.ToDateTime(ItemPlanPagos.fechacuota).ToString(gFormatoFecha);
                }
                gvPlanPagos.DataBind();
            }
            else
            {
                gvPlanPagos.Visible = true;
            }

            // Ajustar valores para la grilla que se usa para descargar los datos a excel.
            if (lstConsulta.Count > 0)
            {
                gvPlanPagos0.Visible = true;
                gvPlanPagos0.DataBind();
                gvPlanPagos0.Columns[1].ItemStyle.Width = 90;
                // Ocultando las columnas que no deben mostrarse
                List<Atributos> lstAtr = new List<Atributos>();
                lstAtr = datosServicio.GenerarAtributosPlan((Usuario)Session["usuario"]);
                for (int i = 4; i <= 18; i++)
                {
                    gvPlanPagos0.Columns[i].Visible = false;
                    int j = 0;
                    foreach (Atributos item in lstAtr)
                    {
                        if (j == i - 4)
                            gvPlanPagos0.Columns[i].HeaderText = item.nom_atr;
                        j = j + 1;
                    }
                }
                // Establecer el ancho de las columnas de valores
                for (int i = 2; i < 20; i++)
                {
                    gvPlanPagos0.Columns[i].ItemStyle.Width = anchocolumna;
                }
                // Ajustando el tamaño de la grilla
                longitud = 0;
                for (int i = 0; i < 20; i++)
                {
                    longitud = longitud + Convert.ToInt32(gvPlanPagos0.Columns[i].ItemStyle.Width.Value);
                }
                gvPlanPagos0.Width = longitud / 2;
                foreach (DatosPlanPagos ItemPlanPagos in lstConsulta)
                {
                    if (ItemPlanPagos.int_1 != 0) { gvPlanPagos0.Columns[4].Visible = true; }
                    if (ItemPlanPagos.int_2 != 0) { gvPlanPagos0.Columns[5].Visible = true; }
                    if (ItemPlanPagos.int_3 != 0) { gvPlanPagos0.Columns[6].Visible = true; }
                    if (ItemPlanPagos.int_4 != 0) { gvPlanPagos0.Columns[7].Visible = true; }
                    if (ItemPlanPagos.int_5 != 0) { gvPlanPagos0.Columns[8].Visible = true; }
                    if (ItemPlanPagos.int_6 != 0) { gvPlanPagos0.Columns[9].Visible = true; }
                    if (ItemPlanPagos.int_7 != 0) { gvPlanPagos0.Columns[10].Visible = true; }
                    if (ItemPlanPagos.int_8 != 0) { gvPlanPagos0.Columns[11].Visible = true; }
                    if (ItemPlanPagos.int_9 != 0) { gvPlanPagos0.Columns[12].Visible = true; }
                    if (ItemPlanPagos.int_10 != 0) { gvPlanPagos0.Columns[13].Visible = true; }
                    if (ItemPlanPagos.int_11 != 0) { gvPlanPagos0.Columns[14].Visible = true; }
                    if (ItemPlanPagos.int_12 != 0) { gvPlanPagos0.Columns[15].Visible = true; }
                    if (ItemPlanPagos.int_13 != 0) { gvPlanPagos0.Columns[16].Visible = true; }
                    if (ItemPlanPagos.int_14 != 0) { gvPlanPagos0.Columns[17].Visible = true; }
                    if (ItemPlanPagos.int_15 != 0) { gvPlanPagos0.Columns[18].Visible = true; }
                }
                gvPlanPagos0.DataBind();
            }
            else
            {
                gvPlanPagos0.Visible = false;
            }

            btnPlanPagosOriginal.Text = @"Plan Pagos Original";
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    public void VerificarLineaSiEsReestructurada(string cod_linea)
    {
        General general = ConsultarParametroGeneral(430, Usuario);
        if (general == null) return;

        bool esLineaReestructurada = general.valor.Split(',').Contains(cod_linea);

        if (esLineaReestructurada)
        {
            btnPlanPagosOriginal.Visible = true;
        }
    }
    private void Actualizar(String pIdObjeto)
    {
        try
        {
            String emptyQuery = "Fila de datos vacia";
            Credito datosApp = new Credito();
            datosApp.numero_radicacion = Int64.Parse(pIdObjeto);
            List<DatosPlanPagos> lstConsultaCreditos = new List<DatosPlanPagos>();
            lstConsultaCreditos.Clear();
            DatosPlanPagosService datosServicio = new DatosPlanPagosService();
            lstConsultaCreditos = datosServicio.ListarDatosPlanPagos(datosApp, (Usuario)Session["usuario"]);
            gvPlanPagos.EmptyDataText = emptyQuery;
            gvPlanPagos.DataSource = lstConsultaCreditos;
            if (lstConsultaCreditos.Count > 0)
            {
                mvLista.ActiveViewIndex = 0;
                gvPlanPagos.DataBind();
                if (gvPlanPagos.Rows.Count < 10)
                    gvPlanPagos.Height = gvPlanPagos.Rows.Count * 30;
            }
            else
            {
                mvLista.ActiveViewIndex = -1;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Actualizar", ex);
        }
    }
    /// <summary>
    /// Actualizar los datos del crédito
    /// </summary>
    /// <param name="codigo"></param>
    /// <param name="gvLista"></param>
    /// <param name="opcion"></param>
    private void Actualizar2(string codigo, GridView gvLista, int opcion)
    {
        DocumentoService documentoServicio = new DocumentoService();
        Documento doc = new Documento();
        try
        {
            List<Documento> lstConsulta = new List<Documento>();

            doc.cod_linea_credito = codigo;

            // Dependiendo del estado del crédito se muestran los documentos
            switch (opcion)
            {

                case 1:
                    lstConsulta = documentoServicio.ListarCartaAprobacion(doc, (Usuario)Session["usuario"]);
                    break;
                case 2:
                    doc.numero_radicacion = Convert.ToInt64(idObjeto);
                    lstConsulta = documentoServicio.ListarCartaAprobacionGenerado(doc, (Usuario)Session["usuario"]);

                    //  gvLista2.Visible = true;
                    break;
            }

            gvLista.PageSize = 10;
            String emptyQuery = "Fila de datos vacia";
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;
            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                //ValidarPermisosGrilla(gvLista);
                if (opcion == 1)
                {
                    foreach (GridViewRow row in gvLista.Rows)
                    {
                        DocumentoService documentoServicio1 = new DocumentoService();
                        string dato = GetCellByName(row, "Requerido").Text;
                        string tipo = GetCellByName(row, "Tipo documento").Text;
                        if (dato == "Si")
                        {
                            CheckBox chkSeleccionar = ((CheckBox)row.FindControl("cbx"));
                            chkSeleccionar.Checked = true;
                        }
                        TextBox txtReferencia = ((TextBox)row.FindControl("txtReferencia"));
                        txtReferencia.Text = this.txtNumRadic.Text;
                        txtReferencia.Enabled = false;

                        txtReferencia.Text = generar(tipo);
                    }
                }
            }
            else
            {
                // gvLista.Visible = false;
                lblTotalRegs.Visible = false;
            }

            Session.Add(creditoPlanServicio.CodigoPrograma + ".consulta", 1);

            foreach (GridViewRow row in gvLista2.Rows)
            {
                //gvLista.Visible = true;
                Documento document = new Documento();
                string cRutaLocal = GetCellByName(row, "ruta").Text;
                string cNombreDocumento = GetCellByName(row, "Tipo documento").Text;
                string sDocumento = Server.MapPath("../GeneracionDocumentos/Documentos\\" + this.txtNumRadic.Text.Trim() + "_" + cNombreDocumento + ".pdf");
                Boolean bExiste = System.IO.File.Exists(sDocumento);
                if (Session["Generado"] != null)
                    bExiste = Convert.ToBoolean(Session["Generado"]);

                ((ImageButton)row.FindControl("btnImprimir")).Visible = bExiste;


                //generardocumento();
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "Actualizar", ex);
        }
    }
    public string generar(string svar)
    {
        DocumentoService documentoServicio = new DocumentoService();
        string resultado = documentoServicio.Listarconsecutivo(svar, (Usuario)Session["Usuario"]);
        return resultado;
    }
    /// <summary>
    /// Dependiendo del estado del crédito se activan las grillas.
    /// </summary>
    /// <param name="vCredito"></param>
    private void validarGrid()
    {
        Documento doc = new Documento();
        List<Documento> lstConsulta = new List<Documento>();
        CreditoPlan credito = new CreditoPlan();
        credito.LineaCredito = txtLinea.Text;
        doc.numero_radicacion = Convert.ToInt64(idObjeto);
        DocumentoService documentoServicio = new DocumentoService();
        lstConsulta = documentoServicio.ListarCartaAprobacionGenerado(doc, (Usuario)Session["usuario"]);

        if (lstConsulta.Count == 0)
        {
            credito.Estado = "A";
            switch (credito.Estado)
            {
                case "A":
                    Actualizar2(credito.LineaCredito, gvLista, 1);
                    lbDocumentos.Text += " A GENERAR:";
                    gvLista2.Visible = false;
                    break;
            }
        }
        else
        {
            credito.Estado = "Desembolsado";
            switch (credito.Estado)
            {
                case "Desembolsado":

                    Actualizar2(credito.LineaCredito, gvLista2, 2);
                    gvLista.Visible = false;
                    lbDocumentos.Text += " GENERADOS:";
                    btnGenerar.Visible = true;
                    break;

            }
        }
    }
    //----- VALORES DE OPCIONES ::::  1 = CONSULTAR LA IMPRESION DE CREDITO ORDEN DE SERVICIO  
    // ------------------------ ::::  2 = CONSULTAR LA IMPRESION DE AUXILIO ORDEN DE SERVICIO  
    private void generardocumento()
    {
        string ruta = Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/Documentos/");

        if (Directory.Exists(ruta))
        {
            String fileName = txtNumRadic.Text + '_' + '3' + '1' + '.' + 'p' + 'd' + 'f';
            string savePath = ruta + fileName;

            FileInfo fi = new FileInfo(savePath);
            bool exists = fi.Exists;
            if (exists == false)
            {
                String Error = "No se envuentra pagaré generado";
                this.LblError.Text = Error;
                validarGrid();
                mvLista.ActiveViewIndex = 3;
            }
            else
            {
                FileStream archivo = new FileStream(savePath, FileMode.Open, FileAccess.Read);
                FileInfo file = new FileInfo(savePath);
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.TransmitFile(file.FullName);
                Response.End();
            }
        }
    }

    byte[] PrevisualizarArchivo()
    {
        VerError("");
        lblNotificacionGuardado.Text = string.Empty;
        lblNotificacionGuardado.Visible = false;

        if (avatarUpload.PostedFile != null && avatarUpload.PostedFile.ContentLength > 0 && avatarUpload.PostedFile.ContentLength <= 20000000)
        {
            if (!avatarUpload.PostedFile.FileName.Contains(".pdf"))
            {
                VerError("Extensión Invalida!.");
                return null;
            }

            string hola = avatarUpload.PostedFile.FileName;

            //CREANDO REPORTE
            byte[] bytes = ObtenerBytesDeFileUpload();

            MostrarArchivoEnLiteral(bytes);

            return bytes;
        }
        else
        {
            VerError("Debes subir un archivo valido!.");
            return null;
        }
    }

    byte[] ObtenerBytesDeFileUpload()
    {
        byte[] bytesArrImagen = null;

        StreamsHelper streamHelper = new StreamsHelper();

        using (Stream streamImagen = avatarUpload.PostedFile.InputStream)
        {
            bytesArrImagen = streamHelper.LeerTodosLosBytesDeUnStream(streamImagen);
        }

        return bytesArrImagen;
    }
    void MostrarArchivoEnLiteral(byte[] bytes)
    {
        Usuario pUsuario = _usuario;

        string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_pagare" + pUsuario.nombre : "";
        // ELIMINANDO ARCHIVOS GENERADOS
        try
        {
            string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
            foreach (string ficheroActual in ficherosCarpeta)
                if (ficheroActual.Contains(pNomUsuario))
                    File.Delete(ficheroActual);
        }
        catch
        { }

        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("Archivos/output" + pNomUsuario + ".pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        //MOSTRANDO REPORTE
        string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"90%\" height=\"700px\">";
        adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
        adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        adjuntar += "</object>";

        ltPagare.Text = string.Format(adjuntar, ResolveUrl("Archivos/output" + pNomUsuario + ".pdf"));
    }
    public void ListarAtributos(long numeroRadicacion)
    {
        List<LineasCredito> lstAtributos = CreditoServicio.ListarAtributosFinanciados(numeroRadicacion, (Usuario)Session["Usuario"]);
        gvAtributos.DataSource = lstAtributos;
        gvAtributos.DataBind();
        mpeDocAnexo.Show();
        gvAtributos.Visible = true;
    }

    Credito CreditoReliquidado(string numRadic)
    {
        return reliquidacionService.CreditoReliquidado(numRadic, (Usuario)Session["usuario"]);
    }
    #endregion

    #region Obtencion de datos
    protected void ObtenerDatos(String pIdObjeto, int Planpagos)
    {
        try
        {
            CreditoPlan credito = new CreditoPlan();
            HistoricoTasa historico = new HistoricoTasa();
            if (pIdObjeto != null)
            {
                credito.Numero_radicacion = Int64.Parse(pIdObjeto);
                credito = creditoPlanServicio.ConsultarCredito(credito.Numero_radicacion, true, (Usuario)Session["usuario"]);
                //Consulto Tasa Historica 
                clasificacionCartera =
                    clasificacionCarteraService.ConsultarClasificacion(credito.cod_clasifica,
                        (Usuario)Session["usuario"]);
                if (clasificacionCartera != null)
                {
                    lsHistoricoTasas = historicoTasaService.listarhistorico(clasificacionCartera.tipo_historico, (Usuario)Session["usuario"]);
                    foreach (HistoricoTasa historicoTasa in lsHistoricoTasas)
                    {
                        if (credito.TasaNom > (double)historicoTasa.VALOR)
                        {
                            if (credito.FechaAprobacion >= historicoTasa.FECHA_INICIAL && credito.FechaAprobacion <= historicoTasa.FECHA_FINAL)
                            {
                                historico.VALOR = historicoTasa.VALOR;
                                ViewState["HistoricoTasa"] = historico.VALOR;
                            }
                        }

                    }
                }

                Credito creditoReliquidado = CreditoReliquidado(pIdObjeto);

                if (!string.IsNullOrEmpty(credito.Estado))
                    if (credito.Estado == "A")
                        txtEstado.Text = "Aprobado";
                    else if ((credito.Estado == "G"))
                        txtEstado.Text = "Generado";
                    else if ((credito.Estado == "C"))
                        txtEstado.Text = "Desembolsado";
                    else if ((credito.Estado == "C"))
                        txtEstado.Text = "Analisis";
                    else
                        txtEstado.Text = credito.Estado;
                if (txtEstado.Text == "Desembolsado")
                {
                    btnTalonario.Visible = true;
                }
                if (!string.IsNullOrEmpty(credito.Numero_radicacion.ToString()))
                    txtNumRadic.Text = HttpUtility.HtmlDecode(credito.Numero_radicacion.ToString());
                if (!string.IsNullOrEmpty(credito.Linea))
                {
                    txtLinea.Text = HttpUtility.HtmlDecode(credito.LineaCredito);
                    VerificarLineaSiEsReestructurada(credito.LineaCredito);
                }
                if (!string.IsNullOrEmpty(credito.LineaCredito))
                    txtNombreLinea.Text = HttpUtility.HtmlDecode(credito.Linea);
                if (!string.IsNullOrEmpty(credito.Identificacion))
                    txtIdentific.Text = HttpUtility.HtmlDecode(credito.Identificacion);
                if (!string.IsNullOrEmpty(credito.Tipo_Identificacion))
                    txtTipoIdentific.Text = HttpUtility.HtmlDecode(credito.Tipo_Identificacion);
                if (!string.IsNullOrEmpty(credito.Nombres))
                    txtNombre.Text = HttpUtility.HtmlDecode(credito.Nombres);
                if (!string.IsNullOrEmpty(credito.Plazo.ToString()))
                    txtPlazo.Text = HttpUtility.HtmlDecode(credito.Plazo.ToString());
                if (!string.IsNullOrEmpty(credito.Monto.ToString()))
                {
                    txtMonto.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtMonto.Text = String.Format("{0:C}", Convert.ToInt64(txtMonto.Text));
                    Session["ValorMonto"] = credito.Monto;
                    txtMontoCalculado.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtMontoCalculado.Text = String.Format("{0:C}", Convert.ToInt64(txtMontoCalculado.Text));
                    txtVrDesembolsado.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtVrDesembolsado.Text = String.Format("{0:C}", Convert.ToInt64(txtVrDesembolsado.Text));
                    Session["VrDesembolso"] = credito.Monto;
                };
                if (!string.IsNullOrEmpty(credito.Periodicidad))
                    txtPeriodicidad.Text = HttpUtility.HtmlDecode(credito.Periodicidad);
                if (!string.IsNullOrEmpty(credito.FormaPago))
                    txtFormaPago.Text = HttpUtility.HtmlDecode(credito.FormaPago);
                if (!string.IsNullOrEmpty(credito.FechaInicio.ToString()))
                {
                    txtFechaInicial.Text = HttpUtility.HtmlDecode(credito.FechaInicio.ToString());
                    txtFechaInicial.Text = String.Format("{0:d}", Convert.ToDateTime(txtFechaInicial.Text));
                };
                if (credito.FechaDesembolso != DateTime.MinValue)
                {
                    txtFechaDesembolso.Text = HttpUtility.HtmlDecode(credito.FechaDesembolso.ToString());
                    txtFechaDesembolso.Text = String.Format("{0:d}", Convert.ToDateTime(txtFechaDesembolso.Text));
                };

                if (credito.FechaAprobacion != DateTime.MinValue)
                {
                    TxtFechaApro.Text = HttpUtility.HtmlDecode(credito.FechaAprobacion.ToString());
                    TxtFechaApro.Text = String.Format("{0:d}", Convert.ToDateTime(TxtFechaApro.Text));
                };

                if (credito.Reestructurado == 1 || creditoReliquidado.numero_radicacion > 0)
                {
                    btnPlanPagosOriginal.Visible = true;
                }

                if (credito.FechaSolicitud != DateTime.MinValue)
                {
                    txtFechaSolicitud.Text = HttpUtility.HtmlDecode(credito.FechaSolicitud.ToString());
                    txtFechaSolicitud.Text = String.Format("{0:d}", Convert.ToDateTime(txtFechaSolicitud.Text));
                };
                if (!string.IsNullOrEmpty(credito.Cuota.ToString()))
                {
                    txtCuota.Text = HttpUtility.HtmlDecode(credito.Cuota.ToString());
                    txtCuota.Text = String.Format("{0:C}", Convert.ToDouble(txtCuota.Text));
                };
                if (!string.IsNullOrEmpty(credito.DiasAjuste.ToString()))
                {
                    txtDiasAjuste.Text = HttpUtility.HtmlDecode(credito.DiasAjuste.ToString());
                };
                if (!string.IsNullOrEmpty(credito.TasaNom.ToString()))
                {
                    txtTasaInteres.Text = HttpUtility.HtmlDecode(credito.TasaNom.ToString());
                    txtTasaInteres.Text = String.Format("{0:P2}", Convert.ToDouble(txtTasaInteres.Text) / 100);

                    txtTasaUsura.Text = HttpUtility.HtmlDecode(historico.VALOR.ToString());
                    txtTasaUsura.Text = String.Format("{0:P2}", Convert.ToDouble(txtTasaUsura.Text) / 100);
                };
                if (!string.IsNullOrEmpty(credito.TasaInteres.ToString()))
                {
                    txtTasaPeriodica.Text = HttpUtility.HtmlDecode(credito.TasaInteres.ToString());
                    txtTasaPeriodica.Text = String.Format("{0:P2}", Convert.ToDouble(txtTasaPeriodica.Text) / 100);

                    txtTasaPeriodicaUsu.Text = HttpUtility.HtmlDecode(historico.VALOR.ToString());
                    txtTasaPeriodicaUsu.Text = String.Format("{0:P2}", (Convert.ToDouble(txtTasaPeriodicaUsu.Text) / 100) / 12);

                };
                if (!string.IsNullOrEmpty(credito.tir.ToString()))
                {
                    txtTIR.Text = HttpUtility.HtmlDecode(credito.tir.ToString());
                    txtTIR.Text = String.Format("{0:P4}", Convert.ToDouble(txtTIR.Text) / 100);
                };
                if (!string.IsNullOrEmpty(credito.Moneda))
                    txtMoneda.Text = HttpUtility.HtmlDecode(credito.Moneda);
                if (Convert.ToString(credito.ciudad) == null)
                {
                    Txtciudad.Text = "  ";
                }
                else
                {
                    Txtciudad.Text = Convert.ToString(credito.ciudad);
                }

                if (Convert.ToString(credito.Direccion) == null)
                {
                    Txtdireccion.Text = "  ";
                }
                else
                {
                    Txtdireccion.Text = Convert.ToString(credito.Direccion);
                }
                if (credito.FechaPrimerPago == null)
                {
                    Txtprimerpago.Text = "  ";
                }
                else
                {
                    Txtprimerpago.Text = Convert.ToDateTime(credito.FechaPrimerPago).ToString(gFormatoFecha);
                }
                if (Convert.ToString(credito.FechaSolicitud) == null)
                {
                    Txtgeneracion.Text = "  ";
                }
                else
                {
                    Txtgeneracion.Text = (credito.FechaSolicitud).ToString(gFormatoFecha);
                }
                if (Convert.ToString(credito.TasaEfe) == null)
                {
                    Txtinteresefectiva.Text = "  ";
                }
                else
                {
                    Txtinteresefectiva.Text = Convert.ToString(credito.TasaEfe);
                    Txtinteresefectiva.Text = String.Format("{0:P2}", Convert.ToDouble(Txtinteresefectiva.Text) / 100);
                }
                if (Convert.ToString(credito.numero_cuotas) == null)
                {
                    Txtcuotas.Text = "  ";
                }
                else
                {
                    Txtcuotas.Text = Convert.ToString(credito.numero_cuotas);
                }
                if (Convert.ToString(credito.pagare) == null)
                {
                    Txtpagare.Text = "  ";
                }
                else
                {
                    Txtpagare.Text = Convert.ToString(credito.pagare);
                }
                if (Convert.ToString(credito.oficina) == null)
                {
                    TxtNomOficina.Text = "  ";
                }
                else
                {
                    TxtNomOficina.Text = Convert.ToString(credito.oficina);
                }
                if (Convert.ToString(credito.cod_oficina) == null)
                {
                    TxtCodOficina.Text = "  ";
                }
                else
                {
                    TxtCodOficina.Text = Convert.ToString(credito.cod_oficina);
                }
                if (credito.cod_clasifica == 3)
                {
                    btnInformeConsumo.Visible = false;
                    btnInformeSolicitud.Visible = true;
                }
                else
                {
                    btnInformeConsumo.Visible = true;
                    btnInformeSolicitud.Visible = false;
                }

                validarGrid();
                Session["Cod_persona"] = credito.Cod_persona;
                if (Planpagos == 1)
                {
                    TablaPlanPagosOri(pIdObjeto);
                }
                else
                {
                    TablaPlanPagos(pIdObjeto);
                }


                List<Documento> lstConsulta = new List<Documento>();
                try
                {
                    DocumentoService documentoServicio = new DocumentoService();
                    Documento doc = new Documento();
                    doc = documentoServicio.ConsultarDocumentos(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
                    if (!string.IsNullOrEmpty(doc.tipo_documento.ToString()))
                        txttipodoc.Text = doc.tipo_documento.ToString();
                }
                catch { }
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    protected void ObtenerDatos2(String radicado)
    {
        try
        {
            Configuracion conf = new Configuracion();
            CreditoPlan credito = new CreditoPlan();

            if (radicado != null)
            {

                credito.Numero_radicacion = Int64.Parse(radicado);
                credito = creditoPlanServicio.ConsultarCredito(credito.Numero_radicacion, true, (Usuario)Session["usuario"]);

                if (!string.IsNullOrEmpty(credito.Estado))
                    if (credito.Estado == "A")
                        txtEstado.Text = "Aprobado";
                    else if ((credito.Estado == "G"))
                        txtEstado.Text = "Generado";
                    else if ((credito.Estado == "C"))
                        txtEstado.Text = "Desembolsado";
                    else if ((credito.Estado == "C"))
                        txtEstado.Text = "Analisis";
                    else
                        txtEstado.Text = credito.Estado;
                if (!string.IsNullOrEmpty(credito.Numero_radicacion.ToString()))
                    txtNumRadic.Text = HttpUtility.HtmlDecode(credito.Numero_radicacion.ToString());
                if (!string.IsNullOrEmpty(credito.Linea))
                {
                    txtLinea.Text = HttpUtility.HtmlDecode(credito.LineaCredito);
                    VerificarLineaSiEsReestructurada(credito.LineaCredito);
                }
                if (!string.IsNullOrEmpty(credito.LineaCredito))
                    txtNombreLinea.Text = HttpUtility.HtmlDecode(credito.Linea);
                if (!string.IsNullOrEmpty(credito.Identificacion))
                    txtIdentific.Text = HttpUtility.HtmlDecode(credito.Identificacion);
                if (!string.IsNullOrEmpty(credito.Nombres))
                    txtNombre.Text = HttpUtility.HtmlDecode(credito.Nombres);
                if (!string.IsNullOrEmpty(credito.Plazo.ToString()))
                    txtPlazo.Text = HttpUtility.HtmlDecode(credito.Plazo.ToString());
                if (!string.IsNullOrEmpty(credito.Monto.ToString()))
                {
                    txtMonto.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtMonto.Text = String.Format("{0:C}", Convert.ToInt64(txtMonto.Text));
                    Session["ValorMonto"] = credito.Monto;
                    txtMontoCalculado.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtMontoCalculado.Text = String.Format("{0:C}", Convert.ToInt64(txtMontoCalculado.Text));
                    txtVrDesembolsado.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    txtVrDesembolsado.Text = String.Format("{0:C}", Convert.ToInt64(txtVrDesembolsado.Text));
                    Session["VrDesembolso"] = credito.Monto;
                };
                if (!string.IsNullOrEmpty(credito.Periodicidad))
                    txtPeriodicidad.Text = HttpUtility.HtmlDecode(credito.Periodicidad);
                if (!string.IsNullOrEmpty(credito.FormaPago))
                    txtFormaPago.Text = HttpUtility.HtmlDecode(credito.FormaPago);
                if (!string.IsNullOrEmpty(credito.FechaInicio.ToString()))
                {
                    txtFechaInicial.Text = HttpUtility.HtmlDecode(credito.FechaInicio.ToString());
                    txtFechaInicial.Text = String.Format("{0:d}", Convert.ToDateTime(txtFechaInicial.Text));
                };

                if (credito.FechaDesembolso != DateTime.MinValue)
                {
                    txtFechaDesembolso.Text = HttpUtility.HtmlDecode(credito.FechaDesembolso.ToString());
                    txtFechaDesembolso.Text = String.Format("{0:d}", Convert.ToDateTime(txtFechaDesembolso.Text));
                };
                if (credito.FechaAprobacion != DateTime.MinValue)
                {
                    TxtFechaApro.Text = HttpUtility.HtmlDecode(credito.FechaAprobacion.ToString());
                    TxtFechaApro.Text = String.Format("{0:d}", Convert.ToDateTime(TxtFechaApro.Text));
                };
                if (!string.IsNullOrEmpty(credito.Cuota.ToString()))
                {
                    txtCuota.Text = HttpUtility.HtmlDecode(credito.Cuota.ToString());
                    txtCuota.Text = String.Format("{0:C}", Convert.ToDouble(txtCuota.Text));
                };
                if (!string.IsNullOrEmpty(credito.DiasAjuste.ToString()))
                {
                    txtDiasAjuste.Text = HttpUtility.HtmlDecode(credito.DiasAjuste.ToString());
                };
                if (!string.IsNullOrEmpty(credito.TasaNom.ToString()))
                {
                    txtTasaInteres.Text = HttpUtility.HtmlDecode(credito.TasaNom.ToString());
                    txtTasaInteres.Text = String.Format("{0:P2}", Convert.ToDouble(txtTasaInteres.Text) / 100);
                };

                if (!string.IsNullOrEmpty(credito.TasaInteres.ToString()))
                {
                    txtTasaPeriodica.Text = HttpUtility.HtmlDecode(credito.TasaInteres.ToString());
                    txtTasaPeriodica.Text = String.Format("{0:P2}", Convert.ToDouble(txtTasaPeriodica.Text) / 100);
                };
                if (!string.IsNullOrEmpty(credito.LeyMiPyme.ToString()))
                {
                    txtTIR.Text = HttpUtility.HtmlDecode(credito.LeyMiPyme.ToString());
                    txtTIR.Text = String.Format("{0:P2}", Convert.ToDouble(txtTIR.Text) / 100);
                };
                if (!string.IsNullOrEmpty(credito.Moneda))
                    txtMoneda.Text = HttpUtility.HtmlDecode(credito.Moneda);
                if (Convert.ToString(credito.ciudad) == null)
                {
                    Txtciudad.Text = "  ";
                }
                else
                {
                    Txtciudad.Text = Convert.ToString(credito.ciudad);
                }


                if (Convert.ToString(credito.Direccion) == null)
                {
                    Txtdireccion.Text = "  ";
                }
                else
                {
                    Txtdireccion.Text = Convert.ToString(credito.Direccion);
                }

                if (credito.FechaPrimerPago == null)
                {
                    Txtprimerpago.Text = "  ";
                }
                else
                {
                    Txtprimerpago.Text = Convert.ToDateTime(credito.FechaPrimerPago).ToString(conf.ObtenerFormatoFecha());
                }

                if (Convert.ToString(credito.FechaSolicitud) == null)
                {
                    Txtgeneracion.Text = "  ";
                }
                else
                {
                    Txtgeneracion.Text = (credito.FechaSolicitud).ToString(conf.ObtenerFormatoFecha());

                }

                if (Convert.ToString(credito.TasaEfe) == null)
                {
                    Txtinteresefectiva.Text = "  ";
                }
                else
                {
                    Txtinteresefectiva.Text = Convert.ToString(credito.TasaEfe);
                    Txtinteresefectiva.Text = String.Format("{0:P2}", Convert.ToDouble(Txtinteresefectiva.Text) / 100);
                }

                if (Convert.ToString(credito.numero_cuotas) == null)
                {
                    Txtcuotas.Text = "  ";
                }
                else
                {
                    Txtcuotas.Text = Convert.ToString(credito.numero_cuotas);
                }

                if (Convert.ToString(credito.pagare) == null)
                {
                    Txtpagare.Text = "  ";
                }
                else
                {
                    Txtpagare.Text = Convert.ToString(credito.pagare);
                }
                if (Convert.ToString(credito.oficina) == null)
                {
                    TxtNomOficina.Text = "  ";
                }
                else
                {
                    TxtNomOficina.Text = Convert.ToString(credito.oficina);
                }

                if (Convert.ToString(credito.cod_oficina) == null)
                {
                    TxtCodOficina.Text = "  ";
                }
                else
                {
                    TxtCodOficina.Text = Convert.ToString(credito.cod_oficina);
                }

                Session["Cod_persona"] = credito.Cod_persona;
                Session["NumeroSolicitud"] = Session["NumeroSolicitud"] == null ? credito.NumeroSolicitud : Session["NumeroSolicitud"];
                TablaPlanPagos(radicado);
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    private Persona1 ObtenerValoresCodeudores()
    {
        Persona1 vPersona1 = new Persona1();

        if (!string.IsNullOrEmpty(txtNumRadic.Text))
            vPersona1.numeroRadicacion = Convert.ToInt64(txtNumRadic.Text);

        vPersona1.seleccionar = "CD"; //Bandera para ejecuta el select del CODEUDOR

        return vPersona1;
    }

    #endregion

    #region Metodos Informes (imprimir)
    protected void Imprimir(int pOpcion)
    {
        Xpinn.Auxilios.Services.SolicitudAuxilioServices AuxilioService = new Xpinn.Auxilios.Services.SolicitudAuxilioServices();
        Usuario pUsu = new Usuario();
        pUsu = (Usuario)Session["usuario"];

        DatosSolicitud entidad = new DatosSolicitud();

        string Identifi_Prov = "", Nombre_Prov = "";
        Int64 Num_PreImpreso = 0;
        if (pOpcion == 1) //TABLA CREDITO ORDEN SERVICIO
        {
            entidad = creditoPlanServicio.ConsultarProveedorXCredito(Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["usuario"]);

            Identifi_Prov = !string.IsNullOrEmpty(entidad.identificacionprov) ? entidad.identificacionprov.ToString() : "";
            Nombre_Prov = !string.IsNullOrEmpty(entidad.nombreprov) ? entidad.nombreprov : "";
            Num_PreImpreso = entidad.num_preimpreso != null ? Convert.ToInt64(entidad.num_preimpreso) : 0;
            //CONSULTAR LA LINEA
            LineasCreditoService LineasCreditoServicio = new LineasCreditoService();
            LineasCredito vLineasCredito = new LineasCredito();
            vLineasCredito = LineasCreditoServicio.ConsultarLineasCredito(Convert.ToString(txtLinea.Text), (Usuario)Session["usuario"]);
            if (vLineasCredito.credito_educativo == 1)
            {
                //SI LA LINEA ES EDUCATIVO
                Xpinn.Auxilios.Entities.SolicitudAuxilio pEntidad = new Xpinn.Auxilios.Entities.SolicitudAuxilio();

                //CONSULTANDO DATOS DEL AUXILIO
                string pFiltro = " WHERE NUMERO_RADICACION = " + txtNumRadic.Text;
                pEntidad = AuxilioService.Consultar_Auxilio_Variado(pFiltro, (Usuario)Session["usuario"]);
                if (pEntidad.numero_auxilio != 0 && pEntidad.cod_persona != 0) //SI EXISTE EL AUXILIO ENLAZADO AL CREDITO EDUCATIVO
                {
                    Session["NumAuxilio"] = pEntidad.numero_auxilio.ToString();
                    decimal vrAuxilio = 0, vrCredito = 0; ;
                    if (pEntidad.estado == "S")
                        vrAuxilio = pEntidad.valor_solicitado;
                    else
                        vrAuxilio = pEntidad.valor_aprobado;

                    vrCredito = Convert.ToDecimal(Session["ValorMonto"].ToString());

                    if (pEntidad.porc_matricula != 0)
                    {
                        if ((vrAuxilio + vrCredito) == pEntidad.porc_matricula)
                        {
                            Session["ValorMonto"] = pEntidad.porc_matricula.ToString("n0");
                        }
                    }
                    /*
                    //CONSULTAR LOS DATOS DE LA MATRICULA
                    Credito vCredito = new Credito();
                    vCredito = CreditoServicio.ConsultarCreditoAsesor(Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["usuario"]);
                    if (vCredito.universidad != null)
                        txtUniversidad.Text = vCredito.universidad;
                    if (vCredito.semestre != null)
                        txtSemestre.Text = vCredito.semestre; */
                }
            }
        }
        else if (pOpcion == 2)
        {
            //TABLA SERVICIOS ORDEN SERVICIO
            if (Session["NUM_AUXILIO"].ToString().Contains("/"))
            {
                string[] sCod = Session["NUM_AUXILIO"].ToString().Split('/');
                AprobacionServiciosServices AprobacionServicios = new AprobacionServiciosServices();
                Servicio vDetalle = new Servicio();
                vDetalle = AprobacionServicios.ConsultarSERVICIO(Convert.ToInt32(sCod[0]), (Usuario)Session["usuario"]);
                if (vDetalle.identificacion != null)
                    txtIdentific.Text = vDetalle.identificacion;
                if (vDetalle.nombre != null)
                    txtNombre.Text = vDetalle.nombre;
                if (vDetalle.fecha_activacion != DateTime.MinValue)
                    txtFechaDesembolso.Text = vDetalle.fecha_activacion.ToShortDateString();
                if (vDetalle.valor_total != 0)
                {
                    Session["ValorMonto"] = vDetalle.valor_total;
                    txtMonto.Text = vDetalle.valor_total.ToString();
                    txtMonto.Text = String.Format("{0:C}", Convert.ToInt64(txtMonto.Text));
                }
                txtNumRadic.Text = vDetalle.numero_servicio.ToString();
                if (vDetalle.nom_ciudad != null)
                    Txtciudad.Text = vDetalle.nom_ciudad;
                //CONSULTAR EL PROVEEDOR
                LineaServiciosServices BOLineaServ = new LineaServiciosServices();
                LineaServicios vData = new LineaServicios();
                vData = BOLineaServ.ConsultarLineaSERVICIO(vDetalle.cod_linea_servicio, (Usuario)Session["usuario"]);
                if (vData != null)
                {
                    if (vData.tipo_servicio == 5) //Si es Tipo Orden de SErvicio
                    {
                        //Identifi_Prov = vData.identificacion_proveedor != null ? vData.identificacion_proveedor : "";
                        //Nombre_Prov = vData.nombre_proveedor != null ? vData.nombre_proveedor : "";
                        Identifi_Prov = vDetalle.identificacion_proveedor != null ? vDetalle.identificacion_proveedor : "";
                        Nombre_Prov = vDetalle.nombre_proveedor != null ? vDetalle.nombre_proveedor : "";
                        Num_PreImpreso = vDetalle.numero_preimpreso != null ? Convert.ToInt64(vDetalle.numero_preimpreso) : 0;
                    }
                }
            }
            else //TABLA AUXILIO ORDEN SERVICIO
            {
                if (Session["NumCred_Orden"] != null)
                {
                    Xpinn.Auxilios.Services.DesembolsoAuxilioServices DesemServicios = new Xpinn.Auxilios.Services.DesembolsoAuxilioServices();
                    Xpinn.Auxilios.Services.SolicitudAuxilioServices SolicAuxilios = new Xpinn.Auxilios.Services.SolicitudAuxilioServices();
                    Xpinn.Auxilios.Entities.SolicitudAuxilio vDetalle = new Xpinn.Auxilios.Entities.SolicitudAuxilio();

                    //CONSULTAR EL BENEFICIARIO
                    vDetalle = DesemServicios.ConsultarAuxilioAprobado(Convert.ToInt32(Session["NumCred_Orden"].ToString()), (Usuario)Session["usuario"]);
                    if (vDetalle.identificacion != null)
                        txtIdentific.Text = vDetalle.identificacion;
                    if (vDetalle.nombre != null)
                        txtNombre.Text = vDetalle.nombre;
                    if (vDetalle.fecha_desembolso != DateTime.MinValue)
                        txtFechaDesembolso.Text = vDetalle.fecha_desembolso.ToShortDateString();
                    if (vDetalle.valor_aprobado != 0)
                    {
                        Session["ValorMonto"] = vDetalle.valor_aprobado;
                        txtMonto.Text = vDetalle.valor_aprobado.ToString();
                        txtMonto.Text = String.Format("{0:C}", Convert.ToInt64(txtMonto.Text));
                    }
                    txtNumRadic.Text = vDetalle.numero_auxilio.ToString();

                    //CONSULTAR EL PROVEEDOR
                    Xpinn.Auxilios.Entities.Auxilio_Orden_Servicio pEntidad = new Xpinn.Auxilios.Entities.Auxilio_Orden_Servicio();
                    String pFiltro = "WHERE NUMERO_AUXILIO = " + vDetalle.numero_auxilio.ToString();
                    pEntidad = SolicAuxilios.ConsultarAUX_OrdenServicio(pFiltro, (Usuario)Session["usuario"]);

                    Identifi_Prov = pEntidad.idproveedor != null ? pEntidad.idproveedor : "";
                    Nombre_Prov = pEntidad.nomproveedor != null ? pEntidad.nomproveedor : "";
                    Num_PreImpreso = pEntidad.numero_preimpreso != null ? Convert.ToInt64(pEntidad.numero_preimpreso) : 0;

                    Site toolBar = (Site)Master;
                    toolBar.MostrarRegresar(false);
                }
            }
        }

        vTipoDoc = tipoDocumentoServicio.ConsultarDocumentoOrden("1", (Usuario)Session["usuario"]);
        if (vTipoDoc != null)
        {
            string cDocumentoGenerado = Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/" + cDocsSubDir + "/" + idObjeto + "_" + vTipoDoc.tipo_documento + '.' + 'p' + 'd' + 'f');
            lstDatosDeDocumento = datosDeDocumentoServicio.ListarDatosDeDocumentoFormato(Convert.ToInt64(idObjeto), vTipoDoc.tipo_documento, (Usuario)Session["usuario"]);
            string html = Encoding.ASCII.GetString(vTipoDoc.Textos);
            if (vTipoDoc.Textos != null)
                ProcessesHTML.ReemplazarEnDocumentoDeWordYGuardarPdf(html, lstDatosDeDocumento, cDocumentoGenerado, ref pError);
            else
                ProcessesHTML.ReemplazarEnDocumentoDeWordYGuardarPdf(vTipoDoc.texto, lstDatosDeDocumento, cDocumentoGenerado, ref pError);
        }
        else
        {

            ReportParameter[] parame = new ReportParameter[14];
            parame[0] = new ReportParameter("entidad", pUsu.empresa);
            parame[1] = new ReportParameter("nit", pUsu.nitempresa);
            parame[2] = new ReportParameter("nom_proveedor", Nombre_Prov);
            parame[3] = new ReportParameter("nitproveedor", Identifi_Prov);
            parame[4] = new ReportParameter("nom_beneficiario", txtNombre.Text);
            parame[5] = new ReportParameter("identi_beneficiario", txtIdentific.Text);
            parame[6] = new ReportParameter("valorMonto", " " + Convert.ToDecimal(Session["ValorMonto"].ToString()).ToString("c2"));

            // Determinando el valor en letras
            Cardinalidad numero = new Cardinalidad();
            string cardinal = " ";
            string valor = Session["ValorMonto"].ToString();
            if (valor != "0")
            {
                cardinal = numero.enletras(valor.Replace(".", ""));
                int cont = cardinal.Length - 1;
                int cont2 = cont - 7;
                if (cont2 >= 0)
                {
                    string c = cardinal.Substring(cont2);
                    if (cardinal.Substring(cont2) == "MILLONES" || cardinal.Substring(cont2 + 2) == "MILLON")
                        cardinal = cardinal + " DE PESOS M/CTE";
                    else
                        cardinal = cardinal + " PESOS M/CTE";
                }
            }

            parame[7] = new ReportParameter("valorMontoLetras", cardinal);
            parame[8] = new ReportParameter("nom_Ciudad", " " + Txtciudad.Text);

            string Fecha = txtFechaDesembolso.Text.Trim() != "" ? Convert.ToDateTime(txtFechaDesembolso.Text).ToString("yyyy/MM/dd").Replace("/", " ") : "";
            parame[9] = new ReportParameter("fecha", Fecha);

            parame[10] = new ReportParameter("num_credito", txtNumRadic.Text);
            // parame[11] = new ReportParameter("nom_oficina", pUsu.nombre_oficina);
            parame[11] = new ReportParameter("nom_oficina", TxtNomOficina.Text);
            if (Num_PreImpreso != 0)
                parame[12] = new ReportParameter("numPreImpreso", TxtCodOficina.Text + " -" + Num_PreImpreso.ToString());
            else
                parame[12] = new ReportParameter("numPreImpreso", " ");
            parame[13] = new ReportParameter("ImagenReport", ImagenReporte());
            //parame[14] = new ReportParameter("cod_of", TxtCodOficina.Text +" -");

            ReportViewer2.LocalReport.EnableExternalImages = true;
            ReportViewer2.LocalReport.DataSources.Clear();
            ReportViewer2.LocalReport.SetParameters(parame);
            ReportViewer2.LocalReport.Refresh();

            ReportViewer2.Visible = true;
            frmPrint.Visible = false;
            mvLista.ActiveViewIndex = 4;
        }
    }
    protected void btnimprimesolicitud(object sender, EventArgs e)
    {
        if (ReportViewersolicitud.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = ReportViewersolicitud.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            Usuario pUsuario = (Usuario)Session["usuario"];
            string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_" + pUsuario.nombre : "";
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output" + pNomUsuario + ".pdf");
            framesolicitud.Visible = true;
            ReportViewersolicitud.Visible = false;

        }
    }
    protected void btnImprimiendose_Click(object sender, EventArgs e)
    {
        if (ReportViewerPlan.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            var bytes = ReportViewerPlan.LocalReport.Render("PDF");
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline;attachment; filename=Reporte.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush(); // send it to the client to download
            Response.Clear();

            //Warning[] warnings;
            //string[] streamids;
            //string mimeType;
            //string encoding;
            //string extension;
            //byte[] bytes = ReportViewerPlan.LocalReport.Render("PDF", null, out mimeType,
            //               out encoding, out extension, out streamids, out warnings);
            //Xpinn.Util.Usuario pUsuario = (Xpinn.Util.Usuario)Session["usuario"];
            //string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_" + pUsuario.nombre : "";
            //FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
            //FileMode.Create);
            //fs.Write(bytes, 0, bytes.Length);
            //fs.Close();
            //Session["Archivo"] = Server.MapPath("output" + pNomUsuario + ".pdf");
            //frmPrint.Visible = true;
            //Iframe1.Visible = true;
            //ReportViewerPlan.Visible = false;            
        }
    }
    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btnImprimir = sender as ImageButton;
        String sID = txtNumRadic.Text;
        GridViewRow row = (GridViewRow)btnImprimir.NamingContainer;

        string cNombreDocumento = GetCellByName(row, "Tipo documento").Text;
        string cNombreDeArchivo = txtNumRadic.Text.Trim() + "_" + cNombreDocumento + ".pdf";
        string cRutaLocalDeArchivoPDF = Server.MapPath("../GeneracionDocumentos/Documentos\\" + cNombreDeArchivo);

        if (GlobalWeb.bMostrarPDF == true)
        {
            // Copiar el archivo a una ruta local
            try
            {
                FileStream fs = File.OpenRead(cRutaLocalDeArchivoPDF);
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                fs.Close();
                // Determinar configuraciòn del tipo de archivo
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "application/pdf";
                // Mostrar el archivo
                Response.BinaryWrite(data);
                Response.End();
            }
            catch
            {
                Session[creditoPlanServicio.CodigoPrograma + ".id"] = sID;
                ObtenerDatos(sID, 0);
            }
        }
        else
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = false;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + cNombreDeArchivo);
            Response.ContentType = "application/octet-stream";
            Response.Flush();
            Response.WriteFile(cRutaLocalDeArchivoPDF);
            Response.End();
        }

    }
    protected void btnInformeSolicitud_Click(object sender, EventArgs e)
    {
        Persona1 vPersona1 = new Persona1();
        Persona1 vPersona2 = new Persona1();
        Persona1 vPersona3 = new Persona1();
        Referncias vPersona4 = new Referncias();
        Referencia refe = new Referencia();
        CreditoPlan creditos = new CreditoPlan();
        Persona1Service DatosClienteServicio = new Persona1Service();
        InformacionNegocio negocio = new InformacionNegocio();
        codeudores Codeudores = new codeudores();
        ConyugeService ConyugeCodeudorServicio = new ConyugeService();
        codeudoresService ServicioCodeudores = new codeudoresService();

        Session["Identificacion"] = txtIdentific.Text;
        negocio = DatosClienteServicio.Consultardatosnegocio(Convert.ToInt64(txtNumRadic.Text), Session["Identificacion"].ToString(), (Usuario)Session["Usuario"]);

        Codeudores = ServicioCodeudores.ConsultarDatosCodeudorRepo(Convert.ToString(txtNumRadic.Text), (Usuario)Session["usuario"]);
        Session["Codeudores"] = Codeudores.codpersona;

        creditos = DatosClienteServicio.ConsultarPersona1Paramcred(Convert.ToInt64(txtNumRadic.Text), Session["Identificacion"].ToString(), (Usuario)Session["Usuario"]);

        mvLista.ActiveViewIndex = 1;

        List<Persona1> resultado = new List<Persona1>();
        List<Referencia> referencias = new List<Referencia>();
        Conyuge conyugecod = new Conyuge();
        vPersona1.numeroRadicacion = Convert.ToInt64(txtNumRadic.Text);
        if (Session["Cod_persona"] != null)
            vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        resultado = DatosClienteServicio.ListadoPersonas1Reporte(vPersona1, (Usuario)Session["usuario"]);
        referencias = DatosClienteServicio.referencias(vPersona1, Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["Usuario"]);
        Session["Codeudores"] = Codeudores.codpersona;
        conyugecod = ConyugeCodeudorServicio.ConsultarRefConyugeRepo(Codeudores.codpersona, (Usuario)Session["Usuario"]);

        DataTable table2 = new DataTable();
        table2.Columns.Add("OBSERVACIONES");
        table2.Columns.Add("NOMBRE");
        table2.Columns.Add("TIEMPO");
        table2.Columns.Add("PROPIETARIO");
        table2.Columns.Add("CONCEPTO");

        DataRow datarw2;
        for (int i = 0; i < referencias.Count; i++)
        {
            datarw2 = table2.NewRow();
            refe = referencias[i];

            datarw2[0] = " " + refe.observaciones;
            datarw2[1] = " " + refe.nombrereferencia;
            datarw2[2] = " " + refe.tiempo;
            datarw2[3] = " " + refe.propietario;
            datarw2[4] = " " + refe.concepto;
            table2.Rows.Add(datarw2);
        }

        ReportParameter[] parame = new ReportParameter[95];

        DataTable table = new DataTable();
        table.Columns.Add("nombre");
        table.Columns.Add("identificacion");
        table.Columns.Add("telefono");
        table.Columns.Add("direccion");
        table.Columns.Add("nombrer");
        table.Columns.Add("tipor");
        table.Columns.Add("parentesco");
        table.Columns.Add("direccionr");
        table.Columns.Add("telefonor");

        DataRow datarw;

        for (int i = 0; i < resultado.Count; i++)
        {
            datarw = table.NewRow();
            if (i == 0)
                vPersona1 = resultado[i];
            if (i == 1)
                vPersona2 = resultado[i];
            if (i >= 2)
            {
                vPersona2 = resultado[i];


                datarw[0] = vPersona2.primer_nombre + " " + vPersona2.segundo_nombre + " " + vPersona2.primer_apellido + " " + vPersona2.segundo_apellido;
                datarw[1] = vPersona3.cod_persona;
                datarw[2] = vPersona2.direccion;
                datarw[3] = vPersona2.telefono;
                datarw[4] = "";
                datarw[5] = "";
                datarw[6] = "";
                datarw[7] = "";
                datarw[8] = "";
                table.Rows.Add(datarw);
            }
            if (txtNumRadic.Text == "" || Convert.ToString(resultado[i].identificacion) == "")
            { }


        }
        Usuario User = (Usuario)Session["usuario"];
        if (resultado.Count < 3)
        {
            parame[91] = new ReportParameter("datosvacios", "NO SE ENCONTRARON DATOS PARA ESTE ITEM");

            datarw = table.NewRow();
            datarw[0] = "";
            datarw[1] = "";
            datarw[2] = "";
            datarw[3] = "";
            datarw[4] = "";
            datarw[5] = "";
            datarw[6] = "";
            datarw[7] = "";
            datarw[8] = "";
            table.Rows.Add(datarw);
        }
        else
            parame[91] = new ReportParameter("datosvacios", " ");

        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";
        Usuario pUsu = (Usuario)Session["usuario"];

        parame[0] = new ReportParameter("Nombres", " " + vPersona1.primer_nombre + " " + vPersona1.segundo_nombre);
        parame[1] = new ReportParameter("Identificación", " " + vPersona1.tipo_identif + " " + vPersona1.identificacion);
        parame[2] = new ReportParameter("Fecha_nacimiento", " " + (vPersona1.fechanacimiento != null ? vPersona1.fechanacimiento.Value.ToShortDateString() : ""));
        parame[3] = new ReportParameter("Nivel_Estudio", " ");
        parame[4] = new ReportParameter("Telefono", " " + vPersona1.telefono);
        parame[5] = new ReportParameter("Apellidos", " " + vPersona1.primer_apellido + " " + vPersona1.segundo_apellido);
        parame[6] = new ReportParameter("Lugar_Expedicion", " " + vPersona1.ciudadexpedicion);
        parame[7] = new ReportParameter("Sexo", " " + vPersona1.sexo);
        parame[8] = new ReportParameter("mail", " " + vPersona1.email);
        parame[9] = new ReportParameter("direccion", " " + vPersona1.direccion);
        //CONYUGE
        parame[10] = new ReportParameter("IdentificaciónConyuge", " " + vPersona2.tipo_identif + " " + vPersona2.identificacion);
        parame[11] = new ReportParameter("TelefonoConyuge", " " + vPersona2.telefono);
        parame[12] = new ReportParameter("ApellidosConyuge", " " + vPersona2.primer_apellido + " " + vPersona2.segundo_apellido);
        parame[13] = new ReportParameter("SexoConyuge", " " + vPersona2.sexo);
        parame[14] = new ReportParameter("mailConyuge", " " + vPersona2.email);
        parame[15] = new ReportParameter("NombresConyuge", " " + vPersona2.primer_nombre + " " + vPersona2.segundo_nombre);
        parame[16] = new ReportParameter("DireccionConyuge", " " + vPersona2.direccion);
        parame[17] = new ReportParameter("EmpresaConyuge", " " + vPersona2.empresa);
        parame[18] = new ReportParameter("ContratoConyuge", " " + vPersona2.tipocontrato);
        parame[19] = new ReportParameter("AntiguedadEmpConyuge", " " + vPersona2.antiguedadlugarempresa);
        parame[20] = new ReportParameter("CelularConyuge", " " + vPersona2.celular);
        parame[21] = new ReportParameter("CelularEmpConyuge", " " + vPersona2.CelularEmpresa);
        //CREDITO
        parame[22] = new ReportParameter("MontoSolicitado", " " + creditos.Monto.ToString("0,0", CultureInfo.InvariantCulture));
        parame[23] = new ReportParameter("NumeroSolicitud", " " + creditos.Numero_Obligacion);
        parame[24] = new ReportParameter("NumerodeCredito", " " + creditos.Numero_radicacion);
        parame[25] = new ReportParameter("LineadeCredito", " " + creditos.LineaCredito);
        parame[26] = new ReportParameter("ValorCuota", " " + creditos.Cuota.ToString("0,0", CultureInfo.InvariantCulture));
        parame[27] = new ReportParameter("FechaSolicitud", " " + creditos.FechaSolicitud.ToShortDateString());
        //NEGOCIO
        parame[28] = new ReportParameter("nombrenegocio", " " + negocio.nombrenegocio);
        parame[29] = new ReportParameter("descripcionnegocio", " " + negocio.descripcion);
        parame[30] = new ReportParameter("direccionnegocio", " " + negocio.direccion);
        parame[31] = new ReportParameter("telefononegocio", " " + negocio.telefono);
        parame[32] = new ReportParameter("Fecha_Expedicion", " " + (vPersona1.fechaexpedicion != null ? vPersona1.fechaexpedicion.Value.ToShortDateString() : " "));
        parame[33] = new ReportParameter("Medio", " " + creditos.Medio);
        //persona
        parame[34] = new ReportParameter("TipoVivienda", " " + creditos.tipo_propiedad);
        parame[35] = new ReportParameter("ArrendadorViv", " " + creditos.arrendador);
        parame[36] = new ReportParameter("AntiguedadLugar", " " + creditos.antiguedad);
        parame[37] = new ReportParameter("TelArrendador", " " + creditos.telefonoarren);
        parame[38] = new ReportParameter("ValorArriendo", " " + creditos.valorarriendo);
        parame[39] = new ReportParameter("EstadoCivil", " " + creditos.EstadoCivil);
        // negocio
        parame[40] = new ReportParameter("Propiedad", " " + negocio.tipo_propiedad);
        parame[41] = new ReportParameter("Arrendador", " " + negocio.arrendador);
        parame[42] = new ReportParameter("TelefonoArrendador", " " + negocio.telefonoarrendador);
        parame[43] = new ReportParameter("Arriendo", " " + negocio.valor_arriendo);
        parame[44] = new ReportParameter("Experiencia", " " + negocio.experiencia);
        parame[45] = new ReportParameter("Antiguedad", " " + negocio.antiguedad);
        parame[46] = new ReportParameter("EmpleadosPerm", " " + negocio.emplperm);
        parame[47] = new ReportParameter("EmpleadosTemp", " " + negocio.empltem);
        parame[48] = new ReportParameter("Barrio", " " + negocio.barrioneg);
        parame[49] = new ReportParameter("Actividad", " " + negocio.descactividad);
        // codeudores
        parame[50] = new ReportParameter("NomCodeudor", " " + Codeudores.primer_nombre + " " + Codeudores.segundo_nombre + " " + Codeudores.primer_apellido + " " + Codeudores.segundo_apellido);
        parame[51] = new ReportParameter("IdentificacionCod", " " + Codeudores.identificacion);
        parame[52] = new ReportParameter("TIdentificacioncod", " " + Codeudores.tipo_identificacion);
        parame[53] = new ReportParameter("EstadoCivilCod", " " + Codeudores.estadocivil);
        parame[54] = new ReportParameter("EscolaridadCod", " " + Codeudores.escolaridad);
        parame[55] = new ReportParameter("DireccionCod", " " + Codeudores.direccion);
        parame[56] = new ReportParameter("BarrioCod", " " + Codeudores.barrio);
        parame[57] = new ReportParameter("TelCod", " " + Codeudores.telefono);
        parame[58] = new ReportParameter("TipoViviendaCod", " " + Codeudores.tipovivienda);
        parame[59] = new ReportParameter("NumPersCargoCod", " " + Codeudores.personascargo);
        parame[60] = new ReportParameter("ArrendadorCod", " " + Codeudores.arrendador);
        parame[61] = new ReportParameter("TelArrendadorCod", " " + Codeudores.telefonoarrendador);
        parame[62] = new ReportParameter("EmpresaCod", " " + Codeudores.empresa);
        parame[63] = new ReportParameter("CargoCod", " " + Codeudores.cargo);
        parame[64] = new ReportParameter("AntiguedadCod", " " + Codeudores.antiguedadempresa);
        parame[65] = new ReportParameter("ContratoCod", " " + Codeudores.tipocontrato);
        parame[66] = new ReportParameter("FechaExpediCod", " " + Codeudores.fechaexpedicion.ToShortDateString());
        parame[67] = new ReportParameter("CiudadExpeCod", " " + Codeudores.ciudadexpedicion);
        parame[68] = new ReportParameter("ValorArriendoCod", " " + Codeudores.valorarriendo);
        parame[69] = new ReportParameter("DirEmpresaCod", " " + Codeudores.direccionempresa);
        parame[70] = new ReportParameter("TelEmpresaCod", " " + Codeudores.telefonoempresa);
        // Analisis solicitud 
        parame[71] = new ReportParameter("Concepto", " " + creditos.concepto);
        parame[72] = new ReportParameter("Monto", " " + creditos.Monto.ToString("0,0", CultureInfo.InvariantCulture));
        parame[73] = new ReportParameter("Plazo", " " + creditos.Plazo);
        parame[74] = new ReportParameter("Cuota", " " + creditos.Cuota.ToString("0,0", CultureInfo.InvariantCulture));
        parame[75] = new ReportParameter("Opinion", " " + creditos.Observaciones);
        parame[76] = new ReportParameter("Ejecutivo", " " + creditos.Ejecutivo);
        parame[77] = new ReportParameter("Oficina", " " + creditos.Oficina);
        parame[78] = new ReportParameter("GarantiaReal", " " + creditos.garantiareal);
        parame[79] = new ReportParameter("GarantiaComunitaria", " " + creditos.garantiacom);
        parame[80] = new ReportParameter("Poliza", " " + creditos.poliza);
        // Conyuge Codeudor
        parame[81] = new ReportParameter("NombreConyuge", " " + conyugecod.primer_nombre + " " + conyugecod.segundo_nombre + " " + conyugecod.primer_apellido + " " + conyugecod.segundo_apellido);
        parame[82] = new ReportParameter("DocumentoConyuge", " " + conyugecod.tipoidentificacion + " " + conyugecod.identificacion);
        parame[83] = new ReportParameter("LugExpConyuge", " " + conyugecod.ciudadexpedicion);
        parame[84] = new ReportParameter("EmpreConyuge", " " + conyugecod.empresa);
        parame[85] = new ReportParameter("CargoEmpConyuge", " " + conyugecod.cargorepo);
        parame[86] = new ReportParameter("AntigEmpConyuge", " " + conyugecod.antiguedad_empresa);
        parame[87] = new ReportParameter("ContratoEmpConyuge", " " + conyugecod.tipocontrato);
        parame[88] = new ReportParameter("TelefEmpConyuge", " " + conyugecod.telefonoempresa);
        parame[89] = new ReportParameter("DireEmpConyuge", " " + conyugecod.direccion_empresa);
        parame[90] = new ReportParameter("CeluConyuge", " " + conyugecod.celular);
        parame[92] = new ReportParameter("nomUsuario", " " + User.nombre);
        parame[93] = new ReportParameter("ImagenReport", cRutaDeImagen);
        parame[94] = new ReportParameter("entidad", pUsu.empresa);

        ReportViewersolicitud.LocalReport.EnableExternalImages = true;
        ReportViewersolicitud.LocalReport.DataSources.Clear();
        ReportViewersolicitud.LocalReport.SetParameters(parame);

        ReportDataSource rds1 = new ReportDataSource("DataSet1", table2);
        ReportViewersolicitud.LocalReport.DataSources.Add(rds1);
        ReportDataSource rds2 = new ReportDataSource("DataSet2", CrearDataTablereferencias());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds2);
        ReportDataSource rds3 = new ReportDataSource("DataSet3", CrearDataTableCodeudores());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds3);
        ReportDataSource rds4 = new ReportDataSource("DataSet4", CrearDataTableFamiliares());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds4);
        ReportDataSource rds5 = new ReportDataSource("DataSet5", CrearDataTableVentasSemanales());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds5);
        ReportDataSource rds6 = new ReportDataSource("DataSet6", CrearDataTableVentasMensuales());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds6);
        ReportDataSource rds7 = new ReportDataSource("DataSet7", CrearDataTableMargenVentas());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds7);
        ReportDataSource rds8 = new ReportDataSource("DataSet8", CrearDataTableInfNegocio());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds8);
        ReportDataSource rds9 = new ReportDataSource("DataSet9", CrearDataTableInffinfamNegocio());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds9);
        ReportDataSource rds10 = new ReportDataSource("DataSet10", CrearDataTableInffinfamNegocioegresos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds10);
        ReportDataSource rds11 = new ReportDataSource("DataSet11", CrearDataTableActivos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds11);
        ReportDataSource rds12 = new ReportDataSource("DataSet12", CrearDataTablePasivos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds12);
        ReportDataSource rds13 = new ReportDataSource("DataSet13", CrearDataTableBalanceFamActivos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds13);
        ReportDataSource rds14 = new ReportDataSource("DataSet14", CrearDataTableBalanceFamPasivos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds14);
        ReportDataSource rds15 = new ReportDataSource("DataSet15", CrearDataTableComposicionPasivo());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds15);
        ReportDataSource rds16 = new ReportDataSource("DataSet16", CrearDataTableInvenACtivoFijos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds16);
        ReportDataSource rds17 = new ReportDataSource("DataSet17", CrearDataTableInvenMateriaPrima());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds17);
        ReportDataSource rds18 = new ReportDataSource("DataSet18", CrearDataTableProductosProceso());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds18);
        ReportDataSource rds19 = new ReportDataSource("DataSet19", CrearDataTableProductosTerminados());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds19);
        ReportDataSource rds20 = new ReportDataSource("DataSet20", CrearDataTableRelacionDocumentos());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds20);
        ReportDataSource rds21 = new ReportDataSource("DataSet21", CrearDataTableRelacionDocDesembolso());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds21);
        ReportDataSource rds22 = new ReportDataSource("DataSet22", CrearDataTableReldocControltiempo());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds22);
        ReportDataSource rds23 = new ReportDataSource("DataSet23", CrearDataTableBienesUnidadFamCode());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds23);
        ReportDataSource rds24 = new ReportDataSource("DataSet24", CrearDataTableVehiculosCode());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds24);
        ReportDataSource rds25 = new ReportDataSource("DataSet25", CrearDataTablePresupuestoEmpresarialCode());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds25);
        ReportDataSource rds26 = new ReportDataSource("DataSet26", CrearDataTablePresupuestoFamiliarCode());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds26);
        ReportDataSource rds27 = new ReportDataSource("DataSet27", CrearDataTableReferenciasCodeudor());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds27);
        ReportDataSource rds28 = new ReportDataSource("DataSet28", CrearDataTableViabilidadFinanciera());
        ReportViewersolicitud.LocalReport.DataSources.Add(rds28);

        ReportViewersolicitud.LocalReport.Refresh();
        mvLista.ActiveViewIndex = 2;
        framesolicitud.Visible = false;
    }
    protected void btnInforme4_Click(object sender, EventArgs e)
    {
        mvLista.ActiveViewIndex = 0;
    }
    protected void btnInforme5_Click(object sender, EventArgs e)
    {
        mvLista.ActiveViewIndex = 0;
    }
    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (ReportViewer2.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = ReportViewer2.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
            Usuario pUsuario = (Usuario)Session["usuario"];
            string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_" + pUsuario.nombre : "";
            FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
            FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            Session["Archivo" + Usuario.codusuario] = Server.MapPath("output" + pNomUsuario + ".pdf");
            frmPrint.Visible = true;
            ReportViewer2.Visible = false;

        }
    }
    protected void btnImprimirOrden_Click(object sender, EventArgs e)
    {
        Imprimir(1);
    }
    protected void btnInformeConsumo_Click(object sender, EventArgs e)
    {
        TiposDocumentoService tipoDocumentoServicio = new TiposDocumentoService();
        // Solicitando la información  del tipo de documento para saber si existe el tipo documento 0 
        TiposDocumento vTipoDoc = new TiposDocumento();
        TipoDocumento vTipoDo = new TipoDocumento();
        vTipoDo = tipoDocumentoServicio.ConsultarTipoDoc((Usuario)Session["usuario"]).FirstOrDefault(x => x.idTipo == "S");
        vTipoDoc = tipoDocumentoServicio.ConsultarTiposDocumento(0, (Usuario)Session["usuario"], vTipoDo.idTipo);
        string cDocumentoGenerado = Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/" + cDocsSubDir + "/" + "Solicitud" + txtNumRadic.Text.Trim() + "_" + '.' + 'p' + 'd' + 'f');
        if (vTipoDoc != null && Convert.ToInt64(vTipoDoc.tipo_documento) > 0)
        {
            // Solicitando la información que debe ser mostrada en el documento parametrizado 
            DatosDeDocumentoService datosDeDocumentoServicio = new DatosDeDocumentoService();
            List<DatosDeDocumento> lstDatosDeDocumento = new List<DatosDeDocumento>();
            lstDatosDeDocumento = datosDeDocumentoServicio.ListarDatosDeDocumentoFormato(Convert.ToInt64(this.txtNumRadic.Text), vTipoDoc.tipo_documento, (Usuario)Session["usuario"]);
            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                //using (FileStream fileStream = new FileStream(file.Name, FileMode.Open))
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/Documentos/"));
                foreach (string ficheroActual in ficherosCarpeta)
                {
                    using (StreamReader stream = new StreamReader(ficheroActual))
                    {
                        stream.Close();
                        System.IO.File.Delete(ficheroActual);
                    }
                }
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(creditoPlanServicio.CodigoPrograma, "btnInformeConsumo_Click", ex);
            }

            if (vTipoDoc.texto != null)
                ProcessesHTML.ReemplazarEnDocumentoDeWordYGuardarPdf(vTipoDoc.texto, lstDatosDeDocumento, cDocumentoGenerado, ref pError);
            else
                ProcessesHTML.ReemplazarEnDocumentoDeWordYGuardarPdf(Encoding.ASCII.GetString(vTipoDoc.Textos), lstDatosDeDocumento, cDocumentoGenerado, ref pError);
            if (!string.IsNullOrEmpty(pError))
                VerError(pError);


            //Descargando el Archivo PDF

            string cRutaLocalDeArchivoPDF = cDocumentoGenerado;
            FileInfo file = new FileInfo(cRutaLocalDeArchivoPDF);
            var sjs = File.ReadAllBytes(cRutaLocalDeArchivoPDF);
            Response.AddHeader("Content-Disposition", "attachment; filename= SolicitudCredito.pdf");
            Response.AddHeader("Content-Length", sjs.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(sjs);
            Response.Flush();
            Response.Close();
        }
        else
        {
            Persona1 vPersona1 = new Persona1();
            Persona1 vPersona2 = new Persona1();
            Persona1 vPersona3 = new Persona1();
            Referncias vPersona4 = new Referncias();
            Referencia refe = new Referencia();
            CreditoPlan creditos = new CreditoPlan();
            Persona1Service DatosClienteServicio = new Persona1Service();
            InformacionNegocio negocio = new InformacionNegocio();
            codeudores Codeudores = new codeudores();
            ConyugeService ConyugeCodeudorServicio = new ConyugeService();
            codeudoresService ServicioCodeudores = new codeudoresService();
            EstadosFinancierosService EstadosFinancierosServicio = new EstadosFinancierosService();
            BeneficiariosService beneficiarioservice = new BeneficiariosService();
            LineasCreditoService LineaCreditoServicio = new LineasCreditoService();
            CreditoSolicitadoService creditoSoliServicio = new CreditoSolicitadoService();
            List<Beneficiarios> beneficiario = new List<Beneficiarios>();

            Session["Identificacion"] = txtIdentific.Text;
            negocio = DatosClienteServicio.Consultardatosnegocio(Convert.ToInt64(txtNumRadic.Text), Session["Identificacion"].ToString(), (Usuario)Session["Usuario"]);

            Codeudores = ServicioCodeudores.ConsultarDatosCodeudorRepo(Convert.ToString(txtNumRadic.Text), (Usuario)Session["usuario"]);
            Session["Codeudores"] = Codeudores.codpersona;

            creditos = DatosClienteServicio.ConsultarPersona1Paramcred(Convert.ToInt64(txtNumRadic.Text), Session["Identificacion"].ToString(), (Usuario)Session["Usuario"]);
            try
            {
                beneficiario = beneficiarioservice.ConsultarBeneficiariosAUXILIOS(Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["usuario"]);
            }
            catch
            {
                VerError("");
            }
            mvLista.ActiveViewIndex = 1;

            DataTable Beneficiario = new DataTable();

            if (beneficiario.Count > 0)
            {

                Beneficiario.Columns.Add("Nombres");
                Beneficiario.Columns.Add("Identificacion");

                foreach (Beneficiarios datos in beneficiario)
                {
                    DataRow parametrosbeneficiario;
                    parametrosbeneficiario = Beneficiario.NewRow();
                    parametrosbeneficiario[0] = datos.nombres;
                    parametrosbeneficiario[1] = datos.identificacion;
                    Beneficiario.Rows.Add(parametrosbeneficiario);
                }
            }
            else
            {
                Beneficiario.Columns.Add("Nombres");
                Beneficiario.Columns.Add("Identificacion");

                DataRow parametrosbeneficiarios;
                parametrosbeneficiarios = Beneficiario.NewRow();
                parametrosbeneficiarios[0] = " ";
                parametrosbeneficiarios[1] = " ";
                Beneficiario.Rows.Add(parametrosbeneficiarios);
            }

            mvLista.ActiveViewIndex = 1;

            List<Persona1> resultado = new List<Persona1>();
            List<Referencia> referencias = new List<Referencia>();
            Conyuge conyugecod = new Conyuge();
            vPersona1.numeroRadicacion = Convert.ToInt64(txtNumRadic.Text);
            if (Session["Cod_persona"] != null)
                vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            EstadosFinancieros InformacionFinanciera = InformacionFinanciera = EstadosFinancierosServicio.listarperosnainfofin(vPersona1.cod_persona, (Usuario)Session["usuario"]);
            resultado = DatosClienteServicio.ListadoPersonas1Reporte(vPersona1, (Usuario)Session["usuario"]);
            referencias = DatosClienteServicio.referencias(vPersona1, Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["Usuario"]);
            Session["Codeudores"] = Codeudores.codpersona;
            conyugecod = ConyugeCodeudorServicio.ConsultarRefConyugeRepo(Codeudores.codpersona, (Usuario)Session["Usuario"]);

            int index = resultado.FindIndex(x => x.cod_persona == Convert.ToInt64(Session["Cod_persona"].ToString()));

            //CONSULTAR LA LINEA DE CREDITO
            LineasCredito eLinea = new LineasCredito();
            eLinea = LineaCreditoServicio.ConsultarLineasCredito(txtLinea.Text, (Usuario)Session["Usuario"]);
            string universidad = "", semestre = "";
            if (eLinea.credito_educativo == 1)
            {
                Credito credito = new Credito();
                credito = CreditoServicio.ConsultarCreditoAsesor(Int64.Parse(txtNumRadic.Text), (Usuario)Session["usuario"]);
                universidad = credito.universidad != null ? credito.universidad : "";
                semestre = credito.semestre != null ? credito.semestre : "";
            }

            ReportParameter[] parame = new ReportParameter[111];

            DataTable table = new DataTable();
            table.Columns.Add("nombre");
            table.Columns.Add("identificacion");
            table.Columns.Add("telefono");
            table.Columns.Add("direccion");
            table.Columns.Add("nombrer");
            table.Columns.Add("tipor");
            table.Columns.Add("parentesco");
            table.Columns.Add("direccionr");
            table.Columns.Add("telefonor");

            DataRow datarw;

            for (int i = 0; i < resultado.Count; i++)
            {
                datarw = table.NewRow();
                if (index == i)
                    vPersona1 = resultado[i];
                else
                    vPersona2 = resultado[i];
                if (i >= 2)
                {
                    vPersona2 = resultado[i];

                    datarw[0] = vPersona2.primer_nombre + " " + vPersona2.segundo_nombre + " " + vPersona2.primer_apellido + " " + vPersona2.segundo_apellido;
                    datarw[1] = vPersona3.cod_persona;
                    datarw[2] = vPersona2.direccion;
                    datarw[3] = vPersona2.telefono;
                    datarw[4] = "";
                    datarw[5] = "";
                    datarw[6] = "";
                    datarw[7] = "";
                    datarw[8] = "";
                    table.Rows.Add(datarw);
                }
                if (txtNumRadic.Text == "" || Convert.ToString(resultado[i].identificacion) == "")
                { }


            }
            if (resultado.Count < 3)
            {

                parame[91] = new ReportParameter("datosvacios", "NO SE ENCONTRARON DATOS PARA ESTE ITEM");

                datarw = table.NewRow();
                datarw[0] = "";
                datarw[1] = "";
                datarw[2] = "";
                datarw[3] = "";
                datarw[4] = "";
                datarw[5] = "";
                datarw[6] = "";
                datarw[7] = "";
                datarw[8] = "";
                table.Rows.Add(datarw);
            }
            else
                parame[90] = new ReportParameter("datosvacios", " ");

            Cardinalidad numero = new Cardinalidad();
            string cardinal = " ";
            string valor = creditos.Monto.ToString();
            if (valor != "0")
            {
                cardinal = numero.enletras(valor.Replace(".", ""));
                int cont = cardinal.Length - 1;
                int cont2 = cont - 7;
                if (cont2 >= 0)
                {
                    string c = cardinal.Substring(cont2);
                    if (cardinal.Substring(cont2) == "MILLONES" || cardinal.Substring(cont2 + 2) == "MILLON")
                        cardinal = cardinal + " DE PESOS M/CTE";
                    else
                        cardinal = cardinal + " PESOS M/CTE";
                }
            }

            parame[0] = new ReportParameter("Nombres", " " + vPersona1.primer_nombre + " " + vPersona1.segundo_nombre);
            parame[1] = new ReportParameter("Identificación", " " + vPersona1.identificacion);
            parame[2] = new ReportParameter("Fecha_nacimiento", " " + vPersona1.fechanacimiento.Value.ToShortDateString());
            parame[3] = new ReportParameter("Nivel_Estudio", " ");
            parame[4] = new ReportParameter("Telefono", " " + vPersona1.telefono);
            parame[5] = new ReportParameter("Apellidos", " " + vPersona1.primer_apellido + " " + vPersona1.segundo_apellido);
            parame[6] = new ReportParameter("Lugar_Expedicion", " " + vPersona1.ciudadexpedicion);
            parame[7] = new ReportParameter("Sexo", " " + vPersona1.sexo);
            parame[8] = new ReportParameter("mail", " " + vPersona1.email);
            parame[9] = new ReportParameter("direccion", " " + vPersona1.direccion);
            //CONYUGE
            parame[10] = new ReportParameter("IdentificaciónConyuge", " " + vPersona2.tipo_identif + " " + vPersona2.identificacion);
            parame[11] = new ReportParameter("TelefonoConyuge", " " + vPersona2.telefono);
            parame[12] = new ReportParameter("ApellidosConyuge", " " + vPersona2.primer_apellido + " " + vPersona2.segundo_apellido);
            parame[13] = new ReportParameter("SexoConyuge", " " + vPersona2.sexo);
            parame[14] = new ReportParameter("mailConyuge", " " + vPersona2.email);
            parame[15] = new ReportParameter("NombresConyuge", " " + vPersona2.primer_nombre + " " + vPersona2.segundo_nombre);
            parame[16] = new ReportParameter("DireccionConyuge", " " + vPersona2.direccion);
            parame[17] = new ReportParameter("EmpresaConyuge", " " + vPersona2.empresa);
            parame[18] = new ReportParameter("ContratoConyuge", " " + vPersona2.tipocontrato);
            parame[19] = new ReportParameter("AntiguedadEmpConyuge", " " + vPersona2.antiguedadlugarempresa);
            parame[20] = new ReportParameter("CelularConyuge", " " + vPersona2.celular);
            parame[21] = new ReportParameter("CelularEmpConyuge", " " + vPersona2.CelularEmpresa);
            //CREDITO
            parame[22] = new ReportParameter("MontoSolicitado", " " + creditos.Monto.ToString("0,0", CultureInfo.InvariantCulture));
            parame[23] = new ReportParameter("NumeroSolicitud", " " + creditos.Numero_Obligacion);
            parame[24] = new ReportParameter("NumerodeCredito", " " + creditos.Numero_radicacion);
            parame[25] = new ReportParameter("LineadeCredito", " " + creditos.LineaCredito);
            parame[26] = new ReportParameter("ValorCuota", " " + creditos.Cuota.ToString("0,0", CultureInfo.InvariantCulture));
            parame[27] = new ReportParameter("FechaSolicitud", " " + creditos.FechaSolicitud.ToShortDateString());
            //NEGOCIO
            parame[28] = new ReportParameter("nombrenegocio", " " + negocio.nombrenegocio);
            parame[29] = new ReportParameter("descripcionnegocio", " " + negocio.descripcion);
            parame[30] = new ReportParameter("direccionnegocio", " " + negocio.direccion);
            parame[31] = new ReportParameter("telefononegocio", " " + negocio.telefono);
            string fecha_expedicion = vPersona1.fechaexpedicion != null && vPersona1.fechaexpedicion != DateTime.MinValue ? vPersona1.fechaexpedicion.Value.ToShortDateString() : "";
            parame[32] = new ReportParameter("Fecha_Expedicion", " " + fecha_expedicion);
            parame[33] = new ReportParameter("Medio", " " + creditos.Medio);
            //persona
            parame[34] = new ReportParameter("TipoVivienda", " " + creditos.tipo_propiedad);
            parame[35] = new ReportParameter("ArrendadorViv", " " + creditos.arrendador);
            parame[36] = new ReportParameter("AntiguedadLugar", " " + creditos.antiguedad);
            parame[37] = new ReportParameter("TelArrendador", " " + creditos.telefonoarren);
            parame[38] = new ReportParameter("ValorArriendo", " " + creditos.valorarriendo);
            parame[39] = new ReportParameter("EstadoCivil", " " + creditos.EstadoCivil);
            // negocio
            parame[40] = new ReportParameter("Propiedad", " " + negocio.tipo_propiedad);
            parame[41] = new ReportParameter("Arrendador", " " + negocio.arrendador);
            parame[42] = new ReportParameter("TelefonoArrendador", " " + negocio.telefonoarrendador);
            parame[43] = new ReportParameter("Arriendo", " " + negocio.valor_arriendo);
            parame[44] = new ReportParameter("Experiencia", " " + negocio.experiencia);
            parame[45] = new ReportParameter("Antiguedad", " " + negocio.antiguedad);
            parame[46] = new ReportParameter("EmpleadosPerm", " " + negocio.emplperm);
            parame[47] = new ReportParameter("EmpleadosTemp", " " + negocio.empltem);
            parame[48] = new ReportParameter("Barrio", " " + negocio.barrioneg);
            parame[49] = new ReportParameter("Actividad", " " + negocio.descactividad);
            // codeudores
            parame[50] = new ReportParameter("NomCodeudor", " " + Codeudores.primer_nombre + " " + Codeudores.segundo_nombre + " " + Codeudores.primer_apellido + " " + Codeudores.segundo_apellido);
            parame[51] = new ReportParameter("IdentificacionCod", " " + Codeudores.identificacion);
            parame[52] = new ReportParameter("TIdentificacioncod", " " + Codeudores.tipo_identificacion);
            parame[53] = new ReportParameter("EstadoCivilCod", " " + Codeudores.estadocivil);
            parame[54] = new ReportParameter("EscolaridadCod", " " + Codeudores.escolaridad);
            parame[55] = new ReportParameter("DireccionCod", " " + Codeudores.direccion);
            parame[56] = new ReportParameter("BarrioCod", " " + Codeudores.barrio);
            parame[57] = new ReportParameter("TelCod", " " + Codeudores.telefono);
            parame[58] = new ReportParameter("TipoViviendaCod", " " + Codeudores.tipovivienda);
            parame[59] = new ReportParameter("NumPersCargoCod", " " + Codeudores.personascargo);
            parame[60] = new ReportParameter("ArrendadorCod", " " + Codeudores.arrendador);
            parame[61] = new ReportParameter("TelArrendadorCod", " " + Codeudores.telefonoarrendador);
            parame[62] = new ReportParameter("EmpresaCod", " " + Codeudores.empresa);
            parame[63] = new ReportParameter("CargoCod", " " + Codeudores.cargo);
            parame[64] = new ReportParameter("AntiguedadCod", " " + Codeudores.antiguedadempresa);
            parame[65] = new ReportParameter("ContratoCod", " " + Codeudores.tipocontrato);
            string fec_expediCod = Codeudores.fechaexpedicion != null && Codeudores.fechaexpedicion != DateTime.MinValue ? Codeudores.fechaexpedicion.ToShortDateString() : "";
            parame[66] = new ReportParameter("FechaExpediCod", " " + fec_expediCod);
            parame[67] = new ReportParameter("CiudadExpeCod", " " + Codeudores.ciudadexpedicion);
            parame[68] = new ReportParameter("ValorArriendoCod", " " + Codeudores.valorarriendo);
            parame[69] = new ReportParameter("DirEmpresaCod", " " + Codeudores.direccionempresa);
            parame[70] = new ReportParameter("TelEmpresaCod", " " + Codeudores.telefonoempresa);

            // Analisis solicitud 
            parame[71] = new ReportParameter("Concepto", " " + creditos.concepto);
            parame[72] = new ReportParameter("Monto", " " + creditos.Monto.ToString("0,0", CultureInfo.InvariantCulture));
            parame[73] = new ReportParameter("Plazo", " " + creditos.Plazo);
            parame[74] = new ReportParameter("Cuota", " " + creditos.Cuota.ToString("0,0", CultureInfo.InvariantCulture));
            parame[75] = new ReportParameter("Opinion", " " + creditos.Observaciones);
            parame[76] = new ReportParameter("Ejecutivo", " " + creditos.Ejecutivo);
            parame[77] = new ReportParameter("Oficina", " " + creditos.Oficina);
            parame[78] = new ReportParameter("GarantiaReal", " " + creditos.garantiareal);
            parame[79] = new ReportParameter("GarantiaComunitaria", " " + creditos.garantiacom);
            parame[80] = new ReportParameter("Poliza", " " + creditos.poliza);

            Usuario user = (Usuario)Session["usuario"];
            // Conyuge Codeudor
            parame[81] = new ReportParameter("NombreConyuge", " " + conyugecod.primer_nombre + " " + conyugecod.segundo_nombre + " " + conyugecod.primer_apellido + " " + conyugecod.segundo_apellido);
            parame[82] = new ReportParameter("DocumentoConyuge", " " + conyugecod.tipoidentificacion + " " + conyugecod.identificacion);
            parame[83] = new ReportParameter("LugExpConyuge", " " + conyugecod.ciudadexpedicion);
            parame[84] = new ReportParameter("EmpreConyuge", " " + conyugecod.empresa);
            parame[85] = new ReportParameter("CargoEmpConyuge", " " + conyugecod.cargorepo);
            parame[86] = new ReportParameter("AntigEmpConyuge", " " + conyugecod.antiguedad_empresa);
            parame[87] = new ReportParameter("ContratoEmpConyuge", " " + conyugecod.tipocontrato);
            parame[88] = new ReportParameter("TelefEmpConyuge", " " + conyugecod.telefonoempresa);
            parame[89] = new ReportParameter("DireEmpConyuge", " " + conyugecod.direccion_empresa);
            parame[90] = new ReportParameter("CeluConyuge", " " + conyugecod.celular);
            parame[92] = new ReportParameter("nomUsuario", " " + user.nombre);


            string cRutaDeImagen;
            cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";
            Usuario pUsu = (Usuario)Session["usuario"];

            parame[93] = new ReportParameter("ImagenReport", cRutaDeImagen);
            parame[94] = new ReportParameter("entidad", pUsu.empresa);
            //Datos Solicitantes
            int days = Convert.ToDateTime(vPersona1.fechanacimiento).Day;
            int mhont = Convert.ToDateTime(vPersona1.fechanacimiento).Month;
            int years = Convert.ToDateTime(vPersona1.fechanacimiento).Year;

            DateTime nacimiento = new DateTime(years, mhont, days); //echa de nacimiento
            int edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;

            int edadEnDias = ((TimeSpan)(DateTime.Now - Convert.ToDateTime(vPersona1.fechanacimiento))).Days;
            parame[95] = new ReportParameter("Periodicidad", " " + txtPeriodicidad.Text);
            parame[96] = new ReportParameter("edad", " " + edad);
            parame[97] = new ReportParameter("celular1", " " + vPersona1.celular);
            parame[98] = new ReportParameter("celular2", " " + "");
            parame[99] = new ReportParameter("celular3", " " + "");//Aportes/personal/Nuevo ruta para Informacion 
            parame[100] = new ReportParameter("personaacargo", " " + vPersona1.PersonasAcargo);

            // Informacion laboral 
            parame[101] = new ReportParameter("empresa", " " + creditos.empresa);
            parame[102] = new ReportParameter("direccionLabor", " " + vPersona1.direccionempresa);
            parame[103] = new ReportParameter("AntiguedadMes", " " + vPersona1.antiguedadLaboral);
            parame[104] = new ReportParameter("SueldoLabor", " " + Convert.ToDecimal(vPersona1.salario).ToString("##,##"));
            parame[105] = new ReportParameter("otrosIngresos", " " + InformacionFinanciera.otrosingresos);
            parame[106] = new ReportParameter("totalIngresos", " " + InformacionFinanciera.totalingreso);
            parame[107] = new ReportParameter("totalEgresos", " " + InformacionFinanciera.totalegresos);
            parame[108] = new ReportParameter("ValorAletras", " " + cardinal);
            parame[109] = new ReportParameter("Universidad", " " + universidad);
            parame[110] = new ReportParameter("Semestre", " " + semestre);

            ReportViewersolicitud.LocalReport.EnableExternalImages = true;
            ReportViewersolicitud.LocalReport.DataSources.Clear();
            ReportViewersolicitud.LocalReport.ReportPath = "Page/FabricaCreditos/Solicitud/PlanPagos/ReportConsumo.rdlc";
            ReportViewersolicitud.LocalReport.SetParameters(parame);

            ReportDataSource rds1 = new ReportDataSource("DataSet1", Beneficiario);
            ReportViewersolicitud.LocalReport.DataSources.Add(rds1);

            ReportDataSource rds2 = new ReportDataSource("DataSet2", CrearDataTablereferencias());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds2);

            ReportDataSource rds3 = new ReportDataSource("DataSet3", CrearDataTableIngresosEgresos());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds3);

            ReportDataSource rds4 = new ReportDataSource("DataSet4", CrearDataTableRelacionDocumentos());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds4);

            ReportDataSource rds5 = new ReportDataSource("DataSet5", CrearDataTableRelacionDocDesembolso());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds5);

            ReportDataSource rds6 = new ReportDataSource("DataSet6", CrearDataTableReldocControltiempo());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds6);

            ReportDataSource rds7 = new ReportDataSource("DataSet7", CrearDataTableComposicionPasivo());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds7);

            ReportDataSource rds8 = new ReportDataSource("DataSet8", CrearDataTableVehiculos());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds8);

            ReportDataSource rds9 = new ReportDataSource("DataSet9", CrearDataTableBienes());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds9);

            ReportDataSource rds10 = new ReportDataSource("DataSet10", CrearDataTableBienesUnidadFamCode());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds10);

            ReportDataSource rds11 = new ReportDataSource("DataSet11", CrearDataTableVehiculosCode());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds11);

            ReportDataSource rds12 = new ReportDataSource("DataSet12", CrearDataTablePresupuestoEmpresarialCode());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds12);

            ReportDataSource rds13 = new ReportDataSource("DataSet13", CrearDataTablePresupuestoFamiliarCode());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds13);

            ReportDataSource rds14 = new ReportDataSource("DataSet14", CrearDataTableReferenciasCodeudor());
            ReportViewersolicitud.LocalReport.DataSources.Add(rds14);

            mvLista.ActiveViewIndex = 2;
            framesolicitud.Visible = false;
        }


    }
    /// <summary>
    /// Esta función corresponde al botón de imprimir y permite generar el reporte del plan de pagos
    /// </summary>
    protected void btnInformePlan_Click(object sender, EventArgs e)
    {
        if (Session["AtributosPlanPagos"] == null)
            return;
        if (Session["PlanPagos"] == null)
            return;
        if (Session["DescuentosPlan"] == null)
            return;

        //----------------------------------------------------------------------------------------------------------
        //Traer los datos de interes, mora, corriente y otros
        //----------------------------------------------------------------------------------------------------------        
        //List<Xpinn.FabricaCreditos.Entities.Credito> lstConsultase = new List<Xpinn.FabricaCreditos.Entities.Credito>();

        // ---------------------------------------------------------------------------------------------------------
        // Traer listado de crèditos recogidos
        // ---------------------------------------------------------------------------------------------------------
        CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
        List<CreditoRecoger> lstConsulta = new List<CreditoRecoger>();
        CreditoRecoger refe = new CreditoRecoger();
        lstConsulta = creditoRecogerServicio.ConsultarCreditoRecoger(Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["usuario"]);

        // LLenar data table con los datos a recoger
        DataTable table = new DataTable();
        table.Columns.Add("numero");
        table.Columns.Add("linea");
        table.Columns.Add("monto");
        table.Columns.Add("tasa");
        table.Columns.Add("saldo");
        table.Columns.Add("total");
        table.Columns.Add("Int_cte");
        table.Columns.Add("Int_Mora");
        table.Columns.Add("Otros");

        DataRow datarw;
        if (lstConsulta.Count == 0)
        {
            datarw = table.NewRow();
            datarw[0] = " ";
            datarw[1] = " ";
            datarw[2] = " ";
            datarw[3] = " ";
            datarw[4] = " ";
            datarw[5] = "0";
            datarw[6] = " ";
            datarw[7] = " ";
            datarw[8] = " ";
            table.Rows.Add(datarw);
        }
        else
        {
            for (int i = 0; i < lstConsulta.Count; i++)
            {
                datarw = table.NewRow();
                refe = lstConsulta[i];
                datarw[0] = " " + refe.numero_radicacion;
                DateTime fecha = DateTime.Now;

                decimal intcorriente = 0, intmora = 0, intotros = 0;
                Credito referea = new Credito();
                if (refe.numero_radicacion.ToString() != "" && refe.numero_radicacion != 0)
                {
                    CreditoService creditoservicio = new CreditoService();
                    try
                    {
                        referea = creditoservicio.consultarinterescredito(refe.numero_radicacion, fecha, (Usuario)Session["usuario"], Convert.ToInt64(txtNumRadic.Text));
                        intcorriente = referea.intcoriente;
                        intmora = referea.interesmora;
                        intotros = referea.otros;
                    }
                    catch
                    {
                        intcorriente = 0;
                        intmora = 0;
                        intotros = 0;
                    }
                }

                refe.valor_recoge = refe.saldo_capital + intcorriente + intmora + intotros;

                datarw[1] = " " + refe.linea_credito;
                datarw[2] = " " + refe.monto.ToString("0,0");
                datarw[3] = " " + refe.interes_corriente.ToString("0,0");
                datarw[4] = " " + referea.saldo_capital.ToString("0,0");
                datarw[5] = " " + refe.valor_recoge.ToString("0,0");
                datarw[6] = " " + intcorriente.ToString("0,0");
                datarw[7] = " " + intmora.ToString("0,0");
                datarw[8] = " " + intotros.ToString("0,0");

                table.Rows.Add(datarw);
            }
        }


        SolicitudCredServRecogidosService serviciosRecogidosServices = new SolicitudCredServRecogidosService();
        List<SolicitudCredServRecogidos> listaServicios = serviciosRecogidosServices.ListarSolicitudCredServRecogidosActualizado(Convert.ToInt64(txtNumRadic.Text), Usuario);

        if (listaServicios.Count >= 1)
        {
            foreach (SolicitudCredServRecogidos drFila in listaServicios)
            {
                datarw = table.NewRow();
                datarw[0] = " " + drFila.numeroservicio;
                datarw[1] = " " + drFila.nom_linea;
                datarw[2] = " " + drFila.valor_cuota.ToString("0,0");
                datarw[3] = " " + "0";
                datarw[4] = " " + "0";
                datarw[5] = " " + drFila.valorrecoger.ToString("0,0");
                datarw[6] = " " + "0";
                datarw[7] = " " + "0";
                datarw[8] = " " + "0";
                table.Rows.Add(datarw);
            }
        }


        // ---------------------------------------------------------------------------------------------------------
        // Traer los datos de los codeudores
        // ---------------------------------------------------------------------------------------------------------
        Persona1Service Persona1Servicio = new Persona1Service();
        Persona1 refere = new Persona1();
        List<Persona1> lstConsultas = new List<Persona1>();
        lstConsultas = Persona1Servicio.ListarPersona1(ObtenerValoresCodeudores(), (Usuario)Session["usuario"]);
        lstConsultas = lstConsultas.OrderBy(x => x.orden).ToList();
        // LLenar la tabla con datos de codeudores
        DataTable table2 = new DataTable();
        table2.Columns.Add("codigo");
        table2.Columns.Add("identificacion");
        table2.Columns.Add("tipo");
        table2.Columns.Add("nombres");
        table2.Columns.Add("empresa");
        table2.Columns.Add("direccion");
        DataRow datarw2;
        if (lstConsultas.Count == 0)
        {
            datarw2 = table2.NewRow();
            datarw2[0] = " ";
            datarw2[1] = " ";
            datarw2[2] = " ";
            datarw2[3] = " ";
            datarw2[4] = " ";
            datarw2[5] = " ";
            table2.Rows.Add(datarw2);
        }
        else
        {
            for (int i = 0; i < lstConsultas.Count; i++)
            {
                datarw2 = table2.NewRow();
                refere = lstConsultas[i];
                datarw2[0] = " " + refere.cod_persona;
                datarw2[1] = " " + refere.identificacion;
                datarw2[2] = " " + refere.tipo_identificacion;
                datarw2[3] = " " + refere.primer_nombre + " " + refere.segundo_nombre + " " + refere.primer_apellido + " " + refere.segundo_apellido;
                datarw2[4] = " " + refere.empresa;
                datarw2[5] = " " + refere.direccionempresa;
                table2.Rows.Add(datarw2);

            }
        }


        // ---------------------------------------------------------------------------------------------------------
        // Traer los datos de las cuotas extras
        // ---------------------------------------------------------------------------------------------------------

        List<CuotasExtras> lstConsulta1 = new List<CuotasExtras>();
        CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
        String Numero_solicitud = Session["NumeroSolicitud"].ToString();
        if (Numero_solicitud != "")
        {
            CuotasExtras cuotas = new CuotasExtras();
            cuotas.numero_radicacion = Convert.ToInt64(txtNumRadic.Text);
            lstConsulta1 = CuoExtServicio.ListarCuotasExtras(cuotas, (Usuario)Session["Usuario"]);

        }
        DataTable table3 = new DataTable();
        table3.Columns.Add("fecha_pago");
        table3.Columns.Add("valor");
        table3.Columns.Add("des_forma_pago");
        table3.Columns.Add("des_tipo_cuota");
        DataRow datarw3;
        if (lstConsulta1.Count == 0)
        {
            datarw3 = table3.NewRow();
            datarw3[0] = " ";
            datarw3[1] = " ";
            datarw3[2] = " ";
            datarw3[3] = " ";
            table3.Rows.Add(datarw3);
        }
        else
        {
            foreach (CuotasExtras referen in lstConsulta1)
            {
                datarw3 = table3.NewRow();
                DateTime fecha = Convert.ToDateTime(referen.fecha_pago);
                Int64 valor = Convert.ToInt64(referen.valor);
                datarw3[0] = " " + fecha.ToString("d");
                datarw3[1] = " " + valor.ToString("0,0", CultureInfo.InvariantCulture);
                if (referen.forma_pago == "1")
                {
                    datarw3[2] = " Caja";
                }
                else
                {
                    datarw3[2] = " Nomina";
                }
                datarw3[3] = " " + referen.des_tipo_cuota;
                table3.Rows.Add(datarw3);
            }
        }


        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos Deducciones
        //
        DataTable table4 = new DataTable();
        table4.Columns.Add("Numero");
        table4.Columns.Add("Descripcion");
        table4.Columns.Add("Valor");
        DataRow datarw4;
        List<DescuentosCredito> lstDes = new List<DescuentosCredito>();
        lstDes = (List<DescuentosCredito>)Session["DescuentosPlan"];
        int f = 0;
        foreach (DescuentosCredito item in lstDes)
        {
            if (item.val_atr != null)
            {
                datarw4 = table4.NewRow();
                datarw4[0] = item.cod_atr;
                datarw4[1] = item.nom_atr;
                datarw4[2] = item.val_atr;
                table4.Rows.Add(datarw4);
            }

        }

        //datos Atributos Tasas
        DataTable table5 = new DataTable();
        table5.Columns.Add("CodAtr");
        table5.Columns.Add("NomAtr");
        table5.Columns.Add("ValorAtr");
        DataRow datarw5;
        List<LineasCredito> lstAtributos = CreditoServicio.ListarAtributosFinanciados(Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["Usuario"]);
        foreach (LineasCredito item in lstAtributos.Where(x => x.cod_atr != 2 && x.cod_atr != 3))
        {
            datarw5 = table5.NewRow();
            datarw5[0] = item.cod_atr;
            datarw5[1] = item.descripcion;
            datarw5[2] = item.tasa;
            table5.Rows.Add(datarw5);
        }

        // ---------------------------------------------------------------------------------------------------------
        // Pasar datos al reporte
        // ---------------------------------------------------------------------------------------------------------
        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

        Usuario User = (Usuario)Session["usuario"];
        ReportParameter[] param = new ReportParameter[59];

        param[0] = new ReportParameter("Entidad", User.empresa);
        param[1] = new ReportParameter("numero_radicacion", txtNumRadic.Text);
        param[2] = new ReportParameter("cod_linea_credito", txtLinea.Text);
        param[3] = new ReportParameter("linea", txtNombreLinea.Text);
        param[4] = new ReportParameter("nombre", txtNombre.Text);
        param[5] = new ReportParameter("identificacion", txtIdentific.Text);
        param[6] = new ReportParameter("direccion", Txtdireccion.Text);
        param[7] = new ReportParameter("ciudad", Txtciudad.Text);
        param[8] = new ReportParameter("fecha_inico", txtFechaInicial.Text);
        param[9] = new ReportParameter("fecha_primer", Txtprimerpago.Text);
        param[10] = new ReportParameter("palzo", txtPlazo.Text);
        param[11] = new ReportParameter("fecha_generacion", Txtgeneracion.Text);
        param[12] = new ReportParameter("periocidad", txtPeriodicidad.Text);
        param[13] = new ReportParameter("cuotas", Txtcuotas.Text);
        param[14] = new ReportParameter("valor_cuota", txtCuota.Text);
        param[15] = new ReportParameter("forma_pago", txtFormaPago.Text);
        param[16] = new ReportParameter("tasa_nominal", txtTasaInteres.Text);
        param[17] = new ReportParameter("tasa_efectiva", Txtinteresefectiva.Text);
        decimal pVr = Convert.ToDecimal(Session["VrDesembolso"].ToString());
        param[18] = new ReportParameter("desembolso", pVr.ToString());
        param[19] = new ReportParameter("pagare", Txtpagare.Text);


        List<Atributos> lstAtr = new List<Atributos>();
        lstAtr = (List<Atributos>)Session["AtributosPlanPagos"];
        int j = 0;
        foreach (Atributos item in lstAtr)
        {
            param[20 + j] = new ReportParameter("titulo" + j, item.nom_atr);
            j = j + 1;
        }
        for (int i = j; i < 15; i++)
        {
            param[20 + i] = new ReportParameter("titulo" + i, " ");
        }
        List<DatosPlanPagos> lstPlan = new List<DatosPlanPagos>();
        lstPlan = (List<DatosPlanPagos>)Session["PlanPagos"];
        Boolean[] bVisible = new Boolean[16];
        for (int i = 1; i <= 15; i++)
        {
            bVisible[i] = false;
            i = i + 1;
        }
        foreach (DatosPlanPagos ItemPlanPagos in lstPlan)
        {
            if (ItemPlanPagos.int_1 != 0) { bVisible[1] = true; }
            if (ItemPlanPagos.int_2 != 0) { bVisible[2] = true; }
            if (ItemPlanPagos.int_3 != 0) { bVisible[3] = true; }
            if (ItemPlanPagos.int_4 != 0) { bVisible[4] = true; }
            if (ItemPlanPagos.int_5 != 0) { bVisible[5] = true; }
            if (ItemPlanPagos.int_6 != 0) { bVisible[6] = true; }
            if (ItemPlanPagos.int_7 != 0) { bVisible[7] = true; }
            if (ItemPlanPagos.int_8 != 0) { bVisible[8] = true; }
            if (ItemPlanPagos.int_9 != 0) { bVisible[9] = true; }
            if (ItemPlanPagos.int_10 != 0) { bVisible[10] = true; }
            if (ItemPlanPagos.int_11 != 0) { bVisible[11] = true; }
            if (ItemPlanPagos.int_12 != 0) { bVisible[12] = true; }
            if (ItemPlanPagos.int_13 != 0) { bVisible[13] = true; }
            if (ItemPlanPagos.int_14 != 0) { bVisible[14] = true; }
            if (ItemPlanPagos.int_15 != 0) { bVisible[15] = true; }
        }
        for (int i = 0; i < 15; i++)
        {
            param[35 + i] = new ReportParameter("visible" + i, bVisible[i + 1].ToString());
        }
        param[50] = new ReportParameter("nomUsuario", User.nombre);
        param[51] = new ReportParameter("ImagenReport", cRutaDeImagen);
        param[52] = new ReportParameter("fecha_solicitud", txtFechaSolicitud.Text);
        if (txtFechaDesembolso.Text.Trim() == "")
            param[53] = new ReportParameter("fecha_desembolso", DateTime.Now.ToString(gFormatoFecha));
        else
            param[53] = new ReportParameter("fecha_desembolso", txtFechaDesembolso.Text);

        param[54] = new ReportParameter("Fecha_aprobacion", " " + TxtFechaApro.Text);
        param[55] = new ReportParameter("estado", " " + " " + txtEstado.Text);
        param[56] = new ReportParameter("tasa_mensual", " " + txtTasaPeriodica.Text);
        decimal pVrAprobado = Convert.ToDecimal(Session["ValorMonto"].ToString());
        param[57] = new ReportParameter("monto_aprobado", pVrAprobado.ToString());
        valorParametro = ConsultarParametroGeneral(981, (Usuario)Session["usuario"]);
        var TasaUsura = ViewState["HistoricoTasa"] == null
            ? "0" : ViewState["HistoricoTasa"].ToString();
        if (valorParametro.valor == "1")
            param[58] = new ReportParameter("TasaUsura", TasaUsura);
        else
            param[58] = new ReportParameter("TasaUsura", TasaUsura);



        ReportViewerPlan.LocalReport.EnableExternalImages = true;
        ReportViewerPlan.LocalReport.SetParameters(param);

        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        ReportDataSource rds2 = new ReportDataSource("DataSet2", table2);
        ReportDataSource rds3 = new ReportDataSource("DataSet3", table3);
        ReportDataSource rds4 = new ReportDataSource("DataSet4", table4);
        ReportDataSource rds5 = new ReportDataSource("DataSet5", table5);
        ReportDataSource rds = new ReportDataSource("DataSetPlanPagos", CrearDataTable(idObjeto));

        ReportViewerPlan.LocalReport.DataSources.Clear();
        ReportViewerPlan.LocalReport.DataSources.Add(rds);
        ReportViewerPlan.LocalReport.DataSources.Add(rds1);
        ReportViewerPlan.LocalReport.DataSources.Add(rds2);
        ReportViewerPlan.LocalReport.DataSources.Add(rds3);
        ReportViewerPlan.LocalReport.DataSources.Add(rds4);
        ReportViewerPlan.LocalReport.DataSources.Add(rds5);
        ReportViewerPlan.LocalReport.Refresh();
        // Mostrar el reporte en pantalla.
        mvLista.ActiveViewIndex = 1;
        Iframe1.Visible = false;
        ReportViewerPlan.Visible = true;
    }
    public DataTable CrearDataTablereferencias()
    {
        Persona1Service DatosClienteServicio = new Persona1Service();
        List<Referncias> LstReferencias = new List<Referncias>();
        Referncias referencia = new Referncias();
        referencia.numero_radicacion = Convert.ToInt64(this.txtNumRadic.Text);
        LstReferencias = DatosClienteServicio.ListadoPersonas1ReporteReferencias(referencia, (Usuario)Session["Usuario"]);
        DataTable table = new DataTable();

        table.Columns.Add("nombre");
        table.Columns.Add("identificacion");
        table.Columns.Add("telefono");
        table.Columns.Add("direccion");
        table.Columns.Add("nombrer");
        table.Columns.Add("tipor");
        table.Columns.Add("parentesco");
        table.Columns.Add("direccionr");
        table.Columns.Add("telefonor");
        table.Columns.Add("identificacionr");
        foreach (Referncias vPersona4 in LstReferencias)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = "";
            datarw[1] = "";
            datarw[2] = "";
            datarw[3] = "";
            datarw[4] = vPersona4.nombres;
            datarw[5] = vPersona4.descripcion;
            datarw[6] = vPersona4.ListaDescripcion;
            datarw[7] = vPersona4.direccion;
            datarw[8] = vPersona4.telefono;
            datarw[9] = vPersona4.identificacion;
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableCodeudores()
    {
        Persona1Service DatosClienteServicio = new Persona1Service();
        List<Persona1> LstCodeudor = new List<Persona1>();
        Persona1 codeudor = new Persona1();
        codeudor.numeroRadicacion = Convert.ToInt64(this.txtNumRadic.Text);


        LstCodeudor = DatosClienteServicio.ListadoPersonas1ReporteCodeudor(codeudor, (Usuario)Session["Usuario"]);
        DataTable table = new DataTable();

        table.Columns.Add("nombre");
        table.Columns.Add("identificacion");
        table.Columns.Add("telefono");
        table.Columns.Add("direccion");
        table.Columns.Add("barrio");

        foreach (Persona1 vCodeudor in LstCodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vCodeudor.primer_nombre + ' ' + vCodeudor.segundo_nombre + ' ' + vCodeudor.primer_apellido + ' ' + vCodeudor.segundo_apellido;
            datarw[1] = vCodeudor.identificacion;
            datarw[2] = vCodeudor.direccion;
            datarw[3] = vCodeudor.telefono;
            datarw[4] = vCodeudor.barrioCorresponden;
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableFamiliares()
    {
        Persona1Service DatosClienteServicio = new Persona1Service();
        List<Persona1> LstFamiliar = new List<Persona1>();
        Persona1 familiar = new Persona1();
        familiar.identificacion = Convert.ToString(txtIdentific.Text);


        LstFamiliar = DatosClienteServicio.ListadoPersonas1ReporteFamiliares(familiar, (Usuario)Session["Usuario"]);
        DataTable table = new DataTable();

        table.Columns.Add("nombre");
        table.Columns.Add("Parentesco");
        table.Columns.Add("Sexo");
        table.Columns.Add("ACargo");
        table.Columns.Add("FechaNacimiento");
        table.Columns.Add("Observaciones");

        foreach (Persona1 vFamiliar in LstFamiliar)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vFamiliar.nombres;
            datarw[1] = vFamiliar.parentesco;
            datarw[2] = vFamiliar.sexo;
            datarw[3] = vFamiliar.acargo;
            datarw[4] = vFamiliar.fechanacimiento.Value.ToShortDateString();
            datarw[5] = vFamiliar.Observaciones;
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableVentasSemanales()
    {
        Persona1Service DatosClienteServicio = new Persona1Service();
        // Xpinn.FabricaCreditos.Services.VentasSemanalesService VentasSemanalesServicio = new Xpinn.FabricaCreditos.Services.VentasSemanalesService();
        List<VentasSemanales> LstVentassemanales = new List<VentasSemanales>();
        VentasSemanales cliente = new VentasSemanales();
        cliente.identificacion = Convert.ToString(txtIdentific.Text);

        LstVentassemanales = DatosClienteServicio.ListadoEstacionalidadSemanal(cliente, (Usuario)Session["Usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("TipoVenta");
        table.Columns.Add("Valor");
        table.Columns.Add("Lunes");
        table.Columns.Add("Martes");
        table.Columns.Add("Miercoles");
        table.Columns.Add("Jueves");
        table.Columns.Add("Viernes");
        table.Columns.Add("Sabado");
        table.Columns.Add("Domingo");
        table.Columns.Add("Total");
        foreach (VentasSemanales vVentasSemanales in LstVentassemanales)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vVentasSemanales.tipoventa;
            datarw[1] = vVentasSemanales.valor.ToString("0,0", CultureInfo.InvariantCulture); ;
            datarw[2] = vVentasSemanales.lunesrepo;
            datarw[3] = vVentasSemanales.martesrepo;
            datarw[4] = vVentasSemanales.miercolesrepo;
            datarw[5] = vVentasSemanales.juevesrepo;
            datarw[6] = vVentasSemanales.viernesrepo;
            datarw[7] = vVentasSemanales.sabadorepo;
            datarw[8] = vVentasSemanales.domingorepo;
            datarw[9] = vVentasSemanales.totalSemanal.ToString();
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableVentasMensuales()
    {
        Persona1Service DatosClienteServicio = new Persona1Service();
        // Xpinn.FabricaCreditos.Services.VentasSemanalesService VentasSemanalesServicio = new Xpinn.FabricaCreditos.Services.VentasSemanalesService();
        List<EstacionalidadMensual> LstVentasmensuales = new List<EstacionalidadMensual>();
        EstacionalidadMensual cliente = new EstacionalidadMensual();
        cliente.identificacion = Convert.ToString(txtIdentific.Text);

        LstVentasmensuales = DatosClienteServicio.ListadoEstacionalidadMensual(cliente, (Usuario)Session["Usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("TipoVenta");
        table.Columns.Add("Valor");
        table.Columns.Add("Enero");
        table.Columns.Add("Febrero");
        table.Columns.Add("Marzo");
        table.Columns.Add("Abril");
        table.Columns.Add("Mayo");
        table.Columns.Add("Junio");
        table.Columns.Add("Julio");
        table.Columns.Add("Agosto");
        table.Columns.Add("Septiembre");
        table.Columns.Add("Octubre");
        table.Columns.Add("Noviembre");
        table.Columns.Add("Diciembre");
        table.Columns.Add("Total");
        foreach (EstacionalidadMensual vVentasMensuales in LstVentasmensuales)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vVentasMensuales.tipoventa;
            datarw[1] = vVentasMensuales.valor.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[2] = vVentasMensuales.enerorepo;
            datarw[3] = vVentasMensuales.febrerorepo;
            datarw[4] = vVentasMensuales.marzorepo;
            datarw[5] = vVentasMensuales.abrilrepo;
            datarw[6] = vVentasMensuales.mayorepo;
            datarw[7] = vVentasMensuales.juniorepo;
            datarw[8] = vVentasMensuales.juliorepo;
            datarw[9] = vVentasMensuales.agostorepo;
            datarw[10] = vVentasMensuales.septiembrerepo;
            datarw[11] = vVentasMensuales.octubrerepo;
            datarw[12] = vVentasMensuales.noviembrerepo;
            datarw[13] = vVentasMensuales.diciembrerepo;
            datarw[14] = vVentasMensuales.totalMensual.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableMargenVentas()
    {
        MargenVentasService ventasServicio = new MargenVentasService();
        List<MargenVentas> LstMargenVentas = new List<MargenVentas>();
        MargenVentas cliente = new MargenVentas();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstMargenVentas = ventasServicio.ListarMargenVentas(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("NombreProducto");
        table.Columns.Add("UnidadesVendidas");
        table.Columns.Add("CostoUnidadVendida");
        table.Columns.Add("PrecioVentaUnidad");
        table.Columns.Add("CostoDeVentas");
        table.Columns.Add("VentaTotal");
        table.Columns.Add("Margen");
        table.Columns.Add("Utilidad");

        foreach (MargenVentas vMargenVentas in LstMargenVentas)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vMargenVentas.nombreproducto;
            datarw[1] = vMargenVentas.univendida;
            datarw[2] = vMargenVentas.costounidven.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vMargenVentas.preciounidven.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[4] = vMargenVentas.costoventa.ToString();
            datarw[5] = vMargenVentas.ventatotal.ToString();
            datarw[6] = vMargenVentas.margen.ToString();
            datarw[7] = vMargenVentas.utilidad.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableInfNegocio()
    {
        InformacionFinancieraService informacionfinancieraServicio = new InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarInformacionFinanNegRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture);

            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableInffinfamNegocio()
    {
        InformacionFinancieraService informacionfinancieraServicio = new InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarInformacionFinanFamRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture);

            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableInffinfamNegocioegresos()
    {
        InformacionFinancieraService informacionfinancieraServicio = new InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarInformacionFinanFamRepoeg(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture);

            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableActivos()
    {
        InformacionFinancieraService informacionfinancieraServicio = new InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarActivos(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture);

            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTablePasivos()
    {
        InformacionFinancieraService informacionfinancieraServicio = new InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarPasivos(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture);

            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableBalanceFamActivos()
    {
        InformacionFinancieraService informacionfinancieraServicio = new InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarbalanceFamActivos(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture);

            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableBalanceFamPasivos()
    {
        InformacionFinancieraService informacionfinancieraServicio = new InformacionFinancieraService();
        List<InformacionFinanciera> LstInformacionFinanciera = new List<InformacionFinanciera>();
        InformacionFinanciera cliente = new InformacionFinanciera();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarbalanceFamPasivos(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Cuenta");
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");

        foreach (InformacionFinanciera vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cuenta;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString("0,0", CultureInfo.InvariantCulture);

            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableComposicionPasivo()
    {
        ComposicionPasivoService informacionfinancieraServicio = new ComposicionPasivoService();
        List<ComposicionPasivo> LstInformacionFinanciera = new List<ComposicionPasivo>();
        ComposicionPasivo cliente = new ComposicionPasivo();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarComposicionPasivoRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Acreedor");
        table.Columns.Add("MontoOtorgado");
        table.Columns.Add("ValorCuota");
        table.Columns.Add("Frecuencia");
        table.Columns.Add("CuotaActual");
        table.Columns.Add("Plazo");

        foreach (ComposicionPasivo vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.acreedor;
            datarw[1] = vinformacionfinancieraServicio.monto_otorgado.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[2] = vinformacionfinancieraServicio.valor_cuota.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vinformacionfinancieraServicio.periodicidad;
            datarw[4] = vinformacionfinancieraServicio.cuota;
            datarw[5] = vinformacionfinancieraServicio.plazo;

            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableInvenACtivoFijos()
    {
        InventarioActivoFijoService informacionfinancieraServicio = new InventarioActivoFijoService();
        List<InventarioActivoFijo> LstInformacionFinanciera = new List<InventarioActivoFijo>();
        InventarioActivoFijo cliente = new InventarioActivoFijo();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarInventarioActivoFijoRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Descripcion");
        table.Columns.Add("Marca");
        table.Columns.Add("Valor");

        foreach (InventarioActivoFijo vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.descripcion;
            datarw[1] = vinformacionfinancieraServicio.marca;
            datarw[2] = vinformacionfinancieraServicio.valor.ToString();
            //  table.Compute("SUM(valor)", string.Empty);
            table.Rows.Add(datarw);
        }

        return table;
    }
    public DataTable CrearDataTableInvenMateriaPrima()
    {
        InventarioMateriaPrimaService informacionfinancieraServicio = new InventarioMateriaPrimaService();
        List<InventarioMateriaPrima> LstInformacionFinanciera = new List<InventarioMateriaPrima>();
        InventarioMateriaPrima cliente = new InventarioMateriaPrima();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarInventarioMateriaPrimaRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Descripcion");
        table.Columns.Add("Valor");


        foreach (InventarioMateriaPrima vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.descripcion;
            datarw[1] = vinformacionfinancieraServicio.valor.ToString();

            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableProductosProceso()
    {
        ProductosProcesoService informacionfinancieraServicio = new ProductosProcesoService();
        List<ProductosProceso> LstInformacionFinanciera = new List<ProductosProceso>();
        ProductosProceso cliente = new ProductosProceso();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarProductosProcesoRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Cantidad");
        table.Columns.Add("Producto");
        table.Columns.Add("Porcentaje");
        table.Columns.Add("ValorUnitario");
        table.Columns.Add("Total");
        foreach (ProductosProceso vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cantidad;
            datarw[1] = vinformacionfinancieraServicio.producto;
            datarw[2] = vinformacionfinancieraServicio.porcpd;
            datarw[3] = vinformacionfinancieraServicio.valunitario.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[4] = vinformacionfinancieraServicio.valortotal.ToString();
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableProductosTerminados()
    {
        ProductosTerminadosService informacionfinancieraServicio = new ProductosTerminadosService();
        List<ProductosTerminados> LstInformacionFinanciera = new List<ProductosTerminados>();
        ProductosTerminados cliente = new ProductosTerminados();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacionFinanciera = informacionfinancieraServicio.ListarProductosTerminadosRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Cantidad");
        table.Columns.Add("Producto");
        table.Columns.Add("ValorUnitario");
        table.Columns.Add("Total");
        foreach (ProductosTerminados vinformacionfinancieraServicio in LstInformacionFinanciera)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.cantidad;
            datarw[1] = vinformacionfinancieraServicio.producto;
            datarw[2] = vinformacionfinancieraServicio.vrunitario.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vinformacionfinancieraServicio.vrtotal.ToString();
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableBienes()
    {
        BienesRaicesService informacionfinancieraServicio = new BienesRaicesService();
        List<BienesRaices> LstInformacioncodeudor = new List<BienesRaices>();
        BienesRaices cliente = new BienesRaices();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;


        LstInformacioncodeudor = informacionfinancieraServicio.ListarBienesRaicesRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Tipo");
        table.Columns.Add("MatriculaInmobiliaria");
        table.Columns.Add("ValorComercial");
        table.Columns.Add("ValorHipoteca");
        foreach (BienesRaices vinformacionfinancieraServicio in LstInformacioncodeudor)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.tipo;
            datarw[1] = vinformacionfinancieraServicio.matricula;
            datarw[2] = vinformacionfinancieraServicio.valorcomercial.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vinformacionfinancieraServicio.valorhipoteca.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableVehiculos()
    {
        VehiculosService informacionfinancieraServicio = new VehiculosService();
        List<Vehiculos> LstInformacioncodeudor = new List<Vehiculos>();
        Vehiculos cliente = new Vehiculos();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        LstInformacioncodeudor = informacionfinancieraServicio.ListarVehiculosRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Marca");
        table.Columns.Add("Placa");
        table.Columns.Add("Modelo");
        table.Columns.Add("ValorComercial");
        table.Columns.Add("ValorPrenda");
        foreach (Vehiculos vinformacionfinancieraServicio in LstInformacioncodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.marca;
            datarw[1] = vinformacionfinancieraServicio.placa;
            datarw[2] = vinformacionfinancieraServicio.modelo;
            datarw[3] = vinformacionfinancieraServicio.valorcomercial.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[4] = vinformacionfinancieraServicio.valorprenda.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableRelacionDocumentos()
    {
        DatosPlanPagosService informacionfinancieraServicio = new DatosPlanPagosService();
        List<DatosPlanPagos> LstInformacionFinanciera = new List<DatosPlanPagos>();
        DatosPlanPagos cliente = new DatosPlanPagos();
        DataTable table = new DataTable();
        table.Columns.Add("Documentos");
        table.Columns.Add("Solicitante");
        table.Columns.Add("Codeudor");
        table.Rows.Add("Solicitud de crédito");
        table.Rows.Add(" * Datos básicos y demográficos del solicitante");
        table.Rows.Add(" * Autorización de consultas a las centrales de Riesgo");
        table.Rows.Add(" * Información Comercial y financiera del negocio del solicitante");
        table.Rows.Add(" * Información del Codeudor");
        table.Rows.Add(" * Concepto de Aprobación");
        table.Rows.Add("Reporte de consulta a las centrales de riesgo");
        table.Rows.Add("Reporte de confirmación de referencias");
        table.Rows.Add("Fotocopia cédula de ciudadanía (150%)");
        table.Rows.Add("Documentos SOporte del negocio(camara de comercio rut y facturas)");
        table.Rows.Add("Fotos(3 domicilio y 3 negocio)");
        table.Rows.Add("Recibos de servicios Públicos(2)");
        table.Rows.Add("Impuesto Predial");
        table.Rows.Add("Certificado de libertad y tradición");
        table.Rows.Add("Certificación Laboral");
        table.Rows.Add("Recibos de Nómina/Certificados de ingresos y retenciones)");
        table.Rows.Add("Póliza Adicional(Opcional)");
        table.Rows.Add("Anexo 1 Garantias Comunitarias(Opcional)");
        table.Rows.Add("Otros");

        return table;
    }
    public DataTable CrearDataTableRelacionDocDesembolso()
    {
        DatosPlanPagosService informacionfinancieraServicio = new DatosPlanPagosService();
        List<DatosPlanPagos> LstInformacionFinanciera = new List<DatosPlanPagos>();
        DatosPlanPagos cliente = new DatosPlanPagos();
        DataTable table = new DataTable();

        table.Columns.Add("Documentos");
        table.Columns.Add("Solicitante");
        table.Columns.Add("Codeudor");

        table.Rows.Add("Comprobante de egreso");
        table.Rows.Add("Plan de pagos");
        table.Rows.Add("Compromiso de actualización de datos");
        table.Rows.Add("Seguro de vida deudores(obligatorio)");

        return table;
    }
    public DataTable CrearDataTableReldocControltiempo()
    {
        DatosPlanPagosService informacionfinancieraServicio = new DatosPlanPagosService();
        List<DatosPlanPagos> LstInformacionFinanciera = new List<DatosPlanPagos>();
        DatosPlanPagos cliente = new DatosPlanPagos();
        DataTable table = new DataTable();
        table.Columns.Add("Documentos");
        table.Columns.Add("Solicitante");
        table.Columns.Add("Codeudor");
        table.Rows.Add("Fecha de solicitud de crédito");
        table.Rows.Add("Consulta en las centrales de Riesgo");
        table.Rows.Add("Levantamiento y análisis de la información");
        table.Rows.Add("Comité de Crédito");
        table.Rows.Add("Notificación al cliente");
        table.Rows.Add("Creación del cliente");
        table.Rows.Add("Radicación de crédito");
        table.Rows.Add("Desembolso");
        table.Rows.Add("           TOTAL DIAS     ");

        return table;
    }
    public DataTable CrearDataTableBienesUnidadFamCode()
    {
        BienesRaicesService informacionfinancieraServicio = new BienesRaicesService();
        List<BienesRaices> LstInformacioncodeudor = new List<BienesRaices>();
        BienesRaices cliente = new BienesRaices();
        cliente.cod_persona = Convert.ToInt64(Session["Codeudores"].ToString());

        LstInformacioncodeudor = informacionfinancieraServicio.ListarBienesRaicesRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Tipo");
        table.Columns.Add("MatriculaInmobiliaria");
        table.Columns.Add("ValorComercial");
        table.Columns.Add("ValorHipoteca");
        foreach (BienesRaices vinformacionfinancieraServicio in LstInformacioncodeudor)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.tipo;
            datarw[1] = vinformacionfinancieraServicio.matricula;
            datarw[2] = vinformacionfinancieraServicio.valorcomercial.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vinformacionfinancieraServicio.valorhipoteca.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableVehiculosCode()
    {
        VehiculosService informacionfinancieraServicio = new VehiculosService();
        List<Vehiculos> LstInformacioncodeudor = new List<Vehiculos>();
        Vehiculos cliente = new Vehiculos();
        cliente.cod_persona = Convert.ToInt64(Session["Codeudores"].ToString());

        LstInformacioncodeudor = informacionfinancieraServicio.ListarVehiculosRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("Marca");
        table.Columns.Add("Placa");
        table.Columns.Add("Modelo");
        table.Columns.Add("ValorComercial");
        table.Columns.Add("ValorPrenda");
        foreach (Vehiculos vinformacionfinancieraServicio in LstInformacioncodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.marca;
            datarw[1] = vinformacionfinancieraServicio.placa;
            datarw[2] = vinformacionfinancieraServicio.modelo;
            datarw[3] = vinformacionfinancieraServicio.valorcomercial.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[4] = vinformacionfinancieraServicio.valorprenda.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTablePresupuestoEmpresarialCode()
    {
        PresupuestoEmpresarialService informacionfinancieraServicio = new PresupuestoEmpresarialService();
        List<PresupuestoEmpresarial> LstInformacioncodeudor = new List<PresupuestoEmpresarial>();
        PresupuestoEmpresarial cliente = new PresupuestoEmpresarial();
        cliente.cod_persona = Convert.ToInt64(Session["Codeudores"].ToString());

        LstInformacioncodeudor = informacionfinancieraServicio.ListarPresupuestoEmpresarialREPO(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("TotalActivo");
        table.Columns.Add("TotalPasivo");
        table.Columns.Add("TotalPatrimonio");
        table.Columns.Add("VentaMensual");
        table.Columns.Add("CostoTotal");
        table.Columns.Add("Utilidad");
        foreach (PresupuestoEmpresarial vinformacionfinancieraServicio in LstInformacioncodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.totalactivo.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[1] = vinformacionfinancieraServicio.totalpasivo.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[2] = vinformacionfinancieraServicio.totalpatrimonio.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vinformacionfinancieraServicio.ventamensual.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[4] = vinformacionfinancieraServicio.costototal.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[5] = vinformacionfinancieraServicio.utilidad.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTablePresupuestoFamiliarCode()
    {
        PresupuestoFamiliarService informacionfinancieraServicio = new PresupuestoFamiliarService();
        List<PresupuestoFamiliar> LstInformacioncodeudor = new List<PresupuestoFamiliar>();
        PresupuestoFamiliar cliente = new PresupuestoFamiliar();
        cliente.cod_persona = Convert.ToInt64(Session["Codeudores"].ToString());

        LstInformacioncodeudor = informacionfinancieraServicio.ListarPresupuestoFamiliarRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("ActividadPrincipal");
        table.Columns.Add("Conyuge");
        table.Columns.Add("OtrosIngresos");
        table.Columns.Add("ConsumoFamiliar");
        table.Columns.Add("ObligacionesOCuotas");
        table.Columns.Add("Excedente");

        foreach (PresupuestoFamiliar vinformacionfinancieraServicio in LstInformacioncodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.actividadprincipal.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[1] = vinformacionfinancieraServicio.conyuge.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[2] = vinformacionfinancieraServicio.otrosingresos.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[3] = vinformacionfinancieraServicio.consumofamiliar.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[4] = vinformacionfinancieraServicio.obligaciones.ToString("0,0", CultureInfo.InvariantCulture);
            datarw[5] = vinformacionfinancieraServicio.excedente.ToString("0,0", CultureInfo.InvariantCulture);
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableReferenciasCodeudor()
    {
        RefernciasService informacionfinancieraServicio = new RefernciasService();
        List<Referncias> LstInformacioncodeudor = new List<Referncias>();
        Referncias cliente = new Referncias();
        cliente.cod_persona = Convert.ToInt64(Session["Codeudores"].ToString());

        LstInformacioncodeudor = informacionfinancieraServicio.ListarReferenciasRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("nombres");
        table.Columns.Add("tiporeferencia");
        table.Columns.Add("telefonoref");
        table.Columns.Add("direccionref");

        foreach (Referncias vinformacionfinancieraServicio in LstInformacioncodeudor)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionfinancieraServicio.nombres;
            datarw[1] = vinformacionfinancieraServicio.descripcion;
            datarw[2] = vinformacionfinancieraServicio.telefono;
            datarw[3] = vinformacionfinancieraServicio.direccion;
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableViabilidadFinanciera()
    {
        ViabilidadFinancieraService informacionviabilidad = new ViabilidadFinancieraService();
        List<ViabilidadFinanciera> Lstviabilidad = new List<ViabilidadFinanciera>();
        ViabilidadFinanciera cliente = new ViabilidadFinanciera();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;
        Lstviabilidad = informacionviabilidad.ListarViabilidadFinancieraRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("PruebaAcida");
        table.Columns.Add("EndeudamientoTotal");
        table.Columns.Add("RotacionCuentasXCobrar");
        table.Columns.Add("GastosFamiliares");
        table.Columns.Add("RotacionCuentasXPagar");
        table.Columns.Add("RotacionCapitalTrabajo");
        table.Columns.Add("RotacionInventarios");
        table.Columns.Add("PuntoEquilibrio");
        table.Columns.Add("EF");

        foreach (ViabilidadFinanciera vinformacionviabilidad in Lstviabilidad)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacionviabilidad.prueba;
            datarw[1] = vinformacionviabilidad.endeudamiento;
            datarw[2] = vinformacionviabilidad.rotacioncuentas;
            datarw[3] = vinformacionviabilidad.gastos;
            datarw[4] = vinformacionviabilidad.rotacioncuentaspagar;
            datarw[5] = vinformacionviabilidad.rotacioncapital;
            datarw[6] = vinformacionviabilidad.rotacioninventarios;
            datarw[7] = vinformacionviabilidad.puntoequilibrio;
            datarw[8] = vinformacionviabilidad.ef;
            table.Rows.Add(datarw);
        }
        return table;
    }
    public DataTable CrearDataTableIngresosEgresos()
    {
        EstadosFinancierosService informacioningresosegresosconsumo = new EstadosFinancierosService();
        List<EstadosFinancieros> Lstestadosfinancierosconsumo = new List<EstadosFinancieros>();
        EstadosFinancieros cliente = new EstadosFinancieros();
        Persona1 vPersona1 = new Persona1();
        vPersona1.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
        cliente.cod_persona = vPersona1.cod_persona;

        Lstestadosfinancierosconsumo = informacioningresosegresosconsumo.ListarIngresosEgresosRepo(cliente, (Usuario)Session["usuario"]);
        DataTable table = new DataTable();
        table.Columns.Add("SueldoSolicitante");
        table.Columns.Add("HonorariosSolicitante");
        table.Columns.Add("ArrendamientosSolicitante");
        table.Columns.Add("OtrosIngresosSolicitante");
        table.Columns.Add("TotalIngresosSolicitante");
        table.Columns.Add("SueldoConyuge");
        table.Columns.Add("HonorariosConyuge");
        table.Columns.Add("ArrendamientosConyuge");
        table.Columns.Add("OtrosIngresosConyuge");
        table.Columns.Add("TotalIngresosConyuge");
        table.Columns.Add("HipotecaSolicitante");
        table.Columns.Add("TarjetaCreditoSolicitante");
        table.Columns.Add("CuotasPresSolicitante");
        table.Columns.Add("GastosFamSolicitante");
        table.Columns.Add("DescuentosSolicitante");
        table.Columns.Add("TotalEgresosSolicitante");
        table.Columns.Add("HipotecaConyuge");
        table.Columns.Add("TarjetaCreditoConyuge");
        table.Columns.Add("CuotasPresConyuge");
        table.Columns.Add("GastosFamConyuge");
        table.Columns.Add("DescuentosConyuge");
        table.Columns.Add("TotalEgresosConyuge");

        foreach (EstadosFinancieros vinformacioningresosegresos in Lstestadosfinancierosconsumo)
        {

            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vinformacioningresosegresos.sueldo;
            datarw[1] = vinformacioningresosegresos.honorarios;
            datarw[2] = vinformacioningresosegresos.arrendamientos;
            datarw[3] = vinformacioningresosegresos.otrosingresos;
            datarw[4] = vinformacioningresosegresos.totalingreso;
            datarw[5] = vinformacioningresosegresos.sueldoconyuge;
            datarw[6] = vinformacioningresosegresos.honorariosconyuge;
            datarw[7] = vinformacioningresosegresos.arrendamientosconyuge;
            datarw[8] = vinformacioningresosegresos.otrosingresosconyuge;
            datarw[9] = vinformacioningresosegresos.totalingresoconyuge;

            datarw[10] = vinformacioningresosegresos.hipoteca;
            datarw[11] = vinformacioningresosegresos.hipotecaconyuge;
            datarw[12] = vinformacioningresosegresos.targeta_credito;
            datarw[13] = vinformacioningresosegresos.targeta_creditoconyuge;
            datarw[14] = vinformacioningresosegresos.otrosprestamos;
            datarw[15] = vinformacioningresosegresos.otrosprestamosconyuge;
            datarw[16] = vinformacioningresosegresos.gastofamiliar;
            datarw[17] = vinformacioningresosegresos.gastofamiliarconyuge;
            datarw[18] = vinformacioningresosegresos.decunomina;
            datarw[19] = vinformacioningresosegresos.decunominaconyuge;
            datarw[20] = vinformacioningresosegresos.totalegresos;
            datarw[21] = vinformacioningresosegresos.totalegresosconyuge;

            table.Rows.Add(datarw);
        }

        return table;

    }
    /// <summary>
    /// Generar tabla para pasar al reporte
    /// </summary>
    /// <returns></returns>
    public DataTable CrearDataTable(String pIdObjeto)
    {
        if (pIdObjeto == "")
            pIdObjeto = txtNumRadic.Text;
        if (Session["PlanPagos"] == null)
            return null;

        DataTable table = new DataTable();

        DatosPlanPagos datosApp = new DatosPlanPagos();
        datosApp.numero_radicacion = Int64.Parse(pIdObjeto);
        List<DatosPlanPagos> lstPlanPagos = new List<DatosPlanPagos>();
        lstPlanPagos = (List<DatosPlanPagos>)Session["PlanPagos"];
        List<Atributos> lstAtr = new List<Atributos>();
        lstAtr = (List<Atributos>)Session["AtributosPlanPagos"];

        table.Columns.Add("numerocuota");
        table.Columns.Add("fechacuota");
        table.Columns.Add("sal_ini");
        table.Columns.Add("capital");
        table.Columns.Add("int_1");
        table.Columns.Add("int_2");
        table.Columns.Add("int_3");
        table.Columns.Add("int_4");
        table.Columns.Add("int_5");
        table.Columns.Add("int_6");
        table.Columns.Add("int_7");
        table.Columns.Add("int_8");
        table.Columns.Add("int_9");
        table.Columns.Add("int_10");
        table.Columns.Add("int_11");
        table.Columns.Add("int_12");
        table.Columns.Add("int_13");
        table.Columns.Add("int_14");
        table.Columns.Add("int_15");
        table.Columns.Add("total");
        table.Columns.Add("sal_fin");

        foreach (DatosPlanPagos item in lstPlanPagos)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = item.numerocuota;
            if (item.fechacuota != null && item.total != 0)
            {
                datarw[1] = item.fechacuota.Value.ToShortDateString();
                datarw[2] = item.sal_ini.ToString("0,0");
                datarw[3] = item.capital.ToString("0,0");
                datarw[4] = item.int_1.ToString("0,0");
                datarw[5] = item.int_2.ToString("0,0");
                datarw[6] = item.int_3.ToString("0,0");
                datarw[7] = item.int_4.ToString("0,0");
                datarw[8] = item.int_5.ToString("0,0");
                datarw[9] = item.int_6.ToString("0,0");
                datarw[10] = item.int_7.ToString("0,0");
                datarw[11] = item.int_8.ToString("0,0");
                datarw[12] = item.int_9.ToString("0,0");
                datarw[13] = item.int_10.ToString("0,0");
                datarw[14] = item.int_11.ToString("0,0");
                datarw[15] = item.int_12.ToString("0,0");
                datarw[16] = item.int_13.ToString("0,0");
                datarw[17] = item.int_14.ToString("0,0");
                datarw[18] = item.int_15.ToString("0,0");
                datarw[19] = item.total.ToString("0,0");
                datarw[20] = item.sal_fin.ToString("0,0");
                table.Rows.Add(datarw);
            }
        }

        return table;
    }
    #endregion

    #region Metodos Guardar
    /// <summary>
    ///  Evento para guardar 
    /// </summary>
    protected Boolean GuardarDocumentos()
    {
        try
        {
            // Verificar datos del crédito   
            Credito vCredito = new Credito();
            vCredito = CreditoServicio.ConsultarCredito(Convert.ToInt64(this.txtNumRadic.Text), (Usuario)Session["usuario"]);
            if (vCredito.estado != "C")
            {
                return false;
            }
            if (idObjeto == "")
            {
                return false;
            }

            // Grabar los documentos del crédito
            DocumentoService documentoServicio = new DocumentoService();
            int numero = 0;
            foreach (GridViewRow row in gvLista.Rows)
            {
                CheckBox chkSeleccionar = ((CheckBox)row.FindControl("cbx"));
                if (chkSeleccionar.Checked)
                {
                    Documento document = new Documento() { iddocumento = null, numero_radicacion = Convert.ToInt64(txtNumRadic.Text), cod_linea_credito = txtLinea.Text };
                    try
                    {
                        string dato = GetCellByName(row, "Tipo documento").Text;
                        document.tipo_documento = Convert.ToInt64(dato);
                        dato = GetCellByName(row, "Descripción").Text;
                        document.descripcion_documento = dato;
                        dato = GetCellByName(row, "Requerido").Text;
                        document.requerido = dato;
                        TextBox txtReferencia = ((TextBox)row.FindControl("txtReferencia"));
                        dato = txtReferencia.Text;
                        document.referencia = dato;
                        dato = GetCellByName(row, "Ruta").Text;
                        document.ruta = dato;
                        documentoServicio.CrearDocumentoGenerado(document, Convert.ToInt64(txtNumRadic.Text), (Usuario)Session["usuario"]);
                        numero += 1;
                    }
                    catch (Exception ex)
                    {
                        lblInfo.Text = String.Format("Ocurrio un error al generar los documentos. \r\n({0})", ex.Message);
                    }
                }
            }
            if (numero == 0)
            {
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "No se seleccionaron documentos para generar";
                return false;
            }

            //// Cargar datos del crédito
            //vCredito.numero_radicacion = Convert.ToInt64(txtNumRadic.Text);
            //vCredito.identificacion = Convert.ToString(txtIdentific.Text.Trim());
            //vCredito.tipo_identificacion = Convert.ToString(txtTipo_identificacion.Text.Trim());
            //vCredito.nombre = Convert.ToString(txtNombre.Text.Trim());
            //vCredito.linea_credito = Convert.ToString(txtLinea_credito.Text.Trim());
            //vCredito.monto = Convert.ToInt64(txtMonto.Text.Trim().Replace(".", ""));
            //vCredito.plazo = Convert.ToInt64(txtPlazo.Text.Trim());
            //vCredito.periodicidad = Convert.ToString(txtPeriodicidad.Text.Trim());
            //vCredito.valor_cuota = Convert.ToInt64(txtValor_cuota.Text.Trim().Replace(".", ""));
            //vCredito.forma_pago = Convert.ToString(txtForma_pago.Text.Trim());
            //vCredito.estado = "G";
            //CreditoServicio.ModificarCredito(vCredito, (Usuario)Session["usuario"]);

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CreditoServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }

        return true;
    }
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (mvLista.ActiveViewIndex == 7)
            {
                DocumentosAnexo.GuardarArchivos(txtNumRadic.Text);
            }
            else
            {
                byte[] bytesArchivo = PrevisualizarArchivo();

                DocumentosAnexosService docService = new DocumentosAnexosService();
                DocumentosAnexos documento = new DocumentosAnexos();

                documento.numero_radicacion = Convert.ToInt64(txtNumRadic.Text);
                documento.tipo_documento = 1;
                documento.descripcion = "Carga Pagare";
                documento.fechaanexo = DateTime.Today;
                documento.estado = 1;
                documento.imagen = bytesArchivo;

                if (!string.IsNullOrWhiteSpace(lblIDImagen.Text) && lblIDImagen.Text != "0")
                {
                    long id = Convert.ToInt64(lblIDImagen.Text);
                    documento.iddocumento = id;

                    documento = docService.ModificarDocumentosAnexos(documento, _usuario);
                }
                else
                {
                    documento = docService.CrearDocumentosAnexos(documento, _usuario);
                }

                if (documento.iddocumento != 0)
                {
                    lblNotificacionGuardado.Text = "Documento guardado satisfactoriamente ID: " + documento.iddocumento;
                    lblNotificacionGuardado.Visible = true;
                    lblIDImagen.Text = documento.iddocumento.ToString();
                }
                else
                {
                    VerError("No se ha podido guardar el documento!.");
                    lblNotificacionGuardado.Text = "No se ha podido guardar el documento!.";
                    lblNotificacionGuardado.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            VerError("No se pudo guardar el documento, " + ex.Message);
        }
    }

    #endregion

}