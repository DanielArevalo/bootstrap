using System; 
using System.Collections.Generic; 
using System.Text; 
using Xpinn.Util; 
using System.ServiceModel; 
using Xpinn.Nomina.Entities; 
using Xpinn.Nomina.Business; 
 
namespace Xpinn.Nomina.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParametrosArlService
    {

        private ParametroslArlBussiness BOParamametrosArl;
        private ExcepcionBusiness BOExcepcion;

        public ParametrosArlService()
        {
            BOParamametrosArl = new ParametroslArlBussiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250618"; } }
       

        public ParametrosArl CrearParametrosArl(ParametrosArl pParametrosArl, Usuario pusuario)
        {
            try
            {
                pParametrosArl = BOParamametrosArl.CrearParametrosArl(pParametrosArl, pusuario);
                return pParametrosArl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosArlService", "CrearParametrosArl", ex);
                return null;
            }
        }
          

        public ParametrosArl ModificarParametrosArl(ParametrosArl pParametrosArl, Usuario pusuario)
        {
            try
            {
                pParametrosArl = BOParamametrosArl.ModificarParametrosArl(pParametrosArl, pusuario);
                return pParametrosArl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosArlService", "ModificarParametrosArl", ex);
                return null;
            }
        }


        public ParametrosArl ConsultarParametrosArl(Int64 pId, Usuario pusuario)
        {
            try
            {
                ParametrosArl ParametrosArl = new ParametrosArl();
                ParametrosArl = BOParamametrosArl.ConsultarParametrosArl(pId, pusuario);
                return ParametrosArl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosArlService", "ConsultarParametrosArl", ex);
                return null;
            }
        }

   

        public List<ParametrosArl> ListarParametrosArl(string pid, Usuario pusuario)
        {
            try
            {
                return BOParamametrosArl.ListaParametrosArl(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosArlService", "ListarParametrosArl", ex);
                return null;
            }
        }



    }
}