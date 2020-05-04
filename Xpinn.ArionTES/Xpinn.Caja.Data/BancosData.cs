using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Data
{
    public class BancosData:GlobalData 
    {
         protected ConnectionDataBase dbConnectionFactory;
        
        /// <summary>
        /// Constructor del objeto de acceso a datos para Programa
        /// </summary>
        public BancosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Obtiene la lista de Bancoses dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Bancoses obtenidas</returns>
        public List<Bancos> ListarBancos(Bancos pEntidad, Usuario pUsuario)
        {
            return ListarBancos(pEntidad, 0, pUsuario);
        } 

        public List<Bancos> ListarBancos(Bancos pEntidad, int pOrden, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Bancos> lstBancos = new List<Bancos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM BANCOS " + ObtenerFiltro(pEntidad);
                        if (pOrden == 1)
                            sql += " Order By cod_banco asc";
                        else
                            sql += " Order By nombrebanco asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Bancos entidad = new Bancos();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_banco"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["cod_banco"]);
                            if (resultado["nombrebanco"] != DBNull.Value) entidad.nombrebanco= Convert.ToString(resultado["nombrebanco"]);
                            if (resultado["cobra_comision"] != DBNull.Value) entidad.cobra_comision = Convert.ToInt64(resultado["cobra_comision"]);
                            lstBancos.Add(entidad);
                        }

                        return lstBancos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ListarBancos", ex);
                        return null;
                    }
                }
            }
        }

        public List<Bancos> ListarBancosegre(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Bancos> lstBancos = new List<Bancos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select  b.cod_banco, b.nombrebanco From bancos b inner join cuenta_bancaria cu on cu.cod_banco = b.cod_banco Left Join chequera ch on ch.idctabancaria = cu.idctabancaria                                                                                 
                                        group by b.cod_banco,b.nombrebanco";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Bancos entidad = new Bancos();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_banco"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["cod_banco"]);
                            if (resultado["nombrebanco"] != DBNull.Value) entidad.nombrebanco= Convert.ToString(resultado["nombrebanco"]);
                        
                            lstBancos.Add(entidad);
                        }

                        return lstBancos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ListarBancos", ex);
                        return null;
                    }
                }
            }
        }

        public List<Bancos> ListarCuentaBancaria_Bancos(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Bancos> lstBancos = new List<Bancos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CU.IDCTABANCARIA, CU.NUM_CUENTA || '-' || B.NOMBREBANCO as NUM_CUENTA "
                        + "FROM BANCOS B INNER JOIN CUENTA_BANCARIA CU ON CU.COD_BANCO = B.COD_BANCO "
                        + "where CU.ESTADO = '1' order by B.NOMBREBANCO,CU.NUM_CUENTA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Bancos entidad = new Bancos();
                            //Asociar todos los valores a la entidad
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.ctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["NUM_CUENTA"]);

                            lstBancos.Add(entidad);
                        }

                        return lstBancos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ListarBancos", ex);
                        return null;
                    }
                }
            }
        }

        public List<Bancos> ListarBancosegrecuentas(string codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Bancos> lstBancos = new List<Bancos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {   
                        string sql = "";
                        if (codigo != null)
                            sql = @" select cu.idctabancaria,cu.num_cuenta, b.cod_banco, b.nombrebanco, ch.num_sig_che, ch.cheque_ini, ch.cheque_fin 
                                        from bancos b inner join cuenta_bancaria cu on cu.cod_banco = b.cod_banco Left Join chequera ch on ch.idctabancaria = cu.idctabancaria
                                        where b.cod_banco = " + codigo;
                        else
                            sql = @" select cu.num_cuenta, b.cod_banco, b.nombrebanco, ch.num_sig_che, ch.cheque_ini, ch.cheque_fin 
                                        from bancos b inner join cuenta_bancaria cu on cu.cod_banco = b.cod_banco Left Join chequera ch on ch.idctabancaria = cu.idctabancaria";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Bancos entidad = new Bancos();
                            //Asociar todos los valores a la entidad
                        if (resultado["idctabancaria"] != DBNull.Value) entidad.ctabancaria = Convert.ToInt32(resultado["idctabancaria"]);
                            if (resultado["num_cuenta"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["num_cuenta"]);
                            lstBancos.Add(entidad);
                        }

                        return lstBancos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ListarBancos", ex);
                        return null;
                    }
                }
            }
        }

        public string soporte(string codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Bancos> lstBancos = new List<Bancos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" Select ch.num_sig_che 
                                        From chequera ch inner join cuenta_bancaria cu on ch.idctabancaria = cu.idctabancaria 
                                        Where cu.num_cuenta = '" + codigo + "' And ch.estado = 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        string respuesta="";
                        if (resultado.Read())
                        {
                            Bancos entidad = new Bancos();
                            //Asociar todos los valores a la entidad
                            respuesta = Convert.ToString(resultado["num_sig_che"]);
                            lstBancos.Add(entidad);
                        }

                        return respuesta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ConsultaChequera", ex);
                        return null;
                    }
                }
            }
        }



        public string Ruta_Cheque(string codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Bancos> lstBancos = new List<Bancos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" Select ban.ruta_cheque
                                        From chequera ch inner join cuenta_bancaria cu on ch.idctabancaria = cu.idctabancaria 
                                        inner join Bancos ban on ban.COD_BANCO = cu.COD_BANCO
                                        Where cu.num_cuenta = '" + codigo + "' And ch.estado = 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        string respuesta = "";
                        if (resultado.Read())
                        {
                            Bancos entidad = new Bancos();
                            //Asociar todos los valores a la entidad
                            respuesta = Convert.ToString(resultado["ruta_cheque"]);
                            lstBancos.Add(entidad);
                        }

                        return respuesta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ConsultaChequera", ex);
                        return null;
                    }
                }
            }
        }


        public Xpinn.Tesoreria.Entities.Chequera ConsultaChequera(string codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Xpinn.Tesoreria.Entities.Chequera entidad = new Xpinn.Tesoreria.Entities.Chequera();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" Select ch.IDCHEQUERA,ch.cheque_ini,ch.cheque_fin,ch.num_sig_che 
                                        From chequera ch inner join cuenta_bancaria cu on ch.idctabancaria = cu.idctabancaria 
                                        Where cu.num_cuenta = '" + codigo.Trim() + "' And ch.estado = 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["IDCHEQUERA"] != DBNull.Value) entidad.idchequera = Convert.ToInt32(resultado["IDCHEQUERA"]);
                            if (resultado["cheque_ini"] != DBNull.Value) entidad.cheque_ini = Convert.ToInt32(resultado["cheque_ini"]);
                            if (resultado["cheque_fin"] != DBNull.Value) entidad.cheque_fin = Convert.ToInt32(resultado["cheque_fin"]);
                            if (resultado["num_sig_che"] != DBNull.Value) entidad.num_sig_che = Convert.ToInt32(resultado["num_sig_che"]);
                        }
                        else
                        {
                            return null;
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ListarBancos", ex);
                        return null;
                    }
                }
            }
        }

        public List<Bancos> ListarBancosEntidad(Bancos pEntidad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Bancos> lstBancos = new List<Bancos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM V_BANCOS_ENTIDAD " + ObtenerFiltro(pEntidad) + " Order By nombrebanco asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Bancos entidad = new Bancos();
                            //Asociar todos los valores a la entidad
                            if (resultado["cod_banco"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["cod_banco"]);
                            if (resultado["nombrebanco"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["nombrebanco"]);
                            lstBancos.Add(entidad);
                        }

                        return lstBancos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ListarBancos", ex);
                        return null;
                    }
                }
            }
        }
        
        public List<CuentaBancaria> ListarCuentaBancos(Int64 pCodBanco, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuentaBancaria> lstCuentaBancaria = new List<CuentaBancaria>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT * FROM CUENTA_BANCARIA WHERE COD_BANCO = " + pCodBanco + " Order By num_cuenta";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuentaBancaria cuentabancaria = new CuentaBancaria();
                            if (resultado["idctabancaria"] != DBNull.Value) cuentabancaria.IdCtaBancaria = Convert.ToInt64(resultado["idctabancaria"]);
                            if (resultado["cod_banco"] != DBNull.Value) cuentabancaria.cod_banco = Convert.ToInt64(resultado["cod_banco"]);
                            if (resultado["tipo_cuenta"] != DBNull.Value) cuentabancaria.tipo_cuenta = Convert.ToInt64(resultado["tipo_cuenta"]);
                            if (resultado["num_cuenta"] != DBNull.Value) cuentabancaria.num_cuenta = Convert.ToString(resultado["num_cuenta"]);
                            if (resultado["cod_cuenta"] != DBNull.Value) cuentabancaria.cod_cuenta = Convert.ToString(resultado["cod_cuenta"]);
                            if (resultado["estado"] != DBNull.Value) cuentabancaria.estado = Convert.ToString(resultado["estado"]);
                            lstCuentaBancaria.Add(cuentabancaria);
                        }

                        return lstCuentaBancaria;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ListarBancos", ex);
                        return null;
                    }
                }
            }
        }

        public int? ConsultarTipoCuenta(string numeroCuenta, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            int? tipoCuenta = null;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " SELECT tipo_cuenta FROM CUENTA_BANCARIA WHERE NUM_CUENTA = '" + numeroCuenta + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["tipo_cuenta"] != DBNull.Value) tipoCuenta = Convert.ToInt32(resultado["tipo_cuenta"]);
                        }

                        return tipoCuenta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ConsultarTipoCuenta", ex);
                        return null;
                    }
                }
            }
        }

        public Bancos ConsultarBancos(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Bancos entidad = new Bancos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM BANCOS" +
                                     " WHERE cod_banco = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["COD_BANCO"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["COBRA_COMISION"] != DBNull.Value) entidad.cobra_comision = Convert.ToInt64(resultado["COBRA_COMISION"]);
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
                        BOExcepcion.Throw("BancosData", "ConsultarBancos", ex);
                        return null;
                    }
                }
            }
        }

        public Bancos CrearBancos(Bancos pBancos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pBancos.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);
 
                        DbParameter pnombrebanco = cmdTransaccionFactory.CreateParameter();
                        pnombrebanco.ParameterName = "p_nombrebanco";
                        pnombrebanco.Value = pBancos.nombrebanco;
                        pnombrebanco.Direction = ParameterDirection.Input;
                        pnombrebanco.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombrebanco);
 
                        DbParameter pcobra_comision = cmdTransaccionFactory.CreateParameter();
                        pcobra_comision.ParameterName = "p_cobra_comision";
                        if (pBancos.cobra_comision == null)
                            pcobra_comision.Value = DBNull.Value;
                        else
                            pcobra_comision.Value = pBancos.cobra_comision;
                        pcobra_comision.Direction = ParameterDirection.Input;
                        pcobra_comision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_comision);
 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_BANCOS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pBancos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "CrearBancos", ex);
                        return null;
                    }
                }
            }
        }

        public Bancos ModificarBancos(Bancos pBancos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pBancos.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pnombrebanco = cmdTransaccionFactory.CreateParameter();
                        pnombrebanco.ParameterName = "p_nombrebanco";
                        pnombrebanco.Value = pBancos.nombrebanco;
                        pnombrebanco.Direction = ParameterDirection.Input;
                        pnombrebanco.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombrebanco);

                        DbParameter pcobra_comision = cmdTransaccionFactory.CreateParameter();
                        pcobra_comision.ParameterName = "p_cobra_comision";
                        if (pBancos.cobra_comision == null)
                            pcobra_comision.Value = DBNull.Value;
                        else
                            pcobra_comision.Value = pBancos.cobra_comision;
                        pcobra_comision.Direction = ParameterDirection.Input;
                        pcobra_comision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcobra_comision);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_BANCOS_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pBancos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "CrearBancos", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarBancos(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Bancos pBancos = new Bancos();
                        pBancos = ConsultarBancos(pId, vUsuario);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pBancos.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_CAJAFIN_BANCOS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);                        
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "EliminarConcepto", ex);
                    }
                }
            }
        }


        public string ConsultaBancoPersona(string codigo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
           // List<Bancos> lstBancos = new List<Bancos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @" select p.identificacion
                                        from bancos b
                                        inner join persona p on p.COD_PERSONA = b.COD_PERSONA
                                        where b.COD_BANCO = '" + codigo + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        string respuesta = "";
                        if (resultado.Read())
                        {
                            //Bancos entidad = new Bancos();
                            respuesta = Convert.ToString(resultado["identificacion"]);
                            //lstBancos.Add(entidad);
                        }

                        return respuesta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BancosData", "ConsultaBancoPersona", ex);
                        return null;
                    }
                }
            }
        }

    }
}
