using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Plan de Pagos
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DatosPlanPagosService
    {
        private DatosPlanPagosBusiness BODatosPlanPagos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public DatosPlanPagosService()
        {
            BODatosPlanPagos = new DatosPlanPagosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100142"; } }

        /// <summary>
        /// Obtiene la lista del plan de pagos del crédito
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Plan de Pagos del Crédito</returns>
        public List<DatosPlanPagos> ListarDatosPlanPagos(Credito pDatos, Usuario pUsuario)
        {
            try
            {
                return BODatosPlanPagos.ListarDatosPlanPagos(pDatos, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosPlanPagosService", "ListarDatosPlanPagos", ex);
                return null;
            }
        }

        public List<DatosPlanPagos> ListarDatosPlanPagosNue(Credito pDatos, Usuario pUsuario)
        {
            try
            {
                return BODatosPlanPagos.ListarDatosPlanPagosNue(pDatos, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosPlanPagosService", "ListarDatosPlanPagosNue", ex);
                return null;
            }
        }


        public List<Atributos> GenerarAtributosPlan(Usuario pUsuario)
        {
            try
            {
                return BODatosPlanPagos.GenerarAtributosPlan(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosPlanPagosService", "GenerarAtributosPlan", ex);
                return null;
            }
        }

        public List<DatosPlanPagos> ListarDatosPlanPagosOriginal(Credito datosApp, Usuario usuario)
        {
            try
            {
                return BODatosPlanPagos.ListarDatosPlanPagosOriginal(datosApp, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosPlanPagosService", "ListarDatosPlanPagosOriginal", ex);
                return null;
            }
        }
    }
}
