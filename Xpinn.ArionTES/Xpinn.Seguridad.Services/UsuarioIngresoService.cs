using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Seguridad.Business;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class UsuarioIngresoService
    {
        private UsuarioIngresoBusiness BOProceso;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Proceso
        /// </summary>
        public UsuarioIngresoService()
        {
            BOProceso = new UsuarioIngresoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90202"; } } //90108

        /// <summary>
        /// Servicio para obtener lista de Procesos a partir de unos filtros
        /// </summary>
        /// <param name="pProceso">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Proceso obtenidos</returns>
        public List<Ingresos> ListarIngresos(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {
                return BOProceso.ListarIngresos(filtro,pFechaIni,pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioIngresoService", "ListarIngresos", ex);
                return null;
            }
        }
    }
}