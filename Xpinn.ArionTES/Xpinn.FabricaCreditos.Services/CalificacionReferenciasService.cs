using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
 
namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CalificacionReferenciasService
    {
        private CalificacionReferenciasBusiness BOCalificacionReferencias;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CalificacionReferencias
        /// </summary>
        public CalificacionReferenciasService()
        {
            BOCalificacionReferencias = new CalificacionReferenciasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "CRE"; } }

        /// <summary>
        /// Servicio para crear CalificacionReferencias
        /// </summary>
        /// <param name="pEntity">Entidad CalificacionReferencias</param>
        /// <returns>Entidad CalificacionReferencias creada</returns>
        public CalificacionReferencias CrearCalificacionReferencias(CalificacionReferencias pCalificacionReferencias, Usuario pUsuario)
        {
            try
            {
                return BOCalificacionReferencias.CrearCalificacionReferencias(pCalificacionReferencias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CalificacionReferenciasService", "CrearCalificacionReferencias", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar CalificacionReferencias
        /// </summary>
        /// <param name="pCalificacionReferencias">Entidad CalificacionReferencias</param>
        /// <returns>Entidad CalificacionReferencias modificada</returns>
        public CalificacionReferencias ModificarCalificacionReferencias(CalificacionReferencias pCalificacionReferencias, Usuario pUsuario)
        {
            try
            {
                return BOCalificacionReferencias.ModificarCalificacionReferencias(pCalificacionReferencias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CalificacionReferenciasService", "ModificarCalificacionReferencias", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar CalificacionReferencias
        /// </summary>
        /// <param name="pId">identificador de CalificacionReferencias</param>
        public void EliminarCalificacionReferencias(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOCalificacionReferencias.EliminarCalificacionReferencias(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarCalificacionReferencias", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener CalificacionReferencias
        /// </summary>
        /// <param name="pId">identificador de CalificacionReferencias</param>
        /// <returns>Entidad CalificacionReferencias</returns>
        public CalificacionReferencias ConsultarCalificacionReferencias(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCalificacionReferencias.ConsultarCalificacionReferencias(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CalificacionReferenciasService", "ConsultarCalificacionReferencias", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de CalificacionReferenciass a partir de unos filtros
        /// </summary>
        /// <param name="pCalificacionReferencias">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CalificacionReferencias obtenidos</returns>
        public List<CalificacionReferencias> ListarCalificacionReferencias(CalificacionReferencias pCalificacionReferencias, Usuario pUsuario)
        {
            try
            {
                return BOCalificacionReferencias.ListarCalificacionReferencias(pCalificacionReferencias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CalificacionReferenciasService", "ListarCalificacionReferencias", ex);
                return null;
            }
        }
    }
}