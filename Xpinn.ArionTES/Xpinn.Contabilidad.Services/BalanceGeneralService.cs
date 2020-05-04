using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class BalanceGeneralService
    {
        private BalanceGeneralBusiness BOBalanceGeneral;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public BalanceGeneralService()
        {
            BOBalanceGeneral = new BalanceGeneralBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30301"; } }
        public string CodigoProgramaNIIF { get { return "210301"; } }

        public List<BalanceGeneral> ListarBalance(BalanceGeneral pEntidad, Usuario vUsuario, int pOpcion)
        {
            return BOBalanceGeneral.ListarBalance(pEntidad, vUsuario, pOpcion); 
        }


        /// <summary>
        /// Servicio para obtener lista de FechaCorte a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de FechaCorte obtenidos</returns>
        public List<BalanceGeneral> ListarFechaCorte(Usuario pUsuario)
        {
            try
            {
                return BOBalanceGeneral.ListarFechaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceGeneralServices", "ListarFechaCorte", ex);
                return null;
            }
        }

        public List<BalanceGeneral> ListarFechaCierre(string pTipo, string pEstado, Usuario pUsuario)
        {
            try
            {
                return BOBalanceGeneral.ListarFechaCierre(pTipo, pEstado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceGeneralServices", "ListarFechaCorte", ex);
                return null;
            }
        }

        public string VerificarComprobantesYCuentas(DateTime fechaCorte, Usuario pUsuario)
        {
            try
            {
                return BOBalanceGeneral.VerificarComprobantesYCuentas(fechaCorte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceGeneralServices", "VerificarComprobantesYCuentas", ex);
                return null;
            }
        }
    }
}