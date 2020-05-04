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
    public class GestionDiariaBusiness : GlobalData
    {

        private GestionDiariaData BOGestionDiariaData;

        public GestionDiariaBusiness()
        {
            BOGestionDiariaData = new GestionDiariaData();
        }



        public List<GestionDiaria> ReporteGestionDiaria(Usuario pUsuario)
        {
            try
            {
                return BOGestionDiariaData.ReporteGestionDiaria(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteAdicionalService", "ListarComponenteAdicional", ex);
                return null;
            }
        }
    }
}


