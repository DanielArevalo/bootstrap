using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.TarjetaDebito.Data;
using Xpinn.TarjetaDebito.Entities;

namespace Xpinn.TarjetaDebito.Business
{

    public class TarjetaConvenioBusiness : GlobalBusiness
    {

        private TarjetaConvenioData DATarjetaConvenio;

        public TarjetaConvenioBusiness()
        {
            DATarjetaConvenio = new TarjetaConvenioData();
        }

        public TarjetaConvenio CrearTarjetaConvenio(TarjetaConvenio pTarjetaConvenio, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTarjetaConvenio = DATarjetaConvenio.CrearTarjetaConvenio(pTarjetaConvenio, pusuario);

                    ts.Complete();

                }

                return pTarjetaConvenio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioBusiness", "CrearTarjetaConvenio", ex);
                return null;
            }
        }


        public TarjetaConvenio ModificarTarjetaConvenio(TarjetaConvenio pTarjetaConvenio, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTarjetaConvenio = DATarjetaConvenio.ModificarTarjetaConvenio(pTarjetaConvenio, pusuario);

                    ts.Complete();

                }

                return pTarjetaConvenio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioBusiness", "ModificarTarjetaConvenio", ex);
                return null;
            }
        }


        public void EliminarTarjetaConvenio(Int32 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATarjetaConvenio.EliminarTarjetaConvenio(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioBusiness", "EliminarTarjetaConvenio", ex);
            }
        }


        public TarjetaConvenio ConsultarTarjetaConvenio(Int32 pId, Usuario pusuario)
        {
            try
            {
                TarjetaConvenio TarjetaConvenio = new TarjetaConvenio();
                TarjetaConvenio = DATarjetaConvenio.ConsultarTarjetaConvenio(pId, pusuario);
                return TarjetaConvenio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioBusiness", "ConsultarTarjetaConvenio", ex);
                return null;
            }
        }


        public List<TarjetaConvenio> ListarTarjetaConvenio(TarjetaConvenio pTarjetaConvenio, Usuario pusuario)
        {
            try
            {
                return DATarjetaConvenio.ListarTarjetaConvenio(pTarjetaConvenio, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioBusiness", "ListarTarjetaConvenio", ex);
                return null;
            }
        }

 
        public List<Email> ListaEmailAlerta(Usuario pusuario)
        {
            try
            {
                return DATarjetaConvenio.ListaEmailAlerta(pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioService", "ListaEmailAlerta", ex);
                return null;
            }
        }


    }

}
