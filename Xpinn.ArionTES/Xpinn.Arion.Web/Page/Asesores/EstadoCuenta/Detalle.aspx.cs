using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using Microsoft.CSharp;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Xpinn.Asesores.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Util;
using System.Web.Services;
using Xpinn.Cartera.Services;
using Xpinn.Cartera.Entities;
using Xpinn.Asesores.Services;

public partial class Detalle : GlobalWeb
{
    private Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
    private Usuario usuario = new Usuario();
    private Zona entityZona = new Zona();
    private Producto producto;
    private ComentarioService serviceComentario = new ComentarioService();
    private EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
    private AcodeudadoService acodeudadosservices = new AcodeudadoService();
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    private static string NAME_CACHE = "EstadoCuenta";
    private CuotasExtrasService CuoExtServicio = new CuotasExtrasService();
    private long numeroRadicacion;
    List<CuotasExtras> lstConsultas = new List<CuotasExtras>();
    CuotasExtras eCuoExt = new CuotasExtras();
    ClasificacionCarteraService clasificacionCarteraService = new ClasificacionCarteraService();
    ClasificacionCartera clasificacionCartera = new ClasificacionCartera();
    IList<HistoricoTasa> lsHistoricoTasas = new List<HistoricoTasa>();
    HistoricoTasaService historicoTasaService = new HistoricoTasaService();
    CreditoPlan credito = new CreditoPlan();
    HistoricoTasa historico = new HistoricoTasa();
    private GarantiaService garantiaService = new GarantiaService();
    ReliquidacionService reliquidacionService = new ReliquidacionService();
    private CreditoService creditoService = new CreditoService();

    private CreditoPlanService creditoPlanServicio = new CreditoPlanService();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(serviceEstadoCuenta.CodigoPrograma, "L");

            Site toolBar = (Site)this.Master;
            toolBar.eventoRegresar += btnRegresar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            toolBar.MostrarExportar(true);
            toolBar.eventoExportar += btnImprimir_Directo_Click;


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Page_PreInit", ex);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            producto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
            usuario = (Usuario)Session["Usuario"];
            ActualizarLinks();
            if (!Page.IsPostBack)
            {
                imgHuella.Visible = false;
                mvReporte.Visible = false;
                pDatosAfiliacion.Visible = false;
                // Cargar listas desplegables
                CargarListasDesplegables();
                // Cargar los datos del estado de cuenta
                ObtenerEstadoCuenta();
                ObtenerComentarios();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "A", "Page_Load", ex);
        }
    }

    /// <summary>
    /// Determinar parámetros de estado de cuenta para segùn eso mostrar los TABs de los productos
    /// </summary>
    /// <returns></returns>
    private Boolean VerificarParametros()
    {
        Boolean continuar = true;

        EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
        string parametrocredito;
        string parametroaportes;
        string parametroahorros;
        string parametroservicios;

        parametrocredito = serviceEstadoCuenta.ConsultarParametroCreditos((Usuario)Session["Usuario"]);
        parametroaportes = serviceEstadoCuenta.ConsultarParametrosAportes((Usuario)Session["Usuario"]);
        parametroahorros = serviceEstadoCuenta.ConsultarParametroAhorros((Usuario)Session["Usuario"]);
        parametroservicios = serviceEstadoCuenta.ConsultarParametroServicios((Usuario)Session["Usuario"]);

        this.tabCreditos.Visible = false;
        this.tabAportes.Visible = false;
        this.tabAhorros.Visible = false;
        this.tabServicios.Visible = false;

        // Mostrar créditos
        if (parametrocredito == "1")
        {
            this.tabCreditos.Visible = true;
            this.btnDatosCliente.Visible = true;
            this.btnPreAnalisis.Visible = true;
            this.btnComentarios.Visible = true;
            this.btnDatosNegocio.Visible = true;
            BtnCobranzas.Visible = true;
            Btnagenda.Visible = true;
        }

        // Mostrar Aportes
        if (parametroaportes == "2")
        {
            this.tabAportes.Visible = true;
        }

        // Mostrar Ahorros
        if (parametroahorros == "3")
        {
            this.tabAhorros.Visible = true;
        }

        // Mostrar Servicios
        parametroservicios = parametroservicios.Contains('|') ? parametroservicios.Split('|')[0] : parametroservicios;
        if (parametroservicios == "4")
        {
            this.tabServicios.Visible = true;
        }

        return continuar;
    }

    protected void MostrarHuellaFoto()
    {
        try
        {
            // Mostrar la información de la huella
            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];
            string pIdObjeto = "huella";
            Int64 pCods_Persona = Convert.ToInt32(txtCodigo.Text);
            vPersona1.cod_persona = pCods_Persona;
            if (vPersona1.cod_persona != int.MinValue)
            {
                vPersona1.soloPersona = 1;
                vPersona1.seleccionar = "Cod_persona";
                vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            }
            imgHuella.Visible = false;
            if (vPersona1.template != null)
            {
                try
                {
                    imgHuella.Visible = true;
                }
                catch // (Exception ex)
                {
                    // VerError("No pudo abrir archivo con imagen de la persona " + ex.Message);
                }
            }

            // Mostrar información de la foto de la persona
            Usuario lusuario = (Usuario)Session["usuario"];
            pIdObjeto = Convert.ToString(vPersona1.cod_persona);
            Int64 pCod_Persona = Convert.ToInt32(txtCodigo.Text);
            Int64 TipoDoc = Convert.ToInt64(vPersona1.tipo_identificacion);
            if (vPersona1.idimagen != null && vPersona1.idimagen != 0)
            {
                try
                {
                    imgFoto.ImageUrl = string.Format("Handler.ashx?id={0}&Us=" + lusuario.identificacion + "&Pw=" + System.Web.HttpUtility.UrlEncode(lusuario.clave_sinencriptar), vPersona1.idimagen);
                }
                catch // (Exception ex)
                {
                    // VerError("No pudo abrir archivo con imagen de la persona " + ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }

    }
    [WebMethod()]
    public void btnClose_Click(string gei)
    {
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
    }
    protected void ObtenerEstadoCuenta()
    {
        if (Session[MOV_GRAL_CRED_PRODUC] != null)
        {
            //*******************************************************************************************************//
            // Traer la información del estado de cuenta (datos de persona y sus productos)
            //*******************************************************************************************************//
            producto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);

            String nameCache = NAME_CACHE + producto.Persona.IdPersona.ToString();
            object cacheValue = System.Web.HttpRuntime.Cache.Get(nameCache);
            DateTime timeExpiration = DateTime.Now.AddSeconds(60);

            if (cacheValue == null)
            {
                producto.Persona = serviceEstadoCuenta.ConsultarPersona(producto.Persona.IdPersona, usuario);
                System.Web.HttpRuntime.Cache.Add(nameCache, producto, null, timeExpiration, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
            }
            else
            {
                producto = (Producto)System.Web.HttpRuntime.Cache.Get(nameCache);
                Session[MOV_GRAL_CRED_PRODUC] = producto;
            }

            //*******************************************************************************************************//
            // Mostrar la información del estado de cuenta
            //*******************************************************************************************************//
            try
            {
                txtFechaGeneracion.Text = DateTime.Now.ToShortDateString();
                txtCodigo.Text = HttpUtility.HtmlDecode(producto.Persona.IdPersona.ToString());
                if (producto.Persona.Telefono != null)
                    txtTelefono.Text = HttpUtility.HtmlDecode(producto.Persona.Telefono.ToString());
                else
                    txtTelefono.Text = "";
                txtCelular.Text = "";
                if (producto.Persona.Celular != null)
                    if (producto.Persona.Celular != "0")
                        txtCelular.Text = HttpUtility.HtmlDecode(producto.Persona.Celular.ToString());
                if (!string.IsNullOrEmpty(producto.Persona.PrimerApellido)) txtPrimerNombre.Text = HttpUtility.HtmlDecode(producto.Persona.PrimerNombre.Trim().ToString());
                if (!string.IsNullOrEmpty(producto.Persona.PrimerNombre)) txtPrimerApellido.Text = HttpUtility.HtmlDecode(producto.Persona.PrimerApellido.Trim().ToString());
                if (!producto.Persona.NumeroDocumento.Equals(0)) txtNumDoc.Text = HttpUtility.HtmlDecode(producto.Persona.NumeroDocumento.ToString());
                if (!string.IsNullOrWhiteSpace(producto.Persona.TipoIdentificacion.NombreTipoIdentificacion)) txtTipoDoc.Text = HttpUtility.HtmlDecode(producto.Persona.TipoIdentificacion.NombreTipoIdentificacion.Trim().ToString());
                if (!string.IsNullOrEmpty(producto.Persona.Direccion)) txtDireccion.Text = HttpUtility.HtmlDecode(producto.Persona.Direccion);
                if (!string.IsNullOrEmpty(producto.Persona.Email)) txtEmail.Text = HttpUtility.HtmlDecode(producto.Persona.Email);
                if (producto.Persona.FechaAfiliacion != null)
                {
                    if (producto.Persona.FechaAfiliacion.ToShortDateString().ToString() == "01/01/0001")
                        txtFechaAfiliacion.Text = "";
                    else
                        txtFechaAfiliacion.Text = HttpUtility.HtmlDecode(producto.Persona.FechaAfiliacion.ToShortDateString().ToString());
                }
                if (!string.IsNullOrEmpty(producto.Persona.TipoCliente)) txtTipoCliente.Text = HttpUtility.HtmlDecode(producto.Persona.TipoCliente);
                if (!string.IsNullOrEmpty(producto.Persona.Oficina.NombreOficina)) txtOficina.Text = HttpUtility.HtmlDecode(producto.Persona.Oficina.NombreOficina);
                if (!string.IsNullOrEmpty(producto.Persona.Ciudad.nomciudad)) txtCiudad.Text = HttpUtility.HtmlDecode(producto.Persona.Ciudad.nomciudad);
                if (!string.IsNullOrEmpty(producto.Persona.Asesor.PrimerNombre)) txtEjecutivo.Text = HttpUtility.HtmlDecode(producto.Persona.Asesor.PrimerNombre);
                if (producto.Persona.EstadoAfiliacion == null) txtestado.Text = "Sin Estado";
                else
                    txtestado.Text = producto.Persona.EstadoAfiliacion;
                if (producto.Persona.nommotivo == null) txtMotivo.Text = "Sin Motivo";
                else
                    txtMotivo.Text = HttpUtility.HtmlDecode(producto.Persona.nommotivo);
                if (!string.IsNullOrEmpty(producto.Persona.Zona.NombreZona)) txtZona.Text = HttpUtility.HtmlDecode(producto.Persona.Zona.NombreZona);

                List<PersonaEmpresaRecaudo> LstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
                PersonaEmpresaRecaudoServices infoEmpresaRecaudo = new PersonaEmpresaRecaudoServices();
                LstEmpresaRecaudo = infoEmpresaRecaudo.ListarPersonaEmpresaRecaudo(Convert.ToInt64(txtCodigo.Text), (Usuario)Session["usuario"]);
                string cadena = "", newPagaduria = "";
                foreach (PersonaEmpresaRecaudo rFila in LstEmpresaRecaudo)
                {
                    if (rFila.idempresarecaudo != 0 && rFila.idempresarecaudo != null)
                        if (rFila.descripcion != "")
                            cadena = cadena + rFila.descripcion + " - ";
                }
                if (cadena != "")
                    newPagaduria = cadena.Substring(0, cadena.Length - 3);

                txtPagadurias.Text = newPagaduria;
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "A", "ObtenerDatos", ex);
            }

            //*******************************************************************************************************//
            // Mostrar información de la afiliación
            //*******************************************************************************************************//
            ObtenerDatosAfiliacion(producto.Persona.IdPersona);

            //*******************************************************************************************************//
            // Mostrar información de los aportes
            //*******************************************************************************************************//
            ActualizarTablaAportes();

            chkaporterminados_CheckedChanged(chkaporterminados, null);

            //*******************************************************************************************************//
            // Mostrar información de los aportes Pendientes
            //*******************************************************************************************************//
            ActualizarAportesPendientes();

            //*******************************************************************************************************//
            // Mostrar información de los créditos
            //*******************************************************************************************************//
            ActualizarTablaCreditos();
            ActualizarTablaAcodeudados();

            //*******************************************************************************************************//
            // Mostrar información de los ahorros a la vista
            //*******************************************************************************************************//
            ActualizarTablaAhorros();

            //*******************************************************************************************************//
            // Mostrar información de los ahorros programados
            //*******************************************************************************************************//
            ActualizarTablaAhoProgra();

            //*******************************************************************************************************//
            // Mostrar información de los CDATS
            //*******************************************************************************************************//
            ActualizarTablaCDATS();

            //*******************************************************************************************************//
            // Mostrar información de las devoluciones
            //*******************************************************************************************************//
            ActualizarTablaDevolucion();

            //*******************************************************************************************************//
            // Mostrar información de los servicios
            //*******************************************************************************************************//
            ActualizarTablaServicios();

            //*******************************************************************************************************//
            // Mostrar información de las Lineas
            //*******************************************************************************************************//
            ActualizarTablaLineas();

            MostrarHuellaFoto();

            // Determinar tab activo
            Tabs.ActiveTabIndex = 0;

            // Mostrar las alertas
            EstadoCuenta vAlerta = new EstadoCuenta();
            vAlerta.Codigo = Convert.ToInt64(txtCodigo.Text);
            vAlerta.fecha_inicio = Convert.ToDateTime(txtFechaGeneracion.Text);
            serviceEstadoCuenta.enviarAlertas(vAlerta, (Usuario)Session["usuario"]);
            if (vAlerta.linea != "")
            {
                //Modificado para agregar el texto con saltos de linea
                string[] cadena = vAlerta.linea.Split('-');
                string textofinal = "";
                foreach (string cad in cadena)
                {
                    textofinal = textofinal + cad + "\n\r";
                }
                txtAlertas.Text = textofinal;
            }
            else
            {
                txtAlertas.Text = "";
            }

            // Determina si muestra datos del negocio
            Usuario xusuario = (Usuario)Session["usuario"];
            if (xusuario.tipo == 1)
            {
                btnDatosNegocio.Visible = true;
            }
            else
            {
                btnDatosNegocio.Visible = false;
            }

            //*****************************
            //carga control moras
            //****************************
            TextBox txtcod = (TextBox)ctlMora.FindControl("txtCodigo");
            txtcod.Text = txtCodigo.Text;

            TextBox txtiden = (TextBox)ctlMora.FindControl("txtIdentificacion");
            txtiden.Text = txtNumDoc.Text;

            TextBox txtfechCort = (TextBox)ctlMora.FindControl("txtFechaCorte");
            if (txtfechCort.Text == DateTime.Now.ToShortDateString() || txtfechCort.Text == "")
            {
                txtfechCort.Text = DateTime.Now.ToShortDateString();
            }

        }
    }

    protected void ObtenerComentarios()
    {
        Int64 codigo = Convert.ToInt64(txtCodigo.Text);
        String idPersona = "0";
        Comentario comentario = new Comentario();
        comentario = serviceComentario.ConsultarComentarios(codigo, (Usuario)Session["usuario"]);
        if (!string.IsNullOrEmpty(comentario.idPersona.ToString()))
            idPersona = HttpUtility.HtmlDecode(comentario.idPersona.ToString());
        if (idPersona != null && idPersona != "0")
        {
            this.btnComentarios.BackColor = Color.Red;
            VerError("Consulte los comentarios del cliente");
        }
    }

    #region Botones del estado de cuenta
    protected void btnDatosCliente_Click(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        Int64 pCods_Persona = Convert.ToInt32(txtCodigo.Text);
        vPersona1.cod_persona = pCods_Persona;
        if (vPersona1.cod_persona != null)
        {
            vPersona1.seleccionar = "Cod_persona";
            vPersona1.soloPersona = 1;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
        }

        String id = txtCodigo.Text;
        Session[serviceEstadoCuenta.CodigoPrograma + ".id"] = id;
        Session[serviceEstadoCuenta.CodigoPrograma + ".modificar"] = 1;

        if (vPersona1.tipo_persona != null)
        {
            if (vPersona1.tipo_persona == "J")
                Navegar("~/Page/Aportes/Afiliaciones/Nuevo.aspx");
            else
            {
                PersonaResponsable pResponsable = new PersonaResponsable();
                string pFiltro = "WHERE R.COD_PERSONA = " + id;
                pResponsable = AfiliacionServicio.ConsultarPersonaResponsable(pFiltro, (Usuario)Session["usuario"]);
                if (pResponsable != null && pResponsable.consecutivo != 0 && pResponsable.cod_persona_tutor != 0)
                    Navegar("~/Page/Aportes/Personas/NuevoMDE.aspx");
                else
                    Navegar("~/Page/Aportes/Personas/Nuevo.aspx");
            }
        }
        else
        {
            Navegar("~/Page/Aportes/Personas/Tabs/Nuevo.aspx");
        }



    }

    protected void btnDatosNegocio_Click(object sender, EventArgs e)
    {
        Navegar("~/Page/Asesores/EstadoCuenta/Negocio/Detalle.aspx");
    }

    protected void BtnCobranzas_Click(object sender, EventArgs e)
    {
        idObjeto = txtCodigo.Text;
        Session["ESTADOCUENTA"] = idObjeto;
        ClienteService clienteServicio = new ClienteService();
        Session[clienteServicio.CodigoPrograma + ".id"] = idObjeto;
        CreditosService creditoServicio = new CreditosService();
        Session[creditoServicio.CodigoPrograma + ".id"] = "";
        Session[creditoServicio.CodigoPrograma + ".txtCliente"] = idObjeto;
        Navegar("~/Page/Asesores/Recuperacion/Lista.aspx");
    }

    protected void btnComentarios_Click(object sender, EventArgs e)
    {
        Navegar("~/Page/Asesores/EstadoCuenta/Creditos/Comentarios/Detalle.aspx");
    }

    protected void btnPreAnalisis_Click(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service personaSer = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Session[personaSer.CodigoProgramaPreAnalisis + ".id"] = txtCodigo.Text;
        Navegar("~/Page/FabricaCreditos/Preanalisis/Nuevo.aspx");
    }

    protected void Btnagenda_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Page/Asesores/GestionarAgenda/Lista.aspx");
    }

    protected void btnVerComprobantes_Click(object sender, EventArgs e)
    {
        Xpinn.Contabilidad.Services.ComprobanteService ComprobanteServicio = new Xpinn.Contabilidad.Services.ComprobanteService();
        //Session[ComprobanteServicio.CodigoPrograma + ".id"] = txtNumDoc.Text;
        Response.Redirect("~/Page/Contabilidad/Comprobante/Lista.aspx?num_documento=" + txtNumDoc.Text);
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/Asesores/EstadoCuenta/Lista.aspx");
    }

    //Agregado para visualizar el consolidado de productos
    protected void btnConsolidado_Click(object sender, EventArgs e)
    {
        Session["cod_persona"] = txtCodigo.Text;
        Navegar("~/Page/Asesores/EstadoCuenta/ConsolidadoProductos/Detalle.aspx");
    }
    #endregion

    #region Mostrar la información de los Créditos

    protected void gvLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvLista.SelectedRow.Cells[0].Text;
        Session[serviceEstadoCuenta.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvLista_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLista.PageIndex = e.NewPageIndex;
            ObtenerEstadoCuenta();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }


    protected void gvLista_RowCommand(object sender, GridViewCommandEventArgs evt)
    {

        if (evt.CommandName == "DetalleCredito" || evt.CommandName == "MovGralCredito" || evt.CommandName == "DetallePago" || evt.CommandName == "Codeudores" || evt.CommandName == "Garantias")
        {
            string[] tmp = evt.CommandArgument.ToString().Split('|');
            producto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
            producto.CodLineaCredito = Convert.ToString(tmp[0]);
            producto.CodRadicacion = Convert.ToString(tmp[1]);
            if (evt.CommandName == "DetallePago") { producto.noconsultaTodo = 0; }

            Session[MOV_GRAL_CRED_PRODUC] = producto;
        }

        if (evt.CommandName == "DetalleCredito") Navegar("~/Page/Asesores/EstadoCuenta/Creditos/Detalle.aspx");
        if (evt.CommandName == "MovGralCredito") Navegar("~/Page/Asesores/EstadoCuenta/Creditos/MovimientoGeneral/Detalle.aspx");
        if (evt.CommandName == "DetallePago") Navegar("~/Page/Asesores/EstadoCuenta/Creditos/Pago/Detalle.aspx");
        if (evt.CommandName == "Codeudores") Navegar("~/Page/Asesores/EstadoCuenta/Creditos/Codeudores/Detalle.aspx");
        if (evt.CommandName == "Garantias") Navegar("~/Page/Asesores/EstadoCuenta/Creditos/Garantia/Detalle.aspx");

    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
        int columnas;
        columnas = serviceEstadoCuenta.ConsultarParametroColumnas((Usuario)Session["Usuario"]);
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (columnas > 0)
            {
                e.Row.Cells[5].Visible = chkCredProceso.Checked;
                e.Row.Cells[12].Controls.Clear();
                e.Row.Cells[12].Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (columnas > 0)
            {
                e.Row.Cells[5].Visible = chkCredProceso.Checked;
                e.Row.Cells[12].Controls.Clear();
                e.Row.Cells[12].Visible = false;
            }

            List<string> lstcomentarios = ComentariosCredito(e);
            if (lstcomentarios.Count > 0)
            {
                var contador = 0;
                var bell = e.Row.Cells[4].FindControl("BellNotification");
                System.Web.UI.HtmlControls.HtmlGenericControl DivGlobal = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.Cells[4].FindControl("DivGlobal");
                bell.Visible = true;

                foreach (var item in lstcomentarios)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl newdivs = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    newdivs.Attributes.Add("class", "sec");
                    newdivs.ID = "DivSec" + contador;
                    DivGlobal.Controls.Add(newdivs);

                    System.Web.UI.HtmlControls.HtmlGenericControl DivSec = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.Cells[4].FindControl("DivSec" + contador);
                    System.Web.UI.HtmlControls.HtmlGenericControl newdiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    newdiv.Attributes.Add("class", "txt");
                    newdiv.ID = "txtInformation" + contador;
                    DivSec.Controls.Add(newdiv);

                    var TextNotification = e.Row.Cells[4].FindControl("txtInformation" + contador);
                    Label label = new Label();
                    label.Text = item;
                    label.CssClass = "NoTxt";
                    TextNotification.Controls.Add(label);
                    contador++;
                }
            }
        }

    }

    protected void chkcredterminados_CheckedChanged(object sender, EventArgs e)
    {
        ActualizarTablaCreditos();
    }

    protected void chkaporterminados_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox aporte_terminado = (CheckBox)sender;
        ActualizarTablaAportes();
        //if (aporte_terminado.Checked)
        //{
        //    ActualizarTablaAportesCerrados();
        //}
        //else
        //{
        //    ActualizarTablaAportes();
        //}
    }
    protected void chkCredProceso_CheckedChanged(object sender, EventArgs e)
    {
        ActualizarTablaCreditos();
    }

    protected void chkCredAnulados_CheckedChanged(object sender, EventArgs e)
    {
        ActualizarTablaCreditos();
    }

    /// <summary>
    /// Mostrar información de los créditos de la persona
    /// </summary>
    private void ActualizarTablaCreditos()
    {
        VerificarParametros();
        ComentarioService serviceComentario = new ComentarioService();
        try
        {
            // Mostrar comentarios de la persona
            producto = (Producto)(Session[MOV_GRAL_CRED_PRODUC]);
            producto.Comentarios = serviceComentario.ListarComentario(producto, (Usuario)Session["usuario"]);
            List<Comentario> coment = producto.Comentarios;

            var lstComentarios = (from p in producto.Comentarios orderby p.idComentario descending select p).ToList();
            if (lstComentarios.Count() > 0)
            {
                txtComentarios.Visible = true;
                int count = 1;
                foreach (Comentario item in coment)
                {
                    txtComentarios.Text += count + ". " + item.descripcion + " \n\r ";
                    //lblComentarios.Text += count + ". " + item.descripcion + " \n\r ";
                    count = count + 1;

                }
            }
            else
            {
                txtComentarios.Visible = false;
            }

            // Mostrar información de los créditos
            List<Producto> lstConsulta = new List<Producto>();
            EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
            String FiltroEstados = " 'DESEMBOLSADO' ";
            if (chkcredterminados.Checked)
            {
                FiltroEstados += ", 'TERMINADO', 'PAGADO'";
            }
            if (chkCredProceso.Checked)
            {
                FiltroEstados += ", 'SOLICITADO','VERIFICAR REF.','APROBADO','GENERADO', 'PRE APROBADO'"; // Agregado estado Pre aprobado 
            }
            if (chkCredAnulados.Checked)
            {
                FiltroEstados += " ,'ANULADO', 'NEGADO'";
            }

            // Aplicar el filtro si se seleccionó el estado
            if (chkcredterminados.Checked && chkCredProceso.Checked && chkCredAnulados.Checked)
            {
                lstConsulta = serviceEstadoCuenta.ListarProductos(producto, (Usuario)Session["usuario"]);
            }
            else
            {
                String FiltroFinal = " (estado Like 'ATRASADO%' Or estado = 'ESTA AL DIA' Or estado In (" + FiltroEstados + "))";
                lstConsulta = serviceEstadoCuenta.ListarProductosPorEstados(producto, (Usuario)Session["usuario"], FiltroFinal);
            }

            if (lstConsulta.Count > 0)
            {
                var lstProductos = (from p in lstConsulta
                                    orderby p.SaldoCapital descending, p.Estado, p.Pagare
                                    select new
                                    {
                                        p.Persona.IdPersona,
                                        p.Estado,
                                        p.Proceso,
                                        p.Pagare,
                                        p.Linea,
                                        FechaSolicitud = p.FechaSolicitud.ToShortDateString().ToString(),
                                        MontoAprobado = p.MontoAprobado.ToString("##,##0", CultureInfo.InvariantCulture),
                                        SaldoCapital = p.SaldoCapital.ToString("##,##0", CultureInfo.InvariantCulture),
                                        garantiacomunitaria = p.garantiacomunitaria.ToString("##,##0", CultureInfo.InvariantCulture),
                                        chkCodeudores = ((p.Codeudor == 0) ? false : true),
                                        chkGarantias = ((p.Garantia == 0) ? false : true),
                                        Cuota = p.Cuota.ToString("##,##0", CultureInfo.InvariantCulture),
                                        Atributos = p.Atributos.ToString("##,##0", CultureInfo.InvariantCulture),
                                        p.Plazo,
                                        p.CuotasPagadas,
                                        p.Codeudores,
                                        p.Garantia,
                                        FechaProximoPago = ((p.FechaProximoPago.ToShortDateString().ToString() == "01/01/0001") ? "" : p.FechaProximoPago.ToShortDateString().ToString()),
                                        FechaVencimiento = ((Convert.ToDateTime(p.FechaVencimiento).ToShortDateString().ToString() == "01/01/0001") ? "" : Convert.ToDateTime(p.FechaVencimiento).ToShortDateString().ToString()),
                                        ValorAPagar = p.ValorAPagar.ToString("##,##0", CultureInfo.InvariantCulture),
                                        ValorTotalAPagar = p.ValorTotalAPagar.ToString("##,##0", CultureInfo.InvariantCulture),
                                        p.Oficina.NombreOficina,
                                        p.CodRadicacion,
                                        p.CalifPromedio,
                                        p.NumRadicion,
                                        p.observaciones,
                                        p.Tasainteres,
                                        FechaDesembolso = ((p.FechaDesembolso.ToShortDateString().ToString() == "01/01/0001") ? "" : p.FechaDesembolso.ToShortDateString().ToString()),
                                        data = (p.CodLineaCredito.ToString() + "|" + p.CodRadicacion.ToString()),
                                        p.vr_ult_dscto,
                                        p.pagadurias,
                                        Disponible = p.Disponible,
                                        p.TipoLinea,
                                        p.FechaReestructurado,
                                        p.valorapagarCE
                                    }
                                    ).ToList();


                lstProductos = lstProductos.OrderByDescending(p => p.Estado == "AL DIA").ToList();

                ViewState["codRadicacion"] = lstProductos.Select(p => p.CodRadicacion).First();
                txtTotalSaldos.Text = (from c in lstConsulta select (long)c.SaldoCapital).Sum().ToString(CultureInfo.InvariantCulture);
                txtTotalSaldosrp.Text = (from c in lstConsulta select (long)c.SaldoCapital).Sum().ToString();
                txtTotalCoutasCreditos.Text = (((from c in lstConsulta select (long)c.ValorCuota).Sum()) - ((from c in lstConsulta where c.SaldoCapital == 0 select (long)c.ValorCuota).Sum())).ToString();
                txtTotalCoutasCredrp.Text = (from c in lstConsulta select (long)c.ValorCuota).Sum().ToString();
                txtVlrPendienteApagar.Text = (from c in lstConsulta select (long)c.ValorAPagar).Sum().ToString(CultureInfo.InvariantCulture);
                txtVlrPendienteApagarCE.Text = (from c in lstConsulta select (long)c.valorapagarCE).Sum().ToString(CultureInfo.InvariantCulture);
                txtVlrPendienteAparep.Text = (from c in lstConsulta select (long)c.ValorAPagar).Sum().ToString();
                Label3.Text = (from c in lstConsulta select (long)c.Atributos).Sum().ToString(CultureInfo.InvariantCulture); //TOTAL ATRIBUTOS
                Label5.Text = (from c in lstConsulta select (long)c.ValorTotalAPagar).Sum().ToString(CultureInfo.InvariantCulture); ; //TOTAL VALOR A PAGAR
                gvLista.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
                gvLista.DataSource = lstProductos;

            }

            if (lstConsulta.Count > 0)
            {
                chkcredterminados.Visible = true;
                gvLista.Visible = true;
                lblInfo.Visible = false;
                lblTotalRegs.Visible = true;
                lblTotalRegs.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLista.DataBind();
                divCreditos.Visible = true;
                lblTotalSaldos.Visible = true;
                txtTotalSaldos.Visible = true;
                lblTotalCuotas.Visible = true;
                txtTotalCoutasCreditos.Visible = true;
                lblTotalPendientes.Visible = true;
                lblTotalPendientesCE.Visible = true;
                txtVlrPendienteApagar.Visible = true;
                txtDevolucionestotal.Visible = true;
                txtVlrPendienteApagarCE.Visible = true;
            }
            else
            {
                gvLista.Visible = false;
                lblInfo.Visible = true;
                lblTotalRegs.Visible = false;
                divCreditos.Visible = false;
                lblTotalSaldos.Visible = false;
                txtTotalSaldos.Visible = false;
                lblTotalCuotas.Visible = false;
                txtTotalCoutasCreditos.Visible = false;
                lblTotalPendientes.Visible = false;
                txtVlrPendienteApagar.Visible = false;
                txtVlrPendienteApagarCE.Visible = false;
                lblTotalPendientesCE.Visible = false;
            }

            Session["DTCREDITOS"] = lstConsulta;
            Session.Add(serviceEstadoCuenta.GetType().Name + ".consulta", 1);

            foreach (GridViewRow item in gvLista.Rows)
            {
                if (item.Cells[30].Text == "2")
                {
                    HyperLink hlf = (HyperLink)item.FindControl("hplink");
                    hlf.NavigateUrl = "../../FabricaCreditos/Rotativo/DetalleAvances/Detalle.aspx?radicado=" + hlf.Text;
                    item.Cells[23].Text = "0";
                }
                else
                {
                    HyperLink hlf = (HyperLink)item.FindControl("hplink");
                    hlf.NavigateUrl = "../../FabricaCreditos/PlanPagos/Detalle.aspx?radicado=" + hlf.Text;
                }
            }
            Session[MOV_GRAL_CRED_PRODUC] = producto;
        }

        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "Actualizar", ex);
        }


    }

    #endregion

    #region Imprimir

    /// <summary>
    /// Botón para imprimir la pantalla
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="evt"></param>
    protected void ucImprimir_Click(object sender, ImageClickEventArgs evt)
    {
        Session.Remove("imprimirCtrl");
        Session["imprimirCtrl"] = gvLista;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", Imprimir.JSCRIPT_PRINT);
    }

    protected void btnImprimir_Directo_Click(object sender, EventArgs e)
    {
        btnImprimir_Click(null, null);
        btnImprime_Click(null, null);
    }
    /// <summary>
    ///  Genera el reporte
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImprimir_Click(object sender, EventArgs e)
    {
        string separadorDecimal = System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        if (RpviewEstado.Visible == false)
        {
            // Crear datatable para los aportes
            System.Data.DataTable tableAporte = new System.Data.DataTable();
            tableAporte.Columns.Add("numero_aporte");
            tableAporte.Columns.Add("estado");
            tableAporte.Columns.Add("linea");
            tableAporte.Columns.Add("fecha_apertura");
            tableAporte.Columns.Add("saldo_total");
            tableAporte.Columns.Add("valor_cuota");
            tableAporte.Columns.Add("fecha_prox_pago");
            tableAporte.Columns.Add("valor_acumulado");
            tableAporte.Columns.Add("valor_total_acumu");
            tableAporte.Columns.Add("valor_a_pagar");

            List<Xpinn.Aportes.Entities.Aporte> lstAporte = new List<Xpinn.Aportes.Entities.Aporte>();
            lstAporte = (List<Xpinn.Aportes.Entities.Aporte>)Session["APORTES"];
            if (lstAporte != null)
            {
                if (lstAporte.Count > 0)
                {
                    foreach (Xpinn.Aportes.Entities.Aporte item in lstAporte)
                    {
                        DataRow data;

                        data = tableAporte.NewRow();
                        data[0] = item.numero_aporte;
                        data[1] = item.estado_Linea;
                        data[2] = item.nom_linea_aporte;
                        data[3] = item.fecha_apertura.ToShortDateString();
                        data[4] = Convert.ToDecimal(item.Saldo).ToString("0").Replace("$", "").Replace(".", "");
                        data[5] = Convert.ToDecimal(item.cuota).ToString().Replace("$", "").Replace(".", "");
                        data[6] = item.fecha_proximo_pago.ToShortDateString();
                        data[7] = Convert.ToDecimal(item.valor_acumulado).ToString().Replace("$", "").Replace(".", "");
                        data[8] = Convert.ToDecimal(item.valor_total_acumu).ToString().Replace("$", "").Replace(".", "");
                        data[9] = Convert.ToDecimal(item.valor_a_pagar).ToString().Replace("$", "").Replace(".", "");


                        tableAporte.Rows.Add(data);
                    }
                }
            }

            ///cargar acodeudados
            System.Data.DataTable tableAcodeudados = new System.Data.DataTable();
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

            List<Acodeudados> pAcodeudadoss = new List<Acodeudados>();
            pAcodeudadoss = acodeudadosservices.ListarAcodeudadoss(new Cliente() { IdCliente = Convert.ToInt64(txtCodigo.Text) }, (Usuario)Session["usuario"]);
            foreach (Acodeudados asc in pAcodeudadoss)
            {
                DataRow datas;
                datas = tableAcodeudados.NewRow();
                datas[0] = Convert.ToDateTime(asc.FechaProxPago).ToShortDateString();
                datas[3] = asc.CodPersona;
                datas[5] = asc.Nombres;
                datas[6] = Convert.ToDecimal(asc.Monto).ToString().Replace("$", "").Replace(".", "");
                datas[7] = Convert.ToDecimal(asc.Saldo).ToString().Replace("$", "").Replace(".", "");
                datas[8] = Convert.ToDecimal(asc.Cuota).ToString().Replace("$", "").Replace(".", "");
                datas[9] = Convert.ToDecimal(asc.Valor_apagar).ToString().Replace("$", "").Replace(".", "");
                datas[10] = asc.identificacion;
                tableAcodeudados.Rows.Add(datas);
            }

            //Datatable CDATS
            DataTable tableCdat = new DataTable();
            tableCdat.Columns.Add("numero_cdat");
            tableCdat.Columns.Add("estado");
            tableCdat.Columns.Add("linea");
            tableCdat.Columns.Add("fecha_apertura");
            tableCdat.Columns.Add("fecha_inicio");
            tableCdat.Columns.Add("fecha_vencimiento");
            tableCdat.Columns.Add("nom_oficina");
            tableCdat.Columns.Add("valor");
            tableCdat.Columns.Add("plazo");
            tableCdat.Columns.Add("tasa_interes");
            tableCdat.Columns.Add("valor_acumulado");
            tableCdat.Columns.Add("valor_total_acumu");

            List<Xpinn.CDATS.Entities.Cdat> lstCdat = new List<Xpinn.CDATS.Entities.Cdat>();
            if (ViewState["CDATS"] != null)
            {
                lstCdat = (List<Xpinn.CDATS.Entities.Cdat>)ViewState["CDATS"];
                if (lstCdat.Count > 0)
                {
                    foreach (Xpinn.CDATS.Entities.Cdat item in lstCdat)
                    {
                        DataRow data;
                        data = tableCdat.NewRow();
                        data[0] = item.numero_cdat;
                        data[1] = item.nom_estado;
                        data[2] = item.nomlinea;
                        data[3] = item.fecha_apertura != DateTime.MinValue ? item.fecha_apertura.ToString(gFormatoFecha) : " ";
                        data[4] = item.fecha_inicio != DateTime.MinValue ? item.fecha_inicio.ToString(gFormatoFecha) : " ";
                        data[5] = item.fecha_vencimiento != DateTime.MinValue ? item.fecha_vencimiento.ToString(gFormatoFecha) : " ";
                        data[6] = " " + item.nomoficina;
                        data[7] = Convert.ToDecimal(item.valor).ToString().Replace("$", "").Replace(".", "");
                        data[8] = " " + item.plazo;
                        data[9] = " " + item.tasa_interes;
                        data[10] = Convert.ToDecimal(item.valor_acumulado).ToString().Replace("$", "").Replace(".", "");
                        data[11] = Convert.ToDecimal(item.valor_total_acumu).ToString().Replace("$", "").Replace(".", "");

                        tableCdat.Rows.Add(data);
                    }
                }
            }


            //Datatable Servicios
            DataTable tableServi = new DataTable();
            tableServi.Columns.Add("numero_servicio");
            tableServi.Columns.Add("estado");
            tableServi.Columns.Add("linea");
            tableServi.Columns.Add("fecha_solicitud");
            tableServi.Columns.Add("plan");
            tableServi.Columns.Add("fecha_inicio");
            tableServi.Columns.Add("fecha_final");
            tableServi.Columns.Add("valor_total");
            tableServi.Columns.Add("vr_cuota");
            tableServi.Columns.Add("saldo");
            tableServi.Columns.Add("saldo_inicial");
            tableServi.Columns.Add("fecha_desembolso");
            tableServi.Columns.Add("fecha_proximo_pago");
            tableServi.Columns.Add("forma_pago");
            tableServi.Columns.Add("plazo");

            List<EstadoCuenta> lstServicio = new List<EstadoCuenta>();
            if (Session["SERVICIOS"] != null)
            {
                lstServicio = (List<EstadoCuenta>)Session["SERVICIOS"];
                if (lstServicio.Count > 0)
                {
                    foreach (EstadoCuenta item in lstServicio)
                    {
                        DataRow data;
                        data = tableServi.NewRow();
                        data[0] = item.num_servicio;
                        data[1] = item.nom_estado;
                        data[2] = item.linea;
                        data[3] = item.fecha_solicitud != DateTime.MinValue ? item.fecha_solicitud.ToString(gFormatoFecha) : " ";
                        data[4] = " " + item.plan;
                        data[5] = item.fecha_inicio_vigencia.HasValue ? item.fecha_inicio_vigencia.Value.ToString(gFormatoFecha) : " ";
                        data[6] = item.fecha_final_vigencia.HasValue ? item.fecha_final_vigencia.Value.ToString(gFormatoFecha) : " ";
                        data[7] = Convert.ToDecimal(item.valor).ToString().Replace("$", "").Replace(".", "");
                        data[8] = Convert.ToDecimal(item.cuota).ToString().Replace("$", "").Replace(".", "");
                        data[9] = Convert.ToDecimal(item.saldo).ToString().Replace("$", "").Replace(".", "");
                        data[10] = Convert.ToDecimal(item.valor).ToString().Replace("$", "").Replace(".", "");
                        data[11] = " " + item.fecha_desembolso.ToString(gFormatoFecha);
                        data[12] = " " + item.fecha_proximo_pago.ToString(gFormatoFecha);
                        data[13] = item.forma_pago != null ? item.forma_pago.ToString() : " ";
                        data[14] = Convert.ToDecimal(item.cuotas).ToString().Replace("$", "").Replace(".", "");
                        tableServi.Rows.Add(data);
                    }
                }
            }

            // Cargar en el datatable la información de las devoluciones
            System.Data.DataTable tableDevolucion = new System.Data.DataTable();
            tableDevolucion.Columns.Add("numero_devolu");
            tableDevolucion.Columns.Add("estado");
            tableDevolucion.Columns.Add("concepto");
            tableDevolucion.Columns.Add("fecha_devolucion");
            tableDevolucion.Columns.Add("valor");
            tableDevolucion.Columns.Add("saldo");

            List<Xpinn.Tesoreria.Entities.Devolucion> lstDevolucion = new List<Xpinn.Tesoreria.Entities.Devolucion>();
            lstDevolucion = (List<Xpinn.Tesoreria.Entities.Devolucion>)Session["DEVOLUCION"];
            if (lstDevolucion != null)
            {
                if (lstDevolucion.Count > 0)
                {
                    foreach (Xpinn.Tesoreria.Entities.Devolucion item in lstDevolucion)
                    {
                        DataRow datad;
                        datad = tableDevolucion.NewRow();
                        datad[0] = item.num_devolucion;
                        datad[1] = item.estado;
                        datad[2] = item.concepto;
                        datad[3] = item.fecha_devolucion.ToShortDateString();
                        datad[4] = Convert.ToDecimal(item.valor).ToString().Replace("$", "").Replace(".", "");
                        datad[5] = Convert.ToDecimal(item.saldo).ToString().Replace("$", "").Replace(".", "");
                        tableDevolucion.Rows.Add(datad);
                    }
                }
            }


            // Cargar en el datatable la información de las lineas
            System.Data.DataTable tableLineas = new System.Data.DataTable();
            tableLineas.Columns.Add("num_linea");
            tableLineas.Columns.Add("cod_serv_fijo");
            tableLineas.Columns.Add("cod_serv_adicional");
            tableLineas.Columns.Add("fecha_act");
            tableLineas.Columns.Add("fecha_ult_rep");
            tableLineas.Columns.Add("valor_fijo");
            tableLineas.Columns.Add("Costo_Factura");
            tableLineas.Columns.Add("fecha_vencimiento");
            tableLineas.Columns.Add("tipo_plan");
            tableLineas.Columns.Add("fecha_inactivacion");

            List<Xpinn.Aportes.Entities.PlanTelefonico> lstLineas = new List<Xpinn.Aportes.Entities.PlanTelefonico>();
            lstLineas = (List<Xpinn.Aportes.Entities.PlanTelefonico>)Session["LINEAS"];
            if (lstLineas != null)
            {
                if (lstLineas.Count > 0)
                {
                    foreach (Xpinn.Aportes.Entities.PlanTelefonico item in lstLineas)
                    {
                        DataRow datad;
                        datad = tableLineas.NewRow();
                        datad[0] = item.num_linea_telefonica;
                        datad[1] = item.cod_serv_fijo;
                        datad[2] = item.cod_serv_adicional;
                        datad[3] = item.fecha_activacion.ToShortDateString();
                        datad[4] = Convert.ToDateTime(item.fecha_ult_reposicion).ToShortDateString();
                        datad[5] = Convert.ToDecimal(item.valor_fijo).ToString().Replace("$", "").Replace(".", "");
                        datad[6] = Convert.ToDecimal(item.valor_total).ToString().Replace("$", "").Replace(".", "");
                        datad[7] = item.fecha_vencimiento != null ? Convert.ToDateTime(item.fecha_vencimiento).ToShortDateString() : null;
                        datad[8] = item.nom_plan;
                        datad[9] = item.fecha_incativacion != null ? Convert.ToDateTime(item.fecha_incativacion).ToShortDateString() : null;
                        tableLineas.Rows.Add(datad);
                    }
                }
            }



            // Cargar en un datatable la información de los ahorros a la vista
            System.Data.DataTable tableAhorro = new System.Data.DataTable();
            tableAhorro.Columns.Add("numero_cuenta");
            tableAhorro.Columns.Add("estado");
            tableAhorro.Columns.Add("linea");
            tableAhorro.Columns.Add("fecha_apertura");
            tableAhorro.Columns.Add("saldo_total");
            tableAhorro.Columns.Add("saldo_canje");
            tableAhorro.Columns.Add("nom_oficina");
            tableAhorro.Columns.Add("valor_cuota");
            tableAhorro.Columns.Add("fecha_proximo_pago");
            tableAhorro.Columns.Add("valor_acumulado");
            tableAhorro.Columns.Add("valor_total_acumu");


            List<Xpinn.Ahorros.Entities.AhorroVista> lstAhorros = new List<Xpinn.Ahorros.Entities.AhorroVista>();
            lstAhorros = (List<Xpinn.Ahorros.Entities.AhorroVista>)Session["AHORROS"];
            if (lstAhorros != null)
            {
                if (lstAhorros.Count > 0)
                {
                    foreach (Xpinn.Ahorros.Entities.AhorroVista itemAh in lstAhorros)
                    {
                        DataRow dataA;
                        dataA = tableAhorro.NewRow();
                        dataA[0] = itemAh.numero_cuenta;
                        dataA[1] = itemAh.nom_estado;
                        dataA[2] = itemAh.nom_linea;
                        dataA[3] = Convert.ToDateTime(itemAh.fecha_apertura).ToShortDateString();
                        dataA[4] = Convert.ToDecimal(Math.Truncate(itemAh.saldo_total)).ToString().Replace("$", "").Replace(".", "");
                        dataA[5] = Convert.ToDecimal(itemAh.saldo_canje).ToString().Replace("$", "").Replace(".", "");
                        dataA[6] = itemAh.nom_oficina;
                        dataA[7] = Convert.ToDecimal(itemAh.valor_cuota).ToString().Replace("$", "").Replace(".", "");
                        dataA[8] = Convert.ToDateTime(itemAh.fecha_proximo_pago).ToShortDateString();
                        dataA[9] = Convert.ToDecimal(Math.Truncate(itemAh.valor_acumulado)).ToString().Replace("$", "").Replace(".", "");
                        dataA[10] = Convert.ToDecimal(Math.Truncate(itemAh.valor_total_acumu)).ToString().Replace("$", "").Replace(".", "");
                        tableAhorro.Rows.Add(dataA);
                    }
                }
            }

            // Cargar en un datatable la información de los ahorros programados
            System.Data.DataTable tableAhoPro = new System.Data.DataTable();
            tableAhoPro.Columns.Add("numero_programado");
            tableAhoPro.Columns.Add("estado");
            tableAhoPro.Columns.Add("plazo");
            tableAhoPro.Columns.Add("cuotas_pagas");
            tableAhoPro.Columns.Add("linea");
            tableAhoPro.Columns.Add("fecha_apertura");
            tableAhoPro.Columns.Add("valor_cuota");
            tableAhoPro.Columns.Add("fecha_ult_mov");
            tableAhoPro.Columns.Add("fecha_proximo_pago");
            tableAhoPro.Columns.Add("fecha_vencimiento");
            tableAhoPro.Columns.Add("nom_oficina");
            tableAhoPro.Columns.Add("periocidad");
            tableAhoPro.Columns.Add("tasa_interes");
            tableAhoPro.Columns.Add("saldo_total");
            tableAhoPro.Columns.Add("valor_acumulado");
            tableAhoPro.Columns.Add("valor_total_acumu");


            List<Xpinn.Programado.Entities.CuentasProgramado> lstAhoPro = new List<Xpinn.Programado.Entities.CuentasProgramado>();
            lstAhoPro = (List<Xpinn.Programado.Entities.CuentasProgramado>)Session["AHOPRO"];
            if (lstAhoPro != null)
            {
                if (lstAhoPro.Count > 0)
                {
                    foreach (Xpinn.Programado.Entities.CuentasProgramado itemAh in lstAhoPro)
                    {
                        DataRow dataAP;
                        dataAP = tableAhoPro.NewRow();
                        dataAP[0] = itemAh.numero_programado;
                        dataAP[1] = itemAh.nom_estado;
                        dataAP[2] = itemAh.plazo;
                        dataAP[3] = itemAh.cuotas_pagadas;
                        dataAP[4] = itemAh.nomlinea;
                        dataAP[5] = Convert.ToDateTime(itemAh.fecha_apertura).ToShortDateString();
                        dataAP[6] = Convert.ToDecimal(itemAh.valor_cuota).ToString().Replace("$", "").Replace(".", "");
                        dataAP[7] = Convert.ToDateTime(itemAh.fecha_ultimo_pago).ToShortDateString();
                        dataAP[8] = Convert.ToDateTime(itemAh.fecha_proximo_pago).ToShortDateString();
                        dataAP[9] = Convert.ToDateTime(itemAh.fecha_vencimiento).ToShortDateString();
                        dataAP[10] = itemAh.nomoficina;
                        dataAP[11] = itemAh.nom_periodicidad;
                        dataAP[12] = Convert.ToDecimal(itemAh.tasa_interes);
                        dataAP[13] = Convert.ToDecimal(itemAh.saldo).ToString().Replace("$", "").Replace(".", "");
                        dataAP[14] = Convert.ToDecimal(itemAh.valor_acumulado).ToString().Replace("$", "").Replace(".", "");
                        dataAP[15] = Convert.ToDecimal(itemAh.valor_total_acumu).ToString().Replace("$", "").Replace(".", "");

                        tableAhoPro.Rows.Add(dataAP);

                    }
                }
            }
            //Carga Cuotas Extras 
            System.Data.DataTable tablaCuotasExtras = new System.Data.DataTable();
            tablaCuotasExtras.Columns.Add("Numero_Radicacion");
            tablaCuotasExtras.Columns.Add("Fecha_Cuota");
            tablaCuotasExtras.Columns.Add("Valor");
            tablaCuotasExtras.Columns.Add("Forma_Pago");
            tablaCuotasExtras.Columns.Add("Tipo_Cuota_Extra");

            List<Xpinn.FabricaCreditos.Entities.CuotasExtras> lstCuotasExtras = new List<Xpinn.FabricaCreditos.Entities.CuotasExtras>();
            List<Producto> lstCreditos = new List<Producto>();
            lstCreditos = (List<Producto>)Session["DTCREDITOS"];


            if (lstCreditos != null)
            {
                if (lstCreditos.Count > 0)
                {
                    foreach (Producto item in lstCreditos)
                    {
                        CuotasExtras eCuoExt = new CuotasExtras();
                        eCuoExt.numero_radicacion = item.NumRadicion;
                        lstCuotasExtras = CuoExtServicio.ListarCuotasExtras(eCuoExt, (Usuario)Session["Usuario"]);
                        foreach (CuotasExtras items in lstCuotasExtras.Where(x => x.saldo_capital > 0).ToList())
                        {
                            DataRow dataCE;
                            dataCE = tablaCuotasExtras.NewRow();

                            dataCE[0] = items.numero_radicacion;
                            dataCE[1] = Convert.ToDateTime(items.fecha_pago).ToString("dd/MM/yyyy");
                            dataCE[2] = Convert.ToInt64(items.valor);
                            dataCE[3] = items.des_forma_pago;
                            dataCE[4] = items.des_tipo_cuota;

                            tablaCuotasExtras.Rows.Add(dataCE);
                        }
                    }
                }
            }

            // Determinar el logo de la empresa
            string cRutaDeImagen;
            cRutaDeImagen = Server.MapPath("~/Images\\") + "LogoEmpresa.jpg";

            mvReporte.Visible = true;

            // Cargar los parámetros del reporte
            Usuario pUsu = (Usuario)Session["usuario"];
            ReportParameter[] param = new ReportParameter[39];
            param[0] = new ReportParameter("Entidad", pUsu.empresa);
            param[1] = new ReportParameter("NumeroDocumento", " " + txtNumDoc.Text);
            param[2] = new ReportParameter("IdPersona", txtCodigo.Text);
            param[3] = new ReportParameter("nombre", txtPrimerNombre.Text + " " + txtPrimerApellido.Text);
            if (txtFechaAfiliacion.Text == "")
                param[4] = new ReportParameter("fechaingreso", "  ");
            else
                param[4] = new ReportParameter("fechaingreso", txtFechaAfiliacion.Text);
            param[5] = new ReportParameter("tipocliente", txtestado.Text);
            param[6] = new ReportParameter("telefono", txtTelefono.Text);
            param[7] = new ReportParameter("direccion", txtDireccion.Text);
            if (txtPagadurias.Text == "")
                txtPagadurias.Text = " ";
            param[8] = new ReportParameter("tipoidentificacion", txtPagadurias.Text);
            if (txtTotalSaldosrp.Text == "")
                txtTotalSaldosrp.Text = "0";
            param[9] = new ReportParameter("totalsaldos", txtTotalSaldosrp.Text);
            if (txtTotalCoutasCredrp.Text == "")
                txtTotalCoutasCredrp.Text = "0";
            param[10] = new ReportParameter("totalcuotas", txtTotalCoutasCredrp.Text);
            if (txtVlrPendienteAparep.Text == "")
                txtVlrPendienteAparep.Text = "0";
            param[11] = new ReportParameter("vlrpendientexpagar", txtVlrPendienteAparep.Text);
            param[12] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now.ToShortDateString()));
            if (txtTotalSaldos.Text == "")
                txtTotalSaldos.Text = "0";
            param[13] = new ReportParameter("total1", Convert.ToDecimal(txtTotalSaldos.Text).ToString("n0"));
            if (txtTotalCoutasCreditos.Text == "")
                txtTotalCoutasCreditos.Text = "0";
            param[14] = new ReportParameter("total2", Convert.ToDecimal(txtTotalCoutasCreditos.Text).ToString("n0"));
            param[15] = new ReportParameter("total3", Convert.ToDecimal(Label3.Text).ToString("n0"));
            if (txtVlrPendienteApagar.Text == "")
                txtVlrPendienteApagar.Text = "0";
            param[16] = new ReportParameter("total4", Convert.ToDecimal(txtVlrPendienteApagar.Text).ToString("n0"));
            param[17] = new ReportParameter("total5", Convert.ToDecimal(Label5.Text).ToString("n0"));
            if (tableAporte.Rows.Count > 0)
                param[18] = new ReportParameter("ReporteAporte", "False");
            else
                param[18] = new ReportParameter("ReporteAporte", "True");
            String ReporteDevolucion = "true";
            if (chkImprimirDevolucion.Checked)
                if (gvDevoluciones.Rows.Count > 0)
                    ReporteDevolucion = "false";
                else
                    ReporteDevolucion = "true";

            param[19] = new ReportParameter("ReporteDevolucion", ReporteDevolucion);
            param[20] = new ReportParameter("fechahora", " " + DateTime.Now);
            param[21] = new ReportParameter("usuarioGenera", " " + pUsu.nombre);
            param[22] = new ReportParameter("ImagenReport", cRutaDeImagen);

            String ReporteAhorro = "";
            if (gvAhorros.Rows.Count > 0)
                ReporteAhorro = "false";
            else
                ReporteAhorro = "true";
            param[23] = new ReportParameter("ReporteAhorro", ReporteAhorro);

            String ReporteAhoPro = "";
            if (gvAhoProgra.Rows.Count > 0)
                ReporteAhoPro = "false";
            else
                ReporteAhoPro = "true";
            param[24] = new ReportParameter("ReporteAhoPro", ReporteAhoPro);

            String ReporteAcodeudados = "";
            if (gvAcodeudados.Rows.Count > 0)
                ReporteAcodeudados = "false";
            else
                ReporteAcodeudados = "true";
            param[25] = new ReportParameter("ReporteAcodeudados", ReporteAcodeudados);

            param[26] = new ReportParameter("total6", " " + txtTotalCuotasAportes.Text);
            param[27] = new ReportParameter("total7", " " + txtTotalAportes.Text);
            param[28] = new ReportParameter("total8", " " + txtAportesCuotas.Text);
            if (txtDevolucionestotal.Text != "")
            {
                param[29] = new ReportParameter("total9", " " + Convert.ToDecimal(txtDevolucionestotal.Text).ToString("##,##").Trim());
            }
            else
            {
                param[29] = new ReportParameter("total9", "0");
            }
            param[30] = new ReportParameter("motivo", " " + txtMotivo.Text);
            param[31] = new ReportParameter("Saldo_Afiliacion", " " + txtSaldoAfili.Text);
            param[32] = new ReportParameter("Fecha_Retiro", " " + txtFechaRetiro.Text);
            param[33] = new ReportParameter("CiudadDireccion", " " + txtCiudad.Text);
            string Reporte = string.Empty;
            if (tableCdat.Rows.Count > 0)
                Reporte = "False";
            else
                Reporte = "True";
            param[34] = new ReportParameter("ReporteCdat", Reporte);
            Reporte = string.Empty;
            if (tableServi.Rows.Count > 0)
                Reporte = "False";
            else
                Reporte = "True";
            param[35] = new ReportParameter("ReporteServi", Reporte);
            param[36] = new ReportParameter("zona", " " + txtZona.Text);

            String ReporteTelefonia = "";
            if (gvLineas.Rows.Count > 0)
                ReporteTelefonia = "false";
            else
                ReporteTelefonia = "true";
            param[37] = new ReportParameter("ReporteTelefonia", ReporteTelefonia);

            Xpinn.Comun.Entities.General General = new Xpinn.Comun.Entities.General();
            General = ConsultarParametroGeneral(7, Usuario);
            if (General.valor == "0" || General.valor == "")
                param[38] = new ReportParameter("ColumCausado", "False");
            else
                param[38] = new ReportParameter("ColumCausado", "True");


            General = ConsultarParametroGeneral(90167, Usuario);
            if (General.valor == "1")
                RpviewEstado.LocalReport.ReportPath = @"Page\Asesores\EstadoCuenta\ReporteEstadodeCuentaVer.rdlc";
            else
                RpviewEstado.LocalReport.ReportPath = @"Page\Asesores\EstadoCuenta\ReporteEstadodeCuenta.rdlc";



            // Cargar los dataset al reporte para poderlos generar
            RpviewEstado.LocalReport.EnableExternalImages = true;
            RpviewEstado.LocalReport.SetParameters(param);
            RpviewEstado.LocalReport.DataSources.Clear();

            ReportDataSource rds = new ReportDataSource("DataSet7", CrearDataTable(idObjeto)); // Crèditos
            RpviewEstado.LocalReport.DataSources.Add(rds);
            ReportDataSource rds1 = new ReportDataSource("DataSet2", tableAporte);
            RpviewEstado.LocalReport.DataSources.Add(rds1);
            ReportDataSource rds2 = new ReportDataSource("DataSet3", tableDevolucion);
            RpviewEstado.LocalReport.DataSources.Add(rds2);
            ReportDataSource rds4 = new ReportDataSource("DataSet4", tableAhorro);
            RpviewEstado.LocalReport.DataSources.Add(rds4);
            ReportDataSource rds5 = new ReportDataSource("DataSet5", tableAhoPro);
            RpviewEstado.LocalReport.DataSources.Add(rds5);
            ReportDataSource rds6 = new ReportDataSource("DataSet6", tableAcodeudados);
            RpviewEstado.LocalReport.DataSources.Add(rds6);
            ReportDataSource rds7 = new ReportDataSource("DataSet8", tableCdat);
            RpviewEstado.LocalReport.DataSources.Add(rds7);
            ReportDataSource rds8 = new ReportDataSource("DataSet9", tableServi);
            RpviewEstado.LocalReport.DataSources.Add(rds8);
            ReportDataSource rds10 = new ReportDataSource("DataSet10", tableLineas);
            RpviewEstado.LocalReport.DataSources.Add(rds10);
            ReportDataSource rds9 = new ReportDataSource("DataSet1", tablaCuotasExtras);
            RpviewEstado.LocalReport.DataSources.Add(rds9);

            string ident = txtNumDoc.Text != "" ? "_" + txtNumDoc.Text : "";

            RpviewEstado.ServerReport.DisplayName = ident;
            RpviewEstado.LocalReport.DisplayName = ident;
            RpviewEstado.LocalReport.Refresh();

            frmPrint.Visible = false;
            RpviewEstado.Visible = true;
            mvReporte.Visible = true;
            mvReporte.ActiveViewIndex = 0;
            Tabs.Visible = false;
            Site ToolBar = (Site)Master;
            ToolBar.MostrarImprimir(false);
        }
        else
        {
            Tabs.Visible = true;
            mvReporte.Visible = false;
            RpviewEstado.Visible = false;
        }
    }

    /// <summary>
    /// Crear el DataTable para poder generar el reporte
    /// </summary>
    /// <param name="pIdObjeto"></param>
    /// <returns></returns>
    public DataTable CrearDataTable(String pIdObjeto)
    {
        System.Data.DataTable table = new System.Data.DataTable();

        table.Columns.Add("Estado");
        table.Columns.Add("NumRadicion");
        table.Columns.Add("Linea");
        table.Columns.Add("FechaSolicitud");
        table.Columns.Add("MontoAprobado");
        table.Columns.Add("SaldoCapital");
        table.Columns.Add("Cuota");
        table.Columns.Add("Atributos");
        table.Columns.Add("Plazo");
        table.Columns.Add("CtaPagadas");
        table.Columns.Add("FechaProximoPago");
        table.Columns.Add("Vlrapagar");
        table.Columns.Add("Vlrtotalapagar");
        table.Columns.Add("CalificacionProm");
        table.Columns.Add("Oficina");
        table.Columns.Add("NumRadicacion");
        table.Columns.Add("CodPersona");
        table.Columns.Add("vrDsctoNomina");
        table.Columns.Add("pagadurias");
        table.Columns.Add("FechaVencimiento");
        table.Columns.Add("CupoDisponible");

        List<Producto> lstEstadoCuenta = new List<Producto>();
        lstEstadoCuenta = (List<Producto>)Session["DTCREDITOS"];
        if (lstEstadoCuenta != null)
        {
            foreach (Producto item in lstEstadoCuenta)
            {
                Acodeudados variable = new Acodeudados();
                DataRow datarw;
                ActualizarTablaAcodeudados();
                datarw = table.NewRow();
                datarw[0] = item.Estado + " " + item.Proceso;
                datarw[1] = item.NumRadicion;
                datarw[2] = item.Linea;
                datarw[3] = item.FechaSolicitud.ToString(gFormatoFecha);
                datarw[4] = Convert.ToDecimal(item.MontoAprobado).ToString().Replace("$", "").Replace(".", "").Replace(",", "");
                datarw[5] = Convert.ToDecimal(item.SaldoCapital).ToString().Replace("$", "").Replace(".", "").Replace(",", "");
                datarw[6] = Convert.ToDecimal(item.Cuota).ToString().Replace("$", "").Replace(".", "").Replace(",", "");
                datarw[7] = Convert.ToDecimal(item.Atributos).ToString().Replace("$", "").Replace(".", "").Replace(",", "");
                datarw[8] = item.Plazo;
                datarw[9] = item.CuotasPagadas;
                datarw[10] = item.FechaProximoPago.ToShortDateString();
                datarw[11] = Convert.ToDecimal(item.ValorAPagar).ToString().Replace("$", "").Replace(".", "").Replace(",", "");
                datarw[12] = Convert.ToDecimal(item.ValorTotalAPagar).ToString().Replace("$", "").Replace(".", "").Replace(",", "");
                datarw[13] = item.Tasainteres;
                datarw[14] = item.Oficina.NombreOficina;
                datarw[15] = variable.NumRadicacion.ToString("##,##0");
                datarw[16] = variable.CodPersona.ToString("##,##0");
                datarw[17] = Convert.ToDecimal(item.vr_ult_dscto).ToString().Replace("$", "").Replace(".", "").Replace(",", "");

                datarw[18] = " " + item.pagadurias;
                datarw[19] = Convert.ToDateTime(item.FechaVencimiento).ToString(gFormatoFecha);
                datarw[20] = Convert.ToDecimal(item.Disponible).ToString().Replace("$", "").Replace(".", "").Replace(",", "");
                table.Rows.Add(datarw);
            }
        }

        return table;
    }

    protected void btnImprime_Click(object sender, EventArgs e)
    {
        if (mvReporte.Visible == true)
        {
            //MOSTRAR REPORTE EN PANTALLA
            string ident = txtNumDoc.Text != "" ? "_" + txtNumDoc.Text : "";

            RpviewEstado.ServerReport.DisplayName = ident;
            var bytes = RpviewEstado.LocalReport.Render("PDF");

            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline;attachment; filename=output" + ident + ".pdf");
            Response.BinaryWrite(bytes);
            Response.Flush(); // send it to the client to download
            Response.Clear();

            //Warning[] warnings;
            //string[] streamids;
            //string mimeType;
            //string encoding;
            //string extension;
            //try
            //{
            //    byte[] bytes = RpviewEstado.LocalReport.Render("PDF", null, out mimeType,
            //                   out encoding, out extension, out streamids, out warnings);
            //    FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("output" + ident  + ".pdf"),
            //    FileMode.Create);
            //    fs.Write(bytes, 0, bytes.Length);
            //    fs.Close();
            //}
            //catch
            //{
            //    VerError("No se pudo imprimir el reporte");
            //    return;
            //}
            //Session["Archivo"] = Server.MapPath("output" + ident + ".pdf");
            //frmPrint.Visible = true;
            //RpviewEstado.Visible = false;

        }
    }

    /// <summary>
    /// Para devolverse al generar el reporte
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAtras_Click(object sender, EventArgs e)
    {
        Site ToolBar = (Site)Master;
        ToolBar.MostrarImprimir(true);
        Tabs.Visible = true;
        mvReporte.Visible = false;
    }

    #endregion

    #region Mostrar información de los servicios
    protected void ActualizarTablaServicios()
    {
        try
        {
            // Mostrar información de los servicios
            try
            {
                Int64 cliente = Convert.ToInt64(txtCodigo.Text);

                EstadoCuenta estadoCuenta = new EstadoCuenta();// { Codigo = Convert.ToInt64(txtCodigo.Text), cerrado = serviciosCerrados };
                estadoCuenta.Codigo = cliente;
                estadoCuenta.cerrado = chkBoxServiciosCerrados.Checked;
                estadoCuenta.anulado = "N";

                List<EstadoCuenta> lstConsulta = serviceEstadoCuenta.consultarServicios(estadoCuenta, Usuario);

                gridServicios.PageSize = pageSize;
                gridServicios.EmptyDataText = emptyQuery;


                if (lstConsulta.Count > 0)
                {
                    gridServicios.DataSource = lstConsulta;
                    gridServicios.Visible = true;
                    lblTotalRegistrosServicios.Visible = true;
                    lblTotalRegistrosServicios.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                    // ViewState["SERVICIOS"] = lstConsulta;
                    lblinfoServicios.Visible = false;
                    gridServicios.DataBind();
                    ValidarPermisosGrilla(gridServicios);
                    Session["SERVICIOS"] = lstConsulta;



                }
                else
                {
                    //  ViewState["SERVICIOS"] = null;
                    gridServicios.DataSource = null;
                    gridServicios.Visible = false;
                    Session["SERVICIOS"] = null;
                    lblinfoServicios.Visible = false;
                    lblTotalRegistrosServicios.Visible = false;


                    // totales 
                    lbltotalValorInicialServicios.Visible = false;
                    lblTotalCuotasServicios.Visible = false;
                    lbltotalServicios.Visible = false;
                    txtServiciosCuotas.Visible = false;
                    txtServiciosTotal.Visible = false;
                    txtTotalServicios.Visible = false;
                    txtTotalCuotasServicios.Visible = false;
                    txtServiciosvalorInicial.Visible = false;
                    txtTotalValorInicialServicios.Visible = false;
                }



                // totalizar 

                decimal servicios = 0;
                decimal totalcuotasservicios = 0;
                decimal totalvalorinicial = 0;
                foreach (GridViewRow rfila in gridServicios.Rows)
                {

                    if (rfila.Cells[3].Text != "" && rfila.Cells[3].Text != "&nbsp;")
                        totalvalorinicial += Convert.ToDecimal(rfila.Cells[3].Text.Replace("$", "").Replace(".", "").Replace(",", ""));

                    if (rfila.Cells[11].Text != "" && rfila.Cells[1].Text != "&nbsp;")
                        servicios += Convert.ToDecimal(rfila.Cells[11].Text.Replace("$", "").Replace(".", "").Replace(",", ""));
                    if (rfila.Cells[12].Text != "" && rfila.Cells[12].Text != "&nbsp;")
                        totalcuotasservicios += Convert.ToDecimal(rfila.Cells[12].Text.Replace("$", "").Replace(".", "").Replace(",", ""));

                    txtTotalServicios.Text = Convert.ToString(servicios.ToString("###,###"));
                    txtTotalCuotasServicios.Text = Convert.ToString(totalcuotasservicios.ToString("###,###"));
                    txtTotalValorInicialServicios.Text = Convert.ToString(totalvalorinicial.ToString("###,###"));

                }
                /// Session.Add(serviceEstadoCuenta.CodigoPrograma + ".consulta", 1);
            }
            catch (Exception ex)
            {
                BOexcepcion.Throw(serviceEstadoCuenta.CodigoPrograma, "Actualizar", ex);
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }

    }

    protected void gridServicios_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        GridView gvListado = (GridView)sender;
        Xpinn.Servicios.Services.AprobacionServiciosServices ExcluServicios = new Xpinn.Servicios.Services.AprobacionServiciosServices();
        Session[ExcluServicios.CodigoProgramaReporteMovimiento + ".id"] = evt.CommandArgument.ToString();
        if (evt.CommandName == "MovGralServicios") Navegar("~/Page/Servicios/ReporteMovimiento/Nuevo.aspx");
    }

    protected void gridServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridServicios.PageIndex = e.NewPageIndex;
            ActualizarTablaServicios();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.CodigoPrograma, "gridServicios_PageIndexChanging", ex);
        }
    }

    protected void chkBoxServiciosCerrados_CheckedChanged(object sender, EventArgs e)
    {

        ActualizarTablaServicios();
    }

    #endregion

    #region Mostrar la información de los Créditos Acodeudados

    protected void gvAcodeudados_PageIndexChanging(object sender, GridViewPageEventArgs evt)
    {
        try
        {
            gvAcodeudados.PageIndex = evt.NewPageIndex;
            ActualizarTablaAcodeudados();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.GetType().Name + "L", "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Cargar tabla de créditos acodeudados
    /// </summary>
    private void ActualizarTablaAcodeudados()
    {
        List<Acodeudados> pAcodeudados = new List<Acodeudados>();
        pAcodeudados = acodeudadosservices.ListarAcodeudadoss(new Cliente() { IdCliente = Convert.ToInt64(txtCodigo.Text) }, (Usuario)Session["usuario"]);

        var acodeudados = from c in pAcodeudados
                          select new
                          {
                              c.CodPersona,
                              Cuota = c.Cuota.ToString("##,##0", CultureInfo.InvariantCulture),
                              c.Estado,
                              FechaProxPago = c.FechaProxPago.ToShortDateString(),
                              c.Linea,
                              Monto = c.Monto.ToString("##,##0", CultureInfo.InvariantCulture),
                              c.Nombres,
                              c.NumRadicacion,
                              c.identificacion,
                              Saldo = c.Saldo.ToString("##,##0", CultureInfo.InvariantCulture),
                              c.Valor_apagar,
                              c.Estado_Codeudor
                          };

        acodeudados = acodeudados.OrderBy(c => c.NumRadicacion).ToList();

        gvAcodeudados.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
        gvAcodeudados.DataSource = acodeudados;

        if (acodeudados.Count() > 0)
        {
            gvAcodeudados.Visible = true;
            lblInfoss.Visible = false;
            lblTotalRegss.Visible = true;
            lblTotalRegss.Text = "<br/> Registros encontrados " + acodeudados.Count().ToString();
            gvAcodeudados.DataBind();
            ValidarPermisosGrilla(gvAcodeudados);
        }
        else
        {
            gvAcodeudados.Visible = false;
            lblTotalRegss.Text = "<br/> Registros encontrados " + acodeudados.Count().ToString();
        }

    }

    #endregion

    #region Mostrar datos de la afiliación
    /// <summary>
    /// Mostrar información de la afiliación de la persona
    /// </summary>
    /// <param name="cod_persona"></param>
    private void ObtenerDatosAfiliacion(Int64 cod_persona)
    {
        Afiliacion pAfili = new Afiliacion();
        pAfili = AfiliacionServicio.ConsultarAfiliacion(cod_persona, (Usuario)Session["usuario"]);

        if (pAfili.idafiliacion != 0)
        {
            try
            {
                pDatosAfiliacion.Visible = true;
                txtcodAfiliacion.Text = HttpUtility.HtmlDecode(Convert.ToString(pAfili.idafiliacion));
                if (pAfili.fecha_afiliacion != DateTime.MinValue)
                    txtFechaAfili.Text = HttpUtility.HtmlDecode(Convert.ToString(pAfili.fecha_afiliacion.ToString(gFormatoFecha)));
                if (pAfili.estado != "")
                    ddlEstadoAfi.SelectedValue = pAfili.estado;
                if (pAfili.fecha_retiro != DateTime.MinValue)
                    txtFechaRetiro.Text = Convert.ToString(pAfili.fecha_retiro.ToString(gFormatoFecha));
                if (pAfili.valor != 0)
                    txtValorAfili.Text = Convert.ToString(pAfili.valor);
                if (pAfili.saldo != 0)
                    txtSaldoAfili.Text = Convert.ToString(pAfili.saldo);
                if (pAfili.fecha_primer_pago != null)
                    txtFecha1Pago.Text = Convert.ToString(pAfili.fecha_primer_pago.Value.ToString(gFormatoFecha));
                if (pAfili.fecha_proximo_pago != null)
                    txtFechaProxPago.Text = Convert.ToString(pAfili.fecha_proximo_pago.Value.ToString(gFormatoFecha));
                if (pAfili.cuotas != 0)
                    txtCuotasAfili.Text = Convert.ToString(pAfili.cuotas);
                if (pAfili.cod_periodicidad != 0)
                    ddlPeriodicidad.SelectedValue = Convert.ToString(pAfili.cod_periodicidad);
                if (pAfili.empresa_formapago != 0)
                    ddlEmpresa.SelectedValue = Convert.ToString(pAfili.empresa_formapago);
                if (pAfili.forma_pago != 0)
                    ddlFormaPago.Value = Convert.ToString(pAfili.forma_pago);
                txtFechaActualizacion.Text = Convert.ToString(pAfili.FechaActualizacion.ToString(gFormatoFecha)) ;
                // Llenar listado de movimientos de afiliacion
                List<Xpinn.Aportes.Entities.TranAfiliacion> lstTranAfilia = new List<Xpinn.Aportes.Entities.TranAfiliacion>();
                Xpinn.Aportes.Entities.TranAfiliacion tranAfilia = new Xpinn.Aportes.Entities.TranAfiliacion();
                tranAfilia.cod_persona = cod_persona;
                lstTranAfilia = AfiliacionServicio.ListarMovAfiliacion(tranAfilia, (Usuario)Session["usuario"]);
                if (lstTranAfilia.Count <= 0)
                {
                    lblTotalRegsAfi.Visible = true;
                    lblTotalRegsAfi.Text = "No hay movimiento de afiliación";
                }
                gvMovAfiliacion.DataSource = lstTranAfilia;
                gvMovAfiliacion.DataBind();

            }
            catch
            {
                VerError("..");
            }
        }
        else
        {
            pDatosAfiliacion.Visible = false;
        }
    }
    #endregion

    #region Mostrar la información de los aportes
    /// <summary>
    /// Mostrar la información de los aportes
    /// </summary>
    private void ActualizarTablaAportes()
    {

        try
        {
            Int64 cliente = Convert.ToInt64(txtCodigo.Text);
            List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            string estado = chkaporterminados.Checked ? "1,2,3" : "1,2";
            lstConsulta = AporteServicio.ListarEstadoCuentaAportestodos(cliente, estado, DateTime.Now, (Usuario)Session["usuario"]);
            /********Validar repetidos***/
            List<Xpinn.Aportes.Entities.Aporte> lstConsultaFiltrada = new List<Xpinn.Aportes.Entities.Aporte>();
            foreach (Xpinn.Aportes.Entities.Aporte aporte in lstConsulta)
            {
                if (lstConsultaFiltrada.Count == 0)
                {
                    lstConsultaFiltrada.Add(aporte);
                }
                else
                {
                    if (lstConsultaFiltrada.Exists(x => x.numero_aporte == aporte.numero_aporte))
                        lstConsultaFiltrada.RemoveAll(x => x.numero_aporte == aporte.numero_aporte);
                    lstConsultaFiltrada.Add(aporte);
                }
            }
            string clasificacion = ""; // AporteServicio.ClasificacionAporte(cliente, (Usuario)Session["usuario"]);
            txtClasificacion.Text = clasificacion;
            gvListaAporte.PageSize = pageSize;
            gvListaAporte.EmptyDataText = emptyQuery;
            gvListaAporte.DataSource = lstConsultaFiltrada;

            if (lstConsultaFiltrada.Count > 0)
            {
                gvListaAporte.Visible = true;
                lblInfoAporte.Visible = false;
                lblTotalRegsAporte.Visible = true;
                lblTotalRegsAporte.Text = "<br/> Registros encontrados " + lstConsultaFiltrada.Count.ToString();
                gvListaAporte.DataBind();
                gvAcodeudados.Visible = true;

                Session["APORTES"] = lstConsultaFiltrada;
            }
            else
            {
                gvListaAporte.Visible = false;
                lblInfoAporte.Visible = true;
                lblTotalRegsAporte.Visible = false;
                gvAcodeudados.Visible = false;
                Session["APORTES"] = null;
                txtAportesCuotas.Visible = false;
                txtAportesPendientes.Visible = false;
                txtAPortesPendientesporPagar.Visible = false;
                txtAportesTotal.Visible = false;
                txtAportesPendientes.Visible = false;
                txtTotalAportes.Visible = false;
                txtTotalCuotasAportes.Visible = false;
                lblTotalCausacionAporte.Visible = false;
                txtAportesCausacion.Visible = false;
                txtTotalCausacionAportes.Visible = false;
            }

            decimal aportes = 0;
            decimal aportespendientes = 0;
            decimal totalcuotasaportes = 0;
            decimal totalcausacion = 0;
            decimal totalcausmassaldoaportes = 0;
            foreach (GridViewRow rfila in gvListaAporte.Rows)
            {
                if (rfila.Cells[6].Text != "" && rfila.Cells[6].Text != "&nbsp;")
                    aportes += Convert.ToDecimal(rfila.Cells[6].Text.Replace("$", "").Replace(".", "").Replace(",", ""));
                if (rfila.Cells[8].Text != "" && rfila.Cells[8].Text != "&nbsp;")
                    aportespendientes += Convert.ToDecimal(rfila.Cells[8].Text.Replace("$", "").Replace(".", "").Replace(",", ""));
                if (rfila.Cells[7].Text != "" && rfila.Cells[7].Text != "&nbsp;")
                    totalcuotasaportes += Convert.ToDecimal(rfila.Cells[7].Text.Replace("$", "").Replace(".", "").Replace(",", ""));
                if (rfila.Cells[13].Text != "" && rfila.Cells[13].Text != "&nbsp;")
                    totalcausacion += Convert.ToDecimal(rfila.Cells[13].Text.Replace("$", "").Replace(".", "").Replace(",", ""));

                txtTotalAportes.Text = Convert.ToString(aportes.ToString("###,###"));
                txtAPortesPendientesporPagar.Text = Convert.ToString(aportespendientes.ToString("###,###"));
                txtTotalCuotasAportes.Text = Convert.ToString(totalcuotasaportes.ToString("###,###"));
                txtTotalCausacionAportes.Text = Convert.ToString(totalcausacion.ToString("###,###"));


                totalcausmassaldoaportes = aportes + totalcausacion;
                txtTotalCausacionmasAportes.Text = Convert.ToString(totalcausmassaldoaportes.ToString("###,###"));

            }
            Session.Add(AporteServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "ActualizarTablaAportes", ex);
        }
    }


    private string obtFiltro()
    {
        string pFiltro = string.Empty;
        pFiltro += " AND a.estado = 1 and d.cod_cliente = " + txtCodigo.Text;

        if (!string.IsNullOrEmpty(pFiltro))
        {
            pFiltro = pFiltro.Substring(4);
            pFiltro = " WHERE " + pFiltro;
        }
        return pFiltro;
    }

    private void ActualizarAportesPendientes()
    {

        try
        {
            Xpinn.Tesoreria.Services.RecaudosMasivosService RecaudosMasivosServicio = new Xpinn.Tesoreria.Services.RecaudosMasivosService();
            List<Xpinn.Tesoreria.Entities.RecaudosMasivos> lstConsulta = new List<Xpinn.Tesoreria.Entities.RecaudosMasivos>();
            string pFiltro = obtFiltro();
            lstConsulta = RecaudosMasivosServicio.ListarDetalleAportePendientes(pFiltro, (Usuario)Session["Usuario"]);

            gvApoPendiente.DataSource = lstConsulta;
            gvApoPendiente.DataBind();

            pnlPendientes.Visible = false;
            if (lstConsulta.Count > 0)
                pnlPendientes.Visible = true;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "ActualizarTablaAportes", ex);
        }
    }


    private void ActualizarTablaAportesCerrados()
    {
        try
        {
            Int64 cliente = Convert.ToInt64(txtCodigo.Text);
            //  List<Xpinn.Aportes.Entities.Aporte> lstConsulta = new List<Xpinn.Aportes.Entities.Aporte>();
            List<Xpinn.Aportes.Entities.Aporte> lstConsultaCerrada = new List<Xpinn.Aportes.Entities.Aporte>();
            // lstConsulta = (List<Xpinn.Aportes.Entities.Aporte>)Session["APORTES"];
            gvListaAporte.PageSize = pageSize;
            gvListaAporte.EmptyDataText = emptyQuery;


            lstConsultaCerrada = AporteServicio.ListarEstadoCuentaAporte(cliente, 3, DateTime.Now, (Usuario)Session["usuario"]);

            if (lstConsultaCerrada.Count > 0)
            {
                gvListaAporte.Visible = true;
                lblInfoAporte.Visible = false;
                lblTotalRegsAporte.Visible = true;
                lblTotalRegsAporte.Text = "<br/> Registros encontrados " + lstConsultaCerrada.Count.ToString();
                gvListaAporte.DataSource = lstConsultaCerrada;
                gvListaAporte.DataBind();
                gvAcodeudados.Visible = true;

                Session["APORTES"] = lstConsultaCerrada;
            }
            else
            {
                gvListaAporte.Visible = false;
                lblInfoAporte.Visible = true;
                lblTotalRegsAporte.Visible = false;
                gvAcodeudados.Visible = false;
                Session["APORTES"] = null;
                txtAportesCuotas.Visible = false;
                txtAportesPendientes.Visible = false;
                txtAPortesPendientesporPagar.Visible = false;
                txtAportesTotal.Visible = false;
                txtAportesPendientes.Visible = false;
                txtTotalAportes.Visible = false;
                txtTotalCuotasAportes.Visible = false;

            }

            decimal aportes = 0;
            decimal aportespendientes = 0;
            decimal totalcuotasaportes = 0;
            foreach (GridViewRow rfila in gvListaAporte.Rows)
            {
                if (rfila.Cells[6].Text != "" && rfila.Cells[6].Text != "&nbsp;")
                    aportes += Convert.ToDecimal(rfila.Cells[6].Text.Replace("$", "").Replace(".", "").Replace(",", ""));
                if (rfila.Cells[8].Text != "" && rfila.Cells[8].Text != "&nbsp;")
                    aportespendientes += Convert.ToDecimal(rfila.Cells[8].Text.Replace("$", "").Replace(".", "").Replace(",", ""));
                if (rfila.Cells[7].Text != "" && rfila.Cells[7].Text != "&nbsp;")
                    totalcuotasaportes += Convert.ToDecimal(rfila.Cells[7].Text.Replace("$", "").Replace(".", "").Replace(",", ""));

                txtTotalAportes.Text = Convert.ToString(aportes.ToString("###,###"));
                txtAPortesPendientesporPagar.Text = Convert.ToString(aportespendientes.ToString("###,###"));
                txtTotalCuotasAportes.Text = Convert.ToString(totalcuotasaportes.ToString("###,###"));

            }
            Session.Add(AporteServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "ActualizarTablaAportes", ex);
        }
    }



    protected void gvListaAporte_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvListaAporte.DataKeys[gvListaAporte.SelectedRow.RowIndex].Value.ToString();
        Session[AporteServicio.CodigoProgramaMov + ".id"] = id;
        Response.Redirect("~/Page/Aportes/Movimiento/Detalle.aspx");
    }

    protected void gvListaAporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvListaAporte.PageIndex = e.NewPageIndex;
        ActualizarTablaAportes();
    }

    #endregion

    #region Mostrar la información de los ahorros
    /// <summary>
    /// Mostrar información de los ahorros a la vista
    /// </summary>
    private void ActualizarTablaAhorros()
    {
        try
        {
            Xpinn.Ahorros.Services.ReporteMovimientoServices ReporteMovService = new Xpinn.Ahorros.Services.ReporteMovimientoServices();
            Int64 cliente = Convert.ToInt64(txtCodigo.Text);
            List<Xpinn.Ahorros.Entities.AhorroVista> lstConsulta = new List<Xpinn.Ahorros.Entities.AhorroVista>();
            String FiltroEstado = " A.ESTADO NOT IN (3) AND"; // CODIGO 4 => ESTADO CERRADOS  //estados ubicados AHORROVISTA/REPORTEMOVIMIENTO
            if (chkAhorro.Checked)
                FiltroEstado = "";

            String filtro = "WHERE " + FiltroEstado + " A.COD_PERSONA = " + cliente;
            DateTime pFecha = DateTime.Now;
            lstConsulta = ReporteMovService.ListarAhorroVista(filtro, pFecha, (Usuario)Session["usuario"]);

            gvListaAporte.PageSize = 20;
            gvListaAporte.EmptyDataText = emptyQuery;
            if (lstConsulta.Count > 0)
            {
                gvAhorros.DataSource = lstConsulta;
                lblTotalRegsAhorro.Visible = true;
                lblTotalRegsAhorro.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                lblInfoAhorro.Visible = false;
                gvAhorros.DataBind();
                ValidarPermisosGrilla(gvAhorros);
                Session["AHORROS"] = lstConsulta;
            }
            else
            {
                gvAhorros.DataSource = null;
                gvAhorros.DataBind();
                lblTotalRegsAhorro.Visible = false;
                lblInfoAhorro.Visible = true;
                Session["AHORROS"] = null;

                // totales 
                lbltotalAhorros.Visible = false;
                lblTotalCuotasAhorros.Visible = false;
                txtAhorrosCuotas.Visible = false;
                txtAhorrosTotal.Visible = false;
                txtTotalAhorros.Visible = false;
                txtTotalCuotasAhorros.Visible = false;
                lblTotalCausacionAhorros.Visible = false;
                txtTotalCausacionAhorros.Visible = false;
                txtAhorrosCausacion.Visible = false;
                lblTotalCanjeAhorros.Visible = false;
                txtTotalCanjeAhorros.Visible = false;
                lblTotalCausacionmassaldo.Visible = false;
                txtTotalCausacionmassaldo.Visible = false;
                txtAhorrosCausacionmasahorros.Visible = false;
            }


            // totalizar 

            decimal ahorros = 0;
            decimal totalcuotasahorros = 0;
            decimal totalcausacion_ahorros = 0;
            decimal totalcanje_ahorros = 0;
            decimal totalcausmassaldoahorros = 0;
            foreach (GridViewRow rfila in gvAhorros.Rows)
            {
                if (rfila.Cells[5].Text != "" && rfila.Cells[5].Text != "&nbsp;")
                    ahorros += Convert.ToDecimal(rfila.Cells[5].Text.Replace("$", "").Replace(".", "").Replace(",", ""));
                if (rfila.Cells[9].Text != "" && rfila.Cells[9].Text != "&nbsp;")
                    totalcuotasahorros += Convert.ToDecimal(rfila.Cells[9].Text.Replace("$", "").Replace(".", "").Replace(",", ""));


                if (rfila.Cells[12].Text != "" && rfila.Cells[12].Text != "&nbsp;")
                    totalcausacion_ahorros += Convert.ToDecimal(rfila.Cells[12].Text.Replace("$", "").Replace(".", "").Replace(",", ""));

                if (rfila.Cells[6].Text != "" && rfila.Cells[6].Text != "&nbsp;")
                    totalcanje_ahorros += Convert.ToDecimal(rfila.Cells[6].Text.Replace("$", "").Replace(".", "").Replace(",", ""));


                txtTotalAhorros.Text = Convert.ToString(ahorros.ToString("###,###"));
                txtTotalCuotasAhorros.Text = Convert.ToString(totalcuotasahorros.ToString("###,###"));
                txtTotalCausacionAhorros.Text = Convert.ToString(totalcausacion_ahorros.ToString("###,###"));
                txtTotalCanjeAhorros.Text = Convert.ToString(totalcanje_ahorros.ToString("###,###"));

                totalcausmassaldoahorros = ahorros + totalcausacion_ahorros;
                txtTotalCausacionmassaldo.Text = Convert.ToString(totalcausmassaldoahorros.ToString("###,###"));

            }


        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.CodigoPrograma, "ActualizarTablaAhorros", ex);
        }
    }

    protected void gvAhorros_SelectedIndexChanged(object sender, EventArgs e)
    {
        Xpinn.Ahorros.Services.ReporteMovimientoServices ReporteMovService = new Xpinn.Ahorros.Services.ReporteMovimientoServices();
        String id = gvAhorros.DataKeys[gvAhorros.SelectedRow.RowIndex].Value.ToString();
        Session[ReporteMovService.CodigoPrograma + ".id"] = id;
        Response.Redirect("~/Page/AhorrosVista/ReporteMovimiento/Detalle.aspx");
    }


    protected void gvAhorros_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAhorros.PageIndex = e.NewPageIndex;
        ActualizarTablaAhorros();
    }

    protected void chkAhorro_CheckedChanged(object sender, EventArgs e)
    {

        ActualizarTablaAhorros();




    }

    #endregion

    protected void PoblarLista(string pTabla, DropDownList ddlControl)
    {
        PoblarLista(pTabla, "", "", "", ddlControl);
    }

    protected void PoblarLista(string pTabla, string pColumnas, string pCondicion, string pOrden, DropDownList ddlControl)
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

    protected void CargarListasDesplegables()
    {
        ddlEstadoAfi.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEstadoAfi.Items.Insert(1, new ListItem("Activo", "A"));
        ddlEstadoAfi.Items.Insert(2, new ListItem("Retirado", "R"));
        ddlEstadoAfi.Items.Insert(3, new ListItem("Inactivo", "I"));
        ddlEstadoAfi.SelectedIndex = 0;
        ddlEstadoAfi.DataBind();

        PoblarLista("periodicidad", ddlPeriodicidad);

        List<Xpinn.Seguridad.Entities.Opcion> lstOpciones = new List<Xpinn.Seguridad.Entities.Opcion>();
        Xpinn.Seguridad.Data.OpcionData DatosOpcion = new Xpinn.Seguridad.Data.OpcionData();
        Usuario pUsu = (Usuario)Session["usuario"];
        Int32 cod_perfil = Convert.ToInt32(pUsu.codperfil);
        lstOpciones = DatosOpcion.ListarOpcionEstadoCuenta(cod_perfil, (Usuario)Session["usuario"]);
        if (lstOpciones.Count > 0)
        {
            ddlOpcion.DataSource = lstOpciones;
            ddlOpcion.DataTextField = "nombre";
            ddlOpcion.DataValueField = "cod_opcion";
            ddlOpcion.AppendDataBoundItems = true;
            ddlOpcion.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlOpcion.SelectedIndex = 0;
            ddlOpcion.DataBind();
        }

        Usuario pUsuario = new Usuario();
        pUsuario = (Usuario)Session["Usuario"];
        List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
        Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
        Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
        ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudo(empresa, pUsuario);
        ddlEmpresa.DataTextField = "nom_empresa";
        ddlEmpresa.DataValueField = "cod_empresa";
        ddlEmpresa.AppendDataBoundItems = true;
        ddlEmpresa.Items.Insert(0, new ListItem("", "0"));
        ddlEmpresa.SelectedIndex = 0;
        ddlEmpresa.DataBind();
        if (pUsuario.codperfil == 0 || pUsuario.codperfil == 1)
        {
            chkExtend.Visible = true;
            txtExtendQuery.Visible = true;
            chkExtend_CheckedChanged(chkExtend, null);
        }
    }

    #region Mostrar la información de las devoluciones
    /// <summary>
    /// Mostrar datos de las devoluciones
    /// </summary>
    private void ActualizarTablaDevolucion()
    {
        Xpinn.Tesoreria.Services.DevolucionServices devolucionServicio = new Xpinn.Tesoreria.Services.DevolucionServices();
        try
        {
            Int64 cliente = Convert.ToInt64(txtCodigo.Text);
            Xpinn.Tesoreria.Entities.Devolucion devol = new Xpinn.Tesoreria.Entities.Devolucion();
            List<Xpinn.Tesoreria.Entities.Devolucion> lstConsulta = new List<Xpinn.Tesoreria.Entities.Devolucion>();

            if (cheksaldo.Checked)
                lstConsulta = devolucionServicio.ListarDevolucion(devol, DateTime.MinValue, (Usuario)Session["usuario"], " And p.identificacion = '" + txtNumDoc.Text + "' ");
            else
                lstConsulta = devolucionServicio.ListarDevolucion(devol, DateTime.MinValue, (Usuario)Session["usuario"], " And p.identificacion = '" + txtNumDoc.Text + "' and d.saldo <> 0 ");

            gvDevoluciones.PageSize = pageSize;
            gvDevoluciones.EmptyDataText = emptyQuery;
            gvDevoluciones.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvDevoluciones.Visible = true;
                lblInfoDev.Visible = false;
                lblTotalRegsDev.Visible = true;
                lblTotalRegsDev.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvDevoluciones.DataBind();
                Session["DEVOLUCION"] = lstConsulta;
                tabDevoluciones.Visible = true;
                lblDevoluciones.Visible = true;
                txtDevolucionestotal.Visible = true;
            }
            else
            {
                gvDevoluciones.Visible = false;
                lblInfoDev.Visible = true;
                lblTotalRegsDev.Visible = false;
                Session["DEVOLUCION"] = null;
                txtDevolucionestotal.Visible = false;
                lblDevoluciones.Visible = false;
                lblInfoDev.Visible = false;
                lblTotalRegsDev.Visible = false;
            }
            decimal devoluciones = 0;
            foreach (GridViewRow ifila in gvDevoluciones.Rows)
            {

                devoluciones += Convert.ToDecimal(gvDevoluciones.DataKeys[ifila.RowIndex].Values[1].ToString());
                txtDevolucionestotal.Text = devoluciones.ToString("##,##");

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(devolucionServicio.CodigoPrograma, "ActualizarTablaDevolucion", ex);
        }

    }

    protected void gvDevoluciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvDevoluciones.DataKeys[gvDevoluciones.SelectedRow.RowIndex].Values[0].ToString();
    }

    protected void gvDevoluciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvDevoluciones.PageIndex = e.NewPageIndex;
            ActualizarTablaDevolucion();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.CodigoPrograma, "gridServicios_PageIndexChanging", ex);
        }
    }

    #endregion

    #region Manejo de los links en la barra superior del estado de cuenta
    //AGREGADO DE LINKS DINAMICOS
    Usuario_LinkService LinkService = new Usuario_LinkService();

    protected void btnAgregarLink_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        mpeAgregar.Show();
    }

    protected void btnParar_Click(object sender, EventArgs e)
    {
        mpeAgregar.Hide();
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            if (validarEmergente())
            {
                Usuario_Link pLink = new Usuario_Link();
                Usuario pUsu = (Usuario)Session["usuario"];

                pLink.idlink = 0;
                pLink.codusuario = Convert.ToInt32(pUsu.codusuario);
                pLink.cod_opcion = Convert.ToInt32(ddlOpcion.SelectedValue);
                pLink.nombre = !string.IsNullOrEmpty(txtExtendQuery.Text) && txtExtendQuery.Visible == true ? txtNombreParametro.Text.ToUpper() + "|?codigo=" + txtExtendQuery.Text.Trim() : txtNombreParametro.Text.ToUpper();
                LinkService.CrearUsuario_Link(pLink, (Usuario)Session["usuario"]);
                //Forzar el postback para que actualize los links creados
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                mpeAgregar.Hide();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "btnContinuar_Click", ex);
        }
    }

    private Boolean validarEmergente()
    {
        if (txtNombreParametro.Text == "")
        {
            lblError.Text = "Ingrese el Nombre del Link a crear.";
            return false;
        }
        if (ddlOpcion.SelectedIndex == 0)
        {
            lblError.Text = "Seleccione una Opcion";
            return false;
        }
        return true;
    }

    public void ActualizarLinks()
    {
        try
        {
            List<Usuario_Link> pLista = new List<Usuario_Link>();
            Usuario pUsu = (Usuario)Session["usuario"];

            pLista = LinkService.ListarUsuario_Link(Convert.ToInt64(pUsu.codusuario), (Usuario)Session["usuario"]);
            if (pLista.Count > 0)
            {
                phLinks.Controls.Clear();
                int cont = 0;
                string pInfoNombre = string.Empty;
                string[] pArrayNombre;
                foreach (Usuario_Link rFila in pLista)
                {
                    cont++;
                    LinkButton lb = new LinkButton();
                    lb.ID = rFila.idlink.ToString();
                    lb.CssClass = "btnLinkEstado";
                    lb.Height = 20;
                    lb.Width = 160;
                    pArrayNombre = rFila.nombre.Contains('|') ? rFila.nombre.Split('|') : new string[] { rFila.nombre };
                    pInfoNombre = pArrayNombre[0];
                    lb.PostBackUrl = "~/Page" + rFila.ruta;
                    if (pArrayNombre.Count() > 1)
                    {
                        lb.PostBackUrl += pArrayNombre[1];
                    }
                    lb.Text = pInfoNombre;
                    if (cont == 1)
                        phLinks.Controls.Add(new LiteralControl("<div class='row' style='width: 100%'>"));
                    phLinks.Controls.Add(lb);
                    if (cont == 4)
                    {
                        phLinks.Controls.Add(new LiteralControl("</div>"));
                        cont = 0;
                    }
                    else
                    {
                        phLinks.Controls.Add(new LiteralControl("&nbsp;"));
                    }
                }

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "ActualizarLinks", ex);
        }
    }


    protected void gvLinks_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLinks.PageIndex = e.NewPageIndex;
            ActualizarLinksGrilla();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "gvLinks_PageIndexChanging", ex);
        }
    }

    void ActualizarLinksGrilla()
    {
        try
        {
            List<Usuario_Link> lstConsulta = new List<Usuario_Link>();
            Usuario pUsu = (Usuario)Session["usuario"];

            lstConsulta = LinkService.ListarUsuario_Link(Convert.ToInt64(pUsu.codusuario), (Usuario)Session["usuario"]);

            gvLinks.PageSize = 15;
            String emptyQuery = "Fila de datos vacia";
            gvLinks.EmptyDataText = emptyQuery;

            if (lstConsulta.Count > 0)
            {
                gvLinks.DataSource = lstConsulta;
                gvLinks.DataBind();
                gvLinks.Visible = true;
                lblTotalLinks.Visible = true;
                lblTotalLinks.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                lblInfoLinks.Visible = false;
            }
            else
            {
                gvLinks.DataSource = null;
                gvLinks.Visible = false;
                lblTotalLinks.Visible = false;
                lblInfoLinks.Visible = true;
            }

            Session.Add(AporteServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AporteServicio.CodigoPrograma, "ActualizarLinks", ex);
        }
    }


    protected void gvLinks_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Int64 id = Convert.ToInt64(gvLinks.DataKeys[e.RowIndex].Values[0].ToString());
        if (id != null && id != 0)
            LinkService.EliminarLink(id, (Usuario)Session["usuario"]);

        mpeEliminar.Hide();
        ActualizarLinks();
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
    }

    protected void btnEliminarLink_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        mpeEliminar.Show();
        ActualizarLinksGrilla();
    }


    protected void btnCerrarLinks_Click(object sender, EventArgs e)
    {
        mpeEliminar.Hide();
    }

    #endregion

    #region Tab de Ahorro Programado
    //TAB AHORRO PROGRAMADO
    protected void gvAhoProgra_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAhoProgra.PageIndex = e.NewPageIndex;
            ActualizarTablaAhoProgra();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.CodigoPrograma, "gvLista_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Muestra información del Ahorro Programado
    /// </summary>
    private void ActualizarTablaAhoProgra()
    {
        try
        {
            Xpinn.Programado.Services.MovimientoCuentasServices MovimientoCtaService = new Xpinn.Programado.Services.MovimientoCuentasServices();
            Int64 cliente = Convert.ToInt64(txtCodigo.Text);
            List<Xpinn.Programado.Entities.CuentasProgramado> lstConsulta = new List<Xpinn.Programado.Entities.CuentasProgramado>();
            String FiltroEstado = "";
            //  this.chkAhoProgra.Visible = false;

            if (chkAhoProgra.Checked)
            {
                FiltroEstado = "";

            }
            else
            {
                FiltroEstado = " A.ESTADO NOT IN (2) AND"; // CODIGO 2 => ESTADO TERMINADOS //estados ubicados PROGRAMADO/MOVIMIENTOCUENTAS               
            }

            String filtro = " WHERE " + FiltroEstado + " A.COD_PERSONA = " + cliente;

            lstConsulta = MovimientoCtaService.ListarAhorrosProgramado(filtro, DateTime.MinValue, (Usuario)Session["usuario"]);

            gvAhoProgra.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvAhoProgra.EmptyDataText = emptyQuery;

            if (lstConsulta.Count > 0)
            {
                gvAhoProgra.DataSource = lstConsulta;
                gvAhoProgra.DataBind();
                lblTotalAhoProgra.Visible = true;
                lblInfoAhoProgra.Visible = false;
                lblTotalAhoProgra.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                Session["AHOPRO"] = lstConsulta;
            }
            else
            {
                gvAhoProgra.DataSource = null;
                lblTotalAhoProgra.Visible = false;
                lblInfoAhoProgra.Visible = true;
                Session["AHOPRO"] = null;

                // totales 
                lblTotalCuotasAhoProgra.Visible = false;
                lbltotalAhoProgramado.Visible = false;
                txtProgramadoCuotas.Visible = false;
                txtAhoProgramadoTotal.Visible = false;
                txtTotalAhoProgramado.Visible = false;
                txtTotalCuotasAhoProgra.Visible = false;
                lblTotalCausacionProgramado.Visible = false;
                txtTotalCausacionProgramado.Visible = false;
                txtProgramadoCausacion.Visible = false;
                lblTotalCaumassaldoProgramado.Visible = false;
                txtProgramadoCausacionmassaldo.Visible = false;
                txtTotalCausacionProgramado.Visible = false;
                txtTotalCausacionmasProgramado.Visible = false;
            }


            // totalizar 

            decimal programado = 0;
            decimal totalcausacion_programado = 0;
            decimal totalcuotasprogramado = 0;
            decimal totalcausacionmasprogramado = 0;
            foreach (GridViewRow rfila in gvAhoProgra.Rows)
            {
                if (rfila.Cells[9].Text != "" && rfila.Cells[9].Text != "&nbsp;")
                    programado += Convert.ToDecimal(rfila.Cells[9].Text.Replace("$", "").Replace(".", "").Replace(",", ""));
                if (rfila.Cells[14].Text != "" && rfila.Cells[14].Text != "&nbsp;")
                    totalcuotasprogramado += Convert.ToDecimal(rfila.Cells[14].Text.Replace("$", "").Replace(".", "").Replace(",", ""));

                if (rfila.Cells[18].Text != "" && rfila.Cells[18].Text != "&nbsp;")
                    totalcausacion_programado += Convert.ToDecimal(rfila.Cells[18].Text.Replace("$", "").Replace(".", "").Replace(",", ""));


                txtTotalAhoProgramado.Text = Convert.ToString(programado.ToString("###,###"));
                txtTotalCuotasAhoProgra.Text = Convert.ToString(totalcuotasprogramado.ToString("###,###"));
                if (totalcausacion_programado == 0)
                {
                    txtTotalCausacionProgramado.Text = "0";

                }
                else
                {
                    txtTotalCausacionProgramado.Text = Convert.ToString(totalcausacion_programado.ToString("###,###"));
                }


                totalcausacionmasprogramado = programado + totalcausacion_programado;
                txtTotalCausacionmasProgramado.Text = Convert.ToString(totalcausacionmasprogramado.ToString("###,###"));


            }

            Session.Add(MovimientoCtaService.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void gvAhoProgra_SelectedIndexChanged(object sender, EventArgs e)
    {
        Xpinn.Programado.Services.MovimientoCuentasServices MovimientoCtaService = new Xpinn.Programado.Services.MovimientoCuentasServices();
        String id = gvAhoProgra.DataKeys[gvAhoProgra.SelectedRow.RowIndex].Value.ToString();
        Session[MovimientoCtaService.CodigoPrograma + ".id"] = id;
        Response.Redirect("~/Page/Programado/MovimientoCuentas/Detalle.aspx");
    }

    protected void chkAhoProgra_CheckedChanged(object sender, EventArgs e)
    {
        ActualizarTablaAhoProgra();
    }

    protected void chkTabCredito_CheckedChanged(object sender, EventArgs e)
    {
        ActualizarTablaDevolucion();
    }

    protected void chkExtend_CheckedChanged(object sender, EventArgs e)
    {
        txtExtendQuery.Visible = chkExtend.Checked;
    }

    #endregion

    #region Tab de CDATS
    //TAB CDATS
    protected void gvCDATS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCDATS.PageIndex = e.NewPageIndex;
            ActualizarTablaCDATS();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.CodigoPrograma, "gvCDATS_PageIndexChanging", ex);
        }
    }

    /// <summary>
    /// Muestra información del CDATS
    /// </summary>
    private void ActualizarTablaCDATS()
    {
        try
        {
            Xpinn.CDATS.Services.AperturaCDATService MovimientoCDATService = new Xpinn.CDATS.Services.AperturaCDATService();
            Int64 cliente = Convert.ToInt64(txtCodigo.Text);
            List<Xpinn.CDATS.Entities.Cdat> lstConsulta = new List<Xpinn.CDATS.Entities.Cdat>();
            String FiltroEstado = "";
            if (chkCDATS.Checked)
            {
                FiltroEstado = " C.ESTADO IN (3,4,5) AND";
            }
            else
            {
                FiltroEstado = " C.ESTADO IN (1,2) AND";
            }
            String filtro = " AND " + FiltroEstado + " T.COD_PERSONA = " + cliente;
            lstConsulta = MovimientoCDATService.ListarCdats(filtro, DateTime.MinValue, (Usuario)Session["usuario"]);
            gvCDATS.PageSize = 20;
            String emptyQuery = "Fila de datos vacia";
            gvCDATS.EmptyDataText = emptyQuery;
            if (lstConsulta.Count > 0)
            {
                gvCDATS.DataSource = lstConsulta;
                gvCDATS.DataBind();
                lblTotalCDATS.Visible = true;
                lblInfoCDATS.Visible = false;
                lblTotalCDATS.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                ViewState["CDATS"] = lstConsulta;
            }
            else
            {
                gvCDATS.DataSource = null;
                lblTotalCDATS.Visible = false;
                lblInfoCDATS.Visible = true;
                ViewState["CDATS"] = null;



                // totales 
                lbltotalCdat.Visible = false;
                txtCdatTotal.Visible = false;
                txtTotalCdat.Visible = false;
                lblTotalCausacionCdat.Visible = false;
                txtCdatCausacion.Visible = false;
                txtTotalCausacionCdat.Visible = false;
                txtTotalCausacionmasCdat.Visible = false;
                lblTotalCaumassaldoCdat.Visible = false;
            }


            // totalizar 

            decimal cdat = 0;
            decimal totalcausacioncdat = 0;
            decimal totalcausacionmascdat = 0;

            foreach (GridViewRow rfila in gvCDATS.Rows)
            {
                if (rfila.Cells[8].Text != "" && rfila.Cells[8].Text != "&nbsp;")
                    cdat += Convert.ToDecimal(rfila.Cells[8].Text.Replace("$", "").Replace(".", "").Replace(",", ""));

                if (rfila.Cells[12].Text != "" && rfila.Cells[12].Text != "&nbsp;")
                    totalcausacioncdat += Convert.ToDecimal(rfila.Cells[12].Text.Replace("$", "").Replace(".", "").Replace(",", ""));


                txtTotalCdat.Text = Convert.ToString(cdat.ToString("###,###"));
                txtTotalCausacionCdat.Text = Convert.ToString(totalcausacioncdat.ToString("###,###"));

                totalcausacionmascdat = cdat + totalcausacioncdat;

                txtTotalCausacionmasCdat.Text = Convert.ToString(totalcausacionmascdat.ToString("###,###"));

            }


            //  Session.Add(serviceEstadoCuenta.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.CodigoPrograma, "Actualizar", ex);
        }
    }


    protected void gvCDATS_SelectedIndexChanged(object sender, EventArgs e)
    {

        Xpinn.CDATS.Services.ReporteMovimientoServices ReporteMovService = new Xpinn.CDATS.Services.ReporteMovimientoServices();
        String id = gvCDATS.DataKeys[gvCDATS.SelectedRow.RowIndex].Value.ToString();
        Session[ReporteMovService.CodigoPrograma + ".id"] = id;
        Response.Redirect("~/Page/CDATS/ReporteMovimiento/Detalle.aspx");
    }

    protected void chkCDATS_CheckedChanged(object sender, EventArgs e)
    {
        ActualizarTablaCDATS();
    }

    #endregion




    protected void gvListaAporte_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
        int columnas;
        columnas = serviceEstadoCuenta.ConsultarParametroColumnas((Usuario)Session["Usuario"]);
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (columnas > 0)
            {
                e.Row.Cells[13].Controls.Clear();
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Controls.Clear();
                e.Row.Cells[14].Visible = false;
                lblTotalCausacionAporte.Visible = false;
                txtTotalCausacionAportes.Visible = false;
                lblTotalCaumassaldoAportes.Visible = false;
                txtAportesCausacionmassaldo.Visible = false;
                txtTotalCausacionmasAportes.Visible = false;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (columnas > 0)
            {
                e.Row.Cells[13].Controls.Clear();
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Controls.Clear();
                e.Row.Cells[14].Visible = false;
                lblTotalCausacionAporte.Visible = false;
                txtTotalCausacionAportes.Visible = false;
                lblTotalCaumassaldoAportes.Visible = false;
                txtAportesCausacionmassaldo.Visible = false;
                txtTotalCausacionmasAportes.Visible = false;
            }



        }

    }


    #region Líneas Telefónicas
    /// <summary>
    /// Mostrar datos de las Lineas
    /// </summary>
    private void ActualizarTablaLineas()
    {
        Xpinn.Aportes.Services.PlanesTelefonicosService LineasServicio = new Xpinn.Aportes.Services.PlanesTelefonicosService();
        try
        {
            Int64 cliente = Convert.ToInt64(txtCodigo.Text);
            Xpinn.Aportes.Entities.PlanTelefonico lineas = new Xpinn.Aportes.Entities.PlanTelefonico();
            List<Xpinn.Aportes.Entities.PlanTelefonico> lstConsulta = new List<Xpinn.Aportes.Entities.PlanTelefonico>();
            bool existe = LineasServicio.ConsultarLineasExistentes((Usuario)Session["usuario"]);
            if (existe == true)
                tabLineas.Visible = true;

            if (CheckLinesInactivas.Checked)
                lstConsulta = LineasServicio.ListarLineas(lineas, DateTime.MinValue, (Usuario)Session["usuario"], " And P.IDENTIFICACION = '" + txtNumDoc.Text + "' AND LT.ESTADO <> 'A'");
            else
                lstConsulta = LineasServicio.ListarLineas(lineas, DateTime.MinValue, (Usuario)Session["usuario"], " And P.IDENTIFICACION = '" + txtNumDoc.Text + "' AND LT.ESTADO = 'A' ");

            gvLineas.PageSize = pageSize;
            gvLineas.EmptyDataText = emptyQuery;
            gvLineas.DataSource = lstConsulta;

            if (lstConsulta.Count > 0)
            {
                gvLineas.Visible = true;
                lblInfoDev.Visible = false;
                lblTotalRegsDev.Visible = true;
                lblTotalRegsDev.Text = "<br/> Registros encontrados " + lstConsulta.Count.ToString();
                gvLineas.DataBind();
                Session["LINEAS"] = lstConsulta;
                tabDevoluciones.Visible = true;
                lblDevoluciones.Visible = true;
                txtDevolucionestotal.Visible = true;
            }
            else
            {
                gvLineas.Visible = false;
                lblInfoDev.Visible = true;
                lblTotalRegsDev.Visible = false;
                Session["LINEAS"] = null;
                txtDevolucionestotal.Visible = false;
                lblDevoluciones.Visible = false;
                lblInfoDev.Visible = false;
                lblTotalRegsDev.Visible = false;
                tabLineas.Visible = false;
            }
            decimal devoluciones = 0;
            foreach (GridViewRow ifila in gvDevoluciones.Rows)
            {

                devoluciones += Convert.ToDecimal(gvDevoluciones.DataKeys[ifila.RowIndex].Values[1].ToString());
                txtDevolucionestotal.Text = devoluciones.ToString("##,##");

            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(LineasServicio.CodigoPrograma, "ActualizarTablaLineas", ex);
        }

    }

    protected void chktabLineas_CheckedChanged(object sender, EventArgs e)
    {
        ActualizarTablaLineas();
    }

    protected void gvLineas_SelectedIndexChanged(object sender, EventArgs e)
    {
        // String id = gvDevoluciones.DataKeys[gvDevoluciones.SelectedRow.RowIndex].Values[0].ToString();               
    }

    protected void gvLineas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvLineas.PageIndex = e.NewPageIndex;
            ActualizarTablaLineas();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(serviceEstadoCuenta.CodigoPrograma, "gvLineas_PageIndexChanging", ex);
        }
    }


    #endregion



    protected void gvdetDevol_RowCommand(object sender, GridViewCommandEventArgs evt)
    {

        GVDetalleDevoluciones.DataSource = null;
        GVDetalleDevoluciones.DataBind();

        if (evt.CommandName == "DetalleDevol")
        {
            try
            {
                int index = Convert.ToInt32(evt.CommandArgument);
                Int32 cod_devol = Convert.ToInt32(gvDevoluciones.Rows[index].Cells[1].Text);
                List<Xpinn.Tesoreria.Entities.Devolucion> lstDevol = new List<Xpinn.Tesoreria.Entities.Devolucion>();
                Xpinn.Tesoreria.Services.DevolucionServices DevolService = new Xpinn.Tesoreria.Services.DevolucionServices();
                lstDevol = DevolService.ConsultarDevolucionDetalle(cod_devol, (Usuario)Session["Usuario"]);
                if (lstDevol.Count > 0)
                {
                    GVDetalleDevoluciones.DataSource = lstDevol;
                    GVDetalleDevoluciones.DataBind();
                    MpeDetalleDevol.Show();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }

    protected void btnCloseAct_Click(object sender, EventArgs e)
    {
        MpeDetalleDevol.Hide();
    }

    protected void gvAhorros_RowCommand(object sender, GridViewCommandEventArgs evt)
    {
        if (evt.CommandName == "Firmas")
        {
            string numero_cuenta = evt.CommandArgument.ToString();
            imgTarjetaFirmas.ImageUrl = "HandlerFirma.ashx?id=" + numero_cuenta + "&Us=" + ((Usuario)Session["Usuario"]).identificacion + "&Pw=" + System.Web.HttpUtility.UrlEncode(((Usuario)Session["Usuario"]).clave_sinencriptar);
            mpeTarjetaFirmas.Show();
        }

    }

    protected void btnCloseFirmas_Click(object sender, EventArgs e)
    {
        mpeTarjetaFirmas.Hide();
    }

    private List<string> ComentariosCredito(GridViewRowEventArgs e)
    {
        HyperLink lin = (HyperLink)e.Row.FindControl("hplink");
        List<string> lstComentarios = new List<string>();

        //consulta Garantias
        List<Garantia> lstGarantia = garantiaService.ListarFullGarantias(" Where G.Numero_radicacion = " + lin.Text, (Usuario)Session["usuario"]);
        Int64 nGarantia = Convert.ToInt64(e.Row.Cells[14].Text.Replace(",", ""));
        if (lstGarantia.Count > 0)
        {
            lstComentarios.Add("Credito con garantia");
        }

        //Consulta Creditos con cuotas Extras.
        eCuoExt.numero_radicacion = Convert.ToInt64(lin.Text);
        lstConsultas = CuoExtServicio.ListarCuotasExtras(eCuoExt, (Usuario)Session["usuario"]);

        if (lstConsultas.Count > 0)
        {
            lstComentarios.Add("Credito con cuotas extras ");
        }

        var valida = 0;
        credito.Numero_radicacion = Int64.Parse(lin.Text);
        credito = creditoPlanServicio.ConsultarCredito(credito.Numero_radicacion, true, (Usuario)Session["usuario"]);
        clasificacionCartera = clasificacionCarteraService.ConsultarClasificacion(credito.cod_clasifica,
                (Usuario)Session["usuario"]);
        lsHistoricoTasas = historicoTasaService.listarhistorico(clasificacionCartera.tipo_historico, (Usuario)Session["usuario"]);
        foreach (HistoricoTasa historicoTasa in lsHistoricoTasas)
        {
            if (credito.TasaNom > (double)historicoTasa.VALOR)
            {
                if (credito.FechaAprobacion >= historicoTasa.FECHA_INICIAL && credito.FechaAprobacion <= historicoTasa.FECHA_FINAL)
                {
                    historico.VALOR = historicoTasa.VALOR;
                    valida = 1;
                    break;
                }
            }
        }

        if (valida == 1)
            if (credito.TasaInteres > Convert.ToInt64(historico.VALOR))
            {
                lstComentarios.Add("Credito creado con tasa de usura");
            }

        Credito creditoReliquidado = reliquidacionService.CreditoReliquidado(lin.Text, (Usuario)Session["usuario"]);

        if (creditoReliquidado.numero_radicacion > 0)
        {
            lstComentarios.Add("Credito reliquidado");
        }

        Credito creditoReestructurado =
            creditoService.ConsultarCreditoAsesor(Convert.ToInt64(lin.Text), (Usuario)Session["usuario"]);

        if (creditoReestructurado.Reestructurado > 0)
        {
            lstComentarios.Add("Credito Reestructurado");
        }

        return lstComentarios;
    }
}