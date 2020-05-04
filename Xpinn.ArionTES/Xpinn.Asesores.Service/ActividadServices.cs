using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

namespace Xpinn.Asesores.Services
{
    public class ActividadServices
    {
        private ActividadBusiness BOActividad;
        private ExcepcionBusiness BOExcepcion;

        public ActividadServices(){
            BOActividad = new ActividadBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "1101223"; } }


        public List<Xpinn.Asesores.Entities.Common.Actividad> ListarActividad(Xpinn.Asesores.Entities.Common.Actividad pActividad, Usuario pUsuario)
        {
            try{
                return BOActividad.ListarActividad(pActividad, pUsuario);
            }
            catch (Exception ex){
                BOExcepcion.Throw("ActividadServices", "ListarActividad", ex);
                return null;
            }
        }
    }
}
