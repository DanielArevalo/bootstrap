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
    public class TipoLiqAporteServices
    {
        private TipoLiqAporteBusiness BOTipoLiqAporte;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TipoLiqAporte
        /// </summary>
        public TipoLiqAporteServices()
        {
            BOTipoLiqAporte = new TipoLiqAporteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170201"; } }

        /// <summary>
        /// Servicio para crear TipoLiqAporte
        /// </summary>
        /// <param name="pEntity">Entidad TipoLiqAporte</param>
        /// <returns>Entidad TipoLiqAporte creada</returns>
        public TipoLiqAporte CrearTipoLiqAporte(TipoLiqAporte vTipoLiqAporte, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiqAporte.CrearTipoLiqAporte(vTipoLiqAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiqAporteService", "CrearTipoLiqAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TipoLiqAporte
        /// </summary>
        /// <param name="pTipoLiqAporte">Entidad TipoLiqAporte</param>
        /// <returns>Entidad TipoLiqAporte modificada</returns>
        public TipoLiqAporte ModificarTipoLiqAporte(TipoLiqAporte vTipoLiqAporte, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiqAporte.ModificarTipoLiqAporte(vTipoLiqAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiqAporteService", "ModificarTipoLiqAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoLiqAporte
        /// </summary>
        /// <param name="pId">identificador de TipoLiqAporte</param>
        public void EliminarTipoLiqAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoLiqAporte.EliminarTipoLiqAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoLiqAporte", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoLiqAporte
        /// </summary>
        /// <param name="pId">identificador de TipoLiqAporte</param>
        /// <returns>Entidad TipoLiqAporte</returns>
        public TipoLiqAporte ConsultarTipoLiqAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiqAporte.ConsultarTipoLiqAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiqAporteService", "ConsultarTipoLiqAporte", ex);
                return null;
            }
        }

        public List<TipoLiqAporte> ListarTipoLiqAporte(TipoLiqAporte pTipoLiqAporte, Usuario pUsuario)
        {
            try
            {
                return BOTipoLiqAporte.ListarTipoLiqAporte(pTipoLiqAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiqAporteService", "ListarTipoLiqAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener TipoLiqAporte
        /// </summary>
        /// <param name="pId">identificador de TipoLiqAporte</param>
        /// <returns>Entidad TipoLiqAporte</returns>
        public TipoLiqAporte ConsultarMax(Usuario pUsuario)
        {
            try
            {
                return BOTipoLiqAporte.ConsultarMax(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoLiqAporteService", "ConsultarMax", ex);
                return null;
            }
        }


    }
}