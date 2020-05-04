using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Configuration;

public partial class icetex : GlobalWeb
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSIcetex.WSIcetexSoapClient BOIcetex = new xpinnWSIcetex.WSIcetexSoapClient();
    static xpinnWSLogin.Persona1 PersonaLogin;


    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            string nom_empresa = string.Empty;
            string titulo = string.Empty;
            ValidarSession();
            VisualizarTitulo(OptionsUrl.ConvenioIcetex, "");
            Site toolBar = (Site)Master;
            toolBar.eventoContinuar += btnContinuar_Click;
            toolBar.eventoGuardar += btnGuardarAnexos_Click;
            toolBar.MostrarGuardar(false);
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("ActualizarDatos", "Page_PreInit", ex);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        PersonaLogin = (xpinnWSLogin.Persona1)Session["Persona"];      
        if (!Page.IsPostBack)
        {
            Session["DATAICETEX" + PersonaLogin.identificacion] = null;
            Session["TIPO_PROCESO" + PersonaLogin.identificacion] = null;
            Session["CreditoIcetex" + PersonaLogin.identificacion] = null;
            txtFecha.Text = DateTime.Now.ToShortDateString();
            CargarDropDown();
            ObtenerDatos(PersonaLogin.cod_persona);
            panelConv1.Visible = true;
        }
    }


    protected void CargarDropDown()
    {
        ddlTipoBeneficiario.Items.Insert(0, new ListItem("Asociado", "0"));
        ddlTipoBeneficiario.Items.Insert(1, new ListItem("Hijo del Asociado", "1"));
        ddlTipoBeneficiario.Items.Insert(2, new ListItem("Nieto del Asociado", "2"));
        ddlTipoBeneficiario.Items.Insert(3, new ListItem("Empleado", "3"));

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstUniversidad = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstUniversidad = EstadoServicio.PoblarListaDesplegable("Universidad", "", "", "2", Session["sec"].ToString());
        if (lstUniversidad.Count > 0)
        {
            ddlUniversidad.DataSource = lstUniversidad;
            ddlUniversidad.DataTextField = "descripcion";
            ddlUniversidad.DataValueField = "idconsecutivo";
            ddlUniversidad.AppendDataBoundItems = true;
            ddlUniversidad.Items.Insert(0, new ListItem("Seleccione un item", "0"));
            ddlUniversidad.SelectedIndex = 0;
            ddlUniversidad.DataBind();
        }
        CargarDropDownPrograma();
    }

    protected void CargarDropDownPrograma()
    {
        ddlPrograma.Items.Clear();
        if (ddlUniversidad.SelectedIndex > 0)
        {
            List<xpinnWSEstadoCuenta.ListaDesplegable> lstPrograma = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
            lstPrograma = EstadoServicio.PoblarListaDesplegable("programa", "", " Cod_universidad = " + ddlUniversidad.SelectedValue, "2", Session["sec"].ToString());
            if (lstPrograma.Count > 0)
            {
                ddlPrograma.DataSource = lstPrograma;
                ddlPrograma.DataTextField = "descripcion";
                ddlPrograma.DataValueField = "idconsecutivo";
                ddlPrograma.DataBind();
            }
        }
    }

    protected List<xpinnWSEstadoCuenta.ListaDesplegable> ListaTipoIdentificacion()
    {
        List<xpinnWSEstadoCuenta.ListaDesplegable> lstIdentificacion = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstIdentificacion = EstadoServicio.PoblarListaDesplegable("tipoidentificacion", "", "", "2", Session["sec"].ToString());

        return lstIdentificacion;
    }


    private void ObtenerDatos(Int64 pCod_Persona)
    {
        try
        {
            xpinnWSEstadoCuenta.Persona1 Persona = new xpinnWSEstadoCuenta.Persona1();
            Persona.cod_persona = pCod_Persona;
            Persona = EstadoServicio.ConsultarPersona(Persona);
            if (Persona.identificacion != "")
                txtIdentificacion.Text = Persona.identificacion;
            if (Persona.primer_nombre != "" && Persona.primer_apellido != "")
                txtNombre.Text = Persona.primer_nombre + " " + Persona.segundo_nombre + " " + Persona.primer_apellido + " " + Persona.segundo_apellido;
            if (Persona.nombre != "")
                txtCiudad.Text = Persona.nombre;
            if (Persona.direccion != "")
                txtDireccion.Text = HttpUtility.HtmlDecode(Persona.direccion);
            Site toolBar = (Site)Master;
            if (Persona.estado == "R")
            {
                lblAlerta.Text = "Usted se encuentra retirado, no puede continuar con el proceso";
                toolBar.MostrarContinuar(false);
            }
            else if (Persona.estado == "I")
            {
                lblAlerta.Text = "Usted se encuentra inhábil, no puede continuar con el proceso";
                toolBar.MostrarContinuar(false);
            }
            //if (Persona.Estrato == 0 || Persona.Estrato == null)
            //{
            //    lblAlerta.Text = lblAlerta.Text.Trim() != "" ? " - " : "";
            //    lblAlerta.Text = lblAlerta.Text.Trim() + "Usted tiene un estrato no válido para acceder a esta convocatoria, comuníquese con nosotros para ayudarlo con este inconveniente";
            //    toolBar.MostrarContinuar(false);
            //}
            //else
            //{
            //    if (Persona.Estrato > 3)
            //    {
            //        lblAlerta.Text = lblAlerta.Text.Trim() != "" ? " - " : "";
            //        lblAlerta.Text = lblAlerta.Text.Trim() + "Usted tiene un estrato mayor al permitido para acceder a este convenio";
            //        toolBar.MostrarContinuar(false);
            //    }
            //}
            //Cargando el historial de Creditos Icetex del Asociado
            Actualizar(pCod_Persona);
            //Cargando datos Validacion de requisitos
            ObtenerValidacionRequisitos();
        }
        catch
        {
            VerError("Se Genero un error al Obtener sus datos.");
        }
    }


    private void ObtenerDatosCreditoIcetex(xpinnWSIcetex.CreditoIcetex pEntidad)
    {
        if (pEntidad != null)
        {
            if (pEntidad.numero_credito > 0)
            {
                lblTituloCredito.Visible = true;
                lblNumeroCredito.Visible = true;
                panelConfirmarData.Visible = true;

                lblNumeroCredito.Text = pEntidad.numero_credito.ToString();
                if (pEntidad.fecha_solicitud != DateTime.MinValue)
                    txtFecha.Text = pEntidad.fecha_solicitud.ToString(gFormatoFecha);
                if (pEntidad.tipo_beneficiario != null)
                    ddlTipoBeneficiario.SelectedValue = pEntidad.tipo_beneficiario;

                List<xpinnWSEstadoCuenta.Persona1> lstInformacion = new List<xpinnWSEstadoCuenta.Persona1>();
                xpinnWSEstadoCuenta.Persona1 pData = new xpinnWSEstadoCuenta.Persona1();
                pData.tipo_identificacion = Convert.ToInt64(pEntidad.codtipoidentificacion);
                pData.identificacion = pEntidad.identificacion;
                pData.primer_apellido = pEntidad.primer_apellido;
                pData.segundo_apellido = pEntidad.segundo_apellido;
                pData.primer_nombre = pEntidad.primer_nombre;
                pData.segundo_nombre = pEntidad.segundo_nombre;
                pData.direccion = pEntidad.direccion;
                pData.telefono = pEntidad.telefono;
                pData.email = pEntidad.email;

                pData.Estrato = pEntidad.estrato;
                lstInformacion.Add(pData);

                gvDatosPersona.DataSource = lstInformacion;
                gvDatosPersona.DataBind();

                if (pEntidad.cod_universidad != null)
                {
                    ddlUniversidad.SelectedValue = pEntidad.cod_universidad;
                    ddlUniversidad_SelectedIndexChanged(ddlUniversidad, null);
                }
                if (pEntidad.cod_programa != null)
                    ddlPrograma.SelectedValue = pEntidad.cod_programa;
                if (pEntidad.tipo_programa != 0)
                    ddlTipoPrograma.SelectedValue = pEntidad.tipo_programa.ToString();
                if (pEntidad.valor > 0)
                    txtValorPrograma.Text = pEntidad.valor.ToString();
                if (pEntidad.periodos != null)
                    ddlPeriodos.SelectedValue = pEntidad.periodos.ToString();
                if (pEntidad.estado != null)
                    lblCodEstado.Text = pEntidad.estado;

                panelConv2.Enabled = false;
                ObtenerDatosAfiliado();
            }
        }
    }

    protected void Actualizar(Int64 pCod_Persona)
    {
        try
        {
            List<xpinnWSIcetex.CreditoIcetex> lstCredito = new List<xpinnWSIcetex.CreditoIcetex>();
            string pFiltro = " where C.COD_PERSONA = " + pCod_Persona.ToString();
            lstCredito = BOIcetex.ListarCreditosIcetex(pFiltro, Session["sec"].ToString());
            if (lstCredito.Count() > 0)
            {
                panelGrid.Visible = true;
                lblTotReg.Text = "<br/> Registros encontrados " + lstCredito.Count();
                lblTotReg.Visible = true;
                lblInfo.Visible = false;
                gvLista.DataSource = lstCredito;
            }
            else
            {
                panelGrid.Visible = false;
                lblTotReg.Visible = false;
                lblInfo.Visible = true;
                gvLista.DataSource = null;
            }
            gvLista.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }

    protected void ObtenerValidacionRequisitos()
    {
        Site toolBar = (Site)Master;
        try
        {
            List<xpinnWSIcetex.ConvocatoriaRequerido> lstRequisitos = new List<xpinnWSIcetex.ConvocatoriaRequerido>();
            DateTime pFecha = Convert.ToDateTime(txtFecha.Text);
            panelReqFooter.Visible = false;
            lblInfoRequisitos.Visible = true;
            lstRequisitos = BOIcetex.ValidacionRequisitos(PersonaLogin.cod_persona, pFecha, Session["sec"].ToString());
            
            if (lstRequisitos.Count() > 0)
            {
                lblCod_Convocatoria.Text = lstRequisitos[0].cod_convocatoria.ToString();
                panelReqFooter.Visible = true;
                gvRequisitos.Visible = true;
                gvRequisitos.DataSource = lstRequisitos;
                lblInfoRequisitos.Visible = false;
            }
            else
            {
                lblInfoRequisitos.Visible = true;
                gvRequisitos.Visible = true;
                gvRequisitos.DataSource = null;
                toolBar.MostrarContinuar(false);
            }
            gvRequisitos.DataBind();

            bool rpta = true;
            foreach (GridViewRow rFila in gvRequisitos.Rows)
            {
                Label lblObli = (Label)rFila.FindControl("lblObli");
                Label lblImg = (Label)rFila.FindControl("lblImg");
                if (lblObli.Text == "1")
                    if (lblImg.Text != "1")
                        rpta = false;
            }
            if (rpta == false)
            {
                toolBar.MostrarContinuar(false);
            }

            //CONSULTANDO CONVOCATORIA ICETEX
            if (lblCod_Convocatoria.Text.Trim() != "")
            {
                xpinnWSIcetex.ConvocatoriaIcetex pConvocatoria = new xpinnWSIcetex.ConvocatoriaIcetex();
                pConvocatoria = BOIcetex.ConsultarConvocatoriaIcetex(Convert.ToInt64(lblCod_Convocatoria.Text.Trim()), Session["sec"].ToString());
                
                if (pConvocatoria != null)
                {
                    if (pConvocatoria.cod_convocatoria > 0)
                    {
                        lblConvocatoria.Text = lblConvocatoria.Text + "( " + pConvocatoria.descripcion + " )";
                        if (pConvocatoria.fecha_inicio != null && pConvocatoria.fecha_final != null)
                        {
                            xpinnWSIcetex.CreditoIcetex pCredito = new xpinnWSIcetex.CreditoIcetex();
                            string pFiltro = " WHERE C.COD_CONVOCATORIA = " + lblCod_Convocatoria.Text + " AND C.COD_PERSONA = " + PersonaLogin.cod_persona;
                            pCredito = BOIcetex.ConsultarCreditoIcetex(pFiltro, Session["sec"].ToString());
                            Session["DATAICETEX" + PersonaLogin.identificacion] = pCredito;
                            if (pCredito != null)
                            {
                                if (pFecha >= pConvocatoria.fecha_inicio && pFecha <= pConvocatoria.fecha_final)
                                {
                                    Session["TIPO_PROCESO" + PersonaLogin.identificacion] = "PRE-INSCRIPCION";
                                    lblRangoFecha.Text = "Convocatoria válida del " + Convert.ToDateTime(pConvocatoria.fecha_inicio).ToShortDateString() + " al "
                                    + Convert.ToDateTime(pConvocatoria.fecha_final).ToShortDateString();
                                }
                                else if (pFecha >= pConvocatoria.fec_ini_inscripcion && pFecha <= pConvocatoria.fec_fin_inscripcion && pCredito.estado == "A")
                                {
                                    Session["TIPO_PROCESO" + PersonaLogin.identificacion] = "INSCRIPCION";
                                    lblRangoFecha.Text = "Inscripciones válidas del " + Convert.ToDateTime(pConvocatoria.fec_ini_inscripcion).ToShortDateString() + " al "
                                    + Convert.ToDateTime(pConvocatoria.fec_fin_inscripcion).ToShortDateString();
                                }
                            }

                            
                        }   
                    }
                }
            }
        }
        catch (Exception ex)
        {
            toolBar.MostrarContinuar(false);
            VerError("[Error] No existen convocatorias actualmente");
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            if (panelConv1.Visible == true)
            {
                ddlTipoBeneficiario_SelectedIndexChanged(ddlTipoBeneficiario, null);

                if (Session["TIPO_PROCESO" + PersonaLogin.identificacion] != null)
                {
                    if (Session["TIPO_PROCESO" + PersonaLogin.identificacion].ToString() == "INSCRIPCION")
                    {
                        xpinnWSIcetex.CreditoIcetex pCredito = new xpinnWSIcetex.CreditoIcetex();
                        if (Session["DATAICETEX" + PersonaLogin.identificacion] != null)
                        {
                            pCredito = (xpinnWSIcetex.CreditoIcetex)Session["DATAICETEX" + PersonaLogin.identificacion];
                            Session.Remove("DATAICETEX" + PersonaLogin.identificacion);
                        }
                        else
                        {
                            string pFiltro = " WHERE C.COD_CONVOCATORIA = " + lblCod_Convocatoria.Text + " AND C.COD_PERSONA = " + PersonaLogin.cod_persona;
                            pCredito = BOIcetex.ConsultarCreditoIcetex(pFiltro, Session["sec"].ToString());
                        }
                        ObtenerDatosCreditoIcetex(pCredito);
                    }
                }

                panelConv1.Visible = false;
                panelConv2.Visible = true;
            }
            else
            {
                Site toolBar = (Site)Master;
                if (Session["TIPO_PROCESO" + PersonaLogin.identificacion].ToString() == "INSCRIPCION")
                {
                    lblmsjDocum.Visible = false;
                    lblInfoDocu.Visible = true;
                    //Validar Información
                    if (panelConfirmarData.Visible == true)
                    {
                        if (rblConfirmacionData.SelectedItem == null)
                        {
                            VerError("Seleccione si esta o no conforme con la información actual");
                            return;
                        }
                        else
                        {
                            if (rblConfirmacionData.SelectedValue == "0")
                            {
                                if (string.IsNullOrEmpty(txtInconformidad.Text.Trim()))
                                {
                                    VerError("Por favor, indiquenos los motivos por la inconformidad de datos.");
                                    return;
                                }
                            }
                        }
                        panelConfirmarData.Visible = false;
                    }

                    toolBar.MostrarGuardar(false);
                    //Cargando los documentos adicionales
                    List<xpinnWSIcetex.IcetexDocumentos> lstAdicional = new List<xpinnWSIcetex.IcetexDocumentos>();
                    lstAdicional = ConsultarDocumentos(" AND C.TIPO_INFORMACION = 2");
                    if (lstAdicional.Count > 0)
                    {
                        lblInfoDocu.Visible = false;
                        gvDocumentosReq.Visible = true;
                        gvDocumentosReq.DataSource = lstAdicional;
                        gvDocumentosReq.DataBind();
                    }
                }
                else
                {
                    xpinnWSIcetex.CreditoIcetex pEntidad = new xpinnWSIcetex.CreditoIcetex();
                    //GUARDANDO EL CREDITO
                    if (!CargarCreditoIcetex(ref pEntidad))
                        return;
                    Session["CreditoIcetex" + PersonaLogin.identificacion] = pEntidad;
                    //LISTANDO LOS DOCUMENTOS REQUERIDOS
                    ActualizarDocumentos(PersonaLogin.cod_persona);
                    toolBar.MostrarGuardar(true);
                }
                toolBar.MostrarContinuar(false);
                panelConv2.Visible = false;
                panelConv3.Visible = true;
                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Icetex", "btnContinuar_Click", ex);
        }
    }

    


    private Boolean CargarCreditoIcetex(ref xpinnWSIcetex.CreditoIcetex pEntidad)
    {
        pEntidad = new xpinnWSIcetex.CreditoIcetex();

        if (txtFecha.Text == "")
        {
            VerError("No existe ninguna fecha cargada actualmente, intente ingresar nuevamente.");
            return false;
        }

        pEntidad.numero_credito = 0;
        pEntidad.cod_convocatoria = Convert.ToInt32(lblCod_Convocatoria.Text);
        pEntidad.fecha_solicitud = Convert.ToDateTime(txtFecha.Text);
        pEntidad.cod_persona = PersonaLogin.cod_persona;
        pEntidad.tipo_beneficiario = ddlTipoBeneficiario.SelectedValue;

        foreach (GridViewRow rFila in gvDatosPersona.Rows)
        {
            DropDownList ddlTipoDoc = (DropDownList)rFila.FindControl("ddlTipoDoc");
            TextBox txtNroDoc = (TextBox)rFila.FindControl("txtNroDoc");
            TextBox txtApellido1 = (TextBox)rFila.FindControl("txtApellido1");
            TextBox txtApellido2 = (TextBox)rFila.FindControl("txtApellido2");
            TextBox txtNombre1 = (TextBox)rFila.FindControl("txtNombre1");
            TextBox txtNombre2 = (TextBox)rFila.FindControl("txtNombre2");
            TextBox txtDireccion = (TextBox)rFila.FindControl("txtDireccion");
            TextBox txtTelefono = (TextBox)rFila.FindControl("txtTelefono");
            TextBox txtEmail = (TextBox)rFila.FindControl("txtEmail");
            TextBox txtEstrato = (TextBox)rFila.FindControl("txtEstrato");
            if (ddlTipoBeneficiario.SelectedValue != "0" && ddlTipoBeneficiario.SelectedValue != "3")
            {
                if (txtNroDoc.Text.Trim() == "")
                {
                    VerError("Ingrese el Nro de Documento, Verifique en los datos generales");
                    return false;
                }
                if (txtApellido1.Text.Trim() == "")
                {
                    VerError("Ingrese su primer apellido, Verifique en los datos generales");
                    return false;
                }
                if (txtNombre1.Text.Trim() == "")
                {
                    VerError("Ingrese su primer nombre, Verifique en los datos generales");
                    return false;
                }
            }
            if (txtDireccion.Text.Trim() == "")
            {
                VerError("Ingrese su dirección, Verifique en los datos generales");
                return false;
            }
            //Cargando datos a entidad
            pEntidad.identificacion = txtNroDoc.Text.Trim();
            pEntidad.codtipoidentificacion = Convert.ToInt32(ddlTipoDoc.SelectedValue);
            pEntidad.primer_nombre = txtNombre1.Text.ToUpper().Trim();
            pEntidad.segundo_nombre = txtNombre2.Text.Trim() != "" ? txtNombre2.Text.ToUpper().Trim() : null;
            pEntidad.primer_apellido = txtApellido1.Text.ToUpper().Trim();
            pEntidad.segundo_apellido = txtApellido2.Text.Trim() != "" ? txtApellido2.Text.ToUpper().Trim() : null;
            pEntidad.direccion = txtDireccion.Text.Trim();
            pEntidad.telefono = txtTelefono.Text.Trim() != "" ? txtTelefono.Text.Trim() : null;
            pEntidad.email = txtEmail.Text.Trim() != "" ? txtEmail.Text.Trim() : null;
            if (txtEstrato.Text.Trim() != "")
            {
                int estrato = Convert.ToInt32(txtEstrato.Text);
                if (estrato >= 1 && estrato <= 3)
                    pEntidad.estrato = Convert.ToInt32(txtEstrato.Text.Trim());
                else
                {
                    VerError("El estrato actual no cumple con las condiciones establecidas. Rando del [ 1 - 3 ]");
                    return false;
                }
            }
            else
            {
                pEntidad.estrato = null;
                VerError("Ingrese el estrado del beneficiario.");
                return false;
            } 
        }
        if (ddlUniversidad.SelectedItem == null)
        {
            VerError("No existen universidades registradas, Consulte con nosotros para solucionar este inconveniente");
            return false;
        }
        if (ddlUniversidad.SelectedIndex <= 0)
        {
            VerError("Seleccione una universidad, verifique los datos del crédito");
            return false;
        }
        if (ddlPrograma.SelectedItem == null)
        {
            VerError("No existen programas asignados para la universidad seleccionada, Consulte con nosotros para solucionar este inconveniente");
            return false;
        }
        if (ddlUniversidad.SelectedIndex <= 0)
        {
            VerError("Seleccione una programa, verifique los datos del crédito");
            return false;
        }
        pEntidad.cod_universidad = ddlUniversidad.SelectedValue;
        pEntidad.cod_programa = ddlPrograma.SelectedValue;
        pEntidad.tipo_programa = Convert.ToInt32(ddlTipoPrograma.SelectedValue);
        if (txtValorPrograma.Text == "" || txtValorPrograma.Text == "0")
        {
            VerError("Ingrese el valor del programa, verifique los datos del crédito");
            return false;
        }

        //Consultando salario Minimo
        //xpinnWSEstadoCuenta.General pInfo = EstadoServicio.consultarsalariominimo(10);
        //decimal vrCalculado = 0, salarioMin = 0;
        //if (pInfo.valor == null)
        //    salarioMin = 737717;
        //else
        //    salarioMin = Convert.ToDecimal(pInfo.valor);
        //vrCalculado = salarioMin * 35;
        if (Convert.ToDecimal(txtValorPrograma.Text.Replace(".", "")) > 25820095)
        {
            VerError("El monto ingresado supera los 35 SMLV");
            return false;
        }
        pEntidad.valor = Convert.ToDecimal(txtValorPrograma.Text.Replace(".", ""));
        pEntidad.periodos = Convert.ToDecimal(ddlPeriodos.SelectedValue);
        pEntidad.estado = "S";

        return true;
    }

    protected List<xpinnWSIcetex.IcetexDocumentos> ConsultarDocumentos(string pAdicional = null)
    {
        List<xpinnWSIcetex.IcetexDocumentos> lstDocum = new List<xpinnWSIcetex.IcetexDocumentos>();
        string pFiltro = " where C.COD_CONVOCATORIA = " + lblCod_Convocatoria.Text + pAdicional;
        lstDocum = BOIcetex.ListarConvocatoriaDocumentos(pFiltro, Session["sec"].ToString());
        return lstDocum;
    }

    protected void ActualizarDocumentos(Int64 pCod_Persona)
    {
        try
        {
            List<xpinnWSIcetex.IcetexDocumentos> lstDocumentos = new List<xpinnWSIcetex.IcetexDocumentos>();
            lstDocumentos = ConsultarDocumentos(" AND C.TIPO_INFORMACION = 1");
            if (lstDocumentos.Count() > 0)
            {
                gvDocumentosReq.Visible = true;
                lblInfoDocu.Visible = false;
                gvDocumentosReq.DataSource = lstDocumentos;
            }
            else
            {
                gvDocumentosReq.Visible = false;
                lblInfoDocu.Visible = true;
                gvDocumentosReq.DataSource = null;
            }
            gvDocumentosReq.DataBind();
        }
        catch (Exception ex)
        {
            VerError(ex.Message);
        }
    }


    private Boolean CargarDocumentosCredito(ref List<xpinnWSIcetex.CreditoIcetexDocumento> lstArchivos)
    {
        string pCod = ddlTipoBeneficiario.SelectedValue;
        //Adicion temporal de evitar la carga de parentesco si es Asociado o empleado
        bool rpta = pCod == "0" || pCod == "3" ? true : false;
        bool resulPost = false;
        if (Session["TIPO_PROCESO" + PersonaLogin.identificacion].ToString() == "INSCRIPCION")
            resulPost = true;
        //Guardando documentos
        foreach (GridViewRow rFila in gvDocumentosReq.Rows)
        {
            int pCod_tipo_doc = Convert.ToInt32(gvDocumentosReq.DataKeys[rFila.RowIndex].Value.ToString());
            xpinnWSIcetex.CreditoIcetexDocumento pEntidad = new xpinnWSIcetex.CreditoIcetexDocumento();
            Label lblPegrunta = (Label)rFila.FindControl("lblPegrunta");
            FileUpload fuArchivo = (FileUpload)rFila.FindControl("fuArchivo");
            CheckBox chkRespuesta = (CheckBox)rFila.FindControl("chkRespuesta");
            if (rpta && pCod_tipo_doc == 4)
            {
            }
            else
            {
                if (fuArchivo != null)
                {
                    if (!fuArchivo.HasFile)
                    {
                        VerError("Ingrese el documento requerido en la Fila " + (rFila.RowIndex + 1));
                        //if (resulPost)
                        //    RegistrarPostBack();
                        return false;
                    }
                    String extension = System.IO.Path.GetExtension(fuArchivo.PostedFile.FileName).ToLower();
                    if (extension != ".pdf")
                    {
                        VerError("El archivo en la Fila " + (rFila.RowIndex + 1) + " no tiene la extensión PDF");
                        //if (resulPost)
                        //    RegistrarPostBack();
                        return false;
                    }
                    //Capturando el tamaño establecido
                    int tamMax = Convert.ToInt32(ConfigurationManager.AppSettings["TamañoMaximoArchivo"]);

                    if (fuArchivo.FileBytes.Length > tamMax)
                    {
                        VerError("El tamaño del archivo en la fila " + (rFila.RowIndex + 1) + " excede el tamaño limite de ( 1MB )");
                        //if (resulPost)
                        //    RegistrarPostBack();
                        return false;
                    }
                }
            }

            StreamsHelper streamHelper = new StreamsHelper();
            byte[] bytesArrImagen;
            using (System.IO.Stream streamImagen = fuArchivo.PostedFile.InputStream)
            {
                bytesArrImagen = streamHelper.LeerTodosLosBytesDeUnStream(streamImagen);
            }

            pEntidad.cod_credoc = 0;
            //pEntidad.numero_credito = Convert.ToInt64(lblNum_Credito.Text);
            pEntidad.cod_tipo_doc = pCod_tipo_doc;
            pEntidad.pregunta = lblPegrunta.Text.Trim() != "" ? lblPegrunta.Text.Trim() : null;
            if (pEntidad.pregunta == null)
                pEntidad.respuesta = null;
            else
                pEntidad.respuesta = chkRespuesta.Checked ? "1" : "0";
            pEntidad.imagen = bytesArrImagen;
            if (rpta && pCod_tipo_doc == 4)
            {
            }
            else
                lstArchivos.Add(pEntidad);
        }
        return true;
    }

    protected void btnGuardarAnexos_Click(object sender, EventArgs e)
    {
        try
        {
            VerError("");
            //GUARDAR DOCUMENTOS

            List<xpinnWSIcetex.CreditoIcetexDocumento> lstArchivos = new List<xpinnWSIcetex.CreditoIcetexDocumento>();
            xpinnWSIcetex.CreditoIcetex pResult = new xpinnWSIcetex.CreditoIcetex();
            if (!CargarDocumentosCredito(ref lstArchivos))
                return;

            if (Session["TIPO_PROCESO" + PersonaLogin.identificacion].ToString() == "INSCRIPCION")
            {
                //PENDIENTE MODIFICAR CREDITO ICETEX Y CARGAR EL DOCUMENTO FINAL
                pResult.numero_credito = Convert.ToInt64(lblNumeroCredito.Text);
                pResult.estado = "I";
                pResult.esconforme = Convert.ToInt32(rblConfirmacionData.SelectedValue);
                pResult.observacion = txtInconformidad.Text.Trim();
                pResult.fecha_inscripcion = DateTime.Now;
                pResult = BOIcetex.CrearCreditoIcetex(pResult, lstArchivos, 2, Session["sec"].ToString());
            }
            else
            {
                if (Session["CreditoIcetex" + PersonaLogin.identificacion] != null)
                {
                    pResult = (xpinnWSIcetex.CreditoIcetex)Session["CreditoIcetex" + PersonaLogin.identificacion];
                    pResult = BOIcetex.CrearCreditoIcetex(pResult, lstArchivos, 1, Session["sec"].ToString());
                    if (pResult == null)
                    {
                        VerError("Se genero un error al guardar el crédito Icetex.");
                        return;
                    }
                    if (pResult.numero_credito != 0)
                        lblNum_Credito.Text = pResult.numero_credito.ToString();
                }
            }
            //MOSTRAR MENSAJE
            ProcesoFinal();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Icetex", "btnGuardarAnexos_Click", ex);
        }
    }

    private void ProcesoFinal()
    {
        if (lblCod_Convocatoria.Text != "")
        {
            if (Session["TIPO_PROCESO" + PersonaLogin.identificacion].ToString() == "INSCRIPCION")
            {
                lblMensaje.Text = "<center>Su proceso de inscripción fue culminado satisfactoriamente</center>";
            }
            else
            {
                xpinnWSIcetex.ConvocatoriaIcetex pEntidad = new xpinnWSIcetex.ConvocatoriaIcetex();
                pEntidad = BOIcetex.ConsultarConvocatoriaIcetex(Convert.ToInt64(lblCod_Convocatoria.Text), Session["sec"].ToString());
                if (pEntidad.mensaje_solicitud != null)
                    lblMensaje.Text = pEntidad.mensaje_solicitud;
            }
            Actualizar(PersonaLogin.cod_persona);
            
            panelGeneral.Visible = false;
            panelConv4.Visible = true;
            EnviarCorreo();

            Site toolBar = (Site)Master;
            toolBar.MostrarContinuar(false);
            toolBar.MostrarGuardar(false);
        }
    }


    protected void EnviarCorreo()
    {
        string correoServer = "", clave = "", pHosting = "";
        int puerto = 0;
        correoServer = ConfigurationManager.AppSettings["CorreoServidor"].ToString();
        clave = ConfigurationManager.AppSettings["Clave"].ToString();

        pHosting = ConfigurationManager.AppSettings["Hosting"].ToString();
        puerto = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());

        DateTime pFec = DateTime.Now;
        if (Session["TIPO_PROCESO" + PersonaLogin.identificacion].ToString() == "INSCRIPCION")
        {
            lblAsiciadoIns.Text = txtNombre.Text.Trim();
            lblIdentifiAsoIns.Text = txtIdentificacion.Text;
            lblFechaTransacIns.Text = string.Format("Cartagena, {0} de {1} de {2} a las {3}", pFec.ToString("dd"), pFec.ToString("MMMM"), pFec.ToString("yyyy"), pFec.ToString("hh:mm tt"));
            lblINFtipoBenIns.Text = ddlTipoBeneficiario.SelectedItem.Text;
            lblINFunivIns.Text = ddlUniversidad.SelectedItem.Text;
            lblINFprogIns.Text = ddlPrograma.SelectedItem != null ? ddlPrograma.SelectedItem.Text : " ";
            lblINFvalIns.Text = txtValorPrograma.Text;
            panelEnvioInscripcion.Visible = true;
        }
        else
        {
            lblAsiciado.Text = txtNombre.Text.Trim();
            lblIdentifiAso.Text = txtIdentificacion.Text;
            lblFechaTransac.Text = string.Format("Cartagena, {0} de {1} de {2} a las {3}", pFec.ToString("dd"), pFec.ToString("MMMM"), pFec.ToString("yyyy"), pFec.ToString("hh:mm tt"));
            lblINFtipoBen.Text = ddlTipoBeneficiario.SelectedItem.Text;
            lblINFuniv.Text = ddlUniversidad.SelectedItem.Text;
            lblINFprog.Text = ddlPrograma.SelectedItem != null ? ddlPrograma.SelectedItem.Text : " ";
            lblINFval.Text = txtValorPrograma.Text;
            panelEnvio.Visible = true;

            if (string.IsNullOrEmpty(lblCorreoEnvio.Text.Trim()))
            {
                TextBox txtEmail = (TextBox)gvDatosPersona.Rows[0].FindControl("txtEmail");
                lblCorreoEnvio.Text = txtEmail.Text.Trim();
            }
        }

        TextBox txtEstrato = (TextBox)gvDatosPersona.Rows[0].FindControl("txtEstrato");
        TextBox txtApellido1 = (TextBox)gvDatosPersona.Rows[0].FindControl("txtApellido1");
        TextBox txtApellido2 = (TextBox)gvDatosPersona.Rows[0].FindControl("txtApellido2");
        TextBox txtNombre1 = (TextBox)gvDatosPersona.Rows[0].FindControl("txtNombre1");
        TextBox txtNombre2 = (TextBox)gvDatosPersona.Rows[0].FindControl("txtNombre2");
        lblINFtitBen.Visible = true;
        lblINFbenefi.Visible = true;
        lblINFtitBenIns.Visible = true;
        lblINFbenefiIns.Visible = true;
        lblINFbenefi.Text = txtApellido1.Text + " " + txtApellido2.Text + " " + txtNombre1.Text + " " + txtNombre2.Text;
        lblINFestrato.Text = txtEstrato.Text;
        lblINFbenefiIns.Text = txtApellido1.Text + " " + txtApellido2.Text + " " + txtNombre1.Text + " " + txtNombre2.Text;
        lblINFestratoIns.Text = txtEstrato.Text;

        CorreoHelper BOCorreo = new CorreoHelper(lblCorreoEnvio.Text, correoServer, clave);
        bool rpta;
        if (Session["TIPO_PROCESO" + PersonaLogin.identificacion].ToString() == "INSCRIPCION")
            rpta = BOCorreo.EnviarHTMLDeControlPorCorreo(panelEnvioInscripcion, pHosting, puerto, "Convenio Icetex", "CONVENIO COOACEDED-ICETEX ETAPA DE INSCRICPION");
        else
            rpta = BOCorreo.EnviarHTMLDeControlPorCorreo(panelEnvio, pHosting, puerto, "Convenio Icetex", "CONVENIO COOACEDED-ICETEX ETAPA DE PRE-INSCRICPION");

        if (!rpta)
        {
            VerError("No se pudo realizar el envio.");
        }
        panelEnvio.Visible = false;
        panelEnvioInscripcion.Visible = false;
    }

    protected void ObtenerDatosAfiliado()
    {
        try
        {
            xpinnWSEstadoCuenta.Persona1 pEntidad = new xpinnWSEstadoCuenta.Persona1();
            pEntidad = EstadoServicio.ConsultarPersonaAPP(PersonaLogin.identificacion);
            if (pEntidad != null)
            {
                if (pEntidad.email != null)
                    lblCorreoEnvio.Text = pEntidad.email_app.Trim();                
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Icetex", "ObtenerDatosAsociado", ex);
        }
    }

    protected void ObtenerDatosAsociado(Int64 pCod_Persona, ref List<xpinnWSEstadoCuenta.Persona1> lstPersona)
    {
        try
        {
            xpinnWSEstadoCuenta.Persona1 pEntidad = new xpinnWSEstadoCuenta.Persona1();
            pEntidad = EstadoServicio.ConsultarPersonaAPP(PersonaLogin.identificacion);
            if (pEntidad != null)
            {
                if(pEntidad.email != null)
                    lblCorreoEnvio.Text = pEntidad.email_app.Trim();
                if (pEntidad.direccion != null)
                    pEntidad.direccion = HttpUtility.HtmlDecode(pEntidad.direccion);
                lstPersona.Add(pEntidad);
            }
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Icetex", "ObtenerDatosAsociado", ex);
        }
    }


    protected void ddlTipoBeneficiario_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            List<xpinnWSEstadoCuenta.Persona1> lstPersona = new List<xpinnWSEstadoCuenta.Persona1>();

            lblmsjDocum.Text = "";
            if (ddlTipoBeneficiario.SelectedValue == "0" || ddlTipoBeneficiario.SelectedValue == "3")
            {
                //Cargar por defecto los datos de la persona
                Int64 pCod_persona = ((xpinnWSLogin.Persona1)Session["persona"]).cod_persona;
                ObtenerDatosAsociado(pCod_persona, ref lstPersona);
                if (lstPersona.Count() == 0)
                    lstPersona.Add(new xpinnWSEstadoCuenta.Persona1());
                lblmsjDocum.Text = "El documento de parentesco no es necesario ingresarlo";
            }
            else
            {
                lstPersona.Add(new xpinnWSEstadoCuenta.Persona1());
            }
            gvDatosPersona.DataSource = lstPersona;
            gvDatosPersona.DataBind();
        }
        catch (Exception ex)
        {
            BOexcepcion.Throw("Icetex", "ddlTipoBeneficiario_SelectedIndexChanged", ex);
        }
    }



    protected void ddlUniversidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDropDownPrograma();
    }

    protected void gvDatosPersona_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlTipoDoc = (DropDownList)e.Row.FindControl("ddlTipoDoc");
            if (ddlTipoDoc != null)
            {
                Label lblTipoDoc = (Label)e.Row.FindControl("lblTipoDoc");
                if (lblTipoDoc != null)
                    if (lblTipoDoc.Text != "")
                        ddlTipoDoc.SelectedValue = lblTipoDoc.Text;
            }
            bool rpta = true;
            if (ddlTipoBeneficiario.SelectedValue == "0" || ddlTipoBeneficiario.SelectedValue == "3")
            {
                rpta = false;
            }
            e.Row.Cells[0].Enabled = rpta;
            e.Row.Cells[1].Enabled = rpta;
            e.Row.Cells[2].Enabled = rpta;
            e.Row.Cells[3].Enabled = rpta;
            e.Row.Cells[4].Enabled = rpta;
            e.Row.Cells[5].Enabled = rpta;
        }
    }


    protected void gvDocumentosReq_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPegrunta = (Label)e.Row.FindControl("lblPegrunta");
            if (lblPegrunta != null)
            {
                CheckBox chkRespuesta = (CheckBox)e.Row.FindControl("chkRespuesta");
                chkRespuesta.Visible = lblPegrunta.Text.Trim() != "" ? true : false;
            }
        }
    }

    protected void chkAceptarTerminos_CheckedChanged(object sender, EventArgs e)
    {
        bool result = chkAceptarTerminos.Checked ? true : false;
        btnGuardar.Visible = result;
    }
}