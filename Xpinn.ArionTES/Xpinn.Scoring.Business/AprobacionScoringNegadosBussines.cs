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
    /// Objeto de negocio para AprobacionScoringNegados
    /// </summary>
    public class AprobacionScoringNegadosBusiness : GlobalData
    {
        private AprobacionScoringNegadosData DAAprobacionScoringNegados;

        /// <summary>
        /// Constructor del objeto de negocio para AprobacionScoringNegados
        /// </summary>
        public AprobacionScoringNegadosBusiness()
        {
            DAAprobacionScoringNegados = new AprobacionScoringNegadosData();
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<AprobacionScoringNegados> ListarAprobScoringNegados(AprobacionScoringNegados pAprobacionScoringNegados, Usuario pUsuario, String filtro)
        {
            try
            {
                return DAAprobacionScoringNegados.ListarAprobScoringNegados(pAprobacionScoringNegados, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionScoringNegadosBusiness", "ListarAprobacionScoringNegados", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de tipos documento dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos documento obtenidos</returns>
        public List<AprobacionScoringNegados> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return DAAprobacionScoringNegados.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarTiposDoc", ex);
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
                return DAAprobacionScoringNegados.ModificarCredito(pAprobacionScoringNegados, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionScoringNegados", "ModificarCredito", ex);
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
                return DAAprobacionScoringNegados.CrearMotivo(pAprobacionScoringNegados, pUsuario);
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
                DAAprobacionScoringNegados.EliminarMotivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarScScoringBoard", ex);
            }
        }



        /// <summary>
        /// Modifica un ScScoringBoardVar
        /// </summary>
        /// <param name="pScScoringBoardVar">Entidad ScScoringBoardVar</param>
        /// <returns>Entidad ScScoringBoardVar modificada</returns>
        public AprobacionScoringNegados ModificarMotivo(AprobacionScoringNegados pAprobacionScoringNegados, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAprobacionScoringNegados = DAAprobacionScoringNegados.ModificarMotivo(pAprobacionScoringNegados, pUsuario);

                    ts.Complete();
                }

                return pAprobacionScoringNegados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("pAprobacionScoringNegados", "ModificarMotivos", ex);
                return null;
            }
        }

    }

}