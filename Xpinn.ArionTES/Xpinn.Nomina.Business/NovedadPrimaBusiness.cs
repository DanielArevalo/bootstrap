using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class NovedadPrimaBusiness : GlobalBusiness
    {

        private NovedadPrimaData DANovedadPrima;

        public NovedadPrimaBusiness()
        {
            DANovedadPrima = new NovedadPrimaData();
        }

        public NovedadPrima CrearNovedadPrima(NovedadPrima pNovedadPrima, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pNovedadPrima = DANovedadPrima.CrearNovedadPrima(pNovedadPrima, pusuario);

                    ts.Complete();
                }

                return pNovedadPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NovedadPrimaBusiness", "CrearNovedadPrima", ex);
                return null;
            }
        }


        public NovedadPrima ModificarNovedadPrima(NovedadPrima pNovedadPrima, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pNovedadPrima = DANovedadPrima.ModificarNovedadPrima(pNovedadPrima, pusuario);

                    ts.Complete();

                }

                return pNovedadPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NovedadPrimaBusiness", "ModificarNovedadPrima", ex);
                return null;
            }
        }


        public void EliminarNovedadPrima(long pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DANovedadPrima.EliminarNovedadPrima(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NovedadPrimaBusiness", "EliminarNovedadPrima", ex);
            }
        }


        public NovedadPrima ConsultarNovedadPrima(long pId, Usuario pusuario)
        {
            try
            {
                NovedadPrima NovedadPrima = new NovedadPrima();
                NovedadPrima = DANovedadPrima.ConsultarNovedadPrima(pId, pusuario);
                return NovedadPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NovedadPrimaBusiness", "ConsultarNovedadPrima", ex);
                return null;
            }
        }


        public List<NovedadPrima> ListarNovedadPrima(string filtro, Usuario pusuario)
        {
            try
            {
                return DANovedadPrima.ListarNovedadPrima(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NovedadPrimaBusiness", "ListarNovedadPrima", ex);
                return null;
            }
        }


    }
}