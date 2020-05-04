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
    public class CierreTercerosService
    {
        private CierreTercerosBusiness BOCierreTerceros;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public CierreTercerosService()
        {
            BOCierreTerceros = new CierreTercerosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30603"; } }
        public string CodigoProgramaNiif { get { return "210117"; } }

        public List<CierreTerceros> ListarFechaCierre(Usuario pUsuario)
        {
            return BOCierreTerceros.ListarFechaCierre(pUsuario);
        }


        /// <summary>
        /// Servicio para obtener lista de FechaCorte a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de FechaCorte obtenidos</returns>
        public List<CierreTerceros> ListarFechaCorte(Usuario pUsuario)
        {
            try
            {
                return BOCierreTerceros.ListarFechaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierreTercerosServices", "ListarFechaCorte", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para GenerarCierreTerceros a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de GenerarCierreTerceros obtenidos</returns>
        public CierreTerceros CrearCierreTerceros(CierreTerceros pcierre, ref string pError, bool IsNiif, Usuario pUsuario)
        {
            try
            {
                return BOCierreTerceros.CrearCierreTerceros(pcierre, ref pError, IsNiif, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierreTercerosServices", "CrearCierreTerceros", ex);
                return null;
            }
        }

    }
}