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
    public class CarteraVencidaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CarteraVencidaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<CarteraVencida> consultarCarteraVencida(string filtro, string fechaini, string fechafin, int dia, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CarteraVencida> lstComponenteAdicional = new List<CarteraVencida>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = @"Select v.fecha_historico, v.rango_mora, Sum(v.valor_vencido) As valor_vencido, Sum(v.porcentaje_vencido) As porcentaje_vencido
                                        From V_IND_EVOLUCIONCARTERAVENCIDA v Where v.fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "') and (v.rango_mora = " + dia + " or v.rango_mora is null)" + filtro + @" 
                                        Group by v.fecha_historico, v.rango_mora
                                        Order by v.rango_mora, v.fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CarteraVencida entidad = new CarteraVencida();

                            if (Convert.ToString(dia) == Convert.ToString(resultado["rango_mora"]))
                            {
                                if (resultado["valor_vencido"] != DBNull.Value) entidad.valor_vencido = (Convert.ToString(resultado["valor_vencido"]));
                                if (resultado["porcentaje_vencido"] != DBNull.Value) entidad.porcentaje_vencido = (Convert.ToDecimal(resultado["porcentaje_vencido"]));
                                if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString("MMM/yyyy");
                                entidad.descripcion = "> A " + dia + " Dias";
                            }
                            else
                            {
                                if (resultado["valor_vencido"] != DBNull.Value) entidad.valor_vencido_tot = (Convert.ToString(resultado["valor_vencido"]));
                                if (resultado["porcentaje_vencido"] != DBNull.Value) entidad.porcentaje_vencido_tot = (Convert.ToDecimal(resultado["porcentaje_vencido"]));
                                if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico_tot = Convert.ToDateTime(resultado["fecha_historico"]).ToString("MMM/yyyy");
                                entidad.descripcion_tot = "Total";
                            }


                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "consultarCarteraVencida", ex);
                        return null;
                    }
                }
            }
        }

        public List<CarteraVencida> ConsultarCarteraVencidaLinea(string filtro, string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CarteraVencida> lstComponenteAdicional = new List<CarteraVencida>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = @"SELECT * FROM V_IND_CARTERAVENCIDALINEA WHERE FECHA_HISTORICO BETWEEN to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "')  AND to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')" + filtro + " ORDER BY FECHA_HISTORICO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CarteraVencida entidad = new CarteraVencida();

                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = (Convert.ToString(resultado["COD_LINEA_CREDITO"]));
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.descripcion = (Convert.ToString(resultado["NOMBRE"]));
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico_tot = Convert.ToDateTime(resultado["fecha_historico"]).ToString("MMM/yyyy");
                            if (resultado["valor_vencido"] != DBNull.Value) entidad.valor_vencido_tot = (Convert.ToString(resultado["valor_vencido"]));
                            if (resultado["porcentaje_vencido"] != DBNull.Value) entidad.porcentaje_vencido_tot = (Convert.ToDecimal(resultado["porcentaje_vencido"]));

                            entidad.descripcion_tot = "Total";

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "consultarCarteraVencida", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadorCartera> ConsultarCarteraVencidaFechas(string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCartera> lstComponenteAdicional = new List<IndicadorCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = @"Select h.fecha_historico, Round(Sum(h.saldo_capital)/1000000) as valor_vencido,
                                        Round(Sum(h.saldo_capital)/
                                                (Select Sum(x.saldo_capital) From historico_cre x 
                                                   Where x.fecha_historico = h.fecha_historico 
                                                    And x.cod_linea_credito Not In (Select pl.cod_linea_credito From PARAMETROS_LINEA pl Where pl.cod_parametro = 320) 
                                                    And x.saldo_capital != 0)*100, 1) As porcentaje_vencido
                                        From historico_cre h 
                                        Inner Join oficina o On h.cod_oficina = o.cod_oficina 
                                        Inner Join lineascredito l On h.cod_linea_credito = l.cod_linea_credito 
                                        Inner Join categorias c On h.cod_categoria_cli = c.cod_categoria 
                                        Where h.cod_linea_credito Not In (Select pl.cod_linea_credito From PARAMETROS_LINEA pl Where pl.cod_parametro = 320) And " + filtro + @"                                         
                                        Group by h.fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCartera entidad = new IndicadorCartera();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToShortDateString();
                            if (resultado["valor_vencido"] != DBNull.Value) entidad.valor_mora = Convert.ToDecimal(resultado["valor_vencido"]);
                            if (resultado["porcentaje_vencido"] != DBNull.Value) entidad.porcentaje_90dias = Convert.ToDecimal(resultado["porcentaje_vencido"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "consultarCarteraVencida", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadorCartera> ConsultarCarteraVencidaXcategoria(string filtro, string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCartera> lstComponenteAdicional = new List<IndicadorCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = @"Select h.fecha_historico, Substr(h.cod_categoria_cli, 0, 1) As cod_categoria, c.descripcion, Round(Sum(h.saldo_capital)/1000000) as valor_vencido,
                                        Round(Sum(h.saldo_capital)/(
                                                Select Sum(x.saldo_capital) From historico_cre x 
                                                Where x.fecha_historico = h.fecha_historico 
                                                And x.cod_linea_credito Not In (Select pl.cod_linea_credito From PARAMETROS_LINEA pl Where pl.cod_parametro = 320) And x.saldo_capital != 0)*100, 2) As porcentaje_vencido
                                        From historico_cre h 
                                        Inner Join oficina o On h.cod_oficina = o.cod_oficina 
                                        Inner Join lineascredito l On h.cod_linea_credito = l.cod_linea_credito 
                                        Inner Join categorias c On h.cod_categoria_cli = c.cod_categoria 
                                        Where h.cod_linea_credito Not In (Select pl.cod_linea_credito From PARAMETROS_LINEA pl Where pl.cod_parametro = 320) " + filtro + @"        
                                        Group by h.fecha_historico, Substr(h.cod_categoria_cli, 0, 1), c.descripcion
                                        Order by h.fecha_historico, Substr(h.cod_categoria_cli, 0, 1)";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCartera entidad = new IndicadorCartera();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToShortDateString();
                            if (resultado["cod_categoria"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["cod_categoria"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.nom_categoria = Convert.ToString(resultado["descripcion"]);
                            if (resultado["valor_vencido"] != DBNull.Value) entidad.valor_mora = Convert.ToDecimal(resultado["valor_vencido"]);
                            if (resultado["porcentaje_vencido"] != DBNull.Value) entidad.porcentaje_90dias = Convert.ToDecimal(resultado["porcentaje_vencido"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "consultarCarteraVencida", ex);
                        return null;
                    }
                }
            }
        }



        public List<IndicadorCartera> consultarparticipacionpagadurias(string filtro, string Orden, string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCartera> lstComponenteAdicional = new List<IndicadorCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"Select v.fecha_historico, MESALETRAS(DateMonth(v.fecha_historico)) as mes, Sum(v.total_cartera) as total_cartera, Sum(v.total_cartera_vencida)as cartera_vencida, Sum(v.total_cartera_aldia) as cartera_aldia, Sum(v.contrucion_alvto) as contribucion, DateYear(v.fecha_historico) as anio 
                                       From V_IND_CARTERA v Where v.fecha_historico Between DateConstruct(DateYear(To_Date('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "'))-1 , DateMonth(To_Date('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')), DateDay(To_Date('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')), 0, 0, 0) and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')" + filtro + " Group by v.fecha_historico, DateMonth(v.fecha_historico) ";
                        else
                            sql = "Select v.fecha_historico, MESALETRAS(DateMonth(v.fecha_historico)) as mes, Sum(v.total_cartera) as total_cartera, Sum(v.total_cartera_vencida)as cartera_vencida, Sum(v.total_cartera_aldia) as cartera_aldia, Sum(v.contrucion_alvto)as contribucion ,DateYear(v.fecha_historico) as anio From V_IND_CARTERA v Where v.fecha_historico between '" + fechaini + "'-1 , DateMonth('" + fechafin + "'), DateDay('" + fechafin + "'), 0, 0, 0) and '" + fechafin + "' " + filtro + " Group by v.fecha_historico, DateMonth(v.fecha_historico) ";
                        sql += " Order by ";
                        if (Orden != "")
                            sql += Orden;
                        else
                            sql += "v.fecha_historico desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCartera entidad = new IndicadorCartera();
                            if (resultado["TOTAL_CARTERA"] != DBNull.Value) entidad.valor_cartera = (Convert.ToDecimal(resultado["TOTAL_CARTERA"]));
                            if (resultado["CARTERA_ALDIA"] != DBNull.Value) entidad.valor_cartera_aldia = (Convert.ToDecimal(resultado["CARTERA_ALDIA"]));
                            if (resultado["CONTRIBUCION"] != DBNull.Value) entidad.contribucion = (Convert.ToDecimal(resultado["CONTRIBUCION"]));
                            if (resultado["CARTERA_VENCIDA"] != DBNull.Value) entidad.valor_mora = (Convert.ToDecimal(resultado["CARTERA_VENCIDA"]));
                            if (resultado["MES"] != DBNull.Value) entidad.mes = (Convert.ToString(resultado["MES"]));
                            if (resultado["ANIO"] != DBNull.Value) entidad.año = (Convert.ToInt32(resultado["ANIO"]));
                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("consultarparticipacionpagadurias", "consultarparticipacionpagadurias", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadorCartera> consultarCarteraVencida(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCartera> lstIndicadorCartera = new List<IndicadorCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select v.fecha_historico,
                                        Sum(Decode(v.rango_mora, null, valor_vencido)) As valor_total, Sum(Decode(v.rango_mora, null, porcentaje_vencido)) As porcentaje_total,
                                        Sum(Decode(v.rango_mora, 30, valor_vencido)) As valor_30dias, Sum(Decode(v.rango_mora, 30, porcentaje_vencido)) As porcentaje_30dias,
                                        Sum(Decode(v.rango_mora, 60, valor_vencido)) As valor_60dias, Sum(Decode(v.rango_mora, 60, porcentaje_vencido)) As porcentaje_60dias,
                                        Sum(Decode(v.rango_mora, 90, valor_vencido)) As valor_90dias, Sum(Decode(v.rango_mora, 90, porcentaje_vencido)) As porcentaje_90dias,
                                        Sum(Decode(v.rango_mora, 120, valor_vencido)) As valor_120dias, Sum(Decode(v.rango_mora, 120, porcentaje_vencido)) As porcentaje_120dias,
                                        Sum(Decode(v.rango_mora, 180, valor_vencido)) As valor_180dias, Sum(Decode(v.rango_mora, 180, porcentaje_vencido)) As porcentaje_180dias,
                                        Sum(Decode(v.rango_mora, 360, valor_vencido)) As valor_360dias, Sum(Decode(v.rango_mora, 360, porcentaje_vencido)) As porcentaje_360dias
                                        From V_IND_EVOLUCIONCARTERAVENCIDA v Where v.fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "') Group by v.fecha_historico Order by v.fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCartera entidad = new IndicadorCartera();

                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString("MMM/yyyy");
                            if (resultado["valor_total"] != DBNull.Value) entidad.valor_total = (Convert.ToDecimal(resultado["valor_total"]));
                            if (resultado["porcentaje_total"] != DBNull.Value) entidad.porcentaje_total = (Convert.ToDecimal(resultado["porcentaje_total"]));
                            if (resultado["valor_30dias"] != DBNull.Value) entidad.valor_30dias = (Convert.ToDecimal(resultado["valor_30dias"]));
                            if (resultado["porcentaje_30dias"] != DBNull.Value) entidad.porcentaje_30dias = (Convert.ToDecimal(resultado["porcentaje_30dias"]));
                            if (resultado["valor_60dias"] != DBNull.Value) entidad.valor_60dias = (Convert.ToDecimal(resultado["valor_60dias"]));
                            if (resultado["porcentaje_60dias"] != DBNull.Value) entidad.porcentaje_60dias = (Convert.ToDecimal(resultado["porcentaje_60dias"]));
                            if (resultado["valor_90dias"] != DBNull.Value) entidad.valor_90dias = (Convert.ToDecimal(resultado["valor_90dias"]));
                            if (resultado["porcentaje_90dias"] != DBNull.Value) entidad.porcentaje_90dias = (Convert.ToDecimal(resultado["porcentaje_90dias"]));
                            if (resultado["valor_120dias"] != DBNull.Value) entidad.valor_120dias = (Convert.ToDecimal(resultado["valor_120dias"]));
                            if (resultado["porcentaje_120dias"] != DBNull.Value) entidad.porcentaje_120dias = (Convert.ToDecimal(resultado["porcentaje_120dias"]));
                            if (resultado["valor_180dias"] != DBNull.Value) entidad.valor_180dias = (Convert.ToDecimal(resultado["valor_180dias"]));
                            if (resultado["porcentaje_180dias"] != DBNull.Value) entidad.porcentaje_180dias = (Convert.ToDecimal(resultado["porcentaje_180dias"]));
                            if (resultado["valor_360dias"] != DBNull.Value) entidad.valor_360dias = (Convert.ToDecimal(resultado["valor_360dias"]));
                            if (resultado["porcentaje_360dias"] != DBNull.Value) entidad.porcentaje_360dias = (Convert.ToDecimal(resultado["porcentaje_360dias"]));
                            lstIndicadorCartera.Add(entidad);
                        }

                        return lstIndicadorCartera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "consultarCarteraVencida", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadorCarteraXClasificacion> consultarCarteraVencidaxClasificacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCarteraXClasificacion> lstIndicadorCartera = new List<IndicadorCarteraXClasificacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select v.fecha_historico,
                                        Sum(Decode(v.cod_clasifica, 1, porcentaje_vencido)) As porcentaje_consumo,
                                        Sum(Decode(v.cod_clasifica, 2, porcentaje_vencido)) As porcentaje_vivienda,
                                        Sum(Decode(v.cod_clasifica, 3, porcentaje_vencido)) As porcentaje_microcredito,
                                        Sum(Decode(v.cod_clasifica, 4, porcentaje_vencido)) As porcentaje_comercial
                                        From V_IND_INDICADORCARTERA v Where v.fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "') Group by v.fecha_historico Order by v.fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCarteraXClasificacion entidad = new IndicadorCarteraXClasificacion();

                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString("MMM/yyyy");
                            if (resultado["porcentaje_consumo"] != DBNull.Value) entidad.porcentaje_consumo = (Convert.ToDecimal(resultado["porcentaje_consumo"]));
                            if (resultado["porcentaje_vivienda"] != DBNull.Value) entidad.porcentaje_vivienda = (Convert.ToDecimal(resultado["porcentaje_vivienda"]));
                            if (resultado["porcentaje_microcredito"] != DBNull.Value) entidad.porcentaje_microcredito = (Convert.ToDecimal(resultado["porcentaje_microcredito"]));
                            if (resultado["porcentaje_comercial"] != DBNull.Value) entidad.porcentaje_comercial = (Convert.ToDecimal(resultado["porcentaje_comercial"]));
                            lstIndicadorCartera.Add(entidad);
                        }

                        return lstIndicadorCartera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "consultarCarteraVencidaxClasificacion", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadorCarteraXClasificacion> consultarCarteraVencida30xClasificacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCarteraXClasificacion> lstIndicadorCartera = new List<IndicadorCarteraXClasificacion>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select v.fecha_historico,
                                        Sum(Decode(v.cod_clasifica, 1, porcentaje_vencido)) As porcentaje_30consumo,
                                        Sum(Decode(v.cod_clasifica, 2, porcentaje_vencido)) As porcentaje_30vivienda,
                                        Sum(Decode(v.cod_clasifica, 3, porcentaje_vencido)) As porcentaje_30microcredito,
                                        Sum(Decode(v.cod_clasifica, 4, porcentaje_vencido)) As porcentaje_30comercial
                                        From V_IND_INDICADORCARTERA30 v Where v.fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "') Group by v.fecha_historico Order by v.fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCarteraXClasificacion entidad = new IndicadorCarteraXClasificacion();

                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString("MMM/yyyy");
                            if (resultado["porcentaje_30consumo"] != DBNull.Value) entidad.porcentaje_30consumo = (Convert.ToDecimal(resultado["porcentaje_30consumo"]));
                            if (resultado["porcentaje_30vivienda"] != DBNull.Value) entidad.porcentaje_30vivienda = (Convert.ToDecimal(resultado["porcentaje_30vivienda"]));
                            if (resultado["porcentaje_30microcredito"] != DBNull.Value) entidad.porcentaje_30microcredito = (Convert.ToDecimal(resultado["porcentaje_30microcredito"]));
                            if (resultado["porcentaje_30comercial"] != DBNull.Value) entidad.porcentaje_30comercial = (Convert.ToDecimal(resultado["porcentaje_30comercial"]));
                            lstIndicadorCartera.Add(entidad);
                        }

                        return lstIndicadorCartera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "consultarCarteraVencidaxClasificacion", ex);
                        return null;
                    }
                }
            }
        }

        ///////////////////

        public List<IndicadorCarteraOficinas> consultarCarteraVencidaOficinas(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCarteraOficinas> lstIndicadorCartera = new List<IndicadorCarteraOficinas>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select v.*
                                        From V_IND_INDICADORCARTERAOFI v Where v.fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "') Order by v.fecha_historico, v.porcentaje_vencido desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCarteraOficinas entidad = new IndicadorCarteraOficinas();

                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString("MMM/yyyy");
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["valor_vencido"] != DBNull.Value) entidad.valor_total = (Convert.ToDecimal(resultado["valor_vencido"]));
                            if (resultado["porcentaje_vencido"] != DBNull.Value) entidad.porcentaje_total = (Convert.ToDecimal(resultado["porcentaje_vencido"]));
                            lstIndicadorCartera.Add(entidad);
                        }

                        return lstIndicadorCartera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "consultarCarteraVencidaOficinas", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadorCarteraOficinas> consultarCarteraVencida30Oficinas(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCarteraOficinas> lstIndicadorCartera = new List<IndicadorCarteraOficinas>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select v.*
                                        From V_IND_INDICADORCARTERAOFI30 v Where v.fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "') Order by v.fecha_historico, v.porcentaje_vencido desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCarteraOficinas entidad = new IndicadorCarteraOficinas();

                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString("MMM/yyyy");
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["valor_vencido"] != DBNull.Value) entidad.valor_30dias = (Convert.ToDecimal(resultado["valor_vencido"]));
                            if (resultado["porcentaje_vencido"] != DBNull.Value) entidad.porcentaje_30dias = (Convert.ToDecimal(resultado["porcentaje_vencido"]));
                            lstIndicadorCartera.Add(entidad);
                        }

                        return lstIndicadorCartera;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "consultarCarteraVencida30Oficinas", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadorCartera> colocacionoficina(string ofi, string fechaini, string fechafin, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCartera> lstComponenteAdicional = new List<IndicadorCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        Configuracion conf = new Configuracion();
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql = @"select l.nombre, count(*) as numero, sum(monto) as valor From historico_cre h Inner Join LineasCredito l On h.cod_linea_credito = l.cod_linea_Credito Where l.aplica_asociado = 1 And h.fecha_historico = To_Date('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "') ";
                            if (ofi != "")
                                sql += "and h.cod_oficina in(" + ofi + ") ";
                            sql += "and fecha_desembolso between To_Date('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and To_Date('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "') " + pFiltro + "group by l.NOMBRE";
                        }
                        else
                        {
                            sql = @"select l.nombre, count(*) as numero, sum(monto) as valor From historico_cre h Inner Join LineasCredito l On h.cod_linea_credito = l.cod_linea_Credito Where l.aplica_asociado = 1 And h.fecha_historico = '" + fechafin + "' ";
                            if (ofi != "")
                                sql += "and h.cod_oficina in(" + ofi + ") ";
                            sql += "and fecha_desembolso between '" + fechaini + "' and '" + fechafin + "'" + pFiltro + " group by l.NOMBRE";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCartera entidad = new IndicadorCartera();
                            if (resultado["numero"] != DBNull.Value) entidad.numero = Convert.ToInt32(resultado["numero"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = (Convert.ToString(resultado["nombre"]));


                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EvolucionDesembolsoData", "colocacionoficina", ex);
                        return null;
                    }
                }
            }
        }


        public List<IndicadorCartera> ListarCarteraCategoria(string pFiltro, string pOrden, DateTime pFechaCorte, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCartera> lstComponenteAdicional = new List<IndicadorCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        Configuracion conf = new Configuracion();
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        {
                            sql = @"Select h.fecha_historico, h.cod_oficina, o.nombre, h.cod_categoria, c.descripcion, Sum(h.saldo_capital) as valor 
                                    From historico_cre h 
                                    Inner Join oficina o On h.cod_oficina = o.cod_oficina 
                                    Inner Join lineascredito l On h.cod_linea_credito = l.cod_linea_credito 
                                    Inner Join categorias c On h.cod_categoria = c.cod_categoria 
                                    Where h.fecha_historico = TO_DATE('" + pFechaCorte.ToShortDateString() + "','dd/MM/yyyy') " + pFiltro
                                    + "Group by h.fecha_historico, h.cod_oficina, o.nombre, h.cod_categoria, c.descripcion ";
                        }
                        else
                        {

                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCartera entidad = new IndicadorCartera();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToString(resultado["fecha_historico"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["cod_categoria"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["cod_categoria"]);
                            if (resultado["descripcion"] != DBNull.Value) entidad.nom_categoria = (Convert.ToString(resultado["descripcion"]));
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EvolucionDesembolsoData", "colocacionoficina", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Datos para indicador de clasificación de cartera por categorias
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="Orden"></param>
        /// <param name="fechaini"></param>
        /// <param name="fechafin"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<IndicadorCartera> ConsultarCarteraCategorias(DateTime pFecha, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCartera> lstIndicador = new List<IndicadorCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {

                    try
                    {
                        Configuracion conf = new Configuracion();

                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"Select h.fecha_historico, h.cod_categoria_cli, c.descripcion, h.cod_oficina, Sum(h.saldo_capital) As saldo_capital
                                        From historico_cre h 
                                        Inner Join oficina o On h.cod_oficina = o.cod_oficina
                                        Inner Join lineascredito l On h.cod_linea_credito = l.cod_linea_credito
                                        Inner Join categorias c On h.cod_categoria_cli = c.cod_categoria
                                        Where h.fecha_historico = To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') " + pFiltro + @"
                                        Group by h.fecha_historico, h.cod_oficina, h.cod_categoria_cli, c.descripcion";
                        else
                            sql = @"Select h.fecha_historico, h.cod_categoria_cli, c.descripcion, h.cod_oficina, Sum(h.saldo_capital) As saldo_capital
                                        From historico_cre h 
                                        Inner Join oficina o On h.cod_oficina = o.cod_oficina
                                        Inner Join lineascredito l On h.cod_linea_credito = l.cod_linea_credito
                                        Inner Join categorias c On h.cod_categoria_cli = c.cod_categoria
                                        Where h.fecha_historico = '" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "' " + pFiltro + @"
                                        Group by h.fecha_historico, h.cod_oficina, h.cod_categoria_cli, c.descripcion";
                        sql += " Order by h.fecha_historico, h.cod_categoria_cli, h.cod_oficina";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCartera entidad = new IndicadorCartera();
                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.fecha_historico = (Convert.ToString(resultado["FECHA_HISTORICO"]));
                            if (resultado["COD_CATEGORIA_CLI"] != DBNull.Value) entidad.cod_categoria = (Convert.ToString(resultado["COD_CATEGORIA_CLI"]));
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.nom_categoria = (Convert.ToString(resultado["DESCRIPCION"]));
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = (Convert.ToInt64(resultado["COD_OFICINA"]));
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.valor_cartera = (Convert.ToDecimal(resultado["SALDO_CAPITAL"]));
                            lstIndicador.Add(entidad);
                        }

                        return lstIndicador;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "ConsultarCarteraCategorias", ex);
                        return null;
                    }
                }
            }
        }


        public List<IndicadorCartera> ColocacionCarteraXLinea(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCartera> lstComponenteAdicional = new List<IndicadorCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        Configuracion conf = new Configuracion();
                        sql = @"SELECT L.NOMBRE, COUNT(*) AS NUMERO, SUM(MONTO) AS VALOR 
                                FROM HISTORICO_CRE H INNER JOIN LINEASCREDITO L 
                                ON H.COD_LINEA_CREDITO = L.COD_LINEA_CREDITO " + pFiltro;
                        sql += " GROUP BY L.NOMBRE ORDER BY 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCartera entidad = new IndicadorCartera();
                            if (resultado["numero"] != DBNull.Value) entidad.numero = Convert.ToInt32(resultado["numero"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = (Convert.ToString(resultado["NOMBRE"]));

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "ColocacionCarteraXLinea", ex);
                        return null;
                    }
                }
            }
        }

        public List<IndicadorCartera> IngresosDatafono(DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCartera> lstComponenteAdicional = new List<IndicadorCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        sql = @"Select o.cod_ofi || '-' || o.cod_usu, Case o.cod_usu When 24 Then 'P.A.CENTRO HISTORICO' Else Upper(f.nombre) End as nombre, Sum(m.valor) As valor, Count(*) As numero
                                    from movimientocaja m Inner Join operacion o On m.cod_ope = o.cod_ope
                                    Inner Join oficina f On o.cod_ofi = f.cod_oficina
                                    Inner Join usuarios u On o.cod_usu = u.codusuario
                                    Where m.cod_tipo_pago = 10 And m.fec_ope Between ";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += "To_Date('" + pFechaIni.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And To_Date('" + pFechaFin.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql += " '" + pFechaIni.ToString(conf.ObtenerFormatoFecha()) + " And '" + pFechaFin.ToString(conf.ObtenerFormatoFecha()) + "' ";
                        sql += " Group by o.cod_ofi || '-' || o.cod_usu, Case o.cod_usu When 24 Then 'P.A.CENTRO HISTORICO' Else Upper(f.nombre) End Order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCartera entidad = new IndicadorCartera();
                            if (resultado["numero"] != DBNull.Value) entidad.numero = Convert.ToInt32(resultado["numero"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = (Convert.ToString(resultado["nombre"]));

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "IngresosDatafono", ex);
                        return null;
                    }
                }
            }
        }


        public List<IndicadorCartera> IngresosPagadurias(DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<IndicadorCartera> lstComponenteAdicional = new List<IndicadorCartera>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        sql = @"Select r.cod_empresa, e.nom_empresa As nombre, Sum(d.valor) As Valor, Count(r.numero_recaudo) As numero
                                    From recaudo_masivo r Inner Join empresa_recaudo e On r.cod_empresa = e.cod_empresa
                                    Inner Join detrecaudo_masivo d On r.numero_recaudo = d.numero_recaudo
                                    Left Join operacion o On r.numero_recaudo = o.num_lista
                                    Where (o.estado = 1 Or o.estado Is Null) And r.cod_empresa Not In (3, 5, 6, 7, 8) And ";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql += " Trunc(r.periodo_corte) Between To_Date('" + pFechaIni.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And To_Date('" + pFechaFin.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') ";
                        else
                            sql += " r.periodo_corte Between '" + pFechaIni.ToString(conf.ObtenerFormatoFecha()) + " And '" + pFechaFin.ToString(conf.ObtenerFormatoFecha()) + "' ";
                        sql += " Group by r.cod_empresa, e.nom_empresa Order by 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            IndicadorCartera entidad = new IndicadorCartera();
                            if (resultado["numero"] != DBNull.Value) entidad.numero = Convert.ToInt32(resultado["numero"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = (Convert.ToString(resultado["nombre"]));

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraVencidaData", "IngresosDatafono", ex);
                        return null;
                    }
                }
            }
        }

        public List<CarteraVencida> ListarLineasCredito(Usuario pUsuario)
        {
            DbDataReader resultado;
            List<CarteraVencida> listLineas = new List<CarteraVencida>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM LINEASCREDITO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CarteraVencida entidad = new CarteraVencida();
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            listLineas.Add(entidad);
                        }
                        return listLineas;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LineasCreditoData", "ListarLineasCredito", ex);
                        return null;
                    }
                    finally
                    {
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                }
            }
        }
    }
}




