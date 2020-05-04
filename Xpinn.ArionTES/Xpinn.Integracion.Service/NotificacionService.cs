using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Integracion.Business;
using Xpinn.Integracion.Entities;
using Xpinn.Util;
using System.Data;
using System.ServiceModel;

namespace Xpinn.Integracion.Services
{
    public class NotificacionService
    {

        private NotificacionBusiness BONotifica;
        private ExcepcionBusiness BOExcepcion;

        public NotificacionService()
        {
            BONotifica = new NotificacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public Notificacion enviarClaveDinamica(Notificacion noti, Usuario usuario)
        {
            try
            {
                return BONotifica.enviarClaveDinamica(noti, usuario);
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
                return BONotifica.enviarNotificaciones(noti, pUsuario);
            }
            catch (Exception ex)
            {
                noti.error += ex.Message;
                return noti;
            }
        }
    }
}