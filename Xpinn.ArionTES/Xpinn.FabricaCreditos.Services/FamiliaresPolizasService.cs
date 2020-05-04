using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.ServiceModel;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Familiares
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class FamiliaresPolizasService
    {
        private FamiliaresPolizasBusiness BOFamiliaresPolizas;
        private ExcepcionBusiness BOExcepcion;

        public int consecutivo;
        /// <summary>
        /// Constructor del servicio para  Familiares
        /// </summary>
        public FamiliaresPolizasService()
        {
            BOFamiliaresPolizas = new FamiliaresPolizasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

                /// <summary>
        /// Obtiene la lista de Familiares dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Familiares obtenidos</returns>
        public List<FamiliaresPolizas> ListarFamiliares(FamiliaresPolizas pFamiliares, Usuario pUsuario)
        {
            try
            {
                return BOFamiliaresPolizas.ListarFamiliares(pFamiliares, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresPolizasService", "ListaFamiliares", ex);
                return null;
            }
        }

         /// <summary>
        /// Crea un Familiar
        /// </summary>
        /// <param name="pEntity">Entidad Familiares</param>
        /// <returns>Entidad creada</returns>
        public FamiliaresPolizas CrearFamiliares(FamiliaresPolizas pFamiliares, Usuario pUsuario)
        {
            try
            {
                return BOFamiliaresPolizas.CrearFamiliares(pFamiliares, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresPolizasService", "CrearFamiliares", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica  un Familiar
        /// </summary>
        /// <param name="pEntity">Entidad Familiares</param>
        /// <returns>Entidad modificada</returns>
        public FamiliaresPolizas ModificarFamiliares(FamiliaresPolizas pFamiliares, Usuario pUsuario)
        {
            try
            {
                return BOFamiliaresPolizas.ModificarFamiliares(pFamiliares, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresPolizasService", "ModificarFamiliares", ex);
                return null;
            }
        }


        /// <summary>
        /// Elimina un Familiar
        /// </summary>
        /// <param name="pEntity">Entidad Familiares</param>
        /// <returns>Entidad eliminada</returns>
        public void EliminarFamiliares(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOFamiliaresPolizas.EliminarFamiliares(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("FamiliaresPolizasService", "EliminarFamiliares", ex);
               
            }
        }
       
    }
}