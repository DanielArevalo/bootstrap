using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Asesores.Entities;
using Xpinn.Aportes.Entities;
using Xpinn.Asesores.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Reflection;
using System.Text;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Services;

public partial class Nuevo : GlobalWeb
{
    Producto producto = new Producto();
    EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    Xpinn.Comun.Services.Formato_NotificacionService COServices = new Xpinn.Comun.Services.Formato_NotificacionService();
    Configuracion conf = new Configuracion();

    private Usuario usuario = new Usuario();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            ctlMensaje.eventoClick += btnContinuar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoExportar += btnExportar_click;
            toolBar.MostrarExportar(false);
            toolBar.MostrarCancelar(false);
            VisualizarOpciones("110126", "A");
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("EnvioMasivo", "Page_PreInit", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                txtFechaGeneracion.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtFechaCorte.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnEnviar.Visible = false;
                panelLista.Visible = false;
                mpeProcesando.Hide();
                mpeFinal.Hide();
                Session["aporte"] = null;
                Session["credito"] = null;
                Session["acodeudados"] = null;
                Session["ahorro_vista"] = null;
                Session["programado"] = null;
                Session["cdats"] = null;
                Session["devoluciones"] = null;
                Session["servicios"] = null;
                Session["telefonia"] = null;
                Session["mensajeFinal"] = null;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("110126", "Page_Load", ex);
        }
    }
    protected void btnEnviar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            mvPrincipal.SetActiveView(ViewProceso);
            mpeNuevo.Show();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("EnvioMasivo", "btnGuardar_Click", ex);
        }
    }

    /// <summary>
    /// Evento para ejecución del proceso
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                IniciarProceso();
                Task.Factory.StartNew(() => { EnviarCorreos(); });
                List<ErroresCargaAportes> lstErrores = new List<ErroresCargaAportes>();
                lstErrores = (List<ErroresCargaAportes>)Session["lsterrores"];
                if (lstErrores != null && lstErrores.Count() > 0)
                {
                    mvPrincipal.SetActiveView(ViewErrores);
                    pErrores.Visible = true;
                    pErroresG.Visible = true;
                    gvErrores.DataSource = lstErrores;
                    gvErrores.DataBind();
                    cpeDemo.CollapsedText = "(Click Aqui para ver " + lstErrores.Count() + " errores...)";
                    cpeDemo.ExpandedText = "(Click Aqui para ocultar listado de errores...)";
                }
                else
                {
                    pProcesando.Visible = false;
                    pFinal.Visible = true;
                    ctlMensaje.Visible = false;
                }

            }
        }
        catch (Exception ex)
        {
            VerError("Se ha producido un error" + ex.Message.ToString());
        }
    }

    /// <summary>
    /// Realizar la consulta de los datos para mostrar en pantalla las personas a las cuales se envia correo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
        Actualizar();
    }

    #region Métodos de proceso
    /// <summary>
    ///  Método que inicia el proceso, deshabilita los botones de consulta y activa el temporizador
    /// </summary>
    public void IniciarProceso()
    {
        mvPrincipal.SetActiveView(ViewProceso);
        btnContinuar.Enabled = false;
        btnCancelar.Enabled = false;
        mpeNuevo.Hide();
        pProcesando.Visible = true;
        mpeProcesando.Show();
        Image1.Visible = true;
        Session["Proceso"] = "INICIO";
        Timer1.Enabled = true;
        Site toolbar = (Site)this.Master;
        toolbar.MostrarConsultar(false);
    }

    /// <summary>
    /// Temporizador para ejecuciòn del proceso
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Session["Proceso"] != null)
            if (Session["Proceso"].ToString() == "FINAL")
                TerminarProceso();
            else
                mpeProcesando.Show();
        else
            mpeProcesando.Hide();
    }

    /// <summary>
    /// Termina el proceso y para el temporizador
    /// </summary>
    public void TerminarProceso()
    {
        mpeProcesando.Hide();
        Image1.Visible = false;
        Session.Remove("Proceso");
        Timer1.Enabled = false;
        mpeFinal.Show();
        if (Session["Error"] != null)
        {
            if (Session["Error"].ToString().Trim() != "")
                lblError.Text = Session["Error"].ToString();
            Session.Remove("Error");
        }
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        mpeNuevo.Hide();
        mpeProcesando.Hide();
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {
        mvPrincipal.SetActiveView(ViewListado);
    }

    protected void btnSalir_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/General/Global/inicio.aspx");
    }

    #endregion

    private List<EstadoCuenta> ObtenerListado()
    {
        List<EstadoCuenta> lstAsociados = new List<EstadoCuenta>();
        foreach (GridViewRow fila in gvLista.Rows)
        {
            CheckBox chkEnviar = (CheckBox)fila.FindControl("chkEnviar");
            EstadoCuenta pExtracto;
            if (chkEnviar.Checked)
            {
                pExtracto = new EstadoCuenta();
                pExtracto.Codigo = Convert.ToInt64(fila.Cells[2].Text);
                pExtracto.nombre_titular = fila.Cells[4].Text;
                pExtracto.email = fila.Cells[5].Text;

                lstAsociados.Add(pExtracto);
            }
        }

        if (lstAsociados.Count > 0)
            return lstAsociados;
        else return null;
    }

    protected bool ValidarDatos()
    {
        TiposDocCobranzasServices _tipoDocumentoServicio = new TiposDocCobranzasServices();
        Usuario pUsuario = (Usuario)Session["usuario"];

        List<EstadoCuenta> lstClientes = new List<EstadoCuenta>();
        lstClientes = ObtenerListado();
        if (lstClientes == null || lstClientes.Count == 0)
        {
            VerError("No ha seleccionado ningún asociado para realizar el envio");
            Session["lstClientes"] = null;
            mvPrincipal.SetActiveView(ViewListado);
        }
        else
            Session["lstClientes"] = lstClientes;
        return true;
    }

    protected void Actualizar()
    {
        try
        {
            Site toolBar = (Site)this.Master;
            string filtro = ObtenerFiltro();
            List<EstadoCuenta> lstClientes = new List<EstadoCuenta>();
            lstClientes = serviceEstadoCuenta.ListarClientes(filtro, Convert.ToDateTime(txtFechaCorte.Texto), Convert.ToDateTime(txtFechaGeneracion.Texto), (Usuario)Session["usuario"]);
            Session["EstadosDeCuenta"] = lstClientes;
            gvLista.EmptyDataText = emptyQuery;
            toolBar.MostrarExportar(true);
            if (lstClientes.Count > 0)
            {
                btnEnviar.Visible = true;
                panelLista.Visible = true;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstClientes.Count.ToString();
                gvLista.DataSource = lstClientes;
                gvLista.DataBind();

            }
            else
            {
                btnEnviar.Visible = false;
                panelLista.Visible = false;
                VerError("Su consulta no obtuvo ningun resultado.");
                lblTotalRegs.Visible = false;
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("EnvioMasivo", "Actualizar", ex);
        }
    }

    /// <summary>
    /// Realizar el envio de los emails de todos los asociados seleccionados
    /// </summary>
    protected void EnviarCorreos()
    {
        List<EstadoCuenta> lstPersonas = new List<EstadoCuenta>();
        List<ErroresCargaAportes> lstErrores = new List<ErroresCargaAportes>();
        lstPersonas = (List<EstadoCuenta>)Session["lstClientes"];
        int cont = 0;
        try
        {
            //Obtiene datos para el envío del correo            
            Xpinn.Comun.Entities.Formato_Notificacion noti = new Xpinn.Comun.Entities.Formato_Notificacion(15);
            noti = COServices.ConsultarDatosEnvio(noti, (Usuario)Session["usuario"]);
            var Fecha = ParsFecha(txtFechaCorte.Texto);
            noti.fecha_consulta = Fecha;
            var lstClientes = lstPersonas.Where(g => g.email != null && g.email != "0" && g.email != "&nbsp;").ToList();
            foreach (EstadoCuenta estado in lstClientes)
            {
                cont++;
                noti.emailReceptor = null;
                noti.texto = noti.textoBase;
                noti.adjunto = null;
                noti.cod_persona = 0;
                if (!string.IsNullOrEmpty(estado.email) && estado.email != "0" && estado.email != "&nbsp;")
                {
                    ObtenerEstadoCuenta(estado.Codigo);
                    noti.cod_persona = Convert.ToInt32(estado.Codigo);
                    noti.emailReceptor = estado.email;
                    noti.texto = noti.texto.Replace("nombrecompletoPersona", estado.nombre_titular);
                    bool error = EnviarDocumento(noti, false);
                    Session["aporte"] = null;
                    Session["credito"] = null;
                    Session["acodeudados"] = null;
                    Session["ahorro_vista"] = null;
                    Session["programado"] = null;
                    Session["cdats"] = null;
                    Session["devoluciones"] = null;
                    Session["servicios"] = null;
                    Session["telefonia"] = null;
                    if (!error) //Agregar en lista de errores si se produjo un error en el envío
                    {
                        ErroresCargaAportes registro = new ErroresCargaAportes();
                        registro.numero_registro = cont.ToString();
                        registro.datos = "Email";
                        registro.error = "Error en el envio del correo para el asociado código Nro. " + estado.Codigo;
                        lstErrores.Add(registro);
                    }
                }
                else
                {
                    ErroresCargaAportes registro = new ErroresCargaAportes();
                    registro.numero_registro = cont.ToString();
                    registro.datos = "Email";
                    registro.error = "Error: el cliente código No. " + estado.Codigo + " no tiene email registrado";
                    lstErrores.Add(registro);
                }
            }
            if (lstErrores.Count == 0)
            {
                //mvPrincipal.SetActiveView(ViewFinal);
                Session["lsterrores"] = null;
            }
            Session["mensajeFinal"] = "Se enviaron " + cont + " estados de cuenta correctamente";
            Session["lsterrores"] = lstErrores;
            Session["Proceso"] = "FINAL";

        }
        catch (Exception ex)
        {
            VerError("Ha ocurrido un error en el envio" + ex.Message.ToString());
            Session["Proceso"] = "FINAL";
            TerminarProceso();
        }
    }

    string ObtenerFiltro()
    {
        string filtro = "";
        //Valida de que el campo de identificacion no venga nulo o vacio. 
        if (!string.IsNullOrEmpty(txtIdentificacion.Text))
            filtro = "And P.identificacion = '" + txtIdentificacion.Text + "'";

        return filtro;
    }

    #region Procesar información y el archivo del estado de cuenta
    protected void ObtenerEstadoCuenta(Int64 cod_cliente)
    {
        try
        {
            String filtro = "";
            //Consultar datos de afiliación
            producto.Persona.IdPersona = cod_cliente;
            producto.Persona = serviceEstadoCuenta.ConsultarPersona(producto.Persona.IdPersona, (Usuario)Session["usuario"]);
            Session["afiliacion"] = producto;

            //Listar Movimiento Aportes
            MovimientoAportes(cod_cliente);
            //Listar Movimiento de Créditos
            MovimientoCreditos(cod_cliente);
            //Listar Movimiento de Ahorros a la Vista
            MovimientoAhorrosVista(cod_cliente);
            //Listar Movimiento de Ahorro Programado
            MovimientoProgramado(cod_cliente);
            //Listar Movimiento de CDATS
            MovimientoCDATS(cod_cliente);
            //Listar Movimiento de Servicios
            MovimientoServicios(cod_cliente);

            //Créditos acodeudados
            List<Acodeudados> lstAcodeudados = new List<Acodeudados>();
            AcodeudadoService acodeudadosservices = new AcodeudadoService();
            Cliente cliente = new Cliente { IdCliente = cod_cliente };
            lstAcodeudados = acodeudadosservices.ListarAcodeudadoss(cliente, (Usuario)Session["usuario"]);
            Session["acodeudados"] = lstAcodeudados;
        }
        catch (Exception ex)
        {
            VerError("No obtuvo el estado de cuenta" + ex.Message.ToString() + cod_cliente);
        }
    }

    void MovimientoAportes(Int64 cod_cliente)
    {
        try
        {
            Xpinn.Aportes.Services.AporteServices aporteServicio = new Xpinn.Aportes.Services.AporteServices();
            Aporte vAporte = new Aporte();
            List<Aporte> lstAporte = new List<Aporte>();
            List<Aporte> lstProductos = new List<Aporte>();
            Aporte pAportes = new Aporte();
            pAportes.cod_persona = cod_cliente;
            lstAporte = aporteServicio.ListarEstadoCuentaAportestodos(cod_cliente, "1,2", Convert.ToDateTime(txtFechaCorte.Texto), (Usuario)Session["usuario"]);
            if (lstAporte.Count > 0)
            {
                foreach (Aporte obj in lstAporte)
                {
                    List<MovimientoAporte> lstA = new List<MovimientoAporte>();
                    lstA = aporteServicio.ListarMovAporte(obj.numero_aporte, Convert.ToDateTime(txtFechaGeneracion.ToDateTime.ToString(conf.ObtenerFormatoFecha())).AddMonths(-12), Convert.ToDateTime(txtFechaGeneracion.ToDateTime.ToString(conf.ObtenerFormatoFecha())), (Usuario)Session["usuario"]);

                    MovimientoAporte movimiento = new MovimientoAporte();
                    if (lstA.Count > 0)
                    {
                        movimiento = lstA.Where(x => x.FechaPago == lstA.Max(y => y.FechaPago)).FirstOrDefault();
                        obj.fecha_ultimo_pago = movimiento.FechaPago;
                        obj.fecha_ultimo_pago = obj.fecha_ultimo_pago > Convert.ToDateTime(txtFechaGeneracion.Texto) ? DateTime.MinValue : obj.fecha_ultimo_pago;
                    }
                    if (lstA.Count > 0 && obj.Saldo > 0)
                        lstProductos.Add(obj);
                }
            }
            Session["aporte"] = lstProductos;
        }
        catch (Exception ex)
        {
            VerError("Error en la consulta de aportes" + ex.Message);
        }
    }

    void MovimientoCreditos(Int64 cod_cliente)
    {
        DetalleProductoService detCreditoServicio = new DetalleProductoService();
        EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
        Producto pEntityProducto = new Producto();
        List<MovimientoProducto> lstCreditos = new List<MovimientoProducto>();
        List<Producto> lstConsulta = new List<Producto>();
        List<Producto> lstProductos = new List<Producto>();
        MovimientoProducto det = new MovimientoProducto();
        List<MovimientoProducto> lstCre = new List<MovimientoProducto>();

        pEntityProducto.Persona.IdPersona = cod_cliente;
        producto.Cuota = 1;
        String FiltroFinal = " (v.estado Like 'ATRASADO%' Or v.estado= 'ESTA AL DIA' Or v.estado= 'DESEMBOLSADO' Or v.estado= 'TERMINADO') and h.Fecha_Historico = To_date('" + Convert.ToDateTime(txtFechaCorte.Texto).ToString("dd/MM/yyyy") + "', 'DD/MM/YYYY')";
        lstConsulta = serviceEstadoCuenta.ListarProductosPorEstados(producto, (Usuario)Session["usuario"], FiltroFinal);

        foreach (Producto vProducto in lstConsulta)
        {
            pEntityProducto.Persona.IdPersona = cod_cliente;
            pEntityProducto.CodRadicacion = vProducto.CodRadicacion;

            lstCreditos = detCreditoServicio.ListarMovCreditos(Convert.ToInt64(pEntityProducto.CodRadicacion), (Usuario)Session["usuario"], 1);
            if (lstCreditos.Count > 0)
            {
                lstCre = lstCreditos.Where(x => x.FechaPago >= Convert.ToDateTime(txtFechaGeneracion.Texto).AddMonths(-12) && x.FechaPago <= Convert.ToDateTime(txtFechaGeneracion.Texto)).ToList();
                det = lstCre.Where(x => x.FechaPago == lstCre.Max(y => y.FechaPago)).FirstOrDefault();
                if (det != null)
                {
                    vProducto.FechaPago = det.FechaPago != null ? det.FechaPago : DateTime.MinValue;
                    vProducto.FechaPago = vProducto.FechaPago > Convert.ToDateTime(txtFechaGeneracion.Texto) ? DateTime.MinValue : vProducto.FechaPago;
                }
            }

            if ((lstCre.Count > 0 && vProducto.SaldoCapital > 0) && (vProducto.Estado != "TERMINADO" || vProducto.Estado != "NEGADO"))
                lstProductos.Add(vProducto);
        }

        Session["credito"] = lstProductos;
    }

    void MovimientoAhorrosVista(Int64 cod_cliente)
    {
        Xpinn.Ahorros.Services.ReporteMovimientoServices ReporteMovService = new Xpinn.Ahorros.Services.ReporteMovimientoServices();
        List<Xpinn.Ahorros.Entities.ReporteMovimiento> lstMovAhorroV = new List<Xpinn.Ahorros.Entities.ReporteMovimiento>();
        List<Xpinn.Ahorros.Entities.AhorroVista> lstAhorro = new List<Xpinn.Ahorros.Entities.AhorroVista>();
        List<Xpinn.Ahorros.Entities.AhorroVista> lstProductos = new List<Xpinn.Ahorros.Entities.AhorroVista>();
        string filtro = " WHERE A.COD_PERSONA = " + cod_cliente + " AND A.ESTADO IN (0,1,2,4)  and h.Fecha_Historico = To_date('" + Convert.ToDateTime(txtFechaCorte.Texto).ToString("dd/MM/yyyy") + "', 'DD/MM/YYYY')";
        lstAhorro = ReporteMovService.ListarAhorroVista(filtro, DateTime.MinValue, (Usuario)Session["usuario"]);

        foreach (Xpinn.Ahorros.Entities.AhorroVista cuenta in lstAhorro)
        {
            List<Xpinn.Ahorros.Entities.ReporteMovimiento> lstMovAhorro = new List<Xpinn.Ahorros.Entities.ReporteMovimiento>();
            lstMovAhorro = ReporteMovService.ListarReporteMovimiento(Convert.ToInt64(cuenta.numero_cuenta), Convert.ToDateTime(txtFechaGeneracion.Text).AddMonths(-12), Convert.ToDateTime(txtFechaGeneracion.Text), (Usuario)Session["usuario"]);

            Xpinn.Ahorros.Entities.ReporteMovimiento movimiento = new Xpinn.Ahorros.Entities.ReporteMovimiento();
            if (lstMovAhorro.Count > 0)
            {
                movimiento = lstMovAhorro.Where(x => x.fecha == lstMovAhorro.Max(y => y.fecha)).FirstOrDefault();
                cuenta.fecha_ultimo_pago = movimiento.fecha;
                cuenta.fecha_ultimo_pago = cuenta.fecha_ultimo_pago > Convert.ToDateTime(txtFechaGeneracion.Texto) ? DateTime.MinValue : cuenta.fecha_ultimo_pago;
                cuenta.saldo_total = movimiento.saldo;
            }

            if (cuenta.fecha_apertura < Convert.ToDateTime(txtFechaCorte.Texto) && lstMovAhorro.Count > 0 && cuenta.saldo_total > 0)
                lstProductos.Add(cuenta);

        }
        Session["ahorro_vista"] = lstProductos;
    }

    void MovimientoProgramado(Int64 cod_cliente)
    {
        Xpinn.Programado.Services.MovimientoCuentasServices ServicioProgramdo = new Xpinn.Programado.Services.MovimientoCuentasServices();
        List<Xpinn.Programado.Entities.CuentasProgramado> lstProgramado = new List<Xpinn.Programado.Entities.CuentasProgramado>();
        List<Xpinn.Programado.Entities.CuentasProgramado> lstProductos = new List<Xpinn.Programado.Entities.CuentasProgramado>();
        List<Xpinn.Ahorros.Entities.ReporteMovimiento> lstMovProgramado = new List<Xpinn.Ahorros.Entities.ReporteMovimiento>();
        string filtro = " WHERE A.COD_PERSONA = " + cod_cliente + @"  AND h.fecha_historico = To_date('" +
                        Convert.ToDateTime(txtFechaCorte.Texto).ToString("dd/MM/yyyy") + "', 'DD/MM/YYYY')";
        lstProgramado = ServicioProgramdo.ListarAhorrosProgramado(filtro, DateTime.MinValue, (Usuario)Session["usuario"]);
        foreach (Xpinn.Programado.Entities.CuentasProgramado cuenta in lstProgramado.Where(x => x.nom_estado != "TERMINADA"))
        {
            List<Xpinn.Ahorros.Entities.ReporteMovimiento> lstConsulta = new List<Xpinn.Ahorros.Entities.ReporteMovimiento>();
            lstConsulta = ServicioProgramdo.ListarDetalleMovimiento(cuenta.numero_programado, Convert.ToDateTime(txtFechaGeneracion.Text).AddMonths(-12), Convert.ToDateTime(txtFechaGeneracion.Text), (Usuario)Session["usuario"]);

            Xpinn.Ahorros.Entities.ReporteMovimiento movimiento = new Xpinn.Ahorros.Entities.ReporteMovimiento();
            if (lstConsulta.Count > 0)
            {
                movimiento = lstConsulta.Where(x => x.fecha == lstConsulta.Max(y => y.fecha)).FirstOrDefault();
                cuenta.fecha_ultimo_pago = cuenta.fecha_ultimo_pago;
                cuenta.saldo_total = cuenta.saldo;
            }

            if (lstConsulta.Count > 0 && cuenta.saldo_total > 0)
                lstProductos.Add(cuenta);

        }
        Session["programado"] = lstProductos;
    }

    void MovimientoCDATS(Int64 cod_cliente)
    {
        Xpinn.CDATS.Services.ReporteMovimientoServices ServicioCDAT = new Xpinn.CDATS.Services.ReporteMovimientoServices();
        List<Xpinn.CDATS.Entities.Cdat> lstCDAT = new List<Xpinn.CDATS.Entities.Cdat>();
        List<Xpinn.CDATS.Entities.Cdat> lstProductos = new List<Xpinn.CDATS.Entities.Cdat>();
        List<Xpinn.CDATS.Entities.ReporteMovimiento> lstMovCDAT = new List<Xpinn.CDATS.Entities.ReporteMovimiento>();
        string filtro = " WHERE T.COD_PERSONA = " + cod_cliente +
                        " AND A.ESTADO IN (1,2,3) AND H.FECHA_HISTORICO =  To_date('" +
                        Convert.ToDateTime(txtFechaCorte.Texto).ToString("dd/MM/yyyy") + "', 'DD/MM/YYYY')";
        lstCDAT = ServicioCDAT.ListarCdat(filtro, DateTime.MinValue, (Usuario)Session["usuario"]);
        foreach (Xpinn.CDATS.Entities.Cdat cuenta in lstCDAT.Where(x => x.nom_estado != "TERMINADO").ToList())
        {
            List<Xpinn.CDATS.Entities.ReporteMovimiento> lstConsulta = new List<Xpinn.CDATS.Entities.ReporteMovimiento>();
            lstConsulta = ServicioCDAT.ListarReporteMovimiento(Convert.ToInt64(cuenta.numero_cdat), Convert.ToDateTime(txtFechaGeneracion.Text).AddMonths(-12), Convert.ToDateTime(txtFechaGeneracion.Text), (Usuario)Session["usuario"]);

            Xpinn.CDATS.Entities.ReporteMovimiento movimiento = new Xpinn.CDATS.Entities.ReporteMovimiento();
            if (lstConsulta.Count > 0)
            {
                movimiento = lstConsulta.Where(x => x.fecha == lstConsulta.Max(y => y.fecha)).FirstOrDefault();
                cuenta.fecha_intereses = movimiento.fecha;
                cuenta.fecha_intereses = cuenta.fecha_intereses > Convert.ToDateTime(txtFechaGeneracion.Texto) ? DateTime.MinValue : cuenta.fecha_intereses;
            }

            if (lstConsulta.Count > 0 || cuenta.valor > 0)
                lstProductos.Add(cuenta);
        }
        Session["cdats"] = lstProductos;
    }

    void MovimientoServicios(Int64 cod_cliente)
    {
        List<Xpinn.Servicios.Entities.Servicio> lstServicios = new List<Xpinn.Servicios.Entities.Servicio>();
        List<Xpinn.Servicios.Entities.Servicio> lstProductos = new List<Xpinn.Servicios.Entities.Servicio>();
        Xpinn.Servicios.Services.AprobacionServiciosServices ExcluServicios = new Xpinn.Servicios.Services.AprobacionServiciosServices();
        string filtro = " AND S.COD_PERSONA = " + cod_cliente + " AND H.ESTADO IN ('C')   AND h.fecha_historico = To_date('" +
                        Convert.ToDateTime(txtFechaCorte.Texto).ToString("dd/MM/yyyy") + "', 'DD/MM/YYYY')";
        lstServicios = ExcluServicios.ListarServicios(filtro, "", DateTime.MinValue, (Usuario)Session["usuario"]);

        foreach (Xpinn.Servicios.Entities.Servicio cuenta in lstServicios)
        {
            Xpinn.Servicios.Entities.Servicio reportemovimiento = new Xpinn.Servicios.Entities.Servicio();
            List<Xpinn.Servicios.Entities.Servicio> lstMovSer = new List<Xpinn.Servicios.Entities.Servicio>();
            reportemovimiento.Fec_ini = Convert.ToDateTime(txtFechaGeneracion.Texto).AddMonths(-1);
            reportemovimiento.Fec_fin = Convert.ToDateTime(txtFechaGeneracion.Texto);
            reportemovimiento.numero_servicio = cuenta.numero_servicio;

            lstMovSer = ExcluServicios.Reportemovimiento(reportemovimiento, (Usuario)Session["usuario"]);

            Xpinn.Servicios.Entities.Servicio movimiento = new Xpinn.Servicios.Entities.Servicio();
            if (lstMovSer.Count > 0)
            {
                movimiento = lstMovSer.Where(x => x.Fec_1Pago == lstMovSer.Max(y => y.Fec_1Pago)).FirstOrDefault();
                cuenta.fecha_ultimo_pago = movimiento.Fec_1Pago;
                cuenta.fecha_ultimo_pago = cuenta.fecha_ultimo_pago > Convert.ToDateTime(txtFechaGeneracion.Texto) ? DateTime.MinValue : cuenta.fecha_ultimo_pago;
            }

            var dateFist = new DateTime(Convert.ToDateTime(txtFechaCorte.Texto).Year,
                Convert.ToDateTime(txtFechaCorte.Texto).Month, 1);
            if (lstMovSer.Count > 0 || cuenta.saldo > 0)
                lstProductos.Add(cuenta);
        }
        Session["servicios"] = lstProductos;
    }

    /// <summary>
    /// Generar el estado de cuenta y enviar por email
    /// </summary>
    /// <param name="noti"></param>
    /// <param name="VerReporte"></param>
    /// <returns></returns>
    protected bool EnviarDocumento(Formato_Notificacion noti, bool VerReporte)
    {
        try
        {
            string separadorDecimal = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
            string alerta = "";
            //Definición del reporte 
            ReportViewer reporte = new ReportViewer();
            reporte.ID = "RpviewEstado";reporte.Font.Name = "Verdana";
            reporte.Font.Size = 8;
            reporte.Visible = false;
            reporte.Height = 500;
            reporte.WaitMessageFont.Name = "Verdana";
            reporte.EnableViewState = true;

            // Crear datatable para los aportes
            DataTable tableAporte = new DataTable();
            tableAporte.Columns.Add("numero_aporte");
            tableAporte.Columns.Add("linea");
            tableAporte.Columns.Add("fecha_apertura");
            tableAporte.Columns.Add("fecha_ult_pago");
            tableAporte.Columns.Add("Fecha_prox_pago");
            tableAporte.Columns.Add("saldo");
            tableAporte.Columns.Add("valor_cuota");

            List<Aporte> lstAporte = new List<Aporte>();
            lstAporte = (List<Aporte>)Session["aporte"];
            int contador = 0;
            if (lstAporte != null)
            {
                if (lstAporte.Count > 0)
                {
                    foreach (Aporte item in lstAporte)
                    {
                        DataRow data;

                        data = tableAporte.NewRow();
                        data[0] = item.numero_aporte;
                        data[1] = item.nom_linea_aporte;
                        data[2] = item.fecha_apertura.ToString("dd/MM/yyyy");
                        data[3] = item.fecha_ultimo_pago != DateTime.MinValue ? item.fecha_ultimo_pago.ToString("dd/MM/yyyy") : "";
                        data[4] = item.fecha_proximo_pago != DateTime.MinValue ? item.fecha_proximo_pago.ToString("dd/MM/yyyy") : "";
                        data[5] = Convert.ToDecimal(item.Saldo).ToString();
                        data[6] = Convert.ToDecimal(item.cuota).ToString();

                        tableAporte.Rows.Add(data);

                        if (item.fecha_ultimo_pago < Convert.ToDateTime(txtFechaGeneracion.Texto).AddMonths(-1) && contador == 0)
                        {
                            alerta = "El cliente tiene mora en aportes" + "\n\r";
                            contador++;
                        }
                    }
                }
            }

            //Créditos
            DataTable tableCredito = new DataTable();

            tableCredito.Columns.Add("NumRadicacion");
            tableCredito.Columns.Add("Linea");
            tableCredito.Columns.Add("FechaDesembolso");
            tableCredito.Columns.Add("SaldoCapital");
            tableCredito.Columns.Add("Cuota");
            tableCredito.Columns.Add("FechaUltPago");
            tableCredito.Columns.Add("Fecha_prox_pago");

            List<Producto> lstCredito = new List<Producto>();
            lstCredito = (List<Producto>)Session["credito"];
            contador = 0;
            if (lstCredito != null)
            {
                if (lstCredito.Count > 0)
                {
                    foreach (Producto item in lstCredito)
                    {
                        //Acodeudados variable = new Acodeudados();
                        DataRow datarw;

                        datarw = tableCredito.NewRow();
                        datarw[0] = item.NumRadicion;
                        datarw[1] = item.Linea;
                        datarw[2] = item.FechaDesembolso.ToString("dd/MM/yyyy");
                        datarw[3] = Convert.ToDecimal(item.SaldoCapital).ToString();
                        datarw[4] = Convert.ToDecimal(item.Cuota).ToString();
                        datarw[5] = item.FechaPago != DateTime.MinValue ? item.FechaPago.ToString("dd/MM/yyyy") : "";
                        datarw[6] = item.FechaProximoPago != DateTime.MinValue ? item.FechaProximoPago.ToString("dd/MM/yyyy") : "";
                        tableCredito.Rows.Add(datarw);

                        if (item.FechaPago < Convert.ToDateTime(txtFechaGeneracion.Texto).AddMonths(-1) && contador == 1)
                        {
                            alerta = "El cliente tiene mora en créditos" + "\n\r";
                            contador++;
                        }
                    }
                }
            }


            ///cargar acodeudados
            DataTable tableAcodeudados = new DataTable();
            tableAcodeudados.Columns.Add("fecha_prox_pago");
            tableAcodeudados.Columns.Add("NumRadicacion");
            tableAcodeudados.Columns.Add("CodPersona");
            tableAcodeudados.Columns.Add("Estado");
            tableAcodeudados.Columns.Add("Linea");
            tableAcodeudados.Columns.Add("Nombre");
            tableAcodeudados.Columns.Add("Montos");
            tableAcodeudados.Columns.Add("Saldos");
            tableAcodeudados.Columns.Add("Cuotas");
            tableAcodeudados.Columns.Add("Valor_apagar");
            tableAcodeudados.Columns.Add("identificacion");

            List<Acodeudados> lstAcodeudados = new List<Acodeudados>();
            lstAcodeudados = (List<Acodeudados>)Session["acodeudados"];
            if (lstAcodeudados != null)
            {
                if (lstAcodeudados.Count > 0)
                {
                    foreach (Acodeudados asc in lstAcodeudados)
                    {
                        DataRow datas;
                        datas = tableAcodeudados.NewRow();
                        datas[0] = Convert.ToDateTime(asc.FechaProxPago).ToString("dd/MM/yyyy");
                        datas[3] = asc.CodPersona;
                        datas[5] = asc.Nombres;
                        datas[6] = Convert.ToDecimal(asc.Monto).ToString();
                        datas[7] = Convert.ToDecimal(asc.Saldo).ToString();
                        datas[8] = Convert.ToDecimal(asc.Cuota).ToString();
                        datas[9] = Convert.ToDecimal(asc.Valor_apagar).ToString();
                        datas[10] = asc.identificacion;
                        tableAcodeudados.Rows.Add(datas);
                    }
                }
            }

            //Datatable CDATS
            DataTable tableCdat = new DataTable();
            tableCdat.Columns.Add("numero_cdat");
            tableCdat.Columns.Add("linea");
            tableCdat.Columns.Add("fecha_apertura");
            tableCdat.Columns.Add("fecha_ult_pago");
            tableCdat.Columns.Add("Intereses");
            tableCdat.Columns.Add("valor");

            List<Xpinn.CDATS.Entities.Cdat> lstCdat = new List<Xpinn.CDATS.Entities.Cdat>();
            lstCdat = (List<Xpinn.CDATS.Entities.Cdat>)Session["cdats"];
            if (lstCdat != null)
            {
                if (lstCdat.Count > 0)
                {
                    foreach (Xpinn.CDATS.Entities.Cdat item in lstCdat)
                    {
                        DataRow data;
                        data = tableCdat.NewRow();
                        data[0] = item.numero_cdat;
                        data[1] = item.nomlinea;
                        data[2] = item.fecha_apertura != DateTime.MinValue ? item.fecha_apertura.ToString("dd/MM/yyyy") : " ";
                        data[3] = item.fecha_intereses != DateTime.MinValue ? item.fecha_intereses.ToString("dd/MM/yyyy") : "";
                        data[4] = Math.Round(item.intereses_cap);
                        data[5] = item.valor;

                        tableCdat.Rows.Add(data);
                    }
                }
            }

            //Datatable Servicios
            DataTable tableServi = new DataTable();
            tableServi.Columns.Add("numero_servicio");
            tableServi.Columns.Add("linea");
            tableServi.Columns.Add("fecha_inicio");
            tableServi.Columns.Add("vr_cuota");
            tableServi.Columns.Add("saldo");
            tableServi.Columns.Add("fecha_ult_pago");
            tableServi.Columns.Add("Fecha_prox_pago");

            List<Xpinn.Servicios.Entities.Servicio> lstServicio = new List<Xpinn.Servicios.Entities.Servicio>();
            lstServicio = (List<Xpinn.Servicios.Entities.Servicio>)Session["servicios"];
            if (lstServicio != null)
            {
                if (lstServicio.Count > 0)
                {
                    foreach (Xpinn.Servicios.Entities.Servicio item in lstServicio)
                    {
                        DataRow data;
                        data = tableServi.NewRow();
                        data[0] = item.numero_servicio;
                        data[1] = item.nom_linea;
                        data[2] = Convert.ToDateTime(item.fecha_inicio_vigencia).ToString("dd/MM/yyyy");
                        data[3] = item.valor_cuota;
                        data[4] = item.saldo;
                        data[5] = item.fecha_ultimo_pago.ToString("dd/MM/yyyy");
                        data[6] = Convert.ToDateTime(item.fecha_proximo_pago.ToString()).ToString("dd/MM/yyyy");
                        tableServi.Rows.Add(data);
                    }
                }
            }

            // Cargar en un datatable la información de los ahorros a la vista
            DataTable tableAhorro = new DataTable();
            tableAhorro.Columns.Add("numero_cuenta");
            tableAhorro.Columns.Add("linea");
            tableAhorro.Columns.Add("fecha_apertura");
            tableAhorro.Columns.Add("saldo_total");
            tableAhorro.Columns.Add("valor_cuota");
            tableAhorro.Columns.Add("fecha_ult_pago");


            List<Xpinn.Ahorros.Entities.AhorroVista> lstAhorros = new List<Xpinn.Ahorros.Entities.AhorroVista>();
            lstAhorros = (List<Xpinn.Ahorros.Entities.AhorroVista>)Session["ahorro_vista"];
            if (lstAhorros != null)
            {
                if (lstAhorros.Count > 0)
                {
                    foreach (Xpinn.Ahorros.Entities.AhorroVista itemAh in lstAhorros)
                    {
                        DataRow dataA;
                        dataA = tableAhorro.NewRow();
                        dataA[0] = itemAh.numero_cuenta;
                        dataA[1] = itemAh.nom_linea;
                        dataA[2] = Convert.ToDateTime(itemAh.fecha_apertura).ToString("dd/MM/yyyy");
                        dataA[3] = Convert.ToDecimal(itemAh.saldo_total);
                        dataA[4] = Convert.ToDecimal(itemAh.valor_cuota);
                        dataA[5] = Convert.ToDateTime(itemAh.fecha_ultimo_pago) != DateTime.MinValue ? Convert.ToDateTime(itemAh.fecha_ultimo_pago).ToString("dd/MM/yyyy") : "";
                        tableAhorro.Rows.Add(dataA);
                    }
                }
            }

            // Cargar en un datatable la información de los ahorros programados
            DataTable tableAhoPro = new DataTable();
            tableAhoPro.Columns.Add("numero_programado");
            tableAhoPro.Columns.Add("linea");
            tableAhoPro.Columns.Add("fecha_apertura");
            tableAhoPro.Columns.Add("valor_cuota");
            tableAhoPro.Columns.Add("fecha_ult_mov");
            tableAhoPro.Columns.Add("saldo_total");


            List<Xpinn.Programado.Entities.CuentasProgramado> lstAhoPro = new List<Xpinn.Programado.Entities.CuentasProgramado>();
            lstAhoPro = (List<Xpinn.Programado.Entities.CuentasProgramado>)Session["programado"];
            if (lstAhoPro != null)
            {
                if (lstAhoPro.Count > 0)
                {
                    foreach (Xpinn.Programado.Entities.CuentasProgramado itemAh in lstAhoPro)
                    {
                        DataRow dataAP;
                        dataAP = tableAhoPro.NewRow();
                        dataAP[0] = itemAh.numero_programado;
                        dataAP[1] = itemAh.nomlinea;
                        dataAP[2] = itemAh.fecha_apertura.ToString("dd/MM/yyyy");
                        dataAP[3] = Convert.ToDecimal(itemAh.valor_cuota);
                        dataAP[4] = itemAh.fecha_ultimo_pago != DateTime.MinValue ? itemAh.fecha_ultimo_pago.ToString("dd/MM/yyyy") : "";
                        dataAP[5] = Convert.ToDecimal(itemAh.saldo_total);
                        tableAhoPro.Rows.Add(dataAP);
                    }
                }
            }

            // Determinar el logo de la empresa
            string cRutaDeImagen;
            cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

            // Cargar los parámetros del reporte
            Usuario pUsu = (Usuario)Session["usuario"];
            Producto objProducto = (Producto)Session["afiliacion"];
            ReportParameter[] param = new ReportParameter[21];
            param[0] = new ReportParameter("Entidad", pUsu.empresa);
            param[1] = new ReportParameter("NumeroDocumento", " " + producto.Persona.NumeroDocumento);
            param[2] = new ReportParameter("IdPersona", " " + producto.Persona.IdPersona);
            param[3] = new ReportParameter("nombre", producto.Persona.PrimerNombre + " " + producto.Persona.PrimerApellido);
            param[4] = new ReportParameter("fechaingreso", producto.Persona.FechaAfiliacion != null ? producto.Persona.FechaAfiliacion.ToString("dd/MM/yyyy") : " ");
            param[5] = new ReportParameter("tipocliente", producto.Persona.EstadoAfiliacion);
            param[6] = new ReportParameter("telefono", producto.Persona.Telefono);
            param[7] = new ReportParameter("direccion", producto.Persona.Direccion);

            List<PersonaEmpresaRecaudo> LstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
            PersonaEmpresaRecaudoServices infoEmpresaRecaudo = new PersonaEmpresaRecaudoServices();
            LstEmpresaRecaudo = infoEmpresaRecaudo.ListarPersonaEmpresaRecaudo(producto.Persona.IdPersona, (Usuario)Session["usuario"]);
            string cadena = "", newPagaduria = "";
            foreach (PersonaEmpresaRecaudo rFila in LstEmpresaRecaudo)
            {
                if (rFila.idempresarecaudo != 0 && rFila.idempresarecaudo != null)
                    if (rFila.descripcion != "")
                        cadena = cadena + rFila.descripcion + " - ";
            }
            if (cadena != "")
                newPagaduria = cadena.Substring(0, cadena.Length - 3);

            param[8] = new ReportParameter("tipoidentificacion", newPagaduria != null ? newPagaduria : " ");
            param[9] = new ReportParameter("pFecha", ParsFecha(txtFechaGeneracion.Text).ToString());
            param[10] = new ReportParameter("ReporteAporte", lstAporte != null && lstAporte.Count > 0 ? "False" : "True");
            param[11] = new ReportParameter("fechahora", " " + ParsFecha(txtFechaGeneracion.Texto).ToString());
            param[12] = new ReportParameter("usuarioGenera", " " + pUsu.nombre);
            param[13] = new ReportParameter("ImagenReport", cRutaDeImagen);
            param[14] = new ReportParameter("ReporteAhorro", lstAhorros != null && lstAhorros.Count > 0 ? "False" : "True");
            param[15] = new ReportParameter("ReporteAhoPro", lstAhoPro != null && lstAhoPro.Count > 0 ? "False" : "True");
            param[16] = new ReportParameter("ReporteAcodeudados", lstAcodeudados != null && lstAcodeudados.Count > 0 ? "False" : "True");
            param[17] = new ReportParameter("CiudadDireccion", " " + producto.Persona.Ciudad.nomciudad);
            param[18] = new ReportParameter("ReporteCdat", lstCdat != null && lstCdat.Count > 0 ? "False" : "True");
            param[19] = new ReportParameter("ReporteServi", lstServicio != null && lstServicio.Count > 0 ? "False" : "True");
            param[20] = new ReportParameter("ReporteCreditos", lstCredito != null && lstCredito.Count > 0 ? "False" : "True");

            string rutaReporte = Server.MapPath("~/Page/Asesores/EnvioMasivoEstadoCuenta/ReporteEstadoCuentaXFecha.rdlc");
            if (!VerReporte)
            {
                reporte.LocalReport.ReportPath = rutaReporte;
                reporte.LocalReport.EnableExternalImages = true;
                reporte.LocalReport.SetParameters(param);

                reporte.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DT_Creditos", tableCredito);
                reporte.LocalReport.DataSources.Add(rds);
                ReportDataSource rds1 = new ReportDataSource("DT_Aportes", tableAporte);
                reporte.LocalReport.DataSources.Add(rds1);
                ReportDataSource rds4 = new ReportDataSource("DT_AVista", tableAhorro);
                reporte.LocalReport.DataSources.Add(rds4);
                ReportDataSource rds5 = new ReportDataSource("DT_AProgramado", tableAhoPro);
                reporte.LocalReport.DataSources.Add(rds5);
                ReportDataSource rds6 = new ReportDataSource("DT_Acodeudados", tableAcodeudados);
                reporte.LocalReport.DataSources.Add(rds6);
                ReportDataSource rds7 = new ReportDataSource("DT_Cdats", tableCdat);
                reporte.LocalReport.DataSources.Add(rds7);
                ReportDataSource rds8 = new ReportDataSource("DT_Servicios", tableServi);
                reporte.LocalReport.DataSources.Add(rds8);

                string ident = producto.Persona.NumeroDocumento != "" ? producto.Persona.NumeroDocumento : "";
                reporte.ServerReport.DisplayName = ident;
                reporte.LocalReport.DisplayName = ident;
                reporte.LocalReport.Refresh();

                var bytes = reporte.LocalReport.Render("PDF"); //Reporte en bytes
                if (bytes != null) //Enviar correo si el reporte se generó
                {
                    noti.adjunto = bytes;
                    return COServices.SendEmailExtracto(noti, (Usuario)Session["usuario"]);
                    //return EnviarCorreoAsociado(noti.nombre, noti.emailReceptor, bytes, ident, noti);                    
                }
                else
                    return false;
            }
            else
            {
                RpviewEstado.LocalReport.ReportPath = rutaReporte;
                RpviewEstado.LocalReport.EnableExternalImages = true;
                RpviewEstado.LocalReport.SetParameters(param);

                RpviewEstado.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DT_Creditos", tableCredito);
                RpviewEstado.LocalReport.DataSources.Add(rds);
                ReportDataSource rds1 = new ReportDataSource("DT_Aportes", tableAporte);
                RpviewEstado.LocalReport.DataSources.Add(rds1);
                ReportDataSource rds4 = new ReportDataSource("DT_AVista", tableAhorro);
                RpviewEstado.LocalReport.DataSources.Add(rds4);
                ReportDataSource rds5 = new ReportDataSource("DT_AProgramado", tableAhoPro);
                RpviewEstado.LocalReport.DataSources.Add(rds5);
                ReportDataSource rds6 = new ReportDataSource("DT_Acodeudados", tableAcodeudados);
                RpviewEstado.LocalReport.DataSources.Add(rds6);
                ReportDataSource rds7 = new ReportDataSource("DT_Cdats", tableCdat);
                RpviewEstado.LocalReport.DataSources.Add(rds7);
                ReportDataSource rds8 = new ReportDataSource("DT_Servicios", tableServi);
                RpviewEstado.LocalReport.DataSources.Add(rds8);

                string ident = producto.Persona.NumeroDocumento != "" ? producto.Persona.NumeroDocumento : "";
                RpviewEstado.ServerReport.DisplayName = ident;
                RpviewEstado.LocalReport.DisplayName = ident;
                RpviewEstado.LocalReport.Refresh();

                return true;
            }
        }
        catch (Exception ex)
        {
            VerError("Error en el envio del mensaje" + ex.Message);
            Session["Proceso"] = "FINAL";
            TerminarProceso();
            return false;
        }

    }

    protected bool EnviarCorreoAsociado(string nomAsociado, string emailAsociado, byte[] bytes, string ident, Formato_Notificacion noti)
    {
        try
        {
            Xpinn.Comun.Data.Formato_NotificacionData DAFormatoNotificacion = new Formato_NotificacionData();
            Usuario pUsuario = (Usuario)Session["usuario"];
            TiposDocCobranzasServices _tipoDocumentoServicio = new TiposDocCobranzasServices();

            Xpinn.Comun.Entities.Empresa empresa = _tipoDocumentoServicio.ConsultarCorreoEmpresa(pUsuario.idEmpresa, pUsuario);
            ParametroCorreo parametroCorreo = (ParametroCorreo)Enum.Parse(typeof(ParametroCorreo), ((int)TipoDocumentoCorreo.EstadoCuenta).ToString());
            TiposDocCobranzas modificardocumento = _tipoDocumentoServicio.ConsultarFormatoDocumentoCorreo((int)parametroCorreo, pUsuario);

            parametrosFormatoCorreo = new Dictionary<ParametroCorreo, string>();
            parametrosFormatoCorreo.Add(ParametroCorreo.NombreCompletoPersona, nomAsociado);
            modificardocumento.texto = ReemplazarParametrosEnElMensajeCorreo(modificardocumento.texto);
            // convert string to stream

            string perror;
            CorreoHelper correoHelper = new CorreoHelper(emailAsociado, empresa.e_mail, empresa.clave_e_mail);
            bool exitoso = correoHelper.sendEmail(bytes, modificardocumento.texto, out perror, "Estado De Cuenta"); //Se adjunta en la clase Correo Helper

            if (exitoso)
                DAFormatoNotificacion.AlmacenarHistorialEnvio(noti, usuario);
            return exitoso;
        }
        catch (Exception ex)
        {
            VerError("Error en el envio del mensaje" + ex.Message);
            return false;
        }

    }
    #endregion

    protected void gvErrores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex >= 0)
            {
                gvErrores.PageIndex = e.NewPageIndex;
                if (Session["lsterrores"] != null)
                {
                    List<ErroresCargaAportes> lstErrores = new List<ErroresCargaAportes>();
                    lstErrores = (List<ErroresCargaAportes>)Session["lsterrores"];
                    gvErrores.DataSource = lstErrores;
                    gvErrores.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("EnvioMasivo", "gvErrores_PageIndexChanging", ex);
        }
    }

    protected void chkEncabezado_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkEncabezado = (CheckBox)sender;
        if (chkEncabezado != null)
        {
            foreach (GridViewRow rFila in gvLista.Rows)
            {
                CheckBox chkEnviar = (CheckBox)rFila.FindControl("chkEnviar");
                chkEnviar.Checked = chkEncabezado.Checked;
            }
        }
    }

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int64 cod_persona = Convert.ToInt64(gvLista.Rows[gvLista.SelectedRow.RowIndex].Cells[2].Text);
        ObtenerEstadoCuenta(cod_persona);
        EnviarDocumento(null, true);
        Site toolBar = (Site)this.Master;
        toolBar.MostrarCancelar(true);
        toolBar.MostrarConsultar(false);
        mvPrincipal.SetActiveView(vReporteExtracto);
    }
    protected void btnExportar_click(object sender, EventArgs e)
    {
        List<EstadoCuenta> lstConsulta = (List<EstadoCuenta>)Session["EstadosDeCuenta"];
        if (Session["EstadosDeCuenta"] != null)
        {
            string fic = "EstadosDeCuenta.csv";
            try
            {
                File.Delete(fic);
            }
            catch
            {
            }
            // Generar el archivo
            bool bTitulos = false;
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("") + fic, true);
            var lstConsultas = from l in lstConsulta
                               select new
                               {
                                   l.Codigo,
                                   l.identificacion,
                                   l.nombre_titular,
                                   l.email,
                                   l.fecha_final_vigencia,
                                   l.fecha_final
                               };


            foreach (var item in lstConsultas)
            {
                string texto = "";
                FieldInfo[] propiedades = item.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if (!bTitulos)
                {
                    foreach (FieldInfo f in propiedades)
                    {
                        try
                        {
                            texto += f.Name.Split('>').First().Replace("<", "") + ";";
                        }
                        catch { texto += ";"; };
                    }
                    sw.WriteLine(texto);
                    bTitulos = true;
                }
                texto = "";
                int i = 0;
                foreach (FieldInfo f in propiedades)
                {
                    i += 1;
                    object valorObject = f.GetValue(item);
                    // Si no soy nulo
                    if (valorObject != null)
                    {
                        string valorString = valorObject.ToString();
                        if (valorObject is DateTime)
                        {
                            DateTime? fechaValidar = valorObject as DateTime?;
                            if (fechaValidar.Value != DateTime.MinValue)
                            {
                                texto += f.GetValue(item) + ";";
                            }
                            else
                            {
                                texto += "" + ";";
                            }
                        }
                        else
                        {
                            texto += f.GetValue(item) + ";";
                            texto.Replace("\r", "").Replace(";", "");
                        }
                    }
                    else
                    {
                        texto += "" + ";";
                    }
                }
                sw.WriteLine(texto);
            }
            sw.Close();
            System.IO.StreamReader sr;
            sr = File.OpenText(Server.MapPath("") + fic);
            string texo = sr.ReadToEnd();
            sr.Close();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "text/plain";
            HttpContext.Current.Response.Write(texo);
            HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment;filename=" + fic);
            HttpContext.Current.Response.Flush();
            File.Delete(Server.MapPath("") + fic);
            HttpContext.Current.Response.End();

        }
    }

    DateTime ParsFecha(string fecha)
    {
        return DateTime.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
    } 

}