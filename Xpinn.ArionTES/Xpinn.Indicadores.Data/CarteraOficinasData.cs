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
    public class CarteraOficinasData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CarteraOficinasData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        /// <summary>
        /// Método para consultar la cartera por oficinas
        /// </summary>
        /// <param name="fechaini"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<CarteraOficinas> consultarCarteraOficinas(string fechaini, int tipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CarteraOficinas> lstComponenteAdicional = new List<CarteraOficinas>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                 try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (tipo == 1)
                            sql = "select  * from V_IND_EVOLUCIONCARTERAOFICINA where fecha_historico = to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') order by total_cartera desc";
                        if (tipo == 2)
                            sql = "select  * from V_IND_EVOLUCIONCARTERALINEA where fecha_historico = to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') order by total_cartera desc";
                        if (tipo == 3)
                            sql = "select  * from V_IND_EVOLUCIONCARTERAASESOR where fecha_historico = to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') order by total_cartera desc";
                        if (tipo == 4)
                            sql = "select  * from V_IND_EVOLUCIONCARTERAACTIV where fecha_historico = to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') order by total_cartera desc";
                        if (tipo == 5)
                            sql = "select  * from V_IND_EVOLUCIONCARTERACLAS where fecha_historico = to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') order by total_cartera desc";


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                   
                        while (resultado.Read())
                        {
                            CarteraOficinas entidad = new CarteraOficinas();

                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = (Convert.ToString(resultado["nombre"]));
                            if (resultado["total_cartera"] != DBNull.Value) entidad.total_cartera = Convert.ToDecimal(resultado["total_cartera"]);
                            if (resultado["participacion_cartera"] != DBNull.Value) entidad.participacion_cartera = Convert.ToDecimal(resultado["participacion_cartera"]);
                            if (resultado["numero_catera"] != DBNull.Value) entidad.numero_cartera = Convert.ToDecimal(resultado["numero_catera"]);
                            if (resultado["participacion_numero"] != DBNull.Value) entidad.participacion_numero = Convert.ToDecimal(resultado["participacion_numero"]);
                            if (resultado["Cod_linea_credito"] != DBNull.Value) entidad.Cod_linea_credito = Convert.ToString(resultado["Cod_linea_credito"]);
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_corte = Convert.ToString(resultado["fecha_historico"]);

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


        public List<CarteraOficinas> consultarfecha(string pTipo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CarteraOficinas> lstComponenteAdicional = new List<CarteraOficinas>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                 try
                    {
                        string sql = "select fecha from cierea where tipo = '" + pTipo + "' and estado ='D' order by 1 desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CarteraOficinas entidad = new CarteraOficinas();
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

        public List<PrestamoPromedioOficinas> consultarPrestamoPromedio(string fechaini, int cod_clasifica, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PrestamoPromedioOficinas> lstComponenteAdicional = new List<PrestamoPromedioOficinas>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select fecha_historico, cod_oficina, nombre, Sum(valor_prestamo_promedio) As valor_prestamo_promedio From V_IND_INDICADORPRETAMOOFI Where fecha_historico = to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') Group by fecha_historico, cod_oficina, nombre Order by valor_prestamo_promedio desc";
                        if (cod_clasifica != 0)
                            sql = "Select  * From V_IND_INDICADORPRETAMOOFI Where fecha_historico = to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') And cod_clasifica = " + cod_clasifica + " order by valor_prestamo_promedio desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PrestamoPromedioOficinas entidad = new PrestamoPromedioOficinas();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_corte = (Convert.ToDateTime(resultado["fecha_historico"]).ToString(conf.ObtenerFormatoFecha()));
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["cod_oficina"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["valor_prestamo_promedio"] != DBNull.Value) entidad.valor_prestamo_promedio = Convert.ToDecimal(resultado["valor_prestamo_promedio"]);
                            lstComponenteAdicional.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraOficinasData", "consultarPrestamoPromedio", ex);
                        return null;
                    }
                }
            }
        }

        public List<DistribucionCarteraOficinas> DistribucionCarteraOficinas(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DistribucionCarteraOficinas> lstIndicadorCartera = new List<DistribucionCarteraOficinas>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select v.fecha_historico, v.cod_oficina, v.nombre,
                                        Sum(Decode(v.rango_mora, '30', valor_vencido)) As valor_30dias, Sum(Decode(v.rango_mora, '30', porcentaje_vencido)) As porcentaje_30dias,
                                        Sum(Decode(v.rango_mora, '60', valor_vencido)) As valor_60dias, Sum(Decode(v.rango_mora, '60', porcentaje_vencido)) As porcentaje_60dias,
                                        Sum(Decode(v.rango_mora, '90', valor_vencido)) As valor_90dias, Sum(Decode(v.rango_mora, '90', porcentaje_vencido)) As porcentaje_90dias,
                                        Sum(Decode(v.rango_mora, '120', valor_vencido)) As valor_120dias, Sum(Decode(v.rango_mora, '120', porcentaje_vencido)) As porcentaje_120dias,
                                        Sum(Decode(v.rango_mora, '150', valor_vencido)) As valor_150dias, Sum(Decode(v.rango_mora, '150', porcentaje_vencido)) As porcentaje_150dias,
                                        Sum(Decode(v.rango_mora, '180', valor_vencido)) As valor_180dias, Sum(Decode(v.rango_mora, '180', porcentaje_vencido)) As porcentaje_180dias,
                                        Sum(Decode(v.rango_mora, '>180', valor_vencido)) As valor_360dias, Sum(Decode(v.rango_mora, '>180', porcentaje_vencido)) As porcentaje_360dias
                                        From V_IND_DISTRIBUCIONCARTERAOFI v Where v.fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "') Group by v.fecha_historico, v.cod_oficina, v.nombre Order by v.fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DistribucionCarteraOficinas entidad = new DistribucionCarteraOficinas();

                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString("MMM/yyyy");
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["valor_30dias"] != DBNull.Value) entidad.valor30 = (Convert.ToDecimal(resultado["valor_30dias"]));
                            if (resultado["porcentaje_30dias"] != DBNull.Value) entidad.porcentaje30 = (Convert.ToDecimal(resultado["porcentaje_30dias"]));
                            if (resultado["valor_60dias"] != DBNull.Value) entidad.valor60 = (Convert.ToDecimal(resultado["valor_60dias"]));
                            if (resultado["porcentaje_60dias"] != DBNull.Value) entidad.porcentaje60 = (Convert.ToDecimal(resultado["porcentaje_60dias"]));
                            if (resultado["valor_90dias"] != DBNull.Value) entidad.valor90 = (Convert.ToDecimal(resultado["valor_90dias"]));
                            if (resultado["porcentaje_90dias"] != DBNull.Value) entidad.porcentaje90 = (Convert.ToDecimal(resultado["porcentaje_90dias"]));
                            if (resultado["valor_120dias"] != DBNull.Value) entidad.valor120 = (Convert.ToDecimal(resultado["valor_120dias"]));
                            if (resultado["porcentaje_120dias"] != DBNull.Value) entidad.porcentaje120 = (Convert.ToDecimal(resultado["porcentaje_120dias"]));
                            if (resultado["valor_150dias"] != DBNull.Value) entidad.valor150 = (Convert.ToDecimal(resultado["valor_150dias"]));
                            if (resultado["porcentaje_150dias"] != DBNull.Value) entidad.porcentaje150 = (Convert.ToDecimal(resultado["porcentaje_150dias"]));
                            if (resultado["valor_180dias"] != DBNull.Value) entidad.valor180 = (Convert.ToDecimal(resultado["valor_180dias"]));
                            if (resultado["porcentaje_180dias"] != DBNull.Value) entidad.porcentaje180 = (Convert.ToDecimal(resultado["porcentaje_180dias"]));
                            if (resultado["valor_360dias"] != DBNull.Value) entidad.valorult = (Convert.ToDecimal(resultado["valor_360dias"]));
                            if (resultado["porcentaje_360dias"] != DBNull.Value) entidad.porcentajeult = (Convert.ToDecimal(resultado["porcentaje_360dias"]));
                            lstIndicadorCartera.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstIndicadorCartera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "DistribucionCarteraOficinas", ex);
                        return null;
                    }
                }
            }
        }

    }
}




