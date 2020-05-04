using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Business
{
    public class SegmentacionPerfilBusiness : GlobalBusiness
    {
        SegmentacionPerfilData DASegmentacionPerfil;

        public SegmentacionPerfilBusiness()
        {
            DASegmentacionPerfil = new SegmentacionPerfilData();
        }

        public List<SegmentacionPerfil> ListarPersonaRiesgo(SegmentacionPerfil pSegmentacionPerfil, Usuario vUsuario)
        {
            try
            {
                return DASegmentacionPerfil.ListarPersonaRiesgo(pSegmentacionPerfil, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SegmentacionPerfilBusiness", "ListarPersonaRiesgo", ex);
                return null;
            }
        }

        public List<PerfilRiesgoSeg> ListarPerfilPesonaRiesgo(PerfilRiesgoSeg pPerfilPersona, Usuario vUsuario)
        {
            try
            {
                return DASegmentacionPerfil.ListarPerfilPesonaRiesgo(pPerfilPersona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SegmentacionPerfilBusiness", "ListarPersonaRiesgo", ex);
                return null;
            }
        }

        public int ExistePerfilPesonaRiesgo(PerfilRiesgoSeg pPerfilRiesgoSeg, Usuario vUsuario)
        {
            try
            {
                return DASegmentacionPerfil.ExistePerfilPesonaRiesgo(pPerfilRiesgoSeg, vUsuario);
            }
            catch
            {
                return 0;
            }
        }


    }
}
