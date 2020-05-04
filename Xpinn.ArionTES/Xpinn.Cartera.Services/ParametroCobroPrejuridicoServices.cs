using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;

namespace Xpinn.Cartera.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParametroCobroPrejuridicoService
    {

        private ParametroCobroPrejuridicoBusiness BOParametroCobroPrejuridico;
        private ExcepcionBusiness BOExcepcion;

        public ParametroCobroPrejuridicoService()
        {
            BOParametroCobroPrejuridico = new ParametroCobroPrejuridicoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "60404"; } }

        public ParametroCobroPrejuridico CrearParametroCobroPrejuridico(ParametroCobroPrejuridico pParametroCobroPrejuridico, Usuario pusuario)
        {
            try
            {
                pParametroCobroPrejuridico = BOParametroCobroPrejuridico.CrearParametroCobroPrejuridico(pParametroCobroPrejuridico, pusuario);
                return pParametroCobroPrejuridico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCobroPrejuridicoService", "CrearParametroCobroPrejuridico", ex);
                return null;
            }
        }


        public ParametroCobroPrejuridico ModificarParametroCobroPrejuridico(ParametroCobroPrejuridico pParametroCobroPrejuridico, Usuario pusuario)
        {
            try
            {
                pParametroCobroPrejuridico = BOParametroCobroPrejuridico.ModificarParametroCobroPrejuridico(pParametroCobroPrejuridico, pusuario);
                return pParametroCobroPrejuridico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCobroPrejuridicoService", "ModificarParametroCobroPrejuridico", ex);
                return null;
            }
        }


        public void EliminarParametroCobroPrejuridico(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOParametroCobroPrejuridico.EliminarParametroCobroPrejuridico(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCobroPrejuridicoService", "EliminarParametroCobroPrejuridico", ex);
            }
        }


        public ParametroCobroPrejuridico ConsultarParametroCobroPrejuridico(Int64 pId, Usuario pusuario)
        {
            try
            {
                ParametroCobroPrejuridico ParametroCobroPrejuridico = new ParametroCobroPrejuridico();
                ParametroCobroPrejuridico = BOParametroCobroPrejuridico.ConsultarParametroCobroPrejuridico(pId, pusuario);
                return ParametroCobroPrejuridico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCobroPrejuridicoService", "ConsultarParametroCobroPrejuridico", ex);
                return null;
            }
        }


        public List<ParametroCobroPrejuridico> ListarParametroCobroPrejuridico(ParametroCobroPrejuridico pParametroCobroPrejuridico, Usuario pusuario)
        {
            try
            {
                return BOParametroCobroPrejuridico.ListarParametroCobroPrejuridico(pParametroCobroPrejuridico, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCobroPrejuridicoService", "ListarParametroCobroPrejuridico", ex);
                return null;
            }
        }


    }
}