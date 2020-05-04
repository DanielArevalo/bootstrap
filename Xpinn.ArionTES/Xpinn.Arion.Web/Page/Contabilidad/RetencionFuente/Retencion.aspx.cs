using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.CSharp;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using iTextSharp.text.pdf;
using iTextSharp.text;

public partial class Retencion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string sArchivo = Server.MapPath("output.pdf");
        string sDestino = Server.MapPath("print.pdf");
        byte[] data = File.ReadAllBytes(sArchivo);

        if (File.Exists(sDestino))
        {
            File.Delete(sDestino);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Imprimirlo
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(data);
        int pageCount = reader.NumberOfPages;
        iTextSharp.text.Rectangle pageSize = reader.GetPageSize(1);
        Document pdf = new Document(pageSize);
        PdfWriter writer = PdfWriter.GetInstance(pdf, new FileStream(sDestino, FileMode.Create));
        pdf.Open();
        PdfAction jAction = PdfAction.JavaScript("javascript:print('" + sDestino + "')", writer);
        writer.AddJavaScript(jAction);
        for (int i = 0; i < pageCount; i++)
        {
            pdf.NewPage();
            PdfImportedPage page = writer.GetImportedPage(reader, i + 1);
            writer.DirectContent.AddTemplate(page, 0, 0);
        }
        pdf.Close();
        reader.Close();
        writer.Close();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// Abrir el PDF y guardarlo en una variable para poder mostrarlo en pantalla
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        File.WriteAllBytes(sDestino, data);
        // Determinar configuraciòn del tipo de archivo
        Response.Buffer = true;
        Response.Clear();
        Response.ContentType = "application/pdf";
        // Mostrar el archivo
        Response.BinaryWrite(data);
        // Eliminando la variable de sesion
        Response.End();
    }
}