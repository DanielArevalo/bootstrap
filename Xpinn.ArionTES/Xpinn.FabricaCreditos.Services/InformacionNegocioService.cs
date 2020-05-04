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
    public class InformacionNegocioService
    {
        private InformacionNegocioBusiness BOInformacionNegocio;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para InformacionNegocio
        /// </summary>
        public InformacionNegocioService()
        {
            BOInformacionNegocio = new InformacionNegocioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }  //100135

        /// <summary>
        /// Servicio para crear InformacionNegocio
        /// </summary>
        /// <param name="pEntity">Entidad InformacionNegocio</param>
        /// <returns>Entidad InformacionNegocio creada</returns>
        public InformacionNegocio CrearInformacionNegocio(InformacionNegocio pInformacionNegocio, Usuario pUsuario)
        {
            try
            {
                return BOInformacionNegocio.CrearInformacionNegocio(pInformacionNegocio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionNegocioService", "CrearInformacionNegocio", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar InformacionNegocio
        /// </summary>
        /// <param name="pInformacionNegocio">Entidad InformacionNegocio</param>
        /// <returns>Entidad InformacionNegocio modificada</returns>
        public InformacionNegocio ModificarInformacionNegocio(InformacionNegocio pInformacionNegocio, Usuario pUsuario)
        {
            try
            {
                return BOInformacionNegocio.ModificarInformacionNegocio(pInformacionNegocio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionNegocioService", "ModificarInformacionNegocio", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar InformacionNegocio
        /// </summary>
        /// <param name="pId">identificador de InformacionNegocio</param>
        public void EliminarInformacionNegocio(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOInformacionNegocio.EliminarInformacionNegocio(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarInformacionNegocio", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener InformacionNegocio
        /// </summary>
        /// <param name="pId">identificador de InformacionNegocio</param>
        /// <returns>Entidad InformacionNegocio</returns>
        public InformacionNegocio ConsultarInformacionNegocio(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOInformacionNegocio.ConsultarInformacionNegocio(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionNegocioService", "ConsultarInformacionNegocio", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de InformacionNegocios a partir de unos filtros
        /// </summary>
        /// <param name="pInformacionNegocio">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionNegocio obtenidos</returns>
        public List<InformacionNegocio> ListarInformacionNegocio(InformacionNegocio pInformacionNegocio, Usuario pUsuario)
        {
            try
            {
                return BOInformacionNegocio.ListarInformacionNegocio(pInformacionNegocio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionNegocioService", "ListarInformacionNegocio", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene  listas desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de elementos obtenidos</returns>
        public List<InformacionNegocio> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return BOInformacionNegocio.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteService", "ListarListasMenu", ex);
                return null;
            }
        }



    }
}