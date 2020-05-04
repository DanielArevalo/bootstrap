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
   
    public class ChequeraData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

     
        public ChequeraData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public Chequera CrearChequera(Chequera pChequera, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidchequera = cmdTransaccionFactory.CreateParameter();
                        pidchequera.ParameterName = "p_idchequera";
                        pidchequera.Value = pChequera.idchequera;
                        pidchequera.Direction = ParameterDirection.Output;
                        pidchequera.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidchequera);

                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        pidctabancaria.Value = pChequera.idctabancaria;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        DbParameter pprefijo = cmdTransaccionFactory.CreateParameter();
                        pprefijo.ParameterName = "p_prefijo";
                        if (pChequera.prefijo != null) pprefijo.Value = pChequera.prefijo; else pprefijo.Value = DBNull.Value;
                        pprefijo.Direction = ParameterDirection.Input;
                        pprefijo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprefijo);

                        DbParameter pcheque_ini = cmdTransaccionFactory.CreateParameter();
                        pcheque_ini.ParameterName = "p_cheque_ini";
                        pcheque_ini.Value = pChequera.cheque_ini;
                        pcheque_ini.Direction = ParameterDirection.Input;
                        pcheque_ini.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcheque_ini);

                        DbParameter pcheque_fin = cmdTransaccionFactory.CreateParameter();
                        pcheque_fin.ParameterName = "p_cheque_fin";
                        pcheque_fin.Value = pChequera.cheque_fin;
                        pcheque_fin.Direction = ParameterDirection.Input;
                        pcheque_fin.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcheque_fin);

                        DbParameter pfecha_entrega = cmdTransaccionFactory.CreateParameter();
                        pfecha_entrega.ParameterName = "p_fecha_entrega";
                        pfecha_entrega.Value = pChequera.fecha_entrega;
                        pfecha_entrega.Direction = ParameterDirection.Input;
                        pfecha_entrega.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_entrega);

                        DbParameter pnum_sig_che = cmdTransaccionFactory.CreateParameter();
                        pnum_sig_che.ParameterName = "p_num_sig_che";
                        pnum_sig_che.Value = pChequera.num_sig_che;
                        pnum_sig_che.Direction = ParameterDirection.Input;
                        pnum_sig_che.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_sig_che);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pChequera.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfechacreacion = cmdTransaccionFactory.CreateParameter();
                        pfechacreacion.ParameterName = "p_fechacreacion";
                        pfechacreacion.Value = pChequera.fechacreacion;
                        pfechacreacion.Direction = ParameterDirection.Input;
                        pfechacreacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacreacion);

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "p_usuariocreacion";
                        pusuariocreacion.Value = pChequera.usuariocreacion;
                        pusuariocreacion.Direction = ParameterDirection.Input;
                        pusuariocreacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CHEQUERA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pChequera.idchequera = Convert.ToInt32(pidchequera.Value);
                        return pChequera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ChequeraData", "CrearChequera", ex);
                        return null;
                    }
                }
            }
        }



        public Chequera ModificarChequera(Chequera pChequera, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidchequera = cmdTransaccionFactory.CreateParameter();
                        pidchequera.ParameterName = "p_idchequera";
                        pidchequera.Value = pChequera.idchequera;
                        pidchequera.Direction = ParameterDirection.Input;
                        pidchequera.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidchequera);

                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        pidctabancaria.Value = pChequera.idctabancaria;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        DbParameter pprefijo = cmdTransaccionFactory.CreateParameter();
                        pprefijo.ParameterName = "p_prefijo";
                        if (pChequera.prefijo != null) pprefijo.Value = pChequera.prefijo; else pprefijo.Value = DBNull.Value;
                        pprefijo.Direction = ParameterDirection.Input;
                        pprefijo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pprefijo);

                        DbParameter pcheque_ini = cmdTransaccionFactory.CreateParameter();
                        pcheque_ini.ParameterName = "p_cheque_ini";
                        pcheque_ini.Value = pChequera.cheque_ini;
                        pcheque_ini.Direction = ParameterDirection.Input;
                        pcheque_ini.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcheque_ini);

                        DbParameter pcheque_fin = cmdTransaccionFactory.CreateParameter();
                        pcheque_fin.ParameterName = "p_cheque_fin";
                        pcheque_fin.Value = pChequera.cheque_fin;
                        pcheque_fin.Direction = ParameterDirection.Input;
                        pcheque_fin.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcheque_fin);

                        DbParameter pfecha_entrega = cmdTransaccionFactory.CreateParameter();
                        pfecha_entrega.ParameterName = "p_fecha_entrega";
                        pfecha_entrega.Value = pChequera.fecha_entrega;
                        pfecha_entrega.Direction = ParameterDirection.Input;
                        pfecha_entrega.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_entrega);

                        DbParameter pnum_sig_che = cmdTransaccionFactory.CreateParameter();
                        pnum_sig_che.ParameterName = "p_num_sig_che";
                        pnum_sig_che.Value = pChequera.num_sig_che;
                        pnum_sig_che.Direction = ParameterDirection.Input;
                        pnum_sig_che.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_sig_che);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pChequera.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfecultmod = cmdTransaccionFactory.CreateParameter();
                        pfecultmod.ParameterName = "p_fecultmod";
                        pfecultmod.Value = pChequera.fecultmod;
                        pfecultmod.Direction = ParameterDirection.Input;
                        pfecultmod.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecultmod);

                        DbParameter pusuariomod = cmdTransaccionFactory.CreateParameter();
                        pusuariomod.ParameterName = "p_usuariomod";
                        pusuariomod.Value = pChequera.usuariomod;
                        pusuariomod.Direction = ParameterDirection.Input;
                        pusuariomod.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusuariomod);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CHEQUERA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pChequera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ChequeraData", "ModificarChequera", ex);
                        return null;
                    }
                }
            }
        }


        
        public void EliminarChequera(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidchequera = cmdTransaccionFactory.CreateParameter();
                        pidchequera.ParameterName = "p_idchequera";
                        pidchequera.Value = pId;
                        pidchequera.Direction = ParameterDirection.Input;
                        pidchequera.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidchequera);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_CHEQUERA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ChequeraData", "EliminarChequera", ex);
                    }
                }
            }
        }


        public List<Chequera> ListarChequera(Chequera pChequera, Usuario vUsuario,String filtro)
        {
            DbDataReader resultado;
            List<Chequera> lstChequera = new List<Chequera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        string sql = @"select chequera.*, cuenta_bancaria.num_cuenta as num_cuenta,bancos.nombrebanco as nombrebanco "
                                      + "from chequera "
                                      + "left join  cuenta_bancaria  on chequera.idctabancaria = cuenta_bancaria.idctabancaria "
                                      + "left join bancos  on bancos.cod_banco = cuenta_bancaria.cod_banco "
                                      + "where  1=1" + filtro + " order by chequera.idchequera";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Chequera entidad = new Chequera();
                            if (resultado["IDCHEQUERA"] != DBNull.Value) entidad.idchequera = Convert.ToInt32(resultado["IDCHEQUERA"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["PREFIJO"] != DBNull.Value) entidad.prefijo = Convert.ToString(resultado["PREFIJO"]);
                            if (resultado["CHEQUE_INI"] != DBNull.Value) entidad.cheque_ini = Convert.ToInt32(resultado["CHEQUE_INI"]);
                            if (resultado["CHEQUE_FIN"] != DBNull.Value) entidad.cheque_fin = Convert.ToInt32(resultado["CHEQUE_FIN"]);
                            if (resultado["NUM_SIG_CHE"] != DBNull.Value) entidad.num_sig_che = Convert.ToInt32(resultado["NUM_SIG_CHE"]);
                            if (resultado["FECHA_ENTREGA"] != DBNull.Value) entidad.fecha_entrega = Convert.ToDateTime(resultado["FECHA_ENTREGA"]);
                            //if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            //if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            //if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            //if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            //if (resultado["USUARIOMOD"] != DBNull.Value) entidad.usuariomod = Convert.ToString(resultado["USUARIOMOD"]);
                            lstChequera.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstChequera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ChequeraData", "ListarChequera", ex);
                        return null;
                    }
                }
            }
        }


        public Chequera ConsultarChequera(Chequera pChequera, Usuario vUsuario)
        {
            DbDataReader resultado;
            Chequera entidad = new Chequera();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select chequera.*, cuenta_bancaria.num_cuenta as num_cuenta,bancos.nombrebanco as nombrebanco "
                                       + "from chequera "
                                       + "left join  cuenta_bancaria  on chequera.idctabancaria = cuenta_bancaria.idctabancaria "
                                       + "left join bancos  on bancos.cod_banco = cuenta_bancaria.cod_banco "
                                       + "where chequera.idchequera = " + pChequera.idchequera;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCHEQUERA"] != DBNull.Value) entidad.idchequera = Convert.ToInt32(resultado["IDCHEQUERA"]);
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["NOMBREBANCO"]);
                            if (resultado["PREFIJO"] != DBNull.Value) entidad.prefijo = Convert.ToString(resultado["PREFIJO"]);
                            if (resultado["CHEQUE_INI"] != DBNull.Value) entidad.cheque_ini = Convert.ToInt32(resultado["CHEQUE_INI"]);
                            if (resultado["CHEQUE_FIN"] != DBNull.Value) entidad.cheque_fin = Convert.ToInt32(resultado["CHEQUE_FIN"]);
                            if (resultado["NUM_SIG_CHE"] != DBNull.Value) entidad.num_sig_che = Convert.ToInt32(resultado["NUM_SIG_CHE"]);
                            if (resultado["FECHA_ENTREGA"] != DBNull.Value) entidad.fecha_entrega = Convert.ToDateTime(resultado["FECHA_ENTREGA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            if (resultado["USUARIOMOD"] != DBNull.Value) entidad.usuariomod = Convert.ToString(resultado["USUARIOMOD"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ChequeraData", "ConsultarChequera", ex);
                        return null;
                    }
                }
            }
        }



        public Chequera ConsultarBanco(Chequera pChequera, Usuario vUsuario)
        {
            DbDataReader resultado;
            Chequera entidad = new Chequera();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                            string sql = @"select b.nombrebanco from bancos b inner join cuenta_bancaria c"
                                           +" on b.cod_banco = c.cod_banco where c.idctabancaria = "+ pChequera.idctabancaria;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NOMBREBANCO"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["NOMBREBANCO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ChequeraData", "ConsultarChequera", ex);
                        return null;
                    }
                }
            }
        }


        public List<Chequera> ListarCuentasBancarias(Chequera pChequera, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Chequera> lstCTASBANCARIAS = new List<Chequera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {


                        string sql = @"select idctabancaria,num_cuenta from cuenta_bancaria ORDER BY IDCTABANCARIA";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Chequera entidad = new Chequera();
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);

                            lstCTASBANCARIAS.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstCTASBANCARIAS;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ChequeraData", "ListarCuentasBancarias", ex);
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
                        string sql = "select max(idchequera)+1 from chequera";

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
        public List<Chequera> ConsultarChequeraReporte(Chequera pChequera, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Chequera> lstChequera = new List<Chequera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT c.FECHA,c.NUM_CHEQUE,c.IDEN_BENEF,
                                        c.NOMBRE_BENEFICIARIO,mv.VALOR,dconta.TIPO_COMP,
                                        c.NUM_COMP from CHEQUE c left join CUENTA_BANCARIA cb
                                         ON c.IDCTABANCARIA=cb.IDCTABANCARIA  
                                       LEFT JOIN E_COMEGRES degres ON degres.NUM_COMP=c.NUM_COMP
                                       left JOIN E_COMCONTA dconta ON dconta.NUM_COMP=c.NUM_COMP
                                       left JOIN CHEQUERA ch ON ch.IDCTABANCARIA=cb.IDCTABANCARIA
                                       left join movimientocaja mv on mv.num_documento=c.num_cheque
                                        where ch.IDCHEQUERA= " + pChequera.idchequera;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Chequera entidad = new Chequera();
                            if (resultado["FECHA"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["NUM_CHEQUE"] != DBNull.Value) entidad.num_sig_che = Convert.ToInt32(resultado["NUM_CHEQUE"]);
                            if (resultado["IDEN_BENEF"] != DBNull.Value) entidad.Cedula = Convert.ToInt64(resultado["IDEN_BENEF"]);
                            if (resultado["NOMBRE_BENEFICIARIO"] != DBNull.Value) entidad.nombrepersona = Convert.ToString(resultado["NOMBRE_BENEFICIARIO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.TipoCom = Convert.ToString(resultado["TIPO_COMP"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.NumComp = Convert.ToInt64(resultado["NUM_COMP"]);
                            lstChequera.Add(entidad);

                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstChequera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ChequeraData", "ConsultarChequera", ex);
                        return null;
                    }
                }
            }
        }

    }
}