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
    public class CiudadServices
    {

        private CiudadBusiness BOCiudad;
        private ExcepcionBusiness BOExcepcion;
      
        public int Codigoaporte;
        /// <summary>
        /// Constructor del servicio para Aporte
        /// </summary>
        public CiudadServices()
        {
            BOCiudad = new CiudadBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "20202"; } }

        
        public Ciudad Crear_Mod_Ciudad(Ciudad pCiudad, Usuario vUsuario, int opcion)
        {
            try
            {
                return BOCiudad.Crear_Mod_Ciudad(pCiudad, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadServices", "Crear_Mod_Ciudad", ex);
                return null;
            }
        }
        
        
        public Ciudad ConsultarCiudad(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOCiudad.ConsultarCiudad(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadServices", "ConsultarCiudad", ex);
                return null;
            }
        }


        public List<Ciudad> ListarCiudad(string filtro, Usuario vUsuario)
        {
            try
            {
                return BOCiudad.ListarCiudad(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadServices", "ListarCiudad", ex);
                return null;
            }
        }

        public void EliminarCiudad(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOCiudad.EliminarCiudad(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadServices", "EliminarCiudad", ex);
            }
        }


    }
}