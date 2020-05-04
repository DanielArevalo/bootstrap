using System; 
using System.Collections.Generic; 
using System.Text; 
using Xpinn.Util; 
using System.ServiceModel; 
using Xpinn.Interfaces.Entities; 
using Xpinn.Interfaces.Business; 
 
namespace Xpinn.Interfaces.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EnpactoServices
    {

        private EnpactoBusiness BOEnpactoBusiness;
        private ExcepcionBusiness BOExcepcion;

        public EnpactoServices()
        {
            BOEnpactoBusiness = new EnpactoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public Enpacto_Aud CrearEnpacto_Aud(Enpacto_Aud pEnpacto_Aud, Usuario pusuario)
        {
            try
            {
                pEnpacto_Aud = BOEnpactoBusiness.CrearEnpacto_Aud(pEnpacto_Aud, pusuario);
                return pEnpacto_Aud;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EnpactoServices", "CrearEnpacto_Aud", ex);
                return null;
            }
        }
    }
}