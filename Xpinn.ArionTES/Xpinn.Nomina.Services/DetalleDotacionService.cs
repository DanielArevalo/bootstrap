using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Business;

namespace Xpinn.Nomina.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Detalle_DotacionService
    {

        private Detalle_DotacionBusiness BODetalle_Dotacion;
        private ExcepcionBusiness BOExcepcion;

        public Detalle_DotacionService()
        {
            BODetalle_Dotacion = new Detalle_DotacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250204"; } }

        public Detalle_Dotacion CrearDetalle_Dotacion(Detalle_Dotacion pDetalle_Dotacion, Usuario pusuario)
        {
            try
            {
                pDetalle_Dotacion = BODetalle_Dotacion.CrearDetalle_Dotacion(pDetalle_Dotacion, pusuario);
                return pDetalle_Dotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Detalle_DotacionService", "CrearDetalle_Dotacion", ex);
                return null;
            }
        }


        public Detalle_Dotacion ModificarDetalle_Dotacion(Detalle_Dotacion pDetalle_Dotacion, Usuario pusuario)
        {
            try
            {
                pDetalle_Dotacion = BODetalle_Dotacion.ModificarDetalle_Dotacion(pDetalle_Dotacion, pusuario);
                return pDetalle_Dotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Detalle_DotacionService", "ModificarDetalle_Dotacion", ex);
                return null;
            }
        }


        public void EliminarDetalle_Dotacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                BODetalle_Dotacion.EliminarDetalle_Dotacion(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Detalle_DotacionService", "EliminarDetalle_Dotacion", ex);
            }
        }


        public Detalle_Dotacion ConsultarDetalle_Dotacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                Detalle_Dotacion Detalle_Dotacion = new Detalle_Dotacion();
                Detalle_Dotacion = BODetalle_Dotacion.ConsultarDetalle_Dotacion(pId, pusuario);
                return Detalle_Dotacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Detalle_DotacionService", "ConsultarDetalle_Dotacion", ex);
                return null;
            }
        }


        public List<Detalle_Dotacion> ListarDetalle_Dotacion(Detalle_Dotacion pDetalle_Dotacion, Usuario pusuario)
        {
            try
            {
                return BODetalle_Dotacion.ListarDetalle_Dotacion(pDetalle_Dotacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Detalle_DotacionService", "ListarDetalle_Dotacion", ex);
                return null;
            }
        }


    }
}
