using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class SimulacionBusiness : GlobalData
    {
        private SimulacionData DADatosSimulacion;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public SimulacionBusiness()
        {
            DADatosSimulacion = new SimulacionData();
        }

        public Simulacion ConsultarSimulacionCuota(long monto, int plazo, int periodicidad, string cod_cred, int tipo_liquidacion, decimal tasa, decimal Comision, decimal Aporte, DateTime? FechaPrimerPago, List<CuotasExtras> lstCuotasExtras, ref string error, Usuario pUsuario, long cod_persona)
        {
            return DADatosSimulacion.ConsultarSimulacionCuota(monto, plazo, periodicidad, cod_cred, tipo_liquidacion, tasa, Comision, Aporte, FechaPrimerPago, lstCuotasExtras, ref error, pUsuario, cod_persona);
        }

        public Simulacion ConsultarSimulacionCuotaInterna(int monto, int plazo, int periodicidad,  int tasa, ref string error, Usuario pUsuario, long cod_persona)
        {
            return DADatosSimulacion.ConsultarSimulacionCuotaInterna(monto, plazo, periodicidad, tasa, ref error, pUsuario, cod_persona);
        }


        /// <summary>
        /// Simular el plan de pagos
        /// </summary>
        /// <param name="pDatos"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<DatosPlanPagos> SimularPlanPagos(Simulacion pDatos, Usuario pUsuario)
        {
            try
            {
                return DADatosSimulacion.SimularPlanPagos(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosPlanPagosBusiness", "SimularPlanPagos", ex);
                return null;
            }
        }
        /// <summary>
        /// Simular el plan de pagos
        /// </summary>
        /// <param name="pDatos"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<DatosPlanPagos> SimularPlanPagosInterno(Simulacion pDatos, Usuario pUsuario)
        {
            try
            {
                return DADatosSimulacion.SimularPlanPagosInterno(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SimulacionBusiness", "SimularPlanPagosInterno", ex);
                return null;
            }
        }

        public List<Atributos> SimularAtributosPlan(Usuario pUsuario)
        {
            try
            {
                return DADatosSimulacion.SimularAtributosPlan(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosPlanPagosBusiness", "SimularAtributosPlan", ex);
                return null;
            }
        }
    }
}
