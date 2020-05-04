using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Aprobador
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PeriodicidadService
    {
        private PeriodicidadBusiness BOPeriodicidad;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public PeriodicidadService()
        {
            BOPeriodicidad = new PeriodicidadBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100209"; } }


        /// <summary>
        /// Servicio para crear Periodicidad
        /// </summary>
        /// <param name="pEntity">Entidad Periodicidad</param>
        /// <returns>Entidad Periodicidad creada</returns>
        public Periodicidad CrearPeriodicidad(Periodicidad vPeriodicidad, Usuario pUsuario)
        {
            try
            {
                return BOPeriodicidad.CrearPeriodicidad(vPeriodicidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadService", "CrearPeriodicidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Periodicidad
        /// </summary>
        /// <param name="pTipoComprobante">Entidad Periodicidad</param>
        /// <returns>Entidad Periodicidad modificada</returns>
        public Periodicidad ModificarPeriodicidad(Periodicidad vPeriodicidad, Usuario pUsuario)
        {
            try
            {
                return BOPeriodicidad.ModificarPeriodicidad(vPeriodicidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadService", "ModificarPeriodicidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Periodicidad
        /// </summary>
        /// <param name="pId">identificador de Periodicidad</param>
        public void EliminarPeriodicidad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOPeriodicidad.EliminarPeriodicidad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadService", "EliminarPeriodicidad", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Periodicidad
        /// </summary>
        /// <param name="pId">identificador de Periodicidad</param>
        /// <returns>Entidad Periodicidad</returns>
        public Periodicidad ConsultarPeriodicidad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPeriodicidad.ConsultarPeriodicidad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadService", "ConsultarPeriodicidad", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de creditos solicitados
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de creditos obtenidos</returns>
        public List<Periodicidad> ListarPeriodicidad(Periodicidad pPeriodicidad, Usuario pUsuario)
        {
            try
            {
                return BOPeriodicidad.ListarPeriodicidad(pPeriodicidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadService", "ListarPeriodicidad", ex);
                return null;
            }
        }


        public List<CreditoSolicitado> ListarTipoTasa(Usuario pUsuario)
        {
            try
            {
                return BOPeriodicidad.ListarTipoTasa(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadService", "ListarPeriodicidad", ex);
                return null;
            }
        }
    }
}
