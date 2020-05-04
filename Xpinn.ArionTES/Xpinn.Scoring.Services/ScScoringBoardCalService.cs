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
    public class ScScoringBoardCalService
    {
        private ScScoringBoardCalBusiness BOScScoringBoardCal;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ScScoringBoardCal
        /// </summary>
        public ScScoringBoardCalService()
        {
            BOScScoringBoardCal = new ScScoringBoardCalBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        //public string CodigoPrograma { get { return "Per01"; } }
        public string CodigoPrograma { get { return "110202"; } }
        public string CodigoProgramaDatacredito { get { return "100101"; } }
        public string CodigoProgramaConyuge { get { return "100119"; } }
        public string CodigoProgramaCodeudor { get { return "100110"; } }

        /// <summary>
        /// Servicio para crear ScScoringBoardCal
        /// </summary>
        /// <param name="pEntity">Entidad ScScoringBoardCal</param>
        /// <returns>Entidad ScScoringBoardCal creada</returns>
        public ScScoringBoardCal CrearScScoringBoardCal(ScScoringBoardCal pScScoringBoardCal, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoardCal.CrearScScoringBoardCal(pScScoringBoardCal, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardCalService", "CrearScScoringBoardCal", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ScScoringBoardCal
        /// </summary>
        /// <param name="pScScoringBoardCal">Entidad ScScoringBoardCal</param>
        /// <returns>Entidad ScScoringBoardCal modificada</returns>
        public ScScoringBoardCal ModificarScScoringBoardCal(ScScoringBoardCal pScScoringBoardCal, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoardCal.ModificarScScoringBoardCal(pScScoringBoardCal, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardCalService", "ModificarScScoringBoardCal", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ScScoringBoardCal
        /// </summary>
        /// <param name="pId">identificador de ScScoringBoardCal</param>
        public void EliminarScScoringBoardCal(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOScScoringBoardCal.EliminarScScoringBoardCal(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarScScoringBoardCal", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ScScoringBoardCal
        /// </summary>
        /// <param name="pId">identificador de ScScoringBoardCal</param>
        /// <returns>Entidad ScScoringBoardCal</returns>
        public ScScoringBoardCal ConsultarScScoringBoardCal(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoardCal.ConsultarScScoringBoardCal(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardCalService", "ConsultarScScoringBoardCal", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ScScoringBoardCals a partir de unos filtros
        /// </summary>
        /// <param name="pScScoringBoardCal">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ScScoringBoardCal obtenidos</returns>
        public List<ScScoringBoardCal> ListarScScoringBoardCal(ScScoringBoardCal pScScoringBoardCal, Usuario pUsuario)
        {
            try
            {
                return BOScScoringBoardCal.ListarScScoringBoardCal(pScScoringBoardCal, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ScScoringBoardCalService", "ListarScScoringBoardCal", ex);
                return null;
            }
        }
        
    }
}