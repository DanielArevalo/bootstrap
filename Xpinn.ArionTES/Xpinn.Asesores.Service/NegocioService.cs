using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class NegocioService
    {
        private NegocioBusiness BONegocioProducto;
        private ExcepcionBusiness BOExcepcion;

        public NegocioService()
        {
            BONegocioProducto = new NegocioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110114"; } }

        public List<Negocio> ListarNegocios(Negocio pEntityNegocio, Usuario pUsuario)
        {
            try
            {
                return BONegocioProducto.ListarNegocios(pEntityNegocio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NegocioService", "ListarNegocios", ex);
                return null;
            }
        }
    }
}