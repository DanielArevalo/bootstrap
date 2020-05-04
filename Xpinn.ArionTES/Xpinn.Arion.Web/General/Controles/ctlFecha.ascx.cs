using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ctlFecha : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        calExtFecha.Format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        rfvFecha.Enabled = false;
    }

    /// <summary>
    /// Método para convertir la fecha 
    /// </summary>
    public DateTime ToDateTime
    {
        set { txtFecha.Text = value.ToShortDateString().ToString(); }
        get { return Convert.ToDateTime(txtFecha.Text); }

    }

    /// <summary>
    /// Método para convertir la fecha a formato corto
    /// </summary>
    public String ToDate
    {
        get { return this.ToDateTime.ToShortDateString().ToString(); }
    }

    /// <summary>
    /// Método para convertir la fecha a caracter
    /// </summary>
    public String Text
    {
        set { txtFecha.Text = value.ToString(); }
        get { return txtFecha.Text.Trim() == "" ? null : Convert.ToDateTime(txtFecha.Text).ToString(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern); }
    }

    /// <summary>
    /// Método para saber si hay datos en el control
    /// </summary>
    public Boolean TieneDatos
    {
        get { if (txtFecha.Text != "") return true; else return false; }
    }

    public bool Habilitado
    {
        get { return txtFecha.Enabled; }
        set { txtFecha.Enabled = value; }
    }

    public string Validador
    {
        get { return txtFecha.ValidationGroup; }
        set { txtFecha.ValidationGroup = value; }
    }

    public bool Requerido
    {
        set { rfvFecha.Enabled = value; }
    }
}