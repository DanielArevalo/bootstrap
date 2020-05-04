using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Integracion.Entities;
 
namespace Xpinn.Integracion.Data
{
    public class NotificacionData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public NotificacionData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public Notificacion ConsultarEnviador(Notificacion noti, Usuario usuario)
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
                            if (resultado["E_mail"] != DBNull.Value) noti.enviador_email = Convert.ToString(resultado["E_mail"]);
                            if (resultado["Clavecorreo"] != DBNull.Value) noti.enviador_clave = Convert.ToString(resultado["Clavecorreo"]).Trim();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return noti;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NotificacionData", "ConsultarEnviador", ex);
                        noti.error += "Error: " + ex.Message;
                        return noti;
                    }
                }
            }
        }

        public Notificacion ObtenerFormatoAEnviar(Notificacion noti, Usuario usuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select * from Formatodocumentoscorreo where tipo in ('" + noti.cod_email +"','"+ noti.cod_mensaje + "')";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            int codigo = 0;
                            if (resultado["TIPO"] != DBNull.Value) codigo = Convert.ToInt32(resultado["TIPO"]);
                            if(codigo == noti.cod_email)
                            {
                                if (resultado["TEXTO"] != DBNull.Value)
                                    noti.correo_texto = Convert.ToString(resultado["TEXTO"]).Trim();
                                else
                                    noti.enviar_email = false;
                            }
                            if (codigo == noti.cod_mensaje)
                            {
                                if (resultado["TEXTO"] != DBNull.Value)
                                    noti.mensaje_texto = Convert.ToString(resultado["TEXTO"]).Trim();
                                else
                                    noti.enviar_mensaje = false;
                            }
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return noti;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Formato_NotificacionData", "ConsultarEnviador", ex);
                        noti.error += "Error: " + ex.Message;
                        return noti;
                    }
                }
            }
        }

        public Notificacion ConsultarRemitente(Notificacion noti, Usuario usuario)
        {
            DbDataReader resultado;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select nombre,email,celular from V_Persona where cod_persona = " + noti.cod_persona;
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {                            
                            if (resultado["email"] != DBNull.Value) noti.email = Convert.ToString(resultado["email"]).Trim();
                            if (resultado["celular"] != DBNull.Value) noti.celular = Convert.ToString(resultado["celular"]).Trim();
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return noti;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("Formato_NotificacionData", "ConsultarRemitente", ex);
                        noti.error += "Error: " + ex.Message;
                        return noti;
                    }
                }
            }
        }

        public void guardarNotificacion(Notificacion noti, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Parameters.Clear();

                        DbParameter P_COD_PERSONA = cmdTransaccionFactory.CreateParameter();
                        P_COD_PERSONA.ParameterName = "P_COD_PERSONA";
                        P_COD_PERSONA.Value = noti.cod_persona;
                        P_COD_PERSONA.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_COD_PERSONA);

                        DbParameter P_MENSAJE = cmdTransaccionFactory.CreateParameter();
                        P_MENSAJE.ParameterName = "P_MENSAJE";
                        P_MENSAJE.Value = noti.enviar_mensaje ? 1 : 0;
                        P_MENSAJE.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_MENSAJE);

                        DbParameter P_CORREO = cmdTransaccionFactory.CreateParameter();
                        P_CORREO.ParameterName = "P_CORREO";
                        P_CORREO.Value = noti.enviar_email ? 1 : 0;
                        P_CORREO.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_CORREO);

                        DbParameter P_DESCRIPCION = cmdTransaccionFactory.CreateParameter();
                        P_DESCRIPCION.ParameterName = "P_DESCRIPCION";
                        P_DESCRIPCION.Value = noti.descripcion;
                        P_DESCRIPCION.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_DESCRIPCION);

                        DbParameter P_CELULAR = cmdTransaccionFactory.CreateParameter();
                        P_CELULAR.ParameterName = "P_CELULAR";
                        P_CELULAR.Value = noti.celular;
                        P_CELULAR.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_CELULAR);

                        DbParameter P_EMAIL = cmdTransaccionFactory.CreateParameter();
                        P_EMAIL.ParameterName = "P_EMAIL";
                        P_EMAIL.Value = noti.email;
                        P_EMAIL.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(P_EMAIL);
                        
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_INT_NOTI_CRE";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);                        
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("NotificacionData", "guardarNotificacion", ex);
                    }
                }
            }
        }
        
    }
}
