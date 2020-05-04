using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AprobacionCtasPorPagarServices
    {
        private AprobacionCtasPorPagarBusiness BOAprobacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AreasCaj
        /// </summary>
        public AprobacionCtasPorPagarServices()
        {
            BOAprobacion = new AprobacionCtasPorPagarBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40703"; } }

        public List<AprobacionCtasPorPagar> ListarCuentasXpagar(String filtro, String Orden, DateTime pFechaIng, DateTime pFechaVencIni, DateTime pFechaVencFin, Usuario vUsuario)
        {
            try
            {
                return BOAprobacion.ListarCuentasXpagar(filtro, Orden, pFechaIng, pFechaVencIni, pFechaVencFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionGirosServices", "ListarCuentasXpagar", ex);
                return null;
            }
        }


        public void AprobarCuentaXpagar(List<Xpinn.Tesoreria.Entities.Giro> lstGiro, Usuario vUsuario)
        {
            try
            {
                BOAprobacion.AprobarCuentaXpagar(lstGiro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionGirosServices", "AprobarCuentasXpagar", ex);               
            }
        }


    }
}