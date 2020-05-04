using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Servicios.Entities;

namespace Xpinn.Servicios.Data
{
    public class ReclamacionServiciosData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public ReclamacionServiciosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }
        public ReclamacionServicios Crearservicios( DateTime pFechaIni,ReclamacionServicios pservicios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pideclamacion = cmdTransaccionFactory.CreateParameter();
                        pideclamacion.ParameterName = "p_ideclamacion";
                        pideclamacion.Value = pservicios.ideclamacion;
                        pideclamacion.Direction = ParameterDirection.Input;
                        pideclamacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pideclamacion);

                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pservicios.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pfecha_reclamacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_reclamacion.ParameterName = "p_fecha_reclamacion";
                        pfecha_reclamacion.Value = pservicios.fecha_reclamacion;
                        pfecha_reclamacion.Direction = ParameterDirection.Input;
                        pfecha_reclamacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_reclamacion);

                        DbParameter pidentificacion_fallecido = cmdTransaccionFactory.CreateParameter();
                        pidentificacion_fallecido.ParameterName = "p_identificacion_fallecido";
                        if (pservicios.identificacion_fallecido == null)
                            pidentificacion_fallecido.Value = DBNull.Value;
                        else
                            pidentificacion_fallecido.Value = pservicios.identificacion_fallecido;
                        pidentificacion_fallecido.Direction = ParameterDirection.Input;
                        pidentificacion_fallecido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion_fallecido);

                        DbParameter pnombre_fallecido = cmdTransaccionFactory.CreateParameter();
                        pnombre_fallecido.ParameterName = "p_nombre_fallecido";
                        if (pservicios.nombre_fallecido == null)
                            pnombre_fallecido.Value = DBNull.Value;
                        else
                            pnombre_fallecido.Value = pservicios.nombre_fallecido;
                        pnombre_fallecido.Direction = ParameterDirection.Input;
                        pnombre_fallecido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_fallecido);

                        DbParameter pcodparentesco = cmdTransaccionFactory.CreateParameter();
                        pcodparentesco.ParameterName = "p_codparentesco";
                        if (pservicios.codparentesco == null)
                            pcodparentesco.Value = DBNull.Value;
                        else
                            pcodparentesco.Value = pservicios.codparentesco;
                        pcodparentesco.Direction = ParameterDirection.Input;
                        pcodparentesco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodparentesco);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pservicios.observaciones == null)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pservicios.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pfechacrea = cmdTransaccionFactory.CreateParameter();
                        pfechacrea.ParameterName = "p_fechacrea";
                        pfechacrea.Value = pFechaIni;
                        pfechacrea.Direction = ParameterDirection.Input;
                        pfechacrea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacrea);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        if (pservicios.codusuario == null)
                            pcodusuario.Value = DBNull.Value;
                        else
                            pcodusuario.Value = pservicios.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_RECLAMACIO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pservicios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("serviciosData", "Crearservicios", ex);
                        return null;
                    }
                }
            }
        }


        public ReclamacionServicios Modificarservicios(DateTime pFechaIni ,ReclamacionServicios pservicios, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pideclamacion = cmdTransaccionFactory.CreateParameter();
                        pideclamacion.ParameterName = "p_ideclamacion";
                        pideclamacion.Value = pservicios.ideclamacion;
                        pideclamacion.Direction = ParameterDirection.Input;
                        pideclamacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pideclamacion);

                        DbParameter pnumero_servicio = cmdTransaccionFactory.CreateParameter();
                        pnumero_servicio.ParameterName = "p_numero_servicio";
                        pnumero_servicio.Value = pservicios.numero_servicio;
                        pnumero_servicio.Direction = ParameterDirection.Input;
                        pnumero_servicio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_servicio);

                        DbParameter pfecha_reclamacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_reclamacion.ParameterName = "p_fecha_reclamacion";
                        pfecha_reclamacion.Value = pservicios.fecha_reclamacion;
                        pfecha_reclamacion.Direction = ParameterDirection.Input;
                        pfecha_reclamacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfecha_reclamacion);

                        DbParameter pidentificacion_fallecido = cmdTransaccionFactory.CreateParameter();
                        pidentificacion_fallecido.ParameterName = "p_identificacion_fallecido";
                        if (pservicios.identificacion_fallecido == null)
                            pidentificacion_fallecido.Value = DBNull.Value;
                        else
                            pidentificacion_fallecido.Value = pservicios.identificacion_fallecido;
                        pidentificacion_fallecido.Direction = ParameterDirection.Input;
                        pidentificacion_fallecido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pidentificacion_fallecido);

                        DbParameter pnombre_fallecido = cmdTransaccionFactory.CreateParameter();
                        pnombre_fallecido.ParameterName = "p_nombre_fallecido";
                        if (pservicios.nombre_fallecido == null)
                            pnombre_fallecido.Value = DBNull.Value;
                        else
                            pnombre_fallecido.Value = pservicios.nombre_fallecido;
                        pnombre_fallecido.Direction = ParameterDirection.Input;
                        pnombre_fallecido.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombre_fallecido);

                        DbParameter pcodparentesco = cmdTransaccionFactory.CreateParameter();
                        pcodparentesco.ParameterName = "p_codparentesco";
                        if (pservicios.codparentesco == null)
                            pcodparentesco.Value = DBNull.Value;
                        else
                            pcodparentesco.Value = pservicios.codparentesco;
                        pcodparentesco.Direction = ParameterDirection.Input;
                        pcodparentesco.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodparentesco);

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "p_observaciones";
                        if (pservicios.observaciones == null)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = pservicios.observaciones;
                        pobservaciones.Direction = ParameterDirection.Input;
                        pobservaciones.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);

                        DbParameter pfechacrea = cmdTransaccionFactory.CreateParameter();
                        pfechacrea.ParameterName = "p_fechacrea";
                        pfechacrea.Value = pFechaIni;
                        pfechacrea.Direction = ParameterDirection.Input;
                        pfechacrea.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechacrea);

                        DbParameter pcodusuario = cmdTransaccionFactory.CreateParameter();
                        pcodusuario.ParameterName = "p_codusuario";
                        if (pservicios.codusuario == null)
                            pcodusuario.Value = DBNull.Value;
                        else
                            pcodusuario.Value = pservicios.codusuario;
                        pcodusuario.Direction = ParameterDirection.Input;
                        pcodusuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodusuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_RECLAMACIO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pservicios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("serviciosData", "Modificarservicios", ex);
                        return null;
                    }
                }
            }
        }


        public void Eliminarservicios(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        ReclamacionServicios pservicios = new ReclamacionServicios();
                        pservicios = Consultarservicios(pId, vUsuario);

                        DbParameter pideclamacion = cmdTransaccionFactory.CreateParameter();
                        pideclamacion.ParameterName = "p_ideclamacion";
                        pideclamacion.Value = pservicios.ideclamacion;
                        pideclamacion.Direction = ParameterDirection.Input;
                        pideclamacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pideclamacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SER_RECLAMACIO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("serviciosData", "Eliminarservicios", ex);
                    }
                }
            }
        }


        public ReclamacionServicios Consultarservicios(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            ReclamacionServicios entidad = new ReclamacionServicios();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM RECLAMACION_SERVICIO WHERE NUMERO_SERVICIO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["IDECLAMACION"] != DBNull.Value) entidad.ideclamacion = Convert.ToInt32(resultado["IDECLAMACION"]);
                            if (resultado["NUMERO_SERVICIO"] != DBNull.Value) entidad.numero_servicio = Convert.ToInt32(resultado["NUMERO_SERVICIO"]);
                            if (resultado["FECHA_RECLAMACION"] != DBNull.Value) entidad.fecha_reclamacion = Convert.ToDateTime(resultado["FECHA_RECLAMACION"]);
                            if (resultado["IDENTIFICACION_FALLECIDO"] != DBNull.Value) entidad.identificacion_fallecido = Convert.ToString(resultado["IDENTIFICACION_FALLECIDO"]);
                            if (resultado["NOMBRE_FALLECIDO"] != DBNull.Value) entidad.nombre_fallecido = Convert.ToString(resultado["NOMBRE_FALLECIDO"]);
                            if (resultado["CODPARENTESCO"] != DBNull.Value) entidad.codparentesco = Convert.ToInt32(resultado["CODPARENTESCO"]);
                            if (resultado["OBSERVACIONES"] != DBNull.Value) entidad.observaciones = Convert.ToString(resultado["OBSERVACIONES"]);
                            if (resultado["FECHACREA"] != DBNull.Value) entidad.fechacrea = Convert.ToDateTime(resultado["FECHACREA"]);
                            if (resultado["CODUSUARIO"] != DBNull.Value) entidad.codusuario = Convert.ToInt32(resultado["CODUSUARIO"]);
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
                        BOExcepcion.Throw("serviciosData", "Consultarservicios", ex);
                        return null;
                    }
                }
            }
        }


        public List<ReclamacionServicios> Listarservicios(string filtro, string pOrden, DateTime pFechaIni, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<ReclamacionServicios> lstservicios = new List<ReclamacionServicios>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select reclamacion_servicio.fecha_reclamacion,reclamacion_servicio.fechacrea,reclamacion_servicio.identificacion_fallecido,reclamacion_servicio.nombre_fallecido,s.numero_servicio,s.fecha_solicitud,s.cod_persona, p.identificacion, "
                                        + " p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido||' '||p.segundo_apellido as Nombre, p.cod_nomina,"
                                        + " l.nombre as nom_linea,z.nombre as nom_Plan,s.num_poliza,s.fecha_inicio_vigencia,s.fecha_final_vigencia, "
                                        + " s.valor_total,s.fecha_primera_cuota,s.numero_cuotas,s.valor_cuota,n.descripcion as nom_Periodicidad, "
                                        + " case s.forma_pago when '1' then 'Caja' when '2' then 'Nomina' end as forma_pago, "
                                        + " s.identificacion_titular,s.nombre_titular "
                                        + " from servicios s "
                                        + " inner join RECLAMACION_SERVICIO on RECLAMACION_SERVICIO.numero_servicio=s.numero_servicio "
                                        + " LEFT join persona p on p.cod_persona = s.cod_persona "
                                        + " LEFT join lineasservicios l on l.cod_linea_servicio = s.cod_linea_servicio "
                                        + " LEFT join planservicios z on z.cod_plan_servicio=s.cod_plan_servicio "
                                        + " LEFT join periodicidad n on n.cod_periodicidad = s.cod_periodicidad  where 1=1 " + filtro;

                        Configuracion conf = new Configuracion();
                        if (pFechaIni != null && pFechaIni != DateTime.MinValue)
                        {
                            if (sql.ToUpper().Contains("WHERE"))
                                sql += " And ";
                            else
                                sql += " Where ";
                            if (dbConnectionFactory.TipoConexion() == "ORACLE")
                                sql += " reclamacion_servicio.fecha_reclamacion = To_Date('" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "')";
                            else
                                sql += " reclamacion_servicio.fecha_reclamacion = '" + Convert.ToDateTime(pFechaIni).ToString(conf.ObtenerFormatoFecha()) + "' ";
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
                            ReclamacionServicios entidad = new ReclamacionServicios();
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
                            lstservicios.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstservicios;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("serviciosData", "Listarservicios", ex);
                        return null;
                    }
                }
            }
        }


        public bool ValidarFallecido(string pIdentificacion, Int64? pIdReclamacion, Usuario pUsuario)
        {
            DbDataReader resultado;
            ReclamacionServicios entidad = new ReclamacionServicios();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pIdReclamacion == null)
                            sql = @"SELECT * FROM RECLAMACION_SERVICIO WHERE IDENTIFICACION_FALLECIDO = '" + pIdentificacion.ToString() + "' ";
                        else
                            sql = @"SELECT * FROM RECLAMACION_SERVICIO WHERE IDENTIFICACION_FALLECIDO = '" + pIdentificacion.ToString() + "' AND IDECLAMACION != " + pIdReclamacion.ToString(); ;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            return true;
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("serviciosData", "Consultarservicios", ex);
                        return true;
                    }
                }
            }
        }

    }
}