using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para TipoMoneda
    /// </summary>
    public class TipoMonedaBusiness : GlobalData
    {
        private TipoMonedaData DATipoMoneda;

        /// <summary>
        /// Constructor del objeto de negocio para TipoMoneda
        /// </summary>
        public TipoMonedaBusiness()
        {
            DATipoMoneda = new TipoMonedaData();
        }

        /// <summary>
        /// Obtiene la lista de TipoMonedaes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoMonedaes obtenidos</returns>
        public List<TipoMoneda> ListarTipoMoneda(TipoMoneda pTipoMoneda, Usuario pUsuario)
        {
            try
            {
                return DATipoMoneda.ListarTipoMoneda(pTipoMoneda, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoMonedaBusiness", "ListarTipoMoneda", ex);
                return null;
            }
        }
    }
}
