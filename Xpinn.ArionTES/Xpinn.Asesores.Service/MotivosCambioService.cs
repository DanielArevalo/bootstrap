using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicios para Programa 
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MotivosCambioService
    {
        private MotivosCambioBusiness BOMotivosCambio;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para MotivosCambio
        /// </summary>
        public MotivosCambioService()
        {
            BOMotivosCambio = new MotivosCambioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110201"; } }

        /// <summary>
        /// Servicio para crear MotivosCambio
        /// </summary>
        /// <param name="pEntity">Entidad MotivosCambio</param>
        /// <returns>Entidad MotivosCambio creada</returns>
        public MotivosCambio CrearMotivosCambio(MotivosCambio pMotivosCambio, Usuario pUsuario)
        {
            try
            {
                return BOMotivosCambio.CrearMotivosCambio(pMotivosCambio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivosCambioService", "CrearMotivosCambio", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar MotivosCambio
        /// </summary>
        /// <param name="pMotivosCambio">Entidad MotivosCambio</param>
        /// <returns>Entidad MotivosCambio modificada</returns>
        public MotivosCambio ModificarMotivosCambio(MotivosCambio pMotivosCambio, Usuario pUsuario)
        {
            try
            {
                return BOMotivosCambio.ModificarMotivosCambio(pMotivosCambio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivosCambioService", "ModificarMotivosCambio", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar MotivosCambio
        /// </summary>
        /// <param name="pId">identificador de MotivosCambio</param>
        public void EliminarMotivosCambio(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOMotivosCambio.EliminarMotivosCambio(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarMotivosCambio", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener MotivosCambio
        /// </summary>
        /// <param name="pId">identificador de MotivosCambio</param>
        /// <returns>Entidad MotivosCambio</returns>
        public MotivosCambio ConsultarMotivosCambio(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOMotivosCambio.ConsultarMotivosCambio(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivosCambioService", "ConsultarMotivosCambio", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MotivosCambios a partir de unos filtros
        /// </summary>
        /// <param name="pMotivosCambio">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MotivosCambio obtenidos</returns>
        public List<MotivosCambio> ListarMotivosCambio(MotivosCambio pMotivosCambio, Usuario pUsuario)
        {
            try
            {
                return BOMotivosCambio.ListarMotivosCambio(pMotivosCambio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivosCambioService", "ListarMotivosCambio", ex);
                return null;
            }
        }
    }
}