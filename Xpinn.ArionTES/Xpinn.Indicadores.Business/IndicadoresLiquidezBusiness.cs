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
    public class IndicadoresLiquidezBusiness : GlobalData
    {

        private IndicadoresLiquidezData BOIndicadoresLiquidezData;

        public IndicadoresLiquidezBusiness()
        {
            BOIndicadoresLiquidezData = new IndicadoresLiquidezData();
        }


        public List<IndicadoresLiquidez> consultarfecha(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresLiquidezData.consultarfecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasBusiness", "consultarfecha", ex);
                return null;
            }
        }

        public List<IndicadoresLiquidez> consultarFondoLiquidez(string fechaini,  Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresLiquidezData.consultarFondoLiquidez(fechaini,  pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresLiquidezBusiness", "consultarFondoLiquidez", ex);
                return null;
            }
        }

        public List<IndicadoresLiquidez> consultarDepositosLiquidez(string fechaini, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresLiquidezData.consultarDepositosLiquidez(fechaini, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresLiquidezBusiness", "consultarDepositosLiquidez", ex);
                return null;
            }
        }


        public List<IndicadoresLiquidez> consultarDisponible(string fechaini, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresLiquidezData.consultarDisponible(fechaini, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresLiquidezBusiness", "consultarDisponible", ex);
                return null;
            }
        }


    }
}


