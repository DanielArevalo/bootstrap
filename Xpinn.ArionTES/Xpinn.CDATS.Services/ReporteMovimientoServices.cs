using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.CDATS.Business;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReporteMovimientoServices
    {
        private ReporteMovimientoBusiness BOReporte;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Destinacion
        /// </summary>
        public ReporteMovimientoServices()
        {
            BOReporte = new ReporteMovimientoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220319"; } }


        public List<ReporteMovimiento> ListarDropDownLineasCdat(ReporteMovimiento pReport, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarDropDownLineasCdat(pReport, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoCdatServices", "ListarDropDownLineasCdat", ex);
                return null;
            }
        }

        public List<Cdat> ListarCdat(string pFiltro, DateTime pFecha, Usuario vUsuario, int estadoCuenta = 1)
        {
            try
            {
                return BOReporte.ListarCdat(pFiltro, pFecha, vUsuario, estadoCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoCdatServices", "ListarCdat", ex);
                return null;
            }
        }

        public List<Cdat> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarCuentasPersona(pCod_Persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoCdatServices", "ListarCuentasPersona", ex);
                return null;
            }
        }
        public Cdat ConsultarCdat(string pNumero_cuenta, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ConsultarCdat(pNumero_cuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoCdatServices", "ConsultarCdat", ex);
                return null;
            }
        }


        public List<ReporteMovimiento> ListarReporteMovimiento(Int64 pNumeroCuenta, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ListarReporteMovimiento(pNumeroCuenta, pFechaIni, pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoCdatServices", "ListarReporteMovimiento", ex);
                return null;
            }
        }

    }
}