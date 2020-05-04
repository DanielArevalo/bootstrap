using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Scoring.Business;
using Xpinn.Scoring.Entities;

namespace Xpinn.Scoring.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>


    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ScoringCreditosService
    {
        private ScoringCreditosBusiness BOScoringCreditos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ScoringCreditos
        /// </summary>
        public ScoringCreditosService()
        {
            BOScoringCreditos = new ScoringCreditosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

      
        public string CodigoPrograma { get { return "160101"; } }
        public string CodigoProgramaApr { get { return "160104"; } }
        public string CodigoProgramaRiesgo { get { return "160105"; } }
        public string CodigoProgramaSeg { get { return "160106"; } }
        public string CodigoProgramaProvRiesgo { get { return "160107"; } }


        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScoringCreditos> ListarScoringCredito(ScoringCreditos pCredito, string pEstadoCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOScoringCreditos.ListarScoringCredito(pCredito, pEstadoCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosService", "ListarCredito", ex);
                return null;
            }
        }

        public List<RiesgoCredito> ListarRiesgoCredito(DateTime pFechaCorte, String filtro, Usuario pUsuario)
        {
            try
            {
                return BOScoringCreditos.ListarRiesgoCredito(pFechaCorte, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosService", "ListarRiesgoCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<ScoringCreditos> ListarCreditosRecogidos(ScoringCreditos pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOScoringCreditos.ListarCreditosRecogidos(pCredito, pUsuario, filtro);
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
        public List<ScoringCreditos> ListarCodeudores(ScoringCreditos pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOScoringCreditos.ListarCodeudores(pCredito, pUsuario, filtro);
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
        public List<ScoringCreditos> ListarAprobScoringNegados(ScoringCreditos pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOScoringCreditos.ListarAprobScoringNegados(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosService", "ListarCredito", ex);
                return null;
            }
        }
    

        /// <summary>
        /// Servicio para crear ScScoringBoardVar
        /// </summary>
        /// <param name="pEntity">Entidad ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar creada</returns>
        public ScScoringCredito CrearScScoringCredito(ScScoringCredito pScScoringCredito, Usuario pUsuario)
        {
            try
            {
                return BOScoringCreditos.CrearScScoringCredito(pScScoringCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosService", "CrearMotivo", ex);
                return null;
            }
        }

        public ScScoringCredito CalculaScScoringCredito(ScScoringCredito pScScoringCredito, Usuario pUsuario)
        {
            try
            {
                return BOScoringCreditos.CalculaScScoringCredito(pScScoringCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosService", "CalculaScScoringCredito", ex);
                return null;
            }
        }

        public ScScoringCredito ValidarScoringCredito(ScScoringCredito pScScoringCredito, Usuario pUsuario)
        {
            try
            {
                return BOScoringCreditos.ValidarScoringCredito(pScScoringCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosService", "ValidarScoringCredito", ex);
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
                return BOScoringCreditos.ListarScScoringCredito(pCredito, pUsuario, filtro);
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
                return BOScoringCreditos.ListarSegumientoScoring(pCredito, pUsuario, filtro);
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
                return BOScoringCreditos.ListarScoringCreditoDetalle(pScoringCreditos, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScoringCreditosServices", "ListarScoringCreditoDetalle", ex);
                return null;
            }
        }

        public List<RiesgoCredito> ListarFechaCierreYaHechas(string pTipo = "R", string pEstado = "D", Usuario usuario = null)
        {
            try
            {
                return BOScoringCreditos.ListarFechaCierreYaHechas(pTipo, pEstado, usuario);
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
                return BOScoringCreditos.CierreSegmentacionCredito(pFechaCorte, pEstado, ref pError, pUsuario);
            }
            catch
            {
                return false;
            }
        }

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre1(string pTipo = "R", Usuario pUsuario = null)
        {
            return BOScoringCreditos.ListarFechaCierre(pTipo, pUsuario);
        }

        public List<RiesgoCredito> ListarCalificaciones(string pFiltro, Usuario usuario = null)
        {
            return BOScoringCreditos.ListarCalificaciones(pFiltro, usuario);
        }

        public List<RiesgoCredito> ListarRiesgoCreditoProvision(DateTime pFechaCorte, String filtro, Usuario pUsuario)
        {
            try
            {
                return BOScoringCreditos.ListarRiesgoCreditoProvision(pFechaCorte, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                return null;
            }
        }




    }



}