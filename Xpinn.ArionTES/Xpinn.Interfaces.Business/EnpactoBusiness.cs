using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Interfaces.Data;
using Xpinn.Interfaces.Entities;
 
namespace Xpinn.Interfaces.Business
{

    public class EnpactoBusiness : GlobalBusiness
    {

        private EnpactoData DAEnpactoData;

        public EnpactoBusiness()
        {
            DAEnpactoData = new EnpactoData();
        }

        public Enpacto_Aud CrearEnpacto_Aud(Enpacto_Aud pEnpacto_Aud, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEnpacto_Aud = DAEnpactoData.CrearEnpacto_Aud(pEnpacto_Aud, pusuario);

                    ts.Complete();

                }

                return pEnpacto_Aud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EnpactoBusiness", "CrearEnpacto_Aud", ex);
                return null;
            }
        }
    }
}