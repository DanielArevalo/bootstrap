using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Xpinn.Util
{
    public class ControlsHelper
    {
        // Sirve para renderizar un page como un string de HTML 
        // O puedes crear un Page programaticamente añadirle un control
        // Y obtener el HTML de ese control
        public string RenderPageAsStringHTML(Page page)
        {
            string htmlPanelMensaje;
            StringBuilder sb = new StringBuilder();

            using (StringWriter tw = new StringWriter(sb))
            using (HtmlTextWriter hw = new HtmlTextWriter(tw))
            {
                page.RenderControl(hw);
                htmlPanelMensaje = sb.ToString();
            }

            return htmlPanelMensaje;
        }


        #region No sirve ponerlo aca, pero es un codigo util que servira en algun momento


        //void MostrarArchivoEnLiteral(byte[] bytes, Literal literal)
        //{
        //    Usuario pUsuario = _usuario;

        //    string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_pagare" + pUsuario.nombre : "";
        //    // ELIMINANDO ARCHIVOS GENERADOS
        //    try
        //    {
        //        string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
        //        foreach (string ficheroActual in ficherosCarpeta)
        //            if (ficheroActual.Contains(pNomUsuario))
        //                File.Delete(ficheroActual);
        //    }
        //    catch
        //    { }

        //    FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("Archivos/output" + pNomUsuario + ".pdf"),
        //    FileMode.Create);
        //    fs.Write(bytes, 0, bytes.Length);
        //    fs.Close();
        //    //MOSTRANDO REPORTE
        //    string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"90%\" height=\"700px\">";
        //    adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
        //    adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        //    adjuntar += "</object>";

        //    literal.Text = string.Format(adjuntar, ResolveUrl("Archivos/output" + pNomUsuario + ".pdf"));
        //}


        #endregion


        //  Debes hacerle override a este metodo para evitar errores de Controles fuera de etiquetas form runat=server
        //  Y desactivar el EventValidation en el *.aspx (EventValidation = "false")
        //  public override void VerifyRenderingInServerForm(Control control)
        //  {
        //    /* Verifies that the control is rendered */  /* Dejar Vacio */
        //  }
        //
        //  Luego de llamar al metodo usar este codigo 
        //  ClientScript.RegisterStartupScript(GetType(), "GridPrint", gridHTML);
        //  pasandole el string devuelto para que se registre la impresion cuando se realize el postback
        //
        // Sirve para imprimir un control desde codigo behind el modelo servira de referencia
        // Tener en cuenta que el estado del control afecta a la renderizacion, si la gridview esta paginada
        // saldra paginada, si el panel esta oculto, no se vera el panel, 
        // CONSEJO: Colocar el control en el estado deseado para imprimir y llamar al metodo, cuando el metodo retorne
        // volver a poner el control en el estado deseado
        public string PrintControl(Control control)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            control.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'").Replace(Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();

            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");

            return sb.ToString();
        }
    }
}
