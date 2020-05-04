using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MotivoNovedadServices
    {
        private MotivoNovedadBusiness BOMotivoNovedad;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para MotivoNovedad
        /// </summary>
        public MotivoNovedadServices()
        {
            BOMotivoNovedad = new MotivoNovedadBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220102"; } }

        /// <summary>
        /// Servicio para crear MotivoNovedad
        /// </summary>
        /// <param name="pEntity">Entidad MotivoNovedad</param>
        /// <returns>Entidad MotivoNovedad creada</returns>
        public MotivoNovedad CrearMotivoNovedad(MotivoNovedad vMotivoNovedad, Usuario pUsuario)
        {
            try
            {
                return BOMotivoNovedad.CrearMotivoNovedad(vMotivoNovedad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoNovedadservice", "CrearMotivoNovedad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar MotivoNovedad
        /// </summary>
        /// <param name="pMotivoNovedad">Entidad MotivoNovedad</param>
        /// <returns>Entidad MotivoNovedad modificada</returns>
        public MotivoNovedad ModificarMotivoNovedad(MotivoNovedad vMotivoNovedad, Usuario pUsuario)
        {
            try
            {
                return BOMotivoNovedad.ModificarMotivoNovedad(vMotivoNovedad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoNovedadservice", "ModificarMotivoNovedad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar MotivoNovedad
        /// </summary>
        /// <param name="pId">identificador de MotivoNovedad</param>
        public void EliminarMotivoNovedad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOMotivoNovedad.EliminarMotivoNovedad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarMotivoNovedad", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener MotivoNovedad
        /// </summary>
        /// <param name="pId">identificador de MotivoNovedad</param>
        /// <returns>Entidad MotivoNovedad</returns>
        public MotivoNovedad ConsultarMotivoNovedad(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOMotivoNovedad.ConsultarMotivoNovedad(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoNovedadservice", "ConsultarMotivoNovedad", ex);
                return null;
            }
        }

        public List<MotivoNovedad> ListarMotivoNovedad(MotivoNovedad pMotivoNovedad, Usuario pUsuario)
        {
            try
            {
                return BOMotivoNovedad.ListarMotivoNovedad(pMotivoNovedad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MotivoNovedadservice", "ListarMotivoNovedad", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOMotivoNovedad.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 0;
            }
        }

    }
}