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
    public class EstFlujoEfectivoServices
    {
        private EstFlujoEfectivoBusiness BOConEstFlujoEfectivo;
        private ExcepcionBusiness BOExcepcion;

        public EstFlujoEfectivoServices()
        {
            BOConEstFlujoEfectivo = new EstFlujoEfectivoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30305"; } }
        public string CodigoProgramaNIIF { get { return "210305"; } }

        public List<EstFlujoEfectivo> ListarDdllServices(Usuario pUsuario, int opcion) 
        {
            try
            {
                return BOConEstFlujoEfectivo.ListarDdllBusiness(pUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstFlujoEfectivoServices", "ListarDdllServices", ex);
                return null;
            }
        }

        public List<EstFlujoEfectivo> getListaReporGridvServices(Usuario pUsuario, DateTime fechaActual, DateTime fechaAnterior, int costoid, int pOpcion)
        {
            try
            {
                return BOConEstFlujoEfectivo.getListaReporGridv(pUsuario, fechaActual, fechaAnterior, costoid, pOpcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstFlujoEfectivoServices", "getListaReporGridvServices", ex);
                return null;
            }
        }
    }
}
