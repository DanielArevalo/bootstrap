using System;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

public partial class Reporte : System.Web.UI.Page
{
    xpinnWSLogin.WSloginSoapClient WSlogin = new xpinnWSLogin.WSloginSoapClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if (Session["Archivo"] == null)
                return;
            string sArchivo = Session["Archivo"].ToString();
            xpinnWSLogin.Persona1 pPersona = new xpinnWSLogin.Persona1();
            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            string sDestino = Server.MapPath("print_" + pPersona.cod_persona.ToString() + ".pdf");
            byte[] data = File.ReadAllBytes(sArchivo);
            
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Imprimirlo
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(data);
            int pageCount = reader.NumberOfPages;
            Rectangle pageSize = reader.GetPageSize(1);            
            Document pdf = new Document(pageSize);
            PdfWriter writer = PdfWriter.GetInstance(pdf, new FileStream(sDestino, FileMode.Create));            
            pdf.Open();
            PdfAction jAction = PdfAction.JavaScript("javascript:print('imprimir.pdf')", writer);
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
            sDestino = Server.MapPath("imprimir.pdf");
            File.WriteAllBytes(sDestino, data);
            // Determinar configuraciòn del tipo de archivo
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/pdf";
            // Mostrar el archivo
            Response.BinaryWrite(data);
            Response.End();              
            // Eliminando la variable de sesion
            Session.Remove("Archivo");           
        }
        catch 
        {
            Response.Redirect("../Reportes/Default.aspx");
        }
        
    }


}