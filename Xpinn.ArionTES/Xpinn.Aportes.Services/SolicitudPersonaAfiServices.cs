using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Business;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Aportes.Services
{

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SolicitudPersonaAfiServices
    {
        private SolicitudPersonaAfiBusiness BOActividad;
        private ExcepcionBusiness BOExcepcion;

        public SolicitudPersonaAfiServices()
        {
            BOActividad = new SolicitudPersonaAfiBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }



        public SolicitudPersonaAfi CrearSolicitudPersonaAfi(SolicitudPersonaAfi pSolicitudPersonaAfi, Usuario vUsuario,int pOpcion)
        {
            try
            {
                return BOActividad.CrearSolicitudPersonaAfi(pSolicitudPersonaAfi, vUsuario, pOpcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudPersonaAfiServices", "CrearSolicitudPersonaAfi", ex);
                return null;
            }
        }
        public SolicitudPersonaAfi ListarPersonasRepresentante(Int64 pident, Usuario vUsuario)
        {
            try
            {
                return BOActividad.ListarPersonasRepresentante(pident, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudPersonaAfiServices", "ListarPersonasRepresentante", ex);
                return null;
            }
        }

        public void guardarPersonaTema(List<Persona1> lstTemas, Usuario vUsuario)
        {
            try
            {
                BOActividad.guardarPersonaTema(lstTemas, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudPersonaAfiServices", "guardarPersonaTema", ex);
            }
        }

        public SolicitudPersonaAfi ConsultarPersona1(string filtro, Usuario usuario)
        {
            try
            {
                return BOActividad.ConsultarPersona1(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudPersonaAfiServices", "ConsultarPersona1", ex);
                return null;
            }
        }
    }
}
