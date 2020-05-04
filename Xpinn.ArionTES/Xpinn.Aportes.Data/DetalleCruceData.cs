using System;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Aportes.Entities;
using System.Collections.Generic;

namespace Xpinn.Aportes.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla Det_cruce
    /// </summary>
    public class DetalleCruceData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la det_cruce
        /// </summary>
        public DetalleCruceData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public void CrearDetalleCruce(DetalleCruce pDetalleCruce, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pCodPersona = cmdTransaccionFactory.CreateParameter();
                        pCodPersona.ParameterName = "PCOD_PERSONA";
                        pCodPersona.Value = pDetalleCruce.Cod_persona;
                        pCodPersona.Direction = ParameterDirection.Input;
                        pCodPersona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodPersona);

                        DbParameter pNumeroProducto = cmdTransaccionFactory.CreateParameter();
                        pNumeroProducto.ParameterName = "PNUMERO_PRODUCTO";
                        pNumeroProducto.Value = pDetalleCruce.Numero_Producto;
                        pNumeroProducto.Direction = ParameterDirection.Input;
                        pNumeroProducto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pNumeroProducto);

                        DbParameter pLineaProducto = cmdTransaccionFactory.CreateParameter();
                        pLineaProducto.ParameterName = "PLINEA_PRODUCTO";
                        if (pDetalleCruce.Linea_Producto != null)
                            pLineaProducto.Value = pDetalleCruce.Linea_Producto;
                        else
                            pLineaProducto.Value = DBNull.Value;
                        pLineaProducto.Direction = ParameterDirection.Input;
                        pLineaProducto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pLineaProducto);

                        DbParameter pConcepto = cmdTransaccionFactory.CreateParameter();
                        pConcepto.ParameterName = "PCONCEPTO";
                        pConcepto.Value = pDetalleCruce.Concepto;
                        pConcepto.Direction = ParameterDirection.Input;
                        pConcepto.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pConcepto);

                        DbParameter pCapital = cmdTransaccionFactory.CreateParameter();
                        pCapital.ParameterName = "PCAPITAL";
                        pCapital.Value = pDetalleCruce.Capital;
                        pCapital.Direction = ParameterDirection.Input;
                        pCapital.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pCapital);

                        DbParameter pIntRendmimiento = cmdTransaccionFactory.CreateParameter();
                        pIntRendmimiento.ParameterName = "PINTERES_RENDIMIENTO";
                        pIntRendmimiento.Value = pDetalleCruce.Interes_rendimiento;
                        pIntRendmimiento.Direction = ParameterDirection.Input;
                        pIntRendmimiento.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pIntRendmimiento);

                        DbParameter pIntMora = cmdTransaccionFactory.CreateParameter();
                        pIntMora.ParameterName = "PINTERES_MORA";
                        pIntMora.Value = pDetalleCruce.Interes_Mora;
                        pIntMora.Direction = ParameterDirection.Input;
                        pIntMora.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pIntMora);

                        DbParameter pOtros = cmdTransaccionFactory.CreateParameter();
                        pOtros.ParameterName = "POTROS";
                        pOtros.Value = pDetalleCruce.Otros;
                        pOtros.Direction = ParameterDirection.Input;
                        pOtros.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pOtros);

                        DbParameter pRetencion = cmdTransaccionFactory.CreateParameter();
                        pRetencion.ParameterName = "PRETENCION";
                        pRetencion.Value = pDetalleCruce.Retencion;
                        pRetencion.Direction = ParameterDirection.Input;
                        pRetencion.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pRetencion);

                        DbParameter pSigno = cmdTransaccionFactory.CreateParameter();
                        pSigno.ParameterName = "PSIGNO";
                        pSigno.Value = pDetalleCruce.Signo;
                        pSigno.Direction = ParameterDirection.Input;
                        pSigno.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pSigno);

                        DbParameter pInteresCausado = cmdTransaccionFactory.CreateParameter();
                        pInteresCausado.ParameterName = "PINTERES_CAUSADO";
                        pInteresCausado.Value = pDetalleCruce.Interes_Causado;
                        pInteresCausado.Direction = ParameterDirection.Input;
                        pInteresCausado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pInteresCausado);

                        DbParameter pRetencionCausada = cmdTransaccionFactory.CreateParameter();
                        pRetencionCausada.ParameterName = "PRETENCION_CAUSADA";
                        pRetencionCausada.Value = pDetalleCruce.Retencion_Causada;
                        pRetencionCausada.Direction = ParameterDirection.Input;
                        pRetencionCausada.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pRetencionCausada);

                        DbParameter pSaldo = cmdTransaccionFactory.CreateParameter();
                        pSaldo.ParameterName = "PSALDO";
                        pSaldo.Value = pDetalleCruce.Saldo;
                        pSaldo.Direction = ParameterDirection.Input;
                        pSaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pSaldo);

                        DbParameter pTotal = cmdTransaccionFactory.CreateParameter();
                        pTotal.ParameterName = "PTOTAL";
                        pTotal.Value = pDetalleCruce.Total;
                        pTotal.Direction = ParameterDirection.Input;
                        pTotal.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pTotal);

                        DbParameter pCodOpe = cmdTransaccionFactory.CreateParameter();
                        pCodOpe.ParameterName = "PCOD_OPE";
                        if (pDetalleCruce.Cod_ope == null)
                            pCodOpe.Value = DBNull.Value;
                        else
                            pCodOpe.Value = pDetalleCruce.Cod_ope;
                        pCodOpe.Direction = ParameterDirection.Input;
                        pCodOpe.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodOpe);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_APO_DET_CRUCE_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetalleCruceData", "CrearDetalleCruce", ex);
                    }
                }
            }
        }


        public List<DetalleCruce> ConsultarDetallesCruce(long pCodPersona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DetalleCruce> lsDetalles = new List<DetalleCruce>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select COD_PERSONA,NUMERO_PRODUCTO,LINEA_PRODUCTO,CONCEPTO,CAPITAL,INTERES_RENDIMIENTO,INTERES_MORA," +
                                    "OTROS,RETENCION,SIGNO,INTERES_CAUSADO,RETENCION_CAUSADA,SALDO,TOTAL " +
                                    "FROM DET_CRUCE where COD_PERSONA = " + pCodPersona;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        DetalleCruce objDetalle;
                        while (resultado.Read())
                        {
                            objDetalle = new DetalleCruce();
                            if (resultado["COD_PERSONA"] != DBNull.Value) objDetalle.Cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) objDetalle.Numero_Producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["LINEA_PRODUCTO"] != DBNull.Value) objDetalle.Linea_Producto = Convert.ToString(resultado["LINEA_PRODUCTO"]);
                            if (resultado["CONCEPTO"] != DBNull.Value) objDetalle.Concepto = Convert.ToString(resultado["CONCEPTO"]);
                            if (resultado["CAPITAL"] != DBNull.Value) objDetalle.Capital = Convert.ToDecimal(resultado["CAPITAL"]);
                            if (resultado["INTERES_RENDIMIENTO"] != DBNull.Value) objDetalle.Interes_rendimiento = Convert.ToDecimal(resultado["INTERES_RENDIMIENTO"]);
                            if (resultado["INTERES_MORA"] != DBNull.Value) objDetalle.Interes_Mora = Convert.ToDecimal(resultado["INTERES_MORA"]);
                            if (resultado["OTROS"] != DBNull.Value) objDetalle.Otros = Convert.ToDecimal(resultado["OTROS"]);
                            if (resultado["RETENCION"] != DBNull.Value) objDetalle.Retencion = Convert.ToDecimal(resultado["RETENCION"]);
                            if (resultado["SIGNO"] != DBNull.Value) objDetalle.Signo = Convert.ToString(resultado["SIGNO"]);
                            if (resultado["INTERES_CAUSADO"] != DBNull.Value) objDetalle.Interes_Causado = Convert.ToDecimal(resultado["INTERES_CAUSADO"]);
                            if (resultado["RETENCION_CAUSADA"] != DBNull.Value) objDetalle.Retencion_Causada = Convert.ToDecimal(resultado["RETENCION_CAUSADA"]);
                            if (resultado["SALDO"] != DBNull.Value) objDetalle.Saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["TOTAL"] != DBNull.Value) objDetalle.Total = Convert.ToDecimal(resultado["TOTAL"]);
                            lsDetalles.Add(objDetalle);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lsDetalles;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AporteData", "ConsultarPersonaRetiro", ex);
                        return null;
                    }
                }
            }
        }

    }
}
