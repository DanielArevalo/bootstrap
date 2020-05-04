using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Configuration;
using System.Collections;
using Xpinn.Contabilidad.Entities;
using Microsoft.Reporting.WebForms;
using System.Globalization;
using System.Drawing.Printing;
using System.Web.Script.Services;
using Xpinn.Comun.Services;
using Xpinn.Comun.Entities;
using Xpinn.TarjetaDebito.Services;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Interfaces.Entities;
using Newtonsoft.Json;
using Xpinn.Tesoreria.Entities;

public partial class Nuevo : GlobalWeb
{
    readonly string _tipoOperacionRetiroEnpacto = "1";
    readonly string _tipoOperacionDepositoEnpacto = "3";
    private string _convenio = "";

    private string _convenioEAN = "";
    private string _refEAN = "";
    private string _ValorEAN = "";
    private string _FechaEAN = "";

    private DateTime fechacaja;
    private DateTime fechacajacierre;
    private DateTime fechadia;
    DateTime fecha;
    Int64 tipoproducto = 0;
    Int64 contador = 0;
    private Xpinn.Caja.Services.TransaccionCajaService tranCajaServicio = new Xpinn.Caja.Services.TransaccionCajaService();
    private Xpinn.Caja.Entities.TransaccionCaja tranCaja = new Xpinn.Caja.Entities.TransaccionCaja();

    private Xpinn.Caja.Services.ReintegroService reintegroService = new Xpinn.Caja.Services.ReintegroService();
    private Xpinn.Caja.Entities.Reintegro reintegro = new Xpinn.Caja.Entities.Reintegro();

    private Xpinn.Caja.Services.HorarioOficinaService HorarioService = new Xpinn.Caja.Services.HorarioOficinaService();
    private Xpinn.Caja.Entities.HorarioOficina horario = new Xpinn.Caja.Entities.HorarioOficina();

    private Xpinn.Caja.Services.CajeroService cajeroService = new Xpinn.Caja.Services.CajeroService();
    private Xpinn.Caja.Entities.Cajero cajero = new Xpinn.Caja.Entities.Cajero();
    private Xpinn.Caja.Services.DetallePagService DetallePagoService = new Xpinn.Caja.Services.DetallePagService();

    ConveniosService BOConvenioService = new ConveniosService();
    PoblarListas poblar = new PoblarListas();

    private Usuario user = new Usuario();
    private Int16 nActiva = 0;
    string DirecionAsc;
    string municipio;
    string pNomUsuario;
    string pNomUsuarios;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(tranCajaServicio.CodigoPrograma3, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _convenio = "";
            //_convenio = ConvenioTarjeta();
            txtNumProducto.Enabled = false;
            this.Form.Attributes.Add("autocomplete", "off");
            if (!Page.IsPostBack)
            {
                ViewState["Productos"] = null;
                this.Lblerror.Text = "";
                fechadia = DateTime.Now;

                txtFechaTransaccion.Text = reintegro.fechareintegro.ToString(gFormatoFecha);
                BntVer.Visible = false;

                txttransacciondia.Text = fechadia.ToString(gFormatoFecha);
                fechadia = Convert.ToDateTime(txttransacciondia.Text);

                bancochquevacio.Text = "";
                numchequevacio.Text = "";
                valorchequevacio.Text = "";
                string ip = Request.ServerVariables["REMOTE_ADDR"];

                ObtenerDatos();
                user = (Usuario)Session["usuario"];
                cajero = cajeroService.ConsultarIfUserIsntCajeroPrincipal(user.codusuario, (Usuario)Session["usuario"]);
                Session["estadoCaj"] = cajero.estado;//estado Cajero
                Session["estadoOfi"] = cajero.estado_ofi;// estado Oficina
                Session["estadoCaja"] = cajero.estado_caja;// estado Caja
                txtDia.Text = HorarioService.getDiaHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]);

                horario = HorarioService.VerificarHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]);
                Session["conteoOfiHorario"] = horario.conteo;

                horario = HorarioService.getHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]);

                Session["Resp1"] = 0;
                Session["Resp2"] = 0;

                //si la hora actual es mayor que de la hora inicial
                if (TimeSpan.Compare(horario.fecha_hoy.TimeOfDay, horario.hora_inicial.TimeOfDay) > 0)
                    Session["Resp1"] = 1;
                //si la hora actual es menor que la hora final
                if (TimeSpan.Compare(horario.fecha_hoy.TimeOfDay, horario.hora_final.TimeOfDay) < 0)
                    Session["Resp2"] = 1;

                if (long.Parse(Session["estadoOfi"].ToString()) == 2)
                    VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
                else
                {
                    if (long.Parse(Session["estadoCaja"].ToString()) == 0)
                        VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
                    else
                    {
                        if (long.Parse(Session["conteoOfiHorario"].ToString()) == 0)
                        {
                            VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
                        }
                        else
                        {
                            if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                            {
                                if (long.Parse(Session["estadoCaj"].ToString()) == 0)
                                    VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                            }
                            else
                                VerError("La Oficina se encuentra por fuera del horario configurado. Dia: " + HorarioService.getDiaHorarioOficina(user.cod_oficina, (Usuario)Session["usuario"]));
                        }

                    }
                }

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
                LlenarComboConvenios();

                // Crea el DATATABLE para poder registrar los valores por cada tipo de moneda y forma de pago
                CrearTablaFormaPago();

                //mvOperacion.ActiveViewIndex = 0;
                mvOperacion.Visible = false;
                panelTransaccion.Visible = true;
                panelGridTran.Visible = false;
                rblOpcRegistro_SelectedIndexChanged(rblOpcRegistro, null);
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "A", "Page_Load", ex);
        }

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
                                    ViewState["CuotaExtra"] = 1;
                                    btnGoTran_Click(null, null);
                                    ViewState["CuotaExtra"] = null;

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
                lstConsulta = serviciosMoras.ConsultarDetalleMoraPersona(txtCodCliente.Text, "", Convert.ToDateTime(txtFechaTransaccion.Text), "1,2,4,3,6", (Usuario)Session["usuario"]);
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
                                    ViewState["CuotaExtra"] = 1;

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
                        ViewState["CuotaExtra"] = null;

                    }

                }
            }

        }

        ddlTipoPago.Items.Clear();
        txtNumProducto.Text = "";
        txtValor.Text = "";
        ViewState["CuotaExtra"] = null;
    }


    protected void gvConsultaDatos_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// LLenar el combo de los tipos de transacciones segùn el tipo de producto
    /// </summary>
    /// <param name="ddlTipoTransaccion"></param>
    protected void LlenarComboTipoProducto(DropDownList ddlTipoProducto)
    {
        Xpinn.Caja.Services.TipoOperacionService tipoopeservices = new Xpinn.Caja.Services.TipoOperacionService();
        String filtro = obtFiltro();

        // Inicializar las variables        
        Usuario usuario = new Usuario();
        usuario = (Usuario)Session["usuario"];
        List<Xpinn.Caja.Entities.TipoOperacion> lsttipo = new List<Xpinn.Caja.Entities.TipoOperacion>();

        //Determinar còdigo de la caja
        Int64 cod_caja = long.Parse(Session["Caja"].ToString());

        // Cargando listado de tipos de productos
        lsttipo = tipoopeservices.ListarTipoProductoCaja(usuario, cod_caja);


        Xpinn.Caja.Entities.TipoOperacion todos = new Xpinn.Caja.Entities.TipoOperacion();
        todos.nom_tipo_producto = "Pago Total Productos";
        todos.tipo_producto = 100;
        lsttipo.Add(todos);


        ddlTipoProducto.DataTextField = "nom_tipo_producto";
        ddlTipoProducto.DataValueField = "tipo_producto";
        ddlTipoProducto.DataSource = lsttipo;
        ddlTipoProducto.DataBind();

        ViewState["Productos"] = lsttipo;
        // Seleccionando tipo de producto por defecto y cargandolo
        ddlTipoProducto.SelectedIndex = 0;
        if (ddlTipoProducto.SelectedItem != null)
        {
            Session["tipoproducto"] = Convert.ToInt64(ddlTipoProducto.SelectedValue);
            LlenarComboTipoPago(Convert.ToInt64(ddlTipoProducto.SelectedValue));
        }
    }

    protected void LlenarComboMonedas(DropDownList ddlMonedas)
    {

        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        Usuario usuario = (Usuario)Session["usuario"];
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, usuario);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();
    }

    protected void LlenarComboBancos(DropDownList ddlBancos)
    {
        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        Usuario usuario = (Usuario)Session["usuario"];
        ddlBancos.DataSource = bancoService.ListarBancos(banco, usuario);
        ddlBancos.DataTextField = "nombrebanco";
        ddlBancos.DataValueField = "cod_banco";
        ddlBancos.DataBind();
        ddlBancos.Items.Insert(0, new ListItem("Seleccione un Banco", "0"));
    }

    protected void LlenarComboConvenios()
    {
        poblar.PoblarListaDesplegable("CONVENIO_RECAUDO", "distinct(COD_CONVENIO),NOMBRE", "", "2", ddlTipoConvenio, (Usuario)Session["usuario"]);
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        txtNumProducto.Text = "";
        ctlBusquedaPersonas.Motrar(true, "txtCodCliente", "txtIdentificacion", "ddlTipoIdentificacion", "txtNombreCliente");
    }
    protected void LlenarComboTipoIden(DropDownList ddlTipoIdentificacion)
    {

        Xpinn.Caja.Services.TipoIdenService IdenService = new Xpinn.Caja.Services.TipoIdenService();
        Xpinn.Caja.Entities.TipoIden identi = new Xpinn.Caja.Entities.TipoIden();
        Usuario usuario = (Usuario)Session["usuario"];
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
        Usuario usuario = (Usuario)Session["usuario"];
        ddlFormaPago.DataSource = pagoService.ListarTipoPago(paguei, usuario);
        ddlFormaPago.DataTextField = "descripcion";
        ddlFormaPago.DataValueField = "cod_tipo_pago";
        ddlFormaPago.DataBind();
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
            tipOpe.cod_caja = long.Parse(Session["Caja"].ToString());

            tipOpe.tipo_producto = ptipo_producto;
            tipOpe.tipo_movimiento = Convert.ToInt64(ddlTipoMovimiento.SelectedValue);
            ddlTipoPago.DataSource = tipOpeServicio.ListarTipoOpeTransac(tipOpe, (Usuario)Session["usuario"]);
            ddlTipoPago.DataTextField = "nombre";
            ddlTipoPago.DataValueField = "cod_operacion";
            ddlTipoPago.DataBind();
            if (ddlTipoProducto.SelectedItem != null)
            {
                if (ddlTipoProducto.SelectedValue == "1")
                    ddlTipoPago.SelectedIndex = 0;
                else
                    ddlTipoPago.Enabled = true;
                    ddlTipoPago.SelectedIndex = 1;
            }
        }
        catch
        {
        }
    }


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



    /// <summary>
    /// Cancelar y salir de la opción y regresar al menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("../../../General/Global/inicio.aspx");
    }


    


    protected void btnCodBarras_Click(object sender, ImageClickEventArgs e)
    {
       
        BtnValidar.Focus();
    }



    /// <summary>
    /// Muestra los datos iniciales en pantalla
    /// </summary>
    protected void ObtenerDatos()
    {

        try
        {
            reintegro = reintegroService.ConsultarCajero((Usuario)Session["usuario"]);

            municipio = reintegro.nomciudad;

            if (!string.IsNullOrEmpty(reintegro.fechareintegro.ToString()))
            {
                txtFechaTransaccion.Text = (new DateTime(reintegro.fechareintegro.Year, reintegro.fechareintegro.Month, reintegro.fechareintegro.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)).ToString();
                txttransacciondia.Text = reintegro.fechareintegro.ToString(gFormatoFecha);
            }

            if (!string.IsNullOrEmpty(reintegro.nomoficina))
                txtOficina.Text = reintegro.nomoficina.ToString();
            if (!string.IsNullOrEmpty(reintegro.nomcaja))
                txtCaja.Text = reintegro.nomcaja.ToString();
            if (!string.IsNullOrEmpty(reintegro.nomcajero))
                txtCajero.Text = reintegro.nomcajero.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.cod_oficina.ToString()))
                Session["Oficina"] = reintegro.cod_oficina.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.cod_caja.ToString()))
                Session["Caja"] = reintegro.cod_caja.ToString().Trim();
            if (!string.IsNullOrEmpty(reintegro.cod_cajero.ToString()))
                Session["Cajero"] = reintegro.cod_cajero.ToString().Trim();
            ObtenerDatosUltCierre();

            if (fechacajacierre < fechadia)
            {
                VerError("no se ha generado APERTURA del día, no puede timbrar operaciones con esta fecha");
                ddlTipoIdentificacion.Enabled = false;
                txtIdentificacion.Enabled = false;
                btnConsultar.Enabled = false;
                btnGoTran.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "A", "ObtenerDatos", ex);
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
    protected void ObtenerDatosUltCierre()
    {

        try
        {
            reintegro = reintegroService.ConsultarFecUltCierre((Usuario)Session["usuario"]);
            if (!string.IsNullOrEmpty(reintegro.fechaarqueo.ToString()))
            {
                fechacajacierre = reintegro.fechaarqueo;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "A", "ObtenerDatos", ex);
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

        Int64 TipoMov = 1;
        // Determina el dato del tipo de producto
        if (ddlTipoProducto.SelectedValue != "10")
        {
            tipOpe.cod_operacion = ddlTipoPago.SelectedValue;
            tipOpe = tipOpeService.ConsultarTipOpeTranCaja(tipOpe, (Usuario)Session["usuario"]);
            TipoMov = tipOpe.tipo_movimiento;
        }

        //se trata de localizar el registro que se hace necesario actualizar
        foreach (DataRow fila in dtAgre2.Rows)
        {
            if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == formapago)
            {
                fila[2] = decimal.Parse(fila[2].ToString()) + valEfectivo;
                fila[5] = TipoMov;
            }

            acum = acum + decimal.Parse(fila[2].ToString());
            fila[5] = TipoMov;
        }

        gvFormaPago.DataSource = dtAgre2;
        gvFormaPago.DataBind();
        Session["tablaSesion2"] = dtAgre2;

        decimal valFormaPagoTotal = 0;

        valFormaPagoTotal = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);
        txtValTotalFormaPago.Text = acum.ToString();

    }

    /// <summary>
    /// En este métodos se cargan el valor del cheque registrado a la grilla de cheques validando
    /// que el valor no excede el valor total en cheques.
    /// </summary>
    /// <returns></returns>
    protected long LlenarFormaPago3()
    {
        DataTable dtAgre4 = new DataTable();
        dtAgre4 = (DataTable)Session["tablaSesion2"];
        long moneda = long.Parse(ddlMonCheque.SelectedValue);
        decimal ValorCheque = decimal.Parse(txtValCheque.Text.Replace(".", ""));
        decimal acum = 0;
        long result = 0;

        foreach (DataRow fila in dtAgre4.Rows)
        {
            if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == 1)
            {
                //se valida que el valor de la forma de pago sea mayor que el valor de cheque
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

            valFormaPagoTotal = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);
            txtValTotalFormaPago.Text = acum.ToString();
        }

        return result;
    }
    protected long LlenarFormaPago2()// este es el metodo que suma 
    {
        DataTable dtAgre2 = new DataTable();
        dtAgre2 = (DataTable)Session["tablaSesion2"];

        long result = 0;
        long moneda = long.Parse(ddlMoneda.SelectedValue);
        long formapago = long.Parse(ddlFormaPago.SelectedValue);
        decimal valorFormaPago = decimal.Parse(txtValor.Text.Replace(".", ""));
        decimal acum = 0;

        if (formapago != 1 && formapago != 2)// se valida que no se incerten cambios en Forma de Pagos en Efectivo y Cheque
        {
            //se trata de localizar el registro que se hace necesario actualizar
            foreach (DataRow fila in dtAgre2.Rows)
            {
                if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == formapago)
                {
                    fila[2] = decimal.Parse(fila[2].ToString()) + valorFormaPago;
                }

                acum = acum + decimal.Parse(fila[2].ToString());
            }

            gvFormaPago.DataSource = dtAgre2;
            gvFormaPago.DataBind();
            Session["tablaSesion2"] = dtAgre2;

            decimal valFormaPagoTotal = 0;

            valFormaPagoTotal = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);
            txtValTotalFormaPago.Text = acum.ToString();
        }
        else
        {
            result = 1;
        }

        return result;
    }
    protected long LlenarFormaPago5()
    {
        DataTable dtAgre5 = new DataTable();
        dtAgre5 = (DataTable)Session["tablaSesion2"];
        long moneda = long.Parse(ddlMoneda.SelectedValue);
        decimal ValorForma = decimal.Parse(txtValor.Text.Replace(".", ""));
        long formapago = long.Parse(ddlFormaPago.SelectedValue);
        decimal acum = 0;
        long result = 0;

        if (formapago != 1 && formapago != 2)
        {
            foreach (DataRow fila in dtAgre5.Rows)
            {
                if (long.Parse(fila[0].ToString()) == moneda && long.Parse(fila[1].ToString()) == 1)
                {
                    //se valida que el valor de la forma de pago(grilla) sea mayor que el valor de forma de pago( textbox)
                    if (decimal.Parse(fila[2].ToString()) >= ValorForma)
                    {
                        fila[2] = decimal.Parse(fila[2].ToString()) - ValorForma;
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
                gvFormaPago.DataSource = dtAgre5;
                gvFormaPago.DataBind();
                Session["tablaSesion2"] = dtAgre5;

                decimal valFormaPagoTotal = 0;

                valFormaPagoTotal = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);
                txtValTotalFormaPago.Text = acum.ToString();
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
        if (ddlTipoProducto.SelectedValue != "10")
        {
            tipOpe.cod_operacion = ddlTipoPago.SelectedValue;
            tipOpe = tipOpeService.ConsultarTipOpeTranCaja(tipOpe, (Usuario)Session["usuario"]);
        }
        string num_cre = "1", control = "";

        if (rblOpcRegistro.SelectedValue != "2")
        {
            foreach (GridViewRow pos in gvTransacciones.Rows)
            {
                num_cre = pos.Cells[8].Text;
                if (!chkMora.Checked)
                {
                    if (num_cre == txtNumProducto.Text.Trim() && Convert.ToInt32(ViewState["CuotaExtra"]) == 0)
                    {
                        control = "0";
                    }
                }
            }
        }
        if (control == "0")
        {
            VerError("Ya se cargo una Transacción a ese Número de Producto");
            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
            return;
        }
        else
        {

            // LLena los datos de la fila 
            fila[0] = ddlTipoProducto.SelectedValue;
            fila[1] = ddlTipoProducto.SelectedValue == "10" ? 10 : tipOpe.tipo_producto;
            if (txtNumProducto.Text.Trim() == "")           // Colocar el nùmero del producto
                fila[2] = "0";
            else
                fila[2] = txtNumProducto.Text;
            fila[3] = txtValTransac.Text.Replace(".", "");
            fila[4] = ddlMonedas.SelectedValue;             // Colocar el tipo de moneda de la transacciòn
            fila[5] = ddlTipoPago.SelectedItem != null && ddlTipoProducto.SelectedValue != "10" ? ddlTipoPago.SelectedItem.Text : "";
            fila[6] = ddlTipoProducto.SelectedItem.Text;    // Colocar el tipo de producto de la transacciòn
            fila[7] = ddlMonedas.SelectedItem.Text;
            fila[8] = tipOpe.tipo_movimiento;
            fila[9] = ddlTipoProducto.SelectedValue == "10" ? "Giros" : tipOpe.nom_tipo_operacion;
            fila[10] = ddlTipoPago.SelectedItem != null && ddlTipoProducto.SelectedValue != "10" ? ddlTipoPago.SelectedValue : null;
            if (txtReferencia.Text.Trim() == "" || txtReferencia.Visible == false)           // Colocar el nùmero de  Referencia
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
            valEfectivo = txtValTransac.Text == "" ? 0 : decimal.Parse(txtValTransac.Text.Replace(".", ""));
            valTotal = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text);
            valTotal = valTotal + valEfectivo;
            txtValorTran.Text = valTotal.ToString();

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
        string cuenta = "";
        int result = 0;
        bool ExisteError = false;
        valor = txtValTransac.Text == "" ? 0 : decimal.Parse(txtValTransac.Text.Replace(".", ""));
        long numProd = txtNumProducto.Text == "" ? 0 : long.Parse(txtNumProducto.Text);
        if (ddlTipoProducto.SelectedValue != "7" && txtNumProducto.Text == "")
        {
            VerError("Seleccione el Número Producto");
            Lblerror.Visible = true;
            Lblerror.Text = "Seleccione el Número Producto";
            ddlTipoPago.Focus();
            ExisteError = true;
        }
        if (ExisteError == false)
        {
            Lblerror.Visible = false;
            //  bool ExisteError = false;
            // Consultar datos del tipo de operaciòn a utilizar
            if (rblOpcRegistro.SelectedValue == "2")
            {
                if (ddlTipoConvenio.SelectedIndex == 0)
                {
                    VerError("Seleccione el convenio al cual realizará la transacción.");
                    ddlTipoConvenio.Focus();
                    ExisteError = true;
                }
                else if (txtValTransac.Text == "0")
                {
                    VerError("Ingrese el valor de la transacción.");
                    txtValTransac.Focus();
                    ExisteError = true;
                }
                else if (txtReferencia.Text.Trim() == "")
                {
                    VerError("Ingrese el Nro de referencia.");
                    txtReferencia.Focus();
                    ExisteError = true;
                }
                if (ExisteError == true)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                    return;
                }
            }

            if (ddlTipoProducto.SelectedValue == "4")
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


            if (ddlTipoProducto.SelectedValue != "10")
            {
                if (ddlTipoPago.SelectedItem == null)
                {
                    VerError("No existen Tipos de Pago para el Producto Seleccionado");
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                    return;
                }
                tipOpe.cod_operacion = ddlTipoPago.SelectedValue;

                //tipom = tipoOpeService.ConsultarTipoOperacion(tipOpe, (Usuario)Session["usuario"]);
                tipOpe = tipoOpeService.ConsultarTipoOperacion(tipOpe, (Usuario)Session["usuario"]);
                Session["tipoproducto"] = tipOpe.tipo_producto;
            }
            else
                Session["tipoproducto"] = 10;

            ExisteError = false;
            if (long.Parse(Session["estadoOfi"].ToString()) == 1)
            {
                if (long.Parse(Session["estadoCaja"].ToString()) == 1)
                {

                    if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                    {
                        if (txtNombreCliente.Text != "")
                        {

                            if (long.Parse(Session["tipoproducto"].ToString()) == 2)
                            {
                                decimal deudaTotal = 0;
                                foreach (GridViewRow fila in gvConsultaDatos.Rows)
                                {
                                    codRadicado = Convert.ToInt64(fila.Cells[2].Text);
                                    deudaTotal = decimal.Parse(fila.Cells[13].Text);
                                    if (codRadicado == numProd)
                                    {
                                        result = 1;
                                        if (valor > deudaTotal)
                                        {
                                            VerError("En el crédito " + codRadicado + " el valor a pagar [" + valor + "] supera el valor total adeudado [" + deudaTotal + "]");
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                                            return;
                                        }

                                    }
                                }
                            }

                            if (long.Parse(Session["tipoproducto"].ToString()) == 3)
                            {
                                decimal saldoTotal = 0;
                                Int32 tipomov = Convert.ToInt32(ddlTipoMovimiento.SelectedValue);
                                foreach (GridViewRow fila in gvAhorroVista.Rows)
                                {
                                    cuenta = (fila.Cells[1].Text);
                                    saldoTotal = decimal.Parse(fila.Cells[8].Text);

                                    if (cuenta == numProd.ToString())
                                    {
                                        result = 1;
                                        if (valor > saldoTotal && tipomov == 1)
                                        {
                                            VerError("En la cuenta  " + cuenta + " el saldo a retirar[" + valor + "] supera el saldo Total de la cuenta [" + saldoTotal + "]");
                                            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                                            return;
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

                                        //if (valor < saldoTotal && tipomov == 2)
                                        //{

                                          //  VerError("En la cuenta  " + cuenta + " el valor a pagar [" + valor + "] no cubre el Valor del Cdat [" + saldoTotal + "]");
                                            //ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
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

                                if (numProd == codRadicado && Convert.ToInt64(ddlTipoProducto.SelectedValue) == tipoproducto)
                                {
                                    if (rblOpcRegistro.SelectedValue == "1")
                                    {
                                        result = 1;
                                        VerError("No se puede adicionar más de una transacción al producto");
                                        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                                        return;
                                    }
                                }
                                contador = Convert.ToInt64(numProd);

                            }


                            if (result == 1)// si el radicado existe para ese cliente entonces se inserta el dato 
                            {
                                VerError("");
                                if (valor > 0)// hay que validar que acepte valores mayores o iguales a cero si es el caso de tipo tran 5
                                {


                                    panelGridTran.Visible = true;
                                    ViewState["CuotaExtra"] = 1;
                                    LlenarTablaTran();
                                    ViewState["CuotaExtra"] = null;
                                    if (rblOpcRegistro.SelectedValue != "2")
                                        txtNumProducto.Text = "";
                                    txtValTransac.Text = "";
                                    txtReferencia.Text = "";
                               
                                }
                                else
                                {
                                    VerError("El Valor de Transacción debe ser mayor a cero");
                                    ExisteError = true;
                                }
                            }
                            else
                            {
                                VerError("El Radicado que ha digitado no coincide con el que aparece en la Consulta de Datos, por favor verificar.");
                                ExisteError = true;
                            }
                        }
                        else
                        {
                            VerError("Se debe Consultar Primero al Cliente");
                            ExisteError = true;
                        }
                    }
                    else
                    {
                        VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                        ExisteError = true;
                    }
                }
                else
                {
                    VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
                    ExisteError = true;
                }
            }
            else
            {
                VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
                ExisteError = true;
            }
            if (ExisteError == true)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                return;
            }

            Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
            Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
            persona.identificacion = txtIdentificacion.Text;
            persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
            persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);

            Actualizar(persona);
        }
    }

    protected void btnGoFormaPago_Click(object sender, EventArgs e)
    {
        decimal valor = 0;
        valor = txtValor.Text == "" ? 0 : decimal.Parse(txtValor.Text.Replace(".", ""));
        long result = 0;
        long result2 = 0;

        if (gvTransacciones.Rows.Count > 0)
        {
            if (valor > 0)
            {
                if (ddlFormaPago.SelectedValue == "10") // Si es por datafono  valide que se seleccionó el numero boucher 
                    if (txtBaucher.Text == "")
                    {
                        VerError("Debe digitar  el numero  de bocuher");
                        return;
                    }

                result = LlenarFormaPago5();

                if (result == 0)
                {

                    result2 = LlenarFormaPago2();

                    if (result2 == 1)
                        VerError("No se puede actualizar los valore de Forma de Pago Efectivo o Cheques, estos deben ser ingresados desde los paneles de Ingreso de Cada Uno");
                }
                else
                    VerError("El Valor de la Forma de Pago debe ser menor al valor Efectivo");
            }
            else
                VerError("El Valor de Forma de Pago debe ser Mayor a Cero");
        }
        else
            VerError("Debe registrar las transacciones primero");
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
                    result = LlenarFormaPago3();

                    if (result == 0)
                        LlenarTablaCheque();
                    else
                        VerError("El Valor del Cheque no puede ser Superior al Valor de Efectivo");
                }
                else
                    VerError("El Valor de Forma de Pago debe ser Mayor a Cero");
            }
            else
                VerError("Debe registrar las transacciones primero");
        }
    }
    /// <summary>
    /// Mètodo para aplicar las transacciones registradas segùn las formas de pago
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 

    protected void btnGuardarCod_Click(object sender, ImageClickEventArgs e)
    {
      
        btnGuardar_Click(sender, e);
    }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        string Error = "";
        if (Session["vengoDeTesoreria"] != null)
            Session.Remove("vengoDeTesoreria");

        string refe = "";
        string refe2 = "";
        Int16 Cheque = 0;
        Decimal MontoDiario = 0, MontoMensual = 0;
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


        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaja"].ToString()) == 1)
            {
                if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                {
                    if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                    {
                        if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                        {
                            decimal ValTotalFPago = 0;
                            decimal ValTotalTran = 0;
                            String ValTotalTranEfectivo = "";
                            decimal ValTotalTrEfectivo = 0;
                            decimal ValTotalTranfinal = 0;
                            nActiva = Convert.ToInt16(mvOperacion.ActiveViewIndex.ToString());
                            if (mvOperacion.Visible == false)
                            {
                                if (txtNumProducto.Text != "" && txtValTransac.Text != "" && txtValTransac.Text != "0")
                                {
                                    btnGoTran_Click(sender, e);
                                };
                                ValTotalFPago = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);// valor total de Forma de Pago
                                ValTotalTran = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text);//Valor Total de Tabla de Transacciones
                                if (ValTotalFPago == 0 && ValTotalTran == 0)
                                    return;
                                mvOperacion.Visible = true;
                                mvOperacion.ActiveViewIndex = 0;
                                panelTransaccion.Visible = false;
                            }
                            else
                            {
                                try
                                {
                                    if (mvOperacion.ActiveViewIndex == 0 /*&& rblOpcRegistro.SelectedValue == "1"*/)
                                    {
                                        ValTotalTran = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text);//Valor Total de Tabla de Transacciones
                                        ValTotalTranfinal = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text);//Valor Total de Tabla de Transacciones

                                        Xpinn.Comun.Entities.General pDataM = new Xpinn.Comun.Entities.General();
                                        Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
                                        pDataM = ConsultaData.ConsultarGeneral(16, (Usuario)Session["usuario"]);

                                        if (pDataM.valor != "" && pDataM.valor != null)
                                            MontoDiario = Convert.ToDecimal(pDataM.valor);


                                        pDataM = ConsultaData.ConsultarGeneral(17, (Usuario)Session["usuario"]);

                                        if (pDataM.valor != "" && pDataM.valor != null)
                                            MontoMensual = Convert.ToDecimal(pDataM.valor);

                                        // CAPTURANDO EL VALOR TOTAL EN EFECTIVO
                                        int tipoPago = 0;
                                        foreach (GridViewRow rFila in gvFormaPago.Rows)
                                        {
                                            if (!string.IsNullOrEmpty(rFila.Cells[1].Text))
                                            {
                                                tipoPago = Convert.ToInt32(rFila.Cells[1].Text);
                                                if (tipoPago == 1) // SI ES EFECTIVO
                                                {
                                                    ValTotalTranEfectivo = rFila.Cells[4].Text;
                                                    if (ValTotalTranEfectivo == "" || ValTotalTranEfectivo == "&nbsp;")
                                                    {
                                                        ValTotalTrEfectivo = 0;
                                                        break;
                                                    }
                                                    ValTotalTrEfectivo = Convert.ToDecimal(ValTotalTranEfectivo);
                                                    break;
                                                }
                                            }
                                        }

                                        if (ValTotalTrEfectivo > 0)
                                        {
                                            if (tranCajaServicio.ValidarControlOperacion(long.Parse(txtCodCliente.Text.ToString()), ref ValTotalTrEfectivo, Convert.ToDateTime(txtFechaTransaccion.Text), (Usuario)Session["usuario"], MontoDiario, MontoMensual))
                                            {
                                                Usuario pUsuario = (Usuario)Session["usuario"];
                                                mvOperacion.ActiveViewIndex = 1;
                                                DateTime Fecha = DateTime.Now;
                                                string iden = "";
                                                string Observaciones = "";
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
                                                dr[0] = municipio;
                                                dr[1] = Fecha.ToString("yyyy");
                                                dr[2] = Fecha.ToString("MM");
                                                dr[3] = Fecha.ToString("dd");
                                                dr[4] = pUsuario.nombre_oficina;
                                                dr[5] = refe2;//Cuenta de Ahorros #                                           
                                                if (ddlTipoMovimiento.SelectedValue == "2")
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
                                                dr[9] = ValTotalTrEfectivo.ToString("n2");
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
                                                    dr[11] = txtNumProducto.Text;
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
                                                string pData = Session["DirASo"] != null ? HttpUtility.HtmlEncode(Session["DirASo"].ToString()) : "";
                                                pData += Session["TelAso"] != null ? Session["TelAso"].ToString() : "";
                                                DirecionAsc = pData;
                                                dr[16] = DirecionAsc;
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
                                                if (ValTotalTran >= MontoDiario)
                                                {
                                                    Observaciones = "Transacción supera o es igual al  monto para operaciones diarias  de : " + MontoDiario.ToString("n2");
                                                }

                                                if (ValTotalTran >= MontoMensual)
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
                                                //mvOperacion.ActiveViewIndex = 1;                                            
                                                RpviewInfo.Visible = true;
                                                btnImprimiendose_Click();
                                                return;
                                            }
                                            else
                                                mvOperacion.ActiveViewIndex = 1;
                                        }
                                        else
                                        {
                                            RpviewInfo.Visible = false;
                                            //btnImprimirRep.Visible = false;
                                            mvOperacion.ActiveViewIndex = 1;
                                        }
                                    }
                                    else
                                        mvOperacion.ActiveViewIndex = 1;

                                    if (mvOperacion.ActiveViewIndex == 1)
                                    {
                                        mvOperacion.ActiveViewIndex = 0;
                                        //VALIDACION DE SALDO DE LA CAJA
                                        Xpinn.Caja.Entities.SaldoCaja saldo = new Xpinn.Caja.Entities.SaldoCaja();
                                        Xpinn.Caja.Services.SaldoCajaService saldoService = new Xpinn.Caja.Services.SaldoCajaService();
                                        saldo.cod_caja = long.Parse(Session["Caja"].ToString());
                                        saldo.cod_cajero = long.Parse(Session["Cajero"].ToString());
                                        saldo.tipo_moneda = 1;
                                        saldo.fecha = Convert.ToDateTime(txttransacciondia.Text);


                                        saldo = saldoService.ConsultarSaldoCaja(saldo, (Usuario)Session["usuario"]);
                                        if (ddlTipoMovimiento.SelectedItem != null && ddlTipoMovimiento.SelectedValue == "1")
                                        {
                                            if (saldo.valor <= 0)
                                            {
                                                VerError("La Caja no tiene Dinero disponible para realizar esta Operación");
                                                return;
                                            }
                                            else
                                            {
                                                decimal valorTotal = Convert.ToDecimal(txtValTotalFormaPago.Text.Replace("$", "").Replace(".", ""));
                                                if (valorTotal > saldo.valor)
                                                {
                                                    VerError("La Caja no tiene Dinero disponible para realizar esta Operación");
                                                    return;
                                                }
                                            }
                                        }

                                        ValTotalFPago = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);// valor total de Forma de Pago
                                        ValTotalTran = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text);//Valor Total de Tabla de Transacciones
                                        if (ValTotalFPago == 0 && ValTotalTran == 0)
                                        {
                                            VerError("Debe especificar los valores a pagar");
                                            return;
                                        }
                                        if (ValTotalFPago == ValTotalTran)// si son iguales en valor entonces deja guardar
                                        {
                                            if (txtCodCliente.Text == null || txtCodCliente.Text == "")
                                                BuscarPersona();
                                            tranCaja.cod_persona = long.Parse(txtCodCliente.Text.ToString());

                                            tranCaja.cod_caja = long.Parse(Session["Caja"].ToString());
                                            tranCaja.cod_cajero = long.Parse(Session["Cajero"].ToString());
                                            tranCaja.cod_oficina = long.Parse(Session["Oficina"].ToString());
                                            tranCaja.fecha_cierre = Convert.ToDateTime(txtFechaTransaccion.Text);
                                            tranCaja.observacion = txtObservaciones.Text;

                                            if (txtValorTran.Text != "")
                                                tranCaja.valor_pago = decimal.Parse(txtValorTran.Text);
                                            else
                                                tranCaja.valor_pago = 0;
                                            tranCaja.tipo_ope = 120; //Registro de Pagos

                                            tranCaja.baucher = this.txtBaucher.Text;

                                            VerError("");
                                            tranCaja = tranCajaServicio.CrearTransaccionCajaOperacion(tranCaja, gvTransacciones, gvFormaPago, gvCheques, (Usuario)Session["usuario"], ref Error);
                                            if (Error.Trim() != "")
                                            {
                                                VerError(Error);
                                                return;
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

                                                            long moneda = long.Parse(fila.Cells[2].Text);
                                                            long tipotran = fila.Cells[12].Text != "&nbsp;" ? long.Parse(fila.Cells[12].Text) : 0;
                                                            long tipomov = long.Parse(fila.Cells[5].Text);

                                                            string nomtipomov = tipomov == 2 ? "INGRESO" : "EGRESO";

                                                            decimal valor2 = decimal.Parse(fila.Cells[9].Text);
                                                            string nroRef = fila.Cells[8].Text;
                                                            string tippago = fila.Cells[12].Text != "&nbsp;" ? fila.Cells[12].Text : null;
                                                            string referencia = fila.Cells[13].Text;

                                                            TransaccionEnpacto transaccionEnpacto = new TransaccionEnpacto();

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
                                                            transaccionEnpacto.monto = (valor2 * 100).ToString();  // Sin carácter decimal, los últimos 2 dígitos son los centavos

                                                            RespuestaEnpacto respuesta = new RespuestaEnpacto();
                                                            string error = string.Empty;

                                                            try
                                                            {
                                                                // Mando a generar la transaccion de enpacto
                                                                string s_usuario_applicance = "webservice";
                                                                string s_clave_appliance = "WW.EE.99";
                                                                SeguridadConvenioTarjeta(ref s_usuario_applicance, ref s_clave_appliance);
                                                                interfazEnpacto.ConfiguracionAppliance(IpApplianceConvenioTarjeta(), s_usuario_applicance, s_clave_appliance);
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
                                                                        descripcion = txtObservaciones.Text,
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


                                            Session[Usuario.codusuario + "codOpe"] = tranCaja.cod_ope;

                                            Navegar("Factura.aspx");

                                        }
                                        else
                                        {
                                            VerError("El Valor Total de Transacción debe ser igual al Valor Total de Transacción distribuida en Formas de Pago");
                                        }
                                    }
                                }
                                catch (ExceptionBusiness ex)
                                {
                                    VerError(ex.Message);
                                }
                            }
                        }
                        else
                            VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                    }
                    else
                        VerError("La Oficina se encuentra por fuera del horario configurado");
                }
                else
                    VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
            }
            else
                VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
    }

    protected void AsignarEventoConfirmar()
    {
        //ConfirmarEventoBoton((LinkButton)Master.FindControl("btnGuardar"), "Desea Aplicar los Pagos?");
        //ImageButton pBoton = (ImageButton)Master.FindControl("btnGuardar");
        //pBoton.Attributes.Add("onClick", "return EvitarClickeoLoco();");
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (long.Parse(Session["estadoOfi"].ToString()) == 1)
        {
            if (long.Parse(Session["estadoCaja"].ToString()) == 1)
            {
                if (long.Parse(Session["conteoOfiHorario"].ToString()) == 1)
                {
                    if (long.Parse(Session["Resp1"].ToString()) == 1 && long.Parse(Session["Resp2"].ToString()) == 1)
                    {
                        if (long.Parse(Session["estadoCaj"].ToString()) == 1)
                        {
                            if (txtIdentificacion.Text != null && !string.Equals(txtIdentificacion.Text, ""))
                            {
                                Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
                                Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();

                                persona.identificacion = txtIdentificacion.Text;
                                //persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
                                VerError("");
                                persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);

                                if (persona.mensajer_error == "" || persona.mensajer_error == null)
                                {
                                    // Session["codpersona"] = persona.cod_persona;
                                    txtCodCliente.Text = Convert.ToString(persona.cod_persona);
                                    txtNombreCliente.Text = persona.nom_persona;
                                    BntVer.Visible = true;
                                    if (txtNombreCliente.Text == " ")
                                    {
                                        txtNombreCliente.Text = persona.razon_social;
                                    }

                                    ddlTipoIdentificacion.SelectedValue = persona.tipo_identificacion.ToString();
                                    Session["DirASo"] = HttpUtility.HtmlDecode(persona.direccion);
                                    Session["TelAso"] = persona.telefono;
                                    // aqui se coloca los datos de la persona, Nro Radicacion, Nombre, Valor CUota, saldo capital
                                    Actualizar(persona);
                                }
                                else
                                    VerError(persona.mensajer_error);
                            }
                        }
                        else
                            VerError("El Cajero se encuentra inactivo y no puede realizar Operación");
                    }
                    else
                        VerError("La Oficina se encuentra por fuera del horario configurado");
                }
                else
                    VerError("La Oficina no cuenta con un horario establecido para el día de hoy");
            }
            else
                VerError("La Caja se encuentra cerrada y no puede realizar Operaciones");
        }
        else
            VerError("La Oficina se encuentra cerrada y no puede realizar Operaciones");
    }

    protected void ConsultaEstadoJuridico(Int64 pIdObjeto)
    {
        VerError("");
        try
        {
            pIdObjeto = Convert.ToInt64(txtNumProducto.Text);
        }
        catch
        {
            VerError("Se presento error al determinar el número del producto ");
            return;
        }
        Xpinn.Asesores.Services.ProcesosCobroService procesosCobroServicio = new ProcesosCobroService();
        Xpinn.Asesores.Entities.ProcesosCobro vProceso = new Xpinn.Asesores.Entities.ProcesosCobro();
        ProcesosCobro proceso = new ProcesosCobro();
        proceso = procesosCobroServicio.ConsultarDatosProceso(pIdObjeto, (Usuario)Session["usuario"]);
        if (!string.IsNullOrEmpty(proceso.descripcion))
            txtEstado.Text = HttpUtility.HtmlDecode(proceso.descripcion) + " ...Por favor verificar el Estado de cuenta para consultar valor a pagar";
    }

    private Xpinn.Caja.Entities.Persona ObtenerValores()
    {
        Xpinn.Caja.Entities.Persona vPersona = new Xpinn.Caja.Entities.Persona();

        if (ddlTipoProducto.Text.Trim() != "")
            vPersona.linea_credito = Convert.ToString(ddlTipoProducto.SelectedValue);


        return vPersona;
    }



    protected void btnConsultarEstado_Click(object sender, EventArgs e)
    {

        Xpinn.Asesores.Entities.Producto producto = new Xpinn.Asesores.Entities.Producto();
        producto.Persona.IdPersona = long.Parse(txtCodCliente.Text);
        Session[MOV_GRAL_CRED_PRODUC] = producto;
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:validar();", true);
        //egar("~/Page/Asesores/EstadoCuenta/Detalle.aspx");

    }
    private string obtFiltro()
    {
        String filtro = String.Empty;


        return filtro;
    }

    protected List<Xpinn.Caja.Entities.Persona> ObtenerPrincipal(List<Xpinn.Caja.Entities.Persona> lstData)
    {
        try
        {
            List<Xpinn.Caja.Entities.Persona> lstAporte = new List<Xpinn.Caja.Entities.Persona>();
            lstAporte = lstData;
            if (lstData != null && lstData.Count > 1)
            {
                lstAporte = new List<Xpinn.Caja.Entities.Persona>();
                Xpinn.Caja.Entities.Persona pEntidad = new Xpinn.Caja.Entities.Persona();
                decimal saldo = 0, total = 0, valor = 0, cuota = 0;
                foreach (Xpinn.Caja.Entities.Persona nAporte in lstData)
                {
                    cuota += Convert.ToInt64(nAporte.valor_cuota.ToString().Replace(".", ""));
                    saldo += Convert.ToInt64(nAporte.saldo_capital.ToString().Replace(".", ""));
                    valor += Convert.ToInt64(nAporte.valor_a_pagar.ToString().Replace(".", ""));
                    total += Convert.ToInt64(nAporte.total_a_pagar.ToString().Replace(".", ""));
                    if (nAporte.estado == 1)
                        pEntidad = nAporte;
                }
                pEntidad.valor_cuota = Convert.ToInt64(cuota);
                pEntidad.saldo_capital = Convert.ToInt64(saldo);
                pEntidad.valor_a_pagar = Convert.ToInt64(valor);
                pEntidad.total_a_pagar = Convert.ToInt64(total);
                lstAporte.Add(pEntidad);
            }
            return lstAporte;
        }
        catch
        {
            return null;
        }
    }


    private void Actualizar(Xpinn.Caja.Entities.Persona pEntidad)
    {
        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();

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

            gvGiros.DataSource = null;
            gvGiros.Visible = false;
            divDatos.Visible = false;

            if (rblOpcRegistro.SelectedValue != "2")
                txtNumProducto.Text = "";
            txtValTransac.Text = "";
            ddlMonedas.SelectedIndex = 0;
            txtEstado.Text = "";


            if (ddlTipoProducto.SelectedValue == "1") //APORTES
            {
                txtReferencia.Visible = false;
                LblReferencia.Visible = false;
                pEntidad.tipo_linea = Convert.ToInt64(Session["tipoproducto"]);
                lstConsulta = personaService.ListarDatosCreditoPersona(pEntidad, (Usuario)Session["usuario"]);
                //lstConsulta = ObtenerPrincipal(lstConsulta);
                if (lstConsulta.Count > 0)
                {
                    divDatos.Visible = true;
                    gvConsultaDatos.Visible = true;
                    gvConsultaDatos.DataSource = lstConsulta;
                    gvConsultaDatos.DataBind();
                }
            }
            else if (ddlTipoProducto.SelectedValue == "2") //CREDITOS
            {
                txtReferencia.Visible = false;
                LblReferencia.Visible = false;
                pEntidad.tipo_linea = Convert.ToInt64(Session["tipoproducto"]);
                lstConsulta = personaService.ListarDatosCreditoPersona(pEntidad, (Usuario)Session["usuario"]);//, (UserSession)Session["user"]);

                if (lstConsulta.Count > 0)
                {
                    divDatos.Visible = true;
                    gvConsultaDatos.Visible = true;
                    gvConsultaDatos.DataSource = lstConsulta;
                    gvConsultaDatos.DataBind();
                }
            }
            else if (ddlTipoProducto.SelectedValue == "3") //AHORROS VISTA
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
                txtReferencia.Visible = true;
                LblReferencia.Visible = true;
                lstAhorros = ReporteMovService.ListarAhorroVista(filtro, pFechaApert, (Usuario)Session["usuario"]);
                if (lstAhorros.Count > 0)
                {
                    gvAhorroVista.Visible = true;
                    gvAhorroVista.DataSource = lstAhorros;
                    gvAhorroVista.DataBind();
                    divDatos.Visible = true;
                }
            }

            else if (ddlTipoProducto.SelectedValue == "4") //SERVICIOS
            {
                Xpinn.Servicios.Services.AprobacionServiciosServices AproServicios = new Xpinn.Servicios.Services.AprobacionServiciosServices();
                List<Xpinn.Servicios.Entities.Servicio> lstServicios = new List<Xpinn.Servicios.Entities.Servicio>();
                String filtro = " and S.COD_PERSONA = " + pEntidad.cod_persona + " AND S.ESTADO = 'C'   AND S.saldo !=0 ";

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

            else if (ddlTipoProducto.SelectedValue == "5") //CDATS
            {
                Xpinn.CDATS.Services.AperturaCDATService AperturaService = new Xpinn.CDATS.Services.AperturaCDATService();
                List<Xpinn.CDATS.Entities.Cdat> lstCdat = new List<Xpinn.CDATS.Entities.Cdat>();
                String filtro = " AND C.ESTADO = 1 and  T.COD_PERSONA = " + pEntidad.cod_persona + " AND T.PRINCIPAL = 1 ";
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
            else if (ddlTipoProducto.SelectedValue == "6")//AFILIACION
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
            else if (ddlTipoProducto.SelectedValue == "9") //AHORRO PROGRAMADO
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
            else if (ddlTipoProducto.SelectedValue == "10") //GIROS
            {
                Xpinn.Tesoreria.Services.RealizacionGirosServices RealizacionService = new Xpinn.Tesoreria.Services.RealizacionGirosServices();
                List<Xpinn.Tesoreria.Entities.Giro> lstGiros = new List<Xpinn.Tesoreria.Entities.Giro>();
                Xpinn.Tesoreria.Entities.Giro vGiro = new Xpinn.Tesoreria.Entities.Giro();
                vGiro.forma_pago = 1;
                if (pEntidad.cod_persona > 0)
                {
                    vGiro.cod_persona = pEntidad.cod_persona;
                    lstGiros = RealizacionService.ListarGiroAprobados(vGiro, "", DateTime.MinValue, DateTime.MinValue, false, (Usuario)Session["usuario"]);
                    if (lstGiros.Count > 0)
                    {
                        gvGiros.DataSource = lstGiros;
                        gvGiros.DataBind();
                        divDatos.Visible = true;
                        gvGiros.Visible = true;
                    }
                }
            }

            Session.Add(personaService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(personaService.GetType().Name + "L", "Actualizar", ex);
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

    private void AdicionarGiro()
    {
        if (ViewState["Productos"] != null)
        {
            List<Xpinn.Caja.Entities.TipoOperacion> lstTipo = new List<TipoOperacion>();
            lstTipo = (List<Xpinn.Caja.Entities.TipoOperacion>)ViewState["Productos"];

            if (rblOpcRegistro.SelectedIndex == 0)
            {
                if (ddlTipoMovimiento.SelectedItem != null)
                {
                    if (ddlTipoMovimiento.SelectedIndex == 1) //EGRESO
                    {
                        int CantItem = 0;
                        CantItem = ddlTipoProducto.Items.Count;
                        ddlTipoProducto.Items.Insert(CantItem, new ListItem("Giros", "10"));
                    }
                    else
                        LimpiarGiro();
                }
            }
            else
                LimpiarGiro();
        }
    }

    private void LimpiarGiro()
    {
        if (ddlTipoProducto.SelectedItem != null)
        {
            string item = string.Empty;
            foreach (ListItem nItem in ddlTipoProducto.Items)
            {
                item = nItem.Text;
                if (item == "Giros")
                {
                    ddlTipoProducto.Items.Remove(nItem);
                    break;
                }
            }
        }
    }

    protected void ddlTipoMovimiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        AdicionarGiro();
        ddlTipoProducto_SelectedIndexChanged(ddlTipoProducto, null);
    }

    protected void ddlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Se colocó para iniciar la variable de sesión para efecto de la validación. FerOrt. 27-Jun-2019
        Session["tipoproducto"] = "";
        txtNumCuotas.Visible = false;
        lblNumeroCuotas.Visible = false;
        txtValTransac.Enabled = true;
        Xpinn.Caja.Services.TipoOperacionService tipoOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoProducto tipProd = new Xpinn.Caja.Entities.TipoProducto();
        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
        Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();

        if (ddlTipoProducto.SelectedValue != "")
        {
            tipProd.cod_tipo_producto = Convert.ToInt64(ddlTipoProducto.SelectedValue);
            tipProd = tipoOpeService.ConsultarTipoProducto(tipProd, (Usuario)Session["usuario"]);

            if (tipProd.cod_tipo_producto == 1)//aportes
            {
                persona.linea_credito = "1";
                txtNumProducto.Enabled = false;
            }
            if (tipProd.cod_tipo_producto == 2)//creditos
            {
                persona.linea_credito = "2";
                txtNumProducto.Enabled = false;
            }
            if (tipProd.cod_tipo_producto == 3)//ahorros vista
            {
                persona.linea_credito = "3";
                txtNumProducto.Enabled = false;
            }
            if (tipProd.cod_tipo_producto == 5)//cdats
            {
                persona.linea_credito = "5";
                txtNumProducto.Enabled = false;
            }
            if (tipProd.cod_tipo_producto == 9)//ahorro programado
            {
                persona.linea_credito = "9";
                txtNumProducto.Enabled = false;
            }
            Session["tipoproducto"] = tipProd.cod_tipo_producto;

            LlenarComboTipoPago(tipProd.cod_tipo_producto);
        }
        if (tipProd.cod_tipo_producto == 7)//otros
        {

            txtNumProducto.Enabled = true;
        }

        if (tipProd.cod_tipo_producto == 6)
        {
            lblMsgNroProducto.Text = "El número de producto no es obligatorio colocarlo en este tipo de transacción, colocar cero en el campo de número de producto ";
            txtNumProducto.Enabled = true;
        }
        else
            lblMsgNroProducto.Text = "";

        persona.identificacion = txtIdentificacion.Text;
        persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
        persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);
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

    protected void gvLista_PageIndexChanging(object sender, System.EventArgs e)
    {

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

    void Totalizar_Pago_Productos(Xpinn.Caja.Entities.Persona pEntidad)
    {
        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
        List<Xpinn.Caja.Entities.Persona> lstConsulta_Aporte = new List<Xpinn.Caja.Entities.Persona>();
        List<Xpinn.Caja.Entities.Persona> lstConsulta_Credito = new List<Xpinn.Caja.Entities.Persona>();
        List<Xpinn.Caja.Entities.Persona> lstAfiliacion = new List<Xpinn.Caja.Entities.Persona>();
        Decimal Valor_Total = 0;
        pEntidad.fecha_pago = Convert.ToDateTime(txtFechaTransaccion.Text);
        //Aportes
        pEntidad.tipo_linea = Convert.ToInt64(1);
        txtReferencia.Visible = false;
        LblReferencia.Visible = false;
        lstConsulta_Aporte = personaService.ListarDatosCreditoPersona(pEntidad, (Usuario)Session["usuario"]);

        //Creditos
        pEntidad.tipo_linea = Convert.ToInt64(2);
        pEntidad.fecha_pago = ConvertirStringToDateN(txtFechaTransaccion.Text);
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
        DateTime? pFecPago = ConvertirStringToDateN(txtFechaTransaccion.Text);
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
                Valor_Total = Valor_Total + elem.total_a_pagar ;
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
                Servicios.total_a_pagar = Convert.ToInt64(elem.total_calculado);
                Valor_Total = Valor_Total + Convert.ToInt64(elem.total_calculado);
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

            valFormaPagoTotal = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);
            txtValTotalFormaPago.Text = acum.ToString();

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
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "L", "gvTransacciones_RowDeleting", ex);
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
                        result = 1;
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

            valFormaPagoTotal = txtValTotalFormaPago.Text == "" ? 0 : decimal.Parse(txtValTotalFormaPago.Text);
            txtValTotalFormaPago.Text = acum.ToString();

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
            BOexcepcion.Throw(tranCajaServicio.GetType().Name + "L", "gvTransacciones_RowDeleting", ex);
        }
    }
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
                    ListaDetallePago = DetallePagoService.DistribuirPago(Convert.ToInt64(stippro), Convert.ToInt32(snumpro), fecha_pago, Convert.ToInt32(svalor), stipopago, (Usuario)Session["usuario"]);

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
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
    }

    protected void btnCloseAct2_Click(object sender, EventArgs e)
    {
        MpeDetallePagoAportes.Hide();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
    }


    /// <summary>
    /// Segùn el tipo de pago muestra los valores
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlTipoPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtValTransac.Enabled = true;
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
        if (ddlTipoProducto.SelectedValue == "5")
        {
            // Segùn el tipo de pago ajustar los datos
            CalcularValordelCdat();
        }


    }
    private void CalcularValordelCdat()
    {
        foreach (GridViewRow fila in this.gvCdat.Rows)
        {
            Decimal saldoTotal = decimal.Parse(fila.Cells[11].Text);
            txtValorTran.Text = Convert.ToString(saldoTotal);

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
        ConsultaEstadoJuridico(numero_radicacion);
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

    /// <summary>
    /// Validar que cuando cambia el nùmero de producto se actualice el valor a pagar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtNumProducto_TextChanged(object sender, EventArgs e)
    {
        CalcularValordelCredito();
    }


    protected void gvConsultaDatos_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        Xpinn.Caja.Services.TipoOperacionService tipoOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoProducto tipProd = new Xpinn.Caja.Entities.TipoProducto();
        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
        Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();

        txtNumProducto.Text = evt.CommandName;
        Xpinn.Caja.Entities.Persona lineacreditos = new Xpinn.Caja.Entities.Persona();
        Xpinn.Caja.Services.PersonaService PersonaServicio = new Xpinn.Caja.Services.PersonaService();
        lineacreditos = PersonaServicio.ConsultarDatosCreditoPersona(Convert.ToString(txtNumProducto.Text), (Usuario)Session["usuario"]);
        txtlinea2.Text = Convert.ToString(lineacreditos.cod_linea_credito);

        TransaccionCaja parametrocastigos = new TransaccionCaja();
        parametrocastigos = tranCajaServicio.ConsultarParametroCastigos((Usuario)Session["usuario"]);
        if (parametrocastigos.parametro != null)
            if (!string.IsNullOrEmpty(parametrocastigos.parametro.ToString()))
                txtlinea.Text = HttpUtility.HtmlDecode(parametrocastigos.parametro);



        if (txtlinea.Text == txtlinea2.Text)
        {
            //ddlTipoProducto.Enabled = false;
            //ddlTipoProducto.SelectedIndex = 1;

            tipProd.cod_tipo_producto = Convert.ToInt64(ddlTipoProducto.SelectedValue);
            tipProd = tipoOpeService.ConsultarTipoProducto(tipProd, (Usuario)Session["usuario"]);
            if (tipProd.cod_tipo_producto == 1)
            {
                persona.linea_credito = "1";
            }
            if (tipProd.cod_tipo_producto == 2)
            {
                persona.linea_credito = "2";
            }
            if (tipProd.cod_tipo_producto == 3)
            {
                persona.linea_credito = "3";
            }
            if (tipProd.cod_tipo_producto == 3)//ahorros vista
            {
                persona.linea_credito = "3";
            }
            if (tipProd.cod_tipo_producto == 5)//cdats
            {
                persona.linea_credito = "5";

            }
            if (tipProd.cod_tipo_producto == 9)//ahorro programado
            {
                persona.linea_credito = "9";
            }
            Session["tipoproducto"] = tipProd.cod_tipo_producto;

            //añadido para credito rotativo 
            Int64 linea = lineacreditos.tipo_linea;
            DateTime fechahoy1 = Convert.ToDateTime(txtFechaTransaccion.Text);
            DateTime fechapago1 = Convert.ToDateTime(lineacreditos.fecha_pago);

            if (linea == 2 && fechapago1 > fechahoy1)
            {
                LlenarComboTipoPagoRotativo1(Convert.ToInt64(ddlTipoProducto.SelectedValue));
                Session["TipoProductoRotativo"] = ddlTipoProducto.SelectedValue;
            }
            if (linea == 2 && fechapago1 < fechahoy1)
            {
                LlenarComboTipoPagoRotativo(Convert.ToInt64(ddlTipoProducto.SelectedValue));
                Session["TipoProductoRotativo"] = ddlTipoProducto.SelectedValue;
            }
            else
                LlenarComboTipoPago(Convert.ToInt64(ddlTipoProducto.SelectedValue));

            if (ddlTipoPago.SelectedValue == null)
            {
                ddlTipoPago.SelectedValue = "50000";
            }
            ddlTipoPago.Enabled = false;
            if (tipProd.cod_tipo_producto == 5)
                lblMsgNroProducto.Text = "El número de producto no es obligatorio colocarlo en este tipo de transacción, colocar cero en el campo de número de producto ";
            else
                lblMsgNroProducto.Text = "";
        }

        //añadido para credito rotativo 
        Int64 linea2 = lineacreditos.tipo_linea;
        DateTime fechahoy = Convert.ToDateTime(txtFechaTransaccion.Text);
        DateTime fechapago = Convert.ToDateTime(lineacreditos.fecha_pago);

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

        Decimal Valor_cdat = Convert.ToDecimal(saldoTotal);
        Decimal Valor_recibido = Convert.ToDecimal(txtValTransac.Text);
        decimal Valor_parcial = Convert.ToDecimal(Valorparcial);
        decimal Valor_Pendiente = 0;
        txtValTransac.Text = Convert.ToString(saldoTotal);


        if (Valor_parcial < Valor_cdat)
        {
            Valor_Pendiente = Convert.ToDecimal(Valor_cdat - Valor_parcial);
            txtValTransac.Text = Convert.ToString(Valor_Pendiente);
        }

       

        if (Valor_recibido > Valor_cdat)
        { 
            txtValTransac.Text = Convert.ToString(saldoTotal);
            txtValTransac.Enabled = false;
        }
        else
        {
            txtValTransac.Enabled = true;
        }

        e.NewEditIndex = -1;

    }


    //SERVICIOS
    protected void gvServicios_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvServicios.DataKeys[e.NewEditIndex].Values[0].ToString();
        txtNumProducto.Text = id;
        e.NewEditIndex = -1;
    }

    protected void gvGiros_RowEditing(object sender, GridViewEditEventArgs e)
    {
        //AJUSTAR
        VerError("");
        string id = gvGiros.DataKeys[e.NewEditIndex].Values[0].ToString();
        txtNumProducto.Text = id;
        String saldoTotal = gvGiros.Rows[e.NewEditIndex].Cells[7].Text;
        txtValTransac.Text = Convert.ToString(saldoTotal);
        txtValTransac.Enabled = false;
        e.NewEditIndex = -1;
    }

    protected void gvGiros_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
            Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
            gvGiros.PageIndex = e.NewPageIndex;
            persona.identificacion = txtIdentificacion.Text;
            persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
            persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);
            Actualizar(persona);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(tranCajaServicio.CodigoPrograma3, "gvGiros_PageIndexChanging", ex);
        }
    }

    protected void BuscarPersona()
    {
        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
        Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
        persona.identificacion = txtIdentificacion.Text;
        persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
        VerError("");
        persona = personaService.ConsultarPersona(persona, (Usuario)Session["usuario"]);
        if (persona.mensajer_error == "")
        {
            //Session["codpersona"] = persona.cod_persona;
            txtCodCliente.Text = Convert.ToString(persona.cod_persona);
            txtNombreCliente.Text = persona.nom_persona;
            // aqui se coloca los datos de la persona, Nro Radicacion, Nombre, Valor CUota, saldo capital
            Actualizar(persona);
        }
        else
        {
            VerError(persona.mensajer_error);
        }
    }

    protected void rblOpcRegistro_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            AdicionarGiro();
            lblTitulo.Text = "Datos del Cliente";
            VisibleControls(true);

            if (rblOpcRegistro.SelectedItem != null && rblOpcRegistro.SelectedValue == "2")
            {
                lblTitulo.Text = "Datos del Convenio";
                VisibleControls(false);
                ImgBntCod.Visible = true;
                LblReferencia.Visible = true;
                txtReferencia.Visible = true;
                ddlTipoMovimiento.SelectedIndex = 0;
                LimpiarControls();
                EnabledControls(false);
            }
            else
            {

                ImgBntCod.Visible = false ;
                if (ViewState["Productos"] != null)
                {
                    List<TipoOperacion> lstTipoProd = new List<TipoOperacion>();
                    lstTipoProd = (List<TipoOperacion>)ViewState["Productos"];
                    if (lstTipoProd.Count > 0)
                    {
                        ddlTipoProducto.DataTextField = "nom_tipo_producto";
                        ddlTipoProducto.DataValueField = "tipo_producto";
                        ddlTipoProducto.DataSource = lstTipoProd;
                        ddlTipoProducto.DataBind();
                        if (ddlTipoProducto.SelectedItem != null)
                            LlenarComboTipoPago(Convert.ToInt64(ddlTipoProducto.SelectedValue));
                    }
                }
                else
                    LlenarComboTipoProducto(ddlTipoProducto);
                ddlTipoProducto_SelectedIndexChanged(ddlTipoProducto, null);
                EnabledControls(true);
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void ddlTipoConvenio_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlTipoProducto.Items.Clear();
            ddlTipoPago.Items.Clear();
            //Generar consulta Convenio_Recuado
            if (ddlTipoConvenio.SelectedIndex != 0)
            {
                if (ddlTipoConvenio.SelectedValue == "" || ddlTipoConvenio.SelectedValue == null)
                    return;
                ConveniosRecaudo pEntidad = new ConveniosRecaudo();
                pEntidad = BOConvenioService.ConsultarConvenioRecaudo(Convert.ToInt64(ddlTipoConvenio.SelectedValue), (Usuario)Session["usuario"]);

                //Asignando datos consultados.
                if (pEntidad != null)
                {
                    //Cargando datos del cliente
                    if (pEntidad.tipo_identificacion != 0)
                        ddlTipoIdentificacion.SelectedValue = pEntidad.tipo_identificacion.ToString();
                    if (pEntidad.identificacion != null)
                        txtIdentificacion.Text = pEntidad.identificacion.Trim();
                    if (pEntidad.cod_persona != Int64.MinValue)
                        txtCodCliente.Text = pEntidad.cod_persona.ToString();
                    if (pEntidad.nombre_persona != null)
                        txtNombreCliente.Text = pEntidad.nombre_persona.Trim();
                    if (pEntidad.numero_producto != null)
                        txtNumProducto.Text = pEntidad.numero_producto.Trim();
                    //Cargando los DropDown
                    if (pEntidad.tipo_producto != 0)
                    {
                        if (pEntidad.nombre_produc != null)
                            ddlTipoProducto.Items.Insert(0, new ListItem(pEntidad.nombre_produc.Trim(), pEntidad.tipo_producto.ToString()));
                    }
                    if (pEntidad.tipo_tran != null)
                    {
                        if (pEntidad.nombre_tran != null)
                            ddlTipoPago.Items.Insert(0, new ListItem(pEntidad.nombre_tran.Trim(), pEntidad.tipo_tran.ToString()));
                    }
                }
            }
            else
            {
                //Limpiando Campos
                LimpiarControls();
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
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
                        deudaTotal = decimal.Parse(fila.Cells[pFilaValor].Text);
                    if (codRadicado == numProd)
                    {
                        if (valor > deudaTotal)
                        {
                            VerError("En el " + pTipoProducto + " " + codRadicado + " el valor a pagar [" + valor.ToString("n0") + "] supera el valor total adeudado [" + deudaTotal.ToString("n0") + "]");
                            return false;
                        }
                        break;
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


    protected void LimpiarControls()
    {
        ddlTipoProducto.Items.Clear();
        ddlTipoPago.Items.Clear();
        ddlTipoIdentificacion.SelectedIndex = 0;
        txtIdentificacion.Text = "";
        txtNombreCliente.Text = "";
        txtNumProducto.Text = "";
        txtValor.Text = "";
        ddlMonedas.SelectedIndex = 0;
        txtReferencia.Text = "";
    }

    protected void VisibleControls(bool variable)
    {
        panelGrids.Visible = variable;
        btnConsultaPersonas.Visible = variable;
        btnConsultar.Visible = variable;
        lblTipoConvenio.Visible = variable == true ? false : true;
        ddlTipoConvenio.Visible = variable == true ? false : true;
        lblComentario.Visible = variable;
        txtEstado.Visible = variable;
    }

    protected void EnabledControls(bool variable)
    {
        txtIdentificacion.Enabled = variable;
        ddlTipoIdentificacion.Enabled = variable;
        ddlTipoProducto.Enabled = variable;
        ddlTipoMovimiento.Enabled = variable;
        txtNumProducto.Enabled = variable;
        ddlTipoPago.Enabled = variable;
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
    void MostrarArchivoEnLiteralL(byte[] bytes)
    {
        Usuario pUsuario = Usuario;

        string pNomUsuario = pUsuario.nombre != "" && pUsuario.nombre != null ? "_Declaracion" + pUsuario.nombre : "";
        // ELIMINANDO ARCHIVOS GENERADOS
        try
        {
            string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Archivos\\"));
            foreach (string ficheroActual in ficherosCarpeta)
                if (ficheroActual.Contains(pNomUsuario))
                    File.Delete(ficheroActual);
        }
        catch
        { }

        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("Archivos/output" + pNomUsuario + ".pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        //MOSTRANDO REPORTE
        string adjuntar = "<object data=\"{0}\" type=\"application/pdf\" width=\"90%\" height=\"700px\">";
        adjuntar += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
        adjuntar += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        adjuntar += "</object>";

        LiteralDcl.Text = string.Format(adjuntar, ResolveUrl("Archivos/output" + pNomUsuario + ".pdf"));
        LiteralDcl.Visible = true;
    }

    protected void txtNumCuotas_TextChanged(object sender, EventArgs e)
    {
        CalcularValordelCredito();
    }


    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
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

    protected void btnConsultaAvances_Click(object sender, EventArgs e)
    {
        listadoavances.Motrar(true, txtNumProducto.Text, "txtAvances", "txtValTransac");
    }

    protected void mpePopUp_OnClick(object sender, EventArgs e)
    {
        RpviewInfo.Visible = true;
        btnImprimiendose_Click();
    }

    protected void ImgBntCod_Click(object sender, EventArgs e)
    {
        if (PnlCod.Visible)
        {
            PnlCod.Visible = false;
        }
        else
        {
            PnlCod.Visible = true;
            txtCodBarras.Focus();
        }
    }

    protected void btneCancelar_Click(object sender, EventArgs e)
    {
        GeneradorDocumento.Hide();
    }



    protected void btnValidar_Click(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Services.ConvenioRecaudoService ConvenioServices = new Xpinn.Tesoreria.Services.ConvenioRecaudoService();
        List<ConvenioRecaudo> vDetalle = new List<ConvenioRecaudo>();
        //(415)7709998010833(8020)000022784535(3900)04790900(96)20200123
        ObtenerValoresEAN();
        vDetalle = ConvenioServices.ListarConvenios(ObtenerValoresFiltro(_convenioEAN), (Usuario)Session["usuario"]);
       
        foreach (ConvenioRecaudo row in vDetalle)
        {


            ddlTipoConvenio.SelectedValue = row.cod_convenio.ToString();
            txtValor.Enabled = true;
            ddlTipoConvenio_SelectedIndexChanged(sender, e);
            txtValTransac.Text  = decimal.Parse(_ValorEAN).ToString();
            txtReferencia.Text = _refEAN;
            valoring.Focus();

        }

    }




    protected void txtCodBarras_KeyUp(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Services.ConvenioRecaudoService ConvenioServices = new Xpinn.Tesoreria.Services.ConvenioRecaudoService();
        List< ConvenioRecaudo> vDetalle = new List<ConvenioRecaudo>();
        //(415)7709998010833(8020)000022784535(3900)04790900(96)20200123
        ObtenerValoresEAN();
        vDetalle = ConvenioServices.ListarConvenios(ObtenerValoresFiltro(_convenioEAN), (Usuario)Session["usuario"]);

        foreach (ConvenioRecaudo row in vDetalle)
        {
            
            ddlTipoConvenio.SelectedValue = row.cod_convenio.ToString();
            txtValTransac.Text = decimal.Parse(_ValorEAN).ToString();
            ddlTipoConvenio_SelectedIndexChanged(sender, e);
            Page.RegisterRequiresPostBack(ddlTipoConvenio);
            txtReferencia.Text = _refEAN;
            valoring.Focus();

        }


            //Cargar_trans(sender, e);
        }


    private string ObtenerValoresFiltro(String EAN)
    {
        string filtro = "";
       



        if (txtCodBarras.Text != "")
        {


            filtro = " where EAN = " + EAN;
        }



        return filtro;
    }

    private string ObtenerValoresEAN()
    {
        string filtro = "";
        string Convenio = "";
        string Valor = "";
        string Fecha = "";
        string Referencia = "";

        //(415)7709998010833(8020)000022784535(3900)04790900(96)20200123

       

        Fecha = txtCodBarras.Text.Substring(txtCodBarras.Text.Length - 8, 8);

        Convenio = txtCodBarras.Text.Substring(3, 13);

        int CantReferencia = txtCodBarras.Text.IndexOf("3900") - 20;

        Referencia = txtCodBarras.Text.Substring(20, CantReferencia);               

        int CantValor = txtCodBarras.Text.IndexOf("3900") + 4;

        int CantFecha = txtCodBarras.Text.IndexOf("96") ;

        if (CantFecha == -1) { CantFecha = txtCodBarras.Text.Length;  }

        Valor = txtCodBarras.Text.Substring(CantValor, CantFecha - CantValor);

        _convenioEAN = Convenio;
        _refEAN = Referencia;
        _ValorEAN = Valor;
        _FechaEAN = Fecha;





        return filtro;
    }
}
