using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Business;
using System.ServiceModel;


namespace Xpinn.FabricaCreditos.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ScoringService
    {
        ScoringBusiness BOScoring;
        ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "100166"; } }

        public ScoringService()
        {
            BOScoring = new ScoringBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public Scoring ConsultarDatosScoring(Scoring pScoring, Usuario pUsuario)
        {
            try
            {
                return BOScoring.ConsultarDatosScoring(pScoring, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringService", "ConsultarDatosScoring", ex);
                return null;
            }
        }


        public Scoring ConsultarFactorAntiguedad(long diferenciaFechas, Usuario pUsuario)
        {
            try
            {
                return BOScoring.ConsultarFactorAntiguedad(diferenciaFechas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringService", "ConsultarFactorAntiguedad", ex);
                return null;
            }
        }

        public decimal CalcularParafiscales(long ingresosTotales, Usuario pUsuario)
        {
            try
            {
                return BOScoring.CalcularParafiscales(ingresosTotales, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringService", "CalcularParafiscales", ex);
                return 0;
            }
        }


        public byte[] ConsultarDocumentoScoring(long idAnalisis, Usuario usuario)
        {
            try
            {
                return BOScoring.ConsultarDocumentoScoring(idAnalisis, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringService", "ConsultarDocumentoScoring", ex);
                return null;
            }
        }


        public Credito GuardarPreanalisisCredito(Credito credito, Usuario usuario)
        {
            try
            {
                return BOScoring.GuardarPreanalisisCredito(credito, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringService", "GuardarPreanalisisCredito", ex);
                return null;
            }
        }

        public List<Scoring> ListarScoresRealizados(string filtro, Usuario usuario)
        {
            try
            {
                return BOScoring.ListarScoresRealizados(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringService", "ListarScoresRealizados", ex);
                return null;
            }
        }
    }
}
