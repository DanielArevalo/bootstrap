using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Seguridad.Business;
using Xpinn.Seguridad.Entities;
using Xpinn.Util;

namespace Xpinn.Seguridad.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AuditoriaStoredProceduresService : IAuditableServices
    {
        private AuditoriaStoredProceduresBusiness BOAuditoriaStoredProcedures;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "90115"; }  }

        public AuditoriaStoredProceduresService()
        {
            BOAuditoriaStoredProcedures = new AuditoriaStoredProceduresBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public AuditoriaStoredProcedures CrearAuditoriaStoredProcedures(AuditoriaStoredProcedures pAuditoriaStoredProcedures, Usuario pusuario)
        {
            try
            {
                pAuditoriaStoredProcedures = BOAuditoriaStoredProcedures.CrearAuditoriaStoredProcedures(pAuditoriaStoredProcedures, pusuario);
                return pAuditoriaStoredProcedures;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaStoredProceduresService", "CrearAuditoriaStoredProcedures", ex);
                return null;
            }
        }


        public AuditoriaStoredProcedures ConsultarAuditoriaStoredProcedures(Int64 pId, Usuario pusuario)
        {
            try
            {
                AuditoriaStoredProcedures AuditoriaStoredProcedures = new AuditoriaStoredProcedures();
                AuditoriaStoredProcedures = BOAuditoriaStoredProcedures.ConsultarAuditoriaStoredProcedures(pId, pusuario);
                return AuditoriaStoredProcedures;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaStoredProceduresService", "ConsultarAuditoriaStoredProcedures", ex);
                return null;
            }
        }

        public List<AuditoriaStoredProcedures> ListarAuditoriaStoredProcedures(string filtro, Usuario pusuario)
        {
            try
            {
                return BOAuditoriaStoredProcedures.ListarAuditoriaStoredProcedures(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaStoredProceduresService", "ListarAuditoriaStoredProcedures", ex);
                return null;
            }
        }

        public List<string> ListarProcedimientos(string prefix, Usuario usuario)
        {
            try
            {
                return BOAuditoriaStoredProcedures.ListarProcedimientos(prefix, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaStoredProceduresService", "ListarProcedimientos", ex);
                return null;
            }
        }
    }
}