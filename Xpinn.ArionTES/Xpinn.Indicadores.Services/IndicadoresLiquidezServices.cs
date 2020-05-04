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
    public class IndicadoresLiquidezService
    {


        private IndicadoresLiquidezBusiness BOIndicadoresLiquidezBusiness;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140801"; } }
       
        public List<IndicadoresLiquidez> consultarfecha(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresLiquidezBusiness.consultarfecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresLiquidezService", "consultarfecha", ex);
                return null;
            }
        }
        
        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
        public IndicadoresLiquidezService()
        {
            BOIndicadoresLiquidezBusiness = new IndicadoresLiquidezBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<IndicadoresLiquidez> consultarFondoLiquidez(string fechaini,  Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresLiquidezBusiness.consultarFondoLiquidez(fechaini, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresLiquidezService", "consultarFondoLiquidez", ex);
                return null;
            }
        }

        public List<IndicadoresLiquidez> consultarDepositosLiquidez(string fechaini, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresLiquidezBusiness.consultarDepositosLiquidez(fechaini, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresLiquidezService", "consultarDepositosLiquidez", ex);
                return null;
            }
        }

        public List<IndicadoresLiquidez> consultarDisponible(string fechaini, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresLiquidezBusiness.consultarDisponible(fechaini, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresLiquidezService", "consultarDisponible", ex);
                return null;
            }
        }



    }
}




