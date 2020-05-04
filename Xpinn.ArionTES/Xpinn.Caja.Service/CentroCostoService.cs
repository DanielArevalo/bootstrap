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
    /// Servicio para CentroCosto
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CentroCostoService
    {
         private CentroCostoBusiness BOCentroCosto;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CentroCosto
        /// </summary>
        public CentroCostoService()
        {
            BOCentroCosto = new CentroCostoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoCentroCosto;

        /// <summary>
        /// Obtiene la lista de Centro de Costos dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Centro de Costos obtenidos</returns>
        public List<CentroCosto> ListarCentroCosto(CentroCosto pCentroCosto, Usuario pUsuario)
        {
            try
            {
                return BOCentroCosto.ListarCentroCosto(pCentroCosto, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroCostoService", "ListarCentroCosto", ex);
                return null;
            }
        }
    }
}
