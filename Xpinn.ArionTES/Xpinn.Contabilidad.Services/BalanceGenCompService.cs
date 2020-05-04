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
    public class BalanceGenCompService
    {
        private BalanceGenCompBusiness BOBalanceGenComp;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Balance General Comparativo
        /// </summary>
        public BalanceGenCompService()
        {
            BOBalanceGenComp = new BalanceGenCompBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30303"; } }
        public string CodigoProgramaNIIF { get { return "210303"; } }


        /// <summary>
        /// Servicio para obtener lista de  balance comparativo a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de balance comparativo obtenidos</returns>
        public List<BalanceGenComp> ListarBalanceComparativo(BalanceGenComp pEntidad, Usuario vUsuario, int pOpcion)
        {
            try
            {
                return BOBalanceGenComp.ListarBalanceComparativo(pEntidad, vUsuario, pOpcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceGenCompServices", "ListarBalanceComparativo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de  balance comparativo a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de balance comparativo obtenidos</returns>
        public List<BalanceGenComp> ListarFecha(Usuario pUsuario)
        {
            try
            {
                return BOBalanceGenComp.ListarFecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceGenCompServices", "ListarFecha", ex);
                return null;
            }
        }       

    }
}