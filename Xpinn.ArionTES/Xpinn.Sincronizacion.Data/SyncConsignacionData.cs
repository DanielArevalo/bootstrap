using System;
using System.Data;
using System.Data.Common;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Data
{
    public class SyncConsignacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        public SyncConsignacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public SyncConsignacion CrearSyncConsignacion(SyncConsignacion pConsignacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodey_opera = cmdTransaccionFactory.CreateParameter();
                        pcodey_opera.ParameterName = "pcodigooper";
                        pcodey_opera.Value = pConsignacion.cod_ope;
                        pcodey_opera.DbType = DbType.Int64;
                        pcodey_opera.Direction = ParameterDirection.Input;

                        DbParameter pcod_consignacion = cmdTransaccionFactory.CreateParameter();
                        pcod_consignacion.ParameterName = "pcodigoconsignacion";
                        pcod_consignacion.Value = pConsignacion.cod_consignacion;
                        pcod_consignacion.DbType = DbType.Int64;
                        pcod_consignacion.Direction = ParameterDirection.Output;

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pConsignacion.cod_caja;
                        pcod_caja.DbType = DbType.Int64;
                        pcod_caja.Direction = ParameterDirection.Input;

                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "pcodigocajero";
                        pcod_cajero.Value = pConsignacion.cod_cajero;
                        pcod_cajero.DbType = DbType.Int64;
                        pcod_cajero.Direction = ParameterDirection.Input;

                        DbParameter pfecha_consignacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_consignacion.ParameterName = "pfechaconsignacion";
                        pfecha_consignacion.Value = pConsignacion.fecha_consignacion;
                        pfecha_consignacion.DbType = DbType.DateTime;
                        pfecha_consignacion.Direction = ParameterDirection.Input;

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "pcodigobanco";
                        pcod_banco.Value = pConsignacion.cod_banco;
                        pcod_banco.DbType = DbType.Int64;
                        pcod_banco.Direction = ParameterDirection.Input;

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "pcodigomoneda";
                        pcod_moneda.Value = pConsignacion.cod_moneda;
                        pcod_moneda.DbType = DbType.Int64;
                        pcod_moneda.Direction = ParameterDirection.Input;

                        DbParameter pvalor_efectivo = cmdTransaccionFactory.CreateParameter();
                        pvalor_efectivo.ParameterName = "pvalorefectivo";
                        pvalor_efectivo.Value = pConsignacion.valor_efectivo;
                        pvalor_efectivo.DbType = DbType.Decimal;
                        pvalor_efectivo.Direction = ParameterDirection.Input;

                        DbParameter pvalor_cheque = cmdTransaccionFactory.CreateParameter();
                        pvalor_cheque.ParameterName = "pvalorcheque";
                        pvalor_cheque.Value = pConsignacion.valor_cheque;
                        pvalor_cheque.DbType = DbType.Decimal;
                        pvalor_cheque.Direction = ParameterDirection.Input;

                        DbParameter pobservacionescon = cmdTransaccionFactory.CreateParameter();
                        pobservacionescon.ParameterName = "pobservaciones";
                        if (string.IsNullOrEmpty(pConsignacion.observaciones))
                            pobservacionescon.Value = DBNull.Value;
                        else
                            pobservacionescon.Value = pConsignacion.observaciones;
                        pobservacionescon.DbType = DbType.String;
                        pobservacionescon.Direction = ParameterDirection.Input;

                        DbParameter pfecha_salida = cmdTransaccionFactory.CreateParameter();
                        pfecha_salida.ParameterName = "pfechasalida";
                        if(pConsignacion.fecha_salida != null)
                            pfecha_salida.Value = pConsignacion.fecha_salida;
                        else
                            pfecha_salida.Value = DBNull.Value;
                        pfecha_salida.DbType = DbType.DateTime;
                        pfecha_salida.Direction = ParameterDirection.Input;

                        DbParameter pcodcuenta = cmdTransaccionFactory.CreateParameter();
                        pcodcuenta.ParameterName = "pcodcuenta";
                        pcodcuenta.Value = pConsignacion.num_cuenta;
                        pcodcuenta.DbType = DbType.String;
                        pcodcuenta.Direction = ParameterDirection.Input;
                        
                        cmdTransaccionFactory.Parameters.Add(pcodey_opera);
                        cmdTransaccionFactory.Parameters.Add(pcod_consignacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);
                        cmdTransaccionFactory.Parameters.Add(pfecha_consignacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);
                        cmdTransaccionFactory.Parameters.Add(pvalor_efectivo);
                        cmdTransaccionFactory.Parameters.Add(pvalor_cheque);
                        cmdTransaccionFactory.Parameters.Add(pobservacionescon);
                        cmdTransaccionFactory.Parameters.Add(pfecha_salida);
                        cmdTransaccionFactory.Parameters.Add(pcodcuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_CONSIGNACION_C";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pConsignacion.cod_consignacion = Convert.ToInt64(pcod_consignacion.Value);

                        return pConsignacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncConsignacionData", "CrearSyncConsignacion", ex);
                        return null;
                    }
                }
            }
        }

        public bool CrearConsignacionCheque(Int64 pCodConsig, Int64 pCodIngEgr, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodi_ingegr = cmdTransaccionFactory.CreateParameter();
                        pcodi_ingegr.ParameterName = "pcodigoingegr";
                        pcodi_ingegr.Value = pCodIngEgr;
                        pcodi_ingegr.DbType = DbType.Int64;
                        pcodi_ingegr.Direction = ParameterDirection.Input;

                        DbParameter pcodi_estado = cmdTransaccionFactory.CreateParameter();
                        pcodi_estado.ParameterName = "pestado";
                        pcodi_estado.Value = 1;
                        pcodi_estado.DbType = DbType.Int64;
                        pcodi_estado.Direction = ParameterDirection.Input;

                        DbParameter pconsignacion = cmdTransaccionFactory.CreateParameter();
                        pconsignacion.ParameterName = "pconsignacion";
                        pconsignacion.Value = pCodConsig;
                        pconsignacion.DbType = DbType.Int64;
                        pconsignacion.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcodi_ingegr);
                        cmdTransaccionFactory.Parameters.Add(pcodi_estado);
                        cmdTransaccionFactory.Parameters.Add(pconsignacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_CONSIGCHEQUE_C";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncConsignacionData", "CrearConsignacionCheque", ex);
                        return false;
                    }
                }
            }
        }


        public SyncConsignacion GrabarCanje(SyncConsignacion pConsignacionCheque, Int64 pCodMotivo, Int64 pCodUsuario, Usuario vUsuario)
        {
            Int64 operacion = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCOD_MOVIMIENTO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOVIMIENTO.ParameterName = "p_cod_ingegr";
                        pCOD_MOVIMIENTO.Value = pConsignacionCheque.cod_consignacion;
                        pCOD_MOVIMIENTO.Direction = ParameterDirection.Input;
                        //pCOD_MOVIMIENTO.DbType = DbType.Int64;

                        DbParameter pCOD_MOTIVO = cmdTransaccionFactory.CreateParameter();
                        pCOD_MOTIVO.ParameterName = "p_cod_motivo";
                        if (pCodMotivo == 0)
                            pCOD_MOTIVO.Value = DBNull.Value;
                        else
                            pCOD_MOTIVO.Value = pCodMotivo;
                        pCOD_MOTIVO.Direction = ParameterDirection.Input;
                        //pCOD_MOTIVO.DbType = DbType.Int64;

                        DbParameter pFECHA_CANJE = cmdTransaccionFactory.CreateParameter();
                        pFECHA_CANJE.ParameterName = "p_fecha_canje";
                        pFECHA_CANJE.Value = pConsignacionCheque.fecha_consignacion;
                        pFECHA_CANJE.Direction = ParameterDirection.Input;
                        pFECHA_CANJE.DbType = DbType.DateTime;

                        DbParameter pFECHA_DEVUELTO = cmdTransaccionFactory.CreateParameter();
                        pFECHA_DEVUELTO.ParameterName = "p_fecha_devuelto";
                        pFECHA_DEVUELTO.Value = pConsignacionCheque.fecha_consignacion;
                        pFECHA_DEVUELTO.Direction = ParameterDirection.Input;
                        pFECHA_DEVUELTO.DbType = DbType.DateTime;

                        DbParameter pESTADO = cmdTransaccionFactory.CreateParameter();
                        pESTADO.ParameterName = "p_estado";
                        pESTADO.Value = pConsignacionCheque.estado;
                        pESTADO.Direction = ParameterDirection.Input;
                        pESTADO.DbType = DbType.Int32;

                        DbParameter pOBSERVACIONES = cmdTransaccionFactory.CreateParameter();
                        pOBSERVACIONES.ParameterName = "p_observaciones";
                        if (pConsignacionCheque.observaciones != null)
                            pOBSERVACIONES.Value = pConsignacionCheque.observaciones;
                        else
                            pOBSERVACIONES.Value = DBNull.Value;
                        pOBSERVACIONES.Direction = ParameterDirection.Input;
                        pOBSERVACIONES.DbType = DbType.String;

                        DbParameter p_operacion = cmdTransaccionFactory.CreateParameter();
                        p_operacion.ParameterName = "p_operacion";
                        p_operacion.Value = pConsignacionCheque.cod_ope;
                        p_operacion.Direction = ParameterDirection.Input;
                        p_operacion.DbType = DbType.Int64;

                        DbParameter pUSUARIO = cmdTransaccionFactory.CreateParameter();
                        pUSUARIO.ParameterName = "p_Usuario";
                        pUSUARIO.Value = pCodUsuario;
                        pUSUARIO.Direction = ParameterDirection.Input;
                        pUSUARIO.DbType = DbType.Int64;

                        cmdTransaccionFactory.Parameters.Add(pCOD_MOVIMIENTO);
                        cmdTransaccionFactory.Parameters.Add(pCOD_MOTIVO);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_CANJE);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_DEVUELTO);
                        cmdTransaccionFactory.Parameters.Add(pESTADO);
                        cmdTransaccionFactory.Parameters.Add(pOBSERVACIONES);
                        cmdTransaccionFactory.Parameters.Add(p_operacion);
                        cmdTransaccionFactory.Parameters.Add(pUSUARIO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CHE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (pConsignacionCheque.cod_ope != null)
                        {
                            operacion = Convert.ToInt64(pConsignacionCheque.cod_ope);
                            pConsignacionCheque.cod_ope = operacion;
                        }

                        return pConsignacionCheque;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncConsignacionData", "GrabarCanje", ex);
                        return null;
                    }
                }
            }
        }


    }
}
