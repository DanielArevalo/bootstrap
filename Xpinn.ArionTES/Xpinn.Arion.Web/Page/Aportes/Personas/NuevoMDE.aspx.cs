using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.IO;
using System.Drawing.Imaging;
using Xpinn.Aportes.Services;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Services;
using Xpinn.FabricaCreditos.Entities;
using System.Text.RegularExpressions;

public partial class NuevoMDE : GlobalWeb
{
    private Xpinn.Aportes.Services.AfiliacionServices AfiliacionServicio = new Xpinn.Aportes.Services.AfiliacionServices();
    Xpinn.FabricaCreditos.Services.Persona1Service persona1Servicio = new Xpinn.FabricaCreditos.Services.Persona1Service();
    PoblarListas poblarLista = new PoblarListas();

    private void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
                VisualizarOpciones(AfiliacionServicio.CodigoPrograma, "E");
            else
                VisualizarOpciones(AfiliacionServicio.CodigoPrograma, "A");

            Site toolBar = (Site)this.Master;
            toolBar.eventoGuardar += btnGuardar_Click;
            toolBar.eventoCancelar += btnCancelar_Click;
            ctlMensaje.eventoClick += btnContinuarMen_Click;
            toolBar.eventoImprimir += btnImprimir_Click;
            ctlFormatos.eventoClick += btnImpresion_Click;
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma + "A", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
                  {
                ctlPersona.Requerido = false;
                obtenerControlesAdicionales();
                //Asignando formato al Calendar Extender de Fecha de nacimiento
                txtFechanacimiento_CalendarExtender.DaysModeTitleFormat = FormatoFecha();
                txtFechanacimiento_CalendarExtender.Format = FormatoFecha();
                txtFechanacimiento_CalendarExtender.TodaysDateFormat = FormatoFecha();

                CargarDropDown();
                Session["IDENTIFICACION"] = null;

                VerificarLlamadoEstadoCuenta();

                if (Session[AfiliacionServicio.CodigoPrograma + ".id"] != null)
                {
                    idObjeto = Session[AfiliacionServicio.CodigoPrograma + ".id"].ToString();
                    Session.Remove(AfiliacionServicio.CodigoPrograma + ".id");
                    ObtenerDatos(idObjeto);
                    if (txtFechaAfili.TieneDatos)
                        txtFechaAfili.Enabled = false;
                    Session["IDENTIFICACION"] = txtIdentificacionE.Text;
                    if (Session[AfiliacionServicio.CodigoPrograma + ".modificar"].ToString() == "1")
                    {
                        Site toolBar = (Site)this.Master;
                        toolBar.MostrarGuardar(false);
                    }
                    ddlOficina.Enabled = false;
                    txtIdentificacionE.Enabled = false;
                }
                else
                {
                    Calcular_Valor_Afiliacion();
                    txtFechaAfili.Text = System.DateTime.Now.ToString(gFormatoFecha);
                    ddlFormaPago.SelectedValue = "2";
                    ddlEstadoAfi.SelectedIndex = 1;
                    ddlTipoE.SelectedIndex = 1;
                    ddlFormaPago_SelectedIndexChanged(ddlFormaPago, null);
                    ddlEstadoAfi_SelectedIndexChanged(ddlEstadoAfi, null);
                    chkAsociado_CheckedChanged(chkAsociado, null);
                }
            }
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

    protected void CargarDropDown()
    {
        try
        {
            ctlFormatos.Inicializar("1");
            poblarLista.PoblarListaDesplegable("TIPOIDENTIFICACION", "", "TIPO_PERSONA IN (0, 2)", "2", ddlTipoE, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("OFICINA", "COD_OFICINA,NOMBRE", "ESTADO = 1", "2", ddlOficina, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("CIUDADES", "CODCIUDAD,NOMCIUDAD", "TIPO = 3", "2", ddlLugarExpedicion, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("CIUDADES", "CODCIUDAD,NOMCIUDAD", "TIPO = 3", "2", ddlCiuCorrespondencia, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("BARRIO", "CODBARRIO,NOMBRE", "", "2", ddlBarrioCorrespondencia, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("CIUDADES", "CODCIUDAD,NOMCIUDAD", "TIPO = 3", "2", ddlLugarNacimiento, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("CIUDADES", "CODCIUDAD,NOMCIUDAD", "TIPO = 1", "2", ddlPais, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("NIVELESCOLARIDAD", ddlNivelEscolaridad, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("ESTADO_PERSONA", ddlEstadoAfi, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("PERIODICIDAD", ddlPeriodicidad, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("EMPRESA_RECAUDO", "", "ESTADO = 1", "2", ddlEmpresa, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("asejecutivos", "ICODIGO, QUITARESPACIOS(Substr(snombre1 || ' ' || snombre2 || ' ' || sapellido1 || ' ' || sapellido2, 0, 240))", "", "", ddlAsesor, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("ZONAS", "", "", "", ddlZona, (Usuario)Session["usuario"]);
            poblarLista.PoblarListaDesplegable("parentescos", "codparentesco,descripcion", "", "", ddlParentesco, (Usuario)Session["usuario"]);
            
            
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "CargarDropDown", ex);
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
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void obtenerControlesAdicionales()
    {
        InformacionAdicionalServices informacion = new InformacionAdicionalServices();
        InformacionAdicional pInfo = new InformacionAdicional();
        List<InformacionAdicional> lstControles = new List<InformacionAdicional>();
        string tipo = "M";
        lstControles = informacion.ListarInformacionAdicional(pInfo, tipo, (Usuario)Session["usuario"]);
        if (lstControles.Count > 0)
        {
            gvInfoAdicional.DataSource = lstControles;
            gvInfoAdicional.DataBind();
        }
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
            Int64 pCod_Persona = 0;
            pCod_Persona = vPersona1.cod_persona;
            vPersona1.seleccionar = "Cod_persona";
            vPersona1.soloPersona = 1;
            vPersona1 = persona1Servicio.ConsultarPersona1Param(vPersona1, (Usuario)Session["usuario"]);

            if (vPersona1.nombre != "errordedatos")
            {
                if (vPersona1.cod_persona != Int64.MinValue)
                    txtCod_persona.Text = HttpUtility.HtmlDecode(vPersona1.cod_persona.ToString().Trim());
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
                if (vPersona1.zona != null && vPersona1.zona != 0)
                    ddlZona.SelectedValue = vPersona1.zona.ToString();
                if (!string.IsNullOrEmpty(vPersona1.telCorrespondencia))
                    txtTelCorrespondencia.Text = HttpUtility.HtmlDecode(vPersona1.telCorrespondencia.ToString().Trim());
                if (!string.IsNullOrEmpty(vPersona1.celular))
                    txtcelular.Text = HttpUtility.HtmlDecode(vPersona1.celular.ToString().Trim());
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
                if (vPersona1.tipo_identificacion != Int64.MinValue)
                    ddlTipoE.SelectedValue = HttpUtility.HtmlDecode(vPersona1.tipo_identificacion.ToString().Trim());
                if (vPersona1.fechaexpedicion != DateTime.MinValue && vPersona1.fechaexpedicion != null)
                    txtFechaexpedicion.Text = HttpUtility.HtmlDecode(vPersona1.fechaexpedicion.Value.ToShortDateString());
                else
                    txtFechaexpedicion.Text = "";
                if (vPersona1.codciudadexpedicion != Int64.MinValue && vPersona1.codciudadexpedicion != null && vPersona1.codciudadexpedicion != -1 && vPersona1.codciudadexpedicion != 0)
                    ddlLugarExpedicion.SelectedValue = HttpUtility.HtmlDecode(vPersona1.codciudadexpedicion.ToString().Trim());
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
                if (vPersona1.cod_oficina != Int64.MinValue)
                    ddlOficina.SelectedValue = HttpUtility.HtmlDecode(vPersona1.cod_oficina.ToString().Trim());

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
                if (!string.IsNullOrEmpty(vPersona1.email))
                    txtEmail.Text = vPersona1.email.ToString();
                if (vPersona1.Estrato != null && vPersona1.Estrato != 0)
                    txtEstrato.Text = vPersona1.Estrato.ToString();
                if (vPersona1.nacionalidad != 0 && vPersona1.nacionalidad != null)
                    ddlPais.SelectedValue = vPersona1.nacionalidad.ToString();
                if (vPersona1.acceso_oficina != 1)
                    checkOficinaVirtual.Checked = false;
                else
                    checkOficinaVirtual.Checked = true;

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

                Int64 pCod = Convert.ToInt64(pIdObjeto);

                List<InformacionAdicional> LstInformacion = new List<InformacionAdicional>();

                InformacionAdicionalServices infoService = new InformacionAdicionalServices();
                LstInformacion = infoService.ListarPersonaInformacion(pCod, "N", (Usuario)Session["usuario"]);
                if (LstInformacion.Count > 0)
                {
                    gvInfoAdicional.DataSource = LstInformacion;
                    gvInfoAdicional.DataBind();
                }
                CalcularEdad();

                ObtenerDatosAfiliacion(pCod_Persona);

                //DATOS DEL TUTOR 
                PersonaResponsable pResponsable = new PersonaResponsable();
                string pFiltro = "WHERE R.COD_PERSONA = " + txtCod_persona.Text.Trim();
                pResponsable = AfiliacionServicio.ConsultarPersonaResponsable(pFiltro, (Usuario)Session["usuario"]);
                if (pResponsable != null && pResponsable.consecutivo != 0)
                {
                    lblCodTutor.Text = pResponsable.consecutivo.ToString();
                    if (pResponsable.identificacion != null && pResponsable.cod_persona_tutor != 0)
                        ctlPersona.AdicionarPersona(pResponsable.identificacion, pResponsable.cod_persona_tutor, pResponsable.nombre_ter, pResponsable.tipo_identificacion);
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


    private void ObtenerDatosAfiliacion(Int64 cod_persona)
    {
        Afiliacion pAfili = new Afiliacion();
       

        pAfili = AfiliacionServicio.ConsultarAfiliacion(cod_persona, (Usuario)Session["usuario"]);
        chkAsociado.Checked = false;
        chkAsociado.Enabled = true;
        if (pAfili.idafiliacion != 0)
        {
            txtcodAfiliacion.Text = Convert.ToString(pAfili.idafiliacion);
            chkAsociado.Checked = true;
            chkAsociado.Enabled = false;
            if (pAfili.fecha_afiliacion != DateTime.MinValue)
                txtFechaAfili.Text = Convert.ToString(pAfili.fecha_afiliacion.ToString(gFormatoFecha));
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



    protected Boolean validarDatos()
    {
        VerError("");
        if (txtIdentificacionE.Text == "")
        {
            VerError("Ingrese el Nro de Identificación - Datos Básicos");
            txtIdentificacionE.Focus();
            return false;
        }
        if (ddlOficina.SelectedValue == "" || ddlOficina.SelectedIndex == 0)
        {
            VerError("Debe seleccionar la Oficina - Datos Básicos");
            ddlOficina.Focus();
            return false;
        }
        if (ddlLugarExpedicion.SelectedValue == "" || ddlLugarExpedicion.SelectedIndex == 0)
        {
            VerError("Debe seleccionar el lugar de expedición - Datos Básicos");
            ddlLugarExpedicion.Focus();
            return false;
        }
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
        if (txtcelular.Text == "")
        {
            VerError("Ingrese el telefono  Celular  - Datos de Localización");
            txtcelular.Focus();
            return false;
        }
        if (txtFechanacimiento.Text == "")
        {
            VerError("Ingrese la fecha de nacimiento - Datos Generales");
            txtFechanacimiento.Focus();
            return false;
        }
        if (txtEdadCliente.Text == "")
        {
            if (Convert.ToInt32(txtEdadCliente.Text) < 0 && Convert.ToInt32(txtEdadCliente.Text) > 90)
            {
                VerError("La Edad del cliente debe estar entre 0 y 90 años, Verifique la fecha de Nacimiento Ingresada");
                return false;
            }
        }
        if (ddlNivelEscolaridad.Text == "" || ddlNivelEscolaridad.SelectedIndex == 0)
        {
            VerError("Ingrese el nivel de escolaridad - Datos Generales");
            ddlNivelEscolaridad.Focus();
            return false;
        }
        if (ddlLugarNacimiento.Text == "" || ddlLugarNacimiento.SelectedIndex == 0)
        {
            VerError("Ingrese la ciudad de nacimiento - Datos Generales");
            ddlLugarNacimiento.Focus();
            return false;
        }
        if (txtEmail.Text != "")
        {
            bool rpta = IsValidEmail(txtEmail.Text);
            if (rpta == false)
            {
                VerError("El Email ingresado no tiene el formato correcto");
                return false;
            }
        }

        //Validar los datos de Afiliacion
        if (chkAsociado.Checked)
        {
            if (txtFechaAfili.Text == "")
            {
                VerError("Ingrese la fecha de Afiliación - Afiliación");
                return false;
            }

            //Agregado para validar que no se modifique la fecha de afiliación si el asociado tiene aportes con saldo
            if (idObjeto != "")
            {
                List<Aporte> lstAporte = new List<Aporte>();
                List<Aporte> lstAportesActivos = new List<Aporte>();
                AporteServices aporteServicio = new AporteServices();
                //Agregado para validar que no se modifique la fecha de afiliación si el asociado tiene aportes con saldo
                DateTime? fecAfiliacion = AfiliacionServicio.FechaAfiliacion(txtCod_persona.Text, (Usuario)Session["usuario"]);
                if (fecAfiliacion != null)
                {
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

            if (txtFechaAfili.Text != "" && ddlEstadoAfi.SelectedIndex != 0 && txtValorAfili.Text != "")
            {
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
            }
        }
        if (ctlPersona.Text == "")
        {
            VerError("Ingrese el tutor o responsable del Menor - Afiliación");
            return false;
        }
        if (ctlPersona.TextCodigo == "")
        {
            VerError("Ingrese un tutor o responsable válido del Menor - Afiliación");
            return false;
        }
        if (txtFechaAfili.Text != "" && txtFechaAfili.Text != null)
        {
            if (Convert.ToDateTime(txtFechaAfili.Text) != DateTime.MinValue && idObjeto == "")
            {
                //Validar fecha de cierre de personas
                AfiliacionServices AfiliacionServicio = new AfiliacionServices();
                Afiliacion pAfiliacion = AfiliacionServicio.ConsultarCierrePersonas((Usuario)Session["usuario"]);

                if (Convert.ToDateTime(txtFechaAfili.Texto) < pAfiliacion.fecha_cierre && pAfiliacion.estadocierre == "D")
                {
                    VerError("No se pueden registrar afiliaciones en periodos ya cerrados");
                    return false;
                }
            }
        }
        return true;
    }

    public static bool IsValidEmail(string strMailAddress)
    {
        // Return true if strIn is in valid e-mail format.
        //return Regex.IsMatch(strMailAddress, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
        return Regex.IsMatch(strMailAddress, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }

    protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            VerError("");
            if (validarDatos())
            {
                string msj = idObjeto == "" ? "grabación" : "modificación";
                ctlMensaje.MostrarMensaje("Desea realizar la " + msj + " de la persona?");
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "btnGuardar_Click", ex);
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();

            Usuario pUsuario = new Usuario();
            pUsuario = (Usuario)Session["usuario"];

            if (idObjeto != "")
            {
                // Consultar datos ya existentes de la persona si se va a modificar
                vPersona1 = persona1Servicio.ConsultarPersona1(Convert.ToInt64(idObjeto), pUsuario);
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
            vPersona1.digito_verificacion = 0;
            if (txtCod_persona.Text != "") vPersona1.cod_persona = Convert.ToInt64(txtCod_persona.Text.Trim());

            //DATOS DE LA PERSONA
            vPersona1.identificacion = (txtIdentificacionE.Text != "") ? Convert.ToString(txtIdentificacionE.Text.Trim()) : String.Empty;
            if (ddlTipoE.Text != "") vPersona1.tipo_identificacion = Convert.ToInt64(ddlTipoE.SelectedValue);
            vPersona1.primer_nombre = txtPrimer_nombreE.Text.ToUpper();
            vPersona1.segundo_nombre = (txtSegundo_nombreE.Text != "") ? Convert.ToString(txtSegundo_nombreE.Text.Trim().ToUpper()) : String.Empty;
            vPersona1.primer_apellido = txtPrimer_apellidoE.Text.ToUpper();
            vPersona1.segundo_apellido = (txtSegundo_apellidoE.Text != "") ? Convert.ToString(txtSegundo_apellidoE.Text.Trim().ToUpper()) : String.Empty;
            if (string.Equals(rblTipo_persona.Text, "Jurídica"))
                vPersona1.tipo_persona = "J";
            else
                vPersona1.tipo_persona = "N";
            vPersona1.cod_oficina = Convert.ToInt64(ddlOficina.SelectedValue.Trim());
            if (txtFechaexpedicion.Text != "") vPersona1.fechaexpedicion = Convert.ToDateTime(txtFechaexpedicion.Text.Trim()); else vPersona1.fechaexpedicion = DateTime.MinValue;
            vPersona1.codciudadexpedicion = Convert.ToInt64(ddlLugarExpedicion.SelectedValue);

            vPersona1.direccion = txtDireccionE.Text.ToUpper();
            vPersona1.Estrato = txtEstrato.Text != "" && txtEstrato.Text != null ? Convert.ToInt32(txtEstrato.Text) : 0;
            vPersona1.nacionalidad = ddlPais.SelectedIndex > 0 ? Convert.ToInt64(ddlPais.SelectedValue) : 0;
            vPersona1.zona = Convert.ToInt64(ddlZona.SelectedValue.Trim());

            //INFORMACION DE CORRESPONDENCIA
            vPersona1.dirCorrespondencia = txtDirCorrespondencia.Text.ToUpper();
            vPersona1.barrioCorrespondencia = Convert.ToInt64(ddlBarrioCorrespondencia.SelectedValue);
            vPersona1.telCorrespondencia = txtTelCorrespondencia.Text;
            vPersona1.celular = txtcelular.Text;
            vPersona1.ciuCorrespondencia = Convert.ToInt64(ddlCiuCorrespondencia.SelectedValue);
            vPersona1.email = txtEmail.Text != "" && txtEmail.Text != null ? txtEmail.Text : "";

            //DATOS GENERALES
            vPersona1.fechanacimiento = Convert.ToDateTime(txtFechanacimiento.Text.Trim());
            vPersona1.codescolaridad = Convert.ToInt64(ddlNivelEscolaridad.SelectedValue);
            vPersona1.codciudadnacimiento = Convert.ToInt64(ddlLugarNacimiento.SelectedValue);
            vPersona1.sexo = (rblSexo.SelectedItem != null) ? Convert.ToString(rblSexo.SelectedValue) : null;

            //VERIFICAR ------------
            vPersona1.codciudadresidencia = Convert.ToInt64(ddlCiuCorrespondencia.SelectedValue);
            vPersona1.razon_social = String.Empty;
            vPersona1.barrioResidencia = 0;
            vPersona1.numHijos = 0;
            vPersona1.salario = 0;
            vPersona1.antiguedadLaboral = 0;
            vPersona1.acceso_oficina = checkOficinaVirtual.Checked ? 1 : 0;
            vPersona1.cod_asesor = pUsuario.codusuario;
            vPersona1.estado = idObjeto == "" ? "A" : vPersona1.estado;

            vPersona1.lstInformacion = new List<InformacionAdicional>();
            vPersona1.lstInformacion = ObtenerListaInformacionAdicional();

            if (hdFileName.Value != null)
            {
                try
                {
                    Stream stream = null;
                    /*Se guarda la imagen en un stream para despues colocarla en un objeto para que la imagen no quede abierta en el servidor*/
                    stream = File.OpenRead(Server.MapPath("Images\\") + Path.GetFileName(this.hdFileName.Value));
                    this.Response.Clear();
                    if (stream.Length > 100000)
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
            if (lblCodTutor.Text.Trim() != "")
                pResponsable.consecutivo = Convert.ToInt64(lblCodTutor.Text);
            pResponsable.cod_persona_tutor = Convert.ToInt64(ctlPersona.TextCodigo.Trim());

            if (idObjeto == "")
            {
                try
                {
                    //VALORES NULOS
                    vPersona1.codestadocivil = null;
                    vPersona1.PersonasAcargo = null;
                    vPersona1.profecion = null;
                    vPersona1.ocupacionApo = 0;
                    vPersona1.Estrato = null;
                    vPersona1.antiguedadlugar = 0;
                    vPersona1.ciudad = null;
                    vPersona1.mujer_familia = -1;
                    vPersona1.fecha_residencia = DateTime.MinValue;
                    vPersona1.cod_nomina = " ";
                    vPersona1.cuota = 0;
                    vPersona1.razon_social = " ";
                    vPersona1.fechacreacion = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
                }
                catch
                {
                    vPersona1.fechacreacion = System.DateTime.Now;
                }
                vPersona1.usuariocreacion = pUsuario.nombre;
                try
                {
                    persona1Servicio.CrearPersonaAporte(vPersona1, true, pResponsable, pUsuario);
                }
                catch (Exception ex)
                {
                    VerError("Error al crear la persona. " + ex.Message);
                    return;
                }
            }
            else
            {
                vPersona1.fecultmod = Convert.ToDateTime(DateTime.Now.ToString(GlobalWeb.gFormatoFecha));
                vPersona1.usuultmod = pUsuario.nombre;

                //Cargar los datos del responsable para modificación
                if (lblCodTutor.Text.Trim() != "")
                    pResponsable.consecutivo = Convert.ToInt64(lblCodTutor.Text);
                pResponsable.cod_persona_tutor = Convert.ToInt64(ctlPersona.TextCodigo.Trim());

                persona1Servicio.ModificarPersonaAporte(vPersona1, true, pResponsable, pUsuario);
            }
            if (chkAsociado.Checked)
                Grabar_Datos_afiliacion(vPersona1.cod_persona);

            Session[AfiliacionServicio.CodigoPrograma + ".nid"] = txtIdentificacionE.Text;
            //Navegar("../Afiliaciones/Lista.aspx");//Se realizo cambio por requerimiento 
            Session["cedula"] = txtIdentificacionE.Text;
            Navegar("../CuentasAportes/Nuevo.aspx");

        }
        catch (Exception ex)
        {
            BOexcepcion.Throw(AfiliacionServicio.CodigoPrograma, "btnContinuarMen_Click", ex);
        }
    }


    private void Grabar_Datos_afiliacion(Int64 pCod)
    {
        AfiliacionServices afiliacionService = new AfiliacionServices();

        Afiliacion afili = new Afiliacion();

        if (txtcodAfiliacion.Text != "")
            afili.idafiliacion = Convert.ToInt64(txtcodAfiliacion.Text);
        else
            afili.idafiliacion = 0;

        afili.cod_persona = pCod;

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

        if (chkAsistioUltAsamblea.Checked)
            afili.asist_ultasamblea = 1;
        else
            afili.asist_ultasamblea = 0;

        if (!string.IsNullOrWhiteSpace(ddlAsesor.SelectedValue))
        {
            afili.cod_asesor = Convert.ToInt64(ddlAsesor.SelectedValue);
        }

        afili.Es_PEPS = false;

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


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
        if (Session[serviceEstadoCuenta.CodigoPrograma + ".id"] != null)
        {
            Session.Remove(serviceEstadoCuenta.CodigoPrograma + ".id");
            Navegar("../../Asesores/EstadoCuenta/Detalle.aspx");
        }
        else
            Navegar("../Afiliaciones/Lista.aspx");
    }

    #region Eventos 

    protected void txtIdentificacionE_TextChanged(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            Xpinn.FabricaCreditos.Entities.Persona1 vPersona1 = new Xpinn.FabricaCreditos.Entities.Persona1();
            vPersona1.seleccionar = "Identificacion";
            vPersona1.parentesco = ddlParentesco.Text;
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

    protected void txtFechanacimiento_TextChanged(object sender, EventArgs e)
    {
        CalcularEdad();
    }

    private void CalcularEdad()
    {
        try
        {
            if (txtFechanacimiento.Text != "")
                txtEdadCliente.Text = Convert.ToString(GetAge(Convert.ToDateTime(txtFechanacimiento.Text)));
        }
        catch
        {
            txtEdadCliente.Text = "";
        }
    }

    public static int GetAge(DateTime birthDate)
    {
        return (int)Math.Floor((DateTime.Now - birthDate).TotalDays / 365.242199);
    }

    protected string FormatoFecha()
    {
        Configuracion conf = new Configuracion();
        return conf.ObtenerFormatoFecha();
    }


    protected void gvInfoAdicional_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtCadena = (TextBox)e.Row.FindControl("txtCadena");
            TextBox txtNumero = (TextBox)e.Row.FindControl("txtNumero");
            fecha txtctlfecha = (fecha)e.Row.FindControl("txtctlfecha");

            DropDownListGrid ddlDropdown = (DropDownListGrid)e.Row.FindControl("ddlDropdown");

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

            Label lblDropdown = (Label)e.Row.FindControl("lblDropdown");
            if (ddlDropdown != null)
            {
                string[] sDatos;
                sDatos = lblDropdown.Text.Split(',');
                if (sDatos.Count() > 0)
                {
                    ddlDropdown.Items.Clear();
                    ddlDropdown.DataSource = sDatos;
                    ddlDropdown.DataBind();
                }
            }

            Label lblValorDropdown = (Label)e.Row.FindControl("lblValorDropdown");
            if (lblValorDropdown != null)
            {
                ddlDropdown.DataSource = lblValorDropdown.Text;
                ddlDropdown.SelectedValue = lblValorDropdown.Text;
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

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {

        
        DeterminarFechaInicioAfiliacion();
    }

    protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFormaPago.SelectedItem.Value == "2" || ddlFormaPago.SelectedItem.Text == "Nomina")
        {
            lblEmpresa.Visible = true;
            ddlEmpresa.Visible = true;
            //RECUPERAR_EMPRESAS_NOMINA();

            DeterminarFechaInicioAfiliacion();
        }
        else
        {
            lblEmpresa.Visible = false;
            ddlEmpresa.Visible = false;
        }
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
            fechainicio = AfiliacionServicio.FechaInicioAfiliacion(txtFechaAfili.ToDateTime, Convert.ToInt64(ddlEmpresa.SelectedValue), (Usuario)Session["Usuario"]);
            if (fechainicio != null)
                txtFecha1Pago.ToDateTime = Convert.ToDateTime(fechainicio);
        }
        catch
        { }
    }


    #endregion


    #region Metodos Relacionados a la carga de imagen

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


    #endregion

    protected void chkAsociado_CheckedChanged(object sender, EventArgs e)
    {
        panelAfiliacion.Enabled = chkAsociado.Checked == true ? true : false;
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        ctlFormatos.lblErrorText = "";
        if (ctlFormatos.ddlFormatosItem != null)
            ctlFormatos.ddlFormatosIndex = 0;
        ctlFormatos.MostrarControl();
    }

    protected void btnVerData_Click(object sender, EventArgs e)
    {
        panelFinal.Visible = false;
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
            ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
        catch (Exception ex)
        {
            ctlFormatos.lblErrorIsVisible = true;
            ctlFormatos.lblErrorText = ex.Message;
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "jsKeys", "javascript:Forzar();", true);
        }
    }
    protected void btnImp_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Page/Aportes/Personas/ImportClasificacion.aspx");
    }

}