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
    public class ScScoringBoardService
    {
        private ScScoringBoardBusiness BOScScoringBoard;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ScScoringBoard
        /// </summary>
        public ScScoringBoardService()
        {
            BOScScoringBoard = new ScScoringBoardBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "160203"; } }

        /// <summary>
        /// Servicio para crear ScScoringBoard
        /// </summary>
        /// <param name="pEntity">Entidad ScScoringBoard</param>
        /// <returns>Entidad ScScoringBoard creada</returns>
        public ScScoringBoard CrearScScoringBoard(ScScoringBoard pScScoringBoard, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoard.CrearScScoringBoard(pScScoringBoard, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardService", "CrearScScoringBoard", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ScScoringBoard
        /// </summary>
        /// <param name="pScScoringBoard">Entidad ScScoringBoard</param>
        /// <returns>Entidad ScScoringBoard modificada</returns>
        public ScScoringBoard ModificarScScoringBoard(ScScoringBoard pScScoringBoard, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoard.ModificarScScoringBoard(pScScoringBoard, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardService", "ModificarScScoringBoard", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ScScoringBoard
        /// </summary>
        /// <param name="pId">identificador de ScScoringBoard</param>
        public void EliminarScScoringBoard(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOScScoringBoard.EliminarScScoringBoard(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarScScoringBoard", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ScScoringBoard
        /// </summary>
        /// <param name="pId">identificador de ScScoringBoard</param>
        /// <returns>Entidad ScScoringBoard</returns>
        public ScScoringBoard ConsultarScScoringBoard(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoard.ConsultarScScoringBoard(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardService", "ConsultarScScoringBoard", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ScScoringBoards a partir de unos filtros
        /// </summary>
        /// <param name="pScScoringBoard">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ScScoringBoard obtenidos</returns>
        public List<ScScoringBoard> ListarScScoringBoard(ScScoringBoard pScScoringBoard, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoard.ListarScScoringBoard(pScScoringBoard, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardService", "ListarScScoringBoard", ex);
                return null;
            }
        }

        public List<Clasificacion> ListarClasificacion(Clasificacion clasificacion, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoard.ListarClasificacion(clasificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardService", "ListarClasificacion", ex);
                return null;
            }
        }

        public List<Lineas> ListarLineas(Lineas vLineas, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoard.ListarLineas(vLineas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardService", "ListarLineas", ex);
                return null;
            }
        }

    }
}