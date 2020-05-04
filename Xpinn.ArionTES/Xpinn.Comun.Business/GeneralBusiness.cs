using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;
 
namespace Xpinn.Comun.Business
{

    public class GeneralBusiness : GlobalBusiness
    {

        private GeneralData DAGeneral;

        public GeneralBusiness()
        {
            DAGeneral = new GeneralData();
        }


        public General ConsultarGeneral(Int64 pId, Usuario pusuario)
        {
            try
            {
                General General = new General();
                General = DAGeneral.ConsultarGeneral(pId, pusuario);
                return General;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeneralBusiness", "ConsultarGeneral", ex);
                return null;
            }
        }



        public Int64 SMLVGeneral(Usuario pusuario)
        {
            try
            {
      
                return DAGeneral.SMLVGeneral(pusuario);
                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeneralBusiness", "SMLVGeneral", ex);
                return 0;
            }
        }



    }
}