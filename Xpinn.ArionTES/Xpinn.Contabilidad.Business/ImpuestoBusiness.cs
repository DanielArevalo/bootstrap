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
    public class ImpuestoBusiness : GlobalBusiness
    {
        private ImpuestoData DAImpuestos;


        public ImpuestoBusiness()
        {
            DAImpuestos = new ImpuestoData();
        }


        public List<Impuesto> ListarRetencion(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return DAImpuestos.ListarRetencion(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosBusiness", "ListarRetencion", ex);
                return null;
            }
        }

        public List<Impuesto> ListarRetencion(string pCodTipoIMpuesto, string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return DAImpuestos.ListarRetencion(pCodTipoIMpuesto, filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosBusiness", "ListarRetencion", ex);
                return null;
            }
        }

        public Impuesto ConsultaTelefonoEmpresa(int id, Usuario vUsuario)
        {
            try
            {
                return DAImpuestos.ConsultaTelefonoEmpresa(id, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosBusiness", "ConsultaTelefonoEmpresa", ex);
                return null;
            }
        }

        public List<Impuesto> getListaGridvBusiness(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario) 
        {
            try
            {
                return DAImpuestos.getListaGridv(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosBusiness", "getListaGridvBusiness", ex);
                return null;
            }
        }


    }
}

