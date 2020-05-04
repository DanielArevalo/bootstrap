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
    public class RangosFondoSolidaridadServices
    {

        private RangosFondoSolidaridadBussiness BORangosFondoSolidaridad;
        private ExcepcionBusiness BOExcepcion;

        public RangosFondoSolidaridadServices()
        {
            BORangosFondoSolidaridad = new RangosFondoSolidaridadBussiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250619"; } }
       

        public RangosFondoSolidaridad CrearRangosFondoSolidaridad(RangosFondoSolidaridad pRangosCajasCompensacion, Usuario pusuario)
        {
            try
            {
                pRangosCajasCompensacion = BORangosFondoSolidaridad.CrearRangosFondoSolidaridad(pRangosCajasCompensacion, pusuario);
                return pRangosCajasCompensacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosFondoSolidaridadServices", "CrearRangosFondoSolidaridad", ex);
                return null;
            }
        }
          

        public RangosFondoSolidaridad ModificarRangosFondoSolidaridad(RangosFondoSolidaridad pRangosCajasCompensacion, Usuario pusuario)
        {
            try
            {
                pRangosCajasCompensacion = BORangosFondoSolidaridad.ModificarRangosFondoSolidaridad(pRangosCajasCompensacion, pusuario);
                return pRangosCajasCompensacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosFondoSolidaridadServices", "ModificarRangosFondoSolidaridad", ex);
                return null;
            }
        }


        public RangosFondoSolidaridad ConsultarRangosFondoSolidaridad(Int64 pId, Usuario pusuario)
        {
            try
            {
                RangosFondoSolidaridad RangosFondoSolidaridad = new RangosFondoSolidaridad();
                RangosFondoSolidaridad = BORangosFondoSolidaridad.ConsultarRangosFondoSolidaridad(pId, pusuario);
                return RangosFondoSolidaridad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosFondoSolidaridadServices", "ConsultarRangosFondoSolidaridad", ex);
                return null;
            }
        }

   

        public List<RangosFondoSolidaridad> ListarRangossFondoSolidaridad(string pid, Usuario pusuario)
        {
            try
            {
                return BORangosFondoSolidaridad.ListarRangosFondoSolidaridad(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosFondoSolidaridadServices", "ListarFondoSolidaridad", ex);
                return null;
            }
        }



    }
}