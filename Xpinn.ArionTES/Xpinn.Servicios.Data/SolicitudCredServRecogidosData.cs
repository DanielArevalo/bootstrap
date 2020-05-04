using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Servicios.Entities;

namespace Xpinn.Servicios.Data
{
    public class SolicitudCredServRecogidosData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public SolicitudCredServRecogidosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public SolicitudCredServRecogidos CrearSolicitudCredServRecogidos(SolicitudCredServRecogidos pSolicitudCredServRecogidos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pSolicitudCredServRecogidos.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pnumeroradicacion = cmdTransaccionFactory.CreateParameter();
                        pnumeroradicacion.ParameterName = "p_numeroradicacion";
                        pnumeroradicacion.Value = pSolicitudCredServRecogidos.numeroradicacion;
                        pnumeroradicacion.Direction = ParameterDirection.Input;
                        pnumeroradicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumeroradicacion);

                        DbParameter pnumeroservicio = cmdTransaccionFactory.CreateParameter();
                        pnumeroservicio.ParameterName = "p_numeroservicio";
                        pnumeroservicio.Value = pSolicitudCredServRecogidos.numeroservicio;
                        pnumeroservicio.Direction = ParameterDirection.Input;
                        pnumeroservicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumeroservicio);

                        DbParameter pvalorrecoger = cmdTransaccionFactory.CreateParameter();
                        pvalorrecoger.ParameterName = "p_valorrecoger";
                        pvalorrecoger.Value = pSolicitudCredServRecogidos.valorrecoger;
                        pvalorrecoger.Direction = ParameterDirection.Input;
                        pvalorrecoger.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorrecoger);

                        DbParameter pfecharecoger = cmdTransaccionFactory.CreateParameter();
                        pfecharecoger.ParameterName = "p_fecharecoger";
                        pfecharecoger.Value = pSolicitudCredServRecogidos.fecharecoger;
                        pfecharecoger.Direction = ParameterDirection.Input;
                        pfecharecoger.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharecoger);

                        DbParameter psaldoservicio = cmdTransaccionFactory.CreateParameter();
                        psaldoservicio.ParameterName = "p_saldoservicio";
                        psaldoservicio.Value = pSolicitudCredServRecogidos.saldoservicio;
                        psaldoservicio.Direction = ParameterDirection.Input;
                        psaldoservicio.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldoservicio);

                        DbParameter pinteressevicio = cmdTransaccionFactory.CreateParameter();
                        pinteressevicio.ParameterName = "p_interessevicio";
                        pinteressevicio.Value = pSolicitudCredServRecogidos.interessevicio;
                        pinteressevicio.Direction = ParameterDirection.Input;
                        pinteressevicio.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pinteressevicio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_SOLICITUDC_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pSolicitudCredServRecogidos.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pSolicitudCredServRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCredServRecogidosData", "CrearSolicitudCredServRecogidos", ex);
                        return null;
                    }
                }
            }
        }


        public SolicitudCredServRecogidos ModificarSolicitudCredServRecogidos(SolicitudCredServRecogidos pSolicitudCredServRecogidos, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pSolicitudCredServRecogidos.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pnumeroradicacion = cmdTransaccionFactory.CreateParameter();
                        pnumeroradicacion.ParameterName = "p_numeroradicacion";
                        pnumeroradicacion.Value = pSolicitudCredServRecogidos.numeroradicacion;
                        pnumeroradicacion.Direction = ParameterDirection.Input;
                        pnumeroradicacion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumeroradicacion);

                        DbParameter pnumeroservicio = cmdTransaccionFactory.CreateParameter();
                        pnumeroservicio.ParameterName = "p_numeroservicio";
                        pnumeroservicio.Value = pSolicitudCredServRecogidos.numeroservicio;
                        pnumeroservicio.Direction = ParameterDirection.Input;
                        pnumeroservicio.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumeroservicio);

                        DbParameter pvalorrecoger = cmdTransaccionFactory.CreateParameter();
                        pvalorrecoger.ParameterName = "p_valorrecoger";
                        pvalorrecoger.Value = pSolicitudCredServRecogidos.valorrecoger;
                        pvalorrecoger.Direction = ParameterDirection.Input;
                        pvalorrecoger.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorrecoger);

                        DbParameter pfecharecoger = cmdTransaccionFactory.CreateParameter();
                        pfecharecoger.ParameterName = "p_fecharecoger";
                        pfecharecoger.Value = pSolicitudCredServRecogidos.fecharecoger;
                        pfecharecoger.Direction = ParameterDirection.Input;
                        pfecharecoger.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecharecoger);

                        DbParameter psaldoservicio = cmdTransaccionFactory.CreateParameter();
                        psaldoservicio.ParameterName = "p_saldoservicio";
                        psaldoservicio.Value = pSolicitudCredServRecogidos.saldoservicio;
                        psaldoservicio.Direction = ParameterDirection.Input;
                        psaldoservicio.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(psaldoservicio);

                        DbParameter pinteressevicio = cmdTransaccionFactory.CreateParameter();
                        pinteressevicio.ParameterName = "p_interessevicio";
                        pinteressevicio.Value = pSolicitudCredServRecogidos.interessevicio;
                        pinteressevicio.Direction = ParameterDirection.Input;
                        pinteressevicio.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pinteressevicio);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_SOLICITUDC_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pSolicitudCredServRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCredServRecogidosData", "ModificarSolicitudCredServRecogidos", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarSolicitudCredServRecogidos(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        SolicitudCredServRecogidos pSolicitudCredServRecogidos = new SolicitudCredServRecogidos();
                        pSolicitudCredServRecogidos = ConsultarSolicitudCredServRecogidos(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pSolicitudCredServRecogidos.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_SOLICITUDC_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCredServRecogidosData", "EliminarSolicitudCredServRecogidos", ex);
                    }
                }
            }
        }


        public SolicitudCredServRecogidos ConsultarSolicitudCredServRecogidos(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            SolicitudCredServRecogidos entidad = new SolicitudCredServRecogidos();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM SolicitudCredServRecogidos WHERE CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NUMERORADICACION"] != DBNull.Value) entidad.numeroradicacion = Convert.ToInt64(resultado["NUMERORADICACION"]);
                            if (resultado["NUMEROSERVICIO"] != DBNull.Value) entidad.numeroservicio = Convert.ToInt32(resultado["NUMEROSERVICIO"]);
                            if (resultado["VALORRECOGER"] != DBNull.Value) entidad.valorrecoger = Convert.ToDecimal(resultado["VALORRECOGER"]);
                            if (resultado["FECHARECOGER"] != DBNull.Value) entidad.fecharecoger = Convert.ToDateTime(resultado["FECHARECOGER"]);
                            if (resultado["SALDOSERVICIO"] != DBNull.Value) entidad.saldoservicio = Convert.ToDecimal(resultado["SALDOSERVICIO"]);
                            if (resultado["INTERESSEVICIO"] != DBNull.Value) entidad.interessevicio = Convert.ToDecimal(resultado["INTERESSEVICIO"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCredServRecogidosData", "ConsultarSolicitudCredServRecogidos", ex);
                        return null;
                    }
                }
            }
        }


        public List<SolicitudCredServRecogidos> ListarSolicitudCredServRecogidos(long numeroCredito, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SolicitudCredServRecogidos> lstSolicitudCredServRecogidos = new List<SolicitudCredServRecogidos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM SolicitudCredServRecogidos WHERE NUMERORADICACION = " + numeroCredito + " ORDER BY CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SolicitudCredServRecogidos entidad = new SolicitudCredServRecogidos();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NUMERORADICACION"] != DBNull.Value) entidad.numeroradicacion = Convert.ToInt64(resultado["NUMERORADICACION"]);
                            if (resultado["NUMEROSERVICIO"] != DBNull.Value) entidad.numeroservicio = Convert.ToInt32(resultado["NUMEROSERVICIO"]);
                            if (resultado["VALORRECOGER"] != DBNull.Value) entidad.valorrecoger = Convert.ToDecimal(resultado["VALORRECOGER"]);
                            if (resultado["FECHARECOGER"] != DBNull.Value) entidad.fecharecoger = Convert.ToDateTime(resultado["FECHARECOGER"]);
                            if (resultado["SALDOSERVICIO"] != DBNull.Value) entidad.saldoservicio = Convert.ToDecimal(resultado["SALDOSERVICIO"]);
                            if (resultado["INTERESSEVICIO"] != DBNull.Value) entidad.interessevicio = Convert.ToDecimal(resultado["INTERESSEVICIO"]);
                            lstSolicitudCredServRecogidos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSolicitudCredServRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCredServRecogidosData", "ListarSolicitudCredServRecogidos", ex);
                        return null;
                    }
                }
            }
        }

        public List<SolicitudCredServRecogidos> ListarSolicitudCredServRecogidosActualizado(long numeroCredito, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<SolicitudCredServRecogidos> lstSolicitudCredServRecogidos = new List<SolicitudCredServRecogidos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT rec.CONSECUTIVO, rec.NUMERORADICACION, rec.FECHARECOGER, rec.NUMEROSERVICIO, n.descripcion as nom_periodicidad, ser.NUMERO_CUOTAS, ser.VALOR_CUOTA, ser.SALDO, ser.fecha_solicitud,
                                        NVL(Calcular_TotalAPagar_Servicio(rec.NUMEROSERVICIO, SYSDATE),(select sum(valor)  from tran_servicios tr where tr.numero_servicio = rec.NUMEROSERVICIO  and  tr.TIPO_TRAN = 34)) as total, CALCULAR_TOTALINTERES_SERVICIO(rec.NUMEROSERVICIO, SYSDATE) as total_interes,
                                            case ser.forma_pago when '1' then 'Caja' when '2' then 'Nomina' end as forma_pago,
                                            l.nombre as nom_linea
                                        FROM SolicitudCredServRecogidos rec
                                        JOIN SERVICIOS ser on rec.NUMEROSERVICIO = ser.NUMERO_SERVICIO
                                        left join periodicidad n on n.cod_periodicidad = ser.cod_periodicidad 
                                        left join lineasservicios l on l.cod_linea_servicio = ser.cod_linea_servicio
                                        WHERE  rec.NUMERORADICACION = " + numeroCredito + " ORDER BY rec.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            SolicitudCredServRecogidos entidad = new SolicitudCredServRecogidos();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NUMERORADICACION"] != DBNull.Value) entidad.numeroradicacion = Convert.ToInt64(resultado["NUMERORADICACION"]);
                            if (resultado["NUMEROSERVICIO"] != DBNull.Value) entidad.numeroservicio = Convert.ToInt32(resultado["NUMEROSERVICIO"]);
                            if (resultado["FECHARECOGER"] != DBNull.Value) entidad.fecharecoger = Convert.ToDateTime(resultado["FECHARECOGER"]);

                            // Datos actualizados segun la fecha de hoy (Se calculan nuevamente)
                            if (resultado["total"] != DBNull.Value) entidad.valorrecoger = Convert.ToDecimal(resultado["total"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldoservicio = Convert.ToDecimal(resultado["SALDO"]);
                            if (resultado["total_interes"] != DBNull.Value) entidad.interessevicio = Convert.ToDecimal(resultado["total_interes"]);
                            if (resultado["nom_periodicidad"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["nom_periodicidad"]);
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) entidad.numero_cuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) entidad.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["fecha_solicitud"] != DBNull.Value) entidad.fecha_solicitud = Convert.ToDateTime(resultado["fecha_solicitud"]);
                            if (resultado["forma_pago"] != DBNull.Value) entidad.forma_pago = Convert.ToString(resultado["forma_pago"]);
                            if (resultado["nom_linea"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["nom_linea"]);

                            lstSolicitudCredServRecogidos.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstSolicitudCredServRecogidos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("SolicitudCredServRecogidosData", "ListarSolicitudCredServRecogidos", ex);
                        return null;
                    }
                }
            }
        }


    }
}