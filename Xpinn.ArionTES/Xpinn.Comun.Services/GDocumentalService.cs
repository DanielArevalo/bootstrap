using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Business;
using System.Web;

namespace Xpinn.Comun.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class GDocumentalService
    {
        private GDocumentalBusiness BOGDocumental;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public GDocumentalService()
        {
            BOGDocumental = new GDocumentalBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "23000"; } }

        public List<GestionDocumental > ListarGDocumental(GestionDocumental pTipo, Usuario pUsuario)
        {
            try
            {
                return BOGDocumental.ListarGDocumental (pTipo, pUsuario);
            }
            catch
            {
                return null;
            }
        }

       
    }
}
