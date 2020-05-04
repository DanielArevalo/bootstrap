using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CruceCtaAProductoServices
    {
        private CruceCtaAProductoBusiness BOAhorroVista;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Destinacion
        /// </summary>
        public CruceCtaAProductoServices()
        {
            BOAhorroVista = new CruceCtaAProductoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170125"; } }

        public Boolean CrearSolicitud_cruce_ahorro(List<Solicitud_cruce_ahorro> lstSolicitud, ref string pError, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.CrearSolicitud_cruce_ahorro(lstSolicitud, ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CruceCtaAProductoServices", "CrearSolicitud_cruce_ahorro", ex);
                return false;
            }
        }


        public void EliminarSolicitud_Cruce_ahorro(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOAhorroVista.EliminarSolicitud_Cruce_ahorro(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CruceCtaAProductoServices", "EliminarSolicitud_Cruce_ahorro", ex);
            }
        }

        public Solicitud_cruce_ahorro ConsultarSolicitud_cruce(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ConsultarSolicitud_cruce(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CruceCtaAProductoServices", "ConsultarSolicitud_cruce", ex);
                return null;
            }
        }

        public List<Solicitud_cruce_ahorro> ListarSolicitud_cruce(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ListarSolicitud_cruce(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CruceCtaAProductoServices", "ListarSolicitud_cruce", ex);
                return null;
            }
        }


        public Boolean AplicarCruceProducto(List<Solicitud_cruce_ahorro> lstSolicitud, Usuario pUsuario, Int64 pProcesoCont, ref List<Xpinn.Tesoreria.Entities.Operacion> lstOperaciones, ref string Error)
        {
            try
            {
                return BOAhorroVista.AplicarCruceProducto(lstSolicitud, pUsuario, pProcesoCont, ref lstOperaciones, ref Error);
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return false;
            }
        }

        public void CambiarEstado_Solicitud_Cruce_ahorro(Solicitud_cruce_ahorro p_solicitud, Usuario _usuario)
        {
            try
            {
                BOAhorroVista.CambiarEstado_Solicitud_Cruce_ahorro(p_solicitud, _usuario);
                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CruceCtaAProductoServices", "CambiarEstado_Solicitud_Cruce_ahorro", ex);
                return;
            }
        }
    }
}