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
    public class EvolucionDesembolsoBusiness : GlobalData
    {

        private EvolucionDesembolsoData BOEvolucionDesembolsoData;

        public EvolucionDesembolsoBusiness()
        {
            BOEvolucionDesembolsoData = new EvolucionDesembolsoData();
        }


        public List<EvolucionDesembolsos> consultarDesembolso(string fechaini, string fechafin, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOEvolucionDesembolsoData.consultarDesembolso(fechaini, fechafin,pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EvolucionDesembolsoBusiness", "consultarDesembolso", ex);
                return null;
            }
        }

        public List<EvolucionDesembolsoOficinas> consultarDesembolsoOficina(string fechaini, string fechafin, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOEvolucionDesembolsoData.consultarDesembolsoOficina(fechaini, fechafin,pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EvolucionDesembolsoBusiness", "consultarDesembolso", ex);
                return null;
            }
        }

       

    }
}


