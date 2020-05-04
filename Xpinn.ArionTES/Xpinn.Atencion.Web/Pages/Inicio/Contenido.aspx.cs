using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Contenido : GlobalWeb
{
    xpinnWSAppFinancial.WSAppFinancialSoapClient appService = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string valor = Convert.ToString(Request.QueryString["rta"]);
            if (!string.IsNullOrWhiteSpace(valor))
            {
                cargarContenido(Convert.ToInt64(valor));
            }
            else
            {
                Response.Redirect("~/Index.aspx?");
            }            
        }
        catch (IOException ex)
        {
            Response.Redirect("~/Index.aspx?");
        }
    }

    public void cargarContenido(Int64 opcion)
    {
        xpinnWSAppFinancial.Contenido content = new xpinnWSAppFinancial.Contenido();
        content = appService.obtenerContenido(opcion, Session["sec"].ToString());
        if(content != null)
        {
            VisualizarTitulo(content.nombre.ToUpper());
            string html = content.html;
            if (!string.IsNullOrEmpty(html))
                contenido.Controls.Add(new LiteralControl(html));
            else
                Response.Redirect("~/Index.aspx?");
        }
        else
            Response.Redirect("~/Index.aspx?");

    }
}                        



