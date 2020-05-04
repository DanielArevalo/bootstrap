using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;
using Xpinn.Util;


namespace Xpinn.Asesores.Business
{
    public class ConsultarAvanceData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ConsultarAvanceData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<ConsultaAvance> Listar(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            return Listar(pNumeroRadicacion, "", pUsuario);
        }


        public List<ConsultaAvance> Listar(Int64 pNumeroRadicacion, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ConsultaAvance> lstAvances = new List<ConsultaAvance>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select idavance, fecha_desembolso, fecha_proximo_pago, fecha_ult_pago,valor_desembolsado,valor_cuota,
                                        plazo, cuotas_pagadas, cuotas_pendientes, fecha_solicitud,fecha_aprobacion, saldo_avance, tasa_interes, 
                                        case Estado when 'C' then 'Desembolsado' when 'T' then 'Terminado' end as Estado,
                                        Liquidar_AvanceConsultar_DetID(sysdate, IDAVANCE, 2) TotalaPagar,
                                        Liquidar_AvanceConsultar_DetID(sysdate, IDAVANCE, 2) - SALDO_AVANCE intereses, NUMERO_RADICACION
                                       From CREDITO_AVANCE  Where  NUMERO_RADICACION=" + pNumeroRadicacion + " And (ESTADO='C' or ESTADO='T') " + pFiltro + " Order by IDAVANCE";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ConsultaAvance entidad = new ConsultaAvance();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.Numero_Radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDAVANCE"] != DBNull.Value) entidad.NumAvance = Convert.ToInt32(resultado["IDAVANCE"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.FechaDesembolsi = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"].ToString());
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.FechaProxPago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"].ToString());
                            if (resultado["FECHA_ULT_PAGO"] != DBNull.Value) entidad.FechaUltiPago = Convert.ToDateTime(resultado["FECHA_ULT_PAGO"].ToString());
                            if (resultado["VALOR_DESEMBOLSADO"] != DBNull.Value) entidad.ValDesembolso = Convert.ToInt64(resultado["VALOR_DESEMBOLSADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.CuotasPagadas = Convert.ToInt32(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CUOTAS_PENDIENTES"] != DBNull.Value) entidad.CuotasPendi = Convert.ToInt32(resultado["CUOTAS_PENDIENTES"]);
                            if (resultado["SALDO_AVANCE"] != DBNull.Value) entidad.SaldoAvance = Convert.ToInt32(resultado["SALDO_AVANCE"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.Tasa = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["Estado"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["Estado"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.ValorCuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["TotalaPagar"] != DBNull.Value) entidad.ValorTotal = Convert.ToInt64(resultado["TotalaPagar"]);
                            if (resultado["Intereses"] != DBNull.Value) entidad.Intereses = Convert.ToInt64(resultado["Intereses"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.Fecha_Solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_Aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);

                            //if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);
                            //if (resultado["FECHACREACION"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREACION"]);
                            //if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["USUARIOCREACION"]);
                            //if (resultado["FECULTMOD"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["FECULTMOD"]);
                            //if (resultado["USUARIOMOD"] != DBNull.Value) entidad.usuariomod = Convert.ToString(resultado["USUARIOMOD"]);
                            lstAvances.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAvances;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ChequeraData", "ListarChequera", ex);
                        return null;
                    }
                }
            }
        }

        public ConsultaAvance ModificarAvances(ConsultaAvance avance, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pnumero_radicacion = cmdTransaccionFactory.CreateParameter();
                        pnumero_radicacion.ParameterName = "LNUM_AVANCE";
                        pnumero_radicacion.Value = avance.NumAvance;
                        pnumero_radicacion.Direction = ParameterDirection.Input;
                        pnumero_radicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_radicacion);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "LESTADO";
                        p_estado.Value = avance.Estado;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter p_plazo = cmdTransaccionFactory.CreateParameter();
                        p_plazo.ParameterName = "LPLAZO";
                        p_plazo.Value = avance.Plazo;
                        p_plazo.Direction = ParameterDirection.Input;
                        p_plazo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_plazo);

                        DbParameter p_tasa = cmdTransaccionFactory.CreateParameter();
                        p_tasa.ParameterName = "LTASA";
                        p_tasa.Value = avance.Tasa;
                        p_tasa.Direction = ParameterDirection.Input;
                        p_tasa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_tasa);

                        DbParameter fe_pago = cmdTransaccionFactory.CreateParameter();
                        fe_pago.ParameterName = "FE_PROX_PAGO";
                        fe_pago.Value = avance.FechaProxPago;
                        fe_pago.Direction = ParameterDirection.Input;
                        fe_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fe_pago);

                        DbParameter cuotas_pen = cmdTransaccionFactory.CreateParameter();
                        cuotas_pen.ParameterName = "CUOTAS_PEN";
                        cuotas_pen.Value = avance.CuotasPendi;
                        cuotas_pen.Direction = ParameterDirection.Input;
                        cuotas_pen.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(cuotas_pen);

                        DbParameter cuotas_paga = cmdTransaccionFactory.CreateParameter();
                        cuotas_paga.ParameterName = "CUOTA_PAGA";
                        cuotas_paga.Value = avance.CuotasPagadas;
                        cuotas_paga.Direction = ParameterDirection.Input;
                        cuotas_paga.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(cuotas_paga);

                        DbParameter fecha_ulti_pago = cmdTransaccionFactory.CreateParameter();
                        fecha_ulti_pago.ParameterName = "P_FE_UL_PAGO";
                        fecha_ulti_pago.Value = avance.FechaUltiPago;
                        fecha_ulti_pago.Direction = ParameterDirection.Input;
                        fecha_ulti_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(fecha_ulti_pago);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CRE_CREDAVANCES_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                       

                        return avance;
                    }
                    catch (Exception ex)
                    {
                        
                        BOExcepcion.Throw("AvanceData", "ModificarControlCreditos", ex);
                        return null;
                    }
                }
            }
        }
    }
}
