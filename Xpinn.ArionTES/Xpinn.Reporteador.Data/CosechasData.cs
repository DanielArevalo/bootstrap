using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Data
{
    /// <summary>
    /// Objeto de acceso a datos Cosechas
    /// </summary>
    public class CosechasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para las cosechas
        /// </summary>
        public CosechasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        #region Análisis
        /// <summary>
        /// Metodo que crea la lista de los encabezados de la gridview
        /// </summary>
        /// <param name="fecha_inicial">solicitada para tener un rango de fechas</param>
        /// <param name="fecha_final">solicitada para tener un rango de fechas</param>
        /// <param name="intervalo">se solicita para saber si se genera 1=semanal, 2=mensual o 3=anualmente</param>
        /// <param name="pUsuario">se usa en general en la solución</param>
        /// <returns></returns>
        public List<string> GenerarTitulos(DateTime fecha_inicial, DateTime fecha_final, Int64 intervalo, Usuario pUsuario)
        {
            DbDataReader resultado;
            Titulos entidad = new Titulos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    connection.Open();
                    try
                    {
                        if (intervalo == 0 || intervalo >= 4)
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
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CosechasData", "GenerarTitulos", ex);
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
                        BOExcepcion.Throw("CosechasData", "GenerarTitulos", ex);
                        return null;
                    }
                }
            }
        }
        #endregion

        #region Resumen
        /// <summary>
        /// Metodo que genera la lista del primer reporte de analisis de cosechas
        /// </summary>
        /// <param name="fecha_inicial">solicitada para tener un rango de fechas</param>
        /// <param name="fecha_final">solicitada para tener un rango de fechas</param>
        /// <param name="pUsuario">se usa en general en la solución</param>
        /// <returns></returns>
        public List<Cosechas> Colocacion(DateTime fecha_inicial, DateTime fecha_final, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Cosechas> ListCosechas = new List<Cosechas>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_COLOCACION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        string sql = @"SELECT * FROM TEMP_COSECHA ORDER BY 1 ASC";
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cosechas entidad = new Cosechas();

                            if (resultado["COMPORTAMIENTO"] != DBNull.Value) entidad.stg_comportamiento = Convert.ToString(resultado["COMPORTAMIENTO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToDecimal(resultado["CANTIDAD"]);
                            if (resultado["GRUPO_LINEA"] != DBNull.Value) entidad.GrupoLinea = Convert.ToString(resultado["GRUPO_LINEA"]);
                            if (resultado["PARTICIPACION_CANTIDAD"] != DBNull.Value) entidad.Participacion_Cantidad = Convert.ToString(resultado["PARTICIPACION_CANTIDAD"]);
                            if (resultado["Participacion_valor"] != DBNull.Value) entidad.Participacion_valor = Convert.ToString(resultado["Participacion_valor"]);

                            ListCosechas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return ListCosechas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CosechasData", "Colocacion", ex);
                        return null;
                    }
                }
            }



        }
        /// <summary>
        /// Metodo usado para conocer el estado de la cartera Vencida
        /// </summary>
        /// <param name="fecha_inicial">solicitada para tener un rango de fechas</param>
        /// <param name="fecha_final">solicitada para tener un rango de fechas</param>
        /// <param name="pUsuario">se usa en general en la solución</param>
        /// <returns></returns>
        public List<Cosechas> ListarCarteraVencida(DateTime fecha_inicial, DateTime fecha_final, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cosechas> lstCarteraVen = new List<Cosechas>();

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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_CARTERAVEN";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        string sql = @"SELECT * FROM TEMP_COSECHA ORDER BY 1, 2";
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cosechas entidad = new Cosechas();
                            if (resultado["COMPORTAMIENTO"] != DBNull.Value) entidad.comportamiento = Convert.ToDateTime(resultado["COMPORTAMIENTO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["CANTIDAD"] != DBNull.Value) entidad.cantidad = Convert.ToDecimal(resultado["CANTIDAD"]);
                            if (resultado["GRUPO_LINEA"] != DBNull.Value) entidad.GrupoLinea = Convert.ToString(resultado["GRUPO_LINEA"]);

                            lstCarteraVen.Add(entidad);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCarteraVen;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CosechasData", "ListarCarteraVencida", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Metodo implementado para conocer la calidad de la cosecha
        /// </summary>
        /// <param name="fecha_inicial">solicitada para tener un rango de fechas</param>
        /// <param name="fecha_final">solicitada para tener un rango de fechas</param>
        /// <param name="pUsuario">se usa en general en la solución</param>
        /// <returns></returns>
        public List<Cosechas> ListarCalidadCosecha(DateTime fecha_inicial, DateTime fecha_final, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Cosechas> lstCarteraVen = new List<Cosechas>();

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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_CALIDADCOSE";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT * FROM TEMP_COSECHA";
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Cosechas entidad = new Cosechas();
                            if (resultado["COMPORTAMIENTO"] != DBNull.Value) entidad.comportamiento = Convert.ToDateTime(resultado["COMPORTAMIENTO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["Participacion_valor"] != DBNull.Value) entidad.Participacion_valor = Convert.ToString(resultado["VALOR"]); //.ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["PARTICIPACION_CANTIDAD"] != DBNull.Value) entidad.Participacion_Cantidad = Convert.ToString(resultado["CANTIDAD"]); //.ToString().Replace(".", conf.ObtenerSeparadorDecimalConfig()));
                            if (resultado["GRUPO_LINEA"] != DBNull.Value) entidad.GrupoLinea = Convert.ToString(resultado["GRUPO_LINEA"]);

                            lstCarteraVen.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCarteraVen;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CosechasData", "ListarCalidadCosecha", ex);
                        return null;
                    }
                }
            }
        }

        #endregion
    }
}