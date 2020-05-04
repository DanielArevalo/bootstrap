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
    /// Objeto de negocio para TiposDocumento
    /// </summary>
    public class TiposDocumentoBusiness : GlobalBusiness
    {
        private TiposDocumentoData DATiposDocumento;

        /// <summary>
        /// Constructor del objeto de negocio para TiposDocumento
        /// </summary>
        public TiposDocumentoBusiness()
        {
            DATiposDocumento = new TiposDocumentoData();
        }

        /// <summary>
        /// Crea un TiposDocumento
        /// </summary>
        /// <param name="pTiposDocumento">Entidad TiposDocumento</param>
        /// <returns>Entidad TiposDocumento creada</returns>
        public TiposDocumento CrearTiposDocumento(TiposDocumento pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTiposDocumento = DATiposDocumento.CrearTiposDocumento(pTiposDocumento, pUsuario);

                    ts.Complete();
                }

                return pTiposDocumento;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "CrearTiposDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un TiposDocumento
        /// </summary>
        /// <param name="pTiposDocumento">Entidad TiposDocumento</param>
        /// <returns>Entidad TiposDocumento modificada</returns>
        public TiposDocumento ModificarTiposDocumento(TiposDocumento pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTiposDocumento = DATiposDocumento.ModificarTiposDocumento(pTiposDocumento, pUsuario);

                    ts.Complete();
                }

                return pTiposDocumento;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ModificarTiposDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TiposDocumento
        /// </summary>
        /// <param name="pId">Identificador de TiposDocumento</param>
        public void EliminarTiposDocumento(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATiposDocumento.EliminarTiposDocumento(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "EliminarTiposDocumento", ex);
            }
        }

        /// <summary>
        /// Obtiene un TiposDocumento
        /// </summary>
        /// <param name="pId">Identificador de TiposDocumento</param>
        /// <returns>Entidad TiposDocumento</returns>
        public TiposDocumento ConsultarTiposDocumento(Int64 pId, Usuario pUsuario, string tipoDoc = null)
        {
            try
            {
                return DATiposDocumento.ConsultarTiposDocumento(pId, pUsuario, tipoDoc);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ConsultarTiposDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un TiposDocumento a través del tipo
        /// </summary>
        /// <param name="pTipo">Tipo</param>
        /// <returns>Entidad TiposDocumento</returns>
        public List<TiposDocumento> ConsultarTiposDocumento(String pTipo, Usuario pUsuario)
        {
            try
            {
                return DATiposDocumento.ConsultarTiposDocumento(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ConsultarTiposDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un TiposDocumento
        /// </summary>
        /// <param name="pId">Identificador de TiposDocumento</param>
        /// <returns>Entidad TiposDocumento</returns>
        public TiposDocumento ConsultarParametroTipoDocumento(Usuario pUsuario)
        {
            try
            {
                return DATiposDocumento.ConsultarParametroTipoDocumento( pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ConsultarParametroTipoDocumento", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un TiposDocumento
        /// </summary>
        /// <param name="pId">Identificador de TiposDocumento</param>
        /// <returns>Entidad TiposDocumento</returns>
        public TiposDocumento ConsultarMaxTiposDocumento(Usuario pUsuario)
        {
            try
            {
                return DATiposDocumento.ConsultarMaxTiposDocumento(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ConsultarMaxTiposDocumento", ex);
                return null;
            }
        }


        public List<TipoDocumento> ConsultarTipoDoc(Usuario pUsuario)
        {
            try
            {
                return DATiposDocumento.ConsultarTipoDoc(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ConsultarTipoDoc", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTiposDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TiposDocumento obtenidos</returns>
        public List<TiposDocumento> ListarTiposDocumento(TiposDocumento pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return DATiposDocumento.ListarTiposDocumento(pTiposDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ListarTiposDocumento", ex);
                return null;
            }
        }
       


        public TiposDocumento ConsultarTiposDocumentoCobranzas(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DATiposDocumento.ConsultarTiposDocumentoCobranzas(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ConsultarTiposDocumentoCobranzas", ex);
                return null;
            }
        }
        public TiposDocumento ConsultarDocumentoOrden(string pId, Usuario pUsuario)
        {
            try
            {
                return DATiposDocumento.ConsultarDocumentoOrden(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoBusiness", "ConsultarDocumentoOrden", ex);
                return null;
            }
        }
    }
}