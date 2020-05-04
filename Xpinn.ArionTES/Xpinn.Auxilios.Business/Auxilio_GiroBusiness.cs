using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Auxilios.Data;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Entities;
using System.Web;
using System.IO;

namespace Xpinn.Auxilios.Business
{
    public class Auxilio_GiroBusiness : GlobalBusiness
    {

        private Auxilio_GiroData DAAuxilio;

        public Auxilio_GiroBusiness()
        {
            DAAuxilio = new Auxilio_GiroData();
        }
        

        public List<Auxilios_Giros> ListarAuxilio_giro(Auxilios_Giros pAuxilio_giro, Usuario vUsuario)
        {
            try
            {
                return DAAuxilio.ListarAuxilio_giro(pAuxilio_giro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Auxilio_GiroBusiness", "CrearAuxilio_giro", ex);
                return null;
            }
        }


    }
}
