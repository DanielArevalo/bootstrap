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
    public class RealizacionGirosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla AreasCaj
        /// </summary>
        public RealizacionGirosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<Giro> ListarGiroAprobados(Giro pGiro,String Orden,DateTime pFechaGiro,DateTime pFechaAprobacion,Boolean Forma_Pago, Usuario vUsuario)
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
                        string sql = @"SELECT g.*,VERIFICAR_DISTRIBUCION(G.NUMERO_RADICACION,G.IDGIRO) AS DISTRIBUCION , D.IDENTIFICACION IDENT_BENE,D.NOMBRE NOMBRE_BENE
                                       FROM V_GIRO G  LEFT JOIN GIRO_DISTRIBUCION D ON D.IDDETGIRO = G.IDGIRO ";
                        Int32 LongSql = sql.Length;
                        sql += ObtenerFiltro(pGiro, "g.");

                        if (sql.Substring(LongSql).ToUpper().Contains("WHERE"))
                            sql += " and g.Estado = 1 and g.forma_pago != 4 ";
                        else
                            sql += " where g.Estado = 1 and g.forma_pago != 4 ";

                        if (sql.Substring(LongSql).ToUpper().Contains("WHERE"))
                            if(Forma_Pago == true)
                                sql += " and g.FORMA_PAGO = 3 ";
                            else
                                sql += " and g.FORMA_PAGO != 3 ";
                        else
                            if (Forma_Pago == true)
                                sql += " where g.FORMA_PAGO = 3 ";
                            else
                                sql += " where g.FORMA_PAGO != 3 ";

                        if (pFechaGiro != null && pFechaGiro != DateTime.MinValue)
                        {
                            if (sql.Substring(LongSql).ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " Trunc(g.FEC_GIRO) = To_Date('" + Convert.ToDateTime(pFechaGiro).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " g.FEC_GIRO = '" + Convert.ToDateTime(pFechaGiro).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (pFechaAprobacion != null && pFechaAprobacion != DateTime.MinValue)
                        {
                            if (sql.Substring(LongSql).ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " Trunc(g.FEC_APRO) = To_Date('" + Convert.ToDateTime(pFechaAprobacion).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " g.FEC_APRO = '" + Convert.ToDateTime(pFechaAprobacion).ToString(conf.ObtenerFormatoFecha()) + "' ";
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
                            if (entidad.forma_pago != null)
                                if (resultado["NOM_FORMA_PAGO"] != DBNull.Value) entidad.nom_forma_pago = entidad.forma_pago + "-" + Convert.ToString(resultado["NOM_FORMA_PAGO"]);
                            if (resultado["TIPO_ACTO"] != DBNull.Value) entidad.tipo_acto = Convert.ToInt32(resultado["TIPO_ACTO"]);
                            if (resultado["FEC_REG"] != DBNull.Value) entidad.fec_reg = Convert.ToDateTime(resultado["FEC_REG"]);
                            if (resultado["FEC_GIRO"] != DBNull.Value) entidad.fec_giro = Convert.ToDateTime(resultado["FEC_GIRO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["COD_OPE"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (entidad.tipo_comp != null)
                                if (resultado["NOM_TIPO_COMP"] != DBNull.Value) entidad.nom_tipo_comp = entidad.tipo_comp +"-"+ Convert.ToString(resultado["NOM_TIPO_COMP"]);
                            if (resultado["USU_GEN"] != DBNull.Value) entidad.usu_gen = Convert.ToString(resultado["USU_GEN"]);
                            if (resultado["USU_APLI"] != DBNull.Value) entidad.usu_apli = Convert.ToString(resultado["USU_APLI"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (entidad.estado != null)
                                if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = entidad.estado + "-" + Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["USU_APRO"] != DBNull.Value) entidad.usu_apro = Convert.ToString(resultado["USU_APRO"]);
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["NUM_REFERENCIA"] != DBNull.Value) entidad.num_referencia = Convert.ToString(resultado["NUM_REFERENCIA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt64(resultado["COD_BANCO"]);
                            if (entidad.cod_banco != null)
                                if (resultado["NOM_BANCO"] != DBNull.Value) entidad.nom_banco = entidad.cod_banco + "-" + Convert.ToString(resultado["NOM_BANCO"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["NUM_REFERENCIA1"] != DBNull.Value) entidad.num_referencia1 = Convert.ToString(resultado["NUM_REFERENCIA1"]);
                            if (resultado["COD_BANCO1"] != DBNull.Value) entidad.cod_banco1 = Convert.ToInt64(resultado["COD_BANCO1"]);
                            if (entidad.cod_banco1 != null)
                                if (resultado["NOM_BANCO1"] != DBNull.Value) entidad.nom_banco1 = entidad.cod_banco1 + "-" + Convert.ToString(resultado["NOM_BANCO1"]);
                            if (resultado["FEC_APRO"] != DBNull.Value) entidad.fec_apro = Convert.ToDateTime(resultado["FEC_APRO"]);
                            if (resultado["COB_COMISION"] != DBNull.Value) entidad.cob_comision = Convert.ToInt32(resultado["COB_COMISION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["NOM_GENERADO"] != DBNull.Value) entidad.nom_generado = Convert.ToString(resultado["NOM_GENERADO"]);
                            if (resultado["DISTRIBUCION"] != DBNull.Value) entidad.distribuir = Convert.ToInt32(resultado["DISTRIBUCION"]);
                            if (resultado["IDENT_BENE"] != DBNull.Value) entidad.identif_bene = Convert.ToString(resultado["IDENT_BENE"]);
                            if (resultado["NOMBRE_BENE"] != DBNull.Value) entidad.nombre_bene = Convert.ToString(resultado["NOMBRE_BENE"]);
                            if (entidad.distribuir > 0)
                                entidad.distribuir = 1;
                            if (resultado["NUM_REFERENCIA"] != DBNull.Value && entidad.forma_pago == 2) entidad.activar = true;
                            if (resultado["NOM_TIPO_CUENTA_CLI"] != DBNull.Value) entidad.nom_tipo_cuenta = Convert.ToString(resultado["NOM_TIPO_CUENTA_CLI"]);
                            if (resultado["TIPO_CUENTA_CLI"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt64(resultado["TIPO_CUENTA_CLI"]);
                            lstGiro.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RealizacionGirosData", "ListarGiroAprobados", ex);
                        return null;
                    }
                }
            }
        }



        public Giro RealizarGiro(Giro pGiro,Usuario vUsuario)
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

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_GIROREALIZAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        
                        return pGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RealizacionGirosData", "RealizarGiro", ex);
                        return null;
                    }
                }
            }
        }


        public GiroRealizado CrearGiroRealizado(GiroRealizado pGiroRealizado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //GRABAR TABLA GIRO_REALIZADO
                        DbParameter pidgirorealizado = cmdTransaccionFactory.CreateParameter();
                        pidgirorealizado.ParameterName = "p_idgirorealizado";
                        pidgirorealizado.Value = pGiroRealizado.idgirorealizado;
                        pidgirorealizado.Direction = ParameterDirection.Output;
                        pidgirorealizado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgirorealizado);

                        DbParameter pidgiro = cmdTransaccionFactory.CreateParameter();
                        pidgiro.ParameterName = "p_idgiro";
                        pidgiro.Value = pGiroRealizado.idgiro;
                        pidgiro.Direction = ParameterDirection.Input;
                        pidgiro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pfec_realiza = cmdTransaccionFactory.CreateParameter();
                        pfec_realiza.ParameterName = "p_fec_realiza";
                        pfec_realiza.Value = pGiroRealizado.fec_realiza;
                        pfec_realiza.Direction = ParameterDirection.Input;
                        pfec_realiza.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_realiza);

                        DbParameter pusu_realiza = cmdTransaccionFactory.CreateParameter();
                        pusu_realiza.ParameterName = "p_usu_realiza";
                        pusu_realiza.Value = pGiroRealizado.usu_realiza;
                        pusu_realiza.Direction = ParameterDirection.Input;
                        pusu_realiza.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_realiza);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pGiroRealizado.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter parchivo = cmdTransaccionFactory.CreateParameter();
                        parchivo.ParameterName = "p_archivo";
                        if (pGiroRealizado.archivo == null)
                            parchivo.Value = DBNull.Value;
                        else
                            parchivo.Value = pGiroRealizado.archivo;
                        parchivo.Direction = ParameterDirection.Input;
                        parchivo.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(parchivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_GIROREALIZ_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pGiroRealizado.idgirorealizado = Convert.ToInt32(pidgirorealizado.Value);

                        DAauditoria.InsertarLog(pGiroRealizado, "GIRO_REALIZADO", vUsuario, Accion.Crear.ToString(), TipoAuditoria.Giros, "Creacion de realizacion de giro con codigo de giro " + pGiroRealizado.idgiro);

                        return pGiroRealizado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RealizadoData", "CrearGiroRealizado", ex);
                        return null;
                    }
                }
            }
        }



        public void ReemplazarConsultaSQL(string pConsulta, ref string pResult, ref string pError, Usuario vUsuario)
        {
            DbDataReader resultado;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = pConsulta;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado[0] != DBNull.Value) pResult = Convert.ToString(resultado[0]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                    }
                }
            }
        }

        
    }
}