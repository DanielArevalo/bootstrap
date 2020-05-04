using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Afiliacion_Default : System.Web.UI.Page
{
    xpinnWSEstadoCuenta.SolicitudPersonaAfi pEntidad = new xpinnWSEstadoCuenta.SolicitudPersonaAfi();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();

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
        if (!Page.IsPostBack)
        {            
            lblError.Text = "";
            cargarDatos();
        }
    }

    private void cargarDatos()
    {
        if (Session["afiliacion"] != null)
            pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

        //DATOS PEP
        ChkRecursosPublicos.SelectedValue = pEntidad.admrecursos.ToString();
        ChkPeps.SelectedValue = pEntidad.peps.ToString();
        ChkFuncionPublica.SelectedValue = pEntidad.funcion_publica.ToString();
        ChkFamiliares.SelectedValue = pEntidad.familiares_cargos_pub.ToString();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["afiliacion"] != null)
                pEntidad = Session["afiliacion"] as xpinnWSEstadoCuenta.SolicitudPersonaAfi;

            //DATOS PEP
            pEntidad.admrecursos = Convert.ToInt32(ChkRecursosPublicos.SelectedValue);
            pEntidad.peps = Convert.ToInt32(ChkPeps.SelectedValue);
            pEntidad.funcion_publica = Convert.ToInt32(ChkFuncionPublica.SelectedValue);
            pEntidad.familiares_cargos_pub = Convert.ToInt32(ChkFamiliares.SelectedValue);

            //ALMACENAR INFORMACION
            pEntidad = EstadoServicio.CrearSolicitudPersonaAfi(pEntidad, 5, Session["sec"].ToString());
            //ALMACENAR INFORMACION
            Session["afiliacion"] = pEntidad;


            Response.Redirect("R06_Financiera.aspx");
        }
        catch (Exception)
        {
        }
    }
}