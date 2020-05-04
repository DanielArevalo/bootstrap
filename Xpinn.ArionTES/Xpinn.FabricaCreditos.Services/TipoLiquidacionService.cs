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
    /// Servicio para TipoLiquidacion
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoLiquidacionService
    {
        private TipoLiquidacionBusiness BOTipoLiquidacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public TipoLiquidacionService()
        {
            BOTipoLiquidacion = new TipoLiquidacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100210"; } }

        /// <summary>
        /// Crea un TipoLiquidacion
        /// </summary>
        /// <param name="pEntity">Entidad Programa</param>
        /// <returns>Entidad creada</returns>
        public TipoLiquidacion CrearTipoLiquidacion(TipoLiquidacion pTipoLiquidacion, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiquidacion.CrearTipoLiquidacion(pTipoLiquidacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "CrearTipoLiquidacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de TipoLiquidaciones
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de TipoLiquidaciones obtenidos</returns>
        public List<TipoLiquidacion> ListarTipoLiquidacion(TipoLiquidacion pTipoLiquidacion, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiquidacion.ListarTipoLiquidacion(pTipoLiquidacion, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "ListarTipoLiquidacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipoLiquidacion
        /// </summary>
        /// <param name="pId">identificador del TipoLiquidacion</param>
        public void EliminarTipoLiquidacion(Int32 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoLiquidacion.EliminarTipoLiquidacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "EliminarOficina", ex);
            }
        }

        /// <summary>
        /// Modifica un TipoLiquidacion
        /// </summary>
        /// <param name="pEntity">Entidad TipoLiquidacion</param>
        /// <returns>Entidad modificada</returns>
        public TipoLiquidacion ModificarTipoLiquidacion(TipoLiquidacion pTipoLiquidacion, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiquidacion.ModificarTipoLiquidacion(pTipoLiquidacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "ModificarTipoLiquidacion", ex);
                return null;
            }

        }

        /// <summary>
        /// Obtiene la lista de TipoLiquidaciones
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de TipoLiquidaciones obtenidos</returns>
        public TipoLiquidacion ConsultarTipoLiquidacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiquidacion.ConsultarTipoLiquidacion(pId, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "ConsultarTipoLiquidacion", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Id TipoLiquidacion
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Id TipoLiquidacion obtenido</returns>
        public object UltimoIdTipoLiquidacion(Usuario pUsuario)
        {
            try
            {
                return BOTipoLiquidacion.UltimoIdTipoLiquidacion(pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "UltimoIdTipoLiquidacion", ex);
                return null;
            }
        }
    }
}
