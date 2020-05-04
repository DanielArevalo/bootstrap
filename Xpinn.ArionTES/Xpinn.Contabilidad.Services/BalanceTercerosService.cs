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
    public class BalanceTercerosService
    {
        private BalanceTercerosBusiness BOBalanceTerceros;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public BalanceTercerosService()
        {
            BOBalanceTerceros = new BalanceTercerosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30209"; } }
        public string CodigoProgramaNIIF { get { return "210307"; } }
        public string CodigoProgramaComp { get { return "30208"; } }

        public string CodigoProgramaReporteCentroCostos { get { return "200115"; } }

        public List<BalanceTerceros> ListarBalance(BalanceTerceros pDatos, Usuario pUsuario)
        {
            return BOBalanceTerceros.ListarBalance(pDatos, pUsuario);
        }
       
        public BalanceTerceros ConsultarBalanceMes13(BalanceTerceros pDatos, Usuario pUsuario)
        {
            return BOBalanceTerceros.ConsultarBalanceMes13(pDatos, pUsuario);
        }

        /// <summary>
        /// Servicio para obtener lista de FechaCorte a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de FechaCorte obtenidos</returns>
        public List<BalanceTerceros> ListarFechaCorte(Usuario pUsuario)
        {
            try
            {
                return BOBalanceTerceros.ListarFechaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceTercerosServices", "ListarFechaCorte", ex);
                return null;
            }
        }

        public List<BalanceTerceros> ListarFechaCierreDefinitivo(string pTipo, Usuario pUsuario)
        {
            try
            {
                return BOBalanceTerceros.ListarFechaCierreDefinitivo(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceTercerosServices", "ListarFechaCierreDefinitivo", ex);
                return null;
            }
        }


        public List<BalanceTerceros> ListarBalanceComprobacion(BalanceTerceros pDatos, Usuario pUsuario)
        {
            return BOBalanceTerceros.ListarBalanceComprobacion(pDatos, pUsuario);
        }
        public List<BalanceTerceros> ListarBalanceCentroCosto(Usuario pUsuario,string centro,string fecha)
        {
            return BOBalanceTerceros.ListarBalanceCentroCosto(pUsuario, centro,fecha);
        }
        public List<BalanceTerceros> ListarCentroCosto(Usuario pUsuario)
        {
            return BOBalanceTerceros.ListarCentroCosto(pUsuario);
        }
        public BalanceTerceros ListarValorCentroCosto(string Centro,string cod_cuenta,Usuario pUsuario)
        {
            return BOBalanceTerceros.ListarValorCentroCosto(Centro,cod_cuenta,pUsuario);
        }
    }
}