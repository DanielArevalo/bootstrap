using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Scoring.Business;
using Xpinn.Scoring.Entities;

namespace Xpinn.Scoring.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>


    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class VariablesService
    {
        private DefinirVariablesBusiness BODefinirVariables;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DefinirVariables
        /// </summary>
        public VariablesService()
        {
            BODefinirVariables = new DefinirVariablesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "160201"; } }

        
        /// <summary>
        /// Servicio para crear DefinirVariables
        /// </summary>
        /// <param name="pEntity">Entidad DefinirVariables</param>
        /// <returns>Entidad DefinirVariables creada</returns>
        public Variables CrearDefinirVariables(Variables pDefinirVariables, Usuario pUsuario)
        {
            try
            {
                return BODefinirVariables.CrearDefinirVariables(pDefinirVariables, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DefinirVariablesService", "CrearDefinirVariables", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar DefinirVariables
        /// </summary>
        /// <param name="pDefinirVariables">Entidad DefinirVariables</param>
        /// <returns>Entidad DefinirVariables modificada</returns>
        public Variables ModificarDefinirVariables(Variables pDefinirVariables, Usuario pUsuario)
        {
            try
            {
                return BODefinirVariables.ModificarDefinirVariables(pDefinirVariables, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DefinirVariablesService", "ModificarDefinirVariables", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar DefinirVariables
        /// </summary>
        /// <param name="pId">identificador de DefinirVariables</param>
        public void EliminarDefinirVariables(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BODefinirVariables.EliminarDefinirVariables(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DefinirVariablesService", "EliminarDefinirVariables", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener DefinirVariables
        /// </summary>
        /// <param name="pId">identificador de DefinirVariables</param>
        /// <returns>Entidad DefinirVariables</returns>
        public Variables ConsultarDefinirVariables(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BODefinirVariables.ConsultarDefinirVariables(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DefinirVariablesService", "ConsultarDefinirVariables", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de DefinirVariabless a partir de unos filtros
        /// </summary>
        /// <param name="pDefinirVariables">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DefinirVariables obtenidos</returns>
        public List<Variables> ListarDefinirVariables(Variables pDefinirVariables, Usuario pUsuario)
        {
            try
            {
                return BODefinirVariables.ListarDefinirVariables(pDefinirVariables, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DefinirVariablesService", "ListarDefinirVariables", ex);
                return null;
            }
        }
        
    }
}