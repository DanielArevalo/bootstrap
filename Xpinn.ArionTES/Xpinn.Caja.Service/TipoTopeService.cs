using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;


namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para TipoTope
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoTopeService
    {
        private Xpinn.Caja.Business.TipoTopeBusiness BOTipoTope;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del Servicio para TipoTope
        /// </summary>
        public TipoTopeService()
        {
            BOTipoTope = new TipoTopeBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public int CodigoTipoTope;


        /// <summary>
        /// Obtiene un TipoTope-Caja
        /// </summary>
        /// <param name="pId">identificador del TipoTope-Caja</param>
        /// <returns>TipoTope-Caja consultada</returns>
        public TipoTope ConsultarTipoTopeCaja(Int64 pId, Int64 pMon, Int64 pCaja, Usuario pUsuario)
        {
            try
            {
                return BOTipoTope.ConsultarTipoTopeCaja(pId,pMon, pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTopeService", "ConsultarTipoTopeCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Tipos de Topes-Monedas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos Topes obtenidos</returns>
        public List<TipoTope> ListarTipoTope(TipoTope pTipTope, Usuario pUsuario)
        {
            try
            {
                return BOTipoTope.ListarTipoTope(pTipTope, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTopeService", "ListarTipoTope", ex);
                return null;
            }
        }
    }
}
