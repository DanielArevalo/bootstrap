using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ProcesoContableService
    {
        private ProcesoContableBusiness BOProceso;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Proceso Contable
        /// </summary>
        public ProcesoContableService()
        {
            BOProceso = new ProcesoContableBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "31002"; } }

        /// <summary>
        /// Servicio para crear Proceso Contable
        /// </summary>
        /// <param name="pEntity">Entidad Proceso Contable</param>
        /// <returns>Entidad Proceso Contable creada</returns>
        public ProcesoContable CrearProcesoContable(ProcesoContable vProcesoContable, Usuario pUsuario)
        {
            try
            {
                return BOProceso.CrearProcesoContable(vProcesoContable, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoContableService", "CrearProcesoContable", ex);
                return null;
            }
        }

        public ProcesoContable ModificarProcesoContable(ProcesoContable pProceso, Usuario vUsuario)
        {
            try
            {
                return BOProceso.ModificarProcesoContable(pProceso, vUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoContableService", "ModificarProcesoContable", ex);
                return null;
            }
        }

        public List<ProcesoContable> ListarProcesoContable(ProcesoContable pProcesoContable, Usuario vUsuario)
        {
            try
            {
                return BOProceso.ListarProcesoContable(pProcesoContable, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoContableService", "ListarProcesoContable", ex);
                return null;
            }
        }

        public ProcesoContable ConsultarProcesoContable(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOProceso.ConsultarProcesoContable(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoContableService", "ConsultarProcesoContable", ex);
                return null;
            }
        }

        public ProcesoContable ConsultarProcesoContableOperacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOProceso.ConsultarProcesoContableOperacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoContableService", "ConsultarProcesoContableOperacion", ex);
                return null;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOProceso.ObtenerSiguienteCodigo(pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoContableService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

    }
}
