using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EstructuraDetalleService
    {
        private EstructuraDetalleBusiness BOEstructuraDetalle;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para EstructuraDetalle
        /// </summary>
        public EstructuraDetalleService()
        {
            BOEstructuraDetalle = new EstructuraDetalleBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90106"; } }

        /// <summary>
        /// Servicio para crear EstructuraDetalle
        /// </summary>
        /// <param name="pEntity">Entidad EstructuraDetalle</param>
        /// <returns>Entidad EstructuraDetalle creada</returns>
        public EstructuraDetalle CrearEstructuraDetalle(EstructuraDetalle vEstructuraDetalle, Usuario pUsuario)
        {
            try
            {
                return BOEstructuraDetalle.CrearEstructuraDetalle(vEstructuraDetalle, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraDetalleService", "CrearEstructuraDetalle", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar EstructuraDetalle
        /// </summary>
        /// <param name="pEstructuraDetalle">Entidad EstructuraDetalle</param>
        /// <returns>Entidad EstructuraDetalle modificada</returns>
        public EstructuraDetalle ModificarEstructuraDetalle(EstructuraDetalle vEstructuraDetalle, Usuario pUsuario)
        {
            try
            {
                return BOEstructuraDetalle.ModificarEstructuraDetalle(vEstructuraDetalle, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraDetalleService", "ModificarEstructuraDetalle", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar EstructuraDetalle
        /// </summary>
        /// <param name="pId">identificador de EstructuraDetalle</param>
        public void EliminarEstructuraDetalle(int pId, Usuario pUsuario)
        {
            try
            {
                BOEstructuraDetalle.EliminarEstructuraDetalle(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarEstructuraDetalle", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener EstructuraDetalle
        /// </summary>
        /// <param name="pId">identificador de EstructuraDetalle</param>
        /// <returns>Entidad EstructuraDetalle</returns>
        public EstructuraDetalle ConsultarEstructuraDetalle(int pId, Usuario pUsuario)
        {
            try
            {
                return BOEstructuraDetalle.ConsultarEstructuraDetalle(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraDetalleService", "ConsultarEstructuraDetalle", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de EstructuraDetalles a partir de unos filtros
        /// </summary>
        /// <param name="pEstructuraDetalle">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EstructuraDetalle obtenidos</returns>
        public List<EstructuraDetalle> ListarEstructuraDetalle(EstructuraDetalle vEstructuraDetalle, Usuario pUsuario)
        {
            try
            {
                return BOEstructuraDetalle.ListarEstructuraDetalle(vEstructuraDetalle, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraDetalleService", "ListarEstructuraDetalle", ex);
                return null;
            }
        }


    }
}