using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Data
{
    public class SyncTransaccionCajaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        public SyncTransaccionCajaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public SyncMovimientoCaja CrearMovimientoCaja(SyncMovimientoCaja pMovimientoCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcodigoope";
                        pcod_ope.Value = pMovimientoCaja.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pfec_ope = cmdTransaccionFactory.CreateParameter();
                        pfec_ope.ParameterName = "pfechaope";
                        pfec_ope.Value = pMovimientoCaja.fec_ope;
                        pfec_ope.Direction = ParameterDirection.Input;
                        pfec_ope.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_ope);

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        if (pMovimientoCaja.cod_caja == null)
                            pcod_caja.Value = DBNull.Value;
                        else
                            pcod_caja.Value = pMovimientoCaja.cod_caja;
                        pcod_caja.Direction = ParameterDirection.Input;
                        pcod_caja.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);

                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "pcodigocajero";
                        if (pMovimientoCaja.cod_cajero == null)
                            pcod_cajero.Value = DBNull.Value;
                        else
                            pcod_cajero.Value = pMovimientoCaja.cod_cajero;
                        pcod_cajero.Direction = ParameterDirection.Input;
                        pcod_cajero.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "ptipomov";
                        if (pMovimientoCaja.tipo_mov == null)
                            ptipo_mov.Value = DBNull.Value;
                        else
                            ptipo_mov.Value = pMovimientoCaja.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter pcod_tipo_pago = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_pago.ParameterName = "ptipopago";
                        pcod_tipo_pago.Value = pMovimientoCaja.cod_tipo_pago;
                        pcod_tipo_pago.Direction = ParameterDirection.Input;
                        pcod_tipo_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_pago);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "pcodigobanco";
                        if (pMovimientoCaja.cod_banco == null)
                            pcod_banco.Value = DBNull.Value;
                        else
                            pcod_banco.Value = pMovimientoCaja.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pnum_documento = cmdTransaccionFactory.CreateParameter();
                        pnum_documento.ParameterName = "pnumdoc";
                        if (pMovimientoCaja.num_documento == null)
                            pnum_documento.Value = DBNull.Value;
                        else
                            pnum_documento.Value = pMovimientoCaja.num_documento;
                        pnum_documento.Direction = ParameterDirection.Input;
                        pnum_documento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_documento);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "pcodigomoneda";
                        if (pMovimientoCaja.cod_moneda == null)
                            pcod_moneda.Value = DBNull.Value;
                        else
                            pcod_moneda.Value = pMovimientoCaja.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        pcod_moneda.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        if (pMovimientoCaja.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pMovimientoCaja.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        //DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        //pestado.ParameterName = "pestado";
                        //if (pMovimientoCaja.estado == null)
                        //    pestado.Value = DBNull.Value;
                        //else
                        //    pestado.Value = pMovimientoCaja.estado;
                        //pestado.Direction = ParameterDirection.Input;
                        //pestado.DbType = DbType.Int32;
                        //cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "pcodpersona";
                        if (pMovimientoCaja.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pMovimientoCaja.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "pidctabancaria";
                        if (pMovimientoCaja.idctabancaria == null)
                            pidctabancaria.Value = DBNull.Value;
                        else
                            pidctabancaria.Value = pMovimientoCaja.idctabancaria;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        DbParameter pcod_movimiento = cmdTransaccionFactory.CreateParameter();
                        pcod_movimiento.ParameterName = "pcod_movimiento";
                        pcod_movimiento.Value = pMovimientoCaja.cod_movimiento;
                        pcod_movimiento.Direction = ParameterDirection.Output;
                        pcod_movimiento.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_movimiento);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SYN_MOVIMICAJA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pMovimientoCaja.cod_movimiento = Convert.ToInt64(pcod_movimiento.Value);
                        return pMovimientoCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncTransaccionCajaData", "CrearMovimientoCaja", ex);
                        return null;
                    }
                }
            }
        }


        public bool ModificarMovimientoCaja(long pCodigoMov, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_ingegr = cmdTransaccionFactory.CreateParameter();
                        pcod_ingegr.ParameterName = "pcodigoingegr";
                        pcod_ingegr.Value = pCodigoMov;
                        pcod_ingegr.DbType = DbType.Int64;
                        pcod_ingegr.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_ingegr);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = 1;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_MOVIMIENTOCAJA_U";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncTransaccionCajaData", "ModificarMovimientoCaja", ex);
                        return false;
                    }
                }
            }
        }


        public SyncTransaccionCaja CrearTransaccionCaja(SyncTransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha_aplica = cmdTransaccionFactory.CreateParameter();
                        pfecha_aplica.ParameterName = "pfechamov";
                        pfecha_aplica.Value = pTransaccionCaja.fecha_aplica;
                        pfecha_aplica.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aplica);

                        DbParameter ptipo_movimiento = cmdTransaccionFactory.CreateParameter();
                        ptipo_movimiento.ParameterName = "ptipomov";
                        ptipo_movimiento.Value = pTransaccionCaja.tipo_movimiento;
                        ptipo_movimiento.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptipo_movimiento);

                        DbParameter pcod_caja = cmdTransaccionFactory.CreateParameter();
                        pcod_caja.ParameterName = "pcodigocaja";
                        pcod_caja.Value = pTransaccionCaja.cod_caja;
                        pcod_caja.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_caja);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "pcodigooper";
                        pcod_ope.Value = pTransaccionCaja.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "pcodigotipotran";
                        if (pTransaccionCaja.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pTransaccionCaja.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcod_cajero = cmdTransaccionFactory.CreateParameter();
                        pcod_cajero.ParameterName = "pcodigocajero";
                        if (pTransaccionCaja.cod_cajero == null)
                            pcod_cajero.Value = DBNull.Value;
                        else
                            pcod_cajero.Value = pTransaccionCaja.cod_cajero;
                        pcod_cajero.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_cajero);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "pcodigousuario";
                        pcod_persona.Value = pTransaccionCaja.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "pcodigomoneda";
                        pcod_moneda.Value = pTransaccionCaja.cod_moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        DbParameter pvalor_pago = cmdTransaccionFactory.CreateParameter();
                        pvalor_pago.ParameterName = "pvalorpago";
                        pvalor_pago.Value = pTransaccionCaja.valor_pago;
                        pvalor_pago.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pvalor_pago);

                        DbParameter ptipo_pago = cmdTransaccionFactory.CreateParameter();
                        ptipo_pago.ParameterName = "pformapago";
                        ptipo_pago.Value = pTransaccionCaja.tipo_pago;
                        ptipo_pago.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(ptipo_pago);

                        DbParameter pnum_producto = cmdTransaccionFactory.CreateParameter();
                        pnum_producto.ParameterName = "pnroprod";
                        if (pTransaccionCaja.num_producto == null)
                            pnum_producto.Value = DBNull.Value;
                        else
                            pnum_producto.Value = pTransaccionCaja.num_producto;
                        pnum_producto.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnum_producto);

                        DbParameter pnum_trancaja = cmdTransaccionFactory.CreateParameter();
                        pnum_trancaja.ParameterName = "pnum_trancaja";
                        pnum_trancaja.Value = pTransaccionCaja.num_trancaja;
                        pnum_trancaja.Direction = ParameterDirection.Output;
                        pnum_trancaja.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_trancaja);


                        //DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        //pestado.ParameterName = "p_estado";
                        //if (pTransaccionCaja.estado == null)
                        //    pestado.Value = DBNull.Value;
                        //else
                        //    pestado.Value = pTransaccionCaja.estado;
                        //pestado.Direction = ParameterDirection.Input;
                        //cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SYN_TRANSACAJA_OPE_C";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTransaccionCaja.num_trancaja = Convert.ToInt64(pnum_trancaja.Value);
                        return pTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncTransaccionCajaData", "CrearTransaccionCaja", ex);
                        return null;
                    }
                }
            }
        }


        public SyncTransaccionCaja CrearReversionTransaccionCaja(SyncTransaccionCaja pTransaccion, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_tipo_anulacion = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_anulacion.ParameterName = "pcodigomotivoreversion";
                        pcod_tipo_anulacion.Value = pTransaccion.tipo_motivo;
                        pcod_tipo_anulacion.Direction = ParameterDirection.Input;

                        DbParameter pcod_transac = cmdTransaccionFactory.CreateParameter();
                        pcod_transac.ParameterName = "pcodigotransaccion";
                        pcod_transac.Value = pTransaccion.num_trancaja;
                        pcod_transac.Direction = ParameterDirection.Input;

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "pcodigousuario";
                        pcod_usuario.Value = pTransaccion.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_anulacion);
                        cmdTransaccionFactory.Parameters.Add(pcod_transac);
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPN_CAJAFIN_REVER_TRANCAJA_C";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pTransaccion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncTransaccionCajaData", "CrearReversionTransaccionCaja", ex);
                        return null;
                    }
                }
            }
        }

        public List<SyncTransaccionCaja> ListarTransaccionesPendientes(SyncTransaccionCaja pTransaccionCaja, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SyncTransaccionCaja> lstTransaccionCaja = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select num_trancaja, fecha_movimiento from transaccionescaja where cod_ope = " + pTransaccionCaja.cod_ope;
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.HasRows)
                        {
                            SyncTransaccionCaja entidad;
                            while (resultado.Read())
                            {
                                entidad = new SyncTransaccionCaja();
                                if (resultado["num_trancaja"] != DBNull.Value) entidad.num_trancaja = Convert.ToInt64(resultado["num_trancaja"]);
                                if (resultado["fecha_movimiento"] != DBNull.Value) entidad.fecha_movimiento = Convert.ToDateTime(resultado["fecha_movimiento"]);
                                lstTransaccionCaja.Add(entidad);
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTransaccionCaja;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncTransaccionCajaData", "ListarTransaccionesPendientes", ex);
                        return null;
                    }
                }
            }
        }


    }
}
