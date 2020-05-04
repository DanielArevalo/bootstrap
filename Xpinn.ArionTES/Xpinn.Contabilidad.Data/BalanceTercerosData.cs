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
    public class BalanceTercerosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public BalanceTercerosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método para consultar el balance de prueba
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<BalanceTerceros> ListarBalance(BalanceTerceros pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceTerceros> lstBalPru = new List<BalanceTerceros>();

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
                        cmdTransaccionFactory.Parameters.Add(PCENINI);

                        DbParameter PCENFIN = cmdTransaccionFactory.CreateParameter();
                        PCENFIN.ParameterName = "PCENFIN";
                        PCENFIN.Value = pEntidad.centro_costo_fin;
                        PCENFIN.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PCENFIN);

                        DbParameter PNIVEL = cmdTransaccionFactory.CreateParameter();
                        PNIVEL.ParameterName = "PNIVEL";
                        PNIVEL.Value = pEntidad.nivel;
                        PNIVEL.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PNIVEL);

                        DbParameter PCUENTASENCERO = cmdTransaccionFactory.CreateParameter();
                        PCUENTASENCERO.ParameterName = "PCUENTASENCERO";
                        PCUENTASENCERO.Value = pEntidad.cuentascero;
                        PCUENTASENCERO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PCUENTASENCERO);

                        DbParameter PMOSTRARMOVPER13 = cmdTransaccionFactory.CreateParameter();
                        PMOSTRARMOVPER13.ParameterName = "PMOSTRARMOVPER13";
                        PMOSTRARMOVPER13.Value = pEntidad.mostrarmovper13 != null ? pEntidad.mostrarmovper13 : 0;
                        PMOSTRARMOVPER13.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PMOSTRARMOVPER13);

                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "PMONEDA";
                        PMONEDA.Value = pEntidad.cod_moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pEntidad.esniif)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BALTERNIIF";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BALTER";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceTercerosData", "USP_XPINN_CON_BALTER", ex);
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

                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            if (pEntidad.centro_costo != 0)
                            {                                    
                                sql = @"SELECT TEMP_BALANCE_TER.*
                                        FROM TEMP_BALANCE_TER LEFT WHERE TEMP_BALANCE_TER.fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') AND TEMP_BALANCE_TER.centro_costo = " + pEntidad.centro_costo + "  ORDER BY TEMP_BALANCE_TER.cod_cuenta, TEMP_BALANCE_TER.identificacion, TEMP_BALANCE_TER.centro_costo";
                            }
                            else
                            {                                
                                sql = @"SELECT TEMP_BALANCE_TER.*
                                        FROM TEMP_BALANCE_TER WHERE TEMP_BALANCE_TER.fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') ORDER BY TEMP_BALANCE_TER.cod_cuenta, TEMP_BALANCE_TER.identificacion, TEMP_BALANCE_TER.centro_costo";
                            }                            
                        else
                            sql = @"SELECT TEMP_BALANCE_TER.*
                                        FROM TEMP_BALANCE_TER WHERE TEMP_BALANCE_TER.fecha = '" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "' ORDER BY TEMP_BALANCE_TER.cod_cuenta, TEMP_BALANCE_TER.identificacion, TEMP_BALANCE_TER.centro_costo";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceTerceros entidad = new BalanceTerceros();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["SALDOINI"] != DBNull.Value) entidad.SaldoIni = Convert.ToDouble(resultado["SALDOINI"]);
                            if (resultado["CREDITOS"] != DBNull.Value) entidad.Creditos = Convert.ToDouble(resultado["CREDITOS"]);
                            if (resultado["DEBITOS"] != DBNull.Value) entidad.Debitos = Convert.ToDouble(resultado["DEBITOS"]);
                            if (resultado["SALDOFIN"] != DBNull.Value) entidad.SaldoFin = Convert.ToDouble(resultado["SALDOFIN"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.tercero = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombreTercero = Convert.ToString(resultado["NOMBRE"]);
                            lstBalPru.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstBalPru;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceTercerosData", "ListarBalance", ex);
                        return null;
                    }
                }
            }
        }


        public BalanceTerceros ConsultarMes13(BalanceTerceros pDatos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            BalanceTerceros lstFechaCierre = new BalanceTerceros();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Distinct fecha from cierea where tipo='N'  AND ESTADO='D' and fecha =  '" + Convert.ToDateTime(pDatos.fecha).ToShortDateString() + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceTerceros entidad = new BalanceTerceros();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                            return entidad;
                        }

                        return lstFechaCierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceTercerosData", "ListarFechaCierre", ex);
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
        public List<BalanceTerceros> ListarFechaCierre(string tipo, string estado, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceTerceros> lstFechaCierre = new List<BalanceTerceros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Distinct fecha From cierea Where tipo = '" +  tipo + "' " + (estado.Trim() != "" ? " And estado = '" + estado + "' " : "") +  " Order by fecha desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceTerceros entidad = new BalanceTerceros();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                            lstFechaCierre.Add(entidad);
                        }

                        return lstFechaCierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalancePruebaData", "ListarFechaCierre", ex);
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
        public BalanceTerceros ConsultarBalanceMes13(BalanceTerceros pDatos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            BalanceTerceros entidad = new BalanceTerceros();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "Select Distinct fecha from cierea where tipo='N'and  estado='D' and fecha= To_Date('" + Convert.ToDateTime(pDatos.fecha).ToShortDateString() + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql = "Select Distinct fecha from cierea where tipo='N'and  estado='D' and fecha= '" + Convert.ToDateTime(pDatos.fecha).ToShortDateString() + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                        }

                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceTercerosData", "ConsultarBalance", ex);
                        return null;
                    }
                }
            }
        }

        public List<BalanceTerceros> ListarBalanceComprobacion(BalanceTerceros pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceTerceros> lstBalPru = new List<BalanceTerceros>();

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
                        if (pEntidad.centro_costo == null)
                            PCENINI.Value = DBNull.Value;
                        else
                            PCENINI.Value = pEntidad.centro_costo;
                        PCENINI.Direction = ParameterDirection.Input;
                        PCENINI.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENINI);

                        DbParameter PCENFIN = cmdTransaccionFactory.CreateParameter();
                        PCENFIN.ParameterName = "PCENFIN";
                        if (pEntidad.centro_costo_fin == null)
                            PCENFIN.Value = DBNull.Value;
                        else
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

                        DbParameter PCUENTASENCERO = cmdTransaccionFactory.CreateParameter();
                        PCUENTASENCERO.ParameterName = "PCUENTASENCERO";
                        PCUENTASENCERO.Value = pEntidad.cuentascero;
                        PCUENTASENCERO.Direction = ParameterDirection.Input;
                        PCUENTASENCERO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCUENTASENCERO);

                        DbParameter PMOSTRARMOVPER13 = cmdTransaccionFactory.CreateParameter();
                        PMOSTRARMOVPER13.ParameterName = "PMOSTRARMOVPER13";
                        if (pEntidad.mostrarmovper13 == null)
                            PMOSTRARMOVPER13.Value = DBNull.Value;
                        else
                            PMOSTRARMOVPER13.Value = pEntidad.mostrarmovper13;
                        PMOSTRARMOVPER13.Direction = ParameterDirection.Input;
                        PMOSTRARMOVPER13.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PMOSTRARMOVPER13);

                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "PMONEDA";
                        PMONEDA.Value = pEntidad.cod_moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BALCOMPROBACION";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarBalanceComprobacion", "USP_XPINN_CON_BALCOMPROBACION", ex);
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
                        
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "Select * from TEMP_LIBROMAYOR Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta";
                        else
                            sql = "Select * from TEMP_LIBROMAYOR Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceTerceros entidad = new BalanceTerceros();
                            double saldo_inicial_debito = 0, saldo_inicial_credito = 0;
                            double saldo_final_debito = 0, saldo_final_credito = 0;

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            //if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo = Convert.ToString(resultado["TIPO"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["SALDO_INICIAL_DEBITO"] != DBNull.Value) saldo_inicial_debito = Convert.ToDouble(resultado["SALDO_INICIAL_DEBITO"]);
                            if (resultado["SALDO_INICIAL_CREDITO"] != DBNull.Value) saldo_inicial_credito = Convert.ToDouble(resultado["SALDO_INICIAL_CREDITO"]);
                            if (resultado["DEBITO"] != DBNull.Value) entidad.debitos = Convert.ToDouble(resultado["DEBITO"]);
                            if (resultado["CREDITO"] != DBNull.Value) entidad.creditos = Convert.ToDouble(resultado["CREDITO"]);
                            if (resultado["SALDO_FINAL_DEBITO"] != DBNull.Value) saldo_final_debito = Convert.ToDouble(resultado["SALDO_FINAL_DEBITO"]);
                            if (resultado["SALDO_FINAL_CREDITO"] != DBNull.Value) saldo_final_credito = Convert.ToDouble(resultado["SALDO_FINAL_CREDITO"]);                            
                            if (entidad.tipo == "D")
                            {
                                entidad.saldo_inicial = saldo_inicial_debito - saldo_inicial_credito;
                                entidad.saldo_final = saldo_final_debito - saldo_final_credito;
                            }
                            else
                            {
                                entidad.saldo_inicial = saldo_inicial_credito - saldo_inicial_debito;
                                entidad.saldo_final = saldo_final_credito - saldo_final_debito;
                            }
                            lstBalPru.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstBalPru;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceTercerosData", "ListarBalance", ex);
                        return null;
                    }
                }
            }
        }
        public List<BalanceTerceros> listarbalancecentrocosto(Usuario pUsuario,string centro,string fecha)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceTerceros> lstComponenteAdicional = new List<BalanceTerceros>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql="";
                        Configuracion conf = new Configuracion();
                        if (centro == "0")
                        {
                            sql = " select C.COD_CUENTA,Nombre,Sum(Valor) as Valor from "+
                                "  PLAN_CUENTAS C join Balance B on C.COD_CUENTA = B.COD_CUENTA "+
                                "  join OFICINA_CUENTA_CONTABLE OCC on OCC.COD_AUXILIAR = C.COD_CUENTA "+
                                "  join CENTRO_COSTO CC on CC.CENTRO_COSTO = B.CENTRO_COSTO "+
                                "  where B.FECHA = TO_DATE('" + fecha+ "','DD,MM,YY') " +
                                "  group by C.COD_CUENTA,Nombre order by C.COD_CUENTA ";
                        }
                        else
                        {
                            sql = "select C.COD_CUENTA,Nombre,Sum(Valor) as Valor,(select sum(Valor) from Balance bl where bl.COD_CUENTA = C.COD_CUENTA and Bl.CENTRO_COSTO = "+centro+") as centro "+
                           " from PLAN_CUENTAS C join Balance B on C.COD_CUENTA = B.COD_CUENTA join OFICINA_CUENTA_CONTABLE OCC on OCC.COD_AUXILIAR = C.COD_CUENTA join CENTRO_COSTO " +
                           " CC on CC.CENTRO_COSTO = B.CENTRO_COSTO where B.FECHA=TO_DATE('" + fecha + "','DD,MM,YY')  group by C.COD_CUENTA,Nombre order by C.COD_CUENTA ";
                        }



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceTerceros entidad = new BalanceTerceros();

                            if (resultado["Nombre"] != DBNull.Value) entidad.nombrecuenta = (Convert.ToString(resultado["nombre"]));
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["Valor"] != DBNull.Value) entidad.valor = Convert.ToDouble(resultado["Valor"]);
                            if (centro!="0")
                            {
                                if (resultado["centro"] != DBNull.Value) entidad.valorcentro = Convert.ToDouble(resultado["centro"]);
                            }

                            lstComponenteAdicional.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraOficinasData", "consultarCarteraOficinas", ex);
                        return null;
                    }
                }
              
            }
        }
        public List<BalanceTerceros> ListarCentroCosto(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceTerceros> lstComponenteAdicional = new List<BalanceTerceros>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select * from CENTRO_COSTO order by Centro_Costo";




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceTerceros entidad = new BalanceTerceros();

                           
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descipcion = Convert.ToString(resultado["DESCRIPCION"]);

                            lstComponenteAdicional.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraOficinasData", "consultarCarteraOficinas", ex);
                        return null;
                    }
                }
            }

        }
        public BalanceTerceros ListarValorCentroCosto(string Centro,string cod_cuenta,Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            BalanceTerceros entidad = new BalanceTerceros();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "select sum(valor) as Valor,PC.COD_CUENTA from Balance bl join PLAN_CUENTAS PC on bl.COD_CUENTA=PC.COD_CUENTA "+
                        " join OFICINA_CUENTA_CONTABLE OCC on OCC.COD_AUXILIAR = PC.COD_CUENTA " +
                        " join CENTRO_COSTO CC on CC.CENTRO_COSTO = bl.CENTRO_COSTO where CC.DESCRIPCION = '"+Centro+ "' and PC.COD_CUENTA='"+cod_cuenta+"' GROUP BY PC.COD_CUENTA,Nombre   order by PC.COD_CUENTA ";




                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                           

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["Valor"] != DBNull.Value) entidad.valorcentro = Convert.ToDouble(resultado["Valor"]);

                          
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraOficinasData", "consultarCarteraOficinas", ex);
                        return null;
                    }
                }
            }
        }
        }


}
