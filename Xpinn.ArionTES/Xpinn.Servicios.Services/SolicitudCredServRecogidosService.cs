using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Business;

namespace Xpinn.Servicios.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SolicitudCredServRecogidosService
    {

        private SolicitudCredServRecogidosBusiness BOSolicitudCredServRecogidos;
        private ExcepcionBusiness BOExcepcion;

        public SolicitudCredServRecogidosService()
        {
            BOSolicitudCredServRecogidos = new SolicitudCredServRecogidosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return ""; } }

        public SolicitudCredServRecogidos CrearSolicitudCredServRecogidos(SolicitudCredServRecogidos pSolicitudCredServRecogidos, Usuario pusuario)
        {
            try
            {
                pSolicitudCredServRecogidos = BOSolicitudCredServRecogidos.CrearSolicitudCredServRecogidos(pSolicitudCredServRecogidos, pusuario);
                return pSolicitudCredServRecogidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosService", "CrearSolicitudCredServRecogidos", ex);
                return null;
            }
        }


        public SolicitudCredServRecogidos ModificarSolicitudCredServRecogidos(SolicitudCredServRecogidos pSolicitudCredServRecogidos, Usuario pusuario)
        {
            try
            {
                pSolicitudCredServRecogidos = BOSolicitudCredServRecogidos.ModificarSolicitudCredServRecogidos(pSolicitudCredServRecogidos, pusuario);
                return pSolicitudCredServRecogidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosService", "ModificarSolicitudCredServRecogidos", ex);
                return null;
            }
        }


        public void EliminarSolicitudCredServRecogidos(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOSolicitudCredServRecogidos.EliminarSolicitudCredServRecogidos(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosService", "EliminarSolicitudCredServRecogidos", ex);
            }
        }


        public SolicitudCredServRecogidos ConsultarSolicitudCredServRecogidos(Int64 pId, Usuario pusuario)
        {
            try
            {
                SolicitudCredServRecogidos SolicitudCredServRecogidos = new SolicitudCredServRecogidos();
                SolicitudCredServRecogidos = BOSolicitudCredServRecogidos.ConsultarSolicitudCredServRecogidos(pId, pusuario);
                return SolicitudCredServRecogidos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosService", "ConsultarSolicitudCredServRecogidos", ex);
                return null;
            }
        }


        public List<SolicitudCredServRecogidos> ListarSolicitudCredServRecogidos(long numeroCredito, Usuario pusuario)
        {
            try
            {
                return BOSolicitudCredServRecogidos.ListarSolicitudCredServRecogidos(numeroCredito, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosService", "ListarSolicitudCredServRecogidos", ex);
                return null;
            }
        }

        public List<SolicitudCredServRecogidos> ListarSolicitudCredServRecogidosActualizado(long numeroCredito, Usuario usuario)
        {
            try
            {
                return BOSolicitudCredServRecogidos.ListarSolicitudCredServRecogidosActualizado(numeroCredito, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudCredServRecogidosService", "ListarSolicitudCredServRecogidosActualizado", ex);
                return null;
            }
        }
    }
}