using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Seguridad.Data;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Business
{
    /// <summary>
    /// Objeto de negocio para Proceso
    /// </summary>
    public class ProcesoBusiness : GlobalBusiness
    {
        private ProcesoData DAProceso;

        /// <summary>
        /// Constructor del objeto de negocio para Proceso
        /// </summary>
        public ProcesoBusiness()
        {
            DAProceso = new ProcesoData();
        }

        /// <summary>
        /// Crea un Proceso
        /// </summary>
        /// <param name="pProceso">Entidad Proceso</param>
        /// <returns>Entidad Proceso creada</returns>
        public Proceso CrearProceso(Proceso pProceso, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProceso = DAProceso.CrearProceso(pProceso, pUsuario);

                    ts.Complete();
                }

                return pProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoBusiness", "CrearProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Proceso
        /// </summary>
        /// <param name="pProceso">Entidad Proceso</param>
        /// <returns>Entidad Proceso modificada</returns>
        public Proceso ModificarProceso(Proceso pProceso, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProceso = DAProceso.ModificarProceso(pProceso, pUsuario);

                    ts.Complete();
                }

                return pProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoBusiness", "ModificarProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Proceso
        /// </summary>
        /// <param name="pId">Identificador de Proceso</param>
        public void EliminarProceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAProceso.EliminarProceso(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoBusiness", "EliminarProceso", ex);
            }
        }

        /// <summary>
        /// Obtiene un Proceso
        /// </summary>
        /// <param name="pId">Identificador de Proceso</param>
        /// <returns>Entidad Proceso</returns>
        public Proceso ConsultarProceso(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAProceso.ConsultarProceso(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoBusiness", "ConsultarProceso", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<Proceso> ListarProceso(Proceso pProceso, Usuario pUsuario)
        {
            try
            {
                return DAProceso.ListarProceso(pProceso, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoBusiness", "ListarProceso", ex);
                return null;
            }
        }

    }
}