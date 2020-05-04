using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Programado.Business;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CuotasExtrasService
    {
        private CuotasExtrasBusiness BOCuotasExtras;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ProgramadoCuotasExtras
        /// </summary>
        public CuotasExtrasService()
        {
            BOCuotasExtras = new CuotasExtrasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220401"; } }

        /// <summary>
        /// Servicio para crear ProgramadoCuotasExtras
        /// </summary>
        /// <param name="pEntity">Entidad ProgramadoCuotasExtras</param>
        /// <returns>Entidad ProgramadoCuotasExtras creada</returns>
        public ProgramadoCuotasExtras CrearCuotasExtras(ProgramadoCuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                return BOCuotasExtras.CrearCuotasExtras(pCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("programadoCuotasExtrasService", "CrearCuotasExtras", ex);
                return null;
            }
        }      
        public ProgramadoCuotasExtras ConsultarCuotasExtras(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCuotasExtras.ConsultarCuotasExtras(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("programadoCuotasExtrasService", "ConsultarCuotasExtras", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de CuotasExtrass a partir de unos filtros
        /// </summary>
        /// <param name="pCuotasExtras">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ProgramadoCuotasExtras obtenidos</returns>
        public List<ProgramadoCuotasExtras> ListarCuotasExtras(ProgramadoCuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                return BOCuotasExtras.ListarCuotasExtras(pCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("programadoCuotasExtrasService", "ListarCuotasExtras", ex);
                return null;
            }
        }

     

    }
}