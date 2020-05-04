using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;
using System.Web;
using System.Globalization;
using Cantidad_a_Letra;
public partial class Detalle : GlobalWeb
{
    StringHelper _stringHelper = new StringHelper();
    LiquidacionVacacionesEmpleadoService _liquidacionVacacionesServices = new LiquidacionVacacionesEmpleadoService();
    SeguridadSocialServices _SeguridadSocialService = new SeguridadSocialServices();


    IngresoPersonalService ingresoPersonalService = new IngresoPersonalService();

    long? _consecutivoEmpleado;
    long? _consecutivoLiquidacion;
    bool _esNuevoRegistro;
    readonly string _viewStateListaEmpleadoDetalle = "listaEmpleadoDetalle";
    readonly string _viewStateListaConceptosNominaLiquidados = "listaConceptosLiquidados";
    int tipoOpe = 140;
    DateTime FechaAct = DateTime.Now;

    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_liquidacionVacacionesServices.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoImprimir += btnImprimir_eventoImprimir;
            ctlMensaje.eventoClick += CtlMensaje_eventoClick;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionVacacionesServices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
            // Si aqui viene algo significa que voy a crear uno
            _consecutivoEmpleado = Session[_liquidacionVacacionesServices.CodigoPrograma + ".idEmpleado"] as long?;

            // Si aqui viene algo significa que voy a modificar uno
            _consecutivoLiquidacion = Session[_liquidacionVacacionesServices.CodigoPrograma + ".idLiquidacion"] as long?;

            _esNuevoRegistro = !_consecutivoLiquidacion.HasValue;

            if (!IsPostBack)
            {
                InicializarPagina();
            }
        //}
        //catch (Exception ex)
        //{
        //    BOexcepcion.Throw(_liquidacionVacacionesServices.CodigoPrograma, "Page_Load", ex);
        //}
    }

    void InicializarPagina()
    {
      
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto);
        ctlGiro.Inicializar();

        txtFechaInicio.Attributes.Add("readonly", "readonly");
        txtFechaTerminacion.Attributes.Add("readonly", "readonly");
        txtFechaPago.Attributes.Add("readonly", "readonly");

        txtFechaInicioPeriodo.Attributes.Add("readonly", "readonly");
        txtFechaTerminacionPeriodo.Attributes.Add("readonly", "readonly");

        EmpleadoService empleadoService = new EmpleadoService();
        List<NominaEmpleado> listaNominas = empleadoService.ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(_consecutivoEmpleado.Value, Usuario);

        ddlTipoNomina.DataSource = listaNominas;
        ddlTipoNomina.DataValueField = "consecutivo";
        ddlTipoNomina.DataTextField = "descripcion";
        ddlTipoNomina.DataBind();

        Site toolBar = (Site)Master;
        if (!_esNuevoRegistro)
        {
            LlenarLiquidacion();
            DeshabilitarControles();

            toolBar.MostrarGuardar(false);
            pnlLiquidacionHecha.Visible = true;
            btnLiquidar.Visible = false;
        }
        else
        {
            ConsultarDatosPersona();
            toolBar.MostrarImprimir(false);
            
        }
      

    }

    void LlenarLiquidacion()
    {

        EmpleadoService empleadoService = new EmpleadoService();
        List<NominaEmpleado> listaNominas = empleadoService.ListarNominasALasQuePerteneceUnEmpleado(_consecutivoEmpleado.Value, Usuario);

        ddlTipoNomina.DataSource = listaNominas;
        ddlTipoNomina.DataValueField = "consecutivo";
        ddlTipoNomina.DataTextField = "descripcion";
        ddlTipoNomina.DataBind();



        LiquidacionVacacionesEmpleado liquidacion = _liquidacionVacacionesServices.ConsultarLiquidacionVacacionesEmpleado(_consecutivoLiquidacion.Value, Usuario);

        if (!string.IsNullOrWhiteSpace(liquidacion.usuario_crea))
        {
            txtusuariocreacion.Text = liquidacion.usuario_crea.ToString();
        }
        txtCodigoLiquidacion.Text = liquidacion.consecutivo.ToString();
        txtIdentificacionn.Text = liquidacion.identificacion;

        if (!string.IsNullOrWhiteSpace(liquidacion.tipo_identificacion))
        {
            ddlTipoIdentificacion.SelectedValue = liquidacion.tipo_identificacion;
        }

        txtCodigoEmpleado.Text = liquidacion.codigoempleado.ToString();
        txtNombreCliente.Text = liquidacion.nombre_empleado;
        txtSalario.Text = Convert.ToString(liquidacion.salario);

        ddlTipoNomina.SelectedValue = liquidacion.codigonomina.ToString();
        ddlCentroCosto.SelectedValue = liquidacion.codigocentrocosto.ToString();

        PaneldatosLiquida.Visible = true;
        txtFechaInicio.Text = liquidacion.fechainicio.ToShortDateString();
        txtFechaTerminacion.Text = liquidacion.fechaterminacion.ToShortDateString();
        txtFechaRegreso.Text = liquidacion.fechafinvacaciones.ToShortDateString();
       
        txtFechaInicioPeriodo.Text = liquidacion.fechainicioperiodo.ToShortDateString();
        txtFechaTerminacionPeriodo.Text = liquidacion.fechaterminacionperiodo.ToShortDateString();

        txtFechaPago.Text = liquidacion.fechaPago.ToShortDateString();
       // txtDias.Text = liquidacion.diasliquidados.ToString();
        txtDiasDisfrutar.Text = liquidacion.diasdisfrutar.ToString();
        txtDiasPagados.Text = liquidacion.diaspagados.ToString();
        
        txtDiastotalpagados.Text = liquidacion.diasliquidados.ToString();
        txtDiaspendientes.Text = liquidacion.diaspendientes.ToString();

        txtSalarioAdicional.Text = liquidacion.salario_ad.ToString();
        txtSalario.Text = liquidacion.salario.ToString();


        IngresoPersonal parametro1 = ingresoPersonalService.ConsultarInformacionDiaslegales(Usuario);

        txtDias.Text = parametro1.diasvacaciones.ToString();
        txtvacacantic.Text = parametro1.pagavacacionesant.ToString();


        txtValorTotal.Text = _stringHelper.FormatearNumerosComoCurrency(liquidacion.valortotalpagar.ToString());

        List<LiquidacionVacacionesDetalleEmpleado> listaLiquidacionDetalleEmpleado = _liquidacionVacacionesServices.ListarLiquidacionVacacionesEmpleadoDeUnaLiquidacion(_consecutivoLiquidacion.Value, Usuario);
        listaLiquidacionDetalleEmpleado = AgregarSimbolosSegunTipoDeCalculo(listaLiquidacionDetalleEmpleado);

        gvVacaciones.DataSource = listaLiquidacionDetalleEmpleado;
        gvVacaciones.DataBind();



       
        ViewState[_viewStateListaEmpleadoDetalle] = listaLiquidacionDetalleEmpleado;
    }

    void ConsultarDatosPersona()
    {
        EmpleadoService empleadoService = new EmpleadoService();

        Empleados empleado = empleadoService.ConsultarInformacionPersonaEmpleado(_consecutivoEmpleado.Value, Usuario);

        txtIdentificacionn.Text = empleado.identificacion.ToString();
        ddlTipoIdentificacion.SelectedValue = empleado.cod_identificacion;
        txtNombreCliente.Text = empleado.nombre;
        txtCodigoEmpleado.Text = _consecutivoEmpleado.Value.ToString();
        txtCodigoPersona.Text = empleado.cod_persona.ToString();
        ddlCentroCosto.SelectedValue = empleado.codigocentrocosto.ToString();


        LiquidacionVacacionesEmpleado liquidacion = _liquidacionVacacionesServices.ConsultarLiquidacionVacacionesEmpleadoXCodigo(_consecutivoEmpleado.Value, Usuario);


        LiquidacionVacacionesEmpleado parametrovacaciones = _liquidacionVacacionesServices.ConsultarPagaVacacionesAnticipadas(Usuario);

        txtFechaInicioPeriodo.Text = liquidacion.fechainicioperiodo.ToShortDateString();
        txtFechaTerminacionPeriodo.Text = liquidacion.fechaterminacionperiodo.ToShortDateString();
        txtFechaPago.Text = Convert.ToString(DateTime.Now.ToShortDateString());
        DateTime fechaterminacion = Convert.ToDateTime(txtFechaTerminacionPeriodo.Text);

        if (fechaterminacion <= DateTime.Now)
        {
            PaneldatosLiquida.Visible = true;

        }

        if (txtFechaInicioPeriodo.Text == "1/01/0001" || txtFechaInicioPeriodo.Text == "01/01/0001")
        {

            txtFechaInicioPeriodo.Text = empleado.fechainicioperiodo.ToShortDateString();
            txtFechaTerminacionPeriodo.Text = empleado.fechaterminacionperiodo.ToShortDateString();

            DateTime fechaterminacion2 = Convert.ToDateTime(txtFechaTerminacionPeriodo.Text);
            if (DateTime.Now <= fechaterminacion2)
            {
                PaneldatosLiquida.Visible = false;
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
            }

            txtFechaInicioPeriodo.Enabled = true;
            txtFechaTerminacionPeriodo.Enabled = true;
            PaneldatosLiquida.Visible = true;
        }
        if (txtFechaInicioPeriodo.Text != "1/01/0001" || txtFechaInicioPeriodo.Text != "01/01/0001")
        {

            if (fechaterminacion <= DateTime.Now)
            {
                PaneldatosLiquida.Visible = true;
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(true);
            }
            if (fechaterminacion >= DateTime.Now && parametrovacaciones.vacacionesanticipadas==0)
            {
                PaneldatosLiquida.Visible = false;
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
            }
            if (fechaterminacion >= DateTime.Now && parametrovacaciones.vacacionesanticipadas == 1)
            {
                PaneldatosLiquida.Visible = true;
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(true);
            }
        }

        IngresoPersonal parametro = ingresoPersonalService.ConsultarInformacionDiaslegales(Usuario);

        if (parametro.diasvacaciones > 0)
        {
            String format = "dd/MM/yyyy";
            Int32 diaslegales = Convert.ToInt32(parametro.diasvacaciones);
            txtDias.Text = diaslegales.ToString();

            if(liquidacion.diaspendientes> 0)

            {
                 txtDias.Text = liquidacion.diaspendientes.ToString();

            }
            txtFechaInicio.Text = Convert.ToString(DateTime.Now);
            DateTime fechainicio = Convert.ToDateTime(txtFechaInicio.Text);
            String fechavalidar = fechainicio.ToShortDateString();

            //DateTime fechaInicio = DateTime.ParseExact(fechavalidar, format, CultureInfo.InvariantCulture);
            DateTime fechaInicio = ConvertirStringToDate(fechavalidar);
            txtFechaInicio.Text = fechavalidar;
            txtFechas_TextChanged(txtFechaInicio.Text, null);
        }
        DateTime fechaterminacionperiodo = Convert.ToDateTime(txtFechaTerminacionPeriodo.Text);


        if (fechaterminacionperiodo > DateTime.Now && parametrovacaciones.vacacionesanticipadas == 0)
        {
                PaneldatosLiquida.Visible = false;
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
        }
     

        if (fechaterminacionperiodo > DateTime.Now && parametrovacaciones.vacacionesanticipadas == 1)
        {
            PaneldatosLiquida.Visible = true;
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(true);
        }


        txtvacacantic.Text =Convert.ToString(parametrovacaciones.vacacionesanticipadas);


    }


    #endregion


    #region Eventos Botonera


    protected void btnLiquidar_Click(object sender, EventArgs e)
    {
        if (ValidarDatos())
        {
            // Si hay un error previo lo limpiamos y hacemos postback para ver el cambio
            if (!string.IsNullOrWhiteSpace(TextoLaberError))
            {
                VerError("");
                RegistrarPostBack();
            }
          
            GenerarLiquidacionVacacion();
        }
        else
        {
            // Si no paso las validaciones registrarmos postback para mostrarlas
            RegistrarPostBack();
        }
    }

    void btnImprimir_eventoImprimir(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarImprimir(false);

        mvDatos.SetActiveView(vReportes);
    }

    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        View actualView = mvDatos.GetActiveView();

        if (actualView != vReportes)
        {
            // Borramos las sesiones para no mezclar cosas luego
            Session.Remove(_liquidacionVacacionesServices.CodigoPrograma + ".idLiquidacion");
            Session.Remove(_liquidacionVacacionesServices.CodigoPrograma + ".idEmpleado");
        }

        if (actualView == vwDatos)
        {
            if (!_esNuevoRegistro)
            {
                Navegar(Pagina.Lista);
            }
            else
            {
                Navegar(Pagina.Nuevo);
            }
        }
        else if (actualView == vFinal)
        {
            Navegar(Pagina.Lista);
        }
        else if (actualView == vReportes)
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarImprimir(true);

            mvDatos.SetActiveView(vwDatos);
        }
    }

    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");

        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible)
        {
            rpta = ctlproceso.Inicializar(tipoOpe, FechaAct, (Usuario)Session["Usuario"]);
            if (rpta >= 1)
            {
                if (ValidarDatos())
                {
                    ctlMensaje.MostrarMensaje("Esta seguro que desea continuar?");
                }
                else

                {
                    VerError("");
                }
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                // Activar demás botones que se requieran
                panelGeneral.Visible = false;
                panelProceso.Visible = true;
                mvDatos.SetActiveView(vwDatos);


            }
            else
            {

                VerError("No se encontró parametrización contable por procesos para el tipo de operación 140 = Liquidación De Vacaciones Individuales");
                return;
            }
        }
    }

    void CtlMensaje_eventoClick(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
        Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        var cod_operacion = 0L;
        Usuario vUsuario = new Usuario();
        vUsuario = (Usuario)Session["Usuario"];

        try
        {
            // Recuperamos los datos de la liquidacion
            List<LiquidacionVacacionesDetalleEmpleado> listaDetalleEmplado = ViewState[_viewStateListaEmpleadoDetalle] as List<LiquidacionVacacionesDetalleEmpleado>;
            List<ConceptosOpcionesLiquidados> listaConceptosNominaLiquidados = ViewState[_viewStateListaConceptosNominaLiquidados] as List<ConceptosOpcionesLiquidados>;

            // Si es nulo significa que no fue clickeado el boton de generar liquidacion,
            // Por lo que generamos la liquidacion y recuperamos los datos
            if (listaDetalleEmplado == null || listaConceptosNominaLiquidados == null)
            {
                // Generamos la liquidacion
                GenerarLiquidacionVacacion();

                // Recuperamos los datos de la liquidacion
                listaDetalleEmplado = ViewState[_viewStateListaEmpleadoDetalle] as List<LiquidacionVacacionesDetalleEmpleado>;
                listaConceptosNominaLiquidados = ViewState[_viewStateListaConceptosNominaLiquidados] as List<ConceptosOpcionesLiquidados>;
            }

            LiquidacionVacacionesEmpleado liquidacion = ObtenerValores();
            liquidacion.valortotalpagar = CalcularValorTotalParaPagar(listaDetalleEmplado);
            if (liquidacion.valortotalpagar > 0)
            {
                Site toolbar = (Site)this.Master;
                toolbar.MostrarGuardar(true);
            }
            else
            {
                Site toolbar = (Site)this.Master;
                toolbar.MostrarGuardar(false);
            }
            if (liquidacion.valortotalpagar > 0)
            { 
                // CREAR OPERACION
                pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = tipoOpe;
            pOperacion.cod_caja = 0;
            pOperacion.cod_cajero = 0;
            pOperacion.observacion = "Liquidación Vcaciones";
            pOperacion.cod_proceso = null;
            pOperacion.fecha_oper = Convert.ToDateTime(txtFechaPago.Text);
            pOperacion.fecha_calc = DateTime.Now;
            pOperacion.cod_ofi = Usuario.cod_oficina;


            // Obtener los datos del giro
            Xpinn.FabricaCreditos.Entities.Giro vGiro = new Xpinn.FabricaCreditos.Entities.Giro();
            vGiro = ctlGiro.ObtenerEntidadGiro(Convert.ToInt64(liquidacion.codigoempleado), Convert.ToDateTime(txtFechaPago.Text), Convert.ToDecimal(liquidacion.valortotalpagar), vUsuario);
            vGiro.tipo_acto = 15;
            Int64 idGiro = 0;
            vGiro.fec_reg = Convert.ToDateTime(txtFechaPago.Text);
            vGiro.fec_giro = DateTime.Now;
            vGiro.valor = Convert.ToInt64(liquidacion.valortotalpagar);
            vGiro.cod_persona = Convert.ToInt64(txtCodigoPersona.Text.ToString());



            // Creamos la liquidacion
           
            liquidacion.codorigen = 4;
            liquidacion.diaspendientes = Convert.ToInt32(txtDiaspendientes.Text);

            liquidacion = _liquidacionVacacionesServices.CrearLiquidacionVacacionesEmpleado(liquidacion, listaDetalleEmplado, listaConceptosNominaLiquidados, Usuario, pOperacion, vGiro, ref idGiro);

            cod_operacion = pOperacion.cod_ope;


            // Si todo fue bien pasamos a la pantalla de exitoso
            if (liquidacion.consecutivo > 0)
            {
                mvDatos.SetActiveView(vFinal);

                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_liquidacionVacacionesServices.CodigoPrograma + ".idLiquidacion");
                Session.Remove(_liquidacionVacacionesServices.CodigoPrograma + ".idEmpleado");

                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarImprimir(true);

                btnLiquidar.Visible = false;


                var usu = (Usuario)Session["usuario"];


                // Generar el comprobante
                if (pOperacion.cod_ope != 0)
                {
                    ctlproceso.CargarVariables(pOperacion.cod_ope, tipoOpe, usu.codusuario, (Usuario)Session["usuario"]);
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }

            }
        }
    }
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }

    protected void btnImprimirDesprendibles_Click(object sender, EventArgs e)
    {
        LiquidacionVacacionesEmpleado empleado = ObtenerValores();
        List<LiquidacionVacacionesDetalleEmpleado> listaLiquidacionDetalle = ViewState[_viewStateListaEmpleadoDetalle] as List<LiquidacionVacacionesDetalleEmpleado>;

        IngresoPersonalService ingresoPersonalService = new IngresoPersonalService();
        IngresoPersonal ingresoPersonal = ingresoPersonalService.ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina(empleado.codigoempleado, empleado.codigonomina, Usuario);

        empleado.salario = Convert.ToDecimal(txtSalario.Text);
        empleado.desc_cargo = ingresoPersonal.desc_cargo;

        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("NombreEmpleado");
        dataTable.Columns.Add("Cedula");
        dataTable.Columns.Add("Cargo");
        dataTable.Columns.Add("Sueldo");
        dataTable.Columns.Add("TotalPagos");
        dataTable.Columns.Add("TotalDescuentos");
        dataTable.Columns.Add("NetoPagar");
        dataTable.Columns.Add("TipoConcepto");
        dataTable.Columns.Add("DescripcionPago");
        dataTable.Columns.Add("ValorPago");
        dataTable.Columns.Add("DescripcionDescuento");
        dataTable.Columns.Add("ValorDescuento");
        dataTable.Columns.Add("FechaInicio");
        dataTable.Columns.Add("FechaFin");
        dataTable.Columns.Add("ValorLetras");
        dataTable.Columns.Add("FechaInicioPeriodo");
        dataTable.Columns.Add("FechaTerminacionPeriodo");
        dataTable.Columns.Add("Diaspagar");
        dataTable.Columns.Add("FechaRegreso");
        dataTable.Columns.Add("PromedioAdicional");

        dataTable.Columns.Add("TotalBase");


        // Hallo los pagos de estas novedades
        List<LiquidacionVacacionesDetalleEmpleado> listaPagos = listaLiquidacionDetalle.Where(x => x.tipoCalculo == 1).ToList();

        // Hallo los descuentos de estas novedades
        List<LiquidacionVacacionesDetalleEmpleado> listaDescuentos = listaLiquidacionDetalle.Where(x => x.tipoCalculo == 2).ToList();

        listaPagos = listaPagos.OrderByDescending(x => x.codigoConcepto == 1).ToList();

        ConstruirReporteDesprendibleVacaciones(dataTable, empleado, listaPagos, listaDescuentos);
    }


    #endregion


    #region Eventos Varios (No Botonera)


    protected void txtFechas_TextChanged(object sender, EventArgs e)
    {
        String format = "dd/MM/yyyy";
        String fechavalidar;
        String fechafin;
        String fecharegreso;
        Int32 diasdisfrutar = 0;

        DateTime fechaterminacion;

        Int32 diaspagados =0;
        Int32 diaslegales = Convert.ToInt32(txtDias.Text);

        long numeroDias = 0;
        long numeroDiastotales = 0;
        long diaspendientes = 0;
        //DateTime fechaInicio = DateTime.ParseExact(txtFechaInicio.Text, format, CultureInfo.InvariantCulture);
        DateTime fechaInicio = ConvertirStringToDate(txtFechaInicio.Text);
      

        DateTime fechaFinal;
        DateTime fecharegreVacac;

        fechavalidar = Convert.ToString(txtFechaInicio.Text);
        IngresoPersonal ingresoPersonal = ingresoPersonalService.ConsultarInformacionFechaFinvacaciones(fechavalidar, diaslegales, _consecutivoEmpleado.Value, Usuario);
      

        txtFechaTerminacion.Text = (ingresoPersonal.fechafinvacaciones).ToString();
        fechaFinal = Convert.ToDateTime(txtFechaTerminacion.Text);
        fechafin = fechaFinal.ToShortDateString();
        txtFechaTerminacion.Text = fechafin;


      
        DateTime fechaIniciovacaciones =Convert.ToDateTime(txtFechaInicioPeriodo.Text);
        DateTime fechafinalvacaciones = Convert.ToDateTime(txtFechaTerminacionPeriodo.Text);


        LiquidacionVacacionesEmpleado diasnovedades = _liquidacionVacacionesServices.ConsultarDiasVacacionesNovedades(_consecutivoEmpleado.Value, fechaIniciovacaciones, fechafinalvacaciones, Usuario);
        txtDiasNovedades.Text = diasnovedades.cantidaddias.ToString();
        if(txtDiaspendientes.Text!="")
        {
            diaspendientes = Convert.ToInt32(txtDiaspendientes.Text);
        }
        else
        {
            diaspendientes = 0;
        }
        if (txtDiasDisfrutar.Text != "")
        {
            diasdisfrutar = Convert.ToInt32(txtDiasDisfrutar.Text);
        }
        else
        {
            diasdisfrutar = 0;
        }
        diaspagados = Convert.ToInt32(diaslegales - diasdisfrutar - diasnovedades.cantidaddias- diaspendientes);
        txtDiasPagados.Text = Convert.ToString(diaspagados);
        
        if (!string.IsNullOrWhiteSpace(txtFechaInicio.Text) && !string.IsNullOrWhiteSpace(txtFechaTerminacion.Text))
        {

            DateTimeHelper dateTimeHelper = new DateTimeHelper();
            numeroDias = dateTimeHelper.DiferenciaEntreDosFechasDias(fechaFinal, fechaInicio);
            if (numeroDias >= diaslegales)
            {
                txtDias.Text = Convert.ToString(diaslegales);

            }
            else
            {
                txtDias.Text = numeroDias.ToString();
            }
            if (!string.IsNullOrWhiteSpace(txtDiasDisfrutar.Text))
            {


                diasdisfrutar = Convert.ToInt32(txtDiasDisfrutar.Text);
                if (diasdisfrutar >= numeroDias)
                {
                    txtDiasDisfrutar.Text = diaslegales.ToString();
                }

                Int32 dias = Convert.ToInt32(txtDiasDisfrutar.Text);
                IngresoPersonalService ingresoPersonalService2 = new IngresoPersonalService();

                if (txtDiaspendientes.Text != "")
                    diaspendientes = Convert.ToInt32(txtDiaspendientes.Text);
                else
                    diaspendientes = 0;

                dias = Convert.ToInt32(diasdisfrutar + diasnovedades.cantidaddias +diaspagados);   

                
                fechavalidar = Convert.ToString(txtFechaInicio.Text);


                IngresoPersonal ingresoPersonal2 = ingresoPersonalService.ConsultarInformacionFechaFinvacaciones(fechavalidar, dias, _consecutivoEmpleado.Value, Usuario);
                txtFechaTerminacion.Text = (ingresoPersonal2.fechafinvacaciones).ToString();
                fechaFinal = Convert.ToDateTime(txtFechaTerminacion.Text);
                fechafin = fechaFinal.ToShortDateString();
                txtFechaTerminacion.Text = fechafin;

                IngresoPersonal ingresoPersonal3 = ingresoPersonalService.ConsultarInformacionFechaRegresovacaciones(fechavalidar, dias, _consecutivoEmpleado.Value, Usuario);
                txtFechaRegreso.Text = (ingresoPersonal3.fecharegresovacaciones).ToString();
                fecharegreVacac = Convert.ToDateTime(txtFechaRegreso.Text);
                fecharegreso = fecharegreVacac.ToShortDateString();
                txtFechaRegreso.Text = fecharegreso;






                if (diasdisfrutar > 0)
                {
                    diaspagados = Convert.ToInt32(diaslegales - diasdisfrutar - diasnovedades.cantidaddias- diaspendientes);
                    txtDiasPagados.Text = Convert.ToString(diaspagados);
                    if (diaspagados < 0)
                    {
                        btnLiquidar.Visible = false;
                    }
                    if (diaspagados >= 0)
                    {
                        btnLiquidar.Visible = true;


                        numeroDiastotales = dateTimeHelper.DiferenciaEntreDosFechasDias(fechaFinal, fechaInicio)+1;
                        txtDiastotalpagados.Text = Convert.ToString(numeroDiastotales);
                    }

                }
            }
        }
      
        // Limpiamos la liquidacion
        LimpiarLiquidacion();
    }

    protected void ddlTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
    {
        LimpiarLiquidacion();
    }


    #endregion


    #region Metodos Ayuda


    LiquidacionVacacionesEmpleado ObtenerValores()
    {
        LiquidacionVacacionesEmpleado liquidacion = new LiquidacionVacacionesEmpleado
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleado.Text),
            fechainicio = !string.IsNullOrWhiteSpace(txtFechaInicio.Text) ? Convert.ToDateTime(txtFechaInicio.Text) : default(DateTime),
            fechaterminacion = !string.IsNullOrWhiteSpace(txtFechaTerminacion.Text) ? Convert.ToDateTime(txtFechaTerminacion.Text) : default(DateTime),
            fechainicioperiodo= !string.IsNullOrWhiteSpace(txtFechaInicioPeriodo.Text) ? Convert.ToDateTime(txtFechaInicioPeriodo.Text) : default(DateTime),
            fechaterminacionperiodo = !string.IsNullOrWhiteSpace(txtFechaTerminacionPeriodo.Text) ? Convert.ToDateTime(txtFechaTerminacionPeriodo.Text) : default(DateTime),           
            consecutivo = _consecutivoLiquidacion.HasValue ? _consecutivoLiquidacion.Value : 0,
            codigonomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long),
            codigocentrocosto = !string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue) ? Convert.ToInt64(ddlCentroCosto.SelectedValue) : default(long),
            diasliquidados = !string.IsNullOrWhiteSpace(txtDiastotalpagados.Text) ? Convert.ToInt64(txtDiastotalpagados.Text) : 0,
            nombre_empleado = txtNombreCliente.Text,
            identificacion = txtIdentificacionn.Text,
            fechaPago = Convert.ToDateTime(txtFechaPago.Text),
            fechafinvacaciones = !string.IsNullOrWhiteSpace(txtFechaRegreso.Text) ? Convert.ToDateTime(txtFechaRegreso.Text) : default(DateTime),
            diasdisfrutar = !string.IsNullOrWhiteSpace(txtDiasDisfrutar.Text) ? Convert.ToInt64(txtDiasDisfrutar.Text) : 0,
            diaspagados = !string.IsNullOrWhiteSpace(txtDiasPagados.Text) ? Convert.ToInt64(txtDiasPagados.Text) : 0

        };

        return liquidacion;
    }

    bool ValidarDatos()
    {
        long pagavacacionesantic = Convert.ToInt16(txtvacacantic.Text);


        Site toolBar = (Site)Master;
        if (string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) || string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue) || string.IsNullOrWhiteSpace(txtDias.Text)
            || string.IsNullOrWhiteSpace(txtFechaInicio.Text) || string.IsNullOrWhiteSpace(txtFechaTerminacion.Text) || string.IsNullOrWhiteSpace(txtCodigoEmpleado.Text)
            || string.IsNullOrWhiteSpace(txtFechaPago.Text)
            || string.IsNullOrWhiteSpace(txtDiasDisfrutar.Text)
            || string.IsNullOrWhiteSpace(txtFechaRegreso.Text))
        {
            VerError("Faltan datos por llenar!.");
            return false;
        }
        DateTime fechaterminacionvacaciones = Convert.ToDateTime(txtFechaTerminacionPeriodo.Text);
        DateTime fechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
        DateTime fechaFinal = Convert.ToDateTime(txtFechaTerminacion.Text);
        if (fechaInicio > fechaFinal)
        {
            toolBar.MostrarGuardar(false);
            VerError("La fecha de inicio no puede ser mayor a la fecha de terminacion!.");
            return false;

        }

        long codigoNomina = Convert.ToInt64(ddlTipoNomina.SelectedValue);
        long codigoEmpleado = Convert.ToInt64(txtCodigoEmpleado.Text);

        IngresoPersonalService ingresoPersonalService = new IngresoPersonalService();
        DateTime? fechaIngreso = ingresoPersonalService.ConsultarFechaIngresoSegunNominaYEmpleado(codigoNomina, codigoEmpleado, Usuario);
        if (!fechaIngreso.HasValue)
        {
            toolBar.MostrarGuardar(false);
            VerError("La fecha de ingreso de este empleado y para esta nomina es invalida!.");
            return false;


        }
        else if (fechaIngreso.Value > fechaInicio)
        {
            toolBar.MostrarGuardar(false);
            VerError("La fecha de inicio de las vacaciones no puede ser inferior a la fecha de ingreso de este empleado!.");
            return false;

        }
        
      
       

        DateTime fechaInicio_periodo = Convert.ToDateTime(txtFechaInicioPeriodo.Text);
        DateTime fechaFinal_periodo = Convert.ToDateTime(txtFechaTerminacionPeriodo.Text);

        bool yaExisteVacacionesParaEstaFecha = _liquidacionVacacionesServices.VerificarSiExisteVacacionesParaEstasFechas(codigoEmpleado, fechaInicio_periodo, fechaFinal_periodo, Usuario);
        if (yaExisteVacacionesParaEstaFecha)
        {
            toolBar.MostrarGuardar(false);
            VerError("Ya existe vacaciones liquidadas para este empleado y estas fechas!.");
            return false;
            
           
        }

        return true;
    }

    void DeshabilitarControles()
    {
        ddlCentroCosto.Enabled = false;
        ddlTipoNomina.Enabled = false;
        txtDias.Enabled = false;
        CalendarExtender2.Enabled = false;
        CalendarExtender1.Enabled = false;
        CalendarExtender7.Enabled = false;
        CalendarExtenderRegreso.Enabled = false;
        txtDiasDisfrutar.Enabled = false;
    }

    void LimpiarLiquidacion()
    {
        gvVacaciones.DataSource = null;
        gvVacaciones.DataBind();
        txtValorTotal.Text = string.Empty;
        pnlLiquidacionHecha.Visible = false;
        ViewState[_viewStateListaConceptosNominaLiquidados] = null;
        ViewState[_viewStateListaEmpleadoDetalle] = null;
    }

    void GenerarLiquidacionVacacion()
    {
        pnlLiquidacionHecha.Visible = true;
        LiquidacionVacacionesEmpleado liquidacion = new LiquidacionVacacionesEmpleado
        {
            codigoempleado = _consecutivoEmpleado.Value,
            codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
            fechainicio = Convert.ToDateTime(txtFechaInicio.Text),
            fechaterminacion = Convert.ToDateTime(txtFechaTerminacion.Text),
            fechainicioperiodo = Convert.ToDateTime(txtFechaInicioPeriodo.Text),
            fechaterminacionperiodo = Convert.ToDateTime(txtFechaTerminacionPeriodo.Text),
            fechafinvacaciones = Convert.ToDateTime(txtFechaRegreso.Text),
            diasdisfrutar = Convert.ToInt64(txtDiasDisfrutar.Text)
        };

        Tuple<List<LiquidacionVacacionesDetalleEmpleado>, List<ConceptosOpcionesLiquidados>> tuple = _liquidacionVacacionesServices.GenerarLiquidacionVacacionesParaUnEmpleado(liquidacion, Usuario);
        List<LiquidacionVacacionesDetalleEmpleado> listaDetalleEmpleado = tuple.Item1;
        List<ConceptosOpcionesLiquidados> listaConceptosLiquidados = tuple.Item2;


        listaDetalleEmpleado = AgregarSimbolosSegunTipoDeCalculo(listaDetalleEmpleado);

        gvVacaciones.DataSource = tuple.Item1;
        gvVacaciones.DataBind();


        decimal valorTotalParaPagar = CalcularValorTotalParaPagar(listaDetalleEmpleado);
        txtValorTotal.Text = _stringHelper.FormatearNumerosComoCurrency(valorTotalParaPagar);



        ViewState[_viewStateListaEmpleadoDetalle] = listaDetalleEmpleado;
        ViewState[_viewStateListaConceptosNominaLiquidados] = listaConceptosLiquidados;
        if (listaDetalleEmpleado.Count == 0)
        {
            Site toolbar = (Site)this.Master;

            txtValorTotal.Visible = false;
            ctlGiro.Visible = false;
            lblMensajenoliquida.Text = "Este empleado tiene un contrato que no genera liquidación de vacaciones";
            toolbar.MostrarGuardar(false);
        }
        if (valorTotalParaPagar > 0)
        {
            Site toolbar = (Site)this.Master;
            toolbar.MostrarGuardar(true);
        }
        else
        {
            Site toolbar = (Site)this.Master;
            toolbar.MostrarGuardar(false);
        }

        
    }

    decimal CalcularValorTotalParaPagar(List<LiquidacionVacacionesDetalleEmpleado> listaDetalleEmpleado)
    {
        decimal valorTotalParaPagar = 0;
        decimal valorPositivo = listaDetalleEmpleado.Where(x => x.tipoCalculo == 1).Sum(x => x.valor);
        decimal valorPositivo1 = listaDetalleEmpleado.Where(x => x.tipoCalculo != 2).Sum(x => x.valor);
        decimal valorNegativo = listaDetalleEmpleado.Where(x => x.tipoCalculo == 2).Sum(x => x.valor);
         valorTotalParaPagar = valorPositivo  - valorNegativo;

        return valorTotalParaPagar;
    }

    List<LiquidacionVacacionesDetalleEmpleado> AgregarSimbolosSegunTipoDeCalculo(List<LiquidacionVacacionesDetalleEmpleado> listaLiquidacionDetalleEmpleado)
    {
        foreach (LiquidacionVacacionesDetalleEmpleado detalle in listaLiquidacionDetalleEmpleado)
        {
            if (detalle.tipoCalculo == 1) // Pago
            {
                detalle.desc_concepto += " (+) ";
            }
            else if (detalle.tipoCalculo == 2) // Descuento
            {
                detalle.desc_concepto += " (-) ";
            }
           
                if (detalle.desc_concepto == "LIQUIDACION VACACIONES (+) " || detalle.desc_concepto == "VACACIONES (+)" || detalle.desc_concepto == "VACACIONES  (+) ")
                {
                    detalle.desc_concepto += " (Liquidación vacaciones +) ";
                    detalle.codigoConcepto = 43;
                }
            
        }

        return listaLiquidacionDetalleEmpleado;
    }


    #endregion


    #region Metodos Reporte


    void ConstruirReporteDesprendibleVacaciones(DataTable dataTable, LiquidacionVacacionesEmpleado empleado, List<LiquidacionVacacionesDetalleEmpleado> listaPagos, List<LiquidacionVacacionesDetalleEmpleado> listaDescuentos)
    {
        Decimal valorapagar = 0;
        string Valor = "";
        string pTexto = "";
        // Hallo el valor total de esos pagos
        decimal totalValorPago = listaPagos.Sum(x => x.valor);
        // Hallo el valor total de esos descuentos
        decimal totalValorDescuentos = listaDescuentos.Sum(x => x.valor);

        // Hallo el numero de pagos
        int numeroPagos = listaPagos.Count();
        // Hallo el numero de descuentos
        int numeroDescuentos = listaDescuentos.Count();

        // Este es el indice maximo que se va a recorrer 
        // (Esto quiere decir que estamos buscando quien es mayor, los pagos o los descuentos, esto con el fin de usar ese indice en un for para ordenar los conceptos para mostrarlo en el reporte)
        int indiceParaRecorrer = 0;
        if (numeroPagos >= numeroDescuentos)
        {
            indiceParaRecorrer = numeroPagos;
        }
        else
        {
            indiceParaRecorrer = numeroDescuentos;
        }

        // Usamos el indice mayor calculado 
        for (int i = 0; i < indiceParaRecorrer; i++)
        {
            // Sacamos el pago de este indice
            LiquidacionVacacionesDetalleEmpleado pago = listaPagos.ElementAtOrDefault(i);
            // Sacamos el descuento de este indice
            LiquidacionVacacionesDetalleEmpleado descuentos = listaDescuentos.ElementAtOrDefault(i);

            // Creamos el row default para este empleado (Llenamos el row con la informacion que siempre es la misma, la usada para hacer el group)
            DataRow row = ConstruirDataRowDefaultDesprendibleDeVacacionesParaUnEmpleado(dataTable, empleado, totalValorPago, totalValorDescuentos);

            row[7] = i; // Solo es usado como muletilla para el grouping en el .rdlc

            // Si efectivamente sacamos un pago en este indice, lo lleno
            if (pago != null)
            {
                row[8] = pago.desc_concepto;
                row[9] = _stringHelper.FormatearNumerosComoCurrency(pago.valor);
            }
            else
            {
                row[8] = string.Empty;
                row[9] = string.Empty;
            }

            // Si efectivamente sacamos un descuento en este indice, lo lleno
            if (descuentos != null)
            {
                row[10] = descuentos.desc_concepto;
                row[11] = _stringHelper.FormatearNumerosComoCurrency(descuentos.valor);
            }
            else
            {
                row[10] = string.Empty;
                row[11] = string.Empty;
            }

            //Valor en letras
            valorapagar = Convert.ToDecimal(totalValorPago - totalValorDescuentos);

            Valor = Convert.ToString(valorapagar);

            //_stringHelper.FormatearNumerosComoCurrency(totalValorPago - totalValorDescuentos);
            Cardinalidad objCardinalidad = new Cardinalidad();
            string cardinal = " ";
            if (Valor != "0")
            {
                cardinal = objCardinalidad.enletras(Valor.Replace(".", ""));
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
                pTexto = cardinal;

            }
            row[14] = pTexto;


            // Añadimos el row
            dataTable.Rows.Add(row);
        }

        Xpinn.Nomina.Entities.SeguridadSocial lstConsultar = new Xpinn.Nomina.Entities.SeguridadSocial();
        lstConsultar = _SeguridadSocialService.ConsultarSeguridadSocial((Usuario)Session["usuario"]);
        String aprobador = lstConsultar.aprobador.ToString();
        String revisor = lstConsultar.revisor.ToString();
       
        ReportParameter[] param = new ReportParameter[]
        {
                new ReportParameter("NombreEmpresa", Usuario.empresa),
                new ReportParameter("NitEmpresa", Usuario.nitempresa),
                new ReportParameter("CentroDeCosto", ddlCentroCosto.SelectedItem.Text),
                new ReportParameter("Nomina", ddlTipoNomina.SelectedItem.Text),
                new ReportParameter("FechaPago", empleado.fechaPago.ToShortDateString()),
                new ReportParameter("ImagenReport", ImagenReporte()),
                new ReportParameter("pElaborado", HttpUtility.HtmlDecode(vacios(txtusuariocreacion.Text))),
                new ReportParameter("pAprobado", HttpUtility.HtmlDecode(vacios(aprobador)))
        };

        rvReporteDesprendible.LocalReport.EnableExternalImages = true;
        rvReporteDesprendible.LocalReport.ReportPath = @"Page\Nomina\LiquidacionVacacionesIndividuales\DesprendibleVacaciones.rdlc";
        rvReporteDesprendible.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", dataTable);
        rvReporteDesprendible.LocalReport.DataSources.Clear();
        rvReporteDesprendible.LocalReport.DataSources.Add(rds);
        rvReporteDesprendible.LocalReport.Refresh();

        pnlReporte.Visible = true;
        rvReporteDesprendible.Visible = true;
    }

    DataRow ConstruirDataRowDefaultDesprendibleDeVacacionesParaUnEmpleado(DataTable dataTable, LiquidacionVacacionesEmpleado empleado, decimal totalValorPago, decimal totalValorDescuentos)
    {
        decimal totalsalario;
        totalsalario = Convert.ToDecimal(txtSalario.Text) ;
        decimal totalsalarioad;
        totalsalarioad = Convert.ToDecimal(txtSalarioAdicional.Text);
        decimal basesalario;
         basesalario = totalsalario + totalsalarioad;
        DataRow row = dataTable.NewRow();
        row[0] = empleado.nombre_empleado;
        row[1] = empleado.identificacion;
        row[2] = empleado.desc_cargo;
        row[3] = _stringHelper.FormatearNumerosComoCurrency(txtSalario.Text);
        row[4] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago);
        row[5] = _stringHelper.FormatearNumerosComoCurrency(totalValorDescuentos);
        row[6] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago - totalValorDescuentos);
        row[12] = empleado.fechainicio.ToShortDateString();
        row[13] = empleado.fechaterminacion.ToShortDateString();
        row[15] = empleado.fechainicioperiodo.ToShortDateString();
        row[16] = empleado.fechaterminacionperiodo.ToShortDateString();
        row[17] = empleado.diasliquidados.ToString();
        row[18] = empleado.fechafinvacaciones.ToShortDateString();
        row[19] = _stringHelper.FormatearNumerosComoCurrency(txtSalarioAdicional.Text);
        row[20] = _stringHelper.FormatearNumerosComoCurrency(basesalario);

        return row;
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
    #endregion



    protected void txtDiaspendientes_TextChanged(object sender, EventArgs e)
    {
        Int32 diaspagados = 0;
        long diaspendientes = 0;
        Int32 diaslegales = Convert.ToInt32(txtDias.Text);
        Int32 diasdisfrutar = Convert.ToInt32(txtDiasDisfrutar.Text);
        Int32 diasnovedades = Convert.ToInt32(txtDiasNovedades.Text);
        if (txtDiaspendientes.Text != "")
            diaspendientes = Convert.ToInt32(txtDiaspendientes.Text);
        else
            diaspendientes = 0;
        if (txtDiaspendientes.Text != "0")
        {




            txtDiaspendientes.Text = Convert.ToString(diaspendientes);
            diaspendientes = Convert.ToInt32(txtDiaspendientes.Text);
            diaspagados = Convert.ToInt32(diaslegales - diasdisfrutar - diasnovedades - diaspendientes);
            txtDiasPagados.Text = Convert.ToString(diaspagados);

        }
        if (txtDiaspendientes.Text == "0")
        {
            diaspagados = Convert.ToInt32(diaslegales - diasdisfrutar - diasnovedades);
            txtDiasPagados.Text = Convert.ToString(diaspagados);

        }
        txtFechas_TextChanged(txtDiasDisfrutar.Text, null);
    }

    protected void txtDiasPagados_TextChanged(object sender, EventArgs e)
    {
        txtDiaspendientes_TextChanged(txtDiaspendientes, null);
    }
}