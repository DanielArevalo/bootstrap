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
    public class TipoActivoservices
    {
        private TipoActivoBusiness BOTipoActivo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoActivo
        /// </summary>
        public TipoActivoservices()
        {
            BOTipoActivo = new TipoActivoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "50201"; } }

        /// <summary>
        /// Servicio para crear TipoActivo
        /// </summary>
        /// <param name="pEntity">Entidad TipoActivo</param>
        /// <returns>Entidad TipoActivo creada</returns>
        public TipoActivo CrearTipoActivo(TipoActivo vTipoActivo, Usuario pUsuario)
        {
            try
            {
                return BOTipoActivo.CrearTipoActivo(vTipoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoservices", "CrearTipoActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TipoActivo
        /// </summary>
        /// <param name="pTipoActivo">Entidad TipoActivo</param>
        /// <returns>Entidad TipoActivo modificada</returns>
        public TipoActivo ModificarTipoActivo(TipoActivo vTipoActivo, Usuario pUsuario)
        {
            try
            {
                return BOTipoActivo.ModificarTipoActivo(vTipoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoservices", "ModificarTipoActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoActivo
        /// </summary>
        /// <param name="pId">identificador de TipoActivo</param>
        public void EliminarTipoActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoActivo.EliminarTipoActivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoActivo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoActivo
        /// </summary>
        /// <param name="pId">identificador de TipoActivo</param>
        /// <returns>Entidad TipoActivo</returns>
        public TipoActivo ConsultarTipoActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoActivo.ConsultarTipoActivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoservices", "ConsultarTipoActivo", ex);
                return null;
            }
        }

        public List<TipoActivo> ListarTipoActivo(TipoActivo pTipoActivo, Usuario pUsuario)
        {
            try
            {
                return BOTipoActivo.ListarTipoActivo(pTipoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoservices", "ListarTipoActivo", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOTipoActivo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 0;
            }
        }

        //AGREGADO

        public List<TipoActivo> ListarTipoActivo_NIIF(TipoActivo pTipoActivo, Usuario pUsuario)
        {
            try
            {
                return BOTipoActivo.ListarTipoActivo_NIIF(pTipoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoservices", "ListarTipoActivo_NIIF", ex);
                return null;
            }
        }


        public List<TipoActivo> ListarUniGeneradora_NIIF(TipoActivo pTipoActivo, Usuario pUsuario)
        {
            try
            {
                return BOTipoActivo.ListarUniGeneradora_NIIF(pTipoActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoActivoservices", "ListarUniGeneradora_NIIF", ex);
                return null;
            }
        }


    }
}