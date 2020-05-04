using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class TipoGarantiaBusiness : GlobalData
    {
        private TipoGarantiaData DATipoGarantia;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public TipoGarantiaBusiness()
        {
            DATipoGarantia = new TipoGarantiaData();
        }

        /// <summary>
        /// Obtiene la lista de Tipos Garantia
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Tipos de Garantia obtenidas</returns>        
        public List<TipoGarantias> ListarTipoGarantia(TipoGarantias pTipoGarantia, Usuario pUsuario)
        {
            try
            {
                return DATipoGarantia.ListarTipoGarantia(pTipoGarantia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoGarantiaBusiness", "ListarTipoGarantia", ex);
                return null;
            }
        }
    }
}
