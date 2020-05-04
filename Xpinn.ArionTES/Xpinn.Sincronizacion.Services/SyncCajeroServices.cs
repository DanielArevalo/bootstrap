using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Sincronizacion.Business;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SyncCajeroServices
    {
        SyncCajeroBusiness BOCajero;
        ExcepcionBusiness BOExcepcion;
        public SyncCajeroServices()
        {
            BOCajero = new SyncCajeroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public EntityGlobal CrearModSyncCajero(string[] pObjCajero, int pOpcion, Usuario vUsuario)
        {
            try
            {
                return BOCajero.CrearModSyncCajero(pObjCajero, pOpcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajeroServices", "CrearModSyncCajero", ex);
                return null;
            }
        }

        public EntityGlobal EliminarSyncCajero(SyncCajero pCajero, Usuario vUsuario)
        {
            return BOCajero.EliminarSyncCajero(pCajero, vUsuario);
        }

        public EntityGlobal AsignarSyncCajero(string pObjCajero, Usuario vUsuario)
        {
            try
            {
                return BOCajero.AsignarSyncCajero(pObjCajero, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajeroServices", "CrearModSyncCajero", ex);
                return null;
            }
        }

        public List<SyncCajero> ListarSyncCajero(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOCajero.ListarSyncCajero(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajeroServices", "ListarSyncCajero", ex);
                return null;
            }
        }


    }
}
