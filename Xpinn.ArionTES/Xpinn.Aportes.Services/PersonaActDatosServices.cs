using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;
using System.IO;
using Xpinn.Tesoreria.Entities;
using System;
using System.Collections.Generic;

namespace Xpinn.Aportes.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]

    public class PersonaActDatosServices
    {
        private PersonaActDatosBusiness BOAporte;
        private ExcepcionBusiness BOExcepcion;

        public PersonaActDatosServices()
        {


            BOAporte = new PersonaActDatosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public PersonaActDatos CrearPersonaActDatos(PersonaActDatos pPersonaActDatos, Usuario pUsuario)
        {
            try
            {
                return BOAporte.CrearPersonaActDatos(pPersonaActDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "CrearAporte", ex);
                return null;
            }
        }

        public SolicitudPersonaAfi ActualizarDatosPersona(SolicitudPersonaAfi pPersona, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ActualizarDatosPersona(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ActualizarDatosPersona", ex);
                return null;
            }
        }
    }
}
