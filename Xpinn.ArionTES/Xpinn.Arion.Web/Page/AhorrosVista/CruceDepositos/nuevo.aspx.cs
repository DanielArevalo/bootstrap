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
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using Xpinn.Util;


public partial class Nuevo : GlobalWeb
{
    Xpinn.Ahorros.Services.AhorroVistaServices Servicio = new Xpinn.Ahorros.Services.AhorroVistaServices();

    Xpinn.Caja.Services.DetallePagService DetallePagoService = new Xpinn.Caja.Services.DetallePagService();
    decimal ValTotalFPago = 0;
    decimal ValTotalTran = 0;
    int nActiva = 0;
    DateTime fecha;
    Usuario user = new Usuario();
    int tipoOpe = 125;
    int producto = 0;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones("220112", "A");
            if (txtValTransac.Text == "0")
                txtValTransac.Text = "";
            Site toolBar = (Site)this.Master;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Servicio.GetType().Name + "A", "Page_PreInit", ex);
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
                string ip = Request.ServerVariables["REMOTE_ADDR"];

                ObtenerDatos();
                user = (Usuario)Session["usuario"];

                AsignarEventoConfirmar();

                // Crea los DATATABLES para registrar las transacciones, los cheques
                CrearTablaTran();

                // Llenar los DROPDOWNLIST de tipos de monedas, tipos de identificaciòn, formas de pago y entidades bancarias
                LlenarComboTipoProducto(ddlTipoProducto);//se carga los tipos de transaccion
                LlenarComboMonedas(ddlMonedas);// se carga el primer combo de monedas en Transaccion
                LlenarComboTipoIden(ddlTipoIdentificacion);// se carga el segundo combo de moneda en Forma de Pago
                calcular();
                mvOperacion.ActiveViewIndex = 0;
                if (txtValTransac.Text == "0")
                    txtValTransac.Text = "";
            }
            else
            {
                txtValorAplicar_TextChanged(null, null);
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
            BOexcepcion.Throw(Servicio.GetType().Name + "A", "Page_Load", ex);
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

        // Cargando listado de tipos de productos de ahorros

        lsttipo = tipoopeservices.ListarTipoProducto(usuario);
        ddlTipoProducto.DataTextField = "nom_tipo_producto";
        ddlTipoProducto.DataValueField = "tipo_producto";
        ddlTipoProducto.DataSource = lsttipo;
        ddlTipoProducto.DataBind();

        // Seleccionando tipo de producto por defecto y cargandolo
        try
        {
            ddlTipoProducto.SelectedIndex = 0;
            Session["tipoproducto"] = Convert.ToInt64(ddlTipoProducto.SelectedValue);
            LlenarComboTipoPago(Convert.ToInt64(ddlTipoProducto.SelectedValue));
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void calcular()
    {
        decimal total = ConvertirStringToDecimal(txtValorTran.Text.Replace(".", ""));
        foreach (GridViewRow rfila in gvAhorros.Rows)
        {
            if (total > 0)
            {
                decimalesGridRow txtValorDevolucion = (decimalesGridRow)rfila.FindControl("txtValorDevolucion");
                if (txtValorDevolucion != null)
                {
                    // Validar el saldo
                    decimal saldoDev = 0;
                    if (rfila.Cells[4].Text.Trim() != "")
                    {
                        try
                        {
                            saldoDev = decimal.Parse(rfila.Cells[4].Text.Trim().Replace(gSeparadorMiles, ""));
                        }
                        catch
                        {
                            saldoDev = 0;
                        }
                    }
                    if (saldoDev > total)
                    {
                        txtValorDevolucion.Text = total.ToString();
                        total = 0;
                    }
                    else
                    {
                        if (saldoDev > 0)
                        {
                            txtValorDevolucion.Text = saldoDev.ToString();
                            total -= saldoDev;
                        }
                    }
                }
            }
        }
        List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
        lstAhorroVista = ObtenerValoresAhorros();
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

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        if (mvOperacion.ActiveViewIndex > 0)
            mvOperacion.ActiveViewIndex -= 1;
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
            if (!string.IsNullOrEmpty(pUsuario.nombre_oficina))
                txtOficina.Text = pUsuario.nombre_oficina;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Servicio.GetType().Name + "A", "ObtenerDatos", ex);
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
                            VerError("En el producto " + codRadicado + " el valor a pagar [" + valor + "] supera el valor total adeudado [" + deudaTotal + "]");
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
                VerError("El número de producto que ha digitado no coincide con el que aparece en la Consulta de Datos, por favor verificar.");
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
        calcular();
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
        VerError("");
        DateTime fechadesembolso = Convert.ToDateTime(Session["fecha"]);
        DateTime fechaactual = DateTime.Now;
        String format = gFormatoFecha;
        DateTime Fechatransaccion = DateTime.ParseExact(txtFechaTransaccion.Text, format, CultureInfo.InvariantCulture);

        // Realizar las validaciones
        if (Fechatransaccion > fechaactual)
        {
            String Error = "La fecha no puede ser superior a la fecha actual";
            this.Lblerror.Text = Error;
            return;
        }
        if (Fechatransaccion <= fechadesembolso)
        {
            String Error = "No se puede aplicar el pago por que es inferior a la fecha de desembolso";
            this.Lblerror.Text = Error;
            return;
        }
        List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
        lstAhorroVista = ObtenerValoresAhorros();
        if (lstAhorroVista == null)
        {
            String Error = "No hay productos para aplicar";
            this.Lblerror.Text = Error;
            return;
        }
        if (lstAhorroVista.Count() <= 0)
        {
            String Error = "No hay productos para aplicar";
            this.Lblerror.Text = Error;
            return;
        }
        if (Fechatransaccion > fechaactual)
        {
            String Error = "La fecha no puede ser superior a la fecha actual";
            this.Lblerror.Text = Error;
            return;
        }
        if (fechadesembolso != null)
        {
            if (Fechatransaccion <= fechadesembolso)
            {
                String Error = "No se puede aplicar el pago por que es inferior a la fecha de desembolso";
                this.Lblerror.Text = Error;
                return;
            }
        }
        ValTotalFPago = (txtValTotalAplicar.Text == "" ? 0 : decimal.Parse(txtValTotalAplicar.Text));
        ValTotalTran = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text); // Valor Total de Tabla de Transacciones
        if (ValTotalFPago == 0 && ValTotalTran == 0)
        {
            VerError("Debe especificar los valores a pagar");
            return;
        }

        // Adicionar la transaccion al listado
        this.Lblerror.Text = "";
        nActiva = Convert.ToInt16(mvOperacion.ActiveViewIndex.ToString());
        if (nActiva == 0)
        {
            if (txtNumProducto.Text != "" && txtValTransac.Text != "" && txtValTransac.Text != "0")
            {
                btnGoTran_Click(sender, e);
            };
            ValTotalFPago = (txtValTotalAplicar.Text == "" ? 0 : decimal.Parse(txtValTotalAplicar.Text));
            ValTotalTran = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text);
            if (ValTotalFPago == 0 && ValTotalTran == 0)
                return;
        }

        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(Convert.ToDateTime(txtFechaTransaccion.Text), tipoOpe) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación " + tipoOpe.ToString() + "=Aplicación de Devoluciones");
            return;
        }

        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (!panelProceso.Visible && panelGeneral.Visible)
        {
            rpta = ctlproceso.Inicializar(tipoOpe, Convert.ToDateTime(txtFechaTransaccion.Text), (Usuario)Session["Usuario"]);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                // Activar demás botones que se requieran
                panelGeneral.Visible = false;
                panelProceso.Visible = true;
            }
            else
            {
                String estado = "";
                DateTime fechacierrehistorico;
                DateTime fechacruce = Convert.ToDateTime(txtFechaTransaccion.Text);
                Xpinn.Ahorros.Entities.AhorroVista vAhorroVistaCierre = new Xpinn.Ahorros.Entities.AhorroVista();
                vAhorroVistaCierre = Servicio.ConsultarCierreAhorroVista((Usuario)Session["usuario"]);
                estado = vAhorroVistaCierre.estadocierre;
                fechacierrehistorico = Convert.ToDateTime(vAhorroVistaCierre.fecha_cierre.ToString());

                if (estado == "D" && fechacruce <= fechacierrehistorico)
                {
                    VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO H,'AHORROS'");

                }
                else
                {
                    // Crear la tarea de ejecución del proceso                
                    if (AplicarDatos())
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
            }
        }

    }


    protected bool AplicarDatos()
    {
        // Aplicar el valor
        try
        {
            this.Lblerror.Text = "";
            if (ValTotalFPago == ValTotalTran)// si son iguales en valor entonces deja guardar
            {
                // Determinar las devoluciones a apliar
                List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
                lstAhorroVista = ObtenerValoresAhorros();

                // Determina los datos de la transacción
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
                tranCaja.tipo_ope = tipoOpe;
                

                // Aplica las devoluciones
                string Error = "";
                VerError("");
                PersonaTransaccion pQuien = new PersonaTransaccion();
                pQuien.titular = true;
                tranCaja = Servicio.AplicarCruce_Ahs_Pro(tranCaja, pQuien, gvTransacciones, lstAhorroVista, txtObservacion.Text, (Usuario)Session["usuario"], ref Error);
                if (Error.Trim() != "")
                {
                    VerError(Error.Substring(0,90));
                    return false;
                }

                // Se genera el comprobante
                DateTime fecha = Convert.ToDateTime(txtFechaTransaccion.Text);
                Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = tranCaja.cod_ope;
                Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = tipoOpe;
                Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFechaTransaccion.Text);
                Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = long.Parse(Session["codpersona"].ToString());
                Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = pUsuario.cod_oficina;
                //Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                return true;
            }
            else
            {
                VerError("El Valor Total de Transacción debe ser igual al Valor Total de Transacción distribiuda en Formas de Pago");
                return false;
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            //BOexcepcion.Throw(ventanillaServicio.GetType().Name + "A", "AplicarDatos", ex);
            VerError(ex.Message);
            return false;
        }

    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Response.Redirect("nuevo.aspx", false);
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnGuardar"), "Desea Aplicar los Pagos?");
    }

    private string obtFiltro()
    {
        String filtro = String.Empty;
        filtro += "AND P.IDENTIFICACION='" + txtIdentificacion.Text + "'";
        return filtro;
    }

    /// <summary>
    /// Método para mostrar los datos de la persona
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        string pFiltro = obtFiltro();
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

                // Mostrar las cuentas de ahorro 
                List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
                Xpinn.Ahorros.Services.AhorroVistaServices TrasladoServicios = new Xpinn.Ahorros.Services.AhorroVistaServices();
                lstAhorroVista = TrasladoServicios.ListarAhorroAptPagos(pFiltro, producto, (Usuario)Session["usuario"]);
                gvAhorros.DataSource = lstAhorroVista;
                gvAhorros.DataBind();

                // aqui se coloca los datos de la persona, Nro Radicacion, Nombre, Valor CUota, saldo capital
                Actualizar(persona);
                pnlFormaPago.Visible = true;
                divDatos.Visible = true;
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
            lstConsulta = personaService.ListarDatosCreditoPersonaAhorros(pEntidad, (Usuario)Session["usuario"]);

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
            Session.Add(personaService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(personaService.GetType().Name + "L", "Actualizar", ex);
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

    private void ActualizardatosAhorros()
    {
        string pFiltro = obtFiltro();
        Lblerror.Text = "";
        try
        {
            // Mostrar las cuentas de ahorro 
            List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
            Xpinn.Ahorros.Services.AhorroVistaServices TrasladoServicios = new Xpinn.Ahorros.Services.AhorroVistaServices();
            lstAhorroVista = TrasladoServicios.ListarAhorroVistApPagos(pFiltro, (Usuario)Session["usuario"]);
            gvAhorros.DataSource = lstAhorroVista;
            gvAhorros.DataBind();


            if (lstAhorroVista.Count > 0)
            {
                //divDatos.Visible = true;
                gvAhorros.Visible = true;
                gvAhorros.DataBind();
            }
            else
            {
                // divDatos.Visible = false;
                gvAhorros.Visible = false;
            }

            Session.Add(Servicio.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Servicio.GetType().Name + "L", "Actualizar", ex);
        }
    }

    protected void ddlTipoTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
    {

        Xpinn.Caja.Services.TipoOperacionService tipoOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoProducto tipProd = new Xpinn.Caja.Entities.TipoProducto();
        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
        Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();

        tipProd.cod_tipo_producto = Convert.ToInt64(ddlTipoProducto.SelectedValue);
        producto = Convert.ToInt32(tipProd.cod_tipo_producto);
        btnConsultar_Click(null, null);

        tipProd = tipoOpeService.ConsultarTipoProductoAhorros(tipProd, (Usuario)Session["usuario"]);
        if (tipProd.cod_tipo_producto == 1)
        {
            persona.linea_credito = "1";
        }
        if (tipProd.cod_tipo_producto == 2)
        {
            persona.linea_credito = "2";
        }

        if (tipProd.cod_tipo_producto == 5)//cdats
        {
            persona.linea_credito = "5";
        }

        if (tipProd.cod_tipo_producto == 9)//PROGRAMADO
        {
            persona.linea_credito = "9";
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
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["tablaSesion"];//se pilla las transacciones

            DataTable tableFP = new DataTable();
            tableFP = (DataTable)Session["tablaSesion2"];// se pilla las formas de pago
            decimal acum = 0;
            long result = 0;

            if (tableFP != null)
            {
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

            txtValorTran.Text = Convert.ToString(decimal.Parse(txtValorTran.Text) - decimal.Parse(table.Rows[e.RowIndex][3].ToString()));
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
            BOexcepcion.Throw(Servicio.GetType().Name + "L", "gvTransacciones_RowDeleting", ex);
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
                    if (stippro == "9")
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
        Xpinn.Caja.Entities.Persona lineacreditos = new Xpinn.Caja.Entities.Persona();
        Xpinn.Caja.Services.PersonaService PersonaServicio = new Xpinn.Caja.Services.PersonaService();
        lineacreditos = PersonaServicio.ConsultarDatosCreditoPersona(Convert.ToString(txtNumProducto.Text), (Usuario)Session["usuario"]);
        fecha = Convert.ToDateTime(lineacreditos.fecha_desembolso);
        Session["fecha"] = fecha;
    }

    protected void gvLista_PageIndexChanging(object sender, System.EventArgs e)
    {

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

    protected void txtFechaTransaccion_TextChanged(object sender, EventArgs e)
    {
        txtFechaCont.Text = txtFechaTransaccion.Text;
    }

    protected void gvAhorros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimalesGridRow txtValorAplicar = (decimalesGridRow)e.Row.FindControl("txtValorAplicar");
            if (txtValorAplicar != null)
                txtValorAplicar.eventoCambiar += txtValorAplicar_TextChanged;
        }
    }

    protected void txtValorAplicar_TextChanged(object sender, EventArgs e)
    {
        List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
        lstAhorroVista = ObtenerValoresAhorros();
    }

    protected List<AhorroVista> ObtenerValoresAhorros()
    {
        List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
        decimal totalDev = 0;
        // Validar el saldo
        decimal saldoAplicar = 0;
        decimal saldoCuenta = 0;
        decimal saldoverif = 0;
        int ind = 0;
        VerError("");
        foreach (GridViewRow rFila in gvAhorros.Rows)
        {
            AhorroVista edevol = new AhorroVista();
            edevol.numero_cuenta = Convert.ToString(gvAhorros.DataKeys[ind].Value);
            // Obtener el valor a aplicar
            edevol.valor_a_aplicar = 0;
            decimalesGridRow txtValorAplicar = (decimalesGridRow)rFila.FindControl("txtValorAplicar");
            CheckBox CheckBoxgv = rFila.FindControl("CheckBoxgv") as CheckBox;

            if (txtValorAplicar != null && CheckBoxgv.Checked == true)
            {
                if (rFila.Cells[3].Text.Trim() != "" && rFila.Cells[3].Text.Trim() != "0")
                {
                    try
                    {
                        if (rFila.Cells[1].Text == "AHORRO PERMANENTE")
                        {
                            edevol.tipo_producto = 1;
                        }
                        else if (rFila.Cells[1].Text == "AHORRO A LA VISTA")
                        {
                            edevol.tipo_producto = 3;
                        }
                        else if (rFila.Cells[1].Text == "AHORRO PROGRAMADO")
                        {
                            edevol.tipo_producto = 9;
                        }
                        saldoverif = decimal.Parse(txtValorAplicar.Text.Trim().Replace(".", "").Replace(",", gSeparadorDecimal));
                        saldoCuenta = decimal.Parse(rFila.Cells[3].Text.Trim().Replace(".", "").Replace(",", gSeparadorDecimal));
                        if (saldoverif > saldoCuenta)
                        {
                            VerError("El valor no puede ser mayor al saldo de la cuenta");
                            //Response.Write("<script>window.alert('El valor no puede ser mayor al saldo de la cuenta');</script>");
                            txtValorAplicar.Text = "";
                        }
                        saldoAplicar = decimal.Parse(rFila.Cells[4].Text.Trim().Replace(".", "").Replace(",", gSeparadorDecimal));
                     
                    }
                    catch
                    {
                        saldoAplicar = 0;
                    }
                }
                // Actualizar el valor
                edevol.valor_a_aplicar = txtValorAplicar.Text.Trim() == "" ? 0 : decimal.Parse(txtValorAplicar.Text.Replace(".", "").Replace(",", gSeparadorDecimal));
                if (edevol.valor_a_aplicar > saldoAplicar && saldoAplicar > 0)
                {
                    edevol.valor_a_aplicar = saldoAplicar;
                    txtValorAplicar.Text = edevol.valor_a_aplicar.ToString();

                }
                // Calcular total de devoluciones
                totalDev += Convert.ToDecimal(edevol.valor_a_aplicar);
            }
            lstAhorroVista.Add(edevol);
            ind += 1;
        }
        txtValTotalAplicar.Text = Convert.ToInt64(totalDev).ToString().Replace(gSeparadorDecimal, ".");
        //txtValTransac.Text = txtValTotalAplicar.Text;        
        return lstAhorroVista;
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
            AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void txtNumCuenta_TextChanged(object sender, EventArgs e)
    {
        ActualizardatosAhorros();
    }

    protected void Check_Clicked(object sender, EventArgs e)
    {
        CheckBox chkHeader = sender as CheckBox;

        if (chkHeader.Checked == true)
        {
            if (gvAhorros.Rows.Count > 0)
            {
                foreach (GridViewRow row in gvAhorros.Rows)
                {
                    CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                    CheckBoxgv.Checked = true;

                }

            }
        }
        else
        {
            foreach (GridViewRow row in gvAhorros.Rows)
            {
                CheckBox CheckBoxgv = row.FindControl("CheckBoxgv") as CheckBox;
                CheckBoxgv.Checked = false;

            }
        }
    }


    protected void Validar_Cantidades(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void txtNumCuotas_TextChanged(object sender, EventArgs e)
    {
        CalcularValordelCredito();
    }

}
