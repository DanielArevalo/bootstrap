using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Obligaciones.Data;
using Xpinn.Obligaciones.Entities;
using System.Web.UI.WebControls;


namespace Xpinn.Obligaciones.Business
{
    public class PeriodicidadCuotaBusiness : GlobalBusiness
    {
         private PeriodicidadCuotaData DAPeriodicidadCuota;

        /// <summary>
        /// Constructor del objeto de negocio para PeriodicidadCuota
        /// </summary>
        public PeriodicidadCuotaBusiness()
        {
            DAPeriodicidadCuota = new PeriodicidadCuotaData();
        }

         /// <summary>
        /// Obtiene la lista de PeriodicidadCuota dados unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PeriodicidadCuota obtenidos</returns>
        public List<PeriodicidadCuota> ListarPeriodicidadCuota(PeriodicidadCuota pTipLiq, Usuario pUsuario)
        {
            try
            {
                return DAPeriodicidadCuota.ListarPeriodicidadCuota(pTipLiq, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadCuotaBusiness", "ListarPeriodicidadCuota", ex);
                return null;
            }
        }

    }
}
