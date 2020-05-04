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
    /// Objeto de negocio para ScScoringBoardVar
    /// </summary>
    public class ScScoringBoardVarBusiness : GlobalData
    {
        private ScScoringBoardVarData DAScScoringBoardVar;

        /// <summary>
        /// Constructor del objeto de negocio para ScScoringBoardVar
        /// </summary>
        public ScScoringBoardVarBusiness()
        {
            DAScScoringBoardVar = new ScScoringBoardVarData();
        }

        /// <summary>
        /// Crea un ScScoringBoardVar
        /// </summary>
        /// <param name="pScScoringBoardVar">Entidad ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar creada</returns>
        public ScScoringBoardVar CrearScScoringBoardVar(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pScScoringBoardVar = DAScScoringBoardVar.CrearScScoringBoardVar(pScScoringBoardVar, pUsuario);

                    ts.Complete();
                }

                return pScScoringBoardVar;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardVarBusiness", "CrearScScoringBoardVar", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ScScoringBoardVar
        /// </summary>
        /// <param name="pScScoringBoardVar">Entidad ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar modificada</returns>
        public ScScoringBoardVar ModificarScScoringBoardVar(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pScScoringBoardVar = DAScScoringBoardVar.ModificarScScoringBoardVar(pScScoringBoardVar, pUsuario);

                    ts.Complete();
                }

                return pScScoringBoardVar;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardVarBusiness", "ModificarScScoringBoardVar", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ScScoringBoardVar
        /// </summary>
        /// <param name="pId">Identificador de ScScoringBoardVar</param>
        public void EliminarScScoringBoardVar(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAScScoringBoardVar.EliminarScScoringBoardVar(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardVarBusiness", "EliminarScScoringBoardVar", ex);
            }
        }

        /// <summary>
        /// Obtiene un ScScoringBoardVar
        /// </summary>
        /// <param name="pId">Identificador de ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar</returns>
        public ScScoringBoardVar ConsultarScScoringBoardVar(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAScScoringBoardVar.ConsultarScScoringBoardVar(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardVarBusiness", "ConsultarScScoringBoardVar", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un ScScoringBoardVar cedula
        /// </summary>
        /// <param name="pId">Identificador de ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar</returns>
        public ScScoringBoardVar ConsultarScScoringBoardVarParam(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            try
            {
                return DAScScoringBoardVar.ConsultarScScoringBoardVarParam(pScScoringBoardVar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardVarBusiness", "ConsultarScScoringBoardVar", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pScScoringBoardVar">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ScScoringBoardVar obtenidos</returns>
        public List<ScScoringBoardVar> ListarScScoringBoardVar(ScScoringBoardVar pScScoringBoardVar, Usuario pUsuario)
        {
            try
            {
                return DAScScoringBoardVar.ListarScScoringBoardVar(pScScoringBoardVar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardVarBusiness", "ListarScScoringBoardVar", ex);
                return null;
            }
        }

    }

}