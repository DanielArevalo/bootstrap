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
    public class consultasdatacreditoService
    {
        private consultasdatacreditoBusiness BOconsultasdatacredito;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para consultasdatacredito
        /// </summary>
        public consultasdatacreditoService()
        {
            BOconsultasdatacredito = new consultasdatacreditoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100107"; } }

        /// <summary>
        /// Servicio para crear consultasdatacredito
        /// </summary>
        /// <param name="pEntity">Entidad consultasdatacredito</param>
        /// <returns>Entidad consultasdatacredito creada</returns>
        public consultasdatacredito Crearconsultasdatacredito(consultasdatacredito pconsultasdatacredito, Usuario pUsuario)
        {
            try
            {
                return BOconsultasdatacredito.Crearconsultasdatacredito(pconsultasdatacredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultasdatacreditoService", "Crearconsultasdatacredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar consultasdatacredito
        /// </summary>
        /// <param name="pconsultasdatacredito">Entidad consultasdatacredito</param>
        /// <returns>Entidad consultasdatacredito modificada</returns>
        public consultasdatacredito Modificarconsultasdatacredito(consultasdatacredito pconsultasdatacredito, Usuario pUsuario)
        {
            try
            {
                return BOconsultasdatacredito.Modificarconsultasdatacredito(pconsultasdatacredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultasdatacreditoService", "Modificarconsultasdatacredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar consultasdatacredito
        /// </summary>
        /// <param name="pId">identificador de consultasdatacredito</param>
        public void Eliminarconsultasdatacredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOconsultasdatacredito.Eliminarconsultasdatacredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "Eliminarconsultasdatacredito", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener consultasdatacredito
        /// </summary>
        /// <param name="pId">identificador de consultasdatacredito</param>
        /// <returns>Entidad consultasdatacredito</returns>
        public consultasdatacredito Consultarconsultasdatacredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOconsultasdatacredito.Consultarconsultasdatacredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultasdatacreditoService", "Consultarconsultasdatacredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de consultasdatacreditos a partir de unos filtros
        /// </summary>
        /// <param name="pconsultasdatacredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de consultasdatacredito obtenidos</returns>
        public List<consultasdatacredito> Listarconsultasdatacredito(consultasdatacredito pconsultasdatacredito, Usuario pUsuario)
        {
            try
            {
                return BOconsultasdatacredito.Listarconsultasdatacredito(pconsultasdatacredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultasdatacreditoService", "Listarconsultasdatacredito", ex);
                return null;
            }
        }


        public List<CreditoEmpresaRecaudo> ListarPersona_Empresa_Recaudo(Int64 pIdPersona, Usuario vUsuario)
        {
            try
            {
                return BOconsultasdatacredito.ListarPersona_Empresa_Recaudo(pIdPersona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("consultasdatacreditoService", "ListarPersona_Empresa_Recaudo", ex);
                return null;
            }
        }


    }
}