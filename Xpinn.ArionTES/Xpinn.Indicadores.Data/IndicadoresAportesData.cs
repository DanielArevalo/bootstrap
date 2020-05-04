using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Indicadores.Entities;
using System.Web;

namespace Xpinn.Indicadores.Data
{
    public class IndicadoresAportesData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public IndicadoresAportesData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<IndicadoresAportes> consultarfechaAfiliacion(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAportes> lstComponenteAdicional = new List<IndicadoresAportes>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //string sql = "select distinct(extract(YEAR from FECHA_AFILIACION)) as fecha from PERSONA_AFILIACION   group by extract(YEAR from FECHA_AFILIACION),extract(MONTH from FECHA_AFILIACION)  ORDER BY 1 desc  ";
                        string sql = "select distinct(extract(YEAR from FECHA_HISTORICO)) as fecha from HISTORICO_PERSONA   group by extract(YEAR from FECHA_HISTORICO), extract(MONTH from FECHA_HISTORICO)  ORDER BY 1 desc   ";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAportes entidad = new IndicadoresAportes();
                            if (resultado["fecha"] != DBNull.Value) entidad.año = (Convert.ToDecimal(resultado["fecha"]));
                            lstComponenteAdicional.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraOficinasData", "consultarfechaAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadoresAportes> consultarfecha(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAportes> lstComponenteAdicional = new List<IndicadoresAportes>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select fecha from cierea where tipo = 'A' and estado ='D' order by 1 desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAportes entidad = new IndicadoresAportes();
                            if (resultado["fecha"] != DBNull.Value) entidad.fecha_corte = (Convert.ToDateTime(resultado["fecha"]).ToString(conf.ObtenerFormatoFecha()));
                            lstComponenteAdicional.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraOficinasData", "consultarfecha", ex);
                        return null;
                    }
                }
            }
        }
        public List<IndicadoresAportes> consultarAportes(string fechaini, string fechafin, Usuario pUsuario,Int64 cod_linea)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAportes> lstComponenteAdicional = new List<IndicadoresAportes>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select fecha_historico, 
                                        total_aportes AS total, numero_aportes AS numero                                        
                                        From V_Ind_EvolucionAportes Where fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                                        "  and cod_linea_aporte= "+ cod_linea + "Order by fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAportes entidad = new IndicadoresAportes();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["total"] != DBNull.Value) entidad.total = Convert.ToDecimal(resultado["total"]);
                            if (resultado["numero"] != DBNull.Value) entidad.numero = Convert.ToDecimal(resultado["numero"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAportesData", "consultarAportes", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadoresAportes> consultarRetiro(string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAportes> lstComponenteAdicional = new List<IndicadoresAportes>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        //string sql = @"Select fecha_historico,    COUNT(COD_PERSONA)   AS numero "  +                                     
                        //                " From V_IND_EVOLUCIONAFI_RET " +
                        //                "  where  ESTADO='R'  and FECHA_RETIRO is not null and fecha_RETIRO between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                        //                "  and  fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                        //                 " GROUP BY fecha_historico  Order by fecha_historico";

                        //
                        //                    string sql = @"select count(cod_persona) AS numero, extract(YEAR from FECHA_RETIRO)AS YEAR, MESALETRAS(DateMonth(fecha_retiro)) as mes,extract(month from FECHA_RETIRO)AS mesnum" +
                        //                      " from PERSONA_AFILIACION Where extract(YEAR from FECHA_RETIRO)=  '" + fechafin + "' and estado = 'R' AND FECHA_RETIRO IS NOT NULL    group by extract(YEAR from FECHA_RETIRO), extract(MONTH from FECHA_RETIRO), DateMonth(FECHA_RETIRO),extract(month from FECHA_RETIRO) order by  4 asc ";

                        string sql = @" select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR,MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                      " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                      " between to_date('01/01/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/01/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " and  FECHA_retiro between to_date('01/01/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/01/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) " +
                                      " UNION ALL" +
                                      " select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR, MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                      " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                      " between to_date('01/02/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('28/02/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " and  FECHA_retiro between to_date('01/02/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('28/02/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) " +
                                      " UNION ALL" +
                                       " select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR, MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                      " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                      " between to_date('01/03/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/03/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " and  FECHA_retiro between to_date('01/03/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/03/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) " +
                                      " UNION ALL" +
                                      " select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR, MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                      " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                      " between to_date('01/04/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/04/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " and  FECHA_retiro between to_date('01/04/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/04/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) " +
                                      " UNION ALL" +
                                      " select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR, MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                     " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                     " between to_date('01/05/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/05/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " and  FECHA_retiro between to_date('01/05/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/05/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                     " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) " +
                                    " UNION ALL" +
                                    " select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR, MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                       " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                    " between to_date('01/06/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/06/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                    " and  FECHA_retiro between to_date('01/06/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/06/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                    " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) " +
                                     " UNION ALL" +
                                     " select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR, MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                     " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                     " between to_date('01/07/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/07/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " and  FECHA_retiro between to_date('01/07/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/07/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) " +
                                    " UNION ALL" +
                                    " select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR, MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                    " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                    " between to_date('01/08/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/08/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                     " and  FECHA_retiro between to_date('01/08/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/08/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                    " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) " +
                                    " UNION ALL" + 
                                    " select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR, MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                     " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                     " between to_date('01/09/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/09/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                     " and  FECHA_retiro between to_date('01/09/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/09/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                     " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) " +
                                    " UNION ALL" +
                                    " select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR, MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                     " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                     " between to_date('01/10/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/10/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                     " and  FECHA_retiro between to_date('01/10/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/10/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                     " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) " +
                                   " UNION ALL" +
                                   " select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR, MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                      " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                      " between to_date('01/11/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/11/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " and  FECHA_retiro between to_date('01/11/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/11/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) " +
                                     " UNION ALL" +
                                     " select count(cod_persona)AS numero, extract(YEAR from FECHA_retiro)AS YEAR, MESALETRAS(DateMonth(FECHA_retiro)) as mes, extract(month from FECHA_retiro)AS mesnum " +
                                     " from historico_persona Where extract(YEAR from FECHA_retiro) = '" + fechafin + "'" + " and  fecha_historico " +
                                     " between to_date('01/12/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/12/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                     " and  FECHA_retiro between to_date('01/12/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/12/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                     " group by extract(YEAR from FECHA_retiro), DateMonth(FECHA_retiro), extract(month from FECHA_retiro) ";



                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAportes entidad = new IndicadoresAportes();
                          //  if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["numero"] != DBNull.Value) entidad.numero = Convert.ToDecimal(resultado["numero"]);
                            if (resultado["YEAR"] != DBNull.Value) entidad.año = Convert.ToDecimal(resultado["YEAR"]);
                          //  if (resultado["MES"] != DBNull.Value) entidad.mes = Convert.ToDecimal(resultado["MES"]);
                            if (resultado["MES"] != DBNull.Value) entidad.mesgrafica = Convert.ToString(resultado["MES"]);


                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAportesData", "consultarRetiro", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadoresAportes> consultarAfiliacion(string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAportes> lstComponenteAdicional = new List<IndicadoresAportes>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        //string sql = @"Select fecha_historico,    COUNT(COD_PERSONA)   AS numero "  +                                     
                        //                " From V_IND_EVOLUCIONAFI_RET " +
                        //                "  where  ESTADO='R'  and FECHA_RETIRO is not null and fecha_RETIRO between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                        //                "  and  fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                        //                 " GROUP BY fecha_historico  Order by fecha_historico";


                       // string sql = @"select count(cod_persona) AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR,MESALETRAS(DateMonth(FECHA_afiliacion)) as mes,extract(month from FECHA_afiliacion)AS mesnum " +
                        //" from historico_persona Where extract(YEAR from FECHA_afiliacion)=  '" + fechafin  + "'"  + "and  fecha_historico =(SELECT MAX(FECHA_HISTORICO) FROM HISTORICO_PERSONA) group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion),extract(month from FECHA_afiliacion) order by  4 asc ";


                        string sql = @" select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR,MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                       " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin  + "'"  + " and  fecha_historico " +
                                       " between to_date('01/01/" + fechafin +"'"+ ", '" + conf.ObtenerFormatoFecha() + "')  " + ""  + " and TO_DATE ('31/01/" + fechafin  + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  "  +
                                        " and  FECHA_afiliacion between to_date('01/01/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/01/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) "+ 
                                       " UNION ALL"+
                                       " select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR, MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                       " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin + "'" + " and  fecha_historico " +
                                       " between to_date('01/02/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('28/02/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                        " and  FECHA_afiliacion between to_date('01/02/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('28/02/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) "+
                                        " UNION ALL" +
                                        " select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR, MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                       " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin + "'" + " and  fecha_historico " +
                                       " between to_date('01/03/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/03/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                        " and  FECHA_afiliacion between to_date('01/03/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/03/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) "+
                                        " UNION ALL" +
                                        " select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR, MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                       " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin + "'" + " and  fecha_historico " +
                                       " between to_date('01/04/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/04/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                        " and  FECHA_afiliacion between to_date('01/04/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/04/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) " +
                                        " UNION ALL" +
                                        " select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR, MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                      " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin + "'" + " and  fecha_historico " +
                                      " between to_date('01/05/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/05/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " and  FECHA_afiliacion between to_date('01/05/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/05/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) " +
                                      " UNION ALL" +
                                      " select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR, MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                        " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin + "'" + " and  fecha_historico " +
                                     " between to_date('01/06/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/06/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                     " and  FECHA_afiliacion between to_date('01/06/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/06/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                     " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) "+
                                       " UNION ALL" +
                                       " select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR, MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                      " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin + "'" + " and  fecha_historico " +
                                      " between to_date('01/07/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/07/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " and  FECHA_afiliacion between to_date('01/07/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/07/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) " +
                                      " UNION ALL" +
                                      " select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR, MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                     " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin + "'" + " and  fecha_historico " +
                                     " between to_date('01/08/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/08/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " and  FECHA_afiliacion between to_date('01/08/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/08/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                     " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) " +
                                     " UNION ALL" +
                                     " select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR, MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                      " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin + "'" + " and  fecha_historico " +
                                      " between to_date('01/09/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/09/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " and  FECHA_afiliacion between to_date('01/09/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/09/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) " +
                                      " UNION ALL" +
                                      " select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR, MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                      " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin + "'" + " and  fecha_historico " +
                                      " between to_date('01/10/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/10/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " and  FECHA_afiliacion between to_date('01/10/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/10/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) " +
                                      " UNION ALL" + 
                                      " select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR, MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                       " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin + "'" + " and  fecha_historico " +
                                       " between to_date('01/11/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/11/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " and  FECHA_afiliacion between to_date('01/11/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('30/11/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                       " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) " +
                                       " UNION ALL" +
                                       " select count(cod_persona)AS numero, extract(YEAR from FECHA_afiliacion)AS YEAR, MESALETRAS(DateMonth(FECHA_afiliacion)) as mes, extract(month from FECHA_afiliacion)AS mesnum " +
                                      " from historico_persona Where extract(YEAR from FECHA_afiliacion) = '" + fechafin + "'" + " and  fecha_historico " +
                                      " between to_date('01/12/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/12/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " and  FECHA_afiliacion between to_date('01/12/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " + "" + " and TO_DATE ('31/12/" + fechafin + "'" + ", '" + conf.ObtenerFormatoFecha() + "')  " +
                                      " group by extract(YEAR from FECHA_afiliacion), DateMonth(FECHA_afiliacion), extract(month from FECHA_afiliacion) ";

                        

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAportes entidad = new IndicadoresAportes();
                            //  if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["numero"] != DBNull.Value) entidad.numero = Convert.ToDecimal(resultado["numero"]);
                            if (resultado["YEAR"] != DBNull.Value) entidad.año = Convert.ToDecimal(resultado["YEAR"]);
                            if (resultado["MES"] != DBNull.Value) entidad.mesgrafica = Convert.ToString(resultado["MES"]);                                                    


                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAportesData", "consultarAfiliacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadoresAportes> consultarAportesVariacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadoresAportes> lstComponenteAdicional = new List<IndicadoresAportes>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from V_IND_VARIACIONCARTERA where fecha_historico = (select max(fecha_historico) from V_IND_VARIACIONCARTERA where fecha_historico <= to_date ('" + fechafin + "', 'dd/mm/yyyy')) order by fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadoresAportes entidad = new IndicadoresAportes();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToString(resultado["fecha_historico"]);
                            if (resultado["variacion_valor"] != DBNull.Value) entidad.variacion_valor = (Convert.ToDecimal(resultado["variacion_valor"]));
                            if (resultado["variacion_numero"] != DBNull.Value) entidad.variacion_numero = Convert.ToDecimal(resultado["variacion_numero"]);                            
                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("IndicadoresAportesData", "consultarAportesVariacion", ex);
                        return null;
                    }
                }
            }
        }
    }
}




