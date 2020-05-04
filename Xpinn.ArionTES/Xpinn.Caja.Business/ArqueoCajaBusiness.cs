using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para ArqueoCaja
    /// </summary>
    public class ArqueoCajaBusiness : GlobalData
    {
        private ArqueoCajaData DAArqueoCaja;

        /// <summary>
        /// Constructor del objeto de negocio para ArqueoCaja
        /// </summary>
        public ArqueoCajaBusiness()
        {
            DAArqueoCaja = new ArqueoCajaData();
        }

        /// <summary>
        /// Crea un ArqueoCaja
        /// </summary>
        /// <param name="pArqueoCaja">Entidad ArqueoCaja</param>
        /// <returns>Entidad ArqueoCaja creada</returns>
        public ArqueoCaja CrearArqueoCaja(ArqueoCaja pArqueoCaja, GridView saldos, GridView cheques, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pArqueoCaja = DAArqueoCaja.CrearArqueoCaja(pArqueoCaja, saldos, cheques, pUsuario);

                    ts.Complete();
                }

                return pArqueoCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "CrearArqueoCaja", ex);
                return null;
            }
        }

        public ArqueoCaja ArqueoCajadetalle(ArqueoCaja pArqueoCaja, GridView saldos, GridView cheques, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    int codMoneda = 0;
                    decimal efectivo = 0;
                    decimal cheque = 0;
                    string concepto;
                    decimal total = 0;

                    foreach (GridViewRow fila in saldos.Rows)
                    {

                        codMoneda = int.Parse(fila.Cells[1].Text);
                        concepto = (fila.Cells[4].Text);
                        efectivo = decimal.Parse(fila.Cells[5].Text);
                        cheque = decimal.Parse(fila.Cells[6].Text);
                        total = decimal.Parse(fila.Cells[7].Text);

                        pArqueoCaja = DAArqueoCaja.ArqueoCajadetalle(pArqueoCaja, Convert.ToInt64(codMoneda), concepto, Convert.ToDouble(efectivo), Convert.ToDouble(total), Convert.ToDouble(cheque), pUsuario);


                    }
                    ts.Complete();
                }

                return pArqueoCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "CrearArqueoCaja", ex);
                return null;
            }
        }



        public ArqueoCaja ArqueosGuardarEnDetalle(ArqueoCaja pArqueoCaja, GridView saldos, GridView cheques, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    int codMoneda = 0;
                    decimal efectivo = 0;
                    decimal cheque = 0;
                    string concepto;
                    decimal total = 0;

                    foreach (GridViewRow fila in saldos.Rows)
                    {

                        codMoneda = int.Parse(fila.Cells[1].Text);
                        concepto = (fila.Cells[3].Text);
                        efectivo = decimal.Parse(fila.Cells[4].Text);
                        cheque = decimal.Parse(fila.Cells[5].Text);
                        total = decimal.Parse(fila.Cells[8].Text);

                        pArqueoCaja = DAArqueoCaja.ArqueoCajadetalle(pArqueoCaja, Convert.ToInt64(codMoneda), concepto, Convert.ToDouble(efectivo), Convert.ToDouble(total), Convert.ToDouble(cheque), pUsuario);


                    }
                    ts.Complete();
                }

                return pArqueoCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "ArqueosGuardarEnDetalle", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ArqueoCaja
        /// </summary>
        /// <param name="pArqueoCaja">Entidad ArqueoCaja</param>
        /// <returns>Entidad ArqueoCaja modificada</returns>
        public ArqueoCaja ModificarArqueoCaja(ArqueoCaja pArqueoCaja, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pArqueoCaja = DAArqueoCaja.ModificarArqueoCaja(pArqueoCaja, pUsuario);

                    ts.Complete();
                }

                return pArqueoCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "ModificarArqueoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ArqueoCaja
        /// </summary>
        /// <param name="pId">Identificador de ArqueoCaja</param>
        public void EliminarArqueoCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAArqueoCaja.EliminarArqueoCaja(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "EliminarArqueoCaja", ex);
            }
        }



        public void EliminarArqueo(DateTime pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAArqueoCaja.EliminarArqueo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch 
            {
            }
        }
        /// <summary>
        /// Obtiene un ArqueoCaja
        /// </summary>
        /// <param name="pId">Identificador de ArqueoCaja</param>
        /// <returns>Entidad ArqueoCaja</returns>
        public ArqueoCaja ConsultarArqueoCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAArqueoCaja.ConsultarArqueoCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "ConsultarArqueoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pArqueoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ArqueoCaja obtenidos</returns>
        public List<ArqueoCaja> ListarArqueoCaja(ArqueoCaja pArqueoCaja, Usuario pUsuario)
        {
            try
            {
                return DAArqueoCaja.ListarArqueoCaja(pArqueoCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "ListarArqueoCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Traslado
        /// </summary>
        /// <param name="pId">identificador del Traslado</param>
        /// <returns>Traslado consultada</returns>
        public ArqueoCaja ConsultarCajero(Usuario pUsuario)
        {
            try
            {
                return DAArqueoCaja.ConsultarCajero(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "ConsultarCajero", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Traslado
        /// </summary>
        /// <param name="pId">identificador del Traslado</param>
        /// <returns>Traslado consultada</returns>
        public ArqueoCaja ConsultarUltFechaArqueoCaja(ArqueoCaja pArqueo, Usuario pUsuario)
        {
            try
            {
                return DAArqueoCaja.ConsultarUltFechaArqueoCaja(pArqueo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "ConsultarUltFechaArqueoCaja", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene una Traslado
        /// </summary>
        /// <param name="pId">identificador del Traslado</param>
        /// <returns>Traslado consultada</returns>
        public ArqueoCaja ConsultarUltFechaArqueoTesoreria(ArqueoCaja pArqueo, Usuario pUsuario)
        {
            try
            {
                return DAArqueoCaja.ConsultarUltFechaArqueoTesoreria(pArqueo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "ConsultarUltFechaArqueoTesoreria", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene un parametro de traslados
        /// </summary>
        /// <param name="pId">identificador del Traslado</param>
        /// <returns>Traslado consultada</returns>
        public ArqueoCaja Consultarparametrotraslados(Usuario pUsuario)
        {
            try
            {
                return DAArqueoCaja.Consultarparametrotraslados(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "Consultarparametrotraslados", ex);
                return null;
            }
        }

        public Boolean ValidarArqueo(ArqueoCaja pArqueoCaja, Usuario pUsuario, ref string Error)
        {
            try
            {
                return DAArqueoCaja.ValidarArqueo(pArqueoCaja, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return false;
            }
        }

        public List<MovimientoCaja> consultararqueolista(MovimientoCaja pMovimientoCaja, Usuario pUsuario, string filtro)
        {
            try
            {

                return DAArqueoCaja.consultararqueolista(pMovimientoCaja, pUsuario, filtro);
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
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    int codMoneda = 0;
                    decimal efectivo = 0;
                    decimal cheque = 0;
                    string concepto;
                    decimal total = 0;

                    foreach (GridViewRow fila in saldos.Rows)
                    {

                        codMoneda = int.Parse(fila.Cells[1].Text);
                        concepto = (fila.Cells[3].Text);
                        efectivo = decimal.Parse(fila.Cells[4].Text);
                        cheque = decimal.Parse(fila.Cells[5].Text);
                        total = decimal.Parse(fila.Cells[8].Text);

                        pArqueoCaja = DAArqueoCaja.ModificarArqueo(pArqueoCaja, Convert.ToInt64(codMoneda), concepto, Convert.ToDouble(efectivo), Convert.ToDouble(total), Convert.ToDouble(cheque), pUsuario);


                    }
                    ts.Complete();
                }

                return pArqueoCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArqueoCajaBusiness", "ModificarArqueo", ex);
                return null;
            }
        }

    }
}