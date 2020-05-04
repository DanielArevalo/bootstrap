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
    public class CierreAnualService
    {
        private CierreAnualBusiness BOCierreAnual;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public CierreAnualService()
        {
            BOCierreAnual = new CierreAnualBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30602"; } }
        public string CodigoProgramaNiif { get { return "210117"; } }

        public List<CierreAnual> ListarFechaCierre(Usuario pUsuario)
        {
            return BOCierreAnual.ListarFechaCierre(pUsuario);
        }


        /// <summary>
        /// Servicio para obtener lista de FechaCorte a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de FechaCorte obtenidos</returns>
        public List<CierreAnual> ListarFechaCorte(Usuario pUsuario)
        {
            try
            {
                return BOCierreAnual.ListarFechaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierreAnualServices", "ListarFechaCorte", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para GenerarCierreAnual a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de GenerarCierreAnual obtenidos</returns>
        public CierreAnual CrearCierreAnual(CierreAnual pcierre, ref string pError, bool IsNiif, Usuario pUsuario)
        {
            try
            {
                return BOCierreAnual.CrearCierreAnual(pcierre, ref pError, IsNiif, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierreAnualServices", "CrearCierreAnual", ex);
                return null;
            }
        }

    }
}