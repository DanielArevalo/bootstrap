using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;


namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para Ciudad
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CiudadService
    {
        private CiudadBusiness BOCiudad;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Ciudad
        /// </summary>
        public CiudadService()
        {
            BOCiudad = new CiudadBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoCiudad;

        /// <summary>
        /// Obtiene la lista de Ciudades dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Ciudades obtenidos</returns>
        public List<Ciudad> ListarCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return BOCiudad.ListarCiudad(pCiudad, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "ListarCiudad", ex);
                return null;
            }
        }

        public Ciudad ConsultarCiudadXNombre(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return BOCiudad.ConsultarCiudadXNombre(pCiudad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "ConsultarCiudadXNombre", ex);
                return null;
            }
        }
        
        public List<Ciudad> ListadoCiudad(Ciudad pCiudad, Usuario pUsuario)
        {
            try
            {
                return BOCiudad.ListadoCiudad(pCiudad, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiudadService", "ListadoCiudad", ex);
                return null;
            }
        }

        public Ciudad CiudadTran(Usuario pUsuario)
        {
            try
            {
                return BOCiudad.CiudadTran(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Ciudad", "Ciudad", ex);
                return null;
            }
        }

    }
}
