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
    public class GestionRiesgoBusiness : GlobalData
    {

        private GestionRiesgoData BOGestionRiesgoData;

        public GestionRiesgoBusiness()
        {
            BOGestionRiesgoData = new GestionRiesgoData();
        }


        public List<GestionRiesgo> ListadoSegmentoCredito(string fechaCorte, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOGestionRiesgoData.ListadoSegmentoCredito(fechaCorte, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GestionRiesgoBusiness", "ListadoSegmentoCredito", ex);
                return null;
            }
        }


       

    }
}


