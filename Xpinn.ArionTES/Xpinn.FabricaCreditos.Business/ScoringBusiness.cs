using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;
using System.Transactions;

namespace Xpinn.FabricaCreditos.Business
{
    public class ScoringBusiness : GlobalBusiness
    {
        ScoringData DAScoring;


        public ScoringBusiness()
        {
            DAScoring = new ScoringData();
        }


        public Scoring ConsultarDatosScoring(Scoring pScoring, Usuario pUsuario)
        {
            try
            {
                Scoring scoring = DAScoring.ConsultarDatosScoring(pScoring, pUsuario);

                return scoring;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringBusiness", "ConsultarDatosScoring", ex);
                return null;
            }
        }

        public Credito GuardarPreanalisisCredito(Credito credito, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Imagenes.Data.ImagenesORAData DAImagenesCre = new Imagenes.Data.ImagenesORAData();

                    credito = DAImagenesCre.CrearCreditoPreAnalisis(credito, usuario);

                    ts.Complete();
                }

                return credito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "CREARCREDITOANALISIS", ex);
                return null;
            }
        }

        public Scoring ConsultarFactorAntiguedad(long diferenciaFechas, Usuario pUsuario)
        {
            try
            {
                Scoring scoring = DAScoring.ConsultarFactorAntiguedad(diferenciaFechas, pUsuario);

                return scoring;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringBusiness", "ConsultarFactorAntiguedad", ex);
                return null;
            }
        }

        public decimal CalcularParafiscales(long ingresosTotales, Usuario pUsuario)
        {
            try
            {
                return DAScoring.CalcularParafiscales(ingresosTotales, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringBusiness", "CalcularParafiscales", ex);
                return 0;
            }
        }

        public byte[] ConsultarDocumentoScoring(long idAnalisis, Usuario usuario)
        {
            try
            {
                return DAScoring.ConsultarDocumentoScoring(idAnalisis, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringBusiness", "ConsultarDocumentoScoring", ex);
                return null;
            }
        }

        public List<Scoring> ListarScoresRealizados(string filtro, Usuario usuario)
        {
            try
            {
                return DAScoring.ListarScoresRealizados(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringBusiness", "ListarScoresRealizados", ex);
                return null;
            }
        }
    }
}
