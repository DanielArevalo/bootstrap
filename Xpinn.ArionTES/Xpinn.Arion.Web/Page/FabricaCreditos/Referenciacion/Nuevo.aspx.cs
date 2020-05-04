using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;
using Microsoft.ReportingServices;


partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.ReferenciaService ReferenciaServicio = new Xpinn.FabricaCreditos.Services.ReferenciaService();
    private Xpinn.FabricaCreditos.Services.ConyugeService conyugeservice = new Xpinn.FabricaCreditos.Services.ConyugeService();
    private Xpinn.FabricaCreditos.Services.ControlTiemposService ControlProcesosServicio = new Xpinn.FabricaCreditos.Services.ControlTiemposService();
    private Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();

    private String ListaSolicitada = null;
    private String FechaDatcaredito = "";
    private ReferenciaService refereniaservi = new ReferenciaService();
    private long codpersona = 0;
    private long nradicacion = 0;
    private long codconyuge = 0;
    private int conyuges;
    private string guardar = "";    
    private Page page;
    private HtmlForm form;
    private StringWriter stringWriter;
    private HtmlTextWriter htmlTxtWriter;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[ReferenciaServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(ReferenciaServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(ReferenciaServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;            
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;                         
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReferenciaServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            btnGuardarMicro.Visible = true;
            if (!IsPostBack)
            {
                Session["HojaDeRuta"] = null;
                CargarListas();
               
                // Mostrar datos del referenciados
                Usuario usuap = (Usuario)Session["usuario"];
                lblReferenciador.Text = (usuap.nombre).ToString();
                // Habilitar paneles para referencias de consumo y microcrédito
                btnGuardarMicro.Visible = true;
                PanelConsumo.Visible = false;
                PanelMicrocredito.Visible = false;                
                // Habilitar datos de la referencias de consumo
                txtTelefonoReferenciaCon.Enabled = true;
                txtDireccionReferenciaCon.Enabled = true;
                txtTipoReferenciaCon.Enabled = true;
                // Habilitar datos de la referencia de microcrédito
                txtTelefonoReferencia.Enabled = true;
                txtDireccionReferencia.Enabled = true;
                txtTipoReferencia.Enabled = true;
                txtObservacionMicrocredito.Enabled = true;
                txtTelOficinaReferencia.Enabled = true;
                txtCelularReferencia.Enabled = true;
                // Habilitar datos del conyuge
                txtNombreconyuge.Visible = true;
                txtIdentificacionconyuge.Visible = true;
                txtTelefonoconyuge.Visible = true;
                txtDirecciónconyuge.Visible = true;
                txtActividadconyuge.Visible = true;
                txtActividad_emprsaconyuge.Visible = true;
                txtNombre_empresaconyuge.Visible = true;
                txtNit_empresaconyuge.Visible = true;
                txtCargoconyuge.Visible = true;
                txtSalarioconyuge.Visible = true;
                txtAntiguedad_empresaconyuge.Visible = true;
                txtDireccion_empresaconyuge.Visible = true;
                Labeldaosconyuge.Visible = true;
                lblTitNombreConyuge.Visible = true;
                lblTitCedulaConyuge.Visible = true;
                lblTitTelefonoConyuge.Visible = true;
                lblTitDireccionConyuge.Visible = true;
                lblTitActividadConyuge.Visible = true;
                lblTitActEmpresaConyuge.Visible = true;
                lblTitEmpresaConyuge.Visible = true;
                lblTitNitEmpresaConyuge.Visible = true;
                lblTitCargoConyuge.Visible = true;
                lblTitSalarioConyuge.Visible = true;
                lblTitAntiguedadConyuge.Visible = true;
                lblTitDirEmpresaConyuge.Visible = true;
                // Habilitar para confirmar referencias conyuge
                lblTitConyuge.Visible = true;
                ddlConyugeP1.Visible = true;
                ddlConyugeP2.Visible = true;
                ddlConyugeP3.Visible = true;
                ddlConyugeP4.Visible = true;
                ddlConyugeP5.Visible = true;
                ddlConyugeP6.Visible = true;
                ddlConyugeP7.Visible = true;
                ddlConyugeP8.Visible = true;
                ddlConyuge9.Visible = true;
                ddlConyugeP10.Visible = true;
                ddlConyugeP11.Visible = true;
                // Habilitar para confirmar referencias microcrédito
                ddlRespuestaMicrocredito1.Enabled = true;
                ddlRespuestaMicrocredito2.Enabled = true;
                ddlRespuestaMicrocredito3.Enabled = true;
                ddlRespuestaMicrocredito4.Enabled = true;
                ddlRespuestaMicrocredito5.Enabled = true;
                ddlRespuestaMicrocredito6.Enabled = true;
                ddlRespuestaMicrocredito7.Enabled = true;
                ddlRespuestaMicrocredito8.Enabled = true;
                ddlRespuestaMicrocredito9.Enabled = true;
                ddlRespuestaMicrocredito10.Enabled = true;
                ddlRespuestaMicrocredito11.Enabled = true;
                ddlRespuestaMicrocredito12.Enabled = true;
                ddlRespuestaConsumo1.Enabled = true;
                // Habilitar para confirmar referencias consumo
                ddlRespuestaConsumo1.Enabled = true;
                ddlRespuestaConsumo2.Enabled = true;
                ddlRespuestaConsumo3.Enabled = true;
                ddlRespuestaConsumo4.Enabled = true;
                ddlRespuestaConsumo5.Enabled = true;
                ddlRespuestaConsumo6.Enabled = true;
                ddlRespuestaConsumo7.Enabled = true;
                ddlRespuestaConsumo8.Enabled = true;
                ddlRespuestaConsumo9.Enabled = true;
                ddlRespuestaConsumo10.Enabled = true;
                // Habilitar para confirmar referencias del deudor
                txtObservacionDeudor.Enabled = true;
                ddlDeudorP1.Enabled = true;
                ddlDeudorP2.Enabled = true;
                ddlDeudorP3.Enabled = true;
                ddlDeudorP4.Enabled = true;
                ddlDeudorP5.Enabled = true;
                ddlDeudorP6.Enabled = true;
                ddlDeudorP7.Enabled = true;
                ddlDeudorP8.Enabled = true;
                ddlDeudorP9.Enabled = true;
                ddlDeudorP10.Enabled = true;
                ddlDeudorP11.Enabled = true;                
                // Ubicarse en la carpeta de confirmación referencias deudor y/o conyuge
                Tabs.TabIndex = 1;
                // Mostrar datos
                if (Session[ReferenciaServicio.CodigoPrograma + ".id"] != null)
                {
                    if (Request.UrlReferrer != null)
                        if (Request.UrlReferrer.Segments[4].ToString() == "HojaDeRuta/")
                            Session["HojaDeRuta"] = "1";                  
                    idObjeto = Session[ReferenciaServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(ReferenciaServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    ObtenerDatosReferencia(idObjeto);
                }
              
            }
            else
            {
                btnGuardarMicro.Visible = true;             
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["HojaDeRuta"] == "1")
            Response.Redirect("~/Page/FabricaCreditos/HojaDeRuta/Lista.aspx");
        else
            {

        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Session[ReferenciaServicio.CodigoPrograma + ".id"] = idObjeto;
            Navegar(Pagina.Lista);
        }
        
          }
    }

    /// <summary>
    /// Método que permite mostrar los datos ya referenciados de la persona
    /// </summary>
    public void llenarcombospersona()
    {
        Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();
        List<Referencia> lista = new List<Referencia>();
        Referencia variable = new Referencia();
        lista = ReferenciaServicio.Listardatosrefereciacion(lblNumeroRadicacion.Text, (Usuario)Session["usuario"], 0);
        if (lista.Count == 0)
        {
            lblNoExisteReferencias.Text = "si";
        }
        else
        {
            LlenarCombosReferencias();
            if (lblEsMicrocredito.Text == "3")
            {
                PanelConsumo.Visible = false;
                PanelMicrocredito.Visible = true;
            }
            else
            {
                PanelMicrocredito.Visible = false;
                PanelConsumo.Visible = true;
            }
            txtObservacionDeudor.Enabled = false;
            ddlDeudorP1.Enabled = false;
            ddlDeudorP2.Enabled = false;
            ddlDeudorP3.Enabled = false;
            ddlDeudorP4.Enabled = false;
            ddlDeudorP5.Enabled = false;
            ddlDeudorP6.Enabled = false;
            ddlDeudorP7.Enabled = false;
            ddlDeudorP8.Enabled = false;
            ddlDeudorP9.Enabled = false;
            ddlDeudorP10.Enabled = false;
            ddlDeudorP11.Enabled = false;
            btnGuardarSolicitante.Enabled = false;

            for (int i = 0; i < lista.Count; i++)
            {
                variable = lista[i];

                if (variable.numero_pregunta == 1)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlDeudorP1.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 2)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlDeudorP2.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 3)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlDeudorP3.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 4)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlDeudorP4.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 5)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlDeudorP5.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 6)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlDeudorP6.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 7)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlDeudorP7.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 8)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlDeudorP8.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 9)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlDeudorP9.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 10)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlDeudorP10.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 11)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlDeudorP11.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 12)
                {
                    long respuesta = variable.numero_respuesta;
                }
            }                      
        }               
    }

    /// <summary>
    /// Método que permite mostrar datos ya referenciados del conyuge
    /// </summary>
    public void llenarcombosconyuge()
    {
        Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();
        List<Referencia> lista = new List<Referencia>();
        Referencia variable = new Referencia();
        lista = ReferenciaServicio.Listardatosrefereciacion(lblNumeroRadicacion.Text, (Usuario)Session["usuario"], 1);
        if (lista.Count == 0)
        {
            lblNoExisteReferencias.Text = "si";
        }
        else
        {
            ddlConyugeP1.Enabled = false;
            ddlConyugeP2.Enabled = false;
            ddlConyugeP3.Enabled = false;
            ddlConyugeP4.Enabled = false;
            ddlConyugeP5.Enabled = false;
            ddlConyugeP6.Enabled = false;
            ddlConyugeP7.Enabled = false;
            ddlConyugeP8.Enabled = false;
            ddlConyuge9.Enabled = false;
            ddlConyugeP10.Enabled = false;
            ddlConyugeP11.Enabled = false;
                       
            for (int i = 0; i < lista.Count; i++)
            {
                variable = lista[i];

                if (variable.numero_pregunta == 1)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlConyugeP1.SelectedValue = Convert.ToString(respuesta);

                }
                if (variable.numero_pregunta == 2)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlConyugeP2.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 3)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlConyugeP3.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 4)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlConyugeP4.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 5)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlConyugeP5.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 6)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlConyugeP6.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 7)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlConyugeP7.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 8)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlConyugeP8.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 9)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlConyuge9.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 10)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlConyugeP10.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 11)
                {
                    long respuesta = variable.numero_respuesta;
                    txtObservacionDeudor.Text = variable.observaciones;
                    ddlConyugeP11.SelectedValue = Convert.ToString(respuesta);
                }
                if (variable.numero_pregunta == 12)
                {
                    long respuesta = variable.numero_respuesta;
                }
            }
        }
    }

    /// <summary>
    /// Método para mostrar listado de personas a referencias bien sea el crédito de microcrédito o consumo
    /// </summary>
    public void LlenarCombosReferencias()
    {
        ddlNombreReferenciaCon.DataSource = ReferenciaServicio.Listarrefereciados(lblCodPersona.Text, (Usuario)Session["usuario"]);
        ddlNombreReferenciaCon.DataTextField = "nombres";
        ddlNombreReferenciaCon.DataValueField = "cod_referencia";
        ddlNombreReferenciaCon.DataBind();
        ddlNombreReferenciaCon.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));

        ddlNombreReferencia.DataSource = ReferenciaServicio.Listarrefereciados(lblCodPersona.Text, (Usuario)Session["usuario"]);        
        ddlNombreReferencia.DataTextField = "nombres";
        ddlNombreReferencia.DataValueField = "cod_referencia";
        ddlNombreReferencia.DataBind();
        ddlNombreReferencia.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));            
    }

    protected void ddlRespuestaMicrocredito1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito1.SelectedValue == "1")
        {
            btnGuardarMicro.Visible = true;
            txtAlerta1.Text = "Alerta";
        }
        else
            txtAlerta1.Text = "";
        btnGuardarMicro.Visible = true;
    }

    protected void ddlRespuestaMicrocredito2_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito2.SelectedValue == "1")
        {
           
            txtAlerta2.Text = "Alerta";
        }
        else
            txtAlerta2.Text = "";
    }

    protected void ddlRespuestaMicrocredito3_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito3.SelectedValue != "1" && ddlRespuestaMicrocredito3.SelectedValue != "0")
        {           
            txtAlerta3.Text = "Alerta";
        }
        else
            txtAlerta3.Text = "";
    
    }

    protected void ddlRespuestaMicrocredito4_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito4.SelectedValue != "1" && ddlRespuestaMicrocredito4.SelectedValue != "0" || ddlRespuestaMicrocredito4.SelectedValue == "1")
        {            
            txtAlerta4.Text = "Alerta";
        }
        if (ddlRespuestaMicrocredito4.SelectedValue == "2" || ddlRespuestaMicrocredito4.SelectedValue == "3" || ddlRespuestaMicrocredito4.SelectedValue == "4")
        {
            txtAlerta4.Text = "";
            btnGuardarMicro.Visible = true;

        }
    }

    protected void ddlRespuestaMicrocredito5_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito5.SelectedValue != "1" && ddlRespuestaMicrocredito5.SelectedValue != "0")
        {
            btnGuardarMicro.Visible = true;
            txtAlerta5.Text = "Alerta";
        }
        else
            txtAlerta5.Text = "";
        btnGuardarMicro.Visible = true;
    }

    protected void ddlRespuestaMicrocredito6_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito6.SelectedValue != "1" && ddlRespuestaMicrocredito6.SelectedValue != "0")
        {
            btnGuardarMicro.Visible = true;
            txtAlerta6.Text = "Alerta";
        }
        else
            txtAlerta6.Text = "";
        btnGuardarMicro.Visible = true;
    }

    protected void ddlRespuestaMicrocredito7_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito7.SelectedValue != "1" && ddlRespuestaMicrocredito7.SelectedValue != "0")
        {
            txtAlerta7.Text = "Alerta";
        }
        else
            txtAlerta7.Text = "";
        btnGuardarMicro.Visible = true;
    }

    protected void ddlRespuestaMicrocredito8_SelectedIndexChanged(object sender, System.EventArgs e)
    {       
        if (ddlRespuestaMicrocredito8.SelectedValue != "1" && ddlRespuestaMicrocredito8.SelectedValue != "0")
        {
            btnGuardarMicro.Visible = true;
            txtAlerta8.Text = "Alerta";
        }
        else
            txtAlerta8.Text = "";
        btnGuardarMicro.Visible = true;
    }

    protected void ddlRespuestaMicrocredito9_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito9.SelectedValue != "1" && ddlRespuestaMicrocredito9.SelectedValue != "0")
        {
            btnGuardarMicro.Visible = true;
            txtAlerta9.Text = "Alerta";
        }
        else
            txtAlerta9.Text = "";
        btnGuardarMicro.Visible = true;
    }

    protected void ddlRespuestaMicrocredito10_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito10.SelectedValue != "1" && ddlRespuestaMicrocredito10.SelectedValue != "0")
        {
            btnGuardarMicro.Visible = true;
            txtAlerta10.Text = "Alerta";
        }
        else
            txtAlerta10.Text = "";
        btnGuardarMicro.Visible = true;
    }

    protected void ddlRespuestaMicrocredito11_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito11.SelectedValue != "1" && ddlRespuestaMicrocredito11.SelectedValue != "0")
        {
            btnGuardarMicro.Visible = true;
            txtAlerta11.Text = "Alerta";
        }
        else
            txtAlerta11.Text = "";

    }

    protected void ddlRespuestaMicrocredito12_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito12.SelectedValue == "1")
        {
            txtAlerta12.Text = "Alerta";
        }
        else
            txtAlerta12.Text = "";

    }

    protected void ddlRespuestaMicrocredito13_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaMicrocredito13.SelectedValue != "1" && ddlRespuestaMicrocredito13.SelectedValue != "0")
        {
            btnGuardarMicro.Visible = true;
            txtAlerta13.Text = "Alerta";
        }
        else
            txtAlerta13.Text = "";

    }

    protected void ddlRespuestaConsumo1_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaConsumo1.SelectedValue == "1")
        {
            txtAlertaConsumo1.Text = "Alerta";
        }
        else
            txtAlertaConsumo1.Text = "";

    }

    protected void ddlRespuestaConsumo2_SelectedIndexChanged(object sender, System.EventArgs e)
    {
         if (ddlRespuestaConsumo2.SelectedValue != "1" && ddlRespuestaConsumo2.SelectedValue != "0")
        {
            txtAlertaConsumo2.Text = "Alerta";
        }
        else
             txtAlertaConsumo2.Text = "";
        
    }

    protected void ddlRespuestaConsumo3_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaConsumo3.SelectedValue != "1" && ddlRespuestaConsumo3.SelectedValue != "0")
        {
            txtAlertaConsumo3.Text = "Alerta";
        }
        else
            txtAlertaConsumo3.Text = "";
    }

    protected void ddlRespuestaConsumo4_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaConsumo4.SelectedValue != "1" && ddlRespuestaConsumo4.SelectedValue != "0")
        {
            txtAlertaConsumo4.Text = "Alerta";
        }
        else
            txtAlertaConsumo4.Text = "";
    }

    protected void ddlRespuestaConsumo5_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaConsumo5.SelectedValue != "1" && ddlRespuestaConsumo5.SelectedValue != "0")
        {
            txtAlertaConsumo5.Text = "Alerta";
        }
        else
            txtAlertaConsumo5.Text = "";
    }

    protected void ddlRespuestaConsumo6_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaConsumo6.SelectedValue != "1" && ddlRespuestaConsumo6.SelectedValue != "0")
        {
            txtAlertaConsumo6.Text = "Alerta";
        }
        else
            txtAlertaConsumo6.Text = "";
    }

    protected void ddlRespuestaConsumo7_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaConsumo7.SelectedValue != "1" && ddlRespuestaConsumo7.SelectedValue != "0")
        {
            txtAlertaConsumo7.Text = "Alerta";
        }
        else
            txtAlertaConsumo7.Text = "";
    }

    protected void ddlRespuestaConsumo8_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaConsumo8.SelectedValue != "1" && ddlRespuestaConsumo8.SelectedValue != "0")
        {
            txtAlertaConsumo8.Text = "Alerta";
        }
        else
            txtAlertaConsumo8.Text = "";
    }

    protected void ddlRespuestaConsumo9_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaConsumo9.SelectedValue == "1")
        {
            txtAlertaConsumo9.Text = "Alerta";
        }
        else
            txtAlertaConsumo9.Text = "";

    }

    protected void ddlRespuestaConsumo10_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (ddlRespuestaConsumo10.SelectedValue == "1")
        {
            txtAlertaConsumo10.Text = "Alerta";
        }
        else
            txtAlertaConsumo10.Text = "";

    }    

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            // Consultar información de referencias en la base de datos
            Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();
            vReferencia = ReferenciaServicio.ConsultarReferenciacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vReferencia.numero_radicacion.ToString()))
                txtRadicacion.Text = HttpUtility.HtmlDecode(vReferencia.numero_radicacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.identificacion.ToString()))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vReferencia.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.nombres))
                txtNombre.Text = HttpUtility.HtmlDecode(vReferencia.nombres.ToString().Trim());

            // Cargar labels con los datos de la persona
            codpersona = vReferencia.cod_Persona;            
            lblNumeroRadicacion.Text = Convert.ToString(vReferencia.numero_radicacion);
            lblCodPersona.Text = Convert.ToString(vReferencia.cod_Persona);
            int clasificacion = vReferencia.cod_Clasificacion;
            lblEsMicrocredito.Text = Convert.ToString(clasificacion);

            // Mostrar los datos ya referenciados de la persona
            llenarcombospersona();
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReferenciaServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    /// <summary>
    /// Método para obtener la información de referencias ya grabadas
    /// </summary>
    /// <param name="pIdObjeto"></param>
    protected void ObtenerDatosReferencia(String pIdObjeto)
    {
        try
        {
            Configuracion conf = new Configuracion();
            Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();
            vReferencia = ReferenciaServicio.ConsultarDatosReferenciacion (Convert.ToInt64(codpersona), (Usuario)Session["usuario"]);

            // Mostrar datos de la persona
            if (!string.IsNullOrEmpty(vReferencia.telefonos))
                txtTelefonopersona.Text = HttpUtility.HtmlDecode(vReferencia.telefonos.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.DIRECCION))
                txtDirecciónPersona.Text = HttpUtility.HtmlDecode(vReferencia.DIRECCION.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.BARRIO))
                txtbarriopersona.Text = HttpUtility.HtmlDecode(vReferencia.BARRIO.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.FECHANACIMIENTO.ToString()))
                txtFecha_nacimientopersona.Text = HttpUtility.HtmlDecode(vReferencia.FECHANACIMIENTO.ToString(conf.ObtenerFormatoFecha()).Trim());
            if (!string.IsNullOrEmpty(vReferencia.ESTADO_CIVIL))
                txtEstadocivilpersona.Text = HttpUtility.HtmlDecode(vReferencia.ESTADO_CIVIL.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.ACTIVIDAD_PERSONA))
                txtActividadpersona.Text = HttpUtility.HtmlDecode(vReferencia.ACTIVIDAD_PERSONA.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.TIPO_VIVIENDA))
                txtTipoviviendapersona.Text = HttpUtility.HtmlDecode(vReferencia.TIPO_VIVIENDA.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.NOMBRE_ARRENDATARIO))
                txtNombreArrendatariopersona.Text = HttpUtility.HtmlDecode(vReferencia.NOMBRE_ARRENDATARIO.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.TELEFONO_ARRENDATARIO.ToString()))
                txtTelefonoarrendatario.Text = HttpUtility.HtmlDecode(vReferencia.TELEFONO_ARRENDATARIO.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.VALOR_ARRIENDO.ToString()))
                txtValorArriendo.Text = HttpUtility.HtmlDecode(vReferencia.VALOR_ARRIENDO.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.PERSONAS_A_CARGO.ToString()))
                txtPersonasacargo.Text = HttpUtility.HtmlDecode(vReferencia.PERSONAS_A_CARGO.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.DIRECCION_NEGOCIO))
                txtUbiacion_negocio.Text = HttpUtility.HtmlDecode(vReferencia.DIRECCION_NEGOCIO.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.ANTIGUEDAD_NEGOCIO))
                txtAntiguedad_negocioperosna.Text = HttpUtility.HtmlDecode(vReferencia.ANTIGUEDAD_NEGOCIO.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.ACTIVIDAD_EMPRESA))
                txtActividad_empresapersona.Text = HttpUtility.HtmlDecode(vReferencia.ACTIVIDAD_EMPRESA.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.NOMBRE_EMPRESA))
                txtNombre_empresapersona.Text = HttpUtility.HtmlDecode(vReferencia.NOMBRE_EMPRESA.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.NIT_EMPRESA))
                txtNit_empresapersona.Text = HttpUtility.HtmlDecode(vReferencia.NIT_EMPRESA.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.CARGO_PERONSA))
                txt_Cargopersona.Text = HttpUtility.HtmlDecode(vReferencia.CARGO_PERONSA.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.SALARIO_PERSONA.ToString()))
                txtSalariopersona.Text = HttpUtility.HtmlDecode(vReferencia.SALARIO_PERSONA.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.ANTIGUEDAD_EMPRESA.ToString()))
                txtAntiguedad_empresapersona.Text = HttpUtility.HtmlDecode(vReferencia.ANTIGUEDAD_EMPRESA.ToString().Trim());
            if (!string.IsNullOrEmpty(vReferencia.DIRECCION_EMPRESA_PERSONA))
                txtDreccion_Empresapersona.Text = HttpUtility.HtmlDecode(vReferencia.DIRECCION_EMPRESA_PERSONA.ToString().Trim());

            // Mostrar información del conyuge si lo tiene
            if (vReferencia.identificacion_conyge == 0)
            {
                lblExisteConyuge.Text = "0";
                txtNombreconyuge.Visible = false;
                txtIdentificacionconyuge.Visible=false;
                txtTelefonoconyuge.Visible=false;
                txtDirecciónconyuge.Visible=false;
                txtActividadconyuge.Visible=false;
                txtActividad_emprsaconyuge.Visible=false;
                txtNombre_empresaconyuge.Visible=false;
                txtNit_empresaconyuge.Visible=false;
                txtCargoconyuge.Visible=false;
                txtSalarioconyuge.Visible=false;
                txtAntiguedad_empresaconyuge.Visible=false;
                txtDireccion_empresaconyuge.Visible = false;
                Labeldaosconyuge.Visible = false;
                lblTitNombreConyuge.Visible=false;
                lblTitCedulaConyuge.Visible=false;
                lblTitTelefonoConyuge.Visible=false;
                lblTitDireccionConyuge.Visible=false;
                lblTitActividadConyuge.Visible=false;
                lblTitActEmpresaConyuge.Visible=false;
                lblTitEmpresaConyuge.Visible=false;
                lblTitNitEmpresaConyuge.Visible=false;
                lblTitCargoConyuge.Visible=false;
                lblTitSalarioConyuge.Visible=false;
                lblTitAntiguedadConyuge.Visible=false;
                lblTitDirEmpresaConyuge.Visible = false;
                lblTitConyuge.Visible = false;
                ddlConyugeP1.Visible = false;
                ddlConyugeP2.Visible = false;
                ddlConyugeP3.Visible = false;
                ddlConyugeP4.Visible = false;
                ddlConyugeP5.Visible = false;
                ddlConyugeP6.Visible = false;
                ddlConyugeP7.Visible = false;
                ddlConyugeP8.Visible = false;
                ddlConyuge9.Visible = false;
                ddlConyugeP10.Visible = false;
                ddlConyugeP11.Visible = false;                   
            }
            else
            {
                lblExisteConyuge.Text = "1";

                if (!string.IsNullOrEmpty(vReferencia.nombres_conyuge))
                    txtNombreconyuge.Text = HttpUtility.HtmlDecode(vReferencia.nombres_conyuge.ToString().Trim());
                if (!string.IsNullOrEmpty(vReferencia.identificacion_conyge.ToString()))
                    txtIdentificacionconyuge.Text = HttpUtility.HtmlDecode(vReferencia.identificacion_conyge.ToString().Trim());
                try
                {
                    if (!string.IsNullOrEmpty(vReferencia.telefono_CONYUGE))
                        txtTelefonoconyuge.Text = HttpUtility.HtmlDecode(vReferencia.telefono_CONYUGE.ToString().Trim());
                    if (!string.IsNullOrEmpty(vReferencia.DIRECCION_CONYUGE))
                        txtDirecciónconyuge.Text = HttpUtility.HtmlDecode(vReferencia.DIRECCION_CONYUGE.ToString().Trim());
                    if (!string.IsNullOrEmpty(vReferencia.ACTIVIDAD_CONYUGE))
                        txtActividadconyuge.Text = HttpUtility.HtmlDecode(vReferencia.ACTIVIDAD_CONYUGE.ToString().Trim());
                    if (!string.IsNullOrEmpty(vReferencia.ACTIVIDAD_EMPRESA_CONYUGE))
                        txtActividad_emprsaconyuge.Text = HttpUtility.HtmlDecode(vReferencia.ACTIVIDAD_EMPRESA_CONYUGE.ToString().Trim());
                    if (!string.IsNullOrEmpty(vReferencia.NOMBRE_EMPRESA_CONYUGE))
                        txtNombre_empresaconyuge.Text = HttpUtility.HtmlDecode(vReferencia.NOMBRE_EMPRESA_CONYUGE.ToString().Trim());
                    if (!string.IsNullOrEmpty(vReferencia.NIT_EMPRESA_CONYUGE))
                        txtNit_empresaconyuge.Text = HttpUtility.HtmlDecode(vReferencia.NIT_EMPRESA_CONYUGE.ToString().Trim());
                    if (!string.IsNullOrEmpty(vReferencia.CARGO_CONYUGE))
                        txtCargoconyuge.Text = HttpUtility.HtmlDecode(vReferencia.CARGO_CONYUGE.ToString().Trim());
                    if (!string.IsNullOrEmpty(vReferencia.SALARIO_CONYUGE.ToString()))
                        txtSalarioconyuge.Text = HttpUtility.HtmlDecode(vReferencia.SALARIO_CONYUGE.ToString().Trim());
                    if (!string.IsNullOrEmpty(vReferencia.ANTIGUEDAD_EMPRESA_CONYUGE.ToString()))
                        txtAntiguedad_empresaconyuge.Text = HttpUtility.HtmlDecode(vReferencia.ANTIGUEDAD_EMPRESA_CONYUGE.ToString().Trim());
                    if (!string.IsNullOrEmpty(vReferencia.DIRECCION_EMPRESA_CONYUGE))
                        txtDireccion_empresaconyuge.Text = HttpUtility.HtmlDecode(vReferencia.DIRECCION_EMPRESA_CONYUGE.ToString().Trim());
                    llenarcombosconyuge();
                }
                catch { }
            }
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(ReferenciaServicio.CodigoPrograma, "ObtenerDatosReferencia", ex);
        }
    }


    protected void ddlNombreReferenciaCon_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();
        vReferencia = ReferenciaServicio.ConsultarReferenciacionPersonas(Convert.ToString(ddlNombreReferenciaCon.SelectedValue), (Usuario)Session["usuario"]);
        txtTelefonoReferenciaCon.Text =Convert.ToString(vReferencia.telefono_referenciado);
        txtDireccionReferenciaCon.Text = vReferencia.direccion_referenciado;
        txtTipoReferenciaCon.Text = vReferencia.tipo_referencia;
    }

    protected void ddlNombreReferencia_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();
        vReferencia = ReferenciaServicio.ConsultarReferenciacionPersonas(Convert.ToString(ddlNombreReferencia.SelectedValue), (Usuario)Session["usuario"]);
        txtTelefonoReferencia.Text = Convert.ToString(vReferencia.telefono_referenciado);
        txtDireccionReferencia.Text = vReferencia.direccion_referenciado;
        txtTipoReferencia.Text = vReferencia.tipo_referencia;
        txtTelOficinaReferencia.Text = Convert.ToString(vReferencia.Celular);
        txtCelularReferencia.Text = Convert.ToString(vReferencia.TELOFICINA);        
    }

    /// <summary>
    /// Método para guardar la información de la referencias tanto para microcrédito como para consumo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardarReferencia_Click(object sender, System.EventArgs e)
    {
        Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();
        Usuario usuap = (Usuario)Session["usuario"];

        if (PanelConsumo.Visible == true)
        {
            if (ddlRespuestaConsumo1.SelectedValue != "1")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferenciaCon.SelectedValue), 2, 1, 5, usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionConsumo.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaConsumo2.SelectedValue != "1" && ddlRespuestaConsumo2.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferenciaCon.SelectedValue), 2, 2, Convert.ToInt16(ddlRespuestaConsumo2.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionConsumo.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaConsumo3.SelectedValue != "1" && ddlRespuestaConsumo3.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferenciaCon.SelectedValue), 2, 3, Convert.ToInt16(ddlRespuestaConsumo3.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionConsumo.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaConsumo4.SelectedValue != "1" && ddlRespuestaConsumo4.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferenciaCon.SelectedValue), 2, 4, Convert.ToInt16(ddlRespuestaConsumo4.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionConsumo.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaConsumo5.SelectedValue != "1" && ddlRespuestaConsumo5.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferenciaCon.SelectedValue), 2, 5, Convert.ToInt16(ddlRespuestaConsumo5.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionConsumo.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaConsumo6.SelectedValue != "1" && ddlRespuestaConsumo6.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferenciaCon.SelectedValue), 2, 6, Convert.ToInt16(ddlRespuestaConsumo6.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionConsumo.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaConsumo7.SelectedValue != "1" && ddlRespuestaConsumo7.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferenciaCon.SelectedValue), 2, 7, Convert.ToInt16(ddlRespuestaConsumo7.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionConsumo.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaConsumo8.SelectedValue != "1" && ddlRespuestaConsumo8.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferenciaCon.SelectedValue), 2, 8, Convert.ToInt16(ddlRespuestaConsumo8.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionConsumo.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaConsumo9.SelectedValue == "1")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferenciaCon.SelectedValue), 2, 9, 5, usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionConsumo.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaConsumo10.SelectedValue == "1")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferenciaCon.SelectedValue), 2, 10, 5, usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionConsumo.Text, (Usuario)Session["Usuario"]);
            }

            ddlRespuestaConsumo1.SelectedValue = "0";
            ddlRespuestaConsumo2.SelectedValue = "0";
            ddlRespuestaConsumo3.SelectedValue = "0";
            ddlRespuestaConsumo4.SelectedValue = "0";
            ddlRespuestaConsumo5.SelectedValue = "0";
            ddlRespuestaConsumo6.SelectedValue = "0";
            ddlRespuestaConsumo7.SelectedValue = "0";
            ddlRespuestaConsumo8.SelectedValue = "0";
            ddlRespuestaConsumo9.SelectedValue = "0";
            ddlRespuestaConsumo10.SelectedValue = "0";
            txtObservacionConsumo.Text = null;
            consultarControl(); 
        }
        
        else
        {
            // Enviar información por correo al asesor si la dirección no corresponde
            if (ddlRespuestaMicrocredito12.SelectedValue == "1")
            {
                Xpinn.FabricaCreditos.Entities.Referencia vcorreo = new Xpinn.FabricaCreditos.Entities.Referencia();
                vcorreo = ReferenciaServicio.ConsultarCorreo(Convert.ToInt64(txtRadicacion.Text), (Usuario)Session["usuario"]);                
                if (vcorreo.correo != "PENDIENTE")
                {
                    if (vcorreo.correo != null)
                    {
                        page = new Page();
                        page.EnableEventValidation = false;
                        form = new HtmlForm();
                        stringWriter = new StringWriter();
                        htmlTxtWriter = new HtmlTextWriter(stringWriter);
                        form.Attributes.Add("runat", "server");

                        page.DesignerInitialize();
                        page.RenderControl(htmlTxtWriter);
                        string strHtml = stringWriter.ToString();
                        HttpContext.Current.Response.Clear();

                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.live.com");

                        mail.From = new MailAddress("xpinn@outlook.com", "REFERENCIA NO LOCALIZADA", Encoding.UTF8);

                        mail.Subject = "REFERENCIA NO LOCALIZADA ";

                        mail.To.Add(vcorreo.correo);

                        mail.IsBodyHtml = true;
                        mail.Body = " Fue ilocalizada la referencia del Sr(a). " + txtNombre.Text + " con Numero de radicacion " + txtRadicacion.Text + " DATOS DE LA REFERANCIA:   Nombre de referancia " + ddlNombreReferencia.Text + " Observaciones: " + txtObservacionMicrocredito.Text;

                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("xpinn@outlook.com", "Software Financial Web");
                        SmtpServer.EnableSsl = true;

                        SmtpServer.Send(mail);
                    }
                }
            }
            
            if (ddlRespuestaMicrocredito1.SelectedValue == "1")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 1, 5, usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito2.SelectedValue == "1")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 2, 5, usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito3.SelectedValue != "1" && ddlRespuestaMicrocredito3.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 3, Convert.ToInt16(ddlRespuestaMicrocredito3.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito4.SelectedValue != "1" && ddlRespuestaMicrocredito4.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 4, Convert.ToInt16(ddlRespuestaMicrocredito4.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
                ReferenciaServicio.guardarpregunta(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 4, Convert.ToInt16(ddlRespuestaMicrocredito4.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito5.SelectedValue != "1" && ddlRespuestaMicrocredito5.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 5, Convert.ToInt16(ddlRespuestaMicrocredito5.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito6.SelectedValue != "1" && ddlRespuestaMicrocredito6.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 6, Convert.ToInt16(ddlRespuestaMicrocredito6.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito7.SelectedValue != "1" && ddlRespuestaMicrocredito7.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 7, Convert.ToInt16(ddlRespuestaConsumo1.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito8.SelectedValue != "1" && ddlRespuestaMicrocredito8.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 8, Convert.ToInt16(ddlRespuestaMicrocredito8.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito9.SelectedValue != "1" && ddlRespuestaMicrocredito9.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 9, Convert.ToInt16(ddlRespuestaMicrocredito9.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito10.SelectedValue != "1" && ddlRespuestaMicrocredito10.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 10, Convert.ToInt16(ddlRespuestaMicrocredito10.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito11.SelectedValue != "1" && ddlRespuestaMicrocredito11.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 11, Convert.ToInt16(ddlRespuestaMicrocredito11.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito12.SelectedValue != "1")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 12, 5, usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            if (ddlRespuestaMicrocredito13.SelectedValue != "1" && ddlRespuestaMicrocredito13.SelectedValue != "0")
            {
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 13, 5, usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionMicrocredito.Text, (Usuario)Session["Usuario"]);
            }
            ddlRespuestaMicrocredito1.SelectedValue = "0";            
            ddlRespuestaMicrocredito2.SelectedValue = "0";
            ddlRespuestaMicrocredito3.SelectedValue = "0";
            ddlRespuestaMicrocredito4.SelectedValue = "0";
            ddlRespuestaMicrocredito5.SelectedValue = "0";
            ddlRespuestaMicrocredito6.SelectedValue = "0";
            ddlRespuestaMicrocredito7.SelectedValue = "0";
            ddlRespuestaMicrocredito8.SelectedValue = "0";
            ddlRespuestaMicrocredito9.SelectedValue = "0";
            ddlRespuestaMicrocredito10.SelectedValue = "0";
            ddlRespuestaMicrocredito11.SelectedValue = "0";
            ddlRespuestaMicrocredito12.SelectedValue = "0";
            ddlRespuestaMicrocredito13.SelectedValue = "0";
            txtObservacionMicrocredito.Text = null;
          
        }
       
        // Actualizar combo con las referencias faltantes por confirmar
        LlenarCombosReferencias();
        consultarControl(); 

      
    }

    protected void btnGuardarParcial_Click(object sender, System.EventArgs e)
    {
    }

    /// <summary>
    /// Método para guardar la verificación de datos del deudor y del conyúge.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardarSolicitante_Click(object sender, System.EventArgs e)
    {
        Usuario usuap = (Usuario)Session["usuario"];
        Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();
        VerError("");
        int error = 1;
        LlenarCombosReferencias();

        // Verificando que todos los campos esten diligenciados
        if (Convert.ToInt64(ddlDeudorP1.SelectedValue) == 0)
            error = 0;
        if (Convert.ToInt64(ddlDeudorP2.SelectedValue) == 0)
            error = 0;
        if (Convert.ToInt64(ddlDeudorP3.SelectedValue) == 0)
            error = 0;
        if (Convert.ToInt64(ddlDeudorP4.SelectedValue) == 0)
            error = 0;            
        if (Convert.ToInt64(ddlDeudorP5.SelectedValue) == 0)
            error = 0;                
        if (Convert.ToInt64(ddlDeudorP6.SelectedValue) == 0)
            error = 0;
        if (Convert.ToInt64(ddlDeudorP7.SelectedValue) == 0)
            error = 0;
        if (Convert.ToInt64(ddlDeudorP8.SelectedValue) == 0)
            error = 0;
        if (Convert.ToInt64(ddlDeudorP9.SelectedValue) == 0)
            error = 0;
        if (Convert.ToInt64(ddlDeudorP10.SelectedValue) == 0)
            error = 0;
        if (Convert.ToInt64(ddlDeudorP11.SelectedValue) == 0)
            error = 0;
        if (Convert.ToInt64(ddlDeudorP1.SelectedValue) == 0)
            error = 0;
        // En el caso de que exista el conyúge verificar que se hayan diligenciado los datos
        if (lblExisteConyuge.Text == "1")
        {
            if (Convert.ToInt64(ddlConyugeP2.SelectedValue) == 0)
                error = 0;
            if (Convert.ToInt64(ddlConyugeP3.SelectedValue) == 0)
                error = 0;
            if (Convert.ToInt64(ddlConyugeP4.SelectedValue) == 0)
                error = 0;
            if (Convert.ToInt64(ddlConyugeP5.SelectedValue) == 0)
                error = 0;
            if (Convert.ToInt64(ddlConyugeP6.SelectedValue) == 0)
                error = 0;
            if (Convert.ToInt64(ddlConyugeP7.SelectedValue) == 0)
                error = 0;
            if (Convert.ToInt64(ddlConyugeP8.SelectedValue) == 0)
                error = 0;
            if (Convert.ToInt64(ddlConyuge9.SelectedValue) == 0)
                error = 0;
            if (Convert.ToInt64(ddlConyugeP10.SelectedValue) == 0)
                error = 0;
            if (Convert.ToInt64(ddlConyugeP11.SelectedValue) == 0)
                error = 0;
        }
        if (error != 0)
        {
           
            error = 1;
            if (lblEsMicrocredito.Text == "3")
            {
                PanelConsumo.Visible = false;
                PanelMicrocredito.Visible = true;
                btnGuardarSolicitante.Enabled = false;                                                                                                        
            }
            else
            {
                PanelMicrocredito.Visible = false;
                PanelConsumo.Visible = true;
                btnGuardarSolicitante.Enabled = false;                                                                                                        
            }
        }
        else
        {
            VerError("Falta un campo por digilenciar");
        }
        
        if (lblEsMicrocredito.Text == "3")
        {
            string b = ddlNombreReferencia.Items.Count.ToString();
            if (b == "1")
            {
                PanelMicrocredito.Visible = false;
                VerError("El Cliente no tiene Referencias");
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 12, 5, usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
                consultarControl();

            }
        }
        else
        {
            string a = ddlNombreReferenciaCon.Items.Count.ToString();
            if (a == "1")
            {
                PanelConsumo.Visible = false;
                VerError("El Cliente no tiene Referencias");
                ReferenciaServicio.guardar(Convert.ToInt64(ddlNombreReferencia.SelectedValue), 2, 12, 5, usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
                consultarControl();
             
            }
        }
        
    }
        
    /// <summary>
    /// Método para guardar la información de la referencias
    /// </summary>
    protected void guardardatos()
    {
        Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();
        Usuario usuap = (Usuario)Session["usuario"];
        if (lblNoExisteReferencias.Text == "si")
        {
            Int64 codReferencia = 0;
            if (PanelConsumo.Visible == true)
                codReferencia = Convert.ToInt64(ddlNombreReferenciaCon.SelectedValue);
            else
                codReferencia = Convert.ToInt64(ddlNombreReferencia.SelectedValue);
            ReferenciaServicio.guardar(codReferencia, 0, 1, Convert.ToInt16(ddlDeudorP1.SelectedValue), Convert.ToInt64(usuap.codusuario), txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 0, 2, Convert.ToInt16(ddlDeudorP2.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 0, 3, Convert.ToInt16(ddlDeudorP3.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 0, 4, Convert.ToInt16(ddlDeudorP4.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 0, 5, Convert.ToInt16(ddlDeudorP5.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 0, 6, Convert.ToInt16(ddlDeudorP6.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 0, 7, Convert.ToInt16(ddlDeudorP7.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 0, 8, Convert.ToInt16(ddlDeudorP8.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 0, 9, Convert.ToInt16(ddlDeudorP9.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 0, 10, Convert.ToInt16(ddlDeudorP10.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 0, 11, Convert.ToInt16(ddlDeudorP11.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 1, 1, Convert.ToInt16(ddlConyugeP1.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 1, 2, Convert.ToInt16(ddlConyugeP2.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 1, 3, Convert.ToInt16(ddlConyugeP3.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 1, 4, Convert.ToInt16(ddlConyugeP4.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 1, 5, Convert.ToInt16(ddlConyugeP5.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 1, 6, Convert.ToInt16(ddlConyugeP6.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 1, 7, Convert.ToInt16(ddlConyugeP7.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 1, 8, Convert.ToInt16(ddlConyugeP8.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 1, 9, Convert.ToInt16(ddlConyuge9.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 1, 10, Convert.ToInt16(ddlConyugeP10.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            ReferenciaServicio.guardar(codReferencia, 1, 11, Convert.ToInt16(ddlConyugeP11.SelectedValue), usuap.codusuario, txtRadicacion.Text, Convert.ToInt64(lblCodPersona.Text), txtObservacionDeudor.Text, (Usuario)Session["Usuario"]);
            consultarControl();
        }
        else
        {
            ReferenciaServicio.modificar(1, Convert.ToInt16(ddlDeudorP1.SelectedValue), txtObservacionDeudor.Text, txtRadicacion.Text, 0, (Usuario)Session["Usuario"]);
        }
        // Limpia los campos de las referencias
        txtTelefonoReferenciaCon.Text = "";
        txtDireccionReferenciaCon.Text = "";
        txtTipoReferenciaCon.Text = "";
        txtTelefonoReferencia.Text = "";
        txtDireccionReferencia.Text = "";
        txtTipoReferencia.Text = "";
        // Deshabilita la modificación o confirmación de referencias del deudor y conyuge.
        txtObservacionDeudor.Enabled = false;
        ddlDeudorP1.Enabled = false;
        ddlDeudorP2.Enabled = false;
        ddlDeudorP3.Enabled = false;
        ddlDeudorP4.Enabled = false;
        ddlDeudorP5.Enabled = false;
        ddlDeudorP6.Enabled = false;
        ddlDeudorP7.Enabled = false;
        ddlDeudorP8.Enabled = false;
        ddlDeudorP9.Enabled = false;
        ddlDeudorP10.Enabled = false;
        ddlDeudorP11.Enabled = false;
        ddlConyugeP1.Enabled = false;
        ddlConyugeP2.Enabled = false;
        ddlConyugeP3.Enabled = false;
        ddlConyugeP4.Enabled = false;
        ddlConyugeP5.Enabled = false;
        ddlConyugeP6.Enabled = false;
        ddlConyugeP7.Enabled = false;
        ddlConyugeP8.Enabled = false;
        ddlConyuge9.Enabled = false;
        ddlConyugeP10.Enabled = false;
        ddlConyugeP11.Enabled = false;

    }

    /// <summary>
    /// Método para generar archivo en excel con datos de referencias.
    /// </summary>
    /// <param name="dt"></param>
    private void GenerateExcelFile(DataTable dt)
    {
        string attachment = "attachment; filename=Referencias.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1254");
        Response.Charset = "windows-1254";

        string tab = "";
        foreach (DataColumn dc in dt.Columns)
        {
            Response.Write(tab + dc.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        int i;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write(tab + dr[i].ToString());
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }

    private void CargarListas()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
            lstDatosSolicitud.Clear();
            ListaSolicitada = "EstadoProceso";
            lstDatosSolicitud = ControlProcesosServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);            
            ddlProceso.DataSource = lstDatosSolicitud;
            ddlProceso.DataTextField = "ListaDescripcion";
            ddlProceso.DataValueField = "ListaId";
            ddlProceso.DataBind();
            ddlProceso.SelectedIndex = 2;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReferenciaServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private void GuardarControl()
    {
        //String FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());
            
        Usuario pUsuario = (Usuario)Session["usuario"];
      
        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
        vControlCreditos.numero_radicacion = Convert.ToInt64(this.txtRadicacion.Text);
        vControlCreditos.codtipoproceso = ddlProceso.SelectedItem != null ? ddlProceso.SelectedValue : null;
        vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
        vControlCreditos.cod_persona = pUsuario.codusuario;
        vControlCreditos.cod_motivo = 0;
        vControlCreditos.observaciones = "PROCESO DE REFERENCIACION";
        vControlCreditos.anexos = null;
        vControlCreditos.nivel = 0;
        if (vControlCreditos.fechaconsulta_dat != null)
        {               
            if ((Session["Datacredito"] != null))
            {
                String FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());    
                if (FechaDatcaredito.Trim() != "")
                    vControlCreditos.fechaconsulta_dat = Convert.ToDateTime(FechaDatcaredito);
            }            
            if ((Session["Datacredito"] == null))
            {
                vControlCreditos.fechaconsulta_dat = FechaDatcaredito == "" ? DateTime.MinValue : Convert.ToDateTime(FechaDatcaredito.Trim());
            }            
        }
        vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, (Usuario)Session["usuario"]);           
    }
   
    private void consultarControl()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];
        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();       
        String radicado  = Convert.ToString(this.txtRadicacion.Text);
        vControlCreditos = ControlCreditosServicio.ConsultarControl_Procesos(3,radicado,(Usuario)Session["usuario"]);              
        Int64 Controlexiste = 0;
        Controlexiste = Convert.ToInt64(vControlCreditos.codtipoproceso);
        if (Controlexiste <= 0)
        {
            GuardarControl();
        }       
    }

    
    
}