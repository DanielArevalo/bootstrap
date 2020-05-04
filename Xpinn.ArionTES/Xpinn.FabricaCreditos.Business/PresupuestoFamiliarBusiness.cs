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
    /// Objeto de negocio para PresupuestoFamiliar
    /// </summary>
    public class PresupuestoFamiliarBusiness : GlobalData
    {
        private PresupuestoFamiliarData DAPresupuestoFamiliar;

        /// <summary>
        /// Constructor del objeto de negocio para PresupuestoFamiliar
        /// </summary>
        public PresupuestoFamiliarBusiness()
        {
            DAPresupuestoFamiliar = new PresupuestoFamiliarData();
        }

        /// <summary>
        /// Crea un PresupuestoFamiliar
        /// </summary>
        /// <param name="pPresupuestoFamiliar">Entidad PresupuestoFamiliar</param>
        /// <returns>Entidad PresupuestoFamiliar creada</returns>
        public PresupuestoFamiliar CrearPresupuestoFamiliar(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPresupuestoFamiliar = DAPresupuestoFamiliar.CrearPresupuestoFamiliar(pPresupuestoFamiliar, pUsuario);

                    ts.Complete();
                }

                return pPresupuestoFamiliar;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoFamiliarBusiness", "CrearPresupuestoFamiliar", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un PresupuestoFamiliar
        /// </summary>
        /// <param name="pPresupuestoFamiliar">Entidad PresupuestoFamiliar</param>
        /// <returns>Entidad PresupuestoFamiliar modificada</returns>
        public PresupuestoFamiliar ModificarPresupuestoFamiliar(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPresupuestoFamiliar = DAPresupuestoFamiliar.ModificarPresupuestoFamiliar(pPresupuestoFamiliar, pUsuario);

                    ts.Complete();
                }

                return pPresupuestoFamiliar;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoFamiliarBusiness", "ModificarPresupuestoFamiliar", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un PresupuestoFamiliar
        /// </summary>
        /// <param name="pId">Identificador de PresupuestoFamiliar</param>
        public void EliminarPresupuestoFamiliar(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPresupuestoFamiliar.EliminarPresupuestoFamiliar(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoFamiliarBusiness", "EliminarPresupuestoFamiliar", ex);
            }
        }

        /// <summary>
        /// Obtiene un PresupuestoFamiliar
        /// </summary>
        /// <param name="pId">Identificador de PresupuestoFamiliar</param>
        /// <returns>Entidad PresupuestoFamiliar</returns>
        public PresupuestoFamiliar ConsultarPresupuestoFamiliar(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAPresupuestoFamiliar.ConsultarPresupuestoFamiliar(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoFamiliarBusiness", "ConsultarPresupuestoFamiliar", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pPresupuestoFamiliar">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoFamiliar obtenidos</returns>
        public List<PresupuestoFamiliar> ListarPresupuestoFamiliar(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            try
            {
                return DAPresupuestoFamiliar.ListarPresupuestoFamiliar(pPresupuestoFamiliar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoFamiliarBusiness", "ListarPresupuestoFamiliar", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pPresupuestoFamiliar">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoFamiliar obtenidos</returns>
        public List<PresupuestoFamiliar> ListarPresupuestoFamiliarRepo(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            try
            {
                return DAPresupuestoFamiliar.ListarPresupuestoFamiliarRepo(pPresupuestoFamiliar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoFamiliarBusiness", "ListarPresupuestoFamiliarRepo", ex);
                return null;
            }
        }

    }
}