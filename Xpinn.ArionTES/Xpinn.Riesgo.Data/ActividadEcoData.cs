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
    public class ActividadEcoData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ActividadEcoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<ActividadEco> ListarActividadEco(ActividadEco Actividad, Usuario usuario)
        {
            DbDataReader resultado;
            List<ActividadEco> listaActividadesEco = new List<ActividadEco>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT ID_ACTIVIDAD ,COD_ACTIVIDAD , DESCRIPCION, CASE VALORACION WHEN 1 THEN 'BAJO' WHEN 2 THEN 'MEDIO' WHEN 3 THEN 'ALTO' WHEN 4 THEN 'EXTREMO' ELSE NULL END AS VALORACION FROM GR_ACTIVIDAD_ECONOMICA " + ObtenerFiltro(Actividad) + " ORDER BY COD_ACTIVIDAD ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ActividadEco entidad = new ActividadEco();
                            if (resultado["ID_ACTIVIDAD"] != DBNull.Value) entidad.Id_actividad = Convert.ToInt64(resultado["ID_ACTIVIDAD"]);
                            if (resultado["COD_ACTIVIDAD"] != DBNull.Value) entidad.Cod_actividad = Convert.ToString(resultado["COD_ACTIVIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALORACION"] != DBNull.Value) entidad.valoracion = Convert.ToString(resultado["VALORACION"]);
                            listaActividadesEco.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaActividadesEco;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadEcoData", "ListarActividadEco", ex);
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
        public ActividadEco CrearActividad(ActividadEco pActividadEco, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_actividad = cmdTransaccionFactory.CreateParameter();
                        p_Cod_actividad.ParameterName = "p_Cod_actividad";
                        p_Cod_actividad.Value = pActividadEco.Cod_actividad;
                        p_Cod_actividad.Direction = ParameterDirection.Input;
                        p_Cod_actividad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_actividad);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pActividadEco.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_valoracion = cmdTransaccionFactory.CreateParameter();
                        p_valoracion.ParameterName = "p_valoracion";
                        p_valoracion.Value = pActividadEco.valoracion;
                        p_valoracion.Direction = ParameterDirection.Input;
                        p_valoracion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valoracion);

                        DbParameter p_cod_usua = cmdTransaccionFactory.CreateParameter();
                        p_cod_usua.ParameterName = "p_cod_usua";
                        p_cod_usua.Value = pActividadEco.cod_usua;
                        p_cod_usua.Direction = ParameterDirection.Input;
                        p_cod_usua.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_usua);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_GR_ACTIVID_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pActividadEco;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadEcoData", "CrearActividad", ex);
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
        public ActividadEco ModificarActividad(ActividadEco pActividadEco, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_actividad = cmdTransaccionFactory.CreateParameter();
                        p_Cod_actividad.ParameterName = "p_Cod_actividad";
                        p_Cod_actividad.Value = pActividadEco.Cod_actividad;
                        p_Cod_actividad.Direction = ParameterDirection.Input;
                        p_Cod_actividad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_actividad);

                        DbParameter p_descripcion = cmdTransaccionFactory.CreateParameter();
                        p_descripcion.ParameterName = "p_descripcion";
                        p_descripcion.Value = pActividadEco.descripcion;
                        p_descripcion.Direction = ParameterDirection.Input;
                        p_descripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_descripcion);

                        DbParameter p_valoracion = cmdTransaccionFactory.CreateParameter();
                        p_valoracion.ParameterName = "p_valoracion";
                        p_valoracion.Value = pActividadEco.valoracion;
                        p_valoracion.Direction = ParameterDirection.Input;
                        p_valoracion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valoracion);

                        DbParameter p_cod_usua = cmdTransaccionFactory.CreateParameter();
                        p_cod_usua.ParameterName = "p_cod_usua";
                        p_cod_usua.Value = pActividadEco.cod_usua;
                        p_cod_usua.Direction = ParameterDirection.Input;
                        p_cod_usua.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_usua);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_GR_ACTIVID_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pActividadEco;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadEcoData", "ModificarActividad", ex);
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
        public void EliminarActividades(ActividadEco pActividadEco, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_actividad = cmdTransaccionFactory.CreateParameter();
                        p_Cod_actividad.ParameterName = "p_Cod_actividad";
                        p_Cod_actividad.Value = pActividadEco.Cod_actividad;
                        p_Cod_actividad.Direction = ParameterDirection.Input;
                        p_Cod_actividad.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_actividad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_GR_ACTIVID_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActividadEcoData", "EliminarActividades", ex);
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
        public ActividadEco ConsultarActividades(ActividadEco pActividadEco, Usuario vUsuario)
        {
            DbDataReader resultado;
            ActividadEco vActividadEco = new ActividadEco();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_ACTIVIDAD_ECONOMICA " + ObtenerFiltro(pActividadEco);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ID_ACTIVIDAD"] != DBNull.Value) vActividadEco.Id_actividad = Convert.ToInt64(resultado["ID_ACTIVIDAD"]);
                            if (resultado["COD_ACTIVIDAD"] != DBNull.Value) vActividadEco.Cod_actividad = Convert.ToString(resultado["COD_ACTIVIDAD"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) vActividadEco.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALORACION"] != DBNull.Value) vActividadEco.valoracion = Convert.ToString(resultado["VALORACION"]);
                            if (resultado["COD_USUA"] != DBNull.Value) vActividadEco.cod_usua = Convert.ToInt64(resultado["COD_USUA"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vActividadEco;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IdentificacionData", "ConsultarFactorRiesgo", ex);
                        return null;
                    }
                }
            }
        }
    }
}
