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
    public class TipoCotizanteService
    {

        private TipoCotizanteBusiness BOContratacion;
        private ExcepcionBusiness BOExcepcion;

        public TipoCotizanteService()
        {
            BOContratacion = new TipoCotizanteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250617"; } }
       

        public TipoCotizante CrearTipoCotizante(TipoCotizante pTipoCotizante, Usuario pusuario)
        {
            try
            {
                pTipoCotizante = BOContratacion.CrearTipoCotizante(pTipoCotizante, pusuario);
                return pTipoCotizante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCotizanteService", "CrearTipoCotizante", ex);
                return null;
            }
        }
          

        public TipoCotizante ModificarTipoCotizante(TipoCotizante pTipoCotizante, Usuario pusuario)
        {
            try
            {
                pTipoCotizante = BOContratacion.ModificarTipoCotizante(pTipoCotizante, pusuario);
                return pTipoCotizante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCotizanteService", "ModificarTipoCotizante", ex);
                return null;
            }
        }


        public TipoCotizante ConsultarTipoCotizante(Int64 pId, Usuario pusuario)
        {
            try
            {
                TipoCotizante TipoCotizante = new TipoCotizante();
                TipoCotizante = BOContratacion.ConsultarTipoCotizante(pId, pusuario);
                return TipoCotizante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCotizanteService", "ConsultarTipoCotizante", ex);
                return null;
            }
        }

   

        public List<TipoCotizante> ListarTipoCotizante(string pid, Usuario pusuario)
        {
            try
            {
                return BOContratacion.ListarTipoCotizante(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCotizanteService", "ListarTipoCotizante", ex);
                return null;
            }
        }



    }
}