using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Reporteador.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Reporteador.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla PresupuestoS
    /// </summary>
    public class ReporteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PresupuestoS
        /// </summary>
        public ReporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public DataTable GenerarReporte(int pCodReporte, DateTime pFecha, List<Parametros> lstParametros, ref string[] aColumnas, ref System.Type[] aTipos, ref int numerocolumnas, ref string sError, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtReporte = new DataTable();
            numerocolumnas = 0;
            string Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        Reporte eReporte = new Reporte();
                        string sql = "";

                        if (pCodReporte == 0 || pCodReporte == -1)
                        {
                            sql = "Select h.fecha_historico, h.numero_radicacion, Nvl(d.fecha_cuota, h.fecha_proximo_pago) fecha_cuota, d.fecha_oper As fecha_pago, " +
                                    "   Round(Nvl(trunc(d.fecha_oper), h.fecha_historico) - Nvl(d.fecha_cuota, h.fecha_proximo_pago))  As dias_mora " +
                                    "   From historico_cre h Left Join  " +
                                    "   (Select d.numero_radicacion, o.fecha_oper, d.fecha_cuota, Sum(d.valor) As valor From det_tran_cred d Inner Join operacion o On d.cod_ope = o.cod_ope " +
                                    "   Where to_char(d.fecha_cuota, 'MMyyyy') = to_char(to_date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "'), 'MMyyyy') " +
                                    "   Group by d.numero_radicacion, o.fecha_oper, d.fecha_cuota) d   On h.numero_radicacion = d.numero_radicacion " +
                                    "   Where h.fecha_historico = to_date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And h.saldo_capital != 0 " +
                                    "   and h.cod_linea_credito Not In (Select p.cod_linea_credito from parametros_linea p where p.cod_parametro = 320 and p.cod_linea_credito = h.cod_linea_credito) " +
                                    "   Order by 1, 2, 3";
                        }
                        else
                        {
                            eReporte = ConsultarReporte(pCodReporte, pUsuario);
                            if (eReporte.tipo_reporte != 1 && eReporte.tipo_reporte != 2)
                                return null;
                            sql = eReporte.sentencia_sql;
                            foreach (Parametros rFila in lstParametros)
                            {
                                string sMarcador = "&" + rFila.descripcion + "&";
                                if (sql.Contains(sMarcador))
                                {
                                    if (rFila.tipo == 2)
                                        sql = sql.Replace(sMarcador, " TO_DATE('" + rFila.valor + "', '" + conf.ObtenerFormatoFecha() + "') ");
                                    else
                                        sql = sql.Replace(sMarcador, rFila.valor);
                                }
                            }
                            string sVariable = "[CODUSUARIO]";
                            if (sql.Contains(sVariable))
                            {
                                sql = sql.Replace(sVariable, pUsuario.codusuario.ToString());
                            }
                            sVariable = "[CODOFICINA]";
                            if (sql.Contains(sVariable))
                            {
                                sql = sql.Replace(sVariable, pUsuario.codusuario.ToString());
                            }
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtReporte.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtReporte, ref aColumnas, ref aTipos, ref numerocolumnas, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "GenerarReporte", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable GenerarLista(string pSentencia, ref string sError, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DataTable dtReporte = new DataTable();
            string[] aColumnas = new string[] { };
            System.Type[] aTipos = new System.Type[] { };
            int numerocolumnas = 0;
            string Error = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = pSentencia;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        dtReporte.Clear();
                        Boolean bRes = TraerResultados(resultado, ref dtReporte, ref aColumnas, ref aTipos, ref numerocolumnas, ref Error);

                        dbConnectionFactory.CerrarConexion(connection);

                        return dtReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "GenerarReporte", ex);
                        return null;
                    }
                }
            }
        }



        public DataTable ConsultarCanal_GarantiasComunitarias(Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "select COD_OFICINA as ID_Canal , Nombre as Nombre_canal , 1 as Tipo_canal , direccion as Ubicacion," +
                                    "case when 7 = any(select dia from horariooficina where cod_oficina = o.cod_oficina) then 1 " +
                                    "when 6 = any(select dia from horariooficina where cod_oficina = o.cod_oficina) then 3 " +
                                    "else 2 end as \"DIAS FUNCIONAMIENTO\"," +
                                    "2 as \"HORARIO FUNCIONAMIENTO\", " +
                                    "case LENGTH(ciu.codciudad) when 4 then '0' || ciu.codciudad else TO_CHAR(ciu.codciudad) end as Ciudad," +
                                    "SUBSTR(dep.codciudad , 1 , LENGTH(dep.codciudad) - 3 ) as Departamento " +
                                    "from oficina o " +
                                    "inner join ciudades ciu on ciu.codciudad = o.COD_CIUDAD " +
                                    "inner join ciudades dep on dep.codciudad = ciu.DEPENDE_DE";

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
                        BOExcepcion.Throw("AfiliacionData", "ConsultarCanal_GarantiasComunitarias", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable ConsultarProductos_GarantiasComunitarias(DateTime pFechaCorte, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DateTime pFechaInicial = DateTime.Parse("1/" + pFechaCorte.Month + "/" + pFechaCorte.Year);

                        string sql = @"select Tipo_producto,Nombre_producto,Nicho_mercado,Monto_minimo ,tasa_rentabilidad  from(
                        select  3 as Tipo_producto, la.NOMBRE as Nombre_producto , '' as Nicho_mercado , '' as Monto_minimo , '' as tasa_rentabilidad
                        from HISTORICO_APORTE ha
                        inner join LINEAAPORTE la on ha.COD_LINEA_APORTE = la.COD_LINEA_APORTE
                        where ha.FECHA_HISTORICO BETWEEN to_date('" + pFechaInicial.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY') and to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY')" +

                        @"union
                        select  1 as Tipo_producto, la.DESCRIPCION as Nombre_producto , '' as Nicho_mercado , '' as Monto_minimo , '' as tasa_rentabilidad
                        from HISTORICO_AHORRO ha
                        inner join LINEAAHORRO la on ha.COD_LINEA_AHORRO = la.COD_LINEA_AHORRO
                        where ha.FECHA_HISTORICO BETWEEN to_date('" + pFechaInicial.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY') and to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY')" +

                        @"union
                        select  3 as Tipo_producto, ls.NOMBRE as Nombre_producto , '' as Nicho_mercado , '' as Monto_minimo,'' as tasa_rentabilidad
                        from HISTORICO_SERVICIOS hs
                        inner join LINEASSERVICIOS ls on hs.COD_LINEA_SERVICIO = HS.COD_LINEA_SERVICIO
                        where hs.FECHA_HISTORICO BETWEEN to_date('" + pFechaInicial.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY') and to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY')" +

                        @"union
                        select  1 as Tipo_producto, lp.NOMBRE as Nombre_producto , '' as Nicho_mercado , '' as Monto_minimo, '' as tasa_rentabilidad
                        from HISTORICO_PROGRAMADO hp
                        inner join LINEAPROGRAMADO lp on hp.COD_LINEA_PROGRAMADO = lp.COD_LINEA_PROGRAMADO
                        where hp.FECHA_HISTORICO BETWEEN to_date('" + pFechaInicial.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY') and to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY')" +

                        @"union
                        select  3 as Tipo_producto, lc.NOMBRE as Nombre_producto , '' as Nicho_mercado , r.minimo as Monto_minimo , '' as tasa_rentabilidad
                        from HISTORICO_CRE hc
                        inner join LINEASCREDITO lc on lc.COD_LINEA_CREDITO = hc.COD_LINEA_CREDITO
                        left join RANGOSTOPES r on r.COD_LINEA_CREDITO = hc.COD_LINEA_CREDITO and r.TIPO_TOPE = 3
                        where hc.FECHA_HISTORICO BETWEEN to_date('" + pFechaInicial.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY') and to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY')" +

                        @"union
                        select  3 as Tipo_producto, lc.DESCRIPCION as Nombre_producto , '' as Nicho_mercado , '' as Monto_minimo, '' as tasa_rentabilidad
                        from HISTORICO_CDAT hc
                        inner join LINEACDAT lc on hc.COD_LINEACDAT = lc.COD_LINEACDAT
                        where hc.FECHA_HISTORICO BETWEEN to_date('" + pFechaInicial.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY') and to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY')
                        )order by Tipo_producto ";



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
                        BOExcepcion.Throw("ReporteData", "ConsultarProductos_GarantiasComunitarias", ex);
                        return null;
                    }
                }
            }
        }

        public DataTable ConsultarTransacciones_GarantiasComunitarias(DateTime pFechaCorte, Usuario vUsuario)
        {
            DbDataReader resultado;
            DataTable dtResultado = new DataTable();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DateTime pFechaInicial = DateTime.Parse("1/" + pFechaCorte.Month + "/" + pFechaCorte.Year);
                        string sql = @"SELECT DISTINCT ID_Transacción, ID_Producto , ID_canal,IDENTIFICACION,ID_Originador, Fecha_y_hora , Monto , Operacion , Operación_inusual , Operación_sospechosa , Linea_producto
                                    FROM(
                                    --Operación 1
                                    --AHORRO A LA VISTA
                                    SELECT TA.NUM_TRAN AS ID_Transacción, TA.NUMERO_CUENTA AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TA.VALOR AS Monto, 1 AS Operacion, NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LA.DESCRIPCION, 'AHORRO A LA VISTA') as Linea_producto
                                    FROM TRAN_AHORRO TA
                                    INNER JOIN OPERACION O ON TA.COD_OPE = O.COD_OPE
                                    INNER JOIN AHORRO_VISTA AV ON TA.NUMERO_CUENTA = AV.NUMERO_CUENTA
                                    INNER JOIN LINEAAHORRO LA ON AV.COD_LINEA_AHORRO = LA.COD_LINEA_AHORRO
                                    INNER JOIN PERSONA P ON AV.COD_PERSONA=P.COD_PERSONA
                                    WHERE TA.TIPO_TRAN IN(201,203,205,207,252)
                                    --CONSIGNACIONES
                                    UNION
                                    select C.COD_CONSIGNACION ID_Transacción, C.NUM_CUENTA AS ID_Producto, CJ.COD_OFICINA AS ID_canal,'' AS IDENTIFICACION,''AS ID_Originador, C.FECHA_CONSIGNACION AS Fecha_y_hora, SUM(C.VALOR_EFECTIVO + C.VALOR_CHEQUE) AS Monto, 1 AS Operación_inusual, NULL AS Operación_sospechosa, NULL, 'CORRIENTE' as Linea_producto
                                    FROM CONSIGNACION C
                                    INNER JOIN CAJA CJ ON C.COD_CAJA = CJ.COD_CAJA
                                    group by C.COD_CONSIGNACION, C.FECHA_CONSIGNACION, C.NUM_CUENTA, CJ.COD_OFICINA
                                    --APORTES
                                    UNION
                                    SELECT TA.NUMERO_TRANSACCION AS ID_Transacción,to_char(TA.NUMERO_APORTE) AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TA.VALOR AS Monto, 1 AS Operacion, NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LA.NOMBRE, 'APORTE') AS Linea_producto
                                    FROM TRAN_APORTE TA
                                    INNER JOIN OPERACION O ON TA.COD_OPE = O.COD_OPE
                                    INNER JOIN APORTE AV ON TA.NUMERO_APORTE = AV.NUMERO_APORTE
                                    INNER JOIN LINEAAPORTE LA ON AV.COD_LINEA_APORTE = LA.COD_LINEA_APORTE
                                    INNER JOIN PERSONA P ON AV.COD_PERSONA=P.COD_PERSONA
                                    WHERE TA.TIPO_TRAN IN(101, 102, 107, 113, 115, 118, 119)
                                    --operación 2
                                    --CDATs
                                    UNION
                                    SELECT TCD.NUMERO_TRANSACCION AS ID_Transacción, TO_CHAR(TCD.CODIGO_CDAT) AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TCD.VALOR AS Monto, 2 AS Operacion, NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LCD.DESCRIPCION, 'CDAT') as Linea_producto
                                    from TRAN_CDAT TCD
                                    INNER JOIN OPERACION O ON TCD.COD_OPE = O.COD_OPE
                                    INNER JOIN CDAT CD ON TCD.CODIGO_CDAT = CD.CODIGO_CDAT
                                    INNER JOIN LINEACDAT LCD ON CD.COD_LINEACDAT = LCD.COD_LINEACDAT
                                    INNER JOIN CDAT_TITULAR CDT ON CDT.CODIGO_CDAT=CD.CODIGO_CDAT
                                    INNER JOIN PERSONA P ON P.COD_PERSONA=CDT.COD_PERSONA
                                    WHERE TCD.TIPO_TRAN IN (301,302,310) and CDT.PRINCIPAL=1
                                    --Operación 3
                                    --CREDITOS
                                    UNION
                                    SELECT TC.NUM_TRAN AS ID_Transacción, to_char(TC.NUMERO_RADICACION) AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TC.VALOR AS Monto, 3 AS Operacion, NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LC.NOMBRE, 'CREDITO') as Linea_producto
                                    FROM TRAN_CRED TC
                                    INNER JOIN OPERACION O ON TC.COD_OPE = O.COD_OPE
                                    INNER JOIN CREDITO C ON TC.NUMERO_RADICACION = C.NUMERO_RADICACION
                                    INNER JOIN LINEASCREDITO LC ON C.COD_LINEA_CREDITO = LC.COD_LINEA_CREDITO
                                    INNER JOIN PERSONA P ON P.COD_PERSONA =C.COD_DEUDOR
                                    WHERE TC.TIPO_TRAN IN (2,3,4,5,6,7,11,12,32,33)
                                    --Operación 4
                                 
                                    --Operación 5
                                    --No IMPLEMENTADA por indicaciones
                                    --Operación 6
                                    --APORTES
                                    UNION
                                    SELECT TA.NUMERO_TRANSACCION AS ID_Transacción,to_char(TA.NUMERO_APORTE) AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TA.VALOR AS Monto, 6 AS Operacion, NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LA.NOMBRE, 'APORTE') AS Linea_producto
                                    FROM TRAN_APORTE TA
                                    INNER JOIN OPERACION O ON TA.COD_OPE = O.COD_OPE
                                    INNER JOIN APORTE AV ON TA.NUMERO_APORTE = AV.NUMERO_APORTE
                                    INNER JOIN LINEAAPORTE LA ON AV.COD_LINEA_APORTE = LA.COD_LINEA_APORTE
                                    INNER JOIN PERSONA P ON AV.COD_PERSONA=P.COD_PERSONA
                                    WHERE TA.TIPO_TRAN IN(106)
                                    --Ahorro vista
                                    UNION
                                    SELECT TA.NUM_TRAN AS ID_Transacción, TA.NUMERO_CUENTA AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TA.VALOR AS Monto, 6 AS Operacion, NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LA.DESCRIPCION, 'AHORRO A LA VISTA') as Linea_producto
                                    FROM TRAN_AHORRO TA
                                    INNER JOIN OPERACION O ON TA.COD_OPE = O.COD_OPE
                                    INNER JOIN AHORRO_VISTA AV ON TA.NUMERO_CUENTA = AV.NUMERO_CUENTA
                                    INNER JOIN LINEAAHORRO LA ON AV.COD_LINEA_AHORRO = LA.COD_LINEA_AHORRO
                                    INNER JOIN PERSONA P ON AV.COD_PERSONA=P.COD_PERSONA
                                    WHERE TA.TIPO_TRAN IN(212)
                                    --CDATs                                   
                                    UNION
                                    SELECT TCD.NUMERO_TRANSACCION AS ID_Transacción, TO_CHAR(TCD.CODIGO_CDAT) AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TCD.VALOR AS Monto, 6 AS Operacion, NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LCD.DESCRIPCION, 'CDAT') as Linea_producto
                                    from TRAN_CDAT TCD
                                    INNER JOIN OPERACION O ON TCD.COD_OPE = O.COD_OPE
                                    INNER JOIN CDAT CD ON TCD.CODIGO_CDAT = CD.CODIGO_CDAT
                                    INNER JOIN LINEACDAT LCD ON CD.COD_LINEACDAT = LCD.COD_LINEACDAT
                                    INNER JOIN CDAT_TITULAR CDT ON CDT.CODIGO_CDAT=CD.CODIGO_CDAT
                                    INNER JOIN PERSONA P ON P.COD_PERSONA=CDT.COD_PERSONA
                                    WHERE TCD.TIPO_TRAN IN (305,306) and CDT.PRINCIPAL=1 
                                    --AHORRO PROGRAMADO
                                    UNION
                                    SELECT TA.NUM_TRAN AS ID_Transacción, TA.NUMERO_PROGRAMADO AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TA.VALOR AS Monto, 6 AS Operacion,
                                    NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LA.NOMBRE, 'AHORRO PROGRAMADO') as Linea_producto
                                    FROM TRAN_PROGRAMADO TA
                                    INNER JOIN OPERACION O ON TA.COD_OPE = O.COD_OPE
                                    INNER JOIN AHORRO_PROGRAMADO AV ON TA.NUMERO_PROGRAMADO = AV.NUMERO_PROGRAMADO
                                    INNER JOIN LINEAPROGRAMADO LA ON AV.COD_LINEA_PROGRAMADO = LA.COD_LINEA_PROGRAMADO
                                    INNER JOIN PERSONA P ON AV.COD_PERSONA=P.COD_PERSONA
                                    WHERE TA.TIPO_TRAN IN(352)
                                    --Operación 7
                                    --CREDITOS
                                    UNION
                                    SELECT TC.NUM_TRAN AS ID_Transacción, to_char(TC.NUMERO_RADICACION) AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TC.VALOR AS Monto, 3 AS Operacion, NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LC.NOMBRE, 'CREDITO') as Linea_producto
                                    FROM TRAN_CRED TC
                                    INNER JOIN OPERACION O ON TC.COD_OPE = O.COD_OPE
                                    INNER JOIN CREDITO C ON TC.NUMERO_RADICACION = C.NUMERO_RADICACION
                                    INNER JOIN LINEASCREDITO LC ON C.COD_LINEA_CREDITO = LC.COD_LINEA_CREDITO
                                     INNER JOIN PERSONA P ON P.COD_PERSONA =C.COD_DEUDOR
                                    WHERE TC.TIPO_TRAN IN (1,5,25,60)
                                    --Operación 8
                                    --AHORRO A LA VISTA
                                    UNION
                                    SELECT TA.NUM_TRAN AS ID_Transacción, TA.NUMERO_CUENTA AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TA.VALOR AS Monto, 8 AS Operacion, NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LA.DESCRIPCION, 'AHORRO A LA VISTA') as Linea_producto
                                    FROM TRAN_AHORRO TA
                                    INNER JOIN OPERACION O ON TA.COD_OPE = O.COD_OPE
                                    INNER JOIN AHORRO_VISTA AV ON TA.NUMERO_CUENTA = AV.NUMERO_CUENTA
                                    INNER JOIN LINEAAHORRO LA ON AV.COD_LINEA_AHORRO = LA.COD_LINEA_AHORRO
                                    INNER JOIN PERSONA P ON AV.COD_PERSONA=P.COD_PERSONA
                                    WHERE TA.TIPO_TRAN IN(206,212,228,231,240,241,242,243)
                                    --AHORRO PROGRAMADO
                                    UNION
                                    SELECT TA.NUM_TRAN AS ID_Transacción, TA.NUMERO_PROGRAMADO AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TA.VALOR AS Monto, 2 AS Operacion,
                                    NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LA.NOMBRE, 'AHORRO PROGRAMADO') as Linea_producto
                                    FROM TRAN_PROGRAMADO TA
                                    INNER JOIN OPERACION O ON TA.COD_OPE = O.COD_OPE
                                    INNER JOIN AHORRO_PROGRAMADO AV ON TA.NUMERO_PROGRAMADO = AV.NUMERO_PROGRAMADO
                                    INNER JOIN LINEAPROGRAMADO LA ON AV.COD_LINEA_PROGRAMADO = LA.COD_LINEA_PROGRAMADO
                                    INNER JOIN PERSONA P ON AV.COD_PERSONA=P.COD_PERSONA
                                    WHERE TA.TIPO_TRAN IN(350, 351, 353)
                                    UNION
                                    SELECT TA.NUM_TRAN AS ID_Transacción, TA.NUMERO_PROGRAMADO AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TA.VALOR AS Monto, 7 AS Operacion,
                                    NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LA.NOMBRE, 'AHORRO PROGRAMADO') as Linea_producto
                                    FROM TRAN_PROGRAMADO TA
                                    INNER JOIN OPERACION O ON TA.COD_OPE = O.COD_OPE
                                    INNER JOIN AHORRO_PROGRAMADO AV ON TA.NUMERO_PROGRAMADO = AV.NUMERO_PROGRAMADO
                                    INNER JOIN LINEAPROGRAMADO LA ON AV.COD_LINEA_PROGRAMADO = LA.COD_LINEA_PROGRAMADO
                                    INNER JOIN PERSONA P ON AV.COD_PERSONA=P.COD_PERSONA
                                    WHERE TA.TIPO_TRAN IN(352, 358)
                                    --Operación 9
                                    --APORTES
                                    --UNION
                                    -- SELECT TA.NUMERO_TRANSACCION AS ID_Transacción,to_char(TA.NUMERO_APORTE) AS ID_Producto, O.COD_OFI AS ID_canal,P.IDENTIFICACION AS IDENTIFICACION,'' AS ID_Originador, O.FECHA_OPER AS Fecha_y_hora, TA.VALOR AS Monto, 1 AS Operacion, NULL AS Operación_inusual, NULL AS Operación_sospechosa, NVL(LA.NOMBRE, 'APORTE') AS Linea_producto
                                    --FROM TRAN_APORTE TA
                                    --INNER JOIN OPERACION O ON TA.COD_OPE = O.COD_OPE
                                    --INNER JOIN APORTE AV ON TA.NUMERO_APORTE = AV.NUMERO_APORTE
                                    --INNER JOIN LINEAAPORTE LA ON AV.COD_LINEA_APORTE = LA.COD_LINEA_APORTE
                                    --INNER JOIN PERSONA P ON AV.COD_PERSONA=P.COD_PERSONA
                                    --WHERE TA.TIPO_TRAN IN(108,109)
                                    --Operación 10
                                    --No Aplica
                                    --Operación 11
                                    --No Aplica
                                    --Operación 12
                                    --OTROS
                                    --No IMPLEMENTADA por indicaciones
                                    )
                                    where Fecha_y_hora BETWEEN to_date('" + pFechaInicial.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY') and to_date('" + pFechaCorte.ToString("dd/MM/yyyy") + @"', 'DD/MM/YYYY')
                                    order by Linea_producto";
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
                        BOExcepcion.Throw("ReporteData", "ConsultarTransacciones_GarantiasComunitarias", ex);
                        return null;
                    }
                }
            }
        }

        public int Estructura(DbDataReader resultado, ref DataTable dtReporte, ref string[] aColumnas, ref System.Type[] aTipos, ref string Error)
        {
            Error = "";
            DataTable schemaTable;
            schemaTable = resultado.GetSchemaTable();
            int numerocolumna = 0;
            foreach (DataRow myField in schemaTable.Rows)
            {
                string NombreColumna = "";
                string TipoColumna = "";
                System.Type Tipo = null;
                foreach (DataColumn myProperty in schemaTable.Columns)
                {
                    if (myProperty.ColumnName.Trim() == "ColumnName")
                        NombreColumna = myField[myProperty].ToString();
                    if (myProperty.ColumnName.Trim() == "DataType")
                    {
                        if (NombreColumna.ToLower() == "numero_radicacion")
                            TipoColumna = "System.Int64";
                        else
                            TipoColumna = myField[myProperty].ToString();
                        Tipo = System.Type.GetType(TipoColumna);
                    }
                }
                try
                {
                    Array.Resize(ref aColumnas, numerocolumna + 1);
                    aColumnas.SetValue(NombreColumna, numerocolumna);
                    Array.Resize(ref aTipos, numerocolumna + 1);
                    aTipos.SetValue(Tipo, numerocolumna);
                    dtReporte.Columns.Add(NombreColumna, Tipo);
                    dtReporte.Columns[numerocolumna].AllowDBNull = true;
                    numerocolumna = numerocolumna + 1;
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    return 0;
                }
            }
            return numerocolumna;
        }

        public Boolean TraerResultados(DbDataReader resultado, ref DataTable dtReporte, ref string[] aColumnas, ref System.Type[] aTipos, ref int numerocolumnas, ref string Error)
        {
            try
            {
                aColumnas = new string[] { };
                aTipos = new System.Type[] { };
            }
            catch
            {
                // No se captura el error
            }

            numerocolumnas = Estructura(resultado, ref dtReporte, ref aColumnas, ref aTipos, ref Error);

            try
            {
                while (resultado.Read())
                {
                    DataRow drFila;
                    drFila = dtReporte.NewRow();
                    for (int i = 0; i < numerocolumnas; i++)
                    {
                        if (resultado[aColumnas[i]] != DBNull.Value)
                        {
                            drFila[i] = Convert.ChangeType(resultado[aColumnas[i]], aTipos[i]);
                        }
                    }
                    dtReporte.Rows.Add(drFila);
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }

            return true;
        }


        /// <summary>
        /// Crea un registro en la tabla REPORTE de la base de datos
        /// </summary>
        /// <param name="pUsuario">Entidad Reporte</param>
        /// <returns>Entidad Reporte creada</returns>
        public Reporte CrearReporte(Reporte pReporte, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idreporte = cmdTransaccionFactory.CreateParameter();
                        p_idreporte.ParameterName = "p_idreporte";
                        p_idreporte.Value = ObtenerSiguienteCodigo(vUsuario);
                        p_idreporte.Direction = ParameterDirection.InputOutput;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pReporte.descripcion;

                        DbParameter p_tipo_reporte = cmdTransaccionFactory.CreateParameter();
                        p_tipo_reporte.ParameterName = "p_tipo_reporte";
                        p_tipo_reporte.Value = pReporte.tipo_reporte;

                        DbParameter p_fecha_creacion = cmdTransaccionFactory.CreateParameter();
                        p_fecha_creacion.ParameterName = "p_fecha_creacion";
                        p_fecha_creacion.DbType = DbType.DateTime;
                        p_fecha_creacion.Value = pReporte.fecha_creacion;

                        DbParameter p_cod_elabora = cmdTransaccionFactory.CreateParameter();
                        p_cod_elabora.ParameterName = "p_cod_elabora";
                        p_cod_elabora.Value = pReporte.cod_elabora;

                        DbParameter p_sentencia_sql = cmdTransaccionFactory.CreateParameter();
                        p_sentencia_sql.ParameterName = "p_sentencia_sql";
                        p_sentencia_sql.DbType = DbType.String;
                        p_sentencia_sql.Value = pReporte.sentencia_sql;

                        DbParameter p_url_crystal = cmdTransaccionFactory.CreateParameter();
                        p_url_crystal.ParameterName = "p_url_crystal";
                        p_url_crystal.DbType = DbType.String;
                        p_url_crystal.Value = pReporte.url_crystal;

                        DbParameter p_encabezado = cmdTransaccionFactory.CreateParameter();
                        p_encabezado.ParameterName = "p_encabezado";
                        p_encabezado.DbType = DbType.String;
                        p_encabezado.Value = pReporte.encabezado;

                        DbParameter p_piepagina = cmdTransaccionFactory.CreateParameter();
                        p_piepagina.ParameterName = "p_piepagina";
                        p_piepagina.DbType = DbType.String;
                        p_piepagina.Value = pReporte.piepagina;

                        DbParameter p_numerar = cmdTransaccionFactory.CreateParameter();
                        p_numerar.ParameterName = "p_numerar";
                        p_numerar.DbType = DbType.Int32;
                        p_numerar.Value = pReporte.numerar;

                        cmdTransaccionFactory.Parameters.Add(p_idreporte);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_reporte);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_creacion);
                        cmdTransaccionFactory.Parameters.Add(p_cod_elabora);
                        cmdTransaccionFactory.Parameters.Add(p_sentencia_sql);
                        cmdTransaccionFactory.Parameters.Add(p_url_crystal);
                        cmdTransaccionFactory.Parameters.Add(p_encabezado);
                        cmdTransaccionFactory.Parameters.Add(p_piepagina);
                        cmdTransaccionFactory.Parameters.Add(p_numerar);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_REPORTE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pReporte, "REPORTE", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pReporte.idreporte = Convert.ToInt64(p_idreporte.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "CrearReporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla USUARIOS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad Usuario modificada</returns>
        public Reporte ModificarReporte(Reporte pReporte, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idreporte = cmdTransaccionFactory.CreateParameter();
                        p_idreporte.ParameterName = "p_idreporte";
                        p_idreporte.Value = pReporte.idreporte;
                        p_idreporte.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pReporte.descripcion;

                        DbParameter p_tipo_reporte = cmdTransaccionFactory.CreateParameter();
                        p_tipo_reporte.ParameterName = "p_tipo_reporte";
                        p_tipo_reporte.Value = pReporte.tipo_reporte;

                        DbParameter p_fecha_creacion = cmdTransaccionFactory.CreateParameter();
                        p_fecha_creacion.ParameterName = "p_fecha_creacion";
                        p_fecha_creacion.DbType = DbType.DateTime;
                        p_fecha_creacion.Value = pReporte.fecha_creacion;

                        DbParameter p_cod_elabora = cmdTransaccionFactory.CreateParameter();
                        p_cod_elabora.ParameterName = "p_cod_elabora";
                        p_cod_elabora.Value = pReporte.cod_elabora;

                        DbParameter p_sentencia_sql = cmdTransaccionFactory.CreateParameter();
                        p_sentencia_sql.ParameterName = "p_sentencia_sql";
                        p_sentencia_sql.DbType = DbType.String;
                        p_sentencia_sql.Value = pReporte.sentencia_sql;

                        DbParameter p_url_crystal = cmdTransaccionFactory.CreateParameter();
                        p_url_crystal.ParameterName = "p_url_crystal";
                        p_url_crystal.DbType = DbType.String;
                        p_url_crystal.Value = pReporte.url_crystal;

                        DbParameter p_encabezado = cmdTransaccionFactory.CreateParameter();
                        p_encabezado.ParameterName = "p_encabezado";
                        p_encabezado.DbType = DbType.String;
                        p_encabezado.Value = pReporte.encabezado;

                        DbParameter p_piepagina = cmdTransaccionFactory.CreateParameter();
                        p_piepagina.ParameterName = "p_piepagina";
                        p_piepagina.DbType = DbType.String;
                        p_piepagina.Value = pReporte.piepagina;

                        DbParameter p_numerar = cmdTransaccionFactory.CreateParameter();
                        p_numerar.ParameterName = "p_numerar";
                        p_numerar.DbType = DbType.Int32;
                        p_numerar.Value = pReporte.numerar;

                        cmdTransaccionFactory.Parameters.Add(p_idreporte);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_reporte);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_creacion);
                        cmdTransaccionFactory.Parameters.Add(p_cod_elabora);
                        cmdTransaccionFactory.Parameters.Add(p_sentencia_sql);
                        cmdTransaccionFactory.Parameters.Add(p_url_crystal);
                        cmdTransaccionFactory.Parameters.Add(p_encabezado);
                        cmdTransaccionFactory.Parameters.Add(p_piepagina);
                        cmdTransaccionFactory.Parameters.Add(p_numerar);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_REPORTE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pReporte, "REPORTE", vUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ModificarReporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla REPORTE de la base de datos
        /// </summary>
        /// <param name="pId">identificador de REPORTE</param>
        public void EliminarReporte(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Reporte pReporte = new Reporte();

                        if (vUsuario.programaGeneraLog)
                            pReporte = ConsultarReporte(pId, vUsuario); //REGISTRO DE AUDITORIA

                        DbParameter p_idreporte = cmdTransaccionFactory.CreateParameter();
                        p_idreporte.ParameterName = "p_idreporte";
                        p_idreporte.Value = pId;
                        p_idreporte.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_idreporte);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_REPORTE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pReporte, "REPORTE", vUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReproteData", "EliminarReporte", ex);
                    }
                }
            }
        }

        public void InicializaReporte(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Reporte pReporte = new Reporte();

                        if (vUsuario.programaGeneraLog)
                            pReporte = ConsultarReporte(pId, vUsuario); //REGISTRO DE AUDITORIA

                        DbParameter p_idreporte = cmdTransaccionFactory.CreateParameter();
                        p_idreporte.ParameterName = "p_idreporte";
                        p_idreporte.Value = pId;
                        p_idreporte.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_idreporte);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_REPORTE_INI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pReporte, "REPORTE", vUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReproteData", "InicializaReporte", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla REPORTE de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla REPORTE</param>
        /// <returns>Entidad Usuario consultado</returns>
        public Reporte ConsultarReporte(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            Reporte entidad = new Reporte();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT Reporte.* FROM Reporte WHERE Reporte.idReporte = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["idReporte"] != DBNull.Value) entidad.idreporte = Convert.ToInt64(resultado["idReporte"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipo_reporte"] != DBNull.Value) entidad.tipo_reporte = Convert.ToInt64(resultado["tipo_reporte"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["cod_elabora"] != DBNull.Value) entidad.cod_elabora = Convert.ToInt64(resultado["cod_elabora"]);
                            if (resultado["SENTENCIA_SQL"] != DBNull.Value) entidad.sentencia_sql = Convert.ToString(resultado["SENTENCIA_SQL"]);
                            if (resultado["url_crystal"] != DBNull.Value) entidad.url_crystal = Convert.ToString(resultado["url_crystal"]);
                            if (resultado["encabezado"] != DBNull.Value) entidad.encabezado = Convert.ToString(resultado["encabezado"]);
                            if (resultado["piepagina"] != DBNull.Value) entidad.piepagina = Convert.ToString(resultado["piepagina"]);
                            if (resultado["numerar"] != DBNull.Value) entidad.numerar = Convert.ToInt32(resultado["numerar"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ConsultarReporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla REPORTE dados unos filtros
        /// </summary>
        /// <param name="pUSUARIOS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de reporte obtenidos</returns>
        public List<Reporte> ListarReporte(Reporte pReporte, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstReporte = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  REPORTE " + ObtenerFiltro(pReporte) + " ORDER BY IDREPORTE";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["idReporte"] != DBNull.Value) entidad.idreporte = Convert.ToInt64(resultado["idReporte"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipo_reporte"] != DBNull.Value) entidad.tipo_reporte = Convert.ToInt64(resultado["tipo_reporte"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["cod_elabora"] != DBNull.Value) entidad.cod_elabora = Convert.ToInt64(resultado["cod_elabora"]);
                            if (resultado["sentencia_sql"] != DBNull.Value) entidad.sentencia_sql = Convert.ToString(resultado["sentencia_sql"]);
                            if (resultado["url_crystal"] != DBNull.Value) entidad.url_crystal = Convert.ToString(resultado["url_crystal"]);

                            lstReporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporte", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> ListarReporteUsuario(Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstReporte = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM REPORTE WHERE cod_elabora = " + vUsuario.codusuario +
                                        " OR idReporte IN (Select u.idreporte From reporte_usuario u Where u.idreporte = reporte.idreporte And u.codusuario = " + vUsuario.codusuario + ") " +
                                        " OR idReporte IN (Select u.idreporte From reporte_perfil u Where u.idreporte = reporte.idreporte And u.codperfil = " + vUsuario.codperfil + ") ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["idReporte"] != DBNull.Value) entidad.idreporte = Convert.ToInt64(resultado["idReporte"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipo_reporte"] != DBNull.Value) entidad.tipo_reporte = Convert.ToInt64(resultado["tipo_reporte"]);
                            if (resultado["fecha_creacion"] != DBNull.Value) entidad.fecha_creacion = Convert.ToDateTime(resultado["fecha_creacion"]);
                            if (resultado["cod_elabora"] != DBNull.Value) entidad.cod_elabora = Convert.ToInt64(resultado["cod_elabora"]);
                            if (resultado["sentencia_sql"] != DBNull.Value) entidad.sentencia_sql = Convert.ToString(resultado["sentencia_sql"]);
                            if (resultado["url_crystal"] != DBNull.Value) entidad.url_crystal = Convert.ToString(resultado["url_crystal"]);

                            lstReporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporte", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(idReporte) + 1 FROM REPORTE ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch
                    {
                        return 1;
                    }
                }
            }
        }

        public List<Formato> ListarFormato(Formato pFormato, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Formato> lstReporte = new List<Formato>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM REPORTE_FORMATOS " + ObtenerFiltro(pFormato) + " ORDER BY IDFORMATO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Formato entidad = new Formato();

                            if (resultado["idformato"] != DBNull.Value) entidad.idformato = Convert.ToInt64(resultado["idformato"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["formato"] != DBNull.Value) entidad.formato = Convert.ToString(resultado["formato"]);
                            if (resultado["tipo_dato"] != DBNull.Value) entidad.tipo_dato = Convert.ToInt32(resultado["tipo_dato"]);

                            lstReporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarFormato", ex);
                        return null;
                    }
                }
            }
        }


        public Parametros CrearParametro(Parametros pParametros, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idreporte = cmdTransaccionFactory.CreateParameter();
                        p_idreporte.ParameterName = "p_idreporte";
                        p_idreporte.Value = pParametros.idreporte;
                        p_idreporte.Direction = ParameterDirection.Input;

                        DbParameter p_idparametro = cmdTransaccionFactory.CreateParameter();
                        p_idparametro.ParameterName = "p_idparametro";
                        p_idparametro.Value = 0;
                        p_idparametro.Direction = ParameterDirection.InputOutput;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pParametros.descripcion;

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.Value = pParametros.tipo;

                        DbParameter p_idlista = cmdTransaccionFactory.CreateParameter();
                        p_idlista.ParameterName = "p_idlista";
                        if (pParametros.idlista == null)
                            p_idlista.Value = DBNull.Value;
                        else
                            p_idlista.Value = pParametros.idlista;

                        cmdTransaccionFactory.Parameters.Add(p_idreporte);
                        cmdTransaccionFactory.Parameters.Add(p_idparametro);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);
                        cmdTransaccionFactory.Parameters.Add(p_idlista);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_PARAMETRO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pParametros, "REPORTE_PARAMETRO", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pParametros.idreporte = Convert.ToInt64(p_idreporte.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pParametros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "CrearParametro", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarParametro(Int64 pIdReporte, Int64 pIdParametro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Parametros pReporte = new Parametros();
                        pReporte.idparametro = pIdParametro;
                        pReporte.idreporte = pIdReporte;

                        DbParameter p_idreporte = cmdTransaccionFactory.CreateParameter();
                        p_idreporte.ParameterName = "p_idreporte";
                        p_idreporte.Value = pIdReporte;
                        p_idreporte.Direction = ParameterDirection.Input;

                        DbParameter p_idparametro = cmdTransaccionFactory.CreateParameter();
                        p_idparametro.ParameterName = "p_idparametro";
                        p_idparametro.Value = pIdParametro;
                        p_idparametro.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_idreporte);
                        cmdTransaccionFactory.Parameters.Add(p_idparametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_PARAMETRO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pReporte, "REPORTE_PARAMETRO", vUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReproteData", "EliminarParametro", ex);
                    }
                }
            }
        }

        public Parametros ModificarParametro(Parametros pParametros, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idreporte = cmdTransaccionFactory.CreateParameter();
                        p_idreporte.ParameterName = "p_idreporte";
                        p_idreporte.Value = pParametros.idreporte;
                        p_idreporte.Direction = ParameterDirection.Input;

                        DbParameter p_idparametro = cmdTransaccionFactory.CreateParameter();
                        p_idparametro.ParameterName = "p_idparametro";
                        p_idparametro.Value = pParametros.idparametro;
                        p_idparametro.Direction = ParameterDirection.Input;

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.DbType = DbType.String;
                        p_descripcion.Value = pParametros.descripcion;

                        DbParameter p_tipo = cmdTransaccionFactory.CreateParameter();
                        p_tipo.ParameterName = "p_tipo";
                        p_tipo.Value = pParametros.tipo;

                        DbParameter p_idlista = cmdTransaccionFactory.CreateParameter();
                        p_idlista.ParameterName = "p_idlista";
                        if (pParametros.idlista == null)
                            p_idlista.Value = DBNull.Value;
                        else
                            p_idlista.Value = pParametros.idlista;

                        cmdTransaccionFactory.Parameters.Add(p_idreporte);
                        cmdTransaccionFactory.Parameters.Add(p_idparametro);
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);
                        cmdTransaccionFactory.Parameters.Add(p_tipo);
                        cmdTransaccionFactory.Parameters.Add(p_idlista);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_PARAMETRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pParametros, "REPORTE_PARAMETRO", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pParametros.idreporte = Convert.ToInt64(p_idreporte.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pParametros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ModificarParametro", ex);
                        return null;
                    }
                }
            }
        }

        public List<Parametros> ListarParametro(Parametros pParametros, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Parametros> lstReporte = new List<Parametros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT REPORTE_PARAMETRO.*, DECODE(REPORTE_PARAMETRO.TIPO, 1, 'Texto', 2, 'Fecha', 3, 'Número', 4, 'Lista', REPORTE_PARAMETRO.TIPO) AS NOMTIPO FROM REPORTE_PARAMETRO " + ObtenerFiltro(pParametros);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Parametros entidad = new Parametros();

                            if (resultado["idReporte"] != DBNull.Value) entidad.idreporte = Convert.ToInt64(resultado["idReporte"]);
                            if (resultado["idParametro"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["idParametro"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["descripcion"]);
                            if (resultado["tipo"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["tipo"]);
                            if (resultado["nomtipo"] != DBNull.Value) entidad.nomtipo = Convert.ToString(resultado["nomtipo"]);
                            if (resultado["idlista"] != DBNull.Value) entidad.idlista = Convert.ToInt64(resultado["idlista"]);

                            lstReporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarParametro", ex);
                        return null;
                    }
                }
            }
        }

        public List<UsuariosReporte> ListarUsuarios(UsuariosReporte pUsuario, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<UsuariosReporte> lstReporte = new List<UsuariosReporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT r.idreporte, r.idusuario, u.codusuario, u.nombre, Nvl(r.idusuario, 0) As autorizar FROM Usuarios u Left Join Reporte_Usuario r On u.codusuario = r.codusuario ";
                        if (pUsuario.idreporte != 0)
                            sql += " AND r.idreporte = " + pUsuario.idreporte;
                        sql += " WHERE u.estado = 1 ORDER BY u.nombre";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            UsuariosReporte entidad = new UsuariosReporte();

                            if (resultado["idReporte"] != DBNull.Value) entidad.idreporte = Convert.ToInt64(resultado["idReporte"]);
                            if (resultado["idUsuario"] != DBNull.Value) entidad.idusuario = Convert.ToInt64(resultado["idUsuario"]);
                            if (resultado["codusuario"] != DBNull.Value) entidad.codusuario = Convert.ToInt64(resultado["codusuario"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["autorizar"] != DBNull.Value)
                                if (Convert.ToString(resultado["autorizar"]) == "0")
                                    entidad.autorizar = false;
                                else
                                    entidad.autorizar = true;

                            lstReporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarParametro", ex);
                        return null;
                    }
                }
            }
        }

        public List<PerfilReporte> ListarPerfil(PerfilReporte pPerfil, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PerfilReporte> lstReporte = new List<PerfilReporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT r.idreporte, r.idperfil, u.codperfil, u.nombreperfil, Nvl(r.idperfil, 0) As autorizar FROM perfil_usuario u Left Join Reporte_Perfil r On u.codperfil = r.codperfil ";
                        if (pPerfil.idreporte != 0)
                            sql += " AND r.idreporte = " + pPerfil.idreporte;
                        sql += " ORDER BY u.codperfil";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PerfilReporte entidad = new PerfilReporte();

                            if (resultado["idReporte"] != DBNull.Value) entidad.idreporte = Convert.ToInt64(resultado["idReporte"]);
                            if (resultado["idperfil"] != DBNull.Value) entidad.idperfil = Convert.ToInt64(resultado["idperfil"]);
                            if (resultado["codperfil"] != DBNull.Value) entidad.codperfil = Convert.ToInt64(resultado["codperfil"]);
                            if (resultado["nombreperfil"] != DBNull.Value) entidad.nombreperfil = Convert.ToString(resultado["nombreperfil"]);
                            if (resultado["autorizar"] != DBNull.Value)
                                if (Convert.ToString(resultado["autorizar"]) == "0")
                                    entidad.autorizar = false;
                                else
                                    entidad.autorizar = true;

                            lstReporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarPerfil", ex);
                        return null;
                    }
                }
            }
        }

        public UsuariosReporte CrearUsuario(UsuariosReporte pUsuariosReporte, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idreporte = cmdTransaccionFactory.CreateParameter();
                        p_idreporte.ParameterName = "p_idreporte";
                        p_idreporte.Value = pUsuariosReporte.idreporte;
                        p_idreporte.Direction = ParameterDirection.Input;

                        DbParameter p_idusuario = cmdTransaccionFactory.CreateParameter();
                        p_idusuario.ParameterName = "p_idusuario";
                        p_idusuario.Value = 0;
                        p_idusuario.Direction = ParameterDirection.InputOutput;

                        DbParameter p_codusuario = cmdTransaccionFactory.CreateParameter();
                        p_codusuario.ParameterName = "p_codusuario";
                        p_codusuario.DbType = DbType.String;
                        p_codusuario.Value = pUsuariosReporte.codusuario;

                        cmdTransaccionFactory.Parameters.Add(p_idreporte);
                        cmdTransaccionFactory.Parameters.Add(p_idusuario);
                        cmdTransaccionFactory.Parameters.Add(p_codusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_REPORTE_US_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pUsuariosReporte, "REPORTE_USUARIO", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pUsuariosReporte.idusuario = Convert.ToInt64(p_idusuario.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUsuariosReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "CrearUsuario", ex);
                        return null;
                    }
                }
            }
        }

        public PerfilReporte CrearPerfil(PerfilReporte pPerfilReporte, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_idreporte = cmdTransaccionFactory.CreateParameter();
                        p_idreporte.ParameterName = "p_idreporte";
                        p_idreporte.Value = pPerfilReporte.idreporte;
                        p_idreporte.Direction = ParameterDirection.Input;

                        DbParameter p_idPerfil = cmdTransaccionFactory.CreateParameter();
                        p_idPerfil.ParameterName = "p_idPerfil";
                        p_idPerfil.Value = 0;
                        p_idPerfil.Direction = ParameterDirection.InputOutput;

                        DbParameter p_codperfil = cmdTransaccionFactory.CreateParameter();
                        p_codperfil.ParameterName = "p_codperfil";
                        p_codperfil.DbType = DbType.String;
                        p_codperfil.Value = pPerfilReporte.codperfil;

                        cmdTransaccionFactory.Parameters.Add(p_idreporte);
                        cmdTransaccionFactory.Parameters.Add(p_idPerfil);
                        cmdTransaccionFactory.Parameters.Add(p_codperfil);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_PERFIL_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pPerfilReporte, "REPORTE_USUARIO", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pPerfilReporte.idperfil = Convert.ToInt64(p_idPerfil.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPerfilReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "CrearPerfil", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarUsuario(Int64 pIdReporte, Int64 pIdUsuario, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        UsuariosReporte pReporte = new UsuariosReporte();
                        pReporte.codusuario = pIdUsuario;
                        pReporte.idreporte = pIdReporte;

                        DbParameter p_idreporte = cmdTransaccionFactory.CreateParameter();
                        p_idreporte.ParameterName = "p_idreporte";
                        p_idreporte.Value = pIdReporte;
                        p_idreporte.Direction = ParameterDirection.Input;

                        DbParameter p_codusuario = cmdTransaccionFactory.CreateParameter();
                        p_codusuario.ParameterName = "p_codusuario";
                        p_codusuario.Value = pIdUsuario;
                        p_codusuario.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(p_idreporte);
                        cmdTransaccionFactory.Parameters.Add(p_codusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_REPORTE_US_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pReporte, "REPORTE_USUARIO", vUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReproteData", "EliminarUsuario", ex);
                    }
                }
            }
        }

        public List<Tabla> ListarTablaBase(Tabla pTabla, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Tabla> lstTabla = new List<Tabla>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM v_tablas " + ObtenerFiltro(pTabla) + " ORDER BY NOMBRE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Tabla entidad = new Tabla();
                            if (resultado["OBJECT_ID"] != DBNull.Value) entidad.idtabla = Convert.ToInt32(resultado["OBJECT_ID"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTabla.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTabla;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TablaData", "ListarTabla", ex);
                        return null;
                    }
                }
            }
        }

        public List<Columna> ListarColumnaBase(string pTabla, Columna pColumna, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Columna> lstColumna = new List<Columna>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT DISTINCT * FROM v_columnas " + ObtenerFiltro(pColumna);
                        if (pTabla.Trim() != "")
                            if (sql.Contains("WHERE"))
                                sql = sql + " AND tabla = '" + pTabla.Trim() + "' ";
                            else
                                sql = sql + " WHERE tabla = '" + pTabla.Trim() + "' ";
                        sql = sql + " ORDER BY tabla, nombre";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Columna entidad = new Columna();
                            if (resultado["COLUMN_ID"] != DBNull.Value) entidad.idcolumna = Convert.ToInt32(resultado["COLUMN_ID"]);
                            if (resultado["TABLA"] != DBNull.Value) entidad.tabla = Convert.ToString(resultado["TABLA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_DATO"] != DBNull.Value) entidad.tipo_dato = Convert.ToString(resultado["TIPO_DATO"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToInt32(resultado["LONGITUD"]);
                            if (resultado["PRECISION"] != DBNull.Value) entidad.precision = Convert.ToInt32(resultado["PRECISION"]);
                            if (resultado["ESCALA"] != DBNull.Value) entidad.escala = Convert.ToInt32(resultado["ESCALA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstColumna.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstColumna;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ColumnaData", "ListarColumna", ex);
                        return null;
                    }
                }
            }
        }

        public Columna ConsultarColumna(string pTabla, string pColumna, Usuario vUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT DISTINCT * FROM v_columnas ";
                        if (pTabla.Trim() != "")
                            if (sql.Contains("WHERE"))
                                sql = sql + " AND tabla = '" + pTabla.Trim() + "' ";
                            else
                                sql = sql + " WHERE tabla = '" + pTabla.Trim() + "' ";
                        if (pColumna.Trim() != "")
                            if (sql.Contains("WHERE"))
                                sql = sql + " AND nombre = '" + pColumna.Trim() + "' ";
                            else
                                sql = sql + " WHERE nombre = '" + pColumna.Trim() + "' ";
                        sql = sql + " ORDER BY tabla, nombre";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            Columna entidad = new Columna();
                            if (resultado["COLUMN_ID"] != DBNull.Value) entidad.idcolumna = Convert.ToInt32(resultado["COLUMN_ID"]);
                            if (resultado["TABLA"] != DBNull.Value) entidad.tabla = Convert.ToString(resultado["TABLA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_DATO"] != DBNull.Value) entidad.tipo_dato = Convert.ToString(resultado["TIPO_DATO"]);
                            if (resultado["LONGITUD"] != DBNull.Value) entidad.longitud = Convert.ToInt32(resultado["LONGITUD"]);
                            if (resultado["PRECISION"] != DBNull.Value) entidad.precision = Convert.ToInt32(resultado["PRECISION"]);
                            if (resultado["ESCALA"] != DBNull.Value) entidad.escala = Convert.ToInt32(resultado["ESCALA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            return entidad;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return null;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ColumnaData", "ConsultarColumna", ex);
                        return null;
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Tabla CrearTabla(Tabla pTabla, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtabla = cmdTransaccionFactory.CreateParameter();
                        pidtabla.ParameterName = "p_idtabla";
                        pidtabla.Value = pTabla.idtabla;
                        pidtabla.Direction = ParameterDirection.Output;
                        pidtabla.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtabla);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pTabla.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pTabla.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pTabla.tipo == null)
                            ptipo.Value = "";
                        else
                            ptipo.Value = pTabla.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pTabla.descripcion == null)
                            pdescripcion.Value = "";
                        else
                            pdescripcion.Value = pTabla.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_TABLA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTabla.idtabla = Convert.ToInt64(pidtabla.Value);

                        return pTabla;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TablaData", "CrearTabla", ex);
                        return null;
                    }
                }
            }
        }

        public Tabla ModificarTabla(Tabla pTabla, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtabla = cmdTransaccionFactory.CreateParameter();
                        pidtabla.ParameterName = "p_idtabla";
                        pidtabla.Value = pTabla.idtabla;
                        pidtabla.Direction = ParameterDirection.Input;
                        pidtabla.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtabla);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pTabla.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pTabla.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pTabla.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pTabla.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_TABLA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pTabla;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TablaData", "ModificarTabla", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarTabla(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Tabla pTabla = new Tabla();

                        DbParameter pidtabla = cmdTransaccionFactory.CreateParameter();
                        pidtabla.ParameterName = "p_idtabla";
                        pidtabla.Value = pId;
                        pidtabla.Direction = ParameterDirection.Input;
                        pidtabla.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtabla);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_TABLA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TablaData", "EliminarTabla", ex);
                    }
                }
            }
        }

        public List<Tabla> ListarTabla(Tabla pTabla, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Tabla> lstTabla = new List<Tabla>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM reporte_tabla " + ObtenerFiltro(pTabla) + " ORDER BY IDTABLA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Tabla entidad = new Tabla();
                            if (resultado["IDTABLA"] != DBNull.Value) entidad.idtabla = Convert.ToInt32(resultado["IDTABLA"]);
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTabla.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTabla;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TablaData", "ListarTabla", ex);
                        return null;
                    }
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Encadenamiento CrearEncadenamiento(Encadenamiento pEncadenamiento, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidencadenamiento = cmdTransaccionFactory.CreateParameter();
                        pidencadenamiento.ParameterName = "p_idencadenamiento";
                        pidencadenamiento.Value = pEncadenamiento.idencadenamiento;
                        pidencadenamiento.Direction = ParameterDirection.Output;
                        pidencadenamiento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidencadenamiento);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pEncadenamiento.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pEncadenamiento.nombre == null)
                            pnombre.Value = "";
                        else
                            pnombre.Value = pEncadenamiento.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptabla1 = cmdTransaccionFactory.CreateParameter();
                        ptabla1.ParameterName = "p_tabla1";
                        ptabla1.Value = pEncadenamiento.tabla1;
                        ptabla1.Direction = ParameterDirection.Input;
                        ptabla1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla1);

                        DbParameter pcolumna1 = cmdTransaccionFactory.CreateParameter();
                        pcolumna1.ParameterName = "p_columna1";
                        pcolumna1.Value = pEncadenamiento.columna1;
                        pcolumna1.Direction = ParameterDirection.Input;
                        pcolumna1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna1);

                        DbParameter ptabla2 = cmdTransaccionFactory.CreateParameter();
                        ptabla2.ParameterName = "p_tabla2";
                        ptabla2.Value = pEncadenamiento.tabla2;
                        ptabla2.Direction = ParameterDirection.Input;
                        ptabla2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla2);

                        DbParameter pcolumna2 = cmdTransaccionFactory.CreateParameter();
                        pcolumna2.ParameterName = "p_columna2";
                        pcolumna2.Value = pEncadenamiento.columna2;
                        pcolumna2.Direction = ParameterDirection.Input;
                        pcolumna2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna2);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_ENCADENA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pEncadenamiento.idencadenamiento = Convert.ToInt64(pidencadenamiento.Value);

                        return pEncadenamiento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EncadenamientoData", "CrearEncadenamiento", ex);
                        return null;
                    }
                }
            }
        }

        public Encadenamiento ModificarEncadenamiento(Encadenamiento pEncadenamiento, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidencadenamiento = cmdTransaccionFactory.CreateParameter();
                        pidencadenamiento.ParameterName = "p_idencadenamiento";
                        pidencadenamiento.Value = pEncadenamiento.idencadenamiento;
                        pidencadenamiento.Direction = ParameterDirection.Input;
                        pidencadenamiento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidencadenamiento);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pEncadenamiento.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pEncadenamiento.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptabla1 = cmdTransaccionFactory.CreateParameter();
                        ptabla1.ParameterName = "p_tabla1";
                        ptabla1.Value = pEncadenamiento.tabla1;
                        ptabla1.Direction = ParameterDirection.Input;
                        ptabla1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla1);

                        DbParameter pcolumna1 = cmdTransaccionFactory.CreateParameter();
                        pcolumna1.ParameterName = "p_columna1";
                        pcolumna1.Value = pEncadenamiento.columna1;
                        pcolumna1.Direction = ParameterDirection.Input;
                        pcolumna1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna1);

                        DbParameter ptabla2 = cmdTransaccionFactory.CreateParameter();
                        ptabla2.ParameterName = "p_tabla2";
                        ptabla2.Value = pEncadenamiento.tabla2;
                        ptabla2.Direction = ParameterDirection.Input;
                        ptabla2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla2);

                        DbParameter pcolumna2 = cmdTransaccionFactory.CreateParameter();
                        pcolumna2.ParameterName = "p_columna2";
                        pcolumna2.Value = pEncadenamiento.columna2;
                        pcolumna2.Direction = ParameterDirection.Input;
                        pcolumna2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna2);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_ENCADENA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pEncadenamiento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EncadenamientoData", "ModificarEncadenamiento", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarEncadenamiento(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidencadenamiento = cmdTransaccionFactory.CreateParameter();
                        pidencadenamiento.ParameterName = "p_idencadenamiento";
                        pidencadenamiento.Value = pId;
                        pidencadenamiento.Direction = ParameterDirection.Input;
                        pidencadenamiento.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidencadenamiento);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_ENCADENA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EncadenamientoData", "EliminarCEliminarEncadenamientooncepto", ex);
                    }
                }
            }
        }

        public List<Encadenamiento> ListarEncadenamiento(Encadenamiento pEncadenamiento, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Encadenamiento> lstEncadenamiento = new List<Encadenamiento>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM reporte_encadenamiento " + ObtenerFiltro(pEncadenamiento) + " ORDER BY IDENCADENAMIENTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Encadenamiento entidad = new Encadenamiento();
                            if (resultado["IDENCADENAMIENTO"] != DBNull.Value) entidad.idencadenamiento = Convert.ToInt32(resultado["IDENCADENAMIENTO"]);
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TABLA1"] != DBNull.Value) entidad.tabla1 = Convert.ToString(resultado["TABLA1"]);
                            if (resultado["COLUMNA1"] != DBNull.Value) entidad.columna1 = Convert.ToString(resultado["COLUMNA1"]);
                            if (resultado["TABLA2"] != DBNull.Value) entidad.tabla2 = Convert.ToString(resultado["TABLA2"]);
                            if (resultado["COLUMNA2"] != DBNull.Value) entidad.columna2 = Convert.ToString(resultado["COLUMNA2"]);
                            lstEncadenamiento.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEncadenamiento;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EncadenamientoData", "ListarEncadenamiento", ex);
                        return null;
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Condicion CrearCondicion(Condicion pCondicion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcondicion = cmdTransaccionFactory.CreateParameter();
                        pidcondicion.ParameterName = "p_idcondicion";
                        pidcondicion.Value = pCondicion.idcondicion;
                        pidcondicion.Direction = ParameterDirection.Output;
                        pidcondicion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcondicion);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pCondicion.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pCondicion.nombre == null)
                            pnombre.Value = "";
                        else
                            pnombre.Value = pCondicion.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pandor = cmdTransaccionFactory.CreateParameter();
                        pandor.ParameterName = "p_andor";
                        if (pCondicion.andor == null)
                            pandor.Value = DBNull.Value;
                        else
                            pandor.Value = pCondicion.andor;
                        pandor.Direction = ParameterDirection.Input;
                        pandor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pandor);

                        DbParameter pparentesisizq = cmdTransaccionFactory.CreateParameter();
                        pparentesisizq.ParameterName = "p_parentesisizq";
                        if (pCondicion.parentesisizq == null)
                            pparentesisizq.Value = DBNull.Value;
                        else
                            pparentesisizq.Value = pCondicion.parentesisizq;
                        pparentesisizq.Direction = ParameterDirection.Input;
                        pparentesisizq.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pparentesisizq);

                        DbParameter ptipo1 = cmdTransaccionFactory.CreateParameter();
                        ptipo1.ParameterName = "p_tipo1";
                        ptipo1.Value = pCondicion.tipo1;
                        ptipo1.Direction = ParameterDirection.Input;
                        ptipo1.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo1);

                        DbParameter ptabla1 = cmdTransaccionFactory.CreateParameter();
                        ptabla1.ParameterName = "p_tabla1";
                        if (pCondicion.tabla1 == null)
                            ptabla1.Value = DBNull.Value;
                        else
                            ptabla1.Value = pCondicion.tabla1;
                        ptabla1.Direction = ParameterDirection.Input;
                        ptabla1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla1);

                        DbParameter pcolumna1 = cmdTransaccionFactory.CreateParameter();
                        pcolumna1.ParameterName = "p_columna1";
                        if (pCondicion.columna1 == null)
                            pcolumna1.Value = DBNull.Value;
                        else
                            pcolumna1.Value = pCondicion.columna1;
                        pcolumna1.Direction = ParameterDirection.Input;
                        pcolumna1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna1);

                        DbParameter pvalor1 = cmdTransaccionFactory.CreateParameter();
                        pvalor1.ParameterName = "p_valor1";
                        if (pCondicion.valor1 == null)
                            pvalor1.Value = DBNull.Value;
                        else
                            pvalor1.Value = pCondicion.valor1;
                        pvalor1.Direction = ParameterDirection.Input;
                        pvalor1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor1);

                        DbParameter poperador = cmdTransaccionFactory.CreateParameter();
                        poperador.ParameterName = "p_operador";
                        poperador.Value = pCondicion.operador;
                        poperador.Direction = ParameterDirection.Input;
                        poperador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(poperador);

                        DbParameter ptipo2 = cmdTransaccionFactory.CreateParameter();
                        ptipo2.ParameterName = "p_tipo2";
                        ptipo2.Value = pCondicion.tipo2;
                        ptipo2.Direction = ParameterDirection.Input;
                        ptipo2.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo2);

                        DbParameter ptabla2 = cmdTransaccionFactory.CreateParameter();
                        ptabla2.ParameterName = "p_tabla2";
                        if (pCondicion.tabla2 == null)
                            ptabla2.Value = "";
                        else
                            ptabla2.Value = pCondicion.tabla2;
                        ptabla2.Direction = ParameterDirection.Input;
                        ptabla2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla2);

                        DbParameter pcolumna2 = cmdTransaccionFactory.CreateParameter();
                        pcolumna2.ParameterName = "p_columna2";
                        if (pCondicion.columna2 == null)
                            pcolumna2.Value = "";
                        else
                            pcolumna2.Value = pCondicion.columna2;
                        pcolumna2.Direction = ParameterDirection.Input;
                        pcolumna2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna2);

                        DbParameter pvalor2 = cmdTransaccionFactory.CreateParameter();
                        pvalor2.ParameterName = "p_valor2";
                        if (pCondicion.valor2 == null)
                            pvalor2.Value = "";
                        else
                            pvalor2.Value = pCondicion.valor2;
                        pvalor2.Direction = ParameterDirection.Input;
                        pvalor2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor2);

                        DbParameter pparentesisder = cmdTransaccionFactory.CreateParameter();
                        pparentesisder.ParameterName = "p_parentesisder";
                        if (pCondicion.parentesisder == null)
                            pparentesisder.Value = DBNull.Value;
                        else
                            pparentesisder.Value = pCondicion.parentesisder;
                        pparentesisder.Direction = ParameterDirection.Input;
                        pparentesisder.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pparentesisder);

                        DbParameter pidlista = cmdTransaccionFactory.CreateParameter();
                        pidlista.ParameterName = "p_idlista";
                        if (pCondicion.idlista == null)
                            pidlista.Value = DBNull.Value;
                        else
                            pidlista.Value = pCondicion.idlista;
                        pidlista.Direction = ParameterDirection.Input;
                        pidlista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidlista);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_CONDICION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pCondicion.idcondicion = Convert.ToInt64(pidcondicion.Value);

                        return pCondicion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CondicionData", "CrearCondicion", ex);
                        return null;
                    }
                }
            }
        }

        public Condicion ModificarCondicion(Condicion pCondicion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcondicion = cmdTransaccionFactory.CreateParameter();
                        pidcondicion.ParameterName = "p_idcondicion";
                        pidcondicion.Value = pCondicion.idcondicion;
                        pidcondicion.Direction = ParameterDirection.Input;
                        pidcondicion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcondicion);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pCondicion.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pCondicion.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pandor = cmdTransaccionFactory.CreateParameter();
                        pandor.ParameterName = "p_andor";
                        if (pCondicion.andor == null)
                            pandor.Value = DBNull.Value;
                        else
                            pandor.Value = pCondicion.andor;
                        pandor.Direction = ParameterDirection.Input;
                        pandor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pandor);

                        DbParameter pparentesisizq = cmdTransaccionFactory.CreateParameter();
                        pparentesisizq.ParameterName = "p_parentesisizq";
                        if (pCondicion.parentesisizq == null)
                            pparentesisizq.Value = DBNull.Value;
                        else
                            pparentesisizq.Value = pCondicion.parentesisizq;
                        pparentesisizq.Direction = ParameterDirection.Input;
                        pparentesisizq.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pparentesisizq);


                        DbParameter ptipo1 = cmdTransaccionFactory.CreateParameter();
                        ptipo1.ParameterName = "p_tipo1";
                        ptipo1.Value = pCondicion.tipo1;
                        ptipo1.Direction = ParameterDirection.Input;
                        ptipo1.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo1);

                        DbParameter ptabla1 = cmdTransaccionFactory.CreateParameter();
                        ptabla1.ParameterName = "p_tabla1";
                        ptabla1.Value = pCondicion.tabla1;
                        ptabla1.Direction = ParameterDirection.Input;
                        ptabla1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla1);

                        DbParameter pcolumna1 = cmdTransaccionFactory.CreateParameter();
                        pcolumna1.ParameterName = "p_columna1";
                        pcolumna1.Value = pCondicion.columna1;
                        pcolumna1.Direction = ParameterDirection.Input;
                        pcolumna1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna1);

                        DbParameter pvalor1 = cmdTransaccionFactory.CreateParameter();
                        pvalor1.ParameterName = "p_valor1";
                        pvalor1.Value = pCondicion.valor1;
                        pvalor1.Direction = ParameterDirection.Input;
                        pvalor1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor1);

                        DbParameter poperador = cmdTransaccionFactory.CreateParameter();
                        poperador.ParameterName = "p_operador";
                        poperador.Value = pCondicion.operador;
                        poperador.Direction = ParameterDirection.Input;
                        poperador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(poperador);

                        DbParameter ptipo2 = cmdTransaccionFactory.CreateParameter();
                        ptipo2.ParameterName = "p_tipo2";
                        ptipo2.Value = pCondicion.tipo2;
                        ptipo2.Direction = ParameterDirection.Input;
                        ptipo2.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo2);

                        DbParameter ptabla2 = cmdTransaccionFactory.CreateParameter();
                        ptabla2.ParameterName = "p_tabla2";
                        ptabla2.Value = pCondicion.tabla2;
                        ptabla2.Direction = ParameterDirection.Input;
                        ptabla2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla2);

                        DbParameter pcolumna2 = cmdTransaccionFactory.CreateParameter();
                        pcolumna2.ParameterName = "p_columna2";
                        pcolumna2.Value = pCondicion.columna2;
                        pcolumna2.Direction = ParameterDirection.Input;
                        pcolumna2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna2);

                        DbParameter pvalor2 = cmdTransaccionFactory.CreateParameter();
                        pvalor2.ParameterName = "p_valor2";
                        pvalor2.Value = pCondicion.valor2;
                        pvalor2.Direction = ParameterDirection.Input;
                        pvalor2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor2);

                        DbParameter pparentesisder = cmdTransaccionFactory.CreateParameter();
                        pparentesisder.ParameterName = "p_parentesisder";
                        if (pCondicion.parentesisder == null)
                            pparentesisder.Value = DBNull.Value;
                        else
                            pparentesisder.Value = pCondicion.parentesisder;

                        DbParameter pidlista = cmdTransaccionFactory.CreateParameter();
                        pidlista.ParameterName = "p_idlista";
                        pidlista.Value = pCondicion.idlista;
                        pidlista.Direction = ParameterDirection.Input;
                        pidlista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidlista);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_CONDICION_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCondicion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CondicionData", "ModificarCondicion", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarCondicion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcondicion = cmdTransaccionFactory.CreateParameter();
                        pidcondicion.ParameterName = "p_idcondicion";
                        pidcondicion.Value = pId;
                        pidcondicion.Direction = ParameterDirection.Input;
                        pidcondicion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcondicion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_CONDICION_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CondicionData", "EliminarCondicion", ex);
                    }
                }
            }
        }

        public List<Condicion> ListarCondicion(Condicion pCondicion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Condicion> lstCondicion = new List<Condicion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT reporte_Condicion.*, (Select a.descripcion From reporte_lista a Where a.idlista = reporte_condicion.idlista) As nomlista FROM reporte_Condicion " + ObtenerFiltro(pCondicion) + " ORDER BY IDCONDICION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Condicion entidad = new Condicion();
                            if (resultado["IDCONDICION"] != DBNull.Value) entidad.idcondicion = Convert.ToInt32(resultado["IDCONDICION"]);
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["ANDOR"] != DBNull.Value) entidad.andor = Convert.ToString(resultado["ANDOR"]);
                            if (resultado["PARENTESISIZQ"] != DBNull.Value) entidad.parentesisizq = Convert.ToString(resultado["PARENTESISIZQ"]);
                            if (resultado["TIPO1"] != DBNull.Value) entidad.tipo1 = Convert.ToInt32(resultado["TIPO1"]);
                            if (resultado["TABLA1"] != DBNull.Value) entidad.tabla1 = Convert.ToString(resultado["TABLA1"]);
                            if (resultado["COLUMNA1"] != DBNull.Value) entidad.columna1 = Convert.ToString(resultado["COLUMNA1"]);
                            if (resultado["VALOR1"] != DBNull.Value) entidad.valor1 = Convert.ToString(resultado["VALOR1"]);
                            if (resultado["OPERADOR"] != DBNull.Value) entidad.operador = Convert.ToString(resultado["OPERADOR"]);
                            if (resultado["TIPO2"] != DBNull.Value) entidad.tipo2 = Convert.ToInt32(resultado["TIPO2"]);
                            if (resultado["TABLA2"] != DBNull.Value) entidad.tabla2 = Convert.ToString(resultado["TABLA2"]);
                            if (resultado["COLUMNA2"] != DBNull.Value) entidad.columna2 = Convert.ToString(resultado["COLUMNA2"]);
                            if (resultado["VALOR2"] != DBNull.Value) entidad.valor2 = Convert.ToString(resultado["VALOR2"]);
                            if (resultado["PARENTESISDER"] != DBNull.Value) entidad.parentesisder = Convert.ToString(resultado["PARENTESISDER"]);
                            if (resultado["IDLISTA"] != DBNull.Value) entidad.idlista = Convert.ToInt32(resultado["IDLISTA"]);
                            if (resultado["NOMLISTA"] != DBNull.Value) entidad.nomlista = Convert.ToString(resultado["NOMLISTA"]);
                            lstCondicion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCondicion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CondicionData", "ListarCondicion", ex);
                        return null;
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ColumnaReporte CrearColumna(ColumnaReporte pColumna, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcolumna = cmdTransaccionFactory.CreateParameter();
                        pidcolumna.ParameterName = "p_idcolumna";
                        pidcolumna.Value = pColumna.idcolumna;
                        pidcolumna.Direction = ParameterDirection.Output;
                        pidcolumna.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcolumna);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pColumna.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter porden = cmdTransaccionFactory.CreateParameter();
                        porden.ParameterName = "p_orden";
                        porden.Value = pColumna.orden;
                        porden.Direction = ParameterDirection.Input;
                        porden.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(porden);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pColumna.tipo == null)
                            ptipo.Value = "";
                        else
                            ptipo.Value = pColumna.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter ptabla = cmdTransaccionFactory.CreateParameter();
                        ptabla.ParameterName = "p_tabla";
                        if (pColumna.tabla == null)
                            ptabla.Value = "";
                        else
                            ptabla.Value = pColumna.tabla;
                        ptabla.Direction = ParameterDirection.Input;
                        ptabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla);

                        DbParameter pcolumna = cmdTransaccionFactory.CreateParameter();
                        pcolumna.ParameterName = "p_columna";
                        if (pColumna.columna == null)
                            pcolumna.Value = "";
                        else
                            pcolumna.Value = pColumna.columna;
                        pcolumna.Direction = ParameterDirection.Input;
                        pcolumna.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna);

                        DbParameter ptitulo = cmdTransaccionFactory.CreateParameter();
                        ptitulo.ParameterName = "p_titulo";
                        ptitulo.Value = pColumna.titulo;
                        if (pColumna.titulo == null)
                            ptitulo.Value = "";
                        else
                            ptitulo.Value = pColumna.titulo;
                        ptitulo.Direction = ParameterDirection.Input;
                        ptitulo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptitulo);

                        DbParameter pformato = cmdTransaccionFactory.CreateParameter();
                        pformato.ParameterName = "p_formato";
                        if (pColumna.formato == null)
                            pformato.Value = "";
                        else
                            pformato.Value = pColumna.formato;
                        pformato.Direction = ParameterDirection.Input;
                        pformato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pformato);

                        DbParameter ptipodato = cmdTransaccionFactory.CreateParameter();
                        ptipodato.ParameterName = "p_tipodato";
                        if (pColumna.tipodato == null)
                            ptipodato.Value = "";
                        else
                            ptipodato.Value = pColumna.tipodato;
                        ptipodato.Direction = ParameterDirection.Input;
                        ptipodato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipodato);

                        DbParameter palineacion = cmdTransaccionFactory.CreateParameter();
                        palineacion.ParameterName = "p_alineacion";
                        if (pColumna.alineacion == null)
                            palineacion.Value = "";
                        else
                            palineacion.Value = pColumna.alineacion;
                        palineacion.Direction = ParameterDirection.Input;
                        palineacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(palineacion);

                        DbParameter pancho = cmdTransaccionFactory.CreateParameter();
                        pancho.ParameterName = "p_ancho";
                        if (pColumna.ancho == null)
                            pancho.Value = 0;
                        else
                            pancho.Value = pColumna.ancho;
                        pancho.Direction = ParameterDirection.Input;
                        pancho.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pancho);

                        DbParameter ptotal = cmdTransaccionFactory.CreateParameter();
                        ptotal.ParameterName = "p_total";
                        ptotal.Value = pColumna.total;
                        ptotal.Direction = ParameterDirection.Input;
                        ptotal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptotal);

                        DbParameter pformula = cmdTransaccionFactory.CreateParameter();
                        pformula.ParameterName = "p_formula";
                        if (pColumna.formula == null)
                            pformula.Value = "";
                        else
                            pformula.Value = pColumna.formula;
                        palineacion.Direction = ParameterDirection.Input;
                        palineacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pformula);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_COLUMNA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pColumna.idcolumna = Convert.ToInt64(pidcolumna.Value);

                        return pColumna;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ColumnaData", "CrearColumna", ex);
                        return null;
                    }
                }
            }
        }

        public ColumnaReporte ModificarColumna(ColumnaReporte pColumna, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcolumna = cmdTransaccionFactory.CreateParameter();
                        pidcolumna.ParameterName = "p_idcolumna";
                        pidcolumna.Value = pColumna.idcolumna;
                        pidcolumna.Direction = ParameterDirection.Input;
                        pidcolumna.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcolumna);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pColumna.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter porden = cmdTransaccionFactory.CreateParameter();
                        porden.ParameterName = "p_orden";
                        porden.Value = pColumna.orden;
                        porden.Direction = ParameterDirection.Input;
                        porden.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(porden);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pColumna.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter ptabla = cmdTransaccionFactory.CreateParameter();
                        ptabla.ParameterName = "p_tabla";
                        ptabla.Value = pColumna.tabla;
                        ptabla.Direction = ParameterDirection.Input;
                        ptabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla);

                        DbParameter pcolumna = cmdTransaccionFactory.CreateParameter();
                        pcolumna.ParameterName = "p_columna";
                        pcolumna.Value = pColumna.columna;
                        pcolumna.Direction = ParameterDirection.Input;
                        pcolumna.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna);

                        DbParameter ptitulo = cmdTransaccionFactory.CreateParameter();
                        ptitulo.ParameterName = "p_titulo";
                        ptitulo.Value = pColumna.titulo;
                        ptitulo.Direction = ParameterDirection.Input;
                        ptitulo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptitulo);

                        DbParameter pformato = cmdTransaccionFactory.CreateParameter();
                        pformato.ParameterName = "p_formato";
                        pformato.Value = pColumna.formato;
                        pformato.Direction = ParameterDirection.Input;
                        pformato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pformato);

                        DbParameter ptipodato = cmdTransaccionFactory.CreateParameter();
                        ptipodato.ParameterName = "p_tipodato";
                        ptipodato.Value = pColumna.tipodato;
                        ptipodato.Direction = ParameterDirection.Input;
                        ptipodato.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipodato);

                        DbParameter palineacion = cmdTransaccionFactory.CreateParameter();
                        palineacion.ParameterName = "p_alineacion";
                        palineacion.Value = pColumna.alineacion;
                        palineacion.Direction = ParameterDirection.Input;
                        palineacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(palineacion);

                        DbParameter pancho = cmdTransaccionFactory.CreateParameter();
                        pancho.ParameterName = "p_ancho";
                        pancho.Value = pColumna.ancho;
                        pancho.Direction = ParameterDirection.Input;
                        pancho.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pancho);

                        DbParameter ptotal = cmdTransaccionFactory.CreateParameter();
                        ptotal.ParameterName = "p_total";
                        ptotal.Value = pColumna.total;
                        ptotal.Direction = ParameterDirection.Input;
                        ptotal.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptotal);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_COLUMNA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pColumna;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ColumnaData", "ModificarColumna", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarColumna(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcolumna = cmdTransaccionFactory.CreateParameter();
                        pidcolumna.ParameterName = "p_idcolumna";
                        pidcolumna.Value = pId;
                        pidcolumna.Direction = ParameterDirection.Input;
                        pidcolumna.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcolumna);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_COLUMNA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ColumnaData", "EliminarColumna", ex);
                    }
                }
            }
        }

        public List<ColumnaReporte> ListarColumna(ColumnaReporte pColumnaReporte, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ColumnaReporte> lstColumnaReporte = new List<ColumnaReporte>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM reporte_Columna " + ObtenerFiltro(pColumnaReporte) + " ORDER BY ORDEN ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ColumnaReporte entidad = new ColumnaReporte();
                            if (resultado["IDCOLUMNA"] != DBNull.Value) entidad.idcolumna = Convert.ToInt32(resultado["IDCOLUMNA"]);
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["ORDEN"] != DBNull.Value) entidad.orden = Convert.ToInt32(resultado["ORDEN"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["TABLA"] != DBNull.Value) entidad.tabla = Convert.ToString(resultado["TABLA"]);
                            if (resultado["COLUMNA"] != DBNull.Value) entidad.columna = Convert.ToString(resultado["COLUMNA"]);
                            if (resultado["TITULO"] != DBNull.Value) entidad.titulo = Convert.ToString(resultado["TITULO"]);
                            if (resultado["FORMATO"] != DBNull.Value) entidad.formato = Convert.ToString(resultado["FORMATO"]);
                            if (resultado["TIPODATO"] != DBNull.Value) entidad.tipodato = Convert.ToString(resultado["TIPODATO"]);
                            if (resultado["ALINEACION"] != DBNull.Value) entidad.alineacion = Convert.ToString(resultado["ALINEACION"]);
                            if (resultado["ANCHO"] != DBNull.Value) entidad.ancho = Convert.ToInt32(resultado["ANCHO"]);
                            if (resultado["TOTAL"] != DBNull.Value)
                                if (Convert.ToString(resultado["TOTAL"]) == "1")
                                    entidad.total = true;
                                else
                                    entidad.total = false;
                            if (resultado["FORMULA"] != DBNull.Value) entidad.formula = Convert.ToString(resultado["FORMULA"]);
                            lstColumnaReporte.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstColumnaReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ColumnaData", "ListarColumna", ex);
                        return null;
                    }
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Orden CrearOrden(Orden pOrden, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidorden = cmdTransaccionFactory.CreateParameter();
                        pidorden.ParameterName = "p_idorden";
                        pidorden.Value = pOrden.idorden;
                        pidorden.Direction = ParameterDirection.Output;
                        pidorden.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidorden);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pOrden.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter ptabla = cmdTransaccionFactory.CreateParameter();
                        ptabla.ParameterName = "p_tabla";
                        ptabla.Value = pOrden.tabla;
                        ptabla.Direction = ParameterDirection.Input;
                        ptabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla);

                        DbParameter pcolumna = cmdTransaccionFactory.CreateParameter();
                        pcolumna.ParameterName = "p_columna";
                        pcolumna.Value = pOrden.columna;
                        pcolumna.Direction = ParameterDirection.Input;
                        pcolumna.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna);

                        DbParameter porden = cmdTransaccionFactory.CreateParameter();
                        porden.ParameterName = "p_orden";
                        porden.Value = pOrden.orden;
                        porden.Direction = ParameterDirection.Input;
                        porden.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(porden);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_ORDEN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pOrden.idorden = Convert.ToInt64(pidorden.Value);

                        return pOrden;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OrdenData", "CrearOrden", ex);
                        return null;
                    }
                }
            }
        }

        public Orden ModificarOrden(Orden pOrden, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidorden = cmdTransaccionFactory.CreateParameter();
                        pidorden.ParameterName = "p_idorden";
                        pidorden.Value = pOrden.idorden;
                        pidorden.Direction = ParameterDirection.Input;
                        pidorden.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidorden);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pOrden.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter ptabla = cmdTransaccionFactory.CreateParameter();
                        ptabla.ParameterName = "p_tabla";
                        ptabla.Value = pOrden.tabla;
                        ptabla.Direction = ParameterDirection.Input;
                        ptabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla);

                        DbParameter pcolumna = cmdTransaccionFactory.CreateParameter();
                        pcolumna.ParameterName = "p_columna";
                        pcolumna.Value = pOrden.columna;
                        pcolumna.Direction = ParameterDirection.Input;
                        pcolumna.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna);

                        DbParameter porden = cmdTransaccionFactory.CreateParameter();
                        porden.ParameterName = "p_orden";
                        porden.Value = pOrden.orden;
                        porden.Direction = ParameterDirection.Input;
                        porden.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(porden);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_ORDEN_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pOrden;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OrdenData", "ModificarOrden", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarOrden(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidorden = cmdTransaccionFactory.CreateParameter();
                        pidorden.ParameterName = "p_idorden";
                        pidorden.Value = pId;
                        pidorden.Direction = ParameterDirection.Input;
                        pidorden.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidorden);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_ORDEN_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OrdenData", "EliminarOrden", ex);
                    }
                }
            }
        }

        public List<Orden> ListarOrden(Orden pOrden, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Orden> lstOrden = new List<Orden>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM reporte_Orden " + ObtenerFiltro(pOrden) + " ORDER BY IDORDEN ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Orden entidad = new Orden();
                            if (resultado["IDORDEN"] != DBNull.Value) entidad.idorden = Convert.ToInt32(resultado["IDORDEN"]);
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["TABLA"] != DBNull.Value) entidad.tabla = Convert.ToString(resultado["TABLA"]);
                            if (resultado["COLUMNA"] != DBNull.Value) entidad.columna = Convert.ToString(resultado["COLUMNA"]);
                            if (resultado["ORDEN"] != DBNull.Value) entidad.orden = Convert.ToString(resultado["ORDEN"]);
                            lstOrden.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstOrden;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("OrdenData", "ListarOrden", ex);
                        return null;
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public Grupo CrearGrupo(Grupo pGrupo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgrupo = cmdTransaccionFactory.CreateParameter();
                        pidgrupo.ParameterName = "p_idgrupo";
                        pidgrupo.Value = pGrupo.idgrupo;
                        pidgrupo.Direction = ParameterDirection.Output;
                        pidgrupo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgrupo);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pGrupo.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter ptabla = cmdTransaccionFactory.CreateParameter();
                        ptabla.ParameterName = "p_tabla";
                        ptabla.Value = pGrupo.tabla;
                        ptabla.Direction = ParameterDirection.Input;
                        ptabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla);

                        DbParameter pcolumna = cmdTransaccionFactory.CreateParameter();
                        pcolumna.ParameterName = "p_columna";
                        pcolumna.Value = pGrupo.columna;
                        pcolumna.Direction = ParameterDirection.Input;
                        pcolumna.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_GRUPO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pGrupo.idgrupo = Convert.ToInt64(pidgrupo.Value);

                        return pGrupo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoData", "CrearGrupo", ex);
                        return null;
                    }
                }
            }
        }

        public Grupo ModificarGrupo(Grupo pGrupo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgrupo = cmdTransaccionFactory.CreateParameter();
                        pidgrupo.ParameterName = "p_idgrupo";
                        pidgrupo.Value = pGrupo.idgrupo;
                        pidgrupo.Direction = ParameterDirection.Input;
                        pidgrupo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgrupo);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pGrupo.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter ptabla = cmdTransaccionFactory.CreateParameter();
                        ptabla.ParameterName = "p_tabla";
                        ptabla.Value = pGrupo.tabla;
                        ptabla.Direction = ParameterDirection.Input;
                        ptabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptabla);

                        DbParameter pcolumna = cmdTransaccionFactory.CreateParameter();
                        pcolumna.ParameterName = "p_columna";
                        pcolumna.Value = pGrupo.columna;
                        pcolumna.Direction = ParameterDirection.Input;
                        pcolumna.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcolumna);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_GRUPO_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pGrupo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoData", "ModificarGrupo", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarGrupo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgrupo = cmdTransaccionFactory.CreateParameter();
                        pidgrupo.ParameterName = "p_idgrupo";
                        pidgrupo.Value = pId;
                        pidgrupo.Direction = ParameterDirection.Input;
                        pidgrupo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgrupo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_GRUPO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoData", "EliminarGrupo", ex);
                    }
                }
            }
        }

        public List<Grupo> ListarGrupo(Grupo pGrupo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Grupo> lstGrupo = new List<Grupo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM reporte_Grupo " + ObtenerFiltro(pGrupo) + " ORDER BY IDGRUPO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Grupo entidad = new Grupo();
                            if (resultado["IDGRUPO"] != DBNull.Value) entidad.idgrupo = Convert.ToInt32(resultado["IDGRUPO"]);
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["TABLA"] != DBNull.Value) entidad.tabla = Convert.ToString(resultado["TABLA"]);
                            if (resultado["COLUMNA"] != DBNull.Value) entidad.columna = Convert.ToString(resultado["COLUMNA"]);
                            lstGrupo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGrupo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GrupoData", "ListarGrupo", ex);
                        return null;
                    }
                }
            }
        }

        public List<Xpinn.Reporteador.Entities.Lista> ListarLista(Xpinn.Reporteador.Entities.Lista pLista, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Lista> lstLista = new List<Lista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Reporte_lista " + ObtenerFiltro(pLista) + " ORDER BY IDLISTA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Lista entidad = new Lista();
                            if (resultado["IDLISTA"] != DBNull.Value) entidad.idlista = Convert.ToInt32(resultado["IDLISTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TEXTFIELD"] != DBNull.Value) entidad.textfield = Convert.ToString(resultado["TEXTFIELD"]);
                            if (resultado["VALUEFIELD"] != DBNull.Value) entidad.valuefield = Convert.ToString(resultado["VALUEFIELD"]);
                            if (resultado["SENTENCIA"] != DBNull.Value) entidad.sentencia = Convert.ToString(resultado["SENTENCIA"]);
                            lstLista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListaData", "ListarLista", ex);
                        return null;
                    }
                }
            }
        }

        public List<Xpinn.Reporteador.Entities.Lista> ListarReporteLista(string filtro, Xpinn.Reporteador.Entities.Lista pLista, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Lista> lstLista = new List<Lista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Reporte_lista " + filtro + " ORDER BY IDLISTA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Lista entidad = new Lista();
                            if (resultado["IDLISTA"] != DBNull.Value) entidad.idlista = Convert.ToInt32(resultado["IDLISTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TEXTFIELD"] != DBNull.Value) entidad.textfield = Convert.ToString(resultado["TEXTFIELD"]);
                            if (resultado["VALUEFIELD"] != DBNull.Value) entidad.valuefield = Convert.ToString(resultado["VALUEFIELD"]);
                            if (resultado["SENTENCIA"] != DBNull.Value) entidad.sentencia = Convert.ToString(resultado["SENTENCIA"]);
                            lstLista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListaData", "ListarLista", ex);
                        return null;
                    }
                }
            }
        }


        public Lista ConsultarLista(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Lista entidad = new Lista();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Reporte_lista WHERE IDLISTA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDLISTA"] != DBNull.Value) entidad.idlista = Convert.ToInt32(resultado["IDLISTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TEXTFIELD"] != DBNull.Value) entidad.textfield = Convert.ToString(resultado["TEXTFIELD"]);
                            if (resultado["VALUEFIELD"] != DBNull.Value) entidad.valuefield = Convert.ToString(resultado["VALUEFIELD"]);
                            if (resultado["SENTENCIA"] != DBNull.Value) entidad.sentencia = Convert.ToString(resultado["SENTENCIA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListaData", "ConsultarLista", ex);
                        return null;
                    }
                }
            }
        }

        public Plantilla CrearPlantilla(Plantilla pPlantilla, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidplantilla = cmdTransaccionFactory.CreateParameter();
                        pidplantilla.ParameterName = "p_idplantilla";
                        pidplantilla.Value = pPlantilla.idplantilla;
                        pidplantilla.Direction = ParameterDirection.Input;
                        pidplantilla.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidplantilla);

                        DbParameter pidreporte = cmdTransaccionFactory.CreateParameter();
                        pidreporte.ParameterName = "p_idreporte";
                        pidreporte.Value = pPlantilla.idreporte;
                        pidreporte.Direction = ParameterDirection.Input;
                        pidreporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidreporte);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pPlantilla.descripcion == "")
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pPlantilla.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter parchivo = cmdTransaccionFactory.CreateParameter();
                        parchivo.ParameterName = "p_archivo";
                        parchivo.Value = pPlantilla.archivo;
                        parchivo.Direction = ParameterDirection.Input;
                        parchivo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(parchivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_PLANTILLA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPlantilla;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "CrearPlantilla", ex);
                        return null;
                    }
                }
            }
        }

        public List<Plantilla> ListarPlantilla(Plantilla pPlantilla, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Plantilla> lstPlantilla = new List<Plantilla>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Reporte_Plantilla " + ObtenerFiltro(pPlantilla) + " ORDER BY IDPLANTILLA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Plantilla entidad = new Plantilla();
                            if (resultado["IDPLANTILLA"] != DBNull.Value) entidad.idplantilla = Convert.ToInt32(resultado["IDPLANTILLA"]);
                            if (resultado["IDREPORTE"] != DBNull.Value) entidad.idreporte = Convert.ToInt32(resultado["IDREPORTE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ARCHIVO"] != DBNull.Value) entidad.archivo = Convert.ToString(resultado["ARCHIVO"]);
                            lstPlantilla.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPlantilla;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarPlantilla", ex);
                        return null;
                    }
                }
            }
        }




        public Lista ConsultarReporteLista(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Lista entidad = new Lista();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Reporte_lista WHERE IDLISTA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDLISTA"] != DBNull.Value) entidad.idlista = Convert.ToInt32(resultado["IDLISTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TEXTFIELD"] != DBNull.Value) entidad.textfield = Convert.ToString(resultado["TEXTFIELD"]);
                            if (resultado["VALUEFIELD"] != DBNull.Value) entidad.valuefield = Convert.ToString(resultado["VALUEFIELD"]);
                            if (resultado["SENTENCIA"] != DBNull.Value) entidad.sentencia = Convert.ToString(resultado["SENTENCIA"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ConsultarReporte", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarReporteLista(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Lista pReporte = new Lista();
                        pReporte = ConsultarReporteLista(pId, vUsuario);

                        DbParameter pidlista = cmdTransaccionFactory.CreateParameter();
                        pidlista.ParameterName = "p_idlista";
                        pidlista.Value = pReporte.idlista;
                        pidlista.Direction = ParameterDirection.Input;
                        pidlista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidlista);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_REPORTE_LI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "EliminarReporte", ex);
                    }
                }
            }
        }



        public Lista CrearReporteLista(Lista pReporte, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidlista = cmdTransaccionFactory.CreateParameter();
                        pidlista.ParameterName = "p_idlista";
                        pidlista.Value = pReporte.idlista;
                        pidlista.Direction = ParameterDirection.Input;
                        pidlista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidlista);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pReporte.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pReporte.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptextfield = cmdTransaccionFactory.CreateParameter();
                        ptextfield.ParameterName = "p_textfield";
                        if (pReporte.textfield == null)
                            ptextfield.Value = DBNull.Value;
                        else
                            ptextfield.Value = pReporte.textfield;
                        ptextfield.Direction = ParameterDirection.Input;
                        ptextfield.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptextfield);

                        DbParameter pvaluefield = cmdTransaccionFactory.CreateParameter();
                        pvaluefield.ParameterName = "p_valuefield";
                        if (pReporte.valuefield == null)
                            pvaluefield.Value = DBNull.Value;
                        else
                            pvaluefield.Value = pReporte.valuefield;
                        pvaluefield.Direction = ParameterDirection.Input;
                        pvaluefield.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvaluefield);

                        DbParameter psentencia = cmdTransaccionFactory.CreateParameter();
                        psentencia.ParameterName = "p_sentencia";
                        if (pReporte.sentencia == null)
                            psentencia.Value = DBNull.Value;
                        else
                            psentencia.Value = pReporte.sentencia;
                        psentencia.Direction = ParameterDirection.Input;
                        psentencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psentencia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_REPORTE_LI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "CrearReporte", ex);
                        return null;
                    }
                }
            }
        }


        public Lista ModificarReporteLista(Lista pReporte, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidlista = cmdTransaccionFactory.CreateParameter();
                        pidlista.ParameterName = "p_idlista";
                        pidlista.Value = pReporte.idlista;
                        pidlista.Direction = ParameterDirection.Input;
                        pidlista.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidlista);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pReporte.descripcion == null)
                            pdescripcion.Value = DBNull.Value;
                        else
                            pdescripcion.Value = pReporte.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter ptextfield = cmdTransaccionFactory.CreateParameter();
                        ptextfield.ParameterName = "p_textfield";
                        if (pReporte.textfield == null)
                            ptextfield.Value = DBNull.Value;
                        else
                            ptextfield.Value = pReporte.textfield;
                        ptextfield.Direction = ParameterDirection.Input;
                        ptextfield.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptextfield);

                        DbParameter pvaluefield = cmdTransaccionFactory.CreateParameter();
                        pvaluefield.ParameterName = "p_valuefield";
                        if (pReporte.valuefield == null)
                            pvaluefield.Value = DBNull.Value;
                        else
                            pvaluefield.Value = pReporte.valuefield;
                        pvaluefield.Direction = ParameterDirection.Input;
                        pvaluefield.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvaluefield);

                        DbParameter psentencia = cmdTransaccionFactory.CreateParameter();
                        psentencia.ParameterName = "p_sentencia";
                        if (pReporte.sentencia == null)
                            psentencia.Value = DBNull.Value;
                        else
                            psentencia.Value = pReporte.sentencia;
                        psentencia.Direction = ParameterDirection.Input;
                        psentencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(psentencia);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REP_REPORTE_LI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ModificarReporte", ex);
                        return null;
                    }
                }
            }
        }


        public List<TransaccionEfectivo> ListarfechaCierrHist(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DateTime fecultcie = DateTime.MinValue;
            List<TransaccionEfectivo> lstFechaCierre = new List<TransaccionEfectivo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (pUsuario != null)
                        {
                            sql = "select fecha from CIEREA where tipo ='A' and estado = 'D' order by fecha desc";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TransaccionEfectivo entidad = new TransaccionEfectivo();
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha_tran = Convert.ToDateTime(resultado["fecha"].ToString());
                            lstFechaCierre.Add(entidad);
                        }

                        return lstFechaCierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarfechaCierrHist", ex);
                        return null;
                    }
                }
            }
        }

        public List<Credito> ListarCreditosDesembolsados(Credito pCredito, Usuario pUsuario, String filtro)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Credito> lstCredito = new List<Credito>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pCredito.check == 1)
                        {
                            sql = @"select DISTINCT T5.NOMBRE, t2.numero_radicacion, T1.identificacion, T1.primer_apellido, T3.NOMBRE AS LINEACRE,
                            T1.segundo_apellido, T1.primer_nombre, T1.segundo_nombre, T2.fecha_desembolso, T2.monto_desembolsado, T2.monto_aprobado
                            from persona T1 
                            inner join credito T2 on t2.cod_deudor = t1.cod_persona
                            inner join lineascredito T3 on T2.COD_LINEA_CREDITO = T3.COD_LINEA_CREDITO
                            inner join historico_cre T4 on T1.COD_PERSONA = T4.COD_CLIENTE
                            inner join oficina T5 on T2.COD_OFICINA = T5.COD_OFICINA 
                            where t2.monto_desembolsado > 0 and T2.estado IN ('C','T') " + filtro + " order by t2.fecha_desembolso desc";
                        }
                        else
                        {
                            sql = @"select DISTINCT T5.NOMBRE, t2.numero_radicacion, T1.identificacion, T1.primer_apellido, T3.NOMBRE AS LINEACRE,
                            T1.segundo_apellido, T1.primer_nombre, T1.segundo_nombre, T2.fecha_desembolso, T2.monto_desembolsado, T2.monto_aprobado
                            from persona T1 
                            inner join credito T2 on t2.cod_deudor = t1.cod_persona
                            inner join lineascredito T3 on T2.COD_LINEA_CREDITO = T3.COD_LINEA_CREDITO
                            inner join oficina T5 on T2.COD_OFICINA = T5.COD_OFICINA 
                            where t2.monto_desembolsado > 0 and T2.estado IN ('C','T') " + filtro + " order by t2.fecha_desembolso desc";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Credito entidad = new Credito();

                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["LINEACRE"] != DBNull.Value) entidad.nom_linea_credito = Convert.ToString(resultado["LINEACRE"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["primer_apellido"] != DBNull.Value) entidad.primer_apellido = Convert.ToString(resultado["primer_apellido"]);
                            if (resultado["segundo_apellido"] != DBNull.Value) entidad.segundo_apellido = Convert.ToString(resultado["segundo_apellido"]);
                            if (resultado["primer_nombre"] != DBNull.Value) entidad.primer_nombre = Convert.ToString(resultado["primer_nombre"]);
                            if (resultado["segundo_nombre"] != DBNull.Value) entidad.segundo_nombre = Convert.ToString(resultado["segundo_nombre"]);
                            if (resultado["fecha_desembolso"] != DBNull.Value) entidad.fecha_desembolso = Convert.ToDateTime(resultado["fecha_desembolso"]);
                            if (resultado["monto_desembolsado"] != DBNull.Value) entidad.monto_desembolsado = Convert.ToInt64(resultado["monto_desembolsado"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            lstCredito.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreditoData", "ListarCredito", ex);
                        return null;
                    }
                }
            }
        }
    }
}