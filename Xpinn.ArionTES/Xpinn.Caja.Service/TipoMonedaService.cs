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
    /// Servicio para TipoMoneda
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoMonedaService
    {
          private TipoMonedaBusiness BOTipoMoneda;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoMoneda
        /// </summary>
        public TipoMonedaService()
        {
            BOTipoMoneda = new TipoMonedaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoTipoMoneda;

        /// <summary>
        /// Obtiene la lista de TipoMonedaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoMonedaes obtenidos</returns>
        public List<TipoMoneda> ListarTipoMoneda(TipoMoneda pTipoMoneda, Usuario pUsuario)
        {
            try
            {
                return BOTipoMoneda.ListarTipoMoneda(pTipoMoneda, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMonedaService", "ListarTipoMoneda", ex);
                return null;
            }
        }
    }
}
