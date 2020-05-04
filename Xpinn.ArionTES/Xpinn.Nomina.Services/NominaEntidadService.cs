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
    public class Nomina_EntidadService
    {

        private Nomina_EntidadBusiness BONomina_Entidad;
        private ExcepcionBusiness BOExcepcion;

        public Nomina_EntidadService()
        {
            BONomina_Entidad = new Nomina_EntidadBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250102"; } }

        public Nomina_Entidad CrearNomina_Entidad(Nomina_Entidad pNomina_Entidad, Usuario pusuario)
        {
            try
            {
                pNomina_Entidad = BONomina_Entidad.CrearNomina_Entidad(pNomina_Entidad, pusuario);
                return pNomina_Entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Nomina_EntidadService", "CrearNomina_Entidad", ex);
                return null;
            }
        }


        public Nomina_Entidad ModificarNomina_Entidad(Nomina_Entidad pNomina_Entidad, Usuario pusuario)
        {
            try
            {
                pNomina_Entidad = BONomina_Entidad.ModificarNomina_Entidad(pNomina_Entidad, pusuario);
                return pNomina_Entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Nomina_EntidadService", "ModificarNomina_Entidad", ex);
                return null;
            }
        }

        //public Nomina_Entidad ConsultarNomina_Entidad(Nomina_Entidad entitidadenti, Usuario usuario)
        //{
        //    throw new NotImplementedException();
        //}

        public void EliminarNomina_Entidad(Int64 pId, Usuario pusuario)
        {
            try
            {
                BONomina_Entidad.EliminarNomina_Entidad(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Nomina_EntidadService", "EliminarNomina_Entidad", ex);
            }
        }


        public Nomina_Entidad ConsultarNomina_Entidad(Int64 pId, Usuario pusuario)
        {
            try
            {
                Nomina_Entidad Nomina_Entidad = new Nomina_Entidad();
                Nomina_Entidad = BONomina_Entidad.ConsultarNomina_Entidad(pId, pusuario);
                return Nomina_Entidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Nomina_EntidadService", "ConsultarNomina_Entidad", ex);
                return null;
            }
        }

        public Nomina_Entidad ConsultaDatosEntidad(string pidentificacion, Usuario pUsuario)
        {
            try
            {
                return BONomina_Entidad.ConsultarDatos(pidentificacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultaDatosPersona", ex);
                return null;
            }
        }


        public List<Nomina_Entidad> ListarNomina_Entidad(string filtro, Usuario pusuario)
        {
            try
            {
                return BONomina_Entidad.ListarNomina_Entidad(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Nomina_EntidadService", "ListarNomina_Entidad", ex);
                return null;
            }
        }


    }
}