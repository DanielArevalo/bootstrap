using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ImpresionTarjetaService
    {
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Persona1
        /// </summary>
        public ImpresionTarjetaService()
        {
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110123"; } }

    }
}