using Microsoft.Reporting.WebForms;
using System;
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
    LiquidacionPrimaService _liquidacionServices = new LiquidacionPrimaService();
    readonly string _viewStateLiquidacion = "Liquidacion"; // Usado para guardar los datos principales de la liquidacion
    readonly string _viewStateLiquidacionDetalle = "LiquidacionDetalle"; // Usado guardar la lista de empleados liquidados
    readonly string _viewStateLiquidacionDetalleEmpleado = "LiquidacionDetalleEmpleado"; // Usado para guardar la lista de los detalles de prima de todos los empleados
    readonly string _viewStateNovedadesLiquidadas = "NovedadesLiquidadas"; // Usado para el control de los conceptos de las opciones que fueron liquidados
    long? _consecutivoLiquidacion;
    int tipoOpe = 109;
    bool _esNuevaLiquidacion;


    #region Eventos Iniciales


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(_liquidacionServices.CodigoPrograma, "L");

            Site toolBar = (Site)Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            ctlMensajeGuardar.eventoClick += CtlMensajeGuardar_eventoClick;
            toolBar.MostrarImprimir(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionServices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _consecutivoLiquidacion = Session[_liquidacionServices.CodigoPrograma + ".id"] as long?;
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
        LlenarListasDesplegables(TipoLista.ConceptoNomina, ddlTipoNovedadModal);

        txtFechaPago.Attributes.Add("readonly", "readonly");

        if (!_esNuevaLiquidacion)
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarImprimir(true);

            gvNovedades.Columns[0].Visible = false;
            gvLista.Columns[10].Visible = false;

            btnLiquidacionDefinitiva.Visible = false;

            ConsultarLiquidacion();
        }
    }


    #endregion


    #region Eventos GridView


    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLista.PageIndex = e.NewPageIndex;

        gvLista.DataSource = ViewState[_viewStateLiquidacionDetalle];
        gvLista.DataBind();
    }

    protected void btnVerRecibo_Click(object sender, EventArgs e)
    {
        ButtonGrid buttonGrid = sender as ButtonGrid;
        int rowIndex = Convert.ToInt32(buttonGrid.CommandArgument);
        GridViewRow row = gvLista.Rows[rowIndex];

        txtCodigoEmpleadoReciboModal.Text = HttpUtility.HtmlDecode(row.Cells[0].Text);
        txtIdentificacionReciboModal.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
        txtNombreReciboModal.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);

        List<LiquidacionPrimaDetEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalleEmpleado] as List<LiquidacionPrimaDetEmpleado>;
        List<LiquidacionPrimaDetEmpleado> listaTemporal = new List<LiquidacionPrimaDetEmpleado>();
        listaTemporal.AddRange(listaLiquidacionDetalle);

        var novedadesDelEmpleado = listaTemporal.Where(x => x.codigoempleado == Convert.ToInt64(txtCodigoEmpleadoReciboModal.Text)).ToList();
        decimal totalValorPositivo = novedadesDelEmpleado.Where(x => x.tipoCalculoNovedad == 1).Sum(x => x.valor);
        decimal totalValorNegativo = novedadesDelEmpleado.Where(x => x.tipoCalculoNovedad == 2).Sum(x => x.valor);

        novedadesDelEmpleado.Add(new LiquidacionPrimaDetEmpleado { descripcionNovedad = "TOTAL NOVEDADES ", valor = totalValorPositivo - totalValorNegativo });

        foreach (LiquidacionPrimaDetEmpleado liquidacion in novedadesDelEmpleado)
        {
            if (!liquidacion.descripcionNovedad.Contains("(+)") && !liquidacion.descripcionNovedad.Contains("(-)"))
            {
                if (liquidacion.tipoCalculoNovedad == 1) // Pagos
                {
                    liquidacion.descripcionNovedad += " (+) ";
                }
                else if (liquidacion.tipoCalculoNovedad == 2) // Descuentos
                {
                    liquidacion.descripcionNovedad += " (-) ";
                }
            }
        }

        gvReciboModal.DataSource = novedadesDelEmpleado;
        gvReciboModal.DataBind();

        mpRecibo.Show();
    }

    protected void btnVerNovedades_Click(object sender, EventArgs e)
    {
        ButtonGrid buttonGrid = sender as ButtonGrid;
        int rowIndex = Convert.ToInt32(buttonGrid.CommandArgument);
        GridViewRow row = gvLista.Rows[rowIndex];

        txtCodigoEmpleadoNovedadesModal.Text = HttpUtility.HtmlDecode(row.Cells[0].Text);
        txtIdentificacionesNovedadesModal.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
        txtNombresNovedadesModal.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);

        long codigoEmpleado = Convert.ToInt64(txtCodigoEmpleadoNovedadesModal.Text);

        long año = Convert.ToInt64(txtAño.Text);
        long semestre = Convert.ToInt64(ddlSemestre.SelectedValue);
        List<LiquidacionPrimaDetEmpleado> listaNovedades = null;

        List<LiquidacionPrimaDetEmpleado> listaNovedadesAPlicadas = _liquidacionServices.ListarNovedadesPrimaDetEmpleadoAplicada(año, semestre, codigoEmpleado, Usuario);


        List<LiquidacionPrimaDetEmpleado> liquidacionPrimaDetalleEmpleado = _liquidacionServices.ListarNovedadesPrimaDetEmpleado(año,semestre, codigoEmpleado, Usuario);


        if (listaNovedadesAPlicadas.Count>0)
        {
            listaNovedades = listaNovedadesAPlicadas;
        }
        else
        {
            listaNovedades = liquidacionPrimaDetalleEmpleado;
        }

        gvNovedades.EmptyDataText = "No hay novedades!.";
        gvNovedades.DataSource = listaNovedades.Where(x => x.codigoempleado == codigoEmpleado).ToList();
        gvNovedades.DataBind();

        mpNovedades.Show();
    }

    protected void btnAgregarNovedades_Click(object sender, EventArgs e)
    {
        if (_esNuevaLiquidacion)
        {
            ButtonGrid buttonGrid = sender as ButtonGrid;
            int rowIndex = Convert.ToInt32(buttonGrid.CommandArgument);
            GridViewRow row = gvLista.Rows[rowIndex];

            txtCodigoEmpleadoModal.Text = HttpUtility.HtmlDecode(row.Cells[0].Text);
            txtIdentificacionModal.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
            txtNombresModal.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
            hiddenIndexSeleccionado.Value = rowIndex.ToString();

            mpeAgregarNovedades.Show();
        }
    }


    #endregion


    #region Metodos Modales


    protected void btnCloseReg1_Click(object sender, EventArgs e)
    {
        LimpiarFormularioAgregarNovedades();
        mpeAgregarNovedades.Hide();
    }

    protected void btnAgregarNovedadModal_Click(object sender, EventArgs e)
    {
         NovedadPrimaService _novedadPrimaService = new NovedadPrimaService();

        Int32 semestre = 0;
        Int64 anio = 0;
        /* if (ValidarModalNuevaNovedad())
         {

             LiquidacionPrimaDetEmpleado detalle = new LiquidacionPrimaDetEmpleado
             {
                 esnovedadcreadamanual = 1, // Si
                 codigotiponovedad = Convert.ToInt32(ddlTipoNovedadModal.SelectedValue),
                 codigoempleado = Convert.ToInt64(txtCodigoEmpleadoModal.Text),
                 valor = Convert.ToDecimal(txtValorModal.Text),
                 descripcionNovedad = ddlTipoNovedadModal.SelectedItem.Text,

             };

             semestre = Convert.ToInt32(ddlSemestre.SelectedValue);
             anio = Convert.ToInt64(txtAño.Text);


             int? tipoCalculoNovedad = _liquidacionServices.ConsultarTipoCalculoNovedadDeUnTipoNovedad(detalle.codigotiponovedad, Usuario);
             if (!tipoCalculoNovedad.HasValue)
             {
                 throw new InvalidOperationException("No se consigue el tipo de calculo para este tipo de novedad!.");
             }

             detalle.tipoCalculoNovedad = tipoCalculoNovedad.Value;

             List<LiquidacionPrimaDetEmpleado> liquidacionDetalleNovedadesEmpleado = ViewState[_viewStateLiquidacionDetalleEmpleado] as List<LiquidacionPrimaDetEmpleado>;
             liquidacionDetalleNovedadesEmpleado.Add(detalle);

             List<LiquidacionPrimaDetalle> liquidacionDetalleEmpleado = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionPrimaDetalle>;
             LiquidacionPrimaDetalle liquidacion = liquidacionDetalleEmpleado.Where(x => x.codigoempleado == Convert.ToInt64(txtCodigoEmpleadoModal.Text)).FirstOrDefault();

             if (detalle.tipoCalculoNovedad == 1) // Pago
             {
                 liquidacion.valortotalpagar += detalle.valor;
             }
             else if (detalle.tipoCalculoNovedad == 2) // Descuento
             {
                 liquidacion.valortotalpagar -= detalle.valor;
             }



             gvLista.DataSource = liquidacionDetalleEmpleado;
             gvLista.DataBind();

             ViewState[_viewStateLiquidacionDetalle] = liquidacionDetalleEmpleado;
             ViewState[_viewStateLiquidacionDetalleEmpleado] = liquidacionDetalleNovedadesEmpleado;

             LimpiarFormularioAgregarNovedades();
             mpeAgregarNovedades.Hide();
         }
         else
         {
             mpeAgregarNovedades.Show();
         }
 */
        NovedadPrima novedadPrima = new NovedadPrima
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleadoModal.Text),
            valor = Convert.ToDecimal(txtValorModal.Text),
            consecutivo = 0,
            codigonomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long),
            codigotiponovedad = !string.IsNullOrWhiteSpace(ddlTipoNovedadModal.SelectedValue) ? Convert.ToInt64(ddlTipoNovedadModal.SelectedValue) : default(long),
            semestre = Convert.ToInt32(ddlSemestre.SelectedValue),
            anio = Convert.ToInt64(txtAño.Text)
        };
        novedadPrima = _novedadPrimaService.CrearNovedadPrima(novedadPrima, Usuario);
        if (novedadPrima.consecutivo > 0)
        {
            LimpiarFormularioAgregarNovedades();
            mpeAgregarNovedades.Hide();
        }           
         else
         {
                mpeAgregarNovedades.Show();
         }
        

    }

    protected void btnCerrarReciboModal_Click(object sender, EventArgs e)
    {
        LimpiarFormularioRecibo();
        mpRecibo.Hide();
    }

    protected void btnCerrarNovedadesModal_Click(object sender, EventArgs e)
    {
        LimpiarFormularioNovedades();
        mpNovedades.Hide();
    }

    protected void OnRowCommandDeleting(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = Convert.ToInt32(e.CommandArgument);
        int codigoEmpleado = Convert.ToInt32(txtCodigoEmpleadoNovedadesModal.Text);

        // Todas las novedades de todos los empleados
        List<LiquidacionPrimaDetEmpleado> liquidacionDetalleEmpleado = ViewState[_viewStateLiquidacionDetalleEmpleado] as List<LiquidacionPrimaDetEmpleado>;

        // Todas las novedades para este empleado solamente
        List<LiquidacionPrimaDetEmpleado> liquidacionDetalleParaEsteEmpleado = liquidacionDetalleEmpleado.Where(x => x.codigoempleado == codigoEmpleado && x.esnovedadcreadamanual == 1).ToList();

        // Liquidacion seleccionada para borrar
        LiquidacionPrimaDetEmpleado liquidacionDetalleParaBorrar = liquidacionDetalleParaEsteEmpleado.ElementAt(rowIndex);
        if (liquidacionDetalleParaBorrar != null)
        {
            // Borramos en la lista global y la lista unica de este usuario
            liquidacionDetalleEmpleado.Remove(liquidacionDetalleParaBorrar);
            liquidacionDetalleParaEsteEmpleado.Remove(liquidacionDetalleParaBorrar);
        }

        // Lista de resumen de todos los empleados
        List<LiquidacionPrimaDetalle> liquidacionDetalle = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionPrimaDetalle>;

        // Resumen de este empleado
        LiquidacionPrimaDetalle liquidacion = liquidacionDetalle.Where(x => x.codigoempleado == codigoEmpleado).FirstOrDefault();

        // Sumamos o restamos el total a pagar para sincronizar
        if (liquidacionDetalleParaBorrar.tipoCalculoNovedad == 1) // Pago
        {
            liquidacion.valortotalpagar -= liquidacionDetalleParaBorrar.valor;
        }
        else if (liquidacionDetalleParaBorrar.tipoCalculoNovedad == 2) // Descuento
        {
            liquidacion.valortotalpagar += liquidacionDetalleParaBorrar.valor;
        }

        // Volvemos a guardar en el viewstate
        ViewState[_viewStateLiquidacionDetalle] = liquidacionDetalle;
        ViewState[_viewStateLiquidacionDetalleEmpleado] = liquidacionDetalleEmpleado;

        // Bindeamos las GridView
        gvLista.DataSource = liquidacionDetalle;
        gvLista.DataBind();
        gvNovedades.DataSource = liquidacionDetalleParaEsteEmpleado;
        gvNovedades.DataBind();

        mpNovedades.Show();
    }

    protected void gvNovedades_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        mpNovedades.Show();
    }

    #endregion


    #region Eventos Botonera


    void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        txtFechaPago.Text = string.Empty;
        txtAño.Text = string.Empty;
        ddlSemestre.SelectedIndex = 0;
        ddlCentroCosto.SelectedIndex = 0;
        ddlTipoNomina.SelectedIndex = 0;

        DeshabilitarCampos(false);
        LimpiarFormularioAgregarNovedades();
        updatePanelListaDetalle.Visible = false;
        lblTotalNomina.Visible = false;
        lblTotalNomina.Text = string.Empty;
        lblTotalNominaIdenti.Visible = false;

        gvLista.DataSource = null;
        gvLista.DataBind();

        gvNovedades.DataSource = null;
        gvNovedades.DataBind();

        gvReciboModal.DataSource = null;
        gvReciboModal.DataBind();
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
            LiquidacionPrima liquidacion = new LiquidacionPrima
            {
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                semestre = Convert.ToInt32(ddlSemestre.SelectedValue),
                anio = Convert.ToInt32(txtAño.Text),
            };

            bool existe = _liquidacionServices.VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(liquidacion, Usuario);
            if (existe)
            {
                VerError("Ya existe una liquidacion de primas generadas para este periodo!.");
                return;
            }
            else
            {
                DeshabilitarCampos(true);

                GenerarLiquidacion();
            }
        }
    }

    void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        if (ValidarPagina())
        {
            ctlMensajeGuardar.MostrarMensaje("Se generara la liquidacion, seguro que desea continuar?");
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

            LiquidacionPrima liquidacion = new LiquidacionPrima
            {
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                semestre = Convert.ToInt32(ddlSemestre.SelectedValue),
                anio = Convert.ToInt32(txtAño.Text),
                codigocentrocosto = Convert.ToInt64(ddlCentroCosto.SelectedValue),
                fechapago = Convert.ToDateTime(txtFechaPago.Text)
            };
            // CREAR OPERACION
            pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = tipoOpe;
            pOperacion.cod_caja = 0;
            pOperacion.cod_cajero = 0;
            pOperacion.observacion = "Liquidación Prima";
            pOperacion.cod_proceso = null;
            pOperacion.fecha_oper = Convert.ToDateTime(txtFechaPago.Text);
            pOperacion.fecha_calc = DateTime.Now;
            pOperacion.cod_ofi = Usuario.cod_oficina;
           

            List<LiquidacionPrimaDetalle> listaLiquidacion = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionPrimaDetalle>;
            List<LiquidacionPrimaDetEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalleEmpleado] as List<LiquidacionPrimaDetEmpleado>;
            List<NovedadPrima> listaConceptosOpcionesLiquidados = null;            
            long año = Convert.ToInt64(txtAño.Text);
            long semestre = Convert.ToInt64(ddlSemestre.SelectedValue);
            List<NovedadPrima> liquidacionPrimaDetalleEmpleado = _liquidacionServices.ListarNovedadesPrima(año, semestre, Usuario);

            listaConceptosOpcionesLiquidados=liquidacionPrimaDetalleEmpleado;

            

            liquidacion.codorigen = 5;
            liquidacion = _liquidacionServices.CrearLiquidacionPrima(liquidacion, listaLiquidacion, listaLiquidacionDetalle, listaConceptosOpcionesLiquidados, Usuario, pOperacion);
            ViewState[_viewStateLiquidacion] = liquidacion;

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarImprimir(true);

            btnLiquidacionDefinitiva.Visible = false;

            gvNovedades.Columns[0].Visible = false;
            gvLista.Columns[10].Visible = false;

            mvPrincipal.SetActiveView(vFinal);

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
            VerError("Error al guardar la liquidacion de prima, " + ex.Message);
        }
    }

    void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarImprimir(false);

        mvPrincipal.SetActiveView(viewImprimir);
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        List<LiquidacionPrimaDetalle> listaDetalle = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionPrimaDetalle>;
        IQueryable<LiquidacionPrimaDetalle> queryable = listaDetalle.AsQueryable();

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

        List<LiquidacionPrimaDetalle> listaFiltrada = queryable.ToList();
        lblTotalNomina.Text = _stringHelper.FormatearNumerosComoCurrency(listaFiltrada.Sum(x => x.valortotalpagar));

        gvLista.DataSource = listaFiltrada;
        gvLista.DataBind();
    }

    protected void btnImprimirDesprendibles_Click(object sender, EventArgs e)
    {
        List<LiquidacionPrimaDetalle> listaEmpleadosLiquidados = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionPrimaDetalle>;
        List<LiquidacionPrimaDetEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalleEmpleado] as List<LiquidacionPrimaDetEmpleado>;

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


        // Recorro a todos los empleados que se liquidaron
        foreach (LiquidacionPrimaDetalle empleado in listaEmpleadosLiquidados)
        {
            // Hallo los conceptos que se liquidaron para este empleado
            var novedadesDelEmpleado = listaLiquidacionDetalle.Where(x => x.codigoempleado == empleado.codigoempleado).ToList();

            // Hallo los pagos de estas novedades
            List<LiquidacionPrimaDetEmpleado> listaPagos = novedadesDelEmpleado.Where(x => x.tipoCalculoNovedad == 1).ToList();

            // Hallo los descuentos de estas novedades
            List<LiquidacionPrimaDetEmpleado> listaDescuentos = novedadesDelEmpleado.Where(x => x.tipoCalculoNovedad == 2).ToList();

            listaPagos = listaPagos.OrderByDescending(x => x.codigotiponovedad == 1).ToList();

            ConstruirReporteDesprendiblePrima(dataTable, empleado, listaPagos, listaDescuentos);
        }
    }

    protected void btnImprimiListado_Click(object sender, EventArgs e)
    {
        List<LiquidacionPrimaDetalle> listaEmpleadosLiquidados = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionPrimaDetalle>;
        List<LiquidacionPrimaDetEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalleEmpleado] as List<LiquidacionPrimaDetEmpleado>;

        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Consecutivo");
        dataTable.Columns.Add("Nombre");
        dataTable.Columns.Add("DiasLiquidados");
        dataTable.Columns.Add("Salario");
        dataTable.Columns.Add("Pagos");
        dataTable.Columns.Add("Descuentos");
        dataTable.Columns.Add("Neto");

        int consecutivo = 0;
        foreach (LiquidacionPrimaDetalle empleadoResumen in listaEmpleadosLiquidados)
        {
            DataRow row = dataTable.NewRow();
            consecutivo++;
            row[0] = consecutivo;
            row[1] = empleadoResumen.nombre_empleado;
            row[2] = empleadoResumen.diasliquidar;
            row[3] = _stringHelper.FormatearNumerosComoCurrency(empleadoResumen.salario);

            List<LiquidacionPrimaDetEmpleado> listaDetalleEmpleado = listaLiquidacionDetalle.Where(x => x.codigoempleado == empleadoResumen.codigoempleado).ToList();
            decimal totalPagosEmpleados = listaDetalleEmpleado.Where(x => x.tipoCalculoNovedad == 1).Sum(x => x.valor);
            decimal totalDescuentosEmpleados = listaDetalleEmpleado.Where(x => x.tipoCalculoNovedad == 2).Sum(x => x.valor);

            row[4] = _stringHelper.FormatearNumerosComoCurrency(totalPagosEmpleados);
            row[5] = _stringHelper.FormatearNumerosComoCurrency(totalDescuentosEmpleados);
            row[6] = _stringHelper.FormatearNumerosComoCurrency(empleadoResumen.valortotalpagar);
            dataTable.Rows.Add(row);
        }

        decimal totalSalario = listaEmpleadosLiquidados.Sum(x => x.salario);
        decimal totalPagos = listaLiquidacionDetalle.Where(x => x.tipoCalculoNovedad == 1).Sum(x => x.valor);
        decimal totalDescuentos = listaLiquidacionDetalle.Where(x => x.tipoCalculoNovedad == 2).Sum(x => x.valor);
        decimal totalNeto = listaEmpleadosLiquidados.Sum(x => x.valortotalpagar);

        LiquidacionPrima liquidacionPrima = ViewState[_viewStateLiquidacion] as LiquidacionPrima;
        DateTime fechaInicio = DateTime.MinValue;
        DateTime fechaFinal = DateTime.MinValue;

        if (liquidacionPrima.semestre == 1) // Primer Semestre
        {
            fechaInicio = new DateTime(Convert.ToInt32(liquidacionPrima.anio), 1, 1);
            fechaFinal = new DateTime(Convert.ToInt32(liquidacionPrima.anio), 6, 30);
        }
        else
        {
            fechaInicio = new DateTime(Convert.ToInt32(liquidacionPrima.anio), 7, 1);
            fechaFinal = new DateTime(Convert.ToInt32(liquidacionPrima.anio), 12, 30);
        }

        ReportParameter[] param = new ReportParameter[]
        {
                new ReportParameter("FechaInicio", fechaInicio.ToShortDateString()),
                new ReportParameter("FechaFin", fechaFinal.ToShortDateString()),
                new ReportParameter("FechaPago", txtFechaPago.Text),
                new ReportParameter("TotalSalario", _stringHelper.FormatearNumerosComoCurrency(totalSalario)),
                new ReportParameter("TotalPagos", _stringHelper.FormatearNumerosComoCurrency(totalPagos)),
                new ReportParameter("TotalDescuentos", _stringHelper.FormatearNumerosComoCurrency(totalDescuentos)),
                new ReportParameter("TotalNetos", _stringHelper.FormatearNumerosComoCurrency(totalNeto)),
                new ReportParameter("FechaGeneracion", liquidacionPrima.fechageneracion.ToString("dd/MM/yyyy hh:mm tt")),
                new ReportParameter("ImagenReport", ImagenReporte()),
                 new ReportParameter("NombreEmpresa", Usuario.empresa),
                new ReportParameter("NitEmpresa", Usuario.nitempresa),
               new ReportParameter("pElaborado", HttpUtility.HtmlDecode(vacios(Usuario.nombre)))
        };

        rvReportePlanilla.LocalReport.EnableExternalImages = true;
        rvReportePlanilla.LocalReport.ReportPath = @"Page\Nomina\LiquidacionPrimas\ListadoPrimas.rdlc";
        rvReportePlanilla.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", dataTable);
        rvReportePlanilla.LocalReport.DataSources.Clear();
        rvReportePlanilla.LocalReport.DataSources.Add(rds);
        rvReportePlanilla.LocalReport.Refresh();

        pnlReporte.Visible = true;
        rvReportePlanilla.Visible = true;
        rvReporteDesprendible.Visible = false;
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


    #region Metodos Ayuda


    void ConsultarLiquidacion()
    {
        long codigoLiquidacion = _consecutivoLiquidacion.Value;

        LiquidacionPrima liquidacionPrima = _liquidacionServices.ConsultarLiquidacionPrima(codigoLiquidacion, Usuario);
        List<LiquidacionPrimaDetalle> liquidacionPrimaDetalle = _liquidacionServices.ListarLiquidacionPrimaDetalle(codigoLiquidacion, Usuario);
        List<LiquidacionPrimaDetEmpleado> liquidacionPrimaDetalleEmpleado = _liquidacionServices.ListarLiquidacionPrimaDetEmpleado(codigoLiquidacion, Usuario);

        ViewState[_viewStateLiquidacionDetalle] = liquidacionPrimaDetalle;
        ViewState[_viewStateLiquidacionDetalleEmpleado] = liquidacionPrimaDetalleEmpleado;
        ViewState[_viewStateLiquidacion] = liquidacionPrima;

        gvLista.DataSource = liquidacionPrimaDetalle;
        gvLista.DataBind();

        updatePanelListaDetalle.Visible = true;
        lblTotalNominaIdenti.Visible = true;
        lblTotalNomina.Visible = true;
        lblTotalNomina.Text = _stringHelper.FormatearNumerosComoCurrency(liquidacionPrimaDetalle.Sum(x => x.valortotalpagar));

        txtAño.Text = liquidacionPrima.anio.ToString();
        ddlSemestre.SelectedValue = liquidacionPrima.semestre.ToString();
        ddlTipoNomina.SelectedValue = liquidacionPrima.codigonomina.ToString();
        ddlCentroCosto.SelectedValue = liquidacionPrima.codigocentrocosto.ToString();
        txtFechaPago.Text = liquidacionPrima.fechapago.ToShortDateString();

        DeshabilitarCampos(true);
    }

    void GenerarLiquidacion(List<Empleados> listaEmpleados = null)
    {
        try
        {
            LiquidacionPrima liquidacion = new LiquidacionPrima
            {
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                codigocentrocosto = Convert.ToInt64(ddlCentroCosto.SelectedValue),
                semestre = Convert.ToInt32(ddlSemestre.SelectedValue),
                anio = Convert.ToInt32(txtAño.Text),
            };

            Tuple<List<LiquidacionPrimaDetalle>, List<LiquidacionPrimaDetEmpleado>, List<NovedadPrima>> listaLiquidacion = _liquidacionServices.GenerarLiquidacionPrima(liquidacion, Usuario);

            gvLista.DataSource = listaLiquidacion.Item1;
            gvLista.DataBind();

            updatePanelListaDetalle.Visible = true;
            lblTotalNominaIdenti.Visible = true;
            lblTotalNomina.Visible = true;
            lblTotalNomina.Text = _stringHelper.FormatearNumerosComoCurrency(listaLiquidacion.Item1.Sum(x => x.valortotalpagar));

            ViewState[_viewStateLiquidacionDetalle] = listaLiquidacion.Item1;
            ViewState[_viewStateLiquidacionDetalleEmpleado] = listaLiquidacion.Item2;
            ViewState[_viewStateNovedadesLiquidadas] = listaLiquidacion.Item3;
        }
        catch (Exception ex)
        {
            VerError("Error al general la liquidacion de la prima, " + ex.Message);
        }
    }

    bool ValidarPagina()
    {
        if (string.IsNullOrWhiteSpace(txtAño.Text))
        {
            VerError("Debe llenar el año para la liquidacion de la prima!.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtFechaPago.Text))
        {
            VerError("Debe llenar la fecha del pago para la liquidacion de la prima!.");
            return false;
        }

        if (ddlSemestre.SelectedIndex == 0)
        {
            VerError("Debe seleccionar un semestre valido!.");
            return false;
        }

        if (ddlCentroCosto.SelectedIndex == 0)
        {
            VerError("Debe seleccionar un centro de costo valido!.");
            return false;
        }

        if (ddlTipoNomina.SelectedIndex == 0)
        {
            VerError("Debe seleccionar un tipo de nomina valido!.");
            return false;
        }

        return true;
    }

    bool ValidarModalNuevaNovedad()
    {
        if (string.IsNullOrWhiteSpace(txtValorModal.Text))
        {
            lblmsjModalNovedadNueva.Text = "Debe digitar el valor de la novedad!.";
            lblmsjModalNovedadNueva.Visible = true;
            return false;
        }

        if (string.IsNullOrWhiteSpace(ddlTipoNovedadModal.SelectedValue))
        {
            lblmsjModalNovedadNueva.Text = "Debe seleccionar el tipo de la novedad!.";
            lblmsjModalNovedadNueva.Visible = true;
            return false;
        }

        lblmsjModalNovedadNueva.Visible = false;
        return true;
    }

    void DeshabilitarCampos(bool deshabilitar)
    {
        txtAño.ReadOnly = deshabilitar;
        ddlCentroCosto.Enabled = !deshabilitar;
        ddlTipoNomina.Enabled = !deshabilitar;
        ddlSemestre.Enabled = !deshabilitar;
        CalendarExtender7.Enabled = !deshabilitar;
    }

    void LimpiarFormularioAgregarNovedades()
    {
        txtValorModal.Text = string.Empty;
        txtIdentificacionModal.Text = string.Empty;
        txtNombresModal.Text = string.Empty;
        ddlTipoNovedadModal.SelectedIndex = 0;
        hiddenIndexSeleccionado.Value = string.Empty;
    }

    void LimpiarFormularioRecibo()
    {
        txtCodigoEmpleadoReciboModal.Text = string.Empty;
        txtIdentificacionReciboModal.Text = string.Empty;
        txtNombreReciboModal.Text = string.Empty;
        gvReciboModal.DataSource = null;
        gvReciboModal.DataBind();
    }

    void LimpiarFormularioNovedades()
    {
        txtCodigoEmpleadoNovedadesModal.Text = string.Empty;
        txtIdentificacionesNovedadesModal.Text = string.Empty;
        txtNombresNovedadesModal.Text = string.Empty;
        gvNovedades.DataSource = null;
        gvNovedades.DataBind();
    }

    #endregion


    #region Metodos Reporte


    void ConstruirReporteDesprendiblePrima(DataTable dataTable, LiquidacionPrimaDetalle empleado, List<LiquidacionPrimaDetEmpleado> listaPagos, List<LiquidacionPrimaDetEmpleado> listaDescuentos)
    {
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
            LiquidacionPrimaDetEmpleado pago = listaPagos.ElementAtOrDefault(i);
            // Sacamos el descuento de este indice
            LiquidacionPrimaDetEmpleado descuentos = listaDescuentos.ElementAtOrDefault(i);

            // Creamos el row default para este empleado (Llenamos el row con la informacion que siempre es la misma, la usada para hacer el group)
            DataRow row = ConstruirDataRowDefaultDesprendibleDePrimaParaUnEmpleado(dataTable, empleado, totalValorPago, totalValorDescuentos);

            row[7] = i; // Solo es usado como muletilla para el grouping en el .rdlc

            // Si efectivamente sacamos un pago en este indice, lo lleno
            if (pago != null)
            {
                row[8] = pago.descripcionNovedad;
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
                row[10] = descuentos.descripcionNovedad;
                row[11] = _stringHelper.FormatearNumerosComoCurrency(descuentos.valor);
            }
            else
            {
                row[10] = string.Empty;
                row[11] = string.Empty;
            }

            // Añadimos el row
            dataTable.Rows.Add(row);
        }

        ReportParameter[] param = new ReportParameter[]
        {
                new ReportParameter("NombreEmpresa", Usuario.empresa),
                new ReportParameter("NitEmpresa", Usuario.nitempresa),
                new ReportParameter("CentroDeCosto", ddlCentroCosto.SelectedItem.Text),
                new ReportParameter("Nomina", ddlTipoNomina.SelectedItem.Text),
                new ReportParameter("FechaPago", txtFechaPago.Text),
                new ReportParameter("ImagenReport", ImagenReporte())
        };

        rvReporteDesprendible.LocalReport.EnableExternalImages = true;
        rvReporteDesprendible.LocalReport.ReportPath = @"Page\Nomina\LiquidacionPrimas\DesprendiblePrima.rdlc";
        rvReporteDesprendible.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", dataTable);
        rvReporteDesprendible.LocalReport.DataSources.Clear();
        rvReporteDesprendible.LocalReport.DataSources.Add(rds);
        rvReporteDesprendible.LocalReport.Refresh();

        pnlReporte.Visible = true;
        rvReporteDesprendible.Visible = true;
        rvReportePlanilla.Visible = false;
    }

    DataRow ConstruirDataRowDefaultDesprendibleDePrimaParaUnEmpleado(DataTable dataTable, LiquidacionPrimaDetalle empleado, decimal totalValorPago, decimal totalValorDescuentos)
    {
        DataRow row = dataTable.NewRow();
        row[0] = empleado.nombre_empleado;
        row[1] = empleado.identificacion_empleado;
        row[2] = empleado.desc_cargo;
        row[3] = _stringHelper.FormatearNumerosComoCurrency(empleado.salario);
        row[4] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago);
        row[5] = _stringHelper.FormatearNumerosComoCurrency(totalValorDescuentos);
        row[6] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago - totalValorDescuentos);
        row[12] = empleado.fechainicio.ToShortDateString();
        row[13] = empleado.fechaterminacion.ToShortDateString();

        return row;
    }


    #endregion



    protected void ddlTipoNovedadModal_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}