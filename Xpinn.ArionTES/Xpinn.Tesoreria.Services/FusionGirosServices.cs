using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class FusionGirosServices
    {
        private FusionGirosBusiness BOFusion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para AreasCaj
        /// </summary>
        public FusionGirosServices()
        {
            BOFusion = new FusionGirosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40301"; } }

        public List<Giro> ListarGiroAprobadosOpendientes(Giro pGiro, String Orden, DateTime pFechaGiro, Usuario vUsuario)
        {
            try
            {
                return BOFusion.ListarGiroAprobadosOpendientes(pGiro, Orden, pFechaGiro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RealizacionGirosServices", "ListarGiroAprobadosOpendientes", ex);
                return null;
            }
        }


        public Giro  FusionarGiro(Giro pGiroTot, Giro pGiro, Usuario vUsuario)
        {
            try
            {
                return BOFusion.FusionarGiro(pGiroTot, pGiro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RealizacionGirosServices", "FusionarGiro", ex);
                return null;
            }
        }

    }
}