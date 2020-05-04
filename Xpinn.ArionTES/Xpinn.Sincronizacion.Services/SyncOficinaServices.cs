using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Sincronizacion.Business;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SyncOficinaServices
    {
        SyncOficinaBusiness BOOficinas;
        ExcepcionBusiness BOExcepcion;
        public SyncOficinaServices()
        {
            BOOficinas = new SyncOficinaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public EntityGlobal GenerarAperturaCierre(string pObjOficina, Usuario vUsuario)
        {
            try
            {
                return BOOficinas.GenerarAperturaCierre(pObjOficina, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "GenerarAperturaCierre", ex);
                return null;
            }
        }


        public Oficina CrearModSyncOficina(Oficina pSync_Oficina, int pOpcion, Usuario vUsuario)
        {
            try
            {
                return BOOficinas.CrearModSyncOficina(pSync_Oficina, pOpcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "CrearModSyncOficina", ex);
                return null;
            }
        }

        public EntityGlobal EliminarSyncOficina(Oficina pOficina, Usuario vUsuario)
        {
            return BOOficinas.EliminarSyncOficina(pOficina, vUsuario);
        }

        public DateTime? ConsultarFecUltCierre(Int64 pCodOficina, Usuario pUsuario)
        {
            return BOOficinas.ConsultarFecUltCierre(pCodOficina, pUsuario);
        }

        public List<Oficina> ListarSyncOficina(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOOficinas.ListarSyncOficina(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "ListarSyncOficina", ex);
                return null;
            }
        }

        public List<SyncHorarioOficina> ListarSyncHorarioOficina(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOOficinas.ListarSyncHorarioOficina(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "ListarSyncHorarioOficina", ex);
                return null;
            }
        }

        public List<SyncProcesoOficina> ListarSyncProcesoOficina(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOOficinas.ListarSyncProcesoOficina(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "ListarSyncProcesoOficina", ex);
                return null;
            }
        }

        public SyncProcesoOficina ConsultarSyncProcesoOficina(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOOficinas.ConsultarSyncProcesoOficina(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncOficinaServices", "ConsultarSyncProcesoOficina", ex);
                return null;
            }
        }

    }
}
