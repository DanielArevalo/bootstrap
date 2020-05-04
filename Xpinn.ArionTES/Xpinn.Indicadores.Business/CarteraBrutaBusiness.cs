using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Indicadores.Data;
using Xpinn.Indicadores.Entities;

namespace Xpinn.Indicadores.Business
{
    public class CarteraBrutaBusiness : GlobalData
    {

        private CarteraBrutaData BOCarteraBrutaData;

        public CarteraBrutaBusiness()
        {
            BOCarteraBrutaData = new CarteraBrutaData();
        }


        public List<CarteraBruta> consultarCartera(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaData.consultarCartera( fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteAdicionalService", "ListarComponenteAdicional", ex);
                return null;
            }
        }
        public List<CarteraBruta> consultarCarteraVariacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOCarteraBrutaData.consultarCarteraVariacion(fechaini, fechafin,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteAdicionalService", "ListarComponenteAdicional", ex);
                return null;
            }
        }
    }
}


