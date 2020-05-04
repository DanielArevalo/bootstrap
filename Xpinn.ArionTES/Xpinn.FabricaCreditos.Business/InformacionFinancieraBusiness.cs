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
    /// Objeto de negocio para InformacionFinanciera
    /// </summary>
    public class InformacionFinancieraBusiness : GlobalBusiness
    {
        private InformacionFinancieraData DAInformacionFinanciera;

        /// <summary>
        /// Constructor del objeto de negocio para InformacionFinanciera
        /// </summary>
        public InformacionFinancieraBusiness()
        {
            DAInformacionFinanciera = new InformacionFinancieraData();
        }

        /// <summary>
        /// Crea un InformacionFinanciera
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad InformacionFinanciera</param>
        /// <returns>Entidad InformacionFinanciera creada</returns>
        public InformacionFinanciera CrearInformacionFinanciera(InformacionFinanciera pInformacionFinanciera, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pInformacionFinanciera = DAInformacionFinanciera.CrearInformacionFinanciera(pInformacionFinanciera, pUsuario);

                    ts.Complete();
                }

                return pInformacionFinanciera;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "CrearInformacionFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un InformacionFinanciera
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad InformacionFinanciera</param>
        /// <returns>Entidad InformacionFinanciera modificada</returns>
        public InformacionFinanciera ModificarInformacionFinanciera(InformacionFinanciera pInformacionFinanciera, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pInformacionFinanciera = DAInformacionFinanciera.ModificarInformacionFinanciera(pInformacionFinanciera, pUsuario);

                    ts.Complete();
                }

                return pInformacionFinanciera;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "ModificarInformacionFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un InformacionFinanciera
        /// </summary>
        /// <param name="pId">Identificador de InformacionFinanciera</param>
        public void EliminarInformacionFinanciera(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAInformacionFinanciera.EliminarInformacionFinanciera(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "EliminarInformacionFinanciera", ex);
            }
        }

        /// <summary>
        /// Obtiene un InformacionFinanciera
        /// </summary>
        /// <param name="pId">Identificador de InformacionFinanciera</param>
        /// <returns>Entidad InformacionFinanciera</returns>
        public InformacionFinanciera ConsultarInformacionFinanciera(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAInformacionFinanciera.ConsultarInformacionFinanciera(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "ConsultarInformacionFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanciera(InformacionFinanciera pInformacionFinanciera, Usuario pUsuario)
        {
            try
            {
                return DAInformacionFinanciera.ListarInformacionFinanciera(pInformacionFinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "ListarInformacionFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera para reporte obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanNegRepo(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return DAInformacionFinanciera.ListarInformacionFinanNegRepo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "ListarInformacionFinanNegRepo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera para reporte obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanFamRepo(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return DAInformacionFinanciera.ListarInformacionFinanFamRepo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "ListarInformacionFinanFamRepo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera para reporte obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanFamRepoeg(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return DAInformacionFinanciera.ListarInformacionFinanFamRepoeg(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "ListarInformacionFinanFamRepoeg", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera para reporte obtenidos</returns>
        public List<InformacionFinanciera> ListarActivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return DAInformacionFinanciera.ListarActivos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "ListarActivos", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera para reporte obtenidos</returns>
        public List<InformacionFinanciera> ListarPasivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return DAInformacionFinanciera.ListarPasivos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "ListarPasivos", ex);
                return null;
            }
        }


        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera para reporte obtenidos</returns>
        public List<InformacionFinanciera> ListarbalanceFamActivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return DAInformacionFinanciera.ListarbalanceFamActivos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "ListarbalanceFamActivos", ex);
                return null;
            }
        }


        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera para reporte obtenidos</returns>
        public List<InformacionFinanciera> ListarbalanceFamPasivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return DAInformacionFinanciera.ListarbalanceFamPasivos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraBusiness", "ListarbalanceFamPasivos", ex);
                return null;
            }
        }
 
    }
}