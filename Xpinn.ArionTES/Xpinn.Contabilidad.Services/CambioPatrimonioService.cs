using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CambioPatrimonioService
    {
        private CambioPatrimonioBusiness BOCambioPatrimonio;
        private ExcepcionBusiness BOExcepcion;

        public CambioPatrimonioService() 
        {
            BOCambioPatrimonio = new CambioPatrimonioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30306"; } }
        public string CodigoProgramaNIIF { get { return "210306"; } }

        public List<CambioPatrimonio> getListaComboServices(Usuario pUsuario) 
        {
            try
            {
                return BOCambioPatrimonio.getListaComboBusiness(pUsuario);
            }
            catch
            {
                BOExcepcion.Throw("CambioPatrimoniServices","getListaComboServices",new Exception());
                return null;
            }
        }
        public List<CambioPatrimonio> getListaCambioPatrimonioServices(Usuario pUsuario, int pOpcion)
        {
            try
            {
                return BOCambioPatrimonio.getListaCambioPatrimonioBusines(pUsuario, pOpcion);
            }
            catch
            {
                BOExcepcion.Throw("getListaCambioPatrimonioServices", "getListaCambioPatrimonioServices", new Exception());
                return null;
            }
        }

    }

}
