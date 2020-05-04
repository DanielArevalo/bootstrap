﻿using System;
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
        set { txtFechaIngreso.Text = value.ToString("dd/MM/yyyy"); }
        get {
            DateTime pFecha = DateTime.MinValue;
            if (txtFechaIngreso.Text.Trim() == "")
                pFecha = DateTime.MinValue;
            else
                pFecha = DateTime.ParseExact(txtFechaIngreso.Text, "dd/MM/yyyy", null); 
            return pFecha;
        }     
    }

    public string Text
    {
        set { txtFechaIngreso.Text = value; }
        get { return txtFechaIngreso.Text; }

    }

    public Unit Width_
    {
        get { return txtFechaIngreso.Width; }
        set { txtFechaIngreso.Width = value; }
    }

    /// <summary>
    /// Método para convertir la fecha a formato corto
    /// </summary>
    public String ToDate {
        get { return Convert.ToDateTime(this.ToDateTime).ToString("dd/MM/yyyy"); } 
    }

    /// <summary>
    /// Método para convertir la fecha a caracter
    /// </summary>
    public String Texto
    {
        get { return Convert.ToDateTime(txtFechaIngreso.Text).ToString("dd/MM/yyyy"); }
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

    public bool Requerido
    {
        set { rfvFecha.Enabled = value; }
    }

}