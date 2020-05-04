using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para CentroCosto
    /// </summary>
    public class CentroCostoBusiness:GlobalData 
    {
        private CentroCostoData DACentroCosto;

        /// <summary>
        /// Constructor del objeto de negocio para CentroCosto
        /// </summary>
        public CentroCostoBusiness()
        {
            DACentroCosto = new CentroCostoData();
        }

        
        /// <summary>
        /// Obtiene la lista de Centros de Costos dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Centro de Costos obtenidos</returns>
        public List<CentroCosto> ListarCentroCosto(CentroCosto pCentroCosto, Usuario pUsuario)
        {
            try
            {
                return DACentroCosto.ListarCentroCosto(pCentroCosto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroCostoBusiness", "ListarCentroCosto", ex);
                return null;
            }
        }
    }
}
