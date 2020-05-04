using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Print : System.Web.UI.Page
{
    //private Imprimir imprimir = new Imprimir();

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    Control ctrl = (Control)Session["imprimirCtrl"];
    //    imprimir.PrintWebControl(ctrl);
    //}
    private General_Controles_EnviarCorreo imprimir = new General_Controles_EnviarCorreo();

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Collections.ArrayList correos;
        correos = new System.Collections.ArrayList();
        Control ctrl = (Control)Session["imprimirCtrl"];
        correos.Add((string)Session["imprimirCtrl2"]);
        correos.Add((string)Session["imprimirCtrl3"]);

        imprimir.PrintWebControl(ctrl, correos);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }
}