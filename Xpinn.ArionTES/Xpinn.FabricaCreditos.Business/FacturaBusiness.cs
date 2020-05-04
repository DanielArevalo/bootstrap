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
    /// Objeto de negocio para la factura
    /// </summary>
    public class FacturaBusiness : GlobalData
    {
        private FacturaData DAFactura;

        /// <summary>
        /// Constructor del objeto de negocio para Programa
        /// </summary>
        public FacturaBusiness()
        {
            DAFactura = new FacturaData();
        }

        /// <summary>
        /// Obtiene una Central
        /// </summary>
        /// <param name="pId">Identificador de la Central</param>
        /// <returns>Programa consultado</returns>
        public Factura ObtenerNumeroFactura(Usuario pUsuario)
        {
            try
            {
                return DAFactura.ObtenerNumeroFactura(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FacturaBusiness", "ObtenerNumeroFactura", ex);
                return null;
            }
        }

    }
}
