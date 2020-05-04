using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TiposDocumentoService
    {
        private TiposDocumentoBusiness BOTiposDocumento;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TiposDocumento
        /// </summary>
        public TiposDocumentoService()
        {
            BOTiposDocumento = new TiposDocumentoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100211"; } }

        /// <summary>
        /// Servicio para crear TiposDocumento
        /// </summary>
        /// <param name="pEntity">Entidad TiposDocumento</param>
        /// <returns>Entidad TiposDocumento creada</returns>
        public TiposDocumento CrearTiposDocumento(TiposDocumento pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.CrearTiposDocumento(pTiposDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "CrearTiposDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TiposDocumento
        /// </summary>
        /// <param name="pTiposDocumento">Entidad TiposDocumento</param>
        /// <returns>Entidad TiposDocumento modificada</returns>
        public TiposDocumento ModificarTiposDocumento(TiposDocumento pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ModificarTiposDocumento(pTiposDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ModificarTiposDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TiposDocumento
        /// </summary>
        /// <param name="pId">identificador de TiposDocumento</param>
        public void EliminarTiposDocumento(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTiposDocumento.EliminarTiposDocumento(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTiposDocumento", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TiposDocumento
        /// </summary>
        /// <param name="pId">identificador de TiposDocumento</param>
        /// <returns>Entidad TiposDocumento</returns>
        public TiposDocumento ConsultarTiposDocumento(Int64 pId, Usuario pUsuario, string tipoDoc = null)
        {
            try
            {
                return BOTiposDocumento.ConsultarTiposDocumento(pId, pUsuario, tipoDoc);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ConsultarTiposDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener TiposDocumento a travez del Tipo
        /// </summary>
        /// <param name="pTipo">identificador de TiposDocumento</param>
        /// <returns>Entidad TiposDocumento</returns>
        public List<TiposDocumento> ConsultarTiposDocumento(String pTipo, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ConsultarTiposDocumento(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ConsultarTiposDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener TiposDocumento
        /// </summary>
        /// <param name="pId">identificador de TiposDocumento</param>
        /// <returns>Entidad TiposDocumento</returns>
        public TiposDocumento ConsultarParametroTipoDocumento(Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ConsultarParametroTipoDocumento(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ConsultarParametroTipoDocumento", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener TiposDocumento
        /// </summary>
        /// <param name="pId">identificador de TiposDocumento</param>
        /// <returns>Entidad TiposDocumento</returns>
        public TiposDocumento ConsultarMaxTiposDocumento(Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ConsultarMaxTiposDocumento(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ConsultarMaxTiposDocumento", ex);
                return null;
            }
        }

        public List<TipoDocumento> ConsultarTipoDoc(Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ConsultarTipoDoc(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ConsultarTipoDoc", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de TiposDocumentos a partir de unos filtros
        /// </summary>
        /// <param name="pTiposDocumento">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TiposDocumento obtenidos</returns>
        public List<TiposDocumento> ListarTiposDocumento(TiposDocumento pTiposDocumento, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ListarTiposDocumento(pTiposDocumento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ListarTiposDocumento", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener TIPOSDOCCOBRANZAS
        /// </summary>
        /// <param name="pId">identificador de TIPOSDOCCOBRANZAS</param>
        /// <returns>Entidad TIPOSDOCCOBRANZAS</returns>
        public TiposDocumento ConsultarTiposDocumentoCobranzas(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ConsultarTiposDocumentoCobranzas(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ConsultarTiposDocumentoCobranzas", ex);
                return null;
            }
        }

        public TiposDocumento ConsultarDocumentoOrden(string pId, Usuario pUsuario)
        {
            try
            {
                return BOTiposDocumento.ConsultarDocumentoOrden(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDocumentoService", "ConsultarDocumentoOrden", ex);
                return null;
            }
        }
    }
}