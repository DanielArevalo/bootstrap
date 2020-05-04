using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Globalization;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;
using Xpinn.Util;


public partial class Nuevo : GlobalWeb
{
    Xpinn.Aportes.Services.AporteServices CruceApoServicio = new Xpinn.Aportes.Services.AporteServices();
    Xpinn.Tesoreria.Services.PagosVentanillaService ventanillaServicio = new Xpinn.Tesoreria.Services.PagosVentanillaService();
    Xpinn.Caja.Services.DetallePagService DetallePagoService = new Xpinn.Caja.Services.DetallePagService();
    decimal ValTotalFPago = 0;
    decimal ValTotalTran = 0;
    int nActiva = 0;
    DateTime fecha;
    Usuario user = new Usuario();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(CruceApoServicio.CodigoProgramaCruce, "A");
            if (txtValTransac.Text == "0")
                txtValTransac.Text = "";
            Site toolBar = (Site)this.Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CruceApoServicio.CodigoProgramaCruce + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlFormaPago.Visible = false;
                divDatos.Visible = false;
                mvOperacion.Visible = false;

                ObtenerDatos();
                user = (Usuario)Session["usuario"];

                AsignarEventoConfirmar();

                // Crea los DATATABLES para registrar las transacciones, los cheques
                CrearTablaTran();

                // Llenar los DROPDOWNLIST de tipos de monedas, tipos de identificaciòn, formas de pago y entidades bancarias
                LlenarComboTipoProducto(ddlTipoProducto);//se carga los tipos de transaccion
                LlenarComboMonedas(ddlMonedas);// se carga el primer combo de monedas en Transaccion
                LlenarComboTipoIden(ddlTipoIdentificacion);// se carga el segundo combo de moneda en Forma de Pago

                mvOperacion.ActiveViewIndex = 0;
                if (txtValTransac.Text == "0")
                    txtValTransac.Text = "";
            }
            else
            {
                txtValorAporte_TextChanged(null, null);
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
            BOexcepcion.Throw(CruceApoServicio.CodigoProgramaCruce + "A", "Page_Load", ex);
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
        ddlTipoProducto.DataTextField = "nom_tipo_producto";
        ddlTipoProducto.DataValueField = "tipo_producto";
        ddlTipoProducto.DataSource = lsttipo;
        ddlTipoProducto.DataBind();

        // Seleccionando tipo de producto por defecto y cargandolo
        try
        {
            ddlTipoProducto.SelectedIndex = 2;
            Session["tipoproducto"] = Convert.ToInt64(ddlTipoProducto.SelectedValue);
            LlenarComboTipoPago(Convert.ToInt64(ddlTipoProducto.SelectedValue));
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
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
    /// Cancelar y salir de la opción y regresar al menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        divDatos.Visible = false;
        gvConsultaDatos.DataSource = null;
        gvConsultaDatos.Visible = false;
        txtIdentificacion.Text = "";
        txtIdentificacion.Enabled = true;
        mvOperacion.Visible = false;
        gvAportes.Visible = false;
        txtNombreCliente.Text = "";
        ddlTipoIdentificacion.SelectedIndex = 0;
        ddlTipoProducto.SelectedIndex = 0;
        txtValorTran.Text = "";
        txtValTotalAporte.Text = "";
        txtNumProducto.Text = "";
        ddlTipoPago.SelectedIndex = -1;
        txtValTransac.Text = "";
        txtObservacion.Text = "";
        gvTransacciones.DataSource = null;
        gvTransacciones.DataBind();
        CrearTablaTran();
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
            if (!string.IsNullOrEmpty(pUsuario.nombre_oficina))
                txtOficina.Text = pUsuario.nombre_oficina;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CruceApoServicio.GetType().Name + "A", "ObtenerDatos", ex);
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
        gvTransacciones.DataSource = dt;
        gvTransacciones.DataBind();
        gvTransacciones.Visible = false;
        Session["tablaSesion"] = dt;
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

        foreach (GridViewRow pos in gvTransacciones.Rows)
        {
            num_cre = pos.Cells[8].Text;
            if (num_cre == txtNumProducto.Text.Trim())
                control = "0";
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

            // Adiciona la fila a la tabla
            dtAgre.Rows.Add(fila);
            gvTransacciones.DataSource = dtAgre;
            gvTransacciones.DataBind();
            Session["tablaSesion"] = dtAgre;

            // Inicializa los totales en efectivo y en cheque
            decimal valTotal = 0;
            decimal valEfectivo = 0;

            // Por defecto el valor total a pagar es efectivo
            valEfectivo = txtValTransac.Text == "" ? 0 : decimal.Parse(txtValTransac.Text.Replace(".", ""));
            valTotal = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text);
            valTotal = valTotal + valEfectivo;
            txtValorTran.Text = valTotal.ToString();

            // Determina el tipo de moneda
            int moneda = Convert.ToInt32(ddlMonedas.SelectedValue);
        }
    }



    /// <summary>
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

        // Cargar el valor de la transacciòn y el nùmero de producto
        decimal valor = 0;
        long codRadicado = 0;
        int result = 0;
        valor = txtValTransac.Text == "" ? 0 : decimal.Parse(txtValTransac.Text.Replace(".", ""));
        long numProd = txtNumProducto.Text == "" ? 0 : long.Parse(txtNumProducto.Text);

        // Consultar datos del tipo de operaciòn a utilizar
        tipOpe.cod_operacion = ddlTipoPago.SelectedValue;
        tipOpe = tipoOpeService.ConsultarTipoOperacion(tipOpe, (Usuario)Session["usuario"]);

        Session["tipoproducto"] = tipOpe.tipo_producto;

        if (txtNombreCliente.Text != "")
        {

            if (long.Parse(Session["tipoproducto"].ToString()) == 2)
            {
                decimal deudaTotal = 0;
                foreach (GridViewRow fila in gvConsultaDatos.Rows)
                {
                    codRadicado = Int64.Parse(fila.Cells[2].Text);
                    deudaTotal = decimal.Parse(fila.Cells[12].Text);
                    if (codRadicado == numProd)
                    {
                        result = 1;
                        if (valor > deudaTotal)
                        {
                            VerError("En el crédito " + codRadicado + " el valor a pagar [" + valor + "] supera el valor total adeudado [" + deudaTotal + "]");
                            return;
                        }
                    }
                }
            }
            else
                result = 1;

            if (result == 1)// si el radicado existe para ese cliente entonces se inserta el dato 
            {
                if (valor > 0)// hay que validar que acepte valores mayores o iguales a cero si es el caso de tipo tran 5
                {
                    LlenarTablaTran();
                    txtNumProducto.Text = "";
                    txtValTransac.Text = "";
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
        this.Lblerror.Text = "";
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
            return;
        }
        if (fechadesembolso != null)
        {
            if (Fechatransaccion < fechadesembolso)
            {
                String Error = "No se puede aplicar el pago por que es inferior a la fecha de desembolso";
                this.Lblerror.Text = Error;
                return;
            }
        }

        // Adicionar el producto a la tabla de transacciones si no se ha adicionado
        if (txtNumProducto.Text != "" && txtValTransac.Text != "" && txtValTransac.Text != "0")
        {
            btnGoTran_Click(sender, e);
        };
        txtValorAporte_TextChanged(null, null);

        // Actualizar el valor de las transacciones
        decimal total = 0;
        string nomtipomov = "";
        decimal valor = 0;
        long tipomov = 0;
        foreach (GridViewRow fila in gvTransacciones.Rows)
        {
            tipomov = long.Parse(fila.Cells[5].Text);
            nomtipomov = tipomov == 2 ? "INGRESO" : "EGRESO";
            valor = decimal.Parse(fila.Cells[9].Text);
            total += valor;
        };
        txtValorTran.Text = total.ToString();

        // Validar que los valores de las transacciones correspondan con lo que se aplica a aportes
        ValTotalTran = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text);             // Valor Total de Tabla de Transacciones
        ValTotalFPago = txtValTotalAporte.Text == "" ? 0 : decimal.Parse(txtValTotalAporte.Text);  // Valor Total Aporte
        if (ValTotalFPago == 0 && ValTotalTran == 0)
        {
            this.Lblerror.Text = "Debe especificar los valores a pagar";
            return;
        }
        if (ValTotalFPago != ValTotalTran)
        {
            this.Lblerror.Text = "El Valor Total de Transacción debe ser igual al Valor Total de Transacción distribuida en Formas de Pago";
            return;
        }

        // Validar que exista la parametrización contable por procesos
        Xpinn.Contabilidad.Services.ComprobanteService compServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        List<Xpinn.Contabilidad.Entities.ProcesoContable> lstProceso = new List<Xpinn.Contabilidad.Entities.ProcesoContable>();
        lstProceso = compServicio.ConsultaProceso(0, 112, Convert.ToDateTime(txtFechaTransaccion.Text), (Usuario)Session["Usuario"]);
        if (lstProceso == null)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 112=Cruce de Aportes con Productos");
            return;
        }
        if (lstProceso.Count <= 0)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 112=Cruce de Aportes con Productos");
            return;
        }

        String estado = "";
        DateTime fechacierrehistorico;
        String formato = gFormatoFecha;
        DateTime Fecharetiro = DateTime.ParseExact(txtFechaTransaccion.Text, formato, CultureInfo.InvariantCulture);

        Xpinn.Aportes.Entities.Aporte vaportes = new Xpinn.Aportes.Entities.Aporte();
        vaportes = CruceApoServicio.ConsultarCierreAportes((Usuario)Session["usuario"]);
        estado = vaportes.estadocierre;
        fechacierrehistorico = Convert.ToDateTime(vaportes.fecha_cierre.ToString());

        if (estado == "D" && Fecharetiro <= fechacierrehistorico)
        {
            VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO A,'APORTES'");
            return;
        }
        else
        {
            try
            {
                grabar();
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }

        }
    }


    private void grabar()
    {
        String Error = "0";
        Usuario usuap = (Usuario)Session["usuario"];
        Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
        Xpinn.FabricaCreditos.Data.Persona1Data PersonaDat = new Xpinn.FabricaCreditos.Data.Persona1Data();
        Xpinn.FabricaCreditos.Entities.Persona1 vPer = new Xpinn.FabricaCreditos.Entities.Persona1();

        // Verificar datos de la persona
        vPer.seleccionar = "Identificacion";
        vPer.noTraerHuella = 1;
        vPer.identificacion = txtIdentificacion.Text;
        vPer = PersonaDat.ConsultarPersona1Param(vPer, (Usuario)Session["usuario"]);

        try
        {
            // Aplicar los valores de las transacciones
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            Xpinn.Caja.Entities.TransaccionCaja tranCaja = new Xpinn.Caja.Entities.TransaccionCaja();
            tranCaja.cod_persona = long.Parse(Session["codpersona"].ToString());
            tranCaja.cod_caja = 0;
            tranCaja.cod_cajero = 0;
            tranCaja.cod_oficina = pUsuario.cod_oficina;
            tranCaja.fecha_movimiento = Convert.ToDateTime(txtFechaTransaccion.Text);
            tranCaja.fecha_aplica = Convert.ToDateTime(txtFechaCont.Text);
            tranCaja.fecha_cierre = Convert.ToDateTime(txtFechaCont.Text);
            tranCaja.observacion = txtObservacion.Text;
            if (txtValorTran.Text != "")
                tranCaja.valor_pago = decimal.Parse(txtValorTran.Text);
            else
                tranCaja.valor_pago = 0;
            tranCaja.tipo_ope = 112;
            VerError("");

            Aporte vAporte = new Aporte();
            vAporte.lstAporte = new List<Aporte>();
            vAporte.lstAporte = ObtenerListaAportes();

            Xpinn.Tesoreria.Business.PagosVentanillaBusiness pagosVentanillaBus = new Xpinn.Tesoreria.Business.PagosVentanillaBusiness();
            PersonaTransaccion pQuien = new PersonaTransaccion();
            pQuien.titular = true;
            tranCaja = pagosVentanillaBus.AplicarPagoCruceAporte(vAporte, tranCaja, pQuien, gvTransacciones, null, null, txtObservacion.Text, (Usuario)Session["usuario"], ref Error);
            if (Error.Trim() != "")
            {
                VerError(Error);
                return;
            }

            PanelDetallePago.Visible = false;
            mvOperacion.ActiveViewIndex = 1;

            if (gvAportes.Rows.Count > 0)
            {
                // generar comprobante 
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = tranCaja.cod_ope;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = Convert.ToInt64(112);
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = DateTime.Parse(txtFechaTransaccion.Text);
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = Convert.ToInt64(vPer.cod_persona);
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = Convert.ToInt64(usuap.cod_oficina); ;
                Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }

    }

    public List<Aporte> ObtenerListaAportes()
    {
        Usuario usuap = (Usuario)Session["usuario"];
        List<Aporte> lstResultado = new List<Aporte>();
        // Retirar los valores de las cuentas de aportes
        foreach (GridViewRow rFila in gvAportes.Rows)
        {
            Aporte aporte = new Aporte();
            aporte.numero_aporte = Convert.ToInt64(rFila.Cells[1].Text); //Numero Aporte
            aporte.cod_linea_aporte = Int32.Parse(rFila.Cells[2].Text);//Cod_Linea
            aporte.cod_persona = long.Parse(Session["codpersona"].ToString());
            aporte.fecha_retiro = DateTime.Parse(txtFechaTransaccion.Text);
            decimalesGridRow txtValorAporte = (decimalesGridRow)rFila.FindControl("txtValorAporte");
            if (txtValorAporte.Text != "")
                aporte.valor_retiro = Convert.ToInt64(txtValorAporte.Text.Replace(".", ""));
            else
                aporte.valor_retiro = 0;
            aporte.autorizacion = "0";
            aporte.observaciones = txtObservacion.Text;
            aporte.cod_usuario = Int32.Parse(usuap.codusuario.ToString());
            aporte.fecha_crea = DateTime.Now;
            aporte.cod_ope = 0;

            // Crear el Retiro
            Int64 saldo = Convert.ToInt64(rFila.Cells[5].Text.Replace(".", "")); //Saldo Total
            if (saldo >= aporte.valor_retiro)
            {
                lstResultado.Add(aporte);
            }
        }

        return lstResultado;
    }


    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Page/General/Inicio.aspx", false);
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
            Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();

            persona.identificacion = txtIdentificacion.Text;
            persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
            VerError("");
            persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);

            if (persona.mensajer_error == "")
            {
                Session["codpersona"] = persona.cod_persona;
                txtNombreCliente.Text = persona.nom_persona;

                // Mostrar los aportes
                Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
                string aportes;
                aportes = serviceEstadoCuenta.ConsultarParametrosAportes((Usuario)Session["Usuario"]);

                Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
                List<Xpinn.Aportes.Entities.Aporte> lstAportes = new List<Xpinn.Aportes.Entities.Aporte>();
                lstAportes = AporteServicio.ListarEstadoCuentaAportePermitePago(persona.cod_persona, DateTime.Now, (Usuario)Session["usuario"]);
                gvAportes.PageSize = pageSize;
                gvAportes.EmptyDataText = emptyQuery;
                gvAportes.DataSource = lstAportes;
                if (lstAportes.Count > 0)
                {
                    gvAportes.Visible = true;
                    gvAportes.DataBind();
                }
                else
                {
                    gvAportes.Visible = false;
                }

                // aqui se coloca los datos de la persona, Nro Radicacion, Nombre, Valor CUota, saldo capital
                Actualizar(persona);
                mvOperacion.Visible = true;
                txtIdentificacion.Enabled = false;
                ddlTipoIdentificacion.Enabled = false;
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
        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();

        try
        {
            pEntidad.tipo_linea = Convert.ToInt64(Session["tipoproducto"]);
            List<Xpinn.Caja.Entities.Persona> lstConsulta = new List<Xpinn.Caja.Entities.Persona>();
            lstConsulta = personaService.ListarDatosCreditoPersonaAportes(pEntidad, (Usuario)Session["usuario"]);

            gvConsultaDatos.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                divDatos.Visible = true;
                gvConsultaDatos.Visible = true;
                gvConsultaDatos.DataBind();
            }
            else
            {
                divDatos.Visible = false;
                gvConsultaDatos.Visible = false;
            }
            if (ddlTipoProducto.SelectedValue == "5") //CDATS
            {
                Xpinn.CDATS.Services.AperturaCDATService AperturaService = new Xpinn.CDATS.Services.AperturaCDATService();
                List<Xpinn.CDATS.Entities.Cdat> lstCdat = new List<Xpinn.CDATS.Entities.Cdat>();
                String filtro = " AND C.ESTADO =1 and  T.COD_PERSONA = " + pEntidad.cod_persona + " AND T.PRINCIPAL = 1 ";
                DateTime FechaApe;
                FechaApe = DateTime.MinValue;

                // txtReferencia.Visible = false;
                //  LblReferencia.Visible = false;
                lstCdat = AperturaService.ListarCdats(filtro, FechaApe, (Usuario)Session["usuario"]);

                if (lstCdat.Count > 0)
                {
                    gvConsultaDatos.Visible = false;
                    divDatos.Visible = true;

                    gvCdat.Visible = true;
                    gvCdat.DataSource = lstCdat;
                    gvCdat.DataBind();
                }
            }

            Session.Add(CruceApoServicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(CruceApoServicio.GetType().Name + "L", "Actualizar", ex);
        }
    }
    //CDATS
    protected void gvCdat_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        string id = gvCdat.DataKeys[e.NewEditIndex].Values[0].ToString();
        txtNumProducto.Text = id;
        String saldoTotal = gvCdat.Rows[e.NewEditIndex].Cells[10].Text;
        txtValTransac.Text = Convert.ToString(saldoTotal);
        txtValTransac.Enabled = true;
        e.NewEditIndex = -1;
    }

    protected void ddlTipoTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        VerError("No se puede aplicar a aportes");
        Xpinn.Caja.Services.TipoOperacionService tipoOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoProducto tipProd = new Xpinn.Caja.Entities.TipoProducto();
        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
        Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();

        tipProd.cod_tipo_producto = Convert.ToInt64(ddlTipoProducto.SelectedValue);
        tipProd = tipoOpeService.ConsultarTipoProducto(tipProd, (Usuario)Session["usuario"]);
        if (tipProd.cod_tipo_producto == 1)
        {
            persona.linea_credito = "1";
            VerError("No se puede aplicar a aportes");
            tipProd.cod_tipo_producto = 2;
            ddlTipoProducto.SelectedValue = "2";
        }
        if (tipProd.cod_tipo_producto == 2)
        {
            persona.linea_credito = "2";
        }

        if (tipProd.cod_tipo_producto == 9)//Programado 
        {
            persona.linea_credito = "9";
        }

        if (tipProd.cod_tipo_producto == 4)//Servicios 
        {
            persona.linea_credito = "4";
        }
        if (tipProd.cod_tipo_producto == 5)//Cdat 
        {
            persona.linea_credito = "5";
        }
        Session["tipoproducto"] = tipProd.cod_tipo_producto;

        LlenarComboTipoPago(tipProd.cod_tipo_producto);

        if (tipProd.cod_tipo_producto == 6)
            lblMsgNroProducto.Text = "El número de producto no es obligatorio colocarlo en este tipo de transacción, colocar cero en el campo de número de producto ";
        else
            lblMsgNroProducto.Text = "";

        persona.identificacion = txtIdentificacion.Text;
        persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
        VerError("");
        persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);
        Actualizar(persona);
    }


    protected void gvTransacciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        VerError("");
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["tablaSesion"];//se pilla las transacciones

            DataTable tableFP = new DataTable();
            tableFP = (DataTable)Session["tablaSesion2"];// se pilla las formas de pago
            decimal acum = 0;
            if (tableFP != null)
            {
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
            }

            decimal valorTran = decimal.Parse(txtValorTran.Text) - decimal.Parse(table.Rows[e.RowIndex][3].ToString());
            if (valorTran < 0)
                valorTran = 0;
            txtValorTran.Text = Convert.ToString(valorTran);

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
            BOexcepcion.Throw(CruceApoServicio.GetType().Name + "L", "gvTransacciones_RowDeleting", ex);
        }
    }

    /// <summary>
    /// Mostrar el detalle del pago para los productos de aportes y créditos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="evt"></param>
    protected void gvTransacciones_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "DetallePago")
        {
            int index = Convert.ToInt32(evt.CommandArgument);

            List<DetallePagos> ListaDetallePago = new List<DetallePagos>();

            GridViewRow gvTransaccionesRow = gvTransacciones.Rows[index];

            String stipo = gvTransacciones.Rows[index].Cells[6].Text;
            String stippro = gvTransacciones.Rows[index].Cells[7].Text;
            String snumpro = gvTransacciones.Rows[index].Cells[8].Text;
            String svalor = gvTransacciones.Rows[index].Cells[9].Text;
            String smoneda = gvTransacciones.Rows[index].Cells[10].Text;
            String stipopago = gvTransacciones.Rows[index].Cells[12].Text;
            if (stippro == "Cr&#233;ditos" || stippro == "Aportes")
            {
                try
                {
                    Configuracion global = new Configuracion();
                    string sseparador = global.ObtenerSeparadorMilesConfig();
                    Int64 valor_pago = int.Parse(txtValorTran.Text.Replace(sseparador, ""));
                    DateTime fecha_pago = Convert.ToDateTime(txtFechaTransaccion.Text);
                    if (stippro == "Cr&#233;ditos")
                    {
                        stippro = "2";
                    }
                    if (stippro == "Aportes")
                    {
                        stippro = "1";
                    }
                    ListaDetallePago = DetallePagoService.DistribuirPago(Convert.ToInt64(stippro), Convert.ToInt32(snumpro), fecha_pago, Convert.ToInt32(svalor), stipopago, (Usuario)Session["Usuario"]);
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
        if (ddlTipoProducto.SelectedValue == "4")
        {
            CalcularValordelservicio();
        }
        else
        {
            txtValTransac.Enabled = true;
        }
    }

    private void CalcularValordelCredito()
    {
        Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
        user = (Usuario)Session["usuario"];

        // Determinar nùmero de crèdito
        Int64 numero_radicacion = 0;
        if (txtNumProducto.Text != "" && ddlTipoProducto.SelectedValue == "2")
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


    private void CalcularValordelservicio()
    {
        Xpinn.Servicios.Services.AtributosTasasServices ServicioService = new Xpinn.Servicios.Services.AtributosTasasServices();
        txtValTransac.Text = "";
        if (txtNumProducto.Text != "" && ddlTipoProducto.SelectedValue == "4")
        {
            try
            {
                decimal valor_apagar = ServicioService.ValorPagoServicio(ddlTipoPago.SelectedValue, txtNumProducto.Text, txtFechaTransaccion.Text, user);
                txtValTransac.Text = valor_apagar.ToString();
            }
            catch
            {
                txtValTransac.Text = "";
                txtValTransac.Enabled = true;
            }
        }

    }

    /// <summary>
    /// Validar que cuando cambia el nùmero de producto se actualice el valor a pagar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtNumProducto_TextChanged(object sender, EventArgs e)
    {
        if (ddlTipoProducto.SelectedValue == "2")
        {
            // Segùn el tipo de pago ajustar los datos
            CalcularValordelCredito();
        }
        if (ddlTipoProducto.SelectedValue == "4")
        {
            // Segùn el tipo de pago ajustar los datos
            CalcularValordelservicio();
        }

    }


    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        txtNumProducto.Text = evt.CommandName;
        Xpinn.Caja.Entities.Persona lineacreditos = new Xpinn.Caja.Entities.Persona();
        Xpinn.Caja.Services.PersonaService PersonaServicio = new Xpinn.Caja.Services.PersonaService();
        lineacreditos = PersonaServicio.ConsultarDatosCreditoPersona(Convert.ToString(txtNumProducto.Text), (Usuario)Session["usuario"]);
        fecha = Convert.ToDateTime(lineacreditos.fecha_desembolso);
        Session["fecha"] = fecha;
        //
        if (ddlTipoProducto.SelectedValue == "2")
        {
            // Segùn el tipo de pago ajustar los datos
            CalcularValordelCredito();
        }
        if (ddlTipoProducto.SelectedValue == "4")
        {
            // Segùn el tipo de pago ajustar los datos
            CalcularValordelservicio();
        }
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


    protected void txtFechaTransaccion_TextChanged(object sender, EventArgs e)
    {
        txtFechaCont.Text = txtFechaTransaccion.Text;
    }


    protected void gvAportes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimalesGridRow txtValorAporte = (decimalesGridRow)e.Row.FindControl("txtValorAporte");
            if (txtValorAporte != null)
                txtValorAporte.eventoCambiar += txtValorAporte_TextChanged;
        }
    }

    protected void txtValorAporte_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        decimal totalApo = 0;
        foreach (GridViewRow rFila in gvAportes.Rows)
        {
            decimalesGridRow txtValorAporte = (decimalesGridRow)rFila.FindControl("txtValorAporte");
            if (txtValorAporte != null)
            {
                // Validar el saldo
                decimal saldoApo = 0;
                if (rFila.Cells[5].Text.Trim() != "")
                {
                    try
                    {
                        saldoApo = decimal.Parse(rFila.Cells[5].Text.Trim());
                    }
                    catch
                    {
                        saldoApo = 0;
                    }
                }
                decimal valorApo = txtValorAporte.Text.Trim() == "" ? 0 : decimal.Parse(txtValorAporte.Text.Replace(".", ""));
                if (valorApo > saldoApo)
                {
                    valorApo = saldoApo;
                    txtValorAporte.Text = valorApo.ToString();
                }
                totalApo += valorApo;
                TextBox txtValorApo = (TextBox)rFila.FindControl("txtValorApo");
                if (txtValorApo != null)
                    txtValorApo.Text = txtValorAporte.Text.Trim();
            }
        }
        txtValTotalAporte.Text = totalApo.ToString("n0");
    }

    protected void txtNumCuotas_TextChanged(object sender, EventArgs e)
    {
        CalcularValordelCredito();
    }

}
