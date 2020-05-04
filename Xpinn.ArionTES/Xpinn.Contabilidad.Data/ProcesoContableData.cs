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
    public class ProcesoContableData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ProcesoContableData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public ProcesoContable CrearProcesoContable(ProcesoContable pProceso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "p_cod_proceso";
                        p_cod_proceso.Value = 0;
                        p_cod_proceso.Direction = ParameterDirection.InputOutput;

                        DbParameter p_tipo_ope = cmdTransaccionFactory.CreateParameter();
                        p_tipo_ope.ParameterName = "p_tipo_ope";
                        p_tipo_ope.Value = pProceso.tipo_ope;

                        DbParameter p_tipo_comp = cmdTransaccionFactory.CreateParameter();
                        p_tipo_comp.ParameterName = "p_tipo_comp";
                        p_tipo_comp.Value = pProceso.tipo_comp;

                        DbParameter p_fecha_inicial = cmdTransaccionFactory.CreateParameter();
                        p_fecha_inicial.ParameterName = "p_fecha_inicial";
                        p_fecha_inicial.DbType = DbType.Date;
                        p_fecha_inicial.Value = pProceso.fecha_inicial;

                        DbParameter p_fecha_final = cmdTransaccionFactory.CreateParameter();
                        p_fecha_final.ParameterName = "p_fecha_final";
                        p_fecha_final.DbType = DbType.Date;
                        p_fecha_final.Value = pProceso.fecha_final;

                        DbParameter P_concepto = cmdTransaccionFactory.CreateParameter();
                        P_concepto.ParameterName = "P_concepto";
                        if (pProceso.concepto == null)
                            P_concepto.Value = DBNull.Value;
                        else
                            P_concepto.Value = pProceso.concepto;

                        DbParameter p_cod_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cod_cuenta.ParameterName = "p_cod_cuenta";
                        p_cod_cuenta.Value = pProceso.cod_cuenta;

                        DbParameter p_cod_est_det = cmdTransaccionFactory.CreateParameter();
                        p_cod_est_det.ParameterName = "p_cod_est_det";
                        if (pProceso.cod_est_det == null)
                            p_cod_est_det.Value = DBNull.Value;
                        else
                            p_cod_est_det.Value = pProceso.cod_est_det;

                        DbParameter p_tipo_mov = cmdTransaccionFactory.CreateParameter();
                        p_tipo_mov.ParameterName = "p_tipo_mov";
                        if (pProceso.tipo_mov == null)
                            p_tipo_mov.Value = DBNull.Value;
                        else
                            p_tipo_mov.Value = pProceso.tipo_mov;
                        p_tipo_mov.DbType = DbType.Int32;
                        
                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_ope);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_comp);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_inicial);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_final);
                        cmdTransaccionFactory.Parameters.Add(P_concepto);
                        cmdTransaccionFactory.Parameters.Add(p_cod_cuenta);
                        cmdTransaccionFactory.Parameters.Add(p_cod_est_det);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_mov);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PROCESO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pProceso, "PROCESO_CONTABLE", vUsuario, Accion.Crear.ToString()); 

                        pProceso.cod_proceso = Convert.ToInt64(p_cod_proceso.Value);
                        dbConnectionFactory.CerrarConexion(connection);
                        return pProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoContableData", "CrearProcesoContable", ex);
                        return null;
                    }
                }
            }
        }


        public ProcesoContable ModificarProcesoContable(ProcesoContable pProceso, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_proceso = cmdTransaccionFactory.CreateParameter();
                        p_cod_proceso.ParameterName = "p_cod_proceso";
                        p_cod_proceso.Value = pProceso.cod_proceso;
                        p_cod_proceso.Direction = ParameterDirection.Input;

                        DbParameter p_tipo_ope = cmdTransaccionFactory.CreateParameter();
                        p_tipo_ope.ParameterName = "p_tipo_ope";
                        p_tipo_ope.Value = pProceso.tipo_ope;

                        DbParameter p_tipo_comp = cmdTransaccionFactory.CreateParameter();
                        p_tipo_comp.ParameterName = "p_tipo_comp";
                        p_tipo_comp.Value = pProceso.tipo_comp;

                        DbParameter p_fecha_inicial = cmdTransaccionFactory.CreateParameter();
                        p_fecha_inicial.ParameterName = "p_fecha_inicial";
                        p_fecha_inicial.DbType = DbType.Date;
                        p_fecha_inicial.Value = pProceso.fecha_inicial;

                        DbParameter p_fecha_final = cmdTransaccionFactory.CreateParameter();
                        p_fecha_final.ParameterName = "p_fecha_final";
                        p_fecha_final.DbType = DbType.Date;
                        p_fecha_final.Value = pProceso.fecha_final;

                        DbParameter P_concepto = cmdTransaccionFactory.CreateParameter();
                        P_concepto.ParameterName = "P_concepto";
                        P_concepto.Value = pProceso.concepto;

                        DbParameter p_cod_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cod_cuenta.ParameterName = "p_cod_cuenta";
                        p_cod_cuenta.Value = pProceso.cod_cuenta;

                        DbParameter p_cod_est_det = cmdTransaccionFactory.CreateParameter();
                        p_cod_est_det.ParameterName = "p_cod_est_det";
                        p_cod_est_det.Value = pProceso.cod_est_det;

                        DbParameter p_tipo_mov = cmdTransaccionFactory.CreateParameter();
                        p_tipo_mov.ParameterName = "p_tipo_mov";
                        p_tipo_mov.DbType = DbType.Int32;
                        p_tipo_mov.Value = pProceso.tipo_mov;

                        cmdTransaccionFactory.Parameters.Add(p_cod_proceso);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_ope);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_comp);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_inicial);
                        cmdTransaccionFactory.Parameters.Add(p_fecha_final);
                        cmdTransaccionFactory.Parameters.Add(P_concepto);
                        cmdTransaccionFactory.Parameters.Add(p_cod_cuenta);
                        cmdTransaccionFactory.Parameters.Add(p_cod_est_det);
                        cmdTransaccionFactory.Parameters.Add(p_tipo_mov);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PROCESO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (vUsuario.programaGeneraLog)
                            DAauditoria.InsertarLog(pProceso, "PROCESO_CONTABLE", vUsuario, Accion.Crear.ToString());

                        dbConnectionFactory.CerrarConexion(connection);
                        return pProceso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoContableData", "CrearProcesoContable", ex);
                        return null;
                    }
                }
            }
        }

        public List<ProcesoContable> ListarProcesoContable(ProcesoContable pProcesoContable, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ProcesoContable> lstProcesoContable = new List<ProcesoContable>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT p.* FROM v_proceso_contable p " + ObtenerFiltro(pProcesoContable) + " ORDER BY COD_PROCESO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ProcesoContable entidad = new ProcesoContable();
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt32(resultado["COD_PROCESO"]);
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt32(resultado["TIPO_OPE"]);
                            if (resultado["NOM_TIPO_OPE"] != DBNull.Value) entidad.nom_tipo_ope = Convert.ToString(resultado["NOM_TIPO_OPE"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["NOM_TIPO_COMP"] != DBNull.Value) entidad.nom_tipo_comp = Convert.ToString(resultado["NOM_TIPO_COMP"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToInt32(resultado["CONCEPTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
                            lstProcesoContable.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProcesoContable;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoContableData", "ListarProcesoContable", ex);
                        return null;
                    }
                }
            }
        }

        public ProcesoContable ConsultarProcesoContable(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ProcesoContable entidad = new ProcesoContable();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM proceso_contable WHERE COD_PROCESO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt32(resultado["COD_PROCESO"]);
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt32(resultado["TIPO_OPE"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToInt32(resultado["CONCEPTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
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
                        BOExcepcion.Throw("ProcesoContableData", "ConsultarProcesoContable", ex);
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
                        string sql = "SELECT MAX(cod_proceso) + 1 FROM proceso_contable ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        string sDato = cmdTransaccionFactory.ExecuteScalar().ToString();
                        if (sDato != null && sDato.Trim() != "")
                            resultado = Convert.ToInt64(sDato);
                        else
                            resultado = 1;
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProcesoContableData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }

        public ProcesoContable ConsultarProcesoContableOperacion(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ProcesoContable entidad = new ProcesoContable();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM proceso_contable WHERE TIPO_OPE = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["COD_PROCESO"] != DBNull.Value) entidad.cod_proceso = Convert.ToInt32(resultado["COD_PROCESO"]);
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.tipo_ope = Convert.ToInt32(resultado["TIPO_OPE"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["FECHA_INICIAL"] != DBNull.Value) entidad.fecha_inicial = Convert.ToDateTime(resultado["FECHA_INICIAL"]);
                            if (resultado["FECHA_FINAL"] != DBNull.Value) entidad.fecha_final = Convert.ToDateTime(resultado["FECHA_FINAL"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) entidad.concepto = Convert.ToInt32(resultado["CONCEPTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_EST_DET"] != DBNull.Value) entidad.cod_est_det = Convert.ToInt32(resultado["COD_EST_DET"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToInt32(resultado["TIPO_MOV"]);
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
                        BOExcepcion.Throw("ProcesoContableData", "ConsultarProcesoContable", ex);
                        return null;
                    }
                }
            }
        }

    }
}
