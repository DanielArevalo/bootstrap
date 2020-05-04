using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Microsoft.CSharp;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Auxilios.Entities;
using Xpinn.Aportes.Entities;


public partial class Nuevo : GlobalWeb
{
    Xpinn.FabricaCreditos.Services.Persona1Service objAhorraServi = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();

    Xpinn.FabricaCreditos.Services.codeudoresService CodeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
    Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
    Xpinn.Auxilios.Services.LineaAuxilioServices LineaAux = new Xpinn.Auxilios.Services.LineaAuxilioServices();

    String ListaSolicitada = null;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            VisualizarOpciones(objAhorraServi.CodigoProgramaCreditoE, "L");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoRegresar += btnRegresar_Click;
            ctlMensaje.eventoClick += btnContinuar_Click;
            ctlNumeroMatricul.eventoCambiar += txtNumer_textChange;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objAhorraServi.GetType().Name + "L", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Session["Beneficiario"] = null;
                ctlBusquedaProveedor.VisibleCtl = false;
                if (Session["TipoCredito"] == null)
                    Session["TipoCredito"] = "C";
                InicializarBeneficiarios();
                cargarddlLineaCredito();
                ddlLineaCredito.SelectedIndex = 1;
                cargarCamposInfor();
                mvReestructuras.ActiveViewIndex = 0;
                CargarListas();
                CargarDDL();
                InicialCodeudores();
                CargarDropDownEmpresa();
                ctlFecha.Text = DateTime.Now.ToShortDateString();
                string FormaPago = ConfigurationManager.AppSettings["ddlFormaPago"].ToString();
                if (FormaPago != null && FormaPago != "")
                {
                    ddlFormaPago.SelectedValue = FormaPago;
                }
                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);

                ddlLineaCredito_SelectedIndexChanged(ddlLineaCredito, null);
                //VALIDAR SI ESTA ACTIVO LA OPCION ORDEN SERVICIO PARA LA LINEA.
                if (ddlLineaCredito.SelectedItem != null)
                    ConsultarOrdenServicioCredito(ddlLineaCredito.SelectedValue);
                if (ddlLineaAuxilio.SelectedItem != null && ctlBusquedaProveedor.VisibleCtl == false)
                    ConsultarOrdenServicioAuxilio(ddlLineaAuxilio.SelectedValue);
                //DETERMINAR LA FECHA DE INICIO DEL CRÉDITO
                ObtenerFechaInicio();
                //CONSULTAR SI TIENE DATOS ENVIADOS DESDE PREANALISIS
                ObtenerDatosSession();
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objAhorraServi.GetType().Name + "A", "Page_Load", ex);
        }
    }

    private void ObtenerDatosSession()
    {
        if (Session[objAhorraServi.CodigoPrograma + ".linea"] != null)
        {
            ddlLineaCredito.SelectedValue = Session[objAhorraServi.CodigoPrograma + ".linea"].ToString();
            Session.Remove(objAhorraServi.CodigoPrograma + ".linea");
        }
        if (Session[objAhorraServi.CodigoPrograma + ".monto"] != null)
        {
            ctlNumeroMatricul.Text = Session[objAhorraServi.CodigoPrograma + ".monto"].ToString();
            Session.Remove(objAhorraServi.CodigoPrograma + ".monto");
        }
        if (Session[objAhorraServi.CodigoPrograma + ".plazo"] != null)
        {
            ddlPlazo.Text = Session[objAhorraServi.CodigoPrograma + ".plazo"].ToString();
            Session.Remove(objAhorraServi.CodigoPrograma + ".plazo");
        }
        if (Session[objAhorraServi.CodigoPrograma + ".periodicidad"] != null)
        {
            ddlperiodicidad.cod_periodicidad = Convert.ToInt32(Session[objAhorraServi.CodigoPrograma + ".periodicidad"].ToString());
            Session.Remove(objAhorraServi.CodigoPrograma + ".periodicidad");
        }
        try
        { 
            ddlLineaAuxilio.SelectedIndex = 1;
            ddlLineaAuxilio_SelectedIndexChanged(null, null);
            ddlLineaCredito_SelectedIndexChanged(null, null);
        }
        catch { }
        ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        //LA CUOTA NO SE RECUPERA
    }

    private void ObtenerFechaInicio()
    {
        try
        {
            CreditoService BOCreditoService = new CreditoService();
            DateTime? Fecha_Ini;
            string pError = "";
            Boolean pRpta = false;
            bool Ejecuta = false;
            if (ddlperiodicidad.cod_periodicidad != 0 && ddlLineaCredito.SelectedItem != null && ctlFecha.Text != "")
            {
                Int32? cod_empresa;
                if (ddlFormaPago.SelectedIndex == 0)
                {
                    cod_empresa = null;
                    Ejecuta = true;
                }
                else
                {
                    if (ddlEmpresa.SelectedIndex != 0)
                    {
                        cod_empresa = Convert.ToInt32(ddlEmpresa.SelectedValue);
                        Ejecuta = true;
                    }
                    else
                    {
                        cod_empresa = null;
                        Ejecuta = false;
                    }
                }
                if (Ejecuta == true)
                {
                    Fecha_Ini = BOCreditoService.FechaInicioCredito(Convert.ToDateTime(ctlFecha.Text), Convert.ToInt32(ddlperiodicidad.cod_periodicidad),
                        Convert.ToInt32(ddlFormaPago.SelectedValue), cod_empresa, ddlLineaCredito.SelectedValue, ref pError, ref pRpta, (Usuario)Session["usuario"]);
                    if (pRpta == false)
                    {
                        if (pError != "")
                        {
                            VerError(pError.ToString());
                        }
                    }
                    else
                    {
                        if (pError == "" && Fecha_Ini != null && Fecha_Ini != DateTime.MinValue)
                        {
                            ctlFechaPPago.Text = Convert.ToDateTime(Fecha_Ini).ToShortDateString();
                        }
                    }
                }
            }
        }
        catch
        { }
    }


    private void ConsultarOrdenServicioCredito(string pCod_Linea)
    {
        Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        vLineasCredito = LineasCreditoServicio.ConsultarLineasCredito(Convert.ToString(ddlLineaCredito.SelectedValue), (Usuario)Session["usuario"]);
        if (vLineasCredito.orden_servicio == 1)
        {
            ctlBusquedaProveedor.VisibleCtl = true;
            ctlBusquedaProveedor.CheckedOrd = true;
        }
    }

    private void ConsultarOrdenServicioAuxilio(string pCod_Linea)
    {
        LineaAuxilio vDatosLinea = new LineaAuxilio();
        vDatosLinea = LineaAux.ConsultarLineaAUXILIO(pCod_Linea, (Usuario)Session["usuario"]);
        if (vDatosLinea.orden_servicio == 1)
        {
            ctlBusquedaProveedor.VisibleCtl = true;
            ctlBusquedaProveedor.CheckedOrd = true;
        }
    }


    /// <summary>
    /// Método para cargar drop down list
    /// </summary>
    private void CargarDDL()
    {
        ddlperiodicidad.Inicializar();
        ddlperiodicidad.cod_periodicidad = 1;

        // LLena el DDL de las líneas de crédito        
        List<Xpinn.FabricaCreditos.Entities.LineasCredito> LstLineas = new List<Xpinn.FabricaCreditos.Entities.LineasCredito>();
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineasServicios = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        Xpinn.FabricaCreditos.Entities.LineasCredito vLineas = new Xpinn.FabricaCreditos.Entities.LineasCredito();

        // Traer las líneas de auxilio y cargar el porcentaje de la primera linea
        List<LineasCredito> lstAtributos = new List<LineasCredito>();
        Xpinn.FabricaCreditos.Entities.LineasCredito obLineas = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        lstAtributos = LineasServicios.ddlLineasCreditoServices((Usuario)Session["usuario"]);
        ddlLineaAuxilio.DataSource = lstAtributos;
        ddlLineaAuxilio.DataTextField = "nombre";
        ddlLineaAuxilio.DataValueField = "cod_atr";
        ddlLineaAuxilio.DataBind();
        var id = (from l in lstAtributos
                  select l.cod_atr).FirstOrDefault();
        var codi = LineasServicios.getPorcentajeMatriculaServices(id.ToString(), (Usuario)Session["usuario"]).porc_corto;
        Session["Porcentaje"] = codi;

        // Llena el DDL de los tipos de tasas
        List<TipoTasa> lstTipoTasa = new List<TipoTasa>();
        TipoTasaService TipoTasaServicios = new TipoTasaService();
        TipoTasa vTipoTasa = new TipoTasa();
        lstTipoTasa = TipoTasaServicios.ListarTipoTasa(vTipoTasa, (Usuario)Session["usuario"]);

        if (Session["usuario"] == null)
            return;
        Xpinn.Asesores.Services.UsuarioAseService serviceEjecutivo = new Xpinn.Asesores.Services.UsuarioAseService();
        Xpinn.Asesores.Entities.UsuarioAse ejec = new Xpinn.Asesores.Entities.UsuarioAse();
        ejec.estado = 1;
        ddlAsesorComercial.DataSource = serviceEjecutivo.ListarUsuario(ejec, (Usuario)Session["usuario"]);
        ddlAsesorComercial.DataTextField = "nombre";
        ddlAsesorComercial.DataValueField = "codusuario";
        ddlAsesorComercial.DataBind();
        ddlAsesorComercial.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
        if (Session["usuario"] != null)
        {
            Xpinn.Util.Usuario usu = (Usuario)Session["usuario"];
            if (usu.nombre != null && usu.nombre != "")
                ddlAsesorComercial.SelectedValue = usu.codusuario.ToString();
        }
    }

    private void CargarDropDownEmpresa()
    {
        List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
        Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
        Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();

        Int64 codigo = Convert.ToInt64(Session["Cod_Persona"].ToString());
        ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudoPersona(codigo, (Usuario)Session["usuario"]);

        ddlEmpresa.DataTextField = "nom_empresa";
        ddlEmpresa.DataValueField = "cod_empresa";
        ddlEmpresa.AppendDataBoundItems = true;
        ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlEmpresa.SelectedIndex = 0;
        ddlEmpresa.DataBind();
    }

    protected void InicialCodeudores()
    {
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        Xpinn.FabricaCreditos.Entities.Persona1 eCodeudor = new Xpinn.FabricaCreditos.Entities.Persona1();
        lstConsulta.Add(eCodeudor);
        Session["Codeudores"] = lstConsulta;
        gvCodeudores.DataSource = lstConsulta;
        gvCodeudores.DataBind();
        gvCodeudores.Visible = true;
        lblTotReg.Visible = false;
        lblTotalRegsCodeudores.Visible = true;
    }

    protected void ddlLineaCredito_SelectedIndexChanged(object sender, EventArgs e)
    {
        ctlBusquedaProveedor.VisibleCtl = false;
        ctlBusquedaProveedor.CheckedOrd = false;
        if (ddlLineaCredito.SelectedItem != null)
        {
            Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
            // Determinar si la línea es de Garantías Comunitarias
            LineaCreditoServicio.LineaEsFondoGarantiasComunitarias(ddlLineaCredito.SelectedValue.ToString(), (Usuario)Session["usuario"]);
            // Calcular el cupo del crédito
            Calcular_Cupo();
            if (ddlLineaAuxilio.SelectedItem != null)
                ConsultarOrdenServicioAuxilio(ddlLineaAuxilio.SelectedValue);

            if (ctlBusquedaProveedor.VisibleCtl == false)
                ConsultarOrdenServicioCredito(ddlLineaCredito.SelectedValue);

            panelAuxilio.Visible = false;
            LineasCredito vLineasCredito = new LineasCredito();
            vLineasCredito = LineasCreditoServicio.ConsultarLineasCredito(Convert.ToString(ddlLineaCredito.SelectedValue), (Usuario)Session["usuario"]);
            if (vLineasCredito.maneja_auxilio == 1)
            {
                panelAuxilio.Visible = true;                
            }
            if (ctlNumeroMatricul.Text.Trim() != "")
            {
                if (Convert.ToDecimal(ctlNumeroMatricul.Text) > 0)
                {
                    if (panelAuxilio.Visible == true)
                        calculaAuxilio();
                    CalcularValor();
                }
            }
        }
    }

    private void Calcular_Cupo()
    {
        Xpinn.FabricaCreditos.Entities.LineasCredito DatosLinea = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCredito = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        try
        {
            DatosLinea = LineaCredito.Calcular_Cupo(ddlLineaCredito.SelectedValue.ToString(), 0, DateTime.Today, (Usuario)Session["usuario"]);
            txtPlazoMaximo.Text = DatosLinea.Plazo_Maximo.ToString();
            txtMontoMaximo.Text = DatosLinea.Monto_Maximo.ToString();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private void cargarddlLineaCredito()
    {
        Persona1Service DatosClienteServicio = new Persona1Service();
        Persona1 ent = new Persona1();
        TraerResultadosLista();
        string pFiltro = " where l.educativo = 1";
        if (Session["ES_EMPLEADO"] != null)
        {
            pFiltro += " And l.aplica_empleado = 1 And Nvl(l.Credito_Gerencial, 0) != 1";
            Session.Remove("ES_EMPLEADO");
        }
        ddlLineaCredito.DataSource = DatosClienteServicio.listaddlServices(pFiltro,(Usuario)Session["usuario"]);
        ddlLineaCredito.DataTextField = "empresa";
        ddlLineaCredito.DataValueField = "cod_persona";
        ddlLineaCredito.SelectedIndex = 0;
        ddlLineaCredito.DataBind();        
    }

    private void TraerResultadosLista()
    {
        Persona1Service DatosClienteServicio = new Persona1Service();
        if (ListaSolicitada == null)
            return;
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["usuario"]);
    }

    private void CargarListas()
    {
        try
        {
            if (Session["TipoCredito"].ToString() == "M")
                ListaSolicitada = "STipoCreditoMicro";
            else
                if (Session["TipoCredito"].ToString() == "C")
                    ListaSolicitada = "STipoCreditoConsumo";
                else
                    ListaSolicitada = "STipoCreditoConsumo";

            ListaSolicitada = "TipoIdentificacion";
            TraerResultadosLista();
            txtTipIdenT.DataSource = lstDatosSolicitud;
            txtTipIdenT.DataTextField = "ListaDescripcion";
            txtTipIdenT.DataValueField = "ListaId";
            txtTipIdenT.DataBind();


            if (Session["TipoCredito"].ToString() == "M")
            {
                ListItem selectedListItem = ddlLineaCredito.Items.FindByValue("301"); // Selección microcrédito por defecto
                if (selectedListItem != null)
                    selectedListItem.Selected = true;
            }

            if (Session["TipoCredito"].ToString() == "C")
            {
                ListItem selectedListItem = ddlLineaCredito.Items.FindByValue("103"); // Selección microcrédito por defecto
                if (selectedListItem != null)
                    selectedListItem.Selected = true;
            }

            Calcular_Cupo();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(objAhorraServi.GetType().Name + "L", "CargarListas", ex);
        }
    }


    private bool validaDatos()
    {
        Int64 pCod_Persona = Convert.ToInt64(Session["Cod_Persona"]);
        if (!ValidarNumCreditosPorLinea(ddlLineaCredito.SelectedValue, null, pCod_Persona, (Usuario)Session["usuario"]))
            return false;

        if (ctlFecha.Text.Trim() == "")
        {
            VerError("Verifique La fecha Solicitud");
            return false;
        }
        if (txtUniversidad.Text.Trim() == "")
        {
            VerError("Complete el Campo Universidad");
            txtUniversidad.Focus();
            return false;
        }
        if (txtSemestre.Text.Trim() == "")
        {
            VerError("Complete el Semestre ");
            txtSemestre.Focus();
            return false;
        }
        if (panelAuxilio.Visible == true)
        {
            if (txtValorAux.Text.Trim() == "" || Convert.ToDecimal(txtValorAux.Text.Replace(".", "")) <= 0)
            {
                VerError("Complete El campo Valor Matricula");
                txtValorAux.Focus();
                return false;
            }
            if (Convert.ToDecimal(txtValorAux.Text.Replace(".", "")) <= 0 || Convert.ToDecimal(txtValorAux.Text.Replace(".", "")) > Convert.ToDecimal(txtValorAux.Text.Replace(".", "")))
            {
                VerError("El valor del auxilio debe ser mayor a cero y debe estar entre el rango del Monto");
                return false;
            }
        }        
        if (ddlLineaCredito.SelectedValue == "")
        {
            VerError("Seleccione Linea");
            ddlLineaCredito.Focus();
            return false;
        }
        if (ctlCredito.Text.Trim() == "" || Convert.ToDecimal(ctlCredito.Text.Replace(".","")) < 0)
        {
            VerError(" Verifique El Campo Credito");
            return false;
        }
        if (ddlAsesorComercial.SelectedIndex <= 0)
        {
            VerError("Complete el Campo asesor Comercial");
            ddlAsesorComercial.Focus();
            return false;
        }
        if (ctlFecha.Text.Trim() == "")
        {
            VerError("Verifique el campo de Fecha de Ingreso");
            return false;
        }
        if (ddlPlazo.Text.Length <= 0 || ddlPlazo.Text.Trim() == "")
        {
            VerError("Complete el Campo Plazo");
            return false;
        }
        if (ddlPlazo.Text.Trim() == "0")
        {
            VerError("Debe ingresar el plazo");
            return false;
        }
        if (ddlFormaPago.SelectedIndex < 0)
        {
            VerError("Complete la Forma de Pago");
            return false;
        }
        if (ddlEmpresa.Visible)
        {
            if (ddlEmpresa.SelectedIndex <= 0)
            {
                VerError("Debe seleccionar la pagaduria.");
                ddlEmpresa.Focus();
                return false;
            }
        }
        if (ddlLineaAuxilio.SelectedIndex < 0)
        {
            VerError("Completre La Linea De Auxilio");
            return false;
        }
        if (ctlNumeroMatricul.Text.Trim() == "" || Convert.ToDecimal(ctlNumeroMatricul.Text) <= 0)
        {
            VerError("Complete el valor de la matricula");
            return false;
        }
        if (txtMontoMaximo.Text.Trim() == "")
            txtMontoMaximo.Text = "0";
        if (Convert.ToDecimal(txtMontoMaximo.Text.Trim().Replace(".", "")) < Convert.ToDecimal(ctlCredito.Text.Replace(".", "")))
        {
            VerError("El valor del credito no debe exceder el monto maximo");
            return false;
        }        
        if (ddlperiodicidad.Text == "Seleccion un Item" || ddlperiodicidad.cod_periodicidad <= 0)
        {
            VerError("Seleccione La perioricidad");
            return false;
        }

        if (ctlBusquedaProveedor.VisibleCtl == true && ctlBusquedaProveedor.CheckedOrd == true)
        {
            if (ctlBusquedaProveedor.TextIdentif == "")
            {
                VerError("Ingrese una identificación valida del Proveedor.");
                return false;
            }
            else
            {
                Persona1 pData = new Persona1();
                Persona1Service PersonaService = new Persona1Service();
                pData.seleccionar = "Identificacion";
                pData.noTraerHuella = 1;
                pData.identificacion = ctlBusquedaProveedor.TextIdentif;
                pData = PersonaService.ConsultarPersona1Param(pData, (Usuario)Session["usuario"]);
                if (pData.nombres == null && pData.apellidos == null)
                {
                    VerError("Ingrese una identificación valida del Proveedor");
                    return false;
                }
            }
        }

        //VALIDAR EL ENDEUDAMIENTO ACTUAL DE LA PERSONA.
        CreditoService BOCredito = new CreditoService();
        Xpinn.Comun.Data.GeneralData DAGeneral = new Xpinn.Comun.Data.GeneralData();
        Xpinn.Comun.Entities.General pEntidad = new Xpinn.Comun.Entities.General();
        pEntidad = DAGeneral.ConsultarGeneral(90164, (Usuario)Session["usuario"]);
        try
        {
            if (pEntidad.valor != null)
            {
                if (Convert.ToDecimal(pEntidad.valor.Replace(".", "")) > 0)
                {
                    decimal paramCantidad = 0, pValor = 0;
                    paramCantidad = Convert.ToDecimal(pEntidad.valor.Replace(".", ""));
                    pValor = BOCredito.ObtenerSaldoTotalXpersona(Convert.ToInt64(Session["Cod_persona"].ToString()), (Usuario)Session["usuario"]);
                    if (pValor > 0)
                    {
                        if (pValor > paramCantidad)
                        {
                            VerError("No se puede registrar la solicitud debido a que el asociado excede el monto total de endeudamiento.");
                            return false;
                        }
                    }
                }
            }
        }
        catch { }

        return true;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        if (validaDatos())
        {
            ctlMensaje.MostrarMensaje("Se Realizara La solicitud");
        }
        //mpeNuevo.Show();
    }


    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Value == "2" || ddlFormaPago.SelectedItem.Text == "Nomina")
        {
            ddlEmpresa.Visible = true;
            lblPagaduri.Visible = true;
        }
        else
        {
            ddlEmpresa.Visible = false;
            lblPagaduri.Visible = false;
        }
        ObtenerFechaInicio();
    }

    /// <summary>
    /// Método para determinar la información del deudor al cual se le van a consultar los productos
    /// </summary>
    private void cargarCamposInfor()
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        Int64 cod = (Int64)Session["Cod_Persona"];
        vPersona1.seleccionar = "Cod_persona";
        vPersona1.cod_persona = cod;
        var vPersona = DatosClienteServicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);

        txtNombre.Text = vPersona1.nombres != "" ? vPersona.nombres : "";
        txtIdentificacion.Text = vPersona.identificacion != "" ? vPersona.identificacion : "";
        txtTipIdenT.SelectedValue = vPersona.tipo_identificacion.ToString() != "" ? vPersona.tipo_identificacion.ToString() : "";
        txtApellido.Text = vPersona.apellidos != "" ? vPersona.apellidos : "";
        txtDireccion.Text = vPersona.direccion != "" ? vPersona.direccion : "";
        txtTelefono.Text = vPersona.telefono != "" ? vPersona.telefono : "";
        txtNombre.Text = vPersona.nombres == "" ? "" : vPersona.nombres;

        BeneficiarioService BeneficiarioServicio = new BeneficiarioService();
        List<Beneficiario> LstBeneficiario = new List<Beneficiario>();
        LstBeneficiario = BeneficiarioServicio.ConsultarBeneficiario(cod, (Usuario)Session["usuario"]);

        List<DetalleSolicitudAuxilio> lstDetalle = new List<DetalleSolicitudAuxilio>();
        foreach (Beneficiario nBeneficiario in LstBeneficiario)
        {
            DetalleSolicitudAuxilio pDetalle = new DetalleSolicitudAuxilio();
            pDetalle.codbeneficiarioaux = Convert.ToInt32(nBeneficiario.idbeneficiario);
            pDetalle.identificacion = nBeneficiario.identificacion_ben;
            pDetalle.nombre = nBeneficiario.nombre_ben;
            pDetalle.cod_parentesco = nBeneficiario.parentesco;
            pDetalle.porcentaje_beneficiario = nBeneficiario.porcentaje_ben;
            lstDetalle.Add(pDetalle);
        }

        if (lstDetalle.Count > 0 && lstDetalle != null)
        {
            ValidarPermisosGrilla(gvBeneficiarios);
            gvBeneficiarios.DataSource = lstDetalle;
            gvBeneficiarios.DataBind();
            Session["Beneficiario"] = lstDetalle;
        }
    }

    protected void btnRegresar_Click(object sender, ImageClickEventArgs e)
    {
        Navegar("~/Page/FabricaCreditos/CreditoEducativo/Lista.aspx");
    }

    protected void txtidentificacion_TextChanged(object sender, EventArgs e)
    {
        Control ctrl = gvCodeudores.FooterRow.FindControl("txtidentificacion");
        if (ctrl != null)
        {
            TextBox txtidentificacion = (TextBox)ctrl;
            if (txtidentificacion.Text != "")
            {
                Xpinn.FabricaCreditos.Services.codeudoresService codeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
                Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
                vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["usuario"]);
                gvCodeudores.FooterRow.Cells[2].Text = vcodeudor.codpersona.ToString();
                gvCodeudores.FooterRow.Cells[4].Text = vcodeudor.primer_nombre;
                gvCodeudores.FooterRow.Cells[5].Text = vcodeudor.segundo_nombre;
                gvCodeudores.FooterRow.Cells[6].Text = vcodeudor.primer_apellido;
                gvCodeudores.FooterRow.Cells[7].Text = vcodeudor.segundo_apellido;
                gvCodeudores.FooterRow.Cells[8].Text = vcodeudor.direccion;
                gvCodeudores.FooterRow.Cells[9].Text = vcodeudor.telefono;
            }
            else
            {
                gvCodeudores.FooterRow.Cells[2].Text = "";
                gvCodeudores.FooterRow.Cells[4].Text = "";
                gvCodeudores.FooterRow.Cells[5].Text = "";
                gvCodeudores.FooterRow.Cells[6].Text = "";
                gvCodeudores.FooterRow.Cells[7].Text = "";
                gvCodeudores.FooterRow.Cells[8].Text = "";
                gvCodeudores.FooterRow.Cells[9].Text = "";
            }
        }
    }

    protected void CalcularValor()
    {
        ctlCredito.Text = "";
        decimal valorma = Convert.ToDecimal(ctlNumeroMatricul.Text);
        decimal resultado = valorma;
        if (panelAuxilio.Visible == true)
        {
            decimal valorAuxilio = Convert.ToDecimal(txtValorAux.Text.Replace(".", ""));
            resultado = valorma - valorAuxilio;
        }
        ctlCredito.Text = resultado.ToString("N0");
    }

    private void calculaAuxilio()
    {
        decimal valorma = Convert.ToDecimal(ctlNumeroMatricul.Text.Replace(".",""));
        decimal resultado = valorma * (Int64)Session["Porcentaje"] / 100;
        resultado = Math.Round(resultado);
        txtValorAux.Text = resultado.ToString("N0");
    }

    /// <summary>
    /// Método para el evento de continuar con la re-estructuración
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        VerError("");

        // Cargar datos de la re-estructuración
        Xpinn.Cartera.Services.ReestructuracionService RestrucServicio = new Xpinn.Cartera.Services.ReestructuracionService();
        Xpinn.Cartera.Entities.Reestructuracion vReestructuracion = new Xpinn.Cartera.Entities.Reestructuracion();

        vReestructuracion.lstAtributos = new List<Atributos>();
        Usuario pUsuario = (Usuario)Session["usuario"];
        string error = "";

        // Cargar los atributos del crédito
        Configuracion conf = new Configuracion();
        Atributos vAtributos = new Atributos();
        vReestructuracion.lstAtributos.Add(vAtributos);

        //GRABACION DE CODEUDORES SI SE INGRESO
        vReestructuracion.lstCodeudores = new List<codeudores>();
        foreach (GridViewRow rFila in gvCodeudores.Rows)
        {
            string COD_CODEUDOR = rFila.Cells[2].Text.Replace("&nbsp", "");
            if (COD_CODEUDOR != "0" && COD_CODEUDOR != "")
            {
                Label lblidentificacion = (Label)rFila.FindControl("lblidentificacion");
                if (lblidentificacion.Text == "")
                {
                    VerError("Error en la fila " + rFila.RowIndex);
                    return;
                }

                Xpinn.FabricaCreditos.Entities.codeudores vCodeudores = new Xpinn.FabricaCreditos.Entities.codeudores();

                vCodeudores.codpersona = Convert.ToInt64(COD_CODEUDOR);
                vCodeudores.identificacion = lblidentificacion.Text;
                vCodeudores.numero_radicacion = 0;

                // Validar datos del codeudor
                string sError = "";
                VerError("");
                CodeudorServicio.ValidarCodeudor(vCodeudores, (Usuario)Session["usuario"], ref sError);
                if (sError.Trim() != "")
                {
                    VerError(sError);
                    return;
                }

                //DATOS PARA GRABAR EL CODEUDOR                    
                vCodeudores.idcodeud = 0;
                vCodeudores.tipo_codeudor = "S";
                vCodeudores.parentesco = 0;
                vCodeudores.opinion = "B";
                vCodeudores.responsabilidad = null;

                vReestructuracion.lstCodeudores.Add(vCodeudores);
            }
        }

        String pFormaPago = String.Empty;
        pFormaPago = ddlFormaPago.SelectedValue == "1" ? "C" : "N";
        Int64 numero_radicacion = Int64.MinValue;
        DateTime pFecha1erPago = ctlFechaPPago.Text == "" ? DateTime.MinValue : Convert.ToDateTime(ctlFechaPPago.Text);
        Int64 pCod_Empresa = ddlEmpresa.Visible == true && ddlEmpresa.SelectedIndex != 0 ? Convert.ToInt64(ddlEmpresa.SelectedValue) : 0;
        CreditoEducativoEntit entitiCredito = new CreditoEducativoEntit
        {

            PFECHA_SOLICITUD = Convert.ToDateTime(ctlFecha.Text),
            PCOD_DEUDOR = (Int64)Session["Cod_Persona"],
            PVALOR_MATRICULA = Convert.ToDecimal(ctlNumeroMatricul.Text),
            PMONTO_SOLICITADO = Convert.ToDecimal(ctlCredito.Text.Replace(".","")),
            PNUMERO_CUOTAS = Convert.ToInt32(ddlPlazo.Text),
            PCOD_LINEA_CREDITO = Convert.ToString(ddlLineaCredito.SelectedValue),
            PCOD_PERIODICIDAD = Convert.ToInt32(ddlperiodicidad.cod_periodicidad),
            PCOD_OFICINA = Convert.ToInt32(pUsuario.cod_oficina),
            PFORMA_PAGO = pFormaPago,
            PFECHA_PRIMERPAGO = pFecha1erPago,
            PCOD_ASESOR = 1,
            PCOD_EMPRESA = pCod_Empresa,
            universidad = txtUniversidad.Text,
            semestre = txtSemestre.Text
        };
        SolicitudAuxilio entiti = new SolicitudAuxilio();
        entiti = null;
        if (panelAuxilio.Visible == true)
        {
            entiti = new SolicitudAuxilio();
            if (ctlFecha.ToDateTime != null)
                entiti.fecha_solicitud = ctlFecha.ToDateTime;
            else
                entiti.fecha_solicitud = DateTime.Now;
            entiti.cod_persona = (Int64)Session["Cod_Persona"];
            entiti.cod_linea_auxilio = ddlLineaAuxilio.SelectedValue;
            entiti.valor_solicitado = Convert.ToDecimal(txtValorAux.Text.Replace(".", ""));
            entiti.fecha_aprobacion = DateTime.MinValue;
            entiti.valor_aprobado = 0;
            entiti.fecha_desembolso = DateTime.MinValue;
            entiti.porc_matricula = Convert.ToDecimal(ctlNumeroMatricul.Text);
            //entiti.lstDetalle = ObtenerListaBeneficiario();
            entiti.estado = "S";
        }
        List<DetalleSolicitudAuxilio> lstDetalle = new List<DetalleSolicitudAuxilio>(); 
        if (gvBeneficiarios.Rows.Count > 0)        
            lstDetalle = ObtenerListaBeneficiario();        

        CreditoOrdenServicio CredOrden = new CreditoOrdenServicio();
        Auxilio_Orden_Servicio AuxOrden = new Auxilio_Orden_Servicio();
        if (ctlBusquedaProveedor.VisibleCtl == true && ctlBusquedaProveedor.CheckedOrd == true)
        {
            Xpinn.FabricaCreditos.Entities.LineasCredito vLineasCredito = new Xpinn.FabricaCreditos.Entities.LineasCredito();
            vLineasCredito = LineasCreditoServicio.ConsultarLineasCredito(Convert.ToString(ddlLineaCredito.SelectedValue), (Usuario)Session["usuario"]);
            if (vLineasCredito.orden_servicio == 1 && ctlBusquedaProveedor.CheckedOrd == true)
            {
                CredOrden.idordenservicio = Convert.ToInt64(0);
                CredOrden.numero_radicacion = Convert.ToInt64(0);
                if (ctlBusquedaProveedor.TextCodigo == 0)
                    CredOrden.cod_persona = (Int64)Session["Cod_Persona"];
                else
                    CredOrden.cod_persona = Convert.ToInt64(ctlBusquedaProveedor.TextCodigo);
                CredOrden.idproveedor = ctlBusquedaProveedor.TextIdentif;
                CredOrden.nomproveedor = ctlBusquedaProveedor.TextNomProv;
                CredOrden.detalle = null;
                CredOrden.estado = null;
            }

            // Determinar si la línea de auxilio genera una orden de servicio
            LineaAuxilio vDatosLinea = new LineaAuxilio();
            if (panelAuxilio.Visible == true)
            {
                vDatosLinea = LineaAux.ConsultarLineaAUXILIO(ddlLineaAuxilio.SelectedValue, (Usuario)Session["usuario"]);
                if (vDatosLinea.orden_servicio == 1 && ctlBusquedaProveedor.CheckedOrd == true)
                {
                    AuxOrden.idordenservicio = Convert.ToInt32(0);
                    AuxOrden.numero_auxilio = Convert.ToInt32(0);
                    AuxOrden.cod_persona = (Int64)Session["Cod_Persona"];
                    AuxOrden.idproveedor = ctlBusquedaProveedor.TextIdentif;
                    AuxOrden.nomproveedor = ctlBusquedaProveedor.TextNomProv;
                    AuxOrden.detalle = null;
                    AuxOrden.estado = 1;
                }
            }
        }

        if (panelAuxilio.Visible == true)
        {
            if (lstDetalle.Count > 0)
            {
                if (!validaGrid())
                {
                    return;
                }
            }
        }

         // Validar biometria
        String codigoPrograma = objAhorraServi.CodigoProgramaCreditoE;
            string srError = "";
            string Cod_persona = Session["Cod_persona"].ToString();
            if (ctlValidarBiometria.IniciarValidacion(Convert.ToInt32(codigoPrograma), objAhorraServi.CodigoProgramaCreditoE, Convert.ToInt64(Cod_persona), DateTime.Now, ref srError))
            {
                VerError(srError);
                return;
            }
        
        CreditoService lineas = new CreditoService();
        lineas.CrearSolicitudCreditoServices((Usuario)Session["usuario"], CredOrden, AuxOrden,
            entitiCredito, vReestructuracion, ref numero_radicacion, ref error, entiti, lstDetalle);

        if (error == "")
        {
            mvReestructuras.ActiveViewIndex = 0;
            Session["numero_radicacion"] = numero_radicacion;
            lblMensajeFin.Text = "Solicitud Realizada Con Exito Con El Numero de Radicacion " + numero_radicacion;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            mvReestructuras.ActiveViewIndex = 1;
        }
        else
        {
            VerError(error);
        }
    }

    protected void btnGuardado_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnFinalClick(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void chkCapitalizar_CheckedChanged(object sender, EventArgs e)
    {
        CalcularValor();
    }

    protected void txtFechaIngreso_TextChanged(object sender, EventArgs e)
    {
        CalcularValor();
    }

    protected void gvCodeudores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtidentificacion = (TextBox)gvCodeudores.FooterRow.FindControl("txtidentificacion");
            if (txtidentificacion.Text.Trim() == "")
            {
                VerError("Ingrese la Identificación del Codeudor por favor.");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                return;
            }
            Xpinn.Comun.Data.GeneralData DAGeneral = new Xpinn.Comun.Data.GeneralData();
            Xpinn.Comun.Entities.General pEntidad = new Xpinn.Comun.Entities.General();
            pEntidad = DAGeneral.ConsultarGeneral(480, (Usuario)Session["usuario"]);
            try
            {
                if (pEntidad.valor != null)
                {
                    if (Convert.ToInt32(pEntidad.valor) > 0)
                    {
                        int paramCantidad = 0, cantReg = 0;
                        paramCantidad = Convert.ToInt32(pEntidad.valor);
                        Xpinn.FabricaCreditos.Entities.codeudores pCodeu = new Xpinn.FabricaCreditos.Entities.codeudores();
                        pCodeu = CodeudorServicio.ConsultarCantidadCodeudores(txtidentificacion.Text, (Usuario)Session["usuario"]);
                        if (pCodeu.cantidad != null)
                        {
                            cantReg = Convert.ToInt32(pCodeu.cantidad);
                            if (cantReg >= paramCantidad)
                            {
                                VerError("No puede adicionar esta persona debido a que ya mantiene el límite de veces como codeudor.");
                                ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                                return;
                            }
                        }
                    }
                }
            }
            catch { }

            List<Persona1> lstCodeudor = new List<Persona1>();
            lstCodeudor = (List<Persona1>)Session["Codeudores"];

            if (lstCodeudor.Count == 1)
            {
                Persona1 gItem = new Persona1();
                gItem = lstCodeudor[0];
                if (gItem.cod_persona == 0)
                    lstCodeudor.Remove(gItem);
            }
            if (lstCodeudor.Count == 2)
                lstCodeudor.RemoveAt(1);

            Xpinn.FabricaCreditos.Services.codeudoresService codeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
            Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
            vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["usuario"]);
            Persona1 gItemNew = new Persona1();
            gItemNew.cod_persona = vcodeudor.codpersona;
            gItemNew.identificacion = vcodeudor.identificacion;
            gItemNew.primer_nombre = vcodeudor.primer_nombre;
            gItemNew.segundo_nombre = vcodeudor.segundo_nombre;
            gItemNew.primer_apellido = vcodeudor.primer_apellido;
            gItemNew.segundo_apellido = vcodeudor.segundo_apellido;
            gItemNew.direccion = vcodeudor.direccion;
            gItemNew.telefono = vcodeudor.telefono;
            lstCodeudor.Add(gItemNew);
            gvCodeudores.DataSource = lstCodeudor;
            gvCodeudores.DataBind();
            Session["Codeudores"] = lstCodeudor;
            if (lstCodeudor.Count > 0)
            {
                lblTotReg.Visible = true;
                lblTotReg.Text = "<br/> Codeudores a registrar : " + lstCodeudor.Count.ToString();
                lblTotalRegsCodeudores.Visible = false;
            }
            else
            {
                lblTotReg.Visible = false;
                lblTotalRegsCodeudores.Visible = true;
            }
        }
        if (e.CommandName.Equals("Delete"))
        {
            List<Persona1> lstCodeudores = new List<Persona1>();
            lstCodeudores = (List<Persona1>)Session["Codeudores"];
            if (lstCodeudores.Count >= 1)
            {
                Persona1 eCodeudor = new Persona1();
                int index = Convert.ToInt32(e.CommandArgument);
                eCodeudor = lstCodeudores[index];
                if (eCodeudor.cod_persona != 0)
                {
                    lstCodeudores.Remove(eCodeudor); //PENDIENTE
                    //CodeudorServicio.EliminarcodeudoresCred(eCodeudor.cod_persona, Convert.ToInt64(txtCredito.Text), (Usuario)Session["usuario"]);
                }
                Session["Codeudores"] = lstCodeudores;
            }
            if (lstCodeudores.Count == 0)
            {
                lblTotReg.Visible = false;
                lblTotalRegsCodeudores.Visible = true;
                InicialCodeudores();
            }
            else
            {
                lblTotReg.Visible = true;
                lblTotReg.Text = "<br/> Codeudores a registrar : " + lstCodeudores.Count.ToString();
                lblTotalRegsCodeudores.Visible = false;
                gvCodeudores.DataSource = lstCodeudores;
                gvCodeudores.DataBind();
                Session["Codeudores"] = lstCodeudores;
            }
        }
    }




    protected void gvCodeudores_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void ddllineas_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Atributos> lstAtributos = new List<Atributos>();
    }

    protected void InicializarBeneficiarios()
    {
        List<DetalleSolicitudAuxilio> lstBeneficiarios = new List<DetalleSolicitudAuxilio>();
        for (int i = gvBeneficiarios.Rows.Count; i < 5; i++)
        {
            DetalleSolicitudAuxilio pDetalle = new DetalleSolicitudAuxilio();
            pDetalle.codbeneficiarioaux = -1;
            pDetalle.identificacion = "";
            pDetalle.nombre = "";
            pDetalle.cod_parentesco = null;
            pDetalle.porcentaje_beneficiario = null;
            lstBeneficiarios.Add(pDetalle);
        }
        gvBeneficiarios.DataSource = lstBeneficiarios;
        gvBeneficiarios.DataBind();

        Session["Beneficiario"] = lstBeneficiarios;
    }


    protected void btnAdicionarFila_Click(object sender, EventArgs e)
    {
        ObtenerListaBeneficiario();
        List<DetalleSolicitudAuxilio> LstPrograma = new List<DetalleSolicitudAuxilio>();
        if (Session["Beneficiario"] != null)
        {
            LstPrograma = (List<DetalleSolicitudAuxilio>)Session["Beneficiario"];

            for (int i = 1; i <= 1; i++)
            {
                DetalleSolicitudAuxilio pDetalle = new DetalleSolicitudAuxilio();
                pDetalle.codbeneficiarioaux = -1;
                pDetalle.identificacion = "";
                pDetalle.nombre = "";
                pDetalle.cod_parentesco = null;
                pDetalle.porcentaje_beneficiario = null;
                LstPrograma.Add(pDetalle);
            }
            gvBeneficiarios.DataSource = LstPrograma;
            gvBeneficiarios.DataBind();

            Session["Beneficiario"] = LstPrograma;
        }
    }

    protected void gvBeneficiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlParentesco = (DropDownListGrid)e.Row.FindControl("ddlParentesco");
            if (ddlParentesco != null)
                PoblarLista("parentescos", ddlParentesco);

            Label lblParentesco = (Label)e.Row.FindControl("lblParentesco");
            if (lblParentesco != null)
                ddlParentesco.SelectedValue = lblParentesco.Text;

        }
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

    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaBeneficiario();

        List<DetalleSolicitudAuxilio> LstDetalle = new List<DetalleSolicitudAuxilio>();
        LstDetalle = (List<DetalleSolicitudAuxilio>)Session["Beneficiario"];
        if (conseID > 0)
        {
            try
            {
                foreach (DetalleSolicitudAuxilio acti in LstDetalle)
                {
                    if (acti.codbeneficiarioaux == conseID)
                    {
                        LstDetalle.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
            catch //(Exception ex)
            {
                //BOexcepcion.Throw(SolicAuxilios.CodigoPrograma, "gvProgramacion_RowDeleting", ex);
            }
        }
        else
        {
            LstDetalle.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);
        }
        Session["Beneficiario"] = LstDetalle;

        gvBeneficiarios.DataSource = LstDetalle;
        gvBeneficiarios.DataBind();
    }

    protected List<DetalleSolicitudAuxilio> ObtenerListaBeneficiario()
    {
        try
        {
            List<DetalleSolicitudAuxilio> lstDetalle = new List<DetalleSolicitudAuxilio>();
            List<DetalleSolicitudAuxilio> lista = new List<DetalleSolicitudAuxilio>();

            foreach (GridViewRow rfila in gvBeneficiarios.Rows)
            {
                DetalleSolicitudAuxilio ePogra = new DetalleSolicitudAuxilio();

                Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
                if (lblCodigo != null)
                    ePogra.codbeneficiarioaux = Convert.ToInt32(lblCodigo.Text);
                else
                    ePogra.codbeneficiarioaux = -1;

                TextBoxGrid txtIdenti_Grid = (TextBoxGrid)rfila.FindControl("txtIdenti_Grid");
                if (txtIdenti_Grid.Text != "")
                    ePogra.identificacion = txtIdenti_Grid.Text;

                TextBoxGrid txtNombreComple = (TextBoxGrid)rfila.FindControl("txtNombreComple");
                if (txtNombreComple.Text != "")
                    ePogra.nombre = txtNombreComple.Text;

                DropDownListGrid ddlParentesco = (DropDownListGrid)rfila.FindControl("ddlParentesco");
                if (ddlParentesco.SelectedIndex != 0)
                    ePogra.cod_parentesco = Convert.ToInt32(ddlParentesco.SelectedValue);

                TextBoxGrid txtPorcBene = (TextBoxGrid)rfila.FindControl("txtPorcBene");
                if (txtPorcBene.Text.Trim() != "")
                    ePogra.porcentaje_beneficiario = Convert.ToDecimal(txtPorcBene.Text);

                lista.Add(ePogra);
                Session["Beneficiario"] = lista;

                if (ePogra.identificacion != null && ePogra.cod_parentesco != 0 && ePogra.porcentaje_beneficiario != 0)
                {
                    lstDetalle.Add(ePogra);
                }
            }
            return lstDetalle;
        }
        catch //(Exception ex)
        {
            //BOexcepcion.Throw(SolicAuxilios.CodigoPrograma, "ObtenerListaBeneficiario", ex);
            return null;
        }
    }

    public bool validaGrid()
    {
        foreach (GridViewRow rfila in gvBeneficiarios.Rows)
        {
            DetalleSolicitudAuxilio ePogra = new DetalleSolicitudAuxilio();

            Label lblCodigo = (Label)rfila.FindControl("lblCodigo");
            if (lblCodigo != null)
            {
                ePogra.codbeneficiarioaux = Convert.ToInt32(lblCodigo.Text);
            }
            else
            {
                if (lblCodigo.Text.Trim() == "" || lblCodigo.Text.Length <= 0)
                {
                    VerError("Ingrese El codigo");
                    return false;
                }
            }

            TextBoxGrid txtIdenti_Grid = (TextBoxGrid)rfila.FindControl("txtIdenti_Grid");
            TextBoxGrid txtNombreComple = (TextBoxGrid)rfila.FindControl("txtNombreComple");
            if (txtIdenti_Grid.Text != "" || txtNombreComple.Text.Trim() != "")
            {
                ePogra.identificacion = txtIdenti_Grid.Text;
                if (txtNombreComple.Text.Trim() != "")
                {
                    ePogra.nombre = txtNombreComple.Text;
                }
                else
                {
                    VerError("Ingrese Nombre Completo del Beneficiario");
                    return false;
                }
                DropDownListGrid ddlParentesco = (DropDownListGrid)rfila.FindControl("ddlParentesco");
                if (ddlParentesco.SelectedIndex != 0)
                    ePogra.cod_parentesco = Convert.ToInt32(ddlParentesco.SelectedValue);
                else
                {
                    VerError("seleccione El Parentesco");
                    return false;
                }
                TextBoxGrid txtPorcBene = (TextBoxGrid)rfila.FindControl("txtPorcBene");
                if (txtPorcBene.Text.Trim() != "")
                    ePogra.porcentaje_beneficiario = Convert.ToDecimal(txtPorcBene.Text);
                else
                {
                    VerError("Ingrese El Porcentaje del Beneficiario");
                    return false;
                }
            }
        }
        return true;
    }

    protected void txtNumer_textChange(object sender, EventArgs e)
    {
        if (ctlNumeroMatricul.Text.Trim() != "" || Convert.ToDecimal(ctlNumeroMatricul.Text) > 0)
        {
            if(panelAuxilio.Visible == true)
                calculaAuxilio();
            CalcularValor();
        }
    }

    protected void ddlLineaAuxilio_SelectedIndexChanged(object sender, EventArgs e)
    {
        ctlBusquedaProveedor.VisibleCtl = false;
        ctlBusquedaProveedor.CheckedOrd = false;
        if (ddlLineaAuxilio.SelectedItem != null)
        {
            Xpinn.FabricaCreditos.Services.LineasCreditoService LineasServicios = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
            if (ddlLineaAuxilio.SelectedValue != "")
            {
                Session["Porcentaje"] = LineasServicios.getPorcentajeMatriculaServices(ddlLineaAuxilio.SelectedValue, (Usuario)Session["usuario"]).porc_corto;
            }
            if (ddlLineaCredito.SelectedItem != null)
                ConsultarOrdenServicioCredito(ddlLineaCredito.SelectedValue);

            if (ctlBusquedaProveedor.VisibleCtl == false)
                ConsultarOrdenServicioAuxilio(ddlLineaAuxilio.SelectedValue);
        }
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObtenerFechaInicio();
    }

    protected void btnPlanPagosClick(object sender, EventArgs e)
    {
        if (Session["numero_radicacion"] != null)
        {
            Xpinn.FabricaCreditos.Services.CreditoPlanService creditoPlanServicio = new Xpinn.FabricaCreditos.Services.CreditoPlanService();
            Session[creditoPlanServicio.CodigoPrograma + ".id"] = Session["numero_radicacion"];
            Navegar("~/Page/FabricaCreditos/PlanPagos/Detalle.aspx");
        }
    }

    protected void btnAprobacionClick(object sender, EventArgs e)
    {
        if (Session["numero_radicacion"] != null)
        {
            Xpinn.FabricaCreditos.Services.CreditoSolicitadoService creditoServicio = new Xpinn.FabricaCreditos.Services.CreditoSolicitadoService();
            Session[creditoServicio.CodigoPrograma + ".id"] = Session["numero_radicacion"];
            Navegar("~/Page/FabricaCreditos/CreditosPorAprobar/Detalle.aspx");
        }
    }

}