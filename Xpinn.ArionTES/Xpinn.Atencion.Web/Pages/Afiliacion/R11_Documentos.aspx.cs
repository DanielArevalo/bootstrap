using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.xml;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Configuration;

public partial class Pages_Afiliacion_Default : System.Web.UI.Page
{
    xpinnWSEstadoCuenta.SolicitudPersonaAfi pEntidad = new xpinnWSEstadoCuenta.SolicitudPersonaAfi();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient AppService = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    public static string baseUrl;
    public static string ReCaptcha_Key = "6LfMUkgUAAAAAKwJw4dzMXAUBFrMlDSyZ64Ngiza";
    public static string ReCaptcha_Secret = "6LfMUkgUAAAAAFq-rB2Gn6G1TONmNPvzkuqs0T9a";

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            TextBox identificacion = Master.FindControl("IDENTIFICACION") as TextBox;            
        }
        catch (Exception ex)
        {

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            baseUrl = Server.MapPath("~");
            panelFinal.Visible = false;
            if (ConfigurationManager.AppSettings["Empresa"] != null)
            {
                string empresa = ConfigurationManager.AppSettings["Empresa"].ToString();
                ent.InnerText = empresa;
                if (empresa == "FECEM")
                {
                    descargarFormato();
                    doc_afilia.Visible = true;
                    infoAsesores.Visible = true;
                    dataFormulario.Visible = true;
                    dataOtros.Visible = false;
                }
                else
                {                    
                    doc_afilia.Visible = false;
                    dataFormulario.Visible = false;
                    dataOtros.Visible = true;
                }
            }
            else
            {
                ent.InnerText = "la entidad";
                doc_afilia.Visible = false;
            }
        }
        btnContinuar.Click += btnContinuarMen_Click;        
    }

    #region METHOD RECAPTCHA

    [WebMethod]
    public static string VerifyCaptcha(string response)
    {
        string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
        return (new WebClient()).DownloadString(url);
    }

    #endregion


    protected Boolean validarDatos()
    {
        // VALIDACION DE RECAPTCHA
        //if (string.IsNullOrEmpty(txtCaptcha.Text))
        //{
        //    lblError.Text = "Metodo captcha inválido..!";
        //    return false;
        //}

        return true;
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        //CARGA DATOS

        if (Session["afiliacion"] != null)
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

            lblError.Text = "";
            try
            {
                if (validarDatos())
                    ctlMensaje.MostrarMensaje("Desea grabar la información?");
                else
                    txtCaptcha.Text = string.Empty;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }        
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["afiliacion"] != null)
            {
                pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;
                //ALMACENAR INFORMACION --FINALIZA LA SOLICITUD
                pEntidad = EstadoServicio.CrearSolicitudPersonaAfi(pEntidad, 8, Session["sec"].ToString());
                //ALMACENAR INFORMACION
                Session["afiliacion"] = null;

                if (pEntidad.id_persona != 0)
                {
                    lblCodigoGenerado.Text = pEntidad.id_persona.ToString();
                    pnlData.Visible = false;
                    panelFinal.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "No se pudieron grabar los datos. Error: " + ex.Message;
            txtCaptcha.Text = string.Empty;
        }
    }

    protected void btnSolicitudCred_Click(object sender, EventArgs e)
    {
        if (lblCodigoGenerado.Text != "")
        {
            Response.Redirect("~/Pages/Credito/Solicitud/Credito.aspx?id=" + lblCodigoGenerado.Text.Trim());
        }
        else
            lblError.Text = "Ocurrio un error al redireccionar a la pagina.";
    }

    protected void btnInicioSesion_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }   

    public void descargarFormato()
    {
        try
        {
            if (Session["afiliacion"] != null)
                pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

            string doc = EstadoServicio.ObtenerFormato(pEntidad.identificacion, 2, Session["sec"].ToString());
            if (!string.IsNullOrEmpty(doc))
            {
                Byte[] archivo = GetPDF(doc);
                //Byte[] archivo = Encoding.ASCII.GetBytes(doc);
                string base64String = Convert.ToBase64String(archivo, 0, archivo.Length);
                doc_afilia.HRef = "data:application/pdf;base64," + base64String;                
            }
        }
        catch (Exception ex)
        {
            doc_afilia.HRef = "./../../files/formato_afiliacion.pdf";
        }
    }

    public byte[] GetPDF(string pHTML)
    {
        byte[] bPDF = null;

        MemoryStream ms = new MemoryStream();
        TextReader txtReader = new StringReader(pHTML);

        // 1: create object of a itextsharp document class
        Document doc = new Document(PageSize.A4, 25, 25, 25, 25);

        // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
        PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

        // 3: we create a worker parse the document
        HTMLWorker htmlWorker = new HTMLWorker(doc);

        // 4: we open document and start the worker on the document
        doc.Open();
        htmlWorker.StartDocument();

        // 5: parse the html into the document
        htmlWorker.Parse(txtReader);

        // 6: close the document and the worker
        htmlWorker.EndDocument();
        htmlWorker.Close();
        doc.Close();

        bPDF = ms.ToArray();

        return bPDF;
    }


    public string Bytes_A_Archivo(string id, Byte[] ImgBytes)
    {
        Stream stream = null;
        string fileName = Server.MapPath("~\\files\\pdf\\") + Path.GetFileName(id + ".pdf");
        if (ImgBytes != null)
        {
            try
            {
                // Guardar imagen en un archivo
                stream = File.OpenWrite(fileName);
                foreach (byte b in ImgBytes)
                {
                    stream.WriteByte(b);
                }
                stream.Close();
                //this.hdFileName.Value = Path.GetFileName(id + ".pdf");
            }
            finally
            {
                /*Limpiamos los objetos*/
                stream.Dispose();
                stream = null;
            }
        }
        fileName = "~/files/pdf/" + Path.GetFileName(id + ".pdf");
        return fileName;
    }


    protected void btnVolver_Click(object sender, EventArgs e)
    {
        if (ConfigurationManager.AppSettings["Empresa"] != null)
        {
            string empresa = ConfigurationManager.AppSettings["Empresa"].ToString();
            if (empresa == "FECEM")
            {
                Response.Redirect("R10_Temas.aspx");
            }
            else
            {
                Response.Redirect("R06_Financiera.aspx");
            }
        }
        else
        {
            Response.Redirect("R06_Financiera.aspx");
        }
    }
}


