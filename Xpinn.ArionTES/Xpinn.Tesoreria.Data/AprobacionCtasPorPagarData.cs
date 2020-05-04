using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla AreasCaj
    /// </summary>
    public class AprobacionCtasPorPagarData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla AreasCaj
        /// </summary>
        public AprobacionCtasPorPagarData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<AprobacionCtasPorPagar> ListarCuentasXpagar(String filtro, String Orden, DateTime pFechaIng, DateTime pFechaVencIni, DateTime pFechaVencFin, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AprobacionCtasPorPagar> lstCuentas = new List<AprobacionCtasPorPagar>();
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.COD_OPE,D.CODPAGOFAC,C.CODIGO_FACTURA,C.NUMERO_FACTURA,C.FECHA_INGRESO,C.FECHA_FACTURA,C.FECHA_VENCIMIENTO,V.COD_PERSONA,V.IDENTIFICACION,V.NOMBRE, "
                                        +"SUM(CD.VALOR_NETO) AS SALDO_ACTUAL,D.VALOR AS VALORTOTAL, "
                                        +"CASE C.MANEJA_DESCUENTOS WHEN 1 THEN D.VALOR_DESCUENTO WHEN 0 THEN 0 END AS VALOR_DESCUENTO, "
                                        +"CASE C.MANEJA_DESCUENTOS WHEN 1 THEN (D.VALOR - D.VALOR_DESCUENTO) WHEN 0 THEN D.VALOR END AS NETO_PAGAR, "
                                        +"SUM(CD.VALOR_NETO) - (CASE C.MANEJA_DESCUENTOS WHEN 1 THEN (D.VALOR - D.VALOR_DESCUENTO) WHEN 0 THEN D.VALOR END) AS NUEVO_SALDO "
                                        +"FROM CUENTASXPAGAR C LEFT JOIN V_PERSONA V ON V.COD_PERSONA = C.COD_PERSONA "
                                        +"LEFT JOIN CUENTAXPAGAR_PAGO D ON D.CODIGO_FACTURA = C.CODIGO_FACTURA "
                                        +"LEFT JOIN CUENTAXPAGAR_DETALLE CD ON CD.CODIGO_FACTURA = C.CODIGO_FACTURA " + filtro;

                        if (pFechaIng != null && pFechaIng != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.FECHA_INGRESO = To_Date('" + Convert.ToDateTime(pFechaIng).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.FECHA_INGRESO = '" + Convert.ToDateTime(pFechaIng).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pFechaVencIni != null && pFechaVencIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.FECHA_VENCIMIENTO >= To_Date('" + Convert.ToDateTime(pFechaVencIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.FECHA_VENCIMIENTO >= '" + Convert.ToDateTime(pFechaVencIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pFechaVencFin != null && pFechaVencFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " C.FECHA_VENCIMIENTO <= To_Date('" + Convert.ToDateTime(pFechaVencFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " C.FECHA_VENCIMIENTO <= '" + Convert.ToDateTime(pFechaVencFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        sql += "GROUP BY  C.COD_OPE,D.CODPAGOFAC,C.CODIGO_FACTURA,C.NUMERO_FACTURA,C.FECHA_INGRESO,C.FECHA_FACTURA,C.FECHA_VENCIMIENTO,V.COD_PERSONA,V.IDENTIFICACION,V.NOMBRE,D.VALOR, "
                                    + "C.MANEJA_DESCUENTOS,D.VALOR_DESCUENTO,D.VALOR ";

                        if (Orden != "")
                            sql += "ORDER BY " + Orden;
                        else
                            sql += "ORDER BY C.CODIGO_FACTURA ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AprobacionCtasPorPagar entidad = new AprobacionCtasPorPagar();
                            if (resultado["CODPAGOFAC"] != DBNull.Value) entidad.codpagofac = Convert.ToInt32(resultado["CODPAGOFAC"]);
                            if (resultado["CODIGO_FACTURA"] != DBNull.Value) entidad.codigo_factura = Convert.ToInt32(resultado["CODIGO_FACTURA"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["NUMERO_FACTURA"] != DBNull.Value) entidad.numero_factura = Convert.ToString(resultado["NUMERO_FACTURA"]);
                            if (resultado["FECHA_INGRESO"] != DBNull.Value) entidad.fecha_ingreso = Convert.ToDateTime(resultado["FECHA_INGRESO"]);
                            if (resultado["FECHA_FACTURA"] != DBNull.Value) entidad.fec_fact = Convert.ToDateTime(resultado["FECHA_FACTURA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALDO_ACTUAL"] != DBNull.Value) entidad.saldo_actual = Convert.ToDecimal(resultado["SALDO_ACTUAL"]);
                            if (resultado["VALORTOTAL"] != DBNull.Value) entidad.valortotal = Convert.ToDecimal(resultado["VALORTOTAL"]);
                            if (resultado["VALOR_DESCUENTO"] != DBNull.Value) entidad.valordescuento = Convert.ToDecimal(resultado["VALOR_DESCUENTO"]);
                            if (resultado["NETO_PAGAR"] != DBNull.Value) entidad.valorneto = Convert.ToDecimal(resultado["NETO_PAGAR"]);
                            if (resultado["NUEVO_SALDO"] != DBNull.Value) entidad.nuevo_saldo = Convert.ToDecimal(resultado["NUEVO_SALDO"]);                            
                            lstCuentas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ListarCuentasXpagar", ex);
                        return null;
                    }
                }
            }
        }


        public void ActualizarEstadoCuentaXpagar(int codpagoFac, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "UPDATE CUENTAXPAGAR_PAGO SET ESTADO = 1 WHERE CODPAGOFAC = " + codpagoFac.ToString();
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ActualizarEstadoCuentaXpagar", ex);                        
                    }
                }
            }
        }


        public void ActualizarGiroEstadoCuentaXpagar(int codpagoFac,int idcuentabancaria, int cod_banco, String num_cuenta, int tipo_cuenta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "UPDATE CUENTASXPAGAR SET  idctabancaria = " + idcuentabancaria.ToString() + (cod_banco == 0 ? "" :  " , cod_banco = "  + cod_banco.ToString()) + (num_cuenta == null ? "" : " , num_cuenta = '" + num_cuenta.ToString()) + "' , tipo_cuenta =" + tipo_cuenta.ToString()   + " WHERE CODigo_factura = " + codpagoFac.ToString() ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ActualizarGiroEstadoCuentaXpagar", ex);
                    }
                }
            }
        }

        public void ActualizarGiroAprobCuentaXpagar(int cod_ope, int idcuentabancaria, int cod_banco, String num_cuenta, int tipo_cuenta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "UPDATE CUENTASXPAGAR SET " + (cod_banco != Int32.MinValue && cod_banco != 0 ? "cod_banco = " + cod_banco.ToString() + ", ": "") + " num_cuenta = " + num_cuenta.ToString() + " , tipo_cuenta = " + tipo_cuenta.ToString() + " WHERE cod_ope = " + cod_ope.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ActualizarGiroAprobCuentaXpagar", ex);
                    }
                }
            }
        }

        public Giro ModificarGiroXCod_ope(Giro pCtas, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        Configuracion conf = new Configuracion();
                        sql = "update giro   set tipo_acto = 10 , fec_apro = To_Date('" + pCtas.fec_apro_giro.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ,"
                        + " usu_apro = '" + pCtas.usu_apro + "', valor = " + pCtas.valor + " , estado = " + pCtas.estadogi + ", forma_pago = " + pCtas.forma_pago
                        + " where cod_ope= " + pCtas.cod_ope;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pCtas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ModificarGiroXCod_ope", ex);
                        return null;
                    }
                }
            }
        }





    }
}