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
    /// Objeto de acceso a datos para la tabla cierre histórico
    /// </summary>
    public class ClasificacionCarteraData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla  cierre histórico
        /// </summary>
        public ClasificacionCarteraData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<ClasificacionCartera> ListarDetalle(string fecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClasificacionCartera> listarchivo = new List<ClasificacionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from V_CLASIFICACION_CARTERA where FECHA_HISTORICO =  to_date ('" + fecha + "', 'dd/mm/yyyy')";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClasificacionCartera entidad = new ClasificacionCartera();
                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.FECHA_HISTORICO = Convert.ToDateTime(resultado["FECHA_HISTORICO"]).ToString("dd/MM/yyyy");
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.COD_LINEA_CREDITO = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.NOMBRE_LINEA = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.IDENTIFICACION = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.NOMBRE = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CLASIFICACION"] != DBNull.Value) entidad.CLASIFICACION = Convert.ToString(resultado["CLASIFICACION"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.FORMA_PAGO = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["TIPO_GARANTIA"] != DBNull.Value) entidad.TIPO_GARANTIA = Convert.ToString(resultado["TIPO_GARANTIA"]);
                            if (resultado["COD_CATEGORIA_CLI"] != DBNull.Value) entidad.COD_CATEGORIA_CLI = Convert.ToString(resultado["COD_CATEGORIA_CLI"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.COD_ATR = Convert.ToString(resultado["COD_ATR"]);
                            if (resultado["NOMBRE_ATRIBUTO"] != DBNull.Value) entidad.NOMBRE_ATRIBUTO = Convert.ToString(resultado["NOMBRE_ATRIBUTO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.SALDO = Convert.ToInt64(resultado["SALDO"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["cod_oficina"]);
                            if (resultado["nombre_oficina"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["nombre_oficina"]);
                            

                            listarchivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarchivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarDetalle", ex);
                        return null;
                    }
                }
            }
        }



        public List<ClasificacionCartera> ListarConsolidado(string fecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClasificacionCartera> listarchivo = new List<ClasificacionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from V_CLASIFICACION_CARTERA_TOTAL where FECHA_HISTORICO = to_date ('" + fecha + "', 'dd/mm/yyyy')";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClasificacionCartera entidad = new ClasificacionCartera();
                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.FECHA_HISTORICO = Convert.ToDateTime(resultado["FECHA_HISTORICO"]).ToString("dd/MM/yyyy");
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.COD_LINEA_CREDITO = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.NOMBRE_LINEA = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["CLASIFICACION"] != DBNull.Value) entidad.CLASIFICACION = Convert.ToString(resultado["CLASIFICACION"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.FORMA_PAGO = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["TIPO_GARANTIA"] != DBNull.Value) entidad.TIPO_GARANTIA = Convert.ToString(resultado["TIPO_GARANTIA"]);
                            if (resultado["COD_CATEGORIA_CLI"] != DBNull.Value) entidad.COD_CATEGORIA_CLI = Convert.ToString(resultado["COD_CATEGORIA_CLI"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.COD_ATR = Convert.ToString(resultado["COD_ATR"]);
                            if (resultado["NOMBRE_ATRIBUTO"] != DBNull.Value) entidad.NOMBRE_ATRIBUTO = Convert.ToString(resultado["NOMBRE_ATRIBUTO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.SALDO = Convert.ToInt64(resultado["SALDO"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["cod_oficina"]);
                            if (resultado["nombre_oficina"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["nombre_oficina"]);
                            
                                listarchivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarchivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarConsolidado", ex);
                        return null;
                    }
                }
            }
        }



        public List<CausacionCartera> ListarDetalleCausacion(string fecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CausacionCartera> listarchivo = new List<CausacionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "select * from V_CAUSACION_CARTERA where Trunc(FECHA_HISTORICO) = to_date ('" + fecha + "', '" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = "select * from V_CAUSACION_CARTERA where FECHA_HISTORICO = '" + fecha + "' ";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CausacionCartera entidad = new CausacionCartera();
                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.FECHA_HISTORICO = Convert.ToDateTime(resultado["FECHA_HISTORICO"]).ToString("dd/MM/yyyy");
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.COD_LINEA_CREDITO = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.NOMBRE_LINEA = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.IDENTIFICACION = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.NOMBRE = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.COD_ATR = Convert.ToString(resultado["COD_ATR"]);
                            if (resultado["NOMBRE_ATRIBUTO"] != DBNull.Value) entidad.NOMBRE_ATRIBUTO = Convert.ToString(resultado["NOMBRE_ATRIBUTO"]);
                            if (resultado["VALOR_CAUSADO"] != DBNull.Value) entidad.VALOR_CAUSADO = Convert.ToInt64(resultado["VALOR_CAUSADO"]);
                            if (resultado["VALOR_ORDEN"] != DBNull.Value) entidad.VALOR_ORDEN = Convert.ToInt64(resultado["VALOR_ORDEN"]);
                            if (resultado["SALDO_CAUSADO"] != DBNull.Value) entidad.SALDO_CAUSADO = Convert.ToInt64(resultado["SALDO_CAUSADO"]);
                            if (resultado["SALDO_ORDEN"] != DBNull.Value) entidad.SALDO_ORDEN = Convert.ToInt64(resultado["SALDO_ORDEN"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.DIAS_MORA = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["cod_oficina"]);
                            if (resultado["nombre_oficina"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["nombre_oficina"]);
                           
                            listarchivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarchivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarDetalleCausacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<CausacionCartera> ListarConsolidadoCausacion(string fecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CausacionCartera> listarchivo = new List<CausacionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "select * from V_CAUSACION_CARTERA_TOT where FECHA_HISTORICO = to_date ('" + fecha + "', '" + conf.ObtenerFormatoFecha() + "')";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CausacionCartera entidad = new CausacionCartera();
                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.FECHA_HISTORICO = Convert.ToDateTime(resultado["FECHA_HISTORICO"]).ToString("dd/MM/yyyy");
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.COD_LINEA_CREDITO = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.NOMBRE_LINEA = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.COD_ATR = Convert.ToString(resultado["COD_ATR"]);
                            if (resultado["NOMBRE_ATRIBUTO"] != DBNull.Value) entidad.NOMBRE_ATRIBUTO = Convert.ToString(resultado["NOMBRE_ATRIBUTO"]);
                            if (resultado["VALOR_CAUSADO"] != DBNull.Value) entidad.VALOR_CAUSADO = Convert.ToInt64(resultado["VALOR_CAUSADO"]);
                            if (resultado["VALOR_ORDEN"] != DBNull.Value) entidad.VALOR_ORDEN = Convert.ToInt64(resultado["VALOR_ORDEN"]);
                            if (resultado["SALDO_CAUSADO"] != DBNull.Value) entidad.SALDO_CAUSADO = Convert.ToInt64(resultado["SALDO_CAUSADO"]);
                            if (resultado["SALDO_ORDEN"] != DBNull.Value) entidad.SALDO_ORDEN = Convert.ToInt64(resultado["SALDO_ORDEN"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["cod_oficina"]);
                            if (resultado["nombre_oficina"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["nombre_oficina"]);
                            listarchivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarchivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarConsolidadoCausacion", ex);
                        return null;
                    }
                }
            }
        }



        public List<ClasificacionCartera> ListarDetalleAbrirCierre(string fecha, string tipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClasificacionCartera> listarchivo = new List<ClasificacionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        sql = @"Select Max(fecha) as fecha,
                                (case                                     
                                    when  tipo = 'A' then 'Cierre Aportes' 
                                    when  tipo = 'B' then 'Interfaz Nomina-Créditos' 
                                    when  tipo = 'C' then 'Cierre Contable' 
                                    when  tipo = 'D' then 'Interfaz Nomina-Aportes Patronales'
                                    when  tipo = 'E' then 'Interfaz Nomina' 
                                    when  tipo = 'F' then 'Interfaz Nomina-Provisión'
                                    when  tipo = 'G' then 'Cierre Contable NIIF'
                                    when  tipo = 'H' then 'Cierre Ahorros a la Vista'
                                    when  tipo = 'I' then 'Provisión Ahorro Vista'
                                    when  tipo = 'J' then 'Provisión General'
                                    when  tipo = 'K' then 'Provisión Cdat'
                                    when  tipo = 'L' then 'Cierre Ahorro Programado'
                                    when  tipo = 'M' then 'Cierre Cdats'
                                    when  tipo = 'N' then 'Cierre Anual Contable' 
                                    when  tipo = 'O' then 'Cierre Anual Niif'    
                                    when  tipo = 'P' then 'Cierre Personas'    
                                    when  tipo = 'Q' then 'Cierre Servicios'          
                                    when  tipo = 'R' then 'Cierre Cartera y Clasificación' 
                                    when  tipo = 'U' then 'Causación' 
                                    when  tipo = 'S' then 'Provisión' 
                                    when  tipo = 'T' then 'T=Interfa Nomina-Primas'
                                    when  tipo = 'V' then 'Provision Programado' 
                                    when  tipo = 'W' then 'Provision Aportes' 
                                    when  tipo = 'X' then 'Cierre Gestión de Riesgo'                     
                                    when  tipo = 'Y' then 'Cierre Activos Fijos'
                                    when  tipo = 'Z' then 'Cierre Segmentaciòn Créditos'
                                    when  tipo = '1' then 'Provisión de Obligaciones'
                                    else tipo
                                end) As Tipo,
                                (case 
                                    when  Estado = 'D' then 'Definitivo' 
                                    when  Estado = 'P' then 'Pruebas'
                                    Else Estado
                                end) As Estado, Min(Campo1) As Campo1, Min(Campo2) As Campo2 
                                From cierea Where tipo Is Not Null ";
                        if (fecha != "")
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += @" And fecha = To_Date ('" + fecha + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += @" And fecha = '" + fecha + "' ";
                        if (tipo != "0")
                            sql += @" And tipo = '" + tipo + "' ";

                        sql += " Group by tipo, estado, Case tipo When 'C' Then '' When 'G' Then '' Else Campo1 End, Campo2 Order by 1 desc, 2";

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClasificacionCartera entidad = new ClasificacionCartera();
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["fecha"]).ToString("dd/MM/yyyy");
                            if (resultado["Tipo"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["Tipo"]);
                            if (resultado["Estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["Estado"]);
                            if (resultado["Campo1"] != DBNull.Value) entidad.campo1 = Convert.ToString(resultado["Campo1"]);
                            if (resultado["Campo2"] != DBNull.Value) entidad.campo2 = Convert.ToString(resultado["Campo2"]);
                            listarchivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarchivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarDetalleAbrirCierre", ex);
                        return null;
                    }
                }
            }

        }


        public string EliminarRegistroAbrirCierres(string fecha, string tipo, string estado, int anular_comprobante, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProvisionCartera> listarchivo = new List<ProvisionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        string error = "";
                        /*if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"DELETE FROM CIEREA WHERE FECHA = TO_DATE('" + fecha + "', '" + conf.ObtenerFormatoFecha() + "') AND TIPO = '" + tipo + "' AND ESTADO = '" + estado + "'";
                        else
                            sql = @"DELETE FROM CIEREA WHERE FECHA = '" + fecha + "' AND TIPO = '" + tipo + "' AND ESTADO = '" + estado + "'";*/

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pfecha";
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.Value = Convert.ToDateTime(fecha);
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "ptipo";
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.Value = tipo;
                        ptipo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Direction = ParameterDirection.Input;
                        pestado.Value = estado;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter panular_comprobante = cmdTransaccionFactory.CreateParameter();
                        panular_comprobante.ParameterName = "panular_comprobante";
                        panular_comprobante.Direction = ParameterDirection.Input;
                        panular_comprobante.Value = anular_comprobante;
                        panular_comprobante.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(panular_comprobante);

                        DbParameter perror = cmdTransaccionFactory.CreateParameter();
                        perror.ParameterName = "perror";
                        perror.Direction = ParameterDirection.InputOutput;
                        perror.DbType = DbType.String;
                        perror.Size = 200;
                        cmdTransaccionFactory.Parameters.Add(perror);
                        
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_ABRIRCIERRE";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        error = perror.Value.ToString();
                        dbConnectionFactory.CerrarConexion(connection);

                        return error;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "EliminarRegistroAbrirCierres", ex);
                        return null;
                    }
                }
            }


        }


        public List<ProvisionCartera> ListarDetalleProvision(string fecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProvisionCartera> listarchivo = new List<ProvisionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from V_PROVISION_CARTERA where FECHA_HISTORICO = to_date ('" + fecha + "', 'dd/mm/yyyy')";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProvisionCartera entidad = new ProvisionCartera();
                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.FECHA_HISTORICO = Convert.ToDateTime(resultado["FECHA_HISTORICO"]).ToString("dd/MM/yyyy");
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NUMERO_RADICACION = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.COD_LINEA_CREDITO = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.NOMBRE_LINEA = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.IDENTIFICACION = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.NOMBRE = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CLASIFICACION"] != DBNull.Value) entidad.CLASIFICACION = Convert.ToString(resultado["CLASIFICACION"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.FORMA_PAGO = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["TIPO_GARANTIA"] != DBNull.Value) entidad.TIPO_GARANTIA = Convert.ToString(resultado["TIPO_GARANTIA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.COD_CATEGORIA = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.COD_ATR = Convert.ToString(resultado["COD_ATR"]);
                            if (resultado["NOMBRE_ATRIBUTO"] != DBNull.Value) entidad.NOMBRE_ATRIBUTO = Convert.ToString(resultado["NOMBRE_ATRIBUTO"]);
                            if (resultado["PORC_PROVISION"] != DBNull.Value) entidad.PORC_PROVISION = Convert.ToString(resultado["PORC_PROVISION"]);
                            if (resultado["VALOR_PROVISION"] != DBNull.Value) entidad.VALOR_PROVISION = Convert.ToInt64(resultado["VALOR_PROVISION"]);
                            if (resultado["DIFERENCIA_PROVISION"] != DBNull.Value) entidad.DIFERENCIA_PROVISION = Convert.ToInt64(resultado["DIFERENCIA_PROVISION"]);
                            if (resultado["DIFERENCIA_ACTUAL"] != DBNull.Value) entidad.DIFERENCIA_ACTUAL = Convert.ToInt64(resultado["DIFERENCIA_ACTUAL"]);
                            if (resultado["DIFERENCIA_ANTERIOR"] != DBNull.Value) entidad.DIFERENCIA_ANTERIOR = Convert.ToInt64(resultado["DIFERENCIA_ANTERIOR"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["cod_oficina"]);
                            if (resultado["nombre_oficina"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["nombre_oficina"]);
                            
                            listarchivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarchivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarDetalleProvision", ex);
                        return null;
                    }
                }
            }
        }

        public List<ProvisionCartera> ListarConsolidadoProvision(string fecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProvisionCartera> listarchivo = new List<ProvisionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from V_PROVISION_CARTERA_TOT where FECHA_HISTORICO = to_date ('" + fecha + "', 'dd/mm/yyyy')";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProvisionCartera entidad = new ProvisionCartera();

                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.FECHA_HISTORICO = Convert.ToDateTime(resultado["FECHA_HISTORICO"]).ToString("dd/MM/yyyy");
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.COD_LINEA_CREDITO = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.NOMBRE_LINEA = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.IDENTIFICACION = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.NOMBRE = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CLASIFICACION"] != DBNull.Value) entidad.CLASIFICACION = Convert.ToString(resultado["CLASIFICACION"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.FORMA_PAGO = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["TIPO_GARANTIA"] != DBNull.Value) entidad.TIPO_GARANTIA = Convert.ToString(resultado["TIPO_GARANTIA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.COD_CATEGORIA = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.COD_ATR = Convert.ToString(resultado["COD_ATR"]);
                            if (resultado["NOMBRE_ATRIBUTO"] != DBNull.Value) entidad.NOMBRE_ATRIBUTO = Convert.ToString(resultado["NOMBRE_ATRIBUTO"]);
                            if (resultado["VALOR_PROVISION"] != DBNull.Value) entidad.VALOR_PROVISION = Convert.ToInt64(resultado["VALOR_PROVISION"]);
                            if (resultado["DIFERENCIA_PROVISION"] != DBNull.Value) entidad.DIFERENCIA_PROVISION = Convert.ToInt64(resultado["DIFERENCIA_PROVISION"]);
                            if (resultado["DIFERENCIA_ACTUAL"] != DBNull.Value) entidad.DIFERENCIA_ACTUAL = Convert.ToInt64(resultado["DIFERENCIA_ACTUAL"]);
                            if (resultado["DIFERENCIA_ANTERIOR"] != DBNull.Value) entidad.DIFERENCIA_ANTERIOR = Convert.ToInt64(resultado["DIFERENCIA_ANTERIOR"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["cod_oficina"]);
                            if (resultado["nombre_oficina"] != DBNull.Value) entidad.nombre_oficina = Convert.ToString(resultado["nombre_oficina"]);
                            
                            listarchivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarchivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarConsolidadoProvision", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Lista de tabla calsificacion para parametrizacion de clasificacion y provision de cartera 
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>

        public List<ClasificacionCartera> ListarClasificacion(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClasificacionCartera> listarclasificacion= new List<ClasificacionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from CLASIFICACION";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClasificacionCartera entidad = new ClasificacionCartera();
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion= Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToString(resultado["TIPO_HISTORICO"]);

                            listarclasificacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarclasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarClasificacion", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Lista de tabla calsificacion para parametrizacion de clasificacion y provision de cartera 
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>

        public List<ClasificacionCartera> ListarDiasCategoria(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClasificacionCartera> listarclasificacion = new List<ClasificacionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql2 = " Select  d.idrango,d.cod_clasifica,c.cod_categoria,d.dias_minimo, d.dias_maximo, d.tipo_provision, d.porc_provision, d.causa From categorias c Left Join dias_categorias d On c.cod_categoria = d.cod_categoria and COD_CLASIFICA = " + pId.ToString();
                        string sql = sql2 + " order by c.cod_categoria asc";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClasificacionCartera entidad = new ClasificacionCartera();
                           if (resultado["IDRANGO"] != DBNull.Value) entidad.rango = Convert.ToInt64(resultado["IDRANGO"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["DIAS_MINIMO"] != DBNull.Value) entidad.diasminimo = Convert.ToInt64(resultado["DIAS_MINIMO"]);
                            if (resultado["DIAS_MAXIMO"] != DBNull.Value) entidad.diasmaximo = Convert.ToInt64(resultado["DIAS_MAXIMO"]);
                            if (resultado["TIPO_PROVISION"] != DBNull.Value) entidad.tipo_provision = Convert.ToInt64(resultado["TIPO_PROVISION"]);
                            if (entidad.tipo_provision == 0)
                                entidad.NOMBRE = "No provisiona";
                            else if (entidad.tipo_provision == 1)
                                entidad.NOMBRE = "Capital, 100% Otros Atributos";
                            else if (entidad.tipo_provision == 2)
                                entidad.NOMBRE = "Todos los Atributos";

                            if (resultado["PORC_PROVISION"] != DBNull.Value) entidad.por_provision = Convert.ToInt64(resultado["PORC_PROVISION"]);
                            if (resultado["CAUSA"] != DBNull.Value) entidad.causa = Convert.ToInt64(resultado["CAUSA"]);

                   
                            listarclasificacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarclasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarClasificacion", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Lista de tabla calsificacion para parametrizacion de clasificacion y provision de cartera 
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public List<ClasificacionCartera> ListarCategorias(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClasificacionCartera> listarclasificacion = new List<ClasificacionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from Categorias";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClasificacionCartera entidad = new ClasificacionCartera();
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                         

                            listarclasificacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarclasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarCategorias", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Consulta de la tabla clasificacion para parametrizacion de clasificacion y provision de cartera 
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>

        public ClasificacionCartera ConsultarClasificacion(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ClasificacionCartera entidad = new ClasificacionCartera();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from CLASIFICACION where COD_CLASIFICA = " + pId.ToString();
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO_HISTORICO"] != DBNull.Value) entidad.tipo_historico = Convert.ToString(resultado["TIPO_HISTORICO"]);

                         
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ConsultarClasificacion", ex);
                        return null;
                    }
                }
            }
        }



        /// <summary>
        /// Consulta de la tabla clasificacion para parametrizacion de clasificacion y provision de cartera 
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>

        public ClasificacionCartera ConsultarDiasCategoria(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            ClasificacionCartera entidad = new ClasificacionCartera();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " Select  d.idrango,d.cod_clasifica,c.cod_categoria,d.dias_minimo, d.dias_maximo, d.tipo_provision, d.porc_provision, d.causa From categorias c Left Join dias_categorias d On c.cod_categoria = d.cod_categoria where IDRANGO = " + pId.ToString();
                       
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDRANGO"] != DBNull.Value) entidad.rango = Convert.ToInt64(resultado["IDRANGO"]);
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["COD_CLASIFICA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["DIAS_MINIMO"] != DBNull.Value) entidad.diasminimo = Convert.ToInt64(resultado["DIAS_MINIMO"]);
                            if (resultado["DIAS_MAXIMO"] != DBNull.Value) entidad.diasmaximo = Convert.ToInt64(resultado["DIAS_MAXIMO"]);
                            if (resultado["TIPO_PROVISION"] != DBNull.Value) entidad.tipo_provision = Convert.ToInt64(resultado["TIPO_PROVISION"]);
                            if (resultado["PORC_PROVISION"] != DBNull.Value) entidad.por_provision = Convert.ToInt64(resultado["PORC_PROVISION"]);
                            if (resultado["CAUSA"] != DBNull.Value) entidad.causa = Convert.ToInt64(resultado["CAUSA"]);

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ConsultarDiasCategoria", ex);
                        return null;
                    }
                }
            }
        }

        public ClasificacionCartera CrearCategorias(ClasificacionCartera pclasificacion, Usuario pUsuario)
        {

            ClasificacionCartera entidad = new ClasificacionCartera();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                     
                        DbParameter pclasifica = cmdTransaccionFactory.CreateParameter();
                        pclasifica.Direction = ParameterDirection.Input;
                        pclasifica.ParameterName = "pclasifica";
                        pclasifica.Value = pclasificacion.clasifica;

                        DbParameter pcategoria = cmdTransaccionFactory.CreateParameter();
                        pcategoria.Direction = ParameterDirection.Input;
                        pcategoria.ParameterName = "pcategoria";
                        pcategoria.Value = pclasificacion.categoria;
                        

                        DbParameter pdiasmin = cmdTransaccionFactory.CreateParameter();
                        pdiasmin.Direction = ParameterDirection.Input;
                        pdiasmin.ParameterName = "pdiasmin";
                        pdiasmin.Value = pclasificacion.diasminimo;

                        DbParameter pdiasmax = cmdTransaccionFactory.CreateParameter();
                        pdiasmax.Direction = ParameterDirection.Input;
                        pdiasmax.ParameterName = "pdiasmax";
                        pdiasmax.Value = pclasificacion.diasmaximo;

                        DbParameter ptipoprov = cmdTransaccionFactory.CreateParameter();
                        ptipoprov.Direction = ParameterDirection.Input;
                        ptipoprov.ParameterName = "ptipoprov";
                        ptipoprov.Value = pclasificacion.tipo_provision;

                        DbParameter pporprov = cmdTransaccionFactory.CreateParameter();
                        pporprov.Direction = ParameterDirection.Input;
                        pporprov.ParameterName = "pporprov";
                        pporprov.Value = pclasificacion.por_provision;

                        DbParameter pcausa = cmdTransaccionFactory.CreateParameter();
                        pcausa.Direction = ParameterDirection.Input;
                        pcausa.ParameterName = "pcausa";
                        pcausa.Value = pclasificacion.causa;

                        DbParameter p_rango = cmdTransaccionFactory.CreateParameter();
                        p_rango.ParameterName = "p_rango";
                        p_rango.DbType = DbType.Int64;
                        p_rango.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pcategoria);
                        cmdTransaccionFactory.Parameters.Add(pclasifica);
                        cmdTransaccionFactory.Parameters.Add(pdiasmin);
                        cmdTransaccionFactory.Parameters.Add(pdiasmax);
                        cmdTransaccionFactory.Parameters.Add(ptipoprov);
                        cmdTransaccionFactory.Parameters.Add(pporprov);
                        cmdTransaccionFactory.Parameters.Add(pcausa);
                        cmdTransaccionFactory.Parameters.Add(p_rango);
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_DIAS_CAT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        pclasificacion.rango = Convert.ToInt64(p_rango.Value);
                        return pclasificacion;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "CrearCategorias", ex);
                        return null;
                    }
                }
            }

        }//end crear


        public ClasificacionCartera ModificarCategorias(ClasificacionCartera pclasificacion, Usuario pUsuario)
        {

            ClasificacionCartera entidad = new ClasificacionCartera();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pclasifica = cmdTransaccionFactory.CreateParameter();
                        pclasifica.Direction = ParameterDirection.Input;
                        pclasifica.ParameterName = "pclasifica";
                        pclasifica.Value = pclasificacion.clasifica;

                        DbParameter pcategoria = cmdTransaccionFactory.CreateParameter();
                        pcategoria.Direction = ParameterDirection.Input;
                        pcategoria.ParameterName = "pcategoria";
                        pcategoria.Value = pclasificacion.categoria;


                        DbParameter pdiasmin = cmdTransaccionFactory.CreateParameter();
                        pdiasmin.Direction = ParameterDirection.Input;
                        pdiasmin.ParameterName = "pdiasmin";
                        pdiasmin.Value = pclasificacion.diasminimo;

                        DbParameter pdiasmax = cmdTransaccionFactory.CreateParameter();
                        pdiasmax.Direction = ParameterDirection.Input;
                        pdiasmax.ParameterName = "pdiasmax";
                        pdiasmax.Value = pclasificacion.diasmaximo;

                        DbParameter ptipoprov = cmdTransaccionFactory.CreateParameter();
                        ptipoprov.Direction = ParameterDirection.Input;
                        ptipoprov.ParameterName = "ptipoprov";
                        ptipoprov.Value = pclasificacion.tipo_provision;

                        DbParameter pporprov = cmdTransaccionFactory.CreateParameter();
                        pporprov.Direction = ParameterDirection.Input;
                        pporprov.ParameterName = "pporprov";
                        pporprov.Value = pclasificacion.por_provision;

                        DbParameter pcausa = cmdTransaccionFactory.CreateParameter();
                        pcausa.Direction = ParameterDirection.Input;
                        pcausa.ParameterName = "pcausa";
                        pcausa.Value = pclasificacion.causa;

                        DbParameter p_rango = cmdTransaccionFactory.CreateParameter();
                        p_rango.ParameterName = "p_rango";
                        p_rango.DbType = DbType.Int64;
                        p_rango.Value = pclasificacion.rango;
                        p_rango.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pcategoria);
                        cmdTransaccionFactory.Parameters.Add(pclasifica);
                        cmdTransaccionFactory.Parameters.Add(pdiasmin);
                        cmdTransaccionFactory.Parameters.Add(pdiasmax);
                        cmdTransaccionFactory.Parameters.Add(ptipoprov);
                        cmdTransaccionFactory.Parameters.Add(pporprov);
                        cmdTransaccionFactory.Parameters.Add(pcausa);
                        cmdTransaccionFactory.Parameters.Add(p_rango);
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_DIAS_CAT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        pclasificacion.rango = Convert.ToInt64(p_rango.Value);
                        return pclasificacion;

                    }
                    catch (DbException ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ModificarCategorias", ex);
                        return null;
                    }
                }
            }

        }

             
        public List<ClasificacionCartera> ListarCategoriasVencidas(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClasificacionCartera> listarclasificacion = new List<ClasificacionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select Distinct Substr(cod_categoria, 0, 1) As cod_categoria, descripcion From Categorias Where cod_categoria != 'A' Order by 1 ";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClasificacionCartera entidad = new ClasificacionCartera();
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            listarclasificacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarclasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarCategoriasVencidas", ex);
                        return null;
                    }
                }
            }
        }



        public List<ClasificacionCartera> ListarLineasCredito(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ClasificacionCartera> listarlineas= new List<ClasificacionCartera>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select Distinct (cod_linea_credito) As cod_linea_credito, nombre as DESCRIPCION From lineascredito Order by 1 ";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ClasificacionCartera entidad = new ClasificacionCartera();
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.COD_LINEA_CREDITO = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            listarlineas.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return listarlineas;
                    }               
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ListarLineasCredito", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Consulta el control de cierres para un tipo de proceso dado
        /// </summary>
        /// <param name="pFecha"></param>
        /// <param name="pTipo"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Xpinn.Comun.Entities.Cierea ConsultarControlCierres(DateTime pFecha, String pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Xpinn.Comun.Entities.Cierea entidad = new Xpinn.Comun.Entities.Cierea();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select * From CIEREA Where ";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += " fecha = TO_DATE('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql += " fecha = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' ";
                        if (pTipo.Trim() != "")
                            sql += " And tipo = '" + pTipo.Trim() + "' ";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["CAMPO1"] != DBNull.Value) entidad.campo1 = Convert.ToString(resultado["CAMPO1"]);
                            if (resultado["CAMPO2"] != DBNull.Value) entidad.campo2 = Convert.ToString(resultado["CAMPO2"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClasificacionCarteraData", "ConsultarControlCierres", ex);
                        return null;
                    }
                }
            }
        }


        public List<String> ListarErroresParametrizacion(int pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<String> lstErrores = new List<String>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (pTipo == 2)         // Validar la parametrización de la provisión
                        {
                              connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            // Determinar parámetros repetidos
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            string sUsrGarantia = "0";
                            if (ContabilizaProvisionPorGarantia(pUsuario).Trim() == "1")
                                sUsrGarantia = "1";
                            string sql = @"Select p.cod_linea_credito, p.cod_atr, p.tipo_cuenta, p.cod_categoria, p.tipo_mov, " + (sUsrGarantia == "0" ? "0" : "p.garantia") + @" As garantia, Count(*) As cantidad
                                            From par_cue_lincred p
                                            Where p.tipo = 2 And p.tipo_cuenta = 2
                                            Group by p.cod_linea_credito, p.cod_atr, p.tipo_cuenta, p.cod_categoria, p.tipo_mov" + (sUsrGarantia == "0" ? "" : ", p.garantia") + @" 
                                            Having count(*) > 1 
                                           Union All 
                                           Select p.cod_linea_credito, p.cod_atr, p.tipo_cuenta, p.cod_categoria, p.tipo_mov, 0 As garantia, Count(*) As cantidad
                                            From par_cue_lincred p
                                            Where p.tipo = 2 And p.tipo_cuenta != 2
                                            Group by p.cod_linea_credito, p.cod_atr, p.tipo_cuenta, p.cod_categoria, p.tipo_mov 
                                            Having count(*) > 1 
                                            Order by 1, 2, 3, 4";
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            while (resultado.Read())
                            {
                                string error = "Hay registros repetidos en ";
                                string tipo_cuenta = "";
                                string tipo_garantia = "";
                                if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) error += "Linea:" + Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                                if (resultado["COD_ATR"] != DBNull.Value) error += "Atributo:" + Convert.ToString(resultado["COD_ATR"]);
                                if (resultado["TIPO_CUENTA"] != DBNull.Value) tipo_cuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                                error += "Tipo Cuenta:" + TipoCuentaProvision(tipo_cuenta);
                                if (resultado["COD_CATEGORIA"] != DBNull.Value) error += "Categoria:" + Convert.ToString(resultado["COD_CATEGORIA"]);
                                if (resultado["TIPO_MOV"] != DBNull.Value) error += "Tipo Movimiento:" + TipoMovimiento(Convert.ToString(resultado["TIPO_MOV"]).ToString().Trim());
                                if (resultado["GARANTIA"] != DBNull.Value) tipo_garantia = Convert.ToString(resultado["GARANTIA"]);
                                if (sUsrGarantia == "1" && tipo_cuenta == "2")
                                    error += "Garantia:" + TipoGarantia(tipo_garantia, pUsuario);

                                lstErrores.Add(error);
                            }
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            // Determinar parámetros faltantes
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            sql = @"Select l.cod_linea_credito, l.nombre, a.cod_atr, a.nombre, c.cod_categoria, " + (sUsrGarantia == "0" ? "' '" : "t.cod_tipo_gar") + @" As tipo_garantia
                                    From lineascredito l, categorias c, " + (sUsrGarantia == "0" ? " " : "tipo_garantia t, ") + @" atributos a
                                    Where l.cod_linea_credito In (Select x.cod_linea_credito From credito x Where x.cod_linea_credito = l.cod_linea_credito)
                                    And a.cod_atr In (Select x.cod_atr From atributos x Where (x.cod_atr = 1 Or x.causa = '1'))
                                    And c.cod_categoria != 'A'
                                    And l.cod_linea_credito Not In (Select cod_linea_credito From Parametros_Linea Where cod_linea_credito = l.cod_linea_credito And cod_parametro = 320)
                                    Order by 1, 3, 5" + (sUsrGarantia == "0" ? " " : " ,6 ") + @" ";
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            while (resultado.Read())
                            {
                                string error = "";
                                string cod_linea_credito = "";
                                int cod_atr = 0;
                                string cod_categoria = "";
                                int? tipo_garantia  = null;
                                string cod_cuenta = "";
                                if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                                if (resultado["COD_ATR"] != DBNull.Value) cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                                if (resultado["COD_CATEGORIA"] != DBNull.Value) cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                                if (sUsrGarantia == "1")
                                    if (resultado["TIPO_GARANTIA"] != DBNull.Value) tipo_garantia = Convert.ToInt32(resultado["TIPO_GARANTIA"]);

                                cod_cuenta = BuscarParametrosProvision(cod_linea_credito, cod_atr, cod_categoria, null, 0, null, pUsuario);
                                if (cod_cuenta == "" || cod_cuenta == null) error += " Falta parametrizar Linea:" + cod_linea_credito + " Atributo:" + cod_atr + " Categoria:" + cod_categoria + " Tipo: Ingreso <br />";

                                cod_cuenta = BuscarParametrosProvision(cod_linea_credito, cod_atr, cod_categoria, null, 1, null, pUsuario);
                                if (cod_cuenta == "" || cod_cuenta == null) error += " Falta parametrizar Linea:" + cod_linea_credito + " Atributo:" + cod_atr + " Categoria:" + cod_categoria + " Tipo: Gasto <br />";

                                cod_cuenta = BuscarParametrosProvision(cod_linea_credito, cod_atr, cod_categoria, tipo_garantia, 2, sUsrGarantia, pUsuario);
                                if (cod_cuenta == "" || cod_cuenta == null) error += " Falta parametrizar Linea:" + cod_linea_credito + " Atributo:" + cod_atr + " Categoria:" + cod_categoria + (sUsrGarantia == "0" ? " " : " Tipo Garantia:" + TipoGarantia(Convert.ToString(tipo_garantia), pUsuario)) + " Tipo: Provisión <br />";

                                if (error.Trim() != "")
                                    lstErrores.Add(error);
                            }
                            dbConnectionFactory.CerrarConexion(connection);
                        }                        
                        return lstErrores;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<String> ListarErroresParametrizacionClasif(int pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<String> lstErrores = new List<String>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (pTipo == 1)         // Validar la parametrización de la clasificación
                        {
                              connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            // Determinar parámetros repetidos 
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            
                            string sql = @"Select p.cod_linea_credito, p.cod_atr, p.tipo_cuenta, p.cod_categoria, p.libranza, case p.libranza when 1 then 'Sin Libranza' when 2 then 'Con Libranza' end as tipo_libranza,
                                            p.garantia, p.tipo_mov, Count(*)  
                                            From par_cue_lincred p Where  p.tipo = 1
                                            Group by p.cod_linea_credito, p.cod_atr, p.tipo_cuenta, p.cod_categoria, p.libranza, p.garantia, p.tipo_mov 
                                            Having count(*) > 1";
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            while (resultado.Read())
                            {
                                string error = "Hay registros repetidos en ";
                                string tipo_cuenta = "";
                                string tipo_garantia = "";
                                if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) error += "Linea:" + Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                                if (resultado["COD_ATR"] != DBNull.Value) error += "Atributo:" + Convert.ToString(resultado["COD_ATR"]);
                                if (resultado["TIPO_CUENTA"] != DBNull.Value) tipo_cuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                                error += "Tipo Cuenta:" + TipoCuentaClasificacion(tipo_cuenta);
                                if (resultado["COD_CATEGORIA"] != DBNull.Value) error += "Categoria:" + Convert.ToString(resultado["COD_CATEGORIA"]);
                                if (resultado["TIPO_LIBRANZA"] != DBNull.Value) error += Convert.ToString(resultado["TIPO_LIBRANZA"]);
                                if (resultado["GARANTIA"] != DBNull.Value) tipo_garantia = Convert.ToString(resultado["GARANTIA"]);
                                error += "Garantia:" + TipoGarantia(tipo_garantia, pUsuario);
                                if (resultado["TIPO_MOV"] != DBNull.Value) error += "Tipo Movimiento:" + TipoMovimiento(Convert.ToString(resultado["TIPO_MOV"]).ToString().Trim());

                                lstErrores.Add(error);
                            }

                            // Determinar parámetros faltantes para cuentas de tipo normal
                            sql = @"With Resultado As
                                    (
                                    Select l.cod_linea_credito, l.nombre, a.nombre As atributo, c.cod_categoria,f.descripcion As formapago,g.descripcion As garantia,
                                    (Select p.cod_cuenta From par_cue_lincred p Where p.cod_atr = a.cod_atr And p.tipo_cuenta = 1 And p.tipo = 1 And p.tipo_mov = '1'
                                    And p.cod_linea_credito = l.cod_linea_credito And p.cod_categoria = c.cod_categoria And p.libranza = f.codigo And p.garantia = g.cod_tipo_gar) As cod_cuenta
                                    From lineascredito l, atributos a, categorias c, tip_for_pag f, tipo_garantia g
                                    Where a.cod_atr In (1, 2, 3) And g.cod_tipo_gar in (1,2) And f.codigo in(1,2)
                                    And a.causa = 1 And l.cod_linea_credito Not In (Select cod_linea_credito From Parametros_Linea Where cod_linea_credito = l.cod_linea_credito And cod_parametro = 320)
                                    ) 
                                    Select * From Resultado Where cod_cuenta is null Order by 1, 3, 4, 5, 6";

                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            while (resultado.Read())
                            {
                                string error = "";
                                string cod_linea_credito = "";
                                string atributo = "";
                                string cod_categoria = "";
                                string garantia = "";
                                string forma_pago = "";
                                string cod_cuenta = "";
                                if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                                if (resultado["ATRIBUTO"] != DBNull.Value) atributo = Convert.ToString(resultado["ATRIBUTO"]);
                                if (resultado["COD_CATEGORIA"] != DBNull.Value) cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                                if (resultado["FORMAPAGO"] != DBNull.Value) forma_pago = Convert.ToString(resultado["FORMAPAGO"]);
                                if (resultado["GARANTIA"] != DBNull.Value) garantia = Convert.ToString(resultado["GARANTIA"]);
                                if (resultado["COD_CUENTA"] != DBNull.Value) cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);

                                if (cod_cuenta == "" || cod_cuenta == null)
                                    error = " Falta parametrizar Linea:" + cod_linea_credito + " Atributo:" + atributo + " Categoria:" + cod_categoria + " Forma pago:" + forma_pago + " Tipo Garantia:" + garantia;

                                if (error.Trim() != "")
                                    lstErrores.Add(error);
                            }

                            //Parametros faltandes para cuentas de orden
                            sql = @"With Resultado As
                                    (
                                    Select l.cod_linea_credito, l.nombre, a.nombre As atributo, c.cod_categoria,f.descripcion As formapago,g.descripcion As garantia,
                                    (Select p.cod_cuenta From par_cue_lincred p Where p.cod_atr = a.cod_atr And p.tipo_cuenta = 3 And p.tipo = 1 And p.tipo_mov = '1'
                                    And p.cod_linea_credito = l.cod_linea_credito And p.cod_categoria = c.cod_categoria And p.libranza = f.codigo And p.garantia = g.cod_tipo_gar) As cta_deb_orden,
                                    (Select p.cod_cuenta From par_cue_lincred p Where p.cod_atr = a.cod_atr And p.tipo_cuenta = 3 And p.tipo = 1 And p.tipo_mov = '2'
                                    And p.cod_linea_credito = l.cod_linea_credito And p.cod_categoria = c.cod_categoria And p.libranza = f.codigo And p.garantia = g.cod_tipo_gar) As cta_cre_orden
                                    From lineascredito l, atributos a, categorias c, tip_for_pag f, tipo_garantia g
                                    Where a.cod_atr In (1, 2, 3) And g.cod_tipo_gar in (1,2) And f.codigo in(1,2)
                                    ) 
                                    Select r.* From Resultado r
                                    Left Join lineascredito l on r.cod_linea_credito = l.cod_linea_credito 
                                    Left Join dias_categorias d on l.cod_clasifica = d.cod_clasifica 
                                    Where (cta_deb_orden is null or cta_cre_orden is null) And r.cod_categoria = d.cod_categoria And d.causa = 0
                                    Order by 1, 3, 4, 5, 6";

                            while (resultado.Read())
                            {
                                string error = "";
                                string cod_linea_credito = "";
                                string atributo = "";
                                string cod_categoria = "";
                                string garantia = "";
                                string forma_pago = "";
                                if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                                if (resultado["ATRIBUTO"] != DBNull.Value) atributo = Convert.ToString(resultado["ATRIBUTO"]);
                                if (resultado["COD_CATEGORIA"] != DBNull.Value) cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                                if (resultado["FORMAPAGO"] != DBNull.Value) forma_pago = Convert.ToString(resultado["FORMAPAGO"]);
                                if (resultado["GARANTIA"] != DBNull.Value) garantia = Convert.ToString(resultado["GARANTIA"]);

                                if (resultado["CTA_DEB_ORDEN"] == DBNull.Value)
                                {
                                    error = " Falta parametrizar Linea:" + cod_linea_credito + " Atributo:" + atributo + " Categoria:" + cod_categoria + " Forma pago:" + forma_pago + " Tipo Garantia:" + garantia + "Cuenta Débito Orden";
                                    lstErrores.Add(error);
                                }
                                if (resultado["CTA_CRE_ORDEN"] == DBNull.Value)
                                {
                                    error = " Falta parametrizar Linea:" + cod_linea_credito + " Atributo:" + atributo + " Categoria:" + cod_categoria + " Forma pago:" + forma_pago + " Tipo Garantia:" + garantia + "Cuenta Crédito Orden";
                                    lstErrores.Add(error);
                                }
                            }


                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return lstErrores;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public List<String> ListarErroresParametrizacionCausa(int pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<String> lstErrores = new List<String>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (pTipo == 3)         // Validar la parametrización de la causación
                        {
                              connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();

                            // Determinar parámetros repetidos

                            string sql = @"Select p.cod_linea_credito, p.cod_atr, p.tipo_cuenta, p.tipo_mov, Count(*) 
                                            From par_cue_lincred p Where  p.tipo = 3 
                                            Group by p.cod_linea_credito, p.cod_atr, p.tipo_cuenta, p.tipo_mov Having count(*) > 1";
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            while (resultado.Read())
                            {
                                string error = "Hay registros repetidos en ";
                                string tipo_cuenta = "";
                                if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) error += "Linea:" + Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                                if (resultado["COD_ATR"] != DBNull.Value) error += "Atributo:" + Convert.ToString(resultado["COD_ATR"]);
                                if (resultado["TIPO_CUENTA"] != DBNull.Value) tipo_cuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                                error += "Tipo Cuenta:" + TipoCuentaCausacion(tipo_cuenta);
                                if (resultado["TIPO_MOV"] != DBNull.Value) error += "Tipo Movimiento:" + TipoMovimiento(Convert.ToString(resultado["TIPO_MOV"]).ToString().Trim());
                                
                                lstErrores.Add(error);
                            }
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            // Determinar parámetros faltantes
                            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            sql = @"With Resultado As
                                    (
                                    Select l.cod_linea_credito, l.nombre, a.nombre As atributo,
                                    (Select p.cod_cuenta From par_cue_lincred p Where p.cod_atr = a.cod_atr And p.tipo_cuenta = 2 And p.tipo = 3 And p.tipo_mov = '1'
                                      And p.cod_linea_credito = l.cod_linea_credito) As cta_deb_causado,
                                    (Select p.cod_cuenta From par_cue_lincred p Where p.cod_atr = a.cod_atr And p.tipo_cuenta = 2 And p.tipo = 3 And p.tipo_mov = '2'
                                      And p.cod_linea_credito = l.cod_linea_credito ) As cta_cre_causado,
                                    (Select p.cod_cuenta From par_cue_lincred p Where p.cod_atr = a.cod_atr And p.tipo_cuenta = 3 And p.tipo = 3 And p.tipo_mov = '1'
                                      And p.cod_linea_credito = l.cod_linea_credito) As cta_deb_orden,
                                    (Select p.cod_cuenta From par_cue_lincred p Where p.cod_atr = a.cod_atr And p.tipo_cuenta = 3 And p.tipo = 3 And p.tipo_mov = '2'
                                      And p.cod_linea_credito = l.cod_linea_credito ) As cta_cre_orden
                                    From lineascredito l, atributos a
                                    Where a.causa = 1 And l.cod_linea_credito Not In (Select cod_linea_credito From Parametros_Linea Where cod_linea_credito = l.cod_linea_credito And cod_parametro = 320)
                                    Order by 1, 3
                                    ) Select * from Resultado Where cta_deb_causado is null or cta_cre_causado is null or cta_deb_orden is null or cta_deb_orden is null or cta_cre_orden is null";

                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            while (resultado.Read())
                            {
                                string error = "";
                                string cod_linea_credito = "";
                                string atributo = "";

                                if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                                if (resultado["ATRIBUTO"] != DBNull.Value) atributo = Convert.ToString(resultado["ATRIBUTO"]);
                                
                                if (resultado["CTA_DEB_CAUSADO"] == DBNull.Value)
                                {
                                    error = " Falta parametrizar Linea:" + cod_linea_credito + " Atributo:" + atributo;
                                    error += " Cuenta Débito Causado";
                                    lstErrores.Add(error);
                                }
                                if (resultado["CTA_CRE_CAUSADO"] == DBNull.Value)
                                {
                                    error = " Falta parametrizar Linea:" + cod_linea_credito + " Atributo:" + atributo;
                                    error += " Cuenta Crédito Causado";
                                    lstErrores.Add(error);
                                }
                                if (resultado["CTA_DEB_ORDEN"] == DBNull.Value)
                                {
                                    error = " Falta parametrizar Linea:" + cod_linea_credito + " Atributo:" + atributo;
                                    error += " Cuenta Débito Orden";
                                    lstErrores.Add(error);
                                }
                                if (resultado["CTA_CRE_ORDEN"] == DBNull.Value)
                                {
                                    error = " Falta parametrizar Linea:" + cod_linea_credito + " Atributo:" + atributo;
                                    error += " Cuenta Crédito Orden";
                                    lstErrores.Add(error);
                                }

                                if (error.Trim() != "")
                                    lstErrores.Add(error);
                            }
                            dbConnectionFactory.CerrarConexion(connection);
                        }
                        return lstErrores;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public string TipoCuentaProvision(string pTipoCuenta)
        {
            if (pTipoCuenta == "0") { return "Disminucion Anterior"; }
            else if (pTipoCuenta == "1") { return "Disminucion Actual"; }
            else if (pTipoCuenta == "2") { return "Aumento"; }
            else { return "-"; }
        }

        public string TipoCuentaClasificacion(string pTipoCuenta)
        {
            if (pTipoCuenta == "1") { return "Normal"; }
            else if (pTipoCuenta == "2") { return ""; }
            else if (pTipoCuenta == "3") { return "Orden"; }
            else { return "-"; }
        }

        public string TipoCuentaCausacion(string pTipoCuenta)
        {
            if (pTipoCuenta == "2") { return "Causado"; }
            else if (pTipoCuenta == "3") { return "Orden"; }
            else { return "-"; }
        }

        public string TipoMovimiento(string pTipoMov)
        {
            if (pTipoMov == "D" || pTipoMov == "1") { return "Débito"; }
            else if (pTipoMov == "C" || pTipoMov == "2") { return "Crédito"; }
            else { return "-"; }
        }

        public string TipoGarantia(string pTipoGarantia, Usuario pUsuario)
        {
            if (pTipoGarantia.Trim() == "" || pTipoGarantia == null)
                return "";
            DbDataReader resultado = default(DbDataReader);
            string descripcion = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select descripcion From tipo_garantia Where cod_tipo_gar = " + pTipoGarantia;
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["DESCRIPCION"] != DBNull.Value) descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return descripcion;
                    }
                    catch
                    {
                        return descripcion;
                    }
                }
            }
        }

        public string ContabilizaProvisionPorGarantia(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            string valor = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select valor From GENERAL Where codigo = 44";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return valor;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }

        public string BuscarParametrosProvision(string pcod_linea_credito, int pcod_atr, string pcod_categoria, int? ptipo_garantia, int ptipo_cuenta, /*string ptipo_mov,*/ string sUsaGarantia, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            string cod_cuenta = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select p.cod_cuenta From par_cue_lincred p 
                                        Where p.tipo = 2 And p.cod_linea_credito = '" + pcod_linea_credito + "' And p.cod_atr = " + pcod_atr + @" 
                                        And p.cod_categoria = '" + pcod_categoria + @"' And p.tipo_cuenta = " + ptipo_cuenta /* + " And p.tipo_mov = '" + ptipo_mov + "' " */ + 
                                        (sUsaGarantia == "1" ? " And p.garantia = " + ptipo_garantia : "");
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_CUENTA"] != DBNull.Value) cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return cod_cuenta;
                    }
                    catch
                    {
                        return cod_cuenta;
                    }
                }
            }  
        }



    }
}
