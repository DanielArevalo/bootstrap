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
    /// Objeto de negocio para PlanesSeguros
    /// </summary>
    /// 
    public class PlanesSegurosBusiness : GlobalData
    {

        private PlanesSegurosData DAPlanesSeguros;

        /// <summary>
        /// Constructor del objeto de negocio para PlanesSeguros
        /// </summary>
        public PlanesSegurosBusiness()
        {
            DAPlanesSeguros = new PlanesSegurosData();
        }

        /// <summary>
        /// Obtiene la lista de PlanesSeguros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanesSeguros obtenidos</returns>
        public List<PlanesSeguros> ListarPlanesSeguros(PlanesSeguros pPlanesSeguros, Usuario pUsuario)
        {
            try
            {
                return DAPlanesSeguros.ListarPlanesSeguros(pPlanesSeguros, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosBusiness", "ListarPlanesSeguros", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un PlanSeguros
        /// /// </summary>
        /// <param name="pEntity">Entidad PlanesSeguros</param>
        /// <returns>Entidad modificada</returns>
        public PlanesSeguros ModificarPlanesSeguros(PlanesSeguros pPlanesSeguros,Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPlanesSeguros = DAPlanesSeguros.ModificarPlanesSeguros(pPlanesSeguros, pUsuario);

                    ts.Complete();
                }

                return pPlanesSeguros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosBusiness", "ModificarPlanesSeguros", ex);
                return null;
            }

        }

        /// <summary>
        /// Crea un PlanSeguros
        /// /// </summary>
        /// <param name="pEntity">Entidad PlanesSeguros</param>
        /// <returns>Entidad Creada</returns>
        public PlanesSeguros InsertarPlanesSeguros(PlanesSeguros pPlanesSeguros, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPlanesSeguros = DAPlanesSeguros.InsertarPlanesSeguros(pPlanesSeguros, pUsuario);

                    ts.Complete();
                }

                return pPlanesSeguros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosBusiness", "InsertarPlanesSeguros", ex);
                return null;
            }

        }
        /// <summary>
        /// Elimina una PlanSeguros
        /// </summary>
        /// <param name="pId">identificador del PlanSeguros</param>
        public void EliminarPlanesSeguros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DAPlanesSeguros.EliminarPlanesSeguros(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosBusiness", "EliminarPlanesSeguros", ex);
            }
        }

        /// <summary>
        /// Obtiene un PlanesSeguros
        /// </summary>
        /// <param name="pId">identificador del PlanesSeguros</param>
        /// <returns>PlanesSeguros consultada</returns>
        public PlanesSeguros ConsultarPlanesSeguros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAPlanesSeguros.ConsultarPlanesSeguros(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanesSegurosBusiness", "ConsultarPlanesSeguros", ex);
                return null;
            }
        }
    }
}
