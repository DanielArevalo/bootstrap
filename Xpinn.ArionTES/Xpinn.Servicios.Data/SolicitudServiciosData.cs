using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Servicios.Entities;

namespace Xpinn.Servicios.Data
{
    public class SolicitudServiciosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public SolicitudServiciosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Servicio CrearSolicitudServicio(Servicio pServicio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pServicio.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Output;
                        pnumero_servicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "p_fecha_solicitud";
                        pfecha_solicitud.Value = pServicio.fecha_solicitud;
                        pfecha_solicitud.Direction = ParameterDirection.Input;
                        pfecha_solicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pServicio.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        pcod_linea_servicio.Value = pServicio.cod_linea_servicio;
                        pcod_linea_servicio.Direction = ParameterDirection.Input;
                        pcod_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_servicio);

                        DbParameter pcod_plan_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_plan_servicio.ParameterName = "p_cod_plan_servicio";
                        if (pServicio.cod_plan_servicio != null) pcod_plan_servicio.Value = pServicio.cod_plan_servicio; else pcod_plan_servicio.Value = DBNull.Value;
                        pcod_plan_servicio.Direction = ParameterDirection.Input;
                        pcod_plan_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_plan_servicio);

                        DbParameter pfecha_inicio_vigencia = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio_vigencia.ParameterName = "p_fecha_inicio_vigencia";
                        if (pServicio.fecha_inicio_vigencia != DateTime.MinValue && pServicio.fecha_inicio_vigencia != null) pfecha_inicio_vigencia.Value = pServicio.fecha_inicio_vigencia; else pfecha_inicio_vigencia.Value = DBNull.Value;
                        pfecha_inicio_vigencia.Direction = ParameterDirection.Input;
                        pfecha_inicio_vigencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio_vigencia);

                        DbParameter pfecha_final_vigencia = cmdTransaccionFactory.CreateParameter();
                        pfecha_final_vigencia.ParameterName = "p_fecha_final_vigencia";
                        if (pServicio.fecha_final_vigencia != DateTime.MinValue && pServicio.fecha_final_vigencia != null) pfecha_final_vigencia.Value = pServicio.fecha_final_vigencia; else pfecha_final_vigencia.Value = DBNull.Value;
                        pfecha_final_vigencia.Direction = ParameterDirection.Input;
                        pfecha_final_vigencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final_vigencia);

                        DbParameter pnum_poliza = cmdTransaccionFactory.CreateParameter();
                        pnum_poliza.ParameterName = "p_num_poliza";
                        if (pServicio.num_poliza != null) pnum_poliza.Value = pServicio.num_poliza; else pnum_poliza.Value = DBNull.Value;
                        pnum_poliza.Direction = ParameterDirection.Input;
                        pnum_poliza.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_poliza);

                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        pvalor_total.Value = pServicio.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);

                        DbParameter pfecha_primera_cuota = cmdTransaccionFactory.CreateParameter();
                        pfecha_primera_cuota.ParameterName = "p_fecha_primera_cuota";
                        if (pServicio.fecha_primera_cuota != DateTime.MinValue && pServicio.fecha_primera_cuota != null) pfecha_primera_cuota.Value = pServicio.fecha_primera_cuota; else pfecha_primera_cuota.Value = DBNull.Value;
                        pfecha_primera_cuota.Direction = ParameterDirection.Input;
                        pfecha_primera_cuota.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_primera_cuota);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        if (pServicio.numero_cuotas != 0) pnumero_cuotas.Value = pServicio.numero_cuotas; else pnumero_cuotas.Value = DBNull.Value;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pvalor_cuota = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota.ParameterName = "p_valor_cuota";
                        if (pServicio.valor_cuota != 0) pvalor_cuota.Value = pServicio.valor_cuota; else pvalor_cuota.Value = DBNull.Value;
                        pvalor_cuota.Direction = ParameterDirection.Input;
                        pvalor_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        if (pServicio.cod_periodicidad != 0) pcod_periodicidad.Value = pServicio.cod_periodicidad; else pcod_periodicidad.Value = DBNull.Value;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pServicio.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pidentificacion_titular = cmdTransaccionFactory.CreateParameter();
                        pidentificacion_titular.ParameterName = "p_identificacion_titular";
                        if (pServicio.identificacion_titular != null) pidentificacion_titular.Value = pServicio.identificacion_titular; else pidentificacion_titular.Value = DBNull.Value;
                        pidentificacion_titular.Direction = ParameterDirection.Input;
                        pidentificacion_titular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion_titular);

                        DbParameter pnombre_titular = cmdTransaccionFactory.CreateParameter();
                        pnombre_titular.ParameterName = "p_nombre_titular";
                        if (pServicio.nombre_titular != null) pnombre_titular.Value = pServicio.nombre_titular; else pnombre_titular.Value = DBNull.Value;
                        pnombre_titular.Direction = ParameterDirection.Input;
                        pnombre_titular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_titular);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pServicio.observaciones != null) pobservaciones.Value = pServicio.observaciones; else pobservaciones.Value = DBNull.Value;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        if (pServicio.saldo != null) psaldo.Value = pServicio.saldo; else psaldo.Value = DBNull.Value;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pfecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                        if (pServicio.fecha_proximo_pago != DateTime.MinValue && pServicio.fecha_proximo_pago != null) pfecha_proximo_pago.Value = pServicio.fecha_proximo_pago; else pfecha_proximo_pago.Value = DBNull.Value;
                        pfecha_proximo_pago.Direction = ParameterDirection.Input;
                        pfecha_proximo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_proximo_pago);

                        DbParameter pfecha_ultimo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_ultimo_pago.ParameterName = "p_fecha_ultimo_pago";
                        if (pServicio.fecha_ultimo_pago != DateTime.MinValue) pfecha_ultimo_pago.Value = pServicio.fecha_ultimo_pago; else pfecha_ultimo_pago.Value = DBNull.Value;
                        pfecha_ultimo_pago.Direction = ParameterDirection.Input;
                        pfecha_ultimo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ultimo_pago);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pServicio.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pServicio.fecha_aprobacion != DateTime.MinValue) pfecha_aprobacion.Value = pServicio.fecha_aprobacion; else pfecha_aprobacion.Value = DBNull.Value;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pfecha_activacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_activacion.ParameterName = "p_fecha_activacion";
                        if (pServicio.fecha_activacion != DateTime.MinValue) pfecha_activacion.Value = pServicio.fecha_activacion; else pfecha_activacion.Value = DBNull.Value;
                        pfecha_activacion.Direction = ParameterDirection.Input;
                        pfecha_activacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_activacion);

                        DbParameter pcuotas_pendientes = cmdTransaccionFactory.CreateParameter();
                        pcuotas_pendientes.ParameterName = "p_cuotas_pendientes";
                        if (pServicio.cuotas_pendientes != 0) pcuotas_pendientes.Value = pServicio.cuotas_pendientes; else pcuotas_pendientes.Value = DBNull.Value;
                        pcuotas_pendientes.Direction = ParameterDirection.Input;
                        pcuotas_pendientes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_pendientes);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        if (pServicio.cod_empresa == null)
                            pcod_empresa.Value = DBNull.Value;
                        else
                            pcod_empresa.Value = pServicio.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pcodigo_proveedor = cmdTransaccionFactory.CreateParameter();
                        pcodigo_proveedor.ParameterName = "p_codigo_proveedor";
                        if (pServicio.codigo_proveedor != null) pcodigo_proveedor.Value = pServicio.codigo_proveedor; else pcodigo_proveedor.Value = DBNull.Value;
                        pcodigo_proveedor.Direction = ParameterDirection.Input;
                        pcodigo_proveedor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_proveedor);

                        DbParameter pidentificacion_proveedor = cmdTransaccionFactory.CreateParameter();
                        pidentificacion_proveedor.ParameterName = "p_identificacion_proveedor";
                        if (pServicio.identificacion_proveedor != null) pidentificacion_proveedor.Value = pServicio.identificacion_proveedor; else pidentificacion_proveedor.Value = DBNull.Value;
                        pidentificacion_proveedor.Direction = ParameterDirection.Input;
                        pidentificacion_proveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion_proveedor);

                        DbParameter pnombre_proveedor = cmdTransaccionFactory.CreateParameter();
                        pnombre_proveedor.ParameterName = "p_nombre_proveedor";
                        if (pServicio.nombre_proveedor != null) pnombre_proveedor.Value = pServicio.nombre_proveedor; else pnombre_proveedor.Value = DBNull.Value;
                        pnombre_proveedor.Direction = ParameterDirection.Input;
                        pnombre_proveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_proveedor);

                        DbParameter pcod_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_destino.ParameterName = "pcod_destino";
                        if (pServicio.cod_destino != null) pcod_destino.Value = pServicio.cod_destino; else pcod_destino.Value = DBNull.Value;
                        pcod_destino.Direction = ParameterDirection.Input;
                        pcod_destino.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_destino);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_SERVICIOS_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pServicio.numero_servicio = Convert.ToInt32(pnumero_servicio.Value);

                        DAauditoria.InsertarLog(pServicio, "Servicios", vUsuario, Accion.Crear.ToString(), TipoAuditoria.Servicios, "Creacion de servicios con numero de servicio " + pServicio.numero_servicio);

                        return pServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "CrearSolicitudServicio", ex);
                        return null;
                    }
                }
            }
        }

        public bool ModificarEstadoSolicitudServicio(Servicio solicitud, Usuario usuario)
        {   
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter P_ID_SOL_RETIRO = cmdTransaccionFactory.CreateParameter();
                        P_ID_SOL_RETIRO.ParameterName = "P_ID_SOL_SERVICIO";
                        P_ID_SOL_RETIRO.Value = solicitud.numero_servicio;
                        P_ID_SOL_RETIRO.Direction = ParameterDirection.Input;
                        P_ID_SOL_RETIRO.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_ID_SOL_RETIRO);

                        DbParameter P_ESTADO = cmdTransaccionFactory.CreateParameter();
                        P_ESTADO.ParameterName = "P_ESTADO";
                        P_ESTADO.Value = solicitud.estado;
                        P_ESTADO.Direction = ParameterDirection.Input;
                        P_ESTADO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_ESTADO);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_SOLSERVICIO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ModificarEstadoSolicitudServicio", ex);
                        return false;
                    }
                }
            }
            
        }   

        public int ConsultarNumeroServiciosPersona(string cod_persona, string cod_linea, Usuario usuario)
        {
            DbDataReader resultado;
            int numeroServicios = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select COUNT(*) as contador from servicios where to_char(fecha_activacion, 'YYYY') = to_char(sysdate, 'YYYY') and cod_persona = " + cod_persona + " and COD_LINEA_SERVICIO = " + cod_linea;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Servicio entidad = new Servicio();
                            if (resultado["contador"] != DBNull.Value) numeroServicios = Convert.ToInt32(resultado["contador"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return numeroServicios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "ConsultarNumeroServiciosPersona", ex);
                        return 0;
                    }
                }
            }
        }

        public Servicio ConsultarDatosPlanDePagos(Servicio servicio, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_cod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        p_cod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        p_cod_linea_servicio.Value = Convert.ToInt64(servicio.cod_linea_servicio);
                        p_cod_linea_servicio.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_cod_linea_servicio);

                        DbParameter p_fechasolicitud = cmdTransaccionFactory.CreateParameter();
                        p_fechasolicitud.ParameterName = "p_fechasolicitud";
                        p_fechasolicitud.Value = servicio.fecha_solicitud;
                        p_fechasolicitud.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_fechasolicitud);

                        DbParameter p_plazo = cmdTransaccionFactory.CreateParameter();
                        p_plazo.ParameterName = "p_plazo";
                        p_plazo.Value = servicio.numero_cuotas;
                        p_plazo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_plazo);

                        DbParameter p_montosolicitado = cmdTransaccionFactory.CreateParameter();
                        p_montosolicitado.ParameterName = "p_montosolicitado";
                        p_montosolicitado.Value = servicio.valor_total;
                        p_montosolicitado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_montosolicitado);

                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = servicio.cod_persona;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_periodicidad = cmdTransaccionFactory.CreateParameter();
                        p_periodicidad.ParameterName = "p_periodicidad";
                        p_periodicidad.Value = servicio.cod_periodicidad;
                        p_periodicidad.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_periodicidad);

                        DbParameter p_numero_servicio = cmdTransaccionFactory.CreateParameter();
                        p_numero_servicio.ParameterName = "p_numero_servicio";
                        p_numero_servicio.Value = servicio.numero_servicio;
                        p_numero_servicio.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_numero_servicio);

                        DbParameter p_tasa = cmdTransaccionFactory.CreateParameter();
                        p_tasa.ParameterName = "p_tasa";
                        p_tasa.Value = servicio.tasa;
                        p_tasa.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(p_tasa);

                        DbParameter p_diasPeriodicidad = cmdTransaccionFactory.CreateParameter();
                        p_diasPeriodicidad.ParameterName = "p_diasPeriodicidad";
                        p_diasPeriodicidad.Value = servicio.dias_periodicidad;
                        p_diasPeriodicidad.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(p_diasPeriodicidad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "usp_xpinn_apo_servicio_tasa";
                        cmdTransaccionFactory.ExecuteNonQuery();


                        servicio.tasa = p_tasa.Value != DBNull.Value ? Convert.ToDecimal(p_tasa.Value) : 0;
                        servicio.dias_periodicidad = p_diasPeriodicidad.Value != DBNull.Value ? Convert.ToInt32(p_diasPeriodicidad.Value) : 0;

                        dbConnectionFactory.CerrarConexion(connection);

                        return servicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "ConsultarDatosPlanDePagos", ex);
                        return null;
                    }
                }
            }
        }


        public Servicio ModificarSolicitudServicio(Servicio pServicio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Servicio servicioAnterior = ConsultarSERVICIO(pServicio.numero_servicio, vUsuario);

                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pServicio.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pfecha_solicitud = cmdTransaccionFactory.CreateParameter();
                        pfecha_solicitud.ParameterName = "p_fecha_solicitud";
                        pfecha_solicitud.Value = pServicio.fecha_solicitud;
                        pfecha_solicitud.Direction = ParameterDirection.Input;
                        pfecha_solicitud.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_solicitud);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        pcod_persona.Value = pServicio.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pcod_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_linea_servicio.ParameterName = "p_cod_linea_servicio";
                        pcod_linea_servicio.Value = pServicio.cod_linea_servicio;
                        pcod_linea_servicio.Direction = ParameterDirection.Input;
                        pcod_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_linea_servicio);

                        DbParameter pcod_plan_servicio = cmdTransaccionFactory.CreateParameter();
                        pcod_plan_servicio.ParameterName = "p_cod_plan_servicio";
                        if (pServicio.cod_plan_servicio != null) pcod_plan_servicio.Value = pServicio.cod_plan_servicio; else pcod_plan_servicio.Value = DBNull.Value;
                        pcod_plan_servicio.Direction = ParameterDirection.Input;
                        pcod_plan_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pcod_plan_servicio);

                        DbParameter pfecha_inicio_vigencia = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicio_vigencia.ParameterName = "p_fecha_inicio_vigencia";
                        if (pServicio.fecha_inicio_vigencia != DateTime.MinValue) pfecha_inicio_vigencia.Value = pServicio.fecha_inicio_vigencia; else pfecha_inicio_vigencia.Value = DBNull.Value;
                        pfecha_inicio_vigencia.Direction = ParameterDirection.Input;
                        pfecha_inicio_vigencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicio_vigencia);

                        DbParameter pfecha_final_vigencia = cmdTransaccionFactory.CreateParameter();
                        pfecha_final_vigencia.ParameterName = "p_fecha_final_vigencia";
                        if (pServicio.fecha_final_vigencia != DateTime.MinValue) pfecha_final_vigencia.Value = pServicio.fecha_final_vigencia; else pfecha_final_vigencia.Value = DBNull.Value;
                        pfecha_final_vigencia.Direction = ParameterDirection.Input;
                        pfecha_final_vigencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final_vigencia);

                        DbParameter pnum_poliza = cmdTransaccionFactory.CreateParameter();
                        pnum_poliza.ParameterName = "p_num_poliza";
                        if (pServicio.num_poliza != null) pnum_poliza.Value = pServicio.num_poliza; else pnum_poliza.Value = DBNull.Value;
                        pnum_poliza.Direction = ParameterDirection.Input;
                        pnum_poliza.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnum_poliza);

                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        pvalor_total.Value = pServicio.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);

                        DbParameter pfecha_primera_cuota = cmdTransaccionFactory.CreateParameter();
                        pfecha_primera_cuota.ParameterName = "p_fecha_primera_cuota";
                        if (pServicio.fecha_primera_cuota != DateTime.MinValue) pfecha_primera_cuota.Value = pServicio.fecha_primera_cuota; else pfecha_primera_cuota.Value = DBNull.Value;
                        pfecha_primera_cuota.Direction = ParameterDirection.Input;
                        pfecha_primera_cuota.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_primera_cuota);

                        DbParameter pnumero_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnumero_cuotas.ParameterName = "p_numero_cuotas";
                        if (pServicio.numero_cuotas != 0) pnumero_cuotas.Value = pServicio.numero_cuotas; else pnumero_cuotas.Value = DBNull.Value;
                        pnumero_cuotas.Direction = ParameterDirection.Input;
                        pnumero_cuotas.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_cuotas);

                        DbParameter pvalor_cuota = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota.ParameterName = "p_valor_cuota";
                        if (pServicio.valor_cuota != 0) pvalor_cuota.Value = pServicio.valor_cuota; else pvalor_cuota.Value = DBNull.Value;
                        pvalor_cuota.Direction = ParameterDirection.Input;
                        pvalor_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota);

                        DbParameter pcod_periodicidad = cmdTransaccionFactory.CreateParameter();
                        pcod_periodicidad.ParameterName = "p_cod_periodicidad";
                        if (pServicio.cod_periodicidad != 0) pcod_periodicidad.Value = pServicio.cod_periodicidad; else pcod_periodicidad.Value = DBNull.Value;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pServicio.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        DbParameter pidentificacion_titular = cmdTransaccionFactory.CreateParameter();
                        pidentificacion_titular.ParameterName = "p_identificacion_titular";
                        pidentificacion_titular.Value = pServicio.identificacion_titular;
                        pidentificacion_titular.Direction = ParameterDirection.Input;
                        pidentificacion_titular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion_titular);

                        DbParameter pnombre_titular = cmdTransaccionFactory.CreateParameter();
                        pnombre_titular.ParameterName = "p_nombre_titular";
                        pnombre_titular.Value = pServicio.nombre_titular;
                        pnombre_titular.Direction = ParameterDirection.Input;
                        pnombre_titular.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_titular);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pServicio.observaciones != null) pobservaciones.Value = pServicio.observaciones; else pobservaciones.Value = DBNull.Value;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        if (pServicio.saldo != 0) psaldo.Value = pServicio.saldo; else psaldo.Value = DBNull.Value;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pfecha_proximo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_proximo_pago.ParameterName = "p_fecha_proximo_pago";
                        if (pServicio.fecha_proximo_pago != DateTime.MinValue) pfecha_proximo_pago.Value = pServicio.fecha_proximo_pago; else pfecha_proximo_pago.Value = DBNull.Value;
                        pfecha_proximo_pago.Direction = ParameterDirection.Input;
                        pfecha_proximo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_proximo_pago);

                        DbParameter pfecha_ultimo_pago = cmdTransaccionFactory.CreateParameter();
                        pfecha_ultimo_pago.ParameterName = "p_fecha_ultimo_pago";
                        if (pServicio.fecha_ultimo_pago != DateTime.MinValue) pfecha_ultimo_pago.Value = pServicio.fecha_ultimo_pago; else pfecha_ultimo_pago.Value = DBNull.Value;
                        pfecha_ultimo_pago.Direction = ParameterDirection.Input;
                        pfecha_ultimo_pago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_ultimo_pago);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pServicio.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "p_cod_empresa";
                        if (pServicio.cod_empresa == null)
                            pcod_empresa.Value = DBNull.Value;
                        else
                            pcod_empresa.Value = pServicio.cod_empresa;
                        pcod_empresa.Direction = ParameterDirection.Input;
                        pcod_empresa.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);

                        DbParameter pcodigo_proveedor = cmdTransaccionFactory.CreateParameter();
                        pcodigo_proveedor.ParameterName = "p_codigo_proveedor";
                        if (pServicio.codigo_proveedor != null) pcodigo_proveedor.Value = pServicio.codigo_proveedor; else pcodigo_proveedor.Value = DBNull.Value;
                        pcodigo_proveedor.Direction = ParameterDirection.Input;
                        pcodigo_proveedor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigo_proveedor);

                        DbParameter pidentificacion_proveedor = cmdTransaccionFactory.CreateParameter();
                        pidentificacion_proveedor.ParameterName = "p_identificacion_proveedor";
                        pidentificacion_proveedor.Value = pServicio.identificacion_proveedor;
                        pidentificacion_proveedor.Direction = ParameterDirection.Input;
                        pidentificacion_proveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion_proveedor);

                        DbParameter pnombre_proveedor = cmdTransaccionFactory.CreateParameter();
                        pnombre_proveedor.ParameterName = "p_nombre_proveedor";
                        pnombre_proveedor.Value = pServicio.nombre_proveedor;
                        pnombre_proveedor.Direction = ParameterDirection.Input;
                        pnombre_proveedor.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_proveedor);

                        DbParameter pcod_destino = cmdTransaccionFactory.CreateParameter();
                        pcod_destino.ParameterName = "pcod_destino";
                        if (pServicio.cod_destino != null) pcod_destino.Value = pServicio.cod_destino; else pcod_destino.Value = DBNull.Value;
                        pcod_destino.Direction = ParameterDirection.Input;
                        pcod_destino.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_destino);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_SERVICIOS_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        DAauditoria.InsertarLog(pServicio, "Servicios", vUsuario, Accion.Modificar.ToString(), TipoAuditoria.Servicios, "Modificacion de servicios con numero de servicio " + pServicio.numero_servicio, servicioAnterior);

                        return pServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "ModificarSolicitudServicio", ex);
                        return null;
                    }
                }
            }
        }



        public List<Servicio> ListarServicios(Servicio pServicio, DateTime pFechaIni, Usuario vUsuario, string filtro)
        {
            DbDataReader resultado;
            List<Servicio> lstServicio = new List<Servicio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select s.numero_servicio,s.fecha_solicitud,s.cod_persona, p.identificacion, "
                                        + "p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido as Nombre, "
                                        + "l.nombre as nom_linea,z.nombre as nom_Plan,p.cod_nomina, s.num_poliza,s.fecha_inicio_vigencia,s.fecha_final_vigencia, "
                                        + "s.valor_total,s.fecha_primera_cuota,s.numero_cuotas,s.valor_cuota,n.descripcion as nom_Periodicidad, "
                                        + "case s.forma_pago when '1' then 'Caja' when '2' then 'Nomina' end as forma_pago, s.saldo, "
                                        + "s.identificacion_titular,s.nombre_titular, s.estado, case s.estado when 'S' then 'Solicitado' when 'A' then 'Aprobado' when 'C' then 'Activado' when 'T' then 'Terminado' end as descripcion_estado "
                                        + "from servicios s left join persona p on p.cod_persona = s.cod_persona "
                                        + "left join lineasservicios l on l.cod_linea_servicio = s.cod_linea_servicio "
                                        + "left join planservicios z on z.cod_plan_servicio=s.cod_plan_servicio "
                                        + "left join periodicidad n on n.cod_periodicidad = s.cod_periodicidad  where 1=1" + filtro;

                        Configuracion conf = new Configuracion();
                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " s.fecha_solicitud = To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " s.fecha_solicitud = '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        sql += " ORDER BY NUMERO_SERVICIO ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Servicio entidad = new Servicio();
                            if (resultado["NUMERO_SERVICIO"] != DBNull.Value) entidad.numero_servicio = Convert.ToInt32(resultado["NUMERO_SERVICIO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["NOM_PLAN"] != DBNull.Value) entidad.nom_plan = Convert.ToString(resultado["NOM_PLAN"]);
                            if (resultado["FECHA_INICIO_VIGENCIA"] != DBNull.Value) entidad.fecha_inicio_vigencia = Convert.ToDateTime(resultado["FECHA_INICIO_VIGENCIA"]);
                            if (resultado["FECHA_FINAL_VIGENCIA"] != DBNull.Value) entidad.fecha_final_vigencia = Convert.ToDateTime(resultado["FECHA_FINAL_VIGENCIA"]);
                            if (resultado["NUM_POLIZA"] != DBNull.Value) entidad.num_poliza = Convert.ToString(resultado["NUM_POLIZA"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["VALOR_TOTAL"]);
                            if (resultado["FECHA_PRIMERA_CUOTA"] != DBNull.Value) entidad.fecha_primera_cuota = Convert.ToDateTime(resultado["FECHA_PRIMERA_CUOTA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["NOM_PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["NOM_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["IDENTIFICACION_TITULAR"] != DBNull.Value) entidad.identificacion_titular = Convert.ToString(resultado["IDENTIFICACION_TITULAR"]);
                            if (resultado["NOMBRE_TITULAR"] != DBNull.Value) entidad.nombre_titular = Convert.ToString(resultado["NOMBRE_TITULAR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["descripcion_estado"] != DBNull.Value) entidad.descripcion_estado = Convert.ToString(resultado["descripcion_estado"]);
                            lstServicio.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "ListarServicios", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarServicio(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pId;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_SERVICIOS_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "EliminarServicio", ex);
                    }
                }
            }
        }


        public Servicio ConsultarSERVICIO(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            Servicio entidad = new Servicio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT servicios.*, lineasservicios.requierebeneficiarios, persona_afiliacion.estado as estados, persona.identificacion, 
                                        persona.primer_nombre ||' '|| persona.segundo_nombre||' '|| persona.primer_apellido||' '||persona.segundo_apellido as Nombre,
                                        lineasservicios.Nombre as Nombre_Linea, per.descripcion as Nombre_Periodicidad, per.tipo_calendario, lineasservicios.tipo_cuota
                                        FROM servicios INNER JOIN persona ON persona.cod_persona = servicios.cod_persona 
                                        LEFT JOIN persona_afiliacion on persona.cod_persona = persona_afiliacion.cod_persona 
                                        INNER JOIN lineasservicios on servicios.cod_linea_servicio = lineasservicios.cod_linea_servicio
                                        INNER JOIN periodicidad per on per.cod_periodicidad = servicios.cod_periodicidad
                                        WHERE numero_servicio = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_SERVICIO"] != DBNull.Value) entidad.numero_servicio = Convert.ToInt32(resultado["NUMERO_SERVICIO"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["COD_LINEA_SERVICIO"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToString(resultado["COD_LINEA_SERVICIO"]);
                            if (resultado["Nombre_Linea"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["Nombre_Linea"]);
                            if (resultado["COD_PLAN_SERVICIO"] != DBNull.Value) entidad.cod_plan_servicio = Convert.ToString(resultado["COD_PLAN_SERVICIO"]);
                            if (resultado["FECHA_INICIO_VIGENCIA"] != DBNull.Value) entidad.Fec_ini = Convert.ToDateTime(resultado["FECHA_INICIO_VIGENCIA"]);
                            if (resultado["FECHA_FINAL_VIGENCIA"] != DBNull.Value) entidad.Fec_fin = Convert.ToDateTime(resultado["FECHA_FINAL_VIGENCIA"]);
                            if (resultado["NUM_POLIZA"] != DBNull.Value) entidad.num_poliza = Convert.ToString(resultado["NUM_POLIZA"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["VALOR_TOTAL"]);
                            if (resultado["FECHA_PRIMERA_CUOTA"] != DBNull.Value) entidad.Fec_1Pago = Convert.ToDateTime(resultado["FECHA_PRIMERA_CUOTA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["Nombre_Periodicidad"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["Nombre_Periodicidad"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["IDENTIFICACION_TITULAR"] != DBNull.Value) entidad.identificacion_titular = Convert.ToString(resultado["IDENTIFICACION_TITULAR"]);
                            if (resultado["NOMBRE_TITULAR"] != DBNull.Value) entidad.nombre_titular = Convert.ToString(resultado["NOMBRE_TITULAR"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["ESTADOS"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADOS"]);
                            if (resultado["REQUIEREBENEFICIARIOS"] != DBNull.Value) entidad.beneficiarios = Convert.ToString(resultado["REQUIEREBENEFICIARIOS"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["TIPO_CALENDARIO"] != DBNull.Value) entidad.tipo_calendario = Convert.ToInt32(resultado["TIPO_CALENDARIO"]);
                            if (resultado["tipo_cuota"] != DBNull.Value) entidad.tipo_cuota = Convert.ToInt32(resultado["tipo_cuota"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["CUOTAS_PENDIENTES"] != DBNull.Value) entidad.cuotas_pendientes = Convert.ToInt32(resultado["CUOTAS_PENDIENTES"]);
                            if (resultado["DESTINACION"] != DBNull.Value) entidad.cod_destino = Convert.ToInt64(resultado["DESTINACION"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "ConsultarSERVICIO", ex);
                        return null;
                    }
                }
            }
        }


        public DetalleServicio CrearDetalleServicio(DetalleServicio pDetalle, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodserbeneficiario = cmdTransaccionFactory.CreateParameter();
                        pcodserbeneficiario.ParameterName = "p_codserbeneficiario";
                        pcodserbeneficiario.Value = pDetalle.codserbeneficiario;
                        pcodserbeneficiario.Direction = ParameterDirection.Output;
                        pcodserbeneficiario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodserbeneficiario);

                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pDetalle.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pDetalle.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pDetalle.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pcodparentesco = cmdTransaccionFactory.CreateParameter();
                        pcodparentesco.ParameterName = "p_codparentesco";
                        pcodparentesco.Value = pDetalle.codparentesco;
                        pcodparentesco.Direction = ParameterDirection.Input;
                        pcodparentesco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodparentesco);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        pporcentaje.Value = pDetalle.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_DETASERVI_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pDetalle.codserbeneficiario = Convert.ToInt64(pcodserbeneficiario.Value);
                        return pDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "CrearDetalleServicio", ex);
                        return null;
                    }
                }
            }
        }


        public DetalleServicio ModificarDetalleServicio(DetalleServicio pDetalle, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodserbeneficiario = cmdTransaccionFactory.CreateParameter();
                        pcodserbeneficiario.ParameterName = "p_codserbeneficiario";
                        pcodserbeneficiario.Value = pDetalle.codserbeneficiario;
                        pcodserbeneficiario.Direction = ParameterDirection.Input;
                        pcodserbeneficiario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodserbeneficiario);

                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pDetalle.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "p_identificacion";
                        pidentificacion.Value = pDetalle.identificacion;
                        pidentificacion.Direction = ParameterDirection.Input;
                        pidentificacion.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "p_nombre";
                        pnombre.Value = pDetalle.nombre;
                        pnombre.Direction = ParameterDirection.Input;
                        pnombre.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre);

                        DbParameter pcodparentesco = cmdTransaccionFactory.CreateParameter();
                        pcodparentesco.ParameterName = "p_codparentesco";
                        pcodparentesco.Value = pDetalle.codparentesco;
                        pcodparentesco.Direction = ParameterDirection.Input;
                        pcodparentesco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodparentesco);

                        DbParameter pporcentaje = cmdTransaccionFactory.CreateParameter();
                        pporcentaje.ParameterName = "p_porcentaje";
                        pporcentaje.Value = pDetalle.porcentaje;
                        pporcentaje.Direction = ParameterDirection.Input;
                        pporcentaje.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pporcentaje);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_DETASERVI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "ModificarDetalleServicio", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarDETALLESERVICIO(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodserbeneficiario = cmdTransaccionFactory.CreateParameter();
                        pcodserbeneficiario.ParameterName = "p_codserbeneficiario";
                        pcodserbeneficiario.Value = pId;
                        pcodserbeneficiario.Direction = ParameterDirection.Input;
                        pcodserbeneficiario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodserbeneficiario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_DETASERVI_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "EliminarDETALLESERVICIO", ex);
                    }
                }
            }
        }


        public List<DetalleServicio> ConsultarDETALLESERVICIO(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<DetalleServicio> LstDetalle = new List<DetalleServicio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM servicio_beneficiarios WHERE NUMERO_SERVICIO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            DetalleServicio entidad = new DetalleServicio();
                            if (resultado["CODSERBENEFICIARIO"] != DBNull.Value) entidad.codserbeneficiario = Convert.ToInt64(resultado["CODSERBENEFICIARIO"]);
                            if (resultado["NUMERO_SERVICIO"] != DBNull.Value) entidad.numero_servicio = Convert.ToInt32(resultado["NUMERO_SERVICIO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt32(resultado["CODPARENTESCO"]);
                            if (resultado["PORCENTAJE"] != DBNull.Value) entidad.porcentaje = Convert.ToDecimal(resultado["PORCENTAJE"]);
                            LstDetalle.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return LstDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "ConsultarDETALLESERVICIO", ex);
                        return null;
                    }
                }
            }
        }


        public List<Servicio> CargarPlanXLinea(Int64 pVar, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Servicio> lstDatos = new List<Servicio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from planservicios where cod_linea_servicio = '" + pVar.ToString() + "' order by COD_PLAN_SERVICIO";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Servicio entidad = new Servicio();
                            if (resultado["COD_PLAN_SERVICIO"] != DBNull.Value) entidad.cod_plan_servicio = Convert.ToString(resultado["COD_PLAN_SERVICIO"]);
                            if (resultado["COD_LINEA_SERVICIO"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToString(resultado["COD_LINEA_SERVICIO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstDatos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstDatos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "CargarPlanXLinea", ex);
                        return null;
                    }
                }
            }
        }



        public Servicio ConsultaProveedorXlinea(Int32 pVar, Usuario vUsuario)
        {
            DbDataReader resultado;
            Servicio entidad = new Servicio();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT IDENTIFICACION_PROVEEDOR,NOMBRE_PROVEEDOR FROM LINEASSERVICIOS where COD_LINEA_SERVICIO = " + pVar.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDENTIFICACION_PROVEEDOR"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION_PROVEEDOR"]);
                            if (resultado["NOMBRE_PROVEEDOR"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE_PROVEEDOR"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "ConsultaProveedorXlinea", ex);
                        return null;
                    }
                }
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(numero_servicio)+1 from servicios ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = Convert.ToInt64(cmdTransaccionFactory.ExecuteScalar());
                        dbConnectionFactory.CerrarConexion(connection);
                        return resultado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ActivoFijoData", "ObtenerSiguienteCodigo", ex);
                        return -1;
                    }
                }
            }
        }



        public ExclusionServicios CrearExclusionServicios(ExclusionServicios pExclusionServicios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidexclusión = cmdTransaccionFactory.CreateParameter();
                        pidexclusión.ParameterName = "p_idexclusión";
                        pidexclusión.Value = pExclusionServicios.idexclusión;
                        pidexclusión.Direction = ParameterDirection.Output;
                        pidexclusión.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidexclusión);

                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pExclusionServicios.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pfecha = cmdTransaccionFactory.CreateParameter();
                        pfecha.ParameterName = "p_fecha";
                        pfecha.Value = pExclusionServicios.fecha;
                        pfecha.Direction = ParameterDirection.Input;
                        pfecha.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pExclusionServicios.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pcodmotivo = cmdTransaccionFactory.CreateParameter();
                        pcodmotivo.ParameterName = "p_codmotivo";
                        if (pExclusionServicios.codmotivo == null)
                            pcodmotivo.Value = DBNull.Value;
                        else
                            pcodmotivo.Value = pExclusionServicios.codmotivo;
                        pcodmotivo.Direction = ParameterDirection.Input;
                        pcodmotivo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodmotivo);

                        DbParameter pcuo_noexcluir = cmdTransaccionFactory.CreateParameter();
                        pcuo_noexcluir.ParameterName = "p_cuo_noexcluir";
                        if (pExclusionServicios.cuo_noexcluir == null)
                            pcuo_noexcluir.Value = DBNull.Value;
                        else
                            pcuo_noexcluir.Value = pExclusionServicios.cuo_noexcluir;
                        pcuo_noexcluir.Direction = ParameterDirection.Input;
                        pcuo_noexcluir.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuo_noexcluir);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pExclusionServicios.cod_ope == null)
                            pcod_ope.Value = DBNull.Value;
                        else
                            pcod_ope.Value = pExclusionServicios.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pfeccrea = cmdTransaccionFactory.CreateParameter();
                        pfeccrea.ParameterName = "p_feccrea";
                        if (pExclusionServicios.feccrea == null)
                            pfeccrea.Value = DBNull.Value;
                        else
                            pfeccrea.Value = pExclusionServicios.feccrea;
                        pfeccrea.Direction = ParameterDirection.Input;
                        pfeccrea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfeccrea);

                        DbParameter pusuariocrea = cmdTransaccionFactory.CreateParameter();
                        pusuariocrea.ParameterName = "p_usuariocrea";
                        if (pExclusionServicios.usuariocrea == null)
                            pusuariocrea.Value = DBNull.Value;
                        else
                            pusuariocrea.Value = pExclusionServicios.usuariocrea;
                        pusuariocrea.Direction = ParameterDirection.Input;
                        pusuariocrea.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pusuariocrea);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_EXCLUSION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pExclusionServicios.idexclusión = Convert.ToInt64(pidexclusión.Value);
                        return pExclusionServicios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ExclusionServiciosData", "CrearExclusionServicios", ex);
                        return null;
                    }
                }
            }
        }


        public bool ConsultarEstadoPersona(Int64? pCodPersona, string pIdentificacion, string pEstado, Usuario pUsuario)
        {
            DbDataReader resultado;
            string estado = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            sql = @"SELECT NVL(persona_afiliacion.estado, persona.estado) As estado 
                                    FROM persona LEFT JOIN persona_afiliacion ON persona.cod_persona = persona_afiliacion.cod_persona";
                        else
                            sql = @"SELECT CASE persona_afiliacion.estado WHEN Null THEN persona_afiliacion.estado ELSE persona.estado END As estado 
                                    FROM persona LEFT JOIN persona_afiliacion ON persona.cod_persona = persona_afiliacion.cod_persona";
                        if (pCodPersona != null || pCodPersona != 0)
                            sql = sql + " WHERE persona.cod_persona = " + pCodPersona.ToString();
                        else
                            sql = sql + " WHERE persona.identificacion = '" + pIdentificacion + "' ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["ESTADO"] != DBNull.Value) estado = Convert.ToString(resultado["ESTADO"]);
                            if (estado == pEstado)
                                return true;
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
        }


        public Servicio CrearSolicitudServicioOficinaVirtual(Servicio pServicio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        

                        DbParameter P_ID_SOL_SERVICIO = cmdTransaccionFactory.CreateParameter();
                        P_ID_SOL_SERVICIO.ParameterName = "P_ID_SOL_SERVICIO";
                        P_ID_SOL_SERVICIO.Value = pServicio.numero_servicio;
                        P_ID_SOL_SERVICIO.Direction = ParameterDirection.Output;
                        P_ID_SOL_SERVICIO.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_ID_SOL_SERVICIO);

                        DbParameter P_FECHA_SERVICIO = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_SERVICIO.ParameterName = "P_FECHA_SERVICIO";
                        P_FECHA_SERVICIO.Value = pServicio.fecha_solicitud;
                        P_FECHA_SERVICIO.Direction = ParameterDirection.Input;
                        P_FECHA_SERVICIO.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_SERVICIO);

                        DbParameter P_COD_LINEA_SERVICIO = cmdTransaccionFactory.CreateParameter();
                        P_COD_LINEA_SERVICIO.ParameterName = "P_COD_LINEA_SERVICIO";
                        P_COD_LINEA_SERVICIO.Value = pServicio.cod_linea_servicio;
                        P_COD_LINEA_SERVICIO.Direction = ParameterDirection.Input;
                        P_COD_LINEA_SERVICIO.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_COD_LINEA_SERVICIO);

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = pServicio.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        P_COD_PERSONA.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);                        
                       
                        DbParameter P_VALOR_TOTAL = cmdTransaccionFactory.CreateParameter();
                        P_VALOR_TOTAL.ParameterName = "P_VALOR_TOTAL";
                        P_VALOR_TOTAL.Value = pServicio.valor_total;
                        P_VALOR_TOTAL.Direction = ParameterDirection.Input;
                        P_VALOR_TOTAL.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(P_VALOR_TOTAL);

                        DbParameter P_FECHA_PRIMERA_CUOTA = cmdTransaccionFactory.CreateParameter();
                        P_FECHA_PRIMERA_CUOTA.ParameterName = "P_FECHA_PRIMERA_CUOTA";
                        if (pServicio.fecha_primera_cuota != DateTime.MinValue && pServicio.fecha_primera_cuota != null) P_FECHA_PRIMERA_CUOTA.Value = pServicio.fecha_primera_cuota; else P_FECHA_PRIMERA_CUOTA.Value = DBNull.Value;
                        P_FECHA_PRIMERA_CUOTA.Direction = ParameterDirection.Input;
                        P_FECHA_PRIMERA_CUOTA.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(P_FECHA_PRIMERA_CUOTA);

                        DbParameter P_NUMERO_CUOTAS = cmdTransaccionFactory.CreateParameter();
                        P_NUMERO_CUOTAS.ParameterName = "P_NUMERO_CUOTAS";
                        if (pServicio.numero_cuotas != 0) P_NUMERO_CUOTAS.Value = pServicio.numero_cuotas; else P_NUMERO_CUOTAS.Value = DBNull.Value;
                        P_NUMERO_CUOTAS.Direction = ParameterDirection.Input;
                        P_NUMERO_CUOTAS.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(P_NUMERO_CUOTAS);

                        DbParameter P_OBSERVACION = cmdTransaccionFactory.CreateParameter();
                        P_OBSERVACION.ParameterName = "P_OBSERVACION";
                        if (pServicio.observaciones != null) P_OBSERVACION.Value = pServicio.observaciones; else P_OBSERVACION.Value = DBNull.Value;
                        P_OBSERVACION.Direction = ParameterDirection.Input;
                        P_OBSERVACION.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(P_OBSERVACION);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_SOLSERVICIO_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pServicio.numero_servicio = Convert.ToInt32(P_ID_SOL_SERVICIO.Value);

                        return pServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "CrearSolicitudServicioOficinaVirtual", ex);
                        return null;
                    }
                }
            }
        }


        public List<Servicio> ListarSolicitudServicio(string filtro, Usuario usuario)
        {
            DbDataReader resultado;
            List<Servicio> lstEntidad = new List<Servicio>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"select l.id_sol_servicio, l.cod_linea_servicio, l.fecha_servicio, l.valor_total, l.fecha_primera_cuota, l.numero_cuotas,
                                        l.estado, l.fecha_solicitud, l.cod_persona, l.observacion, s.nombre, v.nombreyapellido, v.identificacion, case l.ESTADO when 0 then 'Solicitado' when 1 then 'Aprobado' when 2 then 'Negado' else ' ' end as Estado_solicitud
                                        ,aj.snombre1||' '||aj.Sapellido1||' '||aj.Sapellido2 as NOMBREEJE
                                        from solicitud_servicio l
                                        inner join lineasservicios s on s.cod_linea_servicio = l.cod_linea_servicio
                                        inner join v_persona v on v.cod_persona = l.cod_persona
                                        left join Asejecutivos aj on Aj.Icodigo = v.Cod_Asesor
                                        where l.id_sol_servicio is not null " + filtro + @"                                        
                                        order by l.fecha_solicitud desc, fecha_servicio desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Servicio entidad = new Servicio();
                            if (resultado["id_sol_servicio"] != DBNull.Value) entidad.numero_servicio = Convert.ToInt32(resultado["id_sol_servicio"]);
                            if (resultado["cod_linea_servicio"] != DBNull.Value) entidad.cod_linea_servicio = Convert.ToString(resultado["cod_linea_servicio"]);
                            if (resultado["fecha_servicio"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["fecha_servicio"]);
                            if (resultado["valor_total"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["valor_total"]);
                            if (resultado["fecha_primera_cuota"] != DBNull.Value) entidad.fecha_primera_cuota = Convert.ToDateTime(resultado["fecha_primera_cuota"]);
                            if (resultado["numero_cuotas"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["numero_cuotas"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["estado"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["fecha_solicitud"]);
                            if (resultado["cod_persona"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["cod_persona"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["nombre"]);
                            if (resultado["nombreyapellido"] != DBNull.Value) entidad.nombre_titular = Convert.ToString(resultado["nombreyapellido"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["Estado_solicitud"] != DBNull.Value) entidad.descripcion_estado = Convert.ToString(resultado["Estado_solicitud"]);
                            if (resultado["observacion"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["observacion"]);
                            if (resultado["NOMBREEJE"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["NOMBREEJE"]);

                            lstEntidad.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstEntidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudServiciosData", "ListarSolicitudServicio", ex);
                        return null;
                    }
                }
            }
        }        



    }
}
