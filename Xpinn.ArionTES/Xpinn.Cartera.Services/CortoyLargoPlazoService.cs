using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;
using System.Web;
using Xpinn.Util;
using Xpinn.Comun.Entities;

namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CortoyLargoPlazoService
    {
        private CortoyLargoPlazoBusiness BOCortoyLargoPlazo;
        private CortoyLargoPlazoBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public CortoyLargoPlazoService()
        {
            BOCortoyLargoPlazo = new CortoyLargoPlazoBusiness();
            BOExcepcion = new CortoyLargoPlazoBusiness();
        }

        public string CodigoPrograma { get { return "60202"; } }

        public List<Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            try
            {
                return BOCortoyLargoPlazo.ListarFechaCierre(pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<CortoyLargoPlazo> ListarCredito(DateTime pFecha, Usuario pUsuario, String sfiltro)
        {
            try
            {
                return BOCortoyLargoPlazo.ListarCredito(pFecha, pUsuario, sfiltro);
            }
            catch
            {
                return null;
            }
        }

    }

}
