using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Services;

partial class Lista : GlobalWeb
{
    private CreditoGerencialService CredGerencialServicio = new CreditoGerencialService();
    private Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    private List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
    private String ListaSolicitada = null;

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session["Origen"] != null)
                switch (Session["Origen"].ToString())
                {
                    case "CEL":
                        if (Session[DatosClienteServicio.CodigoProgramaCreditoE + ".id"] != null)
                            VisualizarOpciones(DatosClienteServicio.CodigoProgramaCreditoE, "E");
                        else
                            VisualizarOpciones(DatosClienteServicio.CodigoProgramaCreditoE, "A");
                        break;
                    case "CGL":
                        VisualizarOpciones(CredGerencialServicio.CodigoPrograma, "D");
                        break;
                    case "SDCL":
                        VisualizarOpciones(DatosClienteServicio.CodigoPrograma, "C");
                        break;
                    case "MDCL":
                        VisualizarOpciones(DatosClienteServicio.CodigoProgramaMod, "C");
                        break;
                    case "ORI":
                        VisualizarOpciones(DatosClienteServicio.CodigoProgramaMod, "C");
                        break;
                    default:
                        VisualizarOpciones(DatosClienteServicio.CodigoPrograma, "C");
                        break;
                }


            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            // Para crédito gerencial dar opción de continuar con estado.
            if (Session["Origen"] != null)
                if (Session["Origen"].ToString() == "CGL")
                    ctlMensajeEstado.eventoClick += btnContinuarMenEst_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaCreditoE, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                VerError("");
                CargaListas();
                if (Session["Origen"] != null)
                { 
                    switch (Session["Origen"].ToString())
                    {
                        case "CEL":
                            if (Session[DatosClienteServicio.CodigoProgramaCreditoE + ".id"] != null)
                            {
                                ////// Aqui regresar a la pantalla de donde se llamo
                                idObjeto = Session[DatosClienteServicio.CodigoProgramaCreditoE + ".id"].ToString();
                                Session.Remove(DatosClienteServicio.CodigoProgramaCreditoE + ".id");
                                ObtenerDatos(idObjeto);
                            }
                            else
                                Navegar(NavegarAtras());
                            break;
                        case "CGL":
                            if (Session[CredGerencialServicio.CodigoPrograma + ".id"] != null)
                            {
                                idObjeto = Session[CredGerencialServicio.CodigoPrograma + ".id"].ToString();
                                Session.Remove(CredGerencialServicio.CodigoPrograma + ".id");
                                ObtenerDatos(idObjeto);
                            }
                            break;
                        case "SDCL":
                            if (Session[DatosClienteServicio.CodigoPrograma + ".id"] != null)
                            {
                                idObjeto = Session[DatosClienteServicio.CodigoPrograma + ".id"].ToString();
                                Session.Remove(DatosClienteServicio.CodigoPrograma + ".id");
                                ObtenerDatos(idObjeto);
                            }
                            break;
                        case "ORI":
                            string User = Convert.ToString(Request.QueryString["id"]);
                            if (User != null)
                            {
                                idObjeto = Session[DatosClienteServicio.CodigoPrograma + ".id"].ToString();
                                Session.Remove(DatosClienteServicio.CodigoPrograma + ".id");
                                ObtenerDatos(User);
                            }
                            break;
                        default:
                            if (Session[DatosClienteServicio.CodigoPrograma + ".id"] != null)
                            {
                                idObjeto = Session[DatosClienteServicio.CodigoPrograma + ".id"].ToString();
                                Session.Remove(DatosClienteServicio.CodigoPrograma + ".id");
                                ObtenerDatos(idObjeto);
                            }
                            break;
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
            BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaCreditoE, "Page_Load", ex);
        }
    }

    protected Boolean Validarestado(String pIdObjeto)
    {
        Boolean resultado = true;
        if (Session["Origen"] != null)
        {
            if (Session["Origen"].ToString() != "MDCL" && Session["Origen"].ToString() != "ORI")
            {
                Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
                vPersona1.seleccionar = "Identificacion";
                vPersona1.identificacion = pIdObjeto;
                vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
                if (vPersona1.estado == null)
                {
                    vPersona1.cod_persona = Convert.ToInt64(txtCod_personaE.Text);
                    vPersona1 = DatosClienteServicio.ConsultarPersona1(vPersona1.cod_persona, (Usuario)Session["usuario"]);
                    if (vPersona1.estado != null)
                    {
                        if (vPersona1.estado != Convert.ToString('A'))
                        {
                            VerError("Su estado no esta activo");
                            resultado = false;
                        }
                        else
                            Session["ES_EMPLEADO"] = "true";
                    }
                }
                else
                {
                    if (vPersona1.estado != Convert.ToString('A'))
                    {
                        resultado = false;
                        //VerError("Su estado no esta activo");
                        if (Session["Origen"] != null)
                            if (Session["Origen"].ToString() == "CGL")
                            {
                                ctlMensajeEstado.MostrarMensaje("Su estado no esta activo");
                            }
                    }
                }
            }
        }
        return resultado;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            if (Validarestado(idObjeto) == true)
            {
                VerError("");
                GuardarDetalle();
                Navegar(navegaNuevo());
            }
            else
            {
                VerError("Su estado no esta activo");
            }
        }
        else
        {
            return;
        }
    }

    protected void btnContinuarMenEst_Click(object sender, EventArgs e)
    {
        Page.Validate();

        if (Page.IsValid)
        {
            VerError("");
            GuardarDetalle();
            Navegar(navegaNuevo());
        }
        else
        {
            return;
        }
    }

    public String navegaNuevo()
    {
        String Direccion = "";
        if (Session["Origen"] != null)
            switch (Session["Origen"].ToString())
            {
                case "CEL":
                    Direccion = "~/Page/FabricaCreditos/CreditoEducativo/Nuevo.aspx";
                    break;
                case "CGL":
                    Direccion = "~/Page/FabricaCreditos/CreditoGerencial/Detalle.aspx";
                    break;
                case "SDCL":
                    Direccion = "~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx";
                    break;
                case "MDCL":
                    Direccion = "~/Page/FabricaCreditos/ModificacionCliente/Lista.aspx";
                    break;
                case "ORI":
                    Direccion = "~/Page/FabricaCreditos/Solicitud/DatosSolicitud/SolicitudCredito.aspx";
                    break;
                default:
                    Direccion = "~/Page/FabricaCreditos/Solicitud/DatosCliente/Lista.aspx";
                    break;
            }
        return Direccion;
    }

    public String NavegarAtras()
    {
        String Direccion = "";
        if (Session["Origen"] != null)
            switch (Session["Origen"].ToString())
            {
                case "CEL":
                    Direccion = "~/Page/FabricaCreditos/CreditoEducativo/Lista.aspx";
                    break;
                case "CGL":
                    Direccion = "~/Page/FabricaCreditos/CreditoGerencial/Lista.aspx";
                    break;
                case "SDCL":
                    Direccion = "~/Page/FabricaCreditos/Solicitud/DatosCliente/Lista.aspx";
                    break;
                case "MDCL":
                    Direccion = "~/Page/FabricaCreditos/ModificacionCliente/Lista.aspx";
                    break;
                case "ORI":
                    Direccion = "~/Page/FabricaCreditos/DatosCliente/DatosCliente.aspx";
                    break;
                default:
                    Direccion = "~/Page/FabricaCreditos/Solicitud/DatosCliente/Lista.aspx";
                    break;
            }
        return Direccion;
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(false);
            toolBar.MostrarCancelar(true);
            toolBar.MostrarConsultar(false);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaCreditoE, "btnGuardar_Click", ex);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Navegar(NavegarAtras());
    }

    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }

    void ValidarPersonaVacaciones(Int64 pCod_Persona)
    {
        if (pCod_Persona == 0)
            return;
        Xpinn.Tesoreria.Services.EmpresaNovedadService RecaudoService = new Xpinn.Tesoreria.Services.EmpresaNovedadService();
        Xpinn.Tesoreria.Entities.EmpresaNovedad pPersonaVac = new Xpinn.Tesoreria.Entities.EmpresaNovedad();
        string pFiltro = " where vac.cod_persona = " + pCod_Persona + " ";
        pPersonaVac = RecaudoService.ConsultarPersonaVacaciones(pFiltro, Usuario);

        if (pPersonaVac != null)
        {
            if (pPersonaVac.cod_persona > 0)
            {
                if (pPersonaVac.fechacreacion != null && pPersonaVac.fecha_inicial != null && pPersonaVac.fecha_final != null)
                {
                    DateTime pFechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                    if (pPersonaVac.fechacreacion <= pFechaActual && pPersonaVac.fecha_final >= pFechaActual)
                    {
                        VerError("La persona tiene un periodo de vacaciones del [ " + Convert.ToDateTime(pPersonaVac.fecha_inicial).ToString(gFormatoFecha) + " al " + Convert.ToDateTime(pPersonaVac.fecha_final).ToString(gFormatoFecha) + " ]");
                    }
                }
            }
        }
    }

    private void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            if (Session["Origen"] != null)
            { 
                if (Session["Origen"].ToString() == "MDCL")
                {
                    Usuario vUsuario = new Usuario();
                    vUsuario = (Usuario)Session["usuario"];
                    Xpinn.Seguridad.Services.UsuarioService UsuarioServicio = new Xpinn.Seguridad.Services.UsuarioService();
                    vUsuario = UsuarioServicio.ConsultarUsuario(vUsuario.codusuario, vUsuario);
                    List<int> lstAtribuciones = new List<int>();
                    lstAtribuciones = vUsuario.LstAtribuciones;
                    if (lstAtribuciones[3] == 1)
                    {
                        txtPrimer_apellidoE.Enabled = true;
                        txtSegundo_apellidoE.Enabled = true;
                        txtPrimer_nombreE.Enabled = true;
                        txtSegundo_nombreE.Enabled = true;
                    }
                    if (lstAtribuciones[6] == 1)
                    {
                        txtSalario.Enabled = true;
                        mensaje.Text = "";
                    }
                    else
                    {
                        mensaje.Text = "Solo los usuarios autarizados pueden ajustar este valor.";
                    }
                }
            }

            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            vPersona1.seleccionar = "Identificacion";
            vPersona1.identificacion = pIdObjeto;
            vPersona1.soloPersona = 1;
            vPersona1 = DatosClienteServicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);

            if (vPersona1.email != "" && vPersona1.email != null)
                txtemails.Text = vPersona1.email;

            if (vPersona1.nombre != "errordedatos")
            {
                if (vPersona1.cod_persona != Int64.MinValue)
                {
                    txtCod_personaE.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());
                    Session["codigocliente.SolicitudCredito"] = vPersona1.cod_persona;
                }

                // Determinar el tipo de persona
                try
                {
                    if (vPersona1.tipo_persona == "N")
                    {
                        rblTipo_persona.SelectedIndex = 0;
                        rblTipo_persona.Enabled = false;
                        txtrazonsocial.Visible = false;
                        lblrazonsocial.Visible = false;
                    }
                    else if (vPersona1.tipo_persona == "J")
                    {
                        rblTipo_persona.SelectedIndex = 1;
                        rblTipo_persona.Enabled = false;
                        txtrazonsocial.Visible = true;
                        lblrazonsocial.Visible = true;
                    }
                    else
                    {
                        rblTipo_persona.SelectedIndex = 0;
                    }
                }
                catch
                {
                    rblTipo_persona.SelectedIndex = 0;
                }
                if (vPersona1.barrioResidencia != Int64.MinValue && vPersona1.barrioResidencia != null)
                {
                    try
                    {
                        ddlCiuCorrespondencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.barrioResidencia.ToString().Trim());
                    }
                    catch { }
                }
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

                txtSalario.Text = vPersona1.salario.ToString();

                if (!string.IsNullOrEmpty(vPersona1.telCorrespondencia))
                    txtTelCorrespondencia.Text = HttpUtility.HtmlDecode(vPersona1.telCorrespondencia.ToString().Trim());
                if (vPersona1.ciuCorrespondencia != Int64.MinValue && vPersona1.ciuCorrespondencia != null && vPersona1.ciuCorrespondencia != -1)
                    ddlCiuCorrespondencia.SelectedValue = HttpUtility.HtmlDecode(vPersona1.ciuCorrespondencia.ToString().Trim());

                if (!string.IsNullOrEmpty(vPersona1.identificacion))
                {
                    txtIdentificacionE.Text = HttpUtility.HtmlDecode(vPersona1.identificacion.ToString().Trim());
                }
                if (vPersona1.digito_verificacion != Int64.MinValue)
                    txtDigito_verificacion.Text = HttpUtility.HtmlDecode(vPersona1.digito_verificacion.ToString().Trim());
                else
                    txtDigito_verificacion.Text = "";
                if (vPersona1.tipo_identificacion != Int64.MinValue)
                {
                    ddlTipoE.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_identificacion.ToString().Trim());
                }
                if (ddlTipoE.SelectedValue == "2")
                {
                    NEGOCIOMV.Visible = false;
                    PanelDatPersNatural.Visible = false;
                    rblTipo_persona.SelectedValue = HttpUtility.HtmlDecode("Jurídica");
                    RequiredFieldValidator17.Visible = false;
                    rfvApellido.Visible = false;
                }
                if (vPersona1.fechaexpedicion == null)
                {
                    txtFechaexpedicion.Text = "";
                }
                else
                {
                    if (vPersona1.fechaexpedicion != DateTime.MinValue)
                    {
                        txtFechaexpedicion.Text = HttpUtility.HtmlDecode(vPersona1.fechaexpedicion.Value.ToShortDateString());
                    }
                    else
                        txtFechaexpedicion.Text = "";
                }
                if (vPersona1.codciudadexpedicion != Int64.MinValue && vPersona1.codciudadexpedicion != null && vPersona1.codciudadexpedicion != -1 && vPersona1.codciudadexpedicion != 0)
                    ddlLugarExpedicion.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadexpedicion.ToString().Trim());
                try
                {
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
                    }
                }
                catch { }

                if (!string.IsNullOrEmpty(vPersona1.primer_nombre))
                    txtPrimer_nombreE.Text = HttpUtility.HtmlDecode(vPersona1.primer_nombre.ToString().Trim());
                else
                    txtPrimer_nombreE.Text = "";

                if (!string.IsNullOrEmpty(vPersona1.segundo_nombre))
                {
                    txtSegundo_nombreE.Text = HttpUtility.HtmlDecode(vPersona1.segundo_nombre.ToString().Trim());
                }
                else
                {
                    txtSegundo_nombreE.Text = "";
                }
                if (!string.IsNullOrEmpty(vPersona1.primer_apellido))
                {
                    txtPrimer_apellidoE.Text = HttpUtility.HtmlDecode(vPersona1.primer_apellido.ToString().Trim());
                }
                else
                {
                    txtPrimer_apellidoE.Text = "";
                }
                if (!string.IsNullOrEmpty(vPersona1.segundo_apellido))
                {
                    txtSegundo_apellidoE.Text = HttpUtility.HtmlDecode(vPersona1.segundo_apellido.ToString().Trim());
                }
                else
                {
                    txtSegundo_apellidoE.Text = "";
                }
                if (vPersona1.razon_social == null && Session["Negocio"] != null)
                    vPersona1.razon_social = Session["Negocio"].ToString();
                if (!string.IsNullOrEmpty(vPersona1.razon_social))
                {
                    txtRazon_socialE.Text = HttpUtility.HtmlDecode(vPersona1.razon_social.ToString().Trim());
                }
                else
                {
                    txtRazon_socialE.Text = "";
                }

                if (!string.IsNullOrEmpty(vPersona1.razon_social))
                {
                    txtrazonsocial.Text = HttpUtility.HtmlDecode(vPersona1.razon_social.ToString().Trim());
                }
                else
                {
                    txtrazonsocial.Text = "";
                }

                if (vPersona1.fechanacimiento == null)
                {
                    txtFechanacimiento.Text = "";
                }
                else
                {
                    if (vPersona1.fechanacimiento != DateTime.MinValue)
                    {
                        txtFechanacimiento.Text = HttpUtility.HtmlDecode(vPersona1.fechanacimiento.Value.ToShortDateString());
                        //txtFechanacimiento.Enabled = false;
                        txtEdadCliente.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimiento.Text)));
                        String format = gFormatoFecha;
                        DateTime Fechanacimiento = ConvertirStringToDate(txtFechanacimiento.Text);
                        if (Fechanacimiento > DateTime.Now || txtEdadCliente.Text == "" || txtEdadCliente.Text == "0")
                        {
                            txtFechanacimiento.Enabled = true;
                        }
                    }
                    else
                    {
                        txtFechanacimiento.Text = "";
                        txtEdadCliente.Text = "";
                    }
                }
                if (vPersona1.codciudadnacimiento != Int64.MinValue && vPersona1.codciudadnacimiento != null && vPersona1.codciudadnacimiento.ToString().Trim() != "" && vPersona1.codciudadnacimiento != 0)
                    ddlLugarNacimiento.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadnacimiento.ToString().Trim());

                if (vPersona1.codestadocivil != Int64.MinValue && vPersona1.codestadocivil.ToString().Trim() != "")
                {
                    try
                    {
                        ddlEstadoCivil.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codestadocivil.ToString().Trim());
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

                if (vPersona1.codactividadStr != null && vPersona1.codactividadStr.ToString().Trim() != "")
                {
                    try
                    {
                        ddlActividadE.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codactividadStr.ToString().Trim());
                    }
                    catch
                    {
                        ddlActividadE.SelectedValue = ddlActividadE.SelectedValue;
                    }
                }
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
                if (!string.IsNullOrEmpty(vPersona1.telefono))
                {
                    txtTelefonoE.Text = HttpUtility.HtmlDecode(vPersona1.telefono.ToString().Trim());
                }
                else
                {
                    txtTelefonoE.Text = "";
                }
                if (vPersona1.codciudadresidencia != Int64.MinValue && vPersona1.codciudadresidencia.ToString().Trim() != "" && vPersona1.codciudadresidencia != -1)
                {
                    ddlLugarResidenciaE.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadresidencia.ToString().Trim());
                }
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
                if (vPersona1.antiguedadlugarempresa != Int64.MinValue)
                    txtAntiguedadlugarEmpresa.Text = HttpUtility.HtmlDecode(vPersona1.antiguedadlugarempresa.ToString().Trim());
                else
                    txtAntiguedadlugarEmpresa.Text = "";
                if (vPersona1.codcargo == 0)
                {
                    ddlCargo.SelectedValue = "0";
                }
                else
                {
                    if (vPersona1.codcargo != Int64.MinValue)
                        ddlCargo.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codcargo.ToString().Trim());
                    else
                        ddlCargo.SelectedValue = "0";
                }
                if (vPersona1.codtipocontrato == 0)
                {
                    ddlTipoContrato.SelectedValue = "0";
                }
                else
                {
                    if (vPersona1.codtipocontrato != Int64.MinValue)
                        ddlTipoContrato.Text = HttpUtility.HtmlDecode(vPersona1.codtipocontrato.ToString().Trim());
                    else
                        ddlTipoContrato.SelectedValue = "0";
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
                    txtCod_oficina.Text = HttpUtility.HtmlDecode(vPersona1.cod_oficina.ToString().Trim());
                else
                    txtCod_oficina.Text = "";
                if (!string.IsNullOrEmpty(vPersona1.tratamiento))
                    txtTratamiento.Text = HttpUtility.HtmlDecode(vPersona1.tratamiento.ToString().Trim());
                else
                    txtTratamiento.Text = "";

                if (vPersona1.ActividadEconomicaEmpresa != 0)
                    ddlActividadE0.SelectedValue = vPersona1.ActividadEconomicaEmpresa.ToString();
                ddlCiu0.SelectedValue = vPersona1.ciudad.ToString();
                ddlparentesco.SelectedValue = vPersona1.relacionEmpleadosEmprender.ToString();
                txtTelCell0.Text = vPersona1.CelularEmpresa;
                txtProfecion.Text = vPersona1.profecion;
                txtEstrato.Text = vPersona1.Estrato.ToString();
                txtPersonasCargo.Text = vPersona1.PersonasAcargo.ToString();
                Session["Cod_Persona"] = vPersona1.cod_persona;
                if (vPersona1.fecha_ingresoempresa != DateTime.MinValue)
                    txtFechaIngreso.Text = vPersona1.fecha_ingresoempresa.ToString();

                //Carga datos cliente en masterpage y en variables de sesion:
                Session["EstadoCivil"] = ddlEstadoCivil.SelectedValue;
                if (ddlEstadoCivil.SelectedValue == "Seleccione un item")
                {
                    Session["EstadoCivil"] = 0;
                }

                Session["Actividad"] = vPersona1.codactividad;

                String PrimerNombre = txtPrimer_nombreE.Text != "" ? txtPrimer_nombreE.Text.ToString().Trim() : "";
                String SegundoNombre = txtSegundo_nombreE.Text != "" ? txtSegundo_nombreE.Text.ToString().Trim() : "";
                String primerApellido = txtPrimer_apellidoE.Text != "" ? txtPrimer_apellidoE.Text.ToString().Trim() : "";
                String SegundoApellido = txtSegundo_apellidoE.Text != "" ? txtSegundo_apellidoE.Text.ToString().Trim() : "";
                String NombreComple = PrimerNombre + " " + primerApellido + " " + SegundoApellido;

                Session["Nombres"] = NombreComple;
                Session["Identificacion"] = pIdObjeto;
                if (((Label)Master.FindControl("lblNombresApellidos")) != null)
                    ((Label)Master.FindControl("lblNombresApellidos")).Text = NombreComple;
                if (((Label)Master.FindControl("lblIdCliente")) != null)
                    ((Label)Master.FindControl("lblIdCliente")).Text = txtIdentificacionE.Text.Trim();
                if (((Label)Master.FindControl("lblCod_Cliente")) != null)
                    ((Label)Master.FindControl("lblCod_Cliente")).Text = txtCod_personaE.Text;

                validarArriendo();
                validarTipoPersona();
                CalcularEdad();

                // Consulta los datos del negocio para tomar de ellos la direccion y ponerla en "direccion de correspondencia"
                Xpinn.FabricaCreditos.Services.InformacionNegocioService InformacionNegocioServicio = new Xpinn.FabricaCreditos.Services.InformacionNegocioService();
                List<Xpinn.FabricaCreditos.Entities.InformacionNegocio> lstConsulta = new List<Xpinn.FabricaCreditos.Entities.InformacionNegocio>();
                InformacionNegocio pData = new InformacionNegocio();
                pData.cod_persona = Convert.ToInt64(txtCod_personaE.Text);
                lstConsulta = InformacionNegocioServicio.ListarInformacionNegocio(pData, (Usuario)Session["usuario"]);
                if (lstConsulta.Count > 0)
                {
                    if (lstConsulta[0].direccion != null)
                    {
                        if (lstConsulta[0].direccion.ToString().Trim() != "")
                        {
                            try
                            {
                                txtDirCorrespondencia.Text = lstConsulta[0].direccion.ToString();
                            }
                            catch
                            {
                                txtDirCorrespondencia.Text = "";
                            }
                        }
                    }
                }

                ValidarPersonaVacaciones(vPersona1.cod_persona);
            }
            else
            {
                VerError("Error de datos");
            }
        }
        catch (System.IndexOutOfRangeException ex)
        {
            VerError(ex.ToString());
        }
        catch (Exception ex)
        {
            Type Nuevaclase;
            Nuevaclase = ex.GetType();
            String Nombre = Nuevaclase.Name;
            VerError("El número de identificacion ingresado no existe");
        }
    }

    protected void txtFechanacimiento_TextChanged(object sender, EventArgs e)
    {
        CalcularEdad();
    }

    protected void rblTipoVivienda_SelectedIndexChanged(object sender, EventArgs e)
    {
        validarArriendo();
    }

    protected void rblTipoVivienda_DataBound1(object sender, EventArgs e)
    {
        RadioButtonList rbl = (RadioButtonList)sender;
        foreach (ListItem li in rblTipoVivienda.Items)
        {
            li.Attributes.Add("onclick", "javascript:DoSomething('" + li.Value + "')");
        }
    }

    private void validarArriendo()
    {
        if (rblTipoVivienda.SelectedValue == "A")
        {
            txtArrendador.Enabled = true;
            txtTelefonoarrendador.Enabled = true;
            txtValorArriendo.Enabled = true;

        }
        else
        {
            txtArrendador.Enabled = false;
            txtTelefonoarrendador.Enabled = false;
            txtValorArriendo.Enabled = false;
            //txtArrendador.Text = "";
            //txtTelefonoarrendador.Text = "";
            //txtValorArriendo.Text = "";
        }
    }

    private void validarTipoPersona()
    {
        if (rblTipo_persona.SelectedValue == "N")
        {
            txtRazon_socialE.Visible = false;
            //rfvtxtRazon_socialE.Enabled = false;
        }
        else
        {
            txtRazon_socialE.Visible = true;
            //rfvtxtRazon_socialE.Enabled = true;
        }
    }

    private void CalcularEdad()
    {
        if (txtFechanacimiento.Text != "")
            txtEdadCliente.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimiento.Text)));
    }

    private void TraerResultadosLista()
    {
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);

    }

    private void CargaListas()
    {
        try
        {
            ListaSolicitada = "Barrio";
            TraerResultadosLista();
            ddlBarrioResidencia.DataSource = lstDatosSolicitud;
            ddlBarrioCorrespondencia.DataSource = lstDatosSolicitud;
            ddlBarrioResidencia.DataTextField = "ListaDescripcion";
            ddlBarrioCorrespondencia.DataTextField = "ListaDescripcion";
            ddlBarrioResidencia.DataValueField = "ListaId";
            ddlBarrioCorrespondencia.DataValueField = "ListaId";
            ddlBarrioResidencia.DataBind();
            ddlBarrioCorrespondencia.DataBind();

            ListaSolicitada = "TipoIdentificacion";
            TraerResultadosLista();
            ddlTipoE.DataSource = lstDatosSolicitud;
            ddlTipoE.DataTextField = "ListaDescripcion";
            ddlTipoE.DataValueField = "ListaId";
            ddlTipoE.DataBind();

            // Llenar las listas que tienen que ver con ciudades
            ListaSolicitada = "Ciudades";
            TraerResultadosLista();
            ddlLugarExpedicion.DataSource = lstDatosSolicitud;
            ddlLugarNacimiento.DataSource = lstDatosSolicitud;
            ddlLugarResidenciaE.DataSource = lstDatosSolicitud;
            ddlCiuCorrespondencia.DataSource = lstDatosSolicitud;
            ddlCiu0.DataSource = lstDatosSolicitud;

            ddlLugarExpedicion.DataTextField = "ListaDescripcion";
            ddlLugarNacimiento.DataTextField = "ListaDescripcion";
            ddlLugarResidenciaE.DataTextField = "ListaDescripcion";
            ddlCiuCorrespondencia.DataTextField = "ListaDescripcion";
            ddlCiu0.DataTextField = "ListaDescripcion";

            ddlLugarExpedicion.DataValueField = "ListaIdStr";
            ddlLugarNacimiento.DataValueField = "ListaIdStr";
            ddlLugarResidenciaE.DataValueField = "ListaIdStr";
            ddlCiuCorrespondencia.DataValueField = "ListaIdStr";
            ddlCiu0.DataValueField = "ListaIdStr";

            ddlLugarExpedicion.DataBind();
            ddlLugarNacimiento.DataBind();
            ddlLugarResidenciaE.DataBind();
            ddlCiuCorrespondencia.DataBind();
            ddlCiu0.DataBind();

            // Colocar ciudad por defecto

            String CargarCiudad = System.Configuration.ConfigurationManager.AppSettings["CargarCiudad"].ToString();
            if (CargarCiudad == "true")
            {
                String CiudadDefault = System.Configuration.ConfigurationManager.AppSettings["Ciudad"].ToString();
                ddlLugarExpedicion.SelectedValue = CiudadDefault;
                ddlLugarNacimiento.SelectedValue = CiudadDefault;
                ddlCiuCorrespondencia.SelectedValue = CiudadDefault;
                ddlLugarResidenciaE.SelectedValue = CiudadDefault;
                ddlCiu0.SelectedValue = CiudadDefault;
            }

            ListaSolicitada = "EstadoCivil";
            TraerResultadosLista();
            ddlEstadoCivil.DataSource = lstDatosSolicitud;
            ddlEstadoCivil.DataTextField = "ListaDescripcion";
            ddlEstadoCivil.DataValueField = "ListaId";
            ddlEstadoCivil.DataBind();

            ListaSolicitada = "NivelEscolaridad";
            TraerResultadosLista();
            ddlNivelEscolaridad.DataSource = lstDatosSolicitud;
            ddlNivelEscolaridad.DataTextField = "ListaDescripcion";
            ddlNivelEscolaridad.DataValueField = "ListaId";
            ddlNivelEscolaridad.DataBind();

            ListaSolicitada = "Actividad_Negocio";
            TraerResultadosLista();
            ddlActividadE0.DataSource = lstDatosSolicitud;
            ddlActividadE0.DataTextField = "ListaDescripcion";
            ddlActividadE0.DataValueField = "ListaIdStr";
            ddlActividadE0.DataBind();

            ListaSolicitada = "Actividad2";
            TraerResultadosLista();
            ddlActividadE.DataSource = lstDatosSolicitud;
            ddlActividadE.DataTextField = "ListaDescripcion";
            ddlActividadE.DataValueField = "ListaIdStr";
            ddlActividadE.DataBind();

            ListaSolicitada = "TipoContrato";
            TraerResultadosLista();
            ddlTipoContrato.DataSource = lstDatosSolicitud;
            ddlTipoContrato.DataTextField = "ListaDescripcion";
            ddlTipoContrato.DataValueField = "ListaId";
            ddlTipoContrato.DataBind();

            ListaSolicitada = "TipoCargo";
            TraerResultadosLista();
            ddlCargo.DataSource = lstDatosSolicitud;
            ddlCargo.DataTextField = "ListaDescripcion";
            ddlCargo.DataValueField = "ListaId";
            ddlCargo.DataBind();

            ListaSolicitada = "ESTADO_ACTIVO";
            TraerResultadosLista();

        }
        catch
        {

        }
    }

    private void GuardarDetalle()
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona2 = new Xpinn.FabricaCreditos.Entities.Persona1();
            idObjeto = txtIdentificacionE.Text;
            if (idObjeto != "")
                vPersona1 = DatosClienteServicio.ConsultaDatosPersona(Convert.ToString(txtIdentificacionE.Text), (Usuario)Session["usuario"]);

            vPersona2 = DatosClienteServicio.ConsultarPersona1(vPersona1.cod_persona, (Usuario)Session["usuario"]);

            vPersona1.origen = "Solicitud";     //Permite reconocer que se modifica persona desde el formulario "Solicitud"
            vPersona1.barrioResidencia = (ddlBarrioResidencia.Text != "") ? Convert.ToInt64(ddlBarrioResidencia.SelectedValue) : 0;
            vPersona1.dirCorrespondencia = (txtDirCorrespondencia.Text != "") ? Convert.ToString(txtDirCorrespondencia.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.barrioCorrespondencia = (ddlBarrioCorrespondencia.SelectedValue != "") ? Convert.ToInt64(ddlBarrioCorrespondencia.SelectedValue) : 0;
            vPersona1.telCorrespondencia = (txtTelCorrespondencia.Text != "") ? Convert.ToString(txtTelCorrespondencia.Text.Trim()) : String.Empty;
            vPersona1.ciuCorrespondencia = (ddlCiuCorrespondencia.Text != "") ? Convert.ToInt64(ddlCiuCorrespondencia.SelectedValue) : 0;
            if (txtCod_personaE.Text != "") vPersona1.cod_persona = Convert.ToInt64(txtCod_personaE.Text.Trim());
            if (string.Equals(rblTipo_persona.Text, "Jurídica") || rblTipo_persona.SelectedValue == "J")
                vPersona1.tipo_persona = "J";
            else
                vPersona1.tipo_persona = "N";
            vPersona1.identificacion = (txtIdentificacionE.Text != "") ? Convert.ToString(txtIdentificacionE.Text.Trim()) : String.Empty;
            if (txtDigito_verificacion.Text != "") vPersona1.digito_verificacion = Convert.ToInt64(txtDigito_verificacion.Text.Trim());
            if (ddlTipoE.Text != "") vPersona1.tipo_identificacion = Convert.ToInt64(ddlTipoE.SelectedValue);
            if (txtFechaexpedicion.Text != "") vPersona1.fechaexpedicion = Convert.ToDateTime(txtFechaexpedicion.Text.Trim());
            if (ddlLugarExpedicion.SelectedItem != null)
            {
                if (ddlLugarExpedicion.SelectedIndex > 0)
                {
                    vPersona1.codciudadexpedicion = Convert.ToInt64(ddlLugarExpedicion.SelectedValue);
                }
            }

            vPersona1.sexo = (rblSexo.Text != "") ? Convert.ToString(rblSexo.SelectedValue) : String.Empty;
            vPersona1.primer_nombre = (txtPrimer_nombreE.Text != "") ? Convert.ToString(txtPrimer_nombreE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.segundo_nombre = (txtSegundo_nombreE.Text != "") ? Convert.ToString(txtSegundo_nombreE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.primer_apellido = (txtPrimer_apellidoE.Text != "") ? Convert.ToString(txtPrimer_apellidoE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.segundo_apellido = (txtSegundo_apellidoE.Text != "") ? Convert.ToString(txtSegundo_apellidoE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.razon_social = (txtRazon_socialE.Text != "") ? Convert.ToString(txtRazon_socialE.Text.Trim().ToUpper()) : String.Empty;
            if (vPersona1.razon_social == null)
                if (Session["Negocio"] != null)
                    vPersona1.razon_social = Session["Negocio"].ToString();
            if (txtFechanacimiento.Text != "") vPersona1.fechanacimiento = Convert.ToDateTime(txtFechanacimiento.Text.Trim());
            if (vPersona1.codciudadnacimiento == null && vPersona1.codciudadnacimiento == 0)
            {
                vPersona1.codciudadnacimiento = 0;
            }
            else
            {
                if (ddlLugarNacimiento.Text != "")
                {
                    vPersona1.codciudadnacimiento = ddlLugarNacimiento.SelectedIndex != 0 ? Convert.ToInt64(ddlLugarNacimiento.SelectedValue) : 0;
                }
            }
            if (vPersona1.codestadocivil == null && vPersona1.codestadocivil == 0)
            {
                vPersona1.codestadocivil = 0;
            }
            else
            {
                if (ddlEstadoCivil.Text != "")
                    vPersona1.codestadocivil = ddlEstadoCivil.SelectedIndex != 0 ? Convert.ToInt64(ddlEstadoCivil.SelectedValue) : 0;
            }
            if (vPersona1.codescolaridad == null && vPersona1.codestadocivil == 0)
            {
                vPersona1.codescolaridad = 0;
            }
            else
            {
                if (ddlNivelEscolaridad.Text != "")
                    vPersona1.codescolaridad = ddlNivelEscolaridad.SelectedIndex != 0 ? Convert.ToInt64(ddlNivelEscolaridad.SelectedValue) : 0;
            }
            if (vPersona1.codactividadStr == "")
                vPersona1.codactividadStr = null;
            else
                if (ddlActividadE.SelectedItem.Text != "")
                vPersona1.codactividadStr = ddlActividadE.SelectedValue;

            vPersona1.direccion = (txtDireccionE.Text != "") ? Convert.ToString(txtDireccionE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.telefono = (txtTelefonoE.Text != "") ? Convert.ToString(txtTelefonoE.Text.Trim()) : String.Empty;
            if (txtAntiguedadlugar.Text != "") vPersona1.antiguedadlugar = Convert.ToInt64(txtAntiguedadlugar.Text.Trim());
            vPersona1.tipovivienda = (rblTipoVivienda.Text != "") ? Convert.ToString(rblTipoVivienda.SelectedValue) : String.Empty;
            vPersona1.arrendador = (txtArrendador.Text != "") ? Convert.ToString(txtArrendador.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.telefonoarrendador = (txtTelefonoarrendador.Text != "") ? Convert.ToString(txtTelefonoarrendador.Text.Trim()) : String.Empty;
            if (txtValorArriendo.Text != "") vPersona1.ValorArriendo = Convert.ToInt64(txtValorArriendo.Text.Trim().Replace(".", ""));
            vPersona1.celular = (txtCelular.Text != "") ? Convert.ToString(txtCelular.Text.Trim()) : String.Empty;
            vPersona1.email = (txtEmail.Text != "") ? Convert.ToString(txtEmail.Text.Trim()) : String.Empty;
            vPersona1.empresa = (txtEmpresa.Text != "") ? Convert.ToString(txtEmpresa.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.telefonoempresa = (txtTelefonoempresa.Text != "") ? Convert.ToString(txtTelefonoempresa.Text.Trim()) : String.Empty;
            vPersona1.direccionempresa = (txtDireccionEmpresa.Text != "") ? Convert.ToString(txtDireccionEmpresa.Text.Trim().ToUpper()) : String.Empty;
            if (txtAntiguedadlugarEmpresa.Text != "") vPersona1.antiguedadlugarempresa = Convert.ToInt64(txtAntiguedadlugarEmpresa.Text.Trim());
            if (ddlCargo.Text != "") vPersona1.codcargo = Convert.ToInt64(ddlCargo.Text.Trim());
            if (ddlTipoContrato.Text != "") vPersona1.codtipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue);
            if (txtCod_asesor.Text != "") vPersona1.cod_asesor = Convert.ToInt64(txtCod_asesor.Text.Trim());
            vPersona1.residente = (rblResidente.Text != "") ? Convert.ToString(rblResidente.SelectedValue) : String.Empty;
            if (txtFecha_residencia.Text != "") vPersona1.fecha_residencia = Convert.ToDateTime(txtFecha_residencia.Text.Trim());
            if (txtCod_oficina.Text != "") vPersona1.cod_oficina = Convert.ToInt64(txtCod_oficina.Text.Trim());
            vPersona1.tratamiento = (txtTratamiento.Text != "") ? Convert.ToString(txtTratamiento.Text.Trim()) : String.Empty;
            vPersona1.estado = vPersona2.estado;
            vPersona1.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            vPersona1.ActividadEconomicaEmpresaStr = ddlActividadE0.SelectedValue;
            vPersona1.ciudad = Convert.ToInt64(ddlCiu0.SelectedValue);
            vPersona1.relacionEmpleadosEmprender = Convert.ToInt32(ddlparentesco.SelectedValue);
            vPersona1.CelularEmpresa = txtTelCell0.Text;
            vPersona1.profecion = txtProfecion.Text;
            if (txtemails.Text != "")
                vPersona1.email = txtemails.Text;
            if (vPersona1.PersonasAcargo == null && vPersona1.PersonasAcargo == 0)
            {
                vPersona1.PersonasAcargo = 0;
            }
            else
            {
                vPersona1.PersonasAcargo = txtPersonasCargo.Text != "" ? Convert.ToInt64(txtPersonasCargo.Text) : 0;
            }
            vPersona1.valor_afiliacion = 0;

            try
            {
                vPersona1.Estrato = Convert.ToInt32(txtEstrato.Text);
            }
            catch
            {
                vPersona1.Estrato = 0;
            }
            Label lblUsuario = (Label)this.Master.FindControl("header1").FindControl("lblUser");
            vPersona1.usuariocreacion = lblUsuario.Text;
            vPersona1.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
            vPersona1.usuultmod = lblUsuario.Text;
            if (ddlLugarResidenciaE.Text != "") vPersona1.codciudadresidencia = Convert.ToInt64(ddlLugarResidenciaE.SelectedValue);
            vPersona1.ocupacion = "";

            vPersona1.salario = Convert.ToInt64(txtSalario.Text);
            vPersona1.fecha_ingresoempresa = string.IsNullOrEmpty(txtFechaIngreso.Text) ? DateTime.MinValue : Convert.ToDateTime(txtFechaIngreso.Text);
            // Actualizar los datos de la persona
            DatosClienteServicio.ModificarPersona1(vPersona1, (Usuario)Session["usuario"]);
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(DatosClienteServicio.CodigoProgramaCreditoE, "btnGuardar_Click", ex);
        }
    }

}