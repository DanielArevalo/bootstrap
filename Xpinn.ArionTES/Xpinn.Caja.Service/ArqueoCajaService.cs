using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ArqueoCajaService
    {
        private ArqueoCajaBusiness BOArqueoCaja;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ArqueoCaja
        /// </summary>
        public ArqueoCajaService()
        {
            BOArqueoCaja = new ArqueoCajaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "120106"; } }
        public string CodigoPrograma2 { get { return "120105"; } }
        public string CodigoPrograma3 { get { return "120107"; } }
        public string CodigoProgramaConsultasArqueos { get { return "120207"; } }
        public string CodigoProgramaTesoreria { get { return "40106"; } }
        public string CodigoProgramaArqueoCaja { get { return "40107"; } }
        /// <summary>
        /// Servicio para crear ArqueoCaja
        /// </summary>
        /// <param name="pEntity">Entidad ArqueoCaja</param>
        /// <returns>Entidad ArqueoCaja creada</returns>
        public ArqueoCaja CrearArqueoCaja(ArqueoCaja pArqueoCaja, GridView saldos, GridView cheques, Usuario pUsuario)
        {
            try
            {
                return BOArqueoCaja.CrearArqueoCaja(pArqueoCaja, saldos, cheques, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaService", "CrearArqueoCaja", ex);
                return null;
            }
        }

        public ArqueoCaja ArqueoCajadetalle(ArqueoCaja pArqueoCaja, GridView saldos, GridView cheques, Usuario pUsuario)
        {
            try
            {
                return BOArqueoCaja.ArqueoCajadetalle(pArqueoCaja, saldos, cheques, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaService", "CrearArqueoCaja", ex);
                return null;
            }
        }

        public ArqueoCaja ArqueosGuardarEnDetalle(ArqueoCaja pArqueoCaja, GridView saldos, GridView cheques, Usuario pUsuario)
        {
            try
            {
                return BOArqueoCaja.ArqueosGuardarEnDetalle(pArqueoCaja, saldos, cheques, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaService", "ArqueosGuardarEnDetalle", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ArqueoCaja
        /// </summary>
        /// <param name="pArqueoCaja">Entidad ArqueoCaja</param>
        /// <returns>Entidad ArqueoCaja modificada</returns>
        public ArqueoCaja ModificarArqueoCaja(ArqueoCaja pArqueoCaja, Usuario pUsuario)
        {
            try
            {
                return BOArqueoCaja.ModificarArqueoCaja(pArqueoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaService", "ModificarArqueoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ArqueoCaja
        /// </summary>
        /// <param name="pId">identificador de ArqueoCaja</param>
        public void EliminarArqueoCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOArqueoCaja.EliminarArqueoCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarArqueoCaja", ex);
            }
        }


        public void EliminarArqueo(DateTime pId, Usuario pUsuario)
        {
            try
            {
                BOArqueoCaja.EliminarArqueo(pId, pUsuario);
            }
            catch 
            {
            }
        }

        /// <summary>
        /// Servicio para obtener ArqueoCaja
        /// </summary>
        /// <param name="pId">identificador de ArqueoCaja</param>
        /// <returns>Entidad ArqueoCaja</returns>
        public ArqueoCaja ConsultarArqueoCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOArqueoCaja.ConsultarArqueoCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaService", "ConsultarArqueoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de ArqueoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pArqueoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ArqueoCaja obtenidos</returns>
        public List<ArqueoCaja> ListarArqueoCaja(ArqueoCaja pArqueoCaja, Usuario pUsuario)
        {
            try
            {
                return BOArqueoCaja.ListarArqueoCaja(pArqueoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaService", "ListarArqueoCaja", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un ArqueoCaja Cierre
        /// </summary>
        /// <param name="pUsuario">identificador del Usuario</param>
        /// <returns>Reitegro consultada</returns>
        public ArqueoCaja ConsultarCajero(Usuario pUsuario)
        {
            try
            {
                return BOArqueoCaja.ConsultarCajero(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaService", "ConsultarCajero", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un ArqueoCaja Cierre
        /// </summary>
        /// <param name="pUsuario">identificador del Usuario</param>
        /// <returns>Reitegro consultada</returns>
        public ArqueoCaja ConsultarUltFechaArqueoCaja(ArqueoCaja pArqueo, Usuario pUsuario)
        {
            try
            {
                return BOArqueoCaja.ConsultarUltFechaArqueoCaja(pArqueo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaService", "ConsultarUltFechaArqueoCaja", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un ArqueoCaja Cierre
        /// </summary>
        /// <param name="pUsuario">identificador del Usuario</param>
        /// <returns>Reitegro consultada</returns>
        public ArqueoCaja ConsultarUltFechaArqueoTesoreria(ArqueoCaja pArqueo, Usuario pUsuario)
        {
            try
            {
                return BOArqueoCaja.ConsultarUltFechaArqueoTesoreria(pArqueo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaService", "ConsultarUltFechaArqueoTesoreria", ex);
                return null;
            }
        }
        public Boolean ValidarArqueo(ArqueoCaja pArqueoCaja, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BOArqueoCaja.ValidarArqueo(pArqueoCaja, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Obtiene un parametro de traslados
        /// </summary>
        /// <param name="pUsuario">identificador del Usuario</param>
        /// <returns>Reitegro consultada</returns>
        public ArqueoCaja Consultarparametrotraslados(Usuario pUsuario)
        {
            try
            {
                return BOArqueoCaja.Consultarparametrotraslados(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaService", "Consultarparametrotraslados", ex);
                return null;
            }
        }


        public List<MovimientoCaja> consultararqueolista(MovimientoCaja pMovimientoCaja, Usuario pUsuario, string filtro)
        {
            try
            {

                return BOArqueoCaja.consultararqueolista(pMovimientoCaja, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarSaldosTesoreria", ex);
                return null;
            }
        }


        public ArqueoCaja ModificarArqueo(ArqueoCaja pArqueoCaja, GridView saldos, GridView cheques, Usuario pUsuario)
        {
            try
            {
                return BOArqueoCaja.ModificarArqueo(pArqueoCaja, saldos, cheques, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaService", "ModificarArqueo", ex);
                return null;
            }
        }
    }
}