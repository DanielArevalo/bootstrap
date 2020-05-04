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
   
    public class ConciliacionBancariaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;


        public ConciliacionBancariaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public ConciliacionBancaria CrearConciliacion(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconciliacion = cmdTransaccionFactory.CreateParameter();
                        pidconciliacion.ParameterName = "p_idconciliacion";
                        pidconciliacion.Value = pConci.idconciliacion;
                        pidconciliacion.Direction = ParameterDirection.Output;
                        pidconciliacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconciliacion);

                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        pidctabancaria.Value = pConci.idctabancaria;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pConci.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "p_fecha_inicial";
                        pfecha_inicial.Value = pConci.fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        pfecha_final.Value = pConci.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        DbParameter psaldo_contable = cmdTransaccionFactory.CreateParameter();
                        psaldo_contable.ParameterName = "p_saldo_contable";
                        if (pConci.saldo_contable != 0) psaldo_contable.Value = pConci.saldo_contable; else psaldo_contable.Value = DBNull.Value;
                        psaldo_contable.Direction = ParameterDirection.Input;
                        psaldo_contable.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_contable);

                        DbParameter psaldo_extracto = cmdTransaccionFactory.CreateParameter();
                        psaldo_extracto.ParameterName = "p_saldo_extracto";
                        if (pConci.saldo_extracto != 0) psaldo_extracto.Value = pConci.saldo_extracto; else psaldo_extracto.Value = DBNull.Value;
                        psaldo_extracto.Direction = ParameterDirection.Input;
                        psaldo_extracto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_extracto);

                        DbParameter pcodusuario_elabora = cmdTransaccionFactory.CreateParameter();
                        pcodusuario_elabora.ParameterName = "p_codusuario_elabora";
                        pcodusuario_elabora.Value = vUsuario.codusuario;
                        pcodusuario_elabora.Direction = ParameterDirection.Input;
                        pcodusuario_elabora.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario_elabora);

                        DbParameter pcodusuario_aprueba = cmdTransaccionFactory.CreateParameter();
                        pcodusuario_aprueba.ParameterName = "p_codusuario_aprueba";
                        if (pConci.codusuario_aprueba != 0) pcodusuario_aprueba.Value = pConci.codusuario_aprueba; else pcodusuario_aprueba.Value = DBNull.Value;
                        pcodusuario_aprueba.Direction = ParameterDirection.Input;
                        pcodusuario_aprueba.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario_aprueba);

                        DbParameter pnum_extracto = cmdTransaccionFactory.CreateParameter();
                        pnum_extracto.ParameterName = "p_num_extracto";
                        pnum_extracto.Value = pConci.num_extracto;
                        pnum_extracto.Direction = ParameterDirection.Input;
                        pnum_extracto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_extracto);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pConci.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCILIBAN_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pConci.idconciliacion = Convert.ToInt32(pidconciliacion.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pConci;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "CrearConciliacion", ex);
                        return null;
                    }
                }
            }
        }



        public ConciliacionBancaria ModificarConciliacion(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconciliacion = cmdTransaccionFactory.CreateParameter();
                        pidconciliacion.ParameterName = "p_idconciliacion";
                        pidconciliacion.Value = pConci.idconciliacion;
                        pidconciliacion.Direction = ParameterDirection.Input;
                        pidconciliacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconciliacion);

                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        pidctabancaria.Value = pConci.idctabancaria;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        pcod_banco.Value = pConci.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "p_fecha_inicial";
                        pfecha_inicial.Value = pConci.fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter pfecha_final = cmdTransaccionFactory.CreateParameter();
                        pfecha_final.ParameterName = "p_fecha_final";
                        pfecha_final.Value = pConci.fecha_final;
                        pfecha_final.Direction = ParameterDirection.Input;
                        pfecha_final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final);

                        DbParameter psaldo_contable = cmdTransaccionFactory.CreateParameter();
                        psaldo_contable.ParameterName = "p_saldo_contable";
                        if (pConci.saldo_contable != 0) psaldo_contable.Value = pConci.saldo_contable; else psaldo_contable.Value = DBNull.Value;
                        psaldo_contable.Direction = ParameterDirection.Input;
                        psaldo_contable.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_contable);

                        DbParameter psaldo_extracto = cmdTransaccionFactory.CreateParameter();
                        psaldo_extracto.ParameterName = "p_saldo_extracto";
                        if (pConci.saldo_extracto != 0) psaldo_extracto.Value = pConci.saldo_extracto; else psaldo_extracto.Value = DBNull.Value;
                        psaldo_extracto.Direction = ParameterDirection.Input;
                        psaldo_extracto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo_extracto);

                        DbParameter pcodusuario_elabora = cmdTransaccionFactory.CreateParameter();
                        pcodusuario_elabora.ParameterName = "p_codusuario_elabora";
                        pcodusuario_elabora.Value = vUsuario.codusuario;
                        pcodusuario_elabora.Direction = ParameterDirection.Input;
                        pcodusuario_elabora.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario_elabora);

                        DbParameter pcodusuario_aprueba = cmdTransaccionFactory.CreateParameter();
                        pcodusuario_aprueba.ParameterName = "p_codusuario_aprueba";
                        if (pConci.codusuario_aprueba != 0) pcodusuario_aprueba.Value = pConci.codusuario_aprueba; else pcodusuario_aprueba.Value = DBNull.Value;
                        pcodusuario_aprueba.Direction = ParameterDirection.Input;
                        pcodusuario_aprueba.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario_aprueba);

                        DbParameter pnum_extracto = cmdTransaccionFactory.CreateParameter();
                        pnum_extracto.ParameterName = "p_num_extracto";
                        pnum_extracto.Value = pConci.num_extracto;
                        pnum_extracto.Direction = ParameterDirection.Input;
                        pnum_extracto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_extracto);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pConci.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCILIBAN_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pConci;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ModificarConciliacion", ex);
                        return null;
                    }
                }
            }
        }

        
        public void EliminarConciliacion(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidconciliacion = cmdTransaccionFactory.CreateParameter();
                        pidconciliacion.ParameterName = "p_idconciliacion";
                        pidconciliacion.Value = pId;
                        pidconciliacion.Direction = ParameterDirection.Input;
                        pidconciliacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconciliacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "EliminarConciliacion", ex);
                    }
                }
            }
        }


        public List<ConciliacionBancaria> ListarConciliacion(String filtro,DateTime pFechaIni,DateTime pFechaFin, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConciliacionBancaria> lstConcili = new List<ConciliacionBancaria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = @"Select Concbancaria.* ,Cuenta_Bancaria.Num_Cuenta , Bancos.Nombrebanco, case Concbancaria.estado when 1  then 'PENDIENTE' end as estadoConc, "
                                       + "Cuenta_Bancaria.Cod_Cuenta ,Plan_Cuentas.Nombre as NomCuenta , Usuarios.Nombre as UsuarioElabora "
                                        + "From Concbancaria Inner Join Cuenta_Bancaria "
                                        + "On Concbancaria.Idctabancaria = Cuenta_Bancaria.Idctabancaria "
                                        + "Inner Join Bancos On Bancos.Cod_Banco = Concbancaria.Cod_Banco "
                                        + "Inner Join Plan_Cuentas On Plan_Cuentas.Cod_Cuenta = Cuenta_Bancaria.Cod_Cuenta "
                                        + "inner join Usuarios on Usuarios.Codusuario = Concbancaria.Codusuario_Elabora where 1=1 " + filtro;

                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA_INICIAL = To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA_INICIAL = '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " FECHA_FINAL = To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " FECHA_FINAL = '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        
                        sql += " order by Concbancaria.IDCONCILIACION";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConciliacionBancaria entidad = new ConciliacionBancaria();
                            if (resultado["IDCONCILIACION"] != DBNull.Value) entidad.idconciliacion = Convert.ToInt32(resultado["IDCONCILIACION"]);
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            if (resultado["SALDO_CONTABLE"] != DBNull.Value) entidad.saldo_contable = Convert.ToDecimal(resultado["SALDO_CONTABLE"]);
                            if (resultado["SALDO_EXTRACTO"] != DBNull.Value) entidad.saldo_extracto = Convert.ToDecimal(resultado["SALDO_EXTRACTO"]);
                            if (resultado["CODUSUARIO_ELABORA"] != DBNull.Value) entidad.codusuario_elabora = Convert.ToInt32(resultado["CODUSUARIO_ELABORA"]);
                            if (resultado["CODUSUARIO_APRUEBA"] != DBNull.Value) entidad.codusuario_aprueba = Convert.ToInt32(resultado["CODUSUARIO_APRUEBA"]);
                            //AGREGADO
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.numcuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMCUENTA"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMCUENTA"]);
                            if (resultado["USUARIOELABORA"] != DBNull.Value) entidad.usuarioelabora = Convert.ToString(resultado["USUARIOELABORA"]);
                            if (resultado["ESTADOCONC"] != DBNull.Value) entidad.nomestado = Convert.ToString(resultado["ESTADOCONC"]);
                            entidad.nomtipocuenta = "AHORROS";
                            lstConcili.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConcili;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ListarConciliacion", ex);
                        return null;
                    }
                }
            }
        }


        public ConciliacionBancaria ConsultarConciliacion(ConciliacionBancaria pConcili, Usuario vUsuario)
        {
            DbDataReader resultado;
            ConciliacionBancaria entidad = new ConciliacionBancaria();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Concbancaria WHERE IDCONCILIACION =" + pConcili.idconciliacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCONCILIACION"] != DBNull.Value) entidad.idconciliacion = Convert.ToInt32(resultado["IDCONCILIACION"]);
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            if (resultado["SALDO_CONTABLE"] != DBNull.Value) entidad.saldo_contable = Convert.ToDecimal(resultado["SALDO_CONTABLE"]);
                            if (resultado["SALDO_EXTRACTO"] != DBNull.Value) entidad.saldo_extracto = Convert.ToDecimal(resultado["SALDO_EXTRACTO"]);
                            if (resultado["CODUSUARIO_ELABORA"] != DBNull.Value) entidad.codusuario_elabora = Convert.ToInt32(resultado["CODUSUARIO_ELABORA"]);
                            if (resultado["CODUSUARIO_APRUEBA"] != DBNull.Value) entidad.codusuario_aprueba = Convert.ToInt32(resultado["CODUSUARIO_APRUEBA"]);
                            if (resultado["NUM_EXTRACTO"] != DBNull.Value) entidad.num_extracto = Convert.ToInt32(resultado["NUM_EXTRACTO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ConsultarConciliacion", ex);
                        return null;
                    }
                }
            }
        }






        public List<ConciliacionBancaria> ListarCuentasBancarias(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConciliacionBancaria> lstCuentaBanc = new List<ConciliacionBancaria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Idctabancaria, Num_Cuenta,C.Cod_Banco, B.Nombrebanco , C.Tipo_Cuenta , "
                                      + "C.Cod_Cuenta,P.Nombre as NombreCuenta From Cuenta_Bancaria C Inner Join Bancos B On C.Cod_Banco = B.Cod_Banco "
                                      + "inner join Plan_Cuentas p on P.Cod_Cuenta = C.Cod_Cuenta  order by IDCTABANCARIA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConciliacionBancaria entidad = new ConciliacionBancaria();
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.nomtipocuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRECUENTA"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRECUENTA"]);

                            lstCuentaBanc.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentaBanc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ListarCuentasBancarias", ex);
                        return null;
                    }
                }
            }
        }


        public ConciliacionBancaria ConsultarCuentasBancarias(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            DbDataReader resultado;
            ConciliacionBancaria entidad = new ConciliacionBancaria();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Idctabancaria, Num_Cuenta,C.Cod_Banco, B.Nombrebanco , C.Tipo_Cuenta , "
                                      + "C.Cod_Cuenta,P.Nombre as NombreCuenta From Cuenta_Bancaria C Inner Join Bancos B On C.Cod_Banco = B.Cod_Banco "
                                      + "inner join Plan_Cuentas p on P.Cod_Cuenta = C.Cod_Cuenta where IDCTABANCARIA = " + pConci.idctabancaria ;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.nomtipocuenta = Convert.ToString(resultado["TIPO_CUENTA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRECUENTA"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRECUENTA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ConsultarCuentasBancarias", ex);
                        return null;
                    }
                }
            }
        }


        public List<ConciliacionBancaria> ListarPlanCuentas(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConciliacionBancaria> lstCuentaBanc = new List<ConciliacionBancaria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        string sql = @"Select C.Cod_Cuenta,P.Nombre From Cuenta_Bancaria C Inner Join Plan_Cuentas P "
                                       +"On  C.Cod_Cuenta = P.Cod_Cuenta order by C.Cod_Cuenta";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConciliacionBancaria entidad = new ConciliacionBancaria();
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);

                            lstCuentaBanc.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentaBanc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ListarPlanCuentas", ex);
                        return null;
                    }
                }
            }
        }


        public List<ConciliacionBancaria> ListarExtracto(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ConciliacionBancaria> lstCuentaBanc = new List<ConciliacionBancaria>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        string sql = @"Select * From Extracto_Bancario order by IDEXTRACTO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConciliacionBancaria entidad = new ConciliacionBancaria();
                            if (resultado["IDEXTRACTO"] != DBNull.Value) entidad.idextracto = Convert.ToInt32(resultado["IDEXTRACTO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            lstCuentaBanc.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCuentaBanc;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ListarExtracto", ex);
                        return null;
                    }
                }
            }
        }


        public ConciliacionBancaria ConsultarExtracto(ConciliacionBancaria pConci, int pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ConciliacionBancaria entidad = new ConciliacionBancaria();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        string sql = @"Select * From Extracto_Bancario where IDEXTRACTO = "+pId;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDEXTRACTO"] != DBNull.Value) entidad.idextracto = Convert.ToInt32(resultado["IDEXTRACTO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ConsultarExtracto", ex);
                        return null;
                    }
                }
            }
        }



        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(IDCONCILIACION) + 1 from Concbancaria";

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

       


        public ConciliacionBancaria GenerarConciliacion(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "pidctabancaria";
                        pidctabancaria.Value = pConci.idctabancaria;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "pcod_banco";
                        pcod_banco.Value = pConci.cod_banco;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "pcod_cuenta";
                        pcod_cuenta.Value = pConci.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pidextracto = cmdTransaccionFactory.CreateParameter();
                        pidextracto.ParameterName = "pidextracto";
                        pidextracto.Value = pConci.idextracto;
                        pidextracto.Direction = ParameterDirection.Input;
                        pidextracto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidextracto);

                        DbParameter pfecha_inicial = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial.ParameterName = "pfecha_inicial";
                        pfecha_inicial.Value = pConci.fecha_inicial;
                        pfecha_inicial.Direction = ParameterDirection.Input;
                        pfecha_inicial.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial);

                        DbParameter Pfecha_Final = cmdTransaccionFactory.CreateParameter();
                        Pfecha_Final.ParameterName = "Pfecha_Final";
                        Pfecha_Final.Value = pConci.fecha_final;
                        Pfecha_Final.Direction = ParameterDirection.Input;
                        Pfecha_Final.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(Pfecha_Final);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCILIACION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                        return pConci;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "GenerarConciliacion", ex);
                        return null;
                    }
                }
            }
        }



        public List<CONCBANCARIA_RESUMEN> ListarTemporalResumen(CONCBANCARIA_RESUMEN pConci, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CONCBANCARIA_RESUMEN> lstTemporal = new List<CONCBANCARIA_RESUMEN>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        string sql = @"SELECT * FROM TEMP_CONCILIACION order by CODIGO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CONCBANCARIA_RESUMEN entidad = new CONCBANCARIA_RESUMEN();
                            if (resultado["CODIGO"] != DBNull.Value) entidad.idresumen = Convert.ToInt32(resultado["CODIGO"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);

                            lstTemporal.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTemporal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ListarTemporalResumen", ex);
                        return null;
                    }
                }
            }
        }


        public List<CONCBANCARIA_DETALLE> ListarTemporalDetalle(CONCBANCARIA_DETALLE pConci, int opcion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CONCBANCARIA_DETALLE> lstTemporal = new List<CONCBANCARIA_DETALLE>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        string sql = @"SELECT * FROM TEMP_CONCILIACION_DETALLE where TIPO = " + opcion + " order by TIPO,FECHA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CONCBANCARIA_DETALLE entidad = new CONCBANCARIA_DETALLE();
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt64(resultado["TIPO"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["REFERENCIA"] != DBNull.Value) entidad.referencia = Convert.ToString(resultado["REFERENCIA"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.beneficiario = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DIAS"] != DBNull.Value) entidad.dias = Convert.ToInt32(resultado["DIAS"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["NOMTIPO_COMP"] != DBNull.Value) entidad.nomtipo_comp = Convert.ToString(resultado["NOMTIPO_COMP"]);

                            lstTemporal.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstTemporal;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ListarTemporalDetalle", ex);
                        return null;
                    }
                }
            }
        }



        public CONCBANCARIA_RESUMEN CrearResumenConsi(CONCBANCARIA_RESUMEN pConsi, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidresumen = cmdTransaccionFactory.CreateParameter();
                        pidresumen.ParameterName = "p_idresumen";
                        pidresumen.Value = pConsi.idresumen;
                        if (opcion == 1)
                            pidresumen.Direction = ParameterDirection.Output;
                        else
                            pidresumen.Direction = ParameterDirection.Input;
                        pidresumen.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidresumen);

                        DbParameter pidconciliacion = cmdTransaccionFactory.CreateParameter();
                        pidconciliacion.ParameterName = "p_idconciliacion";
                        pidconciliacion.Value = pConsi.idconciliacion;
                        pidconciliacion.Direction = ParameterDirection.Input;
                        pidconciliacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconciliacion);

                        DbParameter pdescripcion = cmdTransaccionFactory.CreateParameter();
                        pdescripcion.ParameterName = "p_descripcion";
                        if (pConsi.descripcion != null) pdescripcion.Value = pConsi.descripcion; else pdescripcion.Value = DBNull.Value;
                        pdescripcion.Direction = ParameterDirection.Input;
                        pdescripcion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pdescripcion);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pConsi.valor != 0)
                            if (pConsi.descripcion != null)
                                pvalor.Value = pConsi.valor;
                            else
                                pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = DBNull.Value;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if(opcion ==1)//CREAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCRESUME_CREAR";
                        else  // MODIFICAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCRESUME_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if(opcion == 1)
                            pConsi.idresumen = Convert.ToInt32(pidresumen.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pConsi;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "CrearResumenConsi", ex);
                        return null;
                    }
                }
            }
        }



        public CONCBANCARIA_DETALLE CrearDetalleConsi(CONCBANCARIA_DETALLE pConci, Usuario vUsuario,int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "p_iddetalle";
                        piddetalle.Value = pConci.iddetalle;
                        if(opcion == 1)//Crear
                            piddetalle.Direction = ParameterDirection.Output;
                        else // MOD
                            piddetalle.Direction = ParameterDirection.Input;
                        piddetalle.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(piddetalle);

                        DbParameter pidconciliacion = cmdTransaccionFactory.CreateParameter();
                        pidconciliacion.ParameterName = "p_idconciliacion";
                        pidconciliacion.Value = pConci.idconciliacion;
                        pidconciliacion.Direction = ParameterDirection.Input;
                        pidconciliacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidconciliacion);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        if (pConci.fecha != DateTime.MinValue) pfecha.Value = pConci.fecha; else pfecha.Value = DBNull.Value; 
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter preferencia = cmdTransaccionFactory.CreateParameter();
                        preferencia.ParameterName = "p_referencia";
                        if (pConci.referencia != null) preferencia.Value = pConci.referencia; else preferencia.Value = DBNull.Value;
                        preferencia.Direction = ParameterDirection.Input;
                        preferencia.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(preferencia);

                        DbParameter pbeneficiario = cmdTransaccionFactory.CreateParameter();
                        pbeneficiario.ParameterName = "p_beneficiario";
                        if (pConci.beneficiario != null) pbeneficiario.Value = pConci.beneficiario; else pbeneficiario.Value = DBNull.Value;
                        pbeneficiario.Direction = ParameterDirection.Input;
                        pbeneficiario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pbeneficiario);

                        DbParameter pconcepto = cmdTransaccionFactory.CreateParameter();
                        pconcepto.ParameterName = "p_concepto";
                        if (pConci.concepto != null) pconcepto.Value = pConci.concepto; else pconcepto.Value = DBNull.Value;
                        pconcepto.Direction = ParameterDirection.Input;
                        pconcepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pconcepto);

                        DbParameter pnum_comp = cmdTransaccionFactory.CreateParameter();
                        pnum_comp.ParameterName = "p_num_comp";
                        if (pConci.num_comp != 0) pnum_comp.Value = pConci.num_comp; else pnum_comp.Value = DBNull.Value;
                        pnum_comp.Direction = ParameterDirection.Input;
                        pnum_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_comp);

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "p_tipo_comp";
                        if (pConci.tipo_comp != 0) ptipo_comp.Value = pConci.tipo_comp; else ptipo_comp.Value = DBNull.Value;
                        ptipo_comp.Direction = ParameterDirection.Input;
                        ptipo_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);

                        DbParameter pdias = cmdTransaccionFactory.CreateParameter();
                        pdias.ParameterName = "p_dias";
                        if (pConci.dias != 0) pdias.Value = pConci.dias; else pdias.Value = DBNull.Value;
                        pdias.Direction = ParameterDirection.Input;
                        pdias.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pdias);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pConci.valor != 0) pvalor.Value = pConci.valor; else pvalor.Value = DBNull.Value;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pConci.tipo != 0) ptipo.Value = pConci.tipo; else ptipo.Value = DBNull.Value;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter pobservacion = cmdTransaccionFactory.CreateParameter();
                        pobservacion.ParameterName = "p_observacion";
                        if (pConci.observacion != null) pobservacion.Value = pConci.observacion; else pobservacion.Value = DBNull.Value;
                        pobservacion.Direction = ParameterDirection.Input;
                        pobservacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if(opcion == 1)//CREAR
                            cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCDETALL_CREAR";
                        else  //MOD
                            cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CONCDETALL_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pConci.iddetalle = Convert.ToInt32(piddetalle.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pConci;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "CrearDetalleConsi", ex);
                        return null;
                    }
                }
            }
        }


        public List<CONCBANCARIA_RESUMEN> ListarResumenConciliacion(Int32 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CONCBANCARIA_RESUMEN> lstResumen = new List<CONCBANCARIA_RESUMEN>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        string sql = @"select * from Concbancaria_Resumen where Idconciliacion = " + pId + " order by IDRESUMEN";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CONCBANCARIA_RESUMEN entidad = new CONCBANCARIA_RESUMEN();
                            if (resultado["IDRESUMEN"] != DBNull.Value) entidad.idresumen = Convert.ToInt32(resultado["IDRESUMEN"]);
                            if (resultado["IDCONCILIACION"] != DBNull.Value) entidad.idconciliacion = Convert.ToInt32(resultado["IDCONCILIACION"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                                                            
                            lstResumen.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstResumen;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ListarResumenConciliacion", ex);
                        return null;
                    }
                }
            }
        }


        public List<CONCBANCARIA_DETALLE> ListarDetalleConciliacion(Int32 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<CONCBANCARIA_DETALLE> lstDetalle = new List<CONCBANCARIA_DETALLE>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        string sql = @"select * from Concbancaria_Detalle where Idconciliacion = " + pId + " order by TIPO,FECHA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CONCBANCARIA_DETALLE entidad = new CONCBANCARIA_DETALLE();
                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt32(resultado["IDDETALLE"]);
                            if (resultado["IDCONCILIACION"] != DBNull.Value) entidad.idconciliacion = Convert.ToInt32(resultado["IDCONCILIACION"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["REFERENCIA"] != DBNull.Value) entidad.referencia = Convert.ToString(resultado["REFERENCIA"]);
                            if (resultado["BENEFICIARIO"] != DBNull.Value) entidad.beneficiario = Convert.ToString(resultado["BENEFICIARIO"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["DIAS"] != DBNull.Value) entidad.dias = Convert.ToInt32(resultado["DIAS"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            lstDetalle.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ConciliacionBancariaData", "ListarDetalleConciliacion", ex);
                        return null;
                    }
                }
            }
        }


    }
}