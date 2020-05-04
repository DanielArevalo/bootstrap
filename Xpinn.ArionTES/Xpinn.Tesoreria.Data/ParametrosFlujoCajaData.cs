using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    public class ParametrosFlujoCajaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ParametrosFlujoCajaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public ParametrosFlujoCaja CrearConcepto(ParametrosFlujoCaja pConceptoFlujo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        pcod_concepto.Value = 0;
                        pcod_concepto.Direction = ParameterDirection.Output;
                        pcod_concepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pConceptoFlujo.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo_concepto = cmdTransaccionFactory.CreateParameter();
                        ptipo_concepto.ParameterName = "p_tipo_concepto";
                        ptipo_concepto.Value = pConceptoFlujo.tipo_concepto;
                        ptipo_concepto.Direction = ParameterDirection.Input;
                        ptipo_concepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_concepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_REPORTE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pConceptoFlujo.cod_concepto = Convert.ToInt64(pcod_concepto.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pConceptoFlujo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosFlujoCajaData", "CrearConceptoReporte", ex);
                        return null;
                    }

                }
            }
        }

        public ParametrosFlujoCaja ModificarConcepto(ParametrosFlujoCaja pConceptoFlujo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        pcod_concepto.Value = pConceptoFlujo.cod_concepto;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pConceptoFlujo.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptipo_concepto = cmdTransaccionFactory.CreateParameter();
                        ptipo_concepto.ParameterName = "p_tipo_concepto";
                        ptipo_concepto.Value = pConceptoFlujo.tipo_concepto;
                        ptipo_concepto.Direction = ParameterDirection.Input;
                        ptipo_concepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(ptipo_concepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_REPORTE_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pConceptoFlujo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosFlujoCajaData", "ModificarConcepto", ex);
                        return null;
                    }
                }
            }
        }

        public ParametrosFlujoCaja CrearConceptoCuenta(ParametrosFlujoCaja pConceptoCuenta, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_cuenta_con = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_con.ParameterName = "p_cod_cuenta_con";
                        pcod_cuenta_con.Value = pConceptoCuenta.cod_cuenta_con;
                        pcod_cuenta_con.Direction = ParameterDirection.Output;
                        pcod_cuenta_con.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_con);

                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        pcod_concepto.Value = pConceptoCuenta.cod_concepto;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter pcuenta = cmdTransaccionFactory.CreateParameter();
                        pcuenta.ParameterName = "p_cuenta";
                        pcuenta.Value = pConceptoCuenta.cod_cuenta;
                        pcuenta.Direction = ParameterDirection.Input;
                        pcuenta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        ptipo_mov.Value = pConceptoCuenta.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CUENTA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pConceptoCuenta.cod_concepto = Convert.ToInt64(pcod_concepto.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pConceptoCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosFlujoCajaData", "CrearConceptoCuenta", ex);
                        return null;
                    }

                }
            }
        }

        public ParametrosFlujoCaja ConsultarConceptoCuenta(Int64 cod_concepto, Usuario vUsuario)
        {
            DbDataReader resultado;
            ParametrosFlujoCaja entidad = new ParametrosFlujoCaja();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CONCEPTO_REPORTE.*, CASE TIPO_CONCEPTO WHEN 1 THEN 'Ingreso' WHEN 2 THEN 'Egreso' WHEN 3 THEN 'Otros Egresos' 
                                      END AS NOM_TIPO_CON FROM CONCEPTO_REPORTE WHERE COD_CONCEPTO = " + cod_concepto;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToInt64(resultado["COD_CONCEPTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_CONCEPTO"] != DBNull.Value) entidad.tipo_concepto = Convert.ToInt64(resultado["TIPO_CONCEPTO"]);
                            if (resultado["NOM_TIPO_CON"] != DBNull.Value) entidad.nom_tipo_concepto = Convert.ToString(resultado["NOM_TIPO_CON"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosFlujoCajaData", "ConsultarConceptoCta", ex);
                        return null;
                    }
                }
            }
        }


        public List<ParametrosFlujoCaja> ListarConceptos(ParametrosFlujoCaja vConcepto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ParametrosFlujoCaja> lstConceptos = new List<ParametrosFlujoCaja>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CONCEPTO_REPORTE.*, CASE TIPO_CONCEPTO WHEN 1 THEN 'Ingreso' WHEN 2 THEN 'Egreso' WHEN 3 THEN 'Otros Egresos' WHEN 4 THEN 'Saldo Caja' 
                                      END AS NOM_TIPO_CON FROM CONCEPTO_REPORTE " + ObtenerFiltro(vConcepto) + " ORDER BY COD_CONCEPTO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ParametrosFlujoCaja entidad = new ParametrosFlujoCaja();

                            if (resultado["COD_CONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToInt64(resultado["COD_CONCEPTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_CONCEPTO"] != DBNull.Value) entidad.tipo_concepto = Convert.ToInt64(resultado["TIPO_CONCEPTO"]);
                            if (resultado["NOM_TIPO_CON"] != DBNull.Value) entidad.nom_tipo_concepto = Convert.ToString(resultado["NOM_TIPO_CON"]);
                            lstConceptos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConceptos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosFlujoCaja", "ListarConceptos", ex);
                        return null;
                    }
                }
            }
        }

        public List<ParametrosFlujoCaja> ListarCuentas(Int64 cod_concepto, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ParametrosFlujoCaja> lstCuentas = new List<ParametrosFlujoCaja>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT C.COD_CUENTA_CON, P.COD_CUENTA, P.NOMBRE AS NOM_CUENTA, C.TIPO_MOV  FROM V_PLAN_CUENTAS P LEFT JOIN CONCEPTO_CUENTA C ON C.COD_CUENTA = P.COD_CUENTA
                                       WHERE C.COD_CONCEPTO = " + cod_concepto + " ORDER BY C.COD_CUENTA_CON ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ParametrosFlujoCaja entidad = new ParametrosFlujoCaja();
                            if (resultado["COD_CUENTA_CON"] != DBNull.Value) entidad.cod_cuenta_con = Convert.ToInt64(resultado["COD_CUENTA_CON"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToInt64(resultado["COD_CUENTA"]);
                            if (resultado["NOM_CUENTA"] != DBNull.Value) entidad.nom_cuenta = Convert.ToString(resultado["NOM_CUENTA"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["TIPO_MOV"]);
                            lstCuentas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosFlujoCaja", "ListarCuentas", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarConcepto(Int64 cod_concepto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        pcod_concepto.Value = cod_concepto;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_REPORTE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosFlujoCajaData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        public void EliminarConceptoCuenta(Int64 cod_cuenta_con, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_cuenta_con";
                        pcod_concepto.Value = cod_cuenta_con;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CUENTA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosFlujoCajaData", "EliminarConceptoCuenta", ex);
                    }
                }
            }
        }

        public List<ParametrosFlujoCaja> ListarConceptosReporte(DateTime fecha_inicial, DateTime fecha_final, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ParametrosFlujoCaja> lstConceptos = new List<ParametrosFlujoCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    connection.Open();
                    try
                    {
                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "p_fecha_inicial";
                        pfecha_inicial.Value = fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        pfecha_final.Value = fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REPORTE_FLUJO_CAJ";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DatosDeDocumentoData", "ListarDatosDeDocumentoFormato", ex);
                        return null;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        string sql = @"SELECT CONCEPTOS_FLUJO_CAJA.*, 
                                        CASE TIPO_CONCEPTO WHEN 1 THEN 'Ingreso' WHEN 2 THEN 'Egreso' WHEN 3 THEN 'Otros Egresos' WHEN 4 THEN 'Saldo Caja' END AS NOM_TIPO_CON
                                        FROM CONCEPTOS_FLUJO_CAJA ORDER BY TIPO_CONCEPTO";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ParametrosFlujoCaja entidad = new ParametrosFlujoCaja();
                            if (resultado["TIPO_CONCEPTO"] != DBNull.Value) entidad.tipo_concepto = Convert.ToInt64(resultado["TIPO_CONCEPTO"]);
                            if (resultado["NOM_TIPO_CON"] != DBNull.Value) entidad.nom_tipo_concepto = Convert.ToString(resultado["NOM_TIPO_CON"]);
                            if (resultado["COD_CONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToInt64(resultado["COD_CONCEPTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALORES"] != DBNull.Value) entidad.valores = Convert.ToString(resultado["VALORES"]);
                            if (resultado["FECHAS"] != DBNull.Value) entidad.fechas = Convert.ToString(resultado["FECHAS"]);

                            lstConceptos.Add(entidad);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConceptos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosFlujoCaja", "ListarConceptosReporte", ex);
                        return null;
                    }
                }
            }
        }

        public List<string> ListarTitulos(DateTime fecha_inicial, DateTime fecha_final, Usuario vUsuario)
        {
            DbDataReader resultado;
            
            ParametrosFlujoCaja entidad = new ParametrosFlujoCaja();
            List<ParametrosFlujoCaja> lstIngresos = new List<ParametrosFlujoCaja>();
            List<ParametrosFlujoCaja> lstEgresos = new List<ParametrosFlujoCaja>();
            List<ParametrosFlujoCaja> lstOtrosEgresos = new List<ParametrosFlujoCaja>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                Int64 intervalo = 0;
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    connection.Open();
                    try
                    {
                        string sql = @"SELECT DIFERENCIA_MESES(TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') , TO_DATE('" + fecha_final.ToShortDateString() + @"','dd/mm/yyyy')) AS INTERVALO from dual";

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["INTERVALO"] != DBNull.Value) intervalo = Convert.ToInt64(resultado["INTERVALO"]);
                        }

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosFlujoCaja", "ListarTitulos", ex);
                        return null;
                    }
                }

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    cmdTransaccionFactory.Connection = connection;
                    try
                    {
                        List<string> lstTitulos = new List<string>();
                        string sql = "";
                        if (intervalo == 1) //Listado por dias
                            sql = @"SELECT EXTRACT (YEAR FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1) AS ANIO, TRIM(TO_CHAR(TO_DATE(TO_CHAR(EXTRACT (MONTH FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1)),'mm'),'Month', 'NLS_DATE_LANGUAGE = SPANISH')) AS MES, EXTRACT (DAY FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1) AS DIA 
                                    FROM DUAL
                                    CONNECT BY LEVEL <= (SELECT TRUNC(TO_DATE('" + fecha_final.ToShortDateString() + @"','dd/mm/yyyy') - TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy')) FROM DUAL) 
                                    GROUP BY EXTRACT(YEAR FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1), EXTRACT(MONTH FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1), EXTRACT(DAY FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1)
                                    ORDER BY ANIO, EXTRACT (MONTH FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1), DIA";
                        else if (intervalo == 2) //Listado por meses
                            sql = @"SELECT TRIM(TO_CHAR(TO_DATE(TO_CHAR(EXTRACT (MONTH FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1)),'mm'),'Month', 'NLS_DATE_LANGUAGE = SPANISH')) AS MES, EXTRACT (YEAR FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1) AS ANIO
                                    FROM DUAL
                                    CONNECT BY LEVEL <= (SELECT TRUNC(TO_DATE('" + fecha_final.ToShortDateString() + @"','dd/mm/yyyy') - TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy')) FROM DUAL) 
                                    GROUP BY EXTRACT(MONTH FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1), EXTRACT(YEAR FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1)
                                    ORDER BY ANIO , EXTRACT (MONTH FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM - 1)";
                        else if (intervalo == 3) //Listado por años
                            sql = @"SELECT EXTRACT (YEAR FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM-1) AS ANIO 
                                        FROM DUAL
                                        CONNECT BY LEVEL <= (SELECT TRUNC(TO_DATE('" + fecha_final.ToShortDateString() + @"','dd/mm/yyyy')- TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy')) FROM DUAL)
                                        GROUP BY EXTRACT (YEAR FROM TO_DATE('" + fecha_inicial.ToShortDateString() + @"','dd/mm/yyyy') + ROWNUM-1)
                                        ORDER BY ANIO ";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (intervalo == 1)
                            {
                                if (resultado["DIA"] != DBNull.Value) entidad.dia = Convert.ToInt64(resultado["DIA"]);
                                if (resultado["MES"] != DBNull.Value) entidad.mes = Convert.ToString(resultado["MES"]).Trim();
                                if (resultado["ANIO"] != DBNull.Value) entidad.anio = Convert.ToInt64(resultado["ANIO"]);
                            }
                            else if (intervalo == 2)
                            {
                                if (resultado["MES"] != DBNull.Value) entidad.mes = Convert.ToString(resultado["MES"]).Trim();
                                if (resultado["ANIO"] != DBNull.Value) entidad.anio = Convert.ToInt64(resultado["ANIO"]);
                            }
                            else if (intervalo == 3)
                                if (resultado["ANIO"] != DBNull.Value) entidad.anio = Convert.ToInt64(resultado["ANIO"]);

                            if (intervalo == 1)
                                entidad.titulos = entidad.mes + "-" + entidad.dia + "-" + entidad.anio;
                            else if (intervalo == 2)
                                entidad.titulos = entidad.mes + "-" + entidad.anio;
                            else if (intervalo == 3)
                                entidad.titulos = entidad.anio.ToString();

                            lstTitulos.Add(entidad.titulos);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTitulos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametrosFlujoCaja", "ListarTitulos", ex);
                        return null;
                    }
                }
            }
        }
    }
}
