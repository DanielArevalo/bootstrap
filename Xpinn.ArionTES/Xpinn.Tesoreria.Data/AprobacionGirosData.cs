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
    /// Objeto de acceso a datos para la tabla AreasCaj
    /// </summary>
    public class AprobacionGirosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla AreasCaj
        /// </summary>
        public AprobacionGirosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<Giro> ListarGiro(Giro pGiro,String Orden,DateTime pFechaGiro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Giro> lstGiro = new List<Giro>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT g.*,VERIFICAR_DISTRIBUCION(G.NUMERO_RADICACION,G.IDGIRO) AS DISTRIBUCION FROM V_Giro g ";

                        Int32 LongSql = sql.Length;
                                                
                        sql += ObtenerFiltro(pGiro, "g.");

                        if (sql.Substring(LongSql).ToUpper().Contains("WHERE"))
                            sql += " and g.Estado = 0 and g.forma_pago != 4 ";
                        else
                            sql += " where g.Estado = 0 and g.forma_pago != 4 ";

                        if (pFechaGiro != null && pFechaGiro != DateTime.MinValue)
                        {
                            if (sql.Substring(LongSql).ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " g.FEC_GIRO = To_Date('" + Convert.ToDateTime(pFechaGiro).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " g.FEC_GIRO = '" + Convert.ToDateTime(pFechaGiro).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (Orden != "")
                            sql += "ORDER BY " + Orden;
                        else
                            sql += " ORDER BY g.IDGIRO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Giro entidad = new Giro();
                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt32(resultado["IDGIRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["NOM_FORMA_PAGO"] != DBNull.Value) entidad.nom_forma_pago = Convert.ToString(resultado["NOM_FORMA_PAGO"]);
                            if (resultado["TIPO_ACTO"] != DBNull.Value) entidad.tipo_acto = Convert.ToInt32(resultado["TIPO_ACTO"]);
                            if (resultado["FEC_REG"] != DBNull.Value) entidad.fec_reg = Convert.ToDateTime(resultado["FEC_REG"]);
                            if (resultado["FEC_GIRO"] != DBNull.Value) entidad.fec_giro = Convert.ToDateTime(resultado["FEC_GIRO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["COD_OPE"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["NOM_TIPO_COMP"] != DBNull.Value) entidad.nom_tipo_comp = Convert.ToString(resultado["NOM_TIPO_COMP"]);
                            if (resultado["USU_GEN"] != DBNull.Value) entidad.usu_gen = Convert.ToString(resultado["USU_GEN"]);
                            if (resultado["USU_APLI"] != DBNull.Value) entidad.usu_apli = Convert.ToString(resultado["USU_APLI"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["USU_APRO"] != DBNull.Value) entidad.usu_apro = Convert.ToString(resultado["USU_APRO"]);
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["NUM_REFERENCIA"] != DBNull.Value) entidad.num_referencia = Convert.ToString(resultado["NUM_REFERENCIA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["COD_BANCO"]);
                            if (resultado["NOM_BANCO"] != DBNull.Value) entidad.nom_banco = Convert.ToString(resultado["NOM_BANCO"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["NUM_REFERENCIA1"] != DBNull.Value) entidad.num_referencia1 = Convert.ToString(resultado["NUM_REFERENCIA1"]);
                            if (resultado["COD_BANCO1"] != DBNull.Value) entidad.cod_banco1 = Convert.ToInt64(resultado["COD_BANCO1"]);
                            if (resultado["NOM_BANCO1"] != DBNull.Value) entidad.nom_banco1 = Convert.ToString(resultado["NOM_BANCO1"]);
                            if (resultado["FEC_APRO"] != DBNull.Value) entidad.fec_apro = Convert.ToDateTime(resultado["FEC_APRO"]);
                            if (resultado["COB_COMISION"] != DBNull.Value) entidad.cob_comision = Convert.ToInt32(resultado["COB_COMISION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["NOM_GENERADO"] != DBNull.Value) entidad.nom_generado = Convert.ToString(resultado["NOM_GENERADO"]);
                            if (resultado["DISTRIBUCION"] != DBNull.Value) entidad.distribuir = Convert.ToInt32(resultado["DISTRIBUCION"]);
                            if (entidad.distribuir > 0)
                                entidad.distribuir = 1;
                            lstGiro.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroData", "ListarGiro", ex);
                        return null;
                    }
                }
            }
        }

        public Giro AprobarGiro(Giro pGiro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidgiro = cmdTransaccionFactory.CreateParameter();
                        pidgiro.ParameterName = "p_idgiro";
                        pidgiro.Value = pGiro.idgiro;
                        pidgiro.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pGiro.estado;
                        pestado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusu_apro = cmdTransaccionFactory.CreateParameter();
                        pusu_apro.ParameterName = "p_usu_apro";
                        pusu_apro.Value = pGiro.usu_apro;
                        pusu_apro.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pusu_apro);

                        DbParameter pfec_apro = cmdTransaccionFactory.CreateParameter();
                        pfec_apro.ParameterName = "p_fec_apro";
                        pfec_apro.Value = pGiro.fec_apro;
                        pfec_apro.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfec_apro);

                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        if (pGiro.idctabancaria != 0) pidctabancaria.Value = pGiro.idctabancaria; else pidctabancaria.Value = DBNull.Value;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_GIROAPROBAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionGirosData", "AprobarGiro", ex);
                        return null;
                    }
                }
            }
        }


        public CuentasBancarias ConsultarCuentasBancariasXNumCuenta(String pNumCuenta, Usuario vUsuario)
        {
            DbDataReader resultado;
            CuentasBancarias entidad = new CuentasBancarias();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select IDCTABANCARIA from CUENTA_BANCARIA where TRIM(NUM_CUENTA) = '" + pNumCuenta.ToString()+"'";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);                            
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasBancariasData", "ConsultarCuentasBancariasXNumCuenta", ex);
                        return null;
                    }
                }
            }
        }

    }
}