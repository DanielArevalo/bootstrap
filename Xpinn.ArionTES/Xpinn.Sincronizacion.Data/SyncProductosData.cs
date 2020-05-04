using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Util;

namespace Xpinn.Sincronizacion.Data
{
    public class SyncProductosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;
        public SyncProductosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Producto> ListarProductosPersona(string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Producto> lstProductos = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM VCAJAPRODUCTOS_SYNC " + pFiltro + " ORDER BY COD_DEUDOR, NUMERO_RADICACION ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            lstProductos = new List<Producto>();
                            Producto entidad;
                            while (resultado.Read())
                            {
                                entidad = new Producto();
                                if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_DEUDOR"]);
                                if (resultado["CODTIPOPRODUCTO"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt32(resultado["CODTIPOPRODUCTO"]);
                                if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_RADICACION"]);
                                if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea_producto = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_linea = Convert.ToString(resultado["NOMBRE"]);
                                if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToDecimal(resultado["MONTO_APROBADO"]);
                                if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                                if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                                if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                                if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                                if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToInt32(resultado["DIAS_MORA"]);
                                if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valor_pago = Convert.ToDecimal(resultado["VALOR_A_PAGAR"]);
                                if (resultado["TOTAL_A_PAGAR"] != DBNull.Value) entidad.total_pago = Convert.ToDecimal(resultado["TOTAL_A_PAGAR"]);
                                if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.tipo_linea = Convert.ToInt32(resultado["TIPO_LINEA"]);
                                if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                                lstProductos.Add(entidad);
                            }
                            resultado.Close();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncProductosData", "ListarProductosPersona", ex);
                        return null;
                    }
                }
            }
        }


        public List<ObjectString> ListarTiraProductosPersona(int codigo, string pFiltro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ObjectString> lstProductos = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        /*
                        string pVista = string.Empty;
                        switch (codigo)
                        {
                            case 1:
                                pVista = "V_SYNC_APORTES";
                                break;
                            case 2:
                                pVista = "V_SYNC_CREDITOS";
                                break;
                            case 3:
                                pVista = "V_SYNC_AHORROS";
                                break;
                            case 4:
                                pVista = "V_SYNC_SERVICIOS";
                                break;
                            case 9:
                                pVista = "V_SYNC_PROGRAMADO";
                                break;
                        }
                        */
                        string sql = @"SELECT cod_tipo_producto || '|' || tipo_producto || '|' || COD_PERSONA || '|' || NUMERO_PRODUCTO || '|' || COD_LINEA_PRODUCTO || '|' ||
                                    nombre_linea || '|' || monto_aprobado || '|' || to_char(fecha_aprobacion, 'dd/MM/yyyy') || '|' ||  NVL(valor_cuota, 0) || '|' || NVL(Saldo_capital, 0) || '|' ||
                                    NVL(garantias_comunitarias, 0) || '|' || to_char(fecha_proximo_pago, 'dd/MM/yyyy') || '|' || NVL(dias_mora, 0) || '|' || NVL(valor_pago, 0) || '|' ||
                                    NVL(total_pago, 0) || '|' || NVL(tipo_linea, 0) || '|' || estado || '|' || nvl(cod_periodicidad, 0) AS TIRA
                                    FROM informe_productos_persona " + pFiltro + " ORDER BY COD_PERSONA, NUMERO_PRODUCTO ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            lstProductos = new List<ObjectString>();
                            ObjectString entidad = null;
                            while (resultado.Read())
                            {
                                entidad = new ObjectString();
                                if (resultado["TIRA"] != DBNull.Value) entidad.datosObjeto = resultado.GetString(0);
                                lstProductos.Add(entidad);
                            }
                            resultado.Close();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncProductosData", "ListarProductosPersona", ex);
                        return null;
                    }
                }
            }
        }

        public EntityGlobal SyncCantidadProductos(int codigo, Usuario vUsuario)
        {
            DbDataReader resultado;
            EntityGlobal pEntidad = new EntityGlobal();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COUNT(NUMERO_PRODUCTO) AS CANTIDADREG FROM  INFORME_PRODUCTOS_PERSONA WHERE COD_TIPO_PRODUCTO = " + codigo;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if(resultado.Read())
                            if (resultado["CANTIDADREG"] != DBNull.Value) pEntidad.NroRegisterAffected = Convert.ToInt32(resultado["CANTIDADREG"]);
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncProductosData", "SyncCantidadProductos", ex);
                        return null;
                    }
                }
            }
            return pEntidad;
        }



        public List<ObjectString> ListarTiraProductosPendientes(DateTime pFecGeneracion, string pTablaGen, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ObjectString> lstProductos = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pFecha = cmdTransaccionFactory.CreateParameter();
                        pFecha.ParameterName = "PFECHA";
                        pFecha.Value = pFecGeneracion;
                        pFecha.Direction = ParameterDirection.Input;
                        pFecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pFecha);

                        DbParameter pTabla = cmdTransaccionFactory.CreateParameter();
                        pTabla.ParameterName = "PTABLA";
                        pTabla.Value = pTablaGen;
                        pTabla.Direction = ParameterDirection.Input;
                        pTabla.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pTabla); 

                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SYN_PRODUCTOS_PENDI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncProductosData", "ListarTiraProductosPendientes", ex);
                        return null;
                    }
                }
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM TEMP_PRODUCTOS_PENDIENTES ";
                        
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.HasRows)
                        {
                            lstProductos = new List<ObjectString>();
                            ObjectString entidad = null;
                            while (resultado.Read())
                            {
                                entidad = new ObjectString();
                                if (resultado["DATA"] != DBNull.Value) entidad.datosObjeto = resultado.GetString(1);
                                lstProductos.Add(entidad);
                            }
                            resultado.Close();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SyncProductosData", "ListarTiraProductosPendientes", ex);
                        return null;
                    }
                }                
            }
        }



    }
}
