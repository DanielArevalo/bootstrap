using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Business
{

    public class ParametroCobroPrejuridicoBusiness : GlobalBusiness
    {

        private ParametroCobroPrejuridicoData DAParametroCobroPrejuridico;

        public ParametroCobroPrejuridicoBusiness()
        {
            DAParametroCobroPrejuridico = new ParametroCobroPrejuridicoData();
        }

        public ParametroCobroPrejuridico CrearParametroCobroPrejuridico(ParametroCobroPrejuridico pParametroCobroPrejuridico, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParametroCobroPrejuridico = DAParametroCobroPrejuridico.CrearParametroCobroPrejuridico(pParametroCobroPrejuridico, pusuario);

                    ts.Complete();

                }

                return pParametroCobroPrejuridico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCobroPrejuridicoBusiness", "CrearParametroCobroPrejuridico", ex);
                return null;
            }
        }


        public ParametroCobroPrejuridico ModificarParametroCobroPrejuridico(ParametroCobroPrejuridico pParametroCobroPrejuridico, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParametroCobroPrejuridico = DAParametroCobroPrejuridico.ModificarParametroCobroPrejuridico(pParametroCobroPrejuridico, pusuario);

                    ts.Complete();

                }

                return pParametroCobroPrejuridico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCobroPrejuridicoBusiness", "ModificarParametroCobroPrejuridico", ex);
                return null;
            }
        }


        public void EliminarParametroCobroPrejuridico(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAParametroCobroPrejuridico.EliminarParametroCobroPrejuridico(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCobroPrejuridicoBusiness", "EliminarParametroCobroPrejuridico", ex);
            }
        }


        public ParametroCobroPrejuridico ConsultarParametroCobroPrejuridico(Int64 pId, Usuario pusuario)
        {
            try
            {
                ParametroCobroPrejuridico ParametroCobroPrejuridico = new ParametroCobroPrejuridico();
                ParametroCobroPrejuridico = DAParametroCobroPrejuridico.ConsultarParametroCobroPrejuridico(pId, pusuario);
                return ParametroCobroPrejuridico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCobroPrejuridicoBusiness", "ConsultarParametroCobroPrejuridico", ex);
                return null;
            }
        }


        public List<ParametroCobroPrejuridico> ListarParametroCobroPrejuridico(ParametroCobroPrejuridico pParametroCobroPrejuridico, Usuario pusuario)
        {
            try
            {
                return DAParametroCobroPrejuridico.ListarParametroCobroPrejuridico(pParametroCobroPrejuridico, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroCobroPrejuridicoBusiness", "ListarParametroCobroPrejuridico", ex);
                return null;
            }
        }


    }
}
