using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PagosVentanillaService
    {
        private TransaccionCajaBusiness BOTransaccionCaja;
        private PagosVentanillaBusiness BOPagosVentanilla;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TransaccionCaja
        /// </summary>
        public PagosVentanillaService()
        {
            BOTransaccionCaja = new TransaccionCajaBusiness();
            BOPagosVentanilla = new PagosVentanillaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40101"; } }
        public string CodigoProgramaDevol { get { return "180303"; } }
        public string CodigoProgramaCart { get { return ""; } } //OPCION NOTAS A PRODUCTOS :::: MODULO CARTERA ::::

        /// <summary>
        /// Servicio para crear TransaccionCaja
        /// </summary>
        /// <param name="pEntity">Entidad TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja creada</returns>
        public TransaccionCaja AplicarPagoVentanilla(TransaccionCaja pTransaccionCaja, PersonaTransaccion perTran, GridView gvTransacciones, GridView gvFormaPago, GridView gvCheques, string pObservacion, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BOPagosVentanilla.AplicarPagoVentanilla(pTransaccionCaja, perTran, gvTransacciones, gvFormaPago, gvCheques, pObservacion, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }
        }

        public TransaccionCaja AplicarPagoVentanilla(TransaccionCaja pTransaccionCaja, GridView gvTransacciones, GridView gvFormaPago, GridView gvCheques, string pObservacion, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BOPagosVentanilla.AplicarPagoVentanilla(pTransaccionCaja, gvTransacciones, gvFormaPago, gvCheques, pObservacion, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }
        }


        public TransaccionCaja ActualizarSaldoPersonaAfiliacion(TransaccionCaja pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOPagosVentanilla.ActualizarSaldoPersonaAfiliacion(pEntidad,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosVentanillaService", "ActualizarSaldoPersonaAfiliacion", ex);
                return null;
            }
        }

        public string ParametroGeneral(Int64 pCodigo, Usuario pUsuario)
        {
            return BOPagosVentanilla.ParametroGeneral(pCodigo, pUsuario);
        }


    }
}