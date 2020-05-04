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
using Xpinn.Contabilidad.Entities;
using Xpinn.Contabilidad.Services;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Services;
using Xpinn.ActivosFijos.Entities;
using Xpinn.ActivosFijos.Services;
using Xpinn.Caja.Services;
using Xpinn.Caja.Entities;

partial class Nuevo : GlobalWeb
{
    private Xpinn.Contabilidad.Services.TerceroService TerceroServicio = new Xpinn.Contabilidad.Services.TerceroService();
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    private EstadosFinancierosService EstadosFinancierosServicio = new EstadosFinancierosService();
    StringHelper _stringHelper = new StringHelper();

    protected void Page_PreInit(object sender, System.EventArgs e)
    {
        try
        {
            if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AfiliacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(AfiliacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoConsultar += btnConsultar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            ctlFormatos.eventoClick += btnImpresion_Click;
            txtFecConstitucion.eventoCambiar += txtFecConstitucion_TextChanged;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                DeshabilitarControlesActivarReadOnly();
                Session["EmpresaRecaudo"] = null;
                obtenerControlesAdicionales();
                txtCodigo.Enabled = false;
                CargarListas();
                Calcular_Valor_Afiliacion();
                VerificarLlamadoEstadoCuenta();
                //LlenarComboTipoIden(ddlTipoID);
                Session["lstAsociados"] = null;
                if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AfiliacionServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    LlenarGVActivoFijos(idObjeto);
                    if (Session[AfiliacionServicio.CodigoPrograma + ".modificar"].ToString() == "1")
                    {
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarGuardar(false);
                        btnBienesActivos.Visible = false;
                    }
                    else
                    {
                        //InicializarModal(this, EventArgs.Empty);
                    }
                    ddlOficina.Enabled = false;
                    mvDatos.ActiveViewIndex = 1;
                    lblInfoBienesActivos.Visible = false;
                }
                else
                {
                    InicializarEmpresaRecaudo();
                    txtCodigo.Text = TerceroServicio.ObtenerSiguienteCodigo((Usuario)Session["Usuario"]).ToString();
                    ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                    chkAsociado_CheckedChanged(chkAsociado, null);
                }
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "Page_Load", ex);
        }
    }

    private void VerificarLlamadoEstadoCuenta()
    {
        Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
        if (Session[serviceEstadoCuenta.CodigoPrograma + ".id"] != null)
        {
            Session[AfiliacionServicio.CodigoPrograma + ".id"] = Session[serviceEstadoCuenta.CodigoPrograma + ".id"];
            Session[AfiliacionServicio.CodigoPrograma + ".modificar"] = 1;
        }
    }
    

    private void obtenerControlesAdicionales()
    {
        InformacionAdicionalServices informacion = new InformacionAdicionalServices();
        InformacionAdicional pInfo = new InformacionAdicional();
        List<InformacionAdicional> lstControles = new List<InformacionAdicional>();
        string tipo = "J";
        lstControles = informacion.ListarInformacionAdicional(pInfo, tipo, (Usuario)Session["usuario"]);
        if (lstControles.Count > 0)
        {
            gvInfoAdicional.DataSource = lstControles;
            gvInfoAdicional.DataBind();
        }
    }



    protected List<InformacionAdicional> ObtenerListaInformacionAdicional()
    {
        List<InformacionAdicional> lstInformacionAdd = new List<InformacionAdicional>();

        foreach (GridViewRow rfila in gvInfoAdicional.Rows)
        {
            InformacionAdicional eActi = new InformacionAdicional();

            if (idObjeto != "")
            {
                Label lblidinfadicional = (Label)rfila.FindControl("lblidinfadicional");
                if (lblidinfadicional != null)
                    eActi.idinfadicional = Convert.ToInt32(lblidinfadicional.Text);
            }
            else
                eActi.idinfadicional = 0;
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
                    {
                        if (ddlDropdown.Text != "")
                            eActi.valor = ddlDropdown.SelectedItem.Text;
                    }
                }
            }

            if (eActi.cod_infadicional != 0)
            {
                lstInformacionAdd.Add(eActi);
            }
        }
        return lstInformacionAdd;
    }



    Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

    Boolean ValidarDatos()
    {
        VerError("");
        if (idObjeto == "")
        {
            if (txtIdentificacion.Text == "")
            {
                VerError("Ingrese el Nit");
                return false;
            }

            Xpinn.FabricaCreditos.Services.EstadosFinancierosService EstadosFinancierosServicio = new Xpinn.FabricaCreditos.Services.EstadosFinancierosService();
            vPersona1 = persona1Servicio.ConsultaDatosPersona(txtIdentificacion.Text, (Usuario)Session["usuario"]);
            if (vPersona1.cod_persona != 0)
            {
                VerError("Ya existe una persona con el NIT asignado");
                return false;
            }
        }

        if (rbTipoPersona.SelectedValue == "J")
        {
            if (txtFecConstitucion.Text == "")
            {
                VerError("Ingrese la fecha de Constitución");
                return false;
            }
        }

        if (ddlOficina.SelectedIndex == 0 || ddlOficina.SelectedItem == null)
        {
            VerError("Seleccione la oficina de la persona jurídica");
            return false;
        }
        if (txtRazonSocial.Text == "")
        {
            VerError("Ingrese la Razón social");
            return false;
        }
        if (txtSigla.Text == "")
        {
            VerError("Ingrese la Sigla");
            return false;
        }
        if (txtDireccion.Text == "")
        {
            VerError("Ingrese la dirección");
            return false;
        }

        if (txtEmail.Text == "")
        {
            VerError("ingrese su correo ");
            txtEmail.Focus();
            return false;
        }

        if (ddlCiudad.SelectedIndex == 0)
        {
            VerError("Seleccione la ciudad");
            return false;
        }
        if (txtTelefono.Text == "")
        {
            VerError("Ingrese el teléfono");
            return false;
        }
        if (txtEmail.Text == "")
        {
            VerError("Ingrese el Email");
            return false;
        }
        if (txtFecha.Text == "")
        {
            VerError("Ingrese la fecha de Creación");
            return false;
        }
        if (ddlActividad.SelectedIndex == 0)
        {
            VerError("Seleccione la Actividad");
            return false;
        }



        if (ddlRegimen.SelectedIndex == 0)
        {
            VerError("Seleccione el Régimen");
            return false;
        }
        if (ddlTipoActoCrea.SelectedIndex == 0)
        {
            VerError("Seleccione el tipo de Acto");
            return false;
        }
        if (txtCam_Comercio.Text == "")
        {
            VerError("Ingrese la cámara de comercio");
            return false;
        }
        //INGRESO OBLIGATORIO DE LOS DATOS DE LA AFILIACION
        if (chkAsociado.Checked)
        {
            if (txtFechaAfili.Text != "" && ddlEstadoAfi.SelectedIndex == 0)
            {
                VerError("Seleccione el Estado de Afiliación");
                return false;
            }
            if (txtFechaAfili.Text == "")
            {
                VerError("Ingrese la fecha de Afiliación");
                txtFechaAfili.Focus();
                return false;
            }
            if (ddlEstadoAfi.SelectedIndex == 0)
            {
                VerError("Seleccione el Estado de Afiliación");
                ddlEstadoAfi.Focus();
                return false;
            }
            if (txtValorAfili.Text == "")
            {
                VerError("Ingrese el Valor de Afiliación");
                txtValorAfili.Focus();
                return false;
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

            if (idObjeto != "")
            {
                //Agregado para validar que no se modifique la fecha de afiliación si el asociado tiene aportes con saldo
                List<Aporte> lstAporte = new List<Aporte>();
                List<Aporte> lstAportesActivos = new List<Aporte>();
                AporteServices aporteServicio = new AporteServices();
                DateTime? fecAfiliacion = AfiliacionServicio.FechaAfiliacion(txtCodigo.Text, (Usuario)Session["usuario"]);
                lstAporte = aporteServicio.ListarEstadoCuentaAportestodos(Convert.ToInt64(txtCodigo.Text), "1,2", DateTime.Now, (Usuario)Session["usuario"]);
                lstAportesActivos = lstAporte.Where(x => x.Saldo > 0).ToList();
                if (lstAportesActivos.Count > 0 && fecAfiliacion != Convert.ToDateTime(txtFechaAfili.Text))
                {
                    VerError("La fecha de afiliación del asociado no se puede modificar, tiene aportes activos");
                    txtFechaAfili.Text = Convert.ToDateTime(fecAfiliacion).ToShortDateString();
                    return false;
                }
            }
        }
        return true;
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidarDatos())
            {
                Usuario vUsuario = (Usuario)Session["Usuario"];

                Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();

                if (idObjeto != "")
                    vTercero = TerceroServicio.ConsultarTercero(Convert.ToInt64(idObjeto), null, (Usuario)Session["usuario"]);

                //vTercero.cod_persona = Convert.ToInt64(txtCodigo.Text.Trim());
                vTercero.tipo_persona = "J";
                vTercero.identificacion = Convert.ToString(txtIdentificacion.Text.Trim());
                if (txtDigitoVerificacion.Text != "")
                    vTercero.digito_verificacion = Convert.ToInt32(txtDigitoVerificacion.Text);
                vTercero.tipo_identificacion = 2;

                vTercero.fechaexpedicion = txtFecha.ToDateTime;
                vTercero.codciudadexpedicion = Convert.ToInt64(ddlCiudad.SelectedValue);

                vTercero.primer_apellido = txtSigla.Text.ToUpper();
                vTercero.razon_social = txtRazonSocial.Text.ToUpper();
                vTercero.tipo_empresa = ddlTipoEmpresa.SelectedIndex > 0 ? Convert.ToInt32(ddlTipoEmpresa.SelectedValue) : 0;
                vTercero.ActividadEconomicaEmpresaStr = ddlActividad.SelectedValue;

                if (ChkEnteTerritorial.Checked == true)
                {
                    vTercero.EnteTerritorial = 1;
                }
                else
                {
                    vTercero.EnteTerritorial = 0;
                }
                vTercero.direccion = txtDireccion.Text.ToUpper();
                vTercero.telefono = txtTelefono.Text;
                vTercero.email = txtEmail.Text;
                vTercero.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue);
                vTercero.estado = "A";
                vTercero.regimen = ddlRegimen.SelectedValue;
                if (ddlRegimen.SelectedValue != "")
                    vTercero.regimen = ddlRegimen.SelectedValue;
                else
                    vTercero.regimen = "";
                vTercero.fecultmod = DateTime.Now;
                vTercero.usuultmod = vUsuario.identificacion;
                if (vTercero.fechacreacion == null)
                    vTercero.fechacreacion = System.DateTime.Now;
                if (vTercero.usuariocreacion == null)
                    vTercero.usuariocreacion = vUsuario.identificacion;

                //Agregado

                vTercero.tipo_acto_creacion = Convert.ToInt32(ddlTipoActoCrea.SelectedValue);
                vTercero.fechanacimiento = txtFecConstitucion.Text.Trim() != "" ? Convert.ToDateTime(txtFecConstitucion.Text) : DateTime.MinValue;

                if (txtNumActoCrea.Text != "")
                    vTercero.num_acto_creacion = txtNumActoCrea.Text;
                else
                    vTercero.num_acto_creacion = null;
                if (txtcelular.Text != "")
                    vTercero.celular = txtcelular.Text;
                else
                    vTercero.celular = null;

                //Fecha y lugar de registro 
                vTercero.codciudadnacimiento = ddlLugRegistro.SelectedIndex > 0 ? Convert.ToInt64(ddlLugRegistro.SelectedValue) : 0;
                vTercero.fecha_residencia = txtFecha_Registro.Text != null && txtFecha_Registro.Text != "" ? Convert.ToDateTime(txtFecha_Registro.Text) : DateTime.MinValue;

                //Agregado para registrar información de cámara de comercio y representante legal
                vTercero.camara_comercio = txtCam_Comercio.Text;
                //vTercero.cod_representante = Convert.ToInt64(txtCodRepresentante.Text);
                vTercero.cod_representante = Convert.ToInt64(ctlPersona.TextCodigo);
                

                //Información de notificación
                vTercero.ubicacion_empresa = ddlTipoUbic.SelectedIndex > 0 ? Convert.ToInt32(ddlTipoUbic.SelectedValue) : 0;
                vTercero.dircorrespondencia = txtDir_Correspondencia.Text != "" && txtDir_Correspondencia.Text != null ? txtDir_Correspondencia.Text.Trim() : "";
                vTercero.ciucorrespondencia = ddlCiudad_Corr.SelectedIndex > -1 ? Convert.ToInt64(ddlCiudad_Corr.SelectedValue) : 0;
                vTercero.barcorrespondencia = ddlBarrio_Corr.SelectedIndex > -1 ? Convert.ToInt64(ddlBarrio_Corr.SelectedValue) : 0;
                vTercero.telcorrespondencia = txtTel_Correspondencia.Text != "" && txtTel_Correspondencia.Text != null ? txtTel_Correspondencia.Text.Trim() : "";
                vTercero.cod_zona = Convert.ToInt64(ddlZona.SelectedValue);

                vTercero.lstInformacion = new List<InformacionAdicional>();
                vTercero.lstInformacion = ObtenerListaInformacionAdicional();

                vTercero.lstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
                vTercero.lstEmpresaRecaudo = ObtenerListaEmpresaRecaudo();

                //Agregado para registrar información de moneda extranjera
                vTercero.lstMonedaExt = new List<EstadosFinancieros>();
                vTercero.lstMonedaExt = ObtenerListaMonedaExt();

                //Agregado para registrar información de productos en el exterior
                vTercero.lsProductosExt = new List<EstadosFinancieros>();
                vTercero.lsProductosExt = ObtenerListaProductosExt();

                vTercero.lstAsociados = new List<Xpinn.Contabilidad.Entities.Tercero>();
                vTercero.lstAsociados = ObtenerAsociados();
                
                // ACTIVIDADES CIIU
                if (gvActividadesCIIU.Rows.Count > 0)
                {
                    byte NumActiSeleccionadas = 0;
                    bool ActPrincipalSeleccionada = false;
                    Label lblCodigo;
                    vTercero.lstActividadCIIU = new List<Actividades>();
                    Actividades objActividad;
                    foreach (GridViewRow rFila in gvActividadesCIIU.Rows)
                    {
                        CheckBoxGrid chkSeleccionado = rFila.FindControl("chkSeleccionar") as CheckBoxGrid;
                        CheckBoxGrid chkPrincipal = rFila.FindControl("chkPrincipal") as CheckBoxGrid;
                        if (chkSeleccionado.Checked)
                        {
                            if (!chkPrincipal.Checked)
                            {
                                objActividad = new Actividades();
                                lblCodigo = rFila.FindControl("lbl_codigo") as Label;
                                objActividad.codactividad = lblCodigo.Text;
                                vTercero.lstActividadCIIU.Add(objActividad);
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
                                vTercero.codactividadStr = lblCodigo.Text;
                            }
                            else
                            {
                                VerError("Ha seleccionado más de una actividad economica principal");
                                return;
                            }
                        }

                        if (NumActiSeleccionadas > 3)
                        {
                            VerError("Se han seleccionado mas de 3 actividades economicas secundarias");
                            return;
                        }
                    }
                }

                if (idObjeto != "")
                {
                    vTercero.cod_persona = Convert.ToInt64(idObjeto);
                    TerceroServicio.ModificarTercero(vTercero, (Usuario)Session["usuario"]);
                }
                else
                {
                    vTercero.cod_persona = 0;
                    vTercero = TerceroServicio.CrearTercero(vTercero, (Usuario)Session["usuario"]);
                    idObjeto = Convert.ToString(vTercero.cod_persona);
                }


                if (chkAsociado.Checked)
                {
                    AfiliacionServices afiliacionService = new AfiliacionServices();

                    Afiliacion afili = new Afiliacion();

                    if (txtcodAfiliacion.Text != "")
                        afili.idafiliacion = Convert.ToInt64(txtcodAfiliacion.Text);
                    else
                        afili.idafiliacion = 0;

                    afili.cod_persona = Convert.ToInt64(vTercero.cod_persona);

                    if (txtFechaAfili.Text != "")
                        afili.fecha_afiliacion = Convert.ToDateTime(txtFechaAfili.Text);

                    if (ddlEstadoAfi.SelectedValue != "0")
                        afili.estado = ddlEstadoAfi.SelectedValue;

                    if (ddlEstadoAfi.SelectedItem.Text == "Activo")
                        afili.fecha_retiro = DateTime.MinValue;
                    else
                    {
                        if (txtFechaRetiro.Text != "")
                            afili.fecha_retiro = Convert.ToDateTime(txtFechaRetiro.Text);
                        else
                            afili.fecha_retiro = DateTime.MinValue;
                    }

                    if (txtValorAfili.Text != "0")
                        afili.valor = Convert.ToDecimal(txtValorAfili.Text);
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

                    if (!string.IsNullOrWhiteSpace(ddlAsesor.SelectedValue))
                    {
                        afili.cod_asesor = Convert.ToInt64(ddlAsesor.SelectedValue);
                    }

                    if (ddlAsociadosEspeciales.SelectedValue != "")
                        afili.cod_asociado_especial = Convert.ToInt32(ddlAsociadosEspeciales.SelectedValue);                    

                    if (chkAdminRecursos.Checked)
                        afili.Administra_recursos_publicos = chkAdminRecursos.Checked;
                    else
                        afili.Administra_recursos_publicos = false;

                    if (txtcodAfiliacion.Text != "")
                    {
                        if (txtFechaAfili.Text != "" && ddlEstadoAfi.SelectedValue != "0")
                        {
                            afiliacionService.ModificarPersonaAfiliacion(afili, (Usuario)Session["usuario"]);
                        }
                    }
                    else
                    {
                        if (txtFechaAfili.Text != "" && ddlEstadoAfi.SelectedValue != "0")
                        {
                            afili = afiliacionService.CrearPersonaAfiliacion(afili, (Usuario)Session["usuario"]);
                        }
                    }
                }

                ///---------------------------------------
                //Registro de información económica
                RegistrarPostBack();
                EstadosFinancieros InformacionFinanciera = new EstadosFinancieros();
                EstadosFinancierosService EstadosFinancierosServicio = new EstadosFinancierosService();
                if (txtingreso_mensual.Text != null && txtingreso_mensual.Text != "")
                    InformacionFinanciera.sueldo = Convert.ToDecimal(txtingreso_mensual.Text);
                InformacionFinanciera.sueldoconyuge = 0;
                InformacionFinanciera.honorarios = 0;
                InformacionFinanciera.honorariosconyuge = 0;
                InformacionFinanciera.arrendamientos = 0;
                InformacionFinanciera.arrendamientosconyuge = 0;
                if (txtotrosIng_soli.Text != null && txtotrosIng_soli.Text != "")
                    InformacionFinanciera.otrosingresos = Convert.ToDecimal(txtotrosIng_soli.Text);
                InformacionFinanciera.otrosingresosconyuge = 0;
                if (hdtotalING_soli.Value != null && hdtotalING_soli.Value != "")
                    InformacionFinanciera.totalingreso = Convert.ToDecimal(hdtotalING_soli.Value);
                InformacionFinanciera.totalingresoconyuge = 0;
                InformacionFinanciera.hipoteca = 0;
                InformacionFinanciera.hipotecaconyuge = 0;
                InformacionFinanciera.targeta_credito = 0;
                InformacionFinanciera.targeta_creditoconyuge = 0;
                InformacionFinanciera.otrosprestamos = 0;
                InformacionFinanciera.otrosprestamosconyuge = 0;
                InformacionFinanciera.gastofamiliar = 0;
                InformacionFinanciera.gastofamiliarconyuge = 0;
                InformacionFinanciera.decunomina = 0;
                InformacionFinanciera.decunominaconyuge = 0;
                if (hdtotalEGR_soli.Value != null && hdtotalEGR_soli.Value != "")
                    InformacionFinanciera.totalegresos = Convert.ToDecimal(hdtotalEGR_soli.Value);
                InformacionFinanciera.totalegresosconyuge = 0;
                if (txtTotal_activos.Text != null && txtTotal_activos.Text != "")
                    InformacionFinanciera.TotAct = Convert.ToInt64(txtTotal_activos.Text);
                InformacionFinanciera.TotActConyuge = 0;
                if (txtTotal_pasivos.Text != null && txtTotal_pasivos.Text != "")
                    InformacionFinanciera.TotPas = Convert.ToInt64(txtTotal_pasivos.Text);
                InformacionFinanciera.TotPasConyuge = 0;
                if (txtTotal_patrimonio.Text != null && txtTotal_patrimonio.Text != "")
                    InformacionFinanciera.TotPat = Convert.ToInt64(txtTotal_patrimonio.Text);
                InformacionFinanciera.TotPatConyuge = 0;

                InformacionFinanciera.conceptootros = txtConceptoOtros_soli.Text;
                InformacionFinanciera.conceptootrosconyuge = "";
                InformacionFinanciera.cod_persona = vTercero.cod_persona;

                EstadosFinancierosServicio.guardarIngreEgre(InformacionFinanciera, (Usuario)Session["Usuario"]);


                ///---------------------------------------
                Session[AfiliacionServicio.CodigoPrograma + ".id"] = idObjeto;
                Navegar(Pagina.Lista);
            }
        }
        catch (ExceptionBusiness ex)
        {
            VerError(ex.Message);
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Navegar(Pagina.Lista);
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (idObjeto == "")
        {
            Navegar(Pagina.Lista);
        }
        else
        {
            Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
            if (Session[serviceEstadoCuenta.CodigoPrograma + ".id"] != null)
            {
                Session.Remove(serviceEstadoCuenta.CodigoPrograma + ".id");
                Navegar("../../Asesores/EstadoCuenta/Detalle.aspx");
            }
            else
                Navegar("../Afiliaciones/Lista.aspx");
            //Session[AfiliacionServicio.CodigoPrograma + ".id"] = idObjeto;
            //Navegar(Pagina.Detalle);
        }
    }

    protected void ObtenerDatos(String pIdObjeto)
    {
        try
        {
            ActividadesServices _servicesActividad = new ActividadesServices();
            Xpinn.Contabilidad.Entities.Tercero vTercero = new Xpinn.Contabilidad.Entities.Tercero();
            vTercero = TerceroServicio.ConsultarTercero(Convert.ToInt64(pIdObjeto), null, (Usuario)Session["usuario"]);
            txtCodigo.Text = HttpUtility.HtmlDecode(vTercero.cod_persona.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.identificacion))
                txtIdentificacion.Text = HttpUtility.HtmlDecode(vTercero.identificacion.ToString().Trim());
            if (vTercero.digito_verificacion != 0)
                txtDigitoVerificacion.Text = HttpUtility.HtmlDecode(vTercero.digito_verificacion.ToString().Trim());
            else
                txtDigitoVerificacion.Text = CalcularDigitoVerificacion(txtIdentificacion.Text);
            if (!string.IsNullOrEmpty(vTercero.primer_apellido))
                txtSigla.Text = HttpUtility.HtmlDecode(vTercero.primer_apellido.ToString().Trim());
            if (vTercero.cod_oficina != null)
                ddlOficina.SelectedValue = vTercero.cod_oficina.ToString();
            if (!string.IsNullOrEmpty(vTercero.razon_social))
                txtRazonSocial.Text = HttpUtility.HtmlDecode(vTercero.razon_social.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.direccion))
                txtDireccion.Text = HttpUtility.HtmlDecode(vTercero.direccion.ToString().Trim());
            if (vTercero.codciudadexpedicion.ToString() != "")
                ddlCiudad.SelectedValue = HttpUtility.HtmlDecode(vTercero.codciudadexpedicion.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.telefono))
                txtTelefono.Text = HttpUtility.HtmlDecode(vTercero.telefono.ToString().Trim());
            if (!string.IsNullOrEmpty(vTercero.email))
                txtEmail.Text = HttpUtility.HtmlDecode(vTercero.email.ToString().Trim());
            if (vTercero.fechaexpedicion != null)
                txtFecha.ToDateTime = Convert.ToDateTime(HttpUtility.HtmlDecode(vTercero.fechaexpedicion.ToString()));
            try
            {
                if (vTercero.actividadempresa != null)
                    ddlActividad.SelectedValue = HttpUtility.HtmlDecode(vTercero.actividadempresa.ToString().Trim());
            }
            catch { ddlActividad.SelectedValue = ""; }

            try
            {
                if (vTercero.EnteTerritorial == 1)
                    ChkEnteTerritorial.Checked = true;
                else
                    ChkEnteTerritorial.Checked = false;

            }
            catch { ChkEnteTerritorial.Checked = false; }


            if (!string.IsNullOrEmpty(vTercero.regimen))
                ddlRegimen.SelectedValue = HttpUtility.HtmlDecode(vTercero.regimen.ToString().Trim());
            if (vTercero.fechanacimiento != null)
            {
                txtFecConstitucion.Text = HttpUtility.HtmlDecode(vTercero.fechanacimiento.ToString());
                txtFecConstitucion_TextChanged(txtFecConstitucion,null);
            }
            try
            {
                ddlTipoActoCrea.SelectedValue = vTercero.tipo_acto_creacion.ToString();
            }
            catch
            {
            }

            if (vTercero.num_acto_creacion != "")
                txtNumActoCrea.Text = vTercero.num_acto_creacion;

            if (vTercero.celular != "")
                txtcelular.Text = vTercero.celular;
            //Agregado para cargar información de cámara de comercio y representante legal
            if (vTercero.camara_comercio != null)
                txtCam_Comercio.Text = HttpUtility.HtmlDecode(vTercero.camara_comercio.ToString());
            if (vTercero.cod_representante != 0)
            {                
                ctlPersona.AdicionarPersona(vTercero.id_representante.ToString(), vTercero.cod_representante, HttpUtility.HtmlDecode(vTercero.nom_representante.ToString()), Convert.ToInt32(vTercero.tipo_id_representante));
            }
            //    //txtCodRepresentante.Text = HttpUtility.HtmlDecode(vTercero.cod_representante.ToString());
            //if (vTercero.tipo_id_representante != 0)
            //    ddlTipoID.SelectedValue = HttpUtility.HtmlDecode(vTercero.tipo_id_representante.ToString());
            //if (vTercero.id_representante != 0)
            //    txtID.Text = HttpUtility.HtmlDecode(vTercero.id_representante.ToString());
            //if (vTercero.nom_representante != null)
            //    txtNombresR.Text = HttpUtility.HtmlDecode(vTercero.nom_representante.ToString());

            if (vTercero.tipo_empresa != 0)
                ddlTipoEmpresa.SelectedValue = vTercero.tipo_empresa.ToString();

            //Información de correspondecia
            if (vTercero.ubicacion_empresa != 0 && vTercero.ubicacion_empresa != null)
                ddlTipoUbic.SelectedValue = vTercero.ubicacion_empresa.ToString();
            if(vTercero.dircorrespondencia != "" && vTercero.dircorrespondencia != null)
                txtDir_Correspondencia.Text = HttpUtility.HtmlDecode(vTercero.dircorrespondencia.ToString());
            if (vTercero.ciucorrespondencia != 0 && vTercero.ciucorrespondencia != null)
                ddlCiudad_Corr.Text = HttpUtility.HtmlDecode(vTercero.ciucorrespondencia.ToString());
            if (vTercero.barcorrespondencia != 0 && vTercero.barcorrespondencia != null)
                ddlBarrio_Corr.Text = HttpUtility.HtmlDecode(vTercero.barcorrespondencia.ToString());
            if (vTercero.telcorrespondencia != "" && vTercero.telcorrespondencia != null)
                txtTel_Correspondencia.Text = HttpUtility.HtmlDecode(vTercero.telcorrespondencia.ToString());
            if (vTercero.cod_zona != 0 && vTercero.cod_zona != null)
                ddlZona.SelectedValue = HttpUtility.HtmlDecode(Convert.ToString(vTercero.cod_zona));

            //Lugar y fecha de registro
            if (vTercero.codciudadnacimiento != 0 && vTercero.codciudadnacimiento != null)
                ddlLugRegistro.SelectedValue = vTercero.codciudadnacimiento.ToString();
            if(vTercero.fecha_residencia != null && vTercero.fecha_residencia != DateTime.MinValue)
                txtFecha_Registro.Text = HttpUtility.HtmlDecode(vTercero.fecha_residencia.ToString());

            List<Actividades> lstActividad = _servicesActividad.ConsultarActividadesEconomicasSecundarias(vTercero.cod_persona, Usuario);
            // ACTIVIDADES CIIU

            Label lblCodigo;
            if (lstActividad != null)
            {
                if (gvActividadesCIIU.Rows.Count > 0)
                {
                    foreach (GridViewRow rFila in gvActividadesCIIU.Rows)
                    {
                        lblCodigo = (Label)rFila.FindControl("lbl_codigo");

                        //Identificar la actividad principal
                        if (lblCodigo.Text == vTercero.codactividadStr)
                        {
                            CheckBoxGrid chkPrincipal = rFila.FindControl("chkPrincipal") as CheckBoxGrid;
                            chkPrincipal.Checked = true;
                            Label lblDescripcion = (Label)rFila.FindControl("lbl_descripcion");
                            txtActividadCIIU.Text = lblDescripcion.Text;
                        }

                        foreach (Xpinn.FabricaCreditos.Entities.Actividades objActividad in lstActividad)
                        {
                            CheckBoxGrid chkSeleccionado = rFila.FindControl("chkSeleccionar") as CheckBoxGrid;

                            if (objActividad.codactividad == lblCodigo.Text)
                            {
                                chkSeleccionado.Checked = true;
                                break;
                            }
                        }
                    }
                }
            }

            //RECUPERAR INFORMACION ADICIONAL
            InformacionAdicionalServices infoService = new InformacionAdicionalServices();
            List<InformacionAdicional> LstInformacion = new List<InformacionAdicional>();

            LstInformacion = infoService.ListarPersonaInformacion(Convert.ToInt64(pIdObjeto), "J", (Usuario)Session["usuario"]);
            if (LstInformacion.Count > 0)
            {
                gvInfoAdicional.DataSource = LstInformacion;
                gvInfoAdicional.DataBind();
            }

            //RECUPERANDO DATOS DE EMPRESAS RECAUDO
            List<PersonaEmpresaRecaudo> LstEmpresaRecaudo = new List<PersonaEmpresaRecaudo>();
            PersonaEmpresaRecaudoServices infoEmpresaRecaudo = new PersonaEmpresaRecaudoServices();
            LstEmpresaRecaudo = infoEmpresaRecaudo.ListarPersonaEmpresaRecaudo(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
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

            //RECUPERAR INFORMACIÓN DE MONEDA EXTRANJERA
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

            //Recuperar datos de asociados
            List<Xpinn.Contabilidad.Entities.Tercero> lstAsociados = new List<Xpinn.Contabilidad.Entities.Tercero>();
            lstAsociados = TerceroServicio.ListarAsociados(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (lstAsociados.Count > 0)
            {
                Session["lstAsociados"] = lstAsociados;
                upAsociados.Visible = true;
                gvAsociados.DataSource = lstAsociados;
                gvAsociados.DataBind();
                chkAsociados_Empresa.Checked = true;
            }
            
            //RECUPERAR INFORMACIÓN ECONÓMICA
            EstadosFinancieros InformacionFinanciera = new EstadosFinancieros();
            List<EstadosFinancieros> lstLstInformacionF = new List<EstadosFinancieros>();

            InformacionFinanciera = EstadosFinancierosServicio.listarperosnainfofin(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (InformacionFinanciera != null)
            {
                if (InformacionFinanciera.ingresomensual != Int64.MinValue)
                    txtingreso_mensual.Text = HttpUtility.HtmlDecode(InformacionFinanciera.sueldo.ToString().Trim());
                if (InformacionFinanciera.otrosingresos != Int64.MinValue)
                    txtotrosIng_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.otrosingresos.ToString().Trim());
                if (InformacionFinanciera.egresomensual != Int64.MinValue)
                    txtegreso_mensual.Text = HttpUtility.HtmlDecode(InformacionFinanciera.totalegresos.ToString().Trim());
                if (InformacionFinanciera.TotAct != Int64.MinValue)
                    txtTotal_activos.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotAct.ToString().Trim());
                if (InformacionFinanciera.TotPas != Int64.MinValue)
                    txtTotal_pasivos.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotPas.ToString().Trim());
                if (InformacionFinanciera.TotPat != Int64.MinValue)
                    txtTotal_patrimonio.Text = HttpUtility.HtmlDecode(InformacionFinanciera.TotPat.ToString().Trim());

                txtConceptoOtros_soli.Text = InformacionFinanciera.conceptootros;

                if (InformacionFinanciera.totalingreso != Int64.MinValue)
                {
                    txttotalING_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.totalingreso.ToString().Trim());
                    hdtotalING_soli.Value = InformacionFinanciera.totalingreso.ToString();
                }

                if (InformacionFinanciera.totalegresos != Int64.MinValue)
                {
                    txttotalEGR_soli.Text = HttpUtility.HtmlDecode(InformacionFinanciera.totalegresos.ToString().Trim());
                    hdtotalEGR_soli.Value = InformacionFinanciera.totalegresos.ToString();
                }
            }


            #region RECUPERAR DATOS DE AFILIACIóN

            chkAsociado.Checked = false;
            chkAsociado.Enabled = true;
            Afiliacion pAfili = AfiliacionServicio.ConsultarAfiliacion(Convert.ToInt64(pIdObjeto), (Usuario)Session["usuario"]);
            if (pAfili.idafiliacion != 0)
            {
                txtcodAfiliacion.Text = Convert.ToString(pAfili.idafiliacion);
                chkAsociado.Checked = true;
                chkAsociado.Enabled = false;
            }
            if (pAfili.fecha_afiliacion != DateTime.MinValue)
                txtFechaAfili.Text = Convert.ToString(pAfili.fecha_afiliacion.ToString(gFormatoFecha));
            if (pAfili.estado != "")
                ddlEstadoAfi.SelectedValue = pAfili.estado;

            ddlEstadoAfi_SelectedIndexChanged(ddlEstadoAfi, null);
            if (pAfili.fecha_retiro != DateTime.MinValue)
                txtFechaRetiro.Text = Convert.ToString(pAfili.fecha_retiro.ToString(gFormatoFecha));
            if (pAfili.valor != null && pAfili.valor != 0)
                txtValorAfili.Text = Convert.ToString(pAfili.valor);
            if (pAfili.fecha_primer_pago != DateTime.MinValue && pAfili.fecha_primer_pago != null)
                txtFecha1Pago.Text = pAfili.fecha_primer_pago.Value.ToString(gFormatoFecha);
            if (pAfili.cuotas != 0)
                txtCuotasAfili.Text = Convert.ToString(pAfili.cuotas);
            if (pAfili.cod_periodicidad != 0)
                ddlPeriodicidad.SelectedValue = Convert.ToString(pAfili.cod_periodicidad);
            if (pAfili.forma_pago != 0)
                ddlFormaPago.SelectedValue = pAfili.forma_pago.ToString();
            ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
            if (pAfili.empresa_formapago != 0 && pAfili.empresa_formapago != null)
                ddlEmpresa.SelectedValue = pAfili.empresa_formapago.ToString();
            if (pAfili.cod_asesor.HasValue)
            {
                ddlAsesor.SelectedValue = pAfili.cod_asesor.ToString();
            }
            if (pAfili.cod_asociado_especial.HasValue)
                ddlAsociadosEspeciales.SelectedValue = pAfili.cod_asociado_especial.Value.ToString();

            chkAsociado_CheckedChanged(chkAsociado, null);

            if (pAfili.Administra_recursos_publicos)
                chkAdminRecursos.Checked = true;

            #endregion


            txtDigitoVerificacion.Enabled = false;
            txtIdentificacion.Enabled = false;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "ObtenerDatos", ex);
        }
    }

    private void CargarListas()
    {
        try
        {
            PoblarLista("Tipo_Acto_Creacion", ddlTipoActoCrea);
            PoblarLista("GR_PERFIL_RIESGO", "COD_PERFIL, DESCRIPCION", "TIPO_PERSONA IN ('J','T')", "1", ddlAsociadosEspeciales);

            List<Persona1> lstCiudades = new List<Persona1>();
            lstCiudades = TraerResultadosLista("Ciudades");

            // Llenar las listas que tienen que ver con ciudades
            ddlCiudad.DataTextField = "ListaDescripcion";
            ddlCiudad.DataValueField = "ListaId";
            ddlCiudad.DataSource = lstCiudades;
            ddlCiudad.DataBind();

            ddlCiudad_Corr.DataTextField = "ListaDescripcion";
            ddlCiudad_Corr.DataValueField = "ListaId";
            ddlCiudad_Corr.DataSource = lstCiudades;
            ddlCiudad_Corr.DataBind();

            ddlLugRegistro.DataTextField = "ListaDescripcion";
            ddlLugRegistro.DataValueField = "ListaId";
            ddlLugRegistro.DataSource = lstCiudades;
            ddlLugRegistro.DataBind();

            //Cargar barrio
            ddlBarrio_Corr.DataTextField = "ListaDescripcion";
            ddlBarrio_Corr.DataValueField = "ListaId";
            ddlBarrio_Corr.DataSource = TraerResultadosLista("Barrio");            
            ddlBarrio_Corr.DataBind();

            // Llenar la actividad
            ddlActividad.DataTextField = "ListaDescripcion";
            ddlActividad.DataValueField = "ListaIdStr";
            ddlActividad.DataSource = TraerResultadosLista("Actividad_Negocio");
            ddlActividad.DataBind();

            //listar Zona
            ddlZona.DataSource = TraerResultadosLista("Zona");
            ddlZona.DataTextField = "ListaDescripcion";
            ddlZona.DataValueField = "ListaId";
            ddlZona.DataBind();

            List<Persona1> lstActividades = TraerResultadosLista("Actividad2");
            ViewState["DTACTIVIDAD" + Usuario.codusuario] = lstActividades;
            gvActividadesCIIU.DataSource = lstActividades;
            gvActividadesCIIU.DataBind();

            ddlEstadoAfi.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlEstadoAfi.Items.Insert(1, new ListItem("Activo", "A"));
            ddlEstadoAfi.Items.Insert(2, new ListItem("Retirado", "R"));
            ddlEstadoAfi.Items.Insert(3, new ListItem("Inactivo", "I"));
            ddlEstadoAfi.SelectedIndex = 0;
            ddlEstadoAfi.DataBind();

            PoblarLista("periodicidad", ddlPeriodicidad);

            ddlEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));

            LlenarListasDesplegables(TipoLista.Asesor, ddlAsesor);
            LlenarListasDesplegables(TipoLista.Oficinas, ddlOficina);
            ctlFormatos.Inicializar("1");

            ddlTipoEmpresa.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlTipoEmpresa.Items.Insert(1, new ListItem("Pública", "1"));
            ddlTipoEmpresa.Items.Insert(2, new ListItem("Privada", "2"));
            ddlTipoEmpresa.Items.Insert(3, new ListItem("Mixta", "3"));
            ddlTipoEmpresa.SelectedIndex = 0;
            ddlTipoEmpresa.DataBind();

            //Listas Modal
            LlenarDDLTipoIdentificacion();
            LlenarDDLTipoActivo();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(TerceroServicio.GetType().Name + "L", "CargarListas", ex);
        }
    }

    private List<Xpinn.FabricaCreditos.Entities.Persona1> TraerResultadosLista(string ListaSolicitada)
    {
        Xpinn.FabricaCreditos.Services.Persona1Service DatosClienteServicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
        List<Xpinn.FabricaCreditos.Entities.Persona1> lstDatosSolicitud = new List<Xpinn.FabricaCreditos.Entities.Persona1>();
        lstDatosSolicitud.Clear();
        lstDatosSolicitud = DatosClienteServicio.ListasDesplegables(ListaSolicitada, (Usuario)Session["Usuario"]);
        return lstDatosSolicitud;
    }

    protected void imgAceptar_Click(object sender, ImageClickEventArgs e)
    {

        Session.Remove("Iniciar");
        if (rbTipoPersona.SelectedValue == "J")
        {
            mvDatos.ActiveViewIndex = 1;
            Site toolBar = (Site)this.Master;
            toolBar.MostrarGuardar(true);
        }
        else if (rbTipoPersona.SelectedValue == "N") { 

        Navegar("../Personas/Tab/Persona.aspx");
        }
        else
            Navegar("../Personas/NuevoMDE.aspx");
    }


    protected void gvInfoAdicional_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtCadena = (TextBox)e.Row.FindControl("txtCadena");
            TextBox txtNumero = (TextBox)e.Row.FindControl("txtNumero");
            fecha txtctlfecha = (fecha)e.Row.FindControl("txtctlfecha");
            txtCadena.Visible = false;
            txtNumero.Visible = false;
            txtctlfecha.Visible = false;

            DropDownListGrid ddlDropdown = (DropDownListGrid)e.Row.FindControl("ddlDropdown");
            ddlDropdown.Visible = false;

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

                    Label lblDropdown = (Label)e.Row.FindControl("lblDropdown");

                    if (lblDropdown.Text != "")
                        Session["Datos"] = lblDropdown.Text;
                    if (ddlDropdown != null)
                    {
                        string[] sDatos;

                        if (lblDropdown.Text != "")
                        {
                            sDatos = lblDropdown.Text.Split(',');
                        }
                        else
                        {
                            if (Session["Datos"] != null)
                                sDatos = Session["Datos"].ToString().Split(',');
                            else
                                sDatos = new string[0];
                        }
                        if (sDatos.Count() > 0)
                        {
                            ddlDropdown.Items.Clear();
                            for (int i = 0; i < sDatos.Count(); i++)
                            {
                                ddlDropdown.Items.Insert(i, new ListItem(sDatos[i].ToString(), sDatos[i].ToString()));
                            }
                            ddlDropdown.DataBind();
                        }
                    }

                    Label lblValorDropdown = (Label)e.Row.FindControl("lblValorDropdown");
                    if (lblValorDropdown.Text != "")
                    {
                        ddlDropdown.SelectedValue = lblValorDropdown.Text;
                    }
                }
            }
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


    protected void ddlEstadoAfi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEstadoAfi.SelectedItem.Text == "Activo")
            panelFecha.Enabled = false;
        else
            panelFecha.Enabled = true;
    }


    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Value == "2" || ddlFormaPago.SelectedItem.Text == "Nomina")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;
            RECUPERAR_EMPRESAS_NOMINA();
        }
        else
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }
    }

    /// <summary>
    /// Método para calcular la antiguedad en base a la fecha de constitución
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtFecConstitucion_TextChanged(object sender, EventArgs e)
    {
        txtAntiguedad.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFecConstitucion.Text)));
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

    protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            vPersona1.seleccionar = "Identificacion";
            vPersona1.noTraerHuella = 1;
            vPersona1.identificacion = txtIdentificacion.Text;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);
            if (idObjeto != "")
            {
                if (vPersona1.identificacion != "" && vPersona1.identificacion != null)
                    VerError("ERROR: El NIT ingresado ya existe");
            }
            else
            {
                if (vPersona1.identificacion != "" && vPersona1.identificacion != null)
                    VerError("ERROR: El NIT ingresado ya existe");
            }
            txtDigitoVerificacion.Text = CalcularDigitoVerificacion(txtIdentificacion.Text);
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected string CalcularDigitoVerificacion(string Nit)
    {
        string Temp;
        int Contador;
        int Residuo;
        int Acumulador;
        int[] Vector = new int[15];

        Vector[0] = 3;
        Vector[1] = 7;
        Vector[2] = 13;
        Vector[3] = 17;
        Vector[4] = 19;
        Vector[5] = 23;
        Vector[6] = 29;
        Vector[7] = 37;
        Vector[8] = 41;
        Vector[9] = 43;
        Vector[10] = 47;
        Vector[11] = 53;
        Vector[12] = 59;
        Vector[13] = 67;
        Vector[14] = 71;

        Acumulador = 0;

        Residuo = 0;

        for (Contador = 0; Contador < Nit.Length; Contador++)
        {
            Temp = Nit.Substring((Nit.Length - 1) - Contador, 1);
            Acumulador = Acumulador + (Convert.ToInt32(Temp) * Vector[Contador]);
        }

        Residuo = Acumulador % 11;

        return Residuo > 1 ? Convert.ToString(11 - Residuo) : Residuo.ToString();
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Determinar programación de la pagaduría
        if (ddlEmpresa.SelectedItem != null)
        {
            if (!txtFechaAfili.TieneDatos)
                txtFechaAfili.ToDateTime = System.DateTime.Now;
            try
            {
                DateTime? FechaInicio = AfiliacionServicio.FechaInicioAfiliacion(Convert.ToDateTime(txtFechaAfili.ToDate), Convert.ToInt64(ddlEmpresa.SelectedItem.Value), (Usuario)Session["Usuario"]);
            }
            catch
            { }
        }
    }


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
                if (vDetalle.valor != 0)
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
            }
            //else
            //{
            //    txtValorAfili.Enabled = true;
            //}
            if (vDetalle.numero_cuotas != 0)
                txtCuotasAfili.Text = vDetalle.numero_cuotas.ToString();
            if (vDetalle.cod_periodicidad != 0)
                ddlPeriodicidad.SelectedValue = vDetalle.cod_periodicidad.ToString();

        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }
    protected void chkAsociado_CheckedChanged(object sender, EventArgs e)
    {
        panelAfiliacion.Enabled = chkAsociado.Checked == true ? true : false;
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

    private void RECUPERAR_EMPRESAS_NOMINA()
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
                    ddlEmpresa.Items.Insert(cont, new ListItem(lblDescripcion.Text, lblcodempresa.Text));
            }
        }
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
    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        ctlFormatos.lblErrorText = "";
        if (ctlFormatos.ddlFormatosItem != null)
            ctlFormatos.ddlFormatosIndex = 0;
        ctlFormatos.MostrarControl();
    }
    protected void btnImpresion_Click(object sender, EventArgs e)
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
            string pRuta = "~/Page/Aportes/Afiliaciones/Documentos/";
            string pVariable = txtCodigo.Text.Trim();
            ctlFormatos.ImprimirFormato(pVariable, pRuta);

            //Descargando el Archivo PDF
            string cNombreDeArchivo = pVariable.Trim() + "_" + ctlFormatos.ddlFormatosValue + ".pdf";
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
    protected void btnVerData_Click(object sender, EventArgs e)
    {
        panelFinal.Visible = false;
    }

    protected void btnConsultaRepresentante_Click(object sender, EventArgs e)
    {
        //ctlBusquedaPersonas.Motrar(true, "txtCodRepresentante", "txtID", "ddlTipoID", "txtNombresR");
    }

    protected void LlenarComboTipoIden(DropDownList ddlTipoID)
    {
        Xpinn.Caja.Services.TipoIdenService IdenService = new Xpinn.Caja.Services.TipoIdenService();
        Xpinn.Caja.Entities.TipoIden identi = new Xpinn.Caja.Entities.TipoIden();
        ddlTipoID.DataSource = IdenService.ListarTipoIden(identi, (Usuario)Session["Usuario"]);
        ddlTipoID.DataTextField = "descripcion";
        ddlTipoID.DataValueField = "codtipoidentificacion";
        ddlTipoID.DataBind();
    }

    private void DeshabilitarControlesActivarReadOnly()
    {
        txttotalING_soli.Attributes.Add("readonly", "readonly");
        txttotalEGR_soli.Attributes.Add("readonly", "readonly");
    }

    protected void chkAsociados_Empresa_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAsociados_Empresa.Checked)
            upAsociados.Visible = true;
        else
            upAsociados.Visible = false;
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

    protected void btnAgregarFila_Click(object sender, EventArgs e)
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
                Moneda.num_cuenta_ext = txtNumCuentaExt.Text != null && txtNumCuentaExt.Text != "" ? txtNumCuentaExt.Text: "";

            TextBox txtNomBancoExt = (TextBox)filaMoneda.FindControl("txtNomBancoExt");
            if (txtNomBancoExt != null)
                Moneda.banco_ext = Convert.ToString(txtNomBancoExt.Text);

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

            Producto.desc_operacion = "";
            Producto.banco_ext = "";

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
    
    #region Metodos MODAL Llenado - Eventos - Validaciones

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

    protected void InicializarModal(object sender, EventArgs e)
    {
        txtModalNombres.Text = txtRazonSocial.Text;
        txtModalIdentificacion.Text = txtIdentificacion.Text;
        //ddlModalIdentificacion.SelectedValue = ddlTipoID.SelectedValue;
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
            LlenarGVActivoFijos(txtCodigo.Text);
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
        activoFijo.cod_persona = Convert.ToInt64(txtCodigo.Text);
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

    /// <summary>
    /// Método para eliminar un activo de la tabla de bienes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvBienesActivos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (!btnBienesActivos.Visible)
            {
                return;
            }

            GarantiaService garantiaService = new GarantiaService();
            List<Garantia> lstReferencia = RecorresGrillaBienes();
            int index = Convert.ToInt32(e.RowIndex);
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
        catch (Exception ex)
        {
            VerError("gvBienesActivos_OnRowCommand, " + ex.Message);
        }
    }

    /// <summary>
    /// Método para generar la ventana modal y editar el activo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
    
    /// <summary>
    /// Obtener datos de la tabla de bienes
    /// </summary>
    /// <returns></returns>
    private List<Garantia> RecorresGrillaBienes()
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

    /// <summary>
    /// Llenar datos de activos en la tabla
    /// </summary>
    /// <param name="objeto"></param>
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

    /// <summary>
    /// Inicializar la tabla de activos
    /// </summary>
    private void InicializarBienesActivosFijos()
    {
        List<Garantia> lstBienes = new List<Garantia>(1) { new Garantia() };
        gvBienesActivos.DataSource = lstBienes;
        gvBienesActivos.DataBind();
    }
       
    /// <summary>
    /// Cargar datos del bien a la ventana modal
    /// </summary>
    /// <param name="IdGarantia"></param>
    protected void ObtenerBienesActivos(Int64 IdGarantia)
    {
        GarantiaService garantiasservicio = new GarantiaService();

        VerError("");
        try
        {
            lblTipoProceso.Text = IdGarantia.ToString();
            ddlEstadoModal.Enabled = true;
            txtModalNombres.Text = txtRazonSocial.Text;
            txtModalIdentificacion.Text = txtIdentificacion.Text;
            //ddlModalIdentificacion.SelectedValue = ddlTipoID.SelectedValue;

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

    #endregion

    #region Panel de asociados

    private List<Xpinn.Contabilidad.Entities.Tercero> ObtenerAsociados()
    {
        List<Xpinn.Contabilidad.Entities.Tercero> lstAsociados = new List<Xpinn.Contabilidad.Entities.Tercero>();
        List<Xpinn.Contabilidad.Entities.Tercero> lista = new List<Xpinn.Contabilidad.Entities.Tercero>();

        foreach (GridViewRow rfila in gvAsociados.Rows)
        {
            Xpinn.Contabilidad.Entities.Tercero vAsociado = new Xpinn.Contabilidad.Entities.Tercero();
            vAsociado.cod_representante = Convert.ToInt64(gvAsociados.DataKeys[rfila.RowIndex].Values[0].ToString());
            vAsociado.cod_representante = Convert.ToInt64(gvAsociados.DataKeys[rfila.RowIndex].Values[0].ToString());
            DropDownList ddlTipo_ID = (DropDownList)rfila.FindControl("ddlTipo_ID");
            if (ddlTipo_ID != null)
                vAsociado.tipo_identificacion = ddlTipo_ID.SelectedIndex > 0 ? Convert.ToInt64(ddlTipo_ID.SelectedValue) : 0;
            TextBox txtID_Asociado = (TextBox)rfila.FindControl("txtID_Asociado");
            if (txtID_Asociado != null)
                vAsociado.identificacion = txtID_Asociado.Text != "" && txtID_Asociado.Text != null ? txtID_Asociado.Text : "";
            TextBox txtNombres = (TextBox)rfila.FindControl("txtNombres");
            if (txtNombres != null)
                vAsociado.nombres = txtNombres.Text != "" && txtNombres.Text != null ? txtNombres.Text : "";
            fecha txtFechaExp = (fecha)rfila.FindControl("txtFechaExp");
            if (txtFechaExp != null)
                vAsociado.fechaexpedicion = txtFechaExp.Text != "" && txtFechaExp.Text != null ? Convert.ToDateTime(txtFechaExp.Text) : DateTime.MinValue;
            TextBox txtPorcentaje = (TextBox)rfila.FindControl("txtPorcentaje");
            if (txtPorcentaje != null)
                vAsociado.porcentaje_patrimonio = txtPorcentaje.Text != "" && txtPorcentaje.Text != null ? Convert.ToInt32(txtPorcentaje.Text) : 0;
            //Agregado
            DropDownList ddlCotiza = (DropDownList)rfila.FindControl("ddlCotiza");
            if (ddlCotiza != null)
                vAsociado.cotiza_bolsa = Convert.ToInt32(ddlCotiza.SelectedValue);

            DropDownList ddlVinculaPEP = (DropDownList)rfila.FindControl("ddlVinculaPEP");
            if (ddlVinculaPEP != null)
                vAsociado.vincula_pep = Convert.ToInt32(ddlVinculaPEP.SelectedValue);

            DropDownList ddlTributacion = (DropDownList)rfila.FindControl("ddlTributacion");
            if (ddlTributacion != null)
                vAsociado.tributacion = Convert.ToInt32(ddlTributacion.SelectedValue);

            lstAsociados.Add(vAsociado);
        }

        Session["lstAsociados"] = lstAsociados;
        return lstAsociados;
    }

    protected void btnDetalle_Asociado_Click(object sender, EventArgs e)
    {
        ObtenerAsociados();
        List<Xpinn.Contabilidad.Entities.Tercero> lstAsociados = new List<Xpinn.Contabilidad.Entities.Tercero>();
        if (Session["lstAsociados"] != null)
        {
            lstAsociados = (List<Xpinn.Contabilidad.Entities.Tercero>)Session["lstAsociados"];
            for (int i = 1; i <= 1; i++)
            {
                Xpinn.Contabilidad.Entities.Tercero vAsociado = new Xpinn.Contabilidad.Entities.Tercero();
                vAsociado.cod_representante = 0;
                vAsociado.tipo_identificacion = 0 ;
                vAsociado.identificacion = "";
                vAsociado.nombres = "";
                vAsociado.fechaexpedicion = null;
                vAsociado.porcentaje_patrimonio = 0;

                lstAsociados.Add(vAsociado);
            }
            gvAsociados.DataSource = lstAsociados;
            gvAsociados.DataBind();
            Session["lstAsociados"] = lstAsociados;
        }
    }

    protected void gvAsociados_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int cod = Convert.ToInt32(gvAsociados.DataKeys[e.RowIndex].Values[0].ToString());

        ObtenerAsociados();

        List<Xpinn.Contabilidad.Entities.Tercero> lstAsociados;

        lstAsociados = (List<Xpinn.Contabilidad.Entities.Tercero>)Session["lstAsociados"];

        if (cod > 0)
        {
            try
            {
                foreach (Xpinn.Contabilidad.Entities.Tercero vAsociado in lstAsociados)
                {
                    if (vAsociado.cod_representante == cod)
                    {
                        //EstadosFinancierosServicio.EliminarCuentasMonedaExtranjera(cod, (Usuario)Session["usuario"]);
                        //LstProductos.Remove(eMoneda);
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
            lstAsociados.RemoveAt((gvAsociados.PageIndex * gvAsociados.PageSize) + e.RowIndex);
        }
        gvAsociados.DataSourceID = null;
        gvAsociados.DataBind();

        gvAsociados.DataSource = lstAsociados;
        gvAsociados.DataBind();

        Session["lstAsociados"] = lstAsociados;
    }
    
    protected void gvAsociados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            List<Xpinn.Contabilidad.Entities.Tercero> lstAsociados = (List<Xpinn.Contabilidad.Entities.Tercero>)Session["lstAsociados"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int cod = Convert.ToInt32(gvAsociados.DataKeys[e.Row.RowIndex].Values[0]);
                DropDownList ddlTipo_ID = (DropDownList)e.Row.FindControl("ddlTipo_ID");

                //AGREGADO
                DropDownList ddlCotiza = (DropDownList)e.Row.FindControl("ddlCotiza");
                DropDownList ddlVinculaPEP = (DropDownList)e.Row.FindControl("ddlVinculaPEP");
                DropDownList ddlTributacion = (DropDownList)e.Row.FindControl("ddlTributacion");

                LlenarListasDesplegables(TipoLista.TipoIdentificacion, ddlTipo_ID);

                if (lstAsociados != null)
                {
                    foreach (Xpinn.Contabilidad.Entities.Tercero vAsociado in lstAsociados)
                    {
                        if (vAsociado.cod_persona == cod)
                        {
                            ddlTipo_ID.SelectedValue = vAsociado.tipo_identificacion.ToString();
                            ddlCotiza.SelectedValue =  vAsociado.cotiza_bolsa.ToString();
                            ddlVinculaPEP.SelectedValue = vAsociado.vincula_pep.ToString();
                            ddlTributacion.SelectedValue = vAsociado.tributacion.ToString();
                        }
                        break;
                    }
                }


            }
        }
        catch (Exception ex)
        {
            VerError("Error al cargar los datos - Panel Asociados" + ex.Message.ToString());
        }
    }

    #endregion


    


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
                else if (!string.IsNullOrEmpty(txtBuscarCodigo.Text.Trim()))
                    lstActividad = lstActividad.Where(x => x.ListaDescripcion.Contains(txtBuscarDescripcion.Text)).ToList();
                gvActividadesCIIU.DataSource = lstActividad;
                gvActividadesCIIU.DataBind();
            }
        }
        MostrarModal();
    }

    private void MostrarModal()
    {
        var ahh = PopupControlExtenderActividades.ClientID;
        var script = @"Sys.Application.add_load(function() { $find('" + ahh + "').showPopup(); });";
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowPopup", script, true);
    }

    #endregion
    
}