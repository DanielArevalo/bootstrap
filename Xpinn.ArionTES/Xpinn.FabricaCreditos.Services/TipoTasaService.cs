using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoTasaService
    {
        private TipoTasaBusiness BOTipoTasa;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Usuario
        /// </summary>
        public TipoTasaService()
        {
            BOTipoTasa = new TipoTasaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90106"; } }

        /// <summary>
        /// Servicio para crear tipo de tasa
        /// </summary>
        /// <param name="pEntity">Entidad tipo tasa</param>
        /// <returns>Entidad tipo tasa creada</returns>
        public TipoTasa CrearTipoTasa(TipoTasa vTipoTasa, Usuario pUsuario)
        {
            try
            {
                return BOTipoTasa.CrearTipoTasa(vTipoTasa, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaService", "CrearTipoTasa", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar tipo de tasa
        /// </summary>
        /// <param name="pUsuario">Entidad tipo de tasa</param>
        /// <returns>Entidad tipo de tasa modificada</returns>
        public TipoTasa ModificarTipoTasa(TipoTasa vTipoTasa, Usuario pUsuario)
        {
            try
            {
                return BOTipoTasa.ModificarTipoTasa(vTipoTasa, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaService", "ModificarTipoTasa", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoTasa
        /// </summary>
        /// <param name="pId">identificador de TipoTasa</param>
        public void EliminarTipoTasa(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoTasa.EliminarTipoTasa(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoTasa", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoTasa
        /// </summary>
        /// <param name="pId">identificador de TipoTasa</param>
        /// <returns>Entidad TipoTasa</returns>
        public TipoTasa ConsultarTipoTasa(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoTasa.ConsultarTipoTasa(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaService", "ConsultarTipoTasa", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TipoTasa a partir de unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoTasa obtenidos</returns>
        public List<TipoTasa> ListarTipoTasa(TipoTasa vTipoTasa, Usuario pUsuario)
        {
            try
            {
                return BOTipoTasa.ListarTipoTasa(vTipoTasa, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaService", "ListarTipoTasa", ex);
                return null;
            }
        }

    }
}