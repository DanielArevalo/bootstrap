using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Data
{

    public class TasaMercadoNIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

       
        public TasaMercadoNIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public void EliminarTasaCondicionNIIF(TasaMercadoCondicionNIF pTasaCondicionNIIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcod_tasa_condicion = cmdTransaccionFactory.CreateParameter();
                        pcod_tasa_condicion.ParameterName = "p_cod_tasa_condicion";
                        pcod_tasa_condicion.Value = pTasaCondicionNIIF.cod_tasa_condicion;
                        pcod_tasa_condicion.Direction = ParameterDirection.Input;
                        pcod_tasa_condicion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tasa_condicion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TASACONDI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TasaMercadoNIIFData", "EliminarTasaCondicionNIIF", ex);
                    }
                }
            }
        }


        public List<TasaMercadoCondicionNIF> FiltrarDatosTasaCondicion(int codigo, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TasaMercadoCondicionNIF> lstTasaCondicion = new List<TasaMercadoCondicionNIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "select * from tasa_mercado_condicion_nif where cod_tasa_mercado = " + codigo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TasaMercadoCondicionNIF entidad = new TasaMercadoCondicionNIF();
                            if (resultado["COD_TASA_CONDICION"] != DBNull.Value) entidad.cod_tasa_condicion = Convert.ToInt32(resultado["COD_TASA_CONDICION"]);
                            if (resultado["VARIABLE"] != DBNull.Value) entidad.variable = Convert.ToInt32(resultado["VARIABLE"]);
                            if (resultado["OPERADOR"] != DBNull.Value) entidad.operador = Convert.ToString(resultado["OPERADOR"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToString(resultado["VALOR"]);
                            lstTasaCondicion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTasaCondicion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TasaMercadoNIIFData", "FiltrarDatosTasaCondicion", ex);
                        return null;
                    }
                }

            }
        }



        public int ObtenerCodigo(TasaMercadoNIF pTasaMercado,Usuario pUsuario)
        {
            int resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(cod_tasa_mercado) FROM tasa_mercado_nif";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado =  Convert.ToInt32(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TasaMercadoNIIFData", "ObtenerCodigo", ex);
                        return -1;
                    }
                }
            }
        }


        public TasaMercadoCondicionNIF ModificarTasaMercado_CondicionNIIF(TasaMercadoCondicionNIF pTasaCondicion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_tasa_condicion = cmdTransaccionFactory.CreateParameter();
                        pcod_tasa_condicion.ParameterName = "p_cod_tasa_condicion";
                        pcod_tasa_condicion.Value = pTasaCondicion.cod_tasa_condicion;
                        pcod_tasa_condicion.Direction = ParameterDirection.Input;
                        pcod_tasa_condicion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tasa_condicion);


                        DbParameter pcod_tasa_mercado = cmdTransaccionFactory.CreateParameter();
                        pcod_tasa_mercado.ParameterName = "p_cod_tasa_mercado";
                        pcod_tasa_mercado.Value = pTasaCondicion.cod_tasa_mercado;
                        pcod_tasa_mercado.Direction = ParameterDirection.Input;
                        pcod_tasa_mercado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tasa_mercado);

                        DbParameter pvariable = cmdTransaccionFactory.CreateParameter();
                        pvariable.ParameterName = "p_variable";
                        pvariable.Value = pTasaCondicion.variable;
                        pvariable.Direction = ParameterDirection.Input;
                        pvariable.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pvariable);

                        DbParameter poperador = cmdTransaccionFactory.CreateParameter();
                        poperador.ParameterName = "p_operador";
                        poperador.Value = pTasaCondicion.operador;
                        poperador.Direction = ParameterDirection.Input;
                        poperador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(poperador);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTasaCondicion.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TASACONDI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTasaCondicion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TasaMercadoNIIFData", "ModificarTasaMercado_CondicionNIIF", ex);
                        return null;
                    }
                }
            }
        }


        public TasaMercadoCondicionNIF CrearTasaMercado_CondicionNIIF(TasaMercadoCondicionNIF pTasaCondicion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_tasa_condicion = cmdTransaccionFactory.CreateParameter();
                        pcod_tasa_condicion.ParameterName = "p_cod_tasa_condicion";
                        pcod_tasa_condicion.Value = pTasaCondicion.cod_tasa_condicion;
                        pcod_tasa_condicion.Direction = ParameterDirection.Output;
                        pcod_tasa_condicion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tasa_condicion);

                        DbParameter pcod_tasa_mercado = cmdTransaccionFactory.CreateParameter();
                        pcod_tasa_mercado.ParameterName = "p_cod_tasa_mercado";
                        pcod_tasa_mercado.Value = pTasaCondicion.cod_tasa_mercado;
                        pcod_tasa_mercado.Direction = ParameterDirection.Input;
                        pcod_tasa_mercado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tasa_mercado);

                        DbParameter pvariable = cmdTransaccionFactory.CreateParameter();
                        pvariable.ParameterName = "p_variable";
                        pvariable.Value = pTasaCondicion.variable;
                        pvariable.Direction = ParameterDirection.Input;
                        pvariable.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pvariable);

                        DbParameter poperador = cmdTransaccionFactory.CreateParameter();
                        poperador.ParameterName = "p_operador";
                        poperador.Value = pTasaCondicion.operador;
                        poperador.Direction = ParameterDirection.Input;
                        poperador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(poperador);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pTasaCondicion.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pvalor);
                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TASACONDI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTasaCondicion.cod_tasa_condicion = Convert.ToInt32(pcod_tasa_condicion.Value);
                        return pTasaCondicion;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TasaMercadoNIIFData", "CrearTasaMercado_CondicionNIIF", ex);
                        return null;
                    }
                }
            }
        }



        public void EliminarTasaMercadoNIIF(TasaMercadoNIF pCarteraNIIF, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcod_tasa_mercado = cmdTransaccionFactory.CreateParameter();
                        pcod_tasa_mercado.ParameterName = "p_cod_tasa_mercado";
                        pcod_tasa_mercado.Value = pCarteraNIIF.cod_tasa_mercado;
                        pcod_tasa_mercado.Direction = ParameterDirection.Input;
                        pcod_tasa_mercado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tasa_mercado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TASAMERC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TasaMercadoNIIFData", "EliminarTasaMercadoNIIF", ex);
                    }
                }
            }
        }



        public TasaMercadoNIF ModificarTasaMercadoNIIF(TasaMercadoNIF pTasaMercado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_tasa_mercado = cmdTransaccionFactory.CreateParameter();
                        pcod_tasa_mercado.ParameterName = "p_cod_tasa_mercado";
                        pcod_tasa_mercado.Value = pTasaMercado.cod_tasa_mercado;
                        pcod_tasa_mercado.Direction = ParameterDirection.Input;
                        pcod_tasa_mercado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tasa_mercado);

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "p_fecha_inicial";
                        pfecha_inicial.Value = pTasaMercado.fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        pfecha_final.Value = pTasaMercado.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        ptasa.Value = pTasaMercado.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        ptipo_tasa.Value = pTasaMercado.tipo_tasa;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        pobservaciones.Value = pTasaMercado.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TASAMER_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTasaMercado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TasaMercadoNIIFData", "ModificarTasaMercadoNIIF", ex);
                        return null;
                    }
                }
            }
        }



        public TasaMercadoNIF CrearTasaMercadoNIIF(TasaMercadoNIF pTasaMercado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pcod_tasa_mercado = cmdTransaccionFactory.CreateParameter();
                        pcod_tasa_mercado.ParameterName = "p_cod_tasa_mercado";
                        pcod_tasa_mercado.Value = pTasaMercado.cod_tasa_mercado;
                        pcod_tasa_mercado.Direction = ParameterDirection.Output;
                        pcod_tasa_mercado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tasa_mercado);

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "p_fecha_inicial";
                        pfecha_inicial.Value = pTasaMercado.fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        pfecha_final.Value = pTasaMercado.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        DbParameter ptasa = cmdTransaccionFactory.CreateParameter();
                        ptasa.ParameterName = "p_tasa";
                        ptasa.Value = pTasaMercado.tasa;
                        ptasa.Direction = ParameterDirection.Input;
                        ptasa.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(ptasa);

                        DbParameter ptipo_tasa = cmdTransaccionFactory.CreateParameter();
                        ptipo_tasa.ParameterName = "p_tipo_tasa";
                        ptipo_tasa.Value = pTasaMercado.tipo_tasa;
                        ptipo_tasa.Direction = ParameterDirection.Input;
                        ptipo_tasa.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tasa);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        pobservaciones.Value = pTasaMercado.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NIF_TASAMER_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pTasaMercado.cod_tasa_mercado = Convert.ToInt32(pcod_tasa_mercado.Value);
                        return pTasaMercado;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TasaMercadoNIIFData", "CrearTasaMercadoNIIF", ex);
                        return null;
                    }
                }
            }
        }


          
        public List<TasaMercadoNIF> ListarTasaMercadoNIIF(TasaMercadoNIF pTasaMercado ,Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TasaMercadoNIF> lstTasaNIIF = new List<TasaMercadoNIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {  
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select * from TASA_MERCADO_NIF order by COD_TASA_MERCADO";   
                       
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;  
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TasaMercadoNIF entidad = new TasaMercadoNIF();
                            if (resultado["COD_TASA_MERCADO"] != DBNull.Value) entidad.cod_tasa_mercado = Convert.ToInt32(resultado["COD_TASA_MERCADO"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            lstTasaNIIF.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTasaNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TasaMercadoNIIFData", "ListarTasaMercadoNIIF", ex);
                        return null;
                    }
                }

            }
        }


        public List<TasaMercadoNIF> DatosCondicionNIIF(TasaMercadoNIF pTasaMercadoNIIF, Usuario vUsuario)
        {

            DbDataReader resultado = default(DbDataReader);
            List<TasaMercadoNIF> lstTasa = new List<TasaMercadoNIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select * from SCVARIABLE order by NOMBRE";
                       
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TasaMercadoNIF entidad = new TasaMercadoNIF();

                            if (resultado["IDVARIABLE"] != DBNull.Value) entidad.idvariable = Convert.ToInt32(resultado["IDVARIABLE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VARIABLE1"] != DBNull.Value) entidad.variable = Convert.ToString(resultado["VARIABLE1"]);

                            lstTasa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTasa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TasaMercadoNIIFData", "DatosCondicionNIIF", ex);
                        return null;
                    }
                }
            }
        }


    }
}