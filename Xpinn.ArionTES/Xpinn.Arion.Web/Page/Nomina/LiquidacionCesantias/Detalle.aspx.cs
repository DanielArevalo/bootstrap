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
using Cantidad_a_Letra;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;


public partial class Detalle : GlobalWeb
{
    StringHelper _stringHelper = new StringHelper();
    SeguridadSocialServices _SeguridadSocialService = new SeguridadSocialServices();

    LiquidacionCesantiasService _liquidacionServices = new LiquidacionCesantiasService();
    readonly string _viewStateLiquidacion = "Liquidacion"; // Usado para guardar los datos principales de la liquidacion
    readonly string _viewStateLiquidacionDetalle = "LiquidacionDetalle"; // Usado guardar la lista de empleados liquidados
    readonly string _viewStateLiquidacionDetalleEmpleado = "LiquidacionDetalleEmpleado"; // Usado para guardar la lista de los detalles de prima de todos los empleados
    readonly string _viewStateNovedadesLiquidadas = "NovedadesLiquidadas"; // Usado para el control de los conceptos de las opciones que fueron liquidados
    long? _consecutivoLiquidacion;
    int tipoOpe = 150;
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
            toolBar.eventoExportar += btnExportar_Click;

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

        List<LiquidacionCesantiasDetEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalleEmpleado] as List<LiquidacionCesantiasDetEmpleado>;
        List<LiquidacionCesantiasDetEmpleado> listaTemporal = new List<LiquidacionCesantiasDetEmpleado>();
        listaTemporal.AddRange(listaLiquidacionDetalle);

        var novedadesDelEmpleado = listaTemporal.Where(x => x.codigoempleado == Convert.ToInt64(txtCodigoEmpleadoReciboModal.Text)).ToList();
        decimal totalValorPositivo = novedadesDelEmpleado.Where(x => x.tipoCalculoNovedad == 1).Sum(x => x.valor);
        decimal totalValorNegativo = novedadesDelEmpleado.Where(x => x.tipoCalculoNovedad == 2).Sum(x => x.valor);

        novedadesDelEmpleado.Add(new LiquidacionCesantiasDetEmpleado { descripcionNovedad = "TOTAL NOVEDADES ", valor = totalValorPositivo - totalValorNegativo });

        foreach (LiquidacionCesantiasDetEmpleado liquidacion in novedadesDelEmpleado)
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
        List<LiquidacionCesantiasDetEmpleado> listaNovedades = null;

        List<LiquidacionCesantiasDetEmpleado> listaNovedadesAPlicadas = _liquidacionServices.ListarNovedadesCesantiasDetEmpleadoAplicada(año, codigoEmpleado, Usuario);


        List<LiquidacionCesantiasDetEmpleado> liquidacionCesantiasDetalleEmpleado = _liquidacionServices.ListarNovedadesCesantiasDetEmpleado(año, codigoEmpleado, Usuario);


        if (listaNovedadesAPlicadas.Count > 0)
        {
            listaNovedades = listaNovedadesAPlicadas;
        }
        else
        {
            listaNovedades = liquidacionCesantiasDetalleEmpleado;
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

    void btnExportar_Click(object sender, ImageClickEventArgs e)
    {
         gvLista.AllowPaging = false;


         //ExportarGridViewEnExcel(gvLista, "LiquidacionCesantias");

         if (gvLista.Rows.Count > 0)
         {
             StringBuilder sb = new StringBuilder();
             using (StringWriter sw = new StringWriter(sb))
             using (HtmlTextWriter htw = new HtmlTextWriter(sw))
             using (Page pagina = new Page())
             using (HtmlForm form = new HtmlForm())
             {
                 gvLista.EnableViewState = false;
                 pagina.EnableEventValidation = false;

                 pagina.DesignerInitialize();
                 pagina.Controls.Add(form);
                 form.Controls.Add(gvLista);


                this.gvLista.Columns[10].Visible = false;
                this.gvLista.Columns[11].Visible = false;
                this.gvLista.Columns[12].Visible = false;

                 pagina.RenderControl(htw);
                 Response.Clear();
                 Response.Buffer = true;
                 Response.ContentType = "application/vnd.ms-excel";
                 Response.AddHeader("Content-Disposition", "attachment;filename=LiquidacionCesantias.xls");

                 Response.Charset = "UTF-8";
                 Response.ContentEncoding = Encoding.Default;
                 Response.Write(sb.ToString());
                 Response.End();
             }


         }

     

    }
    protected void btnCloseReg1_Click(object sender, EventArgs e)
    {
        LimpiarFormularioAgregarNovedades();
        mpeAgregarNovedades.Hide();
    }

    protected void btnAgregarNovedadModal_Click(object sender, EventArgs e)
    {
        LiquidacionCesantiasService _novedadCesantiasService = new LiquidacionCesantiasService();


        Int64 anio = 0;

        NovedadCesantias novedadcesantias = new NovedadCesantias
        {
            codigoempleado = Convert.ToInt64(txtCodigoEmpleadoModal.Text),
            valor = Convert.ToDecimal(txtValorModal.Text),
            consecutivo = 0,
            codigonomina = !string.IsNullOrWhiteSpace(ddlTipoNomina.SelectedValue) ? Convert.ToInt64(ddlTipoNomina.SelectedValue) : default(long),
            codigotiponovedad = !string.IsNullOrWhiteSpace(ddlTipoNovedadModal.SelectedValue) ? Convert.ToInt64(ddlTipoNovedadModal.SelectedValue) : default(long),
            anio = Convert.ToInt64(txtAño.Text)
        };
        novedadcesantias = _liquidacionServices.CrearNovedadCesantias(novedadcesantias, Usuario);
        if (novedadcesantias.consecutivo > 0)
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
        List<LiquidacionCesantiasDetEmpleado> liquidacionDetalleEmpleado = ViewState[_viewStateLiquidacionDetalleEmpleado] as List<LiquidacionCesantiasDetEmpleado>;

        // Todas las novedades para este empleado solamente
        List<LiquidacionCesantiasDetEmpleado> liquidacionDetalleParaEsteEmpleado = liquidacionDetalleEmpleado.Where(x => x.codigoempleado == codigoEmpleado && x.esnovedadcreadamanual == 1).ToList();

        // Liquidacion seleccionada para borrar
        LiquidacionCesantiasDetEmpleado liquidacionDetalleParaBorrar = liquidacionDetalleParaEsteEmpleado.ElementAt(rowIndex);
        if (liquidacionDetalleParaBorrar != null)
        {
            // Borramos en la lista global y la lista unica de este usuario
            liquidacionDetalleEmpleado.Remove(liquidacionDetalleParaBorrar);
            liquidacionDetalleParaEsteEmpleado.Remove(liquidacionDetalleParaBorrar);
        }

        // Lista de resumen de todos los empleados
        List<LiquidacionCesantiasDetalle> liquidacionDetalle = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionCesantiasDetalle>;

        // Resumen de este empleado
        LiquidacionCesantiasDetalle liquidacion = liquidacionDetalle.Where(x => x.codigoempleado == codigoEmpleado).FirstOrDefault();

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
        ddlTipoNomina.SelectedIndex = 0;

        DeshabilitarCampos(false);
        LimpiarFormularioAgregarNovedades();
        updatePanelListaDetalle.Visible = false;
      

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
            LiquidacionCesantias liquidacion = new LiquidacionCesantias
            {
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                anio = Convert.ToInt32(txtAño.Text),
            };

            bool existe = _liquidacionServices.VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(liquidacion, Usuario);
            if (existe)
            {
                VerError("Ya existe una liquidación de cesantias generadas para este periodo!.");
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
            ctlMensajeGuardar.MostrarMensaje("Se generara la liquidación, seguro que desea continuar?");
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

            LiquidacionCesantias liquidacion = new LiquidacionCesantias
            {
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                anio = Convert.ToInt32(txtAño.Text),
                fechapago = Convert.ToDateTime(txtFechaPago.Text)
            };
            // CREAR OPERACION
            pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = tipoOpe;
            pOperacion.cod_caja = 0;
            pOperacion.cod_cajero = 0;
            pOperacion.observacion = "Liquidación Cesantias";
            pOperacion.cod_proceso = null;
            pOperacion.fecha_oper = Convert.ToDateTime(txtFechaPago.Text);
            pOperacion.fecha_calc = DateTime.Now;
            pOperacion.cod_ofi = Usuario.cod_oficina;


            List<LiquidacionCesantiasDetalle> listaLiquidacion = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionCesantiasDetalle>;
            List<LiquidacionCesantiasDetEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalleEmpleado] as List<LiquidacionCesantiasDetEmpleado>;
            List<NovedadPrima> listaConceptosOpcionesLiquidados = null;
            long año = Convert.ToInt64(txtAño.Text);

            List<NovedadPrima> liquidacionCesantiasDetalleEmpleado = _liquidacionServices.ListarNovedadesCesantias(año, Usuario);
            listaConceptosOpcionesLiquidados = liquidacionCesantiasDetalleEmpleado;

            liquidacion.codorigen = 6;
            if (cbCLiquidaInteres.Checked == true)
                liquidacion.liquidainteres = 1;
            else
                liquidacion.liquidacesantias = 0;

            if (cbLiquidaCesantias.Checked == true)
                liquidacion.liquidacesantias = 1;
            else
                liquidacion.liquidacesantias = 0;

         

            if (cbCLiquidaInteres.Checked == true && cbLiquidaCesantias.Checked == false)
            {
                liquidacion.liquidacesantias = 0;
                liquidacion.liquidainteres = 1;
            }

            if (cbLiquidaCesantias.Checked == true && cbCLiquidaInteres.Checked == false)
            {
                liquidacion.liquidacesantias = 1;
                liquidacion.liquidainteres = 0;
            }

            if (cbCLiquidaInteres.Checked == false && cbLiquidaCesantias.Checked == false)
            {
                liquidacion.liquidacesantias = 1;
                liquidacion.liquidainteres = 0;
            }


            if (cbCLiquidaInteres.Checked == true && cbLiquidaCesantias.Checked == true)
            {
                liquidacion.liquidacesantias = 1;
                liquidacion.liquidainteres = 1;
            }


            liquidacion = _liquidacionServices.CrearLiquidacionCesantias(liquidacion, listaLiquidacion, listaLiquidacionDetalle, listaConceptosOpcionesLiquidados, Usuario, pOperacion);
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
            VerError("Error al guardar la liquidacion de cesantias, " + ex.Message);
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
        List<LiquidacionCesantiasDetalle> listaDetalle = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionCesantiasDetalle>;
        IQueryable<LiquidacionCesantiasDetalle> queryable = listaDetalle.AsQueryable();

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

        List<LiquidacionCesantiasDetalle> listaFiltrada = queryable.ToList();
       

        gvLista.DataSource = listaFiltrada;
        gvLista.DataBind();
    }

    protected void btnImprimirDesprendibles_Click(object sender, EventArgs e)
    {
        List<LiquidacionCesantiasDetalle> listaEmpleadosLiquidados = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionCesantiasDetalle>;
        List<LiquidacionCesantiasDetEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalleEmpleado] as List<LiquidacionCesantiasDetEmpleado>;

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
        dataTable.Columns.Add("Valor_Letras");
        dataTable.Columns.Add("FechaInicio");
        dataTable.Columns.Add("FechaFin");
        dataTable.Columns.Add("Base_cesantias");

        // Recorro a todos los empleados que se liquidaron
        foreach (LiquidacionCesantiasDetalle empleado in listaEmpleadosLiquidados)
        {
            // Hallo los conceptos que se liquidaron para este empleado
            var novedadesDelEmpleado = listaLiquidacionDetalle.Where(x => x.codigoempleado == empleado.codigoempleado).ToList();

            // Hallo los pagos de estas novedades
            List<LiquidacionCesantiasDetEmpleado> listaPagos = novedadesDelEmpleado.Where(x => x.tipoCalculoNovedad == 1).ToList();

            // Hallo los descuentos de estas novedades
            List<LiquidacionCesantiasDetEmpleado> listaDescuentos = novedadesDelEmpleado.Where(x => x.tipoCalculoNovedad == 2).ToList();

            listaPagos = listaPagos.OrderByDescending(x => x.codigotiponovedad == 1).ToList();

            ConstruirReporteDesprendibleCesantias(dataTable, empleado, listaPagos, listaDescuentos);
        }
    }

    protected void btnImprimiListado_Click(object sender, EventArgs e)
    {
        List<LiquidacionCesantiasDetalle> listaEmpleadosLiquidados = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionCesantiasDetalle>;
        List<LiquidacionCesantiasDetEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalleEmpleado] as List<LiquidacionCesantiasDetEmpleado>;

        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Consecutivo");
        dataTable.Columns.Add("Nombre");
        dataTable.Columns.Add("DiasLiquidados");
        dataTable.Columns.Add("Salario");
        dataTable.Columns.Add("Pagos");
        dataTable.Columns.Add("Descuentos");
        dataTable.Columns.Add("Neto");
        dataTable.Columns.Add("Intereses");
        dataTable.Columns.Add("fechainicial");
        dataTable.Columns.Add("fechafinal");
        dataTable.Columns.Add("SubsidioTransporte");
        dataTable.Columns.Add("Base_cesantias");

        int consecutivo = 0;
        foreach (LiquidacionCesantiasDetalle empleadoResumen in listaEmpleadosLiquidados)
        {
            DataRow row = dataTable.NewRow();
            consecutivo++;
            row[0] = consecutivo;
            row[1] = empleadoResumen.nombre_empleado;
            row[2] = empleadoResumen.diasliquidar;
            row[3] = _stringHelper.FormatearNumerosComoCurrency(empleadoResumen.salario);

            List<LiquidacionCesantiasDetEmpleado> listaDetalleEmpleado = listaLiquidacionDetalle.Where(x => x.codigoempleado == empleadoResumen.codigoempleado).ToList();
            decimal totalPagosEmpleados = listaDetalleEmpleado.Where(x => x.liquidaCesantias == 1).Sum(x => x.valor);
            decimal totalDescuentosEmpleados = listaDetalleEmpleado.Where(x => x.tipoCalculoNovedad == 2).Sum(x => x.valor);
            decimal totalInteresEmpleados = listaDetalleEmpleado.Where(x => x.liquidainteres == 1).Sum(x => x.interes);
       
            // totalPagosEmpleados = totalPagosEmpleados - totalInteresEmpleados;

            if (totalPagosEmpleados>0)
            row[4] = _stringHelper.FormatearNumerosComoCurrency(totalPagosEmpleados- empleadoResumen.interes);
            else
                row[4] = _stringHelper.FormatearNumerosComoCurrency(totalPagosEmpleados);

            row[5] = _stringHelper.FormatearNumerosComoCurrency(totalDescuentosEmpleados);

        
            row[6] = _stringHelper.FormatearNumerosComoCurrency(empleadoResumen.valortotalpagar);
            

            row[7] = _stringHelper.FormatearNumerosComoCurrency(empleadoResumen.interes);
            row[8] = (empleadoResumen.fechainicio.ToShortDateString());
            row[9] = (empleadoResumen.fechaterminacion.ToShortDateString());
            row[10] = _stringHelper.FormatearNumerosComoCurrency(empleadoResumen.subsidio);
            row[11] = _stringHelper.FormatearNumerosComoCurrency(empleadoResumen.basecesantias);


            dataTable.Rows.Add(row);
        }

        decimal totalSalario = listaEmpleadosLiquidados.Sum(x => x.salario);
  
        decimal totalPagos = listaLiquidacionDetalle.Where(x => x.liquidaCesantias == 1).Sum(x => x.valor);
        decimal totalDescuentos = listaLiquidacionDetalle.Where(x => x.tipoCalculoNovedad == 2).Sum(x => x.valor);
        decimal totalNeto = listaEmpleadosLiquidados.Sum(x => x.valortotalpagar);
        decimal totalIntereses = listaEmpleadosLiquidados.Sum(x => x.interes);
        decimal totalSubsidio = listaEmpleadosLiquidados.Sum(x => x.subsidio);
        decimal totalBase = listaEmpleadosLiquidados.Sum(x => x.basecesantias);



        LiquidacionCesantias liquidacionCesantias = ViewState[_viewStateLiquidacion] as LiquidacionCesantias;
        DateTime fechaInicio = DateTime.MinValue;
        DateTime fechaFinal = DateTime.MinValue;

        fechaInicio = new DateTime(Convert.ToInt32(liquidacionCesantias.anio), 1, 1);
        fechaFinal = new DateTime(Convert.ToInt32(liquidacionCesantias.anio), 12, 30);




        Xpinn.Nomina.Entities.SeguridadSocial lstConsultar = new Xpinn.Nomina.Entities.SeguridadSocial();
        lstConsultar = _SeguridadSocialService.ConsultarSeguridadSocial((Usuario)Session["usuario"]);
        String aprobador = lstConsultar.aprobador.ToString();
        String revisor = lstConsultar.revisor.ToString();


        ReportParameter[] param = new ReportParameter[]
        {
                new ReportParameter("FechaInicio", fechaInicio.ToShortDateString()),
                new ReportParameter("FechaFin", fechaFinal.ToShortDateString()),
                new ReportParameter("FechaPago", txtFechaPago.Text),
                new ReportParameter("TotalSalario", _stringHelper.FormatearNumerosComoCurrency(totalSalario)),
                new ReportParameter("TotalPagos", _stringHelper.FormatearNumerosComoCurrency(totalPagos)),
                new ReportParameter("TotalDescuentos", _stringHelper.FormatearNumerosComoCurrency(totalDescuentos)),
                new ReportParameter("TotalNetos", _stringHelper.FormatearNumerosComoCurrency(totalNeto)),
                new ReportParameter("FechaGeneracion", liquidacionCesantias.fechageneracion.ToString("dd/MM/yyyy hh:mm tt")),
                new ReportParameter("ImagenReport", ImagenReporte()),
                new ReportParameter("NombreEmpresa", Usuario.empresa),
                new ReportParameter("NitEmpresa", Usuario.nitempresa),
                new ReportParameter("pElaborado", HttpUtility.HtmlDecode(vacios(txtusuariocreacion.Text))),
                new ReportParameter("pAprobado", HttpUtility.HtmlDecode(vacios(aprobador))),
                new ReportParameter("pRevisado", HttpUtility.HtmlDecode(vacios(revisor))),
                new ReportParameter("Titulo",liquidacionCesantias.liquidacesantias==1? "REPORTE LIQUIDACIÓN CESANTIAS":"REPORTE LIQUIDACIÓN INTERES CESANTIAS"),
                new ReportParameter("TotalIntereses", _stringHelper.FormatearNumerosComoCurrency(totalIntereses)),
                new ReportParameter("TotalSubsidio", _stringHelper.FormatearNumerosComoCurrency(totalSubsidio)),
                 new ReportParameter("TotalBase", _stringHelper.FormatearNumerosComoCurrency(totalBase)),

        };

        rvReportePlanilla.LocalReport.EnableExternalImages = true;
        rvReportePlanilla.LocalReport.ReportPath = @"Page\Nomina\LiquidacionCesantias\ListadoCesantias.rdlc";
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

        LiquidacionCesantias liquidacionCesantias = _liquidacionServices.ConsultarLiquidacionCesantias(codigoLiquidacion, Usuario);
        List<LiquidacionCesantiasDetalle> LiquidacionCesantiasDetalle = _liquidacionServices.ListarLiquidacionCesantiasDetalle(codigoLiquidacion, Usuario);
        List<LiquidacionCesantiasDetEmpleado> liquidacionCesantiasDetalleEmpleado = _liquidacionServices.ListarLiquidacionCesantiasDetEmpleado(codigoLiquidacion, Usuario);

        ViewState[_viewStateLiquidacionDetalle] = LiquidacionCesantiasDetalle;
        ViewState[_viewStateLiquidacionDetalleEmpleado] = liquidacionCesantiasDetalleEmpleado;
        ViewState[_viewStateLiquidacion] = liquidacionCesantias;

        gvLista.DataSource = LiquidacionCesantiasDetalle;
        gvLista.DataBind();

        updatePanelListaDetalle.Visible = true;
       
        txtAño.Text = liquidacionCesantias.anio.ToString();
        ddlTipoNomina.SelectedValue = liquidacionCesantias.codigonomina.ToString();
        txtFechaPago.Text = liquidacionCesantias.fechapago.ToShortDateString();


        if (!string.IsNullOrWhiteSpace(liquidacionCesantias.desc_usuario))
        {
            txtusuariocreacion.Text = liquidacionCesantias.desc_usuario.ToString();
        }



        DeshabilitarCampos(true);
    }
    void ConsultarUltimaLiquidacion()
    {
        long tiponomina;
        DateTimeHelper dateTimeHelper = new DateTimeHelper();
        if (ddlTipoNomina.SelectedItem.Text != "Seleccione un Item")
        {
            tiponomina = Convert.ToInt64(ddlTipoNomina.SelectedValue);

            DateTime numeroDias;
            LiquidacionCesantias liquidacionCesantias = _liquidacionServices.ConsultarUltpago(tiponomina, Usuario);
            Int64 ultimopago = (liquidacionCesantias.fechapago.Year);
            Int64 fechaactual = DateTime.Now.Year;
            if (ultimopago == fechaactual)
            {
                ultimopago = (liquidacionCesantias.fechapago.Year);
                txtAño.Text = ultimopago.ToString();

            }
            else
            {
                ultimopago = (liquidacionCesantias.fechapago.Year + 1);
                txtAño.Text = ultimopago.ToString();
            }
            txtAño.Enabled = false;



        }

    }

    void GenerarLiquidacion(List<Empleados> listaEmpleados = null)
    {
        try
        {
            LiquidacionCesantias liquidacion = new LiquidacionCesantias
            {
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                anio = Convert.ToInt32(txtAño.Text),
            };

            Tuple<List<LiquidacionCesantiasDetalle>, List<LiquidacionCesantiasDetEmpleado>, List<NovedadCesantias>> listaLiquidacion = _liquidacionServices.GenerarLiquidacionCesantias(liquidacion, Usuario);

            gvLista.DataSource = listaLiquidacion.Item1;
            Session["DTCESANTIAS"] = listaLiquidacion;

            gvLista.DataBind();
            this.gvLista.Columns[7].Visible = false;

            updatePanelListaDetalle.Visible = true;
          
            ViewState[_viewStateLiquidacionDetalle] = listaLiquidacion.Item1;
            ViewState[_viewStateLiquidacionDetalleEmpleado] = listaLiquidacion.Item2;
            ViewState[_viewStateNovedadesLiquidadas] = listaLiquidacion.Item3;
        }
        catch (Exception ex)
        {
            VerError("Error al general la liquidación de las cesantias, " + ex.Message);
        }
    }

    bool ValidarPagina()
    {
        if (string.IsNullOrWhiteSpace(txtAño.Text))
        {
            VerError("Debe llenar el año para la liquidación de las cesantias!.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtFechaPago.Text))
        {
            VerError("Debe llenar la fecha del pago para la liquidación de las cesantias!.");
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
        ddlTipoNomina.Enabled = !deshabilitar;
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


    void ConstruirReporteDesprendibleCesantias(DataTable dataTable, LiquidacionCesantiasDetalle empleado, List<LiquidacionCesantiasDetEmpleado> listaPagos, List<LiquidacionCesantiasDetEmpleado> listaDescuentos)
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
            LiquidacionCesantiasDetEmpleado pago = listaPagos.ElementAtOrDefault(i);
            // Sacamos el descuento de este indice
            LiquidacionCesantiasDetEmpleado descuentos = listaDescuentos.ElementAtOrDefault(i);

            // Creamos el row default para este empleado (Llenamos el row con la informacion que siempre es la misma, la usada para hacer el group)
            DataRow row = ConstruirDataRowDefaultDesprendibleDeCesantiasParaUnEmpleado(dataTable, empleado, totalValorPago, totalValorDescuentos);

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
            row[12] = pTexto;

            row[15] = _stringHelper.FormatearNumerosComoCurrency(empleado.basecesantias);  


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
                new ReportParameter("Nomina", ddlTipoNomina.SelectedItem.Text),
                new ReportParameter("FechaPago", txtFechaPago.Text),
                new ReportParameter("ImagenReport", ImagenReporte()),

                new ReportParameter("pElaborado", HttpUtility.HtmlDecode(vacios(txtusuariocreacion.Text))),
                new ReportParameter("pAprobado", HttpUtility.HtmlDecode(vacios(aprobador))),
                new ReportParameter("pRevisado", HttpUtility.HtmlDecode(vacios(revisor))),
                new ReportParameter("Titulo",empleado.liquidaCesantias==1? "DESPRENDIBLE LIQUIDACIÓN CESANTIAS":"DESPRENDIBLE LIQUIDACIÓN INTERES CESANTIAS"),
               

    };

   




    rvReporteDesprendible.LocalReport.EnableExternalImages = true;
        rvReporteDesprendible.LocalReport.ReportPath = @"Page\Nomina\LiquidacionCesantias\DesprendibleCesantias.rdlc";
        rvReporteDesprendible.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSet1", dataTable);
        rvReporteDesprendible.LocalReport.DataSources.Clear();
        rvReporteDesprendible.LocalReport.DataSources.Add(rds);
        rvReporteDesprendible.LocalReport.Refresh();

        pnlReporte.Visible = true;
        rvReporteDesprendible.Visible = true;
        rvReportePlanilla.Visible = false;
    }

    DataRow ConstruirDataRowDefaultDesprendibleDeCesantiasParaUnEmpleado(DataTable dataTable, LiquidacionCesantiasDetalle empleado, decimal totalValorPago, decimal totalValorDescuentos)
    {
        DataRow row = dataTable.NewRow();
        row[0] = empleado.nombre_empleado;
        row[1] = empleado.identificacion_empleado;
        row[2] = empleado.desc_cargo;
        row[3] = _stringHelper.FormatearNumerosComoCurrency(empleado.salario);
        row[4] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago);
        row[5] = _stringHelper.FormatearNumerosComoCurrency(totalValorDescuentos);
        row[6] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago - totalValorDescuentos);
        row[13] = empleado.fechainicio.ToShortDateString();
        row[14] = empleado.fechaterminacion.ToShortDateString();

        return row;
    }


    #endregion



    protected void ddlTipoNovedadModal_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlTipoNomina_SelectedIndexChanged(object sender, EventArgs e)
    {
        ConsultarUltimaLiquidacion();
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    Decimal subtotalintereses = 0;
    Decimal subtotalcesantias = 0;
    Decimal subtotalbasecesantias = 0;

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            subtotalintereses += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "interes"));
            subtotalcesantias += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valortotalpagar"));

            subtotalbasecesantias += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "basecesantias"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = "Total:";
            e.Row.Cells[7].Text = subtotalbasecesantias.ToString("c");
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[8].Text = subtotalcesantias.ToString("c");
            e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[9].Text = subtotalintereses.ToString("c");
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }
    }
}