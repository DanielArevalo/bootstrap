using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities; 

namespace Xpinn.Caja.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla TIPO_MOTIVO_ANULACION
    /// </summary>
    public class TipoMotivoAnuData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary> 
        /// Constructor del objeto de acceso a datos para la tabla TIPO_MOTIVO_ANULACION
        /// </summary>
        public TipoMotivoAnuData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TIPO_MOTIVO_ANULACION de la base de datos
        /// </summary>
        /// <param name="pTipoMotivoAnu">Entidad TipoMotivoAnu</param>
        /// <returns>Entidad TipoMotivoAnu creada</returns>
        public TipoMotivoAnu CrearTipoMotivoAnu(TipoMotivoAnu pTipoMotivoAnu, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPO_MOTIVO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_MOTIVO.ParameterName = "p_tipo_motivo";
                        pTIPO_MOTIVO.Value = pTipoMotivoAnu.tipo_motivo;
                        pTIPO_MOTIVO.Direction = ParameterDirection.InputOutput;


                        cmdTransaccionFactory.Parameters.Add(pTIPO_MOTIVO);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Caja_TIPO_MOTIVO_ANULACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoMotivoAnu, "TIPO_MOTIVO_ANULACION",pUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pTipoMotivoAnu.tipo_motivo = Convert.ToInt64(pTIPO_MOTIVO.Value);
                        return pTipoMotivoAnu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoMotivoAnuData", "CrearTipoMotivoAnu", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TIPO_MOTIVO_ANULACION de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad TipoMotivoAnu modificada</returns>
        public TipoMotivoAnu ModificarTipoMotivoAnu(TipoMotivoAnu pTipoMotivoAnu, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTIPO_MOTIVO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_MOTIVO.ParameterName = "p_TIPO_MOTIVO";
                        pTIPO_MOTIVO.Value = pTipoMotivoAnu.tipo_motivo;

                        cmdTransaccionFactory.Parameters.Add(pTIPO_MOTIVO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Caja_TIPO_MOTIVO_ANULACION_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoMotivoAnu, "TIPO_MOTIVO_ANULACION",pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA

                        return pTipoMotivoAnu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoMotivoAnuData", "ModificarTipoMotivoAnu", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TIPO_MOTIVO_ANULACION de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TIPO_MOTIVO_ANULACION</param>
        public void EliminarTipoMotivoAnu(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoMotivoAnu pTipoMotivoAnu = new TipoMotivoAnu();

                        if (pUsuario.programaGeneraLog)
                            pTipoMotivoAnu = ConsultarTipoMotivoAnu(pId, pUsuario); //REGISTRO DE AUDITORIA

                        DbParameter pTIPO_MOTIVO = cmdTransaccionFactory.CreateParameter();
                        pTIPO_MOTIVO.ParameterName = "p_tipo_motivo";
                        pTIPO_MOTIVO.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(pTIPO_MOTIVO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_Xpinn_Caja_TIPO_MOTIVO_ANULACION_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoMotivoAnu, "TIPO_MOTIVO_ANULACION", pUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoMotivoAnuData", "EliminarTipoMotivoAnu", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TIPO_MOTIVO_ANULACION de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TIPO_MOTIVO_ANULACION</param>
        /// <returns>Entidad TipoMotivoAnu consultado</returns>
        public TipoMotivoAnu ConsultarTipoMotivoAnu(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TipoMotivoAnu entidad = new TipoMotivoAnu();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPO_MOTIVO_ANULACION WHERE tipo_motivo = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["TIPO_MOTIVO"] != DBNull.Value) entidad.tipo_motivo = Convert.ToInt64(resultado["TIPO_MOTIVO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch(Exception ex)
                    {
                        BOExcepcion.Throw("TipoMotivoAnuData", "ConsultarTipoMotivoAnu", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TIPO_MOTIVO_ANULACION dados unos filtros
        /// </summary>
        /// <param name="pTIPO_MOTIVO_ANULACION">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de TipoMotivoAnu obtenidos</returns>
        public List<TipoMotivoAnu> ListarTipoMotivoAnu(TipoMotivoAnu pTipoMotivoAnu, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoMotivoAnu> lstTipoMotivoAnu = new List<TipoMotivoAnu>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPO_MOTIVO_ANULACION " + ObtenerFiltro(pTipoMotivoAnu) + " ORDER BY TIPO_MOTIVO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoMotivoAnu entidad = new TipoMotivoAnu();

                            if (resultado["TIPO_MOTIVO"] != DBNull.Value) entidad.tipo_motivo = Convert.ToInt64(resultado["TIPO_MOTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstTipoMotivoAnu.Add(entidad);
                        }

                        return lstTipoMotivoAnu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoMotivoAnuData", "ListarTipoMotivoAnu", ex);
                        return null;
                    }
                }
            }
        }




        public TipoMotivoAnu CrearTipoMotivoAnus(TipoMotivoAnu pTipoMotivoAnu, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_motivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_motivo.ParameterName = "p_tipo_motivo";
                        ptipo_motivo.Value = pTipoMotivoAnu.tipo_motivo;
                        ptipo_motivo.Direction = ParameterDirection.Input;
                        ptipo_motivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_motivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pTipoMotivoAnu.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_MOTANULA_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoMotivoAnu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoMotivoAnuData", "CrearTipoMotivoAnu", ex);
                        return null;
                    }
                }
            }
        }


        public TipoMotivoAnu ModificarTipoMotivoAnus(TipoMotivoAnu pTipoMotivoAnu, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter ptipo_motivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_motivo.ParameterName = "p_tipo_motivo";
                        ptipo_motivo.Value = pTipoMotivoAnu.tipo_motivo;
                        ptipo_motivo.Direction = ParameterDirection.Input;
                        ptipo_motivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_motivo);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pTipoMotivoAnu.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_MOTANULA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoMotivoAnu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoMotivoAnuData", "ModificarTipoMotivoAnu", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarTipoMotivoAnus(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoMotivoAnu pTipoMotivoAnu = new TipoMotivoAnu();
                        pTipoMotivoAnu = ConsultarTipoMotivoAnun(pId, vUsuario);

                        DbParameter ptipo_motivo = cmdTransaccionFactory.CreateParameter();
                        ptipo_motivo.ParameterName = "p_tipo_motivo";
                        ptipo_motivo.Value = pTipoMotivoAnu.tipo_motivo;
                        ptipo_motivo.Direction = ParameterDirection.Input;
                        ptipo_motivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_motivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_MOTANULA_ELI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoMotivoAnuData", "EliminarTipoMotivoAnu", ex);
                    }
                }
            }
        }


        public TipoMotivoAnu ConsultarTipoMotivoAnun(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            TipoMotivoAnu entidad = new TipoMotivoAnu();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPO_MOTIVO_ANULACION WHERE TIPO_MOTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["TIPO_MOTIVO"] != DBNull.Value) entidad.tipo_motivo = Convert.ToInt32(resultado["TIPO_MOTIVO"]);
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
                        BOExcepcion.Throw("TipoMotivoAnuData", "ConsultarTipoMotivoAnu", ex);
                        return null;
                    }
                }
            }
        }


        public List<TipoMotivoAnu> ListarTipoMotivoAnus(TipoMotivoAnu pTipoMotivoAnu, Usuario vUsuario, String filtro)
        {
            DbDataReader resultado;
            List<TipoMotivoAnu> lstTipoMotivoAnu = new List<TipoMotivoAnu>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TIPO_MOTIVO_ANULACION " + filtro + " ORDER BY TIPO_MOTIVO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            TipoMotivoAnu entidad = new TipoMotivoAnu();
                            if (resultado["TIPO_MOTIVO"] != DBNull.Value) entidad.tipo_motivo = Convert.ToInt32(resultado["TIPO_MOTIVO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstTipoMotivoAnu.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoMotivoAnu;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoMotivoAnuData", "ListarTipoMotivoAnu", ex);
                        return null;
                    }
                }
            }
        }
 
 
    }
}