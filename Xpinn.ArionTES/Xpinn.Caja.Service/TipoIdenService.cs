using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoIdenService
    {
        private TipoIdenBusiness BOTipoIden;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoIden
        /// </summary>
        public TipoIdenService()
        {
            BOTipoIden = new TipoIdenBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "SG090"; } }

        /// <summary>
        /// Servicio para crear TipoIden
        /// </summary>
        /// <param name="pEntity">Entidad TipoIden</param>
        /// <returns>Entidad TipoIden creada</returns>
        public TipoIden CrearTipoIden(TipoIden pTipoIden, Usuario pUsuario)
        {
            try
            {
                return BOTipoIden.CrearTipoIden(pTipoIden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoIdenService", "CrearTipoIden", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TipoIden
        /// </summary>
        /// <param name="pTipoIden">Entidad TipoIden</param>
        /// <returns>Entidad TipoIden modificada</returns>
        public TipoIden ModificarTipoIden(TipoIden pTipoIden, Usuario pUsuario)
        {
            try
            {
                return BOTipoIden.ModificarTipoIden(pTipoIden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoIdenService", "ModificarTipoIden", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoIden
        /// </summary>
        /// <param name="pId">identificador de TipoIden</param>
        public void EliminarTipoIden(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoIden.EliminarTipoIden(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoIden", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoIden
        /// </summary>
        /// <param name="pId">identificador de TipoIden</param>
        /// <returns>Entidad TipoIden</returns>
        public TipoIden ConsultarTipoIden(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoIden.ConsultarTipoIden(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoIdenService", "ConsultarTipoIden", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TipoIdens a partir de unos filtros
        /// </summary>
        /// <param name="pTipoIden">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoIden obtenidos</returns>
        public List<TipoIden> ListarTipoIden(TipoIden pTipoIden, Usuario pUsuario)
        {
            try
            {
                return BOTipoIden.ListarTipoIden(pTipoIden, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoIdenService", "ListarTipoIden", ex);
                return null;
            }
        }
    }
}