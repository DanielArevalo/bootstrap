using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Data
{
    public class valoracion_controlRiesgoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public valoracion_controlRiesgoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<valoracion_control> Listarvaloracion_control(valoracion_control pvaloracion_control, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<valoracion_control> lstvaloracion_control = new List<valoracion_control>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM GR_VALORACION_CONTROL " + ObtenerFiltro(pvaloracion_control) + " ORDER BY COD_CONTROL";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            valoracion_control entidad = new valoracion_control();

                            if (resultado["COD_CONTROL"] != DBNull.Value) entidad.cod_control = Convert.ToInt64(resultado["COD_CONTROL"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FRECUENCIA"] != DBNull.Value) entidad.frecuencia = Convert.ToInt32(resultado["FRECUENCIA"]);
                            if (resultado["DESC_FRECUENCIA"] != DBNull.Value) entidad.desc_frecuencia = Convert.ToString(resultado["DESC_FRECUENCIA"]);
                            if (resultado["RANGO"] != DBNull.Value) entidad.rango = Convert.ToInt32(resultado["RANGO"]);
                            if (resultado["RANGO_MINIMO"] != DBNull.Value) entidad.rango_minimo = Convert.ToInt32(resultado["RANGO_MINIMO"]);
                            if (resultado["RANGO_MAXIMO"] != DBNull.Value) entidad.rango_maximo = Convert.ToInt32(resultado["RANGO_MAXIMO"]);
                            lstvaloracion_control.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstvaloracion_control; 
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("valoracion_controlData", "Listarvaloracion_control", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Crear registro de las actividades de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con los datos del factor de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public valoracion_control Crearvaloracion_control(valoracion_control pvaloracion_control, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_control = cmdTransaccionFactory.CreateParameter();
                        p_cod_control.ParameterName = "p_cod_control";
                        p_cod_control.Value = pvaloracion_control.cod_control;
                        p_cod_control.Direction = ParameterDirection.InputOutput;
                        p_cod_control.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_control);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = pvaloracion_control.valor;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        DbParameter p_calificacion = cmdTransaccionFactory.CreateParameter();
                        p_calificacion.ParameterName = "p_calificacion";
                        p_calificacion.Value = pvaloracion_control.calificacion;
                        p_calificacion.Direction = ParameterDirection.Input;
                        p_calificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_calificacion);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pvaloracion_control.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_frecuencia = cmdTransaccionFactory.CreateParameter();
                        p_frecuencia.ParameterName = "p_frecuencia";
                        p_frecuencia.Value = pvaloracion_control.frecuencia;
                        p_frecuencia.Direction = ParameterDirection.Input;
                        p_frecuencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_frecuencia);

                        DbParameter p_desc_frecuencia = cmdTransaccionFactory.CreateParameter();
                        p_desc_frecuencia.ParameterName = "p_desc_frecuencia";
                        p_desc_frecuencia.Value = pvaloracion_control.desc_frecuencia;
                        p_desc_frecuencia.Direction = ParameterDirection.Input;
                        p_desc_frecuencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_desc_frecuencia);

                        DbParameter p_rango = cmdTransaccionFactory.CreateParameter();
                        p_rango.ParameterName = "p_rango";
                        p_rango.Value = pvaloracion_control.rango;
                        p_rango.Direction = ParameterDirection.Input;
                        p_rango.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_rango);
                        
                        DbParameter p_rango_minimo = cmdTransaccionFactory.CreateParameter();
                        p_rango_minimo.ParameterName = "p_rango_minimo";
                        p_rango_minimo.Value = pvaloracion_control.rango_minimo;
                        p_rango_minimo.Direction = ParameterDirection.Input;
                        p_rango_minimo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_rango_minimo);
                        
                        DbParameter p_rango_maximo = cmdTransaccionFactory.CreateParameter();
                        p_rango_maximo.ParameterName = "p_rango_maximo";
                        p_rango_maximo.Value = pvaloracion_control.rango_maximo;
                        p_rango_maximo.Direction = ParameterDirection.Input;
                        p_rango_maximo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_rango_maximo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GR_GR_VALORAC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pvaloracion_control;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("valoracion_controlData", "Crearvaloracion_control", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro de las actividades de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con los datos del factor</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public valoracion_control Modificarvaloracion_control(valoracion_control pvaloracion_control, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_control = cmdTransaccionFactory.CreateParameter();
                        p_cod_control.ParameterName = "p_cod_control";
                        p_cod_control.Value = pvaloracion_control.cod_control;
                        p_cod_control.Direction = ParameterDirection.Input;
                        p_cod_control.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_control);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = pvaloracion_control.valor;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        DbParameter p_calificacion = cmdTransaccionFactory.CreateParameter();
                        p_calificacion.ParameterName = "p_calificacion";
                        p_calificacion.Value = pvaloracion_control.calificacion;
                        p_calificacion.Direction = ParameterDirection.Input;
                        p_calificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_calificacion);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pvaloracion_control.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_frecuencia = cmdTransaccionFactory.CreateParameter();
                        p_frecuencia.ParameterName = "p_frecuencia";
                        p_frecuencia.Value = pvaloracion_control.frecuencia;
                        p_frecuencia.Direction = ParameterDirection.Input;
                        p_frecuencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_frecuencia);

                        DbParameter p_desc_frecuencia = cmdTransaccionFactory.CreateParameter();
                        p_desc_frecuencia.ParameterName = "p_desc_frecuencia";
                        p_desc_frecuencia.Value = pvaloracion_control.desc_frecuencia;
                        p_desc_frecuencia.Direction = ParameterDirection.Input;
                        p_desc_frecuencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_desc_frecuencia);

                        DbParameter p_rango = cmdTransaccionFactory.CreateParameter();
                        p_rango.ParameterName = "p_rango";
                        p_rango.Value = pvaloracion_control.rango;
                        p_rango.Direction = ParameterDirection.Input;
                        p_rango.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_rango);

                        DbParameter p_rango_minimo = cmdTransaccionFactory.CreateParameter();
                        p_rango_minimo.ParameterName = "p_rango_minimo";
                        p_rango_minimo.Value = pvaloracion_control.rango_minimo;
                        p_rango_minimo.Direction = ParameterDirection.Input;
                        p_rango_minimo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_rango_minimo);

                        DbParameter p_rango_maximo = cmdTransaccionFactory.CreateParameter();
                        p_rango_maximo.ParameterName = "p_rango_maximo";
                        p_rango_maximo.Value = pvaloracion_control.rango_maximo;
                        p_rango_maximo.Direction = ParameterDirection.Input;
                        p_rango_maximo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_rango_maximo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GR_GR_VALORAC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pvaloracion_control;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("valoracion_controlData", "Modificarvaloracion_control", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Eliminar  registro de las actividades de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con el código del factor</param>
        /// <param name="vUsuario"></param>
        public void Eliminarvaloracion_control(valoracion_control pvaloracion_control, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_control = cmdTransaccionFactory.CreateParameter();
                        p_cod_control.ParameterName = "p_cod_control";
                        p_cod_control.Value = pvaloracion_control.cod_control;
                        p_cod_control.Direction = ParameterDirection.Input;
                        p_cod_control.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_control);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = pvaloracion_control.valor;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        DbParameter p_calificacion = cmdTransaccionFactory.CreateParameter();
                        p_calificacion.ParameterName = "p_calificacion";
                        p_calificacion.Value = pvaloracion_control.calificacion;
                        p_calificacion.Direction = ParameterDirection.Input;
                        p_calificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_calificacion);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pvaloracion_control.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_frecuencia = cmdTransaccionFactory.CreateParameter();
                        p_frecuencia.ParameterName = "p_frecuencia";
                        p_frecuencia.Value = pvaloracion_control.frecuencia;
                        p_frecuencia.Direction = ParameterDirection.Input;
                        p_frecuencia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_frecuencia);

                        DbParameter p_desc_frecuencia = cmdTransaccionFactory.CreateParameter();
                        p_desc_frecuencia.ParameterName = "p_desc_frecuencia";
                        p_desc_frecuencia.Value = pvaloracion_control.desc_frecuencia;
                        p_desc_frecuencia.Direction = ParameterDirection.Input;
                        p_desc_frecuencia.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_desc_frecuencia);

                        DbParameter p_rango = cmdTransaccionFactory.CreateParameter();
                        p_rango.ParameterName = "p_rango";
                        p_rango.Value = pvaloracion_control.rango;
                        p_rango.Direction = ParameterDirection.Input;
                        p_rango.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_rango);

                        DbParameter p_rango_minimo = cmdTransaccionFactory.CreateParameter();
                        p_rango_minimo.ParameterName = "p_rango_minimo";
                        p_rango_minimo.Value = pvaloracion_control.rango_minimo;
                        p_rango_minimo.Direction = ParameterDirection.Input;
                        p_rango_minimo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_rango_minimo);

                        DbParameter p_rango_maximo = cmdTransaccionFactory.CreateParameter();
                        p_rango_maximo.ParameterName = "p_rango_maximo";
                        p_rango_maximo.Value = pvaloracion_control.rango_maximo;
                        p_rango_maximo.Direction = ParameterDirection.Input;
                        p_rango_maximo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_rango_maximo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GR_GR_VALORAC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("valoracion_controlData", "Eliminarvaloracion_control", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Consultar factor de riesgo especifico
        /// </summary>
        /// <param name="pParametro">Objeto con datos para el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public valoracion_control Consultarvaloracion_control(valoracion_control pvaloracion_control, Usuario vUsuario)
        {
            DbDataReader resultado;
            valoracion_control entidad = new valoracion_control();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_VALORACION_CONTROL " + ObtenerFiltro(pvaloracion_control);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_CONTROL"] != DBNull.Value) entidad.cod_control = Convert.ToInt64(resultado["COD_CONTROL"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["CALIFICACION"] != DBNull.Value) entidad.calificacion = Convert.ToString(resultado["CALIFICACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FRECUENCIA"] != DBNull.Value) entidad.frecuencia = Convert.ToInt32(resultado["FRECUENCIA"]);
                            if (resultado["DESC_FRECUENCIA"] != DBNull.Value) entidad.desc_frecuencia = Convert.ToString(resultado["DESC_FRECUENCIA"]);
                            if (resultado["RANGO"] != DBNull.Value) entidad.rango = Convert.ToInt32(resultado["RANGO"]);
                            if (resultado["RANGO_MINIMO"] != DBNull.Value) entidad.rango_minimo = Convert.ToInt32(resultado["RANGO_MINIMO"]);
                            if (resultado["RANGO_MAXIMO"] != DBNull.Value) entidad.rango_maximo = Convert.ToInt32(resultado["RANGO_MAXIMO"]);
                            
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
                        BOExcepcion.Throw("valoracion_controlData", "Consultarvaloracion_control", ex);
                        return null;
                    }
                }
            }
        }
    }
}