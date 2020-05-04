using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TipSopCaj
    /// </summary>
    public class TipSopCajData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TipSopCaj
        /// </summary>
        public TipSopCajData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TipSopCaj de la base de datos
        /// </summary>
        /// <param name="pTipSopCaj">Entidad TipSopCaj</param>
        /// <returns>Entidad TipSopCaj creada</returns>
        public TipSopCaj CrearTipSopCaj(TipSopCaj pTipSopCaj, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtiposop = cmdTransaccionFactory.CreateParameter();
                        pidtiposop.ParameterName = "p_idtiposop";
                        pidtiposop.Value = pTipSopCaj.idtiposop;
                        pidtiposop.Direction = ParameterDirection.Input;
                        pidtiposop.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtiposop);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pTipSopCaj.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pTipSopCaj.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_TIPSOPCAJ_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipSopCaj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipSopCajData", "CrearTipSopCaj", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TipSopCaj de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TipSopCaj modificada</returns>
        public TipSopCaj ModificarTipSopCaj(TipSopCaj pTipSopCaj, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidtiposop = cmdTransaccionFactory.CreateParameter();
                        pidtiposop.ParameterName = "p_idtiposop";
                        pidtiposop.Value = pTipSopCaj.idtiposop;
                        pidtiposop.Direction = ParameterDirection.Input;
                        pidtiposop.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtiposop);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pTipSopCaj.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pTipSopCaj.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_TIPSOPCAJ_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipSopCaj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipSopCajData", "ModificarTipSopCaj", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TipSopCaj de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TipSopCaj</param>
        public void EliminarTipSopCaj(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipSopCaj pTipSopCaj = new TipSopCaj();
                        pTipSopCaj = ConsultarTipSopCaj(pId, vUsuario);

                        DbParameter pidtiposop = cmdTransaccionFactory.CreateParameter();
                        pidtiposop.ParameterName = "p_idtiposop";
                        pidtiposop.Value = pTipSopCaj.idtiposop;
                        pidtiposop.Direction = ParameterDirection.Input;
                        pidtiposop.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidtiposop);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_TIPSOPCAJ_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipSopCajData", "EliminarTipSopCaj", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TipSopCaj de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TipSopCajS</param>
        /// <returns>Entidad TipSopCaj consultado</returns>
        public TipSopCaj ConsultarTipSopCaj(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipSopCaj entidad = new TipSopCaj();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Tip_Sop_Caj WHERE IDTIPOSOP = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDTIPOSOP"] != DBNull.Value) entidad.idtiposop = Convert.ToInt32(resultado["IDTIPOSOP"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
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
                        BOExcepcion.Throw("TipSopCajData", "ConsultarTipSopCaj", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TipSopCaj dados unos filtros
        /// </summary>
        /// <param name="pTipSopCajS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipSopCaj obtenidos</returns>
        public List<TipSopCaj> ListarTipSopCaj(TipSopCaj pTipSopCaj, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<TipSopCaj> lstTipSopCaj = new List<TipSopCaj>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Tip_Sop_Caj " + ObtenerFiltro(pTipSopCaj) + " ORDER BY IDTIPOSOP ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipSopCaj entidad = new TipSopCaj();
                            if (resultado["IDTIPOSOP"] != DBNull.Value) entidad.idtiposop = Convert.ToInt32(resultado["IDTIPOSOP"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipSopCaj.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipSopCaj;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipSopCajData", "ListarTipSopCaj", ex);
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
                        string sql = "SELECT MAX(idtiposop) + 1 FROM Tip_Sop_Caj ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());

                        return resultado;
                    }
                    catch
                    {                        
                        return 0;
                    }
                }
            }
        }



    }
}