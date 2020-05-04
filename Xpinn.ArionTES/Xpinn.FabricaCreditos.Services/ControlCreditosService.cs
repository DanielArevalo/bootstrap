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
    public class ControlCreditosService
    {
        private ControlCreditosBusiness BOControlCreditos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ControlCreditos
        /// </summary>
        public ControlCreditosService()
        {
            BOControlCreditos = new ControlCreditosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100150"; } }

        /// <summary>
        /// Servicio para crear ControlCreditos
        /// </summary>
        /// <param name="pEntity">Entidad ControlCreditos</param>
        /// <returns>Entidad ControlCreditos creada</returns>
        public ControlCreditos CrearControlCreditos(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            try
            {
                return BOControlCreditos.CrearControlCreditos(pControlCreditos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosService", "CrearControlCreditos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ControlCreditos
        /// </summary>
        /// <param name="pControlCreditos">Entidad ControlCreditos</param>
        /// <returns>Entidad ControlCreditos modificada</returns>
        public ControlCreditos ModificarControlCreditos(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            try
            {
                return BOControlCreditos.ModificarControlCreditos(pControlCreditos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosService", "ModificarControlCreditos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ControlCreditos
        /// </summary>
        /// <param name="pControlCreditos">Entidad ControlCreditos</param>
        /// <returns>Entidad ControlCreditos modificada</returns>
        public ControlCreditos Modificarfechadatcredito(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            try
            {
                return BOControlCreditos.Modificarfechadatcredito(pControlCreditos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosService", "Modificarfechadatcredito", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para Eliminar ControlCreditos
        /// </summary>
        /// <param name="pId">identificador de ControlCreditos</param>
        public void EliminarControlCreditos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOControlCreditos.EliminarControlCreditos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarControlCreditos", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ControlCreditos
        /// </summary>
        /// <param name="pId">identificador de ControlCreditos</param>
        /// <returns>Entidad ControlCreditos</returns>
        public ControlCreditos ConsultarControlCreditos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOControlCreditos.ConsultarControlCreditos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosService", "ConsultarControlCreditos", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener ControlCreditos
        /// </summary>
        /// <param name="pId">identificador de ControlCreditos</param>
        /// <returns>Entidad ControlCreditos</returns>
        public ControlCreditos ConsultarControl_Procesos(Int64 pId,String radicacion, Usuario pUsuario)
        {
            try
            {
                return BOControlCreditos.ConsultarControl_Procesos(pId,radicacion,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosService", "ConsultarControl_Procesos", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de ControlCreditoss a partir de unos filtros
        /// </summary>
        /// <param name="pControlCreditos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlCreditos obtenidos</returns>
        public List<ControlCreditos> ListarControlCreditos(ControlCreditos pControlCreditos, Usuario pUsuario)
        {
            try
            {
                return BOControlCreditos.ListarControlCreditos(pControlCreditos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosService", "ListarControlCreditos", ex);
                return null;
            }
        }

        public string obtenerCodTipoProceso(string estado, Usuario pUsuario)
        {
            try
            {
                return BOControlCreditos.obtenerCodTipoProceso(estado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlCreditosService", "obtenerCodTipoProceso", ex);
                return null;
            }
        }
    }
}