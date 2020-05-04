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
    public class AprobacionScoringNegadosService
    {
        private AprobacionScoringNegadosBusiness BOAprobacionScoringNegados;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AprobacionScoringNegados
        /// </summary>
        public AprobacionScoringNegadosService()
        {
            BOAprobacionScoringNegados = new AprobacionScoringNegadosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

      
        public string CodigoPrograma { get { return "160101"; } }


    
       



        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<AprobacionScoringNegados> ListarAprobScoringNegados(AprobacionScoringNegados pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOAprobacionScoringNegados.ListarAprobScoringNegados(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionScoringNegadosService", "ListarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene  listas desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de elementos obtenidos</returns>
        public List<AprobacionScoringNegados> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return BOAprobacionScoringNegados.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionScoringNegados", "ListasDesplegables", ex);
                return null;
            }
        }







        /// <summary>
        /// Servicio para modificar ScScoringBoard
        /// </summary>
        /// <param name="pScScoringBoard">Entidad ScScoringBoard</param>
        /// <returns>Entidad ScScoringBoard modificada</returns>
        public AprobacionScoringNegados ModificarCredito(AprobacionScoringNegados pAprobacionScoringNegados, Usuario pUsuario)
        {
            try
            {
                return BOAprobacionScoringNegados.ModificarCredito(pAprobacionScoringNegados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BOAprobacionScoringNegados", "ModificarCredito", ex);
                return null;
            }
        }




        /// <summary>
        /// Servicio para crear ScScoringBoardVar
        /// </summary>
        /// <param name="pEntity">Entidad ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar creada</returns>
        public AprobacionScoringNegados CrearMotivo(AprobacionScoringNegados pAprobacionScoringNegados, Usuario pUsuario)
        {
            try
            {
                return BOAprobacionScoringNegados.CrearMotivo(pAprobacionScoringNegados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionScoringNegados", "CrearMotivo", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para Eliminar ScScoringBoard
        /// </summary>
        /// <param name="pId">identificador de ScScoringBoard</param>
        public void EliminarMotivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOAprobacionScoringNegados.EliminarMotivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarScScoringBoard", ex);
            }
        }




        /// <summary>
        /// Servicio para modificar ScScoringBoardVar
        /// </summary>
        /// <param name="pScScoringBoardVar">Entidad ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar modificada</returns>
        public AprobacionScoringNegados ModificarMotivo(AprobacionScoringNegados pAprobacionScoringNegados, Usuario pUsuario)
        {
            try
            {
                return BOAprobacionScoringNegados.ModificarMotivo(pAprobacionScoringNegados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionScoringNegados", "ModificarMotivo", ex);
                return null;
            }
        }

    }
}