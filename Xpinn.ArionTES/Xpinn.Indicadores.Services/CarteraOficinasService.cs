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
    public class CarteraOficinasService
    {


      private CarteraOficinasBusiness BOCarteraOficinasBusiness;
      private ExcepcionBusiness BOExcepcion;

      public string CodigoPrograma { get { return "140401"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
      public CarteraOficinasService()
        {
            BOCarteraOficinasBusiness = new CarteraOficinasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

      public List<CarteraOficinas> consultarCarteraOficinas(string fechaini, int tipo, Usuario pUsuario)
        {
            try
            {
                return BOCarteraOficinasBusiness.consultarCarteraOficinas(fechaini, tipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasService", "consultarCarteraOficinas", ex);
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

        public List<CarteraOficinas> consultarfecha(string pTipo, Usuario pUsuario)
        {
            try
            {
                return BOCarteraOficinasBusiness.consultarfecha(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasService", "consultarfecha", ex);
                return null;
            }
        }
     
    }
}




