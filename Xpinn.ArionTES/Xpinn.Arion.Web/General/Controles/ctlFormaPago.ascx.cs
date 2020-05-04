using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void ctlFormaPago_ActionsDelegate(object sender, EventArgs e);

public partial class ctlFormaPago : System.Web.UI.UserControl
{
    public event ctlFormaPago_ActionsDelegate eventoSelectedIndexChanged;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rfvFormaPago.Enabled = false;            
        }
    }

    public string Value
    {
        set { ddlFormaPago.SelectedValue = value; }
        get { return ddlFormaPago.SelectedValue; }

    }

    public string Text
    {
        set { ddlFormaPago.SelectedItem.Text = value; }
        get { return ddlFormaPago.SelectedItem.Text; }

    }

    public bool Requerido
    {
        set { if (rfvFormaPago != null) rfvFormaPago.Enabled = value; }
        get { return rfvFormaPago.Enabled; }

    }

    public Unit Width
    {
        set { ddlFormaPago.Width = value; }
        get { return ddlFormaPago.Width; }

    }

    public bool AutoPostBack
    {
        set { ddlFormaPago.AutoPostBack = value; }
        get { return ddlFormaPago.AutoPostBack; }

    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (eventoSelectedIndexChanged != null)
            eventoSelectedIndexChanged(sender, e);
    }
}