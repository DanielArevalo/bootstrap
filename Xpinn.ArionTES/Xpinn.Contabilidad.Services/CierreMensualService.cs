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
    public class CierreMensualService
    {
        private CierreMensualBusiness BOCierreMensual;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public CierreMensualService()
        {
            BOCierreMensual = new CierreMensualBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30601"; } }

        public List<Cierremensual> ListarFechaCierre(Usuario pUsuario)
        {
            return BOCierreMensual.ListarFechaCierre(pUsuario); 
        }


        /// <summary>
        /// Servicio para obtener lista de FechaCorte a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de FechaCorte obtenidos</returns>
        public List<Cierremensual> ListarFechaCorte(Usuario pUsuario)
        {
            try
            {
                return BOCierreMensual.ListarFechaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierremensualServices", "ListarFechaCorte", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para GenerarCierreMensual a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de GenerarCierreMensual obtenidos</returns>
        public Cierremensual CrearCierreMensual(Cierremensual pcierre, Usuario pUsuario)
        {
            try
            {
                return BOCierreMensual.CrearCierreMensual(pcierre,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierremensualServices", "CrearCierreMensual", ex);
                return null;
            }
        }

    }
}