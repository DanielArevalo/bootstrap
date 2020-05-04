using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util; 
using System.ServiceModel; 
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Business;

namespace Xpinn.Aportes.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class Oficina_ciudadService
    {

        private Oficina_ciudadBusiness BOOficina_ciudad;
        private ExcepcionBusiness BOExcepcion;

        public Oficina_ciudadService()
        {
            BOOficina_ciudad = new Oficina_ciudadBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170206"; } }

        public Oficina_ciudad CrearOficina_ciudad(Oficina_ciudad pOficina_ciudad, Usuario pusuario)
        {
            try
            {
                pOficina_ciudad = BOOficina_ciudad.CrearOficina_ciudad(pOficina_ciudad, pusuario);
                return pOficina_ciudad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadService", "CrearOficina_ciudad", ex);
                return null;
            }
        }


        public Oficina_ciudad ModificarOficina_ciudad(Oficina_ciudad pOficina_ciudad, Usuario pusuario)
        {
            try
            {
                pOficina_ciudad = BOOficina_ciudad.ModificarOficina_ciudad(pOficina_ciudad, pusuario);
                return pOficina_ciudad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadService", "ModificarOficina_ciudad", ex);
                return null;
            }
        }


        public void EliminarOficina_ciudad(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOOficina_ciudad.EliminarOficina_ciudad(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadService", "EliminarOficina_ciudad", ex);
            }
        }


        public Oficina_ciudad ConsultarOficina_ciudad(Int64 pId, Usuario pusuario)
        {
            try
            {
                Oficina_ciudad Oficina_ciudad = new Oficina_ciudad();
                Oficina_ciudad = BOOficina_ciudad.ConsultarOficina_ciudad(pId, pusuario);
                return Oficina_ciudad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadService", "ConsultarOficina_ciudad", ex);
                return null;
            }
        }


        public List<Oficina_ciudad> ListarOficina_ciudad(Oficina_ciudad pOficina_ciudad, Usuario pusuario)
        {
            try
            {
                return BOOficina_ciudad.ListarOficina_ciudad(pOficina_ciudad, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadService", "ListarOficina_ciudad", ex);
                return null;
            }
        }

        public List<Oficina_ciudad> listaOficinaCiudadServices(Usuario pusuario, String pFiltro)
        {
            try
            {
                return BOOficina_ciudad.listaOficinaCiudadBussines(pusuario, pFiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadService", "listaOficinaCiudadServices", ex);
                return null;
            }
        }

        public Oficina_ciudad validaOficinaGuardaServices(Usuario vUsuario, Oficina_ciudad entiti, int opcion)
        {
            try
            {
                return BOOficina_ciudad.validaOficinaGuardaBusines(vUsuario, entiti, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Oficina_ciudadBusiness", "validaOficinaGuardaServices", ex);
                return null;
            }
        }
    }
}
