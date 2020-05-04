using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    public class CreacionMensajeBusiness : GlobalBusiness
    {
        private CreacionMensajeData DACreacionMensaje;

        public CreacionMensajeBusiness()
        {
            DACreacionMensaje = new CreacionMensajeData();
        }

        public CreacionMensaje CrearMensaje(CreacionMensaje pCreacionMensaje, Usuario pUsuario)
        {
            try
            {
                return DACreacionMensaje.CrearMensaje(pCreacionMensaje, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeBusiness", "CrearMensaje", ex);
                return null;
            }
        }

        public CreacionMensaje CrearMensajePersona(CreacionMensaje pCreacionMensaje, Usuario pUsuario)
        {
            try
            {
                return DACreacionMensaje.CrearMensajePersona(pCreacionMensaje, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeBusiness", "CrearMensajePersona", ex);
                return null;
            }
        }
        public List<PesonasTemp> ConsultarPersonasTemp(string pId, Usuario pUsuario)
        {
            try
            {
                return DACreacionMensaje.ConsultarPersonasTemp(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeBusiness", "ConsultarPersonasTemp", ex);
                return null;
            }
        }
        public List<CreacionMensaje> ListarMensajesApp(CreacionMensaje PcreacionMensaje,Usuario pUsuario)
        {
            try
            {
                return DACreacionMensaje.ListarMensajesApp(PcreacionMensaje,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeBusiness", "ListarMensajesApp", ex);
                return null;
            }
        }

        public void EliminarMensaje(Int64 pId, Usuario pUsuario)
        {
            try
            {
                DACreacionMensaje.EliminarMensaje(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeBusiness", "EliminarMensaje", ex);
            }
        }


        public CreacionMensaje ConsultarMensajes(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACreacionMensaje.ConsultarMensajes(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeBusiness", "ConsultarMensajes", ex);
                return null;
            }
        }

        public CreacionMensaje ModificarMensajes(CreacionMensaje pCreacionMensaje, Usuario pUsuario)
        {
            try
            {
                return DACreacionMensaje.ModificarMensajes(pCreacionMensaje, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeBusiness", "ModificarMensajes", ex);
                return null;
            }
       }

        public void EliminarPersonaMensajeTemp(Usuario pUsuario)
        {
            try
            {
                DACreacionMensaje.EliminarPersonaMensajeTemp(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreacionMensajeBusiness", "EliminarPersonaMensajeTemp", ex);
            }
        }
    }
}
