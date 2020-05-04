using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Services;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    StringHelper _stringHelper = new StringHelper();
    LiquidacionNominaService _liquidacionServices = new LiquidacionNominaService();
    readonly string _viewStateLiquidacion = "Liquidacion"; // Usado guardar la lista de empleados liquidados
    readonly string _viewStateLiquidacionDetalle = "LiquidacionDetalle"; // Usado para guardar la lista de los conceptos de nomina de todos los empleados
    readonly string _viewStateControlSeGeneroLiquidacion = "SeGeneroLiquidacion"; // Usado para el control de que se genero la liquidacion definitiva antes de guardar o no
    long? _consecutivoLiquidacion;
    long _codnomina;
    long _tiponomina;
    DateTime _fechaultliquidacion;
    DateTime _fechainicio;
    DateTime _fechafin;
    bool _esNuevaLiquidacion;
    bool _esPruebaLiquidacion=false;
    int tipoOpe = 138;
    DateTime FechaAct = DateTime.Now;

    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_liquidacionServices.CodigoProgramaanticipos, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            ctlMensajeGuardar.eventoClick += CtlMensajeGuardar_eventoClick;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
            panelGeneral.Visible = true;
           
            toolBar.eventoConsultar += (s, evt) =>
            {
                toolBar.MostrarGuardar(true);


            };
            toolBar.MostrarConsultar(false);
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionServices.CodigoProgramaanticipos, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _consecutivoLiquidacion = Session[_liquidacionServices.CodigoProgramaanticipos + ".id"] as long?;
        _esNuevaLiquidacion = !_consecutivoLiquidacion.HasValue || _consecutivoLiquidacion.Value <= 0;

        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }

    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlTipoNomina);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto);
        txtFechaInicio.Attributes.Add("readonly", "readonly");
        txtFechaFin.Attributes.Add("readonly", "readonly");

        if (!_esNuevaLiquidacion)
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarImprimir(true);         
            btnLiquidacionDefinitiva.Visible = false;    

            ConsultarLiquidacion();
            updatePanelLiquidacionGeneradas.Visible = true;
        }
    }


    #endregion


    #region Eventos GridView


    protected void ctlEmpleados_OnErrorControl(object sender, EmpleadosArgs e)
    {
        if (e.Error != null)
        {
            VerError(e.Error.Message);
        }
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;

        gvLista.DataSource = ViewState[_viewStateLiquidacion];
        gvLista.DataBind();
    }


    #endregion


    #region Metodos Modales




    #endregion


    #region Eventos Botonera


    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        View viewActual = mvPrincipal.GetActiveView();

        if (viewActual == viewPrincipal)
        {
            txtFechaInicio.Text = string.Empty;
            txtFechaFin.Text = string.Empty;
            ddlCentroCosto.SelectedIndex = 0;
            ddlTipoNomina.SelectedIndex = 0;

            DeshabilitarCampos(false);
          
            updatePanelLiquidacionGeneradas.Visible = false;

            gvLista.DataSource = null;
            gvLista.DataBind();
        }
        
    }

    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        View viewActual = mvPrincipal.GetActiveView();

        if (viewActual == viewPrincipal || viewActual == vFinal)
        {
            Navegar(Pagina.Lista);
        }
  
        else if (viewActual == viewImprimir)
        {
            mvPrincipal.SetActiveView(viewPrincipal);

            Site toolBar = (Site)Master;
            toolBar.MostrarImprimir(true);
        }
    }

    protected void btnLiquidacionDefinitiva_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarPagina())
        {
            _esPruebaLiquidacion = false;
            Session["EsPrueba"] = null;
            if(Session["EsPrueba"]==null)
          
            btnLiquidacionDefinitiva.BackColor = System.Drawing.Color.Red;
          
            DeshabilitarCampos(true);

            GenerarLiquidacion();

            ViewState[_viewStateControlSeGeneroLiquidacion] = true;
            updatePanelLiquidacionGeneradas.Visible = true;
           
        }
    }

    protected void btnLiquidacionPrueba_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarPagina())
        {
            DeshabilitarCampos(true);
            _esPruebaLiquidacion = true;
            Session["EsPrueba"] = _esPruebaLiquidacion;
            if (Session["EsPrueba"] != null)
                btnLiquidacionDefinitiva.BackColor = System.Drawing.Color.FromName("#359af2");
            GenerarLiquidacion();
           //  mvPrincipal.SetActiveView(viewEmpleados);

            //Site toolBar = (Site)Master;
            //toolBar.MostrarConsultar(true);
            //toolBar.MostrarGuardar(false);

            ViewState[_viewStateControlSeGeneroLiquidacion] = true;
            updatePanelLiquidacionGeneradas.Visible = true;

        }
    }

    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {

        VerError("");

        View viewActual = mvPrincipal.GetActiveView();
        View viewActualPrueba = mvPrincipal.GetActiveView();
        mvPrincipal.SetActiveView(viewPrincipal);
      
        if (Session["EsPrueba"] != null)
        {
            _esPruebaLiquidacion = true;
        }
        else
        { _esPruebaLiquidacion = false;
        }


        if (_esPruebaLiquidacion == false && ValidarProcesoContable(FechaAct, tipoOpe) == false )
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 138 = Anticipos de Nómina");
            return;
        }
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible)
        {
            rpta = ctlproceso.Inicializar(tipoOpe, FechaAct,(Usuario)Session["Usuario"]);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                // Activar demás botones que se requieran
                panelGeneral.Visible = false;
                panelProceso.Visible = true;
                mvPrincipal.SetActiveView(viewPrincipal);
            }
            else
            {
                // Crear la tarea de ejecución del proceso                

                if (viewActual == viewPrincipal && _esPruebaLiquidacion == false)
                {
                    VerError("");
                    if (ValidarPagina())
                    {

                      
                        ctlMensajeGuardar.MostrarMensaje("Se genera la liquidación de anticipos de nómina, seguro que desea continuar?");
                    }
                 
                }
                else
                    VerError("Se presentó error");
            }
        }         

        


       
    }

    void CtlMensajeGuardar_eventoClick(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
        Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        var cod_operacion = 0L;
        try
        {
            VerError("");
            VerError("");
            if (ddlCentroCosto.SelectedItem.Text == "Seleccione un Item")
            {
                ddlCentroCosto.SelectedValue = "1";
            }

            LiquidacionNomina liquidacion = new LiquidacionNomina
            {
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                fechainicio = Convert.ToDateTime(txtFechaInicio.Text),
                fechaterminacion = Convert.ToDateTime(txtFechaFin.Text),
                codigocentrocosto = Convert.ToInt64(ddlCentroCosto.SelectedValue),
                fechageneracion = Convert.ToDateTime(txtFechaAnticipos.Text),
                codigoanticipo= Convert.ToInt64(txtCodigoConsecutivo.Text)
            };

            bool? seGeneroLiquidacionDefinitiva = ViewState[_viewStateControlSeGeneroLiquidacion] as bool?;

            // Si anteriormente no se genero una liquidacion definitiva, la generamos nosotros antes de guardar
            if (!seGeneroLiquidacionDefinitiva.HasValue || !seGeneroLiquidacionDefinitiva.Value)
            {
                GenerarLiquidacion();
            }

            // CREAR OPERACION
            pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = tipoOpe;
            pOperacion.cod_caja = 0;
            pOperacion.cod_cajero = 0;
            pOperacion.observacion = "Anticipos de  Nómina";
            pOperacion.cod_proceso = null;
            pOperacion.fecha_oper = Convert.ToDateTime(FechaAct.ToShortDateString());
            pOperacion.fecha_calc = DateTime.Now;
            pOperacion.cod_ofi = Usuario.cod_oficina;



            List<LiquidacionNominaDetalle> listaLiquidacion = ViewState[_viewStateLiquidacion] as List<LiquidacionNominaDetalle>;
            List<LiquidacionNominaDetaEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionNominaDetaEmpleado>;
            liquidacion.codorigen = 2;
            liquidacion.cod_concepto = 9;
            liquidacion.cod_concepto_trans = 52;
            liquidacion.tipoanticipo = 0;
            bool exitoso = _liquidacionServices.CrearAnticiposNomina(liquidacion, listaLiquidacion, Usuario, pOperacion);
            cod_operacion = pOperacion.cod_ope;


            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarRegresar(false);

            btnLiquidacionDefinitiva.Visible = false;

           
          

          var usu = (Usuario)Session["usuario"];
          
            
            // Generar el comprobante
            if (pOperacion.cod_ope != 0)
            {
                ctlproceso.CargarVariables(pOperacion.cod_ope, tipoOpe, usu.codusuario, (Usuario)Session["usuario"]);
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }

          
        }
        catch (Exception ex)
        {
            VerError("Error al guardar la liquidacion de nomina, " + ex.Message);
        }
    }

    void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarImprimir(false);

        mvPrincipal.SetActiveView(viewImprimir);
    }


    protected void btnImprimirDesprendibles_Click(object sender, EventArgs e)
    {
        List<LiquidacionNominaDetalle> listaEmpleadosLiquidados = ViewState[_viewStateLiquidacion] as List<LiquidacionNominaDetalle>;
        List<LiquidacionNominaDetaEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionNominaDetaEmpleado>;
       
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
        dataTable.Columns.Add("ValorAnticipoSubsidio");

        // Recorro a todos los empleados que se liquidaron
        foreach (LiquidacionNominaDetalle empleado in listaEmpleadosLiquidados)
        {
            // Hallo los conceptos que se liquidaron para este empleado
            var conceptosDelEmpleado1 = listaEmpleadosLiquidados.Where(x => x.codigoempleado == empleado.codigoempleado).ToList();

            // Hallo los pagos de esos conceptos
            List<LiquidacionNominaDetaEmpleado> listaPagos = null; ;
            // Hallo los descuentos de esos conceptos
            List<LiquidacionNominaDetaEmpleado> listaDescuentos = null;
            List<LiquidacionNominaDetalle> lstempleado = listaEmpleadosLiquidados;

            ConstruirReporteDesprendibleNomina(dataTable, lstempleado,empleado, listaPagos, listaDescuentos);
        }
    }

    void ConstruirReporteDesprendibleNomina(DataTable dataTable, List<LiquidacionNominaDetalle> lstempleado, LiquidacionNominaDetalle empleado, List<LiquidacionNominaDetaEmpleado> listaPagos, List<LiquidacionNominaDetaEmpleado> listaDescuentos)
    {
        // Hallo el valor total de esos pagos
        decimal totalValorPago = empleado.valor_anticipo;
        decimal totalValorPagoSubsidio = empleado.valor_anticipo_sub;
        totalValorPago = totalValorPago ;
        // Hallo el valor total de esos descuentos
        decimal totalValorDescuentos = 0;

        // Hallo el numero de pagos
        int numeroPagos = 0;
        // Hallo el numero de descuentos       
        int numeroDescuentos = lstempleado.Count();

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
        for (int i = 0; i < numeroDescuentos; i++)
        {
           
            // Creamos el row default para este empleado (Llenamos el row con la informacion que siempre es la misma, la usada para hacer el group)
            DataRow row = ConstruirDataRowDefaultDesprendibleDeNominaParaUnEmpleado(dataTable, empleado, totalValorPago, totalValorDescuentos);

         
            // Añadimos el row
            dataTable.Rows.Add(row);
        }

        ReportParameter[] param = new ReportParameter[]
        {
                new ReportParameter("NombreEmpresa", Usuario.empresa),
                new ReportParameter("NitEmpresa", Usuario.nitempresa),
                new ReportParameter("CentroDeCosto", ddlCentroCosto.SelectedItem.Text),
                new ReportParameter("FechaPeriodicaInicio", txtFechaInicio.Text),
                new ReportParameter("FechaPeriodicaFinal", txtFechaFin.Text),
                new ReportParameter("Nomina", ddlTipoNomina.SelectedItem.Text),
                new ReportParameter("FechaPago", txtFechaFin.Text),
                new ReportParameter("ImagenReporte", ImagenReporte()),
                new ReportParameter("pElaborado", HttpUtility.HtmlDecode(vacios(Usuario.nombre)))
    };

        rvReporteDesprendible.LocalReport.EnableExternalImages = true;
        rvReporteDesprendible.LocalReport.ReportPath = @"Page\Nomina\AnticiposMasivos\DesprendibleAnticipos.rdlc";
        rvReporteDesprendible.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSetEmpleado", dataTable);
        rvReporteDesprendible.LocalReport.DataSources.Clear();
        rvReporteDesprendible.LocalReport.DataSources.Add(rds);
        rvReporteDesprendible.LocalReport.Refresh();

        pnlReporte.Visible = true;
        rvReporteDesprendible.Visible = true;
        rvReportePlanilla.Visible = false;
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

    DataRow ConstruirDataRowDefaultDesprendibleDeNominaParaUnEmpleado(DataTable dataTable, LiquidacionNominaDetalle empleado, decimal totalValorPago, decimal totalValorDescuentos)
    {
        DataRow row = dataTable.NewRow();
        row[0] = empleado.nombre_empleado;
        row[1] = empleado.identificacion_empleado;
        row[2] = empleado.desc_cargo;
        row[3] = _stringHelper.FormatearNumerosComoCurrency(empleado.salario);
        row[4] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago);
        row[5] = _stringHelper.FormatearNumerosComoCurrency(totalValorDescuentos);
        row[6] = _stringHelper.FormatearNumerosComoCurrency((totalValorPago+ empleado.valor_anticipo_sub) - totalValorDescuentos);
        row[12] = _stringHelper.FormatearNumerosComoCurrency(empleado.valor_anticipo_sub);

        return row;
    }



    protected void btnImprimirPlanilla_Click(object sender, EventArgs e)
    {
        List<LiquidacionNominaDetalle> listaEmpleadosLiquidados = ViewState[_viewStateLiquidacion] as List<LiquidacionNominaDetalle>;
        List<LiquidacionNominaDetaEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionNominaDetaEmpleado>;

        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("CodigoEmpleado");
        dataTable.Columns.Add("Identificacion");
        dataTable.Columns.Add("Nombre");
        dataTable.Columns.Add("Dias");
        dataTable.Columns.Add("Sueldo");
        dataTable.Columns.Add("TotalPagos");
        dataTable.Columns.Add("TotalDescuentos");
        dataTable.Columns.Add("TotalPagado");
        dataTable.Columns.Add("PorcentajeAnticipo");
        dataTable.Columns.Add("AnticipoSubsidio");
        dataTable.Columns.Add("PorcentajeAntSubs");       

        dataTable.Columns.Add("ValorColumnaUno");
        dataTable.Columns.Add("ValorColumnaDos");
        dataTable.Columns.Add("ValorColumnaTres");
        dataTable.Columns.Add("ValorColumnaCuatro");


        // Recorro a todos los empleados que se liquidaron
        foreach (LiquidacionNominaDetalle empleado in listaEmpleadosLiquidados)
        {
            // Hallo los conceptos que se liquidaron para este empleado
            var conceptosDelEmpleado = listaEmpleadosLiquidados.Where(x => x.codigoempleado == empleado.codigoempleado).ToList();

            // Hallo los pagos de esos conceptos
            List<LiquidacionNominaDetaEmpleado> listaPagos = null;
            // Hallo los descuentos de esos conceptos
            List<LiquidacionNominaDetaEmpleado> listaDescuentos = null;

            // Hallo el valor total de esos pagos
            decimal totalValorPago = empleado.valor_anticipo;

            // Hallo el valor total de esos pagos
            decimal totalValorPagoSub = empleado.valor_anticipo_sub;
            totalValorPago = totalValorPago ;
            // Hallo el valor total de esos descuentos
            decimal totalValorDescuentos = 0;
            // Hallo el valor total de las novedades
            decimal totalValorNovedades = 0 ;

            decimal valorPrimeraColumna = 0;

            decimal totalporcentajeanticipos = empleado.porcentaje_anticipo;
            decimal totalporcentajesubsidio = empleado.porcentaje_anticipo_sub;
            decimal totalvaloranticiposubsidio = empleado.valor_anticipo_sub;
         

            DataRow row = dataTable.NewRow();
            row[0] = empleado.consecutivo;
            row[1] = empleado.identificacion_empleado;
            row[2] = empleado.nombre_empleado;
            row[3] = empleado.dias;
            row[4] = _stringHelper.FormatearNumerosComoCurrency(empleado.salario);
            row[5] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago);
            row[6] = _stringHelper.FormatearNumerosComoCurrency(totalValorDescuentos);
            row[7] = _stringHelper.FormatearNumerosComoCurrency((totalValorPago )- totalValorDescuentos);
            row[8] = (totalporcentajeanticipos);
            row[9] = _stringHelper.FormatearNumerosComoCurrency(totalvaloranticiposubsidio);
            row[10] = (totalporcentajesubsidio);
            row[11] = _stringHelper.FormatearNumerosComoCurrency(valorPrimeraColumna);
         
            dataTable.Rows.Add(row);
        }

        // Hallo la cantidad de registros liquidados (Empleados liquidados)
        long totalizadoRegistros = listaEmpleadosLiquidados.Count;
        // Hallo el valor total de esos pagos
        decimal totalizadoValorPago = listaEmpleadosLiquidados.Where(x => x.valor_anticipo>0).Sum(x => x.valor_anticipo);

        // Hallo el valor total de esos descuentos
        decimal totalizadoValorDescuentos =0;
        // Hallo el valor total de las novedades
        decimal totalizadoValorNovedades = totalizadoValorPago - totalizadoValorDescuentos;

        // Hallo el valor total de los anticipos sobre el subsidio de transporte
        decimal totalizadoValorPagoSubsidio = listaEmpleadosLiquidados.Where(x => x.valor_anticipo_sub > 0).Sum(x => x.valor_anticipo_sub);

        decimal totalizadopago = totalizadoValorPago + totalizadoValorPagoSubsidio; 



        ReportParameter[] param = new ReportParameter[]
        {
                new ReportParameter("NombreEmpresa", Usuario.empresa),
                new ReportParameter("NitEmpresa", Usuario.nitempresa),
                new ReportParameter("FechaInicio", txtFechaInicio.Text),
                new ReportParameter("FechaFin", txtFechaFin.Text),
                new ReportParameter("NombreNomina", ddlTipoNomina.SelectedItem.Text),
                new ReportParameter("CantidadRegistros", totalizadoRegistros.ToString()),
                new ReportParameter("TotalPagos", _stringHelper.FormatearNumerosComoCurrency(totalizadoValorPago)),
                new ReportParameter("TotalDescuentos", _stringHelper.FormatearNumerosComoCurrency(totalizadoValorDescuentos)),
                new ReportParameter("TotalNovedades", _stringHelper.FormatearNumerosComoCurrency(totalizadoValorNovedades)),
                new ReportParameter("ImagenReporte", ImagenReporte()),
                new ReportParameter("TotalAnticipoSubsidio", _stringHelper.FormatearNumerosComoCurrency(totalizadoValorPagoSubsidio)),
                 new ReportParameter("TotalPagado", _stringHelper.FormatearNumerosComoCurrency(totalizadopago)),
        };

        rvReportePlanilla.LocalReport.EnableExternalImages = true;
        rvReportePlanilla.LocalReport.ReportPath = @"Page\Nomina\AnticiposMasivos\PlanillaAnticipos.rdlc";
        rvReportePlanilla.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("PlanillaAnticiposDataSet", dataTable);
        rvReportePlanilla.LocalReport.DataSources.Clear();
        rvReportePlanilla.LocalReport.DataSources.Add(rds);
        rvReportePlanilla.LocalReport.Refresh();

        pnlReporte.Visible = true;
        rvReportePlanilla.Visible = true;
        rvReporteDesprendible.Visible = false;
    }


    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        List<LiquidacionNominaDetalle> listaDetalle = ViewState[_viewStateLiquidacion] as List<LiquidacionNominaDetalle>;
        IQueryable<LiquidacionNominaDetalle> queryable = listaDetalle.AsQueryable();

        if (!string.IsNullOrWhiteSpace(txtNombre.Text))
        {
            queryable = queryable.Where(x => x.nombre_empleado.ToUpper().Contains(txtNombre.Text.ToUpper().Trim()));
        }

        if (!string.IsNullOrWhiteSpace(txtIdentificacion.Text))
        {
            queryable = queryable.Where(x => x.identificacion_empleado.Contains(txtIdentificacion.Text.Trim()));
        }

        if (!string.IsNullOrWhiteSpace(txtCodigoEmpleado.Text))
        {
            queryable = queryable.Where(x => x.codigoempleado == Convert.ToInt64(txtCodigoEmpleado.Text.Trim()));
        }

        gvLista.DataSource = queryable.ToList();
        gvLista.DataBind();
    }

   


    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        panelGeneral.Visible = true;
        panelProceso.Visible = false;
    }
    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            panelGeneral.Visible = true;
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


    #region Eventos Text Changed


    protected void txtFechaAnticipos_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtFechaInicio.Text) && !string.IsNullOrWhiteSpace(txtFechaFin.Text))
        {
            DateTime fechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
            DateTime fechaFinal = Convert.ToDateTime(txtFechaFin.Text);

            if (fechaInicio <= fechaFinal)
            {
                bool hayLiquidacion = _liquidacionServices.VerificarQueNoHallaUnaLiquidacionPreviaParaEstasFechasALiquidar(fechaInicio, fechaFinal, Usuario);

                if (hayLiquidacion)
                {
                    VerError("Ya existe una liquidacion previa para estas fechas elegidas!.");
                    RegistrarPostBack();
                }
                else if (!string.IsNullOrWhiteSpace(TextoLaberError))
                {
                    VerError("");
                    RegistrarPostBack();
                }
            }
        }
    }

    protected void ddlTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
    {
       if(ddlTipoNomina.SelectedValue!=" ")
         {
            ConsultarUltFechaLiquidacion();
        }
       else
        {
            txtFechaInicio.Text = "";
            txtFechaFin.Text = "";

        }

    }


    #endregion


    #region Metodos Ayuda


    void ConsultarLiquidacion()
    {
        LiquidacionNomina liquidacion = new LiquidacionNomina
        {
            consecutivo = _consecutivoLiquidacion.Value
        };

        LiquidacionNomina liquidacionNomina = _liquidacionServices.ConsultarAnticiposNomina(liquidacion.consecutivo, Usuario);
        List<LiquidacionNominaDetalle> liquidacionNominaDetalles = _liquidacionServices.ListarAnticiposNominaDetalle(liquidacion, Usuario);
      
        ViewState[_viewStateLiquidacion] = liquidacionNominaDetalles;
     
        gvLista.DataSource = liquidacionNominaDetalles;
        gvLista.DataBind();

        txtFechaInicio.Text = liquidacionNomina.fechainicio.ToShortDateString();
        txtFechaFin.Text = liquidacionNomina.fechaterminacion.ToShortDateString();
        ddlTipoNomina.SelectedValue = liquidacionNomina.codigonomina.ToString();
        ddlCentroCosto.SelectedValue = liquidacionNomina.codigocentrocosto.ToString();
        txtFechaAnticipos.Text=   liquidacionNomina.fechageneracion.ToShortDateString();

    }

    void GenerarLiquidacion(List<Empleados> listaEmpleados = null)
    {
        try
        {
            LiquidacionNomina liquidacion = new LiquidacionNomina
            {
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                fechainicio = Convert.ToDateTime(txtFechaInicio.Text),
                fechaterminacion = Convert.ToDateTime(txtFechaFin.Text),
                codigoempleado = Convert.ToInt64(0)
            };

            Tuple<List<LiquidacionNominaDetalle>, List<LiquidacionNominaDetaEmpleado>> listaLiquidacion = _liquidacionServices.GenerarAnticipos(liquidacion, listaEmpleados, Usuario);

            gvLista.DataSource = listaLiquidacion.Item1;
            gvLista.DataBind();

            ViewState[_viewStateLiquidacion] = listaLiquidacion.Item1;
            ViewState[_viewStateLiquidacionDetalle] = listaLiquidacion.Item2;
        }
        catch (Exception ex)
        {
            VerError("Error al general la liquidacion de la nomina, " + ex.Message);
        }
    }

    bool ValidarPagina()
    {
        if (string.IsNullOrWhiteSpace(txtFechaInicio.Text))
        {
            VerError("Debe llenar la fecha de inicio del periodo!.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtFechaFin.Text))
        {
            VerError("Debe llenar la fecha de terminación del periodo!.");
            return false;
        }

   /*   if (ddlCentroCosto.SelectedIndex == 0)
        {
            VerError("Debe seleccionar un centro de costo valido!.");
            return false;
        }
   */

        if (ddlTipoNomina.SelectedIndex == 0)
        {
            VerError("Debe seleccionar un tipo de nomina valido!.");
            return false;
        }

        if (Convert.ToDateTime(txtFechaInicio.Text) > Convert.ToDateTime(txtFechaFin.Text))
        {
            VerError("La fecha de inicio a liquidar no puede ser mayor a la fecha final!.");
            return false;
        }

        return true;
    }

    void DeshabilitarCampos(bool deshabilitar)
    {
        txtFechaInicio.ReadOnly = deshabilitar;
        txtFechaFin.ReadOnly = deshabilitar;
        ddlCentroCosto.Enabled = !deshabilitar;
        ddlTipoNomina.Enabled = !deshabilitar;
        CalendarExtender7.Enabled = !deshabilitar;
        CalendarExtender1.Enabled = !deshabilitar;
        CalendarExtenderAnticipos.Enabled = !deshabilitar;
    }

 

    void ConsultarUltFechaLiquidacion()
    {
        if (ddlTipoNomina.SelectedValue != " ")
        {
            _codnomina = Convert.ToInt32(ddlTipoNomina.SelectedValue.ToString());
         LiquidacionNomina liquidacionNomina = _liquidacionServices.ConsultarUltimaFechaAnticiposNomina(_codnomina, Usuario);
        _fechaultliquidacion = liquidacionNomina.fechaultliquidacion;
       
        _fechainicio = Convert.ToDateTime(liquidacionNomina.fechainicio);
        txtFechaInicio.Text = _fechainicio.ToShortDateString();

        _fechafin = Convert.ToDateTime(liquidacionNomina.fechaterminacion);
        txtFechaFin.Text = _fechafin.ToShortDateString();
        }
        LiquidacionNomina liquidacionNominaconsecutivo = _liquidacionServices.ConsultarUltimoAnticiposNomina(Usuario);
        if(liquidacionNominaconsecutivo.codigoanticipo==0 ) 
        {
            liquidacionNominaconsecutivo.codigoanticipo = 1;
        }
        txtCodigoConsecutivo.Text = Convert.ToString(liquidacionNominaconsecutivo.codigoanticipo);
    }


    #endregion




    Decimal suma = 0;

    Decimal sumaanticipossub = 0;
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            suma += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_anticipo"));
            sumaanticipossub += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valor_anticipo_sub"));
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[5].Text = "Total:";
            e.Row.Cells[6].Text = suma.ToString("c");
            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;

            e.Row.Cells[8].Text = sumaanticipossub.ToString("c");
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;

        }
    }
}