using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Presupuesto.Business;
using Xpinn.Presupuesto.Entities;

namespace Xpinn.Presupuesto.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EjecucionService
    {
        private PresupuestoBusiness BOPresupuesto;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Presupuesto
        /// </summary>
        public EjecucionService()
        {
            BOPresupuesto = new PresupuestoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "140207"; } }

        public int GetNumeroColumnas()
        {
            return BOPresupuesto.GetNumeroColumnas();
        }


        /// <summary>
        /// Servicio para obtener Presupuesto
        /// </summary>
        /// <param name="pId">identificador de Presupuesto</param>
        /// <returns>Entidad Presupuesto</returns>
        public Xpinn.Presupuesto.Entities.Presupuesto ConsultarPresupuesto(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarPresupuesto(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EjecucionService", "ConsultarPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Presupuestos a partir de unos filtros
        /// </summary>
        /// <param name="pPresupuesto">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Presupuesto obtenidos</returns>
        public List<Xpinn.Presupuesto.Entities.Presupuesto> ListarPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto vPresupuesto, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarPresupuesto(vPresupuesto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EjecucionService", "ListarPresupuesto", ex);
                return null;
            }
        }

        public List<Xpinn.Presupuesto.Entities.Presupuesto> ListarPeriodosPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto vPresupuesto, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarPeriodosPresupuesto(vPresupuesto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarPeriodosPresupuesto", ex);
                return null;
            }
        }

        public DataTable ListarEjecucionPresupuesto(Int64 pIdPresupuesto, Int64 pNumeroPeriodo, Usuario pUsuario, Int32 nivel)
        {
            try
            {
                return BOPresupuesto.ListarEjecucionPresupuesto(pIdPresupuesto, pNumeroPeriodo, pUsuario,nivel);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarEjecucionPresupuesto", ex);
                return null;
            }
        }

   }
}