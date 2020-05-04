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
    public class ConveniosBusiness : GlobalData
    {
        private ConveniosData DAConvenio;

        /// <summary>
        /// Constructor del objeto de negocio para TipoMoneda
        /// </summary>
        public ConveniosBusiness()
        {
            DAConvenio = new ConveniosData();
        }

        public ConveniosRecaudo ConsultarConvenioRecaudo(Int64 pId, Usuario vUsuario)
        {
            try
            {
                ConveniosRecaudo vConvenios = new ConveniosRecaudo();

                vConvenios = DAConvenio.ConsultarConvenioRecaudo(pId, vUsuario);
                return vConvenios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConveniosBusiness", "ConsultarConvenioRecaudo", ex);
                return null;
            }
        }


    }
}
