using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Data
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




        public List<ReporteMovimiento> ListarDropDownLineasAhorro(ReporteMovimiento pReport, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReporteMovimiento> lstLineas = new List<ReporteMovimiento>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM LINEAAHORRO " + ObtenerFiltro(pReport) + " ORDER BY COD_LINEA_AHORRO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ReporteMovimiento entidad = new ReporteMovimiento();
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToInt32(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstLineas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteMovimientoData", "ListarDropDownLineasAhorro", ex);
                        return null;
                    }
                }
            }
        }


        public List<AhorroVista> ListarAhorroVista(string pFiltro, DateTime pFecha, Usuario vUsuario, int EstadoCuenta = 0)
        {
            DbDataReader resultado;

            string sql = "";
            List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        if (EstadoCuenta != 0)
                        {
                            sql = @"select A.NUMERO_CUENTA,A.COD_LINEA_AHORRO,A.COD_PERSONA,A.COD_OFICINA,A.COD_DESTINO,A.OBSERVACIONES,A.MODALIDAD,A.ESTADO,A.FECHA_APERTURA,A.FECHA_CIERRE,h.SALDO_TOTAL,A.SALDO_CANJE,
                        A.FORMA_TASA,A.TIPO_HISTORICO,A.DESVIACION,A.TIPO_TASA,A.TASA,A.FECHA_INTERES,A.SALDO_INTERESES,A.RETENCION,A.COD_FORMA_PAGO,H.FECHA_PROXIMO_PAGO,A.FECHA_ULTIMO_PAGO,
                        A.VALOR_CUOTA,   Calcular_VrAPagarAhorro(A.NUMERO_CUENTA, To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"')) As ValorAPagar,
                        A.COD_PERIODICIDAD,A.COD_ASESOR,A.COD_EMPRESA,A.COD_LINEA_AHORRO||' - '||L.DESCRIPCION AS NOM_LINEA,V.IDENTIFICACION,V.NOMBRE, V.COD_NOMINA, O.NOMBRE AS NOM_OFICINA ,
                        CASE A.ESTADO WHEN 0 THEN 'APERTURA' WHEN 1 THEN 'ACTIVA' WHEN 2 THEN 'INACTIVO' WHEN 3 THEN 'CERRADA'   WHEN 4 THEN 'BLOQUEADA' WHEN 5 THEN 'EMBARGADA'  END AS NOM_ESTADO,CASE A.COD_FORMA_PAGO WHEN 1 THEN 'CAJA' WHEN 2 THEN 'NOMINA' END AS NOM_FORMAPAGO,
                        P.DESCRIPCION AS NOM_PERIODICIDAD,SALDO_ACUMULADO(3,A.NUMERO_CUENTA) AS VALOR_ACUMULADO
                        FROM AHORRO_VISTA A LEFT JOIN LINEAAHORRO L ON L.COD_LINEA_AHORRO = A.COD_LINEA_AHORRO
                        INNER JOIN HISTORICO_AHORRO H ON H.NUMERO_CUENTA = A.NUMERO_CUENTA
                        LEFT JOIN V_PERSONA V ON V.COD_PERSONA = A.COD_PERSONA
                        LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA
                        LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = A.COD_PERIODICIDAD " + pFiltro;
                        }
                        else
                        {
                            sql = @"select A.NUMERO_CUENTA,A.COD_LINEA_AHORRO,A.COD_PERSONA,A.COD_OFICINA,A.COD_DESTINO,A.OBSERVACIONES,A.MODALIDAD,A.ESTADO,A.FECHA_APERTURA,A.FECHA_CIERRE,A.SALDO_TOTAL,A.SALDO_CANJE,
                        A.FORMA_TASA,A.TIPO_HISTORICO,A.DESVIACION,A.TIPO_TASA,A.TASA,A.FECHA_INTERES,A.SALDO_INTERESES,A.RETENCION,A.COD_FORMA_PAGO,A.FECHA_PROXIMO_PAGO,A.FECHA_ULTIMO_PAGO,
                        A.VALOR_CUOTA,   Calcular_VrAPagarAhorro(A.NUMERO_CUENTA, To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"')) As ValorAPagar,
                        A.COD_PERIODICIDAD,A.COD_ASESOR,A.COD_EMPRESA,A.COD_LINEA_AHORRO||' - '||L.DESCRIPCION AS NOM_LINEA,V.IDENTIFICACION,V.NOMBRE, V.COD_NOMINA, O.NOMBRE AS NOM_OFICINA ,
                        CASE A.ESTADO WHEN 0 THEN 'APERTURA' WHEN 1 THEN 'ACTIVA' WHEN 2 THEN 'INACTIVO' WHEN 3 THEN 'CERRADA'   WHEN 4 THEN 'BLOQUEADA' WHEN 5 THEN 'EMBARGADA'  END AS NOM_ESTADO,CASE A.COD_FORMA_PAGO WHEN 1 THEN 'CAJA' WHEN 2 THEN 'NOMINA' END AS NOM_FORMAPAGO,
                        P.DESCRIPCION AS NOM_PERIODICIDAD,SALDO_ACUMULADO(3,A.NUMERO_CUENTA) AS VALOR_ACUMULADO
                        FROM AHORRO_VISTA A LEFT JOIN LINEAAHORRO L ON L.COD_LINEA_AHORRO = A.COD_LINEA_AHORRO
                        LEFT JOIN V_PERSONA V ON V.COD_PERSONA = A.COD_PERSONA
                        LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA
                        LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = A.COD_PERIODICIDAD " + pFiltro;
                        }

                        /*if (pFecha != null && pFecha != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA_INGRESO >= To_Date('" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA_INGRESO >= '" + Convert.ToDateTime(pFecha).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                         */
                        sql += " ORDER BY A.NUMERO_CUENTA";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            entidad.saldo_total = Convert.ToDecimal(entidad.saldo_total - entidad.saldo_canje);

                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            //////
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["NOM_FORMAPAGO"] != DBNull.Value) entidad.nom_formapago = Convert.ToString(resultado["NOM_FORMAPAGO"]);
                            if (resultado["NOM_PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["NOM_PERIODICIDAD"]);
                            if (resultado["VALOR_ACUMULADO"] != DBNull.Value) entidad.valor_acumulado = Convert.ToDecimal(resultado["VALOR_ACUMULADO"]);
                            if (entidad.valor_acumulado > 0)
                            {
                                entidad.valor_total_acumu = entidad.saldo_total + entidad.valor_acumulado;
                            }

                            if (resultado["ValorAPagar"] != DBNull.Value) entidad.valor_pagar = Convert.ToDecimal(resultado["ValorAPagar"]);


                            lstAhorroVista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarAhorroVista", ex);
                        return null;
                    }
                }
            }
        }


        public List<AhorroVista> ListarAhorroVistaClubAhorrador(Int64 pCod_persona, string pFiltro, Boolean pResult, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstAhorroVista = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT A.*,A.COD_LINEA_AHORRO||' - '||L.DESCRIPCION AS NOM_LINEA,V.IDENTIFICACION,V.NOMBRE, O.NOMBRE AS NOM_OFICINA , 
                                    CASE A.ESTADO WHEN 0 THEN 'APERTURA' WHEN 1 THEN 'ACTIVA' WHEN 2 THEN 'INACTIVO' WHEN 3 THEN 'CERRADA'  WHEN 4 THEN 'BLOQUEADA' WHEN 5 THEN 'EMBARGADA'  END AS NOM_ESTADO, 
                                    CASE A.COD_FORMA_PAGO WHEN 1 THEN 'CAJA' WHEN 2 THEN 'NOMINA' END AS NOM_FORMAPAGO, P.DESCRIPCION AS NOM_PERIODICIDAD ,'PROPIO' AS TIPO_AHORRO
                                    ,SALDO_ACUMULADO(3,A.NUMERO_CUENTA) AS VALOR_ACUMULADO, R.NOM_EMPRESA EMPRESA_RECA, B.Nombre_Ben
                                    FROM AHORRO_VISTA A LEFT JOIN LINEAAHORRO L ON L.COD_LINEA_AHORRO = A.COD_LINEA_AHORRO 
                                    LEFT join Beneficiario_Ahorrovista b on A.Numero_Cuenta = B.Numero_Cuenta
                                    LEFT JOIN V_PERSONA V ON V.COD_PERSONA = A.COD_PERSONA 
                                    LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA 
                                    LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = A.COD_PERIODICIDAD
                                    LEFT JOIN EMPRESA_RECAUDO R ON A.COD_EMPRESA = R.COD_EMPRESA
                                    WHERE A.COD_PERSONA = " + pCod_persona.ToString() + pFiltro;
                        if (!pResult)
                        {
                            sql += @" UNION ALL 
                                    SELECT A.*,A.COD_LINEA_AHORRO||' - '||L.DESCRIPCION AS NOM_LINEA,V.IDENTIFICACION,V.NOMBRE, O.NOMBRE AS NOM_OFICINA , 
                                    CASE A.ESTADO WHEN 0 THEN 'APERTURA' WHEN 1 THEN 'ACTIVA' WHEN 2 THEN 'INACTIVO' WHEN 3 THEN 'CERRADA'  END AS NOM_ESTADO, 
                                    CASE A.COD_FORMA_PAGO WHEN 1 THEN 'CAJA' WHEN 2 THEN 'NOMINA' END AS NOM_FORMAPAGO, P.DESCRIPCION AS NOM_PERIODICIDAD , 'CLUB AHORRADOR' AS TIPO_AHORRO
                                    ,SALDO_ACUMULADO(3,A.NUMERO_CUENTA) AS VALOR_ACUMULADO, R.NOM_EMPRESA EMPRESA_RECA, B.Nombre_Ben
                                    FROM AHORRO_VISTA A LEFT JOIN LINEAAHORRO L ON L.COD_LINEA_AHORRO = A.COD_LINEA_AHORRO
                                    LEFT join Beneficiario_Ahorrovista b on A.Numero_Cuenta = B.Numero_Cuenta
                                    LEFT JOIN V_PERSONA V ON V.COD_PERSONA = A.COD_PERSONA
                                    LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA
                                    LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = A.COD_PERIODICIDAD
                                    LEFT JOIN EMPRESA_RECAUDO R ON A.COD_EMPRESA = R.COD_EMPRESA
                                    WHERE A.COD_PERSONA IN (SELECT R.COD_PERSONA FROM PERSONA_RESPONSABLE R WHERE R.COD_PERSONA_TUTOR = " + pCod_persona.ToString() + ") " + pFiltro.ToString();
                        }

                        sql += " ORDER BY 8 DESC, 1 DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            entidad.saldo_total = Convert.ToDecimal(entidad.saldo_total - entidad.saldo_canje);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["NOM_FORMAPAGO"] != DBNull.Value) entidad.nom_formapago = Convert.ToString(resultado["NOM_FORMAPAGO"]);
                            if (resultado["NOM_PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["NOM_PERIODICIDAD"]);
                            if (resultado["TIPO_AHORRO"] != DBNull.Value) entidad.tipo_registro = Convert.ToString(resultado["TIPO_AHORRO"]);
                            if (resultado["VALOR_ACUMULADO"] != DBNull.Value) entidad.valor_acumulado = Convert.ToDecimal(resultado["VALOR_ACUMULADO"]);
                            if (resultado["EMPRESA_RECA"] != DBNull.Value) entidad.empresa = Convert.ToString(resultado["EMPRESA_RECA"]);
                            if (resultado["NOMBRE_BEN"] != DBNull.Value) entidad.nombres_ben = Convert.ToString(resultado["NOMBRE_BEN"]);
                            entidad.valor_total_acumu = (entidad.valor_acumulado + entidad.saldo_total);
                            lstAhorroVista.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAhorroVista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ListarAhorroVista", ex);
                        return null;
                    }
                }
            }
        }

        //Agregado para consultar las cuentas de ahorros a la vista que tenga una persona
        public List<AhorroVista> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AhorroVista> lstCuentas = new List<AhorroVista>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select A.COD_PERSONA, A.NUMERO_CUENTA, L.DESCRIPCION FROM AHORRO_VISTA A INNER JOIN LINEAAHORRO L ON A.COD_LINEA_AHORRO = L.COD_LINEA_AHORRO WHERE A.COD_PERSONA = " + pCod_Persona;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            AhorroVista entidad = new AhorroVista();
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["DESCRIPCION"]);

                            lstCuentas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteMovimientoData", "ListarCuentasPersona", ex);
                        return null;
                    }
                }
            }
        }


        public AhorroVista ConsultarAhorroVista(string pNumero_cuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            AhorroVista entidad = new AhorroVista();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT A.*,A.COD_LINEA_AHORRO||' - '||L.DESCRIPCION AS NOM_LINEA,V.IDENTIFICACION,V.NOMBRE, O.NOMBRE AS NOM_OFICINA , "
                                    + "CASE A.ESTADO WHEN 0 THEN 'APERTURA' WHEN 1 THEN 'ACTIVA' WHEN 2 THEN 'INACTIVO' WHEN 3 THEN 'CERRADA'  WHEN 4 THEN 'BLOQUEADA' WHEN 5 THEN 'EMBARGADA' END AS NOM_ESTADO, "
                                    + "CASE A.COD_FORMA_PAGO WHEN 1 THEN 'CAJA' WHEN 2 THEN 'NOMINA' END AS NOM_FORMAPAGO, P.DESCRIPCION AS NOM_PERIODICIDAD "
                                    + "FROM AHORRO_VISTA A LEFT JOIN LINEAAHORRO L ON L.COD_LINEA_AHORRO = A.COD_LINEA_AHORRO "
                                    + "LEFT JOIN V_PERSONA V ON V.COD_PERSONA = A.COD_PERSONA "
                                    + "LEFT JOIN OFICINA O ON O.COD_OFICINA = A.COD_OFICINA "
                                    + "LEFT JOIN PERIODICIDAD P ON P.COD_PERIODICIDAD = A.COD_PERIODICIDAD WHERE A.NUMERO_CUENTA = " + pNumero_cuenta.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["COD_LINEA_AHORRO"] != DBNull.Value) entidad.cod_linea_ahorro = Convert.ToString(resultado["COD_LINEA_AHORRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToInt32(resultado["MODALIDAD"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fecha_cierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["SALDO_CANJE"] != DBNull.Value) entidad.saldo_canje = Convert.ToDecimal(resultado["SALDO_CANJE"]);
                            if (resultado["FORMA_TASA"] != DBNull.Value) entidad.forma_tasa = Convert.ToInt32(resultado["FORMA_TASA"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToInt32(resultado["TIPO_HISTORICO"]);
                            if (resultado["DESVIACION"] != DBNull.Value) entidad.desviacion = Convert.ToDecimal(resultado["DESVIACION"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["FECHA_INTERES"] != DBNull.Value) entidad.fecha_interes = Convert.ToDateTime(resultado["FECHA_INTERES"]);
                            if (resultado["SALDO_INTERESES"] != DBNull.Value) entidad.saldo_intereses = Convert.ToDecimal(resultado["SALDO_INTERESES"]);
                            if (resultado["RETENCION"] != DBNull.Value) entidad.retencion = Convert.ToInt32(resultado["RETENCION"]);
                            if (resultado["COD_FORMA_PAGO"] != DBNull.Value) entidad.cod_forma_pago = Convert.ToInt32(resultado["COD_FORMA_PAGO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            //////
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["NOM_FORMAPAGO"] != DBNull.Value) entidad.nom_formapago = Convert.ToString(resultado["NOM_FORMAPAGO"]);
                            if (resultado["NOM_PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["NOM_PERIODICIDAD"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ConsultarAhorroVista", ex);
                        return null;
                    }
                }
            }
        }


        public List<ReporteMovimiento> ListarReporteMovimiento(Int64 pNumeroCuenta, DateTime? pFechaIni, DateTime? pFechaFin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ReporteMovimiento> lstMovimientos = new List<ReporteMovimiento>();
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
                        P_NUMERO_CUENTA.DbType = DbType.String;
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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_REPMOVIMIENTO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select ac.* From temp_movimientos ac Where ac.numero_radicacion = " + pNumeroCuenta.ToString() + " " + " Order by ac.consecutivo";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ReporteMovimiento entidad = new ReporteMovimiento();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["numero_radicacion"].ToString());
                            if (resultado["fecha_pago"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha_pago"].ToString());
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["cod_ope"].ToString());
                            if (resultado["tipo_ope"] != DBNull.Value) entidad.tipo_ope = Convert.ToString(resultado["tipo_ope"]);
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["tipo_mov"]);
                            if (resultado["tipo_tran"] != DBNull.Value) entidad.tipo_tran = Convert.ToString(resultado["tipo_tran"]);
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
                        BOExcepcion.Throw("AhorroVistaData", "ListarReporteMovimiento", ex);
                        return null;
                    }
                }
            }
        }

        public AhorroVista EliminarAhorroVista(string pNumero_cuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            AhorroVista entidad = new AhorroVista();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"delete temp_movimientos where numero_radicacion=" + pNumero_cuenta;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "EliminarAhorroVista", ex);
                        return null;
                    }
                }
            }
        }
        public SolicitudProductosWeb ConsultarSolicitud(Usuario vUsuario)
        {
            DbDataReader resultado;
            SolicitudProductosWeb entidad = new SolicitudProductosWeb();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "select max(IDSOLICITUD) as limit FROM SOLICITUDPRODUCTOWEB ORDER BY IDSOLICITUD DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "EliminarAhorroVista", ex);
                        return null;
                    }
                }
            }
        }
        public SolicitudProductosWeb CrearSolicitudProducto(SolicitudProductosWeb pProductos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_IDSOLICITUD = cmdTransaccionFactory.CreateParameter();
                        P_IDSOLICITUD.ParameterName = "P_IDSOLICITUD";
                        P_IDSOLICITUD.Value = pProductos.IDSOLICITUD;
                        P_IDSOLICITUD.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_IDSOLICITUD);

                        DbParameter P_TIPOAHORRO = cmdTransaccionFactory.CreateParameter();
                        P_TIPOAHORRO.ParameterName = "P_TIPOAHORRO";
                        P_TIPOAHORRO.Value = pProductos.TIPOAHORRO;
                        P_TIPOAHORRO.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_TIPOAHORRO);

                        DbParameter P_COD_LINEAAHORRO = cmdTransaccionFactory.CreateParameter();
                        P_COD_LINEAAHORRO.ParameterName = "P_COD_LINEAAHORRO";
                        P_COD_LINEAAHORRO.Value = pProductos.COD_LINEAAHORRO;
                        P_COD_LINEAAHORRO.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_COD_LINEAAHORRO);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = pProductos.COD_PERSONA;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter P_FECHA = cmdTransaccionFactory.CreateParameter();
                        P_FECHA.ParameterName = "P_FECHA";
                        P_FECHA.Value = pProductos.FECHA;
                        P_FECHA.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_FECHA);


                        DbParameter P_VALOR_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_CUOTA.ParameterName = "P_VALOR_CUOTA";
                        P_VALOR_CUOTA.Value = pProductos.VALOR_CUOTA;
                        P_VALOR_CUOTA.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_VALOR_CUOTA);

                        DbParameter P_PERIODICIDAD = cmdTransaccionFactory.CreateParameter();
                        P_PERIODICIDAD.ParameterName = "P_PERIODICIDAD";
                        P_PERIODICIDAD.Value = pProductos.PERIODICIDAD;
                        P_PERIODICIDAD.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_PERIODICIDAD);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = pProductos.ESTADO;
                        P_ESTADO.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        DbParameter P_FORMA_PAGO = cmdTransaccionFactory.CreateParameter();
                        P_FORMA_PAGO.ParameterName = "P_FORMA_PAGO";
                        P_FORMA_PAGO.Value = pProductos.FORMA_PAGO;
                        P_FORMA_PAGO.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_FORMA_PAGO);

                        DbParameter P_MODALIDAD = cmdTransaccionFactory.CreateParameter();
                        P_MODALIDAD.ParameterName = "P_MODALIDAD";
                        P_MODALIDAD.Value = pProductos.MODALIDAD;
                        P_MODALIDAD.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_MODALIDAD);

                        DbParameter P_OFICINA = cmdTransaccionFactory.CreateParameter();
                        P_OFICINA.ParameterName = "P_OFICINA";
                        P_OFICINA.Value = pProductos.OFICINA;
                        P_OFICINA.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_OFICINA);

                        DbParameter P_COD_ASESOR = cmdTransaccionFactory.CreateParameter();
                        P_COD_ASESOR.ParameterName = "P_COD_ASESOR";
                        P_COD_ASESOR.Value = pProductos.COD_ASESOR;
                        P_COD_ASESOR.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_COD_ASESOR);

                        DbParameter P_PLAZO = cmdTransaccionFactory.CreateParameter();
                        P_PLAZO.ParameterName = "P_PLAZO";
                        P_PLAZO.Value = pProductos.PLAZO;
                        P_PLAZO.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_PLAZO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_SOLIPRODUCT";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);



                        return pProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "CrearAhorroVista", ex);
                        return null;
                    }
                }
            }
        }
        public List<SolicitudProductosWeb> ListarSolicitudes(Usuario vUsuario, int producto)
        {
            DbDataReader resultado;
            List<SolicitudProductosWeb> lstConsulta = new List<SolicitudProductosWeb>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "select max(IDSOLICITUD) as limit FROM SOLICITUDPRODUCTOWEB ORDER BY IDSOLICITUD DESC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            SolicitudProductosWeb entidad = new SolicitudProductosWeb();
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                            if (resultado["limit"] != DBNull.Value) entidad.IDSOLICITUD = Convert.ToInt64(resultado["limit"].ToString());
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "EliminarAhorroVista", ex);
                        return null;
                    }
                }
            }
        }
    }
}
