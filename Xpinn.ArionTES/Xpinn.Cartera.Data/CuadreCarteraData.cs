using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Credito
    /// </summary>
    public class CuadreCarteraData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Credito
        /// </summary>
        public CuadreCarteraData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Crea un registro de la condonacion de interes de la base de datos
        /// </summary>
        /// <param name="pDiligencia">Entidad Condonacion</param>
        /// <returns>Entidad Condonacion creada</returns>
        public List<CuadreCartera> ConsultaCuadre(CuadreCartera pcuadre, int tipo_deudor, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuadreCartera> lstCuadre = new List<CuadreCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        DbParameter pFECHA_INICIAL = cmdTransaccionFactory.CreateParameter();
                        pFECHA_INICIAL.ParameterName = "pfecha_inicial";
                        pFECHA_INICIAL.DbType = DbType.Date;
                        pFECHA_INICIAL.Value = pcuadre.fecha_inicial.ToString(conf.ObtenerFormatoFecha());

                        DbParameter pFECHA_FINAL = cmdTransaccionFactory.CreateParameter();
                        pFECHA_FINAL.ParameterName = "pfecha_final";
                        pFECHA_FINAL.DbType = DbType.Date;
                        pFECHA_FINAL.Value = pcuadre.fecha_final.ToString(conf.ObtenerFormatoFecha());

                        DbParameter pTIPOPRODUCTO = cmdTransaccionFactory.CreateParameter();
                        pTIPOPRODUCTO.ParameterName = "pTipoProducto";
                        pTIPOPRODUCTO.DbType = DbType.Int64;
                        pTIPOPRODUCTO.Value = pcuadre.tipo_producto;

                        DbParameter pINCLUYE_EMPLEADOS = cmdTransaccionFactory.CreateParameter();
                        pINCLUYE_EMPLEADOS.ParameterName = "pIncluye_Empleados";
                        pINCLUYE_EMPLEADOS.DbType = DbType.Int64;
                        pINCLUYE_EMPLEADOS.Value = tipo_deudor;

                        cmdTransaccionFactory.Parameters.Add(pFECHA_INICIAL);
                        cmdTransaccionFactory.Parameters.Add(pFECHA_FINAL);
                        cmdTransaccionFactory.Parameters.Add(pTIPOPRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(pINCLUYE_EMPLEADOS);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRECONTABLE";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = @"Select t.*, a.nombre As nom_atr, tc.descripcion As nom_tipo_comp, t.valor_contable - t.valor_operativo As diferencia 
                                        From temp_diferencias t Left Join atributos a On t.cod_atr = a.cod_atr Left Join tipo_comp tc On t.tipo_comp = tc.tipo_comp Order by t.cod_atr, t.num_comp, t.tipo_comp";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuadreCartera entidad = new CuadreCartera();

                            if (resultado["fecha_inicial"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["fecha_inicial"].ToString());
                            if (resultado["fecha_final"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["fecha_final"].ToString());
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt64(resultado["cod_atr"].ToString());
                            if (resultado["nom_atr"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["nom_atr"].ToString());
                            if (resultado["num_comp"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["num_comp"].ToString());
                            if (resultado["tipo_comp"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["tipo_comp"].ToString());
                            if (resultado["nom_tipo_comp"] != DBNull.Value) entidad.nom_tipo_comp = Convert.ToString(resultado["nom_tipo_comp"].ToString());
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha"].ToString());
                            if (resultado["valor_contable"] != DBNull.Value) entidad.valor_contable = Convert.ToDecimal(resultado["valor_contable"].ToString());
                            if (resultado["valor_operativo"] != DBNull.Value) entidad.valor_operativo = Convert.ToDecimal(resultado["valor_operativo"].ToString());
                            if (resultado["diferencia"] != DBNull.Value) entidad.diferencia = Convert.ToDecimal(resultado["diferencia"].ToString());
                            if (resultado["tipo"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["tipo"].ToString());
                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"].ToString());
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_cuenta"].ToString());

                            lstCuadre.Add(entidad);
                        }

                        return lstCuadre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ConsultaCuadre", ex);
                        return null;
                    }
                }
            }
        }

        public List<CuadreCartera> ConsultarCuadreOperativoPorComprobante(CuadreCartera cuadre, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuadreCartera> lstEntidad = new List<CuadreCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT tran.cod_ope, tran.nombre_linea, tran.atributo, tran.tipo_tran, tran.tipo_mov, tran.valor, tran.cod_tipo_producto, tran.num_tran
                                        FROM v_transaccion tran 
                                        JOIN operacion ope on tran.cod_ope = ope.cod_ope
                                        WHERE ope.num_comp = " + cuadre.num_comp + @"
                                        AND ope.tipo_comp = " + cuadre.tipo_comp;
                        if (cuadre.tipo_producto_enum != TipoDeProducto.AhorrosVista && cuadre.tipo_producto_enum != TipoDeProducto.AhorroProgramado && cuadre.tipo_producto_enum != TipoDeProducto.CDATS)
                            sql += " AND tran.cod_atr = " + cuadre.cod_atr;
                        sql += " ORDER BY 6 desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuadreCartera entidad = new CuadreCartera();

                            if (resultado["cod_ope"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["cod_ope"]);
                            if (resultado["nombre_linea"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["nombre_linea"]);
                            if (resultado["atributo"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["atributo"]);
                            if (resultado["tipo_tran"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["tipo_tran"]);
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipo_mov_operativo = Convert.ToString(resultado["tipo_mov"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor_operativo = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["cod_tipo_producto"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt32(resultado["cod_tipo_producto"]);
                            if (resultado["num_tran"] != DBNull.Value) entidad.numero_transaccion = Convert.ToString(resultado["num_tran"]);

                            lstEntidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ConsultarCuadreOperativoPorComprobante", ex);
                        return null;
                    }
                }
            }
        }

        public List<CuadreCartera> ConsultarCuadreContablePorComprobante(CuadreCartera cuadre, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuadreCartera> lstEntidad = new List<CuadreCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string filtro = string.Empty;

                        switch (cuadre.tipo_producto_enum)
                        {
                            case TipoDeProducto.Aporte:
                                filtro = " AND cod_cuenta IN (Select par.cod_cuenta From PAR_CUE_LINAPO par Where (par.tipo = 0 Or par.tipo Is Null) And par.tipo_cuenta = 1 And par.cod_atr = " + (cuadre.cod_atr == 0 ? 1: cuadre.cod_atr) + " ) ";
                                break;
                            case TipoDeProducto.Credito:
                                filtro = " AND cod_cuenta IN (Select par.cod_cuenta From PAR_CUE_LINCRED par Where (par.tipo = 0 Or par.tipo Is Null) And par.tipo_cuenta = 1 And par.cod_atr = " + cuadre.cod_atr + " ) ";
                                break;
                            case TipoDeProducto.AhorrosVista:
                                filtro = " AND cod_cuenta IN (Select par.cod_cuenta From PAR_CUE_LINAHO par) ";
                                break;
                            case TipoDeProducto.Servicios:
                                filtro = " AND cod_cuenta IN (Select par.cod_cuenta From PAR_CUE_LINSER par Where (par.tipo = 0 Or par.tipo Is Null) And par.tipo_cuenta = 1 And par.cod_atr = " + cuadre.cod_atr + " ) ";
                                break;
                            case TipoDeProducto.CDATS:
                                filtro = " AND cod_cuenta IN (Select par.cod_cuenta From PAR_CUE_LINAHO par) ";
                                break;
                            case TipoDeProducto.Afiliacion:
                                break;
                            case TipoDeProducto.Otros:
                                filtro = " AND cod_cuenta IN (Select par.cod_cuenta From PAR_CUE_OTROS par) ";
                                break;
                            case TipoDeProducto.Devoluciones:
                                break;
                            case TipoDeProducto.AhorroProgramado:
                                filtro = " AND cod_cuenta IN (Select par.cod_cuenta From PAR_CUE_LINAHO par) ";
                                break;

                            case TipoDeProducto.ActivosFijos:
                                filtro = " AND cod_cuenta IN (Select par.COD_CUENTA_DEPRECIACION From TIPO_ACTIVO par WHERE COD_CUENTA_DEPRECIACION is not null) ";
                                break;
                        }

                        string sql = @"SELECT cod_cuenta, detalle, CASE tipo WHEN 'C' THEN 'Credito' WHEN 'D' THEN 'Debito' END as tipo_mov,
                                        centro_costo, valor, nom_moneda, centro_gestion, cod_tipo_producto, numero_transaccion 
                                        FROM d_comprobante 
                                        WHERE NUM_COMP = " + cuadre.num_comp  + @"
                                        AND TIPO_COMP  = " + cuadre.tipo_comp + @"
                                        AND cod_cuenta NOT IN (select cod_auxiliar from oficina_cuenta_contable)
                                        AND cod_cuenta NOT IN (select cod_auxiliar_contra from oficina_cuenta_contable) "
                                        + filtro + " ORDER BY 5 desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuadreCartera entidad = new CuadreCartera();

                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_cuenta"]);
                            if (resultado["detalle"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["detalle"]);
                            if (resultado["tipo_mov"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["tipo_mov"]);
                            if (resultado["centro_costo"] != DBNull.Value) entidad.cod_centro_costo = Convert.ToInt32(resultado["centro_costo"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor_contable = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["nom_moneda"] != DBNull.Value) entidad.nom_moneda = Convert.ToString(resultado["nom_moneda"]);
                            if (resultado["centro_gestion"] != DBNull.Value) entidad.cod_centro_gestion = Convert.ToInt32(resultado["centro_gestion"]);
                            if (resultado["cod_tipo_producto"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt32(resultado["cod_tipo_producto"]);
                            if (resultado["numero_transaccion"] != DBNull.Value) entidad.numero_transaccion = Convert.ToString(resultado["numero_transaccion"]);

                            lstEntidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ConsultarCuadreContablePorComprobante", ex);
                        return null;
                    }
                }
            }
        }

        public CuadreHistorico ConsultarSaldosYDiferenciaAportes(string fechaFinal, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            CuadreHistorico entidad = new CuadreHistorico();
            entidad.saldo_contable = 0;
            entidad.saldo_operativo = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select (Select Sum(b.valor) From balance b Where b.fecha = c.fecha And b.cod_cuenta In (Select Distinct p.cod_cuenta From par_cue_linapo p Join lineaaporte l On p.cod_linea_aporte = l.cod_linea_aporte Where (p.tipo = 0 Or p.tipo Is Null) And p.tipo_cuenta = 1 And l.tipo_aporte = 1)) As saldo_contable,
                                            (Select Sum(h.saldo) From historico_aporte h Inner Join aporte a On h.numero_aporte = a.numero_aporte Where h.fecha_historico = c.fecha And a.cod_linea_aporte In (Select Distinct p.cod_linea_aporte From par_cue_linapo p Join lineaaporte l On p.cod_linea_aporte = l.cod_linea_aporte Where (p.tipo = 0 Or p.tipo Is Null) And p.tipo_cuenta = 1 And l.tipo_aporte = 1)) As saldo_operativo
                                            From cierea c
                                            Where c.tipo = 'A' and c.fecha = to_date('" + fechaFinal + "','" + conf.ObtenerFormatoFecha() + "')";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO_CONTABLE"] != DBNull.Value) entidad.saldo_contable = Convert.ToDecimal(resultado["SALDO_CONTABLE"]);
                            if (resultado["SALDO_OPERATIVO"] != DBNull.Value) entidad.saldo_operativo = Convert.ToDecimal(resultado["SALDO_OPERATIVO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ConsultarSaldosYDiferenciaAportes", ex);
                        return null;
                    }
                }
            }
        }

        public CuadreHistorico ConsultarSaldosYDiferenciaCreditos(string fechaFinal, int tipo_deudor, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            CuadreHistorico entidad = new CuadreHistorico();
            entidad.saldo_contable = 0;
            entidad.saldo_operativo = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (EsPlanCuentasNIIF(pUsuario))
                        {
                            // Esto se hizo para tomar saldo de la cartera en plan de cuentas NIIF.
                            if (tipo_deudor == 1)  
                                sql = @"Select r.fecha,
                                        (Select Sum(b.valor) from balance b Where b.fecha = r.fecha And b.valor != 0
                                         and b.cod_cuenta Not In (Select p.depende_de From plan_cuentas p Where p.depende_de Is Not Null)
                                         and (b.cod_cuenta Like '14%' 
                                        -- Intereses y Deterioro Capital e Intereses. VIVIENDA 
                                        AND b.COD_CUENTA NOT LIKE '1406%' AND b.COD_CUENTA NOT LIKE '1408%' AND b.COD_CUENTA NOT LIKE '1409%' AND b.COD_CUENTA NOT LIKE '1410%' 
                                        -- Intereses y Deterioro Capital e Intereses. CONSUMO 
                                        AND b.COD_CUENTA NOT LIKE '1443%' AND b.COD_CUENTA NOT LIKE '1444%' AND b.COD_CUENTA NOT LIKE '1445%' AND b.COD_CUENTA NOT LIKE '1446%' 
                                        -- Intereses y Deterioro Capital e Intereses. MICROCREDITO 
                                        AND b.COD_CUENTA NOT LIKE '1456%' AND b.COD_CUENTA NOT LIKE '1458%' AND b.COD_CUENTA NOT LIKE '1459%' AND b.COD_CUENTA NOT LIKE '1460%'
                                        -- Intereses y Deterioro Capital e Intereses. COMERCIALES 
                                        AND b.COD_CUENTA NOT LIKE '1463%' AND b.COD_CUENTA NOT LIKE '1465%' AND b.COD_CUENTA NOT LIKE '1466%' AND b.COD_CUENTA NOT LIKE '1467%'
                                        -- Deterioro general de cartera.
                                        AND b.COD_CUENTA NOT LIKE '1468%' 
                                        -- Créditos a Empleados                
                                        AND b.COD_CUENTA NOT LIKE '1469%' AND b.COD_CUENTA NOT LIKE '1470%' AND b.COD_CUENTA NOT LIKE '1471%' AND b.COD_CUENTA NOT LIKE '1472%' 
                                        -- Otros convenios
                                        AND b.COD_CUENTA NOT LIKE '1473%')) As saldo_contable,
                                        (Select Sum(h.saldo_capital) From historico_cre h Inner Join lineascredito l On h.cod_linea_credito = l.cod_linea_credito
                                        Where h.fecha_historico = r.fecha and h.saldo_capital != 0 and h.cod_linea_credito Not In (Select cod_linea_credito from parametros_linea where cod_parametro = 320)
                                        and l.aplica_asociado = 1) as saldo_operativo
                                        From cierea r
                                        Where r.tipo = 'R' and r.fecha = to_date('" + fechaFinal + "','dd/mm/yyyy')";
                            else if(tipo_deudor == 2)
                                sql = @"Select r.fecha,
                                        (Select Sum(b.valor) from balance b Where b.fecha = r.fecha And b.valor != 0
                                        and b.cod_cuenta Not In (Select p.depende_de From plan_cuentas p Where p.depende_de Is Not Null) And (b.cod_cuenta Like '1469%' )) As saldo_contable,
                                        (Select Sum(h.saldo_capital) From historico_cre h Inner Join lineascredito l On h.cod_linea_credito = l.cod_linea_credito
                                        Where h.fecha_historico = r.fecha and h.saldo_capital != 0 and h.cod_linea_credito Not In (Select cod_linea_credito from parametros_linea where cod_parametro = 320)
                                        and (l.aplica_empleado = 1 or l.aplica_asociado != 1)) as saldo_operativo
                                        From cierea r
                                        Where r.tipo = 'R' and r.fecha = to_date('" + fechaFinal + "','dd/mm/yyyy')";
                        }
                        else if (EsPlanCuentasCOMERCIAL(pUsuario))
                        {
                            // Esto se hizo para tomar saldo de la cartera en plan de cuentas COMERCIAL.
                            sql = @"Select r.fecha,
                                    (Select Sum(b.valor) from balance b Where b.fecha = r.fecha And b.valor != 0
                                        and b.cod_cuenta Not In (Select p.depende_de From plan_cuentas p Where p.depende_de Is Not Null)
                                        and b.cod_cuenta In (Select x.cod_cuenta From par_cue_lincred x Where x.tipo = 0 And x.cod_atr = 1)) As saldo_contable,
                                    (Select Sum(h.saldo_capital) From historico_cre h Inner Join lineascredito l On h.cod_linea_credito = l.cod_linea_credito
                                    Where h.fecha_historico = r.fecha and h.saldo_capital != 0 and h.cod_linea_credito Not In (Select cod_linea_credito from parametros_linea where cod_parametro = 320)) as saldo_operativo
                                    From cierea r
                                    Where r.tipo = 'R' and r.fecha = to_date('" + fechaFinal + "','dd/mm/yyyy')";
                        }
                        else
                        { 
                            if (tipo_deudor == 1)      // Validar cartera de asociados
                                sql = @"Select r.fecha,
                                        (Select Sum(b.valor) from balance b Where b.fecha = r.fecha And b.cod_cuenta Like '14%' and b.valor != 0
                                        and b.cod_cuenta Not In (Select p.depende_de From plan_cuentas p Where p.depende_de Is Not Null)
                                        and b.cod_cuenta Not Like '1489%' and b.cod_cuenta Not Like '1491%' and b.cod_cuenta Not Like '1493%' and b.cod_cuenta Not Like '1495%' and b.cod_cuenta Not Like '1498%') As saldo_contable,
                                        (Select Sum(h.saldo_capital) From historico_cre h Inner Join lineascredito l On h.cod_linea_credito = l.cod_linea_credito
                                        Where h.fecha_historico = r.fecha and h.saldo_capital != 0 and h.cod_linea_credito Not In (Select cod_linea_credito from parametros_linea where cod_parametro = 320)
                                        and l.aplica_asociado = 1) as saldo_operativo
                                        From cierea r
                                        Where r.tipo = 'R' and r.fecha = to_date('" + fechaFinal + "','dd/mm/yyyy')";
                            else if (tipo_deudor == 2) // Validar cartera de empleados
                                sql = @"Select r.fecha,
                                        (Select Sum(b.valor) from balance b Where b.fecha = r.fecha And b.cod_cuenta Like '1640%' and b.valor != 0
                                        and b.cod_cuenta Not In(Select p.depende_de From plan_cuentas p Where p.depende_de Is Not Null)) As saldo_contable,
                                        (Select Sum(h.saldo_capital) From historico_cre h Inner Join lineascredito l On h.cod_linea_credito = l.cod_linea_credito
                                        Where h.fecha_historico = r.fecha and h.saldo_capital != 0 and h.cod_linea_credito Not In(Select cod_linea_credito from parametros_linea where cod_parametro = 320)
                                        and (l.aplica_empleado = 1 or l.aplica_asociado != 1)) as saldo_operativo
                                        From cierea r
                                        Where r.tipo = 'R' and r.fecha = to_date('" + fechaFinal + "', 'dd/mm/yyyy')";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO_CONTABLE"] != DBNull.Value) entidad.saldo_contable = Convert.ToDecimal(resultado["SALDO_CONTABLE"]);
                            if (resultado["SALDO_OPERATIVO"] != DBNull.Value) entidad.saldo_operativo = Convert.ToDecimal(resultado["SALDO_OPERATIVO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ConsultarSaldosYDiferenciaCreditos", ex);
                        return null;
                    }
                }
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaServicios(string fechaFinal, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            CuadreHistorico entidad = new CuadreHistorico();
            entidad.saldo_contable = 0;
            entidad.saldo_operativo = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                           
                            sql = @"Select r.fecha,
                                    (Select Sum(b.valor) from balance b Where b.fecha = r.fecha And b.valor != 0
                                        and b.cod_cuenta Not In (Select p.depende_de From plan_cuentas p Where p.depende_de Is Not Null)
                                        and b.cod_cuenta In (Select x.cod_cuenta From PAR_CUE_LINSER x Where x.tipo = 0 And x.cod_atr = 1)) As saldo_contable,
                                    (Select Sum(h.saldo) From historico_servicios h Inner Join LINEASSERVICIOS l On h.COD_LINEA_SERVICIO = l.COD_LINEA_SERVICIO
                                    Where h.fecha_historico = r.fecha and h.saldo!= 0) as saldo_operativo
                                    From cierea r
                                    Where r.tipo = 'Q' and r.fecha = to_date('" + fechaFinal + "','dd/mm/yyyy')";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO_CONTABLE"] != DBNull.Value) entidad.saldo_contable = Convert.ToDecimal(resultado["SALDO_CONTABLE"]);
                            if (resultado["SALDO_OPERATIVO"] != DBNull.Value) entidad.saldo_operativo = Convert.ToDecimal(resultado["SALDO_OPERATIVO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ConsultarSaldosYDiferenciaCreditos", ex);
                        return null;
                    }
                }
            }
        }


        public bool EsPlanCuentasNIIF(Usuario pUsuario)
        {
            int cantidad = 0;
            DbDataReader resultado = default(DbDataReader);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"Select Count(*) As NIIF From par_cue_lincred Where cod_atr = 2 And tipo = 0 And cod_cuenta Like '14%' ";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NIIF"] != DBNull.Value) cantidad = Convert.ToInt32(resultado["NIIF"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        if (cantidad > 0)
                            return true;
                        else
                            return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }
      
        public bool EsPlanCuentasCOMERCIAL(Usuario pUsuario)
        {
            int cantidad = 0;
            DbDataReader resultado = default(DbDataReader);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"Select Count(*) As COMERCIAL From par_cue_lincred Where tipo = 0 And cod_atr = 1 And cod_cuenta Like '13%' ";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COMERCIAL"] != DBNull.Value) cantidad = Convert.ToInt32(resultado["COMERCIAL"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        if (cantidad > 0)
                            return true;
                        else
                            return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaAhorroVista(string fechaFinal, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            CuadreHistorico entidad = new CuadreHistorico();
            entidad.saldo_contable = 0;
            entidad.saldo_operativo = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select sum(valor) as saldo_contable
                                            From balance 
                                            Where cod_cuenta In (Select Distinct cod_cuenta From par_cue_linaho Where tipo_ahorro = 3 and (tipo_tran IN (201,203,206) Or tipo_tran Is Null)) 
                                            And fecha = to_date('" + fechaFinal + "','dd/mm/yyyy')";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO_CONTABLE"] != DBNull.Value) entidad.saldo_contable = Convert.ToDecimal(resultado["SALDO_CONTABLE"]);
                        }

                        sql = @"Select sum(h.saldo_total) as saldo_operativo 
                                    From historico_ahorro h inner join ahorro_vista aho on h.numero_cuenta = aho.numero_cuenta
                                    Where h.estado in (0,1,2,3,4) And h.fecha_historico = to_date('" + fechaFinal + "','dd/mm/yyyy')";

                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["saldo_operativo"] != DBNull.Value) entidad.saldo_operativo = Convert.ToDecimal(resultado["saldo_operativo"]);
                        }


                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ConsultarSaldosYDiferenciaAhorroVista", ex);
                        return null;
                    }
                }
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaCDATS(string fechaFinal, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            CuadreHistorico entidad = new CuadreHistorico();
            entidad.saldo_contable = 0;
            entidad.saldo_operativo = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select sum(valor) as saldo_contable
                                        From balance 
                                        Where cod_cuenta In (Select cod_cuenta From par_cue_linaho Where tipo_ahorro = 5 
                                        and (tipo_tran = 301 Or tipo_tran Is Null)) 
                                        And fecha = to_date('" + fechaFinal + "','dd/mm/yyyy')";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO_CONTABLE"] != DBNull.Value) entidad.saldo_contable = Convert.ToDecimal(resultado["SALDO_CONTABLE"]);
                        }

                        sql = @"Select sum(h.valor) as saldo_operativo 
                                from historico_cdat h 
                                inner join cdat c on h.codigo_cdat = c.codigo_cdat
                                where h.estado in (0,1,2,3) 
                                and h.fecha_historico = to_date('" + fechaFinal + "','dd/mm/yyyy')";

                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["saldo_operativo"] != DBNull.Value) entidad.saldo_operativo = Convert.ToDecimal(resultado["saldo_operativo"]);
                        }


                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ConsultarSaldosYDiferenciaAhorroVista", ex);
                        return null;
                    }
                }
            }
        }


        public CuadreHistorico ConsultarSaldosYDiferenciaAhorroProgramado(string fechaFinal, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            CuadreHistorico entidad = new CuadreHistorico();
            entidad.saldo_contable = 0;
            entidad.saldo_operativo = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select sum(valor) as saldo_contable
                                          From balance 
                                          Where cod_cuenta In (Select cod_cuenta From par_cue_linaho Where tipo_ahorro = 9
                                          and (tipo_tran = 350 Or tipo_tran Is Null)) 
                                          And fecha = to_date('" + fechaFinal + "','dd/mm/yyyy')";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO_CONTABLE"] != DBNull.Value) entidad.saldo_contable = Convert.ToDecimal(resultado["SALDO_CONTABLE"]);
                        }

                        sql = @"Select sum(h.saldo_total) as saldo_operativo 
                                from HISTORICO_PROGRAMADO h 
                                inner join ahorro_programado aho on h.numero_programado = aho.numero_programado
                                where h.estado in (0,1,2,3) 
                                and h.fecha_historico = to_date('" + fechaFinal + "','dd/mm/yyyy')";

                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["saldo_operativo"] != DBNull.Value) entidad.saldo_operativo = Convert.ToDecimal(resultado["saldo_operativo"]);
                        }


                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ConsultarSaldosYDiferenciaAhorroVista", ex);
                        return null;
                    }
                }
            }
        }



        public List<CuadreHistorico> ConsultaCuadreHistorico(CuadreCartera pcuadre, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuadreHistorico> lstCuadre = new List<CuadreHistorico>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.DbType = DbType.Date;
                        PFECHA.Value = pcuadre.fecha;

                        DbParameter PCOD_ATR = cmdTransaccionFactory.CreateParameter();
                        PCOD_ATR.ParameterName = "PCOD_ATR";
                        PCOD_ATR.DbType = DbType.Int64;
                        PCOD_ATR.Value = pcuadre.cod_atr;

                        DbParameter PTIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        PTIPO_PRODUCTO.ParameterName = "PTIPO_PRODUCTO";
                        PTIPO_PRODUCTO.DbType = DbType.Int64;
                        PTIPO_PRODUCTO.Value = pcuadre.tipo_producto;

                        cmdTransaccionFactory.Parameters.Add(PFECHA);
                        cmdTransaccionFactory.Parameters.Add(PCOD_ATR);
                        cmdTransaccionFactory.Parameters.Add(PTIPO_PRODUCTO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADREHISTORICO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "";
                        if (pcuadre.tipo_producto == 3 || pcuadre.tipo_producto == 5 || pcuadre.tipo_producto == 9)
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql = "SELECT * FROM CUADRE_AHORRO WHERE TRUNC(FECHA) = TO_DATE('" + pcuadre.fecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') AND DIFERENCIA != 0 ORDER BY FECHA, NUMERO_CUENTA";
                            else
                                sql = "SELECT * FROM CUADRE_AHORRO WHERE FECHA = '" + pcuadre.fecha + "' AND DIFERENCIA != 0 ORDER BY FECHA, NUMERO_CUENTA";
                        }
                        else
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql = "SELECT * FROM CUADRE_CARTERA WHERE TRUNC(FECHA) = TO_DATE('" + pcuadre.fecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') AND DIFERENCIA != 0 ORDER BY FECHA, NUMERO_RADICACION";
                            else
                                sql = "SELECT * FROM CUADRE_CARTERA WHERE FECHA = '" + pcuadre.fecha + "' AND DIFERENCIA != 0 ORDER BY FECHA, NUMERO_RADICACION";
                        }
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuadreHistorico entidad = new CuadreHistorico();
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (pcuadre.tipo_producto == 3 || pcuadre.tipo_producto == 5 || pcuadre.tipo_producto == 9)
                            {
                                if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_radicacion = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            }
                            else
                            {
                                if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            }
                            if (resultado["FECINI"] != DBNull.Value) entidad.fecini = Convert.ToDateTime(resultado["FECINI"]);
                            if (resultado["SALDO_INICIAL"] != DBNull.Value) entidad.saldo_inicial = Convert.ToDecimal(resultado["SALDO_INICIAL"]);
                            if (resultado["DEBITOS"] != DBNull.Value) entidad.debitos = Convert.ToDecimal(resultado["DEBITOS"]);
                            if (resultado["CREDITOS"] != DBNull.Value) entidad.creditos = Convert.ToDecimal(resultado["CREDITOS"]);
                            if (resultado["SALDO_FINAL"] != DBNull.Value) entidad.saldo_final = Convert.ToDecimal(resultado["SALDO_FINAL"]);
                            if (pcuadre.tipo_producto != 2)
                            {
                                entidad.saldo_operativo = Convert.ToDecimal(entidad.saldo_inicial) - Convert.ToDecimal(entidad.debitos) + Convert.ToDecimal(entidad.creditos);
                            }
                            else
                            {
                                entidad.saldo_operativo = Convert.ToDecimal(entidad.saldo_inicial) + Convert.ToDecimal(entidad.debitos) - Convert.ToDecimal(entidad.creditos);
                            }
                            if (resultado["DIFERENCIA"] != DBNull.Value) entidad.diferencia = Convert.ToDecimal(resultado["DIFERENCIA"]);
                            lstCuadre.Add(entidad);
                        }

                        return lstCuadre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ConsultaCuadreHistorico", ex);
                        return null;
                    }
                }
            }
        }
        public void ActualizarSaldoCierre(Int64 pTipoProducto, Int64 pNumero, Int64 pValor, DateTime pFecha, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "ptipo";
                        ptipo.DbType = DbType.Int64;
                        ptipo.Value = pTipoProducto;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pnum_producto = cmdTransaccionFactory.CreateParameter();
                        pnum_producto.ParameterName = "pnum_producto";
                        pnum_producto.DbType = DbType.Int64;
                        pnum_producto.Value = pNumero;
                        cmdTransaccionFactory.Parameters.Add(pnum_producto);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.DbType = DbType.Int64;
                        pvalor.Value = pValor;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pfecha_corte = cmdTransaccionFactory.CreateParameter();
                        pfecha_corte.ParameterName = "pfecha_corte";
                        pfecha_corte.DbType = DbType.DateTime;
                        pfecha_corte.Value = pFecha;
                        cmdTransaccionFactory.Parameters.Add(pfecha_corte);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_MODSALDOCIERRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ActualizarSaldoCierre", ex);
                    }
                }
            }
        }



        public CuadreHistorico ConsultarSaldosYDiferenciaActivosFijos(string fechaFinal, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            CuadreHistorico entidad = new CuadreHistorico();
            entidad.saldo_contable = 0;
            entidad.saldo_operativo = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";

                        sql = @" Select r.fecha,
                                    (Select Sum(b.valor) from balance b Where b.fecha = r.fecha And b.valor != 0
                                        and b.cod_cuenta Not In (Select p.depende_de From plan_cuentas p Where p.depende_de Is Not Null)
                                        and b.cod_cuenta In (Select x.COD_CUENTA_DEPRECIACION From tipo_activo x )) As saldo_contable,
                                    (Select Sum(h.ACUMULADO_DEPRECIACION) From HISTORICO_ACTIVOS_FIJOS h 
                                    Where h.fecha_historico = r.fecha and H.ACUMULADO_DEPRECIACION!= 0) as saldo_operativo
                                    From cierea r
                                    Where r.tipo = 'Y' and r.fecha = to_date('" + fechaFinal + "','dd/mm/yyyy')";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["SALDO_CONTABLE"] != DBNull.Value) entidad.saldo_contable = Convert.ToDecimal(resultado["SALDO_CONTABLE"]);
                            if (resultado["SALDO_OPERATIVO"] != DBNull.Value) entidad.saldo_operativo = Convert.ToDecimal(resultado["SALDO_OPERATIVO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuadreCarteraData", "ConsultarSaldosYDiferenciaCreditos", ex);
                        return null;
                    }
                }
            }
        }


    }

}