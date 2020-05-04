using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Data
{
    public class CuentasBancariasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CuentasBancariasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public CuentasBancarias CrearCuentasContables(CuentasBancarias pCuent, Usuario vUsuario,int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        pidctabancaria.Value = pCuent.idctabancaria;
                        if(opcion == 1)
                            pidctabancaria.Direction = ParameterDirection.Output;
                        else
                            pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pCuent.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pnum_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnum_cuenta.ParameterName = "p_num_cuenta";
                        pnum_cuenta.Value = pCuent.num_cuenta;
                        pnum_cuenta.Direction = ParameterDirection.Input;
                        pnum_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_cuenta);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        ptipo_cuenta.Value = pCuent.tipo_cuenta;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pCuent.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pCuent.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "p_cod_oficina";
                        if (pCuent.cod_oficina != 0)
                            pcod_oficina.Value = pCuent.cod_oficina;
                        else
                            pcod_oficina.Value = DBNull.Value;
                        pcod_oficina.Direction = ParameterDirection.Input;
                        pcod_oficina.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if(opcion == 1 )
                            cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CUENTA_BAN_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CUENTA_BAN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if(opcion == 1)
                            pCuent.idctabancaria = Convert.ToInt32(pidctabancaria.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pCuent;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasBancariasData", "CrearCuentasContables", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarCuentasBancarias(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        pidctabancaria.Value = pId;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CUENTA_BAN_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentData", "EliminarCuentasBancarias", ex);
                    }
                }
            }
        }


        public List<CuentasBancarias> ListarCuentasBancarias(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuentasBancarias> lstCuentas = new List<CuentasBancarias>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select c.idctabancaria,b.nombrebanco,c.num_cuenta,c.cod_cuenta,p.nombre as nombrecuenta, "
                                        +"(case c.estado when '1' then 'Activo' when '2' then 'Inactivo' end) as nomestado "
                                        +"from cuenta_bancaria c left join bancos b on b.cod_banco = c.cod_banco "
                                        +"left join plan_cuentas p on p.cod_cuenta = c.cod_cuenta where 1 = 1 " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuentasBancarias entidad = new CuentasBancarias();
                            if (resultado["idctabancaria"] != DBNull.Value) entidad.idctabancaria = (Convert.ToInt32(resultado["idctabancaria"]));
                            if (resultado["nombrebanco"] != DBNull.Value) entidad.nombrebanco = (Convert.ToString(resultado["nombrebanco"]));
                            if (resultado["num_cuenta"] != DBNull.Value) entidad.num_cuenta = (Convert.ToString(resultado["num_cuenta"]));
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = (Convert.ToString(resultado["cod_cuenta"]));
                            if (resultado["nombrecuenta"] != DBNull.Value) entidad.nombrecuenta= (Convert.ToString(resultado["nombrecuenta"]));
                            if (resultado["nomestado"] != DBNull.Value) entidad.estado = (Convert.ToString(resultado["nomestado"]));
                            lstCuentas.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasBancariasData", "ListarCuentasBancarias", ex);
                        return null;
                    }
                }
            }
        }


        public List<CuentasBancarias> ListarBancos(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuentasBancarias> lstCuentas = new List<CuentasBancarias>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select c.cod_banco,b.nombrebanco From cuenta_bancaria c inner join bancos b on b.cod_banco = c.cod_banco";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuentasBancarias entidad = new CuentasBancarias();
                            if (resultado["cod_banco"] != DBNull.Value) entidad.cod_banco = (Convert.ToInt32(resultado["cod_banco"]));
                            if (resultado["nombrebanco"] != DBNull.Value) entidad.nombrebanco = (Convert.ToString(resultado["nombrebanco"]));
                           
                            lstCuentas.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasBancariasData", "ListarBancos", ex);
                        return null;
                    }
                }
            }
        }

        public List<CuentasBancarias> ListarALLBancos(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuentasBancarias> lstCuentas = new List<CuentasBancarias>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select cod_banco,nombrebanco from bancos order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuentasBancarias entidad = new CuentasBancarias();
                            if (resultado["cod_banco"] != DBNull.Value) entidad.cod_banco = (Convert.ToInt32(resultado["cod_banco"]));
                            if (resultado["nombrebanco"] != DBNull.Value) entidad.nombrebanco = (Convert.ToString(resultado["nombrebanco"]));

                            lstCuentas.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasBancariasData", "ListarALLBancos", ex);
                        return null;
                    }
                }
            }
        }

        public List<CuentasBancarias> ListarNumeroCuentas(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuentasBancarias> lstCuentas = new List<CuentasBancarias>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select c.num_cuenta,c.num_cuenta ||' - '|| b.nombrebanco as nombre "
                                    +"From cuenta_bancaria c inner join bancos b on b.cod_banco = c.cod_banco";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuentasBancarias entidad = new CuentasBancarias();
                            if (resultado["num_cuenta"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["num_cuenta"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["nombre"]);

                            lstCuentas.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasBancariasData", "ListarNumeroCuentas", ex);
                        return null;
                    }
                }
            }
        }

        public CuentasBancarias ConsultarCuentasBancarias(Int32 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasBancarias entidad = new CuentasBancarias();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM cuenta_bancaria WHERE IDCTABANCARIA = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasBancariasData", "ConsultarCuentasBancarias", ex);
                        return null;
                    }
                }
            }
        }


        public List<CuentasBancarias> ListarCuentasBancarias1(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CuentasBancarias> lstCuentas = new List<CuentasBancarias>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select idctabancaria,num_cuenta "
                                    + "From cuenta_bancaria where estado = 1 order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CuentasBancarias entidad = new CuentasBancarias();
                            if (resultado["idctabancaria"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["idctabancaria"]);
                            if (resultado["num_cuenta"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["num_cuenta"]);
                            lstCuentas.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasBancariasData", "ListarCuantasBancarias1", ex);
                        return null;
                    }
                }
            }
        }



        public CuentasBancarias ConsultarCuentasBancariasPorBanco(Int32 pId,string num_cuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasBancarias entidad = new CuentasBancarias();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM cuenta_bancaria WHERE COD_BANCO = " + pId.ToString() + " and num_cuenta = '" + num_cuenta + "'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasBancariasData", "ConsultarCuentasBancariasPorBanco", ex);
                        return null;
                    }
                }
            }
        }


    }
}




