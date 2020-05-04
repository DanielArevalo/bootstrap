using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;


public delegate void ctlLineaAhorro_ActionsDelegate(object sender, EventArgs e);


public partial class ctlLineaAhorro : System.Web.UI.UserControl
{
    public event ctlLineaAhorro_ActionsDelegate eventoSelectedIndexChanged;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rfvLineaAhorro.Enabled = false;
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Xpinn.Ahorros.Services.LineaAhorroServices linahorroServicio = new Xpinn.Ahorros.Services.LineaAhorroServices();
            Xpinn.Ahorros.Entities.LineaAhorro linahorroVista = new Xpinn.Ahorros.Entities.LineaAhorro();
            ddlLineaAhorro.DataTextField = "descripcion";
            ddlLineaAhorro.DataValueField = "cod_linea_ahorro";
            ddlLineaAhorro.DataSource = linahorroServicio.ListarLineaAhorro(linahorroVista, pUsuario);
            ddlLineaAhorro.DataBind();
        }
    }

    public string Value
    {
        set { ddlLineaAhorro.SelectedValue = value; }
        get { return ddlLineaAhorro.SelectedValue; }

    }

    public string Text
    {
        set { ddlLineaAhorro.SelectedItem.Text = value; }
        get { return ddlLineaAhorro.SelectedItem.Text; }

    }

    public int Indice
    {
        set { ddlLineaAhorro.SelectedIndex = value; }
        get { return ddlLineaAhorro.SelectedIndex; }

    }

    public bool Requerido
    {
        set { if (rfvLineaAhorro != null) rfvLineaAhorro.Enabled = value; }
        get { return rfvLineaAhorro.Enabled; }

    }

    public bool Habilitado
    {
        set { ddlLineaAhorro.Enabled = value; }
        get { return ddlLineaAhorro.Enabled; }

    }

    public Unit Width
    {
        set { ddlLineaAhorro.Width = value; }
        get { return ddlLineaAhorro.Width; }

    }

    public bool AutoPostBack
    {
        set { ddlLineaAhorro.AutoPostBack = value; }
        get { return ddlLineaAhorro.AutoPostBack; }

    }

   
    protected void ddlLineaAhorro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (eventoSelectedIndexChanged != null)
            eventoSelectedIndexChanged(sender, e);
    }
}