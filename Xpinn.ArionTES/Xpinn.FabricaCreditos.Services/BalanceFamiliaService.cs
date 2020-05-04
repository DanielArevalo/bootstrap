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
    public class BalanceFamiliaService
    {
        private BalanceFamiliaBusiness BOBalanceFamilia;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para BalanceFamilia
        /// </summary>
        public BalanceFamiliaService()
        {
            BOBalanceFamilia = new BalanceFamiliaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }  //100122

        /// <summary>
        /// Servicio para crear BalanceFamilia
        /// </summary>
        /// <param name="pEntity">Entidad BalanceFamilia</param>
        /// <returns>Entidad BalanceFamilia creada</returns>
        public BalanceFamilia CrearBalanceFamilia(BalanceFamilia pBalanceFamilia, Usuario pUsuario)
        {
            try
            {
                return BOBalanceFamilia.CrearBalanceFamilia(pBalanceFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceFamiliaService", "CrearBalanceFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar BalanceFamilia
        /// </summary>
        /// <param name="pBalanceFamilia">Entidad BalanceFamilia</param>
        /// <returns>Entidad BalanceFamilia modificada</returns>
        public BalanceFamilia ModificarBalanceFamilia(BalanceFamilia pBalanceFamilia, Usuario pUsuario)
        {
            try
            {
                return BOBalanceFamilia.ModificarBalanceFamilia(pBalanceFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceFamiliaService", "ModificarBalanceFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar BalanceFamilia
        /// </summary>
        /// <param name="pId">identificador de BalanceFamilia</param>
        public void EliminarBalanceFamilia(Int64 pId, Usuario pUsuario,Int64 Cod_persona,Int64 Cod_InfFin)
        {
            try
            {
                BOBalanceFamilia.EliminarBalanceFamilia(pId, pUsuario, Cod_persona, Cod_InfFin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarBalanceFamilia", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener BalanceFamilia
        /// </summary>
        /// <param name="pId">identificador de BalanceFamilia</param>
        /// <returns>Entidad BalanceFamilia</returns>
        public BalanceFamilia ConsultarBalanceFamilia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOBalanceFamilia.ConsultarBalanceFamilia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceFamiliaService", "ConsultarBalanceFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de BalanceFamilias a partir de unos filtros
        /// </summary>
        /// <param name="pBalanceFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de BalanceFamilia obtenidos</returns>
        public List<BalanceFamilia> ListarBalanceFamilia(BalanceFamilia pBalanceFamilia, Usuario pUsuario)
        {
            try
            {
                return BOBalanceFamilia.ListarBalanceFamilia(pBalanceFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceFamiliaService", "ListarBalanceFamilia", ex);
                return null;
            }
        }
    }
}