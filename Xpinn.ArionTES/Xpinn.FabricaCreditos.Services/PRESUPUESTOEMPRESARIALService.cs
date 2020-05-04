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
    public class PresupuestoEmpresarialService
    {
        private PresupuestoEmpresarialBusiness BOPresupuestoEmpresarial;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PresupuestoEmpresarial
        /// </summary>
        public PresupuestoEmpresarialService()
        {
            BOPresupuestoEmpresarial = new PresupuestoEmpresarialBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } }//100113

        /// <summary>
        /// Servicio para crear PresupuestoEmpresarial
        /// </summary>
        /// <param name="pEntity">Entidad PresupuestoEmpresarial</param>
        /// <returns>Entidad PresupuestoEmpresarial creada</returns>
        public PresupuestoEmpresarial CrearPresupuestoEmpresarial(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            try
            {
                return BOPresupuestoEmpresarial.CrearPresupuestoEmpresarial(pPresupuestoEmpresarial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoEmpresarialService", "CrearPresupuestoEmpresarial", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar PresupuestoEmpresarial
        /// </summary>
        /// <param name="pPresupuestoEmpresarial">Entidad PresupuestoEmpresarial</param>
        /// <returns>Entidad PresupuestoEmpresarial modificada</returns>
        public PresupuestoEmpresarial ModificarPresupuestoEmpresarial(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            try
            {
                return BOPresupuestoEmpresarial.ModificarPresupuestoEmpresarial(pPresupuestoEmpresarial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoEmpresarialService", "ModificarPresupuestoEmpresarial", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar PresupuestoEmpresarial
        /// </summary>
        /// <param name="pId">identificador de PresupuestoEmpresarial</param>
        public void EliminarPresupuestoEmpresarial(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOPresupuestoEmpresarial.EliminarPresupuestoEmpresarial(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarPresupuestoEmpresarial", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener PresupuestoEmpresarial
        /// </summary>
        /// <param name="pId">identificador de PresupuestoEmpresarial</param>
        /// <returns>Entidad PresupuestoEmpresarial</returns>
        public PresupuestoEmpresarial ConsultarPresupuestoEmpresarial(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPresupuestoEmpresarial.ConsultarPresupuestoEmpresarial(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoEmpresarialService", "ConsultarPresupuestoEmpresarial", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de PresupuestoEmpresarials a partir de unos filtros
        /// </summary>
        /// <param name="pPresupuestoEmpresarial">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoEmpresarial obtenidos</returns>
        public List<PresupuestoEmpresarial> ListarPresupuestoEmpresarial(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            try
            {
                return BOPresupuestoEmpresarial.ListarPresupuestoEmpresarial(pPresupuestoEmpresarial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoEmpresarialService", "ListarPresupuestoEmpresarial", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de PresupuestoEmpresarials a partir de unos filtros
        /// </summary>
        /// <param name="pPresupuestoEmpresarial">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PresupuestoEmpresarial obtenidos</returns>
        public List<PresupuestoEmpresarial> ListarPresupuestoEmpresarialREPO(PresupuestoEmpresarial pPresupuestoEmpresarial, Usuario pUsuario)
        {
            try
            {
                return BOPresupuestoEmpresarial.ListarPresupuestoEmpresarialREPO(pPresupuestoEmpresarial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoEmpresarialService", "ListarPresupuestoEmpresarialREPO", ex);
                return null;
            }
        }
    }
}