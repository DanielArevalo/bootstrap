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
    public class FamiliaresService
    {
        private FamiliaresBusiness BOFamiliares;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Familiares
        /// </summary>
        public FamiliaresService()
        {
            BOFamiliares = new FamiliaresBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } } //100120

        /// <summary>
        /// Servicio para crear Familiares
        /// </summary>
        /// <param name="pEntity">Entidad Familiares</param>
        /// <returns>Entidad Familiares creada</returns>
        public Familiares CrearFamiliares(Familiares pFamiliares, Usuario pUsuario)
        {
            try
            {
                return BOFamiliares.CrearFamiliares(pFamiliares, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresService", "CrearFamiliares", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Familiares
        /// </summary>
        /// <param name="pFamiliares">Entidad Familiares</param>
        /// <returns>Entidad Familiares modificada</returns>
        public Familiares ModificarFamiliares(Familiares pFamiliares, Usuario pUsuario)
        {
            try
            {
                return BOFamiliares.ModificarFamiliares(pFamiliares, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresService", "ModificarFamiliares", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Familiares
        /// </summary>
        /// <param name="pId">identificador de Familiares</param>
        public void EliminarFamiliares(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOFamiliares.EliminarFamiliares(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarFamiliares", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Familiares
        /// </summary>
        /// <param name="pId">identificador de Familiares</param>
        /// <returns>Entidad Familiares</returns>
        public Familiares ConsultarFamiliares(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOFamiliares.ConsultarFamiliares(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresService", "ConsultarFamiliares", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Familiaress a partir de unos filtros
        /// </summary>
        /// <param name="pFamiliares">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Familiares obtenidos</returns>
        public List<Familiares> ListarFamiliares(Familiares pFamiliares, Usuario pUsuario, String Id)
        {
            try
            {
                return BOFamiliares.ListarFamiliares(pFamiliares, pUsuario, Id);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresService", "ListarFamiliares", ex);
                return null;
            }
        }




        /// <summary>
        /// Obtiene  listas desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de elementos obtenidos</returns>
        public List<Familiares> ListasDesplegables(Familiares pFamiliares, Usuario pUsuario, String ListaSolicitada)
        {
            try
            {
                return BOFamiliares.ListasDesplegables(pFamiliares, pUsuario, ListaSolicitada);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteService", "ListarListasMenu", ex);
                return null;
            }
        }




    }
}