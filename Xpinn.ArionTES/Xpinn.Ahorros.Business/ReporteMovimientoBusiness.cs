using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Ahorros.Data;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Business
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
        
        public List<ReporteMovimiento> ListarDropDownLineasAhorro(ReporteMovimiento pReport, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarDropDownLineasAhorro(pReport, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoBusiness", "ListarDropDownLineasAhorro", ex);
                return null;
            }
        }


        public List<AhorroVista> ListarAhorroVista(string pFiltro, DateTime pFecha, Usuario vUsuario, int EstadoCuenta = 0)
        {
            try
            {
                return DAReporte.ListarAhorroVista(pFiltro,pFecha,vUsuario, EstadoCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoBusiness", "ListarAhorroVista", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarAhorroVistaClubAhorrador(Int64 pCod_persona, string pFiltro, Boolean pResult, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarAhorroVistaClubAhorrador(pCod_persona, pFiltro, pResult, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoBusiness", "ListarAhorroVistaClubAhorrador", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarCuentasPersona(pCod_Persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoBusiness", "ListarCuentasPersona", ex);
                return null;
            }
        }

        public AhorroVista ConsultarAhorroVista(string pNumero_cuenta, Usuario vUsuario)
        {
            try
            {
                return DAReporte.ConsultarAhorroVista(pNumero_cuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoBusiness", "ConsultarAhorroVista", ex);
                return null;
            }
        }

        public AhorroVista EliminarAhorroVista(string pNumero_cuenta, Usuario vUsuario)
        {
            try
            {
                return DAReporte.EliminarAhorroVista(pNumero_cuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoBusiness", "EliminarAhorroVista", ex);
                return null;
            }
        }

        public List<ReporteMovimiento> ListarReporteMovimiento(Int64 pNumeroCuenta, DateTime? pFechaIni, DateTime? pFechaFin, Usuario pUsuario)
        {
            try
            {
                return DAReporte.ListarReporteMovimiento(pNumeroCuenta, pFechaIni, pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoBusiness", "ListarReporteMovimiento", ex);
                return null;
            }
        }
        public SolicitudProductosWeb ConsultarSolicitud(Usuario vUsuario)
        {
            try
            {
                return DAReporte.ConsultarSolicitud(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoBusiness", "EliminarAhorroVista", ex);
                return null;
            }
        }
        public SolicitudProductosWeb CrearSolicitudPro(SolicitudProductosWeb vSolicitudPro, Usuario pUsuario)
        {
            try
            {
                return DAReporte.CrearSolicitudProducto(vSolicitudPro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "CrearAhorroVista", ex);
                return null;
            }
        }

    }
}