using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicio para TiposDeFiltro en generación de extracto
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TiposDeFiltroService
    {
        private TiposDeFiltroBusiness BOTiposDeFiltro;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public TiposDeFiltroService()
        {
            BOTiposDeFiltro = new TiposDeFiltroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<TiposDeFiltro> ListarTiposDeFiltros(TiposDeFiltro TiposDeFiltro, Usuario pUsuario)
        {
            try
            {
                return BOTiposDeFiltro.ListarTiposDeFiltros(TiposDeFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TiposDeFiltroService", "ListarTiposDeFiltros", ex);
                return null;
            }
        }
    }
}
