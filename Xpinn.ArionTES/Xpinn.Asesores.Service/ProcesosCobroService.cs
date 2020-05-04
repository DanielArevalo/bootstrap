using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicios para Programa 
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ProcesosCobroService
    {
        private ProcesosCobroBusiness BOProcesosCobro;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ProcesosCobro
        /// </summary>
        public ProcesosCobroService()
        {
            BOProcesosCobro = new ProcesosCobroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110204"; } }

        /// <summary>
        /// Servicio para crear ProcesosCobro
        /// </summary>
        /// <param name="pEntity">Entidad ProcesosCobro</param>
        /// <returns>Entidad ProcesosCobro creada</returns>
        public ProcesosCobro CrearProcesosCobro(ProcesosCobro pProcesosCobro, Usuario pUsuario)
        {
            try
            {
                return BOProcesosCobro.CrearProcesosCobro(pProcesosCobro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroService", "CrearProcesosCobro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ProcesosCobro
        /// </summary>
        /// <param name="pProcesosCobro">Entidad ProcesosCobro</param>
        /// <returns>Entidad ProcesosCobro modificada</returns>
        public ProcesosCobro ModificarProcesosCobro(ProcesosCobro pProcesosCobro, Usuario pUsuario)
        {
            try
            {
                return BOProcesosCobro.ModificarProcesosCobro(pProcesosCobro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroService", "ModificarProcesosCobro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ProcesosCobro
        /// </summary>
        /// <param name="pId">identificador de ProcesosCobro</param>
        public void EliminarProcesosCobro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOProcesosCobro.EliminarProcesosCobro(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarProcesosCobro", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ProcesosCobro
        /// </summary>
        /// <param name="pId">identificador de ProcesosCobro</param>
        /// <returns>Entidad ProcesosCobro</returns>
        public ProcesosCobro ConsultarProcesosCobro(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOProcesosCobro.ConsultarProcesosCobro(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroService", "ConsultarProcesosCobro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener ProcesosCobro
        /// </summary>
        /// <param name="pId">identificador de ProcesosCobro</param>
        /// <returns>Entidad ProcesosCobro</returns>
        public ProcesosCobro ConsultarDatosProceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOProcesosCobro.ConsultarDatosProceso(pId, pUsuario);
               
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroService", "ConsultarDatosProceso", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener ProcesosCobro
        /// </summary>
        /// <param name="pId">identificador de ProcesosCobro</param>
        /// <returns>Entidad ProcesosCobro</returns>
        public ProcesosCobro ConsultarDatosProcesoAbogados( Usuario pUsuario)
        {
            try
            {
                return BOProcesosCobro.ConsultarDatosProcesoAbogados(pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroService", "ConsultarDatosProceso", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de ProcesosCobros a partir de unos filtros
        /// </summary>
        /// <param name="pProcesosCobro">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProcesosCobro obtenidos</returns>
        public List<ProcesosCobro> ListarProcesosCobro(ProcesosCobro pProcesosCobro, Usuario pUsuario)
        {
            try
            {
                return BOProcesosCobro.ListarProcesosCobro(pProcesosCobro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroService", "ListarProcesosCobro", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ProcesosCobros a partir de unos filtros
        /// </summary>
        /// <param name="pProcesosCobro">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProcesosCobro obtenidos</returns>
        public List<ProcesosCobro> ListarProcesosCobroSiguientes(ProcesosCobro pProcesosCobro, Usuario pUsuario)
        {
            try
            {
                return BOProcesosCobro.ListarProcesosCobroSiguientes(pProcesosCobro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosCobroService", "ListarProcesosCobro", ex);
                return null;
            }
        }
    }
}