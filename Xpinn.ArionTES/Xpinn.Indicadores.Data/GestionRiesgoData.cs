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
    public class GestionRiesgoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public GestionRiesgoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<GestionRiesgo> ListadoSegmentoCredito(string fechaCorte, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<GestionRiesgo> lstComponenteAdicional = new List<GestionRiesgo>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                 try
                    {
                        string sql = @"Select hs.fecha_historico, hs.calificacion, hs.segmento, hs.cod_categoria, Count(*) As numero, Sum(hc.saldo_capital) As saldo
                                        From historico_segmenta_cre hs Left Join historico_cre hc On hs.fecha_historico = hc.fecha_historico And hs.numero_radicacion = hc.numero_radicacion
                                        Where hs.fecha_historico = to_date ('" + fechaCorte + "', 'dd/mm/yyyy') " + pFiltro + @"
                                        Group by hs.fecha_historico, hs.calificacion, hs.segmento, hs.cod_categoria Order by hs.fecha_historico, hs.calificacion, hs.segmento, hs.cod_categoria";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                  
                        while (resultado.Read())
                        {
                            GestionRiesgo entidad = new GestionRiesgo();
                            if (resultado["fecha_historico"] != DBNull.Value) entidad.fecha_historico = Convert.ToDateTime(resultado["fecha_historico"]).ToString("dd/MM/yyyy");
                            if (resultado["segmento"] != DBNull.Value) entidad.segmento = (Convert.ToString(resultado["segmento"]));
                            if (resultado["cod_categoria"] != DBNull.Value) entidad.cod_categoria = (Convert.ToString(resultado["cod_categoria"]));
                            if (resultado["saldo"] != DBNull.Value) entidad.saldo = (Convert.ToDecimal(resultado["saldo"]));                            
                            if (resultado["numero"] != DBNull.Value) entidad.numero = Convert.ToDecimal(resultado["numero"]);

                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GestionRiesgoData", "ListadoSegmentoCredito", ex);
                        return null;
                    }
                }
            }
        }
       


    }
}




