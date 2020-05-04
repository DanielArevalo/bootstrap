using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Business
{

    public class ReporteProductoBusiness : GlobalBusiness
    {

        private ReporteProductoData DAReporteProducto;

        public ReporteProductoBusiness()
        {
            DAReporteProducto = new ReporteProductoData();
        }

        public List<ReporteProducto> ListarReporteProducto(ReporteProducto pReporte, DateTime? pFecIni, DateTime? pFecFin, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAReporteProducto.ListarReporteProducto(pReporte, pFecIni, pFecFin, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteProductoBusiness", "ListarReporteProducto", ex);
                return null;
            }
        }

        public List<producto> ListarProductoSaldo(Int64 pCod_Persona, DateTime? pFecIni, DateTime? pFecFin, Usuario pUsuario)
        {
            try
            {
                return DAReporteProducto.ListarProductoSaldo(pCod_Persona, pFecIni, pFecFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteProductoBusiness", "ListarProductoSaldo", ex);
                return null;
            }
        }
        public List<producto> ListarProductoSaldoXAsociado(Int64 pCod_Persona, DateTime? pFecIni, DateTime? pFecFin, Usuario pUsuario)
        {
            try
            {
                return DAReporteProducto.ListarProductoSaldoXAsociado(pCod_Persona, pFecIni, pFecFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteProductoBusiness", "ListarProductoSaldoXAsociado", ex);
                return null;
            }
        }

    }
}
