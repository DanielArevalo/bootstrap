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
    /// Listado de clientes generado para la generación de extracto
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class RegistroGeneraciónExtractoService
    {
        private RegistroGeneraciónExtractoBusiness BORegistroGeneraciónExtracto;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public RegistroGeneraciónExtractoService()
        {
            BORegistroGeneraciónExtracto = new RegistroGeneraciónExtractoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public RegistroGeneraciónExtracto AlmacenarRegistroGeneraciónExtractos(RegistroGeneraciónExtracto RegistroGeneraciónExtracto, Usuario pUsuario)
        {
            try
            {
                return BORegistroGeneraciónExtracto.AlmacenarRegistroGeneraciónExtractos(RegistroGeneraciónExtracto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RegistroGeneraciónExtractoService", "ListarRegistroGeneraciónExtractos", ex);
                return null;
            }
        }
    }
}