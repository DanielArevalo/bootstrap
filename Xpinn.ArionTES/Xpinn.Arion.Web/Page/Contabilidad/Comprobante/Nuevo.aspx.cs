using Cantidad_a_Letra;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Comun.Entities;
using Xpinn.Contabilidad.Entities;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.IO;
using System.Reflection;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
    private Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
    private Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private List<Xpinn.Contabilidad.Entities.ProcesoContable> LstProcesoContable;
    private Xpinn.Contabilidad.Services.ParametroCtasCreditosService servicePar_cue_lin = new Xpinn.Contabilidad.Services.ParametroCtasCreditosService();
    private Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();


    private String operacion = "";
    private String tipobeneficiario;
    //private string idNComp = "";
    //private string idTComp = ""

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Request.QueryString["tipo_comp"] != null)
            {
                if (Request.QueryString["tipo_comp"].ToString().Trim() == "0")
                {
                    ComprobanteServicio.CodigoPrograma = ComprobanteServicio.CodigoProgramaContable;
                }
                else if (Request.QueryString["tipo_comp"].ToString().Trim() == "1")
                {
                    ComprobanteServicio.CodigoPrograma = ComprobanteServicio.CodigoProgramaIngreso;
                }
                else if (Request.QueryString["tipo_comp"].ToString().Trim() == "5")
                {
                    ComprobanteServicio.CodigoPrograma = ComprobanteServicio.CodigoProgramaEgreso;
                }
                else
                {
                    ComprobanteServicio.CodigoPrograma = ComprobanteServicio.CodigoProgramaContable;
                }
            }
            //if(Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] == null || Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] == null)
            //{
            //    Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = Session["idNComp"];
            //    Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] = Session["idTComp"];
            //}
            if (Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] != null & Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] != null)
            {
                VisualizarOpciones(ComprobanteServicio.CodigoPrograma, "E");
                List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();
                Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
                if(ComprobanteServicio.ConsultarComprobante(Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".num_comp"]), Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"]), ref vComprobante, ref LstDetalleComprobante, (Usuario)Session["Usuario"]))
                {
                    Session["DetalleComprobante"] = LstDetalleComprobante;
                }
            }
            else { VisualizarOpciones(ComprobanteServicio.CodigoPrograma, "A");}

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnInforme_Click;

            ctlBusquedaPersonas.eventotxtIdentificacion_TextChanged += (obj, evt) => BuscarNumeroDeFacturaComprobanteEgreso();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                Page.MaintainScrollPositionOnPostBack = true;
                PanelActiveView(vwPanel1);
                // Inicializar Detalle de Comprobante
                Site toolBar = (Site)this.Master;
                toolBar.MostrarImprimir(false);

                /*String sessionbloqueo = Request["_sessionbloqueo"];
                if (sessionbloqueo == "")
                {*/
                    // Inicializar variables de sesion
                    ViewState["DataAtribuciones"] = null;
                    Session["Modificar"] = null;

                    //Session["DetalleComprobante"] = null;
                    Session["Nuevo"] = null;
                    Session["Carga"] = null;
                    Session["idNComp"] = null;
                    Session["idTComp"] = null;
                    Session["cod_ope"] = 0;
                    Session["Ruta_Cheque"] = null;
                    //Agregado para validar cuentas de producto si se le adiciona detalle a un comprobante
               
                    Session["detalle"] = null;
                //}
                // Session["Comprobanteanulacion"] = null;

                // Permite el control del evento de modificación de datos en la identificación del beneficiario y del tercero
                //string js = "javascript:" + Page.GetPostBackEventReference(this, "@@@@@buttonPostBack") + ";";
                //txtIdentificacion.Attributes.Add("onchange", js);
                //txtIdentificD.Attributes.Add("onchange", js);

                // Determinar el separador décimal
                string s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                Configuracion conf = new Configuracion();

                // Coloca la fecha actual por defecto a los comprobantes
                if (ddlTipoComprobante.SelectedValue == "5")
                {
                    txtFecha.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
                    PanelFooter.Visible = true;
                }
                else
                {
                    txtFecha.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
                    PanelFooter.Visible = false;
                }

                // Explota si la url tiene menos de 4 segmentos, ya me ha pasado varias veces (Creando uno nuevo desde 0) ya que no tiene indice 4 si no indice 3
                string segmentoURL = Request.UrlReferrer.Segments.Count() > 4 ? Request.UrlReferrer.Segments[4] : string.Empty;

                if (segmentoURL == "Comprobante/")
                {
                    if (Session[ComprobanteServicio.CodigoPrograma + ".detalle"] != null)
                    {
                        toolBar.MostrarGuardar(false);
                        btnDetalle.Visible = false;
                    }
                }
                // Si ya se determinó los datos del banco, cuenta y número de cheque entonces asignarlos
                if (Session["numerocheque"] != null && Session["entidad"] != null && Session["cuenta"] != null)
                {
                    txtNumSop.Text = Session["numerocheque"].ToString();
                    ddlEntidadOrigen.SelectedValue = Session["entidad"].ToString();
                    try
                    {
                        ddlCuenta.SelectedValue = Session["cuenta"].ToString();
                    }
                    catch
                    {
                        VerError("No pudo asignar la cuenta " + Session["cuenta"].ToString());
                    }
                    ddlEntidadOrigen.Enabled = false;
                    ddlCuenta.Enabled = false;
                }

                // Llenar los DDL para poder seleccionar los datos
                LlenarCombos();

                // Si hay cuentas bancarias a nombre de la entidad las muestras
                ActivarDDLCuentas(false);
                Session["NUM_AUXILIO"] = null;
                if (segmentoURL != "Desembolso/" && segmentoURL != "DesembolsoAuxilios/"
                   && segmentoURL != "DesembolsoServicios/")
                {
                    Session["NumCred_Orden"] = null;
                }
                else
                {
                    if (segmentoURL == "DesembolsoAuxilios/")
                    {
                        if (Session["NumCred_Orden"] != null)
                            Session["NUM_AUXILIO"] = Session["NumCred_Orden"].ToString();
                    }
                    else if (segmentoURL == "DesembolsoServicios/")
                    {
                        if (Session["NumCred_Orden"] != null)
                            Session["NUM_AUXILIO"] = Session["NumCred_Orden"].ToString() + "/1";
                    }
                }

                //Se valida si se esta modificando un comprobante
                if (Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] != null & Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] != null && Session[ComprobanteServicio.CodigoPrograma + ".num_comp"].ToString() != "" & Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"].ToString() != "")
                {
                    try
                    {
                        if (Request.UrlReferrer != null)
                        {
                            Usuario pUsu = (Usuario)Session["Usuario"];
                            if (Request.UrlReferrer.ToString().Contains("/ModificacionComprobantes/"))
                            {
                                Session["Modificar"] = "1";
                                //CONSULTAR SUPER USUARIO
                                ValidarPermisosEdicion(pUsu);
                                toolBar.MostrarImprimir(true);
                            }
                            if (Request.UrlReferrer.ToString().Contains("/Comprobante/"))
                            {
                                ValidarPermisosEdicion(pUsu);
                                toolBar.MostrarImprimir(true);
                            }
                        }
                        if (Session[ComprobanteServicio.CodigoPrograma + "cod_traslado"] != null)
                        {
                            if (ConvertirStringToInt32(Session[ComprobanteServicio.CodigoPrograma + "cod_traslado"].ToString()) > 0)
                            {
                                toolBar.MostrarGuardar(false);
                                Session.Remove(ComprobanteServicio.CodigoPrograma + "cod_traslado");
                            }
                        }
                    }
                    catch
                    {
                    }
                    Usuario usuap = (Usuario)Session["usuario"];
                    string idNComp = Session[ComprobanteServicio.CodigoPrograma + ".num_comp"].ToString();
                    string idTComp = Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"].ToString();
                    idObjeto = idNComp;
                    ObtenerDatos(idNComp, idTComp);
                    ObtenerObservacionesAnulaciones(idNComp, idTComp);
                    PanelActiveView(vwPanel1);
                    tbxOficina.Text = usuap.nombre_oficina;
                    ddlTipoComprobante.Enabled = false;
                    Session["idNComp"] = idNComp;
                    Session["idTComp"] = idTComp;
                }
                else
                {
                    // Se valida si el comprobante nuevo corresponde a un comprobante generado o no.
                    if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
                    {
                        Usuario usuap = (Usuario)Session["usuario"];
                        PanelActiveView(vwPanel3);
                        Int64 pcod_ope = 0;
                        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] != null)
                            pcod_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"]);
                        Int64 ptip_ope = 0;
                        if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
                            ptip_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"]);
                        DateTime pfecha = System.DateTime.Now;
                        if (Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] != null)
                            pfecha = Convert.ToDateTime(Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"]);
                        Int64 pcod_ofi = usuap.cod_oficina;
                        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] != null)
                            pcod_ofi = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"]);
                        Int64 ptipo_comp = 0;
                        if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] != null)
                            ptipo_comp = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"]);
                        Int32 pIdGiro = 0;
                        if (Session[ComprobanteServicio.CodigoPrograma + ".idgiro"] != null)
                            pIdGiro = Convert.ToInt32(Session[ComprobanteServicio.CodigoPrograma + ".idgiro"]);
                        LstProcesoContable = ComprobanteServicio.ConsultaProcesoUlt(pcod_ope, ptip_ope, pfecha, usuap);
                        if (LstProcesoContable.Count() == 0)
                        {
                            VerError("No existen comprobantes parametrizados para esta operación " + ptip_ope);
                            ListItem selectedListItem = ddlTipoOperacion.Items.FindByValue(ptip_ope.ToString());
                            if (selectedListItem != null)
                            {
                                selectedListItem.Selected = true;
                                ddlTipoOperacion.Enabled = false;
                            }
                            toolBar.MostrarGuardar(false);
                            PanelActiveView(vwPanel6);
                        }
                        if (LstProcesoContable.Count() > 1)
                        {
                            lstProcesos.DataTextField = "descripcion";
                            lstProcesos.DataValueField = "cod_proceso";
                            lstProcesos.DataSource = LstProcesoContable;
                            lstProcesos.DataBind();
                        }
                        if (LstProcesoContable.Count() == 1 || ptipo_comp > 0)
                        {
                            GenerarComprobante(pcod_ope, ptip_ope, pfecha, ptipo_comp, pcod_ofi);
                            if (pIdGiro > 0)
                            {  //Si tiene un giro creado se consulta y cargan los datos
                                ObtenerDatosGiro(pIdGiro);
                            }
                            Session["cod_ope"] = pcod_ope != 0 ? pcod_ope : 0;
                        }
                        ddlTipoComprobante.Enabled = false;
                    }
                    else
                    {
                        Usuario usuap = (Usuario)Session["usuario"];
                        tbxOficina.Text = usuap.nombre_oficina;
                        txtCodElabora.Text = Convert.ToString(usuap.codusuario);
                        txtElaboradoPor.Text = usuap.nombre;
                        txtFecha.Enabled = true;
                        // Se crea un detalle vacio para activar botones de la grilla
                        CrearDetalleInicial(0);
                        // Mostrar datos para seleccionar el tipo de comprobante cuando es elaboraciòn
                        string sParametros = Request.QueryString["tipo_comp"];
                        if (sParametros != null)
                        {
                            Session.Remove("Comprobantecopia");
                            if (sParametros.Trim() == "0")
                            {
                                rbContable.Checked = true;
                                PanelActiveView(vwPanel1);
                                Activar();
                            }
                            else if (sParametros.Trim() == "1")
                            {
                                rbIngreso.Checked = true;
                                PanelActiveView(vwPanel1);
                                Activar();
                            }
                            else if (sParametros.Trim() == "5")
                            {
                                rbEgreso.Checked = true;
                                PanelActiveView(vwPanel1);
                                Activar();
                            }
                            else
                            {
                                PanelActiveView(vwPanel0);
                            }
                            ActivarDDLCuentas(false);
                        }
                    }
                    // Se valida si se va a copiar el comprobante 
                    if ((String)Session["Comprobantecopia"] != null)
                    {
                        String Comprobantecopia = (String)Session["Comprobantecopia"];
                        Usuario usuap = (Usuario)Session["usuario"];
                        string idNComp = (String)Session[ComprobanteServicio.CodigoProgramaCarga + ".num_comp"];
                        string idTComp = (String)Session[ComprobanteServicio.CodigoProgramaCarga + ".tipo_comp"];
                        Session.Remove(ComprobanteServicio.CodigoProgramaCarga + ".num_comp");
                        Session.Remove(ComprobanteServicio.CodigoProgramaCarga + ".tipo_comp");
                        idObjeto = idNComp;
                        if (idTComp != null)
                        {
                            if (idTComp.Trim() == "0")
                            {
                                rbContable.Checked = true;
                            }
                            else if (idTComp.Trim() == "1")
                            {
                                rbIngreso.Checked = true;
                            }
                            else if (idTComp.Trim() == "5")
                            {
                                rbEgreso.Checked = true;
                            }
                            else
                            {
                                PanelActiveView(vwPanel0);
                            }
                        }
                        ObtenerDatosCopia(idNComp, idTComp);
                        PanelActiveView(vwPanel1);
                        tbxOficina.Text = usuap.nombre_oficina;
                        Activar();
                        Session["idNComp"] = idNComp;
                        Session["idTComp"] = idTComp;
                    }

                    // Se valida si se va a anular  el comprobante 
                    if ((String)Session["Comprobanteanulacion"] != null)
                    {
                        String Comprobanteanulacion = (String)Session["Comprobanteanulacion"];
                        Usuario usuap = (Usuario)Session["usuario"];
                        string idNComp = (String)Session[ComprobanteServicio.CodigoProgramaAnulacion + ".num_comp"];
                        string idTComp = (String)Session[ComprobanteServicio.CodigoProgramaAnulacion + ".tipo_comp"];
                        //Session.Remove("Comprobanteanulacion");
                        idObjeto = idNComp;
                        rbContable.Checked = true;
                        ObtenerDatosAnulacion(idNComp, idTComp);
                        PanelActiveView(vwPanel1);
                        tbxOficina.Text = usuap.nombre_oficina;
                        Activar();
                        Session["Comprobanteanulacion"] = null;
                        Session["idNComp"] = idNComp;
                        Session["idTComp"] = idTComp;
                    }

                    // Se valida si se va a cargar el comprobante 
                    if ((String)Session["Comprobantecarga"] != null)
                    {
                        Usuario usuap = (Usuario)Session["usuario"];
                        string idTComp = (String)Session["Comprobantecarga"];
                        Session.Remove("Comprobantecarga");
                        if (idTComp.Trim() == "0")
                        {
                            rbContable.Checked = true;
                        }
                        else if (idTComp.Trim() == "1")
                        {
                            rbIngreso.Checked = true;
                        }
                        else if (idTComp.Trim() == "5")
                        {
                            rbEgreso.Checked = true;
                        }
                        else
                        {
                            PanelActiveView(vwPanel0);
                        }
                        Session["Carga"] = "1";
                        ObtenerDatosCarga();
                        PanelActiveView(vwPanel1);
                        tbxOficina.Text = usuap.nombre_oficina;
                        Activar();
                        ddlTipoComprobante.SelectedValue = idTComp;
                    }
                    Usuario pUsuario = (Usuario)Session["usuario"];
                    if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] == null && Session[ComprobanteServicio.CodigoPrograma + ".cod_traslado"] == null)
                    {
                        try
                        {
                            Session["Nuevo"] = "1";
                            if (rbIngreso.Checked == true)
                                txtNumComp.Text = Convert.ToString(ComprobanteServicio.ObtenerSiguienteCodigo(1, System.DateTime.Now, pUsuario.cod_oficina, pUsuario));
                            else if (rbEgreso.Checked == true)
                                txtNumComp.Text = Convert.ToString(ComprobanteServicio.ObtenerSiguienteCodigo(5, System.DateTime.Now, pUsuario.cod_oficina, pUsuario));
                            else
                                txtNumComp.Text = Convert.ToString(ComprobanteServicio.ObtenerSiguienteCodigo(0, System.DateTime.Now, pUsuario.cod_oficina, pUsuario));
                        }
                        catch (Exception ex) { VerError("Error al determinar el número del nuevo comprobante" + ex.Message); }
                        try
                        {
                            Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
                            Xpinn.Caja.Entities.Oficina oficina = new Xpinn.Caja.Entities.Oficina();
                            oficina = oficinaServicio.ConsultarOficina(pUsuario.cod_oficina, pUsuario);
                            ddlCiudad.SelectedValue = Convert.ToString(oficina.cod_ciudad);
                        }
                        catch (Exception ex) { VerError("Error al determinar ciudad de la oficina" + ex.Message); }
                    }
                    // Si es comprobante de causación no habilitar el botón de grabar 
                    if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
                        if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"].ToString() == "37")
                            toolBar.MostrarGuardar(false);
                }
                operacion = (String)Session["operacion"];
                if (operacion == null)
                {
                    operacion = "";
                }
                // Se colocá para validar el tipo de comprobante.
                ddlTipoComprobante_SelectedIndexChanged(ddlTipoComprobante, null);
                /***************************VALIDO SI VIENE DE ALGUN CIERRE****************************/
                //No mostrar el boton guardar y un mensaje que dice que ya se generó el comprobante
                if (Convert.ToBoolean(Session["OrigencCierre"]) == true)
                {
                    toolBar.MostrarGuardar(false);
                    Lblerror.Text = "Ya se generó y guardó el comprobante de cierre.";
                    Lblerror.ForeColor = System.Drawing.Color.Green;
                }

                #region Interactuar WorkManagement

                // No cambiar tipos de datos
                int? esComprobanteDesembolsoCredito = Session[ComprobanteServicio.CodigoPrograma + ".esDesembolsoCredito"] as int?;
                long? numeroRadicacionGenerado = Session[ComprobanteServicio.CodigoPrograma + ".numeroRadicacion"] as long?;

                Session.Remove(ComprobanteServicio.CodigoPrograma + ".esDesembolsoCredito");
                Session.Remove(ComprobanteServicio.CodigoPrograma + ".numeroRadicacion");

                // Valido si vengo de desembolso de credito
                if (esComprobanteDesembolsoCredito.HasValue && esComprobanteDesembolsoCredito.Value == 1 && numeroRadicacionGenerado.HasValue)
                {
                    General parametroHabilitaOperacionesWM = ConsultarParametroGeneral(35);
                    if (parametroHabilitaOperacionesWM != null && parametroHabilitaOperacionesWM.valor.Trim() == "1")
                    {
                        General parametroObligaOperacionesWM = ConsultarParametroGeneral(39);
                        bool obligaOperaciones = parametroObligaOperacionesWM != null && parametroObligaOperacionesWM.valor == "1";
                        bool operacionFueExitosa = false;

                        try
                        {
                            RpviewComprobante.Visible = true;
                            GeneraReporteComprobantePago(null);
                            byte[] bytesReporte = ObtenerBytesReporteAsPDF(RpviewComprobante, FormatoArchivo.PDF);
                            RpviewComprobante.Visible = false;

                            WorkManagementServices workManagementService = new WorkManagementServices();
                            // Buscamos el workflow de este credito, el cual ya deberia existir y estar valido ya que se crea en la solicitud de credito
                            WorkFlowCreditos workFlowCredito = workManagementService.ConsultarWorkFlowCreditoPorNumeroRadicacion(Convert.ToInt32(numeroRadicacionGenerado.Value), Usuario);

                            if (workFlowCredito != null && workFlowCredito.workflowid > 0 && !string.IsNullOrWhiteSpace(workFlowCredito.barCodeRadicacion))
                            {
                                string descripcion = "Comprobante de desembolso para el credito con numero de radicacion: " + numeroRadicacionGenerado.ToString();

                                WorkFlowFilesDTO file = new WorkFlowFilesDTO
                                {
                                    Base64DataFile = Convert.ToBase64String(bytesReporte),
                                    Descripcion = descripcion,
                                    Extension = ".pdf"
                                };

                                InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);
                                bool anexoExitoso = interfaz.AnexarArchivoAWorkFlowCartera(file, workFlowCredito.workflowid, TipoArchivoWorkManagement.ComprobanteDesembolsoCredito);

                                if (anexoExitoso)
                                {
                                    operacionFueExitosa = interfaz.RunTaskWorkFlowCredito(workFlowCredito.barCodeRadicacion, workFlowCredito.workflowid, StepsWorkManagementWorkFlowCredito.CargaComprobanteDesembolso, descripcion);
                                }
                            }

                            if (obligaOperaciones && !operacionFueExitosa)
                            {
                                VerError("No se pudo correr la tarea en el WM para este workFlow");
                            }
                        }
                        catch (Exception)
                        {
                            if (obligaOperaciones)
                            {
                                VerError("No se pudo correr la tarea en el WM para este workFlow");
                            }
                        }
                    }
                }

                // No cambiar tipos de datos 
                bool? esRealizacionGiro = Session[ComprobanteServicio.CodigoPrograma + ".realizoGiro"] as bool?;
                long? codigoOperacion = Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] as long?;

                Session.Remove(ComprobanteServicio.CodigoPrograma + ".realizoGiro");
                // Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_ope");  ==> Se quitó porque al generar comprobantes cuando tiene varios procesos no funciona bien. FerOrt. 27-Oct-2017.

                // Valido si vengo de realizar un giro
                if (esRealizacionGiro.HasValue && esRealizacionGiro.Value && codigoOperacion.HasValue)
                {
                    General parametroHabilitaOperacionesWM = ConsultarParametroGeneral(35);
                    if (parametroHabilitaOperacionesWM != null && parametroHabilitaOperacionesWM.valor.Trim() == "1")
                    {
                        try
                        {
                            WorkManagementServices workManagementService = new WorkManagementServices();
                            List<WorkFlowCreditos> listaWorkFlowCreditos = workManagementService.ConsultarGirosRealizadosPertenecientesAUnWorkFlowCreditosSegunCodigoOperacion(codigoOperacion.Value, Usuario);

                            // Si son giros de WorkFlow Creditos habra cosas aca
                            if (listaWorkFlowCreditos.Count > 0)
                            {
                                RpviewComprobante.Visible = true;
                                GeneraReporteComprobantePago(null);
                                byte[] bytesReporte = ObtenerBytesReporteAsPDF(RpviewComprobante, FormatoArchivo.PDF);
                                RpviewComprobante.Visible = false;

                                InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);
                                foreach (var workFlowCredito in listaWorkFlowCreditos)
                                {
                                    if (workFlowCredito.workflowid > 0 && !string.IsNullOrWhiteSpace(workFlowCredito.barCodeRadicacion))
                                    {
                                        string descripcion = "Comprobante de realizacion de giro para el credito con numero de radicacion: " + workFlowCredito.numeroradicacion.ToString();

                                        WorkFlowFilesDTO file = new WorkFlowFilesDTO
                                        {
                                            Base64DataFile = Convert.ToBase64String(bytesReporte),
                                            Descripcion = descripcion,
                                            Extension = ".pdf"
                                        };

                                        bool anexoExitoso = interfaz.AnexarArchivoAWorkFlowCartera(file, workFlowCredito.workflowid, TipoArchivoWorkManagement.ComprobanteGiroCredito);

                                        if (anexoExitoso)
                                        {
                                            bool runTaskExitoso = interfaz.RunTaskWorkFlowCredito(workFlowCredito.barCodeRadicacion, workFlowCredito.workflowid, StepsWorkManagementWorkFlowCredito.CargaComprobanteGiro, descripcion);
                                        }
                                    }
                                }
                            }
                            else // Si no soy giros de WorkFlow Creditos entonces busco si soy giros de Cruce de cuentas
                            {
                                List<WorkFlowCruceCuentas> listaWorkFlowCruceCuentas = workManagementService.ConsultarGirosRealizadosPertenecientesAUnWorkFlowCruceCuentasSegunCodigoOperacion(codigoOperacion.Value, Usuario);

                                if (listaWorkFlowCruceCuentas.Count > 0)
                                {
                                    RpviewComprobante.Visible = true;
                                    GeneraReporteComprobantePago(null);
                                    byte[] bytesReporte = ObtenerBytesReporteAsPDF(RpviewComprobante, FormatoArchivo.PDF);
                                    RpviewComprobante.Visible = false;

                                    InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);
                                    foreach (var workFlowCruceCuenta in listaWorkFlowCruceCuentas)
                                    {
                                        if (!string.IsNullOrWhiteSpace(workFlowCruceCuenta.barcode))
                                        {
                                            string descripcion = "Comprobante de realizacion de giro de cruce de cuentas";

                                            WorkFlowFilesDTO file = new WorkFlowFilesDTO
                                            {
                                                Base64DataFile = Convert.ToBase64String(bytesReporte),
                                                Descripcion = descripcion,
                                                Extension = ".pdf"
                                            };

                                            bool anexoExitoso = interfaz.AnexarArchivoAFormularioRetiro(file, workFlowCruceCuenta.barcode, TipoArchivoWorkManagement.ComprobanteGiroCruceCuentas);

                                            if (anexoExitoso)
                                            {
                                                bool runTaskExitoso = interfaz.RunTaskWorkFlowRetiroAsociado(workFlowCruceCuenta.barcode, StepsWorkManagementWorkFlowRetiroAsociados.CargaComprobanteGiro, descripcion);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

                // No cambiar tipos de datos
                string radicadoCruceCuentas = Session[ComprobanteServicio.CodigoPrograma + ".radicadoCruceCuentas"] as string;
                Session.Remove(ComprobanteServicio.CodigoPrograma + ".radicadoCruceCuentas");

                // Valido si vengo de un cruce de cuentas
                if (!string.IsNullOrWhiteSpace(radicadoCruceCuentas))
                {
                    RpviewComprobante.Visible = true;
                    GeneraReporteComprobantePago(null);
                    byte[] bytesReporte = ObtenerBytesReporteAsPDF(RpviewComprobante, FormatoArchivo.PDF);
                    RpviewComprobante.Visible = false;

                    string descripcion = "Comprobante de Desembolso de Cruce de Cuentas de radicado: " + radicadoCruceCuentas;
                    WorkFlowFilesDTO file = new WorkFlowFilesDTO
                    {
                        Base64DataFile = Convert.ToBase64String(bytesReporte),
                        Descripcion = descripcion,
                        Extension = ".pdf"
                    };

                    InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);
                    bool anexoExitoso = interfaz.AnexarArchivoAFormularioRetiro(file, radicadoCruceCuentas, TipoArchivoWorkManagement.ComprobanteDesembolsoCruceCuentas);

                    if (anexoExitoso)
                    {
                        string comentario = "Carga de Comprobante de desembolso de Cruce de Cuentas de radicado: " + radicadoCruceCuentas;

                        // RunTask, corre al siguiente proceso, debes identificar en el proceso que estas y añadir las observaciones
                        interfaz.RunTaskWorkFlowRetiroAsociado(radicadoCruceCuentas, StepsWorkManagementWorkFlowRetiroAsociados.CargaComprobanteDeDesembolso, comentario);
                    }
                }


                #endregion


            }
            else
            {
                // Esto es para que no desaparezca el listado de personas cuando se presiona el botón de buscar
                if (ViewState["MostrarBusqueda"] == null)
                {
                    // Calcular el total del comprobante
                    //ObtenerDetalleComprobante(false);
                    //CalcularTotal();
                }
            }

            //***************************************
            //**REIMPRESION RECIBO DE PAGO
            //**************************************
            Xpinn.Caja.Services.TipoOperacionService tipOpeService = new Xpinn.Caja.Services.TipoOperacionService();
            Xpinn.Caja.Entities.TipoOperacion factura = new Xpinn.Caja.Entities.TipoOperacion();

            if (Session[Usuario.codusuario + "codOpe"] == null || Session[Usuario.codusuario + "cod_persona"] == null && Session["CodigoOpe"] != null)
            {
                //Se realiza consulta si existe una factura para ese tipo de operacion que carga el comprobante 
                factura = tipOpeService.ConsultarFacturaCompleta(Convert.ToInt64(Session["Ope_Recib"]), true, (Usuario)Session["usuario"]);
            }

            if (factura.cod_operacion != null && factura.cod_persona != null)
            {
                Session[Usuario.codusuario + "codOpe"] = factura.cod_operacion;
                Session[Usuario.codusuario + "cod_persona"] = factura.cod_persona;
            }

            if (Session[Usuario.codusuario + "codOpe"] == null || Session[Usuario.codusuario + "cod_persona"] == null)
            {
                btnRecibo.Visible = false;
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "Page_Load", ex);
        }

    }


    protected List<int> AtribucionesUsuario(Usuario pUsu)
    {
        Xpinn.Seguridad.Services.UsuarioService BOUsuario = new Xpinn.Seguridad.Services.UsuarioService();
        Usuario vUsuario = new Usuario();
        try { vUsuario = BOUsuario.ConsultarUsuario(pUsu.codusuario, pUsu); } catch { }
        ViewState.Add("DataAtribuciones", vUsuario.LstAtribuciones);
        return vUsuario.LstAtribuciones;
    }

    private void ValidarPermisosEdicion(Usuario pUsu)
    {
        if (pUsu.codperfil == 1)
            txtFecha.Enabled = true;
        else
        {
            //Buscar atribuciones de usuario; 4 => fecha / 5 => Detalle
            List<int> lstAtribuciones = new List<int>();
            lstAtribuciones = AtribucionesUsuario(pUsu);
            if (lstAtribuciones != null)
            {
                if (lstAtribuciones.Count() > 0)
                {
                    //PUEDE MODIFICAR FECHA
                    if (lstAtribuciones[4] == 1)
                        txtFecha.Enabled = true;
                }
            }
        }

        return;
    }


    protected void ObtenerDatosGiro(Int32 pIdGiro)
    {
        try
        {
            if (pIdGiro > 0)
            {
                Xpinn.Tesoreria.Services.GiroServices GiroService = new Xpinn.Tesoreria.Services.GiroServices();
                Xpinn.Tesoreria.Entities.Giro pGiro = new Xpinn.Tesoreria.Entities.Giro();
                pGiro = GiroService.ConsultarGiro(pIdGiro.ToString().Trim(), (Usuario)Session["usuario"]);
                if (pGiro.idgiro != 0)
                {
                    lblIdGiro.Text = pGiro.idgiro.ToString();
                    if (pGiro.forma_pago != null)
                    {
                        ddlFormaPago.SelectedValue = pGiro.forma_pago.ToString();
                        ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                    }
                    if (pGiro.forma_pago == 2 || pGiro.forma_pago == 3)
                    {
                        ActivarBancos(true);
                        panelTransferencia.Visible = false;

                        if (pGiro.cod_banco1 != null)
                            ddlEntidadOrigen.SelectedValue = pGiro.cod_banco1.ToString();
                        ddlEntidadOrigen_SelectedIndexChanged(ddlEntidadOrigen, null);
                        if (pGiro.num_referencia1 != null && pGiro.idctabancaria != 0)
                            ddlCuenta.SelectedValue = pGiro.idctabancaria.ToString();
                        if (pGiro.forma_pago == 3)
                        {
                            if (pGiro.cod_banco != null)
                                ddlEntidad.SelectedValue = pGiro.cod_banco.ToString();
                            if (pGiro.num_cuenta != null)
                                txtNum_cuenta.Text = pGiro.num_cuenta;
                            ddlTipo_cuenta.SelectedValue = pGiro.tipo_cuenta.ToString();
                            panelTransferencia.Visible = true;
                        }
                    }
                }
            }

            return;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void GenerarComprobante(Int64 pcod_ope, Int64 ptip_ope, DateTime pfecha, Int64 ptipo_comp, Int64 pcod_ofi)
    {
        Usuario usuap = (Usuario)Session["usuario"];
        Int64 pnum_comp = 0;
        Int64 pcod_proceso = 0;
        Int64 pcod_persona = 0;
        try
        {
            tbxOficina.Text = usuap.nombre_oficina;
            // Determinar el código del proceso
            if (LstProcesoContable != null)
                if (LstProcesoContable[0] != null)
                    if (LstProcesoContable[0].cod_proceso != null)
                        pcod_proceso = Convert.ToInt64(LstProcesoContable[0].cod_proceso);
            // Determinar el código de la persona
            if (ComprobanteServicio.CodigoPrograma + ".cod_persona" != null)
                pcod_persona = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"]);
        }
        catch (Exception ex)
        {
            VerError("Error al determinar código de proceso: " + ex.Message);
            PanelActiveView(vwPanel5);
        }
        bool bGenerado = false;
        string Error = "";
        try
        {
            // Generar el comprobante            
            bGenerado = ComprobanteServicio.GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref Error, usuap);
            if (Error.Trim() != "")
                VerError(Error);
        }
        catch (Exception ex)
        {
            VerError("Error al generar: " + Error + " " + ex.Message);
            PanelActiveView(vwPanel5);
        }
        if (bGenerado == true)
        {
            // Asignar el número de comprobante generado
            idObjeto = pnum_comp.ToString();
            // Cargar los datos del comprobante generado                
            try
            {
                ObtenerDatos(pnum_comp.ToString(), ptipo_comp.ToString());
            }
            catch (Exception ex)
            {
                VerError("Error al mostrar el comprobante: " + ex.Message);
                PanelActiveView(vwPanel5);
                return;
            }
            // Si se maneja una cuenta bancaria desde el proceso entonces asignarla
            try
            {
                if (Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"] != null)
                    ddlCuenta.SelectedItem.Text = Convert.ToString(Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"]);
            }
            catch { }
            // Ir a la pantalla del comprobante
            PanelActiveView(vwPanel1);
            // Asignar la oficina a la que pertenezca el usuario
            tbxOficina.Text = usuap.nombre_oficina;
        }
        else
        {
            VerError("No se generó el comprobante. Error:" + Error);
            PanelActiveView(vwPanel5);
        }

        return;
    }

    /// <summary>
    /// Método para llenar los combos
    /// </summary>
    protected void LlenarCombos()
    {
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];

        Xpinn.Caja.Services.TipoIdenService IdenService = new Xpinn.Caja.Services.TipoIdenService();
        Xpinn.Caja.Entities.TipoIden identi = new Xpinn.Caja.Entities.TipoIden();
        ddlTipoIdentificacion.DataSource = IdenService.ListarTipoIden(identi, usuario);
        ddlTipoIdentificacion.DataTextField = "descripcion";
        ddlTipoIdentificacion.DataValueField = "codtipoidentificacion";
        ddlTipoIdentificacion.DataBind();

        ddlTipoIdentificacion0.DataSource = IdenService.ListarTipoIden(identi, usuario);
        ddlTipoIdentificacion0.DataTextField = "descripcion";
        ddlTipoIdentificacion0.DataValueField = "codtipoidentificacion";
        ddlTipoIdentificacion0.DataBind();

        Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
        Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();
        ddlTipoComprobante.DataSource = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "", (Usuario)Session["Usuario"]);
        ddlTipoComprobante.DataTextField = "descripcion";
        ddlTipoComprobante.DataValueField = "tipo_comprobante";
        ddlTipoComprobante.DataBind();

        ddlTipoComp.DataSource = ddlTipoComprobante.DataSource;
        ddlTipoComp.DataTextField = "descripcion";
        ddlTipoComp.DataValueField = "tipo_comprobante";
        ddlTipoComp.DataBind();

        ddlFormaPago.Items.Insert(0, new ListItem("Efectivo", "1"));
        ddlFormaPago.Items.Insert(1, new ListItem("Cheque", "2"));
        ddlFormaPago.Items.Insert(2, new ListItem("Transferencia", "3"));
        ddlFormaPago.Items.Insert(3, new ListItem("Otros", "4"));
        ddlFormaPago.Items.Insert(4, new ListItem("Consignación", "5"));
        ddlFormaPago.Items.Insert(5, new ListItem("Datafono", "10"));
        ddlFormaPago.SelectedIndex = 0;
        ddlFormaPago.DataBind();

        Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
        ddlEntidadOrigen.DataSource = BancosService.ListarBancos(Bancos, usuario);
        ddlEntidadOrigen.DataTextField = "nombrebanco";
        ddlEntidadOrigen.DataValueField = "cod_banco";
        ddlEntidadOrigen.DataBind();

        Xpinn.Caja.Services.CiudadService CiudadService = new Xpinn.Caja.Services.CiudadService();
        Xpinn.Caja.Entities.Ciudad Ciudad = new Xpinn.Caja.Entities.Ciudad();
        ddlCiudad.DataSource = CiudadService.ListadoCiudad(Ciudad, usuario);
        ddlCiudad.DataTextField = "nom_ciudad";
        ddlCiudad.DataValueField = "cod_ciudad";
        ddlCiudad.DataBind();

        Xpinn.Contabilidad.Services.ConceptoService ConceptoService = new Xpinn.Contabilidad.Services.ConceptoService();
        Xpinn.Contabilidad.Entities.Concepto Concepto = new Xpinn.Contabilidad.Entities.Concepto();
        ddlConcepto.DataSource = ConceptoService.ListarConcepto(Concepto, usuario);
        ddlConcepto.DataTextField = "descripcion";
        ddlConcepto.DataValueField = "concepto";
        ddlConcepto.DataBind();

        ddlConcep.DataSource = ddlConcepto.DataSource;
        ddlConcep.DataTextField = "descripcion";
        ddlConcep.DataValueField = "concepto";
        ddlConcep.DataBind();

        Xpinn.Caja.Services.TipoOperacionService TipOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoOperacion TipOpe = new Xpinn.Caja.Entities.TipoOperacion();
        ddlTipoOperacion.DataSource = TipOpeService.ListarTipoOpe(usuario);
        ddlTipoOperacion.DataTextField = "nom_tipo_operacion";
        ddlTipoOperacion.DataValueField = "cod_operacion";
        ddlTipoOperacion.DataBind();

        Xpinn.Contabilidad.Services.EstructuraDetalleService EstDetService = new Xpinn.Contabilidad.Services.EstructuraDetalleService();
        Xpinn.Contabilidad.Entities.EstructuraDetalle pEstDet = new Xpinn.Contabilidad.Entities.EstructuraDetalle();
        ddlEstructura.DataSource = EstDetService.ListarEstructuraDetalle(pEstDet, usuario);
        ddlEstructura.DataTextField = "detalle";
        ddlEstructura.DataValueField = "cod_est_det";
        ddlEstructura.DataBind();


        //FORMA DE PAGO AGREGADOS
        ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
        ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
        ddlTipo_cuenta.SelectedIndex = 1;
        ddlTipo_cuenta.DataBind();

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        ddlEntidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        ddlEntidad.DataTextField = "nombrebanco";
        ddlEntidad.DataValueField = "cod_banco";
        ddlEntidad.DataBind();
        return;
    }


    protected List<Xpinn.Caja.Entities.TipoMoneda> ListaMonedas()
    {
        Xpinn.Caja.Entities.TipoMoneda eMoneda = new Xpinn.Caja.Entities.TipoMoneda();
        Xpinn.Caja.Services.TipoMonedaService TipoMonedaServicio = new Xpinn.Caja.Services.TipoMonedaService();
        List<Xpinn.Caja.Entities.TipoMoneda> LstMoneda = new List<Xpinn.Caja.Entities.TipoMoneda>();
        LstMoneda = TipoMonedaServicio.ListarTipoMoneda(eMoneda, (Usuario)Session["usuario"]);
        return LstMoneda;
    }

    protected List<Xpinn.Caja.Entities.CentroCosto> ListaCentrosCosto()
    {
        List<Xpinn.Caja.Entities.CentroCosto> LstCentroCosto = new List<Xpinn.Caja.Entities.CentroCosto>();
        if (Session["CENTROSCOSTO"] == null)
        {
            Xpinn.Caja.Entities.CentroCosto CenCos = new Xpinn.Caja.Entities.CentroCosto();
            Xpinn.Caja.Services.CentroCostoService CentroCostoServicio = new Xpinn.Caja.Services.CentroCostoService();
            LstCentroCosto = CentroCostoServicio.ListarCentroCosto(CenCos, (Usuario)Session["usuario"]);
            Session["CENTROSCOSTO"] = LstCentroCosto;
        }
        else
        {
            LstCentroCosto = (List<Xpinn.Caja.Entities.CentroCosto>)Session["CENTROSCOSTO"];
        }
        return LstCentroCosto;
    }

    protected List<Xpinn.Contabilidad.Entities.CentroGestion> ListaCentroGestion()
    {
        Xpinn.Contabilidad.Entities.CentroGestion CenGes = new Xpinn.Contabilidad.Entities.CentroGestion();
        List<Xpinn.Contabilidad.Entities.CentroGestion> LstCentroGestion = new List<Xpinn.Contabilidad.Entities.CentroGestion>();
        return LstCentroGestion;
    }


    /// <summary>
    /// Méotodo para mostrar los datos del comprobobante
    /// </summary>
    /// <param name="pIdNComp"></param>
    /// <param name="pIdTComp"></param>
    protected void ObtenerDatosCopia(String pIdNComp, String pIdTComp)
    {
        String Comprobantecopia = (String)Session["Comprobantecopia"];
        if (Comprobantecopia != null || Comprobantecopia != "")
        {
            try
            {
                Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
                Xpinn.Contabilidad.Entities.DetalleComprobante vDetalleComprobante = new Xpinn.Contabilidad.Entities.DetalleComprobante();

                List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();
                if (ComprobanteServicio.ConsultarComprobante(Convert.ToInt64(pIdNComp), Convert.ToInt64(pIdTComp), ref vComprobante, ref LstDetalleComprobante, (Usuario)Session["Usuario"]))
                {
                    Session["DetalleComprobante"] = LstDetalleComprobante;
                    // Mostrar datos del encabezado
                    txtNumComp.Text = HttpUtility.HtmlDecode(vComprobante.num_comp.ToString().Trim());
                    txtFecha.Text = vComprobante.fecha.ToString(GlobalWeb.gFormatoFecha);
                    ddlTipoComprobante.SelectedValue = vComprobante.tipo_comp.ToString();
                    ddlCiudad.SelectedValue = Convert.ToString(vComprobante.ciudad);
                    ddlConcepto.SelectedValue = vComprobante.concepto.ToString();

                    Usuario usuap = (Usuario)Session["usuario"];
                    txtElaboradoPor.Text = Convert.ToString(usuap.nombre);
                    txtCodElabora.Text = usuap.codusuario.ToString();
                    txtCodAprobo.Text = vComprobante.cod_aprobo.ToString();
                    txtEstado.Text = "A";
                    if (!string.IsNullOrEmpty(vComprobante.tipo_benef))
                        tipobeneficiario = vComprobante.tipo_benef.ToString();
                    if (!string.IsNullOrEmpty(vComprobante.iden_benef))
                        txtIdentificacion.Text = HttpUtility.HtmlDecode(vComprobante.iden_benef.ToString().Trim());
                    if (!string.IsNullOrEmpty(vComprobante.tipo_identificacion))
                        ddlTipoIdentificacion.SelectedValue = vComprobante.tipo_identificacion.ToString();
                    txtNombres.Text = HttpUtility.HtmlDecode(vComprobante.nombre);
                    tbxObservaciones.Text = HttpUtility.HtmlDecode(vComprobante.observaciones);

                    // Determinar el tipo de pago del comprobante
                    if (vComprobante.tipo_pago != null)
                        try
                        {
                            ddlFormaPago.SelectedValue = vComprobante.tipo_pago.ToString();
                            ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                        }
                        catch
                        {
                        }

                    // Determinar la entidad bancaria y la cuenta bancaria                    
                    try
                    {
                        if (vComprobante.entidad != null)
                            ddlEntidadOrigen.SelectedValue = vComprobante.entidad.ToString();
                        ActivarDDLCuentas(false);
                        if (ddlTipoComprobante.SelectedValue == "5")
                            LlenarCuenta();
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        ActivarDDLCuentas(true);
                        if (vComprobante.entidad != null)
                            ddlEntidadOrigen.SelectedValue = vComprobante.entidad.ToString();
                    }
                    catch (Exception ex)
                    {
                        VerError(ex.ToString());
                    }
                    if (vComprobante.tipo_comp == 1)
                        txtNumSop.Text = vComprobante.num_consig;
                    if (vComprobante.tipo_comp == 2)
                        txtNumSop.Text = vComprobante.n_documento;

                    // Mostrar datos del detalle
                    if ((LstDetalleComprobante == null) || (LstDetalleComprobante.Count == 0))
                    {
                        CrearDetalleInicial(0);
                    }
                    else
                    {
                        Session["DetalleComprobante"] = LstDetalleComprobante;
                    };

                    CalcularTotal();

                    gvDetMovs.DataSource = LstDetalleComprobante;
                    gvDetMovs.DataBind();

                    // Determinar el valor del giro cuando es comprobante de egreso
                    DetalleComprobante var = new DetalleComprobante();
                    if (ddlTipoComprobante.SelectedValue == "5")
                    {
                        PanelFooter.Visible = true;
                        for (int i = 0; i < LstDetalleComprobante.Count; i++)
                        {
                            var = LstDetalleComprobante[i];
                            string a = var.cod_cuenta.Substring(0, 2);
                            if (ComprobanteServicio.CuentaEsGiro(var.cod_cuenta, (Usuario)Session["Usuario"]))
                            {
                                lablerror0.Text = string.Format("{0:N2}", var.valor);
                                i = LstDetalleComprobante.Count + 1;
                            }
                        }
                    }
                    if (lablerror0.Text == "")
                        lablerror0.Text = "0";

                    // Visualizar campos dependiendo del tipo de comprobante
                    // if (vComprobante.tipo_comp == 1) rbIngreso.Checked = true;
                    // if (vComprobante.tipo_comp == 5) rbEgreso.Checked = true;
                    // if (vComprobante.tipo_comp != 1 & vComprobante.tipo_comp != 5) rbContable.Checked = true;

                    //   Activar();

                    txtidenti.Text = txtIdentificacion.Text;
                    txtnom.Text = HttpUtility.HtmlDecode(txtNombres.Text);
                    ddlTipoIdentificacion.SelectedValue = ddlTipoIdentificacion.SelectedValue;

                }
                ddlTipoComprobante.SelectedValue = Convert.ToString(vComprobante.tipo_comp);

                Configuracion conf = new Configuracion();
                txtNumComp.Text = "";
                idObjeto = "";
                txtFecha.Text = "";
                txtFecha.Enabled = true;
                txtFecha.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
                return;
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "ObtenerDatos", ex);
            }
        }
    }
    protected void btnExportarDet_Click(object sender, EventArgs e)
    {
        List<DetalleComprobante> lstConsulta = (List<DetalleComprobante>)Session["DetalleComprobante"];
        if (Session["DetalleComprobante"] != null)
        {
            string fic = "DetalleComprobante.csv";
            try
            {
                File.Delete(fic);
            }
            catch
            {
            }
            // Generar el archivo
            bool bTitulos = false;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + fic, true);
            foreach (DetalleComprobante item in lstConsulta)
            {
                string texto = "";
                FieldInfo[] propiedades = item.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if (!bTitulos)
                {
                    foreach (FieldInfo f in propiedades)
                    {
                        try {
                            if(f.Name.Split('>').First().Replace("<", "") != "isVisible_base_comp" && f.Name.Split('>').First().Replace("<", "") != "isVisible_porcentaje")
                                texto += f.Name.Split('>').First().Replace("<", "") + ";";
                        } catch { texto += ";"; };
                    }
                    sw.WriteLine(texto);
                    bTitulos = true;
                }
                texto = "";
                int i = 0;
                foreach (FieldInfo f in propiedades)
                {
                    i += 1;
                    object valorObject = f.GetValue(item);
                    // Si no soy nulo
                    if (valorObject != null)
                    {
                        string valorString = valorObject.ToString();
                        if (valorObject is DateTime)
                        {
                            DateTime? fechaValidar = valorObject as DateTime?;
                            if (fechaValidar.Value != DateTime.MinValue)
                            {
                                texto += f.GetValue(item) + ";";
                            }
                            else
                            {
                                texto += "" + ";";
                            }
                        }
                        else
                        {
                            texto += f.GetValue(item) + ";";
                            texto.Replace("\r", "").Replace(";", "");
                        }
                    }
                    else
                    {
                        texto += "" + ";";
                    }
                }
                sw.WriteLine(texto);
            }
            sw.Close();
            System.IO.StreamReader sr;
            sr = File.OpenText(Server.MapPath("") + fic);
            string texo = sr.ReadToEnd();
            sr.Close();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(texo);
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
            HttpContext.Current.Response.Flush();
            File.Delete(Server.MapPath("") + fic);
            HttpContext.Current.Response.End();

        }
    }
    protected void ObtenerDatosAnulacion(String pIdNComp, String pIdTComp)
    {
        String Comprobanteanulacion = (String)Session["Comprobanteanulacion"];
        if (Comprobanteanulacion != null || Comprobanteanulacion != "")
        {
            try
            {
                Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
                List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();
                if (ComprobanteServicio.ConsultarComprobante_Anulacion(Convert.ToInt64(pIdNComp), Convert.ToInt64(pIdTComp), ref vComprobante, ref LstDetalleComprobante, (Usuario)Session["Usuario"]))
                {
                    Session["DetalleComprobante"] = LstDetalleComprobante;
                    // Para anulación usar siempre comprobante contable
                    vComprobante.tipo_comp = 2;

                    // Mostrar datos del encabezado
                    txtNumComp.Text = HttpUtility.HtmlDecode(vComprobante.num_comp.ToString().Trim());
                    txtFecha.Text = vComprobante.fecha.ToString(GlobalWeb.gFormatoFecha);
                    try { ddlTipoComprobante.SelectedValue = vComprobante.tipo_comp.ToString(); }
                    catch { }

                    ddlCiudad.SelectedValue = Convert.ToString(vComprobante.ciudad);
                    ddlConcepto.SelectedValue = vComprobante.concepto.ToString();

                    Usuario usuap = (Usuario)Session["usuario"];
                    txtElaboradoPor.Text = Convert.ToString(usuap.nombre);
                    txtCodElabora.Text = usuap.codusuario.ToString();
                    txtCodAprobo.Text = usuap.codusuario.ToString();
                    txtEstado.Text = vComprobante.estado;
                    if (!string.IsNullOrEmpty(vComprobante.tipo_benef))
                        tipobeneficiario = vComprobante.tipo_benef.ToString();
                    if (!string.IsNullOrEmpty(vComprobante.iden_benef))
                        txtIdentificacion.Text = HttpUtility.HtmlDecode(vComprobante.iden_benef.ToString().Trim());
                    if (!string.IsNullOrEmpty(vComprobante.tipo_identificacion))
                        ddlTipoIdentificacion.SelectedValue = vComprobante.tipo_identificacion.ToString();
                    txtNombres.Text = HttpUtility.HtmlDecode(vComprobante.nombre);
                    tbxObservaciones.Text = HttpUtility.HtmlDecode(Convert.ToString(Session["Observaciones"]));

                    // Determinar el tipo de pago del comprobante
                    if (vComprobante.tipo_pago != null)
                        try
                        {
                            ddlFormaPago.SelectedValue = vComprobante.tipo_pago.ToString();
                            ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                        }
                        catch
                        {
                        }

                    // Determinar la entidad bancaria y la cuenta bancaria                    
                    try
                    {
                        if (vComprobante.entidad != null)
                            ddlEntidadOrigen.SelectedValue = vComprobante.entidad.ToString();
                        ActivarDDLCuentas(false);
                        if (ddlTipoComprobante.SelectedValue == "5")
                            LlenarCuenta();
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        ActivarDDLCuentas(true);
                        if (vComprobante.entidad != null)
                            ddlEntidadOrigen.SelectedValue = vComprobante.entidad.ToString();
                    }
                    catch (Exception ex)
                    {
                        VerError(ex.ToString());
                    }
                    if (vComprobante.tipo_comp == 1)
                        txtNumSop.Text = vComprobante.num_consig;
                    if (vComprobante.tipo_comp == 2)
                        txtNumSop.Text = vComprobante.n_documento;

                    // Mostrar datos del detalle
                    if ((LstDetalleComprobante == null) || (LstDetalleComprobante.Count == 0))
                    {
                        CrearDetalleInicial(0);
                    }
                    else
                    {
                        Session["DetalleComprobante"] = LstDetalleComprobante;
                    };

                    CalcularTotal();

                    gvDetMovs.DataSource = LstDetalleComprobante;
                    gvDetMovs.DataBind();

                    // Determinar el valor del giro cuando es comprobante de egreso
                    DetalleComprobante var = new DetalleComprobante();
                    if (ddlTipoComprobante.SelectedValue == "5")
                    {
                        PanelFooter.Visible = true;
                        for (int i = 0; i < LstDetalleComprobante.Count; i++)
                        {
                            var = LstDetalleComprobante[i];
                            string a = var.cod_cuenta.Substring(0, 2);
                            if (ComprobanteServicio.CuentaEsGiro(var.cod_cuenta, (Usuario)Session["Usuario"]))
                            {
                                lablerror0.Text = string.Format("{0:N2}", var.valor);
                                i = LstDetalleComprobante.Count + 1;
                            }
                        }
                    }
                    if (lablerror0.Text == "")
                        lablerror0.Text = "0";

                    //Visualizar campos dependiendo del tipo de comprobante
                    if (vComprobante.tipo_comp == 1) rbIngreso.Checked = true;
                    if (vComprobante.tipo_comp == 5) rbEgreso.Checked = true;
                    if (vComprobante.tipo_comp != 1 & vComprobante.tipo_comp != 5) rbContable.Checked = true;
                    rbContable.Checked = true;

                    // Activar campos según el tipo de comprobante
                    Activar();

                    // Colocar datos del beneficiarío del cheque
                    txtidenti.Text = txtIdentificacion.Text;
                    txtnom.Text = HttpUtility.HtmlDecode(txtNombres.Text);
                    ddlTipoIdentificacion.SelectedValue = ddlTipoIdentificacion.SelectedValue;

                }
                ddlTipoComprobante.SelectedValue = Convert.ToString(vComprobante.tipo_comp);

                Configuracion conf = new Configuracion();
                txtNumComp.Text = "";
                idObjeto = "";
                txtFecha.Text = "";
                txtFecha.Enabled = true;
                txtFecha.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
                return;
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "ObtenerDatos", ex);
            }
        }
    }

    /// <summary>
    /// Méotodo para mostrar los datos del comprobobante
    /// </summary>
    /// <param name="pIdNComp"></param>
    /// <param name="pIdTComp"></param>
    protected void ObtenerDatosCarga()
    {
        String Comprobantecarga = (String)Session["Comprobantecarga"];
        if (Comprobantecarga != null || Comprobantecarga != "")
        {
            try
            {

                List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();
                int codOper = 1;
                if (Session["Codoper"] != null)
                    codOper = (int)Session["Codoper"];
                if (Session["LSTCARGACOMPROBANTE"] != null)
                {
                    LstDetalleComprobante = (List<Xpinn.Contabilidad.Entities.DetalleComprobante>)Session["LSTCARGACOMPROBANTE"];
                    Session.Remove("LSTCARGACOMPROBANTE");
                }
                else
                {
                    string pTipoNorma = "L";
                    if (Session["TipoNormaCarga"] != null)
                        pTipoNorma = Session["TipoNormaCarga"].ToString();
                    if (pTipoNorma == "L")
                        LstDetalleComprobante = ComprobanteServicio.ConsultarCargaComprobanteDetalle(codOper, (Usuario)Session["Usuario"]);
                    else
                        LstDetalleComprobante = ComprobanteServicio.ConsultarCargaComprobanteNiifDetalle(codOper, (Usuario)Session["Usuario"]);
                }
                Session.Remove("TipoNormaCarga");
                Session["DetalleComprobante"] = LstDetalleComprobante;

                Usuario usuap = (Usuario)Session["usuario"];
                txtElaboradoPor.Text = Convert.ToString(usuap.nombre);
                txtCodElabora.Text = usuap.codusuario.ToString();
                txtCodAprobo.Text = usuap.codusuario.ToString();
                txtEstado.Text = "E";

                // Mostrar datos del detalle
                if ((LstDetalleComprobante == null) || (LstDetalleComprobante.Count == 0))
                {
                    CrearDetalleInicial(0);
                }
                else
                {
                    Session["DetalleComprobante"] = LstDetalleComprobante;
                };

                CalcularTotal();

                gvDetMovs.DataSource = LstDetalleComprobante;
                gvDetMovs.DataBind();

                Configuracion conf = new Configuracion();
                //txtNumComp.Text = "";
                //idObjeto = "";
                txtFecha.Text = "";
                txtFecha.Enabled = true;
                txtFecha.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
                return;
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "ObtenerDatos", ex);
            }
        }
    }

    /// <summary>
    /// Método para mostrar los datos del comprobobante
    /// </summary>
    /// <param name="pIdNComp"></param>
    /// <param name="pIdTComp"></param>
    protected void ObtenerDatos(String pIdNComp, String pIdTComp)
    {
        Configuracion conf = new Configuracion();
        try
        {
            Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
            Xpinn.Contabilidad.Entities.DetalleComprobante vDetalleComprobante = new Xpinn.Contabilidad.Entities.DetalleComprobante();
            List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();
            if (pIdNComp == "")
                pIdNComp = "";

            if (ComprobanteServicio.ConsultarComprobante(Convert.ToInt64(pIdNComp), Convert.ToInt64(pIdTComp), ref vComprobante, ref LstDetalleComprobante, (Usuario)Session["Usuario"]))
            {
                LstDetalleComprobante = LstDetalleComprobante.OrderByDescending(x => x.tipo).ToList();
                Session["DetalleComprobante"] = LstDetalleComprobante;
                // Mostrar datos del encabezado
                txtNumComp.Text = HttpUtility.HtmlDecode(vComprobante.num_comp.ToString().Trim());
                txtFecha.Text = vComprobante.fecha.ToString(conf.ObtenerFormatoFecha());
                ddlTipoComprobante.SelectedValue = HttpUtility.HtmlDecode(vComprobante.tipo_comp.ToString());
                ddlCiudad.SelectedValue = HttpUtility.HtmlDecode(Convert.ToString(vComprobante.ciudad));
                ddlConcepto.SelectedValue = HttpUtility.HtmlDecode(vComprobante.concepto.ToString());
                if (vComprobante.tipo_comp == 1)
                    txtNumSop.Text = vComprobante.num_consig;
                else
                    txtNumSop.Text = vComprobante.n_documento;

                Usuario usuap = (Usuario)Session["usuario"];
                txtElaboradoPor.Text = HttpUtility.HtmlDecode(vComprobante.nombres);
                txtCodElabora.Text = vComprobante.cod_elaboro.ToString();

                Comprobante pComprobante = ComprobanteServicio.ConsultarDatosElaboro(Convert.ToInt32(vComprobante.cod_elaboro), (Usuario)Session["Usuario"]);
                {
                    if (!string.IsNullOrEmpty(vComprobante.direccion))
                        tbxDireccion.Text = HttpUtility.HtmlDecode(pComprobante.direccion.ToString());
                    if (!string.IsNullOrEmpty(vComprobante.telefono))
                        tbxTelefono.Text = HttpUtility.HtmlDecode(pComprobante.telefono.ToString());

                }
                txtCodAprobo.Text = vComprobante.cod_aprobo.ToString();
                txtEstado.Text = vComprobante.estado;
                if (!string.IsNullOrEmpty(vComprobante.tipo_benef))
                    tipobeneficiario = vComprobante.tipo_benef.ToString();
                if (!string.IsNullOrEmpty(vComprobante.iden_benef))
                    txtIdentificacion.Text = HttpUtility.HtmlDecode(vComprobante.iden_benef.ToString().Trim());
                if (!string.IsNullOrEmpty(vComprobante.tipo_identificacion))
                    ddlTipoIdentificacion.SelectedValue = vComprobante.tipo_identificacion.ToString();
                txtNombres.Text = HttpUtility.HtmlDecode(vComprobante.nombre);
                tbxObservaciones.Text = HttpUtility.HtmlDecode(vComprobante.observaciones);

                Session["Ope_Recib"] = vComprobante.cod_ope;

                // Determinar el tipo de pago del comprobante
                if (vComprobante.tipo_pago != null)
                    try
                    {
                        ddlFormaPago.SelectedValue = vComprobante.tipo_pago.ToString();
                        ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                    }
                    catch { }
                //Agregado para determinar si es un comprobante automaticamente
                Session["cod_ope"] = vComprobante.cod_ope != 0 ? vComprobante.cod_ope : 0;
                // Determinar la entidad bancaria y la cuenta bancaria
                try
                {
                    if (vComprobante.entidad != null)
                    {
                        ddlEntidadOrigen.SelectedValue = vComprobante.entidad.ToString();
                        /*if (vComprobante.cod_ope != 0 && vComprobante.num_comp != 0 && vComprobante.tipo_comp != 0 )
                            ddlEntidadOrigen.Enabled = false;*/
                    }
                    ActivarDDLCuentas(false);
                    if (ddlTipoComprobante.SelectedValue == "5")
                    {
                        LlenarCuenta();
                        // Buscar los datos del giro y asignarlo al comprobante.
                        Xpinn.Tesoreria.Entities.Giro vGiro = new Xpinn.Tesoreria.Entities.Giro();
                        if (ComprobanteServicio.ConsultarGiroGeneral(Convert.ToInt64(pIdNComp), Convert.ToInt64(pIdTComp), ref vGiro, (Usuario)Session["usuario"]))
                        {
                            if (vGiro.forma_pago != 0 && vGiro.forma_pago != null)
                            {
                                ddlFormaPago.SelectedValue = vGiro.forma_pago.ToString();
                                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                                /*if (vComprobante.cod_ope != 0 && vComprobante.num_comp != 0 && vComprobante.tipo_comp != 0)
                                    ddlFormaPago.Enabled = false;*/
                            }

                            Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
                            Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
                            if (vGiro.idctabancaria != 0)
                            {
                                CuentaBanc = bancoService.ConsultarCuentasBancarias(Convert.ToInt32(vGiro.idctabancaria), (Usuario)Session["usuario"]);
                                if (CuentaBanc.cod_banco != 0)
                                {
                                    ddlEntidadOrigen.SelectedValue = CuentaBanc.cod_banco.ToString();
                                    ddlEntidadOrigen_SelectedIndexChanged(ddlEntidadOrigen, null);
                                    ddlCuenta.SelectedValue = vGiro.idctabancaria.ToString();

                                    /*if (vComprobante.cod_ope != 0 && vComprobante.num_comp != 0 && vComprobante.tipo_comp != 0)
                                    {
                                        ddlCuenta.Enabled = false;
                                        ddlEntidadOrigen.Enabled = false;
                                    }*/
                                }
                            }

                            if (vGiro.num_cuenta != null && vGiro.num_cuenta != "")
                            {
                                txtNum_cuenta.Text = vGiro.num_cuenta;
                                /*if (vComprobante.cod_ope != 0 && vComprobante.num_comp != 0 && vComprobante.tipo_comp != 0)
                                    txtNum_cuenta.Enabled = false;*/
                            }
                            if (vGiro.tipo_cuenta != 0)
                            {
                                ddlTipo_cuenta.SelectedValue = vGiro.tipo_cuenta.ToString();
                                /*if (vComprobante.cod_ope != 0 && vComprobante.num_comp != 0 && vComprobante.tipo_comp != 0)
                                    ddlTipo_cuenta.Enabled = false;*/
                            }
                        }
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    ActivarDDLCuentas(true);
                    if (vComprobante.entidad != null)
                        ddlEntidadOrigen.SelectedValue = vComprobante.entidad.ToString();
                }
                catch { }

                if (vComprobante.tipo_comp == 1)
                {
                    txtNumSop.Text = vComprobante.num_consig;
                }
                else
                {

                    ddlCuenta.SelectedValue = vComprobante.cuenta;
                    Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
                    txtNumSop.Text = vComprobante.n_documento;

                    // Se añadido Consulta de la ruta de la tabla Bancos segun el numero de cuenta
                    if (!String.IsNullOrEmpty(ddlCuenta.SelectedValue) || ddlCuenta.SelectedValue != "")
                        Session["Ruta_Cheque"] = BancosService.Ruta_Cheque(ddlCuenta.SelectedItem.Text, (Usuario)Session["Usuario"]);
                }
                // Asignar el número de cheque si no fue asignado
                if (Session["entidad"] != null && Session["cuenta"] != null)
                {
                    if (vComprobante.cuenta != null && vComprobante.cuenta != "")
                    {
                        if (Session["numerocheque"] == null)
                            AsignarNumeroCheque(vComprobante.cuenta);
                        else
                            if (txtNumSop.Text == "0" || txtNumSop.Text.Trim() == "")
                            txtNumSop.Text = Convert.ToString(Session["numerocheque"]);
                    }
                }

                // Mostrar datos del detalle
                if ((LstDetalleComprobante == null) || (LstDetalleComprobante.Count == 0))
                {
                    CrearDetalleInicial(0);
                }
                else
                {
                    Session["DetalleComprobante"] = LstDetalleComprobante;
                };

                gvDetMovs.DataSource = LstDetalleComprobante;
                gvDetMovs.DataBind();

                CalcularTotal();

                // Determinar el valor del giro cuando es comprobante de egreso
                DetalleComprobante var = new DetalleComprobante();
                if (ddlTipoComprobante.SelectedValue == "5")
                {
                    PanelFooter.Visible = true;
                    for (int i = 0; i < LstDetalleComprobante.Count; i++)
                    {
                        var = LstDetalleComprobante[i];
                        if (var.cod_cuenta != null)
                        {
                            string a = var.cod_cuenta.Substring(0, 2);
                            if (ComprobanteServicio.CuentaEsGiro(var.cod_cuenta, (Usuario)Session["Usuario"]))
                            {
                                lablerror0.Text = string.Format("{0:N2}", var.valor);
                                i = LstDetalleComprobante.Count + 1;
                            }
                        }
                    }
                }
                if (lablerror0.Text == "")
                    lablerror0.Text = "0";

                // Visualizar campos dependiendo del tipo de comprobante
                if (vComprobante.tipo_comp == 1) rbIngreso.Checked = true;
                if (vComprobante.tipo_comp == 5) rbEgreso.Checked = true;
                if (vComprobante.tipo_comp != 1 & vComprobante.tipo_comp != 5) rbContable.Checked = true;

                Activar();

                if (!string.IsNullOrEmpty(vComprobante.cheque_iden_benef))
                {
                    txtidenti.Text = vComprobante.cheque_iden_benef;
                    txtnom.Text = HttpUtility.HtmlDecode(vComprobante.cheque_nombre);
                    /*if (vComprobante.cod_ope != 0 && vComprobante.num_comp != 0 && vComprobante.tipo_comp != 0)
                    {
                        txtidenti.Enabled = false;
                        txtnom.Enabled = false;
                    }*/
                }
                else
                {
                    txtidenti.Text = txtIdentificacion.Text;
                    txtnom.Text = HttpUtility.HtmlDecode(txtNombres.Text);
                }
                ddlTipoIdentificacion0.SelectedValue = ddlTipoIdentificacion.SelectedValue;
                ddlTipoComprobante.SelectedValue = vComprobante.tipo_comp.ToString();

                Xpinn.Contabilidad.Services.PlanCuentasImpuestoService ImpuService = new Xpinn.Contabilidad.Services.PlanCuentasImpuestoService();
                List<PlanCuentasImpuesto> lstImpuesto = new List<PlanCuentasImpuesto>();
                PlanCuentasImpuesto pImpuesto = new PlanCuentasImpuesto();
                string filtro = " WHERE COD_CUENTA = '" + txtNum_cuenta.Text + "'";

                lstImpuesto = ImpuService.ListarPlanCuentasImpuesto(pImpuesto, filtro, (Usuario)Session["usuario"]);

                if (lstImpuesto.Count > 0)
                {
                    gvImpuestos.DataSource = lstImpuesto;
                    gvImpuestos.DataBind();
                }
                Session.Remove(ComprobanteServicio.CodigoPrograma + ".num_comp");
            }
            ddlTipoComprobante.SelectedValue = Convert.ToString(vComprobante.tipo_comp);
            Session.Remove(ComprobanteServicio.CodigoPrograma + ".num_comp");
            return;
            //Agregado para no permitir la modificación del encabezado del comprobante en caso de ser generado
            /*if (vComprobante.cod_ope != 0 && vComprobante.num_comp != 0 && vComprobante.tipo_comp != 0)
            {
                txtNumComp.Enabled = false;
                ddlTipoComprobante.Enabled = false;
                ddlCiudad.Enabled = false;
                ddlConcepto.Enabled = false;
                txtNumSop.Enabled = false;
                txtElaboradoPor.Enabled = false;
                txtCodElabora.Enabled = false;
                txtCodAprobo.Enabled = false;
                txtEstado.Enabled = false;
                txtIdentificacion.Enabled = false;
                txtNombres.Enabled = false;
                tbxObservaciones.Enabled = false;
                if (vComprobante.tipo_pago != null)
                    ddlFormaPago.Enabled = false;
                ddlTipoIdentificacion0.Enabled = false;
                ddlTipoIdentificacion.Enabled = false;
                ddlTipoComprobante.Enabled = false;
                gvImpuestos.Enabled = false;
                btnConsultaPersonas.Enabled = false;
            }*/

        }
        catch (Exception ex)
        {
            VerError("Error al consultar el comprobante No.:>" + pIdNComp + "< Tipo:>" + pIdTComp + "< " + ex.Message);
            //BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }


    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }

    /// <summary>
    /// Método para guardar los datos del comprobante
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        ObtenerDetalleComprobante(false);
        CalcularTotal();
        Session.Remove(ComprobanteServicio.CodigoPrograma + ".num_comp");

        // Si no se ingreso el número de soporte entonces dejarlo en cero
        if (txtNumSop.Text.Trim() == "")
            txtNumSop.Text = "0";
        // Validar y grabar datos
        try
        {
            string data = lblIdGiro.Text;
            if (ValidarDatosComprobante())
            {
                Lblerror.Text = "";
                lblMensajeGrabar.Text = "";

                Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
                List<Xpinn.Contabilidad.Entities.DetalleComprobante> vDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();

                Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
                vDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

                //VALIDAR EL MONTO A GIRAR
                DetalleComprobante var = new DetalleComprobante();
                if (ddlTipoComprobante.SelectedValue == "5")
                {
                    PanelFooter.Visible = true;
                    for (int i = 0; i < vDetalleComprobante.Count; i++)
                    {
                        var = vDetalleComprobante[i];
                        if (var.cod_cuenta != null)
                        {
                            string a = var.cod_cuenta.Substring(0, 2);
                            if (ComprobanteServicio.CuentaEsGiro(var.cod_cuenta, (Usuario)Session["Usuario"]))
                            {
                                lablerror0.Text = string.Format("{0:N2}", var.valor);
                                i = vDetalleComprobante.Count + 1;
                            }
                        }
                    }
                }

                // Determinar el número del comprobante
                if (txtNumComp.Text != "")
                    vComprobante.num_comp = Convert.ToInt64(txtNumComp.Text);

                // Determinar el tipo del comprobante
                vComprobante.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue);

                // Determinar el número de soporte del comprobante
                if (txtNumSop.Text != "")
                {
                    vComprobante.num_consig = txtNumSop.Text;
                    vComprobante.n_documento = txtNumSop.Text;
                }
                else
                {
                    vComprobante.num_consig = "0";
                    vComprobante.n_documento = "0";
                }

                // Determinar datos del comprobante
                vComprobante.fecha = DateTime.ParseExact(txtFecha.Text, gFormatoFecha, null);
                vComprobante.hora = System.DateTime.Now.ToLocalTime();
                vComprobante.ciudad = Convert.ToInt64(ddlCiudad.SelectedValue);
                vComprobante.concepto = Convert.ToInt64(ddlConcepto.SelectedValue);
                if (ddlFormaPago.SelectedValue != "")
                    vComprobante.tipo_pago = Convert.ToInt64(ddlFormaPago.SelectedValue);
                if (ddlEntidadOrigen.SelectedValue != "0")
                    vComprobante.entidad = Convert.ToInt64(ddlEntidadOrigen.SelectedValue);
                else
                    vComprobante.entidad = null;
                vComprobante.descripcion_concepto = ddlTipoComprobante.SelectedItem.Text;
                if (tbxTotalDebitos.Text != "")
                    vComprobante.totalcom = Convert.ToDecimal(tbxTotalDebitos.Text);
                vComprobante.tipo_benef = "A";

                // Validar datos de la persona
                Persona1 = Persona1Servicio.ConsultaDatosPersona(txtIdentificacion.Text, (Usuario)Session["usuario"]);
                if (Persona1.cod_persona == Int64.MinValue)
                {
                    Lblerror.Text = "La persona con identificaciòn no existe " + txtIdentificacion.Text;
                    return;
                }
                vComprobante.cod_benef = Persona1.cod_persona;
                vComprobante.iden_benef = txtIdentificacion.Text;
                vComprobante.tipo_identificacion = ddlTipoIdentificacion.SelectedItem.ToString();
                txtNombres.Text = HttpUtility.HtmlDecode(Persona1.nombre);
                vComprobante.nombre = HttpUtility.HtmlDecode(txtNombres.Text);
                if (txtCodElabora.Text == "")
                {
                    Usuario usuap = (Usuario)Session["usuario"];
                    txtCodElabora.Text = Convert.ToString(usuap.codusuario);
                }
                vComprobante.cod_elaboro = Convert.ToInt64(txtCodElabora.Text);
                if (txtCodAprobo.Text == "")
                {
                    Usuario usuap = (Usuario)Session["usuario"];
                    txtCodAprobo.Text = Convert.ToString(usuap.codusuario);
                }
                vComprobante.cod_aprobo = Convert.ToInt64(txtCodAprobo.Text);
                vComprobante.estado = txtEstado.Text;
                vComprobante.observaciones = tbxObservaciones.Text.Trim() != "" ? HttpUtility.HtmlDecode(tbxObservaciones.Text.Trim()) : null;
                vComprobante.cuenta = ddlCuenta.SelectedValue;

                // Determinar beneficiario del cheque
                vComprobante.cheque_iden_benef = txtidenti.Text;
                vComprobante.cheque_tipo_identificacion = ddlTipoIdentificacion0.SelectedValue;
                vComprobante.cheque_nombre = HttpUtility.HtmlDecode(txtnom.Text);

                bool creandoComprobante = false;
                if (idObjeto != "")
                {
                    ComprobanteServicio.ModificarComprobante(vDetalleComprobante, vComprobante, (Usuario)Session["Usuario"]);
                    creandoComprobante = false;
                }
                else
                {
                    vComprobante = ComprobanteServicio.CrearComprobante(vDetalleComprobante, vComprobante, (Usuario)Session["Usuario"]);
                    txtNumComp.Text = vComprobante.num_comp.ToString();
                    creandoComprobante = true;
                }

                PanelActiveView(vwPanel2);
                Site toolbar = (Site)Master;
                toolbar.MostrarGuardar(false);

                btnImpriOrden.Visible = false;
                if (Session["NumCred_Orden"] != null)
                {
                    btnImpriOrden.Visible = true;
                }

                lblMensajeGrabar.Text = "Comprobante " + txtNumComp.Text + " Grabado Correctamente";
                if(Session["solicitud"] != null)
                {
                    AhorroVista solicitud = Session["solicitud"] as AhorroVista;                                                            
                    solicitud.nom_estado = "1"; // aprueba solicitud
                    Session["solicitud"] = null;
                    AhorroVistaServices _ahorroService = new AhorroVistaServices();
                    _ahorroService.ModificarEstadoSolicitud(solicitud, (Usuario)Session["Usuario"]);
                }

                //Agregado para actualizar numero y tipo de comprobante en el recibo de caja menor si se hace un reintegro
                ActualizarSoportes(vComprobante);

                if ((String)Session["Comprobanteanulacion"] != null || Session["idNComp"] != null)
                {
                    // actualizar anulacion de comporbantes con el comporbante que anulo 
                    string idNComp = Session["idNComp"].ToString();
                    string idTComp = Session["idTComp"].ToString();
                    Xpinn.Contabilidad.Entities.Comprobante anularcomp = new Xpinn.Contabilidad.Entities.Comprobante();
                    anularcomp.fecha = DateTime.Now;
                    anularcomp.tipo_comp = Convert.ToInt32(idTComp);
                    anularcomp.tipo_motivo = 0;
                    anularcomp.num_comp = Convert.ToInt32(idNComp);

                    anularcomp.cod_persona = 0;
                    anularcomp.idanulaicon = -1;
                    anularcomp.num_comp_anula = Convert.ToInt32(txtNumComp.Text);
                    anularcomp.tipo_comp_anula = Convert.ToInt32(ddlTipoComprobante.SelectedValue);
                    ComprobanteServicio.crearanulacioncomprobante(vDetalleComprobante, anularcomp, (Usuario)Session["Usuario"]);
                }


                #region Interactuar WorkManagement


                General parametroHabilitaOperacionesWM = ConsultarParametroGeneral(35);
                if (creandoComprobante && parametroHabilitaOperacionesWM != null && parametroHabilitaOperacionesWM.valor.Trim() == "1")
                {
                    // 0 = Contable, 5 = Egreso
                    string tipoComprobante = Request.QueryString["tipo_comp"];

                    if (!string.IsNullOrWhiteSpace(tipoComprobante))
                    {
                        tipoComprobante = tipoComprobante.ToString().Trim();

                        if (tipoComprobante == "0") // Contable
                        {
                            // Se verifica si esta visible
                            if (txtNumeroFactura.Visible)
                            {
                                InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);
                                string numeroFactura = txtNumeroFactura.Text.Trim();
                                string radicado = interfaz.ConsultarRadicadoPorNumeroFacturaDeFormularioFacturasRecibidas(numeroFactura);

                                if (!string.IsNullOrWhiteSpace(radicado))
                                {
                                    RpviewComprobante.Visible = true;
                                    GeneraReporteComprobantePago(null);
                                    byte[] bytesReporte = ObtenerBytesReporteAsPDF(RpviewComprobante, FormatoArchivo.PDF);
                                    RpviewComprobante.Visible = false;

                                    string descripcion = "Comprobante de Causacion de Factura No. " + numeroFactura;
                                    WorkFlowFilesDTO file = new WorkFlowFilesDTO
                                    {
                                        Base64DataFile = Convert.ToBase64String(bytesReporte),
                                        Descripcion = descripcion,
                                        Extension = ".pdf"
                                    };

                                    bool anexoExitoso = interfaz.AnexarArchivoAFormularioPagoProveedores(file, radicado, TipoArchivoWorkManagement.ComprobanteCausacionPagoProveedores);

                                    if (anexoExitoso)
                                    {
                                        string comentario = "Carga de Comprobante de Causacion de Factura No. " + numeroFactura;

                                        // RunTask, corre al siguiente proceso, debes identificar en el proceso que estas y añadir las observaciones
                                        interfaz.RunTaskWorkFlowPagoProveedores(radicado, StepsWorkManagementWorkFlowPagoProveedores.Causacion, comentario);

                                        WorkManagementServices workManagementServices = new WorkManagementServices();
                                        WorkFlowPagoProveedores workFlowPagoProveedores = new WorkFlowPagoProveedores
                                        {
                                            barcoderadicado = radicado,
                                            estado = 0,
                                            numerofactura = numeroFactura,
                                            tipocomprobante = Convert.ToInt64(ddlTipoComprobante.SelectedValue),
                                            codigobeneficiario = vComprobante.cod_benef,
                                            numerocomprobante = vComprobante.num_comp
                                        };
                                        workFlowPagoProveedores = workManagementServices.CrearWorkFlowPagoProveedores(workFlowPagoProveedores, Usuario);
                                    }
                                }
                            }
                        }
                        else if (tipoComprobante == "5") // Egreso
                        {
                            // Se verifica que este visible
                            if (ddlNumeroFacturas.Visible)
                            {
                                string numeroFactura = ddlNumeroFacturas.SelectedValue;

                                if (!string.IsNullOrWhiteSpace(numeroFactura))
                                {
                                    numeroFactura = numeroFactura.Trim();

                                    InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);
                                    string radicado = interfaz.ConsultarRadicadoPorNumeroFacturaDeFormularioFacturasRecibidas(numeroFactura);

                                    if (!string.IsNullOrWhiteSpace(radicado))
                                    {
                                        RpviewComprobante.Visible = true;
                                        GeneraReporteComprobantePago(null);
                                        byte[] bytesReporte = ObtenerBytesReporteAsPDF(RpviewComprobante, FormatoArchivo.PDF);
                                        RpviewComprobante.Visible = false;

                                        string descripcion = "Comprobante de Egreso de Factura No. " + numeroFactura;
                                        WorkFlowFilesDTO file = new WorkFlowFilesDTO
                                        {
                                            Base64DataFile = Convert.ToBase64String(bytesReporte),
                                            Descripcion = descripcion,
                                            Extension = ".pdf"
                                        };
                                        bool anexoExitoso = interfaz.AnexarArchivoAFormularioPagoProveedores(file, radicado, TipoArchivoWorkManagement.ComprobanteEgresoPagoProveedores);

                                        if (anexoExitoso)
                                        {
                                            string comentario = "Carga de Comprobante de Egreso de Factura No. " + numeroFactura;

                                            // RunTask, corre al siguiente proceso, debes identificar en el proceso que estas y añadir las observaciones
                                            bool runTaskExitoso = interfaz.RunTaskWorkFlowPagoProveedores(radicado, StepsWorkManagementWorkFlowPagoProveedores.Pago, comentario);

                                            if (runTaskExitoso)
                                            {
                                                WorkManagementServices workManagementServices = new WorkManagementServices();
                                                bool exitoso = workManagementServices.MarcarWorkFlowPagoProveedoresComoPagado(radicado, Usuario);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                #endregion


                Session[ComprobanteServicio.CodigoPrograma + ".num_comp"] = idObjeto;
                Session.Remove(ComprobanteServicio.CodigoPrograma + ".num_comp");
                if (Session["numerocheque"] != null)
                    Session.Remove("numerocheque");
                if (Session["entidad"] != null)
                    Session.Remove("entidad");
                if (Session["cuenta"] != null)
                    Session.Remove("cuenta");
                if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] != null)
                    Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_ope");
                if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
                    Session.Remove(ComprobanteServicio.CodigoPrograma + ".tipo_ope");
                if (Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] != null)
                    Session.Remove(ComprobanteServicio.CodigoPrograma + ".fecha_ope");
                if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] != null)
                    Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope");
                if (Session[ComprobanteServicio.CodigoPrograma + ".idgiro"] != null)
                    Session.Remove(ComprobanteServicio.CodigoPrograma + ".idgiro");
                if (Session["Comprobantecopia"] != null)
                    Session.Remove("Comprobantecopia");
                ViewState.Remove("DataAtribuciones");
            }
            else
            {
                string script = "OcultaLoading();";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
                lablerror.Visible = true;
            }

        }
        //catch (ExceptionBusiness ex)
        //{
        //    VerError("btnGuardar_Click: " + ex.Message);
        //}
        catch (Exception ex)
        {
            //BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "btnGuardar_Click", ex);
            VerError("Guardar: " + ex.Message);
        }
    }

    /// <summary>
    /// Método para cancelar el proceso que se esta realizando
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["DetalleComprobante"] != null)
            Session.Remove("DetalleComprobante");
        if (Session["cod_ope"] != null)
            Session.Remove("cod_ope");
        if (Session["tipo_ope"] != null)
            Session.Remove("tipo_ope");
        if (Session["cod_persona"] != null)
            Session.Remove("cod_persona");
        if (Session["numerocheque"] != null)
            Session.Remove("numerocheque");
        if (Session["entidad"] != null)
            Session.Remove("entidad");
        if (Session["cuenta"] != null)
            Session.Remove("cuenta");
        if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
            Session.Remove(ComprobanteServicio.CodigoPrograma + ".tipo_ope");
        if (Session["idNComp"] != null)
            Session.Remove("idNComp");
        if (Session["idTComp"] != null)
            Session.Remove("idTComp");
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_traslado"] != null)
            Session.Remove(ComprobanteServicio.CodigoPrograma + ".cod_traslado");
        if (Session["Modificar"] == "1")
            Response.Redirect("~/Page/Contabilidad/ModificacionComprobantes/Lista.aspx");
        else
            Navegar(Pagina.Lista);
    }

    /// <summary>
    /// Método para instar un detalle en blanco para cuando la grilla no tiene datos
    /// </summary>
    /// <param name="consecutivo"></param>
    private void CrearDetalleInicial(int consecutivo)
    {
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        for (int i = 1; i <= 2; i++)
        {
            DetalleComprobante pDetalleComprobante = new DetalleComprobante();
            pDetalleComprobante.codigo = -1;
            if (txtNumComp.Text != "")
                pDetalleComprobante.num_comp = Convert.ToInt64(txtNumComp.Text);
            if (ddlTipoComprobante.SelectedValue != null)
                pDetalleComprobante.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue.ToString());
            pDetalleComprobante.cod_cuenta = "";
            pDetalleComprobante.nombre_cuenta = "";
            pDetalleComprobante.centro_costo = null;
            pDetalleComprobante.centro_gestion = null;
            pDetalleComprobante.valor = null;
            pDetalleComprobante.tercero = null;
            pDetalleComprobante.moneda = 1;
            pDetalleComprobante.tipo = "D";
            pDetalleComprobante.base_comp = null;
            pDetalleComprobante.porcentaje = null;

            LstDetalleComprobante.Add(pDetalleComprobante);
        }
        gvDetMovs.DataSource = LstDetalleComprobante;
        gvDetMovs.DataBind();

        Session["DetalleComprobante"] = LstDetalleComprobante;

    }


    // == EVENTOS DE LA GRILA ==========================================================================================

    #region Detalle Comprobante

    /// <summary>
    /// Método para cambio de página
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetMovs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            ObtenerDetalleCompOptimizado(false);
            //ObtenerDetalleComprobante(false);
            gvDetMovs.PageIndex = e.NewPageIndex;
            ActualizarDetalle();
            return;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ComprobanteServicio.CodigoPrograma, "gvDetMovs_PageIndexChanging", ex);
        }
    }

    protected void ActualizarDetalle()
    {
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
        gvDetMovs.DataSource = LstDetalleComprobante;
        gvDetMovs.DataBind();
    }


    /// <summary>
    /// Método para borrar un registro de la grilla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDetMovs_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDetMovs.DataKeys[e.RowIndex].Values[0].ToString());

        if (conseID != 0)
        {
            try
            {
                List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
                LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

                LstDetalleComprobante.RemoveAt((gvDetMovs.PageIndex * gvDetMovs.PageSize) + e.RowIndex);
                Session["DetalleComprobante"] = LstDetalleComprobante;

                gvDetMovs.DataSourceID = null;
                gvDetMovs.DataBind();
                gvDetMovs.DataSource = LstDetalleComprobante;
                gvDetMovs.DataBind();

                CalcularTotal();
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(this.ComprobanteServicio.numero_comp + "L", "gvDetMovs_RowDeleting", ex);
            }
        }
        else
        {
            e.Cancel = true;
        }

        return;
    }
    protected void gvDetMovs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            Label _TotalPags = (Label)e.Row.FindControl("lblTotalNumberOfPages");
            _TotalPags.Text = gvDetMovs.PageCount.ToString();
            TextBox _IraPag = (TextBox)e.Row.FindControl("IraPag");
            _IraPag.Text = (gvDetMovs.PageIndex + 1).ToString();
            DropDownList _DropDownList = (DropDownList)e.Row.FindControl("RegsPag");
            _DropDownList.SelectedValue = gvDetMovs.PageSize.ToString();

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //e.Row.Attributes.Add("OnMouseOut", "this.className = this.orignalclassName;");
            //e.Row.Attributes.Add("OnMouseOver", "this.orignalclassName = this.className;this.className = 'altoverow';");
            //TextBox txtValor = (TextBox)e.Row.FindControl("txtValor");
            //if (txtValor != null)
            //    txtValor.eventoCambiar += txtValor_TextChangeds;
            TextBoxGrid txtCodCuenta = (TextBoxGrid)e.Row.FindControl("txtCodCuenta");
            if (txtCodCuenta != null)
            {
                Label lblImpuesto = (Label)e.Row.FindControl("lblImpuesto");
                Label lblBase = (Label)e.Row.FindControl("lblBase");
                Label lblPorcentaje = (Label)e.Row.FindControl("lblPorcentaje");
                TextBox txtBaseGrid = (TextBox)e.Row.FindControl("txtBaseGrid");
                TextBox txtPorcentGrid = (TextBox)e.Row.FindControl("txtPorcentGrid");
                Label lblManejaTer = (Label)e.Row.FindControl("lblManejaTer");
                Label lblTercero = (Label)e.Row.FindControl("lblTercero");
                TextBox txtIdentificD = (TextBox)e.Row.FindControl("txtIdentificD");
                Label lblNombreTercero = (Label)e.Row.FindControl("lblNombreTercero");
                ButtonGrid btnGenerarImpuesto = (ButtonGrid)e.Row.FindControl("btnGenerarImpuesto");
                TextBoxGrid txtCodCuentaNIF = (TextBoxGrid)e.Row.FindControl("txtCodCuentaNIF");
                ButtonGrid btnListadoPlanNIF = (ButtonGrid)e.Row.FindControl("btnListadoPlanNIF");
                ButtonGrid btnListadoPlan = (ButtonGrid)e.Row.FindControl("btnListadoPlan");

                if (txtCodCuenta.Text == null || txtCodCuenta.Text.Trim() == "")
                {
                    if (txtIdentificD != null)
                    {
                        lblTercero.Visible = false;
                        txtIdentificD.Visible = false;
                        lblNombreTercero.Visible = false;
                    }
                }
                else
                {
                    //Se agregó validación para no permitir modificar comprobantes generados
                    Int64 valor = Convert.ToInt64(Session["cod_ope"]);
                    if (((string)Session["Modificar"] == "1" && valor != 0) || valor != 0 || ValidarComCierre())
                    {
                        int validacion = ComprobanteServicio.ValidarCuentaContable(0, txtCodCuenta.Text, (Usuario)Session["usuario"]);
                        if (validacion == 1)
                            e.Row.Enabled = false;
                    }
                    if (lblManejaTer.Text != "1")
                    {
                        lblTercero.Visible = false;
                        txtIdentificD.Visible = false;
                        lblNombreTercero.Visible = false;
                    }
                    else
                    {
                        lblTercero.Visible = true;
                        txtIdentificD.Visible = true;
                        lblNombreTercero.Visible = true;
                    }
                    if (lblImpuesto.Text == "1")
                    {
                        btnGenerarImpuesto.Visible = true;
                    }
                    else
                    {
                        btnGenerarImpuesto.Visible = false;
                    }

                    Par_Cue_LinCred entidad = new Par_Cue_LinCred();
                    Par_Cue_LinCred pCuentasLinCred = new Par_Cue_LinCred();
                    entidad.tipo_cuenta = 1;
                    string pFiltro = " AND COD_CUENTA = '" + txtCodCuenta.Text.Trim() + "'";
                    //entidad.cod_cuenta = txtCodCuenta.Text;

                    pCuentasLinCred = servicePar_cue_lin.ConsultarPar_Cue_LinCred2(entidad, pFiltro, (Usuario)Session["usuario"]);


                    if (pCuentasLinCred.idparametro == 0)
                    {
                        pCuentasLinCred = servicePar_cue_lin.ConsultarPAR_CUE_LINAPO(entidad, pFiltro, (Usuario)Session["usuario"]);
                    }

                    if (pCuentasLinCred.idparametro == 0)
                    {
                        pFiltro = " WHERE COD_CUENTA = '" + txtCodCuenta.Text.Trim() + "'";
                        pCuentasLinCred = servicePar_cue_lin.ConsultarPAR_CUE_OTROS(entidad, pFiltro, (Usuario)Session["usuario"]);
                    }

                    if (pCuentasLinCred.idparametro != 0)
                    {
                        Usuario usu = (Usuario)Session["Usuario"];
                        if (usu.codperfil != 1)
                        {
                            List<int> lstAtribuciones = new List<int>();
                            if (ViewState["DataAtribuciones"] != null)
                                lstAtribuciones = (List<int>)ViewState["DataAtribuciones"];
                            else
                                lstAtribuciones = AtribucionesUsuario(usu);
                            if (lstAtribuciones == null)
                            {
                                e.Row.Enabled = false;
                            }
                            else
                            { 
                                if (lstAtribuciones.Count > 0)
                                {
                                    if (lstAtribuciones[5] == 0)
                                        e.Row.Enabled = false;
                                }
                                else
                                { 
                                    e.Row.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        //return;
    }

    #endregion

    /// <summary>
    /// Método para aceptar cuando se esta haciendo un nuevo comprobante y se seleccionó el tipo de comprobante
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
    {
        PanelActiveView(vwPanel1);
        Activar();
        ActivarDDLCuentas(false);
        VerError("");
    }

    protected void Activar()
    {
        if (rbIngreso.Checked == true)
        {
            lblComprobante.Text = "Ingreso No.";
            lblTipoComp.Visible = false;
            ddlTipoComprobante.Visible = false;
            ddlTipoComprobante.SelectedValue = "1";
            lblFormaPago.Visible = true;
            ddlFormaPago.Visible = true;
            txtNumSop.Visible = true;
            lblSoporte.Visible = true;
            ActivarBancos(true);
        }
        else
        {
            if (rbEgreso.Checked == true)
            {
                lblComprobante.Text = "Egreso No.";
                lblTipoComp.Visible = false;
                ddlTipoComprobante.Visible = false;
                ddlTipoComprobante.SelectedValue = "5";
                ddlFormaPago.Visible = true;
                lblFormaPago.Visible = true;
                txtNumSop.Visible = true;
                lblSoporte.Visible = true;
                ActivarBancos(true);
            }
            else
            {
                Xpinn.Contabilidad.Services.TipoComprobanteService TipoComprobanteService = new Xpinn.Contabilidad.Services.TipoComprobanteService();
                Xpinn.Contabilidad.Entities.TipoComprobante TipoComprobante = new Xpinn.Contabilidad.Entities.TipoComprobante();

                lblComprobante.Text = "Comprobante No.";
                string seleccionado = ddlTipoComprobante.SelectedValue.ToString();
                List<TipoComprobante> lsttipocomp = new List<TipoComprobante>();
                lsttipocomp = TipoComprobanteService.ListarTipoComprobante(TipoComprobante, "tipo_comp Not In (1, 5, BuscarGeneral(4200, 2))", (Usuario)Session["Usuario"]);
                try
                {
                    ddlTipoComprobante.DataSource = lsttipocomp;
                }
                catch { }

                try
                {
                    ddlTipoComprobante.DataTextField = "descripcion";
                    ddlTipoComprobante.DataValueField = "tipo_comprobante";
                    ddlTipoComprobante.DataBind();
                    ddlTipoComprobante.SelectedValue = "2";
                }
                catch
                {

                    TipoComprobante tipocomp = new TipoComprobante();
                    tipocomp.tipo_comprobante = Convert.ToInt32(seleccionado);
                    Xpinn.Contabilidad.Services.TipoComprobanteService tipocompServicio = new Xpinn.Contabilidad.Services.TipoComprobanteService();
                    tipocomp = tipocompServicio.ConsultarTipoComprobante(Convert.ToInt32(tipocomp.tipo_comprobante), (Usuario)Session["Usuario"]);
                    lsttipocomp.Add(tipocomp);
                    ddlTipoComprobante.DataBind();
                }

                ddlFormaPago.Visible = false;
                lblFormaPago.Visible = false;
                txtNumSop.Visible = false;
                lblSoporte.Visible = false;
                ActivarBancos(false);
            }
        }
    }


    /// <summary>
    /// Evento para cuando se cancela la acción de modificación del detalle
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCloseReg_Click(object sender, EventArgs e)
    {
        Session.Remove(ComprobanteServicio.CodigoPrograma + ".indice");
    }

    static public DataControlFieldCell GetCellByName(GridViewRow Row, String CellName)
    {
        foreach (DataControlFieldCell Cell in Row.Cells)
        {
            if (Cell.ContainingField.ToString().ToUpper() == CellName.ToUpper())
                return Cell;
        }
        return null;
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if (Session[ComprobanteServicio.CodigoPrograma + ".ventanilla"] != null)
        {
            if (Session[ComprobanteServicio.CodigoPrograma + ".ventanilla"] == "1")
            {
                Session.Remove(ComprobanteServicio.CodigoPrograma + ".ventanilla");
                Response.Redirect("~/Page/Tesoreria/PagosVentanilla/nuevo.aspx", false);
                return;
            }
        }
        if (Session[ComprobanteServicio.CodigoPrograma + ".generado"] != null)
        {
            if (Session[ComprobanteServicio.CodigoPrograma + ".generado"] != "")
            {
                Response.Redirect(Session[ComprobanteServicio.CodigoPrograma + ".generado"].ToString(), false);
                Session.Remove(ComprobanteServicio.CodigoPrograma + ".generado");
                return;
            }
        }
        if (Session[ComprobanteServicio.CodigoPrograma + ".modificar"] != null)
            if (Session[ComprobanteServicio.CodigoPrograma + ".modificar"] == "1")
            {
                Session.Remove(ComprobanteServicio.CodigoPrograma + ".modificar");
                Response.Redirect("../ModificacionComprobantes/Lista.aspx", false);
                return;
            }
            else
            {
                Response.Redirect(Session[ComprobanteServicio.CodigoPrograma + ".modificar"].ToString(), false);
                Session.Remove(ComprobanteServicio.CodigoPrograma + ".modificar");
                return;
            }

        Navegar(Pagina.Lista);


    }

    public void CalcularTotal()
    {
        decimal? totdeb = 0.00m;
        decimal? totcre = 0.00m;
        decimal? diferencia = 0.00m;
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        if (Session["DetalleComprobante"] != null)
        {
            LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
            for (int i = 0; i < LstDetalleComprobante.Count; i++)
            {
                if (LstDetalleComprobante[i].valor != null)
                {
                    if (LstDetalleComprobante[i].tipo == "D" | LstDetalleComprobante[i].tipo == "d")
                        totdeb = totdeb + LstDetalleComprobante[i].valor;
                    else
                        totcre = totcre + LstDetalleComprobante[i].valor;
                }
            }
            diferencia = totdeb - totcre;
            string sDeb = String.Format("{0:N2}", totdeb);
            tbxTotalDebitos.Text = sDeb;
            string sCre = String.Format("{0:N2}", totcre);
            tbxTotalCreditos.Text = sCre;
            string sDif = String.Format("{0:N2}", diferencia);
            tbxDiferencia.Text = sDif;
            return;
        }
    }

    public Boolean ValidarDatosComprobante()
    {
        Lblerror.Text = "";
        Xpinn.Contabilidad.Entities.PlanCuentas ePlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
        Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();


        #region Validacion WorkManagement


        // Si esta visible significa que la necesito para el WorkManagement
        if (txtNumeroFactura.Visible)
        {
            if (string.IsNullOrWhiteSpace(txtNumeroFactura.Text))
            {
                VerError("Debes digitar el numero de factura!.");
                return false;
            }
        }


        #endregion


        // Validar la fecha del comprobante
        if (txtFecha.ToString() != "")
        {
            string TipoComp = string.Empty;
            if (cbNif.Checked)
                TipoComp = "'G'";
            else if (cbLocal.Checked)
                TipoComp = "'C'";
            string pFec = ComprobanteServicio.Consultafecha((Usuario)Session["Usuario"], TipoComp);
            if (string.IsNullOrWhiteSpace(pFec))
            {
                if (TipoComp == "'G'")
                    VerError("No existe un cierre definitivo para Niif");
                else
                    VerError("No existe un cierre definitivo Local");
                return false;
            }
            DateTime dtUltCierre = Convert.ToDateTime(pFec);
            DateTime fechaComp = new DateTime();
            fechaComp = Convert.ToDateTime(txtFecha.Text);
            if (fechaComp < dtUltCierre)
            {
                VerError("No puede modificar comprobantes en períodos ya cerrados. Fecha Ultimo Cierre:" + dtUltCierre.ToShortDateString());
                return false;
            }
        }

        // Validar el banco
        if (ddlTipoComprobante.SelectedValue != "")
            if (ddlTipoComprobante.SelectedValue == "5")
                if (ddlEntidadOrigen.SelectedValue == "0" && ddlFormaPago.SelectedValue == "2")
                {
                    VerError("Debe seleccionar el banco");
                    return false;
                }

        if (txtIdentificacion.Text == "")
        {
            VerError("Debe ingresar la identificación del Beneficiario.");
            txtIdentificacion.Focus();
            return false;
        }
        else
        {
            if (txtNombres.Text == "")
            {
                VerError("La identificación ingresada no es válida, verifique los datos.");
                txtIdentificacion.Focus();
                return false;
            }
        }

        // Determinar la cuenta contable asignada al banco
        string cod_cuenta = "";
        try
        {
            cod_cuenta = ComprobanteServicio.ConsultarCuenta(Convert.ToInt64(ddlEntidadOrigen.SelectedValue), ddlCuenta.SelectedItem.Text, (Usuario)Session["Usuario"]);
        }
        catch { }

        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(560, (Usuario)Session["usuario"]);
        Int16 Validacuadrecentrodecostos = Convert.ToInt16(pData.valor);



        // Cargar el detalle del comprobante
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        if (ObtenerDetalleCompOptimizado(true) == false)
            return false;
        LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

        // Validar el detalle del comprobante
        decimal? totdeb = 0.00m;
        decimal? totcre = 0.00m;
        decimal? diferencia = 0.00m;

        // VALIDAR VALORES DEL COMPROBANTE POR CENTRO DE COSTO
        decimal? CCtotdeb = 0.00m;
        decimal? CCtotcre = 0.00m;
        decimal? CCdiferencia = 0.00m;

        var lstDatosComprobantes = from c in LstDetalleComprobante
                                   select new
                                   {
                                       c.codigo,
                                       c.num_comp,
                                       c.cod_cuenta,
                                       c.nombre_cuenta,
                                       c.cod_cuenta_niif,
                                       c.nombre_cuenta_nif,
                                       c.centro_costo,
                                       c.centro_gestion,
                                       c.detalle,
                                       c.tipo,
                                       c.valor,
                                       c.moneda,
                                       c.porcentaje,
                                       c.base_comp
                                   };
        lstDatosComprobantes = lstDatosComprobantes.OrderBy(c => c.centro_costo).ToList();
        int valorCCosto = 0, cCostoAnterior = 0, cont = 0;

        foreach (var rFilaDeta in lstDatosComprobantes)
        {
            if (rFilaDeta.valor != null && rFilaDeta.centro_costo != null)
            {
                valorCCosto = Convert.ToInt32(rFilaDeta.centro_costo);
                if (cont == 0)
                    cCostoAnterior = valorCCosto;
                if (cCostoAnterior == valorCCosto)
                {
                    if (rFilaDeta.tipo == "D" | rFilaDeta.tipo == "d")
                        CCtotdeb += rFilaDeta.valor;
                    else
                        CCtotcre += rFilaDeta.valor;
                }
                else
                {
                    //VALIDA SI ESTA CUADRADO
                    CCdiferencia = CCtotdeb - CCtotcre;
                    if (CCdiferencia < 0)
                        CCdiferencia = CCdiferencia * -1;
                    if (CCtotdeb != CCtotcre && Validacuadrecentrodecostos==1)
                    {
                        Lblerror.Text = "No hay sumas iguales en el comprobante para el centro de costo " + cCostoAnterior + ", hay una diferencia de " + String.Format("{0:N2}", CCdiferencia);
                        return false;
                    }
                    //LIMPIA LOS VALORES PARA CAPTURAR EL SIGUIENTE CENTRO DE COSTO A VALIDAR
                    valorCCosto = 0; cCostoAnterior = 0; cont = 0;
                    valorCCosto = Convert.ToInt32(rFilaDeta.centro_costo);
                    if (cont == 0)
                        cCostoAnterior = valorCCosto;
                    if (cCostoAnterior == valorCCosto)
                    {
                        if (rFilaDeta.tipo == "D" | rFilaDeta.tipo == "d")
                            CCtotdeb += rFilaDeta.valor;
                        else
                            CCtotcre += rFilaDeta.valor;
                    }
                }
                cCostoAnterior = valorCCosto;
            }
            else
            {
                Lblerror.Text = "El valor en la cuenta " + rFilaDeta.cod_cuenta + " no puede ser nulo";
                return false;
            }
            cont++;
        }

        CCdiferencia = CCtotdeb - CCtotcre;
        if (CCdiferencia < 0)
            CCdiferencia = CCdiferencia * -1;
        if (CCtotdeb != CCtotcre)
        {
            Lblerror.Text = "No hay sumas iguales en el comprobante para el centro de costo " + cCostoAnterior + ", hay una diferencia de " + String.Format("{0:N2}", CCdiferencia);
            return false;
        }

        Usuario pUsu = new Usuario();
        pUsu = (Usuario)Session["Usuario"];

        // Validar cada registro del detalle
        for (int i = 0; i < LstDetalleComprobante.Count; i++)
        {
            // Sumar valor debitos y creditos
            try
            {
                if (LstDetalleComprobante[i].valor != null)
                {
                    if (LstDetalleComprobante[i].tipo == "D" | LstDetalleComprobante[i].tipo == "d")
                        totdeb = totdeb + LstDetalleComprobante[i].valor;
                    else
                        totcre = totcre + LstDetalleComprobante[i].valor;
                }
                else
                {
                    Lblerror.Text = "El valor en la cuenta " + LstDetalleComprobante[i].cod_cuenta + " no puede ser nulo";
                    return false;
                }
            }
            catch (Exception ex)
            {
                VerError("Error al realizar conversión. (" + ex.Message + ") ");
                return false;
            }
            try
            {
                if (cbLocal.Checked)
                {
                    // Verificar que la cuenta sea auxiliar
                    if (ComprobanteServicio.CuentaEsAuxiliar(LstDetalleComprobante[i].cod_cuenta, (Usuario)Session["Usuario"]) == "")
                    {
                        Lblerror.Text = "La cuenta contable " + LstDetalleComprobante[i].cod_cuenta + " no es auxiliar o no existe";
                        return false;
                    }
                }
                else if (cbNif.Checked)
                {
                    // Verificar que la cuenta niif sea auxiliar
                    if (ComprobanteServicio.CuentaNIFEsAuxiliar(LstDetalleComprobante[i].cod_cuenta_niif, (Usuario)Session["Usuario"]) == "")
                    {
                        Lblerror.Text = "La cuenta contable " + LstDetalleComprobante[i].cod_cuenta_niif + " no es auxiliar o no existe";
                        return false;
                    }
                }
                else
                {  // Verificar que la cuenta sea auxiliar
                    if (ComprobanteServicio.CuentaEsAuxiliar(LstDetalleComprobante[i].cod_cuenta, (Usuario)Session["Usuario"]) == "")
                    {
                        Lblerror.Text = "La cuenta contable " + LstDetalleComprobante[i].cod_cuenta + " no es auxiliar o no existe";
                        return false;
                    }
                    // Verificar que la cuenta niif sea auxiliar
                    if (ComprobanteServicio.CuentaNIFEsAuxiliar(LstDetalleComprobante[i].cod_cuenta_niif, (Usuario)Session["Usuario"]) == "")
                    {
                        Lblerror.Text = "La cuenta contable NIIF: " + LstDetalleComprobante[i].cod_cuenta_niif + ", no es auxiliar o no existe. Fila: " + i.ToString() + " Código de cuenta local:" + LstDetalleComprobante[i].cod_cuenta;
                        return false;
                    }
                }


                // Verficar que la cuenta corresponda al banco
                if (ddlTipoComprobante.SelectedValue == "5" && cod_cuenta.Trim() != "")
                {
                    if (LstDetalleComprobante[i].cod_cuenta.Substring(0, 6) == cod_cuenta.Substring(0, 6))
                    {
                        if (LstDetalleComprobante[i].cod_cuenta != cod_cuenta)
                        {
                            Lblerror.Text = "La cuenta contable " + LstDetalleComprobante[i].cod_cuenta + " no corresponde con la cuenta del banco. (" + cod_cuenta + ")";
                            return false;
                        }
                    }
                }

                // Verificar datos del tercero
                ePlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(LstDetalleComprobante[i].cod_cuenta, (Usuario)Session["Usuario"]);
                if (ePlanCuentas.maneja_ter == 1)
                {
                    if (LstDetalleComprobante[i].tercero == null || LstDetalleComprobante[i].tercero == -1)
                    {
                        Lblerror.Text = "Debe ingresar el tercero para la cuenta " + LstDetalleComprobante[i].cod_cuenta + " fila " + i;
                        return false;
                    }
                    Persona1 = Persona1Servicio.ConsultarPersona1(Convert.ToInt64(LstDetalleComprobante[i].tercero), (Usuario)Session["usuario"]);
                    if (Persona1.cod_persona == Int64.MinValue)
                    {
                        Lblerror.Text = "Para la cuenta contable " + LstDetalleComprobante[i].cod_cuenta + " el tercero " + LstDetalleComprobante[i].tercero + " no existe";
                        return false;
                    }
                    else
                    {
                        int rowIndex = i - (gvDetMovs.PageIndex * gvDetMovs.PageSize);
                        if (rowIndex > 0 && rowIndex < gvDetMovs.Rows.Count)
                        {
                            Label lblTercero = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblTercero");
                            if (lblTercero != null)
                            {
                                if (Persona1.cod_persona != Int64.MinValue)
                                    lblTercero.Text = Convert.ToString(Persona1.cod_persona);
                                else
                                    lblTercero.Text = "";
                            }
                            Label lblNombreTercero = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblNombreTercero");
                            if (lblNombreTercero != null)
                            {
                                if (Persona1.cod_persona != Int64.MinValue)
                                    lblNombreTercero.Text = Persona1.nombre;
                                else
                                    lblNombreTercero.Text = "";
                            }
                        }
                    }
                }

                // Validar la Base
                if (ePlanCuentas.impuesto == 1)
                {
                    if (LstDetalleComprobante[i].porcentaje != null && LstDetalleComprobante[i].porcentaje != 0)
                    {
                        if (LstDetalleComprobante[i].base_comp != null)
                        {
                            decimal Baseminima;
                            Baseminima = Convert.ToDecimal(ePlanCuentas.base_minima);
                            if (Baseminima > LstDetalleComprobante[i].base_comp)
                            {
                                Lblerror.Text = "Debe Ingresar una base mayor a la base minima";
                                return false;
                            }
                        }
                        else
                        {
                            Lblerror.Text = "Debe Ingresar una base para la cuenta " + LstDetalleComprobante[i].cod_cuenta;
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                VerError("ValidarDatosComprobante: " + ex.Message + " fila " + i);
                return false;
            }

        }


        // Validar sumas iguales en el comprobante
        diferencia = totdeb - totcre;
        if (totdeb != totcre)
        {
            Lblerror.Text = "No hay sumas iguales en el comprobante, hay una diferencia de " + String.Format("{0:N2}", diferencia);
            return false;
        }

        return true;
    }

    //Validar comprobantes generados sin operación
    public bool ValidarComCierre()
    {
        Xpinn.Comun.Services.CiereaService CiereaServicio = new Xpinn.Comun.Services.CiereaService();
        Xpinn.Comun.Entities.Cierea pCierea = null;
        List<Cierea> lstCierre = new List<Cierea>();
        string[] cierres = new string[4];
        cierres[0] = "U"; //Causación
        cierres[1] = "R"; //Cartera y Clasificación
        cierres[2] = "S"; //Provisión Individual
        cierres[3] = "J"; //Provisión General

        if (txtNumComp.Text != "" && ddlTipoComprobante.SelectedValue != "")
        {
            for (int i = 0; i < 4; i++)
            {
                pCierea = new Xpinn.Comun.Entities.Cierea();
                pCierea.campo1 = txtNumComp.Text;
                pCierea.campo2 = ddlTipoComprobante.SelectedValue;
                pCierea.tipo = cierres[i];
                lstCierre = CiereaServicio.ListarCierea(pCierea, (Usuario)Session["usuario"]);
                pCierea = lstCierre.Where(x => x.campo1 == txtNumComp.Text && x.campo2 == ddlTipoComprobante.SelectedValue).FirstOrDefault();
                if (pCierea != null)
                {
                    i = 4;
                    return true;
                }
            }
        }
        return false;
    }

    protected void rbIngreso_CheckedChanged(object sender, EventArgs e)
    {
        VerError("");
        if (rbIngreso.Checked == true)
        {
            rbEgreso.Checked = false;
            rbContable.Checked = false;
        }

        return;
    }

    protected void rbEgreso_CheckedChanged(object sender, EventArgs e)
    {
        VerError("");
        if (rbEgreso.Checked == true)
        {
            rbIngreso.Checked = false;
            rbContable.Checked = false;
        }

        return;
    }

    protected void rbContable_CheckedChanged(object sender, EventArgs e)
    {
        VerError("");
        if (rbContable.Checked == true)
        {
            rbIngreso.Checked = false;
            rbEgreso.Checked = false;
        }

        return;
    }

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        VerError("...");
        Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        Persona1.soloPersona = 1;
        Persona1.sinImagen = 1;
        Persona1.seleccionar = "Identificacion";
        Persona1.identificacion = txtIdentificacion.Text;
        Persona1 = Persona1Servicio.ConsultarPersona1Param(Persona1, (Usuario)Session["usuario"]);
        if (Persona1.nombre == "errordedatos")
        {
            txtCodigo.Text = "";
            ddlTipoIdentificacion.SelectedIndex = 0;
            txtNombres.Text = "...";
        }
        //Persona1 = Persona1Servicio.ConsultaDatosPersona(txtIdentificacion.Text, (Usuario)Session["usuario"]);

        // Actualizar el detalle con las cuentas que manejan tercero
        if (txtIdentificacion.Text != null)
        {
            try
            {


                if (Persona1.cod_persona != 0)
                    txtCodigo.Text = Persona1.cod_persona.ToString();
                if (Persona1.nombres != null)
                    txtNombres.Text = HttpUtility.HtmlDecode(Persona1.nombres + " " + Persona1.apellidos);
                if (Persona1.tipo_identificacion != 0)
                    ddlTipoIdentificacion.SelectedValue = Persona1.tipo_identificacion.ToString();
                Boolean bModificar = false;
                List<DetalleComprobante> lstDetalleComprobante = new List<DetalleComprobante>();
                if (Session["DetalleComprobante"] != null)
                {
                    bModificar = true;
                    lstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
                }
                int contador = 0;
                foreach (GridViewRow rfila in gvDetMovs.Rows)
                {
                    int rowIndex = -1;
                    DetalleComprobante detalle = new DetalleComprobante();
                    if (bModificar == true)
                    {
                        rowIndex = (gvDetMovs.PageIndex * gvDetMovs.PageSize) + contador;
                        if (rowIndex < lstDetalleComprobante.Count)
                            detalle = lstDetalleComprobante[rowIndex];
                        else
                            detalle = null;
                    }
                    if (bModificar == false || (bModificar == true && detalle != null))
                    {
                        PlanCuentas ePlanCuentas = new PlanCuentas();
                        DropDownListGrid txtCodCuenta = (DropDownListGrid)rfila.FindControl("txtCodCuenta");
                        if (txtCodCuenta != null)
                            if (txtCodCuenta.SelectedItem != null && txtCodCuenta.SelectedItem.ToString().Trim() != "")
                                ePlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.SelectedItem.ToString().Trim(), (Usuario)Session["Usuario"]);
                        if (ePlanCuentas != null)
                        {
                            if (ePlanCuentas.maneja_ter == 1)
                            {
                                Label lblTercero = (Label)rfila.FindControl("lblTercero");
                                if (lblTercero != null)
                                    if (lblTercero.Text.Trim() == "")
                                        lblTercero.Text = txtCodigo.Text;
                                TextBox txtIdentificD = (TextBox)rfila.FindControl("txtIdentificD");
                                if (txtIdentificD != null)
                                    if (txtIdentificD.Text.Trim() == "")
                                        txtIdentificD.Text = txtIdentificacion.Text;
                                Label lblNombreTercero = (Label)rfila.FindControl("lblNombreTercero");
                                if (lblNombreTercero != null)
                                    if (lblNombreTercero.Text.Trim() == "")
                                        lblNombreTercero.Text = HttpUtility.HtmlDecode(txtNombres.Text);
                            }
                        }
                    }
                    contador += 1;
                }

            }
            catch { }

            #region Interaccion WorkManagement


            BuscarNumeroDeFacturaComprobanteEgreso();


            #endregion
            return;
        }
        else
        {
            VerError("...");
            txtCodigo.Text = "";
            ddlTipoIdentificacion.SelectedIndex = 0;
            txtNombres.Text = "";
        }

        focoEdicion(0, gvDetMovs, "txtCodCuenta", "");

    }

    void BuscarNumeroDeFacturaComprobanteEgreso()
    {
        // 0 = Contable, 5 = Egreso
        string tipoComprobante = Request.QueryString["tipo_comp"];
        bool creandoComprobante = string.IsNullOrWhiteSpace(idObjeto);

        if (creandoComprobante && !string.IsNullOrWhiteSpace(tipoComprobante) && !string.IsNullOrWhiteSpace(txtCodigo.Text))
        {
            tipoComprobante = tipoComprobante.ToString().Trim();
            if (tipoComprobante == "5") // Egreso
            {
                General parametroHabilitaOperacionesWM = ConsultarParametroGeneral(35);
                General parametroTipoComprobanteIniciaWorkflowPagoProveedores = ConsultarParametroGeneral(66);

                // Validamos que las operaciones con el WM esten activas 
                // Y que el parametro de tipo de comprobante para el pago de proveedores exista
                if (parametroHabilitaOperacionesWM != null && parametroHabilitaOperacionesWM.valor.Trim() == "1"
                    && parametroTipoComprobanteIniciaWorkflowPagoProveedores != null && !string.IsNullOrWhiteSpace(parametroTipoComprobanteIniciaWorkflowPagoProveedores.valor))
                {
                    // Verificamos si el valor es un numero
                    int tipoComprobanteIniciaWorkflow = 0;
                    int.TryParse(parametroTipoComprobanteIniciaWorkflowPagoProveedores.valor.Trim(), out tipoComprobanteIniciaWorkflow);

                    // Si tiene un valor mayor a cero buscamos las facturas
                    if (tipoComprobanteIniciaWorkflow > 0)
                    {
                        long codigoBeneficiario = Convert.ToInt64(txtCodigo.Text);

                        WorkManagementServices workManagementServices = new WorkManagementServices();
                        List<WorkFlowPagoProveedores> listaFacturasPendientes = workManagementServices.ConsultarNumerosFacturasPendientesPorPagarParaEsteBeneficiarioPagoProveedores(codigoBeneficiario, Usuario);

                        labelNumeroFactura.Visible = ddlNumeroFacturas.Visible = true;
                        ddlNumeroFacturas.DataSource = listaFacturasPendientes;
                        ddlNumeroFacturas.DataTextField = "numerofactura";
                        ddlNumeroFacturas.DataValueField = "numerofactura";
                        ddlNumeroFacturas.DataBind();
                        return;
                    }
                }
            }
        }
    }

    protected void txtIdentificD_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtIdentificD = (TextBoxGrid)sender;

        if (txtIdentificD != null)
        {
            Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 Persona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Persona1.soloPersona = 1;
            Persona1.seleccionar = "Identificacion";
            Persona1.identificacion = txtIdentificD.Text;
            Persona1 = Persona1Servicio.ConsultarPersona1Param(Persona1, (Usuario)Session["usuario"]);
            //Persona1 = Persona1Servicio.ConsultaDatosPersona(txtIdentificD.Text, (Usuario)Session["usuario"]);
            int rowIndex = Convert.ToInt32(txtIdentificD.CommandArgument) - (gvDetMovs.PageIndex * gvDetMovs.PageSize);
            //if (rowIndex > 0 && rowIndex < gvDetMovs.Rows.Count)
            //{
            Label lblTercero = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblTercero");
            if (lblTercero != null)
            {
                if (Persona1.cod_persona != Int64.MinValue)
                    lblTercero.Text = Convert.ToString(Persona1.cod_persona);
                else
                    lblTercero.Text = "";
            }
            Label lblNombreTercero = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblNombreTercero");
            if (lblNombreTercero != null)
            {
                if (Persona1.cod_persona != Int64.MinValue)
                    lblNombreTercero.Text = Persona1.nombres + " " + Persona1.apellidos;
                else
                    lblNombreTercero.Text = "";
                lblNombreTercero.Visible = lblTercero.Visible;
            }
            //}
            return;
        }
    }

    void NomBCuenta(int rowIndex)
    {

    }

    protected void txtCodCuenta_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtCodCuenta = null;
        GridViewRow rFila = null;
        try
        {
            txtCodCuenta = (TextBoxGrid)sender;
        }
        catch
        {
            rFila = ((GridViewRow)((GridView)sender).NamingContainer.NamingContainer);
            txtCodCuenta = (TextBoxGrid)gvDetMovs.Rows[rFila.RowIndex].FindControl("txtCodCuenta");
        }

        if (txtCodCuenta != null)
        {
            Int32 validacion = 0;
            //Valida si el comprobante es o no generado     
            if ((string)Session["Nuevo"] == "1" || (string)Session["detalle"] == "1" || Session["Carga"] != null || (Session["Modificar"] == null && Convert.ToInt64(Session["cod_ope"]) == 0 && !ValidarComCierre()))
            {
                //Validar cuentas que ya se encuentren en el comprobante si se adiciona detalle
                if ((string)Session["detalle"] == "1")
                {
                    List<Xpinn.Contabilidad.Entities.DetalleComprobante> LstDetalleComprobante = new List<Xpinn.Contabilidad.Entities.DetalleComprobante>();
                    LstDetalleComprobante = (List<Xpinn.Contabilidad.Entities.DetalleComprobante>)Session["DetalleComprobante"];
                    DetalleComprobante vDetalle = new DetalleComprobante();

                    rFila = txtCodCuenta.NamingContainer as GridViewRow;
                    int conseID = Convert.ToInt32(gvDetMovs.DataKeys[rFila.RowIndex].Values[0].ToString());
                    vDetalle = LstDetalleComprobante.Where(x => x.cod_cuenta == txtCodCuenta.Text && x.codigo == conseID).FirstOrDefault();

                    if (vDetalle != null)
                        validacion = 0;
                    else if (conseID == -1)
                        validacion = ComprobanteServicio.ValidarCuentaContable(1, txtCodCuenta.Text, (Usuario)Session["usuario"]);
                }
                else
                    //Verifica cuentas contables que se manejan en productos
                    validacion = ComprobanteServicio.ValidarCuentaContable(1, txtCodCuenta.Text, (Usuario)Session["usuario"]);
            }

            if (validacion == 0)
            {
                Lblerror.Text = "";
                // Determinar los datos de la cuenta contable
                Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
                PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta.Text, (Usuario)Session["usuario"]);
                int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);

                // Mostrar el nombre de la cuenta
                TextBoxGrid ddlNomCuenta = (TextBoxGrid)gvDetMovs.Rows[rowIndex].FindControl("ddlNomCuenta");
                if (ddlNomCuenta != null)
                    ddlNomCuenta.Text = PlanCuentas.nombre;


                // Si la cuenta maneja terceros colocar el tercero
                Label lblManejaTer = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblManejaTer");
                if (lblManejaTer != null)
                    lblManejaTer.Text = Convert.ToString(PlanCuentas.maneja_ter);
                TextBox txtIdentificD = (TextBox)gvDetMovs.Rows[rowIndex].FindControl("txtIdentificD");
                if (txtIdentificD != null)
                {
                    Label lblTercero = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblTercero");
                    Label lblNombreTercero = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblNombreTercero");
                    if (lblManejaTer.Text == "1")
                    {
                        lblTercero.Visible = true;
                        lblNombreTercero.Visible = true;
                        if (PlanCuentas.maneja_ter != 1)
                            txtIdentificD.Visible = false;
                        else
                            txtIdentificD.Visible = true;
                        lblNombreTercero.Visible = txtIdentificD.Visible;
                        lblTercero.Visible = txtIdentificD.Visible;
                        if (txtIdentificD.Text == "")
                        {
                            txtIdentificD.Text = txtIdentificacion.Text;
                            txtIdentificD_TextChanged(txtIdentificD, null);
                        }
                    }
                    else
                    {
                        lblTercero.Visible = false;
                        lblNombreTercero.Visible = false;
                        txtIdentificD.Visible = false;
                    }
                }


                // Colocar la homologación de NIIF para la cuenta
                TextBoxGrid txtCodCuentaNIF = (TextBoxGrid)gvDetMovs.Rows[rowIndex].FindControl("txtCodCuentaNIF");
                if (txtCodCuentaNIF != null)
                    txtCodCuentaNIF.Text = PlanCuentas.cod_cuenta_niif;
                if (txtCodCuentaNIF.Text == null)
                    txtCodCuentaNIF.Text = "";
                if (txtCodCuentaNIF.Text == "")
                    txtCodCuentaNIF.Text = "";
                TextBoxGrid txtNomCuentaNif = (TextBoxGrid)gvDetMovs.Rows[rowIndex].FindControl("txtNomCuentaNif");
                if (txtNomCuentaNif != null)
                    txtNomCuentaNif.Text = PlanCuentas.nombre_niif;
                // Asignar el evento para el valor y que pueda totalizar
                TextBoxGrid txtValor = (TextBoxGrid)gvDetMovs.Rows[rowIndex].FindControl("txtValor");
                //if (txtValor != null)
                //    txtValor.eventoCambiar += txtValor_TextChanged;

                // Si la cuenta maneja impuestos entonces calcular las bases
                Label lblImpuesto = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblImpuesto");
                TextBox txtBaseGrid = (TextBox)gvDetMovs.Rows[rowIndex].FindControl("txtBaseGrid");
                TextBox txtPorcentGrid = (TextBox)gvDetMovs.Rows[rowIndex].FindControl("txtPorcentGrid");
                Label lblBase = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblBase");
                Label lblPorcentaje = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblPorcentaje");
                ButtonGrid btnGenerarImpuesto = (ButtonGrid)gvDetMovs.Rows[rowIndex].FindControl("btnGenerarImpuesto");
                if (PlanCuentas.impuesto != null && PlanCuentas.impuesto == 1)
                {
                    btnGenerarImpuesto.Visible = true;
                    lblImpuesto.Text = PlanCuentas.impuesto.ToString();
                }
                else
                {
                    btnGenerarImpuesto.Visible = false;
                }

                PlanCuentas.es_impuesto = PlanCuentas.es_impuesto != null ? PlanCuentas.es_impuesto : "0";
                if (Convert.ToInt32(PlanCuentas.es_impuesto) >= 1)
                {
                    lblBase.Visible = true;
                    lblPorcentaje.Visible = true;
                    txtBaseGrid.Visible = true;
                    txtPorcentGrid.Visible = true;
                    if (txtPorcentGrid != null || txtPorcentGrid.Text != "")
                    {
                        if (txtValor.Text == "")
                            txtValor.Text = "0";
                        if (PlanCuentas.porcentaje_impuesto != null)
                        {
                            //txtPorcentGrid.Text = Convert.ToString(PlanCuentas.porcentaje_impuesto);
                            if (txtPorcentGrid.Text != "")
                                if (Convert.ToDecimal(txtPorcentGrid.Text) != 0)
                                    txtBaseGrid.Text = (Convert.ToDecimal(txtValor.Text) / (Convert.ToDecimal(txtPorcentGrid.Text)) * 100).ToString("N0");
                        }
                        if (txtBaseGrid.Text == "0")
                        {
                            lblBase.Visible = false;
                            lblPorcentaje.Visible = false;
                            txtBaseGrid.Visible = false;
                            txtPorcentGrid.Visible = false;
                        }
                    }
                }
                else
                {
                    lblBase.Visible = false;
                    lblPorcentaje.Visible = false;
                    txtBaseGrid.Visible = false;
                    txtPorcentGrid.Visible = false;
                    txtBaseGrid.Text = "0";
                    txtPorcentGrid.Text = "0";
                }


                // Asignar cursor
                if (gvDetMovs.Columns[2].Visible == true)
                {
                    focoEdicion(rowIndex, gvDetMovs, "txtCodCuentaNIF", "2doModoFiltro");
                    return;
                }

                else
                {
                    focoEdicion(rowIndex, gvDetMovs, "ddlCentroCosto", "2doModoFiltro");
                    return;
                }


            }
            else
            {
                //Vaciar los campos de código de cuenta y de nombre de cuenta si no se puede agregar
                txtCodCuenta.Text = "";
                int rowIndex = Convert.ToInt32(txtCodCuenta.CommandArgument);
                TextBoxGrid ddlNomCuenta = (TextBoxGrid)gvDetMovs.Rows[rowIndex].FindControl("ddlNomCuenta");
                if (ddlNomCuenta != null)
                    ddlNomCuenta.Text = "";

                Lblerror.Text = "Código contable de productos, no permite modificaciones manuales ";
            }

        }
    }

    protected void txtCodCuentaNIF_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtCodCuentaNIF = (TextBoxGrid)sender;
        if (txtCodCuentaNIF != null)
        {
            // Traer los datos de la cuenta para NIIF
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuentaNIF.Text, (Usuario)Session["usuario"]);
            int rowIndex = Convert.ToInt32(txtCodCuentaNIF.CommandArgument);
            // Mostrar el nombre de la cuenta
            TextBoxGrid txtNomCuentaNif = (TextBoxGrid)gvDetMovs.Rows[rowIndex].FindControl("txtNomCuentaNif");
            if (txtNomCuentaNif != null)
                txtNomCuentaNif.Text = PlanCuentas.nombre;
            // Si la cuenta maneja terceros colocar el tercero
            Label lblManejaTer = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblManejaTer");
            if (lblManejaTer != null)
                lblManejaTer.Text = Convert.ToString(PlanCuentas.maneja_ter);
            TextBox txtIdentificD = (TextBox)gvDetMovs.Rows[rowIndex].FindControl("txtIdentificD");
            if (txtIdentificD != null)
            {
                Label lblTercero = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblTercero");
                Label lblNombreTercero = (Label)gvDetMovs.Rows[rowIndex].FindControl("lblNombreTercero");
                if (PlanCuentas.maneja_ter != 1)
                    txtIdentificD.Visible = false;
                else
                    txtIdentificD.Visible = true;
                lblNombreTercero.Visible = txtIdentificD.Visible;
                lblTercero.Visible = txtIdentificD.Visible;
                if (txtIdentificD.Text == "")
                {
                    txtIdentificD.Text = txtIdentificacion.Text;
                    txtIdentificD_TextChanged(txtIdentificD, null);
                }
            }
            // Evento para el control del valor
            TextBoxGrid txtValor = (TextBoxGrid)gvDetMovs.Rows[rowIndex].FindControl("txtValor");
            //if (txtValor != null)
            //    txtValor.eventoCambiar += txtValor_TextChanged;
            // Manejo de impuestos
            TextBox txtBaseGrid = (TextBox)gvDetMovs.Rows[rowIndex].FindControl("txtBaseGrid");
            TextBox txtPorcentGrid = (TextBox)gvDetMovs.Rows[rowIndex].FindControl("txtPorcentGrid");
            if (PlanCuentas.impuesto != null && PlanCuentas.impuesto == 1)
            {
                if (txtPorcentGrid != null || txtPorcentGrid.Text != "")
                {
                    if (txtValor.Text == "") txtValor.Text = "0";
                    txtPorcentGrid.Text = Convert.ToString(PlanCuentas.porcentaje_impuesto);
                    if (Convert.ToDecimal(txtPorcentGrid.Text) != 0)
                        txtBaseGrid.Text = Convert.ToString(Convert.ToDecimal(txtValor.Text) / (Convert.ToDecimal(txtPorcentGrid.Text)) * 100);
                }
            }
            else
            {
                txtBaseGrid.Text = "0";
                txtPorcentGrid.Text = "0";
            }

            focoEdicion(rowIndex, gvDetMovs, "ddlCentroCosto", "2doModoFiltro");
        }

        return;
    }

    protected void imgAceptarProceso_Click(object sender, ImageClickEventArgs e)
    {
        if (lstProcesos.SelectedValue == "")
        {
            VerError("seleccione un tipo de comprobante ");
            return;
        }
        Usuario usuap = (Usuario)Session["usuario"];
        tbxOficina.Text = usuap.nombre_oficina;
        // Generar el comprobante
        Int64 pcod_proceso = Convert.ToInt64(lstProcesos.SelectedValue);
        Int64 pnum_comp = 0;
        Int64 ptipo_comp = 0;
        Int64 pcod_persona = 0;
        Int64 pcod_ope = 0;
        string pError = "";
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] != null)
            pcod_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"]);
        Int64 ptip_ope = 0;
        if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
            ptip_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"]);
        DateTime pfecha = System.DateTime.Now;
        if (Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] != null)
            pfecha = Convert.ToDateTime(Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"]);
        Int64 pcod_ofi = usuap.cod_oficina;
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] != null)
            pcod_ofi = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"]);
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] != null)
            pcod_persona = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"]);
        if (ComprobanteServicio.GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref pError, usuap) == true)
        {
            idObjeto = Convert.ToString(pnum_comp);
            ObtenerDatos(pnum_comp.ToString(), ptipo_comp.ToString());
            if (Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"] != null)
            {
                try
                {
                    ddlCuenta.SelectedItem.Text = Convert.ToString(Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"]);
                }
                catch
                {
                    if (ptipo_comp == 5)
                        VerError("La cuenta bancaria es incorrecta." + Convert.ToString(Session[ComprobanteServicio.CodigoPrograma + ".CuentaBancaria"]));
                }
            }
            PanelActiveView(vwPanel1);
            tbxOficina.Text = usuap.nombre_oficina;
        }
        if (ddlFormaPago.SelectedItem != null)
            ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
        PanelActiveView(vwPanel1);
    }


    public String vacios(String texto)
    {
        if (String.IsNullOrEmpty(texto))
        {
            return " ";
        }
        else
        {
            return texto;
        }
    }

    //public DataTable CrearDataTableMovimientos()
    //{
    //    ObtenerDetalleComprobante(false);
    //    List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
    //    LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];     
    //    System.Data.DataTable table = new System.Data.DataTable();

    //    table.Columns.Add("CodCuenta").AllowDBNull = true;
    //    table.Columns.Add("NomCuenta").AllowDBNull = true;
    //    table.Columns.Add("Identificacion").AllowDBNull = true;
    //    table.Columns.Add("Detalle").AllowDBNull = true;
    //    table.Columns.Add("ValorDebito").AllowDBNull = true;
    //    table.Columns.Add("ValorCredito").AllowDBNull = true;
    //    table.Columns.Add("tipo").AllowDBNull = true;
    //    table.Columns.Add("CC").AllowDBNull = true;

    //    foreach (DetalleComprobante item in LstDetalleComprobante)
    //    {
    //        DataRow datarw;
    //        datarw = table.NewRow();
    //        datarw[0] = item.cod_cuenta;
    //        datarw[1] = item.nombre_cuenta;
    //        datarw[2] = item.identificacion;
    //        datarw[3] = item.detalle;

    //        if (item.tipo == "D")
    //        {
    //            datarw[4] = item.valor != null ? item.valor.Value.ToString("0,0") : "";
    //        }
    //        if (item.tipo == "C")
    //        {
    //            datarw[5] = item.valor != null ? item.valor.Value.ToString("0,0") : "";
    //        }
    //        datarw[6] = item.tipo;
    //        datarw[7] = item.centro_costo;

    //        table.Rows.Add(datarw);
    //    }
    //    return table;
    //}

    /// <summary>
    /// Método para la impresión
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInforme_Click(object sender, EventArgs e)
    {
        GeneraReporteComprobantePago(sender);

        RpviewComprobante.Visible = true;
        frmPrint.Visible = false;
        //mvComprobante.Visible = true;
        PanelActiveView(vwPanel4);
    }

    private void GeneraReporteComprobantePago(object sender)
    {
        string separadorDecimal = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        Configuracion conf = new Configuracion();
        if (sender == btnImprimir)
        {
            // Si se imprime después de grabar el comprobante
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            btnRegresarComp.Visible = false;
        }
        // Validando datos nulos
        if (tipobeneficiario == null)
            tipobeneficiario = "";

        // Determinar datos del usuario y entidad
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        //CREAR TABLA GENERAL;
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("fecha");
        table.Columns.Add("valor");
        table.Columns.Add("nombre1");
        table.Columns.Add("cardinal");
        table.Columns.Add("entidad");
        table.Columns.Add("nit");
        table.Columns.Add("tipoComprobante");
        table.Columns.Add("num_comp");
        table.Columns.Add("identificacion");
        table.Columns.Add("tipo_indetificacion");
        table.Columns.Add("nombres");
        table.Columns.Add("ciudad");
        table.Columns.Add("concepto");
        table.Columns.Add("cod_cuenta");
        table.Columns.Add("nom_cuenta");
        table.Columns.Add("ident_deta");
        table.Columns.Add("detalle");
        table.Columns.Add("valordebito", typeof(decimal));
        table.Columns.Add("valorcredito", typeof(decimal));
        table.Columns.Add("tipo");
        table.Columns.Add("totaldebito", typeof(decimal));
        table.Columns.Add("totalcredito", typeof(decimal));
        table.Columns.Add("num_cheque");
        table.Columns.Add("detalle_general");
        table.Columns.Add("rptaEgreso");
        table.Columns.Add("CC");
        table.Columns.Add("direccion");
        table.Columns.Add("telefono");
        Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
        Xpinn.Contabilidad.Entities.DetalleComprobante vDetalleComprobante = new Xpinn.Contabilidad.Entities.DetalleComprobante();

        vComprobante = ComprobanteServicio.ConsultarDatosElaboro(Convert.ToInt32(pUsuario.codusuario), (Usuario)Session["Usuario"]);
        {
            tbxDireccion.Text = HttpUtility.HtmlDecode(vComprobante.direccion.ToString());
            tbxTelefono.Text = HttpUtility.HtmlDecode(vComprobante.telefono.ToString());
        }


        //CARGAR LOS DATOS DEL DETALLE
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

        string valor = lablerror0.Text;
        string cRutaDeImagen;
        cRutaDeImagen = new Uri(Server.MapPath("~/Images/LogoEmpresa.jpg")).AbsoluteUri;

        //CONSULTAR SI SE IMPRIMEN EL MONTO EN TEXTO
        Xpinn.Comun.Data.GeneralData DAGenenal = new Xpinn.Comun.Data.GeneralData();
        Xpinn.Comun.Entities.General pEntity = new Xpinn.Comun.Entities.General();

        pEntity = DAGenenal.ConsultarGeneral(90163, (Usuario)Session["usuario"]);

        // Determinar la fecha del comprobante            
        string a = Convert.ToDateTime(txtFecha.Text).ToString("yyyy/MM/dd");
        string fecha = a.Replace("/", "  ");

        // Determinando el valor en letras
        Cardinalidad numero = new Cardinalidad();
        string cardinal = " ";
        if (valor != "0")
        {
            cardinal = numero.enletras(valor.Replace(".", ""));
            int cont = cardinal.Trim().Length - 1;
            int cont2 = cont - 7;
            if (cont2 >= 0)
            {
                string c = cardinal.Substring(cont2);
                if (cardinal.Trim().Substring(cont2) == "MILLONES" || cardinal.Trim().Substring(cont2 + 2) == "MILLON")
                    cardinal = cardinal + " DE PESOS M/CTE";
                else
                    cardinal = cardinal + " PESOS M/CTE";
            }
        }

        if (cbImpresion.Checked)
        {
            List<DetalleComprobante> lstResumen = new List<DetalleComprobante>();
            LstDetalleComprobante = (from o in LstDetalleComprobante
                                     group o by new { o.cod_cuenta, o.cod_cuenta_niif, o.tipo, o.centro_costo } into g
                                     select new DetalleComprobante
                                     {
                                         cod_cuenta_niif = g.Key.cod_cuenta_niif,
                                         nombre_cuenta_nif = g.Where(x => x.cod_cuenta_niif == g.Key.cod_cuenta_niif).FirstOrDefault().nombre_cuenta_nif,
                                         nombre_cuenta = g.Where(x => x.cod_cuenta == g.Key.cod_cuenta).FirstOrDefault().nombre_cuenta,
                                         identificacion = g.Where(x => x.cod_cuenta == g.Key.cod_cuenta).FirstOrDefault().identificacion,
                                         detalle = g.Max(x => x.detalle).ToString(),
                                         valor = Convert.ToDecimal(g.Sum(x => x.valor)),
                                         tipo = g.Key.tipo,
                                         centro_costo = g.Key.centro_costo,
                                         cod_cuenta = g.Key.cod_cuenta

                                     }).ToList<DetalleComprobante>();

        }
        //RECUPERAR EL DETALLE DEL COMPROBANTE SELECCIONADO
        foreach (DetalleComprobante item in LstDetalleComprobante)
        {
            System.Data.DataRow datarw;
            datarw = table.NewRow();
            datarw[0] = vacios(fecha);
            datarw[1] = lablerror0.Text;
            if (txtnom.Text != "")
                datarw[2] = HttpUtility.HtmlDecode(vacios(txtnom.Text));
            else
                datarw[2] = HttpUtility.HtmlDecode(vacios(txtNombres.Text));
            if (pEntity != null && pEntity.valor != null)
            {
                if (pEntity.valor == "1")
                    cardinal = " ";
            }
            datarw[3] = cardinal;
            datarw[4] = pUsuario.empresa;
            datarw[5] = pUsuario.nitempresa;
            datarw[6] = vacios(ddlTipoComprobante.SelectedItem.Text.ToUpper());
            datarw[7] = vacios(txtNumComp.Text);
            datarw[8] = vacios(txtIdentificacion.Text);
            datarw[9] = vacios(ddlTipoIdentificacion.SelectedItem.Text);
            datarw[10] = HttpUtility.HtmlDecode(vacios(txtNombres.Text));
            datarw[11] = vacios(ddlCiudad.SelectedItem.Text);
            datarw[12] = vacios(ddlConcepto.SelectedItem.Text);
            if (cbNif.Checked)
            {
                datarw[13] = item.cod_cuenta_niif;
                datarw[14] = item.nombre_cuenta_nif;
            }
            else
            {
                datarw[13] = item.cod_cuenta;
                datarw[14] = item.nombre_cuenta;
            }
            datarw[15] = item.identificacion;
            datarw[16] = item.detalle;

            if (item.tipo == "D")
            {
                datarw[17] = item.valor;
                datarw[18] = 0;
            }
            if (item.tipo == "C")
            {
                datarw[17] = 0;
                datarw[18] = item.valor;
            }
            datarw[19] = item.tipo;
            datarw[20] = vacios(tbxTotalDebitos.Text);
            datarw[21] = vacios(tbxTotalCreditos.Text);
            datarw[22] = txtNumSop.Text;
            datarw[23] = HttpUtility.HtmlDecode(vacios(tbxObservaciones.Text));
            datarw[24] = vacios(ddlTipoComprobante.SelectedValue);
            datarw[25] = item.centro_costo;

            datarw[26] = HttpUtility.HtmlDecode(vacios(tbxDireccion.Text));
            datarw[27] = HttpUtility.HtmlDecode(vacios(tbxTelefono.Text));

            table.Rows.Add(datarw);
        }


        // Imprimir los comprobantes de EGRESO
        if (ddlTipoComprobante.SelectedValue == "5")
        {
            // Imprimer el reporte según sea cheque u otra forma de pago
            RpviewComprobante.LocalReport.ReportPath = "Page/Contabilidad/Comprobante/ReportEgresos.rdlc";
            if (ddlFormaPago.SelectedValue == "2" || ddlFormaPago.SelectedValue == "1")
            {
                if (pUsuario.reporte_egreso == null)
                {
                    //RpviewComprobante.LocalReport.ReportPath = "Page/Contabilidad/Comprobante/ReportEgresos.rdlc";
                    RpviewComprobante.LocalReport.ReportPath = "Page/Contabilidad/Comprobante/ReportEgresosAnterior.rdlc";
                }
                else
                {
                    // Para cualquier OTRA ENTIDAD BANCARIA.
                    RpviewComprobante.LocalReport.ReportPath = "Page/Contabilidad/Comprobante/" + pUsuario.reporte_egreso;
                }
            }
            else
                RpviewComprobante.LocalReport.ReportPath = "Page/Contabilidad/Comprobante/ReportEgresos.rdlc";

            // Se añadido Consulta de la ruta de la tabla Bancos segun el numero de cuenta y asigna esa ruta al reporte

            if (!String.IsNullOrEmpty(ddlCuenta.SelectedValue) || ddlCuenta.SelectedValue != "")
                if (Session["Ruta_Cheque"] != null)
                    if (Session["Ruta_Cheque"].ToString().Trim() != "")
                        RpviewComprobante.LocalReport.ReportPath = Session["Ruta_Cheque"].ToString();


            // Parámetros del comprobante
            ReportParameter[] param = new ReportParameter[23];
            param[0] = new ReportParameter("pEntidad", HttpUtility.HtmlDecode(pUsuario.empresa));
            param[1] = new ReportParameter("pNumComp", HttpUtility.HtmlDecode(vacios(txtNumComp.Text)));
            param[2] = new ReportParameter("pFecha", HttpUtility.HtmlDecode(vacios(fecha)));
            param[3] = new ReportParameter("pTipoComprobante", HttpUtility.HtmlDecode(vacios(ddlTipoComprobante.SelectedValue)));
            param[4] = new ReportParameter("pTipoComprobante2", HttpUtility.HtmlDecode(vacios(ddlTipoComprobante.SelectedItem.Text.ToUpper())));
            param[5] = new ReportParameter("pIdentificacion", HttpUtility.HtmlDecode(vacios(txtIdentificacion.Text)));
            param[6] = new ReportParameter("pTipoIdentificacion", vacios(ddlTipoIdentificacion.SelectedItem.Text));
            param[7] = new ReportParameter("pNombres", HttpUtility.HtmlDecode(vacios(txtNombres.Text)));
            param[8] = new ReportParameter("pConcepto", HttpUtility.HtmlDecode(vacios(ddlConcepto.SelectedItem.Text)));
            param[9] = new ReportParameter("pElaborado", HttpUtility.HtmlDecode(vacios(txtElaboradoPor.Text)));
            param[10] = new ReportParameter("pCiudad", HttpUtility.HtmlDecode(vacios(ddlCiudad.SelectedItem.Text)));
            param[11] = new ReportParameter("pTipoBeneficiario", HttpUtility.HtmlDecode(vacios(tipobeneficiario)));
            param[12] = new ReportParameter("pTotalDebito", HttpUtility.HtmlDecode(vacios(tbxTotalDebitos.Text.Replace(gSeparadorDecimal, "."))));
            param[13] = new ReportParameter("pTotalCredito", HttpUtility.HtmlDecode(vacios(tbxTotalCreditos.Text.Replace(gSeparadorDecimal, "."))));
            param[14] = new ReportParameter("detalle", HttpUtility.HtmlDecode(vacios(tbxObservaciones.Text)));
            if (pEntity != null && pEntity.valor != null)
            {
                if (pEntity.valor == "1")
                    cardinal = " ";
            }
            param[15] = new ReportParameter("cardinalidad", cardinal);
            param[16] = new ReportParameter("valor", lablerror0.Text);
            if (txtnom.Text.Trim() == "")
                param[17] = new ReportParameter("nombre1", vacios(HttpUtility.HtmlDecode(txtNombres.Text)));
            else
                param[17] = new ReportParameter("nombre1", vacios(HttpUtility.HtmlDecode(txtnom.Text)));
            param[18] = new ReportParameter("NumCheque", HttpUtility.HtmlDecode(txtNumSop.Text));
            param[19] = new ReportParameter("pNit", HttpUtility.HtmlDecode(pUsuario.nitempresa));
            param[20] = new ReportParameter("ImagenReport", cRutaDeImagen);
            param[21] = new ReportParameter("pdireccion", HttpUtility.HtmlDecode(tbxDireccion.Text));
            param[22] = new ReportParameter("ptelefono", HttpUtility.HtmlDecode(tbxTelefono.Text));

            RpviewComprobante.LocalReport.EnableExternalImages = true;
            RpviewComprobante.LocalReport.SetParameters(param);

            var sa = RpviewComprobante.LocalReport.GetDefaultPageSettings();
            RpviewComprobante.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            RpviewComprobante.LocalReport.DataSources.Add(rds);
            RpviewComprobante.LocalReport.Refresh();
        }
        else if (ddlTipoComprobante.SelectedValue == "1")
        {
            //CONSULTAR SI SE IMPRIMEN EL MONTO EN TEXTO           
            Xpinn.Comun.Entities.General pEntitys = new Xpinn.Comun.Entities.General();

            pEntity = DAGenenal.ConsultarGeneral(90163, (Usuario)Session["usuario"]);
            pEntitys = DAGenenal.ConsultarGeneral(170, (Usuario)Session["usuario"]);

            // Determinando el valor en letras
            cardinal = " ";
            if (valor != "0")
            {
                cardinal = numero.enletras(valor.Replace(".", ""));
                int cont = cardinal.Length - 1;
                int cont2 = cont - 7;
                if (cont2 >= 0)
                {
                    string c = cardinal.Substring(cont2);
                    if (cardinal.Substring(cont2) == "MILLONES" || cardinal.Substring(cont2 + 2) == "MILLON")
                        cardinal = cardinal + " DE PESOS M/CTE";
                    else
                        cardinal = cardinal + " PESOS M/CTE";
                }
            }

            if (pUsuario.reporte_ingreso == null)
                RpviewComprobante.LocalReport.ReportPath = "Page/Contabilidad/Comprobante/ReportIngresos.rdlc";
            else
                RpviewComprobante.LocalReport.ReportPath = "Page/Contabilidad/Comprobante/" + pUsuario.reporte_ingreso;

            // Parámetros del comprobante
            ReportParameter[] param = new ReportParameter[29];
            param[0] = new ReportParameter("pEntidad", HttpUtility.HtmlDecode(pUsuario.empresa));
            param[1] = new ReportParameter("pNumComp", HttpUtility.HtmlDecode(vacios(txtNumComp.Text)));
            param[2] = new ReportParameter("pFecha", HttpUtility.HtmlDecode(vacios(fecha)));
            param[3] = new ReportParameter("pTipoComprobante", HttpUtility.HtmlDecode(vacios(ddlTipoComprobante.SelectedValue)));
            param[4] = new ReportParameter("pTipoComprobante2", HttpUtility.HtmlDecode(vacios(ddlTipoComprobante.SelectedItem.Text.ToUpper())));
            param[5] = new ReportParameter("pIdentificacion", HttpUtility.HtmlDecode(vacios(txtIdentificacion.Text)));
            param[6] = new ReportParameter("pTipoIdentificacion", HttpUtility.HtmlDecode(vacios(ddlTipoIdentificacion.SelectedItem.Text)));
            param[7] = new ReportParameter("pNombres", HttpUtility.HtmlDecode(vacios(txtNombres.Text)));
            param[8] = new ReportParameter("pConcepto", HttpUtility.HtmlDecode(vacios(ddlConcepto.SelectedItem.Text)));
            param[9] = new ReportParameter("pElaborado", HttpUtility.HtmlDecode(vacios(txtElaboradoPor.Text)));
            param[10] = new ReportParameter("pCiudad", HttpUtility.HtmlDecode(vacios(ddlCiudad.SelectedItem.Text)));
            param[11] = new ReportParameter("pTipoBeneficiario", HttpUtility.HtmlDecode(vacios(tipobeneficiario)));
            param[12] = new ReportParameter("pTotalDebito", HttpUtility.HtmlDecode(vacios(tbxTotalDebitos.Text)));
            param[13] = new ReportParameter("pTotalCredito", HttpUtility.HtmlDecode(vacios(tbxTotalCreditos.Text)));
            param[14] = new ReportParameter("detalle", HttpUtility.HtmlDecode(vacios(tbxObservaciones.Text)));
            if (pEntity != null && pEntity.valor != null)
            {
                if (pEntity.valor == "1")
                    cardinal = " ";
            }
            param[15] = new ReportParameter("NumCheque", HttpUtility.HtmlDecode(txtNumSop.Text));
            param[16] = new ReportParameter("valor", HttpUtility.HtmlDecode(lablerror0.Text));
            if (txtnom.Text.Trim() == "")
                param[17] = new ReportParameter("nombre1", HttpUtility.HtmlDecode(vacios(txtNombres.Text)));
            else
                param[17] = new ReportParameter("nombre1", HttpUtility.HtmlDecode(vacios(txtnom.Text)));
            param[18] = new ReportParameter("Numerocuenta", HttpUtility.HtmlDecode(txtNumSop.Text));
            param[19] = new ReportParameter("pNit", HttpUtility.HtmlDecode(pUsuario.nitempresa));
            param[20] = new ReportParameter("ImagenReport", cRutaDeImagen);
            param[21] = new ReportParameter("FormaPago", HttpUtility.HtmlDecode((ddlFormaPago.SelectedItem.Text)));
            param[22] = new ReportParameter("pasar", "nada");
            if (ddlEntidadOrigen.SelectedIndex != 0)
            {
                param[23] = new ReportParameter("Banco", HttpUtility.HtmlDecode((ddlEntidadOrigen.SelectedItem.Text)));
            }
            else
            {
                param[23] = new ReportParameter("Banco", " ");
            }
            param[24] = new ReportParameter("Elaborado", HttpUtility.HtmlDecode((txtElaboradoPor.Text)));
            foreach (GridViewRow gvbuscar in gvDetMovs.Rows)
            {
                TextBoxGrid txtcod_cuenta = (TextBoxGrid)gvbuscar.FindControl("txtCodCuenta");
                TextBoxGrid txtvalor = (TextBoxGrid)gvbuscar.FindControl("txtValor");
                if (Convert.ToInt64(string.IsNullOrWhiteSpace(txtcod_cuenta.Text) ? "0" : txtcod_cuenta.Text) == Convert.ToInt64(pEntitys.valor))
                {
                    param[25] = new ReportParameter("valportotal", (txtvalor.Text));
                    string asa = Convert.ToDateTime(txtFecha.Text).ToString("yyyy/MM/dd");
                    string fechas = asa.Replace("/", "     ");

                    // Determinando el valor en letras
                    Cardinalidad numeros = new Cardinalidad();
                    string cardinals = " ";
                    if (txtvalor.Text != "0")
                    {
                        cardinals = numeros.enletras(txtvalor.Text.Replace(".", ""));
                        int cont = cardinals.Length - 1;
                        int cont2 = cont - 7;
                        if (cont2 >= 0)
                        {
                            string c = cardinals.Substring(cont2);
                            if (cardinals.Substring(cont2) == "MILLONES" || cardinals.Substring(cont2 + 2) == "MILLON")
                                cardinals = cardinals + " DE PESOS M/CTE";
                            else
                                cardinals = cardinals + " PESOS M/CTE";
                        }
                    }
                    param[26] = new ReportParameter("cardinalidad", (cardinals));
                }
            }
            if (param[25] == null)
            {
                param[25] = new ReportParameter("valportotal", vacios(tbxTotalDebitos.Text));
                string asas = Convert.ToDateTime(txtFecha.Text).ToString("yyyy/MM/dd");
                string fechass = asas.Replace("/", "     ");

                // Determinando el valor en letras
                Cardinalidad numeros = new Cardinalidad();
                string cardinales = " ";
                if (tbxTotalDebitos.Text != "0")
                {
                    cardinales = numeros.enletras(tbxTotalDebitos.Text.Replace(".", ""));
                    int cont = cardinales.Length - 1;
                    int cont2 = cont - 7;
                    if (cont2 >= 0)
                    {
                        string c = cardinales.Substring(cont2);
                        if (cardinales.Substring(cont2) == "MILLONES" || cardinales.Substring(cont2 + 2) == "MILLON")
                            cardinales = cardinales + " DE PESOS M/CTE";
                        else
                            cardinales = cardinales + " PESOS M/CTE";
                    }
                }
                param[26] = new ReportParameter("cardinalidad", cardinales);
            }
            param[27] = new ReportParameter("pDireccion", tbxDireccion.Text);
            param[28] = new ReportParameter("ptelefono", tbxTelefono.Text);

            RpviewComprobante.LocalReport.EnableExternalImages = true;
            RpviewComprobante.LocalReport.SetParameters(param);

            // Determinando las margenes del comprobante
            Margins margins = new Margins(100, 100, 100, 100);
            var sa = RpviewComprobante.LocalReport.GetDefaultPageSettings();
            RpviewComprobante.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            RpviewComprobante.LocalReport.DataSources.Add(rds);
            RpviewComprobante.LocalReport.Refresh();

        }
        else
        {
            ReportParameter[] param = new ReportParameter[18];
            param[0] = new ReportParameter("pEntidad", HttpUtility.HtmlDecode(pUsuario.empresa));
            param[1] = new ReportParameter("pNumComp", HttpUtility.HtmlDecode(vacios(txtNumComp.Text)));
            param[2] = new ReportParameter("pFecha", HttpUtility.HtmlDecode(vacios(txtFecha.Text)));
            param[3] = new ReportParameter("pTipoComprobante", HttpUtility.HtmlDecode(vacios(ddlTipoComprobante.SelectedValue)));
            param[4] = new ReportParameter("pTipoComprobante2", HttpUtility.HtmlDecode(vacios(ddlTipoComprobante.SelectedItem.Text.ToUpper())));
            param[5] = new ReportParameter("pIdentificacion", HttpUtility.HtmlDecode(vacios(txtIdentificacion.Text)));
            param[6] = new ReportParameter("pTipoIdentificacion", HttpUtility.HtmlDecode(vacios(ddlTipoIdentificacion.SelectedItem.Text)));
            param[7] = new ReportParameter("pNombres", HttpUtility.HtmlDecode(vacios(txtNombres.Text)));
            param[8] = new ReportParameter("pConcepto", HttpUtility.HtmlDecode(vacios(ddlConcepto.SelectedItem.Text)));
            param[9] = new ReportParameter("pElaborado", HttpUtility.HtmlDecode(vacios(txtElaboradoPor.Text)));
            param[10] = new ReportParameter("pCiudad", HttpUtility.HtmlDecode(vacios(ddlCiudad.SelectedItem.Text)));
            param[11] = new ReportParameter("pTipoBeneficiario", HttpUtility.HtmlDecode(vacios(tipobeneficiario)));
            param[12] = new ReportParameter("pTotalDebito", HttpUtility.HtmlDecode(vacios(tbxTotalDebitos.Text)));
            param[13] = new ReportParameter("pTotalCredito", HttpUtility.HtmlDecode(vacios(tbxTotalCreditos.Text)));
            param[14] = new ReportParameter("ImagenReport", cRutaDeImagen);
            param[15] = new ReportParameter("detalle", HttpUtility.HtmlDecode(vacios(tbxObservaciones.Text)));
            param[16] = new ReportParameter("pdireccion", HttpUtility.HtmlDecode(vacios(tbxDireccion.Text)));
            param[17] = new ReportParameter("ptelefono", HttpUtility.HtmlDecode(vacios(tbxTelefono.Text)));

            RpviewComprobante.LocalReport.EnableExternalImages = true;
            RpviewComprobante.LocalReport.SetParameters(param);
            RpviewComprobante.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", table);
            RpviewComprobante.LocalReport.DataSources.Add(rds);

            string ident = !string.IsNullOrWhiteSpace(txtNumComp.Text) ? txtNumComp.Text + "_" : "_";
            ident += !string.IsNullOrWhiteSpace(ddlTipoComprobante.SelectedValue) ? ddlTipoComprobante.SelectedValue : string.Empty;

            RpviewComprobante.ServerReport.DisplayName = ident;
            RpviewComprobante.LocalReport.DisplayName = ident;
            RpviewComprobante.LocalReport.Refresh();

        }
    }

    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (RpviewComprobante.Visible == true)
        {
            var bytes = RpviewComprobante.LocalReport.Render("PDF");
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline;attachment; filename=Reporte.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush(); // send it to the client to download
            Response.Clear();

            //Response.OutputStream.Write(bytes, 0, bytes.Length);
            //Response.OutputStream.Flush();
            //Response.OutputStream.Close();
            //Response.Flush();
            //Response.Close();

            //MOSTRAR REPORTE EN PANTALLA
            //Warning[] warnings;
            //string[] streamids;
            //string mimeType;
            //string encoding;
            //string extension;
            //byte[] bytes = RpviewComprobante.LocalReport.Render("PDF", null, out mimeType,
            //               out encoding, out extension, out streamids, out warnings);
            //Usuario pUsuario = new Usuario();
            //string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_" + pUsuario.nombre : "";
            //FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + pNomUsuario + ".pdf"),
            //FileMode.Create);
            //fs.Write(bytes, 0, bytes.Length);
            //fs.Close();
            //Session["Archivo"] = Server.MapPath("output" + pNomUsuario + ".pdf");
            //frmPrint.Visible = true;
            //RpviewComprobante.Visible = false;

        }
    }



    /// <summary>
    /// Método para cuando se selecciona una entidad bancaria
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEntidadOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        LlenarCuenta();
        try
        {
            if (ddlCuenta.SelectedItem != null)
                AsignarNumeroCheque(ddlCuenta.SelectedItem.Text);
            return;
        }
        catch
        { }

    }

    /// <summary>
    /// Método para llenar las cuentas bancarias que posea la entidad según el banco seleccionado
    /// </summary>
    private void LlenarCuenta()
    {
        try
        {
            Int64 codbanco = 0;
            try
            {
                codbanco = Convert.ToInt64(ddlEntidadOrigen.SelectedValue);
            }
            catch
            {
            }
            ddlCuenta.Items.Clear();
            if (codbanco != 0)
            {
                Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
                Usuario usuario = (Usuario)Session["usuario"];
                ddlCuenta.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
                ddlCuenta.DataTextField = "num_cuenta";
                ddlCuenta.DataValueField = "idctabancaria";
                ddlCuenta.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlCuenta.DataBind();
            }
        }
        catch
        {
            VerError("...");
        }
    }

    protected void ddlCuenta_SelectedIndexChanged(object sender, EventArgs e)
    {
        AsignarNumeroCheque(ddlCuenta.SelectedItem.Text);
        return;
    }

    private void AsignarNumeroCheque(string snumcuenta)
    {
        if (snumcuenta.Trim() != "")
        {
            Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
            if (ddlFormaPago.SelectedValue == "2")
            {
                txtNumSop.Text = BancosService.soporte(snumcuenta, (Usuario)Session["Usuario"]);
                // Se añadido Consulta de la ruta de la tabla Bancos segun el numero de cuenta
                Session["Ruta_Cheque"] = BancosService.Ruta_Cheque(snumcuenta, (Usuario)Session["Usuario"]);
            }
        }
    }

    /// <summary>
    /// Método para cuando se selecciona la forma de pago
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDDLCuentas(false);
        ddlEntidadOrigen_SelectedIndexChanged(ddlEntidadOrigen, null);
        return;
    }

    /// <summary>
    /// Permite activar para la selección de Banco y Cuenta Bancaria para los Comprobantes de Egreso y Forma de Pago en CHEQUE.
    /// </summary>
    private void ActivarDDLCuentas(bool bTodo)
    {
        ActivarBancos(false);
        panelTransferencia.Visible = false;
        // Si el comprobante es de egreso activa campos para datos de la cuenta
        if (ddlTipoComprobante.SelectedValue == "1" || bTodo == true)
        {
            // Llenar drop down list de entidades bancarias. Solamente aquellas en donde la entidad tenga cuentas.
            Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
            Xpinn.Caja.Entities.Bancos Bancos = new Xpinn.Caja.Entities.Bancos();
            ddlEntidadOrigen.DataSource = BancosService.ListarBancos(Bancos, (Usuario)Session["Usuario"]);
            ddlEntidadOrigen.DataTextField = "nombrebanco";
            ddlEntidadOrigen.DataValueField = "cod_banco";
            ddlEntidadOrigen.DataBind();
            ddlEntidadOrigen.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            if (ddlFormaPago.SelectedValue != null && ddlFormaPago.SelectedValue != "")
            {
                if (Convert.ToInt64(ddlFormaPago.SelectedValue) == 2)
                {
                    ActivarBancos(true);
                    panelTransferencia.Visible = false;
                }
                else if (Convert.ToInt64(ddlFormaPago.SelectedValue) == 3)
                {
                    ActivarBancos(true);
                    panelTransferencia.Visible = true;
                }
            }
            return;
        }

        // Si el comprobante es de egreso activa campos para datos de la cuenta
        if (ddlTipoComprobante.SelectedValue == "5")
        {
            // Llenar drop down list de entidades bancarias. Solamente aquellas en donde la entidad tenga cuentas.
            Xpinn.Caja.Services.BancosService BancosService = new Xpinn.Caja.Services.BancosService();
            ddlEntidadOrigen.DataSource = BancosService.ListarBancosegre((Usuario)Session["Usuario"]);
            ddlEntidadOrigen.DataTextField = "nombrebanco";
            ddlEntidadOrigen.DataValueField = "cod_banco";
            ddlEntidadOrigen.DataBind();
            ddlEntidadOrigen.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            // Si es pago por cheque activar para seleccionar la cuenta
            if (ddlFormaPago.SelectedValue != null && ddlFormaPago.SelectedValue != "")
            {
                if (Convert.ToInt64(ddlFormaPago.SelectedValue) == 2)
                {
                    ActivarBancos(true);
                    panelTransferencia.Visible = false;
                }
                else if (Convert.ToInt64(ddlFormaPago.SelectedValue) == 3)
                {
                    ActivarBancos(true);
                    panelTransferencia.Visible = true;
                }
            }
        }

    }

    /// <summary>
    /// Método para cuando da error al generar el comprobante se pueda regresar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (Session["OrigenComprobante"] != null)
            Navegar(Session["OrigenComprobante"].ToString());
    }

    protected void ddlTipoComprobante_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        if (ddlTipoComprobante.SelectedValue != null)
        {
            if (ddlTipoComprobante.SelectedValue == "5")
                PanelFooter.Visible = true;
            else
                PanelFooter.Visible = false;

            Xpinn.Contabilidad.Services.TipoComprobanteService tipoCompServicio = new Xpinn.Contabilidad.Services.TipoComprobanteService();
            Xpinn.Contabilidad.Entities.TipoComprobante tipoComp = new Xpinn.Contabilidad.Entities.TipoComprobante();
            tipoComp = tipoCompServicio.ConsultarTipoComprobante(Convert.ToInt64(ddlTipoComprobante.SelectedValue), (Usuario)Session["usuario"]);
            if (tipoComp.tipo_norma == 1)
            {
                cbLocal.Checked = false;
                cbNif.Checked = true;
                cbAmbos.Checked = false;
            }
            else if (tipoComp.tipo_norma == 2)
            {
                cbLocal.Checked = false;
                cbNif.Checked = false;
                cbAmbos.Checked = true;
            }
            else
            {
                cbLocal.Checked = true;
                cbNif.Checked = false;
                cbAmbos.Checked = false;
            }
            if (cbLocal.Checked)
            {
                gvDetMovs.Columns[1].Visible = true;
                gvDetMovs.Columns[2].Visible = false;
            }
            if (cbNif.Checked)
            {
                gvDetMovs.Columns[1].Visible = false;
                gvDetMovs.Columns[2].Visible = true;
            }
            if (cbAmbos.Checked)
            {
                gvDetMovs.Columns[1].Visible = true;
                gvDetMovs.Columns[2].Visible = true;
            }


            #region Interactuar WorkManagement


            General parametroHabilitaOperacionesWM = ConsultarParametroGeneral(35);
            if (parametroHabilitaOperacionesWM != null && parametroHabilitaOperacionesWM.valor.Trim() == "1")
            {
                // 0 = Contable, 5 = Egreso
                string tipoComprobante = Request.QueryString["tipo_comp"] as string;
                bool creandoComprobante = string.IsNullOrWhiteSpace(idObjeto);

                if (creandoComprobante && !string.IsNullOrWhiteSpace(tipoComprobante))
                {
                    tipoComprobante = tipoComprobante.ToString().Trim();
                    if (tipoComprobante == "0") // Contable
                    {
                        General parametroTipoComprobanteIniciaWorkflowPagoProveedores = ConsultarParametroGeneral(66);
                        if (parametroTipoComprobanteIniciaWorkflowPagoProveedores != null && !string.IsNullOrWhiteSpace(parametroTipoComprobanteIniciaWorkflowPagoProveedores.valor))
                        {
                            int codigoTipoComprobante = 0;
                            int.TryParse(parametroTipoComprobanteIniciaWorkflowPagoProveedores.valor.Trim(), out codigoTipoComprobante);

                            int codigoTipoComprobanteSeleccionado = Convert.ToInt32(ddlTipoComprobante.SelectedValue);

                            // Si el parametro de tipo de comprobante es valido y mayor a cero y se haya seleccionado ese entonces se muestra para digitar la factura si no se oculta
                            // Ocultar / Mostrar factura
                            labelNumeroFactura.Visible = txtNumeroFactura.Visible = codigoTipoComprobante > 0 && codigoTipoComprobante == codigoTipoComprobanteSeleccionado;
                        }
                    }
                }
            }


            #endregion

            return;
        }
    }

    // Método para grabar datos del proceso contable
    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        // Determinar datos del usuario
        Usuario usuap = (Usuario)Session["usuario"];

        // Creando el proceso contable
        Xpinn.Contabilidad.Services.ProcesoContableService ProcesoService = new Xpinn.Contabilidad.Services.ProcesoContableService();
        Xpinn.Contabilidad.Entities.ProcesoContable eProceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
        if (!txtFechaIni.TieneDatos)
        {
            VerError("Debe especificar la fecha inicial");
            return;
        }
        if (!txtFechaFin.TieneDatos)
        {
            VerError("Debe especificar la fecha final");
            return;
        }
        try
        {
            eProceso.cod_proceso = 0;
            eProceso.tipo_ope = Convert.ToInt64(ddlTipoOperacion.SelectedValue);
            eProceso.tipo_comp = Convert.ToInt64(ddlTipoComp.SelectedValue);
            eProceso.fecha_inicial = txtFechaIni.ToDateTime;
            eProceso.fecha_final = txtFechaFin.ToDateTime;
            eProceso.concepto = Convert.ToInt64(ddlConcep.SelectedValue);
            eProceso.cod_cuenta = txtCodCuentaPC.Text;
            eProceso.cod_est_det = Convert.ToInt64(ddlEstructura.SelectedValue);
            if (ddlTipoMov.SelectedValue != "")
                eProceso.tipo_mov = Convert.ToInt32(ddlTipoMov.SelectedValue);
            else
                eProceso.tipo_mov = 0;
        }
        catch (Exception ex)
        {
            VerError("Se presento error al determinar datos del proceso." + ex.Message);
        }
        if (eProceso.tipo_comp == 0 || eProceso.tipo_comp == Int64.MinValue)
        {
            VerError("Debe especificar el tipo de comprobante");
            return;
        }

        try
        {
            eProceso = ProcesoService.CrearProcesoContable(eProceso, usuap);
        }
        catch (Exception ex)
        {
            VerError("Se presento error." + ex.Message);
            return;
        }

        // Ir a la pantalla de comprobantes
        PanelActiveView(vwPanel3);
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(true);

        // Determinar datos de la operación a generar
        Int64 pcod_ope = 0;
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] != null)
            pcod_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"]);
        Int64 ptip_ope = 0;
        if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] != null)
            ptip_ope = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"]);
        DateTime pfecha = System.DateTime.Now;
        if (Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] != null)
            pfecha = Convert.ToDateTime(Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"]);
        Int64 pcod_ofi = usuap.cod_oficina;
        if (Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] != null)
            pcod_ofi = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"]);
        Int64 ptipo_comp = 0;
        if (Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"] != null)
            ptipo_comp = Convert.ToInt64(Session[ComprobanteServicio.CodigoPrograma + ".tipo_comp"]);
        // Consultando el proceso de interfaz contable
        LstProcesoContable = ComprobanteServicio.ConsultaProceso(pcod_ope, ptip_ope, pfecha, (Usuario)Session["Usuario"]);

        // Generar el comprobante de la operación correspondiente
        GenerarComprobante(pcod_ope, ptip_ope, pfecha, ptipo_comp, pcod_ofi);
    }


    protected void ActivarBancos(Boolean bActiva)
    {
        mvBamco.Visible = bActiva;
        upEntidad.Visible = bActiva;
        ddlEntidadOrigen.Visible = bActiva;
        lblEntidad.Visible = bActiva;
        ddlCuenta.Visible = bActiva;
        lblCuenta.Visible = bActiva;
    }

    protected Boolean ObtenerDetalleCompOptimizado(bool bValidar)
    {
        VerError("");
        try
        {
            Boolean bModificar = false;
            List<DetalleComprobante> lstDetalleComprobante = new List<DetalleComprobante>();
            if (Session["DetalleComprobante"] != null)
            {
                bModificar = true;
                lstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
            }
            int contador = 0;
            foreach (GridViewRow rfila in gvDetMovs.Rows)
            {
                int rowIndex = -1;
                DetalleComprobante detalle = new DetalleComprobante();
                if (bModificar == true)
                {
                    rowIndex = (gvDetMovs.PageIndex * gvDetMovs.PageSize) + contador;
                    if (rowIndex < lstDetalleComprobante.Count)
                        detalle = lstDetalleComprobante[rowIndex];
                    else
                        detalle = null;
                }
                if (bModificar == false || (bModificar == true && detalle != null))
                {
                    TextBoxGrid txtCodCuenta = (TextBoxGrid)rfila.FindControl("txtCodCuenta");
                    if (txtCodCuenta != null)
                        if (txtCodCuenta.Text != null && txtCodCuenta.Text.ToString().Trim() != "")
                            detalle.cod_cuenta = txtCodCuenta.Text;
                        else
                            detalle.cod_cuenta = null;
                    else
                        detalle.cod_cuenta = null;

                    TextBoxGrid ddlNomCuenta = (TextBoxGrid)rfila.FindControl("ddlNomCuenta");
                    if (ddlNomCuenta != null)
                        detalle.nombre_cuenta = ddlNomCuenta.Text;

                    TextBoxGrid txtCodCuentaNIF = (TextBoxGrid)rfila.FindControl("txtCodCuentaNIF");
                    if (txtCodCuentaNIF != null)
                        if (txtCodCuentaNIF.Text.Trim() != "")
                            detalle.cod_cuenta_niif = txtCodCuentaNIF.Text;
                        else
                            detalle.cod_cuenta_niif = null;
                    else
                        detalle.cod_cuenta_niif = null;

                    TextBoxGrid txtNomCuentaNif = (TextBoxGrid)rfila.FindControl("txtNomCuentaNif");
                    if (txtNomCuentaNif != null)
                        detalle.nombre_cuenta_nif = txtNomCuentaNif.Text;

                    Label lblManejaTer = (Label)rfila.FindControl("lblManejaTer");
                    if (lblManejaTer != null && lblManejaTer.Text != "")
                        detalle.maneja_ter = Convert.ToInt64(lblManejaTer.Text);

                    Label lblImpuesto = (Label)rfila.FindControl("lblImpuesto");
                    if (lblImpuesto != null && lblImpuesto.Text != "")
                        detalle.impuesto = Convert.ToInt64(lblImpuesto.Text);

                    DropDownListGrid ddlMoneda = (DropDownListGrid)rfila.FindControl("ddlMoneda");
                    if (ddlMoneda != null)
                        detalle.moneda = Convert.ToInt64(ddlMoneda.SelectedItem.Value);
                    else
                        detalle.moneda = 1;
                    DropDownListGrid ddlCentroCosto = (DropDownListGrid)rfila.FindControl("ddlCentroCosto");
                    if (ddlCentroCosto != null)
                        if (ddlCentroCosto.SelectedItem != null && ddlCentroCosto.SelectedItem.ToString().Trim() != "")
                            detalle.centro_costo = Convert.ToInt64(ddlCentroCosto.SelectedItem.Value);
                        else
                            detalle.centro_costo = null;
                    DropDownListGrid ddlCentroGestion = (DropDownListGrid)rfila.FindControl("ddlCentroGestion");
                    if (ddlCentroGestion != null)
                        if (ddlCentroGestion.SelectedItem != null && ddlCentroGestion.SelectedItem.ToString().Trim() != "")
                            detalle.centro_gestion = Convert.ToInt64(ddlCentroGestion.SelectedItem.Value);
                        else
                            detalle.centro_gestion = null;
                    else
                        detalle.centro_gestion = null;
                    TextBox txtDetalle = (TextBox)rfila.FindControl("txtDetalle");
                    if (txtDetalle != null)
                        detalle.detalle = txtDetalle.Text;
                    DropDownListGrid ddlTipo = (DropDownListGrid)rfila.FindControl("ddlTipo");
                    if (ddlTipo != null)
                        detalle.tipo = ddlTipo.SelectedItem.Value;
                    TextBoxGrid txtValor = (TextBoxGrid)rfila.FindControl("txtValor");
                    if (txtValor != null)
                    {
                        if (txtValor.Text != "")
                            detalle.valor = Convert.ToDecimal(txtValor.Text.Replace(".", ""));
                        else
                            detalle.valor = null;
                    }
                    else
                        detalle.valor = null;
                    Label lblTercero = (Label)rfila.FindControl("lblTercero");
                    if (lblTercero != null)
                        if (lblTercero.Text.Trim() != "")
                            detalle.tercero = Convert.ToInt64(lblTercero.Text);
                        else
                            detalle.tercero = null;
                    else
                        detalle.tercero = null;
                    TextBox txtIdentificD = (TextBox)rfila.FindControl("txtIdentificD");
                    if (txtIdentificD != null)
                        detalle.identificacion = txtIdentificD.Text;
                    else
                        detalle.identificacion = "";
                    Label lblNombreTercero = (Label)rfila.FindControl("lblNombreTercero");
                    if (lblNombreTercero != null)
                        detalle.nom_tercero = lblNombreTercero.Text;
                    else
                        detalle.nom_tercero = null;

                    TextBox txtBaseGrid = (TextBox)rfila.FindControl("txtBaseGrid");
                    if (txtBaseGrid.Text != null)
                    {
                        if(detalle.base_comp<=0)
                        detalle.isVisible_base_comp = txtBaseGrid.Visible == true ? true : false;
                    }
                    else
                    {
                      //  detalle.base_comp = null;
                        detalle.isVisible_base_comp = false;
                    }
                    TextBox txtPorcentGrid = (TextBox)rfila.FindControl("txtPorcentGrid");
                    if (txtPorcentGrid != null)
                    {
                        if(detalle.porcentaje<=0)
                        detalle.isVisible_porcentaje = txtPorcentGrid.Visible == true ? true : false;
                    }
                    else
                    {
                       // detalle.porcentaje = null;
                        detalle.isVisible_porcentaje = false;
                    }
                    if (!(detalle.cod_cuenta == null && detalle.centro_costo == null && detalle.detalle == null && detalle.valor == null) || bValidar == false)
                    {
                        if (cbLocal.Checked)
                        {
                            if (detalle.cod_cuenta == null && bValidar == true)
                            {
                                VerError("Debe seleccionar la cuenta contable");
                                return false;
                            }
                        }
                        else if (cbNif.Checked)
                        {
                            if (detalle.cod_cuenta_niif == null && bValidar == true)
                            {
                                VerError("Debe seleccionar la cuenta contable");
                                return false;
                            }
                        }
                        else
                        {
                            if (detalle.cod_cuenta == null && bValidar == true)
                            {
                                VerError("Debe seleccionar la cuenta contable");
                                return false;
                            }
                            if (detalle.cod_cuenta_niif == null && bValidar == true)
                            {
                                VerError("Debe seleccionar la cuenta contable");
                                return false;
                            }
                        }

                        if (detalle.centro_costo == null && bValidar == true)
                        {
                            VerError("Debe seleccionar el centro de costo");
                            return false;
                        }
                        if (detalle.detalle == null && bValidar == true)
                        {
                            VerError("Debe ingresar el detalle");
                            return false;
                        }
                        if ((detalle.valor == null || detalle.valor == 0) && bValidar == true)
                        {
                            VerError("Debe ingresar el valor");
                            return false;
                        }

                        if (bModificar == false)
                            lstDetalleComprobante.Add(detalle);
                    }
                }
                contador += 1;
            }
            Session["DetalleComprobante"] = lstDetalleComprobante;
            return true;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return false;
        }
    }

    protected Boolean ObtenerDetalleComprobante(bool bValidar)
    {
        VerError("");
        try
        {
            Boolean bModificar = false;
            List<DetalleComprobante> lstDetalleComprobante = new List<DetalleComprobante>();
            if (Session["DetalleComprobante"] != null)
            {
                bModificar = true;
                lstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
            }
            int contador = 0;
            foreach (GridViewRow rfila in gvDetMovs.Rows)
            {
                int rowIndex = -1;
                DetalleComprobante detalle = new DetalleComprobante();
                if (bModificar == true)
                {
                    rowIndex = (gvDetMovs.PageIndex * gvDetMovs.PageSize) + contador;
                    if (rowIndex < lstDetalleComprobante.Count)
                        detalle = lstDetalleComprobante[rowIndex];
                    else
                        detalle = null;
                }
                if (bModificar == false || (bModificar == true && detalle != null))
                {
                    TextBoxGrid txtCodCuenta = (TextBoxGrid)rfila.FindControl("txtCodCuenta");
                    if (txtCodCuenta != null)
                        if (txtCodCuenta.Text != null && txtCodCuenta.Text.ToString().Trim() != "")
                            detalle.cod_cuenta = txtCodCuenta.Text;
                        else
                            detalle.cod_cuenta = null;
                    else
                        detalle.cod_cuenta = null;

                    if (cbLocal.Checked)
                    {
                        if (txtCodCuenta.Text.Trim() != "")
                        {
                            txtCodCuenta_TextChanged(txtCodCuenta, null);
                        }
                    }

                    TextBoxGrid ddlNomCuenta = (TextBoxGrid)rfila.FindControl("ddlNomCuenta");
                    if (ddlNomCuenta != null)
                        detalle.nombre_cuenta = ddlNomCuenta.Text;

                    TextBoxGrid txtCodCuentaNIF = (TextBoxGrid)rfila.FindControl("txtCodCuentaNIF");
                    if (txtCodCuentaNIF != null)
                        if (txtCodCuentaNIF.Text.Trim() != "")
                            detalle.cod_cuenta_niif = txtCodCuentaNIF.Text;
                        else
                            detalle.cod_cuenta_niif = null;
                    else
                        detalle.cod_cuenta_niif = null;

                    if (cbNif.Checked)
                    {
                        if (txtCodCuentaNIF.Text.Trim() != "")
                        {
                            txtCodCuentaNIF_TextChanged(txtCodCuentaNIF, null);
                        }
                    }
                    else if (cbAmbos.Checked)
                    {
                        if (txtCodCuenta.Text.Trim() != "") txtCodCuenta_TextChanged(txtCodCuenta, null);
                        if (txtCodCuentaNIF.Text.Trim() != "") txtCodCuentaNIF_TextChanged(txtCodCuentaNIF, null);
                    }

                    TextBoxGrid txtNomCuentaNif = (TextBoxGrid)rfila.FindControl("txtNomCuentaNif");
                    if (txtNomCuentaNif != null)
                        detalle.nombre_cuenta_nif = txtNomCuentaNif.Text;

                    Label lblManejaTer = (Label)rfila.FindControl("lblManejaTer");
                    if (lblManejaTer != null && lblManejaTer.Text != "")
                        detalle.maneja_ter = Convert.ToInt64(lblManejaTer.Text);

                    Label lblImpuesto = (Label)rfila.FindControl("lblImpuesto");
                    if (lblImpuesto != null && lblImpuesto.Text != "")
                        detalle.impuesto = Convert.ToInt64(lblImpuesto.Text);

                    DropDownListGrid ddlMoneda = (DropDownListGrid)rfila.FindControl("ddlMoneda");
                    if (ddlMoneda != null)
                        detalle.moneda = Convert.ToInt64(ddlMoneda.SelectedItem.Value);
                    else
                        detalle.moneda = 1;
                    DropDownListGrid ddlCentroCosto = (DropDownListGrid)rfila.FindControl("ddlCentroCosto");
                    if (ddlCentroCosto != null)
                        if (ddlCentroCosto.SelectedItem != null && ddlCentroCosto.SelectedItem.ToString().Trim() != "")
                            detalle.centro_costo = Convert.ToInt64(ddlCentroCosto.SelectedItem.Value);
                        else
                            detalle.centro_costo = null;
                    DropDownListGrid ddlCentroGestion = (DropDownListGrid)rfila.FindControl("ddlCentroGestion");
                    if (ddlCentroGestion != null)
                        if (ddlCentroGestion.SelectedItem != null && ddlCentroGestion.SelectedItem.ToString().Trim() != "")
                            detalle.centro_gestion = Convert.ToInt64(ddlCentroGestion.SelectedItem.Value);
                        else
                            detalle.centro_gestion = null;
                    else
                        detalle.centro_gestion = null;
                    TextBox txtDetalle = (TextBox)rfila.FindControl("txtDetalle");
                    if (txtDetalle != null)
                        detalle.detalle = txtDetalle.Text;
                    DropDownListGrid ddlTipo = (DropDownListGrid)rfila.FindControl("ddlTipo");
                    if (ddlTipo != null)
                        detalle.tipo = ddlTipo.SelectedItem.Value;
                    TextBoxGrid txtValor = (TextBoxGrid)rfila.FindControl("txtValor");
                    if (txtValor != null)
                    {
                        if (txtValor.Text != "")
                            detalle.valor = Convert.ToDecimal(txtValor.Text.Replace(".", ""));
                        else
                            detalle.valor = null;
                    }
                    else
                        detalle.valor = null;
                    Label lblTercero = (Label)rfila.FindControl("lblTercero");
                    if (lblTercero != null)
                        if (lblTercero.Text.Trim() != "")
                            detalle.tercero = Convert.ToInt64(lblTercero.Text);
                        else
                            detalle.tercero = null;
                    else
                        detalle.tercero = null;
                    TextBox txtIdentificD = (TextBox)rfila.FindControl("txtIdentificD");
                    if (txtIdentificD != null)
                        detalle.identificacion = txtIdentificD.Text;
                    else
                        detalle.identificacion = "";
                    Label lblNombreTercero = (Label)rfila.FindControl("lblNombreTercero");
                    if (lblNombreTercero != null)
                        detalle.nom_tercero = lblNombreTercero.Text;
                    else
                        detalle.nom_tercero = null;

                    TextBox txtBaseGrid = (TextBox)rfila.FindControl("txtBaseGrid");
                    if (txtBaseGrid.Text != null)
                        if (txtBaseGrid.Text.Trim() != "")
                            if (detalle.base_comp <= 0)
                                detalle.base_comp = Convert.ToDecimal(txtBaseGrid.Text);                      
                   
                    TextBox txtPorcentGrid = (TextBox)rfila.FindControl("txtPorcentGrid");
                    if (txtPorcentGrid != null)
                        if (txtPorcentGrid.Text.Trim() != "")
                            if (detalle.porcentaje <= 0)                                   
                            detalle.porcentaje = Convert.ToDecimal(txtPorcentGrid.Text);     
                           
                    

                    if (!(detalle.cod_cuenta == null && detalle.centro_costo == null && detalle.detalle == null && detalle.valor == null) || bValidar == false)
                    {
                        if (cbLocal.Checked)
                        {
                            if (detalle.cod_cuenta == null && bValidar == true)
                            {
                                VerError("Debe seleccionar la cuenta contable");
                                return false;
                            }
                        }
                        else if (cbNif.Checked)
                        {
                            if (detalle.cod_cuenta_niif == null && bValidar == true)
                            {
                                VerError("Debe seleccionar la cuenta contable");
                                return false;
                            }
                        }
                        else
                        {
                            if (detalle.cod_cuenta == null && bValidar == true)
                            {
                                VerError("Debe seleccionar la cuenta contable");
                                return false;
                            }
                            if (detalle.cod_cuenta_niif == null && bValidar == true)
                            {
                                VerError("Debe seleccionar la cuenta contable");
                                return false;
                            }
                        }

                        if (detalle.centro_costo == null && bValidar == true)
                        {
                            VerError("Debe seleccionar el centro de costo");
                            return false;
                        }
                        if (detalle.detalle == null && bValidar == true)
                        {
                            VerError("Debe ingresar el detalle");
                            return false;
                        }
                        if ((detalle.valor == null || detalle.valor == 0) && bValidar == true)
                        {
                            VerError("Debe ingresar el valor");
                            return false;
                        }

                        if (bModificar == false)
                            lstDetalleComprobante.Add(detalle);
                    }
                }
                contador += 1;
            }
            Session["DetalleComprobante"] = lstDetalleComprobante;
            return true;
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
            return false;
        }
    }

    //protected void txtValor_TextChanged(object sender, EventArgs e)
    //{
    //    ObtenerDetalleComprobante(false);
    //    CalcularTotal();
    //}

    /// <summary>
    /// Botón para adicionar un detalle
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        //ObtenerDetalleComprobante(false);
        ObtenerDetalleCompOptimizado(false);
        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];
        for (int i = 1; i <= 1; i++)
        {
            DetalleComprobante pDetalleComprobante = new DetalleComprobante();
            pDetalleComprobante.codigo = -1;
            if (txtNumComp.Text != "")
                pDetalleComprobante.num_comp = Convert.ToInt64(txtNumComp.Text);
            if (ddlTipoComprobante.SelectedValue != null)
                pDetalleComprobante.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue.ToString());
            pDetalleComprobante.cod_cuenta = "";
            pDetalleComprobante.nombre_cuenta = "";
            pDetalleComprobante.centro_costo = null;
            pDetalleComprobante.centro_gestion = null;
            pDetalleComprobante.valor = null;
            pDetalleComprobante.tercero = null;
            pDetalleComprobante.moneda = 1;
            pDetalleComprobante.tipo = "D";
            pDetalleComprobante.identificacion = "";
            pDetalleComprobante.nom_tercero = "";
            pDetalleComprobante.base_comp = null;
            pDetalleComprobante.porcentaje = null;

            LstDetalleComprobante.Add(pDetalleComprobante);
        }
        gvDetMovs.PageIndex = gvDetMovs.PageCount;
        gvDetMovs.DataSource = LstDetalleComprobante;
        gvDetMovs.DataBind();
        Session["detalle"] = "1";
        Session["DetalleComprobante"] = LstDetalleComprobante;
    }

    protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlTipo = (DropDownListGrid)sender;
        int rowIndex = Convert.ToInt32(ddlTipo.CommandArgument);

        ObtenerDetalleCompOptimizado(false);
        CalcularTotal();
        //focoEdicion(rowIndex, gvDetMovs, "ddlTipo", "2doModoFiltro");
        return;
    }

    protected void RegsPag_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerDetalleCompOptimizado(false);
        DropDownList _DropDownList = (DropDownList)sender;
        this.gvDetMovs.PageSize = int.Parse(_DropDownList.SelectedValue);
        ActualizarDetalle();
        return;
    }
    List<DetalleComprobante> ObtenerListaGridView()
    {
        List<DetalleComprobante> lista = new List<DetalleComprobante>();

        foreach (GridViewRow rfila in gvDetMovs.Rows)
        {
            DetalleComprobante oper = new DetalleComprobante();

            TextBox txtCodCuenta = (TextBox)rfila.FindControl("txtCodCuenta");
            if (txtCodCuenta != null)
                if (txtCodCuenta.Visible) { oper.cod_cuenta = Convert.ToString(txtCodCuenta.Text); }

            TextBox ddlNomCuenta = (TextBox)rfila.FindControl("ddlNomCuenta");
            if (ddlNomCuenta != null)
                if (ddlNomCuenta.Visible) { oper.nombre_cuenta = Convert.ToString(ddlNomCuenta.Text); }

            TextBox txtCodCuentaNIF = (TextBox)rfila.FindControl("txtCodCuentaNIF");
            if (txtCodCuentaNIF != null)
                if (txtCodCuentaNIF.Visible) { oper.cod_cuenta_niif = Convert.ToString(txtCodCuentaNIF.Text); }

            TextBox txtNomCuentaNif = (TextBox)rfila.FindControl("txtNomCuentaNif");
            if (txtNomCuentaNif != null)
                if (txtNomCuentaNif.Visible) { oper.nombre_cuenta_nif = Convert.ToString(txtNomCuentaNif.Text); }

            DropDownListGrid ddlCentroCosto = (DropDownListGrid)rfila.FindControl("ddlCentroCosto");
            if (ddlCentroCosto.SelectedValue != null)
                oper.centro_costo = Convert.ToInt32(ddlCentroCosto.SelectedValue);

            DropDownListGrid ddlCentroGestion = (DropDownListGrid)rfila.FindControl("ddlCentroGestion");
            if (ddlCentroGestion.SelectedValue != null && ddlCentroGestion.SelectedValue != "")
                oper.centro_gestion = Convert.ToInt32(ddlCentroGestion.SelectedValue);

            TextBox txtDetalle = (TextBox)rfila.FindControl("txtDetalle");
            if (txtDetalle != null)
                if (txtDetalle.Visible) { oper.detalle = Convert.ToString(txtDetalle.Text); }

            DropDownListGrid ddlTipo = (DropDownListGrid)rfila.FindControl("ddlTipo");
            if (ddlTipo.SelectedValue != null)
                oper.tipo = Convert.ToString(ddlTipo.SelectedValue);

            TextBox txtValor = (TextBox)rfila.FindControl("txtValor");
            if (txtValor.Text != null && txtValor.Text != "")
                if (txtValor.Visible) { oper.valor = Convert.ToDecimal(txtValor.Text); }

            DropDownListGrid ddlMoneda = (DropDownListGrid)rfila.FindControl("ddlMoneda");
            if (ddlMoneda.SelectedValue != null && ddlMoneda.SelectedValue != "")
                oper.moneda = Convert.ToInt32(ddlMoneda.SelectedValue);

            Label lblTercero = (Label)rfila.FindControl("lblTercero");
            if (lblTercero != null && !string.IsNullOrWhiteSpace(lblTercero.Text))
                oper.tercero = Convert.ToInt32(lblTercero.Text);

            TextBox txtIdentificD = (TextBox)rfila.FindControl("txtIdentificD");
            if (txtIdentificD != null)
                if (txtIdentificD.Visible) { oper.identificacion = Convert.ToString(txtIdentificD.Text); }

            Label lblNombreTercero = (Label)rfila.FindControl("lblNombreTercero");
            if (lblNombreTercero != null && !string.IsNullOrWhiteSpace(lblTercero.Text))
                oper.nom_tercero = Convert.ToString(lblNombreTercero.Text);

            TextBox txtBaseGrid = (TextBox)rfila.FindControl("txtBaseGrid");
            if (txtBaseGrid != null)
                if (txtBaseGrid.Visible) { oper.base_comp = Convert.ToInt32(txtBaseGrid.Text); }

            TextBox txtPorcentGrid = (TextBox)rfila.FindControl("txtPorcentGrid");
            if (txtPorcentGrid != null)
                if (txtPorcentGrid.Visible) { oper.porcentaje = Convert.ToInt32(txtPorcentGrid.Text); }

            Label lblCodTipoProducto = (Label)rfila.FindControl("lblCodTipoProducto");
            if (lblCodTipoProducto != null && !string.IsNullOrWhiteSpace(lblCodTipoProducto.Text))
                oper.cod_tipo_producto = Convert.ToInt32(lblCodTipoProducto.Text);

            Label lblNumeroTransaccion = (Label)rfila.FindControl("lblNumeroTransaccion");
            if (lblNumeroTransaccion != null && !string.IsNullOrWhiteSpace(lblNumeroTransaccion.Text))
                oper.numero_transaccion = Convert.ToString(lblNumeroTransaccion.Text);

            lista.Add(oper);
        }

        return lista;
    }
    protected void IraPag(object sender, EventArgs e)
    {
        TextBox _IraPag = (TextBox)sender;

        int _NumPag = 0;

        if (int.TryParse(_IraPag.Text.Trim(), out _NumPag) && _NumPag > 0 && _NumPag <= this.gvDetMovs.PageCount)
        {
            if (int.TryParse(_IraPag.Text.Trim(), out _NumPag) && _NumPag > 0 && _NumPag <= this.gvDetMovs.PageCount)
            {
                // gvAvances.PageIndex = Convert.ToInt32(_IraPag.Text);
                this.gvDetMovs.PageIndex = _NumPag - 1;
                //BtnGuardarDatos_Click(null, null);
                gvDetMovs.DataBind();
            }
            else
            {
                this.gvDetMovs.PageIndex = 0;
            }
        }
        this.gvDetMovs.SelectedIndex = -1;
        //return;
    }
    protected void btnTxt(object sender, EventArgs e)
    {
        TextBox _IraPag = (TextBox)sender;

        int _NumPag = 0;

        if (int.TryParse(_IraPag.Text.Trim(), out _NumPag) && _NumPag > 0 && _NumPag <= this.gvDetMovs.PageCount)
        {
            if (int.TryParse(_IraPag.Text.Trim(), out _NumPag) && _NumPag > 0 && _NumPag <= this.gvDetMovs.PageCount)
            {
                // gvAvances.PageIndex = Convert.ToInt32(_IraPag.Text);
                this.gvDetMovs.PageIndex = _NumPag - 1;
                //BtnGuardarDatos_Click(null, null);
                gvDetMovs.DataBind();
            }
            else
            {
                this.gvDetMovs.PageIndex = 0;
            }
        }
        this.gvDetMovs.SelectedIndex = -1;
    }

    protected void btnRegresarComp_Click(object sender, EventArgs e)
    {
        PanelActiveView(vwPanel1);
    }
    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "txtCodigo", "txtIdentificacion", "ddlTipoIdentificacion", "txtNombres");
    }

    protected void btnListadoPlan_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlan = (ButtonGrid)sender;
        if (btnListadoPlan != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlan.CommandArgument);
            ctlPlanCuentas ctlListadoPlan = (ctlPlanCuentas)gvDetMovs.Rows[rowIndex].FindControl("ctlListadoPlan");
            TextBoxGrid txtCodCuenta = (TextBoxGrid)gvDetMovs.Rows[rowIndex].FindControl("txtCodCuenta");
            TextBoxGrid ddlNomCuenta = (TextBoxGrid)gvDetMovs.Rows[rowIndex].FindControl("ddlNomCuenta");
            ctlListadoPlan.Motrar(true, "txtCodCuenta", "ddlNomCuenta");
            txtCodCuenta_TextChanged(txtCodCuenta, null);
        }

        return;
    }

    protected void btnListadoPlanNIF_Click(object sender, EventArgs e)
    {
        ButtonGrid btnListadoPlanNif = (ButtonGrid)sender;
        if (btnListadoPlanNif != null)
        {
            int rowIndex = Convert.ToInt32(btnListadoPlanNif.CommandArgument);
            ctlPlanCuentasNif ctlListadoPlan = (ctlPlanCuentasNif)gvDetMovs.Rows[rowIndex].FindControl("ctlListadoPlanNif");
            TextBoxGrid txtCodCuentaNIF = (TextBoxGrid)gvDetMovs.Rows[rowIndex].FindControl("txtCodCuentaNIF");
            TextBoxGrid txtNomCuentaNif = (TextBoxGrid)gvDetMovs.Rows[rowIndex].FindControl("txtNomCuentaNif");
            ctlListadoPlan.Motrar(true, "txtCodCuentaNIF", "txtNomCuentaNif");
            txtCodCuentaNIF_TextChanged(txtCodCuentaNIF, null);
        }

        return;
    }


    private void focoEdicion(int indiceFila, GridView grilla, string control, string Opcion)
    {
        int rowIndex = -1;

        if (Opcion == "") //CAPTURAR INDICE MODO Container.DataItemIndex
            rowIndex = indiceFila - (gvDetMovs.PageIndex * gvDetMovs.PageSize);
        else // 2do MODO ((GridViewRow) Container).RowIndex
            rowIndex = indiceFila;
        //try
        //{
        //    GridViewRow fila = grilla.Rows[rowIndex];
        //    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //    if(scriptManager != null )scriptManager.SetFocus((fila.FindControl(control)));
        //}
        //catch
        //{ }
    }

    protected void ddlCentroCosto_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlCentroCosto = (DropDownListGrid)sender;
        int rowIndex = Convert.ToInt32(ddlCentroCosto.CommandArgument);
        focoEdicion(rowIndex, gvDetMovs, "ddlCentroGestion", "");
    }

    protected void ddlCentroGestion_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlCentroGestion = (DropDownListGrid)sender;
        int rowIndex = Convert.ToInt32(ddlCentroGestion.CommandArgument);
        focoEdicion(rowIndex, gvDetMovs, "txtDetalle", "");
    }

    protected void ddlMoneda_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListGrid ddlMoneda = (DropDownListGrid)sender;
        int rowIndex = Convert.ToInt32(ddlMoneda.CommandArgument);
        focoEdicion(rowIndex, gvDetMovs, "ddlMoneda", "");
    }




    protected void gvImpuestos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipoImpuesto = (DropDownListGrid)e.Row.FindControl("ddlTipoImpuesto");
            if (ddlTipoImpuesto != null)
            {
                List<Xpinn.Contabilidad.Entities.PlanCuentas> lstTipo = new List<Xpinn.Contabilidad.Entities.PlanCuentas>();
                Xpinn.Contabilidad.Entities.PlanCuentas vData = new Xpinn.Contabilidad.Entities.PlanCuentas();
                lstTipo = PlanCuentasServicio.ListarTipoImpuesto(vData, (Usuario)Session["usuario"]);

                if (ddlTipoImpuesto != null)
                    if (lstTipo.Count > 0)
                    {
                        ddlTipoImpuesto.DataSource = lstTipo;
                        ddlTipoImpuesto.DataTextField = "nombre_impuesto";
                        ddlTipoImpuesto.DataValueField = "cod_tipo_impuesto";
                        ddlTipoImpuesto.Items.Insert(0, new ListItem("Seleccione un item", "-1"));
                        ddlTipoImpuesto.SelectedIndex = 0;
                        ddlTipoImpuesto.DataBind();
                    }

                Label lblTipoImpuesto = (Label)e.Row.FindControl("lblTipoImpuesto");
                if (lblTipoImpuesto != null)
                    ddlTipoImpuesto.SelectedValue = lblTipoImpuesto.Text;
            }

            if (Session["VALOR_COMP"] != null)
            {
                TextBoxGrid txtBase = (TextBoxGrid)e.Row.FindControl("txtBase");
                if (txtBase != null)
                    txtBase.Text = Session["VALOR_COMP"].ToString();

                TextBox txtPorcentajeImpuesto = (TextBox)e.Row.FindControl("txtPorcentajeImpuesto");
                decimales txtValorImp = (decimales)e.Row.FindControl("txtValorImp");
                if (txtPorcentajeImpuesto.Text != "")
                {
                    decimal base_comp, porcent;
                    base_comp = Convert.ToDecimal(txtBase.Text);
                    porcent = Convert.ToDecimal(txtPorcentajeImpuesto.Text);
                    txtValorImp.Text = (base_comp * (porcent / 100)).ToString("N0");
                }
            }
        }

        return;
    }


    protected void gvImpuestos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvImpuestos.DataKeys[e.RowIndex].Values[0].ToString());

        if (conseID != 0)
        {
            try
            {
                List<PlanCuentasImpuesto> LstDetallePlan = new List<PlanCuentasImpuesto>();
                LstDetallePlan = (List<PlanCuentasImpuesto>)Session["DetallePlanCuentas"];

                LstDetallePlan.RemoveAt((gvImpuestos.PageIndex * gvImpuestos.PageSize) + e.RowIndex);
                Session["DetallePlanCuentas"] = LstDetallePlan;

                gvImpuestos.DataSourceID = null;
                gvImpuestos.DataBind();
                gvImpuestos.DataSource = LstDetallePlan;
                gvImpuestos.DataBind();
                mpeImpuestos.Show();
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(this.ComprobanteServicio.numero_comp + "L", "gvDetMovs_RowDeleting", ex);
            }
        }
        else
        {
            e.Cancel = true;
        }

        return;

    }

    protected void btnGenerarImpuesto_Click(object sender, EventArgs e)
    {
        Lblerror.Text = "";
        ButtonGrid btnGenerarImpuesto = (ButtonGrid)sender;
        int rowIndex = Convert.ToInt32(btnGenerarImpuesto.CommandArgument);

        foreach (GridViewRow rFila in gvDetMovs.Rows)
        {
            if (rFila.RowIndex == rowIndex)
            {
                TextBoxGrid txtCodCuenta_impuesto = (TextBoxGrid)rFila.FindControl("txtCodCuenta_imps");
                CheckBox chkasumido = (CheckBox)rFila.FindControl("cbAsumido");
                TextBoxGrid txtCodCuenta = (TextBoxGrid)rFila.FindControl("txtCodCuenta");
                TextBoxGrid txtValor = (TextBoxGrid)rFila.FindControl("txtValor");
                DropDownListGrid ddlCentroCosto = (DropDownListGrid)rFila.FindControl("ddlCentroCosto");

                if (txtValor != null && txtValor.Text != "0" && txtValor.Text != "")
                {
                    //CARGAR VALORES DE LA CUENTA CORRESPONDIENTE

                    List<PlanCuentasImpuesto> lstImpuestosXCuenta = new List<PlanCuentasImpuesto>();
                    Xpinn.Contabilidad.Services.PlanCuentasImpuestoService ImpuestoService = new Xpinn.Contabilidad.Services.PlanCuentasImpuestoService();
                    PlanCuentasImpuesto pDatos = new PlanCuentasImpuesto();
                    if (txtCodCuenta.Text != "")
                    {
                        gvImpuestos.DataSource = null;
                        string filtro = " WHERE COD_CUENTA = '" + txtCodCuenta.Text + "'";
                        lstImpuestosXCuenta = ImpuestoService.ListarPlanCuentasImpuesto(pDatos, filtro, (Usuario)Session["usuario"]);
                        if (lstImpuestosXCuenta.Count > 0)
                        {
                            gvImpuestos.Visible = true;
                            lblInfo.Visible = false;
                            Session["INDICEADD"] = rowIndex;
                            Session["CC"] = ddlCentroCosto.SelectedIndex != 0 ? ddlCentroCosto.SelectedValue : null;
                            Session["VALOR_COMP"] = txtValor.Text;
                            gvImpuestos.DataSource = lstImpuestosXCuenta;
                            gvImpuestos.DataBind();
                            Session["DetallePlanCuentas"] = lstImpuestosXCuenta;
                            btnGenerarDa.Enabled = true;
                        }
                        else
                        {
                            gvImpuestos.Visible = false;
                            lblInfo.Visible = true;
                            Session["INDICEADD"] = null;
                            Session["VALOR_COMP"] = null;
                            Session["CC"] = null;
                            Session["DetallePlanCuentas"] = null;
                            btnGenerarDa.Enabled = false;
                        }
                        mpeImpuestos.Show();
                    }
                }
                else
                {
                    Lblerror.Text = "Debe Ingresar el valor para la cuenta " + txtCodCuenta.Text;
                    break;
                }
                break;
            }
        }

        return;
    }

    protected void btnCerrarVent_Click(object sender, EventArgs e)
    {
        mpeImpuestos.Hide();
    }

    protected void btnGenerarDa_Click(object sender, EventArgs e)
    {
        ObtenerDetalleComprobante(false);

        List<DetalleComprobante> LstDetalleGeneral = new List<DetalleComprobante>();

        List<DetalleComprobante> LstDetalleComprobante = new List<DetalleComprobante>();
        LstDetalleComprobante = (List<DetalleComprobante>)Session["DetalleComprobante"];

        int cont = 0;
        foreach (DetalleComprobante Detalle in LstDetalleComprobante)
        {
            LstDetalleGeneral.Add(Detalle);
            //PONER ESTA PARTE QUESE QUITO AL FINAL
            if (cont == Convert.ToInt32(Session["INDICEADD"].ToString()))
            {
                if (gvImpuestos.Rows.Count > 0)
                {
                    foreach (GridViewRow rFila in gvImpuestos.Rows)
                    {
                        // Determinar los datos del impuesto
                        string nom_tipoImpuesto = "", cod_cuentaimp = "", tipo_cuentaimp = "";
                        decimal valorGrilla = 0, base_comp = 0, porcentaje = 0;
                        DropDownListGrid ddlTipoImpuesto = (DropDownListGrid)rFila.FindControl("ddlTipoImpuesto");
                        if (ddlTipoImpuesto != null)
                            nom_tipoImpuesto = ddlTipoImpuesto.SelectedItem.Text;

                        TextBoxGrid txtBase = (TextBoxGrid)rFila.FindControl("txtBase");
                        if (txtBase.Text != "" && txtBase != null && txtBase.Text != "0")
                            base_comp = Convert.ToDecimal(txtBase.Text);

                        TextBox txtPorcentajeImpuesto = (TextBox)rFila.FindControl("txtPorcentajeImpuesto");
                        if (txtPorcentajeImpuesto.Text != "" && txtPorcentajeImpuesto != null)
                            porcentaje = Convert.ToDecimal(txtPorcentajeImpuesto.Text);

                        TextBoxGrid txtCodCuenta_imp = (TextBoxGrid)rFila.FindControl("txtCodCuenta_imp");
                        if (txtCodCuenta_imp.Text != "" && txtCodCuenta_imp != null)
                        {
                            cod_cuentaimp = txtCodCuenta_imp.Text;
                        }

                        decimales txtValorImp = (decimales)rFila.FindControl("txtValorImp");
                        if (txtValorImp != null && txtValorImp.Text != "")
                            valorGrilla = Convert.ToDecimal(txtValorImp.Text);

                        // Consultar datos de la cuenta contable de impuestos
                        Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
                        PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta_imp.Text, (Usuario)Session["usuario"]);

                        // Generar registro contable del impuesto
                        DetalleComprobante pDetalleComprobante = new DetalleComprobante();
                        pDetalleComprobante.codigo = -1;
                        if (txtNumComp.Text != "")
                            pDetalleComprobante.num_comp = Convert.ToInt64(txtNumComp.Text);
                        if (ddlTipoComprobante.SelectedValue != null)
                            pDetalleComprobante.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue.ToString());
                        pDetalleComprobante.cod_cuenta = cod_cuentaimp;
                        pDetalleComprobante.detalle = nom_tipoImpuesto;
                        pDetalleComprobante.valor = valorGrilla;
                        pDetalleComprobante.nombre_cuenta = PlanCuentas.nombre;
                        pDetalleComprobante.cod_cuenta_niif = PlanCuentas.cod_cuenta_niif;
                        pDetalleComprobante.nombre_cuenta_nif = PlanCuentas.nombre_niif;
                        if (Session["CC"] != null)
                            pDetalleComprobante.centro_costo = Convert.ToInt64(Session["CC"].ToString());
                        else
                            pDetalleComprobante.centro_costo = null;
                        pDetalleComprobante.centro_gestion = null;
                        pDetalleComprobante.tercero = null;
                        pDetalleComprobante.moneda = 1;
                        pDetalleComprobante.tipo = PlanCuentas.tipo;
                        tipo_cuentaimp = PlanCuentas.tipo;
                        pDetalleComprobante.identificacion = "";
                        pDetalleComprobante.nom_tercero = "";
                        PlanCuentas.es_impuesto = PlanCuentas.es_impuesto != null ? PlanCuentas.es_impuesto : "0";
                        if (Convert.ToInt32(PlanCuentas.es_impuesto) >= 1)
                        {
                            pDetalleComprobante.base_comp = Convert.ToDecimal(base_comp);
                            pDetalleComprobante.porcentaje = Convert.ToDecimal(porcentaje);
                        }
                        else
                        {
                            pDetalleComprobante.base_comp = null;
                            pDetalleComprobante.porcentaje = null;
                        }
                        if (txtBase.Text != "" && txtBase != null && txtBase.Text != "0")
                            LstDetalleGeneral.Add(pDetalleComprobante);

                        // Si el impuesto es asumido entonces generar el registro contable respectivo de contrapartida
                        CheckBox cvasumir = (CheckBox)rFila.FindControl("cbAsumido");
                        if (cvasumir.Checked == true)
                        {
                            TextBoxGrid txtCodCuenta_imps = (TextBoxGrid)rFila.FindControl("txtCodCuenta_imps");
                            if (txtCodCuenta_imps.Text != "" && txtCodCuenta_imps != null)
                            {
                                cod_cuentaimp = txtCodCuenta_imps.Text;
                            }
                            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentass = new Xpinn.Contabilidad.Entities.PlanCuentas();
                            PlanCuentass = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuenta_imp.Text, (Usuario)Session["usuario"]);

                            DetalleComprobante pDetalleComprobantes = new DetalleComprobante();
                            pDetalleComprobantes.codigo = -1;
                            if (txtNumComp.Text != "")
                                pDetalleComprobantes.num_comp = Convert.ToInt64(txtNumComp.Text);
                            if (ddlTipoComprobante.SelectedValue != null)
                                pDetalleComprobantes.tipo_comp = Convert.ToInt64(ddlTipoComprobante.SelectedValue.ToString());
                            pDetalleComprobantes.cod_cuenta = cod_cuentaimp;
                            pDetalleComprobantes.detalle = nom_tipoImpuesto;
                            pDetalleComprobantes.valor = valorGrilla;
                            pDetalleComprobantes.tipo = PlanCuentas.tipo;
                            if (Session["CC"] != null)
                                pDetalleComprobantes.centro_costo = Convert.ToInt64(Session["CC"].ToString());
                            else
                                pDetalleComprobantes.centro_costo = null;
                            pDetalleComprobantes.centro_gestion = null;
                            pDetalleComprobantes.tercero = null;
                            pDetalleComprobantes.moneda = 1;
                            if (tipo_cuentaimp.ToUpper() == "C")
                                pDetalleComprobantes.tipo = "D";
                            else
                                pDetalleComprobantes.tipo = "C";
                            pDetalleComprobantes.identificacion = "";
                            pDetalleComprobantes.nom_tercero = "";
                            PlanCuentas.es_impuesto = PlanCuentas.es_impuesto != null ? PlanCuentas.es_impuesto : "0";
                            pDetalleComprobantes.base_comp = base_comp;
                            pDetalleComprobantes.porcentaje = porcentaje;
                            if (txtBase.Text != "" && txtBase != null && txtBase.Text != "0")
                            {
                                LstDetalleGeneral.Add(pDetalleComprobantes);
                            }
                        }
                    }
                }
            }
            cont++;
        }

        gvDetMovs.PageIndex = gvDetMovs.PageCount;
        gvDetMovs.DataSource = LstDetalleGeneral;
        gvDetMovs.DataBind();

        Session["DetalleComprobante"] = LstDetalleGeneral;

        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        mpeImpuestos.Hide();
        return;
    }

    protected void txtBase_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid txtBase = (TextBoxGrid)sender;

        int rowIndex = Convert.ToInt32(txtBase.CommandArgument);
        decimal NuevoValor = 0;
        foreach (GridViewRow rFila in gvImpuestos.Rows)
        {
            if (rFila.RowIndex == rowIndex)
            {
                TextBox txtPorcentajeImpuesto = (TextBox)rFila.FindControl("txtPorcentajeImpuesto");
                decimales txtValorImp = (decimales)rFila.FindControl("txtValorImp");
                if (txtBase.Text != "")
                {
                    if (txtPorcentajeImpuesto.Text != "")
                    {
                        NuevoValor = Convert.ToDecimal(txtBase.Text) * (Convert.ToDecimal(txtPorcentajeImpuesto.Text) / 100);
                        txtValorImp.Text = NuevoValor.ToString("N0");
                    }
                }
                else
                {
                    txtValorImp.Text = "";
                }
                break;
            }
        }

        return;
    }


    protected void btnImpriOrden_Click(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.CreditoPlanService creditoPlanServicio = new Xpinn.FabricaCreditos.Services.CreditoPlanService();
        if (Session["NumCred_Orden"] != null)
        {
            Session[creditoPlanServicio.CodigoPrograma + ".id"] = Session["NumCred_Orden"].ToString();
            Response.Redirect("~/Page/FabricaCreditos/PlanPagos/Detalle.aspx");
        }

        return;
    }

    protected void txtValor_TextChangeds(object sender, EventArgs e)
    {
        ObtenerDetalleCompOptimizado(false);
        //ObtenerDetalleComprobante(false);
        TextBoxGrid txtValor = (TextBoxGrid)sender;
        int rowIndex = Convert.ToInt32(txtValor.CommandArgument);
        //focoEdicion(rowIndex, gvDetMovs, "ddlMoneda", "2doModoFiltro");
        CalcularTotal();
        return;

    }

    protected void txtValor_PreRender(object sender, EventArgs e)
    {
        //foreach (GridViewRow ins in gvDetMovs.Rows)
        //{
        //TextBoxGrid txtValor = (TextBoxGrid)ins.FindControl("txtValor");
        TextBoxGrid txtValor = (TextBoxGrid)sender;
        string str = txtValor.Text;
        string strDec = "";
        int posDec = 0;
        string formateado = "";

        string s = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        if (s == ".")
            str = str.Replace(",", "");
        else
        {
            str = str.Replace(".", "");
            str = str.Replace(",", ".");
        }

        try
        {
            //posDec = str.IndexOf(",");
            posDec = s == "." ? str.IndexOf(",") : str.IndexOf(".");
            if (posDec > 0)
            {
                strDec = str.Substring(posDec + 1, str.Length - (posDec + 1));
                str = str.Substring(0, posDec);
            }
            if (str != "" && (Convert.ToInt64(str) > 0 || (Convert.ToInt64(str) == 0 && Convert.ToInt64(strDec) > 0)))
            {
                var strI = Convert.ToInt64(str);  //Convierte a entero y luego a string para quitar ceros a la izquierda
                str = strI.ToString();

                if (str.Length > 13)
                { str = str.Substring(0, 13); }

                int longi = str.Length;
                string supermill = "";
                string mill = "";
                string mil = "";
                string cen = "";


                if (longi > 0 && longi <= 3)
                {
                    cen = str.Substring(0, longi);
                    formateado = Convert.ToInt64(cen).ToString();
                }
                else if (longi > 3 && longi <= 6)
                {
                    mil = str.Substring(0, longi - 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mil) + "." + cen;
                }
                else if (longi > 6 && longi <= 9)
                {
                    mill = str.Substring(0, longi - 6);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(mill) + "." + mil + "." + cen;
                }
                else if (longi > 9 && longi <= 14)
                {
                    supermill = str.Substring(0, longi - 9);
                    mill = str.Substring(longi - 9, 3);
                    mil = str.Substring(longi - 6, 3);
                    cen = str.Substring(longi - 3, 3);
                    formateado = Convert.ToInt64(supermill) + "." + mill + "." + mil + "." + cen;
                }
                else
                { formateado = ""; }

                if (posDec > 0 && formateado != "")
                {
                    formateado = formateado + "," + strDec;
                }

            }
            else
            {
                if (strDec != "" && (Convert.ToInt64(strDec) > 0 || (Convert.ToInt64(strDec) == 0 && Convert.ToInt64(strDec) > 0)))
                { formateado = "0," + strDec; }
                else
                { formateado = ""; }

            }
            txtValor.Text = formateado.ToString();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        return;
        //}
    }


    //FUNCION DE MOSTRAR PANELES
    void PanelActiveView(Panel pIdPanel)
    {
        string pNombrePanel = "vwPanel";
        ContentPlaceHolder contenedor = (ContentPlaceHolder)Master.FindControl("cphMain");
        foreach (Control control in contenedor.Controls)
        {
            if (control is Panel)
            {
                if (((Panel)(control)).ID.Contains(pNombrePanel))
                {
                    if (((Panel)(control)).ID == pIdPanel.ID)
                    {
                        ((Panel)(control)).Visible = true;
                    }
                    else
                        ((Panel)(control)).Visible = false;
                }
            }
        }
    }


    protected void ObtenerObservacionesAnulaciones(String pIdNComp, String pIdTComp)
    {
        try
        {
            Xpinn.Contabilidad.Services.ComprobanteService GiroService = new Xpinn.Contabilidad.Services.ComprobanteService();
            Xpinn.Contabilidad.Entities.Comprobante pComprobante = new Xpinn.Contabilidad.Entities.Comprobante();
            Int64 num = Convert.ToInt64(pIdNComp.ToString());
            Int64 tipo = Convert.ToInt64(pIdTComp.ToString());

            pComprobante = ComprobanteServicio.ConsultarObservacionesAnulacion(num, tipo, (Usuario)Session["usuario"]);
            if (pComprobante.num_comp != 0)
            {
                Lblerror.Text = "Comprobante anulado Por:" + pComprobante.usuario.ToString() + " el dia"
                    + pComprobante.fecha.ToShortDateString() + " Motivo:" + pComprobante.descripcion
                    + " Comprobante N." + pComprobante.num_comp_anula + " Tipo_Comp= " + pComprobante.tipo_comp_anula;

            }
        }

        catch (Exception ex)
        {
            VerError(ex.Message);
        }

        return;
    }


    protected void RpviewComprobante_OnLoad(object sender, EventArgs e)
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        if (pUsuario.codperfil != 1)
        {
            RenderingExtension extensionWORD = RpviewComprobante.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals("Word", StringComparison.CurrentCultureIgnoreCase));
            if (extensionWORD != null)
            {
                System.Reflection.FieldInfo fieldInfo = extensionWORD.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo.SetValue(extensionWORD, false);
            }
            RenderingExtension extensionEXCEL = RpviewComprobante.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals("Excel", StringComparison.CurrentCultureIgnoreCase));
            if (extensionEXCEL != null)
            {
                System.Reflection.FieldInfo fieldInfo = extensionEXCEL.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                fieldInfo.SetValue(extensionEXCEL, false);
            }
        }

        return;
    }


    protected void txtCodCuentaPC_TextChanged(object sender, EventArgs e)
    {
        if (txtCodCuentaPC.Text != "")
        {
            Xpinn.Contabilidad.Services.PlanCuentasService PlanCuentasServicio = new Xpinn.Contabilidad.Services.PlanCuentasService();
            Xpinn.Contabilidad.Entities.PlanCuentas PlanCuentas = new Xpinn.Contabilidad.Entities.PlanCuentas();
            PlanCuentas = PlanCuentasServicio.ConsultarPlanCuentas(txtCodCuentaPC.Text, (Usuario)Session["usuario"]);

            // Mostrar el nombre de la cuenta           
            if (txtNomCuentaPC != null)
                txtNomCuentaPC.Text = PlanCuentas.nombre;
        }
        else
            txtNomCuentaPC.Text = "";

        return;
    }

    protected void btnListadoPlanPC_Click(object sender, EventArgs e)
    {
        ctlListadoPlanPC.Motrar(true, "txtCodCuentaPC", "txtNomCuentaPC");
    }
    protected void ActualizarSoportes(Comprobante vComprobante)
    {
        if (Session["lstSoportes"] != null)
        {
            List<Xpinn.Tesoreria.Entities.SoporteCaj> lstSoportes = new List<Xpinn.Tesoreria.Entities.SoporteCaj>();
            Xpinn.Tesoreria.Services.SoporteCajService SoporteCajServicio = new Xpinn.Tesoreria.Services.SoporteCajService();
            lstSoportes = (List<Xpinn.Tesoreria.Entities.SoporteCaj>)Session["lstSoportes"];
            foreach (Xpinn.Tesoreria.Entities.SoporteCaj soporte in lstSoportes)
            {
                soporte.num_comp = Convert.ToInt32(vComprobante.num_comp);
                soporte.tipo_comp = Convert.ToInt32(vComprobante.tipo_comp);
                SoporteCajServicio.ModificarSoporteCaj(soporte, (Usuario)Session["usuario"]);
            }
        }

        return;
    }

    protected void btnImpriReci_Click(object sender, EventArgs e)
    {
        if (Session[Usuario.codusuario + "codOpe"] != null && Session[Usuario.codusuario + "cod_persona"] != null)
        {
            Navegar("../../Tesoreria/PagosVentanilla/ReciboFactura.aspx");
        }
    }
}
