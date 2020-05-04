using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controles_Menu : System.Web.UI.UserControl
{
    xpinnWSLogin.WSloginSoapClient BOAcceso = new xpinnWSLogin.WSloginSoapClient();
    xpinnWSLogin.Persona1 pPersona;
    protected void Page_Load(object sender, EventArgs e)
    {       
        CrearMenu();
    }


    private void CrearMenu()
    {
        try
        {                                                                      
            ArrayList arProcesos = ObtenerProcesos();
            string iniUrl =  ResolveUrl("~/Pages");
            //phOpciones.Controls.Add(new LiteralControl("<ul class='sidebar-menu'><li class='header'>NAVEGACIÓN PRINCIPAL</li>"));
            int contadorEstilo = 0;
            phOpciones.Controls.Add(new LiteralControl("<ul class='nav navbar-nav navbar-left'>"));
            for (int c = 0; c < arProcesos.Count; c++)
            {
                phOpciones.Controls.Add(new LiteralControl("<li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown' aria-expanded='false'>"+ arProcesos[c].ToString() +"<span class='caret'></span></a><ul class='dropdown-menu'>"));
                ArrayList arOpciones = ObtenerOpciones(arProcesos[c].ToString());
                for (int p = 0; p < arOpciones.Count; p++)
                {
                    string nombre = arOpciones[p].ToString().Split('|')[0];
                    string icono = arOpciones[p].ToString().Split('|')[1];
                    string ruta = arOpciones[p].ToString().Split('|')[2];
                    //int largo = 0;

                    string urlFull = iniUrl + ruta;
                    if (ruta.Contains("{0}"))
                    {
                        xpinnWSLogin.Persona1 pPersona = (xpinnWSLogin.Persona1)Session["persona"];
                        if (ruta.Contains("cod_persona"))
                            urlFull = string.Format(urlFull, pPersona.cod_persona);
                    }
                    phOpciones.Controls.Add(new LiteralControl("<li><a href=" + urlFull + "> &nbsp; " + nombre + "</a></li>"));
                    //phOpciones.Controls.Add(new LiteralControl("< li >< asp:HyperLink runat = 'server' NavigateUrl = " + urlFull + " > &nbsp; " + nombre + " </ asp:HyperLink ></ li > "));
                    contadorEstilo++;
                    //phOpciones.Controls.Add(new LiteralControl("<li><asp:HyperLink runat='server' NavigateUrl='" + iniUrl + ruta  + "' style='font-size:small;'><i class='" + icono + "'></i> " + nombre + "</asp:HyperLink>"));
                    //HyperLink hlk = new HyperLink();
                    //hlk.ID = "hlk" + p.ToString() + nombre;
                    //largo = nombre.Length;
                    //if (largo > 25)
                    //    largo = 25;
                    //hlk.Text = nombre.Substring(0, largo);
                    //hlk.NavigateUrl = iniUrl + ruta;

                    //phOpciones.Controls.Add(hlk);
                }
                phOpciones.Controls.Add(new LiteralControl("</ul></li>"));
            }
            phOpciones.Controls.Add(new LiteralControl("</ul>"));
        }
        catch (Exception ex)
        {
            throw new Exception("ctrl_menu.CrearOpciones: " + ex.Message);
        }
    }

    private ArrayList ObtenerProcesos()
    {
        try
        {
            ArrayList arProcesos = new ArrayList();

            if (Session["persona"] == null)
                Response.Redirect("~/Pages/Account/FinSesion.htm");

            pPersona = (xpinnWSLogin.Persona1)Session["persona"];
            List<xpinnWSLogin.Acceso> lstAccesos = new List<xpinnWSLogin.Acceso>();

            if (Session["Procesos"] == null)
            {
                lstAccesos = BOAcceso.ListarAccesoAAC(24, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
                Session["Procesos"] = lstAccesos;
            }
            else
                lstAccesos = (List<xpinnWSLogin.Acceso>)Session["Procesos"];

            foreach (xpinnWSLogin.Acceso acc in lstAccesos)
            {
                Boolean estaProceso = false;

                for (int c = 0; c < arProcesos.Count; c++)
                    if (acc.nombreproceso == arProcesos[c].ToString())
                        estaProceso = true;

                if (!estaProceso)
                    arProcesos.Add(acc.nombreproceso);
            }

            return arProcesos;
        }
        catch (Exception ex)
        {
            throw new Exception("ctrl_menu.ObtenerProcesos: " + ex.Message);
        }
    }


    private ArrayList ObtenerOpciones(string pProceso)
    {
        try
        {
            ArrayList arOpciones = new ArrayList();

            if (Session["persona"] == null)
                Response.Redirect("~/Pages/Account/FinSesion.htm");
            if (Session["Procesos"] != null)
            {
                List<xpinnWSLogin.Acceso> lstAccesos = new List<xpinnWSLogin.Acceso>();
                lstAccesos = (List<xpinnWSLogin.Acceso>)Session["Procesos"];

                foreach (xpinnWSLogin.Acceso acc in lstAccesos)
                { if (acc.nombreproceso == pProceso && acc.accion == "1")
                    {
                        if (!acc.nombreopcion.Contains("|"))
                            acc.nombreopcion = acc.nombreopcion + "|fa fa-circle-o text-yellow";
                        arOpciones.Add(acc.nombreopcion + "|" + acc.ruta);
                        
                    }
                }                        
            }
            return arOpciones;
        }
        catch (Exception ex)
        {
            throw new Exception("ctrl_menu.ObtenerOpciones: " + ex.Message);
        }
    }



}