using System; 
using System.Collections.Generic; 
using System.Text; 
using Xpinn.Util; 
using System.ServiceModel; 
using Xpinn.Contabilidad.Entities; 
using Xpinn.Contabilidad.Business;

namespace Xpinn.Contabilidad.Services 
{ 
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParametroCtasAuxiliosService
    {

        private ParametroCtasAuxiliosBusiness BOPar_Cue_LinAux;
        private ExcepcionBusiness BOExcepcion;
 
        public ParametroCtasAuxiliosService()
        {
            BOPar_Cue_LinAux = new ParametroCtasAuxiliosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
 
        public string CodigoPrograma { get { return "31008"; } }
 
        public Par_Cue_LinAux CrearPar_Cue_LinAux(Par_Cue_LinAux pPar_Cue_LinAux, Usuario pusuario)
        {
            try
            {
                pPar_Cue_LinAux = BOPar_Cue_LinAux.CrearPar_Cue_LinAux(pPar_Cue_LinAux, pusuario);
                return pPar_Cue_LinAux;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinAuxService", "CrearPar_Cue_LinAux", ex);
                return null;
            }
        }
 
 
        public Par_Cue_LinAux ModificarPar_Cue_LinAux(Par_Cue_LinAux pPar_Cue_LinAux, Usuario pusuario)
        {
            try
            {
                pPar_Cue_LinAux = BOPar_Cue_LinAux.ModificarPar_Cue_LinAux(pPar_Cue_LinAux, pusuario);
                return pPar_Cue_LinAux;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinAuxService", "ModificarPar_Cue_LinAux", ex);
                return null;
            }
        }
 
 
        public void EliminarPar_Cue_LinAux(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOPar_Cue_LinAux.EliminarPar_Cue_LinAux(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinAuxService", "EliminarPar_Cue_LinAux", ex);
            }
        }
 
 
        public Par_Cue_LinAux ConsultarPar_Cue_LinAux(Int64 pId, Usuario pusuario)
        {
            try
            {
                Par_Cue_LinAux Par_Cue_LinAux = new Par_Cue_LinAux();
                Par_Cue_LinAux = BOPar_Cue_LinAux.ConsultarPar_Cue_LinAux(pId, pusuario);
                return Par_Cue_LinAux;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinAuxService", "ConsultarPar_Cue_LinAux", ex);
                return null;
            }
        }
 
 
        public List<Par_Cue_LinAux> ListarPar_Cue_LinAux(Par_Cue_LinAux pPar_Cue_LinAux, Usuario pusuario)
        {
            try
            {
                return BOPar_Cue_LinAux.ListarPar_Cue_LinAux(pPar_Cue_LinAux, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Par_Cue_LinAuxService", "ListarPar_Cue_LinAux", ex);
                return null;
            }
        }
 
 
    }
}