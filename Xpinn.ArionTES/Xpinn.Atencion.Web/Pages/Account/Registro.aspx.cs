using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xpinn.Util;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;

public partial class Pages_Account_Registro : GlobalWeb
{

    Validadores ValidarData = new Validadores();
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient ServicioEstCta = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    xpinnWSAppFinancial.WSAppFinancialSoapClient ServicesAppFinancial = new xpinnWSAppFinancial.WSAppFinancialSoapClient();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            //ctlMensaje.eventoClick += btnContinuarMen_Click;
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
            buildSecure();
            //cargarDropDown();
            lblPassword.Text = "";
            Session["VALIDAR"] = null;
            Session["APORTE"] = null;
            cargarRestriccion();
        }
    }

    protected void cargarRestriccion()
    {
        xpinnWSLogin.WSloginSoapClient Logservicio = new xpinnWSLogin.WSloginSoapClient();
        xpinnWSLogin.Perfil vRestriccion = new xpinnWSLogin.Perfil();
        vRestriccion = Logservicio.consultarPerfil(Session["sec"].ToString());
        txtMayuscula.Text = Convert.ToString(vRestriccion.mayuscula);
        txtCaracter.Text = Convert.ToString(vRestriccion.caracter);
        txtNumero.Text = Convert.ToString(vRestriccion.numero);
        txtLongitud.Text = vRestriccion.longitud == 0 ? "6" : Convert.ToString(vRestriccion.longitud);
    }
    /*
    void cargarDropDown()
    {
        int cont = 0;
        //CARGANDO LOS DIAS DEL MES
        for (int i = 1; i <= 31; i++)
        {
            if (cont == 0)
            {
                ddlDia.Items.Insert(cont, new ListItem("Día", "0"));
                cont++;
            }
            ddlDia.Items.Insert(cont, new ListItem(i.ToString(), i.ToString()));
            cont++;
        }

        //CARGANDO LOS MESES DEL AÑO
        ddlMes.Items.Insert(0, new ListItem("Mes", "0"));
        ddlMes.Items.Insert(1, new ListItem("Enero", "1"));
        ddlMes.Items.Insert(2, new ListItem("Febrero", "2"));
        ddlMes.Items.Insert(3, new ListItem("Marzo", "3"));
        ddlMes.Items.Insert(4, new ListItem("Abril", "4"));
        ddlMes.Items.Insert(5, new ListItem("Mayo", "5"));
        ddlMes.Items.Insert(6, new ListItem("Junio", "6"));
        ddlMes.Items.Insert(7, new ListItem("Julio", "7"));
        ddlMes.Items.Insert(8, new ListItem("Agosto", "8"));
        ddlMes.Items.Insert(9, new ListItem("Septiembre", "9"));
        ddlMes.Items.Insert(10, new ListItem("Octubre", "10"));
        ddlMes.Items.Insert(11, new ListItem("Noviembre", "11"));
        ddlMes.Items.Insert(12, new ListItem("Diciembre", "12"));

        //CARGANDO LOS AÑOS
        cont = 0;
        for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 100; i--)
        {
            if (cont == 0)
            {
                ddlAnio.Items.Insert(cont, new ListItem("Año", "0"));
                cont++;
            }
            ddlAnio.Items.Insert(cont, new ListItem(i.ToString(), i.ToString()));
            cont++;
        }
    }
    */

    #region METODOS DE VALIDACION

    private bool ValidarIdentificacion()
    {
        try
        {
            lblError.Visible = false;
            lblError.Text = "";
            if (txtIdentificacion.Text == "")
            {
                lblError.Visible = true;
                lblError.Text = "Ingrese su identificación para generar la consulta. Verifique los datos...";
                txtIdentificacion.Focus();
                return false;
            }
            else
            {
                //generear consulta
                xpinnWSEstadoCuenta.Persona1 pEntidad = new xpinnWSEstadoCuenta.Persona1();
                Usuario pUsu = new Usuario();
                pEntidad = ServicioEstCta.ConsultarPersonaAPP(txtIdentificacion.Text.Trim());

                if (pEntidad == null)
                {
                    lblError.Visible = true;
                    lblError.Text = "Su consulta no obtuvo ningún resultado. Vuelva a intentarlo.";
                    return false;
                }
                else
                {
                    if (pEntidad.nombre == "errordedatos")
                    {
                        lblError.Visible = true;
                        lblError.Text = "Su consulta no obtuvo ningún resultado. Vuelva a intentarlo.";
                        return false;
                    }
                    pEntidad.rptaingreso = true;
                    if (pEntidad.nombre_app != "" || pEntidad.apellidos_app != "")
                        pEntidad.rptaingreso = false;
                    if (pEntidad.rptaingreso == false)
                    {
                        lblError.Visible = true;
                        lblError.Text = "La identificación ingresada ya cuenta con un usuario creado.";
                        return false;
                    }
                    if (pEntidad.cod_persona != 0)
                        txtCodPersona.Text = pEntidad.cod_persona.ToString().Trim();

                    if(pEntidad.tipo_identificacion == 1 || pEntidad.tipo_identificacion == 3 || pEntidad.tipo_identificacion == 5)
                    {
                        lblMessageFinal.Text = "Se grabó correctamente el usuario de la persona con identificación";
                        if (string.IsNullOrEmpty(pEntidad.email))
                        {
                            lblMessageFinal.Text = "No registra un correo electrónico válido en nuestra base de datos, por favor acérquese a la entidad o comuníquese con nosotros.";
                            btnRegresar.Text = "Regresar";
                            mtvPrincipal.ActiveViewIndex = 3;
                            return false;
                        }

                        lblEmailCooperativa.Text = pEntidad.email.Trim();
                        txtNombres.Text = pEntidad.nombres;
                        txtApellidos.Text = pEntidad.apellidos;
                        txtFecNacimiento.Text = pEntidad.fechanacimientoAPP;
                    }
                    else
                    {
                        lblMessageFinal.Text = "El acceso a la Oficina Virtual está restringido para este tipo de documento.";
                        return false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Visible = true;
            lblError.Text = ex.Message;
            return false;
        }
        return true;
    }

    Boolean ValidarDatos()
    {
        if (string.IsNullOrEmpty(txtIdentificacion.Text))
        {
            lblError.Text = "No se generó la consulta del Afiliado.";
            lblError.Visible = true;
            txtIdentificacion.Focus();
            return false;
        }
        if (txtNombres.Text == "")
        {
            lblError.Text = "Ingrese su(s) nombre(s), verifique los datos.";
            lblError.Visible = true;
            txtNombres.Focus();
            return false;
        }
        if (txtApellidos.Text == "")
        {
            lblError.Text = "No se generó la consulta del Afiliado.";
            lblError.Visible = true;
            txtApellidos.Focus();
            return false;
        }
        if (string.IsNullOrWhiteSpace(lblEmailCooperativa.Text.Trim()))
        {
            lblError.Text = "Ingrese una direccion de correo electrónico con el siguiente formato. alguien@example.com";
            lblError.Visible = true;
            return false;
        }
        else
        {
            bool rpta = false;
            rpta = ValidarData.IsValidEmail(lblEmailCooperativa.Text.Trim());
            if (rpta == false)
            {
                lblError.Text = "La dirección de correo electrónico que tenemos registrada no es válida, por favor comuníquese con nosotros indicándonos este problema.";
                lblError.Visible = true;
                return false;
            }
            //Validando que no exista esa direccion de correo electronico
            rpta = false;
            string pFiltro = " where lower(a.email) = '" + lblEmailCooperativa.Text.ToLower() + "'";
            rpta = ServicioEstCta.ExisteRegistrosEmail(pFiltro, Session["sec"].ToString());
            if (rpta == true)
            {
                lblError.Text = "La dirección de correo electrónico ya fue registrado. Modifique la información.";
                lblError.Visible = true;
                return false;
            }
        }
        /*
        if (ddlDia.SelectedIndex == 0)
        {
            lblError.Text = "Seleccione el dia de Nacimiento.";
            lblError.Visible = true;
            ddlDia.Focus();
            return false;
        }
        if (ddlMes.SelectedIndex == 0)
        {
            lblError.Text = "Seleccione el mes de Nacimiento.";
            lblError.Visible = true;
            ddlMes.Focus();
            return false;
        }
        if (ddlAnio.SelectedIndex == 0)
        {
            lblError.Text = "Seleccione el año de Nacimiento.";
            lblError.Visible = true;
            ddlAnio.Focus();
            return false;
        }
        int Valid = 0;
        if (ddlMes.SelectedValue == "2")
        {
            if (Convert.ToInt32(ddlAnio.SelectedValue) % 4 == 0)
            {
                if (Convert.ToInt32(ddlDia.SelectedValue) > 29)
                    Valid = 1;
            }
            else
            {
                if (Convert.ToInt32(ddlDia.SelectedValue) > 28)
                    Valid = 2;
            }
        }
        if (Valid >= 1)
        {
            if (Valid == 1)
                lblError.Text = "Seleccione un dia menor a 29 para el mes de " + ddlMes.SelectedItem.Text;
            else
                lblError.Text = "Seleccione un dia menor a 28 para el mes de " + ddlMes.SelectedItem.Text;
            lblError.Visible = true;
            ddlDia.Focus();
            return false;
        }
        */
        if (txtContrasena.Text == "")
        {
            lblError.Text = "Ingrese su contraseña, mínimo de 6 caracteres";
            lblError.Visible = true;
            return false;
        }
        if (txtConfirmaContra.Text == "")
        {
            lblError.Text = "Ingrese nuevamente su contraseña, mínimo de 6 caracteres";
            lblError.Visible = true;
            return false;
        }
        if (txtContrasena.Text.Length < 6 || txtConfirmaContra.Text.Length < 6)
        {
            lblError.Text = "Debe ingresar una constraseña mayor a 6 caracteres, verifique los datos.";
            lblError.Visible = true;
            return false;
        }
        if (txtContrasena.Text != txtConfirmaContra.Text)
        {
            lblError.Text = "Error. Las contraseñas ingresadas no coinsiden.";
            lblError.Visible = true;
            return false;
        }
        return true;
    }

    #endregion


    #region EVENTOS DE BOTONES

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.RemoveAll();
        Response.Redirect("~/Default.aspx");
    }


    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        lblError.Visible = false;
        try
        {
            Usuario pUsu = new Usuario();
            if (!ValidarIdentificacion())
            {
                return;
            }
            if (ValidarDatos())
            {
                lblPassword.Text = txtContrasena.Text;
                mtvPrincipal.ActiveViewIndex = 1;
                panelCreditos.Visible = false;
                panelOtros.Visible = false;

                //CARGA DE RADIOBUTTONLIST APORTES Y CREDITO
                //CONSULTANDO SI TUVIERA CREDITOS ACTIVOS
                /*
                Xpinn.Asesores.Entities.Producto producto = new Xpinn.Asesores.Entities.Producto();
                producto.Persona.IdPersona = Convert.ToInt64(txtCodPersona.Text);
                List<Xpinn.Asesores.Entities.Producto> lstConsulta = new List<Xpinn.Asesores.Entities.Producto>();
                Xpinn.Asesores.Services.EstadoCuentaService serviceEstadoCuenta = new Xpinn.Asesores.Services.EstadoCuentaService();
                String FiltroEstados = " 'DESEMBOLSADO','SOLICITADO','VERIFICAR REF.','APROBADO','GENERADO'";
                String FiltroFinal = " (estado Like 'ATRASADO%' Or estado = 'ESTA AL DIA' Or estado In (" + FiltroEstados + "))";
                lstConsulta = serviceEstadoCuenta.ListarProductosPorEstados(producto, pUsu, FiltroFinal);

                Int64 RangIni = 0;
                Int64 Rang = 0;
                if (lstConsulta.Count > 0)
                {
                    var lstProductos = (from p in lstConsulta
                                        orderby p.FechaSolicitud descending
                                        select new
                                        {
                                            p.CodRadicacion
                                        }).ToList();
                    RangIni = Convert.ToInt64(lstProductos[0].CodRadicacion) + 1;
                    Rang = Convert.ToInt64(lstProductos[0].CodRadicacion) + 100;
                    Session["VALIDAR"] = lstProductos[0].CodRadicacion;
                    CompletarCheckBoxList(RangIni, Rang, lstProductos[0].CodRadicacion,rbPreguntas);
                    mtvPrincipal.ActiveViewIndex = 1;
                }
                else
                {
                    RangIni = 29000000;
                    Rang = 294956999;
                    Session["VALIDAR"] = "Ninguna de las Anteriores";
                    CompletarCheckBoxList(RangIni, Rang, null, rbPreguntas);
                }

                //LISTADO DE APORTES
                Xpinn.Aportes.Services.AporteServices AporteServicio = new Xpinn.Aportes.Services.AporteServices();
                List<Xpinn.Aportes.Entities.Aporte> lstAporte = new List<Xpinn.Aportes.Entities.Aporte>();

                lstAporte = AporteServicio.ListarEstadoCuentaAporte(Convert.ToInt64(txtCodPersona.Text), 1, DateTime.Now, pUsu);
                
                RangIni = 0;
                Rang = 0;
                if (lstAporte.Count > 0)
                {
                    var lstProdAporte = (from p in lstAporte
                                        orderby p.fecha_apertura descending
                                        select new
                                        {
                                            p.numero_aporte
                                        }).ToList();
                    RangIni = Convert.ToInt64(lstProdAporte[0].numero_aporte) + 1;
                    Rang = Convert.ToInt64(lstProdAporte[0].numero_aporte) + 100;
                    Session["APORTE"] = lstProdAporte[0].numero_aporte;
                    CompletarCheckBoxList(RangIni, Rang, lstProdAporte[0].numero_aporte.ToString(), rbAportes);
                }
                else
                {
                    RangIni = 29000000;
                    Rang = 294956999;
                    Session["APORTE"] = "Ninguna de las Anteriores";
                    CompletarCheckBoxList(RangIni, Rang, null, rbAportes);
                }

                */
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            lblError.Visible = false;
            if (txtCodPersona.Text == "")
            {
                lblError.Text = "Se genero un error, por seguridad vuelva a ingresar y realizar su registro.";
                lblError.Visible = true;
                return;
            }
            Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
            e.IsValid = Captcha1.UserValidated;
            if (e.IsValid)
            {
                lblError.Text = "";
                lblError.Visible = false;
                if (RealizarEnvioCorreo())
                {
                    string pExtrae = string.Empty;
                    string[] pEmailText = lblEmailCooperativa.Text.Split('@');
                    pExtrae = pEmailText[0].Substring(0, 2).Trim();
                    lblContenido.Text = " Su correo encontrado en la entidad es el " + pExtrae + "***********@" + pEmailText[1];
                    mtvPrincipal.ActiveViewIndex = 2;
                }
                else
                {
                    lblError.Text = "Se genero un error, por seguridad vuelva a ingresar y realizar su registro.";
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Se genero un error, por seguridad vuelva a ingresar y realizar su registro. Si el problema persiste verifique su correo con la entidad";
        }
    }

    protected void btnConfirmar_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = string.Empty;
            lblError.Visible = false;
            if (string.IsNullOrEmpty(lblAleatorio.Text))
            {
                lblError.Text = "Ocurrio un error al generar el registro";
                lblError.Visible = true;
                return;
            }
            if (string.IsNullOrEmpty(txtCodigoConf.Text))
            {
                lblError.Text = "Ingrese el código de confirmación.";
                lblError.Visible = true;
                return;
            }
            if (lblAleatorio.Text.Trim() == txtCodigoConf.Text)
            {
                if (Grabar())
                    mtvPrincipal.ActiveViewIndex = 3;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion


    #region METODOS EN GENERAL

    protected void CompletarCheckBoxList(Int64 RangIni, Int64 Rang, string pValor, RadioButtonList pControl)
    {
        Random rand = new Random();
        int aleatorio = rand.Next(0, 3);
        int cont = 0;
        for (int i = 0; i < 5; i++)
        {
            if (aleatorio == cont && pValor != null)
                pControl.Items.Insert(i, new ListItem(pValor, i.ToString()));
            else if (cont == 4)
                pControl.Items.Insert(i, new ListItem("Ninguna de las Anteriores", i.ToString()));
            else
            {
                //Convirtiendo a positivo si nos manda un valor negativo
                Int64 conc = rand.Next((int)RangIni, (int)Rang);
                if (conc < 0)
                    conc = conc * -1;
                //cambiando la variable random hasta que sea diferente al radicado seleccionado
                if (pValor != null)
                {
                    while (conc == Convert.ToInt64(pValor))
                    {
                        conc = rand.Next((int)RangIni, (int)Rang);
                    }
                }

                //Ajustando si tiene longitud menor a la del num de radicacion.
                string caracter = "";
                if (pValor != null)
                {
                    if (conc.ToString().Length < pValor.Length)
                    {
                        caracter = pValor.Substring(0, Convert.ToInt32(pValor.Length - conc.ToString().Length));
                    }
                }
                pControl.Items.Insert(i, new ListItem(caracter + conc.ToString(), i.ToString()));
            }
            cont++;
        }
    }


    protected bool RealizarEnvioCorreo()
    {
        lblError.Text = string.Empty;
        lblError.Visible = false;
        Random rand = new Random();
        int aleatorio = rand.Next(100000, 999999);
        // ALMACENO EL CODIGO ALEATORIO PARA VALIDAR EN EL SIGUIENTE PROCESO
        lblAleatorio.Text = aleatorio.ToString();

        if (string.IsNullOrEmpty(lblEmailCooperativa.Text))
        {
            lblError.Text = "No se encontró el email de la persona.";
            lblError.Visible = true;
            return false;
        }

        try
        {
            string Nombre = txtNombres.Text.Trim() + ", " + txtApellidos.Text.Trim();

            //string[] pObject = { lblEmailCooperativa.Text.Trim(), Nombre, lblAleatorio.Text };
            xpinnWSAppFinancial.ArrayOfString pObject = new xpinnWSAppFinancial.ArrayOfString { lblEmailCooperativa.Text.Trim(), Nombre, lblAleatorio.Text };
            bool result = ServicesAppFinancial.EnvioCodigoActivacion(pObject, xpinnWSAppFinancial.ProcesoAtencionCliente.RegistroAsociado);
            //string salida = ServicesAppFinancial.TestMetod(pObject, xpinnWSAppFinancial.ProcesoAtencionCliente.RegistroAsociado);
            if (!result)
            {
                lblError.Text = "Se generó un error al enviarle el correo de confirmación.";
                lblError.Visible = true;
            }

            return result;
        }
        catch (Exception)
        {
            lblError.Text = "Se generó un error al enviarle el correo de confirmación.";
            lblError.Visible = true;
            return false;
        }
    }


    protected bool Grabar()
    {
        lblError.Text = "";
        lblError.Visible = false;
        try
        {
            Int64 pCodPersona = 0;
            string pClave = "", pNombres = "", pApellidos = "", pEmail = "", pFechaNac = "";
            pCodPersona = Convert.ToInt64(txtCodPersona.Text);
            pClave = lblPassword.Text;
            pNombres = txtNombres.Text.Trim();
            pApellidos = txtApellidos.Text.Trim();
            pEmail = lblEmailCooperativa.Text;
            /*int dia = 0, mes = 0, anio = 0;
            dia = Convert.ToInt32(ddlDia.SelectedValue);
            mes = Convert.ToInt32(ddlMes.SelectedValue);
            anio = Convert.ToInt32(ddlAnio.SelectedValue);*/
            //pFechaNac = dia.ToString("00") + "/" + mes.ToString("00") + "/" + anio;
            pFechaNac = txtFecNacimiento.Text;

            xpinnWSEstadoCuenta.PersonaUsuario vDatos = new xpinnWSEstadoCuenta.PersonaUsuario();

            // Validar si existe el usuario a crear
            // ASIGNACION DE ESTADO DIRECTAMENTE EN EL PROCEDIMIENTO ALMANCENADO.

            vDatos = ServicioEstCta.CrearUsuarioPerAPP(pCodPersona, pClave, pNombres, pApellidos, pEmail, pFechaNac);
            if (vDatos.rpta == true)
                lblIdentificacion.Text = txtIdentificacion.Text;
            else
            {
                if (vDatos.mensaje != null)
                {
                    lblError.Text = vDatos.mensaje;
                    lblError.Visible = true;
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
            return false;
        }
    }

    #endregion


    protected void btnGrabar_Click(object sender, EventArgs e)
    {
        
    }


    protected bool buildSecure()
    {
        string key = ConfigurationManager.AppSettings["key"].ToString();
        string usr = ConfigurationManager.AppSettings["usr"].ToString();
        string ip = "";
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress address in ipHostInfo.AddressList)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
                ip = address.ToString();
        }
        if (!string.IsNullOrEmpty(key) || !string.IsNullOrEmpty(usr) || !string.IsNullOrEmpty(ip))
        {
            CifradoBusiness cifrar = new CifradoBusiness();
            string sec = ip + ";" + usr + ";" + key;
            sec = cifrar.Encriptar(sec);
            Session["sec"] = sec;
        }
        return true;
    }
}