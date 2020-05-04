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
    public class PrestamoPromedioOficinasService
    {


        private CarteraOficinasBusiness BOCarteraOficinasBusiness;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140404"; } }

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
        public PrestamoPromedioOficinasService()
        {
            BOCarteraOficinasBusiness = new CarteraOficinasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<PrestamoPromedioOficinas> consultarPrestamoPromedio(string fechaini, int cod_clasifica, Usuario pUsuario)
        {
            try
            {
                return BOCarteraOficinasBusiness.consultarPrestamoPromedio(fechaini, cod_clasifica, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasService", "consultarPrestamoPromedio", ex);
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




