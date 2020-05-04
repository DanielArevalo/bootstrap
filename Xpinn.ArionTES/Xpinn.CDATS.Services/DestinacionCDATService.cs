using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Business;

namespace Xpinn.CDATS.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DestinacionService
    {

        private DestinacionBusiness BODestinacion;
        private ExcepcionBusiness BOExcepcion;

        public DestinacionService()
        {
            BODestinacion = new DestinacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220105"; } }

        public Destinacion CrearDestinacion(Destinacion pDestinacion, Usuario pusuario)
        {
            try
            {
                pDestinacion = BODestinacion.CrearDestinacion(pDestinacion, pusuario);
                return pDestinacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionService", "CrearDestinacion", ex);
                return null;
            }
        }


        public Destinacion ModificarDestinacion(Destinacion pDestinacion, Usuario pusuario)
        {
            try
            {
                pDestinacion = BODestinacion.ModificarDestinacion(pDestinacion, pusuario);
                return pDestinacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionService", "ModificarDestinacion", ex);
                return null;
            }
        }


        public void EliminarDestinacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                BODestinacion.EliminarDestinacion(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionService", "EliminarDestinacion", ex);
            }
        }


        public Destinacion ConsultarDestinacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Destinacion Destinacion = new Destinacion();
                Destinacion = BODestinacion.ConsultarDestinacion(pId, pusuario);
                return Destinacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionService", "ConsultarDestinacion", ex);
                return null;
            }
        }


        public List<Destinacion> ListarDestinacion(Destinacion pDestinacion, Usuario pusuario)
        {
            try
            {
                return BODestinacion.ListarDestinacion(pDestinacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionService", "ListarDestinacion", ex);
                return null;
            }
        }


    }
}
