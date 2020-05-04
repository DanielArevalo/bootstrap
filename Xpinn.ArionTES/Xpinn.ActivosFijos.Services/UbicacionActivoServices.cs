using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.ActivosFijos.Business;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.ActivosFijos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class UbicacionActivoservices
    {
        private UbicacionActivoBusiness BOUbicacionActivo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para UbicacionActivo
        /// </summary>
        public UbicacionActivoservices()
        {
            BOUbicacionActivo = new UbicacionActivoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "50204"; } }

        /// <summary>
        /// Servicio para crear UbicacionActivo
        /// </summary>
        /// <param name="pEntity">Entidad UbicacionActivo</param>
        /// <returns>Entidad UbicacionActivo creada</returns>
        public UbicacionActivo CrearUbicacionActivo(UbicacionActivo vUbicacionActivo, Usuario pUsuario)
        {
            try
            {
                return BOUbicacionActivo.CrearUbicacionActivo(vUbicacionActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UbicacionActivoservices", "CrearUbicacionActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar UbicacionActivo
        /// </summary>
        /// <param name="pUbicacionActivo">Entidad UbicacionActivo</param>
        /// <returns>Entidad UbicacionActivo modificada</returns>
        public UbicacionActivo ModificarUbicacionActivo(UbicacionActivo vUbicacionActivo, Usuario pUsuario)
        {
            try
            {
                return BOUbicacionActivo.ModificarUbicacionActivo(vUbicacionActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UbicacionActivoservices", "ModificarUbicacionActivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar UbicacionActivo
        /// </summary>
        /// <param name="pId">identificador de UbicacionActivo</param>
        public void EliminarUbicacionActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOUbicacionActivo.EliminarUbicacionActivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarUbicacionActivo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener UbicacionActivo
        /// </summary>
        /// <param name="pId">identificador de UbicacionActivo</param>
        /// <returns>Entidad UbicacionActivo</returns>
        public UbicacionActivo ConsultarUbicacionActivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOUbicacionActivo.ConsultarUbicacionActivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UbicacionActivoservices", "ConsultarUbicacionActivo", ex);
                return null;
            }
        }

        public List<UbicacionActivo> ListarUbicacionActivo(UbicacionActivo pUbicacionActivo, Usuario pUsuario)
        {
            try
            {
                return BOUbicacionActivo.ListarUbicacionActivo(pUbicacionActivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UbicacionActivoservices", "ListarUbicacionActivo", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOUbicacionActivo.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 0;
            }
        }

    }
}