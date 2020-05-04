using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Afiliacion_Default : System.Web.UI.Page
{

    xpinnWSEstadoCuenta.SolicitudPersonaAfi pEntidad = new xpinnWSEstadoCuenta.SolicitudPersonaAfi();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient AppService = new xpinnWSAppFinancial.WSAppFinancialSoapClient();

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
        btnContinuar.Click += btnContinuar_Click;
        cargarDatos();
        if (ConfigurationManager.AppSettings["Empresa"] != null)
        {
            string empresa = ConfigurationManager.AppSettings["Empresa"].ToString();
            if (empresa == "FECEM")
            {
                pnlAporte.Visible = true;
            }
        }
    }

    private void cargarDatos()
    {
        if (Session["afiliacion"] != null)
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

        txtNombreAutoriza.Text = pEntidad.primer_nombre + " " + pEntidad.segundo_nombre + " " + pEntidad.primer_apellido + " " + pEntidad.segundo_apellido;
        txtCedulaAutoriza.Text = pEntidad.identificacion;
        txtCiudadAutoriza.Text = pEntidad.nom_ciudad;
        txtPagadorAutoriza.Text = pEntidad.empresa_contacto;
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if (Session["afiliacion"] != null)
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

        //Valor autorizado
        pEntidad.promedio = Convert.ToDecimal(txtValorAutoriza.Text.Replace(",","").Replace(".", "").Replace("$",""));

        Response.Redirect("R09_Informacion.aspx");
    }
}