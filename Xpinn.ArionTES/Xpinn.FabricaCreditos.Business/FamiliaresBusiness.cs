using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    public class FamiliaresBusiness : GlobalData
    {
        private FamiliaresData DAFamiliares;

        /// <summary>
        /// Constructor del objeto de negocio para Familiares
        /// </summary>
        public FamiliaresBusiness()
        {
            DAFamiliares = new FamiliaresData();
        }

        /// <summary>
        /// Crea un Familiares
        /// </summary>
        /// <param name="pFamiliares">Entidad Familiares</param>
        /// <returns>Entidad Familiares creada</returns>
        public Familiares CrearFamiliares(Familiares pFamiliares, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pFamiliares = DAFamiliares.CrearFamiliares(pFamiliares, pUsuario);

                    ts.Complete();
                }

                return pFamiliares;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresBusiness", "CrearFamiliares", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Familiares
        /// </summary>
        /// <param name="pFamiliares">Entidad Familiares</param>
        /// <returns>Entidad Familiares modificada</returns>
        public Familiares ModificarFamiliares(Familiares pFamiliares, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pFamiliares = DAFamiliares.ModificarFamiliares(pFamiliares, pUsuario);

                    ts.Complete();
                }

                return pFamiliares;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresBusiness", "ModificarFamiliares", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Familiares
        /// </summary>
        /// <param name="pId">Identificador de Familiares</param>
        public void EliminarFamiliares(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAFamiliares.EliminarFamiliares(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresBusiness", "EliminarFamiliares", ex);
            }
        }

        /// <summary>
        /// Obtiene un Familiares
        /// </summary>
        /// <param name="pId">Identificador de Familiares</param>
        /// <returns>Entidad Familiares</returns>
        public Familiares ConsultarFamiliares(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAFamiliares.ConsultarFamiliares(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresBusiness", "ConsultarFamiliares", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pFamiliares">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Familiares obtenidos</returns>
        public List<Familiares> ListarFamiliares(Familiares pFamiliares, Usuario pUsuario, String Id)
        {
            try
            {
                return DAFamiliares.ListarFamiliares(pFamiliares, pUsuario, Id);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresBusiness", "ListarFamiliares", ex);
                return null;
            }
        }




        /// <summary>
        /// Obtiene la lista de los menus desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos documento obtenidos</returns>
        public List<Familiares> ListasDesplegables(Familiares pMenus, Usuario pUsuario, String ListaSolicitada)
        {
            try
            {
                return DAFamiliares.ListasDesplegables(pMenus, pUsuario, ListaSolicitada);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarTiposDoc", ex);
                return null;
            }
        }


    }
}