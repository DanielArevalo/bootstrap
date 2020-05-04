using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Business;

namespace Xpinn.Aportes.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PersonaAutorizacionService
    {

        private PersonaAutorizacionBusiness BOPersonaAutorizacion;
        private ExcepcionBusiness BOExcepcion;

        public PersonaAutorizacionService()
        {
            BOPersonaAutorizacion = new PersonaAutorizacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170112"; } }

        public PersonaAutorizacion CrearPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario pusuario)
        {
            try
            {
                pPersonaAutorizacion = BOPersonaAutorizacion.CrearPersonaAutorizacion(pPersonaAutorizacion, pusuario);
                return pPersonaAutorizacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaAutorizacionService", "CrearPersonaAutorizacion", ex);
                return null;
            }
        }


        public PersonaAutorizacion ModificarPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario pusuario)
        {
            try
            {
                pPersonaAutorizacion = BOPersonaAutorizacion.ModificarPersonaAutorizacion(pPersonaAutorizacion, pusuario);
                return pPersonaAutorizacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaAutorizacionService", "ModificarPersonaAutorizacion", ex);
                return null;
            }
        }


        public void EliminarPersonaAutorizacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOPersonaAutorizacion.EliminarPersonaAutorizacion(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaAutorizacionService", "EliminarPersonaAutorizacion", ex);
            }
        }


        public PersonaAutorizacion ConsultarPersonaAutorizacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                PersonaAutorizacion PersonaAutorizacion = new PersonaAutorizacion();
                PersonaAutorizacion = BOPersonaAutorizacion.ConsultarPersonaAutorizacion(pId, pusuario);
                return PersonaAutorizacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaAutorizacionService", "ConsultarPersonaAutorizacion", ex);
                return null;
            }
        }


        public List<PersonaAutorizacion> ListarPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario pusuario, string filtro)
        {
            try
            {
                return BOPersonaAutorizacion.ListarPersonaAutorizacion(pPersonaAutorizacion, pusuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaAutorizacionService", "ListarPersonaAutorizacion", ex);
                return null;
            }
        }


    }
}