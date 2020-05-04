using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.CSharp;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;
using GenCode128;
using System.Drawing;
using System.Drawing.Imaging;
using Fath;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using iTextSharp.text.pdf;
using iTextSharp.text;

public partial class Detalle : GlobalWeb
{
    Xpinn.Asesores.Services.ImpresionTarjetaService imprimirTarjeta = new Xpinn.Asesores.Services.ImpresionTarjetaService();
    Persona1Service Persona1Servicio = new Persona1Service();
    String sTipo = "EAN128";

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[Persona1Servicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(imprimirTarjeta.CodigoPrograma, "E");
            else
                VisualizarOpciones(imprimirTarjeta.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                frmPrint.Visible = false;
                rvTarjeta.Visible = false;
                btnImprimir.Visible = false;
                if (Session[imprimirTarjeta.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[imprimirTarjeta.CodigoPrograma + ".id"].ToString();
                    Session.Remove(imprimirTarjeta.CodigoPrograma + ".id");
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
            BOexcepcion.Throw(imprimirTarjeta.CodigoPrograma, "Page_Load", ex);
        }
    }

    /// <summary>
    /// Méotodo para limpiar datos de la conuslta
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        btnImprimir.Visible = false;
        Navegar("Nuevo.aspx");
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        VerError("");
        try
        {
            if (pIdObjeto != null)
            {
                txtNombre.Text = "";
                Persona1 persona = new Persona1();
                persona = Persona1Servicio.ConsultaDatosPersona(pIdObjeto, (Usuario)Session["usuario"]);
                if (!string.IsNullOrEmpty(persona.nombre))            
                    txtNombre.Text = HttpUtility.HtmlDecode(persona.nombre);                               
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(imprimirTarjeta.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
   

    /// <summary>
    /// Método del botón para ir a imprimir el talonario del crédito
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGenerar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (txtNombre.Text == "" || txtIdentific.Text == "")
        {
            VerError("Debe seleccionar la persona para poder imprimir la tarjeta");
            return;
        }
        Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
        Session[CreditoServicio.CodigoPrograma + ".id"] = txtIdentific.Text;
        string cRutaDeImagen = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".png";
        string CadenaCodigoDeBarras, CadenaCodigoDeBarrasSinParentes;
        long identifi = Convert.ToInt64(txtIdentific.Text);
        CadenaCodigoDeBarras = "(415)0000000011937" + "(8020)" + identifi.ToString("000000000000000000000000");
        CadenaCodigoDeBarrasSinParentes = "4150000000011937" + "8020" + identifi.ToString("000000000000000000000000");
        if (sTipo == "code128")
        {
            System.Drawing.Image imgCodBarras = Code128Rendering.MakeBarcodeImage(CadenaCodigoDeBarras, 1, true);
            imgCodBarras.Save(cRutaDeImagen, System.Drawing.Imaging.ImageFormat.Png);
            hdFileName.Value = txtIdentific.Text + ".png";
            imgCodBarras.Save(Server.MapPath("Imagenes\\") + txtIdentific.Text + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
        if (sTipo == "EAN128")
        {
            // Generar código de barras
            VerError("");
            Fath.BarcodeX b = new Fath.BarcodeX();
            b.Data = CadenaCodigoDeBarrasSinParentes;
            b.Orientation = 0;
            b.Symbology = Fath.bcType.EAN128;
            b.ShowText = true;
            b.Font = new System.Drawing.Font("Arial", 8);
            b.BackColor = System.Drawing.Color.White;
            b.ForeColor = System.Drawing.Color.Black;
            int w = 300;
            int h = 80;
            try
            {
                System.Drawing.Image g = b.Image(w, h);
                g.Save(Server.MapPath("Imagenes\\") + txtIdentific.Text + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
            // Mostrar imagen con el código de barras
            imgCodigoBarras.ImageUrl = "bcx.aspx?data=" + CadenaCodigoDeBarrasSinParentes + "&identific=" + txtIdentific.Text;
            imgCodigoBarras.ImageUrl = "bcx.aspx?data=" + CadenaCodigoDeBarrasSinParentes + "&identific=" + txtIdentific.Text;
            //imgCodigoBarras.ImageUrl = "Handler.ashx?data=" + CadenaCodigoDeBarrasSinParentes + "&identific=" + txtIdentific.Text;
            hdFileName.Value = txtIdentific.Text + ".png";
            cRutaDeImagen = Server.MapPath("Imagenes\\") + txtIdentific.Text + ".png";
        }
        System.Data.DataTable table = new System.Data.DataTable();
        table.Columns.Add("identificacion");
        table.Columns.Add("nombre");
        table.Columns.Add("codigo_barras");
        table.Columns.Add("imagen_barras");
        DataRow datarw;
        datarw = table.NewRow();
        datarw[0] = txtIdentific.Text;
        datarw[1] = txtNombre.Text;
        datarw[2] = cRutaDeImagen;
        datarw[3] = CadenaCodigoDeBarras;       
        table.Rows.Add(datarw);

        ReportParameter[] param = new ReportParameter[1];
        param[0] = new ReportParameter("imagen", " ");
        rvTarjeta.LocalReport.EnableExternalImages = true;
        rvTarjeta.LocalReport.SetParameters(param);
        ReportDataSource rds1 = new ReportDataSource("DataSet1", table);
        rvTarjeta.LocalReport.DataSources.Add(rds1);
        rvTarjeta.LocalReport.Refresh();

        rvTarjeta.Visible = true;
        lblNombre.Text = txtNombre.Text;
        lblCodigoBarras.Text = CadenaCodigoDeBarras;

        txtIdentific.Enabled = false;
        frmPrint.Visible = false;
        TabsTarjeta.Visible = true;
        btnImprimir.Visible = true;
    }
    
    /// <summary>
    ///  Muestra los datos de la persona una vez se digita la identificación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtIdentific_TextChanged(object sender, EventArgs e)
    {
        rvTarjeta.Visible = false;
        ObtenerDatos(txtIdentific.Text);
    }

    /// <summary>
    /// Mètodo para imprimir la factura
    /// </summary>
    protected void ImprimirGrilla()
    {
        string printScript =
           @"function PrintGridView()
                {         
                div = document.getElementById('DivButtons');
                div.style.display='none';
                var gridInsideDiv = document.getElementById('gvDiv');
                var printWindow = window.open('gview.htm','PrintWindow','letf=0,top=0,width=550,height=300,toolbar=0,scrollbars=0,status=0');
                printWindow.document.write(gridInsideDiv.innerHTML);
                printWindow.document.close();
                printWindow.focus();
                printWindow.print();
                printWindow.close();}";
        this.ClientScript.RegisterStartupScript(Page.GetType(), "PrintGridView", printScript.ToString(), true);
        btnImprimir.Attributes.Add("onclick", "PrintGridView();");
    }


    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        // Guardar el reporte en un PDF
        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        byte[] bytes = rvTarjeta.LocalReport.Render("PDF", null, out mimeType,
                       out encoding, out extension, out streamids, out warnings);
        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        frmPrint.Visible = true;
        TabsTarjeta.Visible = false;

        ////Open existing PDF
        //Document document = new Document();        
        //PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath("output.pdf"));

        ////Getting a instance of new PDF writer
        //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(
        //   HttpContext.Current.Server.MapPath("Print.pdf"), FileMode.Create));
        //document.Open();
        //PdfContentByte cb = writer.DirectContent;

        //int i = 0;
        //int p = 0;
        //int n = reader.NumberOfPages;

        ////Add Page to new document
        //while (i < n)   
        //{
        //    document.NewPage();
        //    p++;
        //    i++;
        //    PdfImportedPage page1 = writer.GetImportedPage(reader, i);
        //    cb.AddTemplate(page1, 1, 1);
        //}

        ////Attach javascript to the document
        //PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer);
        //writer.AddJavaScript(jAction);
        //document.Close();

        ////Attach pdf to the iframe
        //frmPrint.Attributes["src"] = "Print.pdf";
    }


}