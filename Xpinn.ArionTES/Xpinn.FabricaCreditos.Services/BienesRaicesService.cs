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
    public class BienesRaicesService
    {
        private BienesRaicesBusiness BOBienesRaices;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para BienesRaices
        /// </summary>
        public BienesRaicesService()
        {
            BOBienesRaices = new BienesRaicesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } } //100112

        /// <summary>
        /// Servicio para crear BienesRaices
        /// </summary>
        /// <param name="pEntity">Entidad BienesRaices</param>
        /// <returns>Entidad BienesRaices creada</returns>
        public BienesRaices CrearBienesRaices(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            try
            {
                return BOBienesRaices.CrearBienesRaices(pBienesRaices, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BienesRaicesService", "CrearBienesRaices", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar BienesRaices
        /// </summary>
        /// <param name="pBienesRaices">Entidad BienesRaices</param>
        /// <returns>Entidad BienesRaices modificada</returns>
        public BienesRaices ModificarBienesRaices(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            try
            {
                return BOBienesRaices.ModificarBienesRaices(pBienesRaices, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BienesRaicesService", "ModificarBienesRaices", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar BienesRaices
        /// </summary>
        /// <param name="pId">identificador de BienesRaices</param>
        public void EliminarBienesRaices(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOBienesRaices.EliminarBienesRaices(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarBienesRaices", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener BienesRaices
        /// </summary>
        /// <param name="pId">identificador de BienesRaices</param>
        /// <returns>Entidad BienesRaices</returns>
        public BienesRaices ConsultarBienesRaices(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOBienesRaices.ConsultarBienesRaices(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BienesRaicesService", "ConsultarBienesRaices", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de BienesRaicess a partir de unos filtros
        /// </summary>
        /// <param name="pBienesRaices">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de BienesRaices obtenidos</returns>
        public List<BienesRaices> ListarBienesRaices(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            try
            {
                return BOBienesRaices.ListarBienesRaices(pBienesRaices, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BienesRaicesService", "ListarBienesRaices", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de BienesRaicess a partir de unos filtros
        /// </summary>
        /// <param name="pBienesRaices">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de BienesRaices obtenidos</returns>
        public List<BienesRaices> ListarBienesRaicesRepo(BienesRaices pBienesRaices, Usuario pUsuario)
        {
            try
            {
                return BOBienesRaices.ListarBienesRaicesRepo(pBienesRaices, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BienesRaicesService", "ListarBienesRaicesRepo", ex);
                return null;
            }
        }
    }
}

