using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using System.Web;


namespace Xpinn.Tesoreria.Services
{
    public class TrasladoPagaduriasServices
    {
        private TrasladoPagaduriasBusiness BOTraslado;
        private ExcepcionBusiness BOExcepcion;


        public TrasladoPagaduriasServices()
        {
            BOTraslado = new TrasladoPagaduriasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "180109"; } }

        public TrasladoPagadurias ModificarTrasladoPagadurias(TrasladoPagadurias pTrasladoPagadurias, Usuario pUsuario)
        {
            try
            {
                return BOTraslado.ModificarTrasladoPagadurias(pTrasladoPagadurias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoPagaduriasServices", "ModificarTrasladoPagadurias", ex);
                return null;
            }
        }

        public TrasladoPagadurias ConsultarTrasladoPagadurias(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTraslado.ConsultarTrasladoPagadurias(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoPagaduriasServices", "ConsultarTrasladoPagadurias", ex);
                return null;
            }
        }

    }
}
