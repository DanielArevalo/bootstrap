using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    public class Usuario_LinkBusiness : GlobalBusiness
    {
        private Usuario_LinkData BOEstadoCuentaData;

        public Usuario_LinkBusiness()
        {
            BOEstadoCuentaData = new Usuario_LinkData();
        }

        public List<Usuario_Link> ListarUsuario_Link(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOEstadoCuentaData.ListarUsuario_Link(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Usuario_LinkBusiness", "ListarUsuario_Link", ex);
                return null;
            }
        }


        public Usuario_Link CrearUsuario_Link(Usuario_Link pLink, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLink = BOEstadoCuentaData.CrearUsuario_Link(pLink, vUsuario);
                    ts.Complete();
                }
                return pLink;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Usuario_LinkBusiness", "CrearUsuario_Link", ex);
                return null;
            }
        }


        public void EliminarLink(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOEstadoCuentaData.EliminarLink(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Usuario_LinkBusiness", "ListarUsuario_Link", ex);
            }
        }

    }
}
