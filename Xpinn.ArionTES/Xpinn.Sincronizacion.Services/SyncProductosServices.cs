using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Sincronizacion.Business;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SyncProductosServices
    {
        SyncProductosBusiness BOProductos;
        ExcepcionBusiness BOExcepcion;
        public SyncProductosServices()
        {
            BOProductos = new SyncProductosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<Producto> ListarProductosPersona(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOProductos.ListarProductosPersona(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "ListarProductosPersona", ex);
                return null;
            }
        }

        public List<ObjectString> ListarTiraProductosPersona(int codigo, string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOProductos.ListarTiraProductosPersona(codigo, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "ListarTiraProductosPersona", ex);
                return null;
            }
        }

        public EntityGlobal SyncCantidadProductos(int codigo, Usuario vUsuario)
        {
            try
            {
                return BOProductos.SyncCantidadProductos(codigo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "SyncCantidadProductos", ex);
                return null;
            }
        }

        public List<ObjectString> ListarTiraProductosPendientes(DateTime pFecGeneracion, string pTablaGen, Usuario vUsuario)
        {
            try
            {
                return BOProductos.ListarTiraProductosPendientes(pFecGeneracion, pTablaGen, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "ListarTiraProductosPendientes", ex);
                return null;
            }
        }

        public EntityGlobal CrearSyncOperacion(SyncOperacion pOperacion, Usuario vUsuario)
        {
            try
            {
                return BOProductos.CrearSyncOperacion(pOperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "CrearSyncOperacion", ex);
                return null;
            }
        }

        public EntityGlobal CrearSyncConsignacion(SyncOperacion pOperacion, Usuario vUsuario)
        {
            try
            {
                return BOProductos.CrearSyncConsignacion(pOperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "CrearSyncOperacion", ex);
                return null;
            }
        }

        public EntityGlobal CrearSyncCanjeCheque(SyncOperacion pOperacion, Usuario vUsuario)
        {
            try
            {
                return BOProductos.CrearSyncCanjeCheque(pOperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "CrearSyncCanjeCheque", ex);
                return null;
            }
        }

        public EntityGlobal CrearSyncTrasladoDinero(SyncOperacion pOperacion, Usuario vUsuario)
        {
            try
            {
                return BOProductos.CrearSyncTrasladoDinero(pOperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "CrearSyncTrasladoDinero", ex);
                return null;
            }
        }

        public EntityGlobal CrearSyncRecepcionDinero(SyncOperacion pOperacion, Usuario vUsuario)
        {
            try
            {
                return BOProductos.CrearSyncRecepcionDinero(pOperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "CrearSyncRecepcionDinero", ex);
                return null;
            }
        }


        public EntityGlobal CrearSyncReversionOperacion(SyncOperacion pOperacion, Usuario vUsuario)
        {
            try
            {
                return BOProductos.CrearSyncReversionOperacion(pOperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "CrearSyncReversionOperacion", ex);
                return null;
            }
        }


        public SyncHomologaOperacion ConsultarHomologacionOperacion(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOProductos.ConsultarHomologacionOperacion(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "ConsultarHomologacionOperacion", ex);
                return null;
            }
        }

    }
}
