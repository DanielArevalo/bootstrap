using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoComprobanteService
    {
        private TipoComprobanteBusiness BOTipoComprobante;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoComprobante
        /// </summary>
        public TipoComprobanteService()
        {
            BOTipoComprobante = new TipoComprobanteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30701"; } }

        /// <summary>
        /// Servicio para crear TipoComprobante
        /// </summary>
        /// <param name="pEntity">Entidad TipoComprobante</param>
        /// <returns>Entidad TipoComprobante creada</returns>
        public TipoComprobante CrearTipoComprobante(TipoComprobante vTipoComprobante, Usuario pUsuario)
        {
            try
            {
                return BOTipoComprobante.CrearTipoComprobante(vTipoComprobante, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteService", "CrearTipoComprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TipoComprobante
        /// </summary>
        /// <param name="pTipoComprobante">Entidad TipoComprobante</param>
        /// <returns>Entidad TipoComprobante modificada</returns>
        public TipoComprobante ModificarTipoComprobante(TipoComprobante vTipoComprobante, Usuario pUsuario)
        {
            try
            {
                return BOTipoComprobante.ModificarTipoComprobante(vTipoComprobante, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteService", "ModificarTipoComprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoComprobante
        /// </summary>
        /// <param name="pId">identificador de TipoComprobante</param>
        public void EliminarTipoComprobante(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoComprobante.EliminarTipoComprobante(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoComprobante", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoComprobante
        /// </summary>
        /// <param name="pId">identificador de TipoComprobante</param>
        /// <returns>Entidad TipoComprobante</returns>
        public TipoComprobante ConsultarTipoComprobante(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoComprobante.ConsultarTipoComprobante(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteService", "ConsultarTipoComprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TipoComprobantes a partir de unos filtros
        /// </summary>
        /// <param name="pTipoComprobante">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoComprobante obtenidos</returns>
        public List<TipoComprobante> ListarTipoComprobante(TipoComprobante vTipoComprobante, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOTipoComprobante.ListarTipoComprobante(vTipoComprobante, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteService", "ListarTipoComprobante", ex);
                return null;
            }
        }

        public List<TipoComprobante> ListarTipoComp(TipoComprobante pTipoComp, Usuario pUsuario)
        {
            try
            {
                return BOTipoComprobante.ListarTipoComp(pTipoComp, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteService", "ListarTipoComp", ex);
                return null;
            }
        }

        /// <summary>
        /// Valida la existencia y acceso del TipoComprobante en el sistema
        /// </summary>
        /// <param name="pTipoComprobante">nombre de TipoComprobante</param>
        /// <param name="pPassword">clave de acceso</param>
        /// <returns>Entidad TipoComprobante</returns>
        public TipoComprobante ValidarTipoComprobante(Int64 pTipoComprobante, Usuario pUsuario)
        {
            try
            {
                return BOTipoComprobante.ValidarTipoComprobante(pTipoComprobante, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteService", "ValidarTipoComprobante", ex);
                return null;
            }
        }

        public List<TipoComprobante> ListarTipoCompTodos(TipoComprobante pTipoComp, Usuario pUsuario)
        {
            try
            {
                return BOTipoComprobante.ListarTipoCompTodos(pTipoComp, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoComprobanteService", "ListarTipoCompTodos", ex);
                return null;
            }
        }

    }
}