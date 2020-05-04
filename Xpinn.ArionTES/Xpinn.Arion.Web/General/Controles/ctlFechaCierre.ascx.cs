using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class ctlFechaCierre : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rfvFechaCierre.Enabled = false;
        }
    }

    public void Inicializar(string tipo,string estado)
    {
        // Llena el DDL de la periodicidad
        List<Xpinn.Comun.Entities.Cierea> lstCiereaFecha = new List<Xpinn.Comun.Entities.Cierea>();
        Xpinn.Comun.Services.CiereaService CiereaServicio = new Xpinn.Comun.Services.CiereaService();
        Configuracion conf = new Configuracion();
        lstCiereaFecha = CiereaServicio.ListarCiereaFecha(tipo, estado, (Usuario)Session["Usuario"]);
        Session["Ordenar"] = lstCiereaFecha;
        ddlFechaCierre.DataSource = lstCiereaFecha;
        ddlFechaCierre.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCierre.DataTextField = "FECHA";
        ddlFechaCierre.DataValueField = "FECHA";
        ddlFechaCierre.DataBind();
        if (Session["FechaCierre"] != null)
            ddlFechaCierre.SelectedValue = Session["FechaCierre"].ToString();
        Session.Remove("FechaCierre");
    }

    public void Limpiar()
    {
        ddlFechaCierre.Items.Clear();
        ddlFechaCierre.DataSource= null;
        ddlFechaCierre.DataBind();
        ddlFechaCierre.Items.Insert(0,new ListItem("Seleccione un item","0"));
    }


    public void LimpiarSeleccion()
    {
        ddlFechaCierre.SelectedIndex = 0;
    }


    public void ordenar(string tipo)
    {
        if(tipo=="D")
            ddlFechaCierre.DataSource = ((List<Xpinn.Comun.Entities.Cierea>)Session["Ordenar"]).OrderByDescending(x => x.fecha);
        else
            ddlFechaCierre.DataSource = ((List<Xpinn.Comun.Entities.Cierea>)Session["Ordenar"]).OrderBy(x => x.fecha);
        Configuracion conf = new Configuracion();
        ddlFechaCierre.DataTextFormatString = "{0:" + conf.ObtenerFormatoFecha() + "}";
        ddlFechaCierre.DataTextField = "FECHA";
        ddlFechaCierre.DataValueField = "FECHA";
        ddlFechaCierre.DataBind();
        if (Session["FechaCierre"] != null)
            ddlFechaCierre.SelectedValue = Session["FechaCierre"].ToString();
        Session.Remove("FechaCierre");
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

    public DateTime? fecha_cierre
    {
        get
        {            
            if (ddlFechaCierre.SelectedValue != null)
                if (ddlFechaCierre.SelectedValue != "0")
                    return Convert.ToDateTime(ddlFechaCierre.SelectedValue);
            return null;
        }
        set
        {
            ddlFechaCierre.SelectedValue = Convert.ToString(value);
        }
    }

    public bool Requerido
    {
        set { if (rfvFechaCierre != null) rfvFechaCierre.Enabled = value; }
        get { return rfvFechaCierre.Enabled; }

    }

    public Unit Width
    {
        set { rfvFechaCierre.Width = value; }
        get { return rfvFechaCierre.Width; }

    }

    public bool AutoPostBack
    {
        set { ddlFechaCierre.AutoPostBack = value; }
        get { return ddlFechaCierre.AutoPostBack; }

    }
    
}