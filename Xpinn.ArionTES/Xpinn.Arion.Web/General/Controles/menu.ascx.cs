using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Seguridad.Entities;

public partial class ctrl_menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Session["modulo"] != null)
        {
            lblModulo.Text = ObtenerNombreModulo();
            Session["nombreModulo"] = lblModulo.Text;
            if (GlobalWeb.gmenuRetractil == "1")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script type=\"text/javascript\" src=\"<%= ResolveClientUrl(\"~/Scripts/jquery-ui-1.9.2.min.js\") %>\"></script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "<script type=\"text/javascript\" src=\"<%= ResolveClientUrl(\"~/Scripts/jquery.hoveraccordion.min.js\") %>\"></script>");
                CrearMenuRetractil();
            }
            else
            {
                CrearMenu();
            }
            ((Label)Page.Master.FindControl("lblModulo")).Text = lblModulo.Text.Trim();
        }
    }

    private void CrearMenu()
    {
        try
        {
            ArrayList arProcesos = ObtenerProcesos();
            string iniUrl = "~/Page";

            for (int c = 0; c < arProcesos.Count; c++)
            {
                phOpciones.Controls.Add(new LiteralControl("<a type='button' class='accordion'>" + arProcesos[c].ToString() + "</a><div class='panel_menu'>"));
                ArrayList arOpciones = ObtenerOpciones(arProcesos[c].ToString());

                for (int p = 0; p < arOpciones.Count; p++)
                {
                    string nombre = arOpciones[p].ToString().Split('|')[0];
                    string ruta = arOpciones[p].ToString().Split('|')[1];
                    int largo = 0;


                    HyperLink hlk = new HyperLink();
                    hlk.ID = "hlk" + p.ToString() + nombre;
                    largo = nombre.Length;
                    if (largo > 25)
                        largo = 25;
                    hlk.Text = nombre.Substring(0, largo);
                    hlk.NavigateUrl = iniUrl + ruta;

                    phOpciones.Controls.Add(hlk);
                }

                phOpciones.Controls.Add(new LiteralControl("</div>"));
            }
        }
        catch (Exception ex)
        {
            throw new Exception("ctrl_menu.CrearOpciones: " + ex.Message);
        }
    }

    private void CrearMenuRetractil()
    {
        try
        {

            ArrayList arProcesos = ObtenerProcesos();
            string iniUrl = "~/Page";

            phOpciones.Controls.Add(new LiteralControl("<ul id=\"menuPrincipal\">"));

            for (int c = 0; c < arProcesos.Count; c++)
            {
                phOpciones.Controls.Add(new LiteralControl("<li><a href=\"#\">" + arProcesos[c].ToString() + "</a>"));

                phOpciones.Controls.Add(new LiteralControl("<ul>"));
                ArrayList arOpciones = ObtenerOpciones(arProcesos[c].ToString());
                for (int p = 0; p < arOpciones.Count; p++)
                {
                    string nombre = arOpciones[p].ToString().Split('|')[0];
                    string ruta = arOpciones[p].ToString().Split('|')[1];
                    int largo = 0;

                    HyperLink hlk = new HyperLink();
                    hlk.ID = "hlk" + p.ToString() + nombre;
                    largo = nombre.Length;
                    if (largo > 25)
                        largo = 25;
                    hlk.Text = nombre.Substring(0, largo);
                    hlk.NavigateUrl = iniUrl + ruta;
                    Style accordionItemStyle = new Style();
                    accordionItemStyle.BorderWidth = 0;
                    accordionItemStyle.BorderStyle = BorderStyle.Solid;
                    accordionItemStyle.BorderColor = System.Drawing.Color.Black;
                    accordionItemStyle.BackColor = System.Drawing.Color.FromName("#359AF2");
                    accordionItemStyle.Font.Bold = false;
                    hlk.ApplyStyle(accordionItemStyle);
                    hlk.ImageUrl = "";
                    hlk.Attributes.Add("style", "text-align:left; background-image: url(../Images/gr_info.jpg); background-color: White");
                    phOpciones.Controls.Add(hlk);
                }
                phOpciones.Controls.Add(new LiteralControl("</ul>"));

                phOpciones.Controls.Add(new LiteralControl("</li>"));
            }
            phOpciones.Controls.Add(new LiteralControl("</ul>"));
            ClientScriptManager csMan;
            csMan = Page.ClientScript;
            //csMan.RegisterStartupScript(this.GetType(), "AjustarMenu", "AjustarMenu();", true);
        }
        catch (Exception ex)
        {
            throw new Exception("ctrl_menu.CrearOpciones: " + ex.Message);
        }
    }

    /// <summary>
    /// Obtiene el nombre del modulo seleccionado 
    /// </summary>
    /// <returns></returns>
    private string ObtenerNombreModulo()
    {
        try
        {
            Acceso accesos = new Acceso();
            string nombreModulo = "";

            if (Session["accesos"] != null)
            {
                List<Acceso> lstAccesos = new List<Acceso>();
                lstAccesos = (List<Acceso>)Session["accesos"];

                foreach (Acceso acc in lstAccesos)
                {
                    if (acc.cod_modulo == Convert.ToInt64(Session["modulo"]))
                        nombreModulo = acc.nom_modulo.Trim();
                }
            }

            return nombreModulo;
        }
        catch (Exception ex)
        {
            throw new Exception("ctrl_menu.ObtenerNombreModulo: " + ex.Message);
        }
    }

    /// <summary>
    /// Obtiene em conjunto de procesos del modulo seleccionado
    /// </summary>
    /// <returns></returns>
    private ArrayList ObtenerProcesos()
    {
        try
        {
            ArrayList arProcesos = new ArrayList();

            if (Session["accesos"] != null)
            {
                List<Acceso> lstAccesos = new List<Acceso>();
                lstAccesos = (List<Acceso>)Session["accesos"];
                foreach (Acceso acc in lstAccesos)
                {
                    if (acc.cod_modulo == Convert.ToInt64(Session["modulo"]))
                    {
                        Boolean estaProceso = false;

                        for (int c = 0; c < arProcesos.Count; c++)
                            if (acc.nombreproceso == arProcesos[c].ToString())
                                estaProceso = true;

                        if (!estaProceso)
                            arProcesos.Add(acc.nombreproceso);
                    }
                }
            }

            return arProcesos;
        }
        catch (Exception ex)
        {
            throw new Exception("ctrl_menu.ObtenerProcesos: " + ex.Message);
        }
    }

    /// <summary>
    /// Obtiene las opciones de un proceso
    /// </summary>
    /// <param name="pProceso">nombre del proceso</param>
    /// <returns>Conjunto de opciones del proceso</returns>
    private ArrayList ObtenerOpciones(string pProceso)
    {
        try
        {
            ArrayList arOpciones = new ArrayList();

            if (Session["accesos"] != null)
            {
                List<Acceso> lstAccesos = new List<Acceso>();
                lstAccesos = (List<Acceso>)Session["accesos"];

                foreach (Acceso acc in lstAccesos)
                    if (acc.cod_modulo == Convert.ToInt64(Session["modulo"]) && acc.nombreproceso == pProceso && acc.accion == "1")
                        arOpciones.Add(acc.nombreopcion + "|" + acc.ruta);
            }

            return arOpciones;
        }
        catch (Exception ex)
        {
            throw new Exception("ctrl_menu.ObtenerOpciones: " + ex.Message);
        }
    }
}