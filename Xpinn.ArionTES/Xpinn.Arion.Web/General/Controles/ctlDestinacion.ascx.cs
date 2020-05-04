using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class ctlDestinacion : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rfvDestinacion.Enabled = false;
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Xpinn.Ahorros.Services.DestinacionServices destinacionServicio = new Xpinn.Ahorros.Services.DestinacionServices();
            Xpinn.Ahorros.Entities.Destinacion destinacion = new Xpinn.Ahorros.Entities.Destinacion();
            ddlDestinacion.DataTextField = "descripcion";
            ddlDestinacion.DataValueField = "cod_destino";
            ddlDestinacion.DataSource = destinacionServicio.ListarDestinacion(destinacion, pUsuario);
            ddlDestinacion.DataBind();
        }
    }

    public string Value
    {
        set { ddlDestinacion.SelectedValue = value; }
        get { return ddlDestinacion.SelectedValue; }
    }

    public string Text
    {
        set { ddlDestinacion.SelectedItem.Text = value; }
        get { return ddlDestinacion.SelectedItem.Text; }

    }

    public bool Requerido
    {
        set { if (rfvDestinacion != null) rfvDestinacion.Enabled = value; }
        get { return rfvDestinacion.Enabled; }

    }

    public Unit Width
    {
        set { ddlDestinacion.Width = value; }
        get { return ddlDestinacion.Width; }

    }

    public bool AutoPostBack
    {
        set { ddlDestinacion.AutoPostBack = value; }
        get { return ddlDestinacion.AutoPostBack; }

    }

}