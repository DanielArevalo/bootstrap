using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Seguridad.Data;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Business
{

    public class AuditoriaStoredProceduresBusiness : GlobalBusiness
    {

        private AuditoriaStoredProceduresData DAAuditoriaStoredProcedures;

        public AuditoriaStoredProceduresBusiness()
        {
            DAAuditoriaStoredProcedures = new AuditoriaStoredProceduresData();
        }

        public AuditoriaStoredProcedures CrearAuditoriaStoredProcedures(AuditoriaStoredProcedures pAuditoriaStoredProcedures, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    pAuditoriaStoredProcedures = DAAuditoriaStoredProcedures.CrearAuditoriaStoredProcedures(pAuditoriaStoredProcedures, pusuario);

                    ts.Complete();

                }

                return pAuditoriaStoredProcedures;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaStoredProceduresBusiness", "CrearAuditoriaStoredProcedures", ex);
                return null;
            }
        }

        public AuditoriaStoredProcedures ConsultarAuditoriaStoredProcedures(Int64 pId, Usuario pusuario)
        {
            try
            {
                AuditoriaStoredProcedures AuditoriaStoredProcedures = new AuditoriaStoredProcedures();
                AuditoriaStoredProcedures = DAAuditoriaStoredProcedures.ConsultarAuditoriaStoredProcedures(pId, pusuario);
                return AuditoriaStoredProcedures;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaStoredProceduresBusiness", "ConsultarAuditoriaStoredProcedures", ex);
                return null;
            }
        }


        public List<AuditoriaStoredProcedures> ListarAuditoriaStoredProcedures(string filtro, Usuario pusuario)
        {
            try
            {
                return DAAuditoriaStoredProcedures.ListarAuditoriaStoredProcedures(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaStoredProceduresBusiness", "ListarAuditoriaStoredProcedures", ex);
                return null;
            }
        }

        public List<string> ListarProcedimientos(string prefix, Usuario usuario)
        {
            try
            {
                return DAAuditoriaStoredProcedures.ListarProcedimientos(prefix, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaStoredProceduresBusiness", "ListarProcedimientos", ex);
                return null;
            }
        }

        public Int64 CrearUsuarioSessionID(Usuario pUsuario)
        {
            Int64 sessionID = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    sessionID = DAAuditoriaStoredProcedures.CrearUsuarioSessionID(pUsuario);

                    ts.Complete();

                }

                return sessionID;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AuditoriaStoredProceduresBusiness", "CrearAuditoriaStoredProcedures", ex);
                return sessionID;
            }
        }

    }
}