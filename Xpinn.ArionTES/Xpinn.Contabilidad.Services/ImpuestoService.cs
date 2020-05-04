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
    public class ImpuestoService
    {
        private ImpuestoBusiness BOImpuestos;
        private ExcepcionBusiness BOExcepcion;


        public ImpuestoService()
        {
            BOImpuestos = new ImpuestoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "31103"; } }
        public string CodPrograma { get { return "31104"; } }


        public List<Impuesto> ListarRetencion(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return BOImpuestos.ListarRetencion(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosService", "ListarRetencion", ex);
                return null;
            }
        }

        public List<Impuesto> ListarRetencion(string pCodTipoImpuesto, string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return BOImpuestos.ListarRetencion(pCodTipoImpuesto, filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosService", "ListarRetencion", ex);
                return null;
            }
        }

        public Impuesto ConsultaTelefonoEmpresa(int id, Usuario vUsuario)
        {
            try
            {
                return BOImpuestos.ConsultaTelefonoEmpresa(id, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosService", "ConsultaTelefonoEmpresa", ex);
                return null;
            }
        }

        public List<Impuesto> getListaGridvServices(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario) 
        {
            try
            {
                return BOImpuestos.getListaGridvBusiness(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteImpuestosService", "getListaGridvServices", ex);
                return null;
            }
        }

    }
}