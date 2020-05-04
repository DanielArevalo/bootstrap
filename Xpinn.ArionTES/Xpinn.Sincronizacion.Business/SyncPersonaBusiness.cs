using System;
using System.Collections.Generic;
using Xpinn.Sincronizacion.Data;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Business
{
    public class SyncPersonaBusiness : GlobalBusiness
    {
        SyncPersonaData DAPersona;
        public SyncPersonaBusiness()
        {
            DAPersona = new SyncPersonaData();
        }

        public List<Persona> ListarSyncPersona(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAPersona.ListarSyncPersona(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncPersonaBusiness", "ListarSyncPersona", ex);
                return null;
            }
        }

        public int CantidadRegistrosPersona(Usuario vUsuario)
        {
            try
            {
                return DAPersona.CantidadRegistrosPersona(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncPersonaBusiness", "CantidadRegistrosPersona", ex);
                return 0;
            }
        }

    }
}
