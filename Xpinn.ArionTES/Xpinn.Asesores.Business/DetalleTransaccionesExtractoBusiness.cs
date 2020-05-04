using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    public class DetalleTransaccionesExtractoBusiness : GlobalData
    {
        private DetalleTransaccionesExtractoData DADetalleTransaccionesExtracto;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public DetalleTransaccionesExtractoBusiness()
        {
            DADetalleTransaccionesExtracto = new DetalleTransaccionesExtractoData();
        }

              
        public List<DetalleTransaccionesExtracto> ListarDetalleTransaccionesExtractos(DetalleTransaccionesExtracto pDetalleTransaccionesExtracto, Usuario pUsuario)
        {
            try
            {
                return DADetalleTransaccionesExtracto.ListarDetalleTransaccionesExtractos(pDetalleTransaccionesExtracto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DetalleTransaccionesExtractoBusiness", "ListarDetalleTransaccionesExtractos", ex);
                return null;
            }
        }
    }
}