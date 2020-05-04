using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para PlanCuentas
    /// </summary>    
    public class ParametroCtasAportesData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public ParametroCtasAportesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<Par_Cue_LinApo> ListarPar_Cue_LinApo(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_LinApo> lstConsulta = new List<Par_Cue_LinApo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select p.idparametro, l.nombre as nombrelinea, e.detalle as nomestructura, p.cod_cuenta, pc.nombre as nomCuenta, pcn.nombre as nomCuentaNiif, "
                                        + "case p.tipo_mov when 1 then 'Débito' when 2 then 'Crédito' end as nom_tipo_mov, t.descripcion as nomtipo_tran "
                                        + "from PAR_CUE_LINAPO p left join lineaaporte l on l.cod_linea_aporte = p.cod_linea_aporte "
                                        + "left join ESTRUCTURA_DETALLE e on e.cod_est_det = p.cod_est_det "
                                        + "left join plan_cuentas pc on pc.cod_cuenta = p.cod_cuenta "
                                        + "left join plan_cuentas_niif pcn on pcn.cod_cuenta_niif = p.cod_cuenta_niif "
                                        + "left join tipo_tran t on t.tipo_tran = p.tipo_tran ";
                        if (filtro.Trim() != "")
                            sql += filtro.ToUpper().Contains("WHERE") ? " " + filtro : " WHERE 1=1 " + filtro;
                        sql += " ORDER BY p.IDPARAMETRO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_LinApo entidad = new Par_Cue_LinApo();
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["nombrelinea"] != DBNull.Value) entidad.nombrelinea = Convert.ToString(resultado["nombrelinea"]);
                            if (resultado["nomestructura"] != DBNull.Value) entidad.nomestructura = Convert.ToString(resultado["nomestructura"]);
                            if (resultado["cod_Cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_Cuenta"]);
                            if (resultado["nomCuenta"] != DBNull.Value) entidad.nomCuenta = Convert.ToString(resultado["nomCuenta"]);
                            if (resultado["nomCuentaNiif"] != DBNull.Value) entidad.nomCuentaNiif = Convert.ToString(resultado["nomCuentaNiif"]);
                            if (resultado["nom_tipo_mov"] != DBNull.Value) entidad.nom_tipo_mov = Convert.ToString(resultado["nom_tipo_mov"]);
                            if (resultado["nomtipo_tran"] != DBNull.Value) entidad.nomtipo_tran = Convert.ToString(resultado["nomtipo_tran"]);
                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinApoData", "ListarPar_Cue_LinApo", ex);
                        return null;
                    }
                }
            }
        }



        public Par_Cue_LinApo ConsultarPar_Cue_LinApo(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Par_Cue_LinApo entidad = new Par_Cue_LinApo();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PAR_CUE_LINAPO WHERE IDPARAMETRO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea_aporte = Convert.ToInt32(resultado["COD_LINEA_APORTE"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta_niif = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinApoData", "ConsultarPar_Cue_LinApo", ex);
                        return null;
                    }
                }
            }
        }





        public Par_Cue_LinApo CrearParametroAporte(Par_Cue_LinApo pParam, Usuario vUsuario, int opcion)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pParam.idparametro;
                        if (opcion == 1)
                            pidparametro.Direction = ParameterDirection.Output;
                        else
                            pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pcod_linea_aporte = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_aporte.ParameterName = "p_cod_linea_aporte";
                        pcod_linea_aporte.Value = pParam.cod_linea_aporte;
                        pcod_linea_aporte.Direction = ParameterDirection.Input;
                        pcod_linea_aporte.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_aporte);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pParam.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter ptipo_cuenta = cmdTransaccionFactory.CreateParameter();
                        ptipo_cuenta.ParameterName = "p_tipo_cuenta";
                        ptipo_cuenta.Value = pParam.tipo_cuenta;
                        ptipo_cuenta.Direction = ParameterDirection.Input;
                        ptipo_cuenta.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_cuenta);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        if (pParam.cod_est_det != 0) pcod_est_det.Value = pParam.cod_est_det; else pcod_est_det.Value = DBNull.Value;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        if (pParam.cod_cuenta != null) pcod_cuenta.Value = pParam.cod_cuenta; else pcod_cuenta.Value = DBNull.Value;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        ptipo_mov.Value = pParam.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pParam.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pParam.tipo_tran != 0) ptipo_tran.Value = pParam.tipo_tran; else ptipo_tran.Value = DBNull.Value;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pcod_cuenta_niif = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_niif.ParameterName = "p_cod_cuenta_niif";
                        if (pParam.cod_cuenta_niif != null) pcod_cuenta_niif.Value = pParam.cod_cuenta_niif; else pcod_cuenta_niif.Value = DBNull.Value;
                        pcod_cuenta_niif.Direction = ParameterDirection.Input;
                        pcod_cuenta_niif.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_niif);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if(opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PARAMAPORT_CREAR"; // CREAR
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PARAMAPORT_MOD"; // MODIFICAR
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if(opcion == 1)
                            pParam.idparametro = Convert.ToInt64(pidparametro.Value);
                        return pParam;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinApoData", "CrearParametroAporte", ex);
                        return null;
                    }
                }
            }
        }



        public void EliminarParametroAporte(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pId;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PARAMAPORT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinApoData", "EliminarParametroAporte", ex);
                    }
                }
            }
        }



    }
}
