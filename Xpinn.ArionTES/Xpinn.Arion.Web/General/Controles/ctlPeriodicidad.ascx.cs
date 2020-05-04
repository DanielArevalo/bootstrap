using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class ctlPeriodicidad : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rfvPeriodicidad.Enabled = false;
        }
    }

    public void Inicializar()
    {
        // Llena el DDL de la periodicidad
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        String ListaSolicitada = "Periodicidad";
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        ddlPeriodicidad.DataSource = lstDatosSolicitud;
        ddlPeriodicidad.DataTextField = "ListaDescripcion";
        ddlPeriodicidad.DataValueField = "ListaIdStr";
        ddlPeriodicidad.DataBind();
        if (Session["Periodicidad"] != null)
            ddlPeriodicidad.SelectedValue = Session["Periodicidad"].ToString();
        Session.Remove("Periodicidad");
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

    public int? cod_periodicidad
    {
        get
        {            
            if (ddlPeriodicidad.SelectedValue != null)
                if (ddlPeriodicidad.SelectedValue != "")
                    return Convert.ToInt32(ddlPeriodicidad.SelectedValue);
            return null;
        }
        set
        {
            ddlPeriodicidad.SelectedValue = Convert.ToString(value);
        }
    }

    public bool Requerido
    {
        set { if (rfvPeriodicidad != null) rfvPeriodicidad.Enabled = value; }
        get { return rfvPeriodicidad.Enabled; }

    }

    public Unit Width
    {
        set { ddlPeriodicidad.Width = value; }
        get { return ddlPeriodicidad.Width; }

    }

    public bool AutoPostBack
    {
        set { ddlPeriodicidad.AutoPostBack = value; }
        get { return ddlPeriodicidad.AutoPostBack; }

    }
    
}