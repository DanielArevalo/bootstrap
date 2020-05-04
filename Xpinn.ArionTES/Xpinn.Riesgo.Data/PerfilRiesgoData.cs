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
   public class PerfilRiesgoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public PerfilRiesgoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<PerfilRiesgo> ListarPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario usuario)
        {
            DbDataReader resultado;
            List<PerfilRiesgo> listaPerfilRiesgo = new List<PerfilRiesgo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COD_PERFIL, DESCRIPCION, CASE VALORACION WHEN 1 THEN 'BAJO' WHEN 2 THEN 'MEDIO' WHEN 3 THEN 'ALTO' ELSE NULL END AS VALORACION, TIPO_PERSONA FROM GR_PERFIL_RIESGO " + ObtenerFiltro(pPerfilRiesgo) + " ORDER BY COD_PERFIL ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PerfilRiesgo entidad = new PerfilRiesgo();
                            if (resultado["COD_PERFIL"] != DBNull.Value) entidad.Cod_perfil = Convert.ToInt64(resultado["COD_PERFIL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALORACION"] != DBNull.Value) entidad.valoracion = Convert.ToString(resultado["VALORACION"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) entidad.tipoPersona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            entidad.nomtipoPersona = entidad.tipoPersona == "J" ? "Juridica" : "Todos";
                            listaPerfilRiesgo.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaPerfilRiesgo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilRiesoData", "listaPerfilRiesgo", ex);
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
        public PerfilRiesgo CrearPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_perfil = cmdTransaccionFactory.CreateParameter();
                        p_Cod_perfil.ParameterName = "p_Cod_perfil";
                        p_Cod_perfil.Value = pPerfilRiesgo.Cod_perfil;
                        p_Cod_perfil.Direction = ParameterDirection.Input;
                        p_Cod_perfil.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_perfil);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pPerfilRiesgo.Descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_valoracion = cmdTransaccionFactory.CreateParameter();
                        p_valoracion.ParameterName = "p_valoracion";
                        p_valoracion.Value = pPerfilRiesgo.valoracion;
                        p_valoracion.Direction = ParameterDirection.Input;
                        p_valoracion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valoracion);

                        DbParameter p_tipo_persona = cmdTransaccionFactory.CreateParameter();
                        p_tipo_persona.ParameterName = "p_tipo_persona";
                        p_tipo_persona.Value = pPerfilRiesgo.tipoPersona;
                        p_tipo_persona.Direction = ParameterDirection.Input;
                        p_tipo_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_GR_PERFILRI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPerfilRiesgo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CrearPerfilRiesgo", "CrearPerfilRiesgo", ex);
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
        public PerfilRiesgo ModificarPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_perfil = cmdTransaccionFactory.CreateParameter();
                        p_Cod_perfil.ParameterName = "p_Cod_perfil";
                        p_Cod_perfil.Value = pPerfilRiesgo.Cod_perfil;
                        p_Cod_perfil.Direction = ParameterDirection.Input;
                        p_Cod_perfil.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_perfil);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pPerfilRiesgo.Descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_valoracion = cmdTransaccionFactory.CreateParameter();
                        p_valoracion.ParameterName = "p_valoracion";
                        p_valoracion.Value = pPerfilRiesgo.valoracion;
                        p_valoracion.Direction = ParameterDirection.Input;
                        p_valoracion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valoracion);

                        DbParameter p_tipo_persona = cmdTransaccionFactory.CreateParameter();
                        p_tipo_persona.ParameterName = "p_tipo_persona";
                        p_tipo_persona.Value = pPerfilRiesgo.tipoPersona;
                        p_tipo_persona.Direction = ParameterDirection.Input;
                        p_tipo_persona.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_persona);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_GR_PERFILRI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPerfilRiesgo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilRiesoData", "ModificarPerfilRiesgo", ex);
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
        public void EliminarPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_perfil = cmdTransaccionFactory.CreateParameter();
                        p_Cod_perfil.ParameterName = "p_Cod_perfil";
                        p_Cod_perfil.Value = pPerfilRiesgo.Cod_perfil;
                        p_Cod_perfil.Direction = ParameterDirection.Input;
                        p_Cod_perfil.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_perfil);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_GR_PERFILRI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilRiesoData", "EliminarPerfilRiesgo", ex);
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
        public PerfilRiesgo ConsultarPerfilRiesgo(PerfilRiesgo pPerfilRiesgo, Usuario vUsuario)
        {
            DbDataReader resultado;
            PerfilRiesgo vPerfilRiesgo = new PerfilRiesgo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_PERFIL_RIESGO " + ObtenerFiltro(pPerfilRiesgo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERFIL"] != DBNull.Value) vPerfilRiesgo.Cod_perfil = Convert.ToInt64(resultado["COD_PERFIL"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) vPerfilRiesgo.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALORACION"] != DBNull.Value) vPerfilRiesgo.valoracion = Convert.ToString(resultado["VALORACION"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) vPerfilRiesgo.tipoPersona = Convert.ToString(resultado["TIPO_PERSONA"]);

                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vPerfilRiesgo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PerfilRiesoData", "ConsultarPerfilRiesgo", ex);
                        return null;
                    }
                }
            }
        }
    }
}
