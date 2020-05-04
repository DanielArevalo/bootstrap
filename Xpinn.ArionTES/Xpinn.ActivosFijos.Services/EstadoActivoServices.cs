using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.ActivosFijos.Business;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.ActivosFijos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EstadoActivoservices
    {
        private EstadoActivoBusiness BOEstadoActivo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para EstadoActivo
        /// </summary>
        public EstadoActivoservices()
        {
            BOEstadoActivo = new EstadoActivoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "50203"; } }

        /// <summary>
        /// Servicio para crear EstadoActivo
        /// </summary>
        /// <param name="pEntity">Entidad EstadoActivo</param>
        /// <returns>Entidad EstadoActivo creada</returns>
        public EstadoActivo CrearEstadoActivo(EstadoActivo vEstadoActivo, Usuario pUsuario)
        {
            try
            {
                return BOEstadoActivo.CrearEstadoActivo(vEstadoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoActivoservices", "CrearEstadoActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar EstadoActivo
        /// </summary>
        /// <param name="pEstadoActivo">Entidad EstadoActivo</param>
        /// <returns>Entidad EstadoActivo modificada</returns>
        public EstadoActivo ModificarEstadoActivo(EstadoActivo vEstadoActivo, Usuario pUsuario)
        {
            try
            {
                return BOEstadoActivo.ModificarEstadoActivo(vEstadoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoActivoservices", "ModificarEstadoActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar EstadoActivo
        /// </summary>
        /// <param name="pId">identificador de EstadoActivo</param>
        public void EliminarEstadoActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOEstadoActivo.EliminarEstadoActivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarEstadoActivo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener EstadoActivo
        /// </summary>
        /// <param name="pId">identificador de EstadoActivo</param>
        /// <returns>Entidad EstadoActivo</returns>
        public EstadoActivo ConsultarEstadoActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOEstadoActivo.ConsultarEstadoActivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoActivoservices", "ConsultarEstadoActivo", ex);
                return null;
            }
        }

        public List<EstadoActivo> ListarEstadoActivo(EstadoActivo pEstadoActivo, Usuario pUsuario)
        {
            try
            {
                return BOEstadoActivo.ListarEstadoActivo(pEstadoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoActivoservices", "ListarEstadoActivo", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOEstadoActivo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 0;
            }
        }

    }
}