using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Scoring.Data;
using Xpinn.Scoring.Entities;
using Xpinn.Comun.Business;
using Xpinn.Comun.Entities;

namespace Xpinn.Scoring.Business
{
    /// <summary>
    /// Objeto de negocio para ScoringCreditos
    /// </summary>
    public class ScoringCreditosBusiness : GlobalData
    {
        private ScoringCreditosData DAScoringCreditos;
        private Xpinn.Comun.Business.FechasBusiness BOFechas;

        /// <summary>
        /// Constructor del objeto de negocio para ScoringCreditos
        /// </summary>
        public ScoringCreditosBusiness()
        {
            DAScoringCreditos = new ScoringCreditosData();
            BOFechas = new Xpinn.Comun.Business.FechasBusiness();
        }




        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScoringCreditos> ListarScoringCredito(ScoringCreditos pScoringCreditos, string pEstadoCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAScoringCreditos.ListarScoringCredito(pScoringCreditos, pEstadoCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosBusiness", "ListarScoringCreditos", ex);
                return null;
            }
        }

        public List<RiesgoCredito> ListarRiesgoCredito(DateTime pFechaCorte, String filtro, Usuario pUsuario)
        {
            try
            {
                return DAScoringCreditos.ListarRiesgoCredito(pFechaCorte, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScoringCreditos> ListarCreditosRecogidos(ScoringCreditos pScoringCreditos, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAScoringCreditos.ListarCreditosRecogidos(pScoringCreditos, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosBusiness", "ListarScoringCreditos", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScoringCreditos> ListarCodeudores(ScoringCreditos pScoringCreditos, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAScoringCreditos.ListarCodeudores(pScoringCreditos, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosBusiness", "ListarScoringCreditos", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScoringCreditos> ListarAprobScoringNegados(ScoringCreditos pScoringCreditos, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAScoringCreditos.ListarAprobScoringNegados(pScoringCreditos, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosBusiness", "ListarScoringCreditos", ex);
                return null;
            }
        }


        public ScScoringCredito CrearScScoringCredito(ScScoringCredito pScScoringCredito, Usuario pUsuario)
        {            
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pScScoringCredito = DAScoringCreditos.CrearScScoringCredito(pScScoringCredito, pUsuario);

                    ts.Complete();
                }

                return pScScoringCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteBusiness", "CrearReporte", ex);
                return null;
            }
        }

        public ScScoringCredito CalculaScScoringCredito(ScScoringCredito pScScoringCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pScScoringCredito = DAScoringCreditos.CalculaScScoringCredito(pScScoringCredito, pUsuario);

                    ts.Complete();
                }

                return pScScoringCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosBusiness", "CalculaScScoringCredito", ex);
                return null;
            }
        }

        public ScScoringCredito ValidarScoringCredito(ScScoringCredito pScScoringCredito, Usuario pUsuario)
        {
            try
            {
                return DAScoringCreditos.ValidarScoringCredito(pScScoringCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosBusiness", "ValidarScoringCredito", ex);
                return pScScoringCredito;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScScoringCredito> ListarScScoringCredito(ScScoringCredito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAScoringCreditos.ListarScScoringCredito(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosService", "ListarCredito", ex);
                return null;
            }
        }




        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<SeguimientoScoring> ListarSegumientoScoring(SeguimientoScoring pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAScoringCreditos.ListarSegumientoScoring(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosServices", "ListarSegumientoScoring", ex);
                return null;
            }
        }

        public List<ScScoringCreditoDetalle> ListarScoringCreditoDetalle(ScScoringCredito pScoringCreditos, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAScoringCreditos.ListarScoringCreditoDetalle(pScoringCreditos, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosBusiness", "ListarScoringCreditoDetalle", ex);
                return null;
            }
        }

        public List<RiesgoCredito> ListarFechaCierreYaHechas(string pTipo = "R", string pEstado = "D", Usuario usuario = null)
        {
            try
            {
                return DAScoringCreditos.ListarFechaCierreYaHechas(pTipo, pEstado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosServices", "ListarFechaCierreYaHechas", ex);
                return null;
            }
        }

        public bool CierreSegmentacionCredito(DateTime pFechaCorte, string pEstado, ref string pError, Usuario pUsuario)
        {
            try
            {
                return DAScoringCreditos.CierreSegmentacionCredito(pFechaCorte, pEstado, ref pError, pUsuario);
            }
            catch
            {                
                return false;
            }
        }

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre(string pTipo = "R", Usuario pUsuario = null)
        {
            List<Xpinn.Comun.Entities.Cierea> LstCierre = new List<Xpinn.Comun.Entities.Cierea>();
            // Determinar la periodicidad de cierre
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DAScoringCreditos.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            // Determinar la fecha del último cierre realizado
            Xpinn.Comun.Entities.Cierea pCierre = new Xpinn.Comun.Entities.Cierea();
            pCierre.tipo = pTipo;
            pCierre.estado = "D";
            pCierre = DAScoringCreditos.FechaUltimoCierre(pCierre, "", pUsuario);
            DateTime FecIni;
            if (pCierre == null)
            {
                Xpinn.Comun.Entities.Cierea pCierreCar = new Xpinn.Comun.Entities.Cierea();
                pCierreCar.tipo = "R";
                pCierreCar.estado = "D";
                pCierreCar = DAScoringCreditos.FechaPrimerCierre(pCierreCar, "", pUsuario);
                if (pCierreCar == null)
                    FecIni = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1).AddDays(-1);
                else
                    FecIni = pCierreCar.fecha;
            }
            else
            { 
                FecIni = pCierre.fecha;
            }
            if (FecIni == DateTime.MinValue)
                return null;
            if (FecIni > DateTime.Now.AddDays(15))
                return null;
            // Calcular fechas de cierre inicial
            DateTime FecFin = DateTime.MinValue;
            FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);
            if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
            {
                bool control = true;
                do
                {
                    FecFin = FecFin.AddDays(1);
                    if (FecFin.Day == 1)
                    {
                        FecFin = FecFin.AddDays(-1);
                        control = false;
                    }
                } while (control == true);
            }

            // Determinar los periodos pendientes por cerrar
            while (FecFin <= DateTime.Now.AddDays(15))
            {
                Xpinn.Comun.Entities.Cierea cieRea = new Xpinn.Comun.Entities.Cierea();
                cieRea.fecha = FecFin;
                LstCierre.Add(cieRea);
                FecIni = FecFin;
                FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);
                if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
                {
                    bool control = true;
                    do
                    {
                        FecFin = FecFin.AddDays(1);
                        if (FecFin.Day == 1)
                        {
                            FecFin = FecFin.AddDays(-1);
                            control = false;
                        }
                    } while (control == true);
                }
            }
            return LstCierre;
        }

        public List<RiesgoCredito> ListarCalificaciones(string pFiltro, Usuario usuario = null)
        {
            return DAScoringCreditos.ListarCalificaciones(pFiltro, usuario);
        }

        public List<RiesgoCredito> ListarRiesgoCreditoProvision(DateTime pFechaCorte, String filtro, Usuario pUsuario)
        {
            try
            {
                return DAScoringCreditos.ListarRiesgoCreditoProvision(pFechaCorte, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }

}