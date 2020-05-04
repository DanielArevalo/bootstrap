using System;
using System.IO;
using System.Web.UI;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Xpinn.Util;
using System.Web;

public partial class Reporte : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            Xpinn.Util.Usuario eUsuario = new Usuario();
            if (Session["Usuario"] != null)
                eUsuario = (Usuario)Session["Usuario"];
            if (Session["Archivo" + eUsuario.codusuario] == null)
                return;
            string sArchivo = Session["Archivo" + eUsuario.codusuario].ToString();
            
            string sDestino = Server.MapPath("print_" + eUsuario.codusuario.ToString() + ".pdf");
            string sImprimir = Server.MapPath("imprimir.pdf");
            byte[] data = File.ReadAllBytes(sArchivo);

            if (File.Exists(sDestino))
            {
                File.Delete(sDestino);
            }
            if (File.Exists(sImprimir))
            {
                File.Delete(sImprimir);
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Imprimirlo
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(data);
            int pageCount = reader.NumberOfPages;
            Rectangle pageSize = reader.GetPageSize(1);            
            Document pdf = new Document(pageSize);
            PdfWriter writer = PdfWriter.GetInstance(pdf, new FileStream(sDestino, FileMode.Create));            
            pdf.Open();
            PdfAction jAction = PdfAction.JavaScript("javascript:print('" + sImprimir + "')", writer);
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
            File.WriteAllBytes(sImprimir, data);
            // Determinar configuraciòn del tipo de archivo
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/pdf";
            // Mostrar el archivo
            Response.BinaryWrite(data);
            // Eliminando la variable de sesion
            Session.Remove("Archivo");
            Response.End();    
        }
        catch(Exception ex)
        {            
            Response.Redirect("../../Page/Reportes/Default.aspx");
        }
        
    }


}