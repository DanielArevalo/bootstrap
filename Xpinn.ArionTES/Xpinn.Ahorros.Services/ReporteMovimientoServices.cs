using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Services
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

        public string CodigoPrograma { get { return "220202"; } }


        public List<ReporteMovimiento> ListarDropDownLineasAhorro(ReporteMovimiento pReport, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarDropDownLineasAhorro(pReport, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoServices", "ListarDropDownLineasAhorro", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarAhorroVista(string pFiltro, DateTime pFecha, Usuario vUsuario, int EstadoCuenta = 0)
        {
            try
            {
                return BOReporte.ListarAhorroVista(pFiltro, pFecha, vUsuario, EstadoCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoServices", "ListarAhorroVista", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarAhorroVistaClubAhorrador(Int64 pCod_persona, string pFiltro, Boolean pResult, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarAhorroVistaClubAhorrador(pCod_persona, pFiltro, pResult, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoServices", "ListarAhorroVistaClubAhorrador", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarCuentasPersona(pCod_Persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoServices", "ListarCuentasPersona", ex);
                return null;
            }
        }

        public AhorroVista ConsultarAhorroVista(string pNumero_cuenta, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ConsultarAhorroVista(pNumero_cuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoServices", "ConsultarAhorroVista", ex);
                return null;
            }
        }
      
           public AhorroVista EliminarAhorroVista(string pNumero_cuenta, Usuario vUsuario)
        {
            try
            {
                return BOReporte.EliminarAhorroVista(pNumero_cuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoServices", "EliminarAhorroVista", ex);
                return null;
            }
        }

           public List<ReporteMovimiento> ListarReporteMovimiento(Int64 pNumeroCuenta, DateTime? pFechaIni, DateTime? pFechaFin, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ListarReporteMovimiento(pNumeroCuenta, pFechaIni, pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoServices", "ListarReporteMovimiento", ex);
                return null;
            }
        }
        public SolicitudProductosWeb ConsultarSolicitud(Usuario vUsuario)
        {
            try
            {
                return BOReporte.ConsultarSolicitud(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoServices", "ConsultarAhorroVista", ex);
                return null;
            }
        }
        public SolicitudProductosWeb CrearSolicitudProd(SolicitudProductosWeb vSolicitudPro, Usuario pUsuario)
        {
            try
            {
                return BOReporte.CrearSolicitudPro(vSolicitudPro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "CrearAhorroVista", ex);
                return null;
            }
        }

    }
}