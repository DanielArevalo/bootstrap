using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para TipoComprobante
    /// </summary>
    public class TipoComprobanteBusiness : GlobalBusiness
    {
        private TipoComprobanteData DATipoComprobante;

        /// <summary>
        /// Constructor del objeto de negocio para TipoComprobante
        /// </summary>
        public TipoComprobanteBusiness()
        {
            DATipoComprobante = new TipoComprobanteData();
        }

        /// <summary>
        /// Crea un TipoComprobante
        /// </summary>
        /// <param name="pTipoComprobante">Entidad TipoComprobante</param>
        /// <returns>Entidad TipoComprobante creada</returns>
        public TipoComprobante CrearTipoComprobante(TipoComprobante pTipoComprobante, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoComprobante = DATipoComprobante.CrearTipoComprobante(pTipoComprobante, pUsuario);

                    ts.Complete();
                }

                return pTipoComprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteBusiness", "CrearTipoComprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TipoComprobante
        /// </summary>
        /// <param name="pTipoComprobante">Entidad TipoComprobante</param>
        /// <returns>Entidad TipoComprobante modificada</returns>
        public TipoComprobante ModificarTipoComprobante(TipoComprobante pTipoComprobante, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTipoComprobante = DATipoComprobante.ModificarTipoComprobante(pTipoComprobante, pUsuario);

                    ts.Complete();
                }

                return pTipoComprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteBusiness", "ModificarTipoComprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TipoComprobante
        /// </summary>
        /// <param name="pId">Identificador de TipoComprobante</param>
        public void EliminarTipoComprobante(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATipoComprobante.EliminarTipoComprobante(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteBusiness", "EliminarTipoComprobante", ex);
            }
        }

        /// <summary>
        /// Obtiene un TipoComprobante
        /// </summary>
        /// <param name="pId">Identificador de TipoComprobante</param>
        /// <returns>Entidad TipoComprobante</returns>
        public TipoComprobante ConsultarTipoComprobante(Int64 pId, Usuario vUsuario)
        {
            try
            {
                TipoComprobante TipoComprobante = new TipoComprobante();

                TipoComprobante = DATipoComprobante.ConsultarTipoComprobante(pId, vUsuario);

                return TipoComprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteBusiness", "ConsultarTipoComprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoComprobante">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoComprobante obtenidos</returns>
        public List<TipoComprobante> ListarTipoComprobante(TipoComprobante pTipoComprobante, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DATipoComprobante.ListarTipoComprobante(pTipoComprobante, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteBusiness", "ListarTipoComprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un TipoComprobante
        /// </summary>
        /// <param name="pId">Identificador de TipoComprobante</param>
        /// <returns>Entidad TipoComprobante</returns>
        public TipoComprobante ValidarTipoComprobante(Int64 pTipoComprobante, Usuario pUsuario)
        {
            try
            {
                TipoComprobante TipoComprobante = new TipoComprobante();

                try
                {
                    TipoComprobante = DATipoComprobante.ValidarTipoComprobante(pTipoComprobante, pUsuario);
                }
                catch (ExceptionBusiness ex)
                {
                    if (ex.Message.Contains("El registro no existe. Verifique por favor."))
                        throw new ExceptionBusiness("Tipo de Comprobante no encontrado.");
                    else
                        throw new ExceptionBusiness(ex.Message);
                }

                return TipoComprobante;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteBusiness", "ValidarTipoComprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTipoIden">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Comprobantes obtenidos</returns>
        public List<TipoComprobante> ListarTipoComp(TipoComprobante pTipoComp, Usuario pUsuario)
        {
            try
            {
                return DATipoComprobante.ListarTipoComp(pTipoComp, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCompBusiness", "ListarTipoComp", ex);
                return null;
            }
        }

        public List<TipoComprobante> ListarTipoCompTodos(TipoComprobante pTipoComp, Usuario pUsuario)
        {
            try
            {
                return DATipoComprobante.ListarTipoCompTodos(pTipoComp, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoCompBusiness", "ListarTipoCompTodos", ex);
                return null;
            }
        }

    }
}