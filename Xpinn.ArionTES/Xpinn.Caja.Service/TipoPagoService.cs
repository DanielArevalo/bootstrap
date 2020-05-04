using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoPagoService
    {
        private TipoPagoBusiness BOTipoPago;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoPago
        /// </summary>
        public TipoPagoService()
        {
            BOTipoPago = new TipoPagoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30702"; } }

        /// <summary>
        /// Servicio para crear TipoPago
        /// </summary>
        /// <param name="pEntity">Entidad TipoPago</param>
        /// <returns>Entidad TipoPago creada</returns>
        public TipoPago CrearTipoPago(TipoPago pTipoPago, Usuario pUsuario)
        {
            try
            {
                return BOTipoPago.CrearTipoPago(pTipoPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPagoService", "CrearTipoPago", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TipoPago
        /// </summary>
        /// <param name="pTipoPago">Entidad TipoPago</param>
        /// <returns>Entidad TipoPago modificada</returns>
        public TipoPago ModificarTipoPago(TipoPago pTipoPago, Usuario pUsuario)
        {
            try
            {
                return BOTipoPago.ModificarTipoPago(pTipoPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPagoService", "ModificarTipoPago", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoPago
        /// </summary>
        /// <param name="pId">identificador de TipoPago</param>
        public void EliminarTipoPago(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoPago.EliminarTipoPago(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoPago", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoPago
        /// </summary>
        /// <param name="pId">identificador de TipoPago</param>
        /// <returns>Entidad TipoPago</returns>
        public TipoPago ConsultarTipoPago(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoPago.ConsultarTipoPago(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPagoService", "ConsultarTipoPago", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TipoPagos a partir de unos filtros
        /// </summary>
        /// <param name="pTipoPago">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoPago obtenidos</returns>
        public List<TipoPago> ListarTipoPago(TipoPago pTipoPago, Usuario pUsuario)
        {
            try
            {
                return BOTipoPago.ListarTipoPago(pTipoPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPagoService", "ListarTipoPago", ex);
                return null;
            }
        }



        public List<TipoPago> ListarTipoPagoCon(TipoPago pTipoPago, Usuario pUsuario)
        {
            try
            {
                return BOTipoPago.ListarTipoPagoCon(pTipoPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoPagoService", "ListarTipoPagoCon", ex);
                return null;
            }
        }

    }
}