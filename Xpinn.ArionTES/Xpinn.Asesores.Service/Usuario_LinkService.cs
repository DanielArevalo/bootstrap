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
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Usuario_LinkService
    {
        private Usuario_LinkBusiness BOEstadoCuenta;
        private ExcepcionBusiness BOExcepcion;

        public Usuario_LinkService()
        {
            BOEstadoCuenta = new Usuario_LinkBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<Usuario_Link> ListarUsuario_Link(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOEstadoCuenta.ListarUsuario_Link(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Usuario_LinkService", "ListarUsuario_Link", ex);
                return null;
            }
        }


        public Usuario_Link CrearUsuario_Link(Usuario_Link pLink, Usuario vUsuario)
        {
            try
            {
                return BOEstadoCuenta.CrearUsuario_Link(pLink, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Usuario_LinkService", "CrearUsuario_Link", ex);
                return null;
            }
        }

        public void EliminarLink(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOEstadoCuenta.EliminarLink(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Usuario_LinkService", "ListarUsuario_Link", ex);
            }
        }


    }


}
