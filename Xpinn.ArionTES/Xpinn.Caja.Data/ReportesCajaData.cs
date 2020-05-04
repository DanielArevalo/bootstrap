using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Caja.Entities;
using System.Globalization;

namespace Xpinn.Caja.Data
{
    public class ReportesCajaData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ReportesCajaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public List<ReportesCaja> ListarReportemovdiario(Usuario pUsuario, Int64 codigo, DateTime fechaini, DateTime fechafinal)
        {
          
            DbDataReader resultado = default(DbDataReader);
            List<ReportesCaja> lstPrograma = new List<ReportesCaja>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                            string sql = "Select cod_oficina, nombre_oficina, cod_cajero, nombre_cajero, fecha_movimiento, Sum(valor_pago) As valor_pago, Sum(cantidad_pagos) As cantidad_pagos, Sum(efectivo) as efectivo, Sum(cheque) As cheque, Sum(egresos_efectivo) as egresos_efectivo," +
                                         " SALDO_AR_CHEQ,SALDO_AR_EFE , CONSIGNACIONES from VCajaTransaccionesCon  Where FECHA_MOVIMIENTO between to_date('" + fechaini.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') And to_date('" + fechaini.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') " +
                                         " Group by cod_oficina, nombre_oficina, cod_cajero, nombre_cajero, fecha_movimiento,SALDO_AR_CHEQ,SALDO_AR_EFE,CONSIGNACIONES order by cod_oficina";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ReportesCaja entidad = new ReportesCaja();

                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.COD_OFICINA = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.NOMBRE_OFICINA = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["COD_CAJERO"] != DBNull.Value) entidad.COD_CAJERO = Convert.ToInt64(resultado["COD_CAJERO"]);
                            if (resultado["NOMBRE_CAJERO"] != DBNull.Value) entidad.NOMBRE_CAJERO = Convert.ToString(resultado["NOMBRE_CAJERO"]);
                            if (resultado["FECHA_MOVIMIENTO"] != DBNull.Value) entidad.FECHA_MOVIMIENTO = Convert.ToDateTime(resultado["FECHA_MOVIMIENTO"]).ToString("dd/MM/yyyy");
                            if (resultado["VALOR_PAGO"] != DBNull.Value) entidad.VALOR_PAGO = Convert.ToInt64(resultado["VALOR_PAGO"]);
                            if (resultado["CANTIDAD_PAGOS"] != DBNull.Value) entidad.CANTIDAD_PAGOS = Convert.ToInt64(resultado["CANTIDAD_PAGOS"]);
                            if (resultado["EFECTIVO"] != DBNull.Value) entidad.EFECTIVO = Convert.ToInt64(resultado["EFECTIVO"]);
                            if (resultado["CHEQUE"] != DBNull.Value) entidad.CHEQUE = Convert.ToInt64(resultado["CHEQUE"]);
                            if (resultado["EGRESOS_EFECTIVO"] != DBNull.Value) entidad.EGRESOS_EFECTIVO = Convert.ToInt64(resultado["EGRESOS_EFECTIVO"]);
                            if (resultado["SALDO_AR_CHEQ"] != DBNull.Value) entidad.saldocheque = Convert.ToInt64(resultado["SALDO_AR_CHEQ"]);
                            if (resultado["SALDO_AR_EFE"] != DBNull.Value) entidad.saldoefectivo = Convert.ToInt64(resultado["SALDO_AR_EFE"]);
                            if (resultado["CONSIGNACIONES"] != DBNull.Value) entidad.consignaciones = Convert.ToInt64(resultado["CONSIGNACIONES"]);
                            entidad.totalcheque =  entidad.saldocheque+entidad.CHEQUE;
                            entidad.totalefectivo = (entidad.saldoefectivo + entidad.EFECTIVO) - entidad.EGRESOS_EFECTIVO - entidad.consignaciones;
                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReportesCaja", "ListarReportemovdiario", ex);
                        return null;
                    }
                }
            }
        }
    }      
}
