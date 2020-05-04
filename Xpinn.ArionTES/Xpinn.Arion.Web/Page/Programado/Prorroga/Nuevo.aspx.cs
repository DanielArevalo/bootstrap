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
using Xpinn.Programado.Entities;
using Microsoft.Reporting.WebForms;
using Xpinn.Tesoreria.Services;
using Xpinn.Tesoreria.Entities;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;

public partial class Nuevo : GlobalWeb
{

    SolicitudServiciosServices SolicServicios = new SolicitudServiciosServices();
    CierreCuentasServices cierreCuentas = new CierreCuentasServices();
    CuentasProgramadoServices cuentasProgramado = new CuentasProgramadoServices();
    CuentasProgramadoServices CuentasPrograServicios = new CuentasProgramadoServices();
    Xpinn.CDATS.Services.AperturaCDATService AperturaService = new Xpinn.CDATS.Services.AperturaCDATService();
    Boolean flag_tasa = false;
    PoblarListas poblar = new PoblarListas();
    cierreCuentaDetalle entidad;
    Int64 codPersona = 0;
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(cuentasProgramado.CodigoProgramaProrrogaCuenta, "L");

            Site toolBar = (Site)this.Master;
            //toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            //ctlMensaje.eventoClick += btnContinuarMen_Click;
            txtFecha.eventoCambiar += txtFecha_textchange;
            toolBar.eventoGuardar += btnGuardar_Click;
          //  toolBar.eventoImprimir += btnImprimir_Click;

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
                cargarddl();
                CargarDropdown();
                // controltas.Inicializar();
                mvAplicar.ActiveViewIndex = 0;
                ddLinea.Enabled = false;
                ddlPeriodicidad.Enabled = false;
                ddlTipoIdentificacion.Enabled = false;
                //ddlTipoTasa.Enabled = false;
                txtFechaApertura.Enabled = false;
                txtFechaInteres.Enabled = false;
                txtFechaApertura.Enabled = false;
                txtFechaProximoPago.Enabled = false;
                txtFecha.Texto = DateTime.Now.ToShortDateString();
                Session["Tasa"] = null;


                if (Session[cuentasProgramado.CodigoProgramaProrrogaCuenta + ".id"] != null)
                {
                    idObjeto = Session[cuentasProgramado.CodigoProgramaProrrogaCuenta + ".id"].ToString();
                    Session.Remove(cuentasProgramado.CodigoProgramaProrrogaCuenta + ".id");
                    //cargarddl();
                    cargarCampos();
                }
                else
                {

                }

             
                rbCalculoTasa_SelectedIndexChanged(rbCalculoTasa, null);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(SolicServicios.GetType().Name + "L", "Page_Load", ex);
        }

    }
    // carga ddl
    void cargarddl()
    {


        poblar.PoblarListaDesplegable("periodicidad", ddlPeriodicidad, (Usuario)Session["usuario"]);

        poblar.PoblarListaDesplegable("periodicidad", ddlPeriodicidadPrroga, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("TIPOIDENTIFICACION", ddlTipoIdentificacion, (Usuario)Session["usuario"]);
        poblar.PoblarListaDesplegable("lineaprogramado", ddLinea, (Usuario)Session["usuario"]);

        // TASA HISTORICA
        List<Xpinn.FabricaCreditos.Entities.TipoTasaHist> lstTipoTasaHist = new List<Xpinn.FabricaCreditos.Entities.TipoTasaHist>();
        Xpinn.FabricaCreditos.Services.TipoTasaHistService TipoTasaHistServicios = new Xpinn.FabricaCreditos.Services.TipoTasaHistService();
        Xpinn.FabricaCreditos.Entities.TipoTasaHist vTipoTasaHist = new Xpinn.FabricaCreditos.Entities.TipoTasaHist();
        lstTipoTasaHist = TipoTasaHistServicios.ListarTipoTasaHist(vTipoTasaHist, (Usuario)Session["Usuario"]);
        ddlHistorico.DataSource = lstTipoTasaHist;
        ddlHistorico.DataTextField = "descripcion";
        ddlHistorico.DataValueField = "tipo_historico";

        ddlHistorico.DataBind();

        // TIPOS DE TASA
        List<Xpinn.FabricaCreditos.Entities.TipoTasa> lstTipoTasa = new List<Xpinn.FabricaCreditos.Entities.TipoTasa>();
        Xpinn.FabricaCreditos.Services.TipoTasaService TipoTasaServicios = new Xpinn.FabricaCreditos.Services.TipoTasaService();
        Xpinn.FabricaCreditos.Entities.TipoTasa vTipoTasa = new Xpinn.FabricaCreditos.Entities.TipoTasa();
        lstTipoTasa = TipoTasaServicios.ListarTipoTasa(vTipoTasa, (Usuario)Session["Usuario"]);
        ddlTipoTasa.DataSource = lstTipoTasa;
        ddlTipoTasa.DataTextField = "nombre";
        ddlTipoTasa.DataValueField = "cod_tipo_tasa";
        ddlTipoTasa.DataBind();
    }

    void CargarDropdown()
    {
        
        
    }

  

    public void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    public void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
    {
        List<Xpinn.Comun.Entities.ListaDesplegable> plista = new List<Xpinn.Comun.Entities.ListaDesplegable>();
        Xpinn.Comun.Entities.ListaDesplegable pentidad = new Xpinn.Comun.Entities.ListaDesplegable();
        Xpinn.Comun.Services.ListaDeplegableService pservicio = new Xpinn.Comun.Services.ListaDeplegableService();

        ddlControl.Items.Clear();
        plista = pservicio.ListarListaDesplegable(pentidad, pTabla, pColumnas, pCondicion, pOrden, (Usuario)Session["usuario"]);
        pentidad.idconsecutivo = null;
        pentidad.descripcion = "Seleccione un item";
        plista.Insert(0, pentidad);
        ddlControl.DataTextField = "descripcion";
        ddlControl.DataValueField = "idconsecutivo";
        ddlControl.DataSource = plista;
        ddlControl.DataBind();
    }




    // calcula los valores de los campos y los carga 
    protected void cargarCampos()
    {
        // Consultar la cuenta
        cierreCuentaDetalle objetoCuenta = cuentasProgramado.ProrrogarCuentaDService(idObjeto, (Usuario)Session["usuario"]);
        //Xpinn.Programado.Entities.cierreCuentaDetalle entidadF = new Xpinn.Programado.Entities.cierreCuentaDetalle();
        objetoCuenta.fecha_Liquida = Convert.ToDateTime(txtFecha.Texto);
        objetoCuenta.NumeroProgramado = idObjeto.ToString();
        txtCodPersona.Text = Convert.ToString(objetoCuenta.Cod_persona);

        if (txtTasa.BackColor == System.Drawing.Color.AliceBlue)
            objetoCuenta.Tasa_Diferencial = Decimal.Parse(txtTasa.Text);
        // Traer los datos del Ahorro Programado y Calcular la liquidación del ahorro programado
        Xpinn.Programado.Data.CuentasProgramadoData DACierre = new Xpinn.Programado.Data.CuentasProgramadoData();
        objetoCuenta = cuentasProgramado.cerrarCuentasServices(objetoCuenta, (Usuario)Session["usuario"]);

        // Mostrar los datos del ahorro programado
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
            txtOficina.Text = objetoCuenta.Oficina.ToString();
        if (objetoCuenta.Periodicidad != string.Empty)
            ddlPeriodicidad.SelectedValue = objetoCuenta.Codigo_Perioricidad.ToString();
        if (objetoCuenta.TipoIdentificacion != 0)
            ddlTipoIdentificacion.SelectedValue = objetoCuenta.TipoIdentificacion.ToString();
        if (objetoCuenta.Linea != string.Empty)
            ddLinea.SelectedValue = objetoCuenta.Linea;
        if (objetoCuenta.saldo != 0)
            txtSaldoTotal.Text = objetoCuenta.saldo.ToString();
        if (objetoCuenta.fecha_interes != null)
            txtFechaInteres.Texto = objetoCuenta.fecha_interes.ToShortDateString();
        if (objetoCuenta.total_interes != 0)
            txtTotInteres.Text = objetoCuenta.total_interes.ToString();
        if (objetoCuenta.total_retencion != 0)
            txtTotRetencion.Text = objetoCuenta.total_retencion.ToString();

        PanelTasa.Visible = false;
        // Mostrar la liquidación del ahorro programado
        //if (objetoCuenta.Interes_causado != 0)
            txtInteresCausado.Text = objetoCuenta.Interes_causado.ToString();
        //if (objetoCuenta.Retencion_causada != 0)
            txtMenosRetencionCausada.Text = objetoCuenta.Retencion_causada.ToString();
        //if (objetoCuenta.Interes_ != 0)
            txtInteres.Text = objetoCuenta.Interes_.ToString();
        //if (objetoCuenta.Menos_Retencion != 0)
            txtMenosRetencion.Text = objetoCuenta.Menos_Retencion.ToString();
        //if (objetoCuenta.Menos_GMF != 0)
            txtGmf.Text = objetoCuenta.Menos_GMF.ToString();
        //if (objetoCuenta.Menos_Descuento != 0)
            txtMenosDescuento.Text = objetoCuenta.Menos_Descuento.ToString();

        Decimal saldo = Convert.ToDecimal(objetoCuenta.saldo.ToString());
        Decimal totalapagar = Convert.ToDecimal(objetoCuenta.Total_pagar.ToString());
        Decimal valortotal = saldo + totalapagar;
        txtTotalPagar.Text = valortotal.ToString();

        //if (objetoCuenta.TipoTasa > 1)
        //    controltas.TipoTasa = (int)objetoCuenta.TipoTasa;
        CuentasProgramado pCuenta = new CuentasProgramado();
        pCuenta = CuentasPrograServicios.ConsultarAhorroProgramado(Convert.ToString(idObjeto), (Usuario)Session["usuario"]);

        ///////TASA
        if (pCuenta.calculo_tasa != 0)
            rbCalculoTasa.SelectedValue = pCuenta.calculo_tasa.ToString();
        if (pCuenta.tipo_historico != 0)
            ddlHistorico.SelectedValue = pCuenta.tipo_historico.ToString();
        if (pCuenta.desviacion != 0)
            txtDesviacion.Text = pCuenta.desviacion.ToString();
        if (pCuenta.tasa_interes != 0)
            txtTasa.Text = objetoCuenta.Tasa_Diferencial != 0 ? objetoCuenta.Tasa_Diferencial.ToString() : pCuenta.tasa_interes.ToString();
        if (pCuenta.cod_tipo_tasa != 0)
            ddlTipoTasa.SelectedValue = pCuenta.cod_tipo_tasa.ToString();
        ///////

        DateTime fechavencimiento = DateTime.Now;
        Int32 plazo = Convert.ToInt32(txtPlazo.Text);
        Int32 tipocalendario = 1;
        DateTime fecha_proximo_pago = Convert.ToDateTime(txtFecha.Texto.ToString());

        Xpinn.FabricaCreditos.Entities.Periodicidad periodicidad = new Xpinn.FabricaCreditos.Entities.Periodicidad();
        Xpinn.FabricaCreditos.Data.PeriodicidadData PeriodicidadData = new Xpinn.FabricaCreditos.Data.PeriodicidadData();
        periodicidad = PeriodicidadData.ConsultarPeriodicidad(Convert.ToInt64(ddlPeriodicidad.SelectedValue), (Usuario)Session["usuario"]);
        DateTime fecha_apertura = Convert.ToDateTime(txtFechaApertura.Text);
       // fechavencimiento = AperturaService.Calcularfecha(fechavencimiento, fecha_apertura, (plazo * Convert.ToInt32(periodicidad.numero_dias)), tipocalendario);
      //  txtfechaVenci.Text = Convert.ToString(fechavencimiento);
        txtfechaVenci.Text = Convert.ToString(pCuenta.fecha_vencimiento);


        //   if (fechavencimiento > DateTime.Now)
        // {txtTasa.Enabled = true; VerError("Fecha actual menor a fecha vencimiento , Puede modificar Tasa Interes."); }
        //  else { txtTasa.Enabled = false ; }


        //Datos de Proroga 
        ddlPeriodicidadPrroga.SelectedValue = ddlPeriodicidad.SelectedValue;
        txtPlazoPrroga.Text = txtPlazo.Text;
        txtCuotaProroga.Text = txtCuota.Text;

        //Generar Consulta de la Linea Seleccionada
        LineasProgramado vLineaAhorro = new LineasProgramado();
        LineasProgramadoServices lineaServicio = new LineasProgramadoServices();
        List<LineaProgramado_Rango> Lista_rango = new List<LineaProgramado_Rango>();
        vLineaAhorro = lineaServicio.ConsultarLineas_Programado(Convert.ToInt64(ddLinea.SelectedValue), (Usuario)Session["usuario"]);

        if (vLineaAhorro.interes_por_cuenta == 0)
        {
            rbCalculoTasa.Enabled = false;
            txtTasa.Enabled = false;
            ddlTipoTasa.Enabled = false;
            ddlHistorico.Enabled = false;
        }
        if (vLineaAhorro.interes_por_cuenta == 1)
        {
            rbCalculoTasa.Enabled = false;
            txtTasa.Enabled = true;
            ddlTipoTasa.Enabled = false;
            ddlHistorico.Enabled = false;
        }



        CalcularProroga();

         Site toolBar = (Site)this.Master;
        // toolBar.eventoImprimir += btnImprimir_Click;
        entidad = objetoCuenta;

    }

    public void CalcularProroga()

        {
        DateTime fechavencimientoProrroga = DateTime.Now;
        txtFechaAperturaProrroga.Texto = txtfechaVenci.Text;
        DateTime fecha_aperturaProroga = Convert.ToDateTime(txtFechaAperturaProrroga.Texto);
        Xpinn.FabricaCreditos.Entities.Periodicidad periodicidad = new Xpinn.FabricaCreditos.Entities.Periodicidad();
        Xpinn.FabricaCreditos.Data.PeriodicidadData PeriodicidadData = new Xpinn.FabricaCreditos.Data.PeriodicidadData();
        periodicidad = PeriodicidadData.ConsultarPeriodicidad(Convert.ToInt64(ddlPeriodicidadPrroga.SelectedValue), (Usuario)Session["usuario"]);
        Int32 tipocalendario = 1;


        fechavencimientoProrroga = AperturaService.Calcularfecha(fechavencimientoProrroga, fecha_aperturaProroga, (Convert.ToInt32(txtPlazoPrroga.Text) * Convert.ToInt32(periodicidad.numero_dias)), tipocalendario);
        txtfechaVenciProrroga.Text = Convert.ToString(fechavencimientoProrroga);

    }
    public cierreCuentaDetalle getEntidad()
    {
        // Trae los datos de la cuenta
        cierreCuentaDetalle objetoCuenta = cuentasProgramado.cierreCuentaDService(idObjeto, (Usuario)Session["usuario"]);
        Xpinn.Programado.Entities.cierreCuentaDetalle entidadF = new Xpinn.Programado.Entities.cierreCuentaDetalle();
        objetoCuenta.fecha_Liquida = Convert.ToDateTime(txtFecha.Texto);
        objetoCuenta.NumeroProgramado = idObjeto.ToString();
        // Realiza la liquidación de los intereses
        //  if (txtTasa.BackColor == System.Drawing.Color.AliceBlue)
        //    objetoCuenta.Tasa_Diferencial = Decimal.Parse(txtTasa.Text);
        //objetoCuenta = cuentasProgramado.cerrarCuentasServices(objetoCuenta, (Usuario)Session["usuario"]);
        //objetoCuenta.Total_pagar = Convert.ToInt64(txtTotalPagar.Text.Replace(".", ""));

        objetoCuenta.cuota = Convert.ToDecimal(txtCuotaProroga.Text);
        objetoCuenta.Plazo = Convert.ToInt32(txtPlazoPrroga.Text);
        objetoCuenta.Fecha_Apertura = Convert.ToDateTime(txtFechaAperturaProrroga.Texto);
        objetoCuenta.Fecha_Vencimiento = Convert.ToDateTime(txtfechaVenciProrroga.Texto);
        objetoCuenta.Codigo_Perioricidad = Convert.ToInt32(ddlPeriodicidadPrroga.SelectedValue);
        objetoCuenta.Codigo_Perioricidad_int = Convert.ToInt32(ddlPeriodicidadPrroga.SelectedValue);
        objetoCuenta.TipoTasa = Convert.ToInt32(ddlTipoTasa.SelectedValue);
        objetoCuenta.calculo_tasa = Convert.ToInt32(rbCalculoTasa.SelectedValue);
        objetoCuenta.Tasa = Convert.ToDecimal(txtTasa.Text);
        return objetoCuenta;
    }

    // redirecciona ala pagina principal para cancelar acciones 
    public void btnCancelar_Click(object sender, EventArgs e)
    {
       
        Response.Redirect("../../Programado/ReporteVencimiento/Lista.aspx", false);
    
    }


    // Cambio Valor Tasa de Interes y Recalcular VALORES

    protected void txtTasa_TextChanged(object sender, EventArgs e)
    {
        flag_tasa = true;
        txtTasa.BackColor = System.Drawing.Color.AliceBlue;
        cargarCampos();
    }


    // actualiza el metodo cargar campo por si cambio la fecha de liquidacion
    public void txtFecha_textchange(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtFecha.Text) > DateTime.Now)
        {
            VerError("La fecha de cierre no puede ser mayor a la fecha actual");
            txtFecha.Text = DateTime.Now.ToShortDateString();
        }
        else
            cargarCampos();
    }

    // carga la entidad de operacion y la retorna llena 
    Xpinn.Tesoreria.Entities.Operacion cargarOperacion()
    {
        try
        {
            Xpinn.Tesoreria.Entities.Operacion operacion = new Xpinn.Tesoreria.Entities.Operacion();
            operacion.cod_ope = 0;
            operacion.tipo_ope = 45;
            operacion.cod_caja = 0;
            operacion.cod_cajero = 0;
            operacion.observacion = "Grabacion de operacion- Prorroga Cuenta Programado";
            operacion.cod_proceso = null;
            operacion.fecha_oper = DateTime.Now;
            operacion.fecha_calc = Convert.ToDateTime(txtFecha.Texto);

            return operacion;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(cuentasProgramado.GetType().Name + "L", "Page_PreInit", ex);
            return null;
        }
    }


    // cierra la cuenta y le agraga los campos correspondientes 
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Int64 CodigoOperacion = 0;

        // si no hay saldo total o interes 
        if (txtSaldoTotal.Text == string.Empty || txtSaldoTotal.Text.Length <= 0)
        {
            VerError("No hay saldo o intereses que liquidar");
        }
        else
        {

                String estado = "";
                DateTime fechacierrehistorico;
                DateTime fechacierre = Convert.ToDateTime(txtFecha.Texto);
                Xpinn.Programado.Entities.CuentasProgramado vAhorroProgramado = new Xpinn.Programado.Entities.CuentasProgramado();
                vAhorroProgramado = cuentasProgramado.ConsultarCierreAhorroProgramado((Usuario)Session["usuario"]);
                estado = vAhorroProgramado.estadocierre;
                fechacierrehistorico = Convert.ToDateTime(vAhorroProgramado.fecha_cierre.ToString());

                if (estado == "D" && fechacierre <= fechacierrehistorico)
                {
                    VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO L,'AH. PROGRAMADO'");
                }

                else
                {


                // guardar y cerra la cuenta

                   cuentasProgramado.ProrrogaProgramado(getEntidad(), ref CodigoOperacion, cargarOperacion(), (Usuario)Session["usuario"]);
                if (CodigoOperacion != 0)
                {
                   // Xpinn.Programado.Services.CuentasProgramadoServices cuentasProgramado = new Xpinn.Programado.Services.CuentasProgramadoServices();

                    String id = txtCuenta.Text;
                    Session[CuentasPrograServicios.CodigoPrograma + ".id"] = id;
                    Navegar("../Prorroga/Lista.aspx");
                }
                    //if (CodigoOperacion != 0)
                    // {                      

                    //    Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                    //  Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = CodigoOperacion;
                    //  Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = 45;
                    //  Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = getEntidad().Cod_persona; //"<Colocar Aquí el código de la persona del servicio>"
                    //  Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                    //  Session[cuentasProgramado.CodigoProgramaProrrogaCuenta + ".id"] = idObjeto;
                    // }

            }
        }
    }

    


    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];

        ReportParameter[] param = new ReportParameter[31];
        param[0] = new ReportParameter("entidad", pUsuario.empresa);
        param[1] = new ReportParameter("nit", pUsuario.nitempresa);
        param[2] = new ReportParameter("ImagenReport", ImagenReporte());
        param[3] = new ReportParameter("fechaCierre", Convert.ToDateTime(txtFecha.Texto).ToShortDateString());
        param[4] = new ReportParameter("identificacion", txtIdentificacion.Text);
        param[5] = new ReportParameter("TipoIdentificacion", ddlTipoIdentificacion.SelectedItem.Text);
        param[6] = new ReportParameter("NombrePersona", txtNomPersona.Text);
        param[7] = new ReportParameter("cuenta", this.txtCuenta.Text);
        param[8] = new ReportParameter("fechaApertura", Convert.ToDateTime(txtFechaApertura.Texto).ToShortDateString());
        param[9] = new ReportParameter("oficina", txtOficina.Text);
        param[10] = new ReportParameter("Tipolinea", ddLinea.SelectedItem.Text);
        param[11] = new ReportParameter("saldoTotal", txtSaldoTotal.Text);
        param[12] = new ReportParameter("fechaProximoPago", Convert.ToDateTime(txtFechaProximoPago.Texto).ToShortDateString());
        param[13] = new ReportParameter("cuota", txtCuota.Text);
        param[14] = new ReportParameter("plazo", txtPlazo.Text);
        param[15] = new ReportParameter("perioricidad", ddlPeriodicidad.SelectedItem.Text);
        param[16] = new ReportParameter("tipoTasa", this.ddlTipoTasa.SelectedItem.Text.ToString());
        if (txtTotInteres.Text == string.Empty)
            param[17] = new ReportParameter("totalInteres", "0");
        else param[17] = new ReportParameter("totalInteres", txtTotInteres.Text);

        if (txtTotRetencion.Text == string.Empty)
            param[18] = new ReportParameter("totalRetencion", "0");
        else param[18] = new ReportParameter("totalRetencion", txtTotRetencion.Text);
        param[19] = new ReportParameter("FechaInteres", txtFechaInteres.Texto);
        if (txtInteres.Text == string.Empty)
            param[20] = new ReportParameter("Interes", "0");
        else param[20] = new ReportParameter("Interes", txtInteres.Text);
        if (txtMenosRetencion.Text == string.Empty)
            param[21] = new ReportParameter("MenosRetencion", "0");
        else param[21] = new ReportParameter("MenosRetencion", txtMenosRetencion.Text);
        if (txtGmf.Text == string.Empty)
            param[22] = new ReportParameter("menosGMF", "0");
        else param[22] = new ReportParameter("menosGMF", txtGmf.Text);
        if (txtMenosDescuento.Text == string.Empty)
            param[23] = new ReportParameter("menosDescuento", "0");
        else param[23] = new ReportParameter("menosDescuento", txtMenosDescuento.Text);
        if (txtTotalPagar.Text == string.Empty)
            param[24] = new ReportParameter("totalPagar", "0");
        else param[24] = new ReportParameter("totalPagar", txtTotalPagar.Text);

     
        //totalInteres totalRetencion
        // param[25] = new ReportParameter("MostrarGrilla", "true");

        rvReportMov.LocalReport.DataSources.Clear();
        rvReportMov.LocalReport.EnableExternalImages = true;
        rvReportMov.LocalReport.SetParameters(param);

        rvReportMov.LocalReport.Refresh();

        frmPrint.Visible = false;
        rvReportMov.Visible = true;
        Site toolBar = (Site)this.Master;
        toolBar.MostrarImprimir(false);

        mvAplicar.ActiveViewIndex = 1;
    }

    protected void btnDatos_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)this.Master;
        toolBar.MostrarImprimir(true);
        mvAplicar.ActiveViewIndex = 0;
    }

    protected void btnImpresion_Click(object sender, EventArgs e)
    {
        // MOSTRAR REPORTE EN PANTALLA
        Warning[] warnings;
        string[] streamids;
        string mimeType;
        string encoding;
        string extension;
        byte[] bytes = rvReportMov.LocalReport.Render("PDF", null, out mimeType,
                       out encoding, out extension, out streamids, out warnings);
        FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output.pdf"),
        FileMode.Create);
        fs.Write(bytes, 0, bytes.Length);
        fs.Close();
        Session["Archivo" + Usuario.codusuario] = Server.MapPath("output.pdf");
        frmPrint.Visible = true;
        rvReportMov.Visible = false;
    }

    protected void rbCalculoTasa_SelectedIndexChanged(object sender, EventArgs e)
    {
        PanelFija.Visible = false;
        PanelHistorico.Visible = false;
        if (rbCalculoTasa.SelectedIndex == 0)
            PanelFija.Visible = true;
        if (rbCalculoTasa.SelectedIndex == 1)
            PanelHistorico.Visible = true;
        if (rbCalculoTasa.SelectedIndex == 2)
            PanelHistorico.Visible = true;
    }



    protected void txtPlazoPrroga_TextChanged(object sender, EventArgs e)
    {
        CalcularProroga();
    }

    protected void ddlPeriodicidadPrroga_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalcularProroga();
    }
}

