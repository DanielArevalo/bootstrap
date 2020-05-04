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
    public class ReclamacionServiciosServicesService
    {

        private ReclamacionServiciosBussiness BOReclamacionServiciosServices;
        private ExcepcionBusiness BOExcepcion;

        public ReclamacionServiciosServicesService()
        {
            BOReclamacionServiciosServices = new ReclamacionServiciosBussiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "80109"; } }

        public ReclamacionServicios CrearReclamacionServiciosServices(DateTime pFechaIni,ReclamacionServicios pReclamacionServiciosServices, Usuario pusuario)
        {
            try
            {
                pReclamacionServiciosServices = BOReclamacionServiciosServices.Crearservicios(pFechaIni,pReclamacionServiciosServices, pusuario);
                return pReclamacionServiciosServices;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReclamacionServiciosServicesService", "CrearReclamacionServiciosServices", ex);
                return null;
            }
        }

        public bool ValidarFallecido(string pIdentificacion, Int64? pIdReclamacion, Usuario pUsuario)
        {
            try
            {
                return BOReclamacionServiciosServices.ValidarFallecido(pIdentificacion,pIdReclamacion, pUsuario);
               
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("serviciosBusiness", "Eliminarservicios", ex);
                return false;
            }
        }
        public ReclamacionServicios ModificarReclamacionServiciosServices(DateTime pFechaIni,ReclamacionServicios pReclamacionServiciosServices, Usuario pusuario)
        {
            try
            {
                pReclamacionServiciosServices = BOReclamacionServiciosServices.Modificarservicios(pFechaIni,pReclamacionServiciosServices, pusuario);
                return pReclamacionServiciosServices;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReclamacionServiciosServicesService", "ModificarReclamacionServiciosServices", ex);
                return null;
            }
        }


        public void EliminarReclamacionServiciosServices(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOReclamacionServiciosServices.Eliminarservicios(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReclamacionServiciosServicesService", "EliminarReclamacionServiciosServices", ex);
            }
        }


        public ReclamacionServicios ConsultarReclamacionServiciosServices(int pid, Usuario pusuario)
        {
            try
            {
                ReclamacionServicios ReclamacionServiciosServices = new ReclamacionServicios();
                ReclamacionServiciosServices = BOReclamacionServiciosServices.Consultarservicios(pid,pusuario);
                return ReclamacionServiciosServices;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReclamacionServiciosServicesService", "ConsultarReclamacionServiciosServices", ex);
                return null;
            }
        }


        public List<ReclamacionServicios> ListarReclamacionServiciosServices(string filtro, string pOrden, DateTime pFechaIni, Usuario pusuario)
        {
            try
            {
               List<ReclamacionServicios> servicios = new List<ReclamacionServicios>();
               servicios= BOReclamacionServiciosServices.Listarservicios(filtro, pOrden, pFechaIni,  pusuario);
               return servicios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReclamacionServiciosServicesService", "ListarReclamacionServiciosServices", ex);
                return null;
            }
        }


    }
}