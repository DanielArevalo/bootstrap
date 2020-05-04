using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Sincronizacion.Business;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SyncPersonaServices
    {
        SyncPersonaBusiness BOPersona;
        ExcepcionBusiness BOExcepcion;
        public SyncPersonaServices()
        {
            BOPersona = new SyncPersonaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<Persona> ListarSyncPersona(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOPersona.ListarSyncPersona(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "ListarSyncPersona", ex);
                return null;
            }
        }

        public int CantidadRegistrosPersona(Usuario vUsuario)
        {
            try
            {
                return BOPersona.CantidadRegistrosPersona(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "CantidadRegistrosPersona", ex);
                return 0;
            }
        }


    }
}
