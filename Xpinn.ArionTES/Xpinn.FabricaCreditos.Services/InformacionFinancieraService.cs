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
    public class InformacionFinancieraService
    {
        private InformacionFinancieraBusiness BOInformacionFinanciera;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para InformacionFinanciera
        /// </summary>
        public InformacionFinancieraService()
        {
            BOInformacionFinanciera = new InformacionFinancieraBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "InfoFin01"; } }

        /// <summary>
        /// Servicio para crear InformacionFinanciera
        /// </summary>
        /// <param name="pEntity">Entidad InformacionFinanciera</param>
        /// <returns>Entidad InformacionFinanciera creada</returns>
        public InformacionFinanciera CrearInformacionFinanciera(InformacionFinanciera pInformacionFinanciera, Usuario pUsuario)
        {
            try
            {
                return BOInformacionFinanciera.CrearInformacionFinanciera(pInformacionFinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraService", "CrearInformacionFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar InformacionFinanciera
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad InformacionFinanciera</param>
        /// <returns>Entidad InformacionFinanciera modificada</returns>
        public InformacionFinanciera ModificarInformacionFinanciera(InformacionFinanciera pInformacionFinanciera, Usuario pUsuario)
        {
            try
            {
                return BOInformacionFinanciera.ModificarInformacionFinanciera(pInformacionFinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraService", "ModificarInformacionFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar InformacionFinanciera
        /// </summary>
        /// <param name="pId">identificador de InformacionFinanciera</param>
        public void EliminarInformacionFinanciera(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOInformacionFinanciera.EliminarInformacionFinanciera(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarInformacionFinanciera", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener InformacionFinanciera
        /// </summary>
        /// <param name="pId">identificador de InformacionFinanciera</param>
        /// <returns>Entidad InformacionFinanciera</returns>
        public InformacionFinanciera ConsultarInformacionFinanciera(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOInformacionFinanciera.ConsultarInformacionFinanciera(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraService", "ConsultarInformacionFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de InformacionFinancieras a partir de unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanciera(InformacionFinanciera pInformacionFinanciera, Usuario pUsuario)
        {
            try
            {
                return BOInformacionFinanciera.ListarInformacionFinanciera(pInformacionFinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraService", "ListarInformacionFinanciera", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de InformacionFinancieras a partir de unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanNegRepo(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return BOInformacionFinanciera.ListarInformacionFinanNegRepo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraService", "ListarInformacionFinanNegRepo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de InformacionFinancieras a partir de unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanFamRepo(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return BOInformacionFinanciera.ListarInformacionFinanFamRepo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraService", "ListarInformacionFinanFamRepo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de InformacionFinancieras a partir de unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarInformacionFinanFamRepoeg(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return BOInformacionFinanciera.ListarInformacionFinanFamRepoeg(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraService", "ListarInformacionFinanFamRepoeg", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de InformacionFinancieras a partir de unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarActivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return BOInformacionFinanciera.ListarActivos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraService", "ListarActivos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de InformacionFinancieras a partir de unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarPasivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return BOInformacionFinanciera.ListarPasivos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraService", "ListarActivos", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de InformacionFinancieras a partir de unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarbalanceFamActivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return BOInformacionFinanciera.ListarbalanceFamActivos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraService", "ListarbalanceFamActivos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de InformacionFinancieras a partir de unos filtros
        /// </summary>
        /// <param name="pInformacionFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de InformacionFinanciera obtenidos</returns>
        public List<InformacionFinanciera> ListarbalanceFamPasivos(InformacionFinanciera pId, Usuario pUsuario)
        {
            try
            {
                return BOInformacionFinanciera.ListarbalanceFamPasivos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InformacionFinancieraService", "ListarbalanceFamPasivos", ex);
                return null;
            }
        }
    }
}