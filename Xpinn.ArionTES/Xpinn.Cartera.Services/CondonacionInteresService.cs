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
    public class CondonacionInteresService
    {
        private CondonacionInteresBusiness BOCondonacionInteres;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Credito
        /// </summary>
        public CondonacionInteresService()
        {
            BOCondonacionInteres = new CondonacionInteresBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        // Còdigo de programa para Condonacion de intereses
        public string CodigoPrograma { get { return "60105"; } }

       
     
        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CondonacionInteres> ListarCredito(CondonacionInteres pCredito, Usuario pUsuario)
        {
            try
            {
                return BOCondonacionInteres.ListarCredito(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CondonacionInteresService", "ListarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Creditos a partir de unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public CondonacionInteres CrearCondonacion(CondonacionInteres pcondonacion, Usuario pUsuario)
        {
            try
            {
                return BOCondonacionInteres.CrearCondonacion(pcondonacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CondonacionInteresService", "CrearCondonacion", ex);
                return null;
            }
        }
    }
}