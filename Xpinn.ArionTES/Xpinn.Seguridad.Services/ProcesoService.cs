using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Seguridad.Business;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ProcesoService
    {
        private ProcesoBusiness BOProceso;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Proceso
        /// </summary>
        public ProcesoService()
        {
            BOProceso = new ProcesoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90102"; } }

        /// <summary>
        /// Servicio para crear Proceso
        /// </summary>
        /// <param name="pEntity">Entidad Proceso</param>
        /// <returns>Entidad Proceso creada</returns>
        public Proceso CrearProceso(Proceso pProceso, Usuario pUsuario)
        {
            try
            {
                return BOProceso.CrearProceso(pProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "CrearProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Proceso
        /// </summary>
        /// <param name="pProceso">Entidad Proceso</param>
        /// <returns>Entidad Proceso modificada</returns>
        public Proceso ModificarProceso(Proceso pProceso, Usuario pUsuario)
        {
            try
            {
                return BOProceso.ModificarProceso(pProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ModificarProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Proceso
        /// </summary>
        /// <param name="pId">identificador de Proceso</param>
        public void EliminarProceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOProceso.EliminarProceso(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarProceso", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Proceso
        /// </summary>
        /// <param name="pId">identificador de Proceso</param>
        /// <returns>Entidad Proceso</returns>
        public Proceso ConsultarProceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOProceso.ConsultarProceso(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ConsultarProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Procesos a partir de unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<Proceso> ListarProceso(Proceso pProceso, Usuario pUsuario)
        {
            try
            {
                return BOProceso.ListarProceso(pProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoService", "ListarProceso", ex);
                return null;
            }
        }
    }
}