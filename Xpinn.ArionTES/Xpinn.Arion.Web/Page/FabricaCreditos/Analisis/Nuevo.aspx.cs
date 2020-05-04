using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Xpinn.Aportes.Entities;
using Xpinn.Asesores.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;
using Xpinn.Comun.Entities;
using Xpinn.Servicios.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Interfaces.Entities;
using Xpinn.Tesoreria.Services;
using Xpinn.Servicios.Services;

public partial class Nuevo : GlobalWeb
{
    Usuario _usuario;
    long _numeroRadicacion;

    String _identificacion;
    bool _metodo;

    // Diccionario para mantener el id del deudor y codeudor
    Dictionary<TipoDeudor, long?> diccionarioIDPersonas;

    // Listar para mantener los datos para mostrarlos en el reporte
    List<AnalisisPromedio> listaAnalisisPromedio;
    List<AnalisisPromedio> listaHistorial;
    List<Credito> listaCreditosActivos;

    CreditoService _creditoService = new CreditoService();
    General general = new General();
    Persona1Service _creditoPersona = new Persona1Service();
    EstadosFinancierosService _estadoFinanciero = new EstadosFinancierosService();
    RealizacionGirosServices RealizacionService = new RealizacionGirosServices();
    List<CreditoRecoger> lstcreditoRecoger = new List<CreditoRecoger>();
    SolicitudCredServRecogidosService solicitudCredServRecogidosService = new SolicitudCredServRecogidosService();
    List<SolicitudCredServRecogidos> solicitudCredServRecogidos = new List<SolicitudCredServRecogidos>();
    private CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
    private Persona1Service DatosClienteServicio = new Persona1Service();
    private LineasCreditoService lineasCreditoService = new LineasCreditoService();
    private int cod_afiancol = 60;
    private bool metodo = false;


    #region Evento Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_creditoService.CodigoProgramaAnalisisCredito, "E");
            Site toolBar = (Site)this.Master;

            if (Session[_creditoService.CodigoProgramaAnalisisCredito + ".imprimir"] == null)
            {
                metodo = false;
            }
            else
            {
                metodo = (bool)Session[_creditoService.CodigoProgramaAnalisisCredito + ".imprimir"];
            }

            if (metodo)
            {
                toolBar.MostrarImprimir(true);
                toolBar.MostrarGuardar(false);
            }
            else
            {
                toolBar.MostrarImprimir(false);
                toolBar.MostrarGuardar(true);
            }
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnMostrarInforme_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_creditoService.CodigoProgramaAnalisisCredito, "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            // Necesario para evitar perder datos tras un postback siempre se asigna sin importar que
            _usuario = (Usuario)Session["usuario"];
            _numeroRadicacion = (long)Session[_creditoService.CodigoProgramaAnalisisCredito + ".id"];
            _identificacion = (String)Session[_creditoService.CodigoProgramaAnalisisCredito + ".identificacion"];

            if (Session[_creditoService.CodigoProgramaAnalisisCredito + ".imprimir"] == null)
            {
                _metodo = false;
            }
            else
            {
                _metodo = (bool)Session[_creditoService.CodigoProgramaAnalisisCredito + ".imprimir"];
            }

            if (!IsPostBack)
            {
                Session["CodLineaCredito"] = null;
                Session["Frecuencia"] = null;
                Session["Cuota"] = null;
                Session["Numero_Radicacion"] = null;
                Session["Viabilidad"] = null;
                Session["Archivos"] = null;
                CargarDDLProceso();
                diccionarioIDPersonas = new Dictionary<TipoDeudor, long?>();
                mvCredito.ActiveViewIndex = 0;
                DeshabilitarControlesActivarReadOnly();
                LlenarFormulario();
                ocultar();

            }
            if (IsPostBack)
            {
                // Necesario para evitar perder datos tras un postback
                diccionarioIDPersonas = (Dictionary<TipoDeudor, long?>)Session[_creditoService.CodigoProgramaAnalisisCredito + ".datos"];
                listaAnalisisPromedio = (List<AnalisisPromedio>)Session["listaAnalisisPromedio"];
                listaCreditosActivos = (List<Credito>)Session["listaCreditosActivos"];
                listaHistorial = (List<AnalisisPromedio>)Session["listaHistorial"];
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_creditoService.CodigoProgramaAnalisisCredito, "Page_PreInit", ex);
        }
    }


    // Es NECESARIO agregar estos atributos para hacer los campos READONLY, no se puede hacer directo en el .aspx
    // porque se PIERDEN los datos generados por el javascript cuando ocurre un postback
    private void DeshabilitarControlesActivarReadOnly()
    {
        TxtIngresosBrutos1.Attributes.Add("readonly", "readonly");
        TxtIngresosBrutos2.Attributes.Add("readonly", "readonly");
        TxtIngresosBrutos3.Attributes.Add("readonly", "readonly");
        TxtIngresosBrutos4.Attributes.Add("readonly", "readonly");
        TxtB1.Attributes.Add("readonly", "readonly");
        TxtB2.Attributes.Add("readonly", "readonly");
        TxtB3.Attributes.Add("readonly", "readonly");
        TxtB4.Attributes.Add("readonly", "readonly");
        TxtCuotaIngresosNeto1.Attributes.Add("readonly", "readonly");
        TxtIngresosMensual1.Attributes.Add("readonly", "readonly");
        TxtIngresosMensual2.Attributes.Add("readonly", "readonly");
        TxtIngresosMensual3.Attributes.Add("readonly", "readonly");
        TxtIngresosMensual4.Attributes.Add("readonly", "readonly");
        txtCupoDisponible.Attributes.Add("readonly", "readonly");

        // CAPACIDAD DESCUENTO NOMINA Y PAGO
        txtCapDesc1.Attributes.Add("readonly", "readonly");
        txtCapDesc2.Attributes.Add("readonly", "readonly");
        txtCapDesc3.Attributes.Add("readonly", "readonly");
        txtCapDesc4.Attributes.Add("readonly", "readonly");

        txtCapPago1.Attributes.Add("readonly", "readonly");
        txtCapPago2.Attributes.Add("readonly", "readonly");
        txtCapPago3.Attributes.Add("readonly", "readonly");
        txtCapPago4.Attributes.Add("readonly", "readonly");
    }

    private void CargarDDLProceso()
    {
        List<ControlTiempos> lstDatosSolicitud = new List<ControlTiempos>();  //Lista de los menus desplegables
        ControlTiemposService ControlProcesosServicio = new ControlTiemposService();
        lstDatosSolicitud = ControlProcesosServicio.ListasDesplegables("EstadoProceso", _usuario);
        ddlProceso.DataSource = lstDatosSolicitud;
        ddlProceso.DataTextField = "ListaDescripcion";
        ddlProceso.DataValueField = "ListaId";
        ddlProceso.DataBind();
    }
    #endregion

    #region Eventos Botonera

    protected void CkcAfiancol_OnCheckedChanged(object sender, EventArgs e)
    {

        var Cod = lineasCreditoService.ddlatributo((Usuario)Session["Usuario"])
                .Where(x => x.cod_atr == cod_afiancol).Select(x => x.cod_atr).FirstOrDefault();
        if (Cod == 0)
        {
            MensajeViabilidad.Text = @"No se encuentra el atributo de AFIANCOL Creado.";
            CkcAfiancol.Checked = false;
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    private void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        string errores = "Faltan campos por llenar: ";

        if (string.IsNullOrWhiteSpace(TxtJustificacion.Text))
        {
            errores += "Justificación, ";
        }
        else if (string.IsNullOrWhiteSpace(TxtDocumentosPrevistos.Text))
        {
            errores += "Documentos Provistos, ";
        }
        else if (!(RbtViable.Checked || RbtNoViable.Checked))
        {
            errores += "Botones Viabilidad";
        }
        else if (ddlCalifica1.SelectedValue == "")
        {
            errores += "Calificaciòn centrales riesgo";
        }
        else if (!ucFechaConsulta1.TieneDatos)
        {
            errores += "Fecha de Consulta";
        }

        if (errores != "Faltan campos por llenar: ")
        {
            VerError(errores);
            return;
        }
        if (DocumentosAnexo.ContarTablaDatos())
        {
            DocumentosAnexo.BtnAgregarOnClick(null, null);
            DocumentosAnexo.GuardarArchivos(Convert.ToString(_numeroRadicacion));
        }
        ctlMensaje.MostrarMensaje("Desea proceder con el guardado?");

    }


    private void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        var error = "";
        int idAnalisis = GuardarAnalisisCredito();
        Site toolBar = (Site)Master;
        if (idAnalisis > 0)
        {
            List<Analisis_Capacidad_Pago> lstAnalisisCapacidad = MoldearListaCapacidadPago(idAnalisis);
            bool exitoso = GuardarCapacidadPago(lstAnalisisCapacidad);
            exitoso = CuotasExtras.GuardarCuotas(_numeroRadicacion.ToString());
            exitoso = Codeudores.GuardarCodeudores(ref error);
            if (exitoso)
            {

                #region Interfaz WorkManagement


                // Parametro general para habilitar proceso de WM
                General parametroHabilitarOperacionesWM = ConsultarParametroGeneral(35);
                if (parametroHabilitarOperacionesWM != null && parametroHabilitarOperacionesWM.valor.Trim() == "1")
                {
                    try
                    {
                        WorkManagementServices workManagementService = new WorkManagementServices();

                        // Se necesita el workFlowId para saber cual es el workflow que vamos a correrle el step
                        // Este registro workFlowId es llenado en la solicitud del credito
                        WorkFlowCreditos workFlowCredito = workManagementService.ConsultarWorkFlowCreditoPorNumeroRadicacion(Convert.ToInt32(TxtNumeroSolicitud.Text.Trim()), Usuario);

                        if (workFlowCredito != null && !string.IsNullOrWhiteSpace(workFlowCredito.barCodeRadicacion) && workFlowCredito.workflowid > 0)
                        {
                            InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);

                            string observacionesWM = " Analisis de Credito: " + TxtJustificacion.Text;

                            // RunTask, corre al siguiente proceso, debes identificar en el proceso que estas y añadir las observaciones
                            bool aprobacionUnaExitosa = interfaz.RunTaskWorkFlowCredito(workFlowCredito.barCodeRadicacion, workFlowCredito.workflowid, StepsWorkManagementWorkFlowCredito.AprobacionCredito, observacionesWM);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }


                #endregion

                // Activo View de Aviso de Guardado, muestro el Boton de Imprimir
                // Oculto el boton de guardar y engancho el evento para imprimir
                mvCredito.ActiveViewIndex = 1;
                toolBar.MostrarCancelar(false);
            }
        }
        else
        {
            mvCredito.ActiveViewIndex = 1;
            toolBar.MostrarCancelar(true);
        }
        toolBar.MostrarGuardar(false);
        toolBar.MostrarImprimir(true);

    }


    private void btnMostrarInforme_Click(object sender, ImageClickEventArgs e)
    {
        // Activo View del Reporte
        Site toolBar = (Site)Master;
        toolBar.MostrarImprimir(true);
        mvCredito.ActiveViewIndex = 2;
        LlenarFormulario();
        LlenarReporte();
        Session[_creditoService.CodigoProgramaAnalisisCredito + ".imprimir"] = null;
    }


    protected void btnRegresarInforme_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }


    protected void btnImprimirInforme_Click(object sender, EventArgs e)
    {
        if (ReportViewerPlan.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            var bytes = ReportViewerPlan.LocalReport.Render("PDF");
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline;attachment; filename=Reporte.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush(); // send it to the client to download
            Response.Clear();
        }
    }

    protected void RbtViable_OnCheckedChanged(object sender, EventArgs e)
    {
        //90185 Valida analisis de credito
        general = ConsultarParametroGeneral(90185, (Usuario)Session["usuario"]);
        if (!string.IsNullOrEmpty(general.valor) && Convert.ToInt32(general.valor) == 1)
            Validacion();
    }
    #endregion

    #region Lleno el formulario completo CADENA DE METODOS


    // Lleno el formulario completo CADENA DE METODOS :D
    private void LlenarFormulario()
    {

        Credito credito = ConsultarCredito();
        Session["CodLineaCredito"] = credito.cod_linea_credito;
        Session["Frecuencia"] = credito.periodicidad;
        Session["Cuota"] = credito.valor_cuota;
        Session["Numero_Radicacion"] = credito.numero_radicacion;
        idObjeto = credito.numero_radicacion.ToString();
        Persona1 persona = ConsultarPersona(credito);
        long idDeudor = LlenarAnalisisSolicitudCredito(credito, persona);

        //Llenar cuotas extras 
        CuotasExtras.TablaCuoExt(credito.numero_radicacion.ToString());
        //Llenar Codeudores 
        Codeudores.TablaCodeudores();
        //Llenar Documentos Anexos 
        DocumentosAnexo.TablaDocumentosAnexo(Convert.ToString(credito.numero_radicacion), 2);
        //llenar variables de controles
        DocumentosAnexo.IniciarTable();

        LlenarVariables(idDeudor);
        List<AnalisisPromedio> analisisPromedio = LlenarAnalisisPromedio(credito.cod_linea_credito, idDeudor);
        LlenarFooterAnalisisPromedio(analisisPromedio);

        List<Credito> listaCreditos = LlenarEstadoCuenta(idDeudor);
        LlenarFooterEstadoCuenta(listaCreditos);

        // LLENANDO INFORMACION DE INGRESOS Y EGRESOS
        LlenarDatosIngresosEgresos(0, persona.cod_persona);
        Calcular_Cupo(credito.cod_linea_credito, persona.cod_persona, credito.monto, credito.valor_cuota);

        LlenarDatosCalificacion(persona.cod_persona);

        ConsultarCodeudoresYAjustarTabla();

        if (_metodo)
            LlenarDatosAnalisis(_numeroRadicacion);

        // Calcular el nivel de endeudamiento
        Persona1Service _personaServicio = new Persona1Service();
        decimal _nivelEnd = _personaServicio.ConsultarNivelEndeudamiento(_identificacion, (Usuario)Session["usuario"]);
        lblNivelEndeudamiento.Text = string.Format("{0:N2}", _nivelEnd) + "%";

        lblMensajeEndeudamiento.Text = "";
        double ingresos = TxtIngresos1.Text.Trim() != "" ? Convert.ToDouble(TxtIngresos1.Text) : 0;
        double totalCartera = lblTotObliAportes.Text.Trim() != "" ? Convert.ToDouble(lblTotObliAportes.Text) : 0;
        if (totalCartera > ingresos * 4)
            lblMensajeEndeudamiento.Text += String.Format("Total de la cartera {0:N} supera cuatro veces el ingreso {1:N}.", totalCartera, ingresos);
        if (_nivelEnd > 60)
            lblMensajeEndeudamiento.Text += "Capacidad de endeudamiento superado.";

        // Necesario para evitar perder datos del diccionario tras un postback
        Session[_creditoService.CodigoProgramaAnalisisCredito + ".datos"] = diccionarioIDPersonas;
    }

    public void ocultar()
    {
        General general = new General();
        //381: Generar Analisis de Credito, (0-completo  1-Resumido
        general = ConsultarParametroGeneral(381, (Usuario)Session["usuario"]);

        lblgeneral.Text = general.valor.ToString();

        if (general.valor == "1")
        {
            Arriendo.Style["display"] = "none";
            Honorarios.Style["display"] = "none";
            ObliCode.Style["display"] = "none";
            otrDesc.Style["display"] = "none";
            CredVig.Style["display"] = "none";
            DescSer.Style["display"] = "none";
            DeudTerc.Style["display"] = "none";
            ProtSal.Style["display"] = "none";

            //Panel de Descuento por nomina y de pago
            Panel1.Visible = false;
            tblCapacidadDstoPago.Visible = false;
            txtCapDesc1.Text = "";
            txtCapDesc2.Text = "";
            txtCapDesc3.Text = "";
            txtCapDesc4.Text = "";
            txtCapPago1.Text = "";
            txtCapPago2.Text = "";
            txtCapPago3.Text = "";
            txtCapPago4.Text = "";

            TabCupo.Visible = false;
            lblCupoEndeuda.Text = "";
            lblCupoDispo.Text = "";
            lblNivelEndeudamiento.Text = "";
            lblDescuCaja.Text = "";
            lblTotObliAportes.Text = "";
            lblMensajeEndeudamiento.Text = "";
        }
    }

    #region Cadena de Metodos para llenar formulario

    private Persona1 ConsultarPersona(Credito credito)
    {
        try
        {
            Persona1 persona = new Persona1();
            persona.identificacion = credito.identificacion;
            persona = _creditoPersona.ConsultarPersona2Param(persona, Usuario);
            return persona;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_creditoService.CodigoProgramaModifi, "ConsultarPersona", ex);
            return null;
        }
    }

    // Consulto el credito
    private Credito ConsultarCredito()
    {
        try
        {
            Credito credito = _creditoService.ConsultarCreditoAsesor(_numeroRadicacion, _usuario);
            Codeudores.identificacion = credito.identificacion;
            // Guardo el ID del deudor
            diccionarioIDPersonas[TipoDeudor.Deudor] = credito.cod_deudor;
            return credito;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_creditoService.CodigoProgramaModifi, "LlenarAnalisisSolicitudCredito", ex);
            return null;
        }
    }

    // Lleno el analisis de la solicitud
    private long LlenarAnalisisSolicitudCredito(Credito credito, Persona1 persona)
    {
        // Primera Columna
        DateTime pFecha = credito.fecha_aprobacion == null ? Convert.ToDateTime(credito.fecha_solicitud) : Convert.ToDateTime(credito.fecha_aprobacion);
        TxtFechaRecibido.Text = pFecha.ToString("dd/MM/yyyy");
        TxtSolicitante.Text = persona.nombre;
        TxtDocumentoIdentidad.Text = persona.identificacion;
        TxtFormaPago.Text = credito.nomforma_pago;
        TxtEdad.Text = credito.edad.ToString();
        int resultMes;
        if (persona.fecha_afiliacion != DateTime.MinValue)
        {
            resultMes = 0;
            resultMes = CalcularMesesDeDiferencia(persona.fecha_afiliacion, DateTime.Now);
            txtAntAsociado.Text = resultMes + " Meses";
        }
        if (persona.fecha_ingresoempresa != DateTime.MinValue)
        {
            resultMes = 0;
            resultMes = CalcularMesesDeDiferencia(persona.fecha_ingresoempresa, DateTime.Now);
            txtAntLaboral.Text = resultMes + " Meses";
        }
        txtTipoContrato.Text = string.IsNullOrWhiteSpace(persona.tipocontrato) ? null : persona.tipocontrato;

        // Segunda Columna
        TxtValorSolicitado.Text = string.Format("{0:N}", credito.monto);
        TxtModalidadCredito.Text = credito.linea_credito;
        TxtPlazo.Text = credito.numero_cuotas.ToString();
        TxtFrecuenciaCuota.Text = credito.periodicidad;
        txtFechaSoli.Text = credito.fecha_solicitud != null ? Convert.ToDateTime(credito.fecha_solicitud).ToString(gFormatoFecha) : null;
        txtTasa.Text = credito.tasa.ToString();
        TxtNumeroSolicitud.Text = credito.numero_radicacion.ToString();

        // TextBox Valor Cuota en CAPACIDAD DE PAGO
        TxtCuotaCredito1.Text = credito.valor_cuota.ToString();
        txtValorGirar.Text = string.Format("{0:N}", credito.monto_giro);
        txtVrCuotaDescontar.Text = string.Format("{0:N}", (credito.monto - credito.monto_giro));
        txtEmpresaReca.Text = credito.empresa;
        txtObservacion.Height = 50;
        txtObservacion.Text = credito.observacion;
        TxtDestinacion.Text = credito.NombreDestinacion;
        CkcAfiancol.Checked = credito.ReqAfiancol == 0 ? false : true;
        txtZona.Text = persona.nom_zona;
        return credito.cod_deudor;
    }

    // Lleno la GV de analisis promedio y retorno lista para totalizar footer
    private List<AnalisisPromedio> LlenarAnalisisPromedio(string cod_linea, long idDeudor)
    {
        try
        {
            listaAnalisisPromedio = _creditoService.ConsultarAnalisisPromedio(cod_linea, idDeudor, _usuario);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_creditoService.CodigoProgramaModifi, "LlenarAnalisisPromedio", ex);
        }

        if (listaAnalisisPromedio.Count() == 0) // Si esta vacia añado una fila vacia para evitar errores
        {
            listaAnalisisPromedio.Add(new AnalisisPromedio());
        }

        txtCupoDisponible.Text = FormatearNumerosParaReporteODarDefault(listaAnalisisPromedio.First().cupo_disponible.ToString());
        Session["listaAnalisisPromedio"] = listaAnalisisPromedio;
        gvAnalisisPromedio.DataSource = listaAnalisisPromedio;
        gvAnalisisPromedio.DataBind();

        return listaAnalisisPromedio;
    }


    // Totalizo la GV analisis promedio y asigno al footer los valores
    private void LlenarFooterAnalisisPromedio(List<AnalisisPromedio> analisisPromedio)
    {
        var totalizacion = new // Anonymous Type para facilitar el calculo de la totalizacion con LINQ :D
        {
            totalSaldo = analisisPromedio.Sum(x => x.saldo),
            totalCupoDisponible = analisisPromedio.Sum(x => x.cupo_disponible)
        };

        // Footer
        TextBox txtTotalSaldoPromedio = gvAnalisisPromedio.FooterRow.FindControl("txtTotalSaldoPromedio") as TextBox;
        txtTotalSaldoPromedio.Text = "$ " + string.Format("{0:N}", totalizacion.totalSaldo);
        lblSaldoAportes.Text = totalizacion.totalSaldo.ToString();
    }

    private void LlenarDatosIngresosEgresos(int pTipo, Int64 pCodigo, List<Credito> lstCredito = null)
    {
        AnalisisInfo analisisInfo = new AnalisisInfo();
        List<Credito> lstcreditos = new List<Credito>();
        TipoDeudor tipoDeudor = pTipo.ToEnum<TipoDeudor>();

        //90186 Líneas no incluidas en créditos vigentes análisis (Separados por coma)
        List<string> LineasCredito = new List<string>();
        general = ConsultarParametroGeneral(90186, (Usuario)Session["usuario"]);
        if (!string.IsNullOrEmpty(general.valor))
            LineasCredito = general.valor.Split(',').ToList();

        //381: Generar Analisis de Credito, (0-completo  1-Resumido
        general = ConsultarParametroGeneral(381, (Usuario)Session["usuario"]);

        //Obtener periodicidad
        var periodicidad = DatosClienteServicio.ListasDesplegables("PeriodicidadCuotaExt", (Usuario)Session["Usuario"])
                .FirstOrDefault(x => x.ListaDescripcion == Session["Frecuencia"].ToString()).ListaId;

        decimal periodo = (decimal)periodicidad * 12 / 360;
        // OBTENER DATOS DE INGRESOS
        EstadosFinancieros estadoFinan = _estadoFinanciero.listarperosnainfofin(pCodigo, Usuario);

        // CONSULTANDO INFORMACION DE PRODUCTOS
        decimal pVrTotalIng = 0, pVrSalud = 0, pVrAportes = 0, pVrCredito = 0, pVrServicio = 0,
            pProtecSalarial = 0, pVrTotalEgre = 0, pVrNetoMensual = 0, pVrSaldoCredito = 0, pVrSaldoServicio = 0;

        // CONSULTANDO APORTES
        Xpinn.Aportes.Services.AporteServices _aporteServices = new Xpinn.Aportes.Services.AporteServices();
        Xpinn.Aportes.Entities.Aporte pAporte = new Xpinn.Aportes.Entities.Aporte();
        pAporte.estado = 1;
        pAporte.cod_persona = pCodigo;
        List<Xpinn.Aportes.Entities.Aporte> lstAportes = _aporteServices.ListarAporte(pAporte, Usuario);

        if (lstAportes != null)
        {
            pVrAportes = 0;
            decimal valor = (decimal)(30 / periodicidad);
            foreach (var x in lstAportes) pVrAportes += valor * x.cuota;
        }

        // CONSULTANDO CREDITOS
        if (lstCredito == null)
        {
            lstCredito = ListarCreditosActivos(pCodigo);
            lstcreditoRecoger = creditoRecogerServicio.ConsultarCreditoRecoger(Convert.ToInt64(idObjeto), Usuario);
        }

        if (lstCredito != null)
        {
            foreach (var item in lstCredito)
            {
                if (LineasCredito.Exists(x => x == item.cod_linea_credito)) continue;
                lstcreditos.Add(item);
            }

            foreach (var item in lstcreditos)
            {
                if (lstcreditoRecoger.Exists(x => x.numero_radicacion == item.numero_radicacion)) continue;
                pVrCredito += item.valor_cuota;
            }
        }

        // CONSULTANDO SERVICIOS
        Xpinn.Servicios.Services.SolicitudServiciosServices _serviciosServices = new Xpinn.Servicios.Services.SolicitudServiciosServices();
        string pFiltro = " and s.estado = 'C' and s.COD_PERSONA = " + pCodigo;
        //Consulta Servicios Recogidos 
        List<Servicio> lstServicio = _serviciosServices.ListarServicios(new Servicio(), DateTime.MinValue, Usuario, pFiltro);
        solicitudCredServRecogidos = solicitudCredServRecogidosService.ListarSolicitudCredServRecogidos(Convert.ToInt64(idObjeto), Usuario);

        if (lstServicio != null)
        {
            foreach (var item in lstServicio)
            {
                if (solicitudCredServRecogidos.Exists(x => x.numeroservicio == item.numero_servicio)) continue;
                pVrServicio += Convert.ToInt64(item.valor_cuota);
            }
        }

        pVrSaldoCredito = pVrCredito;
        pVrSaldoServicio = pVrServicio;

        if (general.valor == "1")
        {
            estadoFinan.arrendamientos = 0;
            estadoFinan.honorarios = 0;
        }

        pVrTotalIng = estadoFinan.sueldo + estadoFinan.otrosingresos + estadoFinan.arrendamientos + estadoFinan.honorarios;

        if (general.valor == "1")
        {
            pVrCredito = 0;
            pVrSaldoServicio = 0;
            pProtecSalarial = 0;
        }
        else
        {
            pProtecSalarial = (estadoFinan.sueldo) > 0 ? Math.Round((estadoFinan.sueldo - pVrSalud) / 2) : 0;
        }
        pVrSalud = estadoFinan.sueldo > 0 ? Math.Round((estadoFinan.sueldo * Convert.ToDecimal(TxtPorcentaje.Text)) / 100, 0) : 0;

        pVrTotalEgre = pVrAportes + pVrCredito + pVrServicio + pProtecSalarial + pVrSalud;
        pVrNetoMensual = pVrTotalIng - pVrTotalEgre;

        decimal totalDescuento = 0;
        totalDescuento = ((estadoFinan.sueldo) - pVrTotalEgre);
        analisisInfo = _creditoService.ConsultarAnalisis_Info(_numeroRadicacion, pTipo, Usuario);
        if (analisisInfo.NumeroRadicacion == null || analisisInfo.NumeroRadicacion == 0)
        {
            if (estadoFinan != null)
            {
                List<TextBox> lstControls = new List<TextBox>();

                if (tipoDeudor == TipoDeudor.Deudor)
                {
                    // INGRESOS
                    TxtIngresos1.Text = estadoFinan.sueldo.ToString();
                    TxtOtrosIngresos1.Text = estadoFinan.otrosingresos.ToString();
                    txtArrendamiento1.Text = estadoFinan.arrendamientos.ToString();
                    txtHonorario1.Text = estadoFinan.honorarios.ToString();
                    TxtIngresosBrutos1.Text = pVrTotalIng.ToString();

                    // EGRESOS
                    TxtDeduccionesSocial1.Text = pVrSalud.ToString();
                    txtAporte1.Text = pVrAportes.ToString();
                    txtCreditoVig1.Text = pVrCredito.ToString();
                    txtServicio1.Text = pVrServicio.ToString();
                    txtProtSalarial1.Text = pProtecSalarial.ToString();
                    TxtB1.Text = ValidarNumero(pVrTotalEgre).ToString();

                    TxtIngresosMensual1.Text = ValidarNumero(pVrNetoMensual).ToString();
                    TxtIngresoTrimestral1.Text = ValidarNumero((pVrTotalIng - pVrTotalEgre) * periodo).ToString();

                    txtCapDesc1.Text = ValidarNumero(totalDescuento).ToString();
                    var capacidad = estadoFinan.sueldo - pVrTotalEgre;
                    txtCapPago1.Text = ValidarNumero(capacidad).ToString();
                    var ingresoNetoPorc = 0;
                    var cuota = Convert.ToInt32(Session["Cuota"]) == 0 ? 1 : Convert.ToInt32(Session["Cuota"]);

                    var ingresoneto = (cuota / (pVrTotalIng - pVrTotalEgre) * periodo) * 100;
                    TxtCuotaIngresosNeto1.Text = Math.Round(ingresoneto) < 0 ? "0" : Math.Round(ingresoneto).ToString();

                    lblDeudasActuales.Text = ValidarNumero((pVrSaldoCredito + pVrSaldoServicio)).ToString();
                    txtDeudasActuales.Text = ValidarNumero((pVrSaldoCredito + pVrSaldoServicio)).ToString("N0");

                }
                else if (tipoDeudor == TipoDeudor.Codeudor1)
                {
                    // INGRESOS
                    TxtIngresos2.Text = estadoFinan.sueldo.ToString();
                    TxtOtrosIngresos2.Text = estadoFinan.otrosingresos.ToString();
                    txtArrendamiento2.Text = estadoFinan.arrendamientos.ToString();
                    txtHonorario2.Text = estadoFinan.honorarios.ToString();
                    TxtIngresosBrutos2.Text = pVrTotalIng.ToString();

                    // EGRESOS
                    TxtDeduccionesSocial2.Text = pVrSalud.ToString();
                    txtAporte2.Text = pVrAportes.ToString();
                    txtCreditoVig2.Text = pVrCredito.ToString();
                    txtServicio2.Text = pVrServicio.ToString();
                    txtProtSalarial2.Text = pProtecSalarial.ToString();
                    TxtB2.Text = ValidarNumero(pVrTotalEgre).ToString();

                    TxtIngresosMensual2.Text = ValidarNumero(pVrNetoMensual).ToString();
                    TxtIngresoTrimestral2.Text = ValidarNumero(((pVrTotalIng - pVrTotalEgre) * (periodo))).ToString();

                    txtCapDesc2.Text = ValidarNumero(totalDescuento).ToString();
                    var capacidad = estadoFinan.sueldo - pVrTotalEgre;
                    txtCapPago2.Text = ValidarNumero(capacidad).ToString();
                }
                else if (tipoDeudor == TipoDeudor.Codeudor2)
                {
                    // INGRESOS
                    TxtIngresos3.Text = estadoFinan.sueldo.ToString();
                    TxtOtrosIngresos3.Text = estadoFinan.otrosingresos.ToString();
                    txtArrendamiento3.Text = estadoFinan.arrendamientos.ToString();
                    txtHonorario3.Text = estadoFinan.honorarios.ToString();
                    TxtIngresosBrutos3.Text = pVrTotalIng.ToString();

                    // EGRESOS
                    TxtDeduccionesSocial3.Text = pVrSalud.ToString();
                    txtAporte3.Text = pVrAportes.ToString();
                    txtCreditoVig3.Text = pVrCredito.ToString();
                    txtServicio3.Text = pVrServicio.ToString();
                    txtProtSalarial3.Text = pProtecSalarial.ToString();
                    TxtB3.Text = ValidarNumero(pVrTotalEgre).ToString();

                    TxtIngresosMensual3.Text = pVrNetoMensual.ToString();
                    TxtIngresoTrimestral3.Text = ValidarNumero(((pVrTotalIng - pVrTotalEgre) * (periodo))).ToString();

                    txtCapDesc3.Text = ValidarNumero(totalDescuento).ToString();
                    var capacidad = estadoFinan.sueldo - pVrTotalEgre;
                    txtCapPago3.Text = ValidarNumero(capacidad).ToString();
                }
                else if (tipoDeudor == TipoDeudor.Codeudor3)
                {
                    // INGRESOS
                    TxtIngresos4.Text = estadoFinan.sueldo.ToString();
                    TxtOtrosIngresos4.Text = estadoFinan.otrosingresos.ToString();
                    txtArrendamiento4.Text = estadoFinan.arrendamientos.ToString();
                    txtHonorario4.Text = estadoFinan.honorarios.ToString();
                    TxtIngresosBrutos4.Text = pVrTotalIng.ToString();

                    // EGRESOS
                    TxtDeduccionesSocial4.Text = pVrSalud.ToString();
                    txtAporte4.Text = pVrAportes.ToString();
                    txtCreditoVig4.Text = pVrCredito.ToString();
                    txtServicio4.Text = pVrServicio.ToString();
                    txtProtSalarial4.Text = pProtecSalarial.ToString();
                    TxtB4.Text = ValidarNumero(pVrTotalEgre).ToString();

                    TxtIngresosMensual4.Text = pVrNetoMensual.ToString();
                    TxtIngresoTrimestral4.Text = ValidarNumero(((pVrTotalIng - pVrTotalEgre) * (periodo))).ToString();

                    txtCapDesc4.Text = totalDescuento.ToString();
                    var capacidad = estadoFinan.sueldo - pVrTotalEgre;
                    txtCapPago4.Text = ValidarNumero(capacidad).ToString();
                }
            }
        }
        else
        {
            if (tipoDeudor == TipoDeudor.Deudor)
            {
                // INGRESOS
                TxtIngresos1.Text = string.Format(analisisInfo.Ingresos.ToString());
                TxtOtrosIngresos1.Text = analisisInfo.OtrosIngresos.ToString();
                txtArrendamiento1.Text = analisisInfo.Arrendamientos.ToString();
                txtHonorario1.Text = analisisInfo.Honorarios.ToString();
                var total = analisisInfo.Ingresos + analisisInfo.Arrendamientos + analisisInfo.Honorarios + analisisInfo.OtrosIngresos;
                TxtIngresosBrutos1.Text = total.ToString();

                // EGRESOS
                TxtDeduccionesSocial1.Text = analisisInfo.CobroJuridico.ToString();
                TxtCuotasFinanPrincipal1.Text = analisisInfo.CIfinPrincipal.ToString();
                TxtCuotasFinanDeudor1.Text = analisisInfo.CifinCodor.ToString();
                TxtGastosFamiliares1.Text = analisisInfo.Gastosfalimiares.ToString();
                txtAporte1.Text = analisisInfo.Aportes.ToString();
                txtOtrosDsctos1.Text = analisisInfo.OtrosDescuentos.ToString();
                txtCreditoVig1.Text = analisisInfo.CreditosVigentes.ToString();
                txtServicio1.Text = analisisInfo.Servicios.ToString();
                txtDeudasTer1.Text = analisisInfo.DasTercero.ToString();
                txtProtSalarial1.Text = analisisInfo.ProteccionSalarial.ToString();
                var totalB = analisisInfo.CobroJuridico + analisisInfo.CIfinPrincipal + analisisInfo.CifinCodor +
                             analisisInfo.Gastosfalimiares + analisisInfo.Aportes
                             + analisisInfo.OtrosDescuentos + analisisInfo.CreditosVigentes + analisisInfo.Servicios + analisisInfo.DasTercero +
                             pProtecSalarial;
                TxtB1.Text = ValidarNumero(totalB).ToString();

                TxtIngresosMensual1.Text = ValidarNumero((total - totalB)).ToString();

                txtCapDesc1.Text = ValidarNumero(totalDescuento).ToString();
                var capacidad = (estadoFinan.sueldo + estadoFinan.otrosingresos) - pVrTotalEgre;
                txtCapPago1.Text = ValidarNumero(capacidad).ToString();

                TxtIngresoTrimestral1.Text = ValidarNumero(((total - totalB) * (periodo))).ToString();
                var ingresoNetoPorc = 0;
                var cuota = Convert.ToInt32(Session["Cuota"]) == 0 ? 1 : Convert.ToInt32(Session["Cuota"]);
                var ingresoneto = (double)(cuota / ((total - totalB) * periodo) * 100);
                TxtCuotaIngresosNeto1.Text = Math.Round(ingresoneto) < 0 ? "0" : Math.Round(ingresoneto).ToString();

                TxtCuotaIngresosNeto1.Text = ingresoNetoPorc.ToString();

                lblDeudasActuales.Text = (pVrSaldoCredito + pVrSaldoServicio).ToString();
                txtDeudasActuales.Text = (pVrSaldoCredito + pVrSaldoServicio).ToString("N0");

                ddlCalifica1.SelectedValue = analisisInfo.calif_criesgo;
                ucFechaConsulta1.Text = analisisInfo.fecha_consulta.ToString();
            }
            else if (tipoDeudor == TipoDeudor.Codeudor1)
            {
                // INGRESOS
                TxtIngresos2.Text = string.Format(analisisInfo.Ingresos.ToString());
                TxtOtrosIngresos2.Text = analisisInfo.OtrosIngresos.ToString();
                txtArrendamiento2.Text = analisisInfo.Arrendamientos.ToString();
                txtHonorario2.Text = analisisInfo.Honorarios.ToString();
                var total = analisisInfo.Ingresos + analisisInfo.Arrendamientos + analisisInfo.Honorarios + analisisInfo.OtrosIngresos;
                TxtIngresosBrutos2.Text = total.ToString();

                // EGRESOS
                TxtDeduccionesSocial2.Text = analisisInfo.CobroJuridico.ToString();
                TxtCuotasFinanPrincipal2.Text = analisisInfo.CIfinPrincipal.ToString();
                TxtCuotasFinanDeudor2.Text = analisisInfo.CifinCodor.ToString();
                TxtGastosFamiliares2.Text = analisisInfo.Gastosfalimiares.ToString();
                txtAporte2.Text = analisisInfo.Aportes.ToString();
                txtOtrosDsctos2.Text = analisisInfo.OtrosDescuentos.ToString();
                txtCreditoVig2.Text = analisisInfo.CreditosVigentes.ToString();
                txtServicio2.Text = analisisInfo.Servicios.ToString();
                txtDeudasTer2.Text = analisisInfo.DasTercero.ToString();
                txtProtSalarial2.Text = analisisInfo.ProteccionSalarial.ToString();
                var totalB = analisisInfo.CobroJuridico + analisisInfo.CIfinPrincipal + analisisInfo.CifinCodor +
                             analisisInfo.Gastosfalimiares + analisisInfo.Aportes
                             + analisisInfo.OtrosDescuentos + analisisInfo.CreditosVigentes + analisisInfo.Servicios + analisisInfo.DasTercero +
                             pProtecSalarial;
                TxtB2.Text = totalB.ToString();

                TxtIngresosMensual2.Text = ValidarNumero((total - totalB)).ToString();
                TxtIngresoTrimestral2.Text = ValidarNumero(((total - totalB) * (periodo))).ToString();

                txtCapDesc2.Text = ValidarNumero(totalDescuento).ToString();
                var capacidad = estadoFinan.sueldo - pVrTotalEgre;
                txtCapPago2.Text = ValidarNumero(capacidad).ToString();

                ddlCalifica2.SelectedValue = analisisInfo.calif_criesgo;
                ucFechaConsulta2.Text = analisisInfo.fecha_consulta.ToString();
            }
            else if (tipoDeudor == TipoDeudor.Codeudor2)
            {
                // INGRESOS
                TxtIngresos3.Text = string.Format(analisisInfo.Ingresos.ToString());
                TxtOtrosIngresos3.Text = analisisInfo.OtrosIngresos.ToString();
                txtArrendamiento3.Text = analisisInfo.Arrendamientos.ToString();
                txtHonorario3.Text = analisisInfo.Honorarios.ToString();
                var total = analisisInfo.Ingresos + analisisInfo.Arrendamientos + analisisInfo.Honorarios + analisisInfo.OtrosIngresos;
                TxtIngresosBrutos3.Text = total.ToString();

                // EGRESOS
                TxtDeduccionesSocial3.Text = analisisInfo.CobroJuridico.ToString();
                TxtCuotasFinanPrincipal3.Text = analisisInfo.CIfinPrincipal.ToString();
                TxtCuotasFinanDeudor3.Text = analisisInfo.CifinCodor.ToString();
                TxtGastosFamiliares3.Text = analisisInfo.Gastosfalimiares.ToString();
                txtAporte3.Text = analisisInfo.Aportes.ToString();
                txtOtrosDsctos3.Text = analisisInfo.OtrosDescuentos.ToString();
                txtCreditoVig3.Text = analisisInfo.CreditosVigentes.ToString();
                txtServicio3.Text = analisisInfo.Servicios.ToString();
                txtDeudasTer3.Text = analisisInfo.DasTercero.ToString();
                txtProtSalarial3.Text = pProtecSalarial.ToString();
                var totalB = analisisInfo.CobroJuridico + analisisInfo.CIfinPrincipal + analisisInfo.CifinCodor +
                             analisisInfo.Gastosfalimiares + pVrAportes
                             + analisisInfo.OtrosDescuentos + pVrCredito + analisisInfo.Servicios + analisisInfo.DasTercero +
                             pProtecSalarial;
                TxtB3.Text = ValidarNumero(totalB).ToString();

                TxtIngresosMensual3.Text = ValidarNumero(pVrNetoMensual).ToString();
                TxtIngresoTrimestral3.Text = ValidarNumero(((total - totalB) * (periodo))).ToString();


                txtCapDesc3.Text = ValidarNumero(totalDescuento).ToString();
                var capacidad = estadoFinan.sueldo - pVrTotalEgre;
                txtCapPago3.Text = ValidarNumero(capacidad).ToString();

                ddlCalifica3.SelectedValue = analisisInfo.calif_criesgo;
                ucFechaConsulta3.Text = analisisInfo.fecha_consulta.ToString();

            }
            else if (tipoDeudor == TipoDeudor.Codeudor3)
            {
                // INGRESOS
                TxtIngresos4.Text = string.Format(analisisInfo.Ingresos.ToString());
                TxtOtrosIngresos4.Text = analisisInfo.OtrosIngresos.ToString();
                txtArrendamiento4.Text = analisisInfo.Arrendamientos.ToString();
                txtHonorario4.Text = analisisInfo.Honorarios.ToString();
                var total = analisisInfo.Ingresos + analisisInfo.Arrendamientos + analisisInfo.Honorarios + analisisInfo.OtrosIngresos;
                TxtIngresosBrutos4.Text = total.ToString();

                // EGRESOS
                TxtDeduccionesSocial4.Text = analisisInfo.CobroJuridico.ToString();
                TxtCuotasFinanPrincipal4.Text = analisisInfo.CIfinPrincipal.ToString();
                TxtCuotasFinanDeudor4.Text = analisisInfo.CifinCodor.ToString();
                TxtGastosFamiliares4.Text = analisisInfo.Gastosfalimiares.ToString();
                txtAporte4.Text = pVrAportes.ToString();
                txtOtrosDsctos4.Text = analisisInfo.OtrosDescuentos.ToString();
                txtCreditoVig4.Text = pVrCredito.ToString();
                txtServicio4.Text = pVrServicio.ToString();
                txtDeudasTer4.Text = analisisInfo.DasTercero.ToString();
                txtProtSalarial4.Text = pProtecSalarial.ToString();
                var totalB = analisisInfo.CobroJuridico + analisisInfo.CIfinPrincipal + analisisInfo.CifinCodor +
                             analisisInfo.Gastosfalimiares + pVrAportes
                             + analisisInfo.OtrosDescuentos + pVrCredito + pVrServicio + analisisInfo.DasTercero +
                             pProtecSalarial;
                TxtB4.Text = totalB.ToString();

                TxtIngresosMensual4.Text = ValidarNumero((total - totalB)).ToString();
                TxtIngresoTrimestral4.Text = ValidarNumero(((total - totalB) * (periodo))).ToString();

                txtCapDesc4.Text = ValidarNumero(totalDescuento).ToString();
                var capacidad = estadoFinan.sueldo - pVrTotalEgre;
                txtCapPago4.Text = ValidarNumero(capacidad).ToString();

                ddlCalifica4.SelectedValue = analisisInfo.calif_criesgo;
                ucFechaConsulta4.Text = analisisInfo.fecha_consulta.ToString();
            }
        }
    }

    private int ValidarNumero(object numero)
    {
        int resultado = 0;
        return Convert.ToInt32(numero) > 0 ? Convert.ToInt32(numero) : resultado;
    }
    private void LlenarDatosCalificacion(Int64 pCodigo)
    {
        // OBTENER DATOS DE INGRESOS
        listaHistorial = _creditoService.ConsultarCalificacionHistorial(pCodigo, _usuario);
        Session["listaHistorial"] = listaHistorial;
        gvCalificacion.DataSource = listaHistorial;
        gvCalificacion.DataBind();

    }


    private void LlenarDatosAnalisis(Int64 pCodigo)
    {
        Analisis_Credito analisisCredito = new Analisis_Credito();
        analisisCredito.numero_radicacion = pCodigo;
        analisisCredito = _creditoService.ListarAnalisisCredito(analisisCredito, _usuario);

        // OBTENER DATOS DE INGRESOS

        analisisCredito.numero_radicacion = _numeroRadicacion;
        TxtFechaRecibido.Text = analisisCredito.fecha_analisis.ToShortDateString();
        TxtJustificacion.Text = analisisCredito.justificacion;
        TxtDocumentosPrevistos.Text = analisisCredito.analisis_docs;
        TxtGarantiasOfrecidas.Text = analisisCredito.garantias_ofrecidas;
        TxtCodeudor1.Text = analisisCredito.analisis_doc_cod1;
        TxtCodeudor2.Text = analisisCredito.analisis_doc_cod2;
        textBoxCodeudor3.Text = analisisCredito.analisis_doc_cod3;

        if (!string.IsNullOrWhiteSpace(TxtCuotaIngresosNeto1.Text))
        {
            TxtCuotaIngresosNeto1.Text = analisisCredito.capacidad_pago.ToString();
        }

        Session["Viabilidad"] = analisisCredito.viabilidad;

        RbtViable.Checked = analisisCredito.viabilidad == "S" ? true : false;

        RbtNoViable.Checked = analisisCredito.viabilidad == "N" ? true : false;

        TxtDocumentosPrevistos.Text = analisisCredito.documentos_provistos;

    }


    private void Calcular_Cupo(string pCodLinea, Int64 pCodCliente, decimal pMontoSoli, decimal pValorCuota)
    {
        Xpinn.FabricaCreditos.Entities.LineasCredito DatosLinea = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCredito = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        if (!string.IsNullOrEmpty(pCodLinea))
        {
            try
            {
                DatosLinea = LineaCredito.Calcular_Cupo(pCodLinea, pCodCliente, DateTime.Today, (Usuario)Session["usuario"]);
                decimal pMontoMax = DatosLinea.Monto_Maximo != null ? Convert.ToDecimal(DatosLinea.Monto_Maximo) : 0;
                lblCupoEndeuda.Text = pMontoMax.ToString("N0");
                lblCupoDispo.Text = (pMontoMax - pMontoSoli).ToString("N0");

                decimal vrCapDescto = string.IsNullOrEmpty(txtCapDesc1.Text) ? 0 : Convert.ToDecimal(txtCapDesc1.Text);
                lblDescuCaja.Text = (pValorCuota - vrCapDescto) < 0 ? "0" : (pValorCuota - vrCapDescto).ToString("N0");

                decimal deudas = string.IsNullOrEmpty(lblDeudasActuales.Text) ? 0 : Convert.ToDecimal(lblDeudasActuales.Text);
                decimal aportes = string.IsNullOrEmpty(lblSaldoAportes.Text) ? 0 : Convert.ToDecimal(lblSaldoAportes.Text);
                lblTotObliAportes.Text = ((deudas + pMontoSoli) - aportes).ToString("N0");
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
        }
    }

    private List<Credito> ListarCreditosActivos(long idDeudor)
    {
        List<Credito> lstCred = new List<Credito>();
        try
        {
            lstCred = _creditoService.ListarCreditoActivos(idDeudor, _usuario);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_creditoService.CodigoProgramaModifi, "ListarCreditosActivos", ex);
        }
        return lstCred;
    }

    // Llenar GV de estado de cuenta (Creditos Activos) y retorno lista para totalizar footer
    private List<Credito> LlenarEstadoCuenta(long idDeudor)
    {
        listaCreditosActivos = ListarCreditosActivos(idDeudor);

        if (listaCreditosActivos.Count() == 0) // Si esta vacia añado una fila vacia para evitar errores
        {
            listaCreditosActivos.Add(new Credito());
        }

        Session["listaCreditosActivos"] = listaCreditosActivos;
        gvEstadoCuenta.DataSource = listaCreditosActivos;
        gvEstadoCuenta.DataBind();

        return listaCreditosActivos;
    }


    // Totalizo la GV de estado de cuenta y asigno al footer los valores
    void LlenarFooterEstadoCuenta(List<Credito> listaCreditos)
    {
        var totalizacion = new // Anonymous Type para facilitar el calculo de la totalizacion con LINQ :D
        {
            totalSaldo = listaCreditos.Sum(x => x.saldo_capital),
            totalValorInicial = listaCreditos.Sum(x => x.monto_aprobado),
            totalValorCuota = listaCreditos.Sum(x => x.valor_cuota)
        };

        // Footer
        TextBox txtTotalValorInicial = gvEstadoCuenta.FooterRow.FindControl("txtTotalValorInicial") as TextBox;
        txtTotalValorInicial.Text = "$ " + string.Format("{0:N}", totalizacion.totalValorInicial);

        TextBox txtTotalSaldo = gvEstadoCuenta.FooterRow.FindControl("txtTotalSaldo") as TextBox;
        txtTotalSaldo.Text = "$ " + string.Format("{0:N}", totalizacion.totalSaldo);

        TextBox txtTotalValorCuota = gvEstadoCuenta.FooterRow.FindControl("txtTotalValorCuota") as TextBox;
        txtTotalValorCuota.Text = "$ " + string.Format("{0:N}", totalizacion.totalValorCuota);
    }


    // Consulto cuantos Codeudores tiene y oculto columnas segun numero de codeudores, guardo los ID
    private void ConsultarCodeudoresYAjustarTabla()
    {
        List<Cliente> lstCodeudores = null;

        try
        {
            lstCodeudores = _creditoService.ListarCodeudores(_numeroRadicacion, _usuario);
            //Agregado para visualizar codeudor
            if (lstCodeudores.Count() == 1)
            {
                TxtCodeudor1.Text = lstCodeudores[0].NumeroDocumento + " " + lstCodeudores[0].NombreYApellido;
            }
            if (lstCodeudores.Count() == 2)
            {
                TxtCodeudor1.Text = lstCodeudores[0].NumeroDocumento + " " + lstCodeudores[0].NombreYApellido;
                TxtCodeudor2.Text = lstCodeudores[1].NumeroDocumento + " " + lstCodeudores[1].NombreYApellido;
            }
            if (lstCodeudores.Count() == 3)
            {
                TxtCodeudor1.Text = lstCodeudores[0].NumeroDocumento + " " + lstCodeudores[0].NombreYApellido;
                TxtCodeudor2.Text = lstCodeudores[1].NumeroDocumento + " " + lstCodeudores[1].NombreYApellido;
                textBoxCodeudor3.Text = lstCodeudores[2].NumeroDocumento + " " + lstCodeudores[2].NombreYApellido;
            }

            // METODO PARA CARGAR LA INFORMACION DE INGRESOS Y EGRESOS DE CODEUDORES
            if (lstCodeudores.Count > 0)
            {
                Int64 codPersona = 0;
                int NroRegistro = lstCodeudores.Count > 3 ? 3 : lstCodeudores.Count;
                for (int i = 0; i < NroRegistro; i++)
                {
                    codPersona = lstCodeudores.OrderBy(x => x.cod_persona).ToList()[i].IdCliente;
                    if (codPersona != 0)
                    {
                        LlenarDatosIngresosEgresos(i + 1, codPersona);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_creditoService.CodigoProgramaModifi, "ConsultarCodeudoresYAjustarTabla", ex);
        }

        // Guardo el id de los CODEUDORES si tengo

        for (int i = 0; i < lstCodeudores.Count(); i++)
        {
            if (i == 0)
            {
                diccionarioIDPersonas[TipoDeudor.Codeudor1] = lstCodeudores[i].IdCliente;
            }
            else if (i == 1)
            {
                diccionarioIDPersonas[TipoDeudor.Codeudor2] = lstCodeudores[i].IdCliente;
            }
            else
            {
                diccionarioIDPersonas[TipoDeudor.Codeudor3] = lstCodeudores[i].IdCliente;
            }
        }
        Session[_creditoService.CodigoProgramaAnalisisCredito + ".datos"] = diccionarioIDPersonas;

        // Oculto columnas si hace falta CODEUDORES
        switch (lstCodeudores.Count())
        {
            case 0:
                OcultarTodasColumnasCodeudores();
                Ptitulo5.Visible = false;
                TblCoudeudores.Visible = false;
                break;
            case 1:
                OcultarDosColumnasCodeudores();
                cellLblCodeudor3.Visible = false;
                cellLblCodeudor2.Visible = false;
                break;
            case 2:
                OcultarUnaColumnaCodeudores();
                cellLblCodeudor3.Visible = false;
                break;
        }
    }


    #region Metodos Ocultar Columnas Codeudores

    // Oculto ultima columnas
    private void OcultarUnaColumnaCodeudores()
    {
        int cont = 0;
        foreach (TableRow Row in TblCapacidadDepago.Rows)
        {
            foreach (Control ctrl in Row.Cells[4].Controls)
            {
                if (cont == 0) Row.Cells[4].BackColor = System.Drawing.Color.White;
                ctrl.Visible = false;
            }
            cont += 1;
        }

        cont = 0;
        foreach (TableRow Row in tblCapacidadDstoPago.Rows)
        {
            foreach (Control ctrl in Row.Cells[4].Controls)
            {
                if (cont == 0) Row.Cells[4].BackColor = System.Drawing.Color.White;
                ctrl.Visible = false;
            }
            cont++;
        }

    }

    // Oculto ultimas dos columnas
    private void OcultarDosColumnasCodeudores()
    {
        OcultarUnaColumnaCodeudores(); // Reuso Codigo para ocultar la ultima columna añado el necesario

        foreach (TableRow Row in TblCapacidadDepago.Rows)
        {
            //Row.Cells[3].Visible = false;
            foreach (Control ctrl in Row.Cells[3].Controls)
            {
                ctrl.Visible = false;
            }
        }

        foreach (TableRow Row in tblCapacidadDstoPago.Rows)
        {
            foreach (Control ctrl in Row.Cells[3].Controls)
            {
                ctrl.Visible = false;
            }
        }

    }

    // Oculto ultimas tres columnas
    private void OcultarTodasColumnasCodeudores()
    {
        OcultarDosColumnasCodeudores(); // Reuso Codigo para ocultar las dos ultimas columnas añado el necesario

        foreach (TableRow Row in TblCapacidadDepago.Rows)
        {
            //Row.Cells[2].Visible = false;
            foreach (Control ctrl in Row.Cells[2].Controls)
            {
                ctrl.Visible = false;
            }
        }

        foreach (TableRow Row in tblCapacidadDstoPago.Rows)
        {
            //Row.Cells[2].Visible = false;
            foreach (Control ctrl in Row.Cells[2].Controls)
            {
                ctrl.Visible = false;
            }
        }

    }

    #endregion


    #endregion


    #endregion

    #region Metodos Para Guardar Datos 


    // Guardo Analisis de Credito y retorno el ID del analisis para moldear lista de capacidad de pago
    private int GuardarAnalisisCredito()
    {
        Session[_creditoService.CodigoProgramaAnalisisCredito + ".datos"] = diccionarioIDPersonas;
        CreditoSolicitado proceso = new CreditoSolicitado();
        CreditoSolicitadoService servicio = new CreditoSolicitadoService();
        Analisis_Credito analisisCredito = new Analisis_Credito();
        Credito pCredito = new Credito();
        Credito pCredito1 = new Credito();
        analisisCredito.numero_radicacion = _numeroRadicacion;
        analisisCredito.fecha_analisis = Convert.ToDateTime(TxtFechaRecibido.Text);
        analisisCredito.justificacion = TxtJustificacion.Text;
        analisisCredito.analisis_docs = TxtDocumentosPrevistos.Text;
        analisisCredito.garantias_ofrecidas = TxtGarantiasOfrecidas.Text;
        analisisCredito.analisis_doc_cod1 = TxtCodeudor1.Text;
        analisisCredito.analisis_doc_cod2 = TxtCodeudor2.Text;
        analisisCredito.analisis_doc_cod3 = textBoxCodeudor3.Text;

        if (!string.IsNullOrWhiteSpace(TxtCuotaIngresosNeto1.Text))
        {
            analisisCredito.capacidad_pago = Convert.ToDecimal(TxtCuotaIngresosNeto1.Text.Replace("%", "").Replace(".", gSeparadorDecimal)); // Evitar conflictos con el %
        }

        analisisCredito.viabilidad = RbtViable.Checked ? "S" : "N";
        analisisCredito.documentos_provistos = TxtDocumentosPrevistos.Text;
        analisisCredito.cod_usuario = _usuario.codusuario;
        analisisCredito.ReqAfiancol = Convert.ToBoolean(CkcAfiancol.Checked) ? 1 : 0;

        try
        {
            CreditoService creditoServicio = new CreditoService();
            Credito credito = new Credito();
            credito = creditoServicio.ConsultarCredito(analisisCredito.numero_radicacion, _usuario);
            if (analisisCredito.viabilidad == "S")
            {
                // Guarda el analisis del credito y actualiza el estado del mismo
                // Se ejecuta dos metodos en el business uno para guardar y otro para actualizar
                analisisCredito = _creditoService.CrearAnalisisCredito(analisisCredito, _usuario);

            }
            else if (analisisCredito.viabilidad == "N")
            {
                Motivo motivoNegacion = new Motivo();
                motivoNegacion.Codigo = 36;
                proceso.NumeroCredito = analisisCredito.numero_radicacion;
                proceso.ObservacionesAprobacion = "ANALISIS DE CREDITO - SOLICITUD DE CREDITO NEGADA";
                proceso.Nombres = _usuario.nombre;
                analisisCredito = _creditoService.CrearAnalisisCredito(analisisCredito, _usuario);
            }

            credito = creditoServicio.ConsultarCredito(analisisCredito.numero_radicacion, _usuario);
            // SE EVITA INGRESAR REGISTRO DE CONTROL CREDITO PARA ROTATIVOS
            if (!string.IsNullOrEmpty(credito.tipo_credito))
            {
                if (credito.tipo_credito == "1" && credito.nomestado != null)
                {
                    // Consultar el proceso para Pre Aprobacion
                    proceso.estado = credito.nomestado;
                    proceso = servicio.ConsultarCodigodelProceso(proceso, _usuario);
                    ddlProceso.SelectedValue = Convert.ToString(proceso.Codigoproceso);
                    consultarControlGeneracion();
                }
            }

        }
        catch (Exception ex)
        {
            VerError("GuardarAnalisisCredito, " + ex.Message);
        }

        return analisisCredito.idanalisis;
    }


    // Lleno la lista de capacidad de pago con los datos necesarios verificando el numero de codeudores y retorno la lista para guardar
    private List<Analisis_Capacidad_Pago> MoldearListaCapacidadPago(int idAnalisis)
    {
        List<Analisis_Capacidad_Pago> lstAnalisisCapacidad = new List<Analisis_Capacidad_Pago>();

        // Guardo Capacidad Pago DEUDOR
        Analisis_Capacidad_Pago analisisCapacidadDeudor = new Analisis_Capacidad_Pago();
        if (!string.IsNullOrWhiteSpace(TxtOtrosIngresos1.Text))
        {
            analisisCapacidadDeudor.otros_ingresos = Convert.ToDecimal(TxtOtrosIngresos1.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtGastosFamiliares1.Text))
        {
            analisisCapacidadDeudor.gastos_fam = Convert.ToDecimal(TxtGastosFamiliares1.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtDeduccionesSocial1.Text))
        {
            analisisCapacidadDeudor.deduccion_nom = Convert.ToDecimal(TxtDeduccionesSocial1.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtIngresos1.Text))
        {
            analisisCapacidadDeudor.ingresos = Convert.ToDecimal(TxtIngresos1.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtCuotasFinanDeudor1.Text))
        {
            analisisCapacidadDeudor.cuotas_cod = Convert.ToDecimal(TxtCuotasFinanDeudor1.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtCuotasFinanPrincipal1.Text))
        {
            analisisCapacidadDeudor.cuotas_oblig = Convert.ToDecimal(TxtCuotasFinanPrincipal1.Text);
        }

        if (!string.IsNullOrWhiteSpace(txtArrendamiento1.Text))
            analisisCapacidadDeudor.arrendamientos = Convert.ToDecimal(txtArrendamiento1.Text);
        if (!string.IsNullOrWhiteSpace(txtHonorario1.Text))
            analisisCapacidadDeudor.honorarios = Convert.ToDecimal(txtHonorario1.Text);
        if (!string.IsNullOrWhiteSpace(txtAporte1.Text))
            analisisCapacidadDeudor.aportes = Convert.ToDecimal(txtAporte1.Text);
        if (!string.IsNullOrWhiteSpace(txtCreditoVig1.Text))
            analisisCapacidadDeudor.creditos = Convert.ToDecimal(txtCreditoVig1.Text);
        if (!string.IsNullOrWhiteSpace(txtServicio1.Text))
            analisisCapacidadDeudor.servicios = Convert.ToDecimal(txtServicio1.Text);
        if (!string.IsNullOrWhiteSpace(txtProtSalarial1.Text))
            analisisCapacidadDeudor.proteccion_salarial = Convert.ToDecimal(txtProtSalarial1.Text);
        if (!string.IsNullOrWhiteSpace(txtOtrosDsctos1.Text))
            analisisCapacidadDeudor.otro_descuento = Convert.ToDecimal(txtOtrosDsctos1.Text);
        if (!string.IsNullOrWhiteSpace(txtDeudasTer1.Text))
            analisisCapacidadDeudor.deuda_tercero = Convert.ToDecimal(txtDeudasTer1.Text);
        // CAPACIDAD DE DESCUENTO Y PAGO
        if (!string.IsNullOrWhiteSpace(txtCapDesc1.Text))
            analisisCapacidadDeudor.capacidad_descuento = Convert.ToDecimal(txtCapDesc1.Text);
        if (!string.IsNullOrWhiteSpace(txtCapPago1.Text))
            analisisCapacidadDeudor.capacidad_pago = Convert.ToDecimal(txtCapPago1.Text);

        analisisCapacidadDeudor.cod_persona = diccionarioIDPersonas[TipoDeudor.Deudor];
        analisisCapacidadDeudor.idanalisis = idAnalisis;
        diccionarioIDPersonas.Remove(0);
        CrearAnalisisInfo(analisisCapacidadDeudor, 0);
        lstAnalisisCapacidad.Add(analisisCapacidadDeudor);
        for (int i = 1; i <= diccionarioIDPersonas.Count(); i++)
        {
            // Dependiendo del numero de CODEUDORES lleno la lista con sus debidos datos
            if (i == 1)
            {
                guardarCapacidadPagoCodeudor1(idAnalisis, lstAnalisisCapacidad);
                CrearAnalisisInfo(analisisCapacidadDeudor, 1);
            }
            if (i == 2)
            {
                guardarCapacidadPagoCodeudor2(idAnalisis, lstAnalisisCapacidad);
                CrearAnalisisInfo(analisisCapacidadDeudor, 2);
            }
            if (i == 3)
            {
                guardarCapacidadPagoCodeudor3(idAnalisis, lstAnalisisCapacidad);
                CrearAnalisisInfo(analisisCapacidadDeudor, 3);
            }
        }

        return lstAnalisisCapacidad;
    }

    private void CrearAnalisisInfo(Analisis_Capacidad_Pago capacidadPago, int tipoPersona)
    {
        AnalisisInfo analisisInfo = new AnalisisInfo();

        switch (tipoPersona)
        {
            case 0:
                analisisInfo.NumeroRadicacion = _numeroRadicacion;
                analisisInfo.CodPersona = capacidadPago.cod_persona;
                analisisInfo.CodPersonacou = null;
                analisisInfo.Ingresos = Convert.ToInt32(TxtIngresos1.Text.Replace(".", ""));
                analisisInfo.OtrosIngresos = Convert.ToInt32(!string.IsNullOrEmpty(TxtOtrosIngresos1.Text) ? TxtOtrosIngresos1.Text.Replace(".", "") : "0");
                analisisInfo.Arrendamientos = Convert.ToInt32(!string.IsNullOrEmpty(txtArrendamiento1.Text) ? txtArrendamiento1.Text.Replace(".", "") : "0");
                analisisInfo.Honorarios = Convert.ToInt32(!string.IsNullOrEmpty(txtHonorario1.Text) ? txtHonorario1.Text.Replace(".", "") : "0");
                analisisInfo.CobroJuridico = Convert.ToInt32(!string.IsNullOrEmpty(TxtDeduccionesSocial1.Text) ? TxtDeduccionesSocial1.Text.Replace(".", "") : "0");
                analisisInfo.CIfinPrincipal = Convert.ToInt32(!string.IsNullOrEmpty(TxtCuotasFinanPrincipal1.Text) ? TxtCuotasFinanPrincipal1.Text.Replace(".", "") : "0");
                analisisInfo.CifinCodor = Convert.ToInt32(!string.IsNullOrEmpty(TxtCuotasFinanDeudor1.Text) ? TxtCuotasFinanDeudor1.Text.Replace(".", "") : "0");
                analisisInfo.Gastosfalimiares = Convert.ToInt32(!string.IsNullOrEmpty(TxtGastosFamiliares1.Text) ? TxtGastosFamiliares1.Text.Replace(".", "") : "0"); ;
                analisisInfo.OtrosDescuentos = Convert.ToInt32(string.IsNullOrEmpty(txtOtrosDsctos1.Text) ? "0" : txtOtrosDsctos1.Text.Replace(".", ""));
                analisisInfo.DasTercero = Convert.ToInt32(!string.IsNullOrEmpty(txtDeudasTer1.Text) ? txtDeudasTer1.Text.Replace(".", "") : "0");
                analisisInfo.DatoPersona = tipoPersona;
                analisisInfo.Aportes = Convert.ToInt32(txtAporte1.Text.Replace(".", ""));
                analisisInfo.CreditosVigentes = Convert.ToInt32(txtCreditoVig1.Text.Replace(".", ""));
                analisisInfo.Servicios = Convert.ToInt32(txtServicio1.Text.Replace(".", ""));
                analisisInfo.ProteccionSalarial = Convert.ToInt32(txtProtSalarial1.Text.Replace(".", ""));
                analisisInfo.calif_criesgo = ddlCalifica1.SelectedValue;
                if (ucFechaConsulta1.TieneDatos)
                    analisisInfo.fecha_consulta = ucFechaConsulta1.ToDateTime;

                break;
            case 1:
                analisisInfo.NumeroRadicacion = _numeroRadicacion;
                analisisInfo.CodPersona = capacidadPago.cod_persona;
                analisisInfo.CodPersonacou = null;
                analisisInfo.Ingresos = Convert.ToInt32(!string.IsNullOrEmpty(TxtIngresos2.Text) ? TxtIngresos2.Text.Replace(".", "") : "0");
                analisisInfo.OtrosIngresos = Convert.ToInt32(!string.IsNullOrEmpty(TxtOtrosIngresos2.Text) ? TxtOtrosIngresos2.Text.Replace(".", "") : "0");
                analisisInfo.Arrendamientos = Convert.ToInt32(!string.IsNullOrEmpty(txtArrendamiento2.Text) ? txtArrendamiento2.Text.Replace(".", "") : "0");
                analisisInfo.Honorarios = Convert.ToInt32(!string.IsNullOrEmpty(txtHonorario2.Text) ? txtHonorario2.Text.Replace(".", "") : "0");
                analisisInfo.CobroJuridico = Convert.ToInt32(!string.IsNullOrEmpty(TxtDeduccionesSocial2.Text) ? TxtDeduccionesSocial2.Text.Replace(".", "") : "0");
                analisisInfo.CIfinPrincipal = Convert.ToInt32(!string.IsNullOrEmpty(TxtCuotasFinanPrincipal2.Text) ? TxtCuotasFinanPrincipal2.Text.Replace(".", "") : "0");
                analisisInfo.CifinCodor = Convert.ToInt32(!string.IsNullOrEmpty(TxtCuotasFinanDeudor2.Text) ? TxtCuotasFinanDeudor2.Text.Replace(".", "") : "0");
                analisisInfo.Gastosfalimiares = Convert.ToInt32(!string.IsNullOrEmpty(TxtGastosFamiliares2.Text) ? TxtGastosFamiliares2.Text.Replace(".", "") : "0"); ;
                analisisInfo.OtrosDescuentos = Convert.ToInt32(string.IsNullOrEmpty(txtOtrosDsctos2.Text) ? "0" : txtOtrosDsctos2.Text.Replace(".", ""));
                analisisInfo.DasTercero = Convert.ToInt32(!string.IsNullOrEmpty(txtDeudasTer2.Text) ? txtDeudasTer2.Text.Replace(".", "") : "0");
                analisisInfo.DatoPersona = tipoPersona;
                analisisInfo.Aportes = Convert.ToInt32(txtAporte2.Text.Replace(".", ""));
                analisisInfo.CreditosVigentes = Convert.ToInt32(txtCreditoVig2.Text.Replace(".", ""));
                analisisInfo.Servicios = Convert.ToInt32(txtServicio2.Text.Replace(".", ""));
                analisisInfo.ProteccionSalarial = Convert.ToInt32(txtProtSalarial2.Text.Replace(".", ""));
                analisisInfo.calif_criesgo = ddlCalifica2.SelectedValue;
                if (ucFechaConsulta2.TieneDatos)
                    analisisInfo.fecha_consulta = ucFechaConsulta2.ToDateTime;

                break;
            case 2:
                analisisInfo.NumeroRadicacion = _numeroRadicacion;
                analisisInfo.CodPersona = capacidadPago.cod_persona;
                analisisInfo.CodPersonacou = null;
                analisisInfo.Ingresos = Convert.ToInt32(TxtIngresos3.Text.Replace(".", ""));
                analisisInfo.OtrosIngresos = Convert.ToInt32(!string.IsNullOrEmpty(TxtOtrosIngresos3.Text) ? TxtOtrosIngresos3.Text.Replace(".", "") : "0");
                analisisInfo.Arrendamientos = Convert.ToInt32(!string.IsNullOrEmpty(txtArrendamiento3.Text) ? txtArrendamiento3.Text.Replace(".", "") : "0");
                analisisInfo.Honorarios = Convert.ToInt32(!string.IsNullOrEmpty(txtHonorario3.Text) ? txtHonorario3.Text.Replace(".", "") : "0");
                analisisInfo.CobroJuridico = Convert.ToInt32(!string.IsNullOrEmpty(TxtDeduccionesSocial3.Text) ? TxtDeduccionesSocial3.Text.Replace(".", "") : "0");
                analisisInfo.CIfinPrincipal = Convert.ToInt32(!string.IsNullOrEmpty(TxtCuotasFinanPrincipal3.Text) ? TxtCuotasFinanPrincipal3.Text.Replace(".", "") : "0");
                analisisInfo.CifinCodor = Convert.ToInt32(!string.IsNullOrEmpty(TxtCuotasFinanDeudor3.Text) ? TxtCuotasFinanDeudor3.Text.Replace(".", "") : "0");
                analisisInfo.Gastosfalimiares = Convert.ToInt32(!string.IsNullOrEmpty(TxtGastosFamiliares3.Text) ? TxtGastosFamiliares3.Text.Replace(".", "") : "0");
                analisisInfo.OtrosDescuentos = Convert.ToInt32(string.IsNullOrEmpty(txtOtrosDsctos3.Text) ? "0" : txtOtrosDsctos3.Text.Replace(".", ""));
                analisisInfo.DasTercero = Convert.ToInt32(!string.IsNullOrEmpty(txtDeudasTer3.Text) ? txtDeudasTer3.Text.Replace(".", "") : "0");
                analisisInfo.DatoPersona = tipoPersona;
                analisisInfo.Aportes = Convert.ToInt32(txtAporte3.Text.Replace(".", ""));
                analisisInfo.CreditosVigentes = Convert.ToInt32(txtCreditoVig3.Text.Replace(".", ""));
                analisisInfo.Servicios = Convert.ToInt32(txtServicio3.Text.Replace(".", ""));
                analisisInfo.ProteccionSalarial = Convert.ToInt32(txtProtSalarial3.Text.Replace(".", ""));
                analisisInfo.calif_criesgo = ddlCalifica3.SelectedValue;
                if (ucFechaConsulta3.TieneDatos)
                    analisisInfo.fecha_consulta = ucFechaConsulta3.ToDateTime;

                break;
            case 3:
                analisisInfo.NumeroRadicacion = _numeroRadicacion;
                analisisInfo.CodPersona = capacidadPago.cod_persona;
                analisisInfo.CodPersonacou = null;
                analisisInfo.Ingresos = Convert.ToInt32(TxtIngresos4.Text.Replace(".", ""));
                analisisInfo.OtrosIngresos = Convert.ToInt32(!string.IsNullOrEmpty(TxtOtrosIngresos4.Text) ? TxtOtrosIngresos4.Text.Replace(".", "") : "0");
                analisisInfo.Arrendamientos = Convert.ToInt32(!string.IsNullOrEmpty(txtArrendamiento4.Text) ? txtArrendamiento4.Text.Replace(".", "") : "0");
                analisisInfo.Honorarios = Convert.ToInt32(!string.IsNullOrEmpty(txtHonorario4.Text) ? txtHonorario4.Text.Replace(".", "") : "0");
                analisisInfo.CobroJuridico = Convert.ToInt32(!string.IsNullOrEmpty(TxtDeduccionesSocial4.Text) ? TxtDeduccionesSocial4.Text.Replace(".", "") : "0");
                analisisInfo.CIfinPrincipal = Convert.ToInt32(!string.IsNullOrEmpty(TxtCuotasFinanPrincipal4.Text) ? TxtCuotasFinanPrincipal4.Text.Replace(".", "") : "0");
                analisisInfo.CifinCodor = Convert.ToInt32(!string.IsNullOrEmpty(TxtCuotasFinanDeudor4.Text) ? TxtCuotasFinanDeudor4.Text.Replace(".", "") : "0");
                analisisInfo.Gastosfalimiares = Convert.ToInt32(!string.IsNullOrEmpty(TxtGastosFamiliares4.Text) ? TxtGastosFamiliares4.Text.Replace(".", "") : "0");
                analisisInfo.OtrosDescuentos = Convert.ToInt32(string.IsNullOrEmpty(txtOtrosDsctos4.Text) ? "0" : txtOtrosDsctos4.Text.Replace(".", ""));
                analisisInfo.DasTercero = Convert.ToInt32(!string.IsNullOrEmpty(txtDeudasTer4.Text) ? txtDeudasTer4.Text.Replace(".", "") : "0");
                analisisInfo.DatoPersona = tipoPersona;
                analisisInfo.Aportes = Convert.ToInt32(txtAporte4.Text.Replace(".", ""));
                analisisInfo.CreditosVigentes = Convert.ToInt32(txtCreditoVig4.Text.Replace(".", ""));
                analisisInfo.Servicios = Convert.ToInt32(txtServicio4.Text.Replace(".", ""));
                analisisInfo.ProteccionSalarial = Convert.ToInt32(txtProtSalarial4.Text.Replace(".", "")); analisisInfo.calif_criesgo = ddlCalifica1.SelectedValue;
                analisisInfo.calif_criesgo = ddlCalifica4.SelectedValue;
                if (ucFechaConsulta4.TieneDatos)
                    analisisInfo.fecha_consulta = ucFechaConsulta4.ToDateTime;

                break;
        }
        _creditoService.CrearAnalisis_Info(analisisInfo, (Usuario)Session["usuario"]);
    }


    // Lleno los datos de la capacidad de pago de los CODEUDORES si hay CODEUDORES segun "MoldearListaCapacidadPago"
    #region LlenarDatosCodeudores


    private void guardarCapacidadPagoCodeudor3(int idAnalisis, List<Analisis_Capacidad_Pago> lstAnalisisCapacidad)
    {
        Analisis_Capacidad_Pago analisisCapacidadCOD4 = new Analisis_Capacidad_Pago();

        if (!string.IsNullOrWhiteSpace(TxtOtrosIngresos4.Text))
        {
            analisisCapacidadCOD4.otros_ingresos = Convert.ToDecimal(TxtOtrosIngresos4.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtGastosFamiliares4.Text))
        {
            analisisCapacidadCOD4.gastos_fam = Convert.ToDecimal(TxtGastosFamiliares4.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtDeduccionesSocial4.Text))
        {
            analisisCapacidadCOD4.deduccion_nom = Convert.ToDecimal(TxtDeduccionesSocial4.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtIngresos4.Text))
        {
            analisisCapacidadCOD4.ingresos = Convert.ToDecimal(TxtIngresos4.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtCuotasFinanDeudor4.Text))
        {
            analisisCapacidadCOD4.cuotas_cod = Convert.ToDecimal(TxtCuotasFinanDeudor4.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtCuotasFinanPrincipal4.Text))
        {
            analisisCapacidadCOD4.cuotas_oblig = Convert.ToDecimal(TxtCuotasFinanPrincipal4.Text);
        }

        if (!string.IsNullOrWhiteSpace(txtArrendamiento4.Text))
            analisisCapacidadCOD4.arrendamientos = Convert.ToDecimal(txtArrendamiento4.Text);
        if (!string.IsNullOrWhiteSpace(txtHonorario4.Text))
            analisisCapacidadCOD4.honorarios = Convert.ToDecimal(txtHonorario4.Text);
        if (!string.IsNullOrWhiteSpace(txtAporte4.Text))
            analisisCapacidadCOD4.aportes = Convert.ToDecimal(txtAporte4.Text);
        if (!string.IsNullOrWhiteSpace(txtCreditoVig4.Text))
            analisisCapacidadCOD4.creditos = Convert.ToDecimal(txtCreditoVig4.Text);
        if (!string.IsNullOrWhiteSpace(txtServicio4.Text))
            analisisCapacidadCOD4.servicios = Convert.ToDecimal(txtServicio4.Text);
        if (!string.IsNullOrWhiteSpace(txtProtSalarial4.Text))
            analisisCapacidadCOD4.proteccion_salarial = Convert.ToDecimal(txtProtSalarial4.Text);
        if (!string.IsNullOrWhiteSpace(txtOtrosDsctos4.Text))
            analisisCapacidadCOD4.otro_descuento = Convert.ToDecimal(txtOtrosDsctos4.Text);
        if (!string.IsNullOrWhiteSpace(txtDeudasTer4.Text))
            analisisCapacidadCOD4.deuda_tercero = Convert.ToDecimal(txtDeudasTer4.Text);
        // CAPACIDAD DE DESCUENTO Y PAGO
        if (!string.IsNullOrWhiteSpace(txtCapDesc4.Text))
            analisisCapacidadCOD4.capacidad_descuento = Convert.ToDecimal(txtCapDesc4.Text);
        if (!string.IsNullOrWhiteSpace(txtCapPago4.Text))
            analisisCapacidadCOD4.capacidad_pago = Convert.ToDecimal(txtCapPago4.Text);

        analisisCapacidadCOD4.cod_persona = diccionarioIDPersonas[TipoDeudor.Codeudor3];
        analisisCapacidadCOD4.idanalisis = idAnalisis;

        lstAnalisisCapacidad.Add(analisisCapacidadCOD4);
    }


    private void guardarCapacidadPagoCodeudor2(int idAnalisis, List<Analisis_Capacidad_Pago> lstAnalisisCapacidad)
    {
        Analisis_Capacidad_Pago analisisCapacidadCOD3 = new Analisis_Capacidad_Pago();

        if (!string.IsNullOrWhiteSpace(TxtOtrosIngresos3.Text))
        {
            analisisCapacidadCOD3.otros_ingresos = Convert.ToDecimal(TxtOtrosIngresos3.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtGastosFamiliares3.Text))
        {
            analisisCapacidadCOD3.gastos_fam = Convert.ToDecimal(TxtGastosFamiliares3.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtDeduccionesSocial3.Text))
        {
            analisisCapacidadCOD3.deduccion_nom = Convert.ToDecimal(TxtDeduccionesSocial3.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtIngresos3.Text))
        {
            analisisCapacidadCOD3.ingresos = Convert.ToDecimal(TxtIngresos3.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtCuotasFinanDeudor3.Text))
        {
            analisisCapacidadCOD3.cuotas_cod = Convert.ToDecimal(TxtCuotasFinanDeudor3.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtCuotasFinanPrincipal3.Text))
        {
            analisisCapacidadCOD3.cuotas_oblig = Convert.ToDecimal(TxtCuotasFinanPrincipal3.Text);
        }

        if (!string.IsNullOrWhiteSpace(txtArrendamiento3.Text))
            analisisCapacidadCOD3.arrendamientos = Convert.ToDecimal(txtArrendamiento3.Text);
        if (!string.IsNullOrWhiteSpace(txtHonorario3.Text))
            analisisCapacidadCOD3.honorarios = Convert.ToDecimal(txtHonorario3.Text);
        if (!string.IsNullOrWhiteSpace(txtAporte3.Text))
            analisisCapacidadCOD3.aportes = Convert.ToDecimal(txtAporte3.Text);
        if (!string.IsNullOrWhiteSpace(txtCreditoVig3.Text))
            analisisCapacidadCOD3.creditos = Convert.ToDecimal(txtCreditoVig3.Text);
        if (!string.IsNullOrWhiteSpace(txtServicio3.Text))
            analisisCapacidadCOD3.servicios = Convert.ToDecimal(txtServicio3.Text);
        if (!string.IsNullOrWhiteSpace(txtProtSalarial3.Text))
            analisisCapacidadCOD3.proteccion_salarial = Convert.ToDecimal(txtProtSalarial3.Text);
        if (!string.IsNullOrWhiteSpace(txtOtrosDsctos3.Text))
            analisisCapacidadCOD3.otro_descuento = Convert.ToDecimal(txtOtrosDsctos3.Text);
        if (!string.IsNullOrWhiteSpace(txtDeudasTer3.Text))
            analisisCapacidadCOD3.deuda_tercero = Convert.ToDecimal(txtDeudasTer3.Text);
        // CAPACIDAD DE DESCUENTO Y PAGO
        if (!string.IsNullOrWhiteSpace(txtCapDesc3.Text))
            analisisCapacidadCOD3.capacidad_descuento = Convert.ToDecimal(txtCapDesc3.Text);
        if (!string.IsNullOrWhiteSpace(txtCapPago3.Text))
            analisisCapacidadCOD3.capacidad_pago = Convert.ToDecimal(txtCapPago3.Text);

        analisisCapacidadCOD3.cod_persona = diccionarioIDPersonas[TipoDeudor.Codeudor2];
        analisisCapacidadCOD3.idanalisis = idAnalisis;

        lstAnalisisCapacidad.Add(analisisCapacidadCOD3);
    }


    private void guardarCapacidadPagoCodeudor1(int idAnalisis, List<Analisis_Capacidad_Pago> lstAnalisisCapacidad)
    {
        if (Session[_creditoService.CodigoProgramaAnalisisCredito + ".datos"] != null)
            diccionarioIDPersonas = (Dictionary<TipoDeudor, long?>)Session[_creditoService.CodigoProgramaAnalisisCredito + ".datos"];
        Analisis_Capacidad_Pago analisisCapacidadCOD2 = new Analisis_Capacidad_Pago();

        if (!string.IsNullOrWhiteSpace(TxtOtrosIngresos2.Text))
        {
            analisisCapacidadCOD2.otros_ingresos = Convert.ToDecimal(TxtOtrosIngresos2.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtGastosFamiliares2.Text))
        {
            analisisCapacidadCOD2.gastos_fam = Convert.ToDecimal(TxtGastosFamiliares2.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtDeduccionesSocial2.Text))
        {
            analisisCapacidadCOD2.deduccion_nom = Convert.ToDecimal(TxtDeduccionesSocial2.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtIngresos2.Text))
        {
            analisisCapacidadCOD2.ingresos = Convert.ToDecimal(TxtIngresos2.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtCuotasFinanDeudor2.Text))
        {
            analisisCapacidadCOD2.cuotas_cod = Convert.ToDecimal(TxtCuotasFinanDeudor2.Text);
        }
        if (!string.IsNullOrWhiteSpace(TxtCuotasFinanPrincipal2.Text))
        {
            analisisCapacidadCOD2.cuotas_oblig = Convert.ToDecimal(TxtCuotasFinanPrincipal2.Text);
        }

        if (!string.IsNullOrWhiteSpace(txtArrendamiento2.Text))
            analisisCapacidadCOD2.arrendamientos = Convert.ToDecimal(txtArrendamiento2.Text);
        if (!string.IsNullOrWhiteSpace(txtHonorario2.Text))
            analisisCapacidadCOD2.honorarios = Convert.ToDecimal(txtHonorario2.Text);
        if (!string.IsNullOrWhiteSpace(txtAporte2.Text))
            analisisCapacidadCOD2.aportes = Convert.ToDecimal(txtAporte2.Text);
        if (!string.IsNullOrWhiteSpace(txtCreditoVig2.Text))
            analisisCapacidadCOD2.creditos = Convert.ToDecimal(txtCreditoVig2.Text);
        if (!string.IsNullOrWhiteSpace(txtServicio2.Text))
            analisisCapacidadCOD2.servicios = Convert.ToDecimal(txtServicio2.Text);
        if (!string.IsNullOrWhiteSpace(txtProtSalarial2.Text))
            analisisCapacidadCOD2.proteccion_salarial = Convert.ToDecimal(txtProtSalarial2.Text);
        if (!string.IsNullOrWhiteSpace(txtOtrosDsctos2.Text))
            analisisCapacidadCOD2.otro_descuento = Convert.ToDecimal(txtOtrosDsctos2.Text);
        if (!string.IsNullOrWhiteSpace(txtDeudasTer2.Text))
            analisisCapacidadCOD2.deuda_tercero = Convert.ToDecimal(txtDeudasTer2.Text);
        // CAPACIDAD DE DESCUENTO Y PAGO
        if (!string.IsNullOrWhiteSpace(txtCapDesc2.Text))
            analisisCapacidadCOD2.capacidad_descuento = Convert.ToDecimal(txtCapDesc2.Text);
        if (!string.IsNullOrWhiteSpace(txtCapPago2.Text))
            analisisCapacidadCOD2.capacidad_pago = Convert.ToDecimal(txtCapPago2.Text);

        analisisCapacidadCOD2.cod_persona = diccionarioIDPersonas[TipoDeudor.Codeudor1];
        analisisCapacidadCOD2.idanalisis = idAnalisis;

        lstAnalisisCapacidad.Add(analisisCapacidadCOD2);
    }


    #endregion


    // Con la lista de MoldearListaCapacidadPago guardo la capacidad de pago
    private bool GuardarCapacidadPago(List<Analisis_Capacidad_Pago> lstAnalisisCapacidad)
    {
        try
        {
            foreach (var analisisGuardar in lstAnalisisCapacidad)
            {
                _creditoService.CrearAnalisisCapacidadPago(analisisGuardar, _usuario);
            }
            return true;
        }
        catch (Exception ex)
        {
            VerError("GuardarCapacidadPago, " + ex.Message);
            return false;
        }
    }

    //Agregado para consultar proceso del credito
    private void consultarControlGeneracion()
    {
        try
        {
            Usuario pUsuarios = _usuario;
            Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
            Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new ControlCreditosService();
            String radicado = Convert.ToString(this.TxtNumeroSolicitud.Text);
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

    private void GuardarControl()
    {
        Xpinn.FabricaCreditos.Services.ControlCreditosService ControlCreditosServicio = new Xpinn.FabricaCreditos.Services.ControlCreditosService();
        String FechaDatcaredito = "";
        Usuario pUsuario = (Usuario)Session["usuario"];
        Xpinn.FabricaCreditos.Entities.ControlCreditos vControlCreditos = new Xpinn.FabricaCreditos.Entities.ControlCreditos();
        vControlCreditos.numero_radicacion = Convert.ToInt64(this.TxtNumeroSolicitud.Text);
        vControlCreditos.numero_radicacion = Convert.ToInt64(this.TxtNumeroSolicitud.Text);
        vControlCreditos.codtipoproceso = ddlProceso.SelectedItem != null ? ddlProceso.SelectedValue : null;
        vControlCreditos.fechaproceso = Convert.ToDateTime(DateTime.Now);
        vControlCreditos.cod_persona = pUsuario.codusuario;
        vControlCreditos.cod_motivo = 0;
        String viabilidad = RbtViable.Checked ? "S" : "N";
        if (viabilidad == "S")
            vControlCreditos.observaciones = "ANALISIS DE CREDITO - SOLICITUD DE CREDITO VIABLE";
        else if (viabilidad == "N")
            vControlCreditos.observaciones = "ANALISIS DE CREDITO - SOLICITUD DE CREDITO NEGADA";
        vControlCreditos.anexos = null;
        vControlCreditos.nivel = 0;
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
        vControlCreditos = ControlCreditosServicio.CrearControlCreditos(vControlCreditos, (Usuario)Session["usuario"]);
    }

    #endregion

    #region Llenar Reporte

    private void LlenarVariables(long idCliente)
    {
        Codeudores.IdentifSolicitante = idCliente.ToString();
        CuotasExtras.Monto = TxtValorSolicitado.Text;
        CuotasExtras.Periodicidad = TxtFrecuenciaCuota.Text;
        CuotasExtras.PlazoTxt = TxtPlazo.Text;
    }

    private void LlenarReporte()
    {

        LlenarDatosAnalisis(_numeroRadicacion);
        string cRutaDeImagen = ImagenReporte();

        // Lleno DataTable AnalisisPromedio
        DataTable dtAnalisis = LlenarDataTableAnalisisPromedio();

        // Lleno DataTable EstadoCuenta
        DataTable dtEstadoCuenta = LlenarDataTableEstadoCuenta();

        DataTable dtCalificacion = LlenarDataTableHIstorial();

        // Lleno los parametros del reporte
        ReportParameter[] param = LlenarParametrosReporte(cRutaDeImagen);

        // Limpiar DatSource de Reporte
        ReportViewerPlan.LocalReport.DataSources.Clear();

        // Cargo los DataTables al DataSource del reporte y lo muestro
        ReportDataSource rds1 = new ReportDataSource("DataAnalisis", dtAnalisis);
        ReportViewerPlan.LocalReport.DataSources.Add(rds1);

        ReportDataSource rds2 = new ReportDataSource("DataEstado_Cuenta", dtEstadoCuenta);
        ReportViewerPlan.LocalReport.DataSources.Add(rds2);

        ReportDataSource rds3 = new ReportDataSource("DataHistorial", dtCalificacion);
        ReportViewerPlan.LocalReport.DataSources.Add(rds3);

        ReportViewerPlan.LocalReport.ReportPath = "Page\\FabricaCreditos\\Analisis\\ReportAnalisisCredito.rdlc";
        ReportViewerPlan.LocalReport.EnableExternalImages = true;
        ReportViewerPlan.LocalReport.SetParameters(param);
        ReportViewerPlan.LocalReport.Refresh();
        ReportViewerPlan.Visible = true;

    }


    private ReportParameter[] LlenarParametrosReporte(string cRutaDeImagen)
    {
        ReportParameter[] param = new ReportParameter[124];

        param[0] = new ReportParameter("ImagenReport", cRutaDeImagen);
        param[1] = new ReportParameter("Identificacion", TxtDocumentoIdentidad.Text);
        param[2] = new ReportParameter("Solicitante", TxtSolicitante.Text);
        param[3] = new ReportParameter("Forma_Pago", TxtFormaPago.Text);
        param[4] = new ReportParameter("Fecha_Recibido", TxtFechaRecibido.Text);
        param[5] = new ReportParameter("Valor_Solicitado", FormatearNumerosParaReporteODarDefault(TxtValorSolicitado.Text));
        param[7] = new ReportParameter("Frecuencia_Cuota", TxtFrecuenciaCuota.Text);
        param[8] = new ReportParameter("ValorGirar", " " + FormatearNumerosParaReporteODarDefault(txtValorGirar.Text));
        param[10] = new ReportParameter("Num_Solicitud", TxtNumeroSolicitud.Text);
        param[11] = new ReportParameter("Profesion", TxtActividadProfesion.Text);

        TextBox txtTotalSaldoPromedio = gvAnalisisPromedio.FooterRow.FindControl("txtTotalSaldoPromedio") as TextBox;
        param[12] = new ReportParameter("TotalSaldoAnalisis", txtTotalSaldoPromedio.Text);

        param[13] = new ReportParameter("TotalCupoDisponibleAnalisis", FormatearNumerosParaReporteODarDefault(txtCupoDisponible.Text));

        TextBox txtTotalValorInicial = gvEstadoCuenta.FooterRow.FindControl("txtTotalValorInicial") as TextBox;
        param[14] = new ReportParameter("TotalValorInicial", txtTotalValorInicial.Text);

        TextBox txtTotalSaldo = gvEstadoCuenta.FooterRow.FindControl("txtTotalSaldo") as TextBox;
        param[15] = new ReportParameter("TotalSaldoEstado", txtTotalSaldo.Text);

        TextBox txtTotalValorCuota = gvEstadoCuenta.FooterRow.FindControl("txtTotalValorCuota") as TextBox;
        param[16] = new ReportParameter("TotalValorCuotaEstado", txtTotalValorCuota.Text);

        param[17] = new ReportParameter("IngresoDeudor", FormatearNumerosParaReporteODarDefault(TxtIngresos1.Text));
        param[18] = new ReportParameter("OtrosIngresosDeudor", FormatearNumerosParaReporteODarDefault(TxtOtrosIngresos1.Text));
        param[19] = new ReportParameter("TotalBrutosDeudor", FormatearNumerosParaReporteODarDefault(TxtIngresosBrutos1.Text));
        param[20] = new ReportParameter("DeduSocialDeudor", FormatearNumerosParaReporteODarDefault(TxtDeduccionesSocial1.Text));
        param[21] = new ReportParameter("CuoFinanPrinDeudor", FormatearNumerosParaReporteODarDefault(TxtCuotasFinanPrincipal1.Text));
        param[22] = new ReportParameter("CuoFinanCodDeudor", FormatearNumerosParaReporteODarDefault(TxtCuotasFinanDeudor1.Text));
        param[23] = new ReportParameter("GastosFamiDeudor", FormatearNumerosParaReporteODarDefault(TxtGastosFamiliares1.Text));
        param[24] = new ReportParameter("TotalEgresoDeudor", FormatearNumerosParaReporteODarDefault(TxtB1.Text));
        param[25] = new ReportParameter("NetoMensualDeudor", FormatearNumerosParaReporteODarDefault(TxtIngresosMensual1.Text));
        param[27] = new ReportParameter("CuoCreSolicitado", FormatearNumerosParaReporteODarDefault(TxtCuotaCredito1.Text));
        param[28] = new ReportParameter("CuotaSobreIngreso", TxtCuotaIngresosNeto1.Text + " %");

        param[29] = new ReportParameter("IngresoCod1", FormatearNumerosParaReporteODarDefault(TxtIngresos2.Text));
        param[30] = new ReportParameter("OtrosIngresosCod1", FormatearNumerosParaReporteODarDefault(TxtOtrosIngresos2.Text));
        param[31] = new ReportParameter("TotalBrutosCod1", FormatearNumerosParaReporteODarDefault(TxtIngresosBrutos2.Text));
        param[32] = new ReportParameter("DeduSocialCod1", FormatearNumerosParaReporteODarDefault(TxtDeduccionesSocial2.Text));
        param[33] = new ReportParameter("CuoFinanPrinCod1", FormatearNumerosParaReporteODarDefault(TxtCuotasFinanPrincipal2.Text));
        param[34] = new ReportParameter("CuoFinanCodCod1", FormatearNumerosParaReporteODarDefault(TxtCuotasFinanDeudor2.Text));
        param[35] = new ReportParameter("GastosFamiCod1", FormatearNumerosParaReporteODarDefault(TxtGastosFamiliares2.Text));
        param[36] = new ReportParameter("TotalEgresoCod1", FormatearNumerosParaReporteODarDefault(TxtB2.Text));
        param[37] = new ReportParameter("NetoMensualCod1", FormatearNumerosParaReporteODarDefault(TxtIngresosMensual2.Text));

        param[39] = new ReportParameter("IngresoCod2", FormatearNumerosParaReporteODarDefault(TxtIngresos3.Text));
        param[40] = new ReportParameter("OtrosIngresosCod2", FormatearNumerosParaReporteODarDefault(TxtOtrosIngresos3.Text));
        param[41] = new ReportParameter("TotalBrutosCod2", FormatearNumerosParaReporteODarDefault(TxtIngresosBrutos3.Text));
        param[42] = new ReportParameter("DeduSocialCod2", FormatearNumerosParaReporteODarDefault(TxtDeduccionesSocial3.Text));
        param[43] = new ReportParameter("CuoFinanPrinCod2", FormatearNumerosParaReporteODarDefault(TxtCuotasFinanPrincipal3.Text));
        param[44] = new ReportParameter("CuoFinanCodCod2", FormatearNumerosParaReporteODarDefault(TxtCuotasFinanDeudor3.Text));
        param[45] = new ReportParameter("GastosFamiCod2", FormatearNumerosParaReporteODarDefault(TxtGastosFamiliares3.Text));
        param[46] = new ReportParameter("TotalEgresoCod2", FormatearNumerosParaReporteODarDefault(TxtB3.Text));
        param[47] = new ReportParameter("NetoMensualCod2", FormatearNumerosParaReporteODarDefault(TxtIngresosMensual3.Text));

        param[49] = new ReportParameter("IngresoCod3", FormatearNumerosParaReporteODarDefault(TxtIngresos4.Text));
        param[50] = new ReportParameter("OtrosIngresosCod3", FormatearNumerosParaReporteODarDefault(TxtOtrosIngresos4.Text));
        param[51] = new ReportParameter("TotalBrutosCod3", FormatearNumerosParaReporteODarDefault(TxtIngresosBrutos4.Text));
        param[52] = new ReportParameter("DeduSocialCod3", FormatearNumerosParaReporteODarDefault(TxtDeduccionesSocial4.Text));
        param[53] = new ReportParameter("CuoFinanPrinCod3", FormatearNumerosParaReporteODarDefault(TxtCuotasFinanPrincipal4.Text));
        param[54] = new ReportParameter("CuoFinanCodCod3", FormatearNumerosParaReporteODarDefault(TxtCuotasFinanDeudor4.Text));
        param[55] = new ReportParameter("GastosFamiCod3", FormatearNumerosParaReporteODarDefault(TxtGastosFamiliares4.Text));
        param[56] = new ReportParameter("TotalEgresoCod3", FormatearNumerosParaReporteODarDefault(TxtB4.Text));
        param[57] = new ReportParameter("NetoMensualCod3", FormatearNumerosParaReporteODarDefault(TxtIngresosMensual4.Text));

        param[59] = new ReportParameter("GarantiasOfrecidas", TxtGarantiasOfrecidas.Text);
        param[60] = new ReportParameter("DocumentosProvistos", TxtDocumentosPrevistos.Text);
        param[61] = new ReportParameter("GarantiasOfrecidasCod1", TxtCodeudor1.Text);
        param[62] = new ReportParameter("GarantiasOfrecidasCod2", TxtCodeudor2.Text);
        param[63] = new ReportParameter("GarantiasOfrecidasCod3", textBoxCodeudor3.Text);

        if (Session["Viabilidad"].ToString() == "S")
        {
            param[64] = new ReportParameter("Viable", " X ");
            param[65] = new ReportParameter("NoViable", "   ");
        }
        else
        {
            param[64] = new ReportParameter("Viable", "   ");
            param[65] = new ReportParameter("NoViable", " X ");
        }

        param[66] = new ReportParameter("Justificacion", TxtJustificacion.Text);
        param[67] = new ReportParameter("EmpresaNombre", " " + txtEmpresaReca.Text);
        param[71] = new ReportParameter("NetoTrimestralDeudor", FormatearNumerosParaReporteODarDefault(TxtIngresoTrimestral1.Text));
        param[68] = new ReportParameter("NetoTrimestralCod1", FormatearNumerosParaReporteODarDefault(TxtIngresoTrimestral2.Text));
        param[69] = new ReportParameter("NetoTrimestralCod2", FormatearNumerosParaReporteODarDefault(TxtIngresoTrimestral3.Text));
        param[70] = new ReportParameter("NetoTrimestralCod3", FormatearNumerosParaReporteODarDefault(TxtIngresoTrimestral4.Text));

        param[48] = new ReportParameter("Edad", TxtEdad.Text);
        param[26] = new ReportParameter("LineaDeCredito", TxtModalidadCredito.Text);
        param[6] = new ReportParameter("Plazo", TxtPlazo.Text);
        param[9] = new ReportParameter("AnalistaCedula", _usuario.identificacion);
        param[38] = new ReportParameter("AnalistaNombre", _usuario.nombre);
        param[58] = new ReportParameter("FechaDeHoy", DateTime.Now.Date.ToShortDateString());
        param[72] = new ReportParameter("Antig_Asociado", " " + txtAntAsociado.Text);
        param[73] = new ReportParameter("Antig_Laboral", " " + txtAntLaboral.Text);
        param[74] = new ReportParameter("TipoContrato", " " + txtTipoContrato.Text);
        param[75] = new ReportParameter("Fecha_Solicitud", " " + txtFechaSoli.Text);
        param[76] = new ReportParameter("Tasa", " " + txtTasa.Text);
        param[77] = new ReportParameter("ArrendamiDeudor", " " + FormatearNumerosParaReporteODarDefault(txtArrendamiento1.Text));
        param[78] = new ReportParameter("HonorarioDeudor", " " + FormatearNumerosParaReporteODarDefault(txtHonorario1.Text));
        param[79] = new ReportParameter("AportesDeudor", " " + FormatearNumerosParaReporteODarDefault(txtAporte1.Text));
        param[80] = new ReportParameter("CreditosDeudor", " " + FormatearNumerosParaReporteODarDefault(txtCreditoVig1.Text));
        param[81] = new ReportParameter("ServiciosDeudor", " " + FormatearNumerosParaReporteODarDefault(txtServicio1.Text));
        param[82] = new ReportParameter("ProtecSalarialDeudor", " " + FormatearNumerosParaReporteODarDefault(txtProtSalarial1.Text));

        param[83] = new ReportParameter("ArrendamiCod1", " " + FormatearNumerosParaReporteODarDefault(txtArrendamiento2.Text));
        param[84] = new ReportParameter("HonorarioCod1", " " + FormatearNumerosParaReporteODarDefault(txtHonorario2.Text));
        param[85] = new ReportParameter("AportesCod1", " " + FormatearNumerosParaReporteODarDefault(txtAporte2.Text));
        param[86] = new ReportParameter("CreditosCod1", " " + FormatearNumerosParaReporteODarDefault(txtCreditoVig2.Text));
        param[87] = new ReportParameter("ServiciosCod1", " " + FormatearNumerosParaReporteODarDefault(txtServicio2.Text));
        param[88] = new ReportParameter("ProtecSalarialCod1", " " + FormatearNumerosParaReporteODarDefault(txtProtSalarial2.Text));

        param[89] = new ReportParameter("ArrendamiCod2", " " + FormatearNumerosParaReporteODarDefault(txtArrendamiento3.Text));
        param[90] = new ReportParameter("HonorarioCod2", " " + FormatearNumerosParaReporteODarDefault(txtHonorario3.Text));
        param[91] = new ReportParameter("AportesCod2", " " + FormatearNumerosParaReporteODarDefault(txtAporte3.Text));
        param[92] = new ReportParameter("CreditosCod2", " " + FormatearNumerosParaReporteODarDefault(txtCreditoVig3.Text));
        param[93] = new ReportParameter("ServiciosCod2", " " + FormatearNumerosParaReporteODarDefault(txtServicio3.Text));
        param[94] = new ReportParameter("ProtecSalarialCod2", " " + FormatearNumerosParaReporteODarDefault(txtProtSalarial3.Text));

        param[95] = new ReportParameter("ArrendamiCod3", " " + FormatearNumerosParaReporteODarDefault(txtArrendamiento4.Text));
        param[96] = new ReportParameter("HonorarioCod3", " " + FormatearNumerosParaReporteODarDefault(txtHonorario4.Text));
        param[97] = new ReportParameter("AportesCod3", " " + FormatearNumerosParaReporteODarDefault(txtAporte4.Text));
        param[98] = new ReportParameter("CreditosCod3", " " + FormatearNumerosParaReporteODarDefault(txtCreditoVig4.Text));
        param[99] = new ReportParameter("ServiciosCod3", " " + FormatearNumerosParaReporteODarDefault(txtServicio4.Text));
        param[100] = new ReportParameter("ProtecSalarialCod3", " " + FormatearNumerosParaReporteODarDefault(txtProtSalarial4.Text));
        param[101] = new ReportParameter("TotalCuotaDescontar", " " + FormatearNumerosParaReporteODarDefault(txtVrCuotaDescontar.Text));
        param[102] = new ReportParameter("VrDeuda", " " + FormatearNumerosParaReporteODarDefault(txtDeudasActuales.Text));
        param[103] = new ReportParameter("OtroDsctoDeudor", " " + FormatearNumerosParaReporteODarDefault(txtOtrosDsctos1.Text));
        param[104] = new ReportParameter("OtroDsctoCod1", " " + FormatearNumerosParaReporteODarDefault(txtOtrosDsctos2.Text));
        param[105] = new ReportParameter("OtroDsctoCod2", " " + FormatearNumerosParaReporteODarDefault(txtOtrosDsctos3.Text));
        param[106] = new ReportParameter("OtroDsctoCod3", " " + FormatearNumerosParaReporteODarDefault(txtOtrosDsctos4.Text));
        param[107] = new ReportParameter("DeudaTerceroDeudor", " " + FormatearNumerosParaReporteODarDefault(txtDeudasTer1.Text));
        param[108] = new ReportParameter("DeudaTerceroCod1", " " + FormatearNumerosParaReporteODarDefault(txtDeudasTer2.Text));
        param[109] = new ReportParameter("DeudaTerceroCod2", " " + FormatearNumerosParaReporteODarDefault(txtDeudasTer3.Text));
        param[110] = new ReportParameter("DeudaTerceroCod3", " " + FormatearNumerosParaReporteODarDefault(txtDeudasTer4.Text));
        param[111] = new ReportParameter("CapacidadDsctoDeudor", " " + FormatearNumerosParaReporteODarDefault(txtCapDesc1.Text));
        param[112] = new ReportParameter("CapacidadDsctoCod1", " " + FormatearNumerosParaReporteODarDefault(txtCapDesc2.Text));
        param[113] = new ReportParameter("CapacidadDsctoCod2", " " + FormatearNumerosParaReporteODarDefault(txtCapDesc3.Text));
        param[114] = new ReportParameter("CapacidadDsctoCod3", " " + FormatearNumerosParaReporteODarDefault(txtCapDesc4.Text));
        param[115] = new ReportParameter("CapacidadPagoDeudor", " " + FormatearNumerosParaReporteODarDefault(txtCapPago1.Text));
        param[116] = new ReportParameter("CapacidadPagoCod1", " " + FormatearNumerosParaReporteODarDefault(txtCapPago2.Text));
        param[117] = new ReportParameter("CapacidadPagoCod2", " " + FormatearNumerosParaReporteODarDefault(txtCapPago3.Text));
        param[118] = new ReportParameter("CapacidadPagoCod3", " " + FormatearNumerosParaReporteODarDefault(txtCapPago4.Text));
        param[119] = new ReportParameter("CupoEndeudamiento", " " + FormatearNumerosParaReporteODarDefault(lblCupoEndeuda.Text));
        param[120] = new ReportParameter("CupoDisponible", " " + FormatearNumerosParaReporteODarDefault(lblCupoDispo.Text));
        param[121] = new ReportParameter("DescubiertoCaja", " " + FormatearNumerosParaReporteODarDefault(lblDescuCaja.Text));
        param[122] = new ReportParameter("TotalObligaAportes", " " + FormatearNumerosParaReporteODarDefault(lblTotObliAportes.Text));

        General general = new General();
        //381: Generar Analisis de Credito, (0-completo  1-Resumido
        general = ConsultarParametroGeneral(381, (Usuario)Session["usuario"]);
        string valor = "false";
        if (general.valor == "1")
        {
            valor = "true";
        }
        param[123] = new ReportParameter("Ocultar", valor);
        return param;
    }


    private DataTable LlenarDataTableAnalisisPromedio()
    {
        DataTable dtAnalisis = new DataTable();
        dtAnalisis.Columns.Add("producto");
        dtAnalisis.Columns.Add("afiliacion");
        dtAnalisis.Columns.Add("saldo");
        dtAnalisis.Columns.Add("reciprocidad");
        dtAnalisis.Columns.Add("cupo_disponible");

        foreach (AnalisisPromedio entidad in listaAnalisisPromedio)
        {
            DataRow dtfila = dtAnalisis.NewRow();
            dtfila[0] = entidad.producto;
            dtfila[1] = entidad.fecha_apertura.HasValue ? entidad.fecha_apertura.Value.ToShortDateString() : " ";
            dtfila[2] = FormatearNumerosParaReporteODarDefault(entidad.saldo.ToString());
            dtfila[3] = entidad.reciprocidad.ToString();
            dtfila[4] = entidad.cupo_disponible.ToString();
            dtAnalisis.Rows.Add(dtfila);
        }

        return dtAnalisis;
    }


    private DataTable LlenarDataTableEstadoCuenta()
    {
        DataTable dtEstadoCuenta = new DataTable();
        dtEstadoCuenta.Columns.Add("fecha_aprobacion");
        dtEstadoCuenta.Columns.Add("linea_credito");
        dtEstadoCuenta.Columns.Add("numero_radicacion");
        dtEstadoCuenta.Columns.Add("monto_aprobado");
        dtEstadoCuenta.Columns.Add("saldo");
        dtEstadoCuenta.Columns.Add("valor_cuota");

        foreach (Credito entidad in listaCreditosActivos)
        {
            DataRow dtfila = dtEstadoCuenta.NewRow();
            dtfila[0] = entidad.fecha_desembolso_nullable.HasValue ? entidad.fecha_desembolso_nullable.Value.ToShortDateString() : " ";
            dtfila[1] = entidad.linea_credito;
            dtfila[2] = entidad.numero_radicacion;
            dtfila[3] = FormatearNumerosParaReporteODarDefault(entidad.monto_aprobado.ToString());
            dtfila[4] = FormatearNumerosParaReporteODarDefault(entidad.saldo_capital.ToString());
            dtfila[5] = FormatearNumerosParaReporteODarDefault(entidad.valor_cuota.ToString());
            dtEstadoCuenta.Rows.Add(dtfila);
        }

        return dtEstadoCuenta;
    }

    private DataTable LlenarDataTableHIstorial()
    {
        DataTable dtHistorial = new DataTable();
        dtHistorial.Columns.Add("Año");
        dtHistorial.Columns.Add("Ene");
        dtHistorial.Columns.Add("Feb");
        dtHistorial.Columns.Add("Mar");
        dtHistorial.Columns.Add("Abr");
        dtHistorial.Columns.Add("May");
        dtHistorial.Columns.Add("Jun");
        dtHistorial.Columns.Add("Jul");
        dtHistorial.Columns.Add("Ago");
        dtHistorial.Columns.Add("Sep");
        dtHistorial.Columns.Add("Oct");
        dtHistorial.Columns.Add("Nov");
        dtHistorial.Columns.Add("Dic");

        foreach (AnalisisPromedio entidad in listaHistorial)
        {
            DataRow dtfila = dtHistorial.NewRow();
            dtfila[0] = entidad.Año;
            dtfila[1] = entidad.Ene;
            dtfila[2] = entidad.Feb;
            dtfila[3] = entidad.Mar;
            dtfila[4] = entidad.Abr;
            dtfila[5] = entidad.May;
            dtfila[6] = entidad.Jun;
            dtfila[7] = entidad.Jul;
            dtfila[8] = entidad.Ago;
            dtfila[9] = entidad.Sep;
            dtfila[10] = entidad.Oct;
            dtfila[11] = entidad.Nov;
            dtfila[12] = entidad.Dic;
            dtHistorial.Rows.Add(dtfila);
        }

        return dtHistorial;

    }
    private string FormatearNumerosParaReporteODarDefault(string stringTextBox)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(stringTextBox))
            {
                return " ";
            }
            else if (stringTextBox.Trim().StartsWith("0"))
            {
                return "$ " + stringTextBox.Trim();
            }
            else if (stringTextBox.Contains("$"))
            {
                return stringTextBox.Trim();
            }
            else
            {
                return string.Format("$ {0:N}", Convert.ToInt64(stringTextBox.Trim().Replace(".", "").Replace(",00", "")));
            }
        }
        catch (Exception)
        {
            VerError("Pasaste una letra al metodo (FormatearNumerosParaReporteODarDefault) que solo formatea numeros");
            return " ";
        }
    }


    #endregion

    #region Validaciones
    public void Validacion()
    {
        //***********************************
        //************** VARIABLES **********
        //***********************************
        var respuesta = "";
        var error = "";
        var parametros = new[] { "605", "606", "607", "608" };
        var totalEgresos = Convert.ToInt64(!string.IsNullOrEmpty(TxtB1.Text) ? TxtB1.Text.Replace(".", "") : "0");
        var obligacionesFinPrincipal = Convert.ToInt64(!string.IsNullOrEmpty(TxtCuotasFinanPrincipal1.Text) ? TxtCuotasFinanPrincipal1.Text.Replace(".", "") : "0");
        var oblicacionesFinCodeudor = Convert.ToInt64(!string.IsNullOrEmpty(TxtCuotasFinanPrincipal2.Text) ? TxtCuotasFinanPrincipal2.Text.Replace(".", "") : "0");
        var ingresos = Convert.ToInt64(!string.IsNullOrEmpty(TxtIngresos1.Text) ? TxtIngresos1.Text.Replace(".", "") : "0");
        var ingresosBrutos = Convert.ToInt64(!string.IsNullOrEmpty(TxtIngresosBrutos1.Text) ? TxtIngresosBrutos1.Text.Replace(".", "") : "0");
        var cuotaSolicitada = Convert.ToInt32(!string.IsNullOrEmpty(TxtCuotaCredito1.Text) ? TxtCuotaCredito1.Text.Replace(".", "") : "0");
        var proteccionSalarial = Convert.ToInt32(!string.IsNullOrEmpty(txtProtSalarial1.Text) ? txtProtSalarial1.Text.Replace(".", "") : "0");
        var saludPension = Convert.ToInt32(!string.IsNullOrEmpty(TxtDeduccionesSocial1.Text) ? TxtDeduccionesSocial1.Text.Replace(".", "") : "0");
        //************************************* CONSULTAS ********************************
        RealizacionService.ReemplazarConsultaSQL("Select valor As Respuesta From general Where codigo = 10", ref respuesta, ref error, (Usuario)Session["Usuario"]);


        //************************************ CALCULOS ********************************
        //Verificar la capcidad de pago total
        //Formula Total de egresos - Obli.Fin(principal) - Obli.Fin(Codeudor) + Cuota Solicitada / El 50% de los ingresos Netos
        var pocentajeIngreso = lineasCreditoService.ConsultarParametrosLinea(Convert.ToString(Session["CodLineaCredito"]), parametros[0], (Usuario)Session["Usuario"]);

        var ingresoPorcentaje = (ingresos * pocentajeIngreso / 100);
        //Validar Resultado con el salario minimo
        if (ingresoPorcentaje < Convert.ToInt64(respuesta))
        {
            MensajeViabilidad.Text = @"Crédito no viable,El " + pocentajeIngreso + @"% de su sueldo no supera el salario minimo.";
            return;
        }
        var CapacidadDePago = (totalEgresos + cuotaSolicitada - proteccionSalarial - saludPension);
        var porcentajecapacidadPago = (CapacidadDePago * 100 / proteccionSalarial) - 100;
        porcentajecapacidadPago = porcentajecapacidadPago * -1;
        MensajeViabilidad.Text = porcentajecapacidadPago.ToString();
        //valida el porcentaje de capacidad de pago
        if (porcentajecapacidadPago <= Convert.ToInt32(lineasCreditoService.ConsultarParametrosLinea(Convert.ToString(Session["CodLineaCredito"]), parametros[1], (Usuario)Session["Usuario"])))
        {
            MensajeViabilidad.Text = @"Credito no viable, La capacidad de pago es menor a " + Convert.ToInt32(lineasCreditoService.ConsultarParametrosLinea(Convert.ToString(Session["CodLineaCredito"]), parametros[1], (Usuario)Session["Usuario"])) + @"% (Su porcentaje es " + porcentajecapacidadPago + @"%)";
            detalleValidacion.Text = @"Verificar Cuotas Extras (% Primas), plazo, monto, Recoger creditos";
            return;
        }

        //valida Nivel de endeudamiento
        //Formula Total Deudas + Centrales De riesgo / total de ingresos brutos

        var porcentajeEndeudamiento = totalEgresos + obligacionesFinPrincipal + oblicacionesFinCodeudor / ingresosBrutos;

        //Valida el porcentaje del nivel de endeudamiento

        if (Math.Round((double)porcentajeEndeudamiento, 0) < Convert.ToInt32(lineasCreditoService.ConsultarParametrosLinea(Convert.ToString(Session["CodLineaCredito"]), parametros[2], (Usuario)Session["Usuario"])))
        {
            MensajeViabilidad.Text = @"Credito no viable, El nivel de endeudamiento es menor a " + Convert.ToInt32(lineasCreditoService.ConsultarParametrosLinea(Convert.ToString(Session["CodLineaCredito"]), parametros[2], (Usuario)Session["Usuario"])) + @"% (su porcentaje es" + porcentajeEndeudamiento + @"%) ";
            detalleValidacion.Text = @"Verificar Cuotas Extras (% Primas), plazo, monto, Recoger creditos";
            RbtViable.Checked = false;
            return;
        }
        if ((Math.Round((double)porcentajeEndeudamiento, 0) < Convert.ToInt32(lineasCreditoService.ConsultarParametrosLinea(Convert.ToString(Session["CodLineaCredito"]), parametros[3], (Usuario)Session["Usuario"]))) || (Math.Round((double)porcentajeEndeudamiento, 0) == 40))
        {
            MensajeViabilidad.Text = @"Se necesita garantias para el credito (Codeudores/Afiancol) ";
            RbtViable.Checked = false;
            return;
        }
        if (Math.Round((double)porcentajeEndeudamiento, 0) > Convert.ToInt32(lineasCreditoService.ConsultarParametrosLinea(Convert.ToString(Session["CodLineaCredito"]), parametros[3], (Usuario)Session["Usuario"])))
        {
            MensajeViabilidad.Text = @"El credito es viable, cumple con las espectativas.";
        }
    }

    #endregion

}