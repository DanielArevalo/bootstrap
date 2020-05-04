using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.NIIF.Entities;

namespace Xpinn.NIIF.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla PlanCuentasNIIFS
    /// </summary>
    public class AmortizacionNIFData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla PlanCuentasNIIFS
        /// </summary>
        public AmortizacionNIFData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }



        public void ModificarEstadoFechaNIIF(DateTime vFecha, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = vFecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_FECHAMOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                      
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AmortizacionNIIFData", "ModificarEstadoFechaNIIF", ex);
                     
                    }
                }
            }
        }

          
        public List<AmortizacionNIF> ListarAmortizacionNIIF(DateTime vFecha, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<AmortizacionNIF> lstAmortizacionNIIF = new List<AmortizacionNIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "PFECHA";
                        pfecha.Value = vFecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "PUSUARIO";
                        pusuario.Value = vUsuario.codusuario;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pusuario);

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_COSTOAMORTIZADO";
                        cmdTransaccionFactory.ExecuteNonQuery();                                                                        
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AmortizacionNIIFData", "ListarAmortizacionNIIF", ex);
                        return null;
                    }
                };

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select Credito_CostoAmortizado.*, TipoTasa.nombre As nomtipo_tasa from Credito_CostoAmortizado Left Join TipoTasa On Credito_CostoAmortizado.tipo_tasa = TipoTasa.cod_tipo_tasa Where fecha = To_Date('" + Convert.ToDateTime(vFecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') Order by numero_radicacion";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AmortizacionNIF entidad = new AmortizacionNIF();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto = Convert.ToDecimal(resultado["MONTO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt32(resultado["PLAZO"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToDecimal(resultado["CUOTA"]);
                            if (resultado["TASA"] != DBNull.Value) entidad.tasa = Convert.ToDecimal(resultado["TASA"]);
                            if (resultado["TIPO_TASA"] != DBNull.Value) entidad.tipo_tasa = Convert.ToInt32(resultado["TIPO_TASA"]);
                            if (resultado["NOMTIPO_TASA"] != DBNull.Value) entidad.nomtipo_tasa = Convert.ToString(resultado["NOMTIPO_TASA"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["SALDO_TOTAL"] != DBNull.Value) entidad.saldo_total = Convert.ToDecimal(resultado["SALDO_TOTAL"]);
                            if (resultado["PLAZO_FALTANTE"] != DBNull.Value) entidad.plazo_faltante = Convert.ToInt32(resultado["PLAZO_FALTANTE"]);
                            if (resultado["TASA_MERCADO"] != DBNull.Value) entidad.tasa_mercado = Convert.ToDecimal(resultado["TASA_MERCADO"]);
                            if (resultado["TIR"] != DBNull.Value) entidad.tir = Convert.ToDecimal(resultado["TIR"]);
                            if (resultado["VALOR_PRESENTE"] != DBNull.Value) entidad.valor_presente = Convert.ToDecimal(resultado["VALOR_PRESENTE"]);
                            if (resultado["VALOR_AJUSTE"] != DBNull.Value) entidad.valor_ajuste = Convert.ToDecimal(resultado["VALOR_AJUSTE"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToInt32(resultado["ESTADO"]);

                            lstAmortizacionNIIF.Add(entidad);
                        }

                        return lstAmortizacionNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AmortizacionNIIFData", "ListarAmortizacionNIIF", ex);
                        return null;
                    }
                }

            }
        }

        public List<DetalleAmortizacionNIIF> ListarDetalleAmortizacionNIIF(DateTime vFecha, Int64 vNumeroRadicacion, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetalleAmortizacionNIIF> lstAmortizacionNIIF = new List<DetalleAmortizacionNIIF>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();                
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "Select h.fecha_cuota, Sum(Case h.cod_atr When 1 Then h.valor End) As capital from Historico_Amortiza h Where h.fecha_historico = To_Date('" + Convert.ToDateTime(vFecha).ToShortDateString() + "','" + conf.ObtenerFormatoFecha() + "') And h.numero_radicacion = " + vNumeroRadicacion.ToString() + " Group by h.fecha_cuota Order by h.fecha_cuota";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetalleAmortizacionNIIF entidad = new DetalleAmortizacionNIIF();
                            if (resultado["FECHA_CUOTA"] != DBNull.Value) entidad.FechaCuota = Convert.ToDateTime(resultado["FECHA_CUOTA"]);
                            if (resultado["CAPITAL"] != DBNull.Value) entidad.Capital = Convert.ToDecimal(resultado["CAPITAL"]);

                            lstAmortizacionNIIF.Add(entidad);
                        }

                        return lstAmortizacionNIIF;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AmortizacionNIIFData", "ListarDetalleAmortizacionNIIF", ex);
                        return null;
                    }
                }

            }
        }

    }
}