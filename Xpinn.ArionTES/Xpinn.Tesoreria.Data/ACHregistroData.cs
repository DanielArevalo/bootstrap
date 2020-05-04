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
    /// Objeto de acceso a datos para la tabla ACHregistro
    /// </summary>
    public class ACHregistroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ACHregistro
        /// </summary>
        public ACHregistroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla ACHregistro de la base de datos
        /// </summary>
        /// <param name="pACHregistro">Entidad ACHregistro</param>
        /// <returns>Entidad ACHregistro creada</returns>
        public ACHregistro CrearACHregistro(ACHregistro pACHregistro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "p_codigo";
                        pcodigo.Value = pACHregistro.codigo;
                        pcodigo.Direction = ParameterDirection.Output;
                        pcodigo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        if (pACHregistro.nombre == null)
                            pnombre.Value = DBNull.Value;
                        else
                            pnombre.Value = pACHregistro.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pACHregistro.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pACHregistro.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "p_separador";
                        if (pACHregistro.separador == null)
                            pseparador.Value = DBNull.Value;
                        else
                            pseparador.Value = pACHregistro.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHREGISTRO_CREA";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pACHregistro.codigo = Convert.ToInt64(pcodigo.Value);

                        return pACHregistro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHregistroData", "CrearACHregistro", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla ACHregistro de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ACHregistro modificada</returns>
        public ACHregistro ModificarACHregistro(ACHregistro pACHregistro, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigo = cmdTransaccionFactory.CreateParameter();
                        pcodigo.ParameterName = "p_codigo";
                        pcodigo.Value = pACHregistro.codigo;
                        pcodigo.Direction = ParameterDirection.Input;
                        pcodigo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pACHregistro.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pACHregistro.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pACHregistro.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pseparador = cmdTransaccionFactory.CreateParameter();
                        pseparador.ParameterName = "p_separador";
                        if (pACHregistro.separador == null)
                            pseparador.Value = DBNull.Value;
                        else
                            pseparador.Value = pACHregistro.separador;
                        pseparador.Direction = ParameterDirection.Input;
                        pseparador.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pseparador);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHREGISTRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pACHregistro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHregistroData", "ModificarACHregistro", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ACHregistro de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ACHregistro</param>
        public void EliminarCampoXACHregistro(Int64 pRegistro, Int64 pCampo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pregistro = cmdTransaccionFactory.CreateParameter();
                        pregistro.ParameterName = "p_registro";
                        pregistro.Value = pRegistro;
                        pregistro.Direction = ParameterDirection.Input;
                        pregistro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pregistro);

                        DbParameter pcampo = cmdTransaccionFactory.CreateParameter();
                        pcampo.ParameterName = "p_campo";
                        pcampo.Value = pCampo;
                        pcampo.Direction = ParameterDirection.Input;
                        pcampo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcampo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHREGIS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHregistroData", "EliminarCampoXACHregistro", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ACHregistro de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ACHregistroS</param>
        /// <returns>Entidad ACHregistro consultado</returns>
        public ACHregistro ConsultarACHregistro(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ACHregistro entidad = new ACHregistro();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ach_registro WHERE CODIGO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["SEPARADOR"] != DBNull.Value) entidad.separador = Convert.ToString(resultado["SEPARADOR"]);
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
                        BOExcepcion.Throw("ACHregistroData", "ConsultarACHregistro", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ACHregistro dados unos filtros
        /// </summary>
        /// <param name="pACHregistroS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ACHregistro obtenidos</returns>
        public List<ACHregistro> ListarACHregistro(ACHregistro pACHregistro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ACHregistro> lstACHregistro = new List<ACHregistro>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM ach_registro " + ObtenerFiltro(pACHregistro) + " ORDER BY 2 ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ACHregistro entidad = new ACHregistro();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt32(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["SEPARADOR"] != DBNull.Value) entidad.separador = Convert.ToString(resultado["SEPARADOR"]);
                            lstACHregistro.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstACHregistro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHregistroData", "ListarACHregistro", ex);
                        return null;
                    }
                }
            }
        }

        public List<ACHregistro> ListarACHregistro(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ACHregistro> lstACHregistro = new List<ACHregistro>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT a.* FROM ach_registro a INNER JOIN ach_reg_plant b ON a.codigo = b.registro WHERE b.plantilla = " + pId.ToString() + " ORDER BY b.registro ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ACHregistro entidad = new ACHregistro();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt32(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["SEPARADOR"] != DBNull.Value) entidad.separador = Convert.ToString(resultado["SEPARADOR"]);
                            lstACHregistro.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstACHregistro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHregistroData", "ListarACHregistro", ex);
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
                        string sql = "SELECT MAX(codigo) + 1 FROM ach_registro ";

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



        //GRABACION DEL DETALLE POR CADA REGISTRO
        public ACHdet_reg CrearModACHdet_reg(ACHdet_reg pACHdet_reg, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pregistro = cmdTransaccionFactory.CreateParameter();
                        pregistro.ParameterName = "p_registro";
                        pregistro.Value = pACHdet_reg.registro;
                        pregistro.Direction = ParameterDirection.Input;
                        pregistro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pregistro);

                        DbParameter pcampo = cmdTransaccionFactory.CreateParameter();
                        pcampo.ParameterName = "p_campo";
                        pcampo.Value = pACHdet_reg.campo;
                        pcampo.Direction = ParameterDirection.Input;
                        pcampo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcampo);

                        DbParameter porden = cmdTransaccionFactory.CreateParameter();
                        porden.ParameterName = "p_orden";
                        porden.Value = pACHdet_reg.orden;
                        porden.Direction = ParameterDirection.Input;
                        porden.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(porden);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_ACHDETREG_CREMOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pACHdet_reg;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ACHregistroData", "CrearModACHdet_reg", ex);
                        return null;
                    }
                }
            }
        }



    }
}