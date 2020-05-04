using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EstadoResultadoNIIFService
    {
        private EstadoResultadoNIIFBusiness BOEstadoResultado;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Balance General Comparativo
        /// </summary>
        public EstadoResultadoNIIFService()
        {
            BOEstadoResultado = new EstadoResultadoNIIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30303"; } }
        public string CodigoProgramaNIIF { get { return "210310"; } }


        /// <summary>
        /// Servicio para obtener lista de  balance comparativo a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de balance comparativo obtenidos</returns>
        public List<EstadoResultadoNIIF> ListarEstadoResultado(EstadoResultadoNIIF pEntidad, Usuario vUsuario, int pOpcion)
        {
            try
            {
                return BOEstadoResultado.ListarEstadoResultado(pEntidad, vUsuario, pOpcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoResultadoNIIFService", "ListarEstadoResultado", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de  balance comparativo a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de balance comparativo obtenidos</returns>
        public List<EstadoResultadoNIIF> ListarFecha(Usuario pUsuario)
        {
            try
            {
                return BOEstadoResultado.ListarFecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstadoResultadoNIIFService", "ListarFecha", ex);
                return null;
            }
        }       

    }
}