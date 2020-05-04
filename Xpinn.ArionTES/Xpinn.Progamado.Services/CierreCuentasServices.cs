using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Programado.Business;
using Xpinn.Util;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Services
{
     [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CierreCuentasServices
    {
        private CierreCuentasBusiness BOLineasPro;
        private ExcepcionBusiness BOExcepcion;

        public CierreCuentasServices()
        {
            BOLineasPro = new CierreCuentasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }



        public List<CuentasProgramado> ListarProgramadoReporteCierre(DateTime pFechaCierre, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ListarProgramadoReporteCierre(pFechaCierre, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierreCuentasServices", "ListarProgramadoReporteCierre", ex);
                return null;
            }
        }

    }
}
