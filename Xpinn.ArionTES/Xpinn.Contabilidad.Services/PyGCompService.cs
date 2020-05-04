using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PyGCompService
    {
        private PyGCompBusiness BOPyGComp;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PyG Comparativo
        /// </summary>
        public PyGCompService()
        {
            BOPyGComp = new PyGCompBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30304"; } }
        public string CodigoProgramaNIIF { get { return "210304"; } }


        /// <summary>
        /// Servicio para obtener lista de  PyG comparativo a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PyG comparativo obtenidos</returns>
        public List<PyGComp> ListarPyGComparativo(PyGComp pEntidad, Usuario vUsuario, int pOpcion)
        {
            try
            {
                return BOPyGComp.ListarPyGComparativo(pEntidad, vUsuario, pOpcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PyGCompService", "ListarPyGComparativo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de  PyG comparativo a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PyG comparativo obtenidos</returns>
        public List<PyGComp> ListarFecha(Usuario pUsuario)
        {
            try
            {
                return BOPyGComp.ListarFecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PyGCompService", "ListarFecha", ex);
                return null;
            }
        }

    }
}