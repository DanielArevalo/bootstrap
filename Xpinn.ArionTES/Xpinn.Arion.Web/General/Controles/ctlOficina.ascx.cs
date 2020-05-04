using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class ctlOficina : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Inicializar();
        }
    }

    public void Inicializar()
    {
        rfvOficina.Enabled = false;
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
        Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
        ddlOficina.DataTextField = "nombre";
        ddlOficina.DataValueField = "cod_oficina";
        ddlOficina.DataSource = oficinaServicio.ListarOficina(oficina, pUsuario);
        ddlOficina.DataBind();
    }

    public string Value
    {
        get
        {
            string s = ViewState["Value"] as string;
            return s == null ? String.Empty : s;
        }
        set
        {
            ViewState["Value"] = value;
        }
    }

    public string Text
    {
        get
        {
            string s = ViewState["Text"] as string;
            return s == null ? String.Empty : s;
        }
        set
        {
            ViewState["Text"] = value;
        }
    }

    public bool Requerido
    {
        set { if (rfvOficina != null) rfvOficina.Enabled = value; }
        get { return rfvOficina.Enabled; }

    }

    public Unit Width
    {
        set { ddlOficina.Width = value; }
        get { return ddlOficina.Width; }

    }

    public bool AutoPostBack
    {
        set { ddlOficina.AutoPostBack = value; }
        get { return ddlOficina.AutoPostBack; }

    }


}