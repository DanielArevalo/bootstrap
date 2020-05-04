using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;

public partial class Afiliacion : System.Web.UI.Page
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoServicio = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient AppService = new xpinnWSAppFinancial.WSAppFinancialSoapClient();
    Validadores BOValidacion = new Validadores();

    public static string baseUrl;
    public static string ReCaptcha_Key = "6LfMUkgUAAAAAKwJw4dzMXAUBFrMlDSyZ64Ngiza";
    public static string ReCaptcha_Secret = "6LfMUkgUAAAAAFq-rB2Gn6G1TONmNPvzkuqs0T9a";
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            txtIngsalariomensual.eventoCambiar += txtIngsalariomensual_TextChanged;
            txtOtrosing.eventoCambiar += txtOtrosing_TextChanged;
            txtDeducciones.eventoCambiar += txtDeducciones_TextChanged;
            ctlMensaje.eventoClick += btnContinuarMen_Click;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            baseUrl = Server.MapPath("~");
            panelData.Visible = true;
            panelFinal.Visible = false;
            lblError.Text = "";
            //Asignando estilo radioButonList al CheckboxList
            cblSexo.Attributes.Add("onclick", "radioSex(event);");
            cblEstrato.Attributes.Add("onclick", "radioMe(event);");
            cblCabezaFamilia.Attributes.Add("onclick", "radioMeCbz(event);");
            //cblDocumento.Attributes.Add("onclick", "radioMeDocu(event);");
            cblEstadoCivil.Attributes.Add("onclick", "radioMeEstadoCi(event);");
            cbNivelAcademico.Attributes.Add("onclick", "radioMeNivEsc(event);");
            cblDocumentoCony.Attributes.Add("onclick", "radioMeDocuCony(event);");
            ChkPersona.Attributes.Add("onclick", "TipPersona(event);");


            CargarDropDownYCheckBox();
            DateTime pFechaActual = DateTime.Now;
            txtDiaEncabezado.Text = pFechaActual.Day.ToString();
            ddlMesEncabezado.SelectedValue = pFechaActual.Month.ToString();
            txtAnioEncabezado.Text = pFechaActual.Year.ToString();
            ChkPersona_SelectedIndexChanged(ChkPersona, null);            
        }
    }

    #region METHOD RECAPTCHA

    [WebMethod]
    public static string VerifyCaptcha(string response)
    {
        string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
        return (new WebClient()).DownloadString(url);
    }

    #endregion

    protected void CargarDropDownYCheckBox()
    {
        //llenando Drops de Meses
        LlenarMesesDrop(ddlMesEncabezado);
        //LlenarMesesDrop(ddlMesExpedicion);
        //LlenarMesesDrop(ddlMesNacimiento);
        //LlenarMesesDrop(ddlMesInicio);
        //LlenarMesesDrop(ddlMesLiquidacion);

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstCiudades = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        string error;
        lstCiudades = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", " tipo = 3 ", "2", Session["sec"].ToString());
        xpinnWSEstadoCuenta.ListaDesplegable pEntidad = new xpinnWSEstadoCuenta.ListaDesplegable();

        //Llenando ciudades
        if (lstCiudades.Count > 0)
        {
            LlenarDrop(ddlCiudadExpedicion, lstCiudades);
            LlenarDrop(ddlCiudadNacimiento, lstCiudades);
            LlenarDrop(ddlCiudad, lstCiudades);
            LlenarDrop(ddlCiudadLaboral, lstCiudades);
            LlenarDrop(DdlCiudadResidencia, lstCiudades);
            LlenarDrop(DdlCiudadJuridica, lstCiudades);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstDepartamento = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstDepartamento = EstadoServicio.PoblarListaDesplegable("CIUDADES", "", " tipo = 2 ", "2", Session["sec"].ToString());
        //Llenando Departamentos
        if (lstDepartamento.Count > 0)
        {
            LlenarDrop(ddlDepartamento, lstDepartamento);
            LlenarDrop(ddlDepartamentoLaboral, lstDepartamento);
            LlenarDrop(DdlDepartamentoResidecia, lstDepartamento);
            LlenarDrop(DropDownList1, lstDepartamento);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstBarrios = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstBarrios = EstadoServicio.PoblarListaDesplegable("BARRIO", "codbarrio,nombre", "", "1", Session["sec"].ToString());
        //Llenando Barrio
        if (lstBarrios.Count > 0)
        {
            LlenarDrop(ddlBarrio, lstBarrios);
            LlenarDrop(DdlBarrioResidencia, lstBarrios);
            LlenarDrop(DdlBarrioJuridica, lstBarrios);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstTipoContrato = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstTipoContrato = EstadoServicio.PoblarListaDesplegable("TIPOCONTRATO", "", "", "2", Session["sec"].ToString());
        //Llenando Tipo Contrato
        if (lstTipoContrato.Count > 0)
        {
            LlenarDrop(ddlTipoContrato, lstTipoContrato);
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstParentesco;
        lstParentesco = EstadoServicio.PoblarListaDesplegable("PARENTESCOS", "", "", "2", Session["sec"].ToString());
        //Llenando Parentesco
        if (lstParentesco.Count > 0)
        {
            LlenarDrop(ddlParentesco, lstParentesco);
            LlenarDrop(ddlParentescoBenef1, lstParentesco);
            LlenarDrop(ddlParentescoBenef2, lstParentesco);
        }

        //LLenando CheckBoxList Tipo Identificacion
        List<xpinnWSEstadoCuenta.ListaDesplegable> lstTipoIdenti;
        lstTipoIdenti = EstadoServicio.PoblarListaDesplegable("TIPOIDENTIFICACION", "CODTIPOIDENTIFICACION,DESCRIPCION", "", "2", Session["sec"].ToString());
        if (lstTipoIdenti.Count > 0)
        {
            for (int i = 0; i < lstTipoIdenti.Count(); i++)
            {
                ddlDocumento.Items.Add(new ListItem(lstTipoIdenti[i].descripcion.ToString().Trim(), lstTipoIdenti[i].idconsecutivo.ToString()));
                cblDocumentoCony.Items.Add(new ListItem(lstTipoIdenti[i].descripcion.ToString().Trim(), lstTipoIdenti[i].idconsecutivo.ToString()));
                DdlTipoDocumentoRepresentante.Items.Add(new ListItem(lstTipoIdenti[i].descripcion.ToString().Trim(), lstTipoIdenti[i].idconsecutivo.ToString()));
            }
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstEstadoCi = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstEstadoCi = EstadoServicio.PoblarListaDesplegable("ESTADOCIVIL", "", "", "1", Session["sec"].ToString());
        if (lstEstadoCi.Count > 0)
        {
            for (int i = 0; i < lstEstadoCi.Count(); i++)
            {
                cblEstadoCivil.Items.Add(new ListItem(" " + lstEstadoCi[i].descripcion.ToString().Trim() + " ", lstEstadoCi[i].idconsecutivo.ToString()));
            }
        }


        List<xpinnWSEstadoCuenta.ListaDesplegable> lstNivelEsc = new List<xpinnWSEstadoCuenta.ListaDesplegable>();
        lstNivelEsc = EstadoServicio.PoblarListaDesplegable("NIVELESCOLARIDAD", "", "", "1", Session["sec"].ToString());
        if (lstNivelEsc.Count > 0)
        {
            for (int i = 0; i < lstNivelEsc.Count(); i++)
            {
                cbNivelAcademico.Items.Add(new ListItem(" " + lstNivelEsc[i].descripcion.ToString().Trim() + " ", lstNivelEsc[i].idconsecutivo.ToString()));
            }
        }

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstPeriodicidad;
        lstPeriodicidad = EstadoServicio.PoblarListaDesplegable("PERIODICIDAD", "", "", "2", Session["sec"].ToString());
        if (lstPeriodicidad.Count > 0)
            LlenarDrop(ddlPeriodicidadPago, lstPeriodicidad);

        List<xpinnWSEstadoCuenta.ListaDesplegable> lstCargo;
        lstCargo = EstadoServicio.PoblarListaDesplegable("CARGO", "", "", "", Session["sec"].ToString());
        if (lstCargo.Count > 0)
            LlenarDrop(ddlCargo, lstCargo);
        //LLenar temas
        List<xpinnWSAppFinancial.Actividades> lstTemas = new List<xpinnWSAppFinancial.Actividades>();
        lstTemas = AppService.ListarTemasInteres();
        if(lstTemas != null)
        {            
            cbTemas.DataSource = lstTemas;
            cbTemas.DataValueField = "idactividad";
            cbTemas.DataTextField = "descripcion";
            cbTemas.DataBind();
        }
    }

    void LlenarDrop(DropDownList ddlDropCarga, List<xpinnWSEstadoCuenta.ListaDesplegable> listaData)
    {
        ddlDropCarga.DataSource = listaData;
        ddlDropCarga.DataTextField = "descripcion";
        ddlDropCarga.DataValueField = "idconsecutivo";
        ddlDropCarga.AppendDataBoundItems = true;
        ddlDropCarga.Items.Insert(0, new ListItem("Seleccione un item", ""));
        ddlDropCarga.DataBind();
    }

    void LlenarMesesDrop(DropDownList ddlDropCarga)
    {
        ddlDropCarga.Items.Insert(0, new ListItem("Enero", "1"));
        ddlDropCarga.Items.Insert(1, new ListItem("Febrero", "2"));
        ddlDropCarga.Items.Insert(2, new ListItem("Marzo", "3"));
        ddlDropCarga.Items.Insert(3, new ListItem("Abril", "4"));
        ddlDropCarga.Items.Insert(4, new ListItem("Mayo", "5"));
        ddlDropCarga.Items.Insert(5, new ListItem("Junio", "6"));
        ddlDropCarga.Items.Insert(6, new ListItem("Julio", "7"));
        ddlDropCarga.Items.Insert(7, new ListItem("Agosto", "8"));
        ddlDropCarga.Items.Insert(8, new ListItem("Septiembre", "9"));
        ddlDropCarga.Items.Insert(9, new ListItem("Octubre", "10"));
        ddlDropCarga.Items.Insert(10, new ListItem("Noviembre", "11"));
        ddlDropCarga.Items.Insert(11, new ListItem("Diciembre", "12"));
    }


    protected Boolean validarDatos()
    {
        if (ChkPersona.SelectedValue == "1")
        {
            
            if (txtApellido1.Text.Trim() == "")
            {
                lblError.Text = "Ingrese un Apellido ( Datos Personales )";
                return false;
            }
            if (txtNombre1.Text.Trim() == "")
            {
                lblError.Text = "Ingrese un Nombre ( Datos Personales )";
                return false;
            }
            if (cblSexo.SelectedItem == null)
            {
                lblError.Text = "Seleccione el sexo al que pertenece ( Datos Personales ).";
                return false;
            }
            if (ddlDocumento.SelectedItem == null)
            {
                lblError.Text = "Seleccione el tipo de documento de la persona ( Datos Personales ).";
                ddlDocumento.Focus();
                return false;
            }
            if (txtNumero.Text == "")
            {
                lblError.Text = "Ingrese el Número de Identificación ( Datos Personales )";
                return false;
            }
            if (txtDia.Text.Trim() == "")
            {
                lblError.Text = "Ingrese los datos de la fecha de Expedición, Verifique los datos ( Datos Personales )";
                return false;
            }

            if (ddlCiudadExpedicion.SelectedIndex == 0)
            {
                lblError.Text = "Seleccione la Ciudad de Expedición ( Datos Personales )";
                ddlCiudadExpedicion.Focus();
                return false;
            }
            if (TxtNacionalidad.Text == "")
            {
                lblError.Text = "Ingrese su Nacionalidad ( Datos Personales )";
                return false;
            }
            if (txtDianacimiento.Text.Trim() == "")
            {
                lblError.Text = "Ingrese los datos de la fecha de Nacimiento, Verifique los datos ( Datos Personales )";
                txtDianacimiento.Focus();
                return false;
            }
            if (ddlCiudadNacimiento.SelectedIndex == 0)
            {
                lblError.Text = "Seleccione la Ciudad de Nacimiento ( Datos Personales )";
                ddlCiudadNacimiento.Focus();
                return false;
            }
            if (txtDireccion.Text == "")
            {
                lblError.Text = "Ingrese su direccion de residencia ( Datos Personales )";
                return false;
            }
            if (cblEstadoCivil.SelectedItem == null)
            {
                lblError.Text = "Seleccione el estado Civil ( Datos Personales )";
                cblEstadoCivil.Focus();
                return false;
            }
            else
            {
                if (cblEstadoCivil.SelectedIndex == 0)
                {
                    lblError.Text = "Seleccione el estado Civil ( Datos Personales )";
                    cblEstadoCivil.Focus();
                    return false;
                }
            }
            if (cblCabezaFamilia.SelectedItem == null)
            {
                lblError.Text = "Seleccione si es cabeza de familia ( Datos Personales )";
                cblCabezaFamilia.Focus();
                return false;
            }
            if (cbNivelAcademico.SelectedItem == null)
            {
                lblError.Text = "Seleccione su Nivel Académico ( Datos Personales )";
                cbNivelAcademico.Focus();
                return false;
            }
            if (cblEstrato.SelectedItem == null)
            {
                lblError.Text = "Seleccione su estrato ( Datos Personales )";
                cblEstrato.Focus();
                return false;
            }
            if (ddlCiudad.SelectedIndex == 0)
            {
                lblError.Text = "Seleccione la ciudad de residencia ( Datos Personales )";
                ddlCiudad.Focus();
                return false;
            }
            if (txtTelefono.Text == "")
            {
                lblError.Text = "Ingrese su Telefono ( Datos Personales )";
                return false;
            }
            if (txtCelular.Text == "")
            {
                lblError.Text = "Ingrese su Celular ( Datos Personales )";
                return false;
            }
            if (txtEmail.Text == "")
            {
                lblError.Text = "Ingrese su correo electrónico ( Datos Personales )";
                return false;
            }
            Boolean rptaEmail = false;
            rptaEmail = BOValidacion.IsValidEmail(txtEmail.Text);
            if (rptaEmail == false)
            {
                lblError.Text = " El email Ingresado no tiene el formato correcto ( Datos Personales )";
                return false;
            }
            if (ddlEstado.SelectedIndex == 0)
            {
                lblError.Text = " Selleccione su estado laboral (Informacion Laboral)";
                return false;
            }
            else
            {
                if (txtEmpresa.Text == "" && (ddlEstado.SelectedValue != "2" || ddlEstado.SelectedValue != "3"))
                {
                    lblError.Text = "Ingrese el nombre de la empresa donde labora ( Información Laboral )";
                    return false;
                }
                if (txtNit.Text == "" && (ddlEstado.SelectedValue != "2" || ddlEstado.SelectedValue != "3"))
                {
                    lblError.Text = "Ingrese el nit de la empresa donde labora ( Información Laboral )";
                    return false;
                }
                if (ddlCiudadLaboral.SelectedIndex == 0)
                {
                    lblError.Text = "Seleccione la ciudad Laboral ( Información Laboral )";
                    ddlCiudadLaboral.Focus();
                    return false;
                }
                if (txtDiaInicio.Text.Trim() == "" && (ddlEstado.SelectedValue != "2" || ddlEstado.SelectedValue != "3"))
                {
                    lblError.Text = "Ingrese los datos de la fecha de Inicio Laboral, Verifique los datos ( Información Laboral )";
                    return false;
                }
                if (ddlTipoContrato.SelectedIndex == 0)
                {
                    lblError.Text = "Seleccione el Tipo de contrato ( Información Laboral )";
                    ddlTipoContrato.Focus();
                    return false;
                }
                if (txtEmailcontacto.Text.Trim() != "")
                {
                    rptaEmail = false;
                    rptaEmail = BOValidacion.IsValidEmail(txtEmailcontacto.Text);
                    if (rptaEmail == false)
                    {
                        lblError.Text = " El email del contacto no tiene el formato correcto ( Información Laboral )";
                        return false;
                    }
                }
                //if (txtCargo.Text=="")
                //{
                //    lblError.Text = "Ingrese los datos de la fecha de Inicio Laboral, Verifique los datos ( Información Laboral )";
                //    return false;
                //}
            }


            if (txtNumerodocumentoconyugue.Text.Trim() != "")
            {
                if (txtApellido1conyugue.Text.Trim() == "")
                {
                    lblError.Text = "Ingrese un apellido del Conyugue ( Información del conyugue o Familiar )";
                    return false;
                }
                if (txtNombre1conyugue.Text.Trim() == "")
                {
                    lblError.Text = "Ingrese un Nombre del conyugue ( Información del conyugue o Familiar )";
                    return false;
                }
                if (ddlParentesco.SelectedIndex == 0)
                {
                    lblError.Text = "Seleccione el parentesco del Conyugue ( Información del conyugue o Familiar )";
                    ddlParentesco.Focus();
                    return false;
                }
                if (cblDocumentoCony.SelectedItem == null)
                {
                    lblError.Text = "Seleccione el parentesco del Conyugue ( Información del conyugue o Familiar )";
                    ddlParentesco.Focus();
                }
                if (txtDireccionconyugue.Text.Trim() == "")
                {
                    lblError.Text = "ingrese la dirección del conyugue ( información del conyugue o familiar )";
                    return false;
                }

                if (txtEmailconyugue.Text.Trim() != "")
                {
                    rptaEmail = false;
                    rptaEmail = BOValidacion.IsValidEmail(txtEmailconyugue.Text);
                    if (rptaEmail == false)
                    {
                        lblError.Text = " el email del conyugue o familiar no tiene el formato correcto ( información del conyugue o familiar )";
                        return false;
                    }
                }
            }
            
            //if (txtEmailreferencia.Text.Trim() != "")
            //{
            //    rptaEmail = false;
            //    rptaEmail = BOValidacion.IsValidEmail(txtEmailreferencia.Text);
            //    if (rptaEmail == false)
            //    {
            //        lblError.Text = " El email de referencia no tiene el formato correcto ( Información del conyugue o Familiar )";
            //        return false;
            //    }
            //}
        }

        if (ChkPersona.SelectedValue == "0")
        {
            if (txtRazonSoial.Text.Trim() == "")
            {
                lblError.Text = "Ingrese la Razon Social ( Datos de la Entidad )";
                return false;
            }
            if (txtNitJuridica.Text.Trim() == "")
            {
                lblError.Text = "Ingrese el Nit de la Entidad ( Datos de la Entidad )";
                return false;
            }
            if (txtCamaraComercio.Text.Trim() == "")
            {
                lblError.Text = "Ingrese la Camara de Comercio ( Datos de la Entidad )";
                return false;
            }
            if (txtPaisConstitución.Text.Trim() == "")
            {
                lblError.Text = "Ingrese el País de Constitución de la Entidad ( Datos de la Entidad )";
                return false;
            }
            if (txtDirecciónDomicilio.Text.Trim() == "")
            {
                lblError.Text = "Ingrese la Dirección ( Datos de la Entidad )";
                return false;
            }
            if (DdlCiudadResidencia.SelectedIndex == 0)
            {
                lblError.Text = "Seleccione la ciudad de residencia ( Datos de la Entidad )";
                ddlCiudad.Focus();
                return false;
            }

            if (TxtTelefonoResidencia.Text.Trim() == "")
            {
                lblError.Text = "Ingrese el Teléfono  ( Datos de la Entidad )";
                return false;
            }
            if (DdlTipoDocumentoRepresentante.SelectedIndex == 0)
            {
                lblError.Text = "Ingrese la Tipo de documento del representante ( Datos de la Entidad )";
                return false;
            }
            if (TxtDocumentoJuridica.Text.Trim() == "")
            {
                lblError.Text = "Ingrese el documento del representante ( Datos de la Entidad )";
                return false;
            }
            if (TxtDireccionJuridica.Text.Trim() == "")
            {
                lblError.Text = "Ingrese la Dirección del representante ( Datos de la Entidad )";
                return false;
            }
            if (DdlCiudadJuridica.SelectedIndex == 0)
            {
                lblError.Text = "Ingrese la Ciudad del representante ( Datos de la Entidad )";
                return false;
            }
            if (TxtTelefonoRepresentante.Text.Trim() == "")
            {
                lblError.Text = "Ingrese el Teléfono del representante ( Datos de la Entidad )";
                return false;
            }
        }

        // VALIDACION DE RECAPTCHA
        if (string.IsNullOrEmpty(txtCaptcha.Text))
        {
            lblError.Text = "Metodo captcha inválido..!";
            return false;
        }

        return true;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        try
        {
            if (validarDatos())
                ctlMensaje.MostrarMensaje("Desea grabar la información?");
            else
                txtCaptcha.Text = string.Empty;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
        lblError.Text = "";
        try
        {

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnContinuarMen_Click(object sender, EventArgs e)
    {
        try
        {
            xpinnWSEstadoCuenta.SolicitudPersonaAfi pEntidad = new xpinnWSEstadoCuenta.SolicitudPersonaAfi();

            if (ChkPersona.SelectedValue == "1")
            {


                //DATOS PERSONALES
                pEntidad.id_persona = 0;
                pEntidad.tipo_persona = "N";
                pEntidad.fecha_creacion = DateTime.Now;
                pEntidad.primer_nombre = txtNombre1.Text.ToUpper().Trim();
                pEntidad.segundo_nombre = txtNombre2.Text.Trim() != "" ? txtNombre2.Text.ToUpper().Trim() : null;
                pEntidad.primer_apellido = txtApellido1.Text.ToUpper().Trim();
                pEntidad.segundo_apellido = txtApellido2.Text.Trim() != "" ? txtApellido2.Text.ToUpper().Trim() : null;
                if (cblSexo.SelectedItem != null)
                {
                    pEntidad.sexo = cblSexo.SelectedValue;
                }
                else
                    pEntidad.sexo = null;
                pEntidad.tipo_identificacion = Convert.ToInt64(ddlDocumento.SelectedValue);
                pEntidad.identificacion = txtNumero.Text.Trim();

                DateTime pFec = Convert.ToDateTime(txtDia.Text);
                DateTime pFecha = DateTime.ParseExact((pFec.Day.ToString("00") + "/" + pFec.Month.ToString("00") + "/" + pFec.Year.ToString("0000")), "dd/MM/yyyy", null);
                pEntidad.fecha_expedicion = pFecha;
                pEntidad.ciudad_expedicion = Convert.ToInt64(ddlCiudadExpedicion.SelectedValue);
                pEntidad.pais = TxtNacionalidad.Text;
                pFec = Convert.ToDateTime(txtDianacimiento.Text);
                pFecha = DateTime.MinValue;
                pFecha = DateTime.ParseExact((pFec.Day.ToString("00") + "/" + pFec.Month.ToString("00") + "/" + pFec.Year.ToString("0000")), "dd/MM/yyyy", null);
                pEntidad.fecha_nacimiento = pFecha;
                pEntidad.ciudad_nacimiento = Convert.ToInt64(ddlCiudadNacimiento.SelectedValue);
                pEntidad.codestadocivil = cblEstadoCivil.SelectedItem == null ? 0 : Convert.ToInt64(cblEstadoCivil.SelectedValue);
                pEntidad.cabeza_familia = cblCabezaFamilia.SelectedItem == null ? 0 : Convert.ToInt32(cblCabezaFamilia.SelectedValue);
                pEntidad.personas_cargo = txtPersonaCargo.Text.Trim() == "" ? 0 : Convert.ToInt32(txtPersonaCargo.Text.Trim());
                pEntidad.tiene_hijos = txthijos.Text.Trim() == "" ? 0 : Convert.ToInt32(txthijos.Text);
                pEntidad.codescolaridad = Convert.ToInt32(cbNivelAcademico.SelectedValue);
                pEntidad.profesion = txtProfesion.Text.Trim() != "" ? txtProfesion.Text.Trim().ToUpper() : null;
                pEntidad.direccion = txtDireccion.Text.Trim();
                pEntidad.estrato = cblEstrato.SelectedIndex == 0 ? 0 : Convert.ToInt32(cblEstrato.SelectedValue);
                pEntidad.barrio = ddlBarrio.SelectedIndex > 0 ? Convert.ToInt64(ddlBarrio.SelectedValue) : 0;
                pEntidad.ciudad = Convert.ToInt64(ddlCiudad.SelectedValue);
                pEntidad.departamento = ddlDepartamento.SelectedIndex > 0 ? Convert.ToInt64(ddlDepartamento.SelectedValue) : 0;
                pEntidad.telefono = txtTelefono.Text.Trim() != "" ? txtTelefono.Text.Trim() : null;
                pEntidad.celular = txtCelular.Text.Trim() != "" ? txtCelular.Text.Trim() : null;
                pEntidad.email = txtEmail.Text.Trim();

                //DATOS LABORALES
                pEntidad.estado_empresa = ddlEstado.SelectedValue;
                pEntidad.empresa = txtEmpresa.Text.Trim();
                pEntidad.nit = txtNit.Text.Trim();
                pEntidad.direccion_empresa = txtDireccionLaboral.Text.Trim() != "" ? txtDireccionLaboral.Text.Trim() : null;
                pEntidad.telefono_empresa = txtTelefonolaboral.Text.Trim() != "" ? txtTelefonolaboral.Text.Trim() : null;
                pEntidad.ciudad_empresa = Convert.ToInt64(ddlCiudadLaboral.SelectedValue);
                pEntidad.departamento_empresa = ddlDepartamentoLaboral.SelectedIndex > 0 ? Convert.ToInt64(ddlDepartamentoLaboral.SelectedValue) : 0;
                pEntidad.codtipocontrato = Convert.ToInt64(ddlTipoContrato.SelectedValue);
                if (ddlCargo.SelectedIndex > 0)
                    pEntidad.codcargo = Convert.ToInt32(ddlCargo.SelectedValue);
                pFec = Convert.ToDateTime(txtDiaInicio.Text);
                pFecha = DateTime.MinValue;
                pFecha = DateTime.ParseExact((pFec.Day.ToString("00") + "/" + pFec.Month.ToString("00") + "/" + pFec.Year.ToString("0000")), "dd/MM/yyyy", null);
                pEntidad.fecha_inicio = pFecha;

                pEntidad.cod_periodicidad_pago = ddlPeriodicidadPago.SelectedIndex > 0 ? ddlPeriodicidadPago.SelectedValue : null;
                pEntidad.ingresos_mensuales = Convert.ToInt64(txtIngsalariomensual.Text.Replace(".", ""));
                pEntidad.otros_ingresos = Convert.ToDecimal(txtOtrosing.Text.Replace(".", ""));
                pEntidad.deducciones = Convert.ToDecimal(txtDeducciones.Text.Replace(".", ""));
                pEntidad.salario = pEntidad.ingresos_mensuales + pEntidad.otros_ingresos - pEntidad.deducciones;
                pEntidad.admrecursos = Convert.ToInt32(ChkRecursosPublicos.SelectedValue);
                pEntidad.peps = Convert.ToInt32(ChkPeps.SelectedValue);
                pEntidad.actividad_economica = !string.IsNullOrEmpty(TxtDescripcionEconomica.Text) ? TxtDescripcionEconomica.Text : null;
                pEntidad.ciiu = TxtCiiu.Text;
                TxtTotalActivos.Text = string.IsNullOrEmpty(TxtTotalActivos.Text) ? "0" : TxtTotalActivos.Text;
                pEntidad.total_activos = Convert.ToInt64(TxtTotalActivos.Text);
                TxtTotalPasivos.Text = string.IsNullOrEmpty(TxtTotalPasivos.Text) ? "0" : TxtTotalPasivos.Text;
                pEntidad.total_pasivos = Convert.ToInt64(TxtTotalPasivos.Text);
                TxtTotalPatrimonio.Text = string.IsNullOrEmpty(TxtTotalPatrimonio.Text) ? "0" : TxtTotalPatrimonio.Text;
                pEntidad.total_patrimonio = Convert.ToInt64(TxtTotalPatrimonio.Text);



                if (txtDialiquidacion.Text.Trim() == "")
                    pEntidad.fecha_ult_liquidacion = DateTime.MinValue;
                else
                {
                    pFec = Convert.ToDateTime(txtDialiquidacion.Text);
                    pFecha = DateTime.MinValue;
                    pFecha = DateTime.ParseExact((pFec.Day.ToString("00") + "/" + pFec.Month.ToString("00") + "/" + pFec.Year.ToString("0000")), "dd/MM/yyyy", null);
                    pEntidad.fecha_ult_liquidacion = pFecha;
                }

                pEntidad.empresa_contacto = Txtcontacto.Text.Trim() != "" ? Txtcontacto.Text.ToUpper().Trim() : null;
                pEntidad.telefono_contacto = txtTelcontacto.Text.Trim() != "" ? txtTelcontacto.Text.Trim() : null;
                pEntidad.cargo_contacto = txtCargocontacto.Text.Trim() != "" ? txtCargocontacto.Text.Trim() : null;
                pEntidad.email_contacto = txtEmailcontacto.Text.Trim() != "" ? txtEmailcontacto.Text.Trim() : null;
                pEntidad.empresa_anterior_contacto = txtEmpresaAnterior.Text.Trim() != "" ? txtEmpresaAnterior.Text.Trim() : null;
                pEntidad.cargo_anterior = txtCargoanterior.Text.Trim() != "" ? txtCargoanterior.Text.Trim() : null;
                pEntidad.telefono_anterior = txtTelefonoanterior.Text.Trim() != "" ? txtTelefonoanterior.Text.Trim() : null;

                //DATOS CONYUGUE
                pEntidad.primer_nombre_cony = txtNombre1conyugue.Text.Trim() != "" ? txtNombre1conyugue.Text.Trim() : null;
                pEntidad.segundo_nombre_cony = txtNombre2conyugue.Text.Trim() != "" ? txtNombre2conyugue.Text.Trim() : null;
                pEntidad.primer_apellido_cony = txtApellido1conyugue.Text.Trim() != "" ? txtApellido1conyugue.Text.Trim() : null;
                pEntidad.segundo_apellido_cony = txtApellido2conyugue.Text.Trim() != "" ? txtApellido2conyugue.Text.Trim() : null;
                pEntidad.codparentesco = ddlParentesco.SelectedIndex > 0 ? Convert.ToInt32(ddlParentesco.SelectedValue) : 0;
                pEntidad.tipo_identificacion_cony = cblDocumentoCony.SelectedIndex > 0 ? Convert.ToInt64(cblDocumentoCony.SelectedValue) : 0;
                pEntidad.identificacion_cony = txtNumerodocumentoconyugue.Text.Trim();
                pEntidad.direccion_cony = txtDireccionconyugue.Text.Trim();
                pEntidad.telefono_cony = txtTelefonoconyugue.Text.Trim() != "" ? txtTelefonoconyugue.Text.Trim() : null;
                pEntidad.email_cony = txtEmailconyugue.Text.Trim() != "" ? txtEmailconyugue.Text.Trim() : null;
                pEntidad.estado_cony = ddlEstadoconyugue.SelectedValue;
                pEntidad.empresa_cony = txtEmpresaconyugue.Text.Trim() != "" ? txtEmpresaconyugue.Text.Trim() : null;
                pEntidad.ingresos_cony = Convert.ToDecimal(txtIngconyugue.Text.Replace(".", ""));
                pEntidad.cargo_cony = txtCargolaboralconyugue.Text.Trim() != "" ? txtCargolaboralconyugue.Text.Trim() : null;
                pEntidad.direccion_lab_cony = txtDireccionlabConyugue.Text.Trim() != "" ? txtDireccionlabConyugue.Text.Trim() : null;
                pEntidad.telefono_lab_cony = txtTelefonolabconyugue.Text.Trim() != "" ? txtTelefonolabconyugue.Text.Trim() : null;

                //DATOS REFERENCIA
                pEntidad.primer_nombre_referencia = txtNombre1referencia.Text.Trim() != "" ? txtNombre1referencia.Text.Trim() : null;
                pEntidad.segundo_nombre_referencia = txtNombre2referencia.Text.Trim() != "" ? txtNombre2referencia.Text.Trim() : null;
                pEntidad.primer_apellido_referencia = txtApellido1refencia.Text.Trim() != "" ? txtApellido1refencia.Text.Trim() : null;
                pEntidad.segundo_apellido_referencia = txtApellido2refencia.Text.Trim() != "" ? txtApellido2refencia.Text.Trim() : null;
                pEntidad.relacion_referencia = txtRelacionreferencia.Text.Trim() != "" ? txtRelacionreferencia.Text.Trim() : null;
                pEntidad.direccion_referencia = txtDireccionresidencia.Text.Trim() != "" ? txtDireccionresidencia.Text.Trim() : null;
                pEntidad.celular_referencia = txtCelreferencia.Text.Trim() != "" ? txtCelreferencia.Text.Trim() : null;
                pEntidad.telefono_referencia = txtTelfijoRef.Text.Trim() != "" ? txtTelfijoRef.Text.Trim() : null;
                pEntidad.email_referencia = txtEmailreferencia.Text.Trim() != "" ? txtEmailreferencia.Text.Trim() : null;

                //DATOS BENEFICIARIOS
                pEntidad.nombres_primer_benef = txtNombre1benef.Text.Trim() != "" ? txtNombre1benef.Text.Trim() : null;
                pEntidad.apellidos_primer_benef = txtApellido1benef.Text.Trim() != "" ? txtApellido1benef.Text.Trim() : null;
                pEntidad.identificacion_primer_benef = txtDocumentobenef1.Text.Trim() != "" ? txtDocumentobenef1.Text.Trim() : null;
                pEntidad.codparentesco_primer_benef = ddlParentescoBenef1.SelectedIndex > 0 ? Convert.ToInt64(ddlParentescoBenef1.SelectedValue) : 0;
                pEntidad.porcentaje_primer_benef = TxtPorcBenef1.Text.Trim() != "" ? Convert.ToDecimal(TxtPorcBenef1.Text.Trim()) : 0;

                pEntidad.nombres_segun_benef = txtNombre2benef.Text.Trim() != "" ? txtNombre2benef.Text.Trim() : null;
                pEntidad.apellidos_segun_benef = txtApellido2benef.Text.Trim() != "" ? txtApellido2benef.Text.Trim() : null;
                pEntidad.identificacion_segun_benef = txtDocumentobenef2.Text.Trim() != "" ? txtDocumentobenef2.Text.Trim() : null;
                pEntidad.codparentesco_segun_benef = ddlParentescoBenef2.SelectedIndex > 0 ? Convert.ToInt64(ddlParentescoBenef2.SelectedValue) : 0;
                pEntidad.porcentaje_segun_benef = TxtPorcBenef2.Text.Trim() != "" ? Convert.ToDecimal(TxtPorcBenef2.Text.Trim()) : 0;

                pEntidad.estado = 0;

                //DATOS MONEDA EXTRANJERA 
                pEntidad.ncuenta = TxtNumeroCuenta.Text.Trim() == "" ? 0 : Convert.ToInt64(TxtNumeroCuenta.Text);
                pEntidad.nom_banco = TxtBanco.Text.Trim() != "" ? TxtBanco.Text : null;
                pEntidad.paismoneda = TxtPaís.Text.Trim() != "" ? TxtPaís.Text : null;
                pEntidad.ciudadmoneda = TxtCiudad.Text.Trim() != "" ? TxtCiudad.Text : null;
                pEntidad.moneda = TxtMoneda.Text.Trim() != "" ? TxtMoneda.Text : null;
                pEntidad.operacion = TxtCuales.Text.Trim() != "" ? TxtCuales.Text : null;

                pEntidad = EstadoServicio.CrearSolicitudPersonaAfi(pEntidad,1, Session["sec"].ToString());
                if (pEntidad.rpta == false && pEntidad.mensaje_error != null)
                {
                    lblError.Text = pEntidad.mensaje_error;
                    return;
                }
                if (pEntidad.id_persona != 0)
                {
                    lblCodigoGenerado.Text = pEntidad.id_persona.ToString();
                    panelData.Visible = false;
                    panelFinal.Visible = true;
                }

            }
            if (ChkPersona.SelectedValue == "0")
            {
                pEntidad.id_persona = 0;
                pEntidad.tipo_persona = "J";
                pEntidad.fecha_creacion = DateTime.Now;
                pEntidad.razon_social = txtRazonSoial.Text.Trim() != "" ? txtRazonSoial.Text.ToUpper().Trim() : null;
                pEntidad.primer_nombre = txtRazonSoial.Text.Trim() != "" ? txtRazonSoial.Text.ToUpper().Trim() : null;
                pEntidad.nit = txtNitJuridica.Text.Trim() != "" ? txtNitJuridica.Text.Trim() : null;
                pEntidad.camara_comercio = txtCamaraComercio.Text.Trim() != "" ? txtCamaraComercio.Text.Trim() : null;
                pEntidad.pais = txtPaisConstitución.Text.Trim() != "" ? txtPaisConstitución.Text.Trim() : null;
                pEntidad.direccion_empresa = txtDirecciónDomicilio.Text.Trim() != "" ? txtDirecciónDomicilio.Text.Trim() : null;
                if (DdlDepartamentoResidecia.SelectedIndex > 0)
                    pEntidad.departamento_empresa = Convert.ToInt64(DdlDepartamentoResidecia.SelectedValue);

                pEntidad.ciudad_empresa = Convert.ToInt64(DdlCiudadResidencia.SelectedValue);
                pEntidad.telefono_empresa = TxtTelefonoResidencia.Text.Trim() != "" ? TxtTelefonoResidencia.Text.Trim() : null;
                pEntidad.cod_representante = lblCod_repre.Text.Trim() == "" ? 0 : Convert.ToInt64(lblCod_repre.Text);
                pEntidad.actividad_economica = TxtActividadEconomica.Text.Trim() != "" ? TxtActividadEconomica.Text.Trim() : null;
                pEntidad.ciiu = TxtCiiuJuridica.Text.Trim() != "" ? TxtCiiuJuridica.Text.Trim() : null;
                pEntidad.ingresos_mensuales = txtIngresosJuridica.Text.Trim() == "" ? 0 : Convert.ToInt64(txtIngresosJuridica.Text);
                pEntidad.detotros_ingresos = TxtDetalleOtros.Text.Trim() != "" ? TxtDetalleOtros.Text.Trim() : null;
                pEntidad.egresos_mensuales = txtEgresosJuridica.Text.Trim() == "" ? 0 : Convert.ToInt64(txtEgresosJuridica.Text);
                pEntidad.total_activos = txtTotalActivosJuridica.Text.Trim() == "" ? 0 : Convert.ToInt64(txtTotalActivosJuridica.Text);
                pEntidad.total_pasivos = txtTotalPasivosJuridica.Text.Trim() == "" ? 0 : Convert.ToInt64(txtTotalPasivosJuridica.Text);
                pEntidad.total_patrimonio = txtTotalPatrimonioJuridica.Text.Trim() == "" ? 0 : Convert.ToInt64(txtTotalPatrimonioJuridica.Text);
                pEntidad.tipo_empresa = Convert.ToInt32(ChkTipoEmpresa.SelectedValue);
                pEntidad.tipo_identificacion = 2;
                pEntidad.identificacion = txtNitJuridica.Text.Trim() != "" ? txtNitJuridica.Text.Trim() : null;
                pEntidad.fecha_expedicion = DateTime.Now;
                pEntidad.ciudad_expedicion = Convert.ToInt64(DdlCiudadResidencia.SelectedValue);
                pEntidad.ciudad = Convert.ToInt64(DdlCiudadResidencia.SelectedValue);

                pEntidad.ncuenta = TxtNumeroCuenta.Text.Trim() == "" ? 0 : Convert.ToInt64(TxtNumeroCuenta.Text);
                pEntidad.nom_banco = TxtBanco.Text.Trim() != "" ? TxtBanco.Text : null;
                pEntidad.paismoneda = TxtPaís.Text.Trim() != "" ? TxtPaís.Text : null;
                pEntidad.ciudadmoneda = TxtCiudad.Text.Trim() != "" ? TxtCiudad.Text : null;
                pEntidad.moneda = TxtMoneda.Text.Trim() != "" ? TxtMoneda.Text : null;
                pEntidad.operacion = TxtCuales.Text.Trim() != "" ? TxtCuales.Text : null;
                pEntidad.estado = 0;                

                pEntidad = EstadoServicio.CrearSolicitudPersonaAfi(pEntidad, 1, Session["sec"].ToString());
                if (pEntidad.rpta == false && pEntidad.mensaje_error != null)
                {
                    lblError.Text = pEntidad.mensaje_error;
                    txtCaptcha.Text = string.Empty;
                    return;
                }
                if (pEntidad.id_persona != 0)
                {
                    //Almacenar respuestas
                    guardarTemas();
                    lblCodigoGenerado.Text = pEntidad.id_persona.ToString();
                    panelData.Visible = false;
                    panelFinal.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "No se pudieron grabar los datos. Error: " + ex.Message;
            txtCaptcha.Text = string.Empty;
        }
    }

    private void guardarTemas()
    {
        List<xpinnWSEstadoCuenta.Persona1> lstTema = new List<xpinnWSEstadoCuenta.Persona1>();
        foreach (ListItem li in cbTemas.Items)
        {
            if (li.Selected)
            {
                xpinnWSEstadoCuenta.Persona1 item = new xpinnWSEstadoCuenta.Persona1();
                item.id_solicitud = Convert.ToInt32(li.Value);
                item.identificacion = txtNumero.Text;
                item.cod_persona = 0;
                if (li.Text == "Otro")
                    item.descripcion = txtOtroTema.Text;
                else
                    item.descripcion = "";
                lstTema.Add(item);
            }
        }
        if(lstTema != null && lstTema.Count > 0)
        {
            EstadoServicio.guardarPersonaTema(lstTema, Session["sec"].ToString());
        }
    }

    protected void TotalizarIngresos()
    {
        decimal pVrTotal = 0, pVrIngSalario = 0, pVrOtros = 0, pVrDeduccion = 0;

        pVrIngSalario = Convert.ToDecimal(txtIngsalariomensual.Text.Replace(".", ""));
        pVrOtros = Convert.ToDecimal(txtOtrosing.Text.Replace(".", ""));
        pVrDeduccion = Convert.ToDecimal(txtDeducciones.Text.Replace(".", ""));
        pVrTotal = pVrIngSalario + pVrOtros - pVrDeduccion;
        txtTotalIng.Text = pVrTotal.ToString("n0");
    }

    protected void txtIngsalariomensual_TextChanged(object sender, EventArgs e)
    {
        TotalizarIngresos();
    }
    protected void txtOtrosing_TextChanged(object sender, EventArgs e)
    {
        TotalizarIngresos();
    }
    protected void txtDeducciones_TextChanged(object sender, EventArgs e)
    {
        TotalizarIngresos();
    }
    protected void TxtDocumentoJuridica_TextChanged(object sender, EventArgs e)
    {
        Int64 identificacion = Convert.ToInt64(TxtDocumentoJuridica.Text);
        xpinnWSEstadoCuenta.SolicitudPersonaAfi pRepresentante = new xpinnWSEstadoCuenta.SolicitudPersonaAfi();
        //pRepresentante = EstadoServicio.ListarRepresentantes(identificacion);       
        txtRepresentanteLegal.Text = pRepresentante.primer_nombre + " " + pRepresentante.segundo_nombre + " " + pRepresentante.primer_apellido + " " + pRepresentante.segundo_apellido;
        lblCod_repre.Text = pRepresentante.id_persona.ToString();
        DdlTipoDocumentoRepresentante.SelectedIndex = Convert.ToInt32(pRepresentante.tipo_identificacion);
        TxtDireccionJuridica.Text = pRepresentante.direccion;
        TxtTelefonoRepresentante.Text = pRepresentante.telefono;
        //DdlCiudadJuridica.SelectedIndex = Convert.ToInt32(pRepresentante.ciudad);

    }

    protected void btnSolicitudCred_Click(object sender, EventArgs e)
    {
        if (lblCodigoGenerado.Text != "")
        {
            Response.Redirect("~/Pages/Credito/Solicitud/Credito.aspx?id=" + lblCodigoGenerado.Text.Trim());
        }
        else
            lblError.Text = "Ocurrio un error al redireccionar a la pagina.";
    }


    protected void btnInicioSesion_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void ChkPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ChkPersona.SelectedValue == "1")
        {
            Juridica.Visible = false;
            Natural.Visible = true;
        }

        if (ChkPersona.SelectedValue == "0")
        {
            Natural.Visible = false;
            Juridica.Visible = true;
        }

    }


    protected void txtNumero_TextChanged(object sender, EventArgs e)
    {
            cargarDatosPrevios();
    }

    protected void ddlDocumento_SelectedIndexChanged(object sender, EventArgs e)
    {
        cargarDatosPrevios();        
    }

    private void cargarDatosPrevios()
    {
        if (string.IsNullOrWhiteSpace(txtNumero.Text) && string.IsNullOrWhiteSpace(ddlDocumento.SelectedValue.ToString()))
        {
            string sec = "";
            if (Session["sec"] != null)
                sec = Session["sec"].ToString();
            xpinnWSEstadoCuenta.Persona1 cliente = new xpinnWSEstadoCuenta.Persona1();
            cliente.identificacion = txtNumero.Text;
            cliente.tipo_identificacion = Convert.ToInt64(ddlDocumento.SelectedValue);
            cliente = EstadoServicio.ConsultarDatosCliente(cliente, sec);
            if(cliente != null)
            {
                //cliente.cod_persona 
                if(cliente.primer_nombre != null)txtNombre1.Text = cliente.primer_nombre;
                if(cliente.segundo_nombre != null)txtNombre2.Text = cliente.segundo_nombre;
                if(cliente.primer_apellido != null)txtApellido1.Text = cliente.primer_apellido;
                if(cliente.segundo_apellido != null)txtApellido2.Text = cliente.segundo_apellido;
                if(cliente.direccion != null)txtDireccion.Text = cliente.direccion;
                if(cliente.telefono != null)txtCelular.Text = cliente.telefono;
                if(cliente.email != null)txtEmail.Text = cliente.email;
            }
        }
    }

}