using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Riesgo.Entities;
using Xpinn.Riesgo.Business;

namespace Xpinn.Riesgo.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReporteProductoServices
    {

        private ReporteProductoBusiness BOReporteProducto;
        private ExcepcionBusiness BOExcepcion;

        public ReporteProductoServices()
        {
            BOReporteProducto = new ReporteProductoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270105"; } }

        public List<ReporteProducto> ListarReporteProducto(ReporteProducto pReporte, DateTime? pFecIni, DateTime? pFecFin, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOReporteProducto.ListarReporteProducto(pReporte, pFecIni, pFecFin, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteProductoServices", "ListarReporteProducto", ex);
                return null;
            }
        }

        public List<producto> ListarProductoSaldo(Int64 pCod_Persona, DateTime? pFecIni, DateTime? pFecFin, Usuario pUsuario)
        {
            try
            {
                return BOReporteProducto.ListarProductoSaldo(pCod_Persona, pFecIni, pFecFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteProductoServices", "ListarProductoSaldo", ex);
                return null;
            }
        }
        public List<producto> ListarProductoSaldoXAsociado(Int64 pCod_Persona, DateTime? pFecIni, DateTime? pFecFin, Usuario pUsuario)
        {
            try
            {
                return BOReporteProducto.ListarProductoSaldoXAsociado(pCod_Persona, pFecIni, pFecFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteProductoServices", "ListarProductoSaldoXAsociado", ex);
                return null;
            }
        }

    }

}
