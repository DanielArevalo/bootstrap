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
    public class CarteraNIFBusiness
    {
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();
        /// Objeto de negocio para Credito
        /// 
        private CarteraNIFData DACartera;

                /// <summary>
        /// Constructor del objeto de negocio para Atributo
        /// </summary>
        public CarteraNIFBusiness()
        {
            DACartera = new CarteraNIFData();
        }

        public AmortizacionNIF CrearAmortizacionNIIF(AmortizacionNIF pPlanCuentasNIIF, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //pPlanCuentasNIIF = DAAmortizacion.CrearAmortizacionNIIF(pPlanCuentasNIIF, vUsuario);
                    ts.Complete();
                }

                return pPlanCuentasNIIF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AmortizacionNIIFBusiness", "CrearAmortizacionNIIF", ex);
                return null;
            }

        }


        public void EliminarCarteraNIIF(CarteraNIF pCarteraNIIF, DateTime vFecha, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACartera.EliminarCarteraNIIF(pCarteraNIIF,vFecha, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraNIIFBusiness", "EliminarCarteraNIIF", ex);
            }
               
        }




        public List<CarteraNIF> ListarCarteraNIIF(DateTime vFecha, string Orden, Usuario vUsuario)
        {
            try
            {
                return DACartera.ListarCarteraNIIF(vFecha,Orden, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraNIIFBusiness", "ListarCarteraNIIF", ex);
                return null;
            }
        }


        public void ConsultarCarteraNIIF(DateTime vFecha, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACartera.ConsultarCarteraNIIF(vFecha, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraNIIFBusiness", "ConsultarCarteraNIIF", ex);

            }
        }

        public void ModificarEstadoFechaNIIF(DateTime vFecha, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACartera.ModificarEstadoFechaNIIF(vFecha, vUsuario);
                    ts.Complete();
                }

                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraNIIFBusiness", "ModificarEstadoFechaNIIF", ex);
              
            }

        }



    }
}
