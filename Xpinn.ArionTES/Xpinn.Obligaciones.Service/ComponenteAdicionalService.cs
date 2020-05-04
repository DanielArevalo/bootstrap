using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Obligaciones.Business;
using Xpinn.Obligaciones.Entities;

namespace Xpinn.Obligaciones.Services
{
    /// <summary>
    /// Servicios para Tipo Liquidacion
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ComponenteAdicionalService
    {
         private ComponenteAdicionalBusiness BOComponenteAdicional;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
        public ComponenteAdicionalService()
        {
            BOComponenteAdicional = new ComponenteAdicionalBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        
        /// <summary>
        /// Servicio para obtener lista de Tipo de Liquidacion a partir de unos filtros
        /// </summary>
        /// <param name="pSolicitud">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ComponenteAdicional obtenidos</returns>
        public List<ComponenteAdicional> ListarComponenteAdicional(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOComponenteAdicional.ListarComponenteAdicional(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ComponenteAdicionalService", "ListarComponenteAdicional", ex);
                return null;
            }
        }
    }
}
