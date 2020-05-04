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
    public class LibroDiarioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public LibroDiarioData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método para consultar el libro auxiliar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<LibroDiario> ListarDiario(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta, Int64 moneda,Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LibroDiario> lstLibAux = new List<LibroDiario>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                 // Generar el libro diario organizado por cuenta

                 if (bCuenta == true)
                 {                       
                     using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                     {                    
                        cmdTransaccionFactory.Connection = connection;
                        try
                        {
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            DbParameter pfechaInicial = cmdTransaccionFactory.CreateParameter();
                            pfechaInicial.ParameterName = "pfechaInicial";
                            pfechaInicial.Value = FecIni;
                            pfechaInicial.Direction = ParameterDirection.Input;
                            pfechaInicial.DbType = DbType.Date;
                            cmdTransaccionFactory.Parameters.Add(pfechaInicial);

                            DbParameter pfechaFinal = cmdTransaccionFactory.CreateParameter();
                            pfechaFinal.ParameterName = "pfechaFinal";
                            pfechaFinal.Value = FecFin;
                            pfechaFinal.Direction = ParameterDirection.Input;
                            pfechaFinal.DbType = DbType.Date;
                            cmdTransaccionFactory.Parameters.Add(pfechaFinal);

                            DbParameter pCCInicial = cmdTransaccionFactory.CreateParameter();
                            pCCInicial.ParameterName = "pCCInicial";
                            pCCInicial.Value = CenIni;
                            pCCInicial.Direction = ParameterDirection.Input;
                            pCCInicial.DbType = DbType.Int64;
                            cmdTransaccionFactory.Parameters.Add(pCCInicial);

                            DbParameter pCCFinal = cmdTransaccionFactory.CreateParameter();
                            pCCFinal.ParameterName = "pCCFinal";
                            pCCFinal.Value = CenFin;
                            pCCFinal.Direction = ParameterDirection.Input;
                            pCCFinal.DbType = DbType.Int16;
                            cmdTransaccionFactory.Parameters.Add(pCCFinal);

                            DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                            PMONEDA.ParameterName = "PMONEDA";
                            PMONEDA.Value = moneda;
                            PMONEDA.Direction = ParameterDirection.Input;
                            PMONEDA.DbType = DbType.Int16;
                            cmdTransaccionFactory.Parameters.Add(PMONEDA);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_LIBRODIARIO";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            BOExcepcion.Throw("LibroDiarioData", "USP_XPINN_CON_LIBRODIARIO", ex);
                            return null;
                        }
                     };

                     using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                     {
                        cmdTransaccionFactory.Connection = connection;
                        try
                        {
                            Configuracion conf = new Configuracion();
                            string sql = "Select fecha, cod_cuenta, nombre, debito, credito from TEMP_LIBRODIARIO Order by Cod_Cuenta, Fecha";
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();
                            while (resultado.Read())
                            {
                                LibroDiario entidad = new LibroDiario();
                                if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                                if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE"]);
                                if (resultado["DEBITO"] != DBNull.Value) entidad.debito = Convert.ToDouble(resultado["DEBITO"]);
                                if (resultado["CREDITO"] != DBNull.Value) entidad.credito = Convert.ToDouble(resultado["CREDITO"]);
                                lstLibAux.Add(entidad);
                            }
                            return lstLibAux;
                        }
                        catch (Exception ex)
                        {
                            BOExcepcion.Throw("LibroDiarioData", "ListarDiario", ex);
                            return null;
                        }
                     };
                 }

                 // Generar el libro diario organizado por fecha
                 else
                 {
                     using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                     {
                         cmdTransaccionFactory.Connection = connection;
                         try
                         {
                             Configuracion conf = new Configuracion();
                             string sql = "";
                             if (dbConnectionFactory.TipoConexion() == "ORACLE")
                             {
                                 sql = "Select e.fecha, d.cod_cuenta, p.nombre, Sum(Case d.tipo When 'D' Then d.valor Else 0 End) As Debito, Sum(Case d.tipo When 'C' Then d.valor Else 0 End) As Credito " +
                                         "From e_comprobante e, d_comprobante d, plan_cuentas p " +
                                         "Where e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp And e.fecha between To_Date('" + FecIni.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') and To_Date('" + FecFin.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And d.cod_cuenta = p.cod_cuenta " +
                                         "Group by e.fecha, d.cod_cuenta, p.nombre " +
                                         "Order by e.fecha, d.cod_cuenta ";
                             }
                             else
                             {
                                 sql = "Select e.fecha, d.cod_cuenta, p.nombre, Sum(Case d.tipo When 'D' Then d.valor Else 0 End) As Debito, Sum(Case d.tipo When 'C' Then d.valor Else 0 End) As Credito " +
                                         "From e_comprobante e, d_comprobante d, plan_cuentas p " +
                                         "Where e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp And e.fecha between '" + FecIni.ToString(conf.ObtenerFormatoFecha()) + "' and '" + FecFin.ToString(conf.ObtenerFormatoFecha()) + "' And d.cod_cuenta = p.cod_cuenta " +
                                         "Group by e.fecha, d.cod_cuenta, p.nombre " +
                                         "Order by e.fecha, d.cod_cuenta ";
                             }
                             cmdTransaccionFactory.CommandType = CommandType.Text;
                             cmdTransaccionFactory.CommandText = sql;
                             resultado = cmdTransaccionFactory.ExecuteReader();
                             while (resultado.Read())
                             {
                                 LibroDiario entidad = new LibroDiario();
                                 if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                                 if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                                 if (resultado["NOMBRE"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE"]);
                                 if (resultado["DEBITO"] != DBNull.Value) entidad.debito = Convert.ToDouble(resultado["DEBITO"]);
                                 if (resultado["CREDITO"] != DBNull.Value) entidad.credito = Convert.ToDouble(resultado["CREDITO"]);
                                 lstLibAux.Add(entidad);
                             }
                             return lstLibAux;
                         }
                         catch (Exception ex)
                         {
                             BOExcepcion.Throw("LibroDiarioData", "ListarDiario", ex);
                             return null;
                         }
                     }
                 }              
            }
        }



        public List<LibroDiario> ListarDiarioNiif(Int64 CenIni, Int64 CenFin, DateTime FecIni, DateTime FecFin, Boolean bCuenta, Int64 moneda, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LibroDiario> lstLibAux = new List<LibroDiario>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();
                // Generar el libro diario organizado por cuenta
                if (bCuenta == true)
                {
                    using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                    {
                        cmdTransaccionFactory.Connection = connection;
                        try
                        {
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            DbParameter pfechaInicial = cmdTransaccionFactory.CreateParameter();
                            pfechaInicial.ParameterName = "pfechaInicial";
                            pfechaInicial.Value = FecIni;
                            pfechaInicial.Direction = ParameterDirection.Input;
                            pfechaInicial.DbType = DbType.Date;
                            cmdTransaccionFactory.Parameters.Add(pfechaInicial);
                            DbParameter pfechaFinal = cmdTransaccionFactory.CreateParameter();
                            pfechaFinal.ParameterName = "pfechaFinal";
                            pfechaFinal.Value = FecFin;
                            pfechaFinal.Direction = ParameterDirection.Input;
                            pfechaFinal.DbType = DbType.Date;
                            cmdTransaccionFactory.Parameters.Add(pfechaFinal);
                            DbParameter pCCInicial = cmdTransaccionFactory.CreateParameter();
                            pCCInicial.ParameterName = "pCCInicial";
                            pCCInicial.Value = CenIni;
                            pCCInicial.Direction = ParameterDirection.Input;
                            pCCInicial.DbType = DbType.Int64;
                            cmdTransaccionFactory.Parameters.Add(pCCInicial);
                            DbParameter pCCFinal = cmdTransaccionFactory.CreateParameter();
                            pCCFinal.ParameterName = "pCCFinal";
                            pCCFinal.Value = CenFin;
                            pCCFinal.Direction = ParameterDirection.Input;
                            pCCFinal.DbType = DbType.Int16;
                            cmdTransaccionFactory.Parameters.Add(pCCFinal);

                            DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                            PMONEDA.ParameterName = "PMONEDA";
                            PMONEDA.Value = moneda;
                            PMONEDA.Direction = ParameterDirection.Input;
                            PMONEDA.DbType = DbType.Int16;
                            cmdTransaccionFactory.Parameters.Add(PMONEDA);

                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_LIBRODIARIONIIF";
                            cmdTransaccionFactory.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            BOExcepcion.Throw("LibroDiarioData", "USP_XPINN_CON_LIBRODIARIONIIF", ex);
                            return null;
                        }
                    };
                    using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                    {
                        cmdTransaccionFactory.Connection = connection;
                        try
                        {
                            Configuracion conf = new Configuracion();
                            string sql = "Select fecha, cod_cuenta, nombre, debito, credito from TEMP_LIBRODIARIO Order by Cod_Cuenta, Fecha";
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();
                            while (resultado.Read())
                            {
                                LibroDiario entidad = new LibroDiario();
                                if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                                if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE"]);
                                if (resultado["DEBITO"] != DBNull.Value) entidad.debito = Convert.ToDouble(resultado["DEBITO"]);
                                if (resultado["CREDITO"] != DBNull.Value) entidad.credito = Convert.ToDouble(resultado["CREDITO"]);
                                lstLibAux.Add(entidad);
                            }
                            return lstLibAux;
                        }
                        catch (Exception ex)
                        {
                            BOExcepcion.Throw("LibroDiarioData", "ListarDiarioNiif", ex);
                            return null;
                        }
                    };
                }
                // Generar el libro diario organizado por fecha
                else
                {
                    using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                    {
                        cmdTransaccionFactory.Connection = connection;
                        try
                        {
                            Configuracion conf = new Configuracion();
                            string sql = "";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                sql = @"Select e.fecha, d.cod_cuenta_niif, p.nombre, Sum(Case d.tipo When 'D' Then d.valor Else 0 End) As Debito, Sum(Case d.tipo When 'C' Then d.valor Else 0 End) As Credito 
                                        From e_comprobante e, d_comprobante d, plan_cuentas_niif p 
                                        Where e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp And d.cod_cuenta_niif = p.cod_cuenta_niif
                                        And e.fecha between To_Date('" + FecIni.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') 
                                        and To_Date('" + FecFin.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') 
                                        Group by e.fecha, d.cod_cuenta_niif, p.nombre 
                                        Order by e.fecha, d.cod_cuenta_niif ";
                            }
                            else
                            {
                                sql = @"Select e.fecha, d.cod_cuenta_niif, p.nombre, Sum(Case d.tipo When 'D' Then d.valor Else 0 End) As Debito, Sum(Case d.tipo When 'C' Then d.valor Else 0 End) As Credito 
                                        From e_comprobante e, d_comprobante d, plan_cuentas_niif p 
                                        Where e.num_comp = d.num_comp And e.tipo_comp = d.tipo_comp And d.cod_cuenta_niif = p.cod_cuenta_niif 
                                        And e.fecha between '" + FecIni.ToString(conf.ObtenerFormatoFecha()) + "' and '" + FecFin.ToString(conf.ObtenerFormatoFecha()) + @"'
                                        Group by e.fecha, d.cod_cuenta_niif, p.nombre 
                                        Order by e.fecha, d.cod_cuenta_niif ";
                            }
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();
                            while (resultado.Read())
                            {
                                LibroDiario entidad = new LibroDiario();
                                if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                                if (resultado["COD_CUENTA_NIIF"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA_NIIF"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE"]);
                                if (resultado["DEBITO"] != DBNull.Value) entidad.debito = Convert.ToDouble(resultado["DEBITO"]);
                                if (resultado["CREDITO"] != DBNull.Value) entidad.credito = Convert.ToDouble(resultado["CREDITO"]);
                                lstLibAux.Add(entidad);
                            }
                            return lstLibAux;
                        }
                        catch (Exception ex)
                        {
                            BOExcepcion.Throw("LibroDiarioData", "ListarDiarioNiif", ex);
                            return null;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Determinar la fecha de cierre inicial para un tipo de cierre dato
        /// </summary>
        /// <param name="ptipocierre">el tipo de cierre a verificar</param>
        /// <returns></returns>
        public DateTime DeterminarFechaInicial(string ptipocierre, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DateTime fecha_cierre = DateTime.MinValue;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select Min(fecha) As fecha From cierea Where tipo = '" + ptipocierre + "' And estado = 'D' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            if (resultado["FECHA"] != DBNull.Value) fecha_cierre = Convert.ToDateTime(resultado["FECHA"]);
                        }
                        return fecha_cierre;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroAuxiliarData", "CargarCuentas", ex);
                        return DateTime.MinValue;
                    }
                }
            }
        }

        public List<LibroDiario> ListarLibroDiario(LibroDiario pentidad,Usuario vUsuario, ref Double TotDeb, ref Double TotCre)
        {
            DbDataReader resultado = default(DbDataReader);
            List<LibroDiario> lstMayor = new List<LibroDiario>();
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

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        DbParameter pfechaInicial = cmdTransaccionFactory.CreateParameter();
                        pfechaInicial.ParameterName = "pfechaInicial";
                        pfechaInicial.Value =pentidad.fecha_ini;
                        pfechaInicial.Direction = ParameterDirection.Input;
                        pfechaInicial.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechaInicial);

                        DbParameter pfechaFinal = cmdTransaccionFactory.CreateParameter();
                        pfechaFinal.ParameterName = "pfechaFinal";
                        pfechaFinal.Value = pentidad.fecha_fin;
                        pfechaFinal.Direction = ParameterDirection.Input;
                        pfechaFinal.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechaFinal);

                        DbParameter PCENINI = cmdTransaccionFactory.CreateParameter();
                        PCENINI.ParameterName = "PCENINI";
                        PCENINI.Value = pentidad.cenini;
                        PCENINI.Direction = ParameterDirection.Input;
                        PCENINI.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENINI);

                        DbParameter PCENFIN = cmdTransaccionFactory.CreateParameter();
                        PCENFIN.ParameterName = "PCENFIN";
                        PCENFIN.Value = pentidad.cenfin;
                        PCENFIN.Direction = ParameterDirection.Input;
                        PCENFIN.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCENFIN);

                        DbParameter PNIVEL = cmdTransaccionFactory.CreateParameter();
                        PNIVEL.ParameterName = "PNIVEL";
                        PNIVEL.Value = pentidad.nivelint;
                        PNIVEL.Direction = ParameterDirection.Input;
                        PNIVEL.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PNIVEL);

                        DbParameter PCUENTASENCERO = cmdTransaccionFactory.CreateParameter();
                        PCUENTASENCERO.ParameterName = "PCUENTASENCERO";
                        PCUENTASENCERO.Value = pentidad.mostrarceros;
                        PCUENTASENCERO.Direction = ParameterDirection.Input;
                        PCUENTASENCERO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PCUENTASENCERO);

                        DbParameter PPORTERCEROS = cmdTransaccionFactory.CreateParameter();
                        PPORTERCEROS.ParameterName = "PPORTERCEROS";
                        PPORTERCEROS.Value = pentidad.generarterceros;
                        PPORTERCEROS.Direction = ParameterDirection.Input;
                        PPORTERCEROS.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PPORTERCEROS);

                        DbParameter PNIVELSELECCIONADO = cmdTransaccionFactory.CreateParameter();
                        PNIVELSELECCIONADO.ParameterName = "PNIVELSELECCIONADO";
                        PNIVELSELECCIONADO.Value = pentidad.solonivel;
                        PNIVELSELECCIONADO.Direction = ParameterDirection.Input;
                        PNIVELSELECCIONADO.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PNIVELSELECCIONADO);

                        DbParameter PEXCEDENTES = cmdTransaccionFactory.CreateParameter();
                        PEXCEDENTES.ParameterName = "PEXCEDENTES";
                        PEXCEDENTES.Value = pentidad.excedentes;
                        PEXCEDENTES.Direction = ParameterDirection.Input;
                        PEXCEDENTES.DbType = DbType.Int16;
                        cmdTransaccionFactory.Parameters.Add(PEXCEDENTES);

                        DbParameter PMOSTRARMOVPER13 = cmdTransaccionFactory.CreateParameter();
                        PMOSTRARMOVPER13.ParameterName = "PMOSTRARMOVPER13";
                        if (pentidad.mostrarmovper13 == null)
                            PMOSTRARMOVPER13.Value = DBNull.Value;
                        else
                            PMOSTRARMOVPER13.Value = pentidad.mostrarmovper13;
                        PMOSTRARMOVPER13.Direction = ParameterDirection.Output;
                        PMOSTRARMOVPER13.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PMOSTRARMOVPER13);

                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "pMoneda";
                        PMONEDA.Value = pentidad.cod_moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText =  "USP_XPINN_CON_LIBDIARIO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroDiarioData", "USP_XPINN_CON_LIBDIARIO", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select * from TEMP_LIBROMAYOR Order by cod_cuenta";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LibroDiario entidad = new LibroDiario();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombre_cuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["COD_TERCERO"] != DBNull.Value) entidad.cod_tercero = Convert.ToInt64(resultado["COD_TERCERO"]);
                            if (resultado["IDEN_TERCERO"] != DBNull.Value) entidad.iden_tercero = Convert.ToString(resultado["IDEN_TERCERO"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nom_tercero = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["SALDO_INICIAL_DEBITO"] != DBNull.Value) entidad.saldo_inicial_debito = Convert.ToDouble(resultado["SALDO_INICIAL_DEBITO"]);
                            if (resultado["SALDO_INICIAL_CREDITO"] != DBNull.Value) entidad.saldo_inicial_credito = Convert.ToDouble(resultado["SALDO_INICIAL_CREDITO"]);
                            if (resultado["DEBITO"] != DBNull.Value) entidad.debito = Convert.ToDouble(resultado["DEBITO"]);
                            if (resultado["CREDITO"] != DBNull.Value) entidad.credito = Convert.ToDouble(resultado["CREDITO"]);
                            if (resultado["SALDO_FINAL_DEBITO"] != DBNull.Value) entidad.saldo_final_debito = Convert.ToDouble(resultado["SALDO_FINAL_DEBITO"]);
                            if (resultado["SALDO_FINAL_CREDITO"] != DBNull.Value) entidad.saldo_final_credito = Convert.ToDouble(resultado["SALDO_FINAL_CREDITO"]);
                            lstMayor.Add(entidad);
                        }

                        string sqltot = "";
                        if (pentidad.solonivel == 1)
                            sqltot = "Select Sum(debito) As debito, Sum(credito) As credito from TEMP_LIBROMAYOR  ";
                        else
                            sqltot = "Select Sum(debito) As debito, Sum(credito) As credito from TEMP_LIBROMAYOR Where  nivel = 3";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sqltot;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["DEBITO"] != DBNull.Value) TotDeb = Convert.ToDouble(resultado["DEBITO"]);
                            if (resultado["CREDITO"] != DBNull.Value) TotCre = Convert.ToDouble(resultado["CREDITO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return lstMayor;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LibroDiarioData", "ListarLibroDiario", ex);
                        return null;
                    }
                }
            }
        }



    }
}
