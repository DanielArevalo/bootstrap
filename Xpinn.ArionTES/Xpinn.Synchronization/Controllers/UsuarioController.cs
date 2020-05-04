using System;
using System.Collections.Generic;
using System.Web.Http;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Sincronizacion.Services;

namespace Xpinn.Synchronization.Controllers
{
    public class UsuarioController : GlobalApiController
    {
        SyncUsuarioServices ServicesUsuario;
        public UsuarioController()
        {
            ServicesUsuario = new SyncUsuarioServices();
        }

        public IHttpActionResult ListarUsuarios(EntityGlobal pEntity)
        {
            if (pEntity == null)
                return BadRequest("El objeto pEntity no puede ser nulo");
            string pFilter = !string.IsNullOrEmpty(pEntity.Filter) ? pEntity.Filter : "";
            try
            {
                List<SyncUsuario> lstUsuarios = ServicesUsuario.ListarSyncUsuarios(pFilter, Usuario);
                return Ok(lstUsuarios);
            }
            catch (Exception ex)    
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult ListarPerfilUsuarios(EntityGlobal pEntity)
        {
            if (pEntity == null)
                return BadRequest("El objeto pEntity no puede ser nulo");
            string pFilter = !string.IsNullOrEmpty(pEntity.Filter) ? pEntity.Filter : "";
            try
            {
                List<SyncPerfil> lstPerfiles = ServicesUsuario.ListarSyncPerfilUsuario(pFilter, Usuario);
                return Ok(lstPerfiles);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        public IHttpActionResult ListarSyncPerfilAcceso(EntityGlobal pEntity)
        {
            if (pEntity == null)
                return BadRequest("El objeto pEntity no puede ser nulo");
            string pFilter = !string.IsNullOrEmpty(pEntity.Filter) ? pEntity.Filter : "";
            try
            {
                List<SyncAcceso> lstAccesos = ServicesUsuario.ListarSyncPerfilAcceso(pFilter, Usuario);
                return Ok(lstAccesos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}
