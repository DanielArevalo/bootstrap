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
    /// Objeto de negocio para ScScoringBoard
    /// </summary>
    public class ScScoringBoardBusiness : GlobalData
    {
        private ScScoringBoardData DAScScoringBoard;

        /// <summary>
        /// Constructor del objeto de negocio para ScScoringBoard
        /// </summary>
        public ScScoringBoardBusiness()
        {
            DAScScoringBoard = new ScScoringBoardData();
        }

        /// <summary>
        /// Crea un ScScoringBoard
        /// </summary>
        /// <param name="pScScoringBoard">Entidad ScScoringBoard</param>
        /// <returns>Entidad ScScoringBoard creada</returns>
        public ScScoringBoard CrearScScoringBoard(ScScoringBoard pScScoringBoard, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pScScoringBoard = DAScScoringBoard.CrearScScoringBoard(pScScoringBoard, pUsuario);

                    ts.Complete();
                }

                return pScScoringBoard;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardBusiness", "CrearScScoringBoard", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ScScoringBoard
        /// </summary>
        /// <param name="pScScoringBoard">Entidad ScScoringBoard</param>
        /// <returns>Entidad ScScoringBoard modificada</returns>
        public ScScoringBoard ModificarScScoringBoard(ScScoringBoard pScScoringBoard, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pScScoringBoard = DAScScoringBoard.ModificarScScoringBoard(pScScoringBoard, pUsuario);

                    ts.Complete();
                }

                return pScScoringBoard;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardBusiness", "ModificarScScoringBoard", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ScScoringBoard
        /// </summary>
        /// <param name="pId">Identificador de ScScoringBoard</param>
        public void EliminarScScoringBoard(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAScScoringBoard.EliminarScScoringBoard(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardBusiness", "EliminarScScoringBoard", ex);
            }
        }

        /// <summary>
        /// Obtiene un ScScoringBoard
        /// </summary>
        /// <param name="pId">Identificador de ScScoringBoard</param>
        /// <returns>Entidad ScScoringBoard</returns>
        public ScScoringBoard ConsultarScScoringBoard(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAScScoringBoard.ConsultarScScoringBoard(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardBusiness", "ConsultarScScoringBoard", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pScScoringBoard">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ScScoringBoard obtenidos</returns>
        public List<ScScoringBoard> ListarScScoringBoard(ScScoringBoard pScScoringBoard, Usuario pUsuario)
        {
            try
            {
                return DAScScoringBoard.ListarScScoringBoard(pScScoringBoard, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardBusiness", "ListarScScoringBoard", ex);
                return null;
            }
        }

        public List<Clasificacion> ListarClasificacion(Clasificacion clasificacion, Usuario pUsuario)
        {
            try
            {
                return DAScScoringBoard.ListarClasificacion(clasificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardService", "ListarClasificacion", ex);
                return null;
            }
        }

        public List<Lineas> ListarLineas(Lineas Lineas, Usuario pUsuario)
        {
            try
            {
                return DAScScoringBoard.ListarLineas(Lineas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardService", "ListarLineas", ex);
                return null;
            }
        }

    }

}