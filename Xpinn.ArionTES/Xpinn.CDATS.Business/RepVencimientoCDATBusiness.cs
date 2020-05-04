using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Business
{

    public class RepVencimientoCDATBusiness : GlobalBusiness
    {

        private RepVencimientoCDATData DARepVencimientoCDAT;

        public RepVencimientoCDATBusiness()
        {
            DARepVencimientoCDAT = new RepVencimientoCDATData();
        }

        public RepVencimientoCDAT CrearRepVencimientoCDAT(RepVencimientoCDAT pRepVencimientoCDAT, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRepVencimientoCDAT = DARepVencimientoCDAT.CrearRepVencimientoCDAT(pRepVencimientoCDAT, pusuario);

                    ts.Complete();

                }

                return pRepVencimientoCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepVencimientoCDATBusiness", "CrearRepVencimientoCDAT", ex);
                return null;
            }
        }


        public RepVencimientoCDAT ModificarRepVencimientoCDAT(RepVencimientoCDAT pRepVencimientoCDAT, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRepVencimientoCDAT = DARepVencimientoCDAT.ModificarRepVencimientoCDAT(pRepVencimientoCDAT, pusuario);

                    ts.Complete();

                }

                return pRepVencimientoCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepVencimientoCDATBusiness", "ModificarRepVencimientoCDAT", ex);
                return null;
            }
        }


        public void EliminarRepVencimientoCDAT(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DARepVencimientoCDAT.EliminarRepVencimientoCDAT(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepVencimientoCDATBusiness", "EliminarRepVencimientoCDAT", ex);
            }
        }


        public RepVencimientoCDAT ConsultarRepVencimientoCDAT(Int64 pId, Usuario pusuario)
        {
            try
            {
                RepVencimientoCDAT RepVencimientoCDAT = new RepVencimientoCDAT();
                RepVencimientoCDAT = DARepVencimientoCDAT.ConsultarRepVencimientoCDAT(pId, pusuario);
                return RepVencimientoCDAT;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepVencimientoCDATBusiness", "ConsultarRepVencimientoCDAT", ex);
                return null;
            }
        }


        public List<RepVencimientoCDAT> ListarRepVencimientoCDAT(string[] pfiltro, Usuario pusuario)
        {
            try
            {
                return DARepVencimientoCDAT.ListarRepVencimientoCDAT(pfiltro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepVencimientoCDATBusiness", "ListarRepVencimientoCDAT", ex);
                return null;
            }
        }


    }
}
