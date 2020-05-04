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
    public class ScScoringBoardVarService
    {
        private ScScoringBoardVarBusiness BOScScoringBoardVar;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ScScoringBoardVar
        /// </summary>
        public ScScoringBoardVarService()
        {
            BOScScoringBoardVar = new ScScoringBoardVarBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110202"; } }

        /// <summary>
        /// Servicio para crear ScScoringBoardVar
        /// </summary>
        /// <param name="pEntity">Entidad ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar creada</returns>
        public ScScoringBoardVar CrearScScoringBoardVar(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoardVar.CrearScScoringBoardVar(pScScoringBoardVar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardVarService", "CrearScScoringBoardVar", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ScScoringBoardVar
        /// </summary>
        /// <param name="pScScoringBoardVar">Entidad ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar modificada</returns>
        public ScScoringBoardVar ModificarScScoringBoardVar(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoardVar.ModificarScScoringBoardVar(pScScoringBoardVar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardVarService", "ModificarScScoringBoardVar", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ScScoringBoardVar
        /// </summary>
        /// <param name="pId">identificador de ScScoringBoardVar</param>
        public void EliminarScScoringBoardVar(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOScScoringBoardVar.EliminarScScoringBoardVar(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarScScoringBoardVar", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ScScoringBoardVar
        /// </summary>
        /// <param name="pId">identificador de ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar</returns>
        public ScScoringBoardVar ConsultarScScoringBoardVar(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoardVar.ConsultarScScoringBoardVar(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardVarService", "ConsultarScScoringBoardVar", ex);
                return null;
            }
        }





        /// <summary>
        /// Servicio para obtener ScScoringBoardVar por parametros
        /// </summary>
        /// <param name="pId">identificador de ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar</returns>
        public ScScoringBoardVar ConsultarScScoringBoardVarParam(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoardVar.ConsultarScScoringBoardVarParam(pScScoringBoardVar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardVarService", "ConsultarScScoringBoardVar", ex);
                return null;
            }
        }




        /// <summary>
        /// Servicio para obtener lista de ScScoringBoardVars a partir de unos filtros
        /// </summary>
        /// <param name="pScScoringBoardVar">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ScScoringBoardVar obtenidos</returns>
        public List<ScScoringBoardVar> ListarScScoringBoardVar(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoardVar.ListarScScoringBoardVar(pScScoringBoardVar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardVarService", "ListarScScoringBoardVar", ex);
                return null;
            }
        }

    }
}