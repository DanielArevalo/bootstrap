using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para Procesos
    /// </summary>
    public class ProcesosBusiness : GlobalData
    {
        private ProcesosData DAProcesos;

        /// <summary>
        /// Constructor del objeto de negocio para Procesos
        /// </summary>
        public ProcesosBusiness()
        {
            DAProcesos = new ProcesosData();
        }

        /// <summary>
        /// Crea un Procesos
        /// </summary>
        /// <param name="pProcesos">Entidad Procesos</param>
        /// <returns>Entidad Procesos creada</returns>
        public Procesos CrearProcesos(Procesos pProcesos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProcesos = DAProcesos.CrearProcesos(pProcesos, pUsuario);

                    ts.Complete();
                }

                return pProcesos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosBusiness", "CrearProcesos", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Procesos
        /// </summary>
        /// <param name="pProcesos">Entidad Procesos</param>
        /// <returns>Entidad Procesos modificada</returns>
        public Procesos ModificarProcesos(Procesos pProcesos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProcesos = DAProcesos.ModificarProcesos(pProcesos, pUsuario);

                    ts.Complete();
                }

                return pProcesos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosBusiness", "ModificarProcesos", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Procesos
        /// </summary>
        /// <param name="pId">Identificador de Procesos</param>
        public void EliminarProcesos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAProcesos.EliminarProcesos(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosBusiness", "EliminarProcesos", ex);
            }
        }

        /// <summary>
        /// Obtiene un Procesos
        /// </summary>
        /// <param name="pId">Identificador de Procesos</param>
        /// <returns>Entidad Procesos</returns>
        public Procesos ConsultarProcesos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAProcesos.ConsultarProcesos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosBusiness", "ConsultarProcesos", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un Procesos
        /// </summary>
        /// <param name="pId">Identificador de Procesos</param>
        /// <returns>Entidad Procesos</returns>
        public Procesos ConsultarProcesosSiguientes(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAProcesos.ConsultarProcesosSiguientes(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosBusiness", "ConsultarProcesosSiguientes", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProcesos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Procesos obtenidos</returns>
        public List<Procesos> ListarProcesos(Procesos pProcesos, Usuario pUsuario,String filtro)
        {
            try
            {
                return DAProcesos.ListarProcesos(pProcesos, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosBusiness", "ListarProcesos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProcesos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Procesos obtenidos</returns>
        public List<Procesos> ListarProcesosAutomaticos(Procesos pProcesos, Usuario pUsuario)
        {
            try
            {
                return DAProcesos.ListarProcesosAutomaticos(pProcesos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosBusiness", "ListarProcesosAutomaticos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProcesos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Procesos obtenidos</returns>
        public List<Procesos> ListarProcesosSiguientes(Procesos pProcesos, String filtro, Usuario pUsuario)
        {
            try
            {
                return DAProcesos.ListarProcesosSiguientes(pProcesos,filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesosBusiness", "ListarProcesosSiguientes", ex);
                return null;
            }
        }



    }
}