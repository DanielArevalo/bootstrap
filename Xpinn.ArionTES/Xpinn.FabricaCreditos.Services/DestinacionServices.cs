using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Business;

namespace Xpinn.FabricaCreditos.Services
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

        public string CodigoPrograma { get { return "100212"; } }

        public Destinacion CrearDestinacion(Destinacion pDestinacion, Usuario pusuario, byte[] foto = null, byte[] banner = null)
        {
            try
            {
                pDestinacion = BODestinacion.CrearDestinacion(pDestinacion, foto, banner, pusuario);
                return pDestinacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionService", "CrearDestinacion", ex);
                return null;
            }
        }


        public Destinacion ModificarDestinacion(Destinacion pDestinacion, Usuario pusuario, byte[] foto = null, byte[] banner = null)
        {
            try
            {
                pDestinacion = BODestinacion.ModificarDestinacion(pDestinacion, foto, banner, pusuario);
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

        public List<LineaCred_Destinacion> ListarLineaCred_Destinacion(LineaCred_Destinacion pDestinacion, string pFiltro, Usuario pusuario)
        {
            try
            {
                return BODestinacion.ListarLineaCred_Destinacion(pDestinacion, pFiltro,pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DestinacionService", "ListarDestinacion", ex);
                return null;
            }
        }


    }
}
