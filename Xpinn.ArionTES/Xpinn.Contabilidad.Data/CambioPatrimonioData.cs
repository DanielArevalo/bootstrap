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
    public class CambioPatrimonioData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CambioPatrimonioData() 
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<CambioPatrimonio> getListaCombo(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CambioPatrimonio> listaEntiti = new List<CambioPatrimonio>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        String sql = "select * from centro_costo";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CambioPatrimonio entiti = new CambioPatrimonio();
                            if (resultado["NOM_CENTRO"] != DBNull.Value) entiti.Descripcion_Moviminto = Convert.ToString(resultado["NOM_CENTRO"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entiti.totales = Convert.ToInt64(resultado["CENTRO_COSTO"]);
                            listaEntiti.Add(entiti);
                        }
                        return listaEntiti;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CambioPatrimonioData", "getListaCombo", ex);
                        return null;
                    }
                }
            }
        }

        public List<CambioPatrimonio> getListaCambioPatrimonio(Usuario pUsuario,int pOpcion)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CambioPatrimonio> ListaEntiti = new List<CambioPatrimonio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                    }
                    catch
                    {
                        BOExcepcion.Throw("CambioPatrimonioData", "getListaCambioPatrimonio", new Exception());
                    }
                }
            }
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select * from TEMP_CAMBPATRIMONIO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado= cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            CambioPatrimonio entiti = new CambioPatrimonio();
                            if (resultado[""] != DBNull.Value) entiti.Descripcion_Moviminto = Convert.ToString(resultado[""]);
                            if (resultado[""] != DBNull.Value) entiti.aporte_Sociales = Convert.ToDecimal(resultado[""]);
                            if (resultado[""] != DBNull.Value) entiti.reservas = Convert.ToDecimal(resultado[""]);
                            if (resultado[""] != DBNull.Value) entiti.fondos_Destinacion_especificas = Convert.ToDecimal(resultado[""]);
                            if (resultado[""] != DBNull.Value) entiti.valorizacion = Convert.ToDecimal(resultado[""]);
                            if (resultado[""] != DBNull.Value) entiti.excedentes_netos = Convert.ToDecimal(resultado[""]);
                            if (resultado[""] != DBNull.Value) entiti.totales = Convert.ToDecimal(resultado[""]);
                            ListaEntiti.Add(entiti);
                        }
                        return ListaEntiti;
                    }
                    catch
                    {
                        BOExcepcion.Throw("CambioPatrimonioData", "getListaCambioPatrimonio", new Exception());
                        return null;
                    }
                }
            }
        }
    }
}
