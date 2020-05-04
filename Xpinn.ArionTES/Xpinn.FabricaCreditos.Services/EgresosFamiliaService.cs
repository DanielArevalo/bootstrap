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
    public class EgresosFamiliaService
    {
        private EgresosFamiliaBusiness BOEgresosFamilia;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para EgresosFamilia
        /// </summary>
        public EgresosFamiliaService()
        {
            BOEgresosFamilia = new EgresosFamiliaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } } //100125

        /// <summary>
        /// Servicio para crear EgresosFamilia
        /// </summary>
        /// <param name="pEntity">Entidad EgresosFamilia</param>
        /// <returns>Entidad EgresosFamilia creada</returns>
        public EgresosFamilia CrearEgresosFamilia(EgresosFamilia pEgresosFamilia, Usuario pUsuario)
        {
            try
            {
                return BOEgresosFamilia.CrearEgresosFamilia(pEgresosFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EgresosFamiliaService", "CrearEgresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar EgresosFamilia
        /// </summary>
        /// <param name="pEgresosFamilia">Entidad EgresosFamilia</param>
        /// <returns>Entidad EgresosFamilia modificada</returns>
        public EgresosFamilia ModificarEgresosFamilia(EgresosFamilia pEgresosFamilia, Usuario pUsuario)
        {
            try
            {
                return BOEgresosFamilia.ModificarEgresosFamilia(pEgresosFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EgresosFamiliaService", "ModificarEgresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar EgresosFamilia
        /// </summary>
        /// <param name="pId">identificador de EgresosFamilia</param>
        public void EliminarEgresosFamilia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOEgresosFamilia.EliminarEgresosFamilia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarEgresosFamilia", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener EgresosFamilia
        /// </summary>
        /// <param name="pId">identificador de EgresosFamilia</param>
        /// <returns>Entidad EgresosFamilia</returns>
        public EgresosFamilia ConsultarEgresosFamilia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOEgresosFamilia.ConsultarEgresosFamilia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EgresosFamiliaService", "ConsultarEgresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de EgresosFamilias a partir de unos filtros
        /// </summary>
        /// <param name="pEgresosFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de EgresosFamilia obtenidos</returns>
        public List<EgresosFamilia> ListarEgresosFamilia(EgresosFamilia pEgresosFamilia, Usuario pUsuario)
        {
            try
            {
                return BOEgresosFamilia.ListarEgresosFamilia(pEgresosFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EgresosFamiliaService", "ListarEgresosFamilia", ex);
                return null;
            }
        }
    }
}