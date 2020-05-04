using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class ReporteImpuestosBusiness : GlobalBusiness
    {
        private ReporteImpuestosData DAImpuestos;


        public ReporteImpuestosBusiness()
        {
            DAImpuestos = new ReporteImpuestosData();
        }


        public List<ReporteImpuestos> ListarImpuestos(string filtro, DateTime pFechaIni, DateTime pFechaFin, string pOrdenar, Usuario vUsuario)
        {
            try
            {
                return DAImpuestos.ListarImpuestos(filtro, pFechaIni, pFechaFin, pOrdenar, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosBusiness", "ListarImpuestos", ex);
                return null;
            }
        }

        public List<ReporteImpuestos> ListarImpuestosCombo(ReporteImpuestos pImpu, Usuario vUsuario)
        {
            try
            {
                return DAImpuestos.ListarImpuestosCombo(pImpu, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosBusiness", "ListarImpuestos", ex);
                return null;
            }
        }

        public List<ReporteImpuestos> ListarCuentasConImpuesto(ReporteImpuestos pImpu, Usuario vUsuario)
        {
            try
            {
                return DAImpuestos.ListarCuentasConImpuesto(pImpu, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosBusiness", "ListarCuentasConImpuesto", ex);
                return null;
            }
        }


        public List<ReporteImpuestos> ListarCuentasImpuesto(ReporteImpuestos pImpu, Usuario vUsuario)
        {
            try
            {
                return DAImpuestos.ListarCuentasImpuesto(pImpu, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosBusiness", "ListarCuentasImpuesto", ex);
                return null;
            }
        }

    }
}

