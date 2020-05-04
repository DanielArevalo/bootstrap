using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Programado.Entities;

namespace Xpinn.Programado.Data
{

    public class MovimientoCuentasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public MovimientoCuentasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<CuentasProgramado> ListarAhorrosProgramado(String pFiltro, DateTime pFechaApe, Usuario vUsuario, int estadoCuenta = 0)
        {
            DbDataReader resultado;
            string sql = "";
            List<CuentasProgramado> lstConsulta = new List<CuentasProgramado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        if (estadoCuenta != 0)
                        {
                            sql = @"SELECT A.NUMERO_PROGRAMADO,A.COD_OFICINA,A.COD_LINEA_PROGRAMADO,A.COD_PERSONA,A.FECHA_APERTURA,A.FECHA_PRIMERA_CUOTA,A.FECHA_CIERRE,CALCULAR_CUOTA_PROGRAMADO(A.NUMERO_PROGRAMADO) as VALOR_CUOTA,
                        A.PLAZO,A.COD_PERIODICIDAD,H.FECHA_ULTIMO_PAGO,H.FECHA_PROXIMO_PAGO,H.SALDO_TOTAL SALDO,A.FORMA_PAGO,A.COD_EMPRESA,A.CUOTAS_PAGADAS,A.CALCULO_TASA,A.TASA_INTERES,
                        A.COD_TIPO_TASA,A.TIPO_HISTORICO,A.DESVIACION,A.FECHA_INTERES,A.TOTAL_INTERES,A.TOTAL_RETENCION,A.COD_MOTIVO_APERTURA,A.ESTADO,A.COD_ASESOR,
                        L.NOMBRE AS NOMLINEA,O.NOMBRE AS NOMOFICINA,M.DESCRIPCION AS NOMMOTIVO_PROGRA,V.IDENTIFICACION,V.NOMBRE,V.COD_NOMINA, 
                        CASE A.FORMA_PAGO WHEN 1 THEN 'CAJA' WHEN 2 THEN 'NÓMINA' END AS NOMFORMA_PAGO, 
                        CASE h.ESTADO WHEN 0 THEN 'INACTIVO' WHEN 1 THEN 'ACTIVO' WHEN 2 THEN 'TERMINADA' WHEN 3 THEN 'ANULADA' END AS NOM_ESTADO, 
                        P.DESCRIPCION AS NOM_PERIODICIDAD,FECHA_VENCIMIENTO_PROGRAMADO(A.NUMERO_PROGRAMADO) as FECHA_VENCIMIENTO,
                        SALDO_ACUMULADO(9,A.NUMERO_PROGRAMADO) AS VALOR_ACUMULADO, R.NOM_EMPRESA EMPRESA_RECA
                        FROM AHORRO_PROGRAMADO A INNER JOIN LINEAPROGRAMADO L ON L.COD_LINEA_PROGRAMADO = A.COD_LINEA_PROGRAMADO 
                        LEFT JOIN historico_programado h on h.numero_programado =  a.numero_programado
                        LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA 
                        LEFT JOIN MOTIVO_PROGRAMADO M ON M.COD_MOTIVO_APERTURA = A.COD_MOTIVO_APERTURA
                        LEFT JOIN V_PERSONA V ON A.COD_PERSONA = V.COD_PERSONA 
                        LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = A.COD_PERIODICIDAD 
                        LEFT JOIN EMPRESA_RECAUDO R ON A.COD_EMPRESA = R.COD_EMPRESA " + pFiltro;
                        }
                        else
                        {
                            sql = @"SELECT A.NUMERO_PROGRAMADO,A.COD_OFICINA,A.COD_LINEA_PROGRAMADO,A.COD_PERSONA,A.FECHA_APERTURA,A.FECHA_PRIMERA_CUOTA,A.FECHA_CIERRE,CALCULAR_CUOTA_PROGRAMADO(A.NUMERO_PROGRAMADO) as VALOR_CUOTA,
                        A.PLAZO,A.COD_PERIODICIDAD,A.FECHA_ULTIMO_PAGO,A.FECHA_PROXIMO_PAGO,A.SALDO,A.FORMA_PAGO,A.COD_EMPRESA,A.CUOTAS_PAGADAS,A.CALCULO_TASA,A.TASA_INTERES,
                        A.COD_TIPO_TASA,A.TIPO_HISTORICO,A.DESVIACION,A.FECHA_INTERES,A.TOTAL_INTERES,A.TOTAL_RETENCION,A.COD_MOTIVO_APERTURA,A.ESTADO,A.COD_ASESOR,
                        L.NOMBRE AS NOMLINEA,O.NOMBRE AS NOMOFICINA,M.DESCRIPCION AS NOMMOTIVO_PROGRA,V.IDENTIFICACION,V.NOMBRE,V.COD_NOMINA, 
                        CASE A.FORMA_PAGO WHEN 1 THEN 'CAJA' WHEN 2 THEN 'NÓMINA' END AS NOMFORMA_PAGO, 
                        CASE A.ESTADO WHEN 0 THEN 'INACTIVO' WHEN 1 THEN 'ACTIVO' WHEN 2 THEN 'TERMINADA' WHEN 3 THEN 'ANULADA' END AS NOM_ESTADO, 
                        P.DESCRIPCION AS NOM_PERIODICIDAD,FECHA_VENCIMIENTO_PROGRAMADO(A.NUMERO_PROGRAMADO) as FECHA_VENCIMIENTO,
                        SALDO_ACUMULADO(9,A.NUMERO_PROGRAMADO) AS VALOR_ACUMULADO, R.NOM_EMPRESA EMPRESA_RECA
                        FROM AHORRO_PROGRAMADO A INNER JOIN LINEAPROGRAMADO L ON L.COD_LINEA_PROGRAMADO = A.COD_LINEA_PROGRAMADO 
                        LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA 
                        LEFT JOIN MOTIVO_PROGRAMADO M ON M.COD_MOTIVO_APERTURA = A.COD_MOTIVO_APERTURA
                        LEFT JOIN V_PERSONA V ON A.COD_PERSONA = V.COD_PERSONA 
                        LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = A.COD_PERIODICIDAD 
                        LEFT JOIN EMPRESA_RECAUDO R ON A.COD_EMPRESA = R.COD_EMPRESA " + pFiltro;
                        }


                        if (pFechaApe != null && pFechaApe != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " A.FECHA_APERTURA = To_Date('" + Convert.ToDateTime(pFechaApe).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " A.FECHA_APERTURA = '" + Convert.ToDateTime(pFechaApe).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        sql += " ORDER BY A.FECHA_APERTURA DESC, A.NUMERO_PROGRAMADO DESC";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CuentasProgramado entidad = new CuentasProgramado();
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_PRIMERA_CUOTA"] != DBNull.Value) entidad.fecha_primera_cuota = Convert.ToDateTime(resultado["FECHA_PRIMERA_CUOTA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt32(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CALCULO_TASA"] != DBNull.Value) entidad.calculo_tasa = Convert.ToInt32(resultado["CALCULO_TASA"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERES"] != DBNull.Value) entidad.total_interes = Convert.ToDecimal(resultado["TOTAL_INTERES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["COD_MOTIVO_APERTURA"] != DBNull.Value) entidad.cod_motivo_apertura = Convert.ToInt32(resultado["COD_MOTIVO_APERTURA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);

                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["NOMMOTIVO_PROGRA"] != DBNull.Value) entidad.nommotivo_progra = Convert.ToString(resultado["NOMMOTIVO_PROGRA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["NOMFORMA_PAGO"] != DBNull.Value) entidad.nomforma_pago = Convert.ToString(resultado["NOMFORMA_PAGO"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["NOM_PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["NOM_PERIODICIDAD"]);
                            if (resultado["VALOR_ACUMULADO"] != DBNull.Value) entidad.valor_acumulado = Convert.ToDecimal(resultado["VALOR_ACUMULADO"]);
                            if (resultado["EMPRESA_RECA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA_RECA"]);
                            if (entidad.valor_acumulado > 0)
                            {
                                entidad.valor_total_acumu = entidad.saldo + entidad.valor_acumulado;
                            }

                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCuentasData", "ListarAhorrosProgramado", ex);
                        return null;
                    }
                }
            }
        }

        //Agregado para listar las cuentas de ahorro programdo que posea el cliente
        public List<CuentasProgramado> ListarCuentasPersona(Int64 cod_persona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CuentasProgramado> lstConsulta = new List<CuentasProgramado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT A.NUMERO_PROGRAMADO,A.COD_PERSONA, L.NOMBRE FROM AHORRO_PROGRAMADO A INNER JOIN LINEAPROGRAMADO L ON A.COD_LINEA_PROGRAMADO = L.COD_LINEA_PROGRAMADO WHERE A.COD_PERSONA = " + cod_persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuentasProgramado entidad = new CuentasProgramado();
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOMBRE"]);

                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCuentasData", "ListarCuentasPersona", ex);
                        return null;
                    }

                }
            }
        }


        public CuentasProgramado ConsultarAhorrosProgramado(String pNumeroProgramado, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasProgramado entidad = new CuentasProgramado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT A.*,L.NOMBRE AS NOMLINEA,O.NOMBRE AS NOMOFICINA,M.DESCRIPCION AS NOMMOTIVO_PROGRA,V.IDENTIFICACION,V.NOMBRE, "
                                + "CASE A.FORMA_PAGO WHEN 1 THEN 'CAJA' WHEN 2 THEN 'NÓMINA' END AS NOMFORMA_PAGO, "
                                + "CASE A.ESTADO WHEN 0 THEN 'INACTIVO' WHEN 1 THEN 'ACTIVO' WHEN 2 THEN 'TERMINADA' WHEN 3 THEN 'ANULADA' END AS NOM_ESTADO, "
                                + "P.DESCRIPCION AS NOM_PERIODICIDAD "
                                + "FROM AHORRO_PROGRAMADO A INNER JOIN LINEAPROGRAMADO L ON L.COD_LINEA_PROGRAMADO = A.COD_LINEA_PROGRAMADO "
                                + "LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA "
                                + "LEFT JOIN MOTIVO_PROGRAMADO M ON M.COD_MOTIVO_APERTURA = A.COD_MOTIVO_APERTURA "
                                + "LEFT JOIN V_PERSONA V ON A.COD_PERSONA = V.COD_PERSONA "
                                + "LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = A.COD_PERIODICIDAD WHERE NUMERO_PROGRAMADO = " + pNumeroProgramado;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_PRIMERA_CUOTA"] != DBNull.Value) entidad.fecha_primera_cuota = Convert.ToDateTime(resultado["FECHA_PRIMERA_CUOTA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt32(resultado["COD_EMPRESA"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt32(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CALCULO_TASA"] != DBNull.Value) entidad.calculo_tasa = Convert.ToInt32(resultado["CALCULO_TASA"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["TOTAL_INTERES"] != DBNull.Value) entidad.total_interes = Convert.ToDecimal(resultado["TOTAL_INTERES"]);
                            if (resultado["TOTAL_RETENCION"] != DBNull.Value) entidad.total_retencion = Convert.ToDecimal(resultado["TOTAL_RETENCION"]);
                            if (resultado["COD_MOTIVO_APERTURA"] != DBNull.Value) entidad.cod_motivo_apertura = Convert.ToInt32(resultado["COD_MOTIVO_APERTURA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);

                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["NOMOFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOMOFICINA"]);
                            if (resultado["NOMMOTIVO_PROGRA"] != DBNull.Value) entidad.nommotivo_progra = Convert.ToString(resultado["NOMMOTIVO_PROGRA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOMFORMA_PAGO"] != DBNull.Value) entidad.nomforma_pago = Convert.ToString(resultado["NOMFORMA_PAGO"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["NOM_PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["NOM_PERIODICIDAD"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCuentasData", "ConsultarAhorrosProgramado", ex);
                        return null;
                    }
                }
            }
        }

        public List<Xpinn.Ahorros.Entities.ReporteMovimiento> ListarDetalleMovimiento(String pNumeroCuenta, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Xpinn.Ahorros.Entities.ReporteMovimiento> lstMovimientos = new List<Xpinn.Ahorros.Entities.ReporteMovimiento>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_NUMERO_CUENTA = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_CUENTA.ParameterName = "P_NUMERO_CUENTA";
                        P_NUMERO_CUENTA.Direction = ParameterDirection.Input;
                        P_NUMERO_CUENTA.Value = pNumeroCuenta;
                        //P_NUMERO_CUENTA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_CUENTA);

                        DbParameter P_FECHAINI = cmdTransaccionFactory.CreateParameter();
                        P_FECHAINI.ParameterName = "P_FECHAINI";
                        P_FECHAINI.Direction = ParameterDirection.Input;

                        if (pFechaIni == null)
                            P_FECHAINI.Value = DBNull.Value;
                        else
                            // P_FECHAINI.Value = pFechaIni;
                            P_FECHAINI.Value = Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha());
                        P_FECHAINI.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHAINI);

                        DbParameter P_FECHAFIN = cmdTransaccionFactory.CreateParameter();
                        P_FECHAFIN.ParameterName = "P_FECHAFIN";
                        P_FECHAFIN.Direction = ParameterDirection.Input;

                        if (pFechaFin == null)
                            P_FECHAFIN.Value = DBNull.Value;
                        else
                            P_FECHAFIN.Value = Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha());
                        P_FECHAFIN.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHAFIN);
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PRO_MOVIMIENTO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select ac.* From temp_movimientos ac Where ac.numero_radicacion = " + pNumeroCuenta.ToString() + " Order by ac.fecha_pago,ac.cod_ope,ac.tipo_ope";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Xpinn.Ahorros.Entities.ReporteMovimiento entidad = new Xpinn.Ahorros.Entities.ReporteMovimiento();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["numero_radicacion"].ToString());
                            if (resultado["fecha_pago"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha_pago"].ToString());
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["cod_ope"].ToString());
                            if (resultado["tipo_ope"] != DBNull.Value) entidad.tipo_ope = Convert.ToString(resultado["tipo_ope"]);
                            if (resultado["tipo_tran"] != DBNull.Value) entidad.tipo_tran = Convert.ToString(resultado["tipo_tran"]);
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["tipo_mov"]);
                            if (resultado["Saldo"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["Saldo"].ToString());
                            if (resultado["num_comp"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["num_comp"]);
                            if (resultado["tipo_comp"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["tipo_comp"]);
                            if (resultado["Capital"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["Capital"]);
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);

                            lstMovimientos.Add(entidad);
                        }
                        return lstMovimientos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarDetalleMovimiento", ex);
                        return null;
                    }
                }
            }
        }


        public List<CuentasProgramado> ListarProgramadoAvencer(String pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CuentasProgramado> lstConsulta = new List<CuentasProgramado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select a.NUMERO_PROGRAMADO, a.COD_LINEA_PROGRAMADO, l.NOMBRE as NOMLINEA, p.identificacion, p.nombre as nom_persona, 
                                    o.NOMBRE as nom_oficina, a.FECHA_APERTURA, FECHA_VENCIMIENTO_PROGRAMADO(A.NUMERO_PROGRAMADO) as FECHA_VENCIMIENTO,
                                    CASE a.estado when 0 then 'INACTIVO' when 1 then 'ACTIVO' when 2 then 'TERMINADO' when 3 then 'ANULADO' end as ESTADO,
                                    a.VALOR_CUOTA, a.plazo, a.saldo
                                    from AHORRO_PROGRAMADO a inner join LINEAPROGRAMADO l on a.COD_LINEA_PROGRAMADO = l.COD_LINEA_PROGRAMADO
                                    inner join v_persona p on a.cod_persona = p.cod_persona
                                    inner join oficina o on p.cod_oficina = o.cod_oficina
                                    inner join periodicidad p on a.COD_PERIODICIDAD = p.COD_PERIODICIDAD " + pFiltro;

                        sql += " ORDER BY a.NUMERO_PROGRAMADO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        CuentasProgramado entidad;
                        while (resultado.Read())
                        {
                            entidad = new CuentasProgramado();
                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["COD_LINEA_PROGRAMADO"] != DBNull.Value) entidad.cod_linea_programado = Convert.ToString(resultado["COD_LINEA_PROGRAMADO"]);
                            if (resultado["NOMLINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOMLINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["nom_persona"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nom_persona"]);
                            if (resultado["nom_oficina"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["nom_oficina"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("MovimientoCuentasData", "ListarAhorrosProgramado", ex);
                        return null;
                    }
                }
            }
        }
        public List<CuentasProgramado> ListarAprobaciones(Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuentasProgramado> lstAhorros = new List<CuentasProgramado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT NUMERO_PROGRAMADO,M.DESCRIPCION,FECHA_APERTURA FROM AHORRO_PROGRAMADO A JOIN MOTIVO_PROGRAMADO M ON A.COD_MOTIVO_APERTURA=M.COD_MOTIVO_APERTURA WHERE A.COD_MOTIVO_APERTURA=101 order by fecha_apertura desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuentasProgramado entidad = new CuentasProgramado();

                            if (resultado["NUMERO_PROGRAMADO"] != DBNull.Value) entidad.numero_programado = Convert.ToString(resultado["NUMERO_PROGRAMADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);


                            lstAhorros.Add(entidad);
                        }
                        return lstAhorros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarAhorrosBeneficiaros", ex);
                        return null;
                    }
                }
            }
        }



    }
}
