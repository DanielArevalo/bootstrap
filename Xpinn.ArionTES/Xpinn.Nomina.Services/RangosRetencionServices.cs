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
    public class RangosRetencionServices
    {

        private RangosRetencionBussiness BORangosRetencion;
        private ExcepcionBusiness BOExcepcion;

        public RangosRetencionServices()
        {
            BORangosRetencion = new RangosRetencionBussiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250621"; } }
       

        public RangosRetencion CrearRangosRetencion(RangosRetencion PRangosRetencion, Usuario pusuario)
        {
            try
            {
                PRangosRetencion = BORangosRetencion.CrearRangosRetencion(PRangosRetencion, pusuario);
                return PRangosRetencion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosRetencionServices", "CrearRangosRetencion", ex);
                return null;
            }
        }
          

        public RangosRetencion ModificarRangosRetencion(RangosRetencion PRangosRetencion, Usuario pusuario)
        {
            try
            {
                PRangosRetencion = BORangosRetencion.ModificarRangosRetencion(PRangosRetencion, pusuario);
                return PRangosRetencion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosRetencionServices", "ModificarRangosRetencion", ex);
                return null;
            }
        }


        public RangosRetencion ConsultarRangosRetencion(Int64 pId, Usuario pusuario)
        {
            try
            {
                RangosRetencion RangosRetencion = new RangosRetencion();
                RangosRetencion = BORangosRetencion.ConsultarRangosRetencion(pId, pusuario);
                return RangosRetencion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosRetencionServices", "ConsultaRangosRetencion", ex);
                return null;
            }
        }

   

        public List<RangosRetencion> ListarRangosRetencion(string pid, Usuario pusuario)
        {
            try
            {
                return BORangosRetencion.ListarRangosRetencion(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangosRetencionServices", "ListarRangosRetencion", ex);
                return null;
            }
        }



    }
}