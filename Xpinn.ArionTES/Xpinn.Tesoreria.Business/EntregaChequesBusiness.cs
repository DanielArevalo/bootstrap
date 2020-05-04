using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para AreasCaj
    /// </summary>
    public class EntregaChequesBusiness : GlobalBusiness
    {
        private EntregaChequesData DAEntrega;

        /// <summary>
        /// Constructor del objeto de negocio para AreasCaj
        /// </summary>
        public EntregaChequesBusiness()
        {
            DAEntrega = new EntregaChequesData();
        }

        public EntregaCheques CrearEntregaCheque(EntregaCheques pEntregaCheque, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntregaCheque = DAEntrega.CrearEntregaCheque(pEntregaCheque, vUsuario);

                    ts.Complete();
                }
                return pEntregaCheque;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EntregaChequesBusiness", "CrearEntregaCheque", ex);
                return null;
            }
        }

        public List<EntregaCheques> ListarEntregaCheques(EntregaCheques pEntregaCheques, Usuario vUsuario)
        {
            try
            {
                return DAEntrega.ListarEntregaCheques(pEntregaCheques, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EntregaChequesBusiness", "ListarEntregaCheques", ex);
                return null;
            }
        }

    }

}