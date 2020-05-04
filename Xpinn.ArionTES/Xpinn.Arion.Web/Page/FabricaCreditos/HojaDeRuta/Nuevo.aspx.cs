using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using System.Design;
using System.Threading;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;

partial class Lista : GlobalWeb
{
    private ControlTiemposService ControlTiemposServicio = new ControlTiemposService();
    private String HojaRuta = "";
    private String Proceso = "";
    private String SiguienteProceso = "";
    private String Estado = "";
    private String FechaDatcaredito = "";    
    string vAsesor = null;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(ControlTiemposServicio.CodigoProgramaHojaRuta, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar+= btnRegresar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaHojaRuta, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        txtFechaActual.Text = DateTime.Now.ToString();
     
        try
        {
            if (!IsPostBack)
            {
                MultiViewRuta.ActiveViewIndex = 0;
                // Cargar motivos de negación
                DropNegar();
                // Guardar criterios de búsqueda
                CargarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaHojaRuta);
                // Cargar los datos del registro seleccionado
                if (Session[ControlTiemposServicio.CodigoProgramaHojaRuta + ".consulta"] != null)
                {
                    // Activar la vista de hoja de ruta
                    mvControlTiempos.ActiveViewIndex = 0;
                    // Mostrar los datos del cliente
                    ObtenerCliente(Session["Numeroidentificacion"].ToString());
                    // Mostrar los datos del crédito
                    ObtenerCredito(Convert.ToInt64(Session["NumeroCredito"].ToString()));
                    // Determinar en que proceso esta según parámetros de hoja de ruta
                    Proceso = Convert.ToString(Session["Proceso"].ToString());
                    // Determinar el siguiente proceso según parámetros de hoja de ruta
                    if (Session["SiguienteProceso"] != null)
                        SiguienteProceso = Convert.ToString(Session["SiguienteProceso"].ToString());
                    // Cargar fecha de consulta a centrales de riesgo
                    FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());
                    // Cargar el estado actual del crédito
                    Estado = Convert.ToString(Session["Estado"].ToString());
                    // Cargar datos del crédito
                    txtCredito5.Text = (Convert.ToString(Session["NumeroCredito"].ToString()));                    
                    txtFechaConsultaData.Enabled = true;
                    txtObservaciones.Enabled = true;                   
                    txtEstadoActual.Text = Proceso;
                    txtFechaConsultaData.Text = FechaDatcaredito;
                    // Mostrar historial de seguimiento o control de hoja de ruta
                    ActualizarHistoricoSeguimiento();
                    ConsultarHistoricoSeguimientos();
                    // Llenar combo de los estados siguientes
                    LlenarComboEstado();
                }                    
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaHojaRuta, "Page_Load", ex);
        }
    }


    protected void LlenarComboEstado()
    {
        Xpinn.FabricaCreditos.Services.ProcesosService vProcesoServicio = new Xpinn.FabricaCreditos.Services.ProcesosService();
        Xpinn.FabricaCreditos.Entities.Procesos vProceso = new Xpinn.FabricaCreditos.Entities.Procesos();
        CreditoSolicitadoService creditoServicio = new CreditoSolicitadoService();
        CreditoSolicitado proceso = new CreditoSolicitado();
        String filtro = txtEstadoActual.Text;     

        if (SiguienteProceso == "Fin tiempos de Respuesta" || SiguienteProceso == "Fin de Tiempos" || SiguienteProceso =="Fin de  Tiempos")
        {
            btnGrabarSegumiento.Visible = false;
            proceso.estado = SiguienteProceso;

            //Agregado para consultar el proceso según descripción ya que la parametrización en cada entidad varia
            proceso = creditoServicio.ConsultarCodigodelProceso(proceso, (Usuario)Session["Usuario"]);
            ddlAccion.Items.Insert(0, new ListItem("Fin tiempos de Respuesta", Convert.ToString(proceso.Codigoproceso)));
            
            //ddlAccion.Items.Insert(0, new ListItem("Fin tiempos de Respuesta", "6"));
            btnGrabarSegumiento.Visible = false;
            ddlAccion.Enabled = false;
            txtObservaciones.Enabled = false;

        }
        else
        {
            ddlAccion.DataSource = vProcesoServicio.ListarProcesosSiguientes(vProceso, filtro, (Usuario)Session["usuario"]);
            ddlAccion.DataTextField = "descripcion";
            ddlAccion.DataValueField = "cod_proceso_antec";
            ddlAccion.DataBind();
        }

        Proceso = Convert.ToString(ddlAccion.SelectedValue);
        if (Proceso != "")
            vProceso = vProcesoServicio.ConsultarProcesos(Convert.ToInt64(Proceso), (Usuario)Session["usuario"]);

        if (vProceso.tipo_proceso == 1)
        {
            btnGrabarSegumiento.Visible = false;
            ddlAccion.Enabled = false;
            txtObservaciones.Enabled = false;
        }

    }

    protected void btnNuevo_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaHojaRuta);
        Navegar(Pagina.Nuevo);
    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        LimpiarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaHojaRuta);
    }

   protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
   {
       Navegar(Pagina.Lista);
   }

   /// <summary>
   /// Método para mostrar los motivos de negación del crédito
   /// </summary>
   private void DropNegar()
   {
       MotivoService motivoServicio = new MotivoService();
       Motivo motivo = new Motivo();
       ddlNegar.DataSource = motivoServicio.ListarMotivosFiltro(motivo, (Usuario)Session["usuario"], 2);
       ddlNegar.DataTextField = "Descripcion";
       ddlNegar.DataValueField = "Codigo";
       ddlNegar.DataBind();
       ddlNegar.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
   }

    protected void btnConsultar_Click1(object sender, ImageClickEventArgs e)
    {
        ConsultarHistoricoSeguimientos();
    }

    private void ConsultarHistoricoSeguimientos()
    {
        try
        {
            Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
            List<Xpinn.FabricaCreditos.Entities.ControlCreditos> lstConsultaHS = new List<Xpinn.FabricaCreditos.Entities.ControlCreditos>();
            lstConsultaHS = ControlCreditosServicio.ListarControlCreditos(ObtenerValoresHistorico(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsultaHS;

            if (lstConsultaHS.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegsH.Visible = true;
                lblTotalRegsH.Text = "<br/> Registros encontrados " + lstConsultaHS.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegsH.Visible = false;
            }

            Session.Add(ControlTiemposServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoPrograma, "Actualizar", ex);
        }
    }


    private Xpinn.FabricaCreditos.Entities.ControlCreditos ObtenerValoresHistorico()
    {
        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
        vControlCreditos.numero_radicacion = Convert.ToInt64(Session["NumeroCredito"]); 
        return vControlCreditos;
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            ConfirmarEliminarFila(e, "btnBorrar");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    private void Edicion()
    {
        //try
        //{
        //    //if (Session[ControlCreditosServicio.CodigoProgramaHojaRuta + ".id"] != null)
        //    //    VisualizarOpciones(ControlCreditosServicio.CodigoProgramaHojaRuta, "E");
        //    //else
        //    //    VisualizarOpciones(ControlCreditosServicio.CodigoProgramaHojaRuta, "A");

        //    //Page_Load
        //    if (Session[ControlCreditosServicio.CodigoProgramaHojaRuta + ".id"] != null)
        //    {
        //        idObjeto = Session[ControlCreditosServicio.CodigoProgramaHojaRuta + ".id"].ToString();
        //        if (idObjeto != null)
        //        {
        //            ObtenerDatos(idObjeto);
        //            Session.Remove(ControlCreditosServicio.CodigoProgramaHojaRuta + ".id");
        //        }
        //    }            
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(ControlCreditosServicio.GetType().Name + "A", "Page_PreInit", ex);
        //}
    }


    protected void ObtenerCliente(String vCedula)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            vPersona1.identificacion = vCedula;
            vPersona1.seleccionar = "Identificacion";
            vPersona1.noTraerHuella = 1;
            vPersona1.identificacion = Session["Numeroidentificacion"].ToString();

            vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            string Nombres = null;

            if (vPersona1.cod_persona != Int64.MinValue)
                txtCodigo.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.identificacion))
                txtIdenti.Text = HttpUtility.HtmlDecode(vPersona1.identificacion.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.primer_nombre))
                Nombres = HttpUtility.HtmlDecode(vPersona1.primer_nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.segundo_nombre))
                Nombres = Nombres + " " + HttpUtility.HtmlDecode(vPersona1.segundo_nombre.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.primer_apellido))
                Nombres = Nombres + " " + HttpUtility.HtmlDecode(vPersona1.primer_apellido.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.segundo_apellido))
                Nombres = Nombres + " " + HttpUtility.HtmlDecode(vPersona1.segundo_apellido.ToString().Trim());
            txtNombres.Text = Nombres;
            if (!string.IsNullOrEmpty(vPersona1.direccion))
                txtDireccion.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(vPersona1.telefono.ToString().Trim());
            if (!string.IsNullOrEmpty(vPersona1.empresa))
                txtNombreNegocio.Text = HttpUtility.HtmlDecode(vPersona1.empresa.ToString().Trim());

            List<Xpinn.FabricaCreditos.Entities.ControlTiempos> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.ControlTiempos>();  //Lista de los menus desplegables
            lstDatosSolicitud.Clear();
            lstDatosSolicitud = ControlTiemposServicio.ListasDesplegables("Actividad", (Usuario)Session["Usuario"]);
                    
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoPrograma, "ObtenerCliente", ex);
        }
    }

    protected void ObtenerCredito(Int64 vNumeroCredito)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.DatosSolicitud datosSolicitud = new Xpinn.FabricaCreditos.Entities.DatosSolicitud();
            Xpinn.FabricaCreditos.Services.DatosSolicitudService DatosSolicitudServicio = new Xpinn.FabricaCreditos.Services.DatosSolicitudService();

            datosSolicitud.numerosolicitud = vNumeroCredito;
            datosSolicitud = DatosSolicitudServicio.ListarSolicitudCrtlTiempos(datosSolicitud, (Usuario)Session["usuario"]);

            if (datosSolicitud.montosolicitado != Int64.MinValue)
                txtMonto.Text = HttpUtility.HtmlDecode(datosSolicitud.montosolicitado.ToString().Trim());
            if (!string.IsNullOrEmpty(datosSolicitud.nombre))
                txtLinea.Text = HttpUtility.HtmlDecode(datosSolicitud.nombre.ToString().Trim());
            if (datosSolicitud.numerocuotas != Int64.MinValue)
                txtFechaSolicitud.Text = HttpUtility.HtmlDecode(datosSolicitud.fechasolicitud.ToString().Trim());
            if (datosSolicitud.cod_asesor_com != Int64.MinValue)
                txtAsesor.Text = HttpUtility.HtmlDecode(datosSolicitud.cod_asesor_com.ToString().Trim());
            if (datosSolicitud.asesor != "")
                txtNombreAsesor.Text = HttpUtility.HtmlDecode(datosSolicitud.asesor.ToString().Trim());
            if (datosSolicitud.valorcuota != Int64.MinValue)
                txtCuota.Text = HttpUtility.HtmlDecode(datosSolicitud.valorcuota.ToString().Trim());
            if (datosSolicitud.plazosolicitado != Int64.MinValue)
                txtPlazo.Text = HttpUtility.HtmlDecode(datosSolicitud.plazosolicitado.ToString().Trim());
            txtNumeroRadicacion.Text = vNumeroCredito.ToString();
           
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoPrograma, "ObtenerCredito", ex);
        }
    }
   

    private void ActualizarHistoricoSeguimiento()
    {
        try
        {
            Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
            List<Xpinn.FabricaCreditos.Entities.ControlCreditos> lstConsultaH = new List<Xpinn.FabricaCreditos.Entities.ControlCreditos>();
            lstConsultaH = ControlCreditosServicio.ListarControlCreditos(ObtenerValoresHistorico(), (Usuario)Session["usuario"]);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsultaH;

            if (lstConsultaH.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegsH.Visible = true;
                lblTotalRegsH.Text = "<br/> Registros encontrados " + lstConsultaH.Count.ToString();
                gvLista.DataBind();
            }
            else
            {
                gvLista.Visible = false;
                lblTotalRegsH.Visible = false;
            }

            Session.Add(ControlTiemposServicio.CodigoProgramaHojaRuta + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaHojaRuta, "Actualizar", ex);
        }
    }


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            ActualizarHistoricoSeguimiento();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaHojaRuta, "gvListaHistorico_PageIndexChanging", ex);
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        GuardarValoresConsulta(pConsulta, ControlTiemposServicio.CodigoProgramaHojaRuta);
      
        ActualizarHistoricoSeguimiento();
    }

    private void Borrar()
    {
        txtObservaciones.Text = "";
    }

    protected void btnConsultar0_Click1(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/CreditosPorAprobar/Detalle.aspx");
    }

    protected void btnGrabarSeguimiento_Click(object sender, ImageClickEventArgs e)
    {
        grabar();
    }

    protected void grabar()
    {
         try
        {
            Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();

            vControlCreditos.numero_radicacion = Convert.ToInt64(txtNumeroRadicacion.Text.ToString());
            vControlCreditos.codtipoproceso = ddlAccion.SelectedItem != null ? ddlAccion.SelectedValue : null;
            vControlCreditos.fechaproceso = Convert.ToDateTime(txtFechaActual.Text);
            Usuario pUsuario = (Usuario)Session["usuario"];
            vControlCreditos.cod_persona = pUsuario.codusuario;
            vControlCreditos.cod_motivo = 0;
            vControlCreditos.observaciones = txtObservaciones.Text.ToUpper();
            vControlCreditos.anexos = null;
            vControlCreditos.nivel = 0;
            if ((Session["Datacredito"] != null))
            {
                if(txtFechaConsultaData.Text!=null)
                {
                    vControlCreditos.fechaconsulta_dat = txtFechaConsultaData.Text == "" ? DateTime.MinValue : Convert.ToDateTime(txtFechaConsultaData.Text.Trim());                 
                }
            }

            if ((Session["Datacredito"] == null))
            {
                String FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());
                vControlCreditos.fechaconsulta_dat = Convert.ToDateTime(FechaDatcaredito);
                vControlCreditos.fechaconsulta_dat = FechaDatcaredito == "" ? DateTime.MinValue : Convert.ToDateTime(FechaDatcaredito.Trim());
            }
            Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
            vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, (Usuario)Session["usuario"]);
            btnGrabarSegumiento.Visible = false;
            ddlAccion.Enabled = false;
            txtObservaciones.Enabled = false;
            Navegar(Pagina.Lista);

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ControlTiemposServicio.CodigoProgramaHojaRuta, "btnGrabar_Click", ex);
        }

        ActualizarHistoricoSeguimiento();
    }

    private void ModificarFecha()
    {
        Usuario pUsuario = (Usuario)Session["usuario"];

        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditosmod = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
        vControlCreditosmod.numero_radicacion = Convert.ToInt64(this.txtNumeroRadicacion.Text);
        vControlCreditosmod.fechadata= Convert.ToString(txtFechaConsultaData.Text);
        Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
        vControlCreditosmod = ControlCreditosServicio.Modificarfechadatcredito(vControlCreditosmod, (Usuario)Session["usuario"]);
    }

    protected void ddlAccion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccion.Text=="9")
        {
           
            MultiViewRuta.Visible = true;
            MultiViewRuta.ActiveViewIndex = 1;
        }
        if (ddlAccion.Text == "8")
        {
            MultiViewRuta.ActiveViewIndex = 0;
        }
    }

    public void MensajeFinal(string pmensaje)
    {
        lblMensajeGrabar.Text = pmensaje;
    }

    protected void btnAcpNegar_Click(object sender, EventArgs e)
    {
       // Validate("vgEdad");
        if (Page.IsValid)
        {
            CreditoSolicitadoService creditoServicio = new CreditoSolicitadoService();
            CreditoSolicitado credito = new CreditoSolicitado();
            Motivo motivo = new Motivo();
            Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
            credito.Nombres = lblUsuario.Text;
            motivo.Codigo = Int16.Parse(ddlNegar.SelectedValue);
            credito.ObservacionesAprobacion = txtObs5.Text;
            credito.NumeroCredito = Int32.Parse(txtCredito5.Text);        
            creditoServicio.NegarCredito(credito, motivo, (Usuario)Session["usuario"]);
            this.lblMensajeGrabar.Visible = true;
            MensajeFinal("Crédito " + txtNumeroRadicacion.Text + " Negado");
            MultiViewRuta.Visible = false;
            ddlAccion.Enabled = false;
            btnGrabarSegumiento.Visible = false;
            txtObservaciones.Text = txtObs5.Text;
            grabar();
            
        }
    }

    protected void ddlNegar_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  txtCredito5.Text = txtNumeroRadicacion.Text;
    }

    protected void btnCncNegar_Click(object sender, EventArgs e)
    {
      txtObservaciones.Text = "";
      txtObs5.Text = "";
      ddlNegar.SelectedIndex = 0;
    }

}