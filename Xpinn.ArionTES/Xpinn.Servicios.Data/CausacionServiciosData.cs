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
    public class CausacionServiciosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CausacionServiciosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
               
        
        public List<Servicio> ListarServiciosCausacion(string filtro, DateTime pFechaCausa, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Servicio> lstServicio = new List<Servicio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                    
                        string sql = @"SELECT S.NUMERO_SERVICIO, S.FECHA_APROBACION, S.FECHA_PROXIMO_PAGO, S.COD_PERSONA, P.IDENTIFICACION, P.NOMBRE AS NOMBRE, S.SALDO, L.NOMBRE AS NOM_LINEA, Z.NOMBRE AS NOM_PLAN,
                                        S.NUM_POLIZA, S.FECHA_INICIO_VIGENCIA, S.FECHA_FINAL_VIGENCIA, S.VALOR_TOTAL, S.FECHA_PRIMERA_CUOTA, S.NUMERO_CUOTAS, S.VALOR_CUOTA, N.DESCRIPCION AS NOM_PERIODICIDAD, 
                                        CASE S.FORMA_PAGO WHEN '1' THEN 'CAJA' WHEN '2' THEN 'NOMINA' END AS FORMA_PAGO, L.IDENTIFICACION_PROVEEDOR, L.NOMBRE_PROVEEDOR
                                        FROM SERVICIOS s LEFT JOIN V_PERSONA p ON p.COD_PERSONA = s.COD_PERSONA 
                                        LEFT JOIN LINEASSERVICIOS l ON l.COD_LINEA_SERVICIO = s.COD_LINEA_SERVICIO 
                                        LEFT JOIN PLANSERVICIOS Z ON Z.COD_PLAN_SERVICIO = S.COD_PLAN_SERVICIO 
                                        LEFT JOIN PERIODICIDAD N ON N.COD_PERIODICIDAD = S.COD_PERIODICIDAD
                                        WHERE P.ESTADO = 'A' AND S.SALDO >= 0 
                                        AND S.NUMERO_SERVICIO NOT IN (SELECT COD_SERVICIO_ADICIONALES FROM LINEAS_TELEFONICAS WHERE COD_SERVICIO_ADICIONALES = S.NUMERO_SERVICIO AND ESTADO = 'A')                                   
                                        AND S.NUMERO_CUOTAS = 1 AND S.VALOR_TOTAL = S.VALOR_CUOTA AND L.MANEJA_CAUSACION = 1
                                        AND S.FECHA_INICIO_VIGENCIA < DATEPRIMERDIADELMES(TO_DATE('" + Convert.ToDateTime(pFechaCausa).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"'))
                                        AND P.COD_PERSONA NOT IN(SELECT COD_PERSONA FROM VACACIONES WHERE TO_DATE('" + Convert.ToDateTime(pFechaCausa).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') >=  FECHA_INICIAL 
                                                                    AND TO_DATE('" + Convert.ToDateTime(pFechaCausa).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"') <= FECHA_FINAL AND FECHA_FINAL > LAST_DAY(TO_DATE('" + Convert.ToDateTime(pFechaCausa).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"'))) 
                                        AND S.NUMERO_SERVICIO NOT IN (SELECT C.NUMERO_SERVICIO FROM CAUSACION_SERVICIO C WHERE C.NUMERO_SERVICIO = S.NUMERO_SERVICIO "
                                     ;
                        if (pFechaCausa != null && pFechaCausa != DateTime.MinValue)
                        {
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                            {
                                sql += " AND C.FECHA_CAUSACION = TO_DATE('" + Convert.ToDateTime(pFechaCausa).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')) ";
                                //Se elimino rango de fechas Vigencia indicado por CLAUDIA
                            }
                            else
                            {
                                sql += " AND C.FECHA_CAUSACION = '" + Convert.ToDateTime(pFechaCausa).ToString(conf.ObtenerFormatoFecha()) + "') ";
                                //Se elimino rango de fechas Vigencia indicado por CLAUDIA
                            }
                        }

                        sql += " AND((L.SERVICIO_TELEFONIA = 1 AND S.NUMERO_SERVICIO IN(SELECT COD_SERVICIO_FIJO FROM LINEAS_TELEFONICAS WHERE COD_SERVICIO_FIJO = S.NUMERO_SERVICIO AND ESTADO = 'A') OR L.SERVICIO_TELEFONIA = 0 OR L.SERVICIO_TELEFONIA IS NULL))";

                        if (filtro != "")
                        {
                            sql += filtro.ToString();
                        }
                      

                        sql += " ORDER BY S.NUMERO_SERVICIO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Servicio entidad = new Servicio();
                            if (resultado["NUMERO_SERVICIO"] != DBNull.Value) entidad.numero_servicio = Convert.ToInt32(resultado["NUMERO_SERVICIO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);                            
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);                          
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
                            if (resultado["IDENTIFICACION_PROVEEDOR"] != DBNull.Value) entidad.identificacion_proveedor = Convert.ToString(resultado["IDENTIFICACION_PROVEEDOR"]);
                            if (resultado["NOMBRE_PROVEEDOR"] != DBNull.Value) entidad.nombre_proveedor = Convert.ToString(resultado["NOMBRE_PROVEEDOR"]);
                           // if (resultado["servicio_telefonia"] != DBNull.Value) entidad.servicio_telefonia = Convert.ToInt64(resultado["servicio_telefonia"]);
                            lstServicio.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CausacionServiciosData", "ListarServicios", ex);
                        return null;
                    }
                }
            }
        }


        public CausacionServicios CrearCausacionServicios(CausacionServicios pCausacionServicios, ref string pError, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidcausacion = cmdTransaccionFactory.CreateParameter();
                        pidcausacion.ParameterName = "p_idcausacion";
                        pidcausacion.Value = pCausacionServicios.idcausacion;
                        pidcausacion.Direction = ParameterDirection.Input;
                        pidcausacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pidcausacion);

                        DbParameter pfecha_causacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_causacion.ParameterName = "p_fecha_causacion";
                        pfecha_causacion.Value = pCausacionServicios.fecha_causacion;
                        pfecha_causacion.Direction = ParameterDirection.Input;
                        pfecha_causacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_causacion);

                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pCausacionServicios.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pvalor_causado = cmdTransaccionFactory.CreateParameter();
                        pvalor_causado.ParameterName = "p_valor_causado";
                        pvalor_causado.Value = pCausacionServicios.valor_causado;
                        pvalor_causado.Direction = ParameterDirection.Input;
                        pvalor_causado.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_causado);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        if (pCausacionServicios.codusuario == null)
                            pcodusuario.Value = DBNull.Value;
                        else
                            pcodusuario.Value = pCausacionServicios.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        if (pCausacionServicios.cod_ope == null)
                            pcod_ope.Value = DBNull.Value;
                        else
                            pcod_ope.Value = pCausacionServicios.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pmensajeError = cmdTransaccionFactory.CreateParameter();
                        pmensajeError.ParameterName = "p_mensajeError";
                        pmensajeError.Value = DBNull.Value;
                        pmensajeError.Direction = ParameterDirection.Output;
                        pmensajeError.Size = 8000;
                        pmensajeError.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pmensajeError);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_CAUSACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        pError = Convert.ToString(pmensajeError.Value);
                        if (!string.IsNullOrEmpty(pError))
                            return null;
                        return pCausacionServicios;
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                }
            }
        }




        public List<Servicio> ListarServiciosRenovacion(string filtro, DateTime pFechaIni,DateTime pFechaFin, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<Servicio> lstServicio = new List<Servicio>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT S.NUMERO_SERVICIO,S.FECHA_APROBACION,S.FECHA_PROXIMO_PAGO,S.COD_PERSONA, P.IDENTIFICACION, 
                                        P.NOMBRE as NOMBRE, S.SALDO,
                                        l.nombre as nom_linea,z.nombre as nom_Plan,s.num_poliza,s.fecha_inicio_vigencia,s.fecha_final_vigencia, 
                                        s.valor_total,s.fecha_primera_cuota,s.numero_cuotas,s.valor_cuota,n.descripcion as nom_Periodicidad, 
                                        case s.forma_pago when '1' then 'Caja' when '2' then 'Nomina' end as forma_pago, 
                                        L.IDENTIFICACION_PROVEEDOR,L.NOMBRE_PROVEEDOR
                                        from servicios s LEFT join v_persona p on p.cod_persona = s.cod_persona 
                                        LEFT join lineasservicios l on l.cod_linea_servicio = s.cod_linea_servicio 
                                        LEFT JOIN PLANSERVICIOS Z ON Z.COD_PLAN_SERVICIO=S.COD_PLAN_SERVICIO 
                                        LEFT JOIN PERIODICIDAD N ON N.COD_PERIODICIDAD = S.COD_PERIODICIDAD ";
                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " S.FECHA_FINAL_VIGENCIA >= To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " S.FECHA_FINAL_VIGENCIA >= '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }
                        if (pFechaFin != null && pFechaFin != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " S.FECHA_FINAL_VIGENCIA <= To_Date('" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " S.FECHA_FINAL_VIGENCIA <= '" + Convert.ToDateTime(pFechaFin).ToString(conf.ObtenerFormatoFecha()) + "' ";
                        }

                        if (filtro != "")
                        {
                            sql += filtro.ToString();
                        }

                        sql += " ORDER BY S.NUMERO_SERVICIO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            Servicio entidad = new Servicio();
                            if (resultado["NUMERO_SERVICIO"] != DBNull.Value) entidad.numero_servicio = Convert.ToInt32(resultado["NUMERO_SERVICIO"]);
                            if (resultado["FECHA_APROBACION"] != DBNull.Value) entidad.fecha_aprobacion = Convert.ToDateTime(resultado["FECHA_APROBACION"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]);
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
                            if (resultado["IDENTIFICACION_PROVEEDOR"] != DBNull.Value) entidad.identificacion_proveedor = Convert.ToString(resultado["IDENTIFICACION_PROVEEDOR"]);
                            if (resultado["NOMBRE_PROVEEDOR"] != DBNull.Value) entidad.nombre_proveedor = Convert.ToString(resultado["NOMBRE_PROVEEDOR"]);
                            lstServicio.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CausacionServiciosData", "ListarServiciosRenovacion", ex);
                        return null;
                    }
                }
            }
        }


        public RenovacionServicios CrearRenovacionServicios(RenovacionServicios pRenovacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pidrenovacion = cmdTransaccionFactory.CreateParameter();
                        pidrenovacion.ParameterName = "p_idrenovacion";
                        pidrenovacion.Value = pRenovacion.idrenovacion;
                        pidrenovacion.Direction = ParameterDirection.Output;
                        pidrenovacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pidrenovacion);

                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pRenovacion.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pfecha_renovacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_renovacion.ParameterName = "p_fecha_renovacion";
                        if (pRenovacion.fecha_renovacion == null)
                            pfecha_renovacion.Value = DBNull.Value;
                        else
                            pfecha_renovacion.Value = pRenovacion.fecha_renovacion;
                        pfecha_renovacion.Direction = ParameterDirection.Input;
                        pfecha_renovacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_renovacion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pRenovacion.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pfecha_inicial_vigencia = cmdTransaccionFactory.CreateParameter();
                        pfecha_inicial_vigencia.ParameterName = "p_fecha_inicial_vigencia";
                        if (pRenovacion.fecha_inicial_vigencia == null)
                            pfecha_inicial_vigencia.Value = DBNull.Value;
                        else
                            pfecha_inicial_vigencia.Value = pRenovacion.fecha_inicial_vigencia;
                        pfecha_inicial_vigencia.Direction = ParameterDirection.Input;
                        pfecha_inicial_vigencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_inicial_vigencia);

                        DbParameter pfecha_final_vigencia = cmdTransaccionFactory.CreateParameter();
                        pfecha_final_vigencia.ParameterName = "p_fecha_final_vigencia";
                        if (pRenovacion.fecha_final_vigencia == null)
                            pfecha_final_vigencia.Value = DBNull.Value;
                        else
                            pfecha_final_vigencia.Value = pRenovacion.fecha_final_vigencia;
                        pfecha_final_vigencia.Direction = ParameterDirection.Input;
                        pfecha_final_vigencia.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_final_vigencia);

                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        if (pRenovacion.valor_total == null)
                            pvalor_total.Value = DBNull.Value;
                        else
                            pvalor_total.Value = pRenovacion.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);

                        DbParameter pvalor_cuota = cmdTransaccionFactory.CreateParameter();
                        pvalor_cuota.ParameterName = "p_valor_cuota";
                        if (pRenovacion.valor_cuota == null)
                            pvalor_cuota.Value = DBNull.Value;
                        else
                            pvalor_cuota.Value = pRenovacion.valor_cuota;
                        pvalor_cuota.Direction = ParameterDirection.Input;
                        pvalor_cuota.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_cuota);

                        DbParameter pplazo = cmdTransaccionFactory.CreateParameter();
                        pplazo.ParameterName = "p_plazo";
                        if (pRenovacion.plazo == null)
                            pplazo.Value = DBNull.Value;
                        else
                            pplazo.Value = pRenovacion.plazo;
                        pplazo.Direction = ParameterDirection.Input;
                        pplazo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pplazo);

                        DbParameter pcod_usuario = cmdTransaccionFactory.CreateParameter();
                        pcod_usuario.ParameterName = "p_cod_usuario";
                        if (pRenovacion.cod_usuario == null)
                            pcod_usuario.Value = DBNull.Value;
                        else
                            pcod_usuario.Value = pRenovacion.cod_usuario;
                        pcod_usuario.Direction = ParameterDirection.Input;
                        pcod_usuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_usuario);

                        DbParameter ptipo = cmdTransaccionFactory.CreateParameter();
                        ptipo.ParameterName = "p_tipo";
                        ptipo.Value = pRenovacion.tipo;
                        ptipo.Direction = ParameterDirection.Input;
                        ptipo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_RENOVACION_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pRenovacion.idrenovacion = Convert.ToInt32(pidrenovacion.Value);
                        return pRenovacion;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CausacionServiciosData", "CrearRenovacionServicios", ex);
                        return null;
                    }
                }
            }
        }



        public Servicio ModificarRenovacionServicio(Servicio pServicio, Usuario vUsuario)
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

                        DbParameter pvalor_total = cmdTransaccionFactory.CreateParameter();
                        pvalor_total.ParameterName = "p_valor_total";
                        pvalor_total.Value = pServicio.valor_total;
                        pvalor_total.Direction = ParameterDirection.Input;
                        pvalor_total.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor_total);

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

                        DbParameter psaldo = cmdTransaccionFactory.CreateParameter();
                        psaldo.ParameterName = "p_saldo";
                        if (pServicio.saldo != 0) psaldo.Value = pServicio.saldo; else psaldo.Value = DBNull.Value;
                        psaldo.Direction = ParameterDirection.Input;
                        psaldo.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldo);

                        DbParameter pcuotas_pendientes = cmdTransaccionFactory.CreateParameter();
                        pcuotas_pendientes.ParameterName = "p_cuotas_pendientes";
                        if (pServicio.cuotas_pendientes != 0) pcuotas_pendientes.Value = pServicio.cuotas_pendientes; else pcuotas_pendientes.Value = DBNull.Value;
                        pcuotas_pendientes.Direction = ParameterDirection.Input;
                        pcuotas_pendientes.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcuotas_pendientes);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_RENOVSERVI_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return pServicio;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CausacionServiciosData", "ModificarRenovacionServicio", ex);
                        return null;
                    }
                }
            }
        }

        public bool ValidarCausacion(DateTime fechaCausacion, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                DbDataReader resultado;
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT COUNT(DISTINCT l.cod_linea_servicio) AS CANTIDAD FROM causacion_servicio c 
                                        INNER JOIN servicios s ON c.numero_servicio = s.numero_servicio
                                        INNER JOIN lineasservicios l ON s.cod_linea_servicio = l.cod_linea_servicio
                                        WHERE c.fecha_causacion = TO_DATE('" + fechaCausacion.ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + @"')
                                        AND l.maneja_causacion = 1";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        Int64 cant_lineas_causadas = 0;
                        Int64 cant_lineas = 0;
                        if (resultado.Read())
                        {                            
                            
                        }

                        if (resultado.Read())
                        {
                            if (resultado["CANTIDAD"] != DBNull.Value) cant_lineas_causadas = Convert.ToInt64(resultado["CANTIDAD"]);
                            if (cant_lineas_causadas > 0 )
                            {
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = @"SELECT COUNT (DISTINCT l.cod_linea_servicio) AS CANTIDAD FROM LINEASSERVICIOS l INNER JOIN SERVICIOS s ON l.cod_linea_servicio = s.cod_linea_servicio 
                                                                        WHERE ESTADO = 'C' AND valor_cuota > 0; and l.maneja_causacion = 1";
                                resultado = cmdTransaccionFactory.ExecuteReader();

                                if (resultado.Read())
                                {
                                    if (resultado["CANTIDAD"] != DBNull.Value) cant_lineas = Convert.ToInt64(resultado["CANTIDAD"]);
                                }
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);

                        if (cant_lineas_causadas == cant_lineas && cant_lineas_causadas != 0 && cant_lineas != 0)
                            return true;
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CausacionServiciosData", "ValidarCausacion", ex);
                        return false;
                    }
                }
            }
        }

        //Agregado para validar que la línea de servicio no haya sido causada en el mismo mes
        public string ValidarCausacionXFecha(DateTime fechaCausacion, Int64 cod_linea_servicio, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                DbDataReader resultado;
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql = @"SELECT EXTRACT(MONTH FROM MAX(FECHA_CAUSACION)) AS MES,  EXTRACT(YEAR FROM MAX(FECHA_CAUSACION)) AS ANIO  FROM CAUSACION_SERVICIO C
                                        INNER JOIN SERVICIOS S ON C.NUMERO_SERVICIO = S.NUMERO_SERVICIO
                                        WHERE S.COD_LINEA_SERVICIO = " + cod_linea_servicio;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        Int64 mes = 0;
                        Int64 anio = 0;

                        if (resultado.Read())
                        {
                            if (resultado["MES"] != DBNull.Value) mes = Convert.ToInt64(resultado["MES"]);
                            if (resultado["ANIO"] != DBNull.Value) anio = Convert.ToInt64(resultado["ANIO"]);

                        }
                        dbConnectionFactory.CerrarConexion(connection);

                        if (fechaCausacion.Month == mes && fechaCausacion.Year == anio)
                            return "La línea de servicio Nro. " + cod_linea_servicio + " ya se encuentra causada en el presente mes";
                        else
                            return "";
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CausacionServiciosData", "ValidarCausacionXFecha", ex);
                        return "";
                    }
                }
            }
        }

    }
}
