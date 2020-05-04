using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ParametroCtasObligacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public ParametroCtasObligacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<Par_Cue_Obligacion> ListarParametrosCtasOBLI(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Par_Cue_Obligacion> lstConsulta = new List<Par_Cue_Obligacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select p.idparametro,o.nombrelinea ,p.cod_cuenta,pc.nombre as nomCuenta, "
                                        +"case p.tipo_mov when 1 then 'Débito' when 2 then 'Crédito' end as nom_tipo_mov, "
                                        + "e.detalle as nomestructura,b.nombrebanco,c.nombre as nomcomponente "
                                        +"from PAR_CUE_OBLIGACION p left join estructura_detalle e on p.cod_est_det = e.cod_est_det "
                                        +"left join plan_cuentas pc on pc.cod_cuenta = p.cod_cuenta "
                                        +"left join OBLINEAOBLIGACION o on o.codlineaobligacion = p.codlineaobligacion "
                                        +"left join bancos b on b.cod_banco = p.codentidad "
                                        + "left join obcomponente c on c.codcomponente = p.codcomponente where 1 = 1 "
                                        + filtro + " order by p.idparametro";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Par_Cue_Obligacion entidad = new Par_Cue_Obligacion();
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                            if (resultado["nombrelinea"] != DBNull.Value) entidad.nombrelinea = Convert.ToString(resultado["nombrelinea"]);
                            if (resultado["cod_cuenta"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["cod_cuenta"]);
                            if (resultado["nomCuenta"] != DBNull.Value) entidad.nomCuenta = Convert.ToString(resultado["nomCuenta"]);
                            if (resultado["nomestructura"] != DBNull.Value) entidad.nomestructura = Convert.ToString(resultado["nomestructura"]);
                            if (resultado["nom_tipo_mov"] != DBNull.Value) entidad.nom_tipo_mov = Convert.ToString(resultado["nom_tipo_mov"]);
                            if (resultado["nombrebanco"] != DBNull.Value) entidad.nombrebanco = Convert.ToString(resultado["nombrebanco"]);
                            if (resultado["nomcomponente"] != DBNull.Value) entidad.nombrecomponente = Convert.ToString(resultado["nomcomponente"]);
                            lstConsulta.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstConsulta;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasObligacionData", "ListarParametrosCtasOBLI", ex);
                        return null;
                    }
                }
            }
        }



        public Par_Cue_Obligacion ConsultarParametroCtasOBLI(Int32 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Par_Cue_Obligacion entidad = new Par_Cue_Obligacion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PAR_CUE_OBLIGACION WHERE IDPARAMETRO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPARAMETRO"] != DBNull.Value) entidad.idparametro = Convert.ToInt32(resultado["IDPARAMETRO"]);
                            if (resultado["CODLINEAOBLIGACION"] != DBNull.Value) entidad.codlineaobligacion = Convert.ToInt32(resultado["CODLINEAOBLIGACION"]);
                            if (resultado["CODCOMPONENTE"] != DBNull.Value) entidad.codcomponente = Convert.ToInt32(resultado["CODCOMPONENTE"]);
                            if (resultado["TIPO_CUENTA"] != DBNull.Value) entidad.tipo_cuenta = Convert.ToInt32(resultado["TIPO_CUENTA"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToInt32(resultado["TIPO"]);
                            if (resultado["CODENTIDAD"] != DBNull.Value) entidad.codentidad = Convert.ToInt32(resultado["CODENTIDAD"]);
                        }                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasObligacionData", "ConsultarParametroCtasOBLI", ex);
                        return null;
                    }
                }
            }
        }



        public Par_Cue_Obligacion CrearParamCtasObligacion(Par_Cue_Obligacion pParam, Usuario vUsuario,int opcion)
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
                        pidparametro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidparametro);

                        DbParameter pcodlineaobligacion = cmdTransaccionFactory.CreateParameter();
                        pcodlineaobligacion.ParameterName = "p_codlineaobligacion";
                        pcodlineaobligacion.Value = pParam.codlineaobligacion;
                        pcodlineaobligacion.Direction = ParameterDirection.Input;
                        pcodlineaobligacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodlineaobligacion);

                        DbParameter pcodcomponente = cmdTransaccionFactory.CreateParameter();
                        pcodcomponente.ParameterName = "p_codcomponente";
                        if (pParam.codcomponente != 0) pcodcomponente.Value = pParam.codcomponente; else pcodcomponente.Value = DBNull.Value;
                        pcodcomponente.Direction = ParameterDirection.Input;
                        pcodcomponente.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodcomponente);

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
                        pcod_cuenta.Value = pParam.cod_cuenta;
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

                        DbParameter pcodentidad = cmdTransaccionFactory.CreateParameter();
                        pcodentidad.ParameterName = "p_codentidad";
                        if (pParam.codentidad != 0) pcodentidad.Value = pParam.codentidad; else pcodentidad.Value = DBNull.Value;
                        pcodentidad.Direction = ParameterDirection.Input;
                        pcodentidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodentidad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (opcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PARCTAOBLI_CREAR"; // CREAR
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PARCTAOBLI_MOD"; // MODIFICAR
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if (opcion == 1)
                            pParam.idparametro = Convert.ToInt32(pidparametro.Value);
                        return pParam;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasObligacionData", "CrearParamCtasObligacion", ex);
                        return null;
                    }
                }
            }
        }


        


        public void EliminarParametroCtasOBLI(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM PAR_CUE_OBLIGACION WHERE idparametro = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ParametroCtasObligacionData", "EliminarParametroCtasOBLI", ex);
                    }
                }
            }
        }



    }
}
