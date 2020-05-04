using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Scoring.Data;
using Xpinn.Scoring.Entities;

namespace Xpinn.Scoring.Business
{
    /// <summary>
    /// Objeto de negocio para DefinirVariables
    /// </summary>
    public class DefinirVariablesBusiness : GlobalData
    {
        private VariablesData DADefinirVariables;

        /// <summary>
        /// Constructor del objeto de negocio para DefinirVariables
        /// </summary>
        public DefinirVariablesBusiness()
        {
            DADefinirVariables = new VariablesData();
        }

        /// <summary>
        /// Crea un DefinirVariables
        /// </summary>
        /// <param name="pDefinirVariables">Entidad DefinirVariables</param>
        /// <returns>Entidad DefinirVariables creada</returns>
        public Variables CrearDefinirVariables(Variables pDefinirVariables, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDefinirVariables = DADefinirVariables.CrearDefinirVariables(pDefinirVariables, pUsuario);

                    ts.Complete();
                }

                return pDefinirVariables;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DefinirVariablesBusiness", "CrearDefinirVariables", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un DefinirVariables
        /// </summary>
        /// <param name="pDefinirVariables">Entidad DefinirVariables</param>
        /// <returns>Entidad DefinirVariables modificada</returns>
        public Variables ModificarDefinirVariables(Variables pDefinirVariables, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDefinirVariables = DADefinirVariables.ModificarDefinirVariables(pDefinirVariables, pUsuario);

                    ts.Complete();
                }

                return pDefinirVariables;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DefinirVariablesBusiness", "ModificarDefinirVariables", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un DefinirVariables
        /// </summary>
        /// <param name="pId">Identificador de DefinirVariables</param>
        public void EliminarDefinirVariables(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADefinirVariables.EliminarDefinirVariables(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DefinirVariablesBusiness", "EliminarDefinirVariables", ex);
            }
        }

        /// <summary>
        /// Obtiene un DefinirVariables
        /// </summary>
        /// <param name="pId">Identificador de DefinirVariables</param>
        /// <returns>Entidad DefinirVariables</returns>
        public Variables ConsultarDefinirVariables(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DADefinirVariables.ConsultarDefinirVariables(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DefinirVariablesBusiness", "ConsultarDefinirVariables", ex);
                return null;
            }
        }
      
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pDefinirVariables">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DefinirVariables obtenidos</returns>
        public List<Variables> ListarDefinirVariables(Variables pDefinirVariables, Usuario pUsuario)
        {
            try
            {
                return DADefinirVariables.ListarDefinirVariables(pDefinirVariables, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DefinirVariablesBusiness", "ListarDefinirVariables", ex);
                return null;
            }
        }
     
    }
}