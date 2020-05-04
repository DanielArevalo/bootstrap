using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Data
{
    /// <summary>
    /// Objeto de acceso a datos para CierreAnual
    /// </summary>    
    public class CierreAnualData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public CierreAnualData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método para consultar la fecha del último cierre definitivo
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public DateTime FechaUltimoCierre(Usuario pUsuario)
        {
            DateTime fecha = DateTime.MinValue;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Max(fecha) As fecha From cierea Where tipo = 'N' And estado = 'D' ";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                        }
                        return fecha;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreAnualData", "FechaUltimoCierre", ex);
                        return fecha;
                    }
                }
            }
        }

        public void PeriodicidadCierre(ref int dias_cierre, ref int tipo_calendario, Usuario pUsuario)
        {
            dias_cierre = 360;
            tipo_calendario = 1;
            int periodicidad = 0;
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string valor = "";
                        string sql = "Select cod_periodicidad as VALOR, numero_dias, tipo_calendario From periodicidad Where numero_dias = 360 and tipo_calendario = 1 ";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"].ToString().Trim());
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) dias_cierre = Convert.ToInt16(resultado["NUMERO_DIAS"].ToString());
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) tipo_calendario = Convert.ToInt16(resultado["TIPO_CALENDARIO"].ToString());
                        }
                        try
                        {
                            periodicidad = Convert.ToInt16(valor);
                        }
                        catch
                        {
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreAnualData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Modificada una entidad CierreAnual en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad CierreAnual</param>
        /// <returns>Entidad modificada</returns>
        public CierreAnual CrearCierreAnual(CierreAnual pEntidad, ref string pErrorRet, bool IsNiif, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "pFecha";
                        pfecha.Value = pEntidad.fecha;
                        pfecha.DbType = DbType.Date;
                        pfecha.Direction = ParameterDirection.Input;

                        DbParameter pError = cmdTransaccionFactory.CreateParameter();
                        pError.ParameterName = "pError";
                        pError.Value = "                                                                                                                        ";
                        pError.Direction = ParameterDirection.Output;

                        DbParameter pUsua = cmdTransaccionFactory.CreateParameter();
                        pUsua.ParameterName = "pUsuario";
                        pUsua.Value = pUsuario.codusuario;
                        pUsua.DbType = DbType.Int64;
                        pUsua.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pfecha);
                        cmdTransaccionFactory.Parameters.Add(pError);
                        cmdTransaccionFactory.Parameters.Add(pUsua);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = IsNiif == false ? "USP_XPINN_CON_CIERREANUAL" : "USP_XPINN_NIF_CIERREANUAL";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pErrorRet = pError.Value.ToString();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreAnual", "CrearCierreAnual", ex);
                        return null;
                    }

                }
            }

            if (pUsuario.programaGeneraLog)
                DAauditoria.InsertarLog(pEntidad, "CIERREANUAL", pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA                        
            return pEntidad;

        }


    }
}
