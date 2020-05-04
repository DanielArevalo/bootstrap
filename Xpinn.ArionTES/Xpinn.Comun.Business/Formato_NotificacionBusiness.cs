using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;

namespace Xpinn.Comun.Business
{

    public class Formato_NotificacionBusiness : GlobalBusiness
    {

        private Formato_NotificacionData DAFormatoNotificacion;

        public Formato_NotificacionBusiness()
        {
            DAFormatoNotificacion = new Formato_NotificacionData();
        }

        public bool SendEmailPerson(Formato_Notificacion noti, Usuario usuario)
        {
            try
            {
                bool salida = false;
                //Obtener enviador                 
                noti = DAFormatoNotificacion.ConsultarEnviador(noti, usuario);
                if (noti.enviador != null && noti.claveEnviador != null)
                {
                    //Obtiene destinatario
                    noti = DAFormatoNotificacion.ConsultarRemitente(noti, usuario);
                    if (noti.emailReceptor != null)
                    {
                        //Obtiene texto a enviar
                        noti = DAFormatoNotificacion.ObtenerFormatoAEnviar(noti, usuario);
                        if (noti.texto != null)
                        {
                            //Reemplaza el nombre de la persona
                            noti.texto = noti.texto.Replace("NombreCompletoPersona", noti.receptor);

                            if (!string.IsNullOrEmpty(noti.parametros) && !string.IsNullOrEmpty(noti.parametros_reemp))
                            {
                                //Reemplaza valores de parametros
                                string[] valores = noti.parametros_reemp.Split('|');                                
                                if (valores.Length > 0)
                                {
                                    for (int i = 0; i < valores.Length; i++)
                                    {
                                        string[] paramet = valores[i].Split(';');
                                        noti.texto = noti.texto.Replace(paramet[0], paramet[1]);
                                    }
                                }
                            }
                            CorreoHelper email = new CorreoHelper(noti.emailReceptor, noti.enviador, noti.claveEnviador);
                            string error = "";
                            if (noti.adjunto != null)
                                email.sendEmail(noti.adjunto, noti.texto, out error, noti.nombre);
                            else
                                email.sendEmail(noti.texto, noti.nombre);

                            if (!string.IsNullOrEmpty(error))
                                noti.mensaje = error;

                        }
                        else
                        {
                            noti.mensaje = "No se encontró formato para el envío de correo";
                        }
                    }
                    else
                    {
                        noti.mensaje = "No se encontró el correo del receptor";
                    }
                }
                {
                    noti.mensaje = "Correo o clave del correo de envios no registrados";
                }
                return salida;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Formato_NotificacionBusiness", "SendEmailPerson", ex);
                return false;
            }
        }

        public bool SendEmailExtracto(Formato_Notificacion noti, Usuario usuario)
        {
            try
            {
                bool salida = false;
                if (!string.IsNullOrEmpty(noti.parametros) && !string.IsNullOrEmpty(noti.parametros_reemp))
                {
                    //Reemplaza valores de parametros
                    string[] valores = noti.parametros_reemp.Split('|');
                    if (valores.Length > 0)
                    {
                        for (int i = 0; i < valores.Length; i++)
                        {
                            string[] paramet = valores[i].Split(';');
                            noti.texto = noti.texto.Replace(paramet[0], paramet[1]);
                        }
                    }
                }
                CorreoHelper email = new CorreoHelper(noti.emailReceptor, noti.enviador, noti.claveEnviador);
                string error = "";
                if (noti.adjunto != null)
                    email.sendEmail(noti.adjunto, noti.texto, out error, noti.nombre);
                else
                    email.sendEmail(noti.texto, noti.nombre);

                if (!string.IsNullOrEmpty(error))
                {
                    noti.mensaje = error;
                }
                else
                {
                    salida = true;
                    DAFormatoNotificacion.AlmacenarHistorialEnvio(noti, usuario);
                }

                return salida;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Formato_NotificacionBusiness", "SendEmailExtracto", ex);
                return false;
            }
        }

        public Formato_Notificacion ConsultarDatosEnvio(Formato_Notificacion noti, Usuario usuario)
        {
            try
            {
                //Obtener enviador                 
                noti = DAFormatoNotificacion.ConsultarEnviador(noti, usuario);
                if (noti.enviador != null && noti.claveEnviador != null)
                {
                    //Obtiene texto a enviar
                    noti = DAFormatoNotificacion.ObtenerFormatoAEnviar(noti, usuario);
                    if (noti.texto != null)
                    {
                        return noti;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Formato_NotificacionBusiness", "SendEmailPerson", ex);
                return null;
            }
        }
    }
}