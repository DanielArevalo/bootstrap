using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Auxilios.Entities;
using Xpinn.Cartera.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Credito_GiroService
    {
        private Credito_GiroBusiness BOCredito;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Credito
        /// </summary>
        public Credito_GiroService()
        {
            BOCredito = new Credito_GiroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<Credito_Giro> ConsultarCredito_Giro(Credito_Giro pCredito_Giro, Usuario vUsuario)
        {
            try
            {
                return BOCredito.ConsultarCredito_Giro(pCredito_Giro, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Credito_GiroService", "ConsultarCredito_Giro", ex);
                return null;
            }
        }

        public bool CrearGiros(List<Credito_Giro> lstGiros, Usuario vUsuario)
        {
            try
            {
                return BOCredito.CrearGiros(lstGiros, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Credito_GiroService", "CrearGiros", ex);
                return false;
            }
        }

        public List<Credito_Giro> ListarGiros(string radicado, Usuario vUsuario)
        {
            try
            {
                return BOCredito.ListarGiros(radicado, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Credito_GiroService", "ListarGiros", ex);
                return null;
            }
        }
    }
}