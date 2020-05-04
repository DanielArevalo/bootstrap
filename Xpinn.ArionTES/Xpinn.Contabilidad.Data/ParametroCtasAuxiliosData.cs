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
    public class ParametroCtasAuxiliosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public ParametroCtasAuxiliosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Par_Cue_LinAux CrearPar_Cue_LinAux(Par_Cue_LinAux pPar_Cue_LinAux, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pPar_Cue_LinAux.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pcod_linea_auxilio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_auxilio.ParameterName = "p_cod_linea_auxilio";
                        pcod_linea_auxilio.Value = pPar_Cue_LinAux.cod_linea_auxilio;
                        pcod_linea_auxilio.Direction = ParameterDirection.Input;
                        pcod_linea_auxilio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_auxilio);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pPar_Cue_LinAux.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        pcod_est_det.Value = pPar_Cue_LinAux.cod_est_det;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pPar_Cue_LinAux.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        if (pPar_Cue_LinAux.tipo_mov == null)
                            ptipo_mov.Value = DBNull.Value;
                        else
                            ptipo_mov.Value = pPar_Cue_LinAux.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pPar_Cue_LinAux.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pPar_Cue_LinAux.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pPar_Cue_LinAux.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pPar_Cue_LinAux.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_AU_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPar_Cue_LinAux;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinAuxData", "CrearPar_Cue_LinAux", ex);
                        return null;
                    }
                }
            }
        }

        public Par_Cue_LinAux ModificarPar_Cue_LinAux(Par_Cue_LinAux pPar_Cue_LinAux, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pPar_Cue_LinAux.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pcod_linea_auxilio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_auxilio.ParameterName = "p_cod_linea_auxilio";
                        pcod_linea_auxilio.Value = pPar_Cue_LinAux.cod_linea_auxilio;
                        pcod_linea_auxilio.Direction = ParameterDirection.Input;
                        pcod_linea_auxilio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_auxilio);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        pcod_atr.Value = pPar_Cue_LinAux.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pcod_est_det = cmdTransaccionFactory.CreateParameter();
                        pcod_est_det.ParameterName = "p_cod_est_det";
                        pcod_est_det.Value = pPar_Cue_LinAux.cod_est_det;
                        pcod_est_det.Direction = ParameterDirection.Input;
                        pcod_est_det.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_est_det);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pPar_Cue_LinAux.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter ptipo_mov = cmdTransaccionFactory.CreateParameter();
                        ptipo_mov.ParameterName = "p_tipo_mov";
                        if (pPar_Cue_LinAux.tipo_mov == null)
                            ptipo_mov.Value = DBNull.Value;
                        else
                            ptipo_mov.Value = pPar_Cue_LinAux.tipo_mov;
                        ptipo_mov.Direction = ParameterDirection.Input;
                        ptipo_mov.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_mov);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        if (pPar_Cue_LinAux.tipo == null)
                            ptipo.Value = DBNull.Value;
                        else
                            ptipo.Value = pPar_Cue_LinAux.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pPar_Cue_LinAux.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pPar_Cue_LinAux.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_AU_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pPar_Cue_LinAux;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinAuxData", "ModificarPar_Cue_LinAux", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarPar_Cue_LinAux(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Par_Cue_LinAux pPar_Cue_LinAux = new Par_Cue_LinAux();
                        pPar_Cue_LinAux = ConsultarPar_Cue_LinAux(pId, vUsuario);

                        DbParameter pidparametro = cmdTransaccionFactory.CreateParameter();
                        pidparametro.ParameterName = "p_idparametro";
                        pidparametro.Value = pPar_Cue_LinAux.idparametro;
                        pidparametro.Direction = ParameterDirection.Input;
                        pidparametro.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PAR_CUE_AU_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinAuxData", "EliminarPar_Cue_LinAux", ex);
                    }
                }
            }
        }


        public Par_Cue_LinAux ConsultarPar_Cue_LinAux(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Par_Cue_LinAux entidad = new Par_Cue_LinAux();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Par_Cue_LinAux WHERE IDPARAMETRO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilio = Convert.ToString(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
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
                        BOExcepcion.Throw("Par_Cue_LinAuxData", "ConsultarPar_Cue_LinAux", ex);
                        return null;
                    }
                }
            }
        }


        public List<Par_Cue_LinAux> ListarPar_Cue_LinAux(Par_Cue_LinAux pPar_Cue_LinAux, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_LinAux> lstPar_Cue_LinAux = new List<Par_Cue_LinAux>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select p.*, pc.nombre as nomCuenta, l.descripcion as nombrelinea, e.detalle as nomestructura, case p.tipo_mov when 1 then 'Débito' when 2 then 'Crédito' end as nom_tipo_mov, t.descripcion as nomtipo_tran 
                                        from PAR_CUE_LINAUX p 
                                        left join LINEASAUXILIOS l on l.cod_linea_Auxilio = p.cod_linea_Auxilio 
                                        left join ESTRUCTURA_DETALLE e on e.cod_est_det = p.cod_est_det 
                                        left join PLAN_CUENTAS pc on pc.cod_cuenta = p.cod_cuenta 
                                        left join TIPO_TRAN t on t.tipo_tran = p.tipo_tran " + ObtenerFiltro(pPar_Cue_LinAux, "p.")
                                        + "Order By p.IDPARAMETRO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_LinAux entidad = new Par_Cue_LinAux();
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["IDPARAMETRO"]);
                            if (resultado["COD_LINEA_AUXILIO"] != DBNull.Value) entidad.cod_linea_auxilio = Convert.ToString(resultado["COD_LINEA_AUXILIO"]);
                            if (resultado["NOMBRELINEA"] != DBNull.Value) entidad.nom_linea_auxilio = Convert.ToString(resultado["NOMBRELINEA"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["NOMESTRUCTURA"] != DBNull.Value) entidad.nomestructura = Convert.ToString(resultado["NOMESTRUCTURA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["nomCuenta"] != DBNull.Value) entidad.nom_cuenta = Convert.ToString(resultado["nomCuenta"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["NOM_TIPO_MOV"] != DBNull.Value) entidad.nom_tipo_mov = Convert.ToString(resultado["NOM_TIPO_MOV"]);
                            if (resultado["nomtipo_tran"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["nomtipo_tran"]);
                            lstPar_Cue_LinAux.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPar_Cue_LinAux;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Par_Cue_LinAuxData", "ListarPar_Cue_LinAux", ex);
                        return null;
                    }
                }
            }
        }

        public List<Par_Cue_LinAux> ListarLineaSAuxilio(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_LinAux> lstLineaAuxilio = new List<Par_Cue_LinAux>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT pc.nombre as nomCuenta, p.idparametro, l.descripcion as nombrelinea, e.detalle as nomestructura, p.cod_cuenta, case p.tipo_mov when 1 then 'Débito' when 2 then 'Crédito' end as nom_tipo_mov, t.descripcion as nomtipo_tran 
                                        from PAR_CUE_LINAUX p 
                                        left join LINEASAUXILIOS l on l.cod_linea_Auxilio = p.cod_linea_Auxilio 
                                        left join ESTRUCTURA_DETALLE e on e.cod_est_det = p.cod_est_det 
                                        left join PLAN_CUENTAS pc on pc.cod_cuenta = p.cod_cuenta 
                                        left join TIPO_TRAN t on t.tipo_tran = p.tipo_tran where 1 = 1 " + filtro
                                        + "ORDER BY p.IDPARAMETRO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_LinAux entidad = new Par_Cue_LinAux();
                            if (resultado["idparametro"] != DBNull.Value) entidad.idparametro = Convert.ToInt64(resultado["idparametro"]);
                            if (resultado["nombrelinea"] != DBNull.Value) entidad.nom_linea_auxilio = Convert.ToString(resultado["nombrelinea"]);
                            if (resultado["nomestructura"] != DBNull.Value) entidad.nomestructura = Convert.ToString(resultado["nomestructura"]);
                            if (resultado["cod_Cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_Cuenta"]);
                            if (resultado["nomCuenta"] != DBNull.Value) entidad.nom_cuenta = Convert.ToString(resultado["nomCuenta"]);
                            if (resultado["nom_tipo_mov"] != DBNull.Value) entidad.nom_tipo_mov = Convert.ToString(resultado["nom_tipo_mov"]);
                            if (resultado["nomtipo_tran"] != DBNull.Value) entidad.nom_tipo_tran = Convert.ToString(resultado["nomtipo_tran"]);
                            lstLineaAuxilio.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLineaAuxilio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineaAuxilioData", "ListarLineaAuxilio", ex);
                        return null;
                    }
                }
            }
        }


    }
}
