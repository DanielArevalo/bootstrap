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
    public class BalanceGeneralData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public BalanceGeneralData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        /// <summary>
        /// Método para consultar el libro auxiliar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<BalanceGeneral> ListarBalance(BalanceGeneral pEntidad, Usuario vUsuario,int pOpcion)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceGeneral> lstBalGeneral= new List<BalanceGeneral>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
              {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                       
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = pEntidad.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                      

                        DbParameter PCENINI = cmdTransaccionFactory.CreateParameter();
                        PCENINI.ParameterName = "PCENINI";
                        PCENINI.Value = pEntidad.centro_costo;
                        PCENINI.Direction = ParameterDirection.Input;
                        PCENINI.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENINI);
                     

                        DbParameter PCENFIN = cmdTransaccionFactory.CreateParameter();
                        PCENFIN.ParameterName = "PCENFIN";
                        PCENFIN.Value = pEntidad.centro_costo_fin;                      
                        PCENFIN.Direction = ParameterDirection.Input;
                        PCENFIN.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENFIN);



                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "PMONEDA";
                        PMONEDA.Value = pEntidad.moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        PMONEDA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);


                        DbParameter PNIVEL = cmdTransaccionFactory.CreateParameter();
                        PNIVEL.ParameterName = "PNIVEL";
                        PNIVEL.Value = pEntidad.nivel;                      
                        PNIVEL.Direction = ParameterDirection.Input;
                        PNIVEL.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PNIVEL);


                        DbParameter PCUENTASENCERO = cmdTransaccionFactory.CreateParameter();
                        PCUENTASENCERO.ParameterName = "PCUENTASENCERO";
                        PCUENTASENCERO.Value = pEntidad.cuentascero;
                        PCUENTASENCERO.Direction = ParameterDirection.Input;
                        PCUENTASENCERO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCUENTASENCERO);


                        DbParameter PCOMPARTIVOCC = cmdTransaccionFactory.CreateParameter();
                        PCOMPARTIVOCC.ParameterName = "PCOMPARTIVOCC";
                        PCOMPARTIVOCC.Value = pEntidad.comparativo;
                        PCOMPARTIVOCC.Direction = ParameterDirection.Input;
                        PCUENTASENCERO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCOMPARTIVOCC);

                        DbParameter PCUENTASORDEN = cmdTransaccionFactory.CreateParameter();
                        PCUENTASORDEN.ParameterName = "PCUENTASORDEN";
                        PCUENTASORDEN.Value = pEntidad.cuentasorden;
                        PCUENTASORDEN.Direction = ParameterDirection.Input;
                        PCUENTASORDEN.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCUENTASORDEN);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pOpcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BALANCE";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BALANCENIIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceGeneralData", "USP_XPINN_CON_BALANCE", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_BALANCE Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta";                       
                        
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceGeneral entidad = new BalanceGeneral();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDouble(resultado["VALOR"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            lstBalGeneral.Add(entidad);
                        }

                        return lstBalGeneral;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceGeneralData", "ListarBalance", ex);
                        return null;
                    }
                }
            }
        }

        public string VerificarComprobantesYCuentas(DateTime fechaCorte, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pFecha = cmdTransaccionFactory.CreateParameter();
                        pFecha.ParameterName = "pFecha";
                        pFecha.Value = fechaCorte;
                        pFecha.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pFecha);

                        DbParameter p_mensajeerror = cmdTransaccionFactory.CreateParameter();
                        p_mensajeerror.ParameterName = "pMensajeError";
                        p_mensajeerror.Value = DBNull.Value;

                        // No quitar, molesta si lo quitas
                        p_mensajeerror.Size = 8000;
                        p_mensajeerror.DbType = DbType.String;
                        p_mensajeerror.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(p_mensajeerror);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_VERIFICAR_CUE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        string error = p_mensajeerror.Value != DBNull.Value ? p_mensajeerror.Value.ToString() : string.Empty;

                        return error;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceGeneralData", "VerificarComprobantesYCuentas", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para consultar el plan de cuentas
        /// </summary>
        /// <param name="pPlanCuentas"></param>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<BalanceGeneral> ListarFechaCierre(Usuario pUsuario)
        {
            return ListarFechaCierre("C", "", pUsuario);
        }

        public List<BalanceGeneral> ListarFechaCierre(string pTipo, string pEstado, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceGeneral> lstFechaCierre = new List<BalanceGeneral>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Distinct fecha From cierea Where tipo = '" + pTipo + "' " + (pEstado.Trim() == "" ? "": " And estado = '" + pEstado.Trim() + "' ") + " Order by fecha desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceGeneral entidad = new BalanceGeneral();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                            lstFechaCierre.Add(entidad);
                        }

                        return lstFechaCierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceGeneralData", "ListarFechaCierre", ex);
                        return null;
                    }
                }
            }
        }

        public BalanceGeneral CrearBalance(BalanceGeneral pBalanceGeneral, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcentro_costo = cmdTransaccionFactory.CreateParameter();
                        pcentro_costo.ParameterName = "p_centro_costo";
                        pcentro_costo.Value = pBalanceGeneral.centro_costo;
                        pcentro_costo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcentro_costo);

                        DbParameter pcod_cuenta = cmdTransaccionFactory.CreateParameter();
                        pcod_cuenta.ParameterName = "p_cod_cuenta";
                        pcod_cuenta.Value = pBalanceGeneral.cod_cuenta;
                        pcod_cuenta.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_cuenta);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pBalanceGeneral.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pBalanceGeneral.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pBalanceGeneral.estado;
                        pestado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_moneda = cmdTransaccionFactory.CreateParameter();
                        pcod_moneda.ParameterName = "p_cod_moneda";
                        pcod_moneda.Value = pBalanceGeneral.moneda;
                        pcod_moneda.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pcod_moneda);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BALANCE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pBalanceGeneral;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceGeneralData", "CrearBalance", ex);
                        return null;
                    }
                }
            }
        }



    }
}
