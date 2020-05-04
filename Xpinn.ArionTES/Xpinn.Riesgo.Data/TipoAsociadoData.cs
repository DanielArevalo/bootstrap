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
   public class TipoAsociadoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public TipoAsociadoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<TipoAsociado> ListarTipoAsociado(TipoAsociado pTipoAsociado, Usuario usuario)
        {
            DbDataReader resultado;
            List<TipoAsociado> listaTipoAsociado = new List<TipoAsociado>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COD_TIPOASOCIADO, DESCRIPCION, CASE VALORACION WHEN 1 THEN 'BAJO' WHEN 2 THEN 'MEDIO' WHEN 3 THEN 'ALTO' ELSE NULL END AS VALORACION FROM GR_TIPO_ASOCIADO " + ObtenerFiltro(pTipoAsociado) + " ORDER BY COD_TIPOASOCIADO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoAsociado entidad = new TipoAsociado();
                            if (resultado["COD_TIPOASOCIADO"] != DBNull.Value) entidad.Cod_tipoasociado = Convert.ToInt64(resultado["COD_TIPOASOCIADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALORACION"] != DBNull.Value) entidad.valoracion = Convert.ToString(resultado["VALORACION"]);
                            listaTipoAsociado.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaTipoAsociado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilRiesoData", "listaTipoAsociado", ex);
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
        public TipoAsociado CrearTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_tipoasociado = cmdTransaccionFactory.CreateParameter();
                        p_Cod_tipoasociado.ParameterName = "p_Cod_tipoasociado";
                        p_Cod_tipoasociado.Value = pTipoAsociado.Cod_tipoasociado;
                        p_Cod_tipoasociado.Direction = ParameterDirection.Input;
                        p_Cod_tipoasociado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_tipoasociado);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pTipoAsociado.Descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_valoracion = cmdTransaccionFactory.CreateParameter();
                        p_valoracion.ParameterName = "p_valoracion";
                        p_valoracion.Value = pTipoAsociado.valoracion;
                        p_valoracion.Direction = ParameterDirection.Input;
                        p_valoracion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valoracion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_TIPOASOCIADO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoAsociado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CrearTipoAsociado", "CrearTipoAsociado", ex);
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
        public TipoAsociado ModificarTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_tipoasociado = cmdTransaccionFactory.CreateParameter();
                        p_Cod_tipoasociado.ParameterName = "p_Cod_tipoasociado";
                        p_Cod_tipoasociado.Value = pTipoAsociado.Cod_tipoasociado;
                        p_Cod_tipoasociado.Direction = ParameterDirection.Input;
                        p_Cod_tipoasociado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_tipoasociado);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pTipoAsociado.Descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_valoracion = cmdTransaccionFactory.CreateParameter();
                        p_valoracion.ParameterName = "p_valoracion";
                        p_valoracion.Value = pTipoAsociado.valoracion;
                        p_valoracion.Direction = ParameterDirection.Input;
                        p_valoracion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valoracion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_TIPOASOCIADO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoAsociado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilRiesoData", "ModificarTipoAsociado", ex);
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
        public void EliminarTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_tipoasociado = cmdTransaccionFactory.CreateParameter();
                        p_Cod_tipoasociado.ParameterName = "p_Cod_tipoasociado";
                        p_Cod_tipoasociado.Value = pTipoAsociado.Cod_tipoasociado;
                        p_Cod_tipoasociado.Direction = ParameterDirection.Input;
                        p_Cod_tipoasociado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_tipoasociado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_TIPOASOCIADO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilRiesoData", "EliminarActividades", ex);
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
        public TipoAsociado ConsultarTipoAsociado(TipoAsociado pTipoAsociado, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoAsociado vTipoAsociado = new TipoAsociado();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_TIPO_ASOCIADO " + ObtenerFiltro(pTipoAsociado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_TIPOASOCIADO"] != DBNull.Value) vTipoAsociado.Cod_tipoasociado = Convert.ToInt64(resultado["COD_TIPOASOCIADO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) vTipoAsociado.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALORACION"] != DBNull.Value) vTipoAsociado.valoracion = Convert.ToString(resultado["VALORACION"]);
                          
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vTipoAsociado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilRiesoData", "ConsultarTipoAsociado", ex);
                        return null;
                    }
                }
            }
        }
    }
}
