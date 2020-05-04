using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Destinacion
    /// </summary>
    public class DestinacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Destinacion
        /// </summary>
        public DestinacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public Destinacion CrearDestinacion(Destinacion pDestinacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_destino.ParameterName = "p_cod_destino";
                        pcod_destino.Value = pDestinacion.cod_destino;
                        pcod_destino.Direction = ParameterDirection.Input;
                        pcod_destino.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_destino);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pDestinacion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_DESTINAC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDestinacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DestinacionData", "CrearDestinacion", ex);
                        return null;
                    }
                }
            }
        }
        public Destinacion ModificarDestinacion(Destinacion pDestinacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_destino.ParameterName = "p_cod_destino";
                        pcod_destino.Value = pDestinacion.cod_destino;
                        pcod_destino.Direction = ParameterDirection.Input;
                        pcod_destino.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_destino);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pDestinacion.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_DESTINAC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pDestinacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DestinacionData", "ModificarDestinacion", ex);
                        return null;
                    }
                }
            }
        }
        public void EliminarDestinacion(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Destinacion pDestinacion = new Destinacion();
                        pDestinacion = ConsultarDestinacion(pId, vUsuario);

                        DbParameter pcod_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_destino.ParameterName = "p_cod_destino";
                        pcod_destino.Value = pDestinacion.cod_destino;
                        pcod_destino.Direction = ParameterDirection.Input;
                        pcod_destino.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_destino);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_AHO_DESTINAC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DestinacionData", "EliminarDestinacion", ex);
                    }
                }
            }
        }
        public Destinacion ConsultarDestinacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Destinacion entidad = new Destinacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Destinacion_ahorros WHERE COD_DESTINO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
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
                        BOExcepcion.Throw("DestinacionData", "ConsultarDestinacion", ex);
                        return null;
                    }
                }
            }
        }
        public List<Destinacion> ListarDestinacion_Ahorros(Destinacion pDestinacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Destinacion> lstDestinacion = new List<Destinacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DESTINACION_AHORROS " + ObtenerFiltro(pDestinacion) + " ORDER BY COD_DESTINO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Destinacion entidad = new Destinacion();
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstDestinacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDestinacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DestinacionData", "ListarDestinacion_Ahorros", ex);
                        return null;
                    }
                }
            }
        }       
        public List<Destinacion> ListarDestinacion(Destinacion pDestinacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Destinacion> lstDestinacion = new List<Destinacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM DESTINACION_AHORROS " + ObtenerFiltro(pDestinacion) + " ORDER BY COD_DESTINO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Destinacion entidad = new Destinacion();
                            if (resultado["COD_DESTINO"] != DBNull.Value) entidad.cod_destino = Convert.ToInt32(resultado["COD_DESTINO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstDestinacion.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDestinacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DestinacionData", "ListarDestinacion", ex);
                        return null;
                    }
                }
            }
        }
        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(cod_destino) + 1 FROM Destinacion_ahorros ";

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
                        BOExcepcion.Throw("DestinacionData", "ObtenerSiguienteCodigo", ex);
                        return 1;
                    }
                }
            }
        }
    }
}
