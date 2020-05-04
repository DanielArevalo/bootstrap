using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    public class ComprobantePagoExtractoBusiness : GlobalData
    {
        private ComprobantePagoExtractoData DAComprobantePagoExtracto;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public ComprobantePagoExtractoBusiness()
        {
            DAComprobantePagoExtracto = new ComprobantePagoExtractoData();
        }

        /// <summary>
        /// Obtiene la lista de ComprobantePagoExtractos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de ComprobantePagoExtractos obtenidas</returns>        
        public List<ComprobantePagoExtracto> ListarComprobantePagoExtractos(ComprobantePagoExtracto pComprobantePagoExtracto,Usuario pUsuario)
        {
            try
            {
                return DAComprobantePagoExtracto.ListarComprobantePagoExtractos(pComprobantePagoExtracto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComprobantePagoExtractoBusiness", "ListarComprobantePagoExtractos", ex);
                return null;
            }
        }
    }
}