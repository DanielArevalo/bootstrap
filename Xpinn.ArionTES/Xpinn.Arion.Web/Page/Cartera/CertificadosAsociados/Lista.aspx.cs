using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Xpinn.Util;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Linq;
using System.Globalization;
using System.Text;

partial class Lista : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
    private Xpinn.Cartera.Services.CertificacionService AfiliacionServicio = new Xpinn.Cartera.Services.CertificacionService();
    private Usuario usuario = new Usuario();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(AfiliacionServicio.CodigoProgramaCertificacionAsociados, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarCancelar(false);
            toolBar.MostrarImprimir(false);
            ctlBusquedaPersonas.eventoEditar += gvLista_SelectedIndexChanged;
            ctlFormatos.eventoClick += btnImpresion_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoProgramaCertificacionAsociados, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            usuario = (Usuario)Session["usuario"];

            if (!Page.IsPostBack)
            {
                CargarDropDown();
                txtfechaProy.ToDateTime = DateTime.Now;
                mvReporte.ActiveViewIndex = 0;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoProgramaCertificacionAsociados, "Page_Load", ex);
        }
    }
    protected void CargarDropDown()
    {
        //CARGANDO DOCUMENTOS PERTENECIENTES CERTIFICACIONES
        ctlFormatos.Inicializar("5");
    }
    protected void btnImpresion_Click(object sender, EventArgs e)
    {

        try
        {
            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Documentos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
            }
            catch
            { }
            string pRuta = "/Documentos/";
            string pVariable = lblIdentificacion.Text.Trim();
            //Cuando se envia numero credito origen es 0
            string origen = "1";
            if (!ctlFormatos.ImprimirFormato(pVariable, pRuta, origen))
                return;

            //Descargando el Archivo PDF
            string cNombreDeArchivo = pVariable.Trim() + "_" + ctlFormatos.ddlFormatosValue + ".pdf";
            string cRutaLocalDeArchivoPDF = Server.MapPath("/Documentos\\" + cNombreDeArchivo);
            FileInfo file = new FileInfo(cRutaLocalDeArchivoPDF);
            var sjs = File.ReadAllBytes(cRutaLocalDeArchivoPDF);
            Response.AddHeader("Content-Disposition", "attachment; filename= PazYSalvo.pdf");
            Response.AddHeader("Content-Length", sjs.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(sjs);
            Response.Flush();
            Response.Close();

            try
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            }
            catch (Exception exception)
            {
            }

        }
        catch (Exception ex)
        {
            ctlFormatos.lblErrorIsVisible = true;
            ctlFormatos.lblErrorText = ex.Message;
        }

    }



    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        VerError("");
        ctlBusquedaPersonas.Filtro = "";
        ctlBusquedaPersonas.Actualizar(0);


    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        mvReporte.ActiveViewIndex = 0;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(false);
        toolBar.MostrarConsultar(true);
        toolBar.MostrarImprimir(false);
    }



    protected void Actualizar(String id, object sender)
    {
        try
        {
            // Traer los datos de la persona
            Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 pPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            pPersona1.seleccionar = "Cod_persona";
            pPersona1.soloPersona = 1;
            pPersona1.cod_persona = Convert.ToInt64(id);
            pPersona1 = DatosClienteServicio.ConsultarPersona1Param(pPersona1, (Usuario)Session["usuario"]);
            if (pPersona1 == null)
                return;
            lblIdentificacion.Text = id;
            lblNombre.Text = pPersona1.nombres + " " + pPersona1.apellidos;

            // Mostrar información de los créditos
            List<Xpinn.FabricaCreditos.Entities.Credito> lstCreditos = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            Xpinn.FabricaCreditos.Services.CreditoService creditoservice = new Xpinn.FabricaCreditos.Services.CreditoService();
            lstCreditos = creditoservice.ListarCreditoAsociados(Convert.ToInt64(id), Convert.ToDateTime(txtfechaProy.Text), (Usuario)Session["usuario"]);
            if (lstCreditos.Count > 0)
            {
                gvListas.Visible = true;
                gvListas.DataSource = lstCreditos;
                gvListas.DataBind();
                lblMensajeActivos.Text = "Se encontraron " + lstCreditos.Count + " créditos";
            }
            else
            {
                gvListas.Visible = false;
                lblMensajeActivos.Text = "La persona no tiene créditos vigentes";
            }

            // Mostrar créditos terminados        
            List<Xpinn.FabricaCreditos.Entities.Credito> lstConsultas = new List<Xpinn.FabricaCreditos.Entities.Credito>();
            lstConsultas = creditoservice.ConsultarCreditoTerminado(Convert.ToInt64(id), (Usuario)Session["usuario"]);
            if (lstConsultas.Count > 0)
            {
                gvTerminados.Visible = true;
                gvTerminados.DataSource = lstConsultas;
                gvTerminados.DataBind();
                lblMensajeTerminados.Text = "Se encontraron " + lstConsultas.Count + " terminados";
            }
            else
            {
                gvTerminados.Visible = false;
                lblMensajeTerminados.Text = "La persona no tiene créditos vigentes";
            }
            //iReemplazarEnDocumentoDeWordYGuardarPDF("",null,"");

        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        ctlFormatos.lblErrorText = "";
        if (ctlFormatos.ddlFormatosItem == null)
        {
            if (mvReporte.ActiveViewIndex == 2)
            {
                Imprimir(ctlBusquedaPersonas.gvListado);
            }
            else
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                Usuario pUsuario = (Usuario)Session["usuario"];
                string pNomUsuario = pUsuario.nombre != null && pUsuario.nombre != "" ? "_" + pUsuario.nombre : "";
                byte[] bytes = RpviewEstado.LocalReport.Render("PDF", null, out mimeType,
                           out encoding, out extension, out streamids, out warnings);
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
                FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                Session["Archivo" + Usuario.codusuario] = Server.MapPath("output" + pNomUsuario + ".pdf");
                frmPrint.Visible = true;
                RpviewEstado.Visible = false;
            }
        }
        else
        {
            if (ctlFormatos.ddlFormatosItem != null)
                ctlFormatos.ddlFormatosIndex = 0;
            ctlFormatos.MostrarControl();
        }

    }


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = ctlBusquedaPersonas.gvListado.SelectedDataKey.Value.ToString();
        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(true);
        toolBar.MostrarImprimir(true);
        toolBar.MostrarConsultar(false);
        mvReporte.ActiveViewIndex = 2;
        Actualizar(id, sender);
    }

    protected void Imprimir(object sender)
    {

        //Consultando nombre de ciudad para el reporte
        GeneralService BOGeneral = new GeneralService();
        General pEntiCiudad = BOGeneral.ConsultarGeneral(331, Usuario);

        VerError(gSeparadorMiles);
        if (txtfechaProy.Text == "")
        {
            VerError("Ingrese la fecha del certificado, verifique los datos.");
            txtfechaProy.Focus();
            return;
        }
        mvReporte.ActiveViewIndex = 1;
        GridView gvListado = (GridView)sender;

        // Traer los datos de la persona
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 pPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        pPersona1.seleccionar = "Identificacion";
        pPersona1.soloPersona = 1;
        pPersona1.identificacion = ctlBusquedaPersonas.gvListado.SelectedDataKey[2].ToString();
        pPersona1 = DatosClienteServicio.ConsultarPersona1Param(pPersona1, (Usuario)Session["usuario"]);
        mvReporte.ActiveViewIndex = 2;

        // Consultar saldo de aportes
        string pvrAporte = "";
        Xpinn.Aportes.Entities.Aporte pAporte = new Xpinn.Aportes.Entities.Aporte();
        Xpinn.Aportes.Services.AporteServices BOAporte = new Xpinn.Aportes.Services.AporteServices();
        pAporte = BOAporte.ConsultarTotalAportes(pPersona1.cod_persona, Convert.ToDateTime(txtfechaProy.Text), (Usuario)Session["usuario"]);
        pAporte.Saldo = pAporte.Saldo != 0 ? pAporte.Saldo : 0;
        pvrAporte = pAporte.Saldo.ToString("c0");
        string separadorDecimal = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        Configuracion conf = new Configuracion();

        // Traer listado de créditos
        DataTable table = new DataTable();
        table.Columns.Add("Linea");
        table.Columns.Add("Monto", typeof(decimal));
        table.Columns.Add("Saldo", typeof(decimal));
        table.Columns.Add("Cuota", typeof(decimal));
        table.Columns.Add("Total", typeof(decimal));
        table.Columns.Add("Vencimiento", typeof(decimal));
        int numeroCreditos = 0;
        decimal ptotalMora = 0;
        DateTime fechaproxpago = DateTime.Now;
        DateTime fechageneracion = Convert.ToDateTime(txtfechaProy.Texto);
        if (gvListas.Rows.Count > 0)
        {
            foreach (GridViewRow Creditos in gvListas.Rows)
            {
                CheckBox chkManejaCP = (CheckBox)Creditos.FindControl("chkManejaCP");
                if (chkManejaCP.Checked)
                {
                    DataRow datarw;
                    datarw = table.NewRow();
                    datarw[0] = Creditos.Cells[1].Text + "-" + Creditos.Cells[2].Text;
                    datarw[1] = Creditos.Cells[7].Text;
                    datarw[2] = Creditos.Cells[8].Text;
                    datarw[3] = Creditos.Cells[11].Text;
                    datarw[4] = Creditos.Cells[10].Text;
                    datarw[5] = Creditos.Cells[6].Text;
                    fechaproxpago = Convert.ToDateTime(Creditos.Cells[5].Text);
                    if (Convert.ToDecimal(Creditos.Cells[9].Text) != 0)
                        ptotalMora += Convert.ToDecimal(Creditos.Cells[9].Text);
                    table.Rows.Add(datarw);
                    numeroCreditos += 1;
                }
            }
        }

        if (numeroCreditos <= 0)
        {
            DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = "0";
            datarw[1] = "0";
            datarw[2] = "0";
            datarw[3] = "0";
            datarw[4] = "0";
            datarw[5] = "0";
            table.Rows.Add(datarw);
        }

        // Certificar créditos terminados
        int numeroTerminados = 0;
        string certificadoTerminados = "";
        foreach (GridViewRow Creditos in gvTerminados.Rows)
        {
            CheckBox chkselecciona = (CheckBox)Creditos.FindControl("chkselecciona");
            if (chkselecciona.Checked)
            {
                if (numeroTerminados == 0)
                    certificadoTerminados = "El Asociado se encuentra a Paz y Salvo con el";
                certificadoTerminados += " Crédito No. " + Creditos.Cells[1].Text + " cuya cuota era de $" + Creditos.Cells[9].Text;
                numeroTerminados += 1;
            }
        }

        Usuario pUsu = (Usuario)Session["usuario"];
        RpviewEstado.LocalReport.DataSources.Clear();


        ReportParameter[] param = new ReportParameter[18];
        DateTime fecha = Convert.ToDateTime(txtfechaProy.Text);
        Int64 year = fecha.Year;

        String mes = MonthName(fecha.Month).ToUpperInvariant();
        Int64 dia = fecha.Day;
        String fechaimprimir = dia + " de " + mes + " de " + year;



        param[0] = new ReportParameter("fecha", fechaimprimir);
        param[1] = new ReportParameter("nombre", " " + pPersona1.nombres + " " + pPersona1.apellidos);
        param[2] = new ReportParameter("identificacion", " " + pPersona1.identificacion);
        param[3] = new ReportParameter("aportes", " " + pvrAporte);
        String CantidadGrid = "true";
        if (numeroCreditos > 0)
            CantidadGrid = "false";
        param[4] = new ReportParameter("DataGrid", CantidadGrid);
        DateTime fecha2 = DateTime.Now;
        Int64 year2 = fecha2.Year;

        String mes2 = MonthName(fecha2.Month);
        Int64 dia2 = fecha2.Day;
        String fechaimprimiractual = dia2 + " de " + new CultureInfo("en-US", false).TextInfo.ToTitleCase(mes2) + " de " + year2;

        param[5] = new ReportParameter("fechaActual", fechaimprimiractual);
        string txt = pAporte.Saldo == 0 ? ", actualmente Retirado(a)." : ".";
        param[6] = new ReportParameter("texto", txt);
        string txt2 = pAporte.Saldo == 0 ? " fue " : " es ";
        param[7] = new ReportParameter("texto2", txt2);
        string txt3 = ptotalMora == 0 ? "al día" : "";
        param[8] = new ReportParameter("TotalDeuda", ptotalMora.ToString("c0"));
        param[9] = new ReportParameter("texto3", txt3);
        param[10] = new ReportParameter("texto4", certificadoTerminados);
        param[11] = new ReportParameter("entidad", pUsu.empresa);
        string NomCiudad = pUsu.nombre_oficina;
        if (pEntiCiudad != null)
            NomCiudad = pEntiCiudad.valor != null ? pEntiCiudad.valor : pUsu.nombre_oficina;
        param[12] = new ReportParameter("ciudad", NomCiudad);
        param[13] = new ReportParameter("gerente", pUsu.representante_legal);
        param[14] = new ReportParameter("ImagenReport", ImagenReporte());
        String observacion = " ";
        if (TxtObservaciones.Text != "")
            observacion = "Notas: " + TxtObservaciones.Text;
        param[15] = new ReportParameter("Observaciones", observacion);
        param[16] = new ReportParameter("usuario", usuario.nombre);
        if (numeroCreditos > 0 && ptotalMora == 0)
        {
            String entidad = pUsu.empresa;
            string mensaje = "A la fecha se encuentra AL DIA con todas las Obligaciones Crediticias contraídas con:" + entidad;
            param[17] = new ReportParameter("mensaje", mensaje);
        }

        if (numeroCreditos > 0 && ptotalMora > 0 && fechaproxpago < fechageneracion)
        {
            String entidad = pUsu.empresa;

            string mensaje2 = "A la fecha se encuentra en MORA con algunas Obligaciones Crediticias contraídas con:" + entidad;
            param[17] = new ReportParameter("mensaje", mensaje2);

        }
        if (numeroCreditos > 0 && ptotalMora > 0 && fechaproxpago >= fechageneracion)
        {
            String entidad = pUsu.empresa;

            string mensaje2 = "A la fecha se encuentra AL DIA con algunas Obligaciones Crediticias contraídas con:" + entidad;
            param[17] = new ReportParameter("mensaje", mensaje2);

        }
        if (numeroCreditos == 0)
        {
            string mensaje = " ";
            String entidad = pUsu.empresa;

            param[17] = new ReportParameter("mensaje", mensaje);
        }

        TiposDocumento DocCertificado = new TiposDocumento();
        DocCertificado = GenerarDocumento();
        if (DocCertificado != null)
            iReemplazarEnDocumentoDeWordYGuardarPDF(DocCertificado, param);
        else //Si el documento no está parametrizado, generar el reporte
        {
            RpviewEstado.LocalReport.EnableExternalImages = true;
            RpviewEstado.LocalReport.DataSources.Clear();
            RpviewEstado.LocalReport.SetParameters(param);
            ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
            RpviewEstado.LocalReport.DataSources.Add(rds1);
            RpviewEstado.LocalReport.Refresh();
            frmPrint.Visible = false;
            RpviewEstado.Visible = true;
            mvReporte.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarCancelar(true);
            toolBar.MostrarConsultar(false);
            toolBar.MostrarImprimir(true);
        }

    }
    public string MonthName(int month)
    {
        DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", true).DateTimeFormat;
        return dtinfo.GetMonthName(month);
    }


    public string ImagenReporte()
    {
        string cRutaDeImagen;
        cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";
        return cRutaDeImagen;
    }

    private TiposDocumento GenerarDocumento()
    {

        TiposDocumentoService tiposDocumentoServicio = new TiposDocumentoService();
        // Solicitando la información  del tipo de documento para saber si existe el tipo documento 0 
        TiposDocumento vTipoDoc = new TiposDocumento();
        TipoDocumento vTipoDo = new TipoDocumento();
        vTipoDo = tiposDocumentoServicio.ConsultarTipoDoc((Usuario)Session["usuario"]).FirstOrDefault(x => x.idTipo == "CA");
        vTipoDoc = tiposDocumentoServicio.ConsultarTiposDocumento(0, (Usuario)Session["usuario"], vTipoDo.idTipo);

        if (vTipoDoc.Textos != null || vTipoDoc.texto != null)
            return vTipoDoc;
        else
            return null;
    }

    private void iReemplazarEnDocumentoDeWordYGuardarPDF(TiposDocumento documento, ReportParameter[] parametros)
    {
        string pTexto = documento.texto != null ? documento.texto : Encoding.ASCII.GetString(documento.Textos);

        //Crear la tabla de créditos activos
        string tablaActivos = "<table border=1 style=\"border-color:black; font-family: Arial; width:80%;  font-size:8px\"><thead style=\"background-color:#2E9AFE; color:white\">";

        if (gvListas.Rows.Count > 0)
        {
            foreach (DataControlField columna in gvListas.Columns)//Encabezados
            {
                if (columna.HeaderText != "" && columna.HeaderText != "Seleccionados")
                    tablaActivos = tablaActivos + "<th>" + columna.HeaderText + "</th>";
            }
            tablaActivos = tablaActivos + "</tr></thead>";

            for (int fila = 0; fila < gvListas.Rows.Count; fila++)
            {
                tablaActivos = tablaActivos + "<tr>";
                for (int col = 0; col < gvListas.Rows[fila].Cells.Count; col++)
                {
                    if (col != 0 && col != gvListas.Rows[fila].Cells.Count - 1)
                        tablaActivos = tablaActivos + "<td>" + gvListas.Rows[fila].Cells[col].Text + "</td>";
                }
                tablaActivos = tablaActivos + "</tr>";

            }
            tablaActivos = tablaActivos + "</table>";
        }

        //Crear la tabla de créditos terminados
        string tablaTerminados = "<table><thead><tr>";
        if (gvTerminados.Rows.Count > 0)
        {
            foreach (DataControlField columna in gvTerminados.Columns)//Encabezados
            {
                if (columna.HeaderText != "" || columna.HeaderText != "Seleccionados")
                    tablaTerminados = tablaTerminados + "<th>" + columna.HeaderText + "</th>";
            }
            tablaTerminados = tablaTerminados + "</tr></thead>";

            for (int fila = 0; fila < gvTerminados.Rows.Count - 1; fila++)
            {
                if (fila != 0 && fila != gvTerminados.Rows.Count)
                {
                    tablaTerminados = tablaTerminados + "<tr>";
                    for (int col = 0; col < gvTerminados.Rows[fila].Cells.Count; col++)
                    {
                        tablaTerminados = tablaTerminados + "<td>" + gvTerminados.Rows[fila].Cells[col].Text + "</td>";
                    }
                    tablaTerminados = tablaTerminados + "</tr>";
                }
            }
            tablaTerminados = tablaTerminados + "</table>";
        }
        // Validar que el texto tenga contenido
        if (pTexto != null)
            if (pTexto != "")
                if (pTexto.Trim().Length <= 0)
                    return;

        Int64 id_cliente = 0;
        for (int i = 0; i < parametros.Count(); i++)
        {
            ReportParameter param = new ReportParameter();
            param = parametros[i];
            int validar = 0;
            string cCampo = param.Name;
            string cValor = "";
            if (pTexto.Contains(param.Name))
            {
                if (param.Name == "texto")
                {
                    string texto1 = param.Values[0].ToString();
                    pTexto = pTexto.Replace("texto1", texto1);
                    validar++;
                }
                else if (param.Name == "fecha")
                {
                    if (gvListas.Rows.Count == 0)
                    {
                        pTexto = pTexto.Replace("PazySalvo", "A la fecha se encuentra a PAZ Y SALVO con todas las Obligaciones Crediticias contraídas con entidad.");
                        pTexto = pTexto.Replace("tablaCreditosActivos", "");
                    }
                    else
                    {
                        pTexto = pTexto.Replace("PazySalvo", "A la fecha se encuentra en MORA con algunas Obligaciones Crediticias contraídas con: entidad");
                    }


                    pTexto = pTexto.Replace(param.Name, Convert.ToDateTime(param.Values[0].ToString()).ToLongDateString());
                    string nomDia = DateTime.Now.ToString("dddd", CultureInfo.CreateSpecificCulture("es-ES"));
                    pTexto = pTexto.Replace(nomDia + ",", "");
                    validar++;
                }
                else if (param.Name == "texto3" && param.Values[0].ToString() == "")
                {
                    pTexto = pTexto.Replace("TotalDeuda", "");
                    pTexto = pTexto.Replace("texto3", "");
                    validar++;
                }
                else if (param.Name != "ciudad")
                {
                    cValor = param.Values[0].ToString().Trim().Replace("'", "");
                    pTexto = pTexto.Replace(cCampo, cValor);
                    validar++;
                }


                if (validar == 0)
                {
                    pTexto.Replace(param.Name, param.Values[0]);
                }

            }
        }

        int numeroTerminados = 0;
        string certificadoTerminados = "";
        foreach (GridViewRow Creditos in gvTerminados.Rows)
        {
            CheckBox chkselecciona = (CheckBox)Creditos.FindControl("chkselecciona");
            if (chkselecciona.Checked)
            {
                if (numeroTerminados == 0)
                    certificadoTerminados = "El Asociado se encuentra a Paz y Salvo con el";
                certificadoTerminados += " Crédito No. " + Creditos.Cells[1].Text + " cuya cuota era de $" + Creditos.Cells[8].Text;
                numeroTerminados += 1;
            }
        }
        pTexto = pTexto.Replace("certificadoTerminados", certificadoTerminados);

        //Reemplazar tablas
        pTexto = pTexto.Replace("tablaCreditosActivos", gvListas.Rows.Count > 0 ? tablaActivos : "");
        pTexto = pTexto.Replace("tablaCreditosTerminados", gvTerminados.Rows.Count > 0 ? tablaTerminados : "");

        string rutaDocumento = Server.MapPath("~/Page/FabricaCreditos/GeneracionDocumentos/Documentos" + "Certificado" + id_cliente + '.' + 'p' + 'd' + 'f');

        // Convertir a PDF
        StringReader sr = new StringReader(pTexto.Replace("'", ""));
        Document pdfDoc = new Document(PageSize.A4, 20f, 10f, 10f, 10f);
        //Document pdfDoc = new Document(PageSize.A4, 22f, 12f, 12f, 12f);
        PdfWriter.GetInstance(pdfDoc, new FileStream(rutaDocumento, FileMode.OpenOrCreate));
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        //Descargar el archivo al cliente
        try
        {
            if (bMostrarPDF)
            {
                String Fecha = Convert.ToString(DateTime.Now.ToString("MMddyyyy"));
                String fileName = "CertificadoAsociados.pdf";

                FileStream archivo = new FileStream(rutaDocumento, FileMode.Open, FileAccess.Read);
                FileInfo file = new FileInfo(rutaDocumento);
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.TransmitFile(file.FullName);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            VerError("Se produjo un error al generar el certificado" + ex.Message);
        }
    }
}
