using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class ctlMoneda : System.Web.UI.UserControl
{    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
            Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
            ddlMoneda.DataSource = monedaService.ListarTipoMoneda(moneda, (Usuario)Session["Usuario"]);
            ddlMoneda.DataTextField = "descripcion";
            ddlMoneda.DataValueField = "cod_moneda";
            ddlMoneda.AppendDataBoundItems = true;
            ddlMoneda.Items.Insert(0, new ListItem("Integrador", "0"));
            try
            {
                ddlMoneda.SelectedIndex = 1;
            }
            catch
            {
                ddlMoneda.SelectedIndex = 0;
            }
            ddlMoneda.DataBind();
        }
    }

    public string Value
    {
        set { ddlMoneda.SelectedValue = value; }
        get { return ddlMoneda.SelectedValue; }

    }

    public string Text
    {
        set { ddlMoneda.SelectedItem.Text = value; }
        get { return ddlMoneda.SelectedItem.Text; }

    }

    public bool Requerido
    {
        set { if (rfvMoneda != null) rfvMoneda.Enabled = value; }
        get { return rfvMoneda.Enabled; }

    }

    public Unit Width
    {
        set { ddlMoneda.Width = value; }
        get { return ddlMoneda.Width; }

    }

    public bool AutoPostBack
    {
        set { ddlMoneda.AutoPostBack = value; }
        get { return ddlMoneda.AutoPostBack; }

    }
  
}