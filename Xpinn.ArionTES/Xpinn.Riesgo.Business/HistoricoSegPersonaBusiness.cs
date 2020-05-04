using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Business
{
  public  class HistoricoSegPersonaBusiness : GlobalBusiness
    {
        HistoricoSegPerfilData DAHistoricoSegPerfil;


        public HistoricoSegPersonaBusiness()
        {
            DAHistoricoSegPerfil = new HistoricoSegPerfilData();
        }

        public List<HistoricoSegPersona> ListarPersonaHistorico(HistoricoSegPersona pHistoricoSegPersona, string nombre, string apellido, string iden, string perfil, string segR, Usuario Usuario)
        {
            try
            {
                return DAHistoricoSegPerfil.ListarPersonaHistorico(pHistoricoSegPersona, nombre, apellido, iden, perfil, segR,  Usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPersonaBusiness", "ListarPersonaHistorico", ex);
                return null;
            }
        }

        public HistoricoSegPersona ConsultarPersonaHistorico(Int64 cod_persona, string Fecha_cierre, Usuario vUsuario)
        {
            try
            {
                HistoricoSegPersona pHistoricoSegPersona = new HistoricoSegPersona();
                pHistoricoSegPersona = DAHistoricoSegPerfil.ConsultarPersonaHistorico(cod_persona, Fecha_cierre, vUsuario);
                return pHistoricoSegPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPersonaBusiness", "ConsultarPersonaHistorico", ex);
                return null;
            }
        }
        public HistoricoSegPersona Colocaciones(Int64 cod_persona, string Fecha_cierre, Usuario vUsuario)
        {
            try
            {
                HistoricoSegPersona pHistoricoSegPersona = new HistoricoSegPersona();
                pHistoricoSegPersona = DAHistoricoSegPerfil.Colocaciones(cod_persona, Fecha_cierre, vUsuario);
                return pHistoricoSegPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPersonaBusiness", "ConsultarPersonaHistorico", ex);
                return null;
            }
        }
        
        public HistoricoSegPersona Captaciones(Int64 cod_persona, string Fecha_cierre, Usuario vUsuario)
        {
            try
            {
                HistoricoSegPersona pHistoricoSegPersona = new HistoricoSegPersona();
                pHistoricoSegPersona = DAHistoricoSegPerfil.Captaciones(cod_persona, Fecha_cierre, vUsuario);
                return pHistoricoSegPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPersonaBusiness", "ConsultarPersonaHistorico", ex);
                return null;
            }
        }
        
        public List<HistoricoSegPersona> ListarPersonaAnalisis(Int64 pCodPersona, string fecha_cierre, Usuario pUsuario)
        {
            try
            {
                return DAHistoricoSegPerfil.ListarPersonaAnalisis(pCodPersona, fecha_cierre, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPersonaBusiness", "ConsultarPersonaHistorico", ex);
                return null;
            }
        }

        public List<HistoricoSegPersona> ListarHistorialSegementacion(Int64 pCodPersona, Usuario pUsuario)
        {
            try
            {
                return DAHistoricoSegPerfil.ListarHistorialSegementacion(pCodPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPersonaBusiness", "ListarHistorialSegementacion", ex);
                return null;
            }
        }

        public List<HistoricoSegPersona> ListarHistorialSegementacion(Int64 cod_persona, string fecha_cierre, Usuario vUsuario)
        {
            try
            {
                return DAHistoricoSegPerfil.ListarHistorialSegementacion(cod_persona, fecha_cierre, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegPersonaBusiness", "ListarHistorialSegementacion", ex);
                return null;
            }
        }

    }
}
