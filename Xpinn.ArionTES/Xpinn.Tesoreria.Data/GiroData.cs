using System;
using System.Configuration;
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
    /// <summary>
    /// Objeto de acceso a datos para la tabla Giro
    /// </summary>
    public class GiroData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla Giro
        /// </summary>
        public GiroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pGiro"></param>
        /// <param name="pFiltro"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Giro> ListarGiro(Giro pGiro, string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Giro> lstGiro = new List<Giro>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM V_Giro g " + ObtenerFiltro(pGiro, "g.");
                        if (pFiltro != "")
                        {
                            if (sql.Trim().ToUpper().Contains("WHERE"))
                                sql += " AND " + pFiltro;
                            else
                                sql += " WHERE " + pFiltro;
                        }

                        sql += " ORDER BY g.FEC_REG desc ";


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

                            if (resultado["IDENTIFICACION_BENEFICIARIO"] != DBNull.Value) entidad.identificacion_beneficiario = Convert.ToString(resultado["IDENTIFICACION_BENEFICIARIO"]);
                            if (resultado["NOMBRE_BENEFICIARIO"] != DBNull.Value) entidad.nombre_beneficiario = Convert.ToString(resultado["NOMBRE_BENEFICIARIO"]);

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

        public List<Giro> ListarGiroConsulta(Giro pGiro, string pFiltro, string pOrdenar, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Giro> lstGiro = new List<Giro>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM V_Giro g " + ObtenerFiltro(pGiro, "g.");
                        if (pFiltro != "")
                        {
                            if (sql.Trim().ToUpper().Contains("WHERE"))
                                sql += " AND " + pFiltro;
                            else
                                sql += " WHERE " + pFiltro;
                        }

                        if (pOrdenar == "1")
                            sql += " Order by g.IDGIRO  ";
                        else if (pOrdenar == "2")
                            sql += " Order by g.NOMBRE ";
                        else if (pOrdenar == "3")
                            sql += " Order by g.FEC_REG";
                        else if (pOrdenar == "4")
                            sql += " Order by g.IDENTIFICACION";

                        else if (pOrdenar == "5")
                            sql += " Order by g.estado";

                        else if (pOrdenar == "6")
                            sql += " Order by g.primer_nombre";

                        else if (pOrdenar == "7")
                            sql += " Order by g.segundo_nombre";

                        else if (pOrdenar == "8")
                            sql += " Order by g.primer_apellido";

                        else if (pOrdenar == "9")
                            sql += " Order by g.segundo_apellido";

                        else
                            sql += " ORDER BY g.FEC_REG desc ";


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

                            if (resultado["IDENTIFICACION_BENEFICIARIO"] != DBNull.Value) entidad.identificacion_beneficiario = Convert.ToString(resultado["IDENTIFICACION_BENEFICIARIO"]);
                            if (resultado["NOMBRE_BENEFICIARIO"] != DBNull.Value) entidad.nombre_beneficiario = Convert.ToString(resultado["NOMBRE_BENEFICIARIO"]);

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


        public Giro ConsultarGiro(string pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Giro entidad = new Giro();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT G.*,C.COD_BANCO AS BANCO_GIRO,C.COD_CUENTA AS CUENTA_GIRO " 
                                    +"FROM GIRO G LEFT JOIN CUENTA_BANCARIA C ON G.IDCTABANCARIA = C.IDCTABANCARIA WHERE G.IDGIRO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt32(resultado["IDGIRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToInt32(resultado["FORMA_PAGO"]);
                            if (resultado["TIPO_ACTO"] != DBNull.Value) entidad.tipo_acto = Convert.ToInt32(resultado["TIPO_ACTO"]);
                            if (resultado["FEC_REG"] != DBNull.Value) entidad.fec_reg = Convert.ToDateTime(resultado["FEC_REG"]);
                            if (resultado["FEC_GIRO"] != DBNull.Value) entidad.fec_giro = Convert.ToDateTime(resultado["FEC_GIRO"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["COD_OPE"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["USU_GEN"] != DBNull.Value) entidad.usu_gen = Convert.ToString(resultado["USU_GEN"]);
                            if (resultado["USU_APLI"] != DBNull.Value) entidad.usu_apli = Convert.ToString(resultado["USU_APLI"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["USU_APRO"] != DBNull.Value) entidad.usu_apro = Convert.ToString(resultado["USU_APRO"]);
                            if (resultado["FEC_APRO"] != DBNull.Value) entidad.fec_apro = Convert.ToDateTime(resultado["FEC_APRO"]);
                            if (resultado["IDCTABANCARIA"] != DBNull.Value) entidad.idctabancaria = Convert.ToInt32(resultado["IDCTABANCARIA"]);
                            if (resultado["COD_BANCO"] != DBNull.Value) entidad.cod_banco = Convert.ToInt32(resultado["COD_BANCO"]);
                            if (resultado["NUM_CUENTA"] != DBNull.Value) entidad.num_cuenta = Convert.ToString(resultado["NUM_CUENTA"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["COB_COMISION"] != DBNull.Value) entidad.cob_comision = Convert.ToInt32(resultado["COB_COMISION"]);
                            //DATOS DEL IDCTABANCARIA
                            if (resultado["BANCO_GIRO"] != DBNull.Value) entidad.cod_banco1 = Convert.ToInt32(resultado["BANCO_GIRO"]);
                            if (resultado["CUENTA_GIRO"] != DBNull.Value) entidad.num_referencia1 = Convert.ToString(resultado["CUENTA_GIRO"]);
                        }
                       
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroData", "ConsultarGiro", ex);
                        return null;
                    }
                }
            }
        }



        public Giro Crear_ModGiro(Giro pGiro, Usuario vUsuario, int opcion)
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
                        if (opcion == 1)
                            pidgiro.Direction = ParameterDirection.Output;
                        else
                            pidgiro.Direction = ParameterDirection.Input;
                        pidgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pGiro.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pGiro.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter ptipo_acto = cmdTransaccionFactory.CreateParameter();
                        ptipo_acto.ParameterName = "p_tipo_acto";
                        ptipo_acto.Value = pGiro.tipo_acto;
                        ptipo_acto.Direction = ParameterDirection.Input;
                        ptipo_acto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_acto);

                        DbParameter pfec_reg = cmdTransaccionFactory.CreateParameter();
                        pfec_reg.ParameterName = "p_fec_reg";
                        pfec_reg.Value = pGiro.fec_reg;
                        pfec_reg.Direction = ParameterDirection.Input;
                        pfec_reg.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_reg);

                        DbParameter pfec_giro = cmdTransaccionFactory.CreateParameter();
                        pfec_giro.ParameterName = "p_fec_giro";
                        if (pGiro.fec_giro != DateTime.MinValue && pGiro.fec_giro != null) pfec_giro.Value = pGiro.fec_giro; else pfec_giro.Value = DBNull.Value;
                        pfec_giro.Direction = ParameterDirection.Input;
                        pfec_giro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_giro);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        if (pGiro.numero_radicacion != null) pnumero_radicacion.Value = pGiro.numero_radicacion; else pnumero_radicacion.Value = DBNull.Value;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pGiro.cod_ope != null) pcod_ope.Value = pGiro.cod_ope; else pcod_ope.Value = DBNull.Value;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnum_comp = cmdTransaccionFactory.CreateParameter();
                        pnum_comp.ParameterName = "p_num_comp";
                        if (pGiro.num_comp != null) pnum_comp.Value = pGiro.num_comp; else pnum_comp.Value = DBNull.Value;
                        pnum_comp.Direction = ParameterDirection.Input;
                        pnum_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_comp);

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "p_tipo_comp";
                        if (pGiro.tipo_comp != null) ptipo_comp.Value = pGiro.tipo_comp; else ptipo_comp.Value = DBNull.Value;
                        ptipo_comp.Direction = ParameterDirection.Input;
                        ptipo_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);

                        DbParameter pusu_gen = cmdTransaccionFactory.CreateParameter();
                        pusu_gen.ParameterName = "p_usu_gen";
                        if (pGiro.usu_gen != null && pGiro.usu_gen != "") pusu_gen.Value = pGiro.usu_gen; else pusu_gen.Value = DBNull.Value;
                        pusu_gen.Direction = ParameterDirection.Input;
                        pusu_gen.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_gen);

                        DbParameter pusu_apli = cmdTransaccionFactory.CreateParameter();
                        pusu_apli.ParameterName = "p_usu_apli";
                        if (pGiro.usu_apli != null && pGiro.usu_apli != "") pusu_apli.Value = pGiro.usu_apli; else pusu_apli.Value = DBNull.Value;
                        pusu_apli.Direction = ParameterDirection.Input;
                        pusu_apli.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_apli);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pGiro.estado != null)
                            pestado.Value = pGiro.estado;
                        else
                            pestado.Value = "1";
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusu_apro = cmdTransaccionFactory.CreateParameter();
                        pusu_apro.ParameterName = "p_usu_apro";
                        if (pGiro.usu_apro != null && pGiro.usu_apro != "") pusu_apro.Value = pGiro.usu_apro; else pusu_apro.Value = DBNull.Value;
                        pusu_apro.Direction = ParameterDirection.Input;
                        pusu_apro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_apro);

                        DbParameter pfec_apro = cmdTransaccionFactory.CreateParameter();
                        pfec_apro.ParameterName = "p_fec_apro";
                        if (pGiro.fec_apro != DateTime.MinValue && pGiro.fec_apro != null) pfec_apro.Value = pGiro.fec_apro; else pfec_apro.Value = DBNull.Value;
                        pfec_apro.Direction = ParameterDirection.Input;
                        pfec_apro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_apro);

                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        if (pGiro.idctabancaria != 0) pidctabancaria.Value = pGiro.idctabancaria; else pidctabancaria.Value = DBNull.Value;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        if (pGiro.cod_banco != 0 && pGiro.cod_banco != null) pcod_banco.Value = pGiro.cod_banco; else pcod_banco.Value = DBNull.Value;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pnum_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnum_cuenta.ParameterName = "p_num_cuenta";
                        if (pGiro.num_cuenta != "" && pGiro.num_cuenta != null) pnum_cuenta.Value = pGiro.num_cuenta; else pnum_cuenta.Value = DBNull.Value;
                        pnum_cuenta.Direction = ParameterDirection.Input;
                        pnum_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_cuenta);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        if (pGiro.tipo_cuenta != -1 && pGiro.tipo_cuenta != null) ptipo_cuenta.Value = pGiro.tipo_cuenta; else ptipo_cuenta.Value = DBNull.Value;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pcob_comision = cmdTransaccionFactory.CreateParameter();
                        pcob_comision.ParameterName = "p_cob_comision";
                        if (pGiro.cob_comision != 0 && pGiro.cob_comision != null) pcob_comision.Value = pGiro.cob_comision; else pcob_comision.Value = DBNull.Value;
                        pcob_comision.Direction = ParameterDirection.Input;
                        pcob_comision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcob_comision);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pGiro.valor != null) pvalor.Value = pGiro.valor; else pvalor.Value = DBNull.Value;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GIRO_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GIRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        if (opcion == 1)
                            pGiro.idgiro = Convert.ToInt32(pidgiro.Value);
                        return pGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "CrearGiro", ex);
                        return null;
                    }
                }
            }
        }


        public void AnularGiro(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Giro pGiro = new Giro();
                        pGiro = ConsultarGiro(pId.ToString(), vUsuario);

                        DbParameter pidgiro = cmdTransaccionFactory.CreateParameter();
                        pidgiro.ParameterName = "p_idgiro";
                        pidgiro.Value = pGiro.idgiro;
                        pidgiro.Direction = ParameterDirection.Input;
                        pidgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_GIRO_ANULAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroData", "AnularGiro", ex);
                    }
                }
            }
        }

        public List<Giro> ConciliarGiro(Giro pGiro, string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Giro> lstGiro = new List<Giro>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"With girosE As (
                                        Select g.idgiro, g.cod_persona, g.tipo_acto, g.fec_giro, g.cod_ope, g.usu_apro, g.valor, 
                                        o.num_comp, o.tipo_comp, d.cod_cuenta, d.detalle, d.tipo, d.valor As valor_comprobante From giro g 
                                        Left Join operacion o On g.cod_ope = o.cod_ope
                                        Left Join d_comprobante d On d.num_comp = o.num_comp And d.tipo_comp = o.tipo_comp And d.cod_cuenta Like '241595%'),
                                            girosR As (
                                        Select r.*, d.cod_cuenta, d.valor From giro_realizado r 
                                        Left Join operacion ox On r.cod_ope = ox.cod_ope 
                                        Left Join d_comprobante d On d.num_comp = ox.num_comp And d.tipo_comp = ox.tipo_comp And d.cod_cuenta Like '241595%'
                                            And Trim(Substr(d.detalle, Instr(d.detalle, ' ', 1, 1)+1, Instr(d.detalle, ' ', 1, 2)-Instr(d.detalle, ' '))) = To_Char(r.idgiro))
                                        Select g.idgiro, g.cod_persona, g.tipo_acto, g.fec_giro, g.cod_ope, g.usu_apro, g.valor, 
                                        g.num_comp, g.tipo_comp, g.cod_cuenta, g.detalle, g.tipo, g.valor_comprobante,
                                        r.cod_ope As cod_ope_realiza, r.fec_realiza, r.usu_realiza, r.cod_cuenta As cod_cuenta_realiza, r.valor As valor_realiza, persona.identificacion,
                                        (Case persona.tipo_persona When 'N' Then QUITARESPACIOS(Substr(persona.primer_nombre || ' ' || persona.segundo_nombre || ' ' || persona.primer_apellido || ' ' || persona.segundo_apellido, 0, 240)) Else persona.razon_social End) nombre
                                        From girosE g Left Outer Join girosR r On g.idgiro = r.idgiro Left Join persona On g.cod_persona = persona.cod_persona
                                        Where g.cod_cuenta != r.cod_cuenta
                                        Order by 1";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Giro entidad = new Giro();
                            if (resultado["IDGIRO"] != DBNull.Value) entidad.idgiro = Convert.ToInt64(resultado["IDGIRO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_ACTO"] != DBNull.Value) entidad.tipo_acto = Convert.ToInt32(resultado["TIPO_ACTO"]);
                            if (resultado["FEC_GIRO"] != DBNull.Value) entidad.fec_giro = Convert.ToDateTime(resultado["FEC_GIRO"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["USU_APRO"] != DBNull.Value) entidad.usu_apro = Convert.ToString(resultado["USU_APRO"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["DETALLE"] != DBNull.Value) entidad.detalle = Convert.ToString(resultado["DETALLE"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["VALOR_COMPROBANTE"] != DBNull.Value) entidad.valor_comprobante = Convert.ToDecimal(resultado["VALOR_COMPROBANTE"]);
                            if (resultado["COD_OPE_REALIZA"] != DBNull.Value) entidad.cod_ope_realiza = Convert.ToInt64(resultado["COD_OPE_REALIZA"]);
                            if (resultado["FEC_REALIZA"] != DBNull.Value) entidad.fec_realiza = Convert.ToDateTime(resultado["FEC_REALIZA"]);
                            if (resultado["USU_REALIZA"] != DBNull.Value) entidad.usu_realiza = Convert.ToString(resultado["USU_REALIZA"]);
                            if (resultado["COD_CUENTA_REALIZA"] != DBNull.Value) entidad.cod_cuenta_realiza = Convert.ToString(resultado["COD_CUENTA_REALIZA"]);
                            if (resultado["VALOR_REALIZA"] != DBNull.Value) entidad.valor_realiza = Convert.ToDecimal(resultado["VALOR_REALIZA"]);

                            lstGiro.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GiroData", "ConciliarGiro", ex);
                        return null;
                    }
                }
            }
        }

        public Giro CrearGiro(Giro pGiro, Usuario vUsuario, int opcion)
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
                        if (opcion == 1)
                            pidgiro.Direction = ParameterDirection.Output;
                        else
                            pidgiro.Direction = ParameterDirection.Input;
                        pidgiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidgiro);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pGiro.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pGiro.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter ptipo_acto = cmdTransaccionFactory.CreateParameter();
                        ptipo_acto.ParameterName = "p_tipo_acto";
                        ptipo_acto.Value = pGiro.tipo_acto;
                        ptipo_acto.Direction = ParameterDirection.Input;
                        ptipo_acto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_acto);

                        DbParameter pfec_reg = cmdTransaccionFactory.CreateParameter();
                        pfec_reg.ParameterName = "p_fec_reg";
                        pfec_reg.Value = pGiro.fec_reg;
                        pfec_reg.Direction = ParameterDirection.Input;
                        pfec_reg.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_reg);

                        DbParameter pfec_giro = cmdTransaccionFactory.CreateParameter();
                        pfec_giro.ParameterName = "p_fec_giro";
                        if (pGiro.fec_giro != DateTime.MinValue) pfec_giro.Value = pGiro.fec_giro; else pfec_giro.Value = DBNull.Value;
                        pfec_giro.Direction = ParameterDirection.Input;
                        pfec_giro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_giro);

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "p_numero_radicacion";
                        if (pGiro.numero_radicacion != 0) pnumero_radicacion.Value = pGiro.numero_radicacion; else pnumero_radicacion.Value = DBNull.Value;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pGiro.cod_ope != 0 && pGiro.cod_ope != null) pcod_ope.Value = pGiro.cod_ope; else pcod_ope.Value = DBNull.Value;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnum_comp = cmdTransaccionFactory.CreateParameter();
                        pnum_comp.ParameterName = "p_num_comp";
                        pnum_comp.Value = -1;
                        pnum_comp.Direction = ParameterDirection.Input;
                        pnum_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnum_comp);

                        DbParameter ptipo_comp = cmdTransaccionFactory.CreateParameter();
                        ptipo_comp.ParameterName = "p_tipo_comp";
                        if (pGiro.tipo_comp != 0 && pGiro.tipo_comp != null) ptipo_comp.Value = pGiro.tipo_comp; else ptipo_comp.Value = DBNull.Value;
                        ptipo_comp.Direction = ParameterDirection.Input;
                        ptipo_comp.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_comp);

                        DbParameter pusu_gen = cmdTransaccionFactory.CreateParameter();
                        pusu_gen.ParameterName = "p_usu_gen";
                        pusu_gen.Value = pGiro.usu_gen;
                        pusu_gen.Direction = ParameterDirection.Input;
                        pusu_gen.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_gen);

                        DbParameter pusu_apli = cmdTransaccionFactory.CreateParameter();
                        pusu_apli.ParameterName = "p_usu_apli";
                        if (pGiro.usu_apli != null) pusu_apli.Value = pGiro.usu_apli; else pusu_apli.Value = DBNull.Value;
                        pusu_apli.Direction = ParameterDirection.Input;
                        pusu_apli.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_apli);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pGiro.estadogi;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pusu_apro = cmdTransaccionFactory.CreateParameter();
                        pusu_apro.ParameterName = "p_usu_apro";
                        if (pGiro.usu_apro != null) pusu_apro.Value = pGiro.usu_apro; else pusu_apro.Value = DBNull.Value;
                        pusu_apro.Direction = ParameterDirection.Input;
                        pusu_apro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pusu_apro);

                        DbParameter pidctabancaria = cmdTransaccionFactory.CreateParameter();
                        pidctabancaria.ParameterName = "p_idctabancaria";
                        if (pGiro.idctabancaria != 0) pidctabancaria.Value = pGiro.idctabancaria; else pidctabancaria.Value = DBNull.Value;
                        pidctabancaria.Direction = ParameterDirection.Input;
                        pidctabancaria.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidctabancaria);

                        DbParameter pcod_banco = cmdTransaccionFactory.CreateParameter();
                        pcod_banco.ParameterName = "p_cod_banco";
                        if (pGiro.cod_banco != 0) pcod_banco.Value = pGiro.cod_banco; else pcod_banco.Value = DBNull.Value;
                        pcod_banco.Direction = ParameterDirection.Input;
                        pcod_banco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_banco);

                        DbParameter pnum_cuenta = cmdTransaccionFactory.CreateParameter();
                        pnum_cuenta.ParameterName = "p_num_cuenta";
                        if (pGiro.num_cuenta != null) pnum_cuenta.Value = pGiro.num_cuenta; else pnum_cuenta.Value = DBNull.Value;
                        pnum_cuenta.Direction = ParameterDirection.Input;
                        pnum_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_cuenta);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        if (pGiro.tipo_cuenta != -1) ptipo_cuenta.Value = pGiro.tipo_cuenta; else ptipo_cuenta.Value = DBNull.Value;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pfec_apro = cmdTransaccionFactory.CreateParameter();
                        pfec_apro.ParameterName = "p_fec_apro";
                        if (pGiro.fec_apro != DateTime.MinValue) pfec_apro.Value = pGiro.fec_apro; else pfec_apro.Value = DBNull.Value;
                        pfec_apro.Direction = ParameterDirection.Input;
                        pfec_apro.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfec_apro);

                        DbParameter pcob_comision = cmdTransaccionFactory.CreateParameter();
                        pcob_comision.ParameterName = "p_cob_comision";
                        if (pGiro.cob_comision != 0) pcob_comision.Value = pGiro.cob_comision; else pcob_comision.Value = DBNull.Value;
                        pcob_comision.Direction = ParameterDirection.Input;
                        pcob_comision.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcob_comision);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pGiro.valor != 0) pvalor.Value = pGiro.valor; else pvalor.Value = DBNull.Value;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GIRO_CREAR";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_GIRO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        if (opcion == 1)
                            pGiro.idgiro = Convert.ToInt32(pidgiro.Value);
                        return pGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AvanceData", "CrearGiro", ex);
                        return null;
                    }
                }
            }
        }

        public Giro ModificarGiroXCod_ope(Giro pGiro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        Configuracion conf = new Configuracion();
                        sql = "update giro   set  fec_apro = To_Date('" + pGiro.fec_apro_giro.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ,"
                          + " usu_apro = '" + pGiro.usu_apro + "', valor = " + pGiro.valor + " , estado = 2 , forma_pago = " + pGiro.forma_pago
                          + " where cod_ope= " + pGiro.cod_ope;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CuentasPorPagarData", "ModificarGiroXCod_ope", ex);
                        return null;
                    }
                }
            }
        }
    }
}
