using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
 
     /// <summary>
    /// Objeto de negocio para Familiares
    /// </summary>
    /// 
    public class FamiliaresPolizasBusiness : GlobalData
    {

        private FamiliaresPolizasData DAFamiliares;

        /// <summary>
        /// Constructor del objeto de negocio para PolizasSeguros
        /// </summary>
        public FamiliaresPolizasBusiness()
        {
            DAFamiliares = new FamiliaresPolizasData();
        }

        /// <summary>
        /// Obtiene la lista de Familiares dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Familiares obtenidos</returns>
        public List<FamiliaresPolizas> ListarFamiliares(FamiliaresPolizas pFamiliares, Usuario pUsuario)
        {
            try
            {
                return DAFamiliares.ListarFamiliares(pFamiliares, pUsuario); ;
              }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DAFamiliaresPolizasBusiness", "ListarFamiliares", ex);
                return null;
            }
        }
        /// <summary>
        /// Modifica un Familiar
        /// /// </summary>
        /// <param name="pEntity">Entidad Familiares</param>
        /// <returns>Entidad modificada</returns>
        public FamiliaresPolizas ModificarFamiliares(FamiliaresPolizas pFamiliares, Usuario pUsuario)
        {
            try
            {
              //  using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
               // {
                pFamiliares = DAFamiliares.ModificarFamiliares(pFamiliares, pUsuario);

                 //   ts.Complete();
                //}

                return pFamiliares;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresPolizasBusiness", "ModificarFamiliares", ex);
                return null;
            }

        }

        
        /// <summary>
        /// Elimina un Familiares
        /// </summary>
        /// <param name="pId">identificador de  Familiares</param>
        public void EliminarFamiliares(Int64 pId, Usuario pUsuario)
        {
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
              //  {

                    DAFamiliares.EliminarFamiliares(pId, pUsuario);

                    //ts.Complete();
               // }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresPolizasBusiness", "EliminarFamiliares", ex);
            }
        }

        /// <summary>
        /// Obtiene una Familiares
        /// </summary>
        /// <param name="pId">identificador del Familiares</param>
        /// <returns>Familiares consultada</returns>
        public FamiliaresPolizas ConsultarFamiliares(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAFamiliares.ConsultarFamiliaresPolizas(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresPolizasBusiness", "ConsultarFamiliares", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea un Familiares
        /// </summary>
        /// <param name="pEntity">Entidad Familiares</param>
        /// <returns>Entidad creada</returns>
        public FamiliaresPolizas CrearFamiliares(FamiliaresPolizas pFamiliares, Usuario pUsuario)
        {
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
               // {
                    pFamiliares = DAFamiliares.InsertarFamiliares(pFamiliares, pUsuario);

                  //  ts.Complete();
              //  }

                return pFamiliares;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresPolizasBusiness", "CrearFamiliares", ex);
                return null;
            }
        }

    }
}
       