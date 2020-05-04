using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;


public delegate void ToolBarDelegate(object sender, EventArgs e);

public partial class Site : System.Web.UI.MasterPage
{
    xpinnWSDeposito.WSDepositoSoapClient BODeposito = new xpinnWSDeposito.WSDepositoSoapClient();
    
    xpinnWSLogin.Persona1 pPersona;
    public event ToolBarDelegate eventoConsultar;
    ToolBarDelegate consultarToolbar;

    public event ToolBarDelegate eventoContinuar;
    ToolBarDelegate continuarToolbar;

    public event ToolBarDelegate eventoGuardar;
    ToolBarDelegate guardarToolbar;

    public event ToolBarDelegate eventoEliminar;
    ToolBarDelegate eliminarToolbar;

    public event ToolBarDelegate eventoLimpiar;
    ToolBarDelegate limpiarToolbar;

    public event ToolBarDelegate eventoImprimir;
    ToolBarDelegate imprimirToolbar;

    public event ToolBarDelegate eventoConsolidado;
    ToolBarDelegate consolidadoToolbar;

    public event ToolBarDelegate eventoExportar;
    ToolBarDelegate exportarToolbar;

    public event ToolBarDelegate eventoImportar;
    ToolBarDelegate importarToolbar; 

    public event ToolBarDelegate eventoCancelar;
    ToolBarDelegate cancelarToolbar;

    public event ToolBarDelegate eventoRegresar;
    ToolBarDelegate regresarToolbar;

    public event ToolBarDelegate eventoNuevo;
    ToolBarDelegate nuevoToolbar;

    xpinnWSLogin.WSloginSoapClient Acceso = new xpinnWSLogin.WSloginSoapClient();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoCuenta = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            consultarToolbar = eventoConsultar;
            continuarToolbar = eventoContinuar;
            guardarToolbar = eventoGuardar;
            eliminarToolbar = eventoEliminar;
            limpiarToolbar = eventoLimpiar;
            imprimirToolbar = eventoImprimir;
            consolidadoToolbar = eventoConsolidado;
            exportarToolbar = eventoExportar;
            importarToolbar = eventoImportar;
            cancelarToolbar = eventoCancelar;
            regresarToolbar = eventoRegresar;
            nuevoToolbar = eventoNuevo;

            if (consultarToolbar == null)
                btnConsultar.Visible = false;
            if (continuarToolbar == null)
                btnContinuar.Visible = false;
            if (guardarToolbar == null)
                btnGuardar.Visible = false;
            if (eliminarToolbar == null)
                btnEliminar.Visible = false;
            if (limpiarToolbar == null)
                btnLimpiar.Visible = false;
            if (imprimirToolbar == null)
                btnImprimir.Visible = false;
            if (consolidadoToolbar == null)
                btnConsolidado.Visible = false;
            if (exportarToolbar == null)
                btnExportar.Visible = false;
            if (importarToolbar == null)
                btnImportar.Visible = false;
            if (cancelarToolbar == null)
                btnCancelar.Visible = false;
            if (regresarToolbar == null)
                btnRegresar.Visible = false;
            if (nuevoToolbar == null)
                btnNuevo.Visible = false;

            if (!Page.IsPostBack)
            {
                if (Session["persona"] == null)
                    Response.Redirect("~/Pages/Account/FinSesion.htm");
                pPersona = (xpinnWSLogin.Persona1)Session["persona"];


                if (pPersona.nombre != null && pPersona.nombre != "")
                {
                    lblUsuario.Text = pPersona.nombre.Trim();
                    lblNombre.Text = pPersona.nombre.Trim();
                    lblUser2.Text = pPersona.nombre.Trim();
                    lblRol.Text = pPersona.email;
                    lblEmpresa.Text = pPersona.empresa;
                }
                lblNombreFooter.Text = pPersona.empresa;
                lblFecha.Text = DateTime.Now.ToLongDateString();
                //Cargando Notificaciones
                if (Session["rptaIngreso" + pPersona.identificacion] == null)
                    LoadNotications();
                else
                {
                    if (Convert.ToBoolean(Session["rptaIngreso" + pPersona.identificacion].ToString()) == true)
                    {
                        string strHtml = (string)Session["srtAlert" + pPersona.identificacion];
                        phNotification.Controls.Add(new LiteralControl(strHtml));
                    }
                }
                Session["cod_persona"] = pPersona.cod_persona;

                xpinnWSEstadoCuenta.Persona1 Persona = new xpinnWSEstadoCuenta.Persona1();
                Persona = EstadoCuenta.FechaEdad(pPersona.cod_persona, Session["sec"].ToString());
                DateTime FechaNacimiento = Convert.ToDateTime(Persona.fechanacimiento);
                string cumpleaños = FechaNacimiento.ToString("dd/MM");
                DateTime FechaActual = DateTime.Now;
                int Edad =Convert.ToInt32(Persona.Edad);
                string tipoiden = Persona.tipo_identif;
                string fecha = FechaActual.ToString("dd/MM");
                //Persona = EstadoCuenta.NotificacionConsulta(pPersona.cod_persona,1);
                if (fecha == cumpleaños)
                {
                    //Page.ClientScript.RegisterStartupScript(GetType(), "script", " javascript:Notifier('" + Persona.Texto + "');", true);


                }
                if (Edad >= 18 && tipoiden == "T.I")
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "script1", " javascript:Edad('El asociado ya es mayor de edad, Cambie el numero y tipo de identificación.');", true);
                   
                }
            }


        }
        catch(Exception ex)
        {
            lblError.Text = ex.ToString();
        }
    }


    public void LoadNotications()
    {
        pPersona = (xpinnWSLogin.Persona1)Session["persona"];
        
        //Realizando consulta
        List<xpinnWSDeposito.CuentasProgramado> lstCtaAhorroXvencer = new List<xpinnWSDeposito.CuentasProgramado>();
        string pFiltro = string.Empty;
        pFiltro = " WHERE A.ESTADO NOT IN (2,3) AND  A.COD_PERSONA = " + pPersona.cod_persona;
        pFiltro += @" AND TRUNC(SYSDATE) BETWEEN FECSUMDIA(A.FECHA_PROXIMO_PAGO,((A.PLAZO-A.CUOTAS_PAGADAS)* P.NUMERO_DIAS),P.TIPO_CALENDARIO) - NVL(BUSCARGENERAL(91,2),30)
                      AND FECSUMDIA(A.FECHA_PROXIMO_PAGO, ((A.PLAZO - A.CUOTAS_PAGADAS) * P.NUMERO_DIAS), P.TIPO_CALENDARIO)";
        
        int cantProg = 0;
        try
        {
            lstCtaAhorroXvencer = BODeposito.ListarAhorrosProgramado(pFiltro, DateTime.MinValue, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
            cantProg = lstCtaAhorroXvencer.Count;
        }
        catch
        {
            cantProg = 0;
        }

        List<xpinnWSDeposito.Cdat> lstCdatsXvencer = new List<xpinnWSDeposito.Cdat>();
        pFiltro = string.Empty;
        pFiltro = " AND C.ESTADO IN (1,2) AND  T.COD_PERSONA = " + pPersona.cod_persona;
        pFiltro += " AND TRUNC(SYSDATE) BETWEEN C.FECHA_VENCIMIENTO - NVL(BUSCARGENERAL(91,2),30) and C.FECHA_VENCIMIENTO ";
        pFiltro += " AND C.CODIGO_CDAT NOT IN (SELECT X.CODIGO_CDAT FROM SOLICITUD_RENOVACION_CDAT X WHERE X.CODIGO_CDAT = C.CODIGO_CDAT AND X.FECHA_VENCIMIENTO = C.FECHA_VENCIMIENTO) ";

        int cantCdat = 0;
        try
        {
            lstCdatsXvencer = BODeposito.ListarCdats(pFiltro, DateTime.MinValue, pPersona.identificacion, pPersona.clavesinecriptar, Session["sec"].ToString());
            cantCdat = lstCdatsXvencer.Count;
        }
        catch
        {
            cantCdat = 0;
        }
      
        int cantAprVista = 0;
        List<xpinnWSDeposito.AhorroVista> lstAproVista = new List<xpinnWSDeposito.AhorroVista>();
        try
        {
           
            lstAproVista = BODeposito.ListarAprobacionesVista(DateTime.Now, Session["sec"].ToString());
            cantAprVista = lstAproVista.Count;
        }
        catch
        {

            cantAprVista = 0;
        }
        int cantAprVistaCuota = 0;
        List<xpinnWSDeposito.AhorroVista> lstAproVistaCuota = new List<xpinnWSDeposito.AhorroVista>();
        try
        {

            lstAproVistaCuota = BODeposito.ListarAprobacionesCuota(Session["sec"].ToString());
            cantAprVistaCuota = lstAproVistaCuota.Count;
        }
        catch
        {

            cantAprVistaCuota = 0;
        }

        int cant = (cantProg + cantCdat+cantAprVista+ cantAprVistaCuota);

        //CARGANDO NOTIFICACIONES DE AHORROS
        string stHeader = string.Empty;
        if (cant == 0)
            stHeader = "Usted no tiene  Notificaciones";
        else if (cant == 1)
            stHeader = "Usted tiene " + cant + " notificación";
        else
            stHeader = "Usted tiene " + cant + " notificaciones";
        string strHtml = string.Empty;
        strHtml += "<li class='header'>" + stHeader + "</li>";
        strHtml += "<li class='text-primary text-bold text-center' style='padding-bottom:5px'>Avisos</li>";
        if (cant == 0)
            strHtml += "<div class='col-xs-12 container'>Nada nuevo en este momento</div>";
        else
        {
            string srtSpan = string.Empty;
            srtSpan += "<span id='spnBadge' class='label label-danger'>" + cant + "</span>";
            phBadgeAlerta.Controls.Add(new LiteralControl(srtSpan));
            if (lstCtaAhorroXvencer.Count > 0 || lstCdatsXvencer.Count > 0 || lstAproVista.Count>0 )
            {
                strHtml += "<li stryle='padding:2px'><ul class='menu'>";
                //PENDIENTE DE ASIGNACION DE RUTA
                foreach (xpinnWSDeposito.CuentasProgramado nCuentas in lstCtaAhorroXvencer)
                {
                    string pUrlFull = string.Empty;
                    pUrlFull = ResolveUrl("~/Pages/AhorroProgramado/Renovacion/Lista.aspx");
                    strHtml += "<li><a href='" + pUrlFull + "' style='white-space: normal;'><table style='padding:0px'><tr><td><i class='fa fa-warning text-yellow'></i></td>";
                    string pMensaje = "<td>&nbsp;Cuenta de ahorro programado [" + nCuentas.numero_programado + "] esta próxima a vencer el " + nCuentas.fecha_vencimiento.ToShortDateString() + "</td></tr></table>";
                    strHtml += pMensaje + "</a></li>";
                }
                foreach (xpinnWSDeposito.Cdat nCuentas in lstCdatsXvencer)
                {
                    string pUrlFull = string.Empty;
                    pUrlFull = ResolveUrl("~/Pages/Cdat/Renovacion/Lista.aspx");
                    strHtml += "<li><a href='" + pUrlFull + "' style='white-space: normal;'><table style='padding:0px'><tr><td><i class='fa fa-clock-o text-aqua pull-left'></i></td>";
                    string pMensaje = "<td>&nbsp;Su cuenta de CDAT [" + nCuentas.numero_cdat + "] esta próxima a vencer el " + nCuentas.fecha_vencimiento.ToShortDateString() + "</td></tr></table>";
                    strHtml += pMensaje + "</a></li>";
                }
                foreach(xpinnWSDeposito.AhorroVista nCuentas in lstAproVista)
                {
                    strHtml += "<li><table style='padding:2px;text-align:center;'><tr><td>"+nCuentas.tipo_registro+"</td></td><tr><td>"+nCuentas.observaciones+"</td></tr><tr><td> (N°.: "+nCuentas.numero_cuenta+")</td></tr><tr><td> Fecha : "+nCuentas.fecha_apertura+ "</td></tr></table> <hr/> </li>";
                }
                foreach (xpinnWSDeposito.AhorroVista nCuentas in lstAproVistaCuota)
                {
                    strHtml += "<li><table style='padding:2px;text-align:center;'><tr><td>" + nCuentas.tipo_registro + "</td></td><tr><td>Confirmación cambio de cuota</td></tr><tr><td> (N°.: " + nCuentas.numero_cuenta + ")</td></tr><tr><td> Fecha : " + nCuentas.fecha_novedad_cambio + "</td></tr></table> <hr/> </li>";
                }

                strHtml += "</ul></li>";
            }
        }
        strHtml += "<li class='footer'>&nbsp;</li>";
        phNotification.Controls.Add(new LiteralControl(strHtml));
        Session["srtAlert" + pPersona.identificacion] = strHtml;
        Session["rptaIngreso" + pPersona.identificacion] = true;
    }

    protected void hlkCerrar_Click(object sender, EventArgs e)
    {
        if (Session["COD_INGRESO"] != null)
        {
            Boolean pRpta = false;
            Int32 pCodigo = Convert.ToInt32(Session["COD_INGRESO"].ToString());
            try
            {
                pRpta = Acceso.ModificarUsuarioIngreso(pCodigo, Session["sec"].ToString());
            }
            catch { }
        }
        Session.Abandon();
        Session.RemoveAll();
        string ruta = "~/Default.aspx";
        if (ConfigurationManager.AppSettings["RedirectCloseUrl"] != null)
        {
            string redirect = ConfigurationManager.AppSettings["RedirectCloseUrl"].ToString();
            ruta = redirect;
        }
        Response.Redirect(ruta);
    }

    protected void btnCambiarClave_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Pages/Account/CambioClave.aspx");
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (eventoConsultar != null)
            eventoConsultar(sender, e);
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if (eventoContinuar != null)
            eventoContinuar(sender, e);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (eventoGuardar != null)
            eventoGuardar(sender, e);
    }
    
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if (eventoEliminar != null)
            eventoEliminar(sender, e);
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        if (eventoLimpiar != null)
            eventoLimpiar(sender, e);
    }
    
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        if (eventoImprimir != null)
            eventoImprimir(sender, e);
    }

    protected void btnConsolidado_Click(object sender, EventArgs e)
    {
        if (eventoConsolidado != null)
            eventoConsolidado(sender, e);
    }
    
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        if (eventoExportar != null)
            eventoExportar(sender, e);
    }

    protected void btnImportar_Click(object sender, EventArgs e)
    {
        if (eventoImportar != null)
            eventoImportar(sender, e);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (eventoCancelar != null)
            eventoCancelar(sender, e);
    }
    
    protected void btnRegresar_Click(object sender, EventArgs e)
    {
        if (eventoRegresar != null)
            eventoRegresar(sender, e);
    }
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        if (eventoNuevo != null)
            eventoNuevo(sender, e);
    }
    public void MostrarConsultar(Boolean pestado)
    {
        btnConsultar.Visible = pestado;
    }

    public void MostrarContinuar(Boolean pestado)
    {
        btnContinuar.Visible = pestado;
    }

    public void MostrarGuardar(Boolean pestado)
    {
        btnGuardar.Visible = pestado;
    }

    public void MostrarEliminar(Boolean pestado)
    {
        btnEliminar.Visible = pestado;
    }

    public void MostrarLimpiar(Boolean pestado)
    {
        btnLimpiar.Visible = pestado;
    }

    public void MostrarImprimir(Boolean pestado)
    {
        btnImprimir.Visible = pestado;
    }

    public void MostrarConsolidado(Boolean pestado)
    {
        btnConsolidado.Visible = pestado;
    }

    public void MostrarExportar(Boolean pestado)
    {
        btnExportar.Visible = pestado;
    }

    public void MostrarImportar(Boolean pestado)
    {
        btnImportar.Visible = pestado;
    }

    public void MostrarCancelar(Boolean pestado)
    {
        btnCancelar.Visible = pestado;
    }

    public void MostrarRegresar(Boolean pestado)
    {
        btnRegresar.Visible = pestado;
    }
    public void MostrarNuevo(Boolean pestado)
    {
        btnNuevo.Visible = pestado;
    }

    [DllImport("Iphlpapi.dll")]
    private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);

    [DllImport("Ws2_32.dll")]
    private static extern Int32 inet_addr(string ip);
    private static string GetClientMAC(string strClientIP)
    {
        string mac_dest = "";
        try
        {
            Int32 ldest = inet_addr(strClientIP);
            Int32 lhost = inet_addr("");
            Int64 macinfo = new Int64();
            Int32 len = 6;
            int res = SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");

            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }

            for (int i = 0; i < 11; i++)
            {
                if (0 == (i % 2))
                {
                    if (i == 10)
                    {
                        mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                    else
                    {
                        mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                }
            }
        }
        catch (Exception err)
        {
            throw new Exception("Lỗi " + err.Message);
        }
        return mac_dest;
    }


    #region OPCIONES MENU IZQUIERDO
    protected void btnOpc1_Click(object sender, EventArgs e) { Response.Redirect("~/Pages/Inicio/Contenido.aspx?key=1", false); }
    protected void btnOpc2_Click(object sender, EventArgs e) { Response.Redirect("~/Pages/Inicio/Contenido.aspx?key=2", false); }
    protected void btnOpc3_Click(object sender, EventArgs e) { Response.Redirect("~/Pages/Inicio/Contenido.aspx?key=3", false); }
    protected void btnOpc4_Click(object sender, EventArgs e) { Response.Redirect("~/Pages/Inicio/Contenido.aspx?key=4", false); }
    protected void btnOpc5_Click(object sender, EventArgs e) { Response.Redirect("~/Pages/Inicio/Contenido.aspx?key=5", false); }
    protected void btnOpc6_Click(object sender, EventArgs e) { Response.Redirect("~/Pages/Inicio/Contenido.aspx?key=6", false); }
    protected void btnOpc7_Click(object sender, EventArgs e) { Response.Redirect("~/Pages/Inicio/Contenido.aspx?key=7", false); }
    protected void btnOpc8_Click(object sender, EventArgs e) { Response.Redirect("~/Pages/Inicio/Contenido.aspx?key=8", false); }
    protected void btnOpc9_Click(object sender, EventArgs e) { Response.Redirect("~/Pages/Inicio/Contenido.aspx?key=9", false); }
    protected void btnOpc10_Click(object sender, EventArgs e) { Response.Redirect("~/Pages/Inicio/Contenido.aspx?key=10", false); }
    protected void btnOpc11_Click(object sender, EventArgs e) { Response.Redirect("~/Pages/Inicio/Contenido.aspx?key=11", false); }
    #endregion
}
