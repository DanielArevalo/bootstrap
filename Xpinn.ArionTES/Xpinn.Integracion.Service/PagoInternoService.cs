using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Integracion.Business;
using Xpinn.Integracion.Entities;
using Xpinn.Util;
using System.Data;
using System.ServiceModel;

namespace Xpinn.Integracion.Services
{
    public class PagoInternoService
    {

        private PagoInternoBusiness BOPago;
        private ExcepcionBusiness BOExcepcion;

        //Codigos 
        public string CodigoProgramaConsultaTransaccionesWeb { get { return "170138"; } }

        public PagoInternoService()
        {
            BOPago = new PagoInternoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<ProductoPorPagar> listarProductosPorPagar(long cod_persona, string filtro, Usuario vUsuario)
        {
            try
            {
                return BOPago.listarProductosPorPagar(cod_persona, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagoInternoService", "listarProductosPorPagar", ex);
                return null;
            }
        }

        public List<ProductoOrigenPago> listarProductoOrigenPago(long cod_persona, string filtro, Usuario vUsuario)
        {
            try
            {
                return BOPago.listarProductoOrigenPago(cod_persona, filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagoInternoService", "listarProductosOrigenPago", ex);
                return null;
            }
        }

        public Int32 procesarPagoInterno(PagoInterno pago, Usuario vUsuario)
        {
            try
            {
                return BOPago.procesarPagoInterno(pago, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagoInternoService", "procesarPagoInterno", ex);
                return 0;
            }
        }

        public List<PagoInterno> listarPagosInternos(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOPago.listarPagosInternos(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagoInternoService", "listarPagosInternos", ex);
                return null;
            }
        }
    }
}