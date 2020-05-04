using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoListaRecaudoService
    {
        private TipoListaRecaudoBusiness BOTipoListaRecaudo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoListaRecaudo
        /// </summary>
        public TipoListaRecaudoService()
        {
            BOTipoListaRecaudo = new TipoListaRecaudoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "180203"; } }

        /// <summary>
        /// Servicio para crear TipoListaRecaudo
        /// </summary>
        /// <param name="pEntity">Entidad TipoListaRecaudo</param>
        /// <returns>Entidad TipoListaRecaudo creada</returns>
        public TipoListaRecaudo CrearTipoListaRecaudo(TipoListaRecaudo vTipoListaRecaudo, Usuario pUsuario)
        {
            try
            {
                return BOTipoListaRecaudo.CrearTipoListaRecaudo(vTipoListaRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoListaRecaudoService", "CrearTipoListaRecaudo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TipoListaRecaudo
        /// </summary>
        /// <param name="pTipoListaRecaudo">Entidad TipoListaRecaudo</param>
        /// <returns>Entidad TipoListaRecaudo modificada</returns>
        public TipoListaRecaudo ModificarTipoListaRecaudo(TipoListaRecaudo vTipoListaRecaudo, Usuario pUsuario)
        {
            try
            {
                return BOTipoListaRecaudo.ModificarTipoListaRecaudo(vTipoListaRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoListaRecaudoService", "ModificarTipoListaRecaudo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoListaRecaudo
        /// </summary>
        /// <param name="pId">identificador de TipoListaRecaudo</param>
        public void EliminarTipoListaRecaudo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoListaRecaudo.EliminarTipoListaRecaudo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoListaRecaudo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoListaRecaudo
        /// </summary>
        /// <param name="pId">identificador de TipoListaRecaudo</param>
        /// <returns>Entidad TipoListaRecaudo</returns>
        public TipoListaRecaudo ConsultarTipoListaRecaudo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoListaRecaudo.ConsultarTipoListaRecaudo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoListaRecaudoService", "ConsultarTipoListaRecaudo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TipoListaRecaudos a partir de unos filtros
        /// </summary>
        /// <param name="pTipoListaRecaudo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoListaRecaudo obtenidos</returns>
        public List<TipoListaRecaudo> ListarTipoListaRecaudo(TipoListaRecaudo vTipoListaRecaudo, Usuario pUsuario)
        {
            try
            {
                return BOTipoListaRecaudo.ListarTipoListaRecaudo(vTipoListaRecaudo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoListaRecaudoService", "ListarTipoListaRecaudo", ex);
                return null;
            }
        }

        public void EliminarTipoListaRecaudoDetalle(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoListaRecaudo.EliminarTipoListaRecaudoDetalle(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoListaRecaudoDetalle", ex);
            }
        }

    }
}