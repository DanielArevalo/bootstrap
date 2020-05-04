using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Reporteador.Business;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Services
{
    
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class UIAFService
    {
        private UIAFBusiness BOReporte;
        private ExcepcionBusiness BOExcepcion;

        
        public UIAFService()
        {
            BOReporte = new UIAFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "200401"; } }
        public string CodigoProgramaTarjeta { get { return "200404"; } }


        public UIAF CrearUiafREporte(UIAF pUIAF, Usuario vUsuario)
        {
            try
            {
                return BOReporte.CrearUiafREporte(pUIAF, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFService", "CrearUiafREporte", ex);
                return null;
            }
        }


        public UIAF ModificarUiafREporte(UIAF pUIAF, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ModificarUiafREporte(pUIAF, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFService", "ModificarUiafREporte", ex);
                return null;
            }
        }


        public void EliminarReporteUIAF(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOReporte.EliminarReporteUIAF(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFService", "EliminarReporteUIAF", ex);
            }
        }


        public void EliminarUIAFProducto(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOReporte.EliminarUIAFProducto(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFService", "EliminarUIAFProducto", ex);
            }
        }


        public List<UIAF> ListarReporteUIAF(String filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarReporteUIAF( filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFService", "ListarReporteUIAF", ex);
                return null;
            }
        }

        public List<UIAFDetalle> ListarVistaProductos(String filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarVistaProductos(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFService", "ListarVistaProductos", ex);
                return null;
            }
        }

        public List<UIAFDetalle> ListarVistaProductosAll(String filtro, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarVistaProductosAll(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFService", "ListarVistaProductosAll", ex);
                return null;
            }
        }


        public List<UIAFDetalle> ListarUIAFProductos(String filtro, Usuario vUsuario)//DateTime pFechaIni, DateTime pFechaFin,
        {
            try
            {
                return BOReporte.ListarUIAFProductos(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFService", "ListarUIAFProductos", ex);
                return null;
            }
        }

        public UIAF ConsultarReporteUIAF(Int32 pId, Usuario pUsuario)
        {
            try
            {
                return BOReporte.ConsultarReporteUIAF(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFService", "ConsultarReporteUIAF", ex);
                return null;
            }
        }
       

        
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOReporte.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

        public List<UIAFTarjetas> ListarTransaccionesTarjeta(String filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ListarTransaccionesTarjeta(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAFService", "ListarTransaccionesTarjeta", ex);
                return null;
            }
        }


    }
}