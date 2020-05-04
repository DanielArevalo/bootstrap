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
    public class ControlTiemposService
    {
        private ControlTiemposBusiness BOControlTiempos;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ControlTiempos
        /// </summary>
        public ControlTiemposService()
        {
            BOControlTiempos = new ControlTiemposBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100149"; } }
        public string CodigoProgramaHojaRuta { get { return "100401"; } }
        public string CodigoProgramaReporte { get { return "100403"; } }
        public string CodigoProgramaRpEficiencia { get { return "100404"; } }
        /// <summary>
        /// Servicio para crear ControlTiempos
        /// </summary>
        /// <param name="pEntity">Entidad ControlTiempos</param>
        /// <returns>Entidad ControlTiempos creada</returns>
        public ControlTiempos CrearControlTiempos(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            try
            {
                return BOControlTiempos.CrearControlTiempos(pControlTiempos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposService", "CrearControlTiempos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ControlTiempos
        /// </summary>
        /// <param name="pControlTiempos">Entidad ControlTiempos</param>
        /// <returns>Entidad ControlTiempos modificada</returns>
        public ControlTiempos ModificarControlTiempos(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            try
            {
                return BOControlTiempos.ModificarControlTiempos(pControlTiempos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposService", "ModificarControlTiempos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ControlTiempos
        /// </summary>
        /// <param name="pId">identificador de ControlTiempos</param>
        public void EliminarControlTiempos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOControlTiempos.EliminarControlTiempos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarControlTiempos", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ControlTiempos
        /// </summary>
        /// <param name="pId">identificador de ControlTiempos</param>
        /// <returns>Entidad ControlTiempos</returns>
        public ControlTiempos ConsultarControlTiempos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOControlTiempos.ConsultarControlTiempos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposService", "ConsultarControlTiempos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ControlTiemposs a partir de unos filtros
        /// </summary>
        /// <param name="pControlTiempos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlTiempos obtenidos</returns>
        public List<ControlTiempos> ListarControlTiempos(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            try
            {
                return BOControlTiempos.ListarControlTiempos(pControlTiempos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposService", "ListarControlTiempos", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de ControlTiemposs a partir de unos filtros
        /// </summary>
        /// <param name="pControlTiempos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlTiempos obtenidos</returns>
        public List<ControlTiempos> ListarControlTiemposEfic(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            try
            {
                return BOControlTiempos.ListarControlTiemposEfic(pControlTiempos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposService", "ListarControlTiemposEfic", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ControlTiemposs a partir de unos filtros
        /// </summary>
        /// <param name="pControlTiempos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ControlTiempos obtenidos</returns>
        public List<ControlTiempos> ListarReporteMensajeria(ControlTiempos pControlTiempos, Usuario pUsuario)
        {
            try
            {
                return BOControlTiempos.ListarReporteMensajeria(pControlTiempos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ControlTiemposService", "ListarReporteMensajeria", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene  listas desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de elementos obtenidos</returns>
        public List<ControlTiempos> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return BOControlTiempos.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteService", "ListarListasMenu", ex);
                return null;
            }
        }

    }
}