using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;

namespace Xpinn.Aportes.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ParametrosAfiliacionServices
    {

        private ParametrosAfiliacionBusiness BOParametro;
        private ExcepcionBusiness BOExcepcion;
      
        public int Codigoaporte;
        /// <summary>
        /// Constructor del servicio para Aporte
        /// </summary>
        public ParametrosAfiliacionServices()
        {
            BOParametro = new ParametrosAfiliacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "20209"; } }


        public ParametrosAfiliacion CrearParametrosAfiliacion(ParametrosAfiliacion pParam, Usuario vUsuario, int opcion)
        {
            try
            {
                return BOParametro.CrearParametrosAfiliacion(pParam, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionServices", "CrearParametrosAfiliacion", ex);
                return null;
            }
        }


        public ParametrosAfiliacion ConsultarParametrosAfiliacion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BOParametro.ConsultarParametrosAfiliacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionServices", "ConsultarParametrosAfiliacion", ex);
                return null;
            }
        }


        public List<ParametrosAfiliacion> ListarParametrosAfiliacion(string filtro, Usuario vUsuario)
        {
            try
            {
                return BOParametro.ListarParametrosAfiliacion(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionServices", "ListarParametrosAfiliacion", ex);
                return null;
            }
        }

        public void EliminarParametrosAfiliacion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOParametro.EliminarParametrosAfiliacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionServices", "EliminarParametrosAfiliacion", ex);
            }
        }

        public PersonaActualizacion CrearPersona_Actualizacion(PersonaActualizacion pPersona, Usuario vUsuario)
        {
            try
            {
                return BOParametro.CrearPersona_Actualizacion(pPersona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionServices", "CrearPersona_Actualizacion", ex);
                return null;
            }
        }

        public PersonaActualizacion ConsultarPersona_actualizacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOParametro.ConsultarPersona_actualizacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionServices", "ConsultarPersona_actualizacion", ex);
                return null;
            }
        }

        public void ModificarPersona_Actualizacion(ref string pError, List<PersonaActualizacion> lstPersona, Usuario vUsuario)
        {
            try
            {
                BOParametro.ModificarPersona_Actualizacion(ref pError, lstPersona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionServices", "ModificarPersona_Actualizacion", ex);
            }
        }

        public void EliminarPersona_Actualizacion(decimal pId, Usuario vUsuario)
        {
            try
            {
                BOParametro.EliminarPersona_Actualizacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ParametrosAfiliacionServices", "EliminarPersona_Actualizacion", ex);
            }
        }


    }
}