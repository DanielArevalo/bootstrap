using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Cartera.Business;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CondonacionTotalService
    {
        private CondonacionTotalBusiness BOCondonacionInteres;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Credito
        /// </summary>
        public CondonacionTotalService()
        {
            BOCondonacionInteres = new CondonacionTotalBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        // Còdigo de programa para Condonacion total de la deuda
        public string CodigoPrograma { get { return "60107"; } }

       
     
        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CondonacionTotal> ListarCredito(CondonacionTotal pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCondonacionInteres.ListarCredito(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CondonacionTotalService", "ListarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public CondonacionTotal CrearCondonacion(CondonacionTotal pcondonacion, Usuario pUsuario)
        {
            try
            {
                return BOCondonacionInteres.CrearCondonacion(pcondonacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CondonacionTotalService", "CrearCondonacion", ex);
                return null;
            }
        }
    }
}