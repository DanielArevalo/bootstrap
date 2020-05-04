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
    /// Objeto de negocio para ScScoringBoardCal
    /// </summary>
    public class ScScoringBoardCalBusiness : GlobalData
    {
        private ScScoringBoardCalData DAScScoringBoardCal;

        /// <summary>
        /// Constructor del objeto de negocio para ScScoringBoardCal
        /// </summary>
        public ScScoringBoardCalBusiness()
        {
            DAScScoringBoardCal = new ScScoringBoardCalData();
        }

        /// <summary>
        /// Crea un ScScoringBoardCal
        /// </summary>
        /// <param name="pScScoringBoardCal">Entidad ScScoringBoardCal</param>
        /// <returns>Entidad ScScoringBoardCal creada</returns>
        public ScScoringBoardCal CrearScScoringBoardCal(ScScoringBoardCal pScScoringBoardCal, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pScScoringBoardCal = DAScScoringBoardCal.CrearScScoringBoardCal(pScScoringBoardCal, pUsuario);

                    ts.Complete();
                }

                return pScScoringBoardCal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardCalBusiness", "CrearScScoringBoardCal", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ScScoringBoardCal
        /// </summary>
        /// <param name="pScScoringBoardCal">Entidad ScScoringBoardCal</param>
        /// <returns>Entidad ScScoringBoardCal modificada</returns>
        public ScScoringBoardCal ModificarScScoringBoardCal(ScScoringBoardCal pScScoringBoardCal, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pScScoringBoardCal = DAScScoringBoardCal.ModificarScScoringBoardCal(pScScoringBoardCal, pUsuario);

                    ts.Complete();
                }

                return pScScoringBoardCal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardCalBusiness", "ModificarScScoringBoardCal", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ScScoringBoardCal
        /// </summary>
        /// <param name="pId">Identificador de ScScoringBoardCal</param>
        public void EliminarScScoringBoardCal(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAScScoringBoardCal.EliminarScScoringBoardCal(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardCalBusiness", "EliminarScScoringBoardCal", ex);
            }
        }

        /// <summary>
        /// Obtiene un ScScoringBoardCal
        /// </summary>
        /// <param name="pId">Identificador de ScScoringBoardCal</param>
        /// <returns>Entidad ScScoringBoardCal</returns>
        public ScScoringBoardCal ConsultarScScoringBoardCal(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAScScoringBoardCal.ConsultarScScoringBoardCal(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardCalBusiness", "ConsultarScScoringBoardCal", ex);
                return null;
            }
        }


      
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pScScoringBoardCal">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ScScoringBoardCal obtenidos</returns>
        public List<ScScoringBoardCal> ListarScScoringBoardCal(ScScoringBoardCal pScScoringBoardCal, Usuario pUsuario)
        {
            try
            {
                return DAScScoringBoardCal.ListarScScoringBoardCal(pScScoringBoardCal, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardCalBusiness", "ListarScScoringBoardCal", ex);
                return null;
            }
        }

     
    }

}