using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;


namespace Xpinn.FabricaCreditos.Business
{    
     /// <summary>
    /// Objeto de negocio para GarantiasReales
    /// </summary>
    /// 
    public class GarantiasRealesBusiness : GlobalData
    {

        private GarantiasRealesData  DAGarantiasReales;

        /// <summary>
        /// Constructor del objeto de negocio para GarantiasReales
        /// </summary>
        public GarantiasRealesBusiness()
        {
            DAGarantiasReales = new GarantiasRealesData();
        }

        /// <summary>
        /// Obtiene la lista de GarantiasReales dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de GarantiasReales obtenidos</returns>
        public List<GarantiasReales> ListarGarantiasReales(GarantiasReales pGarantiasReales, Usuario pUsuario)
      
        {
            try
            {
                return DAGarantiasReales.ListarGarantiasReales(pGarantiasReales, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasRealesBusiness", "ListarGarantiasReales", ex);
                return null;
            }
        }

       
        /// <summary>
        /// Modifica una GarantiasReales
        /// /// </summary>
        /// <param name="pEntity">Entidad GarantiasReales</param>
        /// <returns>Entidad modificada</returns>
        public GarantiasReales ModificarGarantiasReales(GarantiasReales pGarantiasReales, Usuario pUsuario)
        {
            try
            {
               // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                pGarantiasReales = DAGarantiasReales.ModificarGarantiasReales(pGarantiasReales, pUsuario);

                   // ts.Complete();
               // }

                return pGarantiasReales;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasRealesBusiness", "ModificarGarantiasReales", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina una GarantiasReales
        /// </summary>
        /// <param name="pId">identificador de  GarantiasReales</param>
        public void EliminarGarantiasReales(Int64 pId, Usuario pUsuario)
        {
            try
            {
              // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
               // {

                DAGarantiasReales.EliminarGarantiasReales(pId, pUsuario);

                   // ts.Complete();
                }
          //  }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasRealesBusiness", "EliminarGarantiasReales", ex);
            }
        }

        /// <summary>
        /// Obtiene una GarantiasReales
        /// </summary>
        /// <param name="pId">identificador de  GarantiasReales</param>
        /// <returns>GarantiasReales consultada</returns>
        public GarantiasReales ConsultarGarantiasReales(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAGarantiasReales.ConsultarGarantiasReales(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasRealessBusiness", "ConsultarGarantiasReales", ex);
                return null;
            }
        }

    
       
        /// <summary>
        /// Crea una GarantiasReales
        /// </summary>
        /// <param name="pEntity">Entidad GarantiasReales</param>
        /// <returns>Entidad creada</returns>
        public GarantiasReales CrearGarantiasReales(GarantiasReales pGarantiasReales, Usuario pUsuario)
        {
            try
            {
              //  using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                pGarantiasReales = DAGarantiasReales.InsertarGarantiasReales(pGarantiasReales, pUsuario);

                  //  ts.Complete();
                //}

                return pGarantiasReales;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasRealesBusiness", "CrearGarantiasReales", ex);
                return null;
            }
        }


    }
}
