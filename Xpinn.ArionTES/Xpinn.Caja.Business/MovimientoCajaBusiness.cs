using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para MovimientoCaja
    /// </summary>
    public class MovimientoCajaBusiness : GlobalData
    {
        private MovimientoCajaData DAMovimientoCaja;

        /// <summary>
        /// Constructor del objeto de negocio para MovimientoCaja
        /// </summary>
        public MovimientoCajaBusiness()
        {
            DAMovimientoCaja = new MovimientoCajaData();
        }

        /// <summary>
        /// Crea un MovimientoCaja
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad MovimientoCaja</param>
        /// <returns>Entidad MovimientoCaja creada</returns>
        public MovimientoCaja CrearTempArqueoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMovimientoCaja = DAMovimientoCaja.CrearTempArqueoCaja(pMovimientoCaja, pUsuario);

                    ts.Complete();
                }

                return pMovimientoCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "CrearTempArqueoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea un MovimientoTesoreria
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad MovimientoCaja</param>
        /// <returns>Entidad MovimientoCaja creada</returns>
        public MovimientoCaja CrearTempArqueoTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMovimientoCaja = DAMovimientoCaja.CrearTempArqueoTesoreria(pMovimientoCaja, pUsuario);

                    ts.Complete();
                }

                return pMovimientoCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "CrearTempArqueoTesoreria", ex);
                return null;
            }
        }


        /// <summary>
        /// Modifica un MovimientoCaja
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad MovimientoCaja</param>
        /// <returns>Entidad MovimientoCaja modificada</returns>
        public MovimientoCaja ModificarMovimientoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMovimientoCaja = DAMovimientoCaja.ModificarMovimientoCaja(pMovimientoCaja, pUsuario);

                    ts.Complete();
                }

                return pMovimientoCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ModificarMovimientoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un MovimientoCaja
        /// </summary>
        /// <param name="pId">Identificador de MovimientoCaja</param>
        public void EliminarTempArqueoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAMovimientoCaja.EliminarTempArqueoCaja(pMovimientoCaja, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "EliminarTempArqueoCaja", ex);
            }
        }

        /// <summary>
        /// Elimina un MovimientoCaja
        /// </summary>
        /// <param name="pId">Identificador de MovimientoCaja</param>
        public void EliminarTempArqueoTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAMovimientoCaja.EliminarTempArqueoTesoreria(pMovimientoCaja, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "EliminarTempArqueoTesoreria", ex);
            }
        }



        /// <summary>
        /// Obtiene un MovimientoCaja
        /// </summary>
        /// <param name="pId">Identificador de MovimientoCaja</param>
        /// <returns>Entidad MovimientoCaja</returns>
        public MovimientoCaja ConsultarMovimientoCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAMovimientoCaja.ConsultarMovimientoCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ConsultarMovimientoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarMovimientoCaja(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return DAMovimientoCaja.ListarMovimientoCaja(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarMovimientoCaja", ex);
                return null;
            }
        }





        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarMovimientoTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return DAMovimientoCaja.ListarMovimientoTesoreria(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarMovimientoTesoreria", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarCheques(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return DAMovimientoCaja.ListarCheques(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarCheques", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarChequesPendientes(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return DAMovimientoCaja.ListarChequesPendientes(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarChequesPendientes", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarChequesAsignados(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return DAMovimientoCaja.ListarChequesAsignados(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarChequesAsignados", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarSaldos(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {

                return DAMovimientoCaja.ListarSaldos(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarSaldos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarSaldosTesoreria(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {

                return DAMovimientoCaja.ListarSaldosTesoreria(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarSaldosTesoreria", ex);
                return null;
            }
        }
        public List<MovimientoCaja> Listararqueo(Int64 cod_cajero, DateTime fecha, Usuario pUsuario)
        {
            try
            {

                return DAMovimientoCaja.Listararqueo(cod_cajero, fecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarSaldos", ex);
                return null;
            }
        }
        public List<MovimientoCaja> Listararqueodetalle(Int64 cod_cajero, DateTime fecha, Usuario pUsuario)
        {
            try
            {

                return DAMovimientoCaja.Listararqueodetalle(cod_cajero, fecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarSaldos", ex);
                return null;
            }
        }

        public List<MovimientoCaja> Listarcomprobante(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {

                return DAMovimientoCaja.Listarcomprobante(pMovimientoCaja, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarSaldos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<MovimientoCaja> ListarArqueoTemporal(MovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            try
            {
                return DAMovimientoCaja.ListarArqueoTemporal(pMovimientoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarArqueoTemporal", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<Xpinn.Caja.Entities.MovimientoCaja> ListarChequesCanje(string[] pdata, Usuario pUsuario)
        {
            try
            {
                return DAMovimientoCaja.ListarChequesCanje(pdata, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarChequesCanje", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de MovimientoCaja obtenidos</returns>
        public List<Xpinn.Caja.Entities.MovimientoCaja> ListarChequesNoconsignados(string[] pdata, Usuario pUsuario)
        {
            try
            {
                return DAMovimientoCaja.ListarChequesNoconsignados(pdata, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarChequesNoconsignados", ex);
                return null;
            }
        }

        public MovimientoCaja ConsultarBoucher(MovimientoCaja pMovimiento, Usuario pUsuario)
        {
            try
            {
                return DAMovimientoCaja.ConsultarBoucher(pMovimiento, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ConsultarBoucher", ex);
                return null;
            }
        }

        public List<MovimientoCaja> ListarChequesRecibidos(Int64 Poperacion, Usuario pUsuario)
        {
            try
            {
                return DAMovimientoCaja.ListarChequesRecibidos(Poperacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCajaBusiness", "ListarChequesRecibidos", ex);
                return null;
            }
        }


    }
}