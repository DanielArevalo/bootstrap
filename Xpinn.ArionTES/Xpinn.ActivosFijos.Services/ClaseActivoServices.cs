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
    public class ClaseActivoservices
    {
        private ClaseActivoBusiness BOClaseActivo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ClaseActivo
        /// </summary>
        public ClaseActivoservices()
        {
            BOClaseActivo = new ClaseActivoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "50202"; } }

        /// <summary>
        /// Servicio para crear ClaseActivo
        /// </summary>
        /// <param name="pEntity">Entidad ClaseActivo</param>
        /// <returns>Entidad ClaseActivo creada</returns>
        public ClaseActivo CrearClaseActivo(ClaseActivo vClaseActivo, Usuario pUsuario)
        {
            try
            {
                return BOClaseActivo.CrearClaseActivo(vClaseActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClaseActivoservices", "CrearClaseActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ClaseActivo
        /// </summary>
        /// <param name="pClaseActivo">Entidad ClaseActivo</param>
        /// <returns>Entidad ClaseActivo modificada</returns>
        public ClaseActivo ModificarClaseActivo(ClaseActivo vClaseActivo, Usuario pUsuario)
        {
            try
            {
                return BOClaseActivo.ModificarClaseActivo(vClaseActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClaseActivoservices", "ModificarClaseActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ClaseActivo
        /// </summary>
        /// <param name="pId">identificador de ClaseActivo</param>
        public void EliminarClaseActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOClaseActivo.EliminarClaseActivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarClaseActivo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ClaseActivo
        /// </summary>
        /// <param name="pId">identificador de ClaseActivo</param>
        /// <returns>Entidad ClaseActivo</returns>
        public ClaseActivo ConsultarClaseActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOClaseActivo.ConsultarClaseActivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClaseActivoservices", "ConsultarClaseActivo", ex);
                return null;
            }
        }

        public List<ClaseActivo> ListarClaseActivo(ClaseActivo pClaseActivo, Usuario pUsuario)
        {
            try
            {
                return BOClaseActivo.ListarClaseActivo(pClaseActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClaseActivoservices", "ListarTipoComp", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOClaseActivo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 0;
            }
        }
        
        //AGREGADO

        public List<ClaseActivo> ListarClasificacion(ClaseActivo pClaseActivo, Usuario pUsuario)
        {
            try
            {
                return BOClaseActivo.ListarClasificacion(pClaseActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClaseActivoservices", "ListarClasificacion", ex);
                return null;
            }
        }


    }
}