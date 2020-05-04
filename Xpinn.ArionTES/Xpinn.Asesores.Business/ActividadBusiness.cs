using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Business
{
    public class ActividadBusiness : GlobalBusiness
    {

        private ActividadData actividadData;

        public ActividadBusiness()
        {
            actividadData = new ActividadData();
        }

        public List<Xpinn.Asesores.Entities.Common.Actividad> ListarActividad(Xpinn.Asesores.Entities.Common.Actividad pActividad, Usuario pUsuario)
        {

            try
            {
                return actividadData.ListarActividades(pActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarAcodeudados", ex);
                return null;
            }
        }
    }
}
