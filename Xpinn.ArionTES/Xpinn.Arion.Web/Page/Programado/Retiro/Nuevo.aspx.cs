using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.UI.HtmlControls;
using Xpinn.Servicios.Services;
using Xpinn.Servicios.Entities;
using Xpinn.Programado.Services;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Programado.Entities;
using Xpinn.FabricaCreditos.Entities;
using Microsoft.Reporting.WebForms;

public partial class Nuevo : GlobalWeb
{

    SolicitudServiciosServices SolicServicios = new SolicitudServiciosServices();
    CierreCuentasServices cierreCuentas = new CierreCuentasServices();
    CuentasProgramadoServices cuentasProgramado = new CuentasProgramadoServices();
    PoblarListas poblar = new PoblarListas();
    cierreCuentaDetalle entidad;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(cuentasProgramado.CodigoProgramaRetiro, "L");

            Site toolBar = (Site)this.Master;
            //toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            //ctlMensaje.eventoClick += btnContinuarMen_Click;
            //txtFecha.eventoCambiar += txtFecha_textchange;
            //txtDecimales.eventoCambiar += txtFecha_textchange;
            toolBar.eventoGuardar += btnGuardar_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentasProgramado.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                mvAplicar.ActiveViewIndex = 0;
                ddLinea.Enabled = false;
                ddlPeriodicidad.Enabled = false;
                ddlTipoIdentificacion.Enabled = false;
                //ddlTipoTasa.Enabled = false;
                txtFechaApertura.Enabled = false;
                txtFechaApertura.Enabled = false;
                ddlOficina.Enabled = false;
                txtFechaProximoPago.Enabled = false;
                txtFecha.Text = DateTime.Now.ToShortDateString();


                if (Session[cuentasProgramado.CodigoProgramaRetiro + ".id"] != null)
                {
                    idObjeto = Session[cuentasProgramado.CodigoProgramaRetiro + ".id"].ToString();
                    Session.Remove(cuentasProgramado.CodigoProgramaRetiro + ".id");
                    cargarddl();
                    cargarCampos();
                    CargarCuentas();
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.GetType().Name + "L", "Page_Load", ex);
        }

    }
    // carga ddl
    protected void cargarddl()
    {

        ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
        ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
        ddlTipo_cuenta.DataBind();

        DropDownFormaDesembolso.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        DropDownFormaDesembolso.Items.Insert(1, new ListItem("Efectivo", "1"));
        DropDownFormaDesembolso.Items.Insert(2, new ListItem("Cheque", "2"));
        DropDownFormaDesembolso.Items.Insert(3, new ListItem("Transferencia", "3"));
        DropDownFormaDesembolso.DataBind();
        DropDownFormaDesembolso.SelectedIndex = 1;
        ActivarDesembolso();

        Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
        Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
        DropDownEntidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
        DropDownEntidad.DataTextField = "nombrebanco";
        DropDownEntidad.DataValueField = "cod_banco";
        DropDownEntidad.DataBind();

        ddlEntidadOrigen.DataSource = bancoService.ListarBancosEntidad(banco, (Usuario)Session["usuario"]);
        ddlEntidadOrigen.DataTextField = "nombrebanco";
        ddlEntidadOrigen.DataValueField = "cod_banco";
        ddlEntidadOrigen.DataBind();

        poblar.PoblarListaDesplegable("periodicidad", ddlPeriodicidad, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("TIPOIDENTIFICACION", ddlTipoIdentificacion, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("lineaprogramado", ddLinea, (Usuario)Session["usuario"]);
        Xpinn.Asesores.Data.OficinaData listaOficina = new Xpinn.Asesores.Data.OficinaData();
        Xpinn.Asesores.Entities.Oficina oficina = new Xpinn.Asesores.Entities.Oficina();
        oficina.Estado = 1;
        var lista = listaOficina.ListarOficina(oficina, (Usuario)Session["usuario"]);

        if (lista != null)
        {
            lista.Insert(0, new Xpinn.Asesores.Entities.Oficina { NombreOficina = "Seleccione un Item", IdOficina = 0 });
            ddlOficina.DataSource = lista;
            ddlOficina.DataTextField = "NombreOficina";
            ddlOficina.DataValueField = "IdOficina";
            ddlOficina.DataBind();
        }

    }

    // calcula los valores de los campos y los carga 
    protected void cargarCampos()
    {
        cierreCuentaDetalle objetoCuenta = cuentasProgramado.cierreCuentaDService(idObjeto, (Usuario)Session["usuario"]);

        Xpinn.Programado.Entities.cierreCuentaDetalle entidadF = new Xpinn.Programado.Entities.cierreCuentaDetalle();
        objetoCuenta.fecha_Liquida = Convert.ToDateTime(txtFecha.Texto);
        objetoCuenta.NumeroProgramado = idObjeto.ToString();


        Xpinn.Programado.Data.CuentasProgramadoData DACierre = new Xpinn.Programado.Data.CuentasProgramadoData();
        objetoCuenta = cuentasProgramado.cerrarCuentasServices(objetoCuenta, (Usuario)Session["usuario"]);
        Session["cod_persona"] = objetoCuenta.Cod_persona;

        if (objetoCuenta.Identificacion != string.Empty)
            txtIdentificacion.Text = objetoCuenta.Identificacion.ToString();
        if (objetoCuenta.Nombre != string.Empty)
            txtNomPersona.Text = objetoCuenta.Nombre.ToString();
        if (objetoCuenta.Cuenta != string.Empty)
            txtCuenta.Text = objetoCuenta.Cuenta.ToString();
        if (objetoCuenta.Fecha_Apertura != null)
            txtFechaApertura.Texto = Convert.ToDateTime(objetoCuenta.Fecha_Apertura).ToShortDateString().ToString();
        if (objetoCuenta.Fecha_Proximo_Pago != null)
            txtFechaProximoPago.Texto = objetoCuenta.Fecha_Proximo_Pago.ToShortDateString().ToString();
        if (objetoCuenta.cuota != 0)
            txtCuota.Text = objetoCuenta.cuota.ToString();
        if (objetoCuenta.Plazo != 0)
            txtPlazo.Text = objetoCuenta.Plazo.ToString();
        if (objetoCuenta.Oficina != string.Empty)
            ddlOficina.SelectedItem.Text = objetoCuenta.Oficina.ToString();
        if (objetoCuenta.Periodicidad != string.Empty)
            ddlPeriodicidad.SelectedValue = objetoCuenta.Codigo_Perioricidad.ToString();
        if (objetoCuenta.TipoIdentificacion != 0)
            ddlTipoIdentificacion.SelectedValue = objetoCuenta.TipoIdentificacion.ToString();
        if (objetoCuenta.Linea != string.Empty)
            ddLinea.SelectedValue = objetoCuenta.Linea;
        if (objetoCuenta.saldo != 0)
            txtSaldoTotal.Text = objetoCuenta.saldo.ToString();

        txtPorcentaje.Text = objetoCuenta.porcentaje.ToString();

        //if (objetoCuenta.TipoTasa > -1)
        //    controltas.TipoTasa =(int) objetoCuenta.TipoTasa;
        Site toolBar = (Site)this.Master;
        // toolBar.eventoImprimir += btnImprimir_Click;
        entidad = objetoCuenta;

    }
    public cierreCuentaDetalle getEntidad()
    {

        cierreCuentaDetalle objetoCuenta = cuentasProgramado.cierreCuentaDService(idObjeto, (Usuario)Session["usuario"]);
        objetoCuenta.fecha_Liquida = Convert.ToDateTime(txtFecha.Texto);
        objetoCuenta.NumeroProgramado = idObjeto.ToString();

        Xpinn.Programado.Data.CuentasProgramadoData DACierre = new Xpinn.Programado.Data.CuentasProgramadoData();
        objetoCuenta = cuentasProgramado.cerrarCuentasServices(objetoCuenta, (Usuario)Session["usuario"]);
        return objetoCuenta;
    }

    // redirecciona ala pagina principal para cancelar acciones 
    public void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    // carga la entidad de operacion y la retorna llena 
    protected Xpinn.Tesoreria.Entities.Operacion cargarOperacion()
    {
        try
        {
            Xpinn.Tesoreria.Entities.Operacion operacion = new Xpinn.Tesoreria.Entities.Operacion();
            operacion.cod_ope = 0;
            operacion.tipo_ope = 64;
            operacion.cod_caja = 0;
            operacion.cod_cajero = 0;
            operacion.cod_ofi = Convert.ToInt64(ddlOficina.SelectedValue);
            operacion.observacion = "Grabacion de operacion- Cierre Ahorro Programado";
            operacion.cod_proceso = null;
            operacion.fecha_oper = DateTime.Now;
            operacion.fecha_calc = DateTime.Now;

            return operacion;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentasProgramado.GetType().Name + "L", "Page_PreInit", ex);
            return null;
        }
    }
    protected bool isValido()
    {
        var FechCieCuen = cuentasProgramado.getFechaPosCierreConServices((Usuario)Session["usuario"]);
        var fehcCierProgr = cuentasProgramado.getFechaposProgra((Usuario)Session["usuario"]);
        if (Convert.ToDateTime(txtFecha.Texto.Trim()) <= fehcCierProgr)
        {
            VerError("La fecha de retiro debe ser  posterior a la fecha del último cierre contable definitivo  ");
            return false;
        }
        if (Convert.ToDateTime(txtFecha.Texto.Trim()) <= FechCieCuen)
        {
            VerError(" La fecha de retiro debe ser posterior a la fecha del último cierre de ahorro programado definitivo");
            return false;
        }
        var porcentaje = Convert.ToDecimal(txtPorcentaje.Text);
        var valor = Convert.ToDecimal(txtSaldoTotal.Text);
        var valormaxretirar = (valor * porcentaje) / 100;
        var retiros = Convert.ToDecimal(txtDecimales.Text);
        if ((Int64)retiros > (Int64)valormaxretirar)
        {
            VerError("El valor del retiro no puede superar el porcentaje de retiro. " + " El porcentaje de la Linea es : " + txtPorcentaje.Text
                + "  Puede retirar hasta: " + valormaxretirar);
            return false;
        }
        if (txtDecimales.Text == "" || txtDecimales.Text == "0")
        {
            VerError("Digite un valor a retirar");
            return false;
        }
        if (txtDecimales.Text.Length > 0)
        {
            var saldo = Convert.ToDecimal(txtSaldoTotal.Text);
            var retiro = Convert.ToDecimal(txtDecimales.Text);
            if ((Int64)retiro > (Int64)saldo)
            {
                VerError("saldo a retirar no puede ser mayor a el saldo total de la cuenta");
                return false;
            }
        }
        if (txtDecimales.Text == "" || txtDecimales.Text == "0")
            VerError("Seleccione el tipo de Pago");

        return true;
    }

    // cierra la cuenta y le agraga los campos correspondientes 
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Xpinn.Tesoreria.Services.CuentasBancariasServices bancoService = new Xpinn.Tesoreria.Services.CuentasBancariasServices();
        Xpinn.Tesoreria.Entities.CuentasBancarias CuentaBanc = new Xpinn.Tesoreria.Entities.CuentasBancarias();
        Xpinn.FabricaCreditos.Entities.Giro pGiro = new Xpinn.FabricaCreditos.Entities.Giro();
       
        Int64 CodigoOperacion = 0;
        // si no hay saldo total o interes 
        if (isValido())
        {

            if (DropDownFormaDesembolso.SelectedIndex <= 0)
            {
                VerError("Seleccione el tipo de Pago");
                DropDownFormaDesembolso.Focus();
                return;
            }
            else
            {
                CuentaBanc = bancoService.ConsultarCuentasBancariasPorBanco(Convert.ToInt32(ddlEntidadOrigen.SelectedValue), ddlCuentaOrigen.SelectedItem.Text, (Usuario)Session["usuario"]);
                Int64 idCta = CuentaBanc.idctabancaria;
                //DATOS DE FORMA DE PAGO
                if (DropDownFormaDesembolso.SelectedItem.Text == "Transferencia")
                {
                    pGiro.forma_pago = Convert.ToInt32(DropDownFormaDesembolso.SelectedValue);
                    pGiro.idctabancaria = idCta;
                    pGiro.cod_banco = Convert.ToInt32(DropDownEntidad.SelectedValue);
                    pGiro.num_cuenta = txtnumcuenta.Text;
                    pGiro.tipo_cuenta = Convert.ToInt32(ddlTipo_cuenta.SelectedValue);
                }
                else if (DropDownFormaDesembolso.SelectedItem.Text == "Cheque")
                {
                    pGiro.forma_pago = Convert.ToInt32(DropDownFormaDesembolso.SelectedValue);
                    pGiro.idctabancaria = idCta;
                    pGiro.cod_banco = 0;         //NULO
                    pGiro.num_cuenta = null;    //NULO
                    pGiro.tipo_cuenta = -1;    //NULO
                }
                else
                {
                    pGiro.forma_pago =Convert.ToInt32(DropDownFormaDesembolso.SelectedValue);

                    pGiro.idctabancaria = 0;
                    pGiro.cod_banco = 0;
                    pGiro.num_cuenta = null;
                    pGiro.tipo_cuenta = -1;
                }

                pGiro.tipo_acto = 1;
                pGiro.fec_reg = DateTime.Now;
                pGiro.fec_giro = txtFecha.ToDateTime;
               // pGiro.numero_radicacion = Convert.ToInt64(txtCodigo.Text);// NO ENVIO EL NUMERO DE RADICACION SINO EL NUMERO DE DEVOLUCION
                pGiro.usu_apli = null;
                pGiro.estadogi = 0;
                pGiro.usu_apro = null;
                pGiro.fec_apro = DateTime.MinValue;
                pGiro.estadogi = 0;
                pGiro.tipo_acto = 5;


                pGiro.usu_gen = txtNomPersona.Text;
                Decimal valor = Convert.ToDecimal(txtDecimales.Text);
                pGiro.valor = Convert.ToInt64(valor);
                // objeto implicito para cargar los datos de la cuenta 
                cierreCuentaDetalle objeCuenta = new cierreCuentaDetalle { Fecha_Proximo_Pago =Convert.ToDateTime(txtFecha.Text),NumeroProgramado = txtCuenta.Text, Cod_persona =Convert.ToInt64(Session["cod_persona"]) };
                pGiro.cod_persona = Convert.ToInt64(Session["cod_persona"]);
                DateTime fechaoperacion = Convert.ToDateTime(txtFecha.Text);


                String estado = "";
                DateTime fechacierrehistorico;
                DateTime fecharetiro= Convert.ToDateTime(txtFecha.Texto);
                Xpinn.Programado.Entities.CuentasProgramado vAhorroProgramado = new Xpinn.Programado.Entities.CuentasProgramado();
                vAhorroProgramado = cuentasProgramado.ConsultarCierreAhorroProgramado((Usuario)Session["usuario"]);
                estado = vAhorroProgramado.estadocierre;
                fechacierrehistorico = Convert.ToDateTime(vAhorroProgramado.fecha_cierre.ToString());

                if (estado == "D" && fecharetiro <= fechacierrehistorico)
                {
                    VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO L,'AH. PROGRAMADO'");
                }

                else
                {
                    cuentasProgramado.grabarDatos((Usuario)Session["usuario"], objeCuenta, ref CodigoOperacion, cargarOperacion(), pGiro, Convert.ToDecimal(txtDecimales.Text), fechaoperacion);

                    //Grabar Comprobante 
                    if (CodigoOperacion != 0)
                    {
                        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = CodigoOperacion;
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 64;
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = getEntidad().Cod_persona; //"<Colocar Aquí el código de la persona del servicio>"
                        Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                        Session[cuentasProgramado.CodigoProgramaRetiro + ".id"] = idObjeto;
                    }
                }
            }
        
        }
    }

    protected void DropDownFormaDesembolso_TextChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();
    }

    protected void ActivarDesembolso()
    {
        if (DropDownFormaDesembolso.SelectedItem.Text == "Transferencia")
        {
            lblEntidad.Visible = true;
            lblNumCuenta.Visible = true;
            lblTipoCuenta.Visible = true;
            txtnumcuenta.Visible = true;
            DropDownEntidad.Visible = true;
            ddlTipo_cuenta.Visible = true;
            ddlEntidadOrigen.Visible = true;
            ddlCuentaOrigen.Visible = true;
            lblEntidadOrigen.Visible = true;
            lblNumCuentaOrigen.Visible = true;
        }
        else
        {
            lblEntidad.Visible = false;
            lblNumCuenta.Visible = false;
            lblTipoCuenta.Visible = false;
            txtnumcuenta.Visible = false;
            DropDownEntidad.Visible = false;
            ddlTipo_cuenta.Visible = false;
            if (DropDownFormaDesembolso.SelectedItem.Text == "Efectivo")
            {
                ddlEntidadOrigen.Visible = false;
                ddlCuentaOrigen.Visible = false;
                lblEntidadOrigen.Visible = false;
                lblNumCuentaOrigen.Visible = false;
            }
            else
            {
                ddlEntidadOrigen.Visible = true;
                ddlCuentaOrigen.Visible = true;
                lblEntidadOrigen.Visible = true;
                lblNumCuentaOrigen.Visible = true;
            }
        }
        if (DropDownFormaDesembolso.SelectedItem.Text == "Transferencia")
        {
            CargarCuentas();
        }
    }

    protected void DropDownFormaDesembolso_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();
        if (DropDownFormaDesembolso.SelectedItem.Text == "Transferencia")
        {
            ActividadesServices ActividadServicio = new ActividadesServices();
            List<CuentasBancarias> LstCuentasBanc = new List<CuentasBancarias>();
            if (LstCuentasBanc.Count > 0 && LstCuentasBanc.Count == 1)
            {
                Session["LISTA"] = LstCuentasBanc;                
            }
        }
    }


    protected void ddlEntidadOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCuentas();
    }

    /// <summary>
    /// Método para cargar las centas bancarias según el banco seleccionado
    /// </summary>
    protected void CargarCuentas()
    {
        Int64 codbanco = 0;
        try
        {
            codbanco = Convert.ToInt64(ddlEntidadOrigen.SelectedValue);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentasProgramado.GetType().Name + "L", "CargarCuentas", ex);
        }
        if (codbanco != 0)
        {
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Usuario usuario = (Usuario)Session["usuario"];
            ddlCuentaOrigen.DataSource = bancoService.ListarCuentaBancos(codbanco, usuario);
            ddlCuentaOrigen.DataTextField = "num_cuenta";
            ddlCuentaOrigen.DataValueField = "idctabancaria";
            ddlCuentaOrigen.DataBind();
        }
    }

    protected void ddlCuentaOrigen_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarImprimir(true);
        mvAplicar.ActiveViewIndex = 0;
    }

}
