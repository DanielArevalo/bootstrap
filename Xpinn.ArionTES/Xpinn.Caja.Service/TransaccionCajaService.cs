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
    public class TransaccionCajaService
    {
        private TransaccionCajaBusiness BOTransaccionCaja;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para TransaccionCaja
        /// </summary>
        public TransaccionCajaService()
        {
            BOTransaccionCaja = new TransaccionCajaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "120107"; } }
        public string CodigoPrograma2 { get { return "120110"; } }
        public string CodigoPrograma3 { get { return "120111"; } }
        public string CodigoProgramaGirMoneda { get { return "120113"; } }

        public string CodigoProgramaRepReversiones { get { return "120116"; } }

        /// <summary>
        /// Servicio para crear TransaccionCaja
        /// </summary>
        /// <param name="pEntity">Entidad TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja creada</returns>
        public TransaccionCaja CrearTransaccionCajaReversion(TransaccionCaja pTransaccionCaja, GridView gvOperacion, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.CrearTransaccionCajaReversion(pTransaccionCaja, gvOperacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "CrearTransaccionCaja", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para crear TransaccionCaja
        /// </summary>
        /// <param name="pEntity">Entidad TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja creada</returns>
        public TransaccionCaja CrearTransaccionCajaOperacion(TransaccionCaja pTransaccionCaja, GridView gvTransacciones, GridView gvFormaPago, GridView gvCheques, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BOTransaccionCaja.CrearTransaccionCajaOperacion(pTransaccionCaja, gvTransacciones, gvFormaPago, gvCheques, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                //BOExcepcion.Throw("TransaccionCajaService", "CrearTransaccionCajaOperacion", ex);
                Error = Error + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar TransaccionCaja
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja modificada</returns>
        public TransaccionCaja ModificarTransaccionCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ModificarTransaccionCaja(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ModificarTransaccionCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar TransaccionCaja
        /// </summary>
        /// <param name="pId">identificador de TransaccionCaja</param>
        public void EliminarTransaccionCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOTransaccionCaja.EliminarTransaccionCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarTransaccionCaja", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener TransaccionCaja
        /// </summary>
        /// <param name="pId">identificador de TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja</returns>
        public TransaccionCaja ConsultarTransaccionCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ConsultarTransaccionCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ConsultarTransaccionCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener TransaccionCaja
        /// </summary>
        /// <param name="pId">identificador de TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja</returns>
        public TransaccionCaja ConsultarParametroCastigos(Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ConsultarParametroCastigos( pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ConsultarTransaccionCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TransaccionCajas a partir de unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransaccionCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarTransaccionCaja(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarTransaccionCaja", ex);
                return null;
            }
        }

        public List<TransaccionCaja> ListarTransaccionesComprobanteTot(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarTransaccionesComprobanteTot(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarTransaccionesComprobanteTot", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de TransaccionCajas a partir de unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransacciones(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarTransacciones(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarTransacciones", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de TransaccionCajas a partir de unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransaccionesComprobante(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarTransaccionesComprobante(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarTransaccionesComprobante", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de TransaccionCajas a partir de unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarMovimientosCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarMovimientosCaja(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarMovimientosCaja", ex);
                return null;
            }
        }

        public List<TransaccionCaja> ListarTrasladosCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarTrasladosCaja(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarTrasladosCaja", ex);
                return null;
            }
        }


        public List<TransaccionCaja> ListarSumaMovimientosCaja(TransaccionCaja pTransaccionCaja, DateTime pFechaInicial, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarSumaMovimientosCaja(pTransaccionCaja, pFechaInicial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarSumaMovimientosCaja", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de TransaccionCajas a partir de unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTodosMovimientosCaja(TransaccionCaja pTransaccionCaja, DateTime pFechaInicial,  Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarTodosMovimientosCaja(pTransaccionCaja, pFechaInicial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarTodosMovimientosCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de TransaccionCajas a partir de unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransaccionesPendientes(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarTransaccionesPendientes(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarTransaccionesPendientes", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para obtener lista de TransaccionCajas a partir de unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarOperaciones(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarOperaciones(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarOperaciones", ex);
                return null;
            }
        }


        #region GIRO_MONEDA

        public void CrearTransaccionGiroMoneda(TransaccionCaja pTransaccionCaja, List<GiroMoneda> lstGiros, GridView gvFormaPago, GridView gvCheques, Usuario pUsuario, ref string Error)
        {
            try
            {
                BOTransaccionCaja.CrearTransaccionGiroMoneda(pTransaccionCaja, lstGiros, gvFormaPago, gvCheques, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "CrearTransaccionGiroMoneda", ex);
            }
        }

        public GiroMoneda ConsultarGiroMoneda(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOTransaccionCaja.ConsultarGiroMoneda(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ConsultarGiroMoneda", ex);
                return null;
            }
        }

        public List<GiroMoneda> ListarGiroMoneda(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarGiroMoneda(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarGiroMoneda", ex);
                return null;
            }
        }

        #endregion

        /// <summary>
        /// Servicio para obtener lista de TransaccionCajas a partir de unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public Boolean ValidarControlOperacion(Int64 pcod_persona,ref Decimal pvalortransaccion , DateTime pFecha, Usuario pUsuario,Decimal MontoDiario,Decimal MontoMensual)
        {
            try
            {
                return BOTransaccionCaja.ValidarControlOperacion(pcod_persona,ref pvalortransaccion, pFecha, pUsuario,MontoDiario,MontoMensual);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ValidarControlOperacion", ex);
                return false;
            }
        }


        public List<TransaccionCaja> ListarOperacionesAnuladas(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarOperacionesAnuladas(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarOperacionesAnuladas", ex);
                return null;
            }
        }

        public List<TransaccionCaja> ListarOperacionesAnuladasSincajero(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ListarOperacionesAnuladasSincajero(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ListarOperacionesAnuladasSincajero", ex);
                return null;
            }
        }


        public TransaccionCaja ConsultarOperacionesAnuladas(Int64 cod_ope, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.ConsultarOperacionesAnuladas(cod_ope, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "ConsultarOperacionesAnuladas", ex);
                return null;
            }
        }

        public Int64 CajeroResponsableOficina(Int64 cod_oficina, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.CajeroResponsableOficina(cod_oficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "CajeroResponsableOficina", ex);
                return 0;
            }
        }

        public Int64 UsuarioResponsableOficina(Int64 cod_oficina, Usuario pUsuario)
        {
            try
            {
                return BOTransaccionCaja.UsuarioResponsableOficina(cod_oficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaService", "UsuarioResponsableOficina", ex);
                return 0;
            }
        }

    }
}