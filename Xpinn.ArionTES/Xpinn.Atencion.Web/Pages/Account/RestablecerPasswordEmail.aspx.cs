using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using Xpinn.Util;

public partial class RestablecerPasswordEmail : System.Web.UI.Page
{
    xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient EstadoCuenta = new xpinnWSEstadoCuenta.WSEstadoCuentaSoapClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            buildSecure();
            panelEnvio.Visible = false;
        }
    }    


    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        lblError.Visible = false;
        try
        {
            //realizar la busqueda del Email de la persona TABLA Persona_Acceso
            xpinnWSEstadoCuenta.PersonaUsuario pEntidad = new xpinnWSEstadoCuenta.PersonaUsuario();
            string pEmail = txtEmailRecuperacion.Text;
            string[] pEmailText;
            string pEmailEncontrado = "",pExtrae = "";

            pEntidad = EstadoCuenta.ConsultarPersonaUsuarioGeneral(pEmail, Session["sec"].ToString());
            if(pEntidad != null)
            {
                if (pEntidad.email != null && pEntidad.cod_persona != 0)
                {
                    pEmailEncontrado = pEntidad.email;
                    if (pEmailEncontrado.Contains("@"))
                    {
                        //Capturamos datos que nos serviran mas adelante
                        pEmailText = pEmailEncontrado.Trim().Split('@');
                        pExtrae = pEmailText[0].Substring(0, 2).Trim();
                        rbUbicacion.Text = "Enviar un enlace por correo electrónico a " + pExtrae + "***********@" + pEmailText[1];
                        rbUbicacion.Checked = true;
                        lblEmailEncontrado.Text = pExtrae + "***********@" + pEmailText[1];
                        lblNombre.Text = pEntidad.nombres != null ? pEntidad.nombres : "";
                        lblNombre.Text += pEntidad.apellidos != null ? " " + pEntidad.apellidos : "";
                        lblNombre.Text = lblNombre.Text.Trim();
                        lblCod_Persona.Text = pEntidad.cod_persona.ToString().Trim();
                        mvPrincipal.ActiveViewIndex = 1;
                    }
                    else
                    {
                        lblError.Text = "Se produjo un error al obtener el resultado.";
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Text = "Correo no encontrado, intente con uno diferente o comuniquese con la entidad par verificar el correo registrado.";
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Text = "Correo no encontrado o persona no registrada, intente con uno diferente o comuniquese con la entidad par verificar el correo registrado.";
                lblError.Visible = true;
            }            
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
    }

    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        lblError.Visible = false;
        try
        {
            RealizarEnvioCorreo();
            mvPrincipal.ActiveViewIndex = 2;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.Visible = true;
        }
    }


    protected void RealizarEnvioCorreo()
    {
        Random rand = new Random();
        
        MailMessage msj = new MailMessage();
        SmtpClient cli = new SmtpClient();

        string email = txtEmailRecuperacion.Text.Trim();
        string Nombre = lblNombre.Text;

        //Creando el diseño del envio
        Page page = new Page();
        page.EnableEventValidation = false;
        HtmlForm form = new HtmlForm();
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlTxtWriter = new HtmlTextWriter(stringWriter);

        panelEnvio.Visible = true;
        lblNomApe.Text = Nombre;
        string URLPublicacion = string.Empty;
        if (ConfigurationManager.AppSettings["URLProyecto"] != null)
        {
            URLPublicacion = ConfigurationManager.AppSettings["URLProyecto"].ToString();
        }
        
        hlEnvio.NavigateUrl =  URLPublicacion + "/Pages/Account/RestablecerPassword.aspx?cod_persona=" + lblCod_Persona.Text;

        page.Controls.Add(form);
        form.Attributes.Add("runat", "server");
        form.Controls.Add(panelEnvio);
        page.DesignerInitialize();
        page.RenderControl(htmlTxtWriter);
        string strHtml = stringWriter.ToString();
        HttpContext.Current.Response.Clear();
        
        string correoServer = "", clave = "";
        correoServer = ConfigurationManager.AppSettings["CorreoServidor"].ToString();
        clave = ConfigurationManager.AppSettings["Clave"].ToString();

        msj.From = new MailAddress(correoServer);
        msj.To.Add(email);
        msj.Subject = "Restablece tu Contraseña";
        msj.SubjectEncoding = System.Text.Encoding.UTF8;
        msj.Body = strHtml.ToString();
        msj.BodyEncoding = System.Text.Encoding.UTF8;
        msj.IsBodyHtml = true;

        //GMAIL
        //cli.Host = "smtp.gmail.com";
        cli.Host = ConfigurationManager.AppSettings["Hosting"].ToString();
        cli.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Puerto"].ToString());
        //HOTMAIL
        //cli.Host = "smtp.live.com";
        //cli.Port = 25;
        cli.Credentials = new System.Net.NetworkCredential(correoServer, clave);
        cli.EnableSsl = true;
        cli.Send(msj);
        panelEnvio.Visible = false;
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