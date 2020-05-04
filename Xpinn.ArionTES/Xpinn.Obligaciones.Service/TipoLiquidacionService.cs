using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Obligaciones.Business;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Services
{
    /// <summary>
    /// Servicios para Tipo Liquidacion
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoLiquidacionService
    {
        private TipoLiquidacionBusiness BOTipoLiquidacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoLiquidacion
        /// </summary>
        public TipoLiquidacionService()
        {
            BOTipoLiquidacion = new TipoLiquidacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "130202"; } }
        
        /// <summary>
        /// Servicio para obtener lista de Tipo de Liquidacion a partir de unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoLiquidacion obtenidos</returns>
        public List<TipoLiquidacion> ListarTipoLiquidacion(TipoLiquidacion pTipLiq, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiquidacion.ListarTipoLiquidacion(pTipLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "ListarTipoLiquidacion", ex);
                return null;
            }
        }


        /// <summary>
        /// Elimina Un Tipo de Liquidacion
        /// </summary>
        /// <param name="pId">identificador del Tipo de Liquidacion</param>
        public void EliminarTipoLiq(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoLiquidacion.EliminarTipoLiq(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "EliminarTipoLiq", ex);
            }
        }



        /// <summary>
        /// Crea una TipoLiquidacion
        /// </summary>
        /// <param name="pEntity">Entidad Tipo Liquidacion</param>
        /// <returns>Entidad creada</returns>
        public TipoLiquidacion CrearTipoLiq(TipoLiquidacion pTipoLiq, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiquidacion.CrearTipoLiq(pTipoLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "CrearTipoLiq", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica una TipoLiquidacion
        /// </summary>
        /// <param name="pEntity">Entidad TipoLiquidacion</param>
        /// <returns>Entidad modificada</returns>
        public TipoLiquidacion ModificarTipoLiq(TipoLiquidacion pTipoLiq, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiquidacion.ModificarTipoLiq(pTipoLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "MostrarTipoLiq", ex);
                return null;
            }

        }

        /// <summary>
        /// Obtiene Un Tipo de Liquidacion
        /// </summary>
        /// <param name="pId">identificador del tipo de liquidacion</param>
        /// <returns>Tipo consultado</returns>
        public TipoLiquidacion ConsultarTipoLiq(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiquidacion.ConsultarTipoLiq(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "ConsultarTipoLiq", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene el conteo de usuarios asociados a la oficina especifica 
        /// </summary>
        /// <param name="pId">identificador de la TipoLiquidacion</param>
        /// <returns>TipoLiquidacion consultada</returns>
        public TipoLiquidacion ConsultarTipoLiquidacionXLineaObligacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiquidacion.ConsultarTipoLiquidacionXLineaObligacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiquidacionService", "ConsultarTipoLiquidacionXLineaObligacion", ex);
                return null;
            }
        }
    }
}
