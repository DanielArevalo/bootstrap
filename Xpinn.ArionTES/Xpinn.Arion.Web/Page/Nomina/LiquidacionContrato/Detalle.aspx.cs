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
using Cantidad_a_Letra;
public partial class Detalle : GlobalWeb
{
    StringHelper _stringHelper = new StringHelper();
    LiquidacionContratoService _liquidacionContratoService = new LiquidacionContratoService();
    long? _consecutivoEmpleado;
    long? _consecutivoLiquidacion;
    bool _esNuevoRegistro;
    int tipoOpe = 139;
    DateTime FechaAct = DateTime.Now;
    LiquidacionNominaService _liquidacionServices = new LiquidacionNominaService();
    readonly string _viewStateListaEmpleadoDetalle = "listaEmpleadoDetalle";

    readonly string _viewStateLiquidacion = "Liquidacion"; // Usado guardar la lista de empleados liquidados
    readonly string _viewStateLiquidacionDetalle = "LiquidacionDetalle"; // Usado para guardar la lista de los conceptos de nomina de todos los empleados
    readonly string _viewStateLiquidacionNovedades = "LiquidacionNovedades"; // Usado para guardar la lista de las novedades agregadas manualmente de todos los empleados
    readonly string _viewStateControlSeGeneroLiquidacion = "SeGeneroLiquidacion"; // Usado para el control de que se genero la liquidacion definitiva antes de guardar o no
    readonly string _viewStateConceptosLiquidadosOpciones = "ConceptosLiquidadosOpciones"; // Usado para el control de los conceptos de las opciones que fueron liquidados



    #region  Eventos Carga Inicial


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_liquidacionContratoService.CodigoPrograma, "D");

            Site toolBar = (Site)Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoImprimir += btnImprimir_eventoImprimir;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            ctlMensaje.eventoClick += CtlMensaje_eventoClick;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionContratoService.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Si aqui viene algo significa que voy a crear uno
            _consecutivoEmpleado = Session[_liquidacionContratoService.CodigoPrograma + ".idEmpleado"] as long?;

            // Si aqui viene algo significa que voy a modificar uno
            _consecutivoLiquidacion = Session[_liquidacionContratoService.CodigoPrograma + ".idLiquidacion"] as long?;

            _esNuevoRegistro = !_consecutivoLiquidacion.HasValue;

            if (!IsPostBack)
            {
                CargarListas();
                InicializarPagina();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionContratoService.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void CargarListas()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            ctlGiro.Inicializar();
            // LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlTipoNomina, ddlNominaModal);
          
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionContratoService.GetType().Name + "L", "CargarListas", ex);
        }
    }
    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto);
        LlenarListasDesplegables(TipoLista.Contratacion, ddlContrato);
        LlenarListasDesplegables(TipoLista.TipoRetiroContrato, ddlMotivoRetiro);


       LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto, ddlCentroCostoModal);
        LlenarListasDesplegables(TipoLista.ConceptoNomina, ddlConceptoModal);


        txtFechaIngreso.Attributes.Add("readonly", "readonly");
        txtFechaRetiro.Attributes.Add("readonly", "readonly");
        txtFechaRetiro.Text = DateTime.Now.ToShortDateString();

        Site toolBar = (Site)Master;
        if (!_esNuevoRegistro)
        {
            LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlTipoNomina);
            LlenarLiquidacion();
            DeshabilitarControles();

            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);

            pnlLiquidacion.Visible = true;
            pnlLiquidacionHecha.Visible = true;
            ddlCentroCosto.Visible = true;
            labelCentrodeCosto.Visible = true;

            btnLiquidar.Visible = false;
        }
        else
        {
            EmpleadoService empleadoService = new EmpleadoService();
            List<NominaEmpleado> listaNominas = empleadoService.ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(_consecutivoEmpleado.Value, Usuario);

            ddlTipoNomina.DataSource = listaNominas;
            ddlTipoNomina.DataValueField = "consecutivo";
            ddlTipoNomina.DataTextField = "descripcion";
            ddlTipoNomina.DataBind();

            ConsultarDatosPersona();
            //toolBar.MostrarImprimir(false);
        }

       
    }

    void LlenarLiquidacion()
    {
        LiquidacionContrato liquidacion = _liquidacionContratoService.ConsultarLiquidacionContrato(_consecutivoLiquidacion.Value, Usuario);

        txtCodigoLiquidacion.Text = liquidacion.consecutivo.ToString();
        txtIdentificacionn.Text = liquidacion.identificacion_empleado;

        if (!string.IsNullOrWhiteSpace(liquidacion.tipo_identificacion))
        {
            ddlTipoIdentificacion.SelectedValue = liquidacion.tipo_identificacion;
        }

        txtCodigoIngresoPersonal.Text = liquidacion.codigoingresopersonal.ToString();
        txtCodigoEmpleado.Text = liquidacion.codigoempleado.ToString();
        txtNombreCliente.Text = liquidacion.nombre_empleado;

     
        ddlTipoNomina.SelectedValue = liquidacion.codigonomina.ToString();
       
        ddlCentroCosto.SelectedValue = liquidacion.codigocentrocosto.ToString();
        
        ddlContrato.SelectedValue = liquidacion.codigoTipoContrato.ToString();
        ddlMotivoRetiro.SelectedValue = liquidacion.codigotiporetirocontrato.ToString();

        txtFechaIngreso.Text = liquidacion.fechaingreso.ToShortDateString();
        txtFechaRetiro.Text = liquidacion.fecharetiro.ToShortDateString();
        if (liquidacion.codigonomina==0)
        {
            lblError.Visible= true;
            lblError.Text = "Empleado se encuentra retirado";
        }

        txtValorTotal.Text = _stringHelper.FormatearNumerosComoCurrency(liquidacion.valortotalpagar.ToString());
        GuardarLiquidacionContratoEnHiddens(liquidacion);

        List<LiquidacionContratoDetalle> listaLiquidacionDetalleEmpleado = _liquidacionContratoService.ListarLiquidacionContratoDetalle(_consecutivoLiquidacion.Value, Usuario);
        listaLiquidacionDetalleEmpleado = BuildearListaParaMostrarEnGrilla(listaLiquidacionDetalleEmpleado);

        gvLiquidacion.DataSource = listaLiquidacionDetalleEmpleado;
        gvLiquidacion.DataBind();

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

        
    }


    #endregion


    #region Eventos Botonera

    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        ddlTipoNomina.SelectedIndex = 0;
        ddlTipoNomina_SelectedIndexChanged(ddlTipoNomina, EventArgs.Empty);
    }

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

            GenerarLiquidacionContrato();
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
            Session.Remove(_liquidacionContratoService.CodigoPrograma + ".idLiquidacion");
            Session.Remove(_liquidacionContratoService.CodigoPrograma + ".idEmpleado");
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

                VerError("No se encontró parametrización contable por procesos para el tipo de operación 139 = Liquidación Definitiva Contrato");
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
            List<LiquidacionContratoDetalle> listaDetalleEmplado = ViewState[_viewStateListaEmpleadoDetalle] as List<LiquidacionContratoDetalle>;

            String conceptosprestacionales = "55,56,57,58,59,60";

            for (int i = 0; i < conceptosprestacionales.Split(',').Length; i++)
            {
                LiquidacionContratoDetalle detalle = new LiquidacionContratoDetalle();

                detalle.codigoconceptonomina = Convert.ToInt32(conceptosprestacionales.Split(',')[i]);
                listaDetalleEmplado.Add(detalle);
            }


            // Si es nulo significa que no fue clickeado el boton de generar liquidacion,
            // Por lo que generamos la liquidacion y recuperamos los datos
            if (listaDetalleEmplado == null)
            {
                // Generamos la liquidacion
                GenerarLiquidacionContrato();
               
               
                // Recuperamos los datos de la liquidacion
              
                 listaDetalleEmplado = ViewState[_viewStateListaEmpleadoDetalle] as List<LiquidacionContratoDetalle>;
            }

            // CREAR OPERACION
            pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = tipoOpe;
            pOperacion.cod_caja = 0;
            pOperacion.cod_cajero = 0;
            pOperacion.observacion = "Liquidación Contrato Definitiva";
            pOperacion.cod_proceso = null;
            pOperacion.fecha_oper = DateTime.Now;
            pOperacion.fecha_calc = DateTime.Now;
            pOperacion.cod_ofi = Usuario.cod_oficina;


            LiquidacionContrato liquidacion = ObtenerValores();
            liquidacion.valortotalpagar = CalcularValorTotalParaPagar(listaDetalleEmplado);


            // Obtener los datos del giro
            Xpinn.FabricaCreditos.Entities.Giro vGiro = new Xpinn.FabricaCreditos.Entities.Giro();
            vGiro = ctlGiro.ObtenerEntidadGiro(Convert.ToInt64(liquidacion.codigoempleado), Convert.ToDateTime(txtFechaRetiro.Text), Convert.ToDecimal(liquidacion.valortotalpagar), vUsuario);
            vGiro.tipo_acto = 13;
            Int64 idGiro = 0;
            vGiro.fec_reg = Convert.ToDateTime(txtFechaRetiro.Text);
            vGiro.fec_giro = DateTime.Now;
            vGiro.valor = Convert.ToInt64(liquidacion.valortotalpagar);
            vGiro.cod_persona = Convert.ToInt64(txtCodigoPersona.Text.ToString());


            // Creamos la liquidacion
            liquidacion.codorigen = 3;
            liquidacion.codigocentrocosto = Convert.ToInt32(ddlCentroCosto.SelectedValue);
            liquidacion.fecharetiro = Convert.ToDateTime(txtFechaRetiro.Text);
            liquidacion = _liquidacionContratoService.CrearLiquidacionContrato(liquidacion, listaDetalleEmplado, Usuario, pOperacion, vGiro, ref idGiro);
            cod_operacion = pOperacion.cod_ope;

            // Si todo fue bien pasamos a la pantalla de exitoso

            if (liquidacion.consecutivo > 0)
            {
                mvDatos.SetActiveView(vFinal);


                // Borramos las sesiones para no mezclar cosas luego
                Session.Remove(_liquidacionContratoService.CodigoPrograma + ".idLiquidacion");
                Session.Remove(_liquidacionContratoService.CodigoPrograma + ".idEmpleado");

                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                toolBar.MostrarImprimir(true);
                toolBar.MostrarLimpiar(false);

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
        catch (Exception ex)
        {
            VerError("Error al guardar el registro, " + ex.Message);
        }
    }

    protected void btnImprimirDesprendibles_Click(object sender, EventArgs e)
    {
        LiquidacionContrato empleado = ObtenerValores();
        List<LiquidacionContratoDetalle> listaLiquidacionDetalle = ViewState[_viewStateListaEmpleadoDetalle] as List<LiquidacionContratoDetalle>;

        IngresoPersonalService ingresoPersonalService = new IngresoPersonalService();
        IngresoPersonal ingresoPersonal = ingresoPersonalService.ConsultarInformacionDeContratoPorCodigoIngreso(empleado.codigoingresopersonal, Usuario);

        empleado.salario = ingresoPersonal.salario.Value;
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

        dataTable.Columns.Add("TipoContrato");
        dataTable.Columns.Add("NumeroLiquidacion");
        dataTable.Columns.Add("DiasLaborados");
        dataTable.Columns.Add("MotivoRetiro");
        dataTable.Columns.Add("PrimaCalculo");
        dataTable.Columns.Add("PrimaDias");
        dataTable.Columns.Add("VacacionesCalculo");
        dataTable.Columns.Add("VacacionesDias");
        dataTable.Columns.Add("CesantiasCalculo");
        dataTable.Columns.Add("CesantiasDias");
        dataTable.Columns.Add("FechaIngreso");
        dataTable.Columns.Add("Valorletras");


        // Hallo los pagos de estas novedades
        List<LiquidacionContratoDetalle> listaPagos = listaLiquidacionDetalle.Where(x => x.tipoCalculo == 1).ToList();

        // Hallo los descuentos de estas novedades
        List<LiquidacionContratoDetalle> listaDescuentos = listaLiquidacionDetalle.Where(x => x.tipoCalculo == 2).ToList();

        listaPagos = listaPagos.OrderByDescending(x => x.codigoconceptonomina == 1).ToList();

        ConstruirReporteDesprendibleVacaciones(dataTable, empleado, listaPagos, listaDescuentos);
    }

    protected void btnCloseReg1_Click(object sender, EventArgs e)
    {
        LimpiarFormularioAgregarNovedades();
        mpeAgregarNovedades.Hide();
    }

    protected void btnAgregarNovedades_Click(object sender, EventArgs e)
    {
        if (txtValorTotal.Text != null)
        {
            txtCodigoEmpleadoModal.Text = txtCodigoEmpleado.Text;
            txtIdentificacionModal.Text = txtIdentificacionn.Text;
            txtNombresModal.Text = txtNombreCliente.Text;
            ddlNominaModal.SelectedValue = ddlTipoNomina.SelectedValue;


            mpeAgregarNovedades.Show();
        }

    }

    protected void btnAgregarNovedadModal_Click(object sender, EventArgs e)
    {
        if (ValidarModalNuevaNovedad())
        {
            List<LiquidacionNominaNoveEmpleado> liquidacionNovedades = ViewState[_viewStateLiquidacionNovedades] as List<LiquidacionNominaNoveEmpleado>;

            LiquidacionNominaNoveEmpleado novedad = new LiquidacionNominaNoveEmpleado
            {
                codigocentrocosto = Convert.ToInt64(ddlCentroCostoModal.SelectedValue),
                codigoconcepto = Convert.ToInt32(ddlConceptoModal.SelectedValue),
                codigoempleado = Convert.ToInt64(txtCodigoEmpleadoModal.Text),
                descripcion = txtDescripcionModal.Text.ToUpper(),
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue)
            };

            LiquidacionNominaDetaEmpleado detalle = new LiquidacionNominaDetaEmpleado
            {
                codigoconcepto = Convert.ToInt32(ddlConceptoModal.SelectedValue),
                codigoempleado = Convert.ToInt64(txtCodigoEmpleadoModal.Text),
                descripcion_concepto = txtDescripcionModal.Text.ToUpper(),
            };

            DateTime fechaInicio = Convert.ToDateTime(txtFechaIngreso.Text);
            DateTime fechaFin = Convert.ToDateTime(txtFechaRetiro.Text);

            decimal valor = Convert.ToDecimal(txtValorModal.Text);

            // Verifico si el concepto a guardar es una hora extra
            bool esConceptoHorasExtras = _liquidacionServices.ConsultarSiConceptoEsHoraExtra(detalle.codigoconcepto, Usuario);
            bool esCalculoEnCantidad = chkBoxCantidadModal.Checked;

            LiquidacionNominaDetaEmpleado conceptoEmpleado = null;

            // Si no soy concepto horas extras, entro directo a calcular mi valor concepto en la funcion de la BD
            // Si soy concepto horas extras y estoy calculando las horas extras por cantidad, entonces tambien voy a la funcion de la BD para calcular el valor a pagar
            if (!esConceptoHorasExtras || (esConceptoHorasExtras && esCalculoEnCantidad))
            {
                conceptoEmpleado = _liquidacionServices.CalcularValorConceptoNominaDeUnEmpleado(detalle.codigoconcepto, detalle.codigoempleado, Convert.ToInt64(ddlTipoNomina.SelectedValue), fechaInicio, fechaFin, valor, valor, Usuario,0);
            }
            else
            {
                // Si soy concepto horas extras y estoy calculando las horas extras por valor, simplemente lleno la entidad con los datos que ya tengo
                conceptoEmpleado = new LiquidacionNominaDetaEmpleado
                {
                    tipo = 1, // Pago
                    valorconcepto = valor // Valor a pagar
                };
            }

            novedad.valorconcepto = conceptoEmpleado.valorconcepto;
            novedad.tipo = conceptoEmpleado.tipo;

            detalle.valorconcepto = conceptoEmpleado.valorconcepto;
            detalle.tipo = conceptoEmpleado.tipo;

            if (liquidacionNovedades == null)
            {
                liquidacionNovedades = new List<LiquidacionNominaNoveEmpleado>
                {
                    novedad
                };
            }
            else
            {
                liquidacionNovedades.Add(novedad);
            }

            List<LiquidacionContratoDetalle> liquidacionDetalleEmpleado1 = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionContratoDetalle>;


            List<LiquidacionNominaDetaEmpleado> liquidacionDetalleEmpleado = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionNominaDetaEmpleado>;

            liquidacionDetalleEmpleado.Add(detalle);

            List<LiquidacionNominaDetalle> liquidacionDetalle = ViewState[_viewStateLiquidacion] as List<LiquidacionNominaDetalle>;
            LiquidacionNominaDetalle liquidacion = liquidacionDetalle.Where(x => x.codigoempleado == Convert.ToInt64(txtCodigoEmpleadoModal.Text)).FirstOrDefault();

            if (detalle.tipo == 1) // Pago
            {
                liquidacion.valortotalapagar += detalle.valorconcepto;
            }
            else if (detalle.tipo == 2) // Descuento
            {
                liquidacion.valortotalapagar -= detalle.valorconcepto;
            }



            ViewState[_viewStateLiquidacion] = liquidacionDetalle;
            ViewState[_viewStateLiquidacionDetalle] = liquidacionDetalleEmpleado;
            ViewState[_viewStateLiquidacionNovedades] = liquidacionNovedades;

            LimpiarFormularioAgregarNovedades();
            mpeAgregarNovedades.Hide();
        }
        else
        {
            mpeAgregarNovedades.Show();
        }
    }



    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {

            panelProceso.Visible = false;
            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    #endregion


    #region Eventos Controles (No Botonera)


    protected void ddlTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue))
        {
            long codigoNomina = Convert.ToInt64(ddlTipoNomina.SelectedValue);

            IngresoPersonalService ingresoPersonalService = new IngresoPersonalService();
            IngresoPersonal ingresoPersonal = ingresoPersonalService.ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina(_consecutivoEmpleado.Value, codigoNomina, Usuario);

            
            if (ingresoPersonal.consecutivo > 0)
            { 
            ddlCentroCosto.SelectedValue = ingresoPersonal.codigocentrocosto.ToString();
            ddlContrato.SelectedValue = ingresoPersonal.codigotipocontrato.ToString();
            txtFechaIngreso.Text = ingresoPersonal.fechaingreso.Value.ToShortDateString();
            txtCodigoIngresoPersonal.Text = ingresoPersonal.consecutivo.ToString();

            pnlLiquidacion.Visible = true;
            ddlCentroCosto.Visible = true;
            labelCentrodeCosto.Visible = true;
             lblError.Visible = false;
            }
        else
        {
            pnlLiquidacion.Visible = false;
            ddlCentroCosto.Visible = false;
            labelCentrodeCosto.Visible = false;
            lblError.Visible = true;
             lblError.Text = "Empleado se encuentra retirado";

            }
    
        LimpiarLiquidacion();
        }
    }

    protected void txtFechaRetiro_TextChanged(object sender, EventArgs e)
    {
        LimpiarLiquidacion();
    }



    protected void ddlConceptoModal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ddlConceptoModal.SelectedValue))
        {
            long codigoConcepto = Convert.ToInt64(ddlConceptoModal.SelectedValue);
            int unidadConceptoNomina = _liquidacionServices.ConsultarUnidadConceptoNomina(codigoConcepto, Usuario);
            UnidadConceptoNomina unidad = unidadConceptoNomina.ToEnum<UnidadConceptoNomina>();

            if (unidad == UnidadConceptoNomina.Valor)
            {
                chkBoxCantidadModal.Visible = false;
                chkBoxValorModal.Visible = true;
            }
            else
            {
                chkBoxCantidadModal.Visible = true;
                chkBoxValorModal.Visible = false;
            }
        }
    }


    #endregion


    #region Metodos Ayuda


    LiquidacionContrato ObtenerValores()
    {
        LiquidacionContrato liquidacion = new LiquidacionContrato
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleado.Text),
            fecharetiro = !string.IsNullOrWhiteSpace(txtFechaRetiro.Text) ? Convert.ToDateTime(txtFechaRetiro.Text) : default(DateTime),
            consecutivo = _consecutivoLiquidacion.HasValue ? _consecutivoLiquidacion.Value : 0,
            codigonomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long),
            codigocentrocosto = !string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue) ? Convert.ToInt64(ddlCentroCosto.SelectedValue) : default(long),
            codigotiporetirocontrato = !string.IsNullOrWhiteSpace(ddlMotivoRetiro.SelectedValue) ? Convert.ToInt64(ddlMotivoRetiro.SelectedValue) : 0,
            codigoingresopersonal = Convert.ToInt64(txtCodigoIngresoPersonal.Text),
            fechaingreso = Convert.ToDateTime(txtFechaIngreso.Text),
            primaCalculo = Convert.ToDecimal(hiddenPrimaValorPagado.Value),
            primaDias = Convert.ToInt64(hiddenPrimaDiasPagado.Value),
            cesantiasCalculo = Convert.ToDecimal(hiddenCesantiasValorPagado.Value),
            cesantiasDias = Convert.ToInt64(hiddenCesantiasDiasPagado.Value),
            vacacionesCalculo = Convert.ToDecimal(hiddenVacacionesValorPagado.Value),
            vacacionesDias = Convert.ToInt64(hiddenVacacionesDiasPagado.Value),
            desc_tipo_contrato = ddlContrato.SelectedItem.Text,
            desc_motivo_retiro = ddlMotivoRetiro.SelectedItem.Text,
            nombre_empleado = txtNombreCliente.Text,
            identificacion_empleado = txtIdentificacionn.Text
        };

        return liquidacion;
    }

    void LimpiarFormularioAgregarNovedades()
    {
        txtDescripcionModal.Text = string.Empty;
        txtValorModal.Text = string.Empty;
        txtIdentificacionModal.Text = string.Empty;
        txtNombresModal.Text = string.Empty;
        chkBoxCantidadModal.Checked = false;
        chkBoxCantidadModal.Visible = false;
        chkBoxValorModal.Checked = false;
        hiddenIndexSeleccionado.Value = string.Empty;
    }
    bool ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) || string.IsNullOrWhiteSpace(ddlCentroCosto.SelectedValue) || string.IsNullOrWhiteSpace(ddlMotivoRetiro.SelectedValue)
            || string.IsNullOrWhiteSpace(txtFechaIngreso.Text) || string.IsNullOrWhiteSpace(txtFechaRetiro.Text) || string.IsNullOrWhiteSpace(txtCodigoEmpleado.Text) || string.IsNullOrWhiteSpace(txtCodigoIngresoPersonal.Text)
            || string.IsNullOrWhiteSpace(ddlContrato.SelectedValue))
        {
            VerError("Faltan datos por llenar!.");
            return false;
        }

        DateTime fechaIngreso = Convert.ToDateTime(txtFechaIngreso.Text);
        DateTime fechaRetiro = Convert.ToDateTime(txtFechaRetiro.Text);
        if (fechaIngreso > fechaRetiro)
        {
            VerError("La fecha de retiro no puede ser mayor a la fecha de ingreso!.");
            return false;
        }

        return true;
    }

    void DeshabilitarControles()
    {
        ddlTipoNomina.Enabled = false;
        ddlMotivoRetiro.Enabled = false;
        CalendarExtender1.Enabled = false;
    }

    bool ValidarModalNuevaNovedad()
    {
        if (string.IsNullOrWhiteSpace(txtValorModal.Text))
        {
            lblmsjModalNovedadNueva.Text = "Debe digitar el valor de la novedad!.";
            lblmsjModalNovedadNueva.Visible = true;
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtDescripcionModal.Text))
        {
            lblmsjModalNovedadNueva.Text = "Debe digitar la descripcion de la novedad!.";
            lblmsjModalNovedadNueva.Visible = true;
            return false;
        }

        if (string.IsNullOrWhiteSpace(ddlCentroCostoModal.SelectedValue))
        {
            lblmsjModalNovedadNueva.Text = "La novedad a registrar tiene un centro de costo invalido!.";
            lblmsjModalNovedadNueva.Visible = true;
            return false;
        }

        if (string.IsNullOrWhiteSpace(ddlNominaModal.SelectedValue))
        {
            lblmsjModalNovedadNueva.Text = "La novedad a registrar tiene un tipo de nomina invalido!.";
            lblmsjModalNovedadNueva.Visible = true;
            return false;
        }

        if (string.IsNullOrWhiteSpace(ddlConceptoModal.SelectedValue))
        {
            lblmsjModalNovedadNueva.Text = "Debe seleccionar el concepto de la novedad!.";
            lblmsjModalNovedadNueva.Visible = true;
            return false;
        }

        if (!chkBoxCantidadModal.Checked && !chkBoxValorModal.Checked)
        {
            lblmsjModalNovedadNueva.Text = "Debe seleccionar un tipo para el concepto de la novedad!.";
            lblmsjModalNovedadNueva.Visible = true;
            return false;
        }

        if (chkBoxCantidadModal.Checked && chkBoxValorModal.Checked)
        {
            lblmsjModalNovedadNueva.Text = "No debe seleccionar dos tipos de calculo para el concepto de la novedad, deselecciona uno!.";
            lblmsjModalNovedadNueva.Visible = true;
            return false;
        }

        lblmsjModalNovedadNueva.Visible = false;
        return true;
    }


    void LimpiarLiquidacion()
    {
        gvLiquidacion.DataSource = null;
        gvLiquidacion.DataBind();
        txtValorTotal.Text = string.Empty;
        pnlLiquidacionHecha.Visible = false;
        ViewState[_viewStateListaEmpleadoDetalle] = null;
        hiddenPrimaDiasPagado.Value = string.Empty;
        hiddenPrimaValorPagado.Value = string.Empty;
        hiddenCesantiasDiasPagado.Value = string.Empty;
        hiddenCesantiasValorPagado.Value = string.Empty;
        hiddenVacacionesDiasPagado.Value = string.Empty;
        hiddenVacacionesValorPagado.Value = string.Empty;
    }

    void GenerarLiquidacionContrato()
    {
        pnlLiquidacionHecha.Visible = true;
        LiquidacionContrato liquidacion = new LiquidacionContrato
        {
            codigoempleado = _consecutivoEmpleado.Value,
            codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
            fecharetiro = Convert.ToDateTime(txtFechaRetiro.Text)
        };

        Tuple<List<LiquidacionContratoDetalle>, LiquidacionContrato> tuple = _liquidacionContratoService.GenerarLiquidacionDeContrato(liquidacion, Usuario);
        List<LiquidacionContratoDetalle> listaLiquidacionContrato = tuple.Item1;
        listaLiquidacionContrato = BuildearListaParaMostrarEnGrilla(listaLiquidacionContrato);

        gvLiquidacion.DataSource = listaLiquidacionContrato;
        gvLiquidacion.DataBind();

        decimal valorTotalParaPagar = CalcularValorTotalParaPagar(listaLiquidacionContrato);
        txtValorTotal.Text = _stringHelper.FormatearNumerosComoCurrency(valorTotalParaPagar);

        ViewState[_viewStateListaEmpleadoDetalle] = listaLiquidacionContrato;
        GuardarLiquidacionContratoEnHiddens(tuple.Item2);

        Site toolBar = (Site)Master;
        toolBar.MostrarImprimir(true);

    }

    decimal CalcularValorTotalParaPagar(List<LiquidacionContratoDetalle> listaDetalleEmpleado)
    {
        decimal valorPositivo = listaDetalleEmpleado.Where(x => x.tipoCalculo == 1).Sum(x => x.valor);
        decimal valorNegativo = listaDetalleEmpleado.Where(x => x.tipoCalculo == 2).Sum(x => x.valor);
        decimal valorTotalParaPagar = valorPositivo - valorNegativo;

        return valorTotalParaPagar;
    }

    List<LiquidacionContratoDetalle> BuildearListaParaMostrarEnGrilla(List<LiquidacionContratoDetalle> listaLiquidacionDetalleEmpleado)
    {
        foreach (LiquidacionContratoDetalle detalle in listaLiquidacionDetalleEmpleado)
        {
            if (detalle.tipoCalculo == 1) // Pago
            {
                detalle.valorPago = detalle.valor;
            }
            else if (detalle.tipoCalculo == 2) // Descuento
            {
                detalle.valorDescuento = detalle.valor;
            }
        }

        return listaLiquidacionDetalleEmpleado;
    }

    void GuardarLiquidacionContratoEnHiddens(LiquidacionContrato liquidacion)
    {
        hiddenPrimaDiasPagado.Value = liquidacion.primaDias.ToString();
        hiddenPrimaValorPagado.Value = liquidacion.primaCalculo.ToString();
        hiddenCesantiasDiasPagado.Value = liquidacion.cesantiasDias.ToString();
        hiddenCesantiasValorPagado.Value = liquidacion.cesantiasCalculo.ToString();
        hiddenVacacionesDiasPagado.Value = liquidacion.vacacionesDias.ToString();
        hiddenVacacionesValorPagado.Value = liquidacion.vacacionesCalculo.ToString();
        hiddenDiasLiquidacion.Value = liquidacion.dias.ToString();
    }


    #endregion


    #region Metodos Reporte


    void ConstruirReporteDesprendibleVacaciones(DataTable dataTable, LiquidacionContrato empleado, List<LiquidacionContratoDetalle> listaPagos, List<LiquidacionContratoDetalle> listaDescuentos)
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
            LiquidacionContratoDetalle pago = listaPagos.ElementAtOrDefault(i);
            // Sacamos el descuento de este indice
            LiquidacionContratoDetalle descuentos = listaDescuentos.ElementAtOrDefault(i);

            // Creamos el row default para este empleado (Llenamos el row con la informacion que siempre es la misma, la usada para hacer el group)
            DataRow row = ConstruirDataRowDefaultDesprendibleDeVacacionesParaUnEmpleado(dataTable, empleado, totalValorPago, totalValorDescuentos);

            row[7] = i; // Solo es usado como muletilla para el grouping en el .rdlc

            // Si efectivamente sacamos un pago en este indice, lo lleno
            if (pago != null)
            {
                row[8] = pago.desc_conceptoNomina;
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
                row[10] = descuentos.desc_conceptoNomina;
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
            row[23] = pTexto;





            // Añadimos el row
            dataTable.Rows.Add(row);
        }

        ReportParameter[] param = new ReportParameter[]
        {
                new ReportParameter("NombreEmpresa", Usuario.empresa),
                new ReportParameter("NombreEmpleador", Usuario.representante_legal.ToUpper()),
                new ReportParameter("NitEmpresa", Usuario.nitempresa),
                new ReportParameter("CentroDeCosto", ddlCentroCosto.SelectedItem.Text),
                new ReportParameter("Nomina", ddlTipoNomina.SelectedItem.Text),
                new ReportParameter("FechaTerminacion", empleado.fecharetiro.ToShortDateString()),
                 new ReportParameter("ImagenReport", ImagenReporte())
        };

        rvReporteDesprendible.LocalReport.EnableExternalImages = true;
        rvReporteDesprendible.LocalReport.ReportPath = @"Page\Nomina\LiquidacionContrato\DesprendibleLiquidacionContrato.rdlc";
        rvReporteDesprendible.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", dataTable);
        rvReporteDesprendible.LocalReport.DataSources.Clear();
        rvReporteDesprendible.LocalReport.DataSources.Add(rds);
        rvReporteDesprendible.LocalReport.Refresh();

        pnlReporte.Visible = true;
        rvReporteDesprendible.Visible = true;
    }

    DataRow ConstruirDataRowDefaultDesprendibleDeVacacionesParaUnEmpleado(DataTable dataTable, LiquidacionContrato empleado, decimal totalValorPago, decimal totalValorDescuentos)
    {
        DateTimeHelper dateTimeHelper = new DateTimeHelper();
        DataRow row = dataTable.NewRow();

        row[0] = empleado.nombre_empleado;
        row[1] = empleado.identificacion_empleado;
        row[2] = empleado.desc_cargo;
        row[3] = _stringHelper.FormatearNumerosComoCurrency(empleado.salario);
        row[4] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago);
        row[5] = _stringHelper.FormatearNumerosComoCurrency(totalValorDescuentos);
        row[6] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago - totalValorDescuentos);
        row[12] = empleado.desc_tipo_contrato;
        row[13] = empleado.consecutivo;
        row[14] = hiddenDiasLiquidacion.Value.ToString();
        row[15] = empleado.desc_motivo_retiro;
        row[16] = _stringHelper.FormatearNumerosComoCurrency(empleado.primaCalculo);
        row[17] = empleado.primaDias.ToString("00");
        row[18] = _stringHelper.FormatearNumerosComoCurrency(empleado.vacacionesCalculo);
        row[19] = empleado.vacacionesDias.ToString("00");
        row[20] = _stringHelper.FormatearNumerosComoCurrency(empleado.cesantiasCalculo);
        row[21] = empleado.cesantiasDias.ToString("00");
        row[22] = empleado.fechaingreso.ToShortDateString();

        return row;
    }


    #endregion

}


