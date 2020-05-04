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
    public class CarteraOficinasBusiness : GlobalData
    {

        private CarteraOficinasData BOarteraOficinasData;

        public CarteraOficinasBusiness()
        {
            BOarteraOficinasData = new CarteraOficinasData();
        }


        public List<CarteraOficinas> consultarCarteraOficinas(string fechaini, int tipo, Usuario pUsuario)
        {
            try
            {
                return BOarteraOficinasData.consultarCarteraOficinas(fechaini, tipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasBusiness", "consultarCarteraOficinas", ex);
                return null;
            }
        }

        public List<CarteraOficinas> consultarfecha(string pTipo, Usuario pUsuario)
        {
            try
            {
                return BOarteraOficinasData.consultarfecha(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasBusiness", "consultarfecha", ex);
                return null;
            }
        }

        public List<PrestamoPromedioOficinas> consultarPrestamoPromedio(string fechaini, int cod_clasifica, Usuario pUsuario)
        {
            try
            {
                return BOarteraOficinasData.consultarPrestamoPromedio(fechaini, cod_clasifica, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasBusiness", "consultarPrestamoPromedio", ex);
                return null;
            }
        }

        public List<DistribucionCarteraOficinas> DistribucionCarteraOficinas(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOarteraOficinasData.DistribucionCarteraOficinas(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasBusiness", "DistribucionCarteraOficinas", ex);
                return null;
            }
        }

    }
}


