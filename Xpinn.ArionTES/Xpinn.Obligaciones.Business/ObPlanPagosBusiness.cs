using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Obligaciones.Data;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Business
{
    public class ObPlanPagosBusiness : GlobalData
    {
        private ObPlanPagosData DAObPlanPagos;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public ObPlanPagosBusiness()
        {
            DAObPlanPagos = new ObPlanPagosData();
        }

          /// <summary>
        /// Obtiene la lista del plan de pagos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de plan de pagos obtenidos</returns>
        public List<ObPlanPagos> ListarObPlanPagos(ObPlanPagos pDatos, Usuario pUsuario)
        {
            try
            {
                return DAObPlanPagos.ListarObPlanPagos(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosBusiness", "ListarObPlanPagos", ex);
                return null;
            }
        }


        public List<ObPlanPagos> ConsultarObPlanPagos(ObPlanPagos pDatos, Usuario pUsuario)
        {
            try
            {
                return DAObPlanPagos.ConsultarObPlanPagos(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosBusiness", "ConsultarObPlanPagos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista del plan de pagos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de plan de pagos obtenidos</returns>
        public ObPlanPagos ConsultarObcomponente(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAObPlanPagos.ConsultarObcomponente(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosBusiness", "ConsultarObcomponente", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista del plan de pagos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de plan de pagos obtenidos - Registro de Pago</returns>
        public List<ObPlanPagos> ListarObPlanRegistroPagos(Int64 pId, DateTime pFechaProxPago, Usuario pUsuario)
        {
            try
            {
                return DAObPlanPagos.ListarObPlanRegistroPagos(pId, pFechaProxPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosBusiness", "ListarObPlanRegistroPagos", ex);
                return null;
            }
        }



        /// <summary>
        /// Modifica un ObligacionCredito
        /// </summary>
        /// <param name="pObligacionCredito">Entidad ObligacionCredito</param>
        /// <returns>Entidad ObligacionCredito modificada</returns>
        public ObPlanPagos ModificarPlanPagos(ObPlanPagos pDatos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDatos = DAObPlanPagos.ModificarPlanPagos(pDatos, pUsuario);

                    ts.Complete();
                }

                return pDatos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosBusiness", "ModificarPlanPagos", ex);
                return null;
            }
        }



        /// <summary>
        /// Elimina un Componente adicional
        /// </summary>
        /// <param name="pId">identificador de Componenteadicional</param>
        public void EliminarComponenteadicional(Int64 pId,Int64 codcomponente, Usuario pUsuario)
        {
            try
            {
                // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{

                DAObPlanPagos.EliminarComponenteadicional(pId,codcomponente, pUsuario);

                //   ts.Complete();
                // }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosBusiness", "EliminarComponenteadicional", ex);
            }
        }

    }
}
