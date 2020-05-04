using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CobrosCreditoService
    {
        private CobrosCreditoBusiness BOCobrosCredito;
        private ExcepcionBusiness BOExcepcion;
        public int Credito;
        /// <summary>
        /// Constructor del servicio para CobrosCredito
        /// </summary>
        public CobrosCreditoService()
        {
            BOCobrosCredito = new CobrosCreditoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110103"; } }

        /// <summary>
        /// Servicio para crear CobrosCredito
        /// </summary>
        /// <param name="pEntity">Entidad CobrosCredito</param>
        /// <returns>Entidad CobrosCredito creada</returns>
        public CobrosCredito CrearCobrosCredito(CobrosCredito pCobrosCredito, Usuario pUsuario)
        {
            try
            {
                return BOCobrosCredito.CrearCobrosCredito(pCobrosCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobrosCreditoService", "CrearCobrosCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar CobrosCredito
        /// </summary>
        /// <param name="pCobrosCredito">Entidad CobrosCredito</param>
        /// <returns>Entidad CobrosCredito modificada</returns>
        public CobrosCredito ModificarCobrosCredito(CobrosCredito pCobrosCredito, Usuario pUsuario)
        {
            try
            {
                return BOCobrosCredito.ModificarCobrosCredito(pCobrosCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobrosCreditoService", "ModificarCobrosCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar CobrosCredito
        /// </summary>
        /// <param name="pId">identificador de CobrosCredito</param>
        public void EliminarCobrosCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOCobrosCredito.EliminarCobrosCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarCobrosCredito", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener CobrosCredito
        /// </summary>
        /// <param name="pId">identificador de CobrosCredito</param>
        /// <returns>Entidad CobrosCredito</returns>
        public CobrosCredito ConsultarCobrosCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCobrosCredito.ConsultarCobrosCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobrosCreditoService", "ConsultarCobrosCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de CobrosCreditos a partir de unos filtros
        /// </summary>
        /// <param name="pCobrosCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CobrosCredito obtenidos</returns>
        public List<CobrosCredito> ListarCobrosCredito(CobrosCredito pCobrosCredito, Usuario pUsuario)
        {
            try
            {
                return BOCobrosCredito.ListarCobrosCredito(pCobrosCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CobrosCreditoService", "ListarCobrosCredito", ex);
                return null;
            }
        }
    }
}