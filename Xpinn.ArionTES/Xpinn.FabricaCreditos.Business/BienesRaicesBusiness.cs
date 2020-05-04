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
    /// Objeto de negocio para BienesRaices
    /// </summary>
    public class BienesRaicesBusiness : GlobalData
    {
        private BienesRaicesData DABienesRaices;

        /// <summary>
        /// Constructor del objeto de negocio para BienesRaices
        /// </summary>
        public BienesRaicesBusiness()
        {
            DABienesRaices = new BienesRaicesData();
        }

        /// <summary>
        /// Crea un BienesRaices
        /// </summary>
        /// <param name="pBienesRaices">Entidad BienesRaices</param>
        /// <returns>Entidad BienesRaices creada</returns>
        public BienesRaices CrearBienesRaices(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pBienesRaices = DABienesRaices.CrearBienesRaices(pBienesRaices, pUsuario);

                    ts.Complete();
                }

                return pBienesRaices;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BienesRaicesBusiness", "CrearBienesRaices", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un BienesRaices
        /// </summary>
        /// <param name="pBienesRaices">Entidad BienesRaices</param>
        /// <returns>Entidad BienesRaices modificada</returns>
        public BienesRaices ModificarBienesRaices(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pBienesRaices = DABienesRaices.ModificarBienesRaices(pBienesRaices, pUsuario);

                    ts.Complete();
                }

                return pBienesRaices;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BienesRaicesBusiness", "ModificarBienesRaices", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un BienesRaices
        /// </summary>
        /// <param name="pId">Identificador de BienesRaices</param>
        public void EliminarBienesRaices(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABienesRaices.EliminarBienesRaices(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BienesRaicesBusiness", "EliminarBienesRaices", ex);
            }
        }

        /// <summary>
        /// Obtiene un BienesRaices
        /// </summary>
        /// <param name="pId">Identificador de BienesRaices</param>
        /// <returns>Entidad BienesRaices</returns>
        public BienesRaices ConsultarBienesRaices(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DABienesRaices.ConsultarBienesRaices(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BienesRaicesBusiness", "ConsultarBienesRaices", ex);
                return null;
            }
        }
       

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pBienesRaices">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de BienesRaices obtenidos</returns>
        public List<BienesRaices> ListarBienesRaices(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            try
            {
                return DABienesRaices.ListarBienesRaices(pBienesRaices, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BienesRaicesBusiness", "ListarBienesRaices", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pBienesRaices">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de BienesRaices obtenidos</returns>
        public List<BienesRaices> ListarBienesRaicesRepo(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            try
            {
                return DABienesRaices.ListarBienesRaicesRepo(pBienesRaices, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BienesRaicesBusiness", "ListarBienesRaicesRepo", ex);
                return null;
            }
        }


    }
}