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
using System.IO;
using Cantidad_a_Letra;
public partial class Detalle : GlobalWeb
{
    StringHelper _stringHelper = new StringHelper();
    LiquidacionNominaService _liquidacionServices = new LiquidacionNominaService();

    readonly string _viewStateLiquidacion = "Liquidacion"; // Usado guardar la lista de empleados liquidados
    readonly string _viewStateLiquidacionDetalle = "LiquidacionDetalle"; // Usado para guardar la lista de los conceptos de nomina de todos los empleados
    readonly string _viewStateLiquidacionNovedades = "LiquidacionNovedades"; // Usado para guardar la lista de las novedades agregadas manualmente de todos los empleados
    readonly string _viewStateControlSeGeneroLiquidacion = "SeGeneroLiquidacion"; // Usado para el control de que se genero la liquidacion definitiva antes de guardar o no
    readonly string _viewStateConceptosLiquidadosOpciones = "ConceptosLiquidadosOpciones"; // Usado para el control de los conceptos de las opciones que fueron liquidados
    long? _consecutivoLiquidacion;
    long _codnomina;
    long _tiponomina;
    DateTime _fechaultliquidacion;
    DateTime _fechainicio;
    DateTime _fechafin;
    bool _esNuevaLiquidacion;
    bool _esPruebaLiquidacion = false;
    int tipoOpe = 109;
    DateTime FechaAct = DateTime.Now;
    EmpleadoService _empleadoService = new EmpleadoService();
    ConceptoNominaService _conceptosService = new ConceptoNominaService();
    String _codPersona;
    string _codEmpleado;

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
            ctlMensajeGuardarPrueba.eventoClick += ctlMensajeGuardarPrueba_eventoClick;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
            panelGeneral.Visible = true;

            toolBar.eventoConsultar += (s, evt) =>
            {
                toolBar.MostrarGuardar(true);
                ctlEmpleados.Actualizar();
            };
            toolBar.MostrarConsultar(false);
            toolBar.MostrarImprimir(false);
            //ACTIVAR INDEX
            mvPrincipal.Visible = true;
            mvPrincipal.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionServices.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _consecutivoLiquidacion = Session[_liquidacionServices.CodigoPrograma + ".id"] as long?;
            _esNuevaLiquidacion = !_consecutivoLiquidacion.HasValue || _consecutivoLiquidacion.Value <= 0;
            //  Session["NovedadAplicada"] = null;
            if (!IsPostBack)
            {
                InicializarPagina();
            }


            if (Session[_liquidacionServices.CodigoPrograma + ".id"] != null)
            {
                idObjeto = Session[_liquidacionServices.CodigoPrograma + ".id"].ToString();
                Session.Remove(_liquidacionServices.CodigoPrograma + ".id");

                if (gvLista.Rows.Count > 0)
                    btnCargarDatos.Visible = true;
                Site toolbar = (Site)this.Master;
                toolbar.MostrarGuardar(true);
                toolbar.MostrarExportar(true);
                toolbar.MostrarImprimir(true);

            }
            else
            {
            }



        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_liquidacionServices.CodigoPrograma, "Page_Load", ex);
        }
    }



    void InicializarPagina()
    {
        LlenarListasDesplegables(TipoLista.NominaEmpleado, ddlTipoNomina, ddlNominaModal);
        LlenarListasDesplegables(TipoLista.CentroCostos, ddlCentroCosto, ddlCentroCostoModal);
        LlenarListasDesplegables(TipoLista.ConceptoNomina, ddlConceptoModal);

        txtFechaInicio.Attributes.Add("readonly", "readonly");
        txtFechaFin.Attributes.Add("readonly", "readonly");

        if (!_esNuevaLiquidacion)
        {
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarImprimir(true);

            gvNovedades.Columns[0].Visible = false;
            gvLista.Columns[8].Visible = false;

            btnLiquidacionDefinitiva.Visible = false;
            btnLiquidacionPrueba.Visible = false;

            ConsultarLiquidacion();

            //ACTIVAR INDEX
            mvPrincipal.Visible = true;
            mvPrincipal.ActiveViewIndex = 0;

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

    protected void btnVerRecibo_Click(object sender, EventArgs e)
    {
        ButtonGrid buttonGrid = sender as ButtonGrid;
        int rowIndex = Convert.ToInt32(buttonGrid.CommandArgument);
        GridViewRow row = gvLista.Rows[rowIndex];

        txtCodigoEmpleadoReciboModal.Text = HttpUtility.HtmlDecode(row.Cells[0].Text);
        txtIdentificacionReciboModal.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
        txtNombreReciboModal.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);

        List<LiquidacionNominaDetaEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionNominaDetaEmpleado>;
        List<LiquidacionNominaDetaEmpleado> listaTemporal = new List<LiquidacionNominaDetaEmpleado>();
        listaTemporal.AddRange(listaLiquidacionDetalle);

        //List<LiquidacionNominaNoveEmpleado> liquidacionNominaNovedades = _liquidacionServices.ListarLiquidacionNominaNovedades(Convert.ToInt64(txtCodigoEmpleadoReciboModal.Text), Usuario);

        // List<LiquidacionNominaDetaEmpleado> liquidacionNominaNovedades = _liquidacionServices.ListarLiquidacionNominaNovedadesRecibo(Convert.ToInt64(txtCodigoEmpleadoReciboModal.Text), Usuario);
        //listaTemporal.AddRange(liquidacionNominaNovedades);

        var conceptosDelEmpleado = listaTemporal.Where(x => x.codigoempleado == Convert.ToInt64(txtCodigoEmpleadoReciboModal.Text)).ToList();
        decimal totalValorPositivo = conceptosDelEmpleado.Where(x => x.tipo == 1).Sum(x => x.valorconcepto);
        decimal totalValorNegativo = conceptosDelEmpleado.Where(x => x.tipo == 2).Sum(x => x.valorconcepto);

        conceptosDelEmpleado.Add(new LiquidacionNominaDetaEmpleado { descripcion_concepto = "TOTAL CONCEPTOS ", valorconcepto = totalValorPositivo - totalValorNegativo });

        foreach (LiquidacionNominaDetaEmpleado liquidacion in conceptosDelEmpleado)
        {
            if (!liquidacion.descripcion_concepto.Contains("(+)") && !liquidacion.descripcion_concepto.Contains("(-)"))
            {
                if (liquidacion.tipo == 1) // Pagos
                {
                    liquidacion.descripcion_concepto += " (+) ";
                }
                else if (liquidacion.tipo == 2) // Descuentos
                {
                    liquidacion.descripcion_concepto += " (-) ";
                }
            }
        }



        gvReciboModal.DataSource = conceptosDelEmpleado;
        gvReciboModal.DataBind();

        mpRecibo.Show();
    }

    protected void btnVerNovedades_Click(object sender, EventArgs e)
    {
        Session["NovedadesOld"] = 1;
        ButtonGrid buttonGrid = sender as ButtonGrid;
        int rowIndex = Convert.ToInt32(buttonGrid.CommandArgument);
        GridViewRow row = gvLista.Rows[rowIndex];

        txtCodigoEmpleadoNovedadesModal.Text = HttpUtility.HtmlDecode(row.Cells[0].Text);
        txtIdentificacionesNovedadesModal.Text = HttpUtility.HtmlDecode(row.Cells[1].Text);
        txtNombresNovedadesModal.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);

        long codigoEmpleado = Convert.ToInt64(txtCodigoEmpleadoNovedadesModal.Text);
        LiquidacionNomina nov = new LiquidacionNomina();
        List<LiquidacionNominaNoveEmpleado> liquidacionNominaNovedades = new List<LiquidacionNominaNoveEmpleado>();
        List<LiquidacionNominaNoveEmpleado> listaNovedades = new List<LiquidacionNominaNoveEmpleado>();

        if (Session["NovedadesOld"] != null)
        {
            liquidacionNominaNovedades = null;

        }
        if (Session["NovedadAplicada"] != null)
            liquidacionNominaNovedades = _liquidacionServices.ListarLiquidacionNominaNovedadesAplicadas(Convert.ToInt64(txtNomina.Text), codigoEmpleado, Usuario);
        else
            liquidacionNominaNovedades = _liquidacionServices.ListarLiquidacionNominaNovedades(codigoEmpleado, Usuario);


        listaNovedades = liquidacionNominaNovedades;

        if (liquidacionNominaNovedades == null)
        {
            liquidacionNominaNovedades = new List<LiquidacionNominaNoveEmpleado>();
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
            ddlNominaModal.SelectedValue = ddlTipoNomina.SelectedValue;
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
        ViewState[_viewStateLiquidacionNovedades] = null;



        if (ValidarModalNuevaNovedad())
        {
            List<LiquidacionNominaNoveEmpleado> liquidacionNovedades = ViewState[_viewStateLiquidacionNovedades] as List<LiquidacionNominaNoveEmpleado>;

            LiquidacionNominaNoveEmpleado novedad = new LiquidacionNominaNoveEmpleado
            {

                codigocentrocosto = Convert.ToInt64(ddlCentroCostoModal.SelectedValue),
                codigoconcepto = Convert.ToInt32(ddlConceptoModal.SelectedValue),
                codigoempleado = Convert.ToInt64(txtCodigoEmpleadoModal.Text),
                descripcion = txtDescripcionModal.Text.ToUpper(),
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),

            };

            LiquidacionNominaDetaEmpleado detalle = new LiquidacionNominaDetaEmpleado
            {
                codigoconcepto = Convert.ToInt32(ddlConceptoModal.SelectedValue),
                codigoempleado = Convert.ToInt64(txtCodigoEmpleadoModal.Text),
                descripcion_concepto = txtDescripcionModal.Text.ToUpper(),
            };

            DateTime fechaInicio = Convert.ToDateTime(txtFechaInicio.Text);
            DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);

            decimal valor = Convert.ToDecimal(txtValorModal.Text);

            // Verifico si el concepto a guardar es una hora extra
            bool esConceptoHorasExtras = _liquidacionServices.ConsultarSiConceptoEsHoraExtra(detalle.codigoconcepto, Usuario);
            bool esCalculoEnCantidad = chkBoxCantidadModal.Checked;

            LiquidacionNominaDetaEmpleado conceptoEmpleado = null;

            // Si no soy concepto horas extras, entro directo a calcular mi valor concepto en la funcion de la BD
            // Si soy concepto horas extras y estoy calculando las horas extras por cantidad, entonces tambien voy a la funcion de la BD para calcular el valor a pagar
            if (!esConceptoHorasExtras || (esConceptoHorasExtras && esCalculoEnCantidad))
            {
                conceptoEmpleado = _liquidacionServices.CalcularValorConceptoNominaDeUnEmpleado(detalle.codigoconcepto, detalle.codigoempleado, Convert.ToInt64(ddlTipoNomina.SelectedValue), fechaInicio, fechaFin, valor, valor, Usuario, 1);
            }
            else
            {
                // Si soy concepto horas extras y estoy calculando las horas extras por valor, simplemente lleno la entidad con los datos que ya tengo
                conceptoEmpleado = new LiquidacionNominaDetaEmpleado
                {
                    tipo = 1, // Pago
                    valorconcepto = Convert.ToDecimal(txtValorModal.Text) // Valor a pagar
                };
            }


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

            if (conceptoEmpleado.valorconcepto == 0 && valor > 0)
            {
                novedad.valorconcepto = valor;
            }
            if (conceptoEmpleado.valorconcepto > 0 && valor > 0)
            {
                novedad.valorconcepto = conceptoEmpleado.valorconcepto;
            }

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



            gvLista.DataSource = liquidacionDetalle;
            gvLista.DataBind();

            ViewState[_viewStateLiquidacion] = liquidacionDetalle;
            ViewState[_viewStateLiquidacionDetalle] = liquidacionDetalleEmpleado;
            ViewState[_viewStateLiquidacionNovedades] = liquidacionNovedades;

            bool exitoso = _liquidacionServices.CrearNovedadesNomina(liquidacionNovedades, Usuario);

            liquidacionNovedades = null;
            ViewState[_viewStateLiquidacionNovedades] = null;
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


    protected void gvNovedades_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        long idBorrar = Convert.ToInt64(e.Keys[0]);

        try
        {

            _liquidacionServices.EliminarNovedadesNomina(idBorrar, Usuario);

            btnLiquidacionPrueba_Click(btnLiquidacionPrueba, null);
        }
        catch (Exception ex)
        {
            VerError("Error al borrar el registro, " + ex.Message);
        }
        mpNovedades.Hide();
    }
    void CtlMensajeBorrar_eventoClick(object sender, EventArgs e)
    {
        if (ViewState["idBorrar"] != null)
        {
            long idBorrar = Convert.ToInt64(ViewState["idBorrar"]);

            try
            {

                _liquidacionServices.EliminarNovedadesNomina(idBorrar, Usuario);

                btnLiquidacionPrueba_Click(btnLiquidacionPrueba, null);
            }
            catch (Exception ex)
            {
                VerError("Error al borrar el registro, " + ex.Message);
            }
        }
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
            LimpiarFormularioAgregarNovedades();
            updatePanelLiquidacionGeneradas.Visible = false;

            gvLista.DataSource = null;
            gvLista.DataBind();
        }
        else if (viewActual == viewEmpleados)
        {
            ctlEmpleados.Limpiar();
        }
    }

    void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {

        View viewActual = mvPrincipal.GetActiveView();

        if (viewActual == viewPrincipal || viewActual == vFinal)
        {
            Navegar(Pagina.Lista);
        }
        else if (viewActual == viewEmpleados)
        {
            btnLiquidacionPrueba_Click(btnLiquidacionPrueba, null);

            mvPrincipal.SetActiveView(viewPrincipal);

            DeshabilitarCampos(false);

            ctlEmpleados.Limpiar();

            Site toolBar = (Site)Master;
            toolBar.MostrarConsultar(false);
            toolBar.MostrarGuardar(true);
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
        //ConsultarLiquidacion();

        VerError("");
        if (ValidarPagina())
        {
            _esPruebaLiquidacion = false;
            Session["EsPrueba"] = null;
            if (Session["EsPrueba"] == null)

                btnLiquidacionDefinitiva.BackColor = System.Drawing.Color.Red;
            btnLiquidacionPrueba.BackColor = System.Drawing.Color.FromName("#359af2");

            DeshabilitarCampos(true);

            GenerarLiquidacion();

            ViewState[_viewStateControlSeGeneroLiquidacion] = true;
            updatePanelLiquidacionGeneradas.Visible = true;

        }
    }

    protected void btnLiquidacionPrueba_Click(object sender, EventArgs e)
    {
        // ConsultarLiquidacion();


        Session["NovedadAplicada"] = null;
        VerError("");
        if (ValidarPagina())
        {
            DeshabilitarCampos(true);
            _esPruebaLiquidacion = true;
            Session["EsPrueba"] = _esPruebaLiquidacion;
            if (Session["EsPrueba"] != null)
                btnLiquidacionPrueba.BackColor = System.Drawing.Color.Red;
            btnLiquidacionDefinitiva.BackColor = System.Drawing.Color.FromName("#359af2");
            GenerarLiquidacion();

            //Site toolBar = (Site)Master;
            //toolBar.MostrarConsultar(true);
            // toolBar.MostrarImprimir(true);

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
        {
            _esPruebaLiquidacion = false;
        }


        if (_esPruebaLiquidacion == false && ValidarProcesoContable(FechaAct, tipoOpe) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 109 = Liquidación de Nómina");
            return;
        }
        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible)
        {
            rpta = ctlproceso.Inicializar(tipoOpe, FechaAct, (Usuario)Session["Usuario"]);
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
                        ctlMensajeGuardar.MostrarMensaje("Se genera la liquidación definitiva, seguro que desea continuar?");
                    }

                }
                else
                    VerError("Se presentó error");
            }
        }

        if (viewActual == viewPrincipal && _esPruebaLiquidacion == true)
        {
            VerError("");
            if (ValidarPagina())
            {

                ctlMensajeGuardarPrueba.MostrarMensaje("Se generara la liquidacion Prueba, seguro que desea continuar?");
            }
        }


        else if (viewActual == viewEmpleados)
        {
            mvPrincipal.SetActiveView(viewPrincipal);

            DeshabilitarCampos(true);

            Site toolBar = (Site)Master;
            toolBar.MostrarConsultar(false);

            List<Empleados> listaEmpleados = ctlEmpleados.ObtenerListaEmpleados();
            GenerarLiquidacion(listaEmpleados);

            ctlEmpleados.Limpiar();

            ViewState[_viewStateControlSeGeneroLiquidacion] = false;
            updatePanelLiquidacionGeneradas.Visible = true;

            _esPruebaLiquidacion = false;
            Session["EsPrueba"] = null;


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

            LiquidacionNomina liquidacion = new LiquidacionNomina
            {
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                fechainicio = Convert.ToDateTime(txtFechaInicio.Text),
                fechaterminacion = Convert.ToDateTime(txtFechaFin.Text),
                codigocentrocosto = Convert.ToInt64(ddlCentroCosto.SelectedValue)
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
            pOperacion.observacion = "Liquidación Nómina";
            pOperacion.cod_proceso = null;
            pOperacion.fecha_oper = Convert.ToDateTime(FechaAct.ToShortDateString());
            pOperacion.fecha_calc = DateTime.Now;
            pOperacion.cod_ofi = Usuario.cod_oficina;


            List<LiquidacionNominaDetalle> listaLiquidacion = ViewState[_viewStateLiquidacion] as List<LiquidacionNominaDetalle>;
            List<LiquidacionNominaDetaEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionNominaDetaEmpleado>;
            // List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedades = ViewState[_viewStateLiquidacionNovedades] as List<LiquidacionNominaNoveEmpleado>;
            List<ConceptosOpcionesLiquidados> listaConceptosOpcionesLiquidados = ViewState[_viewStateConceptosLiquidadosOpciones] as List<ConceptosOpcionesLiquidados>;
            //  List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedadescargadas = new List<LiquidacionNominaNoveEmpleado>();

            // if (gvNovedadesLista.Rows.Count > 0)
            //{
            //  listaLiquidacionNovedadescargadas = (List<LiquidacionNominaNoveEmpleado>)Session["Novedades"];
            //}         



            List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedades = _liquidacionServices.ListarLiquidacionNominaNovedadesTodas(Usuario);

            //List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedadescargadas = new List<LiquidacionNominaNoveEmpleado>();

            //if (gvNovedadesLista.Rows.Count > 0)
            //{
            //  listaLiquidacionNovedadescargadas = (List<LiquidacionNominaNoveEmpleado>)Session["Novedades"];
            //}
            List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedadescargadas = new List<LiquidacionNominaNoveEmpleado>();

            listaLiquidacionNovedadescargadas = null;


            liquidacion.estado = "D";
            liquidacion.codorigen = 1;
            bool exitoso = _liquidacionServices.CrearLiquidacionNomina(liquidacion, listaLiquidacion, listaLiquidacionDetalle, listaLiquidacionNovedades, listaLiquidacionNovedadescargadas, listaConceptosOpcionesLiquidados, Usuario, pOperacion);
            cod_operacion = pOperacion.cod_ope;
            txtCodigo.Text = Convert.ToString(liquidacion.consecutivo);

            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarImprimir(false);
            toolBar.MostrarRegresar(false);

            btnLiquidacionDefinitiva.Visible = false;
            btnLiquidacionPrueba.Visible = false;

            gvNovedades.Columns[0].Visible = false;
            gvLista.Columns[8].Visible = false;


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

    void ctlMensajeGuardarPrueba_eventoClick(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
        Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();

        try
        {
            VerError("");

            LiquidacionNomina liquidacion = new LiquidacionNomina
            {
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                fechainicio = Convert.ToDateTime(txtFechaInicio.Text),
                fechaterminacion = Convert.ToDateTime(txtFechaFin.Text),
                codigocentrocosto = Convert.ToInt64(ddlCentroCosto.SelectedValue)
            };

            bool? seGeneroLiquidacionDefinitiva = ViewState[_viewStateControlSeGeneroLiquidacion] as bool?;

            // Si anteriormente no se genero una liquidacion definitiva, la generamos nosotros antes de guardar
            if (!seGeneroLiquidacionDefinitiva.HasValue || !seGeneroLiquidacionDefinitiva.Value)
            {
                GenerarLiquidacion();
            }


            List<LiquidacionNominaDetalle> listaLiquidacion = ViewState[_viewStateLiquidacion] as List<LiquidacionNominaDetalle>;
            List<LiquidacionNominaDetaEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionNominaDetaEmpleado>;
            List<ConceptosOpcionesLiquidados> listaConceptosOpcionesLiquidados = ViewState[_viewStateConceptosLiquidadosOpciones] as List<ConceptosOpcionesLiquidados>;
            //List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedadescargadas = ViewState[_viewStateLiquidacionNovedades] as List<LiquidacionNominaNoveEmpleado>;



            List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedades = _liquidacionServices.ListarLiquidacionNominaNovedadesTodas(Usuario);
            listaLiquidacionNovedades = null;

            //List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedadescargadas = new List<LiquidacionNominaNoveEmpleado>();

            //if (gvNovedadesLista.Rows.Count > 0)
            //{
            //  listaLiquidacionNovedadescargadas = (List<LiquidacionNominaNoveEmpleado>)Session["Novedades"];
            //}
            List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedadescargadas = new List<LiquidacionNominaNoveEmpleado>();

            listaLiquidacionNovedadescargadas = null;


            liquidacion.estado = "P";
            bool exitoso = _liquidacionServices.CrearLiquidacionNomina(liquidacion, listaLiquidacion, listaLiquidacionDetalle, listaLiquidacionNovedades, listaLiquidacionNovedadescargadas, listaConceptosOpcionesLiquidados, Usuario, pOperacion);

            txtCodigo.Text = Convert.ToString(liquidacion.consecutivo);
            Site toolBar = (Site)Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarLimpiar(false);
            toolBar.MostrarImprimir(false);

            btnLiquidacionDefinitiva.Visible = false;
            btnLiquidacionPrueba.Visible = false;

            gvNovedades.Columns[0].Visible = false;
            gvLista.Columns[8].Visible = false;

            mvPrincipal.SetActiveView(viewImprimir);

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
      
      
        txtCodigo.Text = Convert.ToString(idObjeto);

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
     
        dataTable.Columns.Add("FechaIngreso");
        dataTable.Columns.Add("Eps");
        dataTable.Columns.Add("FondoPension");
        dataTable.Columns.Add("CajaCompensacion");
        dataTable.Columns.Add("Arl");
        dataTable.Columns.Add("FormaPago");
        dataTable.Columns.Add("EntidadBancaria");

        dataTable.Columns.Add("DiasLaborados");

        dataTable.Columns.Add("ValorLetras");




        // Recorro a todos los empleados que se liquidaron
        foreach (LiquidacionNominaDetalle empleado in listaEmpleadosLiquidados)
        {
            // Hallo los conceptos que se liquidaron para este empleado
            var conceptosDelEmpleado = listaLiquidacionDetalle.Where(x => x.codigoempleado == empleado.codigoempleado).ToList();

            // Hallo los pagos de esos conceptos
            List<LiquidacionNominaDetaEmpleado> listaPagos = conceptosDelEmpleado.Where(x => x.tipo == 1).ToList();
            // Hallo los descuentos de esos conceptos
            List<LiquidacionNominaDetaEmpleado> listaDescuentos = conceptosDelEmpleado.Where(x => x.tipo == 2).ToList();

            // A peticion ordenar primero sueldo y luego auxilio y luego los demas
            listaPagos = listaPagos.OrderByDescending(x => x.codigoconcepto == 39).ThenByDescending(x => x.codigoconcepto == 4).ToList();

            // A peticion ordenar primero salud y pension y luego los demas
            listaDescuentos = listaDescuentos.OrderByDescending(x => x.codigoconcepto == 1).ThenByDescending(x => x.codigoconcepto == 2).ToList();

            ConstruirReporteDesprendibleNomina(dataTable, empleado, listaPagos, listaDescuentos);
        }
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
        dataTable.Columns.Add("ValorColumnaUno");
        dataTable.Columns.Add("ValorColumnaDos");
        dataTable.Columns.Add("ValorColumnaTres");
        dataTable.Columns.Add("ValorColumnaCuatro");
        dataTable.Columns.Add("ValorColumnaCinco");
        dataTable.Columns.Add("ValorColumnaSeis");
        dataTable.Columns.Add("ValorColumnaSiete");
        dataTable.Columns.Add("ValorColumnaOcho");



        ParColumnasPlanillaLiq primeraColumna = _liquidacionServices.ConsultarParametrizacionColumnas(1, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosPrimeraColumna = null;
        if (primeraColumna.esvisible == 1)
        {
            listaConceptosPrimeraColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(1, Usuario);
        }

        ParColumnasPlanillaLiq segundaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(2, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosSegundaColumna = null;
        if (segundaColumna.esvisible == 1)
        {
            listaConceptosSegundaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(2, Usuario);
        }

        ParColumnasPlanillaLiq terceraColumna = _liquidacionServices.ConsultarParametrizacionColumnas(3, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosTerceraColumna = null;
        if (terceraColumna.esvisible == 1)
        {
            listaConceptosTerceraColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(3, Usuario);
        }

        ParColumnasPlanillaLiq cuartaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(4, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosCuartaColumna = null;
        if (cuartaColumna.esvisible == 1)
        {
            listaConceptosCuartaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(4, Usuario);
        }

        ParColumnasPlanillaLiq quintaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(5, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosQuintaColumna = null;
        if (quintaColumna.esvisible == 1)
        {
            listaConceptosQuintaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(5, Usuario);
        }


        ParColumnasPlanillaLiq sextaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(6, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosSextaColumna = null;
        if (sextaColumna.esvisible == 1)
        {
            listaConceptosSextaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(6, Usuario);
        }


        ParColumnasPlanillaLiq septimaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(7, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosSeptimaColumna = null;
        if (septimaColumna.esvisible == 1)
        {
            listaConceptosSeptimaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(7, Usuario);
        }

        ParColumnasPlanillaLiq OctavaColumna = _liquidacionServices.ConsultarParametrizacionColumnas(8, Usuario);
        List<ParConceptosPlanillaLiq> listaConceptosOctavaColumna = null;
        if (OctavaColumna.esvisible == 1)
        {
            listaConceptosOctavaColumna = _liquidacionServices.ListarConceptosParametrizadosSegunColumna(8, Usuario);
        }


        // Recorro a todos los empleados que se liquidaron
        foreach (LiquidacionNominaDetalle empleado in listaEmpleadosLiquidados)
        {
            // Hallo los conceptos que se liquidaron para este empleado
            var conceptosDelEmpleado = listaLiquidacionDetalle.Where(x => x.codigoempleado == empleado.codigoempleado).ToList();

            // Hallo los pagos de esos conceptos
            List<LiquidacionNominaDetaEmpleado> listaPagos = conceptosDelEmpleado.Where(x => x.tipo == 1).ToList();
            // Hallo los descuentos de esos conceptos
            List<LiquidacionNominaDetaEmpleado> listaDescuentos = conceptosDelEmpleado.Where(x => x.tipo == 2).ToList();

            // Hallo el valor total de esos pagos
            decimal totalValorPago = listaPagos.Sum(x => x.valorconcepto);
            // Hallo el valor total de esos descuentos
            decimal totalValorDescuentos = listaDescuentos.Sum(x => x.valorconcepto);
            // Hallo el valor total de las novedades
            decimal totalValorNovedades = empleado.valortotalapagar;

            decimal valorPrimeraColumna = 0;
            if (listaConceptosPrimeraColumna != null)
            {
                valorPrimeraColumna = conceptosDelEmpleado.Where(x => listaConceptosPrimeraColumna.Any(y => y.codigoconcepto == x.codigoconcepto)).Sum(x => x.valorconcepto);
            }

            decimal valorSegundaColumna = 0;
            if (listaConceptosSegundaColumna != null)
            {
                valorSegundaColumna = conceptosDelEmpleado.Where(x => listaConceptosSegundaColumna.Any(y => y.codigoconcepto == x.codigoconcepto)).Sum(x => x.valorconcepto);
            }

            decimal valorTerceraColumna = 0;
            if (listaConceptosTerceraColumna != null)
            {
                valorTerceraColumna = conceptosDelEmpleado.Where(x => listaConceptosTerceraColumna.Any(y => y.codigoconcepto == x.codigoconcepto)).Sum(x => x.valorconcepto);
            }

            decimal valorCuartaColumna = 0;
            if (listaConceptosCuartaColumna != null)
            {
                valorCuartaColumna = conceptosDelEmpleado.Where(x => listaConceptosCuartaColumna.Any(y => y.codigoconcepto == x.codigoconcepto)).Sum(x => x.valorconcepto);
            }

            decimal valorQuintaColumna = 0;
            if (listaConceptosQuintaColumna != null)
            {
                valorQuintaColumna = conceptosDelEmpleado.Where(x => listaConceptosQuintaColumna.Any(y => y.codigoconcepto == x.codigoconcepto)).Sum(x => x.valorconcepto);
            }


            decimal valorSextaColumna = 0;
            if (listaConceptosSextaColumna != null)
            {
                valorSextaColumna = conceptosDelEmpleado.Where(x => listaConceptosSextaColumna.Any(y => y.codigoconcepto == x.codigoconcepto)).Sum(x => x.valorconcepto);
            }

            decimal valorSeptimaColumna = 0;
            if (listaConceptosSeptimaColumna != null)
            {
                valorSeptimaColumna = conceptosDelEmpleado.Where(x => listaConceptosSeptimaColumna.Any(y => y.codigoconcepto == x.codigoconcepto)).Sum(x => x.valorconcepto);
            }

            decimal valorOctavaColumna = 0;
            if (listaConceptosOctavaColumna != null)
            {
                valorOctavaColumna = conceptosDelEmpleado.Where(x => listaConceptosOctavaColumna.Any(y => y.codigoconcepto == x.codigoconcepto)).Sum(x => x.valorconcepto);
            }

            Nomina_Entidad liquidacionNomina = _liquidacionServices.ConsultarDatosLiquidacion(Convert.ToInt64(txtCodigo.Text), Usuario, empleado.codigoempleado);



            DataRow row = dataTable.NewRow();
            row[0] = empleado.consecutivo;
            row[1] = empleado.identificacion_empleado;
            row[2] = empleado.nombre_empleado;
            row[3] = liquidacionNomina.dias_liquidar;
            row[4] = _stringHelper.FormatearNumerosComoCurrency(empleado.salario);
            row[5] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago);
            row[6] = _stringHelper.FormatearNumerosComoCurrency(totalValorDescuentos);
            row[7] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago - totalValorDescuentos);
            row[8] = _stringHelper.FormatearNumerosComoCurrency(valorPrimeraColumna);
            row[9] = _stringHelper.FormatearNumerosComoCurrency(valorSegundaColumna);
            row[10] = _stringHelper.FormatearNumerosComoCurrency(valorTerceraColumna);
            row[11] = _stringHelper.FormatearNumerosComoCurrency(valorCuartaColumna);
            row[12] = _stringHelper.FormatearNumerosComoCurrency(valorQuintaColumna);
            row[13] = _stringHelper.FormatearNumerosComoCurrency(valorSextaColumna);
            row[14] = _stringHelper.FormatearNumerosComoCurrency(valorSeptimaColumna);
            row[15] = _stringHelper.FormatearNumerosComoCurrency(valorOctavaColumna);

            dataTable.Rows.Add(row);
        }

        // Hallo la cantidad de registros liquidados (Empleados liquidados)
        long totalizadoRegistros = listaEmpleadosLiquidados.Count;
        // Hallo el valor total de esos pagos
        decimal totalizadoValorPago = listaLiquidacionDetalle.Where(x => x.tipo == 1).Sum(x => x.valorconcepto);
        // Hallo el valor total de esos descuentos
        decimal totalizadoValorDescuentos = listaLiquidacionDetalle.Where(x => x.tipo == 2).Sum(x => x.valorconcepto);
        // Hallo el valor total de las novedades
        decimal totalizadoValorNovedades = totalizadoValorPago - totalizadoValorDescuentos;

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
                new ReportParameter("MostrarColumnaUno", primeraColumna.esvisible.ToString()),
                new ReportParameter("MostrarColumnaDos", segundaColumna.esvisible.ToString()),
                new ReportParameter("MostrarColumnaTres", terceraColumna.esvisible.ToString()),
                new ReportParameter("MostrarColumnaCuatro", cuartaColumna.esvisible.ToString()),
                new ReportParameter("NombreColumnaUno", primeraColumna.nombrecolumna),
                new ReportParameter("NombreColumnaDos", segundaColumna.nombrecolumna),
                new ReportParameter("NombreColumnaTres", terceraColumna.nombrecolumna),
                new ReportParameter("NombreColumnaCuatro", cuartaColumna.nombrecolumna),
                new ReportParameter("NombreColumnaCinco", quintaColumna.nombrecolumna),
                new ReportParameter("NombreColumnaSeis", sextaColumna.nombrecolumna),
                new ReportParameter("NombreColumnaSiete", septimaColumna.nombrecolumna),
                new ReportParameter("NombreColumnaOcho", OctavaColumna.nombrecolumna),
                new ReportParameter("ImagenReport", ImagenReporte()),
                 new ReportParameter("pElaborado", HttpUtility.HtmlDecode(vacios(txtusuariocreacion.Text))),
                new ReportParameter("pAprobado", HttpUtility.HtmlDecode(vacios(Usuario.representante_legal)))




    };

        rvReportePlanilla.LocalReport.EnableExternalImages = true;
        rvReportePlanilla.LocalReport.ReportPath = @"Page\Nomina\LiquidacionNomina\PlanillaNomina.rdlc";
        rvReportePlanilla.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("PlanillaNominaDataSet", dataTable);
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


    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnGenerarArchivos_Click(object sender, EventArgs e)
    {
        VerError("");
        lblmsjCarga.Text = "";

        mvPrincipal.Visible = false;
        mvCargar.Visible = true;
        mvCargar.ActiveViewIndex = 0;
        Site toolbar = (Site)Master;
        toolbar.MostrarGuardar(false);
        toolbar.MostrarImprimir(false);
        toolbar.MostrarExportar(false);
    }
    protected void btnAceptarCarga_Click(object sender, EventArgs e)
    {
        VerError("");
        try
        {
            lblmsjCarga.Text = "";
            if (flpArchivo.HasFile)
            {
                String fileName = Path.GetFileName(this.flpArchivo.PostedFile.FileName);
                String extension = Path.GetExtension(this.flpArchivo.PostedFile.FileName).ToLower();
                if (extension != ".txt")
                {
                    lblmsjCarga.Text = "Para realizar la carga de Archivo solo debe seleccionar un archivo de texto";
                    return;
                }

                List<LiquidacionNominaNoveEmpleado> lstDatosCarga = new List<LiquidacionNominaNoveEmpleado>();

                //CARGANDO DATOS AL LISTADO POR SI EXISTEN EN LA GRIDVIEW
                // lstDatosCarga = ObtenerLista();

                string readLine;
                StreamReader strReader;
                Stream stream = flpArchivo.FileContent;

                string ErrorCargue = "";

                //ARCHIVO DE TEXTO
                using (strReader = new StreamReader(stream))
                {
                    while (strReader.Peek() >= 0)
                    {
                        //PASANDO LA FILA DEL ARCHIVO A LA VARIABLE
                        readLine = strReader.ReadLine();
                        if (readLine != "")
                        {
                            //SEPARANDO CADA CAMPO
                            if (readLine.Contains("|") == false)
                            {
                                lblmsjCarga.Text = "El Archivo cargado no contiene los separadores correctos, verifique los datos ( Separador correcto : | )";
                                return;
                            }
                            string[] arrayline = readLine.Split('|');
                            int contadorreg = 0;



                            LiquidacionNominaNoveEmpleado novedadEnt = new LiquidacionNominaNoveEmpleado();
                            //INICIAR LA LECTURA DE DATOS DE LA PRIMERA LINEA
                            int posicionInicial = 1;
                            foreach (string variable in arrayline)
                            {
                                if (posicionInicial >= 0)
                                {
                                    if (variable != null)
                                    {
                                        if (contadorreg == 0) //IDENTIFICACIÓN EMPLEADO
                                        {
                                            novedadEnt.identificacion = variable.ToUpper().Trim();
                                        }

                                        Empleados empleado = _empleadoService.ConsultarInformacionPersonaEmpleadoPorIdentificacion(novedadEnt.identificacion, Usuario);

                                        //  if (contadorreg ==1) //CODIGO EMPLEADO
                                        //{
                                        novedadEnt.codigoempleado = Convert.ToInt32(empleado.consecutivo);
                                        novedadEnt.nombre = Convert.ToString(empleado.nombre);

                                        //}
                                        if (contadorreg == 1) //CONCEPTO
                                        {
                                            novedadEnt.codigoconcepto = Convert.ToInt32(variable.Trim());
                                        }
                                        if (novedadEnt.codigoconcepto > 0)
                                        {

                                            ConceptosNomina conceptos = _conceptosService.ConsultarConceptoNomina(Usuario, Convert.ToString(novedadEnt.codigoconcepto));

                                            //if (contadorreg == 3) //DESCRIPCION DE LA NOVEDAD
                                            //{
                                            novedadEnt.descripcion = conceptos.DESCRIPCION.ToUpper().Trim();
                                            //}
                                        }
                                        if (contadorreg == 2) //VALOR CONCEPTO
                                        {
                                            novedadEnt.valorconcepto = Convert.ToDecimal(variable.Trim());
                                        }

                                        novedadEnt.codigocentrocosto = Convert.ToInt32(empleado.codigocentrocosto);
                                        novedadEnt.codigonomina = Convert.ToInt32(empleado.codigonomina);

                                        if (contadorreg == 3) //TIPO CONCEPTO
                                        {

                                            novedadEnt.tipo = Convert.ToInt32(variable.Trim());

                                        }


                                    }
                                }
                                contadorreg++;

                            }
                            lstDatosCarga.Add(novedadEnt);


                        }
                    }
                }

                if (ErrorCargue != "")
                    VerError("Han surgido problemas al cargar los siguientes detalles: <br>" + ErrorCargue);

                Session["Novedades"] = lstDatosCarga;

                gvNovedadesLista.DataSource = lstDatosCarga;
                gvNovedadesLista.DataBind();
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstDatosCarga.Count.ToString();

                mvCargar.Visible = true;
                mvCargar.ActiveViewIndex = 0;



                Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();

                try
                {
                    VerError("");

                    LiquidacionNomina liquidacion = new LiquidacionNomina
                    {
                        codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                        fechainicio = Convert.ToDateTime(txtFechaInicio.Text),
                        fechaterminacion = Convert.ToDateTime(txtFechaFin.Text),
                        codigocentrocosto = Convert.ToInt64(ddlCentroCosto.SelectedValue)
                    };

                    bool? seGeneroLiquidacionDefinitiva = ViewState[_viewStateControlSeGeneroLiquidacion] as bool?;

                    // Si anteriormente no se genero una liquidacion definitiva, la generamos nosotros antes de guardar
                    if (!seGeneroLiquidacionDefinitiva.HasValue || !seGeneroLiquidacionDefinitiva.Value)
                    {
                        GenerarLiquidacion();
                    }




                    List<LiquidacionNominaDetalle> listaLiquidacion = ViewState[_viewStateLiquidacion] as List<LiquidacionNominaDetalle>;
                    List<LiquidacionNominaDetaEmpleado> listaLiquidacionDetalle = ViewState[_viewStateLiquidacionDetalle] as List<LiquidacionNominaDetaEmpleado>;
                    List<ConceptosOpcionesLiquidados> listaConceptosOpcionesLiquidados = ViewState[_viewStateConceptosLiquidadosOpciones] as List<ConceptosOpcionesLiquidados>;
                    //List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedadescargadas = ViewState[_viewStateLiquidacionNovedades] as List<LiquidacionNominaNoveEmpleado>;
                    listaLiquidacionDetalle = null;
                    listaConceptosOpcionesLiquidados = null;

                    List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedades = _liquidacionServices.ListarLiquidacionNominaNovedadesTodas(Usuario);


                    List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedadescargadas = new List<LiquidacionNominaNoveEmpleado>();

                    if (gvNovedadesLista.Rows.Count > 0)
                    {
                        listaLiquidacionNovedadescargadas = (List<LiquidacionNominaNoveEmpleado>)Session["Novedades"];
                    }



                    liquidacion.estado = "P";
                    bool exitoso = _liquidacionServices.CrearLiquidacionNomina(liquidacion, listaLiquidacion, listaLiquidacionDetalle, listaLiquidacionNovedades, listaLiquidacionNovedadescargadas, listaConceptosOpcionesLiquidados, Usuario, pOperacion);
                }
                catch (Exception ex)
                {
                    VerError("Error al guardar la liquidacion de nomina, " + ex.Message);
                }
                //  btnDetalle.Visible = true;
                Site toolbar = (Site)Master;
                toolbar.MostrarGuardar(true);
                toolbar.MostrarImprimir(true);
                toolbar.MostrarExportar(true);

            }
            else
            {
                lblmsjCarga.Text = "Seleccione un Archivo para realizar la carga de datos.";
            }
        }
        catch (Exception ex)
        {
            VerError("ERROR: " + ex.Message);
        }
    }

    protected void btnCancelarCarga_Click(object sender, EventArgs e)
    {
        mvPrincipal.Visible = true;
        mvPrincipal.ActiveViewIndex = 0;

        Site toolbar = (Site)Master;
        toolbar.MostrarGuardar(true);
        toolbar.MostrarImprimir(true);
        toolbar.MostrarExportar(true);
    }


    protected void btnCargarDatos_Click(object sender, EventArgs e)
    {
        VerError("");
        lblmsjCarga.Text = "";
        mvPrincipal.Visible = false;
        mvCargar.Visible = true;
        mvCargar.ActiveViewIndex = 0;
        Site toolbar = (Site)Master;
        toolbar.MostrarGuardar(false);
        toolbar.MostrarImprimir(false);
        toolbar.MostrarExportar(false);
        toolbar.MostrarRegresar(false);
        toolbar.MostrarCancelar(false);
        PanelEncabezado.Enabled = true;
        PanelEncabezado.Visible = false;
        //  btnCargarDatos_Click(btnCargarDatos, null);
    }

    protected void btnRegresarNovedad_Click(object sender, EventArgs e)
    {
        btnLiquidacionPrueba_Click(btnLiquidacionPrueba, null);
        mvCargar.Visible = false;
        mvPrincipal.Visible = true;
        mvPrincipal.ActiveViewIndex = 0;
    }

    protected void chkBoxValorModal_CheckedChanged(object sender, EventArgs e)
    {

    }


    #endregion


    #region Eventos Text Changed


    protected void txtFechaLiquidacion_TextChanged(object sender, EventArgs e)
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
        if (ddlTipoNomina.SelectedValue != " ")
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
        txtNomina.Text = Convert.ToString(_consecutivoLiquidacion.Value);
        Session["NovedadAplicada"] = 1;

        LiquidacionNomina liquidacionNomina = _liquidacionServices.ConsultarLiquidacionNomina(liquidacion.consecutivo, Usuario);
        List<LiquidacionNominaDetalle> liquidacionNominaDetalles = _liquidacionServices.ListarLiquidacionNominaDetalle(liquidacion, Usuario);
        List<LiquidacionNominaDetaEmpleado> liquidacionNominaConceptos = _liquidacionServices.ListarLiquidacionNominaConceptos(liquidacion, Usuario);

        List<LiquidacionNominaNoveEmpleado> liquidacionNominaNovedades = _liquidacionServices.ListarLiquidacionNominaNovedadesTodasAplicadas(Usuario);

        ViewState[_viewStateLiquidacion] = liquidacionNominaDetalles;
        ViewState[_viewStateLiquidacionDetalle] = liquidacionNominaConceptos;
        ViewState[_viewStateLiquidacionNovedades] = liquidacionNominaNovedades;

        gvLista.DataSource = liquidacionNominaDetalles;
        gvLista.DataBind();

        txtFechaInicio.Text = liquidacionNomina.fechainicio.ToShortDateString();
        txtFechaFin.Text = liquidacionNomina.fechaterminacion.ToShortDateString();
        ddlTipoNomina.SelectedValue = liquidacionNomina.codigonomina.ToString();
        ddlCentroCosto.SelectedValue = liquidacionNomina.codigocentrocosto.ToString();
        txtusuariocreacion.Text = liquidacionNomina.usuariocreacion.ToString(); 
        /*string estado= liquidacionNomina.estado.ToString();
         DeshabilitarCampos(true);

        if(estado=="P")
         {
         Site toolBar = (Site)Master;        
         toolBar.MostrarGuardar(true);
          btnLiquidacionDefinitiva.Visible = true;
          btnLiquidacionPrueba.Visible = true;
          btnLiquidacionPrueba_Click(btnLiquidacionPrueba,null);
          }
         */

    }


    void GenerarLiquidacion(List<Empleados> listaEmpleados = null)
    {
        try
        {
            LiquidacionNomina liquidacion = new LiquidacionNomina
            {
                codigonomina = Convert.ToInt64(ddlTipoNomina.SelectedValue),
                fechainicio = Convert.ToDateTime(txtFechaInicio.Text),
                fechaterminacion = Convert.ToDateTime(txtFechaFin.Text)
            };

            Tuple<List<LiquidacionNominaDetalle>, List<LiquidacionNominaDetaEmpleado>, List<ConceptosOpcionesLiquidados>> listaLiquidacion = _liquidacionServices.GenerarLiquidacion(liquidacion, listaEmpleados, Usuario);

            gvLista.DataSource = listaLiquidacion.Item1;
            gvLista.DataBind();

            ViewState[_viewStateLiquidacion] = listaLiquidacion.Item1;
            ViewState[_viewStateLiquidacionDetalle] = listaLiquidacion.Item2;
            ViewState[_viewStateConceptosLiquidadosOpciones] = listaLiquidacion.Item3;
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
            VerError("Debe llenar la fecha de terminacion del periodo!.");
            return false;
        }

     
  
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

    void DeshabilitarCampos(bool deshabilitar)
    {
        txtFechaInicio.ReadOnly = deshabilitar;
        txtFechaFin.ReadOnly = deshabilitar;
        ddlCentroCosto.Enabled = !deshabilitar;
        ddlTipoNomina.Enabled = !deshabilitar;
        CalendarExtender7.Enabled = !deshabilitar;
        CalendarExtender1.Enabled = !deshabilitar;
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


    void ConsultarUltFechaLiquidacion()
    {
        if (ddlTipoNomina.SelectedValue != " ")
        {
            _codnomina = Convert.ToInt32(ddlTipoNomina.SelectedValue.ToString());
            LiquidacionNomina liquidacionNomina = _liquidacionServices.ConsultarUltimaFechaLiquidacionNomina(_codnomina, Usuario);
            _fechaultliquidacion = liquidacionNomina.fechaultliquidacion;

            _fechainicio = Convert.ToDateTime(liquidacionNomina.fechainicio);
            txtFechaInicio.Text = _fechainicio.ToShortDateString();

            _fechafin = Convert.ToDateTime(liquidacionNomina.fechaterminacion);
            txtFechaFin.Text = _fechafin.ToShortDateString();
        }



    }


    #endregion


    #region Metodos Reporte


    void ConstruirReporteDesprendibleNomina(DataTable dataTable, LiquidacionNominaDetalle empleado, List<LiquidacionNominaDetaEmpleado> listaPagos, List<LiquidacionNominaDetaEmpleado> listaDescuentos)
    {
        // Hallo el valor total de esos pagos
        decimal totalValorPago = listaPagos.Sum(x => x.valorconcepto);
        // Hallo el valor total de esos descuentos
        decimal totalValorDescuentos = listaDescuentos.Sum(x => x.valorconcepto);

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
            LiquidacionNominaDetaEmpleado pago = listaPagos.ElementAtOrDefault(i);
            // Sacamos el descuento de este indice
            LiquidacionNominaDetaEmpleado descuentos = listaDescuentos.ElementAtOrDefault(i);

            // Creamos el row default para este empleado (Llenamos el row con la informacion que siempre es la misma, la usada para hacer el group)
            DataRow row = ConstruirDataRowDefaultDesprendibleDeNominaParaUnEmpleado(dataTable, empleado, totalValorPago, totalValorDescuentos);

            row[7] = i; // Solo es usado como muletilla para el grouping en el .rdlc

            // Si efectivamente sacamos un pago en este indice, lo lleno
            if (pago != null)
            {
                row[8] = pago.descripcion_concepto;
                row[9] = _stringHelper.FormatearNumerosComoCurrency(pago.valorconcepto);
            }
            else
            {
                row[8] = string.Empty;
                row[9] = string.Empty;
            }

            // Si efectivamente sacamos un descuento en este indice, lo lleno
            if (descuentos != null)
            {
                row[10] = descuentos.descripcion_concepto;
                row[11] = _stringHelper.FormatearNumerosComoCurrency(descuentos.valorconcepto);
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
                new ReportParameter("FechaPeriodicaInicio", txtFechaInicio.Text),
                new ReportParameter("FechaPeriodicaFinal", txtFechaFin.Text),
                new ReportParameter("Nomina", ddlTipoNomina.SelectedItem.Text),
                new ReportParameter("FechaPago", txtFechaFin.Text),
                new ReportParameter("ImagenReport", ImagenReporte()),
                new ReportParameter("pElaborado", HttpUtility.HtmlDecode(vacios(txtusuariocreacion.Text))),
                new ReportParameter("pAprobado", HttpUtility.HtmlDecode(vacios(Usuario.representante_legal)))
        };
      


        rvReporteDesprendible.LocalReport.EnableExternalImages = true;
        rvReporteDesprendible.LocalReport.ReportPath = @"Page\Nomina\LiquidacionNomina\DesprendibleNomina.rdlc";
        rvReporteDesprendible.LocalReport.SetParameters(param);

        ReportDataSource rds = new ReportDataSource("DataSetEmpleado", dataTable);
        rvReporteDesprendible.LocalReport.DataSources.Clear();
        rvReporteDesprendible.LocalReport.DataSources.Add(rds);
        rvReporteDesprendible.LocalReport.Refresh();

        pnlReporte.Visible = true;
        rvReporteDesprendible.Visible = true;
        rvReportePlanilla.Visible = false;
    }

    DataRow ConstruirDataRowDefaultDesprendibleDeNominaParaUnEmpleado(DataTable dataTable, LiquidacionNominaDetalle empleado, decimal totalValorPago, decimal totalValorDescuentos)
    {
        Decimal valorapagar = 0;
        string Valor = "";
            string pTexto="";
        Nomina_Entidad liquidacionNomina = _liquidacionServices.ConsultarDatosLiquidacion(Convert.ToInt64(txtCodigo.Text), Usuario,empleado.codigoempleado);

        DataRow row = dataTable.NewRow();
        row[0] = empleado.nombre_empleado;
        row[1] = empleado.identificacion_empleado;
        row[2] = empleado.desc_cargo;
        row[3] = _stringHelper.FormatearNumerosComoCurrency(empleado.salario);
        row[4] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago);
        row[5] = _stringHelper.FormatearNumerosComoCurrency(totalValorDescuentos);
        row[6] = _stringHelper.FormatearNumerosComoCurrency(totalValorPago - totalValorDescuentos);   
        row[12] = liquidacionNomina.Fecha_ingreso;
        row[13] = liquidacionNomina.fondosalud;
        row[14] = liquidacionNomina.fondopension;
        row[15] = liquidacionNomina.cajacompensacion;
        row[16] = liquidacionNomina.arl;
        row[17] = liquidacionNomina.formapago;
        row[18] = liquidacionNomina.entidadpago;
        row[19] = liquidacionNomina.dias_liquidar;

        //Valor en letras
        valorapagar = Convert.ToDecimal(totalValorPago-totalValorDescuentos);
       
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
        row[20] = pTexto;






        return row;
   

    }


    #endregion







    Decimal suma = 0;

    Decimal sumasalarios = 0;
    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if (e.Row.RowType == DataControlRowType.DataRow)
        {
            suma += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "valortotalapagar"));
            sumasalarios += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "salario"));
        }
        
       if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total:";
            e.Row.Cells[5].Text = suma.ToString("c");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;
            
            e.Row.Cells[3].Text = sumasalarios.ToString("c");
            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Font.Bold = true;

        }
        
     
    }
}
   