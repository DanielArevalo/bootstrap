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
    public class IngresosFamiliaService
    {
        private IngresosFamiliaBusiness BOIngresosFamilia;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para IngresosFamilia
        /// </summary>
        public IngresosFamiliaService()
        {
            BOIngresosFamilia = new IngresosFamiliaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }   //100129

        /// <summary>
        /// Servicio para crear IngresosFamilia
        /// </summary>
        /// <param name="pEntity">Entidad IngresosFamilia</param>
        /// <returns>Entidad IngresosFamilia creada</returns>
        public IngresosFamilia CrearIngresosFamilia(IngresosFamilia pIngresosFamilia, Usuario pUsuario)
        {
            try
            {
                return BOIngresosFamilia.CrearIngresosFamilia(pIngresosFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresosFamiliaService", "CrearIngresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar IngresosFamilia
        /// </summary>
        /// <param name="pIngresosFamilia">Entidad IngresosFamilia</param>
        /// <returns>Entidad IngresosFamilia modificada</returns>
        public IngresosFamilia ModificarIngresosFamilia(IngresosFamilia pIngresosFamilia, Usuario pUsuario)
        {
            try
            {
                return BOIngresosFamilia.ModificarIngresosFamilia(pIngresosFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresosFamiliaService", "ModificarIngresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar IngresosFamilia
        /// </summary>
        /// <param name="pId">identificador de IngresosFamilia</param>
        public void EliminarIngresosFamilia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOIngresosFamilia.EliminarIngresosFamilia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarIngresosFamilia", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener IngresosFamilia
        /// </summary>
        /// <param name="pId">identificador de IngresosFamilia</param>
        /// <returns>Entidad IngresosFamilia</returns>
        public IngresosFamilia ConsultarIngresosFamilia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOIngresosFamilia.ConsultarIngresosFamilia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresosFamiliaService", "ConsultarIngresosFamilia", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de IngresosFamilias a partir de unos filtros
        /// </summary>
        /// <param name="pIngresosFamilia">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de IngresosFamilia obtenidos</returns>
        public List<IngresosFamilia> ListarIngresosFamilia(IngresosFamilia pIngresosFamilia, Usuario pUsuario)
        {
            try
            {
                return BOIngresosFamilia.ListarIngresosFamilia(pIngresosFamilia, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresosFamiliaService", "ListarIngresosFamilia", ex);
                return null;
            }
        }
    }
}