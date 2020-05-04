using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ctlFechaHora : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        calExtFecha.Format = "dd-MM-yyyy HH':'mm':'ss";
        rfvFecha.Enabled = false;
    }

    /// <summary>
    /// Método para convertir la fecha 
    /// </summary>
    public DateTime ToDateTime
    {
        set { txtFecha.Text = value.ToString("dd-MM-yyyy HH':'mm':'ss"); }
        get { return DateTime.ParseExact(txtFecha.Text, "dd-MM-yyyy HH':'mm':'ss", null); }

    }

    /// <summary>
    /// Método para convertir la fecha a formato corto
    /// </summary>
    public String ToDate
    {
        get { return this.ToDateTime.ToString("dd-MM-yyyy HH':'mm':'ss"); }
    }

    /// <summary>
    /// Método para convertir la fecha a caracter
    /// </summary>
    public String Text
    {
        set { txtFecha.Text = value.ToString(); }
        get { return txtFecha.Text.Trim() == "" ? null : DateTime.ParseExact(txtFecha.Text, "dd-MM-yyyy HH':'mm':'ss", null).ToString("dd-MM-yyyy HH':'mm':'ss"); }
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