using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xpinn.Sincronizacion.Data;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Business
{
    public class SyncUsuarioBusiness : GlobalBusiness
    {
        SyncUsuarioData DAUsuario;
        public SyncUsuarioBusiness()
        {
            DAUsuario = new SyncUsuarioData();
        }

        public List<SyncUsuario> ListarSyncUsuarios(string pFiltro, Usuario vUsuario)
        {
            try
            {
                List<SyncUsuario> lstUsuarios = new List<SyncUsuario>();
                lstUsuarios = DAUsuario.ListarSyncUsuarios(pFiltro, vUsuario);
                if (lstUsuarios != null)
                {
                    if (lstUsuarios.Count > 0)
                    {
                        foreach (SyncUsuario pUsu in lstUsuarios)
                        {
                            pUsu.lstIpAccesos = DAUsuario.ListarIPUsuario(pUsu, vUsuario);
                            pUsu.lstAtribuciones = DAUsuario.ListarAtribucionesUsuario(pUsu, vUsuario);
                        }
                    }
                }
                return lstUsuarios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncUsuarioBusiness", "ListarSyncUsuarios", ex);
                return null;
            }
        }

        public List<SyncPerfil> ListarSyncPerfilUsuario(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ListarSyncPerfilUsuario(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncUsuarioBusiness", "ListarSyncPerfilUsuario", ex);
                return null;
            }
        }

        public List<SyncAcceso> ListarSyncPerfilAcceso(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAUsuario.ListarSyncPerfilAcceso(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncUsuarioBusiness", "ListarSyncPerfilAcceso", ex);
                return null;
            }
        }

    }
}
