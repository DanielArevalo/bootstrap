using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using Microsoft.Reporting.WebForms;
using AjaxControlToolkit;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Riesgo.Services;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Services;
using RestSharp;


public partial class Nuevo : GlobalWeb
{
    private Xpinn.FabricaCreditos.Services.Persona1Service Persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private Xpinn.FabricaCreditos.Entities.Factura eFactura = new Xpinn.FabricaCreditos.Entities.Factura();
    List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables
    String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    Xpinn.FabricaCreditos.Entities.InformacionNegocio vInformacionNegocio = new Xpinn.FabricaCreditos.Entities.InformacionNegocio();
    private Xpinn.FabricaCreditos.Services.InformacionNegocioService InformacionNegocioServicio = new Xpinn.FabricaCreditos.Services.InformacionNegocioService();
    private Xpinn.FabricaCreditos.Services.ProductoConsumoService productoService = new Xpinn.FabricaCreditos.Services.ProductoConsumoService();
    HistoricoSegmentacionService _historicoService = new HistoricoSegmentacionService();
    TradeUSServices _tradeService = new TradeUSServices();
    Xpinn.Aportes.Services.AfiliacionServices _afiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    ParametrizacionProcesoAfilicacionService _paramProceso = new ParametrizacionProcesoAfilicacionService();
    ImagenesService imagenService = new ImagenesService();


    private Xpinn.FabricaCreditos.Services.consultasdatacreditoService consultasdatacreditoServicio = new Xpinn.FabricaCreditos.Services.consultasdatacreditoService();

    //Servicio para consultar centrales de riesgo:
    Xpinn.FabricaCreditos.Services.PreAnalisisService preAnalisisServicio = new Xpinn.FabricaCreditos.Services.PreAnalisisService();
    Xpinn.FabricaCreditos.Services.FacturaService FaturaServicio = new Xpinn.FabricaCreditos.Services.FacturaService();

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[Persona1Servicio.CodigoProgramaDatacredito + ".id"] != null)
                VisualizarOpciones(Persona1Servicio.CodigoProgramaDatacredito, "E");
            else
                VisualizarOpciones(Persona1Servicio.CodigoProgramaDatacredito, "A");

            Site toolBar = (Site)this.Master;
            ctlCentrales.eventoClick += btnAceptarConfirmacionPago_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.GetType().Name + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {            
            if (!IsPostBack)
            {
                CargarListas();
                if (Session[Persona1Servicio.CodigoProgramaDatacredito + ".id"] != null)
                {
                    idObjeto = Session[Persona1Servicio.CodigoProgramaDatacredito + ".id"].ToString();
                    Session.Remove(Persona1Servicio.CodigoProgramaDatacredito + ".id");
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.GetType().Name + "A", "Page_Load", ex);
        }

        rvReciboPago.LocalReport.Refresh();
    }

    protected void btnCancelarConfirmacion_Click(object sender, ImageClickEventArgs e)
    {
        mvPreAnalisiCliente.ActiveViewIndex = 0;
    }

    protected void btnConsultar_Click(object sender, ImageClickEventArgs e)
    {
    }

    protected void btnAceptarConfirmacionPago_Click(object sender, EventArgs e)
    {
        AceptarConfirmacionPago();
    }

    private void AceptarConfirmacionPago()
    {

        //Consultar valor centrales riesgo
        List<Xpinn.FabricaCreditos.Entities.Parametrizar> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Parametrizar>();
        lstConsulta = preAnalisisServicio.ListarCentral(ObtenerValores(), (Usuario)Session["usuario"]);

        ViewState["valoriva"] = 0;
        ViewState["valor"] = 0;
        ViewState["valorCentral"] = 0;
        if (lstConsulta.Count > 0)
        {
            ViewState["valoriva"] = lstConsulta[0].valoriva;
            ViewState["valor"] = lstConsulta[0].valor;
            ViewState["valorCentral"] = lstConsulta[0].valor - lstConsulta[0].valoriva;
        }

        txtPrimer_nombre0.Text = ViewState["txtPrimer_nombre"].ToString();
        txtPrimer_apellido0.Text = ViewState["txtPrimer_apellido"].ToString();
        txtIdentificacion0.Text = ViewState["txtIdentificacion"].ToString();


        // Consulta consecutivo de factura
        try
        {
            eFactura = FaturaServicio.ObtenerNumeroFactura((Usuario)Session["usuario"]);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }

        ViewState["numerofactura"] = eFactura.numerofactura;
        ViewState["resolucion"] = eFactura.resolucion;
        ViewState["entidad"] = eFactura.entidad;

        //GUARDAR SOLICITUD AUTORIZACION
        Xpinn.FabricaCreditos.Entities.AutorizaConsulta autoriza = new Xpinn.FabricaCreditos.Entities.AutorizaConsulta();
        autoriza.cod_persona = Convert.ToInt32(Session["Cod_persona"]);
        autoriza.identificacion = ViewState["txtIdentificacion"].ToString();
        if (enviarMensaje(autoriza.cod_persona))
        {
            if (productoService.CrearAutorizacion(autoriza, (Usuario)Session["usuario"]))
                mvPreAnalisiCliente.ActiveViewIndex = 2;
        }        
    }


    private Xpinn.FabricaCreditos.Entities.Parametrizar ObtenerValores()
    {
        Xpinn.FabricaCreditos.Entities.Parametrizar parametrizar = new Xpinn.FabricaCreditos.Entities.Parametrizar();
        parametrizar.central = "Datacredito";
        return parametrizar;
    }

    protected void btnAceptarConfirmacionCentrales(object sender, EventArgs e)
    {
        VerError("");        
        //------------------------------------Consulta WS Datacredito------------------------------------

        //ConsultaDataCredito.ServicioHistoriaCredito ServicioHistoriaCredito = new ConsultaDataCredito.ServicioHistoriaCredito();

        //string Identificacion = ViewState["txtIdentificacion"].ToString();
        //string Apellido = ViewState["txtPrimer_apellido"].ToString();

        //string Parametros41 = String.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?> <Solicitudes> <Solicitud clave=\"47NFG\" identificacion=\"{0}\" primerApellido=\"{1}\" producto=\"64\" tipoIdentificacion=\"1\" usuario=\"80093280\"/> </Solicitudes>", Identificacion, Apellido);
        //string Parametros42 = String.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?> <Solicitud> <Solicitud clave=\"47NFG\" identificacion=\"{0}\" primerApellido=\"{1}\" producto=\"64\" tipoIdentificacion=\"1\" usuario=\"80093280\" > <Parametros> <Parametro tipo=\"T\" nombre=\"STRAID\" valor=\"1550\" /> <Parametro tipo=\"T\" nombre=\"STRNAM\" valor=\"Billing_Strategy\" /> <Parametro tipo=\"A\" nombre=\"facturarAciertaCod\" valor=\"true\" /> <Parametro tipo=\"B\" nombre=\"facturarQuantoMinimoCod\" valor=\"true\" /> <Parametro tipo=\"C\" nombre=\"facturarQuantoMaximoCod\" valor=\"true\" /> <Parametro tipo=\"R\" nombre=\"_edad\" valor=\"30\" /> </Parametros> </Solicitud> </Solicitud>", Identificacion, Apellido);

        //string formattedXml41 = XElement.Parse(Parametros41).ToString();
        //string formattedXml42 = XElement.Parse(Parametros42).ToString();

        //string rta41 = Convert.ToString(ServicioHistoriaCredito.consultarHC2(formattedXml41));
        //string rta42 = Convert.ToString(ServicioHistoriaCredito.consultarHC2(formattedXml42));

        //DataSet ds = new DataSet();
        //StringReader reader = new StringReader(rta41);
        //ds.ReadXml(reader);
        //gvInforme.DataSource = ds.Tables["Informe"];
        //gvInforme.DataBind();
        //gvNaturalNacional.DataSource = ds.Tables["NaturalNacional"];
        //gvNaturalNacional.DataBind();
        //gvIdentificacion.DataSource = ds.Tables["Identificacion"];
        //gvIdentificacion.DataBind();
        //gvEdad.DataSource = ds.Tables["Edad"];
        //gvEdad.DataBind();
        //gvScore.DataSource = ds.Tables["Score"];
        //gvScore.DataBind();
        //gvRazon.DataSource = ds.Tables["Razon"];
        //gvRazon.DataBind();

        //-----------------------------------------------------------------------------------------------


        Xpinn.FabricaCreditos.Entities.AutorizaConsulta autoriza = new Xpinn.FabricaCreditos.Entities.AutorizaConsulta();
        autoriza = productoService.verificarAutorizacion(Convert.ToInt32(Session["Cod_persona"]), (Usuario)Session["usuario"]);
        if (autoriza != null)
        {
            if(autoriza.autoriza == 1)
            {
                if (gvScore.Rows.Count > 0)
                    ViewState["Puntaje"] = gvScore.Rows[0].Cells[1].ToString();  //Guarda el puntaje para almacenarlo en tabla historial
                else
                    ViewState["Puntaje"] = "";


                mvPreAnalisiCliente.ActiveViewIndex = 3;
                Reporte();
            }
            else
            {
                VerError("La consulta aún no está autorizada");
            }            
        }        
    }

    private void Reporte()
    {
        //GENERACION DEL REGISTRO EN BD
        Xpinn.FabricaCreditos.Entities.consultasdatacredito vconsultasdatacredito = new Xpinn.FabricaCreditos.Entities.consultasdatacredito();

        vconsultasdatacredito.numerofactura = 0;
        vconsultasdatacredito.fechaconsulta = DateTime.Now;
        vconsultasdatacredito.cedulacliente = Convert.ToString(txtIdentificacion.Text.Trim());
        Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
        vconsultasdatacredito.usuario = lblUsuario.Text;

        //Obtiene IP
        string a = Request.UserHostAddress;
        string b = Request.ServerVariables["REMOTE_ADDR"];


        string ip = "";
        string strHostName = "";
        try
        {
            strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            ip = addr[2].ToString();
        }
        catch
        {
        }

        vconsultasdatacredito.ip = a;
        vconsultasdatacredito.oficina = Convert.ToString("Of01");
        vconsultasdatacredito.valorconsulta = Convert.ToInt64(ViewState["valorCentral"].ToString());
        vconsultasdatacredito.puntaje = ViewState["Puntaje"].ToString() != "" ? Convert.ToInt64(ViewState["Puntaje"].ToString()) : 0;

        vconsultasdatacredito = consultasdatacreditoServicio.Crearconsultasdatacredito(vconsultasdatacredito, (Usuario)Session["usuario"]);


        ReportParameter[] parametrosRecibo = new ReportParameter[17];

        parametrosRecibo[0] = new ReportParameter("pFecha", Convert.ToString(DateTime.Now));
        parametrosRecibo[1] = new ReportParameter("pRazonSocial", txtRazon_social.Text.ToUpper());
        parametrosRecibo[2] = new ReportParameter("pMotivoPago", "Consulta centrales de riesgo");
        parametrosRecibo[3] = new ReportParameter("pNombre", txtPrimer_nombre.Text.ToUpper() + " " + txtPrimer_apellido.Text.ToUpper());
        parametrosRecibo[4] = new ReportParameter("pValorPago", ViewState["valor"].ToString());
        parametrosRecibo[5] = new ReportParameter("pMedioPago", "Efectivo");
        parametrosRecibo[6] = new ReportParameter("pEstadoTransaccion", "Aprobada");
        parametrosRecibo[7] = new ReportParameter("pNumeroAprobacion", "1");
        parametrosRecibo[8] = new ReportParameter("pDireccionIp", "x.x.x.x");
        parametrosRecibo[9] = new ReportParameter("pCentralesRiesgo", ViewState["valorCentral"].ToString());
        parametrosRecibo[10] = new ReportParameter("pIva", ViewState["valoriva"].ToString());
        parametrosRecibo[11] = new ReportParameter("pNumeroFactura", ViewState["numerofactura"].ToString());
        parametrosRecibo[12] = new ReportParameter("pIdentificacion", txtIdentificacion.Text);
        parametrosRecibo[13] = new ReportParameter("pResolucion", ViewState["resolucion"].ToString().ToUpper());
        parametrosRecibo[14] = new ReportParameter("pEntidad", ViewState["entidad"].ToString().ToUpper());
        parametrosRecibo[15] = new ReportParameter("pUsuario", lblUsuario.Text);
        parametrosRecibo[16] = new ReportParameter("ImagenReport", ImagenReporte());

        rvReciboPago.LocalReport.EnableExternalImages = true;
        try
        {
            rvReciboPago.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            rvReciboPago.LocalReport.SetParameters(parametrosRecibo);
            rvReciboPago.LocalReport.Refresh();
        }
        catch
        {
        }
    }

    protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnConsultarResultado_Click(object sender, ImageClickEventArgs e)
    {
        mvPreAnalisiCliente.ActiveViewIndex = 4;

        lblResultadoCentrales.Text = "El cliente obtuvo un puntaje de 420. Es sujeto de credito. ESTE CLIENTE REQUIERE CODEUDOR. ¿Desea realizar la solicitud de credito?";

    }

    private void CargarListas()
    {
        try
        {
            ListaSolicitada = "TipoIdentificacion";
            TraerResultadosLista();
            ddlIdentificacion.DataSource = lstDatosSolicitud;
            ddlIdentificacion.DataTextField = "ListaDescripcion";
            ddlIdentificacion.DataValueField = "ListaId";
            ddlIdentificacion.DataBind();

            ddlTipoIdentificacion.DataSource = lstDatosSolicitud;
            ddlTipoIdentificacion.DataTextField = "ListaDescripcion";
            ddlTipoIdentificacion.DataValueField = "ListaId";
            ddlTipoIdentificacion.DataBind();

            ListaSolicitada = "Actividad";
            TraerResultadosLista();
            ddlActividad.DataSource = lstDatosSolicitud;
            ddlActividad.DataTextField = "ListaDescripcion";
            ddlActividad.DataValueField = "ListaIdStr";
            ddlActividad.DataBind();

            List<Xpinn.FabricaCreditos.Entities.CategoriaProducto> categorias = new List<Xpinn.FabricaCreditos.Entities.CategoriaProducto>();
            categorias = productoService.ListarCatProductoConsumo(" and estado = 1 ", (Usuario)Session["Usuario"]);
            ddlCategoria.DataSource = categorias;
            ddlCategoria.DataTextField = "descripcion";
            ddlCategoria.DataValueField = "id_cat_producto";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Add(new ListItem("Seleccione un item", "0"));
            ddlCategoria.SelectedValue = "0";

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(Persona1Servicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = Persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);

    }

    /// <summary>
    /// Método para generar el reporte de pago
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        Reporte();
    }

    protected void btnAceptarResultadoCentrales_Click(object sender, ImageClickEventArgs e)
    {
        Session[Persona1Servicio.CodigoPrograma + ".id"] = txtIdentificacion.Text;
        Session["Origen"] = "ORI";
        Response.Redirect("~/Page/FabricaCreditos/DatosCliente/DatosCliente.aspx?id=" + txtIdentificacion.Text);
    }

    protected void CancelarResultadoCentrales_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string script = @"<script type='text/javascript'>
                                iprint('#Iframe2');
                          </script>";

            // ScriptManager.RegisterStartupScript(this, typeof(Page), "impresionlistas", script, false);

            List<TradeUSSearchInd> lstOFAC = new List<TradeUSSearchInd>();
            List<Individual> lstONUInd = new List<Individual>();
            List<Entity> lstONUEnt = new List<Entity>();
            lstOFAC = ObtenerListaOFAC();
            lstONUInd = ObtenerListaONUInd();
            lstONUEnt = ObtenerListaONUEnt();
            if(ValidarListas(lstOFAC, lstONUInd))
            {
                ctlCentrales.MostrarMensaje("¿Desea consultar las centrales de riesgo del cliente?");
            }            
        }
        catch (ExceptionBusiness ex)
        {
            // Si el cliente ya existe se NO actualizan los datos:

            lblMensajeError.Text = ex.Message.ToString();
            AceptarConfirmacionPago();
            mvPreAnalisiCliente.ActiveViewIndex = 2;

        }
        catch (Exception ex)
        {
            string A = ex.ToString();
            //BOexcepcion.Throw(Persona1Servicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected bool ValidarListas(List<TradeUSSearchInd> lstOFAC, List<Individual> lstONUInd)
    {
        if (!chkR.Checked)
        {   VerError("Verifique el registro en Registraduría");   return false; }

        if (!chkP.Checked)
        { VerError("Verifique el registro en Procuraduría"); return false; }

        if (!chkC.Checked)
        { VerError("Verifique el registro en Contraloría"); return false; }

        if (!chkU.Checked)
        { VerError("Verifique el registro Rues"); return false; }

        if (!chkL.Checked)
        { VerError("Verifique el registro en Policía"); return false; }

        return true;
    }

    protected void btnListasRestrictivas_Click(object sender, ImageClickEventArgs e)
    {
        Session["TipoCredito"] = rblTipoCredito.SelectedIndex == 0 ? ClasificacionCredito.Consumo : ClasificacionCredito.MicroCredito;

        ViewState["ddlIdentificacion"] = ddlIdentificacion.SelectedValue;
        ViewState["txtPrimer_nombre"] = txtPrimer_nombre.Text.ToUpper();
        ViewState["txtPrimer_apellido"] = txtPrimer_apellido.Text.ToUpper();
        ViewState["txtIdentificacion"] = txtIdentificacion.Text.ToUpper();
        ViewState["ddlproducto"] = ddlProducto.SelectedValue;
        Session["TipoCredito"] = rblTipoCredito.SelectedIndex == 0 ? ClasificacionCredito.Consumo : ClasificacionCredito.MicroCredito;
        Session["telefono"] = txtTelefono.Text.ToString().Trim();

        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        try
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)Session["usuario"];
            if (idObjeto != "")
                vPersona1 = Persona1Servicio.ConsultarPersona1(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
            vPersona1.identificacion = (txtIdentificacion.Text != "") ? Convert.ToString(txtIdentificacion.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.tipo_identificacion = Convert.ToInt64(ddlIdentificacion.SelectedValue);
            vPersona1.tipo_persona = "N";
            vPersona1.digito_verificacion = 0;
            vPersona1.sexo = " ";
            vPersona1.fechaexpedicion = null;
            vPersona1.primer_nombre = (txtPrimer_nombre.Text != "") ? Convert.ToString(txtPrimer_nombre.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.segundo_nombre = (txtSegundo_nombre.Text != "") ? Convert.ToString(txtSegundo_nombre.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.primer_apellido = (txtPrimer_apellido.Text != "") ? Convert.ToString(txtPrimer_apellido.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.segundo_apellido = (txtSegundo_apellido.Text != "") ? Convert.ToString(txtSegundo_apellido.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.razon_social = (txtRazon_social.Text != "") ? Convert.ToString(txtRazon_social.Text.Trim().ToUpper()) : String.Empty;
            if (vPersona1.razon_social.Trim() != "")
                Session["Negocio"] = vPersona1.razon_social;
            vPersona1.codactividadStr = ddlActividad.SelectedValue;
            vPersona1.direccion = "";
            vPersona1.telefono = "";
            vPersona1.ValorArriendo = 0;
            vPersona1.fechanacimiento = null;
            vPersona1.codciudadnacimiento = null;
            vPersona1.codciudadresidencia = null;
            vPersona1.codciudadexpedicion = null;
            vPersona1.codestadocivil = 0;
            vPersona1.codescolaridad = 0;
            vPersona1.antiguedadlugar = 0;
            vPersona1.tipovivienda = "";
            vPersona1.arrendador = "";
            vPersona1.telefonoarrendador = "";
            vPersona1.celular = "";
            vPersona1.empresa = txtRazon_social.Text != "" ? txtRazon_social.Text.Trim() : "";
            vPersona1.telefonoempresa = (txtTelefono.Text != "") ? Convert.ToString(txtTelefono.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.email = (txtEmail.Text != "") ? Convert.ToString(txtEmail.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.direccionempresa = (txtDireccion.Text != "") ? Convert.ToString(txtDireccion.Text.Trim()) : String.Empty;
            vPersona1.antiguedadlugarempresa = 0;
            vPersona1.codcargo = 0;
            vPersona1.codtipocontrato = 0;
            vPersona1.cod_asesor = usuario.codusuario;
            vPersona1.residente = "";
            vPersona1.fecha_residencia = System.DateTime.Now;
            vPersona1.cod_oficina = usuario.cod_oficina;
            vPersona1.tratamiento = "";
            vPersona1.estado = "A";
            vPersona1.fechacreacion = System.DateTime.Now;
            vPersona1.usuariocreacion = usuario.identificacion;
            vPersona1.fecultmod = System.DateTime.Now;
            vPersona1.usuultmod = usuario.identificacion;
            vPersona1.barrioResidencia = 0;
            vPersona1.PersonasAcargo = null;
            vPersona1.Estrato = null;
            if (idObjeto != "")
            {
                vPersona1.cod_persona = Convert.ToInt64(idObjeto);
                Persona1Servicio.ModificarPersona1(vPersona1, (Usuario)Session["usuario"]);
            }
            else
            {
                vPersona1.cod_persona = 0;
                vPersona1 = Persona1Servicio.CrearPersona1(vPersona1, (Usuario)Session["usuario"]);
                idObjeto = vPersona1.cod_persona.ToString();
            }
            Session["Cod_persona"] = vPersona1.cod_persona.ToString();
            GuardarDireccionNegocio();
            Session[Persona1Servicio.CodigoPrograma + ".id"] = idObjeto;
            ddlTipoIdentificacion.SelectedValue = ddlIdentificacion.SelectedValue;
            txtIdentificacion2.Text = txtIdentificacion.Text;
            txtCodigo.Text = vPersona1.cod_persona.ToString();
            ObtenerDatos(vPersona1.cod_persona.ToString());
            mvPreAnalisiCliente.ActiveViewIndex = 6;
        }
        catch (ExceptionBusiness ex)
        {
            // Si el cliente ya existe se NO actualizan los datos:
            lblMensajeError.Text = ex.Message.ToString();
            mvPreAnalisiCliente.ActiveViewIndex = 2;
        }
    }

        private void GuardarDireccionNegocio()
    {
        try
        {            
            vInformacionNegocio.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
            vInformacionNegocio.direccion = (txtDireccion.Text != "") ? Convert.ToString(txtDireccion.Text.Trim()) : String.Empty;
            vInformacionNegocio.telefono = (txtTelefono.Text != "") ? Convert.ToString(txtTelefono.Text.Trim().ToUpper()) : String.Empty;
            vInformacionNegocio.localidad = "";
            vInformacionNegocio.nombrenegocio = txtRazon_social.Text != "" ? txtRazon_social.Text.Trim() : "";
            vInformacionNegocio.descripcion = "";
            vInformacionNegocio.arrendador = "";
            vInformacionNegocio.telefonoarrendador = "";
            vInformacionNegocio.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
            vInformacionNegocio.usuariocreacion = "";
            vInformacionNegocio.usuultmod = "";
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(InformacionNegocioServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }


    protected void btnAceptarMensaje_Click(object sender, ImageClickEventArgs e)
    {
        mvPreAnalisiCliente.ActiveViewIndex = 0;
    }


    protected void btnAdelante_Click(object sender, ImageClickEventArgs e)
    {
        Session["Origen"] = "ORI";
        Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx");
    }


    protected void txtPrimer_apellido_TextChanged(object sender, EventArgs e)
    {

    }

    protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(ddlCategoria.SelectedValue != "0")
        {
            string filtro = " and id_cat_producto = " + ddlCategoria.SelectedValue.ToString() +" ";
            ddlProducto.Items.Clear();
            ddlProducto.DataBind();
            ddlProducto.DataSource = productoService.ListarProductoConsumo(filtro, (Usuario)Session["usuario"]);
            ddlProducto.DataTextField = "descripcion";
            ddlProducto.DataValueField = "id_producto";
            ddlProducto.DataBind();
            ddlProducto.Items.Add(new ListItem("Seleccione un item", "0"));
            ddlProducto.SelectedValue = "0";
        }
    }



    private void ObtenerDatos(string idObjeto)
    {
        //Cargar datos de la persona
        Xpinn.FabricaCreditos.Entities.Persona1 pDatos = new Xpinn.FabricaCreditos.Entities.Persona1();
        Xpinn.FabricaCreditos.Services.Persona1Service personaServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

        pDatos = personaServicio.ConsultaDatosPersona(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);

        if (pDatos.identificacion != null && pDatos.identificacion != "")
        {
            txtCodigo.Text = idObjeto;
            txtIdentificacion.Text = pDatos.identificacion.ToString();
            ddlTipoIdentificacion.SelectedValue = pDatos.tipo_identificacion.ToString();
            txtNombres.Text = pDatos.nombres + " " + pDatos.apellidos;
            txtTipoPersona.Text = pDatos.tipo_persona == "N" ? "NATURAL" : pDatos.tipo_persona == "J" ? "JURIDICA" : "";

            ConsultaONU();
            ConsultaOFAC();
            tabOFAQ.Visible = true;
            tabPolicia.Visible = true;
            TabProcuraduria.Visible = true;
            tabRegistraduria.Visible = true;
            TabContraloria.Visible = true;
            tabRues.Visible = true;
        }
        else
        {
            VerError("No se puede generar la consulta, error al consultar la persona");
        }
    }

    private void ConsultaOFAC()
    {
        try
        {
            InterfazRIESGO intRiesgo = new InterfazRIESGO();
            TradeUSSearchResults tradeResult;
            List<TradeUSSearchInd> lstOFAC = new List<TradeUSSearchInd>();
            TradeUSSearchInd pObjeto;

            tradeResult = intRiesgo.ConsultarPersonaRiesgo(txtIdentificacion.Text, txtNombres.Text);
            if (tradeResult.results.Count > 0)
            {
                foreach (TradeUSSearchEntity entidad in tradeResult.results)
                {
                    pObjeto = new TradeUSSearchInd();
                    pObjeto.id = string.Join(",", (from i in entidad.ids where i.number != null select i.number).ToList());
                    pObjeto.source = entidad.source;
                    pObjeto.name = entidad.name;
                    pObjeto.alt_name = string.Join(",", entidad.alt_names);
                    pObjeto.date_of_birth = string.Join(",", entidad.dates_of_birth);
                    pObjeto.program = string.Join(",", entidad.programs);
                    pObjeto.nationality = string.Join(",", entidad.nationalities);
                    pObjeto.address = string.Join("-", (from i in entidad.addresses where i.address != null select i.address).ToList());
                    pObjeto.citizenship = string.Join(",", entidad.citizenships);
                    pObjeto.place_of_birth = string.Join(",", entidad.places_of_birth);
                    pObjeto.coincidencia = true;
                    lstOFAC.Add(pObjeto);
                }
            }
            else
            {
                pObjeto = new TradeUSSearchInd();
                pObjeto.id = txtIdentificacion.Text;
                pObjeto.source = "No se encuentra registro en las listas";
                pObjeto.name = txtNombres.Text;
                pObjeto.alt_name = "";
                pObjeto.date_of_birth = "";
                pObjeto.program = "";
                pObjeto.nationality = "";
                pObjeto.address = "";
                pObjeto.citizenship = "";
                pObjeto.place_of_birth = "";
                pObjeto.coincidencia = false;
                lstOFAC.Add(pObjeto);
            }

            if (lstOFAC.Count > 0)
            {
                ViewState["lstOFAC"] = lstOFAC;
                gvListaOFAC.DataSource = lstOFAC;
                gvListaOFAC.DataBind();
                lblOFAC.Visible = true;
                lblResultadosOFAC.Text = "Registros encontrados: " + lstOFAC.Count();
            }
            else
                lblResultadosOFAC.Text = "La persona no tiene registro en listas OFAC";
        }
        catch (Exception ex)
        {
            VerError("Error al generar la consulta de lista OFAC " + ex.Message);
        }
    }


    private void ConsultaONU()
    {
        try
        {
            InterfazRIESGO intRiesgo = new InterfazRIESGO();
            Individual PersonaIndividual = new Individual();
            List<Individual> lstIndividual = new List<Individual>();
            Entity PersonaEntidad = new Entity();
            List<Entity> lstEntidad = new List<Entity>();
            string URL = Request.Url.ToString();
            string tipo_persona = txtTipoPersona.Text == "NATURAL" ? "N" : txtTipoPersona.Text == "JURIDICA" ? "J" : "";
            string nombre = "";

            intRiesgo.ConsultarPersonaCSONU(txtIdentificacion.Text, txtNombres.Text, tipo_persona, ref PersonaIndividual, ref PersonaEntidad);

            if (tipo_persona == "N" && (PersonaIndividual.first_name != null || PersonaIndividual.second_name != null || PersonaIndividual.third_name != null))
            {
                if (PersonaIndividual.first_name != null)
                    nombre += PersonaIndividual.first_name.Trim();
                if (PersonaIndividual.second_name != null)
                    nombre += PersonaIndividual.second_name.Trim();
                if (PersonaIndividual.third_name != null)
                    nombre += PersonaIndividual.third_name.Trim();

                PersonaIndividual.first_name = nombre;
                PersonaIndividual.designation = PersonaIndividual.lstDesignation != null ? string.Join(",", PersonaIndividual.lstDesignation.Select(x => x.value)) : "";
                PersonaIndividual.nationality = PersonaIndividual.lstnationality != null ? string.Join(",", PersonaIndividual.lstnationality.Select(x => x.value)) : "";
                PersonaIndividual.list_type = PersonaIndividual.lstType != null ? string.Join(",", PersonaIndividual.lstType.Select(x => x.value)) : "";
                PersonaIndividual.coincidencia = true;

                lstIndividual.Add(PersonaIndividual);
                gvONUIndividual.DataSource = lstIndividual;
                gvONUIndividual.DataBind();
                gvONUIndividual.Visible = true;
                lblOnu.Text = "Registros encontrados: " + lstIndividual.Count();
            }
            else if (tipo_persona == "N" && PersonaIndividual.first_name == null || PersonaIndividual.second_name == null || PersonaIndividual.third_name == null)
            {
                PersonaIndividual.dataid = txtIdentificacion.Text;
                PersonaIndividual.first_name = txtNombres.Text;
                PersonaIndividual.comments1 = "No se encuentra registro en las listas";
                PersonaIndividual.coincidencia = false;

                lstIndividual.Add(PersonaIndividual);
                gvONUIndividual.DataSource = lstIndividual;
                gvONUIndividual.DataBind();
                gvONUIndividual.Visible = true;
                lblOnu.Text = "La persona no tiene registro en listas CS/NU";
            }
            else if (tipo_persona == "J" && PersonaEntidad != null)
            {
                PersonaEntidad.coincidencia = true;
                lstEntidad.Add(PersonaEntidad);
                gvONUEntidad.DataSource = lstEntidad;
                gvONUEntidad.DataBind();
                gvONUEntidad.Visible = true;
                lblOnu.Text = "Registros encontrados: " + lstEntidad.Count();
            }
            else if (tipo_persona == "J" && PersonaEntidad.dataid == null)
            {
                PersonaEntidad.first_name = txtNombres.Text;
                PersonaEntidad.comments1 = "No se encuentra registro en las listas";
                PersonaEntidad.un_list_type = DateTime.Now.ToShortDateString();
                PersonaEntidad.coincidencia = false;

                lstEntidad.Add(PersonaEntidad);
                gvONUEntidad.DataSource = lstEntidad;
                gvONUEntidad.DataBind();
                gvONUEntidad.Visible = true;
                lblOnu.Text = "La persona no tiene registro en listas CS/NU";
            }
            else
            {
                lblOnu.Text = "La persona no tiene registro en listas CS/NU";
            }

        }
        catch (Exception ex)
        {
            VerError("Error al generar la consulta de lista CS/NU " + ex.Message);
        }
    }

    protected void gvListaOFAC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvListaOFAC.PageIndex = e.NewPageIndex;
        if (ViewState["lstOFAC"] != null)
        {
            List<TradeUSSearchInd> lstOFAC = new List<TradeUSSearchInd>();
            lstOFAC = (List<TradeUSSearchInd>)ViewState["lstOFAC"];
            if (lstOFAC.Count > 0)
            {
                gvListaOFAC.DataSource = lstOFAC;
                gvListaOFAC.DataBind();
            }
        }
    }

    protected void gvLista_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gvLista = e.Row.NamingContainer as GridView;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gvLista.ID == "gvListaOFAC")
                {
                    TradeUSSearchInd dataItem = e.Row.DataItem as TradeUSSearchInd;

                    Image imageCheck = e.Row.FindControl("btnCheck") as Image;
                    Image imageEquis = e.Row.FindControl("btnEquis") as Image;

                    imageCheck.Visible = dataItem.coincidencia;
                    imageEquis.Visible = !dataItem.coincidencia;
                }

                if (gvLista.ID == "gvONUIndividual")
                {
                    Individual dataItem = e.Row.DataItem as Individual;

                    Image imageCheck = e.Row.FindControl("btnCheck") as Image;
                    Image imageEquis = e.Row.FindControl("btnEquis") as Image;

                    imageCheck.Visible = dataItem.coincidencia;
                    imageEquis.Visible = !dataItem.coincidencia;
                }

                if (gvLista.ID == "gvONUEntidad")
                {
                    Entity dataItem = e.Row.DataItem as Entity;

                    Image imageCheck = e.Row.FindControl("btnCheck") as Image;
                    Image imageEquis = e.Row.FindControl("btnEquis") as Image;

                    imageCheck.Visible = dataItem.coincidencia;
                    imageEquis.Visible = !dataItem.coincidencia;
                }
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(_historicoService.CodigoPrograma4, "gvLista_RowDataBound", ex);
        }
    }

    private List<TradeUSSearchInd> ObtenerListaOFAC()
    {
        List<TradeUSSearchInd> listaOFAC = new List<TradeUSSearchInd>();
        TradeUSSearchInd pRegistro;

        foreach (GridViewRow rFila in gvListaOFAC.Rows)
        {
            Image imageCheck = rFila.FindControl("btnCheck") as Image;
            Image imageEquis = rFila.FindControl("btnEquis") as Image;

            if (imageCheck.Visible)
            {
                pRegistro = new TradeUSSearchInd();
                pRegistro.id = txtIdentificacion.Text;
                pRegistro.source = rFila.Cells[1].Text;
                pRegistro.name = rFila.Cells[2].Text;
                pRegistro.alt_name = rFila.Cells[3].Text;
                pRegistro.address = rFila.Cells[4].Text;
                pRegistro.date_of_birth = rFila.Cells[5].Text;
                pRegistro.nationality = rFila.Cells[6].Text;
                pRegistro.place_of_birth = rFila.Cells[7].Text;
                pRegistro.citizenship = rFila.Cells[8].Text;
                pRegistro.program = rFila.Cells[9].Text;
                pRegistro.coincidencia = true;

                listaOFAC.Add(pRegistro);
            }
            else if (imageEquis.Visible)
            {
                pRegistro = new TradeUSSearchInd();
                pRegistro.id = txtIdentificacion.Text;
                pRegistro.source = "No se encuentra registro en las listas";
                pRegistro.name = txtNombres.Text;
                pRegistro.alt_name = "";
                pRegistro.date_of_birth = "";
                pRegistro.program = "";
                pRegistro.nationality = "";
                pRegistro.address = "";
                pRegistro.citizenship = "";
                pRegistro.place_of_birth = "";
                pRegistro.coincidencia = false;

                listaOFAC.Add(pRegistro);
            }
        }
        return listaOFAC;
    }

    private List<Individual> ObtenerListaONUInd()
    {
        List<Individual> listaONU = new List<Individual>();
        Individual pRegistro;

        foreach (GridViewRow rFila in gvONUIndividual.Rows)
        {
            Image imageCheck = rFila.FindControl("btnCheck") as Image;
            Image imageEquis = rFila.FindControl("btnEquis") as Image;

            if (imageCheck.Visible)
            {
                pRegistro = new Individual();
                pRegistro.dataid = rFila.Cells[0].Text;
                pRegistro.first_name = rFila.Cells[1].Text;
                pRegistro.un_list_type = rFila.Cells[2].Text;
                pRegistro.listed_on = rFila.Cells[3].Text;
                pRegistro.comments1 = rFila.Cells[4].Text;
                pRegistro.designation = rFila.Cells[5].Text;
                pRegistro.nationality = rFila.Cells[6].Text;
                pRegistro.list_type = rFila.Cells[7].Text;
                pRegistro.coincidencia = true;

                listaONU.Add(pRegistro);
            }
            else if (imageEquis.Visible)
            {
                pRegistro = new Individual();
                pRegistro.dataid = "";
                pRegistro.first_name = txtNombres.Text;
                pRegistro.un_list_type = "";
                pRegistro.listed_on = "";
                pRegistro.comments1 = rFila.Cells[4].Text;
                pRegistro.designation = "";
                pRegistro.nationality = "";
                pRegistro.list_type = "";
                pRegistro.coincidencia = false;

                listaONU.Add(pRegistro);
            }
        }
        return listaONU;
    }

    private List<Entity> ObtenerListaONUEnt()
    {
        List<Entity> listaONU = new List<Entity>();
        Entity pRegistro;

        foreach (GridViewRow rFila in gvONUEntidad.Rows)
        {
            Image imageCheck = rFila.FindControl("btnCheck") as Image;
            Image imageEquis = rFila.FindControl("btnEquis") as Image;

            if (imageCheck.Visible)
            {
                pRegistro = new Entity();
                pRegistro.dataid = rFila.Cells[0].Text;
                pRegistro.first_name = rFila.Cells[1].Text;
                pRegistro.un_list_type = rFila.Cells[2].Text;
                pRegistro.listed_on = rFila.Cells[3].Text;
                pRegistro.comments1 = rFila.Cells[4].Text;
                pRegistro.country = rFila.Cells[5].Text;
                pRegistro.city = rFila.Cells[6].Text;
                pRegistro.coincidencia = true;

                listaONU.Add(pRegistro);
            }
            else if (imageEquis.Visible)
            {
                pRegistro = new Entity();
                pRegistro.dataid = "";
                pRegistro.first_name = txtNombres.Text;
                pRegistro.un_list_type = "";
                pRegistro.listed_on = "";
                pRegistro.comments1 = rFila.Cells[4].Text;
                pRegistro.country = "";
                pRegistro.city = "";
                pRegistro.coincidencia = false;

                listaONU.Add(pRegistro);
            }
        }
        return listaONU;
    }

    public bool enviarMensaje(int cod_persona)
    {
        var client = new RestClient("https://www.onurix.com/api/v1/send-sms");
        var request = new RestRequest(Method.POST);
        request.AddHeader("content-type", "application/x-www-form-urlencoded");
        request.AddParameter("key", "7af4d5cf0bdbd2240cafaa991b71059a9c977f9b5cacfa52b7210");
        request.AddParameter("client", "248");
        string phone = Session["telefono"].ToString();
        request.AddParameter("phone", phone);
        string url = "200.119.47.27/AtencionAlClienteDemo/Autorizacion.aspx?rta=" + cod_persona;
        request.AddParameter("sms", url);
        request.AddParameter("country-code", "CO");
        IRestResponse response = client.Execute(request);

        return true;
    }
}