using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Afiliacion_Register : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime pFechaActual = DateTime.Now;
        txtDiaEncabezado.Text = pFechaActual.Day.ToString();
        txtMesEncabezado.Text = pFechaActual.Month.ToString();
        txtAnioEncabezado.Text = pFechaActual.Year.ToString();
    }
}
