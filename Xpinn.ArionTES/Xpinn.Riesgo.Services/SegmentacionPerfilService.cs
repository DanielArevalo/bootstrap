using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Entities;
using Xpinn.Util;


namespace Xpinn.Riesgo.Services
{

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]

 public  class SegmentacionPerfilService : GlobalBusiness
    {
        private SegmentacionPerfilBusiness BOSegmentacionPerfil;
        private ExcepcionBusiness BOExcepcion;

        public SegmentacionPerfilService()
        {
            BOSegmentacionPerfil = new SegmentacionPerfilBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270106"; } }


        public List<SegmentacionPerfil> ListarSegmentacionPerfil(SegmentacionPerfil vSegmentacionPerfil, Usuario pUsuario)
        {
            try
            {
                return BOSegmentacionPerfil.ListarPersonaRiesgo(vSegmentacionPerfil, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SegmentacionPerfilService", "ListarSegmentacionPerfil", ex);
                return null;
            }
        }

        public List<PerfilRiesgoSeg> ListarPerfilPesonaRiesgo(PerfilRiesgoSeg vPerfilRiesgo, Usuario pUsuario)
        {
            try
            {
                return BOSegmentacionPerfil.ListarPerfilPesonaRiesgo(vPerfilRiesgo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SegmentacionPerfilService", "ListarSegmentacionPerfil", ex);
                return null;
            }
        }

        public int ExistePerfilPesonaRiesgo(PerfilRiesgoSeg pPerfilRiesgoSeg, Usuario vUsuario)
        {
            try
            {
                return BOSegmentacionPerfil.ExistePerfilPesonaRiesgo(pPerfilRiesgoSeg, vUsuario);
            }
            catch
            {
                return 0;
            }
        }



    }
}
