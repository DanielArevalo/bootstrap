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
    public class BalancePruebaService
    {
        private BalancePruebaBusiness BOBalancePrueba;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public BalancePruebaService()
        {
            BOBalancePrueba = new BalancePruebaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30207"; } }
        public string CodigoProgramaComp { get { return "30208"; } }
        public string CodigoProgramaCompTer { get { return "30211"; } }

        public string CodigoProgramaReporteCentroCostos { get { return "200115"; } }

        public List<BalancePrueba> ListarBalance(BalancePrueba pDatos, Usuario pUsuario)
        {
            return BOBalancePrueba.ListarBalance(pDatos, pUsuario);
        }

        public BalancePrueba ConsultarBalanceMes13(BalancePrueba pDatos, Usuario pUsuario)
        {
            return BOBalancePrueba.ConsultarBalanceMes13(pDatos, pUsuario);
        }

        /// <summary>
        /// Servicio para obtener lista de FechaCorte a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de FechaCorte obtenidos</returns>
        public List<BalancePrueba> ListarFechaCorte(Usuario pUsuario)
        {
            try
            {
                return BOBalancePrueba.ListarFechaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalancePruebaServices", "ListarFechaCorte", ex);
                return null;
            }
        }

        public List<BalancePrueba> ListarBalanceComprobacion(BalancePrueba pDatos, ref Double TotDeb, ref Double TotCr, Usuario pUsuario)
        {
            return BOBalancePrueba.ListarBalanceComprobacion(pDatos, ref TotDeb, ref TotCr, pUsuario);
        }
        public List<BalancePrueba> ListarBalanceCentroCosto(Usuario pUsuario, string centro, string fecha)
        {
            return BOBalancePrueba.ListarBalanceCentroCosto(pUsuario, centro, fecha);
        }
        public List<BalancePrueba> ListarCentroCosto(Usuario pUsuario)
        {
            return BOBalancePrueba.ListarCentroCosto(pUsuario);
        }
        public BalancePrueba ListarValorCentroCosto(string Centro, string cod_cuenta, Usuario pUsuario)
        {
            return BOBalancePrueba.ListarValorCentroCosto(Centro, cod_cuenta, pUsuario);
        }

        public void AlertaBalance(DateTime pfecha, ref decimal Activo, ref decimal Pasivo, ref decimal Patrimonio, ref decimal Utilidad, ref decimal Diferencia, Usuario vUsuario)
        {
            BOBalancePrueba.AlertaBalance(pfecha, ref Activo, ref Pasivo, ref Patrimonio, ref Utilidad, ref Diferencia, vUsuario);
        }

        public List<BalancePrueba> ListarBalanceComprobacionTer(BalancePrueba pDatos, Usuario pUsuario)
        {
            return BOBalancePrueba.ListarBalanceComprobacionTer(pDatos, pUsuario);
        }

    }
}