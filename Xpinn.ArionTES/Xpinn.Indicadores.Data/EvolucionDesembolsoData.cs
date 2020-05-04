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
    public class EvolucionDesembolsoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public EvolucionDesembolsoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<EvolucionDesembolsos> consultarDesembolso(string fechaini, string fechafin,string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EvolucionDesembolsos> lstComponenteAdicional = new List<EvolucionDesembolsos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                 try
                    {
                        string sql = @"Select fecha_historico,
                                        Sum(Decode(cod_clasifica, 0, total_desembolsos)) AS tot_desembolsos, Sum(Decode(cod_clasifica, 0, numero_desembolsos)) AS num_desembolsos,
                                        Sum(Decode(cod_clasifica, 1, total_desembolsos)) AS tot_desembolsos_consumo, Sum(Decode(cod_clasifica, 1, numero_desembolsos)) AS num_desembolsos_consumo,
                                        Sum(Decode(cod_clasifica, 2, total_desembolsos)) AS tot_desembolsos_comercial, Sum(Decode(cod_clasifica, 2, numero_desembolsos)) AS num_desembolsos_comercial,
                                        Sum(Decode(cod_clasifica, 3, total_desembolsos)) AS tot_desembolsos_Vivienda, Sum(Decode(cod_clasifica, 3, numero_desembolsos)) AS num_desembolsos_Vivienda,
                                        Sum(Decode(cod_clasifica, 4, total_desembolsos)) AS tot_desembolsos_Microcredito, Sum(Decode(cod_clasifica, 4, numero_desembolsos)) AS num_desembolsos_Microcredito 
                                        from V_IND_EVOLUCIONDESEMBOLSOS where fecha_historico between to_date ('" + fechaini + "', 'dd/mm/yyyy') and to_date ('" + fechafin + "', 'dd/mm/yyyy') " + pFiltro 
                                        + " Group by fecha_historico Order by fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                  
                        while (resultado.Read())
                        {
                            EvolucionDesembolsos entidad = new EvolucionDesembolsos();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString("dd/MM/yyyy");
                            if (resultado["tot_desembolsos_consumo"] != DBNull.Value) entidad.total_desembolsos_consumo = (Convert.ToString(resultado["tot_desembolsos_consumo"]));                            
                            if (resultado["num_desembolsos_consumo"] != DBNull.Value) entidad.numero_desembolsos_consumo = Convert.ToString(resultado["num_desembolsos_consumo"]);

                            if (resultado["tot_desembolsos_comercial"] != DBNull.Value) entidad.total_desembolsos_comercial = (Convert.ToString(resultado["tot_desembolsos_comercial"]));
                            if (resultado["num_desembolsos_comercial"] != DBNull.Value) entidad.numero_desembolsos_comercial = Convert.ToString(resultado["num_desembolsos_comercial"]);

                            if (resultado["tot_desembolsos_vivienda"] != DBNull.Value) entidad.total_desembolsos_vivienda = Convert.ToString(resultado["tot_desembolsos_vivienda"]);                            
                            if (resultado["num_desembolsos_vivienda"] != DBNull.Value) entidad.numero_desembolsos_vivienda = Convert.ToString(resultado["num_desembolsos_vivienda"]);
                            if (resultado["tot_desembolsos_microcredito"] != DBNull.Value) entidad.total_desembolsos_microcredito = Convert.ToString(resultado["tot_desembolsos_microcredito"]);                            
                            if (resultado["num_desembolsos_microcredito"] != DBNull.Value) entidad.numero_desembolsos_microcredito = Convert.ToString(resultado["num_desembolsos_microcredito"]);
                            if (resultado["tot_desembolsos"] != DBNull.Value) entidad.total_desembolsos = Convert.ToString(resultado["tot_desembolsos"]);                            
                            if (resultado["num_desembolsos"] != DBNull.Value) entidad.numero_desembolsos = Convert.ToString(resultado["num_desembolsos"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EvolucionDesembolsoData", "consultarDesembolso", ex);
                        return null;
                    }
                }
            }
        }

        public List<EvolucionDesembolsoOficinas> consultarDesembolsoOficina(string fechaini, string fechafin,string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EvolucionDesembolsoOficinas> lstComponenteAdicional = new List<EvolucionDesembolsoOficinas>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select FECHA_HISTORICO,COD_OFICINA,NOMBRE,SUM(TOTAL_DESEMBOLSOS) as total_desembolsos,SUM(NUMERO_DESEMBOLSOS) as numero_desembolsos
                                     from V_IND_EVOLUCIONDESEMBOLSOSOFI where fecha_historico between to_date ('" + fechaini + "', 'dd/mm/yyyy') and to_date ('" + fechafin + "', 'dd/mm/yyyy') "
                                     + pFiltro + " GROUP BY FECHA_HISTORICO,COD_OFICINA,NOMBRE Order by fecha_historico";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EvolucionDesembolsoOficinas entidad = new EvolucionDesembolsoOficinas();
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["total_desembolsos"] != DBNull.Value) entidad.total_desembolsos = (Convert.ToDecimal(resultado["total_desembolsos"]));
                            if (resultado["numero_desembolsos"] != DBNull.Value) entidad.numero_desembolsos = Convert.ToDecimal(resultado["numero_desembolsos"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("EvolucionDesembolsoData", "consultarDesembolsoOficina", ex);
                        return null;
                    }
                }
            }
        }



       


    }
}




