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

public partial class ImpresionTarjeta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PdfReader reader = new PdfReader(Server.MapPath("output.pdf"));
        PdfStamper stamper = new PdfStamper(reader, new FileStream(Server.MapPath("print.pdf"), FileMode.Create));
        AcroFields fields = stamper.AcroFields;
        stamper.JavaScript = "this.print(true);\r";
        stamper.FormFlattening = true;
        stamper.Close();
        reader.Close();
        // Abrir el PDF y guardarlo en una variable
        FileStream fs = File.OpenRead(Server.MapPath("print.pdf"));
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
}