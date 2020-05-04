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
    public class TipoTasaHistService
    {
        private TipoTasaHistBusiness BOTipoTasaHist;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Usuario
        /// </summary>
        public TipoTasaHistService()
        {
            BOTipoTasaHist = new TipoTasaHistBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90106"; } }

        /// <summary>
        /// Servicio para crear tipo de tasa Hist
        /// </summary>
        /// <param name="pEntity">Entidad tipo tasa Hist</param>
        /// <returns>Entidad tipo tasa Hist creada</returns>
        public TipoTasaHist CrearTipoTasaHist(TipoTasaHist vTipoTasaHist, Usuario pUsuario)
        {
            try
            {
                return BOTipoTasaHist.CrearTipoTasaHist(vTipoTasaHist, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaHistService", "CrearTipoTasaHist", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar tipo de tasa Hist
        /// </summary>
        /// <param name="pUsuario">Entidad tipo de tasa Hist</param>
        /// <returns>Entidad tipo de tasa Hist modificada</returns>
        public TipoTasaHist ModificarTipoTasaHist(TipoTasaHist vTipoTasaHist, Usuario pUsuario)
        {
            try
            {
                return BOTipoTasaHist.ModificarTipoTasaHist(vTipoTasaHist, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaHistService", "ModificarTipoTasaHist", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TipoTasaHist
        /// </summary>
        /// <param name="pId">identificador de TipoTasaHist</param>
        public void EliminarTipoTasaHist(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTipoTasaHist.EliminarTipoTasaHist(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTipoTasaHist", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TipoTasaHist
        /// </summary>
        /// <param name="pId">identificador de TipoTasaHist</param>
        /// <returns>Entidad TipoTasaHist</returns>
        public TipoTasaHist ConsultarTipoTasaHist(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTipoTasaHist.ConsultarTipoTasaHist(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaHistService", "ConsultarTipoTasaHist", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TipoTasaHist a partir de unos filtros
        /// </summary>
        /// <param name="pUsuario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoTasaHist obtenidos</returns>
        public List<TipoTasaHist> ListarTipoTasaHist(TipoTasaHist vTipoTasaHist, Usuario pUsuario)
        {
            try
            {
                return BOTipoTasaHist.ListarTipoTasaHist(vTipoTasaHist, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoTasaHistService", "ListarTipoHistTasa", ex);
                return null;
            }
        }

    } 
}