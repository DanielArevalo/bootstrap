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
    public class PagoExtraordBusiness : GlobalBusiness  
    {
         private PagoExtraordData DAPagoExtraord;

        /// <summary>
        /// Constructor del objeto de negocio para LineaObligacion
        /// </summary>
        public PagoExtraordBusiness()
        {
            DAPagoExtraord = new PagoExtraordData();
        }

        /// <summary>
        /// Obtiene la lista de PagoExtraords dados unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PagoExtraords obtenidos</returns>
        public List<PagoExtraord> ListarPagoExtraord(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAPagoExtraord.ListarPagoExtraord(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagoExtraordBusiness", "ListarPagoExtraord", ex);
                return null;
            }
        }

    }
}
