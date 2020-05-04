using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla ACHplantilla
    /// </summary>
    public class ACHplantillaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ACHplantilla
        /// </summary>
        public ACHplantillaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla ACHplantilla de la base de datos
        /// </summary>
        /// <param name="pACHplantilla">Entidad ACHplantilla</param>
        /// <returns>Entidad ACHplantilla creada</returns>
        public ACHplantilla CrearACHplantilla(ACHplantilla pACHplantilla, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "p_codigo";
                        pcodigo.Value = pACHplantilla.codigo;
                        pcodigo.Direction = ParameterDirection.Output;
                        pcodigo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pACHplantilla.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pACHplantilla.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pACHplantilla.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pactivo = cmdTransaccionFactory.CreateParameter();
                        pactivo.ParameterName = "p_activo";
                        pactivo.Value = pACHplantilla.activo;
                        pactivo.Direction = ParameterDirection.Input;
                        pactivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pactivo);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pACHplantilla.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHPLANTI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pACHplantilla.codigo = Convert.ToInt32(pcodigo.Value);

                        return pACHplantilla;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHplantillaData", "CrearACHplantilla", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla ACHplantilla de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ACHplantilla modificada</returns>
        public ACHplantilla ModificarACHplantilla(ACHplantilla pACHplantilla, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "p_codigo";
                        pcodigo.Value = pACHplantilla.codigo;
                        pcodigo.Direction = ParameterDirection.Input;
                        pcodigo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pACHplantilla.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        pdescripcion.Value = pACHplantilla.descripcion;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pACHplantilla.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pactivo = cmdTransaccionFactory.CreateParameter();
                        pactivo.ParameterName = "p_activo";
                        pactivo.Value = pACHplantilla.activo;
                        pactivo.Direction = ParameterDirection.Input;
                        pactivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pactivo);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pACHplantilla.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHPLANTI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pACHplantilla;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHplantillaData", "ModificarACHplantilla", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ACHplantilla de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ACHplantilla</param>
        public void EliminarACHplantilla(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ACHplantilla pACHplantilla = new ACHplantilla();
                        pACHplantilla = ConsultarACHplantilla(pId, vUsuario);

                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "p_codigo";
                        pcodigo.Value = pACHplantilla.codigo;
                        pcodigo.Direction = ParameterDirection.Input;
                        pcodigo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHPLANTI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHplantillaData", "EliminarACHplantilla", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ACHplantilla de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ACHplantillaS</param>
        /// <returns>Entidad ACHplantilla consultado</returns>
        public ACHplantilla ConsultarACHplantilla(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ACHplantilla entidad = new ACHplantilla();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ach_plantilla WHERE CODIGO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["ACTIVO"] != DBNull.Value) entidad.activo = Convert.ToInt32(resultado["ACTIVO"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["COD_BANCO"]);
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
                        BOExcepcion.Throw("ACHplantillaData", "ConsultarACHplantilla", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ACHplantilla dados unos filtros
        /// </summary>
        /// <param name="pACHplantillaS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ACHplantilla obtenidos</returns>
        public List<ACHplantilla> ListarACHplantilla(ACHplantilla pACHplantilla, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ACHplantilla> lstACHplantilla = new List<ACHplantilla>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ach_plantilla " + ObtenerFiltro(pACHplantilla) + " ORDER BY CODIGO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ACHplantilla entidad = new ACHplantilla();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["ACTIVO"] != DBNull.Value) entidad.activo = Convert.ToInt32(resultado["ACTIVO"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["COD_BANCO"]);
                            lstACHplantilla.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstACHplantilla;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHplantillaData", "ListarACHplantilla", ex);
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
                        string sql = "SELECT MAX(codigo) + 1 FROM ach_plantilla ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());

                        return resultado;
                    }
                    catch
                    {
                        return 1;
                    }
                }
            }
        }


        public ACHregistro CrearACHRegistro(ACHregistro pACHRegistro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pplantilla = cmdTransaccionFactory.CreateParameter();
                        pplantilla.ParameterName = "p_plantilla";
                        pplantilla.Value = pACHRegistro.plantilla;
                        pplantilla.Direction = ParameterDirection.Input;
                        pplantilla.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pplantilla);

                        DbParameter pregistro = cmdTransaccionFactory.CreateParameter();
                        pregistro.ParameterName = "p_registro";
                        pregistro.Value = pACHRegistro.codigo;
                        pregistro.Direction = ParameterDirection.Input;
                        pregistro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pregistro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHREGPLA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pACHRegistro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHRegistroData", "CrearACHRegistro", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarACH_PLANTILLA(Int64 pPlantilla, Int32 pRegistro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pplantilla = cmdTransaccionFactory.CreateParameter();
                        pplantilla.ParameterName = "p_plantilla";
                        pplantilla.Value = pPlantilla;
                        pplantilla.Direction = ParameterDirection.Input;
                        pplantilla.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplantilla);

                        DbParameter pregistro = cmdTransaccionFactory.CreateParameter();
                        pregistro.ParameterName = "p_registro";
                        pregistro.Value = pRegistro;
                        pregistro.Direction = ParameterDirection.Input;
                        pregistro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pregistro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHREGPLA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHRegistroData", "EliminarACH_PLANTILLA", ex);
                    }
                }
            }
        }


        public ACHregistro ConsultarRegisPlantilla(Int64 pPlantilla,Int64 pRegistro,Usuario vUsuario)
        {
            DbDataReader resultado;
            ACHregistro entidad = new ACHregistro();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ACH_REG_PLANT WHERE REGISTRO = " + pRegistro.ToString() + " AND PLANTILLA = " + pPlantilla.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["PLANTILLA"] != DBNull.Value) entidad.plantilla = Convert.ToInt32(resultado["PLANTILLA"]);
                            if (resultado["REGISTRO"] != DBNull.Value) entidad.codigo = Convert.ToInt32(resultado["REGISTRO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHRegistroData", "ConsultarRegisPlantilla", ex);
                        return null;
                    }
                }
            }
        }


    }
}