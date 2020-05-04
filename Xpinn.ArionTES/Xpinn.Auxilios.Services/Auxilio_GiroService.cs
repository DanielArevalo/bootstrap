using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Business;

namespace Xpinn.Auxilios.Services
{
    
    public class Auxilio_GiroService
    {

        private Auxilio_GiroBusiness BOAuxilio;
        private ExcepcionBusiness BOExcepcion;

        public Auxilio_GiroService()
        {
            BOAuxilio = new Auxilio_GiroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
               

        public List<Auxilios_Giros> ListarAuxilio_giro(Auxilios_Giros pAuxilio_giro, Usuario vUsuario)
        {
            try
            {
                return BOAuxilio.ListarAuxilio_giro(pAuxilio_giro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Auxilio_GiroService", "ListarAuxilio_giro", ex);
                return null;
            }
        }


    }
}