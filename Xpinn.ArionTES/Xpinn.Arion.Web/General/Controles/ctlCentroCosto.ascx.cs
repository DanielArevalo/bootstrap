using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public delegate void ctlcostos_ActionsDelegate(object sender, EventArgs e);

public partial class General_Controles_ctlCentroCosto : System.Web.UI.UserControl
{
    public event ctlcostos_ActionsDelegate eventoSelectedIndexChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rfcentrocosto.Enabled = false;
            Usuario pusuario = new Usuario();
            pusuario = (Usuario)Session["Usuario"];
            Xpinn.Contabilidad.Services.CentroCostoService costocervice = new Xpinn.Contabilidad.Services.CentroCostoService();
            Xpinn.Contabilidad.Entities.CentroCosto centrocosto = new Xpinn.Contabilidad.Entities.CentroCosto();
            ddlCentroCosto.DataTextField = "Nom_Centro";
            ddlCentroCosto.DataValueField = "Centro_Costo";
            ddlCentroCosto.DataSource = costocervice.ListarCentroCosto(centrocosto, pusuario);
            ddlCentroCosto.DataBind();
        }
    }


    public string Value
    {
        set { ddlCentroCosto.SelectedValue = value; }
        get { return ddlCentroCosto.SelectedValue; }
    }

    public string Text
    {
        set { ddlCentroCosto.SelectedItem.Text = value; }
        get { return ddlCentroCosto.SelectedItem.Text; }
    }


    public int Indice
    {
        set { ddlCentroCosto.SelectedIndex = value; }
        get { return ddlCentroCosto.SelectedIndex; }

    }

    public bool Requerido
    {
        set { if (rfcentrocosto != null) rfcentrocosto.Enabled = value; }
        get { return rfcentrocosto.Enabled; }
    }

    public bool Habilitado
    {
        set { ddlCentroCosto.Enabled = value; }
        get { return ddlCentroCosto.Enabled; }

    }

    public Unit Width
    {
        set { ddlCentroCosto.Width = value; }
        get { return ddlCentroCosto.Width; }
    }

    public bool AutoPostBack
    {
        set { ddlCentroCosto.AutoPostBack = value; }
        get { return ddlCentroCosto.AutoPostBack; }

    }

    protected void ddlLineaAhorro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (eventoSelectedIndexChanged != null)
            eventoSelectedIndexChanged(sender, e);
    }

}