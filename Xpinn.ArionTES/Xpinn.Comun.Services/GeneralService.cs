using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Business;

namespace Xpinn.Comun.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class GeneralService
    {

        private GeneralBusiness BOGeneral;
        private ExcepcionBusiness BOExcepcion;

        public GeneralService()
        {
            BOGeneral = new GeneralBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

   

        public General ConsultarGeneral(Int64 pId, Usuario pusuario)
        {
            try
            {
                General General = new General();
                General = BOGeneral.ConsultarGeneral(pId, pusuario);
                return General;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeneralService", "ConsultarGeneral", ex);
                return null;
            }
        }

        public Int64 SMLVGeneral(Usuario pusuario)
        {
            try
            {
                return BOGeneral.SMLVGeneral(pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GeneralService", "ConsultarGeneral", ex);
                return 0;
            }
        }

    }

}