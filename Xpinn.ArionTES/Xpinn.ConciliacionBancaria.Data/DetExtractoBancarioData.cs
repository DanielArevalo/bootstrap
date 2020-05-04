using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.ConciliacionBancaria.Entities;

namespace Xpinn.ConciliacionBancaria.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla DetExtractoBancarioS
    /// </summary>
    public class DetExtractoBancarioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla DetExtractoBancarioS
        /// </summary>
        public DetExtractoBancarioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla DetExtractoBancarioS de la base de datos
        /// </summary>
        /// <param name="pDetExtractoBancario">Entidad DetExtractoBancario</param>
        /// <returns>Entidad DetExtractoBancario creada</returns>
        public DetExtractoBancario CrearDetExtractoBancario(DetExtractoBancario pDetExtractoBancario, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pDetExtractoBancario.iddetalle;
                        piddetalle.Direction = ParameterDirection.Output;
                        piddetalle.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter pidextracto = cmdTransaccionFactory.CreateParameter();
                        pidextracto.ParameterName = "p_idextracto";
                        pidextracto.Value = pDetExtractoBancario.idextracto;
                        pidextracto.Direction = ParameterDirection.Input;
                        pidextracto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidextracto);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pDetExtractoBancario.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        if (pDetExtractoBancario.cod_concepto != null) pcod_concepto.Value = pDetExtractoBancario.cod_concepto; else pcod_concepto.Value = DBNull.Value;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter ptipo_movimiento = cmdTransaccionFactory.CreateParameter();
                        ptipo_movimiento.ParameterName = "p_tipo_movimiento";
                        ptipo_movimiento.Value = pDetExtractoBancario.tipo_movimiento;
                        ptipo_movimiento.Direction = ParameterDirection.Input;
                        ptipo_movimiento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_movimiento);

                        DbParameter pnum_documento = cmdTransaccionFactory.CreateParameter();
                        pnum_documento.ParameterName = "p_num_documento";
                        if (pDetExtractoBancario.num_documento != null) pnum_documento.Value = pDetExtractoBancario.num_documento; else pnum_documento.Value = DBNull.Value;
                        pnum_documento.Direction = ParameterDirection.Input;
                        pnum_documento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_documento);

                        DbParameter preferencia1 = cmdTransaccionFactory.CreateParameter();
                        preferencia1.ParameterName = "p_referencia1";
                        if (pDetExtractoBancario.referencia1 != null) preferencia1.Value = pDetExtractoBancario.referencia1; else preferencia1.Value = DBNull.Value;
                        preferencia1.Direction = ParameterDirection.Input;
                        preferencia1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preferencia1);

                        DbParameter preferencia2 = cmdTransaccionFactory.CreateParameter();
                        preferencia2.ParameterName = "p_referencia2";
                        if (pDetExtractoBancario.referencia2 != null) preferencia2.Value = pDetExtractoBancario.referencia2; else preferencia2.Value = DBNull.Value;
                        preferencia2.Direction = ParameterDirection.Input;
                        preferencia2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preferencia2);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pDetExtractoBancario.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_DEXTRACBAN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pDetExtractoBancario.iddetalle = Convert.ToInt64(piddetalle.Value);

                        return pDetExtractoBancario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetExtractoBancarioData", "CrearDetExtractoBancario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla DetExtractoBancarioS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad DetExtractoBancario modificada</returns>
        public DetExtractoBancario ModificarDetExtractoBancario(DetExtractoBancario pDetExtractoBancario, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pDetExtractoBancario.iddetalle;
                        piddetalle.Direction = ParameterDirection.Input;
                        piddetalle.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter pidextracto = cmdTransaccionFactory.CreateParameter();
                        pidextracto.ParameterName = "p_idextracto";
                        pidextracto.Value = pDetExtractoBancario.idextracto;
                        pidextracto.Direction = ParameterDirection.Input;
                        pidextracto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidextracto);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pDetExtractoBancario.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcod_concepto = cmdTransaccionFactory.CreateParameter();
                        pcod_concepto.ParameterName = "p_cod_concepto";
                        if (pDetExtractoBancario.cod_concepto != null) pcod_concepto.Value = pDetExtractoBancario.cod_concepto; else pcod_concepto.Value = DBNull.Value;
                        pcod_concepto.Direction = ParameterDirection.Input;
                        pcod_concepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_concepto);

                        DbParameter ptipo_movimiento = cmdTransaccionFactory.CreateParameter();
                        ptipo_movimiento.ParameterName = "p_tipo_movimiento";
                        ptipo_movimiento.Value = pDetExtractoBancario.tipo_movimiento;
                        ptipo_movimiento.Direction = ParameterDirection.Input;
                        ptipo_movimiento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(ptipo_movimiento);

                        DbParameter pnum_documento = cmdTransaccionFactory.CreateParameter();
                        pnum_documento.ParameterName = "p_num_documento";
                        if (pDetExtractoBancario.num_documento != null) pnum_documento.Value = pDetExtractoBancario.num_documento; else pnum_documento.Value = DBNull.Value;
                        pnum_documento.Direction = ParameterDirection.Input;
                        pnum_documento.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_documento);

                        DbParameter preferencia1 = cmdTransaccionFactory.CreateParameter();
                        preferencia1.ParameterName = "p_referencia1";
                        if (pDetExtractoBancario.referencia1 != null) preferencia1.Value = pDetExtractoBancario.referencia1; else preferencia1.Value = DBNull.Value;
                        preferencia1.Direction = ParameterDirection.Input;
                        preferencia1.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preferencia1);

                        DbParameter preferencia2 = cmdTransaccionFactory.CreateParameter();
                        preferencia2.ParameterName = "p_referencia2";
                        if (pDetExtractoBancario.referencia2 != null) preferencia2.Value = pDetExtractoBancario.referencia2; else preferencia2.Value = DBNull.Value;
                        preferencia2.Direction = ParameterDirection.Input;
                        preferencia2.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preferencia2);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pDetExtractoBancario.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_DEXTRACBAN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pDetExtractoBancario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetExtractoBancarioData", "ModificarDetExtractoBancario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla DetExtractoBancarioS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de DetExtractoBancarioS</param>
        public void EliminarDetExtractoBancario(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM det_extracto_bancario WHERE iddetalle = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetExtractoBancarioData", "EliminarDetExtractoBancario", ex);
                    }
                }
            }
        }

        
        /// <summary>
        /// Obtiene una lista de Entidades de la tabla DetExtractoBancarioS dados unos filtros
        /// </summary>
        /// <param name="pDetExtractoBancarioS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de DetExtractoBancario obtenidos</returns>
        public List<DetExtractoBancario> ListarDetExtractoBancario(Int32 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DetExtractoBancario> lstDetExtractoBancario = new List<DetExtractoBancario>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Det_Extracto_Bancario where IDEXTRACTO = " + pId.ToString() + " ORDER BY IDDETALLE ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DetExtractoBancario entidad = new DetExtractoBancario();
                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt32(resultado["IDDETALLE"]);
                            if (resultado["IDEXTRACTO"] != DBNull.Value) entidad.idextracto = Convert.ToInt32(resultado["IDEXTRACTO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_CONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToString(resultado["COD_CONCEPTO"]);
                            if (resultado["TIPO_MOVIMIENTO"] != DBNull.Value) entidad.tipo_movimiento = Convert.ToString(resultado["TIPO_MOVIMIENTO"]);
                            if (resultado["NUM_DOCUMENTO"] != DBNull.Value) entidad.num_documento = Convert.ToString(resultado["NUM_DOCUMENTO"]);
                            if (resultado["REFERENCIA1"] != DBNull.Value) entidad.referencia1 = Convert.ToString(resultado["REFERENCIA1"]);
                            if (resultado["REFERENCIA2"] != DBNull.Value) entidad.referencia2 = Convert.ToString(resultado["REFERENCIA2"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            lstDetExtractoBancario.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDetExtractoBancario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetExtractoBancarioData", "ListarDetExtractoBancario", ex);
                        return null;
                    }
                }
            }
        }


        public List<DetExtractoBancario> ListarConceptos_Bancarios(Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DetExtractoBancario> lstDetExtractoBancario = new List<DetExtractoBancario>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select cod_concepto,descripcion From concepto_bancario order by cod_concepto";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DetExtractoBancario entidad = new DetExtractoBancario();                            
                            if (resultado["COD_CONCEPTO"] != DBNull.Value) entidad.cod_concepto = Convert.ToString(resultado["COD_CONCEPTO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            lstDetExtractoBancario.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDetExtractoBancario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetExtractoBancarioData", "ListarConceptos_Bancarios", ex);
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
                        string sql = "SELECT MAX(iddetalle) + 1 FROM Det_Extracto_Bancario ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());

                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetExtractoBancarioData", "ObtenerSiguienteCodigo", ex);
                        return 1;
                    }
                }
            }
        }

    }
}