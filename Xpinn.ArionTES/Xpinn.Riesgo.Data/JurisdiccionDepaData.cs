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
    public class JurisdiccionDepaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public JurisdiccionDepaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<JurisdiccionDepa> ListarJurisdiccionDepa (JurisdiccionDepa JurisdiccionDepa, Usuario usuario)
        {
            DbDataReader resultado;
            List<JurisdiccionDepa> listaJurisdiccionDepa = new List<JurisdiccionDepa>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT ID_JURISD, COD_DEPA, NOMBRE, CASE VALORACION WHEN 1 THEN 'BAJO' WHEN 2 THEN 'MEDIO' WHEN 3 THEN 'ALTO' ELSE 'EXTREMO' END AS VALORACION  FROM GR_JURISDICCION_DEPA " + ObtenerFiltro(JurisdiccionDepa) + " ORDER BY COD_DEPA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            JurisdiccionDepa entidad = new JurisdiccionDepa();
                            if (resultado["ID_JURISD"] != DBNull.Value) entidad.Id_Jurid = Convert.ToInt64(resultado["ID_JURISD"]);
                            if (resultado["COD_DEPA"] != DBNull.Value) entidad.Cod_Depa = Convert.ToInt64(resultado["COD_DEPA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALORACION"] != DBNull.Value) entidad.valoracion = Convert.ToString(resultado["VALORACION"]);
                            listaJurisdiccionDepa.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaJurisdiccionDepa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("JurisdiccionDepaData", "ListarJurisdiccionDepa", ex);
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
        public JurisdiccionDepa CrearJurisdiccionDepa (JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_Depa = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Depa.ParameterName = "p_Cod_Depa";
                        p_Cod_Depa.Value = pJurisdiccionDepa.Cod_Depa;
                        p_Cod_Depa.Direction = ParameterDirection.Input;
                        p_Cod_Depa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Depa);

                        DbParameter p_Nombre= cmdTransaccionFactory.CreateParameter();
                        p_Nombre.ParameterName = "p_Nombre";
                        p_Nombre.Value = pJurisdiccionDepa.Nombre;
                        p_Nombre.Direction = ParameterDirection.Input;
                        p_Nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Nombre);

                        DbParameter p_valoracion = cmdTransaccionFactory.CreateParameter();
                        p_valoracion.ParameterName = "p_valoracion";
                        p_valoracion.Value = pJurisdiccionDepa.valoracion;
                        p_valoracion.Direction = ParameterDirection.Input;
                        p_valoracion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valoracion);

                        DbParameter p_cod_usua = cmdTransaccionFactory.CreateParameter();
                        p_cod_usua.ParameterName = "p_cod_usua";
                        p_cod_usua.Value = pJurisdiccionDepa.cod_usua;
                        p_cod_usua.Direction = ParameterDirection.Input;
                        p_cod_usua.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_usua);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_GR_JURISDI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pJurisdiccionDepa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CrearJurisdiccionDepa", "CrearJurisdiccionDepa", ex);
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
        public JurisdiccionDepa ModificarJurisdiccionDepa(JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_Depa = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Depa.ParameterName = "p_Cod_Depa";
                        p_Cod_Depa.Value = pJurisdiccionDepa.Cod_Depa;
                        p_Cod_Depa.Direction = ParameterDirection.Input;
                        p_Cod_Depa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Depa);

                        DbParameter p_Nombre = cmdTransaccionFactory.CreateParameter();
                        p_Nombre.ParameterName = "p_Nombre";
                        p_Nombre.Value = pJurisdiccionDepa.Nombre;
                        p_Nombre.Direction = ParameterDirection.Input;
                        p_Nombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Nombre);

                        DbParameter p_valoracion = cmdTransaccionFactory.CreateParameter();
                        p_valoracion.ParameterName = "p_valoracion";
                        p_valoracion.Value = pJurisdiccionDepa.valoracion;
                        p_valoracion.Direction = ParameterDirection.Input;
                        p_valoracion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valoracion);

                        DbParameter p_cod_usua = cmdTransaccionFactory.CreateParameter();
                        p_cod_usua.ParameterName = "p_cod_usua";
                        p_cod_usua.Value = pJurisdiccionDepa.cod_usua;
                        p_cod_usua.Direction = ParameterDirection.Input;
                        p_cod_usua.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_usua);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_GR_JURISDI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pJurisdiccionDepa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("JurisdiccionDepaData", "ModificarJurisdiccionDepa", ex);
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
        public void EliminarJurisdiccionDepa(JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_Cod_Depa = cmdTransaccionFactory.CreateParameter();
                        p_Cod_Depa.ParameterName = "p_Cod_Depa";
                        p_Cod_Depa.Value = pJurisdiccionDepa.Cod_Depa;
                        p_Cod_Depa.Direction = ParameterDirection.Input;
                        p_Cod_Depa.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_Cod_Depa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_GES_GR_JURISDI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("JurisdiccionDepaData", "EliminarJurisdiccionDepa", ex);
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
        public JurisdiccionDepa ConsultarJurisdiccionDepa(JurisdiccionDepa pJurisdiccionDepa, Usuario vUsuario)
        {
            DbDataReader resultado;
            JurisdiccionDepa vJurisdiccionDepa = new JurisdiccionDepa();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM GR_JURISDICCION_DEPA " + ObtenerFiltro(pJurisdiccionDepa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ID_JURISD"] != DBNull.Value) vJurisdiccionDepa.Id_Jurid = Convert.ToInt64(resultado["ID_JURISD"]);
                            if (resultado["COD_DEPA"] != DBNull.Value) vJurisdiccionDepa.Cod_Depa = Convert.ToInt64(resultado["COD_DEPA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) vJurisdiccionDepa.Nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["VALORACION"] != DBNull.Value) vJurisdiccionDepa.valoracion = Convert.ToString(resultado["VALORACION"]);
                            if (resultado["COD_USUA"] != DBNull.Value) vJurisdiccionDepa.cod_usua = Convert.ToInt64(resultado["COD_USUA"]);
                            vJurisdiccionDepa.Tipo = ConsultarCiudad(vJurisdiccionDepa.Cod_Depa, vUsuario);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return vJurisdiccionDepa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("JurisdiccionDepaData", "ConsultarJurisdiccionDepa", ex);
                        return null;
                    }
                }
            }
        }

        public int? ConsultarCiudad(Int64 pCodCiudad, Usuario pUsuario)
        {
            DbDataReader resultado;
            int Tipo = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT TIPO FROM CIUDADES WHERE CODCIUDAD = " + pCodCiudad;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {                         
                            if (resultado["TIPO"] != DBNull.Value) Tipo = Convert.ToInt32(resultado["TIPO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return Tipo;
                    }
                    catch 
                    {
                        return null;
                    }
                }
            }
        }

        public List<JurisdiccionDepa> ListasDesplegables(Usuario usuario)
        {
            DbDataReader resultado;
            List<JurisdiccionDepa> listaJurisdiccionDepa = new List<JurisdiccionDepa>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT ID_JURISD, COD_DEPA, NOMBRE, CASE VALORACION WHEN 1 THEN 'BAJO' WHEN 2 THEN 'MEDIO' WHEN 3 THEN 'ALTO' ELSE NULL END AS VALORACION  FROM GR_JURISDICCION_DEPA ORDER BY ID_JURISD ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            JurisdiccionDepa entidad = new JurisdiccionDepa();
                            if (resultado["ID_JURISD"] != DBNull.Value) entidad.ListaIdStr = Convert.ToString(resultado["ID_JURISD"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.ListaDescripcion = Convert.ToString(resultado["NOMBRE"]);
                            listaJurisdiccionDepa.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaJurisdiccionDepa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("JurisdiccionDepaData", "ListasDesplegables", ex);
                        return null;
                    }
                }
            }
        }

    }
}
