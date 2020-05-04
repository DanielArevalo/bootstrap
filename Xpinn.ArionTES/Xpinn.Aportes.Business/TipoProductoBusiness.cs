using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Business
{
    /// <summary>
    /// Objeto de negocio para Aporte
    /// </summary>
    public class TipoProductoBusiness : GlobalBusiness
    {
        private TipoProductoData DATipoProducto;

        /// <summary>
        /// Constructor del objeto de negocio para Aporte
        /// </summary>
        public TipoProductoBusiness()
        {
            DATipoProducto = new TipoProductoData();
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pAporte">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de LineaAporte obtenidos</returns>
        public List<TipoProducto> ListarTipoProducto(TipoProducto pTipoProducto, Usuario pUsuario)
        {
            try
            {
                return DATipoProducto.ListarTipoProducto(pTipoProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoProductoBusiness", "ListarTipoProducto", ex);
                return null;
            }
        }
        public List<TipoProducto> ListarTipoTran(TipoProducto pTipoProducto, Usuario pUsuario)
        {
            try
            {
                return DATipoProducto.ListarTipoTran(pTipoProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoProductoBusiness", "ListarTipoTran", ex);
                return null;
            }
        }


    }
}
