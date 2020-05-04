using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Caja.Entities;
using System.Reflection;

namespace Xpinn.Caja.Data
{
    public class DetallePagData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        Configuracion global = new Configuracion();
        string FormatoFecha = " ";

        public DetallePagData()
        {
            dbConnectionFactory = new ConnectionDataBase();
            FormatoFecha = global.ObtenerValorConfig("FormatoFechaBase");
        }

        public List<DetallePagos> ListarValoresAdeudados(Int64 pNumeroRadicacion, Usuario pUsuario, int totalPago = 0)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetallePagos> lstDetPago = new List<DetallePagos>();

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
                        pFechaPago.Value = DateTime.Now;

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
                        string sql = "Select ac.* From Vasesoresdetallepago ac Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + " Order by ac.num_cuota";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetallePagos entidad = new DetallePagos();

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

                            lstDetPago.Add(entidad);
                        }
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

        public List<DetallePagos> Listar(DateTime pFecha, Int64 pNumeroRadicacion, Usuario pUsuario, int totalPago = 0)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetallePagos> lstDetPago = new List<DetallePagos>();

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
                        string sql = "Select ac.* From Vasesoresdetallepago ac Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + " Order by ac.num_cuota";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetallePagos entidad = new DetallePagos();

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


        public List<DetallePagos> DistribuirPago(Int64 pTipoProducto, Int64 pNumeroRadicacion, DateTime pFechaPago, decimal pValorPago, String pTipoPago, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetallePagos> lstDetPago = new List<DetallePagos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pTipo_Producto = cmdTransaccionFactory.CreateParameter();
                        pTipo_Producto.ParameterName = "P_TIPOPRODUCTO";
                        pTipo_Producto.Direction = ParameterDirection.Input;
                        pTipo_Producto.DbType = DbType.Int64;
                        pTipo_Producto.Value = pTipoProducto;

                        DbParameter pNumRadicacion = cmdTransaccionFactory.CreateParameter();
                        pNumRadicacion.ParameterName = "P_NUMRADICACION";
                        pNumRadicacion.Direction = ParameterDirection.Input;
                        pNumRadicacion.DbType = DbType.Int64;
                        pNumRadicacion.Value = pNumeroRadicacion;

                        DbParameter pFecPago = cmdTransaccionFactory.CreateParameter();
                        pFecPago.ParameterName = "P_FECHAPAGO";
                        pFecPago.Direction = ParameterDirection.Input;
                        pFecPago.DbType = DbType.Date;
                        pFecPago.Value = pFechaPago;

                        DbParameter pValPago = cmdTransaccionFactory.CreateParameter();
                        pValPago.ParameterName = "P_VALORPAGO";
                        pValPago.Direction = ParameterDirection.Input;
                        pNumRadicacion.DbType = DbType.Decimal;
                        pValPago.Value = pValorPago;

                        DbParameter pTipPago = cmdTransaccionFactory.CreateParameter();
                        pTipPago.ParameterName = "P_TIPOPAGO";
                        pTipPago.Direction = ParameterDirection.Input;
                        pNumRadicacion.DbType = DbType.String;
                        pTipPago.Value = pTipoPago;

                        cmdTransaccionFactory.Parameters.Add(pTipo_Producto);
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
                        string sql = "Select ac.* From TEMP_DISTPAGO ac Where ac.numero_radicacion = " + pNumeroRadicacion.ToString() + " Order by ac.numero_cuota";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetallePagos entidad = new DetallePagos();

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

    }
}