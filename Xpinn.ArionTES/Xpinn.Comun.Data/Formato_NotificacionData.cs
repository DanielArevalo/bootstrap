using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Comun.Entities;

namespace Xpinn.Comun.Data
{
    public class Formato_NotificacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla General
        /// </summary>
        public Formato_NotificacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Formato_Notificacion ConsultarEnviador(Formato_Notificacion noti, Usuario usuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select E_mail, Clavecorreo from empresa where rownum = 1";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["E_mail"] != DBNull.Value) noti.enviador = Convert.ToString(resultado["E_mail"]);
                            if (resultado["Clavecorreo"] != DBNull.Value) noti.claveEnviador = Convert.ToString(resultado["Clavecorreo"]).Trim();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return noti;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Formato_NotificacionData", "ConsultarEnviador", ex);
                        noti.mensaje = "Error: " + ex.Message;
                        return noti;
                    }
                }
            }
        }

        public Formato_Notificacion ObtenerFormatoAEnviar(Formato_Notificacion noti, Usuario usuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from Formatodocumentoscorreo where tipo = " + noti.codigo+ "";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["DESCRIPCION"] != DBNull.Value) noti.nombre = Convert.ToString(resultado["DESCRIPCION"]).Trim();
                            if (resultado["TEXTO"] != DBNull.Value)
                            {
                                noti.texto = Convert.ToString(resultado["TEXTO"]).Trim();
                                noti.textoBase = noti.texto;
                            }
                            if (resultado["COD_DOCUMENTO"] != DBNull.Value) noti.codigo = Convert.ToInt32(resultado["COD_DOCUMENTO"]);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return noti;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Formato_NotificacionData", "ConsultarEnviador", ex);
                        noti.mensaje = "Error: " + ex.Message;
                        return noti;
                    }
                }
            }
        }

        public Formato_Notificacion ConsultarRemitente(Formato_Notificacion noti, Usuario usuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select nombre,email from V_Persona where cod_persona = "+noti.cod_persona; 
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["nombre"] != DBNull.Value) noti.receptor = Convert.ToString(resultado["nombre"]);
                            if (resultado["email"] != DBNull.Value) noti.emailReceptor = Convert.ToString(resultado["email"]).Trim();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return noti;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Formato_NotificacionData", "ConsultarRemitente", ex);
                        noti.mensaje = "Error: " + ex.Message;
                        return noti;
                    }
                }
            }
        }

        public void AlmacenarHistorialEnvio(Formato_Notificacion noti, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {                        
                        DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                        p_cod_persona.ParameterName = "p_cod_persona";
                        p_cod_persona.Value = noti.cod_persona;
                        p_cod_persona.DbType = DbType.Int32;
                        p_cod_persona.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                        DbParameter p_codigo_noti = cmdTransaccionFactory.CreateParameter();
                        p_codigo_noti.ParameterName = "p_codigo";
                        p_codigo_noti.Value = noti.codigo;
                        p_codigo_noti.DbType = DbType.Int32;
                        p_codigo_noti.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_codigo_noti);

                        DbParameter p_email = cmdTransaccionFactory.CreateParameter();
                        p_email.ParameterName = "p_email";
                        p_email.Value = noti.emailReceptor;
                        p_email.DbType = DbType.String;
                        p_email.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_email);

                        DbParameter p_fecha_consulta = cmdTransaccionFactory.CreateParameter();
                        p_fecha_consulta.ParameterName = "p_fecha_consulta";
                        if (noti.fecha_consulta == null)
                            p_fecha_consulta.Value = DBNull.Value;
                        else
                            p_fecha_consulta.Value = noti.fecha_consulta;
                        p_fecha_consulta.DbType = DbType.Date;
                        p_fecha_consulta.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(p_fecha_consulta);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_COM_HIST_NOTI_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CiereaData", "AlmacenarHistorialEnvio", ex);
                    }
                }
            }
        }
    }
}
