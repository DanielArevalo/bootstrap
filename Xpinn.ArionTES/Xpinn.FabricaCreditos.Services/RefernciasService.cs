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
    public class RefernciasService
    {
        private RefernciasBusiness BOReferncias;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Referncias
        /// </summary>
        public RefernciasService()
        {
            BOReferncias = new RefernciasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }  //100136
        public string CodigoProgramaCode { get { return "100110"; } }

        /// <summary>
        /// Servicio para crear Referncias
        /// </summary>
        /// <param name="pEntity">Entidad Referncias</param>
        /// <returns>Entidad Referncias creada</returns>
        public Referncias CrearReferncias(Referncias pReferncias, Usuario pUsuario)
        {
            try
            {
                return BOReferncias.CrearReferncias(pReferncias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefernciasService", "CrearReferncias", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Referncias
        /// </summary>
        /// <param name="pReferncias">Entidad Referncias</param>
        /// <returns>Entidad Referncias modificada</returns>
        public Referncias ModificarReferncias(Referncias pReferncias, Usuario pUsuario)
        {
            try
            {
                return BOReferncias.ModificarReferncias(pReferncias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefernciasService", "ModificarReferncias", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Referncias
        /// </summary>
        /// <param name="pId">identificador de Referncias</param>
        public void EliminarReferncias(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOReferncias.EliminarReferncias(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarReferncias", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Referncias
        /// </summary>
        /// <param name="pId">identificador de Referncias</param>
        /// <returns>Entidad Referncias</returns>
        public Referncias ConsultarReferncias(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOReferncias.ConsultarReferncias(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefernciasService", "ConsultarReferncias", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Refernciass a partir de unos filtros
        /// </summary>
        /// <param name="pReferncias">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Referncias obtenidos</returns>
        public List<Referncias> ListarReferncias(Referncias pReferncias, Usuario pUsuario)
        {
            try
            {
                return BOReferncias.ListarReferncias(pReferncias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefernciasService", "ListarReferncias", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Refernciass a partir de unos filtros
        /// </summary>
        /// <param name="pReferncias">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Referncias obtenidos</returns>
        public List<Referncias> ListarReferenciasRepo(Referncias pReferncias, Usuario pUsuario)
        {
            try
            {
                return BOReferncias.ListarReferenciasRepo(pReferncias, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefernciasService", "ListarReferenciasRepo", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene  listas desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de elementos obtenidos</returns>
        public List<Referncias> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return BOReferncias.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteService", "ListarListasMenu", ex);
                return null;
            }
        }




        
    }
}