using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class RetroactivoBusiness : GlobalBusiness
    {

        private RetroactivoData DARetroactivo;

        public RetroactivoBusiness()
        {
            DARetroactivo = new RetroactivoData();
        }

        public Retroactivo CrearRetroactivo(Retroactivo pRetroactivo, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRetroactivo = DARetroactivo.CrearRetroactivo(pRetroactivo, pusuario);

                    ts.Complete();

                }

                return pRetroactivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RetroactivoBusiness", "CrearRetroactivo", ex);
                return null;
            }
        }


        public Retroactivo ModificarRetroactivo(Retroactivo pRetroactivo, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pRetroactivo = DARetroactivo.ModificarRetroactivo(pRetroactivo, pusuario);

                    ts.Complete();

                }

                return pRetroactivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RetroactivoBusiness", "ModificarRetroactivo", ex);
                return null;
            }
        }


        public void EliminarRetroactivo(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DARetroactivo.EliminarRetroactivo(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RetroactivoBusiness", "EliminarRetroactivo", ex);
            }
        }


        public Retroactivo ConsultarRetroactivo(Int64 pId, Usuario pusuario)
        {
            try
            {
                Retroactivo Retroactivo = new Retroactivo();
                Retroactivo = DARetroactivo.ConsultarRetroactivo(pId, pusuario);
                return Retroactivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RetroactivoBusiness", "ConsultarRetroactivo", ex);
                return null;
            }
        }


        public List<Retroactivo> ListarRetroactivo(string filtro, Usuario pusuario)
        {
            try
            {
                return DARetroactivo.ListarRetroactivo(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RetroactivoBusiness", "ListarRetroactivo", ex);
                return null;
            }
        }


    }
}