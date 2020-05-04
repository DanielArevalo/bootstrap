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
    public class BalancePruebaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public BalancePruebaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método para consultar el balance de prueba
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<BalancePrueba> ListarBalance(BalancePrueba pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalancePrueba> lstBalPru = new List<BalancePrueba>();

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

                        DbParameter PCOMPARTIVOCC = cmdTransaccionFactory.CreateParameter();
                        PCOMPARTIVOCC.ParameterName = "PCOMPARTIVOCC";
                        PCOMPARTIVOCC.Value = pEntidad.comparativo;
                        PCOMPARTIVOCC.Direction = ParameterDirection.Input;
                        PCUENTASENCERO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCOMPARTIVOCC);

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
                        PMONEDA.ParameterName = "pMoneda";
                        PMONEDA.Value = pEntidad.cod_moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);


                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BALPRU";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalancePruebaData", "USP_XPINN_CON_BALPRU", ex);
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
                            sql = "Select * from TEMP_BALANCE Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta";
                        else
                            sql = "Select * from TEMP_BALANCE Where fecha = '" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "' Order by cod_cuenta";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalancePrueba entidad = new BalancePrueba();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDouble(resultado["VALOR"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            lstBalPru.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstBalPru;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalancePruebaData", "ListarBalance", ex);
                        return null;
                    }
                }
            }
        }

        public BalancePrueba ConsultarMes13(BalancePrueba pDatos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            BalancePrueba lstFechaCierre = new BalancePrueba();

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
                            BalancePrueba entidad = new BalancePrueba();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"].ToString());
                            return entidad;
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
        public List<BalancePrueba> ListarFechaCierre(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalancePrueba> lstFechaCierre = new List<BalancePrueba>();

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
                            BalancePrueba entidad = new BalancePrueba();

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
        public BalancePrueba ConsultarBalanceMes13(BalancePrueba pDatos, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            BalancePrueba entidad = new BalancePrueba();

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
                        BOExcepcion.Throw("BalancePruebaData", "ConsultarBalance", ex);
                        return null;
                    }
                }
            }
        }

        public List<BalancePrueba> ListarBalanceComprobacion(BalancePrueba pEntidad, ref Double TotDeb, ref Double TotCre, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalancePrueba> lstBalPru = new List<BalancePrueba>();
            TotDeb = 0;
            TotCre = 0;
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
                        if (pEntidad.centro_costo_fin < 0)
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
                            BalancePrueba entidad = new BalancePrueba();
                            double saldo_inicial_debito = 0, saldo_inicial_credito = 0;
                            double saldo_final_debito = 0, saldo_final_credito = 0;

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
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
                        string sqltot = "";                       
                        sqltot = "Select fecha, Sum(debito) As debito, Sum(credito) As credito From TEMP_LIBROMAYOR Where nivel = 1 And fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Group by fecha"; 

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqltot;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {                            
                            if (resultado["DEBITO"] != DBNull.Value) TotDeb = Convert.ToDouble(resultado["DEBITO"]);
                            if (resultado["CREDITO"] != DBNull.Value) TotCre = Convert.ToDouble(resultado["CREDITO"]);                                                        
                        }

                        Double utilidad = 0;

                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        /// Calcular la utilidad crédito que fue sumada para que cuadren los valores
                        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                        DateTime _fecha_inicial, _fecha_final;
                        _fecha_final = Convert.ToDateTime(pEntidad.fecha);
                        _fecha_inicial = new DateTime(_fecha_final.Year, _fecha_final.Month, 1).AddDays(-1);
                        if (_fecha_final.Day == 31 && _fecha_final.Month == 12 && pEntidad.mostrarmovper13 == 1)
                            _fecha_inicial = _fecha_final;
                        string s_fecha_inicial, s_fecha_final;
                        s_fecha_inicial = " To_Date('" + _fecha_inicial.ToShortDateString() + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        s_fecha_final   = " To_Date('" + _fecha_final.ToShortDateString() + "', '" + conf.ObtenerFormatoFecha() + "') ";

                        if (pEntidad.mostrarmovper13 == 1 && Convert.ToDateTime(pEntidad.fecha).Day == 31 && Convert.ToDateTime(pEntidad.fecha).Month == 12)
                            sqltot = @"Select -(Case When (Select Count(*) From cierea Where tipo = 'N' And fecha = " + s_fecha_final + @" And estado = 'D') >= 1 
                                                    Then Nvl(Calcular_Utilidad_Anual(" + s_fecha_final + @", " + pEntidad.centro_costo.ToString() + @", " + pEntidad.centro_costo_fin.ToString() + @"), 0)
                                                    Else Nvl(Calcular_Utilidad(" + s_fecha_final + @", " + pEntidad.centro_costo.ToString() + @", " + pEntidad.centro_costo_fin.ToString() + @"), 0)
                                                End) As utilidad 
                                        From TEMP_LIBROMAYOR t Where t.cod_cuenta = '3' And t.fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Group by t.fecha";
                        else
                            sqltot = @"Select (Case When (Select Count(*) From cierea Where tipo = 'N' And fecha = " + s_fecha_final + @" And estado = 'D') >= 1 
                                                    Then Nvl(Calcular_Utilidad_Anual(" + s_fecha_final + @", " + pEntidad.centro_costo.ToString() + @", " + pEntidad.centro_costo_fin.ToString() + @"), 0)
                                                    Else Nvl(Calcular_Utilidad(" + s_fecha_final + @", " + pEntidad.centro_costo.ToString() + @", " + pEntidad.centro_costo_fin.ToString() + @"), 0)
                                                End) -  
                                                (Case When (Select Count(*) From cierea Where tipo = 'N' And fecha = " + s_fecha_inicial + @" And estado = 'D') >= 1
                                                    Then Nvl(Calcular_Utilidad_Anual(" + s_fecha_inicial + @", " + pEntidad.centro_costo.ToString() + @", " + pEntidad.centro_costo_fin.ToString() + @"), 0)
                                                    Else Nvl(Calcular_Utilidad(" + s_fecha_inicial + @", " + pEntidad.centro_costo.ToString() + @", " + pEntidad.centro_costo_fin.ToString() + @"), 0) 
                                                End) As utilidad 
                                        From TEMP_LIBROMAYOR t Where t.cod_cuenta = '3' And t.fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Group by t.fecha";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqltot;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["UTILIDAD"] != DBNull.Value) utilidad = Convert.ToDouble(resultado["UTILIDAD"]);
                            TotCre = TotCre - utilidad;
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstBalPru;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalancePruebaData", "ListarBalance", ex);
                        return null;
                    }
                }
            }
        }
        public List<BalancePrueba> listarbalancecentrocosto(Usuario pUsuario, string centro, string fecha)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalancePrueba> lstComponenteAdicional = new List<BalancePrueba>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        Configuracion conf = new Configuracion();
                        if (centro == "0")
                        {
                            sql = " select C.COD_CUENTA,Nombre,Sum(Valor) as Valor from " +
                                "  PLAN_CUENTAS C join Balance B on C.COD_CUENTA = B.COD_CUENTA " +
                                "  join OFICINA_CUENTA_CONTABLE OCC on OCC.COD_AUXILIAR = C.COD_CUENTA " +
                                "  join CENTRO_COSTO CC on CC.CENTRO_COSTO = B.CENTRO_COSTO " +
                                "  where B.FECHA = TO_DATE('" + fecha + "','DD,MM,YY') " +
                                "  group by C.COD_CUENTA,Nombre order by C.COD_CUENTA ";
                        }
                        else
                        {
                            sql = "select C.COD_CUENTA,Nombre,Sum(Valor) as Valor,(select sum(Valor) from Balance bl where bl.COD_CUENTA = C.COD_CUENTA and Bl.CENTRO_COSTO = " + centro + ") as centro " +
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
                            BalancePrueba entidad = new BalancePrueba();

                            if (resultado["Nombre"] != DBNull.Value) entidad.nombrecuenta = (Convert.ToString(resultado["nombre"]));
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["Valor"] != DBNull.Value) entidad.valor = Convert.ToDouble(resultado["Valor"]);
                            if (centro != "0")
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
        public List<BalancePrueba> ListarCentroCosto(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalancePrueba> lstComponenteAdicional = new List<BalancePrueba>();
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
                            BalancePrueba entidad = new BalancePrueba();


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
        public BalancePrueba ListarValorCentroCosto(string Centro, string cod_cuenta, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            BalancePrueba entidad = new BalancePrueba();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "select sum(valor) as Valor,PC.COD_CUENTA from Balance bl join PLAN_CUENTAS PC on bl.COD_CUENTA=PC.COD_CUENTA " +
                        " join OFICINA_CUENTA_CONTABLE OCC on OCC.COD_AUXILIAR = PC.COD_CUENTA " +
                        " join CENTRO_COSTO CC on CC.CENTRO_COSTO = bl.CENTRO_COSTO where CC.DESCRIPCION = '" + Centro + "' and PC.COD_CUENTA='" + cod_cuenta + "' GROUP BY PC.COD_CUENTA,Nombre   order by PC.COD_CUENTA ";




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

        public void AlertaBalance(DateTime pfecha, ref decimal Activo, ref decimal Pasivo, ref decimal Patrimonio, ref decimal Utilidad, ref decimal Diferencia, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);


            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = @"Select Distinct c.fecha,
                                    (Select Sum(b.valor) From BALANCE b Where b.fecha = c.fecha And b.cod_cuenta = '1') as ACTIVO,
                                    (Select Sum(b.valor) From BALANCE b Where b.fecha = c.fecha And b.cod_cuenta = '2') as PASIVO,
                                    (Select Sum(b.valor) From BALANCE b Where b.fecha = c.fecha And b.cod_cuenta = '3') As PATRIMONIO,
                                    CALCULAR_UTILIDAD(c.fecha, 1, 99) As UTILIDAD,
                                    ((Select Sum(b.valor) From BALANCE b Where b.fecha = c.fecha And b.cod_cuenta = '1') -
                                    (Select Sum(b.valor) From BALANCE b Where b.fecha = c.fecha And b.cod_cuenta = '2') -
                                    (Select Sum(b.valor) From BALANCE b Where b.fecha = c.fecha And b.cod_cuenta = '3')) -
                                    CALCULAR_UTILIDAD(c.fecha, 1, 99) As DIFERENCIA
                                    From cierea c
                                    Where c.tipo = 'C' And c.fecha = To_Date('" + Convert.ToDateTime(pfecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') And c.campo1 = '1' And c.estado = 'P' Order by 1, 2";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ACTIVO"] != DBNull.Value) Activo = Convert.ToDecimal(resultado["ACTIVO"]);
                            if (resultado["PASIVO"] != DBNull.Value) Pasivo = Convert.ToDecimal(resultado["PASIVO"]);
                            if (resultado["PATRIMONIO"] != DBNull.Value) Patrimonio = Convert.ToDecimal(resultado["PATRIMONIO"]);
                            if (resultado["UTILIDAD"] != DBNull.Value) Utilidad = Convert.ToDecimal(resultado["UTILIDAD"]);
                            if (resultado["DIFERENCIA"] != DBNull.Value) Diferencia = Convert.ToDecimal(resultado["DIFERENCIA"]);
                        }

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalancePruebaData", "Alerta_Balance", ex);
                    }
                }
            }
        }

        public List<BalancePrueba> ListarBalanceComprobacionTer(BalancePrueba pEntidad, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalancePrueba> lstBalPru = new List<BalancePrueba>();

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
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BALCOMPROBATER";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarBalanceComprobacion", "USP_XPINN_CON_BALCOMPROBACIONTER", ex);
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
                            sql = "Select * from TEMP_LIBROMAYOR Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta, essaldocuenta desc, cod_tercero";
                        else
                            sql = "Select * from TEMP_LIBROMAYOR Where fecha = To_Date('" + Convert.ToDateTime(pEntidad.fecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by cod_cuenta, essaldocuenta desc, cod_tercero";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalancePrueba entidad = new BalancePrueba();
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
                            if (resultado["IDEN_TERCERO"] != DBNull.Value) entidad.tercero = Convert.ToString(resultado["IDEN_TERCERO"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nombreTercero = Convert.ToString(resultado["NOM_TERCERO"]);
                            lstBalPru.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstBalPru;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalancePruebaData", "ListarBalanceComprobacionTer", ex);
                        return null;
                    }
                }
            }



        }
    }
}
