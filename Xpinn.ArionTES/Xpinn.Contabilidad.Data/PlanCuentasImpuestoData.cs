using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;
using Xpinn.NIIF.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para PlanCuentass
    /// </summary>    
    public class PlanCuentasImpuestoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
                
        public PlanCuentasImpuestoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public PlanCuentasImpuesto CrearPlanCuentasImpuesto(PlanCuentasImpuesto pImpuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidimpuesto = cmdTransaccionFactory.CreateParameter();
                        pidimpuesto.ParameterName = "p_idimpuesto";
                        pidimpuesto.Value = pImpuesto.idimpuesto;
                        pidimpuesto.Direction = ParameterDirection.Output;
                        pidimpuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidimpuesto);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pImpuesto.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        pcod_tipo_impuesto.Value = pImpuesto.cod_tipo_impuesto;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);

                        DbParameter pporcentaje_impuesto = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_impuesto.ParameterName = "p_porcentaje_impuesto";
                        pporcentaje_impuesto.Value = pImpuesto.porcentaje_impuesto;
                        pporcentaje_impuesto.Direction = ParameterDirection.Input;
                        pporcentaje_impuesto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_impuesto);

                        DbParameter pbase_minima = cmdTransaccionFactory.CreateParameter();
                        pbase_minima.ParameterName = "p_base_minima";
                        if (pImpuesto.base_minima != null) pbase_minima.Value = pImpuesto.base_minima; else pbase_minima.Value = DBNull.Value;
                        pbase_minima.Direction = ParameterDirection.Input;
                        pbase_minima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbase_minima);

                        DbParameter pcod_cuenta_imp = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_imp.ParameterName = "p_cod_cuenta_imp";
                        pcod_cuenta_imp.Value = pImpuesto.cod_cuenta_imp;
                        pcod_cuenta_imp.Direction = ParameterDirection.Input;
                        pcod_cuenta_imp.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_imp);

                        DbParameter pasumido = cmdTransaccionFactory.CreateParameter();
                        pasumido.ParameterName = "P_ASUMIDO";
                        pasumido.Value = pImpuesto.asumido;
                        pasumido.Direction = ParameterDirection.Input;
                        pasumido.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pasumido);

                        DbParameter P_COD_CUENTA_ASUMIDO = cmdTransaccionFactory.CreateParameter();
                        P_COD_CUENTA_ASUMIDO.ParameterName = "P_COD_CUENTA_ASUMIDO";
                        P_COD_CUENTA_ASUMIDO.Value = pImpuesto.cod_cuenta_asumido;
                        P_COD_CUENTA_ASUMIDO.Direction = ParameterDirection.Input;
                        P_COD_CUENTA_ASUMIDO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_COD_CUENTA_ASUMIDO);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PLAN_CUENT_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pImpuesto.idimpuesto = Convert.ToInt32(pidimpuesto.Value);
                        return pImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasImpuestoData", "CrearPlanCuentasImpuesto", ex);
                        return null;
                    }
                }
            }
        }

        public PlanCuentasImpuesto ModificarPlanCuentasImpuesto(PlanCuentasImpuesto pImpuesto, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidimpuesto = cmdTransaccionFactory.CreateParameter();
                        pidimpuesto.ParameterName = "p_idimpuesto";
                        pidimpuesto.Value = pImpuesto.idimpuesto;
                        pidimpuesto.Direction = ParameterDirection.Input;
                        pidimpuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidimpuesto);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pImpuesto.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        pcod_cuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pcod_tipo_impuesto = cmdTransaccionFactory.CreateParameter();
                        pcod_tipo_impuesto.ParameterName = "p_cod_tipo_impuesto";
                        pcod_tipo_impuesto.Value = pImpuesto.cod_tipo_impuesto;
                        pcod_tipo_impuesto.Direction = ParameterDirection.Input;
                        pcod_tipo_impuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_tipo_impuesto);

                        DbParameter pporcentaje_impuesto = cmdTransaccionFactory.CreateParameter();
                        pporcentaje_impuesto.ParameterName = "p_porcentaje_impuesto";
                        pporcentaje_impuesto.Value = pImpuesto.porcentaje_impuesto;
                        pporcentaje_impuesto.Direction = ParameterDirection.Input;
                        pporcentaje_impuesto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje_impuesto);

                        DbParameter pbase_minima = cmdTransaccionFactory.CreateParameter();
                        pbase_minima.ParameterName = "p_base_minima";
                        if (pImpuesto.base_minima != null) pbase_minima.Value = pImpuesto.base_minima; else pbase_minima.Value = DBNull.Value;
                        pbase_minima.Direction = ParameterDirection.Input;
                        pbase_minima.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pbase_minima);

                        DbParameter pcod_cuenta_imp = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta_imp.ParameterName = "p_cod_cuenta_imp";
                        pcod_cuenta_imp.Value = pImpuesto.cod_cuenta_imp;
                        pcod_cuenta_imp.Direction = ParameterDirection.Input;
                        pcod_cuenta_imp.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta_imp);


                        DbParameter pasumido = cmdTransaccionFactory.CreateParameter();
                        pasumido.ParameterName = "P_ASUMIDO";
                        pasumido.Value = pImpuesto.asumido;
                        pasumido.Direction = ParameterDirection.Input;
                        pasumido.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pasumido);

                        DbParameter P_COD_CUENTA_ASUMIDO = cmdTransaccionFactory.CreateParameter();
                        P_COD_CUENTA_ASUMIDO.ParameterName = "P_COD_CUENTA_ASUMIDO";
                        P_COD_CUENTA_ASUMIDO.Value = pImpuesto.cod_cuenta_asumido;
                        P_COD_CUENTA_ASUMIDO.Direction = ParameterDirection.Input;
                        P_COD_CUENTA_ASUMIDO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_COD_CUENTA_ASUMIDO);



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PLAN_CUENT_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pImpuesto.idimpuesto = Convert.ToInt32(pidimpuesto.Value);
                        return pImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasImpuestoData", "ModificarPlanCuentasImpuesto", ex);
                        return null;
                    }
                }
            }
        }
        


        public void EliminarPlanCuentaImpuesto(Int32 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidimpuesto = cmdTransaccionFactory.CreateParameter();
                        pidimpuesto.ParameterName = "p_idimpuesto";
                        pidimpuesto.Value = pId;
                        pidimpuesto.Direction = ParameterDirection.Input;
                        pidimpuesto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidimpuesto);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PLAN_CUENT_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasImpuestoData", "EliminarPlanCuentaImpuesto", ex);
                    }
                }
            }
        }


        public void EliminarPlanCuentaImpuestoXCuenta(string pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "DELETE FROM Plan_Cuentas_Impuesto WHERE Cod_Cuenta = '" + pId.ToString() + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasImpuestoData", "EliminarPlanCuentaImpuestoXCuenta", ex);
                    }
                }
            }
        }


        public List<PlanCuentasImpuesto> ListarPlanCuentasImpuesto(PlanCuentasImpuesto pImpu,string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<PlanCuentasImpuesto> lstImpuesto = new List<PlanCuentasImpuesto>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM PLAN_CUENTAS_IMPUESTO " + ObtenerFiltro(pImpu);

                            if(filtro != "")
                                sql += filtro;

                            sql += " ORDER BY IDIMPUESTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PlanCuentasImpuesto entidad = new PlanCuentasImpuesto();
                            if (resultado["IDIMPUESTO"] != DBNull.Value) entidad.idimpuesto = Convert.ToInt32(resultado["IDIMPUESTO"]);
                            if (resultado["ASUMIDO"] != DBNull.Value) entidad.asumido = Convert.ToInt32(resultado["ASUMIDO"]);
                            if (resultado["COD_CUENTA_ASUMIDO"] != DBNull.Value) entidad.cod_cuenta_asumido = Convert.ToString(resultado["COD_CUENTA_ASUMIDO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["COD_TIPO_IMPUESTO"] != DBNull.Value) entidad.cod_tipo_impuesto = Convert.ToInt32(resultado["COD_TIPO_IMPUESTO"]);
                            if (resultado["PORCENTAJE_IMPUESTO"] != DBNull.Value) entidad.porcentaje_impuesto = Convert.ToDecimal(resultado["PORCENTAJE_IMPUESTO"]);
                            if (resultado["BASE_MINIMA"] != DBNull.Value) entidad.base_minima = Convert.ToDecimal(resultado["BASE_MINIMA"]);
                            if (resultado["COD_CUENTA_IMP"] != DBNull.Value) entidad.cod_cuenta_imp = Convert.ToString(resultado["COD_CUENTA_IMP"]);
                            lstImpuesto.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstImpuesto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PlanCuentasImpuestoData", "ListarPlanCuentasImpuesto", ex);
                        return null;
                    }
                }
            }
        }


    }
}
