using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Nuevo : GlobalWeb
{
    Actividad_NominaEntitiesService service = new Actividad_NominaEntitiesService();
    string actividad;

    #region iniciales

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[service.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[service.CodigoPrograma + ".id"].ToString();
                ObtenerDatos();
            }else
            {
                inicializarEditarRegistro();
            }
        }
    }

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if(Session[service.CodigoPrograma + ".id"] == null)
            {
                VisualizarOpciones(service.CodigoPrograma, "A");
            }else
            {
                VisualizarOpciones(service.CodigoPrograma, "D");
            }

            Site toolBar = (Site)Master;
            toolBar.eventoGuardar += btn_guardar_click;
            toolBar.eventoLimpiar += btnlimpiar_Click;
            toolBar.eventoRegresar += (s, evt) =>
            {
                Session.Remove(service.CodigoPrograma + ".id");
                Navegar("Lista.aspx");
            };
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(service.CodigoPrograma, "Page_PreInit", ex);
        }

    }

    void inicializarEditarRegistro()
    {
        
    }

    #region Obtener Datos

    protected void ObtenerDatos()
    {
        try { 
        Xpinn.Nomina.Services.Actividad_NominaEntitiesService service = new Xpinn.Nomina.Services.Actividad_NominaEntitiesService();
        Xpinn.Nomina.Entities.Actividad_Nomina entities = new Xpinn.Nomina.Entities.Actividad_Nomina();

        entities.consecutivo = Convert.ToInt64(idObjeto);
        entities = service.ConsultarActividad_NominaEntities(entities.consecutivo, (Usuario)Session["usuario"]);

        if (entities != null) { 

        if (entities.consecutivo != Int64.MinValue)
            txtconsecutivo.Text = HttpUtility.HtmlDecode(entities.consecutivo.ToString().Trim());

        if (!string.IsNullOrEmpty(entities.nombre_actividad))
            txtnombre.Text = HttpUtility.HtmlDecode(entities.nombre_actividad.ToString().Trim());

        if (!string.IsNullOrEmpty(entities.objetivo))
            txtobjetivo.Text = HttpUtility.HtmlDecode(entities.objetivo.ToString().Trim());

        if (!string.IsNullOrEmpty(entities.centro_costo))
            ctlCentroCosto.Text = HttpUtility.HtmlDecode(entities.centro_costo.ToString().Trim());

        if (!string.IsNullOrEmpty(entities.cod_persona))
            ctlResponsable.Text = HttpUtility.HtmlDecode(entities.cod_persona.ToString().Trim());

        if (!string.IsNullOrEmpty(entities.fecha_inicio))
            ctlFechainicio.Text = HttpUtility.HtmlDecode(entities.fecha_inicio.ToString().Trim());

        if (!string.IsNullOrEmpty(entities.fecha_terminacion))
            ctlFechatermino.Text = HttpUtility.HtmlDecode(entities.fecha_terminacion.ToString().Trim());
        }
        else
            VerError("error de Datos");
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }



    #endregion

    #endregion

    #region Eventos Intermedios

    void btn_guardar_click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarCampos())
        {
            GuardarDatos();
        }
    }

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {
        txtnombre.Text = string.Empty;
        txtconsecutivo.Text = string.Empty;
        txtobjetivo.Text = string.Empty;
    }


    #endregion


    #region Metodos de ayuda

    Actividad_Nomina consultaActividad (Int64 idcaja)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(idcaja.ToString())) return null;
            Actividad_Nomina entiti = service.ConsultarActividad_NominaEntities(Convert.ToInt64(idcaja), Usuario);
            return entiti;
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la nomina, " + ex.Message);
            return null;
        }
    }

    void Llenaractividad (Actividad_Nomina pact)
    {
        txtconsecutivo.Text = Convert.ToInt64(pact.consecutivo).ToString();
        txtnombre.Text = pact.nombre_actividad;
        txtobjetivo.Text = pact.objetivo;
        ctlCentroCosto.Text = Convert.ToString(pact.centro_costo);
        ctlResponsable.Text = Convert.ToString(pact.cod_persona);
        ctlFechainicio.Text = pact.fecha_inicio;
        ctlFechatermino.Text = pact.fecha_terminacion;
        
    }

    void GuardarDatos()
    {
        try
        {
            Xpinn.Nomina.Services.Actividad_NominaEntitiesService service = new Xpinn.Nomina.Services.Actividad_NominaEntitiesService();
            Xpinn.Nomina.Entities.Actividad_Nomina entiti = new Xpinn.Nomina.Entities.Actividad_Nomina();

            if (idObjeto != "")
            {
                entiti = service.ConsultarActividad_NominaEntities(Convert.ToInt64(idObjeto), Usuario);
            }

            if (txtnombre.Text != "")
                entiti.nombre_actividad = (txtnombre.Text != "") ? Convert.ToString(txtnombre.Text.Trim()).ToUpper() : String.Empty;

            if (txtobjetivo.Text != "")
                entiti.objetivo = (txtobjetivo.Text != "") ? Convert.ToString(txtobjetivo.Text.Trim()).ToUpper() : String.Empty;

            if (ctlCentroCosto.Text != "")
                entiti.centro_costo = (ctlCentroCosto.Text != "") ? Convert.ToString(ctlCentroCosto.Text.Trim()) : String.Empty;

            if (ctlResponsable.Text != "")
                entiti.cod_persona = (ctlResponsable.Text != "") ? Convert.ToString(ctlResponsable.Text.Trim()) : String.Empty;

            if (ctlFechainicio.Text != "")
                entiti.fecha_inicio  = (ctlFechainicio.Text != "") ? Convert.ToString(ctlFechainicio.Text.Trim()) : String.Empty;

            if (ctlFechatermino.Text != "")
                entiti.fecha_terminacion = (ctlFechatermino.Text != "") ? Convert.ToString(ctlFechatermino.Text.Trim()) : String.Empty;
          

            if (idObjeto == "")
            {
                Actividad_Nomina pactividad = service.CrearActividad_NominaEntities(entiti, Usuario);
                entiti.consecutivo = pactividad.consecutivo;
            }
            else
            {
                Actividad_Nomina pactividad = service.ModificarActividad_NominaEntities(entiti, Usuario);
            }

           //// if (entiti.consecutivo != 0)
            //{
                Site toolBar = (Site)Master;
                toolBar.MostrarRegresar(true);
                toolBar.MostrarGuardar(false);
                toolBar.MostrarLimpiar(false);

                mvPrincipal.SetActiveView(viewGuardado);

                Session.Remove(service.CodigoPrograma + ".id");
            //}
        }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        } 
    }

    bool ValidarCampos()
    {
        Page.Validate();
        if (!Page.IsValid ||
            string.IsNullOrWhiteSpace(txtnombre.Text) ||
            string.IsNullOrWhiteSpace(txtobjetivo.Text)) 
        {
            VerError("Faltan Campos por validar");
        }
        return true;
    }

    Actividad_Nomina ObtenerDatosGuardar()
    {
        Actividad_Nomina pnomina = new Actividad_Nomina();
        if (!string.IsNullOrWhiteSpace(idObjeto))
        {
            pnomina.consecutivo = consultaActividad(Convert.ToInt64(idObjeto)).consecutivo;
        }

        pnomina.consecutivo = Convert.ToInt64(txtconsecutivo.Text);
        pnomina.nombre_actividad = txtnombre.Text;
        pnomina.objetivo = txtobjetivo.Text;
        pnomina.centro_costo = ctlCentroCosto.Text;
        pnomina.cod_persona = ctlResponsable.Text;
        pnomina.fecha_inicio = ctlFechainicio.Text;
        pnomina.fecha_terminacion = ctlFechatermino.Text;

        return pnomina;
    }


    #endregion


}