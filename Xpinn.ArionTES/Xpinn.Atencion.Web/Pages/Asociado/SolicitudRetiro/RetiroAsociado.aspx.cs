using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class RetiroAsociado : GlobalWeb
{
    xpinnWSCredito.WSCreditoSoapClient BOCredito = new xpinnWSCredito.WSCreditoSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient BOFinancial = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient BOEstadoCuenta = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ValidarSession();
            VisualizarTitulo(OptionsUrl.SolicitarRetiroAsociado, "Aso");
            Site toolBar = (Site)Master;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("SolicitudRetiro", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            obtenerDatos();
        }
    }

    private void obtenerDatos()
    {
        txtFechaSolicitud.Text = DateTime.Today.ToString("dd/MM/yyyy");
        xpinnWSLogin.Persona1 Datospersona = (xpinnWSLogin.Persona1)Session["persona"];

        xpinnWSCredito.Persona Persona = BOCredito.ConsultarPersona(Datospersona.cod_persona, Datospersona.clavesinecriptar, Session["sec"].ToString());
        if (Persona.PrimerNombre != "" && Persona.PrimerApellido != "")
            Persona.PrimerNombre = Persona.PrimerNombre.Trim() + " " + Persona.PrimerApellido.Trim();
        Persona.SegundoNombre = Persona.Ciudad.nomciudad;
        List<xpinnWSCredito.Persona> lstData = new List<xpinnWSCredito.Persona>();
        lstData.Add(Persona);
        frvData.DataSource = lstData;
        frvData.DataBind();

        txtCodPersona.Text = Datospersona.cod_persona.ToString();

        cargarMotivos();
    }

    private void cargarMotivos()
    {        
        xpinnWSLogin.Persona1 Datospersona = (xpinnWSLogin.Persona1)Session["persona"];
        xpinnWSAppFinancial.Motivo motivo = new xpinnWSAppFinancial.Motivo();
        List<xpinnWSAppFinancial.Motivo> lstMotivo = BOFinancial.ListarMotivosRetiro(motivo, Session["sec"].ToString());
        
        ddlMotivo.DataTextField = "Descripcion";
        ddlMotivo.DataValueField = "Codigo";
        ddlMotivo.DataSource = lstMotivo;
        ddlMotivo.DataBind();
        ddlMotivo.SelectedValue = "1";
        ddlMotivo.Enabled = false;
    }

    /// <summary>
    /// Valida la sesión del usuario 
    /// </summary>
    public void ValidarSession()
    {
        if (Session["persona"] == null)
            Response.Redirect("~/Pages/Account/FinSesion.htm");
    }


    protected void frvData_DataBound(object sender, EventArgs e)
    {
        Label lblFechaAfiliacion = (Label)frvData.FindControl("lblFechaAfiliacion");
        if (lblFechaAfiliacion != null)
        {
            if (!string.IsNullOrWhiteSpace(lblFechaAfiliacion.Text))
            {
                if (Convert.ToDateTime(lblFechaAfiliacion.Text) == DateTime.MinValue)
                    lblFechaAfiliacion.Text = "";
            }
        }
    }

    protected void btnGuardarSolicitud_Click(object sender, EventArgs e)
    {
        if (validarRespuestas(rbPregunta1) &&
            validarRespuestas(rbPregunta2) &&
            validarRespuestas(rbPregunta3) &&
            validarRespuestas(rbPregunta4) &&
            validarRespuestas(rbPregunta6) &&
            validarRespuestas(rbPregunta7) &&
            validarRespuestas(rbPregunta8) &&
            validarRespuestas(rbPregunta9) &&
            ddlPregunta5.SelectedValue != "0")
        {
            //Crea el objeto con los datos de la solicitud
            xpinnWSLogin.Persona1 Datospersona = (xpinnWSLogin.Persona1)Session["persona"];
            xpinnWSEstadoCuenta.Persona1 personaRetiro = new xpinnWSEstadoCuenta.Persona1();
            personaRetiro.cod_persona = Datospersona.cod_persona;
            personaRetiro.fecha_retiro = DateTime.ParseExact(txtFechaSolicitud.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); 
            personaRetiro.Observaciones = "";
            personaRetiro.estado = "0";
            personaRetiro.cod_motivo = Convert.ToInt32(ddlMotivo.SelectedValue);
            //Almacena la solicitud mediante el webservice        
            int id = BOEstadoCuenta.InsertarSolicitudRetiro(personaRetiro, Session["sec"].ToString());
            if(id != 0)
            {
                AlmacenarRespuestas(id);
                lblError.Text = "Solicitud enviada correctamente";
                lblError.Visible = true;
                pnldatosRetiro.Visible = false;
            }
            else
            {
                lblError.Text = "Se presentó un error, intentelo más tarde";
                lblError.Visible = true;
            }
        }
        else
        {
            lblError.Text = "Por favor responda todas las preguntas";
            lblError.Visible = true;
        }
    }

    /// <summary>
    /// Valida que esten contestadas todas las preguntas
    /// </summary>
    /// <returns></returns>
    private bool validarRespuestas(RadioButtonList rb)
    {
        if (string.IsNullOrWhiteSpace(rb.SelectedValue))
            return false;                

        return true;
    }

    private void AlmacenarRespuestas(int id)
    {
        List<xpinnWSEstadoCuenta.Persona1> respuestas = new List<xpinnWSEstadoCuenta.Persona1>();
        //Pregunta 1 a 4
        respuestas.Add(obtenerSeleccionados(id, lblPregunta1, rbPregunta1));//respuesta 1
        respuestas.Add(obtenerSeleccionados(id, lblPregunta2, rbPregunta2));//respuesta 2
        respuestas.Add(obtenerSeleccionados(id, lblPregunta3, rbPregunta3));//respuesta 3
        respuestas.Add(obtenerSeleccionados(id, lblPregunta4, rbPregunta4));//respuesta 4        
        //Pregunta 5
        xpinnWSEstadoCuenta.Persona1 item5 = new xpinnWSEstadoCuenta.Persona1();
        item5.id_solicitud = id;
        item5.pregunta = lblPregunta5.Text;
        item5.respuesta = ddlPregunta5.SelectedValue;
        respuestas.Add(item5);
        //Pregunta 6 a 9
        respuestas.Add(obtenerSeleccionados(id, lblPregunta6, rbPregunta6));//respuesta 1
        respuestas.Add(obtenerSeleccionados(id, lblPregunta7, rbPregunta7));//respuesta 2
        respuestas.Add(obtenerSeleccionados(id, lblPregunta8, rbPregunta8));//respuesta 3
        respuestas.Add(obtenerSeleccionados(id, lblPregunta9, rbPregunta9));//respuesta 4        

        //Almacena las respuestas obtenidas
        if (respuestas.Count > 0)
            BOEstadoCuenta.InsertarRespuestasRetiro(respuestas, Session["sec"].ToString());
    }

    private xpinnWSEstadoCuenta.Persona1 obtenerSeleccionados(int id_sol, Label pregunta, RadioButtonList opciones)
    {
        try
        {
            xpinnWSEstadoCuenta.Persona1 item1 = new xpinnWSEstadoCuenta.Persona1();
            item1.id_solicitud = id_sol;
            item1.pregunta = pregunta.Text;
            item1.respuesta = opciones.SelectedValue;
            if(opciones.ID == "rbPregunta4")
            {
                if(!string.IsNullOrEmpty(txtOtras.Text))
                    item1.respuesta = item1.respuesta + " - " + txtOtras.Text;
            }
            return item1;
        }
        catch (Exception)
        {
            return null;
        }
    }
}