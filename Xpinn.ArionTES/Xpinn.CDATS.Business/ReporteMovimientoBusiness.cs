using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Business
{
    /// <summary>
    /// Objeto de negocio para Destinacion
    /// </summary>
    public class ReporteMovimientoBusiness : GlobalBusiness
    {
        private ReporteMovimientoData DAReporte;

        /// <summary>
        /// Constructor del objeto de negocio para Destinacion
        /// </summary>
        public ReporteMovimientoBusiness()
        {
            DAReporte = new ReporteMovimientoData();
        }

        public List<ReporteMovimiento> ListarDropDownLineasCdat(ReporteMovimiento pReport, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarDropDownLineasCdat(pReport, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoCdatBusiness", "ListarDropDownLineasCdat", ex);
                return null;
            }
        }


        public List<Xpinn.CDATS.Entities.Cdat> ListarCdat(string pFiltro, DateTime pFecha, Usuario vUsuario, int estadoCuenta = 1)
        {
            try
            {
                return DAReporte.ListarCdat(pFiltro,pFecha,vUsuario, estadoCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoCdatBusiness", "ListarCdat", ex);
                return null;
            }
        }

        public List<Cdat> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarCuentasPersona(pCod_Persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoCdatBusiness", "ListarCuentasPersona", ex);
                return null;
            }
        }

        public Xpinn.CDATS.Entities.Cdat ConsultarCdat(string pNumero_cuenta, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ConsultarCdat(pNumero_cuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoBusiness", "ConsultarCdat", ex);
                return null;
            }
        }

        public List<ReporteMovimiento> ListarReporteMovimiento(Int64 pNumeroCuenta, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {
                return DAReporte.ListarReporteMovimiento(pNumeroCuenta, pFechaIni, pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoCdatBusiness", "ListarReporteMovimiento", ex);
                return null;
            }
        }

                
    }
}