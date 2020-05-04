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
    /// Objeto de negocio para InformacionNegocio
    /// </summary>
    public class InformacionNegocioBusiness : GlobalData
    {
        private InformacionNegocioData DAInformacionNegocio;

        /// <summary>
        /// Constructor del objeto de negocio para InformacionNegocio
        /// </summary>
        public InformacionNegocioBusiness()
        {
            DAInformacionNegocio = new InformacionNegocioData();
        }

        /// <summary>
        /// Crea un InformacionNegocio
        /// </summary>
        /// <param name="pInformacionNegocio">Entidad InformacionNegocio</param>
        /// <returns>Entidad InformacionNegocio creada</returns>
        public InformacionNegocio CrearInformacionNegocio(InformacionNegocio pInformacionNegocio, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pInformacionNegocio = DAInformacionNegocio.CrearInformacionNegocio(pInformacionNegocio, pUsuario);

                    ts.Complete();
                }

                return pInformacionNegocio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionNegocioBusiness", "CrearInformacionNegocio", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un InformacionNegocio
        /// </summary>
        /// <param name="pInformacionNegocio">Entidad InformacionNegocio</param>
        /// <returns>Entidad InformacionNegocio modificada</returns>
        public InformacionNegocio ModificarInformacionNegocio(InformacionNegocio pInformacionNegocio, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pInformacionNegocio = DAInformacionNegocio.ModificarInformacionNegocio(pInformacionNegocio, pUsuario);

                    ts.Complete();
                }

                return pInformacionNegocio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionNegocioBusiness", "ModificarInformacionNegocio", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un InformacionNegocio
        /// </summary>
        /// <param name="pId">Identificador de InformacionNegocio</param>
        public void EliminarInformacionNegocio(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAInformacionNegocio.EliminarInformacionNegocio(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionNegocioBusiness", "EliminarInformacionNegocio", ex);
            }
        }

        /// <summary>
        /// Obtiene un InformacionNegocio
        /// </summary>
        /// <param name="pId">Identificador de InformacionNegocio</param>
        /// <returns>Entidad InformacionNegocio</returns>
        public InformacionNegocio ConsultarInformacionNegocio(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAInformacionNegocio.ConsultarInformacionNegocio(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionNegocioBusiness", "ConsultarInformacionNegocio", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pInformacionNegocio">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionNegocio obtenidos</returns>
        public List<InformacionNegocio> ListarInformacionNegocio(InformacionNegocio pInformacionNegocio, Usuario pUsuario)
        {
            try
            {
                return DAInformacionNegocio.ListarInformacionNegocio(pInformacionNegocio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionNegocioBusiness", "ListarInformacionNegocio", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de tipos documento dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos documento obtenidos</returns>
        public List<InformacionNegocio> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return DAInformacionNegocio.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionNegocioBusiness", "ListarInformacionNegocio", ex);
                return null;
            }
        }

    }

    }
