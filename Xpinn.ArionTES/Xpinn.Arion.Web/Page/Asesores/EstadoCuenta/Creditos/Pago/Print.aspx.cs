using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class Print : System.Web.UI.Page
{
    private Imprimir imprimir = new Imprimir();

    protected void Page_Load(object sender, EventArgs e)
    {
        Usuario pUsu = (Usuario)Session["Usuario"];
        Control ctrl = (Control)Session["imprimirCtrl"];
        imprimir.Titulo = (string)Session["Titulo"];
        string pContent = Session["Header" + pUsu.codusuario].ToString();
        LiteralControl ltContentHeader = new LiteralControl { Text = pContent };
        imprimir.ControlHeader = ltContentHeader;
        imprimir.PrintWebControl(ctrl);
    }
}