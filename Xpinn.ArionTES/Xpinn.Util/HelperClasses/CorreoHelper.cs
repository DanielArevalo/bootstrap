using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;

namespace Xpinn.Util
{
    public enum Correo
    {
        Gmail,
        Hotmail,
        Yahoo,
        Zoho
    }

    public class CorreoHelper
    {
        public string _correoDestinatario { get; set; }
        string _correoEnviador { get; set; }
        string _claveCorreoEnviador { get; set; }

        //Server  Name SMTP Address    Port  SSL
        //Yahoo!  smtp.mail.yahoo.com  587	 Yes
        //GMail   smtp.gmail.com	   587	 Yes
        //Hotmail smtp.live.com	       587	 Yes
        const int _puertoCorreo = 587;
        const bool useSSL = true;
        const bool useDefaultCredentials = true;

        Dictionary<Correo, string> _servidorCorreoDiccionario = new Dictionary<Correo, string>()
        {
            { Correo.Gmail, "smtp.gmail.com" },
            { Correo.Hotmail, "smtp.live.com" },
            { Correo.Yahoo, "smtp.mail.yahoo.com" },
            { Correo.Zoho, "smtp.zoho.com" }
        };


        public CorreoHelper(string correoDestinatario, string correoEnviador, string claveEnviador)
        {
            _correoDestinatario = correoDestinatario.Trim();
            _claveCorreoEnviador = claveEnviador.Trim();
            _correoEnviador = correoEnviador.Trim();
        }


        // Envia un Control por Correo como HTML puro, siendo ideal para formularios en correo
        public bool EnviarHTMLDeControlPorCorreo(Control control, Correo servidorCorreo, string tema = " ", string nombreEnviador = " ")
        {
            try
            {
                string htmlPanelMensaje = CreateHtmlMensajeFromControl(control);

                using (MailMessage mailMessage = CrearMensaje(htmlPanelMensaje, tema, nombreEnviador, null, "", true))
                {
                    using (SmtpClient smtp = CrearSmtpClient(servidorCorreo))
                    {
                        smtp.Send(mailMessage);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Envia un Control por Correo como HTML puro, siendo ideal para formularios en correo con la diferencia que puedes enviar a un servidor que no este en la Enum
        public bool EnviarHTMLDeControlPorCorreo(Control control, string hosting, int puerto, string tema = " ", string nombreEnviador = " ")
        {
            try
            {
                hosting = hosting.Trim();
                string htmlPanelMensaje = CreateHtmlMensajeFromControl(control);

                using (MailMessage mailMessage = CrearMensaje(htmlPanelMensaje, tema, nombreEnviador, null, "", true))
                {
                    using (SmtpClient smtp = CrearSmtpClient(hosting, puerto))
                    {
                        smtp.Send(mailMessage);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        // Envia Correo como texto puro
        public bool EnviarCorreoSinHTML(string mensaje, Correo servidorCorreo, string tema = " ", string nombreEnviador = " ")
        {
            try
            {
                using (MailMessage mailMessage = CrearMensaje(mensaje, tema, nombreEnviador, null, ""))
                {
                    using (SmtpClient smtp = CrearSmtpClient(servidorCorreo))
                    {
                        smtp.Send(mailMessage);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Enviar correo con archivo adjunto
        public bool EnviarCorreoArchivoAdjunto(string mensaje, Correo servidorCorreo, byte[] bytes, string NomArchivo, string tema = " ", string nombreEnviador = " ")
        {
            try
            {
                using (MailMessage mailMessage = CrearMensaje(mensaje, tema, nombreEnviador, bytes, NomArchivo, true))
                {
                    using (SmtpClient smtp = CrearSmtpClient(servidorCorreo))
                    {
                        smtp.Send(mailMessage);
                        return true;
                    }
                }
            }
            catch //(Exception ex)
            {
                return false;
            }
        }


        // Envia Correo como HTML con la diferencia que puedes enviar a un servidor que no este en la Enum
        public bool EnviarCorreoConHTML(string mensaje, string hosting, int puerto, string tema = " ", string nombreEnviador = " ")
        {
            try
            {
                hosting = hosting.Trim();
                using (MailMessage mailMessage = CrearMensaje(mensaje, tema, nombreEnviador, null, "", true))
                {
                    using (SmtpClient smtp = CrearSmtpClient(hosting, puerto))
                    {
                        smtp.Send(mailMessage);
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }


        // Envia Correo como HTML
        public bool EnviarCorreoConHTML(string mensaje, Correo servidorCorreo, string tema = " ", string nombreEnviador = " ")
        {
            try
            {
                using (MailMessage mailMessage = CrearMensaje(mensaje, tema, nombreEnviador, null, "", true))
                {
                    using (SmtpClient smtp = CrearSmtpClient(servidorCorreo))
                    {
                        smtp.Send(mailMessage);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        string CreateHtmlMensajeFromControl(Control control)
        {
            Page page = new Page();
            HtmlForm form = new HtmlForm();
            ControlsHelper controlHelper = new ControlsHelper();
            string htmlPanelMensaje = string.Empty;

            page.EnableEventValidation = false;
            control.Visible = true;
            page.Controls.Add(form);
            form.Attributes.Add("runat", "server");
            form.Controls.Add(control);
            page.DesignerInitialize();

            htmlPanelMensaje = controlHelper.RenderPageAsStringHTML(page);
            return htmlPanelMensaje;
        }

        //Se agregó variable archivo para poder adjuntar documentos en el correo
        MailMessage CrearMensaje(string mensaje, string tema, string nombreEnviador, byte[] archivo, string NomArchivo, bool isBodyHtml = false)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(_correoDestinatario);
            mailMessage.From = new MailAddress(_correoEnviador, nombreEnviador);
            mailMessage.Subject = tema;
            mailMessage.Body = mensaje;
            mailMessage.IsBodyHtml = isBodyHtml;
            if (archivo != null)
                mailMessage.Attachments.Add(new Attachment(new MemoryStream(archivo), NomArchivo));

            return mailMessage;
        }


        SmtpClient CrearSmtpClient(Correo servidorCorreo)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = _servidorCorreoDiccionario[servidorCorreo];
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential(_correoEnviador, _claveCorreoEnviador);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = _puertoCorreo;

            return smtp;
        }


        SmtpClient CrearSmtpClient(string host, int puerto)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = host.Trim();
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential(_correoEnviador, _claveCorreoEnviador);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = puerto;

            return smtp;
        }

        public bool sendEmail(string mensaje, string tema = "", string copia = "", string copia2 = "")
        {
            try
            {
                //Creamos un nuevo Objeto de mensaje
                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Direccion de correo electronico a la que queremos enviar el mensaje
                mmsg.To.Add(_correoDestinatario);

                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = tema;
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                //Direccion de correo electronico que queremos que reciba una copia del mensaje
                if (!string.IsNullOrWhiteSpace(copia))
                    mmsg.Bcc.Add(copia); //Opcional
                if (!string.IsNullOrWhiteSpace(copia2))
                    mmsg.Bcc.Add(copia2); //Opcional

                //Cuerpo del Mensaje
                mmsg.Body = mensaje;
                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

                //Correo electronico desde la que enviamos el mensaje
                mmsg.From = new System.Net.Mail.MailAddress(_correoEnviador);


                /*-------------------------CLIENTE DE CORREO----------------------*/

                //Creamos un objeto de cliente de correo
                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

                //Hay que crear las credenciales del correo emisor
                cliente.Credentials =
                    new System.Net.NetworkCredential(_correoEnviador, _claveCorreoEnviador);

                //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail                
                cliente.Port = _puertoCorreo;
                cliente.EnableSsl = useSSL;
                //cliente.Host = "cp-14.webhostbox.net";
                cliente.Host = _servidorCorreoDiccionario[0];
                //_servidorCorreoDiccionario[0];//Para Gmail "smtp.gmail.com";

                /*-------------------------ENVIO DE CORREO----------------------*/
                try
                {
                    //Enviamos el mensaje      
                    cliente.Send(mmsg);
                    return true;
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    return false;
                }
                ///
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool sendEmail(string mensaje, out string error, string tema = "", string copia = "", string copia2 = "")
        {
            try
            {
                //Creamos un nuevo Objeto de mensaje
                System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                //Direccion de correo electronico a la que queremos enviar el mensaje
                mmsg.To.Add(_correoDestinatario);

                //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                //Asunto
                mmsg.Subject = tema;
                mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                //Direccion de correo electronico que queremos que reciba una copia del mensaje
                if (!string.IsNullOrWhiteSpace(copia))
                    mmsg.Bcc.Add(copia); //Opcional
                if (!string.IsNullOrWhiteSpace(copia2))
                    mmsg.Bcc.Add(copia2); //Opcional

                //Cuerpo del Mensaje
                mmsg.Body = mensaje;
                mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

                //Correo electronico desde la que enviamos el mensaje
                mmsg.From = new System.Net.Mail.MailAddress(_correoEnviador);


                /*-------------------------CLIENTE DE CORREO----------------------*/

                //Creamos un objeto de cliente de correo
                System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

                //Hay que crear las credenciales del correo emisor
                cliente.Credentials =
                    new System.Net.NetworkCredential(_correoEnviador, _claveCorreoEnviador);

                //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail                
                cliente.Port = _puertoCorreo;
                cliente.EnableSsl = useSSL;
                //cliente.Host = "cp-14.webhostbox.net";
                cliente.Host = _servidorCorreoDiccionario[0];
                //_servidorCorreoDiccionario[0];//Para Gmail "smtp.gmail.com";

                /*-------------------------ENVIO DE CORREO----------------------*/
                try
                {
                    //Enviamos el mensaje      
                    cliente.Send(mmsg);
                    error = "";
                    return true;
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    error = "envío : " + ex.Message + " ------ " + ex.Source + " ------ " + ex.StackTrace + " ------ " + ex.HelpLink;
                    return false;
                }
                ///
            }
            catch (Exception e)
            {
                error = "global : " + e.Message + " ------ " + e.Source + " ------ " + e.StackTrace + " ------ " + e.HelpLink;
                return false;
            }
        }

        public bool sendEmail(byte[] file, string mensaje, out string error, string tema = "", string copia = "", string copia2 = "")
        {
            using (var stream = new MemoryStream(file))
            {

                try
                {
                    //Creamos un nuevo Objeto de mensaje
                    System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

                    //Direccion de correo electronico a la que queremos enviar el mensaje
                    mmsg.To.Add(_correoDestinatario);

                    //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

                    //Asunto
                    mmsg.Subject = tema;
                    mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

                    //Direccion de correo electronico que queremos que reciba una copia del mensaje
                    if (!string.IsNullOrWhiteSpace(copia))
                        mmsg.Bcc.Add(copia); //Opcional
                    if (!string.IsNullOrWhiteSpace(copia2))
                        mmsg.Bcc.Add(copia2); //Opcional

                    //Cuerpo del Mensaje
                    mmsg.Body = mensaje;
                    mmsg.BodyEncoding = System.Text.Encoding.UTF8;
                    mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML

                    //Correo electronico desde la que enviamos el mensaje
                    mmsg.From = new System.Net.Mail.MailAddress(_correoEnviador);

                    Attachment attach = new Attachment(stream, new System.Net.Mime.ContentType("application/pdf"));
                    attach.ContentDisposition.FileName = "EstadoCuenta.pdf";

                    mmsg.Attachments.Add(attach);

                    /*-------------------------CLIENTE DE CORREO----------------------*/

                    //Creamos un objeto de cliente de correo
                    System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

                    //Hay que crear las credenciales del correo emisor
                    cliente.Credentials =
                        new System.Net.NetworkCredential(_correoEnviador, _claveCorreoEnviador);

                    //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail                
                    cliente.Port = _puertoCorreo;
                    cliente.EnableSsl = useSSL;
                    //cliente.Host = "cp-14.webhostbox.net";
                    cliente.Host = _servidorCorreoDiccionario[0];
                    //_servidorCorreoDiccionario[0];//Para Gmail "smtp.gmail.com";

                    /*-------------------------ENVIO DE CORREO----------------------*/
                    try
                    {
                        //Enviamos el mensaje      
                        cliente.Send(mmsg);
                        error = "";
                        return true;
                    }
                    catch (System.Net.Mail.SmtpException ex)
                    {
                        error = "envío : " + ex.Message + " ------ " + ex.Source + " ------ " + ex.StackTrace + " ------ " +
                                ex.HelpLink;
                        return false;
                    }
                    ///
                }
                catch (Exception e)
                {
                    error = "global : " + e.Message + " ------ " + e.Source + " ------ " + e.StackTrace + " ------ " +
                            e.HelpLink;
                    return false;
                }
            }
        }



    }
}
