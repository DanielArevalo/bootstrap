using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using System.Web;

namespace Xpinn.FabricaCreditos.Services
{
    public class GarantiasComunitariasService
    {
        private GarantiasComunitariasBusiness BOGarantias;
        private ExcepcionBusiness BOExcepcion;

        public GarantiasComunitariasService()
        {
            BOGarantias = new GarantiasComunitariasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoProgramaDesembolso { get { return "100301"; } }
        public string CodigoProgramaCartera { get { return "100302"; } }
        public string CodigoProgramaReclamaciones { get { return "100303"; } }
        public string CodigoProgramaAplicacionReclamaciones { get { return "100304"; } }

        public Boolean CargarArchivo(GarantiasComunitarias pEntityEjecutivoMeta, ref string perror, Usuario pUsuario)
        {
            return BOGarantias.CargarArchivo(pEntityEjecutivoMeta, ref perror, pUsuario);
        }


        public List<GarantiasComunitarias> consultargarantiascomunitariasCartera(string fechaini, string fechafin, int cod, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.consultargarantiascomunitariasCartera(fechaini, fechafin, cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }

        public List<GarantiasComunitarias> consultargarantiascomunitariasReclamaciones(string fechaini, string fechafin, int cod, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.consultargarantiascomunitariasReclamaciones(fechaini, fechafin, cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }

        public void CrearReclamacion(GarantiasComunitarias reclamaciones, Usuario pUsuario, string fecha, int encabezado)
        {
            try
            {
                BOGarantias.CrearReclamacion(reclamaciones, pUsuario, fecha, encabezado);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("", "", ex);

            }
        }


        public List<GarantiasComunitarias> consultargarantiasconsultarReclamacion(string fechaini, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.consultargarantiasconsultarReclamacion(fechaini, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }

        public List<GarantiasComunitarias> consultargarantiascomunitariasReclamacionesdetalle(string fechaini, int reclamacion, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.consultargarantiascomunitariasReclamacionesdetalle(fechaini, reclamacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }

        public List<GarantiasComunitarias> consultargarantiascomunitarias(string fechaini, string fechafin, int cod, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.consultargarantiascomunitarias(fechaini, fechafin, cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }

        public List<GarantiasComunitarias> ConsultarGarantiasComunitariasReclamacionesDetalle_Pago(Int64 numero_reclamacion, Usuario pUsuario)
        {
            try
            {
                return BOGarantias.ConsultarGarantiasComunitariasReclamacionesDetalle_Pago(numero_reclamacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }


        public Boolean AplicarPago(Int64 numero_reclamacion, DateTime fecha_reclamacion, List<GarantiasComunitarias> lstReclamaciones, Usuario pUsuario, ref string Error, ref Int64 pCodOpe)
        {
            try
            {
                return BOGarantias.AplicarPago(numero_reclamacion, fecha_reclamacion, lstReclamaciones, pUsuario, ref Error, ref pCodOpe);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return false;
            }

        }


        public Boolean Validar(DateTime fecha_reclamacion, List<GarantiasComunitarias> lstReclamaciones, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BOGarantias.Validar(fecha_reclamacion, lstReclamaciones, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return false;
            }

        }

    }
}
