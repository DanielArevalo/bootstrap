using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    public class ProcesoTipoGiroData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ProcesoTipoGiroData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public ProcesoTipoGiro CrearProcesoTipoGiro(ProcesoTipoGiro pProcesoTipoGiro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidprocesogiro = cmdTransaccionFactory.CreateParameter();
                        pidprocesogiro.ParameterName = "p_idprocesogiro";
                        pidprocesogiro.Value = pProcesoTipoGiro.idprocesogiro;
                        pidprocesogiro.Direction = ParameterDirection.Output;
                        pidprocesogiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidprocesogiro);

                        DbParameter pcod_proceso = cmdTransaccionFactory.CreateParameter();
                        pcod_proceso.ParameterName = "p_cod_proceso";
                        pcod_proceso.Value = pProcesoTipoGiro.cod_proceso;
                        pcod_proceso.Direction = ParameterDirection.Input;
                        pcod_proceso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_proceso);

                        DbParameter ptipo_acto = cmdTransaccionFactory.CreateParameter();
                        ptipo_acto.ParameterName = "p_tipo_acto";
                        ptipo_acto.Value = pProcesoTipoGiro.tipo_acto;
                        ptipo_acto.Direction = ParameterDirection.Input;
                        ptipo_acto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_acto);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pProcesoTipoGiro.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_PROCESO_TI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pProcesoTipoGiro.idprocesogiro = Convert.ToInt32(pidprocesogiro.Value);

                        dbConnectionFactory.CerrarConexion(connection);
                        return pProcesoTipoGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoTipoGiroData", "CrearProcesoTipoGiro", ex);
                        return null;
                    }
                }
            }
        }


        public ProcesoTipoGiro ModificarProcesoTipoGiro(ProcesoTipoGiro pProcesoTipoGiro, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidprocesogiro = cmdTransaccionFactory.CreateParameter();
                        pidprocesogiro.ParameterName = "p_idprocesogiro";
                        pidprocesogiro.Value = pProcesoTipoGiro.idprocesogiro;
                        pidprocesogiro.Direction = ParameterDirection.Input;
                        pidprocesogiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidprocesogiro);

                        DbParameter pcod_proceso = cmdTransaccionFactory.CreateParameter();
                        pcod_proceso.ParameterName = "p_cod_proceso";
                        pcod_proceso.Value = pProcesoTipoGiro.cod_proceso;
                        pcod_proceso.Direction = ParameterDirection.Input;
                        pcod_proceso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_proceso);

                        DbParameter ptipo_acto = cmdTransaccionFactory.CreateParameter();
                        ptipo_acto.ParameterName = "p_tipo_acto";
                        ptipo_acto.Value = pProcesoTipoGiro.tipo_acto;
                        ptipo_acto.Direction = ParameterDirection.Input;
                        ptipo_acto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_acto);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pProcesoTipoGiro.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_PROCESO_TI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProcesoTipoGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoTipoGiroData", "ModificarProcesoTipoGiro", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarProcesoTipoGiro(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ProcesoTipoGiro pProcesoTipoGiro = new ProcesoTipoGiro();
                        pProcesoTipoGiro = ConsultarProcesoTipoGiro(pId, vUsuario);

                        DbParameter pidprocesogiro = cmdTransaccionFactory.CreateParameter();
                        pidprocesogiro.ParameterName = "p_idprocesogiro";
                        pidprocesogiro.Value = pProcesoTipoGiro.idprocesogiro;
                        pidprocesogiro.Direction = ParameterDirection.Input;
                        pidprocesogiro.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidprocesogiro);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_PROCESO_TI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoTipoGiroData", "EliminarProcesoTipoGiro", ex);
                    }
                }
            }
        }


        public ProcesoTipoGiro ConsultarProcesoTipoGiro(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ProcesoTipoGiro entidad = new ProcesoTipoGiro();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Proceso_Tipo_Giro WHERE IDPROCESOGIRO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDPROCESOGIRO"] != DBNull.Value) entidad.idprocesogiro = Convert.ToInt32(resultado["IDPROCESOGIRO"]);
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt32(resultado["COD_PROCESO"]);
                            if (resultado["TIPO_ACTO"] != DBNull.Value) entidad.tipo_acto = Convert.ToInt32(resultado["TIPO_ACTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
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
                        BOExcepcion.Throw("ProcesoTipoGiroData", "ConsultarProcesoTipoGiro", ex);
                        return null;
                    }
                }
            }
        }


        public List<ProcesoTipoGiro> ListarProcesoTipoGiro(ProcesoTipoGiro pProcesoTipoGiro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ProcesoTipoGiro> lstProcesoTipoGiro = new List<ProcesoTipoGiro>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT proc.idprocesogiro, proc.cod_proceso, CASE p.tipo_ope WHEN 149 THEN proc.forma_pago ELSE proc.tipo_acto END AS tipo_acto, plan.cod_cuenta, plan.nombre 
                                        FROM Proceso_Contable p JOIN Proceso_Tipo_Giro proc ON p.cod_proceso = proc.cod_proceso JOIN plan_cuentas plan ON proc.cod_cuenta = plan.cod_cuenta " + ObtenerFiltro(pProcesoTipoGiro, "proc.") + " ORDER BY proc.IDPROCESOGIRO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProcesoTipoGiro entidad = new ProcesoTipoGiro();
                            if (resultado["IDPROCESOGIRO"] != DBNull.Value) entidad.idprocesogiro = Convert.ToInt32(resultado["IDPROCESOGIRO"]);
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt32(resultado["COD_PROCESO"]);
                            if (resultado["TIPO_ACTO"] != DBNull.Value) entidad.tipo_acto = Convert.ToInt32(resultado["TIPO_ACTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.descripcion_cod_cuenta = Convert.ToString(resultado["NOMBRE"]);
                            lstProcesoTipoGiro.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProcesoTipoGiro;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoTipoGiroData", "ListarProcesoTipoGiro", ex);
                        return null;
                    }
                }
            }
        }
    }
}