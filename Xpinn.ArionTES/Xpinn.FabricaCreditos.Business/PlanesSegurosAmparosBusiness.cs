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
    /// Objeto de negocio para PlanesSegurosAmparos
    /// </summary>
    /// 
    public class PlanesSegurosAmparosBusiness : GlobalData
    {

        private PlanesSegurosAmparosData DAPlanesSegurosAmparos;

        /// <summary>
        /// Constructor del objeto de negocio para PlanesSegurosAmparos
        /// </summary>
        public PlanesSegurosAmparosBusiness()
        {
            DAPlanesSegurosAmparos = new PlanesSegurosAmparosData();
        }

        /// <summary>
        /// Obtiene la lista de PlanesSegurosAmparos dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanesSegurosAmparos obtenidos</returns>
        public List<PlanesSegurosAmparos> ListarPlanesSegurosAmparos(PlanesSegurosAmparos pPlanesSegurosAmparos, Usuario pUsuario)
        {
            try
            {
                return DAPlanesSegurosAmparos.ListarPlanesSegurosAmparos(pPlanesSegurosAmparos, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosAmparosBusiness", "ListarPlanesSegurosAmparos", ex);
                return null;
            }
        }


        

        /// <summary>
        /// Modifica un PlanSegurosAmparos
        /// /// </summary>
        /// <param name="pEntity">Entidad PlanesSegurosAmparos</param>
        /// <returns>Entidad modificada</returns>
        public PlanesSegurosAmparos ModificarPlanesSegurosAmparos(PlanesSegurosAmparos pPlanesSegurosAmparos, Usuario pUsuario)
        {
            try
            {
               // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                    pPlanesSegurosAmparos = DAPlanesSegurosAmparos.ModificarPlanesSegurosAmparos(pPlanesSegurosAmparos, pUsuario);

                   // ts.Complete();
               // }

                return pPlanesSegurosAmparos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosAmparosBusiness", "ModificarPlanesSegurosAmparos", ex);
                return null;
            }

        }



        /// <summary>
        /// Crea un PlanSegurosAmparos
        /// /// </summary>
        /// <param name="pEntity">Entidad PlanesSegurosAmparos</param>
        /// <returns>Entidad creada</returns>
        public PlanesSegurosAmparos InsertarPlanesSegurosAmparos(PlanesSegurosAmparos pPlanesSegurosAmparos, Usuario pUsuario)
        {
            try
            {
                // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                pPlanesSegurosAmparos = DAPlanesSegurosAmparos.InsertarPlanesSegurosAmparos(pPlanesSegurosAmparos, pUsuario);

                // ts.Complete();
                // }

                return pPlanesSegurosAmparos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosAmparosBusiness", "InsertarPlanesSegurosAmparos", ex);
                return null;
            }

        }
        /// <summary>
        /// Elimina una PlanSegurosAmparos
        /// </summary>
        /// <param name="pId">identificador del PlanSegurosAmparos</param>
        public void EliminarPlanesSegurosAmparos(Int64 pId, Usuario pUsuario)
        {
            try
            {
               // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{

                    DAPlanesSegurosAmparos.EliminarPlanesSegurosAmparos(pId, pUsuario);

                  //  ts.Complete();
               // }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosAmparosBusiness", "EliminarPlanesSegurosAmparos", ex);
            }
        }

        /// <summary>
        /// Obtiene un PlanesSegurosAmparos
        /// </summary>
        /// <param name="pId">identificador del PlanesSegurosAmparos</param>
        /// <returns>PlanesSegurosAmparos consultada</returns>
        public PlanesSegurosAmparos ConsultarPlanesSegurosAmparos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAPlanesSegurosAmparos.ConsultarPlanesSegurosAmparos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosAmparosusiness", "ConsultarPlanesSegurosAmparos", ex);
                return null;
            }
        }
    }
}
