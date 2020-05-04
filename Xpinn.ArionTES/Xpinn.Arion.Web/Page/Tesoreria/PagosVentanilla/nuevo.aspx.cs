using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Configuration;
using System.Collections;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Drawing.Printing;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;
using Xpinn.Util;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using Xpinn.TarjetaDebito.Services;
using Xpinn.Interfaces.Entities;
using Newtonsoft.Json;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Asesores.Entities;
using System.Net;
using System.ComponentModel;

public partial class Nuevo : GlobalWeb
{
    private readonly string _tipoOperacionRetiroEnpacto = "1";
    private readonly string _tipoOperacionDepositoEnpacto = "3";
    private string _convenio = "";
    Int64 tipoproducto = 0;
    Int64 contador = 0;


    private Xpinn.Tesoreria.Services.PagosVentanillaService ventanillaServicio = new Xpinn.Tesoreria.Services.PagosVentanillaService();
    private Xpinn.Caja.Services.DetallePagService DetallePagoService = new Xpinn.Caja.Services.DetallePagService();
    private Xpinn.Caja.Services.CiudadService CiudadServicio = new Xpinn.Caja.Services.CiudadService();
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    private Xpinn.Programado.Services.CuentasProgramadoServices cuentasProgramado = new Xpinn.Programado.Services.CuentasProgramadoServices();
    private Xpinn.CDATS.Services.LiquidacionCDATService LiquiService = new Xpinn.CDATS.Services.LiquidacionCDATService();
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    private Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
    private Xpinn.Servicios.Services.CierreHistorioService ServiceServicio = new Xpinn.Servicios.Services.CierreHistorioService();
    private Xpinn.Asesores.Services.DetalleProductoService DetalleProducto = new Xpinn.Asesores.Services.DetalleProductoService();
    private Xpinn.Caja.Services.TransaccionCajaService tranCajaServicio = new Xpinn.Caja.Services.TransaccionCajaService();
    private Xpinn.Caja.Entities.TransaccionCaja tranCaja = new Xpinn.Caja.Entities.TransaccionCaja();
    private Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
    RealizacionGirosServices RealizacionService = new RealizacionGirosServices();

    decimal ValTotalFPago = 0;
    decimal ValTotalTran = 0;
    decimal ValTotalTranfinal = 0;
    DateTime fecha;
    DateTime fechatransaccion;
    Usuario user = new Usuario();
    Int16 nActiva = 0;
    string refe = "";
    string refe2 = "";
    string pNomUsuario;
    string pNomUsuarios;
    bool existe;
    string respuesta;
    string error;
    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(ventanillaServicio.CodigoPrograma, "A");
            if (txtValTransac.Text == "0")
                txtValTransac.Text = "";
            Site toolBar = (Site)this.Master;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ventanillaServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _convenio = ConvenioTarjeta();
            if (!IsPostBack)
            {

                Session["Opcion"] = null;
                divDatos.Visible = false;
                mvOperacion.Visible = false;
                txtValTotalCheque.Visible = false;
                lblValTotalCheque.Visible = false;
                bancochquevacio.Text = "";
                numchequevacio.Text = "";
                valorchequevacio.Text = "";
                string ip = Request.ServerVariables["REMOTE_ADDR"];
                Session["cod_ope"] = null;
                Session["val"] = 0;
                ObtenerDatos();
                user = (Usuario)Session["usuario"];

                AsignarEventoConfirmar();

                // Crea los DATATABLES para registrar las transacciones, los cheques
                CrearTablaTran();
                CrearTablaCheque();

                // Llenar los DROPDOWNLIST de tipos de monedas, tipos de identificaciòn, formas de pago y entidades bancarias
                LlenarComboTipoProducto(ddlTipoProducto);//se carga los tipos de transaccion
                LlenarComboMonedas(ddlMonedas);// se carga el primer combo de monedas en Transaccion
                LlenarComboMonedas(ddlMoneda);// se carga el segundo combo de moneda en Forma de Pago
                LlenarComboMonedas(ddlMonCheque);// se carga el tercer combo de moneda en Cheques
                LlenarComboTipoIden(ddlTipoIdentificacion);// se carga el segundo combo de moneda en Forma de Pago
                LlenarComboFormaPago(ddlFormaPago);// se carga el combo para las Formas de Pago
                LlenarComboBancos(ddlBancos);
                LlenarComboTipoIden(ddlTipoDocPersona);

                // Crea el DATATABLE para poder registrar los valores por cada tipo de moneda y forma de pago
                CrearTablaFormaPago();

                mvOperacion.ActiveViewIndex = 0;
                if (txtValTransac.Text == "0")
                    txtValTransac.Text = "";

            }
            if (txtValTransac.Text == "0")
                txtValTransac.Text = "";

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ventanillaServicio.GetType().Name + "A", "Page_Load", ex);
        }

    }
    /// <summary>
    /// LLenar el combo de los tipos de transacciones segùn el tipo de producto
    /// </summary>
    /// <param name="ddlTipoTransaccion"></param>
    protected void LlenarComboTipoProducto(DropDownList ddlTipoProducto)
    {
        Xpinn.Caja.Services.TipoOperacionService tipoopeservices = new Xpinn.Caja.Services.TipoOperacionService();

        // Inicializar las variables        
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];
        List<Xpinn.Caja.Entities.TipoOperacion> lsttipo = new List<Xpinn.Caja.Entities.TipoOperacion>();

        // Cargando listado de tipos de productos
        lsttipo = tipoopeservices.ListarTipoProducto(usuario);
        Xpinn.Caja.Entities.TipoOperacion todos = new Xpinn.Caja.Entities.TipoOperacion();

        //Se agrega consulta de parametro general para que se salga opcion pago total productos, Daniel Arevalo 21/08/2019

        Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
        pData = ConsultaData.ConsultarGeneral(90176, (Usuario)Session["usuario"]);
        if(pData.codigo>0)
        {
            Int64 valor = Convert.ToInt64(pData.valor);

            if (valor == 1)
            {
                todos.nom_tipo_producto = "Pago Total Productos";
                todos.tipo_producto = 100;
                lsttipo.Add(todos);
            }

        }
    

        ddlTipoProducto.DataTextField = "nom_tipo_producto";
        ddlTipoProducto.DataValueField = "tipo_producto";
        ddlTipoProducto.DataSource = lsttipo;
        ddlTipoProducto.DataBind();


        // Seleccionando tipo de producto por defecto y cargandolo
        try
        {
            ddlTipoProducto.SelectedValue = "2";
            Session["tipoproducto"] = Convert.ToInt64(ddlTipoProducto.SelectedValue);
            LlenarComboTipoPago(Convert.ToInt64(ddlTipoProducto.SelectedValue));
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    //si el credito esta en mora
    private void LlenarComboTipoPagoRotativo1(Int64 ptipo_producto)
    {
        ddlTipoPago.Items.Clear();
        try
        {
            Xpinn.Caja.Services.TipoOperacionService tipOpeServicio = new Xpinn.Caja.Services.TipoOperacionService();
            Xpinn.Caja.Entities.TipoOperacion tipOpe = new Xpinn.Caja.Entities.TipoOperacion();
            tipOpe.tipo_producto = ptipo_producto;
            tipOpe.tipo_movimiento = Convert.ToInt64(ddlTipoMovimiento.SelectedValue);
            ddlTipoPago.DataSource = tipOpeServicio.ListarTipoOpeTransacVentRotativo1(tipOpe, (Usuario)Session["usuario"]);
            ddlTipoPago.DataTextField = "nombre";
            ddlTipoPago.DataValueField = "cod_operacion";
            ddlTipoPago.DataBind();
            ddlTipoPago.SelectedIndex = 1;
        }
        catch
        {
        }
    }
    protected void LlenarComboMonedas(DropDownList ddlMonedas)
    {

        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, usuario);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }
    protected void LlenarComboBancos(DropDownList ddlBancos)
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];
        ddlBancos.DataSource = bancoService.ListarBancos(banco, usuario);
        ddlBancos.DataTextField = "nombrebanco";
        ddlBancos.DataValueField = "cod_banco";
        ddlBancos.DataBind();
        ddlBancos.Items.Insert(0, new ListItem("Seleccione un Banco", "0"));

        ddlBancoConsignacion.DataSource = bancoService.ListarCuentaBancaria_Bancos(usuario);
        ddlBancoConsignacion.DataTextField = "nombrebanco";
        ddlBancoConsignacion.DataValueField = "ctabancaria";
        ddlBancoConsignacion.DataBind();
        ddlBancoConsignacion.Items.Insert(0, new ListItem("Seleccione una Cuenta", "0"));
    }
    protected void LlenarComboTipoIden(DropDownList ddlTipoIdentificacion)
    {

        Xpinn.Caja.Services.TipoIdenService IdenService = new Xpinn.Caja.Services.TipoIdenService();
        Xpinn.Caja.Entities.TipoIden identi = new Xpinn.Caja.Entities.TipoIden();
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];
        ddlTipoIdentificacion.DataSource = IdenService.ListarTipoIden(identi, usuario);
        ddlTipoIdentificacion.DataTextField = "descripcion";
        ddlTipoIdentificacion.DataValueField = "codtipoidentificacion";
        ddlTipoIdentificacion.DataBind();
    }
    /// <summary>
    /// LLenar combo de tipos de pago
    /// </summary>
    /// <param name="ddlFormaPago"></param>
    protected void LlenarComboFormaPago(DropDownList ddlFormaPago)
    {

        Xpinn.Caja.Services.TipoPagoService pagoService = new Xpinn.Caja.Services.TipoPagoService();
        Xpinn.Caja.Entities.TipoPago paguei = new Xpinn.Caja.Entities.TipoPago();
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["Usuario"];

        // Adicionando para las consignaciones
        List<Xpinn.Caja.Entities.TipoPago> lstTipoPago = new List<Xpinn.Caja.Entities.TipoPago>();
        lstTipoPago = pagoService.ListarTipoPago(paguei, usuario);
        Xpinn.Caja.Entities.TipoPago pTipoPagoConsig = new Xpinn.Caja.Entities.TipoPago();
        pTipoPagoConsig.cod_tipo_pago = 5;
        pTipoPagoConsig.descripcion = "CONSIGNACION EN CUENTA";
        lstTipoPago.Add(pTipoPagoConsig);

        var lstProductos = (from p in lstTipoPago
                            orderby p.cod_tipo_pago
                            select new { p.cod_tipo_pago, p.descripcion }).ToList();

        ddlFormaPago.DataSource = lstProductos;
        ddlFormaPago.DataTextField = "descripcion";
        ddlFormaPago.DataValueField = "cod_tipo_pago";
        ddlFormaPago.DataBind();

        // Llenando parametros de proceso contable
        Xpinn.Contabilidad.Services.ProcesoContableService procesoContable = new Xpinn.Contabilidad.Services.ProcesoContableService();
        Xpinn.Contabilidad.Entities.ProcesoContable eproceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
        eproceso.tipo_ope = 2;
        ddlProcesoContable.DataSource = procesoContable.ListarProcesoContable(eproceso, usuario);
        ddlProcesoContable.DataTextField = "cod_cuenta";
        ddlProcesoContable.DataValueField = "cod_proceso";
        ddlProcesoContable.DataBind();
    }
    /// <summary>
    /// LLena el combo de tipo de pago dependiendo del tipo de producto seleccionado
    /// </summary>
    /// <param name="ptipo_producto"></param>
    private void LlenarComboTipoPago(Int64 ptipo_producto)
    {
        ddlTipoPago.Items.Clear();
        try
        {
            Xpinn.Caja.Services.TipoOperacionService tipOpeServicio = new Xpinn.Caja.Services.TipoOperacionService();
            Xpinn.Caja.Entities.TipoOperacion tipOpe = new Xpinn.Caja.Entities.TipoOperacion();
            tipOpe.tipo_producto = ptipo_producto;
            tipOpe.tipo_movimiento = Convert.ToInt64(ddlTipoMovimiento.SelectedValue);
            ddlTipoPago.DataSource = tipOpeServicio.ListarTipoOpeTransacVent(tipOpe, (Usuario)Session["usuario"]);
            ddlTipoPago.DataTextField = "nombre";
            ddlTipoPago.DataValueField = "cod_operacion";
            ddlTipoPago.DataBind();
            ddlTipoPago.SelectedIndex = 1;
        }
        catch
        {
        }
    }
    /// <summary>
    /// LLena el combo de tipo de pago dependiendo del tipo de producto seleccionado
    /// </summary>
    /// <param name="ptipo_producto"></param>
    private void LlenarComboTipoPagoRotativo(Int64 ptipo_producto)
    {
        ddlTipoPago.Items.Clear();
        try
        {
            Xpinn.Caja.Services.TipoOperacionService tipOpeServicio = new Xpinn.Caja.Services.TipoOperacionService();
            Xpinn.Caja.Entities.TipoOperacion tipOpe = new Xpinn.Caja.Entities.TipoOperacion();
            tipOpe.tipo_producto = ptipo_producto;
            tipOpe.tipo_movimiento = Convert.ToInt64(ddlTipoMovimiento.SelectedValue);
            ddlTipoPago.DataSource = tipOpeServicio.ListarTipoOpeTransacVentRotativo(tipOpe, (Usuario)Session["usuario"]);
            ddlTipoPago.DataTextField = "nombre";
            ddlTipoPago.DataValueField = "cod_operacion";
            ddlTipoPago.DataBind();
            ddlTipoPago.SelectedIndex = 1;
        }
        catch
        {
        }
    }
    /// <summary>
    /// Cancelar y salir de la opción y regresar al menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (mvOperacion.ActiveViewIndex > 0)
        {
            mvOperacion.ActiveViewIndex -= 1;
            btnGuardar.Visible = true;
            btnGuardar2.Visible = false;
        }
        else
            Navegar("../../../General/Global/inicio.aspx");
    }
    /// <summary>
    /// Muestra los datos iniciales en pantalla
    /// </summary>
    protected void ObtenerDatos()
    {
        Configuracion conf = new Configuracion();
        try
        {
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            txtFechaReal.Text = System.DateTime.Now.ToString();
            txtFechaTransaccion.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
            txtFechaCont.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
            txtfechacorte.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
            if (!string.IsNullOrEmpty(pUsuario.nombre_oficina))
                txtOficina.Text = pUsuario.nombre_oficina;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ventanillaServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }
    /// <summary>
    /// Mètodo para crear un DATATABLE con la informaciòn de las transacciones a pagar
    /// </summary>
    protected void CrearTablaTran()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("tipo");         // codigo de tipo de transaccion
        dt.Columns.Add("tproducto");    // codigo de tipo de producto
        dt.Columns.Add("nroRef");       // nùmero del producto
        dt.Columns.Add("valor");
        dt.Columns.Add("moneda");
        dt.Columns.Add("tipopago");
        dt.Columns.Add("nomtipo");      // nombre tipo transaccion 
        dt.Columns.Add("nommoneda");
        dt.Columns.Add("tipomov");      // tipo de movimiento
        dt.Columns.Add("nomtproducto");
        dt.Columns.Add("codtipopago");
        dt.Columns.Add("referencia");   // documento_soporte
        dt.Columns.Add("idavance");     // números de los avances a aplicar
        gvTransacciones.DataSource = dt;
        gvTransacciones.DataBind();
        gvTransacciones.Visible = false;
        Session["tablaSesion"] = dt;
    }


    protected void CrearTablaFormaPago()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("moneda");
        dt.Columns.Add("fpago");
        dt.Columns.Add("valor");
        dt.Columns.Add("nommoneda");
        dt.Columns.Add("nomfpago");
        dt.Columns.Add("tipomov");
        dt.Columns.Add("cod_banco");
        dt.Columns.Add("baucher");

        foreach (ListItem monedaList in ddlMoneda.Items)
        {
            foreach (ListItem formaPagoList in ddlFormaPago.Items)
            {
                gvFormaPago.Visible = true;
                DataRow fila = dt.NewRow();
                fila[0] = monedaList.Value;
                fila[1] = formaPagoList.Value;
                fila[2] = 0;
                fila[3] = monedaList.Text;
                fila[4] = formaPagoList.Text;
                fila[5] = 0;
                fila[6] = 0;
                fila[7] = 0;
                dt.Rows.Add(fila);
            }
        }

        gvFormaPago.DataSource = dt;
        gvFormaPago.DataBind();
        Session["tablaSesion2"] = dt;
    }
    protected void CrearTablaCheque()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("numcheque");
        dt.Columns.Add("entidad");
        dt.Columns.Add("valor");
        dt.Columns.Add("moneda");
        dt.Columns.Add("nommoneda");
        dt.Columns.Add("nomentidad");

        gvCheques.DataSource = dt;
        gvCheques.DataBind();
        gvCheques.Visible = false;
        Session["tablaSesion3"] = dt;
    }
    protected void LlenarTablaFormaPago(int formapago, int moneda, DataTable dtAgre, decimal valEfectivo)
    {
        DataTable dtAgre2 = new DataTable();
        dtAgre2 = (DataTable)Session["tablaSesion2"];
        decimal acum = 0;

        Xpinn.Caja.Services.TipoOperacionService tipOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoOperacion tipOpe = new Xpinn.Caja.Entities.TipoOperacion();

        // Determina el dato del tipo de producto
        if (ddlTipoPago.SelectedValue != "")
        {
            tipOpe.cod_operacion = ddlTipoPago.SelectedValue;
        }
        else
        {
            tipOpe.cod_operacion = "101"; //Se agrego ya que cuando se utiliza un cheque en todos los productos no cargaba una transaccion 
        }

        tipOpe = tipOpeService.ConsultarTipOpeTranCaja(tipOpe, (Usuario)Session["usuario"]);

        //se trata de localizar el registro que se hace necesario actualizar
        foreach (DataRow fila in dtAgre2.Rows)
        {
            if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == formapago)
            {
                fila[2] = decimal.Parse(fila[2].ToString()) + valEfectivo;
                fila[5] = tipOpe.tipo_movimiento;
            }

            acum = acum + decimal.Parse(fila[2].ToString());
            fila[5] = tipOpe.tipo_movimiento;
        }

        gvFormaPago.DataSource = dtAgre2;
        gvFormaPago.DataBind();
        Session["tablaSesion2"] = dtAgre2;

        decimal valFormaPagoTotal = 0;

        valFormaPagoTotal = ConvertirStringADecimal(txtValTotalFormaPago.Text);
        txtValTotalFormaPago.Text = ConvertirDecimalAString(acum);

    }
    /// <summary>
    /// En este métodos se cargan el valor del cheque registrado a la grilla de cheques validando
    /// que el valor no excede el valor total en cheques.
    /// </summary>
    /// <returns></returns>
    protected long ValidarFormaPagoCheque()
    {
        DataTable dtAgre4 = new DataTable();
        dtAgre4 = (DataTable)Session["tablaSesion2"];

        // Determinar datos de la forma de pago
        long moneda = long.Parse(ddlMonCheque.SelectedValue);
        decimal ValorAPagar = ConvertirStringADecimal(txtValorTran.Text);
        decimal ValorCheque = decimal.Parse(txtValCheque.Text.Replace(".", ""));

        // Validaciones para la forma de pago en cheque
        decimal acum = 0;
        long result = 0;
        foreach (DataRow fila in dtAgre4.Rows)
        {
            if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == 1)
            {
                if (decimal.Parse(fila[2].ToString()) >= ValorCheque)
                {
                    fila[2] = decimal.Parse(fila[2].ToString()) - ValorCheque;
                }
                else
                {
                    result = 1;
                }
            }
            if (result == 0)
                acum = acum + decimal.Parse(fila[2].ToString());
        }

        if (result == 0)
        {
            gvFormaPago.DataSource = dtAgre4;
            gvFormaPago.DataBind();
            Session["tablaSesion2"] = dtAgre4;

            decimal valFormaPagoTotal = 0;
            valFormaPagoTotal = ConvertirStringADecimal(txtValTotalFormaPago.Text);
            txtValTotalFormaPago.Text = ConvertirDecimalAString(acum);
        }

        return result;
    }
    /// <summary>
    ///  Actualiza la forma de pago con el valor ingresado y totaliza 
    /// </summary>
    /// <returns></returns>
    protected long TotalizarFormaPago()// este es el metodo que suma 
    {
        DataTable dtAgre2 = new DataTable();
        dtAgre2 = (DataTable)Session["tablaSesion2"];
        long result = 0;

        // Cargar la información de la forma de apgo
        Configuracion conf = new Configuracion();
        long moneda = long.Parse(ddlMoneda.SelectedValue);
        long formapago = long.Parse(ddlFormaPago.SelectedValue);
        decimal ValorAPagar = ConvertirStringADecimal(txtValorTran.Text);
        decimal valorFormaPago = ConvertirStringADecimal(txtValor.Text);
        int cuentabanco = 0;
        string baucher = "";
        baucher = txtBaucher.Text;
        if (ddlBancoConsignacion.SelectedValue != null)
            cuentabanco = Int32.Parse(ddlBancoConsignacion.SelectedValue);

        // Se valida que no se incerten cambios en Forma de Pagos en Efectivo y Cheque
        if (formapago != 2)
        {
            //se trata de localizar el registro que se hace necesario actualizar
            foreach (DataRow fila in dtAgre2.Rows)
            {
                if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == formapago)
                {
                    // Asigna los datos a la forma de pago seleccionada                    
                    fila[2] = valorFormaPago;
                    if (int.Parse(fila[1].ToString()) == 5)// si la forma de pago es CONSIGNACION
                    {
                        Session["consignacion"] = 1;
                        fila[6] = cuentabanco;
                    }
                    if (int.Parse(fila[1].ToString()) == 10)// si la forma de pago es DATAFONO
                    {
                        Session["datafono"] = 1;
                        fila[6] = baucher;
                    }
                }
            }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Validar que cuadre por todas las formas de pago, de presentar diferencias ajustar el efectivo
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////            
            decimal totalValidar = 0;
            decimal totalFormasPago = 0;
            // Determinar el valor total de las formas de pago
            foreach (DataRow fila in dtAgre2.Rows)
            {
                if (long.Parse(fila[1].ToString()) != 1) //Validando que sume todos los valores, menos el de efectivo
                {
                    totalValidar += ConvertirStringADecimal(fila[2].ToString());
                }
                totalFormasPago = totalFormasPago + decimal.Parse(fila[2].ToString());
            }
            if (ValorAPagar != totalFormasPago)
            {
                foreach (DataRow fila in dtAgre2.Rows)
                {
                    if (long.Parse(fila[1].ToString()) == 1) // Ajustando el efectivo
                    {
                        if (ValorAPagar > totalValidar)
                            fila[2] = ValorAPagar - totalValidar;
                        else
                            fila[2] = 0;
                    }
                }
            }

            // Mostrar los valores de la forma de pago
            gvFormaPago.DataSource = dtAgre2;
            gvFormaPago.DataBind();
            Session["tablaSesion2"] = dtAgre2;

            // Mostrar el total de formas de pago
            decimal valFormaPagoTotal = 0;
            foreach (DataRow fila in dtAgre2.Rows)
            {
                valFormaPagoTotal += decimal.Parse(fila[2].ToString());
            }
            txtValTotalFormaPago.Text = ConvertirDecimalAString(valFormaPagoTotal);

        }
        else
        {
            result = 1;
        }

        return result;
    }
    /// <summary>
    /// Valida que la formas de pago no superen el valor total de las transacciones
    /// </summary>
    /// <returns></returns>
    protected long ValidarValorFormaPago()
    {
        DataTable dtAgre5 = new DataTable();
        dtAgre5 = (DataTable)Session["tablaSesion2"];
        dtAgre5.AcceptChanges();

        // Datos de la forma de pago
        long moneda = long.Parse(ddlMoneda.SelectedValue);
        decimal ValorForma = decimal.Parse(txtValor.Text.Replace(".", ""));
        long formapago = long.Parse(ddlFormaPago.SelectedValue);

        decimal acum = 0;
        long result = 0;

        if (formapago != 2)
        {
            foreach (DataRow fila in dtAgre5.Rows)
            {
                if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == 1)
                {
                    // Se valida que el valor de la forma de pago(grilla) sea mayor que el valor de forma de pago(textbox)
                    if (decimal.Parse(fila[2].ToString()) >= ValorForma)
                    {
                        Session["val"] = 1;
                    }
                    else
                    {
                        if (int.Parse(Session["val"].ToString()) == 0)
                            result = 1;
                        else
                            result = 0;
                    }
                }
                if (result == 0)
                    acum = acum + decimal.Parse(fila[2].ToString());
            }
            if (result == 0)
            {
                gvFormaPago.DataSource = dtAgre5;
                gvFormaPago.DataBind();
                Session["tablaSesion2"] = dtAgre5;

                decimal valFormaPagoTotal = 0;
                valFormaPagoTotal = ConvertirStringADecimal(txtValTotalFormaPago.Text);
                txtValTotalFormaPago.Text = ConvertirDecimalAString(acum);
            }
        }
        else
        {
            result = 1;
        }

        return result;
    }

    /// <summary>
    /// LLenar la tabla de transacciones con los datos de una nueva transacciòn ingresados
    /// </summary>
    protected void LlenarTablaTran()
    {
        gvTransacciones.Visible = true;
        DataTable dtAgre = new DataTable();
        dtAgre = (DataTable)Session["tablaSesion"];
        DataRow fila = dtAgre.NewRow();

        //se consulta el tipo movimiento y el tipo de producto que esta relacionado al tipo de transaccion
        Xpinn.Caja.Services.TipoOperacionService tipOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoOperacion tipOpe = new Xpinn.Caja.Entities.TipoOperacion();
        Xpinn.Caja.Entities.TipoOperacion tipom = new Xpinn.Caja.Entities.TipoOperacion();

        // Determina el dato del tipo de producto
        tipOpe.cod_operacion = ddlTipoPago.SelectedValue;
        tipOpe = tipOpeService.ConsultarTipOpeTranCaja(tipOpe, (Usuario)Session["usuario"]);

        string num_cre = "1", control = "";

        // Validando si se le esta aplicando un pago a un producto ya registrado
        foreach (GridViewRow pos in gvTransacciones.Rows)
        {
            num_cre = pos.Cells[8].Text;
            if (!chkMora.Checked)
            {
                if (num_cre == txtNumProducto.Text.Trim() && Convert.ToInt32(Session["CuotaExtra"]) == 0)
                {
                    control = "0";
                }
            }
        }

        if (control == "0")
        {
            Lblerror.Text = "Ya se cargo una Transacción a ese Número de Producto";
        }
        else
        {
            // LLena los datos de la fila 
            fila[0] = ddlTipoProducto.SelectedValue;
            fila[1] = tipOpe.tipo_producto;
            if (txtNumProducto.Text.Trim() == "")           // Colocar el nùmero del producto
                fila[2] = "0";
            else
                fila[2] = txtNumProducto.Text;
            fila[3] = txtValTransac.Text.Replace(".", "");
            fila[4] = ddlMonedas.SelectedValue;             // Colocar el tipo de moneda de la transacciòn
            fila[5] = ddlTipoPago.SelectedItem.Text;
            fila[6] = ddlTipoProducto.SelectedItem.Text;    // Colocar el tipo de producto de la transacciòn
            fila[7] = ddlMonedas.SelectedItem.Text;
            fila[8] = tipOpe.tipo_movimiento;
            fila[9] = tipOpe.nom_tipo_operacion;
            fila[10] = ddlTipoPago.SelectedValue;
            if (txtReferencia.Text.Trim() == "")           // Colocar el nùmero de  Referencia
                fila[11] = "0";
            else
                fila[11] = txtReferencia.Text;
            fila[12] = txtAvances.Text;

            // Adiciona la fila a la tabla
            dtAgre.Rows.Add(fila);
            gvTransacciones.DataSource = dtAgre;
            gvTransacciones.DataBind();
            Session["tablaSesion"] = dtAgre;

            // Inicializa los totales en efectivo y en cheque
            decimal valTotal = 0;
            decimal valEfectivo = 0;

            // Por defecto el valor total a pagar es efectivo
            Configuracion conf = new Configuracion();
            valEfectivo = txtValTransac.Text == "" ? 0 : decimal.Parse(txtValTransac.Text.Replace(".", ""));
            valTotal = ConvertirStringADecimal(txtValorTran.Text);
            valTotal = valTotal + valEfectivo;
            txtValorTran.Text = ConvertirDecimalAString(valTotal);

            // Determina el tipo de moneda
            int moneda = Convert.ToInt32(ddlMonedas.SelectedValue);

            // Actualiza el valor en efectivo en la tabla de forma de pago
            LlenarTablaFormaPago(1, moneda, dtAgre, valEfectivo);
        }
    }
    protected void LlenarTablaCheque()
    {
        gvCheques.Visible = true;
        DataTable dtAgre3 = new DataTable();
        dtAgre3 = (DataTable)Session["tablaSesion3"];
        DataRow fila = dtAgre3.NewRow();
        fila[0] = txtNumCheque.Text;
        fila[1] = ddlBancos.SelectedValue;
        fila[2] = txtValCheque.Text.Replace(".", "");
        fila[3] = ddlMonCheque.SelectedValue;
        fila[4] = ddlMonCheque.SelectedItem.Text;
        fila[5] = ddlBancos.SelectedItem.Text;

        dtAgre3.Rows.Add(fila);
        gvCheques.DataSource = dtAgre3;
        gvCheques.DataBind();
        Session["tablaSesion3"] = dtAgre3;

        decimal valTotal = 0;
        decimal valCheque = 0;

        valCheque = txtValCheque.Text == "" ? 0 : decimal.Parse(txtValCheque.Text.Replace(".", ""));
        valTotal = txtValTotalCheque.Text == "" ? 0 : decimal.Parse(txtValTotalCheque.Text);

        valTotal = valTotal + valCheque;
        txtValTotalCheque.Text = valTotal.ToString();

        int moneda = Convert.ToInt32(ddlMonCheque.SelectedValue);
        LlenarTablaFormaPago(2, moneda, dtAgre3, valCheque);
    }

    Boolean ValidarMontoPago(int pCodTipo, String pTipoProducto, GridView pGrid, int pFilaValor)
    {
        try
        {
            decimal valor = 0;
            Int64 codRadicado = 0;
            valor = txtValTransac.Text == "" ? 0 : decimal.Parse(txtValTransac.Text.Replace(".", ""));
            long numProd = txtNumProducto.Text == "" ? 0 : long.Parse(txtNumProducto.Text);

            decimal deudaTotal = 0;

            if (long.Parse(Session["tipoproducto"].ToString()) == pCodTipo)
            {
                foreach (GridViewRow fila in pGrid.Rows)
                {
                    codRadicado = Int64.Parse(pGrid.DataKeys[fila.RowIndex].Values[0].ToString());
                    deudaTotal = 0;
                    if (fila.Cells[pFilaValor].Text != "&nbsp;")
                    {
                        deudaTotal = decimal.Parse(fila.Cells[pFilaValor].Text);
                    }
                    if (codRadicado == numProd)
                    {
                        if (valor > deudaTotal)
                        {
                            string parametroGeneral95 = ventanillaServicio.ParametroGeneral(95, (Usuario)Session["Usuario"]);
                            if ((parametroGeneral95 == null ? "0" : "1") != "1")
                            {
                                VerError("En el " + pTipoProducto + " " + codRadicado + " el valor a pagar [" + valor.ToString("n0") + "] supera el valor total adeudado [" + deudaTotal.ToString("n0") + "]");
                                return false;
                            }
                            else
                            {
                                VerError("En el " + pTipoProducto + " " + codRadicado + " el valor a pagar [" + valor.ToString("n0") + "] supera el valor total adeudado [" + deudaTotal.ToString("n0") + "] se generarà devoluciòn");
                            }
                            break;
                        }
                    }
                }
            }
            return true;
        }
        catch
        {
            return false;
        }

    }

    /// <summary>f
    ///  LLenar la grilla de transacciones a aplicar con los datos de la transacciòn ingresados
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGoTran_Click(object sender, EventArgs e)
    {
        VerError("");

        Xpinn.Caja.Services.TipoOperacionService tipoOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoOperacion tipOpe = new Xpinn.Caja.Entities.TipoOperacion();
        Xpinn.Caja.Entities.TipoOperacion tipom = new Xpinn.Caja.Entities.TipoOperacion();
        string cuenta = "";
        // Cargar el valor de la transacciòn y el nùmero de producto
        decimal valor = 0;
        long codRadicado=0;
        int result = 0;
        valor = txtValTransac.Text == "" ? 0 : decimal.Parse(txtValTransac.Text.Replace(".", ""));
        string numProd = txtNumProducto.Text == "" ? "0" : txtNumProducto.Text;

        // Consultar datos del tipo de operaciòn a utilizar
        if (ddlTipoPago.SelectedItem == null)
        {
            VerError("No existen Tipos de Pago para el Producto Seleccionado");
            return;
        }
        tipOpe.cod_operacion = ddlTipoPago.SelectedValue;
        tipOpe = tipoOpeService.ConsultarTipoOperacion(tipOpe, (Usuario)Session["usuario"]);

        Session["tipoproducto"] = tipOpe.tipo_producto;


        // consultar  cierres historicos Ahorros a la vista 

        if (long.Parse(Session["tipoproducto"].ToString()) == 3)
        {
            String estado = "";
            DateTime fechacierrehistorico;
            String format = gFormatoFecha;
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaTransaccion.Text, format, CultureInfo.InvariantCulture);
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarCierreAhorroVista((Usuario)Session["usuario"]);
            if (vAhorroVista != null)
            {
                estado = vAhorroVista.estadocierre;
                fechacierrehistorico = Convert.ToDateTime(vAhorroVista.fecha_cierre.ToString());
            }
            else
            {
                estado = "D";
                fechacierrehistorico = new DateTime(DateTime.Now.Year, 1, 1);
            }

            if (estado == "D" && Fechatransaccion <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO H,'AHORROS'");
                return;
            }
            else
            {
                result = 1;
            }
        }


        if (long.Parse(Session["tipoproducto"].ToString()) == 9)
        {
            //Consultar cierre historico de ahorro programado
            String estado = "";
            DateTime fechacierrehistorico;
            String format = gFormatoFecha;
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaTransaccion.Text, format, CultureInfo.InvariantCulture);
            Xpinn.Programado.Entities.CuentasProgramado vAhorroProgramado = new Xpinn.Programado.Entities.CuentasProgramado();
            vAhorroProgramado = cuentasProgramado.ConsultarCierreAhorroProgramado((Usuario)Session["usuario"]);
            if (vAhorroProgramado != null)
            {
                estado = vAhorroProgramado.estadocierre;
                fechacierrehistorico = Convert.ToDateTime(vAhorroProgramado.fecha_cierre.ToString());
            }
            else
            {
                estado = "D";
                fechacierrehistorico = DateTime.MinValue;
            }

            if (estado == "D" && Fechatransaccion <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO L,'AH. PROGRAMADO'");
                return;
            }
            else
            {
                result = 1;
            }
        }

        if (long.Parse(Session["tipoproducto"].ToString()) == 5)
        {
            //consultar cierre historico de Cdats 
            String estado = "";
            DateTime fechacierrehistorico;
            String format = gFormatoFecha;
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaTransaccion.Text, format, CultureInfo.InvariantCulture);

            Xpinn.CDATS.Entities.LiquidacionCDAT vliquidacioncdat = new Xpinn.CDATS.Entities.LiquidacionCDAT();
            vliquidacioncdat = LiquiService.ConsultarCierreCdats((Usuario)Session["usuario"]);
            estado = vliquidacioncdat.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vliquidacioncdat.fecha_cierre.ToString());

            if (estado == "D" && Fechatransaccion <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO M,'CDAT'S'");
                return;
            }

            else
            {
                result = 1;
            }
        }


        if (long.Parse(Session["tipoproducto"].ToString()) == 1)
        {
            //consultar cierre historico De aportes 
            String estado = "";
            DateTime fechacierrehistorico;
            String format = gFormatoFecha;
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaTransaccion.Text, format, CultureInfo.InvariantCulture);

            Xpinn.Aportes.Entities.Aporte vaportes = new Xpinn.Aportes.Entities.Aporte();
            vaportes = AporteServicio.ConsultarCierreAportes((Usuario)Session["usuario"]);
            if (vaportes != null)
            {

                estado = vaportes.estadocierre;
                fechacierrehistorico = Convert.ToDateTime(vaportes.fecha_cierre.ToString());

                if (estado == "D" && Fechatransaccion <= fechacierrehistorico)
                {
                    VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO A,'APORTES'");
                    return;
                }
            }
            result = 1;
        }


        if (long.Parse(Session["tipoproducto"].ToString()) == 2)
        {
            //consultar cierre historico Cartera
            String estado = "";
            DateTime fechacierrehistorico;
            String format = gFormatoFecha;
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaTransaccion.Text, format, CultureInfo.InvariantCulture);

            Xpinn.FabricaCreditos.Entities.Credito vcredito = new Xpinn.FabricaCreditos.Entities.Credito();
            vcredito = CreditoServicio.ConsultarCierreCartera((Usuario)Session["usuario"]);
            estado = vcredito.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vcredito.fecha_cierre.ToString());

            if (estado == "D" && Fechatransaccion <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO R,'CREDITOS'");
                return;
            }
            else
            {
                result = 1;
            }
        }

        if (long.Parse(Session["tipoproducto"].ToString()) == 4)
        {
            //consultar cierre historico Servicios
            String estado = "";
            DateTime fechacierrehistorico;
            String format = gFormatoFecha;
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaTransaccion.Text, format, CultureInfo.InvariantCulture);

            Xpinn.Servicios.Entities.CierreHistorico vservicio = new Xpinn.Servicios.Entities.CierreHistorico();
            vservicio = ServiceServicio.ConsultarCierreServicios((Usuario)Session["usuario"]);
            if (vservicio != null)
            {
                estado = vservicio.estadocierre;
                fechacierrehistorico = Convert.ToDateTime(vservicio.fecha_cierre.ToString());
            }
            else
            {
                estado = "D";
                fechacierrehistorico = DateTime.MinValue;
            }

            if (estado == "D" && Fechatransaccion <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO Q,'SERVICIOS'");
                return;
            }

            else
            {
                result = 1;
            }
        }


        if (txtNombreCliente.Text != "")
        {
            decimal deudaTotal = 0;
            if (ddlTipoProducto.SelectedValue == "6")
            {
                foreach (GridViewRow rFila in gvDatosAfiliacion.Rows)
                {
                    if (txtNumProducto.Text != "")
                    {
                        if (Convert.ToInt32(txtNumProducto.Text) == Convert.ToInt32(rFila.Cells[1].Text))
                        {
                            if (valor <= Convert.ToDecimal(rFila.Cells[3].Text))
                            {
                                result = 1;
                            }
                            else
                            {
                                VerError("El Valor de la Transacción no puede ser Mayor al valor de la Afiliación");
                                return;
                            }
                        }
                    }
                    else
                    {
                        VerError("Seleccione un registro para realizar la Transacción");
                        return;
                    }
                }
            }

            if (long.Parse(Session["tipoproducto"].ToString()) == 4)
            {
                if (ddlTipoPago.SelectedValue == "34")
                {
                    if (!ValidarMontoPago(4, "Servicio", gvServicios, 13))
                        return;
                }
                else
                {
                    if (!ValidarMontoPago(4, "Servicio", gvServicios, 14))
                        return;
                }
            }


            if (long.Parse(Session["tipoproducto"].ToString()) == 2)
            {
                foreach (GridViewRow fila in gvConsultaDatos.Rows)
                {
                    codRadicado = Int64.Parse(fila.Cells[2].Text);
                    deudaTotal = decimal.Parse(fila.Cells[13].Text);

                    if (codRadicado.ToString() == numProd)
                    {
                        result = 1;
                        if (valor > deudaTotal)
                        {
                            string parametroGeneral95 = ventanillaServicio.ParametroGeneral(95, (Usuario)Session["Usuario"]);
                            if ((parametroGeneral95 == null ? "0" : "1") != "1")
                            {
                                VerError("En el crédito " + codRadicado + " el valor a pagar [" + valor + "] supera el valor total adeudado [" + deudaTotal + "]");
                                return;
                            }
                            else
                            {
                                VerError("En el crédito " + codRadicado + " el valor a pagar [" + valor + "] supera el valor total adeudado [" + deudaTotal + "] se generarà devoluciòn");
                            }
                        }
                    }
                }
            }

            if (long.Parse(Session["tipoproducto"].ToString()) == 5)//cdats controlar valor no sea mayor ni menor
            {
                decimal saldoTotal = 0;
                Int32 tipomov = Convert.ToInt32(ddlTipoMovimiento.SelectedValue);
                foreach (GridViewRow fila in this.gvCdat.Rows)
                {
                    codRadicado = Int64.Parse(fila.Cells[1].Text);
                    saldoTotal = decimal.Parse(fila.Cells[11].Text);

                    if (codRadicado.ToString() == numProd)
                    {
                        result = 1;
                        if (valor > saldoTotal && tipomov == 2)
                        {
                            VerError("En la cuenta  " + codRadicado + " el valor a pagar [" + valor + "] supera el Valor del Cdat [" + saldoTotal + "]");
                            return;
                        }

                        //esto se comentarea  debido a que las entidades pueden pagar el cdat una parte en efectivo y otra por otra forma de pago 

                        //  if (valor < saldoTotal && tipomov == 2)
                        //{
                        //  VerError("En la cuenta  " + codRadicado + " el valor a pagar [" + valor + "] no cubre el Valor del Cdat [" + saldoTotal + "]");
                        // return;
                        // }
                    }
                }
            }

            if (long.Parse(Session["tipoproducto"].ToString()) == 5)//cdats controlar valor no sea mayor ni menor
            {
                decimal saldoTotal = 0;
                Int32 tipomov = Convert.ToInt32(ddlTipoMovimiento.SelectedValue);
                foreach (GridViewRow fila in this.gvCdat.Rows)
                {
                    cuenta = (fila.Cells[1].Text);
                    saldoTotal = decimal.Parse(fila.Cells[11].Text);

                    if (cuenta == numProd.ToString())
                    {
                        result = 1;
                        if (valor > saldoTotal && tipomov == 2)
                        {

                            VerError("En la cuenta  " + cuenta + " el valor a pagar [" + valor + "] supera el Valor del Cdat [" + saldoTotal + "]");
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                            return;
                        }

                        //esto se comentarea  debido a que las entidades pueden pagar el cdat una parte en efectivo y otra por otra forma de pago 

                        //if (valor < saldoTotal && tipomov == 2)
                       // {
                            //VerError("En la cuenta  " + cuenta + " el valor a pagar [" + valor + "] no cubre el Valor del Cdat [" + saldoTotal + "]");
                           // ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                            //return;
                        //}
                    }
                }
            }
            else
                result = 1;

            foreach (GridViewRow fila in gvTransacciones.Rows)
            {
                codRadicado = Convert.ToInt64(fila.Cells[8].Text);
                tipoproducto = Convert.ToInt64(fila.Cells[4].Text);

                if (numProd == Convert.ToString(codRadicado) && Convert.ToInt64(ddlTipoProducto.SelectedValue) == tipoproducto)
                {
                    result = 1;
                    VerError("No se puede adicionar más de una transacción al producto");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                    return;

                }
                contador = Convert.ToInt64(numProd);

            }




            if (result == 1)// si el radicado existe para ese cliente entonces se inserta el dato 
            {
                if (valor > 0)// hay que validar que acepte valores mayores o iguales a cero si es el caso de tipo tran 5
                {
                    // Llenar la tabla de transacciones
                    Lblerror.Text = "";
                    Session["CuotaExtra"] = 1;
                    LlenarTablaTran();
                    Session["CuotaExtra"] = null;
                    txtNumProducto.Text = "";
                    txtValTransac.Text = "";
                    txtAvances.Text = "";
                }
                else
                    VerError("El Valor de Transacción debe ser mayor a cero");
            }
            else
                VerError("El Radicado que ha digitado no coincide con el que aparece en la Consulta de Datos, por favor verificar.");
        }
        else
            VerError("Se debe Consultar Primero al Cliente");


        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
        Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
        persona.identificacion = txtIdentificacion.Text;
        persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
        VerError("");
        persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);

        Actualizar(persona);
    }


    protected void btnGoFormaPago_Click(object sender, EventArgs e)
    {
        VerError("");
        decimal valor = 0;
        valor = txtValor.Text == "" ? 0 : decimal.Parse(txtValor.Text.Replace(".", ""));
        long result = 0;
        long result2 = 0;

        if (gvTransacciones.Rows.Count > 0)
        {
            if (ddlFormaPago.SelectedValue == "1") { Panel1.Visible = true; RpviewInfo.Visible = true; } else { Panel1.Visible = false; RpviewInfo.Visible = false; }
            if (ddlFormaPago.SelectedValue == "5") // Si es consignación valide que se seleccionó el banco
                if (ddlBancoConsignacion.SelectedValue == "" || ddlBancoConsignacion.SelectedValue == "0")
                {
                    VerError("Debe seleccionar el banco de la consignación");
                    return;
                }

            if (ddlFormaPago.SelectedValue == "10") // Si es por datafono  valide que se seleccionó el numero boucher 
                if (txtBaucher.Text == "")
                {
                    VerError("Debe digitar  el numero  de bocuher");
                    return;
                }

            result = ValidarValorFormaPago();

            if (result == 0)
            {
                result2 = TotalizarFormaPago();
                if (result2 == 1)
                    VerError("No se puede actualizar los valore de Forma de Pago Efectivo o Cheques, estos deben ser ingresados desde los paneles de Ingreso de Cada Uno");
            }
            else
            {
                this.Lblerror.Text = "El Valor de la Forma de Pago debe ser menor al valor Efectivo";
                this.Lblerror.Visible = true;
                //VerError("El Valor de la Forma de Pago debe ser menor al valor Efectivo");
            }
        }
        else
        {
            VerError("Debe registrar las transacciones primero");
        }
    }
    protected void btnGoCheque_Click(object sender, EventArgs e)
    {
        int control = 0;

        if (txtNumCheque.Text == "")
        {
            numchequevacio.Text = "Ingrese un Número de Cheque";
            control = 1;
        }

        if (txtValCheque.Text == "")
        {
            valorchequevacio.Text = "Ingrese el Valor";
            control = 1;
        }

        if (ddlBancos.SelectedIndex == 0)
        {
            bancochquevacio.Text = "Seleccione un Banco";
            control = 1;
        }

        if (control != 1)
        {

            decimal valor = 0;
            valor = txtValCheque.Text == "" ? 0 : decimal.Parse(txtValCheque.Text.Replace(".", ""));
            long result = 0;

            if (gvTransacciones.Rows.Count > 0)
            {
                if (valor > 0)
                {
                    bancochquevacio.Text = "";
                    numchequevacio.Text = "";
                    valorchequevacio.Text = "";
                    result = ValidarFormaPagoCheque();
                    if (result == 0)
                    {
                        LlenarTablaCheque();
                        txtValTotalCheque.Visible = true;
                        lblValTotalCheque.Visible = true;
                    }
                    else
                    {
                        VerError("El Valor del Cheque no puede ser Superior al Valor de Efectivo");
                    }
                }
                else
                    VerError("El Valor de Forma de Pago debe ser Mayor a Cero");
            }
            else
                VerError("Debe registrar las transacciones primero");
        }
    }
    private void deshabilitar()
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
    }

    /// <summary>
    /// Mètodo para aplicar las transacciones registradas segùn las formas de pago
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        StringHelper stringHelper = new StringHelper();

        this.Lblerror.Text = "";
        DateTime fechadesembolso = Convert.ToDateTime(Session["fecha"]);
        DateTime fechaactual = DateTime.Now;
        String format = gFormatoFecha;
        DateTime Fechatransaccion = DateTime.ParseExact(txtFechaTransaccion.Text, format, CultureInfo.InvariantCulture);

        int con = 0;
        foreach (GridViewRow row in gvTransacciones.Rows)
        {
            if (gvTransacciones.Rows[con].Cells[7].Text == "Cr&#233;ditos")
            {
                string x = gvTransacciones.Rows[con].Cells[8].Text;
                refe += "- " + x;

                if (refe.StartsWith("-"))
                    refe = refe.Substring(1);
            }
            if (gvTransacciones.Rows[con].Cells[7].Text == "Ahorros Vista")
            {
                string y = gvTransacciones.Rows[con].Cells[8].Text;
                refe2 += "- " + y;
                if (refe2.StartsWith("-"))
                    refe2 = refe2.Substring(1);
            }
            con++;
        }


        if (Fechatransaccion > fechaactual)
        {
            String Error = "La fecha no puede ser superior a la fecha actual";
            this.Lblerror.Text = Error;
        }
        if (Fechatransaccion.AddDays(1) < fechadesembolso)
        {
            String Error = "No se puede aplicar el pago por que es inferior a la fecha de desembolso";
            this.Lblerror.Text = Error;
        }
        else
        {
            this.Lblerror.Text = "";
            btnGuardar2.Visible = false;

            nActiva = Convert.ToInt16(mvOperacion.ActiveViewIndex.ToString());
            if (nActiva == 0)
            {
                if (txtNumProducto.Text != "" && txtValTransac.Text != "" && txtValTransac.Text != "0")
                {
                    btnGoTran_Click(sender, e);
                };
                ValTotalFPago = ConvertirStringADecimal(txtValTotalFormaPago.Text); // valor total de Forma de Pago
                                                                                    // ValTotalTran = ConvertirStringADecimal(txtValorTran.Text); //Valor Total de Tabla de Transacciones
                RpviewInfo.Visible = false;

                calcularMontoFinal();
                string[] val = txtValorTran.Text.Split(',');
                ddlBancoConsignacion.Visible = false;
                txtValor.Text = val[0];
                mvOperacion.ActiveViewIndex = 1;
                btnGuardar.Visible = false;
                btnGuardar2.Visible = true;
            }
        }
    }

    protected void calcularMontoFinal()
    {
        StringHelper stringHelper = new StringHelper();

        this.Lblerror.Text = "";
        btnGuardar.Visible = false;
        Decimal MontoDiario = 0;
        Decimal MontoMensual = 0;
        String ValTotalTranEfectivo = "";
        decimal valorreportel = 0;
        decimal ValTotalTrEfectivo = 0;
        try
        {
            DateTime fechadesembolso = new DateTime();
            if (Session["fecha"] != null)
                fechadesembolso = Convert.ToDateTime(Session["fecha"]);
            DateTime fechaactual = DateTime.Now;
            String format = gFormatoFecha;

            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaTransaccion.Text, format, CultureInfo.InvariantCulture);
            if (Fechatransaccion > fechaactual)
            {
                String Error = "La fecha no puede ser superior a la fecha actual";
                this.Lblerror.Text = Error;
            }
            else
            {
                if (fechadesembolso != null)
                {
                    if (Fechatransaccion.AddDays(1) < fechadesembolso)
                    {
                        String Error = "No se puede aplicar el pago por que es inferior a la fecha de desembolso";
                        this.Lblerror.Text = Error;
                        return;
                    }
                }
                this.Lblerror.Text = "";
                ValTotalFPago = ConvertirStringADecimal(txtValTotalFormaPago.Text); // valor total de Forma de Pago
                ValTotalTran = ConvertirStringADecimal(txtValorTran.Text); // Valor Total de Tabla de Transacciones
                if (ValTotalFPago == 0 && ValTotalTran == 0)
                {
                    VerError("Debe especificar los valores a pagar");
                    return;
                }
                if (ValTotalFPago == ValTotalTran)// si son iguales en valor entonces deja guardar
                {
                    ValTotalTran = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text);//Valor Total de Tabla de Transacciones
                    String ValTotalTrancomparar = stringHelper.FormatearNumerosComoMilesSinDecimales(ValTotalTran);

                    ValTotalTranfinal = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text.ToString());//Valor Total de Tabla de Transacciones

                    if (ValTotalFPago == 0 && ValTotalTran == 0)
                        return;

                    Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
                    Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
                    Xpinn.Comun.Entities.General pData2 = new Xpinn.Comun.Entities.General();
                    Xpinn.Comun.Data.GeneralData ConsultaData2 = new Xpinn.Comun.Data.GeneralData();
                    pData = ConsultaData.ConsultarGeneral(16, (Usuario)Session["usuario"]);

                    if (pData.valor != "" && pData.valor != null)
                        MontoDiario = Convert.ToDecimal(pData.valor);

                    // transacciones acumuladas mensuales
                    pData2 = ConsultaData2.ConsultarGeneral(17, (Usuario)Session["usuario"]);

                    if (pData2.valor != "" && pData2.valor != null)
                        MontoMensual = Convert.ToDecimal(pData2.valor);


                    if (gvFormaPago.Rows[0].Cells[3].Text == "EFECTIVO")
                    {
                        ValTotalTranEfectivo = gvFormaPago.Rows[0].Cells[4].Text;
                        ValTotalTrEfectivo = ConvertirStringToInt(ValTotalTranEfectivo);
                        valorreportel = Convert.ToDecimal(ValTotalTrEfectivo);
                        if (ValTotalTranEfectivo == "")
                        {
                            ValTotalTrEfectivo = Convert.ToInt64(0);
                        }
                    }

                    if (ValTotalTrEfectivo > 0)
                    {
                        //Reporte

                        if (tranCajaServicio.ValidarControlOperacion(long.Parse(Session["codpersona"].ToString()), ref ValTotalTrEfectivo, Convert.ToDateTime(txtFechaTransaccion.Text), (Usuario)Session["usuario"], MontoDiario, MontoMensual))
                        {
                            RpviewInfo.Visible = true;

                            //if (Convert.ToInt32(ValTotalTran) >= MontoDiario)
                            //{
                            Usuario pUsuario = (Usuario)Session["usuario"];
                            mvOperacion.ActiveViewIndex = 1;
                            DateTime Fecha = DateTime.Now;
                            string iden = "";
                            string Observaciones = "";
                            Xpinn.Caja.Entities.Ciudad Ciudad = new Xpinn.Caja.Entities.Ciudad();
                            Ciudad = CiudadServicio.CiudadTran(pUsuario);

                            ////CREAR TABLA INFO;Ok                                                        
                            DataTable tablegeneral = new DataTable();
                            tablegeneral.Columns.Add("Municipio");
                            tablegeneral.Columns.Add("Year");
                            tablegeneral.Columns.Add("Mes");
                            tablegeneral.Columns.Add("Dia");
                            tablegeneral.Columns.Add("NombreOficina");
                            tablegeneral.Columns.Add("NumCtAh");
                            tablegeneral.Columns.Add("Deposito");
                            tablegeneral.Columns.Add("Retiro");
                            tablegeneral.Columns.Add("ME");
                            tablegeneral.Columns.Add("MI");
                            tablegeneral.Columns.Add("Aportes");
                            tablegeneral.Columns.Add("NumeroCDAT");
                            tablegeneral.Columns.Add("Credito");
                            tablegeneral.Columns.Add("NombrePerOper");
                            tablegeneral.Columns.Add("DirTelPerOper");
                            tablegeneral.Columns.Add("NombreAsoc");
                            tablegeneral.Columns.Add("DirTelAsoc");
                            tablegeneral.Columns.Add("Origen");
                            tablegeneral.Columns.Add("Hora");
                            tablegeneral.Columns.Add("NumeroBenf");
                            tablegeneral.Columns.Add("CC");
                            tablegeneral.Columns.Add("TI");
                            tablegeneral.Columns.Add("RC");
                            tablegeneral.Columns.Add("NIT");
                            tablegeneral.Columns.Add("IdentAsoc");
                            tablegeneral.Columns.Add("Observaciones");


                            DataRow dr;
                            dr = tablegeneral.NewRow();
                            string municipio = Ciudad.nom_ciudad;
                            dr[0] = municipio;
                            dr[1] = Fecha.ToString("yyyy");
                            dr[2] = Fecha.ToString("MM");
                            dr[3] = Fecha.ToString("dd");
                            dr[4] = pUsuario.nombre_oficina;
                            dr[5] = refe2;//Cuenta de Ahorros #                                           
                            if (ddlTipoMovimiento.SelectedValue == "1")
                            {
                                dr[6] = "X";
                                dr[7] = "";
                            }
                            else
                            {
                                dr[6] = "";
                                dr[7] = "X";
                            }
                            dr[8] = "";
                            dr[9] = valorreportel.ToString("n2");
                            if (ddlTipoProducto.SelectedValue == "1")
                            {
                                dr[10] = "X";
                            }
                            else
                            {
                                dr[10] = "";
                            }

                            if (ddlTipoProducto.SelectedValue == "5")
                            {
                                dr[11] = "";
                                //CDAT #
                            }
                            else
                            {
                                dr[11] = "";
                            }
                            dr[12] = refe; //numpagare credito #                                       
                            dr[13] = "";
                            dr[14] = "";
                            dr[15] = txtNombreCliente.Text;
                            string dir = Session["DirecionAsc"].ToString();
                            dr[16] = dir;
                            dr[17] = "";
                            dr[18] = Fecha.ToString("hh:mm:ss tt");
                            dr[19] = txtIdentificacion.Text;
                            var ident = ddlTipoIdentificacion.SelectedValue;

                            if (ident == "1")
                            {
                                dr[20] = "X";
                                iden = "C.C.";
                            }
                            else
                            {
                                dr[20] = "";
                            }
                            if (ident == "5")
                            {
                                dr[21] = "X";
                                iden = "T.I.";
                            }
                            else
                            {
                                dr[21] = "";
                            }
                            if (ident == "7")
                            {
                                dr[22] = "X";
                                iden = "R.C.";
                            }
                            else
                            {
                                dr[22] = "";
                            }
                            if (ident == "2")
                            {
                                dr[23] = "X";
                                iden = "N.I.T";
                            }
                            else
                            {
                                dr[23] = "";
                            }
                            dr[24] = iden;


                            if (ValTotalTrEfectivo >= MontoDiario)
                            {
                                Observaciones = "Transacción supera o es igual al  monto para operaciones diarias  de : " + MontoDiario.ToString("n2");
                            }

                            if (ValTotalTrEfectivo >= MontoMensual)
                            {
                                Observaciones = "Transacción supera el monto para operaciones acumuladas en el mes de : " + MontoMensual.ToString("n2");
                            }

                            dr[25] = Observaciones;

                            tablegeneral.Rows.Add(dr);

                            ReportParameter[] param = new ReportParameter[2];
                            param[0] = new ReportParameter("nit", pUsuario.nitempresa);
                            param[1] = new ReportParameter("ImagenReport", ImagenReporte());

                            RpviewInfo.LocalReport.EnableExternalImages = true;
                            RpviewInfo.LocalReport.SetParameters(param);

                            RpviewInfo.LocalReport.DataSources.Clear();
                            ReportDataSource rds = new ReportDataSource("DataSet1", tablegeneral);
                            RpviewInfo.LocalReport.DataSources.Add(rds);
                            RpviewInfo.LocalReport.Refresh();

                            Site toolBar = (Site)this.Master;
                            toolBar.MostrarCancelar(true);
                            toolBar.MostrarExportar(false);
                            mvOperacion.ActiveViewIndex = 1;
                            btnGuardar.Visible = false;
                            btnGuardar2.Visible = true;

                            RpviewInfo.Visible = true;
                            Panel1.Visible = true;
                            btnImprimiendose_Click();


                        }
                    }
                    else
                    {
                        Panel1.Visible = false;
                    }

                    // Validar que exista la parametrización contable por procesos
                    if (ValidarProcesoContable(ConvertirStringToDate(txtFechaCont.Text), 2) == false)
                    {
                        VerError("No se encontró parametrización contable por procesos para el tipo de operación 2=Pagos por Ventanilla");
                        return;
                    }
                }
                else
                {
                    VerError("");
                    Session["Opcion"] = "CUADRE";

                    ctlMensaje.MostrarMensaje("<h6>El Valor Total de Transacción debe ser igual al Valor Total de Transacción distribiuda en Formas de Pago.</h6> <br/> <h5>Desea Cuadrar los montos ingresados ?</h5>");

                }
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ventanillaServicio.GetType().Name + "A", "btnGuardar_Click", ex);
        }
    }

    protected void btnGuardar2_Click(object sender, ImageClickEventArgs e)
    {
        Session["Opcion"] = "GRABAR";
        ctlMensaje.MostrarMensaje("Desea grabar el pago correspondiente?");
    }

    /// <summary>
    /// Aplicaciòn de los transacciones seleccionadas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        if (Session["Opcion"].ToString() == "CUADRE")
        {
            DataTable dtAgre2 = new DataTable();
            dtAgre2 = (DataTable)Session["tablaSesion2"];

            // CAPTURANDO EL VALOR TOTAL QUE SEAN DIFERENTES A TIPO EFECTIVO
            decimal ValorAPagar = ConvertirStringADecimal(txtValorTran.Text);
            decimal totalValidar = 0, valorEfectivo = 0, ValorDiferencia = 0;
            decimal acum = 0;

            foreach (DataRow fila in dtAgre2.Rows)
            {
                if (long.Parse(fila[1].ToString()) != 1) //Validando que sume todos los valores, menos el de efectivo
                {
                    totalValidar += long.Parse(fila[2].ToString());
                }
                acum = acum + decimal.Parse(fila[2].ToString());
            }
            valorEfectivo = acum - totalValidar;
            ValorDiferencia = ValorAPagar - acum;

            // CUADRANDO LOS VALORES DE PAGO
            if (ValorDiferencia > 0 && ValorDiferencia > ValorAPagar)
            {
                VerError("La diferencia del valor existente es mayor al monto por Pagar. Verifique los datos ingresados (Forma de Pago)");
                return;
            }
            else if (ValorDiferencia < 0 && ValorDiferencia < (ValorAPagar * -1))
            {
                VerError("La diferencia del valor existente es mayor al monto por Pagar. Verifique los datos ingresados (Forma de Pago)");
                return;
            }
            else
            {
                acum = 0;
                foreach (DataRow fila in dtAgre2.Rows)
                {
                    if (ValorDiferencia > 0) // si la diferencia de la grilla es menor al monto que se  debe pagar
                    {
                        if (long.Parse(fila[1].ToString()) == 1) // se suma el monto faltante a  tipo EFECTIVO
                            fila[2] = valorEfectivo + ValorDiferencia;
                    }
                    if (ValorDiferencia < 0) // si a diferencia de la grilla es Mayor al monto que se  debe pagar
                    {
                        if (long.Parse(fila[1].ToString()) == 1) // se resta el monto que excede al tipo EFECTIVO
                            fila[2] = valorEfectivo - (ValorDiferencia * -1);
                    }
                    acum = acum + decimal.Parse(fila[2].ToString());
                }
            }

            gvFormaPago.DataSource = dtAgre2;
            gvFormaPago.DataBind();
            Session["tablaSesion2"] = dtAgre2;

            txtValTotalFormaPago.Text = ConvertirDecimalAString(acum);
        }
        else if (Session["Opcion"].ToString() == "GRABAR")
        {
            // Determinar código de proceso contable para generar el comprobante
            Int64? rpta = 0;
            if (!panelProceso.Visible && panelEncabezado.Visible)
            {
                rpta = ctlproceso.Inicializar(2, Convert.ToDateTime(txtFechaCont.Text), (Usuario)Session["Usuario"]);
                if (rpta > 1)
                {
                    panelEncabezado.Visible = false;
                    mvOperacion.Visible = false;
                    panelProceso.Visible = true;
                }
                else
                {
                    if (AplicarPago())
                    {
                        EnviarCorreo();
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                    }
                }
            }
        }
        ctlMensaje.Visible = false;
    }

    /// <summary>
    /// Crear la operaciòn y realizar las transacciones correspondientes
    /// </summary>
    /// <returns></returns>
    private bool AplicarPago()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        Xpinn.Caja.Entities.TransaccionCaja tranCaja = new Xpinn.Caja.Entities.TransaccionCaja();
        tranCaja.cod_persona = long.Parse(Session["codpersona"].ToString());
        tranCaja.cod_caja = 0;
        tranCaja.cod_cajero = pUsuario.codusuario;
        tranCaja.cod_oficina = pUsuario.cod_oficina;
        tranCaja.fecha_movimiento = Convert.ToDateTime(txtFechaTransaccion.Text);  // Se refiere a la fecha con que se hicieron los calculos
        tranCaja.fecha_aplica = Convert.ToDateTime(txtFechaCont.Text);             // Fecha con que queda la operaciòn y el comprobante
        tranCaja.fecha_cierre = Convert.ToDateTime(txtfechacorte.Text);             // Fecha con que hace los calculos.
        tranCaja.observacion = txtObservacion.Text;
        tranCaja.baucher = this.txtBaucher.Text;
        if (ddlProcesoContable.SelectedValue != "" && ddlProcesoContable.SelectedValue != null)
            tranCaja.cod_proceso = Convert.ToInt64(ddlProcesoContable.SelectedValue);
        if (txtValorTran.Text != "")
            tranCaja.valor_pago = ConvertirStringADecimal(txtValorTran.Text);
        else
            tranCaja.valor_pago = 0;

        // Determinar el tipo de operaciòn para PAGOS POR VENTANILLA
        tranCaja.tipo_ope = 2;

        if (ddlTipoPago.SelectedValue == "3")//Pago crédito caja
            tranCaja.tipo_pago = 1;
        if (ddlTipoPago.SelectedValue == "2")//Pago Total Creditos
            tranCaja.tipo_pago = 2;
        if (ddlTipoPago.SelectedValue == "6")//Abono a capital
            tranCaja.tipo_pago = 3;

        //PERSONA QUE REALIZA LA TRANSACCION
        Xpinn.Caja.Entities.PersonaTransaccion perTran = new Xpinn.Caja.Entities.PersonaTransaccion();
        perTran.tipo_documento = Convert.ToInt64(ddlTipoDocPersona.SelectedValue);
        perTran.documento = txtDocPersona.Text == "" ? Convert.ToInt64(0) : Convert.ToInt64(txtDocPersona.Text);
        perTran.primer_nombre = txtPrimerNombrePersona.Text;
        perTran.segundo_nombre = txtSegundoNombrePersona.Text;
        perTran.primer_apellido = txtPrimerApellidoPersona.Text;
        perTran.segundo_apellido = txtSegundoApellidoPersona.Text;
        perTran.titular = chkTitular.Checked;

        // Realizar la aplicaciòn segùn lo seleccionado en la grilla de transacciones
        string Error = "";
        VerError("");
        tranCaja = ventanillaServicio.AplicarPagoVentanilla(tranCaja, perTran, gvTransacciones, gvFormaPago, gvCheques, txtObservacion.Text, (Usuario)Session["usuario"], ref Error);
        if (Error.Trim() != "")
        {
            if (Error.Length > 90)
                VerError(Error.Substring(0, 90));
            else
                VerError(Error);
            return false;
        }


        #region Interactuar Enpacto


        try
        {
            // Busco si esta habilitado las operaciones con Enpacto
            General general = ConsultarParametroGeneral(36);

            if (general != null && general.valor == "1")
            {
                HomologacionServices homologaService = new HomologacionServices();

                // Busco la homologacion de la cedula para los tipos de cedula de enpacto
                Homologacion homologacion = homologaService.ConsultarHomologacionTipoIdentificacion(ddlTipoIdentificacion.SelectedValue, Usuario);

                // Si no tengo los datos para homologar la cedula no hago nada y me voy
                if (homologacion != null && !string.IsNullOrWhiteSpace(homologacion.tipo_identificacion_enpacto))
                {
                    InterfazENPACTO interfazEnpacto = new InterfazENPACTO("0123456789ABCDEFFEDCBA9876543210", "00000000000000000000000000000000");

                    TarjetaService tarjetaService = new TarjetaService();
                    EnpactoServices enpactoService = new EnpactoServices();

                    // Reviso todas las transacciones para aplicar
                    foreach (GridViewRow fila in gvTransacciones.Rows)
                    {
                        string codigoTipoProducto = fila.Cells[4].Text;
                        TipoDeProducto tipoDeProducto = codigoTipoProducto.ToEnum<TipoDeProducto>();

                        // Si no soy ahorro vista no hago nada, siguiente vuelta
                        if (!(tipoDeProducto == TipoDeProducto.AhorrosVista || tipoDeProducto == TipoDeProducto.Credito))
                        {
                            continue;
                        }

                        string nroprod = Convert.ToString(fila.Cells[8].Text);
                        Tarjeta tarjetaDeLaPersona = tarjetaService.ConsultarTarjetaDeUnaCuenta(nroprod, Usuario);

                        // Si el numero de tarjeta no existe, voy a la siguiente vuelta
                        if (tarjetaDeLaPersona == null || string.IsNullOrWhiteSpace(tarjetaDeLaPersona.numtarjeta))
                        {
                            continue;
                        }

                        TransaccionEnpacto transaccionEnpacto = new TransaccionEnpacto();

                        long tipomov = long.Parse(fila.Cells[5].Text);
                        string nomtipomov = tipomov == 2 ? "INGRESO" : "EGRESO";

                        // Tipo movimiento = 2 (Deposito) - 1 = (Retiro)
                        if (tipomov == 2)
                        {
                            transaccionEnpacto.tipo = _tipoOperacionDepositoEnpacto;
                        }
                        else
                        {
                            transaccionEnpacto.tipo = _tipoOperacionRetiroEnpacto;
                        }

                        // Buildeo los datos para emitir la transaccion a enpacto
                        transaccionEnpacto.fecha = DateTime.Now.ToString("yyMMdd");
                        transaccionEnpacto.hora = DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2") + "00";
                        transaccionEnpacto.reverso = "false";
                        transaccionEnpacto.secuencia = tranCaja.cod_ope.ToString();
                        transaccionEnpacto.nombre = txtNombreCliente.Text;
                        transaccionEnpacto.identificacion = txtIdentificacion.Text;
                        transaccionEnpacto.tipo_identificacion = homologacion.tipo_identificacion_enpacto;
                        transaccionEnpacto.tarjeta = tarjetaDeLaPersona.numtarjeta;
                        transaccionEnpacto.cuenta = _convenio + tarjetaDeLaPersona.numero_cuenta;
                        transaccionEnpacto.tipo_cuenta = tarjetaDeLaPersona.tipo_cuenta;

                        decimal valor2 = decimal.Parse(fila.Cells[9].Text);
                        transaccionEnpacto.monto = (valor2 * 100).ToString();  // Sin carácter decimal, los últimos 2 dígitos son los centavos

                        RespuestaEnpacto respuesta = new RespuestaEnpacto();
                        string error = string.Empty;

                        try
                        {
                            // Mando a generar la transaccion de enpacto
                            string s_usuario_applicance = "webservice";
                            string s_clave_appliance = "WW.EE.99";
                            SeguridadConvenioTarjeta(ref s_usuario_applicance, ref s_clave_appliance);
                            interfazEnpacto.ConfiguracionAppliance(IpSwitchConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);
                            interfazEnpacto.GenerarTransaccionENPACTO(_convenio, transaccionEnpacto, false, ref respuesta, ref error);

                            if (string.IsNullOrWhiteSpace(error) && respuesta != null && respuesta.tran != null)
                            {
                                string fechaTransaccionFormato = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2");

                                Movimiento movimiento = new Movimiento
                                {
                                    fecha = fechaTransaccionFormato,
                                    hora = transaccionEnpacto.hora,
                                    documento = transaccionEnpacto.identificacion,
                                    nrocuenta = transaccionEnpacto.cuenta,
                                    tarjeta = transaccionEnpacto.tarjeta,
                                    tipotransaccion = transaccionEnpacto.tipo,
                                    descripcion = txtObservacion.Text,
                                    monto = Convert.ToDecimal(transaccionEnpacto.monto) / 100,
                                    lugar = Usuario.direccion,
                                    operacion = respuesta.tran.secuencia,
                                    comision = 0,
                                    red = "9",
                                    cod_ope = tranCaja.cod_ope,
                                    saldo_total = !string.IsNullOrWhiteSpace(respuesta.tran.saldo_total) ? Convert.ToDecimal(respuesta.tran.saldo_total) / 100 : default(decimal?),
                                    cod_cliente = tarjetaDeLaPersona.cod_persona
                                };

                                CuentaService cuentaService = new CuentaService();
                                cuentaService.CrearMovimiento(movimiento, tranCaja.cod_ope, Usuario);
                            }

                            respuesta.Error = error;

                            // Buildeo la entidad para la auditoria
                            Enpacto_Aud enpactoEntity = new Enpacto_Aud
                            {
                                exitoso = string.IsNullOrWhiteSpace(error) ? 1 : 0,
                                jsonentidadpeticion = JsonConvert.SerializeObject(transaccionEnpacto),
                                jsonentidadrespuesta = JsonConvert.SerializeObject(respuesta),
                                tipooperacion = 1 // 1- WebServices Transacciones
                            };

                            // Creo la auditoria para enpacto
                            enpactoService.CrearEnpacto_Aud(enpactoEntity, Usuario);
                        }
                        catch (Exception ex)
                        {
                            // Buildeo la entidad para la auditoria
                            Enpacto_Aud enpactoEntity = new Enpacto_Aud
                            {
                                exitoso = 0,
                                jsonentidadpeticion = JsonConvert.SerializeObject(transaccionEnpacto),
                                jsonentidadrespuesta = JsonConvert.SerializeObject(ex),
                                tipooperacion = 1 // 1- WebServices Transacciones
                            };

                            // Creo la auditoria para enpacto
                            enpactoService.CrearEnpacto_Aud(enpactoEntity, Usuario);
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            // Hacer algo si falla
        }


        #endregion


        if (ddlTipoProducto.SelectedValue == "6") // Si el tipo de Producto es Afiliacion
        {
            //actualizar los datos de LA PERSONA AFILIADA   
            //FALTA ACTUALIZAR EL SALDO DE LA PERSONA AFILIADA
        }

        // Se genera el comprobante
        DateTime fecha = Convert.ToDateTime(txtFechaTransaccion.Text);

        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = tranCaja.cod_ope;
        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 2;
        Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFechaTransaccion.Text);
        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = long.Parse(Session["codpersona"].ToString());
        Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = pUsuario.cod_oficina;
        Session[ComprobanteServicio.CodigoPrograma + ".ventanilla"] = "1";
        Session["cod_ope"] = tranCaja.cod_ope;
        //Para Imprimir Recibo Caja capturo los valores de las sessiones
        Session["CodigoOpe"] = tranCaja.cod_ope;
        Session[Usuario.codusuario + "codOpe"] = Convert.ToInt64(tranCaja.cod_ope);
        Session[Usuario.codusuario + "cod_persona"] = Convert.ToInt64(Session["codpersona"]);

        return true;
    }
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Response.Redirect("nuevo.aspx", false);
    }
    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnGuardar"), "Desea Aplicar los Pagos?");
    }
    /// <summary>
    /// Método para mostrar los datos de la persona
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Lblerror.Text = "";
        if (txtIdentificacion.Text != null && !string.Equals(txtIdentificacion.Text, ""))
        {
            Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
            //Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
            Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();

            persona.identificacion = txtIdentificacion.Text;
            persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
            VerError("");
            persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);
            persona.fecha_pago = Convert.ToDateTime(txtfechacorte.Text);
            if (persona.mensajer_error == "")
            {
                Session["codpersona"] = persona.cod_persona;
                txtNombreCliente.Text = persona.nom_persona;
                Session["DirecionAsc"] = persona.direccion + " - " + persona.telefono;
                if (txtNombreCliente.Text == " ")
                {
                    txtNombreCliente.Text = persona.razon_social;
                }
                // aqui se coloca los datos de la persona, Nro Radicacion, Nombre, Valor CUota, saldo capital
                Actualizar(persona);
                mvOperacion.Visible = true;
                txtIdentificacion.Enabled = false;
                ddlTipoIdentificacion.Enabled = false;
                if (ddlTipoProducto.SelectedValue == "100")
                {
                    Totalizar_Pago_Productos(persona);
                }
            }
            else
                VerError(persona.mensajer_error);
        }
        else
        {
            mvOperacion.Visible = false;
        }


    }
    /// <summary>
    /// Mostrar los datos de los productos según el tipo seleccionado
    /// </summary>
    /// <param name="pEntidad"></param>
    private void Actualizar(Xpinn.Caja.Entities.Persona pEntidad)
    {
        PersonaService personaService = new PersonaService();
        try
        {
            List<Xpinn.Caja.Entities.Persona> lstConsulta = new List<Xpinn.Caja.Entities.Persona>();

            gvConsultaDatos.DataSource = null;
            gvConsultaDatos.Visible = false;
            gvDatosAfiliacion.DataSource = null;
            gvDatosAfiliacion.Visible = false;
            gvAhorroVista.DataSource = null;
            gvAhorroVista.Visible = false;
            gvProgramado.DataSource = null;
            gvProgramado.Visible = false;
            gvCdat.DataSource = null;
            gvCdat.Visible = false;
            gvServicios.DataSource = null;
            gvServicios.Visible = false;
            divDatos.Visible = false;

            txtNumProducto.Text = "";
            txtValTransac.Text = "";
            ddlMonedas.SelectedIndex = 0;
            txtEstado.Text = "";

            TipoDeProducto tipoProducto = ddlTipoProducto.SelectedValue.ToEnum<TipoDeProducto>();

            if (tipoProducto == TipoDeProducto.Aporte) //APORTES
            {
                pEntidad.tipo_linea = Convert.ToInt64(Session["tipoproducto"]);
                txtReferencia.Visible = false;
                LblReferencia.Visible = false;
                lstConsulta = personaService.ListarDatosCreditoPersona(pEntidad, (Usuario)Session["usuario"]);
                if (lstConsulta.Count > 0)
                {
                    divDatos.Visible = true;
                    gvConsultaDatos.Visible = true;
                    gvConsultaDatos.DataSource = lstConsulta;
                    gvConsultaDatos.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.Credito) //CREDITOS
            {
                pEntidad.tipo_linea = Convert.ToInt64(Session["tipoproducto"]);
                txtReferencia.Visible = false;
                LblReferencia.Visible = false;

                lstConsulta = personaService.ListarDatosCreditoPersona(pEntidad, (Usuario)Session["usuario"]);

                if (lstConsulta.Count > 0)
                {
                    divDatos.Visible = true;
                    gvConsultaDatos.Visible = true;
                    gvConsultaDatos.DataSource = lstConsulta;
                    gvConsultaDatos.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.AhorrosVista) //AHORROS VISTA
            {
                Xpinn.Ahorros.Services.ReporteMovimientoServices ReporteMovService = new Xpinn.Ahorros.Services.ReporteMovimientoServices();
                List<Xpinn.Ahorros.Entities.AhorroVista> lstAhorros = new List<Xpinn.Ahorros.Entities.AhorroVista>();

                Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
                Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
                pData = ConsultaData.ConsultarGeneral(176, (Usuario)Session["usuario"]); // # parametro general indica si se muestras las cuentas inhabilitadas 
                String filtro = "";

                if (pData.valor != null && pData.valor != "" && pData.valor != "0")
                {
                    filtro = " WHERE A.ESTADO IN (0,1,2) AND A.COD_PERSONA = " + pEntidad.cod_persona + " ";
                }
                else
                {
                    filtro = " WHERE A.ESTADO IN (0,1) AND A.COD_PERSONA = " + pEntidad.cod_persona + " ";
                }

                DateTime pFechaApert;
                pFechaApert = DateTime.MinValue;
                lstAhorros = ReporteMovService.ListarAhorroVista(filtro, pFechaApert, (Usuario)Session["usuario"]);
                txtReferencia.Visible = true;
                LblReferencia.Visible = true;
                if (lstAhorros.Count > 0)
                {
                    gvAhorroVista.Visible = true;
                    gvAhorroVista.DataSource = lstAhorros;
                    gvAhorroVista.DataBind();
                    divDatos.Visible = true;
                }
            }
            else if (tipoProducto == TipoDeProducto.Servicios) //SERVICIOS
            {
                Xpinn.Servicios.Services.AprobacionServiciosServices AproServicios = new Xpinn.Servicios.Services.AprobacionServiciosServices();
                List<Xpinn.Servicios.Entities.Servicio> lstServicios = new List<Xpinn.Servicios.Entities.Servicio>();
                String filtro = " and S.COD_PERSONA = " + pEntidad.cod_persona + " AND S.ESTADO = 'C'   AND S.saldo !=0  ";

                string pOrden = "fecha_solicitud desc";

                txtReferencia.Visible = false;
                LblReferencia.Visible = false;
                DateTime? pFecPago = ConvertirStringToDateN(txtFechaTransaccion.Text);
                pFecPago = pFecPago != null ? pFecPago : DateTime.MinValue;
                lstServicios = AproServicios.ListarServicios(filtro, pOrden, DateTime.MinValue, Usuario, pFecPago);

                if (lstServicios.Count > 0)
                {
                    divDatos.Visible = true;
                    gvServicios.Visible = true;
                    gvServicios.DataSource = lstServicios;
                    gvServicios.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.CDATS) //CDATS
            {
                Xpinn.CDATS.Services.AperturaCDATService AperturaService = new Xpinn.CDATS.Services.AperturaCDATService();
                List<Xpinn.CDATS.Entities.Cdat> lstCdat = new List<Xpinn.CDATS.Entities.Cdat>();
                String filtro = " AND C.ESTADO = 1 and T.COD_PERSONA = " + pEntidad.cod_persona + " AND T.PRINCIPAL = 1 ";
                DateTime FechaApe;
                FechaApe = DateTime.MinValue;

                txtReferencia.Visible = false;
                LblReferencia.Visible = false;
                lstCdat = AperturaService.ListarCdats(filtro, FechaApe, (Usuario)Session["usuario"]);

                if (lstCdat.Count > 0)
                {
                    divDatos.Visible = true;
                    gvCdat.Visible = true;
                    gvCdat.DataSource = lstCdat;
                    gvCdat.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.Afiliacion)//AFILIACION
            {
                gvConsultaDatos.Visible = false;

                txtReferencia.Visible = false;
                LblReferencia.Visible = false;
                lstConsulta = personaService.ListarPersonasAfiliacion(pEntidad, (Usuario)Session["usuario"]);
                if (lstConsulta.Count > 0)
                {
                    divDatos.Visible = true;
                    gvDatosAfiliacion.Visible = true;
                    gvDatosAfiliacion.DataSource = lstConsulta;
                    gvDatosAfiliacion.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.Otros) //OTROS
            {
            }
            else if (tipoProducto == TipoDeProducto.Devoluciones) //DEVOLUCION
            {

            }
            else if (tipoProducto == TipoDeProducto.AhorroProgramado) //AHORRO PROGRAMADO
            {
                Xpinn.Programado.Services.CuentasProgramadoServices CuentasPrograServicios = new Xpinn.Programado.Services.CuentasProgramadoServices();
                List<Xpinn.Programado.Entities.CuentasProgramado> lstPrograma = new List<Xpinn.Programado.Entities.CuentasProgramado>();
                String filtro = " WHERE A.ESTADO = 1 AND A.COD_PERSONA = " + pEntidad.cod_persona + " ";
                DateTime pFecha = DateTime.MinValue;

                txtReferencia.Visible = false;
                LblReferencia.Visible = false;
                lstPrograma = CuentasPrograServicios.ListarAhorrosProgramado(filtro, pFecha, (Usuario)Session["usuario"]);

                if (lstPrograma.Count > 0)
                {
                    gvProgramado.DataSource = lstPrograma;
                    gvProgramado.DataBind();
                    divDatos.Visible = true;
                    gvProgramado.Visible = true;
                }
            }

            Session.Add(personaService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(personaService.GetType().Name + "L", "Actualizar", ex);
        }
    }
    protected void ddlTipoTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("");
        PersonaService personaService = new PersonaService();
        Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
        TipoDeProducto tipoProdcuto = ddlTipoProducto.SelectedValue.ToEnum<TipoDeProducto>();

        switch (tipoProdcuto)
        {
            case TipoDeProducto.Aporte:
                persona.linea_credito = "1";

                break;
            case TipoDeProducto.Credito:
                persona.linea_credito = "2";
                break;
            case TipoDeProducto.AhorrosVista:
                persona.linea_credito = "3";
                break;
            case TipoDeProducto.Servicios:
                break;
            case TipoDeProducto.CDATS:
                persona.linea_credito = "5";
                break;
            case TipoDeProducto.Afiliacion:
                break;
            case TipoDeProducto.Otros:
                break;
            case TipoDeProducto.Devoluciones:
                break;
            case TipoDeProducto.AhorroProgramado:
                persona.linea_credito = "9";
                break;
        }

        long tipoProductoLong = (long)tipoProdcuto;
        Session["tipoproducto"] = tipoProductoLong;
        LlenarComboTipoPago(tipoProductoLong);

        if (tipoProdcuto == TipoDeProducto.Otros)
        {
            lblMsgNroProducto.Text = "El número de producto no es obligatorio colocarlo en este tipo de transacción, colocar cero en el campo de número de producto ";
            txtNumProducto.Enabled = true;
        }
        else
            lblMsgNroProducto.Text = "";

        persona.identificacion = txtIdentificacion.Text;
        persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
        persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);
        persona.fecha_pago = Convert.ToDateTime(txtfechacorte.Text);

        Actualizar(persona);

        if (ddlTipoProducto.SelectedValue == "100")
        {
            Totalizar_Pago_Productos(persona);
            btnTotalTran.Visible = true;
            btnGoTran.Visible = false;
            gvtotal.Visible = true;
            chkMora.Visible = true;

        }
        else
        {
            btnTotalTran.Visible = false;
            btnGoTran.Visible = true;
            gvtotal.Visible = false;
            chkMora.Visible = false;
        }
    }
    protected void gvTransacciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["tablaSesion"];//se pilla las transacciones

            DataTable tableFP = new DataTable();
            tableFP = (DataTable)Session["tablaSesion2"];// se pilla las formas de pago
            decimal acum = 0;
            long result = 0;

            foreach (DataRow fila in tableFP.Rows)
            {   // si es efectivo y pesos
                if (long.Parse(fila[0].ToString()) == long.Parse(table.Rows[e.RowIndex][4].ToString()) && long.Parse(fila[1].ToString()) == 1)
                {
                    fila[2] = decimal.Parse(fila[2].ToString()) - decimal.Parse(table.Rows[e.RowIndex][3].ToString());
                    result = 0;
                }
                else
                {
                    result = 1;
                }

                if (result == 0)
                    acum = acum + decimal.Parse(fila[2].ToString());

            }

            gvFormaPago.DataSource = tableFP;
            gvFormaPago.DataBind();
            Session["tablaSesion2"] = tableFP;

            decimal valFormaPagoTotal = 0;

            valFormaPagoTotal = ConvertirStringADecimal(txtValTotalFormaPago.Text);
            txtValTotalFormaPago.Text = ConvertirDecimalAString(acum);

            txtValorTran.Text = ConvertirDecimalAString(ConvertirStringADecimal(txtValorTran.Text) - decimal.Parse(table.Rows[e.RowIndex][3].ToString()));

            table.Rows[e.RowIndex].Delete();
            gvTransacciones.DataSource = table;
            gvTransacciones.DataBind();
            Session["tablaSesion"] = table;
        }

        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ventanillaServicio.GetType().Name + "L", "gvTransacciones_RowDeleting", ex);
        }
    }
    protected void gvCheques_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["tablaSesion3"];//se pilla los Cheques


            DataTable tableFP = new DataTable();
            tableFP = (DataTable)Session["tablaSesion2"];// se pilla las formas de pago
            decimal acum = 0;
            long result = 1;

            foreach (DataRow fila in tableFP.Rows)
            {   // moneda y forma de pago

                if (long.Parse(fila[0].ToString()) == long.Parse(table.Rows[e.RowIndex][3].ToString()) && long.Parse(fila[1].ToString()) == 1)//Efectivo
                {
                    fila[2] = decimal.Parse(fila[2].ToString()) + decimal.Parse(table.Rows[e.RowIndex][2].ToString());
                    result = 0;
                }
                else if (long.Parse(fila[0].ToString()) == long.Parse(table.Rows[e.RowIndex][3].ToString()) && long.Parse(fila[1].ToString()) == 2)//Cheque
                {
                    fila[2] = decimal.Parse(fila[2].ToString()) - decimal.Parse(table.Rows[e.RowIndex][2].ToString());
                    result = 0;
                }
                else if (long.Parse(fila[0].ToString()) == long.Parse(table.Rows[e.RowIndex][3].ToString()) && long.Parse(fila[1].ToString()) != 1 && long.Parse(fila[1].ToString()) != 2)//Otros
                {
                    if (decimal.Parse(fila[2].ToString()) > 0)
                    {
                        fila[2] = decimal.Parse(fila[2].ToString());
                        result = 0;
                    }
                    else
                    {
                        result = 1;
                    }
                }
                else
                {
                    result = 1;
                }


                if (result == 0)
                    acum = acum + decimal.Parse(fila[2].ToString());

            }

            gvFormaPago.DataSource = tableFP;
            gvFormaPago.DataBind();
            Session["tablaSesion2"] = tableFP;

            decimal valFormaPagoTotal = 0;

            valFormaPagoTotal = ConvertirStringADecimal(txtValTotalFormaPago.Text);
            txtValTotalFormaPago.Text = ConvertirDecimalAString(acum);

            txtValTotalCheque.Text = Convert.ToString(decimal.Parse(txtValTotalCheque.Text) - decimal.Parse(table.Rows[e.RowIndex][2].ToString()));

            table.Rows[e.RowIndex].Delete();
            gvCheques.DataSource = table;
            gvCheques.DataBind();
            Session["tablaSesion3"] = table;
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ventanillaServicio.GetType().Name + "L", "gvTransacciones_RowDeleting", ex);
        }
    }
    protected void gvTransacciones_RowCommand(object sender, GridViewCommandEventArgs evt)
    {

        if (evt.CommandName == "DetallePago")
        {
            //MpeDetallePago
            int index = Convert.ToInt32(evt.CommandArgument);

            List<DetallePagos> ListaDetallePago = new List<DetallePagos>();

            GridViewRow gvTransaccionesRow = gvTransacciones.Rows[index];

            String stipo = gvTransacciones.Rows[index].Cells[6].Text;
            String stippro = gvTransacciones.Rows[index].Cells[7].Text;
            String snumpro = gvTransacciones.Rows[index].Cells[8].Text;
            String svalor = gvTransacciones.Rows[index].Cells[9].Text;
            String smoneda = gvTransacciones.Rows[index].Cells[10].Text;
            String referencia = gvTransacciones.Rows[index].Cells[11].Text;
            String stipopago = gvTransacciones.Rows[index].Cells[12].Text;





            if (stippro == "Cr&#233;ditos" || stippro == "Aportes" || stippro == "Afiliaci&#243;n")
            {
                try
                {
                    Configuracion global = new Configuracion();
                    string sseparador = global.ObtenerSeparadorMilesConfig();
                    decimal valor_pago = ConvertirStringADecimal(txtValorTran.Text);
                    DateTime fecha_pago = Convert.ToDateTime(txtFechaTransaccion.Text);
                    if (stippro == "Cr&#233;ditos")
                    {
                        stippro = "2";
                    }
                    if (stippro == "Aportes")
                    {
                        stippro = "1";
                    }
                    if (stippro == "Afiliaci&#243;n")
                        stippro = "8";
                    ListaDetallePago = DetallePagoService.DistribuirPago(Convert.ToInt64(stippro), Convert.ToInt64(snumpro), fecha_pago, Convert.ToDecimal(svalor), stipopago, (Usuario)Session["Usuario"]);

                    if (stippro == "1")
                    {
                        GvPagosAPortes.DataSource = ListaDetallePago;
                        GvPagosAPortes.DataBind();
                        MpeDetallePagoAportes.Show();
                    }
                    if (stippro == "2")
                    {
                        GVDetallePago.DataSource = ListaDetallePago;
                        GVDetallePago.DataBind();
                        MpeDetallePago.Show();
                    }
                    if (stippro == "8")
                    {
                        MpeDetallePago.Show();
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }
    }
    protected void btnCloseAct_Click(object sender, EventArgs e)
    {
        MpeDetallePago.Hide();
    }

    protected void btnCloseAct2_Click(object sender, EventArgs e)
    {
        MpeDetallePagoAportes.Hide();
    }


    /// <summary>
    /// Segùn el tipo de pago muestra los valores
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTipoPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtNumCuotas.Visible = false;
        lblNumeroCuotas.Visible = false;
        // Cuando el tipo de producto es crèditos calcular el valor segùn el tipo de pago
        if (ddlTipoProducto.SelectedValue == "2")
        {
            if (ddlTipoPago.SelectedValue == "40")
            {
                txtNumCuotas.Visible = true;
                lblNumeroCuotas.Visible = true;
            }
            // Segùn el tipo de pago ajustar los datos
            CalcularValordelCredito();
        }
        else if (ddlTipoProducto.SelectedValue == "4")
        {
            CalcularValordelServicio();
        }
        else if (ddlTipoProducto.SelectedValue == "5")
        {
            // Segùn el tipo de pago ajustar los datos
            CalcularValordelCdat();
        }
        else
        {
            txtValTransac.Enabled = true;
        }
    }

    private void CalcularValordelCdat()
    {
        foreach (GridViewRow fila in this.gvCdat.Rows)
        {
            Decimal saldoTotal = decimal.Parse(fila.Cells[11].Text);
            txtValTransac.Text = Convert.ToString(saldoTotal);

        }
    }

    private void CalcularValordelCredito()
    {
        Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
        user = (Usuario)Session["usuario"];

        // Ocultar controles para seleccionar los avances
        LblIdAvances.Visible = false;
        upAvances.Visible = false;

        // Determinar nùmero de crèdito
        Int64 numero_radicacion = 0;
        if (txtNumProducto.Text != "")
            numero_radicacion = Convert.ToInt64(txtNumProducto.Text);
        // ConsultaEstadoJuridico(numero_radicacion);
        // Determinar al fecha de pago
        DateTime fecha_pago = System.DateTime.Now;
        if (txtFechaTransaccion.Text != "")
            fecha_pago = Convert.ToDateTime(txtFechaTransaccion.Text);
        // Determinar nùmero de cuotas
        int numero_cuotas = 0;
        if (txtNumCuotas.Text == "")
            txtNumCuotas.Text = "1";
        if (txtNumCuotas.Text != "")
            numero_cuotas = Convert.ToInt32(txtNumCuotas.Text);

        txtValTransac.Enabled = true;

        // Segùn la forma de pago calcular el valor        
        if (ddlTipoPago.SelectedValue == "2")
        {
            // Cuando es pago total se calcula el valor total adeudado y se inactiva casilla de valor del pago.
            txtValTransac.Enabled = false;
            if (numero_radicacion != 0)
            {
                try
                {
                    decimal valor_apagar = CreditoServicio.AmortizarCredito(numero_radicacion, 2, fecha_pago, user);
                    txtValTransac.Text = valor_apagar.ToString();
                }
                catch
                {
                    txtValTransac.Text = "";
                    txtValTransac.Enabled = true;
                }
            }
            // En los rotativos habilitar para seleccionar el avance
            if (txtNumProducto.Text.Trim() != "")
            {
                // Determinar la línea del producto para poder saber si es un rotativo
                Xpinn.Caja.Entities.Persona lineacreditos = new Xpinn.Caja.Entities.Persona();
                Xpinn.Caja.Services.PersonaService PersonaServicio = new Xpinn.Caja.Services.PersonaService();
                lineacreditos = PersonaServicio.ConsultarDatosCreditoPersona(Convert.ToString(txtNumProducto.Text), (Usuario)Session["usuario"]);
                if (lineacreditos.tipo_linea == 2)
                {
                    LblIdAvances.Visible = true;
                    upAvances.Visible = true;
                }
                else
                {
                    txtAvances.Text = "";
                    LblIdAvances.Visible = false;
                    upAvances.Visible = false;
                }
            }
        }
        if (ddlTipoPago.SelectedValue == "11")
        {
            // Cuando son terminos fijos calcula el valor a pagar
            if (numero_radicacion != 0)
            {
                decimal valor_apagar = CreditoServicio.AmortizarCredito(numero_radicacion, 11, fecha_pago, user);
                txtValTransac.Text = valor_apagar.ToString();
            }
        }
        if (ddlTipoPago.SelectedValue == "40")
        {
            // Cuando es pago total se calcula el valor total adeudado y se inactiva casilla de valor del pago.
            txtValTransac.Enabled = false;
            if (numero_radicacion != 0)
            {
                try
                {
                    decimal valor_apagar = CreditoServicio.AmortizarCreditoNumCuotas(numero_radicacion, fecha_pago, numero_cuotas, user);
                    txtValTransac.Text = valor_apagar.ToString();
                }
                catch
                {
                    txtValTransac.Text = "";
                    txtValTransac.Enabled = true;
                }

            }
        }
    }
    private void CalcularValordelServicio()
    {
        // Determinar nùmero de crèdito
        Int64 num_producto = 0;
        if (txtNumProducto.Text != "")
            num_producto = Convert.ToInt64(txtNumProducto.Text);

        txtValTransac.Enabled = true;

        // Segùn la forma de pago calcular el valor        
        if (ddlTipoPago.SelectedValue == "34")
        {
            // Cuando es pago total se calcula el valor total adeudado y se inactiva casilla de valor del pago.
            txtValTransac.Enabled = false;
            if (num_producto != 0)
            {
                try
                {
                    //obteniendo el valor de la GridView por el Nro de Producto
                    ObtenerValorServicio(13, num_producto);
                }
                catch
                {
                    txtValTransac.Text = "";
                    txtValTransac.Enabled = true;
                }
            }
        }
        else
        {
            //SE COJE EL VALOR DE LA CUOTA
            ObtenerValorServicio(14, num_producto);
        }
    }
    protected decimal ObtenerValorServicio(int pIndice, Int64 pNum_producto)
    {
        decimal pReturn = 0;
        try
        {
            decimal pValor = 0;
            foreach (GridViewRow rFila in gvServicios.Rows)
            {
                Int64 ProdGrid = 0;
                if (rFila.Cells[1].Text != "&nbsp;")
                    ProdGrid = Convert.ToInt64(rFila.Cells[1].Text);
                if (ProdGrid != 0)
                {
                    if (ProdGrid == pNum_producto)
                    {
                        pValor = rFila.Cells[pIndice].Text != "&nbsp;" ? Convert.ToDecimal(rFila.Cells[pIndice].Text.Replace(".", "")) : 0;
                        txtValTransac.Text = pValor.ToString();
                        break;
                    }
                }
            }

            return pReturn = pValor;
        }
        catch
        {
            return pReturn = 0;
        }
    }
    /// <summary>
    /// Validar que cuando cambia el nùmero de producto se actualice el valor a pagar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtNumProducto_TextChanged(object sender, EventArgs e)
    {
        CalcularValordelCredito();
    }

    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        txtNumProducto.Text = evt.CommandName;
        txtValTransac.Text = evt.CommandArgument.ToString();

        Xpinn.Caja.Entities.Persona lineacreditos = new Xpinn.Caja.Entities.Persona();
        Xpinn.Caja.Services.PersonaService PersonaServicio = new Xpinn.Caja.Services.PersonaService();
        lineacreditos = PersonaServicio.ConsultarDatosCreditoPersona(Convert.ToString(txtNumProducto.Text), (Usuario)Session["usuario"]);
        //añadido para credito rotativo 
        Int64 linea = lineacreditos.tipo_linea;
        fecha = Convert.ToDateTime(lineacreditos.fecha_desembolso);
        //añadido para credito rotativo 
        Int64 linea2 = lineacreditos.tipo_linea;
        DateTime fechahoy = Convert.ToDateTime(txtFechaTransaccion.Text);
        DateTime fechapago = Convert.ToDateTime(lineacreditos.fecha_pago);
        // Para los rotativos validar la fecha de pago
        if (linea2 == 2 && fechapago > fechahoy)
        {
            LlenarComboTipoPagoRotativo(Convert.ToInt64(ddlTipoProducto.SelectedValue));
            Session["TipoProductoRotativo"] = ddlTipoProducto.SelectedValue;
        }
        if (linea2 == 2 && fechapago < fechahoy)
        {
            LlenarComboTipoPagoRotativo1(Convert.ToInt64(ddlTipoProducto.SelectedValue));
            Session["TipoProductoRotativo"] = ddlTipoProducto.SelectedValue;
        }
        if (linea2 != 2)
            LlenarComboTipoPago(Convert.ToInt64(ddlTipoProducto.SelectedValue));


        Session["fecha"] = fecha;
    }

    protected void gvLista_PageIndexChanging(object sender, System.EventArgs e)
    {

    }

    /// <summary>
    /// Método para control de forma de pago seleccionada
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblBancoConsignacion.Visible = false;
        ddlBancoConsignacion.Visible = false;
        lblProcesoContable.Visible = false;
        ddlProcesoContable.Visible = false;
        lblBancoConsignacion.Text = "&nbsp;&nbsp;&nbsp;";
        if (ddlFormaPago.SelectedValue == "5")
        {
            lblBancoConsignacion.Visible = true;
            ddlBancoConsignacion.Visible = true;
        }
        if (ddlFormaPago.SelectedValue == "9")
        {
            lblProcesoContable.Visible = true;
            ddlProcesoContable.Visible = true;
        }
        if (ddlFormaPago.SelectedValue == "10")
        {
            lblboucher.Visible = true;
            txtBaucher.Visible = true;
        }
        else
        {
            lblboucher.Visible = false;
            txtBaucher.Visible = false;
        }
    }
    protected void gvConsultaDatos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Método para la búsqueda rápida de personas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "", "txtIdentificacion", "ddlTipoIdentificacion", "txtNombreCliente");
    }

    protected void txtFechaTransaccion_DataBinding(object sender, EventArgs e)
    {

    }
    protected void txtFechaTransaccion_TextChanged(object sender, EventArgs e)
    {
        txtFechaCont.Text = txtFechaTransaccion.Text;
        VerError("");
        if (Convert.ToDateTime(txtFechaTransaccion.Text) > DateTime.Now)
        {
            txtFechaTransaccion.Text = DateTime.Now.ToShortDateString();
            VerError("La fecha de transacción no puede ser mayor a la fecha actual");
        }
    }


    protected void txtFechaCorte_TextChanged(object sender, EventArgs e)
    {
        btnConsultar_Click(sender, e);
    }

    //AGREGADO PAGOS POR AFILIACION
    protected void gvDatosAfiliacion_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        string id = gvDatosAfiliacion.DataKeys[e.NewEditIndex].Values[0].ToString();
        txtNumProducto.Text = id;
        e.NewEditIndex = -1;
    }

    //AHORRO VISTA
    protected void gvAhorroVista_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        string id = gvAhorroVista.DataKeys[e.NewEditIndex].Values[0].ToString();
        txtNumProducto.Text = id;
        e.NewEditIndex = -1;
    }

    //AHORRO PROGRAMADO
    protected void gvProgramado_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        string id = gvProgramado.DataKeys[e.NewEditIndex].Values[0].ToString();
        txtNumProducto.Text = id;
        e.NewEditIndex = -1;
    }

    //CDATS
    protected void gvCdat_RowEditing(object sender, GridViewEditEventArgs e)
    {

        VerError("");
        string id = gvCdat.DataKeys[e.NewEditIndex].Values[0].ToString();
        txtNumProducto.Text = id;
        String saldoTotal = gvCdat.Rows[e.NewEditIndex].Cells[11].Text;
        String Valorparcial = gvCdat.Rows[e.NewEditIndex].Cells[12].Text;
        decimal saldo_Total = Convert.ToDecimal(saldoTotal);
        decimal Valor_parcial = Convert.ToDecimal(Valorparcial);
        decimal Valor_Pendiente = 0;
        if (Valor_parcial < saldo_Total)
        {
            Valor_Pendiente = Convert.ToDecimal(saldo_Total - Valor_parcial);
            txtValTransac.Text = Convert.ToString(Valor_Pendiente);
        }

        txtValTransac.Enabled = false;
        e.NewEditIndex = -1;
    }

    //SERVICIOS
    protected void gvServicios_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvServicios.DataKeys[e.NewEditIndex].Values[0].ToString();
        txtNumProducto.Text = id;
        e.NewEditIndex = -1;
    }


    #region VALIDACION DE PROCESO

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        panelEncabezado.Visible = true;
        mvOperacion.Visible = true;
        panelProceso.Visible = false;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            if (AplicarPago())
            {
                throw new Exception("No se pudo aplicar el pago");
            }

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    #endregion

    protected decimal ConvertirStringADecimal(string pvalor)
    {
        if (pvalor.Trim() == "")
            return 0;
        try { return Convert.ToDecimal(pvalor.Replace(".", "").Replace(",", GlobalWeb.gSeparadorDecimal)); }
        catch { return 0; }

    }
    protected string ConvertirDecimalAString(decimal pvalor)
    {
        decimal enteros = Decimal.Truncate(pvalor);
        decimal decimales = (pvalor - enteros) * 100;
        string svalor;
        if (decimales > 0)
            svalor = enteros.ToString("##############") + gSeparadorDecimal + decimales.ToString("00");
        else
            svalor = enteros.ToString("##############") + gSeparadorDecimal + "00";
        Lblerror.Text = gSeparadorDecimal;
        if (!svalor.Contains(gSeparadorDecimal))
            svalor += (gSeparadorDecimal + "00");
        return svalor;
    }
    protected void btnImprimiendose_Click()
    {
        var Cod_ope = Convert.ToInt64(Session["cod_ope"]);
        if (Cod_ope == 0)
        {
            if (RpviewInfo.Visible == true)
            {
                //MOSTRAR REPORTE EN PANTALLA
                RpviewInfo.LocalReport.EnableExternalImages = true;
                var bytes = RpviewInfo.LocalReport.Render("PDF");
                MostrarArchivoEnLiteralL(bytes, Cod_ope);
                RpviewInfo.Visible = false;
                GeneradorDocumento.Show();
                mpePopUp.Visible = true;
            }
        }
        else
        {
            RpviewInfo.LocalReport.EnableExternalImages = true;
            var bytes = RpviewInfo.LocalReport.Render("PDF");
            MostrarArchivoEnLiteralL(bytes, Cod_ope);
        }

    }
    private void MostrarArchivoEnLiteralL(byte[] bytes, long cod_op)
    {
        Usuario pUsuario = Usuario;

        if (cod_op != 0)
        {
            pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_Declaracion_" + cod_op : "";
        }
        else
        {
            pNomUsuarios = pUsuario.nombre != "" && pUsuario.nombre != null ? "_Declaracion_0" : "";
        }
        // ELIMINANDO ARCHIVOS GENERADOS
        try
        {
            string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
            foreach (string ficheroActual in ficherosCarpeta)
                if (cod_op != 0)
                {
                    if (ficheroActual.Contains(pNomUsuarios))
                        File.Delete(ficheroActual);
                }
                else
                {
                    if (ficheroActual.Contains(pNomUsuario))
                        File.Delete(ficheroActual);
                }

        }
        catch
        { }

        FileStream fs = cod_op != 0 ? new FileStream(HttpContext.Current.Server.MapPath("Archivos/output" + pNomUsuario + ".pdf"),
        FileMode.Create) : new FileStream(HttpContext.Current.Server.MapPath("Archivos/output" + pNomUsuarios + ".pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        //MOSTRANDO REPORTE
        string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"510pc\">";
        adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
        adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        adjuntar += "</object>";

        LiteralDcl.Text = cod_op != 0 ? string.Format(adjuntar, ResolveUrl("Archivos/output" + pNomUsuario + ".pdf")) : string.Format(adjuntar, ResolveUrl("Archivos/output" + pNomUsuarios + ".pdf"));
        LiteralDcl.Visible = true;
    }
    protected void txtNumCuotas_TextChanged(object sender, EventArgs e)
    {
        CalcularValordelCredito();
    }
    //Agregado para enviar correo de notifación al asociado cuando se realiza un pago
    protected void EnviarCorreo()
    {
        try
        {
            VerError("");
            Usuario pUsuario = (Usuario)Session["usuario"];
            Xpinn.Asesores.Services.TiposDocCobranzasServices _tipoDocumentoServicio = new Xpinn.Asesores.Services.TiposDocCobranzasServices();
            Empresa empresa = _tipoDocumentoServicio.ConsultarCorreoEmpresa(pUsuario.idEmpresa, pUsuario);
            Xpinn.Asesores.Entities.Persona persona = _tipoDocumentoServicio.ConsultarCorreoPersona(Convert.ToInt64(Session["codpersona"].ToString()), pUsuario);

            ParametroCorreo parametroCorreo = (ParametroCorreo)Enum.Parse(typeof(ParametroCorreo), ((int)TipoDocumentoCorreo.PagosPorVentanilla).ToString());

            Xpinn.Asesores.Entities.TiposDocCobranzas modificardocumento = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo((int)parametroCorreo, pUsuario);

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
            bool exitoso = correoHelper.EnviarCorreoConHTML(modificardocumento.texto, Correo.Gmail, modificardocumento.descripcion, pUsuario.empresa);

            if (!exitoso)
            {
                VerError("Error al enviar el correo de notificación");
                return;
            }
        }
        catch (Exception ex)
        {
            VerError("Error al enviar el correo, " + ex.Message);
        }
    }
    private void LlenarDiccionarioGlobalWebParaCorreo()
    {
        parametrosFormatoCorreo = new Dictionary<ParametroCorreo, string>();

        parametrosFormatoCorreo.Add(ParametroCorreo.FechaPago, txtFechaReal.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.Identificacion, txtIdentificacion.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.NombreCompletoPersona, txtNombreCliente.Text);
        parametrosFormatoCorreo.Add(ParametroCorreo.OficinaPago, txtOficina.Text);

    }

    #region Total_Pago
    void Totalizar_Pago_Productos(Xpinn.Caja.Entities.Persona pEntidad)
    {
        PersonaService personaService = new PersonaService();
        List<Xpinn.Caja.Entities.Persona> lstConsulta_Aporte = new List<Xpinn.Caja.Entities.Persona>();
        List<Xpinn.Caja.Entities.Persona> lstConsulta_Credito = new List<Xpinn.Caja.Entities.Persona>();
        List<Xpinn.Caja.Entities.Persona> lstAfiliacion = new List<Xpinn.Caja.Entities.Persona>();
        Decimal Valor_Total = 0;
        pEntidad.fecha_pago = Convert.ToDateTime(txtfechacorte.Text);
        //Aportes
        pEntidad.tipo_linea = Convert.ToInt64(1);
        txtReferencia.Visible = false;
        LblReferencia.Visible = false;
        lstConsulta_Aporte = personaService.ListarDatosCreditoPersona(pEntidad, (Usuario)Session["usuario"]);

        //Creditos
        pEntidad.tipo_linea = Convert.ToInt64(2);
        pEntidad.fecha_pago = ConvertirStringToDateN(txtfechacorte.Text);
        txtReferencia.Visible = false;
        LblReferencia.Visible = false;
        lstConsulta_Credito = personaService.ListarDatosCreditoPersona(pEntidad, (Usuario)Session["usuario"]);

        //Servicios
        Xpinn.Servicios.Services.AprobacionServiciosServices AproServicios = new Xpinn.Servicios.Services.AprobacionServiciosServices();
        List<Xpinn.Servicios.Entities.Servicio> lstServicios = new List<Xpinn.Servicios.Entities.Servicio>();
        String filtro = " and S.COD_PERSONA = " + pEntidad.cod_persona + " AND S.ESTADO = 'C' ";
        string pOrden = "fecha_solicitud desc";
        txtReferencia.Visible = false;
        LblReferencia.Visible = false;
        DateTime? pFecPago = ConvertirStringToDateN(txtfechacorte.Text);
        pFecPago = pFecPago != null ? pFecPago : DateTime.MinValue;
        lstServicios = AproServicios.ListarServicios(filtro, pOrden, DateTime.MinValue, Usuario, pFecPago);

        //Ahorros Vista
        Xpinn.Ahorros.Services.ReporteMovimientoServices ReporteMovService = new Xpinn.Ahorros.Services.ReporteMovimientoServices();
        List<Xpinn.Ahorros.Entities.AhorroVista> lstAhorros = new List<Xpinn.Ahorros.Entities.AhorroVista>();
        String filtroAh = " WHERE A.ESTADO IN (0,1) AND A.COD_PERSONA = " + pEntidad.cod_persona + " ";
        DateTime pFechaApert;
        pFechaApert = DateTime.MinValue;
        lstAhorros = ReporteMovService.ListarAhorroVista(filtroAh, pFechaApert, (Usuario)Session["usuario"]);

        //Ahorro Programado
        Xpinn.Programado.Services.CuentasProgramadoServices CuentasPrograServicios = new Xpinn.Programado.Services.CuentasProgramadoServices();
        List<Xpinn.Programado.Entities.CuentasProgramado> lstPrograma = new List<Xpinn.Programado.Entities.CuentasProgramado>();
        String filtroAhp = " WHERE A.ESTADO = 1 AND A.COD_PERSONA = " + pEntidad.cod_persona + " ";
        DateTime pFecha = DateTime.MinValue;
        txtReferencia.Visible = false;
        LblReferencia.Visible = false;
        lstPrograma = CuentasPrograServicios.ListarAhorrosProgramado(filtroAhp, pFecha, (Usuario)Session["usuario"]);

        //Afiliación
        lstAfiliacion = personaService.ListarPersonasAfiliacion(pEntidad, (Usuario)Session["usuario"]);

        //Lista general 
        List<Xpinn.Caja.Entities.Persona> ListaTotal = new List<Xpinn.Caja.Entities.Persona>();
        // Insertamos Aportes a la lista general
        if (lstConsulta_Aporte.Count > 0)
        {
            foreach (Xpinn.Caja.Entities.Persona elem in lstConsulta_Aporte)
            {
                Xpinn.Caja.Entities.Persona Aporte = new Xpinn.Caja.Entities.Persona();
                Aporte.tipo_producto = "Aportes";
                Aporte.numero_radicacion = elem.numero_radicacion;
                Aporte.linea_credito = elem.linea_credito;
                Aporte.Dias_mora = elem.Dias_mora;
                Aporte.fecha_aprobacion = elem.fecha_aprobacion;
                Aporte.valor_cuota = elem.valor_cuota;
                Aporte.saldo_capital = elem.saldo_capital;
                Aporte.fecha_proxima_pago = elem.fecha_proxima_pago;
                Aporte.total_a_pagar = elem.total_a_pagar;
                Valor_Total = Valor_Total + elem.total_a_pagar;
                ListaTotal.Add(Aporte);
            }
        }

        // Insertamos Creditos a la lista general
        if (lstConsulta_Credito.Count > 0)
        {
            foreach (Xpinn.Caja.Entities.Persona elem in lstConsulta_Credito)
            {
                Xpinn.Caja.Entities.Persona Credito = new Xpinn.Caja.Entities.Persona();
                Credito.tipo_producto = "Creditos";
                Credito.numero_radicacion = elem.numero_radicacion;
                Credito.linea_credito = elem.linea_credito;
                Credito.Dias_mora = elem.Dias_mora;
                Credito.fecha_aprobacion = elem.fecha_aprobacion;
                Credito.valor_cuota = elem.valor_cuota;
                Credito.saldo_capital = elem.saldo_capital;
                Credito.fecha_proxima_pago = elem.fecha_proxima_pago;
                Credito.total_a_pagar = elem.valor_a_pagar;
                Credito.valor_CE = elem.valor_CE;
                Valor_Total = Valor_Total + elem.valor_a_pagar + elem.valor_CE;

                ListaTotal.Add(Credito);
            }
        }

        //Insertamos Servicios a la lista general
        if (lstServicios.Count > 0)
        {
            foreach (Xpinn.Servicios.Entities.Servicio elem in lstServicios)
            {
                Xpinn.Caja.Entities.Persona Servicios = new Xpinn.Caja.Entities.Persona();
                Servicios.tipo_producto = "Servicios";
                Servicios.numero_radicacion = elem.numero_servicio;
                Servicios.linea_credito = elem.nom_linea;
                Servicios.Dias_mora = "0";
                Servicios.fecha_aprobacion = Convert.ToDateTime(elem.fecha_inicio_vigencia);
                Servicios.valor_cuota = Convert.ToInt64(elem.valor_cuota);
                Servicios.saldo_capital = Convert.ToInt64(elem.saldo);
                Servicios.fecha_proxima_pago = Convert.ToDateTime(elem.fecha_proximo_pago);
                Servicios.total_a_pagar = Convert.ToInt64(elem.interes_corriente);
                Valor_Total = Valor_Total + Convert.ToInt64(elem.interes_corriente);
                ListaTotal.Add(Servicios);
            }
        }

        //Insertamos Ahorros a la vista a la lista general
        if (lstAhorros.Count > 0)
        {
            foreach (Xpinn.Ahorros.Entities.AhorroVista elem in lstAhorros)
            {
                Xpinn.Caja.Entities.Persona AhVista = new Xpinn.Caja.Entities.Persona();
                AhVista.tipo_producto = "Ahorros";
                AhVista.numero_radicacion = Convert.ToInt64(elem.numero_cuenta);
                AhVista.linea_credito = elem.nom_linea;
                //Calulo dias mora                
                TimeSpan dias = Convert.ToDateTime(txtFechaTransaccion.Text).Subtract(Convert.ToDateTime(elem.fecha_proximo_pago));
                string dia = dias.Days.ToString();
                AhVista.Dias_mora = Convert.ToString(elem.dias);
                AhVista.fecha_aprobacion = Convert.ToDateTime(elem.fecha_apertura);
                AhVista.valor_cuota = Convert.ToInt64(elem.valor_cuota);
                AhVista.saldo_capital = Convert.ToInt64(elem.saldo_total);
                AhVista.fecha_proxima_pago = Convert.ToDateTime(elem.fecha_proximo_pago);
                AhVista.total_a_pagar = Convert.ToInt64(elem.valor_cuota);
                Valor_Total = Valor_Total + Convert.ToInt64(elem.valor_cuota);
                ListaTotal.Add(AhVista);
            }
        }

        //Insertamos Ahorros programados a la lista general
        if (lstPrograma.Count > 0)
        {
            foreach (Xpinn.Programado.Entities.CuentasProgramado elem in lstPrograma)
            {
                Xpinn.Caja.Entities.Persona AhProg = new Xpinn.Caja.Entities.Persona();
                AhProg.tipo_producto = "Ahorro Programado";
                AhProg.numero_radicacion = Convert.ToInt64(elem.numero_programado);
                AhProg.linea_credito = elem.nom_linea;
                AhProg.Dias_mora = Convert.ToString(elem.numero_dias);
                AhProg.fecha_aprobacion = elem.fecha_apertura;
                AhProg.valor_cuota = Convert.ToInt64(elem.valor_cuota);
                AhProg.saldo_capital = Convert.ToInt64(elem.saldo);
                AhProg.fecha_proxima_pago = Convert.ToDateTime(elem.fecha_proximo_pago);
                AhProg.total_a_pagar = Convert.ToInt64(elem.valor_cuota);
                Valor_Total = Valor_Total + Convert.ToInt64(elem.valor_cuota);
                ListaTotal.Add(AhProg);
            }
        }
        if (lstAfiliacion.Count > 0)
        {
            foreach (Xpinn.Caja.Entities.Persona elem in lstAfiliacion)
            {
                Xpinn.Caja.Entities.Persona Afili = new Xpinn.Caja.Entities.Persona();
                Afili.tipo_producto = "Afiliación";
                Afili.numero_radicacion = Convert.ToInt64(elem.idafiliacion);
                Afili.linea_credito = "AFILIACION";
                Afili.Dias_mora = Convert.ToString(elem.Dias_mora);
                Afili.fecha_aprobacion = Convert.ToDateTime(elem.fecha_afiliacion);
                Afili.valor_cuota = Convert.ToInt64(elem.valor);
                Afili.saldo_capital = Convert.ToInt64(elem.saldo);
                Afili.fecha_proxima_pago = Convert.ToDateTime(elem.fecha_proxima_pago);
                Afili.total_a_pagar = Convert.ToInt64(elem.valor_a_pagar);
                Valor_Total = Valor_Total + Convert.ToInt64(elem.valor);
                ListaTotal.Add(Afili);
            }
        }

        if (ListaTotal.Count > 0)
        {
            divDatos.Visible = true;
            gvtotal.Visible = true;
            gvtotal.DataSource = ListaTotal;
            Session["ListaProductos"] = ListaTotal;
            gvtotal.DataBind();

            Decimal btnTotalTran = 0;
            foreach (GridViewRow row in gvtotal.Rows)
            {
                decimal valorPF = Convert.ToDecimal(row.Cells[9].Text);
                decimal valorCE = Convert.ToDecimal(row.Cells[10].Text);
                DateTime Fecha = Convert.ToDateTime(row.Cells[8].Text);
                if ((valorPF > 0 || valorCE > 0) && Fecha <= Convert.ToDateTime(txtFechaTransaccion.Text))
                {
                    CheckBox cb = (CheckBox)row.FindControl("CheckBoxgv");
                    cb.Checked = true;
                    btnTotalTran = btnTotalTran + valorPF + valorCE;
                    txtValTransac.Text = Convert.ToString(btnTotalTran);
                }
            }
        }

    }
    protected void Cargar_trans2(object sender, EventArgs e)
    {
        List<Xpinn.Caja.Entities.Persona> ListaTotal = new List<Xpinn.Caja.Entities.Persona>();
        ListaTotal = (List<Xpinn.Caja.Entities.Persona>)Session["ListaProductos"];
        decimal Valor_tran_total = Convert.ToDecimal(txtValTransac.Text);

        //gvTransacciones
        gvTransacciones.DataSource = null;
        gvTransacciones.DataBind();

        if (ddlTipoProducto.SelectedValue == "100")
        {
            if (ListaTotal.Count > 0)

                foreach (Xpinn.Caja.Entities.Persona elem in ListaTotal)
                {
                    if (elem.total_a_pagar > 0 && Valor_tran_total > 0)
                    {

                        txtNumProducto.Text = elem.numero_radicacion.ToString();
                        txtValTransac.Text = Convert.ToString(elem.total_a_pagar);

                        if (Valor_tran_total < elem.total_a_pagar)
                        {
                            if (Valor_tran_total > 0)
                            {
                                txtValTransac.Text = Convert.ToString(Valor_tran_total);
                            }
                            else
                            {
                                VerError("El valor total a pagar no alcanza para todos los productos");
                            }
                        }
                        Valor_tran_total = Valor_tran_total - elem.total_a_pagar;
                        if (elem.tipo_producto == "Aportes")
                        {
                            LlenarComboTipoPago(1);
                            ddlTipoPago.SelectedIndex = 0;
                        }
                        else if (elem.tipo_producto == "Creditos")
                        {

                            LlenarComboTipoPago(2);
                            ddlTipoPago.SelectedIndex = 1;
                        }
                        else if (elem.tipo_producto == "Servicios")
                        {
                            LlenarComboTipoPago(4);
                            ddlTipoPago.SelectedIndex = 0;
                        }
                        else if (elem.tipo_producto == "Ahorros")
                        {
                            LlenarComboTipoPago(3);
                            ddlTipoPago.SelectedIndex = 2;
                        }
                        else if (elem.tipo_producto == "Ahorro Programado")
                        {
                            LlenarComboTipoPago(9);
                            ddlTipoPago.SelectedIndex = 1;
                        }
                        else if (elem.tipo_producto == "Afiliación")
                        {
                            LlenarComboTipoPago(6);
                            ddlTipoPago.SelectedIndex = 0;
                        }

                        btnGoTran_Click(null, null);



                    }
                }

        }

        ddlTipoPago.Items.Clear();
        txtNumProducto.Text = "";
        txtValor.Text = "";
    }

    protected void Cargar_trans(object sender, EventArgs e)
    {
        decimal Valor_tran_total = Convert.ToDecimal(txtValTransac.Text);
        //gvTransacciones
        gvTransacciones.DataSource = null;
        gvTransacciones.DataBind();

        if (ddlTipoProducto.SelectedValue == "100")
        {
            if (!chkMora.Checked)
            {
                foreach (GridViewRow row in gvtotal.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("CheckBoxgv");

                    if (chk.Checked == true)
                    {
                        Xpinn.Caja.Entities.Persona elem = new Xpinn.Caja.Entities.Persona();
                        elem.total_a_pagar = Convert.ToInt64(row.Cells[9].Text.Replace(".", ""));
                        elem.numero_radicacion = Convert.ToInt64(row.Cells[2].Text);
                        elem.tipo_producto = Convert.ToString(row.Cells[1].Text);
                        elem.valor_CE = Convert.ToInt64(row.Cells[10].Text.Replace(".", ""));
                        Xpinn.Caja.Entities.Persona lineacreditos = new Xpinn.Caja.Entities.Persona();
                        Xpinn.Caja.Services.PersonaService PersonaServicio = new Xpinn.Caja.Services.PersonaService();
                        lineacreditos = PersonaServicio.ConsultarDatosCreditoPersona(Convert.ToString(elem.numero_radicacion), (Usuario)Session["usuario"]);
                        elem.tipo_linea = lineacreditos.tipo_linea;

                        if (elem.total_a_pagar > 0 && Valor_tran_total > 0)
                        {

                            txtNumProducto.Text = elem.numero_radicacion.ToString();
                            txtValTransac.Text = Convert.ToString(elem.total_a_pagar);

                            if (Valor_tran_total < elem.total_a_pagar)
                            {
                                if (Valor_tran_total > 0)
                                {
                                    txtValTransac.Text = Convert.ToString(Valor_tran_total);
                                }
                                else
                                {
                                    VerError("El valor total a pagar no alcanza para todos los productos");
                                }
                            }
                            Valor_tran_total = Valor_tran_total - elem.total_a_pagar;
                            if (elem.tipo_producto == "Aportes")
                            {
                                LlenarComboTipoPago(1);
                                ddlTipoPago.SelectedIndex = 0;
                            }
                            else if (elem.tipo_producto == "Creditos")
                            {
                                if (elem.tipo_linea == 2)
                                {
                                    LlenarComboTipoPagoRotativo(Convert.ToInt64(2));
                                    ddlTipoPago.SelectedIndex = 1;
                                }
                                else
                                {
                                    LlenarComboTipoPago(2);
                                    ddlTipoPago.SelectedIndex = 1;
                                }
                            }
                            else if (elem.tipo_producto == "Servicios")
                            {
                                LlenarComboTipoPago(4);
                                ddlTipoPago.SelectedIndex = 0;
                            }
                            else if (elem.tipo_producto == "Ahorros")
                            {
                                LlenarComboTipoPago(3);
                                ddlTipoPago.SelectedIndex = 1;
                            }
                            else if (elem.tipo_producto == "Ahorro Programado")
                            {
                                LlenarComboTipoPago(9);
                                ddlTipoPago.SelectedIndex = 1;
                            }
                            else if (elem.tipo_producto == "Afiliación")
                            {
                                LlenarComboTipoPago(6);
                                ddlTipoPago.SelectedIndex = 0;
                            }

                            btnGoTran_Click(null, null);

                            if (elem.tipo_producto == "Creditos")
                            {
                                if (elem.valor_CE != 0)
                                {
                                    elem.valor_a_pagar = long.Parse(elem.valor_CE.ToString());
                                    LlenarComboTipoPago(2);
                                    ddlTipoPago.SelectedIndex = 3;
                                    txtNumProducto.Text = elem.numero_radicacion.ToString();
                                    txtValTransac.Text = Convert.ToString(elem.valor_a_pagar);

                                    if (Valor_tran_total < elem.total_a_pagar)
                                    {
                                        if (Valor_tran_total > 0)
                                        {
                                            txtValTransac.Text = Convert.ToString(Valor_tran_total);
                                        }
                                        else
                                        {
                                            VerError("El valor total a pagar no alcanza para todos los productos");
                                        }
                                    }
                                    Valor_tran_total = Valor_tran_total - Convert.ToDecimal(txtValTransac.Text);
                                    Session["CuotaExtra"] = 1;
                                    btnGoTran_Click(null, null);
                                    Session["CuotaExtra"] = null;

                                }
                            }
                        }
                    }
                }
            }

            else
            {

                Xpinn.Asesores.Services.CreditosService serviciosMoras = new Xpinn.Asesores.Services.CreditosService();
                List<Xpinn.Asesores.Entities.ProductosMora> lstConsulta = new List<Xpinn.Asesores.Entities.ProductosMora>();
                lstConsulta = serviciosMoras.ConsultarDetalleMoraPersona(Session["codpersona"].ToString(), "", Convert.ToDateTime(txtfechacorte.Text), "1,2,4,3,6", (Usuario)Session["usuario"]);
                List<Xpinn.Asesores.Entities.ProductosMora> ListConsolidado = new List<Xpinn.Asesores.Entities.ProductosMora>(); ;

                List<Xpinn.Asesores.Entities.ProductosMora> List = lstConsulta.OrderBy(x => x.fecha_vencimento)
                  .ToList();

                decimal Valor_Total_Pago = Convert.ToDecimal(txtValTransac.Text);

                foreach (ProductosMora row in List)
                {
                    if (row.saldo_total > 0 && Valor_Total_Pago > 0)
                    {

                        if (Valor_Total_Pago < row.saldo_total)
                        {
                            if (Valor_Total_Pago > 0)
                            {
                                row.saldo_total = Valor_Total_Pago;

                            }
                        }
                        ListConsolidado.Add(row);
                        Valor_Total_Pago = Valor_Total_Pago - Convert.ToDecimal(row.saldo_total);
                    }
                }


                ListConsolidado = ListConsolidado.GroupBy(d => new { d.cod_persona, d.numero_producto, d.tipo_producto, d.descripcion })
                 .Select(g => new ProductosMora()
                 {
                     cod_persona = g.First().cod_persona,
                     numero_producto = g.First().numero_producto,
                     tipo_producto = g.First().tipo_producto,
                     descripcion = g.First().descripcion,
                     saldo_total = g.Sum(d => d.saldo_total)

                 }).ToList();


                foreach (ProductosMora row in ListConsolidado)
                {
                    Xpinn.Caja.Entities.Persona elem = new Xpinn.Caja.Entities.Persona();
                    elem.total_a_pagar = Convert.ToInt64(row.saldo_total);
                    elem.numero_radicacion = Convert.ToInt64(row.numero_producto);
                    elem.tipo_producto = Convert.ToString(row.tipo_producto);
                    Xpinn.Caja.Entities.Persona lineacreditos = new Xpinn.Caja.Entities.Persona();
                    Xpinn.Caja.Services.PersonaService PersonaServicio = new Xpinn.Caja.Services.PersonaService();
                    lineacreditos = PersonaServicio.ConsultarDatosCreditoPersona(Convert.ToString(row.numero_producto), (Usuario)Session["usuario"]);
                    long tipo_linea = lineacreditos.tipo_linea;
                    TipoOperacionService tipoService = new TipoOperacionService();
                    Xpinn.Caja.Entities.TipoProducto producto = tipoService.ConsultarTipoProducto(new Xpinn.Caja.Entities.TipoProducto() { cod_tipo_producto = long.Parse(row.tipo_producto.ToString()) }, Usuario);


                    if (row.saldo_total > 0 && Valor_tran_total > 0)
                    {
                        txtNumProducto.Text = row.numero_producto.ToString();
                        txtValTransac.Text = row.saldo_total.ToString();


                        if (Valor_tran_total < row.saldo_total)
                        {
                            if (Valor_tran_total > 0)
                            {
                                txtValTransac.Text = Convert.ToString(Valor_tran_total);
                            }
                            else
                            {
                                VerError("El valor total a pagar no alcanza para todos los productos");
                            }
                        }
                        Valor_tran_total = Valor_tran_total - Convert.ToDecimal(txtValTransac.Text);


                        if (producto.cod_tipo_producto == 1)
                        {
                            LlenarComboTipoPago(1);
                            ddlTipoPago.SelectedIndex = 0;
                        }
                        else if (producto.cod_tipo_producto == 2)
                        {
                            if (elem.tipo_linea == 2)
                            {
                                LlenarComboTipoPagoRotativo(Convert.ToInt64(2));
                                ddlTipoPago.SelectedIndex = 1;
                            }
                            else
                            {
                                LlenarComboTipoPago(2);
                                ddlTipoPago.SelectedIndex = 1;
                            }


                            if (producto.cod_tipo_producto == 2)
                            {

                                if (row.descripcion.Contains("Extras"))
                                {
                                    LlenarComboTipoPago(2);
                                    ddlTipoPago.SelectedIndex = 3;
                                    txtNumProducto.Text = row.numero_producto.ToString();
                                    //txtValTransac.Text = Convert.ToString(row.saldo_total);
                                    //Valor_tran_total = Valor_tran_total - Convert.ToDecimal(row.saldo_total);
                                    Session["CuotaExtra"] = 1;

                                }
                            }
                        }
                        else if (producto.cod_tipo_producto == 4)
                        {
                            LlenarComboTipoPago(4);
                            ddlTipoPago.SelectedIndex = 0;
                        }
                        else if (producto.cod_tipo_producto == 3)
                        {
                            LlenarComboTipoPago(3);
                            ddlTipoPago.SelectedIndex = 1;
                        }
                        else if (producto.cod_tipo_producto == 9)
                        {
                            LlenarComboTipoPago(9);
                            ddlTipoPago.SelectedIndex = 1;
                        }
                        else if (producto.cod_tipo_producto == 6)
                        {
                            LlenarComboTipoPago(6);
                            ddlTipoPago.SelectedIndex = 0;
                        }


                        btnGoTran_Click(null, null);
                        Session["CuotaExtra"] = null;

                    }
                }
            }
        }

        ddlTipoPago.Items.Clear();
        txtNumProducto.Text = "";
        txtValor.Text = "";
        Session["CuotaExtra"] = null;
    }

    protected void Check_Clicked(object sender, EventArgs e)
    {
        txtValTransac.Text = "";
        Decimal btnTotalTran = 0;
        foreach (GridViewRow row in gvtotal.Rows)
        {
            decimal valorPF = Convert.ToDecimal(row.Cells[9].Text);
            decimal valorCE = Convert.ToDecimal(row.Cells[10].Text);
            if (valorPF > 0 || valorCE > 0)
            {
                CheckBox cb = (CheckBox)row.FindControl("CheckBoxgv");
                if (cb.Checked == true)
                {
                    btnTotalTran = btnTotalTran + valorPF + valorCE;
                    txtValTransac.Text = Convert.ToString(btnTotalTran);
                }
            }
        }
    }


    protected void btnConsultaAvances_Click(object sender, EventArgs e)
    {
        listadoavances.Motrar(true, txtNumProducto.Text, "txtAvances", "txtValTransac");
    }

    #endregion

    //cerrar popup documentos
    protected void btneCancelar_Click(object sender, EventArgs e)
    {
        GeneradorDocumento.Hide();
    }

    protected void mpePopUp_OnClick(object sender, EventArgs e)
    {
        RpviewInfo.Visible = true;
        btnImprimiendose_Click();
    }

    protected void btnConsultarDoc_Click(object sender, EventArgs e)
    {
    }
    protected void btneConsultarop_Click(object sender, EventArgs e)
    {
    }


}

