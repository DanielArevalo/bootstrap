using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fecha : System.Web.UI.UserControl
{
    
    protected void Page_Load(object sender, EventArgs e){
        if (!IsPostBack)
        {
            rfvFecha.Enabled = false;
        }
    }

    /// <summary>
    /// Método para convertir la fecha 
    /// </summary>
    public DateTime ToDateTime {
        set { txtFechaIngreso.Text = value.ToString(GlobalWeb.gFormatoFecha); }
        get {
            DateTime pFecha = DateTime.MinValue;
            if (txtFechaIngreso.Text.Trim() == "")
                pFecha = DateTime.MinValue;
            else
                try
                {
                    pFecha = DateTime.ParseExact(txtFechaIngreso.Text, GlobalWeb.gFormatoFecha, null);
                }
                catch
                {
                    pFecha = DateTime.MinValue;
                }
            return pFecha;
        }     
    }

    public string Text
    {
        set { txtFechaIngreso.Text = value; }
        get { return txtFechaIngreso.Text; }

    }
    
    /// <summary>
    /// Método para convertir la fecha a formato corto
    /// </summary>
    public String ToDate { 
        get { return Convert.ToDateTime(this.ToDateTime).ToString(GlobalWeb.gFormatoFecha); } 
    }

    /// <summary>
    /// Método para convertir la fecha a caracter
    /// </summary>
    public String Texto
    {
        get { return Convert.ToDateTime(txtFechaIngreso.Text).ToString(GlobalWeb.gFormatoFecha); }
    }

    /// <summary>
    /// Método para saber si hay datos en el control
    /// </summary>
    public Boolean TieneDatos {
        get { if (txtFechaIngreso.Text != "") return true; else return false; }
    }

    public bool Enabled
    {
        get { return txtFechaIngreso.Enabled; }
        set { txtFechaIngreso.Enabled = value; }
    }

    public short TabIndex
    {
        get { return txtFechaIngreso.TabIndex; }
        set { txtFechaIngreso.TabIndex = value; }
    }

    public bool Requerido
    {
        set { rfvFecha.Enabled = value; }
    }

    public Unit Width
    {
        get { return txtFechaIngreso.Width; }
        set { txtFechaIngreso.Width = value; }
    }
}