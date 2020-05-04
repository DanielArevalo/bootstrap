using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public delegate void ActionsDelegateM(object sender, EventArgs e);

public partial class ctlModalidad : System.Web.UI.UserControl
{
    public event ActionsDelegateM eventoSelectedIndexChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rfvModalidad.Enabled = false;          
        }
    }

    public string Value
    {
        set { ddlModalidad.SelectedValue = value; }
        get { return ddlModalidad.SelectedValue; }

    }

    public string Text
    {
        set { ddlModalidad.SelectedItem.Text = value; }
        get { return ddlModalidad.SelectedItem.Text; }

    }

    public bool Requerido
    {
        set { if (rfvModalidad != null) rfvModalidad.Enabled = value; }
        get { return rfvModalidad.Enabled; }

    }

    public Unit Width
    {
        set { ddlModalidad.Width = value; }
        get { return ddlModalidad.Width; }

    }

    public bool AutoPostBack
    {
        set { ddlModalidad.AutoPostBack = value; }
        get { return ddlModalidad.AutoPostBack; }

    }

    protected void ddlModalidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (eventoSelectedIndexChanged != null)
            eventoSelectedIndexChanged(sender, e);
    }

    public int Indice
    {
        set { ddlModalidad.SelectedIndex = value; }
        get { return ddlModalidad.SelectedIndex; }

    }

}