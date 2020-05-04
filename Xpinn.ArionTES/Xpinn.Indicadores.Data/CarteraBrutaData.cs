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
    public class CarteraBrutaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CarteraBrutaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<CarteraBruta> consultarCartera(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CarteraBruta> lstComponenteAdicional = new List<CarteraBruta>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {                    
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"Select fecha_historico, 
                                        Sum(Decode(cod_clasifica, 0, total_cartera)) AS total_cartera, Sum(Decode(cod_clasifica, 0, numero_cartera)) AS numero_cartera,
                                        Sum(Decode(cod_clasifica, 1, total_cartera)) AS total_cartera_consumo, Sum(Decode(cod_clasifica, 1, numero_cartera)) AS numero_cartera_consumo,
                                        Sum(Decode(cod_clasifica, 2, total_cartera)) AS total_cartera_comercial, Sum(Decode(cod_clasifica, 2, numero_cartera)) AS numero_cartera_Comercial,
                                        Sum(Decode(cod_clasifica, 3, total_cartera)) AS total_cartera_Vivienda, Sum(Decode(cod_clasifica, 3, numero_cartera)) AS numero_cartera_Vivienda,
                                        Sum(Decode(cod_clasifica, 4, total_cartera)) AS total_cartera_Microcredito, Sum(Decode(cod_clasifica, 4, numero_cartera)) AS numero_cartera_Microcredito 
                                        From V_Ind_EvolucionCartera Where fecha_historico between to_date ('" + fechaini + "', '" + conf.ObtenerFormatoFecha() + "') and to_date ('" + fechafin + "', '" + conf.ObtenerFormatoFecha() + "')  " +
                                        "Group by fecha_historico Order by fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CarteraBruta entidad = new CarteraBruta();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["total_cartera_consumo"] != DBNull.Value) entidad.total_cartera_consumo = (Convert.ToDecimal(resultado["total_cartera_consumo"]));
                            if (resultado["numero_cartera_consumo"] != DBNull.Value) entidad.numero_cartera_consumo = Convert.ToDecimal(resultado["numero_cartera_consumo"]);
                            if (resultado["total_cartera_comercial"] != DBNull.Value) entidad.total_cartera_comercial= Convert.ToDecimal(resultado["total_cartera_comercial"]);
                            if (resultado["numero_cartera_comercial"] != DBNull.Value) entidad.numero_cartera_comercial = Convert.ToDecimal(resultado["numero_cartera_comercial"]);
                            if (resultado["total_cartera_vivienda"] != DBNull.Value) entidad.total_cartera_vivienda = Convert.ToDecimal(resultado["total_cartera_vivienda"]);
                            if (resultado["numero_cartera_vivienda"] != DBNull.Value) entidad.numero_cartera_vivienda = Convert.ToDecimal(resultado["numero_cartera_vivienda"]);
                            if (resultado["total_cartera_microcredito"] != DBNull.Value) entidad.total_cartera_microcredito = Convert.ToDecimal(resultado["total_cartera_microcredito"]);
                            if (resultado["numero_cartera_Microcredito"] != DBNull.Value) entidad.numero_cartera_microcredito = Convert.ToDecimal(resultado["numero_cartera_Microcredito"]);
                            if (resultado["total_cartera"] != DBNull.Value) entidad.total_cartera = Convert.ToDecimal(resultado["total_cartera"]);
                            if (resultado["numero_cartera"] != DBNull.Value) entidad.numero_cartera = Convert.ToDecimal(resultado["numero_cartera"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraBrutaData", "consultarCartera", ex);
                        return null;
                    }
                }
            }
        }



        public List<CarteraBruta> consultarCarteraVariacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CarteraBruta> lstComponenteAdicional = new List<CarteraBruta>();

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
                            CarteraBruta entidad = new CarteraBruta();
                            if (resultado["variacion_valor_consumo"] != DBNull.Value) entidad.variacion_valor_consumo = (Convert.ToDecimal(resultado["variacion_valor_consumo"]));
                            if (resultado["variacion_numero_consumo"] != DBNull.Value) entidad.variacion_numero_consumo = Convert.ToDecimal(resultado["variacion_numero_consumo"]);
                            if (resultado["variacion_valor_vivienda"] != DBNull.Value) entidad.variacion_valor_vivienda = (Convert.ToDecimal(resultado["variacion_valor_vivienda"]));
                            if (resultado["variacion_numero_vivienda"] != DBNull.Value) entidad.variacion_numero_vivienda = Convert.ToDecimal(resultado["variacion_numero_vivienda"]);
                            if (resultado["variacion_valor_microcredito"] != DBNull.Value) entidad.variacion_valor_microcredito = (Convert.ToDecimal(resultado["variacion_valor_microcredito"]));
                            if (resultado["variacion_numero_microcredito"] != DBNull.Value) entidad.variacion_numero_microcredito = Convert.ToDecimal(resultado["variacion_numero_microcredito"]);
                            if (resultado["variacion_valor"] != DBNull.Value) entidad.variacion_valor = (Convert.ToDecimal(resultado["variacion_valor"]));
                            if (resultado["variacion_numero"] != DBNull.Value) entidad.variacion_numero = Convert.ToDecimal(resultado["variacion_numero"]);

                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToString(resultado["fecha_historico"]);
                
                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CarteraBrutaData", "consultarCarteraVariacion", ex);
                        return null;
                    }
                }
            }
        }
    }
}




