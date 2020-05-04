using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;
using System.Transactions;


namespace Xpinn.CDATS.Business
{
    public class AnulacionCDATBusiness : GlobalBusiness
    {
        AnulacionCDATData BAAnula;

        public AnulacionCDATBusiness()
        {
            BAAnula = new AnulacionCDATData();
        }


        public Cdat ModificarCDATAnulacion(Cdat pCdat, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCdat = BAAnula.ModificarCDATAnulacion(pCdat, vUsuario);
                    
                    ts.Complete();
                }
                return pCdat;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AnulacionCDATBusiness", "ModificarCDATAnulacion", ex);
                return null;
            }
        }


    }
}
