using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Data;
using Xpinn.Util;

namespace Xpinn.Comun.Business
{
    public class EliminarRegistroBusiness : GlobalBusiness
    {
        EliminaRegistroData eliminarRegistroData = new EliminaRegistroData();
        public EliminarRegistro EliminarRegistro(EliminarRegistro eliminarRegistro, Usuario vUsuario)
        {
            try
            {
                return eliminarRegistroData.EliminarRegistro(eliminarRegistro, vUsuario);
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("EliminarRegistroBusiness", "EliminarRegistro", e);
                return null;
            }
        }

    }
}
