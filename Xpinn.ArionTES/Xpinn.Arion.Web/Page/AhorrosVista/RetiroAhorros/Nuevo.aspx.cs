using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Newtonsoft.Json;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Ahorros.Services.AhorroVistaServices ahorrosServicio = new Xpinn.Ahorros.Services.AhorroVistaServices();
    private Int64 tipoOpe = 15;
    private String pErrorgeneral = "";
    private Usuario _usuario;
    private string _convenio = "";

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {                      
            if (Session[ahorrosServicio.CodigoProgramaRet + ".id"] != null)
                VisualizarOpciones(ahorrosServicio.CodigoProgramaRet, "E");
            else
                VisualizarOpciones(ahorrosServicio.CodigoProgramaRet, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.MostrarConsultar(false);
            toolBar.eventoConsultar += btnConsultar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            ctlproceso.eventoCancelarClick += btnCancelarProceso_Click;
            ctlproceso.eventoClick += btnAceptarProceso_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaRet, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _convenio = ConvenioTarjeta(0);
            ctlPersona.Editable(false);
            if (!IsPostBack)
            {
                txtFechaRetiro.Text = DateTime.Now.ToString(gFormatoFecha);
                mvAhorroVista.ActiveViewIndex = 0;
                txtNumeroCuenta.Enabled = false;                
                CargarListas();
                if (Session[ahorrosServicio.CodigoProgramaRet + ".id"] != null)
                {
                    idObjeto = Session[ahorrosServicio.CodigoProgramaRet + ".id"].ToString();
                    Session.Remove(ahorrosServicio.CodigoProgramaRet + ".id");
                    ObtenerDatos(idObjeto);
                }
                else
                {
                    txtNumeroCuenta.Text = "";
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaRet, "Page_Load", ex);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected Boolean ValidarDatos()
    {
        if (string.IsNullOrWhiteSpace(txtFechaRetiro.Text))
        {
            VerError("Ingrese la fecha de retiro.");
            txtFechaRetiro.Focus();
            return false;
        }
        //if (txtsaldo.Text == "" || txtsaldo.Text == "0")
        //{
        //    VerError("No se puede realizar la grabación, verifique el saldo calculado");
        //    return false;
        //}

        decimal pSaldoTotal = string.IsNullOrEmpty(txtSaldoTotal.Text) ? 0 : Convert.ToDecimal(txtSaldoTotal.Text);
        decimal pVrRetiro = string.IsNullOrEmpty(txtValorRetiro.Text) ? 0 : Convert.ToDecimal(txtValorRetiro.Text);
        if (pVrRetiro > pSaldoTotal)
        {
            VerError("No se puede realizar la grabación, el valor a retirar supera el saldo actual del ahorro");
            return false;
        }

        //Validando datos del control de Giro
        if (ctlGiro.IndiceFormaDesem == 0)
        {
            VerError("Seleccione una forma de desembolso");
            return false;
        }
        else
        {
            if (ctlGiro.IndiceFormaDesem == 2 || ctlGiro.IndiceFormaDesem == 3)
            {
                if (ctlGiro.IndiceEntidadOrigen == 0)
                {
                    VerError("Seleccione un Banco de donde se girará");
                    return false;
                }
                if (ctlGiro.IndiceFormaDesem == 3)
                {
                    if (ctlGiro.IndiceEntidadDest == 0)
                    {
                        VerError("Seleccione la Entidad de destino");
                        return false;
                    }
                    if (ctlGiro.TextNumCuenta == "")
                    {
                        VerError("Ingrese el número de la cuenta");
                        return false;
                    }
                }
            }
        }
        return true;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (ValidarDatos())
            ctlMensaje.MostrarMensaje("Desea realizar la grabación de los datos");

    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        VerError("");

        // Validar que exista la parametrización contable por procesos
        if (ValidarProcesoContable(DateTime.Now, tipoOpe) == false)
        {
            VerError("No se encontró parametrización contable por procesos para el tipo de operación 15-Retiros de Cuentas de Ahorros");
            return;
        }

        // Determinar código de proceso contable para generar el comprobante
        Int64? rpta = 0;
        if (mvAhorroVista.ActiveViewIndex == 0)
        {
            rpta = ctlproceso.Inicializar(tipoOpe, DateTime.Now, (Usuario)Session["Usuario"]);
            if (rpta > 1)
            {
                Site toolBar = (Site)Master;
                toolBar.MostrarGuardar(false);
                // Activar demás botones que se requieran
                mvAhorroVista.ActiveViewIndex = 1;
            }
            else
            {
                // Crear la tarea de ejecución del proceso                
                if (AplicarDatos())
                {                    
                    Navegar("../../Contabilidad/Comprobante/Nuevo.aspx");
                }
                else
                {
                    VerError(".." + pErrorgeneral);
                }
            }
        }
        else
        {
            VerError("No se determino el proceso contable");
        }

    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["solicitud"] != null)
        {
            Session["solicitud"] = null;
            Response.Redirect("../../AhorrosVista/ConfirmarRetiroAprobado.aspx", false);
        }
        else
        {
            Navegar(Pagina.Lista);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVista = ahorrosServicio.ConsultarAhorroVista(Convert.ToString(pIdObjeto), (Usuario)Session["usuario"]);

            if (!string.IsNullOrEmpty(vAhorroVista.numero_cuenta.ToString()))
                txtNumeroCuenta.Text = HttpUtility.HtmlDecode(vAhorroVista.numero_cuenta.ToString().Trim());

            if (!string.IsNullOrEmpty(vAhorroVista.nom_oficina.ToString()))
                txtOficina.Text = HttpUtility.HtmlDecode(vAhorroVista.nom_oficina.ToString().Trim());

            if (!string.IsNullOrEmpty(vAhorroVista.nom_linea.ToString()))
                txtLineaAhorro.Text = HttpUtility.HtmlDecode(vAhorroVista.nom_linea.ToString().Trim());

            if (!string.IsNullOrEmpty(vAhorroVista.fecha_apertura.ToString()))
                ucFechaApertura.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vAhorroVista.fecha_apertura.ToString()));

            if (!string.IsNullOrEmpty(vAhorroVista.saldo_total.ToString()))
                txtSaldoTotal.Text = HttpUtility.HtmlDecode(vAhorroVista.saldo_total.ToString().Replace(gSeparadorDecimal, ",").Trim());

            if (!string.IsNullOrEmpty(vAhorroVista.moneda.ToString()))
                txtMoneda.Text = HttpUtility.HtmlDecode(vAhorroVista.moneda.ToString().Trim());

            if (!string.IsNullOrEmpty(vAhorroVista.saldo_final.ToString()))
            {
                decimal saldoDisponible = vAhorroVista.saldo_total - Convert.ToDecimal(vAhorroVista.saldo_canje);
                txtSaldoDisponible.Text = HttpUtility.HtmlDecode(saldoDisponible.ToString().Replace(gSeparadorDecimal, ",").Trim());
            }

            if (vAhorroVista.cod_persona != null)
            {
                Session["cod_persona"] = vAhorroVista.cod_persona;
                if (vAhorroVista.identificacion != null)
                {
                    ctlPersona.AdicionarPersona(vAhorroVista.identificacion.ToString(), Convert.ToInt64(vAhorroVista.cod_persona), vAhorroVista.nombres, vAhorroVista.tipo_identifi);
                }
            }
            this.txtVolante.Text = vAhorroVista.identificacion.ToString();

            ////RECUPERAR DATOS DEL GIRO
            ActividadesServices ActividadServicio = new ActividadesServices();
            List<CuentasBancarias> LstCuentasBanc = new List<CuentasBancarias>();
            Int64 cod = Convert.ToInt64(Session["cod_persona"]);
            string filtro = " and Principal = 1";
            LstCuentasBanc = ActividadServicio.ConsultarCuentasBancarias(cod, filtro, (Usuario)Session["usuario"]);
            ctlGiro.ValueFormaDesem = "2";
            if (LstCuentasBanc.Count > 0)
            {
                ctlGiro.ValueFormaDesem = "3";
                if (LstCuentasBanc[0].cod_banco != null && LstCuentasBanc[0].cod_banco != 0)
                    ctlGiro.ValueEntidadDest = LstCuentasBanc[0].cod_banco.ToString();
                if (LstCuentasBanc[0].numero_cuenta != null && LstCuentasBanc[0].numero_cuenta != "")
                    ctlGiro.TextNumCuenta = LstCuentasBanc[0].numero_cuenta;
                if (LstCuentasBanc[0].tipo_cuenta != null && LstCuentasBanc[0].tipo_cuenta != 0)
                {
                    try
                    {
                        ctlGiro.ValueTipoCta = LstCuentasBanc[0].tipo_cuenta.ToString();
                    }
                    catch { }
                }
            }

            if(Session["solicitud"] != null)
            {
                AhorroVista solicitud = new AhorroVista();
                solicitud = Session["solicitud"] as AhorroVista;
                this.txtValorRetiro.Text = solicitud.retiro.ToString().Replace(".","");
                ctlGiro.ValueFormaDesem = solicitud.forma_giro.ToString();
                if(solicitud.forma_giro == 3)
                {
                    ctlGiro.ValueEntidadDest = solicitud.cod_banco.ToString();
                    this.txtNumeroCuenta.Text = solicitud.numero_cuenta_final.ToString();
                    ctlGiro.ValueTipoCta = solicitud.tipo_cuenta.ToString();
                }

                if (string.IsNullOrWhiteSpace(solicitud.id_solicitud.ToString()))
                {
                    _usuario = (Usuario)Session["Usuario"];
                    solicitud.nom_estado = "1"; // Aprobar Solicitud
                    AhorroVistaServices _ahorrosService = new AhorroVistaServices();
                    _ahorrosService.ModificarEstadoSolicitud(solicitud, _usuario);
                }
            }
            gvDetMovs.Visible = false;

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaRet, "ObtenerDatos", ex);
        }
    }

    private void CargarListas()
    {
        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        try
        {
            ctlGiro.Inicializar();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    protected void btnCancelarProceso_Click(object sender, EventArgs e)
    {
        Site toolBar = (Site)Master;
        toolBar.MostrarGuardar(true);
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(true);
        mvAhorroVista.ActiveViewIndex = 0;
    }

    protected void btnAceptarProceso_Click(object sender, EventArgs e)
    {
        try
        {
            mvAhorroVista.ActiveViewIndex = 0;

            // Aquí va la función que hace lo que se requiera grabar en la funcionalidad
            AplicarDatos();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected bool AplicarDatos()
    {
        String error = "";
        try
        {
            Usuario vUsuario = new Usuario();
            vUsuario = (Usuario)Session["Usuario"];
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVista = new Xpinn.Ahorros.Entities.AhorroVista();

            // Obtener los datos de la cuenta de ahorros
            if (idObjeto != "")
                vAhorroVista = ahorrosServicio.ConsultarAhorroVista(Convert.ToString(idObjeto), (Usuario)Session["usuario"]);
            else
                return false;

            vAhorroVista.fecha_cierre = txtFechaRetiro.ToDateTime;
            vAhorroVista.V_Traslado = Convert.ToDecimal(txtValorRetiro.Text.Replace(".", ""));

            // Obtener los datos del giro
            Xpinn.FabricaCreditos.Entities.Giro vGiro = new Xpinn.FabricaCreditos.Entities.Giro();
            vGiro = ctlGiro.ObtenerEntidadGiro(Convert.ToInt64(Session["cod_persona"]), txtFechaRetiro.ToDateTime, Convert.ToDecimal(txtValorRetiro.Text.Replace(".", "")), vUsuario);
            vGiro.tipo_acto = 5;

            // Generar el comprobante
            Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
            pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = tipoOpe;
            pOperacion.cod_caja = 0;
            pOperacion.cod_cajero = 0;
            pOperacion.fecha_oper = Convert.ToDateTime(vAhorroVista.fecha_cierre);
            pOperacion.fecha_calc = Convert.ToDateTime(vAhorroVista.fecha_cierre);
            pOperacion.observacion = "Retiros Cuenta Ahorros. Volante: " + txtVolante.Text;
            pOperacion.cod_proceso = ctlproceso.cod_proceso;

            Int64 codOpe = 0;

            Int64 idGiro = 0;

            String estado = "";
            DateTime fechacierrehistorico;
            DateTime fecharetiro = txtFechaRetiro.ToDateTime;
            Xpinn.Ahorros.Entities.AhorroVista vAhorroVistaCierre = new Xpinn.Ahorros.Entities.AhorroVista();
            vAhorroVistaCierre = ahorrosServicio.ConsultarCierreAhorroVista((Usuario)Session["usuario"]);
            estado = vAhorroVistaCierre.estadocierre;
            fechacierrehistorico = Convert.ToDateTime(vAhorroVistaCierre.fecha_cierre.ToString());

            if (estado == "D" && fecharetiro <= fechacierrehistorico)
            {
                VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO H,'AHORROS'");
                error = "NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO H,'AHORROS'";
            }
            else
            {
                string pError = "";
                if (ahorrosServicio.RetiroDeposito(ref pError, vAhorroVista, pOperacion, vGiro, ref codOpe, ref idGiro, vUsuario))
                {

                    if (codOpe > 0)
                    {                                                
                        // Realizar interfaz con ENPACTO
                        string identificacion = ctlPersona.Text;
                        // Aplicar transacciones enpacto
                        Xpinn.FabricaCreditos.Services.Persona1Service servicioPersona = new Persona1Service();
                        Xpinn.FabricaCreditos.Entities.Persona1 persona = new Persona1();
                        persona = servicioPersona.ConsultaDatosPersona(identificacion, (Usuario)Session["Usuario"]);
                        string tipoIdentificacion = persona.tipo_identificacion.ToString();
                        string nombre = persona.nombre;
                        AplicarTransaccionEnpacto(tipoIdentificacion, identificacion, nombre, TipoDeProducto.AhorrosVista.ToString(), txtNumeroCuenta.Text, "1", ConvertirStringToDecimal(txtValorRetiro.Text), codOpe, "RETIRO DE CUENTAS DE AHORRO A LA VISTA", ref pError);

                        // Variables para la contabilizaci{on
                        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_ope"] = codOpe;
                        Session[ComprobanteServicio.CodigoPrograma + ".tipo_ope"] = tipoOpe;
                        Session[ComprobanteServicio.CodigoPrograma + ".cod_persona"] = Convert.ToInt64(Session["cod_persona"]);
                        Session[ComprobanteServicio.CodigoPrograma + ".idgiro"] = idGiro.ToString();

                        // Se cargan las variables requeridas para generar el comprobante
                        ctlproceso.CargarVariables(codOpe, Convert.ToInt32(tipoOpe), Convert.ToInt64(Session["cod_persona"]), (Usuario)Session["Usuario"]);

                        return true;
                    }
                    else
                    {
                        pError += "--";
                    }

                    pErrorgeneral = pError.Substring(0, 90);
                    
                    return false;
                }
                else
                {
                    pErrorgeneral += "==>" + pError;
                }
            }
            if (pErrorgeneral == "" && error != "")
            {
                pErrorgeneral = error;
            }
            return false;
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(ahorrosServicio.CodigoProgramaRet, "btnGuardar_Click", ex);
            return false;
        }
               
    }


}