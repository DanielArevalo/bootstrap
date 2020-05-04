using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
 
namespace Xpinn.Integracion.Data
{
    public class PagoInternoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public PagoInternoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<ProductoPorPagar> listarProductosPorPagar(long cod_persona, string filtro, Usuario pUsuario)
        {
            //INSERT INTO INT_OPERACIONES(Id_Operacion, Nombre, Estado) VALUES(2,'Transferencias',1);
            DbDataReader resultado;
            List<ProductoPorPagar> lstResultado = new List<ProductoPorPagar>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from (--Aportes
                                select 1 as id_tipo_prod, 'Aporte' as tipo_prod, TO_CHAR(a.cod_linea_aporte) as cod_linea, la.nombre as nom_linea,
                                       TO_CHAR(a.numero_aporte) as id, a.cuota as cuota,a.fecha_proximo_pago as fec_prox
                                from aporte a inner
                                join lineaaporte la on a.cod_linea_aporte = la.cod_linea_aporte
                                where a.estado = 1 and a.cod_persona = " + cod_persona + @"
                                UNION
                                --Créditos
                                select 2 as id_tipo_prod, 'Crédito' as tipo_prod, TO_CHAR(c.cod_linea_credito) as cod_linea, lc.nombre as nom_linea,
                                       TO_CHAR(c.numero_radicacion) as id, c.valor_cuota as cuota, c.fecha_proximo_pago as fec_prox
                                from credito c inner
                                join lineascredito lc on lc.cod_linea_credito = c.cod_linea_credito
                                where c.estado = 'C' and c.cod_deudor = " + cod_persona + @"
                                UNION
                                --Servicios
                                select 4 as id_tipo_prod, 'Servicio' as tipo_prod, TO_CHAR(s.cod_linea_servicio) as cod_linea, ls.nombre as nom_linea,
                                       TO_CHAR(s.numero_servicio) as id, s.valor_cuota as cuota, s.fecha_proximo_pago as fec_prox
                                from servicios s inner
                                join lineasservicios ls on ls.cod_linea_servicio = s.cod_linea_servicio
                                where estado = 'C' and s.cod_persona = " + cod_persona + @"
                                UNION
                                --Ahorro programado
                                select 9 as id_tipo_prod, 'Ahorro programado' as tipo_prod, TO_CHAR(p.cod_linea_programado) as cod_linea, lp.nombre as nom_linea,
                                       TO_CHAR(p.numero_programado) as id, p.valor_cuota as cuota, p.fecha_proximo_pago as fec_prox
                                from ahorro_programado p inner
                                join lineaprogramado lp on lp.cod_linea_programado = p.cod_linea_programado
                                where p.estado = 1 and p.cod_persona = " + cod_persona + @"
                                UNION
                                --Ahorro vista
                                select 3 as id_tipo_prod, 'Ahorro' as tipo_prod, TO_CHAR(h.cod_linea_ahorro) as cod_linea, lh.descripcion as nom_linea,
                                      TO_CHAR(h.numero_cuenta) as id, h.valor_cuota as cuota, h.fecha_proximo_pago as fec_prox
                                from ahorro_vista h inner
                                join lineaahorro lh on lh.cod_linea_ahorro = h.cod_linea_ahorro
                                where h.estado = 1 and h.cod_persona = " + cod_persona +")";
                        sql += !string.IsNullOrEmpty(filtro) ? " where id > 0 " : " ";
                        sql += "order by fec_prox desc, cuota desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ProductoPorPagar entidad = new ProductoPorPagar();
                            if (resultado["id"] != DBNull.Value) entidad.id_producto = Convert.ToInt64(resultado["id"]);                            
                            if (resultado["id_tipo_prod"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["id_tipo_prod"]);
                            if (resultado["tipo_prod"] != DBNull.Value) entidad.descr_tipo_producto = Convert.ToString(resultado["tipo_prod"]);
                            if (resultado["cod_linea"] != DBNull.Value) entidad.id_linea = Convert.ToInt32(resultado["cod_linea"]);
                            if (resultado["nom_linea"] != DBNull.Value) entidad.descr_linea = Convert.ToString(resultado["nom_linea"]);
                            if (resultado["cuota"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["cuota"]);
                            if (resultado["fec_prox"] != DBNull.Value) entidad.fecha_prox_pago = Convert.ToDateTime(resultado["fec_prox"]);

                            lstResultado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagoInternoData", "listarProductosPorPagar", ex);
                        return null;
                    }
                }
            }
        }


        public List<ProductoOrigenPago> listarProductosOrigenPago(long cod_persona, string filtro, Usuario pUsuario)
        {
            //INSERT INTO INT_OPERACIONES(Id_Operacion, Nombre, Estado) VALUES(2,'Transferencias',1);
            DbDataReader resultado;
            List<ProductoOrigenPago> lstResultado = new List<ProductoOrigenPago>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from (--Aportes
                                select 1 as id_tipo_prod, 'Aporte' as tipo_prod, TO_CHAR(a.cod_linea_aporte) as cod_linea, la.nombre as nom_linea,
                                       TO_CHAR(a.numero_aporte) as id, a.saldo as saldo
                                from aporte a inner
                                join lineaaporte la on a.cod_linea_aporte = la.cod_linea_aporte
                                where a.estado = 1 and la.permite_traslados = 1 and a.cod_persona = " + cod_persona + @"
                                UNION                                
                                --Ahorro vista
                                select 3 as id_tipo_prod, 'Ahorro' as tipo_prod, TO_CHAR(h.cod_linea_ahorro) as cod_linea, lh.descripcion as nom_linea,
                                      TO_CHAR(h.numero_cuenta) as id, h.saldo_total as saldo
                                from ahorro_vista h inner
                                join lineaahorro lh on lh.cod_linea_ahorro = h.cod_linea_ahorro
                                where h.estado = 1 and h.cod_persona = " + cod_persona + ")";
                        sql += !string.IsNullOrEmpty(filtro) ? " where id > 0 " : " ";
                        sql += "order by 1 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            ProductoOrigenPago entidad = new ProductoOrigenPago();
                            if (resultado["id"] != DBNull.Value) entidad.id_producto = Convert.ToInt64(resultado["id"]);
                            if (resultado["id_tipo_prod"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt32(resultado["id_tipo_prod"]);
                            if (resultado["tipo_prod"] != DBNull.Value) entidad.descr_tipo_producto = Convert.ToString(resultado["tipo_prod"]);
                            if (resultado["cod_linea"] != DBNull.Value) entidad.id_linea = Convert.ToInt32(resultado["cod_linea"]);
                            if (resultado["nom_linea"] != DBNull.Value) entidad.descr_linea = Convert.ToString(resultado["nom_linea"]);
                            if (resultado["saldo"] != DBNull.Value) entidad.disponible = Convert.ToDecimal(resultado["saldo"]);

                            lstResultado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagoInternoData", "listarProductosOrigenPago", ex);
                        return null;
                    }
                }
            }
        }


        public Int32 procesarPagoInterno(PagoInterno pago, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID = cmdTransaccionFactory.CreateParameter();
                        P_ID.ParameterName = "P_ID";
                        P_ID.Value = 0;
                        P_ID.Size = 15;
                        P_ID.DbType = DbType.Int32;
                        P_ID.Direction = ParameterDirection.Output;
                        
                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = pago.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                                                                        
                        DbParameter P_ORIGEN_TIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_ORIGEN_TIPO_PRODUCTO.ParameterName = "P_ORIGEN_TIPO_PRODUCTO";
                        P_ORIGEN_TIPO_PRODUCTO.Value = pago.origen_tipo_producto;
                        P_ORIGEN_TIPO_PRODUCTO.Direction = ParameterDirection.Input;

                        DbParameter P_ORIGEN_ID_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_ORIGEN_ID_PRODUCTO.ParameterName = "P_ORIGEN_ID_PRODUCTO";
                        P_ORIGEN_ID_PRODUCTO.Value = pago.origen_id_producto;
                        P_ORIGEN_ID_PRODUCTO.Direction = ParameterDirection.Input;

                        DbParameter P_DESTINO_TIPO_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_DESTINO_TIPO_PRODUCTO.ParameterName = "P_DESTINO_TIPO_PRODUCTO";
                        P_DESTINO_TIPO_PRODUCTO.Value = pago.destino_tipo_producto;
                        P_DESTINO_TIPO_PRODUCTO.Direction = ParameterDirection.Input;

                        DbParameter P_DESTINO_ID_PRODUCTO = cmdTransaccionFactory.CreateParameter();
                        P_DESTINO_ID_PRODUCTO.ParameterName = "P_DESTINO_ID_PRODUCTO";
                        P_DESTINO_ID_PRODUCTO.Value = pago.destino_id_producto;
                        P_DESTINO_ID_PRODUCTO.Direction = ParameterDirection.Input;

                        DbParameter P_VALOR = cmdTransaccionFactory.CreateParameter();
                        P_VALOR.ParameterName = "P_VALOR";
                        P_VALOR.Value = pago.valor;
                        P_VALOR.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(P_ID);
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);
                        cmdTransaccionFactory.Parameters.Add(P_ORIGEN_TIPO_PRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(P_ORIGEN_ID_PRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(P_DESTINO_TIPO_PRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(P_DESTINO_ID_PRODUCTO);
                        cmdTransaccionFactory.Parameters.Add(P_VALOR);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_INT_PAGOINTERNO_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        if(P_ID.Value != null)
                        {
                            return Convert.ToInt32(P_ID.Value);
                        }                        
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagoInternoData", "procesarPagoInterno", ex);
                        return 0;
                    }
                }
            }
            return 0;
        }


        public List<PagoInterno> listarPagosInternos(string filtro, Usuario pUsuario)
        {
            //INSERT INTO INT_OPERACIONES(Id_Operacion, Nombre, Estado) VALUES(2,'Transferencias',1);
            DbDataReader resultado;
            List<PagoInterno> lstResultado = new List<PagoInterno>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT p.nombre, p.identificacion,aj.snombre1||' '||aj.Sapellido1||' '||aj.Sapellido2 as NOMBREEJE, B.* FROM 
                                       (SELECT 
                                       P.ID, P.COD_PERSONA, P.ORIGEN_TIPO, P.DESTINO_TIPO, P.ORIGEN_ID, to_char(P.DESTINO_ID)  AS DESTINO_ID, P.VALOR, P.COD_OPE, FECHA,
                                       CASE P.ORIGEN_TIPO WHEN 1 THEN 'Aporte' WHEN 2 THEN 'Crédito' WHEN 2 THEN 'Servicio' WHEN 9 THEN 'Ahorro programado' WHEN 3 THEN 'Ahorro a la vista' END AS nom_tipo_origen,
                                       CASE P.ORIGEN_TIPO
                                       WHEN 1 THEN -- APORTES
                                          (SELECT la.nombre from aporte a inner join lineaaporte la on a.cod_linea_aporte = la.cod_linea_aporte where a.numero_aporte = p.origen_id)
                                       WHEN 2 THEN --CREDITOS
                                           (SELECT lc.nombre from credito c inner join lineascredito lc on lc.cod_linea_credito = c.cod_linea_credito WHERE c.numero_radicacion = p.origen_id)
                                       WHEN 4 THEN --SERVICIOS
                                           (SELECT ls.nombre from servicios s inner join lineasservicios ls on ls.cod_linea_servicio = s.cod_linea_servicio where s.numero_servicio = p.origen_id)
                                       WHEN 9 THEN --PROGRAMADO
                                           (SELECT lp.nombre from ahorro_programado p inner join lineaprogramado lp on lp.cod_linea_programado = p.cod_linea_programado where p.numero_programado = p.origen_id)
                                       WHEN 3 THEN --VISTA
                                           (SELECT lh.descripcion from ahorro_vista h inner join lineaahorro lh on lh.cod_linea_ahorro = h.cod_linea_ahorro where h.numero_cuenta = p.origen_id)
                                       END AS nom_linea_origen,
                                       CASE P.DESTINO_TIPO WHEN 1 THEN 'Aporte' WHEN 2 THEN 'Crédito' WHEN 2 THEN 'Servicio' WHEN 9 THEN 'Ahorro programado' WHEN 3 THEN 'Ahorro a la vista' END AS nom_tipo_destino,
                                       CASE P.DESTINO_TIPO
                                       WHEN 1 THEN -- APORTES
                                          (SELECT la.nombre from aporte a inner join lineaaporte la on a.cod_linea_aporte = la.cod_linea_aporte where a.numero_aporte = p.destino_id)
                                       WHEN 2 THEN --CREDITOS
                                           (SELECT lc.nombre from credito c inner join lineascredito lc on lc.cod_linea_credito = c.cod_linea_credito WHERE c.numero_radicacion = p.destino_id)
                                       WHEN 4 THEN --SERVICIOS
                                           (SELECT ls.nombre from servicios s inner join lineasservicios ls on ls.cod_linea_servicio = s.cod_linea_servicio where s.numero_servicio = p.destino_id)
                                       WHEN 9 THEN --PROGRAMADO
                                           (SELECT lp.nombre from ahorro_programado p inner join lineaprogramado lp on lp.cod_linea_programado = p.cod_linea_programado where p.numero_programado = p.destino_id)
                                       WHEN 3 THEN --VISTA
                                           (SELECT lh.descripcion from ahorro_vista h inner join lineaahorro lh on lh.cod_linea_ahorro = h.cod_linea_ahorro where h.numero_cuenta = p.destino_id)
                                       END AS nom_linea_destino
                                       FROM 
                                       INT_PAGO_INTERNO P
                                       UNION
                                       select a.id_payment as id, a.cod_persona, 0 as origen_tipo, a.type_product as destino_tipo, 0 as origen_id, to_char(a.number_product) as destino_id,
                                       a.amount as valor, a.cod_ope as COD_OPE, fecha_creacion as fecha, 'PSE' as nom_tipo_origen, 'PSE' as nom_linea_origen,
                                       CASE a.type_product WHEN 1 THEN 'Aporte' WHEN 2 THEN 'Crédito' WHEN 2 THEN 'Servicio' WHEN 9 THEN 'Ahorro programado' WHEN 3 THEN 'Ahorro a la vista' END AS nom_tipo_destino,
                                       CASE a.type_product
                                       WHEN 1 THEN -- APORTES
                                          (SELECT la.nombre from aporte ap inner join lineaaporte la on ap.cod_linea_aporte = la.cod_linea_aporte where ap.numero_aporte = a.number_product)
                                       WHEN 2 THEN --CREDITOS
                                           (SELECT lc.nombre from credito c inner join lineascredito lc on lc.cod_linea_credito = c.cod_linea_credito WHERE c.numero_radicacion = a.number_product)
                                       WHEN 4 THEN --SERVICIOS
                                           (SELECT ls.nombre from servicios s inner join lineasservicios ls on ls.cod_linea_servicio = s.cod_linea_servicio where s.numero_servicio = a.number_product)
                                       WHEN 9 THEN --PROGRAMADO
                                           (SELECT lp.nombre from ahorro_programado p inner join lineaprogramado lp on lp.cod_linea_programado = p.cod_linea_programado where p.numero_programado = a.number_product)
                                       WHEN 3 THEN --VISTA
                                           (SELECT lh.descripcion from ahorro_vista h inner join lineaahorro lh on lh.cod_linea_ahorro = h.cod_linea_ahorro where h.numero_cuenta = a.number_product)
                                       END AS nom_linea_destino
                                       FROM PAYMENT_ACH a
                                       where state in (2))B
                                       inner join v_persona p on p.cod_persona = B.cod_persona
                                       left join Asejecutivos aj on Aj.Icodigo = p.Cod_Asesor
                                       where B.id is not null " + filtro + @"                                        
                                       order by b.fecha desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            PagoInterno entidad = new PagoInterno();
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBREEJE"] != DBNull.Value) entidad.asesor = Convert.ToString(resultado["NOMBREEJE"]);
                            if (resultado["ID"] != DBNull.Value) entidad.id = Convert.ToInt32(resultado["ID"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["ORIGEN_TIPO"] != DBNull.Value) entidad.origen_tipo_producto = Convert.ToInt32(resultado["ORIGEN_TIPO"]);
                            if (resultado["DESTINO_TIPO"] != DBNull.Value) entidad.destino_tipo_producto = Convert.ToInt32(resultado["DESTINO_TIPO"]);
                            if (resultado["ORIGEN_ID"] != DBNull.Value) entidad.origen_id_producto = Convert.ToInt64(resultado["ORIGEN_ID"]);
                            if (resultado["DESTINO_ID"] != DBNull.Value) entidad.destino_id_producto = Convert.ToInt64(resultado["DESTINO_ID"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt32(resultado["COD_OPE"]);
                            if (resultado["FECHA"] != DBNull.Value) entidad.fecha = Convert.ToDateTime(resultado["FECHA"]);
                            if (resultado["NOM_TIPO_ORIGEN"] != DBNull.Value) entidad.nom_tipo_origen = Convert.ToString(resultado["NOM_TIPO_ORIGEN"]);
                            if (resultado["NOM_LINEA_ORIGEN"] != DBNull.Value) entidad.nom_linea_origen = Convert.ToString(resultado["NOM_LINEA_ORIGEN"]);
                            if (resultado["NOM_TIPO_DESTINO"] != DBNull.Value) entidad.nom_tipo_destino = Convert.ToString(resultado["NOM_TIPO_DESTINO"]);
                            if (resultado["NOM_LINEA_DESTINO"] != DBNull.Value) entidad.nom_linea_destino = Convert.ToString(resultado["NOM_LINEA_DESTINO"]);
                            lstResultado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstResultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("PagoInternoData", "listarProductosOrigenPago", ex);
                        return null;
                    }
                }
            }
        }

    }
}

