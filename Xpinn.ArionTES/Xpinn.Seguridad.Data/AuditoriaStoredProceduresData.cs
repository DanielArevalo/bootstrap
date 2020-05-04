using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Xpinn.Seguridad.Entities;
using Xpinn.Util;

namespace Xpinn.Seguridad.Data
{
    public class AuditoriaStoredProceduresData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public AuditoriaStoredProceduresData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public AuditoriaStoredProcedures CrearAuditoriaStoredProcedures(AuditoriaStoredProcedures pAuditoriaStoredProcedures, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario, false)) // Pasar false para no auditar esa ejecucion
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pAuditoriaStoredProcedures.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pnombresp = cmdTransaccionFactory.CreateParameter();
                        pnombresp.ParameterName = "p_nombresp";
                        pnombresp.Value = pAuditoriaStoredProcedures.nombresp;
                        pnombresp.Direction = ParameterDirection.Input;
                        pnombresp.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombresp);

                        DbParameter pfechaejecucion = cmdTransaccionFactory.CreateParameter();
                        pfechaejecucion.ParameterName = "p_fechaejecucion";
                        pfechaejecucion.Value = pAuditoriaStoredProcedures.fechaejecucion;
                        pfechaejecucion.Direction = ParameterDirection.Input;
                        pfechaejecucion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaejecucion);

                        DbParameter pcodigousuario = cmdTransaccionFactory.CreateParameter();
                        pcodigousuario.ParameterName = "p_codigousuario";
                        pcodigousuario.Value = pAuditoriaStoredProcedures.codigousuario;
                        pcodigousuario.Direction = ParameterDirection.Input;
                        pcodigousuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigousuario);

                        DbParameter pnombreusuario = cmdTransaccionFactory.CreateParameter();
                        pnombreusuario.ParameterName = "p_nombreusuario";
                        pnombreusuario.Value = pAuditoriaStoredProcedures.nombreusuario;
                        pnombreusuario.Direction = ParameterDirection.Input;
                        pnombreusuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombreusuario);

                        DbParameter pexitoso = cmdTransaccionFactory.CreateParameter();
                        pexitoso.ParameterName = "p_exitoso";
                        pexitoso.Value = Convert.ToInt32(pAuditoriaStoredProcedures.exitoso);
                        pexitoso.Direction = ParameterDirection.Input;
                        pexitoso.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pexitoso);

                        DbParameter pinformacionenviada = cmdTransaccionFactory.CreateParameter();
                        pinformacionenviada.ParameterName = "p_informacionenviada";
                        pinformacionenviada.Value = pAuditoriaStoredProcedures.informacionenviada;
                        pinformacionenviada.Direction = ParameterDirection.Input;
                        pinformacionenviada.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pinformacionenviada);

                        DbParameter p_codigoOpcion = cmdTransaccionFactory.CreateParameter();
                        p_codigoOpcion.ParameterName = "p_codigoOpcion";
                        if (pAuditoriaStoredProcedures.codigoOpcion.HasValue)
                        {
                            p_codigoOpcion.Value = pAuditoriaStoredProcedures.codigoOpcion;
                        }
                        else
                        {
                            p_codigoOpcion.Value = DBNull.Value;
                        }
                        p_codigoOpcion.Direction = ParameterDirection.Input;
                        p_codigoOpcion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoOpcion);

                        DbParameter p_MensajeError = cmdTransaccionFactory.CreateParameter();
                        p_MensajeError.ParameterName = "p_MensajeError";
                        if (!string.IsNullOrWhiteSpace(pAuditoriaStoredProcedures.mensaje_error))
                        {
                            p_MensajeError.Value = pAuditoriaStoredProcedures.mensaje_error;
                        }
                        else
                        {
                            p_MensajeError.Value = DBNull.Value;
                        }
                        p_MensajeError.Direction = ParameterDirection.Input;
                        p_MensajeError.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(p_MensajeError);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_AUDITORIA__CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAuditoriaStoredProcedures.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pAuditoriaStoredProcedures;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuditoriaStoredProceduresData", "CrearAuditoriaStoredProcedures", ex);
                        return null;
                    }
                }
            }
        }

        public List<string> ListarProcedimientos(string prefix, Usuario usuario)
        {
            DbDataReader resultado;
            List<string> listaProcedimientos = new List<string>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario, false))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT object_name
                                        FROM dba_objects 
                                        WHERE object_type = 'PROCEDURE' 
                                        and owner = 
                                        (
                                            SELECT USER FROM DUAL
                                        )
                                        and UPPER(object_name) LIKE '%" + prefix.ToUpper() + "%' and rownum <= 10 ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            string nombreProcedimiento = string.Empty;

                            if (resultado["object_name"] != DBNull.Value) nombreProcedimiento = Convert.ToString(resultado["object_name"]);

                            if (!string.IsNullOrWhiteSpace(nombreProcedimiento))
                            {
                                listaProcedimientos.Add(nombreProcedimiento);
                            }
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return listaProcedimientos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuditoriaStoredProceduresData", "ListarProcedimientos", ex);
                        return null;
                    }
                }
            }
        }

        public AuditoriaStoredProcedures ConsultarAuditoriaStoredProcedures(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            AuditoriaStoredProcedures entidad = new AuditoriaStoredProcedures();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT aud.*, opci.nombre as nombre_opcion
                                       FROM Auditoria_StoredProcedures aud
                                       Left join opciones opci on aud.codigoopcion = opci.cod_opcion 
                                       WHERE aud.CONSECUTIVO = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NOMBRESP"] != DBNull.Value) entidad.nombresp = Convert.ToString(resultado["NOMBRESP"]);
                            if (resultado["FECHAEJECUCION"] != DBNull.Value) entidad.fechaejecucion = Convert.ToDateTime(resultado["FECHAEJECUCION"]);
                            if (resultado["CODIGOUSUARIO"] != DBNull.Value) entidad.codigousuario = Convert.ToInt32(resultado["CODIGOUSUARIO"]);
                            if (resultado["NOMBREUSUARIO"] != DBNull.Value) entidad.nombreusuario = Convert.ToString(resultado["NOMBREUSUARIO"]);
                            if (resultado["EXITOSO"] != DBNull.Value) entidad.exitoso = Convert.ToBoolean(resultado["EXITOSO"]);
                            if (resultado["nombre_opcion"] != DBNull.Value) entidad.nombre_opcion = Convert.ToString(resultado["nombre_opcion"]);
                            if (resultado["CodigoOpcion"] != DBNull.Value) entidad.codigoOpcion = Convert.ToInt64(resultado["CodigoOpcion"]);
                            if (resultado["INFORMACIONENVIADA"] != DBNull.Value) entidad.informacionenviada = Convert.ToString(resultado["INFORMACIONENVIADA"]);
                            if (resultado["MENSAJEERROR"] != DBNull.Value) entidad.mensaje_error = Convert.ToString(resultado["MENSAJEERROR"]);
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
                        BOExcepcion.Throw("AuditoriaStoredProceduresData", "ConsultarAuditoriaStoredProcedures", ex);
                        return null;
                    }
                }
            }
        }


        public List<AuditoriaStoredProcedures> ListarAuditoriaStoredProcedures(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<AuditoriaStoredProcedures> lstAuditoriaStoredProcedures = new List<AuditoriaStoredProcedures>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT aud.*, opci.nombre as nombre_opcion
                                       FROM Auditoria_StoredProcedures aud
                                       LEFT JOIN opciones opci on aud.codigoopcion = opci.cod_opcion
                                       " + filtro + " ORDER BY 3 desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            AuditoriaStoredProcedures entidad = new AuditoriaStoredProcedures();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["NOMBRESP"] != DBNull.Value) entidad.nombresp = Convert.ToString(resultado["NOMBRESP"]);
                            if (resultado["FECHAEJECUCION"] != DBNull.Value) entidad.fechaejecucion = Convert.ToDateTime(resultado["FECHAEJECUCION"]);
                            if (resultado["CODIGOUSUARIO"] != DBNull.Value) entidad.codigousuario = Convert.ToInt32(resultado["CODIGOUSUARIO"]);
                            if (resultado["NOMBREUSUARIO"] != DBNull.Value) entidad.nombreusuario = Convert.ToString(resultado["NOMBREUSUARIO"]);
                            if (resultado["EXITOSO"] != DBNull.Value) entidad.exitoso = Convert.ToBoolean(resultado["EXITOSO"]);
                            if (resultado["nombre_opcion"] != DBNull.Value) entidad.nombre_opcion = Convert.ToString(resultado["nombre_opcion"]);
                            if (resultado["CodigoOpcion"] != DBNull.Value) entidad.codigoOpcion = Convert.ToInt64(resultado["CodigoOpcion"]);
                            if (resultado["MENSAJEERROR"] != DBNull.Value) entidad.mensaje_error = Convert.ToString(resultado["MENSAJEERROR"]);

                            lstAuditoriaStoredProcedures.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstAuditoriaStoredProcedures;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuditoriaStoredProceduresData", "ListarAuditoriaStoredProcedures", ex);
                        return null;
                    }
                }
            }
        }

        public Int64 CrearUsuarioSessionID(Usuario pUsuario)
        {
            Int64 sessionID = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario, false)) // Pasar false para no auditar esa ejecucion
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcodigousuario = cmdTransaccionFactory.CreateParameter();
                        pcodigousuario.ParameterName = "p_codigousuario";
                        pcodigousuario.Value = pUsuario.codusuario;
                        pcodigousuario.Direction = ParameterDirection.Input;
                        pcodigousuario.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigousuario);

                        DbParameter pnombreusuario = cmdTransaccionFactory.CreateParameter();
                        pnombreusuario.ParameterName = "p_nombreusuario";
                        pnombreusuario.Value = pUsuario.nombre;
                        pnombreusuario.Direction = ParameterDirection.Input;
                        pnombreusuario.DbType = DbType.String;
                        cmdTransaccionFactory.Parameters.Add(pnombreusuario);

                        DbParameter psessionid = cmdTransaccionFactory.CreateParameter();
                        psessionid.ParameterName = "p_sessionid";
                        psessionid.Value = 0;
                        psessionid.Direction = ParameterDirection.Output;
                        psessionid.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(psessionid);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_SEG_SESIONID_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        sessionID = psessionid.Value != DBNull.Value ? Convert.ToInt64(psessionid.Value) : 0;

                        return sessionID;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("AuditoriaStoredProceduresData", "CrearUsuarioSessionID", ex);
                        return sessionID;
                    }
                }
            }
        }



    }
}