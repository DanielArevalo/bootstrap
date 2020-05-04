using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Comun.Business;
using Xpinn.Comun.Entities;
using Xpinn.Util;

namespace Xpinn.Comun.Services
{
    public class EliminarRegistroServices:GlobalService
    {
        EliminarRegistroBusiness EliminarRegistroBusiness = new EliminarRegistroBusiness();

        public EliminarRegistro EliminarRegistro(EliminarRegistro eliminarRegistro, Usuario vUsuario)
        {
            try
            {
                return EliminarRegistroBusiness.EliminarRegistro(eliminarRegistro, vUsuario);
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("EliminarRegistroServices", "EliminarRegistro", e);
                return null;
            }
        }
    }
}
