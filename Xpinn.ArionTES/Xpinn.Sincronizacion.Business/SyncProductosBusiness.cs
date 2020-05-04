using System;
using System.Collections.Generic;
using System.Transactions;
using Xpinn.Sincronizacion.Data;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Business
{
    public class SyncProductosBusiness : GlobalBusiness
    {
        SyncProductosData DAProductos;
        SyncOperacionData DAOperacion;
        SyncTransaccionCajaData DATransac;
        public SyncProductosBusiness()
        {
            DAProductos = new SyncProductosData();
            DAOperacion = new SyncOperacionData();
            DATransac = new SyncTransaccionCajaData();
        }
        public List<Producto> ListarProductosPersona(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAProductos.ListarProductosPersona(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncProductosBusiness", "ListarProductosPersona", ex);
                return null;
            }
        }
        public List<ObjectString> ListarTiraProductosPersona(int codigo, string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAProductos.ListarTiraProductosPersona(codigo, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncProductosBusiness", "ListarTiraProductosPersona", ex);
                return null;
            }
        }

        public EntityGlobal SyncCantidadProductos(int codigo, Usuario vUsuario)
        {
            try
            {
                return DAProductos.SyncCantidadProductos(codigo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncProductosBusiness", "SyncCantidadProductos", ex);
                return null;
            }
        }

        public List<ObjectString> ListarTiraProductosPendientes(DateTime pFecGeneracion, string pTablaGen, Usuario vUsuario)
        {
            try
            {
                return DAProductos.ListarTiraProductosPendientes(pFecGeneracion, pTablaGen, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncProductosBusiness", "ListarTiraProductosPendientes", ex);
                return null;
            }
        }


        public EntityGlobal CrearSyncOperacion(SyncOperacion pOperacion, Usuario vUsuario)
        {
            EntityGlobal pResult = new EntityGlobal();
            SyncCajaData DACaja = new SyncCajaData();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    if (pOperacion != null)
                    {
                        //CREANDO OPERACION
                        string pError = string.Empty;
                        Int64 pCodOpeLocal = pOperacion.cod_ope;
                        SyncOperacion pOpeResult = DAOperacion.CrearSyncOperacion(pOperacion, ref pError, vUsuario);
                        if (!string.IsNullOrEmpty(pError))
                        {
                            pResult.Success = false;
                            pResult.Message = "Se generó un error al registrar la operación. " + pError;
                            return pResult;
                        }
                        Int64 pCodGenerado = pOpeResult.cod_ope_principal;

                        int TipoPago = 0;
                        string pTipoMov = string.Empty;

                        //INSERTANDO MOVIMIENTOS
                        string cod_cajero = string.Empty;
                        string[] camposObject = null;
                        if (!string.IsNullOrEmpty(pOperacion.strMovimientos))
                        {
                            SyncMovimientoCaja pMovCaja = null;
                            string[] lstMovimientos = pOperacion.strMovimientos.Contains(";") ? pOperacion.strMovimientos.Split(';') : new string[] { pOperacion.strMovimientos };

                            Int64 pCodMovLocal = 0;
                            foreach (string nMovimiento in lstMovimientos)
                            {
                                camposObject = nMovimiento.Split('|');
                                pMovCaja = new SyncMovimientoCaja();
                                pCodMovLocal = Convert.ToInt64(camposObject[0]);

                                pTipoMov = camposObject[5];
                                pTipoMov = pTipoMov == "E" ? "1" : "2";
                                TipoPago = Convert.ToInt32(camposObject[6]);
                                if (TipoPago == 1)
                                {
                                    // GENERANDO MOVIMIENDO EN SALDO DE CAJA
                                    SyncSaldoCaja pSaldoMod = new SyncSaldoCaja();
                                    pSaldoMod.fecha = DateTime.ParseExact(camposObject[2], "dd/MM/yyyy", null);
                                    pSaldoMod.cod_caja = Convert.ToInt64(camposObject[3]);
                                    pSaldoMod.cod_cajero = Convert.ToInt64(camposObject[4]);
                                    pSaldoMod.cod_moneda = Convert.ToInt32(camposObject[10]);
                                    pSaldoMod.valor = Convert.ToDecimal(camposObject[11]);
                                    DACaja.ModificarSaldoCaja(pSaldoMod, Convert.ToInt32(pTipoMov), vUsuario);
                                }

                                pMovCaja.cod_ope = pCodGenerado;
                                pMovCaja.fec_ope = DateTime.ParseExact(camposObject[2], "dd/MM/yyyy", null);
                                pMovCaja.cod_caja = Convert.ToInt64(camposObject[3]);
                                pMovCaja.cod_cajero = Convert.ToInt64(camposObject[4]);
                                pMovCaja.tipo_mov = pTipoMov;
                                pMovCaja.cod_tipo_pago = TipoPago;
                                if (string.IsNullOrEmpty(camposObject[7]))
                                    pMovCaja.cod_banco = null;
                                else
                                    pMovCaja.cod_banco = Convert.ToInt64(camposObject[7]);
                                pMovCaja.num_documento = string.IsNullOrEmpty(camposObject[8]) ? null : camposObject[8];
                                pMovCaja.cod_moneda = Convert.ToInt32(camposObject[10]);
                                pMovCaja.valor = Convert.ToDecimal(camposObject[11]);
                                //pMovCaja.estado = 0;
                                pMovCaja.cod_persona = Convert.ToInt64(camposObject[13]);
                                if (string.IsNullOrEmpty(camposObject[14]))
                                    pMovCaja.idctabancaria = null;
                                else
                                    pMovCaja.idctabancaria = Convert.ToInt32(camposObject[14]);

                                cod_cajero = camposObject[4];
                                pMovCaja = DATransac.CrearMovimientoCaja(pMovCaja, vUsuario);

                                // CREANDO HOMOLOGACION DETALLE
                                if (pMovCaja.cod_movimiento != 0)
                                {
                                    SyncHomologaOperacionDeta pDeta = new SyncHomologaOperacionDeta();
                                    pDeta.cod_ope_principal = pCodGenerado;
                                    pDeta.cod_ope_local = pCodOpeLocal;
                                    pDeta.tabla_detalle = "MOVIMIENTOCAJA";
                                    pDeta.campo_tabla = "COD_MOVIMIENTO";
                                    pDeta.codigo_principal = pMovCaja.cod_movimiento.ToString();
                                    pDeta.codigo_local = pCodMovLocal.ToString();

                                    DAOperacion.CrearHomologacionOperacionDetalle(pDeta, vUsuario);
                                }
                            }
                        }

                        //INSERTANDO TRANSACCIONES Y APLICANDO EL PAGO
                        if (!string.IsNullOrEmpty(pOperacion.strTransacciones))
                        {
                            SyncTransaccionCaja pTransaccion = null;
                            string[] lstTransac = pOperacion.strTransacciones.Contains(";") ? pOperacion.strTransacciones.Split(';') : new string[] { pOperacion.strTransacciones };
                            Int64 pNumTranLocal = 0;

                            foreach (string ntransac in lstTransac)
                            {
                                camposObject = ntransac.Split('|');
                                pTransaccion = new SyncTransaccionCaja();
                                pNumTranLocal = Convert.ToInt64(camposObject[0]);

                                cod_cajero = string.IsNullOrEmpty(cod_cajero) ? camposObject[3] : cod_cajero;
                                pTransaccion.cod_ope = pCodGenerado;
                                pTransaccion.cod_caja = Convert.ToInt64(camposObject[2]);
                                pTransaccion.cod_cajero = Convert.ToInt64(camposObject[3]);
                                pTransaccion.fecha_aplica = DateTime.ParseExact(camposObject[5], "dd/MM/yyyy", null);
                                pTransaccion.tipo_movimiento = camposObject[6];
                                pTransaccion.cod_persona = Convert.ToInt64(camposObject[7]);
                                pTransaccion.num_producto = string.IsNullOrEmpty(camposObject[8]) ? null : camposObject[8];
                                pTransaccion.tipo_pago = camposObject[9];
                                if (string.IsNullOrEmpty(camposObject[13]))
                                    pTransaccion.tipo_tran = null;
                                else
                                    pTransaccion.tipo_tran = Convert.ToInt64(camposObject[13]);
                                pTransaccion.cod_moneda = Convert.ToInt32(camposObject[10]);
                                pTransaccion.valor_pago = Convert.ToDecimal(camposObject[11]);
                                pTransaccion.cod_usuario = pOperacion.cod_usuario;

                                pTransaccion = DATransac.CrearTransaccionCaja(pTransaccion, vUsuario);

                                // CREANDO HOMOLOGACION DETALLE
                                if (pTransaccion.num_trancaja != 0)
                                {
                                    SyncHomologaOperacionDeta pDeta = new SyncHomologaOperacionDeta();
                                    pDeta.cod_ope_principal = pCodGenerado;
                                    pDeta.cod_ope_local = pCodOpeLocal;
                                    pDeta.tabla_detalle = "TRANSACCIONESCAJA";
                                    pDeta.campo_tabla = "NUM_TRANCAJA";
                                    pDeta.codigo_principal = pTransaccion.num_trancaja.ToString();
                                    pDeta.codigo_local = pNumTranLocal.ToString();

                                    DAOperacion.CrearHomologacionOperacionDetalle(pDeta, vUsuario);
                                }

                                // APLICANDO EL REGISTRO DE PAGO
                                DAOperacion.RegistrarPagoOperacion(pTransaccion, vUsuario);
                            }
                        }

                        // CONSULTANDO CAJERO
                        SyncCajeroData DACajero = new SyncCajeroData();
                        string pFiltro = " WHERE C.COD_CAJERO = " + cod_cajero;
                        SyncCajero pCajero = DACajero.ConsultarSyncCajero(pFiltro, vUsuario);

                        // CREANDO FACTUARA
                        Xpinn.Caja.Entities.TipoOperacion pTipoOpe = new Xpinn.Caja.Entities.TipoOperacion();
                        pTipoOpe.cod_operacion = pCodGenerado.ToString();
                        pTipoOpe.cod_persona = pCajero.cod_usuario.ToString();
                        DAOperacion.InsertarFactura(pTipoOpe, vUsuario);

                        //GENERANDO HOMOLOGACION
                        if (pCodGenerado > 0)
                        {
                            SyncHomologaOperacion pHomologa = new SyncHomologaOperacion();
                            pHomologa.fecha = DateTime.Now;
                            pHomologa.tabla = "OPERACION";
                            pHomologa.campo_tabla = "COD_OPE";
                            pHomologa.codigo_principal = pCodGenerado.ToString();
                            pHomologa.codigo_local = pCodOpeLocal.ToString();
                            pHomologa.proceso = ProcesosOffline.RegistroOperacion.ToString();
                            pHomologa = DAOperacion.CrearSyncHomologaOperacion(pHomologa, vUsuario);

                            pResult.Success = true;
                            pResult.CodigoGenerado = pCodGenerado.ToString();
                        }
                        else
                        {
                            pResult.Success = false;
                            pResult.Message = "Se generó un error al registrar la operación.";
                        }
                    }
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    pResult.Success = false;
                    pResult.Message = ex.Message;
                    ts.Dispose();
                }
            }
            return pResult;
        }


        public EntityGlobal CrearSyncConsignacion(SyncOperacion pOperacion, Usuario vUsuario)
        {
            SyncConsignacionData DAConsignacion = new SyncConsignacionData();
            SyncCajaData DACaja = new SyncCajaData();

            EntityGlobal pResult = new EntityGlobal();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    if (pOperacion != null)
                    {
                        if (!string.IsNullOrEmpty(pOperacion.strConsignacion))
                        {
                            // CREANDO OPERACION
                            string pError = string.Empty;
                            Int64 pCodOpeLocal = pOperacion.cod_ope;
                            SyncOperacion pOpeResult = DAOperacion.CrearSyncOperacion(pOperacion, ref pError, vUsuario);
                            if (!string.IsNullOrEmpty(pError))
                            {
                                pResult.Success = false;
                                pResult.Message = "Se generó un error al registrar la operación. " + pError;
                                return pResult;
                            }
                            Int64 pCodGenerado = pOpeResult.cod_ope_principal;

                            string[] camposConsig = null;
                            string[] camposObject = null;


                            camposConsig = pOperacion.strConsignacion.Split('|');

                            // MODIFICANDO SALDOS
                            decimal pVrEfectivo = string.IsNullOrEmpty(camposConsig[7]) ? 0 : Convert.ToDecimal(camposConsig[7]);
                            decimal pVrCheque = string.IsNullOrEmpty(camposConsig[8]) ? 0 : Convert.ToDecimal(camposConsig[8]);
                            if (pVrEfectivo > 0)
                            {
                                SyncSaldoCaja pSaldoMod = new SyncSaldoCaja();
                                pSaldoMod.fecha = DateTime.ParseExact(camposConsig[4], "dd/MM/yyyy", null);
                                pSaldoMod.cod_caja = Convert.ToInt64(camposConsig[2]);
                                pSaldoMod.cod_cajero = Convert.ToInt64(camposConsig[3]);
                                pSaldoMod.cod_moneda = Convert.ToInt32(camposConsig[6]);
                                pSaldoMod.valor = pVrEfectivo;
                                DACaja.CrearSaldoCajaConsig(pSaldoMod, vUsuario);
                            }

                            // CREANDO LA CONSIGNACION
                            SyncConsignacion pConsigna = new SyncConsignacion();
                            pConsigna.cod_ope = pCodGenerado;
                            pConsigna.cod_caja = Convert.ToInt64(camposConsig[2].ToString());
                            pConsigna.cod_cajero = Convert.ToInt64(camposConsig[3].ToString());
                            pConsigna.fecha_consignacion = DateTime.ParseExact(camposConsig[4], "dd/MM/yyyy", null);
                            pConsigna.cod_banco = !string.IsNullOrEmpty(camposConsig[5]) ? Convert.ToInt64(camposConsig[5]) : 0;
                            pConsigna.cod_moneda = Convert.ToInt32(camposConsig[6]);
                            pConsigna.valor_efectivo = pVrEfectivo;
                            pConsigna.valor_cheque = pVrCheque;
                            pConsigna.observaciones = !string.IsNullOrEmpty(camposConsig[9]) ? camposConsig[9] : null;
                            if (!string.IsNullOrEmpty(camposConsig[10]))
                                pConsigna.fecha_salida = DateTime.ParseExact(camposConsig[10], "dd/MM/yyyy", null);
                            else
                                pConsigna.fecha_salida = null;
                            pConsigna.num_cuenta = camposConsig[11] != "" ? camposConsig[11] : null;

                            pConsigna = DAConsignacion.CrearSyncConsignacion(pConsigna, vUsuario);


                            // INSERTANDO TRANSACCIONES
                            if (!string.IsNullOrEmpty(pOperacion.strTransacciones))
                            {
                                SyncTransaccionCaja pTransaccion = null;
                                string[] lstTransac = pOperacion.strTransacciones.Contains(";") ? pOperacion.strTransacciones.Split(';') : new string[] { pOperacion.strTransacciones };
                                foreach (string ntransac in lstTransac)
                                {
                                    camposObject = ntransac.Split('|');
                                    pTransaccion = new SyncTransaccionCaja();

                                    pTransaccion.cod_ope = pCodGenerado;
                                    pTransaccion.cod_caja = Convert.ToInt64(camposObject[2]);
                                    pTransaccion.cod_cajero = Convert.ToInt64(camposObject[3]);
                                    pTransaccion.fecha_aplica = DateTime.ParseExact(camposObject[5], "dd/MM/yyyy", null);
                                    pTransaccion.tipo_movimiento = camposObject[6];
                                    pTransaccion.cod_persona = Convert.ToInt64(camposObject[7]);
                                    pTransaccion.num_producto = string.IsNullOrEmpty(camposObject[8]) ? null : camposObject[8];
                                    pTransaccion.tipo_pago = camposObject[9];
                                    if (string.IsNullOrEmpty(camposObject[13]))
                                        pTransaccion.tipo_tran = null;
                                    else
                                        pTransaccion.tipo_tran = Convert.ToInt64(camposObject[13]);
                                    pTransaccion.cod_moneda = Convert.ToInt32(camposObject[10]);
                                    pTransaccion.valor_pago = Convert.ToDecimal(camposObject[11]);

                                    DATransac.CrearTransaccionCaja(pTransaccion, vUsuario);
                                }
                            }

                            // RECORRIENDO LOS MOVIMIENTOS
                            if (!string.IsNullOrEmpty(pOperacion.strConsignacionCheque))
                            {
                                SyncHomologaOperacionDeta pDeta = null;
                                string[] lstConsigCheque = pOperacion.strConsignacionCheque.Contains(";") ? pOperacion.strConsignacionCheque.Split(';') : new string[] { pOperacion.strConsignacionCheque };
                                foreach (string nConsigCheque in lstConsigCheque)
                                {
                                    camposObject = nConsigCheque.Split('|');
                                    // CONSULTAR HOMOLOGACION
                                    pDeta = new SyncHomologaOperacionDeta();
                                    pDeta.tabla_detalle = "MOVIMIENTOCAJA";
                                    pDeta.codigo_local = camposObject[1];
                                    pDeta = DAOperacion.ConsultarSyncHomologaOperacionDetalle(pDeta, vUsuario);

                                    // MODIFICANDO MOVIMIENTO
                                    if (pDeta != null)
                                    {
                                        DATransac.ModificarMovimientoCaja(Convert.ToInt64(pDeta.codigo_principal), vUsuario);

                                        //REGISTRANDO EN CONSIGNACION CHEQUE
                                        DAConsignacion.CrearConsignacionCheque(pConsigna.cod_consignacion, Convert.ToInt64(pDeta.codigo_principal), vUsuario);
                                    }
                                }
                            }

                            //GENERANDO HOMOLOGACION
                            if (pCodGenerado > 0)
                            {
                                SyncHomologaOperacion pHomologa = new SyncHomologaOperacion();
                                pHomologa.fecha = DateTime.Now;
                                pHomologa.tabla = "OPERACION";
                                pHomologa.campo_tabla = "COD_OPE";
                                pHomologa.codigo_principal = pCodGenerado.ToString();
                                pHomologa.codigo_local = pCodOpeLocal.ToString();
                                pHomologa.proceso = ProcesosOffline.ConsignacionOperacion.ToString();
                                pHomologa = DAOperacion.CrearSyncHomologaOperacion(pHomologa, vUsuario);

                                pResult.Success = true;
                                pResult.CodigoGenerado = pCodGenerado.ToString();
                            }
                            else
                            {
                                pResult.Success = false;
                                pResult.Message = "Se generó un error al registrar la operación.";
                            }

                        }
                        ts.Complete();
                    }
                }
                catch (Exception ex)
                {
                    pResult.Success = false;
                    pResult.Message = ex.Message;
                    ts.Dispose();
                }
            }
            return pResult;
        }


        public EntityGlobal CrearSyncCanjeCheque(SyncOperacion pOperacion, Usuario vUsuario)
        {
            SyncConsignacionData DAConsignacion = new SyncConsignacionData();
            SyncCajaData DACaja = new SyncCajaData();

            EntityGlobal pResult = new EntityGlobal();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    if (pOperacion != null)
                    {
                        if (!string.IsNullOrEmpty(pOperacion.strConsignacionCheque))
                        {
                            // CREANDO OPERACION
                            string pError = string.Empty;
                            Int64 pCodOpeLocal = pOperacion.cod_ope;
                            SyncOperacion pOpeResult = DAOperacion.CrearSyncOperacion(pOperacion, ref pError, vUsuario);
                            if (!string.IsNullOrEmpty(pError))
                            {
                                pResult.Success = false;
                                pResult.Message = "Se generó un error al registrar la operación. " + pError;
                                return pResult;
                            }
                            Int64 pCodGenerado = pOpeResult.cod_ope_principal;

                            string[] lstConsigCheque = pOperacion.strConsignacionCheque.Contains(";") ? pOperacion.strConsignacionCheque.Split(';') :
                                new string[] { pOperacion.strConsignacionCheque };

                            string[] camposObject = null;
                            SyncHomologaOperacionDeta pDeta = null;

                            SyncConsignacion pConsig = null;
                            Int64 pCodMotivo = 0;
                            Int64 pCodUsuario = pOperacion.cod_usuario;
                            foreach (string nConsig in lstConsigCheque)
                            {
                                camposObject = nConsig.Split('|');

                                // CONSULTAR HOMOLOGACION
                                pDeta = new SyncHomologaOperacionDeta();
                                pDeta.tabla_detalle = "MOVIMIENTOCAJA";
                                pDeta.codigo_local = camposObject[1];
                                pDeta = DAOperacion.ConsultarSyncHomologaOperacionDetalle(pDeta, vUsuario);

                                if (pDeta != null)
                                {
                                    pConsig = new SyncConsignacion();
                                    pConsig.cod_consignacion = Convert.ToInt64(pDeta.codigo_principal);
                                    pConsig.fecha_consignacion = DateTime.ParseExact(camposObject[3], "dd/MM/yyyy", null);
                                    pConsig.estado = Convert.ToInt32(camposObject[5]);
                                    pConsig.observaciones = string.IsNullOrEmpty(camposObject[6]) ? null : camposObject[6];
                                    pCodMotivo = Convert.ToInt64(camposObject[2]);
                                    DAConsignacion.GrabarCanje(pConsig, pCodMotivo, pCodUsuario, vUsuario);
                                }
                            }

                            //GENERANDO HOMOLOGACION
                            if (pCodGenerado > 0)
                            {
                                SyncHomologaOperacion pHomologa = new SyncHomologaOperacion();
                                pHomologa.fecha = DateTime.Now;
                                pHomologa.tabla = "OPERACION";
                                pHomologa.campo_tabla = "COD_OPE";
                                pHomologa.codigo_principal = pCodGenerado.ToString();
                                pHomologa.codigo_local = pCodOpeLocal.ToString();
                                pHomologa.proceso = ProcesosOffline.ChequeCanje.ToString();
                                pHomologa = DAOperacion.CrearSyncHomologaOperacion(pHomologa, vUsuario);

                                pResult.Success = true;
                                pResult.CodigoGenerado = pCodGenerado.ToString();
                            }
                            else
                            {
                                pResult.Success = false;
                                pResult.Message = "Se generó un error al registrar la operación.";
                            }

                        }
                        ts.Complete();
                    }
                }
                catch (Exception ex)
                {
                    pResult.Success = false;
                    pResult.Message = ex.Message;
                    ts.Dispose();
                }
            }
            return pResult;
        }


        public EntityGlobal CrearSyncTrasladoDinero(SyncOperacion pOperacion, Usuario vUsuario)
        {
            SyncTrasladoCajaData DATraslado = new SyncTrasladoCajaData();
            SyncCajaData DACaja = new SyncCajaData();

            EntityGlobal pResult = new EntityGlobal();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    if (!string.IsNullOrEmpty(pOperacion.strTraslado))
                    {
                        // CREANDO OPERACION
                        string pError = string.Empty;
                        Int64 pCodOpeLocal = pOperacion.cod_ope;
                        SyncOperacion pOpeResult = DAOperacion.CrearSyncOperacion(pOperacion, ref pError, vUsuario);
                        if (!string.IsNullOrEmpty(pError))
                        {
                            pResult.Success = false;
                            pResult.Message = "Se generó un error al registrar la operación. " + pError;
                            return pResult;
                        }
                        Int64 pCodGenerado = pOpeResult.cod_ope_principal;

                        // CREANDO EL TRASLADO
                        string[] objTraslado = string.IsNullOrEmpty(pOperacion.strTraslado) ? null : pOperacion.strTraslado.Split('|');
                        SyncTrasladoCaja pTraslado = new SyncTrasladoCaja();
                        pTraslado.cod_ope = pCodGenerado;
                        pTraslado.fecha_traslado = DateTime.ParseExact(objTraslado[3], "dd/MM/yyyy", null);
                        pTraslado.tipo_traslado = 1;
                        pTraslado.cod_caja_ori = Convert.ToInt64(objTraslado[4]);
                        pTraslado.cod_cajero_ori = Convert.ToInt64(objTraslado[5]);
                        pTraslado.cod_caja_des = Convert.ToInt64(objTraslado[6]);
                        pTraslado.cod_cajero_des = Convert.ToInt64(objTraslado[7]);
                        pTraslado.cod_moneda = Convert.ToInt32(objTraslado[8]);
                        pTraslado.valor = Convert.ToDecimal(objTraslado[9]);
                        pTraslado.estado = 0;
                        pTraslado = DATraslado.CrearTrasladoDinero(pTraslado, vUsuario);

                        // MODIFICANDO SALDO CAJA
                        SyncSaldoCaja pSaldoMod = new SyncSaldoCaja();
                        pSaldoMod.fecha = DateTime.ParseExact(objTraslado[3], "dd/MM/yyyy", null);
                        pSaldoMod.cod_caja = Convert.ToInt64(objTraslado[4]);
                        pSaldoMod.cod_cajero = Convert.ToInt64(objTraslado[5]);
                        pSaldoMod.cod_moneda = Convert.ToInt32(objTraslado[8]);
                        pSaldoMod.valor = Convert.ToDecimal(objTraslado[9]);
                        DACaja.ModificarSaldoCajaTraslado(pSaldoMod, vUsuario);

                        string[] camposObject = null;
                        // REGISTRO LAS TRANSACCIONES DE CAJA
                        if (!string.IsNullOrEmpty(pOperacion.strTransacciones))
                        {
                            SyncTransaccionCaja pTransaccion = null;
                            string[] lstTransac = pOperacion.strTransacciones.Contains(";") ? pOperacion.strTransacciones.Split(';') : new string[] { pOperacion.strTransacciones };
                            Int64 pNumTranLocal = 0;

                            foreach (string ntransac in lstTransac)
                            {
                                camposObject = ntransac.Split('|');
                                pTransaccion = new SyncTransaccionCaja();
                                pNumTranLocal = Convert.ToInt64(camposObject[0]);

                                pTransaccion.cod_ope = pCodGenerado;
                                pTransaccion.cod_caja = Convert.ToInt64(camposObject[2]);
                                pTransaccion.cod_cajero = Convert.ToInt64(camposObject[3]);
                                pTransaccion.fecha_aplica = DateTime.ParseExact(camposObject[5], "dd/MM/yyyy", null);
                                pTransaccion.tipo_movimiento = camposObject[6];
                                pTransaccion.cod_persona = Convert.ToInt64(camposObject[7]);
                                pTransaccion.num_producto = string.IsNullOrEmpty(camposObject[8]) ? null : camposObject[8];
                                pTransaccion.tipo_pago = camposObject[9];
                                if (string.IsNullOrEmpty(camposObject[13]))
                                    pTransaccion.tipo_tran = null;
                                else
                                    pTransaccion.tipo_tran = Convert.ToInt64(camposObject[13]);
                                pTransaccion.cod_moneda = Convert.ToInt32(camposObject[10]);
                                pTransaccion.valor_pago = Convert.ToDecimal(camposObject[11]);

                                pTransaccion = DATransac.CrearTransaccionCaja(pTransaccion, vUsuario);

                                // CREANDO HOMOLOGACION DETALLE
                                if (pTransaccion.num_trancaja != 0)
                                {
                                    SyncHomologaOperacionDeta pDeta = new SyncHomologaOperacionDeta();
                                    pDeta.cod_ope_principal = pCodGenerado;
                                    pDeta.cod_ope_local = pCodOpeLocal;
                                    pDeta.tabla_detalle = "TRANSACCIONESCAJA";
                                    pDeta.campo_tabla = "NUM_TRANCAJA";
                                    pDeta.codigo_principal = pTransaccion.num_trancaja.ToString();
                                    pDeta.codigo_local = pNumTranLocal.ToString();

                                    DAOperacion.CrearHomologacionOperacionDetalle(pDeta, vUsuario);
                                }
                            }
                        }

                        // REGISTRANDO LOS MOVIMIENTOS DE CAJA
                        if (!string.IsNullOrEmpty(pOperacion.strMovimientos))
                        {
                            SyncMovimientoCaja pMovCaja = null;
                            string[] lstMovimientos = pOperacion.strMovimientos.Contains(";") ? pOperacion.strMovimientos.Split(';') : new string[] { pOperacion.strMovimientos };

                            Int64 pCodMovLocal = 0;
                            foreach (string nMovimiento in lstMovimientos)
                            {
                                camposObject = nMovimiento.Split('|');
                                pMovCaja = new SyncMovimientoCaja();
                                pCodMovLocal = Convert.ToInt64(camposObject[0]);

                                pMovCaja.cod_ope = pCodGenerado;
                                pMovCaja.fec_ope = DateTime.ParseExact(camposObject[2], "dd/MM/yyyy", null);
                                pMovCaja.cod_caja = Convert.ToInt64(camposObject[3]);
                                pMovCaja.cod_cajero = Convert.ToInt64(camposObject[4]);
                                pMovCaja.tipo_mov = string.IsNullOrEmpty(camposObject[5]) ? "1" : camposObject[5];
                                pMovCaja.cod_tipo_pago = 1;
                                pMovCaja.cod_banco = null;
                                pMovCaja.num_documento = null;
                                pMovCaja.cod_moneda = Convert.ToInt32(camposObject[10]);
                                pMovCaja.valor = Convert.ToDecimal(camposObject[11]);
                                pMovCaja.cod_persona = Convert.ToInt64(camposObject[13]);
                                pMovCaja.idctabancaria = null;

                                pMovCaja = DATransac.CrearMovimientoCaja(pMovCaja, vUsuario);

                                // CREANDO HOMOLOGACION DETALLE
                                if (pMovCaja.cod_movimiento != 0)
                                {
                                    SyncHomologaOperacionDeta pDeta = new SyncHomologaOperacionDeta();
                                    pDeta.cod_ope_principal = pCodGenerado;
                                    pDeta.cod_ope_local = pCodOpeLocal;
                                    pDeta.tabla_detalle = "MOVIMIENTOCAJA";
                                    pDeta.campo_tabla = "COD_MOVIMIENTO";
                                    pDeta.codigo_principal = pMovCaja.cod_movimiento.ToString();
                                    pDeta.codigo_local = pCodMovLocal.ToString();

                                    DAOperacion.CrearHomologacionOperacionDetalle(pDeta, vUsuario);
                                }
                            }
                        }

                        //GENERANDO HOMOLOGACION
                        if (pCodGenerado > 0)
                        {
                            SyncHomologaOperacion pHomologa = new SyncHomologaOperacion();
                            pHomologa.fecha = DateTime.Now;
                            pHomologa.tabla = "OPERACION";
                            pHomologa.campo_tabla = "COD_OPE";
                            pHomologa.codigo_principal = pCodGenerado.ToString();
                            pHomologa.codigo_local = pCodOpeLocal.ToString();
                            pHomologa.proceso = ProcesosOffline.TrasladoDinero.ToString();
                            pHomologa = DAOperacion.CrearSyncHomologaOperacion(pHomologa, vUsuario);

                            pResult.Success = true;
                            pResult.CodigoGenerado = pCodGenerado.ToString();
                        }
                        else
                        {
                            pResult.Success = false;
                            pResult.Message = "Se generó un error al registrar la operación.";
                        }
                        ts.Complete();
                    }
                }
                catch (Exception ex)
                {
                    pResult.Success = false;
                    pResult.Message = ex.Message;
                    ts.Dispose();
                }
            }
            return pResult;
        }


        public EntityGlobal CrearSyncRecepcionDinero(SyncOperacion pOperacion, Usuario vUsuario)
        {
            SyncTrasladoCajaData DATraslado = new SyncTrasladoCajaData();
            SyncCajaData DACaja = new SyncCajaData();

            EntityGlobal pResult = new EntityGlobal();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    // CREANDO OPERACION
                    string pError = string.Empty;
                    Int64 pCodOpeLocal = pOperacion.cod_ope;
                    SyncOperacion pOpeResult = DAOperacion.CrearSyncOperacion(pOperacion, ref pError, vUsuario);
                    if (!string.IsNullOrEmpty(pError))
                    {
                        pResult.Success = false;
                        pResult.Message = "Se generó un error al registrar la operación. " + pError;
                        return pResult;
                    }
                    Int64 pCodGenerado = pOpeResult.cod_ope_principal;

                    // CREANDO LA RECEPCION
                    string[] objTraslado = string.IsNullOrEmpty(pOperacion.strTraslado) ? null : pOperacion.strTraslado.Split('|');
                    SyncTrasladoCaja pTraslado = new SyncTrasladoCaja();
                    pTraslado.cod_ope = pCodGenerado;
                    pTraslado.fecha_traslado = DateTime.ParseExact(objTraslado[3], "dd/MM/yyyy", null);
                    pTraslado.cod_traslado = Convert.ToInt64(objTraslado[0]);
                    pTraslado.estado = 1;
                    DATraslado.GenerarRecepcionDinero(pTraslado, vUsuario);

                    string[] camposObject = null;
                    // REGISTRO LAS TRANSACCIONES DE CAJA
                    if (!string.IsNullOrEmpty(pOperacion.strTransacciones))
                    {
                        SyncTransaccionCaja pTransaccion = null;
                        string[] lstTransac = pOperacion.strTransacciones.Contains(";") ? pOperacion.strTransacciones.Split(';') : new string[] { pOperacion.strTransacciones };
                        Int64 pNumTranLocal = 0;

                        foreach (string ntransac in lstTransac)
                        {
                            camposObject = ntransac.Split('|');
                            pTransaccion = new SyncTransaccionCaja();
                            pNumTranLocal = Convert.ToInt64(camposObject[0]);
                            
                            pTransaccion.cod_ope = pCodGenerado;
                            pTransaccion.cod_caja = Convert.ToInt64(camposObject[2]);
                            pTransaccion.cod_cajero = Convert.ToInt64(camposObject[3]);
                            pTransaccion.fecha_aplica = DateTime.ParseExact(camposObject[5], "dd/MM/yyyy", null);
                            pTransaccion.tipo_movimiento = camposObject[6];
                            pTransaccion.cod_persona = Convert.ToInt64(camposObject[7]);
                            pTransaccion.num_producto = string.IsNullOrEmpty(camposObject[8]) ? null : camposObject[8];
                            pTransaccion.tipo_pago = camposObject[9];
                            if (string.IsNullOrEmpty(camposObject[13]))
                                pTransaccion.tipo_tran = null;
                            else
                                pTransaccion.tipo_tran = Convert.ToInt64(camposObject[13]);
                            pTransaccion.cod_moneda = Convert.ToInt32(camposObject[10]);
                            pTransaccion.valor_pago = Convert.ToDecimal(camposObject[11]);

                            pTransaccion = DATransac.CrearTransaccionCaja(pTransaccion, vUsuario);

                            // CREANDO HOMOLOGACION DETALLE
                            if (pTransaccion.num_trancaja != 0)
                            {
                                SyncHomologaOperacionDeta pDeta = new SyncHomologaOperacionDeta();
                                pDeta.cod_ope_principal = pCodGenerado;
                                pDeta.cod_ope_local = pCodOpeLocal;
                                pDeta.tabla_detalle = "TRANSACCIONESCAJA";
                                pDeta.campo_tabla = "NUM_TRANCAJA";
                                pDeta.codigo_principal = pTransaccion.num_trancaja.ToString();
                                pDeta.codigo_local = pNumTranLocal.ToString();

                                DAOperacion.CrearHomologacionOperacionDetalle(pDeta, vUsuario);
                            }
                        }
                    }

                    // VARIABLES PARA USAR EN EL MOVIMIENTO SALDO CAJA
                    DateTime pFecOpe = DateTime.MinValue;
                    long pCodCaja = 0, pCodCajero = 0;
                    int pCodMoneda = 0;
                    decimal pValor = 0;

                    // REGISTRANDO LOS MOVIMIENTOS DE CAJA
                    if (!string.IsNullOrEmpty(pOperacion.strMovimientos))
                    {
                        SyncMovimientoCaja pMovCaja = null;
                        string[] lstMovimientos = pOperacion.strMovimientos.Contains(";") ? pOperacion.strMovimientos.Split(';') : new string[] { pOperacion.strMovimientos };

                        Int64 pCodMovLocal = 0;
                        foreach (string nMovimiento in lstMovimientos)
                        {
                            camposObject = nMovimiento.Split('|');
                            pMovCaja = new SyncMovimientoCaja();
                            pCodMovLocal = Convert.ToInt64(camposObject[0]);

                            // CAPTURANDO VALORES
                            pCodCaja = Convert.ToInt64(camposObject[3]);
                            pCodCajero = Convert.ToInt64(camposObject[4]);
                            pCodMoneda = Convert.ToInt32(camposObject[10]);
                            pValor = Convert.ToDecimal(camposObject[11]);

                            pMovCaja.cod_ope = pCodGenerado;
                            pMovCaja.fec_ope = DateTime.ParseExact(camposObject[2], "dd/MM/yyyy", null);
                            pMovCaja.cod_caja = pCodCaja;
                            pMovCaja.cod_cajero = pCodCajero;
                            pMovCaja.tipo_mov = string.IsNullOrEmpty(camposObject[5]) ? "1" : camposObject[5];
                            pMovCaja.cod_tipo_pago = 1;
                            pMovCaja.cod_banco = null;
                            pMovCaja.num_documento = null;
                            pMovCaja.cod_moneda = pCodMoneda;
                            pMovCaja.valor = pValor;
                            pMovCaja.cod_persona = Convert.ToInt64(camposObject[13]);
                            pMovCaja.idctabancaria = null;

                            pMovCaja = DATransac.CrearMovimientoCaja(pMovCaja, vUsuario);

                            // CREANDO HOMOLOGACION DETALLE
                            if (pMovCaja.cod_movimiento != 0)
                            {
                                SyncHomologaOperacionDeta pDeta = new SyncHomologaOperacionDeta();
                                pDeta.cod_ope_principal = pCodGenerado;
                                pDeta.cod_ope_local = pCodOpeLocal;
                                pDeta.tabla_detalle = "MOVIMIENTOCAJA";
                                pDeta.campo_tabla = "COD_MOVIMIENTO";
                                pDeta.codigo_principal = pMovCaja.cod_movimiento.ToString();
                                pDeta.codigo_local = pCodMovLocal.ToString();

                                DAOperacion.CrearHomologacionOperacionDetalle(pDeta, vUsuario);
                            }
                        }
                    }

                    // MODIFICANDO PENDIENTE DE SALDO CAJA
                    SyncSaldoCaja pSaldoMod = new SyncSaldoCaja();
                    pSaldoMod.fecha = pFecOpe;
                    pSaldoMod.cod_caja = pCodCaja;
                    pSaldoMod.cod_cajero = pCodCajero;
                    pSaldoMod.cod_moneda = pCodMoneda;
                    pSaldoMod.valor = pValor;
                    DACaja.CrearSaldoCaja(pSaldoMod, vUsuario);

                    //GENERANDO HOMOLOGACION
                    if (pCodGenerado > 0)
                    {
                        SyncHomologaOperacion pHomologa = new SyncHomologaOperacion();
                        pHomologa.fecha = DateTime.Now;
                        pHomologa.tabla = "OPERACION";
                        pHomologa.campo_tabla = "COD_OPE";
                        pHomologa.codigo_principal = pCodGenerado.ToString();
                        pHomologa.codigo_local = pCodOpeLocal.ToString();
                        pHomologa.proceso = ProcesosOffline.RecepcionDinero.ToString();
                        pHomologa = DAOperacion.CrearSyncHomologaOperacion(pHomologa, vUsuario);

                        pResult.Success = true;
                        pResult.CodigoGenerado = pCodGenerado.ToString();
                    }
                    else
                    {
                        pResult.Success = false;
                        pResult.Message = "Se generó un error al registrar la operación.";
                    }
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    pResult.Success = false;
                    pResult.Message = ex.Message;
                    ts.Dispose();
                }
            }
            return pResult;
        }


        public EntityGlobal CrearSyncReversionOperacion(SyncOperacion pOperacion, Usuario vUsuario)
        {
            EntityGlobal pResult = new EntityGlobal();
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
            {
                try
                {
                    Int64 pCodGenerado = 0;
                    if (!string.IsNullOrEmpty(pOperacion.strTransacciones))
                    {
                        // CONSULTANDO HOMOLOGACION DE OPERACION LOCAL
                        string pFiltro = " WHERE H.TABLA = 'OPERACION' AND H.CAMPO_TABLA = 'COD_OPE' AND H.CODIGO_LOCAL = '" + pOperacion.cod_ope + "'";
                        SyncHomologaOperacion pHomologa = DAOperacion.ConsultarHomologacionOperacion(pFiltro, vUsuario);
                        if (pHomologa == null)
                        {
                            pResult.Success = false;
                            pResult.Message = "Se generó un error al registrar la operación.";
                            return pResult;
                        }
                        pCodGenerado = Convert.ToInt64(pHomologa.codigo_principal);

                        // CONSULTAR LISTA DE TRANSACCIONES
                        string[] lstTransac = pOperacion.strTransacciones.Contains(";") ? pOperacion.strTransacciones.Split(';') : new string[] { pOperacion.strTransacciones };
                        Int64 pCodMotivo = 0;
                        Int64 pCodUsuario = 0;
                        string[] camposObject = null;
                        foreach (string nTransac in lstTransac)
                        {
                            camposObject = nTransac.Split('|');
                            pCodMotivo = Convert.ToInt64(camposObject[2]);
                            pCodUsuario = Convert.ToInt64(camposObject[3]);
                            if (pCodMotivo > 0 && pCodUsuario > 0)
                                break;
                        }
                        SyncTransaccionCaja pEntidad = new SyncTransaccionCaja();
                        pEntidad.cod_ope = pCodGenerado;
                        List<SyncTransaccionCaja> lstTransaccion = DATransac.ListarTransaccionesPendientes(pEntidad, vUsuario);
                        SyncTransaccionCaja pTranCaja = null;
                        
                        foreach (SyncTransaccionCaja nTransaccion in lstTransaccion)
                        {
                            // CREAR REVERSION DE TRANSACCIONES
                            pTranCaja = new SyncTransaccionCaja();
                            pTranCaja.tipo_motivo = pCodMotivo;
                            pTranCaja.num_trancaja = nTransaccion.num_trancaja;
                            pTranCaja.cod_usuario = pCodUsuario;
                            pTranCaja = DATransac.CrearReversionTransaccionCaja(pTranCaja, vUsuario);
                        }
                        
                        // GENERAR ANULACION DE OPERACION
                        SyncOperacion pOpera = new SyncOperacion();
                        pOpera.cod_ope = pCodGenerado;
                        pOpera.cod_usuario = 0; // ENVIO DE CODIGO EN 0 NO SE USA EN EL PL
                        DAOperacion.GenerarAnulacionOperacion(pOpera, vUsuario);

                        //GENERANDO HOMOLOGACION
                        if (pCodGenerado > 0)
                        {
                            SyncHomologaOperacion pHomolo = new SyncHomologaOperacion();
                            pHomolo.fecha = DateTime.Now;
                            pHomolo.tabla = "OPERACION";
                            pHomolo.campo_tabla = "COD_OPE";
                            pHomolo.codigo_principal = pCodGenerado.ToString();
                            pHomolo.codigo_local = pOperacion.cod_ope.ToString();
                            pHomolo.proceso = ProcesosOffline.ReversionOperacion.ToString();
                            pHomolo = DAOperacion.CrearSyncHomologaOperacion(pHomolo, vUsuario);

                            pResult.Success = true;
                            pResult.CodigoGenerado = pCodGenerado.ToString();
                        }
                        else
                        {
                            pResult.Success = false;
                            pResult.Message = "Se generó un error al registrar la operación.";
                        }
                    }
                    else
                    {
                        pResult.Success = false;
                        pResult.Message = "Se generó un error al registrar la reversión.";
                    }
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    pResult.Success = false;
                    pResult.Message = ex.Message;
                    ts.Dispose();
                }
            }
            return pResult;
        }



        public SyncHomologaOperacion ConsultarHomologacionOperacion(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAOperacion.ConsultarHomologacionOperacion(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SyncProductosBusiness", "ConsultarHomologacionOperacion", ex);
                return null;
            }
        }


    }
}
