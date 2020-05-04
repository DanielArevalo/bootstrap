using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Aprobador
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PersonaService
    {
        private PersonaBusiness BOPersona;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoProgramaCre { get { return "100153"; } }
        public string CodigoProgramaMod { get { return "100152"; } }

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public PersonaService()
        {
            BOPersona = new PersonaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<Persona> ListarPersonas(Persona persona, Usuario pUsuario)
        {
            try
            {
                return BOPersona.ListarPersonas(persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaService", "ListarPersonas", ex);
                return null;
            }
        }
    }
}
