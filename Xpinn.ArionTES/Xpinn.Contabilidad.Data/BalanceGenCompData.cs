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
    public class BalanceGenCompData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para PlanCuentas
        /// </summary>
        public BalanceGenCompData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        /// <summary>
        /// Método para consultar el libro SaldosTerceros Consolidado
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<BalanceGenComp>ListarBalanceComparativo(BalanceGenComp pEntidad, Usuario vUsuario, int pOpcion)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceGenComp> lstBalancecomparativo= new List<BalanceGenComp>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
              {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                       
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pfechaprimerper= cmdTransaccionFactory.CreateParameter();
                        pfechaprimerper.ParameterName = "pfecha1";
                        pfechaprimerper.Value = pEntidad.fechaprimerper;
                        pfechaprimerper.Direction = ParameterDirection.Input;
                        pfechaprimerper.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechaprimerper);

                        DbParameter pfechasegunper = cmdTransaccionFactory.CreateParameter();
                        pfechasegunper.ParameterName = "pfecha2";
                        pfechasegunper.Value = pEntidad.fechasegunper;
                        pfechasegunper.Direction = ParameterDirection.Input;
                        pfechasegunper.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechasegunper);

                        DbParameter pfechaterper = cmdTransaccionFactory.CreateParameter();
                        pfechaterper.ParameterName = "pfecha3";
                        if (pEntidad.fechatercerper == null)
                            pfechaterper.Value = DBNull.Value;
                        else
                            pfechaterper.Value = pEntidad.fechatercerper;
                        pfechaterper.Direction = ParameterDirection.Input;
                        pfechaterper.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(pfechaterper);

                        DbParameter pcentroini = cmdTransaccionFactory.CreateParameter();
                        pcentroini.ParameterName = "pCenIni";
                        pcentroini.Value = pEntidad.centro_costoini;
                        pcentroini.Direction = ParameterDirection.Input;
                        pcentroini.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcentroini);

                        DbParameter pcentrofin = cmdTransaccionFactory.CreateParameter();
                        pcentrofin.ParameterName = "pCenFin";
                        pcentrofin.Value = pEntidad.centro_costofin;
                        pcentrofin.Direction = ParameterDirection.Input;
                        pcentrofin.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcentrofin);


                        DbParameter pNivel = cmdTransaccionFactory.CreateParameter();
                        pNivel.ParameterName = "pNivel";
                        pNivel.Value = pEntidad.nivel;
                        pNivel.Direction = ParameterDirection.Input;
                        pNivel.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pNivel);

                        DbParameter PMONEDA = cmdTransaccionFactory.CreateParameter();
                        PMONEDA.ParameterName = "PMONEDA";
                        PMONEDA.Value = pEntidad.cod_moneda;
                        PMONEDA.Direction = ParameterDirection.Input;
                        PMONEDA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PMONEDA);
                    
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pOpcion == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BALCOMP";
                        else // Niif PENDIENTE DE AJUSTAR PL
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CON_BALCOMPNIIF";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceGenCompData", "USP_XPINN_CON_BALCOMP", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = @"Select fecha1, fecha2, fecha3, centro_costo_ini, centro_costo_fin, cod_cuenta, nombre_cuenta, nivel, sum(valor1) as valor1, sum(participacion1) as participacion1, sum(valor2) as valor2, participacion2, sum(diferencia1) as diferencia1, porcentaje1/100 As porcentaje1, sum(valor3) as valor3, participacion3, diferencia2, porcentaje2/100 As porcentaje2 from temp_balcomp 
                                        group by fecha1, fecha2, fecha3, cod_cuenta, nombre_cuenta, centro_costo_ini, centro_costo_fin, nivel, participacion2, participacion3, porcentaje1, diferencia2, porcentaje2 order by cod_cuenta";                       
                        
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceGenComp entidad = new BalanceGenComp();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["NOMBRE_CUENTA"] != DBNull.Value) entidad.nombrecuenta = Convert.ToString(resultado["NOMBRE_CUENTA"]);
                            if (resultado["FECHA1"] != DBNull.Value) entidad.fechaprimerper = Convert.ToDateTime(resultado["FECHA1"]);
                            if (resultado["FECHA2"] != DBNull.Value) entidad.fechasegunper = Convert.ToDateTime(resultado["FECHA2"]);
                            if (resultado["FECHA3"] != DBNull.Value) entidad.fechatercerper = Convert.ToDateTime(resultado["FECHA3"]);
                            if (resultado["CENTRO_COSTO_INI"] != DBNull.Value) entidad.centro_costoini = Convert.ToInt64(resultado["CENTRO_COSTO_INI"]);
                            if (resultado["CENTRO_COSTO_FIN"] != DBNull.Value) entidad.centro_costofin = Convert.ToInt64(resultado["CENTRO_COSTO_FIN"]);
                            if (resultado["NIVEL"] != DBNull.Value) entidad.nivel = Convert.ToInt64(resultado["NIVEL"]);
                            if (resultado["VALOR1"] != DBNull.Value) entidad.balance1 = Convert.ToDecimal(resultado["VALOR1"]);
                            if (resultado["PARTICIPACION1"] != DBNull.Value) entidad.porcpart1 = Convert.ToDecimal(resultado["PARTICIPACION1"]);
                            if (resultado["VALOR2"] != DBNull.Value) entidad.balance2 = Convert.ToDecimal(resultado["VALOR2"]);
                            if (resultado["PARTICIPACION2"] != DBNull.Value) entidad.porcpart2 = Convert.ToDecimal(resultado["PARTICIPACION2"]);
                            if (resultado["DIFERENCIA1"] != DBNull.Value) entidad.diferencia = Convert.ToDecimal(resultado["DIFERENCIA1"]);
                            if (resultado["PORCENTAJE1"] != DBNull.Value) entidad.porcdif = Convert.ToDecimal(resultado["PORCENTAJE1"]);
                            if (resultado["VALOR3"] != DBNull.Value) entidad.balance3 = Convert.ToDecimal(resultado["VALOR3"]);
                            if (resultado["PARTICIPACION3"] != DBNull.Value) entidad.porcpart3 = Convert.ToDecimal(resultado["PARTICIPACION3"]);
                            if (resultado["DIFERENCIA2"] != DBNull.Value) entidad.diferencia2 = Convert.ToDecimal(resultado["DIFERENCIA2"]);
                            if (resultado["PORCENTAJE2"] != DBNull.Value) entidad.porcdif2 = Convert.ToDecimal(resultado["PORCENTAJE2"]);
                            lstBalancecomparativo.Add(entidad);
                        }

                        return lstBalancecomparativo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceGenCompData", "ListarBalanceComparativo", ex);
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
        public List<BalanceGenComp> ListarFecha(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<BalanceGenComp> lstFecha1 = new List<BalanceGenComp>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "Select Distinct fecha from cierea where tipo='C' and estado='D' order by fecha desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            BalanceGenComp entidad = new BalanceGenComp();

                            if (resultado["FECHA"] != DBNull.Value) entidad.fechaprimerper = Convert.ToDateTime(resultado["FECHA"].ToString());
                            lstFecha1.Add(entidad);
                        }

                        return lstFecha1;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalanceGeneralData", "ListarFecha1", ex);
                        return null;
                    }
                }
            }
        }



    }
}
