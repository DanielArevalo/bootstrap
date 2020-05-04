using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
using Xpinn.Integracion.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json;
//using RestSharp;

namespace Xpinn.Integracion.Business
{
    public class NotificacionBusiness : GlobalData
    {
        private NotificacionData BONotificacionData;
        private IntegracionData BOIntegracionData;

        public NotificacionBusiness()
        {
            BONotificacionData = new NotificacionData();
            BOIntegracionData = new IntegracionData();
        }

        public Notificacion enviarClaveDinamica(Notificacion noti, Usuario usuario)
        {
            try
            {
                noti.cod_email = 19;
                noti.cod_mensaje = 20;
                noti = BONotificacionData.ObtenerFormatoAEnviar(noti, usuario);
                noti = BONotificacionData.ConsultarRemitente(noti, usuario);
                noti.descripcion = "Clave dinámica";
                //generar clave dinamica
                Random x = new Random();
                noti.clave_dinamica = x.Next(1000, 9999);
                noti.correo_parametros = "claveDinamica;" + noti.clave_dinamica;
                noti.mensaje_parametros = "claveDinamica;" + noti.clave_dinamica;
                //envía mensaje y email
                if (noti.enviar_mensaje && !string.IsNullOrEmpty(noti.mensaje_texto) && !string.IsNullOrEmpty(noti.celular))
                    noti.enviar_mensaje = EnviarMensaje(noti, usuario);
                else
                    noti.enviar_mensaje = false;
                if (noti.enviar_email && !string.IsNullOrEmpty(noti.correo_texto) && !string.IsNullOrEmpty(noti.email))
                    noti.enviar_email = EnviarEmail(noti, usuario);
                else
                    noti.enviar_email = false;
                BONotificacionData.guardarNotificacion(noti, usuario);
                return noti;
            }
            catch (Exception ex)
            {
                noti.error += ex.Message;
                return noti;
            }
        }

        public Notificacion enviarNotificaciones(Notificacion noti, Usuario pUsuario)
        {            
            try
            {
                noti = BONotificacionData.ObtenerFormatoAEnviar(noti, pUsuario);
                noti = BONotificacionData.ConsultarRemitente(noti, pUsuario);
                if (noti.enviar_mensaje && !string.IsNullOrEmpty(noti.mensaje_texto) && !string.IsNullOrEmpty(noti.celular))
                    noti.enviar_mensaje = EnviarMensaje(noti, pUsuario);
                if (noti.enviar_email && !string.IsNullOrEmpty(noti.correo_texto) && !string.IsNullOrEmpty(noti.email))
                    noti.enviar_email = EnviarEmail(noti,pUsuario);
                BONotificacionData.guardarNotificacion(noti, pUsuario);
                return noti;
            }
            catch (Exception ex)
            {
                noti.error += ex.Message;
                return noti;
            }
        }

        private bool EnviarEmail(Notificacion noti, Usuario pUsuario)
        {
            //Reemplaza datos del mensaje de correo 
            if (!string.IsNullOrEmpty(noti.correo_parametros))
            {
                //Reemplaza valores de parametros
                string[] valores = noti.correo_parametros.Split('|');
                if (valores.Length > 0)
                {
                    for (int i = 0; i < valores.Length; i++)
                    {
                        string[] paramet = valores[i].Split(';');
                        noti.correo_texto = noti.correo_texto.Replace(paramet[0], paramet[1]);
                    }
                }
            }

            noti = BONotificacionData.ConsultarEnviador(noti, pUsuario);
            if (!string.IsNullOrEmpty(noti.enviador_email) && !string.IsNullOrEmpty(noti.enviador_clave))
            {
                Util.CorreoHelper helper = new CorreoHelper(noti.email, noti.enviador_email, noti.enviador_clave);
                return helper.sendEmail(noti.correo_texto, noti.descripcion);
            }
            return false;
        }

        public bool EnviarMensaje(Notificacion noti, Usuario pUsuario)
        {
            try
            {
                //Reemplaza datos del mensaje de correo 
                if (!string.IsNullOrEmpty(noti.mensaje_parametros))
                {
                    //Reemplaza valores de parametros
                    string[] valores = noti.mensaje_parametros.Split('|');
                    if (valores.Length > 0)
                    {
                        for (int i = 0; i < valores.Length; i++)
                        {
                            string[] paramet = valores[i].Split(';');
                            noti.mensaje_texto = noti.mensaje_texto.Replace(paramet[0], paramet[1]);
                        }
                    }
                }

                //Obtener datos de envío integracion
                Entities.Integracion inte = BOIntegracionData.ObtenerIntegracion(3, pUsuario);
                if(inte != null && !string.IsNullOrEmpty(inte.usuario) && !string.IsNullOrEmpty(inte.password))
                {
                    ////var client = new RestClient("https://www.onurix.com/api/v1/send-sms");
                    ////var request = new RestRequest(Method.POST);
                    //request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    //request.AddParameter("key", inte.password);
                    //request.AddParameter("client", inte.usuario);
                    //string phone = noti.celular;
                    //request.AddParameter("phone", phone);                    
                    //request.AddParameter("sms", noti.mensaje_texto);
                    //request.AddParameter("country-code", "CO");
                    //IRestResponse response = client.Execute(request);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

                
    }
}


