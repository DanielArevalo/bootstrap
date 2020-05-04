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
    /// Objeto de negocio para TransaccionCaja
    /// </summary>
    public class TransaccionCajaBusiness : GlobalBusiness
    {
        private TransaccionCajaData DATransaccionCaja;

        /// <summary>
        /// Constructor del objeto de negocio para TransaccionCaja
        /// </summary>
        public TransaccionCajaBusiness()
        {
            DATransaccionCaja = new TransaccionCajaData();
        }

        /// <summary>
        /// Crea un TransaccionCaja
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja creada</returns>
        public TransaccionCaja CrearTransaccionCajaReversion(TransaccionCaja pTransaccionCaja, GridView gvOperaciones, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTransaccionCaja = DATransaccionCaja.CrearTransaccionCajaReversion(pTransaccionCaja, gvOperaciones, pUsuario);

                    ts.Complete();
                }

                return pTransaccionCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "CrearTransaccionCajaReversion", ex);
                return null;
            }
        }


        /// <summary>
        /// Crea un TransaccionCaja
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja creada</returns>
        public TransaccionCaja CrearTransaccionCajaOperacion(TransaccionCaja pTransaccionCaja, GridView gvTransacciones, GridView gvFormaPago, GridView gvCheques, Usuario pUsuario, ref string Error)
        {
            Xpinn.Tesoreria.Data.PagosVentanillaData DAPagosVentanilla = new Tesoreria.Data.PagosVentanillaData();

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Validar Fecha Transaciones                    
                    string NumError = "";
                    string ErrorTitular = "";
                    string NumProducto;
                    int TipoProduc;
                    string FechaAperProd = "";
                    string TipoPagoPro;
                    int producto;
                    List<string> ListCuentasXcobrar = new List<string>();

                    //Validamos fecha Apertura de los productos que entran en la transacción
                    foreach (GridViewRow fila in gvTransacciones.Rows)
                    {
                        TipoProduc = Convert.ToInt32(fila.Cells[4].Text);
                        NumProducto = Convert.ToString(fila.Cells[8].Text);
                        TipoPagoPro = Convert.ToString(fila.Cells[12].Text);

                        if (TipoProduc == 3)
                        {
                            if (TipoPagoPro == "203" || TipoPagoPro == "255")
                            {
                                ListCuentasXcobrar.Add(NumProducto);
                            }
                        }

                        FechaAperProd = DAPagosVentanilla.FechasApertura(NumProducto, TipoProduc, pUsuario);
                        if (FechaAperProd != "" && FechaAperProd != null)
                        {
                            DateTime fecha_cierre = new DateTime(pTransaccionCaja.fecha_cierre.Year, pTransaccionCaja.fecha_cierre.Month, pTransaccionCaja.fecha_cierre.Day, 0, 0, 0);
                            DateTime fecha_aperprod = new DateTime(Convert.ToDateTime(FechaAperProd).Year, Convert.ToDateTime(FechaAperProd).Month, Convert.ToDateTime(FechaAperProd).Day, 0, 0, 0);
                            if (fecha_cierre < fecha_aperprod)
                            {
                                if (NumError != "")
                                    NumError += ", " + NumProducto;
                                else
                                    NumError += NumProducto;
                            }
                        }

                        //validamos el producto con su asociado
                        producto = DAPagosVentanilla.TitularProducto(pTransaccionCaja.cod_persona, NumProducto, TipoProduc, pUsuario);
                        if (TipoProduc == 6 || TipoProduc == 7 || TipoProduc == 8 || TipoProduc == 10) // 6:Afiliación, 7: Otros, 8:devoluciones, 10:Giros
                        {
                            producto = 1;
                        }

                        if (producto == 0)
                        {
                            if (ErrorTitular != "")
                                ErrorTitular += ", " + NumProducto;
                            else
                                ErrorTitular += NumProducto;
                        }

                    }

                    if (NumError != "")
                    {
                        Error = "La fecha de la operación es menor a la de apertura de los productos: " + NumError;
                        return null;
                    }

                    if (ErrorTitular != "")
                    {
                        Error = "Los productos: " + ErrorTitular + ", no pertenecen a la persona que se le registra la operación";
                        return null;
                    }

                    pTransaccionCaja = DATransaccionCaja.CrearTransaccionCajaOperacion(pTransaccionCaja, gvTransacciones, gvFormaPago, gvCheques, pUsuario, ref Error);

                    //Para Aplicar pago a las cuentas por cobrar
                    if (ListCuentasXcobrar.Count > 0)
                    {
                        foreach (string item in ListCuentasXcobrar)
                        {
                            Xpinn.Ahorros.Data.AhorroVistaData DAAhorroVistaData = new Ahorros.Data.AhorroVistaData();
                            List<Xpinn.Ahorros.Entities.CuentasCobrar> lstCuentasCobrar = new List<Ahorros.Entities.CuentasCobrar>();
                            lstCuentasCobrar = DAAhorroVistaData.ListarCuentasCobrar(item, pUsuario);

                            if (lstCuentasCobrar.Count > 0)
                            {
                                foreach (Xpinn.Ahorros.Entities.CuentasCobrar CuetaxCobrar in lstCuentasCobrar)
                                {
                                    CuetaxCobrar.tipo_tran = 257;
                                    DAAhorroVistaData.ProcesoCuentasCobrar(pTransaccionCaja, CuetaxCobrar, pUsuario);
                                }
                            }
                        }
                    }
                    if (pTransaccionCaja != null)
                    {
                        ts.Complete();

                    }
                }

                return pTransaccionCaja;
            }
            catch (Exception ex)
            {
                //BOExcepcion.Throw("TransaccionCajaBusiness", "CrearTransaccionCajaOperacion", ex);
                Error = Error + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Modifica un TransaccionCaja
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja modificada</returns>
        public TransaccionCaja ModificarTransaccionCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTransaccionCaja = DATransaccionCaja.ModificarTransaccionCaja(pTransaccionCaja, pUsuario);

                    ts.Complete();
                }

                return pTransaccionCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ModificarTransaccionCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un TransaccionCaja
        /// </summary>
        /// <param name="pId">Identificador de TransaccionCaja</param>
        public void EliminarTransaccionCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DATransaccionCaja.EliminarTransaccionCaja(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "EliminarTransaccionCaja", ex);
            }
        }

        /// <summary>
        /// Obtiene un TransaccionCaja
        /// </summary>
        /// <param name="pId">Identificador de TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja</returns>
        public TransaccionCaja ConsultarTransaccionCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ConsultarTransaccionCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ConsultarTransaccionCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un parametro de la tabla general
        /// </summary>
        /// <param name="pId">Identificador de parametro</param>
        /// <returns>Entidad parametros</returns>
        public TransaccionCaja ConsultarParametroCastigos(Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ConsultarParametroCastigos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ConsultarParametroCastigos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransaccionCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarTransaccionCaja(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarTransaccionCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene listado total de transacciones
        /// </summary>
        /// <param name="pTransaccionCaja"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<TransaccionCaja> ListarTransaccionesComprobanteTot(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarTransaccionesComprobanteTot(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarTransaccionesComprobanteTot", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransacciones(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarTransacciones(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarTransacciones", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransaccionesComprobante(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarTransaccionesComprobante(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarTransaccionesComprobante", ex);
                return null;
            }
        }


        // <summary>
        /// Obtiene la lista de movimientos de caja dados unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarMovimientosCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarMovimientosCaja(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarMovimientosCaja", ex);
                return null;
            }
        }

        public List<TransaccionCaja> ListarTrasladosCaja(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarTrasladosCaja(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarTrasladosCaja", ex);
                return null;
            }
        }

        // <summary>
        /// Obtiene la lista de movimientos de caja dados unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarSumaMovimientosCaja(TransaccionCaja pTransaccionCaja, DateTime pFechaInicial, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarSumaMovimientosCaja(pTransaccionCaja, pFechaInicial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarSumaMovimientosCaja", ex);
                return null;
            }
        }

        // <summary>
        /// Obtiene la lista de movimientos de caja dados unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTodosMovimientosCaja(TransaccionCaja pTransaccionCaja, DateTime pFechaInicial, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarTodosMovimientosCaja(pTransaccionCaja, pFechaInicial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarTodosMovimientosCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarTransaccionesPendientes(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarTransaccionesPendientes(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarTransaccionesPendientes", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TransaccionCaja obtenidos</returns>
        public List<TransaccionCaja> ListarOperaciones(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarOperaciones(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarOperaciones", ex);
                return null;
            }
        }


        #region GIRO_MONEDA


        public void CrearTransaccionGiroMoneda(TransaccionCaja pTransaccionCaja, List<GiroMoneda> lstGiros, GridView gvFormaPago, GridView gvCheques, Usuario pUsuario, ref string Error)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTransaccionCaja = DATransaccionCaja.CrearTransaccionGiroMoneda(pTransaccionCaja, lstGiros, gvFormaPago, gvCheques, pUsuario, ref Error);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "CrearTransaccionGiroMoneda", ex);
            }
        }

        public GiroMoneda ConsultarGiroMoneda(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DATransaccionCaja.ConsultarGiroMoneda(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ConsultarGiroMoneda", ex);
                return null;
            }
        }

        public List<GiroMoneda> ListarGiroMoneda(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarGiroMoneda(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarGiroMoneda", ex);
                return null;
            }
        }

        #endregion

        public Boolean ValidarControlOperacion(Int64 pCod_Persona, ref Decimal pvalortransaccion, DateTime pFecha, Usuario pUsuario, Decimal MontoDiario, Decimal MontoMensual)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Decimal Transacciones_Dia = DATransaccionCaja.ConsultarTotalTransaccionDia(pCod_Persona, pvalortransaccion, pFecha, pUsuario);
                    if (Transacciones_Dia > 0)
                    {
                        pvalortransaccion = Transacciones_Dia;
                        return true;
                    }
                    else if (pvalortransaccion >= MontoDiario)
                    {
                        return true;
                    }
                    Decimal Transacciones_Mes = DATransaccionCaja.ConsultarTotalTransaccionMes(pCod_Persona, pvalortransaccion, pFecha.Month, pFecha.Year, pUsuario);
                    if (Transacciones_Mes > 0)
                    {
                        pvalortransaccion = Transacciones_Mes;
                        return true;
                    }
                    else if (pvalortransaccion >= MontoMensual)
                    {
                        return true;
                    }
                    ts.Complete();
                }
                return false;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarOperaciones", ex);
                return false;
            }
        }



        public List<TransaccionCaja> ListarOperacionesAnuladas(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarOperacionesAnuladas(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarOperacionesAnuladas", ex);
                return null;
            }
        }
        public List<TransaccionCaja> ListarOperacionesAnuladasSincajero(TransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ListarOperacionesAnuladasSincajero(pTransaccionCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ListarOperacionesAnuladasSincajero", ex);
                return null;
            }
        }


        public TransaccionCaja ConsultarOperacionesAnuladas(Int64 cod_ope, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.ConsultarOperacionesAnuladas(cod_ope, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "ConsultarOperacionesAnuladas", ex);
                return null;
            }
        }

        public Int64 CajeroResponsableOficina(Int64 cod_oficina, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.CajeroResponsableOficina(cod_oficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "CajeroResponsableOficina", ex);
                return 0;
            }
        }

        public Int64 UsuarioResponsableOficina(Int64 cod_oficina, Usuario pUsuario)
        {
            try
            {
                return DATransaccionCaja.UsuarioResponsableOficina(cod_oficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionCajaBusiness", "UsuarioResponsableOficina", ex);
                return 0;
            }
        }


    }
}