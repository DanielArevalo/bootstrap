using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Obligaciones.Business;
using Xpinn.Obligaciones.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.Obligaciones.Services
{
    /// <summary>
    /// Servicio para Plan de Pagos
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ObPlanPagosService

    {

        private ObPlanPagosBusiness BOObPlanPagos;
        private ExcepcionBusiness BOExcepcion;
        public int Credito;
          /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public ObPlanPagosService()
        {
            BOObPlanPagos = new ObPlanPagosBusiness();
            BOExcepcion = new ExcepcionBusiness();

        }

        /// <summary>
        /// Obtiene la lista del plan de pagos del crédito
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Plan de Pagos del Crédito</returns>
        public List<ObPlanPagos> ListarObPlanPagos(ObPlanPagos pDatos, Usuario pUsuario)
        {
            try
            {
                return BOObPlanPagos.ListarObPlanPagos(pDatos, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosService", "ListarObPlanPagos", ex);
                return null;
            }
        }

        public List<ObPlanPagos> ConsultarObPlanPagos(ObPlanPagos pDatos, Usuario pUsuario)
        {
            try
            {
                return BOObPlanPagos.ConsultarObPlanPagos(pDatos, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosService", "ConsultarObPlanPagos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista del plan de pagos del crédito
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Plan de Pagos del Crédito</returns>
        public ObPlanPagos ConsultarObcomponente(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOObPlanPagos.ConsultarObcomponente(pId, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosService", "ConsultarObcomponente", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista del plan de pagos del crédito
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Plan de Pagos del Crédito</returns>
        public List<ObPlanPagos> ListarObPlanRegistroPagos(Int64 pId, DateTime pFechaProxPago, Usuario pUsuario)
        {
            try
            {
                return BOObPlanPagos.ListarObPlanRegistroPagos(pId, pFechaProxPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosService", "ListarObPlanRegistroPagos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ObligacionCredito
        /// </summary>
        /// <param name="pObligacionCredito">Entidad ObligacionCredito</param>
        /// <returns>Entidad ObligacionCredito modificada</returns>
        public ObPlanPagos ModificarPlanPagos(ObPlanPagos pDatos, Usuario pUsuario)
        {
            try
            {
                return BOObPlanPagos.ModificarPlanPagos(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosService", "ModificarPlanPagos", ex);
                return null;
            }
        }



        /// <summary>
        /// Elimina un Componente adicional 
        /// </summary>
        /// <param name="pEntity">Entidad  Componente adicional</param>
        /// <returns>Entidad eliminada</returns>
        public void EliminarComponenteadicional(Int64 pId, Int64 codcomponente,Usuario pUsuario)
        {
            try
            {
                BOObPlanPagos.EliminarComponenteadicional(pId,codcomponente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ObPlanPagosService", "EliminarComponenteadicional", ex);

            }
        }
    }
}
