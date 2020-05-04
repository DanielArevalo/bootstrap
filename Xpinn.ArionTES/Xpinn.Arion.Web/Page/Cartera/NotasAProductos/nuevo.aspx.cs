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
using Xpinn.Asesores.Entities;

public partial class Nuevo : GlobalWeb
{

    private Xpinn.Tesoreria.Services.PagosVentanillaService ventanillaServicio = new Xpinn.Tesoreria.Services.PagosVentanillaService();
    private Xpinn.Caja.Services.DetallePagService DetallePagoService = new Xpinn.Caja.Services.DetallePagService();
    private Xpinn.Cartera.Services.NotasAProductoService NotasProdService = new Xpinn.Cartera.Services.NotasAProductoService();
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    private Xpinn.Programado.Services.CuentasProgramadoServices cuentasProgramado = new Xpinn.Programado.Services.CuentasProgramadoServices();
    private Xpinn.CDATS.Services.LiquidacionCDATService LiquiService = new Xpinn.CDATS.Services.LiquidacionCDATService();
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    private Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();
    private Xpinn.Servicios.Services.CierreHistorioService ServiceServicio = new Xpinn.Servicios.Services.CierreHistorioService();
    private Xpinn.Asesores.Services.DetalleProductoService DetalleProducto = new Xpinn.Asesores.Services.DetalleProductoService();


    decimal ValTotalTran = 0;
    DateTime fecha;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            VisualizarOpciones(NotasProdService.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoLimpiar += btnLimpiar_Click;
            toolBar.eventoGuardar += btnGuardar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
            panelProceso.Visible = false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(NotasProdService.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                divDatos.Visible = false;
                mvOperacion.Visible = false;
                string ip = Request.ServerVariables["REMOTE_ADDR"];

                Session["val"] = 0;
                ObtenerDatos();

                // Crea los DATATABLES para registrar las transacciones, los cheques
                CrearTablaTran();
                CargarDropDown();
                // Llenar los DROPDOWNLIST de tipos de monedas, tipos de identificaciòn, formas de pago y entidades bancarias
                LlenarComboTipoProducto(ddlTipoProducto);//se carga los tipos de transaccion

                rblTipoNota.SelectedIndex = 0;
                rblTipoNota_SelectedIndexChanged(rblTipoNota, null);

                mvOperacion.ActiveViewIndex = 0;
            }

        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(NotasProdService.GetType().Name + "A", "Page_Load", ex);
        }

    }


    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        divDatos.Visible = false;
        gvConsultaDatos.DataSource = null;
        gvConsultaDatos.Visible = false;
        txtIdentificacion.Text = "";
        txtIdentificacion.Enabled = true;
        mvOperacion.Visible = false;
        txtNombreCliente.Text = "";
        ddlTipoIdentificacion.SelectedIndex = 0;
        ddlTipoProducto.SelectedIndex = 0;
        txtValorTran.Text = "";
        txtNumProducto.Text = "";
        txtValTransac.Text = "";
        txtObservacion.Text = "";
        gvTransacciones.DataSource = null;
        gvTransacciones.DataBind();
        CrearTablaTran();
    }


    protected void LlenarComboTipoProducto(DropDownList ddlTipoProducto)
    {
        TipoOperacionService tipoopeservices = new TipoOperacionService();

        // Inicializar las variables        
        List<TipoOperacion> lsttipo = new List<TipoOperacion>();

        // Cargando listado de tipos de productos
        lsttipo = tipoopeservices.ListarTipoProducto(Usuario);
        ddlTipoProducto.DataTextField = "nom_tipo_producto";
        ddlTipoProducto.DataValueField = "tipo_producto";
        ddlTipoProducto.DataSource = lsttipo;
        ddlTipoProducto.DataBind();
    }


    private void CargarDropDown()
    {
        //TIPO IDENTIFICACION
        Xpinn.Caja.Services.TipoIdenService IdenService = new Xpinn.Caja.Services.TipoIdenService();
        Xpinn.Caja.Entities.TipoIden identi = new Xpinn.Caja.Entities.TipoIden();
        ddlTipoIdentificacion.DataSource = IdenService.ListarTipoIden(identi, Usuario);
        ddlTipoIdentificacion.DataTextField = "descripcion";
        ddlTipoIdentificacion.DataValueField = "codtipoidentificacion";
        ddlTipoIdentificacion.DataBind();

        // TIPO MONEDA
        Xpinn.Caja.Services.TipoMonedaService monedaService = new Xpinn.Caja.Services.TipoMonedaService();
        Xpinn.Caja.Entities.TipoMoneda moneda = new Xpinn.Caja.Entities.TipoMoneda();
        ddlMonedas.DataSource = monedaService.ListarTipoMoneda(moneda, Usuario);
        ddlMonedas.DataTextField = "descripcion";
        ddlMonedas.DataValueField = "cod_moneda";
        ddlMonedas.DataBind();

        //ATRIBUTO
        Xpinn.FabricaCreditos.Services.LineasCreditoService Atributosservicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        Xpinn.FabricaCreditos.Entities.Atributos atributo = new Xpinn.FabricaCreditos.Entities.Atributos();
        ddlAtributo.DataSource = Atributosservicio.ListarAtributos(atributo, Usuario);
        ddlAtributo.DataTextField = "descripcion";
        ddlAtributo.DataValueField = "cod_atr";
        ddlAtributo.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        ddlAtributo.SelectedIndex = 0;
        ddlAtributo.DataBind();
    }



    protected void ObtenerDatos()
    {
        Configuracion conf = new Configuracion();
        try
        {
            txtFechaReal.Text = System.DateTime.Now.ToString();
            txtFechaNota.Text = System.DateTime.Now.ToString(conf.ObtenerFormatoFecha());
            if (!string.IsNullOrEmpty(Usuario.nombre_oficina))
                txtOficina.Text = Usuario.nombre_oficina;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(NotasProdService.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    protected void CrearTablaTran()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("tipo");         // codigo de tipo de transaccion
        dt.Columns.Add("tproducto");    // codigo de tipo de producto
        dt.Columns.Add("nroRef");       // nùmero del producto
        dt.Columns.Add("valor");
        dt.Columns.Add("moneda");
        dt.Columns.Add("atributo");
        dt.Columns.Add("nomtipo");      // nombre tipo transaccion 
        dt.Columns.Add("nommoneda");
        dt.Columns.Add("tipomov");      // tipo de movimiento
        dt.Columns.Add("nomtproducto");
        dt.Columns.Add("codtipopago");
        dt.Columns.Add("cod_atr");
        gvTransacciones.DataSource = dt;
        gvTransacciones.DataBind();
        gvTransacciones.Visible = false;
        Session["tablaSesion"] = dt;
    }




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

        string num_cre = "1", control = "", cod_atr = "";

        foreach (GridViewRow pos in gvTransacciones.Rows)
        {
            num_cre = pos.Cells[8].Text;
            cod_atr = pos.Cells[11].Text;
            if (num_cre == txtNumProducto.Text.Trim() && cod_atr == ddlAtributo.SelectedItem.Text.Trim())
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
            fila[1] = ddlTipoProducto.SelectedValue;
            if (txtNumProducto.Text.Trim() == "")           // Colocar el nùmero del producto
                fila[2] = "0";
            else
                fila[2] = txtNumProducto.Text;
            fila[3] = txtValTransac.Text.Replace(".", "");
            fila[4] = ddlMonedas.SelectedValue;             // Colocar el tipo de moneda de la transacciòn
            if (ddlTipoProducto.SelectedValue != "2") // DIFERENTE DE CREDITO
                fila[5] = "";
            else //CREDITO
                fila[5] = ddlAtributo.SelectedItem.Text;
            if (rblTipoNota.SelectedIndex == 0) // CREDITO
                fila[6] = "Nota Débito";
            else if (rblTipoNota.SelectedIndex == 1)
                fila[6] = "Nota Crédito"; // Colocar el tipo de producto de la transacciòn
            else if (rblTipoNota.SelectedIndex == 2)
                fila[6] = "Pago por Valor";
            else if (rblTipoNota.SelectedIndex == 3)
                fila[6] = "Pago Total";
            fila[7] = ddlMonedas.SelectedItem.Text;
            fila[8] = 0;
            fila[9] = ddlTipoProducto.SelectedItem.Text;
            fila[10] = 0;
            fila[11] = ddlAtributo.SelectedValue != "" && ddlAtributo.Visible != false ? ddlAtributo.SelectedValue : "";



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

        }
    }


    private Boolean ValidarMonto(GridView pGrid, decimal valor, int Index, ref int result, TipoDeProducto tipoProducto)
    {
        foreach (GridViewRow rFila in pGrid.Rows)
        {
            if (txtNumProducto.Text != "")
            {
                if (Convert.ToString(txtNumProducto.Text) == Convert.ToString(rFila.Cells[1].Text))
                {
                    if (tipoProducto == TipoDeProducto.AhorrosVista || tipoProducto == TipoDeProducto.AhorroProgramado)
                    {
                        result = 1;
                    }
                    else if (valor <= Convert.ToDecimal(rFila.Cells[Index].Text))
                    {
                        result = 1;
                    }
                    else
                    {
                        VerError("El Valor de la Transacción no puede ser mayor al valor del producto seleccionado");
                        return false;
                    }
                }
            }
            else
            {
                VerError("Seleccione un registro para realizar la Transacción");
                return false;
            }
        }
        return true;
    }

    protected void btnGoTran_Click(object sender, EventArgs e)
    {
        VerError("");

        TipoOperacionService tipoOpeService = new TipoOperacionService();
        TipoOperacion tipOpe = new TipoOperacion();
        TipoOperacion tipom = new TipoOperacion();

        // Cargar el valor de la transacciòn y el nùmero de producto
        // long codRadicado = 0;
        String codRadicado = "";
        int result = 0;
        decimal valor = !string.IsNullOrWhiteSpace(txtValTransac.Text) ? decimal.Parse(txtValTransac.Text.Replace(".", "")) : 0;
        //long numProd = txtNumProducto.Text == "" ? 0 : long.Parse(txtNumProducto.Text);
        String numProd = Convert.ToString(txtNumProducto.Text == "" ? 0 : long.Parse(txtNumProducto.Text));

        // Consultar datos del tipo de operaciòn a utilizar

        ViewState["tipoproducto"] = ddlTipoProducto.SelectedValue;
        TipoDeProducto tipoProducto = ddlTipoProducto.SelectedValue.ToEnum<TipoDeProducto>();



        Session["tipoproducto"] = ViewState["tipoproducto"];




        if (long.Parse(Session["tipoproducto"].ToString()) == 3)
        {
            // consultar  cierres historicos Ahorros a la vista 
            String estado = "";
            DateTime fechacierrehistorico;
            String format = gFormatoFecha;
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaNota.Text, format, CultureInfo.InvariantCulture);
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarCierreAhorroVista((Usuario)Session["usuario"]);
            estado = vAhorroVista.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vAhorroVista.fecha_cierre.ToString());

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
            //consultar cierre historico Ahorro Programado
            String estado = "";
            DateTime fechacierrehistorico;
            String format = gFormatoFecha;
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaNota.Text, format, CultureInfo.InvariantCulture);
            Xpinn.Programado.Entities.CuentasProgramado vAhorroProgramado = new Xpinn.Programado.Entities.CuentasProgramado();
            vAhorroProgramado = cuentasProgramado.ConsultarCierreAhorroProgramado((Usuario)Session["usuario"]);
            estado = vAhorroProgramado.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vAhorroProgramado.fecha_cierre.ToString());

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
            //consultar cierre historico Cdats
            String estado = "";
            DateTime fechacierrehistorico;
            String format = gFormatoFecha;
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaNota.Text, format, CultureInfo.InvariantCulture);

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
            //consultar cierre historico Aportes
            String estado = "";
            DateTime fechacierrehistorico;
            String format = gFormatoFecha;
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaNota.Text, format, CultureInfo.InvariantCulture);

            Xpinn.Aportes.Entities.Aporte vaportes = new Xpinn.Aportes.Entities.Aporte();
            vaportes = AporteServicio.ConsultarCierreAportes((Usuario)Session["usuario"]);
            estado = vaportes.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vaportes.fecha_cierre.ToString());

            if (estado == "D" && Fechatransaccion <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO A,'APORTES'");
                return;
            }

            else
            {
                result = 1;
            }
        }


        if (long.Parse(Session["tipoproducto"].ToString()) == 2)
        {
            //consultar cierre historico Cartera
            String estado = "";
            DateTime fechacierrehistorico;
            String format = gFormatoFecha;
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaNota.Text, format, CultureInfo.InvariantCulture);

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
            DateTime Fechatransaccion = DateTime.ParseExact(txtFechaNota.Text, format, CultureInfo.InvariantCulture);

            Xpinn.Servicios.Entities.CierreHistorico vservicio = new Xpinn.Servicios.Entities.CierreHistorico();
            vservicio = ServiceServicio.ConsultarCierreServicios((Usuario)Session["usuario"]);
            estado = vservicio.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vservicio.fecha_cierre.ToString());

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

        if (long.Parse(Session["tipoproducto"].ToString()) == 10)
        {
            result = 1;
        }


        if (result == 1)
        {


            if (!string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                decimal deudaTotal = 0;
                if (tipoProducto == TipoDeProducto.AhorrosVista)
                {
                    if (!ValidarMonto(gvAhorroVista, valor, 8, ref result, tipoProducto))
                        return;
                }
                else if (tipoProducto == TipoDeProducto.Servicios)
                {
                    if (!ValidarMonto(gvServicios, valor, 13, ref result, tipoProducto))
                        return;
                }
                else if (tipoProducto == TipoDeProducto.CDATS)
                {
                    if (!ValidarMonto(gvCdat, valor, 10, ref result, tipoProducto))
                        return;
                }
                else if (tipoProducto == TipoDeProducto.Afiliacion)
                {
                    if (!ValidarMonto(gvDatosAfiliacion, valor, 3, ref result, tipoProducto))
                        return;
                }
                else if (tipoProducto == TipoDeProducto.AhorroProgramado)
                {
                    if (!ValidarMonto(gvProgramado, valor, 11, ref result, tipoProducto))
                        return;
                }

                if (long.Parse(ViewState["tipoproducto"].ToString()) == 2)
                {
                    foreach (GridViewRow fila in gvConsultaDatos.Rows)
                    {
                        // codRadicado = Int64.Parse(fila.Cells[2].Text);
                        codRadicado = (fila.Cells[2].Text);
                        deudaTotal = decimal.Parse(fila.Cells[12].Text);
                        if (codRadicado == numProd)
                        {
                            result = 1;
                            if (valor > deudaTotal)
                            {
                                if (rblTipoNota.SelectedIndex != 0)
                                {
                                    VerError("En el crédito " + codRadicado + " el valor a pagar [" + valor + "] supera el valor total adeudado [" + deudaTotal + "]");
                                    return;
                                }
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
                        Lblerror.Text = "";


                        if (txtNumProducto.Text != "" && long.Parse(Session["tipoproducto"].ToString()) == 2)
                        {
                            List<ConsultaAvance> ListaDetalleAvance = new List<ConsultaAvance>();
                            ListaDetalleAvance = DetalleProducto.ListarAvances(long.Parse(txtNumProducto.Text), (Usuario)Session["Usuario"]);
                            if (ListaDetalleAvance.Count > 0)
                            {
                                gvAvances.DataSource = ListaDetalleAvance;
                                gvAvances.DataBind();
                                //Consulta Parametro General para que despliegue POP-UP De avances 
                                MpeDetalleAvances.Show();
                            }
                        }

                        LlenarTablaTran();
                        txtNumProducto.Text = "";
                        txtValTransac.Text = "";
                    }
                    else
                    {
                        VerError("El Valor de Transacción debe ser mayor a cero");
                        return;
                    }
                }
                else
                {
                    VerError("El Radicado que ha digitado no coincide con el que aparece en la Consulta de Datos, por favor verificar.");
                    return;
                }
            }
            else
            {
                VerError("Se debe Consultar Primero al Cliente");
                return;
            }

            PersonaService personaService = new PersonaService();
            Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
            persona.identificacion = txtIdentificacion.Text.Trim(); ;
            persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
            persona = personaService.ConsultarPersona(persona, Usuario);

            Actualizar(persona);
            txtValTransac.Enabled = true;
        }
    }

    protected void chkAvance_CheckedChanged(object sender, EventArgs e)
    {

        CheckBoxGrid chkAvance = (CheckBoxGrid)sender;
        int nItem = Convert.ToInt32(chkAvance.CommandArgument);

        TxtTotalAvances.Text = "0";
        TxtTotalCap.Text = "0";
        TxtTotalInt.Text = "0";
        Session["Avances"] = null;
        foreach (GridViewRow rFila in gvAvances.Rows)
        {

            CheckBoxGrid chkAvanceeRow = (CheckBoxGrid)rFila.FindControl("chkAvance");
            if (chkAvanceeRow.Checked)
            {


                TxtTotalAvances.Text = Convert.ToString(long.Parse(TxtTotalAvances.Text) + long.Parse(rFila.Cells[8].Text));
                TxtTotalCap.Text = Convert.ToString(long.Parse(TxtTotalCap.Text) + long.Parse(rFila.Cells[6].Text));
                TxtTotalInt.Text = Convert.ToString(long.Parse(TxtTotalInt.Text) + long.Parse(rFila.Cells[7].Text));

            }

        }



    }
    private void deshabilitar()
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarGuardar(false);
    }

    protected Boolean ValidarDatos()
    {
        this.Lblerror.Text = "";

        DateTime fechadesembolso = new DateTime();
        if (Session["fecha"] != null)
            fechadesembolso = Convert.ToDateTime(Session["fecha"]);
        DateTime fechaactual = DateTime.Now;
        DateTime Fechatransaccion = Convert.ToDateTime(txtFechaReal.Text);
        if (Fechatransaccion > fechaactual)
        {
            String Error = "La fecha no puede ser superior a la fecha actual";
            this.Lblerror.Text = Error;
            return false;
        }
        else
        {
            if (fechadesembolso != null)
            {
                if (Fechatransaccion.AddDays(1) < fechadesembolso)
                {
                    String Error = "No se puede aplicar el pago por que es inferior a la fecha de Transacción";
                    this.Lblerror.Text = Error;
                    return false;
                }
            }

            ValTotalTran = txtValorTran.Text == "" ? 0 : decimal.Parse(txtValorTran.Text); // Valor Total de Tabla de Transacciones
            if (ValTotalTran == 0)
            {
                this.Lblerror.Text = "Debe especificar los valores a pagar";
                return false;
            }
        }
        return true;
    }


    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        this.Lblerror.Text = "";
        if (ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea registrar las notas de los productos generados?");
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");
        // Validar que exista la parametrización contable por procesos
        if (ddlTipoProducto.SelectedValue != "10")
        {
            if (ValidarProcesoContable(Convert.ToDateTime(txtFechaNota.Text), 6) == false)
            {
                VerError("No se encontró parametrización contable por procesos para el tipo de operación 6 =  Notas Débito/Crédito");
                return;
            }
        }
        if (ddlTipoProducto.SelectedValue == "10")
            {
                if (ValidarProcesoContable(Convert.ToDateTime(txtFechaNota.Text), 148) == false)
                {
                    VerError("No se encontró parametrización contable por procesos para el tipo de operación 148 = Pago de Obligaciones Bancarias");
                    return;
                }
            }
            // Determinar código de proceso contable para generar el comprobante
            Int64? rpta = 0;
            if (!panelProceso.Visible && panelGeneral.Visible)
            {
                if (ddlTipoProducto.SelectedValue != "10")
                {
                    rpta = ctlproceso.Inicializar(6, Convert.ToDateTime(txtFechaNota.Text), (Usuario)Session["Usuario"]);

                }
                else
                {
                    rpta = ctlproceso.Inicializar(148, Convert.ToDateTime(txtFechaNota.Text), (Usuario)Session["Usuario"]);

                }
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
                    // Crear la tarea de ejecución del proceso                
                    if (AplicarDatos())
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                    else
                    {
                        VerError("Se presentó error");
                        this.Lblerror.Visible = true;
                        ;
                    }

                }
            
        }
    }

    protected bool AplicarDatos()
    {
        try
        {
            Xpinn.Caja.Entities.TransaccionCaja pOperacion = new Xpinn.Caja.Entities.TransaccionCaja();
            pOperacion.cod_persona = long.Parse(ViewState["codpersona"].ToString());
            pOperacion.cod_caja = 0;
            pOperacion.cod_cajero = 0;
            pOperacion.cod_oficina = Usuario.cod_oficina;
            pOperacion.fecha_movimiento = Convert.ToDateTime(txtFechaReal.Text);
            pOperacion.fecha_aplica = Convert.ToDateTime(txtFechaNota.Text);
            pOperacion.fecha_cierre = Convert.ToDateTime(txtFechaNota.Text);
            pOperacion.observacion = txtObservacion.Text;
            pOperacion.cod_proceso = null;
            if (txtValorTran.Text != "")
                pOperacion.valor_pago = decimal.Parse(txtValorTran.Text);
            else
                pOperacion.valor_pago = 0;
            // TIPO OPERACION
            if (ddlTipoProducto.SelectedValue == "10")
            {
                pOperacion.tipo_ope = 148;
            }
            else
            {
                pOperacion.tipo_ope = 6;
            }
            string pError = "";
            VerError("");

            Devolucion pVar = new Devolucion();
            pVar.num_devolucion = 0;
            pVar.concepto = "Generacion de Notas A productos";
            pVar.cod_persona = long.Parse(ViewState["codpersona"].ToString());
            pVar.identificacion = txtIdentificacion.Text;

            pVar.fecha_devolucion = Convert.ToDateTime(txtFechaNota.Text);
            pVar.valor = Convert.ToDecimal(txtValorTran.Text);
            pVar.saldo = Convert.ToDecimal(txtValorTran.Text);
            pVar.origen = txtNombreCliente.Text;
            pVar.estado = "1"; //PENDIENTE
            pVar.fecha_descuento = Convert.ToDateTime(txtFechaNota.Text);

            //VALORES NULOS
            pVar.num_recaudo = 0;
            pVar.iddetalle = 0;


            pOperacion = NotasProdService.AplicarNotasAProductos(pVar, chkGeneraDev.Checked, pOperacion, gvTransacciones, Usuario, chkPendiente.Checked, ref pError);
            if (pError.Trim() != "")
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                VerError(pError);
                this.Lblerror.Text = pError;
                return false;
            }

            // Se genera el comprobante
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFechaReal.Text);
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = long.Parse(ViewState["codpersona"].ToString());
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = Usuario.cod_oficina;
            if (ddlTipoProducto.SelectedValue == "10")
            {
                ctlproceso.CargarVariables(pOperacion.cod_ope, 148, long.Parse(ViewState["codpersona"].ToString()), Usuario);
            }
            else
            {
                ctlproceso.CargarVariables(pOperacion.cod_ope, 6, long.Parse(ViewState["codpersona"].ToString()), Usuario);

            }
            /*
            DateTime fecha = Convert.ToDateTime(txtFechaReal.Text);
            Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = pOperacion.cod_ope;
            Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 6;
            Session[ComprobanteServicio.CodigoPrograma + ".fecha_ope"] = Convert.ToDateTime(txtFechaReal.Text);
            Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = long.Parse(ViewState["codpersona"].ToString());
            Session[ComprobanteServicio.CodigoPrograma + ".cod_ofi_ope"] = Usuario.cod_oficina;
            Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
            */
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
            return false;
        }
        /*
        catch (Exception ex)
        {
            BOexcepcion.Throw(NotasProdService.GetType().Name + "A", "btnGuardar_Click", ex);
            return false;
        }
        */
        return true;
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Response.Redirect("nuevo.aspx", false);
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Lblerror.Text = "";
        if (txtIdentificacion.Text != null && !string.Equals(txtIdentificacion.Text, ""))
        {
            Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
            Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();

            persona.identificacion = txtIdentificacion.Text.Trim();
            persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
            VerError("");
            persona = personaService.ConsultarPersona(persona, Usuario);

            if (persona.mensajer_error == "")
            {
                ViewState["codpersona"] = persona.cod_persona;
                txtNombreCliente.Text = persona.nom_persona;

                // aqui se coloca los datos de la persona, Nro Radicacion, Nombre, Valor CUota, saldo capital
                Actualizar(persona);
                ddlTipoTipoProducto_SelectedIndexChanged(null, null);
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
            divDatos.Visible = false;

            TipoDeProducto tipoProducto = ddlTipoProducto.SelectedValue.ToEnum<TipoDeProducto>();

            if (tipoProducto == TipoDeProducto.Aporte || tipoProducto == TipoDeProducto.Credito) // si el tipo de Producto es diferente de Afiliacion
            {
                pEntidad.tipo_linea = Convert.ToInt64(ViewState["tipoproducto"]);
                lstConsulta = personaService.ListarDatosCreditoPersona(pEntidad, Usuario);

                if (lstConsulta.Count > 0)
                {
                    divDatos.Visible = true;
                    gvConsultaDatos.DataSource = lstConsulta;
                    divDatos.Visible = true;
                    gvConsultaDatos.Visible = true;
                    gvConsultaDatos.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.AhorrosVista)
            {
                Xpinn.Ahorros.Services.ReporteMovimientoServices ReporteMovService = new Xpinn.Ahorros.Services.ReporteMovimientoServices();
                List<Xpinn.Ahorros.Entities.AhorroVista> lstAhorros = new List<Xpinn.Ahorros.Entities.AhorroVista>();
                String filtro = " WHERE A.ESTADO IN (0,1) AND A.COD_PERSONA = " + pEntidad.cod_persona + " ";
                DateTime pFechaApert;
                pFechaApert = DateTime.MinValue;

                lstAhorros = ReporteMovService.ListarAhorroVista(filtro, pFechaApert, Usuario);
                if (lstAhorros.Count > 0)
                {
                    gvAhorroVista.Visible = true;
                    gvAhorroVista.DataSource = lstAhorros;
                    gvAhorroVista.DataBind();
                    divDatos.Visible = true;
                }
            }
            else if (tipoProducto == TipoDeProducto.Servicios)
            {
                Xpinn.Servicios.Services.AprobacionServiciosServices AproServicios = new Xpinn.Servicios.Services.AprobacionServiciosServices();
                List<Xpinn.Servicios.Entities.Servicio> lstServicios = new List<Xpinn.Servicios.Entities.Servicio>();
                String filtro = " and S.COD_PERSONA = " + pEntidad.cod_persona + " AND S.ESTADO = 'C' ";

                string pOrden = "fecha_solicitud desc";
                lstServicios = AproServicios.ListarServicios(filtro, pOrden, DateTime.MinValue, Usuario);

                if (lstServicios.Count > 0)
                {
                    divDatos.Visible = true;
                    gvServicios.Visible = true;
                    gvServicios.DataSource = lstServicios;
                    gvServicios.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.CDATS)
            {
                Xpinn.CDATS.Services.AperturaCDATService AperturaService = new Xpinn.CDATS.Services.AperturaCDATService();
                List<Xpinn.CDATS.Entities.Cdat> lstCdat = new List<Xpinn.CDATS.Entities.Cdat>();
                String filtro = " AND C.ESTADO = 1 and T.COD_PERSONA = " + pEntidad.cod_persona + " AND T.PRINCIPAL = 1 ";
                DateTime FechaApe;
                FechaApe = DateTime.MinValue;
                lstCdat = AperturaService.ListarCdats(filtro, FechaApe, Usuario);

                if (lstCdat.Count > 0)
                {
                    divDatos.Visible = true;
                    gvCdat.Visible = true;
                    gvCdat.DataSource = lstCdat;
                    gvCdat.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.Afiliacion) // si en caso sea Afiliac
            {
                lstConsulta = personaService.ListarPersonasAfiliacion(pEntidad, Usuario);
                if (lstConsulta.Count > 0)
                {
                    divDatos.Visible = true;
                    gvDatosAfiliacion.Visible = true;
                    gvDatosAfiliacion.DataSource = lstConsulta;
                    gvDatosAfiliacion.DataBind();
                }
            }
            else if (tipoProducto == TipoDeProducto.AhorroProgramado)
            {
                Xpinn.Programado.Services.CuentasProgramadoServices CuentasPrograServicios = new Xpinn.Programado.Services.CuentasProgramadoServices();
                List<Xpinn.Programado.Entities.CuentasProgramado> lstPrograma = new List<Xpinn.Programado.Entities.CuentasProgramado>();
                String filtro = " WHERE A.ESTADO = 1 AND A.COD_PERSONA = " + pEntidad.cod_persona + " ";
                DateTime pFecha = DateTime.MinValue;

                lstPrograma = CuentasPrograServicios.ListarAhorrosProgramado(filtro, pFecha, Usuario);
                if (lstPrograma.Count > 0)
                {
                    gvProgramado.DataSource = lstPrograma;
                    gvProgramado.DataBind();
                    divDatos.Visible = true;
                    gvProgramado.Visible = true;
                }
            }

            else if (tipoProducto == TipoDeProducto.ObligacionesFinancieras)
            {
                Xpinn.Obligaciones.Services.ObligacionCreditoService ObligacionesService = new Xpinn.Obligaciones.Services.ObligacionCreditoService();
                List<Xpinn.Obligaciones.Entities.ObligacionCredito> lstPrograma = new List<Xpinn.Obligaciones.Entities.ObligacionCredito>();
                String filtro = " and  o.ESTADOOBLIGACION ='D' AND bancos.COD_PERSONA = " + pEntidad.cod_persona + " ";
                DateTime pFecha = DateTime.MinValue;

                lstPrograma = ObligacionesService.ListarObligaciones(filtro, Usuario);
                if (lstPrograma.Count > 0)
                {
                    GvObligaciones.DataSource = lstPrograma;
                    GvObligaciones.DataBind();
                    divDatos.Visible = true;
                    GvObligaciones.Visible = true;
                }
            }
            
            Session.Add(NotasProdService.GetType().Name + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(NotasProdService.GetType().Name + "L", "Actualizar", ex);
        }
    }


    protected void ddlTipoTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Cargando tipos de notas
        rblTipoNota.Items.Clear();
        rblTipoNota.Items.Add(new ListItem("Débito", "D"));
        rblTipoNota.Items.Add(new ListItem("Crédito", "C"));
        rblTipoNota.SelectedIndex = 0;

        // Determinando el tipo de producto
        Xpinn.Caja.Services.TipoOperacionService tipoOpeService = new Xpinn.Caja.Services.TipoOperacionService();
        Xpinn.Caja.Entities.TipoProducto tipProd = new Xpinn.Caja.Entities.TipoProducto();
        Xpinn.Caja.Services.PersonaService personaService = new Xpinn.Caja.Services.PersonaService();
        Xpinn.Caja.Entities.Persona persona = new Xpinn.Caja.Entities.Persona();
        tipProd.cod_tipo_producto = Convert.ToInt64(ddlTipoProducto.SelectedValue);
        tipProd = tipoOpeService.ConsultarTipoProducto(tipProd, Usuario);

        // Determinando atributos según tipo de producto
        lblAtributo.Visible = false;
        ddlAtributo.Visible = false;
        if (tipProd.cod_tipo_producto == 1)
        {
            persona.linea_credito = "1";
        }
        else if (tipProd.cod_tipo_producto == 2)
        {
            persona.linea_credito = "2";
            lblAtributo.Visible = true;
            ddlAtributo.Visible = true;
            rblTipoNota.Items.Add(new ListItem("Pago por Valor", "P"));
            rblTipoNota.Items.Add(new ListItem("Pago Total", "T"));
        }
        else if (tipProd.cod_tipo_producto == 3)
            persona.linea_credito = "3";
        else if (tipProd.cod_tipo_producto == 4)
            persona.linea_credito = "4";
        else if (tipProd.cod_tipo_producto == 5)
            persona.linea_credito = "5";
        else if (tipProd.cod_tipo_producto == 9)
            persona.linea_credito = "9";

        ViewState["tipoproducto"] = tipProd.cod_tipo_producto;

        // Si el tipo de producto es otros entonces no ingresa número de producto.
        if (tipProd.cod_tipo_producto == 7)
            lblMsgNroProducto.Text = "El número de producto no es obligatorio colocarlo en este tipo de transacción, colocar cero en el campo de número de producto ";
        else
            lblMsgNroProducto.Text = "";

        // Determinar datos de la persona
        persona.identificacion = txtIdentificacion.Text.Trim();
        persona.tipo_identificacion = long.Parse(ddlTipoIdentificacion.SelectedValue);
        VerError("");
        persona = personaService.ConsultarPersona(persona, Usuario);
        Actualizar(persona);
    }


    protected void gvTransacciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            DataTable table = new DataTable();
            table = (DataTable)Session["tablaSesion"];//se pilla las transacciones

            txtValorTran.Text = Convert.ToString(decimal.Parse(txtValorTran.Text) - decimal.Parse(table.Rows[e.RowIndex][3].ToString()));

            table.Rows[e.RowIndex].Delete();
            gvTransacciones.DataSource = table;
            gvTransacciones.DataBind();
            Session["tablaSesion"] = table;

            // Actualizar los totales
            ActualizarTotal();
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }


    protected void ActualizarTotal()
    {
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
    }


    protected void btnCloseAct2_Click(object sender, EventArgs e)
    {
        MpeDetallePagoAportes.Hide();
    }
    protected void btnCloseAct1_Click(object sender, EventArgs e)
    {
        MpeDetalleAvances.Hide();
    }


    protected void gvConsultaDatos_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        txtNumProducto.Text = evt.CommandName;
        Xpinn.Caja.Entities.Persona lineacreditos = new Xpinn.Caja.Entities.Persona();
        Xpinn.Caja.Services.PersonaService PersonaServicio = new Xpinn.Caja.Services.PersonaService();
        lineacreditos = PersonaServicio.ConsultarDatosCreditoPersona(Convert.ToString(txtNumProducto.Text), Usuario);
        fecha = Convert.ToDateTime(lineacreditos.fecha_desembolso);
        Session["fecha"] = fecha;
    }


    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        ctlBusquedaPersonas.Motrar(true, "", "txtIdentificacion", "ddlTipoIdentificacion", "txtNombreCliente");
    }

    protected void rblTipoNota_SelectedIndexChanged(object sender, EventArgs e)
    {
        string general95 = ventanillaServicio.ParametroGeneral(95, (Usuario)Session["Usuario"]);
        if (rblTipoNota.SelectedIndex == 0 || rblTipoNota.SelectedItem == null || general95 == "1") // DEBITO
        {
            chkGeneraDev.Enabled = true;
        }
        else
        {
            chkGeneraDev.Checked = false;
            chkGeneraDev.Enabled = false;
        }
        CalcularValordelCredito();
    }


    protected void chkAvances_CheckedChanged(object sender, EventArgs e)
    {

        CheckBoxGrid chkAvance = (CheckBoxGrid)sender;
        int nItem = Convert.ToInt32(chkAvance.CommandArgument);

        TxtTotalAvances.Text = "0";
        TxtTotalCap.Text = "0";
        TxtTotalInt.Text = "0";
        Session["Avances"] = null;
        foreach (GridViewRow rFila in gvAvances.Rows)
        {

            CheckBoxGrid chkAvanceeRow = (CheckBoxGrid)rFila.FindControl("chkAvance");
            chkAvanceeRow.Checked = true;
            if (chkAvanceeRow.Checked)
            {


                TxtTotalAvances.Text = Convert.ToString(long.Parse(TxtTotalAvances.Text) + long.Parse(rFila.Cells[8].Text));
                TxtTotalCap.Text = Convert.ToString(long.Parse(TxtTotalCap.Text) + long.Parse(rFila.Cells[6].Text));
                TxtTotalInt.Text = Convert.ToString(long.Parse(TxtTotalInt.Text) + long.Parse(rFila.Cells[7].Text));

            }

        }



    }



    private void CalcularValordelCredito()
    {
        Xpinn.FabricaCreditos.Services.CreditoService CreditoServicio = new Xpinn.FabricaCreditos.Services.CreditoService();

        // Determinar nùmero de crèdito
        Int64 numero_radicacion = 0;
        if (txtNumProducto.Text != "")
            numero_radicacion = Convert.ToInt64(txtNumProducto.Text);
        DateTime fecha_pago = System.DateTime.Now;
        txtValTransac.Enabled = true;
        txtValTransac.Text = "";
        // Segùn la forma de pago calcular el valor        
        if (rblTipoNota.SelectedValue == "T")
        {
            // Cuando es pago total se calcula el valor total adeudado y se inactiva casilla de valor del pago.
            txtValTransac.Enabled = false;
            if (numero_radicacion != 0)
            {
                try
                {
                    decimal valor_apagar = CreditoServicio.AmortizarCredito(numero_radicacion, 2, fecha_pago, Usuario);
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


    #region ACCIONES DE LAS GRIDVIEWS

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

    //OBLIGACIONES FINANCIERAS 
    protected void GvObligaciones_RowEditing(object sender, GridViewEditEventArgs e)
    {
        VerError("");
        string id = GvObligaciones.DataKeys[e.NewEditIndex].Values[0].ToString();
        txtNumProducto.Text = id;
        e.NewEditIndex = -1;
        ddlMonedas.Visible = true;
        lblAtributo.Visible = true;
        ddlAtributo.Visible = true;
        chkGeneraDev.Visible = false;
        chkPendiente.Visible = false;
    }

    //CDATS
    protected void gvCdat_RowEditing(object sender, GridViewEditEventArgs e)
    {

        VerError("");
        string id = gvCdat.DataKeys[e.NewEditIndex].Values[0].ToString();
        txtNumProducto.Text = id;
        String saldoTotal = gvCdat.Rows[e.NewEditIndex].Cells[10].Text;
        txtValTransac.Text = Convert.ToString(saldoTotal);
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

    #endregion

    #region ACCIONES GENERAR COMPROBANTE

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

    #endregion  
}
