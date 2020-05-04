using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class Garantias : GlobalWeb
{
    xpinnWSLogin.Persona1 pPersona;
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSCredito.WSCreditoSoapClient CreditoServicio = new xpinnWSCredito.WSCreditoSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient BOFinancial = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    List<xpinnWSCredito.LineasCredito> docs = new List<xpinnWSCredito.LineasCredito>();
    List<xpinnWSCredito.Credito_Giro> lista = new List<xpinnWSCredito.Credito_Giro>();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {            
            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("SimulaciónCrédito", "Page_PreInit", ex);
        }
    }

    private void btnRegresar_Click(object sender, EventArgs e)
    {
        Navegar("~/Pages/Asociado/EstadoCuenta/Detalle.aspx");
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        xpinnWSCredito.Credito credito = new xpinnWSCredito.Credito();
        credito.numero_radicacion = Convert.ToInt64(Session["credito"].ToString());
        List<xpinnWSCredito.Documento> lstDocs = new List<xpinnWSCredito.Documento>();
        //capturar documentos checkeados
        foreach (GridViewRow rFila in gvGarantias.Rows)
        {
            xpinnWSCredito.Documento pDocu = new xpinnWSCredito.Documento();
            CheckBox chkRequerido = (CheckBox)rFila.FindControl("chkRequerido");
            if (chkRequerido != null && chkRequerido.Checked)
            {

                pDocu.tipo_documento = Convert.ToInt64(rFila.Cells[1].Text.ToString());
                lstDocs.Add(pDocu);
            }                
        }
        credito.lstDocumentos = lstDocs;
        if (lstDocs.Count > 0)
            CreditoServicio.CrearDocGarantias(credito, Session["sec"].ToString());        
        Navegar("~/Pages/Asociado/EstadoCuenta/Detalle.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if(Session["credito"] != null)
            {
                cargarDatos();
            }
        }
    }


    private void cargarDatos()
    {
        string id = Session["credito"].ToString();
        lista = CreditoServicio.listarGiros(id, Session["sec"].ToString());
        if (lista != null && lista.Count > 0)
        {
            pnlDistribucion.Visible = true;
            gvGiros.DataSource = lista;
            gvGiros.DataBind();
        }

        docs = CreditoServicio.ListarDocumentosCredito(Convert.ToInt32(id), Session["sec"].ToString());
        if(docs != null)
        {
            foreach (xpinnWSCredito.LineasCredito item in docs)
            {
                if (!string.IsNullOrEmpty(item.plantilla))
                {
                    //guarda los archivos
                    StringReader sr = new StringReader(item.plantilla.Replace("'", ""));
                    // Convertir a PDF
                    Document pdfDoc = new Document(PageSize.A4, 20f, 10f, 10f, 10f);

                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();

                        htmlparser.Parse(sr);
                        pdfDoc.Close();

                        byte[] bytes = memoryStream.ToArray();
                        string fileName = Bytes_A_Archivo(item.descripcion, bytes);
                        item.Garantia_Requerida = fileName;
                    }
                }                               
            }
            gvGarantias.DataSource = docs;
            gvGarantias.DataBind();
        }                
    }   


    protected void gvGarantias_DataBound(object sender, EventArgs e)
    {        
        xpinnWSCredito.Documento dc = new xpinnWSCredito.Documento();
        dc.numero_radicacion = Convert.ToInt32(Session["credito"].ToString());
        List<xpinnWSCredito.Documento> lstDocs = new List<xpinnWSCredito.Documento>();
        lstDocs = CreditoServicio.ListarDocGarantias(dc, Session["sec"].ToString());
        if (lstDocs != null && lstDocs.Count > 0)
        {
            Site toolBar = (Site)Master;
            toolBar.eventoGuardar -= btnGuardar_Click;
            foreach (xpinnWSCredito.Documento item in lstDocs)
            {
                foreach (GridViewRow row in gvGarantias.Rows)
                {
                    if (item.tipo_documento.ToString() == row.Cells[1].Text.ToString())
                    {
                        CheckBox chkRequerido = (CheckBox)row.FindControl("chkRequerido");
                        chkRequerido.Checked = true;
                        chkRequerido.Enabled = false;
                    }
                }
            }
        }
    }

    public string Bytes_A_Archivo(string id, Byte[] ImgBytes)
    {
        Stream stream = null;
        string fileName = Server.MapPath("..\\..\\..\\Archivos\\") + Path.GetFileName(id + ".pdf");
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
        fileName = "../../../Archivos/" + Path.GetFileName(id + ".pdf");
        return fileName;
    }
}