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
    public class PyGData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public PyGData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        /// <summary>
        /// Método para consultar el libro auxiliar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<PyG> ListarPyG(PyG pEntidad, Usuario vUsuario,int pOpcion)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PyG> lstBalPru = new List<PyG>();

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

                        DbParameter PSALDOSPERIODO = cmdTransaccionFactory.CreateParameter();
                        PSALDOSPERIODO.ParameterName = "PSALDOSPERIODO";
                        PSALDOSPERIODO.Value = pEntidad.saldosperiodo;
                        PSALDOSPERIODO.Direction = ParameterDirection.Input;
                        PSALDOSPERIODO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PSALDOSPERIODO);

                        DbParameter PCIERREANUAL = cmdTransaccionFactory.CreateParameter();
                        PCIERREANUAL.ParameterName = "PCIERREANUAL";
                        PCIERREANUAL.Value = pEntidad.cierreanual;
                        PCIERREANUAL.Direction = ParameterDirection.Input;
                        PCIERREANUAL.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCIERREANUAL);

                        DbParameter PCOMPARTIVOCC = cmdTransaccionFactory.CreateParameter();
                        PCOMPARTIVOCC.ParameterName = "PCOMPARTIVOCC";
                        PCOMPARTIVOCC.Value = pEntidad.comparativo;
                        PCOMPARTIVOCC.Direction = ParameterDirection.Input;
                        PCUENTASENCERO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCOMPARTIVOCC);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pOpcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PyG";
                        else // Niif PENDIENTE POR AJUSTAR PL
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_PYGNIIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalancePruebaData", "USP_XPINN_CON_PyG", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "";
                        if (pEntidad.orden == 1)
                            sql = "Select * from TEMP_BALANCE Where fecha = To_Date('" + pEntidad.fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') Order by item";
                        else
                            sql = "Select * from TEMP_BALANCE Where fecha = To_Date('" + pEntidad.fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "') Order by centro_costo, item, cod_cuenta";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PyG entidad = new PyG();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDouble(resultado["VALOR"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            lstBalPru.Add(entidad);
                        }

                        return lstBalPru;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PyGData", "ListarPyG", ex);
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
        public List<PyG> ListarFechaCierre(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PyG> lstFechaCierre = new List<PyG>();

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
                            PyG entidad = new PyG();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                            lstFechaCierre.Add(entidad);
                        }

                        return lstFechaCierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PyGData", "PyGCierre", ex);
                        return null;
                    }
                }
            }
        }

    }
}
