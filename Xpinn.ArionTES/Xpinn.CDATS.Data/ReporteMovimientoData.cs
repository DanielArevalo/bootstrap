using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Ahorro_Vista
    /// </summary>
    public class ReporteMovimientoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Ahorro_Vista
        /// </summary>
        public ReporteMovimientoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<ReporteMovimiento> ListarDropDownLineasCdat(ReporteMovimiento pReport, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteMovimiento> lstLineas = new List<ReporteMovimiento>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LINEACDAT " + ObtenerFiltro(pReport) + " ORDER BY COD_LINEACDAT";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteMovimiento entidad = new ReporteMovimiento();
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_linea_cdat = Convert.ToInt32(resultado["COD_LINEACDAT"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstLineas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteMovimientoCdatData", "ListarDropDownLineasCdats", ex);
                        return null;
                    }
                }
            }
        }
        public List<Cdat> ListarCdat(string pFiltro, DateTime pFecha, Usuario vUsuario, int estadoCuenta = 1)
        {
            DbDataReader resultado;
            string sql = "";
            List<Cdat> lstAhorroVista = new List<Cdat>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        if (estadoCuenta != 0)
                        {
                            sql = @"SELECT A.*,A.COD_LINEACDAT||' - '||L.DESCRIPCION AS NOM_LINEA,V.IDENTIFICACION,V.NOMBRE,V.COD_NOMINA, O.NOMBRE AS NOM_OFICINA , "
                                        + " CASE h.ESTADO WHEN 1 THEN 'APERTURA' WHEN 2 THEN 'ACTIVA' WHEN 3 THEN 'TERMINADO' WHEN 4 THEN 'ANULADO' WHEN 5 THEN 'EMBARGADO' END AS NOM_ESTADO , t.cod_persona ,  H.VALOR VALOR_H"
                                        + " FROM CDAT A LEFT JOIN lineacdat L ON L.COD_LINEACDAT = A.COD_LINEACDAT "
                                        + " INNER JOIN cdat_titular T ON T.CODIGO_CDAT = A.CODIGO_CDAT AND T.PRINCIPAL=1 "
                                        + "INNER JOIN HISTORICO_CDAT H ON H.NUMERO_CDAT = A.NUMERO_CDAT"
                                        + " LEFT JOIN V_PERSONA V ON V.COD_PERSONA = T.COD_PERSONA "
                                        + " LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA " + pFiltro;
                        }
                        else
                        {
                            sql = @"SELECT A.*,A.COD_LINEACDAT||' - '||L.DESCRIPCION AS NOM_LINEA,V.IDENTIFICACION,V.NOMBRE,V.COD_NOMINA, O.NOMBRE AS NOM_OFICINA , "
                                         + " CASE A.ESTADO WHEN 1 THEN 'APERTURA' WHEN 2 THEN 'ACTIVA' WHEN 3 THEN 'TERMINADO' WHEN 4 THEN 'ANULADO' WHEN 5 THEN 'EMBARGADO' END AS NOM_ESTADO , t.cod_persona "
                                         + " FROM CDAT A LEFT JOIN lineacdat L ON L.COD_LINEACDAT = A.COD_LINEACDAT "
                                         + " INNER JOIN cdat_titular T ON T.CODIGO_CDAT = A.CODIGO_CDAT AND T.PRINCIPAL=1 "
                                         + " LEFT JOIN V_PERSONA V ON V.COD_PERSONA = T.COD_PERSONA "
                                         + " LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA " + pFiltro;
                        } 


                        if (pFecha != null && pFecha != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA_APERTURA >= To_Date('" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA_APERTURA >= '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        sql += " ORDER BY A.CODIGO_CDAT";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt32(resultado["CODIGO_CDAT"]);
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_DESTINACION"] != DBNull.Value) entidad.cod_destinacion = Convert.ToInt32(resultado["COD_DESTINACION"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["CODFORMA_CAPTACION"] != DBNull.Value) entidad.codforma_captacion = Convert.ToInt32(resultado["CODFORMA_CAPTACION"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) entidad.tipo_calendario = Convert.ToInt32(resultado["TIPO_CALENDARIO"]);
                            if (resultado["VALOR_H"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR_H"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.cod_asesor_com = Convert.ToInt32(resultado["COD_ASESOR_COM"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.cod_asesor_com = Convert.ToInt32(resultado["COD_ASESOR_COM"]);
                            if (resultado["TIPO_INTERES"] != DBNull.Value) entidad.tipo_interes = Convert.ToString(resultado["TIPO_INTERES"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["COD_PERIODICIDAD_INT"] != DBNull.Value) entidad.cod_periodicidad_int = Convert.ToInt32(resultado["COD_PERIODICIDAD_INT"]);
                            if (resultado["MODALIDAD_INT"] != DBNull.Value) entidad.modalidad_int = Convert.ToInt32(resultado["MODALIDAD_INT"]);
                            if (resultado["CAPITALIZAR_INT"] != DBNull.Value) entidad.capitalizar_int = Convert.ToInt32(resultado["CAPITALIZAR_INT"]);
                            if (resultado["COBRA_RETENCION"] != DBNull.Value) entidad.cobra_retencion = Convert.ToInt32(resultado["COBRA_RETENCION"]);
                            if (resultado["TASA_NOMINAL"] != DBNull.Value) entidad.tasa_nominal = Convert.ToInt32(resultado["TASA_NOMINAL"]);
                            if (resultado["TASA_EFECTIVA"] != DBNull.Value) entidad.tasa_efectiva = Convert.ToInt32(resultado["TASA_EFECTIVA"]);
                            if (resultado["INTERESES_CAP"] != DBNull.Value) entidad.intereses_cap = Convert.ToInt32(resultado["INTERESES_CAP"]);
                            if (resultado["RETENCION_CAP"] != DBNull.Value) entidad.retencion_cap = Convert.ToInt32(resultado["RETENCION_CAP"]);
                            if (resultado["FECHA_INTERESES"] != DBNull.Value) entidad.fecha_intereses = Convert.ToDateTime(resultado["FECHA_INTERESES"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["DESMATERIALIZADO"] != DBNull.Value) entidad.desmaterializado = Convert.ToInt32(resultado["DESMATERIALIZADO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);

                            lstAhorroVista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteMovimientoCdatData", "ListarCdat", ex);
                        return null;
                    }
                }
            }
        }

        public List<Cdat> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cdat> lstCDAT = new List<Cdat>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT C.NUMERO_CDAT, T.COD_PERSONA, L.DESCRIPCION, C.CODIGO_CDAT
                                        FROM CDAT C 
                                        INNER JOIN CDAT_TITULAR T ON C.CODIGO_CDAT = T.CODIGO_CDAT 
                                        INNER JOIN LINEACDAT L ON C.COD_LINEACDAT = L.COD_LINEACDAT WHERE T.COD_PERSONA = " + pCod_Persona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Cdat entidad = new Cdat();
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt32(resultado["CODIGO_CDAT"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["DESCRIPCION"]);

                            lstCDAT.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCDAT;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteMovimientoCdatData", "ListarCuentasPersona", ex);
                        return null;
                    }
                }
            }
        }


        public Cdat ConsultarCdat(string pNumero_cuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            Cdat entidad = new Cdat();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT A.*,A.COD_LINEACDAT||' - '||L.DESCRIPCION AS NOM_LINEA,V.IDENTIFICACION,V.NOMBRE, O.NOMBRE AS NOM_OFICINA , "
                                  + "CASE A.ESTADO WHEN 1 THEN 'APERTURA' WHEN 2 THEN 'ACTIVO' WHEN 3 THEN 'TERMINADO' WHEN 4 THEN 'ANULADO' WHEN 5 THEN 'EMBARGADA' END AS NOM_ESTADO , t.cod_persona "
                                  + " FROM CDAT A LEFT JOIN lineacdat L ON L.COD_LINEACDAT = A.COD_LINEACDAT "
                                  + " INNER JOIN cdat_titular T ON T.CODIGO_CDAT = A.CODIGO_CDAT AND T.PRINCIPAL=1 "
                                  + "LEFT JOIN V_PERSONA V ON V.COD_PERSONA = T.COD_PERSONA "
                                  + "LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA where  A.CODIGO_CDAT = " + pNumero_cuenta.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO_CDAT"] != DBNull.Value) entidad.codigo_cdat = Convert.ToInt32(resultado["CODIGO_CDAT"]);
                            if (resultado["COD_LINEACDAT"] != DBNull.Value) entidad.cod_lineacdat = Convert.ToString(resultado["COD_LINEACDAT"]);
                            if (resultado["NUMERO_CDAT"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["NUMERO_CDAT"]);
                            if (resultado["NUMERO_FISICO"] != DBNull.Value) entidad.numero_fisico = Convert.ToString(resultado["NUMERO_FISICO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_DESTINACION"] != DBNull.Value) entidad.cod_destinacion = Convert.ToInt32(resultado["COD_DESTINACION"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["CODFORMA_CAPTACION"] != DBNull.Value) entidad.codforma_captacion = Convert.ToInt32(resultado["CODFORMA_CAPTACION"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) entidad.tipo_calendario = Convert.ToInt32(resultado["TIPO_CALENDARIO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["COD_MONEDA"] != DBNull.Value) entidad.cod_moneda = Convert.ToInt32(resultado["COD_MONEDA"]);
                            if (resultado["FECHA_INICIO"] != DBNull.Value) entidad.fecha_inicio = Convert.ToDateTime(resultado["FECHA_INICIO"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.cod_asesor_com = Convert.ToInt32(resultado["COD_ASESOR_COM"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.cod_asesor_com = Convert.ToInt32(resultado["COD_ASESOR_COM"]);
                            if (resultado["TIPO_INTERES"] != DBNull.Value) entidad.tipo_interes = Convert.ToString(resultado["TIPO_INTERES"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt32(resultado["COD_TIPO_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["COD_PERIODICIDAD_INT"] != DBNull.Value) entidad.cod_periodicidad_int = Convert.ToInt32(resultado["COD_PERIODICIDAD_INT"]);
                            if (resultado["MODALIDAD_INT"] != DBNull.Value) entidad.modalidad_int = Convert.ToInt32(resultado["MODALIDAD_INT"]);
                            if (resultado["CAPITALIZAR_INT"] != DBNull.Value) entidad.capitalizar_int = Convert.ToInt32(resultado["CAPITALIZAR_INT"]);
                            if (resultado["COBRA_RETENCION"] != DBNull.Value) entidad.cobra_retencion = Convert.ToInt32(resultado["COBRA_RETENCION"]);
                            if (resultado["TASA_NOMINAL"] != DBNull.Value) entidad.tasa_nominal = Convert.ToInt32(resultado["TASA_NOMINAL"]);
                            if (resultado["TASA_EFECTIVA"] != DBNull.Value) entidad.tasa_efectiva = Convert.ToInt32(resultado["TASA_EFECTIVA"]);
                            if (resultado["INTERESES_CAP"] != DBNull.Value) entidad.intereses_cap = Convert.ToInt32(resultado["INTERESES_CAP"]);
                            if (resultado["RETENCION_CAP"] != DBNull.Value) entidad.retencion_cap = Convert.ToInt32(resultado["RETENCION_CAP"]);
                            if (resultado["FECHA_INTERESES"] != DBNull.Value) entidad.fecha_intereses = Convert.ToDateTime(resultado["FECHA_INTERESES"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["DESMATERIALIZADO"] != DBNull.Value) entidad.desmaterializado = Convert.ToInt32(resultado["DESMATERIALIZADO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nomlinea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nomoficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteMovimientoCdatData", "ConsultarCdat", ex);
                        return null;
                    }
                }
            }
        }

        public List<ReporteMovimiento> ListarReporteMovimiento(Int64 pNumeroCuenta, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ReporteMovimiento> lstMovimientos = new List<ReporteMovimiento>();

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
                        P_NUMERO_CUENTA.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_CUENTA);

                        DbParameter P_FECHAINI = cmdTransaccionFactory.CreateParameter();
                        P_FECHAINI.ParameterName = "P_FECHAINI";
                        P_FECHAINI.Direction = ParameterDirection.Input;
                        if (pFechaIni != DateTime.MinValue) P_FECHAINI.Value = pFechaIni; else P_FECHAINI.Value = DBNull.Value;
                        P_FECHAINI.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHAINI);

                        DbParameter P_FECHAFIN = cmdTransaccionFactory.CreateParameter();
                        P_FECHAFIN.ParameterName = "P_FECHAFIN";
                        P_FECHAFIN.Direction = ParameterDirection.Input;
                        if (pFechaFin != DateTime.MinValue) P_FECHAFIN.Value = pFechaFin; else P_FECHAFIN.Value = DBNull.Value;
                        P_FECHAFIN.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(P_FECHAFIN);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CDA_REPMOVIMIENTO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select ac.* From temp_movimientos ac Where ac.numero_radicacion = " + pNumeroCuenta.ToString() + " Order by  fecha_pago,ac.TIPO_TRAN";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ReporteMovimiento entidad = new ReporteMovimiento();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_cdat = Convert.ToString(resultado["numero_radicacion"].ToString());
                            if (resultado["fecha_pago"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha_pago"].ToString());
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["cod_ope"].ToString());
                            if (resultado["tipo_ope"] != DBNull.Value) entidad.tipo_ope = Convert.ToString(resultado["tipo_ope"]);
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["tipo_mov"]);
                            if (resultado["Saldo"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["Saldo"].ToString());
                            if (resultado["num_comp"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["num_comp"]);
                            if (resultado["tipo_comp"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["tipo_comp"]);
                            if (resultado["Capital"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["Capital"]);
                            if (resultado["documento_soporte"] != DBNull.Value) entidad.soporte = Convert.ToString(resultado["documento_soporte"]);

                            lstMovimientos.Add(entidad);
                        }
                        return lstMovimientos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteMovimientoCdatData", "ListarReporteMovimiento", ex);
                        return null;
                    }
                }
            }
        }



    }
}
