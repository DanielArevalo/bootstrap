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
    public class LibroDiarioColumnarioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public LibroDiarioColumnarioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        /// <summary>
        /// Método para consultar el libro DiarioColumnario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<LibroDiarioColumnario> ListarLibroDiarioNiff(LibroDiarioColumnario pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LibroDiarioColumnario> lstBalGeneral = new List<LibroDiarioColumnario>();

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

                        DbParameter PNIVEL = cmdTransaccionFactory.CreateParameter();
                        PNIVEL.ParameterName = "PNIVEL";
                        PNIVEL.Value = pEntidad.nivel;
                        PNIVEL.Direction = ParameterDirection.Input;
                        PNIVEL.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PNIVEL);

                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "PMONEDA";
                        PMONEDA.Value = pEntidad.moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        PMONEDA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_COMPDIARIONIIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroDiarioColumnarioData", "USP_XPINN_CON_COMPDIARIO", ex);
                        return null;
                    }

                    try
                    {                        

                        Configuracion conf = new Configuracion();

                        string sql = "Select fecha, TIPO_COMP_NIFF || ' ' || DESCRIPCION_NIFF as DESCRIPCION_NIFF, COD_CUENTA_NIFF, NOMBRE_NIFF, sum(DEBITO_NIFF) as debito, sum(CREDITO_NIFF) as credito from TEMP_COMPDIARIO_NIFF Where fecha = To_Date('" + pEntidad.fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') Group by fecha, TIPO_COMP_NIFF || ' ' || DESCRIPCION_NIFF, COD_CUENTA_NIFF, NOMBRE_NIFF Order by 3, 2";
                        cmdTransaccionFactory.Parameters.Clear();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read()) 
                        {
                            LibroDiarioColumnario entidad = new LibroDiarioColumnario();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["DESCRIPCION_NIFF"] != DBNull.Value) entidad.tipocomp = Convert.ToString(resultado["DESCRIPCION_NIFF"]);
                            if (resultado["COD_CUENTA_NIFF"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA_NIFF"]);
                            if (resultado["NOMBRE_NIFF"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_NIFF"]);
                            if (resultado["debito"] != DBNull.Value) entidad.debito = Convert.ToInt64(resultado["debito"]);
                            if (resultado["credito"] != DBNull.Value) entidad.credito = Convert.ToInt64(resultado["credito"]);
                            lstBalGeneral.Add(entidad);
                        }

                        return lstBalGeneral;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroDiarioColumnarioData", "ListarLibroDiario", ex);
                        return null;
                    }

                    dbConnectionFactory.CerrarConexion(connection);

                }
            }
        }

        /// <summary>
        /// Método para consultar el libro DiarioColumnario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<LibroDiarioColumnario> ListarLibroDiario(LibroDiarioColumnario pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LibroDiarioColumnario> lstBalGeneral = new List<LibroDiarioColumnario>();

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
                        
                        DbParameter PNIVEL = cmdTransaccionFactory.CreateParameter();
                        PNIVEL.ParameterName = "PNIVEL";
                        PNIVEL.Value = pEntidad.nivel;                      
                        PNIVEL.Direction = ParameterDirection.Input;
                        PNIVEL.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PNIVEL);
                        
                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "PMONEDA";
                        PMONEDA.Value = pEntidad.moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        PMONEDA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);
                        
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_COMPDIARIO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroDiarioColumnarioData", "USP_XPINN_CON_COMPDIARIO", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select fecha, tipo_comp || ' ' || descripcion as descripcion, cod_cuenta, nombre, sum(debito) as debito, sum(credito) as credito from TEMP_COMPDIARIO Where fecha = To_Date('" + pEntidad.fecha.ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Group by fecha, tipo_comp || ' ' || descripcion, cod_cuenta, nombre Order by 3, 2";                       
                        
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LibroDiarioColumnario entidad = new LibroDiarioColumnario();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.tipocomp = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);                            
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DEBITO"] != DBNull.Value) entidad.debito= Convert.ToInt64(resultado["DEBITO"]);
                            if (resultado["CREDITO"] != DBNull.Value) entidad.credito = Convert.ToInt64(resultado["CREDITO"]);
                            lstBalGeneral.Add(entidad);
                        }

                        return lstBalGeneral;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroDiarioColumnarioData", "ListarLibroDiario", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para consultar la fecha de corte 
        /// </summary>
        /// <param name="pPlanCuentas"></param>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<LibroDiarioColumnario> ListarFechaCorte(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LibroDiarioColumnario> lstFechaCorte = new List<LibroDiarioColumnario>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Distinct fecha from cierea where tipo='C' order by fecha desc";
                                               
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LibroDiarioColumnario entidad = new LibroDiarioColumnario();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                            lstFechaCorte.Add(entidad);
                        }

                        return lstFechaCorte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroDiarioColumnarioData", "ListarFechaCorte", ex);
                        return null;
                    }
                }
            }
        }

    }
}
