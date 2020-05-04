using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Sincronizacion.Business;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SyncCajaServices
    {
        SyncCajaBusiness BOCaja;
        ExcepcionBusiness BOExcepcion;
        public SyncCajaServices()
        {
            BOCaja = new SyncCajaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public EntityGlobal CrearModSyncCaja(string[] pObjCaja, string[] lstTopes, string[] lstAtribuciones, int pOpcion, Usuario vUsuario)
        {
            try
            {
                return BOCaja.CrearModSyncCaja(pObjCaja, lstTopes, lstAtribuciones, pOpcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajaServices", "CrearModSyncCaja", ex);
                return null;
            }
        }

        public EntityGlobal AperturaCierreSyncCaja(string pObjCaja, string pObjSaldo, Usuario vUsuario)
        {
            try
            {
                return BOCaja.AperturaCierreSyncCaja(pObjCaja, pObjSaldo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajaServices", "AperturaCierreSyncCaja", ex);
                return null;
            }
        }

        public EntityGlobal EliminarSyncCaja(SyncCaja pCaja, Usuario vUsuario)
        {
            return BOCaja.EliminarSyncCaja(pCaja, vUsuario);
        }

        public List<SyncCaja> ListarSyncCaja(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOCaja.ListarSyncCaja(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajaServices", "ListarSyncCaja", ex);
                return null;
            }
        }

        public List<SyncTopesCaja> ListarSyncTopesCaja(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOCaja.ListarSyncTopesCaja(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajaServices", "ListarSyncTopesCaja", ex);
                return null;
            }
        }

        public List<SyncAtribucionCaja> ListarSyncAtribucionCaja(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOCaja.ListarSyncAtribucionCaja(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajaServices", "ListarSyncAtribucionCaja", ex);
                return null;
            }
        }

        public List<SyncTipoTran> ListarSyncTiposTran(int pCantidadRegistros, Usuario vUsuario)
        {
            try
            {
                return BOCaja.ListarSyncTiposTran(pCantidadRegistros, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncCajaServices", "ListarSyncTiposTran", ex);
                return null;
            }
        }


    }
}
