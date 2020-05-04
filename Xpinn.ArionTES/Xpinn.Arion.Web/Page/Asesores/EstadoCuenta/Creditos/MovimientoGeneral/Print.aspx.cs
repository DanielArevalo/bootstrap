using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Print : System.Web.UI.Page
{
    private Imprimir imprimir = new Imprimir();

    protected void Page_Load(object sender, EventArgs e)
    {
        Control ctrl = (Control)Session["imprimirCtrl"];
        imprimir.PrintWebControl(ctrl);
    }
}