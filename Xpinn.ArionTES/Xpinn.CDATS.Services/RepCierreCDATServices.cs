using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.CDATS.Business;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class RepCierreCDATServices
    {
        private RepCierreCDATBusiness BOCdat;
        private ExcepcionBusiness BOExcepcion;

        public RepCierreCDATServices()
        {
            BOCdat = new RepCierreCDATBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }



        public List<AdministracionCDAT> ListarCdatReporteCierre(DateTime pFechaCierre, Usuario vUsuario)
        {
            try
            {
                return BOCdat.ListarCdatReporteCierre(pFechaCierre, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepCierreCDATServices", "ListarCdatReporteCierre", ex);
                return null;
            }
        }
    }
}