using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Entities;
using System.Text;
using Xpinn.Util;

namespace Xpinn.Riesgo.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class RangoPerfilServices : GlobalBusiness
    {
        private RangoPerfilBusiness BORangoPerfil;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DefinirVariables
        /// </summary>
        public RangoPerfilServices()
        {
            BORangoPerfil = new RangoPerfilBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270205"; } }

        public List<RangoPerfil> ListarRangosPerfil(RangoPerfil pRangos, Usuario usuario)
        {
            try
            {
                return BORangoPerfil.ListarRangosPerfil(pRangos, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangoPerfilServices", "ListarRangosPerfil", ex);
                return null;
            }
        }
        public RangoPerfil ModificarRangoPerfil(RangoPerfil rangoPerfil, Usuario usuario)
        {
            try
            {
                return BORangoPerfil.ModificarRangoPerfil(rangoPerfil, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangoPerfilServices", "ModificarRangoPerfil", ex);
                return null;
            }
        }
    }
}
