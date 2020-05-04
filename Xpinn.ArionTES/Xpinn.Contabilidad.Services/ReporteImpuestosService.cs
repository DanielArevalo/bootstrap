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
    public class ReporteImpuestosService
    {
        private ReporteImpuestosBusiness BOImpuestos;
        private ExcepcionBusiness BOExcepcion;

       
        public ReporteImpuestosService()
        {
            BOImpuestos = new ReporteImpuestosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "31102"; } }


        public List<ReporteImpuestos> ListarImpuestos(string filtro, DateTime pFechaIni, DateTime pFechaFin, string pOrdenar, Usuario vUsuario)
        {
            try
            {
                return BOImpuestos.ListarImpuestos(filtro, pFechaIni, pFechaFin, pOrdenar, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosService", "ListarImpuestos", ex);
                return null;
            }
        }


        public List<ReporteImpuestos> ListarImpuestosCombo(ReporteImpuestos pImpu, Usuario vUsuario)
        {
            try
            {
                return BOImpuestos.ListarImpuestosCombo(pImpu, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosService", "ListarImpuestos", ex);
                return null;
            }
        }


        public List<ReporteImpuestos> ListarCuentasConImpuesto(ReporteImpuestos pImpu, Usuario vUsuario)
        {
            try
            {
                return BOImpuestos.ListarCuentasConImpuesto(pImpu, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosService", "ListarCuentasConImpuesto", ex);
                return null;
            }
        }


        public List<ReporteImpuestos> ListarCuentasImpuesto(ReporteImpuestos pImpu, Usuario vUsuario)
        {
            try
            {
                return BOImpuestos.ListarCuentasImpuesto(pImpu, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosService", "ListarCuentasImpuesto", ex);
                return null;
            }
        }

    }
}