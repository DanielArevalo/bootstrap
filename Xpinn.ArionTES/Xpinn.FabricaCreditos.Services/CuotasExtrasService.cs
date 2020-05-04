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
    public class CuotasExtrasService
    {
        private CuotasExtrasBusiness BOCuotasExtras;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CuotasExtras
        /// </summary>
        public CuotasExtrasService()
        {
            BOCuotasExtras = new CuotasExtrasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100104"; } }

        /// <summary>
        /// Servicio para crear CuotasExtras
        /// </summary>
        /// <param name="pEntity">Entidad CuotasExtras</param>
        /// <returns>Entidad CuotasExtras creada</returns>
        public CuotasExtras CrearCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                return BOCuotasExtras.CrearCuotasExtras(pCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasService", "CrearCuotasExtras", ex);
                return null;
            }
        }


        public void CrearSolicitudCuotasExtras(List<CuotasExtras> lstCuotasExtras, Usuario pUsuario)
        {
            try
            {
                BOCuotasExtras.CrearSolicitudCuotasExtras(lstCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasService", "CrearSolicitudCuotasExtras", ex);
            }
        }

        public void CrearSolicitudCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                BOCuotasExtras.CrearSolicitudCuotasExtras(pCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasService", "CrearSolicitudCuotasExtras", ex);
            }
        }

        /// <summary>
        /// Servicio para modificar CuotasExtras
        /// </summary>
        /// <param name="pCuotasExtras">Entidad CuotasExtras</param>
        /// <returns>Entidad CuotasExtras modificada</returns>
        public CuotasExtras ModificarCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                return BOCuotasExtras.ModificarCuotasExtras(pCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasService", "ModificarCuotasExtras", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar CuotasExtras
        /// </summary>
        /// <param name="pId">identificador de CuotasExtras</param>
        public void EliminarCuotasExtras(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOCuotasExtras.EliminarCuotasExtras(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarCuotasExtras", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener CuotasExtras
        /// </summary>
        /// <param name="pId">identificador de CuotasExtras</param>
        /// <returns>Entidad CuotasExtras</returns>
        public CuotasExtras ConsultarCuotasExtras(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCuotasExtras.ConsultarCuotasExtras(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasService", "ConsultarCuotasExtras", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de CuotasExtrass a partir de unos filtros
        /// </summary>
        /// <param name="pCuotasExtras">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CuotasExtras obtenidos</returns>
        public List<CuotasExtras> ListarCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                return BOCuotasExtras.ListarCuotasExtras(pCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasService", "ListarCuotasExtras", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de solicitud de CoutasExtras a partir de unos filtros
        /// </summary>
        /// <param name="pCuotasExtras">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CuotasExtras obtenidos</returns>
        public List<CuotasExtras> ListarSolicitudCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                return BOCuotasExtras.ListarSolicitudCuotasExtras(pCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasService", "ListarSolicitudCuotasExtras", ex);
                return null;
            }
        }

        public List<CuotasExtras> ListarCuotasExtrasId(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCuotasExtras.ListarCuotasExtrasId(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasService", "ListarCuotasExtrasId", ex);
                return null;
            }
        }

        public void CrearSimulacionCuotasExtras(List<CuotasExtras> lstCuotasExtras, Usuario pUsuario)
        {
            try
            {
                BOCuotasExtras.CrearSimulacionCuotasExtras(lstCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasService", "CrearSolicitudCuotasExtras", ex);
            }
        }

        public void CrearSimulacionCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                BOCuotasExtras.CrearSimulacionCuotasExtras(pCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasService", "CrearSolicitudCuotasExtras", ex);
            }
        }

        public void EliminarCuotasExtrasTemporales(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOCuotasExtras.EliminarCuotasExtrasTemporales(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasService", "EliminarCuotasExtrasTemporales", ex);
            }
        }

    }
}