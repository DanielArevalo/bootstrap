using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Entities;
using Xpinn.Util;


namespace Xpinn.Riesgo.Services
{

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]

    public class HistoricoSegPerfilService
    {

        private HistoricoSegPersonaBusiness BOHistoricoSegPersonas;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DefinirVariables
        /// </summary>
        public HistoricoSegPerfilService()
        {
            BOHistoricoSegPersonas = new HistoricoSegPersonaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270107"; } }


        public List<HistoricoSegPersona> ListarPersonaHistorico(HistoricoSegPersona pHistoricoSegPersona, string nombre, string apellido, string iden, string perfil, string segR, Usuario vUsuario)
        {
            try
            {
                return BOHistoricoSegPersonas.ListarPersonaHistorico(pHistoricoSegPersona, nombre, apellido, iden, perfil, segR, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPerfilService", "ListarPersonaHistorico", ex);
                return null;
            }
        }

        public HistoricoSegPersona ConsultarPersonaHistorico(Int64 cod_persona, string Fecha_cierre, Usuario vUsuario)
        {
            try
            {
                HistoricoSegPersona pHistoricoSegPersona = new HistoricoSegPersona();
                pHistoricoSegPersona = BOHistoricoSegPersonas.ConsultarPersonaHistorico(cod_persona, Fecha_cierre, vUsuario);
                return pHistoricoSegPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPerfilService", "ConsultarPersonaHistorico", ex);
                return null;
            }
        }
        public HistoricoSegPersona Colocaciones(Int64 cod_persona, string Fecha_cierre, Usuario vUsuario)
        {
            try
            {
                HistoricoSegPersona pHistoricoSegPersona = new HistoricoSegPersona();
                pHistoricoSegPersona = BOHistoricoSegPersonas.Colocaciones(cod_persona, Fecha_cierre, vUsuario);
                return pHistoricoSegPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPerfilService", "ConsultarPersonaHistorico", ex);
                return null;
            }
        }
        public HistoricoSegPersona Captaciones(Int64 cod_persona, string Fecha_cierre, Usuario vUsuario)
        {
            try
            {
                HistoricoSegPersona pHistoricoSegPersona = new HistoricoSegPersona();
                pHistoricoSegPersona = BOHistoricoSegPersonas.Captaciones(cod_persona, Fecha_cierre, vUsuario);
                return pHistoricoSegPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPerfilService", "ConsultarPersonaHistorico", ex);
                return null;
            }
        }

        public List<HistoricoSegPersona> ListarPersonaAnalisis(Int64 pCodPersona, string fecha_cierre, Usuario pUsuario)
        {
            try
            {
                return BOHistoricoSegPersonas.ListarPersonaAnalisis(pCodPersona, fecha_cierre, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPersonaServices", "ListarPersonaAnalisis", ex);
                return null;
            }
        }

        public List<HistoricoSegPersona> ListarHistorialSegementacion(Int64 pCodPersona, Usuario pUsuario)
        {
            try
            {
                return BOHistoricoSegPersonas.ListarHistorialSegementacion(pCodPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPersonaServices", "ListarHistorialSegementacion", ex);
                return null;
            }
        }

        public List<HistoricoSegPersona> ListarHistorialSegementacion(Int64 cod_persona, string fecha_cierre, Usuario vUsuario)
        {
            try
            {
                return BOHistoricoSegPersonas.ListarHistorialSegementacion(cod_persona, fecha_cierre, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPersonaServices", "ListarHistorialSegementacion", ex);
                return null;
            }
        }




    }
}
