using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MovimientoCajaService
    {
        private MovimientoCajaBusiness BOMovimientoCaja;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para MovimientoCaja
        /// </summary>
        public MovimientoCajaService()
        {
            BOMovimientoCaja = new MovimientoCajaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "120105"; } }//Cierre Caja
        public string CodigoPrograma2 { get { return "120108"; } } // Movimientos Caja
        public string CodigoPrograma3 { get { return "120109"; } } // Cheques Caja
        public string CodigoPrograma4 { get { return "120107"; } } // Cheques Caja
        public string CodigoPrograma5 { get { return "120112"; } } // Comprobantes de Movimientos de Caja
        public string CodigoPrograma6 { get { return "120114"; } } // Canje de cheque
        /// <summary>
        /// Servicio para crear MovimientoCaja
        /// </summary>
        /// <param name="pEntity">Entidad MovimientoCaja</param>
        /// <returns>Entidad MovimientoCaja creada</returns>
        public MovimientoCaja CrearTempArqueoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.CrearTempArqueoCaja(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "CrearTempArqueoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para crear arquoe tesoreroia
        /// </summary>
        /// <param name="pEntity">Entidad MovimientoCaja</param>
        /// <returns>Entidad MovimientoCaja creada</returns>
        public MovimientoCaja CrearTempArqueoTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.CrearTempArqueoTesoreria(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "CrearTempArqueoTesoreria", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar MovimientoCaja
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad MovimientoCaja</param>
        /// <returns>Entidad MovimientoCaja modificada</returns>
        public MovimientoCaja ModificarMovimientoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ModificarMovimientoCaja(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ModificarMovimientoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar MovimientoCaja
        /// </summary>
        /// <param name="pId">identificador de MovimientoCaja</param>
        public void EliminarTempArqueoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                BOMovimientoCaja.EliminarTempArqueoCaja(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "EliminarMovimientoCaja", ex);
            }
        }


        /// <summary>
        /// Servicio para Eliminar MovimientoCaja
        /// </summary>
        /// <param name="pId">identificador de MovimientoCaja</param>
        public void EliminarTempArqueoTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                BOMovimientoCaja.EliminarTempArqueoTesoreria(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "EliminarTempArqueoTesoreria", ex);
            }
        }


        /// <summary>
        /// Servicio para obtener MovimientoCaja
        /// </summary>
        /// <param name="pId">identificador de MovimientoCaja</param>
        /// <returns>Entidad MovimientoCaja</returns>
        public MovimientoCaja ConsultarMovimientoCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ConsultarMovimientoCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ConsultarMovimientoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MovimientoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarMovimientoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ListarMovimientoCaja(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarMovimientoCaja", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de MovimientoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarMovimientoTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ListarMovimientoTesoreria(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarMovimientoTesoreria", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MovimientoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarCheques(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ListarCheques(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarCheques", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MovimientoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarChequesPendientes(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ListarChequesPendientes(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarChequesPendientes", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MovimientoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarChequesAsignados(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ListarChequesAsignados(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarChequesAsignados", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MovimientoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarSaldos(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ListarSaldos(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarSaldos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MovimientoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarSaldosTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ListarSaldosTesoreria(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarSaldosTesoreria", ex);
                return null;
            }
        }
        public List<MovimientoCaja> Listararqueo(Int64 cod_cajero, DateTime fecha, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.Listararqueo(cod_cajero, fecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarSaldos", ex);
                return null;
            }
        }
        public List<MovimientoCaja> Listararqueodetalle(Int64 cod_cajero, DateTime fecha, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.Listararqueodetalle(cod_cajero, fecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarSaldos", ex);
                return null;
            }
        }
        public List<MovimientoCaja> Listarcomprobante(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.Listarcomprobante(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarSaldos", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MovimientoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarArqueoTemporal(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ListarArqueoTemporal(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarArqueoTemporal", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MovimientoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<Xpinn.Caja.Entities.MovimientoCaja> ListarChequesCanje(string[] pdata, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ListarChequesCanje(pdata, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarChequesCanje", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de MovimientoCajas a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<Xpinn.Caja.Entities.MovimientoCaja> ListarChequesNoconsignados(string[] pdata, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ListarChequesNoconsignados(pdata, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarChequesNoconsignados", ex);
                return null;
            }
        }


        public MovimientoCaja ConsultarBoucher(MovimientoCaja pMovimiento, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ConsultarBoucher(pMovimiento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ConsultarBoucher", ex);
                return null;
            }
        }


        public List<Xpinn.Caja.Entities.MovimientoCaja> ListarChequesRecibidos(Int64 Poperacion, Usuario pUsuario)
        {
            try
            {
                return BOMovimientoCaja.ListarChequesRecibidos(Poperacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaService", "ListarChequesRecibidos", ex);
                return null;
            }
        }

    }
}