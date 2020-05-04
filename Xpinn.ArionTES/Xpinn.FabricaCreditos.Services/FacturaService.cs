using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Generar factura
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class FacturaService
    {
        private FacturaBusiness BOfactura;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Programa
        /// </summary>
        public FacturaService()
        {
            BOfactura = new FacturaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100202"; } }

        /// <summary>
        /// Obtiene un número de factura
        /// </summary>
        /// <param name="pId">Identificador de la Central</param>
        /// <returns>Central consultado</returns>
        public Factura ObtenerNumeroFactura(Usuario pUsuario)
        {
            try
            {
                return BOfactura.ObtenerNumeroFactura(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FacturaService", "ObtenerNumeroFactura", ex);
                return null;
            }
        }

    }

}
