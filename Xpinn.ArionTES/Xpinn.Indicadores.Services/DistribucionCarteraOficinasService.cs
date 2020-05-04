using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Indicadores.Business;
using Xpinn.Indicadores.Entities;

namespace Xpinn.Indicadores.Services
{
  [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DistribucionCarteraOficinasService
    {


      private CarteraOficinasBusiness BOCarteraOficinasBusiness;
      private ExcepcionBusiness BOExcepcion;

      public string CodigoPrograma { get { return "140405"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
      public DistribucionCarteraOficinasService()
        {
            BOCarteraOficinasBusiness = new CarteraOficinasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

      public List<DistribucionCarteraOficinas> DistribucionCarteraOficinas(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraOficinasBusiness.DistribucionCarteraOficinas(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasService", "DistribucionCarteraOficinas", ex);
                return null;
            }
        }


      public List<CarteraOficinas> consultarfecha(Usuario pUsuario)
        {
            try
            {
                return BOCarteraOficinasBusiness.consultarfecha("R", pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasService", "consultarfecha", ex);
                return null;
            }
        }
     
    }
}




