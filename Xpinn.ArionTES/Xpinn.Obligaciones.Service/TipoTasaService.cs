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
    public class TipoTasaService
    {
        private TipoTasaBusiness BOTipoTasa;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoTasa
        /// </summary>
        public TipoTasaService()
        {
            BOTipoTasa = new TipoTasaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        
        /// <summary>
        /// Servicio para obtener lista de Tipo de Liquidacion a partir de unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoTasa obtenidos</returns>
        public List<TipoTasa> ListarTipoTasa(TipoTasa pTipLiq, Usuario pUsuario)
        {
            try
            {
                return BOTipoTasa.ListarTipoTasa(pTipLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaService", "ListarTipoTasa", ex);
                return null;
            }
        }

        public List<TipoTasa> ListarTipoHistorico(TipoTasa pTipLiq, Usuario pUsuario)
        {
            try
            {
                return BOTipoTasa.ListarTipoHistorico(pTipLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaService", "ListarTipoHistorico", ex);
                return null;
            }
        }

        public double ConsultaTasaHistorica(Int64 pTipoHistorico, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return BOTipoTasa.ConsultaTasaHistorica(pTipoHistorico, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaService", "ConsultaTasaHistorica", ex);
                return 0;
            }
        }

    }
}
