using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;
using System.Reflection;

namespace Xpinn.Asesores.Data
{
    public class DetallePagoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        Configuracion global = new Configuracion();
        string FormatoFecha = " ";

        public DetallePagoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
            FormatoFecha = global.ObtenerValorConfig("FormatoFechaBase");
        }

        public List<DetallePago> ListarValoresAdeudados(Int64 pNumeroRadicacion, Usuario pUsuario, int totalPago = 0)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetallePago> lstDetPago = new List<DetallePago>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNumRadicacion = cmdTransaccionFactory.CreateParameter();
                        pNumRadicacion.ParameterName = "P_NUMRADICACION";
                        pNumRadicacion.Direction = ParameterDirection.Input;
                        pNumRadicacion.Value = pNumeroRadicacion;

                        DbParameter pFechaPago = cmdTransaccionFactory.CreateParameter();
                        pFechaPago.ParameterName = "P_FECHA";
                        pFechaPago.Direction = ParameterDirection.Input;
                        pFechaPago.Value = DateTime.Now;

                        DbParameter P_TOTAL = cmdTransaccionFactory.CreateParameter();
                        P_TOTAL.ParameterName = "P_TOTAL";
                        P_TOTAL.Direction = ParameterDirection.Input;
                        P_TOTAL.Value = totalPago;

                        cmdTransaccionFactory.Parameters.Add(pNumRadicacion);
                        cmdTransaccionFactory.Parameters.Add(pFechaPago);
                        cmdTransaccionFactory.Parameters.Add(P_TOTAL);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_DETPRODUC_CONSULTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ListarValoresAdeudados", "XPF_AS_DETPRODUC_CONSULTAR", ex);
                        return null;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (connection.State == ConnectionState.Closed)
                            connection.Open();
                        string sql = ""; //"Select ac.* From Vasesoresdetallepago ac Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + " Order by ac.num_cuota";
                        sql = @"Select ac.numero_radicacion, ac.num_cuota, ac.fecha_cuota,
                                Sum(Case ac.cod_atr When 1 Then ac.valor Else 0 End) capital,
                                Sum(Case ac.cod_atr When 2 Then ac.valor Else 0 End) intcte,
                                Sum(Case ac.cod_atr When 3 Then ac.valor Else 0 End) intmora,
                                Sum(Nvl(Case ac.cod_atr When 25 Then ac.valor Else 0 End, 0) + Nvl(Case ac.cod_atr When 40 Then ac.valor Else 0 End, 0)) leyMiPyme,
                                Sum(Nvl(Case ac.cod_atr When 26 Then ac.valor Else 0 End, 0) + Nvl(Case ac.cod_atr When 41 Then ac.valor Else 0 End, 0)) iva_leyMiPyme,
                                Sum(Case ac.cod_atr When 11 Then ac.valor Else 0 End) cobranzas,
                                Sum(Case ac.cod_atr When 7 Then ac.valor Else 0 End) poliza,
                                Sum(Case ac.cod_atr When 43 Then ac.valor Else 0 End) garantias_comunitarias,
                                Sum(Case ac.cod_atr When 1  Then 0  When 2  Then 0  When 3  Then 0  When 25  Then  0  When 11  Then  0
                                      When 26  Then 0 When 40  Then  0 When 41  Then 0  When 43  Then 0 When 7  Then 0  Else ac.valor End) otros,
                                Sum(Case ac.cod_atr When 1 Then ac.saldo Else 0 End) saldo,
                                nvl(idavance,0) idavance
                                From temp_pagar ac
                                Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + @"
                                Group by ac.numero_radicacion, ac.num_cuota, ac.fecha_cuota,idavance
                                Order by ac.numero_radicacion, ac.num_cuota, ac.fecha_cuota,idavance";
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetallePago entidad = new DetallePago();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["numero_radicacion"].ToString());
                            if (resultado["num_cuota"] != DBNull.Value) entidad.NumCuota = Convert.ToInt64(resultado["num_cuota"].ToString());
                            if (resultado["fecha_cuota"] != DBNull.Value) entidad.FechaCuota = Convert.ToDateTime(resultado["fecha_cuota"].ToString());
                            if (resultado["capital"] != DBNull.Value) entidad.Capital = Convert.ToDecimal(resultado["capital"].ToString());
                            if (resultado["intcte"] != DBNull.Value) entidad.IntCte = Convert.ToDecimal(resultado["intcte"].ToString());
                            if (resultado["intmora"] != DBNull.Value) entidad.IntMora = Convert.ToDecimal(resultado["intmora"].ToString());
                            if (resultado["leyMiPyme"] != DBNull.Value) entidad.LeyMiPyme = Convert.ToDecimal(resultado["leyMiPyme"].ToString());
                            if (resultado["iva_leyMiPyme"] != DBNull.Value) entidad.ivaLeyMiPyme = Convert.ToDecimal(resultado["iva_leyMiPyme"].ToString());
                            if (resultado["poliza"] != DBNull.Value) entidad.Poliza = Convert.ToDecimal(resultado["poliza"].ToString());
                            if (resultado["otros"] != DBNull.Value) entidad.Otros = Convert.ToDecimal(resultado["otros"].ToString());
                            if (resultado["saldo"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["saldo"].ToString());
                            if (resultado["cobranzas"] != DBNull.Value) entidad.Cobranzas = Convert.ToDecimal(resultado["cobranzas"].ToString());
                            if (resultado["idavance"] != DBNull.Value) entidad.idavance = Convert.ToInt64(resultado["idavance"].ToString());

                            lstDetPago.Add(entidad);
                        }

                        if (connection.State == ConnectionState.Open)
                            dbConnectionFactory.CerrarConexion(connection);
                        return lstDetPago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetallePagoData", "ListarValoresAdeudados", ex);
                        return null;
                    }
                }
            }
        }

        public List<DetallePago> Listar(DateTime pFecha, Int64 pNumeroRadicacion, Usuario pUsuario, int calcularTotal = 0)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetallePago> lstDetPago = new List<DetallePago>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNumRadicacion = cmdTransaccionFactory.CreateParameter();
                        pNumRadicacion.ParameterName = "P_NUMRADICACION";
                        pNumRadicacion.Direction = ParameterDirection.Input;
                        pNumRadicacion.Value = pNumeroRadicacion;

                        DbParameter pFechaPago = cmdTransaccionFactory.CreateParameter();
                        pFechaPago.ParameterName = "P_FECHA";
                        pFechaPago.Direction = ParameterDirection.Input;
                        pFechaPago.Value = pFecha;

                        DbParameter pTotal = cmdTransaccionFactory.CreateParameter();
                        pTotal.ParameterName = "P_TOTAL";
                        pTotal.Direction = ParameterDirection.Input;
                        pTotal.Value = calcularTotal;

                        cmdTransaccionFactory.Parameters.Add(pNumRadicacion);
                        cmdTransaccionFactory.Parameters.Add(pFechaPago);
                        cmdTransaccionFactory.Parameters.Add(pTotal);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_DETPRODUC_CONSULTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Listar", "XPF_AS_DETPRODUC_CONSULTAR", ex);
                        return null;
                    }
                };
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        string sql = ""; //"Select ac.* From Vasesoresdetallepago ac Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + " Order by ac.num_cuota";
                        sql = @"Select ac.numero_radicacion, ac.num_cuota, ac.fecha_cuota,
                                Sum(Case ac.cod_atr When 1 Then ac.valor Else 0 End) capital,
                                Sum(Case ac.cod_atr When 2 Then ac.valor Else 0 End) intcte,
                                Sum(Case ac.cod_atr When 3 Then ac.valor Else 0 End) intmora,
                                Sum(Nvl(Case ac.cod_atr When 25 Then ac.valor Else 0 End, 0) + Nvl(Case ac.cod_atr When 40 Then ac.valor Else 0 End, 0)) leyMiPyme,
                                Sum(Nvl(Case ac.cod_atr When 26 Then ac.valor Else 0 End, 0) + Nvl(Case ac.cod_atr When 41 Then ac.valor Else 0 End, 0)) iva_leyMiPyme,
                                Sum(Case ac.cod_atr When 11 Then ac.valor Else 0 End) cobranzas,
                                Sum(Case ac.cod_atr When 7 Then ac.valor Else 0 End) poliza,
                                Sum(Case ac.cod_atr When 43 Then ac.valor Else 0 End) garantias_comunitarias,
                                Sum(Case ac.cod_atr When 1  Then 0  When 2  Then 0  When 3  Then 0  When 25  Then  0  When 11  Then  0
                                      When 26  Then 0 When 40  Then  0 When 41  Then 0  When 43  Then 0 When 7  Then 0  Else ac.valor End) otros,
                                Sum(Case ac.cod_atr When 1 Then ac.saldo Else 0 End) saldo,
                                nvl(idavance, 0) idavance
                                From temp_pagar ac
                                Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + @"
                                Group by ac.numero_radicacion, ac.num_cuota, ac.fecha_cuota, ac.idavance
                                Order by ac.numero_radicacion, ac.num_cuota, ac.fecha_cuota, ac.idavance";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetallePago entidad = new DetallePago();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["numero_radicacion"].ToString());
                            if (resultado["num_cuota"] != DBNull.Value) entidad.NumCuota = Convert.ToInt64(resultado["num_cuota"].ToString());
                            if (resultado["fecha_cuota"] != DBNull.Value) entidad.FechaCuota = Convert.ToDateTime(resultado["fecha_cuota"].ToString());
                            if (resultado["capital"] != DBNull.Value) entidad.Capital = Convert.ToDecimal(resultado["capital"].ToString());
                            if (resultado["intcte"] != DBNull.Value) entidad.IntCte = Convert.ToDecimal(resultado["intcte"].ToString());
                            if (resultado["intmora"] != DBNull.Value) entidad.IntMora = Convert.ToDecimal(resultado["intmora"].ToString());
                            if (resultado["leyMiPyme"] != DBNull.Value) entidad.LeyMiPyme = Convert.ToDecimal(resultado["leyMiPyme"].ToString());
                            if (resultado["iva_leyMiPyme"] != DBNull.Value) entidad.ivaLeyMiPyme = Convert.ToDecimal(resultado["iva_leyMiPyme"].ToString());
                            if (resultado["poliza"] != DBNull.Value) entidad.Poliza = Convert.ToDecimal(resultado["poliza"].ToString());
                            if (resultado["Garantias_Comunitarias"] != DBNull.Value) entidad.Garantias_Comunitarias = Convert.ToDecimal(resultado["Garantias_Comunitarias"].ToString());
                            if (resultado["otros"] != DBNull.Value) entidad.Otros = Convert.ToDecimal(resultado["otros"].ToString());
                            if (resultado["saldo"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["saldo"].ToString());
                            if (resultado["cobranzas"] != DBNull.Value) entidad.Cobranzas = Convert.ToDecimal(resultado["cobranzas"].ToString());
                            if (resultado["idavance"] != DBNull.Value) entidad.idavance = Convert.ToInt64(resultado["idavance"].ToString());

                            lstDetPago.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDetPago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetallePagoData", "Listar", ex);
                        return null;
                    }
                }
            }
        }


        public List<DetallePago> DistribuirPago(Int64 pNumeroRadicacion, DateTime pFechaPago, Int64 pValorPago, String pTipoPago, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetallePago> lstDetPago = new List<DetallePago>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                      
                        DbParameter pNumRadicacion = cmdTransaccionFactory.CreateParameter();
                        pNumRadicacion.ParameterName = "P_NUMRADICACION";
                        pNumRadicacion.Direction = ParameterDirection.Input;
                        pNumRadicacion.Value = pNumeroRadicacion;

                        DbParameter pFecPago = cmdTransaccionFactory.CreateParameter();
                        pFecPago.ParameterName = "P_FECHAPAGO";
                        pFecPago.Direction = ParameterDirection.Input;
                        pFecPago.DbType = DbType.Date;
                        pFecPago.Value = pFechaPago.ToString(FormatoFecha);

                        DbParameter pValPago = cmdTransaccionFactory.CreateParameter();
                        pValPago.ParameterName = "P_VALORPAGO";
                        pValPago.Direction = ParameterDirection.Input;
                        pValPago.Value = pValorPago;

                        DbParameter pTipPago = cmdTransaccionFactory.CreateParameter();
                        pTipPago.ParameterName = "P_TIPOPAGO";
                        pTipPago.Direction = ParameterDirection.Input;
                        pTipPago.Value = pTipoPago;
                     
                        cmdTransaccionFactory.Parameters.Add(pNumRadicacion);
                        cmdTransaccionFactory.Parameters.Add(pFecPago);
                        cmdTransaccionFactory.Parameters.Add(pValPago);
                        cmdTransaccionFactory.Parameters.Add(pTipPago);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_DISPAGO_CONSULTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select ac.* From TEMP_DISTPAGO ac Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + " Order by ac.num_cuota";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetallePago entidad = new DetallePago();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["numero_radicacion"].ToString());
                            if (resultado["numero_cuota"] != DBNull.Value) entidad.NumCuota = Convert.ToInt64(resultado["numero_cuota"].ToString());
                            if (resultado["fecha_cuota"] != DBNull.Value) entidad.FechaCuota = Convert.ToDateTime(resultado["fecha_cuota"].ToString());
                            if (resultado["capital"] != DBNull.Value) entidad.Capital = Convert.ToDecimal(resultado["capital"].ToString());
                            if (resultado["intcte"] != DBNull.Value) entidad.IntCte = Convert.ToDecimal(resultado["intcte"].ToString());
                            if (resultado["intmora"] != DBNull.Value) entidad.IntMora = Convert.ToDecimal(resultado["intmora"].ToString());
                            if (resultado["leyMiPyme"] != DBNull.Value) entidad.LeyMiPyme = Convert.ToDecimal(resultado["leyMiPyme"].ToString());
                            if (resultado["iva_leyMiPyme"] != DBNull.Value) entidad.ivaLeyMiPyme = Convert.ToDecimal(resultado["iva_leyMiPyme"].ToString());
                            if (resultado["poliza"] != DBNull.Value) entidad.Poliza = Convert.ToDecimal(resultado["poliza"].ToString());
                            if (resultado["otros"] != DBNull.Value) entidad.Otros = Convert.ToDecimal(resultado["otros"].ToString());
                            if (resultado["saldo"] != DBNull.Value) entidad.Saldo = Convert.ToDecimal(resultado["saldo"].ToString());
                            if (resultado["total"] != DBNull.Value) entidad.Total = Convert.ToDecimal(resultado["total"].ToString());
                            if (resultado["cobranzas"] != DBNull.Value) entidad.Cobranzas = Convert.ToDecimal(resultado["cobranzas"].ToString());

                            lstDetPago.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDetPago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetallePagoData", "DistribuirPago", ex);
                        return null;
                    }
                }
            }
        }

        public bool ListarValoresAPagar(DateTime pFecha, Int64 pNumeroRadicacion, ref decimal capital, ref decimal otros, Usuario pUsuario, int totalPago = 0)
        {
            DbDataReader resultado = default(DbDataReader);
            capital = 0;
            otros = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNumRadicacion = cmdTransaccionFactory.CreateParameter();
                        pNumRadicacion.ParameterName = "P_NUMRADICACION";
                        pNumRadicacion.Direction = ParameterDirection.Input;
                        pNumRadicacion.Value = pNumeroRadicacion;

                        DbParameter pFechaPago = cmdTransaccionFactory.CreateParameter();
                        pFechaPago.ParameterName = "P_FECHA";
                        pFechaPago.Direction = ParameterDirection.Input;
                        pFechaPago.Value = pFecha;

                        DbParameter P_TOTAL = cmdTransaccionFactory.CreateParameter();
                        P_TOTAL.ParameterName = "P_TOTAL";
                        P_TOTAL.Direction = ParameterDirection.Input;
                        P_TOTAL.Value = totalPago;

                        cmdTransaccionFactory.Parameters.Add(pNumRadicacion);
                        cmdTransaccionFactory.Parameters.Add(pFechaPago);
                        cmdTransaccionFactory.Parameters.Add(P_TOTAL);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_DETPRODUC_CONSULTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select ac.numero_radicacion, Sum(Case ac.cod_atr When 1 Then ac.valor Else 0 End) As capital, Sum(Case ac.cod_atr When 1 Then 0 Else ac.valor End) As otros From Temp_Pagar ac Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + " Group by ac.numero_radicacion";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {                            
                            if (resultado["capital"] != DBNull.Value) capital += Convert.ToDecimal(resultado["capital"].ToString());                            
                            if (resultado["otros"] != DBNull.Value) otros += Convert.ToDecimal(resultado["otros"].ToString());                            
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {                        
                        return false;
                    }
                }
            }
        }


        public List<Atributo> ListarDetallePago(DateTime pFecha, Int64 pNumeroRadicacion, Usuario pUsuario, int totalPago = 0)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Atributo> lstDetPago = new List<Atributo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pNumRadicacion = cmdTransaccionFactory.CreateParameter();
                        pNumRadicacion.ParameterName = "P_NUMRADICACION";
                        pNumRadicacion.Direction = ParameterDirection.Input;
                        pNumRadicacion.Value = pNumeroRadicacion;

                        DbParameter pFechaPago = cmdTransaccionFactory.CreateParameter();
                        pFechaPago.ParameterName = "P_FECHA";
                        pFechaPago.Direction = ParameterDirection.Input;
                        pFechaPago.Value = pFecha;

                        DbParameter P_TOTAL = cmdTransaccionFactory.CreateParameter();
                        P_TOTAL.ParameterName = "P_TOTAL";
                        P_TOTAL.Direction = ParameterDirection.Input;
                        P_TOTAL.Value = totalPago;

                        cmdTransaccionFactory.Parameters.Add(pNumRadicacion);
                        cmdTransaccionFactory.Parameters.Add(pFechaPago);
                        cmdTransaccionFactory.Parameters.Add(P_TOTAL);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "XPF_AS_DETPRODUC_CONSULTAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = @"Select ac.numero_radicacion, ac.cod_atr, a.nombre, Sum(ac.valor) As valor From temp_pagar ac Left Join atributos a On ac.cod_atr = a.cod_atr Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + " Group by ac.numero_radicacion, ac.cod_atr, a.nombre Order by ac.cod_atr";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Atributo entidad = new Atributo();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["numero_radicacion"].ToString());
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["cod_atr"].ToString());
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"].ToString());
                            if (resultado["valor"] != DBNull.Value) entidad.saldo_atributo = Convert.ToDecimal(resultado["valor"].ToString());

                            lstDetPago.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDetPago;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetallePagoData", "ListarDetallePago", ex);
                        return null;
                    }
                }
            }
        }



    }
}