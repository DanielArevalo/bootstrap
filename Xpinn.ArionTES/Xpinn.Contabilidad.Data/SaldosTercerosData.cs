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
    public class SaldosTercerosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public SaldosTercerosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        /// <summary>
        /// Método para consultar el libro SaldosTerceros Consolidado
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<SaldosTerceros> ListarSaldoConsolidado(SaldosTerceros pEntidad, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SaldosTerceros> lstSaldosTerceros = new List<SaldosTerceros>();
            SalIni = 0;
            TotDeb = 0;
            TotCre = 0;
            SalFin = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
              {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                       
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pFechaInicial = cmdTransaccionFactory.CreateParameter();
                        pFechaInicial.ParameterName = "pFechaInicial";
                        pFechaInicial.Value = pEntidad.fechaini;
                        pFechaInicial.Direction = ParameterDirection.Input;
                        pFechaInicial.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pFechaInicial);
                        
                        DbParameter pFechaFinal = cmdTransaccionFactory.CreateParameter();
                        pFechaFinal.ParameterName = "pFechaFinal";
                        pFechaFinal.Value = pEntidad.fechafin;
                        pFechaFinal.Direction = ParameterDirection.Input;
                        pFechaFinal.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pFechaFinal);

                        DbParameter pCodCuenta = cmdTransaccionFactory.CreateParameter();
                        pCodCuenta.ParameterName = "pCodCuenta";
                        pCodCuenta.Value = pEntidad.cod_cuenta;
                        pCodCuenta.Direction = ParameterDirection.Input;
                        pCodCuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pCodCuenta);

                        DbParameter pMoneda = cmdTransaccionFactory.CreateParameter();
                        pMoneda.ParameterName = "pMoneda";
                        pMoneda.Value = pEntidad.cod_moneda;
                        pMoneda.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pMoneda);
                        
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_SALDOSTERCON";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldosTercerosData", "USP_XPINN_CON_SALDOSTERCON", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select cod_cuenta, nombre_cuenta, cod_ter, identificacion, nombre_tercero,centro_costo, centro_gestion, Sum(saldo_inicial) as SALDO_INICIAL, Sum(debitos) as DEBITOS,Sum(creditos) as CREDITOS, Sum(saldo) as SALDO From TEMP_SALDOTERCEROS  Group by cod_cuenta, nombre_cuenta, cod_ter, identificacion, nombre_tercero,centro_costo, centro_gestion Order by cod_cuenta, cod_ter,centro_costo, centro_gestion";                       
                        
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SaldosTerceros entidad = new SaldosTerceros();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["COD_TER"] != DBNull.Value) entidad.codtercero = Convert.ToString(resultado["COD_TER"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identercero = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE_TERCERO"] != DBNull.Value) entidad.nombretercero= Convert.ToString(resultado["NOMBRE_TERCERO"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt64(resultado["CENTRO_GESTION"]);
                            if (resultado["SALDO_INICIAL"] != DBNull.Value) entidad.saldoinicial = Convert.ToDouble(resultado["SALDO_INICIAL"]);
                            if (resultado["DEBITOS"] != DBNull.Value) entidad.debito = Convert.ToDouble(resultado["DEBITOS"]);
                            if (resultado["CREDITOS"] != DBNull.Value) entidad.credito = Convert.ToDouble(resultado["CREDITOS"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldofinal = Convert.ToDouble(resultado["SALDO"]);

                            if (entidad.saldoinicial != null)
                                SalIni += entidad.saldoinicial;
                            if (entidad.debito != null)
                                TotDeb += entidad.debito;
                            if (entidad.credito != null)
                                TotCre += entidad.credito;
                            if (entidad.saldofinal != null)
                                SalFin += entidad.saldofinal;

                            lstSaldosTerceros.Add(entidad);
                        }

                        return lstSaldosTerceros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldosTercerosData", "ListarSaldoConsolidado", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para consultar el libro SaldosTerceros 
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<SaldosTerceros> ListarSaldosTerceros(SaldosTerceros pEntidad, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SaldosTerceros> lstSaldosTerceros = new List<SaldosTerceros>();
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

                        DbParameter pFechaInicial = cmdTransaccionFactory.CreateParameter();
                        pFechaInicial.ParameterName = "pFechaInicial";
                        pFechaInicial.Value = pEntidad.fechaini;
                        pFechaInicial.Direction = ParameterDirection.Input;
                        pFechaInicial.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pFechaInicial);

                        DbParameter pFechaFinal = cmdTransaccionFactory.CreateParameter();
                        pFechaFinal.ParameterName = "pFechaFinal";
                        pFechaFinal.Value = pEntidad.fechafin;
                        pFechaFinal.Direction = ParameterDirection.Input;
                        pFechaFinal.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pFechaFinal);

                        DbParameter pCodCuenta = cmdTransaccionFactory.CreateParameter();
                        pCodCuenta.ParameterName = "pCodCuenta";
                        if (pEntidad.cod_cuenta == null)
                            pCodCuenta.Value = DBNull.Value;
                        else
                            pCodCuenta.Value = pEntidad.cod_cuenta;
                        pCodCuenta.Direction = ParameterDirection.Input;
                        pCodCuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pCodCuenta);

                        DbParameter pCentroCostoIn = cmdTransaccionFactory.CreateParameter();
                        pCentroCostoIn.ParameterName = "lcentro_inicial";
                        pCentroCostoIn.Value = pEntidad.centro_costo;
                        pCentroCostoIn.Direction = ParameterDirection.Input;
                        pCentroCostoIn.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCentroCostoIn);

                        DbParameter pCentroCostoFin = cmdTransaccionFactory.CreateParameter();
                        pCentroCostoFin.ParameterName = "lcentro_final";
                        pCentroCostoFin.Value = pEntidad.centro_costo_fin;
                        pCentroCostoFin.Direction = ParameterDirection.Input;
                        pCentroCostoFin.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCentroCostoFin);

                        DbParameter pMoneda = cmdTransaccionFactory.CreateParameter();
                        pMoneda.ParameterName = "pMoneda";
                        pMoneda.Value = pEntidad.cod_moneda;
                        pMoneda.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pMoneda);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_SALDOSTER";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldosTercerosData", "USP_XPINN_CON_SALDOSTER", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = @"Select cod_cuenta, nombre_cuenta, cod_ter, identificacion, nombre_tercero, centro_costo, centro_gestion, Sum(saldo_inicial) as SALDO_INICIAL, Sum(debitos) as DEBITOS,Sum(creditos) as CREDITOS, Sum(saldo) as SALDO 
                                        From TEMP_SALDOTERCEROS  
                                        Group by cod_cuenta, nombre_cuenta, cod_ter, identificacion, nombre_tercero, centro_costo, centro_gestion 
                                        Order by cod_cuenta, cod_ter, centro_costo, centro_gestion";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SaldosTerceros entidad = new SaldosTerceros();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["COD_TER"] != DBNull.Value) entidad.codtercero = Convert.ToString(resultado["COD_TER"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identercero = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE_TERCERO"] != DBNull.Value) entidad.nombretercero = Convert.ToString(resultado["NOMBRE_TERCERO"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt64(resultado["CENTRO_GESTION"]);
                            if (resultado["SALDO_INICIAL"] != DBNull.Value) entidad.saldoinicial = Convert.ToDouble(resultado["SALDO_INICIAL"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldofinal = Convert.ToDouble(resultado["SALDO"]);
                            if (resultado["DEBITOS"] != DBNull.Value) entidad.debito = Convert.ToDouble(resultado["DEBITOS"]);
                            if (resultado["CREDITOS"] != DBNull.Value) entidad.credito = Convert.ToDouble(resultado["CREDITOS"]);

                            if (entidad.saldoinicial != null)
                                SalIni += entidad.saldoinicial;
                            if (entidad.debito != null)
                                TotDeb += entidad.debito;
                            if (entidad.credito != null)
                                TotCre += entidad.credito;
                            if (entidad.saldofinal != null)
                                SalFin += entidad.saldofinal;

                            lstSaldosTerceros.Add(entidad);
                        }

                        return lstSaldosTerceros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldosTercerosData", "ListarSaldoConsolidado", ex);
                        return null;
                    }
                }
            }
        }

        
        //METODOS PARA NIIF
        public List<SaldosTerceros> ListarSaldoConsolidadoNIIF(SaldosTerceros pEntidad, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SaldosTerceros> lstSaldosTerceros = new List<SaldosTerceros>();
            SalIni = 0;
            TotDeb = 0;
            TotCre = 0;
            SalFin = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pFechaInicial = cmdTransaccionFactory.CreateParameter();
                        pFechaInicial.ParameterName = "pFechaInicial";
                        pFechaInicial.Value = pEntidad.fechaini;
                        pFechaInicial.Direction = ParameterDirection.Input;
                        pFechaInicial.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pFechaInicial);

                        DbParameter pFechaFinal = cmdTransaccionFactory.CreateParameter();
                        pFechaFinal.ParameterName = "pFechaFinal";
                        pFechaFinal.Value = pEntidad.fechafin;
                        pFechaFinal.Direction = ParameterDirection.Input;
                        pFechaFinal.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pFechaFinal);

                        DbParameter pCodCuenta = cmdTransaccionFactory.CreateParameter();
                        pCodCuenta.ParameterName = "pCodCuenta";
                        pCodCuenta.Value = pEntidad.cod_cuenta;
                        pCodCuenta.Direction = ParameterDirection.Input;
                        pCodCuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pCodCuenta);

                        DbParameter pMoneda = cmdTransaccionFactory.CreateParameter();
                        pMoneda.ParameterName = "pMoneda";
                        pMoneda.Value = pEntidad.cod_moneda;
                        pMoneda.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pMoneda);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_SALDOSTERCONIIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldosTercerosData", "USP_XPINN_CON_SALDOSTERCONIIF", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select cod_cuenta, nombre_cuenta, cod_ter, identificacion, nombre_tercero,centro_costo, centro_gestion, Sum(saldo_inicial) as SALDO_INICIAL, Sum(debitos) as DEBITOS,Sum(creditos) as CREDITOS, Sum(saldo) as SALDO From TEMP_SALDOTERCEROS  Group by cod_cuenta, nombre_cuenta, cod_ter, identificacion, nombre_tercero,centro_costo, centro_gestion Order by cod_cuenta, cod_ter,centro_costo, centro_gestion";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SaldosTerceros entidad = new SaldosTerceros();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["COD_TER"] != DBNull.Value) entidad.codtercero = Convert.ToString(resultado["COD_TER"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identercero = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE_TERCERO"] != DBNull.Value) entidad.nombretercero = Convert.ToString(resultado["NOMBRE_TERCERO"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt64(resultado["CENTRO_GESTION"]);
                            if (resultado["SALDO_INICIAL"] != DBNull.Value) entidad.saldoinicial = Convert.ToDouble(resultado["SALDO_INICIAL"]);
                            if (resultado["DEBITOS"] != DBNull.Value) entidad.debito = Convert.ToDouble(resultado["DEBITOS"]);
                            if (resultado["CREDITOS"] != DBNull.Value) entidad.credito = Convert.ToDouble(resultado["CREDITOS"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldofinal = Convert.ToDouble(resultado["SALDO"]);

                            if (entidad.saldoinicial != null)
                                SalIni += entidad.saldoinicial;
                            if (entidad.debito != null)
                                TotDeb += entidad.debito;
                            if (entidad.credito != null)
                                TotCre += entidad.credito;
                            if (entidad.saldofinal != null)
                                SalFin += entidad.saldofinal;

                            lstSaldosTerceros.Add(entidad);
                        }

                        return lstSaldosTerceros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldosTercerosData", "ListarSaldoConsolidadoNIIF", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para consultar el libro SaldosTerceros 
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<SaldosTerceros> ListarSaldosTercerosNIIF(SaldosTerceros pEntidad, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SaldosTerceros> lstSaldosTerceros = new List<SaldosTerceros>();
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

                        DbParameter pFechaInicial = cmdTransaccionFactory.CreateParameter();
                        pFechaInicial.ParameterName = "pFechaInicial";
                        pFechaInicial.Value = pEntidad.fechaini;
                        pFechaInicial.Direction = ParameterDirection.Input;
                        pFechaInicial.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pFechaInicial);

                        DbParameter pFechaFinal = cmdTransaccionFactory.CreateParameter();
                        pFechaFinal.ParameterName = "pFechaFinal";
                        pFechaFinal.Value = pEntidad.fechafin;
                        pFechaFinal.Direction = ParameterDirection.Input;
                        pFechaFinal.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pFechaFinal);

                        DbParameter pCodCuenta = cmdTransaccionFactory.CreateParameter();
                        pCodCuenta.ParameterName = "pCodCuenta";
                        if (pEntidad.cod_cuenta == null)
                            pCodCuenta.Value = DBNull.Value;
                        else
                            pCodCuenta.Value = pEntidad.cod_cuenta;
                        pCodCuenta.Direction = ParameterDirection.Input;
                        pCodCuenta.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pCodCuenta);

                        DbParameter pCentroCostoIn = cmdTransaccionFactory.CreateParameter();
                        pCentroCostoIn.ParameterName = "lcentro_inicial";
                        pCentroCostoIn.Value = pEntidad.centro_costo;
                        pCentroCostoIn.Direction = ParameterDirection.Input;
                        pCentroCostoIn.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCentroCostoIn);

                        DbParameter pCentroCostoFin = cmdTransaccionFactory.CreateParameter();
                        pCentroCostoFin.ParameterName = "lcentro_final";
                        pCentroCostoFin.Value = pEntidad.centro_costo_fin;
                        pCentroCostoFin.Direction = ParameterDirection.Input;
                        pCentroCostoFin.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCentroCostoFin);

                        DbParameter pMoneda = cmdTransaccionFactory.CreateParameter();
                        pMoneda.ParameterName = "pMoneda";
                        pMoneda.Value = pEntidad.cod_moneda;
                        pMoneda.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pMoneda);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CON_SALDOSTERNIIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldosTercerosData", "USP_XPINN_CON_SALDOSTERNIIF", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = @"Select cod_cuenta, nombre_cuenta, cod_ter, identificacion, nombre_tercero, centro_costo, centro_gestion, Sum(saldo_inicial) as SALDO_INICIAL, Sum(debitos) as DEBITOS,Sum(creditos) as CREDITOS, Sum(saldo) as SALDO 
                                        From TEMP_SALDOTERCEROS  
                                        Group by cod_cuenta, nombre_cuenta, cod_ter, identificacion, nombre_tercero, centro_costo, centro_gestion 
                                        Order by cod_cuenta, cod_ter, centro_costo, centro_gestion";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SaldosTerceros entidad = new SaldosTerceros();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["COD_TER"] != DBNull.Value) entidad.codtercero = Convert.ToString(resultado["COD_TER"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identercero = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE_TERCERO"] != DBNull.Value) entidad.nombretercero = Convert.ToString(resultado["NOMBRE_TERCERO"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            if (resultado["CENTRO_GESTION"] != DBNull.Value) entidad.centro_gestion = Convert.ToInt64(resultado["CENTRO_GESTION"]);
                            if (resultado["SALDO_INICIAL"] != DBNull.Value) entidad.saldoinicial = Convert.ToDouble(resultado["SALDO_INICIAL"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldofinal = Convert.ToDouble(resultado["SALDO"]);
                            if (resultado["DEBITOS"] != DBNull.Value) entidad.debito = Convert.ToDouble(resultado["DEBITOS"]);
                            if (resultado["CREDITOS"] != DBNull.Value) entidad.credito = Convert.ToDouble(resultado["CREDITOS"]);

                            if (entidad.saldoinicial != null)
                                SalIni += entidad.saldoinicial;
                            if (entidad.debito != null)
                                TotDeb += entidad.debito;
                            if (entidad.credito != null)
                                TotCre += entidad.credito;
                            if (entidad.saldofinal != null)
                                SalFin += entidad.saldofinal;

                            lstSaldosTerceros.Add(entidad);
                        }

                        return lstSaldosTerceros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldosTercerosData", "ListarSaldosTercerosNIIF", ex);
                        return null;
                    }
                }
            }
        }

        public SaldosTerceros CrearTrasladoSaldoTer(SaldosTerceros pEntidad, Usuario vUsuario)
        {
            SaldosTerceros vEntidad = new SaldosTerceros();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {                

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        
                        DbParameter p_cod_traslado = cmdTransaccionFactory.CreateParameter();
                        p_cod_traslado.ParameterName = "p_cod_traslado";
                        p_cod_traslado.Value = pEntidad.cod_traslado;
                        p_cod_traslado.Direction = ParameterDirection.Output;
                        p_cod_traslado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_traslado);
                        
                        DbParameter p_fecha = cmdTransaccionFactory.CreateParameter();
                        p_fecha.ParameterName = "p_fecha";
                        p_fecha.Value = pEntidad.fechaini;
                        p_fecha.Direction = ParameterDirection.Input;
                        p_fecha.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(p_fecha);

                        DbParameter p_cuenta = cmdTransaccionFactory.CreateParameter();
                        p_cuenta.ParameterName = "p_cuenta";
                        p_cuenta.Value = pEntidad.cod_cuenta;
                        p_cuenta.Direction = ParameterDirection.Input;
                        p_cuenta.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cuenta);

                        DbParameter p_cod_tercero = cmdTransaccionFactory.CreateParameter();
                        p_cod_tercero.ParameterName = "p_cod_tercero";
                        p_cod_tercero.Value = pEntidad.codtercero;
                        p_cod_tercero.Direction = ParameterDirection.Input;
                        p_cod_tercero.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_tercero);

                        DbParameter p_cod_usuario = cmdTransaccionFactory.CreateParameter();
                        p_cod_usuario.ParameterName = "p_cod_usuario";
                        p_cod_usuario.Value = vUsuario.codusuario;
                        p_cod_usuario.Direction = ParameterDirection.Input;
                        p_cod_usuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_usuario);

                        DbParameter p_saldo_traslado = cmdTransaccionFactory.CreateParameter();
                        p_saldo_traslado.ParameterName = "p_saldo_traslado";
                        p_saldo_traslado.Value = pEntidad.saldo;
                        p_saldo_traslado.Direction = ParameterDirection.Input;
                        p_saldo_traslado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_saldo_traslado);

                        DbParameter p_tipo_mov = cmdTransaccionFactory.CreateParameter();
                        p_tipo_mov.ParameterName = "p_tipo_mov";
                        p_tipo_mov.Value = pEntidad.tipo_mov;
                        p_tipo_mov.Direction = ParameterDirection.Input;
                        p_tipo_mov.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_mov);
                        
                        DbParameter p_dir_ip = cmdTransaccionFactory.CreateParameter();
                        p_dir_ip.ParameterName = "p_dir_ip";
                        p_dir_ip.Value = vUsuario.IP;
                        p_dir_ip.Direction = ParameterDirection.Input;
                        p_dir_ip.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_dir_ip);

                        connection.Open();
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TRASLADO_SALDO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.cod_traslado = Convert.ToInt64(p_cod_traslado.Value);
                        return pEntidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldoTercerosData", "CrearTrasladoSaldoTer", ex);
                        return null;
                    }
                };
                                
            }
        }

        public SaldosTerceros CrearTrasladoSaldoTerDet(SaldosTerceros pEntidad, Usuario vUsuario)
        {
            SaldosTerceros vEntidad = new SaldosTerceros();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        DbParameter p_cod_traslado_det = cmdTransaccionFactory.CreateParameter();
                        p_cod_traslado_det.ParameterName = "p_cod_traslado_det";
                        p_cod_traslado_det.Value = pEntidad.cod_traslado_det;
                        p_cod_traslado_det.Direction = ParameterDirection.Output;
                        p_cod_traslado_det.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_traslado_det);

                        DbParameter p_cod_traslado = cmdTransaccionFactory.CreateParameter();
                        p_cod_traslado.ParameterName = "p_cod_traslado";
                        p_cod_traslado.Value = pEntidad.cod_traslado;
                        p_cod_traslado.Direction = ParameterDirection.Input;
                        p_cod_traslado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_traslado);

                        DbParameter p_cod_tercero_ant = cmdTransaccionFactory.CreateParameter();
                        p_cod_tercero_ant.ParameterName = "p_cod_tercero_ant";
                        if (pEntidad.codtercero != null)
                            p_cod_tercero_ant.Value = pEntidad.codtercero;
                        else
                            p_cod_tercero_ant.Value = DBNull.Value;
                        p_cod_tercero_ant.Direction = ParameterDirection.Input;
                        p_cod_tercero_ant.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_tercero_ant);

                        DbParameter p_valor_traslado = cmdTransaccionFactory.CreateParameter();
                        p_valor_traslado.ParameterName = "p_valor_traslado";
                        p_valor_traslado.Value = pEntidad.saldo;
                        p_valor_traslado.Direction = ParameterDirection.Input;
                        p_valor_traslado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valor_traslado);

                        DbParameter p_tipo_mov = cmdTransaccionFactory.CreateParameter();
                        p_tipo_mov.ParameterName = "p_tipo_mov";
                        p_tipo_mov.Value = pEntidad.tipo_mov;
                        p_tipo_mov.Direction = ParameterDirection.Input;
                        p_tipo_mov.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_mov);

                        DbParameter p_centro_costo = cmdTransaccionFactory.CreateParameter();
                        p_centro_costo.ParameterName = "p_centro_costo";
                        p_centro_costo.Value = pEntidad.centro_costo;
                        p_centro_costo.Direction = ParameterDirection.Input;
                        p_centro_costo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_centro_costo);

                        connection.Open();
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TRASLSALDO_DET_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pEntidad.cod_traslado_det = Convert.ToInt64(p_cod_traslado_det.Value);
                        return pEntidad;

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldoTercerosData", "CrearTrasladoSaldoTerDet", ex);
                        return null;
                    }
                };

            }
        }

        public List<SaldosTerceros> ListarTercerosTraslado(DateTime pFecha, Int64 cod_cuenta, Int64 centro_costo, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<SaldosTerceros> lstSaldosTerceros = new List<SaldosTerceros>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {                
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        //string sql = @"Select e.fecha, d.tercero, d.nom_tercero, d.identificacion, Case d.tipo When 'C' Then 'Credito' When 'D' Then 'Debito'Else null End as Tipo, d.valor, d.centro_costo 
                        //                From d_comprobante d Inner Join e_comprobante e On d.num_comp = e.num_comp
                        //                And e.tipo_comp = d.tipo_comp Where d.cod_cuenta = '" + cod_cuenta+ @"' 
                        //                And e.fecha BETWEEN (Select Case When Max(t.cod_traslado) is null Then FechaCierreContable() + 1
                        //                When Max(t.fecha)+1 < FechaCierreContable() +1 Then FechaCierreContable() + 1  
                        //                When Max(t.fecha)+1 > FechaCierreContable() + 1 Then Max(t.fecha)+1
                        //                else FechaCierreContable() + 1 END AS FECHA From Traslado_saldo_ter t Where t.cod_cuenta = '" + cod_cuenta+ @"') 
                        //                And To_Date('" + pFecha.ToShortDateString()+"','"+conf.ObtenerFormatoFecha()+"') Order by e.fecha, To_number(d.identificacion)";

                        string sql = @"Select t.fecha, t.cod_ter As Tercero, p.nombre As nom_tercero, p.identificacion, Case p.tipo When 'C' Then 'Credito' When 'D' Then 'Debito'Else null End as Tipo, t.saldo_fin As valor, t.centro_costo 
                                        From balance_ter t Left Join v_persona p On t.cod_ter = p.cod_persona Join plan_cuentas p On t.cod_cuenta = p.cod_cuenta
                                        Where t.fecha = To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + @"', '" + conf.ObtenerFormatoFecha() + @"') 
                                        And t.cod_cuenta = '" + cod_cuenta + "' " + (centro_costo > 0 ? " And t.centro_costo = " + centro_costo : "") + @"
                                        Order by t.fecha, t.cod_ter";

                        connection.Open();
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SaldosTerceros entidad = new SaldosTerceros();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fechaini = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["TERCERO"] != DBNull.Value) entidad.codtercero = Convert.ToString(resultado["TERCERO"]);
                            if (resultado["NOM_TERCERO"] != DBNull.Value) entidad.nombretercero = Convert.ToString(resultado["NOM_TERCERO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identercero = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["TIPO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.saldo = Convert.ToDouble(resultado["VALOR"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt64(resultado["CENTRO_COSTO"]);

                            lstSaldosTerceros.Add(entidad);
                        }

                        return lstSaldosTerceros;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SaldosTercerosData", "ListarTercerosTraslado", ex);
                        return null;
                    }
                }
            }
        }


    }
}
