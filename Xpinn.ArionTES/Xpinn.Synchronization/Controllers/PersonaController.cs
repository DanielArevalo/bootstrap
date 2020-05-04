using System;
using System.Collections.Generic;
using System.Web.Http;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Sincronizacion.Services;

namespace Xpinn.Synchronization.Controllers
{
    public class PersonaController : GlobalApiController
    {
        SyncPersonaServices ServicesPersona;
        public PersonaController()
        {
            ServicesPersona = new SyncPersonaServices();
        }
        
        public IHttpActionResult ListarPersonas(EntityGlobal pEntity)
        {
            if (pEntity == null)
                return BadRequest("El objeto pEntity no puede ser nulo");
            string pFilter = !string.IsNullOrEmpty(pEntity.Filter) ? pEntity.Filter : "";
            try
            {
                List<Persona> lstPersonas = ServicesPersona.ListarSyncPersona(pFilter, Usuario);
                return Ok(lstPersonas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public IHttpActionResult CantidadRegistrosPersona()
        {
            try
            {
                EntityGlobal pEntity = new EntityGlobal();
                pEntity.NroRegisterAffected = ServicesPersona.CantidadRegistrosPersona(Usuario);
                return Ok(pEntity);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
