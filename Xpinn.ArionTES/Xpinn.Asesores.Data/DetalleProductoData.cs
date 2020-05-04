using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class DetalleProductoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public DetalleProductoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<DetalleProducto> Listar(Producto pEntityProducto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetalleProducto> lstProductos = new List<DetalleProducto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql;

                        if (Convert.ToInt64(pEntityProducto.CodRadicacion) > 0)
                        {
                            sql = "SELECT * FROM  VASESORESCLIENTEDETALLEPRODUCT WHERE NUMERO_RADICACION = " + pEntityProducto.CodRadicacion;
                            if (pEntityProducto.Persona.IdPersona > 0)
                                sql = sql + " AND COD_DEUDOR = " + pEntityProducto.Persona.IdPersona;
                        }
                        else
                        {
                            sql = "SELECT * FROM  VASESORESCLIENTEDETALLEPRODUCT WHERE COD_DEUDOR = " + pEntityProducto.Persona.IdPersona;
                        }
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetalleProducto entidad = new DetalleProducto();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroRadicacion            = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.Producto.CodRadicacion      = Convert.ToString(resultado["numero_radicacion"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value)        entidad.Producto.Persona.IdPersona  = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.Producto.CodLineaCredito    = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["numero_obligacion"] != DBNull.Value) entidad.NumeroObligacion            = Convert.ToInt64(resultado["numero_obligacion"]);
                            if (resultado["PROCESOCOBRO"] != DBNull.Value)       entidad.EstadoCredito              = resultado["PROCESOCOBRO"].ToString();
                            if (resultado["destinacion"] != DBNull.Value)       entidad.Destinacion                 = resultado["destinacion"].ToString();
                            if (resultado["linea"] != DBNull.Value)             entidad.Producto.Linea              = resultado["linea"].ToString();
                            if (resultado["estado"] != DBNull.Value)            entidad.Producto.Estado             = resultado["estado"].ToString();
                            if (resultado["forma_pago"] != DBNull.Value)        entidad.FormaPago                   = resultado["forma_pago"].ToString();
                            if (resultado["cuotas_pagadas"] != DBNull.Value)    entidad.Producto.CuotasPagadas      = Convert.ToInt64(resultado["cuotas_pagadas"]);
                            if (resultado["PLAZO"] != DBNull.Value)             entidad.Producto.Plazo              = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["formato_plazo"] != DBNull.Value)     entidad.FormatoPlazo                = resultado["formato_plazo"].ToString();
                            if (resultado["periodicidad"] != DBNull.Value)      entidad.periodicidad                = resultado["periodicidad"].ToString();
                            if (resultado["tasa"] != DBNull.Value)              entidad.TasaNM                      = Convert.ToDouble(resultado["tasa"]);
                            if (resultado["calif_promedio"] != DBNull.Value)    entidad.Producto.CalifPromedio      = Convert.ToInt64(resultado["calif_promedio"]);
                            if (resultado["monto_solicitado"] != DBNull.Value)  entidad.MontoSolicitado             = Convert.ToInt64(resultado["monto_solicitado"]);
                            if (resultado["monto_aprobado"] != DBNull.Value)    entidad.Producto.MontoAprobado      = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["saldo_capital"] != DBNull.Value)     entidad.Producto.SaldoCapital       = Convert.ToInt64(resultado["saldo_capital"]);
                            if (resultado["valor_cuota"] != DBNull.Value)       entidad.Producto.Cuota              = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["atributos"] != DBNull.Value)         entidad.Producto.Atributos          = Convert.ToInt64(resultado["atributos"]);
                            if (resultado["saldo_pendiente"] != DBNull.Value)   entidad.SaldoPendiente              = Convert.ToInt64(resultado["saldo_pendiente"]);
                            if (resultado["valor_a_pagar"] != DBNull.Value)     entidad.Producto.ValorAPagar        = Convert.ToInt64(resultado["valor_a_pagar"]);
                            if (resultado["total_a_pagar"] != DBNull.Value)     entidad.Producto.ValorTotalAPagar   = Convert.ToInt64(resultado["total_a_pagar"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value)   entidad.Producto.FechaSolicitud     = Convert.ToDateTime(resultado["fecha_solicitud"]);
                            if (resultado["fecha_aprobacion"] != DBNull.Value)  entidad.FechaAprobacion             = Convert.ToDateTime(resultado["fecha_aprobacion"]);
                            if (resultado["fecha_desembolso"] != DBNull.Value)  entidad.FechaDesembolso             = Convert.ToDateTime(resultado["fecha_desembolso"]);
                            if (resultado["fecha_terminacion"] != DBNull.Value) entidad.FechaTerminacion            = Convert.ToDateTime(resultado["fecha_terminacion"]);
                            if (resultado["fecha_ultimo_pago"] != DBNull.Value) entidad.FechaUltimoPago             = Convert.ToDateTime(resultado["fecha_ultimo_pago"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value)entidad.FechaProximoPago            = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["oficina"] != DBNull.Value)     entidad.Producto.Oficina.NombreOficina    = Convert.ToString(resultado["oficina"]);
                            if (resultado["TIPOLINEA"] != DBNull.Value)         entidad.Producto.TipoLinea = Convert.ToInt64(resultado["TIPOLINEA"]);

                            lstProductos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetalleProducto", "ListarDetalleProductos", ex);
                        return null;
                    }
                }
            }
        }

        public List<DetalleProducto> ListarValoresAdeudados(Int64 pRadicado, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<DetalleProducto> lstProductos = new List<DetalleProducto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql;
                        sql = "SELECT * FROM  VASESORESCLIENTEDETALLEPRODUCT WHERE NUMERO_RADICACION = " + pRadicado;
                                             
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            DetalleProducto entidad = new DetalleProducto();

                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.NumeroRadicacion = Convert.ToInt64(resultado["numero_radicacion"]);
                            if (resultado["numero_radicacion"] != DBNull.Value) entidad.Producto.CodRadicacion = Convert.ToString(resultado["numero_radicacion"]);
                            if (resultado["cod_deudor"] != DBNull.Value) entidad.Producto.Persona.IdPersona = Convert.ToInt64(resultado["cod_deudor"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.Producto.CodLineaCredito = Convert.ToString(resultado["cod_linea_credito"]);
                            if (resultado["numero_obligacion"] != DBNull.Value) entidad.NumeroObligacion = Convert.ToInt64(resultado["numero_obligacion"]);
                            if (resultado["procesocobro"] != DBNull.Value) entidad.EstadoCredito = resultado["procesocobro"].ToString();
                            if (resultado["destinacion"] != DBNull.Value) entidad.Destinacion = resultado["destinacion"].ToString();
                            if (resultado["linea"] != DBNull.Value) entidad.Producto.Linea = resultado["linea"].ToString();
                            if (resultado["estado"] != DBNull.Value) entidad.Producto.Estado = resultado["estado"].ToString();
                            if (resultado["forma_pago"] != DBNull.Value) entidad.FormaPago = resultado["forma_pago"].ToString();
                            if (resultado["cuotas_pagadas"] != DBNull.Value) entidad.Producto.CuotasPagadas = Convert.ToInt64(resultado["cuotas_pagadas"]);
                            if (resultado["plazo"] != DBNull.Value) entidad.Producto.Plazo = Convert.ToInt64(resultado["plazo"]);
                            if (resultado["formato_plazo"] != DBNull.Value) entidad.FormatoPlazo = resultado["formato_plazo"].ToString();
                            if (resultado["periodicidad"] != DBNull.Value) entidad.periodicidad = resultado["periodicidad"].ToString();
                            if (resultado["tasa"] != DBNull.Value) entidad.TasaNM = Convert.ToDouble(resultado["tasa"]);
                            if (resultado["calif_promedio"] != DBNull.Value) entidad.Producto.CalifPromedio = Convert.ToInt64(resultado["calif_promedio"]);
                            if (resultado["monto_solicitado"] != DBNull.Value) entidad.MontoSolicitado = Convert.ToInt64(resultado["monto_solicitado"]);
                            if (resultado["monto_aprobado"] != DBNull.Value) entidad.Producto.MontoAprobado = Convert.ToInt64(resultado["monto_aprobado"]);
                            if (resultado["saldo_capital"] != DBNull.Value) entidad.Producto.SaldoCapital = Convert.ToInt64(resultado["saldo_capital"]);
                            if (resultado["valor_cuota"] != DBNull.Value) entidad.Producto.Cuota = Convert.ToInt64(resultado["valor_cuota"]);
                            if (resultado["atributos"] != DBNull.Value) entidad.Producto.Atributos = Convert.ToInt64(resultado["atributos"]);
                            if (resultado["saldo_pendiente"] != DBNull.Value) entidad.SaldoPendiente = Convert.ToInt64(resultado["saldo_pendiente"]);
                            if (resultado["valor_a_pagar"] != DBNull.Value) entidad.Producto.ValorAPagar = Convert.ToInt64(resultado["valor_a_pagar"]);
                            if (resultado["total_a_pagar"] != DBNull.Value) entidad.Producto.ValorTotalAPagar = Convert.ToInt64(resultado["total_a_pagar"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.Producto.FechaSolicitud = Convert.ToDateTime(resultado["fecha_solicitud"]);
                            if (resultado["fecha_aprobacion"] != DBNull.Value) entidad.FechaAprobacion = Convert.ToDateTime(resultado["fecha_aprobacion"]);
                            if (resultado["fecha_desembolso"] != DBNull.Value) entidad.FechaDesembolso = Convert.ToDateTime(resultado["fecha_desembolso"]);
                            if (resultado["fecha_terminacion"] != DBNull.Value) entidad.FechaTerminacion = Convert.ToDateTime(resultado["fecha_terminacion"]);
                            if (resultado["fecha_ultimo_pago"] != DBNull.Value) entidad.FechaUltimoPago = Convert.ToDateTime(resultado["fecha_ultimo_pago"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.FechaProximoPago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["oficina"] != DBNull.Value) entidad.Producto.Oficina.NombreOficina = Convert.ToString(resultado["oficina"]);

                            lstProductos.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("DetalleProducto", "ListarDetalleProductos", ex);
                        return null;
                    }
                }
            }
        }
    }
}