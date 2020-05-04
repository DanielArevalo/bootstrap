using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Afiliacion_Default : System.Web.UI.Page
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient AppService = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    xpinnWSEstadoCuenta.SolicitudPersonaAfi pEntidad = new xpinnWSEstadoCuenta.SolicitudPersonaAfi();
    xpinnWSLogin.Persona1 DataPersona = new xpinnWSLogin.Persona1();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            TextBox identificacion = Master.FindControl("IDENTIFICACION") as TextBox;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string Nomempresa = ConfigurationManager.AppSettings["Empresa"] != null ?
        ConfigurationManager.AppSettings["Empresa"].ToString() : "la entidad";
        empresa.InnerText = Nomempresa;
        btnContinuar.Click += btnContinuar_Click;
        if (!Page.IsPostBack)
        {
            cargarTemas();
            if (Session["afiliacion"] != null)
            {

            }
            else if (Session["persona"] != null)
            {
                Label titulo = this.Master.FindControl("Lbltitulo") as Label;
                titulo.Text = "Actualización de datos";
            }
        }
    }

    private void cargarTemas()
    {
        //LLenar temas
        List<xpinnWSAppFinancial.Actividades> lstTemas = new List<xpinnWSAppFinancial.Actividades>();
        lstTemas = AppService.ListarTemasInteres();
        if (lstTemas != null)
        {
            cbTemas.DataSource = lstTemas;
            cbTemas.DataValueField = "idactividad";
            cbTemas.DataTextField = "descripcion";
            cbTemas.DataBind();
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        guardarTemas();
        if (Session["afiliacion"] != null)
        {
            Response.Redirect("R11_Documentos.aspx");
        }
        else if (Session["persona"] != null)
        {
            DataPersona = Session["persona"] as xpinnWSLogin.Persona1;
            Response.Redirect("~/Index.aspx", false);
        }        
    }

    private void guardarTemas()
    {
        string ident = "";
        long cod = 0;
        if (Session["persona"] != null)
        {
            DataPersona = Session["persona"] as xpinnWSLogin.Persona1;
            if(!string.IsNullOrEmpty(DataPersona.identificacion)) ident = DataPersona.identificacion;
            if (DataPersona.cod_persona != 0) cod = DataPersona.cod_persona;
        }
        else if (Session["afiliacion"] != null)
        {
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;
            if (!string.IsNullOrEmpty(pEntidad.identificacion)) ident = pEntidad.identificacion;
            cod = 0;
        }

        List<xpinnWSEstadoCuenta.Persona1> lstTema = new List<xpinnWSEstadoCuenta.Persona1>();
        foreach (ListItem li in cbTemas.Items)
        {
            if (li.Selected)
            {
                xpinnWSEstadoCuenta.Persona1 item = new xpinnWSEstadoCuenta.Persona1();
                item.id_solicitud = Convert.ToInt32(li.Value);
                item.identificacion = ident;
                item.cod_persona = cod;
                if (li.Text == "Otro")
                    item.descripcion = txtOtroTema.Text;
                else
                    item.descripcion = "";
                lstTema.Add(item);
            }
        }
        if (lstTema != null && lstTema.Count > 0)
        {
            EstadoServicio.guardarPersonaTema(lstTema, Session["sec"].ToString());
        }
    }
}