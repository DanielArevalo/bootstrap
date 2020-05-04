using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Business;

namespace Xpinn.Comun.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Formato_NotificacionService
    {

        private Formato_NotificacionBusiness BOformatoNotificacion;
        private ExcepcionBusiness BOExcepcion;

        public Formato_NotificacionService()
        {
            BOformatoNotificacion = new Formato_NotificacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public bool SendEmailPerson(Formato_Notificacion noti, Usuario usuario)
        {
            try
            {
                return BOformatoNotificacion.SendEmailPerson(noti, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Formato_NotificacionService", "SendEmailPerson", ex);
                return false;
            }
        }

        public Formato_Notificacion ConsultarDatosEnvio(Formato_Notificacion noti, Usuario usuario)
        {
            try
            {
                return BOformatoNotificacion.ConsultarDatosEnvio(noti, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Formato_NotificacionService", "ConsultarDatosEnvio", ex);
                return null;
            }
        }

        public bool SendEmailExtracto(Formato_Notificacion noti, Usuario usuario)
        {
            try
            {
                return BOformatoNotificacion.SendEmailExtracto(noti, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Formato_NotificacionService", "SendEmailExtracto", ex);
                return false;
            }
        }
    }

}