using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void txtFechaIngreso_ActionsDelegate(object sender, EventArgs e);

public partial class fechaeditable : System.Web.UI.UserControl
{

    public event txtFechaIngreso_ActionsDelegate eventoCambiar;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Método para convertir la fecha 
    /// </summary>
    public DateTime ToDateTime
    {
        set { txtFechaIngreso.Text = value.ToShortDateString().ToString(); }
        get
        {
            if (!string.IsNullOrEmpty(txtFechaIngreso.Text))
            {
                return Convert.ToDateTime(txtFechaIngreso.Text);
            }
            else
            {
                return DateTime.MinValue;
            }
        }

    }

    /// <summary>
    /// Método para convertir la fecha a formato corto
    /// </summary>
    public String ToDate
    {
        get
        {
            if (!string.IsNullOrEmpty(txtFechaIngreso.Text))
            {
                return ToDateTime.ToShortDateString().ToString();
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Método para convertir la fecha a caracter
    /// </summary>
    public String Texto
    {
        set { txtFechaIngreso.Text = value; }
        get
        {
            if (string.IsNullOrWhiteSpace(txtFechaIngreso.Text))
            {
                return string.Empty;
            }
            else
            {
                try { return Convert.ToDateTime(txtFechaIngreso.Text).ToString(GlobalWeb.gFormatoFecha); }
                catch { return string.Empty; }
            }
        }
    }

    public string Text { set { Texto = value; } get { return Texto; } }

    /// <summary>
    /// Método para saber si hay datos en el control
    /// </summary>
    public Boolean TieneDatos
    {
        get { if (!string.IsNullOrWhiteSpace(txtFechaIngreso.Text)) return true; else return false; }
    }

    public bool Enabled
    {
        get { return txtFechaIngreso.Enabled; }
        set { txtFechaIngreso.Enabled = value; }
    }

    protected void txtFechaIngreso_TextChanged(object sender, EventArgs e)
    {
        if (eventoCambiar != null)
            eventoCambiar(sender, e);
    }
}