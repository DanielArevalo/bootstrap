using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para ProcesoOficina
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ProcesoOficinaService
    {
        private ProcesoOficinaBusiness BOProcesoOficina;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del Servicio para ProcesoOficina
        /// </summary>
        public ProcesoOficinaService()
        {
            BOProcesoOficina = new ProcesoOficinaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "120202"; } }

        /// <summary>
        /// Crea un procesoOficina
        /// </summary>
        /// <param name="pEntity">Entidad ProcesoOficina</param>
        /// <returns>Entidad creada</returns>
        public ProcesoOficina CrearProcesoOficina(ProcesoOficina pProcOficina, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOProcesoOficina.CrearProcesoOficina(pProcOficina, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoOficinaService", "CrearProcesoOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Oficina por parametro de consulta
        /// </summary>
        /// <param name="pId">identificador de la Oficina</param>
        /// <returns>Oficina consultada</returns>
        public ProcesoOficina ConsultarXProcesoOficina(ProcesoOficina pProcesoOficina, Usuario pUsuario)
        {
            try
            {
                return BOProcesoOficina.ConsultarXProcesoOficina(pProcesoOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoOficinaService", "ConsultarXProcesoOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Oficina por parametro de consulta
        /// </summary>
        /// <param name="pId">identificador de la Oficina</param>
        /// <returns>Oficina consultada</returns>
        public ProcesoOficina ConsultarUsuarioAperturo(ProcesoOficina pProcesoOficina, Usuario pUsuario)
        {
            try
            {
                return BOProcesoOficina.ConsultarUsuarioAperturo(pProcesoOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoOficinaService", "ConsultarUsuarioAperturo", ex);
                return null;
            }
        }


        public int CrearCajasAbrir(List<Xpinn.Caja.Entities.Caja> pListCaja,string pcod_cajero, Usuario pUsuario)
        {
            try
            {
                return BOProcesoOficina.CrearCajasAbrir(pListCaja, pcod_cajero, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoOficinaService", "CrearProcesoOficina", ex);
                return 0;
            }
        }
    }
   
}
