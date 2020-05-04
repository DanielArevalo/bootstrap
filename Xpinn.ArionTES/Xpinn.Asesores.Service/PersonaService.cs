using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PersonaService
    {

        private PersonaBusiness busPersona;
        private ExcepcionBusiness excepBusinnes;

        public PersonaService()
        {
            busPersona = new PersonaBusiness();
            excepBusinnes = new ExcepcionBusiness();

        }

        public string CodigoPrograma { get { return "AseServicePersona010"; } }

        public Persona ConsultarPersona(Int64 pIdEntityPersona, Usuario pUsuario)
        {
            try
            {
                return busPersona.Consultar(pIdEntityPersona, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("PersonaService", "ConsultarPersona", ex);
                return null;
            }
        }

        public List<Persona> ListarPersona(Persona pEntityPersona, Usuario pUsuario)
        {
            try
            {
                return busPersona.Listar(pEntityPersona, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("PersonaService", "ListarPersona", ex);
                return null;
            }
        }
        public Persona ConsultaryaExistente(Int64 pIdEntityPersona, Usuario pUsuario)
        {
            try
            {
                return busPersona.ConsultaryaExistente(pIdEntityPersona, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("PersonaService", "ConsultarPersona", ex);
                return null;
            }
        }
    }
}