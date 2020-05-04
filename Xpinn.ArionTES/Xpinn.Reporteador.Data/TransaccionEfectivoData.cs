using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Data
{
    public class TransaccionEfectivoData: GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public TransaccionEfectivoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<TransaccionEfectivo> ListaTransaccionEfectivo(DateTime pFecIni, DateTime pfecha, Usuario pUsuario)
        {
            DbDataReader resultado;
            List <TransaccionEfectivo> LstTran = new List<TransaccionEfectivo>();
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DateTime fechainicial = pFecIni == DateTime.MinValue ? new DateTime(pfecha.Year, pfecha.Month, 1) : pFecIni;
                        // Generar transacciones individuales mayores de $10Millones (parametro general 16) y acumulads mayores a $50 Millones (pàrametro general 17)
                        string sql = @"Select v.fecha, v.valor, v.moneda, v.cod_oficina, v.cod_ciudad, v.tipo_producto, v.tipo_tran, v.num_producto, v.tipo_identificacion, v.identificacion, 
                                        v.primer_apellido, v.segundo_apellido, v.primer_nombre, v.segundo_nombre, v.razon_social, v.actividad_economica, v.ingreso_mensual, v.tipo_identificacion2, v.identificacion2, 
                                        v.primer_apellido2, v.segundo_apellido2, v.primer_nombre2, v.segundo_nombre2 
                                        From V_TRANSACCION_EFECTIVO v 
                                        Where v.FECHA Between to_date('" + fechainicial.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And to_date('" + pfecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') And v.valor >= (select valor from general where codigo = 16)                                         
                                      Union all 
                                      Select v.fecha, Sum(v.valor) As valor, v.moneda, v.cod_oficina, v.cod_ciudad, v.tipo_producto, v.tipo_tran, v.num_producto, v.tipo_identificacion, v.identificacion, 
                                        v.primer_apellido, v.segundo_apellido, v.primer_nombre, v.segundo_nombre, v.razon_social, v.actividad_economica, v.ingreso_mensual, v.tipo_identificacion2, v.identificacion2,
                                        v.primer_apellido2, v.segundo_apellido2, v.primer_nombre2, v.segundo_nombre2  
                                        From V_TRANSACCION_EFECTIVO v
                                        Where v.identificacion In (Select IDENTIFICACION From V_TRANSACCION_EFECTIVO t Where t.FECHA Between to_date('" + fechainicial.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And to_date('" + pfecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') Group by IDENTIFICACION Having sum(VALOR) > (select valor from general where codigo = 17)) 
                                        And v.FECHA Between to_date('" + fechainicial.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And to_date('" + pfecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') 
                                        Group by v.fecha, v.moneda, v.cod_oficina, v.cod_ciudad, v.tipo_producto, v.tipo_tran, v.num_producto, v.tipo_identificacion, v.identificacion, 
                                        v.primer_apellido, v.segundo_apellido, v.primer_nombre, v.segundo_nombre, v.razon_social, v.actividad_economica, v.ingreso_mensual, v.identificacion2, v.tipo_identificacion2, 
                                        v.primer_apellido2, v.segundo_apellido2, v.primer_nombre2, v.segundo_nombre2  ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        Int64 CONSECUTIVO = 1;
                        while (resultado.Read())
                        {
                            TransaccionEfectivo entidad = new TransaccionEfectivo();
                                entidad.consecutivo = CONSECUTIVO;
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_tran = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor_tran = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.tipo_moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            if (resultado["COD_CIUDAD"] != DBNull.Value) entidad.cod_ciudad = Convert.ToString(resultado["COD_CIUDAD"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToString(resultado["TIPO_TRAN"]);
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) entidad.num_producto = Convert.ToString(resultado["NUM_PRODUCTO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion1 = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion1 = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido1 = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido1 = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre1 = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre1 = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social1 = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (resultado["ACTIVIDAD_ECONOMICA"] != DBNull.Value) entidad.actividad_economica = Convert.ToString(resultado["ACTIVIDAD_ECONOMICA"]);
                            if (resultado["INGRESO_MENSUAL"] != DBNull.Value) entidad.ingresos = Convert.ToString(resultado["INGRESO_MENSUAL"]);
                            if (resultado["TIPO_IDENTIFICACION2"] != DBNull.Value) entidad.tipo_identificacion2 = Convert.ToString(resultado["TIPO_IDENTIFICACION2"]);
                            if (resultado["IDENTIFICACION2"] != DBNull.Value) entidad.identificacion2 = Convert.ToString(resultado["IDENTIFICACION2"]);
                            if (resultado["PRIMER_APELLIDO2"] != DBNull.Value) entidad.primer_apellido2 = Convert.ToString(resultado["PRIMER_APELLIDO2"]);
                            if (resultado["SEGUNDO_APELLIDO2"] != DBNull.Value) entidad.segundo_apellido2 = Convert.ToString(resultado["SEGUNDO_APELLIDO2"]);
                            if (resultado["PRIMER_NOMBRE2"] != DBNull.Value) entidad.primer_nombre2 = Convert.ToString(resultado["PRIMER_NOMBRE2"]);
                            if (resultado["SEGUNDO_NOMBRE2"] != DBNull.Value) entidad.segundo_nombre2 = Convert.ToString(resultado["SEGUNDO_NOMBRE2"]);
                            LstTran.Add(entidad);
                            CONSECUTIVO = CONSECUTIVO + 1;
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return LstTran;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionEfectivoData", "ListaTransaccionEfectivo", ex);
                        return null;
                    }
                }
            }
        }



        public List<TransaccionEfectivo> ListaClientesExonerados(DateTime pfecha, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<TransaccionEfectivo> LstTran = new List<TransaccionEfectivo>();
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "Select v.* From V_TRANSACCION_EFECTIVO v ";
                        sql += "Where v.FECHA Between to_date('01/" + pfecha.Month.ToString() + "/" + pfecha.Year.ToString() + "', '" + conf.ObtenerFormatoFecha() + "') And to_date('" + pfecha.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And v.valor < (select valor from general where codigo = 16)";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        Int64 CONSECUTIVO = 1;
                        while (resultado.Read())
                        {
                            TransaccionEfectivo entidad = new TransaccionEfectivo();
                            entidad.consecutivo = CONSECUTIVO;
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha_tran = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor_tran = Convert.ToInt64(resultado["VALOR"]);
                            if (resultado["MONEDA"] != DBNull.Value) entidad.tipo_moneda = Convert.ToString(resultado["MONEDA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            if (resultado["COD_CIUDAD"] != DBNull.Value) entidad.cod_ciudad = Convert.ToString(resultado["COD_CIUDAD"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToString(resultado["TIPO_TRAN"]);
                            if (resultado["NUM_PRODUCTO"] != DBNull.Value) entidad.num_producto = Convert.ToString(resultado["NUM_PRODUCTO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion1 = Convert.ToString(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion1 = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["PRIMER_APELLIDO"] != DBNull.Value) entidad.primer_apellido1 = Convert.ToString(resultado["PRIMER_APELLIDO"]);
                            if (resultado["SEGUNDO_APELLIDO"] != DBNull.Value) entidad.segundo_apellido1 = Convert.ToString(resultado["SEGUNDO_APELLIDO"]);
                            if (resultado["PRIMER_NOMBRE"] != DBNull.Value) entidad.primer_nombre1 = Convert.ToString(resultado["PRIMER_NOMBRE"]);
                            if (resultado["SEGUNDO_NOMBRE"] != DBNull.Value) entidad.segundo_nombre1 = Convert.ToString(resultado["SEGUNDO_NOMBRE"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social1 = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (resultado["ACTIVIDAD_ECONOMICA"] != DBNull.Value) entidad.actividad_economica = Convert.ToString(resultado["ACTIVIDAD_ECONOMICA"]);
                            if (resultado["INGRESO_MENSUAL"] != DBNull.Value) entidad.ingresos = Convert.ToString(resultado["INGRESO_MENSUAL"]);
                            LstTran.Add(entidad);
                            CONSECUTIVO = CONSECUTIVO + 1;
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return LstTran;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("TransaccionEfectivoData", "ListaTransaccionEfectivo", ex);
                        return null;
                    }
                }
            }
        }


    }
}
