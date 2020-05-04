using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    public class ReporteData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public ReporteData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Reporte> ListarReporteactivo(Usuario pUsuario, Int64 codigo)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT d.COD_DEUDOR, d.IDENTIFICACION, d.NOMBRE, b.COD_LINEA_CREDITO LIN, B.NUMero_RADICacion, b.FECHA_PROXIMO_PAGO PROXIMO_PAGO, " +
                          " B.MONTO_APRObado, b.saldo_capital,  b.VALOR_CUOTA, b.NUMERO_CUOTAS, b.CUOTAS_PAGADAS, b.COD_ASESOR_COM, " +
                          " e.REFERENCIA PAGARE,D.DIRECCION_EMPRESA,d.TELEFONO_EMPRESA, d.BARRIO_EMPRESA, d.DIRECCION_NEGOCIO," +
                          " D.TELEFONO_NEGOCIO, D.BARRIO_NEGOCIO,D.DIRECCION_RESIDENCIA,D.BARRIO_RESIDENCIA,D.TELEFONO_RESIDENCIA,D.CELULAR,D.CIUDAD,B.COD_OFICINA AS OFICINA" +
                          " FROM  credito B,  V_DIRECCION_CRE_REPOR D, DOCUMENTOSGARANTIA E WHERE b.COD_DEUDOR = D.COD_DEUDOR And E.NUMERO_RADICACION = B.NUMERO_RADICACION " +
                          " AND D.COD_DEUDOR= B.COD_DEUDOR AND B.SALDO_CAPital != 0 " +
                          " And B.COD_LINEA_CREDITO != 405 And E.TIPO_DOCUMENTO= 1  And b.COD_OFICINA = " + codigo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["LIN"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LIN"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToString(resultado["PROXIMO_PAGO"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.codigo_asesor = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["DIRECCION_EMPRESA"] != DBNull.Value) entidad.direccion_oficina = Convert.ToString(resultado["DIRECCION_EMPRESA"]);
                            if (resultado["BARRIO_EMPRESA"] != DBNull.Value) entidad.barrio_empresa = Convert.ToString(resultado["BARRIO_EMPRESA"]);
                            if (resultado["TELEFONO_EMPRESA"] != DBNull.Value) entidad.telefono_empresa = Convert.ToString(resultado["TELEFONO_EMPRESA"]);
                            if (resultado["DIRECCION_NEGOCIO"] != DBNull.Value) entidad.direccion_negocio = Convert.ToString(resultado["DIRECCION_NEGOCIO"]);
                            if (resultado["BARRIO_NEGOCIO"] != DBNull.Value) entidad.barrio_negocio = Convert.ToString(resultado["BARRIO_NEGOCIO"]);
                            if (resultado["TELEFONO_NEGOCIO"] != DBNull.Value) entidad.telefono_negocio = Convert.ToString(resultado["TELEFONO_NEGOCIO"]);
                            if (resultado["DIRECCION_RESIDENCIA"] != DBNull.Value) entidad.direccion_residencia = Convert.ToString(resultado["DIRECCION_RESIDENCIA"]);
                            if (resultado["BARRIO_RESIDENCIA"] != DBNull.Value) entidad.barrio_residencia = Convert.ToString(resultado["BARRIO_RESIDENCIA"]);
                            if (resultado["TELEFONO_RESIDENCIA"] != DBNull.Value) entidad.telefono_residencia = Convert.ToString(resultado["TELEFONO_RESIDENCIA"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["CIUDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);


                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteMora", ex);
                        return null;
                    }
                }
            }
        }
        public List<Reporte> ListarReporteactivoporasesor(Usuario pUsuario, Int64 codigo)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT d.COD_DEUDOR, d.IDENTIFICACION, d.NOMBRE, b.COD_LINEA_CREDITO LIN, B.NUMero_RADICacion, b.FECHA_PROXIMO_PAGO PROXIMO_PAGO, " +
                          " B.MONTO_APRObado, b.saldo_capital,  b.VALOR_CUOTA, b.NUMERO_CUOTAS, b.CUOTAS_PAGADAS, b.COD_ASESOR_COM, " +
                          " e.REFERENCIA PAGARE,D.DIRECCION_EMPRESA,d.TELEFONO_EMPRESA, d.BARRIO_EMPRESA, d.DIRECCION_NEGOCIO," +
                          " D.TELEFONO_NEGOCIO, D.BARRIO_NEGOCIO,D.DIRECCION_RESIDENCIA,D.BARRIO_RESIDENCIA,D.TELEFONO_RESIDENCIA,D.CELULAR,D.CIUDAD,B.COD_OFICINA AS OFICINA" +
                          " FROM  credito B,  V_DIRECCION_CRE_REPOR D, DOCUMENTOSGARANTIA E WHERE b.COD_DEUDOR = D.COD_DEUDOR And E.NUMERO_RADICACION = B.NUMERO_RADICACION " +
                          " AND D.COD_DEUDOR= B.COD_DEUDOR AND B.SALDO_CAPital != 0 " +
                          " And B.COD_LINEA_CREDITO != 405 And E.TIPO_DOCUMENTO= 1  And b.COD_ASESOR_COM = " + codigo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["LIN"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LIN"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToString(resultado["PROXIMO_PAGO"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.codigo_asesor = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["DIRECCION_EMPRESA"] != DBNull.Value) entidad.direccion_oficina = Convert.ToString(resultado["DIRECCION_EMPRESA"]);
                            if (resultado["BARRIO_EMPRESA"] != DBNull.Value) entidad.barrio_empresa = Convert.ToString(resultado["BARRIO_EMPRESA"]);
                            if (resultado["TELEFONO_EMPRESA"] != DBNull.Value) entidad.telefono_empresa = Convert.ToString(resultado["TELEFONO_EMPRESA"]);
                            if (resultado["DIRECCION_NEGOCIO"] != DBNull.Value) entidad.direccion_negocio = Convert.ToString(resultado["DIRECCION_NEGOCIO"]);
                            if (resultado["BARRIO_NEGOCIO"] != DBNull.Value) entidad.barrio_negocio = Convert.ToString(resultado["BARRIO_NEGOCIO"]);
                            if (resultado["TELEFONO_NEGOCIO"] != DBNull.Value) entidad.telefono_negocio = Convert.ToString(resultado["TELEFONO_NEGOCIO"]);
                            if (resultado["DIRECCION_RESIDENCIA"] != DBNull.Value) entidad.direccion_residencia = Convert.ToString(resultado["DIRECCION_RESIDENCIA"]);
                            if (resultado["BARRIO_RESIDENCIA"] != DBNull.Value) entidad.barrio_residencia = Convert.ToString(resultado["BARRIO_RESIDENCIA"]);
                            if (resultado["TELEFONO_RESIDENCIA"] != DBNull.Value) entidad.telefono_residencia = Convert.ToString(resultado["TELEFONO_RESIDENCIA"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["CIUDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);


                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteactivoporasesor", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> ListarProductosPorAsesor(string cod_asesor, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select 
                                          aho.numero_cuenta, aho.fecha_apertura, 'Ahorro Vista' as TipoProducto, aho.saldo_total,
                                          per.IDENTIFICACION, per.PRIMER_NOMBRE || ' ' || per.PRIMER_APELLIDO as Nombre, per.RAZON_SOCIAL,
                                          CASE aho.estado WHEN 0 THEN 'Apertura' WHEN 1 THEN 'Activa' WHEN 2 THEN 'Inactiva' WHEN 3 THEN 'Bloqueada' WHEN 4 THEN 'Cerrada' WHEN 5 THEN 'Embargada' END as Estado,
                                          ofi.nombre as nombre_oficina
                                        from ahorro_vista aho
                                        join persona per on per.COD_PERSONA = aho.COD_PERSONA
                                        join oficina ofi on ofi.COD_OFICINA = aho.COD_OFICINA
                                        where aho.COD_ASESOR = " + cod_asesor +
                                        " order by 2 desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["numero_cuenta"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["numero_cuenta"]);
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["fecha_apertura"]);
                            if (resultado["saldo_total"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["saldo_total"]);
                            if (resultado["TipoProducto"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TipoProducto"]);
                            if (resultado["Estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["Estado"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombre"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (resultado["Nombre_Oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["Nombre_Oficina"]);

                            lstPrograma.Add(entidad);
                        }

                        sql = @"select aho.numero_programado, aho.fecha_apertura, 'Ahorro Programado' as TipoProducto, aho.saldo,
                                  per.IDENTIFICACION, per.PRIMER_NOMBRE || ' ' || per.PRIMER_APELLIDO as Nombre, per.RAZON_SOCIAL,
                                  //CASE aho.estado WHEN 0 THEN 'Inactiva' WHEN 1 THEN 'Activa' WHEN 2 THEN 'Terminada' WHEN 3 THEN 'Anulada' END as Estado,
                                  ofi.nombre as nombre_oficina
                                from ahorro_programado aho
                                join persona per on per.COD_PERSONA = aho.COD_PERSONA
                                join oficina ofi on ofi.COD_OFICINA = aho.COD_OFICINA
                                where aho.COD_ASESOR = " + cod_asesor +
                                " order by 2 desc ";

                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["numero_programado"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["numero_programado"]);
                            if (resultado["fecha_apertura"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["fecha_apertura"]);
                            if (resultado["saldo"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["saldo"]);
                            if (resultado["TipoProducto"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TipoProducto"]);
                            if (resultado["Estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["Estado"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombre"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (resultado["Nombre_Oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["Nombre_Oficina"]);

                            lstPrograma.Add(entidad);
                        }

                        sql = @"select aho.numero_tarjeta, aho.FECHA_ASIGNACION, 'Tarjeta' as TipoProducto, aho.saldo_total,
                                  per.IDENTIFICACION, per.PRIMER_NOMBRE || ' ' || per.PRIMER_APELLIDO as Nombre, per.RAZON_SOCIAL,
                                  CASE aho.estado WHEN 0 THEN 'Pendiente' WHEN 1 THEN 'Activa' WHEN 2 THEN 'Inactiva' WHEN 3 THEN 'Bloqueda' END as Estado,
                                  ofi.nombre as nombre_oficina
                                from tarjeta aho
                                join persona per on per.COD_PERSONA = aho.COD_PERSONA
                                join oficina ofi on ofi.COD_OFICINA = aho.COD_OFICINA
                                where aho.COD_ASESOR = " + cod_asesor +
                                " order by 2 desc ";

                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["numero_tarjeta"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["numero_tarjeta"]);
                            if (resultado["FECHA_ASIGNACION"] != DBNull.Value) entidad.fecha_apertura = Convert.ToDateTime(resultado["FECHA_ASIGNACION"]);
                            if (resultado["saldo_total"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["saldo_total"]);
                            if (resultado["TipoProducto"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TipoProducto"]);
                            if (resultado["Estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["Estado"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombre"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (resultado["Nombre_Oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["Nombre_Oficina"]);

                            lstPrograma.Add(entidad);
                        }

                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarAfiliacionPorAsesor", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> ListarAfiliacionPorAsesor(string cod_asesor, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select 
                                          pera.IDAFILIACION, pera.FECHA_AFILIACION, pera.VALOR, 
                                          CASE pera.Estado WHEN 'A' THEN 'Activo' WHEN 'R' THEN 'Retirado' END as Estado,
                                          per.IDENTIFICACION, per.PRIMER_NOMBRE || ' ' || per.PRIMER_APELLIDO as Nombre, per.RAZON_SOCIAL,
                                          ofi.NOMBRE as Nombre_Oficina
                                        from persona_afiliacion pera
                                        join persona per on pera.COD_PERSONA = per.COD_PERSONA
                                        join oficina ofi on per.COD_OFICINA = ofi.COD_OFICINA 
                                        WHERE pera.COD_ASESOR = " + cod_asesor +
                                        " ORDER BY 2 DESC ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["IDAFILIACION"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["IDAFILIACION"]);
                            if (resultado["FECHA_AFILIACION"] != DBNull.Value) entidad.fecha_afiliacion = Convert.ToDateTime(resultado["FECHA_AFILIACION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["Estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["Estado"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["Nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["Nombre"]);
                            if (resultado["RAZON_SOCIAL"] != DBNull.Value) entidad.razon_social = Convert.ToString(resultado["RAZON_SOCIAL"]);
                            if (resultado["Nombre_Oficina"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["Nombre_Oficina"]);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarAfiliacionPorAsesor", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> ListarReportecartejecutivo(Usuario pUsuario, Int64 codigo)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT A.CODIGO, A.CEDULA_CLIENTE, A.NOMBRE_CLIENTE, A.CREDITO, A.LIN, A.MONTO, to_char(fecha_desembolso,'mm/dd/yyyy') fecha_desembolso, A.SALDO," +
                        "to_char(FECHA_PROX,'mm/dd/yyyy') Fecha_proximo_pago, (case when (round(sysdate - fecha_prox)<0) then 0 else (round(sysdate - fecha_prox)) end) dias_mora, A.NOMBRE" +
                        " FROM COLOCACION_X_EJECUTIVO A WHERE A.SALDO != 0 And A.CODIGO = " + codigo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["CEDULA_CLIENTE"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["CEDULA_CLIENTE"]);
                            if (resultado["NOMBRE_CLIENTE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE_CLIENTE"]);
                            if (resultado["CREDITO"] != DBNull.Value) entidad.credito = Convert.ToInt64(resultado["CREDITO"]);
                            if (resultado["LIN"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LIN"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto_pago = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fecha_desenbolso = Convert.ToString(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToString(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["NOMBRE"]);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteMora", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método que permite consultar los créditos de un asesor
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarReporteMora(Usuario pUsuario, Int64 codigo)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();
            //string fecha_acuerdostring="";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM V_REPORTE_MORA A WHERE A.I_COD_PROMOTOR = " + codigo + " AND A.COD_LINEA_CREDITO NOT IN(SELECT COD_LINEA_CREDITO FROM PARAMETROS_LINEA WHERE COD_PARAMETRO = 320)";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["I_CODIGO"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["I_CODIGO"]);
                            if (resultado["C_NOMBRES_CLIENTE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["C_NOMBRES_CLIENTE"]);
                            if (resultado["C_IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["C_IDENTIFICACION"]);
                            if (resultado["I_NUM_RADIC"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["I_NUM_RADIC"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["D_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["D_CUOTA"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["D_SALDO_CAP"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["D_SALDO_CAP"]);
                            if (resultado["GARANTIA_COMUNITARIA"] != DBNull.Value) entidad.garantia_comunitaria = Convert.ToDouble(resultado["GARANTIA_COMUNITARIA"]);
                            if (resultado["PEND_UNA_CUOTA"] != DBNull.Value) entidad.pendite_cuota = Convert.ToInt64(resultado["PEND_UNA_CUOTA"]);
                            if (resultado["FECHA_CUOTA"] != DBNull.Value) entidad.Fecha_cuota = Convert.ToDateTime(resultado["FECHA_CUOTA"]);
                            if (resultado["DIRECCION_EMPRESA"] != DBNull.Value) entidad.direccion_empresa = Convert.ToString(resultado["DIRECCION_EMPRESA"]);
                            if (resultado["BARRIO_EMPRESA"] != DBNull.Value) entidad.barrio_empresa = Convert.ToString(resultado["BARRIO_EMPRESA"]);
                            if (resultado["TELEFONO_EMPRESA"] != DBNull.Value) entidad.telefono_empresa = Convert.ToString(resultado["TELEFONO_EMPRESA"]);
                            if (resultado["DIRECCION_NEGOCIO"] != DBNull.Value) entidad.direccion_negocio = Convert.ToString(resultado["DIRECCION_NEGOCIO"]);
                            if (resultado["BARRIO_NEGOCIO"] != DBNull.Value) entidad.barrio_negocio = Convert.ToString(resultado["BARRIO_NEGOCIO"]);
                            if (resultado["TELEFONO_NEGOCIO"] != DBNull.Value) entidad.telefono_negocio = Convert.ToString(resultado["TELEFONO_NEGOCIO"]);
                            if (resultado["DIRECCION_RESIDENCIA"] != DBNull.Value) entidad.direccion_residencia = Convert.ToString(resultado["DIRECCION_RESIDENCIA"]);
                            if (resultado["BARRIO_RESIDENCIA"] != DBNull.Value) entidad.barrio_residencia = Convert.ToString(resultado["BARRIO_RESIDENCIA"]);
                            if (resultado["TELEFONO_RESIDENCIA"] != DBNull.Value) entidad.telefono_residencia = Convert.ToString(resultado["TELEFONO_RESIDENCIA"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["CIUDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["I_COD_PROMOTOR"] != DBNull.Value) entidad.idpromotor = Convert.ToInt64(resultado["I_COD_PROMOTOR"]);
                            if (resultado["NOMBRES_PROMOTOR"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["NOMBRES_PROMOTOR"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] == DBNull.Value)
                            {
                                entidad.fecha_acuerdo = Convert.ToString(entidad.fecha_acuerdostring);
                            }
                            else
                            {
                                entidad.fecha_acuerdorepo = Convert.ToDateTime(resultado["FECHA_ACUERDO"].ToString());
                                entidad.fecha_acuerdo = entidad.fecha_acuerdorepo.ToShortDateString();
                            }
                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["USUARIO_DILIGENCIA"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIO_DILIGENCIA"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALOR_A_PAGAR"]);
                            if (resultado["diasmora_ucierre"] != DBNull.Value) entidad.dias_mora_u_cierre = Convert.ToInt64(resultado["diasmora_ucierre"]);


                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteMora", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método que permite consultar los créditos de un asesor
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarReporteMoraTodos(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  * FROM V_REPORTE_MORA A  WHERE A.COD_LINEA_CREDITO NOT IN(SELECT COD_LINEA_CREDITO FROM PARAMETROS_LINEA WHERE COD_PARAMETRO = 320)";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["I_CODIGO"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["I_CODIGO"]);
                            if (resultado["C_NOMBRES_CLIENTE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["C_NOMBRES_CLIENTE"]);
                            if (resultado["C_IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["C_IDENTIFICACION"]);
                            if (resultado["I_NUM_RADIC"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["I_NUM_RADIC"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["D_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["D_CUOTA"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["D_SALDO_CAP"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["D_SALDO_CAP"]);
                            if (resultado["GARANTIA_COMUNITARIA"] != DBNull.Value) entidad.garantia_comunitaria = Convert.ToDouble(resultado["GARANTIA_COMUNITARIA"]);
                            if (resultado["PEND_UNA_CUOTA"] != DBNull.Value) entidad.pendite_cuota = Convert.ToInt64(resultado["PEND_UNA_CUOTA"]);
                            if (resultado["FECHA_CUOTA"] != DBNull.Value) entidad.Fecha_cuota = Convert.ToDateTime(resultado["FECHA_CUOTA"]);
                            if (resultado["DIRECCION_EMPRESA"] != DBNull.Value) entidad.direccion_empresa = Convert.ToString(resultado["DIRECCION_EMPRESA"]);
                            if (resultado["BARRIO_EMPRESA"] != DBNull.Value) entidad.barrio_empresa = Convert.ToString(resultado["BARRIO_EMPRESA"]);
                            if (resultado["TELEFONO_EMPRESA"] != DBNull.Value) entidad.telefono_empresa = Convert.ToString(resultado["TELEFONO_EMPRESA"]);
                            if (resultado["DIRECCION_NEGOCIO"] != DBNull.Value) entidad.direccion_negocio = Convert.ToString(resultado["DIRECCION_NEGOCIO"]);
                            if (resultado["BARRIO_NEGOCIO"] != DBNull.Value) entidad.barrio_negocio = Convert.ToString(resultado["BARRIO_NEGOCIO"]);
                            if (resultado["TELEFONO_NEGOCIO"] != DBNull.Value) entidad.telefono_negocio = Convert.ToString(resultado["TELEFONO_NEGOCIO"]);
                            if (resultado["DIRECCION_RESIDENCIA"] != DBNull.Value) entidad.direccion_residencia = Convert.ToString(resultado["DIRECCION_RESIDENCIA"]);
                            if (resultado["BARRIO_RESIDENCIA"] != DBNull.Value) entidad.barrio_residencia = Convert.ToString(resultado["BARRIO_RESIDENCIA"]);
                            if (resultado["TELEFONO_RESIDENCIA"] != DBNull.Value) entidad.telefono_residencia = Convert.ToString(resultado["TELEFONO_RESIDENCIA"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["CIUDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["I_COD_PROMOTOR"] != DBNull.Value) entidad.idpromotor = Convert.ToInt64(resultado["I_COD_PROMOTOR"]);
                            if (resultado["NOMBRES_PROMOTOR"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["NOMBRES_PROMOTOR"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] == DBNull.Value) entidad.fecha_acuerdo = Convert.ToString(entidad.fecha_acuerdostring);

                            else
                            {
                                entidad.fecha_acuerdorepo = Convert.ToDateTime(resultado["FECHA_ACUERDO"].ToString());
                                entidad.fecha_acuerdo = entidad.fecha_acuerdorepo.ToShortDateString();
                            }

                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["USUARIO_DILIGENCIA"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIO_DILIGENCIA"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALOR_A_PAGAR"]);
                            if (resultado["diasmora_ucierre"] != DBNull.Value) entidad.dias_mora_u_cierre = Convert.ToInt64(resultado["diasmora_ucierre"]);




                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteMora", ex);
                        return null;
                    }
                }
            }
        }
        public List<Reporte> ListarReportecobranza(Usuario pUsuario, Int64 codigo, string rango, string asesores, string oficina)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();
            string sql = "";
            string ofc = "";
            string eje = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (oficina == "todos")
                        {
                            ofc = "";
                        }
                        else
                        {
                            ofc = "and cod_oficina = " + oficina + " ";
                        }
                        if (asesores == "todos")
                        {
                            eje = "";
                        }
                        else
                        {
                            eje = "and COD_ASESOR = " + asesores + " ";
                        }
                        if (rango == "1")
                            sql = "SELECT * FROM V_REPORTE_CARTERA where dias_mora BETWEEN 0 AND 30 " + ofc + eje;
                        if (rango == "2")
                            sql = "SELECT * FROM V_REPORTE_CARTERA where dias_mora BETWEEN 31 AND 60 " + ofc + eje;
                        if (rango == "3")
                            sql = "SELECT * FROM V_REPORTE_CARTERA where dias_mora BETWEEN 61 AND 90 " + ofc + eje;
                        if (rango == "4")
                            sql = "SELECT * FROM V_REPORTE_CARTERA where dias_mora BETWEEN 91 AND 120 " + ofc + eje;
                        if (rango == "5")
                            sql = "SELECT * FROM V_REPORTE_CARTERA where dias_mora BETWEEN 121 AND 180 " + ofc + eje;
                        if (rango == "6")
                            sql = "SELECT * FROM V_REPORTE_CARTERA where dias_mora > 180" + ofc + eje;
                        if (rango.Contains("AND"))
                            sql = "SELECT * FROM V_REPORTE_CARTERA where dias_mora BETWEEN " + rango + " " + ofc + eje;

                        sql += " and numero_radicacion not in (select ca.numero_radicacion from cobros_credito ca where ca.numero_radicacion = V_REPORTE_CARTERA.numero_Radicacion)";

                        sql += "Order by NUMERO_RADICACION asc";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["EMAIL"] != DBNull.Value) entidad.email = Convert.ToString(resultado["EMAIL"]);
                            if (resultado["TELEFONOEMPRESA"] != DBNull.Value) entidad.telefono_oficina = Convert.ToString(resultado["TELEFONOEMPRESA"]);
                            if (resultado["DIRECCIONEMPRESA"] != DBNull.Value) entidad.direccion_oficina = Convert.ToString(resultado["DIRECCIONEMPRESA"]);
                            if (resultado["DIRCORRESPONDENCIA"] != DBNull.Value) entidad.direccion_correspondencia = Convert.ToString(resultado["DIRCORRESPONDENCIA"]);
                            if (resultado["TELCORRESPONDENCIA"] != DBNull.Value) entidad.telefono_correspondencia = Convert.ToString(resultado["TELCORRESPONDENCIA"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.codigo_oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valor_pagar = Convert.ToString(resultado["VALOR_A_PAGAR"]);
                            if (resultado["COD_CODEUDOR"] != DBNull.Value) entidad.cod_codeudor = Convert.ToString(resultado["COD_CODEUDOR"]);
                            if (resultado["IDENTIFICACION_CODEUDOR"] != DBNull.Value) entidad.identificacion_codeudor = Convert.ToString(resultado["IDENTIFICACION_CODEUDOR"]);
                            if (resultado["NOMBRE_CODEUDOR"] != DBNull.Value) entidad.nombre_codeudor = Convert.ToString(resultado["NOMBRE_CODEUDOR"]);
                            if (resultado["DIRECCION_CODEUDOR"] != DBNull.Value) entidad.direcion_codeudor = Convert.ToString(resultado["DIRECCION_CODEUDOR"]);
                            if (resultado["TELEFONO_CODEUDOR"] != DBNull.Value) entidad.telefono_codeudor = Convert.ToString(resultado["TELEFONO_CODEUDOR"]);
                            if (resultado["TELEFONOEMPRESA_CODEUDOR"] != DBNull.Value) entidad.telefono_empresa_codeudor = Convert.ToString(resultado["TELEFONOEMPRESA_CODEUDOR"]);
                            if (resultado["DIRCORRESPONDENCIA_CODEUDOR"] != DBNull.Value) entidad.direcion_corespondecia_codeudor = Convert.ToString(resultado["DIRCORRESPONDENCIA_CODEUDOR"]);
                            if (resultado["TELCORRESPONDENCIA_CODEUDOR"] != DBNull.Value) entidad.telefono_correspondecia_codeudor = Convert.ToString(resultado["TELCORRESPONDENCIA_CODEUDOR"]);
                            if (resultado["NOM_ASESOR"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["NOM_ASESOR"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["NOM_ESTADO"]);

                            if (resultado["EMPRESA_DEUDOR"] != DBNull.Value) entidad.empresa_recaudo_code = Convert.ToString(resultado["EMPRESA_DEUDOR"]);
                            if (resultado["EMPRESA_CODEUDOR"] != DBNull.Value) entidad.empresa_recaudo = Convert.ToString(resultado["EMPRESA_CODEUDOR"]);
                            if (resultado["EMAIL_CODEUDOR"] != DBNull.Value) entidad.email_codeudor = Convert.ToString(resultado["EMAIL_CODEUDOR"]);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteMora", ex);
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// Método que permite consultar todos los créditos
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarReporteMoraofici(Usuario pUsuario, Int64 oficina)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  * FROM V_REPORTE_MORA A  WHERE A.OFICINA= " + oficina + " ORDER BY A.I_COD_PROMOTOR ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["I_CODIGO"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["I_CODIGO"]);
                            if (resultado["C_NOMBRES_CLIENTE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["C_NOMBRES_CLIENTE"]);
                            if (resultado["C_IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["C_IDENTIFICACION"]);
                            if (resultado["I_NUM_RADIC"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["I_NUM_RADIC"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["D_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["D_CUOTA"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["D_SALDO_CAP"] != DBNull.Value) entidad.saldo_capital = Convert.ToInt64(resultado["D_SALDO_CAP"]);
                            if (resultado["GARANTIA_COMUNITARIA"] != DBNull.Value) entidad.garantia_comunitaria = Convert.ToDouble(resultado["GARANTIA_COMUNITARIA"]);
                            if (resultado["PEND_UNA_CUOTA"] != DBNull.Value) entidad.pendite_cuota = Convert.ToInt64(resultado["PEND_UNA_CUOTA"]);
                            if (resultado["FECHA_CUOTA"] != DBNull.Value) entidad.Fecha_cuota = Convert.ToDateTime(resultado["FECHA_CUOTA"]);
                            if (resultado["DIRECCION_EMPRESA"] != DBNull.Value) entidad.direccion_empresa = Convert.ToString(resultado["DIRECCION_EMPRESA"]);
                            if (resultado["BARRIO_EMPRESA"] != DBNull.Value) entidad.barrio_empresa = Convert.ToString(resultado["BARRIO_EMPRESA"]);
                            if (resultado["TELEFONO_EMPRESA"] != DBNull.Value) entidad.telefono_empresa = Convert.ToString(resultado["TELEFONO_EMPRESA"]);
                            if (resultado["DIRECCION_NEGOCIO"] != DBNull.Value) entidad.direccion_negocio = Convert.ToString(resultado["DIRECCION_NEGOCIO"]);
                            if (resultado["BARRIO_NEGOCIO"] != DBNull.Value) entidad.barrio_negocio = Convert.ToString(resultado["BARRIO_NEGOCIO"]);
                            if (resultado["TELEFONO_NEGOCIO"] != DBNull.Value) entidad.telefono_negocio = Convert.ToString(resultado["TELEFONO_NEGOCIO"]);
                            if (resultado["DIRECCION_RESIDENCIA"] != DBNull.Value) entidad.direccion_residencia = Convert.ToString(resultado["DIRECCION_RESIDENCIA"]);
                            if (resultado["BARRIO_RESIDENCIA"] != DBNull.Value) entidad.barrio_residencia = Convert.ToString(resultado["BARRIO_RESIDENCIA"]);
                            if (resultado["TELEFONO_RESIDENCIA"] != DBNull.Value) entidad.telefono_residencia = Convert.ToString(resultado["TELEFONO_RESIDENCIA"]);
                            if (resultado["CELULAR"] != DBNull.Value) entidad.celular = Convert.ToString(resultado["CELULAR"]);
                            if (resultado["CIUDAD"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["CIUDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["I_COD_PROMOTOR"] != DBNull.Value) entidad.idpromotor = Convert.ToInt64(resultado["I_COD_PROMOTOR"]);
                            if (resultado["NOMBRES_PROMOTOR"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["NOMBRES_PROMOTOR"]);
                            if (resultado["VALOR_ACUERDO"] != DBNull.Value) entidad.valor_acuerdo = Convert.ToInt64(resultado["VALOR_ACUERDO"]);
                            if (resultado["FECHA_ACUERDO"] == DBNull.Value) entidad.fecha_acuerdo = Convert.ToString(entidad.fecha_acuerdostring);

                            else
                            {
                                entidad.fecha_acuerdorepo = Convert.ToDateTime(resultado["FECHA_ACUERDO"].ToString());
                                entidad.fecha_acuerdo = entidad.fecha_acuerdorepo.ToShortDateString();
                            }

                            if (resultado["RESPUESTA"] != DBNull.Value) entidad.respuesta = Convert.ToString(resultado["RESPUESTA"]);
                            if (resultado["USUARIO_DILIGENCIA"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIO_DILIGENCIA"]);
                            if (resultado["OBSERVACION"] != DBNull.Value) entidad.observacion = Convert.ToString(resultado["OBSERVACION"]);
                            if (resultado["VALOR_A_PAGAR"] == DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALOR_A_PAGAR"]);
                            if (resultado["diasmora_ucierre"] != DBNull.Value) entidad.dias_mora_u_cierre = Convert.ToInt64(resultado["diasmora_ucierre"]);



                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteMora", ex);
                        return null;
                    }
                }
            }
        }


        public List<Reporte> ListarReportepoliza(Usuario pUsuario, Int64 codigo, DateTime fechaini, DateTime fechafinal)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT DISTINCT  A.IDENTIFICACION, A.NOMBRE_AFILIADO, A.PORCENTAJE, A.VALOR_PRIMA_MENSUAL, A.NUMERO_RADICACION, A.OFICINA," +
                                     "A.TIPO_PLAN, A.MONTO_PAGO, A.CEDULA_ASESOR, A.NOMBRES_ASESOR, fecha_pago, FECHA_FIN_VIG" +
                                     " FROM V_REPORTE_ASEGURADORA A WHERE FECHA_PAGO between to_date('" + fechaini.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') And  to_date('" + fechafinal.ToString("dd/MM/yyyy") + "' ,'dd/MM/yyyy') And A.OFICINA = " + codigo + " ORDER BY 11 ASC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE_AFILIADO"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE_AFILIADO"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]);
                            if (resultado["VALOR_PRIMA_MENSUAL"] != DBNull.Value) entidad.valor_prima = Convert.ToInt64(resultado["VALOR_PRIMA_MENSUAL"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["TIPO_PLAN"] != DBNull.Value) entidad.tipoplan = Convert.ToString(resultado["TIPO_PLAN"]);
                            if (resultado["MONTO_PAGO"] != DBNull.Value) entidad.monto_pago = Convert.ToInt64(resultado["MONTO_PAGO"]);
                            if (resultado["CEDULA_ASESOR"] != DBNull.Value) entidad.cedula_asesor = Convert.ToString(resultado["CEDULA_ASESOR"]);
                            if (resultado["NOMBRES_ASESOR"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["NOMBRES_ASESOR"]);
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.fehca_pago = Convert.ToDateTime(resultado["FECHA_PAGO"]).ToString("dd/MM/yyyy");
                            if (resultado["FECHA_FIN_VIG"] != DBNull.Value) entidad.fecha_vigencia = Convert.ToDateTime(resultado["FECHA_FIN_VIG"]).ToString("dd/MM/yyyy");


                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteMora", ex);
                        return null;
                    }
                }
            }
        }
        public List<Reporte> Listarcolocacioneje(Usuario pUsuario, Int64 codigo, DateTime fechaini, DateTime fechafinal)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT DISTINCT  A.CODIGO, A.NOMBRE, A.OFICINA, SUM(A.MONTO), COUNT(*) AS NUMERO" +
                                        " FROM V_COLOCACION_EJECUTIVO A WHERE  FECHA_DESEMBOLSO between to_date('" + fechaini.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') And  to_date('" + fechafinal.ToString("dd/MM/yyyy") + "' ,'dd/MM/yyyy') And A.OFICINA = " + codigo + " GROUP BY  A.CODIGO, A.NOMBRE, A.OFICINA ORDER BY 4 ASC, 2 ASC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["SUM(A.MONTO)"] != DBNull.Value) entidad.monto_pago = Convert.ToInt64(resultado["SUM(A.MONTO)"]);
                            if (resultado["NUMERO"] != DBNull.Value) entidad.cuenta = Convert.ToInt64(resultado["NUMERO"]);


                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteMora", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> ListarReportecierreoficina(Usuario pUsuario, Int64 codigo, DateTime fechaini)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "  SELECT * FROM vs_TOTAL_EJEC_A_CIERRE  WHERE FECHA_CIERRE = to_date (' " + fechaini.ToShortDateString() + "', 'dd/MM/yyyy') And OFICINA = " + codigo + " order by codigo ASC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"].ToString());
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fechacierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["NO_CRED"] != DBNull.Value) entidad.numero_credito = Convert.ToString(resultado["NO_CRED"]);
                            if (resultado["SALDO_AL_CIERRE"] != DBNull.Value) entidad.saldo_cierre = Convert.ToString(resultado["SALDO_AL_CIERRE"]);
                            if (resultado["NO_COLOCACION_MES"] != DBNull.Value) entidad.numero_colocacion_mes = Convert.ToString(resultado["NO_COLOCACION_MES"]);
                            if (resultado["MONTO_COLOCACION_MES"] != DBNull.Value) entidad.monto_colocacion_mes = Convert.ToString(resultado["MONTO_COLOCACION_MES"]);
                            if (resultado["NO_MORA_TOTAL"] != DBNull.Value) entidad.total_mora = Convert.ToString(resultado["NO_MORA_TOTAL"]);
                            if (resultado["SALDO_MORA_TOTAL"] != DBNull.Value) entidad.saldo_mora = Convert.ToString(resultado["SALDO_MORA_TOTAL"]);
                            if (resultado["NO_MORA_MENOR_30"] != DBNull.Value) entidad.mora_menor_30 = Convert.ToString(resultado["NO_MORA_MENOR_30"]);
                            if (resultado["MONTO_MORA_MENOR_30"] != DBNull.Value) entidad.monto_menor_30 = Convert.ToString(resultado["MONTO_MORA_MENOR_30"]);
                            if (resultado["NO_MORA_MAYOR_30"] != DBNull.Value) entidad.mora_mayor_30 = Convert.ToString(resultado["NO_MORA_MAYOR_30"]);
                            if (resultado["MONTO_MORA_MAYOR_30"] != DBNull.Value) entidad.monto_mayor_30 = Convert.ToString(resultado["MONTO_MORA_MAYOR_30"]);
                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteMora", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> ListarReportecierreoficinatodos(Usuario pUsuario, DateTime fechaini)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "  SELECT * FROM vs_TOTAL_EJEC_A_CIERRE WHERE FECHA_CIERRE = to_date (' " + fechaini.ToShortDateString() + "', 'dd/MM/yyyy') ORDER BY OFICINA ASC";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["CODIGO"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"].ToString());
                            if (resultado["FECHA_CIERRE"] != DBNull.Value) entidad.fechacierre = Convert.ToDateTime(resultado["FECHA_CIERRE"]);
                            if (resultado["NO_CRED"] != DBNull.Value) entidad.numero_credito = Convert.ToString(resultado["NO_CRED"]);
                            if (resultado["SALDO_AL_CIERRE"] != DBNull.Value) entidad.saldo_cierre = Convert.ToString(resultado["SALDO_AL_CIERRE"]);
                            if (resultado["NO_COLOCACION_MES"] != DBNull.Value) entidad.numero_colocacion_mes = Convert.ToString(resultado["NO_COLOCACION_MES"]);
                            if (resultado["MONTO_COLOCACION_MES"] != DBNull.Value) entidad.monto_colocacion_mes = Convert.ToString(resultado["MONTO_COLOCACION_MES"]);
                            if (resultado["NO_MORA_TOTAL"] != DBNull.Value) entidad.total_mora = Convert.ToString(resultado["NO_MORA_TOTAL"]);
                            if (resultado["SALDO_MORA_TOTAL"] != DBNull.Value) entidad.saldo_mora = Convert.ToString(resultado["SALDO_MORA_TOTAL"]);
                            if (resultado["NO_MORA_MENOR_30"] != DBNull.Value) entidad.mora_menor_30 = Convert.ToString(resultado["NO_MORA_MENOR_30"]);
                            if (resultado["MONTO_MORA_MENOR_30"] != DBNull.Value) entidad.monto_menor_30 = Convert.ToString(resultado["MONTO_MORA_MENOR_30"]);
                            if (resultado["NO_MORA_MAYOR_30"] != DBNull.Value) entidad.mora_mayor_30 = Convert.ToString(resultado["NO_MORA_MAYOR_30"]);
                            if (resultado["MONTO_MORA_MAYOR_30"] != DBNull.Value) entidad.monto_mayor_30 = Convert.ToString(resultado["MONTO_MORA_MAYOR_30"]);
                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteMora", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método que permite consultar los créditos de un asesor
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarReporteGestionCobranzas(Usuario pUsuario, Int64 codigo, DateTime fechaini, DateTime fechafinal)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * from vs_recuperacion_cobro  where  FECHA_diligencia between to_date('" + fechaini.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') And to_date('" + fechafinal.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') AND  COD_LINEA_CREDITO NOT IN(SELECT COD_LINEA_CREDITO FROM PARAMETROS_LINEA WHERE COD_PARAMETRO = 320) ";
                        if (codigo != 0)
                        {
                            sql += "and CODIGO = " + codigo;
                        }
                        sql = sql + " order BY NO_GESTION_X_COBRADOR asc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["CODIGO"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["CODIGO"]);
                            if (resultado["COBRADOR"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["COBRADOR"]);
                            if (resultado["CREDITO"] != DBNull.Value) entidad.numero_credito = Convert.ToString(resultado["CREDITO"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.codigo = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombrecliente = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.fecha_pago = Convert.ToDateTime(resultado["FECHA_PAGO"]);
                            if (resultado["VALOR_PAGO"] != DBNull.Value) entidad.valor_pago = Convert.ToInt64(resultado["VALOR_PAGO"]);
                            if (resultado["NO_GESTION_X_COBRADOR"] != DBNull.Value) entidad.cantidad = Convert.ToInt64(resultado["NO_GESTION_X_COBRADOR"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["ACUERDO"] != DBNull.Value) entidad.Acuerdo = Convert.ToInt32(resultado["ACUERDO"]);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarReporteGestionCobranzas", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Método que permite consultar los créditos de un asesor
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarRepCierreDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "SELECT  * FROM vs_detalle_historica_cartera WHERE fecha_cierre = to_date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = "SELECT  * FROM vs_detalle_historica_cartera WHERE fecha_cierre = '" + fecha.ToString(conf.ObtenerFormatoFecha()) + "')";
                        if (filtro.Trim() != "")
                            sql += filtro;
                        sql += " ORDER BY oficina, numero_radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.Fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["SALDO_AL_CIERRE"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_AL_CIERRE"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.Fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.Fecha_ult_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.codigo_asesor = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["NOMBRE_ASESOR"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["NOMBRE_ASESOR"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["COD_CATEGORIA_CLI"] != DBNull.Value) entidad.cod_categoria_cli = Convert.ToString(resultado["COD_CATEGORIA_CLI"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["MONTO_DESEMBOLSADO"] != DBNull.Value) entidad.monto_desembolsado = Convert.ToInt64(resultado["MONTO_DESEMBOLSADO"]);
                            if (resultado["EMPRESA_RECAUDO"] != DBNull.Value) entidad.empresa_recaudo = Convert.ToString(resultado["EMPRESA_RECAUDO"]);
                            if (resultado["NOM_TIPO_GARANTIA"] != DBNull.Value) entidad.nom_tipo_garantia = Convert.ToString(resultado["NOM_TIPO_GARANTIA"]);
                            if (resultado["VALOR_GARANTIA"] != DBNull.Value) entidad.valor_garantia = Convert.ToDecimal(resultado["VALOR_GARANTIA"]);
                            if (resultado["VALOR_APORTES"] != DBNull.Value) entidad.aporte_resta = Convert.ToDecimal(resultado["VALOR_APORTES"]);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarRepCierreDetallado", ex);
                        return null;
                    }
                }
            }
        }


        public List<Reporte> ListarRepCausacionDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        // Determinar fecha de causación del mes anterior
                        DateTime? fechaanterior = null;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            cmdTransaccionFactory.CommandText = "Select Max(fecha) As fecha_anterior From cierea Where tipo = 'U' And estado = 'D' and Trunc(fecha) < to_date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            cmdTransaccionFactory.CommandText = "Select Max(fecha) As fecha_anterior From cierea Where tipo = 'U' And estado = 'D' and fecha) < '" + fecha.ToString(conf.ObtenerFormatoFecha()) + "' )";
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["FECHA_ANTERIOR"] != DBNull.Value) fechaanterior = Convert.ToDateTime(resultado["FECHA_ANTERIOR"]);
                        }

                        // Determinar datos de la causación
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "SELECT  * FROM v_causacion_cartera WHERE fecha_historico = to_date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = "SELECT  * FROM v_causacion_cartera WHERE fecha_historico = '" + fecha.ToString(conf.ObtenerFormatoFecha()) + "')";
                        if (filtro.Trim() != "")
                            sql += filtro;
                        sql += " ORDER BY cod_oficina, numero_radicacion";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.codigo_oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["NOMBRE_ATRIBUTO"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["NOMBRE_ATRIBUTO"]);
                            if (resultado["VALOR_CAUSADO"] != DBNull.Value) entidad.valor_causado = Convert.ToDecimal(resultado["VALOR_CAUSADO"]);
                            if (resultado["VALOR_ORDEN"] != DBNull.Value) entidad.valor_orden = Convert.ToDecimal(resultado["VALOR_ORDEN"]);
                            if (resultado["SALDO_CAUSADO"] != DBNull.Value) entidad.saldo_causado = Convert.ToDecimal(resultado["SALDO_CAUSADO"]);
                            if (resultado["SALDO_ORDEN"] != DBNull.Value) entidad.saldo_orden = Convert.ToDecimal(resultado["SALDO_ORDEN"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["COD_CATEGORIA_CLI"] != DBNull.Value) entidad.cod_categoria_cli = Convert.ToString(resultado["COD_CATEGORIA_CLI"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["DIAS_CAUSADOS"] != DBNull.Value) entidad.dias_causados = Convert.ToInt32(resultado["DIAS_CAUSADOS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToString(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["DIAS_AMORTIZA"] != DBNull.Value) entidad.dias_amortiza = Convert.ToInt64(resultado["DIAS_AMORTIZA"]);
                            if (resultado["MOVIMIENTOS"] != DBNull.Value) entidad.valor_movimiento = Convert.ToDecimal(resultado["MOVIMIENTOS"]);
                            // Determinando valor causado del período anterior
                            if (fechaanterior != null)
                                entidad.saldo_causado_ant = SaldoCausado(fechaanterior, entidad.NumRadicacion, entidad.cod_atr, connection, pUsuario);
                            // Esto se colocó para FONSODI porque daba error al generar el reporte. 5-Oct-2017. FerOrt.
                            if (entidad.dias_causados == 0)
                                entidad.dias_causados = DiasCausados(fecha, entidad.NumRadicacion, entidad.cod_atr, connection, pUsuario);
                            //Calcular valor contabilizado
                            entidad.saldo_contable = entidad.saldo_causado - entidad.saldo_causado_ant + entidad.valor_movimiento;

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarRepCierreDetallado", ex);
                        return null;
                    }
                }
            }
        }

        public decimal SaldoCausado(DateTime? pfecha, Int64 pnumero_radicacion, Int32 pcod_atr, DbConnection connection, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            decimal saldo_causado = 0;

            using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
            {
                try
                {
                    Configuracion conf = new Configuracion();
                    if (connection.State == ConnectionState.Closed) connection.Open();
                    string sql = "";
                    if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        sql = "SELECT saldo_causado FROM causacion WHERE fecha_corte = to_date('" + Convert.ToDateTime(pfecha).ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                    else
                        sql = "SELECT saldo_causado FROM causacion WHERE fecha_corte = '" + Convert.ToDateTime(pfecha).ToString(conf.ObtenerFormatoFecha()) + "')";
                    sql += " AND numero_radicacion = " + pnumero_radicacion + " AND cod_atr = " + pcod_atr;
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    if (resultado.Read())
                    {
                        if (resultado["SALDO_CAUSADO"] != DBNull.Value) saldo_causado = Convert.ToDecimal(resultado["SALDO_CAUSADO"]);
                    }
                    return saldo_causado;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("ReporteData", "SaldoCausado", ex);
                    return saldo_causado;
                }
            }
        }

        public List<Reporte> ListarRepProvisionDetallado(DateTime fecha, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"SELECT * FROM V_PROVISION_DETALLE_CARTERA                                 
                                    WHERE fecha_historico = to_date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = @"SELECT * FROM V_PROVISION_DETALLE_CARTERA
                                    WHERE fecha_historico = '" + fecha.ToString(conf.ObtenerFormatoFecha()) + "')";

                        if (filtro.Trim() != "")
                            sql += filtro;
                        sql += " ORDER BY cod_oficina, numero_radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.codigo_oficina = Convert.ToString(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE_OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["NOMBRE_OFICINA"]);
                            if (resultado["COD_ATR"] != DBNull.Value) entidad.cod_atr = Convert.ToInt32(resultado["COD_ATR"]);
                            if (resultado["NOMBRE_ATRIBUTO"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["NOMBRE_ATRIBUTO"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["PORC_PROVISION"] != DBNull.Value) entidad.porc_provision = Convert.ToDecimal(resultado["PORC_PROVISION"]);
                            if (resultado["VALOR_PROVISION"] != DBNull.Value) entidad.valor_provision = Convert.ToDecimal(resultado["VALOR_PROVISION"]);
                            if (resultado["APORTE_RESTA"] != DBNull.Value) entidad.aporte_resta = Convert.ToDecimal(resultado["APORTE_RESTA"]);
                            if (resultado["DIFERENCIA_PROVISION"] != DBNull.Value) entidad.diferencia_provision = Convert.ToDecimal(resultado["DIFERENCIA_PROVISION"]);
                            if (resultado["DIFERENCIA_ACTUAL"] != DBNull.Value) entidad.diferencia_actual = Convert.ToDecimal(resultado["DIFERENCIA_ACTUAL"]);
                            if (resultado["DIFERENCIA_ANTERIOR"] != DBNull.Value) entidad.diferencia_anterior = Convert.ToDecimal(resultado["DIFERENCIA_ANTERIOR"]);
                            if (resultado["BASE_PROVISION"] != DBNull.Value) entidad.base_provision = Convert.ToDecimal(resultado["BASE_PROVISION"]);
                            if (resultado["cod_categoria"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["cod_categoria"]);
                            if (resultado["cod_categoria_cli"] != DBNull.Value) entidad.cod_categoria_cli = Convert.ToString(resultado["cod_categoria_cli"]);
                            if (resultado["cod_clasifica"] != DBNull.Value) entidad.cod_clasificacion = Convert.ToString(resultado["cod_clasifica"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion_clasificacion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["VALOR_GARANTIA"] != DBNull.Value) entidad.valor_garantia = Convert.ToDecimal(resultado["VALOR_GARANTIA"]);
                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarRepCierreDetallado", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> ListarRepCierreDiarios(DateTime fecha, string filtro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "SELECT informe_creditos.*,empresarecaudo(credito.cod_empresa) as EMPRESA_RECAUDO,(CASE credito.forma_pago WHEN 'C' THEN 'Caja' WHEN 'N' THEN 'Nomina' WHEN '1' THEN 'Caja' WHEN '2' THEN 'Nomina' ELSE credito.forma_pago END) forma_pago FROM informe_creditos INNER JOIN CREDITO ON CREDITO.NUMERO_RADICACION = informe_creditos.NUMERO_RADICACION WHERE fecha_corte = to_date('" + fecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";
                        else
                            sql = "SELECT  informe_creditos.*,empresarecaudo(credito.cod_empresa) as EMPRESA_RECAUDO,(CASE credito.forma_pago WHEN 'C' THEN 'Caja' WHEN 'N' THEN 'Nomina' WHEN '1' THEN 'Caja' WHEN '2' THEN 'Nomina' ELSE credito.forma_pago END) forma_pago FROM informe_creditos INNER JOIN CREDITO ON CREDITO.NUMERO_RADICACION = informe_creditos.NUMERO_RADICACION  WHERE fecha_corte = '" + fecha.ToString(conf.ObtenerFormatoFecha()) + "')";
                        if (filtro.Trim() != "")
                            sql += filtro;
                        sql += " ORDER BY informe_creditos.cod_oficina, informe_creditos.numero_radicacion";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.Fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE_LINEA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.Fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.Fecha_ult_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALOR_A_PAGAR"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.codigo_asesor = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["NOMBRE_ASESOR"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["NOMBRE_ASESOR"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["COD_CATEGORIA"] != DBNull.Value) entidad.cod_categoria = Convert.ToString(resultado["COD_CATEGORIA"]);
                            if (resultado["COD_CATEGORIA_CLI"] != DBNull.Value) entidad.cod_categoria_cli = Convert.ToString(resultado["COD_CATEGORIA_CLI"]);
                            if (resultado["TASA_INTERES"] != DBNull.Value) entidad.tasa_interes = Convert.ToDecimal(resultado["TASA_INTERES"]);
                            if (resultado["CIUCORRESPONDENCIA"] != DBNull.Value) entidad.ciudad = Convert.ToString(resultado["CIUCORRESPONDENCIA"]);
                            if (resultado["DIRECCION"] != DBNull.Value) entidad.direccion = Convert.ToString(resultado["DIRECCION"]);
                            if (resultado["TELEFONO"] != DBNull.Value) entidad.telefono = Convert.ToString(resultado["TELEFONO"]);
                            if (resultado["EMPRESA_RECAUDO"] != DBNull.Value) entidad.empresa_recaudo = Convert.ToString(resultado["EMPRESA_RECAUDO"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);

                            //Saco los valores de que conforman el valor a pagar del credito. 
                            DetallePagoData DADetPago = new DetallePagoData();
                            List<Atributo> LstDetalle = new List<Atributo>();
                            LstDetalle = DADetPago.ListarDetallePago(DateTime.Now, entidad.NumRadicacion, pUsuario);
                            entidad.ValorInteres = LstDetalle.FirstOrDefault(x => x.cod_atr == 2) == null ? 0 : LstDetalle.FirstOrDefault(x => x.cod_atr == 2).saldo_atributo;
                            entidad.ValorCapital = LstDetalle.FirstOrDefault(x => x.cod_atr == 1) == null ? 0 : LstDetalle.FirstOrDefault(x => x.cod_atr == 1).saldo_atributo;
                            entidad.ValorOtros = LstDetalle.FirstOrDefault(x => x.cod_atr != 1 && x.cod_atr != 2 && x.cod_atr != 3) == null ? 0 : LstDetalle.FirstOrDefault(x => x.cod_atr != 1 && x.cod_atr != 2 && x.cod_atr != 3).saldo_atributo;
                            entidad.ValorMora = LstDetalle.FirstOrDefault(x => x.cod_atr == 3) == null ? 0 : LstDetalle.FirstOrDefault(x => x.cod_atr == 3).saldo_atributo;


                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarRepCierreDetallado", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> ListarFechaCorte(Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = "";
                        sql = "SELECT Distinct Fecha_Corte FROM informe_creditos ORDER BY 1 desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["FECHA_CORTE"] != DBNull.Value) entidad.fechacierre = Convert.ToDateTime(resultado["FECHA_CORTE"]);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarFechaCorte", ex);
                        return null;
                    }
                }
            }
        }


        /// <summary>
        /// Método que permite consultar los créditos de un asesor
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarRepCierreDetAsesor(DateTime fecha, Usuario pUsuario, Int64 codigo)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstPrograma = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  * FROM vs_detalle_historica_cartera  where FECHA_CIERRE= to_date('" + fecha.ToString("dd/MM/yyyy") + "','dd/MM/yyyy') And COD_ASESOR_COM = " + codigo;
                        sql = sql + " order BY DIAS_MORA asc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.icodigo = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.Apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.pagare = Convert.ToString(resultado["PAGARE"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.Fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["MONTO"] != DBNull.Value) entidad.monto_aprobado = Convert.ToInt64(resultado["MONTO"]);
                            if (resultado["SALDO_AL_CIERRE"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_AL_CIERRE"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.Fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.dias_mora = Convert.ToString(resultado["DIAS_MORA"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.Fecha_ult_pago = Convert.ToDateTime(resultado["BARRIO_OFICINA"]);
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.codigo_asesor = Convert.ToInt64(resultado["COD_ASESOR_COM"]);
                            if (resultado["NOMBRE_ASESOR"] != DBNull.Value) entidad.nombre_asesor = Convert.ToString(resultado["NOMBRE_ASESOR"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.cuotas_pagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["PERIODICIDAD"] != DBNull.Value) entidad.periodicidad = Convert.ToString(resultado["PERIODICIDAD"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["oficina"]);

                            lstPrograma.Add(entidad);
                        }
                        return lstPrograma;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "ListarRepCierreDetallado", ex);
                        return null;
                    }
                }
            }
        }

        public List<Reporte> Consultarusuariopagoespecial(Int64 pId, Usuario pUsuario, string filtro)
        {
            DbDataReader resultado;
            List<Reporte> lstentidad = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    Configuracion conf = new Configuracion();
                    try
                    {
                        string sql = @"Select credito.*, persona.identificacion, persona.primer_nombre || ' ' || persona.primer_apellido as nombress, oficina.nombre, lineascredito.nombre as nombres From credito Inner Join lineascredito On lineascredito.cod_linea_credito = credito.cod_linea_credito Inner Join oficina On oficina.cod_oficina = credito.cod_oficina Inner join persona on persona.cod_persona = credito.cod_deudor Where credito.pago_especial = 1 " + filtro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["NOMBRESS"] != DBNull.Value) entidad.nombress = Convert.ToString(resultado["NOMBRESS"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.Nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]).ToString(conf.ObtenerFormatoFecha());
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.plazo = Convert.ToInt64(resultado["NUMERO_CUOTAS"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            //if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            lstentidad.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstentidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("UsuarioAtribucionesData", "Consultarusuariopagoespecial", ex);
                        return null;
                    }
                }
            }
        }

        public int DiasCausados(DateTime pFechaHistorico, Int64 pNumeroRadicacion, int pCodAtr, DbConnection connection, Usuario pUsuario)
        {
            DbDataReader resultado;
            int dias_mes_cau = 0;

            using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
            {
                Configuracion conf = new Configuracion();
                try
                {
                    string sql = "";

                    if (dbConnectionFactory.TipoConexion() == "ORACLE")
                        sql = @"Select Sum(x.dias_mes_cau) From det_causacion x Where x.fecha_corte = To_Date('" + pFechaHistorico.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') And x.numero_radicacion = " + pNumeroRadicacion + " And x.cod_atr = " + pCodAtr + " And x.valor_base != 0";
                    else
                        sql = @"Select Sum(x.dias_mes_cau) From det_causacion x Where x.fecha_corte = '" + pFechaHistorico.ToString(conf.ObtenerFormatoFecha()) + "' And x.numero_radicacion = " + pNumeroRadicacion + " And x.cod_atr = " + pCodAtr + " And x.valor_base != 0";


                    if (connection.State == ConnectionState.Closed) connection.Open();
                    cmdTransaccionFactory.Connection = connection;
                    cmdTransaccionFactory.CommandType = CommandType.Text;
                    cmdTransaccionFactory.CommandText = sql;
                    resultado = cmdTransaccionFactory.ExecuteReader();

                    if (resultado.Read())
                    {
                        if (resultado["DIAS_MES_CAU"] != DBNull.Value) dias_mes_cau = Convert.ToInt32(resultado["DIAS_MES_CAU"]);
                    }

                    return dias_mes_cau;
                }
                catch
                {
                    return dias_mes_cau;
                }
            }

        }


        public List<Reporte> CuadreSaldos(DateTime pFecha, int pTipo, string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Reporte> lstReporte = new List<Reporte>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.Value = pFecha;
                        PFECHA.Direction = ParameterDirection.Input;
                        PFECHA.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHA);

                        DbParameter PTIPO = cmdTransaccionFactory.CreateParameter();
                        PTIPO.ParameterName = "PTIPO";
                        if (EsPlanCuentasNIIF(pUsuario))
                            PTIPO.Value = "1";
                        else
                            PTIPO.Value = "0";
                        PTIPO.Direction = ParameterDirection.Input;
                        PTIPO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PTIPO);

                        DbParameter PAJUSTAR = cmdTransaccionFactory.CreateParameter();
                        PAJUSTAR.ParameterName = "PAJUSTAR";
                        PAJUSTAR.Value = "0";
                        PAJUSTAR.Direction = ParameterDirection.Input;
                        PAJUSTAR.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PAJUSTAR);

                        DbParameter PCOD_USUARIO = cmdTransaccionFactory.CreateParameter();
                        PCOD_USUARIO.ParameterName = "PCOD_USUARIO";
                        PCOD_USUARIO.Value = pUsuario.codusuario;
                        PCOD_USUARIO.Direction = ParameterDirection.InputOutput;
                        cmdTransaccionFactory.Parameters.Add(PCOD_USUARIO);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pTipo == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CART";
                        else if (pTipo == 2)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CAU";
                        else if (pTipo == 3)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CONT";
                        else if (pTipo == 4)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_PROV";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CART";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalancePruebaData", "USP_XPINN_CON_BALPRU", ex);
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        Configuracion conf = new Configuracion();

                        string sql = "";

                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = "Select c.cod_cuenta, p.nombre, c.centro_costo, c.saldo_operativo, c.saldo_contable From CUADRE_CONTABLE C LEFT JOIN PLAN_CUENTAS P ON C.COD_CUENTA = P.COD_CUENTA LEFT JOIN CENTRO_COSTO D ON C.CENTRO_COSTO = D.CENTRO_COSTO Where c.saldo_operativo != 0 Or c.saldo_contable != 0 Order by c.cod_cuenta";
                        else
                            sql = "Select c.cod_cuenta, p.nombre, c.centro_costo, c.saldo_operativo, c.saldo_contable From CUADRE_CONTABLE C LEFT JOIN PLAN_CUENTAS P ON C.COD_CUENTA = P.COD_CUENTA LEFT JOIN CENTRO_COSTO D ON C.CENTRO_COSTO = D.CENTRO_COSTO Where c.saldo_operativo != 0 Or c.saldo_contable != 0 Order by c.cod_cuenta";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Reporte entidad = new Reporte();

                            if (resultado["COD_CUENTA"] != DBNull.Value) entidad.cod_cuenta = Convert.ToString(resultado["COD_CUENTA"]);
                            if (resultado["CENTRO_COSTO"] != DBNull.Value) entidad.centro_costo = Convert.ToInt32(resultado["CENTRO_COSTO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALDO_OPERATIVO"] != DBNull.Value) entidad.saldo_operativo = Convert.ToDecimal(resultado["SALDO_OPERATIVO"]);
                            if (resultado["SALDO_CONTABLE"] != DBNull.Value) entidad.saldo_contable = Convert.ToDecimal(resultado["SALDO_CONTABLE"]);
                            entidad.diferencia_actual = entidad.saldo_operativo - entidad.saldo_contable;
                            lstReporte.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstReporte;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("BalancePruebaData", "ListarBalance", ex);
                        return null;
                    }
                }
            }
        }

        public bool GuardarCuadreSaldos(DateTime pFecha, int pTipo, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter PFECHA = cmdTransaccionFactory.CreateParameter();
                        PFECHA.ParameterName = "PFECHA";
                        PFECHA.Value = pFecha;
                        PFECHA.Direction = ParameterDirection.Input;
                        PFECHA.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(PFECHA);

                        DbParameter PTIPO = cmdTransaccionFactory.CreateParameter();
                        PTIPO.ParameterName = "PTIPO";
                        if (EsPlanCuentasNIIF(pUsuario))
                            PTIPO.Value = "1";
                        else
                            PTIPO.Value = "0";
                        PTIPO.Direction = ParameterDirection.Input;
                        PTIPO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PTIPO);

                        DbParameter PAJUSTAR = cmdTransaccionFactory.CreateParameter();
                        PAJUSTAR.ParameterName = "PAJUSTAR";
                        PAJUSTAR.Value = "1";
                        PAJUSTAR.Direction = ParameterDirection.Input;
                        PAJUSTAR.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PAJUSTAR);

                        DbParameter PCOD_USUARIO = cmdTransaccionFactory.CreateParameter();
                        PCOD_USUARIO.ParameterName = "PCOD_USUARIO";
                        PCOD_USUARIO.Value = pUsuario.codusuario;
                        PCOD_USUARIO.Direction = ParameterDirection.InputOutput;
                        cmdTransaccionFactory.Parameters.Add(PCOD_USUARIO);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        if (pTipo == 1)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CART";
                        else if (pTipo == 2)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CAU";
                        else if (pTipo == 3)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CONT";
                        else if (pTipo == 4)
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_PROV";
                        else
                            cmdTransaccionFactory.CommandText = "USP_XPINN_CAR_CUADRE_COMP_CART";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ReporteData", "GuardarCuadreSaldos", ex);
                        return false;
                    }
                };

            }
        }

        public bool EsPlanCuentasNIIF(Usuario pUsuario)
        {
            int cantidad = 0;
            DbDataReader resultado = default(DbDataReader);
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = @"Select Count(*) As NIIF From par_cue_lincred Where cod_atr = 2 And tipo = 0 And cod_cuenta Like '14%' ";
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NIIF"] != DBNull.Value) cantidad = Convert.ToInt32(resultado["NIIF"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        if (cantidad > 0)
                            return true;
                        else
                            return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }


    }
}
