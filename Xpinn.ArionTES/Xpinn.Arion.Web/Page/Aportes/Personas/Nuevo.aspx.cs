using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Drawing.Imaging;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Services;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
//using Subgurim.Controles;
using System.Text.RegularExpressions;
using Xpinn.Asesores.Entities;
using Xpinn.Caja.Entities;
using Xpinn.Caja.Services;
using Xpinn.ActivosFijos.Services;
using Xpinn.ActivosFijos.Entities;
using Xpinn.Comun.Entities;
using System.Configuration;
using System.Globalization;


public partial class Nuevo : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TerceroService DatosClienteServicio = new Xpinn.Contabilidad.Services.TerceroService();
    private Xpinn.Aportes.Services.AfiliacionServices _afiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    private ActividadesServices ActiServices = new ActividadesServices();
    private BeneficiarioService BeneficiarioServicio = new BeneficiarioService();
    private Xpinn.FabricaCreditos.Entities.Georeferencia pGeo = new Xpinn.FabricaCreditos.Entities.Georeferencia();
    private Xpinn.FabricaCreditos.Services.GeoreferenciaService Georeferencia = new Xpinn.FabricaCreditos.Services.GeoreferenciaService();
    private FormatoDocumentoServices DocumentoService = new FormatoDocumentoServices();
    private ImagenesService ImagenSERVICE = new ImagenesService();
    String Operacion;

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[_afiliacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(_afiliacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(_afiliacionServicio.CodigoPrograma, "A");
            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += BtnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            ctlFormatos.eventoClick += btnImpresion_Click;
            //lblempresas.Visible = false;
            //txtsueldo_soli.eventoCambiar += txtsueldoSoli_TextChanged; 

            // validacion tipo de persona
            string Tip_persona = (string)(Session["Tip_persona"]);
            if (Tip_persona == "N") {
                CheckAct.Visible = true;
                txtfechaAct.Visible = true;
                Session.Remove("Tip_persona");
            }
            else
            {
                CheckAct.Visible = false;
                txtfechaAct.Visible = false;
            }

        }
        catch //(Exception ex)
        {
            //BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string id = Page.Request.Params["__EVENTTARGET"];

            txtFechanacimiento_CalendarExtender.DaysModeTitleFormat = FormatoFecha();
            txtFechanacimiento_CalendarExtender.Format = FormatoFecha();
            txtFechanacimiento_CalendarExtender.TodaysDateFormat = FormatoFecha();

            Usuario usuap = (Usuario)Session["usuario"];

            Session["BUSQUEDA"] = "2"; // opcion para filtrar el google Maps por Direccion            
            Session[Usuario.codusuario + "DatosActividad"] = null;
            ViewState[Usuario.codusuario + "DatosBene"] = null;
            Session[Usuario.codusuario + "DatosCuentaBanc"] = null;
            
            if (!Page.IsPostBack)
            {
                DeshabilitarControlesActivarReadOnly();
                ViewState["ListaInfoAdicional"] = null;
                Session[Usuario.codusuario + "Cod_persona_conyuge"] = null;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "obtener_localizacion();", true);
                //rblOpcion.SelectedValue = "0";
                //InicializarGoogleMapsServer(4.60971, -74.08175);
                //mostarpuntomapa(4.60971, -74.08175);

                // Llenar información de los combos o listas desplegables  
                panelPEPS.Visible = false;
                rblTipoVivienda.SelectedValue = "P";
                validarArriendo();
                CargarListas();
                CargarDropdowEstado(); //DROPDOWN estado de acordion afiliacion 
                Calcular_Valor_Afiliacion();

                // Cargar el código de la persona si se paso desde el estado de cuenta
                if (VerificarLlamadoEstadoCuenta())
                {
                    ContentPlaceHolder Content1 = new ContentPlaceHolder();
                    Content1 = (ContentPlaceHolder)this.Master.FindControl("Content1");
                    if (Content1 == null)
                        Content1 = (ContentPlaceHolder)txtIdentificacionE.NamingContainer;
                    if (Content1 != null)
                    {
                        Deshabilitar(Content1);
                        Site toolBar = (Site)Master;
                        toolBar.MostrarGuardar(false);
                    }
                }
                Session["lstParentescos"] = null;
                // Según corresponda si es nuevo o modificación
                if (Session[_afiliacionServicio.CodigoPrograma + ".id"] != null)
                {
                    //if (usuap.codperfil == 1)
                    //{
                    //    txtIdentificacionE.Enabled = true;
                    //}
                    //else
                    //{
                    //    txtIdentificacionE.Enabled = false;
                    //}
                    Session["Operacion"] = "2";
                    idObjeto = Session[_afiliacionServicio.CodigoPrograma + ".id"].ToString();
                    ObtenerDatos(idObjeto);
                    ObtenerDatosConyuge(Convert.ToInt64(idObjeto));
                    ObtenerDatosParentescos(Convert.ToInt64(idObjeto));
                    ObtenerDatosDocumentos(Convert.ToInt64(idObjeto));
                    LlenarGVActivoFijos(idObjeto);
                    Session["IDENTIFICACION"] = txtIdentificacionE.Text;
                    Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
                    if (Session[_afiliacionServicio.CodigoPrograma + ".modificar"].ToString() == "1")
                    {
                        Site toolBar = (Site)Master;
                        toolBar.MostrarGuardar(false);
                        btnBienesActivos.Visible = false;
                        DeshabilitarObjetosPantalla(panelDatos);
                    }
                    else
                    {
                        // inicializo la modal
                        InicializarModal(this, EventArgs.Empty);
                    }
                    txtCod_oficina.Enabled = false;
                    lblInfoBienesActivos.Visible = false;
                    //rblOpcion_SelectedIndexChanged(rblOpcion, null);
                }
                else
                {
                    obtenerControlesAdicionales();
                    PanelConyuge.Enabled = false;
                    txtdirec_cony.Text = "0";
                    Session["Operacion"] = "1";
                    InicializarBeneficiarios();
                    InicializarActividades();
                    InicializarCuentasBan();
                    InicializarEmpresaRecaudo();
                    InicializarBienesActivosFijos();
                    idObjeto = "";
                    Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
                    txtCod_persona.Text = TerceroServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]).ToString();
                    ddlEstadoAfi.SelectedIndex = 1;
                    panelFecha.Enabled = false;
                    txtFechaAfili.Text = System.DateTime.Now.ToString(gFormatoFecha);
                    ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                    ddlparentesco_SelectedIndexChanged(ddlparentesco, null);
                    txtFechaIngreso_TextChanged(txtFechaIngreso, null);
                    rblSexo_SelectedIndexChanged(rblSexo, null);
                    chkAsociado_CheckedChanged(chkAsociado, null);
                    ddlFormaPago.SelectedValue = "2";
                    ddlEmpresa.Visible = true;
                    lblEmpresa.Visible = true;
                    btnBienesActivos.Visible = false;
                    lblInfoBienesActivos.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private void DeshabilitarControlesActivarReadOnly()
    {
        txttotalING_soli.Attributes.Add("readonly", "readonly");
        txttotalING_cony.Attributes.Add("readonly", "readonly");
        txttotalEGR_soli.Attributes.Add("readonly", "readonly");
        txttotalEGR_cony.Attributes.Add("readonly", "readonly");
    }
    private bool VerificarLlamadoEstadoCuenta()
    {
        EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
        if (Session[serviceEstadoCuenta.CodigoPrograma + ".id"] != null)
        {
            Session[_afiliacionServicio.CodigoPrograma + ".id"] = Session[serviceEstadoCuenta.CodigoPrograma + ".id"];
            Session[_afiliacionServicio.CodigoPrograma + ".modificar"] = 1;
            return true;
        }
        return false;
    }



    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        EstadoCuentaService serviceEstadoCuenta = new EstadoCuentaService();
        if (Session[serviceEstadoCuenta.CodigoPrograma + ".id"] != null)
        {
            Session.Remove(serviceEstadoCuenta.CodigoPrograma + ".id");
            Navegar("../../Asesores/EstadoCuenta/Detalle.aspx");
        }
        else
            Navegar("../Afiliaciones/Lista.aspx");

    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            Usuario lusuario = (Usuario)Session["usuario"];
            Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            ActividadesServices ActividadServicio = new ActividadesServices();

            vPersona1.cod_persona = Convert.ToInt64(pIdObjeto);
            Int64 pCod_Persona = vPersona1.cod_persona;

            vPersona1.seleccionar = "Cod_persona";
            vPersona1.soloPersona = 1;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);

            if (vPersona1.nombre != "errordedatos")
            {
                if (vPersona1.cod_persona != Int64.MinValue)
                    txtCod_persona.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());

                if (vPersona1.ubicacion_correspondencia != 0)
                    ddlTipoUbicCorr.SelectedValue = vPersona1.ubicacion_correspondencia.ToString();
                if (!string.IsNullOrEmpty(vPersona1.dirCorrespondencia))
                {
                    try
                    {
                        txtDirCorrespondencia.Text = HttpUtility.HtmlDecode(vPersona1.dirCorrespondencia.ToString().Trim());
                        if (txtDirCorrespondencia.Text == "")
                            txtDirCorrespondencia.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
                    }
                    catch
                    {
                        VerError("La dirección de correspondencia esta errada, no esta en el formato correcto");
                    }
                }
                if (vPersona1.barrioCorrespondencia != Int64.MinValue && vPersona1.barrioCorrespondencia != null)
                {
                    try
                    {
                        ddlBarrioCorrespondencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.barrioCorrespondencia.ToString().Trim());
                    }
                    catch
                    {
                        ddlBarrioCorrespondencia.SelectedValue = ddlBarrioCorrespondencia.SelectedValue;
                    }
                }
                if (!string.IsNullOrEmpty(vPersona1.telCorrespondencia))
                    txtTelCorrespondencia.Text = HttpUtility.HtmlDecode(vPersona1.telCorrespondencia.ToString().Trim());
                if (vPersona1.ciuCorrespondencia != Int64.MinValue && vPersona1.ciuCorrespondencia != null && vPersona1.ciuCorrespondencia != -1)
                    ddlCiuCorrespondencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.ciuCorrespondencia.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.tipo_persona))
                    if (string.Equals(vPersona1.tipo_persona, 'N'))
                        rblTipo_persona.SelectedValue = HttpUtility.HtmlDecode("Natural");
                if (string.Equals(vPersona1.tipo_persona, 'J'))
                    rblTipo_persona.SelectedValue = HttpUtility.HtmlDecode("Jurídica");
                if (!string.IsNullOrEmpty(vPersona1.identificacion))
                {
                    txtIdentificacionE.Text = HttpUtility.HtmlDecode(vPersona1.identificacion.ToString().Trim());
                }
                txtIdentificacionE.Enabled = false;
                if (vPersona1.digito_verificacion != Int64.MinValue)
                    txtDigito_verificacion.Text = HttpUtility.HtmlDecode(vPersona1.digito_verificacion.ToString().Trim());
                else
                    txtDigito_verificacion.Text = "";
                if (vPersona1.tipo_identificacion != Int64.MinValue)
                    ddlTipoE.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_identificacion.ToString().Trim());
                if (vPersona1.fechaexpedicion != DateTime.MinValue && vPersona1.fechaexpedicion != null)
                    txtFechaexpedicion.Text = HttpUtility.HtmlDecode(vPersona1.fechaexpedicion.Value.ToShortDateString());
                else
                    txtFechaexpedicion.Text = "";
                if (vPersona1.codciudadexpedicion != Int64.MinValue && vPersona1.codciudadexpedicion != null && vPersona1.codciudadexpedicion != -1 && vPersona1.codciudadexpedicion != 0)
                    ddlLugarExpedicion.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadexpedicion.ToString());
                if (!string.IsNullOrEmpty(vPersona1.sexo) && !string.Equals(vPersona1.sexo.ToString().Trim(), ""))
                {
                    try
                    {
                        rblSexo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.sexo.ToString().Trim());
                    }
                    catch
                    {
                        rblSexo.SelectedValue = rblSexo.SelectedValue;
                    }
                    rblSexo_SelectedIndexChanged(rblSexo, null);
                }
                if (!string.IsNullOrEmpty(vPersona1.primer_nombre))
                    txtPrimer_nombreE.Text = HttpUtility.HtmlDecode(vPersona1.primer_nombre.ToString().Trim());
                else
                    txtPrimer_nombreE.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.segundo_nombre))
                    txtSegundo_nombreE.Text = HttpUtility.HtmlDecode(vPersona1.segundo_nombre.ToString().Trim());
                else
                    txtSegundo_nombreE.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.primer_apellido))
                    txtPrimer_apellidoE.Text = HttpUtility.HtmlDecode(vPersona1.primer_apellido.ToString().Trim());
                else
                    txtPrimer_apellidoE.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.segundo_apellido))
                    txtSegundo_apellidoE.Text = HttpUtility.HtmlDecode(vPersona1.segundo_apellido.ToString().Trim());
                else
                    txtSegundo_apellidoE.Text = "";
                if (vPersona1.razon_social == null && Session["Negocio"] != null)
                    vPersona1.razon_social = Session["Negocio"].ToString();
                if (vPersona1.fechanacimiento != DateTime.MinValue && vPersona1.fechanacimiento != null)
                {
                    txtFechanacimiento.Text = HttpUtility.HtmlDecode(vPersona1.fechanacimiento.Value.ToShortDateString());
                    txtEdadCliente.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimiento.Text)));
                }
                else
                {
                    txtFechanacimiento.Text = "";

                    txtEdadCliente.Text = "";
                }
                if (vPersona1.codciudadnacimiento != Int64.MinValue && vPersona1.codciudadnacimiento != null && vPersona1.codciudadnacimiento.ToString().Trim() != "" && vPersona1.codciudadnacimiento != 0)
                    ddlLugarNacimiento.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadnacimiento.ToString().Trim());
                if (vPersona1.nacionalidad != Int64.MinValue && vPersona1.nacionalidad != null && vPersona1.nacionalidad.ToString().Trim() != "" && vPersona1.nacionalidad != 0)
                    ddlPais.SelectedValue = HttpUtility.HtmlDecode(vPersona1.nacionalidad.ToString().Trim());
                if (vPersona1.codestadocivil != Int64.MinValue && vPersona1.codestadocivil.ToString().Trim() != "")
                {
                    try
                    {
                        ddlEstadoCivil.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codestadocivil.ToString().Trim());
                        if (ddlEstadoCivil.SelectedValue == "1" || ddlEstadoCivil.SelectedValue == "3")
                            acoInfConyuge.Visible = true;
                    }
                    catch
                    {
                        ddlEstadoCivil.SelectedValue = ddlEstadoCivil.SelectedValue;
                    }
                }

                if (vPersona1.codescolaridad != Int64.MinValue && vPersona1.codescolaridad.ToString().Trim() != "")
                {
                    try
                    {
                        ddlNivelEscolaridad.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codescolaridad.ToString().Trim());
                    }
                    catch
                    {
                        ddlNivelEscolaridad.SelectedValue = ddlNivelEscolaridad.SelectedValue;
                    }
                }

                //if (vPersona1.codactividadStr != "")
                //{
                //    try
                //    {
                //        ddlActividadE.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codactividadStr.ToString().Trim());
                //    }
                //    catch
                //    {
                //        ddlActividadE.SelectedValue = ddlActividadE.SelectedValue;
                //    }
                //}

                //Alertar al usuario de que la persona cumplio la mayoria de edad y requiere actualizar datos
                if (Session["alertaMayoriaEdad"] != null)
                {
                    bool alerta = Convert.ToBoolean(Session["alertaMayoriaEdad"]);
                    if (alerta)
                        VerError("La persona cumplió la mayoria de edad y requiere actualizar datos");
                }

                Label lblCodigo;
                foreach (GridViewRow rFila in gvActividadesCIIU.Rows)
                {
                    lblCodigo = (Label)rFila.FindControl("lbl_codigo");

                    //Identificar la actividad principal
                    if (lblCodigo.Text == vPersona1.codactividadStr)
                    {
                        CheckBoxGrid chkPrincipal = rFila.FindControl("chkPrincipal") as CheckBoxGrid;
                        chkPrincipal.Checked = true;
                        Label lblDescripcion = (Label)rFila.FindControl("lbl_descripcion");
                        txtActividadCIIU.Text = lblDescripcion.Text;
                    }

                    foreach (Xpinn.FabricaCreditos.Entities.Actividades objActividad in vPersona1.lstActEconomicasSecundarias)
                    {
                        CheckBoxGrid chkSeleccionado = rFila.FindControl("chkSeleccionar") as CheckBoxGrid;

                        if (objActividad.codactividad == lblCodigo.Text)
                        {
                            chkSeleccionado.Checked = true;
                            break;
                        }
                    }
                }
                if (vPersona1.ubicacion_residencia != 0)
                    ddlTipoUbic.SelectedValue = vPersona1.ubicacion_residencia.ToString();

                if (!string.IsNullOrEmpty(vPersona1.direccion))
                {
                    try
                    {
                        txtDireccionE.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
                    }
                    catch
                    {
                        VerError("El formato de la dirección no corresponde");
                    }
                }
                else
                {
                    txtDireccionE.Text = "";
                }
                if (vPersona1.barrioResidencia != 0)
                    ddlBarrioResid.SelectedValue = vPersona1.barrioResidencia.ToString();
                if (vPersona1.codciudadresidencia != 0)
                    ddlCiudadResidencia.SelectedValue = vPersona1.codciudadresidencia.ToString();
                if (!string.IsNullOrEmpty(vPersona1.telefono))
                    txtTelefonoE.Text = HttpUtility.HtmlDecode(vPersona1.telefono.ToString().Trim());
                else
                    txtTelefonoE.Text = "";
                if (vPersona1.antiguedadlugar != Int64.MinValue)
                    txtAntiguedadlugar.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugar.ToString().Trim());
                else
                    txtAntiguedadlugar.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.tipovivienda) && !string.Equals(vPersona1.tipovivienda.ToString().Trim(), "0"))
                {
                    if (vPersona1.tipovivienda != "P" && vPersona1.tipovivienda != "A" && vPersona1.tipovivienda != "F")
                        vPersona1.tipovivienda = "P";
                    rblTipoVivienda.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipovivienda.ToString().Trim());
                }
                if (!string.IsNullOrEmpty(vPersona1.arrendador))
                    txtArrendador.Text = HttpUtility.HtmlDecode(vPersona1.arrendador.ToString().Trim());
                else
                    txtArrendador.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.telefonoarrendador))
                    txtTelefonoarrendador.Text = HttpUtility.HtmlDecode(vPersona1.telefonoarrendador.ToString().Trim());
                else
                    txtTelefonoarrendador.Text = "";
                if (vPersona1.ValorArriendo != Int64.MinValue)
                    txtValorArriendo.Text = HttpUtility.HtmlDecode(vPersona1.ValorArriendo.ToString().Trim());
                else
                    txtValorArriendo.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.celular))
                    txtCelular.Text = HttpUtility.HtmlDecode(vPersona1.celular.ToString().Trim());
                else
                    txtCelular.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.email))
                    txtEmail.Text = HttpUtility.HtmlDecode(vPersona1.email.ToString().Trim());
                else
                    txtEmail.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.empresa))
                    txtEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.empresa.ToString().Trim());
                else
                    txtEmpresa.Text = "";
                if (vPersona1.nit_empresa != 0 && vPersona1.nit_empresa != null)
                    txtNitEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.nit_empresa.ToString().Trim());
                else
                    txtNitEmpresa.Text = "";
                if (vPersona1.tipo_empresa != 0 && vPersona1.tipo_empresa != null)
                    ddlTipoEmpresa.SelectedValue = vPersona1.tipo_empresa.ToString();
                else
                    ddlTipoEmpresa.SelectedValue = "0";
                if (!string.IsNullOrEmpty(vPersona1.cod_nomina_empleado))
                    txtCodigoEmpleado.Text = HttpUtility.HtmlDecode(vPersona1.cod_nomina_empleado.ToString().Trim());
                else
                    txtCodigoEmpleado.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.telefonoempresa))
                    txtTelefonoempresa.Text = HttpUtility.HtmlDecode(vPersona1.telefonoempresa.ToString().Trim());
                else
                    txtTelefonoempresa.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.direccionempresa))
                {
                    try
                    {
                        txtDireccionEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.direccionempresa.ToString().Trim());
                        if (vPersona1.direccionempresa == "" || vPersona1.direccionempresa == "0")
                            txtDireccionEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.direccion.ToString().Trim());
                    }
                    catch
                    {
                        VerError("El formato de dirección de la empresa no corresponde");
                    }
                }
                else
                    txtDireccionEmpresa.Text = "";
                if (vPersona1.ubicacion_empresa != 0 && vPersona1.ubicacion_empresa != null)
                    ddlTipoUbicEmpresa.SelectedValue = vPersona1.ubicacion_empresa.ToString();
                else
                    ddlTipoUbicEmpresa.SelectedValue = "0";

                if (vPersona1.fecha_ingresoempresa != DateTime.MinValue)
                    txtFechaIngreso.Text = vPersona1.fecha_ingresoempresa.ToShortDateString();

                if (vPersona1.antiguedadlugarempresa != Int64.MinValue)
                    txtAntiguedadlugarEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugarempresa.ToString().Trim());
                else
                    txtAntiguedadlugarEmpresa.Text = "";

                if (vPersona1.idescalafon != 0)
                    ddlescalafon.SelectedValue = vPersona1.idescalafon.ToString();
                else
                    ddlescalafon.SelectedValue = "0";

                txtFechaIngreso_TextChanged(txtFechaIngreso, null);

                if (vPersona1.codcargo != Int64.MinValue)
                    ddlCargo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codcargo.ToString().Trim());
                else
                    ddlCargo.SelectedItem.Value = "0";
                if (vPersona1.codtipocontrato != Int64.MinValue)
                    ddlTipoContrato.Text = HttpUtility.HtmlDecode(vPersona1.codtipocontrato.ToString().Trim());
                else
                    ddlTipoContrato.SelectedItem.Value = "0";

                //Cargar actividad empresa
                foreach (GridViewRow rFila in gvActEmpresa.Rows)
                {
                    lblCodigo = (Label)rFila.FindControl("lbl_codigo");

                    //Identificar la actividad principal
                    if (vPersona1.act_ciiu_empresa != null)
                    {
                        if (lblCodigo.Text == vPersona1.act_ciiu_empresa.ToString())
                        {
                            CheckBoxGrid chkPrincipal = rFila.FindControl("chkPrincipal") as CheckBoxGrid;
                            chkPrincipal.Checked = true;
                            Label lblDescripcion = (Label)rFila.FindControl("lbl_descripcion");
                            txtCIIUEmpresa.Text = lblDescripcion.Text;
                            hfActEmpresa.Value = lblCodigo.Text;
                        }
                    }
                    else
                        break;
                }

                if (vPersona1.cod_asesor != Int64.MinValue)
                    txtCod_asesor.Text = HttpUtility.HtmlDecode(vPersona1.cod_asesor.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.residente) && !string.Equals(vPersona1.residente.ToString().Trim(), "0"))
                    try
                    {
                        rblResidente.Text = HttpUtility.HtmlDecode(vPersona1.residente.ToString().Trim());
                    }
                    catch
                    {
                        VerError("La información de si es residente es incorrecta");
                    }
                if (vPersona1.fecha_residencia != DateTime.MinValue)
                    txtFecha_residencia.Text = HttpUtility.HtmlDecode(vPersona1.fecha_residencia.ToShortDateString());
                else
                    txtFecha_residencia.Text = "";
                if (vPersona1.cod_oficina != Int64.MinValue)
                    txtCod_oficina.SelectedValue = HttpUtility.HtmlDecode(vPersona1.cod_oficina.ToString().Trim());
                else
                    txtCod_oficina.SelectedValue = "";
                if (!string.IsNullOrEmpty(vPersona1.tratamiento))
                    txtTratamiento.Text = HttpUtility.HtmlDecode(vPersona1.tratamiento.ToString().Trim());
                else
                    txtTratamiento.Text = "";
                ddlActividadE0.SelectedValue = vPersona1.ActividadEconomicaEmpresa.ToString();
                if (vPersona1.sector != null)
                {
                    ddlSector.SelectedValue = vPersona1.sector.ToString();
                }
                if (vPersona1.zona != null)
                {
                    ddlZona.SelectedValue = vPersona1.zona.ToString();
                }


                ddlCiu0.SelectedValue = vPersona1.ciudad.ToString();

                ddlparentesco.SelectedValue = vPersona1.relacionEmpleadosEmprender.ToString();
                //Se modificó para registrar los datos en el anexo de PARENTESCOS
                /*ddlparentesco_SelectedIndexChanged(ddlparentesco, null);
                if (txtnomFuncionario.Visible == true)
                    if (vPersona1.nombre_funcionario != "")
                        txtnomFuncionario.Text = vPersona1.nombre_funcionario;*/

                if (vPersona1.parentesco_madminis == 1 || vPersona1.parentesco_madminis == 2)
                    ddlFamiliarAdmin.SelectedValue = vPersona1.parentesco_madminis.ToString();

                if (vPersona1.parentesco_mcontrol == 1 || vPersona1.parentesco_mcontrol == 2)
                    ddlFamiliarControl.SelectedValue = vPersona1.parentesco_mcontrol.ToString();

                if (vPersona1.parentesco_pep == 1 || vPersona1.parentesco_pep == 2)
                    ddlFamiliarPEPS.SelectedValue = vPersona1.parentesco_pep.ToString();

                txtTelCell0.Text = vPersona1.CelularEmpresa;
                if (vPersona1.profecion != "")
                    txtProfecion.Text = vPersona1.profecion;
                if (vPersona1.ocupacionApo != 0)
                {
                    ddlOcupacion.SelectedValue = vPersona1.ocupacionApo.ToString();
                    if (vPersona1.ocupacionApo != 7 && vPersona1.ocupacionApo != 0)
                        acoInformacionLaboral.Visible = true;
                }
                txtEstrato.Text = vPersona1.Estrato.ToString();
                txtPersonasCargo.Text = vPersona1.PersonasAcargo.ToString();

                if (vPersona1.empleado_entidad != null && vPersona1.empleado_entidad != 0)
                    chkEmpleadoEntidad.Checked = true;
                if (vPersona1.mujer_familia != null && vPersona1.mujer_familia != 0)
                    chkMujerCabeFami.Checked = true;
                if (vPersona1.jornada_laboral != null)
                    rblJornadaLaboral.SelectedValue = vPersona1.jornada_laboral.ToString();

                // Mostrar imagenes de la persona
                if (vPersona1.foto != null)
                {
                    try
                    {
                        imgFoto.ImageUrl = Bytes_A_Archivo(pIdObjeto, vPersona1.foto);
                        imgFoto.ImageUrl = string.Format("Handler.ashx?id={0}", vPersona1.idimagen + "&Us=" + lusuario.identificacion + "&Pw=" + lusuario.clave);

                    }
                    catch // (Exception ex)
                    {
                        // VerError("No pudo abrir archivo con imagen de la persona " + ex.Message);
                    }
                }

                InicializarBeneficiarios();
                InicializarActividades();
                InicializarCuentasBan();

                Int64 pCod = Convert.ToInt64(pIdObjeto);

                List<Beneficiario> LstBeneficiario = new List<Beneficiario>();
                List<Actividades> LstActividad = new List<Actividades>();
                List<CuentasBancarias> LstCuentasBanc = new List<CuentasBancarias>();
                List<InformacionAdicional> LstInformacion = new List<InformacionAdicional>();
                List<PersonaEmpresaRecaudo> LstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();

                LstBeneficiario = BeneficiarioServicio.ConsultarBeneficiario(pCod, (Usuario)Session["usuario"]);
                if (LstBeneficiario.Count > 0)
                {
                    if ((LstBeneficiario != null) || (LstBeneficiario.Count != 0))
                    {
                        //ValidarPermisosGrilla(gvBeneficiarios);
                        gvBeneficiarios.DataSource = LstBeneficiario;
                        gvBeneficiarios.DataBind();
                    }
                    ViewState[Usuario.codusuario + "DatosBene"] = LstBeneficiario;
                }

                LstActividad = ActividadServicio.ConsultarActividad(pCod, (Usuario)Session["usuario"]);
                if (LstActividad.Count > 0)
                {
                    if ((LstActividad != null) || (LstActividad.Count != 0))
                    {
                        //ValidarPermisosGrilla(gvActividades);
                        gvActividades.DataSource = LstActividad;
                        gvActividades.DataBind();
                    }
                    Session[Usuario.codusuario + "DatosActividad"] = LstActividad;
                }
                string filtro = "";
                LstCuentasBanc = ActividadServicio.ConsultarCuentasBancarias(pCod, filtro, (Usuario)Session["usuario"]);
                if (LstCuentasBanc.Count > 0)
                {
                    if ((LstCuentasBanc != null) || (LstCuentasBanc.Count != 0))
                    {
                        //ValidarPermisosGrilla(gvCuentasBancarias);
                        gvCuentasBancarias.DataSource = LstCuentasBanc;
                        gvCuentasBancarias.DataBind();
                    }
                    Session[Usuario.codusuario + "DatosCuentaBanc"] = LstCuentasBanc;
                }

                InformacionAdicionalServices infoService = new InformacionAdicionalServices();
                LstInformacion = infoService.ListarPersonaInformacion(pCod, "N", (Usuario)Session["usuario"]);
                if (LstInformacion.Count > 0)
                {
                    gvInfoAdicional.DataSource = LstInformacion;
                    gvInfoAdicional.DataBind();
                }
                ViewState.Add("ListaInfoAdicional", LstInformacion);

                PersonaEmpresaRecaudoServices infoEmpresaRecaudo = new PersonaEmpresaRecaudoServices();
                LstEmpresaRecaudo = infoEmpresaRecaudo.ListarPersonaEmpresaRecaudo(pCod, (Usuario)Session["usuario"]);
                if (LstEmpresaRecaudo.Count > 0)
                {
                    lblempresas.Visible = true;
                    gvEmpresaRecaudo.DataSource = LstEmpresaRecaudo;
                    gvEmpresaRecaudo.DataBind();
                }
                else
                {
                    lblempresas.Visible = false;
                }

                foreach (GridViewRow rFila in gvEmpresaRecaudo.Rows)
                {
                    CheckBoxGrid chkSeleccionar = (CheckBoxGrid)rFila.FindControl("chkSeleccionar");
                    if (chkSeleccionar != null && chkSeleccionar.Checked == true)
                    {
                        chkSeleccionar_CheckedChanged(chkSeleccionar, null);
                    }
                }

                validarArriendo();
                CalcularEdad(1);

                ObtenerDatosInformacion_INGRE_EGRE(pCod_Persona);
                ObtenerDatosGeorefencia(pCod_Persona);
                ObtenerDatosAfiliacion(pCod_Persona);

                if (vPersona1.lstBeneficiarios != null)
                {
                    gvBeneficiarios.DataSource = vPersona1.lstBeneficiarios;
                    gvBeneficiarios.DataBind();
                }


                //RECUPERAR INFORMACIÓN DE TRANSACCIONES Y PRODUCTOS EN EL EXTERIOR
                List<EstadosFinancieros> LstMonedaExt = new List<EstadosFinancieros>();
                List<EstadosFinancieros> LstTransaccionesExt = new List<EstadosFinancieros>();
                List<EstadosFinancieros> LstProductosExt = new List<EstadosFinancieros>();
                LstMonedaExt = EstadosFinancierosServicio.ListarCuentasMonedaExtranjera(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
                LstTransaccionesExt = LstMonedaExt.Where(x => x.tipo_producto == "" || x.tipo_producto == null).ToList();
                LstProductosExt = LstMonedaExt.Where(x => x.tipo_producto != "" && x.tipo_producto != null).ToList();

                if (LstTransaccionesExt.Count > 0)
                {
                    if ((LstTransaccionesExt != null) || (LstTransaccionesExt.Count != 0))
                    {
                        chkMonedaExtranjera.Checked = true;
                        panelMonedaExtranjera.Visible = true;
                        gvMonedaExtranjera.DataSource = LstTransaccionesExt;
                        gvMonedaExtranjera.DataBind();
                    }
                    else
                    {
                        chkMonedaExtranjera.Checked = false;
                        panelMonedaExtranjera.Visible = false;
                    }
                    Session["DatosCuentaExtranjera"] = LstTransaccionesExt;
                }

                if (LstProductosExt.Count > 0)
                {
                    if ((LstProductosExt != null) || (LstProductosExt.Count != 0))
                    {
                        chkTransaccionExterior.Checked = true;
                        pProductosExt.Visible = true;
                        gvProductosExterior.DataSource = LstProductosExt;
                        gvProductosExterior.DataBind();
                    }
                    else
                    {
                        chkTransaccionExterior.Checked = false;
                        pProductosExt.Visible = false;
                    }
                    Session["DatosProductosFinExt"] = LstProductosExt;
                }

            }
            else
                VerError("Error de datos");

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }

    }

    protected void ObtenerDatosGeorefencia(Int64 cod_persona)
    {
        pGeo = Georeferencia.ConsultarGeoreferenciaXPERSONA(cod_persona, (Usuario)Session["usuario"]);
        //if (pGeo.codgeoreferencia != 0)
        //    txtCodGeorefencia.Text = Convert.ToString(pGeo.codgeoreferencia);
        //if (pGeo.latitud != "")
        //    txtLatitud.Text = pGeo.latitud;
        //if (pGeo.longitud != "")
        //    txtLongitud.Text = pGeo.longitud;
    }

    #region METODOS DE CONYUGE


    protected void chkConyuge_CheckedChanged(object sender, EventArgs e)
    {
        if (chkConyuge.Checked)
        {
            PanelConyuge.Enabled = true;
        }
        else
        {
            PanelConyuge.Enabled = false;
            if (txtcod_conyuge.Text != "")
            {
                ConyugeService ConService = new ConyugeService();
                ConService.EliminarConyuge(Convert.ToInt64(txtcod_conyuge.Text), (Usuario)Session["usuario"]);
            }
        }
    }


    /// <summary>
    /// EVENTO PARA CONSULTAR DATOS DEL CONYUGE EN CASO SEA UNA PERSONA REGISTRADA LA QUE SE ESTE INGRESANDO
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtIdent_cony_TextChanged(object sender, EventArgs e)
    {
        VerError("");
        Persona1 vPersona1 = new Persona1();
        Persona1Service persona1Servicio = new Persona1Service();

        try
        {
            vPersona1.identificacion = txtIdent_cony.Text;
            vPersona1.seleccionar = "Identificacion";
            vPersona1.soloPersona = 1;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (vPersona1 != null)
            {
                if (vPersona1.nombre != "errordedatos")
                {
                    // OBTENIENDO LOS DATOS DE CONYUGE
                    txtcod_conyuge.Text = vPersona1.cod_persona.ToString();
                    ddlTipo.SelectedValue = vPersona1.tipo_identificacion.ToString();
                    txtnombre1_cony.Text = vPersona1.primer_nombre;
                    txtnombre2_cony.Text = vPersona1.segundo_nombre;
                    txtapellido1_cony.Text = vPersona1.primer_apellido;
                    txtapellido2_cony.Text = vPersona1.segundo_apellido;
                    if (!string.IsNullOrEmpty(vPersona1.sexo))
                        rbsexo_cony.SelectedValue = vPersona1.sexo;
                    txtFechaExp_Cony.Text = vPersona1.fechaexpedicion != null ? Convert.ToDateTime(vPersona1.fechaexpedicion).ToString("dd/MM/yyyy") : null;
                    if (vPersona1.codciudadexpedicion != null && vPersona1.codciudadexpedicion != -1)
                        ddlcuidadExp_Cony.SelectedValue = vPersona1.codciudadexpedicion.ToString();
                    txtfechaNac_Cony.Text = vPersona1.fechanacimiento != null ? Convert.ToDateTime(vPersona1.fechanacimiento).ToString("dd/MM/yyyy") : null;
                    txtEdad_Cony.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtfechaNac_Cony.Text)));
                    if (vPersona1.codciudadnacimiento != null)
                        ddlLugNacimiento_Cony.SelectedValue = vPersona1.codciudadnacimiento.ToString();
                    txtemail_cony.Text = !string.IsNullOrEmpty(vPersona1.email) ? vPersona1.email : null;
                    txtCell_cony.Text = !string.IsNullOrEmpty(vPersona1.celular) ? vPersona1.celular : null;
                    if (vPersona1.Estrato != null)
                        txtEstrato_Cony.Text = vPersona1.Estrato.ToString();
                    txtempresa_cony.Text = !string.IsNullOrEmpty(vPersona1.empresa) ? vPersona1.empresa : null;
                    txtantiguedad_cony.Text = vPersona1.antiguedadlugarempresa.ToString();
                    ddlcontrato_cony.SelectedValue = vPersona1.codtipocontrato.ToString();
                    ddlCargo_cony.SelectedValue = vPersona1.codcargo != 0 ? vPersona1.codcargo.ToString() : null;
                    txtTelefonoEmp_Cony.Text = !string.IsNullOrEmpty(vPersona1.telefonoempresa) ? vPersona1.telefonoempresa : null;
                }
            }

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void ObtenerDatosConyuge(Int64 cod_persona)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        ActividadesServices ActividadServicio = new ActividadesServices();

        vPersona1.cod_persona = cod_persona;

        vPersona1 = persona1Servicio.ConsultarPersona1conyuge(vPersona1, (Usuario)Session["usuario"]);

        if (vPersona1.cod_persona != 0 && vPersona1.identificacion != "")
        {
            chkConyuge.Checked = true;
        }
        else
        {
            chkConyuge.Checked = false;
        }
        chkConyuge_CheckedChanged(chkConyuge, null);

        if (vPersona1.nombre != "errordedatos")
        {

            if (vPersona1.cod_persona != Int64.MinValue)
                txtcod_conyuge.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());

            if (vPersona1.tipo_identificacion != Int64.MinValue)
                ddlTipo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_identificacion.ToString().Trim());

            if (!string.IsNullOrEmpty(vPersona1.identificacion))
            {
                txtIdent_cony.Text = HttpUtility.HtmlDecode(vPersona1.identificacion.ToString().Trim());
            }

            if (!string.IsNullOrEmpty(vPersona1.primer_nombre))
                txtnombre1_cony.Text = HttpUtility.HtmlDecode(vPersona1.primer_nombre.ToString().Trim());
            else
                txtnombre1_cony.Text = "";
            if (!string.IsNullOrEmpty(vPersona1.segundo_nombre))
                txtnombre2_cony.Text = HttpUtility.HtmlDecode(vPersona1.segundo_nombre.ToString().Trim());
            else
                txtnombre2_cony.Text = "";
            if (!string.IsNullOrEmpty(vPersona1.primer_apellido))
                txtapellido1_cony.Text = HttpUtility.HtmlDecode(vPersona1.primer_apellido.ToString().Trim());
            else
                txtapellido1_cony.Text = "";
            if (!string.IsNullOrEmpty(vPersona1.segundo_apellido))
                txtapellido2_cony.Text = HttpUtility.HtmlDecode(vPersona1.segundo_apellido.ToString().Trim());
            else
                txtapellido2_cony.Text = "";

            if (vPersona1.codciudadexpedicion != Int64.MinValue && vPersona1.codciudadexpedicion != null && vPersona1.codciudadexpedicion != -1 && vPersona1.codciudadexpedicion != 0)
                ddlcuidadExp_Cony.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadexpedicion.ToString().Trim());

            if (!string.IsNullOrEmpty(vPersona1.celular))
                txtCell_cony.Text = HttpUtility.HtmlDecode(vPersona1.celular.ToString().Trim());
            else
                txtCell_cony.Text = "";

            if (vPersona1.fechaexpedicion != DateTime.MinValue)
                txtFechaExp_Cony.Text = HttpUtility.HtmlDecode(vPersona1.fechaexpedicion.ToString().Trim());
            else
                txtFechaExp_Cony.Text = "";

            if (vPersona1.Estrato != Int64.MinValue)
                txtEstrato_Cony.Text = HttpUtility.HtmlDecode(vPersona1.Estrato.ToString().Trim());
            else
                txtEstrato_Cony.Text = "";

            if (!string.IsNullOrEmpty(vPersona1.ocupacion))
                ddlOcupacion_Cony.SelectedValue = HttpUtility.HtmlDecode(vPersona1.ocupacion.ToString().Trim());
            else
                ddlOcupacion_Cony.SelectedValue = "0";

            if (vPersona1.codciudadnacimiento != 0 && vPersona1.codciudadnacimiento != null)
                ddlLugNacimiento_Cony.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadnacimiento.ToString().Trim());
            else
                ddlLugNacimiento_Cony.SelectedValue = "0";

            //informacion de la empresa
            if (!string.IsNullOrEmpty(vPersona1.empresa))
                txtempresa_cony.Text = HttpUtility.HtmlDecode(vPersona1.empresa.ToString().Trim());
            else
                txtempresa_cony.Text = "";
            if (vPersona1.antiguedadlugarempresa != Int64.MinValue)
                txtantiguedad_cony.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugarempresa.ToString().Trim());
            else
                txtantiguedad_cony.Text = "";

            if (vPersona1.codtipocontrato != Int64.MinValue)
                ddlcontrato_cony.Text = HttpUtility.HtmlDecode(vPersona1.codtipocontrato.ToString().Trim());
            else
                ddlcontrato_cony.SelectedItem.Value = "0";
            if (vPersona1.codcargo != Int64.MinValue)
                ddlCargo_cony.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codcargo.ToString().Trim());
            else
                ddlCargo_cony.SelectedItem.Value = "0";
            if (!string.IsNullOrEmpty(vPersona1.telefonoempresa))
                txtTelefonoEmp_Cony.Text = HttpUtility.HtmlDecode(vPersona1.telefonoempresa.ToString().Trim());
            else
                txtTelefonoEmp_Cony.Text = "";
            if (!string.IsNullOrEmpty(vPersona1.email))
                txtemail_cony.Text = HttpUtility.HtmlDecode(vPersona1.email.ToString().Trim());
            else
                txtemail_cony.Text = "";
            if (!string.IsNullOrEmpty(vPersona1.sexo) && !string.Equals(vPersona1.sexo.ToString().Trim(), ""))
            {
                try
                {
                    rbsexo_cony.SelectedValue = HttpUtility.HtmlDecode(vPersona1.sexo.ToString().Trim());
                }
                catch
                {
                    rbsexo_cony.SelectedValue = rblSexo.SelectedValue;
                }
            }
            if (vPersona1.fechanacimiento != DateTime.MinValue)
            {
                txtfechaNac_Cony.Text = HttpUtility.HtmlDecode(vPersona1.fechanacimiento.Value.ToShortDateString());
                CalcularEdad(2);
            }
            else
            {
                txtfechaNac_Cony.Text = "";
            }
            if (!string.IsNullOrEmpty(vPersona1.direccionempresa))
            {
                try
                {
                    txtdirec_cony.Text = HttpUtility.HtmlDecode(vPersona1.direccionempresa.ToString().Trim());
                }
                catch
                {
                    VerError("El formato de la dirección no corresponde");
                }
            }
            else
            {
                txtdirec_cony.Text = "0";
            }
        }
    }

    private void GuardarConyuge()
    {
        Xpinn.FabricaCreditos.Entities.Conyuge vConyuge = new Xpinn.FabricaCreditos.Entities.Conyuge();
        Xpinn.FabricaCreditos.Services.ConyugeService ConyugeServicio = new Xpinn.FabricaCreditos.Services.ConyugeService();
        Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();

        vPersona1.identificacion = txtIdent_cony.Text;
        vPersona1.seleccionar = "Identificacion";
        vPersona1.soloPersona = 1;
        vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
        vPersona1.cod_persona = vPersona1.nombre != "errordedatos" ? vPersona1.cod_persona : 0;

        //DATOS BASICOS CONYUGUE
        vPersona1.origen = "Afiliacion-Conyuge";

        vPersona1.primer_nombre = (txtnombre1_cony.Text != "") ? Convert.ToString(txtnombre1_cony.Text.Trim()) : String.Empty;
        vPersona1.segundo_nombre = (txtnombre2_cony.Text != "") ? Convert.ToString(txtnombre2_cony.Text.Trim()) : String.Empty;
        vPersona1.primer_apellido = (txtapellido1_cony.Text != "") ? Convert.ToString(txtapellido1_cony.Text.Trim()) : String.Empty;
        vPersona1.segundo_apellido = (txtapellido2_cony.Text != "") ? Convert.ToString(txtapellido2_cony.Text.Trim()) : String.Empty;

        if (ddlTipo.Text != "") vPersona1.tipo_identificacion = Convert.ToInt64(ddlTipo.SelectedValue);
        vPersona1.identificacion = (txtIdent_cony.Text != "") ? Convert.ToString(txtIdent_cony.Text.Trim()) : String.Empty;
        if (ddlcuidadExp_Cony.Text != "") vPersona1.codciudadexpedicion = Convert.ToInt64(ddlcuidadExp_Cony.SelectedValue);
        vPersona1.celular = (txtCell_cony.Text != "") ? Convert.ToString(txtCell_cony.Text.Trim()) : String.Empty;
        vPersona1.sexo = (rbsexo_cony.Text != "") ? Convert.ToString(rbsexo_cony.SelectedValue) : String.Empty;
        vPersona1.fechaexpedicion = !string.IsNullOrEmpty(txtFechaExp_Cony.Text) ? Convert.ToDateTime(txtFechaExp_Cony.Texto) : DateTime.MinValue;
        vPersona1.Estrato = txtEstrato_Cony.Text != "" ? Convert.ToInt32(txtEstrato_Cony.Text) : 0;
        try
        {
            if (txtfechaNac_Cony.Text != "") vPersona1.fechanacimiento = Convert.ToDateTime(txtfechaNac_Cony.Text.Trim());
        }
        catch
        {
        }
        if (ddlLugNacimiento_Cony.SelectedIndex != -1)
            vPersona1.codciudadnacimiento = Convert.ToInt64(ddlLugNacimiento_Cony.SelectedValue);
        else
            vPersona1.codciudadnacimiento = 0;
        vPersona1.email = (txtemail_cony.Text != "") ? Convert.ToString(txtemail_cony.Text.Trim()) : String.Empty;
        vPersona1.ocupacion = ddlOcupacion_Cony.SelectedValue != "0" ? ddlOcupacion_Cony.SelectedValue : "";

        //DATOS BASICOS EMPRESA
        vPersona1.empresa = (txtempresa_cony.Text != "") ? Convert.ToString(txtempresa_cony.Text.Trim()) : String.Empty;
        if (txtantiguedad_cony.Text != "") vPersona1.antiguedadlugarempresa = Convert.ToInt64(txtantiguedad_cony.Text.Trim());

        if (ddlcontrato_cony.Text != "") vPersona1.codtipocontrato = Convert.ToInt64(ddlcontrato_cony.SelectedValue);
        if (ddlCargo_cony.SelectedItem != null) vPersona1.codcargo = Convert.ToInt64(ddlCargo_cony.Text.Trim());
        vPersona1.telefonoempresa = (txtTelefonoEmp_Cony.Text != "") ? Convert.ToString(txtTelefonoEmp_Cony.Text.Trim()) : String.Empty;

        if (txtdirec_cony.Text != "")
            vPersona1.direccionempresa = txtdirec_cony.Text;
        else
            vPersona1.direccionempresa = "0";

        // Asignar fecha de ùltima modificaciòn del conyùge
        try
        {
            vPersona1.fecultmod = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
        }
        catch
        {
            vPersona1.fecultmod = DateTime.Now;
        }

        if (ddlOcupacion_Cony.SelectedIndex > 0)
            vPersona1.ocupacion = ddlOcupacion_Cony.SelectedValue;

        //DATOS NULOS -->\\
        vPersona1.tipo_persona = (rblTipoPersona.Text != "") ? Convert.ToString(rblTipoPersona.SelectedValue) : String.Empty;
        if (txtdigito_cony.Text != "") vPersona1.digito_verificacion = Convert.ToInt64(txtdigito_cony.Text.Trim());

        /*try
        {
            if (txtfechaExp_cony.Text != "") vPersona1.fechaexpedicion = Convert.ToDateTime(txtfechaExp_cony.Text.Trim());
        }
        catch
        {
        }*/
        vPersona1.razon_social = (txtRazonSoc_cony.Text != "") ? Convert.ToString(txtRazonSoc_cony.Text.Trim()) : String.Empty;

        if (ddlLugarNacimiento.SelectedIndex > 0) vPersona1.codciudadnacimiento = Convert.ToInt64(ddlLugarNacimiento.SelectedValue);
        if (ddlEstadoCivil.SelectedIndex > 0) vPersona1.codestadocivil = Convert.ToInt64(ddlEstadoCivil.SelectedValue);
        if (ddlNivelEscolaridad.SelectedIndex > 0) vPersona1.codescolaridad = Convert.ToInt64(ddlNivelEscolaridad.SelectedValue);
        if (ddlActividad_cony.SelectedIndex > 0) vPersona1.codactividad = Convert.ToInt64(ddlActividad_cony.SelectedValue);
        vPersona1.direccion = (txtdirec_cony.Text != "") ? Convert.ToString(txtdirec_cony.Text.Trim()) : String.Empty;
        vPersona1.telefono = String.Empty;
        if (ddlActividad_cony.SelectedIndex > 0) vPersona1.codciudadresidencia = Convert.ToInt64(ddlActividad_cony.SelectedValue);
        vPersona1.tipovivienda = (rblTipoVivienda.Text != "") ? Convert.ToString(rblTipoVivienda.SelectedValue) : String.Empty;
        vPersona1.arrendador = (txtArrendador.Text != "") ? Convert.ToString(txtArrendador.Text.Trim()) : String.Empty;
        vPersona1.telefonoarrendador = (txtTelefonoarrendador.Text != "") ? Convert.ToString(txtTelefonoarrendador.Text.Trim()) : String.Empty;
        if (txtSalario_cony.Text != "") vPersona1.salario = Convert.ToInt64(txtSalario_cony.Text.Trim());
        if (txtCod_asesor.Text != "") vPersona1.cod_asesor = Convert.ToInt64(txtCod_asesor.Text.Trim());
        vPersona1.residente = (rblResidente.Text != "") ? Convert.ToString(rblResidente.SelectedValue) : String.Empty;
        try
        {
            if (txtFecha_residencia.Text != "") vPersona1.fecha_residencia = Convert.ToDateTime(txtFecha_residencia.Text.Trim());
        }
        catch
        {
        }
        if (txtCod_oficina.Text != "") vPersona1.cod_oficina = Convert.ToInt64(txtCod_oficina.Text.Trim());
        vPersona1.tratamiento = (txtTratamiento.Text != "") ? Convert.ToString(txtTratamiento.Text.Trim()) : String.Empty;
        vPersona1.estado = (ddlEstado.Text != "") ? Convert.ToString(ddlEstado.SelectedValue) : String.Empty;
        // Asignar la fecha de creaciòn del conyugè
        try
        {
            vPersona1.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
        }
        catch
        {
            vPersona1.fechacreacion = DateTime.Now;
        }
        Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
        vPersona1.usuariocreacion = lblUsuario.Text;

        vPersona1.usuultmod = lblUsuario.Text;
        vPersona1.codciudadresidencia = 0;
        vPersona1.telCorrespondencia = "";
        vPersona1.dirCorrespondencia = "";
        vPersona1.barrioResidencia = 0;
        vPersona1.ciuCorrespondencia = 0;
        vPersona1.barrioCorrespondencia = 0;
        vPersona1.ActividadEconomicaEmpresa = 0;
        vPersona1.ciudad = 0;
        vPersona1.relacionEmpleadosEmprender = 0;
        vPersona1.CelularEmpresa = " ";
        vPersona1.profecion = " ";
        vPersona1.PersonasAcargo = 0;

        PersonaResponsable pResponsable = new PersonaResponsable();
        if (vPersona1.cod_persona != 0)
        {
            Session[Usuario.codusuario + "Cod_persona_conyuge"] = txtcod_conyuge.Text;
            persona1Servicio.ModificarConyugeAporte(vPersona1, false, pResponsable, (Usuario)Session["usuario"]);
        }
        else
        {
            persona1Servicio.CrearPersona1(vPersona1, (Usuario)Session["usuario"]);
        }
        vConyuge.cod_conyuge = vPersona1.cod_persona;
        Session[Usuario.codusuario + "Cod_persona_conyuge"] = vConyuge.cod_conyuge;
        vConyuge.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "Cod_persona"].ToString());
        ConyugeServicio.CrearConyuge(vConyuge, (Usuario)Session["usuario"]);
        idObjeto = vPersona1.cod_persona.ToString();
    }

    #endregion

    protected void ObtenerDatosInformacion_INGRE_EGRE(Int64 cod_persona)
    {
        Xpinn.FabricaCreditos.Entities.EstadosFinancieros InformacionFinanciera = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();
        List<Xpinn.FabricaCreditos.Entities.EstadosFinancieros> lstLista = new List<Xpinn.FabricaCreditos.Entities.EstadosFinancieros>();
        Int64 cod;
        cod = cod_persona;
        try
        {
            InformacionFinanciera = EstadosFinancierosServicio.listarperosnainfofin(cod, (Usuario)Session["usuario"]);
            if (InformacionFinanciera != null)
            {
                if (InformacionFinanciera.cod_persona != Int64.MinValue)
                    cod_per.Text = HttpUtility.HtmlDecode(InformacionFinanciera.cod_persona.ToString().Trim());
                if (InformacionFinanciera.cod_personaconyuge != Int64.MinValue)
                    cod_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.cod_personaconyuge.ToString().Trim());
                if (InformacionFinanciera.sueldo != Int64.MinValue)
                    txtsueldo_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.sueldo.ToString().Trim());
                if (InformacionFinanciera.sueldoconyuge != Int64.MinValue)
                    txtsueldo_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.sueldoconyuge.ToString().Trim());
                if (InformacionFinanciera.honorarios != Int64.MinValue)
                    txthonorario_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.honorarios.ToString().Trim());
                if (InformacionFinanciera.honorariosconyuge != Int64.MinValue)
                    txthonorario_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.honorariosconyuge.ToString().Trim());
                if (InformacionFinanciera.arrendamientos != Int64.MinValue)
                    txtarrenda_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.arrendamientos.ToString().Trim());
                if (InformacionFinanciera.arrendamientosconyuge != Int64.MinValue)
                    txtarrenda_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.arrendamientosconyuge.ToString().Trim());
                if (InformacionFinanciera.otrosingresos != Int64.MinValue)
                    txtotrosIng_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.otrosingresos.ToString().Trim());
                if (InformacionFinanciera.otrosingresosconyuge != Int64.MinValue)
                    txtotrosIng_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.otrosingresosconyuge.ToString().Trim());

                txtConceptoOtros_soli.Text = InformacionFinanciera.conceptootros;
                txtConceptoOtros_cony.Text = InformacionFinanciera.conceptootrosconyuge;

                // TOTALIZANDO VALORES DE INGRESOS
                decimal pTotalSoli = InformacionFinanciera.sueldo + InformacionFinanciera.honorarios + InformacionFinanciera.arrendamientos
                                    + InformacionFinanciera.otrosingresos;
                decimal pTotalCony = InformacionFinanciera.sueldoconyuge + InformacionFinanciera.honorariosconyuge +
                                    InformacionFinanciera.arrendamientosconyuge + InformacionFinanciera.otrosingresosconyuge;

                if (InformacionFinanciera.totalingreso != Int64.MinValue)
                {
                    txttotalING_soli.Text = pTotalSoli == InformacionFinanciera.totalingreso ? HttpUtility.HtmlDecode(InformacionFinanciera.totalingreso.ToString().Trim()) : pTotalSoli.ToString();
                    hdtotalING_soli.Value = pTotalSoli == InformacionFinanciera.totalingreso ? InformacionFinanciera.totalingreso.ToString() : pTotalSoli.ToString();
                }
                if (InformacionFinanciera.totalingresoconyuge != Int64.MinValue)
                {
                    txttotalING_cony.Text = pTotalCony == InformacionFinanciera.totalingreso ? HttpUtility.HtmlDecode(InformacionFinanciera.totalingresoconyuge.ToString().Trim()) : pTotalCony.ToString();
                    hdtotalING_cony.Value = pTotalCony == InformacionFinanciera.totalingreso ? InformacionFinanciera.totalingresoconyuge.ToString() : pTotalCony.ToString();
                }

                if (InformacionFinanciera.hipoteca != Int64.MinValue)
                    txthipoteca_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.hipoteca.ToString().Trim());
                if (InformacionFinanciera.hipotecaconyuge != Int64.MinValue)
                    txthipoteca_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.hipotecaconyuge.ToString().Trim());
                if (InformacionFinanciera.targeta_credito != Int64.MinValue)
                    txttarjeta_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.targeta_credito.ToString().Trim());
                if (InformacionFinanciera.targeta_creditoconyuge != Int64.MinValue)
                    txttarjeta_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.targeta_creditoconyuge.ToString().Trim());
                if (InformacionFinanciera.otrosprestamos != Int64.MinValue)
                    txtotrosPres_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.otrosprestamos.ToString().Trim());
                if (InformacionFinanciera.otrosprestamosconyuge != Int64.MinValue)
                    txtotrosPres_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.otrosprestamosconyuge.ToString().Trim());
                if (InformacionFinanciera.gastofamiliar != Int64.MinValue)
                    txtgastosFam_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.gastofamiliar.ToString().Trim());
                if (InformacionFinanciera.gastofamiliarconyuge != Int64.MinValue)
                    txtgastosFam_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.gastofamiliarconyuge.ToString().Trim());
                if (InformacionFinanciera.decunomina != Int64.MinValue)
                    txtnomina_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.decunomina.ToString().Trim());
                if (InformacionFinanciera.decunominaconyuge != Int64.MinValue)
                    txtnomina_cony.Text = HttpUtility.HtmlDecode(InformacionFinanciera.decunominaconyuge.ToString().Trim());

                // TOTALIZANDO VALORES DE EGRESOS
                pTotalSoli = InformacionFinanciera.hipoteca + InformacionFinanciera.targeta_credito + InformacionFinanciera.otrosprestamos
                                    + InformacionFinanciera.gastofamiliar + InformacionFinanciera.decunomina;
                pTotalCony = InformacionFinanciera.hipotecaconyuge + InformacionFinanciera.targeta_creditoconyuge + InformacionFinanciera.otrosprestamosconyuge
                                    + InformacionFinanciera.gastofamiliarconyuge + InformacionFinanciera.decunominaconyuge;

                if (InformacionFinanciera.totalegresos != Int64.MinValue)
                {
                    txttotalEGR_soli.Text = pTotalSoli == InformacionFinanciera.totalegresos ? HttpUtility.HtmlDecode(InformacionFinanciera.totalegresos.ToString().Trim()) : pTotalSoli.ToString();
                    hdtotalEGR_soli.Value = pTotalSoli == InformacionFinanciera.totalegresos ? InformacionFinanciera.totalegresos.ToString() : pTotalSoli.ToString();
                }
                if (InformacionFinanciera.totalegresosconyuge != Int64.MinValue)
                {
                    txttotalEGR_cony.Text = pTotalCony == InformacionFinanciera.totalegresosconyuge ? HttpUtility.HtmlDecode(InformacionFinanciera.totalegresosconyuge.ToString().Trim()) : pTotalCony.ToString();
                    hdtotalEGR_cony.Value = pTotalCony == InformacionFinanciera.totalegresosconyuge ? InformacionFinanciera.totalegresosconyuge.ToString() : pTotalCony.ToString();
                }

                //Agregado para listar información de activos, pasivos y patrimonio
                if (InformacionFinanciera.TotAct != Int64.MinValue)
                    txtactivos_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotAct.ToString().Trim());
                if (InformacionFinanciera.TotActConyuge != Int64.MinValue)
                    txtactivos_conyuge.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotActConyuge.ToString().Trim());
                if (InformacionFinanciera.TotPas != Int64.MinValue)
                    txtpasivos_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotPas.ToString().Trim());
                if (InformacionFinanciera.TotPasConyuge != Int64.MinValue)
                    txtpasivos_conyuge.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotPasConyuge.ToString().Trim());
                if (InformacionFinanciera.TotPat != Int64.MinValue)
                    txtpatrimonio_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotPat.ToString().Trim());
                if (InformacionFinanciera.TotPatConyuge != Int64.MinValue)
                    txtpatrimonio_conyuge.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotPatConyuge.ToString().Trim());
            }
        }
        catch { }
    }

    /// <summary>
    /// Determinar la edad de la persona con base en la fecha de nacimiento
    /// </summary>
    /// <param name="birthDate"></param>
    /// <returns></returns>
    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }

    /// <summary>
    /// Cargar información de las listas desplegables
    /// </summary>
    private void CargarListas()
    {
        try
        {
            String ListaSolicitada = null;  // Cadena en la que se indica la lista a solicitar
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
            List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosFiltrado = new List<Xpinn.FabricaCreditos.Entities.Persona1>();

            ListaSolicitada = "Barrio";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlBarrioCorrespondencia.DataSource = lstDatosSolicitud;
            ddlBarrioResid.DataSource = lstDatosSolicitud;

            ddlBarrioCorrespondencia.DataTextField = "ListaDescripcion";
            ddlBarrioResid.DataTextField = "ListaDescripcion";

            ddlBarrioCorrespondencia.DataValueField = "ListaId";
            ddlBarrioResid.DataValueField = "ListaId";

            ddlBarrioCorrespondencia.DataBind();
            ddlBarrioResid.DataBind();

            /*ListaSolicitada = "TipoIdentificacion";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            lstDatosFiltrado = null;
            lstDatosFiltrado = lstDatosSolicitud.Where(x => x.ListaId != 2 && x.ListaId != 4 && x.ListaId != 6 && x.ListaId != 7).ToList();
            ddlTipoE.DataSource = lstDatosFiltrado;
            ddlTipoE.DataTextField = "ListaDescripcion";
            ddlTipoE.DataValueField = "ListaId";
            ddlTipoE.DataBind();*/

            PoblarLista("TIPOIDENTIFICACION", "CODTIPOIDENTIFICACION, DESCRIPCION", "TIPO_PERSONA IN (0,1)", "1", ddlTipoE);

            ListaSolicitada = "Ciudades";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlLugarExpedicion.DataSource = lstDatosSolicitud;
            ddlLugarNacimiento.DataSource = lstDatosSolicitud;
            ddlLugNacimiento_Cony.DataSource = lstDatosSolicitud;
            ddlCiuCorrespondencia.DataSource = lstDatosSolicitud;
            ddlCiu0.DataSource = lstDatosSolicitud;
            ddlCiudadResidencia.DataSource = lstDatosSolicitud;

            ddlLugarExpedicion.DataTextField = "ListaDescripcion";
            ddlLugarNacimiento.DataTextField = "ListaDescripcion";
            ddlLugNacimiento_Cony.DataTextField = "ListaDescripcion";
            ddlCiuCorrespondencia.DataTextField = "ListaDescripcion";
            ddlCiu0.DataTextField = "ListaDescripcion";
            ddlCiudadResidencia.DataTextField = "ListaDescripcion";

            ddlLugarExpedicion.DataValueField = "ListaId";
            ddlLugarNacimiento.DataValueField = "ListaId";
            ddlLugNacimiento_Cony.DataValueField = "ListaId";
            ddlCiuCorrespondencia.DataValueField = "ListaId";
            ddlCiu0.DataValueField = "ListaId";
            ddlCiudadResidencia.DataValueField = "ListaId";

            ddlLugarExpedicion.DataBind();
            ddlLugarNacimiento.DataBind();
            ddlLugNacimiento_Cony.DataBind();
            ddlCiuCorrespondencia.DataBind();
            ddlCiu0.DataBind();
            ddlCiudadResidencia.DataBind();

            PoblarLista("CIUDADES", "CODCIUDAD, NOMCIUDAD", " TIPO = 1", "1", ddlPais);

            // Colocar ciudad por defecto
            String CargarCiudad = System.Configuration.ConfigurationManager.AppSettings["CargarCiudad"].ToString();
            if (CargarCiudad == "true")
            {
                String CiudadDefault = System.Configuration.ConfigurationManager.AppSettings["Ciudad"].ToString();
                ddlLugarExpedicion.SelectedValue = CiudadDefault;
                ddlLugarNacimiento.SelectedValue = CiudadDefault;
                ddlCiuCorrespondencia.SelectedValue = CiudadDefault;
                ddlCiu0.SelectedValue = CiudadDefault;
                ddlCiudadResidencia.SelectedValue = CiudadDefault;
            }

            ListaSolicitada = "EstadoCivil";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlEstadoCivil.DataSource = lstDatosSolicitud;
            ddlEstadoCivil.DataTextField = "ListaDescripcion";
            ddlEstadoCivil.DataValueField = "ListaId";
            ddlEstadoCivil.DataBind();

            ListaSolicitada = "NivelEscolaridad";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlNivelEscolaridad.DataSource = lstDatosSolicitud;
            ddlNivelEscolaridad.DataTextField = "ListaDescripcion";
            ddlNivelEscolaridad.DataValueField = "ListaId";
            ddlNivelEscolaridad.DataBind();

            ListaSolicitada = "Actividad_Negocio";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlActividadE0.DataSource = lstDatosSolicitud;
            ddlActividadE0.DataTextField = "ListaDescripcion";
            ddlActividadE0.DataValueField = "ListaIdStr";
            ddlActividadE0.DataBind();

            ListaSolicitada = "Actividad2";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            //ddlActividadE.DataSource = lstDatosSolicitud;
            //ddlActividadE.DataTextField = "ListaDescripcion";
            //ddlActividadE.DataValueField = "ListaIdStr";
            //ddlActividadE.DataBind();
            ViewState["DTACTIVIDAD" + Usuario.codusuario] = lstDatosSolicitud;
            gvActividadesCIIU.DataSource = lstDatosSolicitud;
            gvActividadesCIIU.DataBind();

            gvActEmpresa.DataSource = lstDatosSolicitud;
            gvActEmpresa.DataBind();

            ListaSolicitada = "TipoContrato";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlTipoContrato.DataSource = lstDatosSolicitud;
            ddlTipoContrato.DataTextField = "ListaDescripcion";
            ddlTipoContrato.DataValueField = "ListaId";
            ddlTipoContrato.DataBind();

            ListaSolicitada = "TipoCargo";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlCargo.DataSource = lstDatosSolicitud;
            ddlCargo.DataTextField = "ListaDescripcion";
            ddlCargo.DataValueField = "ListaId";
            ddlCargo.AppendDataBoundItems = true;
            ddlCargo.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlCargo.SelectedIndex = 0;
            ddlCargo.DataBind();

            ListaSolicitada = "ESTADO_ACTIVO";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlEstado.DataSource = lstDatosSolicitud;
            ddlEstado.DataTextField = "ListaDescripcion";
            ddlEstado.DataValueField = "ListaId";
            ddlEstado.DataBind();

            List<Xpinn.Caja.Entities.Oficina> lstOficina = new List<Xpinn.Caja.Entities.Oficina>();
            Xpinn.Caja.Services.OficinaService oficinaServicio = new Xpinn.Caja.Services.OficinaService();
            Xpinn.Caja.Entities.Oficina pOficina = new Xpinn.Caja.Entities.Oficina();
            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["Usuario"];
            lstOficina = oficinaServicio.ListarOficina(pOficina, pUsuario);
            txtCod_oficina.DataSource = lstOficina;
            txtCod_oficina.DataTextField = "nombre";
            txtCod_oficina.DataValueField = "cod_oficina";
            txtCod_oficina.DataBind();

            //DATOS CONYUGE

            /*ListaSolicitada = "TipoIdentificacion";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            lstDatosFiltrado = null;
            lstDatosFiltrado = lstDatosSolicitud.Where(x => x.ListaId != 2 && x.ListaId != 4 && x.ListaId != 6 && x.ListaId != 7).ToList();
            ddlTipo.DataSource = lstDatosFiltrado;
            ddlTipo.DataTextField = "ListaDescripcion";
            ddlTipo.DataValueField = "ListaId";
            ddlTipo.DataBind();*/

            PoblarLista("TIPOIDENTIFICACION", "CODTIPOIDENTIFICACION, DESCRIPCION", "TIPO_PERSONA IN (0,1)", "1", ddlTipo);

            ListaSolicitada = "TipoCargo";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlCargo_cony.DataSource = lstDatosSolicitud;
            ddlCargo_cony.DataTextField = "ListaDescripcion";
            ddlCargo_cony.DataValueField = "ListaId";
            ddlCargo_cony.DataBind();

            ListaSolicitada = "TipoContrato";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlcontrato_cony.DataSource = lstDatosSolicitud;
            ddlcontrato_cony.DataTextField = "ListaDescripcion";
            ddlcontrato_cony.DataValueField = "ListaId";
            ddlcontrato_cony.DataBind();

            ListaSolicitada = "Lugares";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlcuidadExp_Cony.DataSource = lstDatosSolicitud;
            ddlcuidadExp_Cony.DataTextField = "ListaDescripcion";
            ddlcuidadExp_Cony.DataValueField = "ListaIdStr";
            ddlcuidadExp_Cony.DataBind();

            ListaSolicitada = "Sector";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlSector.DataSource = lstDatosSolicitud;
            ddlSector.DataTextField = "ListaDescripcion";
            ddlSector.DataValueField = "ListaId";
            ddlSector.DataBind();
            ddlSector.Items.Insert(0, new ListItem("Seleccione un item"));

            ListaSolicitada = "Zona";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlZona.DataSource = lstDatosSolicitud;
            ddlZona.DataTextField = "ListaDescripcion";
            ddlZona.DataValueField = "ListaId";
            ddlZona.DataBind();
            ddlZona.Items.Insert(0, new ListItem("Seleccione un item"));

            ddlOcupacion.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlOcupacion.Items.Insert(1, new ListItem("Empleado", "1"));
            ddlOcupacion.Items.Insert(2, new ListItem("Independiente", "2"));
            ddlOcupacion.Items.Insert(3, new ListItem("Pensionado", "3"));
            ddlOcupacion.Items.Insert(4, new ListItem("Estudiante", "4"));
            ddlOcupacion.Items.Insert(5, new ListItem("Hogar", "5"));
            ddlOcupacion.Items.Insert(6, new ListItem("Cesante", "6"));
            ddlOcupacion.Items.Insert(6, new ListItem("Desempleado", "7"));
            ddlOcupacion.SelectedIndex = 0;
            ddlOcupacion.DataBind();

            ddlOcupacion_Cony.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlOcupacion_Cony.Items.Insert(1, new ListItem("Empleado", "1"));
            ddlOcupacion_Cony.Items.Insert(2, new ListItem("Independiente", "2"));
            ddlOcupacion_Cony.Items.Insert(3, new ListItem("Pensionado", "3"));
            ddlOcupacion_Cony.Items.Insert(4, new ListItem("Estudiante", "4"));
            ddlOcupacion_Cony.Items.Insert(5, new ListItem("Hogar", "5"));
            ddlOcupacion_Cony.Items.Insert(6, new ListItem("Cesante", "6"));
            ddlOcupacion_Cony.Items.Insert(6, new ListItem("Desempleado", "7"));
            ddlOcupacion_Cony.SelectedIndex = 0;
            ddlOcupacion_Cony.DataBind();

            ListaSolicitada = "Zona";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlZona.DataSource = lstDatosSolicitud;
            ddlZona.DataTextField = "ListaDescripcion";
            ddlZona.DataValueField = "ListaId";
            ddlZona.DataBind();
            ddlZona.Items.Insert(0, new ListItem("Seleccione un item"));

            //LlenarListasDesplegables(TipoLista.Asesor, ddlAsesor);
            PoblarLista("asejecutivos", "ICODIGO, QUITARESPACIOS(Substr(snombre1 || ' ' || snombre2 || ' ' || sapellido1 || ' ' || sapellido2, 0, 240))", "", "", ddlAsesor);

            //CARGANDO DOCUMENTOS PERTENECIENTES A AFILIACION
            ctlFormatos.Inicializar("1");
            //idObjeto = Session[_afiliacionServicio.CodigoPrograma + ".id"].ToString();

            //ctlFormatos.Inicializar("1");

            ListaSolicitada = "AsociadosEspeciales";
            lstDatosSolicitud = TraerResultadosLista(ListaSolicitada);
            ddlAsociadosEspeciales.DataSource = lstDatosSolicitud;
            ddlAsociadosEspeciales.DataTextField = "ListaDescripcion";
            ddlAsociadosEspeciales.DataValueField = "ListaId";
            ddlAsociadosEspeciales.DataBind();
            ddlAsociadosEspeciales.Items.Insert(0, new ListItem("Seleccione un item", ""));
            ddlAsociadosEspeciales.SelectedIndex = 0;

            //Listas Modal
            LlenarDDLTipoIdentificacion();
            LlenarDDLTipoActivo();

            //Listar Cargos Peps
            PoblarLista("CARGOS_PEP", "COD_CARGO, DESCRIPCION", "", "1", txtCargoPEPS);

            //Listar tipo empresa
            ddlTipoEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoEmpresa.Items.Insert(1, new ListItem("Pública", "1"));
            ddlTipoEmpresa.Items.Insert(2, new ListItem("Privada", "2"));
            ddlTipoEmpresa.Items.Insert(3, new ListItem("Mixta", "3"));
            ddlTipoEmpresa.SelectedIndex = 0;
            ddlTipoEmpresa.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = persona1Servicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }

    /// <summary>
    /// Método para guardar la información de datos básicos del cliente
    /// </summary>
    ///    
    protected void rblTipoVivienda_DataBound(object sender, EventArgs e)
    {
        RadioButtonList rbl = (RadioButtonList)sender;
        foreach (ListItem li in rblTipoVivienda.Items)
        {
            li.Attributes.Add("onclick", "javascript:DoSomething('" + li.Value + "')");
        }
    }

    protected void rblTipoVivienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        validarArriendo();
    }

    private void validarArriendo()
    {
        if (rblTipoVivienda.SelectedValue == "A")
        {
            txtArrendador.Enabled = true;
            txtTelefonoarrendador.Enabled = true;
            txtValorArriendo.Enabled = true;
            lblValorArriendo.Visible = true;
            txtValorArriendo.Visible = true;
            lblPropietario.Text = "Nombre Arrendador";
            lblTelefPropietario.Text = "Teléfono Arrendador";
        }
        else if (rblTipoVivienda.SelectedValue == "P" || rblTipoVivienda.SelectedValue == "F")
        {
            /*txtArrendador.Enabled = false;
            txtTelefonoarrendador.Enabled = false;
            txtValorArriendo.Enabled = false;
            txtArrendador.Text = "";
            txtTelefonoarrendador.Text = "";
            txtValorArriendo.Text = "";*/
            lblPropietario.Text = "Nombre Propietario";
            lblTelefPropietario.Text = "Teléfono Propietario";
            lblValorArriendo.Visible = false;
            txtValorArriendo.Visible = false;
        }
    }

    /// <summary>
    /// Método para calcular la edad según la fecha de nacimiento ingresada
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtFechanacimiento_TextChanged(object sender, EventArgs e)
    {
        CalcularEdad(1);
    }

    /// <summary>
    /// Determinar la edad del conyugue en base a la fecha de nacimiento
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtfechaNac_Cony_TextChanged(object sender, EventArgs e)
    {
        CalcularEdad(2);
    }

    /// <summary>
    /// Cálculo de la edad
    /// </summary>
    private void CalcularEdad(int valor)
    {
        try
        {
            //Consultar parametro general de mayoria de edad
            Xpinn.Comun.Services.GeneralService generalServicio = new Xpinn.Comun.Services.GeneralService();
            General pGeneral = new General();
            pGeneral = generalServicio.ConsultarGeneral(104, (Usuario)Session["usuario"]);

            int mayoriaEdad = 0;
            mayoriaEdad = pGeneral.valor != null && pGeneral.valor != "" ? Convert.ToInt32(pGeneral.valor) : 18;

            if (txtFechanacimiento.Text != "" && valor == 1)
            {
                txtEdadCliente.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimiento.Text)));
                if (Convert.ToInt64(txtEdadCliente.Text) >= 0)
                {
                    if (Convert.ToInt64(txtEdadCliente.Text) < mayoriaEdad)
                    {
                        VerError("La persona debe ser mayor de edad");
                        txtFechanacimiento.Text = "";
                        txtEdadCliente.Text = "";
                        RegistrarPostBack();
                    }
                }
            }
            else if (txtfechaNac_Cony.Text != "" && valor == 2)
            {
                txtEdad_Cony.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtfechaNac_Cony.Text)));
            }
        }
        catch
        {
            txtEdadCliente.Text = "";
        }
    }

    protected string FormatoFecha()
    {
        Configuracion conf = new Configuracion();
        return conf.ObtenerFormatoFecha();
    }

    protected void linkBt_Click(object sender, EventArgs e)
    {
        try
        {
            /*Este evento se generara desde la funcion javascript asignarRutaFoto*/
            if (fuFoto.HasFile == true)
            {
                cargarFotografia();
                mostrarImagen();
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnCargarImagen_Click(object sender, EventArgs e)
    {
        try
        {/*Este evento se generara desde la funcion javascript asignarRutaFoto*/
            if (fuFoto.HasFile == true)
            {
                cargarFotografia();
                mostrarImagen();
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    private void cargarFotografia()
    {
        /*Obtenemos el nombre y la extension del archivo*/
        String fileName = Path.GetFileName(this.fuFoto.PostedFile.FileName);
        String extension = Path.GetExtension(this.fuFoto.PostedFile.FileName).ToLower();

        try
        {

            if (extension != ".png" && extension != ".jpg" && extension != ".bmp")
            {
                VerError("El archivo ingresado no es una imagen");
            }
            else
            {
                /*Se guarda la imagen en el servidor*/
                fuFoto.PostedFile.SaveAs(Server.MapPath("Images\\") + fileName);
                /*Obtenemos el nombre temporal de la imagen con la siguiente funcion*/
                String nombreImgServer = getNombreImagenServidor(extension);
                hdFileName.Value = nombreImgServer;
                /*Cambiamos el nombre de la imagen por el nuevo*/
                File.Move(Server.MapPath("Images\\") + fileName, Server.MapPath("Images\\" + nombreImgServer));
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    public String getNombreImagenServidor(String extension)
    {
        /*Devuelve el nombre temporal de la imagen*/
        Random nRandom = new Random();
        String nr = Convert.ToString(nRandom.Next(0, 32000));
        String nombre = nr + "_" + DateTime.Today.ToString("ddMMyyyy") + extension;
        nRandom = null;
        return nombre;
    }

    private void mostrarImagen()
    {
        /*Muestra la imagen como un thumbnail*/
        System.Drawing.Image objImage = null, objThumbnail = null;
        Int32 width, height;
        String fileName = Server.MapPath("Images\\") + Path.GetFileName(this.hdFileName.Value);
        Stream stream = null;
        try
        {
            /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
            stream = File.OpenRead(fileName);
            if (stream.Length > 2000000)
            {
                VerError("La imagen tiene un valor muy grande");
            }

            objImage = System.Drawing.Image.FromStream(stream);
            width = 100;
            height = objImage.Height / (objImage.Width / width);
            this.Response.Clear();
            /*Se crea el thumbnail y se muestra en la imagen*/
            objThumbnail = objImage.GetThumbnailImage(width, height, null, IntPtr.Zero);
            objThumbnail.Save(Server.MapPath("Images\\") + "thumb_" + this.hdFileName.Value, ImageFormat.Jpeg);
            imgFoto.Visible = true;
            String nombreImgThumb = "thumb_" + this.hdFileName.Value;
            this.hdFileNameThumb.Value = nombreImgThumb;
            imgFoto.ImageUrl = "Images\\" + nombreImgThumb;

        }
        catch (Exception ex)
        {
            VerError("No pudro abrir archivo con imagen de la persona " + ex.Message);
        }
        finally
        {
            /*Limpiamos los objetos*/
            objImage.Dispose();
            objThumbnail.Dispose();
            stream.Dispose();
            objImage = null;
            objThumbnail = null;
            stream = null;
        }
    }

    #region beneficiarios

    protected void InicializarBeneficiarios()
    {
        List<Beneficiario> lstBeneficiarios = new List<Beneficiario>();
        for (int i = gvBeneficiarios.Rows.Count; i < 5; i++)
        {
            Beneficiario eBenef = new Beneficiario();
            eBenef.idbeneficiario = -1;
            eBenef.nombre = "";
            eBenef.identificacion_ben = "";
            eBenef.tipo_identificacion_ben = null;
            eBenef.nombre_ben = "";
            eBenef.fecha_nacimiento_ben = null;
            eBenef.parentesco = null;
            eBenef.porcentaje_ben = null;
            lstBeneficiarios.Add(eBenef);
        }
        gvBeneficiarios.DataSource = lstBeneficiarios;
        gvBeneficiarios.DataBind();

        ViewState[Usuario.codusuario + "DatosBene"] = lstBeneficiarios;
    }

    protected List<Beneficiario> ObtenerListaBeneficiarios()
    {
        List<Beneficiario> lstBeneficiarios = new List<Beneficiario>();
        List<Beneficiario> lista = new List<Beneficiario>();

        foreach (GridViewRow rfila in gvBeneficiarios.Rows)
        {
            Beneficiario eBenef = new Beneficiario();
            Label lblidbeneficiario = (Label)rfila.FindControl("lblidbeneficiario");
            if (lblidbeneficiario != null)
                eBenef.idbeneficiario = Convert.ToInt64(lblidbeneficiario.Text);

            DropDownListGrid ddlParentezco = (DropDownListGrid)rfila.FindControl("ddlParentezco");
            if (ddlParentezco.SelectedValue != null || ddlParentezco.SelectedIndex != 0)
                eBenef.parentesco = Convert.ToInt32(ddlParentezco.SelectedValue);

            TextBox txtIdentificacion = (TextBox)rfila.FindControl("txtIdentificacion");
            if (txtIdentificacion != null)
                eBenef.identificacion_ben = Convert.ToString(txtIdentificacion.Text);
            TextBox txtNombres = (TextBox)rfila.FindControl("txtNombres");
            if (txtNombres != null)
                eBenef.nombre_ben = Convert.ToString(txtNombres.Text);
            fecha txtFechaNacimientoBen = (fecha)rfila.FindControl("txtFechaNacimientoBen");
            if (txtFechaNacimientoBen != null)
                if (txtFechaNacimientoBen.Text != "")
                    eBenef.fecha_nacimiento_ben = txtFechaNacimientoBen.ToDateTime;
                else
                    eBenef.fecha_nacimiento_ben = null;
            else
                eBenef.fecha_nacimiento_ben = null;
            decimalesGridRow txtPorcentaje = (decimalesGridRow)rfila.FindControl("txtPorcentaje");
            if (txtPorcentaje != null)
                eBenef.porcentaje_ben = Convert.ToDecimal(txtPorcentaje.Text);

            lista.Add(eBenef);
            ViewState[Usuario.codusuario + "DatosBene"] = lista;

            if (eBenef.identificacion_ben.Trim() != "" && eBenef.nombre_ben.Trim() != null)
            {
                lstBeneficiarios.Add(eBenef);
            }
        }
        return lstBeneficiarios;
    }

    protected void gvBeneficiarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlParentezco = (DropDownList)e.Row.FindControl("ddlParentezco");
            if (ddlParentezco != null)
            {
                Beneficiario Ben = new Beneficiario();
                ddlParentezco.DataSource = BeneficiarioServicio.ListarParentesco(Ben, (Usuario)Session["usuario"]);
                ddlParentezco.DataTextField = "DESCRIPCION";
                ddlParentezco.DataValueField = "CODPARENTESCO";
                ddlParentezco.Items.Insert(0, new ListItem("<Seleccione un item>", "0"));
                ddlParentezco.DataBind();

            }

            Label lblParentezco = (Label)e.Row.FindControl("lblParentezco");
            if (lblParentezco.Text != null)
                ddlParentezco.SelectedValue = lblParentezco.Text;

        }
    }

    protected void gvBeneficiarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvBeneficiarios.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaBeneficiarios();

        List<Beneficiario> LstBene;
        LstBene = (List<Beneficiario>)ViewState[Usuario.codusuario + "DatosBene"];

        if (conseID > 0)
        {
            try
            {
                foreach (Beneficiario bene in LstBene)
                {
                    if (bene.idbeneficiario == conseID)
                    {
                        BeneficiarioServicio.EliminarBeneficiario(conseID, (Usuario)Session["usuario"]);
                        LstBene.Remove(bene);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstBene.RemoveAt((gvBeneficiarios.PageIndex * gvBeneficiarios.PageSize) + e.RowIndex);
        }

        gvBeneficiarios.DataSourceID = null;
        gvBeneficiarios.DataBind();

        gvBeneficiarios.DataSource = LstBene;
        gvBeneficiarios.DataBind();

        ViewState[Usuario.codusuario + "DatosBene"] = LstBene;
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        ObtenerListaBeneficiarios();

        List<Beneficiario> lstBene = new List<Beneficiario>();

        if (ViewState[Usuario.codusuario + "DatosBene"] != null)
        {
            lstBene = (List<Beneficiario>)ViewState[Usuario.codusuario + "DatosBene"];
            int porcentaje = 0;
            porcentaje = Convert.ToInt32(lstBene.Where(x => x.porcentaje_ben > 0).Sum(x => x.porcentaje_ben));
            if (porcentaje < 100)
            {
                for (int i = 1; i <= 1; i++)
                {
                    Beneficiario eBenef = new Beneficiario();
                    eBenef.idbeneficiario = -1;
                    eBenef.nombre = "";
                    eBenef.identificacion_ben = "";
                    eBenef.tipo_identificacion_ben = null;
                    eBenef.nombre_ben = "";
                    eBenef.fecha_nacimiento_ben = null;
                    eBenef.parentesco = null;
                    eBenef.porcentaje_ben = null;
                    lstBene.Add(eBenef);
                }
                gvBeneficiarios.DataSource = lstBene;
                gvBeneficiarios.DataBind();

                ViewState[Usuario.codusuario + "DatosBene"] = lstBene;
            }
        }
        else
        {
            for (int i = 1; i <= 1; i++)
            {
                Beneficiario eBenef = new Beneficiario();
                eBenef.idbeneficiario = -1;
                eBenef.nombre = "";
                eBenef.identificacion_ben = "";
                eBenef.tipo_identificacion_ben = null;
                eBenef.nombre_ben = "";
                eBenef.fecha_nacimiento_ben = null;
                eBenef.parentesco = null;
                eBenef.porcentaje_ben = null;
                lstBene.Add(eBenef);
            }
            gvBeneficiarios.DataSource = lstBene;
            gvBeneficiarios.DataBind();
            ViewState[Usuario.codusuario + "DatosBene"] = lstBene;
        }
    }


    #endregion


    #region Actividades

    protected void InicializarActividades()
    {
        List<Actividades> lstActividades = new List<Actividades>();
        for (int i = gvActividades.Rows.Count; i < 5; i++)
        {
            Actividades eActi = new Actividades();
            eActi.idactividad = -1;
            eActi.fecha_realizacion = null;
            eActi.tipo_actividad = null;
            eActi.descripcion = "";
            eActi.participante = null;
            eActi.calificacion = "";
            eActi.duracion = "";
            lstActividades.Add(eActi);
        }
        gvActividades.DataSource = lstActividades;
        gvActividades.DataBind();
        Session[Usuario.codusuario + "DatosActividad"] = lstActividades;

    }

    protected List<Actividades> ObtenerListaActividades()//Int64 cod
    {
        List<Actividades> lstActividades = new List<Actividades>();
        List<Actividades> lista = new List<Actividades>();


        foreach (GridViewRow rfila in gvActividades.Rows)
        {
            Actividades eActi = new Actividades();
            Label lblactividad = (Label)rfila.FindControl("lblactividad");

            if (lblactividad != null)
                eActi.idactividad = Convert.ToInt32(lblactividad.Text);
            fecha txtfecha = (fecha)rfila.FindControl("txtfecha");
            if (txtfecha != null)
                if (txtfecha.Text != "")
                    eActi.fecha_realizacion = txtfecha.ToDateTime;
                else
                    eActi.fecha_realizacion = null;
            else
                eActi.fecha_realizacion = null;
            DropDownListGrid ddlActividad = (DropDownListGrid)rfila.FindControl("ddlActividad");
            if (ddlActividad.SelectedValue != null)
                eActi.tipo_actividad = Convert.ToInt32(ddlActividad.SelectedValue);
            TextBox txtDescripcion = (TextBox)rfila.FindControl("txtDescripcion");
            if (txtDescripcion != null)
                eActi.descripcion = Convert.ToString(txtDescripcion.Text);
            DropDownListGrid ddlParticipante = (DropDownListGrid)rfila.FindControl("ddlParticipante");
            if (ddlParticipante.SelectedValue != null)
                eActi.participante = Convert.ToInt32(ddlParticipante.SelectedValue);
            TextBox txtCalificacion = (TextBox)rfila.FindControl("txtCalificacion");
            if (txtCalificacion != null)
                eActi.calificacion = Convert.ToString(txtCalificacion.Text);
            TextBox txtDuracion = (TextBox)rfila.FindControl("txtDuracion");
            if (txtDuracion != null)
                eActi.duracion = Convert.ToString(txtDuracion.Text);
            //eActi.cod_persona = cod;
            lista.Add(eActi);
            Session[Usuario.codusuario + "DatosActividad"] = lista;

            if (eActi.tipo_actividad.Value != 0 && eActi.participante.Value != 0 && eActi.fecha_realizacion != DateTime.MinValue)
            {
                lstActividades.Add(eActi);
            }
        }
        return lstActividades;
    }

    protected void gvActividades_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ActividadesServices ActService = new ActividadesServices();

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlActividad = (DropDownList)e.Row.FindControl("ddlActividad");
            DropDownList ddlParticipante = (DropDownList)e.Row.FindControl("ddlParticipante");


            if (ddlActividad != null)
            {
                ddlActividad.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlActividad.Items.Insert(1, new ListItem("ACTIVIDAD", "1"));
                ddlActividad.Items.Insert(2, new ListItem("EVENTO", "2"));
                ddlActividad.Items.Insert(3, new ListItem("CURSO", "3"));
                ddlActividad.Items.Insert(3, new ListItem("EDUCACIÓN FINANCIERA", "4"));
                ddlActividad.Items.Insert(3, new ListItem("EDUCACIÓN E. SOLIDARIA", "5"));
                ddlActividad.SelectedIndex = 0;
                ddlActividad.DataBind();
            }

            if (ddlParticipante != null)
            {
                ddlParticipante.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
                ddlParticipante.Items.Insert(1, new ListItem("ASOCIADO", "1"));
                ddlParticipante.Items.Insert(2, new ListItem("FAMILIAR", "2"));
                ddlParticipante.SelectedIndex = 0;
                ddlParticipante.DataBind();
            }

            Label lblTipoActividad = (Label)e.Row.FindControl("lblTipoActividad");
            if (lblTipoActividad != null)
                ddlActividad.SelectedValue = lblTipoActividad.Text;

            Label lblParticipante = (Label)e.Row.FindControl("lblParticipante");
            if (lblParticipante != null)
                ddlParticipante.SelectedValue = lblParticipante.Text;

        }
    }

    protected void gvActividades_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int conseID = Convert.ToInt32(gvActividades.DataKeys[e.NewEditIndex].Values[0].ToString());

        if (conseID != 0)
        {

            gvActividades.EditIndex = e.NewEditIndex;
        }
        else
        {
            e.Cancel = true;
        }
    }

    protected void gvActividades_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvActividades.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaActividades();

        List<Actividades> LstActi;
        LstActi = (List<Actividades>)Session[Usuario.codusuario + "DatosActividad"];

        if (conseID > 0)
        {
            try
            {
                foreach (Actividades acti in LstActi)
                {
                    if (acti.idactividad == conseID)
                    {

                        ActiServices.EliminarActividadPersona(conseID, (Usuario)Session["usuario"]);
                        LstActi.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstActi.RemoveAt((gvActividades.PageIndex * gvActividades.PageSize) + e.RowIndex);
        }

        gvActividades.DataSourceID = null;
        gvActividades.DataBind();

        gvActividades.DataSource = LstActi;
        gvActividades.DataBind();

        Session[Usuario.codusuario + "DatosActividad"] = LstActi;
    }

    protected void btnDetalle_Click(object sender, EventArgs e)
    {
        ObtenerListaActividades();
        List<Actividades> lstActividades = new List<Actividades>();


        if (Session[Usuario.codusuario + "DatosActividad"] != null)
        {
            lstActividades = (List<Actividades>)Session[Usuario.codusuario + "DatosActividad"];

            for (int i = 1; i <= 1; i++)
            {
                Actividades eActi = new Actividades();
                eActi.idactividad = -1;
                eActi.fecha_realizacion = null;
                eActi.tipo_actividad = null;
                eActi.descripcion = "";
                eActi.participante = null;
                eActi.calificacion = "";
                eActi.duracion = "";
                lstActividades.Add(eActi);
            }
            //gvActividades.PageIndex = gvActividades.PageCount;
            gvActividades.DataSource = lstActividades;
            gvActividades.DataBind();

            Session[Usuario.codusuario + "DatosActividad"] = lstActividades;
        }
    }

    #endregion


    #region Informacion Financiera


    protected void InicializarCuentasBan()
    {
        List<CuentasBancarias> lstCuentasBan = new List<CuentasBancarias>();
        for (int i = gvCuentasBancarias.Rows.Count; i < 5; i++)
        {
            CuentasBancarias eCuenta = new CuentasBancarias();
            eCuenta.idcuentabancaria = -1;
            eCuenta.tipo_cuenta = null;
            eCuenta.numero_cuenta = "";
            eCuenta.cod_banco = null;
            eCuenta.sucursal = "";
            eCuenta.fecha_apertura = null;
            eCuenta.principal = null;
            lstCuentasBan.Add(eCuenta);
        }
        gvCuentasBancarias.DataSource = lstCuentasBan;
        gvCuentasBancarias.DataBind();

        Session[Usuario.codusuario + "DatosCuentaBanc"] = lstCuentasBan;
    }

    protected void btnAgregarFila_Click(object sender, EventArgs e)
    {
        ObtenerListaCuentaBanc();

        List<CuentasBancarias> lstBene = new List<CuentasBancarias>();

        if (Session[Usuario.codusuario + "DatosCuentaBanc"] != null)
        {
            lstBene = (List<CuentasBancarias>)Session[Usuario.codusuario + "DatosCuentaBanc"];

            for (int i = 1; i <= 1; i++)
            {
                CuentasBancarias eCuenta = new CuentasBancarias();
                eCuenta.idcuentabancaria = -1;
                eCuenta.tipo_cuenta = null;
                eCuenta.numero_cuenta = "";
                eCuenta.cod_banco = null;
                eCuenta.sucursal = "";
                eCuenta.fecha_apertura = null;
                eCuenta.principal = null;
                lstBene.Add(eCuenta);
            }
            gvCuentasBancarias.DataSource = lstBene;
            gvCuentasBancarias.DataBind();

            Session[Usuario.codusuario + "DatosCuentaBanc"] = lstBene;
        }
    }

    protected void gvCuentasBancarias_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList ddltipoProducto = (DropDownList)e.Row.FindControl("ddltipoProducto");
        DropDownList ddlentidad = (DropDownList)e.Row.FindControl("ddlentidad");

        if (ddltipoProducto != null)
        {
            ddltipoProducto.Items.Insert(0, new ListItem("AHORROS", "0"));
            ddltipoProducto.Items.Insert(1, new ListItem("CORRIENTE", "1"));
            ddltipoProducto.SelectedIndex = 0;
            ddltipoProducto.DataBind();
        }

        if (ddlentidad != null)
        {
            Xpinn.Caja.Services.BancosService bancoService = new Xpinn.Caja.Services.BancosService();
            Xpinn.Caja.Entities.Bancos banco = new Xpinn.Caja.Entities.Bancos();
            ddlentidad.DataSource = bancoService.ListarBancos(banco, (Usuario)Session["usuario"]);
            ddlentidad.DataTextField = "nombrebanco";
            ddlentidad.DataValueField = "cod_banco";
            ddlentidad.Items.Insert(0, new ListItem("<Seleccione un Item>", "0"));
            ddlentidad.SelectedIndex = 0;
            ddlentidad.DataBind();
        }

        Label lbltipoProducto = (Label)e.Row.FindControl("lbltipoProducto");
        if (lbltipoProducto != null)
            ddltipoProducto.SelectedValue = lbltipoProducto.Text;

        Label lblentidad = (Label)e.Row.FindControl("lblentidad");
        if (lblentidad != null)
            ddlentidad.SelectedValue = lblentidad.Text;

    }

    protected void gvCuentasBancarias_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int conseID = Convert.ToInt32(gvCuentasBancarias.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaCuentaBanc();

        List<CuentasBancarias> LstCuentas;
        LstCuentas = (List<CuentasBancarias>)Session[Usuario.codusuario + "DatosCuentaBanc"];

        if (conseID > 0)
        {
            try
            {
                foreach (CuentasBancarias acti in LstCuentas)
                {
                    if (acti.idcuentabancaria == conseID)
                    {
                        ActiServices.EliminarCuentasBancarias(conseID, (Usuario)Session["usuario"]);
                        LstCuentas.Remove(acti);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstCuentas.RemoveAt((gvCuentasBancarias.PageIndex * gvCuentasBancarias.PageSize) + e.RowIndex);
        }
        gvCuentasBancarias.DataSourceID = null;
        gvCuentasBancarias.DataBind();

        gvCuentasBancarias.DataSource = LstCuentas;
        gvCuentasBancarias.DataBind();

        Session[Usuario.codusuario + "DatosCuentaBanc"] = LstCuentas;
    }

    protected List<CuentasBancarias> ObtenerListaCuentaBanc()//Int64 cod
    {
        List<CuentasBancarias> lstListaCuenta = new List<CuentasBancarias>();
        List<CuentasBancarias> lista = new List<CuentasBancarias>();

        foreach (GridViewRow rfila in gvCuentasBancarias.Rows)
        {
            CuentasBancarias eCuenta = new CuentasBancarias();
            Label lblidcuentabancaria = (Label)rfila.FindControl("lblidcuentabancaria");
            if (lblidcuentabancaria != null)
                eCuenta.idcuentabancaria = Convert.ToInt32(lblidcuentabancaria.Text);

            DropDownListGrid ddltipoProducto = (DropDownListGrid)rfila.FindControl("ddltipoProducto");
            if (ddltipoProducto.SelectedValue != null)
                eCuenta.tipo_cuenta = Convert.ToInt32(ddltipoProducto.SelectedValue);

            TextBox txtnum_Producto = (TextBox)rfila.FindControl("txtnum_Producto");
            if (txtnum_Producto != null)
                eCuenta.numero_cuenta = Convert.ToString(txtnum_Producto.Text.Trim());

            DropDownListGrid ddlentidad = (DropDownListGrid)rfila.FindControl("ddlentidad");
            if (ddlentidad.SelectedValue != null)
                eCuenta.cod_banco = Convert.ToInt32(ddlentidad.SelectedValue);

            TextBox txtsucursal = (TextBox)rfila.FindControl("txtsucursal");
            if (txtsucursal != null)
                if (txtsucursal.Text != "")
                    eCuenta.sucursal = Convert.ToString(txtsucursal.Text.ToUpper());
                else
                    eCuenta.sucursal = null;

            fecha txtfecha = (fecha)rfila.FindControl("txtfecha");
            if (txtfecha != null)
                if (txtfecha.Text != "")
                    eCuenta.fecha_apertura = txtfecha.ToDateTime;
                else
                    eCuenta.fecha_apertura = null;
            else
                eCuenta.fecha_apertura = null;
            CheckBoxGrid cbSeleccionar = (CheckBoxGrid)rfila.FindControl("cbSeleccionar");
            if (cbSeleccionar.Checked == true)
                eCuenta.principal = 1;
            else
                eCuenta.principal = 0;

            lista.Add(eCuenta);

            if (eCuenta.numero_cuenta != "" && eCuenta.cod_banco.Value != 0)
            {
                lstListaCuenta.Add(eCuenta);
            }
        }
        Session[Usuario.codusuario + "DatosCuentaBanc"] = lista;
        return lstListaCuenta;
    }


    #endregion


    Xpinn.FabricaCreditos.Services.EstadosFinancierosService EstadosFinancierosServicio = new Xpinn.FabricaCreditos.Services.EstadosFinancierosService();
    Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

    private Boolean validarDatos()
    {
        VerError("");

        if (txtIdentificacionE.Text == "")
        {
            VerError("Ingrese el Nro de Identificación - Datos Básicos");
            return false;
        }
        if (txtCod_oficina.SelectedValue == "" || txtCod_oficina.SelectedIndex == 0)
        {
            VerError("Debe seleccionar la Oficina - Datos Básicos");
            txtCod_oficina.Focus();
            return false;
        }
        //if (ddlLugarExpedicion.SelectedValue == "" || ddlLugarExpedicion.SelectedIndex == 0)
        //{
        //    VerError("Debe seleccionar el lugar de expedición - Datos Básicos");
        //    ddlLugarExpedicion.Focus();
        //    return false;
        //}
        if (txtPrimer_nombreE.Text == "")
        {
            VerError("Ingrese el primer nombre - Datos Básicos");
            txtPrimer_nombreE.Focus();
            return false;
        }
        if (txtPrimer_apellidoE.Text == "")
        {
            VerError("Ingrese el primer apellido - Datos Básicos");
            txtPrimer_apellidoE.Focus();
            return false;
        }
        if (ddlCiuCorrespondencia.SelectedValue == "" || ddlCiuCorrespondencia.SelectedIndex == 0)
        {
            VerError("Debe seleccionar la ciudad de correspondencia - Datos Básicos");
            ddlCiuCorrespondencia.Focus();
            return false;
        }
        if (ddlBarrioCorrespondencia.SelectedValue == "" || ddlBarrioCorrespondencia.SelectedIndex == 0)
        {
            VerError("Debe seleccionar el barrio de correspondencia - Datos Básicos");
            ddlBarrioCorrespondencia.Focus();
            return false;
        }
        if (txtTelCorrespondencia.Text == "")
        {
            VerError("Ingrese el telefono de correspondencia - Datos de Localización");
            txtTelCorrespondencia.Focus();
            return false;
        }
        if (txtCelular.Text == "")
        {
            VerError("Ingrese el celular - Datos Generales");
            txtCelular.Focus();
            return false;
        }
        //if (ddlActividadE.SelectedValue == "" || ddlActividadE.SelectedIndex == 0)
        //{
        //    VerError("Ingrese la actividad - Datos Generales");
        //    ddlActividadE.Focus();
        //    return false;
        //}
        if (Convert.ToDateTime(txtFechanacimiento.Text) > DateTime.Now)
        {
            VerError("La fecha de nacimiento no puede ser mayor a la fecha actual");
            txtFechanacimiento.Focus();
            return false;
        }
        //if (txtEdadCliente.Text == "")
        //{
        //    if (Convert.ToInt32(txtEdadCliente.Text) < 0 && Convert.ToInt32(txtEdadCliente.Text) > 90)
        //    {
        //        VerError("La Edad del cliente debe estar entre 0 y 90 años, Verifique la fecha de Nacimiento Ingresada");
        //        return false;
        //    }
        //}
        //if (ddlLugarNacimiento.Text == "" || ddlLugarNacimiento.SelectedIndex == 0)
        //{
        //    VerError("Ingrese la ciudad de nacimiento - Datos Generales");
        //    ddlLugarNacimiento.Focus();
        //    return false;
        //}
        if (ddlNivelEscolaridad.Text == "" || ddlNivelEscolaridad.SelectedIndex == 0)
        {
            VerError("Ingrese el nivel de escolaridad - Datos Generales");
            ddlNivelEscolaridad.Focus();
            return false;
        }
        if (ddlEstadoCivil.Text == "" || ddlEstadoCivil.SelectedIndex == 0)
        {
            VerError("Ingrese el estado civil - Datos Generales");
            ddlEstadoCivil.Focus();
            return false;
        }
        if (txtPersonasCargo.Text == "")
        {
            VerError("Ingrese el número de personas a cargo - Datos Generales");
            txtPersonasCargo.Focus();
            return false;
        }
        if (txtEstrato.Text == "")
        {
            VerError("Ingrese el estrato - Datos Generales");
            txtEstrato.Focus();
            return false;
        }

        if (txtEmail.Text != "")
        {
            bool rpta = IsValidEmail(txtEmail.Text);
            if (rpta == false)
            {
                VerError("El Email ingresado no tiene el Formato Correcto - Información Laboral");
                return false;
            }
        }
        if (ddlOcupacion.SelectedItem != null)
        {
            if (ddlOcupacion.SelectedItem.Text == "Empleado" && ddlOcupacion.SelectedValue == "1")
            {
                //if (txtEmpresa.Text == "")
                //{
                //    VerError("Ingrese la empresa - Información Laboral");
                //    txtEmpresa.Focus();
                //    return false;
                //}
                if (txtFechaIngreso.Text == "")
                {
                    VerError("Ingrese la fecha de Ingreso - Información Laboral");
                    return false;
                }
            }
        }

        //Validar los datos de Afiliacion
        if (chkAsociado.Checked)
        {
            if (txtFechaAfili.Text == "")
            {
                VerError("Ingrese la fecha de Afiliación - Afiliación");
                txtFechaAfili.Focus();
                return false;
            }
            if (ddlEstadoAfi.SelectedIndex == 0)
            {
                VerError("Seleccione el Estado de Afiliación - Afiliación");
                ddlEstadoAfi.Focus();
                return false;
            }
            if (txtValorAfili.Text == "")
            {
                VerError("Ingrese el Valor de Afiliación - Afiliación");
                txtValorAfili.Focus();
                return false;
            }
            if (Convert.ToInt64(txtValorAfili.Text.Replace(".", "")) < 0)
            {
                VerError("Ingrese el Valor de Afiliación - Afiliación");
                txtValorAfili.Focus();
                return false;
            }
            if (ddlFormaPago.SelectedItem.Value == "2" || ddlFormaPago.SelectedItem.Text == "Nomina")
            {
                if (ddlEmpresa.SelectedIndex == 0)
                {
                    if (ddlEmpresa.Items.Count == 1)
                    {
                        VerError("Debe seleccionar por lo menos una empresa de Recaudo - Afiliación");
                        return false;
                    }
                    else
                    {
                        VerError("Seleccione una empresa - Afiliación");
                        return false;
                    }
                }
            }
            if (ddlEstadoAfi.SelectedValue != "A")
            {
                if (txtFechaRetiro.Text == "")
                {
                    VerError("Ingrese la Fecha de Retiro - Afiliación");
                    return false;
                }
            }

            AfiliacionServices AfiliacionServicio = new AfiliacionServices();
            Afiliacion pAfiliacion = AfiliacionServicio.ConsultarCierrePersonas((Usuario)Session["usuario"]);

            if (Convert.ToDateTime(txtFechaAfili.Texto) != DateTime.MinValue && idObjeto == "")
            {
                //Validar fecha de cierre de personas  
                if (Convert.ToDateTime(txtFechaAfili.Texto) < pAfiliacion.fecha_cierre && pAfiliacion.estadocierre == "D")
                {
                    VerError("No se pueden registrar afiliaciones en periodos ya cerrados");
                    return false;
                }
            }
            AporteServices aporteServicio = new AporteServices();
            List<Aporte> lstAporte = new List<Aporte>();
            List<Aporte> lstAportesActivos = new List<Aporte>();

            if (idObjeto != "")
            {
                //Agregado para validar que no se modifique la fecha de afiliación si el asociado tiene aportes con saldo
                DateTime? fecAfiliacion = AfiliacionServicio.FechaAfiliacion(txtCod_persona.Text, (Usuario)Session["usuario"]);
                lstAporte = aporteServicio.ListarEstadoCuentaAportestodos(Convert.ToInt64(txtCod_persona.Text), "1,2", DateTime.Now, (Usuario)Session["usuario"]);
                lstAportesActivos = lstAporte.Where(x => x.Saldo > 0).ToList();
                if (lstAportesActivos.Count > 0 && fecAfiliacion != Convert.ToDateTime(txtFechaAfili.Text))
                {
                    VerError("La fecha de afiliación del asociado no se puede modificar, tiene aportes activos");
                    txtFechaAfili.Text = Convert.ToDateTime(fecAfiliacion).ToShortDateString();
                    return false;
                }
            }
        }

        if (chkConyuge.Checked)
        {
            if (txtnombre1_cony.Text == "")
            {
                VerError("Ingrese el Nombre del Conyuge - Información del Conyuge");
                return false;
            }
            if (txtapellido1_cony.Text == "")
            {
                VerError("Ingrese el Primer Apellido del Conyuge - Información del Conyuge");
                return false;
            }
            if (txtIdent_cony.Text == "")
            {
                VerError("Ingrese la Identificación del Conyuge - Información del Conyuge");
                return false;
            }
            if (txtIdentificacionE.Text.Trim() == txtIdent_cony.Text.Trim())
            {
                VerError("La identificación del conyuge no puede ser igual a la del asociado - Información del Conyuge");
                return false;
            }
            if (txtfechaNac_Cony.Text == "")
            {
                VerError("Ingrese la fecha de Nacimiento - Información del Conyuge");
                return false;
            }
            if (txtemail_cony.Text != "")
            {
                bool rpta = IsValidEmail(txtemail_cony.Text);
                if (rpta == false)
                {
                    VerError("El Email del Conyugue no tiene el Formato Correcto");
                    return false;
                }
            }
        }

        // SI(ES PEPS)  debe ser obligatorio la captura del parentesco hasta segundo grado.
        if (chkPEPS.Checked)
        {
            List<PersonaParentescos> listaParentesco = RecorrerGrillaParentescos(true);
            if (listaParentesco == null || listaParentesco.Count <= 0)
            {
                VerError("Si la persona esta marcada como es PEPS, debe registrar al menos un parentesco");
                return false;
            }
            if (txtCargoPEPS.SelectedValue == "0")
            {
                VerError("Si la persona esta marcada como PEPS debe registrar el cargo");
                return false;
            }
            if (!txtFechaVinculacionPEPS.TieneDatos)
            {
                VerError("Si la persona esta marcada como PEPS debe registrar la fecha de vinculación al cargo");
                return false;
            }
        }
        if (ddlparentesco.SelectedValue == "1" || ddlFamiliarPEPS.SelectedValue == "1" || ddlFamiliarAdmin.SelectedValue == "1" || ddlFamiliarControl.SelectedValue == "1")
        {
            List<PersonaParentescos> listaParentesco = RecorrerGrillaParentescos(true);
            if (listaParentesco == null || listaParentesco.Count <= 0)
            {
                VerError("Debe registrar al menos un familiar en el anexo de parentesco");
                return false;
            }
        }


        LineaAporte inflineaaport = new LineaAporte();
        inflineaaport = _afiliacionServicio.ConsultarLineaObligatoria(Usuario);

        if (rblTipo_persona.SelectedValue == "Natural")
        {
            if (inflineaaport.cod_linea_aporte != 0 && inflineaaport.tipo_cuota == 5)
            {
                if (txtsueldo_soli.Text == "")
                {
                    txtsueldo_soli.Focus();
                    VerError("Ingresar el valor del salario para calculo de cuota de aporte - Información Económica");
                    return false;
                }
            }
        }

        if (txtFechanacimiento.Text == "" && txtEdadCliente.Text == "")
        {
            VerError("Debe ingresar la fecha de nacimiento de la persona");
        }
        else if (Convert.ToInt64(txtEdadCliente.Text) > 0)
        {
            //Consultar parametro general de mayoria de edad
            Xpinn.Comun.Services.GeneralService generalServicio = new Xpinn.Comun.Services.GeneralService();
            General pGeneral = new General();
            pGeneral = generalServicio.ConsultarGeneral(104, (Usuario)Session["usuario"]);
            if (pGeneral.valor != null)
            {
                if (Convert.ToInt64(txtEdadCliente.Text) < Convert.ToInt64(pGeneral.valor))
                {
                    VerError("La persona debe ser mayor de edad");
                    return false;
                }
            }
        }
        ObtenerListaBeneficiarios();
        List<Beneficiario> lstBene = new List<Beneficiario>();
        lstBene = (List<Beneficiario>)ViewState[Usuario.codusuario + "DatosBene"];
        int porcentaje = 0;
        porcentaje = Convert.ToInt32(lstBene.Where(x => x.porcentaje_ben > 0).Sum(x => x.porcentaje_ben));
        if (porcentaje < 100)
        {
            VerError("Debe ocupar el 100% de sus beneficiarios.");
            return false;
        }
        return true;
    }

    protected void BtnGuardar_Click(object sender, EventArgs e)
    {
        VerError("");
        bool editar = false;
        try
        {
            if (validarDatos())
            {
                bool redireccionImagen = false;

                //if (!panelRiesgo.Visible)
                //{
                #region GRABAR DATOS DE LA PERSONA               

                Usuario pUsuario = new Usuario();
                pUsuario = (Usuario)Session["usuario"];

                if (idObjeto != "")
                {
                    // Consultar datos ya existentes de la persona si se va a modificar
                    vPersona1 = persona1Servicio.ConsultarPersona1(Convert.ToInt64(idObjeto), pUsuario);
                    editar = true;
                }
                else
                {
                    // Validar que la persona no exista si se va a crear
                    vPersona1 = persona1Servicio.ConsultaDatosPersona(txtIdentificacionE.Text, pUsuario);
                    if (vPersona1.cod_persona != 0)
                    {
                        VerError("Ya existe una persona con la identificación dada");
                        return;
                    }
                }

                if (txtDigito_verificacion.Text != "") vPersona1.digito_verificacion = Convert.ToInt64(txtDigito_verificacion.Text.Trim());

                if (txtCod_persona.Text != "") vPersona1.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());
                vPersona1.origen = "Contabilidad";

                //DATOS DE LA PERSONA
                vPersona1.identificacion = (txtIdentificacionE.Text != "") ? Convert.ToString(txtIdentificacionE.Text.Trim()) : String.Empty;
                if (ddlTipoE.Text != "") vPersona1.tipo_identificacion = Convert.ToInt64(ddlTipoE.SelectedValue);
                vPersona1.sexo = (rblSexo.SelectedItem != null) ? Convert.ToString(rblSexo.SelectedValue) : null;
                vPersona1.primer_nombre = txtPrimer_nombreE.Text.ToUpper();
                vPersona1.segundo_nombre = (txtSegundo_nombreE.Text != "") ? Convert.ToString(txtSegundo_nombreE.Text.Trim().ToUpper()) : String.Empty;
                vPersona1.primer_apellido = txtPrimer_apellidoE.Text.ToUpper();
                vPersona1.segundo_apellido = (txtSegundo_apellidoE.Text != "") ? Convert.ToString(txtSegundo_apellidoE.Text.Trim().ToUpper()) : String.Empty;
                if (string.Equals(rblTipo_persona.Text, "Jurídica"))
                    vPersona1.tipo_persona = "J";
                else
                    vPersona1.tipo_persona = "N";
                vPersona1.cod_oficina = Convert.ToInt64(txtCod_oficina.SelectedValue.Trim());
                if (txtFechaexpedicion.Text != "") vPersona1.fechaexpedicion = Convert.ToDateTime(txtFechaexpedicion.Text.Trim()); else vPersona1.fechaexpedicion = DateTime.MinValue;
                if (ddlLugarExpedicion.SelectedIndex > 0)
                    vPersona1.codciudadexpedicion = Convert.ToInt64(ddlLugarExpedicion.SelectedValue);
                else
                    vPersona1.codciudadexpedicion = null;

                if (ddlPais.SelectedIndex > 0)
                    vPersona1.nacionalidad = Convert.ToInt64(ddlPais.SelectedValue);
                else
                    vPersona1.nacionalidad = null;

                //INFORMACION RESIDENCIA
                if (ddlTipoUbic.SelectedIndex > 0)
                    vPersona1.ubicacion_residencia = Convert.ToInt32(ddlTipoUbic.SelectedValue);
                else
                    vPersona1.ubicacion_residencia = 0;

                vPersona1.direccion = txtDireccionE.Text.ToUpper();
                vPersona1.telefono = txtTelefonoE.Text;

                if (ddlBarrioResid.SelectedIndex > 0)
                    vPersona1.barrioResidencia = Convert.ToInt64(ddlBarrioResid.SelectedValue);
                else
                    vPersona1.barrioResidencia = 0;

                if (ddlCiudadResidencia.SelectedIndex > 0)
                    vPersona1.codciudadresidencia = Convert.ToInt64(ddlCiudadResidencia.SelectedValue);
                else
                    vPersona1.codciudadresidencia = 0;


                //INFORMACION DE CORRESPONDENCIA
                if (ddlTipoUbicCorr.SelectedIndex > 0)
                    vPersona1.ubicacion_correspondencia = Convert.ToInt32(ddlTipoUbicCorr.SelectedValue);
                else
                    vPersona1.ubicacion_correspondencia = 0;

                vPersona1.dirCorrespondencia = txtDirCorrespondencia.Text.ToUpper();

                if (ddlBarrioCorrespondencia.SelectedIndex > 0)
                    vPersona1.barrioCorrespondencia = Convert.ToInt64(ddlBarrioCorrespondencia.SelectedValue);
                else
                    vPersona1.barrioCorrespondencia = 0;

                vPersona1.telCorrespondencia = txtTelCorrespondencia.Text;

                if (ddlCiuCorrespondencia.SelectedIndex > 0)
                    vPersona1.ciuCorrespondencia = Convert.ToInt64(ddlCiuCorrespondencia.SelectedValue);
                else
                    vPersona1.ciuCorrespondencia = 0;

                vPersona1.email = txtEmail.Text;

                //DATOS GENERALES
                vPersona1.celular = txtCelular.Text;
                //vPersona1.codactividadStr = ddlActividadE.SelectedValue;

                byte NumActiSeleccionadas = 0;
                bool ActPrincipalSeleccionada = false;
                Label lblCodigo;
                vPersona1.lstActEconomicasSecundarias = new List<Actividades>();
                foreach (GridViewRow rFila in gvActividadesCIIU.Rows)
                {
                    CheckBoxGrid chkSeleccionado = rFila.FindControl("chkSeleccionar") as CheckBoxGrid;
                    CheckBoxGrid chkPrincipal = rFila.FindControl("chkPrincipal") as CheckBoxGrid;
                    if (chkSeleccionado.Checked)
                    {
                        if (!chkPrincipal.Checked)
                        {
                            Actividades objActividad = new Actividades();
                            lblCodigo = rFila.FindControl("lbl_codigo") as Label;
                            objActividad.codactividad = lblCodigo.Text;
                            vPersona1.lstActEconomicasSecundarias.Add(objActividad);
                        }
                        else
                        {
                            Label lblDescripcion = rFila.FindControl("lbl_descripcion") as Label;
                            VerError("La actividad económica " + lblDescripcion.Text + " fue seleccionada tanto como principal como secundaria");
                            return;
                        }
                        NumActiSeleccionadas++;
                    }

                    if (chkPrincipal.Checked)
                    {
                        if (!ActPrincipalSeleccionada)
                        {
                            ActPrincipalSeleccionada = true;
                            lblCodigo = rFila.FindControl("lbl_codigo") as Label;
                            vPersona1.codactividadStr = lblCodigo.Text;
                        }
                        else
                        {
                            VerError("Ha seleccionado más de una actividad economica principal");
                            return;
                        }
                    }

                    if (NumActiSeleccionadas > 3)
                    {
                        VerError("Se han seleccionado mas de 3 actividades econocmicas secundarias");
                        return;
                    }
                }

                if (!ActPrincipalSeleccionada)
                {
                    VerError("La activida economica principal no fue seleccionada");
                    return;
                }

                if (txtFechanacimiento.Text == "")
                    vPersona1.fechanacimiento = null;
                else
                    vPersona1.fechanacimiento = Convert.ToDateTime(txtFechanacimiento.Text.Trim());

                vPersona1.codescolaridad = Convert.ToInt64(ddlNivelEscolaridad.SelectedValue);
                if (ddlLugarNacimiento.SelectedIndex > 0)
                    vPersona1.codciudadnacimiento = Convert.ToInt64(ddlLugarNacimiento.SelectedValue);
                else
                    vPersona1.codciudadnacimiento = null;
                vPersona1.codestadocivil = Convert.ToInt64(ddlEstadoCivil.SelectedValue);
                vPersona1.PersonasAcargo = txtPersonasCargo.Text != "" ? Convert.ToInt32(txtPersonasCargo.Text) : 0;
                if (txtProfecion.Text != "") vPersona1.profecion = txtProfecion.Text; else vPersona1.profecion = String.Empty;
                if (ddlOcupacion.SelectedItem != null)
                    vPersona1.ocupacionApo = ddlOcupacion.SelectedIndex != 0 ? Convert.ToInt32(ddlOcupacion.SelectedValue) : 0;
                else
                    vPersona1.ocupacionApo = 0;
                vPersona1.Estrato = txtEstrato.Text != "" ? Convert.ToInt32(txtEstrato.Text) : 0;

                vPersona1.tipovivienda = (rblTipoVivienda.Text != "") ? Convert.ToString(rblTipoVivienda.SelectedValue) : String.Empty;
                if (rblTipoVivienda.SelectedValue == "A")
                {
                    vPersona1.arrendador = (txtArrendador.Text != "") ? Convert.ToString(txtArrendador.Text.Trim().ToUpper()) : null;
                    vPersona1.telefonoarrendador = (txtTelefonoarrendador.Text != "") ? Convert.ToString(txtTelefonoarrendador.Text.Trim()) : null;
                    if (txtValorArriendo.Text != "") vPersona1.ValorArriendo = Convert.ToInt64(txtValorArriendo.Text.Trim().Replace(".", "")); else vPersona1.ValorArriendo = 0;
                }
                else
                {
                    vPersona1.arrendador = null;
                    vPersona1.telefonoarrendador = txtTelefonoarrendador.Text != "" && txtTelefonoarrendador.Text != null ? txtTelefonoarrendador.Text.Trim() : "";
                    vPersona1.ValorArriendo = 0;
                }
                vPersona1.antiguedadlugar = txtAntiguedadlugar.Text.Trim() != "" ? Convert.ToInt64(txtAntiguedadlugar.Text.Trim()) : 0;

                //INFORMACION LABORAL
                if (txtEmpresa.Text != "")
                    vPersona1.empresa = txtEmpresa.Text.ToUpper();
                else
                    vPersona1.empresa = null;
                vPersona1.nit_empresa = txtNitEmpresa.Text != null && txtNitEmpresa.Text != "" ? Convert.ToInt64(txtNitEmpresa.Text) : 0;
                vPersona1.tipo_empresa = ddlTipoEmpresa.SelectedIndex > 0 ? Convert.ToInt32(ddlTipoEmpresa.SelectedValue) : 0;
                //actividad economica empresa                    
                vPersona1.act_ciiu_empresa = !string.IsNullOrEmpty(hfActEmpresa.Value) ? hfActEmpresa.Value : null;

                if (ddlCargo.SelectedIndex != 0)
                    vPersona1.codcargo = Convert.ToInt64(ddlCargo.SelectedValue.Trim());
                else
                    vPersona1.codcargo = 0;
                vPersona1.telefonoempresa = txtTelefonoempresa.Text;

                try
                {
                    vPersona1.fecha_ingresoempresa = Convert.ToDateTime(txtFechaIngreso.Text);
                }
                catch
                {
                    vPersona1.fecha_ingresoempresa = DateTime.MinValue;
                }
                vPersona1.antiguedadlugarempresa = txtAntiguedadlugarEmpresa.Text.Trim() != "" ? Convert.ToInt64(txtAntiguedadlugarEmpresa.Text.Trim()) : 0;
                if (txtTelCell0.Text != "") vPersona1.CelularEmpresa = txtTelCell0.Text; else vPersona1.CelularEmpresa = String.Empty;
                if (Convert.ToString(ddlTipoContrato.SelectedItem) != "")
                    vPersona1.codtipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue);
                else
                    vPersona1.codtipocontrato = 0;
                vPersona1.ciudad = Convert.ToInt64(ddlCiu0.SelectedValue);
                vPersona1.ActividadEconomicaEmpresaStr = ddlActividadE0.SelectedValue;
                if (ddlSector.SelectedIndex != 0)
                {
                    vPersona1.sector = Convert.ToInt64(ddlSector.SelectedValue);
                }
                if (ddlZona.SelectedIndex != 0)
                {
                    vPersona1.zona = Convert.ToInt64(ddlZona.SelectedValue);
                }
                if (ddlparentesco.SelectedIndex != 0)
                {
                    vPersona1.relacionEmpleadosEmprender = Convert.ToInt32(ddlparentesco.SelectedValue);
                    //Se modificó para que el registro sea hecho en el anexo de PARENTESCOS
                    /*if (ddlparentesco.SelectedItem.Text == "SI")
                        if (txtnomFuncionario.Text != "")
                            vPersona1.nombre_funcionario = txtnomFuncionario.Text;
                        else
                            vPersona1.nombre_funcionario = null;
                    else
                        vPersona1.nombre_funcionario = null;*/
                }
                else
                {
                    vPersona1.relacionEmpleadosEmprender = 0;
                    vPersona1.nombre_funcionario = null;
                }
                //Si es familiar de un miembro de administración
                if (ddlFamiliarAdmin.SelectedIndex != 0)
                    vPersona1.parentesco_madminis = Convert.ToInt32(ddlFamiliarAdmin.SelectedValue);
                else
                    vPersona1.parentesco_madminis = 0;
                //Si es familiar de un miembro de control
                if (ddlFamiliarControl.SelectedIndex != 0)
                    vPersona1.parentesco_mcontrol = Convert.ToInt32(ddlFamiliarControl.SelectedValue);
                else
                    vPersona1.parentesco_mcontrol = 0;

                //Si es familiar de una PEP
                if (ddlFamiliarPEPS.SelectedIndex != 0)
                    vPersona1.parentesco_pep = Convert.ToInt32(ddlFamiliarPEPS.SelectedValue);
                else
                    vPersona1.parentesco_pep = 0;

                vPersona1.direccionempresa = txtDireccionEmpresa.Text.ToUpper();

                if (ddlTipoUbicEmpresa.SelectedIndex > 0)
                    vPersona1.ubicacion_empresa = Convert.ToInt32(ddlTipoUbicEmpresa.SelectedValue);
                else
                    vPersona1.ubicacion_empresa = null;

                vPersona1.cod_nomina_empleado = txtCodigoEmpleado.Text;
                vPersona1.empleado_entidad = chkEmpleadoEntidad.Checked ? 1 : 0;

                if (rblSexo.SelectedItem.Text == "F")
                    vPersona1.mujer_familia = chkMujerCabeFami.Checked ? 1 : 0;
                else
                    vPersona1.mujer_familia = -1;
                vPersona1.jornada_laboral = Convert.ToInt32(rblJornadaLaboral.SelectedValue);

                //VERIFICAR ------------                    
                vPersona1.razon_social = String.Empty;

                vPersona1.numHijos = 0;
                vPersona1.salario = 0;
                vPersona1.antiguedadLaboral = 0;
                //-----------------------

                //escalafon
                if (ddlescalafon.SelectedValue != "" && ddlescalafon.SelectedValue != null)
                {
                    vPersona1.idescalafon = Convert.ToInt32(ddlescalafon.SelectedValue);
                }
                else
                {
                    vPersona1.idescalafon = 0;
                }

                vPersona1.cod_asesor = pUsuario.codusuario;
                vPersona1.residente = (rblResidente.Text != "") ? Convert.ToString(rblResidente.SelectedValue) : String.Empty;
                if (txtFecha_residencia.Text != "") vPersona1.fecha_residencia = Convert.ToDateTime(txtFecha_residencia.Text.Trim()); else vPersona1.fecha_residencia = DateTime.MinValue;
                vPersona1.tratamiento = (txtTratamiento.Text != "") ? Convert.ToString(txtTratamiento.Text.Trim()) : String.Empty;

                vPersona1.estado = idObjeto == "" ? "A" : vPersona1.estado;

                vPersona1.lstBeneficiarios = new List<Beneficiario>();
                vPersona1.lstBeneficiarios = ObtenerListaBeneficiarios();
                if (chkPEPS.Checked && vPersona1.lstBeneficiarios.Count == 0)
                {
                    VerError("Esta afilicación debe tener por los menos un beneficiario");
                    return;
                }

                vPersona1.lstActividad = new List<Actividades>();
                vPersona1.lstActividad = ObtenerListaActividades();

                vPersona1.lstCuentasBan = new List<CuentasBancarias>();
                vPersona1.lstCuentasBan = ObtenerListaCuentaBanc();

                vPersona1.lstInformacion = new List<InformacionAdicional>();
                vPersona1.lstInformacion = ObtenerListaInformacionAdicional();

                vPersona1.lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
                vPersona1.lstEmpresaRecaudo = ObtenerListaEmpresaRecaudo();

                //Agregado para registro de moneda extranejera
                vPersona1.lstMonedaExt = new List<EstadosFinancieros>();
                vPersona1.lstMonedaExt = ObtenerListaMonedaExt();

                vPersona1.lstProductosFinExt = new List<EstadosFinancieros>();
                vPersona1.lstProductosFinExt = ObtenerListaProductosExt();

                // Cargando la imagen de la persona
                //if (fuFoto.PostedFile != null && fuFoto.PostedFile.FileName != "")
                //{
                //    byte[] myimage = new byte[fuFoto.PostedFile.ContentLength];
                //    HttpPostedFile Image = fuFoto.PostedFile;
                //    Image.InputStream.Read(myimage, 0, Convert.ToInt32(fuFoto.PostedFile.ContentLength));
                //    vPersona1.foto = new byte[fuFoto.PostedFile.ContentLength];
                //    vPersona1.foto = myimage; 
                //}
                if (hdFileName.Value != null)
                {
                    try
                    {
                        Stream stream = null;
                        /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
                        stream = File.OpenRead(Server.MapPath("Images\\") + Path.GetFileName(this.hdFileName.Value));
                        this.Response.Clear();
                        if (stream.Length > 2000000)
                        {
                            VerError("La imagen excede el tamaño máximo que es de " + 100000);
                            return;
                        }
                        using (BinaryReader br = new BinaryReader(stream))
                        {
                            vPersona1.foto = br.ReadBytes(Convert.ToInt32(stream.Length));
                        }
                    }
                    catch
                    {
                        vPersona1.foto = null;
                    }
                }

                PersonaResponsable pResponsable = new PersonaResponsable();
                if (idObjeto == "")
                {
                    try
                    {
                        vPersona1.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
                    }
                    catch
                    {
                        vPersona1.fechacreacion = System.DateTime.Now;
                    }
                    vPersona1.usuariocreacion = pUsuario.nombre;

                    String estado = "";
                    DateTime fechacierrehistorico;
                    String formato = gFormatoFecha;
                    DateTime Fechaafiliacion = DateTime.ParseExact(txtFechaAfili.Text, formato, CultureInfo.InvariantCulture);


                    Xpinn.FabricaCreditos.Entities.Afiliacion vafiliacion = new Xpinn.FabricaCreditos.Entities.Afiliacion();
                    vafiliacion = _afiliacionServicio.ConsultarCierrePersonas((Usuario)Session["usuario"]);
                    if (vafiliacion != null)
                    {
                        estado = vafiliacion.estadocierre;
                        fechacierrehistorico = Convert.ToDateTime(vafiliacion.fecha_cierre.ToString());

                        if (estado == "D" && Fechaafiliacion <= fechacierrehistorico)
                        {
                            VerError("NO PUEDE INGRESAR TRANSACCIONES EN PERIODOS YA CERRADOS, TIPO P,'PERSONAS'");
                            return;
                        }
                    }
                    try
                    {
                        persona1Servicio.CrearPersonaAporte(vPersona1, false, pResponsable, pUsuario);
                    }
                    catch (Exception ex)
                    {
                        VerError("Error al crear la persona. " + ex.Message);
                        return;
                    }
                    Session[Usuario.codusuario + "Cod_persona"] = vPersona1.cod_persona;
                }

                else
                {
                    vPersona1.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
                    vPersona1.usuultmod = pUsuario.nombre;

                    persona1Servicio.ModificarPersonaAporte(vPersona1, false, pResponsable, pUsuario);
                    Session[Usuario.codusuario + "Cod_persona"] = vPersona1.cod_persona;

                    if (CheckAct.Checked == true)
                    {
                        //registro CONTROL  ACTUALIZAR DATOS
                        Xpinn.Aportes.Services.PersonaActDatosServices PersonaActDatosServices = new Xpinn.Aportes.Services.PersonaActDatosServices();
                        Xpinn.Aportes.Entities.PersonaActDatos PersonaActDatosEnti = new Xpinn.Aportes.Entities.PersonaActDatos();

                        PersonaActDatosEnti.cod_persona = vPersona1.cod_persona;
                        PersonaActDatosEnti.cod_usua = pUsuario.codusuario;
                        PersonaActDatosEnti.fecha_act = DateTime.Now;


                        PersonaActDatosServices.CrearPersonaActDatos(PersonaActDatosEnti, pUsuario);

                    }
                }
                Session[persona1Servicio.CodigoPrograma + ".id"] = idObjeto;
                #endregion

                #region GRABADO DE INFORMACIÓN ECONÓMICA 
                RegistrarPostBack();
                Xpinn.FabricaCreditos.Entities.EstadosFinancieros InformacionFinanciera = new Xpinn.FabricaCreditos.Entities.EstadosFinancieros();
                if (txtsueldo_soli.Text != null && txtsueldo_soli.Text != "")
                    InformacionFinanciera.sueldo = Convert.ToDecimal(txtsueldo_soli.Text);
                if (txtsueldo_cony.Text != null && txtsueldo_cony.Text != "")
                    InformacionFinanciera.sueldoconyuge = Convert.ToDecimal(txtsueldo_cony.Text);
                if (txthonorario_soli.Text != null && txthonorario_soli.Text != "")
                    InformacionFinanciera.honorarios = Convert.ToDecimal(txthonorario_soli.Text);
                if (txthonorario_cony.Text != null && txthonorario_cony.Text != "")
                    InformacionFinanciera.honorariosconyuge = Convert.ToDecimal(txthonorario_cony.Text);
                if (txtarrenda_soli.Text != null && txtarrenda_soli.Text != "")
                    InformacionFinanciera.arrendamientos = Convert.ToDecimal(txtarrenda_soli.Text);
                if (txtarrenda_cony.Text != null && txtarrenda_cony.Text != "")
                    InformacionFinanciera.arrendamientosconyuge = Convert.ToDecimal(txtarrenda_cony.Text);
                if (txtotrosIng_soli.Text != null && txtotrosIng_soli.Text != "")
                    InformacionFinanciera.otrosingresos = Convert.ToDecimal(txtotrosIng_soli.Text);
                if (txtotrosIng_cony.Text != null && txtotrosIng_cony.Text != "")
                    InformacionFinanciera.otrosingresosconyuge = Convert.ToDecimal(txtotrosIng_cony.Text);
                decimal Total = 0;
                Total = InformacionFinanciera.sueldo + InformacionFinanciera.honorarios + InformacionFinanciera.arrendamientos + InformacionFinanciera.otrosingresos;
                if (hdtotalING_soli.Value != null && hdtotalING_soli.Value != "")
                {
                    if (hdtotalING_soli.Value != Total.ToString())
                        InformacionFinanciera.totalingreso = Total;
                    else
                        InformacionFinanciera.totalingreso = Convert.ToDecimal(hdtotalING_soli.Value);
                }

                decimal TotalCony = 0;
                TotalCony = InformacionFinanciera.sueldoconyuge + InformacionFinanciera.honorariosconyuge + InformacionFinanciera.arrendamientosconyuge + InformacionFinanciera.otrosingresosconyuge;
                if (hdtotalING_cony.Value != null && hdtotalING_cony.Value != "")
                {
                    if (hdtotalING_soli.Value != TotalCony.ToString())
                        InformacionFinanciera.totalingresoconyuge = TotalCony;
                    else
                        InformacionFinanciera.totalingresoconyuge = Convert.ToDecimal(hdtotalING_cony.Value);
                }

                if (txthipoteca_soli.Text != null && txthipoteca_soli.Text != "")
                    InformacionFinanciera.hipoteca = Convert.ToDecimal(txthipoteca_soli.Text);
                if (txthipoteca_cony.Text != null && txthipoteca_cony.Text != "")
                    InformacionFinanciera.hipotecaconyuge = Convert.ToDecimal(txthipoteca_cony.Text);
                if (txttarjeta_soli.Text != null && txttarjeta_soli.Text != "")
                    InformacionFinanciera.targeta_credito = Convert.ToDecimal(txttarjeta_soli.Text);
                if (txttarjeta_cony.Text != null && txttarjeta_cony.Text != "")
                    InformacionFinanciera.targeta_creditoconyuge = Convert.ToDecimal(txttarjeta_cony.Text);
                if (txtotrosPres_soli.Text != null && txtotrosPres_soli.Text != "")
                    InformacionFinanciera.otrosprestamos = Convert.ToDecimal(txtotrosPres_soli.Text);
                if (txtotrosPres_cony.Text != null && txtotrosPres_cony.Text != "")
                    InformacionFinanciera.otrosprestamosconyuge = Convert.ToDecimal(txtotrosPres_cony.Text);
                if (txtgastosFam_soli.Text != null && txtgastosFam_soli.Text != "")
                    InformacionFinanciera.gastofamiliar = Convert.ToDecimal(txtgastosFam_soli.Text);
                if (txtgastosFam_cony.Text != null && txtgastosFam_cony.Text != "")
                    InformacionFinanciera.gastofamiliarconyuge = Convert.ToDecimal(txtgastosFam_cony.Text);
                if (txtnomina_soli.Text == "")
                    InformacionFinanciera.decunomina = 0;
                else
                    InformacionFinanciera.decunomina = Convert.ToDecimal(txtnomina_soli.Text);
                if (txtnomina_cony.Text != null && txtnomina_cony.Text != "")
                    InformacionFinanciera.decunominaconyuge = Convert.ToDecimal(txtnomina_cony.Text);
                Total = 0;
                Total = InformacionFinanciera.hipoteca + InformacionFinanciera.targeta_credito + InformacionFinanciera.otrosprestamos + InformacionFinanciera.gastofamiliar +
                    InformacionFinanciera.decunomina;
                if (hdtotalEGR_soli.Value != null && hdtotalEGR_soli.Value != "")
                {
                    if (hdtotalEGR_soli.Value != Total.ToString())
                        InformacionFinanciera.totalegresos = Total;
                    else
                        InformacionFinanciera.totalegresos = Convert.ToDecimal(hdtotalEGR_soli.Value);
                }

                TotalCony = 0;
                TotalCony = InformacionFinanciera.hipotecaconyuge + InformacionFinanciera.targeta_creditoconyuge + InformacionFinanciera.otrosprestamosconyuge 
                    + InformacionFinanciera.gastofamiliarconyuge + InformacionFinanciera.decunominaconyuge;
                if (hdtotalEGR_cony.Value != null && hdtotalEGR_cony.Value != "")
                {
                    if (hdtotalEGR_cony.Value != TotalCony.ToString())
                        InformacionFinanciera.totalegresosconyuge = TotalCony;
                    else
                        InformacionFinanciera.totalegresosconyuge = Convert.ToDecimal(hdtotalEGR_cony.Value);
                }

                //Agregado para guardar información de activos, pasivos y patrimonio
                if (txtactivos_soli.Text != null && txtactivos_soli.Text != "")
                    InformacionFinanciera.TotAct = Convert.ToInt64(txtactivos_soli.Text);
                if (txtactivos_conyuge.Text != null && txtactivos_conyuge.Text != "")
                    InformacionFinanciera.TotActConyuge = Convert.ToInt64(txtactivos_conyuge.Text);
                if (txtpasivos_soli.Text != null && txtpasivos_soli.Text != "")
                    InformacionFinanciera.TotPas = Convert.ToInt64(txtpasivos_soli.Text);
                if (txtpasivos_conyuge.Text != null && txtpasivos_conyuge.Text != "")
                    InformacionFinanciera.TotPasConyuge = Convert.ToInt64(txtpasivos_conyuge.Text);
                if (txtpatrimonio_soli.Text != null && txtpatrimonio_soli.Text != "")
                    InformacionFinanciera.TotPat = Convert.ToInt64(txtpatrimonio_soli.Text);
                if (txtpatrimonio_conyuge.Text != null && txtpatrimonio_conyuge.Text != "")
                    InformacionFinanciera.TotPatConyuge = Convert.ToInt64(txtpatrimonio_conyuge.Text);

                InformacionFinanciera.conceptootros = txtConceptoOtros_soli.Text;
                InformacionFinanciera.conceptootrosconyuge = txtConceptoOtros_cony.Text;

                InformacionFinanciera.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "Cod_persona"].ToString());

                if (Session[Usuario.codusuario + "Cod_persona_conyuge"] != null)
                    InformacionFinanciera.cod_personaconyuge = Convert.ToInt64(Session[Usuario.codusuario + "Cod_persona_conyuge"].ToString());

                EstadosFinancierosServicio.guardarIngreEgre(InformacionFinanciera, (Usuario)Session["Usuario"]);

                #endregion

                #region GRABAR GEOREFERECIA.
                //try
                //{
                //    if (txtLatitud.Text != "" && txtLongitud.Text != "")
                //    {
                //        pGeo.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "Cod_persona"].ToString());

                //        pGeo.latitud = txtLatitud.Text;
                //        pGeo.longitud = txtLongitud.Text;

                //        if (idObjeto != "") // modificar
                //        {
                //            if (txtCodGeorefencia.Text != "")
                //            {
                //                pGeo.codgeoreferencia = Convert.ToInt64(txtCodGeorefencia.Text);
                //                pGeo.fecultmod = DateTime.Today;
                //                pGeo.usuultmod = pUsuario.nombre;

                //                Georeferencia.ModificarGeoreferencia(pGeo, (Usuario)Session["usuario"]);
                //            }
                //            else
                //            {
                //                pGeo.codgeoreferencia = 0;
                //                pGeo.fechacreacion = DateTime.Today;
                //                pGeo.usuariocreacion = pUsuario.nombre;

                //                Georeferencia.CrearGeoreferencia(pGeo, (Usuario)Session["usuario"]);
                //            }
                //        }
                //        else // grabar
                //        {
                //            pGeo.codgeoreferencia = 0;
                //            pGeo.fechacreacion = DateTime.Today;
                //            pGeo.usuariocreacion = pUsuario.nombre;

                //            Georeferencia.CrearGeoreferencia(pGeo, (Usuario)Session["usuario"]);
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    BOexcepcion.Throw(DatosClienteServicio.CodigoPrograma, "GRABAR_GEOREFERENCIA", ex);
                //}

                #endregion

                #region GRABAR AFILIACION

                if (chkAsociado.Checked)
                {
                    Grabar_Datos_afiliacion();

                    if (!string.IsNullOrWhiteSpace(txtFechaAfili.Text))
                    {
                        vPersona1.fecha_afiliacion = Convert.ToDateTime(txtFechaAfili.Text);
                    }
                }


                #endregion

                #region WORKMANAGEMENT

                // Codigo de tipo de informacion adicinal de WM
                string codigoInfoAdicionalBarCode = (string)ConfigurationManager.AppSettings["CodigoTipoInformacionAdicionalWorkManagement"];

                // Parametro para habilitar operaciones con WM
                General general = ConsultarParametroGeneral(45);
                if (general != null && general.valor.Trim() == "1" && !string.IsNullOrWhiteSpace(codigoInfoAdicionalBarCode))
                {
                    try
                    {
                        InterfazWorkManagement interfazWM = new InterfazWorkManagement(Usuario);

                        // Homologar de codigo de tipo de identificacion a la que maneja el WM
                        // Si parece estupido pero el WM no maneja codigo solo descripcion 
                        switch (vPersona1.tipo_identificacion)
                        {
                            case 1:
                                vPersona1.tipo_identificacion_descripcion = "Cedula de Ciudadania";
                                break;
                            case 2:
                                vPersona1.tipo_identificacion_descripcion = "Nit";
                                break;
                            case 3:
                                vPersona1.tipo_identificacion_descripcion = "Nit Extranjero";
                                break;
                            case 4:
                                vPersona1.tipo_identificacion_descripcion = "Cedula de Extranjeria";
                                break;
                            case 5:
                                vPersona1.tipo_identificacion_descripcion = "Tarjeta de Identidad";
                                break;
                        }

                        // Homologar nombre ciudad
                        if (vPersona1.codciudadresidencia.HasValue)
                        {
                            Persona1Service personaSer = new Persona1Service();
                            Persona1 departamentoBuscado = personaSer.BuscarDepartamentoPorCodigoCiudad(vPersona1.codciudadresidencia.Value, Usuario);

                            vPersona1.nombre_ciudad = departamentoBuscado.nombre_ciudad;
                        }

                        Tuple<bool, string, bool> response = interfazWM.InteractuarRegistroFormularioHistoriaAsociado(vPersona1);
                        redireccionImagen = true;

                        // Si fue exitoso entro
                        if (response.Item1 || response.Item2.Trim() != "")
                        {
                            InformacionAdicionalServices infoSer = new InformacionAdicionalServices();
                            InformacionAdicional pAdicional = new InformacionAdicional
                            {
                                cod_persona = vPersona1.cod_persona,
                                cod_infadicional = Convert.ToInt32(codigoInfoAdicionalBarCode),
                                valor = response.Item2
                            };

                            // Miro si creo o modifico
                            if (response.Item3)
                            {
                                // Creo la informacion adicional para esta persona con el barCode
                                pAdicional = infoSer.CrearPersona_InfoAdicional(pAdicional, Usuario);
                            }
                            else
                            {
                                // Modifica la informacion adicional para esta persona con el barCode
                                infoSer.ModificarPersona_InfoAdicional(pAdicional, Usuario);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        // Hacer algo si falla
                    }
                }

                #endregion

                #region GRABAR PERSONA PARENTESCOS

                List<PersonaParentescos> listaParentescos = RecorrerGrillaParentescos(filtrarValidos: true);

                foreach (PersonaParentescos parentesco in listaParentescos)
                {
                    parentesco.codigopersona = vPersona1.cod_persona;
                    if (parentesco.consecutivo > 0)
                    {
                        _afiliacionServicio.ModificarPersonaParentesco(parentesco, Usuario);
                    }
                    else
                    {
                        _afiliacionServicio.CrearPersonaParentesco(parentesco, Usuario);
                    }
                }

                #endregion

                #region GRABAR DATOS DEL CONYUGE
                if (chkConyuge.Checked)
                    GuardarConyuge();
                #endregion

                #region GUARDAR DOCUMENTOS PERSONA
                string[] lstArchivos = NombreSer();
                int count = 0;
                Imagenes pImagen = new Imagenes();
                foreach (GridViewRow rFila in gvDocumentos.Rows)
                {
                    Label txtNombreArchivo = (Label)rFila.FindControl("txtNombreArchivo");

                    string Validar = lstArchivos[count];
                    if (Validar != null)
                    {

                        Stream stream = null;
                        /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
                        stream = File.OpenRead(Server.MapPath("Images\\") + Path.GetFileName(lstArchivos[count]));
                        this.Response.Clear();
                        //if (stream.Length > 100000)
                        //{
                        //    VerError("La imagen excede el tamaño máximo que es de " + 100000);
                        //    return;
                        //}
                        using (BinaryReader br = new BinaryReader(stream))
                        {
                            pImagen.imagen = br.ReadBytes(Convert.ToInt32(stream.Length));
                        }

                        pImagen.idimagen = 0;

                        pImagen.tipo_imagen = Convert.ToInt32(ddlTipoE.SelectedItem.Value);
                        pImagen.cod_persona = vPersona1.cod_persona;
                        pImagen.fecha = DateTime.Now;
                        pImagen.imagenEsPDF = true;

                        if (txtNombreArchivo.Text == "0")
                        {
                            ImagenSERVICE.CrearImagenesPersona(pImagen, pUsuario);
                        }

                        //BOCredito.CrearDocSolicitud(pSolicitud);

                    }

                    count = count + 1;
                }
                #endregion

                /*}
                else
                {*/
                if (idObjeto != "")
                {
                    editar = true;
                }
                //

                Session[_afiliacionServicio.CodigoPrograma + ".nid"] = txtIdentificacionE.Text;

                // LIMPIANDO SESIONES
                if (Session[Usuario.codusuario + "Cod_persona_conyuge"] != null)
                    Session.Remove(Usuario.codusuario + "Cod_persona_conyuge");
                if (Session[Usuario.codusuario + "DatosActividad"] != null)
                    Session.Remove(Usuario.codusuario + "DatosActividad");
                if (ViewState[Usuario.codusuario + "DatosBene"] != null)
                    ViewState.Remove(Usuario.codusuario + "DatosBene");
                if (Session[Usuario.codusuario + "DatosCuentaBanc"] != null)
                    Session.Remove(Usuario.codusuario + "DatosCuentaBanc");

                if (redireccionImagen)
                {
                    ImagenesService imagenService = new ImagenesService();
                    if (Session[Usuario.codusuario + "Cod_persona"] != null)
                        Session[imagenService.CodigoPrograma.ToString() + ".cod_persona"] = Session[Usuario.codusuario + "Cod_persona"].ToString();
                    Navegar("../ImagenesPersona/Nuevo.aspx");
                }
                else if (editar == true)
                {
                    if (chkPEPS.Checked) //&& !panelRiesgo.Visible)
                    {
                        /*panelDAtaGeneral.Visible = false;
                        panelRiesgo.Visible = true;
                        txtIdentificacionRiesgo.Text = txtIdentificacionE.Text;
                        txtNombreRiesgo.Text = txtPrimer_nombreE.Text + (txtSegundo_nombreE.Text.Trim() != "" ? " " + txtSegundo_nombreE.Text : "") + " " + txtPrimer_apellidoE.Text + (txtSegundo_apellidoE.Text.Trim() != "" ? " " + txtSegundo_apellidoE.Text : "");*/
                        Session[_afiliacionServicio.CodigoPrograma + ".id"] = txtCod_persona.Text;
                        Session[_afiliacionServicio.CodigoPrograma + ".nuevo"] = idObjeto == "" ? true : false;
                        Navegar("../../GestionRiesgo/ConsultasOFAC/Nuevo.aspx");
                    }
                    else
                    {
                        Navegar("../Afiliaciones/Lista.aspx");
                    }
                }
                else if (chkAsociado.Checked) //Solamente si la persona es asociada, se redirecciona para registrar el aporte
                {
                    Session["cedula"] = txtIdentificacionE.Text;
                    Navegar("../CuentasAportes/Nuevo.aspx");
                }

                // Limpiar variable de sesion. 
                if (Session[Usuario.codusuario + "Cod_persona"] != null)
                    Session.Remove(Usuario.codusuario + "Cod_persona");

            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }


    }



    private void calcula_total_ING_Solicitante()
    {
        double SumTotal = 0;
        if (txtsueldo_soli.Text == "") txtsueldo_soli.Text = "0";
        if (txthonorario_soli.Text == "") txthonorario_soli.Text = "0";
        if (txtarrenda_soli.Text == "") txtarrenda_soli.Text = "0";
        if (txtotrosIng_soli.Text == "") txtotrosIng_soli.Text = "0";
        SumTotal = Convert.ToDouble(txtsueldo_soli.Text) + Convert.ToDouble(txthonorario_soli.Text) + Convert.ToDouble(txtarrenda_soli.Text) + Convert.ToDouble(txtotrosIng_soli.Text);
        txttotalING_soli.Text = Convert.ToString(SumTotal);
    }

    protected void txtsueldoSoli_TextChanged(object sender, EventArgs e)
    {
        calcula_total_ING_Solicitante();
    }

    public string posiionlon = "";
    public string posiionlat = "";
    private readonly object txtTotal_pasivos;

    void BuscarPosicionEmprender()
    {
        //double a, b;
        //TextBox1.Visible = true;
        //TextBox2.Visible = true;
        //if (posiionlat == "")
        //{
        //    try
        //    {
        //        a = Convert.ToDouble(TextBox1.Text.Replace(".", ","));
        //        b = Convert.ToDouble(TextBox2.Text.Replace(".", ","));
        //        txtLatitud.Text = Convert.ToString(a);
        //        txtLongitud.Text = Convert.ToString(b);
        //        InicializarGoogleMapsServer(a, b);
        //        mostarpuntomapa(a, b);
        //    }
        //    catch
        //    {

        //    }
        //}
        //else
        //{
        //}
    }

    private void InicializarGoogleMapsServer(double a, double b)
    {
        //        // centramos en Paraguay el mapa al iniciar
        //        GLatLng latlng = new GLatLng(4.60971, -74.08175);

        //        // centramos el mapa en las coordenadas
        //        gMap.setCenter(latlng, 14);

        //        // agregamos los controles de navegacion y zoom a los 3
        //        gMap.addControl(new GControl(GControl.preBuilt.LargeMapControl));

        //        // agregamos los listeners
        //        gMap.addListener(new GListener(gMap.GMap_Id, GListener.Event.zoomend,
        //        string.Format(@"
        //            function(oldLevel, newLevel)
        //            {{
        //              if ((newLevel > 7) || (newLevel < 4))
        //              {{
        //                  var ev = new serverEvent('AdvancedZoom', {0});
        //                  ev.addArg(newLevel);
        //                  ev.send();
        //              }}
        //            }}
        //            ", gMap.GMap_Id)));
    }

    protected string mostarpuntomapa(double a, double b)
    {
        //// creamos las coordenadas con el clic que hizo el usuario
        //GLatLng latlng = new GLatLng(a, b);
        //// limpiamos todos los marcadores del mapa
        //gMap.resetMarkers();
        //// creamos un marcador
        //GMarkerOptions mkrOpts = new GMarkerOptions();
        //// seteamos que no se pueda arrastrar, asi estara obligado a dar clic de nuevo si quiere cambiar
        //mkrOpts.draggable = false;
        //// creamos un marcador nuevo, con las coordenadas del usuario
        //GMarker marker = new GMarker(latlng, mkrOpts);
        //// agregamos el marcador al mapa
        //gMap.Add(marker);
        //// centramos el mapa con las coordenadas del usuario
        //gMap.setCenter(latlng);
        //// agregamos un tool tip para facilitar el entendimiento al usuario
        //marker.options.title = "Aqui se encuentra";
        //// retornamos vacio
        return string.Empty;
    }

    //protected string gMap_Click(object s, GAjaxServerEventArgs e)
    //{
    //// Mostramos las coordenadas
    //Response.Write("Sus Coordenadas son: \r\n Latitud: " + e.point.lat + "\r\n" + "Logitud: " + e.point.lng);
    //// creamos las coordenadas con el clic que hizo el usuario
    //GLatLng latlng = new GLatLng(e.point.lat, e.point.lng);
    //posiionlat = Convert.ToString(e.point.lat);
    //posiionlon = Convert.ToString(e.point.lng);

    //// limpiamos todos los marcadores del mapa
    //gMap.resetMarkers();
    //// creamos un marcador
    //GMarkerOptions mkrOpts = new GMarkerOptions();
    //// seteamos que no se pueda arrastrar, asi estara obligado a dar clic de nuevo si quiere cambiar
    //mkrOpts.draggable = false;
    //// creamos un marcador nuevo, con las coordenadas del usuario
    //GMarker marker = new GMarker(latlng, mkrOpts);
    //// agregamos el marcador al mapa
    //gMap.Add(marker);
    //// centramos el mapa con las coordenadas del usuario
    //gMap.setCenter(latlng);
    //// agregamos un tool tip para facilitar el entendimiento al usuario
    //marker.options.title = "Aqui se encuentra";
    //// retornamos vacio
    //txtLatitud.Text = posiionlat;
    //txtLongitud.Text = posiionlon;
    //return string.Empty;        
    //}

    protected void rblOpcion_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (rblOpcion.SelectedItem.Text == "Por Coordenadas")
        //{
        //    txtLatitud.Visible = true;
        //    txtLongitud.Visible = true;
        //    Session["BUSQUEDA"] = "1";
        //}
        //else // POR UBICACION
        //{
        //    Session["BUSQUEDA"] = "2";
        //    txtLatitud.Visible = false;
        //    txtLongitud.Visible = false;
        //}
        //BuscarDireccionPersona();
    }

    void BuscarDireccionPersona()
    {
        //try
        //{
        //    if (Session["BUSQUEDA"].ToString() == "1") // BUSQUEDA POR COORDENADAS
        //    {
        //        pGeo = Georeferencia.ConsultarGeoreferenciaXPERSONA(Convert.ToInt64(idObjeto), (Usuario)Session["usuario"]);
        //        double lati = 0, longi = 0;
        //        if (pGeo.latitud != "")
        //            lati = Convert.ToDouble(pGeo.latitud);
        //        if (pGeo.longitud != "")
        //            longi = Convert.ToDouble(pGeo.longitud);
        //        mostarpuntomapa(lati, longi);
        //}
        //    else // BUSQUEDA POR DIRECCION
        //    {
        //        string sMapKey = System.Configuration.ConfigurationManager.AppSettings.Get("googlemaps.Subgurim.net");
        //        GeoCode geocode = new GeoCode();
        //        string NEWDireccion;

        //        if (ddlCiuCorrespondencia.Text != "")
        //        {
        //            NEWDireccion = this.txtDirCorrespondencia.Text + " " + ddlCiuCorrespondencia.SelectedItem.Text + " COLOMBIA";
        //            geocode = GMap.geoCodeRequest(NEWDireccion, sMapKey);
        //        }
        //        else
        //        {
        //            geocode = GMap.geoCodeRequest(this.txtDirCorrespondencia.Text + " COLOMBIA", sMapKey);
        //        }

        //        if (geocode.valid == true)
        //        {
        //            //LATITUD
        //            this.txtLatitud.Text = geocode.Placemark.coordinates.lat.ToString();
        //            //LONGITUD
        //            this.txtLongitud.Text = geocode.Placemark.coordinates.lng.ToString();

        //            Subgurim.Controles.GLatLng gLatLng = new Subgurim.Controles.GLatLng(geocode.Placemark.coordinates.lat, geocode.Placemark.coordinates.lng);
        //            gMap.setCenter(gLatLng, 14, Subgurim.Controles.GMapType.GTypes.Normal);
        //            mostarpuntomapa(Convert.ToDouble(txtLatitud.Text), Convert.ToDouble(txtLongitud.Text));
        //        }
        //        else
        //        {
        //            VerError("No se puede encontrar la ubicación");
        //        }
        //    }            
        //}
        //catch (Exception ex)
        //{
        //    VerError("btnConsultaCiudad_Click-" + ex);
        //}

    }

    protected void btnConsultaCiudad_Click(object sender, ImageClickEventArgs e)
    {
        VerError("");
        Session["BUSQUEDA"] = "2";
        BuscarDireccionPersona();
    }

    void obtenerControlesAdicionales()
    {
        InformacionAdicionalServices informacion = new InformacionAdicionalServices();
        InformacionAdicional pInfo = new InformacionAdicional();
        List<InformacionAdicional> lstControles = new List<InformacionAdicional>();
        string tipo = "N";
        lstControles = informacion.ListarInformacionAdicional(pInfo, tipo, (Usuario)Session["usuario"]);
        if (lstControles.Count > 0)
        {
            gvInfoAdicional.DataSource = lstControles;
            gvInfoAdicional.DataBind();
        }
        ViewState.Add("ListaInfoAdicional", lstControles);
    }

    protected void gvInfoAdicional_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtCadena = (TextBox)e.Row.FindControl("txtCadena");
            TextBox txtNumero = (TextBox)e.Row.FindControl("txtNumero");
            fecha txtctlfecha = (fecha)e.Row.FindControl("txtctlfecha");

            DropDownListGrid ddlDropdown = (DropDownListGrid)e.Row.FindControl("ddlDropdown");

            //Llenando DropDown
            Label lblDropdown = (Label)e.Row.FindControl("lblDropdown");
            if (ddlDropdown != null)
            {
                string[] sDatos;
                sDatos = lblDropdown.Text.Split(',');
                if (sDatos.Count() > 0 && sDatos[0] != "")
                {
                    ddlDropdown.Items.Clear();
                    ddlDropdown.DataSource = sDatos;
                    ddlDropdown.DataBind();
                }
            }

            Label lblopcionaActivar = (Label)e.Row.FindControl("lblopcionaActivar");
            if (lblopcionaActivar != null)
            {
                if (lblopcionaActivar.Text == "1")//CARACTER
                {
                    txtCadena.Visible = true;
                }
                else if (lblopcionaActivar.Text == "2")//FECHA
                {
                    txtctlfecha.Visible = true;
                }
                else if (lblopcionaActivar.Text == "3") //NUMERO
                {
                    txtNumero.Visible = true;
                }
                else if (lblopcionaActivar.Text == "4") // DROPDOWNLIST
                {
                    ddlDropdown.Visible = true;
                }
            }

            //Capturando Valor del DropDown
            Label lblValorDropdown = (Label)e.Row.FindControl("lblValorDropdown");
            if (lblValorDropdown != null)
            {
                ddlDropdown.SelectedValue = lblValorDropdown.Text;
            }

            Label lblidinfadicional = (Label)e.Row.FindControl("lblcod_infadicional");
            if (lblValorDropdown != null)
            {
                // Codigo de tipo de informacion adicinal de WM
                string codigoInfoAdicionalBarCode = (string)ConfigurationManager.AppSettings["CodigoTipoInformacionAdicionalWorkManagement"];

                if (!string.IsNullOrWhiteSpace(codigoInfoAdicionalBarCode) && lblidinfadicional.Text.Trim() == codigoInfoAdicionalBarCode.Trim())
                {
                    txtCadena.Enabled = false;
                    txtNumero.Enabled = false;
                    txtctlfecha.Enabled = false;
                }
            }
        }
    }

    protected void gvInfoAdicional_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvInfoAdicional.PageIndex = e.NewPageIndex;
        if (ViewState["ListaInfoAdicional"] != null)
        {
            List<InformacionAdicional> LstInformacion = new List<InformacionAdicional>();
            LstInformacion = (List<InformacionAdicional>)ViewState["ListaInfoAdicional"];
            if (LstInformacion.Count > 0)
            {
                gvInfoAdicional.DataSource = LstInformacion;
                gvInfoAdicional.DataBind();
            }
        }
    }

    protected List<InformacionAdicional> ObtenerListaInformacionAdicional()
    {
        List<InformacionAdicional> lstInformacionAdd = new List<InformacionAdicional>();

        foreach (GridViewRow rfila in gvInfoAdicional.Rows)
        {
            InformacionAdicional eActi = new InformacionAdicional();

            Label lblidinfadicional = (Label)rfila.FindControl("lblidinfadicional");

            if (lblidinfadicional != null)
                eActi.idinfadicional = Convert.ToInt32(lblidinfadicional.Text);

            Label lblcod_infadicional = (Label)rfila.FindControl("lblcod_infadicional");
            if (lblcod_infadicional != null)
                eActi.cod_infadicional = Convert.ToInt32(lblcod_infadicional.Text);

            Label lblopcionaActivar = (Label)rfila.FindControl("lblopcionaActivar");

            if (lblopcionaActivar != null)
            {
                if (lblopcionaActivar.Text == "1")//CARACTER
                {
                    TextBox txtCadena = (TextBox)rfila.FindControl("txtCadena");
                    if (txtCadena != null)
                        eActi.valor = txtCadena.Text;
                }
                else if (lblopcionaActivar.Text == "2")//FECHA
                {
                    fecha txtctlfecha = (fecha)rfila.FindControl("txtctlfecha");
                    if (txtctlfecha != null)
                        eActi.valor = txtctlfecha.Text;
                }
                else if (lblopcionaActivar.Text == "3") //NUMERO
                {
                    TextBox txtNumero = (TextBox)rfila.FindControl("txtNumero");
                    if (txtNumero != null)
                        eActi.valor = txtNumero.Text;
                }
                else if (lblopcionaActivar.Text == "4") // DROPDOWNLIST
                {
                    DropDownListGrid ddlDropdown = (DropDownListGrid)rfila.FindControl("ddlDropdown");
                    if (ddlDropdown != null)
                        eActi.valor = ddlDropdown.SelectedItem.Text;
                    if (ddlDropdown.Text != "")
                        eActi.valor = ddlDropdown.SelectedItem.Text;
                }
            }

            if (eActi.valor != "" && eActi.cod_infadicional != 0)
            {
                lstInformacionAdd.Add(eActi);
            }
        }
        return lstInformacionAdd;
    }

    #region ACORDION AFILIACION


    void CargarDropdowEstado()
    {
        List<Estado_Persona> lstEstado = new List<Estado_Persona>();
        Estado_Persona pEntidad = new Estado_Persona();
        lstEstado = _afiliacionServicio.ListarEstadoPersona(pEntidad, (Usuario)Session["usuario"]);
        if (lstEstado.Count > 0)
        {
            ddlEstadoAfi.DataSource = lstEstado;
            ddlEstadoAfi.DataTextField = "descripcion";
            ddlEstadoAfi.DataValueField = "estado";
            ddlEstadoAfi.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlEstadoAfi.SelectedIndex = 0;
            ddlEstadoAfi.DataBind();
        }

        PoblarLista("periodicidad", ddlPeriodicidad);

        ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));

        EscalafonSalarialService escalafon = new EscalafonSalarialService();
        EscalafonSalarial escalaf = new EscalafonSalarial();
        List<EscalafonSalarial> lista = new List<EscalafonSalarial>();
        lista = escalafon.ListarEscalafonSalarial("", escalaf, (Usuario)Session["usuario"]);
        ddlescalafon.DataSource = lista;
        ddlescalafon.DataTextField = "grado";
        ddlescalafon.DataValueField = "idescalafon";
        ddlescalafon.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        ddlescalafon.SelectedIndex = 0;
        ddlescalafon.DataBind();
    }


    private void Grabar_Datos_afiliacion()
    {
        AfiliacionServices afiliacionService = new AfiliacionServices();

        Afiliacion afili = new Afiliacion();

        if (txtcodAfiliacion.Text != "")
            afili.idafiliacion = Convert.ToInt64(txtcodAfiliacion.Text);
        else
            afili.idafiliacion = 0;

        afili.cod_persona = Convert.ToInt64(Session[Usuario.codusuario + "Cod_persona"].ToString());

        if (txtFechaAfili.Text != "")
            afili.fecha_afiliacion = Convert.ToDateTime(txtFechaAfili.Text);

        if (ddlEstadoAfi.SelectedIndex != 0)
            afili.estado = ddlEstadoAfi.SelectedValue;

        if (ddlEstadoAfi.SelectedValue == "A")
            afili.fecha_retiro = DateTime.MinValue;
        else
        {
            if (txtFechaRetiro.Text != "")
                afili.fecha_retiro = Convert.ToDateTime(txtFechaRetiro.Text);
            else
                afili.fecha_retiro = DateTime.MinValue;
        }

        if (txtValorAfili.Text != "0")
            afili.valor = Convert.ToDecimal(txtValorAfili.Text.Replace(".", ""));
        else
            afili.valor = 0;

        if (ddlFormaPago.SelectedValue != "")
            afili.forma_pago = Convert.ToInt32(ddlFormaPago.SelectedValue);
        else
            afili.forma_pago = 0;

        if (ddlEmpresa.Visible == true && ddlEmpresa.SelectedIndex != 0)
            afili.empresa_formapago = Convert.ToInt32(ddlEmpresa.SelectedValue);
        else
            afili.empresa_formapago = 0;

        if (txtFecha1Pago.Text != "")
            afili.fecha_primer_pago = Convert.ToDateTime(txtFecha1Pago.Text);
        else
            afili.fecha_primer_pago = DateTime.MinValue;

        if (txtCuotasAfili.Text != "")
            afili.cuotas = Convert.ToInt32(txtCuotasAfili.Text);
        else
            afili.cuotas = 0;

        if (ddlPeriodicidad.SelectedValue != "")
            afili.cod_periodicidad = Convert.ToInt32(ddlPeriodicidad.SelectedValue);
        else
            afili.cod_periodicidad = 0;

        if (chkAsistioUltAsamblea.Checked)
            afili.asist_ultasamblea = 1;
        else
            afili.asist_ultasamblea = 0;

        if (!String.IsNullOrWhiteSpace(txtNoAsistencias.Text))
            afili.numero_asistencias = Convert.ToInt32(txtNoAsistencias.Text);

        if (ddlAsociadosEspeciales.SelectedValue != "")
            afili.cod_asociado_especial = Convert.ToInt32(ddlAsociadosEspeciales.SelectedValue);

        afili.Es_PEPS = chkPEPS.Checked;
        panelPEPS.Visible = chkPEPS.Checked;
        afili.Administra_recursos_publicos = chkAdmiRecursosPublicos.Checked;
        afili.Miembro_administracion = chkMiembroAministracion.Checked;
        afili.Miembro_control = ChkMiembroControl.Checked;
        afili.cargo_PEPS = txtCargoPEPS.SelectedValue;
        afili.institucion = txtInstitucion.Text;
        if (txtFechaVinculacionPEPS.TieneDatos)
            afili.fecha_vinculacion_PEPS = txtFechaVinculacionPEPS.ToDateTime;
        if (txtFechaDesVinculacionPEPS.TieneDatos)
            afili.fecha_desvinculacion_PEPS = txtFechaDesVinculacionPEPS.ToDateTime;
        /*if (chkPEPS.Checked)
            afili.consultaRIESGO = Iframe1.InnerHtml;*/

        if (!string.IsNullOrWhiteSpace(ddlAsesor.SelectedValue))
        {
            afili.cod_asesor = Convert.ToInt64(ddlAsesor.SelectedValue);
        }

        afili.entidad_externa = txtOtraOS.Text != null && txtOtraOS.Text != "" ? txtOtraOS.Text : "";
        afili.cargo_directivo = txtCargosDirectivos.Text != null && txtCargosDirectivos.Text != "" ? txtCargosDirectivos.Text : "";

        if (txtcodAfiliacion.Text != "")
        {
            if (txtFechaAfili.Text != "" && ddlEstadoAfi.SelectedIndex != 0)
            {
                afiliacionService.ModificarPersonaAfiliacion(afili, (Usuario)Session["usuario"]);
            }
        }
        else
        {
            if (txtFechaAfili.Text != "" && ddlEstadoAfi.SelectedIndex != 0)
            {
                afiliacionService.CrearPersonaAfiliacion(afili, (Usuario)Session["usuario"]);
            }
        }
    }


    private void ObtenerDatosAfiliacion(Int64 cod_persona)
    {
        Afiliacion pAfili = new Afiliacion();
        chkAsociado.Checked = false;
        chkAsociado.Enabled = true;
        pAfili = _afiliacionServicio.ConsultarAfiliacion(cod_persona, (Usuario)Session["usuario"]);
        if (pAfili.idafiliacion != 0)
        {
            txtcodAfiliacion.Text = Convert.ToString(pAfili.idafiliacion);
            chkAsociado.Checked = true;
            chkAsociado.Enabled = false;
        }
        AfiliacionServices AfiliacionServicio = new AfiliacionServices();
        DateTime? fecAfiliacion = AfiliacionServicio.FechaAfiliacion(txtCod_persona.Text, (Usuario)Session["usuario"]);
        if (fecAfiliacion != null && fecAfiliacion != DateTime.MinValue)
            txtFechaAfili.Text = Convert.ToDateTime(fecAfiliacion).ToString(gFormatoFecha);
        else
            txtFechaAfili.Text = pAfili.fecha_afiliacion.ToString(gFormatoFecha);
        if (pAfili.estado != "")
            ddlEstadoAfi.SelectedValue = pAfili.estado;
        ddlEstadoAfi_SelectedIndexChanged(ddlEstadoAfi, null);
        if (pAfili.fecha_retiro != DateTime.MinValue)
            txtFechaRetiro.Text = Convert.ToString(pAfili.fecha_retiro.ToString(gFormatoFecha));
        if (pAfili.valor != 0)
            txtValorAfili.Text = Convert.ToString(pAfili.valor);
        if (pAfili.fecha_primer_pago != null)
            txtFecha1Pago.Text = Convert.ToString(pAfili.fecha_primer_pago.Value.ToString(gFormatoFecha));
        if (pAfili.cuotas != 0)
            txtCuotasAfili.Text = Convert.ToString(pAfili.cuotas);
        if (pAfili.cod_periodicidad != 0)
            ddlPeriodicidad.SelectedValue = Convert.ToString(pAfili.cod_periodicidad);
        if (pAfili.forma_pago != 0)
            ddlFormaPago.SelectedValue = pAfili.forma_pago.ToString();
        ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
        if (pAfili.empresa_formapago != 0 && pAfili.empresa_formapago != null)
            ddlEmpresa.SelectedValue = pAfili.empresa_formapago.ToString();
        if (pAfili.asist_ultasamblea != 0)
            chkAsistioUltAsamblea.Checked = true;
        if (pAfili.cod_asesor.HasValue)
        {
            ddlAsesor.SelectedValue = pAfili.cod_asesor.ToString();
        }
        if (pAfili.cod_asociado_especial.HasValue)
            ddlAsociadosEspeciales.SelectedValue = pAfili.cod_asociado_especial.Value.ToString();
        chkPEPS.Checked = pAfili.Es_PEPS;
        panelPEPS.Visible = chkPEPS.Checked;
        txtCargoPEPS.SelectedValue = pAfili.cargo_PEPS;
        txtInstitucion.Text = pAfili.institucion;
        if (pAfili.fecha_vinculacion_PEPS != null)
            txtFechaVinculacionPEPS.Text = Convert.ToDateTime(pAfili.fecha_vinculacion_PEPS).ToString(gFormatoFecha);
        if (pAfili.fecha_desvinculacion_PEPS != null)
            txtFechaDesVinculacionPEPS.Text = Convert.ToDateTime(pAfili.fecha_desvinculacion_PEPS).ToString(gFormatoFecha);
        chkAdmiRecursosPublicos.Checked = pAfili.Administra_recursos_publicos;
        chkAsociado_CheckedChanged(chkAsociado, null);
        txtNoAsistencias.Text = pAfili.numero_asistencias.ToString();
        if (Convert.ToString(pAfili.Miembro_administracion) != null)
        {
            chkMiembroAministracion.Checked = pAfili.Miembro_administracion;
        }
        if (Convert.ToString(pAfili.Miembro_control) != null)
        {
            ChkMiembroControl.Checked = pAfili.Miembro_control;
        }
        if (pAfili.entidad_externa != null && pAfili.entidad_externa != "")
        {
            txtOtraOS.Text = pAfili.entidad_externa;
            ckAfiliadoOtraOS.Checked = true;
            lblOtraOS.Visible = true;
            txtOtraOS.Visible = true;
        }

        if (pAfili.cargo_directivo != null && pAfili.cargo_directivo != "")
        {
            txtCargosDirectivos.Text = pAfili.cargo_directivo;
            ckCargosOS.Checked = true;
            txtCargosDirectivos.Visible = true;
            lblCargoOS.Visible = true;

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


    #endregion


    protected void cbSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid cbSeleccionar = (CheckBoxGrid)sender;
        int nItem = Convert.ToInt32(cbSeleccionar.CommandArgument);

        foreach (GridViewRow rFila in gvCuentasBancarias.Rows)
        {
            CheckBoxGrid check = (CheckBoxGrid)rFila.FindControl("cbSeleccionar");
            check.Checked = false;
            if (rFila.RowIndex == nItem)
            {
                check.Checked = true;
            }
        }

    }

    protected void ddlEstadoAfi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEstadoAfi.SelectedValue == "A")
            panelFecha.Enabled = false;
        else
            panelFecha.Enabled = true;
    }

    public string Bytes_A_Archivo(string idPersona, Byte[] ImgBytes)
    {
        Stream stream = null;
        string fileName = Server.MapPath("Images\\") + Path.GetFileName(idPersona + ".jpg");
        if (ImgBytes != null)
        {
            try
            {
                // Guardar imagen en un archivo
                stream = File.OpenWrite(fileName);
                foreach (byte b in ImgBytes)
                {
                    stream.WriteByte(b);
                }
                stream.Close();
                this.hdFileName.Value = Path.GetFileName(idPersona + ".jpg");
                mostrarImagen();
            }
            finally
            {
                /*Limpiamos los objetos*/
                stream.Dispose();
                stream = null;
            }
        }
        return fileName;
    }

    protected void InicializarEmpresaRecaudo()
    {
        PersonaEmpresaRecaudoServices perempresaServicio = new PersonaEmpresaRecaudoServices();
        List<PersonaEmpresaRecaudo> lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
        lstEmpresaRecaudo = perempresaServicio.ListarEmpresaRecaudo(false, (Usuario)Session["Usuario"]);
        gvEmpresaRecaudo.DataSource = lstEmpresaRecaudo;
        gvEmpresaRecaudo.DataBind();
        lblempresas.Visible = false;
        Session["EmpresaRecaudo"] = lstEmpresaRecaudo;
    }

    protected List<PersonaEmpresaRecaudo> ObtenerListaEmpresaRecaudo()
    {
        List<PersonaEmpresaRecaudo> lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
        List<PersonaEmpresaRecaudo> lista = new List<PersonaEmpresaRecaudo>();

        foreach (GridViewRow rfila in gvEmpresaRecaudo.Rows)
        {
            PersonaEmpresaRecaudo eActi = new PersonaEmpresaRecaudo();
            Label lblidempresarecaudo = (Label)rfila.FindControl("lblidempresarecaudo");
            if (lblidempresarecaudo != null)
                if (lblidempresarecaudo.Text != "")
                    eActi.idempresarecaudo = Convert.ToInt64(lblidempresarecaudo.Text);
            Label lblcodempresa = (Label)rfila.FindControl("lblcodempresa");
            if (lblcodempresa != null)
                eActi.cod_empresa = Convert.ToInt64(lblcodempresa.Text);
            Label lblDescripcion = (Label)rfila.FindControl("lblDescripcion");
            if (lblDescripcion != null)
                eActi.descripcion = lblDescripcion.Text;
            CheckBox chkSeleccionar = (CheckBox)rfila.FindControl("chkSeleccionar");
            if (chkSeleccionar != null)
                eActi.seleccionar = chkSeleccionar.Checked;

            lstEmpresaRecaudo.Add(eActi);
            lista.Add(eActi);
            Session["EmpresaRecaudo"] = lista;
        }
        return lstEmpresaRecaudo;
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Value == "2" || ddlFormaPago.SelectedItem.Text == "Nomina")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;

            RECUPERAR_EMPRESAS_NOMINA();
            ddlEmpresa.SelectedIndex = 0;
            DeterminarFechaInicioAfiliacion();
        }
        else
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        DeterminarFechaInicioAfiliacion();
    }

    /// <summary>
    /// Visualizar el panel de información laborar dependiendo de la ocupación
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlOcupacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOcupacion.SelectedValue != "7" && ddlOcupacion.SelectedValue != "0")
            acoInformacionLaboral.Visible = true;
        else
            acoInformacionLaboral.Visible = false;
    }

    /// <summary>
    /// Visulizar campo para registrar la otra entidad a la cual el cliente se encuentra asociado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ckAfiliadoOtraOS_CheckedChanged(object sender, EventArgs e)
    {
        if (ckAfiliadoOtraOS.Checked)
        {
            txtOtraOS.Visible = true;
            lblOtraOS.Visible = true;
        }
        else
        {
            txtOtraOS.Visible = false;
            lblOtraOS.Visible = false;
        }
    }

    //Visualizar campo para registrar cargos directivos ocupados
    protected void ckCargosOS_CheckedChanged(object sender, EventArgs e)
    {
        if (ckCargosOS.Checked)
        {
            txtCargosDirectivos.Visible = true;
            lblCargoOS.Visible = true;
        }
        else
        {
            txtCargosDirectivos.Visible = false;
            lblCargoOS.Visible = false;
        }
    }

    protected void chkPignorado_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPignorado.Checked)
        {
            txtPorcPignorado.Visible = true;
            lblPorcPignorado.Visible = true;
        }
        else
        {
            txtPorcPignorado.Visible = false;
            lblPorcPignorado.Visible = false;
        }
    }

    protected void ddlEstadoCivil_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEstadoCivil.SelectedValue == "2" || ddlEstadoCivil.SelectedValue == "3")
            acoInfConyuge.Visible = true;
        else
            acoInfConyuge.Visible = false;
    }

    protected void DeterminarFechaInicioAfiliacion()
    {
        if (!txtFechaAfili.TieneDatos)
            txtFechaAfili.ToDateTime = System.DateTime.Now;
        if (ddlEmpresa.SelectedValue == null)
            return;
        DateTime? fechainicio;
        try
        {
            fechainicio = _afiliacionServicio.FechaInicioAfiliacion(txtFechaAfili.ToDateTime, Convert.ToInt64(ddlEmpresa.SelectedValue), (Usuario)Session["Usuario"]);
            if (fechainicio != null)
                txtFecha1Pago.ToDateTime = Convert.ToDateTime(fechainicio);
        }
        catch
        { }
    }

    protected void txtIdentificacionE_TextChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            vPersona1.seleccionar = "Identificacion";
            vPersona1.soloPersona = 1;
            vPersona1.identificacion = txtIdentificacionE.Text;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (idObjeto != "")
            {
                if (vPersona1.identificacion != "" && vPersona1.identificacion != null && vPersona1.identificacion != Session["IDENTIFICACION"].ToString())
                    VerError("ERROR: La Identificación ingresada ya existe");
            }
            else
            {
                if (vPersona1.identificacion != "" && vPersona1.identificacion != null)
                    VerError("ERROR: La Identificación ingresada ya existe");
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void ddlparentesco_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Se omitió para registrar el nombre del funcionario en el módulo de PARENTESCO
        /*if (ddlparentesco.SelectedIndex != 0 && ddlparentesco.SelectedValue == "1")
        {
            lblnomFuncionario.Visible = true;
            txtnomFuncionario.Visible = true;
        }
        else
        {
            lblnomFuncionario.Visible = false;
            txtnomFuncionario.Visible = false;
        }*/
    }

    public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
    {
        return Math.Abs((fechaHasta.Month - fechaDesde.Month) + 12 * (fechaHasta.Year - fechaDesde.Year));
    }

    protected void txtFechaIngreso_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtFechaIngreso.Text != "")
                txtAntiguedadlugarEmpresa.Text = CalcularMesesDeDiferencia(Convert.ToDateTime(txtFechaIngreso.Text), DateTime.Now).ToString();
            else
                txtAntiguedadlugarEmpresa.Text = "";
        }
        catch
        {
            txtAntiguedadlugarEmpresa.Text = "";
        }
    }

    protected void rblSexo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSexo.SelectedItem.Text == "F")
            chkMujerCabeFami.Visible = true;
        else
            chkMujerCabeFami.Visible = false;
    }

    protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
    {
        CheckBoxGrid chkSeleccionar = (CheckBoxGrid)sender;
        int nItem = Convert.ToInt32(chkSeleccionar.CommandArgument);

        foreach (GridViewRow rFila in gvEmpresaRecaudo.Rows)
        {
            //RECUPERANDO CAMPO SELECCIONADO           
            if (rFila.RowIndex == nItem)
            {
                //RECUPERANDO EL CODIGO DE LA EMPRESA
                Label lblcodempresa = (Label)rFila.FindControl("lblcodempresa");
                if (lblcodempresa.Text != "")
                {
                    int cont = 0;
                    //CONSULTANDO LAS EMPRESAS EXCLUYENTES ===> (COD_EMPRESA) 
                    List<Xpinn.Tesoreria.Entities.EmpresaExcluyente> lstExcluyente = new List<Xpinn.Tesoreria.Entities.EmpresaExcluyente>();
                    Xpinn.Tesoreria.Services.EmpresaExcluyenteServices EmpresaExclu = new Xpinn.Tesoreria.Services.EmpresaExcluyenteServices();
                    lstExcluyente = EmpresaExclu.ListarEmpresaExcluyente(Convert.ToInt32(lblcodempresa.Text), (Usuario)Session["usuario"]);
                    if (lstExcluyente.Count > 0)
                    {
                        foreach (Xpinn.Tesoreria.Entities.EmpresaExcluyente Excluyente in lstExcluyente)
                        {
                            //Filtrando solo excluyentes de la empresa seleccionada
                            if (Excluyente.cod_empresa_excluye != null && Excluyente.cod_empresa_excluye != 0)
                            {
                                //Validando que las excluyentes no esten seleccionadas
                                foreach (GridViewRow valida in gvEmpresaRecaudo.Rows)
                                {
                                    Label lblcod_empresa = (Label)valida.FindControl("lblcodempresa");
                                    CheckBoxGrid check = (CheckBoxGrid)valida.FindControl("chkSeleccionar");

                                    if (lblcodempresa.Text != "")
                                        if (Excluyente.cod_empresa_excluye == Convert.ToInt32(lblcod_empresa.Text))
                                        {
                                            cont++;
                                            if (chkSeleccionar.Checked)
                                                check.Visible = false;
                                            else
                                                check.Visible = true;
                                            if (check.Checked)
                                            {
                                                check.Checked = false;
                                                break;
                                            }
                                        }
                                }
                            }
                        }
                    }
                    //CONSULTANDO LAS EMPRESAS EXCLUYENTES ===> (COD_EMPRESA_EXCLUYE)
                    if (cont == 0) // SI NO SE ENCONTRARON REGISTROS POR COD_EMPRESA
                    {
                        lstExcluyente = EmpresaExclu.ListarEmpresaExcluyenteINV(Convert.ToInt32(lblcodempresa.Text), (Usuario)Session["usuario"]);
                        if (lstExcluyente.Count > 0)
                        {
                            foreach (Xpinn.Tesoreria.Entities.EmpresaExcluyente INVExcluye in lstExcluyente)
                            {
                                //Filtrando solo excluyentes de la empresa seleccionada
                                if (INVExcluye.cod_empresa_excluye != null && INVExcluye.cod_empresa_excluye != 0)
                                {
                                    //Validando que las excluyentes no esten seleccionadas
                                    foreach (GridViewRow valida in gvEmpresaRecaudo.Rows)
                                    {
                                        Label lblcod_empresa = (Label)valida.FindControl("lblcodempresa");
                                        CheckBoxGrid check = (CheckBoxGrid)valida.FindControl("chkSeleccionar");

                                        if (lblcodempresa.Text != "")
                                            if (INVExcluye.cod_empresa == Convert.ToInt32(lblcod_empresa.Text))
                                            {
                                                if (chkSeleccionar.Checked)
                                                    check.Visible = false;
                                                else
                                                    check.Visible = true;
                                                if (check.Checked)
                                                {
                                                    check.Checked = false;
                                                    break;
                                                }
                                            }
                                    }
                                }
                            }
                        }

                    }

                }
                break;
            }
        }
        //TERMINO DE VALIDACION---////--------------
        RECUPERAR_EMPRESAS_NOMINA();
    }

    void RECUPERAR_EMPRESAS_NOMINA()
    {
        ddlEmpresa.Items.Clear();
        int cont = 0;
        ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
        foreach (GridViewRow rFila in gvEmpresaRecaudo.Rows)
        {
            CheckBoxGrid chkSeleccionar = (CheckBoxGrid)rFila.FindControl("chkSeleccionar");
            Label lblDescripcion = (Label)rFila.FindControl("lblDescripcion");
            Label lblcodempresa = (Label)rFila.FindControl("lblcodempresa");
            if (chkSeleccionar.Checked)
            {
                cont++;
                if (lblDescripcion.Text != "" && lblcodempresa.Text != "")
                {
                    ddlEmpresa.Items.Insert(cont, new ListItem(lblDescripcion.Text, lblcodempresa.Text));
                    ddlEmpresa.SelectedIndex = 1;
                }
            }
        }
    }

    //Calcular Valor de afiliacion
    void Calcular_Valor_Afiliacion()
    {
        try
        {
            ParametrosAfiliacion vDetalle = new ParametrosAfiliacion();
            ParametrosAfiliacionServices ParametroService = new ParametrosAfiliacionServices();
            vDetalle = ParametroService.ConsultarParametrosAfiliacion(0, (Usuario)Session["usuario"]);
            decimal SalarioMin = 0, nuevoValor = 0, valor = 0;
            if (vDetalle.valor != 0 && vDetalle.idparametros != 0)
            {
                valor = vDetalle.valor;
                if (vDetalle.tipo_calculo == 2) // si es de tipo % SMLMV
                {
                    //RECUPERAR SALARIO MINIMO
                    Xpinn.Comun.Entities.General pData = new Xpinn.Comun.Entities.General();
                    Xpinn.Comun.Data.GeneralData ConsultaData = new Xpinn.Comun.Data.GeneralData();
                    pData = ConsultaData.ConsultarGeneral(10, (Usuario)Session["usuario"]);

                    if (pData.valor != "" && pData.valor != null)
                        SalarioMin = Convert.ToDecimal(pData.valor);

                    nuevoValor = (SalarioMin * valor) / 100;
                    txtValorAfili.Text = nuevoValor.ToString("n0");
                }
                else // es de tipo Valor FIJO
                {
                    txtValorAfili.Text = valor.ToString("n0");
                }

                if (vDetalle.numero_cuotas != 0)
                    txtCuotasAfili.Text = vDetalle.numero_cuotas.ToString();
                if (vDetalle.cod_periodicidad != 0)
                    ddlPeriodicidad.SelectedValue = vDetalle.cod_periodicidad.ToString();
            }
            else
            {
                txtValorAfili.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    //validar email Valido
    public static bool IsValidEmail(string strMailAddress)
    {
        // Return true if strIn is in valid e-mail format.
        //return Regex.IsMatch(strMailAddress, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        return Regex.IsMatch(strMailAddress, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }

    protected void chkAsociado_CheckedChanged(object sender, EventArgs e)
    {
        panelAfiliacion.Enabled = chkAsociado.Checked;
        chkPEPS.Enabled = chkAsociado.Checked;
    }

    protected void chkPEPS_CheckedChanged(object sender, EventArgs e)
    {
        ddlAsociadosEspeciales.Enabled = chkPEPS.Checked;
        panelPEPS.Visible = chkPEPS.Checked;
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        ctlFormatos.lblErrorText = "";
        if (ctlFormatos.ddlFormatosItem != null)
            ctlFormatos.ddlFormatosIndex = 0;
        ctlFormatos.MostrarControl();
    }

    protected void btnImpresion_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "abc", "javascript: btnImpresion2.click()", true);
    }

    protected void btnImpresion2_Click(object sender, EventArgs e)
    {
        try
        {
            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Documentos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
            }
            catch
            { }
            string pRuta = "~/Page/Aportes/Personas/Documentos/";
            string pVariable = txtCod_persona.Text.Trim();
            ctlFormatos.ImprimirFormato(pVariable, pRuta);

            //Descargando el Archivo PDF
            string cNombreDeArchivo = pVariable.Trim() + "_" + ctlFormatos.ddlFormatosValue + ".pdf";
            string cRutaLocalDeArchivoPDF = Server.MapPath("Documentos\\" + cNombreDeArchivo);

            if (GlobalWeb.bMostrarPDF == true)
            {
                // Copiar el archivo a una ruta local
                try
                {
                    FileStream archivo = new FileStream(cRutaLocalDeArchivoPDF, FileMode.Open, FileAccess.Read);
                    FileInfo file = new FileInfo(cRutaLocalDeArchivoPDF);
                    Response.Clear();
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + cNombreDeArchivo);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/pdf";
                    Response.TransmitFile(file.FullName);
                    Response.End();
                }
                catch (Exception ex)
                {
                    ctlFormatos.lblErrorText = ex.Message;
                    ctlFormatos.lblErrorIsVisible = true;
                }
            }
            else
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = false;
                Response.AddHeader("Content-Disposition", "attachment;filename=" + cNombreDeArchivo);
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.WriteFile(cRutaLocalDeArchivoPDF);
                Response.End();
            }
            //RegistrarPostBack();
            //Response.Clear();
        }
        catch (Exception ex)
        {
            ctlFormatos.lblErrorIsVisible = true;
            ctlFormatos.lblErrorText = ex.Message;
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
    }

    protected void btnVerData_Click(object sender, EventArgs e)
    {
        panelFinal.Visible = false;
    }

    #region Metodos MODAL Llenado - Eventos - Validaciones


    protected void InicializarModal(object sender, EventArgs e)
    {
        txtModalNombres.Text = txtPrimer_nombreE.Text + " " + txtSegundo_nombreE.Text + " " + txtPrimer_apellidoE.Text + " " + txtSegundo_apellidoE.Text;
        txtModalIdentificacion.Text = txtIdentificacionE.Text;
        ddlModalIdentificacion.SelectedValue = ddlTipoE.SelectedValue;
        // Simulo evento para llenar ddl "Rango Vivienda"
        ddlModalVIS_SelectedIndexChanged(this, EventArgs.Empty);
        // Simulo evento para llenar ddl "Tipo Activo"
        ddlModalTipoActivo.SelectedIndex = 0;
        ddlModalTipoActivo_SelectedIndexChanged(this, EventArgs.Empty);
        lblErrorModal.Text = "";
        lblTipoProceso.Text = "";
        ddlEstadoModal.SelectedValue = "1";
        ddlEstadoModal.Enabled = false;
    }


    protected void LlenarDDLTipoIdentificacion()
    {
        TipoIdenService IdenService = new TipoIdenService();
        List<TipoIden> lstTipoIden = new List<TipoIden>(1);

        try
        {
            lstTipoIden = IdenService.ListarTipoIden(new TipoIden(), (Usuario)Session["usuario"]);
        }
        catch (Exception ex)
        {
            VerError("LlenarDDLTipoIdentificacion" + ex.Message);
            return;
        }

        if (lstTipoIden.Count == 0)
        {
            lstTipoIden.Add(new TipoIden());
        }


        // Lleno ddlIdentificacion de la View de Registro de Activos
        ddlModalIdentificacion.DataSource = lstTipoIden;
        ddlModalIdentificacion.DataTextField = "descripcion";
        ddlModalIdentificacion.DataValueField = "codtipoidentificacion";
        ddlModalIdentificacion.DataBind();
    }


    // Consulto y lleno DDL TipoActivoFijo con Anonymous Type para no perder los demas datos se utiliza el "-" como separador
    protected void LlenarDDLTipoActivo()
    {
        ActivosFijoservices activoService = new ActivosFijoservices();

        var lstActivoDataSource = from lista in activoService.ListarTipoActivoFijo((Usuario)Session["usuario"])
                                  select new
                                  {
                                      Descripcion = lista.nomclase,
                                      Value = lista.str_clase.ToString() + "-" + lista.cod_act.ToString()
                                  };

        ddlModalTipoActivo.DataSource = lstActivoDataSource;
        ddlModalTipoActivo.DataTextField = "Descripcion";
        ddlModalTipoActivo.DataValueField = "Value";
        ddlModalTipoActivo.DataBind();

        // Necesario tener el "-" para que no explote en el Split en el SelectIndex Event
        ddlModalTipoActivo.Items.Insert(0, new ListItem("Seleccione un Tipo", "0-0"));
    }


    protected void ddlModalVIS_SelectedIndexChanged(object sender, EventArgs e)
    {
        var dataConVIS = new[]
        {
             new { Valor = 1, Descripcion = "Tipo 1: Cuyo valor de la vivienda sea menor o igual a 50 SMML"} ,
             new { Valor = 2, Descripcion = "Tipo 2: Cuyo valor de la vivienda sea mayor a 50 SMML y menor o igual a 70 SMML"} ,
             new { Valor = 3, Descripcion = "Tipo 3: Cuyo valor de la vivienda sea mayor a 70 SMML y menor o igual a 100 SMML"} ,
             new { Valor = 4, Descripcion = "Tipo 4: Cuyo valor de la vivienda sea mayor a 100 SMML y menor o igual a 135 SMML"} ,
        };

        var dataSinVIS = new[]
        {
             new { Valor = 5, Descripcion = "Rango 1: Cuyo monto sea mayor a VIS y menor o igual a 643.100 UVR"} ,
             new { Valor = 6, Descripcion = "Rango 2: Cuyo monto sea mayor a 643.100 UVR y menor o igual a 2’411.625 UVR"} ,
             new { Valor = 7, Descripcion = "Rango 3: Cuyo valor sea mayor a 2’411.625 UVR"} ,
        };


        int tieneVIS = Convert.ToInt32(ddlModalVIS.SelectedValue);

        if (tieneVIS == (int)Tiene.Si)
        {
            ddlModalRangoVivienda.DataSource = dataConVIS;
        }
        else
        {
            ddlModalRangoVivienda.DataSource = dataSinVIS;
        }

        ddlModalRangoVivienda.DataTextField = "Descripcion";
        ddlModalRangoVivienda.DataValueField = "Valor";
        ddlModalRangoVivienda.DataBind();
    }


    protected void ddlModalTipoActivo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] tipoActivoSeleccionado = ddlModalTipoActivo.SelectedItem.Value.Split('-');

        if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Inmueble).ToString())
        {
            panelTipoActivoInmueble.Visible = true;
            pnlTipoActivoMaquinaria.Visible = false;
        }
        else if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Vehiculo).ToString())
        {
            panelTipoActivoInmueble.Visible = false;
            pnlTipoActivoMaquinaria.Visible = true;
        }
        else
        {
            panelTipoActivoInmueble.Visible = false;
            pnlTipoActivoMaquinaria.Visible = false;
        }
    }


    protected void btnGuardarModal_click(object sender, EventArgs e)
    {
        try
        {
            lblErrorModal.Text = "";
            string error = string.Empty;

            // Valido Campos Obligatorios y lleno entidad
            ActivoFijo activoFijo = ValidarCamposActivoFijo(out error);

            // Si hay algun error notifico y retorno
            if (!string.IsNullOrWhiteSpace(error))
            {
                lblErrorModal.Text = error;
                return;
            }

            // Lleno el resto de la entidad segun tipo Activo seleccionado en el DDL y procedo a guardar
            // Si tengo un tipo activo invalido retorno
            bool tipoActivoSeleccionadoCorrecto = LlenarEntidadActivoFijoGuardar(activoFijo);

            if (!tipoActivoSeleccionadoCorrecto)
            {
                return;
            }
            GarantiaService garantiasservicio = new GarantiaService();
            if (string.IsNullOrEmpty(lblTipoProceso.Text))
                garantiasservicio.CrearActivoFijoPersonal(activoFijo, Usuario);
            else
                garantiasservicio.ModificarActivoFijoPersonal(activoFijo, Usuario);
            LlenarGVActivoFijos(Session[_afiliacionServicio.CodigoPrograma + ".id"].ToString());
            mpeNuevoActividad.Hide();

            VaciarFormularioActivoFijo(upReclasificacion);
            InicializarModal(this, EventArgs.Empty);
            //RegistrarPostBack();
        }
        catch (Exception ex)
        {
            lblErrorModal.Text = "btnGuardarModal_click: " + ex.Message;
        }
    }


    // Limpio formulario despues de guardar
    public void VaciarFormularioActivoFijo(Control pControl)
    {
        foreach (var controlhij in pControl.Controls)
        {
            if (controlhij is TextBox)
            {
                var texbox = controlhij as TextBox;
                if (!texbox.ID.Contains(txtModalNombres.ID) || !texbox.ID.Contains(txtModalIdentificacion.ID))
                {
                    texbox.Text = "";
                }
            }
            else
            {
                VaciarFormularioActivoFijo((Control)controlhij);
            }
        }
    }


    #region Validar Campos


    // Validaciones en Activo Fijo cada error en campos obligatorios es llenado en variable error y devuelto
    private ActivoFijo ValidarCamposActivoFijo(out string error)
    {
        ActivoFijo activoFijo = new ActivoFijo();
        string fechaCompra = txtModalFechaIni.Text;
        string valor_avaluo = txtModalValorComercial.Text;
        string valor_comprometido = txtModalValorComprometido.Text;
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(valor_avaluo))
        {
            error += " Valor Comercial debe tener un valor valido, ";
            return activoFijo;
        }
        if (string.IsNullOrWhiteSpace(fechaCompra))
        {
            error += " Fecha de Adquisición debe ser llenada, ";
            return activoFijo;
        }

        if (string.IsNullOrEmpty(lblTipoProceso.Text))
        {
            foreach (GridViewRow row in gvBienesActivos.Rows)
            {
                string pCod = row.Cells[2].Text;
                if (!string.IsNullOrEmpty(pCod) && pCod != "0")
                {
                    Decimal a = Convert.ToDecimal(row.Cells[6].Text);
                    DateTime b = Convert.ToDateTime(row.Cells[5].Text);
                    string c = Convert.ToString(row.Cells[4].Text);
                    if (c == Convert.ToString(txtModalDescripcion.Text))
                    {
                        if (b == Convert.ToDateTime(fechaCompra))
                        {
                            if (a == Convert.ToDecimal(valor_avaluo))
                            {
                                error += "Valores repetidos con los anteriores Activos Fijos";
                                return activoFijo;
                            }
                        }
                    }
                }
            }
        }


        activoFijo.idActivo = string.IsNullOrEmpty(lblTipoProceso.Text) ? 0 : Convert.ToInt32(lblTipoProceso.Text);
        activoFijo.fecha_compra = Convert.ToDateTime(fechaCompra);
        activoFijo.valor_comprometido = string.IsNullOrWhiteSpace(valor_comprometido) ? 0 : Convert.ToDecimal(valor_comprometido);
        activoFijo.valor_compra = Convert.ToDecimal(valor_avaluo);
        activoFijo.cod_persona = Convert.ToInt64(Session[_afiliacionServicio.CodigoPrograma + ".id"]);
        activoFijo.descripcion = txtModalDescripcion.Text;
        activoFijo.estado = ddlEstadoModal.SelectedIndex;
        return activoFijo;
    }


    #endregion


    #region Llenar Entidad para guardar


    // Verifico que seleccion tiene mi DDL TipoActivo y llamo al metodo adecuado segun seleccion para llenar la entidad
    private bool LlenarEntidadActivoFijoGuardar(ActivoFijo activoFijo)
    {
        string[] tipoActivoSeleccionado = ddlModalTipoActivo.SelectedItem.Value.Split('-');
        activoFijo.cod_tipo_activo_per = Convert.ToInt64(tipoActivoSeleccionado[1]);

        if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Inmueble).ToString())
        {
            LlenarEntidadActivoFijoInmueble(activoFijo);
        }
        else if (tipoActivoSeleccionado[0] == ((char)TipoActivoFijo.Vehiculo).ToString())
        {
            LlenarEntidadActivoFijoVehiculo(activoFijo);
        }
        else
        {
            return false;
        }
        return true;
    }


    // Lleno entidad en modo VEHICULO
    private void LlenarEntidadActivoFijoVehiculo(ActivoFijo activoFijo)
    {
        string fechaImportacion = txtModalFechaImportacion.Text;

        if (!string.IsNullOrWhiteSpace(fechaImportacion))
        {
            activoFijo.fecha_importacion = Convert.ToDateTime(txtModalFechaImportacion.Text);
        }

        activoFijo.marca = txtModalMarca.Text;
        activoFijo.referencia = txtModalReferencia.Text;
        activoFijo.modelo = txtModalModelo.Text;
        activoFijo.cod_uso = Convert.ToInt32(ddlModalUso.SelectedValue);
        activoFijo.capacidad = txtModalCapacidad.Text;
        activoFijo.num_chasis = txtModalNoChasis.Text;
        activoFijo.num_motor = txtModalNoSerieMotor.Text;
        activoFijo.placa = txtModalPlaca.Text;
        activoFijo.color = txtModalColor.Text;
        activoFijo.documentos_importacion = txtModalDocImportacion.Text;
        if (chkPignorado.Checked)
        {
            if (string.IsNullOrWhiteSpace(txtPorcPignorado.Text))
            {
                VerError("El vehiculo se encuentra marcado como pignorado, registre el porcentaje");
                return;
            }
            else
                activoFijo.porcentaje_pignorado = Convert.ToInt32(txtPorcPignorado.Text);
        }
        activoFijo.porcentaje_pignorado = 0;
    }


    // Lleno entidad en modo Inmueble
    private void LlenarEntidadActivoFijoInmueble(ActivoFijo activoFijo)
    {
        activoFijo.direccion = txtModalDireccion.Text;
        activoFijo.localizacion = txtModalLocalizacion.Text;
        activoFijo.matricula = txtModalNotaria.Text;
        activoFijo.escritura = txtModalEscritura.Text;
        activoFijo.notaria = txtModalNotaria.Text;
        activoFijo.SENALVIS = Convert.ToInt32(ddlModalVIS.SelectedValue);
        activoFijo.tipo_vivienda = ddlModalTipoVivienda.SelectedValue;
        activoFijo.rango_vivienda = ddlModalRangoVivienda.SelectedValue;
        activoFijo.entidad_redescuento = ddlModalEntidadReDesc.SelectedValue;
        activoFijo.margen_redescuento = txtModalmargenReDesc.Text == "" ? null : txtModalmargenReDesc.Text;
        activoFijo.desembolso_directo = txtModalDesembolsoDirecto.Text == "" ? null : txtModalDesembolsoDirecto.Text;
        activoFijo.desembolso = ddlModalDesembolso.SelectedValue;
        activoFijo.hipoteca = chkHipoteca.Checked ? 1 : 0;
    }


    #endregion


    protected void btnCancelarModal_click(object sender, EventArgs e)
    {
        mpeNuevoActividad.Hide();
        VaciarFormularioActivoFijo(upReclasificacion);
        //RegistrarPostBack();
    }


    #endregion

    #region AccordionPanel Bienes/Activo


    protected void gvBienesActivos_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Delete")
            {
                if (!btnBienesActivos.Visible)
                {
                    return;
                }

                GarantiaService garantiaService = new GarantiaService();
                List<Garantia> lstReferencia = RecorresGrillaReferencias();
                int index = Convert.ToInt32(e.CommandArgument);
                Garantia garantia = lstReferencia[index];

                lstReferencia.RemoveAt(index);
                string pError = string.Empty;
                garantiaService.EliminarActivoFijo(garantia.IdActivo, 0, ref pError, (Usuario)Session["usuario"]);

                if (lstReferencia.Count == 0)
                {
                    lstReferencia.Add(new Garantia());
                }
                gvBienesActivos.DataSource = lstReferencia;
                gvBienesActivos.DataBind();
            }
        }
        catch (Exception ex)
        {
            VerError("gvBienesActivos_OnRowCommand, " + ex.Message);
        }
    }

    protected void gvBienesActivos_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Int64 conseID = Convert.ToInt64(gvBienesActivos.DataKeys[e.NewEditIndex].Values[0].ToString());
        lblErrorModal.Text = "";
        if (conseID != 0)
        {
            gvBienesActivos.EditIndex = e.NewEditIndex;
            ObtenerBienesActivos(conseID);
            mpeNuevoActividad.Show();
        }
        else
        {
            e.Cancel = true;
        }
        e.NewEditIndex = -1;
    }



    private List<Garantia> RecorresGrillaReferencias()
    {
        List<Garantia> lstBienes = new List<Garantia>();

        foreach (GridViewRow gFila in gvBienesActivos.Rows)
        {
            Garantia garantia = new Garantia()
            {
                IdActivo = Convert.ToInt64(gvBienesActivos.DataKeys[gFila.RowIndex].Value),
                descripcion_activo = gFila.Cells[3].Text,
                Descripcion = gFila.Cells[4].Text,
                Fecha_adquisicionactivo = !string.IsNullOrWhiteSpace(gFila.Cells[5].Text) ? Convert.ToDateTime(gFila.Cells[5].Text) : default(DateTime?),
                valor_comercial = !string.IsNullOrWhiteSpace(gFila.Cells[6].Text) ? Convert.ToInt64(gFila.Cells[6].Text.Replace(".", "")) : default(long?),
                estado_descripcion = gFila.Cells[6].Text
            };

            lstBienes.Add(garantia);
        }

        return lstBienes;

    }


    private void LlenarGVActivoFijos(string objeto)
    {
        List<Garantia> lstConsultas = new List<Garantia>(1);
        GarantiaService garantiasservicio = new GarantiaService();

        try
        {
            lstConsultas = garantiasservicio.Listaractivos(objeto, (Usuario)Session["usuario"]);
        }
        catch (Exception ex)
        {
            VerError("LlenarGVActivoFijos: " + ex.Message);
            return;
        }

        if (lstConsultas.Count == 0)
        {
            lstConsultas.Add(new Garantia());
        }

        gvBienesActivos.DataSource = lstConsultas;
        gvBienesActivos.DataBind();
    }


    private void InicializarBienesActivosFijos()
    {
        List<Garantia> lstBienes = new List<Garantia>(1) { new Garantia() };
        gvBienesActivos.DataSource = lstBienes;
        gvBienesActivos.DataBind();
    }


    // Es necesario este evento vacio para que pueda borrar la Row
    protected void gvBienesActivos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void ObtenerBienesActivos(Int64 IdGarantia)
    {
        GarantiaService garantiasservicio = new GarantiaService();

        VerError("");
        try
        {
            lblTipoProceso.Text = IdGarantia.ToString();
            ddlEstadoModal.Enabled = true;
            txtModalNombres.Text = txtPrimer_nombreE.Text + " " + txtSegundo_nombreE.Text + " " + txtPrimer_apellidoE.Text + " " + txtSegundo_apellidoE.Text;
            txtModalIdentificacion.Text = txtIdentificacionE.Text;
            ddlModalIdentificacion.SelectedValue = ddlTipoE.SelectedValue;

            ActivoFijo pActivo = new ActivoFijo();
            pActivo = garantiasservicio.ConsultarActivoFijoPersonal(IdGarantia, Usuario);
            if (pActivo != null)
            {
                if (pActivo.cod_tipo_activo_per != null)
                {
                    string value = pActivo.str_clase + "-" + pActivo.cod_tipo_activo_per;
                    ddlModalTipoActivo.SelectedValue = value;
                    ddlModalTipoActivo_SelectedIndexChanged(this, EventArgs.Empty);
                    ddlEstadoModal.SelectedValue = pActivo.estado.ToString();
                    txtModalDescripcion.Text = pActivo.descripcion != null ? pActivo.descripcion : string.Empty;
                    txtModalFechaIni.Text = pActivo.fecha_compra != null && pActivo.fecha_compra != DateTime.MinValue ? pActivo.fecha_compra.ToString() : string.Empty;
                    txtModalValorComercial.Text = pActivo.valor_compra.ToString();
                    txtModalValorComprometido.Text = pActivo.valor_comprometido.ToString();

                    if (pActivo.str_clase == "H")
                    {
                        if (!string.IsNullOrEmpty(pActivo.direccion))
                            txtModalDireccion.Text = HttpUtility.HtmlDecode(pActivo.direccion);
                        if (!string.IsNullOrEmpty(pActivo.localizacion))
                            txtModalLocalizacion.Text = HttpUtility.HtmlDecode(pActivo.localizacion);
                        if (pActivo.SENALVIS != null)
                            ddlModalVIS.SelectedValue = pActivo.SENALVIS.ToString();
                        if (!string.IsNullOrEmpty(pActivo.matricula))
                            txtModalNoMatricula.Text = pActivo.matricula;
                        if (!string.IsNullOrEmpty(pActivo.escritura))
                            txtModalEscritura.Text = pActivo.escritura;
                        if (!string.IsNullOrEmpty(pActivo.notaria))
                            txtModalNotaria.Text = pActivo.notaria;
                        if (!string.IsNullOrEmpty(pActivo.entidad_redescuento))
                            ddlModalEntidadReDesc.SelectedValue = pActivo.entidad_redescuento;
                        if (!string.IsNullOrEmpty(pActivo.margen_redescuento))
                            txtModalmargenReDesc.Text = pActivo.margen_redescuento;
                        if (!string.IsNullOrEmpty(pActivo.tipo_vivienda))
                            ddlModalTipoVivienda.SelectedValue = pActivo.tipo_vivienda;
                        if (!string.IsNullOrEmpty(pActivo.desembolso))
                            ddlModalDesembolso.SelectedValue = pActivo.desembolso;
                        if (!string.IsNullOrEmpty(pActivo.desembolso_directo))
                            txtModalDesembolsoDirecto.Text = pActivo.desembolso_directo;
                        if (!string.IsNullOrEmpty(pActivo.rango_vivienda))
                            ddlModalRangoVivienda.SelectedValue = pActivo.rango_vivienda;
                        if (pActivo.hipoteca != null && pActivo.hipoteca != 0)
                            chkHipoteca.Checked = true;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(pActivo.marca))
                            txtModalMarca.Text = pActivo.marca;
                        if (!string.IsNullOrEmpty(pActivo.referencia))
                            txtModalReferencia.Text = pActivo.referencia;
                        if (!string.IsNullOrEmpty(pActivo.modelo))
                            txtModalModelo.Text = pActivo.modelo;
                        if (pActivo.cod_uso != null)
                            ddlModalUso.SelectedValue = pActivo.cod_uso.ToString();
                        if (!string.IsNullOrEmpty(pActivo.num_chasis))
                            txtModalNoChasis.Text = pActivo.num_chasis;
                        if (!string.IsNullOrEmpty(pActivo.capacidad))
                            txtModalCapacidad.Text = pActivo.capacidad;
                        if (!string.IsNullOrEmpty(pActivo.num_motor))
                            txtModalNoSerieMotor.Text = pActivo.num_motor;
                        if (!string.IsNullOrEmpty(pActivo.placa))
                            txtModalPlaca.Text = pActivo.placa;
                        if (!string.IsNullOrEmpty(pActivo.color))
                            txtModalColor.Text = pActivo.color;
                        if (!string.IsNullOrEmpty(pActivo.documentos_importacion))
                            txtModalDocImportacion.Text = pActivo.documentos_importacion;
                        if (pActivo.fecha_importacion != null && pActivo.fecha_importacion != DateTime.MinValue)
                            txtModalFechaImportacion.Text = pActivo.fecha_importacion.ToString();
                        if (pActivo.porcentaje_pignorado != null && pActivo.porcentaje_pignorado != 0 && pActivo.porcentaje_pignorado > 0)
                        {
                            chkPignorado.Checked = true;
                            txtPorcPignorado.Text = pActivo.porcentaje_pignorado.ToString();
                            txtPorcPignorado.Visible = true;
                            lblPorcPignorado.Visible = true;
                        }

                    }
                }

            }
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    #endregion

    #region METODO DE ACTIVIDADES CIIU

    protected void gvActividadesCIIU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkPrincipal = (CheckBox)e.Row.FindControl("chkPrincipal");
            Label lblDescripcion = (Label)e.Row.FindControl("lbl_descripcion");
            chkPrincipal.Attributes.Add("onclick", "MostrarCIIUPrincipal('" + lblDescripcion.Text + "')");
        }
    }

    protected void imgBuscar_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["DTACTIVIDAD" + Usuario.codusuario] != null)
        {
            List<Persona1> lstActividad = (List<Persona1>)ViewState["DTACTIVIDAD" + Usuario.codusuario];
            if (lstActividad != null)
            {
                if (!string.IsNullOrEmpty(txtBuscarCodigo.Text.Trim()) && !string.IsNullOrEmpty(txtBuscarDescripcion.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaIdStr.Contains(txtBuscarCodigo.Text) || x.ListaDescripcion.Contains(txtBuscarDescripcion.Text)).ToList();
                else if (!string.IsNullOrEmpty(txtBuscarCodigo.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaIdStr.Contains(txtBuscarCodigo.Text)).ToList();
                else if (!string.IsNullOrEmpty(txtBuscarDescripcion.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaDescripcion.Contains(txtBuscarDescripcion.Text)).ToList();
                gvActividadesCIIU.DataSource = lstActividad;
                gvActividadesCIIU.DataBind();
            }
        }
        MostrarModal();
    }

    private void MostrarModal()
    {
        var ahh = txtRecoger_PopupControlExtender.ClientID;
        var script = @"Sys.Application.add_load(function() { $find('" + ahh + "').showPopup(); });";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPopup", script, true);
    }


    protected void gvActEmpresa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkPrincipal = (CheckBox)e.Row.FindControl("chkPrincipal");
            Label lbl_codigo = (Label)e.Row.FindControl("lbl_codigo");
            Label lblDescripcion = (Label)e.Row.FindControl("lbl_descripcion");
            chkPrincipal.Attributes.Add("onclick", "MostrarCIIUPrincipalEmp('" + lbl_codigo.Text + "','" + lblDescripcion.Text + "')");
        }
    }

    protected void imgBuscar2_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["DTACTIVIDAD" + Usuario.codusuario] != null)
        {
            List<Persona1> lstActividad = (List<Persona1>)ViewState["DTACTIVIDAD" + Usuario.codusuario];
            if (lstActividad != null)
            {
                if (!string.IsNullOrEmpty(txtBuscarCodigo2.Text.Trim()) && !string.IsNullOrEmpty(txtBuscarDescripcion2.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaIdStr.Contains(txtBuscarCodigo2.Text) || x.ListaDescripcion.Contains(txtBuscarDescripcion2.Text)).ToList();
                else if (!string.IsNullOrEmpty(txtBuscarCodigo2.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaIdStr.Contains(txtBuscarCodigo2.Text)).ToList();
                else if (!string.IsNullOrEmpty(txtBuscarCodigo2.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaDescripcion.Contains(txtBuscarDescripcion2.Text)).ToList();
                gvActEmpresa.DataSource = lstActividad;
                gvActEmpresa.DataBind();
            }
        }
        MostrarModal2();
    }

    private void MostrarModal2()
    {
        var ahh = PopupControlExtender3.ClientID;
        var script = @"Sys.Application.add_load(function() { $find('" + ahh + "').showPopup(); });";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPopup", script, true);
    }

    #endregion


    /// <summary>
    /// Cargar en el campo de actividad el registro marcado como actividad principal
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnAgregarFilaParentesco_Click(object sender, EventArgs e)
    {
        List<PersonaParentescos> listaParentescos = RecorrerGrillaParentescos();
        listaParentescos.Add(new PersonaParentescos());

        gvParentescos.DataSource = listaParentescos;
        gvParentescos.DataBind();
    }
    protected void btnAgregarFilaDocumento_Click(object sender, EventArgs e)
    {
        List<Imagenes> listaParentescos = RecorrerGrillaDocumentos();
        listaParentescos.Add(new Imagenes());

        gvDocumentos.DataSource = listaParentescos;
        gvDocumentos.DataBind();
    }
    protected void btnImprimirDoc_Click(object sender, EventArgs e)
    {
        try
        {
            // ELIMINANDO ARCHIVOS GENERADOS
            try
            {
                string[] ficherosCarpeta = Directory.GetFiles(Server.MapPath("Documentos\\"));
                foreach (string ficheroActual in ficherosCarpeta)
                    File.Delete(ficheroActual);
            }
            catch
            { }
            string pRuta = "~/Page/Aportes/Personas/Documentos/";
            Label lblCodDocumetno = gvDocumentos.SelectedRow.FindControl("lblCodDocumetno") as Label;
            string pVariable = lblCodDocumetno.Text.Trim();
            ctlFormatos.ImprimirFormato(pVariable, pRuta);

            //Descargando el Archivo PDF
            string cNombreDeArchivo = pVariable.Trim() + "_" + lblCodDocumetno + ".pdf";
            string cRutaLocalDeArchivoPDF = Server.MapPath("Documentos\\" + cNombreDeArchivo);

            if (GlobalWeb.bMostrarPDF == true)
            {
                // Copiar el archivo a una ruta local
                try
                {
                    FileStream fs = File.OpenRead(cRutaLocalDeArchivoPDF);
                    if (fs.Length <= 0)
                    {
                        ctlFormatos.lblErrorText = cRutaLocalDeArchivoPDF;
                        ctlFormatos.lblErrorIsVisible = true;
                        //ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
                    }
                    byte[] data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    fs.Close();
                    Session["Archivo" + Usuario.codusuario] = cRutaLocalDeArchivoPDF;
                    panelFinal.Visible = true;
                }
                catch (Exception ex)
                {
                    ctlFormatos.lblErrorText = ex.Message;
                    ctlFormatos.lblErrorIsVisible = true;
                }
            }
            else
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = false;
                Response.AddHeader("Content-Disposition", "attachment;filename=" + cNombreDeArchivo);
                Response.ContentType = "application/octet-stream";
                Response.Flush();
                Response.WriteFile(cRutaLocalDeArchivoPDF);
                Response.End();
            }
            RegistrarPostBack();
        }
        catch (Exception ex)
        {
            ctlFormatos.lblErrorIsVisible = true;
            ctlFormatos.lblErrorText = ex.Message;
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
    }
    protected void gvParentescos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownListGrid ddlParentescoLocal = e.Row.FindControl("ddlParentesco") as DropDownListGrid;
            DropDownListGrid ddlTipoIdentificacion = e.Row.FindControl("ddlTipoIdentificacion") as DropDownListGrid;
            DropDownListGrid ddlActividadEconomica = e.Row.FindControl("ddlActividadEconomica") as DropDownListGrid;
            CheckBoxList chListaEstatus = e.Row.FindControl("chListaEstatus") as CheckBoxList;
            TextBox txtIdenficacion = e.Row.FindControl("txtIdenficacion") as TextBox;


            LlenarListasDesplegables(TipoLista.Parentesco, ddlParentescoLocal);
            LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipoIdentificacion);
            LlenarListasDesplegables(TipoLista.Actividad_Negocio, ddlActividadEconomica);

            Label lblCodParentesco = e.Row.FindControl("lblCodParentesco") as Label;
            if (!string.IsNullOrWhiteSpace(lblCodParentesco.Text))
            {
                ddlParentescoLocal.SelectedValue = lblCodParentesco.Text;
            }

            Label lblTipoIdentificacion = e.Row.FindControl("lblTipoIdentificacion") as Label;
            if (!string.IsNullOrWhiteSpace(lblTipoIdentificacion.Text))
            {
                ddlTipoIdentificacion.SelectedValue = lblTipoIdentificacion.Text;
            }

            Label lblCodigoActividadEconomica = e.Row.FindControl("lblCodigoActividadEconomica") as Label;
            if (!string.IsNullOrWhiteSpace(lblCodigoActividadEconomica.Text))
            {
                ddlActividadEconomica.SelectedValue = lblCodigoActividadEconomica.Text;
            }

            List<PersonaParentescos> listaParentescos = (List<PersonaParentescos>)Session["lstParentescos"];
            if (listaParentescos != null)
            {
                if (listaParentescos.Count > 0)
                {
                    PersonaParentescos persona = new PersonaParentescos();
                    persona = listaParentescos.Where(x => x.identificacion == txtIdenficacion.Text).FirstOrDefault();
                    if (persona != null)
                    {
                        if (persona.empleado_entidad == 1)
                            chListaEstatus.Items[0].Selected = true;
                        if (persona.miembro_administracion == 1)
                            chListaEstatus.Items[1].Selected = true;
                        if (persona.miembro_control == 1)
                            chListaEstatus.Items[2].Selected = true;
                        if (persona.es_pep == 1)
                            chListaEstatus.Items[3].Selected = true;
                    }
                }
            }
        }
    }
    protected void gvDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //DropDownListGrid ddlDocumento = e.Row.FindControl("ddlDocumento") as DropDownListGrid;
            //List<FormatoDocumento> lstDatosSolicitud = DocumentoService.ListarFormatoDocumentoDrop(Usuario, 1, 0);


            //ddlDocumento.DataSource = lstDatosSolicitud;
            //ddlDocumento.DataTextField = "DESCRIPCION";
            //ddlDocumento.DataValueField = "COD_DOCUMENTO";
            //ddlDocumento.DataBind();


            //Label lblDocumento = e.Row.FindControl("lblCodDocumetno") as Label;
            //if (!string.IsNullOrWhiteSpace(lblDocumento.Text))
            //{
            //    ddlDocumento.SelectedValue = lblDocumento.Text;
            //}

        }
    }
    protected void gvParentescos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string consecutivoParaBorrar = gvParentescos.DataKeys[e.RowIndex].Value.ToString();

            if (!string.IsNullOrWhiteSpace(consecutivoParaBorrar))
            {
                _afiliacionServicio.EliminarPersonaParentesco(Convert.ToInt64(consecutivoParaBorrar), Usuario);
            }

            List<PersonaParentescos> listaParentescos = RecorrerGrillaParentescos();
            listaParentescos.RemoveAt(e.RowIndex);

            if (listaParentescos.Count <= 0)
            {
                listaParentescos.Add(new PersonaParentescos());
            }

            gvParentescos.DataSource = listaParentescos;
            gvParentescos.DataBind();
        }
        catch (Exception)
        {
            VerError("No se pudo borrar el registro del parentesco!.");
            RegistrarPostBack();
        }
    }
    protected void gvDocumentos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string coddocumetno = gvDocumentos.DataKeys[e.RowIndex].Value.ToString();



            List<Imagenes> listaDocumento = RecorrerGrillaDocumentos();
            listaDocumento.RemoveAt(e.RowIndex);

            if (listaDocumento.Count <= 0)
            {
                listaDocumento.Add(new Imagenes());
            }

            gvDocumentos.DataSource = listaDocumento;
            gvDocumentos.DataBind();
        }
        catch (Exception)
        {
            VerError("No se pudo borrar el registro del documetno!.");
            RegistrarPostBack();
        }
    }
    List<PersonaParentescos> RecorrerGrillaParentescos(bool filtrarValidos = false)
    {
        List<PersonaParentescos> listaParentescos = new List<PersonaParentescos>();

        foreach (GridViewRow row in gvParentescos.Rows)
        {
            PersonaParentescos parentesco = new PersonaParentescos();

            string consecutivo = gvParentescos.DataKeys[row.RowIndex].Value.ToString();
            if (!string.IsNullOrWhiteSpace(consecutivo))
            {
                parentesco.consecutivo = Convert.ToInt64(consecutivo);
            }

            DropDownListGrid ddlParentescoLocal = row.FindControl("ddlParentesco") as DropDownListGrid;
            if (!string.IsNullOrWhiteSpace(ddlParentescoLocal.SelectedValue))
            {
                parentesco.codigoparentesco = Convert.ToInt64(ddlParentescoLocal.SelectedValue);
            }

            DropDownListGrid ddlTipoIdentificacion = row.FindControl("ddlTipoIdentificacion") as DropDownListGrid;
            if (!string.IsNullOrWhiteSpace(ddlTipoIdentificacion.SelectedValue))
            {
                parentesco.codigotipoidentificacion = Convert.ToInt64(ddlTipoIdentificacion.SelectedValue);
            }

            DropDownListGrid ddlActividadEconomica = row.FindControl("ddlActividadEconomica") as DropDownListGrid;
            if (!string.IsNullOrWhiteSpace(ddlActividadEconomica.SelectedValue))
            {
                parentesco.codigoactividadnegocio = ddlActividadEconomica.SelectedValue;
            }

            TextBox txtIdenficacion = row.FindControl("txtIdenficacion") as TextBox;
            parentesco.identificacion = txtIdenficacion.Text;

            TextBox txtNombres = row.FindControl("txtNombres") as TextBox;
            parentesco.nombresapellidos = txtNombres.Text;

            TextBox txtEmpresa = row.FindControl("txtEmpresa") as TextBox;
            parentesco.empresa = txtEmpresa.Text;

            TextBox txtCargo = row.FindControl("txtCargo") as TextBox;
            parentesco.cargo = txtCargo.Text;

            CheckBoxList chListaEstatus = row.FindControl("chListaEstatus") as CheckBoxList;
            foreach (ListItem item in chListaEstatus.Items)
            {
                if (item.Value == "0" && item.Selected == true)
                    parentesco.empleado_entidad = 1;
                else if (item.Value == "0" && item.Selected == false)
                    parentesco.empleado_entidad = 0;

                if (item.Value == "1" && item.Selected == true)
                    parentesco.miembro_administracion = 1;
                else if (item.Value == "1" && item.Selected == false)
                    parentesco.miembro_administracion = 0;

                if (item.Value == "2" && item.Selected == true)
                    parentesco.miembro_control = 1;
                else if (item.Value == "2" && item.Selected == false)
                    parentesco.miembro_control = 0;

                if (item.Value == "3" && item.Selected == true)
                    parentesco.es_pep = 1;
                if (item.Value == "3" && item.Selected == false)
                    parentesco.es_pep = 0;
            }
            if (parentesco.miembro_control == 1 && parentesco.miembro_administracion == 1)
            {
                VerError("El familiar registrado no puede ser miembro de administración y control a la vez");
                parentesco.miembro_administracion = 0;
                parentesco.miembro_control = 0;
            }

            TextBox txtIngresoMensual = row.FindControl("txtIngresoMensual") as TextBox;
            if (!string.IsNullOrWhiteSpace(txtIngresoMensual.Text))
            {
                parentesco.ingresomensual = Convert.ToDecimal(txtIngresoMensual.Text);
            }
            // Si solo estoy recorriendo la grilla para agregar un registro mas, no valido nada y agrego todo
            // Si si estoy validando (Porque estoy guardando o modificando) filtro los validos
            if (!filtrarValidos || (parentesco.codigoparentesco > 0 && parentesco.codigotipoidentificacion > 0 && !string.IsNullOrWhiteSpace(txtNombres.Text) && !string.IsNullOrWhiteSpace(txtIdenficacion.Text)))
            {
                listaParentescos.Add(parentesco);
            }
        }

        return listaParentescos;
    }
    List<Imagenes> RecorrerGrillaDocumentos(bool filtrarValidos = false)
    {
        List<Imagenes> listaDocumentos = new List<Imagenes>();

        foreach (GridViewRow row in gvDocumentos.Rows)
        {
            Imagenes documento = new Imagenes();



            Label lblDocumento = row.FindControl("txtNombreArchivo") as Label;
            if (!string.IsNullOrWhiteSpace(lblDocumento.Text))
            {
                documento.idimagen = Convert.ToInt64(lblDocumento.Text);
            }


            //Si solo estoy recorriendo la grilla para agregar un registro mas, no valido nada y agrego todo
            // Si si estoy validando (Porque estoy guardando o modificando) filtro los validos
            if (!filtrarValidos || (documento.idimagen > 0))
            {
                listaDocumentos.Add(documento);
            }
        }

        return listaDocumentos;
    }

    void ObtenerDatosParentescos(long codigoPersona)
    {
        try
        {
            List<PersonaParentescos> listaParentescos = _afiliacionServicio.ListarParentescoDeUnaPersona(codigoPersona, Usuario);

            if (listaParentescos.Count <= 0)
            {
                listaParentescos.Add(new PersonaParentescos());
            }
            Session["lstParentescos"] = listaParentescos;

            gvParentescos.DataSource = listaParentescos;
            gvParentescos.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la lista de parentesco de esta persona, error:" + ex.Message);
        }
    }
    void ObtenerDatosDocumentos(long codigoPersona)
    {
        try
        {
            Imagenes entidad = new Imagenes();
            entidad.cod_persona = codigoPersona;
            List<Imagenes> listaParentescos = ImagenSERVICE.ListaDocumentos(entidad, Usuario);

            if (listaParentescos.Count <= 0)
            {
                listaParentescos.Add(new Imagenes());
            }

            gvDocumentos.DataSource = listaParentescos;
            gvDocumentos.DataBind();
        }
        catch (Exception ex)
        {
            VerError("Error al consultar la lista de parentesco de esta persona, error:" + ex.Message);
        }
    }

    #region Eventos sección moneda extranjera

    protected void chkMonedaExtranjera_CheckedChanged(object sender, EventArgs e)
    {
        if (chkMonedaExtranjera.Checked)
        {
            panelMonedaExtranjera.Visible = true;
        }
        else
        {
            panelMonedaExtranjera.Visible = false;
        }
    }

    /// <summary>
    /// Visualizar el botón para agregar registros de productos en el exterior
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkTransaccionExterior_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTransaccionExterior.Checked)
        {
            pProductosExt.Visible = true;
        }
        else
        {
            pProductosExt.Visible = false;
        }
    }

    protected void gvMonedaExtranjera_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int cod = Convert.ToInt32(gvMonedaExtranjera.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaMonedaExt();

        List<EstadosFinancieros> LstCuentas;
        LstCuentas = (List<EstadosFinancieros>)Session["DatosCuentaExtranjera"];

        if (cod > 0)
        {
            try
            {
                foreach (EstadosFinancieros eMoneda in LstCuentas)
                {
                    if (eMoneda.cod_moneda_ext == cod)
                    {
                        EstadosFinancierosServicio.EliminarCuentasMonedaExtranjera(cod, (Usuario)Session["usuario"]);
                        LstCuentas.Remove(eMoneda);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstCuentas.RemoveAt((gvMonedaExtranjera.PageIndex * gvMonedaExtranjera.PageSize) + e.RowIndex);
        }
        gvMonedaExtranjera.DataSourceID = null;
        gvMonedaExtranjera.DataBind();

        gvMonedaExtranjera.DataSource = LstCuentas;
        gvMonedaExtranjera.DataBind();

        Session["DatosCuentaExtranjera"] = LstCuentas;
    }

    /// <summary>
    /// Método para eliminar filas de la tabla de productos en el exterior
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProductosExterior_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int cod = Convert.ToInt32(gvProductosExterior.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerListaProductosExt();

        List<EstadosFinancieros> LstProductos;
        LstProductos = (List<EstadosFinancieros>)Session["DatosProductosFinExt"];

        if (cod > 0)
        {
            try
            {
                foreach (EstadosFinancieros eMoneda in LstProductos)
                {
                    if (eMoneda.cod_moneda_ext == cod)
                    {
                        EstadosFinancierosServicio.EliminarCuentasMonedaExtranjera(cod, (Usuario)Session["usuario"]);
                        LstProductos.Remove(eMoneda);
                        break;
                    }
                }
            }
            catch (Xpinn.Util.ExceptionBusiness ex)
            {
                VerError(ex.Message);
            }
        }
        else
        {
            LstProductos.RemoveAt((gvProductosExterior.PageIndex * gvMonedaExtranjera.PageSize) + e.RowIndex);
        }
        gvProductosExterior.DataSourceID = null;
        gvProductosExterior.DataBind();

        gvProductosExterior.DataSource = LstProductos;
        gvProductosExterior.DataBind();

        Session["DatosProductosFinExt"] = LstProductos;
    }


    protected void btnAgregarFilaM_Click(object sender, EventArgs e)
    {
        ObtenerListaMonedaExt();
        List<EstadosFinancieros> lsMonedaExtranjera = new List<EstadosFinancieros>();
        if (Session["DatosCuentaExtranjera"] != null)
        {
            lsMonedaExtranjera = (List<EstadosFinancieros>)Session["DatosCuentaExtranjera"];
            for (int i = 1; i <= 1; i++)
            {
                EstadosFinancieros eMonedaExtranjera = new EstadosFinancieros();
                eMonedaExtranjera.cod_moneda_ext = 0;
                eMonedaExtranjera.num_cuenta_ext = "";
                eMonedaExtranjera.banco_ext = null;
                eMonedaExtranjera.pais = null;
                eMonedaExtranjera.ciudad = null;
                eMonedaExtranjera.moneda = null;
                eMonedaExtranjera.desc_operacion = null;
                lsMonedaExtranjera.Add(eMonedaExtranjera);
            }
            gvMonedaExtranjera.DataSource = lsMonedaExtranjera;
            gvMonedaExtranjera.DataBind();
            Session["DatosCuentaExtranjera"] = lsMonedaExtranjera;
        }
    }

    /// <summary>
    /// Método para agregar un nuevo registro a la tabla de Productos en el exterior
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProductoExt_Click(object sender, EventArgs e)
    {
        ObtenerListaProductosExt();
        List<EstadosFinancieros> lstProductosExt = new List<EstadosFinancieros>();
        if (Session["DatosProductosFinExt"] != null)
        {
            lstProductosExt = (List<EstadosFinancieros>)Session["DatosProductosFinExt"];
            for (int i = 1; i <= 1; i++)
            {
                EstadosFinancieros eProducto = new EstadosFinancieros();
                eProducto.cod_moneda_ext = 0;
                eProducto.tipo_producto = "";
                eProducto.num_cuenta_ext = "";
                eProducto.pais = null;
                eProducto.ciudad = null;
                eProducto.moneda = null;
                lstProductosExt.Add(eProducto);
            }
            gvProductosExterior.DataSource = lstProductosExt;
            gvProductosExterior.DataBind();
            Session["DatosProductosFinExt"] = lstProductosExt;
        }
    }

    protected List<EstadosFinancieros> ObtenerListaMonedaExt()
    {
        List<EstadosFinancieros> lstListaMoneda = new List<EstadosFinancieros>();
        List<EstadosFinancieros> lista = new List<EstadosFinancieros>();

        foreach (GridViewRow filaMoneda in gvMonedaExtranjera.Rows)
        {
            EstadosFinancieros Moneda = new EstadosFinancieros();

            Label lblCodMoneda = (Label)filaMoneda.FindControl("lblCodMoneda");
            if (lblCodMoneda != null)
                Moneda.cod_moneda_ext = Convert.ToInt64(lblCodMoneda.Text);

            TextBox txtNumCuentaExt = (TextBox)filaMoneda.FindControl("txtNumCuentaExt");
            if (txtNumCuentaExt != null)
                Moneda.num_cuenta_ext = txtNumCuentaExt.Text != null && txtNumCuentaExt.Text != "" ? txtNumCuentaExt.Text : "";

            TextBox txtNomBancoExt = (TextBox)filaMoneda.FindControl("txtNomBancoExt");
            if (txtNomBancoExt != null)
                Moneda.banco_ext = txtNomBancoExt.Text != null && txtNomBancoExt.Text != "" ? Convert.ToString(txtNomBancoExt.Text) : "";

            TextBox txtNomPais = (TextBox)filaMoneda.FindControl("txtNomPais");
            if (txtNomPais != null)
                Moneda.pais = Convert.ToString(txtNomPais.Text);

            TextBox txtNomCiudad = (TextBox)filaMoneda.FindControl("txtNomCiudad");
            if (txtNomCiudad != null)
                Moneda.ciudad = Convert.ToString(txtNomCiudad.Text);

            TextBox txtNomMoneda = (TextBox)filaMoneda.FindControl("txtNomMoneda");
            if (txtNomMoneda != null)
                Moneda.moneda = Convert.ToString(txtNomMoneda.Text);

            TextBox txtOperacion = (TextBox)filaMoneda.FindControl("txtOperacion");
            if (txtOperacion != null)
                Moneda.desc_operacion = Convert.ToString(txtOperacion.Text);

            Moneda.tipo_producto = "";

            lista.Add(Moneda);

            if (Moneda.num_cuenta_ext != "" && Moneda.num_cuenta_ext != null)
            {
                lstListaMoneda.Add(Moneda);
            }
        }
        Session["DatosCuentaExtranjera"] = lista;
        return lstListaMoneda;
    }

    /// <summary>
    /// Método para obtener la lista de productos en el exterior
    /// </summary>
    /// <returns></returns>
    protected List<EstadosFinancieros> ObtenerListaProductosExt()
    {
        List<EstadosFinancieros> lstProductos = new List<EstadosFinancieros>();
        List<EstadosFinancieros> lista = new List<EstadosFinancieros>();

        foreach (GridViewRow filaMoneda in gvProductosExterior.Rows)
        {
            EstadosFinancieros Producto = new EstadosFinancieros();

            Label lblCodProducto = (Label)filaMoneda.FindControl("lblCodProducto");
            if (lblCodProducto != null)
                Producto.cod_moneda_ext = Convert.ToInt64(lblCodProducto.Text);

            TextBox txtTipoProducto = (TextBox)filaMoneda.FindControl("txtTipoProducto");
            if (txtTipoProducto != null)
                Producto.tipo_producto = Convert.ToString(txtTipoProducto.Text);

            TextBox txtNumProducto = (TextBox)filaMoneda.FindControl("txtNumProducto");
            if (txtNumProducto != null)
                Producto.num_cuenta_ext = txtNumProducto.Text != null && txtNumProducto.Text != "" ? txtNumProducto.Text : "";

            TextBox txtNomPais = (TextBox)filaMoneda.FindControl("txtNomPais");
            if (txtNomPais != null)
                Producto.pais = Convert.ToString(txtNomPais.Text);

            TextBox txtNomCiudad = (TextBox)filaMoneda.FindControl("txtNomCiudad");
            if (txtNomCiudad != null)
                Producto.ciudad = Convert.ToString(txtNomCiudad.Text);

            TextBox txtNomMoneda = (TextBox)filaMoneda.FindControl("txtNomMoneda");
            if (txtNomMoneda != null)
                Producto.moneda = Convert.ToString(txtNomMoneda.Text);

            Producto.banco_ext = "";
            Producto.desc_operacion = "";

            lista.Add(Producto);

            if (Producto.num_cuenta_ext != "" && Producto.num_cuenta_ext != null)
            {
                lstProductos.Add(Producto);
            }
        }
        Session["DatosProductosFinExt"] = lista;
        return lstProductos;
    }

    #endregion

    //Agregado para validar la lista de beneficiarios
    protected void TxtIde_TextChanged(object sender, EventArgs e)
    {
        TextBoxGrid Textact = (TextBoxGrid)sender;
        int index = Convert.ToInt32(Textact.CommandArgument);

        VerError("");
        bool resultSearch = false;

        foreach (GridViewRow row in gvBeneficiarios.Rows)
        {
            TextBoxGrid rowtxtIdentificacion = row.FindControl("txtIdentificacion") as TextBoxGrid;
            if (index != row.RowIndex)
            {
                if (Textact.Text.Trim() == rowtxtIdentificacion.Text.Trim())
                {
                    if (Textact.Text.Trim() != "" && rowtxtIdentificacion.Text.Trim() != "")
                    {
                        resultSearch = true;
                        break;
                    }
                }
            }
        }

        TextBox txtNombres = (TextBox)gvBeneficiarios.Rows[index].FindControl("txtNombres");
        if (resultSearch)
        {
            VerError("Identifiación ya ingresada -- Beneficiarios");
            Textact.Text = "";
            txtNombres.Text = "";
            return;
        }

        if (!string.IsNullOrEmpty(Textact.Text))
        {
            // REALIZANDO BUSQUEDA DE LA PERSONA SI EXISTE.
            Persona1Service persona1Servicio = new Persona1Service();
            Persona1 vPersona1 = new Persona1();

            vPersona1.identificacion = Textact.Text;
            vPersona1.seleccionar = "Identificacion";
            vPersona1.soloPersona = 1;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (vPersona1.nombre != "errordedatos")
            {
                txtNombres.Text = vPersona1.tipo_persona == "N" ? vPersona1.nombres + " " + vPersona1.apellidos : vPersona1.razon_social;
            }
        }
        else
            txtNombres.Text = "";

    }
    public string[] NombreSer()
    {
        int count = 0;
        string[] nomServ = new string[gvDocumentos.Rows.Count];
        foreach (GridViewRow rFila in gvDocumentos.Rows)
        {

            FileUpload fuArchivo = (FileUpload)rFila.FindControl("FluArchivo");
            /*Obtenemos el nombre y la extension del archivo*/
            String fileName = Path.GetFileName(fuArchivo.PostedFile.FileName);
            String extension = Path.GetExtension(fuArchivo.PostedFile.FileName).ToLower();

            try
            {
                if (fuArchivo.HasFile)
                {
                    /*Se guarda la imagen en el servidor*/
                    fuArchivo.PostedFile.SaveAs(Server.MapPath("Images\\") + fileName);
                    /*Obtenemos el nombre temporal de la imagen con la siguiente funcion*/
                    String nombreImgServer = getNombreImagenServidor(extension);

                    nomServ[count] = nombreImgServer;

                    /*Cambiamos el nombre de la imagen por el nuevo*/
                    File.Move(Server.MapPath("Images\\") + fileName, Server.MapPath("Images\\" + nombreImgServer));
                }
                count = count + 1;
            }
            catch (Exception ex)
            {
                VerError(ex.Message);
            }
        }
        //Session["ListArchivos"] = nomServ;
        return nomServ;
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {

        int id = int.Parse((sender as LinkButton).CommandArgument);
        byte[] bytes;
        Int64 idimagen = 0;
        bytes = ImagenSERVICE.DocumentosPersona(id, ref idimagen, Usuario);
        if (bytes != null)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=download.pdf");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

    }
    protected void btnImp_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Page/Aportes/Personas/ImportClasificacion.aspx");
    }
}