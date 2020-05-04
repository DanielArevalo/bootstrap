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
    /// Objeto de acceso a datos para cIERREMENSUAL
    /// </summary>    
    public class CierremensualData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public CierremensualData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método para consultar la fecha del último cierre definitivo
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public DateTime FechaUltimoCierre(Usuario pUsuario,string pTipo = "C")
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
                        
                        string sql = "Select Max(fecha) As fecha From cierea Where tipo = '" + pTipo + "' And estado = 'D' ";                                               
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
                        BOExcepcion.Throw("CierremensualData", "FechaUltimoCierre", ex);
                        return fecha;
                    }
                }
            }
        }

        public void PeriodicidadCierre(ref int dias_cierre, ref int tipo_calendario, Usuario pUsuario)
        {
            dias_cierre = 30;
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
                        string sql = "Select valor From general Where codigo = 4100 ";
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) valor = Convert.ToString(resultado["VALOR"].ToString().Trim());
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
                        BOExcepcion.Throw("CierremensualData", "PeriodicidadCierre", ex);
                        return;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select numero_dias, tipo_calendario From periodicidad Where cod_periodicidad = " + periodicidad;
                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_DIAS"] != DBNull.Value) dias_cierre = Convert.ToInt16(resultado["NUMERO_DIAS"].ToString());
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) tipo_calendario = Convert.ToInt16(resultado["TIPO_CALENDARIO"].ToString());
                        }
                        return;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierremensualData", "PeriodicidadCierre", ex);
                        return;
                    }
                }

            }
        }
                
             /// <summary>
        /// Modificada una entidad Cierremensual en la base de datos
        /// </summary>
        /// <param name="pEntidad">Entidad Cierremensual</param>
        /// <returns>Entidad modificada</returns>
        public Cierremensual CrearCierremensual(Cierremensual pEntidad, Usuario pUsuario)
        {

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                       
                        DbParameter p_fecha= cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "pFecha";
                        p_fecha.Value = pEntidad.fecha;
                        p_fecha.DbType = DbType.Date;
                        p_fecha.Direction = ParameterDirection.Input;

                        DbParameter pCentroCosto = cmdTransaccionFactory.CreateParameter();
                        pCentroCosto.ParameterName = "pCentroCosto";
                        pCentroCosto.Value = pEntidad.centro_costo;
                        pCentroCosto.DbType = DbType.Int64;
                        pCentroCosto.Direction = ParameterDirection.Input;

                        DbParameter pPorTercero = cmdTransaccionFactory.CreateParameter();
                        pPorTercero.ParameterName = "pPorTercero";
                        pPorTercero.Value = pEntidad.terceros;
                        pPorTercero.DbType = DbType.Int64;
                        pPorTercero.Direction = ParameterDirection.Input;

                        DbParameter pEstado = cmdTransaccionFactory.CreateParameter();
                        pEstado.ParameterName = "pEstado";
                        pEstado.Value = pEntidad.estado;
                        pEstado.DbType = DbType.String;
                        pEstado.Direction = ParameterDirection.Input;

                        DbParameter pUsua = cmdTransaccionFactory.CreateParameter();
                        pUsua.ParameterName = "pUsuario";
                        pUsua.Value = pUsuario.codusuario;
                        pUsua.DbType = DbType.Int64;
                        pUsua.Direction = ParameterDirection.Input;
                        
                        cmdTransaccionFactory.Parameters.Add(p_fecha);
                        cmdTransaccionFactory.Parameters.Add(pCentroCosto);
                        cmdTransaccionFactory.Parameters.Add(pPorTercero);
                        cmdTransaccionFactory.Parameters.Add(pEstado);
                        cmdTransaccionFactory.Parameters.Add(pUsua);

                          connection.Open();  GuardarConexion(cmdTransaccionFactory,dbConnectionFactory.dbProveedorFactory.CreateCommand(), connection, pUsuario.codusuario);   dbConnectionFactory.CerrarConexion(connection);  connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_CIERRECCOSTO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        // if (pUsuario.programaGeneraLog)
                        // DAauditoria.InsertarLog(pEntidad, pUsuario, Accion.Modificar.ToString()); //REGISTRO DE AUDITORIA
                        //pEntidad.cod_poliza = Convert.ToString(p_cod_poliza.Value);

                        return pEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CierreMensual", "CrearCierremensual", ex);
                        return null;
                    }

                }
            }
        }


    }
}
