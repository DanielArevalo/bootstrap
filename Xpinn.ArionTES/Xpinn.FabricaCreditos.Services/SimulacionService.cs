using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SimulacionService
    {
        private LineasCreditoBusiness BOLinea;
        private PeriodicidadBusiness BOPeriodicidad;
        private ExcepcionBusiness BOExcepcion;
        private SimulacionBusiness BOSimulacion;

        public SimulacionService()
        {
            BOLinea = new LineasCreditoBusiness();
            BOPeriodicidad = new PeriodicidadBusiness();
            BOExcepcion = new ExcepcionBusiness();
            BOSimulacion = new SimulacionBusiness();
        }

        public string CodigoPrograma { get { return "100143"; } }
        public string CodigoProgramaSimInterna { get { return "100155"; } }

        public List<Periodicidad> ListarPeriodicidad(Periodicidad Periodic, Usuario pUsuario)
        {
            try
            {
                return BOPeriodicidad.ListarPeriodicidad(Periodic, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PeriodicidadService", "ListarPeriodicidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Realizar el cálculo de la cuota del credito
        /// </summary>
        /// <param name="monto"></param>
        /// <param name="plazo"></param>
        /// <param name="periodicidad"></param>
        /// <param name="cod_cred"></param>
        /// <returns></returns>
        public Simulacion ConsultarSimulacionCuota(long monto, int plazo, int periodicidad, string cod_cred, int tipo_liquidacion, decimal tasa, decimal Comision, decimal Aporte, DateTime? FechaPrimerPago, ref string error, Usuario pUsuario, long cod_persona, List<CuotasExtras> lstCuotasExtras = null)
        {
            return BOSimulacion.ConsultarSimulacionCuota(monto, plazo, periodicidad, cod_cred, tipo_liquidacion, tasa, Comision, Aporte, FechaPrimerPago, lstCuotasExtras, ref error, pUsuario, cod_persona);
        }

        public Simulacion ConsultarSimulacionCuotaInterna(int monto, int plazo, int periodicidad,  int tasa, ref string error, Usuario pUsuario, long cod_persona)
        {
            return BOSimulacion.ConsultarSimulacionCuotaInterna(monto, plazo, periodicidad,  tasa, ref error, pUsuario, cod_persona);
        }

        /// <summary>
        /// Realizar el cálculo del plan de pagos de acuerdo a datos de la simulación.
        /// </summary>
        /// <param name="pDatos"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<DatosPlanPagos> SimularPlanPagos(Simulacion pDatos, Usuario pUsuario)
        {
            try
            {
                return BOSimulacion.SimularPlanPagos(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SimulacionService", "SimularPlanPagos", ex);
                return null;
            }
        }
        /// <summary>
        /// Realizar el cálculo del plan de pagos de acuerdo a datos de la simulación.
        /// </summary>
        /// <param name="pDatos"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<DatosPlanPagos> SimularPlanPagosInterno(Simulacion pDatos, Usuario pUsuario)
        {
            try
            {
                return BOSimulacion.SimularPlanPagosInterno(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SimulacionService", "SimularPlanPagosInterno", ex);
                return null;
            }
        }


        public List<Atributos> SimularAtributosPlan(Usuario pUsuario)
        {
            try
            {
                return BOSimulacion.SimularAtributosPlan(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SimulacionService", "SimularAtributosPlan", ex);
                return null;
            }
        }

    }
}
