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
    public class LibroDiarioColumnarioService
    {
        private LibroDiarioColumnarioBusiness BOLibroDiarioColumnario;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public LibroDiarioColumnarioService()
        {
            BOLibroDiarioColumnario = new LibroDiarioColumnarioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        public string CodigoProgramaNIff { get { return "210119"; } }
        public string CodigoPrograma { get { return "30203"; } }

        public List<LibroDiarioColumnario> ListarLibroDiarioNiff(LibroDiarioColumnario pDatos, Usuario pUsuario)
        {
            return BOLibroDiarioColumnario.ListarLibroDiarioNiff(pDatos, pUsuario);
        }
        public List<LibroDiarioColumnario> ListarLibroDiario(LibroDiarioColumnario pDatos, Usuario pUsuario)
        {
            return BOLibroDiarioColumnario.ListarLibroDiario(pDatos, pUsuario); 
        }


        /// <summary>
        /// Servicio para obtener lista de FechaCorte a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de FechaCorte obtenidos</returns>
        public List<LibroDiarioColumnario> ListarFechaCorte(Usuario pUsuario)
        {
            try
            {
                return BOLibroDiarioColumnario.ListarFechaCorte(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LibroDiarioColumnarioServices", "ListarFechaCorte", ex);
                return null;
            }
        }

    }
}