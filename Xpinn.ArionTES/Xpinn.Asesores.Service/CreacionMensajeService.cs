using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CreacionMensajeService:GlobalService
    {
        private CreacionMensajeBusiness BOCreacionMensaje;
        private ExcepcionBusiness BOExcepcion;

        public CreacionMensajeService()
        {
            BOCreacionMensaje = new CreacionMensajeBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110110"; } }
        public CreacionMensaje CrearMensaje(CreacionMensaje pCreacionMensaje, Usuario pUsuario)
        {
            try
            {
                return BOCreacionMensaje.CrearMensaje(pCreacionMensaje, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeService", "CrearMensaje", ex);
                return null;
            }
        }

        public CreacionMensaje CrearMensajePersona(CreacionMensaje pCreacionMensaje, Usuario pUsuario)
        {
            try
            {
                return BOCreacionMensaje.CrearMensajePersona(pCreacionMensaje, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeService", "CrearMensajePersona", ex);
                return null;
            }
        }

        public List<PesonasTemp> ConsultarPersonasTemp(string pId, Usuario pUsuario)
        {
            try
            {
                return BOCreacionMensaje.ConsultarPersonasTemp(pId,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeService", "ConsultarPersonasTemp", ex);
                return null;
            }
        }
        public List<CreacionMensaje> ListarMensajesApp(CreacionMensaje PcreacionMensaje,Usuario pUsuario)
        {
            try
            {
                return BOCreacionMensaje.ListarMensajesApp(PcreacionMensaje,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeService", "ListarMensajesApp", ex);
                return null;
            }
        }

        public void EliminarMensaje(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOCreacionMensaje.EliminarMensaje(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeService", "EliminarMensaje", ex);
            }
        }

        public CreacionMensaje ConsultarMensajes(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCreacionMensaje.ConsultarMensajes(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeService", "ConsultarMensajes", ex);
                return null;
            }
        }

        public CreacionMensaje ModificarMensajes(CreacionMensaje pCreacionMensaje, Usuario pUsuario)
        {
            try
            {
                return BOCreacionMensaje.ModificarMensajes(pCreacionMensaje, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeService", "ModificarMensajes", ex);
                return null;
            }
        }

        public void EliminarPersonaMensajeTemp(Usuario pUsuario)
        {
            try
            {
                BOCreacionMensaje.EliminarPersonaMensajeTemp(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeService", "EliminarPersonaMensajeTemp", ex);
            }
        }
    }
}
