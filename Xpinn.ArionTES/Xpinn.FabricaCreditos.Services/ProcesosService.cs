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
    public class ProcesosService
    {
        private ProcesosBusiness BOProcesos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Procesos
        /// </summary>
        public ProcesosService()
        {
            BOProcesos = new ProcesosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        public int CodigoProceso;
        public string CodigoPrograma { get { return "100402"; } }

        /// <summary>
        /// Servicio para crear Procesos
        /// </summary>
        /// <param name="pEntity">Entidad Procesos</param>
        /// <returns>Entidad Procesos creada</returns>
        public Procesos CrearProcesos(Procesos pProcesos, Usuario pUsuario)
        {
            try
            {
                return BOProcesos.CrearProcesos(pProcesos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosService", "CrearProcesos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Procesos
        /// </summary>
        /// <param name="pProcesos">Entidad Procesos</param>
        /// <returns>Entidad Procesos modificada</returns>
        public Procesos ModificarProcesos(Procesos pProcesos, Usuario pUsuario)
        {
            try
            {
                return BOProcesos.ModificarProcesos(pProcesos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosService", "ModificarProcesos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Procesos
        /// </summary>
        /// <param name="pId">identificador de Procesos</param>
        public void EliminarProcesos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOProcesos.EliminarProcesos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosService", "EliminarProcesos", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Procesos
        /// </summary>
        /// <param name="pId">identificador de Procesos</param>
        /// <returns>Entidad Procesos</returns>
        public Procesos ConsultarProcesos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOProcesos.ConsultarProcesos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosService", "ConsultarProcesos", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener Procesos
        /// </summary>
        /// <param name="pId">identificador de Procesos</param>
        /// <returns>Entidad Procesos</returns>
        public Procesos ConsultarProcesosSiguientes(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOProcesos.ConsultarProcesosSiguientes(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosService", "ConsultarProcesosSiguientes", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Procesoss a partir de unos filtros
        /// </summary>
        /// <param name="pProcesos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Procesos obtenidos</returns>
        public List<Procesos> ListarProcesos(Procesos pProcesos, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOProcesos.ListarProcesos(pProcesos, pUsuario,filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosService", "ListarProcesos", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de Procesoss a partir de unos filtros
        /// </summary>
        /// <param name="pProcesos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Procesos obtenidos</returns>
        public List<Procesos> ListarProcesosAutomaticos(Procesos pProcesos, Usuario pUsuario)
        {
            try
            {
                return BOProcesos.ListarProcesosAutomaticos(pProcesos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosService", "ListarProcesosAutomaticos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Procesoss a partir de unos filtros
        /// </summary>
        /// <param name="pProcesos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Procesos obtenidos</returns>
        public List<Procesos> ListarProcesosSiguientes(Procesos pProcesos, String filtro,Usuario pUsuario)
        {
            try
            {
                return BOProcesos.ListarProcesosSiguientes(pProcesos, filtro,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosService", "ListarProcesosSiguientes", ex);
                return null;
            }
        }

        

        
    }
}