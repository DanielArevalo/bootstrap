using System;
using System.Drawing.Imaging;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Comun.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Servicios.Services;
using Xpinn.Servicios.Entities;
using Xpinn.Util;
using System.Linq;
using Microsoft.Reporting.WebForms;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Services;
using System.Drawing;

public partial class Detalle : GlobalWeb
{
    #region Conexiones a los servicios y Variables 
    private consultasdatacreditoService consultasdatacreditoServicio = new consultasdatacreditoService();
    private Persona1Service Persona1Servicio = new Persona1Service();
    private SolicitudCreditosRecogidosService SolicitudCreditosRecogidosServicio = new SolicitudCreditosRecogidosService();
    private ReferenciaService ReferenciaServicio = new ReferenciaService();
    private DocumentosAnexosService DocumentosAnexosServicio = new DocumentosAnexosService();
    private codeudoresService CodeudorServicio = new codeudoresService();
    private ControlCreditosService ControlCreditosServicio = new ControlCreditosService();
    private List<Persona1> lstDatoSolicitud = new List<Persona1>();  //Lista de los menus desplegables    
    LineasCreditoService LineasCreditoServicio = new LineasCreditoService();
    LineasCredito vLineasCredito = new LineasCredito();
    private List<ControlTiempos> lstDatosSolicitud = new List<ControlTiempos>();  //Lista de los menus desplegables
    private ControlTiemposService ControlProcesosServicio = new ControlTiemposService();
    private CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
    private ModalidadTasaService ModalidadTasaServicio = new ModalidadTasaService();
    private CreditoSolicitadoService creditoServicio = new CreditoSolicitadoService();
    private DatosAprobadorService datosServicio = new DatosAprobadorService();
    private PeriodicidadService periodicidadServicio = new PeriodicidadService();
    private PoblarListas poblar = new PoblarListas();
    DataCreditoServices dataCreditoServices = new DataCreditoServices();
    ControlTiempos control = new ControlTiempos();
    CreditoService _creditoService = new CreditoService();
    CreditoSolicitado credito = new CreditoSolicitado();
    CreditoSolicitadoService CreditoSolicitadoServicio = new CreditoSolicitadoService();
    private Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    // Servicio para Consultar Avances 
    DetalleProductoService DetalleProducto = new DetalleProductoService();
    //Variables para historico tasa
    CreditoPlan creditoc = new CreditoPlan();
    HistoricoTasa historico = new HistoricoTasa();
    private CreditoPlanService creditoPlanServicio = new CreditoPlanService();
    HistoricoTasaService historicoTasaService = new HistoricoTasaService();
    ClasificacionCartera clasificacionCartera = new ClasificacionCartera();
    ClasificacionCarteraService clasificacionCarteraService = new ClasificacionCarteraService();
    IList<HistoricoTasa> lsHistoricoTasas = new List<HistoricoTasa>();




    int cod_afiancol = 60;
    string _validacion;
    String FechaDatcaredito = "";
    String pIdCodLinea = "";
    String tasa = "0";
    String ListaSolicitada = null;
    Int64 tipoempresa = 0;
    Usuario _usuario;
    int number = 0;
    bool _validaCodeudores;
    General valorParametro = new General();
    #endregion

    #region Metodos Inicio de page

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[creditoServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(creditoServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(creditoServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlFormatos.eventoClick += btnImpresion_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            _usuario = (Usuario)Session["usuario"];
            // Permite modificar cierta información cuando es perfil ADMINISTRADOR.
            //bool bHabilitar = false;
            // if ((_usuario).codperfil == 1)
            //   bHabilitar = true;
            if (!IsPostBack)
            {
                mvAprobacion.ActiveViewIndex = 0;
                Session["Distribucion"] = null;
                Session["ES_EMPLEADO"] = null;
                Session["NumeroSolicitud"] = null;
                Session["TotalCuoExt"] = 0;
                //   gvDeducciones.Enabled = bHabilitar;

                // Determinar si el usuario puede modificar la tasa
                OficinaService oficinaService = new OficinaService();
                Usuario usuap = _usuario;
                int cod = Convert.ToInt32(usuap.codusuario);
                int consulta = oficinaService.UsuarioPuedecambiartasas(cod, _usuario);
                if (consulta == 0) // Si no tiene atribuciones                 
                    chkTasa.Visible = false;
                else
                    chkTasa.Visible = true;

                // Determinar como se modifican los datos
                rbTipoAprobacion.SelectedIndex = 0;
                rbTipoAprobacion_SelectedIndexChanged(rbTipoAprobacion, null);

                tipoempresa = Convert.ToInt64(usuap.tipo);
                if (tipoempresa == 2)
                {
                    lbledad.Visible = false;
                    txtEdad.Visible = false;
                    mvAprobacion.ActiveViewIndex = 0;
                    Lblaprob.Text = "APROBACIÓN DE CRÉDITOS";
                    panelAprobacion.Visible = false;
                }
                if (tipoempresa == 1)
                {
                    lbledad.Visible = true;
                    mvAprobacion.ActiveViewIndex = 0;
                    Lblaprob.Text = "APROBACIÓN DE CRÉDITOS MODIFICANDO CONDICIONES";
                }
                Session["COD_LINEA"] = null;
                Session["HojaDeRuta"] = null;
                CargarListas();
                chkDistribuir_CheckedChanged(chkDistribuir, null);
                if (Session[creditoServicio.CodigoPrograma + ".id"] != null)
                {

                    // Determinar si el llamado se hizo desde la hoja de ruta
                    if (Request.UrlReferrer != null)
                        if (Request.UrlReferrer.Segments[4].ToString() == "HojaDeRuta/")
                            Session["HojaDeRuta"] = "1";
                    // Mostrar los datos del crédito
                    idObjeto = Session[creditoServicio.CodigoPrograma + ".id"].ToString();
                    ViewState["NumeroRadicacion"] = idObjeto;
                    ObtenerDatos(idObjeto);

                    //DETERMINAR SI SE VISUALIZARA EL CAMPO DE APORTE Y COMISION
                    bool comision = false;
                    bool aporte = false;
                    bool seguro = false;
                    oficinaService.ValidarComisionAporte(Session["COD_LINEA"].ToString(), ref comision, ref aporte, ref seguro, _usuario);
                    //txtComision.Visible = comision;
                    //lblComision.Visible = comision;
                    //txtAporte.Visible = aporte;
                    //lblAporte.Visible = aporte;
                    //txtSeguro.Visible = seguro;
                    //lblSeguro.Visible = seguro;
                    Session.Remove("COD_LINEA");

                    ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                }
                else
                {
                    chkTasa_CheckedChanged(chkTasa, null);
                }
            }
            else
                CalculaTotalXColumna();
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    #endregion

    #region Metodos de eventos 
    protected void chkAvance_CheckedChanged(object sender, EventArgs e)
    {

        CheckBoxGrid chkAvance = (CheckBoxGrid)sender;
        int nItem = Convert.ToInt32(chkAvance.CommandArgument);

        TxtTotalAvances.Text = "0";
        TxtTotalCap.Text = "0";
        TxtTotalInt.Text = "0";
        Session["Avances"] = null;
        foreach (GridViewRow rFila in gvAvances.Rows)
        {

            CheckBoxGrid chkAvanceeRow = (CheckBoxGrid)rFila.FindControl("chkAvance");
            if (chkAvanceeRow.Checked)
            {
                TxtTotalAvances.Text = Convert.ToString(long.Parse(TxtTotalAvances.Text) + long.Parse(rFila.Cells[8].Text));
                TxtTotalCap.Text = Convert.ToString(long.Parse(TxtTotalCap.Text) + long.Parse(rFila.Cells[6].Text));
                TxtTotalInt.Text = Convert.ToString(long.Parse(TxtTotalInt.Text) + long.Parse(rFila.Cells[7].Text));
            }
        }


        CalcularTotalRecoge();
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["HojaDeRuta"] == "1")
            Response.Redirect("~/Page/FabricaCreditos/HojaDeRuta/Lista.aspx");
        else
        {
            Navegar(Pagina.Lista);
        }
    }
    //Metodo apra consultar 
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    // Mètodo para cuando se aprueba el crèdito
    protected void btnAcpAprobar_Click(object sender, EventArgs e)
    {
        string sError = "";
        VerError("");
        Validate("vgEdad");

        if (Page.IsValid)
        {
            try
            {
                CreditoSolicitado credito = new CreditoSolicitado();
                CreditoService CreditoServicio = new CreditoService();
                credito.ObservacionesAprobacion = txtObsApr.Text;
                credito.NumeroCredito = Int64.Parse(txtCredito2.Text);
                Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
                credito.Nombres = lblUsuario.Text;
                credito.fecha = DateTime.Now;
                credito.lstCodeudores = new List<Persona1>();
                credito.lstCodeudores = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];
                credito.lstCuoExt = new List<CuotasExtras>();
                credito.lstCuoExt = (List<CuotasExtras>)Session["CuoExt"];


                #region Interfaz WorkManagement


                // Parametro general para habilitar proceso de WM
                General parametroHabilitarOperacionesWM = ConsultarParametroGeneral(35);
                if (parametroHabilitarOperacionesWM != null && parametroHabilitarOperacionesWM.valor.Trim() == "1")
                {
                    General parametroObligaOperacionesWM = ConsultarParametroGeneral(39);
                    bool obligaOperaciones = parametroObligaOperacionesWM != null && parametroObligaOperacionesWM.valor == "1";
                    bool operacionFueExitosa = false;

                    try
                    {
                        WorkManagementServices workManagementService = new WorkManagementServices();

                        // Se necesita el workFlowId para saber cual es el workflow que vamos a correrle el step
                        // Este registro workFlowId es llenado en la solicitud del credito
                        WorkFlowCreditos workFlowCredito = workManagementService.ConsultarWorkFlowCreditoPorNumeroRadicacion(Convert.ToInt32(txtCredito2.Text.Trim()), Usuario);

                        if (workFlowCredito != null && !string.IsNullOrWhiteSpace(workFlowCredito.barCodeRadicacion) && workFlowCredito.workflowid > 0)
                        {
                            InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);

                            string observacionesWM = " Aprobacion para el credito con radicado: " + txtCredito.Text;
                            observacionesWM += !string.IsNullOrWhiteSpace(txtObs3.Text) ? " Observaciones: " + txtObs3.Text : string.Empty;

                            // RunTask, corre al siguiente proceso, debes identificar en el proceso que estas y añadir las observaciones
                            //bool aprobacionUnaExitosa = interfaz.RunTaskWorkFlowCredito(workFlowCredito.barCodeRadicacion, workFlowCredito.workflowid, StepsWorkManagementWorkFlowCredito.AprobacionCredito, observacionesWM);

                            // RunTask, corre al siguiente proceso, debes identificar en el proceso que estas y añadir las observaciones
                            bool aprobacionDosExitosa = interfaz.RunTaskWorkFlowCredito(workFlowCredito.barCodeRadicacion, workFlowCredito.workflowid, StepsWorkManagementWorkFlowCredito.AprobacionCredito2, observacionesWM);

                            operacionFueExitosa = true;
                        }

                        if (obligaOperaciones && !operacionFueExitosa)
                        {
                            VerError("No se pudo correr la tarea en el WM para este workFlow");
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        if (obligaOperaciones)
                        {
                            VerError("No se pudo correr la tarea en el WM para este workFlow");
                            return;
                        }
                    }
                }


                #endregion


                GuardarRecoger();

                creditoServicio.AprobarCredito(credito, _usuario, ref sError);

                if (sError.Trim() != "")
                {
                    VerError(sError);
                    return;
                }

                MensajeFinal("Crédito " + txtCredito2.Text + " Aprobado Correctamente");

                Usuario pUsuario = (Usuario)Session["usuario"];

                CreditoSolicitado proceso = new CreditoSolicitado();
                proceso.estado = "Aprobado";
                if (pUsuario.idioma == "1") { proceso.estado = "Approuve"; } else if (pUsuario.idioma == "2") { proceso.estado = "Approved"; } else { proceso.estado = "Aprobado"; }
                // Consultar el proceso para aprobacion
                proceso = creditoServicio.ConsultarCodigodelProceso(proceso, _usuario);
                ddlProceso.SelectedValue = Convert.ToString(proceso.Codigoproceso);

                consultarControlAprobados();
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(creditoServicio.GetType().Name + "L", "AprobarCredito", ex);
            }

        }
    }
    protected void btnCncAprobar_Click(object sender, EventArgs e)
    {
        txtObsApr.Text = "";
    }
    // Método para aprobar crèditos modificando condiciones
    protected void btnAcpAproModif_Click(object sender, EventArgs e)
    {
        Configuracion conf = new Configuracion();
        VerError("");
        lblErrorCod.Text = string.Empty;
        lblError.Text = "";
        if (ValidarAprobacion())
        {
            string sError = "";
            Validate("vgEdad");
            if (Page.IsValid)
            {
                CreditoService CreditoServicio = new CreditoService();
                CreditoSolicitado credito = new CreditoSolicitado();
                Periodicidad perio = new Periodicidad();

                try
                {
                    Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");

                    credito.Nombres = lblUsuario.Text;
                    credito.ObservacionesAprobacion = txtObs3.Text;
                    credito.NumeroCredito = Int64.Parse(txtCredito3.Text);
                    credito.Monto = Decimal.Parse(txtMonto2.Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    credito.Plazo = txtPlazo2.Text;
                    credito.fecha = DateTime.Now;
                    credito.cod_Periodicidad = Int32.Parse(ddlPlazo.SelectedValue);
                    //credito.tasa = Convert.ToDecimal(txtTasa.Text);
                    //credito.tipotasa = ddlTipoTasa.SelectedValue;
                    credito.lstCodeudores = new List<Persona1>();
                    credito.lstCodeudores = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];
                    credito.lstCuoExt = new List<CuotasExtras>();
                    credito.lstCuoExt = (List<CuotasExtras>)Session["CuoExt"];
                    credito.cod_linea_credito = ddlLineaCredito.SelectedValue;
                    credito.cod_Destino = ddldestino2.SelectedValue;
                    credito.forma_pago = ddlFormaPago.SelectedValue;
                    if (ucFechaPrimerPago.TieneDatos)
                        credito.fecha_primer_pago = ucFechaPrimerPago.ToDateTime;
                    if (Checkpoliza.Checked)
                        credito.reqpoliza = 1;
                    else
                        credito.reqpoliza = 0;
                    if (chekpago.Checked)
                        credito.condicion_especial = 1;
                    else
                        credito.condicion_especial = 0;

                    // Créditos a recoger
                    List<CreditoRecoger> lstCreditoRecoger = DevolverListaCreditosARecoger();


                    SolicitudCreditosRecogidos vSolicitudCreditosRecogidos = new SolicitudCreditosRecogidos();
                    DateTime fechaRecoger = Convert.ToDateTime(credito.fecha);
                    long numeroSolicitud = Convert.ToInt64(credito.numsolicitud.ToString().Trim());

                    foreach (GridViewRow row in gvListaSolicitudCreditosRecogidos.Rows)
                    {
                        if (((CheckBoxGrid)row.Cells[8].FindControl("chkRecoger")).Checked)
                        {
                            vSolicitudCreditosRecogidos.idsolicitudrecoge = 0;
                            vSolicitudCreditosRecogidos.numerosolicitud = numeroSolicitud;
                            vSolicitudCreditosRecogidos.numero_recoge = Convert.ToInt64(row.Cells[1].Text.Trim());
                            vSolicitudCreditosRecogidos.fecharecoge = fechaRecoger;
                            vSolicitudCreditosRecogidos.fechapago = DateTime.Now;
                            vSolicitudCreditosRecogidos.saldocapital = Convert.ToInt64(row.Cells[4].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.saldointcorr = Convert.ToInt64(row.Cells[5].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.saldointmora = Convert.ToInt64(row.Cells[6].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.saldootros = Convert.ToInt64(row.Cells[7].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.saldomipyme = Convert.ToInt64(row.Cells[8].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.saldoivamipyme = Convert.ToInt64(row.Cells[9].Text.Trim().Replace(gSeparadorMiles, ""));
                            if (dejarCuotaPendiente())
                                vSolicitudCreditosRecogidos.valor_nominas = Convert.ToInt64(row.Cells[12].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos = SolicitudCreditosRecogidosServicio.CrearSolicitudCreditosRecogidos(vSolicitudCreditosRecogidos, (Usuario)Session["usuario"]);
                        }
                    }

                    SolicitudRecogidoAvance vSolicitudCreditosRecogidosAvances = new SolicitudRecogidoAvance();
                    foreach (GridViewRow rFila in gvAvances.Rows)
                    {

                        CheckBoxGrid chkAvanceeRow = (CheckBoxGrid)rFila.FindControl("chkAvance");
                        if (chkAvanceeRow.Checked)
                        {
                            vSolicitudCreditosRecogidosAvances.Radicado = Convert.ToInt64(Session["NumProducto"].ToString());
                            vSolicitudCreditosRecogidosAvances.ValorTotal = Convert.ToInt64(rFila.Cells[8].Text);
                            vSolicitudCreditosRecogidosAvances.Intereses = Convert.ToInt64(rFila.Cells[7].Text);
                            vSolicitudCreditosRecogidosAvances.SaldoAvance = Convert.ToInt64(rFila.Cells[6].Text);
                            vSolicitudCreditosRecogidosAvances.FechaDesembolsi = credito.fecha;
                            vSolicitudCreditosRecogidosAvances.NumAvance = Convert.ToInt64(rFila.Cells[1].Text);

                            vSolicitudCreditosRecogidosAvances = SolicitudCreditosRecogidosServicio.CrearSolicitudCreditosRecogidosAvances(vSolicitudCreditosRecogidosAvances, (Usuario)Session["usuario"]);

                        }
                    }

                    // Empresas de recaudo
                    List<CreditoEmpresaRecaudo> lstDetalle = null;

                    // Guardar las empresas de recaudo
                    if (ddlFormaPago.SelectedIndex == 1)// FORMA DE PAGO POR NOMINA
                    {
                        lstDetalle = ObtenerListaEmpresaRecaudadora();
                    }

                    List<Credito_Giro> lstGiro = new List<Credito_Giro>();
                    bool pGrabarCreditoGiro = chkDistribuir.Checked ? true : false;
                    if (pGrabarCreditoGiro == true)
                        lstGiro = ObtenerListaDistribucion();

                    int opcion = 0;
                    //VALIDAR SI EXISTE UNA ORDEN DE SERVICIO
                    CreditoOrdenServicio CredOrden = new CreditoOrdenServicio();
                    if (ctlBusquedaProveedor.VisibleCtl == true)
                    {
                        if (ctlBusquedaProveedor.TextOrdCred != "")
                        {
                            if (ctlBusquedaProveedor.CheckedOrd == true)
                            {
                                //DATOS A MODIFICAR
                                CreditoOrdenServicio pEntidad = new CreditoOrdenServicio();
                                String pFiltro = " WHERE NUMERO_RADICACION = " + txtCredito.Text.Trim();
                                pEntidad = CreditoServicio.ConsultarCREDITO_OrdenServ(pFiltro, _usuario);

                                CredOrden.idordenservicio = Convert.ToInt64(ctlBusquedaProveedor.TextOrdCred);
                                CredOrden.numero_radicacion = Convert.ToInt64(txtCredito.Text);
                                CredOrden.cod_persona = ctlBusquedaProveedor.TextCodigo;
                                CredOrden.idproveedor = ctlBusquedaProveedor.TextIdentif;
                                CredOrden.nomproveedor = ctlBusquedaProveedor.TextNomProv;
                                CredOrden.detalle = pEntidad.detalle;
                                CredOrden.estado = pEntidad.estado;
                                CredOrden.numero_preimpreso = pEntidad.numero_preimpreso;
                                opcion = 2;
                            }
                            else
                            {
                                //DATOS A ELIMINAR
                                CredOrden.idordenservicio = Convert.ToInt64(ctlBusquedaProveedor.TextOrdCred);
                                CredOrden.numero_radicacion = Convert.ToInt64(txtCredito.Text);
                                opcion = 3;
                            }
                        }
                        else
                        {
                            if (ctlBusquedaProveedor.CheckedOrd == true)
                            {
                                //DATOS A REGISTRAR
                                CredOrden.idordenservicio = Convert.ToInt64(0);
                                CredOrden.numero_radicacion = Convert.ToInt64(txtCredito.Text);
                                CredOrden.cod_persona = Convert.ToInt64(txtCodigo.Text);
                                CredOrden.idproveedor = ctlBusquedaProveedor.TextIdentif;
                                CredOrden.nomproveedor = ctlBusquedaProveedor.TextNomProv;
                                CredOrden.detalle = null;
                                CredOrden.estado = null;
                                opcion = 1;
                            }
                        }
                    }

                    // Cargar atributos descontados/financiados a modificar del crédito
                    credito.lstDescuentosCredito = new List<DescuentosCredito>();
                    foreach (GridViewRow gFila in gvDeducciones.Rows)
                    {
                        try
                        {
                            DescuentosCredito eDescuento = new DescuentosCredito();
                            eDescuento.numero_radicacion = credito.NumeroCredito;
                            eDescuento.cod_atr = ValorSeleccionado((Label)gFila.FindControl("lblCodAtr"));
                            eDescuento.nom_atr = Convert.ToString(gFila.Cells[1].ToString());
                            eDescuento.tipo_descuento = ValorSeleccionado((DropDownList)gFila.FindControl("ddlTipoDescuento"));
                            eDescuento.tipo_liquidacion = ValorSeleccionado((DropDownList)gFila.FindControl("ddlTipoLiquidacion"));
                            eDescuento.forma_descuento = Convert.ToInt32(ValorSeleccionado((DropDownList)gFila.FindControl("ddlFormaDescuento")));
                            eDescuento.val_atr = ValorSeleccionado((TextBox)gFila.FindControl("txtvalor"));
                            eDescuento.numero_cuotas = ValorSeleccionado((TextBox)gFila.FindControl("txtnumerocuotas"));
                            eDescuento.cobra_mora = ValorSeleccionado((CheckBox)gFila.FindControl("cbCobraMora"));
                            credito.lstDescuentosCredito.Add(eDescuento);
                        }
                        catch (Exception ex)
                        {
                            VerError("Se presentaron errores al cargar atributos descontados/financiados a modificar del crédito. Error:" + ex.Message);
                            return;
                        }
                    }

                    // Guardar la tasa
                    string calculo_atr = rbCalculoTasa.SelectedValue;
                    string tasa = string.Empty, tipotasa = string.Empty, desviacion = string.Empty, tipohisto = string.Empty, codart = string.Empty;
                    bool cambioTasa = chkTasa.Checked;
                    List<CreditoSolicitado> lstCambiTasa = new List<CreditoSolicitado>();
                    if (cambioTasa)
                    {
                        if (PanelFija.Visible == true)
                        {
                            lstCambiTasa = ObtenerDatosCambiTasa();
                        }
                        if (PanelHistorico.Visible == true)
                        {
                            desviacion = txtDesviacion.Text != "0" ? txtDesviacion.Text : "";
                            tipohisto = ddlHistorico.SelectedValue != "" && ddlHistorico.SelectedItem != null ? ddlHistorico.SelectedValue : "";
                            CreditoSolicitado atrCred = new CreditoSolicitado();
                            atrCred.calculo_atr = calculo_atr;
                            atrCred.tasa = Convert.ToDecimal(ConvertirStringADecimal(tasa, ','));
                            atrCred.TipoTasa = 0;
                            atrCred.TipoHistorico = tipohisto;
                            atrCred.Desviacion = desviacion;
                            atrCred.CodAtr = 2;
                            lstCambiTasa.Add(atrCred);
                        }
                    }

                    // Realizar los cambios al crédito
                    creditoServicio.AprobarCreditoModificando(credito, pGrabarCreditoGiro, lstGiro, opcion, CredOrden, _usuario, ref sError, lstCreditoRecoger, lstDetalle, cambioTasa, lstCambiTasa);

                    if (sError.Trim() != "")
                    {
                        VerError(sError);
                        return;
                    }

                    CuotasExtras.GuardarCuotas(txtCredito3.Text);

                    // Finalizar el proceso 
                    MensajeFinal("Crédito " + txtCredito2.Text + " Aprobado Correctamente Con las Nuevas Condiciones");
                    Usuario pUsuario = (Usuario)Session["usuario"];
                    CreditoSolicitado proceso = new CreditoSolicitado();
                    proceso.estado = "Aprobado";
                    if (pUsuario.idioma == "1") { proceso.estado = "Approuve"; } else if (pUsuario.idioma == "2") { proceso.estado = "Approved"; } else { proceso.estado = "Aprobado"; }
                    // Consultar el proceso para aprobacion
                    proceso = creditoServicio.ConsultarCodigodelProceso(proceso, _usuario);
                    ddlProceso.SelectedValue = Convert.ToString(proceso.Codigoproceso);
                    consultarControlAprobados();

                    hiddenOperacionCredito.Value = ((int)TipoDocumentoCorreo.CreditoAprobado).ToString();


                    #region Interactuar WorkManagement


                    // Parametro general para habilitar proceso de WM, lo dejo luego de aprobar porque confio mas en WM en no fallar que en Financial
                    // Si fuera al revez y Financial fallara, WM estaria por delante y seria mas problema sincronizarlos de nuevo
                    General parametroHabilitarOperacionesWM = ConsultarParametroGeneral(35);
                    if (parametroHabilitarOperacionesWM != null && parametroHabilitarOperacionesWM.valor.Trim() == "1")
                    {
                        General parametroObligaOperacionesWM = ConsultarParametroGeneral(39);
                        bool obligaOperaciones = parametroObligaOperacionesWM != null && parametroObligaOperacionesWM.valor == "1";
                        bool operacionFueExitosa = false;
                        try
                        {
                            WorkManagementServices workManagementService = new WorkManagementServices();

                            // Se necesita el workFlowId para saber cual es el workflow que vamos a correrle el step
                            // Este registro workFlowId es llenado en la solicitud del credito
                            WorkFlowCreditos workFlowCredito = workManagementService.ConsultarWorkFlowCreditoPorNumeroRadicacion(Convert.ToInt32(txtCredito2.Text.Trim()), Usuario);

                            if (workFlowCredito != null && !string.IsNullOrWhiteSpace(workFlowCredito.barCodeRadicacion) && workFlowCredito.workflowid > 0)
                            {
                                InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);

                                string observacionesWM = !string.IsNullOrWhiteSpace(txtObs3.Text) ? txtObs3.Text : " Aprobacion para el credito con radicado: " + txtCredito.Text;

                                // RunTask, corre al siguiente proceso, debes identificar en el proceso que estas y añadir las observaciones
                                bool aprobacionUnaExitosa = interfaz.RunTaskWorkFlowCredito(workFlowCredito.barCodeRadicacion, workFlowCredito.workflowid, StepsWorkManagementWorkFlowCredito.AprobacionCredito, observacionesWM);

                                // RunTask, corre al siguiente proceso, debes identificar en el proceso que estas y añadir las observaciones
                                bool aprobacionDosExitosa = interfaz.RunTaskWorkFlowCredito(workFlowCredito.barCodeRadicacion, workFlowCredito.workflowid, StepsWorkManagementWorkFlowCredito.AprobacionCredito2, observacionesWM);

                                operacionFueExitosa = true;
                            }

                            if (obligaOperaciones && !operacionFueExitosa)
                            {
                                VerError("No se pudo correr la tarea en el WM para este workFlow");
                                return;
                            }
                        }
                        catch (Exception)
                        {
                            if (obligaOperaciones)
                            {
                                VerError("No se pudo correr la tarea en el WM para este workFlow");
                                return;
                            }
                        }
                    }


                    #endregion


                    mvAprobacion.ActiveViewIndex = 1;
                    Session.Remove(Usuario.codusuario + "Codeudores");
                }
                catch (Exception ex)
                {
                    int indice = ex.Message.IndexOf(":");
                    string mensaje = ex.Message.Substring(indice * 3 + 30);
                    VerError(mensaje);
                }
            }
        }
    }

    protected void btnCncAproModif_Click(object sender, EventArgs e)
    {
        txtObs3.Text = "";
        txtMonto2.Text = "";
        txtPlazo2.Text = "";
        ddlPlazo.SelectedIndex = 0;
    }

    // Método para aplazar el crédito
    protected void btnAcpAplazar_Click(object sender, EventArgs e)
    {
        Validate("vgEdad");
        VerError("");

        if (Page.IsValid)
        {
            CreditoSolicitado credito = new CreditoSolicitado();
            CreditoSolicitadoService servicio = new CreditoSolicitadoService();
            Motivo motivo = new Motivo();
            Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
            credito.Nombres = lblUsuario.Text;
            motivo.Codigo = Int16.Parse(ddlAplazar.SelectedValue);
            credito.ObservacionesAprobacion = txtObs4.Text;
            credito.NumeroCredito = Int64.Parse(txtCredito4.Text);

            GuardarRecoger();
            creditoServicio.AplazarCredito(credito, motivo, _usuario);

            hiddenOperacionCredito.Value = ((int)TipoDocumentoCorreo.CreditoAplazado).ToString();

            MensajeFinal("Crédito " + txtCredito2.Text + " Aplazado");
            //Modificado para seleccionar el código del proceso según los parametros de la entidad
            //if (ddlProceso.Items.Count > 16)
            //    ddlProceso.SelectedIndex = 16;
            credito.estado = "Aplazado";
            credito = servicio.ConsultarCodigodelProceso(credito, _usuario);
            ddlProceso.SelectedValue = Convert.ToString(credito.Codigoproceso);

            consultarControlAplazados();
        }
    }
    protected void btnCncAplaz_Click(object sender, EventArgs e)
    {
        txtObs4.Text = "";
        ddlAplazar.SelectedIndex = 0;

    }
    // Método para negar el crédito
    protected void btnAcpNegar_Click(object sender, EventArgs e)
    {
        Validate("vgEdad");
        VerError("");

        if (Page.IsValid)
        {
            CreditoSolicitado credito = new CreditoSolicitado();
            CreditoSolicitadoService servicio = new CreditoSolicitadoService();
            Motivo motivo = new Motivo();

            Label lblUsuario = (Label)Master.FindControl("header1").FindControl("lblUser");
            credito.Nombres = lblUsuario.Text;
            motivo.Codigo = Int16.Parse(ddlNegar.SelectedValue);
            credito.ObservacionesAprobacion = txtObs5.Text;
            credito.NumeroCredito = Int64.Parse(txtCredito5.Text);
            GuardarRecoger();
            creditoServicio.NegarCredito(credito, motivo, _usuario);

            hiddenOperacionCredito.Value = ((int)TipoDocumentoCorreo.CreditoNegado).ToString();

            MensajeFinal("Crédito " + txtCredito2.Text + " Negado");
            //Modificado para seleccionar el código del proceso según los parametros de la entidad

            //if (ddlProceso.Items.Count > 15)
            //    ddlProceso.SelectedIndex = 15;

            credito.estado = "Negado";
            credito = servicio.ConsultarCodigodelProceso(credito, _usuario);
            ddlProceso.SelectedValue = Convert.ToString(credito.Codigoproceso);

            btnContinuar.Visible = false;
            consultarControlNegados();
        }
    }
    protected void btnCncNegar_Click(object sender, EventArgs e)
    {
        txtObs5.Text = "";
        ddlNegar.SelectedIndex = 0;
    }
    // Mètodo para controlar el cambio de pàgina en la grilla de observaciones del referenciador
    protected void gvObservacionesRef_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvObservacionesRef.PageIndex = e.NewPageIndex;
            TableReferencias(txtCredito.Text);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "gvObservacionesRef_PageIndexChanging", ex);
        }
    }
    // Mètodo para controlar el cambio de pàgina en la grilla de los aprobadores
    protected void gvAprobacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAprobacion.PageIndex = e.NewPageIndex;
            TablaAprobadores(txtCredito.Text);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "gvAprobacion_PageIndexChanging", ex);
        }
    }

    // Método para control de paginación de los anexos.
    protected void gvListaModalidadTasas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaModalidadTasas.PageIndex = e.NewPageIndex;
            TablaModalidaTasa(pIdCodLinea);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DocumentosAnexosServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }
    // Mètodo para botòn de continuar el cual lleva a la lista de consulta
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }
    // Guardar informaciòn de los crèditos a recoger
    private void GuardarRecoger()
    {
        CreditoRecoger vCreditoRecoger = new CreditoRecoger();

        foreach (GridViewRow row in gvListaSolicitudCreditosRecogidos.Rows)
        {
            CheckBoxGrid chkRecoger = (CheckBoxGrid)row.FindControl("chkRecoger");
            if (chkRecoger != null)
            {
                if (chkRecoger.Checked)
                {

                    vCreditoRecoger.numero_radicacion = Convert.ToInt64(txtCredito.Text.Trim());
                    vCreditoRecoger.numero_credito = Convert.ToInt64(row.Cells[1].Text.Trim());
                    Int64 valor_recoge = 0;
                    Int64 valor;
                    valor = Convert.ToInt64(row.Cells[4].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    valor = Convert.ToInt64(row.Cells[5].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    valor = Convert.ToInt64(row.Cells[6].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    valor = Convert.ToInt64(row.Cells[7].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    if (valor_recoge < 0)
                        valor_recoge = 0;
                    vCreditoRecoger.valor_recoge = valor_recoge;
                    vCreditoRecoger.fecha_pago = DateTime.Now;
                    vCreditoRecoger.interes_corriente = Convert.ToInt64(row.Cells[5].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    vCreditoRecoger.interes_mora = Convert.ToInt64(row.Cells[6].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    vCreditoRecoger.otros = Convert.ToInt64(row.Cells[7].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    vCreditoRecoger.saldo_capital = Convert.ToInt64(row.Cells[4].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    if (dejarCuotaPendiente())
                        vCreditoRecoger.valor_nominas = Convert.ToInt64(row.Cells[12].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    creditoRecogerServicio.EliminarCreditoRecoger(Convert.ToInt64(row.Cells[1].Text.Trim()), _usuario);
                    creditoRecogerServicio.CrearCreditoRecoger(vCreditoRecoger, _usuario);
                    idObjeto = vCreditoRecoger.numero_radicacion.ToString();
                }
                else
                {
                    creditoRecogerServicio.EliminarCreditoRecoger(Convert.ToInt64(row.Cells[1].Text.Trim()), _usuario);
                }
            }
        }
    }
    protected void registro_Click(object sender, EventArgs e)
    {
        Session["NumeroCredito"] = Convert.ToInt64(txtCredito.Text.Trim());
        Session["Numeroidentificacion"] = txtId.Text;
        Response.Redirect("~/Page/FabricaCreditos/ControlTiempos/Lista.aspx");
    }
    protected void chkTasa_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTasa.Checked)
        {
            rbCalculoTasa.Visible = true;
            rbCalculoTasa.SelectedIndex = 0;
            rbCalculoTasa_SelectedIndexChanged(rbCalculoTasa, null);
        }
        else
        {
            rbCalculoTasa.Visible = false;
            PanelFija.Visible = false;
            PanelHistorico.Visible = false;
        }
    }
    protected void ddlLineaCredito_TextChanged(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        LineasCredito eLinea = new LineasCredito();
        eLinea = LineaCreditoServicio.ConsultarLineasCredito(ddlLineaCredito.SelectedValue.ToString(), _usuario);
        //Actualiza las destinaciones para cambiar
        poblar.PoblarListaDesplegable2(ddlLineaCredito.SelectedValue.ToString(), ddldestino2, (Usuario)Session["usuario"]);
    }
    protected void gvListaModalidadTasas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ModalidadTasa modalidad = new ModalidadTasa();
                modalidad = (ModalidadTasa)e.Row.DataItem;
                CheckBox chkModalidadTasa = new CheckBox();
                chkModalidadTasa = (CheckBox)e.Row.Cells[3].FindControl("chkSeleccionarTasa");
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModalidadTasaServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }
    private void GuardarControl()
    {
        Usuario pUsuario = _usuario;


        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
        vControlCreditos.numero_radicacion = Convert.ToInt64(this.txtCredito.Text);
        vControlCreditos.codtipoproceso = ddlProceso.SelectedItem != null ? ddlProceso.SelectedValue : null;
        vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
        vControlCreditos.cod_persona = pUsuario.codusuario;
        vControlCreditos.cod_motivo = Convert.ToInt64(this.ddlAplazar.SelectedValue);
        if (txtObsApr.Text != "" || txtObs3.Text != "" || txtObs4.Text != "" || txtObs5.Text != "")
        {
            if (txtObsApr.Text != "")
            {
                vControlCreditos.observaciones = this.txtObsApr.Text.Length >= 250 ? txtObsApr.Text.Substring(0, 249) : txtObsApr.Text.Substring(0, txtObsApr.Text.Length).ToUpper();
            }
            if (txtObs3.Text != "")
            {
                vControlCreditos.observaciones = this.txtObs3.Text.Length >= 250 ? txtObs3.Text.Substring(0, 249) : txtObs3.Text.Substring(0, txtObs3.Text.Length).ToUpper();
            }
            if (txtObs4.Text != "")
            {
                vControlCreditos.observaciones = this.txtObs4.Text.Length >= 250 ? txtObs4.Text.Substring(0, 249) : txtObs4.Text.Substring(0, txtObs4.Text.Length).ToUpper();
            }
            if (txtObs5.Text != "")
            {
                vControlCreditos.observaciones = this.txtObs5.Text.Length >= 250 ? txtObs5.Text.Substring(0, 249) : txtObs5.Text.Substring(0, txtObs5.Text.Length).ToUpper();
            }
        }
        else
        {
            vControlCreditos.observaciones = "0";
        }

        vControlCreditos.anexos = null;
        vControlCreditos.nivel = -1;

        if (Session["Datacredito"] == null)
        {
            if (FechaDatcaredito == "" || FechaDatcaredito == null || Session["Datacredito"] == "" || Session["Datacredito"].ToString() == "" || Session["Datacredito"] == null)
            {
                vControlCreditos.fechaconsulta_dat = FechaDatcaredito == "" ? DateTime.MinValue : Convert.ToDateTime(FechaDatcaredito.Trim());
            }
            else
            {
                if (FechaDatcaredito != null || FechaDatcaredito != "" || Session["Datacredito"] != null)
                {
                    FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());
                    vControlCreditos.fechaconsulta_dat = Convert.ToDateTime(FechaDatcaredito);
                }
            }
        }

        vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, _usuario);

    }
    private void GuardarControlNegado()
    {
        Usuario pUsuario = _usuario;


        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
        vControlCreditos.numero_radicacion = Convert.ToInt64(this.txtCredito.Text);
        vControlCreditos.codtipoproceso = ddlProceso.SelectedItem != null ? ddlProceso.SelectedValue : null;
        vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
        vControlCreditos.cod_persona = pUsuario.codusuario;
        vControlCreditos.cod_motivo = Convert.ToInt64(this.ddlNegar.SelectedValue);
        if (txtObsApr.Text != "" || txtObs3.Text != "" || txtObs4.Text != "" || txtObs5.Text != "")
        {
            if (txtObsApr.Text != "")
            {
                vControlCreditos.observaciones = this.txtObsApr.Text.Length >= 250 ? txtObsApr.Text.Substring(0, 249) : txtObsApr.Text.Substring(0, txtObsApr.Text.Length).ToUpper();
            }
            if (txtObs3.Text != "")
            {
                vControlCreditos.observaciones = this.txtObs3.Text.Length >= 250 ? txtObs3.Text.Substring(0, 249) : txtObs3.Text.Substring(0, txtObs3.Text.Length).ToUpper();
            }
            if (txtObs4.Text != "")
            {
                vControlCreditos.observaciones = this.txtObs4.Text.Length >= 250 ? txtObs4.Text.Substring(0, 249) : txtObs4.Text.Substring(0, txtObs4.Text.Length).ToUpper();
            }
            if (txtObs5.Text != "")
            {
                vControlCreditos.observaciones = this.txtObs5.Text.Length >= 250 ? txtObs5.Text.Substring(0, 249) : txtObs5.Text.Substring(0, txtObs5.Text.Length).ToUpper();
            }
        }
        else
        {
            vControlCreditos.observaciones = "0";
        }

        vControlCreditos.anexos = null;
        vControlCreditos.nivel = -1;

        if (Session["Datacredito"] == null)
        {
            if (FechaDatcaredito == "" || FechaDatcaredito == null || Session["Datacredito"] == "" || Session["Datacredito"].ToString() == "" || Session["Datacredito"] == null)
            {
                vControlCreditos.fechaconsulta_dat = FechaDatcaredito == "" ? DateTime.MinValue : Convert.ToDateTime(FechaDatcaredito.Trim());
            }
            else
            {
                if (FechaDatcaredito != null || FechaDatcaredito != "" || Session["Datacredito"] != null)
                {
                    FechaDatcaredito = Convert.ToString(Session["Datacredito"].ToString());
                    vControlCreditos.fechaconsulta_dat = Convert.ToDateTime(FechaDatcaredito);
                }
            }
        }

        vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, _usuario);

    }
    protected void chkSeleccionarTasa_CheckedChanged(object sender, EventArgs e)
    {

        CheckBoxGrid chkSeleccionarTasa = (CheckBoxGrid)sender;
        int posicion = Convert.ToInt32(chkSeleccionarTasa.CommandArgument);
        tasa = gvListaModalidadTasas.Rows[posicion].Cells[2].Text;
        TxtValorTasaCred.Text = tasa;

        int contador = 0;
        foreach (GridViewRow row in gvListaModalidadTasas.Rows)
        {
            if (posicion != contador)
            {
                CheckBox chkModalidadTasa = row.FindControl("chkSeleccionarTasa") as CheckBox;

                chkModalidadTasa.Checked = false;
                // chkTasa.Enabled = false;
            }
            if (chkSeleccionarTasa.Checked == true)
            {
                //  chkTasa.Enabled = false;    

            }
            else
            {
                chkTasa.Enabled = true;
            }
            contador += 1;
        }
    }
    protected void rbCalculoTasa_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanelFija.Visible = false;
        PanelHistorico.Visible = false;
        if (rbCalculoTasa.SelectedIndex == 0)
            PanelFija.Visible = true;
        if (rbCalculoTasa.SelectedIndex == 1)
            PanelHistorico.Visible = true;
        if (rbCalculoTasa.SelectedIndex == 2)
            PanelHistorico.Visible = true;
    }
    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        panelEmpresaRec.Visible = false;
        if (ddlFormaPago.SelectedIndex != 0)
        {
            List<CreditoEmpresaRecaudo> lstConsulta = new List<CreditoEmpresaRecaudo>();
            lstConsulta = consultasdatacreditoServicio.ListarPersona_Empresa_Recaudo(Convert.ToInt64(txtCodigo.Text), _usuario);
            if (lstConsulta.Count > 0)
            {
                panelEmpresaRec.Visible = true;
                gvEmpresaRecaudora.DataSource = lstConsulta;
                gvEmpresaRecaudora.DataBind();
            }
            if (lstConsulta.Count == 1)
            {
                TextBox txtPorcentaje = (TextBox)gvEmpresaRecaudora.Rows[0].FindControl("txtPorcentaje");
                txtPorcentaje.Text = "100";
                txtPorcentaje_OnTextChanged(null, null);
            }
        }
    }
    protected void txtPorcentaje_OnTextChanged(object sender, EventArgs e)
    {
        decimal numero = 0;
        TextBoxGrid txtporcentaje = (TextBoxGrid)sender;
        if (txtporcentaje == null)
        {
            txtporcentaje = (TextBoxGrid)gvEmpresaRecaudora.Rows[0].FindControl("txtPorcentaje");
        }
        if (txtporcentaje != null)
        {
            if (txtporcentaje.Text != null && txtporcentaje.Text != "")
            {
                numero = Convert.ToDecimal(txtporcentaje.Text);
                decimal numerodividio = numero;
                numero = numerodividio / 100;
            }
            if (txtCuota.Text != null && txtCuota.Text != "")
            {
                numero = Math.Round(numero * Convert.ToInt64(txtCuota.Text.Replace(".", "")));
            }

            int rowIndex = Convert.ToInt32(txtporcentaje.CommandArgument);
            TextBox txtValor = (TextBox)gvEmpresaRecaudora.Rows[rowIndex].FindControl("txtValor");
            txtValor.Text = "";
            if (numero != 0)
            {
                try
                {
                    if (txtValor != null)
                    {
                        txtValor.Text = numero.ToString("##,##");
                    }
                }
                catch { }
            }
        }
    }
    protected void chkRecoger_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            //VALIDACION SI CUMPLE EL MINIMO Y MAXIMO DE REFINANCIACION
            LineasCredito pDatosLinea = new LineasCredito();
            Xpinn.FabricaCreditos.Data.LineasCreditoData vData = new Xpinn.FabricaCreditos.Data.LineasCreditoData();

            CheckBoxGrid chkRecoger = (CheckBoxGrid)sender;
            int nItem = Convert.ToInt32(chkRecoger.CommandArgument);

            Decimal pVrTotal = 0, pMontoApro = 0;
            pMontoApro = txtMonto2.Text != "" ? Convert.ToDecimal(txtMonto2.Text.Replace(".", "")) : 0;

            foreach (GridViewRow rFila in gvListaSolicitudCreditosRecogidos.Rows)
            {
                CheckBoxGrid chkRecogerRow = (CheckBoxGrid)rFila.FindControl("chkRecoger");
                if (chkRecogerRow.Checked)
                    pVrTotal += rFila.Cells[8].Text != "&nbsp;" && rFila.Cells[8].Text != "" ? Convert.ToDecimal(rFila.Cells[8].Text.Replace(".", "")) : 0;

                if (chkRecoger.Checked)
                {
                    if (rFila.RowIndex == nItem)
                    {
                        if (rFila.Cells[0].Text != "")
                        {
                            List<ConsultaAvance> ListaDetalleAvance = new List<ConsultaAvance>();
                            ListaDetalleAvance = DetalleProducto.ListarAvances(long.Parse(rFila.Cells[0].Text), (Usuario)Session["Usuario"]);
                            if (ListaDetalleAvance.Count > 0)
                            {
                                gvAvances.DataSource = ListaDetalleAvance;
                                gvAvances.DataBind();
                                MpeDetalleAvances.Show();
                                rFila.Cells[8].Text = "0";
                                //rFila.Cells[5].Text = "0";
                                //rFila.Cells[4].Text = "0";
                                Session["NumProducto"] = rFila.Cells[0].Text;
                            }
                        }


                        //CAPTURAR EL CODIGO DE LA LINEA
                        string Linea = rFila.Cells[2].Text;
                        string[] sDatos = Linea.ToString().Split('-');
                        string cod_linea = sDatos[0].ToString();
                        if (cod_linea != "")
                        {
                            pDatosLinea = vData.ConsultaLineaCredito(cod_linea, _usuario);
                            //VARIABLES A VALIDAR
                            decimal minimo = 0, maximo = 0;
                            minimo = Convert.ToDecimal(pDatosLinea.minimo_refinancia);
                            maximo = Convert.ToDecimal(pDatosLinea.maximo_refinancia);
                            if (pDatosLinea.tipo_refinancia == 0) //SI ES POR RANGO DE SALDO
                            {
                                //RECUPERAR SALDO CAPITAL
                                decimal saldoCapital = 0;
                                if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")
                                    saldoCapital = Convert.ToDecimal(rFila.Cells[4].Text);
                                if (saldoCapital < minimo || saldoCapital > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que el Saldo Capital esta fuera del Rango establecido");
                                }
                            }
                            else if (pDatosLinea.tipo_refinancia == 2) //SI ES POR % SALDO
                            {
                                //RECUPERAR SALDO CAPITAL/MONTO
                                decimal saldoCapital = 0, monto = 0, valor = 0;
                                if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")
                                    saldoCapital = Convert.ToDecimal(rFila.Cells[4].Text);
                                if (rFila.Cells[3].Text != "" && rFila.Cells[3].Text != "&nbsp;")
                                    monto = Convert.ToDecimal(rFila.Cells[3].Text);
                                valor = saldoCapital / monto;
                                if (valor < minimo || valor > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que el valor calculado esta fuera del Rango establecido");
                                }
                            }
                            else if (pDatosLinea.tipo_refinancia == 3) // SI ES POR % CUOTAS PAGAS
                            {
                                //RECUPERAR LAS CUOTAS PAGADAS
                                decimal cuotas = 0;
                                if (rFila.Cells[9].Text != "" && rFila.Cells[9].Text != "&nbsp;")
                                    cuotas = Convert.ToDecimal(rFila.Cells[9].Text);
                                if (cuotas < minimo || cuotas > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que las cuotas Pagadas estan fuera del Rango establecido");
                                }
                            }
                        }
                    }
                }
            }
            if (pVrTotal != 0)
            {
                if (pVrTotal > pMontoApro)
                {
                    decimal valorNoCapitalizado = creditoRecogerServicio.ConsultarValorNoCapitalizado(txtCredito.Text, _usuario);

                    pVrTotal -= valorNoCapitalizado;

                    if (pVrTotal > pMontoApro)
                    {
                        lblError.Visible = true;
                        lblError.Text = ("El monto total a recoger supera el monto solicitado de [ $ " + pMontoApro.ToString("n0") + " ].");
                    }
                }
            }
            CalcularTotalRecoge();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    protected void rbTipoAprobacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        panelAprobacion.Visible = false;
        panelAprobacionCom.Visible = false;
        panelAplazamiento.Visible = false;
        panelNegar.Visible = false;
        if (rbTipoAprobacion.SelectedItem.Value == "1")
        {
            Usuario usuap = _usuario;
            if (usuap.tipo == 2)
            {
                lbledad.Visible = false;
                txtEdad.Visible = false;
                mvAprobacion.ActiveViewIndex = 0;
                Lblaprob.Text = "APROBACIÓN DE CRÉDITOS";
                panelAprobacion.Visible = false;
            }
            if (tipoempresa == 1)
            {
                lbledad.Visible = true;
                mvAprobacion.ActiveViewIndex = 0;
                Lblaprob.Text = "APROBACIÓN DE CRÉDITOS MODIFICANDO CONDICIONES";
            }
            panelAprobacionCom.Visible = true;
        }
        else if (rbTipoAprobacion.SelectedItem.Value == "2")
        {
            panelAplazamiento.Visible = true;
        }
        else if (rbTipoAprobacion.SelectedItem.Value == "3")
        {
            panelNegar.Visible = true;
        }
    }
    protected void btnGenerarCarta_Click(object sender, EventArgs e)
    {
        ctlFormatos.lblErrorText = "";
        if (ctlFormatos.ddlFormatosItem != null)
            ctlFormatos.ddlFormatosIndex = 0;
        ctlFormatos.MostrarControl();
    }
    protected void btnImpresion_Click(object sender, EventArgs e)
    {
        try
        {
            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Documentos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
            }
            catch
            { }
            string pRuta = "~/Page/FabricaCreditos/CreditosPorAprobar/Documentos/";
            string pVariable = txtCredito.Text.Trim();
            if (!ctlFormatos.ImprimirFormato(pVariable, pRuta))
                return;

            //Descargando el Archivo PDF
            string cNombreDeArchivo = pVariable.Trim() + "_" + ctlFormatos.ddlFormatosValue + ".pdf";
            string cRutaLocalDeArchivoPDF = Server.MapPath("Documentos\\" + cNombreDeArchivo);

            if (GlobalWeb.bMostrarPDF == true)
            {
                // Copiar el archivo a una ruta local
                try
                {
                    FileStream fs = File.OpenRead(cRutaLocalDeArchivoPDF);
                    if (fs.Length <= 0)
                    {
                        ctlFormatos.lblErrorText = cRutaLocalDeArchivoPDF;
                        ctlFormatos.lblErrorIsVisible = true;
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                    }
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    fs.Close();
                    Session["Archivo" + Usuario.codusuario] = cRutaLocalDeArchivoPDF;
                    panelReporte.Visible = true;
                }
                catch (Exception ex)
                {
                    ctlFormatos.lblErrorText = ex.Message;
                    ctlFormatos.lblErrorIsVisible = true;
                }
            }
            else
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = false;
                Response.AddHeader("Content-Disposition", "attachment;filename=" + cNombreDeArchivo);
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.WriteFile(cRutaLocalDeArchivoPDF);
                Response.End();
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
        catch (Exception ex)
        {
            ctlFormatos.lblErrorIsVisible = true;
            ctlFormatos.lblErrorText = ex.Message;
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
    }
    protected void btnVerData_Click(object sender, EventArgs e)
    {
        panelReporte.Visible = false;
    }
    protected void btnCorreo_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");

            TiposDocCobranzasServices _tipoDocumentoServicio = new TiposDocCobranzasServices();

            Empresa empresa = _tipoDocumentoServicio.ConsultarCorreoEmpresa(_usuario.idEmpresa, _usuario);

            Xpinn.Asesores.Entities.Persona persona = _tipoDocumentoServicio.ConsultarCorreoPersona(Convert.ToInt64(txtCodigo.Text), _usuario);

            ParametroCorreo parametroCorreo = (ParametroCorreo)Enum.Parse(typeof(ParametroCorreo), hiddenOperacionCredito.Value);

            TiposDocCobranzas modificardocumento = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo((int)parametroCorreo, _usuario);

            if (string.IsNullOrWhiteSpace(persona.Email))
            {
                VerError("El asociado no tiene email registrado");
                return;
            }
            else if (string.IsNullOrWhiteSpace(empresa.e_mail) || string.IsNullOrWhiteSpace(empresa.clave_e_mail))
            {
                VerError("La empresa no tiene configurado un email para enviar el correo");
                return;
            }
            else if (string.IsNullOrWhiteSpace(modificardocumento.texto))
            {
                VerError("No esta parametrizado el formato del correo a enviar");
                return;
            }

            LlenarDiccionarioGlobalWebParaCorreo();

            modificardocumento.texto = ReemplazarParametrosEnElMensajeCorreo(modificardocumento.texto);

            CorreoHelper correoHelper = new CorreoHelper(persona.Email, empresa.e_mail, empresa.clave_e_mail);
            bool exitoso = correoHelper.EnviarCorreoConHTML(modificardocumento.texto, Correo.Gmail, modificardocumento.descripcion, _usuario.empresa);

            if (!exitoso)
            {
                VerError("Error al enviar el correo");
                return;
            }

            btnCorreo.Text = "Envio Satisfactorio";
            btnCorreo.Enabled = false;
        }
        catch (Exception ex)
        {
            VerError("Error al enviar el correo, " + ex.Message);
        }
    }
    public void btnImprimirAprobacion_Click(object sender, EventArgs e)
    {
        // CREANDO DATA TABLES
        DataTable tableCodeudor = new DataTable();
        tableCodeudor.Columns.Add("codpersona");
        tableCodeudor.Columns.Add("identificacion");
        tableCodeudor.Columns.Add("nombre");
        tableCodeudor.Columns.Add("direccion");
        tableCodeudor.Columns.Add("telefono");
        string pHidden = "true";
        if (gvListaCodeudores.Rows.Count > 0)
        {
            long codPersona = 0;
            string caractercampo = string.Empty;
            Label lblContenido;
            foreach (GridViewRow rFilaCod in gvListaCodeudores.Rows)
            {
                lblContenido = (Label)rFilaCod.FindControl("lblCodPersona");
                codPersona = !string.IsNullOrEmpty(lblContenido.Text) ? Convert.ToInt64(lblContenido.Text) : 0;
                if (codPersona != 0)
                {
                    pHidden = "false";
                    DataRow data;
                    data = tableCodeudor.NewRow();
                    data[0] = codPersona;
                    data[1] = ((Label)rFilaCod.FindControl("lblidentificacion")).Text;
                    caractercampo = ((Label)rFilaCod.FindControl("lblPrimerNombre")).Text + " " + ((Label)rFilaCod.FindControl("lblSegundoNombre")).Text;
                    caractercampo += ((Label)rFilaCod.FindControl("lblPrimerApellido")).Text + " " + ((Label)rFilaCod.FindControl("lblSegundoApellido")).Text;
                    data[2] = caractercampo;
                    lblContenido = (Label)rFilaCod.FindControl("lblDireccionRow");
                    data[3] = lblContenido.Text;
                    lblContenido = (Label)rFilaCod.FindControl("lblTelefonoRow");
                    data[4] = lblContenido.Text;
                    tableCodeudor.Rows.Add(data);
                }
            }
        }

        DataTable tableRecogidos = new DataTable();
        tableRecogidos.Columns.Add("numero_radicacion");
        tableRecogidos.Columns.Add("linea");
        tableRecogidos.Columns.Add("monto");
        tableRecogidos.Columns.Add("saldocapital");
        tableRecogidos.Columns.Add("interes");
        tableRecogidos.Columns.Add("otros");
        tableRecogidos.Columns.Add("valortotal");
        tableRecogidos.Columns.Add("cuotaspagadas");
        tableRecogidos.Columns.Add("interesmora");

        if (gvListaSolicitudCreditosRecogidos.Rows.Count > 0)
        {
            bool pExisteRecogidos = gvListaSolicitudCreditosRecogidos.Rows.OfType<GridViewRow>().Where(x => ((CheckBoxGrid)x.FindControl("chkRecoger")).Checked == true).Any();
            if (pExisteRecogidos)
            {
                string caractercampo = string.Empty;
                foreach (GridViewRow rFilaReco in gvListaSolicitudCreditosRecogidos.Rows)
                {
                    DataRow data;
                    data = tableRecogidos.NewRow();
                    caractercampo = rFilaReco.Cells[1].Text.Replace("&nbsp;", "");
                    data[0] = caractercampo;
                    caractercampo = rFilaReco.Cells[2].Text.Replace("&nbsp;", "");
                    data[1] = caractercampo;
                    caractercampo = rFilaReco.Cells[3].Text.Replace("&nbsp;", "");
                    data[2] = caractercampo;
                    caractercampo = rFilaReco.Cells[4].Text.Replace("&nbsp;", "");
                    data[3] = caractercampo;
                    caractercampo = rFilaReco.Cells[5].Text.Replace("&nbsp;", "");
                    data[4] = caractercampo; // INTERES
                    caractercampo = rFilaReco.Cells[7].Text.Replace("&nbsp;", "");
                    data[5] = caractercampo; // OTROS
                    caractercampo = rFilaReco.Cells[8].Text.Replace("&nbsp;", "");
                    data[6] = caractercampo; // TOTAL
                    caractercampo = rFilaReco.Cells[9].Text.Replace("&nbsp;", "");
                    data[7] = caractercampo; // CUOTAS PAGAS
                    caractercampo = rFilaReco.Cells[6].Text.Replace("&nbsp;", "");
                    data[8] = caractercampo; // INTERES MORA
                    tableRecogidos.Rows.Add(data);
                }
            }
        }

        //REGISTRANDO PARAMETROS
        ReportParameter[] param = new ReportParameter[25];
        param[0] = new ReportParameter("Entidad", Usuario.empresa);
        param[1] = new ReportParameter("ImagenReport", ImagenReporte());
        param[2] = new ReportParameter("usuarioGenera", Usuario.nombre);
        param[3] = new ReportParameter("CodPersona", txtCodigo.Text);
        param[4] = new ReportParameter("Identificacion", txtId.Text);
        param[5] = new ReportParameter("Nombre", txtNombres.Text);
        param[6] = new ReportParameter("NroRadicacion", txtCredito.Text);
        param[7] = new ReportParameter("Linea", txtLinea.Text);
        param[8] = new ReportParameter("MontoSolicitado", txtMonto.Text);
        param[9] = new ReportParameter("Plazo", txtPlazo.Text);
        param[10] = new ReportParameter("Cuota", txtCuota.Text);
        param[11] = new ReportParameter("FormaPago", txtFormaPago.Text);
        param[12] = new ReportParameter("Asesor", txtAsesor.Text);
        param[13] = new ReportParameter("Periodicidad", txtPeriodicidad.Text);
        param[14] = new ReportParameter("Destino", ddlDestino.SelectedItem.Text);
        param[15] = new ReportParameter("ObservacionSolici", txtObsSoli.Text);
        param[16] = new ReportParameter("fechahora", " " + DateTime.Now);
        string pEstadoCredito = rbTipoAprobacion.SelectedValue == "1" ? "APROBADO" : rbTipoAprobacion.SelectedValue == "2" ? "APLAZADO" : "NEGADO";
        param[17] = new ReportParameter("EstadoCredito", pEstadoCredito);
        param[18] = new ReportParameter("ObservacionAproba", " " + txtObs3.Text);
        param[19] = new ReportParameter("MontoAprobado", txtMonto2.Text);
        param[20] = new ReportParameter("PlazoAprobado", txtPlazo2.Text + " " + ddlPlazo.SelectedItem.Text);
        param[21] = new ReportParameter("FecPrimerPago", ucFechaPrimerPago.Text);
        param[22] = new ReportParameter("FormaPagoAprobacion", ddlFormaPago.SelectedItem.Text);
        param[23] = new ReportParameter("InfoCodeudores", pHidden);
        pHidden = gvListaSolicitudCreditosRecogidos.Rows.OfType<GridViewRow>().Where(x => ((CheckBoxGrid)x.FindControl("chkRecoger")).Checked == true).Count() > 0 ? "false" : "true";
        param[24] = new ReportParameter("InfoRecogidos", pHidden);
        RptAprobacion.LocalReport.EnableExternalImages = true;
        RptAprobacion.LocalReport.SetParameters(param);

        RptAprobacion.LocalReport.DataSources.Clear();
        ReportDataSource rds1 = new ReportDataSource("DataSet1", tableCodeudor);
        RptAprobacion.LocalReport.DataSources.Add(rds1);
        ReportDataSource rds2 = new ReportDataSource("DataSet2", tableRecogidos);
        RptAprobacion.LocalReport.DataSources.Add(rds2);
        RptAprobacion.LocalReport.Refresh();

        var bytes = RptAprobacion.LocalReport.Render("PDF");

        string pNameFile = Usuario.codusuario + txtCredito.Text.Trim() + ".pdf";
        string pRuta = Server.MapPath("Documentos\\" + pNameFile);

        using (FileStream fs = new FileStream(pRuta, FileMode.Create, FileAccess.Write))
        {
            fs.Write(bytes, 0, bytes.Length);
        }

        FileStream archivo = new FileStream(pRuta, FileMode.Open, FileAccess.Read);
        FileInfo file = new FileInfo(pRuta);

        Response.Clear();
        Response.AppendHeader("Content-Disposition", "attachment; filename=ReporteAprobacion.pdf");
        Response.AddHeader("Content-Length", file.Length.ToString());
        Response.ContentType = "application/pdf";
        Response.TransmitFile(file.FullName);
        Response.End();

    }
    protected void CkcAfiancol_OnCheckedChanged(object sender, EventArgs e)
    {

        var Cod = LineasCreditoServicio.ddlatributo((Usuario)Session["Usuario"])
                .Where(x => x.cod_atr == cod_afiancol).Select(x => x.cod_atr).FirstOrDefault();
        if (Cod == 0)
        {
            lblError.Text = @"No se encuentra el atributo de AFIANCOL Creado.";
            CkcAfiancol.Checked = false;
        }
    }

    private void LlenarDiccionarioGlobalWebParaCorreo()
    {
        parametrosFormatoCorreo = new Dictionary<ParametroCorreo, string>();
        string plazoCredito = string.Empty;
        string montoCredito = string.Empty;
        string lineaCredito = string.Empty;

        parametrosFormatoCorreo.Add(ParametroCorreo.NumeroRadicacion, txtCredito.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.Identificacion, txtId.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.NombreCompletoPersona, txtNombres.Text);


        if (hiddenOperacionCredito.Value == ((int)TipoDocumentoCorreo.CreditoAprobado).ToString())
        {
            plazoCredito = txtPlazo2.Text;
            montoCredito = txtMonto2.Text;
            lineaCredito = ddlLineaCredito.SelectedItem.Text;
            parametrosFormatoCorreo.Add(ParametroCorreo.FechaCredito, ucFechaPrimerPago.Texto);
        }
        else //Todo menos aprobado
        {
            plazoCredito = txtPlazo.Text;
            montoCredito = txtMonto.Text;
            lineaCredito = txtLinea.Text;
        }

        parametrosFormatoCorreo.Add(ParametroCorreo.PlazoCredito, plazoCredito);
        parametrosFormatoCorreo.Add(ParametroCorreo.MontoCredito, montoCredito);
        parametrosFormatoCorreo.Add(ParametroCorreo.LineaCredito, lineaCredito);
    }
    protected void gvAtributos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LineasCreditoService Atributosservicio = new LineasCreditoService();
            HistoricoTasaService HistoricoServicio = new HistoricoTasaService();
            TipoTasaService TasaServicio = new TipoTasaService();

            DropDownList ddltipotasa = (DropDownList)e.Row.FindControl("ddltipotasa");
            if (ddltipotasa != null)
            {
                TipoTasa tasa = new TipoTasa();
                ddltipotasa.DataSource = TasaServicio.ListarTipoTasa(tasa, (Usuario)Session["usuario"]);
                ddltipotasa.DataTextField = "NOMBRE";
                ddltipotasa.DataValueField = "COD_TIPO_TASA";
                ddltipotasa.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddltipotasa.SelectedIndex = 0;
                ddltipotasa.DataBind();
            }

            Label lbltipotasa = (Label)e.Row.FindControl("lbltipotasa");
            if (lbltipotasa.Text != "")
                ddltipotasa.SelectedValue = lbltipotasa.Text;


        }
    }
    protected void gvServiciosRecogidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvServiciosRecogidos.PageIndex = e.NewPageIndex;
        ConsultarServiciosMarcadosParaRecoger();
    }
    protected void btnCloseAct2_Click(object sender, EventArgs e)
    {
        Decimal pVrTotal = 0;
        Decimal pVrTotalInt = 0;
        Decimal pVrTotalCapital = 0;

        String NomPro = Session["NumProducto"].ToString();
        foreach (GridViewRow rFila2 in gvListaSolicitudCreditosRecogidos.Rows)
        {
            if (NomPro == rFila2.Cells[0].Text)
            {
                pVrTotal = Convert.ToInt64(TxtTotalAvances.Text == "" ? "0" : TxtTotalAvances.Text);
                pVrTotalCapital = Convert.ToInt64(TxtTotalCap.Text == "" ? "0" : TxtTotalCap.Text);
                pVrTotalInt = Convert.ToInt64(TxtTotalInt.Text == "" ? "0" : TxtTotalInt.Text);
                rFila2.Cells[8].Text = pVrTotal.ToString();
                rFila2.Cells[5].Text = pVrTotalInt.ToString();
                rFila2.Cells[4].Text = pVrTotalCapital.ToString();
            }
        }
        MpeDetalleAvances.Hide();
        CalcularTotalRecoge();
    }

    #endregion

    #region Metodos de Validacion
    protected Boolean Validarestado(Int64 pCodPersona, String pIdentificacion)
    {
        Session["ES_EMPLEADO"] = "false";
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Boolean resultado = true;
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        vPersona1.seleccionar = "Identificacion";
        vPersona1.identificacion = pIdentificacion;
        vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, _usuario);
        if (vPersona1.estado == null)
        {
            vPersona1 = DatosClienteServicio.ConsultarPersona1(pCodPersona, _usuario);
            if (vPersona1.estado != null)
            {
                if (vPersona1.estado != Convert.ToString('A'))
                {
                    VerError("Su estado no esta activo");
                    resultado = false;
                }
                else
                    Session["ES_EMPLEADO"] = "true";
            }
        }
        else
        {
            if (vPersona1.estado != Convert.ToString('A'))
            {
                VerError("Su estado no esta activo");
                resultado = false;
            }
        }
        return resultado;
    }
    void ValidarPersonaVacaciones(Int64 pCod_Persona)
    {
        if (pCod_Persona == 0)
            return;
        Xpinn.Tesoreria.Services.EmpresaNovedadService RecaudoService = new Xpinn.Tesoreria.Services.EmpresaNovedadService();
        Xpinn.Tesoreria.Entities.EmpresaNovedad pPersonaVac = new Xpinn.Tesoreria.Entities.EmpresaNovedad();
        string pFiltro = " where vac.cod_persona = " + pCod_Persona + " order by vac.fecha_novedad desc";
        pPersonaVac = RecaudoService.ConsultarPersonaVacaciones(pFiltro, Usuario);



        if (pPersonaVac != null)
        {
            if (pPersonaVac.cod_persona > 0)
            {
                if (pPersonaVac.fechacreacion != null && pPersonaVac.fecha_inicial != null && pPersonaVac.fecha_final != null)
                {
                    DateTime pFechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    if (pPersonaVac.fechacreacion <= pFechaActual && pPersonaVac.fecha_final >= pFechaActual)
                    {
                        string lblErrorAnt = ((Label)Master.FindControl("lblError")).Text;
                        lblErrorAnt = string.IsNullOrEmpty(lblErrorAnt) ? string.Empty : "<li>" + lblErrorAnt + "</li>";
                        VerError(lblErrorAnt + "<li>La persona tiene un periodo de vacaciones del [ " + Convert.ToDateTime(pPersonaVac.fecha_inicial).ToString(gFormatoFecha) + " al " + Convert.ToDateTime(pPersonaVac.fecha_final).ToString(gFormatoFecha) + " ]</li>");
                    }
                }
            }
        }
    }

    private void CalcularTotalRecoge()
    {
        Decimal pVrTotal = 0;
        foreach (GridViewRow rFila in gvListaSolicitudCreditosRecogidos.Rows)
        {
            CheckBoxGrid chkRecogerRow = (CheckBoxGrid)rFila.FindControl("chkRecoger");
            if (chkRecogerRow.Checked)
            {
                pVrTotal += rFila.Cells[8].Text != "&nbsp;" && rFila.Cells[8].Text != "" ? Convert.ToDecimal(rFila.Cells[8].Text.Replace(".", "")) : 0;
                decimal valorNominas = rFila.Cells[8].Text != "&nbsp;" && rFila.Cells[12].Text != "" ? Convert.ToDecimal(rFila.Cells[12].Text.Replace(".", "")) : 0;
                pVrTotal -= valorNominas;
                if (pVrTotal < 0)
                    pVrTotal = 0;
            }
        }

        foreach (GridViewRow row in gvServiciosRecogidos.Rows)
        {
            string valorTotalTexto = HttpUtility.HtmlDecode(row.Cells[9].Text);
            decimal valorTotal = 0;

            Decimal.TryParse(valorTotalTexto, out valorTotal);

            pVrTotal += valorTotal;
        }

        txtVrTotRecoger.Text = "0";
        txtVrDesembolsar.Text = txtMonto.Text.Replace(".", "");
        if (pVrTotal != 0)
        {
            txtVrTotRecoger.Text = pVrTotal.ToString();
            if (txtMonto.Text != "" && txtMonto.Text != "0")
                txtVrDesembolsar.Text = (Convert.ToDecimal(txtMonto.Text.Replace(".", "")) - pVrTotal).ToString(); ;
        }
    }

    Boolean ValidarAprobacion()
    {
        lblCalculoTasa.Visible = false;
        lblError.Visible = false;
        lblError.Text = "";
        if (ddldestino2.SelectedValue == "" || ddldestino2.SelectedValue == "0")
        {
            lblError.Visible = true;
            lblError.Text = "Debe Ingresar la destinacion que desea actualizar";
            return false;
        }
        if (ddlFormaPago.SelectedIndex == 1 && gvEmpresaRecaudora.Rows.Count > 0)// si la forma de pago es por NOMINA
        {
            List<CreditoEmpresaRecaudo> lstDatos = new List<CreditoEmpresaRecaudo>();
            lstDatos = ObtenerListaEmpresaRecaudadora();

            decimal porcentaje = 0;
            int cont = 0;
            foreach (CreditoEmpresaRecaudo rCred in lstDatos)
            {
                if (lstDatos[cont].porcentaje != 0)
                    porcentaje += lstDatos[cont].porcentaje;
                cont++;
            }
            if (porcentaje > 100)
            {
                lblError.Visible = true;
                lblError.Text = "El valor total del Porcentaje no puede ser mayor al 100%";
                return false;
            }
            else if (porcentaje < 100)
            {
                lblError.Visible = true;
                lblError.Text = "El valor total del porcentaje debe ser el 100%";
                return false;
            }
            else if (porcentaje == 0)
            {
                lblError.Visible = true;
                lblError.Text = "Debe Ingresar el valor de porcentaje";
                return false;
            }
        }

        Decimal pVrTotal = 0, pMontoApro = 0;
        pMontoApro = txtMonto2.Text != "" ? Convert.ToDecimal(txtMonto2.Text.Replace(".", "")) : 0;
        foreach (GridViewRow rFila in gvListaSolicitudCreditosRecogidos.Rows)
        {
            CheckBoxGrid chkRecogerRow = (CheckBoxGrid)rFila.FindControl("chkRecoger");
            if (chkRecogerRow.Checked)
            {
                pVrTotal += rFila.Cells[8].Text != "&nbsp;" && rFila.Cells[8].Text != "" ? Convert.ToDecimal(rFila.Cells[8].Text.Replace(".", "")) : 0;
                decimal valorNominas = rFila.Cells[12].Text != "&nbsp;" && rFila.Cells[12].Text != "" ? Convert.ToDecimal(rFila.Cells[12].Text.Replace(".", "")) : 0;
                pVrTotal -= valorNominas;
                if (pVrTotal < 0) pVrTotal = 0;
            }
        }

        foreach (GridViewRow row in gvServiciosRecogidos.Rows)
        {
            string valorTotalTexto = HttpUtility.HtmlDecode(row.Cells[9].Text);
            decimal valorTotal = 0;

            Decimal.TryParse(valorTotalTexto, out valorTotal);

            pVrTotal += valorTotal;
        }

        if (pVrTotal != 0)
        {
            if (pVrTotal > pMontoApro)
            {
                decimal valorNoCapitalizado = creditoRecogerServicio.ConsultarValorNoCapitalizado(txtCredito.Text, _usuario);

                pVrTotal -= valorNoCapitalizado;

                if (pVrTotal > pMontoApro)
                {
                    lblError.Visible = true;
                    lblError.Text = ("El monto total a recoger supera el monto solicitado de [ $ " + pMontoApro.ToString("n0") + " ].");
                    return false;
                }
            }
        }

        if (chkTasa.Checked)
        {
            if (rbCalculoTasa.SelectedItem == null)
            {
                lblError.Visible = true;
                lblError.Text = "Seleccione una Opcion";
                lblCalculoTasa.Visible = true;
                return false;
            }
        }

        if (chkDistribuir.Checked)
        {
            List<Credito_Giro> lstCant = new List<Credito_Giro>();
            lstCant = ObtenerListaDistribucion();
            if (lstCant.Count == 0)
            {
                lblError.Visible = true;
                lblError.Text = "No agrego ninguna distribución de giros. Ingrese mínimo una distribución.";
                return false;
            }
            else
            {
                CalculaTotalXColumna();
            }
        }
        if (ctlBusquedaProveedor.VisibleCtl == true && ctlBusquedaProveedor.CheckedOrd == true)
        {
            if (ctlBusquedaProveedor.TextIdentif == "")
            {
                lblError.Visible = true;
                lblError.Text = "Debe de ingresar la identificación del proveedor.";
                return false;
            }
            if (ctlBusquedaProveedor.TextIdentif != "" && ctlBusquedaProveedor.TextNomProv == "")
            {
                lblError.Visible = true;
                lblError.Text = "Debe de ingresar una identificación correcta del proveedor.";
                return false;
            }
        }

        foreach (GridViewRow items in gvListaCodeudores.Rows)
        {
            //Valida que los numeros de orden no esten repetidos
            if (_validacion != null)
                _validaCodeudores = _validacion == ((Label)items.FindControl("lblOrdenRow")).Text;
            _validacion = ((Label)items.FindControl("lblOrdenRow")).Text;
        }

        if (_validaCodeudores)
        {
            VerError("No se ingresó de manera correcta la información de los codeudores, verifique la orden de cada codeudor.");
            return false;
        }

        long codeudor = LineasCreditoServicio.ConsultarLineasCredito(Convert.ToString(ddlLineaCredito.SelectedValue), _usuario).numero_codeudores;
        if (!CkcAfiancol.Checked)
        {
            if (codeudor >= 1 && codeudor > gvListaCodeudores.Rows.Count)
            {
                VerError("Para esta linea de credito es necesario " + codeudor + " Codeudores. Ha registrado " + gvListaCodeudores.Rows.Count + " codeudores");
                return false;
            }
        }
        return true;
    }
    protected Int32? ValorSeleccionado(DropDownList ddlControl)
    {
        if (ddlControl != null)
            if (ddlControl.SelectedValue != null)
                if (ddlControl.SelectedValue != "")
                    return Convert.ToInt32(ddlControl.SelectedValue);
        return null;
    }
    protected decimal? ValorSeleccionado(TextBox txtControl)
    {
        if (txtControl != null)
            if (txtControl.Text != null)
                if (txtControl.Text != "")
                    return ConvertirStringToDecimal(txtControl.Text);
        return null;
    }
    protected Int32? ValorSeleccionado(Label txtControl)
    {
        if (txtControl != null)
            if (txtControl.Text != null)
                if (txtControl.Text != "")
                    return Convert.ToInt32(txtControl.Text);
        return null;
    }
    protected int ValorSeleccionado(CheckBox txtControl)
    {
        if (txtControl != null)
            if (txtControl.Checked != null)
                return Convert.ToInt32(txtControl.Checked);
        return 0;
    }

    void ValidatasaUsuara(CreditoSolicitado pcredito, List<LineasCredito> lstAtributos)
    {
        valorParametro = ConsultarParametroGeneral(981, (Usuario)Session["usuario"]);
        decimal totalAtributos = 0;
        var valido = 0;
        creditoc.Numero_radicacion = pcredito.NumeroCredito;
        creditoc = creditoPlanServicio.ConsultarCredito(creditoc.Numero_radicacion, true, (Usuario)Session["usuario"]);
        clasificacionCartera = clasificacionCarteraService.ConsultarClasificacion(creditoc.cod_clasifica,
            (Usuario)Session["usuario"]);
        lsHistoricoTasas = historicoTasaService.listarhistorico(clasificacionCartera.tipo_historico, (Usuario)Session["usuario"]);
        foreach (HistoricoTasa historicoTasa in lsHistoricoTasas)
        {
            if (creditoc.FechaAprobacion >= historicoTasa.FECHA_INICIAL  || creditoc.FechaAprobacion <= historicoTasa.FECHA_FINAL)
            {
                historico.VALOR = historicoTasa.VALOR;
                valido = 1;
                break;
            }
        }

        foreach (LineasCredito item in lstAtributos)
        {
            if (item.tasa > 0)
            {
                totalAtributos += Convert.ToDecimal(item.tasa);
            }
        }

        if (valido == 1)
        {
            if (totalAtributos > historico.VALOR)
            {
                var texto = "";
                texto = "Las tasas parametrizadas en el crédito superan la tasa de usura. ";
                labelWarning.InnerText = texto;
                labelWarning.Visible = true;
            }
        }
        else
        {
            VerError("No hay parametrización de tasa de usura actualmente.");
            btnAcpAproModif.Visible = false;
            btnCncAproModif.Visible = false;
        }
    }
    #endregion

    #region Metodos obtener datos
    //filtra los datos
    private consultasdatacredito ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.consultasdatacredito vconsultasdatacredito = new Xpinn.FabricaCreditos.Entities.consultasdatacredito();

        if (txtId.Text != "")
            vconsultasdatacredito.cedulacliente = txtId.Text;

        return vconsultasdatacredito;
    }
    //Muestra los datos para aprobar
    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Usuario pUsuario = (Usuario)Session["usuario"];

            if (pIdObjeto != null)
            {
                credito.NumeroCredito = Int64.Parse(pIdObjeto);
                credito.cod_linea_credito = Convert.ToString(Session["CodLinea"]);
                credito = creditoServicio.ConsultarCredito(credito, _usuario);

                //Agregado para consultar proceso anterior a Aprobado según la parametrización de la entidad  

                string estado = "Aprobado";
                if (pUsuario.idioma == "1")
                {
                    estado = "Approuve";
                }
                else if (pUsuario.idioma == "2")
                {
                    estado = "Approved";
                }
                else
                {
                    estado = "Aprobado";
                }

                control = CreditoSolicitadoServicio.ConsultarProcesoAnterior(estado, (Usuario)Session["usuario"]);
                if (control.estado != null && control.estado != "")
                    credito.estado = control.estado;
                else
                    control.estado = credito.estado;

                if (credito.LineaCredito != null) //Se muestra el detalle sólo si este existe
                {

                    if (credito.estado != "S" && credito.estado != "L" && credito.estado != "W" &&
                        credito.estado != "V" && credito.estado != "G")
                    {
                        VerError("El crédito no esta en el estado correspondiente para poder ser aprobado. " +
                                 credito.estado);
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarConsultar(false);
                        btnAcpAprobar.Visible = false;
                        btnAcpAproModif.Visible = false;
                        btnAcpAplazar.Visible = false;
                        btnAcpNegar.Visible = false;
                        btnCncAprobar.Visible = false;
                        btnCncNegar.Visible = false;
                        btnCncAproModif.Visible = false;
                        gvListaCodeudores.Enabled = false;
                        gvListaSolicitudCreditosRecogidos.Enabled = false;
                        gvListaModalidadTasas.Enabled = false;
                        CuotasExtras.Visible = false;
                        txtObsApr.Enabled = false;
                    }

                    Validarestado(credito.CodigoCliente, credito.Identificacion);

                    Session["Cod_persona"] = credito.CodigoCliente;
                    Session["TipoCredito"] = credito.cod_clasifica;

                    DropLineaCredito();
                    if (!string.IsNullOrEmpty(credito.NumeroCredito.ToString()))
                        txtCredito.Text = HttpUtility.HtmlDecode(credito.NumeroCredito.ToString());
                    if (!string.IsNullOrEmpty(credito.Calificacion.ToString()))
                        txtCalificacion.Text = HttpUtility.HtmlDecode(credito.Calificacion.ToString());
                    if (!string.IsNullOrEmpty(credito.CodigoCliente.ToString()))
                        txtCodigo.Text = HttpUtility.HtmlDecode(credito.CodigoCliente.ToString());
                    if (!string.IsNullOrEmpty(credito.Concepto))
                        txtPropuesta.Text = HttpUtility.HtmlDecode(credito.Concepto.ToString());
                    if (!string.IsNullOrEmpty(credito.Cuota.ToString()))
                        txtCuota.Text = HttpUtility.HtmlDecode(credito.Cuota.ToString());
                    if (!string.IsNullOrEmpty(credito.Disponible.ToString()))
                        txtDisp.Text = HttpUtility.HtmlDecode(credito.Disponible.ToString());
                    if (!string.IsNullOrEmpty(credito.forma_pago.ToString()))
                        txtFormaPago.Text = HttpUtility.HtmlDecode(credito.forma_pago.ToString());
                    if (!string.IsNullOrEmpty(credito.fecha_primer_pago.ToString()))
                        ucFechaPrimerPago.ToDateTime =
                            Convert.ToDateTime(HttpUtility.HtmlDecode(credito.fecha_primer_pago.ToString()));
                    if (credito.fecha_primer_pago != null)
                        ucFechaPrimerPago.ToDateTime = Convert.ToDateTime(credito.fecha_primer_pago);
                    else
                    {
                        try
                        {
                            CreditoService CreditoServicio = new CreditoService();
                            DateTime? FechaInicio =
                                CreditoServicio.FechaInicioDESEMBOLSO(credito.NumeroCredito, _usuario);
                            if (FechaInicio != null)
                                ucFechaPrimerPago.Text = Convert.ToDateTime(FechaInicio).ToShortDateString();
                        }
                        catch
                        {
                        }
                    }

                    if (tipoempresa == 2)
                    {
                        lbledad.Visible = false;
                        txtEdad.Visible = false;
                    }

                    if (tipoempresa == 1)
                    {
                        lbledad.Visible = true;
                        if (!string.IsNullOrEmpty(credito.Edad.ToString()))
                            txtEdad.Text = HttpUtility.HtmlDecode(credito.Edad.ToString());
                    }

                    if (!string.IsNullOrEmpty(credito.Identificacion))
                        txtId.Text = HttpUtility.HtmlDecode(credito.Identificacion.ToString());
                    if (!string.IsNullOrEmpty(credito.LineaCredito))
                        txtLinea.Text = HttpUtility.HtmlDecode(credito.LineaCredito.ToString());
                    if (credito.cod_linea_credito != null)
                        Session["COD_LINEA"] = credito.cod_linea_credito;
                    if (!string.IsNullOrEmpty(credito.Monto.ToString()))
                    {
                        txtMonto.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                        txtMonto2.Text = HttpUtility.HtmlDecode(credito.Monto.ToString());
                    }

                    if (!string.IsNullOrEmpty(credito.Nombres))
                        txtNombres.Text = HttpUtility.HtmlDecode(credito.Nombres.ToString());
                    if (!string.IsNullOrEmpty(credito.Cod_Periodicidad.ToString()))
                    {
                        ddlPlazo.SelectedValue = HttpUtility.HtmlDecode(credito.Cod_Periodicidad.ToString());
                    }

                    if (!string.IsNullOrEmpty(credito.Periodicidad))
                    {
                        txtPeriodicidad.Text = HttpUtility.HtmlDecode(credito.Periodicidad);
                    }

                    ctlBusquedaProveedor.VisibleCtl = false;
                    if (!string.IsNullOrEmpty(credito.cod_linea_credito))
                    {
                        string linea = HttpUtility.HtmlDecode(credito.cod_linea_credito.ToString());
                        ListItemCollection lista = ddlLineaCredito.Items;
                        ListItem listaitem = lista.FindByValue(linea);
                        if (listaitem == null)
                        {
                            listaitem = new ListItem();
                            listaitem.Value = linea;
                            listaitem.Text = credito.LineaCredito;
                            ddlLineaCredito.Items.Add(listaitem);
                        }

                        ddlLineaCredito.SelectedValue = linea;
                        if (credito.cod_linea_credito != null)
                        {

                            vLineasCredito =
                                LineasCreditoServicio.ConsultarLineasCredito(
                                    Convert.ToString(ddlLineaCredito.SelectedValue), _usuario);
                            if (vLineasCredito.orden_servicio == 1)
                            {
                                ctlBusquedaProveedor.VisibleCtl = true;
                                ctlBusquedaProveedor.CheckedOrd =
                                    ctlBusquedaProveedor.ObtenerDatosOrdenCred(credito.NumeroCredito.ToString())
                                        ? true
                                        : false;
                            }
                        }
                    }

                    txtCredito2.Text = HttpUtility.HtmlDecode(credito.NumeroCredito.ToString());
                    txtCredito3.Text = HttpUtility.HtmlDecode(credito.NumeroCredito.ToString());
                    txtCredito4.Text = HttpUtility.HtmlDecode(credito.NumeroCredito.ToString());
                    txtCredito5.Text = HttpUtility.HtmlDecode(credito.NumeroCredito.ToString());

                    if (!string.IsNullOrEmpty(credito.Plazo))
                    {
                        txtPlazo.Text = HttpUtility.HtmlDecode(credito.Plazo.ToString());
                        txtPlazo2.Text = HttpUtility.HtmlDecode(credito.Plazo.ToString());
                    }

                    //if (credito.tasa != 0)
                    //{ 
                    //    txtTasa.Text = credito.tasa.ToString();
                    //}
                    //if (!string.IsNullOrEmpty(credito.cod_tipotasa))
                    //    ddlTipoTasa.SelectedValue = HttpUtility.HtmlDecode(credito.cod_tipotasa);
                    Checkpoliza.Checked = false;
                    if (!string.IsNullOrEmpty(credito.reqpoliza.ToString()))
                        if (credito.reqpoliza == 1)
                            Checkpoliza.Checked = true;
                    if (!string.IsNullOrEmpty(credito.cod_linea_credito.ToString()))
                        pIdCodLinea = HttpUtility.HtmlDecode(credito.cod_linea_credito.ToString());
                    if (!string.IsNullOrEmpty(credito.Disponible.ToString()))
                    {
                        try
                        {
                            ucFechaPrimerPago.ToDateTime =
                                Convert.ToDateTime(HttpUtility.HtmlDecode(credito.fecha_primer_pago.ToString()));
                        }
                        catch
                        {
                        }
                    }


                    if (credito.forma_pago != null && credito.forma_pago != "")
                        ddlFormaPago.SelectedValue = credito.forma_pago;
                    if (txtFormaPago.Text == "1" || txtFormaPago.Text == "C" || txtFormaPago.Text == "c")
                        txtFormaPago.Text = "Caja";
                    if (txtFormaPago.Text == "2" || txtFormaPago.Text == "N" || txtFormaPago.Text == "n")
                        txtFormaPago.Text = "Nomina";
                    if (credito.forma_pago == "1")
                        ddlFormaPago.SelectedValue = "C";
                    if (credito.forma_pago == "2")
                        ddlFormaPago.SelectedValue = "N";

                    string IdLinea = credito.cod_linea_credito;
                    poblar.PoblarListaDesplegable2(IdLinea, ddlDestino, (Usuario)Session["usuario"]);
                    poblar.PoblarListaDesplegable2(IdLinea, ddldestino2, (Usuario)Session["usuario"]);

                    if (!string.IsNullOrEmpty(credito.cod_Destino))
                    {
                        ddlDestino.SelectedValue = credito.cod_Destino;
                        ddlDestino.Enabled = false;
                        ddldestino2.SelectedValue = credito.cod_Destino;
                    }

                    if (credito.Obs_Concepto != null)
                    {
                        if (!string.IsNullOrEmpty(credito.Obs_Concepto))
                        {
                            txtObsSoli.Text = HttpUtility.HtmlDecode(credito.Obs_Concepto.ToString());
                        }
                    }

                    // Inicializar cuotas extras
                    CuotasExtras.TablaCuoExt(pIdObjeto);
                    LlenarVariables();
                    // Cargar las diferentes tablas del sistema
                    TablaAprobadores(pIdObjeto);
                    DocumentosAnexos.TablaDocumentosAnexo(pIdObjeto, 2);
                    TablaCodeudores(pIdObjeto);
                    TablaCentrales();
                    TablaCreditosRecogidos();
                    TableReferencias(pIdObjeto);
                    TablaModalidaTasa(pIdCodLinea);
                    ConsultarServiciosMarcadosParaRecoger();
                    // LLenar los dropdownlist para aplazar el crédito o negar
                    DropAplazar();
                    DropNegar();
                    InicializarDistribucion();
                    CalculaTotalXColumna();

                }

                Tabs.Enabled = true;
                Tabs.Visible = true;
                Tabs.ActiveTabIndex = 0;

                chkTasa_CheckedChanged(chkTasa, null);
                rbCalculoTasa_SelectedIndexChanged(rbCalculoTasa, null);


                // Mostrar los datos de descuentos del crédito
                gvDeducciones.DataSource = credito.lstDescuentosCredito;
                gvDeducciones.DataBind();

                //Cargar Lista Tasas
                List<LineasCredito> LstAtributos = new List<LineasCredito>();
                Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio =
                    new Xpinn.FabricaCreditos.Services.LineasCreditoService();
                LstAtributos = LineaCreditoServicio.ConsultarLineasCrediatributo2(credito.cod_linea_credito,
                    (Usuario)Session["Usuario"], txtCredito.Text);

                if (LstAtributos.Count > 0)
                {
                    if ((LstAtributos != null) || (LstAtributos.Count != 0))
                    {
                        gvAtributos.DataSource = LstAtributos;
                        gvAtributos.DataBind();


                    }

                    ValidarPersonaVacaciones(credito.CodigoCliente);
                }
                ValidatasaUsuara(credito, LstAtributos);
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }
    // Método para mostrar las consultas a centrales de riesgo del deudor
    private void TablaCentrales()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.consultasdatacredito> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.consultasdatacredito>();

            lstConsulta = consultasdatacreditoServicio.Listarconsultasdatacredito(ObtenerValores(), _usuario);

            gvLista.PageSize = pageSize;
            gvLista.EmptyDataText = emptyQuery;
            gvLista.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLista.Visible = true;
                lblTotalRegsConsultasCentrales.Visible = true;
                lblTotalRegsConsultasCentrales.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                tabCentrales.Visible = true;
            }
            else
            {
                gvLista.Visible = false;
                tabCentrales.Visible = false;
                lblTotalRegsConsultasCentrales.Visible = true;
                lblTotalRegsConsultasCentrales.Text = "No se encontrarón registros de consultas a centrales de riesgo para este cliente";
            }

            Session.Add(consultasdatacreditoServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(consultasdatacreditoServicio.CodigoPrograma, "Actualizar", ex);
        }
    }
    // Método para mostrar los créditos del deudor para recoger
    private void TablaCreditosRecogidos()
    {
        try
        {
            List<CreditoRecoger> lstConsulta = new List<CreditoRecoger>();
            CreditoRecoger creditoRecoger = new CreditoRecoger();

            // Cargar los créditos a recoger
            creditoRecoger.numero_radicacion = Convert.ToInt64(txtCredito.Text);
            lstConsulta = creditoRecogerServicio.ListarCreditoRecoger(creditoRecoger, _usuario);

            gvListaSolicitudCreditosRecogidos.PageSize = pageSize;
            gvListaSolicitudCreditosRecogidos.EmptyDataText = emptyQuery;

            foreach (CreditoRecoger variable in lstConsulta)
            {
                variable.valor_total = variable.saldo_capital + variable.interes_mora + variable.interes_corriente + variable.otros;
                if (!dejarCuotaPendiente())
                    variable.valor_nominas = 0;
            }

            gvListaSolicitudCreditosRecogidos.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvListaSolicitudCreditosRecogidos.Visible = true;
                lblTotRec.Visible = true;
                txtVrTotRecoger.Visible = true;
                lblMontoDesembolso.Visible = true;
                txtVrDesembolsar.Visible = true;
                lblTotalRegsSolicitudCreditosRecogidos.Visible = true;
                lblTotalRegsSolicitudCreditosRecogidos.Text = "<br/> Registros encontrados: " + lstConsulta.Count.ToString();
                gvListaSolicitudCreditosRecogidos.DataBind();
                ValidarPermisosGrilla(gvLista);
            }
            else
            {
                gvListaSolicitudCreditosRecogidos.Visible = false;
                lblTotRec.Visible = false;
                txtVrTotRecoger.Visible = false;
                lblMontoDesembolso.Visible = false;
                txtVrDesembolsar.Visible = false;
                lblTotalRegsSolicitudCreditosRecogidos.Visible = true;
                lblTotalRegsSolicitudCreditosRecogidos.Text = "No se encontraron créditos recogidos para este crédito";
            }

            CalcularTotalRecoge();
            Session.Add(SolicitudCreditosRecogidosServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoRecogerServicio.CodigoPrograma, "Page_PreInit", ex);
        }

        // Según parametro del WEBCONFIG no marcar los créditos a recoger
        if (GlobalWeb.gMarcarRecogerDesembolso == "1")
        {
            foreach (GridViewRow row in gvListaSolicitudCreditosRecogidos.Rows)
            {
                ((CheckBoxGrid)row.Cells[8].FindControl("chkRecoger")).Checked = false;
            }
        }

    }
    // Método para mostrar los créditos del deudor para recoger
    private void TablaModalidaTasa(String pIdCodLinea)
    {
        try
        {
            List<ModalidadTasa> lstConsulta = new List<ModalidadTasa>();
            ModalidadTasa modalidadtasa = new ModalidadTasa();
            lstConsulta = ModalidadTasaServicio.ListarModalidadTasa(pIdCodLinea, _usuario);


            gvListaModalidadTasas.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvListaModalidadTasas.Visible = true;
                lblTotalRegsModalidadTasa.Text = "<br/> Registros encontrados: " + lstConsulta.Count.ToString();
                gvListaModalidadTasas.DataBind();
                tabAnexos.Visible = true;
            }
            else
            {
                gvListaModalidadTasas.Visible = false;
                lblTotalRegsModalidadTasa.Visible = true;
                lblTotalRegsModalidadTasa.Text = "No se encontraron registros para este crédito";
                tabAnexos.Visible = false;
            }

            Session.Add(ModalidadTasaServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ModalidadTasaServicio.CodigoPrograma, "Page_PreInit", ex);
        }


    }
    // Método para mostrar la información del proceso de referenciación
    private void TableReferencias(String pIdObjeto)
    {
        List<Referencia> lstConsulta = new List<Referencia>();
        Xpinn.FabricaCreditos.Entities.Referencia vReferencia = new Xpinn.FabricaCreditos.Entities.Referencia();
        vReferencia.numero_radicacion = Convert.ToInt64(pIdObjeto.ToString());
        lstConsulta = ReferenciaServicio.ObservacionReferenciacion(vReferencia, _usuario);

        gvObservacionesRef.DataSource = lstConsulta;

        if (lstConsulta.Count > 0)
        {
            gvObservacionesRef.Visible = true;
            tabReferenciador.Visible = true;
            lblTotalRegsObservacionesRef.Visible = true;
            lblTotalRegsObservacionesRef.Text = "<br/> Registros encontrados: " + lstConsulta.Count.ToString();
            gvObservacionesRef.DataBind();
        }
        else
        {
            gvObservacionesRef.Visible = false;
            tabReferenciador.Visible = false;
            lblTotalRegsObservacionesRef.Visible = true;
            lblTotalRegsObservacionesRef.Text = "No se encontraron datos de la referenciación del crédito";
        }
    }
    // Método para mostrar los conceptos de los aprobadores
    private void TablaAprobadores(String pIdObjeto)
    {
        DatosAprobador datosApp = new DatosAprobador();
        datosApp.Radicacion = Int64.Parse(pIdObjeto);
        List<DatosAprobador> lstConsulta = new List<DatosAprobador>();
        lstConsulta = datosServicio.ListarDatosAprobador(datosApp, _usuario);
        //Llena Campo del asesor.
        if (!string.IsNullOrEmpty(lstConsulta.Select(x => x.Nombres).FirstOrDefault()))
            txtAsesor.Text = HttpUtility.HtmlDecode(lstConsulta.Select(x => x.Nombres).FirstOrDefault());
        Analisis_Credito analisisCredito = new Analisis_Credito();
        analisisCredito.numero_radicacion = Convert.ToInt64(pIdObjeto);
        analisisCredito = _creditoService.ListarAnalisisCredito(analisisCredito, _usuario);

        foreach (var datosAprobador in lstConsulta)
        {
            if (!string.IsNullOrEmpty(datosAprobador.Nombres))
            {
                if (datosAprobador.EstadoDescripcion == "Pre Aprobado")
                {
                    txtPropuesta.Text += HttpUtility.HtmlDecode(datosAprobador.Nombres + " (" + datosAprobador.EstadoDescripcion + ") : " + analisisCredito.justificacion + "\n");
                    txtPropuesta.Height = 100;
                }
                else
                {
                    txtPropuesta.Text += HttpUtility.HtmlDecode(datosAprobador.Nombres + " (" + datosAprobador.EstadoDescripcion + ") : " + datosAprobador.Observaciones + "\n");
                    txtPropuesta.Height = 100;
                }
            }
        }
        gvAprobacion.DataSource = lstConsulta;
        if (lstConsulta.Count > 0)
        {
            gvAprobacion.Visible = true;
            TabPanel1.Visible = true;
            lblInfo.Visible = false;
            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
            gvAprobacion.DataBind();
            try { ValidarPermisosGrilla(gvAprobacion); }
            catch { }
        }
        else
        {
            gvAprobacion.Visible = false;
            TabPanel1.Visible = false;
            lblInfo.Visible = true;
            lblTotalRegs.Visible = true;
            lblTotalRegs.Text = "No se encontraron registros de aprobadores del crédito";
        }

    }
    // Método para llenar el DDL de los motivos de aplazamiento del crédito
    private void DropAplazar()
    {
        MotivoService motivoServicio = new MotivoService();
        Motivo motivo = new Motivo();
        ddlAplazar.DataSource = motivoServicio.ListarMotivosFiltro(motivo, _usuario, 1);
        ddlAplazar.DataTextField = "Descripcion";
        ddlAplazar.DataValueField = "Codigo";
        ddlAplazar.DataBind();
        ddlAplazar.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    // Método para mostrar los motivos de negación del crédito
    private void DropNegar()
    {
        MotivoService motivoServicio = new MotivoService();
        Motivo motivo = new Motivo();
        ddlNegar.DataSource = motivoServicio.ListarMotivosFiltro(motivo, _usuario, 2);
        ddlNegar.DataTextField = "Descripcion";
        ddlNegar.DataValueField = "Codigo";
        ddlNegar.DataBind();
        ddlNegar.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
    }

    private List<CreditoRecoger> DevolverListaCreditosARecoger()
    {
        List<CreditoRecoger> lstCreditoRecoger = new List<CreditoRecoger>();

        foreach (GridViewRow row in gvListaSolicitudCreditosRecogidos.Rows)
        {
            CreditoRecoger vCreditoRecoger = new CreditoRecoger();
            CheckBoxGrid chkRecoger = (CheckBoxGrid)row.FindControl("chkRecoger");
            if (chkRecoger != null)
            {
                if (chkRecoger.Checked)
                {

                    vCreditoRecoger.numero_radicacion = Convert.ToInt64(txtCredito.Text.Trim());
                    vCreditoRecoger.numero_credito = Convert.ToInt64(row.Cells[1].Text.Trim());
                    Int64 valor_recoge = 0;
                    Int64 valor;
                    valor = Convert.ToInt64(row.Cells[4].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    valor = Convert.ToInt64(row.Cells[5].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    valor = Convert.ToInt64(row.Cells[6].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    valor = Convert.ToInt64(row.Cells[7].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    valor_recoge = valor_recoge + valor;
                    vCreditoRecoger.valor_recoge = valor_recoge;
                    vCreditoRecoger.fecha_pago = DateTime.Now;
                    vCreditoRecoger.saldo_capital = Convert.ToInt64(row.Cells[4].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    vCreditoRecoger.fecha_pago = DateTime.Now;
                    vCreditoRecoger.interes_corriente = Convert.ToInt64(row.Cells[5].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    vCreditoRecoger.interes_mora = Convert.ToInt64(row.Cells[6].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    vCreditoRecoger.otros = Convert.ToInt64(row.Cells[7].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));
                    if (dejarCuotaPendiente())
                        vCreditoRecoger.valor_nominas = Convert.ToInt64(row.Cells[12].Text.Trim().Replace(GlobalWeb.gSeparadorMiles, ""));

                    lstCreditoRecoger.Add(vCreditoRecoger);
                }
                else
                {
                    vCreditoRecoger.numero_credito = Convert.ToInt64(row.Cells[1].Text.Trim());
                    vCreditoRecoger.solo_borrar = true;

                    lstCreditoRecoger.Add(vCreditoRecoger);
                }

            }
        }

        return lstCreditoRecoger;
    }
    private void DropLineaCredito()
    {
        string ListaSolicitada = "";
        try
        {
            if (Session["ES_EMPLEADO"] == "true")
            {
                ListaSolicitada = "TipoCreditoEmple";
            }
            else
            {
                if (Session["TipoCredito"].ToString() == "3")
                    ListaSolicitada = "TipoCreditoMicro";
                else if (Session["TipoCredito"].ToString() == "1")
                    ListaSolicitada = "TipoCreditoConsumo";
                else
                    ListaSolicitada = "TipoCreditoMicro";
            }
            Session.Remove("ES_EMPLEADO");
            ddlLineaCredito.DataSource = TraerResultadosLista(ListaSolicitada);
            ddlLineaCredito.DataTextField = "ListaDescripcion";
            ddlLineaCredito.DataValueField = "ListaIdStr";
            ddlLineaCredito.DataBind();
        }
        catch { }
    }
    private List<Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, _usuario);
        return lstDatosSolicitud;
    }
    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = ControlProcesosServicio.ListasDesplegables(ListaSolicitada, _usuario);
    }
    private void CargarListas()
    {
        try
        {
            ListaSolicitada = "EstadoProceso";
            TraerResultadosLista();
            ddlProceso.DataSource = lstDatosSolicitud;
            ddlProceso.DataTextField = "ListaDescripcion";
            ddlProceso.DataValueField = "ListaId";
            ddlProceso.DataBind();


            Periodicidad perio = new Periodicidad();
            ddlPlazo.DataSource = periodicidadServicio.ListarPeriodicidad(perio, _usuario);
            ddlPlazo.DataTextField = "Descripcion";
            ddlPlazo.DataValueField = "Codigo";
            ddlPlazo.DataBind();
            ddlPlazo.Text = txtPeriodicidad.Text;


            List<TipoTasaHist> lstTipoTasaHist = new List<TipoTasaHist>();
            TipoTasaHistService TipoTasaHistServicios = new TipoTasaHistService();
            TipoTasaHist vTipoTasaHist = new TipoTasaHist();
            lstTipoTasaHist = TipoTasaHistServicios.ListarTipoTasaHist(vTipoTasaHist, _usuario);
            ddlHistorico.DataSource = lstTipoTasaHist;
            ddlHistorico.DataTextField = "descripcion";
            ddlHistorico.DataValueField = "tipo_historico";
            ddlHistorico.DataBind();

            // Llena el DDL de los tipos de tasas
            //List<TipoTasa> lstTipoTasa = new List<TipoTasa>();
            //TipoTasaService TipoTasaServicios = new TipoTasaService();
            //TipoTasa vTipoTasa = new TipoTasa();
            //lstTipoTasa = TipoTasaServicios.ListarTipoTasa(vTipoTasa, _usuario);
            //ddlTipoTasa.DataSource = lstTipoTasa;
            //ddlTipoTasa.DataTextField = "nombre";
            //ddlTipoTasa.DataValueField = "cod_tipo_tasa";
            //ddlTipoTasa.DataBind();

            //CARGANDO DOCUMENTOS PERTENECIENTES A AFILIACION
            ctlFormatos.Inicializar("2");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ReferenciaServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }
    protected List<CreditoEmpresaRecaudo> ObtenerListaEmpresaRecaudadora()
    {
        List<CreditoEmpresaRecaudo> lstLista = new List<CreditoEmpresaRecaudo>();
        int cantRow = 0;
        decimal vrtotal = 0, vrReemplazo = 0;
        if (gvEmpresaRecaudora.Rows.Count > 0)
            cantRow = gvEmpresaRecaudora.Rows.Count - 1;
        foreach (GridViewRow rFila in gvEmpresaRecaudora.Rows)
        {
            CreditoEmpresaRecaudo vData = new CreditoEmpresaRecaudo();

            Label lblCodigo = (Label)rFila.FindControl("lblCodigo"); //CODIGO DE LA EMPRESA
            if (lblCodigo != null && lblCodigo.Text != "")
                vData.cod_empresa = Convert.ToInt32(lblCodigo.Text);

            Label lblNombre = (Label)rFila.FindControl("lblNombre");
            if (lblNombre != null && lblNombre.Text != "")
                vData.nom_empresa = lblNombre.Text;

            TextBoxGrid txtPorcentaje = (TextBoxGrid)rFila.FindControl("txtPorcentaje");
            if (txtPorcentaje.Text != "")
                vData.porcentaje = Convert.ToDecimal(txtPorcentaje.Text);

            vData.valor = 0;
            TextBox txtValor = (TextBox)rFila.FindControl("txtValor");
            if (txtValor.Text != "")
                vData.valor = Convert.ToDecimal(txtValor.Text.Replace(".", ""));

            vrtotal += vData.valor;
            if (cantRow == rFila.RowIndex)
            {
                //---validando que cuadre el valor dividido con el monto de cuota.
                if (txtCuota.Text != "" && txtCuota != null)
                {
                    int cuota = Convert.ToInt32(txtCuota.Text.Replace(".", ""));
                    if (vrtotal > cuota)
                    {
                        vrReemplazo = vrtotal - cuota;
                        vData.valor = vData.valor - vrReemplazo;
                    }
                    else if (vrtotal < cuota)
                    {
                        vrReemplazo = cuota - vrtotal;
                        vData.valor = vData.valor + vrReemplazo;
                    }
                }
            }



            if (vData.porcentaje != 0)
                lstLista.Add(vData);
        }

        return lstLista;
    }
    protected List<CreditoSolicitado> ObtenerDatosCambiTasa()
    {
        List<CreditoSolicitado> lstLista = new List<CreditoSolicitado>();
        int cantRow = 0;
        if (gvAtributos.Rows.Count > 0)
            cantRow = gvAtributos.Rows.Count - 1;
        foreach (GridViewRow rFila in gvAtributos.Rows)
        {
            CreditoSolicitado vData = new CreditoSolicitado();

            Label lblCodAtributo = (Label)rFila.FindControl("lblcodatributo"); //CODIGO DE LA EMPRESA
            if (lblCodAtributo != null && lblCodAtributo.Text != "")
                vData.CodAtr = Convert.ToInt64(lblCodAtributo.Text);

            ASP.general_controles_ctlnumerocondecimales_ascx txttasa = (ASP.general_controles_ctlnumerocondecimales_ascx)rFila.FindControl("txttasa");
            if (txttasa != null && txttasa.Text != "")
            {
                vData.tasa = Convert.ToDecimal(ConvertirStringADecimal(txttasa.Text, ','));
            }

            DropDownList ddltipotasa = (DropDownList)rFila.FindControl("ddltipotasa");
            if (ddltipotasa.SelectedItem != null && ddltipotasa.Text != "")
                vData.TipoTasa = Convert.ToInt64(ddltipotasa.SelectedValue);

            string calculo_atr = rbCalculoTasa.SelectedValue;
            vData.calculo_atr = calculo_atr;

            vData.Operacion = 1;
            vData.TipoHistorico = "";
            vData.Desviacion = "";

            lstLista.Add(vData);
        }

        return lstLista;
    }
    protected List<ListasFijas> ListarTiposdeDecuento()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoTipoDeDescuento();
    }
    protected List<ListasFijas> ListaCreditoTipoDeLiquidacion()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoTipoDeLiquidacion();
    }
    protected List<ListasFijas> ListaCreditoFormadeDescuento()
    {
        GlobalWeb glob = new GlobalWeb();
        return glob.ListaCreditoFormadeDescuento();
    }
    protected List<ListasFijas> ListaImpuestos()
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        List<Xpinn.Comun.Entities.ListasFijas> lstImpuestos = new List<Xpinn.Comun.Entities.ListasFijas>();
        lstImpuestos.Add(new Xpinn.Comun.Entities.ListasFijas { codigo = "", descripcion = "" });
        lstImpuestos.Add(new Xpinn.Comun.Entities.ListasFijas { codigo = "0", descripcion = "" });
        List<LineasCredito> lstResult = new List<LineasCredito>();
        lstResult = LineasCreditoServicio.ddlimpuestos(_usuario);
        if (lstResult.Count > 0)
        {
            foreach (LineasCredito nLinea in lstResult)
            {
                lstImpuestos.Add(new Xpinn.Comun.Entities.ListasFijas { codigo = nLinea.cod_atr.ToString(), descripcion = nLinea.nombre });
            }
        }
        return lstImpuestos;
    }
    void LlenarVariables()
    {
        CuotasExtras.Monto = txtMonto.Text;
        CuotasExtras.Periodicidad = DatosClienteServicio.ListasDesplegables("PeriodicidadCuotaExt", (Usuario)Session["Usuario"])
                .FirstOrDefault(x => x.ListaDescripcion == txtPeriodicidad.Text).ListaId.ToString();
        CuotasExtras.PlazoTxt = txtPlazo.Text;
    }
    #endregion

    #region Mensajes y Conversiones
    public void MensajeFinal(string pmensaje)
    {
        mvAprobacion.ActiveViewIndex = 1;
        lblMensajeGrabar.Text = pmensaje;
    }
    protected decimal? ConvertirStringADecimal(string pDato, char pSeparadorDecimal)
    {
        decimal? Data = null;
        string[] datos = pDato.Split(pSeparadorDecimal);
        if (datos.Length >= 1)
            if (datos.Length == 1)
                Data = Convert.ToInt64(datos[0]);
            else
                Data = Convert.ToDecimal(Convert.ToInt64(datos[0]) + Convert.ToInt64(datos[1]) / (Math.Pow(10, datos[1].Length)));

        return Data;
    }


    #endregion

    #region Centrales De Riesgo
    //  Métodos para control de la grilla de centrales de riesgo
    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            TablaCentrales();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(consultasdatacreditoServicio.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[consultasdatacreditoServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    #endregion

    #region EVENTOS Y METODOS DE LA GRIDVIEW CODEUDORES
    // Métodos para control de la grilla de codeudores
    protected void ObtenerSiguienteOrden()
    {
        var maxValue = 0;
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            List<Persona1> lstCodeudor = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];
            maxValue = lstCodeudor.Max(x => x.orden);
        }
        ((TextBox)gvListaCodeudores.FooterRow.FindControl("txtOdenFooter")).Text = (maxValue + 1).ToString();
    }

    private Xpinn.FabricaCreditos.Entities.Persona1 ObtenerValoresCodeudores(string pIdObjeto)
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        if (pIdObjeto != "")
            vPersona1.numeroRadicacion = Convert.ToInt64(pIdObjeto.ToString());

        vPersona1.seleccionar = "CD"; //Bandera para ejecuta el select del CODEUDOR

        return vPersona1;
    }

    private void TablaCodeudores(string pIdObjeto)
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            lstConsulta = Persona1Servicio.ListarPersona1(ObtenerValoresCodeudores(pIdObjeto), _usuario);

            gvListaCodeudores.PageSize = 5;
            gvListaCodeudores.EmptyDataText = "No se encontraron registros";
            gvListaCodeudores.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvListaCodeudores.Visible = true;
                lblTotalRegsCodeudores.Visible = false;
                lblTotalRegsCodeudores.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvListaCodeudores.DataBind();
                ValidarPermisosGrilla(gvLista);
                Session[Usuario.codusuario + "Codeudores"] = lstConsulta;
                ObtenerSiguienteOrden();
            }
            else
            {
                idObjeto = "";
                gvListaCodeudores.Visible = false;
                lblTotalRegsCodeudores.Text = "No hay codeudores para este crédito";
                lblTotalRegsCodeudores.Visible = true;
                InicialCodeudores();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.CodigoProgramaCodeudor, "Actualizar", ex);
        }

    }

    protected void InicialCodeudores()
    {
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        Xpinn.FabricaCreditos.Entities.Persona1 eCodeudor = new Xpinn.FabricaCreditos.Entities.Persona1();
        lstConsulta.Add(eCodeudor);
        Session[Usuario.codusuario + "Codeudores"] = lstConsulta;
        gvListaCodeudores.DataSource = lstConsulta;
        gvListaCodeudores.DataBind();
        gvListaCodeudores.Visible = true;
        ObtenerSiguienteOrden();
    }

    protected void gvListaCodeudores_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // GENERAR EDICION
        lblErrorCod.Text = string.Empty;
        gvListaCodeudores.EditIndex = e.NewEditIndex;
        string id = gvListaCodeudores.DataKeys[e.NewEditIndex].Value.ToString();
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            gvListaCodeudores.DataSource = Session[Usuario.codusuario + "Codeudores"];
            gvListaCodeudores.DataBind();
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();
        OcultarGridFooter(gvListaCodeudores, false);
    }

    protected void gvListaCodeudores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        // GENERAR REVERSION
        lblErrorCod.Text = string.Empty;
        gvListaCodeudores.EditIndex = -1;
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            gvListaCodeudores.DataSource = Session[Usuario.codusuario + "Codeudores"];
            gvListaCodeudores.DataBind();
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();
        OcultarGridFooter(gvListaCodeudores, true);
    }

    protected void gvListaCodeudores_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        lblErrorCod.Text = string.Empty;
        gvListaCodeudores.EditIndex = -1;
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            TextBox txtOrdenRow = (TextBox)gvListaCodeudores.Rows[e.RowIndex].FindControl("txtOrdenRow");
            if (string.IsNullOrEmpty(txtOrdenRow.Text))
            {
                lblErrorCod.Text = "<li>Ingrese el orden al que pertenece el codeudor.</li>";
                return;
            }
            List<Persona1> lstCodeudores = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];
            lstCodeudores[e.RowIndex].orden = Convert.ToInt32(txtOrdenRow.Text);
            gvListaCodeudores.DataSource = lstCodeudores;
            gvListaCodeudores.DataBind();
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudores;
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();
        OcultarGridFooter(gvListaCodeudores, true);
    }

    protected void gvListaCodeudores_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblErrorCod.Text = string.Empty;
        List<Persona1> lstCodeudores = new List<Persona1>();
        lstCodeudores = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];
        if (lstCodeudores.Count >= 1)
        {
            Persona1 eCodeudor = new Persona1();
            int index = Convert.ToInt32(e.RowIndex);
            eCodeudor = lstCodeudores[index];
            if (eCodeudor.cod_persona != 0)
            {
                if (eCodeudor.idcodeudor != null)
                {
                    lstCodeudores.Remove(eCodeudor);
                    CodeudorServicio.EliminarcodeudoresCred(eCodeudor.idcodeudor, Convert.ToInt64(txtCredito.Text),
                        _usuario);
                }
            }
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudores;
        }
        if (lstCodeudores.Count == 0)
        {
            InicialCodeudores();
        }
        else
        {
            gvListaCodeudores.DataSource = lstCodeudores;
            gvListaCodeudores.DataBind();
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudores;
            ObtenerSiguienteOrden();
            for (int i = Convert.ToInt32(e.RowIndex); i < lstCodeudores.Count; i++)
            {
                try
                {
                    var orden = lstCodeudores[i].orden;
                    lstCodeudores[i].orden = Convert.ToInt32(orden) - 1;
                    gvListaCodeudores.DataBind();
                }
                catch (Exception f)
                {
                    //
                }
            }
            ObtenerSiguienteOrden();
        }

    }

    protected void gvListaCodeudores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("AddNew"))
        {
            lblErrorCod.Text = string.Empty;
            TextBox txtidentificacion = (TextBox)gvListaCodeudores.FooterRow.FindControl("txtidentificacion");
            TextBox txtOdenFooter = (TextBox)gvListaCodeudores.FooterRow.FindControl("txtOdenFooter");
            if (txtidentificacion.Text.Trim() == "")
            {
                lblErrorCod.Text = "<li>Ingrese la Identificación del Codeudor por favor.</li>";
                return;
            }
            if (string.IsNullOrEmpty(txtOdenFooter.Text))
            {
                lblErrorCod.Text = "<li>Ingrese el orden del codeudor por favor.</li>";
                return;
            }
            string IdentifSolic = txtId.Text;
            if (IdentifSolic.Trim() == txtidentificacion.Text.Trim())
            {
                lblErrorCod.Text = "<li>No puede ingresar como codeudor a la persona solicitante.</li>";
                return;
            }
            Xpinn.Comun.Data.GeneralData DAGeneral = new Xpinn.Comun.Data.GeneralData();
            Xpinn.Comun.Entities.General pEntidad = new Xpinn.Comun.Entities.General();
            pEntidad = DAGeneral.ConsultarGeneral(480, _usuario);
            try
            {
                if (pEntidad.valor != null)
                {
                    if (Convert.ToInt32(pEntidad.valor) > 0)
                    {
                        int paramCantidad = 0, cantReg = 0;
                        paramCantidad = Convert.ToInt32(pEntidad.valor);
                        Xpinn.FabricaCreditos.Entities.codeudores pCodeu = new Xpinn.FabricaCreditos.Entities.codeudores();
                        pCodeu = CodeudorServicio.ConsultarCantidadCodeudores(txtidentificacion.Text, _usuario);
                        if (pCodeu.cantidad != null)
                        {
                            cantReg = Convert.ToInt32(pCodeu.cantidad);
                            if (cantReg >= paramCantidad)
                            {
                                lblErrorCod.Text = "<li>No puede adicionar esta persona debido a que ya mantiene el límite de veces como codeudor.</li>";
                                return;
                            }
                        }
                    }
                }
            }
            catch { }

            List<Persona1> lstCodeudor = new List<Persona1>();
            lstCodeudor = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];

            if (lstCodeudor.Count == 1)
            {
                Persona1 gItem = new Persona1();
                gItem = lstCodeudor[0];
                if (gItem.cod_persona == 0)
                    lstCodeudor.Remove(gItem);
            }

            Xpinn.FabricaCreditos.Services.codeudoresService codeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
            Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
            vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, _usuario);
            Persona1 gItemNew = new Persona1();
            gItemNew.cod_persona = vcodeudor.codpersona;
            gItemNew.identificacion = vcodeudor.identificacion;
            gItemNew.primer_nombre = vcodeudor.primer_nombre;
            gItemNew.segundo_nombre = vcodeudor.segundo_nombre;
            gItemNew.primer_apellido = vcodeudor.primer_apellido;
            gItemNew.segundo_apellido = vcodeudor.segundo_apellido;
            gItemNew.direccion = vcodeudor.direccion;
            gItemNew.telefono = vcodeudor.telefono;
            gItemNew.idcodeudor = vcodeudor.idcodeud;
            gItemNew.orden = Convert.ToInt32(txtOdenFooter.Text);

            // validar que no existe el mismo codeudor en la gridview
            bool isValid = gvListaCodeudores.Rows.OfType<GridViewRow>().Where(x => ((Label)x.FindControl("lblCodPersona")).Text == gItemNew.cod_persona.ToString()).Any();
            if (!isValid)
                lstCodeudor.Add(gItemNew);

            gvListaCodeudores.DataSource = lstCodeudor;
            gvListaCodeudores.DataBind();
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudor;
            ObtenerSiguienteOrden();
        }
    }

    protected void txtidentificacion_TextChanged(object sender, EventArgs e)
    {
        lblErrorCod.Text = string.Empty;
        Control ctrl = gvListaCodeudores.FooterRow.FindControl("txtidentificacion");
        if (ctrl != null)
        {
            TextBox txtidentificacion = (TextBox)ctrl;
            if (txtidentificacion.Text != "")
            {
                Xpinn.FabricaCreditos.Services.codeudoresService codeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
                Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
                vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["usuario"]);
                if (vcodeudor.codpersona != 0)
                {
                    ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).Text = vcodeudor.codpersona.ToString();
                    gvListaCodeudores.FooterRow.Cells[4].Text = vcodeudor.primer_nombre;
                    gvListaCodeudores.FooterRow.Cells[5].Text = vcodeudor.segundo_nombre;
                    gvListaCodeudores.FooterRow.Cells[6].Text = vcodeudor.primer_apellido;
                    gvListaCodeudores.FooterRow.Cells[7].Text = vcodeudor.segundo_apellido;
                    gvListaCodeudores.FooterRow.Cells[8].Text = vcodeudor.direccion;
                    gvListaCodeudores.FooterRow.Cells[9].Text = vcodeudor.telefono;
                }
                else
                {
                    ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).ForeColor = System.Drawing.Color.Red;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:validar();", true);
                }
            }
            else
            {
                ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).Text = "";
                gvListaCodeudores.FooterRow.Cells[4].Text = "";
                gvListaCodeudores.FooterRow.Cells[5].Text = "";
                gvListaCodeudores.FooterRow.Cells[6].Text = "";
                gvListaCodeudores.FooterRow.Cells[7].Text = "";
                gvListaCodeudores.FooterRow.Cells[8].Text = "";
                gvListaCodeudores.FooterRow.Cells[9].Text = "";
            }
        }
    }


    #endregion

    #region Eventos y metodos de Créditos recogidos
    //  Métodos para control de la grilla de créditos recogidos
    protected void gvListaSolicitudCreditosRecogidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaSolicitudCreditosRecogidos.PageIndex = e.NewPageIndex;
            TablaCreditosRecogidos();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "gvListaSolicitudCreditosRecogidos_PageIndexChanging", ex);
        }
    }

    protected void gvListaSolicitudCreditosRecogidos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string a = Convert.ToString(e.ToString());
    }

    protected void gvListaSolicitudCreditosRecogidos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CreditoRecoger cRec = new CreditoRecoger();
                cRec = (CreditoRecoger)e.Row.DataItem;
                CheckBoxGrid chkRecoger = new CheckBoxGrid();
                chkRecoger = (CheckBoxGrid)e.Row.Cells[8].FindControl("chkRecoger");
                if (cRec.recoger == true)
                    chkRecoger.Checked = true;
            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    protected void gvListaSolicitudCreditosRecogidos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            SolicitudCreditosRecogidosServicio.EliminarSolicitudCreditosRecogidos(id, _usuario);
            TablaCreditosRecogidos();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicitudCreditosRecogidosServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    protected void gvListaSolicitudCreditosRecogidos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvLista.Rows[e.NewEditIndex].Cells[0].Text;
        Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Editar);
    }

    protected void gvListaSolicitudCreditosRecogidos_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.DataKeys[gvLista.SelectedRow.RowIndex].Value.ToString();
        Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }


    #endregion

    #region Consultas
    private void consultarControlAprobados()
    {
        try
        {
            Usuario pUsuario = _usuario;
            Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
            String radicado = Convert.ToString(this.txtCredito.Text);
            if (ddlProceso.SelectedValue != null)
            {
                if (ddlProceso.SelectedValue != "")
                {
                    GuardarControl();
                }
            }
        }
        catch (Exception ex)
        {
            VerError("No se pudieron grabar datos del control de créditos. Error:" + ex.Message);
        }

    }
    private void consultarControlNegados()
    {
        Usuario pUsuario = _usuario;

        ControlCreditos vControlCreditos = new ControlCreditos();
        String radicado = Convert.ToString(this.txtCredito.Text);

        vControlCreditos = ControlCreditosServicio.ConsultarControl_Procesos(Convert.ToInt64(ddlProceso.SelectedValue), radicado, _usuario);
        Int64 Controlexiste = 0;
        Controlexiste = Convert.ToInt64(vControlCreditos.codtipoproceso);
        if (Controlexiste <= 0)
        {
            GuardarControlNegado();
            ;
        }
    }
    private void consultarControlAplazados()
    {
        Usuario pUsuario = _usuario;


        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();

        String radicado = Convert.ToString(this.txtCredito.Text);
        vControlCreditos = ControlCreditosServicio.ConsultarControl_Procesos(Convert.ToInt64(ddlProceso.SelectedValue), radicado, _usuario);
        Int64 Controlexiste = 0;
        Controlexiste = Convert.ToInt64(vControlCreditos.codtipoproceso);
        if (Controlexiste <= 0)
        {
            GuardarControl();
        }


    }
    void ConsultarServiciosMarcadosParaRecoger()
    {
        long numeroCredito = Convert.ToInt64(ViewState["NumeroRadicacion"]);

        SolicitudCredServRecogidosService serviciosRecogidosServices = new SolicitudCredServRecogidosService();
        List<SolicitudCredServRecogidos> listaServicios = serviciosRecogidosServices.ListarSolicitudCredServRecogidosActualizado(numeroCredito, Usuario);

        gvServiciosRecogidos.ShowHeaderWhenEmpty = true;
        gvServiciosRecogidos.EmptyDataText = "No se encontraron servicios para recoger!.";
        gvServiciosRecogidos.DataSource = listaServicios;
        gvServiciosRecogidos.DataBind();
        CalcularTotalRecoge();
    }


    #endregion

    #region FUNCIONALIDAD DE LA GRIDVIEW DISTRIBUCION


    protected void txtIdentificacionD_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
            Xpinn.FabricaCreditos.Entities.Persona1 DatosPersona = new Xpinn.FabricaCreditos.Entities.Persona1();
            TextBoxGrid txtIdentificacion = (TextBoxGrid)sender;
            int rowIndex = Convert.ToInt32(txtIdentificacion.CommandArgument);

            Label lblcod_persona = (Label)gvDistribucion.Rows[rowIndex].FindControl("lblcod_persona");
            TextBoxGrid txtNombre = (TextBoxGrid)gvDistribucion.Rows[rowIndex].FindControl("txtNombre");
            if (txtIdentificacion.Text != "")
            {
                DatosPersona = UsuarioServicio.ConsultarPersona1(txtIdentificacion.Text, _usuario);

                if (DatosPersona.cod_persona != 0)
                {
                    if (lblcod_persona != null)
                        lblcod_persona.Text = DatosPersona.cod_persona.ToString();
                    if (DatosPersona.nombre != null)
                        txtNombre.Text = DatosPersona.nombre;
                }
                else
                    lblcod_persona.Text = "";
            }
            else
                lblcod_persona.Text = "";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(creditoServicio.CodigoPrograma, "txtIdentificacion_TextChanged", ex);
        }
    }

    protected List<Credito_Giro> ObtenerListaDistribucion()
    {
        try
        {
            List<Credito_Giro> lstDetalle = new List<Credito_Giro>();
            List<Credito_Giro> lista = new List<Credito_Giro>();

            foreach (GridViewRow rfila in gvDistribucion.Rows)
            {
                Credito_Giro eBene = new Credito_Giro();

                Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
                if (lblCodigo != null)
                    eBene.idgiro = Convert.ToInt32(lblCodigo.Text);
                else
                    eBene.idgiro = -1;

                Label lblCod_persona = (Label)rfila.FindControl("lblCod_persona");
                if (lblCod_persona.Text != "")
                    eBene.cod_persona = Convert.ToInt64(lblCod_persona.Text);

                TextBoxGrid txtIdentificacionD = (TextBoxGrid)rfila.FindControl("txtIdentificacionD");
                if (txtIdentificacionD.Text != "")
                    eBene.identificacion = txtIdentificacionD.Text;

                TextBoxGrid txtNombre = (TextBoxGrid)rfila.FindControl("txtNombre");
                if (txtNombre.Text != "")
                    eBene.nombre = txtNombre.Text;

                DropDownListGrid ddlTipo = (DropDownListGrid)rfila.FindControl("ddlTipo");
                eBene.tipo = Convert.ToInt32(ddlTipo.SelectedValue);

                decimalesGridRow txtValor = (decimalesGridRow)rfila.FindControl("txtValor");
                if (txtValor.Text.Trim() != "")
                    eBene.valor = Convert.ToDecimal(txtValor.Text);

                lista.Add(eBene);
                Session["Distribucion"] = lista;

                if (eBene.identificacion != null && eBene.nombre != null && eBene.valor != null)
                {
                    lstDetalle.Add(eBene);
                }
            }
            return lstDetalle;
        }
        catch
        {
            return null;
        }
    }

    protected void InicializarDistribucion()
    {

        List<Credito_Giro> lstDistribucion = new List<Credito_Giro>();
        for (int i = gvDistribucion.Rows.Count; i < 2; i++)
        {
            Credito_Giro pDetalle = new Credito_Giro();
            pDetalle.idgiro = -1;
            pDetalle.cod_persona = null;
            pDetalle.identificacion = "";
            pDetalle.nombre = "";
            pDetalle.valor = null;
            pDetalle.tipo = 0;
            lstDistribucion.Add(pDetalle);
        }
        gvDistribucion.DataSource = lstDistribucion;
        gvDistribucion.DataBind();

        Session["Distribucion"] = lstDistribucion;
    }

    protected void btnAdicionarFila_Click(object sender, EventArgs e)
    {
        ObtenerListaDistribucion();

        List<Credito_Giro> LstPrograma = new List<Credito_Giro>();
        if (Session["Distribucion"] != null)
        {
            LstPrograma = (List<Credito_Giro>)Session["Distribucion"];

            for (int i = 1; i <= 1; i++)
            {
                Credito_Giro pDetalle = new Credito_Giro();
                pDetalle.idgiro = -1;
                pDetalle.cod_persona = null;
                pDetalle.identificacion = "";
                pDetalle.nombre = "";
                pDetalle.valor = null;
                pDetalle.tipo = 0;
                LstPrograma.Add(pDetalle);
            }
            gvDistribucion.DataSource = LstPrograma;
            gvDistribucion.DataBind();

            Session["Distribucion"] = LstPrograma;
            CalculaTotalXColumna();
        }
    }


    protected void gvDistribucion_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvDistribucion.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaDistribucion();

        List<Credito_Giro> LstDetalle = new List<Credito_Giro>();
        LstDetalle = (List<Credito_Giro>)Session["Distribucion"];
        if (conseID > 0)
        {
            try
            {
                foreach (Credito_Giro acti in LstDetalle)
                {
                    if (acti.idgiro == conseID)
                    {
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstDetalle.RemoveAt((gvDistribucion.PageIndex * gvDistribucion.PageSize) + e.RowIndex);
        }
        Session["Distribucion"] = LstDetalle;

        gvDistribucion.DataSource = LstDetalle;
        gvDistribucion.DataBind();
        CalculaTotalXColumna();
    }

    protected void gvDistribucion_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlTipo = (DropDownListGrid)e.Row.FindControl("ddlTipo");
            if (ddlTipo != null)
            {
                ddlTipo.Items.Insert(0, new ListItem("Asociado", "0"));
                ddlTipo.Items.Insert(1, new ListItem("Tercero", "1"));
                ddlTipo.SelectedIndex = 0;
                ddlTipo.DataBind();

                Label lblTipo = (Label)e.Row.FindControl("lblTipo");
                if (lblTipo.Text != "")
                    ddlTipo.SelectedValue = lblTipo.Text;
            }

        }
    }

    protected void chkDistribuir_CheckedChanged(object sender, EventArgs e)
    {
        panelDistribucion.Visible = chkDistribuir.Checked ? true : false;
    }

    void CalculaTotalXColumna()
    {
        lblErrorDist.Text = "";
        decimal Fvalor = 0, MontoAprobado = 0;
        Label lblTotalVr = (Label)gvDistribucion.FooterRow.FindControl("lblTotalVr");

        foreach (GridViewRow rfila in gvDistribucion.Rows)
        {
            decimalesGridRow txtValor = (decimalesGridRow)rfila.FindControl("txtValor");
            if (txtValor.Text != "")
                Fvalor += Convert.ToDecimal(txtValor.Text.Replace(gSeparadorMiles, ""));
        }
        if (lblTotalVr != null)
            lblTotalVr.Text = Fvalor.ToString("c0");
        MontoAprobado = txtMonto2.Text != "" ? Convert.ToDecimal(txtMonto2.Text.Replace(gSeparadorMiles, "")) : 0;
        if (Fvalor > MontoAprobado)
        {
            lblErrorDist.Text = "Error al ingresar los datos, El monto total de las distribuciones no puede superar el monto Aprobado";
            return;
        }
    }


    #endregion

}