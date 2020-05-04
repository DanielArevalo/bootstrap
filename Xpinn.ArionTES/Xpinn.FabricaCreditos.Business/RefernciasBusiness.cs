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
    /// Objeto de negocio para Referncias
    /// </summary>
    public class RefernciasBusiness : GlobalData
    {
        private RefernciasData DAReferncias;

        /// <summary>
        /// Constructor del objeto de negocio para Referncias
        /// </summary>
        public RefernciasBusiness()
        {
            DAReferncias = new RefernciasData();
        }

        /// <summary>
        /// Crea un Referncias
        /// </summary>
        /// <param name="pReferncias">Entidad Referncias</param>
        /// <returns>Entidad Referncias creada</returns>
        public Referncias CrearReferncias(Referncias pReferncias, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pReferncias = DAReferncias.CrearReferncias(pReferncias, pUsuario);

                    ts.Complete();
                }

                return pReferncias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefernciasBusiness", "CrearReferncias", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Referncias
        /// </summary>
        /// <param name="pReferncias">Entidad Referncias</param>
        /// <returns>Entidad Referncias modificada</returns>
        public Referncias ModificarReferncias(Referncias pReferncias, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pReferncias = DAReferncias.ModificarReferncias(pReferncias, pUsuario);

                    ts.Complete();
                }

                return pReferncias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefernciasBusiness", "ModificarReferncias", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un Referncias
        /// </summary>
        /// <param name="pId">Identificador de Referncias</param>
        public void EliminarReferncias(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAReferncias.EliminarReferncias(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefernciasBusiness", "EliminarReferncias", ex);
            }
        }

        /// <summary>
        /// Obtiene un Referncias
        /// </summary>
        /// <param name="pId">Identificador de Referncias</param>
        /// <returns>Entidad Referncias</returns>
        public Referncias ConsultarReferncias(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAReferncias.ConsultarReferncias(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefernciasBusiness", "ConsultarReferncias", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pReferncias">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Referncias obtenidos</returns>
        public List<Referncias> ListarReferncias(Referncias pReferncias, Usuario pUsuario)
        {
            try
            {
                return DAReferncias.ListarReferncias(pReferncias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefernciasBusiness", "ListarReferncias", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pReferncias">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Referncias obtenidos</returns>
        public List<Referncias> ListarReferenciasRepo(Referncias pReferncias, Usuario pUsuario)
        {
            try
            {
                return DAReferncias.ListarReferenciasRepo(pReferncias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefernciasBusiness", "ListarReferenciasRepo", ex);
                return null;
            }
        }





        /// <summary>
        /// Obtiene la lista de tipos documento dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos documento obtenidos</returns>
        public List<Referncias> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return DAReferncias.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarTiposDoc", ex);
                return null;
            }
        }

    }
}