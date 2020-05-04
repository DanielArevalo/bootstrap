using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ctlDireccion : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hfControlText.Value))
            txtDireccion.Text = hfControlText.Value;
    }

    public string Text
    {
        set {
            txtDireccion.Text = value;
            hfControlText.Value = value;
        }
        get { return hfControlText.Value; }
    }

    public string Direccion
    {
        set { txtDireccion.Text = value; }
        get { return txtDireccion.Text.Trim(); }
    }

    // 1 = URBANA , 2 = RURAL SE AJUSTO COMO HABIA DEJADO EN UN INICIO NORVEY. ESTO PARA CRUZAR INFORMACION
    public int Tipo_Ubicacion
    {
        set { if (value == 1) rbtnDetalleZonaGeo.SelectedValue = "U"; else rbtnDetalleZonaGeo.SelectedValue = "R";}
        get { if (rbtnDetalleZonaGeo.Text == "U") return 1; else return 2; }
    }

    
}