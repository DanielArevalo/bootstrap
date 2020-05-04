using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Seguridad.Business;
using Xpinn.Seguridad.Entities;

namespace Xpinn.Seguridad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ModuloService
    {
        private ModuloBusiness BOModulo;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Modulo
        /// </summary>
        public ModuloService()
        {
            BOModulo = new ModuloBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90101"; } }

        /// <summary>
        /// Servicio para crear Modulo
        /// </summary>
        /// <param name="pEntity">Entidad Modulo</param>
        /// <returns>Entidad Modulo creada</returns>
        public Modulo CrearModulo(Modulo pModulo, Usuario pUsuario)
        {
            try
            {
                return BOModulo.CrearModulo(pModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModuloService", "CrearModulo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Modulo
        /// </summary>
        /// <param name="pModulo">Entidad Modulo</param>
        /// <returns>Entidad Modulo modificada</returns>
        public Modulo ModificarModulo(Modulo pModulo, Usuario pUsuario)
        {
            try
            {
                return BOModulo.ModificarModulo(pModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModuloService", "ModificarModulo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Modulo
        /// </summary>
        /// <param name="pId">identificador de Modulo</param>
        public void EliminarModulo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOModulo.EliminarModulo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarModulo", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Modulo
        /// </summary>
        /// <param name="pId">identificador de Modulo</param>
        /// <returns>Entidad Modulo</returns>
        public Modulo ConsultarModulo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOModulo.ConsultarModulo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModuloService", "ConsultarModulo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Modulos a partir de unos filtros
        /// </summary>
        /// <param name="pModulo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Modulo obtenidos</returns>
        public List<Modulo> ListarModulo(Modulo pModulo, Usuario pUsuario)
        {
            try
            {
                return BOModulo.ListarModulo(pModulo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ModuloService", "ListarModulo", ex);
                return null;
            }
        }
    }
}