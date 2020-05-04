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
    public class AprobacionServiciosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public AprobacionServiciosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Servicio> ListarOficinas(Servicio pPerso, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Servicio> lstPerso = new List<Servicio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM Oficina " + ObtenerFiltro(pPerso) + " ORDER BY COD_OFICINA ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Servicio entidad = new Servicio();
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt32(resultado["COD_OFICINA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            lstPerso.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstPerso;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AperturaCDATData", "ListarOficinas", ex);
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

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pServicio.fecha_aprobacion != null) pfecha_aprobacion.Value = pServicio.fecha_aprobacion; else pfecha_aprobacion.Value = DBNull.Value;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter pfecha_activacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_activacion.ParameterName = "p_fecha_activacion";
                        if (pServicio.fecha_activacion != DateTime.MinValue) pfecha_activacion.Value = pServicio.fecha_activacion; else pfecha_activacion.Value = DBNull.Value;
                        pfecha_activacion.Direction = ParameterDirection.Input;
                        pfecha_activacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_activacion);

                        DbParameter pobservacionaproba = cmdTransaccionFactory.CreateParameter();
                        pobservacionaproba.ParameterName = "p_observacionaproba";
                        if (pServicio.observacionaproba != null) pobservacionaproba.Value = pServicio.observacionaproba; else pobservacionaproba.Value = DBNull.Value;
                        pobservacionaproba.Direction = ParameterDirection.Input;
                        pobservacionaproba.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservacionaproba);

                        DbParameter pcuotas_pendientes = cmdTransaccionFactory.CreateParameter();
                        pcuotas_pendientes.ParameterName = "p_cuotas_pendientes";
                        if (pServicio.cuotas_pendientes != 0) pcuotas_pendientes.Value = pServicio.cuotas_pendientes; else pcuotas_pendientes.Value = DBNull.Value;
                        pcuotas_pendientes.Direction = ParameterDirection.Input;
                        pcuotas_pendientes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_pendientes);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_APRUEBASERVI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "ModificarSolicitudServicio", ex);
                        return null;
                    }
                }
            }
        }



        public List<Servicio> ListarServicios(string filtro, string pOrden, DateTime pFechaIni, DateTime pFecPago, Usuario vUsuario, int estadoCuenta = 0)
        {
            DbDataReader resultado;
            string sql = "";
            List<Servicio> lstServicio = new List<Servicio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        if (estadoCuenta != 0)
                        {
                            sql = @"select reclamacion_servicio.fecha_reclamacion,reclamacion_servicio.fechacrea,reclamacion_servicio.identificacion_fallecido,reclamacion_servicio.nombre_fallecido,s.numero_servicio,s.fecha_solicitud,s.cod_persona, p.identificacion, "
                                  + " p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido as Nombre, p.cod_nomina ,"
                                  + " l.nombre as nom_linea,z.nombre as nom_Plan,s.num_poliza,s.fecha_inicio_vigencia,s.fecha_final_vigencia, "
                                  + " s.valor_total,s.fecha_primera_cuota,s.numero_cuotas,s.valor_cuota,n.descripcion as nom_Periodicidad, "
                                  + " case s.forma_pago when '1' then 'Caja' when '2' then 'Nomina' end as forma_pago, s.identificacion_titular,s.nombre_titular, H.saldo,H.fecha_proximo_pago,";
                        }
                        else
                        {
                            sql = @"select reclamacion_servicio.fecha_reclamacion,reclamacion_servicio.fechacrea,reclamacion_servicio.identificacion_fallecido,reclamacion_servicio.nombre_fallecido,s.numero_servicio,s.fecha_solicitud,s.cod_persona, p.identificacion, "
                                         + " p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido as Nombre, p.cod_nomina ,"
                                         + " l.nombre as nom_linea,z.nombre as nom_Plan,s.num_poliza,s.fecha_inicio_vigencia,s.fecha_final_vigencia, "
                                         + " s.valor_total,s.fecha_primera_cuota,s.numero_cuotas,s.valor_cuota,n.descripcion as nom_Periodicidad, "
                                         + " case s.forma_pago when '1' then 'Caja' when '2' then 'Nomina' end as forma_pago, s.identificacion_titular,s.nombre_titular, s.saldo,s.fecha_proximo_pago,";
                        }


                        if (pFecPago != null && pFecPago != DateTime.MinValue)
                        {
                            string FechaPago = Convert.ToDateTime(pFecPago).ToString(conf.ObtenerFormatoFecha());
                            sql += " Calcular_TotalAPagar_Servicio(s.numero_servicio, to_Date('" + FechaPago + "', '" + conf.ObtenerFormatoFecha() + "')) As total, ";
                            sql += " CALCULAR_TOTALINTERES_SERVICIO(s.numero_servicio, to_Date('" + FechaPago + "', '" + conf.ObtenerFormatoFecha() + "')) As total_interes,";
                            sql += "CALCULAR_PAGO_SERVICIO(s.numero_servicio, to_Date('" + FechaPago + "', '" + conf.ObtenerFormatoFecha() + "')) As pago_fecha ";
                        }
                        else
                        {
                            sql += " Calcular_TotalAPagar_Servicio(s.numero_servicio, SYSDATE) as total, CALCULAR_TOTALINTERES_SERVICIO(s.numero_servicio, SYSDATE) as total_interes, ";
                            sql += "0 as pago_fecha";
                        }

                        if (estadoCuenta != 0)
                        {
                            sql += @" from servicios s 
                                        INNER JOIN HISTORICO_SERVICIOS H ON H.NUMERO_SERVICIO = S.NUMERO_SERVICIO
                                        LEFT join RECLAMACION_SERVICIO on RECLAMACION_SERVICIO.numero_servicio=s.numero_servicio 
                                        LEFT join persona p on p.cod_persona = s.cod_persona 
                                        LEFT join lineasservicios l on l.cod_linea_servicio = s.cod_linea_servicio 
                                        LEFT join planservicios z on z.cod_plan_servicio=s.cod_plan_servicio 
                                        LEFT join periodicidad n on n.cod_periodicidad = s.cod_periodicidad  where 1 = 1 " + filtro;

                        }
                        else
                        {
                            sql += @" from servicios s 
                                        LEFT join RECLAMACION_SERVICIO on RECLAMACION_SERVICIO.numero_servicio=s.numero_servicio 
                                        LEFT join persona p on p.cod_persona = s.cod_persona 
                                        LEFT join lineasservicios l on l.cod_linea_servicio = s.cod_linea_servicio 
                                        LEFT join planservicios z on z.cod_plan_servicio=s.cod_plan_servicio 
                                        LEFT join periodicidad n on n.cod_periodicidad = s.cod_periodicidad  where 1 = 1 " + filtro;

                        }


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

                        if (pOrden != "")
                            sql += " ORDER BY s." + pOrden;
                        else
                            sql += " ORDER BY s.NUMERO_SERVICIO";
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
                            if (resultado["IDENTIFICACION_FALLECIDO"] != DBNull.Value) entidad.identificacion_fallecido = Convert.ToString(resultado["IDENTIFICACION_FALLECIDO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOMBRE_FALLECIDO"] != DBNull.Value) entidad.nombre_fallecido = Convert.ToString(resultado["NOMBRE_FALLECIDO"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["NOM_PLAN"] != DBNull.Value) entidad.nom_plan = Convert.ToString(resultado["NOM_PLAN"]);
                            if (resultado["FECHA_INICIO_VIGENCIA"] != DBNull.Value) entidad.fecha_inicio_vigencia = Convert.ToDateTime(resultado["FECHA_INICIO_VIGENCIA"]);
                            if (resultado["FECHA_FINAL_VIGENCIA"] != DBNull.Value) entidad.fecha_final_vigencia = Convert.ToDateTime(resultado["FECHA_FINAL_VIGENCIA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["NUM_POLIZA"] != DBNull.Value) entidad.num_poliza = Convert.ToString(resultado["NUM_POLIZA"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["VALOR_TOTAL"]);
                            if (resultado["FECHA_PRIMERA_CUOTA"] != DBNull.Value) entidad.fecha_primera_cuota = Convert.ToDateTime(resultado["FECHA_PRIMERA_CUOTA"]);
                            if (resultado["FECHACREA"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["FECHACREA"]);
                            if (resultado["FECHA_RECLAMACION"] != DBNull.Value) entidad.fecha_reclamacion = Convert.ToDateTime(resultado["FECHA_RECLAMACION"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["NOM_PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["NOM_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["IDENTIFICACION_TITULAR"] != DBNull.Value) entidad.identificacion_titular = Convert.ToString(resultado["IDENTIFICACION_TITULAR"]);
                            if (resultado["NOMBRE_TITULAR"] != DBNull.Value) entidad.nombre_titular = Convert.ToString(resultado["NOMBRE_TITULAR"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.total_calculado = Convert.ToDecimal(resultado["TOTAL"]);
                            if (resultado["total_interes"] != DBNull.Value) entidad.total_interes_calculado = Convert.ToDecimal(resultado["total_interes"]);
                            if (resultado["pago_fecha"] != DBNull.Value) entidad.interes_corriente = Convert.ToDecimal(resultado["pago_fecha"]);
                            lstServicio.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "ListarServicios", ex);
                        return null;
                    }
                }
            }
        }

        public List<Servicio> ListarServiciosClubAhorrador(Int64 pCodPersona, string pFiltro, Boolean pResult, Usuario pUsuario)
        {
            DbDataReader resultado;
            List<Servicio> lstextract = new List<Servicio>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Empty;
                        sql = @"SELECT servicios.*, lineasservicios.nombre, planservicios.nombre AS nombres, 
                                        CASE SERVICIOS.ESTADO WHEN 'S' THEN 'Solicitado' WHEN 'A' THEN 'Aprobado' WHEN 'C' THEN  'Activado' WHEN 'T' THEN 'Terminado' end as nom_estado ,
                                        CASE SERVICIOS.FORMA_PAGO WHEN '1' THEN 'Caja' WHEN '2' THEN 'Nomina' END AS NOM_FORMA_PAGO,
                                        V_PERSONA.IDENTIFICACION, V_PERSONA.NOMBRE AS NOM_PERSONA, 'PROPIO' AS TIPO_SERVICIO
                                        FROM servicios INNER JOIN lineasservicios ON lineasservicios.cod_linea_servicio=servicios.cod_linea_servicio 
                                        INNER JOIN V_PERSONA ON V_PERSONA.COD_PERSONA = servicios.cod_persona 
                                        LEFT JOIN planservicios ON servicios.cod_linea_servicio = planservicios.cod_linea_servicio
                                        WHERE servicios.cod_persona = " + pCodPersona.ToString() + pFiltro.ToString();
                        if (!pResult)
                        {
                            sql += @" UNION ALL
                                    SELECT servicios.*, lineasservicios.nombre, planservicios.nombre AS nombres, 
                                    CASE SERVICIOS.ESTADO WHEN 'S' THEN 'Solicitado' WHEN 'A' THEN 'Aprobado' WHEN 'C' THEN  'Activado' WHEN 'T' THEN 'Terminado' end as nom_estado,
                                    CASE SERVICIOS.FORMA_PAGO WHEN '1' THEN 'Caja' WHEN '2' THEN 'Nomina' END AS NOM_FORMA_PAGO,
                                    V_PERSONA.IDENTIFICACION, V_PERSONA.NOMBRE AS NOM_PERSONA,'CLUB AHORRADOR' AS TIPO_SERVICIO
                                    FROM servicios INNER JOIN lineasservicios ON lineasservicios.cod_linea_servicio=servicios.cod_linea_servicio
                                    INNER JOIN V_PERSONA ON V_PERSONA.COD_PERSONA = servicios.cod_persona 
                                    LEFT JOIN planservicios ON servicios.cod_linea_servicio = planservicios.cod_linea_servicio
                                    WHERE servicios.cod_persona IN (SELECT R.COD_PERSONA FROM PERSONA_RESPONSABLE R WHERE R.COD_PERSONA_TUTOR = " + pCodPersona.ToString() + ")" + pFiltro.ToString() + " ORDER BY 2 DESC";
                        }

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
                            if (resultado["FECHA_INICIO_VIGENCIA"] != DBNull.Value) entidad.fecha_inicio_vigencia = Convert.ToDateTime(resultado["FECHA_INICIO_VIGENCIA"]);
                            if (resultado["FECHA_FINAL_VIGENCIA"] != DBNull.Value) entidad.fecha_final_vigencia = Convert.ToDateTime(resultado["FECHA_FINAL_VIGENCIA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToInt32(resultado["VALOR_TOTAL"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToInt32(resultado["SALDO"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt32(resultado["VALOR_CUOTA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nom_plan = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["NOM_FORMA_PAGO"] != DBNull.Value) entidad.nom_forma_pago = Convert.ToString(resultado["NOM_FORMA_PAGO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOM_PERSONA"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOM_PERSONA"]);
                            if (resultado["TIPO_SERVICIO"] != DBNull.Value) entidad.tipo_registro = Convert.ToString(resultado["TIPO_SERVICIO"]);
                            if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_activacion = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            lstextract.Add(entidad);
                        }

                        return lstextract;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "ListarServiciosClubAhorrador", ex);
                        return null;
                    }
                }
            }
        }

        public List<Servicio> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Servicio> lstServicios = new List<Servicio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT S.NUMERO_SERVICIO, S.COD_PERSONA, L.NOMBRE, S.FECHA_INICIO_VIGENCIA, S.VALOR_CUOTA
                                        FROM SERVICIOS S INNER JOIN LINEASSERVICIOS L ON S.COD_LINEA_SERVICIO = L.COD_LINEA_SERVICIO WHERE S.COD_PERSONA = " + pCod_Persona;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Servicio entidad = new Servicio();
                            if (resultado["NUMERO_SERVICIO"] != DBNull.Value) entidad.numero_servicio = Convert.ToInt32(resultado["NUMERO_SERVICIO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_INICIO_VIGENCIA"] != DBNull.Value) entidad.fecha_inicio_vigencia = Convert.ToDateTime(resultado["FECHA_INICIO_VIGENCIA"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToInt64(resultado["VALOR_CUOTA"]);

                            lstServicios.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstServicios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "ListarCuentasPersona", ex);
                        return null;
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
                        string sql = @"SELECT SERVICIOS.*, V_PERSONA.IDENTIFICACION, V_PERSONA.NOMBRE AS NOMBRE , "
                                        + "LINEASSERVICIOS.NOMBRE AS NOM_LINEA, PLANSERVICIOS.NOMBRE AS NOM_PLAN , "
                                        + "PERIODICIDAD.DESCRIPCION AS NOM_PERIODICIDAD, "
                                        + "CASE SERVICIOS.FORMA_PAGO WHEN '1' THEN 'Caja' WHEN '2' THEN 'Nomina' END AS NOM_FORMA_PAGO,V_PERSONA.TIPO_IDENTIFICACION,CIUDADES.NOMCIUDAD, "
                                        + "SERVICIOS.COD_PROVEEDOR, SERVICIOS.IDENTIFICACION_PROVEEDOR, SERVICIOS.NOMBRE_PROVEEDOR "
                                        + "FROM SERVICIOS INNER JOIN V_PERSONA ON V_PERSONA.COD_PERSONA = SERVICIOS.COD_PERSONA "
                                        + "LEFT JOIN LineasServicios ON servicios.cod_linea_servicio = LineasServicios.cod_linea_servicio "
                                        + "LEFT JOIN PLANSERVICIOS ON SERVICIOS.COD_PLAN_SERVICIO = PLANSERVICIOS.COD_PLAN_SERVICIO "
                                        + "LEFT join periodicidad on periodicidad.cod_periodicidad = SERVICIOS.cod_periodicidad "
                                        + "LEFT join CIUDADES on CIUDADES.CODCIUDAD = V_PERSONA.CODCIUDADRESIDENCIA "
                                        + "WHERE servicios.numero_servicio = " + pId.ToString();

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
                            if (resultado["COD_PLAN_SERVICIO"] != DBNull.Value) entidad.cod_plan_servicio = Convert.ToString(resultado["COD_PLAN_SERVICIO"]);
                            if (resultado["FECHA_INICIO_VIGENCIA"] != DBNull.Value) entidad.Fec_ini = Convert.ToDateTime(resultado["FECHA_INICIO_VIGENCIA"]);
                            if (resultado["FECHA_FINAL_VIGENCIA"] != DBNull.Value) entidad.Fec_fin = Convert.ToDateTime(resultado["FECHA_FINAL_VIGENCIA"]);
                            if (resultado["NUM_POLIZA"] != DBNull.Value) entidad.num_poliza = Convert.ToString(resultado["NUM_POLIZA"]);
                            if (resultado["VALOR_TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToDecimal(resultado["VALOR_TOTAL"]);
                            if (resultado["FECHA_PRIMERA_CUOTA"] != DBNull.Value) entidad.Fec_1Pago = Convert.ToDateTime(resultado["FECHA_PRIMERA_CUOTA"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToInt32(resultado["COD_PERIODICIDAD"]);
                            if (resultado["FORMA_PAGO"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["FORMA_PAGO"]);
                            if (resultado["IDENTIFICACION_TITULAR"] != DBNull.Value) entidad.identificacion_titular = Convert.ToString(resultado["IDENTIFICACION_TITULAR"]);
                            if (resultado["NOMBRE_TITULAR"] != DBNull.Value) entidad.nombre_titular = Convert.ToString(resultado["NOMBRE_TITULAR"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["FECHA_ULTIMO_PAGO"] != DBNull.Value) entidad.fecha_ultimo_pago = Convert.ToDateTime(resultado["FECHA_ULTIMO_PAGO"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_ACTIVACION"] != DBNull.Value) entidad.fecha_activacion = Convert.ToDateTime(resultado["FECHA_ACTIVACION"]);
                            if (resultado["OBSERVACIONAPROBA"] != DBNull.Value) entidad.observacionaproba = Convert.ToString(resultado["OBSERVACIONAPROBA"]);
                            if (resultado["CUOTAS_PENDIENTES"] != DBNull.Value) entidad.cuotas_pendientes = Convert.ToInt32(resultado["CUOTAS_PENDIENTES"]);
                            if (resultado["NUMERO_PREIMPRESO"] != DBNull.Value) entidad.numero_preimpreso = Convert.ToInt64(resultado["NUMERO_PREIMPRESO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["NOM_LINEA"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA"]);
                            if (resultado["NOM_PLAN"] != DBNull.Value) entidad.nom_plan = Convert.ToString(resultado["NOM_PLAN"]);
                            if (resultado["NOM_PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["NOM_PERIODICIDAD"]);
                            if (resultado["NOM_FORMA_PAGO"] != DBNull.Value) entidad.nom_forma_pago = Convert.ToString(resultado["NOM_FORMA_PAGO"]);
                            if (resultado["TIPO_IDENTIFICACION"] != DBNull.Value) entidad.tipo_identificacion = Convert.ToInt32(resultado["TIPO_IDENTIFICACION"]);
                            if (resultado["NOMCIUDAD"] != DBNull.Value) entidad.nom_ciudad = Convert.ToString(resultado["NOMCIUDAD"]);

                            if (resultado["COD_PROVEEDOR"] != DBNull.Value) entidad.codigo_proveedor = Convert.ToInt64(resultado["COD_PROVEEDOR"]);
                            if (resultado["IDENTIFICACION_PROVEEDOR"] != DBNull.Value) entidad.identificacion_proveedor = Convert.ToString(resultado["IDENTIFICACION_PROVEEDOR"]);
                            if (resultado["NOMBRE_PROVEEDOR"] != DBNull.Value) entidad.nombre_proveedor = Convert.ToString(resultado["NOMBRE_PROVEEDOR"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "ConsultarSERVICIO", ex);
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
                        BOExcepcion.Throw("AprobacionServiciosData", "CrearDetalleServicio", ex);
                        return null;
                    }
                }
            }
        }




        public List<Servicio> Reportemovimiento(Servicio pAhorroVista, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Servicio> lstProvision = new List<Servicio>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter cod_linea_ahorro = cmdTransaccionFactory.CreateParameter();
                        cod_linea_ahorro.ParameterName = "P_NUMERO_SERVICIO";
                        cod_linea_ahorro.Direction = ParameterDirection.Input;
                        cod_linea_ahorro.Value = pAhorroVista.numero_servicio;
                        cod_linea_ahorro.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(cod_linea_ahorro);

                        DbParameter fecha_apertura = cmdTransaccionFactory.CreateParameter();
                        fecha_apertura.ParameterName = "P_FECHA_INICIAL";
                        fecha_apertura.Value = pAhorroVista.Fec_ini;
                        fecha_apertura.Direction = ParameterDirection.Input;
                        fecha_apertura.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(fecha_apertura);

                        DbParameter fecha_cierre = cmdTransaccionFactory.CreateParameter();
                        fecha_cierre.ParameterName = "P_FECHA_FINAL";
                        fecha_cierre.Direction = ParameterDirection.Input;
                        fecha_cierre.Value = pAhorroVista.Fec_fin;
                        fecha_cierre.DbType = DbType.Date;
                        cmdTransaccionFactory.Parameters.Add(fecha_cierre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_MOVIMIENTO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        cmdTransaccionFactory.Dispose();
                        cmdTransaccionFactory.Parameters.Clear();
                        string sql = "Select TEMP_MOVIMIENTOS.* From TEMP_MOVIMIENTOS ORDER BY FECHA_PAGO, cod_ope";
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Servicio entidad = new Servicio();

                            if (resultado["FECHA_PAGO"] != DBNull.Value) entidad.Fec_1Pago = Convert.ToDateTime(resultado["FECHA_PAGO"].ToString());
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_persona = Convert.ToInt32(resultado["COD_OPE"].ToString());
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt32(resultado["TIPO_COMP"]);
                            if (resultado["TIPO_MOV"] != DBNull.Value) entidad.tipo_mov = Convert.ToString(resultado["TIPO_MOV"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToInt32(resultado["SALDO"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.numero_tran = Convert.ToInt32(resultado["COD_OPE"]);
                            if (resultado["TIPO_OPE"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["TIPO_OPE"]);
                            if (resultado["TOTAL"] != DBNull.Value) entidad.valor_total = Convert.ToInt32(resultado["TOTAL"]);
                            if (resultado["INTCTE"] != DBNull.Value) entidad.interes_corriente = Convert.ToDecimal(resultado["INTCTE"]);
                            if (resultado["capital"] != DBNull.Value) entidad.capital = Convert.ToDecimal(resultado["capital"]);

                            lstProvision.Add(entidad);
                        }
                        return lstProvision;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AhorroVistaData", "ReportePeriodico", ex);
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
                        BOExcepcion.Throw("AprobacionServiciosData", "ModificarDetalleServicio", ex);
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
                        BOExcepcion.Throw("AprobacionServiciosData", "EliminarDETALLESERVICIO", ex);
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
                        string sql = @"SELECT SERVICIO_BENEFICIARIOS.*,PARENTESCOS.DESCRIPCION,RECLAMACION_SERVICIO.NOMBRE_FALLECIDO "
                                        + "FROM SERVICIO_BENEFICIARIOS LEFT JOIN PARENTESCOS "
                                        + "ON PARENTESCOS.CODPARENTESCO = SERVICIO_BENEFICIARIOS.CODPARENTESCO "
                                        + "LEFT JOIN RECLAMACION_SERVICIO ON SERVICIO_BENEFICIARIOS.NUMERO_SERVICIO = RECLAMACION_SERVICIO.NUMERO_SERVICIO "
                                        + "WHERE SERVICIO_BENEFICIARIOS.NUMERO_SERVICIO = " + pId.ToString();
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
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["NOMBRE_FALLECIDO"] != DBNull.Value) entidad.nombre_fallecido = Convert.ToString(resultado["NOMBRE_FALLECIDO"]);

                            LstDetalle.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return LstDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "ConsultarDETALLESERVICIO", ex);
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
                        BOExcepcion.Throw("AprobacionServiciosData", "CargarPlanXLinea", ex);
                        return null;
                    }
                }
            }
        }



        //AGREGADO

        public CONTROLSERVICIOS CrearControlServicios(CONTROLSERVICIOS pControl, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcontrol_servicios = cmdTransaccionFactory.CreateParameter();
                        pidcontrol_servicios.ParameterName = "p_idcontrol_servicios";
                        pidcontrol_servicios.Value = pControl.idcontrol_servicios;
                        pidcontrol_servicios.Direction = ParameterDirection.Output;
                        pidcontrol_servicios.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidcontrol_servicios);

                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pControl.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pcodtipo_proceso = cmdTransaccionFactory.CreateParameter();
                        pcodtipo_proceso.ParameterName = "p_codtipo_proceso";
                        pcodtipo_proceso.Value = pControl.codtipo_proceso;
                        pcodtipo_proceso.Direction = ParameterDirection.Input;
                        pcodtipo_proceso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodtipo_proceso);

                        DbParameter pfechaproceso = cmdTransaccionFactory.CreateParameter();
                        pfechaproceso.ParameterName = "p_fechaproceso";
                        pfechaproceso.Value = pControl.fechaproceso;
                        pfechaproceso.Direction = ParameterDirection.Input;
                        pfechaproceso.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaproceso);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        pcod_usuario.Value = vUsuario.codusuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_CONTROLSER_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pControl.idcontrol_servicios = Convert.ToInt32(pidcontrol_servicios.Value);
                        return pControl;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "CrearControlServicios", ex);
                        return null;
                    }
                }
            }
        }



        public CONTROLSERVICIOS ConsultarControlServicio(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            CONTROLSERVICIOS entidad = new CONTROLSERVICIOS();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM controlservicios WHERE NUMERO_SERVICIO =" + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDCONTROL_SERVICIOS"] != DBNull.Value) entidad.idcontrol_servicios = Convert.ToInt32(resultado["IDCONTROL_SERVICIOS"]);
                            if (resultado["NUMERO_SERVICIO"] != DBNull.Value) entidad.numero_servicio = Convert.ToInt32(resultado["NUMERO_SERVICIO"]);
                            if (resultado["CODTIPO_PROCESO"] != DBNull.Value) entidad.codtipo_proceso = Convert.ToInt32(resultado["CODTIPO_PROCESO"]);
                            if (resultado["FECHAPROCESO"] != DBNull.Value) entidad.fechaproceso = Convert.ToDateTime(resultado["FECHAPROCESO"]);
                            if (resultado["COD_USUARIO"] != DBNull.Value) entidad.cod_usuario = Convert.ToInt32(resultado["COD_USUARIO"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "ConsultarControlServicio", ex);
                        return null;
                    }
                }
            }
        }


        public Servicio AprobarSolicitud(Servicio pServicio, Usuario vUsuario)
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
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pServicio.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pfecha_aprobacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aprobacion.ParameterName = "p_fecha_aprobacion";
                        if (pServicio.fecha_aprobacion != null) pfecha_aprobacion.Value = pServicio.fecha_aprobacion; else pfecha_aprobacion.Value = DBNull.Value;
                        pfecha_aprobacion.Direction = ParameterDirection.Input;
                        pfecha_aprobacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_aprobacion);

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        psaldo.Value = pServicio.saldo;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pcuotas_pendientes = cmdTransaccionFactory.CreateParameter();
                        pcuotas_pendientes.ParameterName = "p_cuotas_pendientes";
                        pcuotas_pendientes.Value = pServicio.cuotas_pendientes;
                        pcuotas_pendientes.Direction = ParameterDirection.Input;
                        pcuotas_pendientes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_pendientes);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_ESTADOAPROBA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "AprobarSolicitud", ex);
                        return null;
                    }
                }
            }
        }



        public Servicio ModificarServiciosActivos(Servicio pServicio, Usuario vUsuario)
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
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

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
                        pcod_periodicidad.Value = pServicio.cod_periodicidad;
                        pcod_periodicidad.Direction = ParameterDirection.Input;
                        pcod_periodicidad.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_periodicidad);

                        DbParameter pforma_pago = cmdTransaccionFactory.CreateParameter();
                        pforma_pago.ParameterName = "p_forma_pago";
                        pforma_pago.Value = pServicio.forma_pago;
                        pforma_pago.Direction = ParameterDirection.Input;
                        pforma_pago.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pforma_pago);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_SERVICACTI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "ModificarServiciosActivos", ex);
                        return null;
                    }
                }
            }
        }




        public Servicio ModificarServiciosExcluidos(Servicio pServicio, Usuario vUsuario)
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
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pcuotas_no_excluir = cmdTransaccionFactory.CreateParameter();
                        pcuotas_no_excluir.ParameterName = "p_cuotas_no_excluir";
                        if (pServicio.numero_cuotas != 0) pcuotas_no_excluir.Value = pServicio.numero_cuotas; else pcuotas_no_excluir.Value = DBNull.Value;
                        pcuotas_no_excluir.Direction = ParameterDirection.Input;
                        pcuotas_no_excluir.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_no_excluir);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pServicio.valor_cuota != 0) pvalor.Value = pServicio.valor_cuota; else pvalor.Value = DBNull.Value;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pServicio.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_EXCLUIR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "ModificarServiciosExcluidos", ex);
                        return null;
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
                        BOExcepcion.Throw("AprobacionServiciosData", "CrearExclusionServicios", ex);
                        return null;
                    }
                }
            }
        }


        public Int64 ObtenerNumeroPreImpreso(Usuario pUsuario)
        {
            Int64 resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "select max(NUMERO_PREIMPRESO) + 1 from SERVICIOS";

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
                        BOExcepcion.Throw("AprobacionServiciosData", "ObtenerNumeroPreImpreso", ex);
                        return -1;
                    }
                }
            }
        }

        public Servicio CrearTranServicios(Servicio pControl, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_numero_transaccion = cmdTransaccionFactory.CreateParameter();
                        p_numero_transaccion.ParameterName = "p_numero_transaccion";
                        p_numero_transaccion.Value = pControl.numero_tran;
                        p_numero_transaccion.Direction = ParameterDirection.Output;
                        p_numero_transaccion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_numero_transaccion);

                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pControl.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter p_cod_ope = cmdTransaccionFactory.CreateParameter();
                        p_cod_ope.ParameterName = "p_cod_ope";
                        p_cod_ope.Value = pControl.cod_ope;
                        p_cod_ope.Direction = ParameterDirection.Input;
                        p_cod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_ope);

                        DbParameter p_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        p_cod_cliente.ParameterName = "p_cod_cliente";
                        p_cod_cliente.Value = pControl.cod_cliente;
                        p_cod_cliente.Direction = ParameterDirection.Input;
                        p_cod_cliente.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_cod_cliente);

                        DbParameter p_linea_servicio = cmdTransaccionFactory.CreateParameter();
                        p_linea_servicio.ParameterName = "p_linea_servicio";
                        p_linea_servicio.Value = pControl.cod_linea_servicio;
                        p_linea_servicio.Direction = ParameterDirection.Input;
                        p_linea_servicio.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_linea_servicio);

                        DbParameter p_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        p_tipo_tran.ParameterName = "p_tipo_tran";
                        p_tipo_tran.Value = 1;
                        p_tipo_tran.Direction = ParameterDirection.Input;
                        p_tipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_tipo_tran);

                        DbParameter p_cod_det_lis = cmdTransaccionFactory.CreateParameter();
                        p_cod_det_lis.ParameterName = "p_cod_det_lis";
                        p_cod_det_lis.Value = DBNull.Value;
                        p_cod_det_lis.Direction = ParameterDirection.Input;
                        p_cod_det_lis.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_cod_det_lis);

                        DbParameter p_cod_atr = cmdTransaccionFactory.CreateParameter();
                        p_cod_atr.ParameterName = "p_cod_atr";
                        p_cod_atr.Value = 1;
                        p_cod_atr.Direction = ParameterDirection.Input;
                        p_cod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_cod_atr);

                        DbParameter p_valor = cmdTransaccionFactory.CreateParameter();
                        p_valor.ParameterName = "p_valor";
                        p_valor.Value = pControl.valor_total;
                        p_valor.Direction = ParameterDirection.Input;
                        p_valor.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_valor);

                        DbParameter p_estado = cmdTransaccionFactory.CreateParameter();
                        p_estado.ParameterName = "p_estado";
                        p_estado.Value = 1;
                        p_estado.Direction = ParameterDirection.Input;
                        p_estado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_estado);

                        DbParameter p_num_tran_anula = cmdTransaccionFactory.CreateParameter();
                        p_num_tran_anula.ParameterName = "p_num_tran_anula";
                        p_num_tran_anula.Value = DBNull.Value;
                        p_num_tran_anula.Direction = ParameterDirection.Input;
                        p_num_tran_anula.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_num_tran_anula);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_TRANSER_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);


                        pControl.numero_tran = Convert.ToInt32(p_numero_transaccion.Value);
                        return pControl;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "CrearControlServicios", ex);
                        return null;
                    }
                }
            }
        }

        public string CancelarServicio(Int64 NumServicio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = NumServicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pmensaje = cmdTransaccionFactory.CreateParameter();
                        pmensaje.ParameterName = "p_mensaje";
                        pmensaje.Value = DBNull.Value;
                        pmensaje.Direction = ParameterDirection.Output;
                        pmensaje.DbType = DbType.String;
                        pmensaje.Size = 100;
                        cmdTransaccionFactory.Parameters.Add(pmensaje);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_IND_CANCELAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        string mensaje = pmensaje.Value.ToString();
                        return mensaje;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AprobacionServiciosData", "CancelarServicio", ex);
                        return "";
                    }
                }
            }
        }

    }
}
