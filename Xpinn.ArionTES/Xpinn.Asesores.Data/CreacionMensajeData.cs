using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xpinn.Asesores.Entities;
using Xpinn.Util;

namespace Xpinn.Asesores.Data
{
    public class CreacionMensajeData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public CreacionMensajeData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }


        public CreacionMensaje CrearMensaje(CreacionMensaje pCreacionMensaje, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pIdMensaje = cmdTransaccionFactory.CreateParameter();
                        pIdMensaje.Direction = ParameterDirection.Output;
                        pIdMensaje.ParameterName = "PIdMensaje";
                        pIdMensaje.Value = pCreacionMensaje.IdMensaje;
                        pIdMensaje.DbType = DbType.Int32;
                        pIdMensaje.Direction = ParameterDirection.Output;

                        DbParameter PDescripcion = cmdTransaccionFactory.CreateParameter();
                        PDescripcion.ParameterName = "PDescripcion";
                        PDescripcion.Value = pCreacionMensaje.Descripcion;
                        PDescripcion.DbType = DbType.String;
                        PDescripcion.Direction = ParameterDirection.Input;

                        DbParameter PEstado = cmdTransaccionFactory.CreateParameter();
                        PEstado.ParameterName = "PEstado";
                        PEstado.Value = pCreacionMensaje.IdEstado;
                        PEstado.DbType = DbType.Int32;
                        PEstado.Direction = ParameterDirection.Input;

                        DbParameter PUser = cmdTransaccionFactory.CreateParameter();
                        PUser.ParameterName = "PUser";
                        PUser.Value = pUsuario.codusuario;
                        PUser.DbType = DbType.Int32;
                        PUser.Direction = ParameterDirection.Input;


                        cmdTransaccionFactory.Parameters.Add(pIdMensaje);
                        cmdTransaccionFactory.Parameters.Add(PDescripcion);
                        cmdTransaccionFactory.Parameters.Add(PEstado);
                        cmdTransaccionFactory.Parameters.Add(PUser);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_MENSAJESAPP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        pCreacionMensaje.IdMensaje = Convert.ToInt64(pIdMensaje.Value);
                        return pCreacionMensaje;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreacionMensajeData", "CrearMensaje", ex);
                        return null;
                    }
                }
            }
        }
        public List<CreacionMensaje> ListarMensajesApp(CreacionMensaje PcreacionMensaje, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<CreacionMensaje> lstCreacionMensaje = new List<CreacionMensaje>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT ID_MENSAJE,DESCRIPCION, CASE WHEN ESTADO=0 THEN 'CREADO' ELSE 'LEIDO' END AS ESTADO_DESC FROM  MENSAJESAPP" + ObtenerFiltro(PcreacionMensaje);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            CreacionMensaje entidad = new CreacionMensaje();

                            if (resultado["ID_MENSAJE"] != DBNull.Value) entidad.IdMensaje = Convert.ToInt64(resultado["ID_MENSAJE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO_DESC"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["ESTADO_DESC"]);

                            lstCreacionMensaje.Add(entidad);
                        }
                        return lstCreacionMensaje;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreacionMensajeData", "ListarMensajesApp", ex);
                        return null;
                    }
                }
            }
        }

        public CreacionMensaje CrearMensajePersona(CreacionMensaje pCreacionMensaje, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pDocumento = cmdTransaccionFactory.CreateParameter();
                        pDocumento.Direction = ParameterDirection.Output;
                        pDocumento.ParameterName = "pDocumento";
                        pDocumento.Value = pCreacionMensaje.documentoPersona;
                        pDocumento.DbType = DbType.String;
                        pDocumento.Direction = ParameterDirection.Input;

                        DbParameter PUser = cmdTransaccionFactory.CreateParameter();
                        PUser.ParameterName = "PUser";
                        PUser.Value = pUsuario.codusuario;
                        PUser.DbType = DbType.Int32;
                        PUser.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pDocumento);
                        cmdTransaccionFactory.Parameters.Add(PUser);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_MENSAJESPER_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pCreacionMensaje;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreacionMensajeData", "CrearMensaje", ex);
                        return null;
                    }
                }
            }
        }
        public List<PesonasTemp> ConsultarPersonasTemp(string pId, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<PesonasTemp> lstPesonasTemp = new List<PesonasTemp>();
            string sql = "";
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        //if (pId == 0)
                        //{
                        //    sql = @"SELECT 
                        //               P.IDENTIFICACION,
                        //               P.PRIMER_NOMBRE || ' ' || P.SEGUNDO_NOMBRE || ' ' || P.PRIMER_APELLIDO || ' ' || P.SEGUNDO_APELLIDO AS NOMBRE_COMPLETO 
                        //               FROM MENSAJESAPP_PERSONAS MP INNER JOIN PERSONA P ON MP.COD_PERSONA = P.COD_PERSONA 
                        //               WHERE MP.ID_MENSAJE IS NULL AND MP.USUARIO_ID = " + pUsuario.codusuario;
                        //}
                        //else
                        //{
                        //    sql = @"SELECT 
                        //               P.IDENTIFICACION,
                        //               P.PRIMER_NOMBRE || ' ' || P.SEGUNDO_NOMBRE || ' ' || P.PRIMER_APELLIDO || ' ' || P.SEGUNDO_APELLIDO AS NOMBRE_COMPLETO 
                        //               FROM MENSAJESAPP_PERSONAS MP INNER JOIN PERSONA P ON MP.COD_PERSONA = P.COD_PERSONA 
                        //               WHERE MP.ID_MENSAJE=" + pId;
                        //}

                        sql = @"SELECT 
                        P.IDENTIFICACION,
                        P.PRIMER_NOMBRE || ' ' || P.SEGUNDO_NOMBRE || ' ' || P.PRIMER_APELLIDO || ' ' || P.SEGUNDO_APELLIDO AS NOMBRE_COMPLETO 
                        FROM MENSAJESAPP_PERSONAS MP INNER JOIN PERSONA P ON MP.COD_PERSONA = P.COD_PERSONA 
                        WHERE ((MP.ID_MENSAJE IS NULL)OR(MP.ID_MENSAJE='" + pId + "')) AND MP.USUARIO_ID ='" + pUsuario.codusuario + "'";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            PesonasTemp entidad = new PesonasTemp();

                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.documento = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE_COMPLETO"] != DBNull.Value) entidad.Nombre = Convert.ToString(resultado["NOMBRE_COMPLETO"]);

                            lstPesonasTemp.Add(entidad);
                        }
                        return lstPesonasTemp;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreacionMensajeData", "ListarMensajesApp", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarMensaje(Int64 pId, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CreacionMensaje pCreacionMensaje = new CreacionMensaje();

                        DbParameter pIdmensaje = cmdTransaccionFactory.CreateParameter();
                        pIdmensaje.ParameterName = "pIdmensaje";
                        pIdmensaje.Value = pId;
                        pIdmensaje.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pIdmensaje);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_MENSAJESAPP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreacionMensajeData", "EliminarMensaje", ex);
                    }
                }
            }
        }

        public CreacionMensaje ConsultarMensajes(Int64 pId, Usuario pUsuario)
        {
            DbDataReader resultado;
            CreacionMensaje entidad = new CreacionMensaje();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT ID_MENSAJE,DESCRIPCION, CASE WHEN ESTADO=1 THEN 'CREADO' ELSE 'LEIDO' END AS ESTADO_DESC FROM  MENSAJESAPP WHERE ID_MENSAJE = " + pId.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ID_MENSAJE"] != DBNull.Value) entidad.IdMensaje = Convert.ToInt64(resultado["ID_MENSAJE"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.Descripcion = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["ESTADO_DESC"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["ESTADO_DESC"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreacionMensajeData", "ConsultarMensajes", ex);
                        return null;
                    }
                }
            }
        }

        public CreacionMensaje ModificarMensajes(CreacionMensaje pCreacionMensaje, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter PIdMensaje = cmdTransaccionFactory.CreateParameter();
                        PIdMensaje.ParameterName = "PIdMensaje";
                        PIdMensaje.Value = pCreacionMensaje.IdMensaje;
                        PIdMensaje.DbType = DbType.Int32;
                        PIdMensaje.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PIdMensaje);

                        DbParameter PDescripcion = cmdTransaccionFactory.CreateParameter();
                        PDescripcion.ParameterName = "PDescripcion";
                        PDescripcion.Value = pCreacionMensaje.Descripcion;
                        PDescripcion.DbType = DbType.String;
                        PDescripcion.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PDescripcion);

                        DbParameter PEstado = cmdTransaccionFactory.CreateParameter();
                        PEstado.ParameterName = "PEstado";
                        PEstado.Value = pCreacionMensaje.IdEstado;
                        PEstado.DbType = DbType.Int32;
                        PEstado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PEstado);

                        DbParameter PUser = cmdTransaccionFactory.CreateParameter();
                        PUser.ParameterName = "PUser";
                        PUser.Value = pUsuario.codusuario;
                        PUser.DbType = DbType.Int32;
                        PUser.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(PUser);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_MENSAJESAPP_MODIF";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        return pCreacionMensaje;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreacionMensajeData", "ModificarMensajes", ex);
                        return null;
                    }
                }
            }
        }

        public void EliminarPersonaMensajeTemp(Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        CreacionMensaje pCreacionMensaje = new CreacionMensaje();

                        DbParameter PUser = cmdTransaccionFactory.CreateParameter();
                        PUser.ParameterName = "PUser";
                        PUser.Value = pUsuario.codusuario;
                        PUser.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(PUser);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_PERMSMTEMP_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("CreacionMensajeData", "EliminarPersonaMensajeTemp", ex);
                    }
                }
            }
        }
    }
}
