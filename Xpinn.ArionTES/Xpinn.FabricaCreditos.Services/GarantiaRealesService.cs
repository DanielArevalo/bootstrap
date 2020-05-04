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
    /// Servicios para Garantias 
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class GarantiasRealesService
    {
        private GarantiasRealesBusiness BOGarantiasReales;
        private ExcepcionBusiness BOExcepcion;
        public int consecutivo;

        public string CodigoPrograma { get { return "100205"; } }//codigo anterio 100144
        /// <summary>
        /// Constructor del servicio para GarantiasReales
        /// </summary>
        public GarantiasRealesService()
        {
            BOGarantiasReales = new GarantiasRealesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

       

        /// <summary>
        /// Servicio para crear GarantiasReales
        /// </summary>
        /// <param name="pEntity">Entidad GarantiasReales</param>
        /// <returns>Entidad GarantiasReales creada</returns>
        public GarantiasReales CrearGarantiasReales(GarantiasReales pGarantiasReales, Usuario pUsuario)
        {
            try
            {
                return BOGarantiasReales.CrearGarantiasReales(pGarantiasReales, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasRealesService", "CrearGarantiasReales", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar GarantiasReales
        /// </summary>
        /// <param name="pGarantiasReales">Entidad GarantiasReales</param>
        /// <returns>Entidad GarantiasReales modificada</returns>
        public GarantiasReales ModificarGarantiasReales(GarantiasReales pGarantiasReales, Usuario pUsuario)
        {
            try
            {
                return BOGarantiasReales.ModificarGarantiasReales(pGarantiasReales, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasRealesService", "ModificarGarantiasReales", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar GarantiasReales
        /// </summary>
        /// <param name="pId">identificador de GarantiasReales</param>
        public void EliminarGarantiasReales(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOGarantiasReales.EliminarGarantiasReales(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarGarantiasReales", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener GarantiasReales
        /// </summary>
        /// <param name="pId">identificador de GarantiasReales</param>
        /// <returns>Entidad GarantiasReales</returns>
        public GarantiasReales ConsultarGarantiasReales(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOGarantiasReales.ConsultarGarantiasReales(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasRealesService", "ConsultarGarantiasReales", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de GarantiasReales a partir de unos filtros
        /// </summary>
        /// <param name="pGarantiasReales">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de GarantiasReales obtenidos</returns>
        public List<GarantiasReales> ListarGarantiasReales(GarantiasReales pGarantiasReales, Usuario pUsuario)
        {
            try
            {
                return BOGarantiasReales.ListarGarantiasReales(pGarantiasReales, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasRealesService", "ListarGarantiasReales", ex);
                return null;
            }
        }



    }
}