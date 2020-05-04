using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla USUARIOS
    /// </summary>
    public class TipoTasaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla TIPO_TASA
        /// </summary>
        public TipoTasaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla TIPO_TASA de la base de datos
        /// </summary>
        /// <param name="pUsuario">Entidad tipo tasa</param>
        /// <returns>Entidad tipo_tasa creada</returns>
        public TipoTasa CrearTipoTasa(TipoTasa pTipoTasa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        p_cod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        p_cod_tipo_tasa.Value = 0;
                        p_cod_tipo_tasa.Direction = ParameterDirection.InputOutput;

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        p_nombre.Value = pTipoTasa.nombre;

                        DbParameter p_efectiva_nomina = cmdTransaccionFactory.CreateParameter();
                        p_efectiva_nomina.ParameterName = "p_efectiva_nomina";
                        p_efectiva_nomina.Value = pTipoTasa.efectiva_nomina;

                        DbParameter p_cod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_cod_periodicidad.ParameterName = "p_cod_periodicidad";
                        p_cod_periodicidad.Value = pTipoTasa.cod_periodicidad;

                        DbParameter p_modalidad = cmdTransaccionFactory.CreateParameter();
                        p_modalidad.ParameterName = "p_modalidad";
                        p_modalidad.Value = pTipoTasa.modalidad;

                        DbParameter p_cod_periodicidad_cap = cmdTransaccionFactory.CreateParameter();
                        p_cod_periodicidad_cap.ParameterName = "p_cod_periodicidad_cap";
                        p_cod_periodicidad_cap.Value = pTipoTasa.cod_periodicidad_cap;

                        cmdTransaccionFactory.Parameters.Add(p_cod_tipo_tasa);
                        cmdTransaccionFactory.Parameters.Add(p_nombre);
                        cmdTransaccionFactory.Parameters.Add(p_efectiva_nomina);
                        cmdTransaccionFactory.Parameters.Add(p_cod_periodicidad);
                        cmdTransaccionFactory.Parameters.Add(p_modalidad);
                        cmdTransaccionFactory.Parameters.Add(p_cod_periodicidad_cap);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TIPOTASA_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoTasa, "TIPO_TASA", vUsuario, Accion.Crear.ToString()); //REGISTRO DE AUDITORIA

                        pTipoTasa.cod_tipo_tasa = Convert.ToInt64(p_cod_tipo_tasa.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoTasa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTasaData", "CrearTipoTasa", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla TIPO_TASA de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad tipotasa modificada</returns>
        public TipoTasa ModificarTipoTasa(TipoTasa pTipoTasa, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        p_cod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        p_cod_tipo_tasa.Value = 0;
                        p_cod_tipo_tasa.Direction = ParameterDirection.InputOutput;

                        DbParameter p_nombre = cmdTransaccionFactory.CreateParameter();
                        p_nombre.ParameterName = "p_nombre";
                        p_nombre.Value = pTipoTasa.nombre;

                        DbParameter p_efectiva_nomina = cmdTransaccionFactory.CreateParameter();
                        p_efectiva_nomina.ParameterName = "p_efectiva_nomina";
                        p_efectiva_nomina.Value = pTipoTasa.efectiva_nomina;

                        DbParameter p_cod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_cod_periodicidad.ParameterName = "p_cod_periodicidad";
                        p_cod_periodicidad.Value = pTipoTasa.cod_periodicidad;

                        DbParameter p_modalidad = cmdTransaccionFactory.CreateParameter();
                        p_modalidad.ParameterName = "p_modalidad";
                        p_modalidad.Value = pTipoTasa.modalidad;

                        DbParameter p_cod_periodicidad_cap = cmdTransaccionFactory.CreateParameter();
                        p_cod_periodicidad_cap.ParameterName = "p_cod_periodicidad_cap";
                        p_cod_periodicidad_cap.Value = pTipoTasa.cod_periodicidad_cap;

                        cmdTransaccionFactory.Parameters.Add(p_cod_tipo_tasa);
                        cmdTransaccionFactory.Parameters.Add(p_nombre);
                        cmdTransaccionFactory.Parameters.Add(p_efectiva_nomina);
                        cmdTransaccionFactory.Parameters.Add(p_cod_periodicidad);
                        cmdTransaccionFactory.Parameters.Add(p_modalidad);
                        cmdTransaccionFactory.Parameters.Add(p_cod_periodicidad_cap);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TIPOTASA_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoTasa, "TIPOTASA", vUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                        return pTipoTasa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTasaData", "ModificarTipoTasa", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla TIPOTASA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de TIPOTASA</param>
        public void EliminarTipoTasa(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        TipoTasa pTipoTasa = new TipoTasa();

                        DbParameter p_cod_tipo_tasa = cmdTransaccionFactory.CreateParameter();
                        p_cod_tipo_tasa.ParameterName = "p_cod_tipo_tasa";
                        p_cod_tipo_tasa.Value = pId;

                        cmdTransaccionFactory.Parameters.Add(p_cod_tipo_tasa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TIPOTASA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pTipoTasa, "TIPOTASA", vUsuario, Accion.Eliminar.ToString()); //REGISTRO DE AUDITORIA
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioData", "EliminarUsuario", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla TIPOTASA de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla TIPOTASA</param>
        /// <returns>Entidad Usuario consultado</returns>
        public TipoTasa ConsultarTipoTasa(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            TipoTasa entidad = new TipoTasa();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT tipotasa.*" +
                                     " FROM tipotasa " +
                                     " WHERE cod_tipo_tasa = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt64(resultado["COD_TIPO_TASA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["EFECTIVA_NOMINA"] != DBNull.Value) entidad.efectiva_nomina = Convert.ToString(resultado["EFECTIVA_NOMINA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["COD_PERIODICIDAD_CAP"] != DBNull.Value) entidad.cod_periodicidad_cap = Convert.ToInt64(resultado["COD_PERIODICIDAD_CAP"]);
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
                        BOExcepcion.Throw("TipoTasaData", "ConsultarTipoTasa", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla TIPO TASA dados unos filtros
        /// </summary>
        /// <param name="pUSUARIOS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos de tasa obtenidos</returns>
        public List<TipoTasa> ListarTipoTasa(TipoTasa pTipoTasa, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<TipoTasa> lstTipoTasa = new List<TipoTasa>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  TIPOTASA " + ObtenerFiltro(pTipoTasa);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            TipoTasa entidad = new TipoTasa();

                            if (resultado["COD_TIPO_TASA"] != DBNull.Value) entidad.cod_tipo_tasa = Convert.ToInt64(resultado["COD_TIPO_TASA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["EFECTIVA_NOMINA"] != DBNull.Value) entidad.efectiva_nomina = Convert.ToString(resultado["EFECTIVA_NOMINA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt64(resultado["COD_PERIODICIDAD"]);
                            if (resultado["MODALIDAD"] != DBNull.Value) entidad.modalidad = Convert.ToString(resultado["MODALIDAD"]);
                            if (resultado["COD_PERIODICIDAD_CAP"] != DBNull.Value) entidad.cod_periodicidad_cap = Convert.ToInt64(resultado["COD_PERIODICIDAD_CAP"]);

                            lstTipoTasa.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTipoTasa;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TipoTasaData", "ListarTipoTasa", ex);
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
            TipoTasa entidad = new TipoTasa();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT MAX(cod_tipo_tasa) + 1 FROM  TIPOTASA ";

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
                        BOExcepcion.Throw("TipoTasaData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }


    }

}