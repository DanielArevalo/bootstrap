using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para Giro
    /// </summary>
    public class ImpresionMasivaBusiness : GlobalBusiness
    {
        private ImpresionMasivaData DAImpresion;

        /// <summary>
        /// Constructor del objeto de negocio para TransaccionCaja
        /// </summary>
        public ImpresionMasivaBusiness()
        {
            DAImpresion = new ImpresionMasivaData();
        }

        public List<Xpinn.Contabilidad.Entities.Comprobante> ListarComprobante(Xpinn.Contabilidad.Entities.Comprobante pComprobante, Usuario pUsuario, String filtro, String orden)
        {
            try
            {
                return DAImpresion.ListarComprobante(pComprobante, pUsuario, filtro, orden);             
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImpresionMasivaBusiness", "ListarComprobante", ex);
                return null;
            }
        }

    }
}
