using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controles_Contenido : System.Web.UI.UserControl
{
    xpinnWSLogin.WSloginSoapClient BOAcceso = new xpinnWSLogin.WSloginSoapClient();
    xpinnWSLogin.Persona1 pPersona;
    protected void Page_Load(object sender, EventArgs e)
    {       
        CrearContenido();
    }


    private void CrearContenido()
    {
        try
        {
            List<xpinnWSLogin.Contenido> contenido = new List<xpinnWSLogin.Contenido>();
            contenido = ObtenerContenido();
            if(contenido != null && contenido.Count > 0)
            {                
                phContenido.Controls.Add(new LiteralControl("<ul class='sidebar-menu opt'>"));
                foreach (xpinnWSLogin.Contenido item in contenido)
                {
                    string UrlFull = ResolveUrl("~/Pages/Inicio/Contenido.aspx?rta={0}");
                    phContenido.Controls.Add(new LiteralControl("<li class='opt1'>"));
                    UrlFull = item.cod_opcion > 0 ? UrlFull.Replace("{0}", item.cod_opcion.ToString()) : UrlFull.Replace("{0}","0");
                    phContenido.Controls.Add(new LiteralControl("<a href='" + UrlFull + "'>"));
                    phContenido.Controls.Add(new LiteralControl("<i class='glyphicon glyphicon-list-alt'></i>"));
                    phContenido.Controls.Add(new LiteralControl("<span>" + item.nombre + "</span>"));
                    phContenido.Controls.Add(new LiteralControl("</a>"));
                    phContenido.Controls.Add(new LiteralControl("</li>"));
                }
                phContenido.Controls.Add(new LiteralControl("</ul>"));
            }            
        }
        catch (Exception ex)
        {
            throw new Exception("ctrl_menu.CrearContenido: " + ex.Message);
        }
    }

    private List<xpinnWSLogin.Contenido> ObtenerContenido()
    {
        try
        {            
            if (Session["persona"] == null)
                Response.Redirect("~/Pages/Account/FinSesion.htm");

            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            List<xpinnWSLogin.Contenido> lstContenido = new List<xpinnWSLogin.Contenido>();
            xpinnWSLogin.Contenido cont = new xpinnWSLogin.Contenido();
            lstContenido = BOAcceso.ListarContenido(cont, Session["sec"].ToString());
            return lstContenido;
        }
        catch (Exception ex)
        {
            throw new Exception("ctrl_menu.ObtenerContenidos: " + ex.Message);
        }
    }
}