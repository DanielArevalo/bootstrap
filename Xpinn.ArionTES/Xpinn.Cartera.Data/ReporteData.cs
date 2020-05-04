using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Cartera.Entities;

namespace Xpinn.Cartera.Data
{
    public class ReporteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ReporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
               

        /// <summary>
        /// Método que permite consultar los créditos de un asesor
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarRepCierreDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "SELECT  * FROM vs_detalle_historica_cartera WHERE fecha_cierre = to_date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = "SELECT  * FROM vs_detalle_historica_cartera WHERE fecha_cierre = '" + fecha.ToString(conf.ObtenerFormatoFecha()) + "')";
                        if (filtro.Trim() != "")
                            sql += filtro;
                        sql += " ORDER BY oficina, numero_radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.Fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["SALDO_AL_CIERRE"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_AL_CIERRE"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.Fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.Fecha_ult_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.codigo_asesor = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["NOMBRE_ASESOR"] != DBNull.Value) entidad.nombre_asesor= Convert.ToString(resultado["NOMBRE_ASESOR"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["COD_CATEGORIA_CLI"] != DBNull.Value) entidad.cod_categoria_cli = Convert.ToString(resultado["COD_CATEGORIA_CLI"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);


                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarRepCierreDetallado", ex);
                        return null;
                    }
                }
            }
        }


        public List<Reporte> ListarRepCausacionDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        // Determinar fecha de causación del mes anterior
                        DateTime? fechaanterior = null;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            cmdTransaccionFactory.CommandText = "Select Max(fecha) As fecha_anterior From cierea Where tipo = 'U' And estado = 'D' and Trunc(fecha) < to_date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            cmdTransaccionFactory.CommandText = "Select Max(fecha) As fecha_anterior From cierea Where tipo = 'U' And estado = 'D' and fecha) < '" + fecha.ToString(conf.ObtenerFormatoFecha()) + "' )";
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA_ANTERIOR"] != DBNull.Value) fechaanterior = Convert.ToDateTime(resultado["FECHA_ANTERIOR"]);
                        }
                        
                        // Determinar datos de la causación
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "SELECT  * FROM v_causacion_cartera WHERE fecha_historico = to_date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = "SELECT  * FROM v_causacion_cartera WHERE fecha_historico = '" + fecha.ToString(conf.ObtenerFormatoFecha()) + "')";
                        if (filtro.Trim() != "")
                            sql += filtro;
                        sql += " ORDER BY cod_oficina, numero_radicacion";
                                                
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.codigo_oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["NOMBRE_ATRIBUTO"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["NOMBRE_ATRIBUTO"]);
                            if (resultado["VALOR_CAUSADO"] != DBNull.Value) entidad.valor_causado = Convert.ToDecimal(resultado["VALOR_CAUSADO"]);
                            if (resultado["VALOR_ORDEN"] != DBNull.Value) entidad.valor_orden = Convert.ToDecimal(resultado["VALOR_ORDEN"]);
                            if (resultado["SALDO_CAUSADO"] != DBNull.Value) entidad.saldo_causado = Convert.ToDecimal(resultado["SALDO_CAUSADO"]);
                            if (resultado["SALDO_ORDEN"] != DBNull.Value) entidad.saldo_orden = Convert.ToDecimal(resultado["SALDO_ORDEN"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["COD_CATEGORIA_CLI"] != DBNull.Value) entidad.cod_categoria_cli = Convert.ToString(resultado["COD_CATEGORIA_CLI"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["DIAS_CAUSADOS"] != DBNull.Value) entidad.dias_causados = Convert.ToInt32(resultado["DIAS_CAUSADOS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToString(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["DIAS_AMORTIZA"] != DBNull.Value) entidad.dias_amortiza = Convert.ToInt64(resultado["DIAS_AMORTIZA"]);
                            // Determinando valor causado del período anterior
                            if (fechaanterior != null)
                                entidad.saldo_causado_ant = SaldoCausado(fechaanterior, entidad.NumRadicacion, entidad.cod_atr, connection, pUsuario);
                            // Esto se colocó para FONSODI porque daba error al generar el reporte. 5-Oct-2017. FerOrt.
                            if (entidad.dias_causados == 0)
                                entidad.dias_causados = DiasCausados(fecha, entidad.NumRadicacion, entidad.cod_atr, connection, pUsuario);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarRepCierreDetallado", ex);
                        return null;
                    }
                }
            }
        }

        public decimal SaldoCausado(DateTime? pfecha, Int64 pnumero_radicacion, Int32 pcod_atr, DbConnection connection, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal saldo_causado = 0;

            using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
            {
                try
                {
                    Configuracion conf = new Configuracion();
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    string sql = "";
                    if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        sql = "SELECT saldo_causado FROM causacion WHERE fecha_corte = to_date('" + Convert.ToDateTime(pfecha).ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                    else
                        sql = "SELECT saldo_causado FROM causacion WHERE fecha_corte = '" + Convert.ToDateTime(pfecha).ToString(conf.ObtenerFormatoFecha()) + "')";
                    sql += " AND numero_radicacion = " + pnumero_radicacion + " AND cod_atr = " + pcod_atr;
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    if (resultado.Read())
                    {
                        if (resultado["SALDO_CAUSADO"] != DBNull.Value) saldo_causado = Convert.ToDecimal(resultado["SALDO_CAUSADO"]);
                    }
                    return saldo_causado;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ReporteData", "SaldoCausado", ex);
                    return saldo_causado;
                }
            }
        }

        public List<Reporte> ListarRepProvisionDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"SELECT * FROM V_PROVISION_DETALLE_CARTERA                                 
                                    WHERE fecha_historico = to_date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = @"SELECT * FROM V_PROVISION_DETALLE_CARTERA
                                    WHERE fecha_historico = '" + fecha.ToString(conf.ObtenerFormatoFecha()) + "')";

                        if (filtro.Trim() != "")
                            sql += filtro;
                        sql += " ORDER BY cod_oficina, numero_radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.codigo_oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["NOMBRE_ATRIBUTO"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["NOMBRE_ATRIBUTO"]);                            
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["PORC_PROVISION"] != DBNull.Value) entidad.porc_provision = Convert.ToDecimal(resultado["PORC_PROVISION"]);
                            if (resultado["VALOR_PROVISION"] != DBNull.Value) entidad.valor_provision = Convert.ToDecimal(resultado["VALOR_PROVISION"]);
                            if (resultado["APORTE_RESTA"] != DBNull.Value) entidad.aporte_resta = Convert.ToDecimal(resultado["APORTE_RESTA"]);
                            if (resultado["DIFERENCIA_PROVISION"] != DBNull.Value) entidad.diferencia_provision = Convert.ToDecimal(resultado["DIFERENCIA_PROVISION"]);
                            if (resultado["DIFERENCIA_ACTUAL"] != DBNull.Value) entidad.diferencia_actual = Convert.ToDecimal(resultado["DIFERENCIA_ACTUAL"]);
                            if (resultado["DIFERENCIA_ANTERIOR"] != DBNull.Value) entidad.diferencia_anterior = Convert.ToDecimal(resultado["DIFERENCIA_ANTERIOR"]);
                            if (resultado["BASE_PROVISION"] != DBNull.Value) entidad.base_provision = Convert.ToDecimal(resultado["BASE_PROVISION"]);
                            if (resultado["cod_categoria"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["cod_categoria"]);
                            if (resultado["cod_categoria_cli"] != DBNull.Value) entidad.cod_categoria_cli = Convert.ToString(resultado["cod_categoria_cli"]);
                            if (resultado["cod_clasifica"] != DBNull.Value) entidad.cod_clasificacion = Convert.ToString(resultado["cod_clasifica"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion_clasificacion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["VALOR_GARANTIA"] != DBNull.Value) entidad.valor_garantia = Convert.ToDecimal(resultado["VALOR_GARANTIA"]);
                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarRepCierreDetallado", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> ListarRepCierreDiarios(DateTime fecha, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "SELECT  * FROM informe_creditos WHERE fecha_corte = to_date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = "SELECT  * FROM informe_creditos WHERE fecha_corte = '" + fecha.ToString(conf.ObtenerFormatoFecha()) + "')";
                        if (filtro.Trim() != "")
                            sql += filtro;
                        sql += " ORDER BY cod_oficina, numero_radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);                            
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.Fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.Fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.Fecha_ult_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALOR_A_PAGAR"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.codigo_asesor = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["NOMBRE_ASESOR"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["NOMBRE_ASESOR"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["COD_CATEGORIA_CLI"] != DBNull.Value) entidad.cod_categoria_cli = Convert.ToString(resultado["COD_CATEGORIA_CLI"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["CIUCORRESPONDENCIA"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["CIUCORRESPONDENCIA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarRepCierreDetallado", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> ListarFechaCorte(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        sql = "SELECT Distinct Fecha_Corte FROM informe_creditos ORDER BY 1 desc";
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["FECHA_CORTE"] != DBNull.Value) entidad.fechacierre = Convert.ToDateTime(resultado["FECHA_CORTE"]);
                            
                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarFechaCorte", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Método que permite consultar los créditos de un asesor
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarRepCierreDetAsesor(DateTime fecha, Usuario pUsuario,Int64 codigo)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  * FROM vs_detalle_historica_cartera  where FECHA_CIERRE= to_date('" + fecha.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') And COD_ASESOR_COM = " + codigo;
                        sql = sql + " order BY DIAS_MORA asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.Fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["SALDO_AL_CIERRE"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_AL_CIERRE"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.Fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.Fecha_ult_pago = Convert.ToDateTime(resultado["BARRIO_OFICINA"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.codigo_asesor = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["NOMBRE_ASESOR"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["NOMBRE_ASESOR"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarRepCierreDetallado", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> Consultarusuariopagoespecial(Int64 pId, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado;
            List<Reporte> lstentidad = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    Configuracion conf = new Configuracion();
                    try
                    {
                        string sql = @"Select credito.*, persona.identificacion, persona.primer_nombre || ' ' || persona.primer_apellido as nombress, oficina.nombre, lineascredito.nombre as nombres From credito Inner Join lineascredito On lineascredito.cod_linea_credito = credito.cod_linea_credito Inner Join oficina On oficina.cod_oficina = credito.cod_oficina Inner join persona on persona.cod_persona = credito.cod_deudor Where credito.pago_especial = 1 " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMBRESS"] != DBNull.Value) entidad.nombress = Convert.ToString(resultado["NOMBRESS"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            lstentidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioAtribucionesData", "Consultarusuariopagoespecial", ex);
                        return null;
                    }
                }
            }
        }

        public int DiasCausados(DateTime pFechaHistorico, Int64 pNumeroRadicacion, int pCodAtr, DbConnection connection, Usuario pUsuario)
        {
            DbDataReader resultado;
            int dias_mes_cau = 0;

            using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
            {
                Configuracion conf = new Configuracion();
                try
                {
                    string sql = "";

                    if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        sql = @"Select Sum(x.dias_mes_cau) From det_causacion x Where x.fecha_corte = To_Date('" + pFechaHistorico.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() +  "') And x.numero_radicacion = " + pNumeroRadicacion + " And x.cod_atr = " + pCodAtr + " And x.valor_base != 0";
                    else
                        sql = @"Select Sum(x.dias_mes_cau) From det_causacion x Where x.fecha_corte = '" + pFechaHistorico.ToString(conf.ObtenerFormatoFecha()) + "' And x.numero_radicacion = " + pNumeroRadicacion + " And x.cod_atr = " + pCodAtr + " And x.valor_base != 0";


                    if (connection.State == ConnectionState.Closed) connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    if (resultado.Read())
                    {
                        if (resultado["DIAS_MES_CAU"] != DBNull.Value) dias_mes_cau = Convert.ToInt32(resultado["DIAS_MES_CAU"]);
                    }

                    return dias_mes_cau;
                }
                catch 
                {
                    return dias_mes_cau;
                }
            }

        }


        public List<Reporte> CuadreSaldos(DateTime pFecha, int pTipo, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstReporte = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.Value = pFecha;
                        PFECHA.Direction = ParameterDirection.Input;
                        PFECHA.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHA);

                        DbParameter PTIPO = cmdTransaccionFactory.CreateParameter();
                        PTIPO.ParameterName = "PTIPO";
                        if (EsPlanCuentasNIIF(pUsuario))
                            PTIPO.Value = "1";
                        else
                            PTIPO.Value = "0";
                        PTIPO.Direction = ParameterDirection.Input;
                        PTIPO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PTIPO);

                        DbParameter PAJUSTAR = cmdTransaccionFactory.CreateParameter();
                        PAJUSTAR.ParameterName = "PAJUSTAR";
                        PAJUSTAR.Value = "0";
                        PAJUSTAR.Direction = ParameterDirection.Input;
                        PAJUSTAR.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PAJUSTAR);
                       
                        DbParameter PCOD_USUARIO = cmdTransaccionFactory.CreateParameter();
                        PCOD_USUARIO.ParameterName = "PCOD_USUARIO";
                        PCOD_USUARIO.Value = pUsuario.codusuario;
                        PCOD_USUARIO.Direction = ParameterDirection.InputOutput;
                        cmdTransaccionFactory.Parameters.Add(PCOD_USUARIO);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pTipo == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CART";
                        else if (pTipo == 2)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CAU";
                        else if (pTipo == 3)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CONT";
                        else if (pTipo == 4)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_PROV";
                        else if (pTipo == 5)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_APO";
                        else if (pTipo == 6)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_PERM";
                        else if (pTipo == 7)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_AHO";
                        else if (pTipo == 8)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_PRO";
                        else if (pTipo == 9)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CDAT";
                        else if (pTipo == 10)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_PROG";
                        else 
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CART";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "CuadreSaldos", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "";

                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"Select c.cod_cuenta, p.nombre, c.centro_costo, c.saldo_operativo, c.saldo_contable, t.identificacion 
                                    From CUADRE_CONTABLE C 
                                    LEFT JOIN PLAN_CUENTAS P ON C.COD_CUENTA = P.COD_CUENTA 
                                    LEFT JOIN CENTRO_COSTO D ON C.CENTRO_COSTO = D.CENTRO_COSTO 
                                    LEFT JOIN V_PERSONA T ON C.TERCERO = T.COD_PERSONA
                                    Where c.saldo_operativo != 0 Or c.saldo_contable != 0 Order by c.cod_cuenta, c.centro_costo, t.identificacion";
                        else
                            sql = @"Select c.cod_cuenta, p.nombre, c.centro_costo, c.saldo_operativo, c.saldo_contable, t.identificacion 
                                    From CUADRE_CONTABLE C 
                                    LEFT JOIN PLAN_CUENTAS P ON C.COD_CUENTA = P.COD_CUENTA 
                                    LEFT JOIN CENTRO_COSTO D ON C.CENTRO_COSTO = D.CENTRO_COSTO 
                                    LEFT JOIN V_PERSONA T ON C.TERCERO = T.COD_PERSONA 
                                    Where c.saldo_operativo != 0 Or c.saldo_contable != 0 Order by c.cod_cuenta, c.centro_costo, t.identificacion";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["SALDO_OPERATIVO"] != DBNull.Value) entidad.saldo_operativo = Convert.ToDecimal(resultado["SALDO_OPERATIVO"]);
                            if (resultado["SALDO_CONTABLE"] != DBNull.Value) entidad.saldo_contable = Convert.ToDecimal(resultado["SALDO_CONTABLE"]);
                            entidad.diferencia_actual = entidad.saldo_operativo - entidad.saldo_contable;
                            lstReporte.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "CuadreSaldos", ex);
                        return null;
                    }
                }
            }
        }

        public bool GuardarCuadreSaldos(DateTime pFecha, int pTipo, Usuario pUsuario)
        {            
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.Value = pFecha;
                        PFECHA.Direction = ParameterDirection.Input;
                        PFECHA.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHA);

                        DbParameter PTIPO = cmdTransaccionFactory.CreateParameter();
                        PTIPO.ParameterName = "PTIPO";
                        if (EsPlanCuentasNIIF(pUsuario))
                            PTIPO.Value = "1";
                        else
                            PTIPO.Value = "0";
                        PTIPO.Direction = ParameterDirection.Input;
                        PTIPO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PTIPO);

                        DbParameter PAJUSTAR = cmdTransaccionFactory.CreateParameter();
                        PAJUSTAR.ParameterName = "PAJUSTAR";
                        PAJUSTAR.Value = "1";
                        PAJUSTAR.Direction = ParameterDirection.Input;
                        PAJUSTAR.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PAJUSTAR);

                        DbParameter PCOD_USUARIO = cmdTransaccionFactory.CreateParameter();
                        PCOD_USUARIO.ParameterName = "PCOD_USUARIO";
                        PCOD_USUARIO.Value = pUsuario.codusuario;
                        PCOD_USUARIO.Direction = ParameterDirection.InputOutput;
                        cmdTransaccionFactory.Parameters.Add(PCOD_USUARIO);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pTipo == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CART";
                        else if (pTipo == 2)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CAU";
                        else if (pTipo == 3)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CONT";
                        else if (pTipo == 4)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_PROV";
                        else if (pTipo == 5)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_APO";
                        else if (pTipo == 6)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_PERM";
                        else if (pTipo == 7)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_AHO";
                        else if (pTipo == 8)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_PRO";
                        else if (pTipo == 9)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CDAT";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CART";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        connection.Close();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "GuardarCuadreSaldos", ex);
                        return false;
                    }
                };
                
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<ReporteConsolidadoCierre> ConsultarReporteConsolidadoCierre(string fechaCorte, string filtro, Usuario usuario)
        {
            DbDataReader resultado;
            List<ReporteConsolidadoCierre> lstReporte = new List<ReporteConsolidadoCierre>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT hist.fecha_historico, clas.descripcion as Descripcion_clasificacion, 
                                        Case hist.forma_pago When '2' Then 'Con Libranza' When '1' Then 'Sin Libranza' When 'N' Then 'Con Libranza' When 'C' Then 'Sin Libranza' End As forma_pago, 
                                        Case hist.tipo_garantia When 1 Then 'Admisible' Else 'Otras Garantias' End As tipo_garantia,
                                        cat.descripcion as Descripcion_categoria, Substr(hist.cod_categoria_cli, 0, 1) as Codigo_Categoria, o.nombre as Nombre_Oficina, hist.cod_linea_credito,ln.nombre as linea,
                                        SUM(saldo_capital) as saldo_capital, COUNT(*) as Numero_Creditos
                                        FROM HISTORICO_CRE hist
                                        JOIN OFICINA O on o.cod_oficina = hist.cod_oficina
                                        JOIN CATEGORIAS CAT on hist.cod_categoria_cli = cat.cod_categoria
                                        JOIN CLASIFICACION clas on hist.cod_clasifica = clas.cod_clasifica 
                                        JOIN LINEASCREDITO ln on ln.cod_linea_credito= hist.cod_linea_credito ";
                        Configuracion conf = new Configuracion();
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += @" WHERE hist.fecha_historico = To_Date('" + fechaCorte + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql += @" WHERE hist.fecha_historico = '" + fechaCorte + "' ";
                        sql += filtro + " GROUP BY  hist.fecha_historico, o.nombre, cat.descripcion, Substr(hist.cod_categoria_cli, 0, 1), hist.cod_categoria_cli,clas.descripcion, hist.forma_pago, hist.tipo_garantia, hist.cod_linea_credito,ln.nombre  ORDER BY hist.cod_categoria_cli,hist.cod_linea_credito ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ReporteConsolidadoCierre entidad = new ReporteConsolidadoCierre();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]);
                            if (resultado["Descripcion_clasificacion"] != DBNull.Value) entidad.Clasificacion = Convert.ToString(resultado["Descripcion_clasificacion"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.FormaPago = Convert.ToString(resultado["forma_pago"]);
                            if (resultado["tipo_garantia"] != DBNull.Value) entidad.TipoGarantia = Convert.ToString(resultado["tipo_garantia"]);
                            if (resultado["Codigo_Categoria"] != DBNull.Value) entidad.CodigoCategoria = Convert.ToString(resultado["Codigo_Categoria"]);
                            if (resultado["Descripcion_categoria"] != DBNull.Value) entidad.Categoria = Convert.ToString(resultado["Descripcion_categoria"]);
                            if (resultado["Nombre_Oficina"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["Nombre_Oficina"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.SaldoCapital = Convert.ToDecimal(resultado["saldo_capital"]);
                            if (resultado["Numero_Creditos"] != DBNull.Value) entidad.NumeroCreditos = Convert.ToInt64(resultado["Numero_Creditos"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.CodigoLinea = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["linea"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["linea"]);

                            lstReporte.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ConsultarReporteConsolidadoCierre", ex);
                        return null;
                    }
                }
            }
        }

        public List<ReporteConsolidadoCierre> ConsultarReporteConsolidadoCausacion(string fechaCorte, string filtro, Usuario usuario)
        {
            DbDataReader resultado;
            List<ReporteConsolidadoCierre> lstReporte = new List<ReporteConsolidadoCierre>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT hist.fecha_historico, clas.descripcion as Descripcion_clasificacion, 
                                        cat.descripcion as Descripcion_categoria, hist.cod_categoria_cli as Codigo_Categoria, o.nombre as Nombre_Oficina,
                                        atr.nombre as Nombre_Atributo, caus.cod_atr as Codigo_Atributo, hist.cod_linea_credito,ln.nombre as linea,
                                        SUM(caus.saldo_causado) as Saldo_Causado, SUM(caus.saldo_orden) as Saldo_Orden
                                        FROM HISTORICO_CRE hist
                                        JOIN causacion caus on hist.numero_radicacion = caus.numero_radicacion AND caus.fecha_corte = hist.fecha_historico
                                        JOIN atributos atr on caus.cod_atr = atr.cod_atr
                                        JOIN oficina O on o.cod_oficina = hist.cod_oficina
                                        JOIN categorias CAT on hist.cod_categoria_cli = cat.cod_categoria
                                        JOIN clasificacion clas on hist.cod_clasifica = clas.cod_clasifica
                                        JOIN LINEASCREDITO ln on ln.cod_linea_credito= hist.cod_linea_credito ";
                        Configuracion conf = new Configuracion();
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += @" WHERE hist.fecha_historico = To_Date('" + fechaCorte + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql += @" WHERE hist.fecha_historico = '" + fechaCorte + "' ";

                        sql += filtro + " GROUP BY  hist.fecha_historico, o.nombre, cat.descripcion, hist.cod_categoria_cli, clas.descripcion, atr.nombre, caus.cod_atr, hist.cod_linea_credito,ln.nombre ORDER BY hist.cod_categoria_cli,hist.cod_linea_credito";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ReporteConsolidadoCierre entidad = new ReporteConsolidadoCierre();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]);
                            if (resultado["Descripcion_clasificacion"] != DBNull.Value) entidad.Clasificacion = Convert.ToString(resultado["Descripcion_clasificacion"]);
                            if (resultado["Descripcion_categoria"] != DBNull.Value) entidad.Categoria = Convert.ToString(resultado["Descripcion_categoria"]);
                            if (resultado["Codigo_Categoria"] != DBNull.Value) entidad.CodigoCategoria = Convert.ToString(resultado["Codigo_Categoria"]);
                            if (resultado["Nombre_Oficina"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["Nombre_Oficina"]);
                            if (resultado["Nombre_Atributo"] != DBNull.Value) entidad.Atributo = Convert.ToString(resultado["Nombre_Atributo"]);
                            if (resultado["Codigo_Atributo"] != DBNull.Value) entidad.CodigoAtributo = Convert.ToInt64(resultado["Codigo_Atributo"]);
                            if (resultado["Saldo_Causado"] != DBNull.Value) entidad.SaldoCausado = Convert.ToDecimal(resultado["Saldo_Causado"]);
                            if (resultado["Saldo_Orden"] != DBNull.Value) entidad.SaldoOrden = Convert.ToDecimal(resultado["Saldo_Orden"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.CodigoLinea = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["linea"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["linea"]);

                            lstReporte.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ConsultarReporteConsolidadoCausacion", ex);
                        return null;
                    }
                }
            }
        }


        public List<ReporteConsolidadoCierre> ConsultarReporteConsolidadoProvision(string fechaCorte, string filtro, Usuario usuario)
        {
            DbDataReader resultado;
            List<ReporteConsolidadoCierre> lstReporte = new List<ReporteConsolidadoCierre>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT hist.fecha_historico, clas.descripcion as Descripcion_clasificacion, 
                                        cat.descripcion as Descripcion_categoria, hist.cod_categoria_cli as Codigo_Categoria, o.nombre as Nombre_Oficina,
                                        atr.nombre as Nombre_Atributo, pro.cod_atr as Codigo_Atributo,
                                        Case hist.tipo_garantia When 1 Then 'Admisible' Else 'Otras Garantias' End As tipo_garantia,  hist.cod_linea_credito,ln.nombre as linea,
                                        SUM(pro.valor_provision) as Valor_Provision
                                        FROM HISTORICO_CRE hist
                                        JOIN PROVISION pro on pro.numero_radicacion = hist.numero_radicacion AND pro.fecha_corte = hist.fecha_historico
                                        JOIN ATRIBUTOS atr on pro.cod_atr = atr.cod_atr
                                        JOIN OFICINA O on o.cod_oficina = hist.cod_oficina
                                        JOIN CATEGORIAS CAT on hist.cod_categoria_cli = cat.cod_categoria
                                        JOIN CLASIFICACION clas on hist.cod_clasifica = clas.cod_clasifica
                                        JOIN LINEASCREDITO ln on ln.cod_linea_credito= hist.cod_linea_credito ";
                        Configuracion conf = new Configuracion();
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += @" WHERE hist.fecha_historico = To_Date('" + fechaCorte + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql += @" WHERE hist.fecha_historico = '" + fechaCorte + "' ";

                        sql += filtro + " GROUP BY hist.fecha_historico, o.nombre, cat.descripcion, hist.cod_categoria_cli, clas.descripcion, atr.nombre, pro.cod_atr,  hist.tipo_garantia, hist.cod_linea_credito,ln.nombre ORDER BY hist.cod_categoria_cli,hist.cod_linea_credito";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ReporteConsolidadoCierre entidad = new ReporteConsolidadoCierre();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]);
                            if (resultado["Descripcion_clasificacion"] != DBNull.Value) entidad.Clasificacion = Convert.ToString(resultado["Descripcion_clasificacion"]);
                            if (resultado["Descripcion_categoria"] != DBNull.Value) entidad.Categoria = Convert.ToString(resultado["Descripcion_categoria"]);
                            if (resultado["Codigo_Categoria"] != DBNull.Value) entidad.CodigoCategoria = Convert.ToString(resultado["Codigo_Categoria"]);
                            if (resultado["tipo_garantia"] != DBNull.Value) entidad.TipoGarantia = Convert.ToString(resultado["tipo_garantia"]);
                            if (resultado["Nombre_Oficina"] != DBNull.Value) entidad.Oficina = Convert.ToString(resultado["Nombre_Oficina"]);
                            if (resultado["Nombre_Atributo"] != DBNull.Value) entidad.Atributo = Convert.ToString(resultado["Nombre_Atributo"]);
                            if (resultado["Codigo_Atributo"] != DBNull.Value) entidad.CodigoAtributo = Convert.ToInt64(resultado["Codigo_Atributo"]);
                            if (resultado["Valor_Provision"] != DBNull.Value) entidad.ValorProvision = Convert.ToDecimal(resultado["Valor_Provision"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.CodigoLinea = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["linea"] != DBNull.Value) entidad.LineaCredito = Convert.ToString(resultado["linea"]);

                            lstReporte.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ConsultarReporteConsolidadoProvision", ex);
                        return null;
                    }
                }
            }
        }


    }
}
