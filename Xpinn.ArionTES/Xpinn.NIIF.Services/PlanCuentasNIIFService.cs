using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;
using Xpinn.Util;
using System.Data;
using System.ServiceModel;

namespace Xpinn.NIIF.Services
{
    public class PlanCuentasNIIFService
    {

        private PlanCuentasNIIFBusiness BOPlanCuentasNIIF;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoProgramaNew;
        public string CodigoProgramaPlan;
        public string CodigoProgramaUpdate;

        public PlanCuentasNIIFService()
        {
            BOPlanCuentasNIIF = new PlanCuentasNIIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
            CodigoProgramaNew = "210401";
            CodigoProgramaPlan = "210402";
            CodigoProgramaUpdate = "210403";
        }

        public string CodigoPrograma { get { return "30502"; } }
        public string CodigoProgramaHomolo { get { return "210108"; } }
        public string CodigoProgramaRepComp { get { return "210113"; } }

        /// <summary>
        /// Servicio para crear PlanCuentasNIIF
        /// </summary>
        /// <param name="pEntity">Entidad PlanCuentasNIIF</param>
        /// <returns>Entidad PlanCuentasNIIF creada</returns>
        public Xpinn.Contabilidad.Entities.PlanCuentas CrearPlanCuentasNIIF(Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentasNIIF, List<PlanCtasHomologacionNIF> lstData, Usuario vUsuario)
        {
            try
            {
                return BOPlanCuentasNIIF.CrearPlanCuentasNIIF(pPlanCuentasNIIF, lstData, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFService", "CrearPlanCuentasNIIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar PlanCuentasNIIF
        /// </summary>
        /// <param name="pPlanCuentasNIIF">Entidad PlanCuentasNIIF</param>
        /// <returns>Entidad PlanCuentasNIIF modificada</returns>
        public Xpinn.Contabilidad.Entities.PlanCuentas ModificarPlanCuentasNIIF(Xpinn.Contabilidad.Entities.PlanCuentas pPlanCuentasNIIF, List<PlanCtasHomologacionNIF> lstData, Usuario vUsuario)
        {
            try
            {
                return BOPlanCuentasNIIF.ModificarPlanCuentasNIIF(pPlanCuentasNIIF, lstData, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFService", "ModificarPlanCuentasNIIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar PlanCuentasNIIF
        /// </summary>
        /// <param name="pId">identificador de PlanCuentasNIIF</param>
        public void EliminarHomologacionNIIFLocal(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOPlanCuentasNIIF.EliminarHomologacionNIIFLocal(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFService", "EliminarHomologacionNIIFLocal", ex);
            }
        }


        public void EliminarPlanCuentasNIIF(string pId, Usuario vUsuario)
        {
            try
            {
                BOPlanCuentasNIIF.EliminarPlanCuentasNIIF(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFService", "EliminarPlanCuentasNIIF", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener PlanCuentasNIIF
        /// </summary>
        /// <param name="pId">identificador de PlanCuentasNIIF</param>
        /// <returns>Entidad PlanCuentasNIIF</returns>
        public PlanCuentasNIIF ConsultarPlanCuentasNIIF(string pId, Usuario pUsuario)
        {
            try
            {
                return BOPlanCuentasNIIF.ConsultarPlanCuentasNIIF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFService", "ConsultarPlanCuentasNIIF", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de PlanCuentasNIIFs a partir de unos filtros
        /// </summary>
        /// <param name="pPlanCuentasNIIF">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanCuentasNIIF obtenidos</returns>
        public List<PlanCuentasNIIF> ListarPlanCuentasNIIF(PlanCuentasNIIF vPlanCuentasNIIF, Usuario pUsuario)
        {
            try
            {
                return BOPlanCuentasNIIF.ListarPlanCuentasNIIF(vPlanCuentasNIIF, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFService", "ListarPlanCuentasNIIF", ex);
                return null;
            }
        }

        public void GenerarHomologacionMovimientos(ref string pError, DateTime pFechaIni, DateTime pFechaFin, Int64 pTipoComp, Usuario vUsuario)
        {
            BOPlanCuentasNIIF.GenerarHomologacionMovimientos(ref pError, pFechaIni, pFechaFin, pTipoComp, vUsuario);
        }

        public List<PlanCuentasNIIF> ListarReporteComparativoNIIF(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOPlanCuentasNIIF.ListarReporteComparativoNIIF(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFService", "ListarReporteComparativoNIIF", ex);
                return null;
            }
        }

        public List<PlanCtasHomologacionNIF> ListarCuentasHomologadas(string pFiltro, string pOpcion, Usuario vUsuario)
        {
            try
            {
                return BOPlanCuentasNIIF.ListarCuentasHomologadas(pFiltro, pOpcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasNIIFService", "ListarCuentasHomologadas", ex);
                return null;
            }
        }

    }
}