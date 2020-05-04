using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class General_Controles_ctlOficinaCon : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public void Inicializar()
    {
        Xpinn.FabricaCreditos.Services.OficinaService oficinaService = new Xpinn.FabricaCreditos.Services.OficinaService();
        Xpinn.FabricaCreditos.Entities.Oficina oficina = new Xpinn.FabricaCreditos.Entities.Oficina();

        Usuario usuap = (Usuario)Session["usuario"];

        int cod = Convert.ToInt32(usuap.codusuario);
        int consulta = oficinaService.UsuarioPuedeConsultarCreditosOficinas(cod, (Usuario)Session["Usuario"]);
        ddlOficina.AppendDataBoundItems = true;
        if (consulta >= 1)
        {
            ddlOficina.DataSource = oficinaService.ListarOficinas(oficina, (Usuario)Session["usuario"]);
            ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlOficina.DataTextField = "nombre";
            ddlOficina.DataValueField = "codigo";
            ddlOficina.DataBind();
            ddlOficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
            ddlOficina.Enabled = true;
        }
        else
        {
            ddlOficina.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlOficina.Items.Insert(1, new ListItem(Convert.ToString(usuap.nombre_oficina), Convert.ToString(usuap.cod_oficina)));
            ddlOficina.DataBind();
            ddlOficina.SelectedValue = Convert.ToString(usuap.cod_oficina);
            ddlOficina.Enabled = false;
        }
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
    public int SelectedIndex
    { 
        set{ddlOficina.SelectedIndex=value;}
        get { return ddlOficina.SelectedIndex; }    
    }
    public string SelectedValue
    {
        set { ddlOficina.SelectedValue = Value; }
        get { return ddlOficina.SelectedValue; }
    }

    public string SelectedItem
    {
        set { ddlOficina.SelectedItem.Text = value; }
        get { return ddlOficina.SelectedItem.Text; }
    
    }

}