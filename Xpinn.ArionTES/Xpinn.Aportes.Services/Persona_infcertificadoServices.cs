using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Business;

namespace Xpinn.Aportes.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Persona_infcertificadoServices
    {

        private Persona_infcertificadoBusiness BOPersona;
        private ExcepcionBusiness BOExcepcion;

        public Persona_infcertificadoServices()
        {
            BOPersona = new Persona_infcertificadoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<Int32> ListarAniosPersonaCertificado(Int64 pCodAsociado, Usuario pUsuario)
        {
            return BOPersona.ListarAniosPersonaCertificado(pCodAsociado, pUsuario);
        }

        public List<Persona_infcertificado> ListarInformacionCertificado(Persona_infcertificado pInfor, string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOPersona.ListarInformacionCertificado(pInfor, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona_infcertificadoServices", "ListarInformacionCertificado", ex);
                return null;
            }
        }

    }
}