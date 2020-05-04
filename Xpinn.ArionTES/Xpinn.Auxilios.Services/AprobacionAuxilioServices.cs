using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Business;
using Xpinn.Util;

namespace Xpinn.Auxilios.Services
{
    public class AprobacionAuxilioServices
    {
        public string CodigoPrograma { get { return "70102"; } }

        private AprobacionAuxilioBusiness BOAuxilio;
        private ExcepcionBusiness BOExcepcion;

        public AprobacionAuxilioServices()
        {
            BOAuxilio = new AprobacionAuxilioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public AprobacionAuxilio AprobarAuxilios(AprobacionAuxilio pAuxilio, int pOpcion, Auxilio_Orden_Servicio pAuxOrden, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.AprobarAuxilios(pAuxilio,pOpcion,pAuxOrden, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionAuxilioServices", "AprobarAuxilios", ex);
                return null;
            }
        }

        public AprobacionAuxilio CrearControlAuxilio(AprobacionAuxilio pAuxilio, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.CrearControlAuxilio(pAuxilio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionAuxilioServices", "CrearControlAuxilio", ex);
                return null;
            }
        }

        //CARGAR AUXILIOS
        public void RegistrarAuxiliosCargados(ref Int64 vCod_Ope, Xpinn.Tesoreria.Entities.Operacion pOperacion, List<SolicitudAuxilio> lstAuxilios, Usuario vUsuario)
        {
            try
            {
                BOAuxilio.RegistrarAuxiliosCargados(ref vCod_Ope, pOperacion, lstAuxilios, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosServices", "RegistrarAuxiliosCargados", ex);
            }
        }

    }
}
