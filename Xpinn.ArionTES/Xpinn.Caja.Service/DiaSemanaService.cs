using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para DiaSemana
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DiaSemanaService
    {
        private DiaSemanaBusiness BODiaSemana;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DiaSemana
        /// </summary>
        public DiaSemanaService()
        {
            BODiaSemana = new DiaSemanaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoDiaSemana;

         /// <summary>
        /// Obtiene la lista de Dias de Semana dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Dias de Semana obtenidos</returns>
        public List<DiaSemana> ListarDiaSemana(DiaSemana pDiaSemana, Usuario pUsuario)
        {
            try
            {
                return BODiaSemana.ListarDiaSemana(pDiaSemana, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("diaSemanaService", "ListarDiaSemana", ex);
                return null;
            }
        }
    }
}
