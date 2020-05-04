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
    public class TipoPresupuestoService
    {
        private TipoPresupuestoBusiness BOTipoPresupuesto;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoPresupuesto
        /// </summary>
        public TipoPresupuestoService()
        {
            BOTipoPresupuesto = new TipoPresupuestoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90106"; } }

        /// <summary>
        /// Servicio para crear TipoPresupuesto
        /// </summary>
        /// <param name="pEntity">Entidad TipoPresupuesto</param>
        /// <returns>Entidad TipoPresupuesto creada</returns>
        public TipoPresupuesto CrearTipoPresupuesto(TipoPresupuesto vTipoPresupuesto, Usuario pUsuario)
        {
            try
            {
                return BOTipoPresupuesto.CrearTipoPresupuesto(vTipoPresupuesto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPresupuestoService", "CrearTipoPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TipoPresupuesto
        /// </summary>
        /// <param name="pTipoPresupuesto">Entidad TipoPresupuesto</param>
        /// <returns>Entidad TipoPresupuesto modificada</returns>
        public TipoPresupuesto ModificarTipoPresupuesto(TipoPresupuesto vTipoPresupuesto, Usuario pUsuario)
        {
            try
            {
                return BOTipoPresupuesto.ModificarTipoPresupuesto(vTipoPresupuesto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPresupuestoService", "ModificarTipoPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoPresupuesto
        /// </summary>
        /// <param name="pId">identificador de TipoPresupuesto</param>
        public void EliminarTipoPresupuesto(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoPresupuesto.EliminarTipoPresupuesto(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoPresupuesto", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoPresupuesto
        /// </summary>
        /// <param name="pId">identificador de TipoPresupuesto</param>
        /// <returns>Entidad TipoPresupuesto</returns>
        public TipoPresupuesto ConsultarTipoPresupuesto(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoPresupuesto.ConsultarTipoPresupuesto(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPresupuestoService", "ConsultarTipoPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TipoPresupuestos a partir de unos filtros
        /// </summary>
        /// <param name="pTipoPresupuesto">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoPresupuesto obtenidos</returns>
        public List<TipoPresupuesto> ListarTipoPresupuesto(TipoPresupuesto vTipoPresupuesto, Usuario pUsuario)
        {
            try
            {
                return BOTipoPresupuesto.ListarTipoPresupuesto(vTipoPresupuesto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPresupuestoService", "ListarTipoPresupuesto", ex);
                return null;
            }
        }



    }
}