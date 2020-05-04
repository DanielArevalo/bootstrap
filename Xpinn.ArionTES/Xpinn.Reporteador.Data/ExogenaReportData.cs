using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Reporteador.Entities;
using System.Globalization;

namespace Xpinn.Reporteador.Data
{
    public class ExogenaReportData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ExogenaReportData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<ExogenaReport> FormatoAhorros(ExogenaReport exo, int Año, Usuario vUsuario, ref string pError, int Cuantia)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<ExogenaReport> lstExogena1019 = new List<ExogenaReport>();
            string fecha = "01/01/" + Año.ToString();
            DateTime FechaInicial = Convert.ToDateTime(fecha);
            fecha = "31/12/" + Año.ToString();
            DateTime FechaFinal = Convert.ToDateTime(fecha);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfechaini = cmdTransaccionFactory.CreateParameter();
                        pfechaini.ParameterName = "FechaInicial";
                        pfechaini.Value = FechaInicial;
                        pfechaini.Direction = ParameterDirection.Input;
                        pfechaini.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechaini);

                        DbParameter pfechafin = cmdTransaccionFactory.CreateParameter();
                        pfechafin.ParameterName = "FechaFinal";
                        pfechafin.Value = FechaFinal;
                        pfechafin.Direction = ParameterDirection.Input;
                        pfechafin.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechafin);


                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = exo.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        DbParameter pCUANTIAS = cmdTransaccionFactory.CreateParameter();
                        pCUANTIAS.ParameterName = "pCUANTIAS";
                        pCUANTIAS.Value = Cuantia;

                        pCUANTIAS.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(pCUANTIAS);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_EXO_REPORT";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();
                        string sql;
                        if (Cuantia == 1)
                        {
                            sql = "Select * from TEMP_EXOGENA order by IDLINEA";
                        }
                        else
                        {
                            sql = "Select * from TEMP_EXOGENA_MENORES order by IDLINEA";
                        }
                        //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ExogenaReport entidad = new ExogenaReport();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstExogena1019.Add(entidad);
                        }

                        return lstExogena1019;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "FormatoAhorros", ex);
                        return null;
                    }
                }
            }
        }
        public List<ExogenaReport> FormatoAhorros1019(ExogenaReport exo, string FechaInicial, string FechaFinal, Usuario vUsuario, ref string pError, int Cuantia)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<ExogenaReport> lstExogena1019 = new List<ExogenaReport>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfechaini = cmdTransaccionFactory.CreateParameter();
                        pfechaini.ParameterName = "FechaInicial";
                        pfechaini.Value = FechaInicial;
                        pfechaini.Direction = ParameterDirection.Input;
                        pfechaini.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechaini);

                        DbParameter pfechafin = cmdTransaccionFactory.CreateParameter();
                        pfechafin.ParameterName = "FechaFinal";
                        pfechafin.Value = FechaFinal;
                        pfechafin.Direction = ParameterDirection.Input;
                        pfechafin.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechafin);


                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "PSEPARADOR";
                        pseparador.Value = "|";
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        DbParameter pCUANTIAS = cmdTransaccionFactory.CreateParameter();
                        pCUANTIAS.ParameterName = "pCUANTIAS";
                        pCUANTIAS.Value = Cuantia;

                        pCUANTIAS.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(pCUANTIAS);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_EXO_REPORT";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        //BOExcepcion.Throw("ConfecoopData", "USP_XPINN_SUPER_PUC", ex);
                        return null;
                    }
                }


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql;
                        if (Cuantia == 1)
                        {
                            sql = "Select * from TEMP_EXOGENA order by IDLINEA";
                        }
                        else
                        {
                            sql = "Select  * from TEMP_EXOGENA_MENORES order by IDLINEA";
                        } //Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta"                       

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ExogenaReport entidad = new ExogenaReport();

                            if (resultado["IDLINEA"] != DBNull.Value) entidad.idlinea = Convert.ToInt32(resultado["IDLINEA"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);

                            lstExogena1019.Add(entidad);
                        }

                        return lstExogena1019;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "FormatoAhorros", ex);
                        return null;
                    }
                }
            }
        }
        public int Validar_Saldo_Mensual(string Numero_cuenta, int Año, Usuario vUsuario)
        {
            DbDataReader resultado;
            int lstConsulta = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        int añoant = Año;


                        string sql = @"SELECT VALIDAR_SALDO_MES('" + Numero_cuenta + "',TO_DATE('01/01/" + añoant + "','DD/MM/YYYY'),TO_DATE('31/12/" + añoant + "','DD/MM/YYYY')) as Resultado FROM DUAL";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["Resultado"] != DBNull.Value) lstConsulta = Convert.ToInt32(resultado["Resultado"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }

            }
        }

        public Int64 Saldo_Total(string Numero_cuenta, int Año, Usuario vUsuario)
        {
            DbDataReader resultado;
            Int64 lstConsulta = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        int añoant = Año;


                        string sql = @"SELECT   ((SELECT SUM(VALOR) FROM TRAN_AHORRO ta JOIN Operacion op ON ta.cod_ope=op.cod_ope
                                        WHERE NUMERO_CUENTA= " + Numero_cuenta + " AND TIPO_TRAN = 203 AND op.fecha_oper BETWEEN TO_DATE('01/01/" + añoant + "','DD/MM/YYYY') AND TO_DATE('31/12/" + añoant + "','DD/MM/YYYY') GROUP BY ta.numero_cuenta)-(SELECT SUM(VALOR) FROM TRAN_AHORRO ta JOIN Operacion op ON ta.cod_ope=op.cod_ope " +
                                        @" WHERE NUMERO_CUENTA=" + Numero_cuenta + " AND TIPO_TRAN=206 AND op.fecha_oper BETWEEN TO_DATE('01/01/" + añoant + "','DD/MM/YYYY') AND TO_DATE('31/12/" + añoant + "','DD/MM/YYYY') group by ta.numero_cuenta)) as Resultado FROM DUAL";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["Resultado"] != DBNull.Value) lstConsulta = Convert.ToInt64(resultado["Resultado"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch //(Exception ex)
                    {
                        return 0;
                    }
                }

            }
        }

        public DataTable FormatoAporte(int Año, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        string sql = @"SELECT DISTINCT TI.COD_DIAN,P.IDENTIFICACION,P.DIGITO_VERIFICACION,P.PRIMER_APELLIDO,P.SEGUNDO_APELLIDO,P.PRIMER_NOMBRE,p.segundo_nombre,p.razon_social,
                                        P.DIRECCION,(SELECT RTRIM(CODCIUDAD,'000') FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA)) AS COD_DPTO,
                                        CHR(39)||to_char((SELECT substr(CODCIUDAD,-3,3) FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA),'000') AS COD_MUNICIPIO,
                                        (SELECT CODCIUDAD FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES
                                        WHERE CODCIUDAD=(SELECT depende_de FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA))) AS COD_PAIS,
                                        sum(HA.saldo) AS Saldo,NVL( (SELECT sum(valor_a_distribuir) FROM revalorizar WHERE numero_aporte=ha.numero_aporte
                                        AND fecha BETWEEN to_date('01/01/" + Año + "','DD/MM/YYYY') AND to_date('31/12/" + Año + "','DD/MM/YYYY')),0) AS PORCENTAJE_PARTICIPACION, " +
                                       @" ROUND((NVL((SELECT sum(valor_a_distribuir) FROM revalorizar WHERE numero_aporte = ha.numero_aporte
                                        AND fecha BETWEEN  to_date('01/01/" + Año + @"','DD/MM/YYYY') AND to_date('31/12/" + Año + @"','DD/MM/YYYY')), 0) * 100)/(CASE (SELECT
                                        SUM(PORCENTAJE_PARTICIPACION) AS P FROM(
                                        SELECT 
                                        sum(HA.saldo)AS Saldo, NVL((SELECT sum(valor_a_distribuir) FROM revalorizar WHERE numero_aporte = ha.numero_aporte
                                        AND fecha BETWEEN to_date('01/01/" + Año + @"','DD/MM/YYYY') AND to_date('31/12/" + Año + @"','DD/MM/YYYY')), 0) AS PORCENTAJE_PARTICIPACION
                                         FROM aporte Ap INNER JOIN PERSONA P ON ap.COD_PERSONA = P.COD_PERSONA
                                        INNER JOIN TIPOIDENTIFICACION TI ON P.TIPO_IDENTIFICACION = TI.CODTIPOIDENTIFICACION INNER JOIN PERSONA_AFILIACION PA ON pa.cod_persona = p.cod_persona
                                        JOIN HISTORICO_APORTE HA ON HA.NUMERO_APORTE = AP.NUMERO_APORTE JOIN LINEAAPORTE LA ON AP.COD_LINEA_APORTE = LA.COD_LINEA_APORTE
                                        WHERE HA.FECHA_HISTORICO = to_date('31/12/" + Año + @"','DD/MM/YYYY') AND LA.TIPO_APORTE = 1
                                        GROUP BY TI.COD_DIAN,P.IDENTIFICACION,P.PRIMER_APELLIDO,P.SEGUNDO_APELLIDO,P.PRIMER_NOMBRE,p.segundo_nombre,p.razon_social,
                                        P.DIRECCION,CODCIUDADRESIDENCIA,P.DIGITO_VERIFICACION,ha.numero_aporte having SUM(HA.SALDO) >= (SELECT VALOR FROM GENERAL WHERE CODIGO = 90172))) WHEN 0 THEN 1 END),2) AS Por_Parti_Decimal " +
                                        @" FROM aporte Ap INNER JOIN PERSONA P ON ap.COD_PERSONA=P.COD_PERSONA
                                        INNER JOIN TIPOIDENTIFICACION TI ON P.TIPO_IDENTIFICACION=TI.CODTIPOIDENTIFICACION INNER JOIN PERSONA_AFILIACION PA ON pa.cod_persona=p.cod_persona
                                        JOIN HISTORICO_APORTE HA ON HA.NUMERO_APORTE=AP.NUMERO_APORTE JOIN LINEAAPORTE LA ON AP.COD_LINEA_APORTE=LA.COD_LINEA_APORTE
                                        WHERE HA.FECHA_HISTORICO=to_date('31/12/" + Año + "','DD/MM/YYYY') AND LA.TIPO_APORTE=1 " + @" 
                                        GROUP BY TI.COD_DIAN,P.IDENTIFICACION,P.PRIMER_APELLIDO,P.SEGUNDO_APELLIDO,P.PRIMER_NOMBRE,p.segundo_nombre,p.razon_social,
                                        P.DIRECCION,CODCIUDADRESIDENCIA,P.DIGITO_VERIFICACION,ha.numero_aporte having SUM(HA.SALDO)>=(SELECT VALOR FROM GENERAL WHERE CODIGO = 90172) ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader());

                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "FormatoAporte", ex);
                        return null;
                    }
                }
            }
        }
        public DataTable FormatoCDAT(string Inicio, string final, Usuario vUsuario)
        {
            DbDataReader resultado;

            // Determinar nombre del datatable según el mes
            DateTime Fecha = Convert.ToDateTime(Inicio);
            string Nombre_mes = MonthName(Fecha.Month);
            DataTable dtResultado = new DataTable(Nombre_mes);

            // Determinar fecha del período anterior
            DateTime fecPerAnt = Convert.ToDateTime(Inicio).AddDays(-1);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = @"SELECT Distinct TI.COD_DIAN, P.IDENTIFICACION, P.DIGITO_VERIFICACION, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO, P.PRIMER_NOMBRE, p.SEGUNDO_NOMBRE, p.RAZON_SOCIAL, P.DIRECCION,
                                        TO_CHAR( (SELECT RTRIM(CODCIUDAD,'000') FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD = P.CODCIUDADRESIDENCIA))) AS COD_DPTO,
                                        CHR(39) || (SELECT substr(CODCIUDAD,-3,3) FROM CIUDADES WHERE CODCIUDAD = P.CODCIUDADRESIDENCIA) AS COD_MUNICIPIO,
                                        (SELECT CODCIUDAD FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD = P.CODCIUDADRESIDENCIA))) AS COD_PAIS,
                                        c.NUMERO_CDAT, 3 AS Tipo_Titulo,
                                        CASE  WHEN EXISTS(SELECT codigo_cdat FROM prorroga_cdat WHERE c.codigo_cdat = codigo_cdat And fecha_inicio <= TO_DATE('" + final + "','" + conf.ObtenerFormatoFecha() + "') And fecha_final >= TO_DATE('" + final + "','" + conf.ObtenerFormatoFecha() + @"') 
                                          and not exists (SELECT t.codigo_cdat FROM tran_cdat t JOIN operacion o ON t.cod_ope = o.cod_ope WHERE o.tipo_ope = 11  AND Trunc(o.fecha_oper) between TO_DATE('" + Inicio + "','" + conf.ObtenerFormatoFecha() + @"') and TO_DATE('" + final + "','" + conf.ObtenerFormatoFecha() + @"')
                                                            AND t.codigo_cdat = C.CODIGO_CDAT
                                                            GROUP BY t.codigo_cdat)) THEN '2'
                                              WHEN (c.fecha_apertura between  To_date('" + Inicio + "','" + conf.ObtenerFormatoFecha() + @"') and  TO_DATE('" + final + "','" + conf.ObtenerFormatoFecha() + @"')) then '1' 
                                              WHEN (HC.ESTADO = 1 OR HC.ESTADO = 2) THEN '4' 
                                              ELSE (CASE  WHEN NOT EXISTS ( SELECT t.codigo_cdat, MAX(Trunc(o.fecha_oper)) AS fecha_cierre FROM tran_cdat t JOIN operacion o ON t.cod_ope = o.cod_ope WHERE o.tipo_ope = 11 AND Trunc(o.fecha_oper) between TO_DATE('" + Inicio + "','" + conf.ObtenerFormatoFecha() + @"') and TO_DATE('" + final + "','" + conf.ObtenerFormatoFecha() + @"') AND codigo_cdat = C.CODIGO_CDAT 
                                                                            GROUP BY t.codigo_cdat) THEN '4' ELSE '3' END) END  AS Tipo_Mtvo, h.VALOR, 
                                        (SELECT NVL(SUM(T.VALOR), 0) FROM TRAN_CDAT T INNER JOIN OPERACION O ON T.COD_OPE = O.COD_OPE WHERE T.TIPO_TRAN = 301 AND T.CODIGO_CDAT = C.CODIGO_CDAT AND TRUNC(O.FECHA_OPER) BETWEEN TO_DATE('" + Inicio + "','" + conf.ObtenerFormatoFecha() + @"') and TO_DATE('" + final + "','" + conf.ObtenerFormatoFecha() + @"')) AS VR_INVERSION, 
                                        (SELECT SUM(NVL(H.INTERES_CAU_ACUM, CA.VALOR_ACUMULADO))
                                            FROM HISTORICO_CDAT h 
                                            LEFT JOIN CAUSACION_CDAT ca ON CA.CODIGO_CDAT = H.CODIGO_CDAT AND CA.FECHA_CAUSACION = H.FECHA_HISTORICO  
                                            WHERE H.FECHA_HISTORICO = HC.FECHA_HISTORICO AND H.CODIGO_CDAT = HC.CODIGO_CDAT) AS VR_INTERES_CAUSADO,
                                        NVL((SELECT SUM(T.VALOR)
                                        FROM TRAN_CDAT t 
                                        JOIN OPERACION o ON t.cod_ope = o.cod_ope 
                                        WHERE T.TIPO_TRAN IN (312, 307, 303) AND O.ESTADO=1
                                        AND T.CODIGO_CDAT = C.CODIGO_CDAT 
                                        AND Trunc(o.fecha_oper) BETWEEN TO_DATE('" + Inicio + "','" + conf.ObtenerFormatoFecha() + @"') AND TO_DATE('" + final + "','" + conf.ObtenerFormatoFecha() + @"')), 0) AS VR_INTERES_PAGADO,
                                        NVL( CASE SIGN(TO_DATE(CIE.FECHA_CIERRE) - TO_DATE('" + final + "','" + conf.ObtenerFormatoFecha() + @"')) WHEN 1 THEN C.VALOR ELSE (CASE HC.ESTADO WHEN 2 THEN C.VALOR WHEN 3 THEN 0 END) END,0)  AS SALDO_FINAL
                                        FROM CDAT c 
                                        JOIN CDAT_TITULAR ct ON c.CODIGO_CDAT = ct.CODIGO_CDAT 
                                        JOIN PERSONA P ON ct.COD_PERSONA = P.COD_PERSONA
                                        JOIN TIPOIDENTIFICACION TI ON P.TIPO_IDENTIFICACION = TI.CODTIPOIDENTIFICACION 
                                        LEFT JOIN HISTORICO_CDAT HC ON HC.CODIGO_CDAT = C.CODIGO_CDAT AND HC.FECHA_HISTORICO = HC.FECHA_HISTORICO = To_date('" + final + "','" + conf.ObtenerFormatoFecha() + @"') AND HC.VALOR != 0 
                                        LEFT JOIN (Select t.codigo_cdat, Max(Trunc(o.fecha_oper)) As fecha_cierre From tran_cdat t Join operacion o On t.cod_ope = o.cod_ope Where o.tipo_ope = 11 AND Trunc(o.fecha_oper) >= To_date('" + Inicio + "','" + conf.ObtenerFormatoFecha() + @"') Group by t.codigo_cdat) cie ON cie.CODIGO_CDAT = C.CODIGO_CDAT
                                        WHERE ct.principal = 1 AND c.estado NOT IN (4, 5) AND (HC.FECHA_HISTORICO IS NOT NULL OR CIE.FECHA_CIERRE >= To_date('" + Inicio + "','" + conf.ObtenerFormatoFecha() + @"')) AND C.FECHA_APERTURA <= To_date('" + final + "','" + conf.ObtenerFormatoFecha() + @"') ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader());

                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "FormatoCDAT", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable Formato1008(int Año, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        // Determinar si se realizó el cierre anual
                        string tabla = "balance_ter";
                        string sqlexist = @"Select * From cierea Where tipo = 'N' And fecha = To_date('31/12/" + Año + "','dd/mm/yyyy')";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlexist;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            tabla = "balance_ter_anual";
                        }
                        dbConnectionFactory.CerrarConexion(connection);

                        // Ejecutar la consulta
                        string sql = @"
                                        SELECT DISTINCT C.CONCEPTO, 0 AS COD_DIAN, '222222222' AS IDENTIFICACION, NULL AS DIGITO_VERIFICACION, NULL AS PRIMER_APELLIDO, NULL AS SEGUNDO_APELLIDO,
                                        'CUANTIAS MENORES' AS PRIMER_NOMBRE, NULL AS SEGUNDO_NOMBRE, NULL AS RAZON_SOCIAL, NULL AS DIRECCION,
                                        NULL AS COD_DPTO,NULL AS COD_MUNICIPIO,NULL AS COD_PAIS, 
                                        SUM(C.SALDO_CUENTAS) AS SALDO_CUENTAS
                                        FROM V_FORMATO1008 C                                         
                                        WHERE TO_CHAR(FECHA) = CASE WHEN COMP = 1 THEN TO_CHAR('" + Año + "') ELSE TO_CHAR(TO_DATE('31/12/" + Año + @"', 'dd/mm/yyyy')) END
                                        AND C.SALDO_CUENTAS < (SELECT VALOR FROM GENERAL WHERE CODIGO = 90175)
                                        GROUP BY C.CONCEPTO
                                        UNION ALL
                                        SELECT CONCEPTO, COD_DIAN, IDENTIFICACION, DIGITO_VERIFICACION, PRIMER_APELLIDO, SEGUNDO_APELLIDO, PRIMER_NOMBRE, SEGUNDO_NOMBRE,
                                        RAZON_SOCIAL, DIRECCION, COD_DPTO, COD_MUNICIPIO, COD_PAIS, SUM(SALDO_CUENTAS) AS SALDO_CUENTAS
                                        FROM V_FORMATO1008 WHERE TO_CHAR(FECHA) = CASE WHEN COMP = 1 THEN TO_CHAR('" + Año + @"') ELSE TO_CHAR(TO_DATE('31/12/" + Año + @"', 'dd/mm/yyyy')) END 
                                        AND SALDO_CUENTAS >= (SELECT VALOR FROM GENERAL WHERE CODIGO = 90175)
                                        GROUP BY CONCEPTO,COD_DIAN,IDENTIFICACION,DIGITO_VERIFICACION,PRIMER_APELLIDO,SEGUNDO_APELLIDO,PRIMER_NOMBRE,SEGUNDO_NOMBRE,
                                        RAZON_SOCIAL,DIRECCION,COD_DPTO,COD_MUNICIPIO,COD_PAIS 
                                    ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader());

                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "Formato1008", ex);
                        return null;
                    }
                }
            }
        }
        public DataTable Formato1001(int Año, int cuantia, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // Determinar las cuantias mayores
                        //string filtro_cuantia = " HAVING Sum(Pago_No_Deducible) >= 100000";
                        //if (cuantia == 2)
                        //    filtro_cuantia = " HAVING Sum(Pago_No_Deducible) < 100000";
                        // Determinar si se realizó el cierre anual para poder sacar los saldos por terceros
                        string tabla = "balance_ter";
                        string sqlexist = @"Select * From cierea Where tipo = 'N' And fecha = To_date('31/12/" + Año + "','dd/mm/yyyy')";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlexist;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            tabla = "balance_ter_anual";
                        }
                        dbConnectionFactory.CerrarConexion(connection);

                        Configuracion conf = new Configuracion();
                        string sql = @" SELECT concepto,COD_DIAN, IDENTIFICACION, DIGITO_VERIFICACION, PRIMER_APELLIDO, SEGUNDO_APELLIDO,
                                        PRIMER_NOMBRE, SEGUNDO_NOMBRE, RAZON_SOCIAL, DIRECCION, COD_DPTO, COD_MUNICIPIO, COD_PAIS,
                                        Pago_Deducible, Sum(Pago_No_Deducible) as Pago_No_Deducible, IVA_Mayor_Deducible, IVA_Mayor_NoDeducible, Sum(RETENCION_Practicada) as RETENCION_Practicada,
                                        Retencion_Asumida, Retencion_Prac_IVA, Retencion_Asum_IVA, Retencion_Prac_NOIVA, Reten_pract_CREE, Reten_asum_CREE
                                        FROM( 
                                            SELECT distinct pcdian.concepto, ti.COD_DIAN, P.IDENTIFICACION, P.DIGITO_VERIFICACION, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO,
                                            P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.RAZON_SOCIAL, P.DIRECCION,
                                            (SELECT RTRIM(CODCIUDAD,'000') FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA)) AS COD_DPTO,
                                            CHR(39)||(SELECT substr(CODCIUDAD,-3,3) FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA) AS COD_MUNICIPIO,
                                            (SELECT CODCIUDAD FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD=(SELECT depende_de FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA))) AS COD_PAIS,
                                            0 AS Pago_Deducible, BT.SALDO_FIN AS Pago_No_Deducible, 0 AS IVA_Mayor_Deducible, 0 AS IVA_Mayor_NoDeducible,
                                            0 AS RETENCION_Practicada,
                                            0 AS Retencion_Asumida, 0 AS Retencion_Prac_IVA, 0 AS Retencion_Asum_IVA, 0 AS Retencion_Prac_NOIVA, 0 AS Reten_pract_CREE, 0 AS Reten_asum_CREE
                                            FROM " + tabla + " bt JOIN persona p ON bt.cod_ter=p.cod_persona JOIN plan_cuentas pc ON pc.cod_cuenta = bt.cod_cuenta " + @"
                                            JOIN TIPOIDENTIFICACION TI ON P.TIPO_IDENTIFICACION = TI.CODTIPOIDENTIFICACION
                                            JOIN PLAN_CUENTAS_HOMOLOGA_DIAN PCDIAN ON PC.COD_CUENTA LIKE '%' || PCDIAN.COD_CUENTA || '%'
                                            WHERE bt.fecha = to_date('31/12/" + Año + "','dd/mm/yyyy') " + @"
                                            AND PCDIAN.CONCEPTO IN (5001, 5002, 5003, 5004, 5005, 5006, 5016, 5010, 5011, 5022, 5058, 5063) AND pc.cod_cuenta NOT IN ('510551','510554','510557','510558')
                                            UNION ALL
                                            SELECT distinct pcdian.concepto, ti.COD_DIAN, P.IDENTIFICACION, P.DIGITO_VERIFICACION, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO,
                                            P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.RAZON_SOCIAL, P.DIRECCION,
                                            (SELECT RTRIM(CODCIUDAD,'000') FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA)) AS COD_DPTO,
                                            CHR(39)||(SELECT substr(CODCIUDAD,-3,3) FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA) AS COD_MUNICIPIO,
                                            (SELECT CODCIUDAD FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD=(SELECT depende_de FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA))) AS COD_PAIS,
                                            0 AS Pago_Deducible, 0 AS Pago_No_Deducible, 0 AS IVA_Mayor_Deducible, 0 AS IVA_Mayor_NoDeducible,
                                            BT.SALDO_FIN AS RETENCION_Practicada,
                                            0 AS Retencion_Asumida, 0 AS Retencion_Prac_IVA, 0 AS Retencion_Asum_IVA, 0 AS Retencion_Prac_NOIVA, 0 AS Reten_pract_CREE, 0 AS Reten_asum_CREE
                                            FROM " + tabla + " bt JOIN persona p ON bt.cod_ter=p.cod_persona JOIN plan_cuentas pc ON pc.cod_cuenta = bt.cod_cuenta " + @"
                                            JOIN TIPOIDENTIFICACION TI ON P.TIPO_IDENTIFICACION = TI.CODTIPOIDENTIFICACION
                                            JOIN PLAN_CUENTAS_HOMOLOGA_DIAN PCDIAN ON PC.COD_CUENTA LIKE '%' || PCDIAN.COD_CUENTA || '%'
                                            WHERE bt.fecha = to_date('31/12/" + Año + "','dd/mm/yyyy') " + @"
                                            AND PCDIAN.CONCEPTO IN (5001, 5002, 5003, 5004, 5005, 5006, 5016, 5010, 5011, 5022, 5058, 5063) ) 
                                        GROUP BY concepto, COD_DIAN, IDENTIFICACION, DIGITO_VERIFICACION, PRIMER_APELLIDO, SEGUNDO_APELLIDO,
                                        PRIMER_NOMBRE,SEGUNDO_NOMBRE,RAZON_SOCIAL, DIRECCION, COD_DPTO, COD_MUNICIPIO, COD_PAIS, Pago_Deducible, IVA_Mayor_Deducible,
                                        IVA_Mayor_NoDeducible, Retencion_Asumida, Retencion_Prac_IVA, Retencion_Asum_IVA, Retencion_Prac_NOIVA, Reten_pract_CREE, Reten_asum_CREE 
                                        HAVING SUM(Pago_No_Deducible) > (SELECT VALOR FROM GENERAL WHERE CODIGO = 90176)   
                                         
                                        UNION ALL
                                           
                                        SELECT  concepto,0 AS COD_DIAN,'222222222' AS IDENTIFICACION,0 AS DIGITO_VERIFICACION,'' AS PRIMER_APELLIDO,'' AS SEGUNDO_APELLIDO,
                                        ''AS  PRIMER_NOMBRE,''AS SEGUNDO_NOMBRE,'CUANTIAS MENORES' AS RAZON_SOCIAL,'' AS DIRECCION,'' AS COD_DPTO,'' AS COD_MUNICIPIO,0 AS COD_PAIS,
                                        0 AS  Pago_Deducible, Sum(Pago_No_Deducible) AS Pago_No_Deducible,0 AS IVA_Mayor_Deducible,0 AS IVA_Mayor_NoDeducible, Sum(RETENCION_Practicada) AS RETENCION_Practicada,
                                        0 AS Retencion_Asumida,0 AS Retencion_Prac_IVA,0 AS Retencion_Asum_IVA,0 AS Retencion_Prac_NOIVA,0 AS Reten_pract_CREE,0 AS Reten_asum_CREE from (
                                        SELECT concepto, COD_DIAN, IDENTIFICACION, DIGITO_VERIFICACION, PRIMER_APELLIDO, SEGUNDO_APELLIDO,
                                        PRIMER_NOMBRE, SEGUNDO_NOMBRE, RAZON_SOCIAL, DIRECCION, COD_DPTO, COD_MUNICIPIO, COD_PAIS,
                                        Pago_Deducible, Sum(Pago_No_Deducible) as Pago_No_Deducible, IVA_Mayor_Deducible, IVA_Mayor_NoDeducible, Sum(RETENCION_Practicada) as RETENCION_Practicada,
                                        Retencion_Asumida, Retencion_Prac_IVA, Retencion_Asum_IVA, Retencion_Prac_NOIVA, Reten_pract_CREE, Reten_asum_CREE
                                            FROM( 
                                            SELECT distinct pcdian.concepto, ti.COD_DIAN, P.IDENTIFICACION, P.DIGITO_VERIFICACION, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO,
                                            P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.RAZON_SOCIAL, P.DIRECCION,
                                            (SELECT RTRIM(CODCIUDAD,'000') FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA)) AS COD_DPTO,
                                            CHR(39)||(SELECT substr(CODCIUDAD,-3,3) FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA) AS COD_MUNICIPIO,
                                            (SELECT CODCIUDAD FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD=(SELECT depende_de FROM CIUDADES WHERE CODCIUDAD = P.CODCIUDADRESIDENCIA))) AS COD_PAIS,
                                            0 AS Pago_Deducible, BT.SALDO_FIN AS Pago_No_Deducible, 0 AS IVA_Mayor_Deducible, 0 AS IVA_Mayor_NoDeducible,
                                            0 AS RETENCION_Practicada,
                                            0 AS Retencion_Asumida, 0 AS Retencion_Prac_IVA, 0 AS Retencion_Asum_IVA, 0 AS Retencion_Prac_NOIVA, 0 AS Reten_pract_CREE, 0 AS Reten_asum_CREE
                                            FROM " + tabla + " bt JOIN persona p ON bt.cod_ter=p.cod_persona JOIN plan_cuentas pc ON pc.cod_cuenta = bt.cod_cuenta " + @" 
                                            JOIN TIPOIDENTIFICACION TI ON P.TIPO_IDENTIFICACION = TI.CODTIPOIDENTIFICACION
                                            JOIN PLAN_CUENTAS_HOMOLOGA_DIAN PCDIAN ON PC.COD_CUENTA LIKE '%'||PCDIAN.COD_CUENTA||'%'
                                            WHERE bt.fecha = to_date('31/12/" + Año + "','dd/mm/yyyy') " + @"  
                                            AND PCDIAN.CONCEPTO IN (5001, 5002, 5003, 5004, 5005, 5006, 5016, 5010, 5011, 5022, 5058, 5036)  AND pc.cod_cuenta LIKE '51%' and pc.cod_cuenta NOT IN ('510551', '510554', '510557', '510558')
                                            UNION ALL
                                            SELECT distinct pcdian.concepto, ti.COD_DIAN, P.IDENTIFICACION, P.DIGITO_VERIFICACION, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO,
                                            P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.RAZON_SOCIAL, P.DIRECCION,
                                            (SELECT RTRIM(CODCIUDAD,'000') FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA)) AS COD_DPTO,
                                            CHR(39)||(SELECT substr(CODCIUDAD,-3,3) FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA) AS COD_MUNICIPIO,
                                            (SELECT CODCIUDAD FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD=(SELECT depende_de FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA))) AS COD_PAIS,
                                            0 AS Pago_Deducible, 0 AS Pago_No_Deducible, 0 AS IVA_Mayor_Deducible, 0 AS IVA_Mayor_NoDeducible,
                                            BT.SALDO_FIN AS RETENCION_Practicada,
                                            0 AS Retencion_Asumida, 0 AS Retencion_Prac_IVA, 0 AS Retencion_Asum_IVA, 0 AS Retencion_Prac_NOIVA, 0 AS Reten_pract_CREE, 0 AS Reten_asum_CREE
                                            FROM " + tabla + " bt JOIN persona p ON bt.cod_ter=p.cod_persona JOIN plan_cuentas pc ON pc.cod_cuenta = bt.cod_cuenta " + @"  
                                            JOIN TIPOIDENTIFICACION TI ON P.TIPO_IDENTIFICACION = TI.CODTIPOIDENTIFICACION
                                            JOIN PLAN_CUENTAS_HOMOLOGA_DIAN PCDIAN ON PC.COD_CUENTA LIKE '%'||PCDIAN.COD_CUENTA||'%'
                                            WHERE bt.fecha = to_date('31/12/" + Año + "','dd/mm/yyyy') " + @"   
                                            AND PCDIAN.CONCEPTO IN (5001, 5002, 5003, 5004, 5005, 5006, 5016, 5010, 5011, 5022, 5058, 5036)  AND pc.cod_cuenta LIKE '2445%' ) 
                                        GROUP BY concepto, COD_DIAN, IDENTIFICACION, DIGITO_VERIFICACION, PRIMER_APELLIDO, SEGUNDO_APELLIDO,
                                        PRIMER_NOMBRE,SEGUNDO_NOMBRE,RAZON_SOCIAL, DIRECCION, COD_DPTO, COD_MUNICIPIO, COD_PAIS, Pago_Deducible, IVA_Mayor_Deducible,
                                        IVA_Mayor_NoDeducible, Retencion_Asumida, Retencion_Prac_IVA, Retencion_Asum_IVA, Retencion_Prac_NOIVA, Reten_pract_CREE, Reten_asum_CREE HAVING Sum(Pago_No_Deducible) < 100000 )
                                        GROUP BY concepto";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader());

                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "Formato1001", ex);
                        return null;
                    }
                }
            }
        }
        public string MonthName(int month)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(month);
        }
        public List<ExogenaReport> TiposConcepto(Usuario vUsuario)
        {

            DbDataReader resultado = default(DbDataReader);
            List<ExogenaReport> lstConsulta = new List<ExogenaReport>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select CODCONCEPTO,CODCONCEPTO ||' - '|| NOMBRE AS NOMBRE  from CONCEPTOSDIAN";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ExogenaReport entidad = new ExogenaReport();

                            if (resultado["CODCONCEPTO"] != DBNull.Value) entidad.codconcepto = Convert.ToInt64(resultado["CODCONCEPTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);

                            lstConsulta.Add(entidad);
                        }

                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "TiposConcepto", ex);
                        return null;
                    }
                }
            }
        }
        public ExogenaReport CrearTiposConceptos(ExogenaReport pTiposConcepto, Usuario pUsuario, int pOpcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCodConcepto = cmdTransaccionFactory.CreateParameter();
                        pCodConcepto.ParameterName = "pCodConcepto";
                        pCodConcepto.Value = pTiposConcepto.codconcepto;

                        DbParameter pNombre = cmdTransaccionFactory.CreateParameter();
                        pNombre.ParameterName = "pNombre";
                        pNombre.Value = pTiposConcepto.nombre;

                        cmdTransaccionFactory.Parameters.Add(pCodConcepto);
                        cmdTransaccionFactory.Parameters.Add(pNombre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = pOpcion == 1 ? "USP_XPINN_EXO_CPTODIAN_CRE" : "USP_XPINN_EXO_CPTODIAN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pTiposConcepto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "CrearTiposConceptos", ex);
                        return null;
                    }
                }
            }
        }
        public ExogenaReport ConsultarConceptosDian(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            ExogenaReport entidad = new ExogenaReport();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM CONCEPTOSDIAN " + pFiltro;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODCONCEPTO"] != DBNull.Value) entidad.codconcepto = Convert.ToInt32(resultado["CODCONCEPTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "ConsultarConceptosDian", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarCodConcepto(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCodConcepto = cmdTransaccionFactory.CreateParameter();
                        pCodConcepto.ParameterName = "pCodConcepto";
                        pCodConcepto.Value = pId;
                        pCodConcepto.Direction = ParameterDirection.Input;
                        pCodConcepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pCodConcepto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_EXO_CPTODIAN_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "EliminarCodConcepto", ex);
                    }
                }
            }
        }
        public ExogenaReport CrearPlanCtasHomologacionDIAN(ExogenaReport pPlanCtasHomologacionDian, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidhomologa = cmdTransaccionFactory.CreateParameter();
                        pidhomologa.ParameterName = "p_idhomologa";
                        pidhomologa.Value = pPlanCtasHomologacionDian.idhomologa;
                        pidhomologa.Direction = ParameterDirection.Output;
                        pidhomologa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidhomologa);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pPlanCtasHomologacionDian.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter P_CONCEPTO = cmdTransaccionFactory.CreateParameter();
                        P_CONCEPTO.ParameterName = "P_CONCEPTO";
                        P_CONCEPTO.Value = pPlanCtasHomologacionDian.codconcepto;
                        P_CONCEPTO.Direction = ParameterDirection.Input;
                        P_CONCEPTO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_CONCEPTO);

                        DbParameter P_INFORME = cmdTransaccionFactory.CreateParameter();
                        P_INFORME.ParameterName = "P_INFORME";
                        P_INFORME.Value = pPlanCtasHomologacionDian.Formato;
                        P_INFORME.Direction = ParameterDirection.Input;
                        P_INFORME.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_INFORME);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_EXO_CTAHOMOLOG_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pPlanCtasHomologacionDian.idhomologa = Convert.ToInt64(pidhomologa.Value);

                        return pPlanCtasHomologacionDian;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "CrearPlanCtasHomologacionDIAN", ex);
                        return null;
                    }
                }
            }
        }
        public ExogenaReport MODPlanCtasHomologacionDIAN(ExogenaReport pPlanCtasHomologacionDian, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidhomologa = cmdTransaccionFactory.CreateParameter();
                        pidhomologa.ParameterName = "p_idhomologa";
                        pidhomologa.Value = pPlanCtasHomologacionDian.idhomologa;
                        pidhomologa.Direction = ParameterDirection.Input;
                        pidhomologa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidhomologa);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pPlanCtasHomologacionDian.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter P_CONCEPTO = cmdTransaccionFactory.CreateParameter();
                        P_CONCEPTO.ParameterName = "P_CONCEPTO";
                        P_CONCEPTO.Value = pPlanCtasHomologacionDian.codconcepto;
                        P_CONCEPTO.Direction = ParameterDirection.Input;
                        P_CONCEPTO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_CONCEPTO);

                        DbParameter P_INFORME = cmdTransaccionFactory.CreateParameter();
                        P_INFORME.ParameterName = "P_INFORME";
                        P_INFORME.Value = pPlanCtasHomologacionDian.Formato;
                        P_INFORME.Direction = ParameterDirection.Input;
                        P_INFORME.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_INFORME);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_EXO_CTAHOMOLOG_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        //pPlanCtasHomologacionDian.idhomologa = Convert.ToInt64(pidhomologa.Value);

                        return pPlanCtasHomologacionDian;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "MODPlanCtasHomologacionDIAN", ex);
                        return null;
                    }
                }
            }
        }
        public List<ExogenaReport> lstHomologacionDian(string cod_cuenta, Usuario vUsuario)
        {

            DbDataReader resultado = default(DbDataReader);
            List<ExogenaReport> lstConsulta = new List<ExogenaReport>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "select * from  PLAN_CUENTAS_HOMOLOGA_DIAN where COD_CUENTA='" + cod_cuenta + "'";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ExogenaReport entidad = new ExogenaReport();

                            if (resultado["IDHOMOLOGA"] != DBNull.Value) entidad.idhomologa = Convert.ToInt64(resultado["IDHOMOLOGA"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.codconcepto = Convert.ToInt64(resultado["CONCEPTO"]);
                            if (resultado["INFORME"] != DBNull.Value) entidad.Formato = Convert.ToString(resultado["INFORME"]);

                            lstConsulta.Add(entidad);
                        }

                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "FormatoAhorros", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable Formato1026(int Año, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT ti.cod_dian,p.identificacion,p.digito_verificacion,
                                        P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.RAZON_SOCIAL, P.DIRECCION,
                                        TO_CHAR( (SELECT RTRIM(CODCIUDAD,'000') FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD = P.CODCIUDADRESIDENCIA))) AS COD_DPTO,
                                        CHR(39) || (SELECT substr(CODCIUDAD,-3,3) FROM CIUDADES WHERE CODCIUDAD = P.CODCIUDADRESIDENCIA) AS COD_MUNICIPIO,
                                        sum(NVL(g.valor, c.monto_aprobado)) as vr_prestamos_otorgados
                                        FROM CREDITO c JOIN PERSONA P ON C.COD_DEUDOR = P.COD_PERSONA JOIN LINEASCREDITO L ON c.cod_linea_credito = l.cod_linea_credito
                                        INNER JOIN TIPOIDENTIFICACION TI ON P.TIPO_IDENTIFICACION = TI.CODTIPOIDENTIFICACION   
                                        LEFT JOIN GIRO G ON c.NUMERO_RADICACION = g.NUMERO_RADICACION AND G.TIPO_ACTO = 1 AND G.ESTADO = 2 
                                        WHERE c.fecha_desembolso between To_date('01/01/" + Año + "','" + conf.ObtenerFormatoFecha() + "') and To_date('31/12/" + Año + "','" + conf.ObtenerFormatoFecha() + "') and (c.estado='C' or c.estado='T')" +
                                        @"  GROUP BY c.cod_clasifica,p.digito_verificacion,ti.cod_dian,P.IDENTIFICACION, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.RAZON_SOCIAL, P.DIRECCION,
                                        P.CODCIUDADRESIDENCIA HAVING  sum(NVL(g.valor, c.monto_aprobado)) >= (SELECT VALOR FROM GENERAL WHERE CODIGO = 90177)";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader());

                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "Formato1026", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable Formato1007(int Año, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // Determinar si se realizó el cierre anual para poder sacar los saldos por terceros
                        string sql = @"SELECT DISTINCT C.CONCEPTO, 0 AS COD_DIAN, '222222222' AS IDENTIFICACION, NULL AS DIGITO_VERIFICACION, NULL AS PRIMER_APELLIDO, NULL AS SEGUNDO_APELLIDO,
                                    'CUANTIAS MENORES' AS PRIMER_NOMBRE, NULL AS SEGUNDO_NOMBRE, NULL AS RAZON_SOCIAL, NULL AS DIRECCION,NULL AS COD_DPTO,NULL AS COD_MUNICIPIO,NULL AS COD_PAIS, 
                                    SUM(C.ING_OPERACION) AS ING_OPERACION, 0 AS ING_CONSORCIOS, 0 AS ING_MANDATOS, 0 AS ING_EXPLORACION, 0 AS ING_FIDUCIAS, 0 AS ING_TERCEROS, 0 AS DEVOLUCIONES,NULL AS NUM_COMP
                                    FROM V_FORMATO1007 C                                         
                                    WHERE FECHA = TO_CHAR('" + Año + @"') AND C.ING_OPERACION < (SELECT VALOR FROM GENERAL WHERE CODIGO = 90170)
                                    GROUP BY C.CONCEPTO
                                    UNION ALL
                                    SELECT DISTINCT CONCEPTO, COD_DIAN, IDENTIFICACION, DIGITO_VERIFICACION, PRIMER_APELLIDO, SEGUNDO_APELLIDO, PRIMER_NOMBRE, SEGUNDO_NOMBRE, RAZON_SOCIAL, DIRECCION, COD_DPTO, COD_MUNICIPIO, COD_PAIS, ING_OPERACION, ING_CONSORCIOS, ING_MANDATOS, ING_EXPLORACION, ING_FIDUCIAS, ING_TERCEROS, DEVOLUCIONES, NUM_COMP
                                    FROM V_FORMATO1007
                                    WHERE FECHA = TO_CHAR('"+Año+ @"') AND ING_OPERACION > (SELECT VALOR FROM GENERAL WHERE CODIGO = 90170) ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader());

                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "Formato1007", ex);
                        return null;
                    }
                }
            }
        }
        public DataTable Formato1009(int Año, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // Determinar si se realizó el cierre anual para poder sacar los saldos por terceros
                        string tabla = "balance_ter";
                        string sqlexist = @"Select * From cierea Where tipo = 'N' And fecha = To_date('31/12/" + Año + "','dd/mm/yyyy')";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqlexist;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            tabla = "balance_ter_anual";
                        }
                        dbConnectionFactory.CerrarConexion(connection);

                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT CONCEPTO, COD_DIAN, IDENTIFICACION, DIGITO_VERIFICACION, PRIMER_APELLIDO, SEGUNDO_APELLIDO, PRIMER_NOMBRE, SEGUNDO_NOMBRE, RAZON_SOCIAL, 
                                        DIRECCION, COD_DPTO, COD_MUNICIPIO, COD_PAIS, SUM(SALDO) AS SALDO                    
                                        FROM (SELECT  pcdian.CONCEPTO, ti.COD_DIAN, P.IDENTIFICACION, P.DIGITO_VERIFICACION, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO,
                                              P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.RAZON_SOCIAL, P.DIRECCION,
                                              (SELECT RTRIM(CODCIUDAD,'000') FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA)) AS COD_DPTO,
                                              CHR(39)||(SELECT substr(CODCIUDAD,-3,3) FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA) AS COD_MUNICIPIO,
                                              (SELECT CODCIUDAD FROM CIUDADES WHERE CODCIUDAD = (SELECT DEPENDE_DE FROM CIUDADES WHERE CODCIUDAD = (SELECT depende_de FROM CIUDADES WHERE CODCIUDAD=P.CODCIUDADRESIDENCIA))) AS COD_PAIS,
                                              BT.SALDO_FIN AS SALDO
                                              FROM " + tabla + " bt JOIN PERSONA p ON bt.COD_TER = p.COD_PERSONA JOIN PLAN_CUENTAS pc ON pc.COD_CUENTA = bt.COD_CUENTA " + @"
                                              JOIN TIPOIDENTIFICACION TI ON P.TIPO_IDENTIFICACION = TI.CODTIPOIDENTIFICACION
                                              JOIN PLAN_CUENTAS_HOMOLOGA_DIAN PCDIAN ON PC.COD_CUENTA LIKE PCDIAN.COD_CUENTA||'%'                                          
                                              WHERE bt.FECHA = TO_DATE('31/12/" + Año + "','dd/MM/yyyy') " + @"
                                              AND PCDIAN.CONCEPTO IN (2201, 2202, 2203, 2204, 2205, 2206, 2207, 2208, 2209) 
                                              )
                                        GROUP BY CONCEPTO, COD_DIAN, IDENTIFICACION, DIGITO_VERIFICACION, PRIMER_APELLIDO, SEGUNDO_APELLIDO, PRIMER_NOMBRE, SEGUNDO_NOMBRE, RAZON_SOCIAL, 
                                        DIRECCION, COD_DPTO, COD_MUNICIPIO, COD_PAIS HAVING SUM(SALDO) > 0";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader());

                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "Formato1009", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable ReporteCausacion(int Año, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT cas.FECHA_CORTE, parlc.COD_CUENTA, P.IDENTIFICACION, P.DIGITO_VERIFICACION, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.RAZON_SOCIAL, 
SUM(CAS.VALOR_INGRESO) AS VALOR_INGRESO, (SELECT cie.CAMPO1 FROM CIEREA cie WHERE cie.TIPO = 'U' AND cie.FECHA = cas.fecha_corte) AS NUM_COMP,
(SELECT cie.CAMPO2 FROM CIEREA cie WHERE cie.TIPO = 'U' AND cie.FECHA = cas.fecha_corte) AS TIPO_COMP, CAS.NUMERO_RADICACION
FROM CAUSACION cas 
JOIN HISTORICO_CRE c ON cas.FECHA_CORTE = c.FECHA_HISTORICO AND cas.NUMERO_RADICACION = c.NUMERO_RADICACION 
JOIN PAR_CUE_LINCRED parlc ON parlc.COD_LINEA_CREDITO = c.COD_LINEA_CREDITO
JOIN PERSONA p ON c.COD_CLIENTE = p.COD_PERSONA
WHERE TO_CHAR(cas.FECHA_CORTE, 'yyyy') = '" + Año + "'" + @" 
AND (c.COD_LINEA_CREDITO = parlc.COD_LINEA_CREDITO AND cas.COD_ATR = parlc.COD_ATR AND parlc.TIPO = 3 AND parlc.TIPO_CUENTA = 2) 
AND parlc.COD_CUENTA IN (SELECT PCDIAN.COD_CUENTA FROM PLAN_CUENTAS_HOMOLOGA_DIAN PCDIAN WHERE PCDIAN.COD_CUENTA = parlc.COD_CUENTA AND PCDIAN.concepto In (4001, 4002, 4003))
GROUP BY cas.FECHA_CORTE, parlc.COD_CUENTA, P.IDENTIFICACION, P.DIGITO_VERIFICACION, P.PRIMER_APELLIDO, P.SEGUNDO_APELLIDO, P.PRIMER_NOMBRE, P.SEGUNDO_NOMBRE, P.RAZON_SOCIAL, CAS.NUMERO_RADICACION
ORDER BY 1, 3";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader());

                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "ReporteCausacion", ex);
                        return null;
                    }
                }
            }
        }
        public DataTable ReporteInteresCDAT(int Año, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        Configuracion conf = new Configuracion();
                        string sql = @"  SELECT p.identificacion,p.primer_nombre||' '||p.segundo_nombre||' '||p.primer_apellido||' '||p.segundo_apellido as Nombre_Completo
                          ,c.numero_cdat,lc.descripcion as Linea_CDAT,sum(t.VALOR) as Interes FROM tran_cdat t JOIN operacion o ON t.cod_ope = o.cod_ope
                        JOIN cdat c ON c.codigo_cdat=t.codigo_cdat JOIN lineacdat lc ON
                        c.cod_lineacdat=lc.cod_lineacdat JOIN cdat_titular ct ON c.codigo_cdat=ct.codigo_cdat
                        join persona p on p.cod_persona=ct.cod_persona
                        WHERE o.fecha_oper BETWEEN to_date('01/01/" + Año + "', 'dd/MM/yyyy') " +
                     @" AND to_date('31/12/" + Año + "', 'dd/MM/yyyy') AND t.tipo_tran IN (303, 307)  " +
                     @"  AND t.estado=1 AND o.tipo_ope NOT IN (7)
                       group by c.numero_cdat,lc.descripcion,p.identificacion,p.primer_nombre||' '||p.segundo_nombre||' '||p.primer_apellido||' '||p.segundo_apellido";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        dtResultado.Load(cmdTransaccionFactory.ExecuteReader());

                        dbConnectionFactory.CerrarConexion(connection);
                        return dtResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExogenaReportData", "ReporteInteresCDAT", ex);
                        return null;
                    }
                }
            }
        }
    }
}
