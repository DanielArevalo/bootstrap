using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DestinacionServices
    {
        private DestinacionBusiness BODestinacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Destinacion
        /// </summary>
        public DestinacionServices()
        {
            BODestinacion = new DestinacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220103"; } }

        /// <summary>
        /// Servicio para crear Destinacion
        /// </summary>
        /// <param name="pEntity">Entidad Destinacion</param>
        /// <returns>Entidad Destinacion creada</returns>
        public Destinacion CrearDestinacion(Destinacion vDestinacion, Usuario pUsuario)
        {
            try
            {
                return BODestinacion.CrearDestinacion(vDestinacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Destinacionservice", "CrearDestinacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Destinacion
        /// </summary>
        /// <param name="pDestinacion">Entidad Destinacion</param>
        /// <returns>Entidad Destinacion modificada</returns>
        public Destinacion ModificarDestinacion(Destinacion vDestinacion, Usuario pUsuario)
        {
            try
            {
                return BODestinacion.ModificarDestinacion(vDestinacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Destinacionservice", "ModificarDestinacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Destinacion
        /// </summary>
        /// <param name="pId">identificador de Destinacion</param>
        public void EliminarDestinacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BODestinacion.EliminarDestinacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarDestinacion", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Destinacion
        /// </summary>
        /// <param name="pId">identificador de Destinacion</param>
        /// <returns>Entidad Destinacion</returns>
        public Destinacion ConsultarDestinacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BODestinacion.ConsultarDestinacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Destinacionservice", "ConsultarDestinacion", ex);
                return null;
            }
        }

        public List<Destinacion> ListarDestinacion(Destinacion pDestinacion, Usuario pUsuario)
        {
            try
            {
                return BODestinacion.ListarDestinacion(pDestinacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Destinacionservice", "ListarDestinacion", ex);
                return null;
            }
        }
        public List<Destinacion> ListarDestinacion_Ahorros(Destinacion pDestinacion, Usuario pUsuario)
        {
            try
            {
                return BODestinacion.ListarDestinacion_Ahorros(pDestinacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Destinacionservice", "ListarDestinacion_Ahorros", ex);
                return null;
            }
        }
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BODestinacion.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 0;
            }
        }


    }
}