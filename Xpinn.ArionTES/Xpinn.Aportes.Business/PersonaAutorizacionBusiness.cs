using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
 
namespace Xpinn.Aportes.Business
{
        public class PersonaAutorizacionBusiness : GlobalBusiness
        {
 
            private PersonaAutorizacionData DAPersonaAutorizacion;
 
            public PersonaAutorizacionBusiness()
            {
                DAPersonaAutorizacion = new PersonaAutorizacionData();
            }
 
            public PersonaAutorizacion CrearPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pPersonaAutorizacion = DAPersonaAutorizacion.CrearPersonaAutorizacion(pPersonaAutorizacion, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pPersonaAutorizacion;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("PersonaAutorizacionBusiness", "CrearPersonaAutorizacion", ex);
                    return null;
                }
            }
 
 
            public PersonaAutorizacion ModificarPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        pPersonaAutorizacion = DAPersonaAutorizacion.ModificarPersonaAutorizacion(pPersonaAutorizacion, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pPersonaAutorizacion;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("PersonaAutorizacionBusiness", "ModificarPersonaAutorizacion", ex);
                    return null;
                }
            }
 
 
            public void EliminarPersonaAutorizacion(Int64 pId, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DAPersonaAutorizacion.EliminarPersonaAutorizacion(pId, pusuario);
 
                        ts.Complete();
 
                    }
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("PersonaAutorizacionBusiness", "EliminarPersonaAutorizacion", ex);
                }
            }
 
 
            public PersonaAutorizacion ConsultarPersonaAutorizacion(Int64 pId, Usuario pusuario)
            {
                try
                {
                    PersonaAutorizacion PersonaAutorizacion = new PersonaAutorizacion();
                    PersonaAutorizacion = DAPersonaAutorizacion.ConsultarPersonaAutorizacion(pId, pusuario);
                    return PersonaAutorizacion;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("PersonaAutorizacionBusiness", "ConsultarPersonaAutorizacion", ex);
                    return null;
                }
            }


            public List<PersonaAutorizacion> ListarPersonaAutorizacion(PersonaAutorizacion pPersonaAutorizacion, Usuario pusuario, string filtro)
            {
                try
                {
                    return DAPersonaAutorizacion.ListarPersonaAutorizacion(pPersonaAutorizacion, pusuario, filtro);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("PersonaAutorizacionBusiness", "ListarPersonaAutorizacion", ex);
                    return null;
                }
            }
 
 
        }
    
}