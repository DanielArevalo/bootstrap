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
    /// Objeto de acceso a datos para la tabla ExtractoBancarioS
    /// </summary>
    public class ExtractoBancarioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla ExtractoBancarioS
        /// </summary>
        public ExtractoBancarioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Crea un registro en la tabla ExtractoBancarioS de la base de datos
        /// </summary>
        /// <param name="pExtractoBancario">Entidad ExtractoBancario</param>
        /// <returns>Entidad ExtractoBancario creada</returns>
        public ExtractoBancario CrearExtractoBancario(ExtractoBancario pExtractoBancario, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidextracto = cmdTransaccionFactory.CreateParameter();
                        pidextracto.ParameterName = "p_idextracto";
                        pidextracto.Value = pExtractoBancario.idextracto;
                        pidextracto.Direction = ParameterDirection.Output;
                        pidextracto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidextracto);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pExtractoBancario.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pExtractoBancario.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pExtractoBancario.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter psaldo_anterior = cmdTransaccionFactory.CreateParameter();
                        psaldo_anterior.ParameterName = "p_saldo_anterior";
                        psaldo_anterior.Value = pExtractoBancario.saldo_anterior;
                        psaldo_anterior.Direction = ParameterDirection.Input;
                        psaldo_anterior.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_anterior);

                        DbParameter pdebitos = cmdTransaccionFactory.CreateParameter();
                        pdebitos.ParameterName = "p_debitos";
                        pdebitos.Value = pExtractoBancario.debitos;
                        pdebitos.Direction = ParameterDirection.Input;
                        pdebitos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdebitos);

                        DbParameter pcreditos = cmdTransaccionFactory.CreateParameter();
                        pcreditos.ParameterName = "p_creditos";
                        pcreditos.Value = pExtractoBancario.creditos;
                        pcreditos.Direction = ParameterDirection.Input;
                        pcreditos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcreditos);

                        DbParameter pperiodo = cmdTransaccionFactory.CreateParameter();
                        pperiodo.ParameterName = "p_periodo";
                        pperiodo.Value = pExtractoBancario.periodo;
                        pperiodo.Direction = ParameterDirection.Input;
                        pperiodo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pperiodo);

                        DbParameter pmes = cmdTransaccionFactory.CreateParameter();
                        pmes.ParameterName = "p_mes";
                        pmes.Value = pExtractoBancario.mes;
                        pmes.Direction = ParameterDirection.Input;
                        pmes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmes);

                        DbParameter pdia = cmdTransaccionFactory.CreateParameter();
                        pdia.ParameterName = "p_dia";
                        if (pExtractoBancario.dia != 0) pdia.Value = pExtractoBancario.dia; else pdia.Value = DBNull.Value;
                        pdia.Direction = ParameterDirection.Input;
                        pdia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdia);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pExtractoBancario.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcodusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pcodusuariocreacion.ParameterName = "p_codusuariocreacion";
                        pcodusuariocreacion.Value = vUsuario.codusuario;
                        pcodusuariocreacion.Direction = ParameterDirection.Input;
                        pcodusuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuariocreacion);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pExtractoBancario.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_EXTRACBANC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pExtractoBancario.idextracto = Convert.ToInt64(pidextracto.Value);
                        return pExtractoBancario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoBancarioData", "CrearExtractoBancario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Modifica un registro en la tabla ExtractoBancarioS de la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad $Objeto</param>
        /// <returns>Entidad ExtractoBancario modificada</returns>
        public ExtractoBancario ModificarExtractoBancario(ExtractoBancario pExtractoBancario, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidextracto = cmdTransaccionFactory.CreateParameter();
                        pidextracto.ParameterName = "p_idextracto";
                        pidextracto.Value = pExtractoBancario.idextracto;
                        pidextracto.Direction = ParameterDirection.Input;
                        pidextracto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidextracto);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pExtractoBancario.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pExtractoBancario.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pnumero_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuenta.ParameterName = "p_numero_cuenta";
                        pnumero_cuenta.Value = pExtractoBancario.numero_cuenta;
                        pnumero_cuenta.Direction = ParameterDirection.Input;
                        pnumero_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuenta);

                        DbParameter psaldo_anterior = cmdTransaccionFactory.CreateParameter();
                        psaldo_anterior.ParameterName = "p_saldo_anterior";
                        psaldo_anterior.Value = pExtractoBancario.saldo_anterior;
                        psaldo_anterior.Direction = ParameterDirection.Input;
                        psaldo_anterior.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_anterior);

                        DbParameter pdebitos = cmdTransaccionFactory.CreateParameter();
                        pdebitos.ParameterName = "p_debitos";
                        pdebitos.Value = pExtractoBancario.debitos;
                        pdebitos.Direction = ParameterDirection.Input;
                        pdebitos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pdebitos);

                        DbParameter pcreditos = cmdTransaccionFactory.CreateParameter();
                        pcreditos.ParameterName = "p_creditos";
                        pcreditos.Value = pExtractoBancario.creditos;
                        pcreditos.Direction = ParameterDirection.Input;
                        pcreditos.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pcreditos);

                        DbParameter pperiodo = cmdTransaccionFactory.CreateParameter();
                        pperiodo.ParameterName = "p_periodo";
                        pperiodo.Value = pExtractoBancario.periodo;
                        pperiodo.Direction = ParameterDirection.Input;
                        pperiodo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pperiodo);

                        DbParameter pmes = cmdTransaccionFactory.CreateParameter();
                        pmes.ParameterName = "p_mes";
                        pmes.Value = pExtractoBancario.mes;
                        pmes.Direction = ParameterDirection.Input;
                        pmes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pmes);

                        DbParameter pdia = cmdTransaccionFactory.CreateParameter();
                        pdia.ParameterName = "p_dia";
                        if (pExtractoBancario.dia != 0) pdia.Value = pExtractoBancario.dia; else pdia.Value = DBNull.Value;
                        pdia.Direction = ParameterDirection.Input;
                        pdia.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdia);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pExtractoBancario.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcodusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pcodusuariocreacion.ParameterName = "p_codusuariocreacion";
                        pcodusuariocreacion.Value = pUsuario.codusuario;
                        pcodusuariocreacion.Direction = ParameterDirection.Input;
                        pcodusuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuariocreacion);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pExtractoBancario.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_EXTRACBANC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pExtractoBancario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoBancarioData", "ModificarExtractoBancario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Elimina un registro en la tabla ExtractoBancarioS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de ExtractoBancarioS</param>
        public void EliminarExtractoBancario(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidextracto = cmdTransaccionFactory.CreateParameter();
                        pidextracto.ParameterName = "p_idextracto";
                        pidextracto.Value = pId;
                        pidextracto.Direction = ParameterDirection.Input;
                        pidextracto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidextracto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_EXTRACBANC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoBancarioData", "EliminarExtractoBancario", ex);
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene un registro en la tabla ExtractoBancarioS de la base de datos
        /// </summary>
        /// <param name="pId">identificador de regitro en la tabla ExtractoBancarioS</param>
        /// <returns>Entidad ExtractoBancario consultado</returns>
        public ExtractoBancario ConsultarExtractoBancario(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ExtractoBancario entidad = new ExtractoBancario();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Extracto_Bancario WHERE IDEXTRACTO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDEXTRACTO"] != DBNull.Value) entidad.idextracto = Convert.ToInt32(resultado["IDEXTRACTO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["SALDO_ANTERIOR"] != DBNull.Value) entidad.saldo_anterior = Convert.ToDecimal(resultado["SALDO_ANTERIOR"]);
                            if (resultado["DEBITOS"] != DBNull.Value) entidad.debitos = Convert.ToDecimal(resultado["DEBITOS"]);
                            if (resultado["CREDITOS"] != DBNull.Value) entidad.creditos = Convert.ToDecimal(resultado["CREDITOS"]);
                            if (resultado["PERIODO"] != DBNull.Value) entidad.periodo = Convert.ToString(resultado["PERIODO"]);
                            if (resultado["MES"] != DBNull.Value) entidad.mes = Convert.ToInt32(resultado["MES"]);
                            if (resultado["DIA"] != DBNull.Value) entidad.dia = Convert.ToInt32(resultado["DIA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["CODUSUARIOCREACION"] != DBNull.Value) entidad.codusuariocreacion = Convert.ToInt32(resultado["CODUSUARIOCREACION"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                        }
                       
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoBancarioData", "ConsultarExtractoBancario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla ExtractoBancarioS dados unos filtros
        /// </summary>
        /// <param name="pExtractoBancarioS">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ExtractoBancario obtenidos</returns>
        public List<ExtractoBancario> ListarExtractoBancario(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ExtractoBancario> lstExtractoBancario = new List<ExtractoBancario>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT e.idextracto,e.fecha,e.cod_banco,b.nombrebanco,e.numero_cuenta,e.saldo_anterior,e.periodo, "
                                     +"case e.estado when 0 then 'Pendiente' when 1 then 'Conciliado' when 2 then 'Anulado' end as nomestado "
                                     +"FROM Extracto_Bancario e left join bancos b on b.cod_banco = e.cod_banco where 1 = 1 " +
                                     filtro + " ORDER BY e.IDEXTRACTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ExtractoBancario entidad = new ExtractoBancario();
                            if (resultado["IDEXTRACTO"] != DBNull.Value) entidad.idextracto = Convert.ToInt64(resultado["IDEXTRACTO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombanco = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["NUMERO_CUENTA"] != DBNull.Value) entidad.numero_cuenta = Convert.ToString(resultado["NUMERO_CUENTA"]);
                            if (resultado["SALDO_ANTERIOR"] != DBNull.Value) entidad.saldo_anterior = Convert.ToDecimal(resultado["SALDO_ANTERIOR"]);
                            if (resultado["PERIODO"] != DBNull.Value) entidad.periodo = Convert.ToString(resultado["PERIODO"]);
                            if (resultado["NOMESTADO"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["NOMESTADO"]);
                            lstExtractoBancario.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstExtractoBancario;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoBancarioData", "ListarExtractoBancario", ex);
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
                        string sql = "SELECT MAX(idextracto) + 1 FROM Extracto_Bancario ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());

                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExtractoBancarioData", "ObtenerSiguienteCodigo", ex);
                        return 1;
                    }
                }
            }
        }


        
    }
}