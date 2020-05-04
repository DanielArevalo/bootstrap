using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Xpinn.Sincronizacion.Business;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SyncUsuarioServices
    {
        SyncUsuarioBusiness BOUsuario;
        ExcepcionBusiness BOExcepcion;
        public SyncUsuarioServices()
        {
            BOExcepcion = new ExcepcionBusiness();
            BOUsuario = new SyncUsuarioBusiness();
        }

        public List<SyncUsuario> ListarSyncUsuarios(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOUsuario.ListarSyncUsuarios(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncUsuarioServices", "ListarSyncUsuarios", ex);
                return null;
            }
        }

        public List<SyncPerfil> ListarSyncPerfilUsuario(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ListarSyncPerfilUsuario(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncUsuarioServices", "ListarSyncPerfilUsuario", ex);
                return null;
            }
        }

        public List<SyncAcceso> ListarSyncPerfilAcceso(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOUsuario.ListarSyncPerfilAcceso(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncUsuarioServices", "ListarSyncPerfilAcceso", ex);
                return null;
            }
        }


    }
}
