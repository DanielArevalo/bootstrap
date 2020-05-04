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
    public class PresupuestoFamiliarService
    {
        private PresupuestoFamiliarBusiness BOPresupuestoFamiliar;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PresupuestoFamiliar
        /// </summary>
        public PresupuestoFamiliarService()
        {
            BOPresupuestoFamiliar = new PresupuestoFamiliarBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } } //100114

        /// <summary>
        /// Servicio para crear PresupuestoFamiliar
        /// </summary>
        /// <param name="pEntity">Entidad PresupuestoFamiliar</param>
        /// <returns>Entidad PresupuestoFamiliar creada</returns>
        public PresupuestoFamiliar CrearPresupuestoFamiliar(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            try
            {
                return BOPresupuestoFamiliar.CrearPresupuestoFamiliar(pPresupuestoFamiliar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoFamiliarService", "CrearPresupuestoFamiliar", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar PresupuestoFamiliar
        /// </summary>
        /// <param name="pPresupuestoFamiliar">Entidad PresupuestoFamiliar</param>
        /// <returns>Entidad PresupuestoFamiliar modificada</returns>
        public PresupuestoFamiliar ModificarPresupuestoFamiliar(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            try
            {
                return BOPresupuestoFamiliar.ModificarPresupuestoFamiliar(pPresupuestoFamiliar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoFamiliarService", "ModificarPresupuestoFamiliar", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar PresupuestoFamiliar
        /// </summary>
        /// <param name="pId">identificador de PresupuestoFamiliar</param>
        public void EliminarPresupuestoFamiliar(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOPresupuestoFamiliar.EliminarPresupuestoFamiliar(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarPresupuestoFamiliar", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener PresupuestoFamiliar
        /// </summary>
        /// <param name="pId">identificador de PresupuestoFamiliar</param>
        /// <returns>Entidad PresupuestoFamiliar</returns>
        public PresupuestoFamiliar ConsultarPresupuestoFamiliar(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPresupuestoFamiliar.ConsultarPresupuestoFamiliar(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoFamiliarService", "ConsultarPresupuestoFamiliar", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de PresupuestoFamiliars a partir de unos filtros
        /// </summary>
        /// <param name="pPresupuestoFamiliar">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoFamiliar obtenidos</returns>
        public List<PresupuestoFamiliar> ListarPresupuestoFamiliar(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            try
            {
                return BOPresupuestoFamiliar.ListarPresupuestoFamiliar(pPresupuestoFamiliar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoFamiliarService", "ListarPresupuestoFamiliar", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de PresupuestoFamiliars a partir de unos filtros
        /// </summary>
        /// <param name="pPresupuestoFamiliar">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoFamiliar obtenidos</returns>
        public List<PresupuestoFamiliar> ListarPresupuestoFamiliarRepo(PresupuestoFamiliar pPresupuestoFamiliar, Usuario pUsuario)
        {
            try
            {
                return BOPresupuestoFamiliar.ListarPresupuestoFamiliar(pPresupuestoFamiliar, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoFamiliarService", "ListarPresupuestoFamiliarRepo", ex);
                return null;
            }
        }
    }


}