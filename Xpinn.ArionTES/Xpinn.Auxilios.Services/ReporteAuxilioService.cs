using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Business;

namespace Xpinn.Auxilios.Services
{
    
    public class ReporteAuxilioService
    {

        private ReporteAuxilioBusiness BOReporteAuxilio;
        private ExcepcionBusiness BOExcepcion;

        public ReporteAuxilioService()
        {
            BOReporteAuxilio = new ReporteAuxilioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "70104"; } }
        public string CodigoProgramaAnulacion { get { return "70105"; } }

        public ReporteAuxilio CrearAuxilio(ReporteAuxilio pReporteAuxilio, Usuario pusuario)
        {
            try
            {
                pReporteAuxilio = BOReporteAuxilio.CrearAuxilio(pReporteAuxilio, pusuario);
                return pReporteAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioService", "CrearReporteAuxilio", ex);
                return null;
            }
        }


        public ReporteAuxilio ModificarAuxilio(ReporteAuxilio pReporteAuxilio, Usuario pusuario)
        {
            try
            {
                pReporteAuxilio = BOReporteAuxilio.ModificarAuxilio(pReporteAuxilio, pusuario);
                return pReporteAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioService", "ModificarReporteAuxilio", ex);
                return null;
            }
        }


        public void EliminarAuxilio(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOReporteAuxilio.EliminarAuxilio(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioService", "EliminarReporteAuxilio", ex);
            }
        }


        public ReporteAuxilio ConsultarAuxilio(Int64 pId, Usuario pusuario)
        {
            try
            {
                ReporteAuxilio ReporteAuxilio = new ReporteAuxilio();
                ReporteAuxilio = BOReporteAuxilio.ConsultarAuxilio(pId, pusuario);
                return ReporteAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioService", "ConsultarReporteAuxilio", ex);
                return null;
            }
        }


        public List<ReporteAuxilio> ListarAuxilio(String filtro,DateTime pFechaIni, DateTime pFechaFin, Usuario pusuario)
        {
            try
            {
                return BOReporteAuxilio.ListarAuxilio(filtro,pFechaIni,pFechaFin, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioService", "ListarReporteAuxilio", ex);
                return null;
            }
        }

        public List<ReporteAuxilio> ListarAuxilioPorAnular(String filtro, Usuario vUsuario)
        {
            try
            {
                return BOReporteAuxilio.ListarAuxilioPorAnular(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioService", "ListarAuxilioPorAnular", ex);
                return null;
            }
        }

        public void GenerarAnulacionAuxilio(ReporteAuxilio pData, Usuario vUsuario)
        {
            try
            {
                BOReporteAuxilio.GenerarAnulacionAuxilio(pData, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioService", "GenerarAnulacionAuxilio", ex);
            }
        }


    }
}