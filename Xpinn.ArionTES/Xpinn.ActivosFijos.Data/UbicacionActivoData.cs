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
    /// Objeto de acceso a datos para la tabla UbicacionActivoS
    /// </summary>
    public class UbicacionActivoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla UbicacionActivoS
        /// </summary>
        public UbicacionActivoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla UbicacionActivoS de la base de datos
        /// </summary>
        /// <param name="pUbicacionActivo">Entidad UbicacionActivo</param>
        /// <returns>Entidad UbicacionActivo creada</returns>
        public UbicacionActivo CrearUbicacionActivo(UbicacionActivo pUbicacionActivo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_ubica = cmdTransaccionFactory.CreateParameter();
                        pcod_ubica.ParameterName = "p_cod_ubica";
                        pcod_ubica.Value = pUbicacionActivo.cod_ubica;
                        pcod_ubica.Direction = ParameterDirection.Input;
                        pcod_ubica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ubica);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pUbicacionActivo.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_UBICACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUbicacionActivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UbicacionActivoData", "CrearUbicacionActivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla UbicacionActivoS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad UbicacionActivo modificada</returns>
        public UbicacionActivo ModificarUbicacionActivo(UbicacionActivo pUbicacionActivo, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_ubica = cmdTransaccionFactory.CreateParameter();
                        pcod_ubica.ParameterName = "p_cod_ubica";
                        pcod_ubica.Value = pUbicacionActivo.cod_ubica;
                        pcod_ubica.Direction = ParameterDirection.Input;
                        pcod_ubica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ubica);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pUbicacionActivo.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_UBICACION_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pUbicacionActivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UbicacionActivoData", "ModificarUbicacionActivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla UbicacionActivoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de UbicacionActivoS</param>
        public void EliminarUbicacionActivo(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        UbicacionActivo pUbicacionActivo = new UbicacionActivo();
                        pUbicacionActivo = ConsultarUbicacionActivo(pId, vUsuario);

                        DbParameter pcod_ubica = cmdTransaccionFactory.CreateParameter();
                        pcod_ubica.ParameterName = "p_cod_ubica";
                        pcod_ubica.Value = pUbicacionActivo.cod_ubica;
                        pcod_ubica.Direction = ParameterDirection.Input;
                        pcod_ubica.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ubica);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_ACT_UBICACION_ELIMI";
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
        /// Obtiene un registro en la tabla UbicacionActivoS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla UbicacionActivoS</param>
        /// <returns>Entidad UbicacionActivo consultado</returns>
        public UbicacionActivo ConsultarUbicacionActivo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            UbicacionActivo entidad = new UbicacionActivo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Ubicacion_Activo WHERE COD_UBICA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_UBICA"] != DBNull.Value) entidad.cod_ubica = Convert.ToInt32(resultado["COD_UBICA"]);
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
                        BOExcepcion.Throw("UbicacionActivoData", "ConsultarUbicacionActivo", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla UbicacionActivo dados unos filtros
        /// </summary>
        /// <param name="pUbicacionActivo">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de UbicacionActivos obtenidos</returns>
        public List<UbicacionActivo> ListarUbicacionActivo(UbicacionActivo pUbicacionActivo, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<UbicacionActivo> lstUbicacionActivo = new List<UbicacionActivo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Ubicacion_Activo " + ObtenerFiltro(pUbicacionActivo) + " ORDER BY COD_UBICA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            UbicacionActivo entidad = new UbicacionActivo();
                            if (resultado["COD_UBICA"] != DBNull.Value) entidad.cod_ubica = Convert.ToInt32(resultado["COD_UBICA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstUbicacionActivo.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstUbicacionActivo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UbicacionActivoData", "ListarUbicacionActivo", ex);
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
                        string sql = "SELECT MAX(cod_ubica) + 1 FROM Ubicacion_Activo ";

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
                        BOExcepcion.Throw("UbicacionActivoData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


    }
}