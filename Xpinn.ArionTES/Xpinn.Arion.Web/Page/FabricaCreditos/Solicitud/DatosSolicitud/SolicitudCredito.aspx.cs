using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms.VisualStyles;
using Xpinn.Ahorros.Entities;
using Xpinn.Ahorros.Services;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Comun.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.Interfaces.Entities;
using Xpinn.Interfaces.Services;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Services;
using Xpinn.Util;

public partial class Detalle : GlobalWeb
{
    private PoblarListas poblar = new PoblarListas();
    private Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();  //Lista de los menus desplegables    
    private Xpinn.FabricaCreditos.Services.DatosSolicitudService DatosSolicitudServicio = new Xpinn.FabricaCreditos.Services.DatosSolicitudService();
    private Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
    private CreditoRecogerService creditoRecogerServicio = new CreditoRecogerService();
    private Xpinn.FabricaCreditos.Services.LineasCreditoService LineasCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
    private CreditoPlanService creditoPlanServicio = new CreditoPlanService();
    private Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService SolicitudCreditosRecogidosServicio = new Xpinn.FabricaCreditos.Services.SolicitudCreditosRecogidosService();
    private Xpinn.FabricaCreditos.Services.codeudoresService CodeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
    private String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
    private long _codPersona;
    private int cod_afiancol = 60;
    bool _validaCodeudores;
    string _validacion;

    // Servicio para Consultar Avances 
    Xpinn.Asesores.Services.DetalleProductoService DetalleProducto = new Xpinn.Asesores.Services.DetalleProductoService();

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ((Label)Master.FindControl("lblNombresApellidos")).Text = Session["Nombres"].ToString();
            ((Label)Master.FindControl("lblIdCliente")).Text = Session["Identificacion"].ToString();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoPrograma + "A", "Page_PreInit", ex);
        }
        try
        {
            VisualizarOpciones(DatosClienteServicio.CodigoPrograma, "D");

            Site1 toolBar = (Site1)this.Master;
            toolBar.eventoGuardar += btnGuardarDatosSolicitud_Click;
            toolBar.eventoCancelar += btnAtr0_Click;
            ctlValidarBiometria.eventoGuardarimg += btnGuardarDatosSolicitud_Click;
            ImageButton btnAdelante = Master.FindControl("btnAdelante") as ImageButton;
            btnAdelante.ImageUrl = "~/Images/btnGrupoFamiliar.jpg";
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoPrograma + "A", "Page_PreInit", ex);
        }


    }

    /// <summary>
    /// Cargar información apenas se ingresa a la opción
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Cod_persona"] != null)
                _codPersona = Convert.ToInt64(Session["Cod_persona"]);
            else
                _codPersona = 0;
            if (!IsPostBack)
            {
                LimpiarVariablesSesion();

                if (Session["TipoCredito"] == null)
                    Session["TipoCredito"] = ClasificacionCredito.Consumo;
                // Inicializar datos para orden de servicio
                ctlBusquedaProveedor.cargar = "0";
                ctlBusquedaProveedor.VisibleCtl = false;
                // Determinar datos de encabezado de la solicitud
                lblFecha.Text = DateTime.Today.ToShortDateString();
                // Llenar información de los combos o listas desplegables
                if (!CargarListas())
                    return;
                // Recuperar la forma de pago por defecto
                string FormaPago = ConfigurationManager.AppSettings["ddlFormaPago"].ToString();
                if (FormaPago != null && FormaPago != "")
                {
                    ddlFormaPago.SelectedValue = FormaPago;
                }
                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                if (ddlTipoCredito.Items.Count == 1)
                    ddlTipoCredito.SelectedIndex = 0;
                if (ddlTipoCredito.Items.Count > 0)
                    ddlTipoCredito_TextChanged(ddlTipoCredito, null);
                validarMedio();
                // Cargar el código de la persona si se paso desde la consulta de datacredito
                if (Session[DatosClienteServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[DatosClienteServicio.CodigoPrograma + ".id"].ToString();
                    CuotasExtras.TablaCuoExt(idObjeto);
                }
                // Cargar el número de solicitud
                if (Session["Solicitud"] != null)
                {
                    lblNumero.Text = Session["Solicitud"].ToString();
                }
                // Deshabilitar campos que dependen de la línea seleccionada
                txtPeriodo.Enabled = false;
                Usuario usuap = (Usuario)Session["usuario"];
                lblOficina.Text = usuap.nombre_oficina;
                // Cargando algunos datos por defecto
                if (Session[DatosClienteServicio.CodigoPrograma + ".linea"] != null)
                {
                    ddlTipoCredito.SelectedValue = Session[DatosClienteServicio.CodigoPrograma + ".linea"].ToString();
                    Session.Remove(DatosClienteServicio.CodigoPrograma + ".linea");
                    ddlTipoCredito_TextChanged(ddlTipoCredito, null);
                }
                if (Session[DatosClienteServicio.CodigoPrograma + ".monto"] != null)
                {
                    txtMonto.Text = Session[DatosClienteServicio.CodigoPrograma + ".monto"].ToString();
                    Session.Remove(DatosClienteServicio.CodigoPrograma + ".monto");
                }
                if (Session[DatosClienteServicio.CodigoPrograma + ".plazo"] != null)
                {
                    txtPlazo.Text = Session[DatosClienteServicio.CodigoPrograma + ".plazo"].ToString();
                    Session.Remove(DatosClienteServicio.CodigoPrograma + ".plazo");
                }
                if (Session[DatosClienteServicio.CodigoPrograma + ".cuota"] != null)
                {
                    txtCuota.Text = Session[DatosClienteServicio.CodigoPrograma + ".cuota"].ToString();
                    Session.Remove(DatosClienteServicio.CodigoPrograma + ".cuota");
                }
                if (Session[DatosClienteServicio.CodigoPrograma + ".periodicidad"] != null)
                {
                    ddlPeriodicidad.SelectedValue = Session[DatosClienteServicio.CodigoPrograma + ".periodicidad"].ToString();
                    Session.Remove(DatosClienteServicio.CodigoPrograma + ".periodicidad");
                }

                ddlTipoCredito.Focus();
                txtPlazo.Focus();
                TablaCodeudores(); // Consultar si tiene codeudores si los tiene los muestra, si no inicializa la gv vacia
                CuotasExtras.InicialCuoExt();
                TablaCreditosRecogidos(0);
                InicializarReferencias();
                LlenarDDLQuienReferenciaGVReferencias(new List<Persona1>());
                //Llena la variable para las cuotas extras
                LlenarVariables();

                // Parametro general para habilitar proceso de WM
                General habilitarOperacionesWM = ConsultarParametroGeneral(35);
                if (habilitarOperacionesWM != null && habilitarOperacionesWM.valor == "1")
                {
                    // Se necesita el codigo de tipo de informacion adicional de persona del barCode, si no existe BOOOOOOOOM
                    InformacionAdicionalServices informacionAdicionalService = new InformacionAdicionalServices();
                    string codigoTipoInformacionPersonalBarCode = ConfigurationManager.AppSettings["CodigoTipoInformacionAdicionalWorkManagement"];

                    if (!string.IsNullOrWhiteSpace(codigoTipoInformacionPersonalBarCode))
                    {
                        // Se necesita el barCode para identificar la persona en el WM
                        string barCode = informacionAdicionalService.ConsultarInformacionPersonalDeUnaPersona(Convert.ToInt64(Session["Cod_persona"].ToString()), Convert.ToInt64(codigoTipoInformacionPersonalBarCode), Usuario);

                        // Si el barcode no existe en financial, procedo a validar que ya exista en el WM
                        if (string.IsNullOrWhiteSpace(barCode))
                        {
                            InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);

                            Persona1Service personaService = new Persona1Service();
                            string identificacion = personaService.ConsultarIdentificacionPersona(_codPersona, Usuario);

                            Persona1 personaConsultada = interfaz.ConsultarInformacionPersonaPorIdentificacion(identificacion);

                            // Si la persona ya existe en el WM, entonces la creo y prosigo
                            if (personaConsultada != null && !string.IsNullOrWhiteSpace(personaConsultada.barCode))
                            {
                                InformacionAdicionalServices infoSer = new InformacionAdicionalServices();
                                InformacionAdicional pAdicional = new InformacionAdicional
                                {
                                    cod_persona = _codPersona,
                                    cod_infadicional = Convert.ToInt32(codigoTipoInformacionPersonalBarCode),
                                    valor = personaConsultada.barCode
                                };

                                // Creo la informacion adicional para esta persona con el barCode
                                pAdicional = infoSer.CrearPersona_InfoAdicional(pAdicional, Usuario);
                            }
                            else
                            {
                                // Si definitivamente no existe el barcode ni en Financial ni en el WM, muestro la alerta
                                VerError("No se encuentra el codigo del registro del WorkManagement para esta persona(No esta registrada en el WM), si prosigues no se guardara en el WorkManagement");
                            }
                        }
                    }
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "D", "Page_Load", ex);
        }
    }

    protected void AsignarEventoConfirmar()
    {
        ConfirmarEventoBoton((LinkButton)Master.FindControl("btnEliminar"), "Esta seguro que desea eliminar el registro?");
    }


    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "A", "ObtenerDatos", ex);
        }
    }


    /// <summary>
    /// Cargar información de las listas desplegables
    /// </summary>
    private Boolean CargarListas()
    {
        try
        {


            if (Session["ES_EMPLEADO"] != null)
            {
                ListaSolicitada = "STipoCreditoEmple";
                Session.Remove("ES_EMPLEADO");
            }
            else
            {
                var s = Session["TipoCredito"];
                ClasificacionCredito clasificacion = (ClasificacionCredito)Session["TipoCredito"];

                if (clasificacion == ClasificacionCredito.Consumo)
                    ListaSolicitada = "STipoCreditoConsumo";
                else if (clasificacion == ClasificacionCredito.MicroCredito)
                    ListaSolicitada = "STipoCreditoMicro";
                else if (clasificacion == ClasificacionCredito.Vivienda)
                    ListaSolicitada = "STipoCreditoVivienda";
                else if (clasificacion == ClasificacionCredito.Comercial)
                    ListaSolicitada = "STipoCreditoComercial";
            }
            TraerResultadosLista();
            if (lstDatosSolicitud.Count <= 0)
            {
                VerError("No existe registros para la lista solicitada " + ListaSolicitada);
                return false;
            }
            ddlTipoCredito.DataSource = lstDatosSolicitud;
            ddlTipoCredito.DataTextField = "ListaDescripcion";
            if (ListaSolicitada == "STipoCreditoVivienda" || ListaSolicitada == "STipoCreditoComercial")
                ddlTipoCredito.DataValueField = "ListaId";
            else
                ddlTipoCredito.DataValueField = "ListaIdStr";
            ddlTipoCredito.DataBind();

            ListaSolicitada = "Periodicidad";
            TraerResultadosLista();
            ddlPeriodicidad.DataSource = lstDatosSolicitud;
            ddlPeriodicidad.DataTextField = "ListaDescripcion";
            ddlPeriodicidad.DataValueField = "ListaIdStr";
            ddlPeriodicidad.DataBind();

            ListaSolicitada = "Medio";
            TraerResultadosLista();
            ddlMedio.DataSource = lstDatosSolicitud;
            ddlMedio.DataTextField = "ListaDescripcion";
            ddlMedio.DataValueField = "ListaIdStr";
            ddlMedio.DataBind();

            ListaSolicitada = "TipoLiquidacion";
            TraerResultadosLista();
            ddlTipoLiquidacion.DataSource = lstDatosSolicitud;
            ddlTipoLiquidacion.DataTextField = "ListaDescripcion";
            ddlTipoLiquidacion.DataValueField = "ListaId";
            ddlTipoLiquidacion.DataBind();

            ListItem selectedListItem2 = ddlPeriodicidad.Items.FindByValue("1"); //Selecciona mensual por defecto
            if (selectedListItem2 != null)
                selectedListItem2.Selected = true;

            if (Session["TipoCredito"].ToString() == "M")
            {
                ListItem selectedListItem = ddlTipoCredito.Items.FindByValue("301"); // Selección microcrédito por defecto
                if (selectedListItem != null)
                    selectedListItem.Selected = true;
            }

            if (Session["TipoCredito"].ToString() == "C")
            {
                ListItem selectedListItem = ddlTipoCredito.Items.FindByValue("103"); // Selección microcrédito por defecto
                if (selectedListItem != null)
                    selectedListItem.Selected = true;
            }
            ListItem selectedTipoLiq = ddlTipoLiquidacion.Items.FindByValue("2"); // Seleccionar tipo de liquidación por defecto
            if (selectedTipoLiq != null)
                selectedTipoLiq.Selected = true;

            asesores();
            Calcular_Cupo();

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            List<Xpinn.Tesoreria.Entities.EmpresaRecaudo> lstEmpresas = new List<Xpinn.Tesoreria.Entities.EmpresaRecaudo>();
            Xpinn.Tesoreria.Entities.EmpresaRecaudo empresa = new Xpinn.Tesoreria.Entities.EmpresaRecaudo();
            Xpinn.Tesoreria.Services.EmpresaRecaudoServices empresaServicio = new Xpinn.Tesoreria.Services.EmpresaRecaudoServices();
            if (Session["Cod_persona"] == null)
            {
                ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudo(empresa, pUsuario);
            }
            else
            {
                try
                {
                    Int64 Cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
                    ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudoPersona(Cod_persona, pUsuario);
                }
                catch
                {
                    ddlEmpresa.DataSource = empresaServicio.ListarEmpresaRecaudo(empresa, pUsuario);
                }
            }
            ddlEmpresa.DataTextField = "nom_empresa";
            ddlEmpresa.DataValueField = "cod_empresa";
            ddlEmpresa.AppendDataBoundItems = true;
            ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlEmpresa.SelectedIndex = 0;
            ddlEmpresa.DataBind();

            // Llenando listas desplegables

            ddlTipo_cuenta.Items.Insert(0, new ListItem("Ahorros", "0"));
            ddlTipo_cuenta.Items.Insert(1, new ListItem("Corriente", "1"));
            ddlTipo_cuenta.DataBind();

            DropDownFormaDesembolso.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            DropDownFormaDesembolso.Items.Insert(1, new ListItem("Efectivo", "1"));
            DropDownFormaDesembolso.Items.Insert(2, new ListItem("Cheque", "2"));
            DropDownFormaDesembolso.Items.Insert(3, new ListItem("Transferencia", "3"));
            DropDownFormaDesembolso.Items.Insert(4, new ListItem("Tranferencia Cuenta Ahorro Vista Interna", "4"));
            DropDownFormaDesembolso.Items.Insert(5, new ListItem("Otros", "5"));
            DropDownFormaDesembolso.DataBind();
            DropDownFormaDesembolso.SelectedIndex = 1;

            ActivarDesembolso();

            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
            DropDownEntidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
            DropDownEntidad.DataTextField = "nombrebanco";
            DropDownEntidad.DataValueField = "cod_banco";
            DropDownEntidad.DataBind();

            AhorroVistaServices ahorroServices = new AhorroVistaServices();
            List<AhorroVista> lstAhorros = ahorroServices.ListarCuentaAhorroVista(Convert.ToInt64(_codPersona), Usuario);

            ddlCuentaAhorroVista.DataSource = lstAhorros;
            ddlCuentaAhorroVista.DataTextField = "numero_cuenta";
            ddlCuentaAhorroVista.DataValueField = "numero_cuenta";
            ddlCuentaAhorroVista.DataBind();

            ActividadesServices ActividadServicio = new ActividadesServices();
            string filtro = string.Empty;
            List<Xpinn.FabricaCreditos.Entities.CuentasBancarias> LstCuentasBanc = ActividadServicio.ConsultarCuentasBancarias(_codPersona, filtro, (Usuario)Session["usuario"]);

            ViewState.Add("lstCuentasSolicitudCredito", LstCuentasBanc);

            return true;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "L", "CargarListas", ex);
            return false;
        }
    }

    /// <summary>
    /// Cargar listado de asesores comerciales
    /// </summary>
    private void asesores()
    {
        string ddlusuarios = ConfigurationManager.AppSettings["ddlusuarios"].ToString();
        if (ddlusuarios == "1")
        {
            // Cargar los asesores ejecutivos
            UsuarioAseService serviceEjecutivo = new UsuarioAseService();
            UsuarioAse ejec = new UsuarioAse();
            List<UsuarioAse> lstAsesores = new List<UsuarioAse>();
            lstAsesores = serviceEjecutivo.ListartodosUsuarios(ejec, (Usuario)Session["usuario"]);
            if (lstAsesores.Count > 0)
            {
                Ddlusuarios.DataSource = lstAsesores;
                Ddlusuarios.DataTextField = "nombre";
                Ddlusuarios.DataValueField = "codusuario";
                Ddlusuarios.DataBind();
                Ddlusuarios.Items.Insert(0, new ListItem("<Seleccione un Item>", "-1"));
            }
            else
            {
                ddlusuarios = "0";
            }
        }
        Xpinn.Util.Usuario usu = new Xpinn.Util.Usuario();
        if (ddlusuarios != "1")
        {
            // Cargar usuarios cuando no se manejan asesores
            Xpinn.Seguridad.Services.UsuarioService serviceEjecutivo = new Xpinn.Seguridad.Services.UsuarioService();
            usu.estado = 1;
            Ddlusuarios.DataSource = serviceEjecutivo.ListarUsuario(usu, (Usuario)Session["usuario"]);
            Ddlusuarios.DataTextField = "nombre";
            Ddlusuarios.DataValueField = "codusuario";
            Ddlusuarios.DataBind();
            Ddlusuarios.Items.Insert(0, new ListItem("<Seleccione un Item>", "-1"));
            Ddlusuarios.Enabled = false;
        }
        // Colocar por defecto el usuario
        if (Session["usuario"] != null)
        {
            usu = (Usuario)Session["usuario"];
            usu.estado = 1;
            try
            {
                if (usu.nombre != null && usu.nombre != "")
                    Ddlusuarios.SelectedValue = usu.codusuario.ToString();
            }
            catch
            {
                //Ignore
            }
        }
    }

    private void TraerResultadosLista()
    {
        if (ListaSolicitada == null)
            return;
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
    }

    /// <summary>
    ///  Deshabilita los campos una vez grabados
    /// </summary>
    private void deshabilitaText()
    {
        txtMonto.Enabled = false;
        txtPlazo.Enabled = false;
        txtCuota.Enabled = false;
        ddlTipoCredito.Enabled = false;
        ddlPeriodicidad.Enabled = false;
        txtPeriodo.Enabled = false;
        ddlMedio.Enabled = false;
        txtOtro.Enabled = false;
        txtCliente.Enabled = false;
    }



    protected void btnAgregarReferencia_Click(object sender, EventArgs e)
    {
        List<Referncias> lstReferencia = RecorresGrillaReferencias();

        lstReferencia.Insert(0, new Referncias() { tiporeferencia = 1, cod_persona_quien_referencia = _codPersona });
        var idSeleccionado = lstReferencia.Select(x => x.cod_persona_quien_referencia).ToArray();

        gvReferencias.DataSource = lstReferencia;
        gvReferencias.DataBind();

        LlenarDDLQuienReferenciaGVReferencias(((List<Persona1>)Session[Usuario.codusuario + "Codeudores"]), idSeleccionado);
    }


    protected void gvReferencia_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        List<Referncias> lstReferencia = RecorresGrillaReferencias();

        lstReferencia.RemoveAt(Convert.ToInt32(e.CommandArgument));
        var idSeleccionado = lstReferencia.Select(x => x.cod_persona_quien_referencia).ToArray();

        gvReferencias.DataSource = lstReferencia;
        gvReferencias.DataBind();

        LlenarDDLQuienReferenciaGVReferencias(((List<Persona1>)Session[Usuario.codusuario + "Codeudores"]), idSeleccionado);
    }

    protected void ddlTipoReferencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        var ddlTipoReferenciaEvent = sender as DropDownListGrid;
        int rowIndex = Convert.ToInt32(ddlTipoReferenciaEvent.CommandArgument);

        var ddlParentesco = gvReferencias.Rows[rowIndex].FindControl("ddlParentesco") as DropDownList;

        var selectedValue = Convert.ToInt32(ddlTipoReferenciaEvent.SelectedValue);

        if (ddlTipoReferenciaEvent != null && selectedValue != (int)TipoReferencia.Familiar)
        {
            ddlParentesco.SelectedValue = "0";
            ddlParentesco.Enabled = false;
        }
        else
        {
            ddlParentesco.Enabled = true;
        }
    }


    private List<Referncias> RecorresGrillaReferencias()
    {
        List<Referncias> lstReferencia = new List<Referncias>();

        foreach (GridViewRow gFila in gvReferencias.Rows)
        {
            Referncias referencia = new Referncias()
            {
                cod_persona_quien_referencia = Convert.ToInt64(((DropDownList)gFila.FindControl("ddlQuienReferencia")).SelectedValue),
                tiporeferencia = Convert.ToInt64(((DropDownList)gFila.FindControl("ddlTipoReferencia")).SelectedValue),
                nombres = ((TextBox)gFila.FindControl("txtNombres")).Text,
                codparentesco = Convert.ToInt64(((DropDownList)gFila.FindControl("ddlParentesco")).SelectedValue),
                direccion = ((TextBox)gFila.FindControl("txtDireccion")).Text,
                telefono = ((TextBox)gFila.FindControl("txtTelefono")).Text,
                teloficina = ((TextBox)gFila.FindControl("txtTelOficina")).Text,
                celular = ((TextBox)gFila.FindControl("txtCelular")).Text
            };

            lstReferencia.Add(referencia);
        }

        return lstReferencia;
    }


    // Es necesario este evento vacio para que pueda borrar la Row
    protected void gvReferencias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected List<Referncias> ListarParentesco()
    {
        RefernciasService lineasServicio = new RefernciasService();
        List<Referncias> lstAtributos = lineasServicio.ListasDesplegables("Parentesco", (Usuario)Session["Usuario"]);

        return lstAtributos;
    }

    protected void InicializarReferencias()
    {
        Referncias[] lstAtributos = new Referncias[4]
        {
            new Referncias() { tiporeferencia = 1},
            new Referncias() { tiporeferencia = 1},
            new Referncias() { tiporeferencia = 1},
            new Referncias() { tiporeferencia = 1},
        };

        gvReferencias.DataSource = lstAtributos;
        gvReferencias.DataBind();
    }



    /// <summary>
    /// Guardar información de la solicitud de crédito
    /// </summary>
    private void Guardar()
    {
        lblMensajeValidacion.Text = "";
        string NumeroSolicitud = "";
        if (Session["NumeroSolicitud"] != null)
            NumeroSolicitud = Session["NumeroSolicitud"].ToString();
        string Cod_persona = Session["Cod_persona"].ToString();

        Int64 tipoempresa = 0;
        Usuario usuap = (Usuario)Session["usuario"];
        tipoempresa = Convert.ToInt64(usuap.tipo);

        if (NumeroSolicitud == "") // Crea el numero de solicitud si no ha sido creado
        {
            if (Cod_persona != "")
            {
                DatosSolicitud datosSolicitud = new DatosSolicitud();
                try
                {
                    // Validar monto de los créditos a recoger, aqui dentro del metodo soltamos un mensaje si hay error
                    if (!ValidarMontoRecoger())
                    {
                        return;
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////
                    // Determinando datos de la solicitud
                    /////////////////////////////////////////////////////////////////////////////////////////////
                    datosSolicitud.numerosolicitud = 0;
                    datosSolicitud.fechasolicitud = DateTime.Now;
                    datosSolicitud.cod_cliente = Convert.ToString(Session["Cod_persona"].ToString());
                    datosSolicitud.cod_persona = Convert.ToInt64(Session["Cod_persona"].ToString());
                    datosSolicitud.montosolicitado = txtMonto.Text != "" ? Convert.ToInt64(txtMonto.Text.Trim().Replace(".", "")) : 0;
                    datosSolicitud.plazosolicitado = txtPlazo.Text != "" ? Convert.ToInt64(txtPlazo.Text.Trim().Replace(".", "")) : 0;
                    datosSolicitud.cuotasolicitada = txtCuota.Text != "" ? Convert.ToInt64(txtCuota.Text.Trim().Replace(".", "")) : 0;
                    datosSolicitud.tipocrdito = Convert.ToString(ddlTipoCredito.SelectedValue);
                    if (ddlPeriodicidad.Text != "") datosSolicitud.periodicidad = Convert.ToInt64(ddlPeriodicidad.SelectedValue);
                    if (ddlMedio.SelectedValue != null)
                        if (ddlMedio.SelectedValue != "") datosSolicitud.medio = Convert.ToInt64(ddlMedio.SelectedValue);
                    datosSolicitud.otro = txtOtro.Text.Trim();
                    datosSolicitud.concepto = txtConcepto.Text.Trim();
                    if (ddlDestino.SelectedValue != null)
                        datosSolicitud.destino = Convert.ToInt64(ddlDestino.SelectedValue);

                    datosSolicitud.forma_pago = Convert.ToInt64(ddlFormaPago.SelectedValue);
                    if (Checkgarantia_real.Checked == true) datosSolicitud.garantia = 1;
                    if (Checkgarantia_comunitaria.Checked == true) datosSolicitud.garantia_comunitaria = 1;
                    if (Checkgarantia_comunitaria.Checked == true)
                    {
                        if (txt_ValorGaran.Text != "")
                        {
                            datosSolicitud.Valor_fondo_garantia = Convert.ToDecimal(txt_ValorGaran.Text);
                        }
                    }
                    if (Checkpoliza.Checked == true) datosSolicitud.poliza = 1;
                    datosSolicitud.tipo_liquidacion = Convert.ToInt64(ddlTipoLiquidacion.SelectedValue);
                    datosSolicitud.forma_pago = Convert.ToInt64(ddlFormaPago.SelectedValue);
                    if (datosSolicitud.forma_pago == 2)
                        datosSolicitud.empresa_recaudo = Convert.ToInt64(ddlEmpresa.SelectedValue);
                    else
                        datosSolicitud.empresa_recaudo = null;

                    Xpinn.FabricaCreditos.Services.TipoLiquidacionService TipoLiquidacionServicio = new Xpinn.FabricaCreditos.Services.TipoLiquidacionService();
                    Xpinn.FabricaCreditos.Entities.TipoLiquidacion vTipoLiquidacion = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
                    vTipoLiquidacion = TipoLiquidacionServicio.ConsultarTipoLiquidacion(Convert.ToInt64(datosSolicitud.tipo_liquidacion), (Usuario)Session["usuario"]);

                    if (vTipoLiquidacion.tipo_cuota == 1)
                    {
                        datosSolicitud.fecha_primer_pago = Convert.ToDateTime(txtfechapripago.Text);
                    }

                    // Determinando datos de la oficina y el usuario
                    Usuario user = (Usuario)Session["usuario"];
                    datosSolicitud.cod_oficina = user.cod_oficina;

                    string ddlusuarios = ConfigurationManager.AppSettings["ddlusuarios"].ToString();
                    if (ddlusuarios == "1")
                    {
                        datosSolicitud.cod_usuario = Convert.ToInt64(Ddlusuarios.SelectedValue);
                    }
                    else
                    {
                        datosSolicitud.cod_usuario = user.codusuario;
                    }

                    // Registrando datos del proveedor para la orden
                    if (ctlBusquedaProveedor.VisibleCtl == true && ctlBusquedaProveedor.CheckedOrd == true && ctlBusquedaProveedor.TextIdentif.Trim() != "")
                    {
                        ctlBusquedaProveedor.cargar = "1";
                        ctlBusquedaProveedor.ActualizarGridPersonas();
                        datosSolicitud.identificacionprov = Convert.ToString(ctlBusquedaProveedor.TextIdentif);
                        datosSolicitud.nombreprov = ctlBusquedaProveedor.TextNomProv;
                    }

                    // Validar los datos de la solicitud
                    string sError = "";
                    datosSolicitud = DatosSolicitudServicio.ValidarSolicitud(datosSolicitud, (Usuario)Session["usuario"], ref sError);
                    if (sError.Trim() != "")
                    {
                        if (sError.Contains("ORA-20101"))
                        {
                            lblMensajeValidacion.Text = "No se pudieron validar datos de la solicitud. " + sError.Substring(18);
                        }
                        else if (sError.Contains("ORA-"))
                        {
                            lblMensajeValidacion.Text = sError;
                        }
                        else
                        {
                            lblMensajeValidacion.Text = "No se pudieron validar datos de la solicitud. Error:" + sError;
                        }
                        lblMensajeValidacion.Text = "ERROR" + lblMensajeValidacion.Text;
                        return;
                    }
                    if (datosSolicitud.mensaje.Trim() != "")
                    {
                        lblMensajeValidacion.Text = datosSolicitud.mensaje;
                        return;
                    }

                    TipoFormaDesembolso formaDesembolso = DropDownFormaDesembolso.SelectedValue.ToEnum<TipoFormaDesembolso>();

                    datosSolicitud.forma_desembolso = DropDownFormaDesembolso.SelectedValue;

                    if (formaDesembolso == TipoFormaDesembolso.Transferencia)
                    {
                        datosSolicitud.tipo_cuenta = ddlTipo_cuenta.SelectedValue;
                        datosSolicitud.cod_banco = Convert.ToInt32(DropDownEntidad.SelectedValue);
                        //datosSolicitud.numero_cuenta = ddlNumeroCuenta.SelectedValue;
                        datosSolicitud.numero_cuenta = txtNumeroCuentaBanco.Text;
                    }
                    else if (formaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                    {
                        datosSolicitud.numero_cuenta = ddlCuentaAhorroVista.SelectedValue;
                    }

                    datosSolicitud.Afiancol = CkcAfiancol.Checked;

                    /////////////////////////////////////////////////////////////////////////////////////////////
                    // Grabar datos de la solicitud
                    /////////////////////////////////////////////////////////////////////////////////////////////
                    datosSolicitud = DatosSolicitudServicio.CrearSolicitud(datosSolicitud, (Usuario)Session["usuario"]);
                    if (datosSolicitud.numerosolicitud == 0)
                    {
                        lblMensajeValidacion.Text = "Ocurrio un error al determinar el numero de Solicitud";
                        VerError("Ocurrio un error al determinar el numero de Solicitud");
                        return;
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////
                    // Guardar datos de codeudores
                    /////////////////////////////////////////////////////////////////////////////////////////////
                    #region codeudores
                    List<codeudores> lstCodeudores = new List<codeudores>();
                    int cont = 0;
                    string orden = string.Empty;
                    foreach (GridViewRow rFila in gvListaCodeudores.Rows)
                    {
                        Label codPersona = (Label)rFila.FindControl("lblCodPersona");
                        string COD_CODEUDOR = codPersona.Text.Replace("&nbsp", "");
                        Label lblOrdeRow = (Label)rFila.FindControl("lblOrdenRow");
                        orden = lblOrdeRow.Text.ToString().Replace("&nbsp", "");
                        if (COD_CODEUDOR != "0" && COD_CODEUDOR != "")
                        {
                            cont++;
                            Label lblidentificacion = (Label)rFila.FindControl("lblidentificacion");
                            if (string.IsNullOrEmpty(lblidentificacion.Text))
                            {
                                VerError("Error en la fila " + rFila.RowIndex);
                                return;
                            }

                            Xpinn.FabricaCreditos.Entities.codeudores vCodeudores = new Xpinn.FabricaCreditos.Entities.codeudores();

                            vCodeudores.codpersona = Convert.ToInt64(COD_CODEUDOR);
                            vCodeudores.identificacion = lblidentificacion.Text;
                            vCodeudores.numero_solicitud = Convert.ToInt64(datosSolicitud.numerosolicitud.ToString());
                            Int64 num_Radica = 0;
                            if (Session["Numero_Radicacion"] != null)
                                num_Radica = Convert.ToInt64(Session["Numero_Radicacion"].ToString());
                            vCodeudores.numero_radicacion = num_Radica;

                            // Validar datos del codeudor
                            sError = "";
                            VerError("");
                            CodeudorServicio.ValidarCodeudor(vCodeudores, (Usuario)Session["usuario"], ref sError);
                            if (sError.Trim() != "")
                            {
                                VerError(sError.Substring(0, sError.IndexOf("ORA-")));
                                lblMensajeValidacion.Text = sError;
                                return;
                            }

                            vCodeudores.idcodeud = 0;
                            vCodeudores.tipo_codeudor = "C";
                            vCodeudores.parentesco = 0;
                            vCodeudores.opinion = "B";
                            vCodeudores.responsabilidad = null;
                            vCodeudores.orden = Convert.ToInt32(orden);
                            lstCodeudores.Add(vCodeudores);
                        }
                    }
                    CodeudorServicio.Crearcodeudores(lstCodeudores, (Usuario)Session["usuario"]);
                    #endregion

                    /////////////////////////////////////////////////////////////////////////////////////////////
                    // Guardar datos de referencias
                    /////////////////////////////////////////////////////////////////////////////////////////////
                    #region referencias
                    string error = string.Empty;
                    List<Referncias> lstReferencia = LlenarListaDeGVReferencias(datosSolicitud.numerosolicitud, out error);
                    // Si hay un error muestro y retorno
                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        VerError(error);
                        return;
                    }
                    RefernciasService referenciaService = new RefernciasService();
                    foreach (var referencia in lstReferencia)
                    {
                        referenciaService.CrearReferncias(referencia, (Usuario)Session["usuario"]);
                    }
                    #endregion

                    /////////////////////////////////////////////////////////////////////////////////////////////
                    // Guardar datos de créditos recogidos
                    /////////////////////////////////////////////////////////////////////////////////////////////
                    #region crèditos y avances recogidos
                    SolicitudCreditosRecogidos vSolicitudCreditosRecogidos = new SolicitudCreditosRecogidos();
                    DateTime fechaRecoger = Convert.ToDateTime(lblFecha.Text.Trim());
                    long numeroSolicitud = Convert.ToInt64(datosSolicitud.numerosolicitud.ToString().Trim());

                    foreach (GridViewRow row in gvListaSolicitudCreditosRecogidos.Rows)
                    {
                        if (((CheckBoxGrid)row.Cells[8].FindControl("chkRecoger")).Checked)
                        {
                            vSolicitudCreditosRecogidos.idsolicitudrecoge = 0;
                            vSolicitudCreditosRecogidos.numerosolicitud = numeroSolicitud;
                            vSolicitudCreditosRecogidos.numero_recoge = Convert.ToInt64(row.Cells[1].Text.Trim());
                            vSolicitudCreditosRecogidos.fecharecoge = fechaRecoger;
                            vSolicitudCreditosRecogidos.fechapago = DateTime.Now;
                            vSolicitudCreditosRecogidos.saldocapital = Convert.ToInt64(row.Cells[4].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.saldointcorr = Convert.ToInt64(row.Cells[5].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.saldointmora = Convert.ToInt64(row.Cells[6].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.saldootros = Convert.ToInt64(row.Cells[7].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.saldomipyme = Convert.ToInt64(row.Cells[8].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.saldoivamipyme = Convert.ToInt64(row.Cells[9].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.valorrecoge = Convert.ToInt64(row.Cells[10].Text.Trim().Replace(gSeparadorMiles, ""));
                            vSolicitudCreditosRecogidos.valor_nominas = Convert.ToInt64(row.Cells[14].Text.Trim().Replace(gSeparadorMiles, ""));

                            vSolicitudCreditosRecogidos = SolicitudCreditosRecogidosServicio.CrearSolicitudCreditosRecogidos(vSolicitudCreditosRecogidos, (Usuario)Session["usuario"]);
                        }
                    }


                    SolicitudRecogidoAvance vSolicitudCreditosRecogidosAvances = new SolicitudRecogidoAvance();
                    foreach (GridViewRow rFila in gvAvances.Rows)
                    {
                        CheckBoxGrid chkAvanceeRow = (CheckBoxGrid)rFila.FindControl("chkAvance");
                        if (chkAvanceeRow.Checked)
                        {
                            vSolicitudCreditosRecogidosAvances.Radicado = Convert.ToInt64(Session["NumProducto"].ToString());
                            vSolicitudCreditosRecogidosAvances.ValorTotal = Convert.ToInt64(rFila.Cells[8].Text);
                            vSolicitudCreditosRecogidosAvances.Intereses = Convert.ToInt64(rFila.Cells[7].Text);
                            vSolicitudCreditosRecogidosAvances.SaldoAvance = Convert.ToInt64(rFila.Cells[6].Text);
                            vSolicitudCreditosRecogidosAvances.FechaDesembolsi = fechaRecoger;
                            vSolicitudCreditosRecogidosAvances.NumAvance = Convert.ToInt64(rFila.Cells[1].Text);

                            vSolicitudCreditosRecogidosAvances = SolicitudCreditosRecogidosServicio.CrearSolicitudCreditosRecogidosAvances(vSolicitudCreditosRecogidosAvances, (Usuario)Session["usuario"]);
                        }
                    }
                    #endregion

                    //Guarda Cuotas Extras de la solicitud
                    var numeroradi = datosSolicitud.numerosolicitud.ToString();
                    CuotasExtras.GuardarCuotas(Convert.ToString(numeroradi), -1);

                    // Mostrando informaciòn del nùmero de solicitud grabada y generando el crédito
                    #region liquidacion
                    lblNumero.Text = datosSolicitud.numerosolicitud.ToString();
                    lblFecha.Text = DateTime.Now.ToString();
                    deshabilitaText();

                    lblMensaje.Text = "Su solicitud de credito se ha registrado con el número " + lblNumero.Text;
                    Session["NumeroSolicitud"] = lblNumero.Text;
                    if (tipoempresa == 2)
                    {
                        CreditoPlan credito = new CreditoPlan();
                        credito.NumeroSolicitud = Convert.ToInt64(lblNumero.Text.Trim());

                        credito = creditoPlanServicio.Liquidar(credito, (Usuario)Session["Usuario"]);
                        String radicacion = credito.Numero_Radicacion.ToString();
                        Session["Numero_Radicacion"] = credito.Numero_Radicacion.ToString();

                        //Agregado para Enviar el valor de la garantía del atributo #14
                        if (Checkgarantia_comunitaria.Checked == true)
                        {
                            if (txt_ValorGaran.Text != "" && Convert.ToDecimal(txt_ValorGaran.Text) > 0)
                            {
                                LineasCredito valoresgarantias = new LineasCredito();
                                Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
                                // Determinar valores de la linea para el atributo 14 fondo de  garantias 
                                valoresgarantias = LineaCreditoServicio.ConsultarDeducciones(ddlTipoCredito.SelectedValue.ToString(), 14, (Usuario)Session["Usuario"]);

                                CreditoSolicitadoService servicedescuentogarantia = new CreditoSolicitadoService();
                                DescuentosCredito garantia = new DescuentosCredito();

                                garantia.numero_radicacion = Convert.ToInt64(credito.Numero_Radicacion.ToString());
                                garantia.cod_atr = Convert.ToInt32(valoresgarantias.cod_atr);
                                garantia.tipo_liquidacion = Convert.ToInt32(valoresgarantias.tipo_liquidacion);
                                garantia.val_atr = Convert.ToDecimal(txt_ValorGaran.Text);
                                garantia.numero_cuotas = valoresgarantias.numero_cuotas1;
                                garantia.forma_descuento = Convert.ToInt32(valoresgarantias.Forma_descuento);
                                garantia.tipo_impuesto = Convert.ToInt32(valoresgarantias.tipoimpuesto);
                                garantia.tipo_descuento = Convert.ToInt32(valoresgarantias.tipo_descuento);
                                garantia = servicedescuentogarantia.modificardeduccionesCredito(garantia, (Usuario)Session["Usuario"]);
                            }
                        }
                    }
                    #endregion

                    /////////////////////////////////////////////////////////////////////////////////////////////
                    // Guardar datos de servicios recogidos
                    /////////////////////////////////////////////////////////////////////////////////////////////
                    #region servicios recogidos
                    SolicitudCredServRecogidosService serviciosRecogidos = new SolicitudCredServRecogidosService();
                    SolicitudCredServRecogidos servicios = new SolicitudCredServRecogidos();
                    long numeroRadicacion = Convert.ToInt64(Session["Numero_Radicacion"]);

                    foreach (GridViewRow row in gvServicios.Rows)
                    {
                        CheckBoxGrid checkBoxGrid = row.FindControl("chkRecogerServicios") as CheckBoxGrid;
                        if (checkBoxGrid.Checked)
                        {
                            servicios.numeroradicacion = numeroRadicacion;
                            servicios.numeroservicio = Convert.ToInt32(HttpUtility.HtmlDecode(row.Cells[0].Text));
                            servicios.valorrecoger = Convert.ToDecimal(HttpUtility.HtmlDecode(row.Cells[9].Text.Trim().Replace(gSeparadorMiles, "")));
                            servicios.fecharecoger = fechaRecoger;
                            servicios.saldoservicio = Convert.ToDecimal(HttpUtility.HtmlDecode(row.Cells[7].Text.Trim().Replace(gSeparadorMiles, "")));
                            servicios.interessevicio = Convert.ToDecimal(HttpUtility.HtmlDecode(row.Cells[8].Text.Trim().Replace(gSeparadorMiles, "")));

                            serviciosRecogidos.CrearSolicitudCredServRecogidos(servicios, Usuario);
                        }
                    }
                    #endregion

                    #region Interacciones WM


                    // Parametro general para habilitar proceso de WM
                    General parametroHabilitarOperacionesWM = ConsultarParametroGeneral(35);
                    if (parametroHabilitarOperacionesWM != null && parametroHabilitarOperacionesWM.valor.Trim() == "1")
                    {
                        bool operacionWMExitosa = false;

                        try
                        {
                            // Se necesita el codigo de tipo de informacion adicional de persona del barCode, si no existe bam
                            InformacionAdicionalServices informacionAdicionalService = new InformacionAdicionalServices();
                            string codigoTipoInformacionPersonal = ConfigurationManager.AppSettings["CodigoTipoInformacionAdicionalWorkManagement"];

                            // Valido que tenga el codigo de tipo de informacion adicional para el barcode
                            if (!string.IsNullOrWhiteSpace(codigoTipoInformacionPersonal))
                            {
                                // Se necesita el barCode para identificar la persona en el WM
                                string barCodePersona = informacionAdicionalService.ConsultarInformacionPersonalDeUnaPersona(Convert.ToInt64(Session["Cod_persona"]), Convert.ToInt64(codigoTipoInformacionPersonal), Usuario);

                                // Valido que tengo el barcode de la persona
                                if (!string.IsNullOrWhiteSpace(barCodePersona))
                                {
                                    InterfazWorkManagement interfaz = new InterfazWorkManagement(Usuario);

                                    // Consultamos la persona para verificar los datos que estan registrados en el WM y usar esos 
                                    Persona1 personaWM = interfaz.ConsultarInformacionPersona(barCodePersona);

                                    Persona1Service personaService = new Persona1Service();

                                    // Verificar si la persona es asociado
                                    bool esAsociado = personaService.VerificarSiPersonaEsAsociado(Convert.ToInt64(Session["Cod_persona"]), Usuario);
                                    datosSolicitud.esAsociado = esAsociado;

                                    // Verificar si la persona es natural
                                    bool esNatural = personaService.VerificarSiPersonaEsNatural(Convert.ToInt64(Session["Cod_persona"]), Usuario);
                                    datosSolicitud.esPersonaNatural = esNatural;

                                    // Homologar como WM lo pide
                                    if (esNatural)
                                    {
                                        datosSolicitud.tipoDePersona = "Natural";
                                    }
                                    else
                                    {
                                        datosSolicitud.tipoDePersona = "Juridica";
                                    }

                                    // Llenar resto de datos
                                    datosSolicitud.identificacion = personaWM.identificacion;
                                    datosSolicitud.nombre = personaWM.nombre;
                                    datosSolicitud.nombre_ciudad = personaWM.departamento;
                                    datosSolicitud.numeroradicado = Convert.ToInt64(Session["Numero_Radicacion"]);

                                    // Usamos la clasificacion de creditos seleccionada antes y la mandamos
                                    ClasificacionCredito clasificacion = (ClasificacionCredito)Session["TipoCredito"];
                                    datosSolicitud.tipocrdito = clasificacion.ToString();

                                    // Creamos el formulario de creditos
                                    Tuple<bool, string, long?> tupleResultadoFormularioCredito = interfaz.InteractuarRegistroFormulariosSolicitudCredito(datosSolicitud);

                                    // Si la creacion del formulario de creditos fue exitosa prosigo a iniciar el WorkFlow
                                    if (tupleResultadoFormularioCredito.Item1 && !string.IsNullOrWhiteSpace(tupleResultadoFormularioCredito.Item2) && tupleResultadoFormularioCredito.Item3.HasValue)
                                    {
                                        // Se necesita el barCode para identificar el radicado del credito en el WM
                                        string barCodeCredito = tupleResultadoFormularioCredito.Item2;
                                        long workFlowID = tupleResultadoFormularioCredito.Item3.Value;
                                        WorkManagementServices workManagementService = new WorkManagementServices();

                                        // Registro la solicitud del credito junto con el WorkFlowID para llevar la trayectoria en los siguientes pasos
                                        WorkFlowCreditos workFlowCredito = new WorkFlowCreditos
                                        {
                                            numeroradicacion = Convert.ToInt64(Session["Numero_Radicacion"]),
                                            codigopersona = Convert.ToInt64(Session["Cod_persona"]),
                                            workflowid = workFlowID,
                                            barCodeRadicacion = tupleResultadoFormularioCredito.Item2
                                        };

                                        // Creo el workflow en financial
                                        workManagementService.CrearWorkFlowCreditos(workFlowCredito, Usuario);

                                        // Marco como exitosa si fue creado el workflow en el WM y en finacial y el resto de operacion de runtask fueron exitosas
                                        operacionWMExitosa = true;
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }


                    #endregion


                    // Redireccionando a plan de pagos
                    if (tipoempresa == 1)
                    {
                        Response.Redirect("~/Page/FabricaCreditos/Solicitud/CapturaDocumentos/DocumentosAnexos/Lista.aspx", false);
                    }
                    if (tipoempresa == 2)
                    {
                        Response.Redirect("~/Page/FabricaCreditos/Solicitud/PlanPagos/Lista.aspx", false);
                    }

                    if (Session[Usuario.codusuario + "Codeudores"] != null)
                    {
                        Session.Remove(Usuario.codusuario + "Codeudores");
                    }
                }
                catch (Exception ex)
                {
                    VerError(ex.Message);
                }
            }
        }
        else //Si ya creo la solicitud pregunta si requiere garantia
        {
            TablaCodeudores();
        }
    }

    bool ValidarMontoRecoger()
    {
        decimal valorRecoger = ValorMarcadoParaRecoger();
        decimal montoSolicitado = Convert.ToDecimal(txtMonto.Text.Replace(".", "")); ;

        if (valorRecoger > montoSolicitado)
        {
            VerError("El monto total a recoger supera el monto solicitado de [ $ " + montoSolicitado.ToString("n0") + " ].");
            return false;
        }

        return true;
    }


    private List<Referncias> LlenarListaDeGVReferencias(long numero_solicitud, out string error)
    {
        error = string.Empty;
        List<Referncias> lstReferencia = new List<Referncias>();

        foreach (GridViewRow gFila in gvReferencias.Rows)
        {
            Referncias referencia = new Referncias();
            string nombres = ((TextBox)gFila.FindControl("txtNombres")).Text;
            string telefono = ((TextBox)gFila.FindControl("txtTelefono")).Text;
            string tipoReferencia = ((DropDownList)gFila.FindControl("ddlTipoReferencia")).SelectedValue;
            string telefonoOficina = ((TextBox)gFila.FindControl("txtTelOficina")).Text;
            string direccion = ((TextBox)gFila.FindControl("txtDireccion")).Text;
            string celular = ((TextBox)gFila.FindControl("txtCelular")).Text;
            string codparentesco = ((DropDownList)gFila.FindControl("ddlParentesco")).SelectedValue;

            if (!string.IsNullOrWhiteSpace(nombres) || !string.IsNullOrWhiteSpace(telefono) || !string.IsNullOrWhiteSpace(direccion) || !string.IsNullOrWhiteSpace(telefonoOficina) || !string.IsNullOrWhiteSpace(celular))
            {
                if (string.IsNullOrWhiteSpace(nombres))
                {
                    error += "Debe ingresar el nombre en las referencias";
                    return lstReferencia;
                }
                if (string.IsNullOrWhiteSpace(telefono))
                {
                    error += "Debe ingresar el telefono en las referencias";
                    return lstReferencia;
                }
                if (tipoReferencia == "1" && codparentesco == "0")
                {
                    error += "Debe ingresar el tipo de parentesco si la referencia es personal";
                    return lstReferencia;
                }

                referencia.tiporeferencia = Convert.ToInt64(tipoReferencia);
                referencia.nombres = nombres;
                referencia.codparentesco = Convert.ToInt64(codparentesco);
                referencia.direccion = direccion;
                referencia.telefono = telefono;
                referencia.teloficina = telefonoOficina;
                referencia.celular = celular;
                referencia.numero_radicacion = Session["Numero_Radicacion"] != null ? Convert.ToInt64(Session["Numero_Radicacion"]) : 0;
                referencia.cod_persona = Convert.ToInt64(((DropDownList)gFila.FindControl("ddlQuienReferencia")).SelectedValue);
                referencia.numero_solicitud = numero_solicitud;
                referencia.estado = 0;

                lstReferencia.Add(referencia);
            }
        }

        return lstReferencia;
    }


    protected void btnAtr0_Click(object sender, ImageClickEventArgs e)
    {
        Int64 tipoempresa = 0;
        Usuario usuap = (Usuario)Session["usuario"];
        tipoempresa = Convert.ToInt64(usuap.tipo);

        if (tipoempresa == 2)
        {
            Session[DatosClienteServicio.CodigoPrograma + ".NumDoc"] = ((Label)Master.FindControl("lblIdCliente")).Text;
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/DatosCLiente/Lista.aspx");
        }
        if (tipoempresa == 1)
        {
            if (Session["TipoCredito"].ToString() == "C")
                Response.Redirect("~/Page/FabricaCreditos/Solicitud/Referencias/Lista.aspx");
            else
                Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionFinanciera/InformacionFinancieraNegocio/Blance.aspx");
        }
    }


    protected string obtFiltro()
    {
        string tipoCredito = ddlTipoCredito.SelectedValue;
        int cont = gvListaSolicitudCreditosRecogidos.Rows.OfType<GridViewRow>().Where(x => ((CheckBoxGrid)x.FindControl("chkRecoger")).Checked).Count();
        string pConcat = string.Empty;
        if (cont > 0)
        {
            foreach (GridViewRow rFila in gvListaSolicitudCreditosRecogidos.Rows)
            {
                CheckBoxGrid chkRecoger = (CheckBoxGrid)rFila.FindControl("chkRecoger");
                if (chkRecoger != null)
                {
                    if (chkRecoger.Checked)
                    {
                        Int64 pNumero_radicacion = Convert.ToInt64(gvListaSolicitudCreditosRecogidos.DataKeys[rFila.RowIndex].Value.ToString());
                        string pLinea = rFila.Cells[2].Text != "&nbsp;" ? rFila.Cells[2].Text : null;
                        if (pLinea != null)
                        {
                            string[] pCod_linea = pLinea.ToString().Split('-');
                            string Codigo = pCod_linea[0];
                            if (Codigo == tipoCredito)
                            {
                                pConcat += pNumero_radicacion + ",";
                            }
                        }
                    }
                }
            }
        }
        if (!string.IsNullOrEmpty(pConcat))
        {
            if (pConcat.Contains(","))
                pConcat = pConcat.Substring(0, ((pConcat.Length) - 1));
        }
        return string.IsNullOrEmpty(pConcat) ? null : pConcat;
    }

    protected void btnGuardarDatosSolicitud_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        // validando tipo de Destinacion
        if (ddlDestino.SelectedValue.Trim() == "")
        {
            VerError("Debe seleccionar un tipo de Destinación del crédito");
            return;
        }
        // Validando la lìnea
        if (ddlTipoCredito.SelectedValue.Trim() == "")
        {
            VerError("Debe seleccionar la línea de crédito");

            return;
        }
        if (Ddlusuarios.SelectedValue.Trim() == "-1")
        {
            VerError("Debe seleccionar un asesor");
            return;
        }

        if (Checkgarantia_comunitaria.Checked == true)
        {
            if (txt_ValorGaran.Text == "")
            {
                VerError("Debe Ingresar el valor de la garantía");
                return;
            }
        }

        // Validacion de CREDITOS MAXIMOS POR LINEA
        string tipoCredito = ddlTipoCredito.SelectedValue;
        long codigoCliente = Convert.ToInt64(Session["codigocliente.SolicitudCredito"]);
        Usuario usuario = (Usuario)Session["usuario"];

        string pFiltro = obtFiltro();
        if (!ValidarNumCreditosPorLinea(tipoCredito, pFiltro, codigoCliente, usuario))
            return;

        //================================================
        // Validar Codeudores Necesarios para la línea
        //================================================
        LineasCreditoService lineasCredito = new LineasCreditoService();
        long? codeudoresRequeridos = lineasCredito.ConsultarNumeroCodeudoresXLinea(ddlTipoCredito.SelectedValue, usuario);
        codeudoresRequeridos = codeudoresRequeridos == null ? 0 : codeudoresRequeridos;

        // Reviso con LINQ el numero de codeudores registrados que tengan ID DIFERENTE A "0" y " " y devuelvo la cuenta de cuantos hay
        var codeudoresRegistrados = gvListaCodeudores.DataKeys
                                    .OfType<System.Web.UI.WebControls.DataKey>()
                                    .Where(gvFila => gvFila.Value.ToString() != "0" && !string.IsNullOrWhiteSpace(gvFila.Value.ToString()))
                                    .Count();

        /*if (codeudoresRegistrados > codeudoresRequeridos)
        {
            VerError("El número de codeudores supera lo establecido en la línea de crédito.");
            return;
        }*/

        foreach (GridViewRow items in gvListaCodeudores.Rows)
        {
            //Valida que los numeros de orden no esten repetidos
            if (_validacion != null)
                _validaCodeudores = _validacion == ((Label)items.FindControl("lblOrdenRow")).Text;
            _validacion = ((Label)items.FindControl("lblOrdenRow")).Text;
        }

        if (_validaCodeudores)
        {
            VerError("No se ingresó de manera correcta la información de los codeudores, verifique la orden de cada codeudor.");
            return ;
        }

        long codeudor = LineasCreditoServicio.ConsultarLineasCredito(Convert.ToString(ddlTipoCredito.SelectedValue), usuario).numero_codeudores;
        if (!CkcAfiancol.Checked)
        {
            if (codeudor >= 1 && codeudor > gvListaCodeudores.Rows.Count)
            {
                VerError("Para esta linea de credito es necesario " + codeudor + " Codeudores. Ha registrado " + gvListaCodeudores.Rows.Count + " codeudores");
                return ;
            }
        }



        if (ctlBusquedaProveedor.VisibleCtl == true && ctlBusquedaProveedor.CheckedOrd == true)
        {
            Persona1 pData = new Persona1();
            Persona1Service PersonaService = new Persona1Service();
            if (ctlBusquedaProveedor.TextIdentif != "")
            {
                pData.seleccionar = "Identificacion";
                pData.noTraerHuella = 1;
                pData.identificacion = ctlBusquedaProveedor.TextIdentif;
                pData = PersonaService.ConsultarPersona1Param(pData, (Usuario)Session["usuario"]);
                if (pData.nombres == null && pData.apellidos == null || ctlBusquedaProveedor.TextNomProv == "")
                {
                    VerError("Ingrese una identificación valida del Proveedor");
                    return;
                }
            }
            else
            {
                VerError("Ingrese una identificación valida del Proveedor");
                return;
            }
        }

        //================================================
        //VALIDAR EL ENDEUDAMIENTO ACTUAL DE LA PERSONA.
        //================================================
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
                            return;
                        }
                    }
                }
            }
        }
        catch { }


        TipoFormaDesembolso formaDesembolso = DropDownFormaDesembolso.SelectedValue.ToEnum<TipoFormaDesembolso>();
        if (formaDesembolso == TipoFormaDesembolso.Transferencia)
        {
            //if (string.IsNullOrWhiteSpace(ddlTipo_cuenta.SelectedValue) || string.IsNullOrWhiteSpace(DropDownEntidad.SelectedValue) || string.IsNullOrWhiteSpace(ddlNumeroCuenta.SelectedValue))
            if (string.IsNullOrWhiteSpace(ddlTipo_cuenta.SelectedValue) || string.IsNullOrWhiteSpace(DropDownEntidad.SelectedValue) || string.IsNullOrWhiteSpace(txtNumeroCuentaBanco.Text))
            {
                VerError("Datos para la forma de pago por transferencia invalida!.");
                return;
            }
        }
        else if (formaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
        {
            if (string.IsNullOrWhiteSpace(ddlCuentaAhorroVista.SelectedValue))
            {
                VerError("Datos para la forma de pago por Ahorro Vista invalida!.");
                return;
            }
        }

        //================================================
        // Guarda los datos de los datos de la solicitud  
        //================================================
        if (txtMonto.Text != "0" && txtPlazo.Text != "0" && !string.IsNullOrWhiteSpace(txtMonto.Text) && !string.IsNullOrWhiteSpace(txtPlazo.Text))
        {
            //Validar cuotas extras
            decimal totalCoutaExtra = Convert.ToDecimal(Session["TotalCuoExt"].ToString());
            if (totalCoutaExtra > Convert.ToDecimal(txtMonto.Text))
            {
                VerError("El valor total de las Cuotas Extras no debe se mayor al monto solicitado.");
                return;
            }
            // Validar biometria
            String codigoPrograma = DatosClienteServicio.CodigoPrograma;
            string sError = "";
            string Cod_persona = Session["Cod_persona"].ToString();
            if (ctlValidarBiometria.IniciarValidacion(Convert.ToInt32(codigoPrograma), DatosClienteServicio.CodigoPrograma, Convert.ToInt64(Cod_persona), DateTime.Now, ref sError))
            {
                VerError(sError);
                return;
            }

            Guardar();
        }
        else
        {
            VerError("No se puede registrar la solicitud sin monto solicitado y plazo.");
            return;
        }

    }



    protected void gvAvances_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAvances.PageIndex = e.NewPageIndex;
            List<ConsultaAvance> ListaDetalleAvance = new List<ConsultaAvance>();
            ListaDetalleAvance = DetalleProducto.ListarAvances(long.Parse(Session["Producto"].ToString()), (Usuario)Session["Usuario"]);
            if (ListaDetalleAvance.Count > 0)
            {
                gvAvances.DataSource = ListaDetalleAvance;
                gvAvances.DataBind();


            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void ddlTipoCredito_TextChanged(object sender, EventArgs e)
    {
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCreditoServicio = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        // Determinar si la línea es de Garantías Comunitarias
        if (LineaCreditoServicio.LineaEsFondoGarantiasComunitarias(ddlTipoCredito.SelectedValue.ToString(), (Usuario)Session["Usuario"]))
        {
            Checkgarantia_comunitaria.Checked = true;
            txt_ValorGaran.Visible = true;
            lbvalorfondo.Visible = true;
        }
        else
        {
            Checkgarantia_comunitaria.Checked = false;
        }
        // Determinar datos de la línea
        ddlDestino.Items.Clear();
        if (ddlTipoCredito.SelectedValue != "")
        {
            LineasCredito eLinea = new LineasCredito();
            eLinea = LineaCreditoServicio.ConsultarLineasCredito(ddlTipoCredito.SelectedValue.ToString(), (Usuario)Session["Usuario"]);
            ddlTipoLiquidacion.SelectedValue = eLinea.tipo_liquidacion.ToString();
            // Determinar si la línea maneja periodo de gracia
            if (string.Equals(eLinea.maneja_pergracia, "1"))
                txtPeriodo.Enabled = true;
            else
                txtPeriodo.Enabled = false;
            //if (eLinea.cuotas_extras == 1)
            //{
            //    panelCuotasExtras.Visible = true;
            //}
            //else
            //{
            //    panelCuotasExtras.Visible = false;
            //}
            gvListaSolicitudCreditosRecogidos.Enabled = true;
            // Determinar si es orden de servicio
            if (eLinea.orden_servicio == 1)
            {
                txtorden.Text = Convert.ToString(eLinea.orden_servicio);

                ctlBusquedaProveedor.VisibleCtl = true;
                ctlBusquedaProveedor.CheckedOrd = true;

                gvListaSolicitudCreditosRecogidos.Enabled = false;
                foreach (GridViewRow rfila in gvListaSolicitudCreditosRecogidos.Rows)
                {
                    CheckBoxGrid chkrecoger = (CheckBoxGrid)rfila.FindControl("chkRecoger");
                    chkrecoger.Checked = false;
                }
            }
            else
            {
                ctlBusquedaProveedor.VisibleCtl = false;
                ctlBusquedaProveedor.CheckedOrd = false;
            }
            //  


            Xpinn.FabricaCreditos.Services.TipoLiquidacionService TipoLiquidacionServicio = new Xpinn.FabricaCreditos.Services.TipoLiquidacionService();
            Xpinn.FabricaCreditos.Entities.TipoLiquidacion vTipoLiquidacion = new Xpinn.FabricaCreditos.Entities.TipoLiquidacion();
            vTipoLiquidacion = TipoLiquidacionServicio.ConsultarTipoLiquidacion(Convert.ToInt64(eLinea.tipo_liquidacion), (Usuario)Session["usuario"]);


            if (vTipoLiquidacion.tipo_cuota != 1)
            {
                ddlPeriodicidad.Visible = true;
                LblPerio.Visible = true;
                lblFechaPrimerPago.Visible = false;
                txtfechapripago.Visible = false;
            }
            else
            {
                ddlPeriodicidad.Visible = false;
                LblPerio.Visible = false;
                lblFechaPrimerPago.Visible = true;
                txtfechapripago.Visible = true;
            }


            // Calcular el cupo del crédito
            Calcular_Cupo();
            string IdLinea = ddlTipoCredito.SelectedValue;
            poblar.PoblarListaDesplegable2(IdLinea, ddlDestino, (Usuario)Session["usuario"]);
        }
    }

    private void Calcular_Cupo()
    {
        if (ddlTipoCredito.SelectedValue.ToString() == "")
            return;
        Xpinn.FabricaCreditos.Entities.LineasCredito DatosLinea = new Xpinn.FabricaCreditos.Entities.LineasCredito();
        Xpinn.FabricaCreditos.Services.LineasCreditoService LineaCredito = new Xpinn.FabricaCreditos.Services.LineasCreditoService();
        try
        {
            if (!string.Equals(_codPersona, "") && !string.Equals(lblFecha.Text, ""))
                DatosLinea = LineaCredito.Calcular_Cupo(ddlTipoCredito.SelectedValue.ToString(), Convert.ToInt64(_codPersona), Convert.ToDateTime(lblFecha.Text), (Usuario)Session["usuario"]);
            else
                DatosLinea = LineaCredito.Calcular_Cupo(ddlTipoCredito.SelectedValue.ToString(), 0, DateTime.Today, (Usuario)Session["usuario"]);
            txtPlazoMaximo.Text = DatosLinea.Plazo_Maximo.ToString();
            txtMontoMaximo.Text = String.Format("{0:##0}", DatosLinea.Monto_Maximo);
            txtMontoMaximoMostrar.Text = String.Format("{0:N0}", DatosLinea.Monto_Maximo);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    protected void ddlMedio_TextChanged(object sender, EventArgs e)
    {
        if (string.Equals(ddlMedio.SelectedItem.ToString().ToLower(), "otro"))
        {
            txtOtro.Enabled = true;
        }

        else
        {
            txtOtro.Text = "";
            txtOtro.Enabled = false;
        }
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Mètodo para el botòn de ir a adicionar codeudores al crèdito
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSi_Click(object sender, EventArgs e)
    {
        Int64 tipoempresa = 0;
        Usuario usuap = (Usuario)Session["usuario"];
        tipoempresa = Convert.ToInt64(usuap.tipo);
        Session["Retorno"] = "0";
        if (tipoempresa == 2)
        {
            Session["EstadoCodeudor"] = "0";  // Variables de sesion para saber estado civil y codigo del codeudor
            Session["CodCodeudor"] = "0";
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/Codeudor/Lista.aspx");
        }
        if (tipoempresa == 1)
        {

            Session["EstadoCodeudor"] = "0";  // Variables de sesion para saber estado civil y codigo del codeudor
            Session["CodCodeudor"] = "0";
            Response.Redirect("~/Page/FabricaCreditos/Solicitud/InformacionCodeudor/Codeudor/Lista.aspx");
        }

    }


    // ------------------------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Mètodo para ir a la pantalla de polizas
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSiMicro_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Page/FabricaCreditos/PolizasSeguros/Lista.aspx");
    }


    protected void ddlMedio_SelectedIndexChanged(object sender, EventArgs e)
    {
        validarMedio();
    }

    /// <summary>
    /// Mètodo para calcular la edad de la persona
    /// </summary>
    /// <param name="birthDate"></param>
    /// <returns></returns>
    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }

    /// <summary>
    /// Método para validar el tipo de medio por el cual se enteró de la entidad
    /// </summary>
    private void validarMedio()
    {
        txtOtro.Visible = ddlMedio.SelectedValue == "0" ? true : false;
        lblCual.Visible = ddlMedio.SelectedValue == "0" ? true : false;
    }


    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Value == "2")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;
        }
        else
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }
    }



    /// <summary>
    /// Método para mostrar los créditos del deudor para recoger
    /// </summary>
    private void TablaCreditosRecogidos(Int64 Credito)
    {
        try
        {
            CreditoRecoger creditoRecoger = new CreditoRecoger();
            long codigoPersona = Convert.ToInt64(Session["Cod_persona"]);

            // Cargar los créditos a recoger
            creditoRecoger.cod_deudor = codigoPersona;
            creditoRecoger.numero_radicacion = Convert.ToInt64(Credito);
            creditoRecoger.linea_credito = ddlTipoCredito.SelectedValue;

            bool bdejaCuotasPendientes = dejarCuotaPendiente();

            List<CreditoRecoger> lstConsulta = creditoRecogerServicio.ListarCreditoRecogerSolicitud(creditoRecoger, (Usuario)Session["usuario"]);
            foreach (CreditoRecoger variable in lstConsulta)
            {
                variable.valor_total = variable.saldo_capital + variable.interes_mora + variable.interes_corriente + variable.otros + variable.leymipyme + variable.iva_leymipyme;
                if (bdejaCuotasPendientes == false)
                {
                    variable.valor_nominas = 0;
                    variable.cantidad_nominas = 0;
                }
            }

            if (lstConsulta.Count <= 0)
            {
                lstConsulta.Add(new CreditoRecoger());
            }

            gvListaSolicitudCreditosRecogidos.EmptyDataText = emptyQuery;
            gvListaSolicitudCreditosRecogidos.DataSource = lstConsulta;
            gvListaSolicitudCreditosRecogidos.DataBind();

            if (txtorden.Text == "1")
            {
                if (gvListaSolicitudCreditosRecogidos.Rows.Count > 0)
                {
                    lblTotRec.Visible = true;
                    txtVrTotRecoger.Visible = true;
                    lblMontoDesembolso.Visible = true;
                    txtVrDesembolsar.Visible = true;
                }

                gvListaSolicitudCreditosRecogidos.Enabled = false;
                lblMensajeCredRecogidos.Text = "";
                lblMensajeCredRecogidos.Text = "No se pueden recoger créditos porque la línea maneja orden de servicio";
                lblMensajeCredRecogidos.Visible = true;
            }

            else
            { lblMensajeCredRecogidos.Text = ""; lblMensajeCredRecogidos.Visible = true; }

            AprobacionServiciosServices servicioService = new AprobacionServiciosServices();
            string filtro = " and S.COD_PERSONA = " + codigoPersona + " AND S.ESTADO = 'C' AND S.SALDO > 0";
            string pOrden = "fecha_solicitud desc";

            List<Servicio> listaServicios = servicioService.ListarServicios(filtro, pOrden, DateTime.MinValue, Usuario);
            gvServicios.DataSource = listaServicios;
            gvServicios.DataBind();

            if (lstConsulta.Count > 0 || listaServicios.Count > 0)
            {
                lblTotRec.Visible = true;
                txtVrTotRecoger.Visible = true;
                lblMontoDesembolso.Visible = true;
                txtVrDesembolsar.Visible = true;
                lblTotalRegsSolicitudCreditosRecogidos.Visible = true;
                lblTotalRegsSolicitudCreditosRecogidos.Text = "<br/> Registros encontrados: " + (lstConsulta.Count + listaServicios.Count).ToString();
            }
            else
            {
                lblTotRec.Visible = false;
                txtVrTotRecoger.Visible = false;
                lblMontoDesembolso.Visible = false;
                txtVrDesembolsar.Visible = false;
                lblTotalRegsSolicitudCreditosRecogidos.Visible = true;
                lblTotalRegsSolicitudCreditosRecogidos.Text = "No se encontraron créditos/servicios para recoger";
            }

            CalcularTotalRecoge();
            Session.Add(DatosSolicitudServicio.CodigoPrograma + ".consulta", 1);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.CodigoPrograma, "Page_PreInit", ex);
        }
        LineasCreditoService LineaCreditoServicio = new LineasCreditoService();
        LineasCredito eLinea = new LineasCredito();
        if (ddlTipoCredito.SelectedValue.ToString() != "")
        {
            eLinea = LineaCreditoServicio.ConsultarLineasCredito(ddlTipoCredito.SelectedValue.ToString(), (Usuario)Session["Usuario"]);
            ddlTipoLiquidacion.SelectedValue = eLinea.tipo_liquidacion.ToString();

            // Según parametro del WEBCONFIG no marcar los créditos a recoger
            if (GlobalWeb.gMarcarRecogerDesembolso == "1" || eLinea.orden_servicio == 1)
            {
                foreach (GridViewRow row in gvListaSolicitudCreditosRecogidos.Rows)
                {
                    ((CheckBoxGrid)row.Cells[8].FindControl("chkRecoger")).Checked = false;

                }
            }
        }
    }

    protected void chkRecogerServicios_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CalcularTotalRecoge();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void chkRecoger_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            //VALIDACION SI CUMPLE EL MINIMO Y MAXIMO DE REFINANCIACION
            LineasCredito pDatosLinea = new LineasCredito();
            Xpinn.FabricaCreditos.Data.LineasCreditoData vData = new Xpinn.FabricaCreditos.Data.LineasCreditoData();

            CheckBoxGrid chkRecoger = (CheckBoxGrid)sender;
            int nItem = Convert.ToInt32(chkRecoger.CommandArgument);
            Decimal pVrTotal = 0;

            foreach (GridViewRow rFila in gvListaSolicitudCreditosRecogidos.Rows)
            {
                CheckBoxGrid chkRecogerRow = (CheckBoxGrid)rFila.FindControl("chkRecoger");
                if (chkRecogerRow.Checked)
                {
                    pVrTotal += rFila.Cells[10].Text != "&nbsp;" && rFila.Cells[10].Text != "" ? Convert.ToDecimal(rFila.Cells[10].Text.Replace(".", "")) : 0;
                    Decimal pVrNominas = rFila.Cells[14].Text != "&nbsp;" && rFila.Cells[14].Text != "" ? Convert.ToDecimal(rFila.Cells[14].Text.Replace(".", "")) : 0;                    
                    pVrTotal -= pVrNominas;
                }
                if (chkRecoger.Checked)
                {
                    if (rFila.RowIndex == nItem)
                    {
                        if (rFila.Cells[0].Text != "")
                        {
                            List<ConsultaAvance> ListaDetalleAvance = new List<ConsultaAvance>();
                            ListaDetalleAvance = DetalleProducto.ListarAvances(long.Parse(rFila.Cells[0].Text), (Usuario)Session["Usuario"]);
                            if (ListaDetalleAvance.Count > 0)
                            {
                                gvAvances.DataSource = ListaDetalleAvance;
                                gvAvances.DataBind();
                                MpeDetalleAvances.Show();
                                rFila.Cells[10].Text = "0";
                                Session["NumProducto"] = rFila.Cells[0].Text;
                            }
                        }

                        //CAPTURAR EL CODIGO DE LA LINEA
                        string Linea = rFila.Cells[2].Text;
                        string[] sDatos = Linea.ToString().Split('-');
                        string cod_linea = sDatos[0].ToString();
                        if (!string.IsNullOrWhiteSpace(cod_linea))
                        {
                            pDatosLinea = vData.ConsultaLineaCredito(cod_linea, (Usuario)Session["usuario"]);

                            //VARIABLES A VALIDAR
                            decimal minimo = Convert.ToDecimal(pDatosLinea.minimo_refinancia);
                            decimal maximo = Convert.ToDecimal(pDatosLinea.maximo_refinancia);

                            if (pDatosLinea.tipo_refinancia == 0) //SI ES POR RANGO DE SALDO
                            {
                                //RECUPERAR SALDO CAPITAL
                                decimal saldoCapital = 0;
                                if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")
                                    saldoCapital = Convert.ToDecimal(rFila.Cells[4].Text);
                                if (saldoCapital < minimo || saldoCapital > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que el Saldo Capital esta fuera del Rango de Saldo establecido. Línea:" + cod_linea);
                                    RegistrarPostBack();
                                    return;
                                }
                            }
                            else if (pDatosLinea.tipo_refinancia == 2) //SI ES POR % SALDO
                            {
                                //RECUPERAR SALDO CAPITAL/MONTO
                                decimal saldoCapital = 0, monto = 0, valor = 0;
                                if (rFila.Cells[4].Text != "" && rFila.Cells[4].Text != "&nbsp;")
                                    saldoCapital = Convert.ToDecimal(rFila.Cells[4].Text);
                                if (rFila.Cells[3].Text != "" && rFila.Cells[3].Text != "&nbsp;")
                                    monto = Convert.ToDecimal(rFila.Cells[3].Text);
                                valor = saldoCapital / monto;
                                if (valor < minimo || valor > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que el valor calculado esta fuera del Rango de %Saldo establecido. Línea:" + cod_linea);
                                    RegistrarPostBack();
                                    return;
                                }
                            }
                            else if (pDatosLinea.tipo_refinancia == 3) // SI ES POR % CUOTAS PAGAS
                            {
                                //RECUPERAR LAS CUOTAS PAGADAS
                                decimal cuotas = 0;
                                if (rFila.Cells[11].Text != "" && rFila.Cells[11].Text != "&nbsp;")
                                    cuotas = Convert.ToDecimal(rFila.Cells[11].Text);
                                if (cuotas < minimo || cuotas > maximo)
                                {
                                    chkRecoger.Checked = false;
                                    VerError("No puede recoger este credito ya que las cuotas Pagadas estan fuera del Rango cuotas pagadas establecido. Línea:" + cod_linea);
                                    RegistrarPostBack();
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            CalcularTotalRecoge();

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnCloseAct2_Click(object sender, EventArgs e)
    {
        Decimal pVrTotal = 0;
        Decimal pVrTotalInt = 0;
        Decimal pVrTotalCapital = 0;

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

        CalcularTotalRecoge();

        String NomPro = Session["NumProducto"].ToString();
        foreach (GridViewRow rFila2 in gvListaSolicitudCreditosRecogidos.Rows)
        {
            if (NomPro == rFila2.Cells[0].Text)
            {

                pVrTotal = Convert.ToInt64(TxtTotalAvances.Text == "" ? "0" : TxtTotalAvances.Text);
                pVrTotalCapital = Convert.ToInt64(TxtTotalCap.Text == "" ? "0" : TxtTotalCap.Text);
                pVrTotalInt = Convert.ToInt64(TxtTotalInt.Text == "" ? "0" : TxtTotalInt.Text);
                rFila2.Cells[10].Text = pVrTotal.ToString();
                rFila2.Cells[5].Text = pVrTotalInt.ToString();
                rFila2.Cells[4].Text = pVrTotalCapital.ToString();
            }
        }

        MpeDetalleAvances.Hide();
        CalcularTotalRecoge();

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

        CalcularTotalRecoge();
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

        CalcularTotalRecoge();
    }


    protected void txtMonto_eventoCambiar(object sender, EventArgs e)
    {
        LlenarVariables();
        CalcularTotalRecoge();
    }

    protected void CalcularTotalRecoge()
    {
        decimal valorParaRecoger = ValorMarcadoParaRecoger();

        string textoMontoSolicitado = txtMonto.Text.Replace(".", "");
        decimal montoSolicitado = !string.IsNullOrWhiteSpace(textoMontoSolicitado) ? Convert.ToDecimal(textoMontoSolicitado) : 0;
        txtVrTotRecoger.Text = valorParaRecoger.ToString();
        txtVrDesembolsar.Text = (montoSolicitado - valorParaRecoger).ToString();

        if (montoSolicitado != 0 && valorParaRecoger > montoSolicitado)
        {
            VerError("El monto total a recoger supera el monto solicitado de [ $ " + montoSolicitado.ToString("n0") + " ].");
            RegistrarPostBack();
        }
        else if (!string.IsNullOrWhiteSpace(TextoLaberError))
        {
            VerError("");
            RegistrarPostBack();
        }
    }

    decimal ValorMarcadoParaRecoger()
    {
        Decimal montoParaRecoger = 0;
        foreach (GridViewRow rFila in gvListaSolicitudCreditosRecogidos.Rows)
        {
            CheckBoxGrid chkRecogerRow = (CheckBoxGrid)rFila.FindControl("chkRecoger");
            if (chkRecogerRow.Checked)
            {
                montoParaRecoger += rFila.Cells[10].Text != "&nbsp;" && rFila.Cells[10].Text != "" ? Convert.ToDecimal(rFila.Cells[10].Text.Replace(".", "")) : 0;
                decimal valorNominas = rFila.Cells[14].Text != "&nbsp;" && rFila.Cells[14].Text != "" ? Convert.ToDecimal(rFila.Cells[14].Text.Replace(".", "")) : 0;
                montoParaRecoger -= valorNominas;
                if (montoParaRecoger < 0)
                    montoParaRecoger = 0;
            }
        }

        foreach (GridViewRow row in gvServicios.Rows)
        {
            CheckBoxGrid checkBoxGrid = row.FindControl("chkRecogerServicios") as CheckBoxGrid;
            if (checkBoxGrid.Checked)
            {
                string textoValorTotal = HttpUtility.HtmlDecode(row.Cells[9].Text);
                decimal valor = 0;
                Decimal.TryParse(textoValorTotal, out valor);
                montoParaRecoger += valor;
            }
        }

        return montoParaRecoger;
    }

    protected void gvListaSolicitudCreditosRecogidos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CreditoRecoger cRec = new CreditoRecoger();
                cRec = (CreditoRecoger)e.Row.DataItem;
                CheckBoxGrid chkRecoger = new CheckBoxGrid();
                chkRecoger = (CheckBoxGrid)e.Row.Cells[8].FindControl("chkRecoger");
                if (cRec.recoger == true)
                    chkRecoger.Checked = true;




            }

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.CodigoPrograma + "L", "gvLista_RowDataBound", ex);
        }
    }

    /*
    protected void gvListaSolicitudCreditosRecogidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvListaSolicitudCreditosRecogidos.PageIndex = e.NewPageIndex;
            TablaCreditosRecogidos(Convert.ToInt64(Session["Numero_Radicacion"]));
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.CodigoPrograma, "gvListaSolicitudCreditosRecogidos_PageIndexChanging", ex);
        }
    }
    */

    protected void gvListaSolicitudCreditosRecogidos_SelectedIndexChanged(object sender, EventArgs e)
    {
        String id = gvListaSolicitudCreditosRecogidos.DataKeys[gvListaSolicitudCreditosRecogidos.SelectedRow.RowIndex].Value.ToString();
        Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] = id;
        Navegar(Pagina.Detalle);
    }

    protected void gvListaSolicitudCreditosRecogidos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string a = Convert.ToString(e.ToString());
    }

    protected void gvListaSolicitudCreditosRecogidos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        String id = gvListaSolicitudCreditosRecogidos.Rows[e.NewEditIndex].Cells[0].Text;
        Session[SolicitudCreditosRecogidosServicio.CodigoPrograma + ".id"] = id;
    }

    protected void gvListaSolicitudCreditosRecogidos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Int64 id = Convert.ToInt64(e.Keys[0]);
            SolicitudCreditosRecogidosServicio.EliminarSolicitudCreditosRecogidos(id, (Usuario)Session["usuario"]);
            TablaCreditosRecogidos(Convert.ToInt64(Session["Numero_Radicacion"]));
        }
        catch (Xpinn.Util.ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.CodigoPrograma, "gvLista_RowDeleting", ex);
        }
    }

    #region EVENTOS Y METODOS DE LA GRIDVIEW CODEUDORES

    protected void ObtenerSiguienteOrden()
    {
        var maxValue = 0;
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            List<Persona1> lstCodeudor = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];
            maxValue = lstCodeudor.Max(x => x.orden);
        }
        ((TextBox)gvListaCodeudores.FooterRow.FindControl("txtOdenFooter")).Text = (maxValue + 1).ToString();
    }

    private Xpinn.FabricaCreditos.Entities.Persona1 ObtenerValoresCodeudores()
    {
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

        if (Session["Numero_Radicacion"] != null)
            vPersona1.numeroRadicacion = Convert.ToInt64(Session["Numero_Radicacion"].ToString());

        vPersona1.seleccionar = "CD"; //Bandera para ejecuta el select del CODEUDOR

        return vPersona1;
    }


    private void TablaCodeudores()
    {
        try
        {
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            lstConsulta = DatosClienteServicio.ListarPersona1(ObtenerValoresCodeudores(), (Usuario)Session["usuario"]);

            gvListaCodeudores.PageSize = 5;

            if (lstConsulta.Count > 0)
            {
                gvListaCodeudores.Visible = true;
                lblTotalRegsCodeudores.Visible = false;
                lblTotReg.Visible = true;
                lblTotReg.Text = "<br/> Codeudores a registrar : " + lstConsulta.Count.ToString();

                gvListaCodeudores.DataSource = lstConsulta;
                gvListaCodeudores.DataBind();
                Session[Usuario.codusuario + "Codeudores"] = lstConsulta;
            }
            else
            {
                idObjeto = "";
                gvListaCodeudores.Visible = true;
                lblTotReg.Visible = false;
                lblTotalRegsCodeudores.Visible = true;
                InicialCodeudores();
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosSolicitudServicio.CodigoPrograma, "Actualizar", ex);
        }
    }

    protected void InicialCodeudores()
    {
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        Xpinn.FabricaCreditos.Entities.Persona1 eCodeudor = new Xpinn.FabricaCreditos.Entities.Persona1();
        lstConsulta.Add(eCodeudor);
        Session[Usuario.codusuario + "Codeudores"] = lstConsulta;
        gvListaCodeudores.DataSource = lstConsulta;
        gvListaCodeudores.DataBind();
        ObtenerSiguienteOrden();
    }

    protected void gvListaCodeudores_RowEditing(object sender, GridViewEditEventArgs e)
    {
        // GENERAR EDICION
        gvListaCodeudores.EditIndex = e.NewEditIndex;
        string id = gvListaCodeudores.DataKeys[e.NewEditIndex].Value.ToString();
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            gvListaCodeudores.DataSource = Session[Usuario.codusuario + "Codeudores"];
            gvListaCodeudores.DataBind();
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();

        OcultarGridFooter(gvListaCodeudores, false);
    }

    protected void gvListaCodeudores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        // GENERAR REVERSION
        gvListaCodeudores.EditIndex = -1;
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            gvListaCodeudores.DataSource = Session[Usuario.codusuario + "Codeudores"];
            gvListaCodeudores.DataBind();
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();
        OcultarGridFooter(gvListaCodeudores, true);
    }

    protected void gvListaCodeudores_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvListaCodeudores.EditIndex = -1;
        if (Session[Usuario.codusuario + "Codeudores"] != null)
        {
            TextBox txtOrdenRow = (TextBox)gvListaCodeudores.Rows[e.RowIndex].FindControl("txtOrdenRow");
            if (string.IsNullOrEmpty(txtOrdenRow.Text))
            {
                VerError("Ingrese el orden al que pertenece el codeudor.");
                return;
            }
            List<Persona1> lstCodeudores = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];
            lstCodeudores[e.RowIndex].orden = Convert.ToInt32(txtOrdenRow.Text);
            gvListaCodeudores.DataSource = lstCodeudores;
            gvListaCodeudores.DataBind();
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudores;
            ObtenerSiguienteOrden();
        }
        else
            InicialCodeudores();
        OcultarGridFooter(gvListaCodeudores, true);
    }

    protected void gvListaCodeudores_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<Persona1> lstCodeudores = new List<Persona1>();
        lstCodeudores = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];
        if (lstCodeudores.Count >= 1)
        {
            Persona1 eCodeudor = new Persona1();
            int index = Convert.ToInt32(e.RowIndex);
            eCodeudor = lstCodeudores[index];
            if (eCodeudor.cod_persona != 0)
            {
                lstCodeudores.Remove(eCodeudor); //PENDIENTE
                                                 //CodeudorServicio.EliminarcodeudoresCred(eCodeudor.cod_persona, Convert.ToInt64(txtCredito.Text), (Usuario)Session["usuario"]);
            }
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudores;
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
            gvListaCodeudores.DataSource = lstCodeudores;
            gvListaCodeudores.DataBind();
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudores;
            ObtenerSiguienteOrden();
        }
        BorrarReferenciaCodeudorBorradoGVReferencias(lstCodeudores);
    }

    protected void gvListaCodeudores_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        VerError("");
        if (e.CommandName.Equals("AddNew"))
        {
            TextBox txtidentificacion = (TextBox)gvListaCodeudores.FooterRow.FindControl("txtidentificacion");
            TextBox txtOdenFooter = (TextBox)gvListaCodeudores.FooterRow.FindControl("txtOdenFooter");
            if (txtidentificacion.Text.Trim() == "")
            {
                VerError("Ingrese la Identificación del Codeudor a Agregar por favor.");
                return;
            }
            if (string.IsNullOrEmpty(txtOdenFooter.Text))
            {
                VerError("Ingrese el orden del codeudor por favor.");
                return;
            }
            string IdentifSolic = ((Label)Master.FindControl("lblIdCliente")).Text;
            if (IdentifSolic.Trim() == txtidentificacion.Text.Trim())
            {
                VerError("No puede ingresar como codeudor a la persona solicitante.");
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
                                return;
                            }
                        }
                    }
                }
            }
            catch { }

            List<Persona1> lstCodeudor = new List<Persona1>();
            if (Session[Usuario.codusuario + "Codeudores"] != null)
            {
                lstCodeudor = (List<Persona1>)Session[Usuario.codusuario + "Codeudores"];

                if (lstCodeudor.Count == 1)
                {
                    // si no se adicionón ningún codeudor entonces quita el que se creo para inicializar la gridView porque es vacio
                    Persona1 gItem = new Persona1();
                    gItem = lstCodeudor[0];
                    if (gItem.cod_persona == 0)
                        lstCodeudor.Remove(gItem);
                }
            }

            Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
            vcodeudor = CodeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["usuario"]);
            Persona1 gItemNew = new Persona1();
            gItemNew.cod_persona = vcodeudor.codpersona;
            gItemNew.identificacion = vcodeudor.identificacion;
            gItemNew.primer_nombre = vcodeudor.primer_nombre;
            gItemNew.segundo_nombre = vcodeudor.segundo_nombre;
            gItemNew.primer_apellido = vcodeudor.primer_apellido;
            gItemNew.segundo_apellido = vcodeudor.segundo_apellido;
            gItemNew.direccion = vcodeudor.direccion;
            gItemNew.telefono = vcodeudor.telefono;
            gItemNew.orden = Convert.ToInt32(txtOdenFooter.Text);

            // validar que no existe el mismo codeudor en la gridview
            // PENDIENTE VALIDAR
            bool isValid = gvListaCodeudores.Rows.OfType<GridViewRow>().Where(x => ((Label)x.FindControl("lblCodPersona")).Text == gItemNew.cod_persona.ToString()).Any();
            if (!isValid)
                lstCodeudor.Add(gItemNew);

            gvListaCodeudores.DataSource = lstCodeudor;
            gvListaCodeudores.DataBind();
            Session[Usuario.codusuario + "Codeudores"] = lstCodeudor;
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

            LlenarDDLQuienReferenciaGVReferencias(lstCodeudor);
            ObtenerSiguienteOrden();
        }
    }

    private void BorrarReferenciaCodeudorBorradoGVReferencias(List<Persona1> lstCodeudores)
    {
        List<long> lstIDCodeudores = lstCodeudores.Select(x => x.cod_persona).ToList();
        lstIDCodeudores.Add(_codPersona);

        List<Referncias> lstReferencia = RecorresGrillaReferencias();

        lstReferencia = lstReferencia.Where(x => lstIDCodeudores.Contains(x.cod_persona_quien_referencia)).ToList();
        long[] idSeleccionado = lstReferencia.Select(x => x.cod_persona_quien_referencia).ToArray();

        if (lstReferencia.Count == 0)
        {
            lstReferencia.Add(new Referncias());
        }

        gvReferencias.DataSource = lstReferencia;
        gvReferencias.DataBind();

        LlenarDDLQuienReferenciaGVReferencias(lstCodeudores, idSeleccionado);
    }

    private void LlenarDDLQuienReferenciaGVReferencias(List<Persona1> lstCodeudor, long[] idSeleccionado = null)
    {
        int contador = 0;
        var listaABindearDDL = (from codeudor in lstCodeudor
                                where codeudor.cod_persona != 0
                                select codeudor).ToList();

        Persona1 deudor = new Persona1() { primer_nombre = "Solicitante", cod_persona = _codPersona };
        listaABindearDDL.Add(deudor);

        foreach (GridViewRow row in gvReferencias.Rows)
        {
            DropDownList ddlQuienReferencia = row.Cells[1].FindControl("ddlQuienReferencia") as DropDownList;

            if (ddlQuienReferencia != null)
            {
                var valueSeleccionadoEnDDL = ddlQuienReferencia.SelectedValue;

                ddlQuienReferencia.DataSource = listaABindearDDL;
                ddlQuienReferencia.DataTextField = "nombreYApellido";
                ddlQuienReferencia.DataValueField = "cod_persona";
                ddlQuienReferencia.DataBind();

                if (idSeleccionado != null)
                {
                    ddlQuienReferencia.SelectedValue = idSeleccionado[contador].ToString();
                    contador += 1;
                }
                else
                {
                    ddlQuienReferencia.SelectedValue = valueSeleccionadoEnDDL;
                }
            }
            else
            {
                VerError("Ocurrio un error al agregar la referencia del codeudor, LlenarDDLQuienReferenciaGVReferencias");
                return;
            }
        }
    }

    protected void txtidentificacion_TextChanged(object sender, EventArgs e)
    {
        Control ctrl = gvListaCodeudores.FooterRow.FindControl("txtidentificacion");
        if (ctrl != null)
        {
            TextBox txtidentificacion = (TextBox)ctrl;
            if (txtidentificacion.Text != "")
            {
                Xpinn.FabricaCreditos.Services.codeudoresService codeudorServicio = new Xpinn.FabricaCreditos.Services.codeudoresService();
                Xpinn.FabricaCreditos.Entities.codeudores vcodeudor = new Xpinn.FabricaCreditos.Entities.codeudores();
                vcodeudor = codeudorServicio.ConsultarDatosCodeudor(txtidentificacion.Text, (Usuario)Session["usuario"]);
                if (vcodeudor.codpersona != 0)
                {
                    ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).Text = vcodeudor.codpersona.ToString();
                    gvListaCodeudores.FooterRow.Cells[4].Text = vcodeudor.primer_nombre;
                    gvListaCodeudores.FooterRow.Cells[5].Text = vcodeudor.segundo_nombre;
                    gvListaCodeudores.FooterRow.Cells[6].Text = vcodeudor.primer_apellido;
                    gvListaCodeudores.FooterRow.Cells[7].Text = vcodeudor.segundo_apellido;
                    gvListaCodeudores.FooterRow.Cells[8].Text = vcodeudor.direccion;
                    gvListaCodeudores.FooterRow.Cells[9].Text = vcodeudor.telefono;
                }
                else
                {
                    ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).ForeColor = System.Drawing.Color.Red;
                    string pagina = "";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:validar();", true);
                }
            }
            else
            {
                ((Label)gvListaCodeudores.FooterRow.FindControl("lblCodPersonaFooter")).Text = "";
                gvListaCodeudores.FooterRow.Cells[4].Text = "";
                gvListaCodeudores.FooterRow.Cells[5].Text = "";
                gvListaCodeudores.FooterRow.Cells[6].Text = "";
                gvListaCodeudores.FooterRow.Cells[7].Text = "";
                gvListaCodeudores.FooterRow.Cells[8].Text = "";
                gvListaCodeudores.FooterRow.Cells[9].Text = "";
            }
        }
    }

    protected void btnConsultaPersonas_Click(object sender, EventArgs e)
    {
        BusquedaRapida ctlBusquedaPersonas = (BusquedaRapida)gvListaCodeudores.FooterRow.FindControl("ctlBusquedaPersonas");
        ctlBusquedaPersonas.Motrar(true, "txtidentificacion", "");
    }

    #endregion


    protected void DropDownFormaDesembolso_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActivarDesembolso();
    }



    protected void ActivarDesembolso()
    {
        TipoFormaDesembolso formaDesembolso = DropDownFormaDesembolso.SelectedValue.ToEnum<TipoFormaDesembolso>();

        OcultarControlesDesembolso();

        if (formaDesembolso == TipoFormaDesembolso.Cheque)
        {
            pnlCuentasBancarias.Visible = true;
        }
        else if (formaDesembolso == TipoFormaDesembolso.Transferencia)
        {
            lblEntidad.Visible = true;
            lblNumCuenta.Visible = true;
            lblTipoCuenta.Visible = true;
            txtNumeroCuentaBanco.Visible = true;
            DropDownEntidad.Visible = true;
            ddlTipo_cuenta.Visible = true;
            pnlCuentasBancarias.Visible = true;
        }
        else if (formaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
        {
            ddlCuentaAhorroVista.Visible = true;
            lblCuentaAhorroVista.Visible = true;
            pnlCuentaAhorroVista.Visible = true;
        }
    }

    void OcultarControlesDesembolso()
    {
        pnlCuentaAhorroVista.Visible = false;
        pnlCuentasBancarias.Visible = false;
        lblEntidad.Visible = false;
        lblNumCuenta.Visible = false;
        lblTipoCuenta.Visible = false;
        txtNumeroCuentaBanco.Visible = false;
        DropDownEntidad.Visible = false;
        ddlTipo_cuenta.Visible = false;
        ddlCuentaAhorroVista.Visible = false;
        lblCuentaAhorroVista.Visible = false;
    }

    protected void CkcAfiancol_OnCheckedChanged(object sender, EventArgs e)
    {

        var Cod = LineasCreditoServicio.ddlatributo((Usuario)Session["Usuario"])
                .Where(x => x.cod_atr == cod_afiancol).Select(x => x.cod_atr).FirstOrDefault();
        if (Cod == 0)
        {
            lblMensajeValidacion.Text = @"No se encuentra el atributo de AFIANCOL Creado.";
            CkcAfiancol.Checked = false;
        }
    }

    void LimpiarVariablesSesion()
    {
        Session[Usuario.codusuario + "Codeudores"] = null;
        Session["NumProducto"] = null;
        Session["lstDatosSolicitud"] = null;
        Session["TotalCuoExt"] = 0;
        Session["NumeroSolicitud"] = null;
        Session["Monto"] = null;
    }

    void LlenarVariables()
    {
        CuotasExtras.Monto = txtMonto.Text;
        CuotasExtras.Periodicidad = ddlPeriodicidad.SelectedItem.Text;
        CuotasExtras.PlazoTxt = txtPlazo.Text;
    }

    protected void ddlPeriodicidad_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LlenarVariables();
        return;
    }
}