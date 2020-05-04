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
    public class GestionDiariaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public GestionDiariaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<GestionDiaria> ReporteGestionDiaria(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<GestionDiaria> lstComponenteAdicional = new List<GestionDiaria>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM V_REPORT_GESTION_DIARIA ORDER BY OFICINA, NOMBRE desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            GestionDiaria entidad = new GestionDiaria();
                            if (resultado["FECHA_HISTORICO"] != DBNull.Value) entidad.FECHA_HISTORICO = Convert.ToDateTime(resultado["FECHA_HISTORICO"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.OFICINA = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.COD_ASESOR_COM = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.NOMBRE = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NUM_CRED_CIERRE"] != DBNull.Value) entidad.NUM_CRED_CIERRE = Convert.ToDecimal(resultado["NUM_CRED_CIERRE"]);
                            if (resultado["SALDO_CAPITAL_CIERRE"] != DBNull.Value) entidad.SALDO_CAPITAL_CIERRE = Convert.ToDecimal(resultado["SALDO_CAPITAL_CIERRE"]);
                            if (resultado["NUM_CRED_ACTUAL"] != DBNull.Value) entidad.NUM_CRED_ACTUAL = Convert.ToDecimal(resultado["NUM_CRED_ACTUAL"]);
                            if (resultado["SALDO_CAPITAL_ACTUAL"] != DBNull.Value) entidad.SALDO_CAPITAL_ACTUAL = Convert.ToDecimal(resultado["SALDO_CAPITAL_ACTUAL"]);
                            if (resultado["NO_COLOCACION_CIERRE"] != DBNull.Value) entidad.NO_COLOCACION_CIERRE = Convert.ToDecimal(resultado["NO_COLOCACION_CIERRE"]);
                            if (resultado["MONTO_COLOCACION_CIERRE"] != DBNull.Value) entidad.MONTO_COLOCACION_CIERRE = Convert.ToDecimal(resultado["MONTO_COLOCACION_CIERRE"]);
                            if (resultado["NO_COLOCACION_ACTUAL"] != DBNull.Value) entidad.NO_COLOCACION_ACTUAL = Convert.ToDecimal(resultado["NO_COLOCACION_ACTUAL"]);
                            if (resultado["MONTO_COLOCACION_ACTUAL"] != DBNull.Value) entidad.MONTO_COLOCACION_ACTUAL = Convert.ToDecimal(resultado["MONTO_COLOCACION_ACTUAL"]);
                            if (resultado["META_COLOCACIONES"] != DBNull.Value) entidad.META_COLOCACIONES = Convert.ToDecimal(resultado["META_COLOCACIONES"]);
                            if (resultado["CUMPLIMIENTO_COLOCACIONES"] != DBNull.Value) entidad.CUMPLIMIENTO_COLOCACIONES = Convert.ToDecimal(resultado["CUMPLIMIENTO_COLOCACIONES"]);
                            if (resultado["NUM_CREDMORA_CIERRE"] != DBNull.Value) entidad.NUM_CREDMORA_CIERRE = Convert.ToDecimal(resultado["NUM_CREDMORA_CIERRE"]);
                            if (resultado["SALDO_MORA_CIERRE"] != DBNull.Value) entidad.SALDO_MORA_CIERRE = Convert.ToDecimal(resultado["SALDO_MORA_CIERRE"]);
                            if (resultado["NUM_CREDMORA_ACTUAL"] != DBNull.Value) entidad.NUM_CREDMORA_ACTUAL = Convert.ToDecimal(resultado["NUM_CREDMORA_ACTUAL"]);
                            if (resultado["SALDO_MORA_ACTUAL"] != DBNull.Value) entidad.SALDO_MORA_ACTUAL = Convert.ToDecimal(resultado["SALDO_MORA_ACTUAL"]);
                            if (resultado["SALDO_GCOMUNITARIA_CIERRE"] != DBNull.Value) entidad.SALDO_GCOMUNITARIA_CIERRE = Convert.ToDecimal(resultado["SALDO_GCOMUNITARIA_CIERRE"]);
                            if (resultado["SALDO_GCOMUNITARIA_ACTUAL"] != DBNull.Value) entidad.SALDO_GCOMUNITARIA_ACTUAL = Convert.ToDecimal(resultado["SALDO_GCOMUNITARIA_ACTUAL"]);
                            if (resultado["NUM_CREDMORAMAYOR30_ACTUAL"] != DBNull.Value) entidad.NUM_CREDMORAMAYOR30_ACTUAL = Convert.ToDecimal(resultado["NUM_CREDMORAMAYOR30_ACTUAL"]);
                            if (resultado["SALDO_MORAMAYOR30_ACTUAL"] != DBNull.Value) entidad.SALDO_MORAMAYOR30_ACTUAL = Convert.ToDecimal(resultado["SALDO_MORAMAYOR30_ACTUAL"]);
                            if (resultado["META_MORAMENOR30"] != DBNull.Value) entidad.META_MORAMENOR30 = Convert.ToDecimal(resultado["META_MORAMENOR30"]);
                            if (resultado["CUMPLIMIENTO_MORAMENOR30"] != DBNull.Value) entidad.CUMPLIMIENTO_MORAMENOR30 = Convert.ToDecimal(resultado["CUMPLIMIENTO_MORAMENOR30"]);
                            if (resultado["NUM_CREDMORAMAYOR60_ACTUAL"] != DBNull.Value) entidad.NUM_CREDMORAMAYOR60_ACTUAL = Convert.ToDecimal(resultado["NUM_CREDMORAMAYOR60_ACTUAL"]);
                            if (resultado["SALDO_MORAMAYOR60_ACTUAL"] != DBNull.Value) entidad.SALDO_MORAMAYOR60_ACTUAL = Convert.ToDecimal(resultado["SALDO_MORAMAYOR60_ACTUAL"]);
                            if (resultado["NUM_CREDMORAMENOR30_ACTUAL"] != DBNull.Value) entidad.NUM_CREDMORAMENOR30_ACTUAL = Convert.ToDecimal(resultado["NUM_CREDMORAMENOR30_ACTUAL"]);
                            if (resultado["SALDO_MORAMENOR30_ACTUAL"] != DBNull.Value) entidad.SALDO_MORAMENOR30_ACTUAL = Convert.ToDecimal(resultado["SALDO_MORAMENOR30_ACTUAL"]);
                            if (resultado["META_MORAMAYOR30"] != DBNull.Value) entidad.META_MORAMAYOR30 = Convert.ToDecimal(resultado["META_MORAMAYOR30"]);
                            if (resultado["CUMPLIMIENTO_MORAMAYOR30"] != DBNull.Value) entidad.CUMPLIMIENTO_MORAMAYOR30 = Convert.ToDecimal(resultado["CUMPLIMIENTO_MORAMAYOR30"]);
                            
                            lstComponenteAdicional.Add(entidad);
                        }

                        return lstComponenteAdicional;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("GestionDiariaData", "ReporteGestionDiaria", ex);
                        return null;
                    }
                }
            }
        }
    }
}




