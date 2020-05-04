using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.ActivosFijos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ClaseActivoS
    /// </summary>
    public class ClaseActivoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ClaseActivoS
        /// </summary>
        public ClaseActivoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla ClaseActivoS de la base de datos
        /// </summary>
        /// <param name="pClaseActivo">Entidad ClaseActivo</param>
        /// <returns>Entidad ClaseActivo creada</returns>
        public ClaseActivo CrearClaseActivo(ClaseActivo pClaseActivo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pclase = cmdTransaccionFactory.CreateParameter();
                        pclase.ParameterName = "p_clase";
                        pclase.Value = pClaseActivo.clase;
                        pclase.Direction = ParameterDirection.Input;
                        pclase.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pclase);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pClaseActivo.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_CLASE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pClaseActivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClaseActivoData", "CrearClaseActivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla ClaseActivoS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ClaseActivo modificada</returns>
        public ClaseActivo ModificarClaseActivo(ClaseActivo pClaseActivo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pclase = cmdTransaccionFactory.CreateParameter();
                        pclase.ParameterName = "p_clase";
                        pclase.Value = pClaseActivo.clase;
                        pclase.Direction = ParameterDirection.Input;
                        pclase.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pclase);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pClaseActivo.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_CLASE_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pClaseActivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClaseActivoData", "ModificarClaseActivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ClaseActivoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ClaseActivoS</param>
        public void EliminarClaseActivo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ClaseActivo pClaseActivo = new ClaseActivo();
                        pClaseActivo = ConsultarClaseActivo(pId, vUsuario);

                        DbParameter pclase = cmdTransaccionFactory.CreateParameter();
                        pclase.ParameterName = "p_clase";
                        pclase.Value = pClaseActivo.clase;
                        pclase.Direction = ParameterDirection.Input;
                        pclase.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pclase);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_CLASE_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConceptoData", "EliminarConcepto", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ClaseActivoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ClaseActivoS</param>
        /// <returns>Entidad ClaseActivo consultado</returns>
        public ClaseActivo ConsultarClaseActivo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ClaseActivo entidad = new ClaseActivo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Clase_Activo WHERE CLASE = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToInt32(resultado["CLASE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
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
                        BOExcepcion.Throw("ClaseActivoData", "ConsultarClaseActivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ClaseActivo dados unos filtros
        /// </summary>
        /// <param name="pClaseActivo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ClaseActivos obtenidos</returns>
        public List<ClaseActivo> ListarClaseActivo(ClaseActivo pClaseActivo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ClaseActivo> lstClaseActivo = new List<ClaseActivo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Clase_Activo " + ObtenerFiltro(pClaseActivo) + " ORDER BY CLASE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ClaseActivo entidad = new ClaseActivo();
                            if (resultado["CLASE"] != DBNull.Value) entidad.clase = Convert.ToInt32(resultado["CLASE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstClaseActivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstClaseActivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClaseActivoData", "ListarClaseActivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el siguiente codigo disponible de la tabla
        /// </summary>
        /// <returns>codigo disponible</returns>
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(Clase) + 1 FROM Clase_Activo ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClaseActivoData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


        //AGREGADO

        public List<ClaseActivo> ListarClasificacion(ClaseActivo pClaseActivo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ClaseActivo> lstClasificacion = new List<ClaseActivo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM clasificacion_activo_nif " + ObtenerFiltro(pClaseActivo) + " ORDER BY CODCLASIFICACION_NIF ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ClaseActivo entidad = new ClaseActivo();
                            if (resultado["CODCLASIFICACION_NIF"] != DBNull.Value) entidad.cod_clasifica = Convert.ToInt32(resultado["CODCLASIFICACION_NIF"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstClasificacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstClasificacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ClaseActivoData", "ListarClasificacion", ex);
                        return null;
                    }
                }
            }
        }


       
    }
}