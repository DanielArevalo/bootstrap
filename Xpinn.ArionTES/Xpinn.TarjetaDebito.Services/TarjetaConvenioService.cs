using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Business;

namespace Xpinn.TarjetaDebito.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TarjetaConvenioService
    {

        private TarjetaConvenioBusiness BOTarjetaConvenio;
        private ExcepcionBusiness BOExcepcion;

        public TarjetaConvenioService()
        {
            BOTarjetaConvenio = new TarjetaConvenioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220502"; } }

        public TarjetaConvenio CrearTarjetaConvenio(TarjetaConvenio pTarjetaConvenio, Usuario pusuario)
        {
            try
            {
                pTarjetaConvenio = BOTarjetaConvenio.CrearTarjetaConvenio(pTarjetaConvenio, pusuario);
                return pTarjetaConvenio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioService", "CrearTarjetaConvenio", ex);
                return null;
            }
        }


        public TarjetaConvenio ModificarTarjetaConvenio(TarjetaConvenio pTarjetaConvenio, Usuario pusuario)
        {
            try
            {
                pTarjetaConvenio = BOTarjetaConvenio.ModificarTarjetaConvenio(pTarjetaConvenio, pusuario);
                return pTarjetaConvenio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioService", "ModificarTarjetaConvenio", ex);
                return null;
            }
        }


        public void EliminarTarjetaConvenio(Int32 pId, Usuario pusuario)
        {
            try
            {
                BOTarjetaConvenio.EliminarTarjetaConvenio(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioService", "EliminarTarjetaConvenio", ex);
            }
        }


        public TarjetaConvenio ConsultarTarjetaConvenio(Int32 pId, Usuario pusuario)
        {
            try
            {
                TarjetaConvenio TarjetaConvenio = new TarjetaConvenio();
                TarjetaConvenio = BOTarjetaConvenio.ConsultarTarjetaConvenio(pId, pusuario);
                return TarjetaConvenio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioService", "ConsultarTarjetaConvenio", ex);
                return null;
            }
        }


        public List<TarjetaConvenio> ListarTarjetaConvenio(TarjetaConvenio pTarjetaConvenio, Usuario pusuario)
        {
            try
            {
                return BOTarjetaConvenio.ListarTarjetaConvenio(pTarjetaConvenio, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioService", "ListarTarjetaConvenio", ex);
                return null;
            }
        }

        public List<Email> ListaEmailAlerta(Usuario pusuario)
        {
            try
            {                
                return BOTarjetaConvenio.ListaEmailAlerta(pusuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaConvenioService", "ListaEmailAlerta", ex);
                return null;
            }
        }


    }
}
