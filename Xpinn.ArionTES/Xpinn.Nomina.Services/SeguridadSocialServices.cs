using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Business;
using System.ServiceModel;

namespace Xpinn.Nomina.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]


    public class SeguridadSocialServices
    {
        private SeguridadSocialBusiness BOSeguridadSocial;
        private ExcepcionBusiness BOExcepcion;

        public SeguridadSocialServices()
        {
            BOSeguridadSocial = new SeguridadSocialBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250610"; } }

        public SeguridadSocial CrearSeguridadSocial (SeguridadSocial pSeguridadSocial,Usuario vUsuario)
        {
            try
            {
                pSeguridadSocial = BOSeguridadSocial.CrearSeguridadSocial(pSeguridadSocial, vUsuario);
                return pSeguridadSocial;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguridadSocialService", "CrearSeguridadSocial", ex);
                return null;
            }
        }
        public SeguridadSocial ConsultarSeguridadSocial(Usuario vUsuario)
        {
            try
            {
                SeguridadSocial SeguridadSocial = BOSeguridadSocial.ConsultarSeguridadSocial(vUsuario);
                return SeguridadSocial;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguridadSocialService", "ConsultarSeguridadSocial", ex);
                return null;
            }
        }
        public SeguridadSocial ModificarSeguridadSocial(SeguridadSocial pSeguridadSocial, Usuario vUsuario)
        {
            try
            {
                pSeguridadSocial = BOSeguridadSocial.ModificarSeguridadSocial(pSeguridadSocial, vUsuario);
                return pSeguridadSocial;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguridadSocialService", "CrearSeguridadSocial", ex);
                return null;
            }
        }
    }
}
