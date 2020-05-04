using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Aprobador
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MotivoService
    {
        private MotivoBusiness BOMotivoNegacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public MotivoService()
        {
            BOMotivoNegacion = new MotivoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100204"; } }

        /// <summary>
        /// Crea un Aprobador
        /// </summary>
        /// <param name="pEntity">Entidad Programa</param>
        /// <returns>Entidad creada</returns>
        public Motivo CrearMotivo(Motivo pMotivo, Usuario pUsuario)
        {
            try
            {
                return BOMotivoNegacion.CrearMotivo(pMotivo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoService", "CrearMotivo", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de motivos de negacion
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de motivos de negacion obtenidos</returns>
        public List<Motivo> ListarMotivos(Motivo pMotivo, Usuario pUsuario)
        {
            try
            {
                return BOMotivoNegacion.ListarMotivos(pMotivo, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoService", "ListarMotivos", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un motivo de negacion
        /// </summary>
        /// <param name="pId">identificador del motivo de negacion</param>
        public void EliminarMotivo(Int32 pId, Usuario pUsuario)
        {
            try
            {
                BOMotivoNegacion.EliminarMotivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoService", "EliminarMotivo", ex);
            }
        }

        /// <summary>
        /// Obtiene la lista de motivos de negacion
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de motivos de negacion obtenidos</returns>
        public List<Motivo> ListarMotivosFiltro(Motivo pMotivo, Usuario pUsuario, int filtro)
        {
            try
            {
                return BOMotivoNegacion.ListarMotivosFiltro(pMotivo, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoService", "ListarMotivosFiltro", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de motivos de retiro
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de motivos de retiro obtenidos</returns>
        public List<Motivo> ListarMotivosRetiro(Motivo pMotivo, Usuario pUsuario)
        {
            try
            {
                return BOMotivoNegacion.ListarMotivosRetiro(pMotivo, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoService", "ListarMotivos", ex);
                return null;
            }
        }

    }
}
