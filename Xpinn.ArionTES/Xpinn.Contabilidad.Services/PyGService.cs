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
    public class PyGService
    {
        private PyGBusiness BOPyG;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public PyGService()
        {
            BOPyG = new PyGBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30302"; } }
        public string CodigoProgramaNIIF { get { return "210302"; } }
        /// <summary>
        /// Método para consultar los datos del PyG
        /// </summary>
        /// <param name="pDatos"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<PyG> ListarPyG(PyG pEntidad, Usuario vUsuario, int pOpcion)
        {
            return BOPyG.ListarPyG(pEntidad, vUsuario, pOpcion);
        }


        /// <summary>
        /// Servicio para obtener lista de FechaCorte a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de FechaCorte obtenidos</returns>
        public List<PyG> ListarFechaCorte(Usuario pUsuario)
        {
            try
            {
                return BOPyG.ListarFechaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PyGServices", "ListarFechaCorte", ex);
                return null;
            }
        }

    }
}