using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Data;
using System.Transactions;
using System.Data;
using Xpinn.Util;

namespace Xpinn.NIIF.Business
{
    public class AmortizacionNIFBusiness
    {
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();
        /// Objeto de negocio para Credito
        /// 
        private AmortizacionNIFData DAAmortizacion;

                /// <summary>
        /// Constructor del objeto de negocio para Atributo
        /// </summary>
        public AmortizacionNIFBusiness()
        {
            DAAmortizacion = new AmortizacionNIFData();
        }
        

        public List<AmortizacionNIF> ListarAmortizacionNIIF(DateTime vFecha, Usuario vUsuario)
        {
            try
            {
                return DAAmortizacion.ListarAmortizacionNIIF(vFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizacionNIIFBusiness", "ListarAmortizacionNIIF", ex);
                return null;
            }
        }


        public void ModificarEstadoFechaNIIF(DateTime vFecha, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAmortizacion.ModificarEstadoFechaNIIF(vFecha, vUsuario);
                    ts.Complete();
                }

                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizacionNIIFBusiness", "ModificarEstadoFechaNIIF", ex);
              
            }

        }

        public List<DetalleAmortizacionNIIF> ListarDetalleAmortizacionNIIF(DateTime vFecha, Int64 vNumeroRadicacion, Usuario vUsuario)
        {
            try
            {
                return DAAmortizacion.ListarDetalleAmortizacionNIIF(vFecha, vNumeroRadicacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizacionNIIFBusiness", "ListarDetalleAmortizacionNIIF", ex);
                return null;
            }
        }

    }
}
