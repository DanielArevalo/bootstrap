using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;
 
namespace Xpinn.Nomina.Business
{

    public class ParametroslArlBussiness : GlobalBusiness
    {

        private ParametrosArlData DAParametrosArl;

        public ParametroslArlBussiness()
        {
            DAParametrosArl = new ParametrosArlData();
        }

        public ParametrosArl CrearParametrosArl(ParametrosArl pParametrosArl, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParametrosArl = DAParametrosArl.CrearParametrosArl(pParametrosArl, pusuario);

                    ts.Complete();

                }

                return pParametrosArl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroslArlBussines", "CrearParametrosArl", ex);
                return null;
            }
        }


        public ParametrosArl ModificarParametrosArl(ParametrosArl pParametrosArl, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pParametrosArl = DAParametrosArl.ModificarParametrosArl(pParametrosArl, pusuario);

                    ts.Complete();

                }

                return pParametrosArl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroslArlBussines", "ModificarParametrosArl", ex);
                return null;
            }
        }



        public ParametrosArl ConsultarParametrosArl(Int64 pId, Usuario pusuario)
        {
            try
            {
                ParametrosArl ParametrosArl = new ParametrosArl();
                ParametrosArl = DAParametrosArl.ConsultarParametrosArl(pId, pusuario);
                return ParametrosArl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametroslArlBussines", "ConsultarParametrosArl", ex);
                return null;
            }
        }

        
        public List<ParametrosArl> ListaParametrosArl(string pid, Usuario pusuario)
        {
            try
            {
                return DAParametrosArl.ListarParametrosArl(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCotizanteBusiness", "ListarParametrosArl", ex);
                return null;
            }
        }


    }
}
