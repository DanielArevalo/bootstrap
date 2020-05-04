using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Data;
using System.Transactions;
using System.Data;
using Xpinn.Util;

namespace Xpinn.NIIF.Business
{
    public class TransicionRangosNIFBusiness
    {
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();
        /// Objeto de negocio para Credito
        /// 
        private TransicionRangosNIFData BATrans;

        public TransicionRangosNIFBusiness()
        {
            BATrans = new TransicionRangosNIFData();
        }
         

        public TransicionRangosNIF ModificarTransicionRangos(TransicionRangosNIF pRango, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pRango.lstRangos != null)
                    {
                        int num = 0;
                        int grab = 0;
                        int modi = 0;
                        foreach (TransicionRangosNIF rFila in pRango.lstRangos)
                        {
                            TransicionRangosNIF nTransi = new TransicionRangosNIF();
                            if (rFila.codrango <= 0 || rFila.codrango == null)
                            {
                                nTransi = BATrans.CrearTransicionRangos(rFila, vUsuario);
                                grab++;
                            }
                            else
                            {
                                nTransi = BATrans.ModificarTransicionRangos(rFila, vUsuario);
                                modi++;
                            }
                            num += 1;
                        }
                        pRango.num_grab = grab;
                        pRango.num_modi = modi;
                    }

                    ts.Complete();
                }

                return pRango;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionRangosNIFBusiness", "ModificarTransicionRangos", ex);
                return null;
            }

        }


        public List<TransicionRangosNIF> ListarTransicionRango(TransicionRangosNIF pRango, Usuario vUsuario)
        {
            try
            {
                return BATrans.ListarTransicionRango(pRango, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionRangosNIFBusiness", "ListarTransicionRango", ex);
                return null;
            }
        }


        public void EliminarTransicionRango(Int32 pId, Usuario vUsuario)
        {

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BATrans.EliminarTransicionRango(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionRangosNIFBusiness", "EliminarTransicionRango", ex);
            }

        }

    }
}
