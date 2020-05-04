using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;


public delegate void ctlresponsable_ActionsDelegate(object sender, EventArgs e);

public partial class General_Controles_ctlResponsable : System.Web.UI.UserControl
{
    public event ctlresponsable_ActionsDelegate eventoSelectedIndexChanged;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rfrespon.Enabled = false;
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];

            Xpinn.Caja.Services.PersonaService personaservice = new Xpinn.Caja.Services.PersonaService();
            Xpinn.Caja.Entities.Persona ppersona = new Xpinn.Caja.Entities.Persona();
            ddlResponsable.DataTextField = "nom_persona";
            ddlResponsable.DataValueField = "cod_persona";
            ddlResponsable.DataSource = personaservice.ListarPersona(ppersona, pUsuario);
            ddlResponsable.DataBind();
        }
    }


    public string Value
    {
        set { ddlResponsable.SelectedValue = value; }
        get { return ddlResponsable.SelectedValue; }
    }

    public string Text
    {
        set { ddlResponsable.SelectedItem.Text = value; }
        get { return ddlResponsable.SelectedItem.Text; }
    }


    public int Indice
    {
        set { ddlResponsable.SelectedIndex = value; }
        get { return ddlResponsable.SelectedIndex; }

    }

    public bool Requerido
    {
        set { if (rfrespon != null) rfrespon.Enabled = value; }
        get { return rfrespon.Enabled; }
    }

    public bool Habilitado
    {
        set { ddlResponsable.Enabled = value; }
        get { return ddlResponsable.Enabled; }

    }

    public Unit Width
    {
        set { ddlResponsable.Width = value; }
        get { return ddlResponsable.Width; }
    }

    public bool AutoPostBack
    {
        set { ddlResponsable.AutoPostBack = value; }
        get { return ddlResponsable.AutoPostBack; }

    }

    protected void ddlLineaAhorro_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (eventoSelectedIndexChanged != null)
            eventoSelectedIndexChanged(sender, e);
    }

}